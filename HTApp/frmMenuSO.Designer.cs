namespace HTApp
{
    partial class frmMenuSO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuSO));
            this.mmTopMenu = new System.Windows.Forms.MainMenu();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbOutPos = new System.Windows.Forms.PictureBox();
            this.pbSellOut = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbExpressOrderDPL = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbExpressOrderSBV = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(33, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "销售出库拣货";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(133, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "出库货位管理";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbOutPos
            // 
            this.pbOutPos.Image = ((System.Drawing.Image)(resources.GetObject("pbOutPos.Image")));
            this.pbOutPos.Location = new System.Drawing.Point(139, 33);
            this.pbOutPos.Name = "pbOutPos";
            this.pbOutPos.Size = new System.Drawing.Size(65, 65);
            this.pbOutPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOutPos.Click += new System.EventHandler(this.pbOutPos_Click);
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
            // pbExit
            // 
            this.pbExit.Image = ((System.Drawing.Image)(resources.GetObject("pbExit.Image")));
            this.pbExit.Location = new System.Drawing.Point(0, 0);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(32, 32);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // pbExpressOrderDPL
            // 
            this.pbExpressOrderDPL.Image = ((System.Drawing.Image)(resources.GetObject("pbExpressOrderDPL.Image")));
            this.pbExpressOrderDPL.Location = new System.Drawing.Point(39, 142);
            this.pbExpressOrderDPL.Name = "pbExpressOrderDPL";
            this.pbExpressOrderDPL.Size = new System.Drawing.Size(65, 65);
            this.pbExpressOrderDPL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExpressOrderDPL.Click += new System.EventHandler(this.pbExpressOrderDPL_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(33, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "发货快递单";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbExpressOrderSBV
            // 
            this.pbExpressOrderSBV.Image = ((System.Drawing.Image)(resources.GetObject("pbExpressOrderSBV.Image")));
            this.pbExpressOrderSBV.Location = new System.Drawing.Point(139, 142);
            this.pbExpressOrderSBV.Name = "pbExpressOrderSBV";
            this.pbExpressOrderSBV.Size = new System.Drawing.Size(65, 65);
            this.pbExpressOrderSBV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExpressOrderSBV.Click += new System.EventHandler(this.pbExpressOrderSBV_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(133, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.Text = "销售发票快递单";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmMenuSO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 305);
            this.ControlBox = false;
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbExpressOrderSBV);
            this.Controls.Add(this.pbOutPos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbExpressOrderDPL);
            this.Controls.Add(this.pbSellOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuSO";
            this.Text = "销售管理";
            this.Load += new System.EventHandler(this.frmMenuSO_Load);
            this.Closed += new System.EventHandler(this.frmMenuSO_Closed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMenuSO_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mmTopMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbSellOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbOutPos;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.PictureBox pbExpressOrderDPL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbExpressOrderSBV;
        private System.Windows.Forms.Label label4;
    }
}