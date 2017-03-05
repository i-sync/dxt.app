using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CheckDetail
    {
        /// <summary>
        /// 存货编码
        /// </summary>
        private string m_cinvcode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cinvcode
        {
            get { return m_cinvcode; }
            set { m_cinvcode = value; }
        }

        /// <summary>
        /// 规格
        /// </summary>
        private string m_cinvstd;
        /// <summary>
        /// 规格
        /// </summary>
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        /// <summary>
        /// 存货名称
        /// </summary>
        private string m_cinvname;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cinvname
        {
            get { return m_cinvname; }
            set { m_cinvname = value; }
        }


        /// <summary>
        /// 主计量单位
        /// </summary>
        private string m_strComUnit;
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string ComUnit
        {
            get { return m_strComUnit; }
            set { m_strComUnit = value; }
        }

        /// <summary>
        /// 辅计量单位
        /// </summary>
        private string m_strAssComUnit;
        /// <summary>
        /// 辅计量单位
        /// </summary>
        public string AssComUnit
        {
            get { return m_strAssComUnit; }
            set { m_strAssComUnit = value; }
        }

        /// <summary>
        /// 主计量单位名称
        /// </summary>
        private string m_strComUnitName;
        /// <summary>
        /// 主计量单位名称
        /// </summary>
        public string ComUnitName
        {
            get { return m_strComUnitName; }
            set { m_strComUnitName = value; }
        }

        private string m_cPosition;
        /// <summary>
        /// 货位
        /// </summary>
        public string cPosition
        {
            get { return m_cPosition; }
            set { m_cPosition = value; }
        }

        private string m_cbatch;
        /// <summary>
        /// 批次
        /// </summary>
        public string cbatch
        {
            get { return m_cbatch; }
            set { m_cbatch = value; }
        }

        private decimal m_iQuantity;
        /// <summary>
        /// 盘点数量
        /// </summary>
        public decimal iQuantity
        {
            get { return m_iQuantity; }
            set { m_iQuantity = value; }
        }
        private decimal m_iCVQuantity;
        /// <summary>
        /// 账面数量
        /// </summary>
        public decimal iCVQuantity
        {
            get { return m_iCVQuantity; }
            set { m_iCVQuantity = value; }
        }

        /// <summary>
        /// 盈亏数量
        /// </summary>
        public decimal CalQuantity
        {
            get { return m_iQuantity - m_iCVQuantity; }
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

        private DateTime m_dMadeDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dMadeDate
        {
            get { return m_dMadeDate; }
            set { m_dMadeDate = value; }
        }
        private DateTime m_cExpirationdate;
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime cExpirationdate
        {
            get { return m_cExpirationdate; }
            set { m_cExpirationdate = value; }
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

        public CheckDetail()
        {
        }


        public CheckDetail(CheckDetail cd)
        {
            this.cinvcode = cd.cinvcode;
            this.cinvname = cd.cinvname;
            this.cinvstd = cd.cinvstd;
            this.cinvdefine1 = cd.cinvdefine1;
            this.cinvdefine6 = cd.cinvdefine6;
            this.ComUnitName = cd.ComUnitName;
            this.cbatch = cd.cbatch;
            this.cPosition = cd.cPosition;
            this.iCVQuantity = cd.iCVQuantity;
            this.dMadeDate = cd.dMadeDate;
            this.cExpirationdate = cd.cExpirationdate;
            this.dvdate = cd.dvdate;
        }
        public CheckDetail(System.Data.DataRow dr)
        {
            this.cinvcode = DB2String(dr["cinvcode"]);
            this.cinvname = DB2String(dr["cinvname"]);
            this.cinvstd = DB2String(dr["cinvstd"]);
            this.cinvdefine1 = DB2String(dr["cinvdefine1"]);
            this.cinvdefine6 = DB2String(dr["cinvdefine6"]);
            this.ComUnitName = DB2String(dr["cComUnitName"]);
            this.cbatch = DB2String(dr["cCVBatch"]);
            this.cPosition = DB2String(dr["cPosition"]);
            this.iCVQuantity = DB2Decimal(dr["iCVQuantity"]);
            this.dMadeDate = DB2DateTime(dr["dMadeDate"]);
            this.cExpirationdate = DB2DateTime(dr["cExpirationdate"]);
            this.dvdate = DB2DateTime(dr["dDisDate"]);
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
    }
}
