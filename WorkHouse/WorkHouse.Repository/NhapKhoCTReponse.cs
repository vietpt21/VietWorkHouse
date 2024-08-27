using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class NhapKhoCTReponse
    {
        private readonly string _connectionString;

        public NhapKhoCTReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<NhapKhoCT> GetAllNhapKhoCt()
        {
            List<NhapKhoCT> nhapKhoCtList = new List<NhapKhoCT>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @" SELECT n.id, n.nhap_kho_id, n.ngay_nhap, n.san_pham_id, n.nhom_san_pham, n.hang_sx, n.hinh_anh, n.thong_tin, n.han_su_dung, n.quy_cach, n.dvt, n.so_lo, n.gia_nhap, n.sl_nhap, n.sl_xuat, n.sl_ton, n.ngay_het_han, n.ghi_chu, n.ngay_tao, n.ngay_cap_nhat, n.nguoi_tao FROM nhap_kho_ct n JOIN nhap_kho nk ON n.nhap_kho_id = nk.id JOIN san_pham sp ON n.san_pham_id = sp.id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NhapKhoCT nhapKhoCt = new NhapKhoCT
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    NhapKhoId = reader["nhap_kho_id"].ToString(),
                                    NgayNhap = Convert.ToDateTime(reader["ngay_nhap"]),
                                    SanPhamId = Convert.ToInt32(reader["san_pham_id"]),
                                    NhomSanPham = reader["nhom_san_pham"].ToString(),
                                    HangSX = reader["hang_sx"].ToString(),
                                    HinhAnh = reader["hinh_anh"].ToString(),
                                    ThongTin = reader["thong_tin"].ToString(),
                                    HanSuDung = Convert.ToDateTime(reader["han_su_dung"]),
                                    QuyCach = reader["quy_cach"].ToString(),
                                    Dvt = reader["dvt"].ToString(),
                                    SoLo = reader["so_lo"].ToString(),
                                    GiaNhap = Convert.ToSingle(reader["gia_nhap"]),
                                    SlNhap = Convert.ToInt32(reader["sl_nhap"]),
                                    SlXuat = Convert.ToInt32(reader["sl_xuat"]),
                                    SlTon = Convert.ToInt32(reader["sl_ton"]),
                                    NgayHetHan = Convert.ToDateTime(reader["ngay_het_han"]),
                                    GhiChu = reader["ghi_chu"].ToString(),
                                    NgayTao = Convert.ToDateTime(reader["ngay_tao"]),
                                    NgayCapNhat = Convert.ToDateTime(reader["ngay_cap_nhat"]),
                                    NguoiTao = reader["nguoi_tao"].ToString()
                                };

                                nhapKhoCtList.Add(nhapKhoCt);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving NhapKhoCt records: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }

            return nhapKhoCtList;
        }
        public bool AddNhapKhoCT(NhapKhoCT nhapKhoCT)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @" INSERT INTO nhap_kho_ct ( nhap_kho_id, ngay_nhap, san_pham_id, nhom_san_pham, hang_sx, hinh_anh, thong_tin, han_su_dung, quy_cach, dvt, so_lo, gia_nhap, sl_nhap, sl_xuat, sl_ton, ngay_het_han, ghi_chu, ngay_tao, ngay_cap_nhat, nguoi_tao ) VALUES ( @NhapKhoId, @NgayNhap, @SanPhamId, @NhomSanPham, @HangSX, @HinhAnh, @ThongTin, @HanSuDung, @QuyCach, @Dvt, @SoLo, @GiaNhap, @SlNhap, @SlXuat, @SlTon, @NgayHetHan, @GhiChu, @NgayTao, @NgayCapNhat, @NguoiTao )";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NhapKhoId", nhapKhoCT.NhapKhoId);
                        command.Parameters.AddWithValue("@NgayNhap", nhapKhoCT.NgayNhap);
                        command.Parameters.AddWithValue("@SanPhamId", nhapKhoCT.SanPhamId);
                        command.Parameters.AddWithValue("@NhomSanPham", nhapKhoCT.NhomSanPham ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HangSX", nhapKhoCT.HangSX ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HinhAnh", nhapKhoCT.HinhAnh ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ThongTin", nhapKhoCT.ThongTin ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HanSuDung", nhapKhoCT.HanSuDung);
                        command.Parameters.AddWithValue("@QuyCach", nhapKhoCT.QuyCach ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Dvt", nhapKhoCT.Dvt ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SoLo", nhapKhoCT.SoLo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@GiaNhap", nhapKhoCT.GiaNhap);
                        command.Parameters.AddWithValue("@SlNhap", nhapKhoCT.SlNhap);
                        command.Parameters.AddWithValue("@SlXuat", nhapKhoCT.SlXuat);
                        command.Parameters.AddWithValue("@SlTon", nhapKhoCT.SlTon);
                        command.Parameters.AddWithValue("@NgayHetHan", nhapKhoCT.NgayHetHan);
                        command.Parameters.AddWithValue("@GhiChu", nhapKhoCT.GhiChu ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NgayTao", nhapKhoCT.NgayTao);
                        command.Parameters.AddWithValue("@NgayCapNhat", nhapKhoCT.NgayCapNhat);
                        command.Parameters.AddWithValue("@NguoiTao", nhapKhoCT.NguoiTao ?? (object)DBNull.Value);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0; // Nếu có ít nhất 1 dòng bị ảnh hưởng, coi như thành công
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"Error updating NCC record: {ex.Message}");
                            // Xử lý lỗi theo nhu cầu
                            return false; // Thất bại
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error adding NhapKhoCT: {ex.Message}");
                return false;
            }
        }
        public bool UpdateNhapKhoCT(NhapKhoCT nhapKhoCT)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @"UPDATE nhap_kho_ct SET nhap_kho_id = @NhapKhoId, ngay_nhap = @NgayNhap, san_pham_id = @SanPhamId, nhom_san_pham = @NhomSanPham, hang_sx = @HangSX, hinh_anh = @HinhAnh, thong_tin = @ThongTin, han_su_dung = @HanSuDung, quy_cach = @QuyCach, dvt = @Dvt, so_lo = @SoLo, gia_nhap = @GiaNhap, sl_nhap = @SlNhap, sl_xuat = @SlXuat, sl_ton = @SlTon, ngay_het_han = @NgayHetHan, ghi_chu = @GhiChu, ngay_tao = @NgayTao, ngay_cap_nhat = @NgayCapNhat, nguoi_tao = @NguoiTao WHERE id = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", nhapKhoCT.Id);
                        command.Parameters.AddWithValue("@NhapKhoId", nhapKhoCT.NhapKhoId);
                        command.Parameters.AddWithValue("@NgayNhap", nhapKhoCT.NgayNhap);
                        command.Parameters.AddWithValue("@SanPhamId", nhapKhoCT.SanPhamId);
                        command.Parameters.AddWithValue("@NhomSanPham", nhapKhoCT.NhomSanPham ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HangSX", nhapKhoCT.HangSX ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HinhAnh", nhapKhoCT.HinhAnh ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ThongTin", nhapKhoCT.ThongTin ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@HanSuDung", nhapKhoCT.HanSuDung);
                        command.Parameters.AddWithValue("@QuyCach", nhapKhoCT.QuyCach ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Dvt", nhapKhoCT.Dvt ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SoLo", nhapKhoCT.SoLo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@GiaNhap", nhapKhoCT.GiaNhap);
                        command.Parameters.AddWithValue("@SlNhap", nhapKhoCT.SlNhap);
                        command.Parameters.AddWithValue("@SlXuat", nhapKhoCT.SlXuat);
                        command.Parameters.AddWithValue("@SlTon", nhapKhoCT.SlTon);
                        command.Parameters.AddWithValue("@NgayHetHan", nhapKhoCT.NgayHetHan);
                        command.Parameters.AddWithValue("@GhiChu", nhapKhoCT.GhiChu ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NgayTao", nhapKhoCT.NgayTao);
                        command.Parameters.AddWithValue("@NgayCapNhat", nhapKhoCT.NgayCapNhat);
                        command.Parameters.AddWithValue("@NguoiTao", nhapKhoCT.NguoiTao ?? (object)DBNull.Value);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu ít nhất 1 dòng bị ảnh hưởng
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error updating NhapKhoCT: {ex.Message}");
                return false;
            }
        }
        public bool DeleteNhapKhoCT(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @"
            DELETE FROM nhap_kho_ct
            WHERE id = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu ít nhất 1 dòng bị ảnh hưởng
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error deleting NhapKhoCT: {ex.Message}");
                return false;
            }
        }


    }

}
