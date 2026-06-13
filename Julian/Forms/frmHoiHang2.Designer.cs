namespace Julian.Forms
{
    partial class frmHoiHang2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHoiHang2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTotalFound = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.txtPoNhuom1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSheets1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFileName1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectedFile1 = new System.Windows.Forms.Button();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.btnXNYVConfig = new System.Windows.Forms.Button();
            this.cbSheets2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFileName2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectedFile2 = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalRow = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb1.SuspendLayout();
            this.gb2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(4, 388);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 132;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblTotalFound
            // 
            this.lblTotalFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalFound.AutoSize = true;
            this.lblTotalFound.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalFound.Location = new System.Drawing.Point(262, 393);
            this.lblTotalFound.Name = "lblTotalFound";
            this.lblTotalFound.Size = new System.Drawing.Size(13, 13);
            this.lblTotalFound.TabIndex = 136;
            this.lblTotalFound.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 393);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 133;
            this.label4.Text = "Tìm thấy:";
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.txtPoNhuom1);
            this.gb1.Controls.Add(this.label6);
            this.gb1.Controls.Add(this.cbSheets1);
            this.gb1.Controls.Add(this.label7);
            this.gb1.Controls.Add(this.lblFileName1);
            this.gb1.Controls.Add(this.label1);
            this.gb1.Controls.Add(this.btnSelectedFile1);
            this.gb1.Location = new System.Drawing.Point(4, 4);
            this.gb1.Name = "gb1";
            this.gb1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb1.Size = new System.Drawing.Size(400, 91);
            this.gb1.TabIndex = 139;
            this.gb1.TabStop = false;
            this.gb1.Text = "File hối hàng";
            // 
            // txtPoNhuom1
            // 
            this.txtPoNhuom1.Location = new System.Drawing.Point(114, 62);
            this.txtPoNhuom1.Name = "txtPoNhuom1";
            this.txtPoNhuom1.Size = new System.Drawing.Size(78, 20);
            this.txtPoNhuom1.TabIndex = 119;
            this.txtPoNhuom1.Text = "F";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 122;
            this.label6.Text = "PO nhà nhuộm";
            // 
            // cbSheets1
            // 
            this.cbSheets1.FormattingEnabled = true;
            this.cbSheets1.Location = new System.Drawing.Point(6, 61);
            this.cbSheets1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets1.Name = "cbSheets1";
            this.cbSheets1.Size = new System.Drawing.Size(100, 21);
            this.cbSheets1.TabIndex = 118;
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
            // lblFileName1
            // 
            this.lblFileName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileName1.AutoEllipsis = true;
            this.lblFileName1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName1.Location = new System.Drawing.Point(7, 16);
            this.lblFileName1.Name = "lblFileName1";
            this.lblFileName1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName1.Size = new System.Drawing.Size(352, 24);
            this.lblFileName1.TabIndex = 115;
            this.lblFileName1.Text = "...";
            this.lblFileName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Sheet";
            // 
            // btnSelectedFile1
            // 
            this.btnSelectedFile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectedFile1.AutoEllipsis = true;
            this.btnSelectedFile1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectedFile1.BackgroundImage")));
            this.btnSelectedFile1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectedFile1.FlatAppearance.BorderSize = 0;
            this.btnSelectedFile1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectedFile1.Location = new System.Drawing.Point(366, 15);
            this.btnSelectedFile1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectedFile1.Name = "btnSelectedFile1";
            this.btnSelectedFile1.Size = new System.Drawing.Size(26, 26);
            this.btnSelectedFile1.TabIndex = 114;
            this.btnSelectedFile1.UseVisualStyleBackColor = true;
            this.btnSelectedFile1.Click += new System.EventHandler(this.btnSelectedFile1_Click);
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.btnXNYVConfig);
            this.gb2.Controls.Add(this.cbSheets2);
            this.gb2.Controls.Add(this.label3);
            this.gb2.Controls.Add(this.lblFileName2);
            this.gb2.Controls.Add(this.label8);
            this.gb2.Controls.Add(this.btnSelectedFile2);
            this.gb2.Location = new System.Drawing.Point(410, 4);
            this.gb2.Name = "gb2";
            this.gb2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb2.Size = new System.Drawing.Size(400, 91);
            this.gb2.TabIndex = 139;
            this.gb2.TabStop = false;
            this.gb2.Text = "File nguồn";
            // 
            // btnXNYVConfig
            // 
            this.btnXNYVConfig.BackgroundImage = global::Julian.Properties.Resources.settings;
            this.btnXNYVConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnXNYVConfig.Location = new System.Drawing.Point(337, 59);
            this.btnXNYVConfig.Name = "btnXNYVConfig";
            this.btnXNYVConfig.Size = new System.Drawing.Size(22, 22);
            this.btnXNYVConfig.TabIndex = 119;
            this.btnXNYVConfig.UseVisualStyleBackColor = true;
            this.btnXNYVConfig.Click += new System.EventHandler(this.btnXNYVConfig_Click);
            // 
            // cbSheets2
            // 
            this.cbSheets2.FormattingEnabled = true;
            this.cbSheets2.Location = new System.Drawing.Point(6, 61);
            this.cbSheets2.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets2.Name = "cbSheets2";
            this.cbSheets2.Size = new System.Drawing.Size(100, 21);
            this.cbSheets2.TabIndex = 118;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 163);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 117;
            // 
            // lblFileName2
            // 
            this.lblFileName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileName2.AutoEllipsis = true;
            this.lblFileName2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName2.Location = new System.Drawing.Point(7, 16);
            this.lblFileName2.Name = "lblFileName2";
            this.lblFileName2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName2.Size = new System.Drawing.Size(352, 24);
            this.lblFileName2.TabIndex = 115;
            this.lblFileName2.Text = "...";
            this.lblFileName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 45);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 117;
            this.label8.Text = "Sheet";
            // 
            // btnSelectedFile2
            // 
            this.btnSelectedFile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectedFile2.AutoEllipsis = true;
            this.btnSelectedFile2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectedFile2.BackgroundImage")));
            this.btnSelectedFile2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectedFile2.FlatAppearance.BorderSize = 0;
            this.btnSelectedFile2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectedFile2.Location = new System.Drawing.Point(366, 15);
            this.btnSelectedFile2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectedFile2.Name = "btnSelectedFile2";
            this.btnSelectedFile2.Size = new System.Drawing.Size(26, 26);
            this.btnSelectedFile2.TabIndex = 114;
            this.btnSelectedFile2.UseVisualStyleBackColor = true;
            this.btnSelectedFile2.Click += new System.EventHandler(this.btnSelectedFile2_Click);
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
            this.Column5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(4, 98);
            this.dgvData.Margin = new System.Windows.Forms.Padding(0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 30;
            this.dgvData.Size = new System.Drawing.Size(806, 287);
            this.dgvData.TabIndex = 154;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "PONhuom";
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "PO Nhuộm";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "KetQua";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.FillWeight = 18F;
            this.Column2.HeaderText = "Kết quả";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ChiTiet";
            this.Column3.FillWeight = 18F;
            this.Column3.HeaderText = "Chi tiết";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ETD";
            this.Column4.FillWeight = 18F;
            this.Column4.HeaderText = "ETD";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "GhiChuGiaoHang";
            this.Column5.FillWeight = 26F;
            this.Column5.HeaderText = "Ghi chú giao hàng";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 133;
            this.label2.Text = "Số dòng:";
            // 
            // lblTotalRow
            // 
            this.lblTotalRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalRow.AutoSize = true;
            this.lblTotalRow.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRow.Location = new System.Drawing.Point(141, 393);
            this.lblTotalRow.Name = "lblTotalRow";
            this.lblTotalRow.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRow.TabIndex = 136;
            this.lblTotalRow.Text = "0";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(728, 388);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(82, 23);
            this.btnCopy.TabIndex = 155;
            this.btnCopy.Text = "Sao chép";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // frmHoiHang2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 415);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.gb2);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTotalRow);
            this.Controls.Add(this.lblTotalFound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Name = "frmHoiHang2";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Hối hàng 2";
            this.Load += new System.EventHandler(this.frmHoiHang2_Load);
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.gb2.ResumeLayout(false);
            this.gb2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTotalFound;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.TextBox txtPoNhuom1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSheets1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFileName1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectedFile1;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.ComboBox cbSheets2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFileName2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSelectedFile2;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalRow;
        private System.Windows.Forms.Button btnXNYVConfig;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}