namespace Julian_Server
{
    partial class frmMain2
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tất cả");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Đơn đang xử lý");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Đơn đã hoàn thành");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Đơn hàng", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Đơn đang xử lý");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Đơn đã nhận");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Đơn đặt nhuộm", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Vật liệu");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Khách hàng");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsvExcelData = new System.Windows.Forms.ListView();
            this.txtExcelData = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.cbMaKH = new System.Windows.Forms.ComboBox();
            this.cbPONhuom = new System.Windows.Forms.ComboBox();
            this.cbETD = new System.Windows.Forms.ComboBox();
            this.cbPONhuomMoi = new System.Windows.Forms.ComboBox();
            this.cbNgayDat = new System.Windows.Forms.ComboBox();
            this.cbNgayGiao = new System.Windows.Forms.ComboBox();
            this.cbSlDat = new System.Windows.Forms.ComboBox();
            this.cbSlGiao = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "nodeOrderCustomer_All";
            treeNode1.Text = "Tất cả";
            treeNode2.Name = "nodeOrderCustomer_Pending";
            treeNode2.Text = "Đơn đang xử lý";
            treeNode3.Name = "nodeOrderCustomer_Completed";
            treeNode3.Text = "Đơn đã hoàn thành";
            treeNode4.Name = "nodeOrderCustomer";
            treeNode4.Text = "Đơn hàng";
            treeNode5.Name = "nodeOrderDye_Pending";
            treeNode5.Text = "Đơn đang xử lý";
            treeNode6.Name = "nodeOrderDye_Received";
            treeNode6.Text = "Đơn đã nhận";
            treeNode7.Name = "Node1";
            treeNode7.Text = "Đơn đặt nhuộm";
            treeNode8.Name = "nodeMetarial";
            treeNode8.Text = "Vật liệu";
            treeNode9.Name = "nodeCustomer";
            treeNode9.Text = "Khách hàng";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode8,
            treeNode9});
            this.treeView1.Size = new System.Drawing.Size(228, 624);
            this.treeView1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(1381, 624);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsvExcelData);
            this.groupBox2.Controls.Add(this.txtExcelData);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(552, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 164);
            this.groupBox2.TabIndex = 222;
            this.groupBox2.TabStop = false;
            // 
            // lsvExcelData
            // 
            this.lsvExcelData.HideSelection = false;
            this.lsvExcelData.Location = new System.Drawing.Point(246, 19);
            this.lsvExcelData.Name = "lsvExcelData";
            this.lsvExcelData.Size = new System.Drawing.Size(259, 139);
            this.lsvExcelData.TabIndex = 214;
            this.lsvExcelData.UseCompatibleStateImageBehavior = false;
            // 
            // txtExcelData
            // 
            this.txtExcelData.Location = new System.Drawing.Point(6, 19);
            this.txtExcelData.Multiline = true;
            this.txtExcelData.Name = "txtExcelData";
            this.txtExcelData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExcelData.Size = new System.Drawing.Size(208, 139);
            this.txtExcelData.TabIndex = 212;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 213;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(218, 69);
            this.button3.Margin = new System.Windows.Forms.Padding(1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 213;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.cbMaKH);
            this.groupBox1.Controls.Add(this.cbPONhuom);
            this.groupBox1.Controls.Add(this.cbETD);
            this.groupBox1.Controls.Add(this.cbPONhuomMoi);
            this.groupBox1.Controls.Add(this.cbNgayDat);
            this.groupBox1.Controls.Add(this.cbNgayGiao);
            this.groupBox1.Controls.Add(this.cbSlDat);
            this.groupBox1.Controls.Add(this.cbSlGiao);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(86, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 164);
            this.groupBox1.TabIndex = 221;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 211;
            this.label1.Text = "Mã KH";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 135);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 23);
            this.button4.TabIndex = 218;
            this.button4.Text = "Reset All";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // cbMaKH
            // 
            this.cbMaKH.FormattingEnabled = true;
            this.cbMaKH.Location = new System.Drawing.Point(73, 19);
            this.cbMaKH.Name = "cbMaKH";
            this.cbMaKH.Size = new System.Drawing.Size(131, 25);
            this.cbMaKH.TabIndex = 210;
            // 
            // cbPONhuom
            // 
            this.cbPONhuom.FormattingEnabled = true;
            this.cbPONhuom.Location = new System.Drawing.Point(316, 19);
            this.cbPONhuom.Name = "cbPONhuom";
            this.cbPONhuom.Size = new System.Drawing.Size(131, 25);
            this.cbPONhuom.TabIndex = 210;
            // 
            // cbETD
            // 
            this.cbETD.FormattingEnabled = true;
            this.cbETD.Location = new System.Drawing.Point(73, 46);
            this.cbETD.Name = "cbETD";
            this.cbETD.Size = new System.Drawing.Size(131, 25);
            this.cbETD.TabIndex = 210;
            // 
            // cbPONhuomMoi
            // 
            this.cbPONhuomMoi.FormattingEnabled = true;
            this.cbPONhuomMoi.Location = new System.Drawing.Point(316, 46);
            this.cbPONhuomMoi.Name = "cbPONhuomMoi";
            this.cbPONhuomMoi.Size = new System.Drawing.Size(131, 25);
            this.cbPONhuomMoi.TabIndex = 210;
            // 
            // cbNgayDat
            // 
            this.cbNgayDat.FormattingEnabled = true;
            this.cbNgayDat.Location = new System.Drawing.Point(73, 73);
            this.cbNgayDat.Name = "cbNgayDat";
            this.cbNgayDat.Size = new System.Drawing.Size(131, 25);
            this.cbNgayDat.TabIndex = 210;
            // 
            // cbNgayGiao
            // 
            this.cbNgayGiao.FormattingEnabled = true;
            this.cbNgayGiao.Location = new System.Drawing.Point(316, 73);
            this.cbNgayGiao.Name = "cbNgayGiao";
            this.cbNgayGiao.Size = new System.Drawing.Size(131, 25);
            this.cbNgayGiao.TabIndex = 210;
            // 
            // cbSlDat
            // 
            this.cbSlDat.FormattingEnabled = true;
            this.cbSlDat.Location = new System.Drawing.Point(73, 100);
            this.cbSlDat.Name = "cbSlDat";
            this.cbSlDat.Size = new System.Drawing.Size(131, 25);
            this.cbSlDat.TabIndex = 210;
            // 
            // cbSlGiao
            // 
            this.cbSlGiao.FormattingEnabled = true;
            this.cbSlGiao.Location = new System.Drawing.Point(316, 100);
            this.cbSlGiao.Name = "cbSlGiao";
            this.cbSlGiao.Size = new System.Drawing.Size(131, 25);
            this.cbSlGiao.TabIndex = 210;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 17);
            this.label9.TabIndex = 211;
            this.label9.Text = "PO Nhuộm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(232, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 17);
            this.label12.TabIndex = 211;
            this.label12.Text = "Sl giao";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 211;
            this.label2.Text = "Ngày đặt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 17);
            this.label4.TabIndex = 211;
            this.label4.Text = "ETD";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(232, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 17);
            this.label10.TabIndex = 211;
            this.label10.Text = "PO Nhuộm mới";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(232, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 17);
            this.label11.TabIndex = 211;
            this.label11.Text = "Ngày giao";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 211;
            this.label3.Text = "Sl đặt";
            // 
            // frmMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 624);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain2";
            this.Text = "frmMain2";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsvExcelData;
        private System.Windows.Forms.TextBox txtExcelData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbMaKH;
        private System.Windows.Forms.ComboBox cbPONhuom;
        private System.Windows.Forms.ComboBox cbETD;
        private System.Windows.Forms.ComboBox cbPONhuomMoi;
        private System.Windows.Forms.ComboBox cbNgayDat;
        private System.Windows.Forms.ComboBox cbNgayGiao;
        private System.Windows.Forms.ComboBox cbSlDat;
        private System.Windows.Forms.ComboBox cbSlGiao;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
    }
}