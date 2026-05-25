namespace SMTT
{
    partial class frmProductionCheckLot
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.col_input_pocustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_input_marerialid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_input_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_production_created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_production_lotid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_production_materialnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_production_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_production_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outbound_created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outbound_lotid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outbound_materialnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outboundqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outbound_totalqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outboundinvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_outboundremarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_input_itemid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.col_input_lotid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveTrans = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.updSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblProcess = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.col_outbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // col_input_pocustomer
            // 
            this.col_input_pocustomer.DataPropertyName = "Input_POCustomer";
            this.col_input_pocustomer.HeaderText = "POCustomer";
            this.col_input_pocustomer.Name = "col_input_pocustomer";
            // 
            // col_input_marerialid
            // 
            this.col_input_marerialid.DataPropertyName = "Input_MaterialID";
            this.col_input_marerialid.HeaderText = "MaterialID";
            this.col_input_marerialid.Name = "col_input_marerialid";
            // 
            // col_input_qty
            // 
            this.col_input_qty.DataPropertyName = "Input_Qty";
            this.col_input_qty.FillWeight = 75F;
            this.col_input_qty.HeaderText = "Qty";
            this.col_input_qty.Name = "col_input_qty";
            this.col_input_qty.Width = 75;
            // 
            // col_production_created
            // 
            this.col_production_created.DataPropertyName = "Production_Created";
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Honeydew;
            this.col_production_created.DefaultCellStyle = dataGridViewCellStyle15;
            this.col_production_created.HeaderText = "Created";
            this.col_production_created.Name = "col_production_created";
            this.col_production_created.Width = 75;
            // 
            // col_production_lotid
            // 
            this.col_production_lotid.DataPropertyName = "Production_LotID";
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Honeydew;
            this.col_production_lotid.DefaultCellStyle = dataGridViewCellStyle16;
            this.col_production_lotid.HeaderText = "LotID";
            this.col_production_lotid.Name = "col_production_lotid";
            this.col_production_lotid.Width = 140;
            // 
            // col_production_materialnumber
            // 
            this.col_production_materialnumber.DataPropertyName = "Production_MaterialNumber";
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Honeydew;
            this.col_production_materialnumber.DefaultCellStyle = dataGridViewCellStyle17;
            this.col_production_materialnumber.HeaderText = "MaterialNumber";
            this.col_production_materialnumber.Name = "col_production_materialnumber";
            // 
            // col_production_qty
            // 
            this.col_production_qty.DataPropertyName = "Production_Qty";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Honeydew;
            this.col_production_qty.DefaultCellStyle = dataGridViewCellStyle18;
            this.col_production_qty.HeaderText = "Qty";
            this.col_production_qty.Name = "col_production_qty";
            this.col_production_qty.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Production_TotalQty";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle19.Format = "#,##0.000";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle19;
            this.Column3.FillWeight = 200F;
            this.Column3.HeaderText = "TotalQty";
            this.Column3.Name = "Column3";
            this.Column3.Width = 75;
            // 
            // col_production_date
            // 
            this.col_production_date.DataPropertyName = "Production_Date";
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Honeydew;
            this.col_production_date.DefaultCellStyle = dataGridViewCellStyle20;
            this.col_production_date.HeaderText = "Date";
            this.col_production_date.Name = "col_production_date";
            this.col_production_date.Width = 75;
            // 
            // col_outbound_created
            // 
            this.col_outbound_created.DataPropertyName = "Outbound_Created";
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outbound_created.DefaultCellStyle = dataGridViewCellStyle21;
            this.col_outbound_created.HeaderText = "Created";
            this.col_outbound_created.Name = "col_outbound_created";
            this.col_outbound_created.Width = 75;
            // 
            // col_outbound_lotid
            // 
            this.col_outbound_lotid.DataPropertyName = "OutBound_LotID";
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outbound_lotid.DefaultCellStyle = dataGridViewCellStyle22;
            this.col_outbound_lotid.HeaderText = "LotID";
            this.col_outbound_lotid.Name = "col_outbound_lotid";
            this.col_outbound_lotid.Width = 140;
            // 
            // col_outbound_materialnumber
            // 
            this.col_outbound_materialnumber.DataPropertyName = "Outbound_MaterialNumber";
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outbound_materialnumber.DefaultCellStyle = dataGridViewCellStyle23;
            this.col_outbound_materialnumber.HeaderText = "MaterialNumber";
            this.col_outbound_materialnumber.Name = "col_outbound_materialnumber";
            // 
            // col_outboundqty
            // 
            this.col_outboundqty.DataPropertyName = "Outbound_Qty";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outboundqty.DefaultCellStyle = dataGridViewCellStyle24;
            this.col_outboundqty.FillWeight = 400F;
            this.col_outboundqty.HeaderText = "Qty";
            this.col_outboundqty.Name = "col_outboundqty";
            this.col_outboundqty.Width = 60;
            // 
            // col_outbound_totalqty
            // 
            this.col_outbound_totalqty.DataPropertyName = "Outbound_TotalQty";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle25.Format = "#,##0.00";
            this.col_outbound_totalqty.DefaultCellStyle = dataGridViewCellStyle25;
            this.col_outbound_totalqty.HeaderText = "TotalQty";
            this.col_outbound_totalqty.Name = "col_outbound_totalqty";
            this.col_outbound_totalqty.Width = 75;
            // 
            // col_outboundinvoice
            // 
            this.col_outboundinvoice.DataPropertyName = "Outbound_Invoice";
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outboundinvoice.DefaultCellStyle = dataGridViewCellStyle26;
            this.col_outboundinvoice.HeaderText = "Invoice";
            this.col_outboundinvoice.Name = "col_outboundinvoice";
            this.col_outboundinvoice.Width = 200;
            // 
            // col_outboundremarks
            // 
            this.col_outboundremarks.DataPropertyName = "Outbound_Remarks";
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outboundremarks.DefaultCellStyle = dataGridViewCellStyle27;
            this.col_outboundremarks.HeaderText = "Remarks";
            this.col_outboundremarks.Name = "col_outboundremarks";
            this.col_outboundremarks.Width = 120;
            // 
            // col_input_itemid
            // 
            this.col_input_itemid.DataPropertyName = "Input_ItemCode";
            this.col_input_itemid.HeaderText = "ItemCode";
            this.col_input_itemid.Name = "col_input_itemid";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // col_input_lotid
            // 
            this.col_input_lotid.DataPropertyName = "Input_LotID";
            this.col_input_lotid.FillWeight = 150F;
            this.col_input_lotid.HeaderText = "LotID";
            this.col_input_lotid.Name = "col_input_lotid";
            this.col_input_lotid.Width = 140;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnRemoveTrans);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.updSpeed);
            this.groupBox1.Controls.Add(this.lblProcess);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(270, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(819, 203);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // btnRemoveTrans
            // 
            this.btnRemoveTrans.Enabled = false;
            this.btnRemoveTrans.Location = new System.Drawing.Point(547, 152);
            this.btnRemoveTrans.Name = "btnRemoveTrans";
            this.btnRemoveTrans.Size = new System.Drawing.Size(88, 23);
            this.btnRemoveTrans.TabIndex = 127;
            this.btnRemoveTrans.Text = "Xóa bản nháp";
            this.btnRemoveTrans.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(286, 100);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(79, 23);
            this.btnCopy.TabIndex = 126;
            this.btnCopy.Text = "Sao chép";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(72, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(144, 20);
            this.txtPassword.TabIndex = 29;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(72, 13);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(144, 20);
            this.txtUsername.TabIndex = 30;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(127, 103);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(153, 18);
            this.progressBar1.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Password";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 100);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 23);
            this.btnStart.TabIndex = 23;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Speed";
            // 
            // updSpeed
            // 
            this.updSpeed.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.updSpeed.Location = new System.Drawing.Point(57, 74);
            this.updSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updSpeed.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.updSpeed.Name = "updSpeed";
            this.updSpeed.Size = new System.Drawing.Size(61, 20);
            this.updSpeed.TabIndex = 19;
            this.updSpeed.ThousandsSeparator = true;
            this.updSpeed.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(195, 81);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(13, 13);
            this.lblProcess.TabIndex = 18;
            this.lblProcess.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Process:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 6);
            this.textBox1.MaxLength = 3276700;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(258, 123);
            this.textBox1.TabIndex = 12;
            // 
            // col_outbound
            // 
            this.col_outbound.DataPropertyName = "OutBound_Date";
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.col_outbound.DefaultCellStyle = dataGridViewCellStyle28;
            this.col_outbound.HeaderText = "Date";
            this.col_outbound.Name = "col_outbound";
            this.col_outbound.Width = 75;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_input_lotid,
            this.col_input_pocustomer,
            this.col_input_itemid,
            this.col_input_marerialid,
            this.col_input_qty,
            this.col_production_created,
            this.col_production_lotid,
            this.col_production_materialnumber,
            this.col_production_qty,
            this.Column3,
            this.col_production_date,
            this.col_outbound_created,
            this.col_outbound_lotid,
            this.col_outbound_materialnumber,
            this.col_outboundqty,
            this.col_outbound_totalqty,
            this.col_outboundinvoice,
            this.col_outboundremarks,
            this.col_outbound});
            this.dataGridView1.Location = new System.Drawing.Point(9, 239);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1494, 402);
            this.dataGridView1.TabIndex = 13;
            // 
            // frmProductionCheckLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmProductionCheckLot";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmProductionCheckLot";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn col_input_pocustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_input_marerialid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_input_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_production_created;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_production_lotid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_production_materialnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_production_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_production_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outbound_created;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outbound_lotid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outbound_materialnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outboundqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outbound_totalqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outboundinvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outboundremarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_input_itemid;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_input_lotid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveTrans;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown updSpeed;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_outbound;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}