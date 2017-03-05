namespace HTApp
{
    partial class frmPurchaseArrival
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.dtpValidDate = new System.Windows.Forms.DateTimePicker();
            this.dtpProDate = new System.Windows.Forms.DateTimePicker();
            this.chkChinese = new System.Windows.Forms.CheckBox();
            this.dtpChineseDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoNoCheck = new System.Windows.Forms.RadioButton();
            this.rdoCheck = new System.Windows.Forms.RadioButton();
            this.lblScanNum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.lblcInvStd = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblcInvName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderCode = new System.Windows.Forms.TextBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBarcode
            // 
            this.txtBarcode.Enabled = false;
            this.txtBarcode.Location = new System.Drawing.Point(72, 73);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(165, 23);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(1, 76);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(67, 16);
            this.lblLabel.Text = "物料标签:";
            // 
            // dtpValidDate
            // 
            this.dtpValidDate.CustomFormat = "";
            this.dtpValidDate.Location = new System.Drawing.Point(72, 216);
            this.dtpValidDate.Name = "dtpValidDate";
            this.dtpValidDate.Size = new System.Drawing.Size(165, 24);
            this.dtpValidDate.TabIndex = 6;
            this.dtpValidDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // dtpProDate
            // 
            this.dtpProDate.CustomFormat = "";
            this.dtpProDate.Location = new System.Drawing.Point(72, 191);
            this.dtpProDate.Name = "dtpProDate";
            this.dtpProDate.Size = new System.Drawing.Size(165, 24);
            this.dtpProDate.TabIndex = 5;
            this.dtpProDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // chkChinese
            // 
            this.chkChinese.Location = new System.Drawing.Point(218, 170);
            this.chkChinese.Name = "chkChinese";
            this.chkChinese.Size = new System.Drawing.Size(20, 20);
            this.chkChinese.TabIndex = 4;
            this.chkChinese.CheckStateChanged += new System.EventHandler(this.chkChinese_CheckStateChanged);
            // 
            // dtpChineseDate
            // 
            this.dtpChineseDate.CustomFormat = "";
            this.dtpChineseDate.Location = new System.Drawing.Point(72, 166);
            this.dtpChineseDate.Name = "dtpChineseDate";
            this.dtpChineseDate.Size = new System.Drawing.Size(139, 24);
            this.dtpChineseDate.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 16);
            this.label11.Text = "中 成 药:";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.Enabled = false;
            this.cmbWarehouse.Location = new System.Drawing.Point(72, 48);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(165, 23);
            this.cmbWarehouse.TabIndex = 1;
            this.cmbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(1, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 16);
            this.label12.Text = "入库仓库:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.Text = "有效期至:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.Text = "生产日期:";
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(212, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(26, 20);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.Text = "是否质检:";
            // 
            // rdoNoCheck
            // 
            this.rdoNoCheck.Checked = true;
            this.rdoNoCheck.Enabled = false;
            this.rdoNoCheck.Location = new System.Drawing.Point(157, 29);
            this.rdoNoCheck.Name = "rdoNoCheck";
            this.rdoNoCheck.Size = new System.Drawing.Size(70, 20);
            this.rdoNoCheck.TabIndex = 350;
            this.rdoNoCheck.Text = "非质检";
            // 
            // rdoCheck
            // 
            this.rdoCheck.Enabled = false;
            this.rdoCheck.Location = new System.Drawing.Point(72, 28);
            this.rdoCheck.Name = "rdoCheck";
            this.rdoCheck.Size = new System.Drawing.Size(63, 21);
            this.rdoCheck.TabIndex = 349;
            this.rdoCheck.TabStop = false;
            this.rdoCheck.Text = "质检";
            // 
            // lblScanNum
            // 
            this.lblScanNum.Location = new System.Drawing.Point(182, 269);
            this.lblScanNum.Name = "lblScanNum";
            this.lblScanNum.Size = new System.Drawing.Size(54, 17);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(140, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 16);
            this.label7.Text = "已扫：";
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Location = new System.Drawing.Point(72, 241);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(165, 23);
            this.txtBatch.TabIndex = 7;
            this.txtBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBatch_KeyPress);
            // 
            // lblcInvStd
            // 
            this.lblcInvStd.Location = new System.Drawing.Point(72, 122);
            this.lblcInvStd.Name = "lblcInvStd";
            this.lblcInvStd.Size = new System.Drawing.Size(164, 23);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(1, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 16);
            this.label9.Text = "规    格:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(188, 289);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 35);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(137, 289);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(50, 35);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(69, 289);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(67, 35);
            this.btnDone.TabIndex = 10;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(1, 289);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(67, 35);
            this.btnSource.TabIndex = 9;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(72, 265);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(68, 23);
            this.txtCount.TabIndex = 8;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(1, 269);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 16);
            this.label8.Text = "数    量:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.Text = "批    次:";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(72, 144);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(165, 23);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.Text = "产    地:";
            // 
            // lblcInvName
            // 
            this.lblcInvName.Location = new System.Drawing.Point(72, 101);
            this.lblcInvName.Name = "lblcInvName";
            this.lblcInvName.Size = new System.Drawing.Size(164, 23);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.Text = "品    称:";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Location = new System.Drawing.Point(72, 3);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(134, 23);
            this.txtOrderCode.TabIndex = 0;
            this.txtOrderCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderCode_KeyPress);
            // 
            // lblOrder
            // 
            this.lblOrder.Location = new System.Drawing.Point(1, 6);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(67, 16);
            this.lblOrder.Text = "采购订单:";
            // 
            // frmPurchaseArrival
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 326);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.dtpValidDate);
            this.Controls.Add(this.dtpProDate);
            this.Controls.Add(this.chkChinese);
            this.Controls.Add(this.dtpChineseDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoNoCheck);
            this.Controls.Add(this.rdoCheck);
            this.Controls.Add(this.lblScanNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.lblcInvStd);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblcInvName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrderCode);
            this.Controls.Add(this.lblOrder);
            this.Menu = this.mainMenu1;
            this.Name = "frmPurchaseArrival";
            this.Text = "frmPurchaseArrival";
            this.Load += new System.EventHandler(this.frmPurchaseArrival_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.DateTimePicker dtpValidDate;
        private System.Windows.Forms.DateTimePicker dtpProDate;
        private System.Windows.Forms.CheckBox chkChinese;
        private System.Windows.Forms.DateTimePicker dtpChineseDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoNoCheck;
        private System.Windows.Forms.RadioButton rdoCheck;
        private System.Windows.Forms.Label lblScanNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label lblcInvStd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblcInvName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label lblOrder;
    }
}