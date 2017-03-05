namespace HTApp
{
    partial class frmExpressOrderSBV
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
            this.cmbExpress = new System.Windows.Forms.ComboBox();
            this.lblCusName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblMaker = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSure = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExpressOrder = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbExpress
            // 
            this.cmbExpress.Location = new System.Drawing.Point(78, 176);
            this.cmbExpress.Name = "cmbExpress";
            this.cmbExpress.Size = new System.Drawing.Size(100, 23);
            this.cmbExpress.TabIndex = 31;
            // 
            // lblCusName
            // 
            this.lblCusName.Location = new System.Drawing.Point(78, 145);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(152, 20);
            this.lblCusName.Text = "制单人：";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(78, 109);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(85, 20);
            this.lblDate.Text = "制单人：";
            // 
            // lblMaker
            // 
            this.lblMaker.Location = new System.Drawing.Point(78, 66);
            this.lblMaker.Name = "lblMaker";
            this.lblMaker.Size = new System.Drawing.Size(71, 20);
            this.lblMaker.Text = "xxx";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(144, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 29);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(30, 260);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(72, 29);
            this.btnSure.TabIndex = 30;
            this.btnSure.Text = "确定";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.Text = "制单时间：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.Text = "快递单号：";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.Text = "快递方式：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.Text = "客户名称：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.Text = "制单人：";
            // 
            // txtExpressOrder
            // 
            this.txtExpressOrder.Location = new System.Drawing.Point(78, 216);
            this.txtExpressOrder.Name = "txtExpressOrder";
            this.txtExpressOrder.Size = new System.Drawing.Size(131, 23);
            this.txtExpressOrder.TabIndex = 28;
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(78, 23);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(131, 23);
            this.txtOrder.TabIndex = 27;
            this.txtOrder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrder_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.Text = "单据号：";
            // 
            // frmExpressOrderSBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 337);
            this.Controls.Add(this.cmbExpress);
            this.Controls.Add(this.lblCusName);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblMaker);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExpressOrder);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.label1);
            this.Name = "frmExpressOrderSBV";
            this.Text = "销售发票快递单号";
            this.Load += new System.EventHandler(this.frmExpressOrderSBV_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbExpress;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblMaker;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExpressOrder;
        public System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Label label1;
    }
}