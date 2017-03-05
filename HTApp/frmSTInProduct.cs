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
    public partial class frmSTInProduct : Form
    {
        STInProduct stin;
        STInProductDetail dd;
        /// <summary>
        /// 存货某一仓库下货位信息
        /// </summary>
        private List<Position> list = null;

        public frmSTInProduct()
        {
            InitializeComponent();

            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblEnterprise.Text = "";
            lblScanedNum.Text = "";
            lblPrice.Text = "";

            stin = new STInProduct();
            try
            {
                //绑定仓库列表
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
                txtBarcode.Enabled = false;
                return;
            }
            if (wh.bwhpos == 1) //表示货位管理
            {
                Bwhpos = true;
                txtCPosition.Enabled = true;
                txtCPosition.Focus();
                txtBarcode.Enabled = false;

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
                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            //如果流通监管码为空，则表示刚才初始化界面，所以流通监管码获取当前焦点
            if (string.IsNullOrEmpty(txtRegCode.Text))
            {
                txtRegCode.Focus();
            }
        }


        /// <summary>
        /// 输入流通监管码回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Bwhpos)
                {
                    txtCPosition.Focus();
                }
                else
                {
                    txtBarcode.Focus();
                }
            }
        }

        /// <summary>
        /// 判断是否为二维条码
        /// </summary>
        public bool IsQR
        {
            set 
            {
                if (value)
                {
                    dtpProDate.Enabled = false;
                    dtpValidDate.Enabled = false;
                    txtCBatch.Enabled = false;
                    txtCBatch.Text = string.Empty;
                }
                else
                {
                    dtpProDate.Enabled = true;
                    dtpValidDate.Enabled = true;
                    txtCBatch.Enabled = true;
                }
            }
            get
            {
                return !txtCBatch.Enabled;
            }
        }

        /// <summary>
        /// 生产编码
        /// </summary>
        private string cInvCode = string.Empty;

        //保质期天数与保质期单位
        private int iMassDate;
        private int cMassUnit;

        /// <summary>
        /// 输入货位后回车
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

                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
        }
        
        /// <summary>
        /// 扫描条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //成品标签扫描
            if (e.KeyChar == 13 && txtBarcode.Text.Length > 0)
            {
                try
                {
                    string errMsg = null;

                    //首先判断扫描的是一维条码还是二维条码，条件是否包含@
                    string strBarcode = txtBarcode.Text.Trim();
                    string[] barcode = new string[7] {"", "", "", "", "", "", "" };
                    if (strBarcode.IndexOf('@') == -1)//没有找到@，说明该条码是一维条码
                    {
                        string cInvCode = string.Empty;//存货编码
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(strBarcode, out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！" + errMsg);
                            return;
                        }
                        barcode[1] = cInvCode;
                        IsQR = false;
                    }
                    else //二维码
                    {
                        barcode = strBarcode.Split('@');
                        IsQR = true;
                    }
                    this.cInvCode = barcode[1];

                    if (STInProductBusiness.GetSTInProduct(barcode[1], out dd, out errMsg))
                    {
                        if (dd == null)
                        {
                            MessageBox.Show("获取条码错误");
                            txtCost.Enabled = false;
                            txtBarcode.SelectAll();
                            txtBarcode.Focus();
                            txtCost.Text = "";
                            txtCount.Text = "";
                        }
                        else
                        {
                            lblInvName.Text = dd.cinvname;
                            lblInvStd.Text = dd.cinvstd;
                            lblEnterprise.Text = dd.cinvdefine1;

                            //保质期天数与保质期单位
                            iMassDate = dd.imassdate;
                            cMassUnit = dd.cmassunit;

                            if (IsQR)//如果扫描的是二维条码
                            {
                                dd.cbatch = barcode[3];
                                dd.dmadedate = barcode[4];
                                dd.dvdate = Convert.ToDateTime(barcode[5]).AddDays(1).ToString("yyyy-MM-dd");
                                txtCBatch.Text = dd.cbatch;
                                dtpProDate.Value = Convert.ToDateTime(dd.dmadedate);
                                dtpValidDate.Value = Convert.ToDateTime(dd.dvdate).AddDays(-1);

                                VerifyPCB(txtCPosition.Text, barcode[1], dd.cbatch);
                            }
                            else //一维条码
                            {
                                ////触发日期的计算，生产日期默认为今天
                                dtpProDate_ValueChanged(dtpProDate, e);
                                txtCBatch.Focus();
                                //dtpProDate.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("条码解析失败");
                        txtCost.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    txtCost.Enabled = false;
                }
            }
        }


        /// <summary>
        /// 生产日期改变事件：根据保质期天数自动更新生产日期或有效期至
        /// </summary>
        /// <param name="sender">生产日期或有效期至</param>
        /// <param name="e"></param>
        private void dtpProDate_ValueChanged(object sender, EventArgs e)
        {
            //首先判断改变的是生产日期还是有效期至
            DateTimePicker dtp = sender as DateTimePicker;
            if (dtp == null)
                return;
            if (dtp.Name == dtpProDate.Name)//修改的是生产日期
            {
                dtpValidDate.Value = dtp.Value.AddMonths(iMassDate).AddDays(-1);
            }
            else//修改的是有效期至
            {
                dtpProDate.Value = dtp.Value.AddMonths(-1 * iMassDate).AddDays(1);
            }
        }

        /// <summary>
        /// 输入批次号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //批号
            string cbatch = txtCBatch.Text.Trim().ToUpper();
            //如果是回车且长度大于0
            if (e.KeyChar == (char)Keys.Enter && cbatch.Length > 0)
            {
                VerifyPCB(txtCPosition.Text.Trim().ToUpper(), cInvCode, cbatch);
            }
        }

        /// <summary>
        /// 验证货位、存货编码、批次是否已经存在
        /// </summary>
        /// <param name="cposition"></param>
        /// <param name="cinvcode"></param>
        /// <param name="cbatch"></param>
        private void VerifyPCB(string cposition, string cinvcode, string cbatch)
        {
            dd.cbatch = cbatch;
            dd.cwhcode = (cmbWarehouse.SelectedItem as Warehouse).cwhcode;
            dd.cposition = cposition;
            dd.dmadedate = dtpProDate.Value.ToString("yyyy-MM-dd");
            dd.dvdate = dtpValidDate.Value.AddDays(1).ToString("yyyy-MM-dd");
            dd.cExpirationdate = dtpValidDate.Value.ToString("yyyy-MM-dd");

            STInProductDetail stemp = null;
            //首先判断是否有货位管理
            if (Bwhpos)
            {
                //判断同货位、存货编码、批次的是否存在
                stemp = stin.U8Details.Find(delegate(STInProductDetail tdd) { return tdd.cinvcode.Equals(cinvcode) && tdd.cbatch.Equals(cbatch) && tdd.cposition.Equals(cposition); });
            }
            else
            {
                //判断存货编码、批次的是否存在
                stemp = stin.U8Details.Find(delegate(STInProductDetail tdd) { return tdd.cinvcode.Equals(cinvcode) && tdd.cbatch.Equals(cbatch) ; });
            }

            if (stemp == null)
            {
                stin.U8Details.Add(dd);
            }
            else
            {
                dd = stemp;
                lblScanedNum.Text = stemp.iquantity.ToString("F2");
                txtCost.Text = stemp.iunitcost.ToString("F2");
                lblPrice.Text = stemp.iprice.ToString("F2");
            }
            //检验单号
            txtCChkCode.Enabled = true;
            txtCChkCode.Focus();
        }

        /// <summary>
        /// 输入检验单号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCChkCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtCChkCode.Text.Trim().Length > 0)
            {
                txtCost.Enabled = true;
                txtCost.Focus();
            }
        }

        /// <summary>
        /// 输入单价回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtCost.Text.Length > 0)
            {
                if (txtCost.Text.Trim() == "")
                {
                    MessageBox.Show("未输入单价！");
                    txtCost.SelectAll();
                    txtCost.Focus();
                    return;
                }
                if (!isNumeric(txtCost.Text))
                {
                    MessageBox.Show("请输入数字！");
                    txtCost.SelectAll();
                    txtCost.Focus();
                    return;
                }
                decimal cost = Convert.ToDecimal(txtCost.Text);
                dd.iunitcost = cost;
                dd.iprice = cost * dd.iquantity;
                lblPrice.Text = dd.iprice.ToString("F2");
                txtCount.Enabled = true;
                txtCount.Focus();
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
                //检验单号
                dd.cCheckCode = txtCChkCode.Text.Trim();

                decimal count = Convert.ToDecimal(txtCount.Text);
                dd.iquantity += count;
                dd.iprice = dd.iquantity * dd.iunitcost;
                lblPrice.Text = dd.iprice.ToString("F2");
                lblScanedNum.Text = dd.iquantity.ToString("F2");
                btnDone.Enabled = true;
                btnSubmit.Enabled = true;
                if (!stin.OperateDetails.Contains(dd))
                {
                    stin.OperateDetails.Add(dd);
                }

                Clear();
                //第一次提交成功后，仓库不可再选
                if (cmbWarehouse.Enabled)
                {
                    cmbWarehouse.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 输入数量后清空数据
        /// </summary>
        private void Clear()
        {
            dd = null;
            lblInvName.Text = "";
            lblInvStd.Text = "";
            lblEnterprise.Text = "";
            iMassDate = 0;
            cMassUnit = 0;
            IsQR = true;
            lblScanedNum.Text = "";
            lblPrice.Text = "";

            txtCPosition.Text = "";
            txtBarcode.Text = "";
            txtCChkCode.Enabled = false;
            txtCChkCode.Text = "";
            txtCost.Text = "";
            txtCost.Enabled = false;
            txtCount.Text = "";
            txtCount.Enabled = false;

            //判断是否货位管理
            if (Bwhpos)
            {
                txtCPosition.Focus();
            }
            else
            {
                txtBarcode.Focus();
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            frmSTInPorductList f = new frmSTInPorductList(stin);
            f.ShowDialog();
            f.Dispose();
            if (dd != null)
            {
                lblScanedNum.Text = dd.iquantity.ToString("F2");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ///退出前首先判断是否已经有扫描的存货若有则提示确认退出
            if (stin != null && stin.OperateDetails.Count > 0)
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            stin.cmaker = Common.CurrentUser.UserName;
            stin.cdefine10 = txtRegCode.Text;
            stin.cwhcode = cmbWarehouse.SelectedValue.ToString();
            try
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                string errMsg = "";
                int rt = STInProductBusiness.SaveProductIn(stin, out errMsg);
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
            Cursor.Current = Cursors.WaitCursor;
            Model.Regulatory data = U8Business.Regulatory.GetModel(out errMsg);
            Cursor.Current = Cursors.Default;
            if (data == null)
            {
                MessageBox.Show(errMsg);
                chkRegCode.Checked = false;
                return;
            }
            txtRegCode.Text = data.RegCode;
        }

    }
}