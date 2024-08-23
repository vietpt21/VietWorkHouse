using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class SanPhamReponse
    {
        private string _connectionString;

        // Constructor với chuỗi kết nối
        public SanPhamReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SanPham> GetAllSanPham()
        {
            List<SanPham> sanPhamList = new List<SanPham>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT id, ten_san_pham, hien_thi, nhom_san_pham, hang_sx, hinh_anh, dia_chi, thong_tin, han_su_dung, quy_cach, dvt, gia_nhap, sl_toi_thieu, sl_toi_da, sl_nhap, sl_xuat, sl_ton, trang_thai, ghi_chu, ngay_tao, ngay_cap_nhat, nguoi_tao FROM san_pham";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SanPham sanPham = new SanPham
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    TenSanPham = reader["ten_san_pham"] == DBNull.Value ? null : reader["ten_san_pham"].ToString(),
                                    HienThi = reader["hien_thi"] == DBNull.Value ? null : reader["hien_thi"].ToString(),
                                    NhomSanPham = reader["nhom_san_pham"] == DBNull.Value ? null : reader["nhom_san_pham"].ToString(),
                                    HangSX = reader["hang_sx"] == DBNull.Value ? null : reader["hang_sx"].ToString(),
                                    HinhAnh = reader["hinh_anh"] == DBNull.Value ? null : reader["hinh_anh"].ToString(),
                                    DiaChi = reader["dia_chi"] == DBNull.Value ? null : reader["dia_chi"].ToString(),
                                    ThongTin = reader["thong_tin"] == DBNull.Value ? null : reader["thong_tin"].ToString(),
                                    HanSuDung = (DateTime)(reader["han_su_dung"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["han_su_dung"])),
                                    QuyCach = reader["quy_cach"] == DBNull.Value ? null : reader["quy_cach"].ToString(),
                                    Dvt = reader["dvt"] == DBNull.Value ? null : reader["dvt"].ToString(),
                                    GiaNhap = Convert.ToSingle(reader["gia_nhap"]),
                                    SlToiThieu = Convert.ToInt32(reader["sl_toi_thieu"]),
                                    SlToiDa = Convert.ToInt32(reader["sl_toi_da"]),
                                    SlNhap = Convert.ToInt32(reader["sl_nhap"]),
                                    SlXuat = Convert.ToInt32(reader["sl_xuat"]),
                                    SlTon = Convert.ToInt32(reader["sl_ton"]),
                                    TrangThai = reader["trang_thai"] == DBNull.Value ? null : reader["trang_thai"].ToString(),
                                    GhiChu = reader["ghi_chu"] == DBNull.Value ? null : reader["ghi_chu"].ToString(),
                                    NgayTao = (DateTime)(reader["ngay_tao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ngay_tao"])),
                                    NgayCapNhat = (DateTime)(reader["ngay_cap_nhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ngay_cap_nhat"])),
                                    NguoiTao = reader["nguoi_tao"] == DBNull.Value ? null : reader["nguoi_tao"].ToString()
                                };


                                sanPhamList.Add(sanPham);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the error message to a file or monitoring system
                Console.WriteLine($"Error retrieving San Pham: {ex.Message}");
                // Optionally rethrow or handle further
                throw;
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }

            return sanPhamList;
        }

        public void AddSanPham(SanPham sanPham)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO san_pham (ten_san_pham, hien_thi, nhom_san_pham, hang_sx, hinh_anh, dia_chi, thong_tin, han_su_dung, quy_cach, dvt, gia_nhap, sl_toi_thieu, sl_toi_da, sl_nhap, sl_xuat, sl_ton, trang_thai, ghi_chu, ngay_tao, ngay_cap_nhat, nguoi_tao)
                           VALUES (@TenSanPham, @HienThi, @NhomSanPham, @HangSX, @HinhAnh, @DiaChi, @ThongTin, @HanSuDung, @QuyCach, @Dvt, @GiaNhap, @SlToiThieu, @SlToiDa, @SlNhap, @SlXuat, @SlTon, @TrangThai, @GhiChu, @NgayTao, @NgayCapNhat, @NguoiTao)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HienThi", sanPham.HienThi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NhomSanPham", sanPham.NhomSanPham ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HangSX", sanPham.HangSX ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HinhAnh", sanPham.HinhAnh ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DiaChi", sanPham.DiaChi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ThongTin", sanPham.ThongTin ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HanSuDung", sanPham.HanSuDung);
                    command.Parameters.AddWithValue("@QuyCach", sanPham.QuyCach ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Dvt", sanPham.Dvt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GiaNhap", sanPham.GiaNhap);
                    command.Parameters.AddWithValue("@SlToiThieu", sanPham.SlToiThieu);
                    command.Parameters.AddWithValue("@SlToiDa", sanPham.SlToiDa);
                    command.Parameters.AddWithValue("@SlNhap", sanPham.SlNhap);
                    command.Parameters.AddWithValue("@SlXuat", sanPham.SlXuat);
                    command.Parameters.AddWithValue("@SlTon", sanPham.SlTon);
                    command.Parameters.AddWithValue("@TrangThai", sanPham.TrangThai ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GhiChu", sanPham.GhiChu ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayTao", sanPham.NgayTao);
                    command.Parameters.AddWithValue("@NgayCapNhat", sanPham.NgayCapNhat);
                    command.Parameters.AddWithValue("@NguoiTao", sanPham.NguoiTao ?? (object)DBNull.Value);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error deleting NCC record: {ex.Message}");
                        // Handle errors as needed
                    }
                }
            }
        }
        public void UpdateSanPham(SanPham sanPham)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @" UPDATE san_pham SET ten_san_pham = @TenSanPham, hien_thi = @HienThi, nhom_san_pham = @NhomSanPham, hang_sx = @HangSX, hinh_anh = @HinhAnh, dia_chi = @DiaChi, thong_tin = @ThongTin, han_su_dung = @HanSuDung, quy_cach = @QuyCach, dvt = @Dvt, gia_nhap = @GiaNhap, sl_toi_thieu = @SlToiThieu, sl_toi_da = @SlToiDa, sl_nhap = @SlNhap, sl_xuat = @SlXuat, sl_ton = @SlTon, trang_thai = @TrangThai, ghi_chu = @GhiChu, ngay_cap_nhat = @NgayCapNhat, nguoi_tao = @NguoiTao WHERE id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = sanPham.Id });
                    command.Parameters.Add(new SqlParameter("@TenSanPham", SqlDbType.NVarChar, 255) { Value = (object)sanPham.TenSanPham ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@HienThi", SqlDbType.NVarChar, 255) { Value = (object)sanPham.HienThi ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@NhomSanPham", SqlDbType.NVarChar, 255) { Value = (object)sanPham.NhomSanPham ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@HangSX", SqlDbType.NVarChar, 255) { Value = (object)sanPham.HangSX ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@HinhAnh", SqlDbType.NVarChar, 255) { Value = (object)sanPham.HinhAnh ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 255) { Value = (object)sanPham.DiaChi ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@ThongTin", SqlDbType.Text) { Value = (object)sanPham.ThongTin ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@HanSuDung", SqlDbType.DateTime) { Value = (object)sanPham.HanSuDung ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@QuyCach", SqlDbType.NVarChar, 255) { Value = (object)sanPham.QuyCach ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@Dvt", SqlDbType.NVarChar, 50) { Value = (object)sanPham.Dvt ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@GiaNhap", SqlDbType.Decimal) { Value = sanPham.GiaNhap });
                    command.Parameters.Add(new SqlParameter("@SlToiThieu", SqlDbType.Int) { Value = sanPham.SlToiThieu });
                    command.Parameters.Add(new SqlParameter("@SlToiDa", SqlDbType.Int) { Value = sanPham.SlToiDa });
                    command.Parameters.Add(new SqlParameter("@SlNhap", SqlDbType.Int) { Value = sanPham.SlNhap });
                    command.Parameters.Add(new SqlParameter("@SlXuat", SqlDbType.Int) { Value = sanPham.SlXuat });
                    command.Parameters.Add(new SqlParameter("@SlTon", SqlDbType.Int) { Value = sanPham.SlTon });
                    command.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = (object)sanPham.TrangThai ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@GhiChu", SqlDbType.Text) { Value = (object)sanPham.GhiChu ?? DBNull.Value });
                    command.Parameters.Add(new SqlParameter("@NgayCapNhat", SqlDbType.DateTime) { Value = sanPham.NgayCapNhat });
                    command.Parameters.Add(new SqlParameter("@NguoiTao", SqlDbType.NVarChar, 255) { Value = (object)sanPham.NguoiTao ?? DBNull.Value });

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error updating NCC record: {ex.Message}");
                        // Handle errors as needed
                    }
                }
            }
        }
        public void DeleteSanPham(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM san_pham WHERE id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error deleting NCC record: {ex.Message}");
                        // Handle errors as needed
                    }
                }
            }
        }
    }

}
