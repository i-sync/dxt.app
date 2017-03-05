using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using U8Business;
using Model;
using System.Threading;
using System.Xml;

namespace HTApp
{
    public partial class frmLogin : Form
    {
        /// <summary>
        /// 临时存储用户名
        /// </summary>
        string username = string.Empty;
        /// <summary>
        /// 当前程序版本
        /// </summary>
        string version = string.Empty;
        /// <summary>
        /// 服务器web地址
        /// </summary>
        string url = string.Empty;
        Thread thread;

        public frmLogin()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtYear.Text = DateTime.Now.Year.ToString();
                Common.CurrentUser = new User();
                Common.CurrentUser.ServiceUrl = url = OperationXml.ServiceUrl();//WEBSERVICE的url记录
                txtUserCode.Text = OperationXml.getConfig("login", "name");
                this.labversion.Text = version = Assembly.LoadFrom("htapp.exe").GetName().Version.ToString();

                this.txtUserCode.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                Close();
            }

            //获取服务器程序最新版本
            string ver = Common.GetInstance().GetVersion();
            if (!ver.Equals(version))
            {
                thread = new Thread(new ThreadStart(UpdateProrgram));
                thread.Start();
            }

        }

        /// <summary>
        /// 更新程序 
        /// </summary>
        private void UpdateProrgram()
        {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            
            //下载最新的XML文档
            string content = Common.GetInstance().GetNewDocument();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            doc.Save(filePath + "\\update.xml");

            System.Diagnostics.Process.Start(filePath + "\\update.exe", string.Format("{0} {1}", System.Diagnostics.Process.GetCurrentProcess().Id, url));
            
            thread.Abort();
        }

        /// <summary>
        /// 输入用户名后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        /// <summary>
        /// 用户名获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserCode_GotFocus(object sender, EventArgs e)
        {
            //获取用户名信息
            username = txtUserCode.Text.Trim();
        }

        /// <summary>
        /// 用户名失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserCode_LostFocus(object sender, EventArgs e)
        {
            //获取用户名信息
            string username = txtUserCode.Text.Trim();
            //判断用户名是否修改，若修改则清空账套信息
            if (!this.username.Equals(username))
            {
                this.cmbAccId.DataSource = null;
                this.username = username;

                this.lblMessage.Text = string.Empty;
            }
        }

        /// <summary>
        /// 输入密码后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtYear.Focus();
                txtYear.SelectAll();
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Up)
                txtPassword.Focus();
            else if (e.KeyChar == (char)Keys.Down)
                btnLogin.Focus();
            else if (e.KeyChar != 13 || this.txtYear.Text == "")
                return;
            //this.txtaccid.Focus();
            this.cmbAccId.Focus();
        }

        /// <summary>
        /// 账套获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccId_GotFocus(object sender, EventArgs e)
        {
            //首先判断数据源是否为空
            if (this.cmbAccId.DataSource == null)
            {
                //根据用户名重新读取数据
                Cursor.Current = Cursors.WaitCursor;
                DataTable dt = Common.GetUAAcountInfo(username);
                Cursor.Current = Cursors.Default;

                if (dt == null || dt.Rows.Count == 0)
                {
                    lblMessage.Text = "读取账套错误！不存在的用户名或已被注销";
                    txtUserCode.Focus();
                    txtUserCode.SelectAll();
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

                cmbAccId.DataSource = list;
                cmbAccId.DisplayMember = "Name";
                cmbAccId.ValueMember = "Key";

            }
        }

        /// <summary>
        /// 回车    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAccId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && this.cmbAccId.DataSource != null)
            {
                this.btnLogin_Click(null, null);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (cmbAccId.DataSource == null)
            {
                MessageBox.Show("请先选择账套！");
                return;
            }

            Common.CurrentUser.UserId = this.txtUserCode.Text.Trim();
            Common.CurrentUser.Password = this.txtPassword.Text.Trim();
            Common.CurrentUser.Year = this.txtYear.Text.Trim();
            Common.CurrentUser.Accid = (this.cmbAccId.SelectedItem as KV).Key.ToString();

            #region Login
            try
            {               

                this.lblMessage.Text = "正在登录......";
                this.lblMessage.Refresh();

                Common.UserLogin();
                GetBaseInfo();

                this.lblMessage.Text = "";
                //记录用户名
                OperationXml.setConfig("login", "name", username);
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = "";
                MessageBox.Show(ex.Message);
                txtUserCode.Focus();
                txtUserCode.SelectAll();
                return;
            }
            #endregion

            //隐藏登录窗体，显示主窗体
            frmMenu obj = new frmMenu(this);
            obj.Show();
            this.Hide();
        }


        #region GetBaseInfo
        private void GetBaseInfo()
        {
            try
            {
                //Common.s_cometence = null;
                //Common.s_DepartMent.Clear();
                //Common.s_packag.Clear();
                //Common.s_RD_Style.Clear();
                Common.s_Warehouse.Clear();

                Common.GetInformation();
                //Competence.getCompetence();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 退出应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
    }
}