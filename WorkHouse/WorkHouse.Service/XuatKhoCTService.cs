using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class XuatKhoCTService
    {
        private readonly XuatKhoCTReponse _db;

        // Constructor với chuỗi kết nối
        public XuatKhoCTService(string connectionString)
        {
            _db = new XuatKhoCTReponse(connectionString);
        }

        public List<XuatKhoCT> GetAllXuatKhoCT()
        {
            try
            {
                return _db.GetAllXuatKhoCt();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<XuatKhoCT>();
            }
        }
    }

}
