using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using U8Business;
using Model;

namespace HTApp
{
    public partial class frmCheckList : Form
    {
        List<CheckDetail> silist;
        public frmCheckList(List<CheckDetail> _siList)
        {
            InitializeComponent();
            if (_siList == null || _siList.Count < 1)
                return;
            this.silist = _siList;
            List<CheckDetail> ds = this.silist.FindAll(delegate(CheckDetail v) { return v.iQuantity > 0; });
            #region initData
            DataGridTableStyle dts = new DataGridTableStyle();

            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cinvcode";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cinvname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格";
            dtbc.MappingName = "cinvstd";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "cinvdefine6";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "计量单位";
            dtbc.MappingName = "ComUnitName";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位";
            dtbc.MappingName = "cPosition";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批次";
            dtbc.MappingName = "cbatch";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "账面数量";
            dtbc.MappingName = "iCVQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盘点数量";
            dtbc.MappingName = "iQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "盈亏数量";
            dtbc.MappingName = "CalQuantity";
            dtbc.Format = "F2";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dMadeDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "cExpirationdate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "dvdate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产单位";
            dtbc.MappingName = "cinvdefine1";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dataGrid1.TableStyles.Add(dts);
            dataGrid1.RowHeadersVisible = true;
            dts.MappingName = _siList.GetType().Name;
            this.dataGrid1.DataSource = ds;
            #endregion
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (silist.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;

                }
                if (MessageBox.Show("确定要删除吗？" + this.silist[dataGrid1.CurrentRowIndex].cinvcode,
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int rindex = dataGrid1.CurrentRowIndex;
                    dataGrid1.DataSource = null;
                    //silist[rindex].iQuantity = 0;
                    silist.RemoveAt(rindex);
                    List<CheckDetail> ds = this.silist;//this.silist.FindAll(delegate(CheckDetail v) { return v.iQuantity > 0; });
                    dataGrid1.DataSource = ds;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}