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
    public partial class frmPurchaseArrivalDone : Form
    {
        private ArrivalVouch list;
        public frmPurchaseArrivalDone()
        {
            InitializeComponent();
        }

        public frmPurchaseArrivalDone(ArrivalVouch list)
            : this()
        {
            this.list = list;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurchaseArrivalDone_Load(object sender, EventArgs e)
        {
            if (list == null || list.OperateDetails.Count == 0)
                return;
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
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "cBatch";
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
            dts.MappingName = list.OperateDetails.GetType().Name;
            this.dataGrid1.DataSource = list.OperateDetails;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.OperateDetails.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗？",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int index = dataGrid1.CurrentRowIndex;
                    ArrivalVouchs avs = list.OperateDetails[index];
                    //存货编号及数量（批次）
                    decimal sub = avs.iScanQuantity;
                    string cinvcode = avs.cInvCode;
                    string cbatch = avs.cBatch;

                    dataGrid1.DataSource = null;
                    list.OperateDetails.RemoveAt(index);//删除操作数据
                    dataGrid1.DataSource = list.OperateDetails;

                    // 删除来源中已扫数量
                    ArrivalVouchs temp = list.U8Details.Find(delegate(ArrivalVouchs avtemp) { return avtemp.cInvCode.Equals(cinvcode); });
                    temp.iScanQuantity -= sub;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}