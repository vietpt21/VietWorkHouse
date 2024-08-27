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
        public NhapKho nhapkho { get; set; }
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

            for (int i = 0; i < nhapkho.SlNhap; i++)
            {
                NhapKhoCT nkct = new NhapKhoCT()
                {
                    NhapKhoId = IdNhapKho,
                    NgayNhap = DateTime.Parse(txtNgayNhap.Text),
                    SanPhamId = (int)(txtSanPham.EditValue),
                    NhomSanPham = cboNhomSanPham.SelectedItem.ToString(),
                    HangSX = txtHangSx.Text,
                    HinhAnh = null,
                    ThongTin = txtThongTin.Text,
                    HanSuDung = DateTime.Parse(txtHanSuDung.Text),
                    QuyCach = txtQuyCach.Text,
                    Dvt = txtDvt.Text,
                    SoLo = txtSoLo.Text,
                    GiaNhap = int.Parse(txtGiaNhap.Text),
                    SlNhap = int.Parse(txtSLNhap.Text), // Mỗi bản sao nhập 1 đơn vị
                    SlXuat = int.Parse(txtSLXuat.Text),
                    SlTon = int.Parse(txtSLTon.Text),
                    NgayHetHan = DateTime.Parse(txtNgayHetHan.Text),
                    GhiChu = txtGhiChu.Text,
                    NgayTao = DateTime.Parse(txtNgayTao.Text),
                    NgayCapNhat = DateTime.Parse(txtNgayCapNhat.Text),
                    NguoiTao = txtNguoiTao.Text,
                };
                listNhapKhoCT.Add(nkct);
                ExportToExcelNhapKhoCt();
                var splist = _UnitService.SanPhamService.GetAllSanPham();
                var sp = splist.FirstOrDefault(x => x.Id == nkct.SanPhamId);
                if (sp != null)
                {
                    // Update the quantities
                    sp.SlNhap += nkct.SlNhap; // Assuming you want to add the new quantities
                    sp.SlTon = sp.SlTon + nkct.SlNhap - sp.SlXuat; // Update stock quantity
                    listSanPham.Add(sp);
                    ExportToExcelSanPham();
                    // Save changes to the database
                }
                MessageBox.Show("Them thanh cong");
                ClearText();
            }

            // Hiển thị thông báo thành công
            MessageBox.Show("Dữ liệu đã được thêm thành công. Bạn có thể tiếp tục thêm dữ liệu mới.");
        }

        private bool ValidateNhapKhoCTData()
        {
            // Thực hiện kiểm tra tính hợp lệ của dữ liệu nhập vào (ví dụ kiểm tra các trường không rỗng, định dạng ngày tháng, số lượng, v.v.)
            // Trả về true nếu dữ liệu hợp lệ, false nếu không hợp lệ.
            return true;
        }
        private void ClearText()
        {
         
            txtNgayNhap.Text = null;
            txtSanPham.EditValue = null;
            cboNhomSanPham.SelectedItem = -1;
            txtHangSx.Text = null;
            txtThongTin.Text = string.Empty;
            txtHanSuDung.Text = string.Empty;
            txtQuyCach.Text = string.Empty;
            txtDvt.Text = string.Empty;
            txtSoLo.Text = string.Empty;
            txtGiaNhap.Text = string.Empty;
            txtSLNhap.Text = string.Empty;
            txtSLXuat.Text = string.Empty;
            txtSLTon.Text = string.Empty;
            txtNgayHetHan.Text = null;
            txtGhiChu.Text = string.Empty;
            txtNgayTao.Text = string.Empty;
            txtNgayCapNhat.Text = null;
            txtNguoiTao.Text = string.Empty;
        }
        private void ExportToExcelNhapKhoCt()
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not properly installed!");
                return;
            }

            string directoryPath = @"C:\Temp";
            string filePath = Path.Combine(directoryPath, "NhapKhoCTData.xlsx");
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                if (File.Exists(filePath))
                {
                    // Mở workbook hiện tại nếu tệp đã tồn tại
                    workbook = excelApp.Workbooks.Open(filePath);
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                }
                else
                {
                    // Tạo workbook và worksheet mới nếu tệp không tồn tại
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1] = "NhapKhoId";
                    worksheet.Cells[1, 2] = "NgayNhap";
                    worksheet.Cells[1, 3] = "SanPhamId";
                    worksheet.Cells[1, 4] = "NhomSanPham";
                    worksheet.Cells[1, 5] = "HangSX";
                    worksheet.Cells[1, 6] = "HinhAnh";
                    worksheet.Cells[1, 7] = "ThongTin";
                    worksheet.Cells[1, 8] = "HanSuDung";
                    worksheet.Cells[1, 9] = "QuyCach";
                    worksheet.Cells[1, 10] = "Dvt";
                    worksheet.Cells[1, 11] = "SoLo";
                    worksheet.Cells[1, 12] = "GiaNhap";
                    worksheet.Cells[1, 13] = "SlNhap";
                    worksheet.Cells[1, 14] = "SlXuat";
                    worksheet.Cells[1, 15] = "SlTon";
                    worksheet.Cells[1, 16] = "NgayHetHan";
                    worksheet.Cells[1, 17] = "GhiChu";
                    worksheet.Cells[1, 18] = "NgayTao";
                    worksheet.Cells[1, 19] = "NgayCapNhat";
                    worksheet.Cells[1, 20] = "NguoiTao";
                }

                // Tìm hàng trống đầu tiên để bắt đầu thêm dữ liệu mới
                int row = worksheet.Cells[worksheet.Rows.Count, 1].End(Excel.XlDirection.xlUp).Row + 1;
                foreach (var item in listNhapKhoCT)
                {
                    worksheet.Cells[row, 1] = item.NhapKhoId;
                    worksheet.Cells[row, 2] = item.NgayNhap.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 3] = item.SanPhamId;
                    worksheet.Cells[row, 4] = item.NhomSanPham;
                    worksheet.Cells[row, 5] = item.HangSX;
                    worksheet.Cells[row, 6] = item.HinhAnh;
                    worksheet.Cells[row, 7] = item.ThongTin;
                    worksheet.Cells[row, 8] = item.HanSuDung.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 9] = item.QuyCach;
                    worksheet.Cells[row, 10] = item.Dvt;
                    worksheet.Cells[row, 11] = item.SoLo;
                    worksheet.Cells[row, 12] = item.GiaNhap;
                    worksheet.Cells[row, 13] = item.SlNhap;
                    worksheet.Cells[row, 14] = item.SlXuat;
                    worksheet.Cells[row, 15] = item.SlTon;
                    worksheet.Cells[row, 16] = item.NgayHetHan.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 17] = item.GhiChu;
                    worksheet.Cells[row, 18] = item.NgayTao.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 19] = item.NgayCapNhat.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 20] = item.NguoiTao;
                    row++;
                }

                // Lưu và đóng workbook
                workbook.SaveAs(filePath);
                MessageBox.Show("Dữ liệu đã được xuất ra file Excel thành công tại: " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu file Excel: " + ex.Message);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

                workbook = null;
                excelApp = null;
                GC.Collect();
            }
        }

        public void ExportToExcelSanPham()
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                Console.WriteLine("Excel is not properly installed!");
                return;
            }
            string directoryPath = @"C:\Temp";
            string filePath = Path.Combine(directoryPath, "SanPhamData.xlsx");
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                if (File.Exists(filePath))
                {
                    // Mở workbook hiện tại nếu tệp đã tồn tại
                    workbook = excelApp.Workbooks.Open(filePath);
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                }
                else
                {
                    // Tạo workbook và worksheet mới nếu tệp không tồn tại
                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                    // Thiết lập tiêu đề cột
                    worksheet.Cells[1, 1] = "Id";
                    worksheet.Cells[1, 2] = "TenSanPham";
                    worksheet.Cells[1, 3] = "HienThi";
                    worksheet.Cells[1, 4] = "NhomSanPham";
                    worksheet.Cells[1, 5] = "HangSX";
                    worksheet.Cells[1, 6] = "HinhAnh";
                    worksheet.Cells[1, 7] = "DiaChi";
                    worksheet.Cells[1, 8] = "ThongTin";
                    worksheet.Cells[1, 9] = "HanSuDung";
                    worksheet.Cells[1, 10] = "QuyCach";
                    worksheet.Cells[1, 11] = "Dvt";
                    worksheet.Cells[1, 12] = "GiaNhap";
                    worksheet.Cells[1, 13] = "SlToiThieu";
                    worksheet.Cells[1, 14] = "SlToiDa";
                    worksheet.Cells[1, 15] = "SlNhap";
                    worksheet.Cells[1, 16] = "SlXuat";
                    worksheet.Cells[1, 17] = "SlTon";
                    worksheet.Cells[1, 18] = "TrangThai";
                    worksheet.Cells[1, 19] = "GhiChu";
                    worksheet.Cells[1, 20] = "NgayTao";
                    worksheet.Cells[1, 21] = "NgayCapNhat";
                    worksheet.Cells[1, 22] = "NguoiTao";
                }

                // Tìm hàng trống đầu tiên để bắt đầu thêm dữ liệu mới
                int row = worksheet.Cells[worksheet.Rows.Count, 1].End(Excel.XlDirection.xlUp).Row + 1;

                foreach (var item in listSanPham)
                {
                    worksheet.Cells[row, 1] = item.Id;
                    worksheet.Cells[row, 2] = item.TenSanPham;
                    worksheet.Cells[row, 3] = item.HienThi;
                    worksheet.Cells[row, 4] = item.NhomSanPham;
                    worksheet.Cells[row, 5] = item.HangSX;
                    worksheet.Cells[row, 6] = item.HinhAnh;
                    worksheet.Cells[row, 7] = item.DiaChi;
                    worksheet.Cells[row, 8] = item.ThongTin;
                    worksheet.Cells[row, 9] = item.HanSuDung.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 10] = item.QuyCach;
                    worksheet.Cells[row, 11] = item.Dvt;
                    worksheet.Cells[row, 12] = item.GiaNhap;
                    worksheet.Cells[row, 13] = item.SlToiThieu;
                    worksheet.Cells[row, 14] = item.SlToiDa;
                    worksheet.Cells[row, 15] = item.SlNhap;
                    worksheet.Cells[row, 16] = item.SlXuat;
                    worksheet.Cells[row, 17] = item.SlTon;
                    worksheet.Cells[row, 18] = item.TrangThai;
                    worksheet.Cells[row, 19] = item.GhiChu;
                    worksheet.Cells[row, 20] = item.NgayTao.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 21] = item.NgayCapNhat.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 22] = item.NguoiTao;
                    row++;
                }

                // Lưu và đóng workbook
                workbook.SaveAs(filePath);
                Console.WriteLine("Dữ liệu đã được xuất ra file Excel thành công tại: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra khi lưu file Excel: " + ex.Message);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

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

        private void frmNhapKhoChiTiet_Load(object sender, EventArgs e)
        {
            var listsp = _UnitService.SanPhamService.GetAllSanPham();
            txtSanPham.Properties.DataSource = listsp;
            txtSanPham.Properties.DisplayMember = "TenSanPham";  // Thuộc tính để hiển thị
            txtSanPham.Properties.ValueMember = "Id";

        }
    }
    
}