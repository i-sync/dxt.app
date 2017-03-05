using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace U8Business
{
    public class checkvouch
    {
        #region 属性

        private string m_cMaker;
        public string cMaker
        {
            get { return m_cMaker; }
            set { m_cMaker = value; }
        }
        private string m_dCVDate;
        public string dCVDate
        {
            get { return m_dCVDate; }
            set { m_dCVDate = value; }
        }
        private string m_dnmaketime;
        public string dnmaketime
        {
            get { return m_dnmaketime; }
            set { m_dnmaketime = value; }
        }
        private string m_cDepCode;
        public string cDepCode
        {
            get { return m_cDepCode; }
            set { m_cDepCode = value; }
        }
        private string m_cWhCode;
        public string cWhCode
        {
            get { return m_cWhCode; }
            set { m_cWhCode = value; }
        }
        private string m_cIRdCode;
        public string cIRdCode
        {
            get { return m_cIRdCode; }
            set { m_cIRdCode = value; }
        }
        private string m_cORdCode;
        public string cORdCode
        {
            get { return m_cORdCode; }
            set { m_cORdCode = value; }
        }

        #endregion

        //操作数据
        private List<CheckDetail> m_CheckOperateDetail;
        public List<CheckDetail> CheckOperateDetail
        {
            get { return m_CheckOperateDetail; }
            set { m_CheckOperateDetail = value; }
        }

        public checkvouch()
        {
            this.m_CheckOperateDetail = new List<CheckDetail>();
        }

        /// <summary>
        /// 获取盘点单号及对应的仓库名称
        /// </summary>
        /// <param name="WHList">仓库编码列表</param>
        /// <returns>盘点单列表</returns>
        public static List<string> GetCheckVouchList(out List<string> WHList)
        {
            List<string> CVList = new List<string>();
            Common co = Common.GetInstance();
            string errMsg = "";
            DataSet ds = null;
            co.Service.getCVcodeList(Common.CurrentUser.ConnectionString, out ds, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
                return null;
            }
            else
            {
                WHList = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CVList.Add(Common.DB2String(dr[0]));
                    WHList.Add(Common.DB2String(dr[1]));
                }
                return CVList;
            }
        }

        /// <summary>
        /// 取条码数量和账面数量
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="cCVCode"></param>
        /// <param name="qty">条码数量</param>
        /// <param name="sqty">账面数量（合计）</param>
        /// <param name="autoid"></param>
        public static void getQtyByBarcode(string barcode, string cCVCode, ref string cCVBatch, out string qty, out string sqty, out string autoid, out string cinvname, out string cinvdefine1, out string cinvstd, out string cinvdefine6)
        {
            Common co = Common.GetInstance();
            string errMsg = "";
            co.Service.getQtyByBarcode(barcode, cCVCode,ref cCVBatch, Common.CurrentUser.ConnectionString, out qty, out sqty, out autoid, out cinvname, out cinvdefine1, out cinvstd,out cinvdefine6,out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            return;
        }

        /// <summary>
        /// 根据货位及存货编码获取盘点单子表数据
        /// </summary>
        /// <param name="cCVCode">盘点单</param>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cPosition">货位</param>
        /// <param name="cBatch">批次</param>
        /// <param name="ds">查询结果列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static void GetQtyByCode( string cCVCode, string cInvCode, string cPosition, string cBatch ,out List<CheckDetail> list)
        {
            Common co = Common.GetInstance();
            string errMsg = string.Empty;
            DataSet ds ;
            list = null;
            int result = co.Service.GetQtyByCode(Common.CurrentUser.ConnectionString, cCVCode, cInvCode, cPosition, cBatch, out ds, out errMsg);
            if (result == 0)
            {
                if (ds == null)
                {
                    return;
                }
                list = new List<CheckDetail>();
                ///循环表中数据
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new CheckDetail(row));
                }
            }
            else
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 提交盘点单
        /// </summary>
        /// <param name="cCVCode">盘点单</param>
        public void SubmitCheckVouchs(string cCVCode)
        {
            Common co = Common.GetInstance();
            string errMsg = "";
            //声明并初始化盘点数组
            U8Business.Service.CheckDetail[] list = new U8Business.Service.CheckDetail[this.CheckOperateDetail.Count];
            for(int i=0;i<this.CheckOperateDetail.Count;i++)
            {
                list[i] = new U8Business.Service.CheckDetail();
                list[i].cinvcode =CheckOperateDetail[i].cinvcode;
                list[i].cinvname = CheckOperateDetail[i].cinvname;
                list[i].cinvstd = CheckOperateDetail[i].cinvstd;
                list[i].cinvdefine1 = CheckOperateDetail[i].cinvdefine1;
                list[i].cinvdefine6 = CheckOperateDetail[i].cinvdefine6;
                list[i].ComUnitName = CheckOperateDetail[i].ComUnitName;
                list[i].cbatch = CheckOperateDetail[i].cbatch;
                list[i].cPosition = CheckOperateDetail[i].cPosition;
                list[i].iCVQuantity = CheckOperateDetail[i].iCVQuantity;
                list[i].dMadeDate = CheckOperateDetail[i].dMadeDate;
                list[i].cExpirationdate = CheckOperateDetail[i].cExpirationdate;
                list[i].dvdate = CheckOperateDetail[i].dvdate;
                list[i].iQuantity = CheckOperateDetail[i].iQuantity;
            }
            
            co.Service.SubmitCheckVouchs(cCVCode, list, Common.CurrentUser.ConnectionString, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            return;
        }
    }
}
