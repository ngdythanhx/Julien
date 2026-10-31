namespace SHC
{
    partial class frmLstGuiDiem
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCreate = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbCus = new System.Windows.Forms.ComboBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtFilename = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(6, 6);
            this.txtInput.MaxLength = 32767000;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1272, 96);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(345, 533);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
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
            this.Column2,
            this.Column4,
            this.Column6,
            this.Column3,
            this.Column1,
            this.Column7,
            this.Column12});
            this.dgvMain.Location = new System.Drawing.Point(6, 108);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1272, 418);
            this.dgvMain.TabIndex = 298;
            this.dgvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentClick);
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "ID";
            this.Column19.HeaderText = "ID";
            this.Column19.Name = "Column19";
            this.Column19.Width = 60;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Lieu";
            this.Column2.HeaderText = "Liệu";
            this.Column2.Name = "Column2";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Mau";
            this.Column4.HeaderText = "Màu";
            this.Column4.Name = "Column4";
            this.Column4.Width = 90;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MauThe";
            this.Column6.HeaderText = "Màu thế";
            this.Column6.Name = "Column6";
            this.Column6.Width = 90;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Article";
            this.Column3.HeaderText = "Article";
            this.Column3.Name = "Column3";
            this.Column3.Width = 90;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "PO";
            this.Column1.HeaderText = "PO";
            this.Column1.Name = "Column1";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "PONhuom";
            this.Column7.HeaderText = "PO Nhuộm";
            this.Column7.Name = "Column7";
            this.Column7.Width = 120;
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
            // cbCus
            // 
            this.cbCus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCus.FormattingEnabled = true;
            this.cbCus.Location = new System.Drawing.Point(6, 534);
            this.cbCus.Name = "cbCus";
            this.cbCus.Size = new System.Drawing.Size(81, 21);
            this.cbCus.TabIndex = 314;
            this.cbCus.SelectedIndexChanged += new System.EventHandler(this.cbCus_SelectedIndexChanged);
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpCreateDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(97, 535);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(98, 20);
            this.dtpCreateDate.TabIndex = 313;
            this.dtpCreateDate.ValueChanged += new System.EventHandler(this.dtpCreateDate_ValueChanged);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(201, 535);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 312;
            this.txtFilename.TextChanged += new System.EventHandler(this.txtFilename_TextChanged);
            // 
            // frmLstGuiDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.cbCus);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtInput);
            this.Name = "frmLstGuiDiem";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmLstGuiDiem";
            this.Load += new System.EventHandler(this.frmLstGuiDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.ComboBox cbCus;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.TextBox txtFilename;
    }
}