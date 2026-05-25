namespace Julian.Forms
{
    partial class frmSuperFilter
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dsadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dsadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsadsaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsadsaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dsadaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 26);
            // 
            // dsadaToolStripMenuItem
            // 
            this.dsadaToolStripMenuItem.Checked = true;
            this.dsadaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dsadaToolStripMenuItem.Name = "dsadaToolStripMenuItem";
            this.dsadaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dsadaToolStripMenuItem.Text = "dsada";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dsadsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dsadsToolStripMenuItem
            // 
            this.dsadsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dsadsaToolStripMenuItem,
            this.dsadsaToolStripMenuItem1});
            this.dsadsToolStripMenuItem.Name = "dsadsToolStripMenuItem";
            this.dsadsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.dsadsToolStripMenuItem.Text = "dsads";
            // 
            // dsadsaToolStripMenuItem
            // 
            this.dsadsaToolStripMenuItem.Name = "dsadsaToolStripMenuItem";
            this.dsadsaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dsadsaToolStripMenuItem.Text = "dsadsa";
            // 
            // dsadsaToolStripMenuItem1
            // 
            this.dsadsaToolStripMenuItem1.Name = "dsadsaToolStripMenuItem1";
            this.dsadsaToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.dsadsaToolStripMenuItem1.Text = "dsadsa";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(515, 326);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 94);
            this.checkedListBox1.TabIndex = 2;
            // 
            // frmSuperFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmSuperFilter";
            this.Text = "frmSuperFilter";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dsadaToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dsadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dsadsaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dsadsaToolStripMenuItem1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}