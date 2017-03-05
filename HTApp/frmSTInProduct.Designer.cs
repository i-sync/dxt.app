namespace HTApp
{
    partial class frmSTInProduct
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
            this.lblScanedNum = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInvStd = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCBatch = new System.Windows.Forms.TextBox();
            this.dtpValidDate = new System.Windows.Forms.DateTimePicker();
            this.dtpProDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCPosition = new System.Windows.Forms.TextBox();
            this.lblEnterprise = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtCChkCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkRegCode = new System.Windows.Forms.CheckBox();
            this.btnRegCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(174, 305);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 31);
            this.btnExit.TabIndex = 277;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblScanedNum
            // 
            this.lblScanedNum.Location = new System.Drawing.Point(156, 260);
            this.lblScanedNum.Name = "lblScanedNum";
            this.lblScanedNum.Size = new System.Drawing.Size(71, 20);
            this.lblScanedNum.Text = "123";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(109, 305);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(51, 31);
            this.btnSubmit.TabIndex = 276;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(15, 305);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(80, 31);
            this.btnDone.TabIndex = 275;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(115, 260);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 20);
            this.label12.Text = "已扫";
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(46, 280);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(65, 23);
            this.txtCount.TabIndex = 273;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 20);
            this.label11.Text = "数量：";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 20);
            this.label10.Text = "批次：";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 20);
            this.label9.Text = "有效期至：";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.Text = "生产日期：";
            // 
            // lblInvStd
            // 
            this.lblInvStd.Location = new System.Drawing.Point(73, 117);
            this.lblInvStd.Name = "lblInvStd";
            this.lblInvStd.Size = new System.Drawing.Size(153, 23);
            this.lblInvStd.Text = "10g/袋";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.Text = "规格：";
            // 
            // lblInvName
            // 
            this.lblInvName.Location = new System.Drawing.Point(73, 94);
            this.lblInvName.Name = "lblInvName";
            this.lblInvName.Size = new System.Drawing.Size(157, 23);
            this.lblInvName.Text = "人参";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.Text = "品名：";
            // 
            // txtRegCode
            // 
            this.txtRegCode.Enabled = false;
            this.txtRegCode.Location = new System.Drawing.Point(73, 25);
            this.txtRegCode.Name = "txtRegCode";
            this.txtRegCode.ReadOnly = true;
            this.txtRegCode.Size = new System.Drawing.Size(126, 23);
            this.txtRegCode.TabIndex = 272;
            this.txtRegCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLable_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.Text = "流通监管码";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.Location = new System.Drawing.Point(73, 2);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(153, 23);
            this.cmbWarehouse.TabIndex = 294;
            this.cmbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 20);
            this.label6.Text = "仓库名称";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Enabled = false;
            this.txtBarcode.Location = new System.Drawing.Point(73, 71);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(153, 23);
            this.txtBarcode.TabIndex = 297;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.Text = "成品标签：";
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(156, 284);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(75, 20);
            this.lblPrice.Text = "123";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(115, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 20);
            this.label8.Text = "金额";
            // 
            // txtCost
            // 
            this.txtCost.Enabled = false;
            this.txtCost.Location = new System.Drawing.Point(46, 257);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(65, 23);
            this.txtCost.TabIndex = 303;
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 260);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 20);
            this.label13.Text = "单价：";
            // 
            // txtCBatch
            // 
            this.txtCBatch.Enabled = false;
            this.txtCBatch.Location = new System.Drawing.Point(73, 211);
            this.txtCBatch.Name = "txtCBatch";
            this.txtCBatch.Size = new System.Drawing.Size(146, 23);
            this.txtCBatch.TabIndex = 320;
            this.txtCBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCBatch_KeyPress);
            // 
            // dtpValidDate
            // 
            this.dtpValidDate.Enabled = false;
            this.dtpValidDate.Location = new System.Drawing.Point(73, 187);
            this.dtpValidDate.Name = "dtpValidDate";
            this.dtpValidDate.Size = new System.Drawing.Size(146, 24);
            this.dtpValidDate.TabIndex = 321;
            this.dtpValidDate.ValueChanged += new System.EventHandler(this.dtpProDate_ValueChanged);
            // 
            // dtpProDate
            // 
            this.dtpProDate.Enabled = false;
            this.dtpProDate.Location = new System.Drawing.Point(73, 163);
            this.dtpProDate.Name = "dtpProDate";
            this.dtpProDate.Size = new System.Drawing.Size(146, 24);
            this.dtpProDate.TabIndex = 322;
            this.dtpProDate.ValueChanged += new System.EventHandler(this.dtpProDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.Text = "货位：";
            // 
            // txtCPosition
            // 
            this.txtCPosition.Enabled = false;
            this.txtCPosition.Location = new System.Drawing.Point(73, 48);
            this.txtCPosition.Name = "txtCPosition";
            this.txtCPosition.Size = new System.Drawing.Size(94, 23);
            this.txtCPosition.TabIndex = 273;
            this.txtCPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPosition_KeyPress);
            // 
            // lblEnterprise
            // 
            this.lblEnterprise.Location = new System.Drawing.Point(73, 141);
            this.lblEnterprise.Name = "lblEnterprise";
            this.lblEnterprise.Size = new System.Drawing.Size(167, 20);
            this.lblEnterprise.Text = "xx";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 20);
            this.label15.Text = "生产厂家：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(193, 50);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(27, 20);
            this.btnClear.TabIndex = 343;
            this.btnClear.Text = "清";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtCChkCode
            // 
            this.txtCChkCode.Enabled = false;
            this.txtCChkCode.Location = new System.Drawing.Point(73, 234);
            this.txtCChkCode.Name = "txtCChkCode";
            this.txtCChkCode.Size = new System.Drawing.Size(147, 23);
            this.txtCChkCode.TabIndex = 367;
            this.txtCChkCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCChkCode_KeyPress);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 237);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 20);
            this.label14.Text = "检验单号：";
            // 
            // chkRegCode
            // 
            this.chkRegCode.Location = new System.Drawing.Point(199, 28);
            this.chkRegCode.Name = "chkRegCode";
            this.chkRegCode.Size = new System.Drawing.Size(21, 20);
            this.chkRegCode.TabIndex = 388;
            this.chkRegCode.CheckStateChanged += new System.EventHandler(this.chkRegCode_CheckStateChanged);
            // 
            // btnRegCode
            // 
            this.btnRegCode.Enabled = false;
            this.btnRegCode.Location = new System.Drawing.Point(220, 28);
            this.btnRegCode.Name = "btnRegCode";
            this.btnRegCode.Size = new System.Drawing.Size(20, 20);
            this.btnRegCode.TabIndex = 389;
            this.btnRegCode.Text = "读";
            this.btnRegCode.Click += new System.EventHandler(this.btnRegCode_Click);
            // 
            // frmSTInProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 337);
            this.ControlBox = false;
            this.Controls.Add(this.btnRegCode);
            this.Controls.Add(this.chkRegCode);
            this.Controls.Add(this.txtCChkCode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblEnterprise);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dtpProDate);
            this.Controls.Add(this.dtpValidDate);
            this.Controls.Add(this.txtCBatch);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblScanedNum);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCPosition);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblInvStd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRegCode);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSTInProduct";
            this.Text = "产成品入库";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblScanedNum;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInvStd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCBatch;
        private System.Windows.Forms.DateTimePicker dtpValidDate;
        private System.Windows.Forms.DateTimePicker dtpProDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCPosition;
        private System.Windows.Forms.Label lblEnterprise;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtCChkCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkRegCode;
        private System.Windows.Forms.Button btnRegCode;
    }
}