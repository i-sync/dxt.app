namespace HTApp
{
    partial class frmPUIn
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InputPannel = new System.Windows.Forms.Button();
            this.rbtQCheck = new System.Windows.Forms.RadioButton();
            this.rbtArrival = new System.Windows.Forms.RadioButton();
            this.lblDoneNum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblProDate = new System.Windows.Forms.Label();
            this.lblValityDate = new System.Windows.Forms.Label();
            this.cbxBatch = new System.Windows.Forms.ComboBox();
            this.lblBatch = new System.Windows.Forms.Label();
            this.txtRegCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRegCode = new System.Windows.Forms.Button();
            this.chkRegCode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "有效期至：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.Text = "生产日期：";
            // 
            // InputPannel
            // 
            this.InputPannel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.InputPannel.Location = new System.Drawing.Point(210, 4);
            this.InputPannel.Name = "InputPannel";
            this.InputPannel.Size = new System.Drawing.Size(26, 20);
            this.InputPannel.TabIndex = 178;
            this.InputPannel.Text = "清";
            this.InputPannel.Click += new System.EventHandler(this.InputPannel_Click);
            // 
            // rbtQCheck
            // 
            this.rbtQCheck.Enabled = false;
            this.rbtQCheck.Location = new System.Drawing.Point(167, 29);
            this.rbtQCheck.Name = "rbtQCheck";
            this.rbtQCheck.Size = new System.Drawing.Size(70, 20);
            this.rbtQCheck.TabIndex = 177;
            this.rbtQCheck.TabStop = false;
            this.rbtQCheck.Text = "质检单";
            this.rbtQCheck.Click += new System.EventHandler(this.rbt_Click);
            // 
            // rbtArrival
            // 
            this.rbtArrival.Checked = true;
            this.rbtArrival.Enabled = false;
            this.rbtArrival.Location = new System.Drawing.Point(73, 29);
            this.rbtArrival.Name = "rbtArrival";
            this.rbtArrival.Size = new System.Drawing.Size(74, 20);
            this.rbtArrival.TabIndex = 176;
            this.rbtArrival.Text = "到货单";
            this.rbtArrival.Click += new System.EventHandler(this.rbt_Click);
            // 
            // lblDoneNum
            // 
            this.lblDoneNum.Location = new System.Drawing.Point(181, 282);
            this.lblDoneNum.Name = "lblDoneNum";
            this.lblDoneNum.Size = new System.Drawing.Size(54, 17);
            this.lblDoneNum.Text = "3212";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(140, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.Text = "已扫：";
            // 
            // lblStandard
            // 
            this.lblStandard.Location = new System.Drawing.Point(69, 167);
            this.lblStandard.Name = "lblStandard";
            this.lblStandard.Size = new System.Drawing.Size(165, 20);
            this.lblStandard.Text = "XXXXXX";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.Text = "规格：";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(188, 301);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 35);
            this.btnExit.TabIndex = 174;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(137, 301);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(50, 35);
            this.btnSubmit.TabIndex = 173;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(69, 301);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(67, 35);
            this.btnDone.TabIndex = 172;
            this.btnDone.Text = "已扫数据";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnSource
            // 
            this.btnSource.Enabled = false;
            this.btnSource.Location = new System.Drawing.Point(1, 301);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(67, 35);
            this.btnSource.TabIndex = 171;
            this.btnSource.Text = "来源单据";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // txtCount
            // 
            this.txtCount.Enabled = false;
            this.txtCount.Location = new System.Drawing.Point(69, 278);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(68, 23);
            this.txtCount.TabIndex = 170;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(1, 282);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 17);
            this.label8.Text = "数量：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.Text = "批次：";
            // 
            // lblAddrCode
            // 
            this.lblAddrCode.Location = new System.Drawing.Point(70, 188);
            this.lblAddrCode.Name = "lblAddrCode";
            this.lblAddrCode.Size = new System.Drawing.Size(165, 20);
            this.lblAddrCode.Text = "XXX";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.Text = "产地：";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(69, 146);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(165, 20);
            this.lblName.Text = "人参";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "品称：";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(70, 3);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(134, 23);
            this.txtOrder.TabIndex = 168;
            this.txtOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrder_KeyPress);
            // 
            // lblOrder
            // 
            this.lblOrder.Location = new System.Drawing.Point(1, 7);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(77, 16);
            this.lblOrder.Text = "来源单据：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.Text = "单据类型：";
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(0, 122);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(73, 16);
            this.lblLabel.Text = "物料标签：";
            // 
            // txtLabel
            // 
            this.txtLabel.Enabled = false;
            this.txtLabel.Location = new System.Drawing.Point(69, 119);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(166, 23);
            this.txtLabel.TabIndex = 169;
            this.txtLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLabel_KeyPress);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 16);
            this.label12.Text = "入库仓库：";
            // 
            // txtPosition
            // 
            this.txtPosition.Enabled = false;
            this.txtPosition.Location = new System.Drawing.Point(69, 96);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(166, 23);
            this.txtPosition.TabIndex = 261;
            this.txtPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPosition_KeyPress);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(1, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 18);
            this.label13.Text = "货位：";
            // 
            // lblStore
            // 
            this.lblStore.Location = new System.Drawing.Point(70, 51);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(163, 20);
            this.lblStore.Text = "东兴堂健康仓库\r\n";
            // 
            // lblProDate
            // 
            this.lblProDate.Location = new System.Drawing.Point(69, 207);
            this.lblProDate.Name = "lblProDate";
            this.lblProDate.Size = new System.Drawing.Size(163, 20);
            this.lblProDate.Text = "yyyy-MM-dd";
            // 
            // lblValityDate
            // 
            this.lblValityDate.Location = new System.Drawing.Point(69, 230);
            this.lblValityDate.Name = "lblValityDate";
            this.lblValityDate.Size = new System.Drawing.Size(163, 20);
            this.lblValityDate.Text = "yyyy-MM-dd";
            // 
            // cbxBatch
            // 
            this.cbxBatch.Location = new System.Drawing.Point(69, 254);
            this.cbxBatch.Name = "cbxBatch";
            this.cbxBatch.Size = new System.Drawing.Size(166, 23);
            this.cbxBatch.TabIndex = 313;
            this.cbxBatch.LostFocus += new System.EventHandler(this.cbxBatch_LostFocus);
            this.cbxBatch.SelectedIndexChanged += new System.EventHandler(this.cbxBatch_SelectedIndexChanged);
            // 
            // lblBatch
            // 
            this.lblBatch.Enabled = false;
            this.lblBatch.Location = new System.Drawing.Point(70, 250);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(163, 23);
            this.lblBatch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBatch_KeyPress);
            // 
            // txtRegCode
            // 
            this.txtRegCode.Enabled = false;
            this.txtRegCode.Location = new System.Drawing.Point(70, 72);
            this.txtRegCode.Name = "txtRegCode";
            this.txtRegCode.ReadOnly = true;
            this.txtRegCode.Size = new System.Drawing.Size(130, 23);
            this.txtRegCode.TabIndex = 336;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(-1, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 18);
            this.label10.Text = "流通监管码";
            // 
            // btnRegCode
            // 
            this.btnRegCode.Enabled = false;
            this.btnRegCode.Location = new System.Drawing.Point(220, 73);
            this.btnRegCode.Name = "btnRegCode";
            this.btnRegCode.Size = new System.Drawing.Size(20, 20);
            this.btnRegCode.TabIndex = 391;
            this.btnRegCode.Text = "读";
            this.btnRegCode.Click += new System.EventHandler(this.btnRegCode_Click);
            // 
            // chkRegCode
            // 
            this.chkRegCode.Location = new System.Drawing.Point(200, 74);
            this.chkRegCode.Name = "chkRegCode";
            this.chkRegCode.Size = new System.Drawing.Size(21, 20);
            this.chkRegCode.TabIndex = 390;
            this.chkRegCode.CheckStateChanged += new System.EventHandler(this.chkRegCode_CheckStateChanged);
            // 
            // frmPUIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 337);
            this.ControlBox = false;
            this.Controls.Add(this.btnRegCode);
            this.Controls.Add(this.chkRegCode);
            this.Controls.Add(this.txtRegCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxBatch);
            this.Controls.Add(this.lblValityDate);
            this.Controls.Add(this.lblProDate);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputPannel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtQCheck);
            this.Controls.Add(this.rbtArrival);
            this.Controls.Add(this.lblDoneNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblBatch);
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
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.lblOrder);
            this.Name = "frmPUIn";
            this.Text = "采购入库扫描";
            this.Load += new System.EventHandler(this.frmPUIn_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button InputPannel;
        private System.Windows.Forms.RadioButton rbtQCheck;
        private System.Windows.Forms.RadioButton rbtArrival;
        private System.Windows.Forms.Label lblDoneNum;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.Label lblProDate;
        private System.Windows.Forms.Label lblValityDate;
        private System.Windows.Forms.ComboBox cbxBatch;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.TextBox txtRegCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRegCode;
        private System.Windows.Forms.CheckBox chkRegCode;
    }
}