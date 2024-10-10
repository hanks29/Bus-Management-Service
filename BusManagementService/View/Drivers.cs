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
        public Drivers()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            LoadStudentData();
        }
        private void LoadStudentData()
        {
            var collection = mongoConnection.GetCollection<Driver>("drivers");

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
}
