namespace AutoBackup
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrderFormFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSMTTFolderPath = new System.Windows.Forms.TextBox();
            this.btnOrderFormSelectedFolder = new System.Windows.Forms.Button();
            this.btnSMTTSelectedFolder = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn OrderForm";
            // 
            // txtOrderFormFolderPath
            // 
            this.txtOrderFormFolderPath.Location = new System.Drawing.Point(12, 28);
            this.txtOrderFormFolderPath.Name = "txtOrderFormFolderPath";
            this.txtOrderFormFolderPath.Size = new System.Drawing.Size(277, 20);
            this.txtOrderFormFolderPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Đường dẫn SMTT";
            // 
            // txtSMTTFolderPath
            // 
            this.txtSMTTFolderPath.Location = new System.Drawing.Point(12, 75);
            this.txtSMTTFolderPath.Name = "txtSMTTFolderPath";
            this.txtSMTTFolderPath.Size = new System.Drawing.Size(277, 20);
            this.txtSMTTFolderPath.TabIndex = 1;
            // 
            // btnOrderFormSelectedFolder
            // 
            this.btnOrderFormSelectedFolder.AutoEllipsis = true;
            this.btnOrderFormSelectedFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOrderFormSelectedFolder.BackgroundImage")));
            this.btnOrderFormSelectedFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOrderFormSelectedFolder.FlatAppearance.BorderSize = 0;
            this.btnOrderFormSelectedFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderFormSelectedFolder.Location = new System.Drawing.Point(296, 26);
            this.btnOrderFormSelectedFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOrderFormSelectedFolder.Name = "btnOrderFormSelectedFolder";
            this.btnOrderFormSelectedFolder.Size = new System.Drawing.Size(26, 22);
            this.btnOrderFormSelectedFolder.TabIndex = 149;
            this.btnOrderFormSelectedFolder.UseVisualStyleBackColor = true;
            // 
            // btnSMTTSelectedFolder
            // 
            this.btnSMTTSelectedFolder.AutoEllipsis = true;
            this.btnSMTTSelectedFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSMTTSelectedFolder.BackgroundImage")));
            this.btnSMTTSelectedFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSMTTSelectedFolder.FlatAppearance.BorderSize = 0;
            this.btnSMTTSelectedFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSMTTSelectedFolder.Location = new System.Drawing.Point(296, 75);
            this.btnSMTTSelectedFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSMTTSelectedFolder.Name = "btnSMTTSelectedFolder";
            this.btnSMTTSelectedFolder.Size = new System.Drawing.Size(26, 22);
            this.btnSMTTSelectedFolder.TabIndex = 149;
            this.btnSMTTSelectedFolder.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(104, 102);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(59, 20);
            this.dateTimePicker1.TabIndex = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 151;
            this.label3.Text = "Backup mỗi ngày";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 251);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnSMTTSelectedFolder);
            this.Controls.Add(this.btnOrderFormSelectedFolder);
            this.Controls.Add(this.txtSMTTFolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrderFormFolderPath);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Auto Backup";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderFormFolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSMTTFolderPath;
        private System.Windows.Forms.Button btnOrderFormSelectedFolder;
        private System.Windows.Forms.Button btnSMTTSelectedFolder;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
    }
}

