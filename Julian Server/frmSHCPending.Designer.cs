namespace Julian_Server
{
    partial class frmSHCPending
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSHCPending));
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaHang = new System.Windows.Forms.TextBox();
            this.txtGhiChuXuatHang = new System.Windows.Forms.TextBox();
            this.txtNgayXuatHang = new System.Windows.Forms.TextBox();
            this.txtMaDonKH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFileName_OrderForm = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectFile_OrderForm = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Found";
            this.Column6.FillWeight = 14F;
            this.Column6.HeaderText = "Found";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Material";
            this.Column3.FillWeight = 24F;
            this.Column3.HeaderText = "Mã Hàng";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PurchOrder";
            this.Column2.FillWeight = 24F;
            this.Column2.HeaderText = "Mã Đơn KH";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SheetName";
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "Sheet";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(256, 9);
            this.dgvData.Margin = new System.Windows.Forms.Padding(0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(517, 214);
            this.dgvData.TabIndex = 171;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "OpenQty";
            this.Column4.FillWeight = 18F;
            this.Column4.HeaderText = "Qty";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // txtMaHang
            // 
            this.txtMaHang.Location = new System.Drawing.Point(118, 93);
            this.txtMaHang.Name = "txtMaHang";
            this.txtMaHang.Size = new System.Drawing.Size(133, 20);
            this.txtMaHang.TabIndex = 170;
            // 
            // txtGhiChuXuatHang
            // 
            this.txtGhiChuXuatHang.Location = new System.Drawing.Point(118, 145);
            this.txtGhiChuXuatHang.Name = "txtGhiChuXuatHang";
            this.txtGhiChuXuatHang.Size = new System.Drawing.Size(133, 20);
            this.txtGhiChuXuatHang.TabIndex = 169;
            // 
            // txtNgayXuatHang
            // 
            this.txtNgayXuatHang.Location = new System.Drawing.Point(118, 119);
            this.txtNgayXuatHang.Name = "txtNgayXuatHang";
            this.txtNgayXuatHang.Size = new System.Drawing.Size(133, 20);
            this.txtNgayXuatHang.TabIndex = 168;
            // 
            // txtMaDonKH
            // 
            this.txtMaDonKH.Location = new System.Drawing.Point(118, 67);
            this.txtMaDonKH.Name = "txtMaDonKH";
            this.txtMaDonKH.Size = new System.Drawing.Size(133, 20);
            this.txtMaDonKH.TabIndex = 167;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 164;
            this.label2.Text = "Mã hàng";
            // 
            // cbSheet
            // 
            this.cbSheet.FormattingEnabled = true;
            this.cbSheet.Location = new System.Drawing.Point(118, 40);
            this.cbSheet.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheet.Name = "cbSheet";
            this.cbSheet.Size = new System.Drawing.Size(133, 21);
            this.cbSheet.TabIndex = 166;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 148);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 165;
            this.label1.Text = "Ghi chú xuất hàng";
            // 
            // lblFileName_OrderForm
            // 
            this.lblFileName_OrderForm.AutoEllipsis = true;
            this.lblFileName_OrderForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_OrderForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_OrderForm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName_OrderForm.Location = new System.Drawing.Point(118, 11);
            this.lblFileName_OrderForm.Name = "lblFileName_OrderForm";
            this.lblFileName_OrderForm.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName_OrderForm.Size = new System.Drawing.Size(133, 22);
            this.lblFileName_OrderForm.TabIndex = 159;
            this.lblFileName_OrderForm.Text = "...";
            this.lblFileName_OrderForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 122);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 163;
            this.label11.Text = "Ngày gửi list kho xh";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 162;
            this.label9.Text = "OrderForm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 70);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 161;
            this.label10.Text = "Mã đơn KH";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 160;
            this.label8.Text = "Sheet";
            // 
            // btnSelectFile_OrderForm
            // 
            this.btnSelectFile_OrderForm.AutoEllipsis = true;
            this.btnSelectFile_OrderForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_OrderForm.BackgroundImage")));
            this.btnSelectFile_OrderForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_OrderForm.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_OrderForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_OrderForm.Location = new System.Drawing.Point(75, 9);
            this.btnSelectFile_OrderForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile_OrderForm.Name = "btnSelectFile_OrderForm";
            this.btnSelectFile_OrderForm.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFile_OrderForm.TabIndex = 158;
            this.btnSelectFile_OrderForm.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(62, 208);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(189, 35);
            this.lblStatus.TabIndex = 157;
            this.lblStatus.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 156;
            this.label6.Text = "Status:";
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Location = new System.Drawing.Point(19, 175);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(63, 17);
            this.chkBackup.TabIndex = 155;
            this.chkBackup.Text = "Backup";
            this.chkBackup.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(118, 171);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(133, 23);
            this.btnStart.TabIndex = 154;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // frmSHCPending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 232);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtMaHang);
            this.Controls.Add(this.txtGhiChuXuatHang);
            this.Controls.Add(this.txtNgayXuatHang);
            this.Controls.Add(this.txtMaDonKH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSheet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFileName_OrderForm);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSelectFile_OrderForm);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.btnStart);
            this.Name = "frmSHCPending";
            this.Text = "frmSHCPending";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.TextBox txtGhiChuXuatHang;
        private System.Windows.Forms.TextBox txtNgayXuatHang;
        private System.Windows.Forms.TextBox txtMaDonKH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFileName_OrderForm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSelectFile_OrderForm;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnStart;
    }
}