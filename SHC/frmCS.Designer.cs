namespace SHC
{
    partial class frmCS
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.txtPurchaseSpecification = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFollow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArticle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.ucFilter1 = new Julian_Server.ucFilter();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblConfirmed = new System.Windows.Forms.Label();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.gb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Location = new System.Drawing.Point(158, 4);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(1);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1122, 21);
            this.pnlSearch.TabIndex = 237;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column8,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvMain.Location = new System.Drawing.Point(160, 29);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.Size = new System.Drawing.Size(1118, 311);
            this.dgvMain.TabIndex = 236;
            this.dgvMain.VirtualMode = true;
            this.dgvMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
            this.dgvMain.SelectionChanged += new System.EventHandler(this.dgvMain_SelectionChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(160, 510);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 241;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(258, 510);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 241;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.Location = new System.Drawing.Point(354, 510);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 241;
            this.btnDel.Text = "Xóa";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // gb1
            // 
            this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb1.Controls.Add(this.button1);
            this.gb1.Controls.Add(this.textBox1);
            this.gb1.Controls.Add(this.txtPurchaseSpecification);
            this.gb1.Controls.Add(this.lblID);
            this.gb1.Controls.Add(this.label6);
            this.gb1.Controls.Add(this.txtLocation);
            this.gb1.Controls.Add(this.lblConfirmed);
            this.gb1.Controls.Add(this.label9);
            this.gb1.Controls.Add(this.txtColor);
            this.gb1.Controls.Add(this.label8);
            this.gb1.Controls.Add(this.label3);
            this.gb1.Controls.Add(this.txtNote);
            this.gb1.Controls.Add(this.txtMaterial);
            this.gb1.Controls.Add(this.label7);
            this.gb1.Controls.Add(this.label5);
            this.gb1.Controls.Add(this.label2);
            this.gb1.Controls.Add(this.txtFollow);
            this.gb1.Controls.Add(this.label4);
            this.gb1.Controls.Add(this.txtArticle);
            this.gb1.Controls.Add(this.label1);
            this.gb1.Location = new System.Drawing.Point(160, 346);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(1118, 152);
            this.gb1.TabIndex = 242;
            this.gb1.TabStop = false;
            // 
            // txtPurchaseSpecification
            // 
            this.txtPurchaseSpecification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPurchaseSpecification.Location = new System.Drawing.Point(369, 42);
            this.txtPurchaseSpecification.Multiline = true;
            this.txtPurchaseSpecification.Name = "txtPurchaseSpecification";
            this.txtPurchaseSpecification.Size = new System.Drawing.Size(210, 98);
            this.txtPurchaseSpecification.TabIndex = 258;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(81, 19);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(27, 13);
            this.lblID.TabIndex = 248;
            this.lblID.Text = "N/A";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 247;
            this.label6.Text = "ID";
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLocation.Location = new System.Drawing.Point(84, 120);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(200, 20);
            this.txtLocation.TabIndex = 251;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(613, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 249;
            this.label9.Text = "Xác nhận";
            // 
            // txtColor
            // 
            this.txtColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtColor.Location = new System.Drawing.Point(84, 94);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(200, 20);
            this.txtColor.TabIndex = 250;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 245;
            this.label8.Text = "Vị trí";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 241;
            this.label3.Text = "Màu";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNote.Location = new System.Drawing.Point(672, 42);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(210, 98);
            this.txtNote.TabIndex = 253;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMaterial.Location = new System.Drawing.Point(84, 68);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(200, 20);
            this.txtMaterial.TabIndex = 254;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(622, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 244;
            this.label7.Text = "Ghi chú";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(308, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 43);
            this.label5.TabIndex = 244;
            this.label5.Text = "Cân nhắc mua hàng";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 243;
            this.label2.Text = "Liệu";
            // 
            // txtFollow
            // 
            this.txtFollow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFollow.Location = new System.Drawing.Point(369, 16);
            this.txtFollow.Name = "txtFollow";
            this.txtFollow.Size = new System.Drawing.Size(210, 20);
            this.txtFollow.TabIndex = 255;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 242;
            this.label4.Text = "Follow";
            // 
            // txtArticle
            // 
            this.txtArticle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtArticle.Location = new System.Drawing.Point(84, 42);
            this.txtArticle.Name = "txtArticle";
            this.txtArticle.Size = new System.Drawing.Size(200, 20);
            this.txtArticle.TabIndex = 252;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 246;
            this.label1.Text = "Article";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadData.Location = new System.Drawing.Point(6, 510);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(148, 23);
            this.btnLoadData.TabIndex = 243;
            this.btnLoadData.Text = "Tải dữ liệu";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // ucFilter1
            // 
            this.ucFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ucFilter1.Location = new System.Drawing.Point(6, 4);
            this.ucFilter1.Name = "ucFilter1";
            this.ucFilter1.Size = new System.Drawing.Size(148, 505);
            this.ucFilter1.TabIndex = 244;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "ID";
            this.Column9.HeaderText = "ID";
            this.Column9.Name = "Column9";
            this.Column9.Width = 50;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Article";
            this.Column1.HeaderText = "Article";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Material";
            this.Column2.HeaderText = "Liệu";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Color";
            this.Column3.HeaderText = "Màu";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Follow";
            this.Column4.HeaderText = "Follow";
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "PurchaseSpecification";
            this.Column8.HeaderText = "Cân nhắc mua hàng";
            this.Column8.Name = "Column8";
            this.Column8.Width = 270;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Note";
            this.Column5.HeaderText = "Ghi chú";
            this.Column5.Name = "Column5";
            this.Column5.Width = 170;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "SwatchLocation";
            this.Column6.HeaderText = "Vị trí lưu";
            this.Column6.Name = "Column6";
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Confirmed";
            this.Column7.HeaderText = "Đã xác nhận";
            this.Column7.Name = "Column7";
            // 
            // lblConfirmed
            // 
            this.lblConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConfirmed.AutoSize = true;
            this.lblConfirmed.Location = new System.Drawing.Point(672, 19);
            this.lblConfirmed.Name = "lblConfirmed";
            this.lblConfirmed.Size = new System.Drawing.Size(27, 13);
            this.lblConfirmed.TabIndex = 249;
            this.lblConfirmed.Text = "N/A";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportToExcel.Location = new System.Drawing.Point(1154, 510);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(126, 23);
            this.btnExportToExcel.TabIndex = 245;
            this.btnExportToExcel.Text = "Xuất file Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(924, 19);
            this.textBox1.MaxLength = 3276700;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(154, 104);
            this.textBox1.TabIndex = 259;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(994, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 260;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 539);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.ucFilter1);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.dgvMain);
            this.Name = "frmCS";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmCS";
            this.Load += new System.EventHandler(this.frmCS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFollow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtArticle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadData;
        private Julian_Server.ucFilter ucFilter1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPurchaseSpecification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label lblConfirmed;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}