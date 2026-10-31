namespace NR
{
    partial class frmMain2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain2));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tsNR = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsNR_LstDiem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNR_PGH = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNR_INVPKL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRR = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsDLDN = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsEMP = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNR,
            this.tsRR,
            this.tsDLDN,
            this.tsEMP});
            this.toolStrip2.Location = new System.Drawing.Point(1, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(1182, 27);
            this.toolStrip2.TabIndex = 19;
            this.toolStrip2.Text = "Julian Server";
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
            this.tcMain.Size = new System.Drawing.Size(1174, 678);
            this.tcMain.TabIndex = 20;
            // 
            // tsNR
            // 
            this.tsNR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNR_LstDiem,
            this.tsNR_PGH,
            this.tsNR_INVPKL});
            this.tsNR.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsNR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNR.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsNR.Name = "tsNR";
            this.tsNR.Size = new System.Drawing.Size(42, 24);
            this.tsNR.Text = "NR";
            this.tsNR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsNR_LstDiem
            // 
            this.tsNR_LstDiem.Name = "tsNR_LstDiem";
            this.tsNR_LstDiem.Size = new System.Drawing.Size(189, 24);
            this.tsNR_LstDiem.Text = "List Gửi Diễm";
            this.tsNR_LstDiem.Click += new System.EventHandler(this.tsNR_LstDiem_Click);
            // 
            // tsNR_PGH
            // 
            this.tsNR_PGH.Name = "tsNR_PGH";
            this.tsNR_PGH.Size = new System.Drawing.Size(189, 24);
            this.tsNR_PGH.Text = "Phiếu Giao Hàng";
            // 
            // tsNR_INVPKL
            // 
            this.tsNR_INVPKL.Name = "tsNR_INVPKL";
            this.tsNR_INVPKL.Size = new System.Drawing.Size(189, 24);
            this.tsNR_INVPKL.Text = "INV-PKL";
            // 
            // tsRR
            // 
            this.tsRR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsRR.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsRR.Image = ((System.Drawing.Image)(resources.GetObject("tsRR.Image")));
            this.tsRR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRR.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsRR.Name = "tsRR";
            this.tsRR.Size = new System.Drawing.Size(40, 24);
            this.tsRR.Text = "RR";
            this.tsRR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsDLDN
            // 
            this.tsDLDN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsDLDN.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsDLDN.Image = ((System.Drawing.Image)(resources.GetObject("tsDLDN.Image")));
            this.tsDLDN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDLDN.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsDLDN.Name = "tsDLDN";
            this.tsDLDN.Size = new System.Drawing.Size(62, 24);
            this.tsDLDN.Text = "DLDN";
            // 
            // tsEMP
            // 
            this.tsEMP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsEMP.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsEMP.Image = ((System.Drawing.Image)(resources.GetObject("tsEMP.Image")));
            this.tsEMP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEMP.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsEMP.Name = "tsEMP";
            this.tsEMP.Size = new System.Drawing.Size(51, 24);
            this.tsEMP.Text = "EMP";
            // 
            // frmMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 711);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip2);
            this.Name = "frmMain2";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "frmMain2";
            this.Load += new System.EventHandler(this.frmMain2_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.ToolStripDropDownButton tsNR;
        private System.Windows.Forms.ToolStripMenuItem tsNR_LstDiem;
        private System.Windows.Forms.ToolStripMenuItem tsNR_PGH;
        private System.Windows.Forms.ToolStripMenuItem tsNR_INVPKL;
        private System.Windows.Forms.ToolStripDropDownButton tsRR;
        private System.Windows.Forms.ToolStripDropDownButton tsDLDN;
        private System.Windows.Forms.ToolStripDropDownButton tsEMP;
    }
}