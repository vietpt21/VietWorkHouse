using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class NhapKhoCTService
    {
        private readonly NhapKhoCTReponse _db;

        // Constructor với chuỗi kết nối
        public NhapKhoCTService(string connectionString)
        {
            _db = new NhapKhoCTReponse(connectionString);
        }

        public List<NhapKhoCT> GetAllNhapKhoCT()
        {
            try
            {
                return _db.GetAllNhapKhoCt();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<NhapKhoCT>();
            }
        }
        public Boolean AddNhapKhoCt(NhapKhoCT nkct)
        {
            try
            {
                _db.AddNhapKhoCT(nkct);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding computer: {ex.Message}");
                return false;
            }
        }

        public bool UpdateNhapKhoCt(NhapKhoCT nkct)
        {
            try
            {
                _db.UpdateNhapKhoCT(nkct);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing computer: {ex.Message}");
                return false;
            }
        }

        public void DeleteNHapKhoCt(int nkId)
        {
            try
            {
                _db.DeleteNhapKhoCT(nkId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
            }
        }
    }

}
