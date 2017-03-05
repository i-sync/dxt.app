namespace HTApp
{
    partial class frmCheck
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbSourceNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodebar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEnterprise = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProAddress = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCBatch = new System.Windows.Forms.ComboBox();
            this.lblProDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblValidDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbSourceNo
            // 
            this.cmbSourceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbSourceNo.Location = new System.Drawing.Point(67, 4);
            this.cmbSourceNo.Name = "cmbSourceNo";
            this.cmbSourceNo.Size = new System.Drawing.Size(153, 23);
            this.cmbSourceNo.TabIndex = 0;
            this.cmbSourceNo.SelectedIndexChanged += new System.EventHandler(this.cmbSourceNo_SelectedIndexChanged);
            this.cmbSourceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSourceNo_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.Text = "仓库名称";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Enabled = false;
            this.txtQuantity.Location = new System.Drawing.Point(67, 269);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(80, 23);
            this.txtQuantity.TabIndex = 307;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 20);
            this.label11.Text = "数量：";
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(67, 245);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(153, 20);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "账面数量";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(67, 101);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(157, 20);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.Text = "品名：";
            // 
            // txtCodebar
            // 
            this.txtCodebar.Enabled = false;
            this.txtCodebar.Location = new System.Drawing.Point(67, 76);
            this.txtCodebar.Name = "txtCodebar";
            this.txtCodebar.Size = new System.Drawing.Size(153, 23);
            this.txtCodebar.TabIndex = 306;
            this.txtCodebar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodebar_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "成品标签";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "盘点单号";
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.Location = new System.Drawing.Point(67, 31);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(157, 20);
            this.lblWarehouse.Text = "仓库";
            // 
            // btnDetail
            // 
            this.btnDetail.Enabled = false;
            this.btnDetail.Location = new System.Drawing.Point(24, 294);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(79, 36);
            this.btnDetail.TabIndex = 322;
            this.btnDetail.Text = "已扫数据";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(166, 294);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 36);
            this.btnExit.TabIndex = 324;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(109, 294);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 36);
            this.btnSubmit.TabIndex = 323;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 20);
            this.label10.Text = "批次：";
            // 
            // lblEnterprise
            // 
            this.lblEnterprise.Location = new System.Drawing.Point(67, 221);
            this.lblEnterprise.Name = "lblEnterprise";
            this.lblEnterprise.Size = new System.Drawing.Size(173, 20);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.Text = "生产厂家";
            // 
            // txtCPosition
            // 
            this.txtCPosition.Enabled = false;
            this.txtCPosition.Location = new System.Drawing.Point(67, 52);
            this.txtCPosition.Name = "txtCPosition";
            this.txtCPosition.Size = new System.Drawing.Size(89, 23);
            this.txtCPosition.TabIndex = 335;
            this.txtCPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPosition_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.Text = "货位";
            // 
            // lblProAddress
            // 
            this.lblProAddress.Location = new System.Drawing.Point(67, 197);
            this.lblProAddress.Name = "lblProAddress";
            this.lblProAddress.Size = new System.Drawing.Size(170, 20);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.Text = "产地：";
            // 
            // cmbCBatch
            // 
            this.cmbCBatch.Enabled = false;
            this.cmbCBatch.Location = new System.Drawing.Point(67, 171);
            this.cmbCBatch.Name = "cmbCBatch";
            this.cmbCBatch.Size = new System.Drawing.Size(153, 23);
            this.cmbCBatch.TabIndex = 341;
            this.cmbCBatch.SelectedIndexChanged += new System.EventHandler(this.cmbCBatch_SelectedIndexChanged);
            // 
            // lblProDate
            // 
            this.lblProDate.Location = new System.Drawing.Point(67, 125);
            this.lblProDate.Name = "lblProDate";
            this.lblProDate.Size = new System.Drawing.Size(157, 20);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 20);
            this.label12.Text = "生产日期";
            // 
            // lblValidDate
            // 
            this.lblValidDate.Location = new System.Drawing.Point(67, 149);
            this.lblValidDate.Name = "lblValidDate";
            this.lblValidDate.Size = new System.Drawing.Size(157, 20);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 20);
            this.label14.Text = "有效期至";
            // 
            // frmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 332);
            this.ControlBox = false;
            this.Controls.Add(this.lblValidDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblProDate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbCBatch);
            this.Controls.Add(this.lblProAddress);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCPosition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblEnterprise);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.lblWarehouse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSourceNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCodebar);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheck";
            this.Text = "盘点";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSourceNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodebar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEnterprise;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCBatch;
        private System.Windows.Forms.Label lblProDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblValidDate;
        private System.Windows.Forms.Label label14;
    }
}