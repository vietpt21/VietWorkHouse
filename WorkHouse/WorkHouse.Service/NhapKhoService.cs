using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class NhapKhoService
    {
        private readonly NhapKhoReponse _db;

        // Constructor với chuỗi kết nối
        public NhapKhoService(string connectionString)
        {
            _db = new NhapKhoReponse(connectionString);
        }

        public List<NhapKho> GetAllNhapKho()
        {
            try
            {
                return _db.GetAllNhapKho();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<NhapKho>();
            }
        }
        public bool AddNhapKho(NhapKho nhapKho)
        {
            try
            {
                _db.AddNhapKho(nhapKho); // Giả sử đây là phương thức thêm dữ liệu vào cơ sở dữ liệu
                return true; // Nếu không có lỗi xảy ra, coi như thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding NhapKho: {ex.Message}");
                return false; // Nếu có lỗi xảy ra, coi như thất bại
            }
        }

        public bool EditNhapKho(NhapKho nhapKho)
        {
            try
            {
                _db.UpdtaeNhapKho(nhapKho); // Giả sử đây là phương thức thêm dữ liệu vào cơ sở dữ liệu
                return true; // Nếu không có lỗi xảy ra, coi như thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding NhapKho: {ex.Message}");
                return false; // Nếu có lỗi xảy ra, coi như thất bại
            }
        }

        public void DeleteNhapKho(int Id)
        {
            try
            {
                _db.DeleteNhapKho(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
            }
        }
    }

}
