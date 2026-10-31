namespace JCalc
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSMTTData = new System.Windows.Forms.TextBox();
            this.txtXuatKho = new System.Windows.Forms.TextBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalRemainQty = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStart_TinhSlConLai = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblTotalSMTT = new System.Windows.Forms.Label();
            this.lblTotalXuatKho = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(280, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "SMTT Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Xuất Kho";
            // 
            // txtSMTTData
            // 
            this.txtSMTTData.Location = new System.Drawing.Point(274, 21);
            this.txtSMTTData.MaxLength = 3276700;
            this.txtSMTTData.Multiline = true;
            this.txtSMTTData.Name = "txtSMTTData";
            this.txtSMTTData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSMTTData.Size = new System.Drawing.Size(259, 109);
            this.txtSMTTData.TabIndex = 1;
            // 
            // txtXuatKho
            // 
            this.txtXuatKho.Location = new System.Drawing.Point(6, 21);
            this.txtXuatKho.MaxLength = 3276700;
            this.txtXuatKho.Multiline = true;
            this.txtXuatKho.Name = "txtXuatKho";
            this.txtXuatKho.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXuatKho.Size = new System.Drawing.Size(259, 109);
            this.txtXuatKho.TabIndex = 0;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMain.Location = new System.Drawing.Point(6, 157);
            this.dgvMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 25;
            this.dgvMain.Size = new System.Drawing.Size(527, 256);
            this.dgvMain.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(354, 426);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Tổng SL Còn lại:";
            // 
            // lblTotalRemainQty
            // 
            this.lblTotalRemainQty.AutoSize = true;
            this.lblTotalRemainQty.Location = new System.Drawing.Point(446, 426);
            this.lblTotalRemainQty.Name = "lblTotalRemainQty";
            this.lblTotalRemainQty.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRemainQty.TabIndex = 9;
            this.lblTotalRemainQty.Text = "0";
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Location = new System.Drawing.Point(149, 426);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRows.TabIndex = 10;
            this.lblTotalRows.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 426);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Số dòng:";
            // 
            // btnStart_TinhSlConLai
            // 
            this.btnStart_TinhSlConLai.Location = new System.Drawing.Point(6, 421);
            this.btnStart_TinhSlConLai.Name = "btnStart_TinhSlConLai";
            this.btnStart_TinhSlConLai.Size = new System.Drawing.Size(75, 23);
            this.btnStart_TinhSlConLai.TabIndex = 2;
            this.btnStart_TinhSlConLai.Text = "Start";
            this.btnStart_TinhSlConLai.UseVisualStyleBackColor = true;
            this.btnStart_TinhSlConLai.Click += new System.EventHandler(this.btnStart_TinhSlConLai_Click);
            // 
            // lblTotalSMTT
            // 
            this.lblTotalSMTT.AutoSize = true;
            this.lblTotalSMTT.Location = new System.Drawing.Point(474, 133);
            this.lblTotalSMTT.Name = "lblTotalSMTT";
            this.lblTotalSMTT.Size = new System.Drawing.Size(13, 13);
            this.lblTotalSMTT.TabIndex = 12;
            this.lblTotalSMTT.Text = "0";
            // 
            // lblTotalXuatKho
            // 
            this.lblTotalXuatKho.AutoSize = true;
            this.lblTotalXuatKho.Location = new System.Drawing.Point(206, 133);
            this.lblTotalXuatKho.Name = "lblTotalXuatKho";
            this.lblTotalXuatKho.Size = new System.Drawing.Size(13, 13);
            this.lblTotalXuatKho.TabIndex = 13;
            this.lblTotalXuatKho.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(417, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Tổng SL:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Tổng SL:";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "PO";
            this.Column1.FillWeight = 267.4856F;
            this.Column1.HeaderText = "PO";
            this.Column1.Name = "Column1";
            this.Column1.Width = 180;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SLXuatKho";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0.000";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.FillWeight = 40.60913F;
            this.Column2.HeaderText = "SL Xuất";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SLSMTT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0.000";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.FillWeight = 45.95265F;
            this.Column3.HeaderText = "SL SMTT";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "SLChenhLech";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0.000";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.FillWeight = 45.95265F;
            this.Column4.HeaderText = "Chênh lệch";
            this.Column4.Name = "Column4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTotalSMTT);
            this.Controls.Add(this.lblTotalXuatKho);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblTotalRemainQty);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnStart_TinhSlConLai);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSMTTData);
            this.Controls.Add(this.txtXuatKho);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Tính số lượng chênh lệch";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSMTTData;
        private System.Windows.Forms.TextBox txtXuatKho;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalRemainQty;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnStart_TinhSlConLai;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblTotalSMTT;
        private System.Windows.Forms.Label lblTotalXuatKho;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button button1;
    }
}

