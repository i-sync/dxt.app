namespace HTApp
{
    partial class frmDone
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
            this.dgDone = new System.Windows.Forms.DataGrid();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPosition = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dgDone
            // 
            this.dgDone.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgDone.Location = new System.Drawing.Point(3, 0);
            this.dgDone.Name = "dgDone";
            this.dgDone.Size = new System.Drawing.Size(234, 300);
            this.dgDone.TabIndex = 9;
            this.dgDone.CurrentCellChanged += new System.EventHandler(this.dgDone_CurrentCellChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(170, 301);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 35);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(2, 301);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 35);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPosition
            // 
            this.btnPosition.Location = new System.Drawing.Point(86, 301);
            this.btnPosition.Name = "btnPosition";
            this.btnPosition.Size = new System.Drawing.Size(67, 35);
            this.btnPosition.TabIndex = 10;
            this.btnPosition.Text = "货位";
            this.btnPosition.Visible = false;
            this.btnPosition.Click += new System.EventHandler(this.btnPosition_Click);
            // 
            // frmDone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 337);
            this.Controls.Add(this.btnPosition);
            this.Controls.Add(this.dgDone);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Name = "frmDone";
            this.Text = "已扫描数据";
            this.Load += new System.EventHandler(this.frmDone_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgDone;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPosition;
    }
}