using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class XuatKhoCT
    {
        public int Id { get; set; }
        public string LoaiXuat { get; set; }
        public int XuatKhoId { get; set; }
        public XuatKho xuatKho { get; set; }
        public DateTime NgayXuat { get; set; }
        public SanPham sanPham { get; set; }
        public int SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public string NhomSanPham { get; set; }
        public string HangSx { get; set; }
        public string HinhAnh { get; set; }
        public string ThongTin { get; set; }
        public string QuyCach { get; set; }
        public string Dvt { get; set; }
        public string SoLo { get; set; }
        public DateTime NgayHetHan { get; set; }
        public int SlXuat { get; set; }
        public int SlXuatTong { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiTao { get; set; }
    }

}
