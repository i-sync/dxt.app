using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmPUArrival : Form
    {
        ArrivalVouch tempAVH;//临时表头对象（多订单时用）
        decimal scanCount;//包装数量
        bool is2Code;//是否二维码
        int iFocus;

        public frmPUArrival()
        {
            InitializeComponent();

            //绑定仓库列表
            this.cmbWarehouse.DataSource = Common.s_Warehouse;
            this.cmbWarehouse.ValueMember = "cwhcode";
            this.cmbWarehouse.DisplayMember = "cwhname";
        }

        private void frmArr_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            Clear();
            Init();
        }

        #region KeyPress

        /// <summary>
        /// 录入订单并回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cOrderCode = txtOrder.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(cOrderCode))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (!verifyPomain(cOrderCode))
                    {
                        txtOrder.Tag = null;
                        txtOrder.Focus();
                        txtOrder.SelectAll();
                        return;
                    }

                    cmbWarehouse.Enabled = true;
                    if (cmbWarehouse.Items.Count == 1)//只有一个仓库
                    {
                        txtLabel.Enabled = true;
                        txtLabel.Focus();
                        txtLabel.SelectAll();
                    }
                    else
                    {
                        txtLabel.Enabled = false;
                        cmbWarehouse.Focus();
                    }
                    
                    //显示来源已扫数据参数
                    tempAVH.bIsOut = false;
                    tempAVH.cSaveVouch = "01";

                    txtOrder.Enabled = false;
                    btnSource.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("操作失误,请重试!");
                    return;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 扫描条码回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            string barcode = txtLabel.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(barcode))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (!ScanLabel())
                        return;
                    if (ReadLabel())
                    {
                        txtLabel.Enabled = false;
                        InputPannel.Enabled = true;
                        btnSubmit.Enabled = false;
                        cmbWarehouse.Enabled = false;

                        ArrivalVouchs sdl;
                        sdl = tempAVH.U8Details.Find(delegate(ArrivalVouchs sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper(); });
                        if (sdl != null && sdl.bInvBatch && !is2Code)//是否批次管理及是否二维码扫描
                        {
                            txtBatch.Enabled = true;
                            txtBatch.Focus();
                            txtBatch.SelectAll();
                        }
                        else
                        {
                            txtCount.Text = "1";
                            txtCount.Enabled = true;
                            txtCount.Focus();
                            txtCount.SelectAll();
                        }
                    }
                    else
                    {
                        txtLabel.Focus();
                        txtLabel.SelectAll();
                    }
                }
                catch
                {
                    MessageBox.Show("操作失误,请重试!");
                    return;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 录入批次并回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cBatch = txtBatch.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(cBatch))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    txtBatch.Text = cBatch.ToUpper();//转换为大写

                    txtCount.Text = "1";
                    txtBatch.Enabled = false;
                    txtCount.Enabled = true;
                    txtCount.Focus();
                    txtCount.SelectAll();
                }
                catch
                {
                    MessageBox.Show("操作失误,请重试!");
                    return;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (tempAVH.U8Details == null || tempAVH.U8Details.Count < 1)
                    return;

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    #region 数值格式
                    if (string.IsNullOrEmpty(this.txtCount.Text))
                    {
                        MessageBox.Show("没有输入数量！");
                        return;
                    }
                    else if (!isNumeric(txtCount.Text))
                    {
                        MessageBox.Show("请输入正确的数字格式！");
                        return;
                    }
                    else if (decimal.Parse(txtCount.Text) <= 0)
                    {
                        MessageBox.Show("请输入正数！");
                        txtCount.Text = txtCount.Text.Remove(0, 1);
                        return;
                    }

                    #endregion

                    if (RecordNum())
                    {
                        Clear();
                        btnDone.Enabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show("操作失误,请重试!");
                    return;
                }
                finally
                {
                    if (tempAVH.OperateDetails.Count > 0)
                        btnSubmit.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        #endregion

        #region Click_Change
        /// <summary>
        /// 清空数据
        /// </summary>
        private void InputPannel_Click(object sender, EventArgs e)
        {
            InputPannel.Enabled = !InputPannel.Enabled;
            Clear();
            if (tempAVH != null && tempAVH.OperateDetails != null && tempAVH.OperateDetails.Count > 0)
                btnSubmit.Enabled = true;
            txtLabel.Focus();
            txtLabel.SelectAll();
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        private void rbt_Click(object sender, EventArgs e)
        {
            txtOrder.Focus();
            txtOrder.SelectAll();
        }
        /// <summary>
        /// 选择仓库
        /// </summary>
        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtOrder.Enabled)
                return;
            if (cmbWarehouse.DataSource == null)
                return;
            if (cmbWarehouse.SelectedValue.ToString() == "-1")
                return;
            txtLabel.Enabled = true;
            txtLabel.Focus();
            txtLabel.SelectAll();
        }
        /// <summary>
        /// 中成药生产日期启用
        /// </summary>
        private void cbChinese_CheckStateChanged(object sender, EventArgs e)
        {
            dtpChineseDate.Enabled = cbChinese.Checked;
        }
        /// <summary>
        /// 生产日期推算有效期至
        /// </summary>
        private void dtpProDate_ValueChanged(object sender, EventArgs e)
        {
            if (tempAVH == null || tempAVH.U8Details == null || tempAVH.U8Details.Count < 1)
                return;
            try
            {
                ArrivalVouchs sdl;
                sdl = tempAVH.U8Details.Find(delegate(ArrivalVouchs sdd) { return sdd.cInvCode == txtLabel.Text.Trim() && sdd.nQuantity > 0; });
                DateTime date = dtpProDate.Value.AddMonths(Convert.ToInt32(sdl.iMassDate)).AddDays(-1);
                if (sdl != null && dtpValityDate.Value != date)
                    dtpValityDate.Value = date;
            }
            catch { return; }
        }
        /// <summary>
        /// 有效期至推算生产日期
        /// </summary>
        private void dtpValityDate_ValueChanged(object sender, EventArgs e)
        {
            if (tempAVH == null || tempAVH.U8Details == null || tempAVH.U8Details.Count < 1 || string.IsNullOrEmpty(dtpValityDate.Text))
                return;
            try
            {
                ArrivalVouchs sdl;
                sdl = tempAVH.U8Details.Find(delegate(ArrivalVouchs sdd) { return sdd.cInvCode == txtLabel.Text.Trim() && sdd.nQuantity > 0; });
                DateTime date = dtpValityDate.Value.AddMonths(Convert.ToInt32(sdl.iMassDate) * -1).AddDays(1);
                if (sdl != null && dtpProDate.Value != date)
                    dtpProDate.Value = date;
            }
            catch { return; }
            SetFocus();
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        private void dtpDate_Validated(object sender, EventArgs e)
        {
            iFocus++;

            if (iFocus > 3 && iFocus % 2 == 0)
            {
                //设置焦点
                SetFocus();
                iFocus = 0;
            }
        }
        /// <summary>
        /// 来源数据
        /// </summary>
        private void btnSource_Click(object sender, EventArgs e)
        {
            if (tempAVH.U8Details == null || tempAVH.U8Details.Count < 1)
            {
                MessageBox.Show("无来源数据！");
                SetFocus();
                return;
            }

            frmPurchaseArrivalSource source = new frmPurchaseArrivalSource(tempAVH.U8Details);
            source.ShowDialog();
            SetFocus();
        }
        /// <summary>
        /// 已扫描数据
        /// </summary>
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (tempAVH.OperateDetails == null || tempAVH.OperateDetails.Count < 1)
            {
                MessageBox.Show("无操作数据！");
                return;
            }
            try
            {
                using (frmDone DataDone = new frmDone(tempAVH))
                {
                    int count = tempAVH.OperateDetails.Count;
                    DataDone.ShowDialog();
                    //刷新已扫数量
                    if (count != tempAVH.OperateDetails.Count && !string.IsNullOrEmpty(txtLabel.Text) && txtLabel.Enabled == false)
                    {
                        ArrivalVouchs sdl;
                        sdl = tempAVH.OperateDetails.Find(delegate(ArrivalVouchs sdd) { return sdd.cInvCode == txtLabel.Text.Trim() && sdd.Quantity > 0; });
                        lblDoneNum.Text = sdl == null ? "0" : sdl.Quantity.ToString("F4");
                    }
                    SetFocus();
                }

                //无已扫数量禁用
                if (tempAVH.OperateDetails == null || tempAVH.OperateDetails.Count < 1)
                {
                    btnDone.Enabled = false;
                }
            }
            catch { MessageBox.Show("操作失误,请重试!"); }
            finally { SetFocus(); }
        }
        /// <summary>
        /// 单据提交
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tempAVH.OperateDetails == null || tempAVH.OperateDetails.Count < 1)
            {
                MessageBox.Show("对不起，您还未扫描任何数据！");
                return;
            }

            try
            {
                if (!VerifyScan())
                {
                    DialogResult res = MessageBox.Show("数据未全部扫描,确定要提交吗?", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (res == DialogResult.No)
                    {
                        return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor;
                tempAVH.cVerifier = Common.CurrentUser.UserName;
                tempAVH.dDate = DateTime.Now.ToString("yyyy-MM-dd");
                tempAVH.cMakeTime = DateTime.Now.ToString();
                tempAVH.cMaker = Common.CurrentUser.UserName;
                tempAVH.cVouchType = "采购到货单";
                tempAVH.cDepCode = "0103";     //采购部   部门编码
                tempAVH.iTaxRate = tempAVH.U8Details[0].iTaxRate;

                if (SubmitData())
                {
                    MessageBox.Show("提交成功!");
                    Clear();
                    Init();
                }
                //else if (ErrSubmit())
                //{
                //    MessageBox.Show("提交成功!");
                //    Clear();
                //    Init();
                //}
                else
                {
                    MessageBox.Show("提交失败!");
                    SetFocus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (tempAVH.U8Details != null && tempAVH.U8Details.Count > 0)
            {
                DialogResult res = MessageBox.Show("已扫描的数据未保存,确定要退出吗?", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        #endregion

        #region Read
        /// <summary>
        /// 读取标签
        /// </summary>
        /// <returns>是否存在对应存货</returns>
        private bool ReadLabel()
        {
            try
            {
                foreach (ArrivalVouchs sid in tempAVH.U8Details)
                {
                    if (sid.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper())
                    {
                        lblName.Text = sid.cInvName;        //品名
                        lblStandard.Text = sid.cInvStd;     //规格 
                        //lblcInvCode.Text = sid.cInvCode;    //生产编码
                        lblAddrCode.Text = sid.Define22;//sid.cAddress;    //产地
                        decimal qty = 0;//已扫数量
                        foreach (ArrivalVouchs sdd in tempAVH.OperateDetails)
                        {
                            if (sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Quantity > 0)
                            {
                                if (!string.IsNullOrEmpty(txtBatch.Text) && sdd.cBatch.ToUpper() != txtBatch.Text.Trim().ToUpper())
                                    continue;
                                qty += sdd.Quantity;
                            }
                        }
                        lblDoneNum.Text = qty.ToString("F4");
                        //非二维码扫描且效期管理
                        if (!is2Code && sid.bInvBatch && sid.iMassDate > 0)
                        {
                            cbChinese.Enabled = true;
                            if (!string.IsNullOrEmpty(sid.Define23))
                            {
                                cbChinese.Checked = true;
                                dtpChineseDate.Value = ConvertDate(sid.Define23);
                            }
                            dtpProDate.Enabled = true;
                            dtpValityDate.Enabled = true;
                            if (dtpProDate.Value == dtpValityDate.Value || dtpProDate.Value == DateTime.Now)
                            {
                                ArrivalVouchs temp = null;
                                temp = tempAVH.OperateDetails.Find(delegate(ArrivalVouchs avs) { return avs.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && avs.cBatch.ToUpper() == txtBatch.Text.Trim().ToUpper(); });
                                try
                                {
                                    if (temp != null)
                                    {
                                        dtpValityDate.Value = ConvertDate(temp.cExpirationDate);
                                        dtpProDate.Value = temp.dPDate;// ConvertDate(temp.dPDate);
                                        dtpChineseDate.Value = ConvertDate(temp.Define23);
                                    }
                                    else
                                    {
                                        dtpValityDate.Value = ConvertDate(sid.cExpirationDate);
                                        dtpProDate.Value = temp.dPDate;// ConvertDate(sid.dPDate);
                                    }
                                }
                                catch
                                {
                                    dtpValityDate.Value = Common.err_Time.AddMonths(sid.iMassDate).AddDays(-1);
                                    dtpProDate.Value = Common.err_Time;
                                }
                            }
                        }
                        else
                        {
                            dtpProDate.Enabled = false;
                            dtpValityDate.Enabled = false;
                        }

                        //
                        txtCount.Focus();
                        txtCount.SelectAll();
                        txtOrder.Tag = sid;
                        return true;
                    }
                }

                MessageBox.Show("没有相应的物料信息");
                Clear();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// 录入数量
        /// </summary>
        /// <returns>是否录入正确</returns>
        private bool RecordNum()
        {
            string _qty = txtCount.Text.Trim();
            try
            {
                if (txtOrder.Tag != null)
                {
                    decimal qty = Convert.ToDecimal(_qty) * scanCount;
                    ArrivalVouchs sdl;
                    sdl = tempAVH.U8Details.Find(delegate(ArrivalVouchs sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.nQuantity > 0; });

                    if (sdl == null)
                    {
                        MessageBox.Show("该货物已全部扫描！");
                        return false;
                    }

                    if (sdl.bInvBatch && string.IsNullOrEmpty(txtBatch.Text))
                    {
                        MessageBox.Show("该货物有批次管理,请输入批次后重试！");
                        return false;
                    }
                    else if (!sdl.bInvBatch && !string.IsNullOrEmpty(txtBatch.Text))
                    {
                        MessageBox.Show("该货物没有批次管理,请勿输入批次！");
                        return false;
                    }

                    if (qty > sdl.nQuantity)
                    {
                        MessageBox.Show("输入数量大于应到货数量！" + Environment.NewLine + "应不大于" + sdl.nQuantity.ToString("F4") + sdl.cInvm_Unit);
                        return false;
                    }

                    ArrivalVouchs tempAVS = sdl.getNewDetail();
                    if (!addData(qty, tempAVS))
                        return false;

                    Clear();
                    txtLabel.Focus();
                }
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                txtCount.Focus();
                return false;
            }
        }
        /// <summary>
        /// 提交到货单
        /// </summary>
        /// <returns>是否提交成功</returns>
        private bool SubmitData()
        {
            try
            {
                this.Enabled = false;
                ArrivalBusiness.Save(tempAVH, tempAVH.cSaveVouch);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.Enabled = true;
            }
        }

        #endregion

        #region Function

        #region Init
        /// <summary>
        /// 界面初始化
        /// </summary>
        private void Init()
        {
            tempAVH = new ArrivalVouch();

            cmbWarehouse.DataSource = null;
            cmbWarehouse.Items.Clear();
            cmbWarehouse.Refresh();
            cmbWarehouse.Enabled = false;

            InputPannel.Enabled = false;
            txtOrder.Enabled = true;
            txtLabel.Enabled = false;
            btnSource.Enabled = false;
            btnDone.Enabled = false;
            btnSubmit.Enabled = false;
            rbtCheck.Checked = false;
            rbtNoCheck.Checked = false;

            txtOrder.Text = "";

            SetFocus();
        }

        #endregion

        #region Clear
        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            txtLabel.Enabled = true;
            txtBatch.Enabled = false;
            txtCount.Enabled = false;
            cbChinese.Checked = false;
            cbChinese.Enabled = false;
            dtpChineseDate.Value = DateTime.Now;
            dtpValityDate.Value = DateTime.Now;
            dtpProDate.Value = DateTime.Now;
            dtpChineseDate.Enabled = false;
            dtpProDate.Enabled = false;
            dtpValityDate.Enabled = false;

            txtLabel.Text = "";
            txtBatch.Text = "";
            txtCount.Text = "";
            lblBarCode.Text = "";
            lblDoneNum.Text = "";
            lblName.Text = "";
            lblStandard.Text = "";
            //lblSaleCode.Text = "";
            lblAddrCode.Text = "";

            iFocus = 0;
            is2Code = false;
        }

        #endregion

        #region 字符串格式
        /// <summary>
        /// 是否数字
        /// </summary>
        private bool isNumeric(string strQty)
        {
            try
            {
                decimal.Parse(strQty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 时间格式
        /// </summary>
        private DateTime ConvertDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return DateTime.Now;
            try 
            {
                return DateTime.Parse(date);
            }
            catch { return DateTime.Now; }
        }

        #endregion

        #region verifyPomain
        /// <summary>
        /// 获取采购订单信息
        /// </summary>
        /// <param name="PoCode">采购订单号</param>
        /// <returns>是否存在</returns>
        private bool verifyPomain(string PoCode)
        {
            try
            {
                //DataSet ds;
                //tempAVH = ArrivalBusiness.CreateAVOrderByPomain(PoCode, out ds);
                string errMsg;
                tempAVH = new PurchaseArrivalBusiness().PO_POMian_Load(PoCode, out errMsg);
                if (tempAVH.U8Details == null || tempAVH.U8Details.Count ==0)
                    return false;
                //是否质检单
                foreach (ArrivalVouchs avs in tempAVH.U8Details)
                {
                    if (avs.bGsp)
                    {
                        rbtCheck.Checked = true;
                        return true;
                    }
                }

                rbtNoCheck.Checked = true;

                //Clear();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                Init();
                return false;
            }
        }
        #endregion

        #region addData
        /// <summary>
        /// 数据累计
        /// </summary>
        /// <param name="_qty">扫描数量</param>
        /// <param name="sd">累加货物</param>
        /// <returns>是否累加成功</returns>
        private bool addData(decimal _qty, ArrivalVouchs sd)
        {
            try
            {
                decimal num = 0;
                ArrivalVouchs temp = null;
                temp = tempAVH.U8Details.Find(delegate(ArrivalVouchs av) { return av.cInvCode.ToUpper() == sd.cInvCode.ToUpper(); });
                foreach (ArrivalVouchs avs in tempAVH.OperateDetails)
                {
                    if (avs.cInvCode.ToUpper() == sd.cInvCode.ToUpper())
                    {
                        num += avs.Quantity;
                        if (num + _qty >= temp.nQuantity)
                            break;
                    }
                }
                if (num + _qty > temp.nQuantity)
                {
                    MessageBox.Show("扫描数量大于最大扫描数量");
                    Clear();
                    return false;
                }

                sd.cBatch = txtBatch.Text.Trim().ToUpper();
                if (tempAVH.OperateDetails.Count > 0)
                {
                    foreach (ArrivalVouchs siodl in tempAVH.OperateDetails)
                    {
                        if (siodl.cInvCode.ToUpper() == sd.cInvCode.ToUpper() && siodl.cBatch == sd.cBatch && siodl.nQuantity > 0)//看还要加什么条件？
                        {
                            if (siodl.nQuantity >= _qty + siodl.Quantity)
                            {

                                siodl.Quantity = siodl.Quantity + _qty;//数量累加
                                if (!siodl.bGsp)
                                {
                                    //非质检到货合格数量
                                    siodl.fRealQuantity += _qty;
                                    siodl.fValidQuantity += _qty;
                                }

                                siodl.iMoney = siodl.Quantity * siodl.iCost;//本币无税金额
                                siodl.iTaxPrice = siodl.iMoney * siodl.iTaxRate * Convert.ToDecimal(0.01);//本币税额
                                siodl.iSum = siodl.iMoney + siodl.iTaxPrice;//本币价税合计
                                siodl.iOriMoney = siodl.Quantity * siodl.iOriCost;//原币无税金额
                                siodl.iOriTaxPrice = siodl.iOriMoney * siodl.iTaxRate * Convert.ToDecimal(0.01);//原币税额
                                siodl.iOriSum = siodl.iOriMoney + siodl.iOriTaxPrice;//原币价税合计
                                if (siodl.iMassDate > 0 && siodl.dPDate != Cast.ToDateTime(dtpProDate.Value.ToShortDateString()))
                                {
                                    siodl.dPDate = Cast.ToDateTime(dtpProDate.Value.ToShortDateString());//生产日期
                                    sd.dVDate =Cast.ToDateTime( dtpValityDate.Value.AddDays(1).ToShortDateString());//失效日期
                                    sd.cExpirationDate = dtpValityDate.Value.ToString("yyyy-MM-dd");//有效期至
                                }
                                if (cbChinese.Checked)
                                    siodl.Define23 = dtpChineseDate.Value.ToString("yyyy-MM-dd");//中成药生产日期

                                return true;
                            }
                            else
                            {
                                MessageBox.Show("已扫描数量大于最大扫描数量" + Environment.NewLine + "应小于" + sd.nQuantity.ToString("F4") + sd.cInvm_Unit + ",已扫描" + siodl.Quantity.ToString("F4") + sd.cInvm_Unit);
                                Clear();
                                return false;
                            }
                        }
                    }
                }
                //统一仓库
                sd.cWhCode = cmbWarehouse.SelectedValue.ToString();
                sd.cWhName = cmbWarehouse.Text;
                if (!sd.bGsp)
                {
                    //非质检到货合格数量
                    sd.fRealQuantity = _qty;
                    sd.fValidQuantity = _qty;
                }
                sd.Quantity = _qty;
                sd.iMoney = sd.Quantity * sd.iCost;//本币无税金额
                sd.iTaxPrice = sd.iMoney * sd.iTaxRate * Convert.ToDecimal(0.01);//本币税额
                sd.iSum = sd.iMoney + sd.iTaxPrice;//本币价税合计
                sd.iOriMoney = sd.Quantity * sd.iOriCost;//原币无税金额
                sd.iOriTaxPrice = sd.iOriMoney * sd.iTaxRate * Convert.ToDecimal(0.01);//原币税额
                sd.iOriSum = sd.iOriMoney + sd.iOriTaxPrice;//原币价税合计
                //是否效期管理
                if (sd.iMassDate > 0)
                {
                    sd.dPDate = Cast.ToDateTime( dtpProDate.Value.ToShortDateString());//生产日期
                    sd.dVDate = Cast.ToDateTime( dtpValityDate.Value.AddDays(1).ToShortDateString());//失效日期
                    sd.cExpirationDate = dtpValityDate.Value.ToString("yyyy-MM-dd");//有效期至
                    sd.cMassUnit = 2;
                    sd.iExpiratDateCalcu = 2;
                }
                if (cbChinese.Checked)
                    sd.Define23 = dtpChineseDate.Value.ToString("yyyy-MM-dd");//中成药生产日期

                tempAVH.OperateDetails.Add(sd);

                btnSubmit.Enabled = true;
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                txtCount.Focus();
                return false;
            }
        }
        #endregion        

        #region VerifyScan
        /// <summary>
        /// 验证已操作数据
        /// </summary>
        /// <returns>是否全部扫描</returns>
        private bool VerifyScan()
        {
            if (tempAVH.U8Details.Count > tempAVH.OperateDetails.Count)
            {
                return false;
            }

            foreach (ArrivalVouchs sid in tempAVH.U8Details)
            {
                decimal quan = 0;
                foreach (ArrivalVouchs avs in tempAVH.OperateDetails)
                {
                    if (sid.cInvCode.ToUpper() == avs.cInvCode.ToUpper())
                        quan += avs.Quantity;
                }
                if (sid.nQuantity > quan)
                    return false;
            }
            return true;
        }
        #endregion

        #region ScanLabel
        /// <summary>
        /// 读取标签
        /// </summary>
        /// <returns>标签格式是否正确</returns>
        private bool ScanLabel()
        {
            string errMsg = "";
            string cInvCode = "";
            string strBarCode = txtLabel.Text.Trim().ToUpper();
            txtLabel.Text = "";
            string[] barcode = new string[7] {"", "", "", "", "", "", "" };
            try
            {
                if (strBarCode.IndexOf('@') == -1)
                {   //一维码
                    is2Code = false;
                    if (!Common.GetCInvCode(strBarCode, out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！" );
                        return false;
                    }
                    txtLabel.Text = cInvCode;
                    lblBarCode.Text = strBarCode;
                }
                else
                {   //二维码
                    is2Code = true;
                    barcode = strBarCode.Split('@');
                    lblBarCode.Text = barcode[0];
                    if (!Common.GetCInvCode(barcode[0], out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！");
                        return false;
                    }
                    barcode[2] = cInvCode;
                    txtLabel.Text = barcode[2];
                    txtBatch.Text = string.IsNullOrEmpty(barcode[3]) ? "" : barcode[3];
                    try
                    {
                        if (!string.IsNullOrEmpty(barcode[4]))
                        {
                            dtpValityDate.Value = ConvertDate(barcode[5]);
                            dtpProDate.Value = ConvertDate(barcode[4]);
                        }
                    }
                    catch
                    {
                        dtpValityDate.Value = Common.err_Time;
                        dtpProDate.Value = Common.err_Time;
                    }
                }
                //读取包装数量
                if (barcode.Length < 7 || string.IsNullOrEmpty(barcode[6]))
                    scanCount = 1;
                else
                {
                    try { scanCount = decimal.Parse(barcode[6]); }
                    catch { scanCount = 1; }
                }
                return true;
            }
            catch 
            {
                Clear();
                MessageBox.Show("对不起,条码格式错误!");
                return false;
            }
            finally { SetFocus(); }
        }
        #endregion

        #region SetFocus
        /// <summary>
        /// 设置焦点
        /// </summary>
        private void SetFocus()
        {
            if (txtOrder.Enabled)
            {
                txtOrder.Focus();
                txtOrder.SelectAll();
            }
            else if (txtLabel.Enabled)
            {
                txtLabel.Focus();
                txtLabel.SelectAll();
            }
            else if (txtBatch.Enabled)
            {
                txtBatch.Focus();
                txtBatch.SelectAll();
            }
            else
            {
                txtCount.Focus();
                txtCount.SelectAll();
            }
        }

        #endregion

        

        #endregion
    }
}