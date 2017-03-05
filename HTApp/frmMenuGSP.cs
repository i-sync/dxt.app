using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenuGSP : Form
    {
        frmMenu menu;

        public frmMenuGSP()
        {
            InitializeComponent();
        }

        public frmMenuGSP(frmMenu frmMenu)
            :this()
        {
            this.menu = frmMenu;
        }

        private void frmMenuGSP_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Location = Point.Empty;

                //销售出库GSP
                pbSellOut.Enabled = Common.s_Competence.XSCKGSP;
                //销售退货GSP
                pbSellRefund.Enabled = Common.s_Competence.XSTHGSP;
                //采购退货GSP
                pbPURefund.Enabled = Common.s_Competence.CGTHGSP;

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
            using (frmSaleOutGSP frmSO = new frmSaleOutGSP())
            {
                //this.Hide();
                frmSO.ShowDialog();
                this.Show();
            }
        }

        private void pbSellRefund_Click(object sender, EventArgs e)
        {
            using (frmSaleBackGSP frmSB = new frmSaleBackGSP())
            {
                //this.Hide();
                frmSB.ShowDialog();
                this.Show();
            }
        }

        private void pbPURefund_Click(object sender, EventArgs e)
        {
            using (frmPurchaseBackGSP frmPR = new frmPurchaseBackGSP())
            {
                //this.Hide();
                frmPR.ShowDialog();
                this.Show();
            }
        }

        private void frmMenuGSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
        }

        private void frmMenuGSP_Closed(object sender, EventArgs e)
        {
            this.menu.Show();
            this.menu.Activate();
        }
    }
}