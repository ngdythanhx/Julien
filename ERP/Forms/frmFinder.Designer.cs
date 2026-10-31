namespace ERP.Forms
{
    partial class frmFinder
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
            this.chkCustomerOrderNumber = new System.Windows.Forms.CheckBox();
            this.txtCustomerOrderNumber = new System.Windows.Forms.TextBox();
            this.chkCustomerMaterialCode = new System.Windows.Forms.CheckBox();
            this.txtCustomerMaterialCode = new System.Windows.Forms.TextBox();
            this.chkCustomerQty = new System.Windows.Forms.CheckBox();
            this.txtCustomerQty = new System.Windows.Forms.TextBox();
            this.dgvFind = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFind)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // chkCustomerOrderNumber
            // 
            this.chkCustomerOrderNumber.AutoSize = true;
            this.chkCustomerOrderNumber.Location = new System.Drawing.Point(6, 81);
            this.chkCustomerOrderNumber.Name = "chkCustomerOrderNumber";
            this.chkCustomerOrderNumber.Size = new System.Drawing.Size(63, 17);
            this.chkCustomerOrderNumber.TabIndex = 0;
            this.chkCustomerOrderNumber.Text = "Mã đơn";
            this.chkCustomerOrderNumber.UseVisualStyleBackColor = true;
            this.chkCustomerOrderNumber.CheckedChanged += new System.EventHandler(this.chkCustomerOrderNumber_CheckedChanged);
            // 
            // txtCustomerOrderNumber
            // 
            this.txtCustomerOrderNumber.Enabled = false;
            this.txtCustomerOrderNumber.Location = new System.Drawing.Point(6, 101);
            this.txtCustomerOrderNumber.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtCustomerOrderNumber.Multiline = true;
            this.txtCustomerOrderNumber.Name = "txtCustomerOrderNumber";
            this.txtCustomerOrderNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomerOrderNumber.Size = new System.Drawing.Size(120, 40);
            this.txtCustomerOrderNumber.TabIndex = 1;
            this.txtCustomerOrderNumber.TextChanged += new System.EventHandler(this.txtCustomerOrderNumber_TextChanged);
            // 
            // chkCustomerMaterialCode
            // 
            this.chkCustomerMaterialCode.AutoSize = true;
            this.chkCustomerMaterialCode.Location = new System.Drawing.Point(132, 15);
            this.chkCustomerMaterialCode.Name = "chkCustomerMaterialCode";
            this.chkCustomerMaterialCode.Size = new System.Drawing.Size(94, 17);
            this.chkCustomerMaterialCode.TabIndex = 0;
            this.chkCustomerMaterialCode.Text = "Mã liệu (Code)";
            this.chkCustomerMaterialCode.UseVisualStyleBackColor = true;
            this.chkCustomerMaterialCode.CheckedChanged += new System.EventHandler(this.chkCustomerMaterialCode_CheckedChanged);
            // 
            // txtCustomerMaterialCode
            // 
            this.txtCustomerMaterialCode.Enabled = false;
            this.txtCustomerMaterialCode.Location = new System.Drawing.Point(132, 35);
            this.txtCustomerMaterialCode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtCustomerMaterialCode.Multiline = true;
            this.txtCustomerMaterialCode.Name = "txtCustomerMaterialCode";
            this.txtCustomerMaterialCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomerMaterialCode.Size = new System.Drawing.Size(120, 40);
            this.txtCustomerMaterialCode.TabIndex = 1;
            this.txtCustomerMaterialCode.TextChanged += new System.EventHandler(this.txtCustomerMaterialCode_TextChanged);
            // 
            // chkCustomerQty
            // 
            this.chkCustomerQty.AutoSize = true;
            this.chkCustomerQty.Location = new System.Drawing.Point(132, 81);
            this.chkCustomerQty.Name = "chkCustomerQty";
            this.chkCustomerQty.Size = new System.Drawing.Size(68, 17);
            this.chkCustomerQty.TabIndex = 0;
            this.chkCustomerQty.Text = "Số lượng";
            this.chkCustomerQty.UseVisualStyleBackColor = true;
            this.chkCustomerQty.CheckedChanged += new System.EventHandler(this.chkCustomerQty_CheckedChanged);
            // 
            // txtCustomerQty
            // 
            this.txtCustomerQty.Enabled = false;
            this.txtCustomerQty.Location = new System.Drawing.Point(132, 101);
            this.txtCustomerQty.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtCustomerQty.Multiline = true;
            this.txtCustomerQty.Name = "txtCustomerQty";
            this.txtCustomerQty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomerQty.Size = new System.Drawing.Size(120, 40);
            this.txtCustomerQty.TabIndex = 1;
            this.txtCustomerQty.TextChanged += new System.EventHandler(this.txtCustomerQty_TextChanged);
            // 
            // dgvFind
            // 
            this.dgvFind.AllowUserToAddRows = false;
            this.dgvFind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFind.Location = new System.Drawing.Point(6, 176);
            this.dgvFind.Name = "dgvFind";
            this.dgvFind.Size = new System.Drawing.Size(246, 382);
            this.dgvFind.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mã Khách Hàng (Chỉ nhập 1 dòng)";
            // 
            // txtCustCode
            // 
            this.txtCustCode.Location = new System.Drawing.Point(6, 55);
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(120, 20);
            this.txtCustCode.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtCustCode);
            this.groupBox1.Controls.Add(this.dgvFind);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCustomerOrderNumber);
            this.groupBox1.Controls.Add(this.txtCustomerQty);
            this.groupBox1.Controls.Add(this.chkCustomerQty);
            this.groupBox1.Controls.Add(this.chkCustomerOrderNumber);
            this.groupBox1.Controls.Add(this.txtCustomerMaterialCode);
            this.groupBox1.Controls.Add(this.chkCustomerMaterialCode);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 564);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập thông tin tìm kiếm";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 147);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(246, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvMain);
            this.groupBox2.Location = new System.Drawing.Point(270, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(816, 356);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả tìm kiếm";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(4, 55);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(808, 505);
            this.dgvMain.TabIndex = 300;
            // 
            // frmFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 576);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmFinder";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmFinder";
            this.Load += new System.EventHandler(this.frmFinder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFind)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCustomerOrderNumber;
        private System.Windows.Forms.TextBox txtCustomerOrderNumber;
        private System.Windows.Forms.CheckBox chkCustomerMaterialCode;
        private System.Windows.Forms.TextBox txtCustomerMaterialCode;
        private System.Windows.Forms.CheckBox chkCustomerQty;
        private System.Windows.Forms.TextBox txtCustomerQty;
        private System.Windows.Forms.DataGridView dgvFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvMain;
    }
}