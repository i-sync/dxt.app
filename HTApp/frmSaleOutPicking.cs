using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
using U8Business;

namespace HTApp
{
    public partial class frmSaleOutPicking : Form
    {
        Model.DispatchList dispatchlist;
        Model.DispatchDetail dd;
        decimal favailqtty;

        /// <summary>
        /// 仓库货位信息
        /// </summary>
        List<Position> list;
        /// <summary>
        /// 货位下货物数量信息
        /// </summary>
        Position p;

        public frmSaleOutPicking()
        {
            InitializeComponent();

            try
            {
                //绑定仓库列表
                this.cmbWarehouse.DataSource = Common.s_Warehouse;
                this.cmbWarehouse.ValueMember = "cwhcode";
                this.cmbWarehouse.DisplayMember = "cwhname";
                

                lblInvName.Text = "";
                lblInvStd.Text = "";
                lblProAddress.Text = "";
                lblEnterprise.Text = "";
                lblProDate.Text = "";
                lblValidDate.Text = "";
                lblScanedNum.Text = "";

                txtSource.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 标识某一仓库是否货位管理
        /// </summary>
        public bool Bwhpos
        { get; set; }

        private void frmSaleOutPicking_Load(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// 点击退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (dispatchlist != null && dispatchlist.OperateDetails.Count > 0)
            {
                DialogResult dr = MessageBox.Show("确认要退出吗？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //选择取消退出，直接返回
                if (dr == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        /// <summary>
        /// 输入销售订单后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            //扫描销售订单号
            if (e.KeyChar == 13 && txtSource.Text.Length > 0)
            {
                string errMsg = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (DispatchListBusiness.VerifySO_SO(txtSource.Text, out dispatchlist, out errMsg))
                    {
                        btnSource.Enabled = true;
                        //txtLable.Enabled = true;
                        //txtLable.Focus();
                        //txtCPosition.Enabled = true;
                        //txtCPosition.Focus();
                        cmbWarehouse_SelectedIndexChanged(sender, e);

                        //销售订单文本框不再可用
                        txtSource.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("销售订单错误:" + errMsg);
                        btnSource.Enabled = false;
                        //txtLable.Enabled = false;
                        txtSource.SelectAll();
                        txtSource.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnSource.Enabled = false;
                    //txtLable.Enabled = false;
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
        /// 选择仓库改变事件
        /// 2012－10－17 tianzhenyun 修改
        /// 若该仓库有货位管理，则货位就可用，若该仓库没有货位管理，那么货位就不可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取仓库对像
            Warehouse wh = cmbWarehouse.SelectedItem as Warehouse;
            if (wh == null)
            {
                return;
            }
            if (wh.cwhcode.Equals("-1"))
            {
                cmbWarehouse.Focus();
                txtCPosition.Enabled = false;
                txtLable.Enabled = false;
                return;
            }
            if (wh.bwhpos == 1) //表示货位管理
            {
                Bwhpos = true;
                txtCPosition.Enabled = true;
                txtCPosition.Focus();
                txtLable.Enabled = false;

                ///获取该仓库下的货位信息
                list = null;
                string errMsg;
                Cursor.Current = Cursors.WaitCursor;
                bool flag = Common.GetPosition(wh.cwhcode, out list, out errMsg);
                Cursor.Current = Cursors.Default;
                if (!flag)
                {
                    MessageBox.Show("获取货位错误，" + errMsg);
                }
            }
            else //没有货位管理
            {
                Bwhpos = false;
                txtCPosition.Enabled = false;
                txtLable.Enabled = true;
                txtLable.Focus();
            }

            //如果销售订单为空，则表示刚才初始化界面，所以销售订单获取当前焦点
            if (string.IsNullOrEmpty(txtSource.Text))
            {
                txtSource.Focus();
            }
        }

        /// <summary>
        /// 输入货位回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cposition = txtCPosition.Text.Trim().ToUpper();
            if (e.KeyChar == (char)Keys.Enter && cposition.Length > 0)
            {
                //首先判断货位是否在该仓库下
                Position p = list.Find(delegate(Position temp) { return temp.cPosCode.Equals(cposition); });
                if (p == null)//没有找到货位信息
                {
                    MessageBox.Show("货位错误，该仓库下没有该货位！");
                    txtCPosition.SelectAll();
                    return;
                }

                txtLable.Enabled = true;
                txtLable.Focus();
            }
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
                    if (strBarcode.IndexOf('@') == -1)//没有找到@，说明该条码是一维条码
                    {
                        string errMsg = string.Empty;
                        string cInvCode= string.Empty;//存货编码
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(strBarcode, out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！"+errMsg);
                            return;
                        }
                        barcode[2] = cInvCode;
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
                    }

                    ///在来源单据中查看存货编码为CInvCode
                    dd = dispatchlist.U8Details.Find(delegate(Model.DispatchDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                    if (dd == null)
                    {
                        MessageBox.Show("条码错误:存货编码不在订单中");
                        txtLable.SelectAll();
                        txtLable.Focus();
                        txtCount.Enabled = false;
                    }
                    else
                    {
                        //首先判断是否有货位管理2012-10-17
                        if (Bwhpos)
                        {
                            //判断该货位下是否有此货物
                            p = list.Find(delegate(Position tp) { return tp.cPosCode.Equals(txtCPosition.Text.Trim().ToUpper()) && tp.cInvCode.Equals(barcode[2]); });
                            if (p == null || p.iQuantity <= 0 )
                            {
                                MessageBox.Show("此货位下没有该货物或货物数量为0");
                                return;
                            }
                        }

                        //绑定批次
                        GetBatchList( barcode[2],cmbWarehouse.SelectedValue.ToString(),txtCPosition.Text.Trim());

                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        //lblProAddress.Text = dd.cvenabbname; 2012-10-19改
                        lblProAddress.Text = dd.cdefine22;
                        lblEnterprise.Text = dd.cinvdefine1;
                        lblScanedNum.Text = dd.inewquantity.ToString("F3");
                        if (barcode[3].Equals("") || barcode[4].Equals("") || barcode[5].Equals(""))
                        {
                            lblProDate.Text = "";
                            lblValidDate.Text = "";
                            //txtcBatch.Enabled = true;
                            cmbBatch.Enabled = true;
                            txtCount.Enabled = true;
                            cmbBatch.Focus();
                            favailqtty = 0;
                        }
                        else
                        {
                            dd.invbatch = barcode[3];
                            bool flag = true;
                            //cmbBatch.SelectedValue = barcode[3];
                            foreach (object obj in cmbBatch.Items)
                            {
                                if ((obj as BatchInfo).Batch == barcode[3])
                                {
                                    cmbBatch.SelectedItem = obj;
                                    //如果找到对应的批次后，则cmbBatch设置为不可用 
                                    flag = false;
                                    break;
                                }
                            }
                            //如果没有找到对应的批次，说明该货位下没有此批次不能继续
                            if (flag)
                            {
                                MessageBox.Show("批次不正确,可能此货位下没有该批次！");
                                return;
                            }
                            //GetSTOutInvBatch(dd, cmbWarehouse.SelectedValue.ToString(), barcode[2], barcode[3]);
                            //txtcBatch.Enabled = true;
                            cmbBatch.Enabled = flag;
                            txtCount.Enabled = true;
                            txtCount.SelectAll();
                            txtCount.Focus();
                        }
                        cmbBatch_SelectedIndexChanged(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 根据仓库,存货编码获取批次信息
        /// </summary>
        /// <param name="cwhCode"></param>
        /// <param name="cInvCode"></param>
        private void GetBatchList(string cInvCode,string whCode,string cPosition)
        {
            //绑定之前先清空数据
            cmbBatch.DataSource = null;

            List<BatchInfo> list = DispatchListBusiness .GetBatchList(cInvCode, whCode,cPosition);
            cmbBatch.DataSource = list;
            cmbBatch.DisplayMember = "DisPlayMember";
            cmbBatch.ValueMember = "Batch";
        }

        /// <summary>
        /// 批次改变选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBatch_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (dd == null)
            {
                return;
            }
            //判断数据源是否为空
            if (cmbBatch.DataSource == null)
            {
                return;
            }
            //GetSTOutInvBatch(dd, this.cmbWarehouse.SelectedValue.ToString(), dd.cinvcode, (cmbBatch.SelectedItem as BatchInfo).Batch);

            //绑定数据显示
            BatchInfo bi = cmbBatch.SelectedItem as BatchInfo;
            //生产日期
            dd.dmdate = Convert.ToDateTime(bi.Mdate);
            lblProDate.Text = dd.dmdate.ToString("yyyy-MM-dd");
            //失效日期
            dd.dvdate = Convert.ToDateTime(bi.VDate);
            //有效期至
            dd.cexpirationdate = string.IsNullOrEmpty(bi.Expirationdate) ? dd.dvdate.AddDays(-1) : Convert.ToDateTime(bi.Expirationdate);
            lblValidDate.Text = dd.cexpirationdate.ToString("yyyy-MM-dd");
            //有效期计算项
            dd.dexpirationdate = string.IsNullOrEmpty(bi.Expirationdate) ? dd.dvdate.AddDays(-1) : Convert.ToDateTime(bi.Expirationdate);
            dd.cmassunit = bi.MassUnit;
            //结存数量
            favailqtty = bi.Quantity;
        }

        /// <summary>
        /// 批号,仓库，存货编码 获取生产日期失败日期信息等
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="cWhCode"></param>
        /// <param name="cInvCode"></param>
        /// <param name="cBatch"></param>
        public void GetSTOutInvBatch(DispatchDetail dd, string cWhCode, string cInvCode, string cBatch)
        {
            DataSet ds_batch = null;
            string errMsg = string.Empty;
            if (Common.getSTOutInvBatch(cWhCode, cInvCode, cBatch, out ds_batch, out errMsg) == 0)
            {
                try
                {
                    //生产日期
                    dd.dmdate = Convert.ToDateTime(ds_batch.Tables[0].Rows[0]["dMDate"]);
                    lblProDate.Text = dd.dmdate.ToString("yyyy-MM-dd");

                    //失效日期
                    dd.dvdate = Convert.ToDateTime(ds_batch.Tables[0].Rows[0]["dvdate"]);
                    //有效期至为失效日期－1
                    lblValidDate.Text = dd.dvdate.AddDays(-1).ToString("yyyy-MM-dd");
                    dd.cmassunit = Convert.ToInt32(ds_batch.Tables[0].Rows[0]["cmassunit"]);

                    dd.dexpirationdate = ds_batch.Tables[0].Rows[0]["dexpirationdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds_batch.Tables[0].Rows[0]["dexpirationdate"]);
                    dd.cexpirationdate = ds_batch.Tables[0].Rows[0]["cexpirationdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds_batch.Tables[0].Rows[0]["cexpirationdate"]);
                    favailqtty = Convert.ToDecimal(ds_batch.Tables[0].Rows[0]["fAvailQtty"]);

                    txtCount.SelectAll();
                    txtCount.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("批号错误："+errMsg);
            }
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            frmSaleOutSourceList f = new frmSaleOutSourceList(dispatchlist.U8Details);
            f.ShowDialog();
            f.Dispose();
        }

        /// <summary>
        /// 输入扫描数量后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCount.Text.Length > 0)
            {
                if (cmbBatch.SelectedItem == null)
                {
                    MessageBox.Show("请先确认批次！");
                    return;
                }
                //获取批次、货位、仓库编码
                string batch = (cmbBatch.SelectedItem as BatchInfo).Batch;
                string cposition = txtCPosition.Text.Trim().ToUpper();
                Warehouse wh = cmbWarehouse.SelectedItem as Warehouse;
                if (wh.cwhcode.Equals("-1"))
                {
                    MessageBox.Show("请重新选择仓库");
                    return;
                }
                string cwhcode = wh.cwhcode;
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
                if (string.IsNullOrEmpty(batch)) //txtcBatch.Text.Trim() == "")    // || lblProDate.Text == "" || lblValidDate.Text == "" )
                {
                    MessageBox.Show("请先确认批号");
                    cmbBatch.Focus();
                    return;
                }

                //如果有货位管理
                if (Bwhpos)
                {
                    Model.DispatchDetail ddtmp = dispatchlist.OperateDetails.Find(delegate(Model.DispatchDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode) && tdd.invbatch.Equals(batch) && tdd.cposition.Equals(cposition); });
                    if (ddtmp == null)
                    {
                        ddtmp = dd.CreateAttriveDetail();
                        //批次货位仓库编码
                        ddtmp.invbatch = batch;
                        ddtmp.cposition = cposition;
                        ddtmp.cwhcode = cwhcode;
                        decimal qty = Convert.ToDecimal(txtCount.Text);
                        if (qty <= ddtmp.iquantity - ddtmp.inewquantity - ddtmp.IFHQuantity && qty <= favailqtty )
                        {
                            ddtmp.inewquantity = qty;
                            dd.inewquantity += qty;
                            //ddtmp.cwhcode = this.cmbWarehouse.SelectedValue.ToString();
                            dispatchlist.OperateDetails.Add(ddtmp);
                            btnDone.Enabled = true;
                            btnSubmit.Enabled = true;
                            lblScanedNum.Text = dd.inewquantity.ToString("F2");
                            Clear();
                            //第一次提交成功后，仓库不可再选
                            if (cmbWarehouse.Enabled)
                            {
                                cmbWarehouse.Enabled = false;                            
                            }
                        }
                        else
                        {
                            MessageBox.Show("输入数量大于应发货数量或可发货数量,或大于此货位下该货物数量");
                            txtCount.SelectAll();
                            txtCount.Focus();
                        }
                    }
                    else
                    {
                        decimal qty = Convert.ToDecimal(txtCount.Text);

                        ///这里送去dd.inewquantity的数量：这个临时变量中已扫描数量不能表示该存货已扫描数量
                        ///而上面的是临时变量的已扫描数量，是因为临时变量是来源对象的拷贝与dd.inewquantity是一样的。
                        if (qty <= ddtmp.iquantity - dd.inewquantity - ddtmp.IFHQuantity && qty <= favailqtty - ddtmp.inewquantity )
                        {
                            ddtmp.inewquantity += qty;
                            dd.inewquantity += qty;
                            lblScanedNum.Text = dd.inewquantity.ToString("F2");
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("输入数量大于应发货数量或可发货数量,或大于此货位下该货物数量");
                            txtCount.SelectAll();
                            txtCount.Focus();
                        }
                    }
                }
                //没有货位管理
                //2012－10－17复制修改
                else 
                {
                    Model.DispatchDetail ddtmp = dispatchlist.OperateDetails.Find(delegate(Model.DispatchDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode) && tdd.invbatch.Equals(batch); });
                    if (ddtmp == null)
                    {
                        ddtmp = dd.CreateAttriveDetail();
                        //批次货位仓库编码
                        ddtmp.invbatch = batch;
                        //ddtmp.cposition = cposition;
                        ddtmp.cwhcode = cwhcode;
                        decimal qty = Convert.ToDecimal(txtCount.Text);
                        if (qty <= ddtmp.iquantity - ddtmp.inewquantity - ddtmp.IFHQuantity && qty <= favailqtty ) 
                        {
                            ddtmp.inewquantity = qty;
                            dd.inewquantity += qty;
                            //ddtmp.cwhcode = this.cmbWarehouse.SelectedValue.ToString();
                            dispatchlist.OperateDetails.Add(ddtmp);
                            btnDone.Enabled = true;
                            btnSubmit.Enabled = true;
                            lblScanedNum.Text = dd.inewquantity.ToString("F2");
                            Clear();
                            //第一次提交成功后，仓库不可再选
                            if (cmbWarehouse.Enabled)
                            {
                                cmbWarehouse.Enabled = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("输入数量大于应发货数量或可发货数量");
                            txtCount.SelectAll();
                            txtCount.Focus();
                        }
                    }
                    else
                    {
                        decimal qty = Convert.ToDecimal(txtCount.Text);
                        if (qty <= ddtmp.iquantity - dd.inewquantity - ddtmp.IFHQuantity && qty <= favailqtty - ddtmp.inewquantity )
                        {
                            ddtmp.inewquantity += qty;
                            dd.inewquantity += qty;
                            lblScanedNum.Text = dd.inewquantity.ToString("F2");
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
        }
        
        /// <summary>
        /// 提交按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ///首先判断销售订单中的数量是否扫描完全，如果没有扫描完全，提交时给出提示，
            ///让操作人员来判断是否继续提交。
            if (dispatchlist.OperateDetails == null || dispatchlist.OperateDetails.Count == 0)
            {
                return;            
            }
            bool flag = true;
            //存货没有扫描完全
            if (dispatchlist.U8Details.Count > dispatchlist.OperateDetails.Count)
            {
                flag = false;
            }
            //存货的数量扫描不完全
            else
            {
                //循环遍历所有的订单存货
                foreach (DispatchDetail detail in dispatchlist.U8Details)
                {
                    decimal num = 0;//某存货已扫描数量
                    //在已操作数量中查询来源中的存货（同一存货的扫描数量累加）
                    foreach (DispatchDetail oDetail in dispatchlist.OperateDetails)
                    {
                        if (detail.cinvcode == oDetail.cinvcode)
                        {
                            num += oDetail.inewquantity;
                        }
                    }
                    if (num == 0 || detail.iquantity -detail.IFHQuantity > num)
                    {
                        flag = false;
                        break;//只要有一个存货没有扫描完成，即可跳出
                    }
                }
            }
            if (!flag)//没有扫描完全
            {
                DialogResult dr = MessageBox.Show("还有存货没有扫描完成，是否提交", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)//取消提交
                {
                    return;
                }
            }

            //添加监管码
            dispatchlist.cdefine10 = txtRegCode.Text.Trim();

            dispatchlist.cmaker = Common.CurrentUser.UserName;
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                int rt = DispatchListBusiness.SaveDispatchList(dispatchlist, out errMsg);
                if (rt == 0)
                {
                    MessageBox.Show("提交成功！");
                    //默认为没有监管码
                    chkRegCode.Checked = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("提交失败！" + errMsg);
                }

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

        private void Clear()
        {
            dd = null;
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblScanedNum.Text = "";

            //清空批次
            cmbBatch.DataSource = null;
            cmbBatch.Enabled = false;

            txtCount.Text = "";
            txtCount.Enabled = false;

            txtLable.Text = "";
            txtCPosition.Text = "";
            
            //判断是否货位管理
            if (Bwhpos)
            {
                txtCPosition.Focus();
            }
            else
            {
                txtLable.Focus();
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            frmSaleOutList f = new frmSaleOutList(dispatchlist);
            f.ShowDialog();
            f.Dispose();
            if (dd != null)
            {
                lblScanedNum.Text = dd.inewquantity.ToString("F2");
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