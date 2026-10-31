namespace NR
{
    partial class frmPhieuGiaoHang
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtInput = new System.Windows.Forms.TextBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbCus = new System.Windows.Forms.ComboBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(6, 5);
            this.txtInput.MaxLength = 32767000;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1272, 96);
            this.txtInput.TabIndex = 322;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
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
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column1,
            this.Column2});
            this.dgvMain.Location = new System.Drawing.Point(6, 107);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1272, 422);
            this.dgvMain.TabIndex = 324;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "ID";
            this.Column16.HeaderText = "ID";
            this.Column16.Name = "Column16";
            this.Column16.Width = 50;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "PO";
            this.Column17.HeaderText = "PO";
            this.Column17.Name = "Column17";
            this.Column17.Width = 120;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "MaterialCode";
            this.Column18.HeaderText = "Code";
            this.Column18.Name = "Column18";
            this.Column18.Width = 120;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "Description";
            this.Column20.HeaderText = "Description";
            this.Column20.Name = "Column20";
            this.Column20.Width = 150;
            // 
            // Column21
            // 
            this.Column21.DataPropertyName = "Color";
            this.Column21.HeaderText = "Color";
            this.Column21.Name = "Column21";
            // 
            // Column22
            // 
            this.Column22.DataPropertyName = "Width";
            this.Column22.HeaderText = "Width";
            this.Column22.Name = "Column22";
            this.Column22.Width = 60;
            // 
            // Column23
            // 
            this.Column23.DataPropertyName = "Qty";
            this.Column23.HeaderText = "Qty";
            this.Column23.Name = "Column23";
            this.Column23.Width = 80;
            // 
            // Column24
            // 
            this.Column24.DataPropertyName = "Unit";
            this.Column24.HeaderText = "Unit";
            this.Column24.Name = "Column24";
            this.Column24.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "RollQty";
            this.Column1.HeaderText = "Roll Qty";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Roll";
            this.Column2.HeaderText = "Roll";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // cbCus
            // 
            this.cbCus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCus.FormattingEnabled = true;
            this.cbCus.Location = new System.Drawing.Point(6, 535);
            this.cbCus.Name = "cbCus";
            this.cbCus.Size = new System.Drawing.Size(81, 21);
            this.cbCus.TabIndex = 328;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpCreateDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(97, 536);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(98, 20);
            this.dtpCreateDate.TabIndex = 327;
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(201, 536);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 326;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(345, 534);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 325;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmPhieuGiaoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.cbCus);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.dgvMain);
            this.Name = "frmPhieuGiaoHang";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmPhieuGiaoHang";
            this.Load += new System.EventHandler(this.frmPhieuGiaoHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ComboBox cbCus;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnCreate;
    }
}