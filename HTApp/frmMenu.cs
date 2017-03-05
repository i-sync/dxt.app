using System;
using System.Drawing;
using System.Windows.Forms;

using U8Business;

namespace HTApp
{
    public partial class frmMenu : Form
    {
        frmLogin login;
        public frmMenu()
        {
            InitializeComponent();
        }

        public frmMenu(frmLogin login)
            : this()
        {
            this.login = login;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            this.Location = Point.Empty;
            Common.s_Competence = Common.s_Competence == null ? new Model.Competence() : Common.s_Competence;
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("确定要退出吗?", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (res == DialogResult.No)
            {
                return;
            }

            Application.Exit();
        }

        private void pbSell_Click(object sender, EventArgs e)
        {
            using (frmMenuSO frmSO = new frmMenuSO(this))
            {
                //this.Hide();
                frmSO.ShowDialog();
            }
        }

        private void pbPurchase_Click(object sender, EventArgs e)
        {
            using (frmMenuPU frmPU = new frmMenuPU(this))
            {
                //this.Hide();
                frmPU.ShowDialog();
            }
        }

        private void pbOutsourcing_Click(object sender, EventArgs e)
        {
            using (frmMenuOM frmOM = new frmMenuOM(this))
            {
                //this.Hide();
                frmOM.ShowDialog();
            }
        }

        private void pbGSP_Click(object sender, EventArgs e)
        {
            using (frmMenuGSP frmGSP = new frmMenuGSP(this))
            {
                //this.Hide();
                frmGSP.ShowDialog();
            }
        }

        private void pbStock_Click(object sender, EventArgs e)
        {
            using (frmMenuST frmST = new frmMenuST(this))
            {
                //this.Hide();
                frmST.ShowDialog();
            }
        }

        private void pbCurrent_Click(object sender, EventArgs e)
        {
            using (frmQuantitySearch frmCS = new frmQuantitySearch())
            {
                //this.Hide();
                frmCS.ShowDialog();
            }
        }

        private void frmMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                miExit_Click(sender, e);
            }
        }


        /// <summary>
        /// 设置PictureBox图片
        /// </summary>
        /// <param name="con">需处理的控件</param>
        public void SetEnabled(Control con)
        {
            string sType = con.GetType().ToString();
            if (sType == "System.Windows.Forms.PictureBox" && !con.Enabled)
            {
                ((PictureBox)con).Image = Properties.Resources.ban;
            }
            else if (sType == "System.Windows.Forms.Panel")
            {
                foreach (Control cons in ((Panel)con).Controls)
                {
                    SetEnabled(cons);
                }
            }
        }
    }
}