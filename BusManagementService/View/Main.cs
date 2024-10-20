using BusManagementService.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusManagementService
{
    public partial class Main : Form
    {
        public string UserRole { get; set; }
        public Main()
        {
            InitializeComponent();
            LoadStudentsForm();
            userLogin();
            LoadUserSession();
        }



        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnStudents_Click_1(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            RoutesForm routesForm = new RoutesForm();

            routesForm.TopLevel = false;
            routesForm.FormBorderStyle = FormBorderStyle.None;
            routesForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(routesForm);

            routesForm.Show();
        }

        private void btnStudents_Click_2(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            Students studentForm = new Students();


            studentForm.TopLevel = false;
            studentForm.FormBorderStyle = FormBorderStyle.None;
            studentForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(studentForm);

            studentForm.Show();
        }



        private void btnTuyenDuong_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            RoutesForm route = new RoutesForm();


            route.TopLevel = false;
            route.FormBorderStyle = FormBorderStyle.None;
            route.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(route);

            route.Show();
        }

        private void LoadStudentsForm()
        {
            BodyPanel.Controls.Clear();

            Students studentForm = new Students();

            studentForm.TopLevel = false;
            studentForm.FormBorderStyle = FormBorderStyle.None;
            studentForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(studentForm);
            studentForm.Show();
        }

        private void btnDrives_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            Drivers driverForm = new Drivers();


            driverForm.TopLevel = false;
            driverForm.FormBorderStyle = FormBorderStyle.None;
            driverForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(driverForm);

            driverForm.Show();
        }

        private void btnBus_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            Bus busForm = new Bus();


            busForm.TopLevel = false;
            busForm.FormBorderStyle = FormBorderStyle.None;
            busForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(busForm);

            busForm.Show();
        }
        private void btnDaily_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            DailyDriver dailyForm = new DailyDriver();


            dailyForm.TopLevel = false;
            dailyForm.FormBorderStyle = FormBorderStyle.None;
            dailyForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(dailyForm);

            dailyForm.Show();
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            BodyPanel.Controls.Clear();

            Account accForm = new Account();


            accForm.TopLevel = false;
            accForm.FormBorderStyle = FormBorderStyle.None;
            accForm.Dock = DockStyle.Fill;

            BodyPanel.Controls.Add(accForm);

            accForm.Show();
        }

        public void userLogin()
        {
            if (UserRole == "admin")
            {
                btnAccount.Visible = true;
            }
            else
            {
                btnAccount.Visible = false;
            }
        }




        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (System.IO.File.Exists("userSession.json"))
                {
                    System.IO.File.Delete("userSession.json");
                }

                Login loginForm = new Login();
                loginForm.Show();
                this.Hide(); 
            }
        }

        private void LoadUserSession()
        {
            if (System.IO.File.Exists("userSession.json"))
            {
                string json = System.IO.File.ReadAllText("userSession.json");
                var userSession = JsonConvert.DeserializeObject<UserSession>(json);

                if (userSession != null)
                {
                    UserRole = userSession.Role;
                    userLogin();
                }
            }
        }

       
    }
}
