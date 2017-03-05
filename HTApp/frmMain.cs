using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using U8Business;

namespace HTApp
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 登录窗体对象
        /// </summary>
        frmLogin login;
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取登录窗体
        /// </summary>
        /// <param name="login"></param>
        public frmMain(frmLogin login)
            : this()
        {
            this.login = login;
        }

        /// <summary>
        /// 点击退出时，再次显示登录窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //frmLogin f = new frmLogin();
            //f.ShowDialog();
            //f.Dispose();
            this.Dispose();
            login.Show();
            login.Activate();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            ///根据权限来控件操作界面按钮是否可用
            //采购到货
            btnPUArrival.Enabled = Common.s_Competence.CGDH;
            //采购入库
            btnPUIn.Enabled = Common.s_Competence.CGRK;
            //采购退货
            btnPURefund.Enabled = Common.s_Competence.CGRK;
            //采购退货GSP
            btnPurchaseBackGSP.Enabled = Common.s_Competence.CGTHGSP;
            //销售出库拣货
            btnPicking.Enabled = Common.s_Competence.XSFH;
            //销售出库GSP
            btnSaleOutGSP.Enabled = Common.s_Competence.XSCKGSP;
            //销售退货GSP
            btnSaleBackGSP.Enabled = Common.s_Competence.XSTHGSP;
            //销售出库红字
            btnSaleOutRed.Enabled = Common.s_Competence.XSCK;
            //产成品入库
            btnSTInProduct.Enabled = Common.s_Competence.CCPRK;
            //盘点
            btnCheck.Enabled = Common.s_Competence.PD;
            //材料出库
            btnStuffOut.Enabled = Common.s_Competence.CLCK;
            //委外到货
            btnOSHalfIn.Enabled = Common.s_Competence.WWDH;
            //其它出库
            btnAllotOut.Enabled = btnDIFinalOut.Enabled = btnPAHalfOut.Enabled = Common.s_Competence.QTCK;
            //其它入库
            btnAllontIn.Enabled = btnDIHalfIn.Enabled = btnPAFinalIn.Enabled = Common.s_Competence.QTRK;
            //货位管理
            btnPosition.Enabled = Common.s_Competence.HWGL;

        }

        /// <summary>
        /// 采购到货扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPUArrival_Click(object sender, EventArgs e)
        {
            //采购入库到货扫描界面
            frmPUArrival fpu = new frmPUArrival();
            fpu.ShowDialog();

        }

        /// <summary>
        /// 采购入库扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPUIn_Click(object sender, EventArgs e)
        {
            frmPUIn frm = new frmPUIn();
            frm.ShowDialog();
        }

        /// <summary>
        /// 销售出库拣货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPicking_Click(object sender, EventArgs e)
        {
            frmSaleOutPicking frmp = new frmSaleOutPicking();
            frmp.ShowDialog();
            frmp.Dispose();
        }

        /// <summary>
        /// 销售出库GSP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaleOutGSP_Click(object sender, EventArgs e)
        {
            frmSaleOutGSP fgsp = new frmSaleOutGSP();
            fgsp.ShowDialog();
            fgsp.Dispose();
        }

        /// <summary>
        /// 产成品入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSTInProduct_Click(object sender, EventArgs e)
        {
            frmSTInProduct fstinp = new frmSTInProduct();
            fstinp.ShowDialog();
            fstinp.Dispose();
        }

        /// <summary>
        /// 销售出库红字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaleOutRed_Click(object sender, EventArgs e)
        {
            frmSaleOutRed fsor = new frmSaleOutRed();
            fsor.ShowDialog();
            fsor.Dispose();
        }

        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            frmCheck fc = new frmCheck();
            fc.ShowDialog();
            fc.Dispose();
        }

        /// <summary>
        /// 销售退货GSP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaleBackGSP_Click(object sender, EventArgs e)
        {
            frmSaleBackGSP fsbg = new frmSaleBackGSP();
            fsbg.ShowDialog();
            fsbg.Dispose();
        }

        /// <summary>
        /// 采购退货GSP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurchaseBackGSP_Click(object sender, EventArgs e)
        {
            frmPurchaseBackGSP fpbg = new frmPurchaseBackGSP();
            fpbg.ShowDialog();
            fpbg.Dispose();
        }

        /// <summary>
        /// 材料出库扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStuffOut_Click(object sender, EventArgs e)
        {
            using (frmOSStuffOut fsot = new frmOSStuffOut())
            {
                fsot.ShowDialog();
            }
        }

        /// <summary>
        /// 采购入库退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPURefund_Click(object sender, EventArgs e)
        {
            using (frmPURefund fprd = new frmPURefund())
            {
                fprd.ShowDialog();
            }
        }

        /// <summary>
        /// 调拨出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllotOut_Click(object sender, EventArgs e)
        {
            using (frmAllotOut faot = new frmAllotOut())
            {
                faot.ShowDialog();
            }
        }

        /// <summary>
        /// 调拨入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllontIn_Click(object sender, EventArgs e)
        {
            using (frmAllotIn fain = new frmAllotIn())
            {
                fain.ShowDialog();
            }
        }

        /// <summary>
        /// 组装成品入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPAFinalIn_Click(object sender, EventArgs e)
        {
            using (frmPAFinalIn fpfi = new frmPAFinalIn())
            {
                fpfi.ShowDialog();
            }
        }

        /// <summary>
        /// 组装半成品出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPAHalfOut_Click(object sender, EventArgs e)
        {
            using (frmPAHalfOut fpho = new frmPAHalfOut())
            {
                fpho.ShowDialog();
            }
        }

        /// <summary>
        /// 拆卸成品出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDIFinalOut_Click(object sender, EventArgs e)
        {
            using (frmDIFinalOut fdfi = new frmDIFinalOut())
            {
                fdfi.ShowDialog();
            }
        }

        /// <summary>
        /// 拆卸半成品入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDIHalfIn_Click(object sender, EventArgs e)
        {
            using (frmDIHalfIn fdhi = new frmDIHalfIn())
            {
                fdhi.ShowDialog();
            }
        }

        /// <summary>
        /// 委外到货扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOSHalfIn_Click(object sender, EventArgs e)
        {
            using (frmOSArrival fohi = new frmOSArrival())
            {
                fohi.ShowDialog();
            }
        }


        /// <summary>
        /// 点击出库货位管理，自动为没有添加货位的出库单添加货位信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPosition_Click(object sender, EventArgs e)
        {
            string errMsg;
            Cursor.Current = Cursors.WaitCursor;
            int result = DispatchListBusiness.InsertInvPosition(out errMsg);
            Cursor.Current = Cursors.Default;
            if (result == -2) //表示没有要处理的出库单
            {
                MessageBox.Show("没有要处理的出库单");
            }
            else if (result == -1)
            {
                MessageBox.Show("处理出错：" + errMsg);
            }
            else
            {
                MessageBox.Show("处理完成！");
            }    
        }


        /// <summary>
        /// 点击查询，弹出查询窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuantity_Click(object sender, EventArgs e)
        {
            frmQuantitySearch frm = new frmQuantitySearch();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmMenu frmM = new frmMenu(login))
            {
                frmM.ShowDialog();
            }
        }
    }
}