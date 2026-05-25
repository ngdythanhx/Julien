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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.col_makh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ngaydathang = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.col_invoicephieugiaohang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_makh,
            this.col_ngaydathang,
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
            this.col_invoicephieugiaohang,
            this.col_article});
            this.dgvMain.Location = new System.Drawing.Point(6, 6);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.Size = new System.Drawing.Size(1572, 344);
            this.dgvMain.TabIndex = 0;
            // 
            // col_makh
            // 
            this.col_makh.DataPropertyName = "MaKH";
            this.col_makh.HeaderText = "Mã KH";
            this.col_makh.Name = "col_makh";
            this.col_makh.Width = 80;
            // 
            // col_ngaydathang
            // 
            this.col_ngaydathang.DataPropertyName = "NgayDat";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            this.col_ngaydathang.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_ngaydathang.HeaderText = "Ngày đặt hàng";
            this.col_ngaydathang.Name = "col_ngaydathang";
            this.col_ngaydathang.Width = 120;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_kho.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0.00";
            this.col_sldat.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_sldat.HeaderText = "SL đặt hàng";
            this.col_sldat.Name = "col_sldat";
            // 
            // col_dongia
            // 
            this.col_dongia.DataPropertyName = "DonGia";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0.00";
            this.col_dongia.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_dongia.HeaderText = "Đơn giá";
            this.col_dongia.Name = "col_dongia";
            // 
            // col_tongtien
            // 
            this.col_tongtien.DataPropertyName = "TongTien";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0.00";
            this.col_tongtien.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_tongtien.HeaderText = "Tổng tiền";
            this.col_tongtien.Name = "col_tongtien";
            // 
            // col_etd
            // 
            this.col_etd.DataPropertyName = "ETD";
            dataGridViewCellStyle6.Format = "yyyy-MM-dd";
            this.col_etd.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_etd.HeaderText = "ETD";
            this.col_etd.Name = "col_etd";
            // 
            // col_ngayxuat
            // 
            this.col_ngayxuat.DataPropertyName = "NgayXuat";
            dataGridViewCellStyle7.Format = "yyyy-MM-dd";
            this.col_ngayxuat.DefaultCellStyle = dataGridViewCellStyle7;
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
            // col_invoicephieugiaohang
            // 
            this.col_invoicephieugiaohang.DataPropertyName = "InvoicePHG";
            this.col_invoicephieugiaohang.HeaderText = "Invoice PHG";
            this.col_invoicephieugiaohang.Name = "col_invoicephieugiaohang";
            this.col_invoicephieugiaohang.Width = 130;
            // 
            // col_article
            // 
            this.col_article.DataPropertyName = "Article";
            this.col_article.HeaderText = "Article";
            this.col_article.Name = "col_article";
            // 
            // frmReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 761);
            this.Controls.Add(this.dgvMain);
            this.Name = "frmReporter";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmReporter";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_makh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ngaydathang;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn col_invoicephieugiaohang;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_article;
    }
}