using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkHouse.userControl;

namespace WorkHouse
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void btnNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            userNhapKho userNhapKho = new userNhapKho();    
            userNhapKho.Size = paneMain.ClientSize;
            userNhapKho.Dock = DockStyle.Fill;
            paneMain.Controls.Clear();
            paneMain.Controls.Add(userNhapKho);
        }

        private void btnXuatKho_ItemClick(object sender, ItemClickEventArgs e)
        {

            userXuatKho userXuatKho = new userXuatKho();
            userXuatKho.Size = paneMain.ClientSize;
            userXuatKho.Dock = DockStyle.Fill;
            paneMain.Controls.Clear();
            paneMain.Controls.Add(userXuatKho);
        }
    }
}