namespace HTApp
{
    partial class frmSaleOutGSP
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
            this.label10 = new System.Windows.Forms.Label();
            this.lblcBatch = new System.Windows.Forms.Label();
            this.cmbCBatch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbTypeCHM = new System.Windows.Forms.RadioButton();
            this.rbTypeCommon = new System.Windows.Forms.RadioButton();
            this.lblEnterprise = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbCresult = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(187, 294);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 36);
            this.btnExit.TabIndex = 253;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(135, 294);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 36);
            this.btnSubmit.TabIndex = 252;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(69, 294);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(65, 36);
            this.btnDone.TabIndex = 251;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(3, 294);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(65, 36);
            this.btnSource.TabIndex = 250;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lblScanedNum
            // 
            this.lblScanedNum.Location = new System.Drawing.Point(198, 271);
            this.lblScanedNum.Name = "lblScanedNum";
            this.lblScanedNum.Size = new System.Drawing.Size(38, 20);
            this.lblScanedNum.Text = "123";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(135, 271);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 20);
            this.label12.Text = "已扫描：";
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(76, 270);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(55, 23);
            this.txtCount.TabIndex = 249;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 271);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.Text = "数量：";
            // 
            // lblValidDate
            // 
            this.lblValidDate.Location = new System.Drawing.Point(76, 220);
            this.lblValidDate.Name = "lblValidDate";
            this.lblValidDate.Size = new System.Drawing.Size(157, 20);
            this.lblValidDate.Text = "20150101";
            // 
            // lblProDate
            // 
            this.lblProDate.Location = new System.Drawing.Point(76, 198);
            this.lblProDate.Name = "lblProDate";
            this.lblProDate.Size = new System.Drawing.Size(157, 20);
            this.lblProDate.Text = "20120101";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.Text = "有效期至：";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.Text = "生产日期：";
            // 
            // lblProAddress
            // 
            this.lblProAddress.Location = new System.Drawing.Point(76, 130);
            this.lblProAddress.Name = "lblProAddress";
            this.lblProAddress.Size = new System.Drawing.Size(157, 20);
            this.lblProAddress.Text = "XX";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "产地：";
            // 
            // lblInvStd
            // 
            this.lblInvStd.Location = new System.Drawing.Point(76, 105);
            this.lblInvStd.Name = "lblInvStd";
            this.lblInvStd.Size = new System.Drawing.Size(153, 20);
            this.lblInvStd.Text = "10g/袋";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "规格：";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(76, 80);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(157, 20);
            this.lblInvName.Text = "人参";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.Text = "品名：";
            // 
            // txtLable
            // 
            this.txtLable.Enabled = false;
            this.txtLable.Location = new System.Drawing.Point(76, 52);
            this.txtLable.Name = "txtLable";
            this.txtLable.Size = new System.Drawing.Size(153, 23);
            this.txtLable.TabIndex = 247;
            this.txtLable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLable_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.Text = "成品标签：";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(76, 4);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(136, 23);
            this.txtSource.TabIndex = 246;
            this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.Text = "发货单：";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.Text = "批次：";
            // 
            // lblcBatch
            // 
            this.lblcBatch.Location = new System.Drawing.Point(76, 245);
            this.lblcBatch.Name = "lblcBatch";
            this.lblcBatch.Size = new System.Drawing.Size(157, 20);
            this.lblcBatch.Text = "20150101";
            // 
            // cmbCBatch
            // 
            this.cmbCBatch.Location = new System.Drawing.Point(76, 243);
            this.cmbCBatch.Name = "cmbCBatch";
            this.cmbCBatch.Size = new System.Drawing.Size(153, 23);
            this.cmbCBatch.TabIndex = 270;
            this.cmbCBatch.Visible = false;
            this.cmbCBatch.SelectedIndexChanged += new System.EventHandler(this.cmbCBatch_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.Text = "生单类型";
            // 
            // rbTypeCHM
            // 
            this.rbTypeCHM.Checked = true;
            this.rbTypeCHM.Location = new System.Drawing.Point(76, 29);
            this.rbTypeCHM.Name = "rbTypeCHM";
            this.rbTypeCHM.Size = new System.Drawing.Size(110, 20);
            this.rbTypeCHM.TabIndex = 289;
            this.rbTypeCHM.Text = "中药材/饮片";
            // 
            // rbTypeCommon
            // 
            this.rbTypeCommon.Location = new System.Drawing.Point(180, 29);
            this.rbTypeCommon.Name = "rbTypeCommon";
            this.rbTypeCommon.Size = new System.Drawing.Size(58, 20);
            this.rbTypeCommon.TabIndex = 290;
            this.rbTypeCommon.TabStop = false;
            this.rbTypeCommon.Text = "普通";
            // 
            // lblEnterprise
            // 
            this.lblEnterprise.Location = new System.Drawing.Point(76, 151);
            this.lblEnterprise.Name = "lblEnterprise";
            this.lblEnterprise.Size = new System.Drawing.Size(157, 20);
            this.lblEnterprise.Text = "XX";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 151);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.Text = "生产厂家：";
            // 
            // cmbCresult
            // 
            this.cmbCresult.Items.Add("合格");
            this.cmbCresult.Items.Add("不合格");
            this.cmbCresult.Location = new System.Drawing.Point(76, 171);
            this.cmbCresult.Name = "cmbCresult";
            this.cmbCresult.Size = new System.Drawing.Size(153, 23);
            this.cmbCresult.TabIndex = 310;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.Text = "质量情况：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(213, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(27, 20);
            this.btnClear.TabIndex = 332;
            this.btnClear.Text = "清";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmSaleOutGSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 333);
            this.ControlBox = false;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cmbCresult);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblEnterprise);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rbTypeCommon);
            this.Controls.Add(this.rbTypeCHM);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbCBatch);
            this.Controls.Add(this.lblcBatch);
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
            this.Controls.Add(this.txtLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSaleOutGSP";
            this.Text = "销售出库GSP检验扫描";
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblcBatch;
        private System.Windows.Forms.ComboBox cmbCBatch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbTypeCHM;
        private System.Windows.Forms.RadioButton rbTypeCommon;
        private System.Windows.Forms.Label lblEnterprise;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbCresult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClear;
    }
}