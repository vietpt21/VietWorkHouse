using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class KhoService
    {
        private readonly KhoReponse _db;

        // Constructor với chuỗi kết nối
        public KhoService(string connectionString)
        {
            _db = new KhoReponse(connectionString);
        }

        public List<Kho> GetAllKho()
        {
            try
            {
                return _db.GetAllKho();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<Kho>();
            }
        }
        public void AddKho(Kho kho)
        {
            try
            {
                _db.AddKho(kho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding computer: {ex.Message}");
            }
        }

        public void EditKho(Kho kho)
        {
            try
            {
                _db.UpdateKho(kho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing computer: {ex.Message}");
            }
        }

        public void DeleteKho(int khoId)
        {
            try
            {
                _db.DeleteKho(khoId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
            }
        }
    }

}
