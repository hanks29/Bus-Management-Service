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
            dgvdrives.Font = new Font("Segoe UI", 10);
            dgvdrives.CellClick += new DataGridViewCellEventHandler(dgvdrives_CellClick);
            dgvdrives.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
  
                dataTable.Rows.Add(student.driverId, student.hoTen, student.soBangLai, student.CCCD);
            }

            dgvdrives.DataSource = dataTable;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            CheckTextBoxEmpty();
            string driverId = GenerateNewDriverId();
            string hoTen = txthoten.Text;
            string soBangLai = txtsobanglai.Text;
            string cccd = txtcccd.Text;

            Driver newDriver = new Driver
            {
                driverId = driverId,
                hoTen = hoTen,
                soBangLai = soBangLai,
                CCCD = cccd,
            };
            collection.InsertOne(newDriver);
            LoadStudentData();
            ClearTextBox();
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

            string driverId = txtmataixe.Text;
            string hoTen = txthoten.Text;
            string soBangLai = txtsobanglai.Text;
            string cccd = txtcccd.Text;

            var filter = Builders<Driver>.Filter.Eq(d => d.driverId, driverId);
            var update = Builders<Driver>.Update
                .Set(d => d.hoTen, hoTen)
                .Set(d => d.soBangLai, soBangLai)
                .Set(d => d.CCCD, cccd);
            collection.UpdateOne(filter, update);
            LoadStudentData();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string driverId = txtmataixe.Text;
            var filter = Builders<Driver>.Filter.Eq(d => d.driverId, driverId);
            collection.DeleteOne(filter);

            LoadStudentData();
            ClearTextBox();
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
            }
        }
        private void CheckTextBoxEmpty()
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
            
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
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
                dataTable.Rows.Add(driver.driverId, driver.hoTen, driver.soBangLai, driver.CCCD);
            }

            dgvdrives.DataSource = dataTable;
            if (searchResults.Count == 0)
            {
                MessageBox.Show("Không tìm thấy tài xế nào với từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {

        }
    }
    public class Driver
    {
        public ObjectId _id { get; set; }
        public string driverId { get; set; }
        public string hoTen { get; set; }
        public string soBangLai { get; set; }
        public string CCCD { get; set; }
    }
}
