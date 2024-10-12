using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManagementService.View
{
    internal class Buses
    {
        public ObjectId _id { get; set; }
        public string busId { get; set; }
        public string bienSoXe { get; set; }
        public int soChoNgoi { get; set; }
        
        public string[] cacChuyenHangNgay { get; set; }
    }
}
