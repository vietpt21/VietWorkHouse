using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class frmNhapKhoChiTiet : DevExpress.XtraEditors.XtraForm
    {
        static readonly string connectionString = "Data Source=localhost;Initial Catalog=QLKho;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        static UnitService _UnitService;
        public string IdNhapKho { get; set; }
        List<NhapKhoCT> listNhapKhoCT = new List<NhapKhoCT>();
        List<SanPham> listSanPham = new List<SanPham>();
        public frmNhapKhoChiTiet()
        {
            InitializeComponent();
            _UnitService = new UnitService(connectionString);
            txtSanPham.Properties.EditValueChanged += txtSanPham_Properties_EditValueChanged;
            txtSLNhap.Properties.EditValueChanged += txtSLNhap_Properties_EditValueChanged;
        }
      
        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            var nhapkhoList = _UnitService.NhapKhoService.GetAllNhapKho();
            var nhapkho = nhapkhoList.FirstOrDefault(x => x.Id == IdNhapKho);
            NhapKhoCT nkct = new NhapKhoCT()
            {
                NhapKhoId = IdNhapKho,
                NgayNhap = DateTime.Parse(txtNgayNhap.Text),
                SanPhamId = (int)(txtSanPham.EditValue),
                NhomSanPham = cboNhomSanPham.SelectedItem.ToString(),
                HangSX = txtHangSx.Text,
                ThongTin = txtThongTin.Text,
                HanSuDung = DateTime.Parse(txtHangSx.Text),
                QuyCach = txtQuyCach.Text,
                Dvt = txtDvt.Text,
                SlNhap = int.Parse(txtSLNhap.Text),
                SlXuat = int.Parse(txtSLXuat.Text),
                SlTon = int.Parse(txtSLTon.Text),
                NgayHetHan = DateTime.Parse(txtNgayHetHan.Text),
                GhiChu = txtGhiChu.Text,
                NgayTao = DateTime.Parse(txtNgayTao.Text),
                NgayCapNhat = DateTime.Parse(txtNgayCapNhat.Text),
                NguoiTao = txtNguoiTao.Text,
            };
            listNhapKhoCT.Add(nkct);

            var splist = _UnitService.SanPhamService.GetAllSanPham();
            var sp = splist.FirstOrDefault(x => x.Id == nkct.SanPhamId);

            if (sp != null)
            {
                // Update the quantities
                sp.SlNhap += nkct.SlNhap; // Assuming you want to add the new quantities
                sp.SlTon = sp.SlTon + nkct.SlNhap - sp.SlXuat; // Update stock quantity

                // Save changes to the database

            }
            ExportToExcel();
        }
        private void ExportToExcel()
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not properly installed!");
                return;
            }

            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Thiết lập tiêu đề cột
            worksheet.Cells[1, 1] = "NhapKhoId";
            worksheet.Cells[1, 2] = "NgayNhap";
            worksheet.Cells[1, 3] = "SanPhamId";
            worksheet.Cells[1, 4] = "NhomSanPham";
            worksheet.Cells[1, 5] = "HangSX";
            worksheet.Cells[1, 6] = "ThongTin";
            worksheet.Cells[1, 7] = "HanSuDung";
            worksheet.Cells[1, 8] = "QuyCach";
            worksheet.Cells[1, 9] = "Dvt";
            worksheet.Cells[1, 10] = "SlNhap";
            worksheet.Cells[1, 11] = "SlXuat";
            worksheet.Cells[1, 12] = "SlTon";
            worksheet.Cells[1, 13] = "NgayHetHan";
            worksheet.Cells[1, 14] = "GhiChu";
            worksheet.Cells[1, 15] = "NgayTao";
            worksheet.Cells[1, 16] = "NgayCapNhat";
            worksheet.Cells[1, 17] = "NguoiTao";

            int row = 2;
            foreach (var item in listNhapKhoCT)
            {
                worksheet.Cells[row, 1] = item.NhapKhoId;
                worksheet.Cells[row, 2] = item.NgayNhap.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 3] = item.SanPhamId;
                worksheet.Cells[row, 4] = item.NhomSanPham;
                worksheet.Cells[row, 5] = item.HangSX;
                worksheet.Cells[row, 6] = item.ThongTin;
                worksheet.Cells[row, 7] = item.HanSuDung.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 8] = item.QuyCach;
                worksheet.Cells[row, 9] = item.Dvt;
                worksheet.Cells[row, 10] = item.SlNhap;
                worksheet.Cells[row, 11] = item.SlXuat;
                worksheet.Cells[row, 12] = item.SlTon;
                worksheet.Cells[row, 13] = item.NgayHetHan.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 14] = item.GhiChu;
                worksheet.Cells[row, 15] = item.NgayTao.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 16] = item.NgayCapNhat.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 17] = item.NguoiTao;
                row++;
            }

            // Đặt đường dẫn và lưu file Excel
            string directoryPath = @"C:\Temp";
            string filePath = Path.Combine(directoryPath, "NhapKhoCTData.xlsx");

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
        private bool _isUpdating = false;
        private void txtSanPham_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            var selectedSanPhamId = (int?)txtSanPham.EditValue;

            if (selectedSanPhamId.HasValue)
            {
                // Tìm sản phẩm đã chọn trong danh sách
                var selectedSanPham = _UnitService.SanPhamService.GetAllSanPham()
                    .Find(sp => sp.Id == selectedSanPhamId.Value);
                if (selectedSanPham != null)
                {
                    _isUpdating = true;
                    // Điền thông tin vào các trường khác của Form
                    txtHangSx.Text = selectedSanPham.HangSX;
                    txtSLNhap.Text = selectedSanPham.SlNhap.ToString();
                    txtSLXuat.Text = selectedSanPham.SlXuat.ToString();
                    txtSLTon.Text = selectedSanPham.SlTon.ToString();
                    _isUpdating = false;
                }
            }
        }

        private void txtSLNhap_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            if (int.TryParse(txtSLNhap.Text, out int newSLNhap))
            {
                // Lấy ID sản phẩm đã chọn
                var selectedSanPhamId = (int?)txtSanPham.EditValue;
                if (selectedSanPhamId.HasValue)
                {
                    // Tìm sản phẩm đã chọn trong danh sách
                    var selectedSanPham = _UnitService.SanPhamService.GetAllSanPham()
                        .Find(sp => sp.Id == selectedSanPhamId.Value);
                    if (selectedSanPham != null)
                    {
                        // Tính toán SLTon mới
                        int currentSLTon = selectedSanPham.SlTon;
                        int updatedSLTon = currentSLTon + newSLNhap;

                        // Cập nhật giá trị cho txtSLTon
                        txtSLTon.Text = updatedSLTon.ToString();
                    }
                }
            }
            else
            {
                // Xử lý lỗi nếu không thể chuyển đổi SLNhap sang số nguyên
                MessageBox.Show("SLNhap không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}