using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkHouse.Model;
using WorkHouse.userControl.formControl;

namespace WorkHouse.userControl
{
    public partial class userNhapKho : DevExpress.XtraEditors.XtraUserControl
    {
        public userNhapKho()
        {
            InitializeComponent();
        }
        public string IdNhapKho { get; set; }
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            int stt = 1; // Bạn có thể thay đổi giá trị STT để tạo các mã ID khác nhau
            string idNhapKho = GenerateId(stt);
            IdNhapKho = idNhapKho; // Cập nhật IdNhapKho của userNhapKho

            // Tạo đối tượng frmNhapKho và thiết lập IdNhapKho sau đó
            frmNhapKho frmNhapKho = new frmNhapKho();
            frmNhapKho.IdNhapKho = IdNhapKho;
            frmNhapKho.Show();
        }
        static string GenerateId(int stt)
        {
            // Lấy ngày hiện tại
            DateTime now = DateTime.Now;

            // Tạo chuỗi định dạng
            string dateStr = now.ToString("ddMMyy"); // Ngày tháng năm theo định dạng ddMMyy
            string idCode = $"NH{dateStr}{stt:00}";   // Thêm STT vào mã ID (đảm bảo STT có 2 chữ số)

            return idCode;
        }
    }
}
