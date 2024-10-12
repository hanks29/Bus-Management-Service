using BusManagementService.View;
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
        public Main()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnStudents_Click_1(object sender, EventArgs e)
        {

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
    }
}
