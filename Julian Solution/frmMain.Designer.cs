namespace Julian_Solution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsDatabase = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsEmployeeTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsGroupByPO = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCreateNew = new System.Windows.Forms.ToolStripButton();
            this.tsEdit = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsCancel = new System.Windows.Forms.ToolStripButton();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDatabase,
            this.toolStripSeparator3,
            this.tsTools,
            this.toolStripSeparator4,
            this.tsReload,
            this.toolStripSeparator1,
            this.tsCreateNew,
            this.tsEdit,
            this.tsDelete,
            this.toolStripSeparator2,
            this.tsSave,
            this.tsCancel});
            this.toolStrip2.Location = new System.Drawing.Point(4, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(1176, 25);
            this.toolStrip2.TabIndex = 15;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsDatabase
            // 
            this.tsDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEmployeeTask,
            this.tsEmployee,
            this.tsCustomer,
            this.tsMaterial,
            this.tsOrder});
            this.tsDatabase.Image = ((System.Drawing.Image)(resources.GetObject("tsDatabase.Image")));
            this.tsDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDatabase.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsDatabase.Name = "tsDatabase";
            this.tsDatabase.Size = new System.Drawing.Size(73, 22);
            this.tsDatabase.Text = "Dữ liệu";
            this.tsDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsEmployeeTask
            // 
            this.tsEmployeeTask.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsEmployeeTask.Name = "tsEmployeeTask";
            this.tsEmployeeTask.Size = new System.Drawing.Size(139, 22);
            this.tsEmployeeTask.Text = "Nhiệm Vụ";
            this.tsEmployeeTask.Click += new System.EventHandler(this.tsEmployeeTask_Click);
            // 
            // tsEmployee
            // 
            this.tsEmployee.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsEmployee.Name = "tsEmployee";
            this.tsEmployee.Size = new System.Drawing.Size(139, 22);
            this.tsEmployee.Text = "Nhân Viên";
            this.tsEmployee.Click += new System.EventHandler(this.tsEmployee_Click);
            // 
            // tsCustomer
            // 
            this.tsCustomer.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsCustomer.Name = "tsCustomer";
            this.tsCustomer.Size = new System.Drawing.Size(139, 22);
            this.tsCustomer.Text = "Khách Hàng";
            this.tsCustomer.Click += new System.EventHandler(this.tsCustomer_Click);
            // 
            // tsMaterial
            // 
            this.tsMaterial.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsMaterial.Name = "tsMaterial";
            this.tsMaterial.Size = new System.Drawing.Size(139, 22);
            this.tsMaterial.Text = "Vật Liệu";
            this.tsMaterial.Click += new System.EventHandler(this.tsMaterial_Click);
            // 
            // tsOrder
            // 
            this.tsOrder.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsOrder.Name = "tsOrder";
            this.tsOrder.Size = new System.Drawing.Size(139, 22);
            this.tsOrder.Text = "Đơn Hàng";
            this.tsOrder.Click += new System.EventHandler(this.tsOrder_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsTools
            // 
            this.tsTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsGroupByPO,
            this.tsAddOrder,
            this.tsCompare});
            this.tsTools.Image = ((System.Drawing.Image)(resources.GetObject("tsTools.Image")));
            this.tsTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(81, 22);
            this.tsTools.Text = "Công cụ";
            // 
            // tsGroupByPO
            // 
            this.tsGroupByPO.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsGroupByPO.Name = "tsGroupByPO";
            this.tsGroupByPO.Size = new System.Drawing.Size(161, 22);
            this.tsGroupByPO.Text = "Gộp PO";
            this.tsGroupByPO.Click += new System.EventHandler(this.tsGroupByPO_Click);
            // 
            // tsAddOrder
            // 
            this.tsAddOrder.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsAddOrder.Name = "tsAddOrder";
            this.tsAddOrder.Size = new System.Drawing.Size(161, 22);
            this.tsAddOrder.Text = "Thêm Đơn Hàng";
            this.tsAddOrder.Click += new System.EventHandler(this.tsAddOrder_Click);
            // 
            // tsCompare
            // 
            this.tsCompare.Name = "tsCompare";
            this.tsCompare.Size = new System.Drawing.Size(161, 22);
            this.tsCompare.Text = "So Sánh Dữ Liệu";
            this.tsCompare.Click += new System.EventHandler(this.tsCompare_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsReload
            // 
            this.tsReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsReload.Image = ((System.Drawing.Image)(resources.GetObject("tsReload.Image")));
            this.tsReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsReload.Name = "tsReload";
            this.tsReload.Size = new System.Drawing.Size(23, 22);
            this.tsReload.Text = "toolStripButton1";
            this.tsReload.Click += new System.EventHandler(this.tsReload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsCreateNew
            // 
            this.tsCreateNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCreateNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCreateNew.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.tsCreateNew.Name = "tsCreateNew";
            this.tsCreateNew.Size = new System.Drawing.Size(23, 22);
            this.tsCreateNew.Text = "toolStripButton1";
            this.tsCreateNew.ToolTipText = "Thêm";
            this.tsCreateNew.Click += new System.EventHandler(this.tsCreateNew_Click);
            // 
            // tsEdit
            // 
            this.tsEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsEdit.Image")));
            this.tsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEdit.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.tsEdit.Name = "tsEdit";
            this.tsEdit.Size = new System.Drawing.Size(23, 22);
            this.tsEdit.Text = "toolStripButton2";
            this.tsEdit.ToolTipText = "Sửa";
            this.tsEdit.Click += new System.EventHandler(this.tsEdit_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(23, 22);
            this.tsDelete.Text = "toolStripButton3";
            this.tsDelete.ToolTipText = "Xóa";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(23, 22);
            this.tsSave.Text = "toolStripButton4";
            this.tsSave.ToolTipText = "Lưu";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsCancel
            // 
            this.tsCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsCancel.Image")));
            this.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCancel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(23, 22);
            this.tsCancel.Text = "toolStripButton5";
            this.tsCancel.ToolTipText = "Hủy lưu";
            this.tsCancel.Click += new System.EventHandler(this.tsCancel_Click);
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(4, 28);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(10, 5);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1178, 729);
            this.tcMain.TabIndex = 16;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton tsDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsEmployeeTask;
        private System.Windows.Forms.ToolStripMenuItem tsEmployee;
        private System.Windows.Forms.ToolStripMenuItem tsCustomer;
        private System.Windows.Forms.ToolStripMenuItem tsMaterial;
        private System.Windows.Forms.ToolStripMenuItem tsOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton tsTools;
        private System.Windows.Forms.ToolStripMenuItem tsGroupByPO;
        private System.Windows.Forms.ToolStripMenuItem tsAddOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsCreateNew;
        private System.Windows.Forms.ToolStripButton tsEdit;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsCancel;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.ToolStripMenuItem tsCompare;
        private System.Windows.Forms.ToolStripButton tsReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}