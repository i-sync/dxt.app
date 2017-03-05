namespace HTApp
{
    partial class frmMenuGSP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuGSP));
            this.mmTopMenu = new System.Windows.Forms.MainMenu();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbPURefund = new System.Windows.Forms.PictureBox();
            this.pbSellRefund = new System.Windows.Forms.PictureBox();
            this.pbSellOut = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mmTopMenu
            // 
            this.mmTopMenu.MenuItems.Add(this.miExit);
            // 
            // miExit
            // 
            this.miExit.Text = "返回";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(133, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "销售退货GSP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(33, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "销售出库GSP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(33, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "采购退货GSP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbExit
            // 
            this.pbExit.Image = ((System.Drawing.Image)(resources.GetObject("pbExit.Image")));
            this.pbExit.Location = new System.Drawing.Point(0, 0);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(32, 32);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // pbPURefund
            // 
            this.pbPURefund.Image = ((System.Drawing.Image)(resources.GetObject("pbPURefund.Image")));
            this.pbPURefund.Location = new System.Drawing.Point(39, 123);
            this.pbPURefund.Name = "pbPURefund";
            this.pbPURefund.Size = new System.Drawing.Size(65, 65);
            this.pbPURefund.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPURefund.Click += new System.EventHandler(this.pbPURefund_Click);
            // 
            // pbSellRefund
            // 
            this.pbSellRefund.Image = ((System.Drawing.Image)(resources.GetObject("pbSellRefund.Image")));
            this.pbSellRefund.Location = new System.Drawing.Point(139, 33);
            this.pbSellRefund.Name = "pbSellRefund";
            this.pbSellRefund.Size = new System.Drawing.Size(65, 65);
            this.pbSellRefund.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSellRefund.Click += new System.EventHandler(this.pbSellRefund_Click);
            // 
            // pbSellOut
            // 
            this.pbSellOut.Image = ((System.Drawing.Image)(resources.GetObject("pbSellOut.Image")));
            this.pbSellOut.Location = new System.Drawing.Point(39, 33);
            this.pbSellOut.Name = "pbSellOut";
            this.pbSellOut.Size = new System.Drawing.Size(65, 65);
            this.pbSellOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSellOut.Click += new System.EventHandler(this.pbSellOut_Click);
            // 
            // frmMenuGSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 305);
            this.ControlBox = false;
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbPURefund);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbSellRefund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbSellOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuGSP";
            this.Text = "GSP质量管理";
            this.Load += new System.EventHandler(this.frmMenuGSP_Load);
            this.Closed += new System.EventHandler(this.frmMenuGSP_Closed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMenuGSP_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mmTopMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbSellRefund;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbSellOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbPURefund;
        private System.Windows.Forms.PictureBox pbExit;
    }
}