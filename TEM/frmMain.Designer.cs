namespace TEM
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
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsCreateTEM = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsSHC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTX = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSGC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupByPO = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHoiHang = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBaoCaoSanLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.tsThemVaiMoc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBaoCaoTienDo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsThoaBe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsThoaBeSHC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsThoaBeSHD = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCreateTEM});
            this.toolStrip2.Location = new System.Drawing.Point(1, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(1182, 25);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "Julian Server";
            // 
            // tsCreateTEM
            // 
            this.tsCreateTEM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSHC,
            this.tsTX,
            this.tsSGC});
            this.tsCreateTEM.Image = global::TEM.Properties.Resources.plus;
            this.tsCreateTEM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsCreateTEM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCreateTEM.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsCreateTEM.Name = "tsCreateTEM";
            this.tsCreateTEM.Size = new System.Drawing.Size(83, 22);
            this.tsCreateTEM.Text = "Tạo TEM";
            this.tsCreateTEM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsSHC
            // 
            this.tsSHC.Name = "tsSHC";
            this.tsSHC.Size = new System.Drawing.Size(180, 22);
            this.tsSHC.Text = "SHC";
            this.tsSHC.Click += new System.EventHandler(this.tsSHC_Click);
            // 
            // tsTX
            // 
            this.tsTX.Name = "tsTX";
            this.tsTX.Size = new System.Drawing.Size(180, 22);
            this.tsTX.Text = "TX";
            this.tsTX.Click += new System.EventHandler(this.tsTX_Click);
            // 
            // tsSGC
            // 
            this.tsSGC.Name = "tsSGC";
            this.tsSGC.Size = new System.Drawing.Size(180, 22);
            this.tsSGC.Text = "SGC";
            this.tsSGC.Click += new System.EventHandler(this.tsSGC_Click);
            // 
            // tsGroupByPO
            // 
            this.tsGroupByPO.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsGroupByPO.Name = "tsGroupByPO";
            this.tsGroupByPO.Size = new System.Drawing.Size(220, 22);
            this.tsGroupByPO.Text = "Gộp PO";
            // 
            // tsAddOrder
            // 
            this.tsAddOrder.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsAddOrder.Name = "tsAddOrder";
            this.tsAddOrder.Size = new System.Drawing.Size(220, 22);
            this.tsAddOrder.Text = "Thêm Đơn Hàng";
            // 
            // tsCompare
            // 
            this.tsCompare.Name = "tsCompare";
            this.tsCompare.Size = new System.Drawing.Size(220, 22);
            this.tsCompare.Text = "So Sánh Dữ Liệu";
            // 
            // tsHoiHang
            // 
            this.tsHoiHang.Name = "tsHoiHang";
            this.tsHoiHang.Size = new System.Drawing.Size(220, 22);
            this.tsHoiHang.Text = "Hối hàng";
            // 
            // tsBaoCaoSanLuong
            // 
            this.tsBaoCaoSanLuong.Name = "tsBaoCaoSanLuong";
            this.tsBaoCaoSanLuong.Size = new System.Drawing.Size(220, 22);
            this.tsBaoCaoSanLuong.Text = "Báo Cáo Sản Lượng";
            // 
            // tsThemVaiMoc
            // 
            this.tsThemVaiMoc.Name = "tsThemVaiMoc";
            this.tsThemVaiMoc.Size = new System.Drawing.Size(220, 22);
            this.tsThemVaiMoc.Text = "Thêm Vải Mộc Vào Báo Cáo";
            // 
            // tsBaoCaoTienDo
            // 
            this.tsBaoCaoTienDo.Name = "tsBaoCaoTienDo";
            this.tsBaoCaoTienDo.Size = new System.Drawing.Size(220, 22);
            this.tsBaoCaoTienDo.Text = "Báo cáo tiến độ";
            // 
            // tsThoaBe
            // 
            this.tsThoaBe.Name = "tsThoaBe";
            this.tsThoaBe.Size = new System.Drawing.Size(220, 22);
            this.tsThoaBe.Text = "Thoa Bé";
            // 
            // tsThoaBeSHC
            // 
            this.tsThoaBeSHC.Name = "tsThoaBeSHC";
            this.tsThoaBeSHC.Size = new System.Drawing.Size(97, 22);
            this.tsThoaBeSHC.Text = "SHC";
            // 
            // tsThoaBeSHD
            // 
            this.tsThoaBeSHD.Name = "tsThoaBeSHD";
            this.tsThoaBeSHD.Size = new System.Drawing.Size(97, 22);
            this.tsThoaBeSHD.Text = "SHD";
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(5, 29);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(10, 5);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1178, 628);
            this.tcMain.TabIndex = 19;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip2);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "frmMain";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton tsCreateTEM;
        private System.Windows.Forms.ToolStripMenuItem tsGroupByPO;
        private System.Windows.Forms.ToolStripMenuItem tsAddOrder;
        private System.Windows.Forms.ToolStripMenuItem tsCompare;
        private System.Windows.Forms.ToolStripMenuItem tsHoiHang;
        private System.Windows.Forms.ToolStripMenuItem tsBaoCaoSanLuong;
        private System.Windows.Forms.ToolStripMenuItem tsThemVaiMoc;
        private System.Windows.Forms.ToolStripMenuItem tsBaoCaoTienDo;
        private System.Windows.Forms.ToolStripMenuItem tsThoaBe;
        private System.Windows.Forms.ToolStripMenuItem tsThoaBeSHC;
        private System.Windows.Forms.ToolStripMenuItem tsThoaBeSHD;
        private System.Windows.Forms.ToolStripMenuItem tsSHC;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.ToolStripMenuItem tsTX;
        private System.Windows.Forms.ToolStripMenuItem tsSGC;
    }
}