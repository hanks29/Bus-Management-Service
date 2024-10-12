using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusManagementService.View.Routes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BusManagementService.View
{
    public partial class RoutesForm : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<Route> collection;
        private List<KeyValuePair<string, ObjectId>> routeList = new List<KeyValuePair<string, ObjectId>>();

        public RoutesForm()
        {
            RoutesFormLoad();
        }

        private void RoutesFormLoad()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            collection = mongoConnection.GetCollection<Route>("routes");
            Routes_Load(); // Remove argument here
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem DataGridView có ít nhất một dòng
            
        }


        public void Routes_Load()
        {
            // Xóa các dòng cũ để tránh bị trùng
            dgvroutes.Rows.Clear();
            dgvroutes.Columns.Clear(); // Xóa các cột

            // Định nghĩa lại các cột nếu chưa có
            dgvroutes.Columns.Add("tram", "Trạm");
            dgvroutes.Columns.Add("thoiGian", "Thời Gian");
            dgvroutes.Columns.Add("diaDiem", "Địa Điểm");
            dgvroutes.Columns.Add("quangDuong", "Quảng Đường");

            // Cài đặt chiều rộng cho cột Địa Điểm
            dgvroutes.Columns["diaDiem"].Width = 400;
            dgvroutes.Columns["tram"].Width = 150;

            cbo_TenTuyenDuong.Items.Clear(); // Xóa các mục trong ComboBox

            // Lấy tất cả các tuyến đường từ MongoDB
            var routes = collection.Find(new BsonDocument()).ToList();

            // Duyệt qua từng tuyến và thêm vào DataGridView và ComboBox
            foreach (var route in routes)
            {
                if (route.cacDiemDung != null)
                {
                    // Sắp xếp các điểm dừng theo tên trạm
                    var sortedStops = route.cacDiemDung.OrderBy(d => d.tram).ToList();

                    foreach (var diemDung in sortedStops)
                    {
                        // Thêm dữ liệu điểm dừng vào DataGridView
                        dgvroutes.Rows.Add(diemDung.tram, diemDung.thoiGian, diemDung.diaDiem, diemDung.quangDuong);
                    }
                }

                // Thêm tên tuyến vào ComboBox dưới dạng KeyValuePair

                routeList.Add(new KeyValuePair<string, ObjectId>(route.tenTuyenDuong, route.Id));
            
            }
            // Liên kết dữ liệu với ComboBox
            cbo_TenTuyenDuong.DataSource = routeList;
            cbo_TenTuyenDuong.DisplayMember = "Key";   // Hiển thị tên tuyến
            cbo_TenTuyenDuong.ValueMember = "Value";   // Lưu trữ ObjectId

            // Chọn tên tuyến đầu tiên trong ComboBox
            if (cbo_TenTuyenDuong.Items.Count > 0)
            {
                cbo_TenTuyenDuong.SelectedIndex = 0;
            }

            if (dgvroutes.Rows.Count > 0)
            {
                // Lựa chọn dòng đầu tiên
                dgvroutes.Rows[0].Selected = true;

                // Lấy dữ liệu từ dòng đầu tiên và gán vào các TextBox
                txt_tram.Text = dgvroutes.Rows[0].Cells["tram"].Value.ToString();
                txt_thoigian.Text = dgvroutes.Rows[0].Cells["thoiGian"].Value.ToString();
                txt_diadiem.Text = dgvroutes.Rows[0].Cells["diaDiem"].Value.ToString();
                txt_quangduong.Text = dgvroutes.Rows[0].Cells["quangDuong"].Value.ToString();
            }
        }




        private void cbo_TenTuyenDuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectId selectedRouteId = ((KeyValuePair<string, ObjectId>)cbo_TenTuyenDuong.SelectedItem).Value;

            var route = collection.Find(r => r.Id == selectedRouteId).FirstOrDefault();


            if (route != null)
            {
                dgvroutes.Rows.Clear(); // Xóa dữ liệu cũ

                // Sắp xếp các điểm dừng theo tên trạm
                var sortedStops = route.cacDiemDung.OrderBy(d => d.tram).ToList();

                foreach (var diemDung in sortedStops)
                {
                    dgvroutes.Rows.Add(diemDung.tram, diemDung.thoiGian, diemDung.diaDiem, diemDung.quangDuong);
                }
            }


        }



        public class Route
        {
            [BsonId]
            public ObjectId Id { get; set; }

            [BsonElement("routeId")]
            public string routeId { get; set; }  // Add this line

            [BsonElement("tenTuyenDuong")]
            public string tenTuyenDuong { get; set; }

            [BsonElement("cacDiemDung")]
            public List<DiemDung> cacDiemDung { get; set; }
        }



        public class DiemDung
        {
            [BsonElement("tram")]
            public string tram { get; set; }

            [BsonElement("thoiGian")]
            public string thoiGian { get; set; }

            [BsonElement("diaDiem")]
            public string diaDiem { get; set; }

            [BsonElement("quangDuong")]
            public string quangDuong { get; set; }
        }

        private void dgvroutes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Kiểm tra nếu có dòng được chọn
            {
                // Lấy dữ liệu từ các ô trên dòng đã chọn và gán vào các TextBox
                txt_tram.Text = dgvroutes.Rows[e.RowIndex].Cells["tram"].Value.ToString();
                txt_thoigian.Text = dgvroutes.Rows[e.RowIndex].Cells["thoiGian"].Value.ToString();
                txt_diadiem.Text = dgvroutes.Rows[e.RowIndex].Cells["diaDiem"].Value.ToString();
                txt_quangduong.Text = dgvroutes.Rows[e.RowIndex].Cells["quangDuong"].Value.ToString();
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            var selectedRoute = cbo_TenTuyenDuong.SelectedItem.ToString();
            var route = collection.Find(r => r.tenTuyenDuong == selectedRoute).FirstOrDefault();

            if (route != null)
            {
                var diemDungMoi = new DiemDung
                {
                    tram = txt_tram.Text,
                    thoiGian = txt_thoigian.Text,
                    diaDiem = txt_diadiem.Text,
                    quangDuong = txt_quangduong.Text
                };

                route.cacDiemDung.Add(diemDungMoi);

                var filter = Builders<Route>.Filter.Eq(r => r.tenTuyenDuong, selectedRoute);
                var update = Builders<Route>.Update.Set(r => r.cacDiemDung, route.cacDiemDung);
                collection.UpdateOne(filter, update);

                dgvroutes.Rows.Add(diemDungMoi.tram, diemDungMoi.thoiGian, diemDungMoi.diaDiem, diemDungMoi.quangDuong);

                MessageBox.Show("Thêm điểm dừng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvroutes.SelectedRows.Count > 0)
            {
                int rowIndex = dgvroutes.SelectedRows[0].Index;
                string tram = dgvroutes.Rows[rowIndex].Cells["tram"].Value.ToString();

                var selectedRoute = cbo_TenTuyenDuong.SelectedItem.ToString();
                var route = collection.Find(r => r.tenTuyenDuong == selectedRoute).FirstOrDefault();

                if (route != null)
                {
                    var diemDung = route.cacDiemDung.FirstOrDefault(d => d.tram == tram);
                    if (diemDung != null)
                    {
                        diemDung.tram = txt_tram.Text;
                        diemDung.thoiGian = txt_thoigian.Text;
                        diemDung.diaDiem = txt_diadiem.Text;
                        diemDung.quangDuong = txt_quangduong.Text;

                        var filter = Builders<Route>.Filter.Eq(r => r.tenTuyenDuong, selectedRoute);
                        var update = Builders<Route>.Update.Set(r => r.cacDiemDung, route.cacDiemDung);
                        collection.UpdateOne(filter, update);

                        dgvroutes.Rows[rowIndex].Cells["tram"].Value = diemDung.tram;
                        dgvroutes.Rows[rowIndex].Cells["thoiGian"].Value = diemDung.thoiGian;
                        dgvroutes.Rows[rowIndex].Cells["diaDiem"].Value = diemDung.diaDiem;
                        dgvroutes.Rows[rowIndex].Cells["quangDuong"].Value = diemDung.quangDuong;

                        MessageBox.Show("Sửa điểm dừng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }



        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvroutes.SelectedRows.Count > 0)
            {
                int rowIndex = dgvroutes.SelectedRows[0].Index;

                // Check if the "tram" column exists and has a value
                if (dgvroutes.Rows[rowIndex].Cells["tram"].Value != null)
                {
                    string tram = dgvroutes.Rows[rowIndex].Cells["tram"].Value.ToString();

                    // Proceed with the logic of removing the "tram" from MongoDB
                    var selectedRoute = cbo_TenTuyenDuong.SelectedItem.ToString();
                    var route = collection.Find(r => r.tenTuyenDuong == selectedRoute).FirstOrDefault();

                    if (route != null)
                    {
                        var diemDung = route.cacDiemDung.FirstOrDefault(d => d.tram == tram);
                        if (diemDung != null)
                        {
                            // Remove diemDung and update the database
                            route.cacDiemDung.Remove(diemDung);

                            var filter = Builders<Route>.Filter.Eq(r => r.tenTuyenDuong, selectedRoute);
                            var update = Builders<Route>.Update.Set(r => r.cacDiemDung, route.cacDiemDung);
                            collection.UpdateOne(filter, update);

                            // Remove row from DataGridView
                            dgvroutes.Rows.RemoveAt(rowIndex);

                            MessageBox.Show("Xóa điểm dừng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy điểm dừng tương ứng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tuyến đường!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không có trạm được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.ToLower();  // Get the search keyword from TextBox

            foreach (DataGridViewRow row in dgvroutes.Rows)
            {
                if (row.IsNewRow) continue;  // Skip new rows

                bool matchFound = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchKeyword))
                    {
                        matchFound = true;
                        break;
                    }
                }

                row.Visible = matchFound;
            }
        }

        private void btn_themRoute_Click(object sender, EventArgs e)
        {
            AddRouteForm them = new AddRouteForm();
            them.ShowDialog();
        }

        private void btn_xoaRoute_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có giá trị nào được chọn trong ComboBox không
            if (cbo_TenTuyenDuong.SelectedItem != null)
            {
                // Lấy giá trị đã chọn từ ComboBox (cbo)
                ObjectId selectedRouteId = ((KeyValuePair<string, ObjectId>)cbo_TenTuyenDuong.SelectedItem).Value;

                // Tạo filter tìm kiếm tuyến đường cần xóa
                var filter = Builders<Route>.Filter.Eq(r => r.Id, selectedRouteId);

                // Thực hiện xóa tuyến đường từ collection
                var result = collection.DeleteOne(filter);

                // Kiểm tra kết quả xóa
                if (result.DeletedCount > 0)
                {
                    MessageBox.Show("Đã xóa tuyến đường thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tuyến đường!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tuyến đường để xóa.");
            }
            
        }

        private void btn_suaRoute_Click(object sender, EventArgs e)
        {
            ObjectId selectedRouteId = ((KeyValuePair<string, ObjectId>)cbo_TenTuyenDuong.SelectedItem).Value;

            var route = collection.Find(r => r.Id == selectedRouteId).FirstOrDefault();


            Object Id;

            Id = route.Id;
            


            // Khởi tạo và hiển thị form EditRouteForm
            EditRouteForm editForm = new EditRouteForm(Id);
            editForm.ShowDialog();  // Hiển thị form dưới dạng modal dialog
        }
    }


}
