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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkHouse.Model;
using WorkHouse.Service;
using WorkHouse.WorkHouse;
using Excel = Microsoft.Office.Interop.Excel;

namespace WorkHouse.userControl.formControl
{
    public partial class frmNhapKho : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        static readonly string connectionString = "Data Source=localhost;Initial Catalog=QLKho;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        static UnitService _dbUnit;
        string NewID = String.Empty;
        private userNhapKho _userNhapKho;
        SoaLib soaLib = new SoaLib(connectionString);
      
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
           /* txtKho.Text = string.Empty;
            txtNCC.Text = string.Empty;
            cboLoaiNhap.SelectedItem = null;
            txtNguoiTao.Text = string.Empty;
            txtSoLuongNhap.Text = string.Empty;
            txtNoiDungNhap.Text = string.Empty;
            txtNguoiGiao.Text = string.Empty;
            txtNgayNhap.Text = string.Empty;
            txtNgayTao.Text = string.Empty;
            txtNgayCapNhat.Text = string.Empty;*/
        }
        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            NewID  = soaLib.GenerateId();
            lblIdNhap.Text = NewID;
            var KhoList = _dbUnit.KhoService.GetAllKho();
            var nccList = _dbUnit.NCCService.GetAllNCC();
            txtKho.Properties.DataSource = KhoList;
            txtKho.Properties.ValueMember = "Id";  
            txtNCC.Properties.DataSource = nccList;
            txtNCC.Properties.ValueMember = "Id";
        }
        private bool ValidateNhapKhoData()
        {
            
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


            if (ValidateNhapKhoData())
            {
                NhapKho nhapKho = new NhapKho
                {
                    Id = lblIdNhap.Text,
                    LoaiNhap = cboLoaiNhap.SelectedItem.ToString(),
                    NgayNhap = DateTime.Now,
                    NccId = (int)txtNCC.EditValue,
                    KhoId = (int)txtKho.EditValue,
                    SlNhap = int.Parse(txtSoLuongNhap.Text),
                    NguoiGiao = txtNguoiGiao.Text,
                    NoiDungNhap = txtNoiDungNhap.Text,
                    NgayTao = DateTime.Parse(txtNgayTao.Text),
                    NgayCapNhat = DateTime.Parse(txtNgayCapNhat.Text),
                    NguoiTao = txtNguoiTao.Text,
                };
                listNhapKho.Add(nhapKho);
                this.Enabled = false;
                frmNhapKhoChiTiet frm = new frmNhapKhoChiTiet();
                frm.FormClosing += new FormClosingEventHandler(frmNhapKhoChiTiet_FormClosing);
                frm.FormClosed += new FormClosedEventHandler(frmNhapKhoChiTiet_FormClosed);
                /* frm.TopMost = true;*/
                frm.Show();
                frm.IdNhapKho = nhapKho.Id;
                frm.nhapkho = nhapKho;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại các trường dữ liệu.");
            }
        }
        private void ExportToExcel()
        {

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
                // Giải phóng các đối tượng COM
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

        private void frmNhapKhoChiTiet_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;

        }
        private void frmNhapKhoChiTiet_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = true;
        }
    }
}