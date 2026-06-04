namespace Julian_Server
{
    partial class frmReporter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpSanLuong = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvHoiHang = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHoiHang_TotalRows = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnHoiHang_ThemTienDo = new System.Windows.Forms.Button();
            this.btnHoiHang_ExportReport = new System.Windows.Forms.Button();
            this.btnHoiHang_Apply = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpHoiHang_ToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpHoiHang_FromDate = new System.Windows.Forms.DateTimePicker();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tpDebitNote = new System.Windows.Forms.TabPage();
            this.tpVita = new System.Windows.Forms.TabPage();
            this.filterHoiHang_MaKH = new Julian_Server.ucFilter();
            this.col_makh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ngaydat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ponhuom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ponhuommoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_madonkh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mahangkh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_lieukh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_lieuthaythe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_kho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_maukh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mauthaythe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_motamau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sldat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dongia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tongtien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_etd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ngayxuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_invoicehoadon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_invoicepgh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFilter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoiHang)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_makh,
            this.col_ngaydat,
            this.col_brand,
            this.col_ponhuom,
            this.col_ponhuommoi,
            this.col_madonkh,
            this.col_mahangkh,
            this.col_lieukh,
            this.col_lieuthaythe,
            this.col_kho,
            this.col_maukh,
            this.col_mauthaythe,
            this.col_motamau,
            this.col_sldat,
            this.col_dongia,
            this.col_tongtien,
            this.col_etd,
            this.col_ngayxuat,
            this.col_invoicehoadon,
            this.col_invoicepgh,
            this.col_article});
            this.dgvMain.Location = new System.Drawing.Point(6, 29);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.Size = new System.Drawing.Size(1572, 343);
            this.dgvMain.TabIndex = 0;
            this.dgvMain.VirtualMode = true;
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpSanLuong);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage3);
            this.tcMain.Controls.Add(this.tpDebitNote);
            this.tcMain.Controls.Add(this.tpVita);
            this.tcMain.Location = new System.Drawing.Point(6, 378);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1572, 377);
            this.tcMain.TabIndex = 227;
            // 
            // tpSanLuong
            // 
            this.tpSanLuong.Location = new System.Drawing.Point(4, 22);
            this.tpSanLuong.Name = "tpSanLuong";
            this.tpSanLuong.Padding = new System.Windows.Forms.Padding(3);
            this.tpSanLuong.Size = new System.Drawing.Size(1564, 351);
            this.tpSanLuong.TabIndex = 1;
            this.tpSanLuong.Text = "Sản lượng";
            this.tpSanLuong.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filterHoiHang_MaKH);
            this.tabPage1.Controls.Add(this.dgvHoiHang);
            this.tabPage1.Controls.Add(this.lblHoiHang_TotalRows);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.btnHoiHang_ThemTienDo);
            this.tabPage1.Controls.Add(this.btnHoiHang_ExportReport);
            this.tabPage1.Controls.Add(this.btnHoiHang_Apply);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.dtpHoiHang_ToDate);
            this.tabPage1.Controls.Add(this.dtpHoiHang_FromDate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1564, 351);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Hối hàng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvHoiHang
            // 
            this.dgvHoiHang.AllowUserToAddRows = false;
            this.dgvHoiHang.AllowUserToDeleteRows = false;
            this.dgvHoiHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHoiHang.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvHoiHang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHoiHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvHoiHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn12});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHoiHang.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvHoiHang.GridColor = System.Drawing.SystemColors.Control;
            this.dgvHoiHang.Location = new System.Drawing.Point(119, 26);
            this.dgvHoiHang.Name = "dgvHoiHang";
            this.dgvHoiHang.ReadOnly = true;
            this.dgvHoiHang.RowHeadersWidth = 30;
            this.dgvHoiHang.Size = new System.Drawing.Size(1439, 241);
            this.dgvHoiHang.TabIndex = 256;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MaKH";
            this.dataGridViewTextBoxColumn1.HeaderText = "KH";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Brand";
            this.dataGridViewTextBoxColumn3.HeaderText = "Brand";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "PONhuom";
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn6.FillWeight = 24F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Mã Nhuộm Sd";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "LieuKH";
            this.dataGridViewTextBoxColumn7.FillWeight = 18F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Mã Liệu";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 90;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "MauKH";
            this.dataGridViewTextBoxColumn8.HeaderText = "Mã Màu";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Kho";
            this.dataGridViewTextBoxColumn9.HeaderText = "Khổ";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 50;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "SlDat";
            this.dataGridViewTextBoxColumn10.HeaderText = "Sl đặt";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "TienDo";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "#,##0.00";
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn12.HeaderText = "Tiến độ";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 250;
            // 
            // lblHoiHang_TotalRows
            // 
            this.lblHoiHang_TotalRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHoiHang_TotalRows.AutoSize = true;
            this.lblHoiHang_TotalRows.Location = new System.Drawing.Point(266, 273);
            this.lblHoiHang_TotalRows.Margin = new System.Windows.Forms.Padding(3);
            this.lblHoiHang_TotalRows.Name = "lblHoiHang_TotalRows";
            this.lblHoiHang_TotalRows.Size = new System.Drawing.Size(13, 13);
            this.lblHoiHang_TotalRows.TabIndex = 255;
            this.lblHoiHang_TotalRows.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(212, 273);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 254;
            this.label10.Text = "Số dòng:";
            // 
            // btnHoiHang_ThemTienDo
            // 
            this.btnHoiHang_ThemTienDo.Location = new System.Drawing.Point(119, 291);
            this.btnHoiHang_ThemTienDo.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnHoiHang_ThemTienDo.Name = "btnHoiHang_ThemTienDo";
            this.btnHoiHang_ThemTienDo.Size = new System.Drawing.Size(122, 23);
            this.btnHoiHang_ThemTienDo.TabIndex = 253;
            this.btnHoiHang_ThemTienDo.Text = "Thêm tiến độ";
            this.btnHoiHang_ThemTienDo.UseVisualStyleBackColor = true;
            // 
            // btnHoiHang_ExportReport
            // 
            this.btnHoiHang_ExportReport.Location = new System.Drawing.Point(119, 316);
            this.btnHoiHang_ExportReport.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnHoiHang_ExportReport.Name = "btnHoiHang_ExportReport";
            this.btnHoiHang_ExportReport.Size = new System.Drawing.Size(122, 23);
            this.btnHoiHang_ExportReport.TabIndex = 253;
            this.btnHoiHang_ExportReport.Text = "Xuất báo cáo Excel";
            this.btnHoiHang_ExportReport.UseVisualStyleBackColor = true;
            // 
            // btnHoiHang_Apply
            // 
            this.btnHoiHang_Apply.Location = new System.Drawing.Point(6, 316);
            this.btnHoiHang_Apply.Name = "btnHoiHang_Apply";
            this.btnHoiHang_Apply.Size = new System.Drawing.Size(98, 23);
            this.btnHoiHang_Apply.TabIndex = 251;
            this.btnHoiHang_Apply.Text = "Áp dụng";
            this.btnHoiHang_Apply.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 249;
            this.label6.Text = "Bảng báo cáo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 249;
            this.label1.Text = "Đến ngày";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 250;
            this.label5.Text = "Từ ngày";
            // 
            // dtpHoiHang_ToDate
            // 
            this.dtpHoiHang_ToDate.CustomFormat = "yyyy/MM/dd";
            this.dtpHoiHang_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoiHang_ToDate.Location = new System.Drawing.Point(6, 290);
            this.dtpHoiHang_ToDate.Name = "dtpHoiHang_ToDate";
            this.dtpHoiHang_ToDate.Size = new System.Drawing.Size(98, 20);
            this.dtpHoiHang_ToDate.TabIndex = 247;
            // 
            // dtpHoiHang_FromDate
            // 
            this.dtpHoiHang_FromDate.CustomFormat = "yyyy/MM/dd";
            this.dtpHoiHang_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoiHang_FromDate.Location = new System.Drawing.Point(6, 247);
            this.dtpHoiHang_FromDate.Name = "dtpHoiHang_FromDate";
            this.dtpHoiHang_FromDate.Size = new System.Drawing.Size(98, 20);
            this.dtpHoiHang_FromDate.TabIndex = 248;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1564, 351);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Báo cáo tuần";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tpDebitNote
            // 
            this.tpDebitNote.Location = new System.Drawing.Point(4, 22);
            this.tpDebitNote.Name = "tpDebitNote";
            this.tpDebitNote.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebitNote.Size = new System.Drawing.Size(1564, 351);
            this.tpDebitNote.TabIndex = 4;
            this.tpDebitNote.Text = "Công nợ";
            this.tpDebitNote.UseVisualStyleBackColor = true;
            // 
            // tpVita
            // 
            this.tpVita.Location = new System.Drawing.Point(4, 22);
            this.tpVita.Name = "tpVita";
            this.tpVita.Padding = new System.Windows.Forms.Padding(3);
            this.tpVita.Size = new System.Drawing.Size(1564, 351);
            this.tpVita.TabIndex = 5;
            this.tpVita.Text = "Vita";
            this.tpVita.UseVisualStyleBackColor = true;
            // 
            // filterHoiHang_MaKH
            // 
            this.filterHoiHang_MaKH.FilterText = "Mã Khách Hàng";
            this.filterHoiHang_MaKH.Location = new System.Drawing.Point(6, 6);
            this.filterHoiHang_MaKH.Name = "filterHoiHang_MaKH";
            this.filterHoiHang_MaKH.Size = new System.Drawing.Size(100, 222);
            this.filterHoiHang_MaKH.TabIndex = 261;
            // 
            // col_makh
            // 
            this.col_makh.DataPropertyName = "MaKH";
            this.col_makh.HeaderText = "Mã KH";
            this.col_makh.Name = "col_makh";
            this.col_makh.Width = 80;
            // 
            // col_ngaydat
            // 
            this.col_ngaydat.DataPropertyName = "NgayDat";
            dataGridViewCellStyle16.Format = "yyyy-MM-dd";
            this.col_ngaydat.DefaultCellStyle = dataGridViewCellStyle16;
            this.col_ngaydat.HeaderText = "Ngày đặt hàng";
            this.col_ngaydat.Name = "col_ngaydat";
            this.col_ngaydat.Width = 120;
            // 
            // col_brand
            // 
            this.col_brand.DataPropertyName = "Brand";
            this.col_brand.HeaderText = "Brand";
            this.col_brand.Name = "col_brand";
            this.col_brand.Width = 60;
            // 
            // col_ponhuom
            // 
            this.col_ponhuom.DataPropertyName = "PONhuom";
            this.col_ponhuom.HeaderText = "PO Nhuộm";
            this.col_ponhuom.Name = "col_ponhuom";
            this.col_ponhuom.Width = 120;
            // 
            // col_ponhuommoi
            // 
            this.col_ponhuommoi.DataPropertyName = "PONhuomMoi";
            this.col_ponhuommoi.HeaderText = "PO Nhuộm mới";
            this.col_ponhuommoi.Name = "col_ponhuommoi";
            this.col_ponhuommoi.Width = 120;
            // 
            // col_madonkh
            // 
            this.col_madonkh.DataPropertyName = "MaDonKH";
            this.col_madonkh.HeaderText = "Mã đơn KH";
            this.col_madonkh.Name = "col_madonkh";
            // 
            // col_mahangkh
            // 
            this.col_mahangkh.DataPropertyName = "MaHangKH";
            this.col_mahangkh.HeaderText = "Mã hàng KH";
            this.col_mahangkh.Name = "col_mahangkh";
            // 
            // col_lieukh
            // 
            this.col_lieukh.DataPropertyName = "LieuKH";
            this.col_lieukh.HeaderText = "Liệu KH";
            this.col_lieukh.Name = "col_lieukh";
            // 
            // col_lieuthaythe
            // 
            this.col_lieuthaythe.DataPropertyName = "LieuThayThe";
            this.col_lieuthaythe.HeaderText = "Liệu thay thế";
            this.col_lieuthaythe.Name = "col_lieuthaythe";
            // 
            // col_kho
            // 
            this.col_kho.DataPropertyName = "Kho";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_kho.DefaultCellStyle = dataGridViewCellStyle17;
            this.col_kho.HeaderText = "Khổ";
            this.col_kho.Name = "col_kho";
            this.col_kho.Width = 60;
            // 
            // col_maukh
            // 
            this.col_maukh.DataPropertyName = "MauKH";
            this.col_maukh.HeaderText = "Màu KH";
            this.col_maukh.Name = "col_maukh";
            // 
            // col_mauthaythe
            // 
            this.col_mauthaythe.DataPropertyName = "MauThayThe";
            this.col_mauthaythe.HeaderText = "Màu thay thế";
            this.col_mauthaythe.Name = "col_mauthaythe";
            // 
            // col_motamau
            // 
            this.col_motamau.DataPropertyName = "MoTaMau";
            this.col_motamau.HeaderText = "Mô tả màu";
            this.col_motamau.Name = "col_motamau";
            // 
            // col_sldat
            // 
            this.col_sldat.DataPropertyName = "SLDat";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "#,##0.00";
            this.col_sldat.DefaultCellStyle = dataGridViewCellStyle18;
            this.col_sldat.HeaderText = "SL đặt hàng";
            this.col_sldat.Name = "col_sldat";
            // 
            // col_dongia
            // 
            this.col_dongia.DataPropertyName = "DonGia";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "#,##0.00";
            this.col_dongia.DefaultCellStyle = dataGridViewCellStyle19;
            this.col_dongia.HeaderText = "Đơn giá";
            this.col_dongia.Name = "col_dongia";
            // 
            // col_tongtien
            // 
            this.col_tongtien.DataPropertyName = "TongTien";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "#,##0.00";
            this.col_tongtien.DefaultCellStyle = dataGridViewCellStyle20;
            this.col_tongtien.HeaderText = "Tổng tiền";
            this.col_tongtien.Name = "col_tongtien";
            // 
            // col_etd
            // 
            this.col_etd.DataPropertyName = "ETD";
            dataGridViewCellStyle21.Format = "yyyy-MM-dd";
            this.col_etd.DefaultCellStyle = dataGridViewCellStyle21;
            this.col_etd.HeaderText = "ETD";
            this.col_etd.Name = "col_etd";
            // 
            // col_ngayxuat
            // 
            this.col_ngayxuat.DataPropertyName = "NgayXuat";
            dataGridViewCellStyle22.Format = "yyyy-MM-dd";
            this.col_ngayxuat.DefaultCellStyle = dataGridViewCellStyle22;
            this.col_ngayxuat.HeaderText = "Ngày xuất hàng";
            this.col_ngayxuat.Name = "col_ngayxuat";
            this.col_ngayxuat.Width = 120;
            // 
            // col_invoicehoadon
            // 
            this.col_invoicehoadon.DataPropertyName = "InvoiceHoaDon";
            this.col_invoicehoadon.HeaderText = "Invoice Hóa đơn";
            this.col_invoicehoadon.Name = "col_invoicehoadon";
            this.col_invoicehoadon.Width = 130;
            // 
            // col_invoicepgh
            // 
            this.col_invoicepgh.DataPropertyName = "InvoicePHG";
            this.col_invoicepgh.HeaderText = "Invoice PHG";
            this.col_invoicepgh.Name = "col_invoicepgh";
            this.col_invoicepgh.Width = 130;
            // 
            // col_article
            // 
            this.col_article.DataPropertyName = "Article";
            this.col_article.HeaderText = "Article";
            this.col_article.Name = "col_article";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.Location = new System.Drawing.Point(4, 4);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(1);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1576, 21);
            this.pnlFilter.TabIndex = 228;
            // 
            // frmReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 761);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.dgvMain);
            this.Name = "frmReporter";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmReporter";
            this.Load += new System.EventHandler(this.frmReporter_Load);
            this.Shown += new System.EventHandler(this.frmReporter_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoiHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpSanLuong;
        private System.Windows.Forms.TabPage tabPage1;
        private ucFilter filterHoiHang_MaKH;
        private System.Windows.Forms.DataGridView dgvHoiHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Label lblHoiHang_TotalRows;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnHoiHang_ThemTienDo;
        private System.Windows.Forms.Button btnHoiHang_ExportReport;
        private System.Windows.Forms.Button btnHoiHang_Apply;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpHoiHang_ToDate;
        private System.Windows.Forms.DateTimePicker dtpHoiHang_FromDate;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tpDebitNote;
        private System.Windows.Forms.TabPage tpVita;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_makh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ngaydat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ponhuom;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ponhuommoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_madonkh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mahangkh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_lieukh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_lieuthaythe;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_kho;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_maukh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mauthaythe;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_motamau;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sldat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dongia;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tongtien;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_etd;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ngayxuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_invoicehoadon;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_invoicepgh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_article;
        private System.Windows.Forms.Panel pnlFilter;
    }
}