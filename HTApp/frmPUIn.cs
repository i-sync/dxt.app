using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmPUIn : Form
    {
        bool isloaded = false;
        StockIn tempSIO;//临时表头对象（多订单时用）

        decimal scanCount;//包装数量
        bool is2Code;//是否二维码

        #region 页面初始化
        public frmPUIn()
        {
            InitializeComponent();
            try
            {
                //DataSet ds;
                //StockInBusiness.CreateSIOrderArrive("", "", "", 0, out ds);
                //if (ds.Tables == null || ds.Tables["head"].Rows.Count == 0)
                //{
                //    StockInBusiness.CreateSIOrderByGSPVouch(null, null, out ds);
                //    if (ds.Tables == null || ds.Tables["head"].Rows.Count == 0)
                //    {
                //        MessageBox.Show("无可操作单据!");
                //        return;
                //    }
                //    else
                //    {
                //        rbtArrival.Checked = false;
                //        rbtQCheck.Checked = true;
                //    }
                //}

                isloaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPUIn_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            Clear();
            Init();
        }

        #endregion

        #region KeyPress

        private void txtOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (isloaded && string.IsNullOrEmpty(txtOrder.Text))
                    return;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (!VerifyOrder())
                    {
                        MessageBox.Show("请输入正确的单据号后重试!");
                        return;
                    }
                    if (!verifyArrive(txtOrder.Text))
                    {
                        txtOrder.Tag = null;
                        txtOrder.Focus();
                        txtOrder.SelectAll();
                        return;
                    }
                    //显示来源已扫数据参数
                    tempSIO.IsOut = false;
                    tempSIO.SaveVouch = "02";

                    txtOrder.Enabled = false;
                    rbtArrival.Enabled = false;
                    rbtQCheck.Enabled = false;
                    btnSource.Enabled = true;

                    //是否货位管理
                    SetPosition();
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

        private void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (string.IsNullOrEmpty(txtPosition.Text) || !tempSIO.WhPos)
                        return;

                    if (ReadPosition())
                    {
                        txtPosition.Enabled = false;
                        txtLabel.Enabled = true;
                        txtLabel.Focus();
                        txtLabel.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("不存在该货位！");
                        txtPosition.Focus();
                        txtPosition.SelectAll();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("操作失误,请重试!"+ex.Message);
                    return;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void txtLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtLabel.Text))
                    return;

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (!ScanLabel())
                        return;
                    if (ReadLabel())
                    {
                        txtPosition.Enabled = false;
                        txtLabel.Enabled = false;
                        lblBatch.Enabled = true;
                        InputPannel.Enabled = true;
                        btnSubmit.Enabled = false;

                        txtCount.Text = "1";
                        txtCount.Enabled = true;
                        txtCount.Focus();
                        txtCount.SelectAll();
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

        private void txtBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    lblBatch.Text = lblBatch.Text.Trim().ToUpper();
                    if (ReadBatch())
                    {
                        txtCount.Text = "1";
                        lblBatch.Enabled = false;
                        txtCount.Enabled = true;
                        txtCount.Focus();
                        txtCount.SelectAll();
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

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (tempSIO.U8Details == null || tempSIO.U8Details.Count < 1)
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
                    if (tempSIO.OperateDetails.Count > 0)
                        btnSubmit.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        #endregion

        #region Click_Change

        /// <summary>
        /// 清空
        /// </summary>
        private void InputPannel_Click(object sender, EventArgs e)
        {
            InputPannel.Enabled = !InputPannel.Enabled;
            Clear();
            if (tempSIO != null && tempSIO.OperateDetails != null && tempSIO.OperateDetails.Count > 0)
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
        /// 批次文本框赋值
        /// </summary>
        private void cbxBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLabel.Text) || txtLabel.Enabled)
                return;

            if (sender != null)
            {
                lblBatch.Text = cbxBatch.SelectedValue.ToString();
            }

            //刷新产期及已扫数量
            ReadLabel();
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        private void cbxBatch_LostFocus(object sender, EventArgs e)
        {
            if (cbxBatch.DataSource == null || cbxBatch.Items.Count < 0 || cbxBatch.SelectedIndex < 0 || txtLabel.Enabled)
                return;
            txtCount.Text = "1";
            txtCount.Focus();
            txtCount.SelectAll();
        }
        /// <summary>
        /// 来源单据
        /// </summary>
        private void btnSource_Click(object sender, EventArgs e)
        {
            if (tempSIO.U8Details == null || tempSIO.U8Details.Count < 1)
            {
                MessageBox.Show("无来源数据！");
                SetFocus();
                return;
            }

            using (frmSource DataSource = new frmSource(tempSIO))
            {
                DataSource.ShowDialog();
                SetFocus();
            }
        }
        /// <summary>
        /// 已扫数据
        /// </summary>
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (tempSIO.OperateDetails == null || tempSIO.OperateDetails.Count < 1)
            {
                MessageBox.Show("无操作数据！");
                return;
            }
            try
            {
                using (frmDone DataDone = new frmDone(tempSIO))
                {
                    DataDone.ShowDialog();
                    //刷新已扫数量
                    if (!string.IsNullOrEmpty(txtLabel.Text) && txtLabel.Enabled == false)
                    {
                        StockInDetail sdl;
                        sdl = tempSIO.OperateDetails.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Batch.ToUpper() == lblBatch.Text.Trim().ToUpper(); });
                        if (sdl != null)
                            lblDoneNum.Text = sdl.Quantity.ToString("F4");
                        else
                            lblDoneNum.Text = "0";
                    }
                    SetFocus();
                }

                //无已扫数量禁用
                if (tempSIO.OperateDetails == null || tempSIO.OperateDetails.Count < 1)
                {
                    btnDone.Enabled = false;
                }
            }
            catch { MessageBox.Show("操作失误,请重试!"); }
            finally { SetFocus(); }
        }
        /// <summary>
        /// 提交
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tempSIO.OperateDetails == null || tempSIO.OperateDetails.Count < 1)
            {
                MessageBox.Show("对不起，您还未扫描任何数据！");
                return;
            }

            try
            {
                int result = VerifyScan();
                if (result != 0)
                {
                    DialogResult res = MessageBox.Show("数据未全部扫描,确定要提交吗?", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (res == DialogResult.No)
                    {
                        return;
                    }
                }
                if (result == 2 && rbtQCheck.Checked)
                {
                    MessageBox.Show("GSP入库同一批次必须一次性录入!");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                tempSIO.Handler = Common.CurrentUser.UserName;
                tempSIO.Date = DateTime.Now.ToString("yyyy-MM-dd");
                tempSIO.Vouchtype = "01";
                tempSIO.Rdcode = "101";
                tempSIO.Rdname = "普通采购";
                tempSIO.nmaketime = DateTime.Now.ToString();
                tempSIO.nverifytime = tempSIO.nmaketime;
                tempSIO.Veridate = DateTime.Now.ToString();
                tempSIO.Maker = Common.CurrentUser.UserName;
                tempSIO.Taxrate = tempSIO.U8Details[0].Taxrate;//统一表头税率
                tempSIO.Ordercode = tempSIO.U8Details[0].Poid;
                tempSIO.Vencode = tempSIO.U8Details[0].VenCode;
                tempSIO.Venname = tempSIO.U8Details[0].Venname;
                tempSIO.Chkdate = tempSIO.U8Details[0].CheckDate;
                tempSIO.ProOrderId = tempSIO.U8Details[0].iProOrderId;
                tempSIO.BredVouch = "0";

                //2012－12－14添加监管码
                tempSIO.Define10 = txtRegCode.Text.Trim();

                if (SubmitData())
                {
                    MessageBox.Show("提交成功!");
                    Clear();
                    Init();
                    //默认为没有监管码
                    chkRegCode.Checked = false;
                }
                else if (ErrSubmit())
                {
                    MessageBox.Show("提交成功!");
                    Clear();
                    Init();
                }
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
            if (tempSIO.U8Details != null && tempSIO.U8Details.Count > 0)
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
        /// 验证货位
        /// </summary>
        /// <returns>是否存在对应货位</returns>
        private bool ReadPosition()
        {
            txtPosition.Text = txtPosition.Text.Trim().ToUpper();
            //是否误扫标签
            if (txtPosition.Text.IndexOf('-') != -1 || txtPosition.Text.IndexOf('@') != -1 || txtPosition.Text.Length > 12)
                return false;
            //是否已扫
            StockInDetail position = null;
            position = tempSIO.OperateDetails.Find(delegate(StockInDetail sd) { return sd.Position.ToUpper() == txtPosition.Text; });
            if (position != null)
                return true;
            //是否存在
            return StockInBusiness.GetPTExist(tempSIO.Whcode, txtPosition.Text);
        }
        /// <summary>
        /// 验证存货
        /// </summary>
        /// <returns>是否存在对应存货</returns>
        private bool ReadLabel()
        {
            foreach (StockInDetail sid in tempSIO.U8Details)
            {
                if (sid.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper())
                {
                    if (sid.bInvBatch && sid.Batch.ToUpper() != lblBatch.Text.Trim().ToUpper())
                        continue;
                    lblName.Text = sid.Invname;        //品名
                    lblStandard.Text = sid.cInvStd;     //规格 
                    //lblcInvCode.Text = sid.cInvCode;    //存货编码
                    lblAddrCode.Text = sid.Address;    //产地
                    decimal qty = 0;
                    foreach (StockInDetail sdd in tempSIO.OperateDetails)
                    {
                        //累计扫描数量
                        if (sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Quantity > 0 && sdd.Batch.ToUpper() == lblBatch.Text.Trim().ToUpper())
                        {
                            if (!string.IsNullOrEmpty(lblBatch.Text) && sdd.Batch.ToUpper() != lblBatch.Text.Trim().ToUpper())
                                continue;
                            qty += sdd.Quantity;
                        }
                    }
                    lblDoneNum.Text = qty.ToString("F4");

                    //非二维码扫描从来源单据读取产期
                    if (!is2Code && sid.bInvBatch && sid.Massdate > 0)
                    {
                        try
                        {
                            lblValityDate.Text = DateTime.Parse(sid.Vdate).AddDays(-1).ToString("yyyy-MM-dd");
                            lblProDate.Text = DateTime.Parse(sid.Madedate).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            lblValityDate.Text = Common.err_Time.AddMonths((int)sid.Massdate).AddDays(-1).ToString("yyyy-MM-dd");
                            lblProDate.Text = Common.err_Time.ToString("yyyy-MM-dd");
                        }
                    }
                    //txtBatch.Text = sid.Batch;
                    //
                    cbxBatch.Visible = sid.bInvBatch;
                    cbxBatch.Enabled = sid.bInvBatch;
                    txtCount.Focus();
                    txtCount.SelectAll();
                    txtOrder.Tag = sid;
                    return true;
                }
            }

            MessageBox.Show("没有相应的物料信息！");
            Clear();
            return false;
        }
        /// <summary>
        /// 读取批号
        /// </summary>
        /// <returns>格式是否正确</returns>
        private bool ReadBatch()
        {
            try
            {
                //来源单据是否存在该批号
                StockInDetail sdl = null;
                sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == txtLabel.Text.Trim() && sdd.Batch == lblBatch.Text; });
                if (sdl == null)
                    return false;

                try
                {
                    if (sdl.Massdate > 0)
                    {
                        lblValityDate.Text = DateTime.Parse(sdl.Vdate).AddDays(-1).ToString("yyyy-MM-dd");
                        lblProDate.Text = DateTime.Parse(sdl.Madedate).ToString("yyyy-MM-dd");
                    }
                    return true;
                }
                catch
                {
                    lblProDate.Text = "";
                    lblValityDate.Text = "";
                    return false;
                }
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
                    decimal qty = Convert.ToDecimal(_qty) * scanCount;  //待入库数量
                    decimal inQty = 0;  //已入库数量
                    StockInDetail sdl;
                    sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Batch.ToUpper() == lblBatch.Text.Trim().ToUpper() && sdd.Nquantity > 0; });
                    if (sdl == null)
                    {
                        MessageBox.Show("该批次货物已全部扫描！");
                        return false;
                    }
                    inQty = sdl.fShallInQuan - sdl.Nquantity;

                    if (sdl.bInvBatch && string.IsNullOrEmpty(lblBatch.Text))
                    {
                        MessageBox.Show("该货物有批次管理,请输入批次后重试！");
                        return false;
                    }
                    else if (!sdl.bInvBatch && !string.IsNullOrEmpty(lblBatch.Text))
                    {
                        MessageBox.Show("该货物没有批次管理,请勿输入批次！");
                        return false;
                    }

                    if (qty + inQty > sdl.fShallInQuan)
                    {
                        MessageBox.Show("扫描数量大于应入库数量！" + Environment.NewLine + "应不大于" + sdl.Nquantity.ToString("F4") + sdl.Inva_unit);
                        return false;
                    }

                    StockInDetail tempSDL = sdl.getNewDetail();
                    if (!addData(qty, tempSDL))
                        return false;
                    AddPosition(qty);
                    //来源数据数量减少
                    sdl.Nquantity -= qty;

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
        /// 提交单据
        /// </summary>
        /// <returns>是否提交成功</returns>
        private bool SubmitData()
        {
            if (rbtArrival.Checked == true)
                tempSIO.Source = "采购到货单";
            else
                tempSIO.Source = "入库验收单";

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                StockInBusiness.Save(tempSIO, tempSIO.SaveVouch);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
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
            tempSIO = new StockIn();
            tempSIO.U8Details = new List<StockInDetail>();
            tempSIO.OperateDetails = new List<StockInDetail>();
            tempSIO.OperaPositions = new List<InvPositionInfo>();

            InputPannel.Enabled = false;
            txtOrder.Enabled = true;
            rbtArrival.Enabled = true;
            rbtQCheck.Enabled = true;
            txtPosition.Enabled = false;
            txtLabel.Enabled = false;
            btnSource.Enabled = false;
            btnDone.Enabled = false;
            btnSubmit.Enabled = false;

            txtOrder.Text = "";
            txtPosition.Text = "";
            lblStore.Text = "";

            SetFocus();
        }
        #endregion

        #region Clear
        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            lblBatch.Enabled = false;
            lblBatch.Visible = false;
            txtCount.Enabled = false;
            cbxBatch.Enabled = false;
            cbxBatch.Visible = false;

            txtLabel.Text = "";
            lblBatch.Text = "";
            txtCount.Text = "";
            lblDoneNum.Text = "";
            lblName.Text = "";
            lblStandard.Text = "";
            lblAddrCode.Text = "";
            lblProDate.Text = "";
            lblValityDate.Text = "";

            is2Code = false;
            SetPosition();
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
        #endregion

        #region VerifyOrder
        /// <summary>
        /// 验证单据号
        /// </summary>
        /// <returns>格式是否正确</returns>
        private bool VerifyOrder()
        {
            if (txtOrder.Text.Length > 23 || txtOrder.Text.Length < 8)//长度匹配
            {
                txtOrder.Text = "";
                return false;
            }
            else if (txtOrder.Text.IndexOf('-') != -1 || txtOrder.Text.IndexOf('@') != -1)//是否误扫标签
            {
                txtOrder.Text = "";
                return false;
            }
            else
            {
                txtOrder.Text = txtOrder.Text.Trim().ToUpper();
                return true;
            }
        }
        #endregion

        #region verifyArrive
        /// <summary>
        /// 获取到货单信息
        /// </summary>
        /// <param name="ArriveCode">到货单号</param>
        /// <returns>是否存在</returns>
        private bool verifyArrive(string ArriveCode)
        {
            ArriveCode = ArriveCode.ToUpper();
            try
            {
                VerifyLength(ArriveCode);
                DataSet ds;
                if (rbtArrival.Checked)
                {
                    tempSIO = StockInBusiness.CreateSIOrderArrive(ArriveCode, "", "", 0, out ds);
                }
                else
                {
                    if (ArriveCode.Length < 12)
                        tempSIO = StockInBusiness.CreateSIOrderByGSPVouch(ArriveCode, null, out ds);//质检单来源
                    else
                        tempSIO = StockInBusiness.CreateSIOrderByGSPVouch(null, ArriveCode, out ds);//到货单来源
                }
                if (tempSIO.U8Details == null || tempSIO.U8Details.Count < 1)
                    return false;
                //是否录入仓库编码
                if (string.IsNullOrEmpty(tempSIO.Whcode))
                {
                    MessageBox.Show("对不起,该单据尚未指明入库仓库!");
                    return false;
                }
                //是否有单据仓库操作权限
                Warehouse stock = null;
                stock = Common.s_Warehouse.Find(delegate(Warehouse wh) { return wh.cwhcode.Trim() == tempSIO.Whcode.Trim(); });
                if (stock == null)
                {
                    MessageBox.Show("对不起,您没有该单据仓库的操作权限!");
                    return false;
                }
                //补全仓库信息
                if (string.IsNullOrEmpty(tempSIO.Whname))
                {
                    tempSIO.Whname = stock.cwhname;
                    tempSIO.WhPos = stock.bwhpos == 1 ? true : false;
                }

                //仓库名赋值
                lblStore.Text = tempSIO.Whname;
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

        #region VerifyLength
        /// <summary>
        /// 判断单号类型
        /// </summary>
        /// <param name="ArriveCode">单据号</param>
        private void VerifyLength(string ArriveCode)
        {
            DialogResult dr;
            if (rbtArrival.Checked && ArriveCode.Length == 10)
            {
                dr = MessageBox.Show("是否要扫描质检单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    rbtArrival.Checked = false;
                    rbtQCheck.Checked = true;
                }
            }
            else if (rbtQCheck.Checked && ArriveCode.Length != 10)
            {
                dr = MessageBox.Show("是否要扫描到货单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    rbtQCheck.Checked = false;
                    rbtArrival.Checked = true;
                }
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
        private bool addData(decimal _qty, StockInDetail sd)
        {
            try
            {
                string strPostion = string.IsNullOrEmpty(txtPosition.Text) ? "" : txtPosition.Text.Trim().ToUpper();//货位编码
                sd.Batch = lblBatch.Text.Trim().ToUpper();
                if (tempSIO.OperateDetails.Count > 0)
                {
                    foreach (StockInDetail siodl in tempSIO.OperateDetails)
                    {
                        if (siodl.cInvCode == sd.cInvCode && siodl.Batch.ToUpper() == sd.Batch && siodl.Nquantity > 0)//看还要加什么条件？
                        {
                            if (siodl.fShallInQuan >= _qty + siodl.Quantity)
                            {
                                siodl.Quantity = siodl.Quantity + Convert.ToDecimal(_qty);//数量累加

                                siodl.Price = siodl.Quantity * siodl.Unitcost;  //本币无税金额
                                siodl.Taxprice = siodl.Price * siodl.Taxrate * Convert.ToDecimal(0.01); //本币税额
                                siodl.Sum = siodl.Price + siodl.Taxprice;   //本币价税合计
                                siodl.Orimoney = siodl.Quantity * siodl.Oricost;    //原币无税金额
                                siodl.Oritaxprice = siodl.Orimoney * siodl.Taxrate * Convert.ToDecimal(0.01);   //原币税额
                                siodl.Orisum = siodl.Orimoney + siodl.Oritaxprice;  //原币价税合计
                                if (!string.IsNullOrEmpty(strPostion) && siodl.Position != strPostion)
                                    siodl.Position = "";

                                return true;
                            }
                            else
                            {
                                MessageBox.Show("已扫描数量大于最大扫描数量");
                                Clear();
                                return false;
                            }
                        }
                    }
                }
                //统一仓库
                sd.cWhCode = tempSIO.Whcode;
                sd.cWhName = tempSIO.Whname;
                sd.Quantity = _qty;
                sd.Price = sd.Quantity * sd.Unitcost;//本币无税金额
                sd.Taxprice = sd.Price * sd.Taxrate * Convert.ToDecimal(0.01);//本币税额
                sd.Sum = sd.Price + sd.Taxprice;//本币价税合计
                sd.Orimoney = sd.Quantity * sd.Oricost;//原币无税金额
                sd.Oritaxprice = sd.Orimoney * sd.Taxrate * Convert.ToDecimal(0.01);//原币税额
                sd.Orisum = sd.Orimoney + sd.Oritaxprice;//原币价税合计
                //效期管理
                if (sd.Massdate > 0)
                {
                    sd.Madedate = lblProDate.Text;
                    sd.cExpirationDate = lblValityDate.Text;
                    if (!string.IsNullOrEmpty(sd.cExpirationDate.Trim()))
                        sd.Vdate = DateTime.Parse(sd.cExpirationDate).AddDays(1).ToString("yyyy-MM-dd");
                    sd.Massunit = "2";
                    sd.iExpiratDateCalcu = 2;
                }
                //货位管理
                if (!string.IsNullOrEmpty(strPostion) && tempSIO.WhPos)
                {
                    sd.Position = strPostion;
                    sd.IsPos = true;//已扫数据显示参数
                }

                tempSIO.OperateDetails.Add(sd);
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

        #region addPosition
        /// <summary>
        /// 货位信息累加
        /// </summary>
        /// <param name="qty">扫描数量</param>
        private void AddPosition(decimal qty)
        {
            if (string.IsNullOrEmpty(txtPosition.Text) || !tempSIO.WhPos)
                return;
            string tempBatch = lblBatch.Text.Trim().ToUpper();//批号
            string tempICode = txtLabel.Text.Trim().ToUpper();//存货编码
            string tempPCode = txtPosition.Text.Trim().ToUpper();//货位编码
            if (string.IsNullOrEmpty(tempICode))
                return;
            //该记录是否存在
            InvPositionInfo position = null;
            if (tempSIO.OperaPositions != null && tempSIO.OperaPositions.Count > 0)
                position = tempSIO.OperaPositions.Find(delegate(InvPositionInfo ipi) { return ipi.PosCode == tempPCode && ipi.Batch == tempBatch && ipi.InvCode == tempICode; });
            if (position != null)
            {
                position.Quantity += qty;
                return;
            }
            else
            {
                position = new InvPositionInfo();
                position.Quantity = qty;
                position.Batch = tempBatch;
                position.InvCode = tempICode;
                position.PosCode = tempPCode;
                position.InvName = lblName.Text;
                position.InvStd = lblStandard.Text;
                position.Address = lblAddrCode.Text;
                position.MadeDate = lblProDate.Text;
                position.ExpirationDate = lblValityDate.Text;
                if (!string.IsNullOrEmpty(position.ExpirationDate.Trim()))
                    position.VDate = DateTime.Parse(position.ExpirationDate).AddDays(1).ToString("yyyy-MM-dd");
                tempSIO.OperaPositions.Add(position);
            }
        }

        #endregion

        #region VerifyScan
        /// <summary>
        /// 验证已操作数据
        /// </summary>
        /// <returns>0全部扫描1少批次2批次内少数量</returns>
        private int VerifyScan()
        {
            int result = 0;
            if (tempSIO.U8Details.Count > tempSIO.OperateDetails.Count)
            {
                result = 1;
            }

            foreach (StockInDetail sid in tempSIO.U8Details)
            {
                decimal quan = 0;
                foreach (StockInDetail avs in tempSIO.OperateDetails)//已扫数据累加
                {
                    if (sid.cInvCode.ToUpper() == avs.cInvCode.ToUpper() && sid.Batch.ToUpper() == avs.Batch.ToUpper())
                        quan += avs.Quantity;
                }
                if (sid.Nquantity > quan)
                {
                    result = 1;
                    if (quan != 0)//已经扫描过
                    {
                        result = 2;
                        break;
                    }
                }
            }
            return result;
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
                    barcode[0] = strBarCode;
                    if (!Common.GetCInvCode(strBarCode, out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！");
                        return false;
                    }
                    barcode[2] = cInvCode;
                    txtLabel.Text = barcode[2];
                    //是否有该存货及是否批次管理
                    StockInDetail sd;
                    sd = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == cInvCode; });
                    if (sd != null && sd.bInvBatch)
                    {
                        cbxBatch.Visible = true;
                        cbxBatch.Enabled = true;
                        List<string> batchList = new List<string>();
                        foreach (StockInDetail sid in tempSIO.U8Details)
                        {
                            if (sid.cInvCode.ToUpper() == cInvCode.ToUpper())
                            {
                                //是否已扫描完毕
                                StockInDetail opare = null;
                                opare = tempSIO.OperateDetails.Find(delegate(StockInDetail odl) { return odl.cInvCode == sid.cInvCode && odl.Batch == sid.Batch; });
                                if (opare != null && opare.Quantity >= sid.fShallInQuan)
                                    continue;
                                //是否已添加
                                string temp = null;
                                temp = batchList.Find(delegate(string batch) { return batch.ToUpper() == sid.Batch.ToUpper(); });
                                if (string.IsNullOrEmpty(temp))
                                    batchList.Add(sid.Batch.ToUpper());
                            }
                        }
                        cbxBatch.DataSource = batchList;
                        if (cbxBatch.Items.Count > 0)
                            cbxBatch.SelectedIndex = 0;
                        //else
                        //    return false;
                        if (cbxBatch.DataSource != null && cbxBatch.Items.Count > 0)
                            lblBatch.Text = cbxBatch.SelectedValue.ToString();
                    }
                }
                else
                {   //二维码
                    is2Code = true;
                    barcode = strBarCode.Split('@');
                    cbxBatch.Enabled = false;
                    cbxBatch.Visible = false;
                    lblBatch.Enabled = true;
                    lblBatch.Visible = true;
                    if (!Common.GetCInvCode(barcode[0], out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！");
                        return false;
                    }
                    barcode[2] = cInvCode;
                    txtLabel.Text = barcode[2];
                    lblBatch.Text = string.IsNullOrEmpty(barcode[3]) ? "" : barcode[3];
                    //读取产期
                    try
                    {
                        lblProDate.Text = DateTime.Parse(barcode[4]).ToString("yyyy-MM-dd");
                        lblValityDate.Text = DateTime.Parse(barcode[5]).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        lblProDate.Text = Common.err_Time.ToString("yyy-MM-dd");
                        lblValityDate.Text = Common.err_Time.ToString("yyy-MM-dd");
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

        #region SetPosition
        /// <summary>
        /// 设置货位录入状态
        /// </summary>
        private void SetPosition()
        {
            if (tempSIO == null)
            {
                txtPosition.Enabled = false;
                txtLabel.Enabled = false;
                txtOrder.Focus();
                txtOrder.SelectAll();
                return;
            }

            if (tempSIO.WhPos)
            {
                txtLabel.Enabled = false;
                txtPosition.Enabled = true;
                txtPosition.Focus();
                txtPosition.SelectAll();
            }
            else
            {
                txtPosition.Enabled = false;
                txtLabel.Enabled = true;
                txtLabel.Focus();
                txtLabel.SelectAll();
            }
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
            else
            {
                txtCount.Focus();
                txtCount.SelectAll();
            }
        }

        #endregion

        #region ErrSubmit
        /// <summary>
        /// 提交失败单据状况
        /// </summary>
        /// <returns>是否提交成功</returns>
        private bool ErrSubmit()
        {
            string cCode = txtOrder.Text;
            DataSet ds;
            StockIn sk;
            if (rbtArrival.Checked)
            {
                sk = StockInBusiness.CreateSIOrderArrive(cCode, "", "", 0, out ds);
            }
            else
            {
                sk = StockInBusiness.CreateSIOrderByGSPVouch(null, cCode, out ds);
            }
            //行数是否相同
            if (sk == null)
                return true;
            if (sk.U8Details == null || sk.U8Details.Count < 1)
                return true;
            if (sk.U8Details.Count != tempSIO.U8Details.Count)
                return true;
            //数量是否相同
            StockInDetail opera = tempSIO.OperateDetails[0];
            StockInDetail sd = null;
            sd = sk.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == opera.cInvCode.ToUpper() && sdd.Batch.ToUpper() == opera.Batch.ToUpper(); });
            if (sd == null || sd.fShallInQuan != opera.fShallInQuan)
                return true;

            return false;
        }
        #endregion

        

        #endregion


        /// <summary>
        /// checkbox选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRegCode_CheckStateChanged(object sender, EventArgs e)
        {
            //如果选中
            if (chkRegCode.Checked)
            {
                txtRegCode.Enabled = true;
                btnRegCode.Enabled = true;
            }
            else
            {
                txtRegCode.Text = string.Empty;
                txtRegCode.Enabled = false;
                btnRegCode.Enabled = false;
            }
        }

        /// <summary>
        /// 点击读取监管码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegCode_Click(object sender, EventArgs e)
        {
            string errMsg;
            Model.Regulatory data = U8Business.Regulatory.GetModel(out errMsg);
            if (data == null)
            {
                MessageBox.Show(errMsg);
                return;
            }
            txtRegCode.Text = data.RegCode;
        }
    }
}