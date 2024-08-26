using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkHouse.Model;
using WorkHouse.Service;
using Excel = Microsoft.Office.Interop.Excel;

namespace WorkHouse.userControl.formControl
{
    public partial class frmNhapKho : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        static readonly string connectionString = "Data Source=localhost;Initial Catalog=QLKho;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        static UnitService _dbUnit;
        private userNhapKho _userNhapKho;
        public string IdNhapKho { get; set; }
        List<NhapKho> listNhapKho = new List<NhapKho>();
        public frmNhapKho()
        {
            InitializeComponent();
            _dbUnit = new UnitService(connectionString);

        }

     
        public userNhapKho UserNhapKho
        {
            set
            {
                _userNhapKho = value;
            }
        }
        private void ResetNhapKho()
        {
            cboLoaiNhap.Text = string.Empty;
            txtNguoiTao.Text = string.Empty;
            txtSoLuongNhap.Text = string.Empty;
            txtNoiDungNhap.Text = string.Empty;
            txtNguoiGiao.Text = string.Empty;
            txtNgayNhap.Text = string.Empty;
            txtNgayTao.Text = string.Empty;
            txtNgayCapNhat.Text = string.Empty;
        }
        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            var KhoList = _dbUnit.KhoService.GetAllKho();
            var nccList = _dbUnit.NCCService.GetAllNCC();

            // Cấu hình LookUpEdit
            txtKho.Properties.DataSource = KhoList;
            txtKho.Properties.DisplayMember = "TenKho";  // Thuộc tính để hiển thị
            txtKho.Properties.ValueMember = "Id";     // Thuộc tính để làm giá trị TenNcc

            txtNCC.Properties.DataSource = nccList;
            txtNCC.Properties.DisplayMember = "TenNcc";  // Thuộc tính để hiển thị
            txtNCC.Properties.ValueMember = "Id";

            gridDataNhapKho.DataSource = _dbUnit.NhapKhoService.GetAllNhapKho();
        }
        private bool ValidateNhapKhoData()
        {
            // Kiểm tra các trường dữ liệu cần thiết
            if (string.IsNullOrEmpty(cboLoaiNhap.Text) ||
                string.IsNullOrEmpty(txtNguoiTao.Text) ||
                string.IsNullOrEmpty(txtSoLuongNhap.Text) ||
                string.IsNullOrEmpty(txtNoiDungNhap.Text) ||
                string.IsNullOrEmpty(txtNguoiGiao.Text) ||
                string.IsNullOrEmpty(txtNgayNhap.Text) ||
                string.IsNullOrEmpty(txtNgayTao.Text) ||
                string.IsNullOrEmpty(txtNgayCapNhat.Text))
            {
                return false;
            }

            // Kiểm tra định dạng ngày tháng và số lượng
            if (!DateTime.TryParse(txtNgayNhap.Text, out _) ||
                !DateTime.TryParse(txtNgayTao.Text, out _) ||
                !DateTime.TryParse(txtNgayCapNhat.Text, out _) ||
                !int.TryParse(txtSoLuongNhap.Text, out _))
            {
                return false;
            }

            // Kiểm tra NCC và Kho đã được chọn
            if (txtNCC.EditValue == null || txtKho.EditValue == null)
            {
                return false;
            }

            return true;
        }
        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập
            if (ValidateNhapKhoData())
            {
                NhapKho nhapKho = new NhapKho
                {
                    Id = IdNhapKho,
                    LoaiNhap = cboLoaiNhap.SelectedItem.ToString(),
                    NgayNhap = DateTime.Now,
                    NccId = (int)txtNCC.EditValue,
                    KhoId = (int)txtKho.EditValue,
                    SlNhap = int.Parse(txtSoLuongNhap.Text),
                    NguoiGiao = txtNguoiGiao.Text,
                    NoiDungNhap = txtNoiDungNhap.Text,
                    NgayTao = DateTime.Parse(txtNgayTao.Text),
                    NgayCapNhat = DateTime.Parse(txtNgayCapNhat.Text),
                    NguoiTao = txtNguoiTao.Text, // Thay đổi theo người dùng hiện tại
                };

                // Thay vì lưu vào cơ sở dữ liệu, chỉ thêm vào danh sách
                listNhapKho.Add(nhapKho);

                // Xuất dữ liệu ra Excel
                ExportToExcel();

                // Đặt lại các trường dữ liệu
                ResetNhapKho();

                // Hiển thị form chi tiết
                frmNhapKhoChiTiet frm = new frmNhapKhoChiTiet();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại các trường dữ liệu.");
            }
        }
        private void ExportToExcel()
        {
            // Khởi tạo Excel
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not properly installed!");
                return;
            }

            // Tạo một workbook và worksheet mới
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Thiết lập tiêu đề cột
            worksheet.Cells[1, 1] = "ID";
            worksheet.Cells[1, 2] = "LoaiNhap";
            worksheet.Cells[1, 3] = "NgayNhap";
            worksheet.Cells[1, 4] = "NCC";
            worksheet.Cells[1, 5] = "Kho";
            worksheet.Cells[1, 6] = "SoLuongNhap";
            worksheet.Cells[1, 7] = "NguoiGiao";
            worksheet.Cells[1, 8] = "NoiDungNhap";
            worksheet.Cells[1, 9] = "NgayTao";
            worksheet.Cells[1, 10] = "NgayCapNhat";
            worksheet.Cells[1, 11] = "NguoiTao";

            // Điền dữ liệu vào các ô
            int row = 2;
            foreach (var item in listNhapKho)
            {
                worksheet.Cells[row, 1] = item.Id;
                worksheet.Cells[row, 2] = item.LoaiNhap;
                worksheet.Cells[row, 3] = item.NgayNhap.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 4] = item.NccId; // Có thể cần phải chuyển đổi ID thành tên
                worksheet.Cells[row, 5] = item.KhoId; // Có thể cần phải chuyển đổi ID thành tên
                worksheet.Cells[row, 6] = item.SlNhap;
                worksheet.Cells[row, 7] = item.NguoiGiao;
                worksheet.Cells[row, 8] = item.NoiDungNhap;
                worksheet.Cells[row, 9] = item.NgayTao.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 10] = item.NgayCapNhat.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 11] = item.NguoiTao;
                row++;
            }
            // Đặt đường dẫn và tên file cho Excel
            string directoryPath = @"C:\Temp";
            string filePath = Path.Combine(directoryPath, "NhapKhoData.xlsx");

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                workbook.SaveAs(filePath);
                MessageBox.Show("Dữ liệu đã được xuất ra file Excel thành công tại: " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu file Excel: " + ex.Message);
            }
            finally
            {
                workbook.Close(false);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                workbook = null;
                excelApp = null;
                GC.Collect();
            }
        }
    }
}