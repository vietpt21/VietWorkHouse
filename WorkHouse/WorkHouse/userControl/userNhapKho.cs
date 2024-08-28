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
          
        
            frmNhapKho frmNhapKho = new frmNhapKho();
            frmNhapKho.Show();
        }

     
    }
}
