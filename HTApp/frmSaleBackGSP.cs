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
    public partial class frmSaleBackGSP : Form
    {
        SaleBackGSPVouch salebackgsp;
        SaleBackGSPDetail dd;

        public frmSaleBackGSP()
        {
            InitializeComponent();

            salebackgsp = new SaleBackGSPVouch();

            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblcBatch.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblScanedNum.Text = "";

            this.cmbWarehouse.DataSource = Common.s_Warehouse;
            this.cmbWarehouse.ValueMember = "cwhcode";
            this.cmbWarehouse.DisplayMember = "cwhname";

            this.cmbCoutinstance.SelectedIndex = 0;
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


        private void txtSource_KeyPress(object sender, KeyPressEventArgs e) 
        {
            //扫描销售出库单号 
            if (e.KeyChar == 13 && txtSource.Text.Length > 0)
            {
                string errMsg = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (SaleBackGSPBusiness.GetSaleBack(txtSource.Text, out salebackgsp, out errMsg))
                    {
                        btnSource.Enabled = true;
                        txtLable.Enabled = true;
                        txtLable.Focus();

                        ///判断当前仓库集合中是否有该单据的仓库，若没有则表明该用户无权操作。
                        string cwhcode = salebackgsp.U8Details[0].cwhcode;
                        bool flag = false;
                        foreach (Warehouse w in Common.s_Warehouse)
                        {
                            if (w.cwhcode == cwhcode)
                            {
                                cmbWarehouse.SelectedItem = w;
                                flag = true;
                                //退化单文本框不再可用
                                txtSource.Enabled = false;
                                break;
                            }
                        }
                        if (!flag)//如果没有找到对应的仓库
                        {
                            MessageBox.Show("该单据您无权操作！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("销售退货单错误:" + errMsg);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (salebackgsp != null && salebackgsp.OperateDetails != null && salebackgsp.OperateDetails.Count > 0)
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

        private void btnSource_Click(object sender, EventArgs e)
        {
            frmSaleBackGSPSourceList f = new frmSaleBackGSPSourceList(salebackgsp.U8Details);
            f.ShowDialog();
            f.Dispose();
        }

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
                    if (strBarcode.IndexOf('@') == -1)//没有  找到@，说明该条码是一维条码
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
                    dd = salebackgsp.U8Details.Find(delegate(SaleBackGSPDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); }); //&&tdd.CBATCH.Equals(barcode[3]); });
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
                        lblProAddress.Text = dd.CDEFINE22;
                        ///查找已扫描集合中是否有该产品，若有则取对应的扫描数据，若没有则取来源集合中的数量（默认为0）
                        SaleBackGSPDetail temp = salebackgsp.OperateDetails.Find(delegate(SaleBackGSPDetail s) { return s.cinvcode.Equals(dd.cinvcode); });
                        if (temp != null)
                        {
                            lblScanedNum.Text = temp.ScanCount.ToString("F3");
                        }
                        else
                        {
                            lblScanedNum.Text = dd.ScanCount.ToString("F3");
                        }
                        lblcBatch.Text = dd.CBATCH;
                        lblProDate.Text = Convert.ToDateTime(dd.DPRODATE).ToString("yyyy-MM-dd"); //dd.DPRODATE.Substring(0,10);
                        lblValidDate.Text = Convert.ToDateTime(dd.DVDATE).ToString("yyyy-MM-dd"); //dd.DVDATE.Substring(0, 10);
                        txtCount.Enabled = true;
                        txtCount.SelectAll();
                        txtCount.Focus();

                    }
                     * */
                    ///如果是二维码
                    if (IsQR)
                    {
                        //按存货编码和批次查询
                        dd = salebackgsp.U8Details.Find(delegate(SaleBackGSPDetail tdd) { return tdd.cinvcode.Equals(barcode[2]) && tdd.CBATCH.Equals(barcode[3]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.CDEFINE22;
                        lblEnterprise.Text = dd.cinvdefine1;
                        lblcBatch.Text = dd.CBATCH;
                        lblScanedNum.Text = dd.ScanCount.ToString("F2");
                        lblProDate.Text = Convert.ToDateTime(dd.DPRODATE).ToString("yyyy-MM-dd");
                        lblValidDate.Text = Convert.ToDateTime(dd.CVALDATE).ToString("yyyy-MM-dd");
                        //txtCount.Text = dd.iquantity.ToString("F3");
                        txtCount.Enabled = true;
                        txtCount.Focus();

                    }
                    //如果是一维码
                    else
                    {
                        //只按存货编码查询
                        dd = salebackgsp.U8Details.Find(delegate(SaleBackGSPDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        //然后查询该存货编码的所有批次绑定到cmbCbatch中
                        var v = from t in salebackgsp.U8Details where t.cinvcode == dd.cinvcode select t;
                        cmbCBatch.DataSource = new BindingSource(v, null) ;
                        cmbCBatch.DisplayMember = "CBATCH";
                        cmbCBatch.ValueMember = "CBATCH";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 若为一维码，批次选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCBatch_SelectedIndexChangeed(object sender, EventArgs e)
        {
            //如果数据源为空,直接返回
            if (cmbCBatch.DataSource == null)
                return;
            //获取选择的对象
            dd = cmbCBatch.SelectedItem as SaleBackGSPDetail;
            lblInvName.Text = dd.cinvname;
            lblInvStd.Text = dd.cinvstd;
            lblProAddress.Text = dd.CDEFINE22;
            lblEnterprise.Text = dd.cinvdefine1;
            lblScanedNum.Text = dd.ScanCount.ToString("F2");
            //lblcBatch.Text = dd.cbatch;
            lblProDate.Text = Convert.ToDateTime(dd.DPRODATE).ToString("yyyy-MM-dd");
            lblValidDate.Text = Convert.ToDateTime(dd.CVALDATE).ToString("yyyy-MM-dd");

            txtCount.Enabled = true;
            txtCount.Focus();
        }

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
                //添加外观质量情况
                dd.COUTINSTANCE = cmbCoutinstance.SelectedItem.ToString();

                SaleBackGSPDetail ddtmp = salebackgsp.OperateDetails.Find(delegate(SaleBackGSPDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode) && tdd.CBATCH.Equals(dd.CBATCH); });
                {
                    if (ddtmp == null)
                    {
                        ddtmp = dd.CreateAttriveDetail();
                        decimal qty = Convert.ToDecimal(txtCount.Text);
                        if (qty <= ddtmp.FQUANTITY - ddtmp.ScanCount)//这里减去的是已扫描数据中的已扫数量（因为它与来源数据中的已扫描数量一样）
                        {
                            dd.ScanCount = qty;//在来源单据中修改已扫描数据是为了显示“已扫描数量”
                            ddtmp.ScanCount = qty;
                            salebackgsp.OperateDetails.Add(ddtmp);
                            btnDone.Enabled = true;
                            btnSubmit.Enabled = true;
                            lblScanedNum.Text = ddtmp.ScanCount.ToString("F2");
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
                        if (qty <= ddtmp.FQUANTITY - ddtmp.ScanCount)
                        {
                            dd.ScanCount += qty;
                            ddtmp.ScanCount += qty;
                            lblScanedNum.Text = ddtmp.ScanCount.ToString("F2");

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
        /// 清空数据
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
            txtCount.Enabled = false;
            txtLable.Text = "";
            txtLable.Focus();
            dd = null;

            //默认为二维码
            IsQR = true;
            cmbCBatch.DataSource = null;
        }

        /// <summary>
        /// 点击查询已扫描数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            frmSaleBackGSPList f = new frmSaleBackGSPList(salebackgsp);
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
            if (salebackgsp.U8Details.Count == salebackgsp.OperateDetails.Count)
            {
                foreach (SaleBackGSPDetail detail in salebackgsp.U8Details)
                {
                    SaleBackGSPDetail ddtmp = salebackgsp.OperateDetails.Find(delegate(SaleBackGSPDetail tdd) { return tdd.cinvcode.Equals(detail.cinvcode) && tdd.CBATCH.Equals(detail.CBATCH); });
                    if (ddtmp == null)
                    {
                        MessageBox.Show("还有没扫描的货物:" + detail.cinvname);
                        return;
                    }
                    if (ddtmp.FQUANTITY !=  ddtmp.ScanCount)
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

            salebackgsp.CMAKER = Common.CurrentUser.UserName;
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                int rt = SaleBackGSPBusiness.SaveSaleBackGSP(salebackgsp, out errMsg);
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