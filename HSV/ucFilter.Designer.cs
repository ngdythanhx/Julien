namespace Julian_Server
{
    partial class ucFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lsbData = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(3, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(81, 17);
            this.chkAll.TabIndex = 234;
            this.chkAll.Text = "Chọn tất cả";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // lsbData
            // 
            this.lsbData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbData.CheckOnClick = true;
            this.lsbData.FormattingEnabled = true;
            this.lsbData.Location = new System.Drawing.Point(0, 25);
            this.lsbData.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lsbData.Name = "lsbData";
            this.lsbData.ScrollAlwaysVisible = true;
            this.lsbData.Size = new System.Drawing.Size(138, 184);
            this.lsbData.TabIndex = 232;
            this.lsbData.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsbData_ItemCheck);
            // 
            // ucFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.lsbData);
            this.Name = "ucFilter";
            this.Size = new System.Drawing.Size(138, 217);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckedListBox lsbData;
    }
}
