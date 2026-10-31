namespace JH
{
    partial class frmSQ
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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column19,
            this.Column5,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column2,
            this.Column4,
            this.Column12,
            this.Column6,
            this.Column1,
            this.Column11,
            this.Column3,
            this.Column7,
            this.column13,
            this.Column14,
            this.Column15});
            this.dgvMain.Location = new System.Drawing.Point(6, 107);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1272, 422);
            this.dgvMain.TabIndex = 317;
            this.dgvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentClick);
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "OrderDate";
            this.Column19.HeaderText = "Ngày đặt hàng";
            this.Column19.Name = "Column19";
            this.Column19.Width = 110;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Brand";
            this.Column5.HeaderText = "Brand";
            this.Column5.Name = "Column5";
            this.Column5.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CusName";
            this.Column8.HeaderText = "Mã KH";
            this.Column8.Name = "Column8";
            this.Column8.Width = 70;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "POOLD";
            this.Column9.HeaderText = "PO cũ";
            this.Column9.Name = "Column9";
            this.Column9.Width = 120;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PONEW";
            this.Column10.HeaderText = "PO mới";
            this.Column10.Name = "Column10";
            this.Column10.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Material";
            this.Column2.HeaderText = "Liệu";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Color";
            this.Column4.HeaderText = "Màu";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "#,##0.000";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column12.HeaderText = "Qty";
            this.Column12.Name = "Column12";
            this.Column12.Width = 80;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Unit";
            this.Column6.HeaderText = "Unit";
            this.Column6.Name = "Column6";
            this.Column6.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ETD";
            this.Column1.HeaderText = "Ngày giao hàng";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Season";
            this.Column11.HeaderText = "Season";
            this.Column11.Name = "Column11";
            this.Column11.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Art";
            this.Column3.HeaderText = "Article";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Follow";
            this.Column7.HeaderText = "Follow";
            this.Column7.Name = "Column7";
            this.Column7.Width = 120;
            // 
            // column13
            // 
            this.column13.DataPropertyName = "PurchaseSpecification";
            this.column13.HeaderText = "Tiêu chuẩn mua hàng";
            this.column13.Name = "column13";
            this.column13.Width = 180;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "PurchaserName";
            this.Column14.HeaderText = "Tên KD";
            this.Column14.Name = "Column14";
            this.Column14.Width = 80;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "Width";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column15.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column15.HeaderText = "Khổ";
            this.Column15.Name = "Column15";
            this.Column15.Width = 60;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(150, 533);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 316;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(6, 5);
            this.txtInput.MaxLength = 32767000;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1272, 96);
            this.txtInput.TabIndex = 315;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 538);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 321;
            this.label1.Text = "Số dòng:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Location = new System.Drawing.Point(305, 538);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRows.TabIndex = 321;
            this.lblTotalRows.Text = "0";
            this.lblTotalRows.Click += new System.EventHandler(this.lblTotalRows_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 538);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 321;
            this.label2.Text = "Số lượng:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(433, 538);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(13, 13);
            this.lblTotalQty.TabIndex = 321;
            this.lblTotalQty.Text = "0";
            this.lblTotalQty.Click += new System.EventHandler(this.lblTotalQty_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(6, 535);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 318;
            this.txtFilename.TextChanged += new System.EventHandler(this.txtFilename_TextChanged);
            // 
            // frmSQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtInput);
            this.Name = "frmSQ";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmSQ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.TextBox txtFilename;
    }
}