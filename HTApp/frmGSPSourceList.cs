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
    public partial class frmGSPSourceList : Form
    {
        public frmGSPSourceList(List<GSPVouchDetail> ls)
        {
            InitializeComponent();
            if (ls == null || ls.Count < 1)
            {
                return;
            }

            DataGridTableStyle dts = new DataGridTableStyle();

            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "仓库名称";
            dtbc.MappingName = "cwhname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
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
            dtbc.MappingName = "cinvm_unit";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "保质期";
            dtbc.MappingName = "imassdate";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "cbatch";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位";
            dtbc.MappingName = "cposition";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iquantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "客户全称";
            dtbc.MappingName = "cdefine11";
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