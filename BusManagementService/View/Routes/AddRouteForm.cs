using MongoDB.Bson;
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
using static BusManagementService.View.RoutesForm;

namespace BusManagementService.View.Routes
{
    public partial class AddRouteForm : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<Route> collection;
        public AddRouteForm()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            collection = mongoConnection.GetCollection<Route>("routes");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập liệu trong form
            string routeId = txtRouteId.Text;
            string tenTuyenDuong = txtTenTuyenDuong.Text;

            // Tạo đối tượng Route mới mà không có điểm dừng
            Route newRoute = new Route
            {
                routeId = routeId,
                tenTuyenDuong = tenTuyenDuong,
                cacDiemDung = new List<DiemDung>()  // Không có điểm dừng
            };

            // Thêm vào collection
            collection.InsertOne(newRoute);

            // Hiển thị thông báo thành công
            MessageBox.Show("Đã thêm tuyến đường thành công!");

           
            this.Close();
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
    }
}
