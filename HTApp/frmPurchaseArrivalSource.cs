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
    public partial class frmPurchaseArrivalSource : Form
    {
        public frmPurchaseArrivalSource()
        {
            InitializeComponent();
        }

        public frmPurchaseArrivalSource(List<ArrivalVouchs> list)
            : this()
        {
            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "订单编号";
            dtbc.MappingName = "cOrderCode";
            dtbc.Width = 100;
            dtbc.Format = "G";
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
            dtbc.HeaderText = "主计量";
            dtbc.MappingName = "cInvm_Unit";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "Quantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "已到货数量";
            dtbc.MappingName = "iArrQty";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "已扫数量";
            dtbc.MappingName = "iScanQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "原币单价";
            dtbc.MappingName = "iunitprice";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "原币含税单价";
            dtbc.MappingName = "iTaxPrice";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "原币无税金额";
            dtbc.MappingName = "iMoney";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "原币税额";
            dtbc.MappingName = "iTax";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "原币价税合计";
            dtbc.MappingName = "iSum";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "税率";
            dtbc.MappingName = "itaxrate";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);


            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "计划到货日期";
            dtbc.MappingName = "dArriveDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "供应商简称";
            dtbc.MappingName = "cVenAbbName";
            dtbc.Width = 150;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);

            dataGrid1.TableStyles.Add(dts);
            dataGrid1.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;
            this.dataGrid1.DataSource = list;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}