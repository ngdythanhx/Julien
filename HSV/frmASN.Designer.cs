namespace HSV
{
    partial class frmASN
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
            this.btnOpenSCM = new System.Windows.Forms.Button();
            this.btnGetTemCount = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenSCM
            // 
            this.btnOpenSCM.Location = new System.Drawing.Point(12, 12);
            this.btnOpenSCM.Name = "btnOpenSCM";
            this.btnOpenSCM.Size = new System.Drawing.Size(120, 25);
            this.btnOpenSCM.TabIndex = 0;
            this.btnOpenSCM.Text = "Mở SCM";
            this.btnOpenSCM.UseVisualStyleBackColor = true;
            this.btnOpenSCM.Click += new System.EventHandler(this.btnOpenSCM_Click);
            // 
            // btnGetTemCount
            // 
            this.btnGetTemCount.Location = new System.Drawing.Point(138, 12);
            this.btnGetTemCount.Name = "btnGetTemCount";
            this.btnGetTemCount.Size = new System.Drawing.Size(120, 25);
            this.btnGetTemCount.TabIndex = 1;
            this.btnGetTemCount.Text = "Đếm số tem";
            this.btnGetTemCount.UseVisualStyleBackColor = true;
            this.btnGetTemCount.Click += new System.EventHandler(this.btnGetTemCount_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.col_Article,
            this.Column2});
            this.dgvMain.Location = new System.Drawing.Point(6, 43);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(1272, 512);
            this.dgvMain.TabIndex = 299;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(264, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 17);
            this.checkBox1.TabIndex = 300;
            this.checkBox1.Text = "Tách theo Article";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaterialCode";
            this.Column1.HeaderText = "Material Code";
            this.Column1.Name = "Column1";
            this.Column1.Width = 130;
            // 
            // col_Article
            // 
            this.col_Article.DataPropertyName = "Article";
            this.col_Article.HeaderText = "Article";
            this.col_Article.Name = "col_Article";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "TemCount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "TemCount";
            this.Column2.Name = "Column2";
            this.Column2.Width = 70;
            // 
            // frmASN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 561);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnGetTemCount);
            this.Controls.Add(this.btnOpenSCM);
            this.Name = "frmASN";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmASN";
            this.Load += new System.EventHandler(this.frmASN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSCM;
        private System.Windows.Forms.Button btnGetTemCount;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Article;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}