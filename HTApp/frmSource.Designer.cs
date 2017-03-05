namespace HTApp
{
    partial class frmSource
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgSource = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "返回";
            this.button1.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dgSource
            // 
            this.dgSource.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgSource.Location = new System.Drawing.Point(3, 0);
            this.dgSource.Name = "dgSource";
            this.dgSource.Size = new System.Drawing.Size(234, 300);
            this.dgSource.TabIndex = 4;
            // 
            // frmSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 337);
            this.ControlBox = false;
            this.Controls.Add(this.dgSource);
            this.Controls.Add(this.button1);
            this.Name = "frmSource";
            this.Text = "来源单据信息";
            this.Load += new System.EventHandler(this.frmSource_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dgSource;
    }
}