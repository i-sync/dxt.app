using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace HTApp
{
    public partial class frmSaleOutSourceList : Form
    {
        public frmSaleOutSourceList(List<DispatchDetail> ls)
        {
            InitializeComponent();
            if (ls == null || ls.Count < 1)
            {
                return;
            }

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
            dts.MappingName = ls.GetType().Name;
            this.dataGrid1.DataSource = ls;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}