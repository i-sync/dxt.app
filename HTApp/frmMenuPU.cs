using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenuPU : Form
    {
        frmMenu menu;

        public frmMenuPU()
        {
            InitializeComponent();
        }

        public frmMenuPU(frmMenu frmMenu)
            :this()
        {
            this.menu = frmMenu;
        }

        private void frmMenuPU_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Location = Point.Empty;

                //采购到货
                pbPUArrival.Enabled = Common.s_Competence.CGDH;

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

        private void pbPUArrival_Click(object sender, EventArgs e)
        {
            //using (frmPUArrival frmPA = new frmPUArrival())
            //{
            //    ////this.Hide();
            //    frmPA.ShowDialog();
            //    this.Show();
            //}
            using (frmPurchaseArrival form = new frmPurchaseArrival())
            {
                form.ShowDialog();
            }
        }

        private void frmMenuPU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
        }

        private void frmMenuPU_Closed(object sender, EventArgs e)
        {
            this.menu.Show();
            this.menu.Activate();
        }
    }
}