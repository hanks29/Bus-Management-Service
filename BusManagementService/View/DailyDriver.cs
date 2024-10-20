using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusManagementService.View
{
    public partial class DailyDriver : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<Route> collectionRoutes;
        private IMongoCollection<Drivers> collectionDrivers;
        private IMongoCollection<Bus> collectionBus; 
        private IMongoCollection<DailyDrives> collectionDaily;
        private List<KeyValuePair<string, string>> busList = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> drivesList = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> routesList = new List<KeyValuePair<string, string>>();
        List<DailyDrives> allDailyDrives = new List<DailyDrives>();

        public DailyDriver()
        {
            InitializeComponent();
            dgvdaily.Font = new Font("Segoe UI", 10);
            mongoConnection = new MongoConnection();
            collectionBus = mongoConnection.GetCollection<Bus>("bus");
            collectionDrivers = mongoConnection.GetCollection<Drivers>("drivers");
            collectionRoutes = mongoConnection.GetCollection<Route>("routes");
            collectionDaily = mongoConnection.GetCollection<DailyDrives>("dailydrive");
            Load_CboBus();
            Load_CboDrivers();
            Load_CboRoutes();
            Load_CboDaily();
            Daily_Load();

            if (dgvdaily.Rows.Count > 0)
            {
                dgvdaily.Rows[0].Selected = true; // Chọn hàng đầu tiên
                dgvdaily_CellContentClick(this, new DataGridViewCellEventArgs(0, 0)); // Gọi sự kiện để điền dữ liệu
            }
        }

        private void Daily_Load()
        {
            dgvdaily.Rows.Clear();
            dgvdaily.Columns.Clear();
            allDailyDrives.Clear();

            dgvdaily.Columns.Add("dailyId", "DailyId");
            dgvdaily.Columns.Add("thoiGian", "Thời gian");
            dgvdaily.Columns.Add("taixe", "Tài xế");
            dgvdaily.Columns.Add("xe", "Xe buýt");
            dgvdaily.Columns.Add("tuyenduong", "Tuyến đường");

            var Daily = collectionDaily.Find(new BsonDocument()).ToList();

            foreach (var daily in Daily)
            {
                var drivers = collectionDrivers.Find(r => r.driverId == daily.taiXe).FirstOrDefault();

                var bus = collectionBus.Find(r => r.busId == daily.xeBus).FirstOrDefault();

                var route = collectionRoutes.Find(r => r.routeId == daily.tuyenDuong).FirstOrDefault();

                allDailyDrives.Add(daily);

                dgvdaily.Rows.Add(daily.dailyDriveId,daily.thu,drivers.hoTen,bus.bienSoXe,route.tenTuyenDuong);
            }

        }

        private void Load_CboDaily()
        {          
            List<String> thoiGian = new List<string> { 
                "Thứ 2",
                "Thứ 3",
                "Thứ 4",
                "Thứ 5",
                "Thứ 6",
                "Thứ 7",
            };

            cboThoiGian.Items.Clear();
            cbo_thu.Items.Clear();

            foreach (string thu in thoiGian)
            {
                cboThoiGian.Items.Add(thu);
                cbo_thu.Items.Add(thu);
            }    
        }

        private void Load_CboBus()
        {
            cboXebus.Items.Clear();

            var Bus = collectionBus.Find(new BsonDocument()).ToList();

            foreach (var bus in Bus)
            {

                busList.Add(new KeyValuePair<string, string>(bus.bienSoXe, bus.busId));

            }
            cboXebus.DataSource = busList;
            cboXebus.DisplayMember = "Key";   // Hiển thị tên tuyến
            cboXebus.ValueMember = "Value";
        }
        private void Load_CboDrivers()
        {
            cboTaixe.Items.Clear();

            var Drivers = collectionDrivers.Find(new BsonDocument()).ToList();

            foreach (var driver in Drivers)
            {

                drivesList.Add(new KeyValuePair<string, string>(driver.hoTen, driver.driverId));

            }
            cboTaixe.DataSource = drivesList;
            cboTaixe.DisplayMember = "Key";   // Hiển thị tên tuyến
            cboTaixe.ValueMember = "Value";
        }

        private void Load_CboRoutes()
        {
            cboTuyenduong.Items.Clear();

            var Routes = collectionRoutes.Find(new BsonDocument()).ToList();

            foreach (var route in Routes)
            {

                routesList.Add(new KeyValuePair<string, string>(route.tenTuyenDuong, route.routeId));

            }
            cboTuyenduong.DataSource = routesList;
            cboTuyenduong.DisplayMember = "Key";   // Hiển thị tên tuyến
            cboTuyenduong.ValueMember = "Value";
        }

        public class Bus
        {
            public ObjectId _id { get; set; }
            public string busId { get; set; }
            public string bienSoXe { get; set; }
            public int soChoNgoi { get; set; }
        }

        public class Drivers
        {
            public ObjectId _id { get; set; }
            public string driverId { get; set; }
            public string hoTen { get; set; }
            public string soBangLai { get; set; }
            public string CCCD { get; set; }
        }

        public class Route
        {
            public ObjectId Id { get; set; }
            public string routeId { get; set; }  // Add this line
            public string tenTuyenDuong { get; set; }
            public List<DiemDung> cacDiemDung { get; set; }
        }

        public class DiemDung
        {
           
            public string tram { get; set; }

            public string thoiGian { get; set; }

            public string diaDiem { get; set; }
            public string quangDuong { get; set; }
        }
        public class DailyDrives
        {
            public ObjectId _id { get; set; }
            public string dailyDriveId { get; set; }
            public string thu { get; set; }
            public string taiXe { get; set; }
            public string xeBus { get; set; }
            public string tuyenDuong { get; set; }
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            string idDaily = txt_id.Text;

            var daily = collectionDaily.Find(r => r.dailyDriveId == idDaily).FirstOrDefault();


            if (daily != null)
            {
                MessageBox.Show("DriveId đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (cboThoiGian.SelectedItem == null || cboTaixe.SelectedItem == null || cboXebus.SelectedItem == null || cboTuyenduong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                var dailyMoi = new DailyDrives
                {
                    dailyDriveId = txt_id.Text,
                    thu = cboThoiGian.SelectedItem.ToString(),
                    taiXe = ((KeyValuePair<string, string>)cboTaixe.SelectedItem).Value,
                    xeBus = ((KeyValuePair<string, string>)cboXebus.SelectedItem).Value,
                    tuyenDuong = ((KeyValuePair<string, string>)cboTuyenduong.SelectedItem).Value
                };

                collectionDaily.InsertOne(dailyMoi);

                MessageBox.Show("Drive đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Daily_Load();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvdaily.SelectedRows.Count > 0)
            {
                string idDaily = txt_id.Text;
                

                // Check if the record with the given dailyDriveId exists
                var daily = collectionDaily.Find(r => r.dailyDriveId == idDaily).FirstOrDefault();

                if (daily != null)
                {
                    // Ensure that the ComboBox selections are not null
                    if (cboThoiGian.SelectedItem == null || cboTaixe.SelectedItem == null || cboXebus.SelectedItem == null || cboTuyenduong.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create the update object with new values
                    var dailyMoi = new DailyDrives
                    {
                        dailyDriveId = txt_id.Text,
                        thu = cboThoiGian.SelectedItem.ToString(),
                        taiXe = ((KeyValuePair<string, string>)cboTaixe.SelectedItem).Value,
                        xeBus = ((KeyValuePair<string, string>)cboXebus.SelectedItem).Value,
                        tuyenDuong = ((KeyValuePair<string, string>)cboTuyenduong.SelectedItem).Value
                    };

                    // Filter to match the document you want to update based on `dailyDriveId`
                    var filter = Builders<DailyDrives>.Filter.Eq(r => r.dailyDriveId, idDaily);

                    // Update the document with new values
                    var update = Builders<DailyDrives>.Update
                        .Set(r => r.thu, dailyMoi.thu)
                        .Set(r => r.taiXe, dailyMoi.taiXe)
                        .Set(r => r.xeBus, dailyMoi.xeBus)
                        .Set(r => r.tuyenDuong, dailyMoi.tuyenDuong);

                    
                    var result = collectionDaily.UpdateOne(filter, update);
                    
                    if (result.ModifiedCount > 0)
                    {
                        MessageBox.Show("Sửa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Daily_Load();
                    }
                    else
                    {
                        MessageBox.Show("Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DriveId!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvdaily.SelectedRows.Count > 0)
            {
                int rowIndex = dgvdaily.SelectedRows[0].Index;
                // Get the selected row
                string idDaily = txt_id.Text;

                // Check if the selected row exists in the collection
                var daily = collectionDaily.Find(r => r.dailyDriveId == idDaily).FirstOrDefault();

                if (daily != null)
                {
                    // Perform the delete operation
                    var filter = Builders<DailyDrives>.Filter.Eq(r => r.dailyDriveId, idDaily);
                    var result = collectionDaily.DeleteOne(filter);
                    dgvdaily.Rows.RemoveAt(rowIndex);
                    allDailyDrives.Remove(daily);
                    if (result.DeletedCount > 0)
                    {
                        MessageBox.Show("Xóa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Daily_Load();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DriveId!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvdaily_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Kiểm tra nếu có dòng được chọn
            {
                // Lấy dữ liệu từ các ô trên dòng đã chọn và gán vào các TextBox
                string id = dgvdaily.Rows[e.RowIndex].Cells["dailyId"].Value.ToString();
                txt_id.Text = id;
                cboThoiGian.SelectedItem = dgvdaily.Rows[e.RowIndex].Cells["thoiGian"].Value.ToString();

                string taixe = dgvdaily.Rows[e.RowIndex].Cells["taixe"].Value.ToString();
                var taixeItem = cboTaixe.Items.Cast<KeyValuePair<string, string>>()
                    .FirstOrDefault(kv => kv.Key == taixe);
                if (taixeItem.Key != null) 
                {
                    cboTaixe.SelectedItem = taixeItem;
                }

                string xebus = dgvdaily.Rows[e.RowIndex].Cells["xe"].Value.ToString();
                var xebusItem = cboXebus.Items.Cast<KeyValuePair<string, string>>()
                    .FirstOrDefault(kv => kv.Key == xebus);
                if(xebusItem.Key != null)
                {
                    cboXebus.SelectedItem = xebusItem;
                }    
                
                string tuyenduong = dgvdaily.Rows[e.RowIndex].Cells["tuyenduong"].Value.ToString();
                var tuyenduongItem = cboTuyenduong.Items.Cast<KeyValuePair<string, string>>()
                    .FirstOrDefault(kv => kv.Key == tuyenduong);
                if(tuyenduongItem.Key != null)
                {
                    cboTuyenduong.SelectedItem = tuyenduongItem;
                }    
                
            }
        }

        private void cbo_thu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy thứ đã chọn từ ComboBox
            string selectedThu = cbo_thu.SelectedItem?.ToString();

            // Kiểm tra nếu đã chọn một giá trị hợp lệ
            if (!string.IsNullOrEmpty(selectedThu))
            {
                // Lọc danh sách dựa trên thứ đã chọn
                var filteredDaily = allDailyDrives.Where(d => d.thu == selectedThu).ToList();

                // Cập nhật DataGridView
                dgvdaily.Rows.Clear(); // Xóa toàn bộ hàng cũ

                foreach (var daily in filteredDaily)
                {
                    var drivers = collectionDrivers.Find(r => r.driverId == daily.taiXe).FirstOrDefault();
                    var bus = collectionBus.Find(r => r.busId == daily.xeBus).FirstOrDefault();
                    var route = collectionRoutes.Find(r => r.routeId == daily.tuyenDuong).FirstOrDefault();

                    dgvdaily.Rows.Add(daily.dailyDriveId, daily.thu, drivers?.hoTen, bus?.bienSoXe, route?.tenTuyenDuong);
                }

                // Tự động chọn hàng đầu tiên sau khi lọc nếu có kết quả
                if (dgvdaily.Rows.Count > 0)
                {
                    dgvdaily.Rows[0].Selected = true;
                    dgvdaily_CellContentClick(this, new DataGridViewCellEventArgs(0, 0));
                }
            }
        }

    }
}
