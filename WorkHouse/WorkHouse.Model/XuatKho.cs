using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class XuatKho
    {
        public int Id { get; set; }
        public string LoaiXuat { get; set; }
        public DateTime NgayXuat { get; set; }
        public int NhanVienId { get; set; }
        public string MaHoaDon { get; set; }
        public int SlSanPham { get; set; }
        public int SlXuat { get; set; }
        public string NoiDungXuat { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiTao { get; set; }
    }

}
