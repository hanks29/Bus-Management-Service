using MongoDB.Bson.Serialization.Attributes;
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
    public partial class Account : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<User> userCollection;

        public Account()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            userCollection = mongoConnection.GetCollection<User>("users");
            loadGender();
            loadPosition();
            loadData();
            dgvAccount.Font = new Font("Segoe UI", 10);
            dgvAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadData()
        {
            List<User> users = userCollection.Find(_ => true).ToList();

            dgvAccount.DataSource = users;

            dgvAccount.Columns["id"].Visible = false;

            dgvAccount.Columns["username"].HeaderText = "Tên đăng nhập";
            dgvAccount.Columns["password"].HeaderText = "Mật khẩu";
            dgvAccount.Columns["role"].HeaderText = "Chức vụ";
            dgvAccount.Columns["hoTen"].HeaderText = "Họ và tên";
            dgvAccount.Columns["tuoi"].HeaderText = "Tuổi";
            dgvAccount.Columns["gioiTinh"].HeaderText = "Giới tính";
        }


        private void loadGender()
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            cboGioiTinh.SelectedItem = "Nam";
        }

        private void loadPosition()
        {
            cboChucVu.Items.Add("admin");
            cboChucVu.Items.Add("manager");

            cboChucVu.SelectedItem = "manager";
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new User
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text,
                    role = cboChucVu.SelectedItem.ToString(),
                    hoTen = txtHoTen.Text,
                    tuoi = Convert.ToDouble(txtTuoi.Text),
                    gioiTinh = cboGioiTinh.SelectedItem.ToString()
                };

                userCollection.InsertOne(user);

                MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var selectedUser = (User)dgvAccount.SelectedRows[0].DataBoundItem;
                        userCollection.DeleteOne(u => u.id == selectedUser.id);
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn cập nhật thông tin tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var selectedUser = (User)dgvAccount.SelectedRows[0].DataBoundItem;
                        var filter = Builders<User>.Filter.Eq(u => u.id, selectedUser.id);

                        var update = Builders<User>.Update
                            .Set(u => u.username, txtUsername.Text)
                            .Set(u => u.password, txtPassword.Text)
                            .Set(u => u.role, cboChucVu.SelectedItem.ToString())
                            .Set(u => u.hoTen, txtHoTen.Text)
                            .Set(u => u.tuoi, Convert.ToDouble(txtTuoi.Text))
                            .Set(u => u.gioiTinh, cboGioiTinh.SelectedItem.ToString());

                        userCollection.UpdateOne(filter, update);
                        MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAccount.Rows[e.RowIndex];

                txtUsername.Text = row.Cells["username"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();
                cboChucVu.SelectedItem = row.Cells["role"].Value.ToString();
                txtHoTen.Text = row.Cells["hoTen"].Value.ToString();
                txtTuoi.Text = row.Cells["tuoi"].Value.ToString();
                cboGioiTinh.SelectedItem = row.Cells["gioiTinh"].Value.ToString();
            }
        }
    }

}
