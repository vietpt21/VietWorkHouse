namespace WorkHouse
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNhapKho = new DevExpress.XtraBars.BarButtonItem();
            this.btnXuatKho = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.paneMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNhapKho,
            this.btnXuatKho});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 3;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1180, 158);
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Caption = "Nhập Kho";
            this.btnNhapKho.Id = 1;
            this.btnNhapKho.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNhapKho.ImageOptions.Image")));
            this.btnNhapKho.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNhapKho.ImageOptions.LargeImage")));
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNhapKho_ItemClick);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Caption = "Xuất Kho";
            this.btnXuatKho.Id = 2;
            this.btnXuatKho.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatKho.ImageOptions.Image")));
            this.btnXuatKho.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnXuatKho.ImageOptions.LargeImage")));
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXuatKho_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Kho Hang";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNhapKho);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnXuatKho);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // paneMain
            // 
            this.paneMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneMain.Location = new System.Drawing.Point(0, 158);
            this.paneMain.Name = "paneMain";
            this.paneMain.Size = new System.Drawing.Size(1180, 530);
            this.paneMain.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 688);
            this.Controls.Add(this.paneMain);
            this.Controls.Add(this.ribbon);
            this.Name = "frmMain";
            this.Ribbon = this.ribbon;
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnNhapKho;
        private DevExpress.XtraBars.BarButtonItem btnXuatKho;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private System.Windows.Forms.Panel paneMain;
    }
}