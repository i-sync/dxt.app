using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PurchaseBackDetail
    {
        /*
         * select ID 采购入库单主表编号 ,ccode 入库单号,darvdate 到货日期,zpurRkdList.cvencode 供货单位编码,vendor.cvenname as cvenname 供货单位,
         * cinvcode 存货编码,cinvname 存货名称,cinvstd 存货规格,cbatch 批号,iquantity 数量,round(inum,8) as inum 件数,
         * cwhcode 仓库编码,cwhname 仓库名称,autoid 子表id,dmadedate 生产日期,zpurRkdList.cmemo 备注,cmaker 制单人,
         * cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,
         * cdefine1 供应商全称,cdefine2 客户全称,cdefine3 所在省,cdefine4 日期,cdefine5,cdefine6,cdefine7 0,cdefine8,cdefine9,
         * cdefine10 药品流通监管码,cdefine11 单位全称(其它出库),cdefine12 销售订单号,cdefine13,cdefine14,cdefine15,cdefine16,
         * cdefine22 产地,cdefine23 中成药生产日期,cdefine24 所在省,cdefine25 请货单号,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,
         * cinva_unit 辅计量单位,cinvm_unit 计量单位,ltrim(rtrim(str(iinvexchrate,19,5))) as iinvexchrate 换算率,
         * cassunit 辅计量单位码,id 主表ID,ufts,imassdate 保质期,
         * zpurRkdList.CMASSUNIT 保质期单位,
         * (CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS [IEXPIRATDATECALCU] 有效期推算方式,
         * case  isnull([Inventory_Sub].[iExpiratDateCalcu],0) when 1 then convert(varchar ,Year(dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(dExpirationdate) ,2),2) when 2 then convert(varchar ,Year(dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(dExpirationdate) ,2),2)+'-'+RIGHT('00'+convert(varchar,day(dExpirationdate), 2),2) Else '' end  as CVALDATES 有效期至,
         * cbatchproperty1,cbatchproperty2,cbatchproperty3,cbatchproperty4,cbatchproperty5,cbatchproperty6,cbatchproperty7,cbatchproperty8,cbatchproperty9,cbatchproperty10   
            from zpurRkdList 
            inner join vendor on vendor.cvencode=zpurRkdList.cvencode  
            INNER JOIN [dbo].[Inventory_Sub] ON  [dbo].[zpurRkdList].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode] 
            where ddate>=N'2007-03-01' and isnull(bpufirst,0)=0 
            and isnull(biafirst,0)=0  and ddate>=N'2007-03-01'    
            and isnull(cgspstate,'')=N'' and isnull(cHandler,'')<>N'' and isnull(iquantity,0)<0 
            And (CCODE = N'109201207100018') 
            order by ID
         * */

        #region 属性

        private string m_iRdId;
        /// <summary>
        /// 采购入库单主表编号
        /// </summary>
        public string iRdId
        {
            get { return m_iRdId; }
            set { m_iRdId = value; }
        }

        private string m_cRdCode;
        /// <summary>
        /// 采购入库单编号
        /// </summary>
        public string cRdCode
        {
            get { return m_cRdCode; }
            set { m_cRdCode = value; }
        }

        private string m_dArvdate;
        /// <summary>
        /// 到货日期
        /// </summary>
        public string dArvdate
        {
            get { return m_dArvdate; }
            set { m_dArvdate = value; }
        }

        private string m_cVenCode;
        /// <summary>
        /// 供货单位编码 
        /// </summary>
        public string cVenCode
        {
            get { return m_cVenCode; }
            set { m_cVenCode = value; }
        }

        private string m_cRdMaker;
        /// <summary>
        /// 退货人
        /// </summary>
        public string cRdMaker
        {
            get { return m_cRdMaker; }
            set { m_cRdMaker = value; }
        }

        private string m_cDefine1;
        /// <summary>
        /// 表头自定义项1（供应商全称）
        /// </summary>
        public string cDefine1
        {
            get { return m_cDefine1; }
            set { m_cDefine1 = value; }
        }

        private string m_cDefine2;
        /// <summary>
        /// 表头自定义项2（客户全称）
        /// </summary>
        public string cDefine2
        {
            get { return m_cDefine2; }
            set { m_cDefine2 = value; }
        }

        private string m_iRdsID;
        /// <summary>
        /// 采购入库单子表编号
        /// </summary>
        public string IRdsID
        {
            get { return m_iRdsID; }
            set { m_iRdsID = value; }
        }

        private string m_cInvcode;
        /// <summary>
        /// 商品编码
        /// </summary>
        public string cInvcode
        {
            get { return m_cInvcode; }
            set { m_cInvcode = value; }
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

        private string m_cBatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch
        {
            get { return m_cBatch; }
            set { m_cBatch = value; }
        }

        private string m_dMadeDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string dMadeDate
        {
            get { return m_dMadeDate; }
            set { m_dMadeDate = value; }
        }

        private string m_dValDate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string dValDate
        {
            get { return m_dValDate; }
            set { m_dValDate = value; }
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

        private string m_CValDate;
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public string CValDate
        {
            get { return m_CValDate; }
            set { m_CValDate = value; }
        }

        private string m_cdefine22;
        /// <summary>
        /// 表头自定义项22(产地)
        /// </summary>
        public string cdefine22
        {
            get { return m_cdefine22; }
            set { m_cdefine22 = value; }
        }

        private decimal m_iQuantity;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal iQuantity
        {
            get { return m_iQuantity; }
            set { m_iQuantity = value; }
        }

        private string m_cWhCode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode
        {
            get { return m_cWhCode; }
            set { m_cWhCode = value; }
        }

        private string m_cWhName;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName
        {
            get { return m_cWhName; }
            set { m_cWhName = value; }
        }

        private decimal m_ScanCount;
        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal ScanCount
        {
            get { return m_ScanCount; }
            set { m_ScanCount = value; }
        }

        private string m_cInstance;
        /// <summary>
        /// 质量情况
        /// </summary>
        public string cInstance
        {
            get { return m_cInstance; }
            set { m_cInstance = value; }
        }


        #endregion

        public PurchaseBackDetail CreateAttriveDetail()
        {
            return (PurchaseBackDetail)this.MemberwiseClone();
        } 

        #region 构造函数
        public PurchaseBackDetail()
        { }

        public PurchaseBackDetail(System.Data.DataRow dr)
        {
            //给表头部分
            this.iRdId = DB2String(dr["ID"]);
            this.cRdCode = DB2String(dr["ccode"]);
            this.dArvdate = DB2String(dr["darvdate"]);
            this.cVenCode = DB2String(dr["cvencode"]);
            this.cRdMaker = DB2String(dr["cmaker"]);
            this.cWhCode = DB2String(dr["cwhcode"]);
            this.cWhName = DB2String(dr["cwhname"]);
            this.cDefine1 = DB2String(dr["cdefine1"]);
            //给表体部分
            this.IRdsID = DB2String(dr["autoid"]);
            this.cInvcode = DB2String(dr["cInvcode"]);
            this.cinvname = DB2String(dr["cinvname"]);
            this.cinvstd = DB2String(dr["cinvstd"]);
            this.dMadeDate = DB2String(dr["dmadedate"]);
            this.cBatch = DB2String(dr["cbatch"]);
            this.imassDate = DB2Int(dr["imassdate"]);
            this.dValDate = DB2DateTime(dr["dmadedate"]).AddMonths(this.imassDate).ToString("yyyy-MM-dd");//DB2String(dr["CVALDATES"]);
            this.iQuantity = DB2Decimal(dr["iquantity"]);
            this.cdefine22 = DB2String(dr["cdefine22"]);
            this.cMassUnit = DB2String(dr["CMASSUNIT"]);
            //dr["CVALDATES"]有时为空，所以改为如下
            this.CValDate = string.IsNullOrEmpty(dr["CVALDATES"].ToString())?DB2DateTime(dr["dmadedate"]).AddMonths(this.imassDate).AddDays(-1).ToString("yyyy-MM-dd"):DB2String(dr["CVALDATES"]);

            this.cDefine1 = DB2String(dr["cdefine1"]);
            this.cDefine2 = DB2String(dr["cdefine2"]);
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
