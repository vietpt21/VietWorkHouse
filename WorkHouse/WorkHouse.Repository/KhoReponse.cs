using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHouse.Model;

namespace WorkHouse.Repository
{
    public class KhoReponse
    {
        private string _connectionString;

        // Constructor với chuỗi kết nối
        public KhoReponse(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Lấy tất cả các bản ghi từ bảng kho
        public List<Kho> GetAllKho()
        {
            List<Kho> khoList = new List<Kho>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, ten_kho, hien_thi, ghi_chu, nguoi_tao, ngay_tao, cap_nhat FROM kho";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Kho kho = new Kho
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    TenKho = reader["ten_kho"].ToString(),
                                    HienThi = reader["hien_thi"].ToString(),
                                    GhiChu = reader["ghi_chu"].ToString(),
                                    NguoiTao = reader["nguoi_tao"].ToString(),
                                    NgayTao = Convert.ToDateTime(reader["ngay_tao"]),
                                    CapNhat = Convert.ToDateTime(reader["cap_nhat"])
                                };

                                khoList.Add(kho);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error retrieving Kho records: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }

            return khoList;
        }

        public void AddKho(Kho kho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO kho (ten_kho, hien_thi, ghi_chu, nguoi_tao, ngay_tao, cap_nhat) " +
                             "VALUES (@ten_kho, @hien_thi, @ghi_chu, @nguoi_tao, @ngay_tao, @cap_nhat)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ten_kho", kho.TenKho);
                    command.Parameters.AddWithValue("@hien_thi", kho.HienThi);
                    command.Parameters.AddWithValue("@ghi_chu", kho.GhiChu);
                    command.Parameters.AddWithValue("@nguoi_tao", kho.NguoiTao);
                    command.Parameters.AddWithValue("@ngay_tao", kho.NgayTao);
                    command.Parameters.AddWithValue("@cap_nhat", kho.CapNhat);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error inserting Kho record: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }
        }


        public void UpdateKho(Kho kho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE kho SET ten_kho = @ten_kho, hien_thi = @hien_thi, ghi_chu = @ghi_chu, " +
                             "nguoi_tao = @nguoi_tao, ngay_tao = @ngay_tao, cap_nhat = @cap_nhat WHERE id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", kho.Id);
                    command.Parameters.AddWithValue("@ten_kho", kho.TenKho);
                    command.Parameters.AddWithValue("@hien_thi", kho.HienThi);
                    command.Parameters.AddWithValue("@ghi_chu", kho.GhiChu);
                    command.Parameters.AddWithValue("@nguoi_tao", kho.NguoiTao);
                    command.Parameters.AddWithValue("@ngay_tao", kho.NgayTao);
                    command.Parameters.AddWithValue("@cap_nhat", kho.CapNhat);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error updating Kho record: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }
        }


        public void DeleteKho(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM kho WHERE id = @id";

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
                        Console.WriteLine($"Error deleting Kho record: {ex.Message}");
                        // Xử lý lỗi theo nhu cầu (ghi log, throw, v.v.)
                    }
                }
            }
        }
    }

}
