using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using U8Business;
using U8Business.Service;
using Model;

namespace HTApp
{
    public partial class frmDoneScanList : Form
    {
        List<ArrivalVouchs> arList;
        List<PoDetail> pdList = null;


        public frmDoneScanList(List<ArrivalVouchs> _arList, List<PoDetail> _pdList)
        {
            InitializeComponent();

            arList = _arList;
            pdList = _pdList;


            DataGridTableStyle dts = new DataGridTableStyle();

            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "CInvCode";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);
            dataGrid1.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "IQuantity";
            dtbc.Width = 50;
            dts.GridColumnStyles.Add(dtbc);
            dataGrid1.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "订单号";
            dtbc.MappingName = "Cordercode";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);
            dataGrid1.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "IPOsID";
            dtbc.MappingName = "IPOsID";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);
            dataGrid1.TableStyles.Add(dts);

            dts.MappingName = arList.GetType().Name;
            this.dataGrid1.DataSource = arList;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (arList.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗？" + arList[dataGrid1.CurrentRowIndex].cInvCode,
          "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int SelectedIndex = dataGrid1.CurrentRowIndex;
                    arList.RemoveAt(SelectedIndex);//删除操作数据
                    pdList.RemoveAt(SelectedIndex);//删除来源数据
                    dataGrid1.DataSource = arList;
                    dataGrid1.Refresh();
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