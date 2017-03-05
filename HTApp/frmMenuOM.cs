using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenuOM : Form
    {
        frmMenu menu;

        public frmMenuOM()
        {
            InitializeComponent();
        }

        public frmMenuOM(frmMenu frmMenu)
            :this()
        {
            this.menu = frmMenu;
        }

        private void frmMenuOM_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Location = Point.Empty;

                //委外到货
                pbOSArrival.Enabled = Common.s_Competence.WWDH;

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

        private void pbOSArrival_Click(object sender, EventArgs e)
        {
            using (frmOSArrival frmOA = new frmOSArrival())
            {
                //this.Hide();
                frmOA.ShowDialog();
                this.Show();
            }
        }

        private void frmMenuOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
        }

        private void frmMenuOM_Closed(object sender, EventArgs e)
        {
            this.menu.Show();
            this.menu.Activate();
        }
    }
}