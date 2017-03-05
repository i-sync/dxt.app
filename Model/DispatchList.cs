using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DispatchList
    {
        #region 属性
        private string m_csoid;
        /// <summary>
        /// 订单ID
        /// </summary>
        public string csoid
        {
            get { return m_csoid; }
            set { m_csoid = value; }
        }

        private string m_cDLCode;
        /// <summary>
        /// 发货单号
        /// </summary>
        public string cDLCode
        {
            get { return m_cDLCode; }
            set { m_cDLCode = value; }
        }
        
        private DateTime m_dDate;
        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime dDate
        {
            get { return m_dDate; }
            set { m_dDate = value; }
        }
        
        private string m_cbustype;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string cbustype
        {
            get { return m_cbustype; }
            set { m_cbustype = value; }
        }
        
        private string m_cstcode;
        /// <summary>
        /// 销售类型
        /// </summary>
        public string cstcode
        {
            get { return m_cstcode; }
            set { m_cstcode = value; }
        }
        private string m_csocode;
        /// <summary>
        /// 订单号
        /// </summary>
        public string csocode
        {
            get { return m_csocode; }
            set { m_csocode = value; }
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

        private string m_cdepcode;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cdepcode
        {
            get { return m_cdepcode; }
            set { m_cdepcode = value; }
        }

        private string m_cdepname;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string cdepname
        {
            get { return m_cdepname; }
            set { m_cdepname = value; }
        }

        private string m_cpersoncode;
        /// <summary>
        /// 业务员编码
        /// </summary>
        public string cpersoncode
        {
            get { return m_cpersoncode; }
            set { m_cpersoncode = value; }
        }

        private string m_cpersonname;
        /// <summary>
        /// 业务员
        /// </summary>
        public string cpersonname
        {
            get { return m_cpersonname; }
            set { m_cpersonname = value; }
        }

        private string m_ccusperson;
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string ccusperson
        {
            get { return m_ccusperson; }
            set { m_ccusperson = value; }
        }

        private string m_ccusoaddress;
        /// <summary>
        /// 发货地址
        /// </summary>
        public string ccusoaddress
        {
            get { return m_ccusoaddress; }
            set { m_ccusoaddress = value; }
        }

        private string m_caddcode;
        /// <summary>
        /// 发货地址编码 
        /// </summary>
        public string caddcode
        {
            get { return m_caddcode; }
            set { m_caddcode = value; }
        }
        
        private decimal m_itaxrate;
        /// <summary>
        /// 税率
        /// </summary>
        public decimal itaxrate
        {
            get { return m_itaxrate; }
            set { m_itaxrate = value; }
        }

        private decimal m_iExchRate;
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal iExchRate
        {
            get { return m_iExchRate; }
            set { m_iExchRate = value; }
        }

        private string m_cexch_name;
        /// <summary>
        /// 货币名称
        /// </summary>
        public string cexch_name
        {
            get { return m_cexch_name; }
            set { m_cexch_name = value; }
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

        private string m_cdefine2;
        /// <summary>
        /// cdefine2
        /// </summary>
        public string cdefine2
        {
            get { return m_cdefine2; }
            set { m_cdefine2 = value; }
        }

        private string m_cdefine3;
        /// <summary>
        /// cdefine3
        /// </summary>
        public string cdefine3
        {
            get { return m_cdefine3; }
            set { m_cdefine3 = value; }
        }

        private string m_cdefine10;
        /// <summary>
        /// 流通监管码
        /// </summary>
        public string cdefine10
        {
            get { return m_cdefine10; }
            set { m_cdefine10 = value; }
        }

        private string m_cdefine11;
        /// <summary>
        /// cdefine11
        /// </summary>
        public string cdefine11
        {
            get { return m_cdefine11; }
            set { m_cdefine11 = value; }
        }

        private string m_cdefine13;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string cDefine13
        {
            get { return m_cdefine13; }
            set { m_cdefine13 = value; }
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

        /// <summary>
        /// 发运方式编码
        /// </summary>
        private string m_cSCCode;
        /// <summary>
        /// 发运方式编码
        /// </summary>
        public string cSCCode
        {
            get { return m_cSCCode; }
            set { m_cSCCode = value; }
        }

        #endregion

        private List<DispatchDetail> m_U8Details;
        /// <summary>
        /// 销售订单
        /// </summary>
        public List<DispatchDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        private List<DispatchDetail> m_OperateDetails;
        /// <summary>
        /// 扫描数据
        /// </summary>
        public List<DispatchDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }


        //U8V11.1属性
        /// <summary>
        /// 信用单位编码
        /// </summary>
        public string ccreditcuscode
        {
            get;
            set;
        }
        /// <summary>
        /// 信用单位名称 
        /// </summary>
        public string ccreditcusname
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人编码 
        /// </summary>
        public string ccuspersoncode
        {
            get;
            set;
        }
        /// <summary>
        /// 开票单位编码 
        /// </summary>
        public string cinvoicecompany
        {
            get;
            set;
        }

        #region 构造函数
        public DispatchList()
        { }

        public DispatchList(System.Data.DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.csoid = DB2String(ds.Tables[0].Rows[0]["id"]);
                this.cstcode = DB2String(ds.Tables[0].Rows[0]["cstcode"]);
                this.csocode = DB2String(ds.Tables[0].Rows[0]["csocode"]);
                this.ccuscode = DB2String(ds.Tables[0].Rows[0]["ccuscode"]);
                this.ccusabbname = DB2String(ds.Tables[0].Rows[0]["ccusabbname"]);
                this.ccusname = DB2String(ds.Tables[0].Rows[0]["ccusname"]);
                this.cdepcode = DB2String(ds.Tables[0].Rows[0]["cdepcode"]);
                this.cdepname = DB2String(ds.Tables[0].Rows[0]["cdepname"]);
                this.cpersoncode = DB2String(ds.Tables[0].Rows[0]["cpersoncode"]);
                this.cpersonname = DB2String(ds.Tables[0].Rows[0]["cpersonname"]);
                this.caddcode = DB2String(ds.Tables[0].Rows[0]["caddcode"]);
                this.ccusoaddress = DB2String(ds.Tables[0].Rows[0]["ccusoaddress"]);
                this.cexch_name = DB2String(ds.Tables[0].Rows[0]["cexch_name"]);
                this.itaxrate = DB2Decimal(ds.Tables[0].Rows[0]["itaxrate"]);
                this.cmemo = DB2String(ds.Tables[0].Rows[0]["cmemo"]);
                this.cbustype = DB2String(ds.Tables[0].Rows[0]["cbustype"]);
                this.ccusperson = DB2String(ds.Tables[0].Rows[0]["ccusperson"]);
                this.iExchRate = DB2Decimal(ds.Tables[0].Rows[0]["iExchRate"]);
                this.cdefine2 = DB2String(ds.Tables[0].Rows[0]["cdefine2"]);
                this.cdefine3 = DB2String(ds.Tables[0].Rows[0]["cdefine3"]);
                this.cdefine11 = DB2String(ds.Tables[0].Rows[0]["cdefine11"]);
                this.cSCCode = DB2String(ds.Tables[0].Rows[0]["cSCCode"]);//发运方式编码 
                this.ccuspersoncode = DB2String(ds.Tables[0].Rows[0]["ccuspersoncode"]);//联系人编码 
                this.cinvoicecompany = DB2String(ds.Tables[0].Rows[0]["cinvoicecompany"]);//开票单位编码 
            }
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
        #endregion
    }
}
