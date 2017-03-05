﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmOSStuffOut : Form
    {
        StockIn tempSIO;    //临时表头对象（多订单时用）

        decimal scanCount;  //包装数量
        bool is2Code;   //是否二维码
        List<BatchInfo> batchList;  //批次信息

        #region 页面初始化

        public frmOSStuffOut()
        {
            InitializeComponent();
        }

        private void frmOSStuffOut_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            Clear();
            Init();
        }
        #endregion

        private void txtOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtOrder.Text))
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
                    tempSIO.IsOut = true;
                    tempSIO.SaveVouch = "04";

                    txtOrder.Enabled = false;
                    btnSource.Enabled = true;

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
                        //rbtGet.Enabled = true;
                        //rbtAdd.Enabled = true;
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
                        //rbtGet.Enabled = false;
                        //rbtAdd.Enabled = false;
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

        private void cbxWareHouse_LostFocus(object sender, EventArgs e)
        {
            txtLabel.Focus();
            txtLabel.SelectAll();
        }

        private void cbxBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLabel.Text) || txtLabel.Enabled)
                return;

            if (sender != null)
            {
                lblBatch.Text = cbxBatch.SelectedValue.ToString();
            }

            ReadLabel();
        }

        private void cbxBatch_LostFocus(object sender, EventArgs e)
        {
            if (cbxBatch.DataSource == null || cbxBatch.Items.Count < 0 || cbxBatch.SelectedIndex < 0 || txtLabel.Enabled)
                return;
            txtCount.Text = "1";
            txtCount.Focus();
            txtCount.SelectAll();
        }

        private void InputPannel_Click(object sender, EventArgs e)
        {
            InputPannel.Enabled = !InputPannel.Enabled;
            Clear();
            if (tempSIO != null && tempSIO.OperateDetails != null && tempSIO.OperateDetails.Count > 0)
                btnSubmit.Enabled = true;
            txtLabel.Focus();
            txtLabel.SelectAll();
        }

        private void rbt_Click(object sender, EventArgs e)
        {
            txtLabel.Focus();
            txtLabel.SelectAll();
        }

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

                if (tempSIO.OperateDetails == null || tempSIO.OperateDetails.Count < 1)
                {
                    btnDone.Enabled = false;
                }
            }
            catch { MessageBox.Show("操作失误,请重试!"); }
            finally { SetFocus(); }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tempSIO.OperateDetails == null || tempSIO.OperateDetails.Count < 1)
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
                tempSIO.Handler = Common.CurrentUser.UserName;
                tempSIO.Date = DateTime.Now.ToString("yyyy-MM-dd");
                tempSIO.Vouchtype = "11";
                tempSIO.Rdcode = "209";
                tempSIO.Rdname = "委外发料";
                tempSIO.Bustype = "委外发料";
                tempSIO.nmaketime = DateTime.Now.ToString();
                tempSIO.nverifytime = tempSIO.nmaketime;
                tempSIO.Veridate = DateTime.Now.ToString();
                tempSIO.Maker = Common.CurrentUser.UserName;
                tempSIO.Source = "委外订单";
                tempSIO.Taxrate = tempSIO.U8Details[0].Taxrate;
                tempSIO.BredVouch = "0";

                if (SubmitData())
                {
                    MessageBox.Show("提交成功!");
                    Clear();
                    Init();
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

        private bool ReadPosition()
        {
            txtPosition.Text = txtPosition.Text.Trim().ToUpper();
            if (txtPosition.Text.IndexOf('-') != -1 || txtPosition.Text.IndexOf('@') != -1 || txtPosition.Text.Length > 12)
                return false;
            StockInDetail position = null;
            position = tempSIO.OperateDetails.Find(delegate(StockInDetail sd) { return sd.Position.ToUpper() == txtPosition.Text; });
            if (position != null)
                return true;
            return StockInBusiness.GetPTExist(tempSIO.Whcode, txtPosition.Text);
        }

        private bool ReadLabel()
        {
            foreach (StockInDetail sid in tempSIO.U8Details)
            {
                if (sid.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper())
                {
                    if (sid.bInvBatch && batchList == null || sid.bInvBatch && batchList.Count < 1)
                        continue;
                    StockInDetail opare = null;
                    opare = tempSIO.OperateDetails.Find(delegate(StockInDetail odl) { return odl.cInvCode == sid.cInvCode && odl.cMoDetailsID == sid.cMoDetailsID; });
                    if (opare != null)
                    {
                        if (opare.Quantity >= opare.fShallInQuan)
                            continue;
                    }
                    lblName.Text = sid.Invname;        //品名
                    lblStandard.Text = sid.cInvStd;     //规格 
                    //lblcInvCode.Text = sid.cInvCode;    //存货编码
                    lblAddrCode.Text = sid.Address;    //产地
                    decimal qty = 0;
                    foreach (StockInDetail sdd in tempSIO.OperateDetails)
                    {
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
                        lblValityDate.Text = batchList[cbxBatch.SelectedIndex].Expirationdate;
                        lblProDate.Text = batchList[cbxBatch.SelectedIndex].Mdate;
                        if (string.IsNullOrEmpty(lblValityDate.Text))
                        {
                            lblValityDate.Text = DateTime.Parse(lblProDate.Text).AddMonths(Convert.ToInt32(sid.Massdate)).AddDays(-1).ToString("yyyy-MM-dd");
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

            MessageBox.Show("没有相应的物料信息或已扫描完毕");
            Clear();
            return false;
        }

        private bool ReadBatch()
        {
            try
            {
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

        private bool RecordNum()
        {
            string _qty = txtCount.Text.Trim();
            try
            {
                if (txtOrder.Tag != null)
                {
                    decimal qty = Convert.ToDecimal(_qty) * scanCount;
                    decimal inQty = 0;
                    StockInDetail sdl;
                    sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Nquantity > 0; });

                    if (sdl == null)
                    {
                        MessageBox.Show("该批次货物已全部扫描！");
                        return false;
                    }

                    //if (sdl.Nquantity <= 0)
                    //{
                    //    DialogResult res = MessageBox.Show("该货物已全部扫描,仍需要扫描吗?", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //    if (res == DialogResult.No)
                    //    {
                    //        return false;
                    //    }
                    //}
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
                        MessageBox.Show("扫描数量大于最大出库数量！" + Environment.NewLine + "应不大于" + sdl.Nquantity.ToString("F4") + sdl.Inva_unit);
                        Clear();
                        return false;
                    }

                    if (!string.IsNullOrEmpty(txtPosition.Text))
                    {
                        if (qty + inQty > StockInBusiness.GetPTQuan(txtLabel.Text, "", tempSIO.Whcode, txtPosition.Text))
                        {
                            MessageBox.Show("扫描数量大于货位现有数量！");
                            Clear();
                            return false;
                        }
                    }

                    if (qty + inQty > StockInBusiness.GetWHQuan(txtLabel.Text, "", tempSIO.Whcode))
                    {
                        MessageBox.Show("扫描数量大于库存现有数量！");
                        Clear();
                        return false;
                    }

                    if (batchList != null && batchList.Count > 0)
                    {
                        if (inQty > 0)
                        {
                            StockInDetail sid;
                            sid = tempSIO.OperateDetails.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Batch == lblBatch.Text && sdd.Quantity > 0; });
                            if (sid != null)
                                inQty = sid.Quantity;
                            else
                                inQty = 0;
                        }
                        if (qty + inQty > batchList[cbxBatch.SelectedIndex].Quantity)
                        {
                            MessageBox.Show("扫描数量大于该批次现有数量！");
                            Clear();
                            return false;
                        }
                    }

                    StockInDetail tempSDL = sdl.getNewDetail();
                    if (!addData(qty, tempSDL))
                        return false;
                    AddPosition(qty);
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

        private bool SubmitData()
        {
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                //if (!ConvertValue())
                //    return false;
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
            }
        }

        private void Init()
        {
            tempSIO = new StockIn();
            tempSIO.U8Details = new List<StockInDetail>();
            tempSIO.OperateDetails = new List<StockInDetail>();
            tempSIO.OperaPositions = new List<InvPositionInfo>();

            InputPannel.Enabled = false;
            txtOrder.Enabled = true;
            txtPosition.Enabled = false;
            //rbtGet.Enabled = false;
            //rbtAdd.Enabled = false;
            txtLabel.Enabled = false;
            btnSource.Enabled = false;
            btnDone.Enabled = false;
            btnSubmit.Enabled = false;

            txtOrder.Text = "";
            txtPosition.Text = "";
            lblStore.Text = "";

            SetFocus();
        }

        private void Clear()
        {
            lblBatch.Enabled = false;
            lblBatch.Visible = false;
            txtCount.Enabled = false;
            cbxBatch.Enabled = false;
            cbxBatch.Visible = false;

            //rbtGet.Enabled = true;
            //rbtAdd.Enabled = true;
            
            txtLabel.Text = "";
            lblBarCode.Text = "";
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

        #region verifyPomain
        private bool verifyArrive(string ArriveCode)
        {
            ArriveCode = ArriveCode.ToUpper();
            try
            {
                DataSet ds;
                tempSIO = StockInBusiness.CreateSIOrderMomain(ArriveCode, out ds);
                if (tempSIO.U8Details == null || tempSIO.U8Details.Count < 1)
                    return false;
                //if (!ConvertValue())
                //    return false;

                if (string.IsNullOrEmpty(tempSIO.Whcode))
                {
                    MessageBox.Show("对不起,该单据尚未指明入库仓库!");
                    return false;
                }
                Warehouse stock = null;
                stock = Common.s_Warehouse.Find(delegate(Warehouse wh) { return wh.cwhcode.Trim() == tempSIO.Whcode.Trim(); });
                if (stock == null)
                {
                    MessageBox.Show("对不起,您没有该单据仓库的操作权限!");
                    return false;
                }

                if (string.IsNullOrEmpty(tempSIO.Whname))
                {
                    tempSIO.Whname = stock.cwhname;
                    tempSIO.WhPos = stock.bwhpos == 1 ? true : false;
                }

                //Clear();
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

        #region addData
        private bool addData(decimal _qty, StockInDetail sd)
        {
            try
            {
                string strPostion = string.IsNullOrEmpty(txtPosition.Text) ? "" : txtPosition.Text.Trim().ToUpper();
                sd.Batch = lblBatch.Text.Trim().ToUpper(); 
                StockInDetail siodl;
                siodl = tempSIO.OperateDetails.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == sd.cInvCode && sdd.Batch == sd.Batch && sdd.cMoDetailsID == sd.cMoDetailsID && sdd.Nquantity > 0; });

                if (tempSIO.OperateDetails.Count > 0 && siodl != null)
                {
                    decimal quan = 0;
                    foreach (StockInDetail si in tempSIO.OperateDetails)
                    {
                        if (si.cInvCode == sd.cInvCode && si.cMoDetailsID == sd.cMoDetailsID)
                            quan += si.Quantity;
                    }
                    if (siodl.fShallInQuan >= _qty + quan)
                    {
                        siodl.Quantity = siodl.Quantity + Convert.ToDecimal(_qty);

                        siodl.Price = siodl.Quantity * siodl.Unitcost;
                        siodl.Taxprice = siodl.Price * siodl.Taxrate * Convert.ToDecimal(0.01);
                        siodl.Sum = siodl.Price + siodl.Taxprice;
                        siodl.Orimoney = siodl.Quantity * siodl.Oricost;
                        siodl.Oritaxprice = siodl.Orimoney * siodl.Taxrate * Convert.ToDecimal(0.01);
                        siodl.Orisum = siodl.Orimoney + siodl.Oritaxprice;
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
                sd.InvCode = tempSIO.PspCode;
                sd.cWhCode = tempSIO.Whcode;
                sd.cWhName = tempSIO.Whname;
                //sd.Bvencode = tempSIO.Vencode;
                sd.Quantity = _qty;
                sd.Price = sd.Quantity * sd.Unitcost;
                sd.Taxprice = sd.Price * sd.Taxrate * Convert.ToDecimal(0.01);
                sd.Sum = sd.Price + sd.Taxprice;
                sd.Orimoney = sd.Quantity * sd.Oricost;
                sd.Oritaxprice = sd.Orimoney * sd.Taxrate * Convert.ToDecimal(0.01);
                sd.Orisum = sd.Orimoney + sd.Oritaxprice;
                sd.Batch = lblBatch.Text.Trim();
                if (sd.Massdate > 0)
                {
                    sd.Madedate = lblProDate.Text;
                    sd.cExpirationDate = lblValityDate.Text;
                    if (!string.IsNullOrEmpty(sd.cExpirationDate.Trim()))
                        sd.Vdate = DateTime.Parse(sd.cExpirationDate).AddDays(1).ToString("yyyy-MM-dd");
                    sd.Massunit = "2";
                }
                if (!string.IsNullOrEmpty(strPostion) && tempSIO.WhPos)
                {
                    sd.Position = strPostion;
                    sd.IsPos = true;
                }

                tempSIO.OperateDetails.Add(sd);

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

        #region 数值转换

        private bool ConvertValue()
        {
            try
            {
                foreach (StockInDetail temp in tempSIO.OperateDetails)
                {
                    temp.Taxprice *= -1;
                    temp.Price *= -1;
                    temp.Aprice *= -1;
                    temp.Money *= -1;
                    temp.Orimoney *= -1;
                    temp.Orisum *= -1;
                    temp.Oritaxprice *= -1;
                    temp.Nquantity *= -1;
                    temp.Quantity *= -1;
                    temp.fShallInQuan *= -1;
                }
                foreach (StockInDetail temp in tempSIO.U8Details)
                {
                    temp.Taxprice *= -1;
                    temp.Price *= -1;
                    temp.Aprice *= -1;
                    temp.Money *= -1;
                    temp.Orimoney *= -1;
                    temp.Orisum *= -1;
                    temp.Oritaxprice *= -1;
                    temp.Nquantity *= -1;
                    temp.Quantity *= -1;
                    temp.fShallInQuan *= -1;
                }

                foreach (InvPositionInfo temp in tempSIO.OperaPositions)
                {
                    temp.Quantity *= -1;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 验证扫描

        private bool VerifyScan()
        {
            if (tempSIO.U8Details.Count > tempSIO.OperateDetails.Count)
            {
                return false;
            }

            foreach (StockInDetail sid in tempSIO.U8Details)
            {
                decimal quan = 0;
                foreach (StockInDetail avs in tempSIO.OperateDetails)
                {
                    if (sid.cInvCode.ToUpper() == avs.cInvCode.ToUpper())
                        quan += avs.Quantity;
                }
                if (sid.Nquantity > quan)
                    return false;
            }
            return true;
        }
        #endregion

        #region addPosition

        private void AddPosition(decimal qty)
        {
            if (string.IsNullOrEmpty(txtPosition.Text) || !tempSIO.WhPos)
                return;
            string tempBatch = lblBatch.Text.Trim().ToUpper();
            string tempICode = txtLabel.Text.Trim().ToUpper();
            string tempPCode = txtPosition.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(tempICode))
                return;
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

        #region ScanLabel
        private bool ScanLabel()
        {
            string errMsg = "";
            string cInvCode = "";
            string strBarCode = txtLabel.Text.Trim().ToUpper();
            txtLabel.Text = "";
            string[] barcode = new string[7] { "", "", "", "", "", "", "" };
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
                    lblBarCode.Text = barcode[0];
                    txtLabel.Text = barcode[2];
                    StockInDetail sd;
                    sd = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == cInvCode; });
                    if (sd != null && sd.bInvBatch)
                    {
                        cbxBatch.Visible = true;
                        cbxBatch.Enabled = true;
                        batchList = StockInBusiness.GetBatchInfo(cInvCode, "", tempSIO.Whcode);
                        cbxBatch.DataSource = batchList;
                        cbxBatch.DisplayMember = "DisPlayMember";
                        cbxBatch.ValueMember = "Batch";
                        if (cbxBatch.DataSource != null && cbxBatch.Items.Count > 0)
                            cbxBatch.SelectedIndex = 0;
                        else
                        {
                            MessageBox.Show("该仓库暂无存货!");
                            return false;
                        }
                        lblBatch.Text = cbxBatch.SelectedValue.ToString();
                    }
                }
                else
                {   //二维码
                    is2Code = true;
                    barcode = strBarCode.Split('@');
                    cbxBatch.Visible = false;
                    cbxBatch.Enabled = false;
                    batchList = null;
                    lblBatch.Enabled = true;
                    lblBatch.Visible = true;
                    if (!Common.GetCInvCode(barcode[0], out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！");
                        return false;
                    }
                    barcode[2] = cInvCode;
                    lblBarCode.Text = barcode[0];
                    txtLabel.Text = barcode[2];
                    lblBatch.Text = string.IsNullOrEmpty(barcode[3]) ? "" : barcode[3];
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

        #region VerifyOrder

        private bool VerifyOrder()
        {
            if (txtOrder.Text.Length > 23 || txtOrder.Text.Length < 8)
            {
                txtOrder.Text = "";
                return false;
            }
            else if (txtOrder.Text.IndexOf('-') != -1 || txtOrder.Text.IndexOf('@') != -1)
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
            sk = StockInBusiness.CreateSIOrderMomain(cCode, out ds);

            if (sk.U8Details == null || sk.U8Details.Count < 1)
                return true;
            if (sk.U8Details.Count != tempSIO.U8Details.Count)
                return true;
            StockInDetail opera = tempSIO.OperateDetails[0];
            StockInDetail sd = null;
            sd = sk.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == opera.cInvCode.ToUpper() && sdd.cMoDetailsID == opera.cMoDetailsID; });
            if (sd == null || sd.fShallInQuan != opera.fShallInQuan)
                return true;

            return false;
        }

        #endregion
    }
}