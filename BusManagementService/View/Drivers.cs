using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusManagementService.View
{
    public partial class Drivers : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<Driver> collection;
        public Drivers()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            collection = mongoConnection.GetCollection<Driver>("drivers");
            LoadStudentData();
            dgvdrives.CellClick += new DataGridViewCellEventHandler(dgvdrives_CellClick);
            LoadAllDailyDrives();
        }
        private void LoadStudentData()
        {

            var filter = Builders<Driver>.Filter.Empty;
            var studentList = collection.Find(filter).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã tài xế", typeof(string));
            dataTable.Columns.Add("Họ Tên", typeof(string));
            dataTable.Columns.Add("Số bằng lái", typeof(string));
            dataTable.Columns.Add("CCCD", typeof(string));
            dataTable.Columns.Add("Các chuyến phụ trách", typeof(string));

            foreach (var student in studentList)
            {
                string cacChuyenPhuTrachStr = string.Join(", ", student.cacChuyenPhuTrach);
                dataTable.Rows.Add(student.driverId, student.hoTen, student.soBangLai, student.CCCD, cacChuyenPhuTrachStr);
            }

            dgvdrives.DataSource = dataTable;
            dgvdrives.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            //CheckTextBoxEmpty();
            //CheckListBox();
            if (string.IsNullOrWhiteSpace(txthoten.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txthoten.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtsobanglai.Text))
            {
                MessageBox.Show("Vui lòng nhập số bằng lái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsobanglai.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtcccd.Text))
            {
                MessageBox.Show("Vui lòng nhập CCCD.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcccd.Focus();
                return;
            }
            if (Chk_listbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một chuyến phụ trách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string driverId = GenerateNewDriverId();
            string hoTen = txthoten.Text;
            string soBangLai = txtsobanglai.Text;
            string cccd = txtcccd.Text;
            List<string> selectedDailyDrives = new List<string>();
            foreach (var item in Chk_listbox.CheckedItems)
            {
                var dailyDrive = item as DailyDriveItem; 
                if (dailyDrive != null)
                {
                    selectedDailyDrives.Add(dailyDrive.Value); 
                }
            }

            string cacChuyenPhuTrach = string.Join(", ", selectedDailyDrives);
            Driver newDriver = new Driver
            {
                driverId = driverId,
                hoTen = hoTen,
                soBangLai = soBangLai,
                CCCD = cccd,
                cacChuyenPhuTrach = cacChuyenPhuTrach.Split(',').Select(x => x.Trim()).ToArray() 
            };
            collection.InsertOne(newDriver);
            LoadStudentData();
            ClearTextBox();
            ClearCheckedItems();
        }
        private string GenerateNewDriverId()
        {
            var driverIds = collection.Find(Builders<Driver>.Filter.Empty)
                                      .Project(d => d.driverId)
                                      .ToList();

            if (driverIds.Count == 0)
            {
                return "driver_001"; 
            }
            List<int> numericIds = driverIds
                .Select(id => int.Parse(id.Substring(7))) 
                .OrderBy(id => id) 
                .ToList();
            for (int i = 0; i < numericIds.Count; i++)
            {
                if (i + 1 < numericIds.Count && numericIds[i + 1] != numericIds[i] + 1)
                {
                    int missingId = numericIds[i] + 1;
                    return "driver_" + missingId.ToString().PadLeft(3, '0');
                }
            }
            int newIdNumber = numericIds.Last() + 1;
            return "driver_" + newIdNumber.ToString().PadLeft(3, '0');
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txthoten.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txthoten.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtsobanglai.Text))
            {
                MessageBox.Show("Vui lòng nhập số bằng lái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsobanglai.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtcccd.Text))
            {
                MessageBox.Show("Vui lòng nhập CCCD.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcccd.Focus();
                return;
            }
            string driverId = txtmataixe.Text;
            string hoTen = txthoten.Text;
            string soBangLai = txtsobanglai.Text;
            string cccd = txtcccd.Text;
            List<string> selectedDailyDrives = new List<string>();
            foreach (var item in Chk_listbox.CheckedItems)
            {
                var dailyDrive = item as DailyDriveItem; 
                if (dailyDrive != null)
                {
                    selectedDailyDrives.Add(dailyDrive.Value);
                }
            }
            string[] cacChuyenPhuTrach = selectedDailyDrives.Select(x => x.Trim()).ToArray();

            var filter = Builders<Driver>.Filter.Eq(d => d.driverId, driverId);
            var update = Builders<Driver>.Update
                .Set(d => d.hoTen, hoTen)
                .Set(d => d.soBangLai, soBangLai)
                .Set(d => d.CCCD, cccd)
                .Set(d => d.cacChuyenPhuTrach, cacChuyenPhuTrach); 
            collection.UpdateOne(filter, update);
            LoadStudentData();
            ClearCheckedItems();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string driverId = txtmataixe.Text;
            var filter = Builders<Driver>.Filter.Eq(d => d.driverId, driverId);
            collection.DeleteOne(filter);

            LoadStudentData();
            ClearTextBox();
            ClearCheckedItems();
        }
        private void dgvdrives_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvdrives.Rows[e.RowIndex];
                txtmataixe.Text = row.Cells["Mã tài xế"].Value.ToString();
                txthoten.Text = row.Cells["Họ Tên"].Value.ToString();
                txtsobanglai.Text = row.Cells["Số bằng lái"].Value.ToString();
                txtcccd.Text = row.Cells["CCCD"].Value.ToString();
                txtcacchuyenphutrach.Text = row.Cells["Các chuyến phụ trách"].Value.ToString();
            }
        }
        private void CheckTextBoxEmpty()
        {
            
        }
        private void CheckListBox()
        {
            
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            ClearCheckedItems();
        }
        private void ClearTextBox()
        {
            foreach (var control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
        }
        private void ClearCheckedItems()
        {
            for (int i = 0; i < Chk_listbox.Items.Count; i++)
            {
                Chk_listbox.SetItemChecked(i, false); 
            }
        }

        private void btntimkem_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                LoadStudentData();
                return;
            }
            var filter = Builders<Driver>.Filter.Regex("hoTen", new BsonRegularExpression(searchKeyword, "i"));
            var searchResults = collection.Find(filter).ToList();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã tài xế", typeof(string));
            dataTable.Columns.Add("Họ Tên", typeof(string));
            dataTable.Columns.Add("Số bằng lái", typeof(string));
            dataTable.Columns.Add("CCCD", typeof(string));
            dataTable.Columns.Add("Các chuyến phụ trách", typeof(string));

            foreach (var driver in searchResults)
            {
                string cacChuyenPhuTrachStr = string.Join(", ", driver.cacChuyenPhuTrach);
                dataTable.Rows.Add(driver.driverId, driver.hoTen, driver.soBangLai, driver.CCCD, cacChuyenPhuTrachStr);
            }

            dgvdrives.DataSource = dataTable;
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy tài xế nào với từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadAllDailyDrives()
        {
            var dailyDriveCollection = mongoConnection.GetCollection<DailyDrive>("dailydrive");
            var dailyDrives = dailyDriveCollection.Find(FilterDefinition<DailyDrive>.Empty).ToList();
            Chk_listbox.Items.Clear();
            foreach (var dailyDrive in dailyDrives)
            {
                Chk_listbox.Items.Add(new DailyDriveItem { Text = dailyDrive.dailyDriveId, Value = dailyDrive.dailyDriveId });
            }
            Chk_listbox.DisplayMember = "Text";
            Chk_listbox.ValueMember = "Value";
        }
        private void btnluu_Click(object sender, EventArgs e)
        {

        }

        private void txtsobanglai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtcccd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    public class Driver
    {
        public ObjectId _id { get; set; }
        public string driverId { get; set; }
        public string hoTen { get; set; }
        public string soBangLai { get; set; }
        public string CCCD { get; set; }
        public string[] cacChuyenPhuTrach { get; set; }
    }
    public class DailyDrive
    {
        public ObjectId _id { get; set; }
        public string dailyDriveId { get; set; }
        public string thu { get; set; }
        public string taiXe { get; set; }
        public string xeBus { get; set; }
        public string tuyenDuong {  get; set; }

    }
    public class DailyDriveItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
