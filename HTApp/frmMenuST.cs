using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenuST : Form
    {
        frmMenu menu;

        public frmMenuST()
        {
            InitializeComponent();
        }

        public frmMenuST(frmMenu frmMenu)
            :this()
        {
            this.menu = frmMenu;
        }

        private void frmMenuST_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Location = Point.Empty;

                //销售出库红字
                pbSellOut.Enabled = Common.s_Competence.XSCK;
                //采购入库
                pbPurIn.Enabled = Common.s_Competence.CGRK;
                //采购退货
                pbPurRefund.Enabled = Common.s_Competence.CGRK;
                //材料出库
                pbStuffOut.Enabled = Common.s_Competence.CLCK;
                //产成品入库
                pbProIn.Enabled = Common.s_Competence.CCPRK;
                //盘点
                pbCheck.Enabled = Common.s_Competence.PD;
                //其它出库
                pbAllotOut.Enabled = pbPackOut.Enabled = pbApartOut.Enabled = Common.s_Competence.QTCK;
                //其它入库
                pbAlloIn.Enabled = pbPackIn.Enabled = pbApartIn.Enabled = Common.s_Competence.QTRK;

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

        private void pbST_Click(object sender, EventArgs e)
        {
            plVerify.Visible = !plVerify.Visible;
            plMake.Visible = !plMake.Visible;
        }

        private void pbSellOut_Click(object sender, EventArgs e)
        {
            using (frmSaleOutRed frmSOR = new frmSaleOutRed())
            {
                //this.Hide();
                frmSOR.ShowDialog();
                this.Show();
            }
        }

        private void pbPurIn_Click(object sender, EventArgs e)
        {
            using (frmPUIn frmPI = new frmPUIn())
            {
                //this.Hide();
                frmPI.ShowDialog();
                this.Show();
            }
        }

        private void pbPurRefund_Click(object sender, EventArgs e)
        {
            using (frmPURefund frmPR = new frmPURefund())
            {
                //this.Hide();
                frmPR.ShowDialog();
                this.Show();
            }
        }

        private void pbStuffOut_Click(object sender, EventArgs e)
        {
            using (frmOSStuffOut frmOSO = new frmOSStuffOut())
            {
                //this.Hide();
                frmOSO.ShowDialog();
                this.Show();
            }
        }

        private void pbProIn_Click(object sender, EventArgs e)
        {
            using (frmSTInProduct frmSIP = new frmSTInProduct())
            {
                //this.Hide();
                frmSIP.ShowDialog();
                this.Show();
            }
        }

        private void pbCheck_Click(object sender, EventArgs e)
        {
            using (frmCheck frmCK = new frmCheck())
            {
                //this.Hide();
                frmCK.ShowDialog();
                this.Show();
            }
        }

        private void pbAllotOut_Click(object sender, EventArgs e)
        {
            using (frmAllotOut frmAO = new frmAllotOut())
            {
                //this.Hide();
                frmAO.ShowDialog();
                this.Show();
            }
        }

        private void pbAlloIn_Click(object sender, EventArgs e)
        {
            using (frmAllotIn frmAI = new frmAllotIn())
            {
                //this.Hide();
                frmAI.ShowDialog();
                this.Show();
            }
        }

        private void pbPackOut_Click(object sender, EventArgs e)
        {
            using (frmPAHalfOut frmPHO = new frmPAHalfOut())
            {
                //this.Hide();
                frmPHO.ShowDialog();
                this.Show();
            }
        }

        private void pbPackIn_Click(object sender, EventArgs e)
        {
            using (frmPAFinalIn frmPFI = new frmPAFinalIn())
            {
                //this.Hide();
                frmPFI.ShowDialog();
                this.Show();
            }
        }

        private void pbApartOut_Click(object sender, EventArgs e)
        {
            using (frmDIFinalOut frmDFO = new frmDIFinalOut())
            {
                //this.Hide();
                frmDFO.ShowDialog();
            }
        }

        private void pbApartIn_Click(object sender, EventArgs e)
        {
            using (frmDIHalfIn frmDHI = new frmDIHalfIn())
            {
                //this.Hide();
                frmDHI.ShowDialog();
                this.Show();
            }
        }

        private void frmMenuST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
            else if (e.KeyChar == (char)Keys.Right || e.KeyChar == (char)Keys.Up || e.KeyChar == (char)Keys.Left || e.KeyChar == (char)Keys.Down)
            {
                pbST_Click(sender, e);
            }
        }

        private void frmMenuST_Closed(object sender, EventArgs e)
        {
            this.menu.Show();
            this.menu.Activate();
        }
    }
}