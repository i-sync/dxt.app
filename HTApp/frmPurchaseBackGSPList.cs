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
    public partial class frmPurchaseBackGSPList : Form
    {
        PurchaseBackVouch pv;

        public frmPurchaseBackGSPList(PurchaseBackVouch pv)
        {
            InitializeComponent();
            if (pv.OperateDetails == null || pv.OperateDetails.Count < 1)
            {
                return;
            }
            this.pv = pv;
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

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "扫描数量";
            dtbc.MappingName = "ScanCount";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dMadeDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "dValDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dataGrid1.TableStyles.Add(dts);
            dataGrid1.RowHeadersVisible = true;
            dts.MappingName = pv.OperateDetails.GetType().Name;
            this.dataGrid1.DataSource = pv.OperateDetails;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (pv.OperateDetails.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗？",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //int rindex = dataGrid1.CurrentRowIndex;
                    //dataGrid1.DataSource = null;
                    //dl.RemoveAt(rindex);//删除操作数据
                    //dataGrid1.DataSource = dl;

                    int rindex = dataGrid1.CurrentRowIndex;
                    //已扫描数量
                    decimal sub = pv.OperateDetails[rindex].ScanCount;
                    string cinvcode = pv.OperateDetails[rindex].cInvcode;
                    string cbatch = pv.OperateDetails[rindex].cBatch;
                    dataGrid1.DataSource = null;
                    pv.OperateDetails.RemoveAt(rindex);//删除操作数据
                    dataGrid1.DataSource = pv.OperateDetails;
                    //查询时同时根据存货编码与批次
                    PurchaseBackDetail dd = pv.U8Details.Find((delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(cinvcode) && tdd.cBatch.Equals(cbatch); }));
                    dd.ScanCount -= sub;
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