using System;
using System.Collections.Generic;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmDone : Form
    {
        int doneType;   //已扫描数据类型
        StockIn stock;    //已扫描入库数据
        ArrivalVouch arrival;    //已扫描到货数据

        /// <summary>
        /// 已扫描数据显示
        /// </summary>
        /// <param name="obj">已扫描数据</param>
        public frmDone(object obj)
        {
            InitializeComponent();

            if (obj.GetType().Equals(typeof(StockIn)))
            {
                stock = obj as StockIn;
                StockDone();
            }

            else if (obj.GetType().Equals(typeof(ArrivalVouch)))
            {
                arrival = obj as ArrivalVouch;
                ArrivalDone();
            }

            else
            {
                Close();
            }
        }

        private void frmDone_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
            BindData();
        }

        private void dgDone_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (doneType == 1)
                {
                    if (stock.OperateDetails == null || stock.OperateDetails.Count < 1)
                    {
                        return;
                    }
                    int index = dgDone.CurrentRowIndex;
                    if (doneType == 1 && index >= 0 && index < stock.OperateDetails.Count && stock.OperateDetails[index].IsPos)
                        btnPosition.Visible = true;
                    else
                        btnPosition.Visible = false;
                }
                else if (doneType == 2)
                {
                    if (arrival.OperateDetails == null || arrival.OperateDetails.Count < 1)
                    {
                        return;
                    }
                }
            }
            catch { return; }
        }
        /// <summary>
        /// 删除选定行
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgDone.CurrentRowIndex;
                if (index < 0)
                {
                    MessageBox.Show("没有选择操作的数据！");
                    return;
                }
                if (doneType == 1)
                {
                    DialogResult dr = MessageBox.Show("确定要删除" + stock.OperateDetails[index].Invname + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes)
                    {
                        string cInvCode = stock.OperateDetails[index].cInvCode;
                        string cBatch = stock.OperateDetails[index].Batch;
                        string cMoDetailsID = stock.OperateDetails[index].cMoDetailsID;
                        decimal quan = stock.OperateDetails[index].Quantity;
                        StockInDetail sid;
                        if (stock.SaveVouch == "04")
                            sid = stock.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == cInvCode && sdd.cMoDetailsID == cMoDetailsID; });
                        else
                            sid = stock.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == cInvCode && sdd.Batch.ToUpper() == cBatch; });
                        DelPosition(cInvCode, cBatch);
                        stock.OperateDetails.RemoveAt(index);
                        sid.Nquantity += quan;
                        dgDone.DataSource = null;
                        dgDone.Refresh();
                        BindData();
                    }
                    if (stock.OperateDetails == null || stock.OperateDetails.Count < 1)
                    {
                        MessageBox.Show("暂无已扫描的数据！");
                        btnDelete.Enabled = false;
                        btnPosition.Visible = false;
                        return;
                    }
                }
                else if (doneType == 2)
                {
                    DialogResult dr = MessageBox.Show("确定要删除" + arrival.OperateDetails[index].cInvName + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes)
                    {
                        dgDone.DataSource = null;
                        dgDone.Refresh();
                        arrival.OperateDetails.RemoveAt(index);
                        BindData();
                    }
                    if (arrival.OperateDetails == null || arrival.OperateDetails.Count < 1)
                    {
                        MessageBox.Show("暂无已扫描的数据！");
                        btnDelete.Enabled = false;
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("操作失误,请重试!");
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            if (doneType != 1 || stock.OperaPositions == null || stock.OperaPositions.Count < 1)
                return;
            StockInDetail sdl = stock.OperateDetails[dgDone.CurrentRowIndex];
            List<InvPositionInfo> tempPos = GetTempPosList(sdl);
            if (tempPos == null || tempPos.Count < 1)
                return;

            using (frmPosition frmPos = new frmPosition(tempPos))
            {
                decimal inQty = sdl.Quantity;
                int count = tempPos.Count;
                frmPos.ShowDialog();
                if (count > tempPos.Count)
                {
                    StockInDetail sid;
                    if (stock.SaveVouch == "04")
                        sid = stock.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == sdl.cInvCode && sdd.cMoDetailsID == sdl.cMoDetailsID; });
                    else
                        sid = stock.U8Details.Find(delegate(StockInDetail sdd) { return sdd.cInvCode.ToUpper() == sdl.cInvCode && sdd.Batch.ToUpper() == sdl.Batch; });
                    DelPosition(sdl.cInvCode, sdl.Batch);
                    if (tempPos == null || tempPos.Count < 1)
                    {
                        stock.OperateDetails.Remove(sdl);
                        sid.Nquantity += inQty;
                    }
                    else
                    {
                        decimal quan = 0;
                        foreach (InvPositionInfo pos in tempPos)
                        {
                            quan += pos.Quantity;
                        }
                        sdl.Quantity = quan;
                        if (tempPos.Count == 1)
                            sdl.Position = tempPos[0].PosCode;
                        stock.OperaPositions.AddRange(tempPos);
                        sid.Nquantity += (inQty - quan);
                    }
                }
            }

            BindData();
        }

        #region Function

        #region StockDone

        /// <summary>
        /// 初始化已扫描入库数据表头
        /// </summary>
        private void StockDone()
        {
            doneType = 1;

            DataGridTableStyle dts = new DataGridTableStyle();

            #region DataGridTextBoxColumn
            string cIsOut = stock.IsOut ? "出库" : "入库";
            string cVouch = stock.SaveVouch;

            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = cIsOut + "仓库";
            dtbc.MappingName = "cWhName";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            if (cVouch[0] != 't')
            {
                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "供应商简称";
                dtbc.MappingName = "VenAbbName";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgDone.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "供应商全称";
                dtbc.MappingName = "Venname";
                dtbc.Width = 200;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgDone.TableStyles.Add(dts);
            }

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cInvCode";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "Invname";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格型号";
            dtbc.MappingName = "cInvStd";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "主计量";
            dtbc.MappingName = "Invm_unit";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = cIsOut + "数量";
            dtbc.MappingName = "Quantity";
            dtbc.Width = 100;
            dtbc.Format = "F4";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            if (cVouch[0] != 't')
            {
                if (cVouch != "04")
                {
                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "原币含税单价";
                    dtbc.MappingName = "Oritaxcost";
                    dtbc.Width = 100;
                    dtbc.Format = "F2";
                    dts.GridColumnStyles.Add(dtbc);
                    dgDone.TableStyles.Add(dts);

                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "原币价税合计";
                    dtbc.MappingName = "Orisum";
                    dtbc.Width = 100;
                    dtbc.Format = "F2";
                    dts.GridColumnStyles.Add(dtbc);
                    dgDone.TableStyles.Add(dts);
                }

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "本币无税金额";
                dtbc.MappingName = "Price";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgDone.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "税率";
                dtbc.MappingName = "Taxrate";
                dtbc.Width = 50;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgDone.TableStyles.Add(dts);
            }

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "Batch";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "Address";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "Madedate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "保质期";
            dtbc.MappingName = "Massdate";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "cExpirationDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "Vdate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);
            #endregion

            dts.MappingName = stock.OperateDetails.GetType().Name;
        }
        #endregion

        #region ArrivalDone

        /// <summary>
        /// 初始化已扫描到货数据表头
        /// </summary>
        private void ArrivalDone()
        {
            doneType = 2;

            DataGridTableStyle dts = new DataGridTableStyle();

            #region DataGridTextBoxColumn
            string cIsOut = arrival.bIsOut ? "出货" : "到货";

            DataGridTextBoxColumn dtbc;

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = cIsOut + "仓库";
            dtbc.MappingName = "cWhName";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "供应商简称";
            dtbc.MappingName = "cVenAbbName";
            dtbc.Width = 150;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            //dtbc = new DataGridTextBoxColumn();
            //dtbc.HeaderText = "供应商全称";
            //dtbc.MappingName = "cVenName";
            //dtbc.Width = 200;
            //dtbc.Format = "G";
            //dts.GridColumnStyles.Add(dtbc);
            //dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cInvCode";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cInvName";
            dtbc.Width = 120;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格型号";
            dtbc.MappingName = "cInvStd";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "主计量";
            dtbc.MappingName = "cInvm_Unit";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = cIsOut + "数量";
            dtbc.MappingName = "Quantity";
            dtbc.Width = 100;
            dtbc.Format = "F4";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "含税单价";
            dtbc.MappingName = "iOriTaxCost";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "价税合计";
            dtbc.MappingName = "ioriSum";
            dtbc.Width = 100;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "税率";
            dtbc.MappingName = "iTaxRate";
            dtbc.Width = 50;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "cBatch";
            dtbc.Width = 100;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dPDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "保质期";
            dtbc.MappingName = "iMassDate";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "cExpirationDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "dVDate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "cAddress";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "是否检验";
            dtbc.MappingName = "cGsp";
            dtbc.Width = 50;
            dtbc.Format = "G";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "订单编号";
            dtbc.MappingName = "cOrderCode";
            dtbc.Width = 100;
            dtbc.Format = "D";
            dts.GridColumnStyles.Add(dtbc);
            dgDone.TableStyles.Add(dts);
            #endregion

            dts.MappingName = arrival.OperateDetails.GetType().Name;
        }
        #endregion

        #region BindData

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            try
            {
                if (doneType == 1)
                {
                    if (stock.OperateDetails == null || stock.OperateDetails.Count < 1)
                    {
                        dgDone.DataSource = null;
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        dgDone.DataSource = stock.OperateDetails;
                        dgDone.CurrentRowIndex = 0;
                        btnDelete.Enabled = true;
                        if (stock.OperateDetails[0].IsPos)
                            btnPosition.Visible = true;
                        else
                            btnPosition.Visible = false;
                    }
                    dgDone.Refresh();
                }
                else if (doneType == 2)
                {
                    if (arrival.OperateDetails == null || arrival.OperateDetails.Count < 1)
                    {
                        dgDone.DataSource = null;
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        dgDone.DataSource = arrival.OperateDetails;
                        dgDone.CurrentRowIndex = 0;
                        btnDelete.Enabled = true;
                    }
                    dgDone.Refresh();
                }
            }
            catch
            {
                MessageBox.Show("操作失误,请重试!");
                return;
            }
        }
        #endregion

        #region DelPositon
        /// <summary>
        /// 删除货位
        /// </summary>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cBatch">批号</param>
        /// <returns>是否删除成功</returns>
        private bool DelPosition(string cInvCode,string cBatch)
        {
            try
            {
                for (int i = 0; i < stock.OperaPositions.Count; i++)
                {
                    if (stock.OperaPositions[i].InvCode == cInvCode && stock.OperaPositions[i].Batch == cBatch)
                    {
                        stock.OperaPositions.RemoveAt(i);
                        i--;
                    }
                }
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region GetTempPosList
        /// <summary>
        /// 获取对应货位列表
        /// </summary>
        /// <param name="sdl">已扫信息</param>
        /// <returns>对应货位列表</returns>
        private List<InvPositionInfo> GetTempPosList(StockInDetail sdl)
        {
            List<InvPositionInfo> tempPos = new List<InvPositionInfo>();
            foreach (InvPositionInfo pos in stock.OperaPositions)
            {
                if (pos.InvCode == sdl.cInvCode && pos.Batch == sdl.Batch)
                    tempPos.Add(pos);
            }
            return tempPos;
        }
        #endregion

        #endregion
    }
}