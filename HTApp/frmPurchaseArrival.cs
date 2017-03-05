using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using U8Business;
using Model;

namespace HTApp
{
    public partial class frmPurchaseArrival : Form
    {
        /// <summary>
        /// 订单对象
        /// </summary>
        private ArrivalVouch arrivalVouch;
        /// <summary>
        /// 临时订单子表对象
        /// </summary>
        private ArrivalVouchs arrivalVouchs;
        public frmPurchaseArrival()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurchaseArrival_Load(object sender, EventArgs e)
        {
            //綁定仓库列表
            cmbWarehouse.DataSource = Common.s_Warehouse;
            cmbWarehouse.DisplayMember = "cwhname";
            cmbWarehouse.ValueMember = "cwhcode";

            Init();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void Init()
        {
            dtpChineseDate.Enabled = dtpProDate.Enabled = dtpValidDate.Enabled = chkChinese.Enabled = false;
            dtpChineseDate.Value = DateTime.Now;
            txtBatch.Enabled = txtCount.Enabled = false;
        }

        /// <summary>
        /// 循环扫描条码
        /// </summary>
        private void BarCodeLooper()
        {
            //清空文本
            txtBarcode.Text = lblcInvName.Text = lblAddress.Text = lblcInvStd.Text = txtBatch.Text = txtCount.Text = lblScanNum.Text = string.Empty;
            chkChinese.Checked = false;
            dtpProDate.Value = dtpValidDate.Value = dtpChineseDate.Value = DateTime.Now;
            dtpProDate.Enabled = dtpValidDate.Enabled = txtBatch.Enabled = txtCount.Enabled = false;

            txtBarcode.Focus();
        }

        /// <summary>
        /// 扫描订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cOrderCode = txtOrderCode.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(cOrderCode))
            {
                string errMsg;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    arrivalVouch = new U8Business.PurchaseArrivalBusiness().PO_POMian_Load(cOrderCode, out errMsg);
                    if (arrivalVouch == null)
                    {
                        MessageBox.Show(errMsg);
                        txtOrderCode.Focus();
                        txtOrderCode.SelectAll();
                        return;
                    }
                    //判断是否质检单
                    foreach (ArrivalVouchs avs in arrivalVouch.U8Details)
                    {
                        if (avs.bGsp)
                        {
                            rdoCheck.Checked = true;
                            break;
                        }
                    }
                    btnClear.Enabled = true;
                    cmbWarehouse.Enabled = true;
                    btnSource.Enabled = true;

                    txtOrderCode.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 仓库选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取仓库对像
            Warehouse wh = cmbWarehouse.SelectedItem as Warehouse;
            if (wh == null || wh.cwhcode.Equals("-1"))
            {
                txtBarcode.Enabled = false;
                return;
            }
            txtBarcode.Enabled = true;
            txtBarcode.Focus();
            txtBarcode.SelectAll();
        }

        /// <summary>
        /// 扫描条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strBarcode = txtBarcode.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(strBarcode))
            {
                try
                {
                    string[] barcode = new string[7] { "", "", "", "", "", "", "" };
                    string errMsg = string.Empty;
                    string cInvCode = string.Empty;//存货编码
                    bool IsQR = false;
                    if (strBarcode.IndexOf('@') == -1)//没有查到说明为一维条码
                    {
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(strBarcode, out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！" + errMsg);
                            return;
                        }
                        barcode[2] = cInvCode;
                    }
                    else
                    {
                        barcode = strBarcode.Split('@');
                        IsQR = true;
                        ///根据20121109日讨论结果：以69码为主，根据69码查询对应的存货编码
                        //根据一维条码查询存货编码
                        bool flag = Common.GetCInvCode(barcode[0], out cInvCode, out errMsg);
                        if (!flag)
                        {
                            MessageBox.Show("没有找到对应的存货编码！" + errMsg);
                            return;
                        }
                        barcode[2] = cInvCode;
                    }
                    //在来源单据中查找存货编号为cInvCode
                    arrivalVouchs = arrivalVouch.U8Details.Find(delegate(ArrivalVouchs avs) { return avs.cInvCode.Equals(barcode[2]); });
                    if (arrivalVouchs == null)//没有找到对象，说明不是该单据中的存货
                    {
                        MessageBox.Show("条码错误:存货编码不在订单中");
                        txtBarcode.Focus();
                        txtBarcode.SelectAll();
                        return;
                    }
                    //显示存货基本信息
                    lblcInvName.Text = arrivalVouchs.cInvName;
                    lblcInvStd.Text = arrivalVouchs.cInvStd;
                    lblAddress.Text = arrivalVouchs.Define22;//产地
                    chkChinese.Enabled = true;//中成药可选
                    lblScanNum.Text = arrivalVouchs.iScanQuantity.ToString("F2");//已扫数量
                    //如果是二维则批次及生产日期、有效期不可修改
                    if (IsQR)
                    {
                        //dtpProDate.Enabled = dtpValidDate.Enabled = true;
                        dtpProDate.Text = barcode[4];
                        dtpValidDate.Text = barcode[5];
                        txtBatch.Text = barcode[3];

                        //到数量
                        txtCount.Enabled = true;
                        txtCount.Focus();
                    }
                    else
                    {
                        //是否保质期管理
                        dtpProDate.Enabled = dtpValidDate.Enabled = arrivalVouchs.bInvQuality;
                        dtpProDate.Value = DateTime.Now;

                        //是否批次管理
                        txtBatch.Enabled = arrivalVouchs.bInvBatch;
                        txtCount.Enabled = !arrivalVouchs.bInvBatch;

                        //选择焦点
                        bool b = arrivalVouchs.bInvQuality ? dtpProDate.Focus() : arrivalVouchs.bInvBatch ? txtBatch.Focus() : txtCount.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        /// <summary>
        /// 中成药选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkChinese_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkChinese.Checked)
                dtpChineseDate.Enabled = true;
            else
                dtpChineseDate.Enabled = false;
        }

        /// <summary>
        /// 日期选择改变事件：自动更改生产日期或失效日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //首先判断改变的是生产日期还是有效期至
            DateTimePicker dtp = sender as DateTimePicker;
            if (dtp == null || arrivalVouchs == null)
                return;
            //判断是否保质期管理
            if (!arrivalVouchs.bInvQuality)
                return;
            if (dtp.Name == dtpProDate.Name)//修改的是生产日期
            {
                dtpValidDate.Value = dtp.Value.AddMonths(arrivalVouchs.iMassDate).AddDays(-1);
            }
            else//修改的是有效期至
            {
                dtpProDate.Value = dtp.Value.AddMonths(-1 * arrivalVouchs.iMassDate).AddDays(1);
            }
        }


        /// <summary>
        /// 录入批号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cBatch = txtBatch.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(cBatch))
            {
                txtBatch.Text = cBatch.ToUpper();

                //数量可用
                txtCount.Enabled = true;
                txtCount.Focus();
            }
        }

        /// <summary>
        /// 录入数量回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strCount = txtCount.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(strCount))
            {
                //判断数量是否正确
                Decimal qty = (Decimal)Cast.ToDouble(strCount);
                if (qty <= 0)
                {
                    MessageBox.Show("请输入正确的数量！");
                    txtCount.Focus();
                    txtCount.SelectAll();
                    return;
                }
                //仓库
                Warehouse wh = cmbWarehouse.SelectedItem as Warehouse;
                if (wh == null || wh.cwhcode.Equals("-1"))
                {
                    MessageBox.Show("请选择仓库");
                    return;
                }

                String cBatch = txtBatch.Text.Trim();
                //在已操作数据中查找
                ArrivalVouchs avs = null;
                //判断是否有批次管理：如果没有批次
                if (arrivalVouchs.bInvBatch)
                {
                    if (string.IsNullOrEmpty(cBatch))
                    {
                        MessageBox.Show("请输入批次！");
                        txtBatch.Focus();
                        txtBatch.SelectAll();
                        return;
                    }
                    //按存货批次查询
                    avs = arrivalVouch.OperateDetails.Find(delegate(ArrivalVouchs temp) { return temp.cInvCode.Equals(arrivalVouchs.cInvCode) && temp.cBatch.Equals(cBatch); });
                }
                else
                {
                    //只按存货查询
                    avs = arrivalVouch.OperateDetails.Find(delegate(ArrivalVouchs temp) { return temp.cInvCode.Equals(arrivalVouchs.cInvCode); });
                }

                if (avs == null)//说明还没有扫描该存货
                {
                    avs = arrivalVouchs.getNewDetail();
                    //判断数量是否正确
                    if (avs.Quantity - avs.iArrQty - avs.iScanQuantity < qty)
                    {
                        MessageBox.Show("录入数量大于订单数量！");
                        txtCount.Focus();
                        txtCount.SelectAll();
                        return;
                    }
                    if (chkChinese.Checked)
                        avs.Define23 = dtpChineseDate.Value.ToString("yyyy-MM-dd");//中成药
                    //判断是否保质期管理
                    if (avs.bInvQuality)
                    {
                        avs.dPDate = Cast.ToDateTime( dtpProDate.Value.ToShortDateString());//生产日期
                        avs.dVDate = Cast.ToDateTime( dtpValidDate.Value.AddDays(1).ToShortDateString());//失败日期
                        avs.dExpirationDate =Cast.ToDateTime( dtpValidDate.Value.ToShortDateString());//有效期计算项 
                        avs.cExpirationDate = dtpValidDate.Value.ToString("yyyy-MM-dd");//有效期至
                    }

                    avs.cBatch = cBatch;//批次
                    avs.cWhCode = wh.cwhcode;//仓库

                    if (!btnDone.Enabled)
                        btnDone.Enabled = btnSubmit.Enabled = true;

                    //数量
                    avs.iScanQuantity = qty;//扫描数量
                    arrivalVouchs.iScanQuantity += qty;//来源中的扫描数量
                    //是否质检
                    if (!avs.bGsp)
                    {
                        avs.fRealQuantity = qty;
                        avs.fValidQuantity = qty;
                    }

                    avs.iMoney = avs.iunitprice * qty;//原币无税金额
                    avs.iTax = (avs.iTaxPrice - avs.iunitprice) * qty;//原币税额
                    avs.iSum = avs.iTaxPrice * qty;//原币价税合计 

                    //添加到已操作
                    arrivalVouch.OperateDetails.Add(avs);
                }
                else
                {
                    //判断数量是否正确
                    if (avs.Quantity - avs.iArrQty - arrivalVouchs.iScanQuantity < qty)//可能分多个批次 所以 减去的是arrivalVouchs中的已扫数量
                    {
                        MessageBox.Show("录入数量大于订单数量！");
                        txtCount.Focus();
                        txtCount.SelectAll();
                        return;
                    }

                    //数量
                    avs.iScanQuantity += qty;
                    arrivalVouchs.iScanQuantity += qty;
                    if (!avs.bGsp)
                    {
                        avs.fRealQuantity += qty;
                        avs.fValidQuantity += qty;
                    }

                    avs.iMoney = avs.iunitprice * avs.iScanQuantity;//原币无税金额
                    avs.iTax = (avs.iTaxPrice - avs.iunitprice) * avs.iScanQuantity;//原币税额
                    avs.iSum = avs.iTaxPrice * avs.iScanQuantity;//原币价税合计 
                }

                arrivalVouchs = null;
                //循环扫描条码
                BarCodeLooper();
            }
        }

        /// <summary>
        /// 点击来源单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSource_Click(object sender, EventArgs e)
        {
            using (frmPurchaseArrivalSource form = new frmPurchaseArrivalSource(arrivalVouch.U8Details))
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 点击已操作单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            //判断已扫描中是否有数量
            if (arrivalVouch.OperateDetails == null || arrivalVouch.OperateDetails.Count == 0)
                return;
            using (frmPurchaseArrivalDone form = new frmPurchaseArrivalDone(arrivalVouch))
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 点击提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (arrivalVouch.OperateDetails == null || arrivalVouch.OperateDetails.Count == 0)
            {
                return;
            }
            bool flag = true;
            //存货没有扫描完全
            if (arrivalVouch.U8Details.Count > arrivalVouch.OperateDetails.Count)
            {
                flag = false;
            }
            //存货的数量扫描不完全
            else
            {
                //循环遍历所有的订单存货
                foreach (ArrivalVouchs avs in arrivalVouch.U8Details)
                {
                    decimal num = 0;//某存货已扫描数量
                    //在已操作数量中查询来源中的存货（同一存货的扫描数量累加）
                    foreach (ArrivalVouchs temp in arrivalVouch.OperateDetails)
                    {
                        if (avs.cInvCode == temp.cInvCode)
                        {
                            num += temp.iScanQuantity;
                        }
                    }
                    if (num == 0 || avs.Quantity - avs.iArrQty > num)
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

            arrivalVouch.cMaker = Common.CurrentUser.UserName;
            arrivalVouch.dDate = DateTime.Now.ToString("yyyy-MM-dd");
            arrivalVouch.cMakeTime = DateTime.Now.ToString();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string errMsg;
                flag = new PurchaseArrivalBusiness().PU_ArrivalVouch_Save(arrivalVouch, out errMsg);
                if (flag)
                {
                    MessageBox.Show("保存成功");
                    this.Close();
                }
                else
                    MessageBox.Show("保存失败：" + errMsg);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 点击退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //判断是否有已扫数据，如果有提示
            if (arrivalVouch != null && arrivalVouch.OperateDetails.Count > 0)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            arrivalVouchs = null;
            BarCodeLooper();
        }

    }
}