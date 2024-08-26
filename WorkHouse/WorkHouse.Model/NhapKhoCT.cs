using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class NhapKhoCT
    {
        public int Id { get; set; }
        public string NhapKhoId { get; set; }
        public NhapKho NhapKho { get; set; }
        public DateTime NgayNhap { get; set; }
        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }
        public string NhomSanPham { get; set; }
        public string HangSX { get; set; }
        public string HinhAnh { get; set; }
        public string ThongTin { get; set; }
        public DateTime HanSuDung { get; set; }
        public string QuyCach { get; set; }
        public string Dvt { get; set; }
        public string SoLo { get; set; }
        public float GiaNhap { get; set; }
        public int SlNhap { get; set; }
        public int SlXuat { get; set; }
        public int SlTon { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiTao { get; set; }
    }

}
