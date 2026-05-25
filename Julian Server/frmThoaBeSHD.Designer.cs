namespace Julian_Server
{
    partial class frmThoaBeSHD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThoaBeSHD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblFileName_OrderForm = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelectFile_OrderForm = new System.Windows.Forms.Button();
            this.gbPO = new System.Windows.Forms.GroupBox();
            this.txtInput_PO = new System.Windows.Forms.TextBox();
            this.chkView = new System.Windows.Forms.CheckBox();
            this.cbSheet = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectSheet = new System.Windows.Forms.Button();
            this.gbCode = new System.Windows.Forms.GroupBox();
            this.txtInput_Code = new System.Windows.Forms.TextBox();
            this.gbQty = new System.Windows.Forms.GroupBox();
            this.txtInput_Qty = new System.Windows.Forms.TextBox();
            this.gbDeliveryDate = new System.Windows.Forms.GroupBox();
            this.txtInput_DeliveryDate = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtStartIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfig_PO = new System.Windows.Forms.TextBox();
            this.lblPO = new System.Windows.Forms.Label();
            this.txtConfig_Code = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txtConfig_DeliveryNote = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfig_PODate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFilteredRows = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.col_podate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_po = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_deliverydate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfig_Qty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbPO.SuspendLayout();
            this.gbCode.SuspendLayout();
            this.gbQty.SuspendLayout();
            this.gbDeliveryDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileName_OrderForm
            // 
            this.lblFileName_OrderForm.AutoEllipsis = true;
            this.lblFileName_OrderForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_OrderForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_OrderForm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName_OrderForm.Location = new System.Drawing.Point(9, 30);
            this.lblFileName_OrderForm.Name = "lblFileName_OrderForm";
            this.lblFileName_OrderForm.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName_OrderForm.Size = new System.Drawing.Size(265, 22);
            this.lblFileName_OrderForm.TabIndex = 152;
            this.lblFileName_OrderForm.Text = "...";
            this.lblFileName_OrderForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 17);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 13);
            this.label9.TabIndex = 153;
            this.label9.Text = "OrderForm (đóng file trước khi chọn)";
            // 
            // btnSelectFile_OrderForm
            // 
            this.btnSelectFile_OrderForm.AutoEllipsis = true;
            this.btnSelectFile_OrderForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_OrderForm.BackgroundImage")));
            this.btnSelectFile_OrderForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_OrderForm.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_OrderForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_OrderForm.Location = new System.Drawing.Point(253, 6);
            this.btnSelectFile_OrderForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile_OrderForm.Name = "btnSelectFile_OrderForm";
            this.btnSelectFile_OrderForm.Size = new System.Drawing.Size(20, 20);
            this.btnSelectFile_OrderForm.TabIndex = 151;
            this.btnSelectFile_OrderForm.UseVisualStyleBackColor = true;
            this.btnSelectFile_OrderForm.Click += new System.EventHandler(this.btnSelectFile_OrderForm_Click);
            // 
            // gbPO
            // 
            this.gbPO.Controls.Add(this.txtInput_PO);
            this.gbPO.Location = new System.Drawing.Point(280, 6);
            this.gbPO.Name = "gbPO";
            this.gbPO.Size = new System.Drawing.Size(151, 72);
            this.gbPO.TabIndex = 154;
            this.gbPO.TabStop = false;
            this.gbPO.Text = "PO";
            // 
            // txtInput_PO
            // 
            this.txtInput_PO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput_PO.Location = new System.Drawing.Point(3, 16);
            this.txtInput_PO.Multiline = true;
            this.txtInput_PO.Name = "txtInput_PO";
            this.txtInput_PO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput_PO.Size = new System.Drawing.Size(145, 53);
            this.txtInput_PO.TabIndex = 0;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Location = new System.Drawing.Point(167, 59);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(49, 17);
            this.chkView.TabIndex = 155;
            this.chkView.Text = "View";
            this.chkView.UseVisualStyleBackColor = true;
            // 
            // cbSheet
            // 
            this.cbSheet.FormattingEnabled = true;
            this.cbSheet.Location = new System.Drawing.Point(51, 55);
            this.cbSheet.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cbSheet.Name = "cbSheet";
            this.cbSheet.Size = new System.Drawing.Size(108, 21);
            this.cbSheet.TabIndex = 157;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 60);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 156;
            this.label8.Text = "Sheet";
            // 
            // btnSelectSheet
            // 
            this.btnSelectSheet.Location = new System.Drawing.Point(222, 55);
            this.btnSelectSheet.Name = "btnSelectSheet";
            this.btnSelectSheet.Size = new System.Drawing.Size(52, 23);
            this.btnSelectSheet.TabIndex = 158;
            this.btnSelectSheet.Text = "Chọn";
            this.btnSelectSheet.UseVisualStyleBackColor = true;
            this.btnSelectSheet.Click += new System.EventHandler(this.btnSelectSheet_Click);
            // 
            // gbCode
            // 
            this.gbCode.Controls.Add(this.txtInput_Code);
            this.gbCode.Location = new System.Drawing.Point(437, 6);
            this.gbCode.Name = "gbCode";
            this.gbCode.Size = new System.Drawing.Size(151, 72);
            this.gbCode.TabIndex = 154;
            this.gbCode.TabStop = false;
            this.gbCode.Text = "Code";
            // 
            // txtInput_Code
            // 
            this.txtInput_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput_Code.Location = new System.Drawing.Point(3, 16);
            this.txtInput_Code.Multiline = true;
            this.txtInput_Code.Name = "txtInput_Code";
            this.txtInput_Code.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput_Code.Size = new System.Drawing.Size(145, 53);
            this.txtInput_Code.TabIndex = 0;
            // 
            // gbQty
            // 
            this.gbQty.Controls.Add(this.txtInput_Qty);
            this.gbQty.Location = new System.Drawing.Point(594, 6);
            this.gbQty.Name = "gbQty";
            this.gbQty.Size = new System.Drawing.Size(151, 72);
            this.gbQty.TabIndex = 154;
            this.gbQty.TabStop = false;
            this.gbQty.Text = "Qty";
            // 
            // txtInput_Qty
            // 
            this.txtInput_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput_Qty.Location = new System.Drawing.Point(3, 16);
            this.txtInput_Qty.Multiline = true;
            this.txtInput_Qty.Name = "txtInput_Qty";
            this.txtInput_Qty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput_Qty.Size = new System.Drawing.Size(145, 53);
            this.txtInput_Qty.TabIndex = 0;
            // 
            // gbDeliveryDate
            // 
            this.gbDeliveryDate.Controls.Add(this.txtInput_DeliveryDate);
            this.gbDeliveryDate.Location = new System.Drawing.Point(751, 6);
            this.gbDeliveryDate.Name = "gbDeliveryDate";
            this.gbDeliveryDate.Size = new System.Drawing.Size(151, 72);
            this.gbDeliveryDate.TabIndex = 154;
            this.gbDeliveryDate.TabStop = false;
            this.gbDeliveryDate.Text = "Ngày giao yêu cầu";
            // 
            // txtInput_DeliveryDate
            // 
            this.txtInput_DeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput_DeliveryDate.Location = new System.Drawing.Point(3, 16);
            this.txtInput_DeliveryDate.Multiline = true;
            this.txtInput_DeliveryDate.Name = "txtInput_DeliveryDate";
            this.txtInput_DeliveryDate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput_DeliveryDate.Size = new System.Drawing.Size(145, 53);
            this.txtInput_DeliveryDate.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_podate,
            this.col_po,
            this.col_code,
            this.col_qty,
            this.col_deliverydate});
            this.dataGridView1.Location = new System.Drawing.Point(6, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(896, 412);
            this.dataGridView1.TabIndex = 159;
            // 
            // txtStartIndex
            // 
            this.txtStartIndex.Location = new System.Drawing.Point(71, 91);
            this.txtStartIndex.Name = "txtStartIndex";
            this.txtStartIndex.Size = new System.Drawing.Size(40, 20);
            this.txtStartIndex.TabIndex = 160;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 161;
            this.label1.Text = "StartIndex";
            // 
            // txtConfig_PO
            // 
            this.txtConfig_PO.Location = new System.Drawing.Point(304, 91);
            this.txtConfig_PO.Name = "txtConfig_PO";
            this.txtConfig_PO.Size = new System.Drawing.Size(40, 20);
            this.txtConfig_PO.TabIndex = 160;
            // 
            // lblPO
            // 
            this.lblPO.AutoSize = true;
            this.lblPO.Location = new System.Drawing.Point(276, 94);
            this.lblPO.Name = "lblPO";
            this.lblPO.Size = new System.Drawing.Size(22, 13);
            this.lblPO.TabIndex = 161;
            this.lblPO.Text = "PO";
            // 
            // txtConfig_Code
            // 
            this.txtConfig_Code.Location = new System.Drawing.Point(391, 91);
            this.txtConfig_Code.Name = "txtConfig_Code";
            this.txtConfig_Code.Size = new System.Drawing.Size(40, 20);
            this.txtConfig_Code.TabIndex = 160;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(353, 94);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(32, 13);
            this.label99.TabIndex = 161;
            this.label99.Text = "Code";
            // 
            // txtConfig_DeliveryNote
            // 
            this.txtConfig_DeliveryNote.Location = new System.Drawing.Point(678, 91);
            this.txtConfig_DeliveryNote.Name = "txtConfig_DeliveryNote";
            this.txtConfig_DeliveryNote.Size = new System.Drawing.Size(40, 20);
            this.txtConfig_DeliveryNote.TabIndex = 160;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(579, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 161;
            this.label4.Text = "Ghi chú ngày giao";
            // 
            // txtConfig_PODate
            // 
            this.txtConfig_PODate.Location = new System.Drawing.Point(211, 91);
            this.txtConfig_PODate.Name = "txtConfig_PODate";
            this.txtConfig_PODate.Size = new System.Drawing.Size(40, 20);
            this.txtConfig_PODate.TabIndex = 160;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 161;
            this.label5.Text = "Ngày đặt hàng";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(774, 91);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 22);
            this.btnEdit.TabIndex = 162;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(827, 91);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 22);
            this.btnApply.TabIndex = 162;
            this.btnApply.Text = "Áp dụng";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 541);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 163;
            this.label2.Text = "Tổng số dòng trong file:";
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Location = new System.Drawing.Point(510, 541);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(13, 13);
            this.lblTotalRows.TabIndex = 163;
            this.lblTotalRows.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(561, 541);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 163;
            this.label6.Text = "Số dòng đã lọc:";
            // 
            // lblFilteredRows
            // 
            this.lblFilteredRows.AutoSize = true;
            this.lblFilteredRows.Location = new System.Drawing.Point(650, 541);
            this.lblFilteredRows.Name = "lblFilteredRows";
            this.lblFilteredRows.Size = new System.Drawing.Size(13, 13);
            this.lblFilteredRows.TabIndex = 163;
            this.lblFilteredRows.Text = "0";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(827, 536);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.TabIndex = 162;
            this.btnSave.Text = "Lưu file";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // col_podate
            // 
            this.col_podate.HeaderText = "Ngày đặt hàng";
            this.col_podate.Name = "col_podate";
            this.col_podate.Width = 120;
            // 
            // col_po
            // 
            this.col_po.HeaderText = "PO";
            this.col_po.Name = "col_po";
            this.col_po.Width = 120;
            // 
            // col_code
            // 
            this.col_code.HeaderText = "Code";
            this.col_code.Name = "col_code";
            this.col_code.Width = 120;
            // 
            // col_qty
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "#,##0.00";
            this.col_qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_qty.HeaderText = "Số lượng đặt";
            this.col_qty.Name = "col_qty";
            // 
            // col_deliverydate
            // 
            dataGridViewCellStyle2.Format = "yyyy-MM-dd";
            this.col_deliverydate.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_deliverydate.HeaderText = "Ngày giao hàng yêu cầu";
            this.col_deliverydate.Name = "col_deliverydate";
            this.col_deliverydate.Width = 150;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(55, 541);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(324, 17);
            this.lblStatus.TabIndex = 165;
            this.lblStatus.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 541);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 166;
            this.label3.Text = "Status:";
            // 
            // txtConfig_Qty
            // 
            this.txtConfig_Qty.Location = new System.Drawing.Point(483, 91);
            this.txtConfig_Qty.Name = "txtConfig_Qty";
            this.txtConfig_Qty.Size = new System.Drawing.Size(40, 20);
            this.txtConfig_Qty.TabIndex = 160;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(445, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 161;
            this.label7.Text = "Code";
            // 
            // frmThoaBeSHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 561);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFilteredRows);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConfig_DeliveryNote);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label99);
            this.Controls.Add(this.txtConfig_Qty);
            this.Controls.Add(this.txtConfig_Code);
            this.Controls.Add(this.lblPO);
            this.Controls.Add(this.txtConfig_PO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtConfig_PODate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartIndex);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSelectSheet);
            this.Controls.Add(this.cbSheet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkView);
            this.Controls.Add(this.gbDeliveryDate);
            this.Controls.Add(this.gbQty);
            this.Controls.Add(this.gbCode);
            this.Controls.Add(this.gbPO);
            this.Controls.Add(this.lblFileName_OrderForm);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSelectFile_OrderForm);
            this.Name = "frmThoaBeSHD";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmThoaBeSHD";
            this.gbPO.ResumeLayout(false);
            this.gbPO.PerformLayout();
            this.gbCode.ResumeLayout(false);
            this.gbCode.PerformLayout();
            this.gbQty.ResumeLayout(false);
            this.gbQty.PerformLayout();
            this.gbDeliveryDate.ResumeLayout(false);
            this.gbDeliveryDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileName_OrderForm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSelectFile_OrderForm;
        private System.Windows.Forms.GroupBox gbPO;
        private System.Windows.Forms.TextBox txtInput_PO;
        private System.Windows.Forms.CheckBox chkView;
        private System.Windows.Forms.ComboBox cbSheet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSelectSheet;
        private System.Windows.Forms.GroupBox gbCode;
        private System.Windows.Forms.TextBox txtInput_Code;
        private System.Windows.Forms.GroupBox gbQty;
        private System.Windows.Forms.TextBox txtInput_Qty;
        private System.Windows.Forms.GroupBox gbDeliveryDate;
        private System.Windows.Forms.TextBox txtInput_DeliveryDate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtStartIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConfig_PO;
        private System.Windows.Forms.Label lblPO;
        private System.Windows.Forms.TextBox txtConfig_Code;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txtConfig_DeliveryNote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConfig_PODate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFilteredRows;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_podate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_po;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_deliverydate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfig_Qty;
        private System.Windows.Forms.Label label7;
    }
}