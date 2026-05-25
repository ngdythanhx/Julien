namespace KhoHang
{
    partial class frmAutoUpdateKhoHang
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
            this.chkTonKho = new System.Windows.Forms.CheckBox();
            this.chkXuatKho = new System.Windows.Forms.CheckBox();
            this.updDelayTime = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.updDelayTime)).BeginInit();
            this.SuspendLayout();
            // 
            // chkTonKho
            // 
            this.chkTonKho.AutoSize = true;
            this.chkTonKho.Location = new System.Drawing.Point(15, 15);
            this.chkTonKho.Name = "chkTonKho";
            this.chkTonKho.Size = new System.Drawing.Size(67, 17);
            this.chkTonKho.TabIndex = 0;
            this.chkTonKho.Text = "Tồn Kho";
            this.chkTonKho.UseVisualStyleBackColor = true;
            // 
            // chkXuatKho
            // 
            this.chkXuatKho.AutoSize = true;
            this.chkXuatKho.Location = new System.Drawing.Point(15, 38);
            this.chkXuatKho.Name = "chkXuatKho";
            this.chkXuatKho.Size = new System.Drawing.Size(70, 17);
            this.chkXuatKho.TabIndex = 1;
            this.chkXuatKho.Text = "Xuất Kho";
            this.chkXuatKho.UseVisualStyleBackColor = true;
            // 
            // updDelayTime
            // 
            this.updDelayTime.Location = new System.Drawing.Point(12, 93);
            this.updDelayTime.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.updDelayTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.updDelayTime.Name = "updDelayTime";
            this.updDelayTime.Size = new System.Drawing.Size(84, 20);
            this.updDelayTime.TabIndex = 2;
            this.updDelayTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 119);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "giây";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Đơn hàng TM";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 146);
            this.textBox1.TabIndex = 5;
            // 
            // frmAutoUpdateKhoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 152);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.updDelayTime);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chkXuatKho);
            this.Controls.Add(this.chkTonKho);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAutoUpdateKhoHang";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "AutoUpdateKhoHang";
            ((System.ComponentModel.ISupportInitialize)(this.updDelayTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTonKho;
        private System.Windows.Forms.CheckBox chkXuatKho;
        private System.Windows.Forms.NumericUpDown updDelayTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}