using System;
using System.Collections.Generic;
using System.Windows.Forms;

using U8Business;
using Model;

namespace HTApp
{
    public partial class frmSource : Form
    {
        /// <summary>
        /// 来源数据显示
        /// </summary>
        /// <param name="List">来源数据</param>
        public frmSource(object obj)
        {
            InitializeComponent();

            DataGridTableStyle dts = new DataGridTableStyle();
            DataGridTextBoxColumn dtbc;

            if (obj.GetType().Equals(typeof(StockIn)))
            {
                StockIn stock = obj as StockIn;
                List<StockInDetail> dataList = stock.U8Details;
                #region DataGridTextBoxColumn
                string cIsOut = stock.IsOut ? "出库" : "入库";
                string cVouch = stock.SaveVouch.ToLower();

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = cIsOut + "仓库";
                dtbc.MappingName = "cWhName";
                dtbc.Width = 100;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                if (cVouch[0] != 't')
                {
                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "供应商简称";
                    dtbc.MappingName = "VenAbbName";
                    dtbc.Width = 150;
                    dtbc.Format = "G";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);

                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "供应商全称";
                    dtbc.MappingName = "Venname";
                    dtbc.Width = 200;
                    dtbc.Format = "G";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);
                }

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "存货编码";
                dtbc.MappingName = "cInvCode";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "存货名称";
                dtbc.MappingName = "Invname";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "规格型号";
                dtbc.MappingName = "cInvStd";
                dtbc.Width = 100;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "主计量";
                dtbc.MappingName = "Invm_unit";
                dtbc.Width = 50;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                if (cVouch != "04" && stock.U8Details[0].bInvBatch)
                {
                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "批号";
                    dtbc.MappingName = "Batch";
                    dtbc.Width = 100;
                    dtbc.Format = "G";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);
                }

                cIsOut = cVouch == "04" ? "领料" : cIsOut;
                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "应" + cIsOut + "数量";
                dtbc.MappingName = "OrderQuantity";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "已" + cIsOut + "数量";
                dtbc.MappingName = "fValidInQuan";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "待" + cIsOut + "数量";
                dtbc.MappingName = "fShallInQuan";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                if (cVouch[0] != 't')
                {
                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "本币无税单价";
                    dtbc.MappingName = "Unitcost";
                    dtbc.Width = 100;
                    dtbc.Format = "F2";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);

                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "本币无税金额";
                    dtbc.MappingName = "Price";
                    dtbc.Width = 100;
                    dtbc.Format = "F2";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);

                    dtbc = new DataGridTextBoxColumn();
                    dtbc.HeaderText = "税率";
                    dtbc.MappingName = "Taxrate";
                    dtbc.Width = 50;
                    dtbc.Format = "F2";
                    dts.GridColumnStyles.Add(dtbc);
                    dgSource.TableStyles.Add(dts);
                }
                #endregion
                dts.MappingName = dataList.GetType().Name;

                dgSource.DataSource = dataList;
                return;
            }

            else if (obj.GetType().Equals(typeof(ArrivalVouch)))
            {
                ArrivalVouch arrival = obj as ArrivalVouch;
                List<ArrivalVouchs> dataList = arrival.U8Details;
                #region DataGridTextBoxColumn
                string cIsOut = arrival.bIsOut ? "出货" : "到货";

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "供应商简称";
                dtbc.MappingName = "cVenAbbName";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                //dtbc = new DataGridTextBoxColumn();
                //dtbc.HeaderText = "供应商全称";
                //dtbc.MappingName = "cVenName";
                //dtbc.Width = 200;
                //dtbc.Format = "G";
                //dts.GridColumnStyles.Add(dtbc);
                //dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "存货编码";
                dtbc.MappingName = "cInvCode";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "存货名称";
                dtbc.MappingName = "cInvName";
                dtbc.Width = 150;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "规格型号";
                dtbc.MappingName = "cInvStd";
                dtbc.Width = 100;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "主计量";
                dtbc.MappingName = "cInvm_Unit";
                dtbc.Width = 50;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "应" + cIsOut + "数量";
                dtbc.MappingName = "OrderQuantity";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "已" + cIsOut + "数量";
                dtbc.MappingName = "fValidInQuan";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "待" + cIsOut + "数量";
                dtbc.MappingName = "nQuantity";
                dtbc.Width = 100;
                dtbc.Format = "F4";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "原币单价";
                dtbc.MappingName = "iOriCost";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "原币含税单价";
                dtbc.MappingName = "iOriTaxCost";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "原币金额";
                dtbc.MappingName = "iOriMoney";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "原币税额";
                dtbc.MappingName = "iOriTaxPrice";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "原币价税合计";
                dtbc.MappingName = "iOriSum";
                dtbc.Width = 100;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "税率";
                dtbc.MappingName = "iTaxRate";
                dtbc.Width = 50;
                dtbc.Format = "F2";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "订单编号";
                dtbc.MappingName = "cOrderCode";
                dtbc.Width = 100;
                dtbc.Format = "G";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);

                dtbc = new DataGridTextBoxColumn();
                dtbc.HeaderText = "计划" + cIsOut + "日期";
                dtbc.MappingName = "dArriveDate";
                dtbc.Width = 100;
                dtbc.Format = "yyyy-MM-dd";
                dts.GridColumnStyles.Add(dtbc);
                dgSource.TableStyles.Add(dts);
                #endregion
                dts.MappingName = dataList.GetType().Name;

                dgSource.DataSource = dataList;
                return;
            }
        }

        private void frmSource_Load(object sender, EventArgs e)
        {
            this.Location = System.Drawing.Point.Empty;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}