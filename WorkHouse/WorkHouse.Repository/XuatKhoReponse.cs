using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class XuatKhoReponse
    {
        private readonly string _connectionString;

        public XuatKhoReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<XuatKho> GetAllXuatKho()
        {
            List<XuatKho> xuatKhoList = new List<XuatKho>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT id, loat_xuat, ngay_xuat, nhan_vien_id, ma_hoa_don, sl_san_pham, sl_xuat, noi_dung_xuat, ghi_chu, ngay_tao, ngay_cap_nhat, nguoi_tao FROM xuat_kho";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                XuatKho xuatKho = new XuatKho
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    LoaiXuat = reader["loat_xuat"].ToString(),
                                    NgayXuat = Convert.ToDateTime(reader["ngay_xuat"]),
                                    NhanVienId = Convert.ToInt32(reader["nhan_vien_id"]),
                                    MaHoaDon = reader["ma_hoa_don"].ToString(),
                                    SlSanPham = Convert.ToInt32(reader["sl_san_pham"]),
                                    SlXuat = Convert.ToInt32(reader["sl_xuat"]),
                                    NoiDungXuat = reader["noi_dung_xuat"].ToString(),
                                    GhiChu = reader["ghi_chu"].ToString(),
                                    NgayTao = Convert.ToDateTime(reader["ngay_tao"]),
                                    NgayCapNhat = Convert.ToDateTime(reader["ngay_cap_nhat"]),
                                    NguoiTao = reader["nguoi_tao"].ToString()
                                };

                                xuatKhoList.Add(xuatKho);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving XuatKho records: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }

            return xuatKhoList;
        }
    }

}
