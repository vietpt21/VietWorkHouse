using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class XuatKhoCTReponse
    {
        private readonly string _connectionString;

        public XuatKhoCTReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<XuatKhoCT> GetAllXuatKhoCt()
        {
            List<XuatKhoCT> xuatKhoCtList = new List<XuatKhoCT>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
               SELECT id,LoaiXuat, xuat_kho_id, ngay_xuat, san_pham_id, ten_san_pham, nhom_san_pham, hang_sx, hinh_anh, thong_tin, quy_cach, dvt, so_lo, ngay_het_han, sl_xuat, sl_xuat_tong, ngay_tao, ngay_cap_nhat, nguoi_tao FROM xuat_kho_ct";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                XuatKhoCT xuatKhoCt = new XuatKhoCT
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    LoaiXuat = reader["LoaiXuat"].ToString(),
                                    XuatKhoId = Convert.ToInt32(reader["xuat_kho_id"]),
                                    NgayXuat = Convert.ToDateTime(reader["ngay_xuat"]),
                                    SanPhamId = Convert.ToInt32(reader["san_pham_id"]),
                                    TenSanPham = reader["ten_san_pham"].ToString(),
                                    NhomSanPham = reader["nhom_san_pham"].ToString(),
                                    HangSx = reader["hang_sx"].ToString(),
                                    HinhAnh = reader["hinh_anh"].ToString(),
                                    ThongTin = reader["thong_tin"].ToString(),
                                    QuyCach = reader["quy_cach"].ToString(),
                                    Dvt = reader["dvt"].ToString(),
                                    SoLo = reader["so_lo"].ToString(),
                                    NgayHetHan = Convert.ToDateTime(reader["ngay_het_han"]),
                                    SlXuat = Convert.ToInt32(reader["sl_xuat"]),
                                    SlXuatTong = Convert.ToInt32(reader["sl_xuat_tong"]),
                                    NgayTao = Convert.ToDateTime(reader["ngay_tao"]),
                                    NgayCapNhat = Convert.ToDateTime(reader["ngay_cap_nhat"]),
                                    NguoiTao = reader["nguoi_tao"].ToString()
                                };

                                xuatKhoCtList.Add(xuatKhoCt);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving XuatKhoCt records: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }
            return xuatKhoCtList;
        }

        public void AddXuatKhoCt(XuatKhoCT xuatKhoCt)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                INSERT INTO xuat_kho_ct
                    (LoaiXuat, xuat_kho_id, ngay_xuat, san_pham_id, ten_san_pham, nhom_san_pham, hang_sx, hinh_anh, thong_tin, quy_cach, dvt, so_lo, ngay_het_han, sl_xuat, sl_xuat_tong, ngay_tao, ngay_cap_nhat, nguoi_tao)
                VALUES
                    (@LoaiXuat, @XuatKhoId, @NgayXuat, @SanPhamId, @TenSanPham, @NhomSanPham, @HangSx, @HinhAnh, @ThongTin, @QuyCach, @Dvt, @SoLo, @NgayHetHan, @SlXuat, @SlXuatTong, @NgayTao, @NgayCapNhat, @NguoiTao)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@LoaiXuat", xuatKhoCt.LoaiXuat);
                    command.Parameters.AddWithValue("@XuatKhoId", xuatKhoCt.XuatKhoId);
                    command.Parameters.AddWithValue("@NgayXuat", xuatKhoCt.NgayXuat);
                    command.Parameters.AddWithValue("@SanPhamId", xuatKhoCt.SanPhamId);
                    command.Parameters.AddWithValue("@TenSanPham", xuatKhoCt.TenSanPham);
                    command.Parameters.AddWithValue("@NhomSanPham", xuatKhoCt.NhomSanPham);
                    command.Parameters.AddWithValue("@HangSx", xuatKhoCt.HangSx);
                    command.Parameters.AddWithValue("@HinhAnh", xuatKhoCt.HinhAnh);
                    command.Parameters.AddWithValue("@ThongTin", xuatKhoCt.ThongTin);
                    command.Parameters.AddWithValue("@QuyCach", xuatKhoCt.QuyCach);
                    command.Parameters.AddWithValue("@Dvt", xuatKhoCt.Dvt);
                    command.Parameters.AddWithValue("@SoLo", xuatKhoCt.SoLo);
                    command.Parameters.AddWithValue("@NgayHetHan", xuatKhoCt.NgayHetHan);
                    command.Parameters.AddWithValue("@SlXuat", xuatKhoCt.SlXuat);
                    command.Parameters.AddWithValue("@SlXuatTong", xuatKhoCt.SlXuatTong);
                    command.Parameters.AddWithValue("@NgayTao", xuatKhoCt.NgayTao);
                    command.Parameters.AddWithValue("@NgayCapNhat", xuatKhoCt.NgayCapNhat);
                    command.Parameters.AddWithValue("@NguoiTao", xuatKhoCt.NguoiTao);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error inserting XuatKhoCt record: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }
        }
    }

}
