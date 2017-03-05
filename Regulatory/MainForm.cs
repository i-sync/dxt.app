using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using Model;

namespace Regulatory
{
    public partial class MainForm : Form
    {
        private int pageIndex = 1;
        private int pageSize = 20;
        private Service.Service service;
        private string connectionString;
        /// <summary>
        /// 账套号
        /// </summary>
        private string accID;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string accID)
            : this()
        {
            this.accID = accID;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //名称
            this.Text = string.Format("[{0}]{1}",accID,this.Text);

            service = new Regulatory.Service.Service();
            //绑定数据
            List<KV> list = new List<KV>();
            list.Add(new KV() { Key = -1, Value = "全部" });
            list.Add(new KV() { Key = 0, Value = "未使用" });
            list.Add(new KV() { Key = 1, Value = "已使用" });
            cmbIsUsed.DataSource = list;
            cmbIsUsed.DisplayMember = "Value";
            cmbIsUsed.ValueMember = "Key";

            //获取配置文件
            pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            //禁止自动生成列
            dgvRegulatory.AutoGenerateColumns = false;
        }
        /// <summary>
        /// 批量导入监管码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertReg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "excel files|*.xls";
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //获取execl表的数据
                DataSet ds = ExcelToDataSet(openFileDialog.FileName);
                if (ds == null)
                    return;
                Service.Regulatory data;
                string errMsg = string.Empty;
                //存储错误信息
                string info = string.Empty;
                //循环监管添加
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    data = new Regulatory.Service.Regulatory();
                    if (row[0].ToString().Length != 32)
                        continue;
                    data.RegCode = row[0].ToString();
                    data.AccID = accID;
                    //判断是否存在
                    bool flag = service.ExistsRegulatory(connectionString, data);
                    if (flag)//说明已存在 跳过
                    {
                        info += string.Format("监管码：{0}已存在！\r\n", row[0].ToString());
                        continue;
                    }
                    //添加监管码
                    flag = service.AddRegulatory(connectionString, data, out errMsg);
                    if (!flag)
                    {
                        info += string.Format("监管码：{0}添加失败！\r\n", row[0].ToString());
                        break;
                    }
                }
                if(!string.IsNullOrEmpty(info))
                    MessageBox.Show("添加完成，详细信息如下：\r\n"+info);
                btnSearch_Click(null, null);
            }
        }

          /// <summary>
        /// 读取Excel文件，将内容存储在DataSet中
        /// </summary>
        /// <param name="opnFileName">带路径的Excel文件名</param>
        /// <returns>DataSet</returns>
        private DataSet ExcelToDataSet(string openFileName)
        {
            string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=NO;\"", openFileName);
            OleDbConnection conn = new OleDbConnection(strConn);
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = new DataSet();

            strExcel = "select * from [sheet1$]";
            try
            {
                conn.Open();
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, "dtSource");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入出错：" + ex, "错误信息");
                ds = null;
                return ds;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }


        /// <summary>
        /// 根据条件进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //根据条件检索
            pageIndex = 1;

            //获取界面数据
            string regCode = txtRegCode.Text.Trim();
            string cardNumber = txtCardNumber.Text.Trim();
            string cardName = txtCardName.Text.Trim();
            string cardCode = txtCardCode.Text.Trim();
            int isUsed = (int)(cmbIsUsed.SelectedItem as KV).Key;

            SearchData(regCode, cardNumber, cardName, cardCode, isUsed);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="regCode"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="cardCode"></param>
        /// <param name="isUsed"></param>
        private void SearchData(string regCode,string cardNumber,string cardName,string cardCode,int isUsed)
        {
            //封装数据
            Service.Regulatory data = new Regulatory.Service.Regulatory();
            data.RegCode = regCode;
            data.CardNumber = cardNumber;
            data.CardName = cardName;
            data.CardCode = cardCode;
            data.IsUsed = isUsed;
            data.PageIndex = pageIndex;
            data.PageSize = pageSize;

            //
            data.AccID = accID;

            //查询数据
            int total;
            DataTable dt = service.GetRegulatoryList(connectionString, data,out total);
            dgvRegulatory.DataSource = dt;
            lblPageC.Text = pageIndex.ToString();
            if (total == 0)
            {
                total = 1;
            }
            lblPageT.Text = total%pageSize==0?(total/pageSize).ToString():(total/pageSize +1).ToString();
            
            btnControl();
        }


        #region 分页设置

        /// <summary>
        /// 点击分布控件按钮事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPage_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int total = Convert.ToInt32(lblPageT.Text);
            ///判断按钮类型
            switch (btn.Name)
            { 
                case "btnFrist":
                    pageIndex = 1;
                    break;
                case "btnPrev":
                    pageIndex--;
                    break;
                case "btnNext":
                    pageIndex++;
                    break;
                case "btnLast":
                    pageIndex = total;
                    break;
            }

            //获取界面数据
            string regCode = txtRegCode.Text.Trim();
            string cardNumber = txtCardNumber.Text.Trim();
            string cardName = txtCardName.Text.Trim();
            string cardCode = txtCardCode.Text.Trim();
            int isUsed = (int)(cmbIsUsed.SelectedItem as KV).Key;

            SearchData(regCode, cardNumber, cardName, cardCode, isUsed);

        }

        //控制按钮是否可用
        private void btnControl()
        {
            btnFrist.Enabled = btnPrev.Enabled = btnNext.Enabled = btnLast.Enabled = true;
            int total = Convert.ToInt32(lblPageT.Text);
            if (pageIndex == 1)//当前为第一页
            {
                btnFrist.Enabled = false;
                btnPrev.Enabled = false;
                if (pageIndex == total)//说明共一页
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
            }
            else if(pageIndex==total)//当前为最后一页
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGO_Click(object sender, EventArgs e)
        {
            //如果为空直接返回
            if(string.IsNullOrEmpty(txtGO.Text.Trim()))
            {
                return;
            }
            int pageC;
            if (!int.TryParse(txtGO.Text.Trim(), out pageC))
            {
                MessageBox.Show("请输入正确的数字");
                txtGO.Text = string.Empty;
                return;
            }
            int total = Convert.ToInt32(lblPageT.Text);

            if (pageC <= 0 || pageC > total)
            {
                MessageBox.Show("请输入正确的页数");
                txtGO.Text = string.Empty;
                return;
            }

            pageIndex = pageC;
            //获取界面数据
            string regCode = txtRegCode.Text.Trim();
            string cardNumber = txtCardNumber.Text.Trim();
            string cardName = txtCardName.Text.Trim();
            string cardCode = txtCardCode.Text.Trim();
            int isUsed = (int)(cmbIsUsed.SelectedItem as KV).Key;

            SearchData(regCode, cardNumber, cardName, cardCode, isUsed);

            //清空
            txtGO.Text = string.Empty;
        }

        #endregion

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    
}
