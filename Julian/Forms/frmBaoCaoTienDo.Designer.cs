namespace Julian.Forms
{
    partial class frmBaoCaoTienDo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaoCaoTienDo));
            this.gb_Customer = new System.Windows.Forms.GroupBox();
            this.txtCotGhiDuLieu = new System.Windows.Forms.TextBox();
            this.txtMaHangKH_Customer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaDonKH_Customer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSheets_Customer = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFileName_Customer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile_Customer = new System.Windows.Forms.Button();
            this.gb_OrderForm = new System.Windows.Forms.GroupBox();
            this.txtPoNhuom_OrderForm = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMaHangKH_OrderForm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaDonKH_OrderForm = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSheets_OrderForm = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFileName_OrderForm = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSelectFile_OrderForm = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTotalFind = new System.Windows.Forms.Label();
            this.lblReport = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.gb_Customer.SuspendLayout();
            this.gb_OrderForm.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Customer
            // 
            this.gb_Customer.Controls.Add(this.txtCotGhiDuLieu);
            this.gb_Customer.Controls.Add(this.txtMaHangKH_Customer);
            this.gb_Customer.Controls.Add(this.label3);
            this.gb_Customer.Controls.Add(this.label2);
            this.gb_Customer.Controls.Add(this.txtMaDonKH_Customer);
            this.gb_Customer.Controls.Add(this.label6);
            this.gb_Customer.Controls.Add(this.cbSheets_Customer);
            this.gb_Customer.Controls.Add(this.label7);
            this.gb_Customer.Controls.Add(this.lblFileName_Customer);
            this.gb_Customer.Controls.Add(this.label1);
            this.gb_Customer.Controls.Add(this.btnSelectFile_Customer);
            this.gb_Customer.Location = new System.Drawing.Point(5, 5);
            this.gb_Customer.Name = "gb_Customer";
            this.gb_Customer.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb_Customer.Size = new System.Drawing.Size(375, 116);
            this.gb_Customer.TabIndex = 117;
            this.gb_Customer.TabStop = false;
            this.gb_Customer.Text = "File KH gửi";
            // 
            // txtCotGhiDuLieu
            // 
            this.txtCotGhiDuLieu.Location = new System.Drawing.Point(263, 81);
            this.txtCotGhiDuLieu.Name = "txtCotGhiDuLieu";
            this.txtCotGhiDuLieu.Size = new System.Drawing.Size(75, 20);
            this.txtCotGhiDuLieu.TabIndex = 120;
            this.txtCotGhiDuLieu.Text = "P";
            // 
            // txtMaHangKH_Customer
            // 
            this.txtMaHangKH_Customer.Location = new System.Drawing.Point(182, 81);
            this.txtMaHangKH_Customer.Name = "txtMaHangKH_Customer";
            this.txtMaHangKH_Customer.Size = new System.Drawing.Size(75, 20);
            this.txtMaHangKH_Customer.TabIndex = 119;
            this.txtMaHangKH_Customer.Text = "D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 122;
            this.label3.Text = "Cột ghi dữ liệu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Mã Liệu KH";
            // 
            // txtMaDonKH_Customer
            // 
            this.txtMaDonKH_Customer.Location = new System.Drawing.Point(101, 81);
            this.txtMaDonKH_Customer.Name = "txtMaDonKH_Customer";
            this.txtMaDonKH_Customer.Size = new System.Drawing.Size(75, 20);
            this.txtMaDonKH_Customer.TabIndex = 119;
            this.txtMaDonKH_Customer.Text = "C";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 122;
            this.label6.Text = "Mã đơn KH";
            // 
            // cbSheets_Customer
            // 
            this.cbSheets_Customer.FormattingEnabled = true;
            this.cbSheets_Customer.Location = new System.Drawing.Point(18, 81);
            this.cbSheets_Customer.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets_Customer.Name = "cbSheets_Customer";
            this.cbSheets_Customer.Size = new System.Drawing.Size(75, 21);
            this.cbSheets_Customer.TabIndex = 118;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 163);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 117;
            // 
            // lblFileName_Customer
            // 
            this.lblFileName_Customer.AutoEllipsis = true;
            this.lblFileName_Customer.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_Customer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_Customer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName_Customer.Location = new System.Drawing.Point(18, 24);
            this.lblFileName_Customer.Name = "lblFileName_Customer";
            this.lblFileName_Customer.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName_Customer.Size = new System.Drawing.Size(316, 24);
            this.lblFileName_Customer.TabIndex = 115;
            this.lblFileName_Customer.Text = "...";
            this.lblFileName_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Sheet";
            // 
            // btnSelectFile_Customer
            // 
            this.btnSelectFile_Customer.AutoEllipsis = true;
            this.btnSelectFile_Customer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_Customer.BackgroundImage")));
            this.btnSelectFile_Customer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_Customer.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_Customer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_Customer.Location = new System.Drawing.Point(341, 24);
            this.btnSelectFile_Customer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile_Customer.Name = "btnSelectFile_Customer";
            this.btnSelectFile_Customer.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFile_Customer.TabIndex = 114;
            this.btnSelectFile_Customer.UseVisualStyleBackColor = true;
            this.btnSelectFile_Customer.Click += new System.EventHandler(this.btnSelectFile_Customer_Click);
            // 
            // gb_OrderForm
            // 
            this.gb_OrderForm.Controls.Add(this.txtPoNhuom_OrderForm);
            this.gb_OrderForm.Controls.Add(this.label17);
            this.gb_OrderForm.Controls.Add(this.txtMaHangKH_OrderForm);
            this.gb_OrderForm.Controls.Add(this.label5);
            this.gb_OrderForm.Controls.Add(this.txtMaDonKH_OrderForm);
            this.gb_OrderForm.Controls.Add(this.label8);
            this.gb_OrderForm.Controls.Add(this.cbSheets_OrderForm);
            this.gb_OrderForm.Controls.Add(this.label9);
            this.gb_OrderForm.Controls.Add(this.lblFileName_OrderForm);
            this.gb_OrderForm.Controls.Add(this.label11);
            this.gb_OrderForm.Controls.Add(this.btnSelectFile_OrderForm);
            this.gb_OrderForm.Location = new System.Drawing.Point(5, 134);
            this.gb_OrderForm.Name = "gb_OrderForm";
            this.gb_OrderForm.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb_OrderForm.Size = new System.Drawing.Size(375, 116);
            this.gb_OrderForm.TabIndex = 117;
            this.gb_OrderForm.TabStop = false;
            this.gb_OrderForm.Text = "File OrderForm";
            // 
            // txtPoNhuom_OrderForm
            // 
            this.txtPoNhuom_OrderForm.Location = new System.Drawing.Point(262, 81);
            this.txtPoNhuom_OrderForm.Name = "txtPoNhuom_OrderForm";
            this.txtPoNhuom_OrderForm.Size = new System.Drawing.Size(75, 20);
            this.txtPoNhuom_OrderForm.TabIndex = 119;
            this.txtPoNhuom_OrderForm.Text = "H";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(259, 65);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label17.Size = new System.Drawing.Size(81, 13);
            this.label17.TabIndex = 122;
            this.label17.Text = "PO nhà nhuộm";
            // 
            // txtMaHangKH_OrderForm
            // 
            this.txtMaHangKH_OrderForm.Location = new System.Drawing.Point(182, 81);
            this.txtMaHangKH_OrderForm.Name = "txtMaHangKH_OrderForm";
            this.txtMaHangKH_OrderForm.Size = new System.Drawing.Size(75, 20);
            this.txtMaHangKH_OrderForm.TabIndex = 119;
            this.txtMaHangKH_OrderForm.Text = "J";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "Mã Liệu KH";
            // 
            // txtMaDonKH_OrderForm
            // 
            this.txtMaDonKH_OrderForm.Location = new System.Drawing.Point(101, 81);
            this.txtMaDonKH_OrderForm.Name = "txtMaDonKH_OrderForm";
            this.txtMaDonKH_OrderForm.Size = new System.Drawing.Size(75, 20);
            this.txtMaDonKH_OrderForm.TabIndex = 119;
            this.txtMaDonKH_OrderForm.Text = "I";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "Mã đơn KH";
            // 
            // cbSheets_OrderForm
            // 
            this.cbSheets_OrderForm.FormattingEnabled = true;
            this.cbSheets_OrderForm.Location = new System.Drawing.Point(18, 81);
            this.cbSheets_OrderForm.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets_OrderForm.Name = "cbSheets_OrderForm";
            this.cbSheets_OrderForm.Size = new System.Drawing.Size(75, 21);
            this.cbSheets_OrderForm.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 163);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 117;
            // 
            // lblFileName_OrderForm
            // 
            this.lblFileName_OrderForm.AutoEllipsis = true;
            this.lblFileName_OrderForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_OrderForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_OrderForm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName_OrderForm.Location = new System.Drawing.Point(18, 24);
            this.lblFileName_OrderForm.Name = "lblFileName_OrderForm";
            this.lblFileName_OrderForm.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName_OrderForm.Size = new System.Drawing.Size(316, 24);
            this.lblFileName_OrderForm.TabIndex = 115;
            this.lblFileName_OrderForm.Text = "...";
            this.lblFileName_OrderForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 65);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 117;
            this.label11.Text = "Sheet";
            // 
            // btnSelectFile_OrderForm
            // 
            this.btnSelectFile_OrderForm.AutoEllipsis = true;
            this.btnSelectFile_OrderForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_OrderForm.BackgroundImage")));
            this.btnSelectFile_OrderForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_OrderForm.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_OrderForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_OrderForm.Location = new System.Drawing.Point(341, 24);
            this.btnSelectFile_OrderForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile_OrderForm.Name = "btnSelectFile_OrderForm";
            this.btnSelectFile_OrderForm.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFile_OrderForm.TabIndex = 114;
            this.btnSelectFile_OrderForm.UseVisualStyleBackColor = true;
            this.btnSelectFile_OrderForm.Click += new System.EventHandler(this.btnSelectFile_OrderForm_Click);
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(7, 256);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 131;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblTotalFind
            // 
            this.lblTotalFind.AutoSize = true;
            this.lblTotalFind.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalFind.Location = new System.Drawing.Point(325, 264);
            this.lblTotalFind.Name = "lblTotalFind";
            this.lblTotalFind.Size = new System.Drawing.Size(13, 13);
            this.lblTotalFind.TabIndex = 135;
            this.lblTotalFind.Text = "0";
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.BackColor = System.Drawing.Color.Transparent;
            this.lblReport.Location = new System.Drawing.Point(163, 264);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(24, 13);
            this.lblReport.TabIndex = 136;
            this.lblReport.Text = "0/0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(88, 258);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 20);
            this.progressBar1.TabIndex = 134;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtLogs);
            this.groupBox3.Location = new System.Drawing.Point(2, 282);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox3.Size = new System.Drawing.Size(378, 122);
            this.groupBox3.TabIndex = 133;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // txtLogs
            // 
            this.txtLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogs.Location = new System.Drawing.Point(5, 18);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(368, 99);
            this.txtLogs.TabIndex = 109;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(269, 263);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 132;
            this.label18.Text = "Tìm thấy:";
            // 
            // frmBaoCaoTienDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 406);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTotalFind);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.gb_OrderForm);
            this.Controls.Add(this.gb_Customer);
            this.Name = "frmBaoCaoTienDo";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "frmBaoCaoTienDo";
            this.Load += new System.EventHandler(this.frmBaoCaoTienDo_Load);
            this.gb_Customer.ResumeLayout(false);
            this.gb_Customer.PerformLayout();
            this.gb_OrderForm.ResumeLayout(false);
            this.gb_OrderForm.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Customer;
        private System.Windows.Forms.TextBox txtCotGhiDuLieu;
        private System.Windows.Forms.TextBox txtMaDonKH_Customer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSheets_Customer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFileName_Customer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFile_Customer;
        private System.Windows.Forms.TextBox txtMaHangKH_Customer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_OrderForm;
        private System.Windows.Forms.TextBox txtMaHangKH_OrderForm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaDonKH_OrderForm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSheets_OrderForm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFileName_OrderForm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSelectFile_OrderForm;
        private System.Windows.Forms.TextBox txtPoNhuom_OrderForm;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTotalFind;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Label label18;
    }
}