using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static BusManagementService.View.RoutesForm;

namespace BusManagementService.View.Routes
{
    public partial class EditRouteForm : Form
    {
        private Object Id;
        private MongoConnection mongoConnection;
        private IMongoCollection<Route> collection;

        public EditRouteForm(Object Id)
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            collection = mongoConnection.GetCollection<Route>("routes");

            this.Id = Id;

            // Khi form khởi tạo, load thông tin tuyến đường từ MongoDB
            LoadRouteData();
        }

        // Lớp Route để đại diện cho đối tượng tuyến đường
        public class Route
        {
            [BsonId]
            public ObjectId Id { get; set; }

            [BsonElement("routeId")]
            public string routeId { get; set; }

            [BsonElement("tenTuyenDuong")]
            public string tenTuyenDuong { get; set; }

            [BsonElement("cacDiemDung")]
            public List<DiemDung> cacDiemDung { get; set; }
        }

        // Hàm load thông tin tuyến đường từ MongoDB vào form
        private void LoadRouteData()
        {
            // Tìm kiếm tuyến đường theo Id
            var filter = Builders<Route>.Filter.Eq(r => r.Id, new ObjectId(Id.ToString()));
            var route = collection.Find(filter).FirstOrDefault();

            // Nếu tìm thấy, hiển thị lên các trường nhập liệu
            if (route != null)
            {
                txtRouteId.Text = route.routeId;
                txtTenTuyenDuong.Text = route.tenTuyenDuong;
            }
            else
            {
                MessageBox.Show("Không tìm thấy tuyến đường.");
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập liệu
            string newRouteId = txtRouteId.Text;
            string newTenTuyenDuong = txtTenTuyenDuong.Text;

            // Tạo filter để tìm tuyến đường theo Id
            var filter = Builders<Route>.Filter.Eq(r => r.Id, new ObjectId(Id.ToString()));

            // Tạo update object để cập nhật các trường
            var update = Builders<Route>.Update
                .Set(r => r.routeId, newRouteId)
                .Set(r => r.tenTuyenDuong, newTenTuyenDuong);

            // Thực hiện cập nhật
            var result = collection.UpdateOne(filter, update);

            // Kiểm tra kết quả cập nhật
            if (result.ModifiedCount > 0)
            {
                MessageBox.Show("Cập nhật tuyến đường thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào.");
            }
        }
    }
}
