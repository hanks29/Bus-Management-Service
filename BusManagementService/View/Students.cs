using System;
using System.Data;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace BusManagementService
{
    public partial class Students : Form
    {
        private MongoConnection mongoConnection;

        public Students()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            LoadStudentData();
            SetupDataGridView();
            loadGender();
            loadRoute();
            dataGridViewStudents.Font = new Font("Segoe UI", 10);
        }

        private void LoadStudentData()
        {
            var studentCollection = mongoConnection.GetCollection<Student>("students");
            var routeCollection = mongoConnection.GetCollection<Route>("routes");

            var students = studentCollection.Find(Builders<Student>.Filter.Empty).ToList();
            var routes = routeCollection.Find(Builders<Route>.Filter.Empty).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã Học Sinh", typeof(string));
            dataTable.Columns.Add("Họ Tên", typeof(string));
            dataTable.Columns.Add("Giới Tính", typeof(string));
            dataTable.Columns.Add("Lớp", typeof(string));
            dataTable.Columns.Add("Địa Chỉ", typeof(string));
            dataTable.Columns.Add("Phụ Huynh", typeof(string));
            dataTable.Columns.Add("SĐT Phụ Huynh", typeof(string));
            dataTable.Columns.Add("Quan Hệ", typeof(string));
            dataTable.Columns.Add("Tuyến Đi", typeof(string));
            dataTable.Columns.Add("Trạm", typeof(string));

            foreach (var student in students)
            {
                string diaChi = string.Join(", ", student.diaChi.duong, student.diaChi.phuong, student.diaChi.quan, student.diaChi.thanhPho);

                // Tìm tên tuyến đường
                string tenTuyenDuong = "Unknown";
                if (!string.IsNullOrEmpty(student.tuyenDi))
                {
                    var route = routes.FirstOrDefault(r => r.tenTuyenDuong == student.tuyenDi);
                    tenTuyenDuong = route?.tenTuyenDuong ?? "Unknown";
                }
                else if (!string.IsNullOrEmpty(student.routeId))
                {
                    var route = routes.FirstOrDefault(r => r.routeId == student.routeId);
                    tenTuyenDuong = route?.tenTuyenDuong ?? "Unknown";
                }

                // Kiểm tra thông tin phụ huynh
                string phuHuynhHoTen = student.phuHuynh?.hoTen ?? "Không có thông tin";
                string phuHuynhSDT = student.phuHuynh?.sdt ?? "Không có thông tin";
                string phuHuynhQuanHe = student.phuHuynh?.quanHe ?? "Không có thông tin";

                dataTable.Rows.Add(student.maHS,
                                   student.hoTen,
                                   student.gioiTinh,
                                   student.lop,
                                   diaChi,
                                   phuHuynhHoTen,
                                   phuHuynhSDT,
                                   phuHuynhQuanHe,
                                   tenTuyenDuong,
                                   student.tram
                                   );
            }

            dataGridViewStudents.DataSource = dataTable;
        }


        private void LoadStudentData(List<Student> studentList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã Học Sinh", typeof(string));
            dataTable.Columns.Add("Họ Tên", typeof(string));
            dataTable.Columns.Add("Giới Tính", typeof(string));
            dataTable.Columns.Add("Lớp", typeof(string));
            dataTable.Columns.Add("Địa Chỉ", typeof(string));
            dataTable.Columns.Add("Phụ Huynh", typeof(string));
            dataTable.Columns.Add("SĐT Phụ Huynh", typeof(string));
            dataTable.Columns.Add("Quan Hệ", typeof(string));
            dataTable.Columns.Add("Tuyến Đi", typeof(string));
            dataTable.Columns.Add("Trạm", typeof(string));

            foreach (var student in studentList)
            {
                string diaChi = student.diaChi.duong + ", " +
                                student.diaChi.phuong + ", " +
                                student.diaChi.quan + ", " +
                                student.diaChi.thanhPho;

                dataTable.Rows.Add(student.maHS,
                                   student.hoTen,
                                   student.gioiTinh,
                                   student.lop,
                                   diaChi,
                                   student.phuHuynh.hoTen,
                                   student.phuHuynh.sdt,
                                   student.phuHuynh.quanHe,
                                   student.tuyenDi,
                                   student.tram
                                   );
            }

            dataGridViewStudents.DataSource = dataTable;
        }


        private void SetupDataGridView()
        {
            //dataGridViewStudents.Columns["Mã HS"].Width = 100;      
            //dataGridViewStudents.Columns["Họ Tên"].Width = 200;     
            //dataGridViewStudents.Columns["Giới Tính"].Width = 80;  
            //dataGridViewStudents.Columns["Lớp"].Width = 60;         
            //dataGridViewStudents.Columns["Địa Chỉ"].Width = 250;   
            //dataGridViewStudents.Columns["Phụ Huynh"].Width = 150;  
            //dataGridViewStudents.Columns["SĐT Phụ Huynh"].Width = 120;
            //dataGridViewStudents.Columns["Quan Hệ"].Width = 100;   
            //dataGridViewStudents.Columns["Tuyến Đi"].Width = 100;

            dataGridViewStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadGender()
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            cboGioiTinh.SelectedItem = "Nam";
        }

        private void loadRoute()
        {
            // Retrieve the collection of routes from MongoDB
            var routeCollection = mongoConnection.GetCollection<Route>("routes");

            // Find all routes
            var routes = routeCollection.Find(Builders<Route>.Filter.Empty).ToList();

            // Clear the ComboBox before adding new items
            cboTuyenDuongDi.Items.Clear();

            // Iterate through each route and add the route name (tenTuyenDuong) to the ComboBox
            if(routes != null)
            {
                cboTram.Items.Clear();

                foreach (var route in routes)
                {
                    cboTuyenDuongDi.Items.Add(route.tenTuyenDuong);

                }

                if (routes.Count > 0 && routes[0].cacDiemDung != null)
                {
                    // Get the stops of the first route to display in cboTram
                    foreach (var tram in routes[0].cacDiemDung)
                    {
                        // Add each tram's diaDiem to the Tram ComboBox
                        cboTram.Items.Add(tram.tram);
                    }
                }

                // Set the default selected index if there are items in the ComboBoxes
                if (cboTuyenDuongDi.Items.Count > 0)
                {
                    cboTuyenDuongDi.SelectedIndex = 0;
                }

                if (cboTram.Items.Count > 0)
                {
                    cboTram.SelectedIndex = 0;
                }
            }    
        }


       

        private void dataGridViewStudents_MouseClick(object sender, MouseEventArgs e)
        {
            tvMaHS.Enabled = false;
            if (dataGridViewStudents.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridViewStudents.CurrentRow;

                tvMaHS.Text = selectedRow.Cells["Mã Học Sinh"].Value.ToString();
                tvHoTen.Text = selectedRow.Cells["Họ Tên"].Value.ToString();
                cboGioiTinh.SelectedItem = selectedRow.Cells["Giới Tính"].Value.ToString();
                tvLop.Text = selectedRow.Cells["Lớp"].Value.ToString();

                string[] diaChiParts = selectedRow.Cells["Địa Chỉ"].Value.ToString().Split(new string[] { ", " }, StringSplitOptions.None);
                tvDuong.Text = diaChiParts.Length > 0 ? diaChiParts[0] : "";
                tvPhuong.Text = diaChiParts.Length > 1 ? diaChiParts[1] : "";
                tvQuan.Text = diaChiParts.Length > 2 ? diaChiParts[2] : "";
                tvThanhPho.Text = diaChiParts.Length > 3 ? diaChiParts[3] : "";

                tvHoTenPH.Text = selectedRow.Cells["Phụ Huynh"].Value.ToString();
                tvSdtPH.Text = selectedRow.Cells["SĐT Phụ Huynh"].Value.ToString();
                tvQuanhe.Text = selectedRow.Cells["Quan Hệ"].Value.ToString();
                cboTuyenDuongDi.SelectedItem = selectedRow.Cells["Tuyến Đi"].Value.ToString();
                cboTram.SelectedItem = selectedRow.Cells["Trạm"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            tvMaHS.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa học sinh này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var collection = mongoConnection.GetCollection<Student>("students");

                    string maHS = dataGridViewStudents.SelectedRows[0].Cells["Mã Học Sinh"].Value.ToString();

                    var filter = Builders<Student>.Filter.Eq("maHS", maHS);

                    var deleteResult = collection.DeleteOne(filter);

                    if (deleteResult.DeletedCount > 0)
                    {
                        MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    LoadStudentData();
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn học sinh để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(tvHoTen.Text) ||
                    cboGioiTinh.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(tvLop.Text) ||
                    string.IsNullOrWhiteSpace(tvDuong.Text) ||
                    string.IsNullOrWhiteSpace(tvPhuong.Text) ||
                    string.IsNullOrWhiteSpace(tvQuan.Text) ||
                    string.IsNullOrWhiteSpace(tvThanhPho.Text) ||
                    string.IsNullOrWhiteSpace(tvHoTenPH.Text) ||
                    string.IsNullOrWhiteSpace(tvSdtPH.Text) ||
                    string.IsNullOrWhiteSpace(tvQuanhe.Text))
                {
                    MessageBox.Show("Nhập đầy đủ thông tin để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var collection = mongoConnection.GetCollection<Student>("students");
                var routeCollection = mongoConnection.GetCollection<Route>("routes");

                string maHS = dataGridViewStudents.SelectedRows[0].Cells["Mã Học Sinh"].Value.ToString();
                var filter = Builders<Student>.Filter.Eq("maHS", maHS);

                string selectedRouteName = cboTuyenDuongDi.SelectedItem.ToString();
                var selectedRoute = routeCollection.Find(Builders<Route>.Filter.Eq("tenTuyenDuong", selectedRouteName)).FirstOrDefault();
                string routeId = selectedRoute != null ? selectedRoute.routeId : string.Empty; // Cập nhật thành string

                var update = Builders<Student>.Update
                    .Set("hoTen", tvHoTen.Text)
                    .Set("gioiTinh", cboGioiTinh.SelectedItem.ToString())
                    .Set("lop", tvLop.Text)
                    .Set("diaChi.duong", tvDuong.Text)
                    .Set("diaChi.phuong", tvPhuong.Text)
                    .Set("diaChi.quan", tvQuan.Text)
                    .Set("diaChi.thanhPho", tvThanhPho.Text)
                    .Set("phuHuynh.hoTen", tvHoTenPH.Text)
                    .Set("phuHuynh.sdt", tvSdtPH.Text)
                    .Set("phuHuynh.quanHe", tvQuanhe.Text)
                    .Set("routeId", routeId)
                    .Set("tram", cboTram.SelectedItem.ToString());

                var updateResult = collection.UpdateOne(filter, update);

                if (updateResult.ModifiedCount > 0)
                {
                    MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStudentData();
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn học sinh để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var collection = mongoConnection.GetCollection<Student>("students");

            var filter = Builders<Student>.Filter.Or(
                Builders<Student>.Filter.Eq("maHS", tvTimKiem.Text),
                Builders<Student>.Filter.Regex("hoTen", new BsonRegularExpression(tvTimKiem.Text, "i"))
            );

            var studentList = collection.Find(filter).ToList();

            if (studentList.Count > 0)
            {
                LoadStudentData(studentList);
            }
            else
            {
                MessageBox.Show("Không tìm thấy học sinh nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tvHoTen.Text) ||
        string.IsNullOrEmpty(tvLop.Text) ||
        string.IsNullOrEmpty(tvDuong.Text) ||
        string.IsNullOrEmpty(tvPhuong.Text) ||
        string.IsNullOrEmpty(tvQuan.Text) ||
        string.IsNullOrEmpty(tvThanhPho.Text) ||
        string.IsNullOrEmpty(tvHoTenPH.Text) ||
        string.IsNullOrEmpty(tvSdtPH.Text) ||
        string.IsNullOrEmpty(tvQuanhe.Text) ||
        cboGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Nhập đầy đủ thông tin trước khi thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var collection = mongoConnection.GetCollection<Student>("students");
            var routeCollection = mongoConnection.GetCollection<Route>("routes");

            var existingStudent = collection.Find(Builders<Student>.Filter.Eq("maHS", tvMaHS.Text)).FirstOrDefault();
            if (existingStudent != null)
            {
                MessageBox.Show("Học sinh đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedRouteName = cboTuyenDuongDi.SelectedItem.ToString();
            var selectedRoute = routeCollection.Find(Builders<Route>.Filter.Eq("tenTuyenDuong", selectedRouteName)).FirstOrDefault();
            string routeId = selectedRoute != null ? selectedRoute.routeId : string.Empty;

            if (string.IsNullOrWhiteSpace(routeId))
            {
                MessageBox.Show("Tuyến đường không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var student = new Student
            {
                maHS = tvMaHS.Text,
                hoTen = tvHoTen.Text,
                gioiTinh = cboGioiTinh.SelectedItem.ToString(),
                lop = tvLop.Text,
                diaChi = new DiaChi
                {
                    duong = tvDuong.Text,
                    phuong = tvPhuong.Text,
                    quan = tvQuan.Text,
                    thanhPho = tvThanhPho.Text
                },
                phuHuynh = new PhuHuynh
                {
                    hoTen = tvHoTenPH.Text,
                    sdt = tvSdtPH.Text,
                    quanHe = tvQuanhe.Text
                },
                routeId = routeId,
                tram = cboTram.SelectedItem.ToString()

            };

            collection.InsertOne(student);
            MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadStudentData();
        }

        private void btnThem_Move(object sender, EventArgs e)
        {
            tvMaHS.Enabled = false;
        }

        private void cboTuyenDuongDi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the existing items in the Tram ComboBox
            cboTram.Items.Clear();

            // Check if there are any routes loaded
            if (cboTuyenDuongDi.Items.Count > 0)
            {
                // Get the selected route name
                string selectedRouteName = cboTuyenDuongDi.SelectedItem.ToString();

                // Retrieve the collection of routes from MongoDB
                var routeCollection = mongoConnection.GetCollection<Route>("routes");

                // Find the selected route based on the route name
                var selectedRoute = routeCollection
                    .Find(route => route.tenTuyenDuong == selectedRouteName)
                    .FirstOrDefault();

                // Check if the selected route exists
                if (selectedRoute != null && selectedRoute.cacDiemDung != null)
                {
                    // Iterate through the stops (Tram) of the selected route and add them to the ComboBox
                    foreach (var tram in selectedRoute.cacDiemDung)
                    {
                        // Add each tram's diaDiem to the Tram ComboBox
                        cboTram.Items.Add(tram.tram);
                    }

                    // Set the default selected index if there are items in cboTram
                    if (cboTram.Items.Count > 0)
                    {
                        cboTram.SelectedIndex = 0;
                    }
                }
            }
        }

    }

    public class Student
    {
        public ObjectId _id { get; set; }
        public string maHS { get; set; }
        public string hoTen { get; set; }
        public string gioiTinh { get; set; }
        public string lop { get; set; }
        public DiaChi diaChi { get; set; }
        public PhuHuynh phuHuynh { get; set; }
        public string tuyenDi { get; set; }
        public string routeId { get; set; }   
        public string tram { get; set; }
    }

    public class DiaChi
    {
        public string duong { get; set; }
        public string phuong { get; set; }
        public string quan { get; set; }
        public string thanhPho { get; set; }
    }

    public class PhuHuynh
    {
        public string hoTen { get; set; }
        public string sdt { get; set; }
        public string quanHe { get; set; }
    }

    public class Tram
    {
        public string tram { get; set; }
        public string thoiGian { get; set; }
        public string diaDiem { get; set; }
        public string quangDuong { get; set; }
    }

    public class Route
    {
        public ObjectId Id { get; set; }
        public string routeId { get; set; }
        public string tenTuyenDuong { get; set; }
        public List<Tram> cacDiemDung { get; set; }
        public List<string> cacChuyenHangNgay { get; set; } 
    }


}