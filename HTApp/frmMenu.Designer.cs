namespace HTApp
{
    partial class frmMenu
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmTopMenu;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.mmTopMenu = new System.Windows.Forms.MainMenu();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbCurrent = new System.Windows.Forms.PictureBox();
            this.pbStock = new System.Windows.Forms.PictureBox();
            this.pbGSP = new System.Windows.Forms.PictureBox();
            this.pbOutsourcing = new System.Windows.Forms.PictureBox();
            this.pbSell = new System.Windows.Forms.PictureBox();
            this.pbPurchase = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mmTopMenu
            // 
            this.mmTopMenu.MenuItems.Add(this.miExit);
            // 
            // miExit
            // 
            this.miExit.Text = "退出";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(33, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "销售管理";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(133, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "采购管理";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(33, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.Text = "委外管理";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(133, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.Text = "质量管理";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(33, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.Text = "库存管理";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(133, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.Text = "现存量查询";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // pbCurrent
            // 
            this.pbCurrent.Image = ((System.Drawing.Image)(resources.GetObject("pbCurrent.Image")));
            this.pbCurrent.Location = new System.Drawing.Point(139, 213);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(65, 65);
            this.pbCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCurrent.Click += new System.EventHandler(this.pbCurrent_Click);
            // 
            // pbStock
            // 
            this.pbStock.Image = ((System.Drawing.Image)(resources.GetObject("pbStock.Image")));
            this.pbStock.Location = new System.Drawing.Point(39, 213);
            this.pbStock.Name = "pbStock";
            this.pbStock.Size = new System.Drawing.Size(65, 65);
            this.pbStock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStock.Click += new System.EventHandler(this.pbStock_Click);
            // 
            // pbGSP
            // 
            this.pbGSP.Image = ((System.Drawing.Image)(resources.GetObject("pbGSP.Image")));
            this.pbGSP.Location = new System.Drawing.Point(139, 123);
            this.pbGSP.Name = "pbGSP";
            this.pbGSP.Size = new System.Drawing.Size(65, 65);
            this.pbGSP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGSP.Click += new System.EventHandler(this.pbGSP_Click);
            // 
            // pbOutsourcing
            // 
            this.pbOutsourcing.Image = ((System.Drawing.Image)(resources.GetObject("pbOutsourcing.Image")));
            this.pbOutsourcing.Location = new System.Drawing.Point(39, 123);
            this.pbOutsourcing.Name = "pbOutsourcing";
            this.pbOutsourcing.Size = new System.Drawing.Size(65, 65);
            this.pbOutsourcing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOutsourcing.Click += new System.EventHandler(this.pbOutsourcing_Click);
            // 
            // pbSell
            // 
            this.pbSell.Image = ((System.Drawing.Image)(resources.GetObject("pbSell.Image")));
            this.pbSell.Location = new System.Drawing.Point(39, 33);
            this.pbSell.Name = "pbSell";
            this.pbSell.Size = new System.Drawing.Size(65, 65);
            this.pbSell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSell.Click += new System.EventHandler(this.pbSell_Click);
            // 
            // pbPurchase
            // 
            this.pbPurchase.Image = ((System.Drawing.Image)(resources.GetObject("pbPurchase.Image")));
            this.pbPurchase.Location = new System.Drawing.Point(139, 33);
            this.pbPurchase.Name = "pbPurchase";
            this.pbPurchase.Size = new System.Drawing.Size(65, 65);
            this.pbPurchase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPurchase.Click += new System.EventHandler(this.pbPurchase_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 305);
            this.ControlBox = false;
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.pbStock);
            this.Controls.Add(this.pbGSP);
            this.Controls.Add(this.pbOutsourcing);
            this.Controls.Add(this.pbSell);
            this.Controls.Add(this.pbPurchase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenu";
            this.Text = "主菜单";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMenu_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.PictureBox pbPurchase;
        private System.Windows.Forms.PictureBox pbSell;
        private System.Windows.Forms.PictureBox pbOutsourcing;
        private System.Windows.Forms.PictureBox pbGSP;
        private System.Windows.Forms.PictureBox pbCurrent;
        private System.Windows.Forms.PictureBox pbStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbExit;
    }
}