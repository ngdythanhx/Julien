namespace TyXuan
{
    partial class frmThuDamBao
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDRDefectCode = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTesting = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblArt = new System.Windows.Forms.Label();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDRDefectCode = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtTesting = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtArt = new System.Windows.Forms.TextBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(250, 40);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(131, 42);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(113, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Location = new System.Drawing.Point(6, 290);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 69);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tạo file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Template";
            // 
            // lblDRDefectCode
            // 
            this.lblDRDefectCode.AutoSize = true;
            this.lblDRDefectCode.Location = new System.Drawing.Point(642, 167);
            this.lblDRDefectCode.Name = "lblDRDefectCode";
            this.lblDRDefectCode.Size = new System.Drawing.Size(83, 13);
            this.lblDRDefectCode.TabIndex = 12;
            this.lblDRDefectCode.Text = "DR defect code";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(536, 167);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(37, 13);
            this.lblResult.TabIndex = 13;
            this.lblResult.Text = "Result";
            // 
            // lblTesting
            // 
            this.lblTesting.AutoSize = true;
            this.lblTesting.Location = new System.Drawing.Point(430, 167);
            this.lblTesting.Name = "lblTesting";
            this.lblTesting.Size = new System.Drawing.Size(42, 13);
            this.lblTesting.TabIndex = 14;
            this.lblTesting.Text = "Testing";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(324, 167);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(46, 13);
            this.lblQty.TabIndex = 15;
            this.lblQty.Text = "Quantity";
            // 
            // lblArt
            // 
            this.lblArt.AutoSize = true;
            this.lblArt.Location = new System.Drawing.Point(218, 167);
            this.lblArt.Name = "lblArt";
            this.lblArt.Size = new System.Drawing.Size(29, 13);
            this.lblArt.TabIndex = 16;
            this.lblArt.Text = "ART";
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Location = new System.Drawing.Point(112, 167);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(71, 13);
            this.lblOrderNumber.TabIndex = 17;
            this.lblOrderNumber.Text = "Order number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Material";
            // 
            // txtDRDefectCode
            // 
            this.txtDRDefectCode.Location = new System.Drawing.Point(642, 183);
            this.txtDRDefectCode.Multiline = true;
            this.txtDRDefectCode.Name = "txtDRDefectCode";
            this.txtDRDefectCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDRDefectCode.Size = new System.Drawing.Size(100, 100);
            this.txtDRDefectCode.TabIndex = 5;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(536, 183);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(100, 100);
            this.txtResult.TabIndex = 6;
            // 
            // txtTesting
            // 
            this.txtTesting.Location = new System.Drawing.Point(430, 183);
            this.txtTesting.Multiline = true;
            this.txtTesting.Name = "txtTesting";
            this.txtTesting.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTesting.Size = new System.Drawing.Size(100, 100);
            this.txtTesting.TabIndex = 7;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(324, 183);
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQty.Size = new System.Drawing.Size(100, 100);
            this.txtQty.TabIndex = 8;
            // 
            // txtArt
            // 
            this.txtArt.Location = new System.Drawing.Point(218, 183);
            this.txtArt.Multiline = true;
            this.txtArt.Name = "txtArt";
            this.txtArt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArt.Size = new System.Drawing.Size(100, 100);
            this.txtArt.TabIndex = 9;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(112, 183);
            this.txtOrderNumber.Multiline = true;
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOrderNumber.Size = new System.Drawing.Size(100, 100);
            this.txtOrderNumber.TabIndex = 10;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Location = new System.Drawing.Point(6, 183);
            this.txtMaterial.Multiline = true;
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMaterial.Size = new System.Drawing.Size(100, 100);
            this.txtMaterial.TabIndex = 11;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(6, 6);
            this.txtInput.MaxLength = 3276700;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(736, 150);
            this.txtInput.TabIndex = 4;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(33, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Filename";
            // 
            // frmThuDamBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 364);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDRDefectCode);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblTesting);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblArt);
            this.Controls.Add(this.lblOrderNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDRDefectCode);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtTesting);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtArt);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.txtMaterial);
            this.Controls.Add(this.txtInput);
            this.Name = "frmThuDamBao";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmThuDamBao";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDRDefectCode;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTesting;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblArt;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDRDefectCode;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtTesting;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtArt;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}