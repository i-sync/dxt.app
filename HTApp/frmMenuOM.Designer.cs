namespace HTApp
{
    partial class frmMenuOM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuOM));
            this.mmTopMenu = new System.Windows.Forms.MainMenu();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pbOSArrival = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
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
            this.label1.Text = "委外到货扫描";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbOSArrival
            // 
            this.pbOSArrival.Image = ((System.Drawing.Image)(resources.GetObject("pbOSArrival.Image")));
            this.pbOSArrival.Location = new System.Drawing.Point(39, 33);
            this.pbOSArrival.Name = "pbOSArrival";
            this.pbOSArrival.Size = new System.Drawing.Size(65, 65);
            this.pbOSArrival.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOSArrival.Click += new System.EventHandler(this.pbOSArrival_Click);
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
            // frmMenuOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(243, 305);
            this.ControlBox = false;
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbOSArrival);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuOM";
            this.Text = "委外管理";
            this.Load += new System.EventHandler(this.frmMenuOM_Load);
            this.Closed += new System.EventHandler(this.frmMenuOM_Closed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMenuOM_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mmTopMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbOSArrival;
        private System.Windows.Forms.PictureBox pbExit;
    }
}