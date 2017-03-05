using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace HTApp
{
    public partial class frmSaleOutList : Form
    {
        DispatchList dl;

        public frmSaleOutList(DispatchList dl)
        {
            InitializeComponent();
            if (dl.OperateDetails == null || dl.OperateDetails.Count < 1)
            {
                return;
            }
            this.dl = dl;
            DataGridTableStyle dts = new DataGridTableStyle();

            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cinvcode";
            dtbc.Width = 80;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cinvname";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iquantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "累计发货数量";
            dtbc.MappingName = "IFHQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "扫描数量";
            dtbc.MappingName = "inewquantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);
            
            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格";
            dtbc.MappingName = "cinvstd";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "cdefine22";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "主计量单位";
            dtbc.MappingName = "comunit";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "含税单价";
            dtbc.MappingName = "itaxunitprice";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "invbatch";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位";
            dtbc.MappingName = "cposition";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dmdate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "cexpirationdate";
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
            dtbc.HeaderText = "客户名称";
            dtbc.MappingName = "ccusname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "客户简称";
            dtbc.MappingName = "ccusabbname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dataGrid1.TableStyles.Add(dts);
            dataGrid1.RowHeadersVisible = true;
            dts.MappingName = dl.OperateDetails.GetType().Name;
            this.dataGrid1.DataSource = dl.OperateDetails;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dl.OperateDetails.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗？",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int rindex = dataGrid1.CurrentRowIndex;
                    decimal sub = dl.OperateDetails[rindex].inewquantity;
                    string cinvcode = dl.OperateDetails[rindex].cinvcode;
                    dataGrid1.DataSource = null;
                    dl.OperateDetails.RemoveAt(rindex);//删除操作数据
                    dataGrid1.DataSource = dl.OperateDetails;
                    /// TODO:批次与货位
                    DispatchDetail dd = dl.U8Details.Find(delegate(DispatchDetail tdd) { return tdd.cinvcode.Equals(cinvcode); });
                    dd.inewquantity -= sub;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

    }
}