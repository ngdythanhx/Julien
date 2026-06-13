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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMain = new System.Windows.Forms.DataGridView();
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpSanLuong = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tpBaoCaoTuan = new System.Windows.Forms.TabPage();
            this.tpDebitNote = new System.Windows.Forms.TabPage();
            this.tpVita = new System.Windows.Forms.TabPage();
            this.pnlFilter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
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
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            this.col_ngaydat.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col_ponhuom.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_kho.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0.00";
            this.col_sldat.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_sldat.HeaderText = "SL đặt hàng";
            this.col_sldat.Name = "col_sldat";
            // 
            // col_dongia
            // 
            this.col_dongia.DataPropertyName = "DonGia";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0.00";
            this.col_dongia.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_dongia.HeaderText = "Đơn giá";
            this.col_dongia.Name = "col_dongia";
            // 
            // col_tongtien
            // 
            this.col_tongtien.DataPropertyName = "TongTien";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "#,##0.00";
            this.col_tongtien.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_tongtien.HeaderText = "Tổng tiền";
            this.col_tongtien.Name = "col_tongtien";
            // 
            // col_etd
            // 
            this.col_etd.DataPropertyName = "ETD";
            dataGridViewCellStyle7.Format = "yyyy-MM-dd";
            this.col_etd.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_etd.HeaderText = "ETD";
            this.col_etd.Name = "col_etd";
            // 
            // col_ngayxuat
            // 
            this.col_ngayxuat.DataPropertyName = "NgayXuat";
            dataGridViewCellStyle8.Format = "yyyy-MM-dd";
            this.col_ngayxuat.DefaultCellStyle = dataGridViewCellStyle8;
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
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpSanLuong);
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tpBaoCaoTuan);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1564, 351);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Hối hàng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tpBaoCaoTuan
            // 
            this.tpBaoCaoTuan.Location = new System.Drawing.Point(4, 22);
            this.tpBaoCaoTuan.Name = "tpBaoCaoTuan";
            this.tpBaoCaoTuan.Padding = new System.Windows.Forms.Padding(3);
            this.tpBaoCaoTuan.Size = new System.Drawing.Size(1564, 351);
            this.tpBaoCaoTuan.TabIndex = 3;
            this.tpBaoCaoTuan.Text = "Báo cáo tuần";
            this.tpBaoCaoTuan.UseVisualStyleBackColor = true;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpSanLuong;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpBaoCaoTuan;
        private System.Windows.Forms.TabPage tpDebitNote;
        private System.Windows.Forms.TabPage tpVita;
        private System.Windows.Forms.Panel pnlFilter;
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
    }
}