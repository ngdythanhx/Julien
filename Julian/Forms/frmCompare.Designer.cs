namespace  Julian.Forms
{
    partial class frmCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompare));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnStart = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chkFilterData = new System.Windows.Forms.CheckBox();
            this.txtFilterColumns = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.cbSheets2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFileName2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectFile2 = new System.Windows.Forms.Button();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.cbSheets1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFileName1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile1 = new System.Windows.Forms.Button();
            this.txtFilterString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.gb2.SuspendLayout();
            this.gb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(593, 82);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 26);
            this.btnStart.TabIndex = 117;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(682, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 15);
            this.label9.TabIndex = 119;
            this.label9.Text = "Số dòng tìm thấy: ";
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.Location = new System.Drawing.Point(792, 88);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(12, 15);
            this.lblRowsCount.TabIndex = 119;
            this.lblRowsCount.Text = "_";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvData, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 112);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1019, 393);
            this.tableLayoutPanel1.TabIndex = 120;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(3, 3);
            this.dgvData.Margin = new System.Windows.Forms.Padding(0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(1013, 387);
            this.dgvData.TabIndex = 119;
            this.dgvData.VirtualMode = true;
            // 
            // chkFilterData
            // 
            this.chkFilterData.AutoSize = true;
            this.chkFilterData.Checked = true;
            this.chkFilterData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterData.Location = new System.Drawing.Point(266, 87);
            this.chkFilterData.Name = "chkFilterData";
            this.chkFilterData.Size = new System.Drawing.Size(157, 19);
            this.chkFilterData.TabIndex = 121;
            this.chkFilterData.Text = "Hiển thị dữ liệu theo cột:";
            this.chkFilterData.UseVisualStyleBackColor = true;
            // 
            // txtFilterColumns
            // 
            this.txtFilterColumns.Location = new System.Drawing.Point(429, 83);
            this.txtFilterColumns.Name = "txtFilterColumns";
            this.txtFilterColumns.Size = new System.Drawing.Size(141, 23);
            this.txtFilterColumns.TabIndex = 122;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.gb2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gb1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1019, 67);
            this.tableLayoutPanel2.TabIndex = 123;
            // 
            // gb2
            // 
            this.gb2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb2.Controls.Add(this.cbSheets2);
            this.gb2.Controls.Add(this.label2);
            this.gb2.Controls.Add(this.lblFileName2);
            this.gb2.Controls.Add(this.label4);
            this.gb2.Controls.Add(this.btnSelectFile2);
            this.gb2.Location = new System.Drawing.Point(512, 3);
            this.gb2.Name = "gb2";
            this.gb2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb2.Size = new System.Drawing.Size(504, 61);
            this.gb2.TabIndex = 117;
            this.gb2.TabStop = false;
            this.gb2.Text = "File nhập";
            // 
            // cbSheets2
            // 
            this.cbSheets2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSheets2.FormattingEnabled = true;
            this.cbSheets2.Location = new System.Drawing.Point(396, 24);
            this.cbSheets2.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets2.Name = "cbSheets2";
            this.cbSheets2.Size = new System.Drawing.Size(99, 23);
            this.cbSheets2.TabIndex = 118;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 163);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 117;
            // 
            // lblFileName2
            // 
            this.lblFileName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileName2.AutoEllipsis = true;
            this.lblFileName2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName2.Location = new System.Drawing.Point(51, 24);
            this.lblFileName2.Name = "lblFileName2";
            this.lblFileName2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName2.Size = new System.Drawing.Size(297, 24);
            this.lblFileName2.TabIndex = 115;
            this.lblFileName2.Text = "...";
            this.lblFileName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 117;
            this.label4.Text = "Sheet";
            // 
            // btnSelectFile2
            // 
            this.btnSelectFile2.AutoEllipsis = true;
            this.btnSelectFile2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile2.BackgroundImage")));
            this.btnSelectFile2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile2.FlatAppearance.BorderSize = 0;
            this.btnSelectFile2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile2.Location = new System.Drawing.Point(18, 24);
            this.btnSelectFile2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile2.Name = "btnSelectFile2";
            this.btnSelectFile2.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFile2.TabIndex = 114;
            this.btnSelectFile2.UseVisualStyleBackColor = true;
            this.btnSelectFile2.Click += new System.EventHandler(this.btnSelectFile2_Click);
            // 
            // gb1
            // 
            this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb1.Controls.Add(this.cbSheets1);
            this.gb1.Controls.Add(this.label7);
            this.gb1.Controls.Add(this.lblFileName1);
            this.gb1.Controls.Add(this.label1);
            this.gb1.Controls.Add(this.btnSelectFile1);
            this.gb1.Location = new System.Drawing.Point(3, 3);
            this.gb1.Name = "gb1";
            this.gb1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb1.Size = new System.Drawing.Size(503, 61);
            this.gb1.TabIndex = 116;
            this.gb1.TabStop = false;
            this.gb1.Text = "File tổng";
            // 
            // cbSheets1
            // 
            this.cbSheets1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSheets1.FormattingEnabled = true;
            this.cbSheets1.Location = new System.Drawing.Point(395, 24);
            this.cbSheets1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheets1.Name = "cbSheets1";
            this.cbSheets1.Size = new System.Drawing.Size(99, 23);
            this.cbSheets1.TabIndex = 118;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 163);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
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
            this.lblFileName1.Location = new System.Drawing.Point(51, 24);
            this.lblFileName1.Name = "lblFileName1";
            this.lblFileName1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName1.Size = new System.Drawing.Size(296, 24);
            this.lblFileName1.TabIndex = 115;
            this.lblFileName1.Text = "...";
            this.lblFileName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 117;
            this.label1.Text = "Sheet";
            // 
            // btnSelectFile1
            // 
            this.btnSelectFile1.AutoEllipsis = true;
            this.btnSelectFile1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile1.BackgroundImage")));
            this.btnSelectFile1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile1.FlatAppearance.BorderSize = 0;
            this.btnSelectFile1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile1.Location = new System.Drawing.Point(18, 24);
            this.btnSelectFile1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile1.Name = "btnSelectFile1";
            this.btnSelectFile1.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFile1.TabIndex = 114;
            this.btnSelectFile1.UseVisualStyleBackColor = true;
            this.btnSelectFile1.Click += new System.EventHandler(this.btnSelectFile1_Click);
            // 
            // txtFilterString
            // 
            this.txtFilterString.Location = new System.Drawing.Point(96, 83);
            this.txtFilterString.Name = "txtFilterString";
            this.txtFilterString.Size = new System.Drawing.Size(144, 23);
            this.txtFilterString.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 124;
            this.label5.Text = "Dữ liệu lọc:";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(846, 82);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(79, 23);
            this.btnCopy.TabIndex = 125;
            this.btnCopy.Text = "Sao chép";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // frmCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1031, 511);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.txtFilterColumns);
            this.Controls.Add(this.chkFilterData);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblRowsCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFilterString);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmCompare";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "So sánh dữ liệu";
            this.Load += new System.EventHandler(this.frmCompare_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gb2.ResumeLayout(false);
            this.gb2.PerformLayout();
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chkFilterData;
        private System.Windows.Forms.TextBox txtFilterColumns;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtFilterString;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.ComboBox cbSheets2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFileName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectFile2;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.ComboBox cbSheets1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFileName1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFile1;
        private System.Windows.Forms.Button btnCopy;
    }
}