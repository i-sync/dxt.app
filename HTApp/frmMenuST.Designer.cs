namespace HTApp
{
    partial class frmMenuST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuST));
            this.plTop = new System.Windows.Forms.Panel();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.plContent = new System.Windows.Forms.Panel();
            this.plMake = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pbCheck = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pbProIn = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pbStuffOut = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pbPurRefund = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.pbPurIn = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pbSellOut = new System.Windows.Forms.PictureBox();
            this.pbSTVerify = new System.Windows.Forms.PictureBox();
            this.plVerify = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pbApartIn = new System.Windows.Forms.PictureBox();
            this.pbApartOut = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pbPackIn = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pbPackOut = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pbAlloIn = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pbAllotOut = new System.Windows.Forms.PictureBox();
            this.pbSTMake = new System.Windows.Forms.PictureBox();
            this.plTop.SuspendLayout();
            this.plContent.SuspendLayout();
            this.plMake.SuspendLayout();
            this.plVerify.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.pbExit);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(243, 30);
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
            // plContent
            // 
            this.plContent.Controls.Add(this.plMake);
            this.plContent.Controls.Add(this.plVerify);
            this.plContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContent.Location = new System.Drawing.Point(0, 30);
            this.plContent.Name = "plContent";
            this.plContent.Size = new System.Drawing.Size(243, 275);
            // 
            // plMake
            // 
            this.plMake.Controls.Add(this.label13);
            this.plMake.Controls.Add(this.pbCheck);
            this.plMake.Controls.Add(this.label14);
            this.plMake.Controls.Add(this.pbProIn);
            this.plMake.Controls.Add(this.label15);
            this.plMake.Controls.Add(this.pbStuffOut);
            this.plMake.Controls.Add(this.label16);
            this.plMake.Controls.Add(this.pbPurRefund);
            this.plMake.Controls.Add(this.label17);
            this.plMake.Controls.Add(this.pbPurIn);
            this.plMake.Controls.Add(this.label18);
            this.plMake.Controls.Add(this.pbSellOut);
            this.plMake.Controls.Add(this.pbSTVerify);
            this.plMake.Dock = System.Windows.Forms.DockStyle.Right;
            this.plMake.Location = new System.Drawing.Point(27, 0);
            this.plMake.Name = "plMake";
            this.plMake.Size = new System.Drawing.Size(216, 275);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label13.Location = new System.Drawing.Point(106, 249);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 20);
            this.label13.Text = "盘点业务";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbCheck
            // 
            this.pbCheck.Image = ((System.Drawing.Image)(resources.GetObject("pbCheck.Image")));
            this.pbCheck.Location = new System.Drawing.Point(112, 183);
            this.pbCheck.Name = "pbCheck";
            this.pbCheck.Size = new System.Drawing.Size(65, 65);
            this.pbCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCheck.Click += new System.EventHandler(this.pbCheck_Click);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label14.Location = new System.Drawing.Point(6, 249);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 20);
            this.label14.Text = "产成品入库";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbProIn
            // 
            this.pbProIn.Image = ((System.Drawing.Image)(resources.GetObject("pbProIn.Image")));
            this.pbProIn.Location = new System.Drawing.Point(12, 183);
            this.pbProIn.Name = "pbProIn";
            this.pbProIn.Size = new System.Drawing.Size(65, 65);
            this.pbProIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbProIn.Click += new System.EventHandler(this.pbProIn_Click);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(106, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 20);
            this.label15.Text = "材料出库扫描";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbStuffOut
            // 
            this.pbStuffOut.Image = ((System.Drawing.Image)(resources.GetObject("pbStuffOut.Image")));
            this.pbStuffOut.Location = new System.Drawing.Point(112, 93);
            this.pbStuffOut.Name = "pbStuffOut";
            this.pbStuffOut.Size = new System.Drawing.Size(65, 65);
            this.pbStuffOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStuffOut.Click += new System.EventHandler(this.pbStuffOut_Click);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label16.Location = new System.Drawing.Point(6, 159);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 20);
            this.label16.Text = "采购退货扫描";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbPurRefund
            // 
            this.pbPurRefund.Image = ((System.Drawing.Image)(resources.GetObject("pbPurRefund.Image")));
            this.pbPurRefund.Location = new System.Drawing.Point(12, 93);
            this.pbPurRefund.Name = "pbPurRefund";
            this.pbPurRefund.Size = new System.Drawing.Size(65, 65);
            this.pbPurRefund.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPurRefund.Click += new System.EventHandler(this.pbPurRefund_Click);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label17.Location = new System.Drawing.Point(106, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 20);
            this.label17.Text = "采购入库扫描";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbPurIn
            // 
            this.pbPurIn.Image = ((System.Drawing.Image)(resources.GetObject("pbPurIn.Image")));
            this.pbPurIn.Location = new System.Drawing.Point(112, 3);
            this.pbPurIn.Name = "pbPurIn";
            this.pbPurIn.Size = new System.Drawing.Size(65, 65);
            this.pbPurIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPurIn.Click += new System.EventHandler(this.pbPurIn_Click);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label18.Location = new System.Drawing.Point(6, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 20);
            this.label18.Text = "销售出库红字";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbSellOut
            // 
            this.pbSellOut.Image = ((System.Drawing.Image)(resources.GetObject("pbSellOut.Image")));
            this.pbSellOut.Location = new System.Drawing.Point(12, 3);
            this.pbSellOut.Name = "pbSellOut";
            this.pbSellOut.Size = new System.Drawing.Size(65, 65);
            this.pbSellOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSellOut.Click += new System.EventHandler(this.pbSellOut_Click);
            // 
            // pbSTVerify
            // 
            this.pbSTVerify.Image = ((System.Drawing.Image)(resources.GetObject("pbSTVerify.Image")));
            this.pbSTVerify.Location = new System.Drawing.Point(181, 113);
            this.pbSTVerify.Name = "pbSTVerify";
            this.pbSTVerify.Size = new System.Drawing.Size(32, 32);
            this.pbSTVerify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSTVerify.Click += new System.EventHandler(this.pbST_Click);
            // 
            // plVerify
            // 
            this.plVerify.Controls.Add(this.label7);
            this.plVerify.Controls.Add(this.label8);
            this.plVerify.Controls.Add(this.pbApartIn);
            this.plVerify.Controls.Add(this.pbApartOut);
            this.plVerify.Controls.Add(this.label9);
            this.plVerify.Controls.Add(this.pbPackIn);
            this.plVerify.Controls.Add(this.label10);
            this.plVerify.Controls.Add(this.pbPackOut);
            this.plVerify.Controls.Add(this.label11);
            this.plVerify.Controls.Add(this.pbAlloIn);
            this.plVerify.Controls.Add(this.label12);
            this.plVerify.Controls.Add(this.pbAllotOut);
            this.plVerify.Controls.Add(this.pbSTMake);
            this.plVerify.Dock = System.Windows.Forms.DockStyle.Left;
            this.plVerify.Location = new System.Drawing.Point(0, 0);
            this.plVerify.Name = "plVerify";
            this.plVerify.Size = new System.Drawing.Size(211, 275);
            this.plVerify.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(133, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.Text = "拆卸入库审核";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(33, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.Text = "拆卸出库审核";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbApartIn
            // 
            this.pbApartIn.Image = ((System.Drawing.Image)(resources.GetObject("pbApartIn.Image")));
            this.pbApartIn.Location = new System.Drawing.Point(139, 183);
            this.pbApartIn.Name = "pbApartIn";
            this.pbApartIn.Size = new System.Drawing.Size(65, 65);
            this.pbApartIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbApartIn.Click += new System.EventHandler(this.pbApartIn_Click);
            // 
            // pbApartOut
            // 
            this.pbApartOut.Image = ((System.Drawing.Image)(resources.GetObject("pbApartOut.Image")));
            this.pbApartOut.Location = new System.Drawing.Point(39, 183);
            this.pbApartOut.Name = "pbApartOut";
            this.pbApartOut.Size = new System.Drawing.Size(65, 65);
            this.pbApartOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbApartOut.Click += new System.EventHandler(this.pbApartOut_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(133, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.Text = "组装入库审核";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbPackIn
            // 
            this.pbPackIn.Image = ((System.Drawing.Image)(resources.GetObject("pbPackIn.Image")));
            this.pbPackIn.Location = new System.Drawing.Point(139, 93);
            this.pbPackIn.Name = "pbPackIn";
            this.pbPackIn.Size = new System.Drawing.Size(65, 65);
            this.pbPackIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPackIn.Click += new System.EventHandler(this.pbPackIn_Click);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(33, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 20);
            this.label10.Text = "组装出库审核";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbPackOut
            // 
            this.pbPackOut.Image = ((System.Drawing.Image)(resources.GetObject("pbPackOut.Image")));
            this.pbPackOut.Location = new System.Drawing.Point(39, 93);
            this.pbPackOut.Name = "pbPackOut";
            this.pbPackOut.Size = new System.Drawing.Size(65, 65);
            this.pbPackOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPackOut.Click += new System.EventHandler(this.pbPackOut_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(133, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 20);
            this.label11.Text = "调拨入库审核";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbAlloIn
            // 
            this.pbAlloIn.Image = ((System.Drawing.Image)(resources.GetObject("pbAlloIn.Image")));
            this.pbAlloIn.Location = new System.Drawing.Point(139, 3);
            this.pbAlloIn.Name = "pbAlloIn";
            this.pbAlloIn.Size = new System.Drawing.Size(65, 65);
            this.pbAlloIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAlloIn.Click += new System.EventHandler(this.pbAlloIn_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label12.Location = new System.Drawing.Point(33, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.Text = "调拨出库审核";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbAllotOut
            // 
            this.pbAllotOut.Image = ((System.Drawing.Image)(resources.GetObject("pbAllotOut.Image")));
            this.pbAllotOut.Location = new System.Drawing.Point(39, 3);
            this.pbAllotOut.Name = "pbAllotOut";
            this.pbAllotOut.Size = new System.Drawing.Size(65, 65);
            this.pbAllotOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAllotOut.Click += new System.EventHandler(this.pbAllotOut_Click);
            // 
            // pbSTMake
            // 
            this.pbSTMake.Image = ((System.Drawing.Image)(resources.GetObject("pbSTMake.Image")));
            this.pbSTMake.Location = new System.Drawing.Point(3, 113);
            this.pbSTMake.Name = "pbSTMake";
            this.pbSTMake.Size = new System.Drawing.Size(32, 32);
            this.pbSTMake.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSTMake.Click += new System.EventHandler(this.pbST_Click);
            // 
            // frmMenuST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 305);
            this.ControlBox = false;
            this.Controls.Add(this.plContent);
            this.Controls.Add(this.plTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuST";
            this.Text = "库存管理";
            this.Load += new System.EventHandler(this.frmMenuST_Load);
            this.Closed += new System.EventHandler(this.frmMenuST_Closed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMenuST_KeyPress);
            this.plTop.ResumeLayout(false);
            this.plContent.ResumeLayout(false);
            this.plMake.ResumeLayout(false);
            this.plVerify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTop;
        private System.Windows.Forms.Panel plContent;
        private System.Windows.Forms.Panel plVerify;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbApartIn;
        private System.Windows.Forms.PictureBox pbApartOut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pbPackIn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pbPackOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pbAlloIn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pbAllotOut;
        private System.Windows.Forms.PictureBox pbSTMake;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.Panel plMake;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pbCheck;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pbProIn;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbStuffOut;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pbPurRefund;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pbPurIn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pbSellOut;
        private System.Windows.Forms.PictureBox pbSTVerify;
    }
}