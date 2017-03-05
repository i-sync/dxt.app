namespace HTApp
{
    partial class frmSaleOutPicking
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.lblScanedNum = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblValidDate = new System.Windows.Forms.Label();
            this.lblProDate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblProAddress = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblInvStd = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBatch = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCPosition = new System.Windows.Forms.TextBox();
            this.lblEnterprise = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRegCode = new System.Windows.Forms.Button();
            this.chkRegCode = new System.Windows.Forms.CheckBox();
            this.txtRegCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(187, 300);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 34);
            this.btnExit.TabIndex = 206;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(135, 300);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 34);
            this.btnSubmit.TabIndex = 205;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(69, 300);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(65, 34);
            this.btnDone.TabIndex = 204;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(3, 300);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(65, 34);
            this.btnSource.TabIndex = 203;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lblScanedNum
            // 
            this.lblScanedNum.Location = new System.Drawing.Point(190, 277);
            this.lblScanedNum.Name = "lblScanedNum";
            this.lblScanedNum.Size = new System.Drawing.Size(38, 20);
            this.lblScanedNum.Text = "123";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(127, 277);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 20);
            this.label12.Text = "已扫描：";
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(71, 277);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(55, 23);
            this.txtCount.TabIndex = 202;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 277);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.Text = "数量：";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.Text = "批次：";
            // 
            // lblValidDate
            // 
            this.lblValidDate.Location = new System.Drawing.Point(71, 231);
            this.lblValidDate.Name = "lblValidDate";
            this.lblValidDate.Size = new System.Drawing.Size(157, 20);
            this.lblValidDate.Text = "20150101";
            // 
            // lblProDate
            // 
            this.lblProDate.Location = new System.Drawing.Point(71, 208);
            this.lblProDate.Name = "lblProDate";
            this.lblProDate.Size = new System.Drawing.Size(157, 20);
            this.lblProDate.Text = "20120101";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.Text = "有效期至：";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.Text = "生产日期：";
            // 
            // lblProAddress
            // 
            this.lblProAddress.Location = new System.Drawing.Point(71, 169);
            this.lblProAddress.Name = "lblProAddress";
            this.lblProAddress.Size = new System.Drawing.Size(157, 20);
            this.lblProAddress.Text = "XX";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "产地：";
            // 
            // lblInvStd
            // 
            this.lblInvStd.Location = new System.Drawing.Point(75, 146);
            this.lblInvStd.Name = "lblInvStd";
            this.lblInvStd.Size = new System.Drawing.Size(153, 20);
            this.lblInvStd.Text = "10g/袋";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "规格：";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(71, 123);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(157, 20);
            this.lblInvName.Text = "人参";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.Text = "品名：";
            // 
            // txtLable
            // 
            this.txtLable.Enabled = false;
            this.txtLable.Location = new System.Drawing.Point(75, 100);
            this.txtLable.Name = "txtLable";
            this.txtLable.Size = new System.Drawing.Size(153, 23);
            this.txtLable.TabIndex = 200;
            this.txtLable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLable_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.Text = "成品标签：";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(75, 2);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(134, 23);
            this.txtSource.TabIndex = 199;
            this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.Text = "销售订单：";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.Location = new System.Drawing.Point(75, 52);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(153, 23);
            this.cmbWarehouse.TabIndex = 228;
            this.cmbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.Text = "仓库名称";
            // 
            // cmbBatch
            // 
            this.cmbBatch.Enabled = false;
            this.cmbBatch.Location = new System.Drawing.Point(71, 254);
            this.cmbBatch.Name = "cmbBatch";
            this.cmbBatch.Size = new System.Drawing.Size(157, 23);
            this.cmbBatch.TabIndex = 246;
            this.cmbBatch.SelectedIndexChanged += new System.EventHandler(this.cmbBatch_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.Text = "货位";
            // 
            // txtCPosition
            // 
            this.txtCPosition.Enabled = false;
            this.txtCPosition.Location = new System.Drawing.Point(75, 76);
            this.txtCPosition.Name = "txtCPosition";
            this.txtCPosition.Size = new System.Drawing.Size(112, 23);
            this.txtCPosition.TabIndex = 200;
            this.txtCPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPosition_KeyPress);
            // 
            // lblEnterprise
            // 
            this.lblEnterprise.Location = new System.Drawing.Point(72, 189);
            this.lblEnterprise.Name = "lblEnterprise";
            this.lblEnterprise.Size = new System.Drawing.Size(160, 20);
            this.lblEnterprise.Text = "XX";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 20);
            this.label14.Text = "生产厂家：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(210, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(27, 20);
            this.btnClear.TabIndex = 333;
            this.btnClear.Text = "清";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRegCode
            // 
            this.btnRegCode.Enabled = false;
            this.btnRegCode.Location = new System.Drawing.Point(220, 30);
            this.btnRegCode.Name = "btnRegCode";
            this.btnRegCode.Size = new System.Drawing.Size(20, 20);
            this.btnRegCode.TabIndex = 393;
            this.btnRegCode.Text = "读";
            this.btnRegCode.Click += new System.EventHandler(this.btnRegCode_Click);
            // 
            // chkRegCode
            // 
            this.chkRegCode.Location = new System.Drawing.Point(199, 30);
            this.chkRegCode.Name = "chkRegCode";
            this.chkRegCode.Size = new System.Drawing.Size(21, 20);
            this.chkRegCode.TabIndex = 392;
            this.chkRegCode.CheckStateChanged += new System.EventHandler(this.chkRegCode_CheckStateChanged);
            // 
            // txtRegCode
            // 
            this.txtRegCode.Enabled = false;
            this.txtRegCode.Location = new System.Drawing.Point(73, 27);
            this.txtRegCode.Name = "txtRegCode";
            this.txtRegCode.ReadOnly = true;
            this.txtRegCode.Size = new System.Drawing.Size(126, 23);
            this.txtRegCode.TabIndex = 391;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.Text = "流通监管码";
            // 
            // frmSaleOutPicking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 337);
            this.ControlBox = false;
            this.Controls.Add(this.btnRegCode);
            this.Controls.Add(this.chkRegCode);
            this.Controls.Add(this.txtRegCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblEnterprise);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbBatch);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.lblScanedNum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblValidDate);
            this.Controls.Add(this.lblProDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblProAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblInvStd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCPosition);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label1);
            this.Name = "frmSaleOutPicking";
            this.Text = "销售出库拣货扫描";
            this.Load += new System.EventHandler(this.frmSaleOutPicking_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lblScanedNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblValidDate;
        private System.Windows.Forms.Label lblProDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblProAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInvStd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbBatch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCPosition;
        private System.Windows.Forms.Label lblEnterprise;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRegCode;
        private System.Windows.Forms.CheckBox chkRegCode;
        private System.Windows.Forms.TextBox txtRegCode;
        private System.Windows.Forms.Label label13;
    }
}