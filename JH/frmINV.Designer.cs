namespace JH
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
            this.btnCreateINVPKL = new System.Windows.Forms.Button();
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
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExchangRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
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
            // btnCreateINVPKL
            // 
            this.btnCreateINVPKL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateINVPKL.Location = new System.Drawing.Point(537, 535);
            this.btnCreateINVPKL.Name = "btnCreateINVPKL";
            this.btnCreateINVPKL.Size = new System.Drawing.Size(145, 23);
            this.btnCreateINVPKL.TabIndex = 308;
            this.btnCreateINVPKL.Text = "Tạo INV+PKL+DEBIT";
            this.btnCreateINVPKL.UseVisualStyleBackColor = true;
            this.btnCreateINVPKL.Click += new System.EventHandler(this.btnCreateINVPKL_Click);
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.BackColor = System.Drawing.Color.Yellow;
            this.lblGrandTotal.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrandTotal.Location = new System.Drawing.Point(822, 509);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(14, 14);
            this.lblGrandTotal.TabIndex = 298;
            this.lblGrandTotal.Text = "0";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGTGT
            // 
            this.lblGTGT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGTGT.AutoSize = true;
            this.lblGTGT.BackColor = System.Drawing.Color.Yellow;
            this.lblGTGT.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGTGT.Location = new System.Drawing.Point(641, 509);
            this.lblGTGT.Name = "lblGTGT";
            this.lblGTGT.Size = new System.Drawing.Size(14, 14);
            this.lblGTGT.TabIndex = 299;
            this.lblGTGT.Text = "0";
            this.lblGTGT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.BackColor = System.Drawing.Color.Yellow;
            this.lblTotalAmount.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(394, 509);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(14, 14);
            this.lblTotalAmount.TabIndex = 300;
            this.lblTotalAmount.Text = "0";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.BackColor = System.Drawing.Color.Yellow;
            this.lblTotalQty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(179, 509);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(14, 14);
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
            this.lblRows.AutoSize = true;
            this.lblRows.BackColor = System.Drawing.Color.Yellow;
            this.lblRows.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRows.Location = new System.Drawing.Point(58, 509);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(14, 14);
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
            this.Column19,
            this.Column1,
            this.Column4,
            this.Column8,
            this.Column9,
            this.Column11,
            this.Column2,
            this.Column12,
            this.Column13,
            this.Column18,
            this.Column16});
            this.dgvMain.Location = new System.Drawing.Point(3, 124);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1277, 372);
            this.dgvMain.TabIndex = 297;
            // 
            // txtInput2
            // 
            this.txtInput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput2.Location = new System.Drawing.Point(4, 4);
            this.txtInput2.MaxLength = 3276700;
            this.txtInput2.Multiline = true;
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput2.Size = new System.Drawing.Size(1276, 114);
            this.txtInput2.TabIndex = 312;
            this.txtInput2.TextChanged += new System.EventHandler(this.txtInput2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 540);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 324;
            this.label4.Text = "Tên file";
            // 
            // txtExchangRate
            // 
            this.txtExchangRate.Location = new System.Drawing.Point(460, 537);
            this.txtExchangRate.Name = "txtExchangRate";
            this.txtExchangRate.Size = new System.Drawing.Size(71, 20);
            this.txtExchangRate.TabIndex = 323;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(418, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 322;
            this.label2.Text = "Tỷ giá";
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(247, 537);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 321;
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "ID";
            this.Column19.HeaderText = "ID";
            this.Column19.Name = "Column19";
            this.Column19.Width = 40;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "PO";
            this.Column1.HeaderText = "PO";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MaterialCode";
            this.Column4.HeaderText = "Material Code";
            this.Column4.Name = "Column4";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Description";
            this.Column8.HeaderText = "Description";
            this.Column8.Name = "Column8";
            this.Column8.Width = 120;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Color";
            this.Column9.HeaderText = "Color";
            this.Column9.Name = "Column9";
            this.Column9.Width = 120;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Width";
            this.Column11.HeaderText = "Width";
            this.Column11.Name = "Column11";
            this.Column11.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Weight";
            this.Column2.HeaderText = "Weight";
            this.Column2.Name = "Column2";
            this.Column2.Width = 75;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "#,##0.000";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column12.HeaderText = "Qty";
            this.Column12.Name = "Column12";
            this.Column12.Width = 65;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Unit";
            this.Column13.HeaderText = "Unit";
            this.Column13.Name = "Column13";
            this.Column13.Width = 50;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0.00";
            this.Column18.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column18.HeaderText = "UnitPrice";
            this.Column18.Name = "Column18";
            this.Column18.Width = 75;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "DeliveryDate";
            this.Column16.HeaderText = "Delivery Date";
            this.Column16.Name = "Column16";
            // 
            // frmINV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExchangRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this.cbCus);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.btnCreateINVPKL);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCus;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.Button btnCreateINVPKL;
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
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExchangRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
    }
}