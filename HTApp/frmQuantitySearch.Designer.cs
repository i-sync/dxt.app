namespace HTApp
{
    partial class frmQuantitySearch
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
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbType1 = new System.Windows.Forms.RadioButton();
            this.rbType2 = new System.Windows.Forms.RadioButton();
            this.txtCBarCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(138, 312);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 25);
            this.btnExit.TabIndex = 51;
            this.btnExit.Text = "返回";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(2, 45);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(234, 266);
            this.dataGrid1.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "类型：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "编码：";
            // 
            // rbType1
            // 
            this.rbType1.Checked = true;
            this.rbType1.Location = new System.Drawing.Point(60, 2);
            this.rbType1.Name = "rbType1";
            this.rbType1.Size = new System.Drawing.Size(71, 20);
            this.rbType1.TabIndex = 55;
            this.rbType1.Text = "现存量";
            this.rbType1.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbType2
            // 
            this.rbType2.Location = new System.Drawing.Point(143, 2);
            this.rbType2.Name = "rbType2";
            this.rbType2.Size = new System.Drawing.Size(91, 20);
            this.rbType2.TabIndex = 56;
            this.rbType2.Text = "货位存量";
            this.rbType2.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // txtCBarCode
            // 
            this.txtCBarCode.Location = new System.Drawing.Point(60, 22);
            this.txtCBarCode.Name = "txtCBarCode";
            this.txtCBarCode.Size = new System.Drawing.Size(160, 23);
            this.txtCBarCode.TabIndex = 57;
            this.txtCBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCBarCode_KeyPress);
            // 
            // frmQuantitySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 337);
            this.Controls.Add(this.txtCBarCode);
            this.Controls.Add(this.rbType2);
            this.Controls.Add(this.rbType1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGrid1);
            this.Name = "frmQuantitySearch";
            this.Text = "库存量查询";
            this.Load += new System.EventHandler(this.frmQuantitySearch_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbType1;
        private System.Windows.Forms.RadioButton rbType2;
        private System.Windows.Forms.TextBox txtCBarCode;
    }
}