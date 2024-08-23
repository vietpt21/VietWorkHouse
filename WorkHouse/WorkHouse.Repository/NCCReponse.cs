using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class NCCReponse
    {
        private string _connectionString;

        // Constructor với chuỗi kết nối
        public NCCReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<NCC> GetAllNCC()
        {
            List<NCC> nccList = new List<NCC>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, ten_ncc, hien_thi, ten_day_du, loai_ncc, logo, nguoi_dai_dien, sdt, tinh_trang, nv_phu_trach, ghi_chu, ngay_tao, ngay_cap_nhat FROM ncc";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NCC ncc = new NCC
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    TenNcc = reader["ten_ncc"].ToString(),
                                    HienThi = reader["hien_thi"].ToString(),
                                    TenDayDu = reader["ten_day_du"].ToString(),
                                    LoaiNcc = reader["loai_ncc"].ToString(),
                                    Logo = reader["logo"] == DBNull.Value ? null : reader["logo"].ToString(),
                                    NguoiDaiDien = reader["nguoi_dai_dien"].ToString(),
                                    Sdt = reader["sdt"].ToString(),
                                    TinhTrang = reader["tinh_trang"].ToString(),
                                    NvPhuTrach = reader["nv_phu_trach"].ToString(),
                                    GhiChu = reader["ghi_chu"].ToString(),
                                    NgayTao = Convert.ToDateTime(reader["ngay_tao"]),
                                    NgayCapNhat = (DateTime)(reader["ngay_cap_nhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ngay_cap_nhat"]))
                                };

                                nccList.Add(ncc);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving NCCs: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }

            return nccList;
        }
        public void AddNCC(NCC ncc)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO ncc (ten_ncc, hien_thi, ten_day_du, loai_ncc, logo, nguoi_dai_dien, sdt, tinh_trang, nv_phu_trach, ghi_chu, ngay_tao, ngay_cap_nhat) " +
                             "VALUES (@ten_ncc, @hien_thi, @ten_day_du, @loai_ncc, @logo, @nguoi_dai_dien, @sdt, @tinh_trang, @nv_phu_trach, @ghi_chu, @ngay_tao, @ngay_cap_nhat)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ten_ncc", ncc.TenNcc);
                    command.Parameters.AddWithValue("@hien_thi", ncc.HienThi);
                    command.Parameters.AddWithValue("@ten_day_du", ncc.TenDayDu);
                    command.Parameters.AddWithValue("@loai_ncc", ncc.LoaiNcc);
                    command.Parameters.AddWithValue("@logo", ncc.Logo);
                    command.Parameters.AddWithValue("@nguoi_dai_dien", ncc.NguoiDaiDien);
                    command.Parameters.AddWithValue("@sdt", ncc.Sdt);
                    command.Parameters.AddWithValue("@tinh_trang", ncc.TinhTrang);
                    command.Parameters.AddWithValue("@nv_phu_trach", ncc.NvPhuTrach);
                    command.Parameters.AddWithValue("@ghi_chu", ncc.GhiChu);
                    command.Parameters.AddWithValue("@ngay_tao", ncc.NgayTao);
                    command.Parameters.AddWithValue("@ngay_cap_nhat", (object)ncc.NgayCapNhat ?? DBNull.Value);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error inserting NCC record: {ex.Message}");
                        // Handle errors as needed
                    }
                }
            }
        }

        // Update an existing record
        public void UpdateNCC(NCC ncc)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE ncc SET ten_ncc = @ten_ncc, hien_thi = @hien_thi, ten_day_du = @ten_day_du, loai_ncc = @loai_ncc, " +
                             "logo = @logo, nguoi_dai_dien = @nguoi_dai_dien, sdt = @sdt, tinh_trang = @tinh_trang, nv_phu_trach = @nv_phu_trach, " +
                             "ghi_chu = @ghi_chu, ngay_tao = @ngay_tao, ngay_cap_nhat = @ngay_cap_nhat WHERE id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", ncc.Id);
                    command.Parameters.AddWithValue("@ten_ncc", ncc.TenNcc);
                    command.Parameters.AddWithValue("@hien_thi", ncc.HienThi);
                    command.Parameters.AddWithValue("@ten_day_du", ncc.TenDayDu);
                    command.Parameters.AddWithValue("@loai_ncc", ncc.LoaiNcc);
                    command.Parameters.AddWithValue("@logo", ncc.Logo);
                    command.Parameters.AddWithValue("@nguoi_dai_dien", ncc.NguoiDaiDien);
                    command.Parameters.AddWithValue("@sdt", ncc.Sdt);
                    command.Parameters.AddWithValue("@tinh_trang", ncc.TinhTrang);
                    command.Parameters.AddWithValue("@nv_phu_trach", ncc.NvPhuTrach);
                    command.Parameters.AddWithValue("@ghi_chu", ncc.GhiChu);
                    command.Parameters.AddWithValue("@ngay_tao", ncc.NgayTao);
                    command.Parameters.AddWithValue("@ngay_cap_nhat", (object)ncc.NgayCapNhat ?? DBNull.Value);

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

        // Delete a record
        public void DeleteNCC(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM ncc WHERE id = @id";

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
