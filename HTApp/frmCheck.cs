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
    public partial class frmCheck : Form
    {
        checkvouch tempCV;
        bool isloaded = false;
        Warehouse wh;//选择的盘点单对应的仓库
        /// <summary>
        /// 仓库货位信息
        /// </summary>
        List<Position> list;

        public frmCheck()
        {
            InitializeComponent();
            try
            {
                //设置调拨单的下拉框
                List<string> whlist = null;
                List<string> orderlist = checkvouch.GetCheckVouchList(out whlist);
                if (orderlist == null)
                {
                    MessageBox.Show("盘点单未审核，无法进行其它入库操作!");
                    this.Close();
                }
                else
                {
                    this.cmbSourceNo.DataSource = orderlist;
                    this.cmbSourceNo.Text = "";
                    this.cmbSourceNo.SelectedIndex = -1;
                    this.lblWarehouse.Tag = whlist;
                }
                isloaded = true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        /// <summary>
        /// 判断是否为二维码
        /// </summary>
        public bool IsQR
        {
            get
            {
                //默认为二维条码，批次为不可用
                return !cmbCBatch.Enabled;
            }
            set
            {
                //批次选择是否可用
                cmbCBatch.Enabled = !value;
            }
        }

        //全局变量存储存货编码和批次
        private string cInvCode = string.Empty;
        private string cBatch = string.Empty;

        private void cmbSourceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && cmbSourceNo.Text.Length > 0)
            {
                for (int i = 0; i < cmbSourceNo.Items.Count; i++)
                {
                    if (cmbSourceNo.Text.Equals(cmbSourceNo.Items[i].ToString()))
                    {
                        cmbSourceNo.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void cmbSourceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isloaded && cmbSourceNo.SelectedIndex != -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (verifyCheck(cmbSourceNo.SelectedItem.ToString()))
                {
                    this.cmbSourceNo.Enabled = false;
                    cmbSourceNo.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbSourceNo.DropDownStyle = ComboBoxStyle.DropDown;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        #region 验证盘点单
        private bool verifyCheck(string CVcode)
        {
            try
            {
                tempCV = new checkvouch();
                //lblWarehouse.Text = ((List<string>)lblWarehouse.Tag)[cmbSourceNo.SelectedIndex];
                //获取盘点单对应仓库编码
                string cwhcode = ((List<string>)lblWarehouse.Tag)[cmbSourceNo.SelectedIndex];
                //验证该操作人员是否有权限操作此盘点单
                wh = Common.s_Warehouse.Find(delegate(Warehouse t) { return t.cwhcode == cwhcode; });
                if (wh == null)//该操作人员没有该仓库的操作权限
                {
                    MessageBox.Show("您无权操作该盘点单！");
                    return false;
                }
                lblWarehouse.Text = wh.cwhname;
                //判断货位是否可用
                if (wh.bwhpos == 1)
                {
                    //货位可用
                    txtCPosition.Enabled = true;
                    txtCPosition.Focus();

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
                else
                {
                    //标签可用
                    txtCodebar.Enabled = true;
                    txtCodebar.Focus();
                }
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                cmbSourceNo.Text = "";
                txtQuantity.Text = "";
                lblInvName.Text = "";
                cmbSourceNo.Focus();
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 输入货位后 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            //获取货位
            string cposition = txtCPosition.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(cposition) && e.KeyChar == (char)Keys.Enter)
            {
                //首先判断货位是否在该仓库下
                Position p = list.Find(delegate(Position temp) { return temp.cPosCode.Equals(cposition); });
                if (p == null)//没有找到货位信息
                {
                    MessageBox.Show("货位错误，该仓库下没有该货位！");
                    txtCPosition.SelectAll();
                    return;
                }

                txtCodebar.Enabled = true;
                txtCodebar.Focus();
            }
        }

        private void txtCodebar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCodebar.Text.Length > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                string cposition = txtCPosition.Text.Trim().ToUpper();

                //首先判断扫描的是一维条码还是二维条码，条件是否包含@
                string strBarcode = txtCodebar.Text.Trim();
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
                        Cursor.Current = Cursors.Default;
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

                /*if (verifyCheckDetail(barcode[2],barcode[3]))
                {
                    //如果验证通过则从全局变量中取值
                    lblcBatch.Text = cBatch;
                    CheckDetail detail = tempCV.CheckOperateDetail.Find(delegate(CheckDetail sid) { return sid.cinvcode.Equals(cInvCode) && sid.cbatch.Equals(cBatch); });
                    lblInvName.Text = detail.cinvname;
                    lblEnterprise.Text = detail.cinvdefine1;
                    lblQuantity.Text = detail.iCVQuantity.ToString();
                    txtCodebar.Tag = detail;
                    txtCodebar.Enabled = true;

                    txtQuantity.Focus();
                }
                else
                {
                    txtCodebar.Tag = null;
                    lblInvName.Text = "";
                    txtQuantity.Text = "";
                    lblQuantity.Text = "";
                    lblEnterprise.Text = "";
                }
                 * */
                if (!verifyCheckDetail(cposition,barcode[2],barcode[3]))
                {
                    ///验证失败
                    lblInvName.Text = "";
                    txtQuantity.Text = "";
                    lblProAddress.Text = "";
                    lblQuantity.Text = "";
                    lblEnterprise.Text = "";
                }
                Cursor.Current = Cursors.Default;
            }
        }

        #region 验证存货编码
        /// <summary>
        /// 根据货位、存货、批次进行验证存货
        /// </summary>
        /// <param name="cinvcode"></param>
        /// <param name="cbatch"></param>
        /// <returns></returns>
        private bool verifyCheckDetail(string cposition,string cinvcode, string cbatch)
        {
            try
            {
                List<CheckDetail> list;
                checkvouch.GetQtyByCode(cmbSourceNo.SelectedItem.ToString(), cinvcode, cposition, cbatch, out list);
                if (list == null||list.Count==0)
                {
                    MessageBox.Show("存货不存在或货位批次不对照");
                    return false;
                }
                cmbCBatch.DataSource = list;
                cmbCBatch.DisplayMember = "cbatch";
                //保存存货编码,批次
                this.cInvCode = cinvcode;
                this.cBatch = cbatch;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                this.cInvCode = string.Empty;
                this.cBatch = string.Empty;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 批次改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCBatch.DataSource == null)
            {
                return;
            }
            CheckDetail cb = cmbCBatch.SelectedItem as CheckDetail;
            //显示基本字段
            lblInvName.Text = cb.cinvname;
            lblProDate.Text = cb.dMadeDate.ToString("yyyy-MM-dd");
            lblValidDate.Text = cb.cExpirationdate.ToString("yyyy-MM-dd");
            lblProAddress.Text = cb.cinvdefine6;
            lblEnterprise.Text = cb.cinvdefine1;
            lblQuantity.Text = cb.iCVQuantity.ToString("F2");
            //tempCV.CheckOperateDetail.Add(cb);

            txtQuantity.Enabled = true;
            txtQuantity.Focus();
        }

        /// <summary>
        /// 录入盘点数量后回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //获取录入数量
            string strQty = txtQuantity.Text.Trim();
            if (!string.IsNullOrEmpty(strQty) && e.KeyChar == (char)Keys.Enter)
            {
                if (!isNumeric(strQty))
                {
                    MessageBox.Show("请输入数字！");
                    return;
                }
                //获取当前选择的盘点对象
                CheckDetail cd = cmbCBatch.SelectedItem as CheckDetail;
                if (cd == null)
                {
                    MessageBox.Show("获取盘点对象失败");
                    return;
                }

                decimal qty = Convert.ToDecimal(strQty);
                //判断是否货位管理：如果货位管理就添加货位条件，否则不加
                if (wh.bwhpos == 1)
                {
                    ///按存货编码、批次、货位进行查询
                    CheckDetail temp = tempCV.CheckOperateDetail.Find(delegate(CheckDetail c) { return c.cinvcode.Equals(cd.cinvcode) && c.cbatch.Equals(cd.cbatch) && c.cPosition.Equals(cd.cPosition); });
                    if (temp == null)//已扫描对象中还没有该对象
                    {
                        cd.iQuantity = qty;
                        tempCV.CheckOperateDetail.Add(cd);
                        this.btnSubmit.Enabled = true;
                        this.btnDetail.Enabled = true;
                    }
                    else
                    {
                        temp.iQuantity += qty;
                    }
                }
                else
                {
                    ///按存货编码、批次进行查询
                    CheckDetail temp = tempCV.CheckOperateDetail.Find(delegate(CheckDetail c) { return c.cinvcode.Equals(cd.cinvcode) && c.cbatch.Equals(cd.cbatch); });
                    if (temp == null)//已扫描对象中还没有该对象
                    {
                        cd.iQuantity = qty;
                        tempCV.CheckOperateDetail.Add(cd);
                        this.btnSubmit.Enabled = true;
                        this.btnDetail.Enabled = true;
                    }
                    else
                    {
                        temp.iQuantity += qty;
                    }
                }
                Clear();
            }
        }

        
        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            txtCPosition.Text = "";
            txtCodebar.Text = "";
            lblInvName.Text = "";
            lblProAddress.Text = "";
            lblEnterprise.Text = "";
            lblProDate.Text = "";
            lblValidDate.Text = "";
            lblQuantity.Text = "";
            txtQuantity.Text = "";
            txtQuantity.Enabled = false;
            //默认为二维条码
            IsQR = true;
            cmbCBatch.DataSource = null;
            //判断仓库是否有货位管理
            if (wh.bwhpos == 1)
            {
                txtCPosition.Focus();
                txtCodebar.Enabled = false;
            }
            else
            {
                txtCodebar.Focus();
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

        private void btnDetail_Click(object sender, EventArgs e)
        {
            frmCheckList f = new frmCheckList(tempCV.CheckOperateDetail);
            f.ShowDialog();
            f.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (tempCV != null && tempCV.CheckOperateDetail.Count > 0)
            {
                DialogResult dr = MessageBox.Show("有未提交的扫描数据，确定要退出吗？", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                    return;
            }
            Close();
        }

        /// <summary>
        /// 点击提交盘点单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tempCV == null || tempCV.CheckOperateDetail.Count < 1)
                return;
            this.Enabled = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tempCV.SubmitCheckVouchs(this.cmbSourceNo.SelectedItem.ToString());
                MessageBox.Show("提交成功");
                Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                this.Enabled = true;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}