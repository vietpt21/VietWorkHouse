using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Service;

namespace WorkHouse.WorkHouse
{
    public class SoaLib
    {
        public NhapKhoService nhapKhoService;
        public string _dbConnect;

        public SoaLib(string dbConnect)
        {
            _dbConnect = dbConnect;
            nhapKhoService = new NhapKhoService(_dbConnect);
        }


        public string GenerateId()
        {
            var IdNew = nhapKhoService.GetAllNhapKho().Select(n => n.Id).ToList().LastOrDefault();
            string Id = "";
            if (IdNew != null)
            {
               
                Id = IdNew.Substring(8, 2);

                if (int.TryParse(IdNew, out int idNumber))
                {
                    idNumber++;
                    string newIdPart = idNumber.ToString("D2");
                    Id = newIdPart;
                }
                else
                {
                    Id = "01";
                }
            }           
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("ddMMyy");
            string idCode = $"NH{dateStr}{Id}";
            return idCode;
        }
    }
}
