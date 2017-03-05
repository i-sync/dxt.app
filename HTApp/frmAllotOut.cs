using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmAllotOut : Form
    {
        bool isloaded = false;
        StockIn tempSIO;//临时表头对象（多订单时用）
        
        decimal scanCount;//包装数量
        bool is2Code;//是否二维码

        public frmAllotOut()
        {
            InitializeComponent();
            try
            {
                //DataSet ds;
                //StockInBusiness.CreateSIOrderByDismantle("", out ds, "21");
                //if (ds.Tables == null || ds.Tables["head"].Rows.Count == 0)
                //{
                //    MessageBox.Show("暂无未审核调拨单！");
                //    return;
                //}

                isloaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAllotOut_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            Clear();
            Init();
        }

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
                    tempSIO.IsOut = true;
                    tempSIO.SaveVouch = "t21";

                    txtOrder.Enabled = false;
                    rbtOther.Enabled = false;
                    rbtRecord.Enabled = false;
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

        private void txtPosition_EnabledChanged(object sender, EventArgs e)
        {
            if (!txtPosition.Enabled)
            {
                txtLabel.Focus();
                txtLabel.SelectAll();
                return;
            }
            if (tempSIO == null || string.IsNullOrEmpty(tempSIO.WhPos.ToString()))
                return;

            txtPosition.Enabled = tempSIO.WhPos;
            if (txtPosition.Enabled)
            {
                txtPosition.Focus();
                txtPosition.SelectAll();
                return;
            }
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
            txtOrder.Focus();
            txtOrder.SelectAll();
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
                    MessageBox.Show("提交失败！未完成全部扫描！");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                tempSIO.Handler = Common.CurrentUser.UserName;
                tempSIO.Date = DateTime.Now.ToString("yyyy-MM-dd");
                tempSIO.Rdcode = "202";
                tempSIO.Rdname = "调拨出库";
                tempSIO.nmaketime = DateTime.Now.ToString();
                tempSIO.nverifytime = tempSIO.nmaketime;
                tempSIO.Veridate = DateTime.Now.ToString();
                tempSIO.Maker = Common.CurrentUser.UserName;
                tempSIO.Source = "调拨出库单";
                tempSIO.Taxrate = tempSIO.U8Details[0].Taxrate;

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
                    if (!string.IsNullOrEmpty(lblBatch.Text) && sid.Batch.ToUpper() != lblBatch.Text.Trim().ToUpper())
                        continue;
                    lblName.Text = sid.Invname;        //品名
                    lblStandard.Text = sid.cInvStd;     //规格 
                    lblcInvCode.Text = sid.cInvCode;    //存货编码
                    lblAddrCode.Text = sid.Address;    //产地
                    decimal qty = 0;
                    foreach (StockInDetail sdd in tempSIO.OperateDetails)
                    {
                        if (sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Quantity > 0 && sdd.Batch.ToUpper() == lblBatch.Text.Trim().ToUpper())
                        {
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

            MessageBox.Show("没有相应的物料信息");
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
                    sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Batch.ToUpper() == lblBatch.Text.Trim().ToUpper() && sdd.Nquantity > 0; });

                    if (sdl == null)
                    {
                        MessageBox.Show("该批次货物已全部扫描！");
                        return false;
                    }
                    inQty = sdl.fShallInQuan - sdl.Nquantity;

                    if (sdl.bInvBatch && string.IsNullOrEmpty(sdl.Batch))
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
                        MessageBox.Show("扫描数量大于应出库数量！" + Environment.NewLine + "应不大于" + sdl.Nquantity.ToString("F4") + sdl.Inva_unit);
                        return false;
                    }

                    if (!string.IsNullOrEmpty(txtPosition.Text))
                    {
                        if (qty + inQty > StockInBusiness.GetPTQuan(txtLabel.Text, lblBatch.Text, tempSIO.Whcode, txtPosition.Text))
                        {
                            MessageBox.Show("扫描数量大于货位现有数量！");
                            Clear();
                            return false;
                        }
                    }

                    if (qty + inQty > StockInBusiness.GetWHQuan(txtLabel.Text, lblBatch.Text, tempSIO.Whcode))
                    {
                        MessageBox.Show("扫描数量大于库存现有数量！");
                        Clear();
                        return false;
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
                StockInBusiness.Save(tempSIO, tempSIO.SaveVouch);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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
            rbtOther.Enabled = true;
            rbtRecord.Enabled = true;
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

        private void Clear()
        {
            cbxBatch.DataSource = null;
            cbxBatch.Items.Clear();
            cbxBatch.Refresh();

            txtPosition.Enabled = true;
            txtLabel.Enabled = true;
            lblBatch.Enabled = false;
            lblBatch.Visible = false;
            cbxBatch.Enabled = false;
            cbxBatch.Visible = false;
            txtCount.Enabled = false;

            txtLabel.Text = "";
            lblBatch.Text = "";
            txtCount.Text = "";
            lblDoneNum.Text = "";
            lblName.Text = "";
            lblStandard.Text = "";
            lblcInvCode.Text = "";
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

        #region verifyArrive
        private bool verifyArrive(string OrderCode)
        {
            try
            {
                DataSet ds;
                if (rbtOther.Checked)
                    tempSIO = StockInBusiness.CreateSIOrderByDismantle("21", "", OrderCode, out ds);
                else if (rbtRecord.Checked)
                    tempSIO = StockInBusiness.CreateSIOrderByDismantle("21", OrderCode, "", out ds);
                if (tempSIO.U8Details == null || tempSIO.U8Details.Count < 1)
                    return false;

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

                if (tempSIO.OperateDetails.Count > 0)
                {
                    foreach (StockInDetail siodl in tempSIO.OperateDetails)
                    {
                        if (siodl.cInvCode == sd.cInvCode && siodl.Batch == sd.Batch && siodl.Nquantity > 0)//看还要加什么条件？
                        {
                            if (siodl.fShallInQuan >= _qty + siodl.Quantity)
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
                                MessageBox.Show("已扫描数量大于最大扫描数量" + Environment.NewLine + "应小于" + sd.Nquantity.ToString("F4") + sd.Invm_unit + ",已扫描" + siodl.Quantity.ToString("F4") + sd.Invm_unit);
                                Clear();
                                return false;
                            }
                        }
                    }
                }
                sd.cWhCode = tempSIO.Whcode;
                sd.cWhName = tempSIO.Whname;
                sd.Bvencode = tempSIO.Vencode;
                sd.Venname = tempSIO.Venname;
                sd.Quantity = _qty;
                sd.Price = sd.Quantity * sd.Unitcost;
                sd.Taxprice = sd.Price * sd.Taxrate * Convert.ToDecimal(0.01);
                sd.Sum = sd.Price + sd.Taxprice;
                sd.Orimoney = sd.Quantity * sd.Oricost;
                sd.Oritaxprice = sd.Orimoney * sd.Taxrate * Convert.ToDecimal(0.01);
                sd.Orisum = sd.Orimoney + sd.Oritaxprice;
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
                    StockInDetail sd;
                    sd = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == cInvCode; });
                    if (sd != null && sd.bInvBatch)
                    {
                        cbxBatch.Visible = true;
                        cbxBatch.Enabled = true;
                        if (cbxBatch.DataSource != null && cbxBatch.Items.Count > 0)
                            return true;
                        List<string> batchList = new List<string>();
                        foreach (StockInDetail sid in tempSIO.U8Details)
                        {
                            if (sid.cInvCode.ToUpper() == cInvCode.ToUpper())
                            {
                                StockInDetail opare = null;
                                opare = tempSIO.OperateDetails.Find(delegate(StockInDetail odl) { return odl.cInvCode == sid.cInvCode && odl.Batch == sid.Batch; });
                                if (opare != null && opare.Quantity >= sid.fShallInQuan)
                                    continue;
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
                    cbxBatch.Visible = false;
                    cbxBatch.Enabled = false;
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
            sk = StockInBusiness.CreateSIOrderByDismantle("21", "", cCode, out ds);
            if (sk == null)
                return true;
            if (sk.U8Details == null || sk.U8Details.Count < 1)
                return true;
            if (sk.U8Details.Count != tempSIO.U8Details.Count)
                return true;
            StockInDetail opera = tempSIO.OperateDetails[0];
            StockInDetail sd = null;
            sd = sk.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == opera.cInvCode.ToUpper() && sdd.Batch.ToUpper() == opera.Batch.ToUpper(); });
            if (sd == null || sd.fShallInQuan != opera.fShallInQuan)
                return true;

            return false;
        }

        #endregion
    }
}