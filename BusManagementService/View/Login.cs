using System;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BusManagementService.View
{
    public partial class Login : Form
    {
        private MongoConnection mongoConnection;
        private IMongoCollection<User> userCollection;

        public Login()
        {
            InitializeComponent();
            mongoConnection = new MongoConnection();
            userCollection = mongoConnection.GetCollection<User>("users");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tvUsername.Text;
            string password = tvPassword.Text;

            MongoConnection mongoConnection = new MongoConnection();
            var usersCollection = mongoConnection.GetCollection<User>("users");

            var user = usersCollection.Find(u => u.username == username && u.password == password).FirstOrDefault();

            if (user != null)
            {
                SaveUserSession(user.username, user.role);
                Main mainForm = new Main();
                mainForm.UserRole = user.role; 
                mainForm.userLogin();
                mainForm.Show();
                this.Hide();
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }
        }

        private void SaveUserSession(string username, string role)
        {
            var userSession = new UserSession
            {
                Username = username,
                Role = role
            };

            string json = JsonConvert.SerializeObject(userSession);
            System.IO.File.WriteAllText("userSession.json", json);
        }
    }

  

    public class User
    {
        [BsonId]
        public Object id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string hoTen { get; set; }
        public double tuoi { get; set; }
        public string gioiTinh { get; set; }

        public User()
        {
            id = ObjectId.GenerateNewId();
        }
    }

    public class UserSession
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

}

