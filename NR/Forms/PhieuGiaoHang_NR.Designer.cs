namespace NR.Forms
{
    partial class PhieuGiaoHang_NR
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.dgvMain.Location = new System.Drawing.Point(6, 112);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1072, 367);
            this.dgvMain.TabIndex = 326;
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtInput);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1072, 100);
            this.groupBox1.TabIndex = 327;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập dữ liệu List gửi Diễm";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 16);
            this.txtInput.MaxLength = 32767000;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(1066, 81);
            this.txtInput.TabIndex = 326;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpCreateDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(6, 485);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(98, 20);
            this.dtpCreateDate.TabIndex = 330;
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFilename.Location = new System.Drawing.Point(110, 485);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(138, 20);
            this.txtFilename.TabIndex = 329;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(254, 484);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 328;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // PhieuGiaoHang_NR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 511);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvMain);
            this.Name = "PhieuGiaoHang_NR";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "PhieuGiaoHang_NR";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}