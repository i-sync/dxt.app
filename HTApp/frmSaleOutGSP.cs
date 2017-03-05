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
    public partial class frmSaleOutGSP : Form
    {
        SaleOutGSPVouch saleoutlist;
        GSPVouchDetail dd;

        public frmSaleOutGSP()
        {
            InitializeComponent();

            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblScanedNum.Text = "";
            lblValidDate.Text = "";
            lblcBatch.Text = "";

            this.cmbCresult.SelectedIndex = 0;
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
        /// 输入来源单据后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            //扫描销售发货单号，－－出库单号
            if (e.KeyChar == 13 && txtSource.Text.Length > 0)
            {
                string errMsg = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (SaleOutGSPBusiness.GetSaleOut(txtSource.Text, out saleoutlist, out errMsg))
                    {
                        btnSource.Enabled = true;
                        txtLable.Enabled = true;
                        txtLable.Focus();
                        //发货单文本框不再可用
                        txtSource.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("销售出库单错误:" + errMsg);
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

        private void btnSource_Click(object sender, EventArgs e)
        {
            frmGSPSourceList f = new frmGSPSourceList(saleoutlist.U8Details);
            f.ShowDialog();
            f.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (saleoutlist != null && saleoutlist.OperateDetails.Count > 0)
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
                        ///根据20121109日讨论结果：以69码为主，根据69码查询对应的存货编码
                        string errMsg = string.Empty;
                        string cInvCode = string.Empty;//存货编码
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(barcode[0], out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！" + errMsg);
                            return;
                        }
                        barcode[2] = cInvCode;
                        IsQR = true;
                    }


                    /*
                    dd = saleoutlist.U8Details.Find(delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                    if (dd == null)
                    {
                        MessageBox.Show("条码错误:存货编码不在订单中");
                        txtLable.SelectAll();
                        txtLable.Focus();
                        txtCount.Enabled = false;
                    }
                    else
                    {
                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.cinvdefine6;
                        GSPVouchDetail temp = saleoutlist.OperateDetails.Find(delegate(GSPVouchDetail v) { return v.cinvcode.Equals(dd.cinvcode); });
                        if (temp != null)
                        {
                            lblScanedNum.Text =temp.FQUANTITY.ToString("F3"); 
                        }
                        else
                        {
                            lblScanedNum.Text = dd.FQUANTITY.ToString("F3");
                        }
                        lblcBatch.Text = dd.cbatch;
                        lblProDate.Text = dd.dmadedate.ToString("yyyy-MM-dd");
                        lblValidDate.Text = dd.dvdate.ToString("yyyy-MM-dd");
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
                        dd = saleoutlist.U8Details.Find(delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(barcode[2]) && tdd.cbatch.Equals(barcode[3]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.cinvdefine6;
                        lblEnterprise.Text = dd.cinvdefine1;
                        lblScanedNum.Text = dd.FQUANTITY.ToString("F2");
                        lblcBatch.Text = dd.cbatch;
                        lblProDate.Text = Convert.ToDateTime(dd.dmadedate).ToString("yyyy-MM-dd");//dd.dmadedate.Substring(0, 10);
                        lblValidDate.Text = Convert.ToDateTime(dd.CVALDATES).ToString("yyyy-MM-dd");// dd.dvdate.Substring(0, 10);
                        //txtCount.Text = dd.iquantity.ToString("F3");
                        txtCount.Enabled = true;
                        txtCount.Focus();

                    }
                    //如果是一维码
                    else
                    {
                        //只按存货编码查询
                        dd = saleoutlist.U8Details.Find(delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            IsQR = true;
                            return;
                        }

                        //然后查询该存货编码的所有批次绑定到cmbCbatch中
                        var v = from t in saleoutlist.U8Details where t.cinvcode == dd.cinvcode select t;
                        cmbCBatch.DataSource = new BindingSource(v, null);
                        cmbCBatch.DisplayMember = "cbatch";
                        cmbCBatch.ValueMember = "cbatch";
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
            dd = cmbCBatch.SelectedItem as GSPVouchDetail;
            lblInvName.Text = dd.cinvname;
            lblInvStd.Text = dd.cinvstd;
            lblProAddress.Text = dd.cinvdefine6;
            lblEnterprise.Text = dd.cinvdefine1;
            lblScanedNum.Text = dd.FQUANTITY.ToString("F2");
            //lblcBatch.Text = dd.cbatch;
            lblProDate.Text = Convert.ToDateTime(dd.dmadedate).ToString("yyyy-MM-dd");
            lblValidDate.Text = Convert.ToDateTime(dd.CVALDATES).ToString("yyyy-MM-dd");

            txtCount.Enabled = true;
            txtCount.Focus();
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
                dd.CRESULT = cmbCresult.SelectedItem.ToString();

                GSPVouchDetail ddtmp = saleoutlist.OperateDetails.Find(delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode)&&tdd.cbatch.Equals(dd.cbatch); });
                {
                    if (ddtmp == null)
                    {
                        ddtmp = dd.CreateAttriveDetail();
                        decimal qty = Convert.ToDecimal(txtCount.Text);
                        if (qty <= ddtmp.iquantity - ddtmp.FQUANTITY)
                        {
                            dd.FQUANTITY += qty;
                            ddtmp.FQUANTITY = qty;
                            saleoutlist.OperateDetails.Add(ddtmp);
                            btnDone.Enabled = true;
                            btnSubmit.Enabled = true;
                            lblScanedNum.Text = ddtmp.FQUANTITY.ToString("F2");

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
                        if (qty <= ddtmp.iquantity - ddtmp.FQUANTITY)
                        {
                            ddtmp.FQUANTITY += qty;
                            dd.FQUANTITY += qty;
                            lblScanedNum.Text = ddtmp.FQUANTITY.ToString("F2");

                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("输入数量大于单据数量");
                            txtCount.SelectAll();
                            txtCount.Focus();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查看已扫描数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            frmGSPList f = new frmGSPList(saleoutlist);
            f.ShowDialog();
            f.Dispose();
            if (dd != null)
            {
                lblScanedNum.Text = dd.FQUANTITY.ToString("F2");
            }
        }

        /// <summary>
        /// 点击提交按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (saleoutlist.U8Details.Count == saleoutlist.OperateDetails.Count)
            {
                foreach (GSPVouchDetail detail in saleoutlist.U8Details)
                {
                    ///这个需要按存货与批次进行查询，同一存货可以不同批次
                    GSPVouchDetail ddtmp = saleoutlist.OperateDetails.Find(delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(detail.cinvcode) && tdd.cbatch.Equals(detail.cbatch); });
                    if (ddtmp == null)
                    {
                        MessageBox.Show("还有没扫描的货物:" + detail.cinvname);
                        return;
                    }
                    if (ddtmp.iquantity != ddtmp.FQUANTITY)
                    {
                        MessageBox.Show("货物:" + detail.cinvname + "的数量与单据数量不符");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("还有没扫描的货物!");
                return;
            }
            saleoutlist.CMAKER = Common.CurrentUser.UserName;
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                //判断生单类型
                bool flag = rbTypeCHM.Checked;//true:中药材/饮片;false:普通
                int rt = SaleOutGSPBusiness.SaveSaleOutGSP(saleoutlist,flag, out errMsg);
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
        /// 输入数量后清空数据
        /// </summary>
        private void Clear()
        {
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblcBatch.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblScanedNum.Text = "";
            txtCount.Text = "";
            txtLable.Text = "";
            txtLable.Focus();
            dd = null;

            IsQR = true;//默认为二维码
            cmbCBatch.DataSource = null;
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