namespace  Julian_Solution.Forms
{
    partial class frmGroupByPO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupByPO));
            this.btnExport = new System.Windows.Forms.Button();
            this.linkResult = new System.Windows.Forms.LinkLabel();
            this.lblFileName1 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(16, 63);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(131, 25);
            this.btnExport.TabIndex = 112;
            this.btnExport.Text = "Xuất dữ liệu";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // linkResult
            // 
            this.linkResult.AutoEllipsis = true;
            this.linkResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkResult.Location = new System.Drawing.Point(3, 19);
            this.linkResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.linkResult.Name = "linkResult";
            this.linkResult.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.linkResult.Size = new System.Drawing.Size(351, 24);
            this.linkResult.TabIndex = 121;
            this.linkResult.TabStop = true;
            this.linkResult.Text = "linkResult";
            this.linkResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkResult.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkResult_LinkClicked);
            // 
            // lblFileName1
            // 
            this.lblFileName1.AutoEllipsis = true;
            this.lblFileName1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName1.Location = new System.Drawing.Point(17, 33);
            this.lblFileName1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileName1.Name = "lblFileName1";
            this.lblFileName1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblFileName1.Size = new System.Drawing.Size(318, 27);
            this.lblFileName1.TabIndex = 123;
            this.lblFileName1.Text = "...";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AutoEllipsis = true;
            this.btnSelectFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile.BackgroundImage")));
            this.btnSelectFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile.FlatAppearance.BorderSize = 0;
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.Location = new System.Drawing.Point(343, 31);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(30, 30);
            this.btnSelectFile.TabIndex = 122;
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 126;
            this.label3.Text = "File nguồn";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkResult);
            this.groupBox1.Location = new System.Drawing.Point(16, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 46);
            this.groupBox1.TabIndex = 128;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // frmGroupByPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(388, 152);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFileName1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnExport);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmGroupByPO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gộp PO";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.LinkLabel linkResult;
        private System.Windows.Forms.Label lblFileName1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}