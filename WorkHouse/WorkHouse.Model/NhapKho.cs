using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class NhapKho
    {
        public string Id { get; set; }
        public string LoaiNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public int NccId { get; set; }
        public NCC ncc { get; set; }
        public int KhoId { get; set; }
        public Kho kho { get; set; }
        public int SlNhap { get; set; }
        public string NguoiGiao { get; set; }
        public string NoiDungNhap { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiTao { get; set; }
    }

}
