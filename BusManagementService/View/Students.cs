using System;
using System.Data;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

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
        }

        private void LoadStudentData()
        {
            var collection = mongoConnection.GetCollection<Student>("students");

            var filter = Builders<Student>.Filter.Empty;
            var studentList = collection.Find(filter).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã HS", typeof(string));
            dataTable.Columns.Add("Họ Tên", typeof(string));
            dataTable.Columns.Add("Giới Tính", typeof(string));
            dataTable.Columns.Add("Lớp", typeof(string));
            dataTable.Columns.Add("Địa Chỉ", typeof(string));

            foreach (var student in studentList)
            {
                string diaChi = student.diaChi.duong + ", " +
                                student.diaChi.phuong + ", " +
                                student.diaChi.quan + ", " +
                                student.diaChi.thanhPho;

                dataTable.Rows.Add(student.maHS, student.hoTen, student.gioiTinh, student.lop, diaChi);
            }

            dataGridViewStudents.DataSource = dataTable;
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
}