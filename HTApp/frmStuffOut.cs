using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmStuffOut : Form
    {
        bool isloaded = false;
        StockIn tempSIO;//临时表头对象（多订单时用）
        List<Om_MoHeadInfo> MODetails;//绑定产品信息用
        List<StockInDetail> tempSDL;//产品明细临时列表
        
        decimal scanCount;//扫描计数
        bool is2Code;//是否二维码

        public frmStuffOut()
        {
            InitializeComponent();
        }

        private void frmStuffOut_Load(object sender, EventArgs e)
        {
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
                    tempSIO.SaveVouch = "04";

                    cbxMoDetails.DataSource = MODetails;
                    cbxMoDetails.DisplayMember = "DisplayMember";
                    cbxMoDetails.ValueMember = "MoDetailsID";

                    isloaded = true;
                    txtOrder.Enabled = false;
                    cbxMoDetails.Enabled = true;
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
                        cbxMoDetails.Enabled = false;
                        txtLabel.Enabled = false;
                        lblBatch.Enabled = true;
                        InputPannel.Enabled = true;
                        btnSubmit.Enabled = false;

                        txtUnitCost.Enabled = true;
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

        private void txtUnitCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                #region 数值格式
                if (string.IsNullOrEmpty(this.txtUnitCost.Text))
                {
                    MessageBox.Show("没有输入单价！");
                    return;
                }
                else if (!isNumeric(txtUnitCost.Text))
                {
                    MessageBox.Show("请输入正确的数字格式！");
                    return;
                }
                else if (decimal.Parse(txtUnitCost.Text) <= 0)
                {
                    MessageBox.Show("请输入正数！");
                    txtCount.Text = (decimal.Parse(txtUnitCost.Text) * -1).ToString();
                    return;
                }

                #endregion

                //StockInDetail sdl;
                //sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode == txtLabel.Text.Trim() && sdd.cMoDetailsID == cbxMoDetails.SelectedValue.ToString(); });
                //sdl.Unitcost = decimal.Parse(txtUnitCost.Text);

                txtUnitCost.Enabled = false;
                txtCount.Focus();
                txtCount.SelectAll();
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

        private void InputPannel_Click(object sender, EventArgs e)
        {
            InputPannel.Enabled = !InputPannel.Enabled;
            Clear();
            if (tempSIO != null && tempSIO.OperateDetails != null && tempSIO.OperateDetails.Count > 0)
                btnSubmit.Enabled = true;
            txtLabel.Focus();
            txtLabel.SelectAll();
        }

        private void cbxMoDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isloaded)
                return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!ReadMoDetails())
                {
                    return;
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

        private void cbxBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLabel.Text))
                return;

            if (sender != null)
            {
                lblBatch.Text = cbxBatch.Text;
            }

            ReadLabel();
        }

        private void cbxBatch_LostFocus(object sender, EventArgs e)
        {
            if (cbxBatch.DataSource == null || cbxBatch.Items.Count < 0 || cbxBatch.SelectedIndex < 0)
                return;
            lblBatch.Text = cbxBatch.SelectedValue.ToString();
            txtCount.Text = "1";
            txtCount.Focus();
            txtCount.SelectAll();
        }

        private void rbt_Click(object sender, EventArgs e)
        {
            txtOrder.Focus();
            txtOrder.SelectAll();
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
            }
            SetFocus();
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
                tempSIO.Rdcode = "209";
                tempSIO.Rdname = "委外发料";
                tempSIO.Bustype = "委外发料";
                tempSIO.nmaketime = DateTime.Now.ToString();
                tempSIO.nverifytime = tempSIO.nmaketime;
                tempSIO.Veridate = DateTime.Now.ToString();
                tempSIO.Maker = Common.CurrentUser.UserName;
                tempSIO.Source = "委外订单";
                tempSIO.Taxrate = tempSIO.U8Details[0].Taxrate;

                string tempMoDetailID = tempSIO.OperateDetails[0].cMoDetailsID;
                Om_MoHeadInfo ommo;
                ommo = MODetails.Find(delegate(Om_MoHeadInfo omhi) { return omhi.MoDetailsID != tempMoDetailID; });
                if (ommo == null)
                {
                    ommo = MODetails.Find(delegate(Om_MoHeadInfo omhi) { return omhi.MoDetailsID == tempMoDetailID; });
                    tempSIO.MQuantity = ommo.NQuantity;
                }

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
                    btnDone.Enabled = false;
                    btnSubmit.Enabled = false;
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
            if (txtPosition.Text.IndexOf('-') != -1 || txtPosition.Text.IndexOf('@') != -1)
                return false;
            StockInDetail position = null;
            position = tempSIO.OperateDetails.Find(delegate(StockInDetail sd) { return sd.Position.ToUpper() == txtPosition.Text; });
            if (position != null)
                return true;
            return StockInBusiness.GetPTExist(tempSIO.Whcode, txtPosition.Text);
        }

        private bool ReadMoDetails()
        {
            if (cbxMoDetails.DataSource == null || cbxMoDetails.Items.Count < 1 || string.IsNullOrEmpty(cbxMoDetails.SelectedValue.ToString()))
                return false;
            string strMoDID = cbxMoDetails.SelectedValue.ToString();
            if (tempSIO != null && tempSIO.U8Details != null && tempSIO.U8Details.Count > 0)
            {
                StockInDetail sdl;
                sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.Nquantity > 0 && sdd.cMoDetailsID == strMoDID; });
                if (sdl != null)
                    return true;
            }
            tempSDL = null;
            tempSDL = StockInBusiness.GetOMMOBody(strMoDID);
            if (tempSDL == null || tempSDL.Count < 1)
                return false;
            tempSIO.U8Details.Concat(tempSDL);

            return true;
        }

        private bool ReadLabel()
        {
            foreach (StockInDetail sid in tempSIO.U8Details)
            {
                if (sid.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sid.cMoDetailsID == cbxMoDetails.SelectedValue.ToString())
                {
                    if (!cbxBatch.Visible && sid.Batch.ToUpper() != lblBatch.Text.Trim().ToUpper())
                        continue;
                    if (!cbxBatch.Visible)
                    {
                        StockInDetail sdl;
                        sdl = tempSIO.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper(); });
                        if (sdl != null)
                            lblBatch.Text = sdl.Batch;
                        else
                        {
                            MessageBox.Show("不存在此存货!");
                            return false;
                        }
                    }
                    lblName.Text = sid.Invname;        //品名
                    lblStandard.Text = sid.cInvStd;     //规格 
                    lblAddrCode.Text = sid.Address;    //产地
                    decimal qty = 0;
                    decimal cost = 0;
                    foreach (StockInDetail sdd in tempSIO.OperateDetails)
                    {
                        if (sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Quantity > 0 && sdd.cMoDetailsID == cbxMoDetails.SelectedValue.ToString())
                        {
                            if (!cbxBatch.Visible && sdd.Batch.ToUpper() != lblBatch.Text.Trim().ToUpper())
                                continue;
                            qty += sdd.Quantity;
                            cost = sdd.Unitcost;
                        }
                    }
                    lblDoneNum.Text = qty.ToString("F4");
                    txtUnitCost.Text = cost.ToString("F2");

                    //生产日期，有效期，批次，从标签中拆分出来，加以显示
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
                    //ln.Write("qty:", _qty);
                    if (_qty == "")
                    {
                        MessageBox.Show("未输入数量！");
                        return false;
                    }
                    if (!isNumeric(_qty))
                    {
                        MessageBox.Show("请输入数字！");
                        return false;
                    }
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

                    if (sdl.bInvBatch && string.IsNullOrEmpty(lblBatch.Text))
                    {
                        MessageBox.Show("该货物有批次管理,请输入批次后重试！");
                        return false;
                    }

                    if (qty + inQty > sdl.Nquantity)
                    {
                        MessageBox.Show("扫描数量大于应出库数量！");
                        return false;
                    }

                    if (qty + inQty > StockInBusiness.GetPTQuan(txtLabel.Text, lblBatch.Text, tempSIO.Whcode, txtPosition.Text))
                    {
                        MessageBox.Show("扫描数量大于货位现有数量！");
                        return false;
                    }

                    StockInDetail tempSDL = sdl.getNewDetail();
                    if (!addData(qty, tempSDL))
                        return false;
                    AddPosition(qty);

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
                if (!ConvertValue())
                    return false;
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
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void Init()
        {
            tempSIO = new StockIn();
            tempSIO.U8Details = new List<StockInDetail>();
            tempSIO.OperateDetails = new List<StockInDetail>();
            tempSIO.OperaPositions = new List<InvPositionInfo>();

            MODetails = new List<Om_MoHeadInfo>();
            tempSDL = new List<StockInDetail>();

            cbxMoDetails.DataSource = null;
            cbxMoDetails.Items.Clear();
            cbxMoDetails.Enabled = false;

            InputPannel.Enabled = false;
            txtOrder.Enabled = true;
            txtPosition.Enabled = false;
            txtLabel.Enabled = false;
            btnSource.Enabled = false;
            btnDone.Enabled = false;
            btnSubmit.Enabled = false;

            txtOrder.Text = "";
            txtPosition.Text = "";

            SetFocus();
        }

        private void Clear()
        {
            cbxBatch.DataSource = null;
            cbxBatch.Items.Clear();
            cbxBatch.Refresh();

            cbxMoDetails.Enabled = true;
            txtPosition.Enabled = true;
            txtLabel.Enabled = false;
            lblBatch.Enabled = false;
            lblBatch.Visible = false;
            cbxBatch.Enabled = false;
            cbxBatch.Visible = false;
            txtUnitCost.Enabled = false;
            txtCount.Enabled = false;

            txtLabel.Text = "";
            lblBatch.Text = "";
            txtUnitCost.Text = "";
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

        #region 验证委外订单
        private bool verifyArrive(string cCode)
        {
            cCode = cCode.ToUpper();
            try
            {
                DataSet ds;
                tempSIO = StockInBusiness.GetOMMOHead(cCode, out MODetails, out ds);
                if (MODetails == null || MODetails.Count < 1)
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

                tempSIO.U8Details = StockInBusiness.GetOMMOBody(MODetails[0].MoDetailsID);
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
        private bool addData(decimal _qty, StockInDetail sd)
        {
            try
            {
                string strPostion = string.IsNullOrEmpty(txtPosition.Text) ? "" : txtPosition.Text.Trim().ToUpper();
                sd.Batch = lblBatch.Text.Trim().ToUpper();
                sd.Unitcost = decimal.Parse(txtUnitCost.Text);
                sd.Oricost = decimal.Parse(txtUnitCost.Text);
                if (tempSIO.OperateDetails.Count > 0)
                {
                    foreach (StockInDetail siodl in tempSIO.OperateDetails)
                    {
                        if (siodl.cInvCode.ToUpper() == sd.cInvCode.ToUpper() && siodl.VouchRowNo == sd.VouchRowNo && siodl.Batch == sd.Batch && siodl.Nquantity > 0)//看还要加什么条件？
                        {
                            if (siodl.Nquantity >= _qty + siodl.Quantity)
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

                Om_MoHeadInfo ommo;
                ommo = MODetails.Find(delegate(Om_MoHeadInfo omhi) { return omhi.MoDetailsID == cbxMoDetails.SelectedValue.ToString(); });
                if (ommo != null)
                    sd.InvCCode = ommo.InvCode;

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

        #region 设置产品索引

        private void SetMoDetails()
        {
            StockInDetail sdl;
            sdl = tempSIO.OperateDetails.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == txtLabel.Text.Trim().ToUpper() && sdd.Quantity > 0 && sdd.Quantity == sdd.Nquantity && sdd.cMoDetailsID == cbxMoDetails.SelectedValue.ToString(); });
            if (sdl == null)
                return;
            if (cbxMoDetails.SelectedIndex < cbxMoDetails.Items.Count)
                cbxMoDetails.SelectedIndex += 1;
            else
            {
                MessageBox.Show("已全部扫描完毕！");
                return;
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
                }
                //foreach (StockInDetail temp in tempSIO.U8Details)
                //{
                //    temp.Taxprice *= -1;
                //    temp.Price *= -1;
                //    temp.Aprice *= -1;
                //    temp.Money *= -1;
                //    temp.Orimoney *= -1;
                //    temp.Orisum *= -1;
                //    temp.Oritaxprice *= -1;
                //    temp.Nquantity *= -1;
                //    temp.Quantity *= -1;
                //}

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
                    if (!Common.GetCInvCode(strBarCode, out cInvCode, out errMsg))
                    {
                        MessageBox.Show("没有找到对应的存货编码！" );
                        return false;
                    }
                    barcode[2] = cInvCode;
                    cbxBatch.Visible = true;
                    cbxBatch.Enabled = true;
                    List<string> batchList = new List<string>();
                    foreach (StockInDetail sid in tempSIO.U8Details)
                    {
                        if (sid.cInvCode.ToUpper() == cInvCode.ToUpper())
                        {
                            StockInDetail opare = null;
                            opare = tempSIO.OperateDetails.Find(delegate(StockInDetail odl) { return odl.cInvCode == sid.cInvCode && odl.Batch == sid.Batch; });
                            if (opare != null && opare.Quantity >= opare.Nquantity)
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
                    txtLabel.Text = barcode[2];
                    if (cbxBatch.DataSource != null && cbxBatch.Items.Count > 0)
                        lblBatch.Text = cbxBatch.SelectedValue.ToString();
                }
                else
                {   //二维码
                    is2Code = true;
                    barcode = strBarCode.Split('@');
                    cbxBatch.Visible = false;
                    cbxBatch.Enabled = false;
                    lblBatch.Enabled = true;
                    lblBatch.Visible = true;
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
            sk = StockInBusiness.GetOMMOHead(cCode, out MODetails, out ds);
            if (sk.U8Details == null || sk.U8Details.Count < 1)
                return true;
            if (sk.U8Details.Count != tempSIO.U8Details.Count)
                return true;
            StockInDetail opera = tempSIO.OperateDetails[0];
            StockInDetail sd = null;
            sd = sk.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == opera.cInvCode.ToUpper() && sdd.Batch.ToUpper() == opera.Batch.ToUpper(); });
            if (sd == null || sd.Nquantity != opera.Nquantity)
                return true;

            return false;
        }

        #endregion

    }
}