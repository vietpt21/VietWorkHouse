using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class XuatKhoService
    {
        private readonly XuatKhoReponse _db;

        // Constructor với chuỗi kết nối
        public XuatKhoService(string connectionString)
        {
            _db = new XuatKhoReponse(connectionString);
        }

        public List<XuatKho> GetAllXuatKho()
        {
            try
            {
                return _db.GetAllXuatKho();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<XuatKho>();
            }
        }
    }

}
