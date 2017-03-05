using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using U8Business;
using System.Linq;

namespace HTApp
{
    public partial class frmPurchaseBackGSP : Form
    {
        PurchaseBackVouch backgsp;
        PurchaseBackDetail dd;

        public frmPurchaseBackGSP()
        {
            InitializeComponent();

            backgsp = new PurchaseBackVouch();
            lblWarehouse.Text = "";
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblcBatch.Text = "";
            lblProAddress.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblScanedNum.Text = "";

            this.cmbCInstance.SelectedIndex = 0;
        }

        /// <summary>
        /// 判断是否为二维码
        /// </summary>
        public bool IsQR
        {
            get
            {
                return lblcBatch.Visible;
            }
            set
            {
                //批次选择的显示与隐藏
                cmbCBatch.Visible = !value;
                cmbCBatch.Enabled = !value;
                lblcBatch.Visible = value;
            }
        }

        /// <summary>
        /// 扫描来源单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            //扫描采购入库单红字单号
            if (e.KeyChar == 13 && txtSource.Text.Length > 0)
            {
                string errMsg = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (PurchaseBackBusiness.GetPurchaseBack(txtSource.Text, out backgsp, out errMsg))
                    {
                        btnSource.Enabled = true;
                        txtLable.Enabled = true;
                        txtLable.Focus();
                        lblWarehouse.Text = backgsp.cWhName;
                        //入库单（红）文本框不可再用
                        txtSource.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("采购入库单红字单号错误:" + errMsg);
                        btnSource.Enabled = false;
                        txtLable.Enabled = false;
                        txtSource.SelectAll();
                        txtSource.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnSource.Enabled = false;
                    txtLable.Enabled = false;
                    txtSource.SelectAll();
                    txtSource.Focus();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 扫描成品标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLable_KeyPress(object sender, KeyPressEventArgs e)
        {
            //成品标签扫描
            if (e.KeyChar == 13 && txtLable.Text.Length > 0)
            {
                try
                {
                    //string[] barcode = txtLable.Text.Split('@');
                    //首先判断扫描的是一维条码还是二维条码，条件是否包含@
                    string strBarcode = txtLable.Text.Trim();
                    string[] barcode = new string[7] { "", "", "", "", "", "", "" };
                    if (strBarcode.IndexOf('@') == -1)//没有找到@，说明该条码是一维条码
                    {
                        string errMsg = string.Empty;
                        string cInvCode = string.Empty;//存货编码
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(strBarcode, out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！" + errMsg);
                            return;
                        }
                        barcode[2] = cInvCode;
                        IsQR = false;
                    }
                    else //二维码
                    {
                        barcode = strBarcode.Split('@');
                        IsQR = true;
                    }

                    /*
                    dd = backgsp.U8Details.Find(delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(barcode[1]); });//&& tdd.cBatch.Equals(barcode[3]); });
                    if (dd == null)
                    {
                        MessageBox.Show("条码错误:存货编码或批号不在单据中");
                        txtLable.SelectAll();
                        txtLable.Focus();
                        txtCount.Enabled = false;
                    }
                    else
                    {
                        //计算失败日期
                        DateTime lastDate = Convert.ToDateTime(dd.dMadeDate).AddMonths(dd.imassDate);
                        dd.dValDate = lastDate.ToString("yyyy-MM-dd");
                        dd.CValDate = lastDate.AddDays(-1).ToString("yyyy-MM-dd");
                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.cdefine22;

                        //再次判断OPerateDetail中是否存在与dd一样的对象，若有则取对象的数据
                        PurchaseBackDetail temp = backgsp.OperateDetails.Find(delegate(PurchaseBackDetail p) { return p.cInvcode.Equals(dd.cInvcode) && p.cBatch.Equals(dd.cBatch); });
                        if(temp !=null)
                        {
                            lblScanedNum.Text = temp.ScanCount.ToString("F3");
                        }
                        else
                        {
                            lblScanedNum.Text = dd.ScanCount.ToString("F3");
                        }
                        lblcBatch.Text = dd.cBatch;
                        lblProDate.Text = Convert.ToDateTime(dd.dMadeDate).ToString("yyyy-MM-dd");//dd.dMadeDate.Substring(0, 10);
                        lblValidDate.Text = dd.CValDate.Substring(0, 10);
                        txtCount.Enabled = true;
                        txtCount.SelectAll();
                        txtCount.Focus();

                    }
                     * 
                     * */

                    //如果是二维码
                    if (IsQR)
                    {
                        //按存货编码和批次查询
                        dd = backgsp.U8Details.Find(delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(barcode[2]) && tdd.cBatch.Equals(barcode[3],StringComparison.CurrentCultureIgnoreCase); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.cdefine22;
                        lblcBatch.Text = dd.cBatch;
                        lblScanedNum.Text = dd.ScanCount.ToString("F2");
                        lblProDate.Text = Convert.ToDateTime(dd.dMadeDate).ToString("yyyy-MM-dd");//dd.dmadedate.Substring(0, 10);
                        lblValidDate.Text = Convert.ToDateTime(dd.dMadeDate).AddMonths(dd.imassDate).AddDays(-1).ToString("yyyy-MM-dd");//Convert.ToDateTime(dd.dValDate).ToString("yyyy-MM-dd");// dd.dvdate.Substring(0, 10);
                        //txtCount.Text = dd.iquantity.ToString("F3");
                        txtCount.Enabled = true;
                        txtCount.Focus();

                    }
                    //如果是一维码
                    else
                    {
                        //只按存货编码查询
                        dd = backgsp.U8Details.Find(delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(barcode[2]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        //然后查询该存货编码的所有批次绑定到cmbCbatch中
                        var v = from t in backgsp.U8Details where t.cInvcode == dd.cInvcode select t;
                        cmbCBatch.DataSource = new BindingSource(v, null);
                        cmbCBatch.DisplayMember = "cBatch";
                        cmbCBatch.ValueMember = "cBatch";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 批次选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///如果数据源为空直接返回
            if (cmbCBatch.DataSource == null)
                return;

            //获取选择的对象
            dd = cmbCBatch.SelectedItem as PurchaseBackDetail;
            lblInvName.Text = dd.cinvname;
            lblInvStd.Text = dd.cinvstd;
            lblProAddress.Text = dd.cdefine22;
            lblScanedNum.Text = dd.ScanCount.ToString("F2");
            //lblcBatch.Text = dd.cbatch;
            lblProDate.Text = Convert.ToDateTime(dd.dMadeDate).ToString("yyyy-MM-dd");
            lblValidDate.Text = Convert.ToDateTime(dd.dMadeDate).AddMonths(dd.imassDate).AddDays(-1).ToString("yyyy-MM-dd");// Convert.ToDateTime(dd.CValDate).ToString("yyyy-MM-dd");

            txtCount.Enabled = true;
            txtCount.Focus();
        }

        /// <summary>
        /// 输入数量后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCount.Text.Length > 0)
            {
                if (txtCount.Text.Trim() == "")
                {
                    MessageBox.Show("未输入数量！");
                    txtCount.SelectAll();
                    txtCount.Focus();
                    return;
                }
                if (!isNumeric(txtCount.Text))
                {
                    MessageBox.Show("请输入数字！");
                    txtCount.SelectAll();
                    txtCount.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtCount.Text) <= 0)
                {
                    MessageBox.Show("请输入正确的数量！");
                    txtCount.SelectAll();
                    txtCount.Focus();
                    return;
                }
                //添加质量情况
                dd.cInstance = cmbCInstance.SelectedItem.ToString();

                PurchaseBackDetail ddtmp = backgsp.OperateDetails.Find(delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(dd.cInvcode) && tdd.cBatch.Equals(dd.cBatch); });
                if (ddtmp == null)
                {
                    ddtmp = dd.CreateAttriveDetail();
                    decimal qty = Convert.ToDecimal(txtCount.Text);
                    if (qty <= ddtmp.iQuantity * (-1) - ddtmp.ScanCount)
                    {
                        ddtmp.ScanCount = qty;
                        dd.ScanCount += qty;
                        backgsp.OperateDetails.Add(ddtmp);
                        btnDone.Enabled = true;
                        btnSubmit.Enabled = true;
                        lblScanedNum.Text = dd.ScanCount.ToString("F2");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("输入数量大于单据数量");
                        txtCount.SelectAll();
                        txtCount.Focus();
                    }
                }
                else
                {
                    decimal qty = Convert.ToDecimal(txtCount.Text);
                    if (qty <= ddtmp.iQuantity * (-1) - ddtmp.ScanCount)
                    {
                        ddtmp.ScanCount += qty;
                        dd.ScanCount += qty;
                        lblScanedNum.Text = dd.ScanCount.ToString("F2");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("输入数量大于应发货数量或可发货数量");
                        txtCount.SelectAll();
                        txtCount.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            dd = null;
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblProAddress.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblScanedNum.Text = "";
            lblcBatch.Text = "";
            txtCount.Text = "";
            txtCount.Enabled = false;
            txtLable.Text = "";
            lblScanedNum.Text = "";
            txtLable.Text = "";
            txtLable.Focus();

            //默认扫描二维码
            IsQR = true;
            cmbCBatch.DataSource = null;
        }

        /// <summary>
        /// 查看来源单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSource_Click(object sender, EventArgs e)
        {
            frmPurchaseBackGSPSourceList f = new frmPurchaseBackGSPSourceList(backgsp.U8Details);
            f.ShowDialog();
            f.Dispose();
        }

        /// <summary>
        /// 查看已扫描数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            frmPurchaseBackGSPList f = new frmPurchaseBackGSPList(backgsp);
            f.ShowDialog();
            f.Dispose();
            if (dd != null)
            {
                lblScanedNum.Text = dd.ScanCount.ToString("F2");
            }
        }

        /// <summary>
        /// 点击提交按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证数量是否已扫描完
            if (backgsp.U8Details.Count == backgsp.OperateDetails.Count)
            {
                foreach (PurchaseBackDetail detail in backgsp.U8Details)
                {
                    PurchaseBackDetail ddtmp = backgsp.OperateDetails.Find(delegate(PurchaseBackDetail tdd) { return tdd.cInvcode.Equals(detail.cInvcode) && tdd.cBatch.Equals(detail.cBatch); });
                    if (ddtmp == null)
                    {
                        MessageBox.Show("还有没扫描的货物:" + detail.cinvname);
                        return;
                    }
                    if (ddtmp.iQuantity != -1 * ddtmp.ScanCount)
                    {
                        MessageBox.Show("货物:" + detail.cinvname + "的数量与单据数量不符");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("还有没扫描的货物！");
                return;
            }

            backgsp.cMaker = Common.CurrentUser.UserName;

            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                int rt = PurchaseBackBusiness.SavePurchaseBackGSP(backgsp, out errMsg);
                if (rt == 0)
                {
                    MessageBox.Show("提交成功！");
                }
                else
                {
                    MessageBox.Show("提交失败！" + errMsg);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("提交失败！" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 点击退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (backgsp != null && backgsp.OperateDetails != null && backgsp.OperateDetails.Count > 0)
            {
                DialogResult dr = MessageBox.Show("确认要退出吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //选择取消退出，直接返回
                if (dr == DialogResult.No)
                {
                    return;
                }
            }
            Close();
        }
        
        private bool isNumeric(string s)
        {
            try
            {
                decimal.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 清空数据按钮（当扫描存货错误时，点击清空数据重新扫描）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}