﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;

namespace HTApp
{
    public partial class frmGSPList : Form
    {
        SaleOutGSPVouch sv;

        public frmGSPList(SaleOutGSPVouch sv)
        {
            InitializeComponent();
            if (sv.OperateDetails == null || sv.OperateDetails.Count < 1)
            {
                return;
            }
            this.sv = sv;
            DataGridTableStyle dts = new DataGridTableStyle();

            DataGridTextBoxColumn dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "仓库名称";
            dtbc.MappingName = "cwhname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货编码";
            dtbc.MappingName = "cinvcode";
            dtbc.Width = 80;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "存货名称";
            dtbc.MappingName = "cinvname";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "批号";
            dtbc.MappingName = "cbatch";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "货位";
            dtbc.MappingName = "cposition";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "数量";
            dtbc.MappingName = "iquantity";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "扫描数量";
            dtbc.MappingName = "FQUANTITY";
            dtbc.Width = 70;
            dtbc.Format = "F2";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "规格";
            dtbc.MappingName = "cinvstd";
            dtbc.Width = 90;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "产地";
            dtbc.MappingName = "cinvdefine6";
            dtbc.Width = 100;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "计量单位";
            dtbc.MappingName = "cinvm_unit";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "保质期";
            dtbc.MappingName = "imassdate";
            dtbc.Width = 70;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "生产日期";
            dtbc.MappingName = "dmadedate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "有效期至";
            dtbc.MappingName = "CVALDATES";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "失效日期";
            dtbc.MappingName = "dvdate";
            dtbc.Width = 100;
            dtbc.Format = "yyyy-MM-dd";
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "客户全称";
            dtbc.MappingName = "cdefine11";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);

            dtbc = new DataGridTextBoxColumn();
            dtbc.HeaderText = "客户简称";
            dtbc.MappingName = "ccusabbname";
            dtbc.Width = 120;
            dts.GridColumnStyles.Add(dtbc);


            dataGrid1.TableStyles.Add(dts);
            dataGrid1.RowHeadersVisible = true;
            dts.MappingName = sv.OperateDetails.GetType().Name;
            this.dataGrid1.DataSource = sv.OperateDetails;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sv.OperateDetails.Count < 1)
                {
                    MessageBox.Show("没有操作的数据!");
                    return;
                }
                if (MessageBox.Show("确定要删除吗？",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int rindex = dataGrid1.CurrentRowIndex;
                    //已扫描数量
                    decimal sub = sv.OperateDetails[rindex].FQUANTITY;
                    string cinvcode = sv.OperateDetails[rindex].cinvcode;
                    string cbatch = sv.OperateDetails[rindex].cbatch;
                    dataGrid1.DataSource = null;
                    sv.OperateDetails.RemoveAt(rindex);//删除操作数据
                    dataGrid1.DataSource = sv.OperateDetails;
                    //查询时同时根据存货编码与批次
                    GSPVouchDetail dd = sv.U8Details.Find((delegate(GSPVouchDetail tdd) { return tdd.cinvcode.Equals(cinvcode) && tdd.cbatch.Equals(cbatch); }));
                    dd.FQUANTITY -= sub;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
            
    }
}