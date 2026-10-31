namespace TX
{
    partial class frmINV
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
            this.cbCus = new System.Windows.Forms.ComboBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.lblGTGT = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtExchangRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCus
            // 
            this.cbCus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCus.FormattingEnabled = true;
            this.cbCus.Location = new System.Drawing.Point(4, 536);
            this.cbCus.Name = "cbCus";
            this.cbCus.Size = new System.Drawing.Size(81, 21);
            this.cbCus.TabIndex = 311;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpCreateDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(95, 537);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(98, 20);
            this.dtpCreateDate.TabIndex = 310;
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(270, 537);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 309;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(571, 534);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(131, 23);
            this.btnCreate.TabIndex = 308;
            this.btnCreate.Text = "Tạo INV+PKL HĐ";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreateINVPKL_Click);
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGrandTotal.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrandTotal.Location = new System.Drawing.Point(822, 509);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(121, 15);
            this.lblGrandTotal.TabIndex = 298;
            this.lblGrandTotal.Text = "0";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGTGT
            // 
            this.lblGTGT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGTGT.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGTGT.Location = new System.Drawing.Point(641, 509);
            this.lblGTGT.Name = "lblGTGT";
            this.lblGTGT.Size = new System.Drawing.Size(129, 15);
            this.lblGTGT.TabIndex = 299;
            this.lblGTGT.Text = "0";
            this.lblGTGT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(394, 509);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(133, 15);
            this.lblTotalAmount.TabIndex = 300;
            this.lblTotalAmount.Text = "0";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalQty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(179, 509);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(121, 15);
            this.lblTotalQty.TabIndex = 301;
            this.lblTotalQty.Text = "0";
            this.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label9.Location = new System.Drawing.Point(776, 509);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 302;
            this.label9.Text = "Tổng:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label7.Location = new System.Drawing.Point(533, 509);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 303;
            this.label7.Text = "Thuế GTGT(8%):";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label5.Location = new System.Drawing.Point(306, 509);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 304;
            this.label5.Text = "Total Amount:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label3.Location = new System.Drawing.Point(110, 509);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 305;
            this.label3.Text = "Total Qty:";
            // 
            // lblRows
            // 
            this.lblRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRows.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRows.Location = new System.Drawing.Point(58, 509);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(46, 15);
            this.lblRows.TabIndex = 306;
            this.lblRows.Text = "0";
            this.lblRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label1.Location = new System.Drawing.Point(10, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 307;
            this.label1.Text = "Rows:";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column14,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            this.dgvMain.Location = new System.Drawing.Point(3, 122);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.Size = new System.Drawing.Size(1277, 374);
            this.dgvMain.TabIndex = 297;
            // 
            // txtExchangRate
            // 
            this.txtExchangRate.Location = new System.Drawing.Point(483, 537);
            this.txtExchangRate.Name = "txtExchangRate";
            this.txtExchangRate.Size = new System.Drawing.Size(71, 20);
            this.txtExchangRate.TabIndex = 319;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 318;
            this.label2.Text = "Tỷ giá";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 540);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 320;
            this.label4.Text = "Tên file";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtInput);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1276, 112);
            this.groupBox2.TabIndex = 314;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nhập dữ liệu list gửi diễm";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 16);
            this.txtInput.MaxLength = 3276700;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1270, 93);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput2_TextChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ID";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Lieu";
            this.Column2.HeaderText = "Liệu";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Width";
            this.Column14.HeaderText = "Khổ";
            this.Column14.Name = "Column14";
            this.Column14.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "MauKD";
            this.Column3.HeaderText = "Màu KD";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MauKho";
            this.Column4.HeaderText = "Màu Kho";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "NgayDatHang_String";
            this.Column5.HeaderText = "Ngày đặt hàng";
            this.Column5.Name = "Column5";
            this.Column5.Width = 110;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Brand";
            this.Column6.HeaderText = "Brand";
            this.Column6.Name = "Column6";
            this.Column6.Width = 70;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CusPO";
            this.Column7.HeaderText = "Mã KH";
            this.Column7.Name = "Column7";
            this.Column7.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CusPO";
            this.Column8.HeaderText = "Mã Đơn KH";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "CusItemCode";
            this.Column9.HeaderText = "Mã Liệu KH";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PoNhuom";
            this.Column10.HeaderText = "PO Nhuộm";
            this.Column10.Name = "Column10";
            this.Column10.Width = 120;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "#,##0.000";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column11.HeaderText = "Số lượng";
            this.Column11.Name = "Column11";
            this.Column11.Width = 90;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0.00";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column12.HeaderText = "Đơn giá (USD)";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Roll";
            this.Column13.HeaderText = "Số cây";
            this.Column13.Name = "Column13";
            this.Column13.Width = 250;
            // 
            // frmINV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExchangRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbCus);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.lblGTGT);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMain);
            this.Name = "frmINV";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "frmINV";
            this.Load += new System.EventHandler(this.frmINV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCus;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblGrandTotal;
        private System.Windows.Forms.Label lblGTGT;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtExchangRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    }
}