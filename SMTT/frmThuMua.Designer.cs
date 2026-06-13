namespace SMTT
{
    partial class frmThuMua
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btnLoadThuMuaData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 7);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(357, 94);
            this.checkedListBox1.TabIndex = 0;
            // 
            // btnLoadThuMuaData
            // 
            this.btnLoadThuMuaData.Location = new System.Drawing.Point(6, 107);
            this.btnLoadThuMuaData.Name = "btnLoadThuMuaData";
            this.btnLoadThuMuaData.Size = new System.Drawing.Size(114, 23);
            this.btnLoadThuMuaData.TabIndex = 1;
            this.btnLoadThuMuaData.Text = "Tải dữ liệu Thu Mua";
            this.btnLoadThuMuaData.UseVisualStyleBackColor = true;
            this.btnLoadThuMuaData.Click += new System.EventHandler(this.btnLoadThuMuaData_Click);
            // 
            // frmThuMua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoadThuMuaData);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "frmThuMua";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmThuMua";
            this.Load += new System.EventHandler(this.frmThuMua_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btnLoadThuMuaData;
    }
}