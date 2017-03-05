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
    public partial class frmPosition : Form
    {
        List<InvPositionInfo> posList;

        public frmPosition(List<InvPositionInfo> tempPos)
        {
            InitializeComponent();

            posList = tempPos;

            PositionDone();
        }

        private void PositionDone()
        {
            DataGridTableStyle dts = new DataGridTableStyle();

            #region DataGridTextBoxColumn

            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位编码";
            dtbc.MappingName = "PosCode";
            dtbc.Width = 80;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "InvCode";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "InvName";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格型号";
            dtbc.MappingName = "InvStd";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "扫描数量";
            dtbc.MappingName = "Quantity";
            dtbc.Width = 100;
            dtbc.Format = "F4";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "Batch";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "Address";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "MadeDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "ExpirationDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "VDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgPosition.TableStyles.Add(dts);

            #endregion

            dts.MappingName = posList.GetType().Name;
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            BindData();
        }

        private void dgPosition_CurrentCellChanged(object sender, EventArgs e)
        {
            if (posList == null || posList.Count < 1)
            {
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgPosition.CurrentRowIndex;
                if (index < 0)
                {
                    MessageBox.Show("没有选择操作的数据！");
                    return;
                } 
                DialogResult dr = MessageBox.Show("确定要删除" + posList[index].PosCode + "货位上的" + posList[index].InvName + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    string cInvCode = posList[index].InvCode;
                    string cBatch = posList[index].Batch;
                    dgPosition.DataSource = null;
                    dgPosition.Refresh();
                    posList.RemoveAt(index);
                    BindData();
                }
                if (posList == null || posList.Count < 1)
                {
                    MessageBox.Show("暂无已扫描的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
            }
            catch
            {
                MessageBox.Show("操作失误,请重试!");
                return;
            }
        }

        private void BindData()
        {
            if (posList == null || posList.Count < 1)
            {
                dgPosition.DataSource = null;
                btnDelete.Enabled = false;
            }
            else
            {
                dgPosition.DataSource = posList;
                dgPosition.CurrentRowIndex = 0;
                btnDelete.Enabled = true;
            }
            dgPosition.Refresh();
        }
    }
}