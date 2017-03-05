using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenuSO : Form
    {
        frmMenu menu;

        public frmMenuSO()
        {
            InitializeComponent();
        }

        public frmMenuSO(frmMenu frmMenu)
            :this()
        {
            this.menu = frmMenu;
        }

        private void frmMenuSO_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Location = Point.Empty;

                //销售出库拣货
                pbSellOut.Enabled = Common.s_Competence.XSFH;
                //货位管理
                pbOutPos.Enabled = Common.s_Competence.HWGL;

                foreach (Control con in this.Controls)
                {
                    menu.SetEnabled(con);
                }
            }
            catch { return; }
            finally { Cursor.Current = Cursors.Default; }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbSellOut_Click(object sender, EventArgs e)
        {
            using (frmSaleOutPicking frmSOP = new frmSaleOutPicking())
            {
                //this.Hide();
                frmSOP.ShowDialog();
                this.Show();
            }
        }

        private void pbOutPos_Click(object sender, EventArgs e)
        {
            string errMsg;
            Cursor.Current = Cursors.WaitCursor;
            int result = U8Business.DispatchListBusiness.InsertInvPosition(out errMsg);
            Cursor.Current = Cursors.Default;
            if (result == -2) //表示没有要处理的出库单
            {
                MessageBox.Show("没有要处理的出库单");
            }
            else if (result == -1)
            {
                MessageBox.Show("处理出错：" + errMsg);
            }
            else
            {
                MessageBox.Show("处理完成！");
            }   
        }

        /// <summary>
        /// 销售发货单快递单号回写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbExpressOrderDPL_Click(object sender, EventArgs e)
        {
            using (frmExpressOrderDPL frmDPL = new frmExpressOrderDPL())
            {
                frmDPL.ShowDialog();
            }
        }

        /// <summary>
        /// 销售发票快递单号回写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbExpressOrderSBV_Click(object sender, EventArgs e)
        {
            using (frmExpressOrderSBV frmSBV = new frmExpressOrderSBV())
            {
                frmSBV.ShowDialog();
            }
        }

        private void frmMenuSO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
        }

        private void frmMenuSO_Closed(object sender, EventArgs e)
        {
            this.menu.Show();
            this.menu.Activate();
        }
    }
}