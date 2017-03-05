namespace HTApp
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPUArrival = new System.Windows.Forms.Button();
            this.btnPUIn = new System.Windows.Forms.Button();
            this.btnPicking = new System.Windows.Forms.Button();
            this.btnSaleOutGSP = new System.Windows.Forms.Button();
            this.btnSTInProduct = new System.Windows.Forms.Button();
            this.btnSaleOutRed = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnSaleBackGSP = new System.Windows.Forms.Button();
            this.btnPurchaseBackGSP = new System.Windows.Forms.Button();
            this.btnStuffOut = new System.Windows.Forms.Button();
            this.btnPURefund = new System.Windows.Forms.Button();
            this.btnAllotOut = new System.Windows.Forms.Button();
            this.btnAllontIn = new System.Windows.Forms.Button();
            this.btnPAFinalIn = new System.Windows.Forms.Button();
            this.btnPAHalfOut = new System.Windows.Forms.Button();
            this.btnDIFinalOut = new System.Windows.Forms.Button();
            this.btnDIHalfIn = new System.Windows.Forms.Button();
            this.btnOSHalfIn = new System.Windows.Forms.Button();
            this.btnPosition = new System.Windows.Forms.Button();
            this.btnQuantity = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(143, 271);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 20);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPUArrival
            // 
            this.btnPUArrival.Location = new System.Drawing.Point(3, 3);
            this.btnPUArrival.Name = "btnPUArrival";
            this.btnPUArrival.Size = new System.Drawing.Size(104, 20);
            this.btnPUArrival.TabIndex = 1;
            this.btnPUArrival.Text = "采购到货扫描";
            this.btnPUArrival.Click += new System.EventHandler(this.btnPUArrival_Click);
            // 
            // btnPUIn
            // 
            this.btnPUIn.Location = new System.Drawing.Point(120, 3);
            this.btnPUIn.Name = "btnPUIn";
            this.btnPUIn.Size = new System.Drawing.Size(104, 20);
            this.btnPUIn.TabIndex = 2;
            this.btnPUIn.Text = "采购入库扫描";
            this.btnPUIn.Click += new System.EventHandler(this.btnPUIn_Click);
            // 
            // btnPicking
            // 
            this.btnPicking.Location = new System.Drawing.Point(3, 55);
            this.btnPicking.Name = "btnPicking";
            this.btnPicking.Size = new System.Drawing.Size(104, 20);
            this.btnPicking.TabIndex = 5;
            this.btnPicking.Text = "销售出库拣货";
            this.btnPicking.Click += new System.EventHandler(this.btnPicking_Click);
            // 
            // btnSaleOutGSP
            // 
            this.btnSaleOutGSP.Location = new System.Drawing.Point(120, 55);
            this.btnSaleOutGSP.Name = "btnSaleOutGSP";
            this.btnSaleOutGSP.Size = new System.Drawing.Size(104, 20);
            this.btnSaleOutGSP.TabIndex = 6;
            this.btnSaleOutGSP.Text = "销售出库GSP";
            this.btnSaleOutGSP.Click += new System.EventHandler(this.btnSaleOutGSP_Click);
            // 
            // btnSTInProduct
            // 
            this.btnSTInProduct.Location = new System.Drawing.Point(3, 107);
            this.btnSTInProduct.Name = "btnSTInProduct";
            this.btnSTInProduct.Size = new System.Drawing.Size(104, 20);
            this.btnSTInProduct.TabIndex = 9;
            this.btnSTInProduct.Text = "产成品入库";
            this.btnSTInProduct.Click += new System.EventHandler(this.btnSTInProduct_Click);
            // 
            // btnSaleOutRed
            // 
            this.btnSaleOutRed.Location = new System.Drawing.Point(120, 81);
            this.btnSaleOutRed.Name = "btnSaleOutRed";
            this.btnSaleOutRed.Size = new System.Drawing.Size(104, 20);
            this.btnSaleOutRed.TabIndex = 8;
            this.btnSaleOutRed.Text = "销售出库红字";
            this.btnSaleOutRed.Click += new System.EventHandler(this.btnSaleOutRed_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(120, 107);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(104, 20);
            this.btnCheck.TabIndex = 10;
            this.btnCheck.Text = "盘点";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnSaleBackGSP
            // 
            this.btnSaleBackGSP.Location = new System.Drawing.Point(3, 81);
            this.btnSaleBackGSP.Name = "btnSaleBackGSP";
            this.btnSaleBackGSP.Size = new System.Drawing.Size(104, 20);
            this.btnSaleBackGSP.TabIndex = 7;
            this.btnSaleBackGSP.Text = "销售退货GSP";
            this.btnSaleBackGSP.Click += new System.EventHandler(this.btnSaleBackGSP_Click);
            // 
            // btnPurchaseBackGSP
            // 
            this.btnPurchaseBackGSP.Location = new System.Drawing.Point(120, 29);
            this.btnPurchaseBackGSP.Name = "btnPurchaseBackGSP";
            this.btnPurchaseBackGSP.Size = new System.Drawing.Size(104, 20);
            this.btnPurchaseBackGSP.TabIndex = 4;
            this.btnPurchaseBackGSP.Text = "采购退货GSP";
            this.btnPurchaseBackGSP.Click += new System.EventHandler(this.btnPurchaseBackGSP_Click);
            // 
            // btnStuffOut
            // 
            this.btnStuffOut.Location = new System.Drawing.Point(3, 133);
            this.btnStuffOut.Name = "btnStuffOut";
            this.btnStuffOut.Size = new System.Drawing.Size(104, 20);
            this.btnStuffOut.TabIndex = 11;
            this.btnStuffOut.Text = "材料出库扫描";
            this.btnStuffOut.Click += new System.EventHandler(this.btnStuffOut_Click);
            // 
            // btnPURefund
            // 
            this.btnPURefund.Location = new System.Drawing.Point(3, 29);
            this.btnPURefund.Name = "btnPURefund";
            this.btnPURefund.Size = new System.Drawing.Size(104, 20);
            this.btnPURefund.TabIndex = 3;
            this.btnPURefund.Text = "采购退货扫描";
            this.btnPURefund.Click += new System.EventHandler(this.btnPURefund_Click);
            // 
            // btnAllotOut
            // 
            this.btnAllotOut.Location = new System.Drawing.Point(3, 185);
            this.btnAllotOut.Name = "btnAllotOut";
            this.btnAllotOut.Size = new System.Drawing.Size(104, 20);
            this.btnAllotOut.TabIndex = 14;
            this.btnAllotOut.Text = "调拨出库";
            this.btnAllotOut.Click += new System.EventHandler(this.btnAllotOut_Click);
            // 
            // btnAllontIn
            // 
            this.btnAllontIn.Location = new System.Drawing.Point(3, 159);
            this.btnAllontIn.Name = "btnAllontIn";
            this.btnAllontIn.Size = new System.Drawing.Size(104, 20);
            this.btnAllontIn.TabIndex = 13;
            this.btnAllontIn.Text = "调拨入库";
            this.btnAllontIn.Click += new System.EventHandler(this.btnAllontIn_Click);
            // 
            // btnPAFinalIn
            // 
            this.btnPAFinalIn.Location = new System.Drawing.Point(120, 237);
            this.btnPAFinalIn.Name = "btnPAFinalIn";
            this.btnPAFinalIn.Size = new System.Drawing.Size(104, 20);
            this.btnPAFinalIn.TabIndex = 18;
            this.btnPAFinalIn.Text = "组装成品入库";
            this.btnPAFinalIn.Click += new System.EventHandler(this.btnPAFinalIn_Click);
            // 
            // btnPAHalfOut
            // 
            this.btnPAHalfOut.Location = new System.Drawing.Point(120, 211);
            this.btnPAHalfOut.Name = "btnPAHalfOut";
            this.btnPAHalfOut.Size = new System.Drawing.Size(104, 20);
            this.btnPAHalfOut.TabIndex = 17;
            this.btnPAHalfOut.Text = "组装半成品出库";
            this.btnPAHalfOut.Click += new System.EventHandler(this.btnPAHalfOut_Click);
            // 
            // btnDIFinalOut
            // 
            this.btnDIFinalOut.Location = new System.Drawing.Point(120, 185);
            this.btnDIFinalOut.Name = "btnDIFinalOut";
            this.btnDIFinalOut.Size = new System.Drawing.Size(104, 20);
            this.btnDIFinalOut.TabIndex = 16;
            this.btnDIFinalOut.Text = "拆卸成品出库";
            this.btnDIFinalOut.Click += new System.EventHandler(this.btnDIFinalOut_Click);
            // 
            // btnDIHalfIn
            // 
            this.btnDIHalfIn.Location = new System.Drawing.Point(120, 159);
            this.btnDIHalfIn.Name = "btnDIHalfIn";
            this.btnDIHalfIn.Size = new System.Drawing.Size(104, 20);
            this.btnDIHalfIn.TabIndex = 15;
            this.btnDIHalfIn.Text = "拆卸半成品入库";
            this.btnDIHalfIn.Click += new System.EventHandler(this.btnDIHalfIn_Click);
            // 
            // btnOSHalfIn
            // 
            this.btnOSHalfIn.Location = new System.Drawing.Point(120, 133);
            this.btnOSHalfIn.Name = "btnOSHalfIn";
            this.btnOSHalfIn.Size = new System.Drawing.Size(104, 20);
            this.btnOSHalfIn.TabIndex = 12;
            this.btnOSHalfIn.Text = "委外到货扫描";
            this.btnOSHalfIn.Click += new System.EventHandler(this.btnOSHalfIn_Click);
            // 
            // btnPosition
            // 
            this.btnPosition.Location = new System.Drawing.Point(3, 237);
            this.btnPosition.Name = "btnPosition";
            this.btnPosition.Size = new System.Drawing.Size(104, 20);
            this.btnPosition.TabIndex = 19;
            this.btnPosition.Text = "出库货位管理";
            this.btnPosition.Click += new System.EventHandler(this.btnPosition_Click);
            // 
            // btnQuantity
            // 
            this.btnQuantity.Location = new System.Drawing.Point(3, 271);
            this.btnQuantity.Name = "btnQuantity";
            this.btnQuantity.Size = new System.Drawing.Size(104, 20);
            this.btnQuantity.TabIndex = 20;
            this.btnQuantity.Text = "库存量查询";
            this.btnQuantity.Click += new System.EventHandler(this.btnQuantity_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 20);
            this.button1.TabIndex = 21;
            this.button1.Text = "主菜单";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnQuantity);
            this.Controls.Add(this.btnPosition);
            this.Controls.Add(this.btnAllotOut);
            this.Controls.Add(this.btnAllontIn);
            this.Controls.Add(this.btnPAFinalIn);
            this.Controls.Add(this.btnPAHalfOut);
            this.Controls.Add(this.btnDIFinalOut);
            this.Controls.Add(this.btnDIHalfIn);
            this.Controls.Add(this.btnOSHalfIn);
            this.Controls.Add(this.btnStuffOut);
            this.Controls.Add(this.btnPURefund);
            this.Controls.Add(this.btnPurchaseBackGSP);
            this.Controls.Add(this.btnSaleBackGSP);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnSaleOutRed);
            this.Controls.Add(this.btnSTInProduct);
            this.Controls.Add(this.btnSaleOutGSP);
            this.Controls.Add(this.btnPicking);
            this.Controls.Add(this.btnPUIn);
            this.Controls.Add(this.btnPUArrival);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "主界面";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPUArrival;
        private System.Windows.Forms.Button btnPUIn;
        private System.Windows.Forms.Button btnPicking;
        private System.Windows.Forms.Button btnSaleOutGSP;
        private System.Windows.Forms.Button btnSTInProduct;
        private System.Windows.Forms.Button btnSaleOutRed;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnSaleBackGSP;
        private System.Windows.Forms.Button btnPurchaseBackGSP;
        private System.Windows.Forms.Button btnStuffOut;
        private System.Windows.Forms.Button btnPURefund;
        private System.Windows.Forms.Button btnAllotOut;
        private System.Windows.Forms.Button btnAllontIn;
        private System.Windows.Forms.Button btnPAFinalIn;
        private System.Windows.Forms.Button btnPAHalfOut;
        private System.Windows.Forms.Button btnDIFinalOut;
        private System.Windows.Forms.Button btnDIHalfIn;
        private System.Windows.Forms.Button btnOSHalfIn;
        private System.Windows.Forms.Button btnPosition;
        private System.Windows.Forms.Button btnQuantity;
        private System.Windows.Forms.Button button1;
    }
}