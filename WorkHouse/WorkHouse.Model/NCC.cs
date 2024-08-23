using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class NCC
    {
        public int Id { get; set; }
        public string TenNcc { get; set; }
        public string HienThi { get; set; }
        public string TenDayDu { get; set; }
        public string LoaiNcc { get; set; }
        public string Logo { get; set; } // Nullable
        public string NguoiDaiDien { get; set; }
        public string Sdt { get; set; }
        public string TinhTrang { get; set; }
        public string NvPhuTrach { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; } // Nullable
    }

}
