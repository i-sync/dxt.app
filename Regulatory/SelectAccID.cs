using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Regulatory
{
    public partial class SelectAccID : Form
    {
        public SelectAccID()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAccID_Load(object sender, EventArgs e)
        {

            cmbAccID.GotFocus += new EventHandler(cmbAccID_GotFocus);
            
        }

        /// <summary>
        /// 获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccID_GotFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (cmbAccID.DataSource == null)
            {
                Service.Service service = new Regulatory.Service.Service();
                DataTable dt = service.GetUAAcountInfo(null);
                if (dt == null)
                {
                    MessageBox.Show("账套读取错误！");
                    return;
                }

                List<KV> list = new List<KV>();
                KV kv;

                foreach (DataRow row in dt.Rows)
                {
                    kv = new KV();
                    kv.Key = row["code"];
                    kv.Value = row["name"].ToString();
                    list.Add(kv);
                }

                cmbAccID.DataSource = list;
                cmbAccID.DisplayMember = "Name";
                cmbAccID.ValueMember = "Key";

            }
        }

        /// <summary>
        /// 点击确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            KV kv = cmbAccID.SelectedItem as KV;
            if (kv == null)
            {
                MessageBox.Show("请选择账套");
                return;
            }

            MainForm mainForm = new MainForm(kv.Key.ToString());
            mainForm.Show();
            this.Hide();
        }

        /// <summary>
        /// 点击取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
