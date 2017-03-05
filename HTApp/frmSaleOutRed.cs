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
    public partial class frmSaleOutRed : Form
    {
        SaleOutRedList saleoutredlist;
        SaleOutRedDetail dd;
        /// <summary>
        /// 存货某一仓库下货位信息
        /// </summary>
        private List<Position> list = null;

        public frmSaleOutRed()
        {
            InitializeComponent();

            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblcBatch.Text = "";
            lblScanedNum.Text = "";

            try
            {
                this.cmbWarehouse.DataSource = Common.s_Warehouse;
                this.cmbWarehouse.ValueMember = "cwhcode";
                this.cmbWarehouse.DisplayMember = "cwhname";
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
        {
            get;
            set;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (saleoutredlist != null && saleoutredlist.OperateDetails.Count > 0)
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
            if (e.KeyChar == 13 && txtSource.Text.Length > 0)
            {
                string errMsg = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (SaleOutRedBusiness.VerifyGSPBack(txtSource.Text, out saleoutredlist, out errMsg))
                    {
                        btnSource.Enabled = true;
                        //txtCPosition.Enabled = true;
                        //txtCPosition.Focus();

                        ///判断当前仓库集合中是否有该单据的仓库，若没有则表明该用户无权操作。
                        string cwhcode = saleoutredlist.U8Details[0].cwhcode;
                        bool flag = false;
                        foreach (Warehouse w in Common.s_Warehouse)
                        {
                            if (w.cwhcode == cwhcode)
                            {
                                cmbWarehouse.SelectedItem = w;
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)//如果没有找到对应的仓库
                        {
                            MessageBox.Show("该单据您无权操作！");
                            return;
                        }

                        cmbWarehouse_SelectedIndexChanged(sender, e);
                        //检验单文本框不再可用
                        txtSource.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("检验单错误:" + errMsg);
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

        private void btnSource_Click(object sender, EventArgs e)
        {
            frmSaleOutRedSourceList f = new frmSaleOutRedSourceList(saleoutredlist.U8Details);
            f.ShowDialog();
            f.Dispose();
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

            //如果来源单据为空，则表示刚才初始化界面，所以来源单据获取当前焦点
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
            //货位
            string cposition = txtCPosition.Text.Trim().ToUpper();

            //如果是回车且长度大于0
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

        /// <summary>
        /// 扫描成品标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLable_KeyPress(object sender, KeyPressEventArgs e)
        {
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

                    //dd = saleoutredlist.U8Details.Find(delegate(SaleOutRedDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                    //if (dd == null)
                    //{
                    //    MessageBox.Show("条码错误:存货编码不在订单中");
                    //    txtLable.SelectAll();
                    //    txtLable.Focus();
                    //    txtCount.Enabled = false;
                    //    return;
                    //}
                    ///如果是二维码
                    if (IsQR)
                    {
                        //按存货编码和批次查询
                        dd = saleoutredlist.U8Details.Find(delegate(SaleOutRedDetail tdd) { return tdd.cinvcode.Equals(barcode[2]) && tdd.cbatch.Equals(barcode[3]); });
                       
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return; 
                        }
                        
                        lblInvName.Text = dd.cinvname;
                        lblInvStd.Text = dd.cinvstd;
                        lblProAddress.Text = dd.cdefine22;
                        lblEnterprise.Text = dd.cinvdefine1;
                        lblcBatch.Text = dd.cbatch;
                        lblScanedNum.Text = dd.inewquantity.ToString("F2");
                        lblProDate.Text = Convert.ToDateTime(dd.dmadedate).ToString("yyyy-MM-dd");//dd.dmadedate.Substring(0, 10);
                        lblValidDate.Text = Convert.ToDateTime(dd.dvdate).AddDays(-1).ToString("yyyy-MM-dd");// dd.dvdate.Substring(0, 10);
                        txtCount.Text = dd.iquantity.ToString("F3");
                        txtCount.Enabled = true;
                        txtCount.Focus();
                    
                    }
                    //如果是一维码
                    else
                    {
                        //只按存货编码查询
                        dd = saleoutredlist.U8Details.Find(delegate(SaleOutRedDetail tdd) { return tdd.cinvcode.Equals(barcode[2]); });
                        if (dd == null)
                        {
                            MessageBox.Show("条码错误:存货编码不在订单中");
                            return;
                        }

                        //然后查询该存货编码的所有批次绑定到cmbCbatch中
                        var v = from t in saleoutredlist.U8Details where t.cinvcode == dd.cinvcode select t;
                        cmbCBatch.DataSource = new BindingSource(v,null);
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
        /// 若为一维码，批次选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCBatch_SelectedIndexChangeed(object sender, EventArgs e)
        {
            //如果数据源为空直接返回
            if (cmbCBatch.DataSource == null)
                return;
            //获取选择的对象
            dd = cmbCBatch.SelectedItem as SaleOutRedDetail;
            lblInvName.Text = dd.cinvname;
            lblInvStd.Text = dd.cinvstd;
            lblProAddress.Text = dd.cdefine22;
            lblEnterprise.Text = dd.cinvdefine1;
            lblScanedNum.Text = dd.inewquantity.ToString("F2");
            //lblcBatch.Text = dd.cbatch;
            lblProDate.Text = Convert.ToDateTime(dd.dmadedate).ToString("yyyy-MM-dd");
            lblValidDate.Text = Convert.ToDateTime(dd.dvdate).AddDays(-1).ToString("yyyy-MM-dd");

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
                //仓库、货位
                string cwhcode = (cmbWarehouse.SelectedItem as Warehouse).cwhcode;
                string cposition = txtCPosition.Text.Trim().ToUpper();

                SaleOutRedDetail ddtmp = null;
                //首先判断是否有货位管理
                if (Bwhpos)
                {
                    //判断同货位、存货编码、批次的是否存在
                    ddtmp = saleoutredlist.OperateDetails.Find(delegate(SaleOutRedDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode) && tdd.cposition.Equals(cposition) && tdd.cbatch.Equals(dd.cbatch); });
                }
                else
                {
                    //判断存货编码、批次的是否存在
                    ddtmp = saleoutredlist.OperateDetails.Find(delegate(SaleOutRedDetail tdd) { return tdd.cinvcode.Equals(dd.cinvcode)  && tdd.cbatch.Equals(dd.cbatch); });
                }


                if (ddtmp == null)
                {
                    ddtmp = dd.CreateAttriveDetail();
                    ddtmp.cposition = cposition;

                    decimal qty = Convert.ToDecimal(txtCount.Text);
                    if (qty <= ddtmp.iquantity && qty <= dd.iquantity - dd.inewquantity)
                    {
                        ddtmp.inewquantity = qty;
                        dd.inewquantity += qty;//来源单据中的扫描数量

                        saleoutredlist.OperateDetails.Add(ddtmp);
                        btnDone.Enabled = true;
                        btnSubmit.Enabled = true;

                        Clear();
                        //重新刷新仓库
                        //cmbWarehouse_SelectedIndexChanged(sender, e);
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
                    if (qty <= ddtmp.iquantity && qty <= dd.iquantity - dd.inewquantity)//分多次扫描
                    {
                        ddtmp.inewquantity += qty;
                        dd.inewquantity += qty;

                        Clear();
                        //重新刷新仓库
                        //cmbWarehouse_SelectedIndexChanged(sender, e);
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            frmSaleOutRedList f = new frmSaleOutRedList(saleoutredlist);
            f.ShowDialog();
            f.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {            
            //判断已扫描数量是否完整
            //同一个存货同一个批次的数量必须相等
            //首先循环来源单据
            foreach (SaleOutRedDetail detail in saleoutredlist.U8Details)
            {
                decimal scannum = 0;
                //从已扫描数量中查找存货编码与批次相同的记录,最后判断数量是否相等
                var v = from od in saleoutredlist.OperateDetails where od.cinvcode == detail.cinvcode && od.cbatch == detail.cbatch select od;
                foreach (SaleOutRedDetail temp in v)
                {
                    //累加已扫描数量
                    scannum += temp.inewquantity;
                }
                if (scannum != detail.iquantity)
                {
                    MessageBox.Show(string.Format("货物:{0}的数量与单据数量不符!",detail.cinvname));
                    return;
                }
            }
            saleoutredlist.cmaker = Common.CurrentUser.UserName;
            saleoutredlist.cwhcode = cmbWarehouse.SelectedValue.ToString();
            //监管码
            saleoutredlist.cdefine10 = txtRegCode.Text.Trim();
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                int rt = SaleOutRedBusiness.SaveSaleOutRed(saleoutredlist, out errMsg);
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
        /// 输入数量后清空相应的数据
        /// </summary>
        private void Clear()
        {

            dd = null;
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblcBatch.Text = "";
            txtCount.Text = "";
            txtCount.Enabled = false;
            txtLable.Text = "";
            lblScanedNum.Text = "";
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

            //默认为二维码
            IsQR = true;
            //清空批次数据
            cmbCBatch.DataSource = null;
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