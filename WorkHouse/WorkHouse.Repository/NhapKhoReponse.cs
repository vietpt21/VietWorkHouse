using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class NhapKhoReponse
    {
        private readonly string _connectionString;

        public NhapKhoReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<NhapKho> GetAllNhapKho()
        {
            List<NhapKho> nhapKhoList = new List<NhapKho>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                SELECT 
                    nk.id, nk.loai_nhap, nk.ngay_nhap, nk.ncc_id, nk.kho_id, nk.sl_nhap, nk.nguoi_giao, nk.noi_dung_nhap, nk.ngay_tao, nk.ngay_cap_nhat, nk.nguoi_tao,
                    n.ten_ncc, n.hien_thi AS ncc_hien_thi, n.ten_day_du, n.loai_ncc, n.logo, n.nguoi_dai_dien, n.sdt, n.tinh_trang, n.nv_phu_trach, n.ghi_chu, n.ngay_tao AS ncc_ngay_tao, n.ngay_cap_nhat AS ncc_ngay_cap_nhat,
                    k.ten_kho, k.hien_thi AS kho_hien_thi, k.ghi_chu AS kho_ghi_chu, k.nguoi_tao AS kho_nguoi_tao, k.ngay_tao AS kho_ngay_tao, k.cap_nhat AS kho_cap_nhat
                FROM nhap_kho nk
                INNER JOIN ncc n ON nk.ncc_id = n.id
                INNER JOIN kho k ON nk.kho_id = k.id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NhapKho nhapKho = new NhapKho
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    LoaiNhap = reader["loai_nhap"].ToString(),
                                    NgayNhap = Convert.ToDateTime(reader["ngay_nhap"]),
                                    NccId = Convert.ToInt32(reader["ncc_id"]),
                                    KhoId = Convert.ToInt32(reader["kho_id"]),
                                    SlNhap = Convert.ToInt32(reader["sl_nhap"]),
                                    NguoiGiao = reader["nguoi_giao"].ToString(),
                                    NoiDungNhap = reader["noi_dung_nhap"].ToString(),
                                    NgayTao = reader["ngay_tao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ngay_tao"]),
                                    NgayCapNhat = reader["ngay_cap_nhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ngay_cap_nhat"]),
                                    NguoiTao = reader["nguoi_tao"].ToString(),
                                    ncc = new NCC
                                    {
                                        Id = Convert.ToInt32(reader["ncc_id"]),
                                        TenNcc = reader["ten_ncc"].ToString(),
                                        HienThi = reader["ncc_hien_thi"].ToString(),
                                        TenDayDu = reader["ten_day_du"].ToString(),
                                        LoaiNcc = reader["loai_ncc"].ToString(),
                                        Logo = reader["logo"].ToString(),
                                        NguoiDaiDien = reader["nguoi_dai_dien"].ToString(),
                                        Sdt = reader["sdt"].ToString(),
                                        TinhTrang = reader["tinh_trang"].ToString(),
                                        NvPhuTrach = reader["nv_phu_trach"].ToString(),
                                        GhiChu = reader["ghi_chu"].ToString(),
                                        NgayTao = Convert.ToDateTime(reader["ncc_ngay_tao"]),
                                        NgayCapNhat = Convert.ToDateTime(reader["ncc_ngay_cap_nhat"])
                                    },
                                    kho = new Kho
                                    {
                                        Id = Convert.ToInt32(reader["kho_id"]),
                                        TenKho = reader["ten_kho"].ToString(),
                                        HienThi = reader["kho_hien_thi"].ToString(),
                                        GhiChu = reader["kho_ghi_chu"].ToString(),
                                        NguoiTao = reader["kho_nguoi_tao"].ToString(),
                                        NgayTao = Convert.ToDateTime(reader["kho_ngay_tao"]),
                                        CapNhat = Convert.ToDateTime(reader["kho_cap_nhat"])
                                    }
                                };

                                nhapKhoList.Add(nhapKho);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving NhapKho records: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }

            return nhapKhoList;
        }
        public bool AddNhapKho(NhapKho nhapKho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"
            INSERT INTO nhap_kho (loai_nhap, ngay_nhap, ncc_id, kho_id, sl_nhap, nguoi_giao, noi_dung_nhap, ngay_tao, ngay_cap_nhat, nguoi_tao)
            VALUES (@LoaiNhap, @NgayNhap, @NccId, @KhoId, @SlNhap, @NguoiGiao, @NoiDungNhap, @NgayTao, @NgayCapNhat, @NguoiTao)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@LoaiNhap", nhapKho.LoaiNhap ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayNhap", nhapKho.NgayNhap);
                    command.Parameters.AddWithValue("@NccId", nhapKho.NccId);
                    command.Parameters.AddWithValue("@KhoId", nhapKho.KhoId);
                    command.Parameters.AddWithValue("@SlNhap", nhapKho.SlNhap);
                    command.Parameters.AddWithValue("@NguoiGiao", nhapKho.NguoiGiao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NoiDungNhap", nhapKho.NoiDungNhap ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayTao", nhapKho.NgayTao.HasValue ? (object)nhapKho.NgayTao.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NgayCapNhat", nhapKho.NgayCapNhat.HasValue ? (object)nhapKho.NgayCapNhat.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NguoiTao", nhapKho.NguoiTao ?? (object)DBNull.Value);

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
        public bool UpdtaeNhapKho(NhapKho nhapKho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE nhap_kho SET loai_nhap = @LoaiNhap, ngay_nhap = @NgayNhap, ncc_id = @NccId, kho_id = @KhoId, sl_nhap = @SlNhap, nguoi_giao = @NguoiGiao, noi_dung_nhap = @NoiDungNhap, ngay_tao = @NgayTao, ngay_cap_nhat = @NgayCapNhat, nguoi_tao = @NguoiTao WHERE id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", nhapKho.Id);
                    command.Parameters.AddWithValue("@LoaiNhap", nhapKho.LoaiNhap ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayNhap", nhapKho.NgayNhap);
                    command.Parameters.AddWithValue("@NccId", nhapKho.NccId);
                    command.Parameters.AddWithValue("@KhoId", nhapKho.KhoId);
                    command.Parameters.AddWithValue("@SlNhap", nhapKho.SlNhap);
                    command.Parameters.AddWithValue("@NguoiGiao", nhapKho.NguoiGiao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NoiDungNhap", nhapKho.NoiDungNhap ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayTao", nhapKho.NgayTao.HasValue ? (object)nhapKho.NgayTao.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NgayCapNhat", nhapKho.NgayCapNhat.HasValue ? (object)nhapKho.NgayCapNhat.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NguoiTao", nhapKho.NguoiTao ?? (object)DBNull.Value);

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
        public void DeleteNhapKho(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM nhap_kho WHERE id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

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
