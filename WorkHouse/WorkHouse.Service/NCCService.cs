using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;
using WorkHouse.Repository;

namespace WorkHouse.Service
{
    public class NCCService
    {
        private readonly NCCReponse _db;

        // Constructor với chuỗi kết nối
        public NCCService(string connectionString)
        {
            _db = new NCCReponse(connectionString);
        }

        public List<NCC> GetAllNCC()
        {
            try
            {
                return _db.GetAllNCC();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                return new List<NCC>();
            }
        }
        public void AddNCC(NCC ncc)
        {
            try
            {
                _db.AddNCC(ncc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding computer: {ex.Message}");
            }
        }

        public void EditNCC(NCC ncc)
        {
            try
            {
                _db.UpdateNCC(ncc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing computer: {ex.Message}");
            }
        }

        public void DeleteNCC(int nccId)
        {
            try
            {
                _db.DeleteNCC(nccId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
            }
        }
    }

}
