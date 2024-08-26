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

namespace WorkHouse.userControl.formControl
{
    public partial class frmNhapKho : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        static readonly string connectionString = "Data Source=localhost;Initial Catalog=QLKho;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        static UnitService _dbUnit;
        private userNhapKho _userNhapKho;
        public string IdNhapKho { get; set; }
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

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            NhapKho nhapKho = new NhapKho
            {
                Id = IdNhapKho,
                LoaiNhap = cboLoaiNhap.SelectedItem.ToString(),
                NgayNhap = DateTime.Parse(txtNgayNhap.Text),
                NccId = (int)txtNCC.EditValue,
                KhoId = (int)txtKho.EditValue,
                SlNhap = int.Parse(txtSoLuongNhap.Text),
                NguoiGiao = txtNguoiGiao.Text,
                NoiDungNhap = txtNoiDungNhap.Text,
                NgayTao = DateTime.Parse(txtNgayTao.Text),
                NgayCapNhat = DateTime.Parse(txtNgayCapNhat.Text),
                NguoiTao = txtNguoiTao.Text, // Thay đổi theo người dùng hiện tại

            };
            _dbUnit.NhapKhoService.AddNhapKho(nhapKho);
            ResetNhapKho();
            frmNhapKhoChiTiet frm = new frmNhapKhoChiTiet();
            frm.Show();
        }
    }
}