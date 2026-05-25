namespace Julian.Forms
{
    partial class frmThemVaiMoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemVaiMoc));
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnSelectFiles_ThuMua = new System.Windows.Forms.Button();
            this.lblFileName_BaoCao = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectFile_BaoCao = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 122;
            this.label1.Text = "Danh sách tệp tin nguồn (các file thu mua)";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(338, 134);
            this.listBox1.TabIndex = 121;
            // 
            // btnSelectFiles_ThuMua
            // 
            this.btnSelectFiles_ThuMua.AutoEllipsis = true;
            this.btnSelectFiles_ThuMua.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFiles_ThuMua.BackgroundImage")));
            this.btnSelectFiles_ThuMua.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFiles_ThuMua.FlatAppearance.BorderSize = 0;
            this.btnSelectFiles_ThuMua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFiles_ThuMua.Location = new System.Drawing.Point(324, 2);
            this.btnSelectFiles_ThuMua.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFiles_ThuMua.Name = "btnSelectFiles_ThuMua";
            this.btnSelectFiles_ThuMua.Size = new System.Drawing.Size(26, 26);
            this.btnSelectFiles_ThuMua.TabIndex = 120;
            this.btnSelectFiles_ThuMua.UseVisualStyleBackColor = true;
            this.btnSelectFiles_ThuMua.Click += new System.EventHandler(this.btnSelectFiles_ThuMua_Click);
            // 
            // lblFileName_BaoCao
            // 
            this.lblFileName_BaoCao.AutoEllipsis = true;
            this.lblFileName_BaoCao.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_BaoCao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_BaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName_BaoCao.Location = new System.Drawing.Point(12, 195);
            this.lblFileName_BaoCao.Name = "lblFileName_BaoCao";
            this.lblFileName_BaoCao.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblFileName_BaoCao.Size = new System.Drawing.Size(304, 22);
            this.lblFileName_BaoCao.TabIndex = 128;
            this.lblFileName_BaoCao.Text = "...";
            this.lblFileName_BaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 129;
            this.label3.Text = "File Báo cáo tổng hợp";
            // 
            // btnSelectFile_BaoCao
            // 
            this.btnSelectFile_BaoCao.AutoEllipsis = true;
            this.btnSelectFile_BaoCao.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_BaoCao.BackgroundImage")));
            this.btnSelectFile_BaoCao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_BaoCao.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_BaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_BaoCao.Location = new System.Drawing.Point(322, 195);
            this.btnSelectFile_BaoCao.Name = "btnSelectFile_BaoCao";
            this.btnSelectFile_BaoCao.Size = new System.Drawing.Size(28, 22);
            this.btnSelectFile_BaoCao.TabIndex = 127;
            this.btnSelectFile_BaoCao.UseVisualStyleBackColor = true;
            this.btnSelectFile_BaoCao.Click += new System.EventHandler(this.btnSelectFile_BaoCao_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(88, 220);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 23);
            this.progressBar1.TabIndex = 131;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(11, 220);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 23);
            this.btnStart.TabIndex = 130;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(69, 258);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(13, 13);
            this.lblProcessing.TabIndex = 132;
            this.lblProcessing.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 133;
            this.label2.Text = "Tiến trình:";
            // 
            // frmThemVaiMoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 283);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblFileName_BaoCao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectFile_BaoCao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnSelectFiles_ThuMua);
            this.Name = "frmThemVaiMoc";
            this.Text = "frmThemVaiMoc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnSelectFiles_ThuMua;
        private System.Windows.Forms.Label lblFileName_BaoCao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFile_BaoCao;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.Label label2;
    }
}