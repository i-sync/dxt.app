namespace Regulatory
{
    partial class MainForm
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
            this.btnInsertReg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbIsUsed = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvRegulatory = new System.Windows.Forms.DataGridView();
            this.NID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFrist = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnGO = new System.Windows.Forms.Button();
            this.txtGO = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPageC = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPageT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegulatory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsertReg
            // 
            this.btnInsertReg.Location = new System.Drawing.Point(694, 7);
            this.btnInsertReg.Name = "btnInsertReg";
            this.btnInsertReg.Size = new System.Drawing.Size(75, 23);
            this.btnInsertReg.TabIndex = 0;
            this.btnInsertReg.Text = "导入";
            this.btnInsertReg.UseVisualStyleBackColor = true;
            this.btnInsertReg.Click += new System.EventHandler(this.btnInsertReg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "监管码";
            // 
            // txtRegCode
            // 
            this.txtRegCode.Location = new System.Drawing.Point(70, 9);
            this.txtRegCode.Name = "txtRegCode";
            this.txtRegCode.Size = new System.Drawing.Size(154, 21);
            this.txtRegCode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "单据类型";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(297, 9);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(93, 21);
            this.txtCardNumber.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "单据名称";
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(469, 9);
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(114, 21);
            this.txtCardName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "单据号";
            // 
            // txtCardCode
            // 
            this.txtCardCode.Location = new System.Drawing.Point(72, 36);
            this.txtCardCode.Name = "txtCardCode";
            this.txtCardCode.Size = new System.Drawing.Size(152, 21);
            this.txtCardCode.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "使用情况";
            // 
            // cmbIsUsed
            // 
            this.cmbIsUsed.FormattingEnabled = true;
            this.cmbIsUsed.Location = new System.Drawing.Point(297, 37);
            this.cmbIsUsed.Name = "cmbIsUsed";
            this.cmbIsUsed.Size = new System.Drawing.Size(93, 20);
            this.cmbIsUsed.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(508, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvRegulatory
            // 
            this.dgvRegulatory.AllowUserToAddRows = false;
            this.dgvRegulatory.AllowUserToDeleteRows = false;
            this.dgvRegulatory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegulatory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NID,
            this.RegCode,
            this.CardNumber,
            this.CardName,
            this.CardCode,
            this.UpdateDate,
            this.IsUsed});
            this.dgvRegulatory.Location = new System.Drawing.Point(4, 62);
            this.dgvRegulatory.Name = "dgvRegulatory";
            this.dgvRegulatory.ReadOnly = true;
            this.dgvRegulatory.RowTemplate.Height = 23;
            this.dgvRegulatory.Size = new System.Drawing.Size(765, 405);
            this.dgvRegulatory.TabIndex = 6;
            // 
            // NID
            // 
            this.NID.DataPropertyName = "nid";
            this.NID.HeaderText = "序号";
            this.NID.Name = "NID";
            this.NID.ReadOnly = true;
            this.NID.Width = 40;
            // 
            // RegCode
            // 
            this.RegCode.DataPropertyName = "RegCode";
            this.RegCode.HeaderText = "监管码";
            this.RegCode.Name = "RegCode";
            this.RegCode.ReadOnly = true;
            this.RegCode.Width = 230;
            // 
            // CardNumber
            // 
            this.CardNumber.DataPropertyName = "CardNumber";
            this.CardNumber.HeaderText = "单据类型";
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.ReadOnly = true;
            this.CardNumber.Width = 80;
            // 
            // CardName
            // 
            this.CardName.DataPropertyName = "CardName";
            this.CardName.HeaderText = "单据名称";
            this.CardName.Name = "CardName";
            this.CardName.ReadOnly = true;
            // 
            // CardCode
            // 
            this.CardCode.DataPropertyName = "CardCode";
            this.CardCode.HeaderText = "单据号";
            this.CardCode.Name = "CardCode";
            this.CardCode.ReadOnly = true;
            this.CardCode.Width = 120;
            // 
            // UpdateDate
            // 
            this.UpdateDate.DataPropertyName = "UpdateDate";
            this.UpdateDate.HeaderText = "使用日期";
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.ReadOnly = true;
            this.UpdateDate.Width = 105;
            // 
            // IsUsed
            // 
            this.IsUsed.DataPropertyName = "IsUsed";
            this.IsUsed.HeaderText = "使用";
            this.IsUsed.Name = "IsUsed";
            this.IsUsed.ReadOnly = true;
            this.IsUsed.Width = 40;
            // 
            // btnFrist
            // 
            this.btnFrist.Location = new System.Drawing.Point(72, 473);
            this.btnFrist.Name = "btnFrist";
            this.btnFrist.Size = new System.Drawing.Size(75, 23);
            this.btnFrist.TabIndex = 7;
            this.btnFrist.Text = "首页";
            this.btnFrist.UseVisualStyleBackColor = true;
            this.btnFrist.Click += new System.EventHandler(this.btnPage_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(153, 473);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 7;
            this.btnPrev.Text = "上一页";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPage_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(234, 473);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnPage_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(315, 473);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 7;
            this.btnLast.Text = "末页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnPage_Click);
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(576, 473);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(33, 23);
            this.btnGO.TabIndex = 7;
            this.btnGO.Text = "GO";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // txtGO
            // 
            this.txtGO.Location = new System.Drawing.Point(519, 475);
            this.txtGO.Name = "txtGO";
            this.txtGO.Size = new System.Drawing.Size(51, 21);
            this.txtGO.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 478);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "共";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(480, 478);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "页";
            // 
            // lblPageC
            // 
            this.lblPageC.AutoSize = true;
            this.lblPageC.Location = new System.Drawing.Point(424, 478);
            this.lblPageC.Name = "lblPageC";
            this.lblPageC.Size = new System.Drawing.Size(17, 12);
            this.lblPageC.TabIndex = 10;
            this.lblPageC.Text = "01";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(443, 478);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "/";
            // 
            // lblPageT
            // 
            this.lblPageT.AutoSize = true;
            this.lblPageT.Location = new System.Drawing.Point(452, 478);
            this.lblPageT.Name = "lblPageT";
            this.lblPageT.Size = new System.Drawing.Size(17, 12);
            this.lblPageT.TabIndex = 12;
            this.lblPageT.Text = "01";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 506);
            this.Controls.Add(this.lblPageT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblPageC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtGO);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnFrist);
            this.Controls.Add(this.dgvRegulatory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbIsUsed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCardCode);
            this.Controls.Add(this.txtCardName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRegCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsertReg);
            this.Name = "MainForm";
            this.Text = "监管码";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegulatory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsertReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCardCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbIsUsed;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvRegulatory;
        private System.Windows.Forms.Button btnFrist;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.TextBox txtGO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPageC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPageT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUsed;
    }
}

