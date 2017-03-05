using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HTApp
{
    public partial class frmExpressOrderSBV :Form
    {
        public frmExpressOrderSBV()
        {
            InitializeComponent();
        }
        private Model.SaleBillVouch salBillVouch;

        private void Clear()
        {
            txtOrder.Text = string.Empty;
            txtExpressOrder.Text = string.Empty;
            lblMaker.Text = string.Empty;
            lblDate.Text = string.Empty;
            lblCusName.Text = string.Empty;
            salBillVouch = null;
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExpressOrderSBV_Load(object sender, EventArgs e)
        {
            //加载快递方式
            List<Model.ShippingChoice> sc = U8Business.ExpressOrderBusiness.GetShoppingChoiceList();
            cmbExpress.DataSource = sc;
            cmbExpress.ValueMember = "cSCCode";
            cmbExpress.DisplayMember = "cSCName";
            Clear();
        }

        /// <summary>
        /// 输入单据号后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            string order = txtOrder.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(order))
            {
                string errMsg;
                salBillVouch = U8Business.ExpressOrderBusiness.GetSaleBillVouchByCSBVCode(order, out errMsg);
                if (salBillVouch == null)
                {
                    MessageBox.Show(errMsg);
                    return;
                }
                lblMaker.Text = salBillVouch.cMaker;
                lblDate.Text = salBillVouch.dDate.ToShortDateString();
                lblCusName.Text = salBillVouch.cCusName;
                cmbExpress.SelectedValue = salBillVouch.cSCCode;
                txtExpressOrder.Focus();
                txtExpressOrder.SelectAll();
            }
        }

        /// <summary>
        /// 点击提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            string express = cmbExpress.SelectedValue.ToString();
            string expressOrder = txtExpressOrder.Text.Trim();

            if (salBillVouch == null)
            {
                MessageBox.Show("请查询先查询单据！");
                return;
            }
            if (express.Equals("00"))
            {
                MessageBox.Show("请选择快递方式！");
                return;
            }
            if (string.IsNullOrEmpty(expressOrder))
            {
                MessageBox.Show("请输入快递单号！");
            }
            salBillVouch.cSCCode = express;
            salBillVouch.cDefine13 = expressOrder;
            Cursor.Current = Cursors.WaitCursor;
            bool flag = U8Business.ExpressOrderBusiness.UpdateSaleBillVouchExpressOrder(salBillVouch);
            Cursor.Current = Cursors.Default;
            if (flag)
            {
                MessageBox.Show("提交成功！");
                Clear();
            }
            else
            {
                MessageBox.Show("提交失败！");
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}