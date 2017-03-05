namespace HTApp
{
    partial class frmOSArrival
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
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.dtpValityDate = new System.Windows.Forms.DateTimePicker();
            this.dtpProDate = new System.Windows.Forms.DateTimePicker();
            this.cbChinese = new System.Windows.Forms.CheckBox();
            this.dtpChineseDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxWareHouse = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InputPannel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtNoCheck = new System.Windows.Forms.RadioButton();
            this.rbtCheck = new System.Windows.Forms.RadioButton();
            this.lblDoneNum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.lblStandard = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAddrCode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLabel
            // 
            this.txtLabel.Enabled = false;
            this.txtLabel.Location = new System.Drawing.Point(72, 71);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(165, 23);
            this.txtLabel.TabIndex = 357;
            this.txtLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLabel_KeyPress);
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(3, 74);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(73, 16);
            this.lblLabel.Text = "物料标签：";
            // 
            // dtpValityDate
            // 
            this.dtpValityDate.CustomFormat = "";
            this.dtpValityDate.Location = new System.Drawing.Point(72, 228);
            this.dtpValityDate.Name = "dtpValityDate";
            this.dtpValityDate.Size = new System.Drawing.Size(165, 24);
            this.dtpValityDate.TabIndex = 356;
            this.dtpValityDate.ValueChanged += new System.EventHandler(this.dtpValityDate_ValueChanged);
            this.dtpValityDate.Validated += new System.EventHandler(this.dtpDate_Validated);
            // 
            // dtpProDate
            // 
            this.dtpProDate.CustomFormat = "";
            this.dtpProDate.Location = new System.Drawing.Point(72, 203);
            this.dtpProDate.Name = "dtpProDate";
            this.dtpProDate.Size = new System.Drawing.Size(165, 24);
            this.dtpProDate.TabIndex = 355;
            this.dtpProDate.ValueChanged += new System.EventHandler(this.dtpProDate_ValueChanged);
            this.dtpProDate.Validated += new System.EventHandler(this.dtpDate_Validated);
            // 
            // cbChinese
            // 
            this.cbChinese.Location = new System.Drawing.Point(218, 182);
            this.cbChinese.Name = "cbChinese";
            this.cbChinese.Size = new System.Drawing.Size(20, 20);
            this.cbChinese.TabIndex = 354;
            this.cbChinese.CheckStateChanged += new System.EventHandler(this.cbChinese_CheckStateChanged);
            // 
            // dtpChineseDate
            // 
            this.dtpChineseDate.CustomFormat = "";
            this.dtpChineseDate.Location = new System.Drawing.Point(72, 178);
            this.dtpChineseDate.Name = "dtpChineseDate";
            this.dtpChineseDate.Size = new System.Drawing.Size(139, 24);
            this.dtpChineseDate.TabIndex = 353;
            this.dtpChineseDate.Validated += new System.EventHandler(this.dtpDate_Validated);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 20);
            this.label11.Text = "中成药产期";
            // 
            // cbxWareHouse
            // 
            this.cbxWareHouse.Location = new System.Drawing.Point(72, 46);
            this.cbxWareHouse.Name = "cbxWareHouse";
            this.cbxWareHouse.Size = new System.Drawing.Size(165, 23);
            this.cbxWareHouse.TabIndex = 352;
            this.cbxWareHouse.SelectedIndexChanged += new System.EventHandler(this.cbxWareHouse_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 16);
            this.label12.Text = "入库仓库：";
            // 
            // lblBarCode
            // 
            this.lblBarCode.Location = new System.Drawing.Point(75, 99);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(161, 20);
            this.lblBarCode.Text = "|||||||||||||||";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.Text = "69码：";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "有效期至：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.Text = "生产日期：";
            // 
            // InputPannel
            // 
            this.InputPannel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.InputPannel.Location = new System.Drawing.Point(212, 3);
            this.InputPannel.Name = "InputPannel";
            this.InputPannel.Size = new System.Drawing.Size(26, 20);
            this.InputPannel.TabIndex = 351;
            this.InputPannel.Text = "清";
            this.InputPannel.Click += new System.EventHandler(this.InputPannel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.Text = "是否质检：";
            // 
            // rbtNoCheck
            // 
            this.rbtNoCheck.Enabled = false;
            this.rbtNoCheck.Location = new System.Drawing.Point(167, 28);
            this.rbtNoCheck.Name = "rbtNoCheck";
            this.rbtNoCheck.Size = new System.Drawing.Size(70, 20);
            this.rbtNoCheck.TabIndex = 350;
            this.rbtNoCheck.TabStop = false;
            this.rbtNoCheck.Text = "非质检";
            this.rbtNoCheck.Click += new System.EventHandler(this.rbt_Click);
            // 
            // rbtCheck
            // 
            this.rbtCheck.Enabled = false;
            this.rbtCheck.Location = new System.Drawing.Point(76, 28);
            this.rbtCheck.Name = "rbtCheck";
            this.rbtCheck.Size = new System.Drawing.Size(63, 20);
            this.rbtCheck.TabIndex = 349;
            this.rbtCheck.TabStop = false;
            this.rbtCheck.Text = "质检";
            this.rbtCheck.Click += new System.EventHandler(this.rbt_Click);
            // 
            // lblDoneNum
            // 
            this.lblDoneNum.Location = new System.Drawing.Point(182, 281);
            this.lblDoneNum.Name = "lblDoneNum";
            this.lblDoneNum.Size = new System.Drawing.Size(54, 17);
            this.lblDoneNum.Text = "3212";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(140, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 16);
            this.label7.Text = "已扫：";
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Location = new System.Drawing.Point(72, 253);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(165, 23);
            this.txtBatch.TabIndex = 348;
            this.txtBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBatch_KeyPress);
            // 
            // lblStandard
            // 
            this.lblStandard.Location = new System.Drawing.Point(72, 139);
            this.lblStandard.Name = "lblStandard";
            this.lblStandard.Size = new System.Drawing.Size(164, 20);
            this.lblStandard.Text = "XXXXXX";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.Text = "规格：";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(188, 300);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 35);
            this.btnExit.TabIndex = 347;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(137, 300);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(50, 35);
            this.btnSubmit.TabIndex = 346;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(69, 300);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(67, 35);
            this.btnDone.TabIndex = 345;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(1, 300);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(67, 35);
            this.btnSource.TabIndex = 344;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(72, 277);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(68, 23);
            this.txtCount.TabIndex = 343;
            this.txtCount.Text = "1";
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 17);
            this.label8.Text = "数量：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.Text = "批次：";
            // 
            // lblAddrCode
            // 
            this.lblAddrCode.Location = new System.Drawing.Point(71, 159);
            this.lblAddrCode.Name = "lblAddrCode";
            this.lblAddrCode.Size = new System.Drawing.Size(165, 20);
            this.lblAddrCode.Text = "XXX";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.Text = "产地：";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(72, 120);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(164, 20);
            this.lblName.Text = "人参";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "品称：";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(72, 2);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(134, 23);
            this.txtOrder.TabIndex = 342;
            this.txtOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrder_KeyPress);
            // 
            // lblOrder
            // 
            this.lblOrder.Location = new System.Drawing.Point(3, 6);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(77, 16);
            this.lblOrder.Text = "委外订单：";
            // 
            // frmOSArrival
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 335);
            this.ControlBox = false;
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.dtpValityDate);
            this.Controls.Add(this.dtpProDate);
            this.Controls.Add(this.cbChinese);
            this.Controls.Add(this.dtpChineseDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxWareHouse);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblBarCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputPannel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtNoCheck);
            this.Controls.Add(this.rbtCheck);
            this.Controls.Add(this.lblDoneNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.lblStandard);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddrCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.lblOrder);
            this.Name = "frmOSArrival";
            this.Text = "委外到货扫描";
            this.Load += new System.EventHandler(this.frmOSArrival_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.DateTimePicker dtpValityDate;
        private System.Windows.Forms.DateTimePicker dtpProDate;
        private System.Windows.Forms.CheckBox cbChinese;
        private System.Windows.Forms.DateTimePicker dtpChineseDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxWareHouse;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button InputPannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtNoCheck;
        private System.Windows.Forms.RadioButton rbtCheck;
        private System.Windows.Forms.Label lblDoneNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label lblStandard;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAddrCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Label lblOrder;
    }
}