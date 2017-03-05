using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PoInfo
    {
        /*
        string s = @"select p.cPOID 采购订单号,p.dPODate 单据日期,v.cVenName 供应商名称,d.cDepName 部门名称,hp.cPsn_Name 业务员名称,
pt.cPTName 采购类型,p.cexch_name 币种名称,p.nflat 汇率,p.iTaxRate 表头税率,p.iCost 运费,p.iBargain 订金,
p.cState 状态,p.cMaker 制单人,p.cVerifier 审核人,p.cCloser 关闭人,p.POID 采购订单主表标识,p.iVTid 单据模版号,
p.ufts 时间戳,p.cBusType 业务类型,p.iDiscountTaxType 扣税类别,(case p.iverifystateex when 0 then '输入' when 1 then '审核执行' when 2 then '关闭' end) 单据状态,
p.ireturncount 打回次数,p.IsWfControlled 是否启用工作流,(case pd.bGsp when 0 then '否' when 1 then '是' end) 是否质检
 from dbo.PO_Pomain p
 inner join dbo.Vendor v
 on p.cVenCode=v.cVenCode
 inner join dbo.Department d
 on p.cDepCode = d.cDepCode
 left join dbo.hr_hi_person hp
 on p.cPersonCode=hp.cPsn_Num
 inner join dbo.PurchaseType pt
 on p.cPTCode=pt.cPTCode 
 inner join dbo.PO_Podetails pd
 on p.POID=pd.POID
 where p.cPOID ='JHY200703050002';*/

        #region
        private string cPOID;
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string CPOID
        {
            get { return cPOID; }
            set { cPOID = value; }
        }

        private DateTime dPODate;
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime DPODate
        {
            get { return dPODate; }
            set { dPODate = value; }
        }

        private string cVenName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string CVenName
        {
            get { return cVenName; }
            set { cVenName = value; }
        }

        private string cDepName;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string CDepName
        {
            get { return cDepName; }
            set { cDepName = value; }
        }

        private string cPsn_Name;
        /// <summary>
        /// 业务员名称
        /// </summary>
        public string CPsn_Name
        {
            get { return cPsn_Name; }
            set { cPsn_Name = value; }
        }

        private string cPTName;
        /// <summary>
        /// 采购类型
        /// </summary>
        public string CPTName
        {
            get { return cPTName; }
            set { cPTName = value; }
        }

        private string cexch_name;
        /// <summary>
        /// 币种名称
        /// </summary>
        public string Cexch_name
        {
            get { return cexch_name; }
            set { cexch_name = value; }
        }

        private float nflat;
        /// <summary>
        /// 汇率
        /// </summary>
        public float Nflat
        {
            get { return nflat; }
            set { nflat = value; }
        }

        private float iTaxRate;
        /// <summary>
        /// 表头税率
        /// </summary>
        public float ITaxRate
        {
            get { return iTaxRate; }
            set { iTaxRate = value; }
        }

        private float iCost;
        /// <summary>
        /// 运费
        /// </summary>
        public float ICost
        {
            get { return iCost; }
            set { iCost = value; }
        }

        private float iBargain;
        /// <summary>
        /// 订金
        /// </summary>
        public float IBargain
        {
            get { return iBargain; }
            set { iBargain = value; }
        }
        

        private string cState;
        /// <summary>
        /// 状态
        /// </summary>
        public string CState
        {
            get { return cState; }
            set { cState = value; }
        }

        private string cMaker;
        /// <summary>
        /// 制单人
        /// </summary>
        public string CMaker
        {
            get { return cMaker; }
            set { cMaker = value; }
        }

        private string cVerifier;
        /// <summary>
        /// 审核人
        /// </summary>
        public string CVerifier
        {
            get { return cVerifier; }
            set { cVerifier = value; }
        }

        private string cCloser;
        /// <summary>
        /// 关闭人
        /// </summary>
        public string CCloser
        {
            get { return cCloser; }
            set { cCloser = value; }
        }

        private int m_POID;
        /// <summary>
        /// 采购订单主表标识
        /// </summary>
        public int POID
        {
            get { return m_POID; }
            set { m_POID = value; }
        }

        private int iVTid;
        /// <summary>
        /// 单据模版号
        /// </summary>
        public int IVTid
        {
            get { return iVTid; }
            set { iVTid = value; }
        }
        #endregion

        #region
        private DateTime ufts;
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Ufts
        {
            get { return ufts; }
            set { ufts = value; }
               
        }

        private string cBusType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string CBusType
        {
            get {return cBusType; }
            set { cBusType = value; }
        }

        private int iDiscountTaxType;
        /// <summary>
        /// 扣税类别
        /// </summary>
        public int IDiscountTaxType
        {
            get { return iDiscountTaxType; }
            set { iDiscountTaxType = value; }
        }

        private string iverifystateex;
        /// <summary>
        /// 单据状态
        /// </summary>
        public string Iverifystateex
        {
            get { return iverifystateex; }
            set { iverifystateex = value; }
        }

        private int ireturncount;
        /// <summary>
        /// 打回次数
        /// </summary>
        public int Ireturncount
        {
            get { return ireturncount; }
            set { ireturncount = value; }
        }

        public bool isWfControlled;
        /// <summary>
        /// 是否启用工作流
        /// </summary>
        public bool IsWfControlled
        {
            get { return isWfControlled; }
            set { isWfControlled = value; }
        }

        private string bGsp;
        /// <summary>
        /// 是否质检
        /// </summary>
        public string BGsp
        {
            get { return bGsp; }
            set { bGsp = value; }
        }
        #endregion
    }
}
