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
    public partial class frmQuantitySearch : Form
    {
        public frmQuantitySearch()
        {
            InitializeComponent();    
        }
        
        /// <summary>
        /// 结果集合
        /// </summary>
        private List<IQuantitySearch> list = new List<IQuantitySearch> ();

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQuantitySearch_Load(object sender, EventArgs e)
        {
            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "仓库名称";
            dtbc.MappingName = "cWhName";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cInvCode";
            dtbc.Width = 80;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cInvName";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格";
            dtbc.MappingName = "cInvStd";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);
            
            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批次";
            dtbc.MappingName = "cBatch";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位";
            dtbc.MappingName = "cPosCode";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iQuantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "cInvDefine6";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产厂家";
            dtbc.MappingName = "cInvDefine1";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dMdate";
            dtbc.Width = 90;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "cExpirationdate";
            dtbc.Width = 90;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "dVDate";
            dtbc.Width = 90;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "保质期";
            dtbc.MappingName = "iMassDate";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dataGrid1.TableStyles.Add(dts);
            //dataGrid1.RowHeadersVisible = true;
            dts.MappingName = list.GetType().Name;

            txtCBarCode.Focus();
        }

        /// <summary>
        /// 类型选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            txtCBarCode.Focus();
        }

        /// <summary>
        /// 输入条码后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //获取条码
            string cBarCode = txtCBarCode.Text.Trim();
            if (!string.IsNullOrEmpty(cBarCode) && e.KeyChar == (char)Keys.Enter)
            {
                bool isPosition = !rbType1.Checked;//货位存量查询
                string errMsg;
                int result = U8Business.Common.QuantitySerarch(cBarCode, isPosition, out list, out errMsg);
                if (result == 0)
                {
                    if (list == null)
                    {
                        MessageBox.Show("没有查询到数据");
                        dataGrid1.DataSource = null;
                        return;
                    }
                    dataGrid1.DataSource = null;
                    dataGrid1.DataSource = list;
                }
                else
                {
                    MessageBox.Show("查询失败：" + errMsg);
                }
                txtCBarCode.Text = "";
                txtCBarCode.Focus();
            }
            else if(e.KeyChar==(char)Keys.Escape)//如果按下esc，则退出窗体
            {
                this.Close();
            }

        }

        /// <summary>
        /// 点击返回关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}