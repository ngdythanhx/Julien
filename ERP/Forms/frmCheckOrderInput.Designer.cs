namespace ERP.Forms
{
    partial class frmCheckOrderInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckOrderInput));
            this.lblFileName_OrderForm = new System.Windows.Forms.Label();
            this.btnSelectFile_OrderForm = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileName_OrderForm
            // 
            this.lblFileName_OrderForm.AutoEllipsis = true;
            this.lblFileName_OrderForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFileName_OrderForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName_OrderForm.Location = new System.Drawing.Point(12, 13);
            this.lblFileName_OrderForm.Name = "lblFileName_OrderForm";
            this.lblFileName_OrderForm.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblFileName_OrderForm.Size = new System.Drawing.Size(242, 20);
            this.lblFileName_OrderForm.TabIndex = 151;
            this.lblFileName_OrderForm.Text = "...";
            this.lblFileName_OrderForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectFile_OrderForm
            // 
            this.btnSelectFile_OrderForm.AutoEllipsis = true;
            this.btnSelectFile_OrderForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectFile_OrderForm.BackgroundImage")));
            this.btnSelectFile_OrderForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFile_OrderForm.FlatAppearance.BorderSize = 0;
            this.btnSelectFile_OrderForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile_OrderForm.Location = new System.Drawing.Point(261, 13);
            this.btnSelectFile_OrderForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectFile_OrderForm.Name = "btnSelectFile_OrderForm";
            this.btnSelectFile_OrderForm.Size = new System.Drawing.Size(26, 20);
            this.btnSelectFile_OrderForm.TabIndex = 150;
            this.btnSelectFile_OrderForm.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(307, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 152;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(422, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 153;
            this.btnStart.Text = "button1";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1160, 663);
            this.dataGridView1.TabIndex = 154;
            // 
            // frmCheckOrderInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 711);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblFileName_OrderForm);
            this.Controls.Add(this.btnSelectFile_OrderForm);
            this.Name = "frmCheckOrderInput";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmCheckOrderInput";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileName_OrderForm;
        private System.Windows.Forms.Button btnSelectFile_OrderForm;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}