namespace JulianProduce
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Order = new System.Windows.Forms.TabPage();
            this.tabPage_OrderItem = new System.Windows.Forms.TabPage();
            this.tabPage_ = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage_Order);
            this.tabControl1.Controls.Add(this.tabPage_OrderItem);
            this.tabControl1.Controls.Add(this.tabPage_);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1264, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Order
            // 
            this.tabPage_Order.Location = new System.Drawing.Point(4, 4);
            this.tabPage_Order.Name = "tabPage_Order";
            this.tabPage_Order.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Order.Size = new System.Drawing.Size(1256, 535);
            this.tabPage_Order.TabIndex = 0;
            this.tabPage_Order.Text = "tabPage1";
            this.tabPage_Order.UseVisualStyleBackColor = true;
            // 
            // tabPage_OrderItem
            // 
            this.tabPage_OrderItem.Location = new System.Drawing.Point(4, 4);
            this.tabPage_OrderItem.Name = "tabPage_OrderItem";
            this.tabPage_OrderItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_OrderItem.Size = new System.Drawing.Size(1256, 535);
            this.tabPage_OrderItem.TabIndex = 1;
            this.tabPage_OrderItem.Text = "tabPage2";
            this.tabPage_OrderItem.UseVisualStyleBackColor = true;
            // 
            // tabPage_
            // 
            this.tabPage_.Location = new System.Drawing.Point(4, 4);
            this.tabPage_.Name = "tabPage_";
            this.tabPage_.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_.Size = new System.Drawing.Size(1256, 535);
            this.tabPage_.TabIndex = 2;
            this.tabPage_.Text = "tabPage1";
            this.tabPage_.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Order;
        private System.Windows.Forms.TabPage tabPage_OrderItem;
        private System.Windows.Forms.TabPage tabPage_;
    }
}