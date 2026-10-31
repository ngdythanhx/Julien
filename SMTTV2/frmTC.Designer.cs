namespace SMTTV2
{
    partial class frmTC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.GroupBox();
            this.lblRemainingQty = new System.Windows.Forms.Label();
            this.lblUsedQty = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.lbl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 45);
            this.groupBox1.TabIndex = 240;
            this.groupBox1.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(389, 15);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 249;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 247;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 248;
            this.label2.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(79, 17);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(122, 20);
            this.txtUsername.TabIndex = 245;
            this.txtUsername.Text = "jenny@yeechain.com";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(283, 17);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 246;
            this.txtPassword.Text = "WUN2bioxMjM0";
            // 
            // lbl
            // 
            this.lbl.Controls.Add(this.lblRemainingQty);
            this.lbl.Controls.Add(this.lblUsedQty);
            this.lbl.Controls.Add(this.lblTotalQty);
            this.lbl.Controls.Add(this.label6);
            this.lbl.Controls.Add(this.label4);
            this.lbl.Controls.Add(this.label3);
            this.lbl.Location = new System.Drawing.Point(487, 4);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(793, 45);
            this.lbl.TabIndex = 241;
            this.lbl.TabStop = false;
            // 
            // lblRemainingQty
            // 
            this.lblRemainingQty.AutoSize = true;
            this.lblRemainingQty.Location = new System.Drawing.Point(454, 21);
            this.lblRemainingQty.Name = "lblRemainingQty";
            this.lblRemainingQty.Size = new System.Drawing.Size(13, 13);
            this.lblRemainingQty.TabIndex = 1;
            this.lblRemainingQty.Text = "0";
            // 
            // lblUsedQty
            // 
            this.lblUsedQty.AutoSize = true;
            this.lblUsedQty.Location = new System.Drawing.Point(292, 21);
            this.lblUsedQty.Name = "lblUsedQty";
            this.lblUsedQty.Size = new System.Drawing.Size(13, 13);
            this.lblUsedQty.TabIndex = 1;
            this.lblUsedQty.Text = "0";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Location = new System.Drawing.Point(111, 21);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(13, 13);
            this.lblTotalQty.TabIndex = 1;
            this.lblTotalQty.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Còn lại:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Đã sử dụng:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tổng số lượng:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlSearch);
            this.groupBox3.Controls.Add(this.dgvMain);
            this.groupBox3.Location = new System.Drawing.Point(4, 52);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1276, 505);
            this.groupBox3.TabIndex = 242;
            this.groupBox3.TabStop = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Location = new System.Drawing.Point(4, 17);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(1);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1268, 21);
            this.pnlSearch.TabIndex = 241;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeight = 30;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.Location = new System.Drawing.Point(4, 42);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.Size = new System.Drawing.Size(1266, 457);
            this.dgvMain.TabIndex = 240;
            this.dgvMain.VirtualMode = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TCReferenceNumber";
            this.Column1.HeaderText = "TC Reference Number";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ProductCategory";
            this.Column2.HeaderText = "Product Category";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ProductComposition";
            this.Column3.HeaderText = "Product Composition";
            this.Column3.Name = "Column3";
            this.Column3.Width = 180;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TotalQuantityString";
            this.Column4.HeaderText = "Total (Kgs)";
            this.Column4.Name = "Column4";
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "UsedQuantityString";
            this.Column5.HeaderText = "Used (Kgs)";
            this.Column5.Name = "Column5";
            this.Column5.Width = 130;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "AvailableQuantityString";
            this.Column6.HeaderText = "Remaining Quantity (Kgs)";
            this.Column6.Name = "Column6";
            this.Column6.Width = 180;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CertificationStandardsString";
            this.Column7.HeaderText = "Certification Type";
            this.Column7.Name = "Column7";
            this.Column7.Width = 220;
            // 
            // frmTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTC";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "frmTC";
            this.Load += new System.EventHandler(this.frmTC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.lbl.ResumeLayout(false);
            this.lbl.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lblRemainingQty;
        private System.Windows.Forms.Label lblUsedQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}