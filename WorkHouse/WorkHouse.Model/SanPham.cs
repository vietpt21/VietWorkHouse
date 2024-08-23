using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Model
{
    public class SanPham
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public string HienThi { get; set; }
        public string NhomSanPham { get; set; }
        public string HangSX { get; set; }
        public string HinhAnh { get; set; }
        public string DiaChi { get; set; }
        public string ThongTin { get; set; }
        public DateTime HanSuDung { get; set; }
        public string QuyCach { get; set; }
        public string Dvt { get; set; }
        public float GiaNhap { get; set; }
        public int SlToiThieu { get; set; }
        public int SlToiDa { get; set; }
        public int SlNhap { get; set; }
        public int SlXuat { get; set; }
        public int SlTon { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiTao { get; set; }
    }

}
