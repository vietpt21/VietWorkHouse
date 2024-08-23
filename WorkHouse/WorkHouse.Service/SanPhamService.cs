using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class SanPhamService
    {
        private readonly SanPhamReponse _db;

        // Constructor với chuỗi kết nối
        public SanPhamService(string connectionString)
        {
            _db = new SanPhamReponse(connectionString);
        }

        public List<SanPham> GetAllSanPham()
        {
            try
            {
                return _db.GetAllSanPham();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<SanPham>();
            }
        }
        public void AddSanPham(SanPham sp)
        {
            try
            {
                _db.AddSanPham(sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding computer: {ex.Message}");
            }
        }

        public void EditSanPham(SanPham sp)
        {
            try
            {
                _db.UpdateSanPham(sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing computer: {ex.Message}");
            }
        }

        public void DeleteSanPham(int spId)
        {
            try
            {
                _db.DeleteSanPham(spId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
            }
        }
    }

}
