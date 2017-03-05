using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DispatchDetail
    {
        #region 属性

        private string m_cwhname;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cwhname
        {
            get { return m_cwhname; }
            set { m_cwhname = value; }
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
        /// 规格
        /// </summary>
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        private string m_cvenabbname;
        /// <summary>
        /// 产地 生产厂家
        /// </summary>
        public string cvenabbname
        {
            get { return m_cvenabbname; }
            set { m_cvenabbname = value; }
        }

        private string m_comunit;
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string comunit
        {
            get { return m_comunit; }
            set { m_comunit = value; }
        }

        ///// <summary>
        ///// 主计量单位名称
        ///// </summary>
        //private string m_strComUnitName;
        //public string ComUnitName
        //{
        //    get { return m_strComUnitName; }
        //    set { m_strComUnitName = value; }
        //}

        private decimal m_iquantity;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal iquantity
        {
            get { return m_iquantity; }
            set { m_iquantity = value; }
        }

        private decimal m_inewquantity;
        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal inewquantity
        {
            get { return m_inewquantity; }
            set { m_inewquantity = value; }
        }

        private decimal m_iquotedprice;
        /// <summary>
        /// 报价
        /// </summary>
        public decimal iquotedprice
        {
            get { return m_iquotedprice; }
            set { m_iquotedprice = value; }
        }

        private decimal m_itaxunitprice;
        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal itaxunitprice
        {
            get { return m_itaxunitprice; }
            set { m_itaxunitprice = value; }
        }

        private decimal m_imoney;
        /// <summary>
        /// 无税金额
        /// </summary>
        public decimal imoney
        {
            get { return m_imoney; }
            set { m_imoney = value; }
        }

        private decimal m_isum;
        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal isum
        {
            get { return m_isum; }
            set { m_isum = value; }
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

        private string m_invbatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string invbatch
        {
            get { return m_invbatch; }
            set { m_invbatch = value; }
        }

        private DateTime m_dmdate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dmdate
        {
            get { return m_dmdate; }
            set { m_dmdate = value; }
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

        private DateTime m_dvdate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime dvdate
        {
            get { return m_dvdate; }
            set { m_dvdate = value; }
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

        private int m_irowno;
        /// <summary>
        /// 订单行号
        /// </summary>
        public int irowno
        {
            get { return m_irowno; }
            set { m_irowno = value; }
        }

        private decimal m_kl;
        /// <summary>
        /// 扣率
        /// </summary>
        public decimal kl
        {
            get { return m_kl; }
            set { m_kl = value; }
        }

        private decimal m_kl2;
        /// <summary>
        /// 扣率2
        /// </summary>
        public decimal kl2
        {
            get { return m_kl2; }
            set { m_kl2 = value; }
        }

        private decimal m_iunitprice;
        /// <summary>
        /// 原币无税单价
        /// </summary>
        public decimal iunitprice
        {
            get { return m_iunitprice; }
            set { m_iunitprice = value; }
        }

        private decimal m_itax;
        /// <summary>
        /// 原币税额
        /// </summary>
        public decimal itax
        {
            get { return m_itax; }
            set { m_itax = value; }
        }

        private decimal m_idiscount;
        /// <summary>
        /// 原币折扣额
        /// </summary>
        public decimal idiscount
        {
            get { return m_idiscount; }
            set { m_idiscount = value; }
        }

        private decimal m_inatunitprice;
        /// <summary>
        /// 本币无税单价 
        /// </summary>
        public decimal inatunitprice
        {
            get { return m_inatunitprice; }
            set { m_inatunitprice = value; }
        }

        private decimal m_inatmoney;
        /// <summary>
        /// 本币无税金额 
        /// </summary>
        public decimal inatmoney
        {
            get { return m_inatmoney; }
            set { m_inatmoney = value; }
        }

        private decimal m_inattax;
        /// <summary>
        /// 本币税额 
        /// </summary>
        public decimal inattax
        {
            get { return m_inattax; }
            set { m_inattax = value; }
        }

        private decimal m_inatsum;
        /// <summary>
        /// 本币价税合计 
        /// </summary>
        public decimal inatsum
        {
            get { return m_inatsum; }
            set { m_inatsum = value; }
        }

        private decimal m_inatdiscount;
        /// <summary>
        /// 本币折扣额 
        /// </summary>
        public decimal inatdiscount
        {
            get { return m_inatdiscount; }
            set { m_inatdiscount = value; }
        }

        private int m_isosid;
        /// <summary>
        /// 销售订单子表标识2 
        /// </summary>
        public int isosid
        {
            get { return m_isosid; }
            set { m_isosid = value; }
        }

        private string m_cdefine22;
        /// <summary>
        /// cdefine22 产地
        /// </summary>
        public string cdefine22
        {
            get { return m_cdefine22; }
            set { m_cdefine22 = value; }
        }

        private decimal m_fsalecost;
        /// <summary>
        /// 零售单价 
        /// </summary>
        public decimal fsalecost
        {
            get { return m_fsalecost; }
            set { m_fsalecost = value; }
        }

        private decimal m_fsaleprice;
        /// <summary>
        /// 零售金额 
        /// </summary>
        public decimal fsaleprice
        {
            get { return m_fsaleprice; }
            set { m_fsaleprice = value; }
        }

        private string m_csocode;
        /// <summary>
        /// 销售订单号 
        /// </summary>
        public string csocode
        {
            get { return m_csocode; }
            set { m_csocode = value; }
        }

        private string m_cordercode;
        /// <summary>
        /// 订单号 
        /// </summary>
        public string cordercode
        {
            get { return m_cordercode; }
            set { m_cordercode = value; }
        }

        private decimal m_fcusminprice;
        /// <summary>
        /// 客户最低售价 
        /// </summary>
        public decimal fcusminprice
        {
            get { return m_fcusminprice; }
            set { m_fcusminprice = value; }
        }

        private int m_cmassunit;
        /// <summary>
        /// 保值期单位 
        /// </summary>
        public int cmassunit
        {
            get { return m_cmassunit; }
            set { m_cmassunit = value; }
        }

        private int m_iexpiratdatecalcu;
        /// <summary>
        /// 有效期推算方式 
        /// </summary>
        public int iexpiratdatecalcu
        {
            get { return m_iexpiratdatecalcu; }
            set { m_iexpiratdatecalcu = value; }
        }

        private DateTime m_dexpirationdate;
        /// <summary>
        /// 有效期计算项 
        /// </summary>
        public DateTime dexpirationdate
        {
            get { return m_dexpirationdate; }
            set { m_dexpirationdate = value; }
        }

        private DateTime m_cexpirationdate;
        /// <summary>
        /// 有效期至 
        /// </summary>
        public DateTime cexpirationdate
        {
            get { return m_cexpirationdate; }
            set { m_cexpirationdate = value; }
        }

        private string m_cvencode;
        /// <summary>
        /// 入库单供应商编码 
        /// </summary>
        public string cvencode
        {
            get { return m_cvencode; }
            set { m_cvencode = value; }
        }

        private decimal m_iFHQuantity;
        /// <summary>
        /// 累计发货数量
        /// </summary>
        public decimal IFHQuantity
        {
            get { return m_iFHQuantity; }
            set { m_iFHQuantity = value; }
        }
        #endregion


        private string m_cposition;
        /// <summary>
        /// 货位编码
        /// </summary>
        public string cposition
        {
            get { return m_cposition; }
            set { m_cposition = value; }
        }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string cinvdefine1 { get; set; }

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

        /// <summary>
        /// 请货单号
        /// </summary>
        public string cdefine25
        {
            get;
            set;
        }

        /// <summary>
        /// 订单行号
        /// </summary>
        public int iorderrowno
        {
            get;
            set;
        }

        /// <summary>
        /// 报价含税标识 
        /// </summary>
        public int bsaleprice
        {
            get;
            set;
        }
        /// <summary>
        /// 是否赠品 
        /// </summary>
        public int bgift
        {
            get;
            set;
        }


        #region 构造函数
        public DispatchDetail()
        { }

        public DispatchDetail(System.Data.DataRow dr)
        {
            this.cwhname = DB2String(dr["cwhname"]);
            this.cwhcode = DB2String(dr["cwhcode"]);
            this.cinvcode = DB2String(dr["cinvcode"]);
            this.cinvname = DB2String(dr["cinvname"]);
            this.cinvstd = DB2String(dr["cinvstd"]);
            this.cvenabbname = DB2String(dr["cvenabbname"]);
            this.comunit = DB2String(dr["cinvm_unit"]);
            this.iquantity = DB2Decimal(dr["iquantity"]);
            this.iquotedprice = DB2Decimal(dr["iquotedprice"]);
            this.itaxunitprice = DB2Decimal(dr["itaxunitprice"]);
            this.imoney = DB2Decimal(dr["imoney"]);
            this.isum = DB2Decimal(dr["isum"]);
            this.itaxrate = DB2Decimal(dr["itaxrate"]);
            this.kl = DB2Decimal(dr["kl"]);
            this.kl2 = DB2Decimal(dr["kl2"]);
            this.imassdate = DB2Int(dr["imassdate"]);
            this.irowno = DB2Int(dr["irowno"]);
            this.iunitprice = DB2Decimal(dr["iunitprice"]);
            this.itax = DB2Decimal(dr["itax"]);
            this.idiscount = DB2Decimal(dr["idiscount"]);
            this.inatunitprice = DB2Decimal(dr["inatunitprice"]);
            this.inatmoney = DB2Decimal(dr["inatmoney"]);
            this.inattax = DB2Decimal(dr["inattax"]);
            this.inatsum = DB2Decimal(dr["inatsum"]);
            this.inatdiscount = DB2Decimal(dr["inatdiscount"]);
            this.isosid = DB2Int(dr["isosid"]);
            this.cdefine22 = DB2String(dr["cdefine22"]);
            this.fsalecost = DB2Decimal(dr["fsalecost"]);
            this.fsaleprice = DB2Decimal(dr["fsaleprice"]);
            this.csocode = DB2String(dr["csocode"]);
            this.cordercode = DB2String(dr["cordercode"]);
            this.fcusminprice = DB2Decimal(dr["fcusminprice"]);
            this.iexpiratdatecalcu = DB2Int(dr["iexpiratdatecalcu"]);
            this.cvencode = DB2String(dr["cvencode"]);
            this.IFHQuantity = DB2Decimal(dr["iFHQuantity"]);
            //2012-10-23
            this.cinvdefine1 = DB2String(dr["cinvdefine1"]);
            this.cdefine25= DB2String(dr["cdefine25"]);

            this.iorderrowno = DB2Int(dr["iorderrowno"]);

            this.bsaleprice = DB2Int(dr["bsaleprice"]);
            this.bgift = DB2Int(dr["bgift"]);
        }
        #endregion

        

        public DispatchDetail CreateAttriveDetail()
        {
            return (DispatchDetail)this.MemberwiseClone();
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
        #endregion
    }
}
