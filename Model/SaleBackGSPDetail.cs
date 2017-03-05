using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaleBackGSPDetail
    {
        /*
         * SELECT dispatchlists.cwhcode 仓库编码,warehouse.cwhname 仓库,dispatchlists.cinvcode 存货编码,
         * dispatchlists.cinvname 存货名称,ccode 入库单号,dispatchlist.csocode 销售订单号,
         * convert(char,convert(money,dispatchlist.ufts),2) as ufts,cbustype 业务类型,caccounter 记账人,cdlcode 发货退货单号,
         * dispatchlist.cvouchtype 单据类型编码,cvouchname,dispatchlist.cstcode 销售类型编码,cstname,ddate 单据日期,
         * dispatchlist.cdepcode 部门编码,cdepname,dispatchlist.cpersoncode 业务员编码,cpersonname,
         * dispatchlist.ccuscode 客户编码,ccusabbname,dispatchlist.cpaycode 付款条件编码,cexch_name 币种名称,
         * iexchrate 汇率,dispatchlist.itaxrate 表头税率,cdefine1,cdefine2,breturnflag 退货标志,cpayname,
         * dispatchlist.dlid 发货退货单主表标识,cverifier 审核人,cmaker 制单人,bfirst 销售期初标志,
         * cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,
         * cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,isale 是否先发货,Customer.ccusname 客户名称,Customer.ccusabbname 客户简称,
         * dispatchlist.ccusperson 客户联系人,ccuspostcode 邮政编码,bservice,cinvstd 规格型号,
         * (case when igrouptype=1 then cunitid else '' end) as cunitid 辅计量单位编码,unit1.ccomunitname as cinvm_unit 计量单位,
         * igrouptype 计量单位组类别,inventory.cgroupcode 计量单位组编码,
         * (case when igrouptype=1 then unit2.ccomunitname else '' end) as cinva_unit 计量单位,
         * (case when isnull(dispatchlists.itb, 0) <> 0 then abs(dispatchlists.tbnum) else (case when igrouptype=0 then null else  abs(dispatchlists.inum)  end) end) as inum 辅计量数量 ,
         * (case when igrouptype=1 then convert(decimal(19,5),iinvexchrate) else null end) as iinvexchrate 换算率 ,
         * (case when isnull(dispatchlists.itb, 0) <> 0 then abs(dispatchlists.tbquantity) else abs(dispatchlists.iquantity) end) as iquantity 数量,
         * isettlenum,isettlequantity,iquotedprice,itaxunitprice,iunitprice,abs(imoney) as imoney 原币无税金额,
         * cbatch 批号,icorid 原发货退货单子表标识,binvbatch,bfree1,bfree2,dvdate 失效日期,dmdate 生产日期,
         * dispatchlists.cExpirationdate AS cvaldate 有效期至,dispatchlists.dexpirationdate AS dvaldate 有效期计算项,
         * dispatchlists.imassdate 保质期, 
         * (CASE dispatchlists.CMASSUNIT WHEN 0 THEN '' WHEN 1 THEN '年' WHEN 2 THEN '月' WHEN 3 THEN '日' END) AS CMASSUNIT 保质期单位,
         * (CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS IEXPIRATDATECALCU 有效期推算方式,
         * cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine32,cdefine31,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,
         * idlsid 发货退货单子表标识2 ,citemcode 项目编码,citem_class 项目大类编码,cvenabbname 供应商简称,citemname 项目名称,
         * citem_cname 项目大类名称,dispatchlist.cmemo 备注,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,
         * case when bgsp=0 then '否' else '是' end as bgsp,bSpecialties as '特殊药品标志',abs(isum) as isum 原币价税合计,
         * (case when isnull(foutquantity,0)=0 then null else foutquantity end) as 累计出库数量,
         * (case when isnull(foutnum,0)=0 then null else foutnum end) as 累计出库件数,
         * Inventory.cinvdefine1,Inventory.cinvdefine2,Inventory.cinvdefine3,Inventory.cinvdefine4,Inventory.cinvdefine5,
         * Inventory.cinvdefine6,Inventory.cinvdefine7,Inventory.cinvdefine8,Inventory.cinvdefine9,Inventory.cinvdefine10,
         * Inventory.cinvdefine11,Inventory.cinvdefine12,Inventory.cinvdefine13,Inventory.cinvdefine14,Inventory.cinvdefine15,Inventory.cinvdefine16,
         * DisPatchLists.AutoID 发货退货单子表标识, DisPatchLists.cbatchproperty1, DisPatchLists.cbatchproperty2,DisPatchLists.cbatchproperty3,
         * DisPatchLists.cbatchproperty4,DisPatchLists.cbatchproperty5,DisPatchLists.cbatchproperty6,DisPatchLists.cbatchproperty7,
         * DisPatchLists.cbatchproperty8,DisPatchLists.cbatchproperty9,DisPatchLists.cbatchproperty10 
         * FROM DisPatchList 
         * INNER JOIN DisPatchLists ON DisPatchList.dlid=DisPatchLists.dlid 
         * LEFT OUTER JOIN Customer ON DispatchList.cCusCode = Customer.cCusCode 
         * LEFT OUTER JOIN Department ON DispatchList.cDepCode = Department.cDepCode 
         * LEFT OUTER JOIN PayCondition ON DispatchList.cPayCode = PayCondition.cPayCode 
         * LEFT OUTER JOIN Person ON DispatchList.cPersonCode = Person.cPersonCode 
         * LEFT OUTER JOIN SaleType ON DispatchList.cSTCode = SaleType.cSTCode 
         * LEFT OUTER JOIN ShippingChoice ON DispatchList.cSCCode = ShippingChoice.cSCCode 
         * LEFT OUTER JOIN VouchType ON DispatchList.cVouchType = VouchType.cVouchType 
         * LEFT JOIN Warehouse ON DispatchLists.cWhCode = Warehouse.cWhCode 
         * LEFT JOIN Inventory ON DispatchLists.cInvCode = Inventory.cInvCode 
         * left join ComputationUnit as Unit1 on inventory.cComUnitCode=Unit1.cComUnitCode 
         * left join ComputationUnit as Unit2 on dispatchlists.cunitid=Unit2.cComUnitCode 
         * INNER JOIN  [dbo].[Inventory_Sub] ON  [dbo].[Inventory].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode]  
         * where (dispatchlist.cvouchtype=N'05' or dispatchlist.cvouchtype=N'06') and dispatchlist.ddate>=N'2007-03-01' 
         * and isnull(bgsp,0)=1 and isnull(cgspstate,'')=N'' and isnull(iquantity,0)<0 and isnull(cVerifier,'')<>N'' 
         * AND ISNULL(dispatchlists.bSettleAll,0)=0 AND isnull(itb,0)<>1 and isnull(dispatchlists.cwhcode,'')<>N'' 
         * and cdlcode = N'cdlcode'
         * order by dispatchlist.dlid
         * */

        #region 属性
        private string m_cwhcode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cwhcode
        {
            get { return m_cwhcode; }
            set { m_cwhcode = value; }
        }

        private string m_cwhname;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cwhname
        {
            get { return m_cwhname; }
            set { m_cwhname = value; }
        }

        private string m_cinvcode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cinvcode
        {
            get { return m_cinvcode; }
            set { m_cinvcode = value; }
        }

        private string m_cinvname;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cinvname
        {
            get { return m_cinvname; }
            set { m_cinvname = value; }
        }

        private string m_cinvstd;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        private decimal m_FQUANTITY;
        /// <summary>
        /// 实收数量
        /// </summary>
        public decimal FQUANTITY
        {
            get { return m_FQUANTITY; }
            set { m_FQUANTITY = value; }
        }

        private decimal m_FARVQUANTITY;
        /// <summary>
        /// 到货数量 
        /// </summary>
        public decimal FARVQUANTITY
        {
            get { return m_FARVQUANTITY; }
            set { m_FARVQUANTITY = value; }
        }

        private string m_DPRODATE;
        /// <summary>
        /// 生产日期 
        /// </summary>
        public string DPRODATE
        {
            get { return m_DPRODATE; }
            set { m_DPRODATE = value; }
        }

        private string m_DVDATE;
        /// <summary>
        /// 验收员签字日期 
        /// </summary>
        public string DVDATE
        {
            get { return m_DVDATE; }
            set { m_DVDATE = value; }
        }

        private string m_CVALDATE;
        /// <summary>
        /// 有效期 
        /// </summary>
        public string CVALDATE
        {
            get { return m_CVALDATE; }
            set { m_CVALDATE = value; }
        }

        private string m_DDATE_T;
        /// <summary>
        /// 退货日期
        /// </summary>
        public string DDATE_T
        {
            get { return m_DDATE_T; }
            set { m_DDATE_T = value; }
        }

        private decimal m_FELGQUANTITY;
        /// <summary>
        /// 合格数量 
        /// </summary>
        public decimal FELGQUANTITY
        {
            get { return m_FELGQUANTITY; }
            set { m_FELGQUANTITY = value; }
        }

        private string m_CBATCH;
        /// <summary>
        /// 批号
        /// </summary>
        public string CBATCH
        {
            get { return m_CBATCH; }
            set { m_CBATCH = value; }
        }

        private string m_CCUSCODE;
        /// <summary>
        /// 退货单位编码 
        /// </summary>
        public string CCUSCODE
        {
            get { return m_CCUSCODE; }
            set { m_CCUSCODE = value; }
        }

        private string m_CDEFINE22;
        /// <summary>
        /// 表体自定义项22(产地) 
        /// </summary>
        public string CDEFINE22
        {
            get { return m_CDEFINE22; }
            set { m_CDEFINE22 = value; }
        }

        private string m_ICODE_T;
        /// <summary>
        /// 采购到货退货单号 
        /// </summary>
        public string ICODE_T
        {
            get { return m_ICODE_T; }
            set { m_ICODE_T = value; }
        }

        private int m_imassDate;
        /// <summary>
        /// 保质期 
        /// </summary>
        public int imassDate
        {
            get { return m_imassDate; }
            set { m_imassDate = value; }
        }

        private string m_cMassUnit;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string cMassUnit
        {
            get { return m_cMassUnit; }
            set { m_cMassUnit = value; }
        }

        private string m_DValDate;
        /// <summary>
        /// 有效期后一天
        /// </summary>
        public string DValDate
        {
            get { return m_DValDate; }
            set { m_DValDate = value; }
        }

        private string m_cdlcode;
        /// <summary>
        /// 采购到货退货单号 
        /// </summary>
        public string cdlcode
        {
            get { return m_cdlcode; }
            set { m_cdlcode = value; }
        }

        private string m_dlid;
        /// <summary>
        /// 采购到货退货单主表标识 
        /// </summary>
        public string dlid
        {
            get { return m_dlid; }
            set { m_dlid = value; }
        }

        private string m_ddate;
        /// <summary>
        /// 到货退货日期 
        /// </summary>
        public string ddate
        {
            get { return m_ddate; }
            set { m_ddate = value; }
        }

        private decimal m_scanCount;
        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal ScanCount
        {
            get { return m_scanCount; }
            set { m_scanCount = value; }
        }

        private string m_cinvdefine1;
        /// <summary>
        /// 生产单位
        /// </summary>
        public string cinvdefine1
        {
            get { return m_cinvdefine1; }
            set { m_cinvdefine1 = value; }
        }

        private string m_cinvm_unit;
        /// <summary>
        /// 计量单位
        /// </summary>
        public string cinvm_unit
        {
            get { return m_cinvm_unit; }
            set { m_cinvm_unit = value; }
        }

        private string m_ccusname;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ccusname
        {
            get { return m_ccusname; }
            set { m_ccusname = value; }
        }

        private string m_ccusabbname;
        /// <summary>
        /// 客户简称
        /// </summary>
        public string ccusabbname
        {
            get { return m_ccusabbname; }
            set { m_ccusabbname = value; }
        }

        private string m_cvenabbname;
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string cvenabbname
        {
            get { return m_cvenabbname; }
            set { m_cvenabbname = value; }
        }

        private string m_COUTINSTANCE;
        /// <summary>
        /// 外观质量情况
        /// </summary>
        public string COUTINSTANCE
        {
            get { return m_COUTINSTANCE; }
            set { m_COUTINSTANCE = value; }
        }
        #endregion

        public SaleBackGSPDetail CreateAttriveDetail()
        {
            return (SaleBackGSPDetail)this.MemberwiseClone();
        } 

        #region 构造函数
        public SaleBackGSPDetail()
        { }

        public SaleBackGSPDetail(System.Data.DataRow dr)
        {
            //给表头部分
            this.cdlcode = DB2String(dr["cdlcode"]);
            this.dlid = DB2String(dr["dlid"]);
            this.ddate = DB2String(dr["ddate"]);
            //给表体部分
            this.cwhcode = DB2String(dr["cwhcode"]);
            this.cwhname = DB2String(dr["cwhname"]);
            this.cinvcode = DB2String(dr["cinvcode"]);
            this.cinvname = DB2String(dr["cinvname"]);
            this.cinvstd = DB2String(dr["cinvstd"]);
            this.FQUANTITY = DB2Decimal(dr["iquantity"]);
            this.FARVQUANTITY = DB2Decimal(dr["iquantity"]);
            this.CBATCH = DB2String(dr["cbatch"]);
            this.DPRODATE = DB2DateTime(dr["dmdate"]).ToString("yyyy-MM-dd"); //DB2String(dr["dmdate"]);
            this.DVDATE = DB2DateTime(dr["dvdate"]).ToString("yyyy-MM-dd");  //DB2String(dr["dvdate"]);
            this.CVALDATE = DB2DateTime(dr["cvaldate"]).ToString("yyyy-MM-dd");// DB2String(dr["cvaldate"]);
            this.imassDate = DB2Int(dr["imassdate"]);
            this.CCUSCODE = DB2String(dr["ccuscode"]);
            this.CDEFINE22 = DB2String(dr["cdefine22"]);
            this.ICODE_T = DB2String(dr["idlsid"]);
            //生产单位
            this.cinvdefine1 = DB2String(dr["cinvdefine1"]);
            this.cinvm_unit = DB2String(dr["cinvm_unit"]);
            this.ccusname = DB2String(dr["ccusname"]);
            this.ccusabbname = DB2String(dr["ccusabbname"]);
            this.cvenabbname = DB2String(dr["cvenabbname"]);
            
        }
        #endregion

        #region 转换
        public static string DB2String(object DBValue)
        {
            return DBValue != System.DBNull.Value ? DBValue.ToString() : "";
        }

        public static int DB2Int(object DBValue)
        {
            int iReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) iReturn = Convert.ToInt32(DBValue);
            }
            catch
            {
                iReturn = -10;
            }
            return iReturn;
        }

        public static Decimal DB2Decimal(object DBValue)
        {
            Decimal dReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) dReturn = Convert.ToDecimal(DBValue);
            }
            catch
            {
                dReturn = -10;
            }
            return dReturn;
        }
        public static Boolean DB2Bool(object DBValue)
        {
            bool blnReturn = false;
            try
            {
                if (DBValue != System.DBNull.Value) blnReturn = Convert.ToBoolean(DBValue);
            }
            catch
            {
                blnReturn = false;
            }
            return blnReturn;
        }

        public static DateTime DB2DateTime(object DBValue)
        {
            DateTime btReturn = DateTime.MinValue;
            try
            {
                if (DBValue != System.DBNull.Value)
                {
                    btReturn = Convert.ToDateTime(DBValue);
                }
            }
            catch
            {
                btReturn = DateTime.MaxValue;
            }
            return btReturn;
        }

        public static string GetNull(string str)
        {
            if (str == "null" || str == "")
                return "Null";
            else
                return "N'" + str + "'";
        }
        #endregion
    }
}
