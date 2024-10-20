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
    public partial class Bus : Form
    {
        private MongoConnection mongoConnection;
        public Bus()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            dgvBus.Font = new Font("Segoe UI", 10);
            LoadBusData();
        }

        private void LoadBusData()
        {
            var collection = mongoConnection.GetCollection<Buses>("bus");

            var filter = Builders<Buses>.Filter.Empty;
            var bustList = collection.Find(filter).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã Chuyến Xe", typeof(string));
            dataTable.Columns.Add("Biển Số Xe", typeof(string));
            dataTable.Columns.Add("Số Chỗ Ngồi", typeof(string));

            foreach (var bus in bustList)
            {
                dataTable.Rows.Add(bus.busId, bus.bienSoXe, bus.soChoNgoi);
            }

            dgvBus.DataSource = dataTable;
            dgvBus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            ClearText();
            MessageBox.Show("Bạn hãy điền dữ liệu và sau đó nhấn LƯU!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
      
            if (string.IsNullOrEmpty(txtIDBus.Text))
            {
                MessageBox.Show("Vui lòng nhập mã chuyến xe để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var collection = mongoConnection.GetCollection<Buses>("bus");
            var filter = Builders<Buses>.Filter.Eq("busId", txtIDBus.Text);
            var update = Builders<Buses>.Update
                .Set("bienSoXe", txtBienSoXe.Text)
                .Set("soChoNgoi", int.Parse(txtSoChoNgoi.Text));

            var result = collection.UpdateOne(filter, update);
            if (result.ModifiedCount > 0)
            {
                MessageBox.Show("Đã sửa thông tin chuyến xe!");
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã chuyến xe hoặc không có thay đổi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearText();
            LoadBusData();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            var collection = mongoConnection.GetCollection<Buses>("bus");

            var filter = Builders<Buses>.Filter.Eq("busId", txtIDBus.Text);
            collection.DeleteOne(filter);

            MessageBox.Show("Đã xóa chuyến xe thành công!");
            ClearText();
            LoadBusData(); 
        }

        private void dgvBus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                
                DataGridViewRow row = dgvBus.Rows[e.RowIndex];

                txtIDBus.Text = row.Cells["Mã Chuyến Xe"].Value.ToString();
                txtBienSoXe.Text = row.Cells["Biển Số Xe"].Value.ToString();
                txtSoChoNgoi.Text = row.Cells["Số Chỗ Ngồi"].Value.ToString();
            }
        }

      

        public bool CheckEnoughInformation()
        {
            if (string.IsNullOrEmpty(txtIDBus.Text) || string.IsNullOrEmpty(txtBienSoXe.Text) ||
                string.IsNullOrEmpty(txtSoChoNgoi.Text) || string.IsNullOrEmpty(txtStart.Text) ||
                string.IsNullOrEmpty(txtEnd.Text))
            {
                return false;
            }

            return true;
        }

        public bool CheckTrungLap()
        {
            var collection = mongoConnection.GetCollection<Buses>("bus");

          
            var existingBusById = collection.Find(Builders<Buses>.Filter.Eq("busId", txtIDBus.Text)).FirstOrDefault();
            if (existingBusById != null)
            {
                MessageBox.Show("Mã chuyến xe đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            // Kiểm tra trùng lặp biển số xe
            var existingBusByLicense = collection.Find(Builders<Buses>.Filter.Eq("bienSoXe", txtBienSoXe.Text)).FirstOrDefault();
            if (existingBusByLicense != null)
            {
                MessageBox.Show("Biển số xe đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true; 
            }

            return false; 
        }

        public void ClearText ()
        {
            txtIDBus.Text = "";
            txtBienSoXe.Text = "";
            txtSoChoNgoi.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckEnoughInformation())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (CheckTrungLap())
            {

                return;
            }


            var collection = mongoConnection.GetCollection<Buses>("bus");


            var bus = new Buses
            {
                busId = txtIDBus.Text,
                bienSoXe = txtBienSoXe.Text,
                soChoNgoi = int.Parse(txtSoChoNgoi.Text)
            };


            collection.InsertOne(bus);
            MessageBox.Show("Đã thêm chuyến xe thành công!");


            ClearText();
            LoadBusData();
        }

        private void Bus_Load(object sender, EventArgs e)
        {

        }

        private void dgvBus_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dgvBus.Rows[e.RowIndex];

                txtIDBus.Text = row.Cells["Mã Chuyến Xe"].Value.ToString();
                txtBienSoXe.Text = row.Cells["Biển Số Xe"].Value.ToString();
                txtSoChoNgoi.Text = row.Cells["Số Chỗ Ngồi"].Value.ToString();
            }
        }
    }
}
