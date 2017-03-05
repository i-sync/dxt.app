using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GSPVouchDetail
    {
        /*
         * select cbuscode 业务号,KCSaleOuth.cwhcode 仓库编码,KCSaleOuth.cwhname 仓库,cinvcode 存货编码,cmaker 制单人,
         * KCSaleOutB.ID as id 主表ID,cCode 出库单号,cinvm_unit 计量单位,iquantity 数量,
         * ddate 出库日期,cbatch 批号,cposition 货位,ccuscode 客户编码,cinvname 存货名称,cinvstd 规格型号,dvdate 失效日期,
         * ccusabbname 客户,autoid 其它入库单编号,dmadedate生产日期,ltrim(rtrim(str(iinvexchrate,19,5))) as iinvexchrate 转换率,
         * cdefine2 客户全称,cdefine3 所在行政省,cdefine7 表头自定义项7 (0),cdefine11 表头自定义项11,cdefine22 表头自定义项22,
         * cmemo 备注,cinvdefine1 生产单位,cinvdefine6 产地,cinvdefine7 上海复星货号,imassdate 保质期, CMASSUNIT 保质期单位,
         * (CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS [IEXPIRATDATECALCU] 有效期推算方式,
         * KCSaleOutB.dExpirationdate as CVALDATE  有效期计算项,
         * case  isnull([Inventory_Sub].[iExpiratDateCalcu],0) when 1 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)   when 2 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)+'-'+RIGHT('00'+convert(varchar,day(KCSaleOutB.dExpirationdate), 2),2)  else '' end  as CVALDATES 有效期至,
         *from KCSaleOutB inner join KCSaleOuth on kcsaleouth.id=kcsaleoutb.id 
INNER JOIN  [dbo].[Inventory_Sub] ON  [dbo].[KCSaleOutB].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode] 
where ddate>=N'2007-03-01' and  1=1   And ((CCODE >= N'EMO201207180002') And (CCODE <= N'EMO201207180002')) and isnull(cgspstate,'')=N'' and isnull(cHandler,'')<>N'' and isnull(iquantity,0)>0 order by ID

         */
        #region 属性

        private string m_cbuscode;
        /// <summary>
        /// 业务号
        /// </summary>
        public string cbuscode
        {
            get { return m_cbuscode; }
            set { m_cbuscode = value; }
        }

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

        private string m_cmaker;
        /// <summary>
        /// 制单人
        /// </summary>
        public string cmaker
        {
            get { return m_cmaker; }
            set { m_cmaker = value; }
        }

        private int m_ID;
        /// <summary>
        /// 主表ID
        /// </summary>
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        private string m_cCode;
        /// <summary>
        /// 出库单号
        /// </summary>
        public string cCode
        {
            get { return m_cCode; }
            set { m_cCode = value; }
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

        private decimal m_iquantity;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal iquantity
        {
            get { return m_iquantity; }
            set { m_iquantity = value; }
        }

        private decimal m_FQUANTITY;
        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal FQUANTITY
        {
            get { return m_FQUANTITY; }
            set { m_FQUANTITY = value; }
        }

        private DateTime m_ddate;
        /// <summary>
        /// 出库日期
        /// </summary>
        public DateTime ddate
        {
            get { return m_ddate; }
            set { m_ddate = value; }
        }

        private string m_cbatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string cbatch
        {
            get { return m_cbatch; }
            set { m_cbatch = value; }
        }

        private string m_cposition;
        /// <summary>
        /// 货位
        /// </summary>
        public string cposition
        {
            get { return m_cposition; }
            set { m_cposition = value; }
        }

        private string m_ccuscode;
        /// <summary>
        /// 客户编码
        /// </summary>
        public string ccuscode
        {
            get { return m_ccuscode; }
            set { m_ccuscode = value; }
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

        private DateTime m_dvdate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime dvdate
        {
            get { return m_dvdate; }
            set { m_dvdate = value; }
        }

        private string m_ccusabbname;
        /// <summary>
        /// 客户
        /// </summary>
        public string ccusabbname
        {
            get { return m_ccusabbname; }
            set { m_ccusabbname = value; }
        }

        private string m_autoid;
        /// <summary>
        /// 其它入库单编号
        /// </summary>
        public string autoid
        {
            get { return m_autoid; }
            set { m_autoid = value; }
        }

        private DateTime m_dmadedate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dmadedate
        {
            get { return m_dmadedate; }
            set { m_dmadedate = value; }
        }

        private string m_iinvexchrate;
        /// <summary>
        /// 转换率
        /// </summary>
        public string iinvexchrate
        {
            get { return m_iinvexchrate; }
            set { m_iinvexchrate = value; }
        }

        private string m_cdefine2;
        /// <summary>
        /// 客户全称
        /// </summary>
        public string cdefine2
        {
            get { return m_cdefine2; }
            set { m_cdefine2 = value; }
        }

        private string m_cdefine3;
        /// <summary>
        /// 所在行政省
        /// </summary>
        public string cdefine3
        {
            get { return m_cdefine3; }
            set { m_cdefine3 = value; }
        }

        private decimal m_cdefine7;
        /// <summary>
        /// 表头自定义项7 (0)
        /// </summary>
        public decimal cdefine7
        {
            get { return m_cdefine7; }
            set { m_cdefine7 = value; }
        }

        private string m_cdefine11;
        /// <summary>
        /// 表头自定义项11(客户全称)
        /// </summary>
        public string cdefine11
        {
            get { return m_cdefine11; }
            set { m_cdefine11 = value; }
        }

        private string m_cdefine22;
        /// <summary>
        /// 表头自定义项22
        /// </summary>
        public string cdefine22
        {
            get { return m_cdefine22; }
            set { m_cdefine22 = value; }
        }

        private string m_cmemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string cmemo
        {
          get { return m_cmemo; }
          set { m_cmemo = value; }
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

        private string m_cinvdefine6;
        /// <summary>
        /// 产地
        /// </summary>
        public string cinvdefine6
        {
            get { return m_cinvdefine6; }
            set { m_cinvdefine6 = value; }
        }

        private string m_cinvdefine7;
        /// <summary>
        /// 上海复星货号
        /// </summary>
        public string cinvdefine7
        {
            get { return m_cinvdefine7; }
            set { m_cinvdefine7 = value; }
        }

        private int m_imassdate;
        /// <summary>
        /// 保质期
        /// </summary>
        public int imassdate
        {
            get { return m_imassdate; }
            set { m_imassdate = value; }
        }

        private string m_CMASSUNIT;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string CMASSUNIT
        {
            get { return m_CMASSUNIT; }
            set { m_CMASSUNIT = value; }
        }

        private string m_IEXPIRATDATECALCU;
        /// <summary>
        /// 有效期推算方式
        /// </summary>
        public string IEXPIRATDATECALCU
        {
            get { return m_IEXPIRATDATECALCU; }
            set { m_IEXPIRATDATECALCU = value; }
        }

        private DateTime m_CVALDATE;
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public DateTime CVALDATE
        {
            get { return m_CVALDATE; }
            set { m_CVALDATE = value; }
        }

        private DateTime m_CVALDATES;
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime CVALDATES
        {
            get { return m_CVALDATES; }
            set { m_CVALDATES = value; }
        }

        /// <summary>
        /// 质量情况：2012－10－24
        /// </summary>
        public string CRESULT { get; set; }

        #endregion

        public GSPVouchDetail CreateAttriveDetail()
        {
            return (GSPVouchDetail)this.MemberwiseClone();
        }

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

        #region 构造函数
        public GSPVouchDetail()
        { }

        public GSPVouchDetail(System.Data.DataRow dr)
        {
            this.cbuscode = DB2String(dr["cbuscode"]);
            this.cwhcode = DB2String(dr["cwhcode"]);
            this.cwhname = DB2String(dr["cwhname"]);
            this.cinvcode = DB2String(dr["cinvcode"]);
            this.cmaker = DB2String(dr["cmaker"]);
            this.ID = DB2Int(dr["id"]);
            this.cCode = DB2String(dr["cCode"]);
            this.cinvm_unit = DB2String(dr["cinvm_unit"]);
            this.iquantity = DB2Decimal(dr["iquantity"]);
            this.ddate = DB2DateTime(dr["ddate"]);
            this.cbatch = DB2String(dr["cbatch"]);
            this.cposition = DB2String(dr["cposition"]);
            this.ccuscode = DB2String(dr["ccuscode"]);
            this.cinvname = DB2String(dr["cinvname"]);
            this.cinvstd = DB2String(dr["cinvstd"]);
            this.dvdate = DB2DateTime(dr["dvdate"]);
            this.ccusabbname = DB2String(dr["ccusabbname"]);
            this.autoid = DB2String(dr["autoid"]);
            this.dmadedate = DB2DateTime(dr["dmadedate"]);
            this.iinvexchrate = DB2String(dr["iinvexchrate"]);
            this.cdefine2 = DB2String(dr["cdefine2"]);
            this.cdefine3 = DB2String(dr["cdefine3"]);
            this.cdefine7 = DB2Decimal(dr["cdefine7"]);
            this.cdefine11 = DB2String(dr["cdefine11"]);
            this.cdefine22 = DB2String(dr["cdefine22"]);
            this.cmemo = DB2String(dr["cmemo"]);
            this.cinvdefine1 = DB2String(dr["cinvdefine1"]);
            this.cinvdefine6 = DB2String(dr["cinvdefine6"]);
            this.cinvdefine7 = DB2String(dr["cinvdefine7"]);
            this.imassdate = DB2Int(dr["imassdate"]);
            this.CMASSUNIT = DB2String(dr["CMASSUNIT"]);
            this.IEXPIRATDATECALCU = DB2String(dr["IEXPIRATDATECALCU"]);
            this.CVALDATES = DB2DateTime(dr["CVALDATES"]);
            this.CVALDATE = DB2DateTime(dr["CVALDATE"]);
        }
        #endregion
    }
}
