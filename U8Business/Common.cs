using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Model;
using System.Data;

namespace U8Business
{
    public class Common
    {
        public static DateTime err_Time = new DateTime(0001, 1, 1, 0, 0, 0, 0);
        public static List<Warehouse> s_Warehouse = new List<Warehouse>();
        /// <summary>
        /// 操作权限
        /// </summary>
        public static Competence s_Competence;
        private U8Business.Service.Service service;
        public U8Business.Service.Service Service
        {
            get { return service; }
        }

        private static Common co;

        private Common()
        {
            service = new U8Business.Service.Service();
            service.Url = Common.CurrentUser.ServiceUrl;
        }
        /// <summary>
        /// 单例获取Common
        /// </summary>
        /// <returns>实例</returns>
        public static Common GetInstance()
        {
            if (co == null)
                co = new Common();

            return co;
        }

        private static User m_CurrentUser;
        /// <summary>
        /// 当前登录的操作者
        /// </summary>
        public static User CurrentUser
        {
            get { return m_CurrentUser; }
            set { m_CurrentUser = value; }
        }


        private static string m_connectionString;
        private static string m_CustomerName;
        private static string m_ErrorMsg = "";

        /// <summary>
        /// 获取服务器程序版本
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return Common.GetInstance().service.GetVersion();
        }
        /// <summary>
        /// 获取服务器最新的配置文档
        /// </summary>
        /// <returns></returns>
        public string GetNewDocument()
        {
            return Common.GetInstance().service.GetNewDocument();
        }
                
        /// <summary>
        /// 登录前查询账套信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetUAAcountInfo(string userID)
        {
            return Common.GetInstance().service.GetUAAcountInfo(userID);
        }

        public static void UserLogin()
        {
            Common co = Common.GetInstance();
            int i = co.service.LoginIn(m_CurrentUser.UserId, m_CurrentUser.Password, m_CurrentUser.Accid, m_CurrentUser.Year, out m_CustomerName, out m_connectionString, out m_ErrorMsg);
            if (i != 0)
            {
                throw new Exception(m_ErrorMsg);
            }

            m_CurrentUser.ConnectionString = m_connectionString;
            m_CurrentUser.UserName = m_CustomerName;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        public static void GetInformation()
        {
            Common.co = Common.GetInstance();
            DataSet ds = null;
            string errMsg = "";
            co.service.GetInformation(m_CurrentUser.UserId,m_CurrentUser.Accid,m_CurrentUser.Year, Common.CurrentUser.ConnectionString, out ds, out errMsg);
            DepartMent m_dep;
            RD_Style m_rd;
            Warehouse m_wa;
            //Person person;
            //foreach (DataRow dr in ds.Tables["Department"].Rows)
            //{
            //    m_dep = new DepartMent();
            //    m_dep.cdepcode = dr["cdepcode"].ToString();
            //    m_dep.cdepname = dr["cdepname"].ToString();
            //    s_DepartMent.Add(m_dep);
            //}

            //foreach (DataRow dr in ds.Tables["rd_style"].Rows)
            //{
            //    m_rd = new RD_Style();
            //    m_rd.crdcode = dr["crdcode"].ToString();
            //    m_rd.crdname = dr["crdname"].ToString();
            //    m_rd.brdflag = Convert.ToInt32(dr["brdflag"]);
            //    s_RD_Style.Add(m_rd);
            //}
            foreach (DataRow dr in ds.Tables["Warehouse"].Rows)
            {
                m_wa = new Warehouse();
                m_wa.cwhcode = dr["cwhcode"].ToString();
                m_wa.cwhname = dr["cwhname"].ToString();
                m_wa.bwhpos = Convert.ToInt32(dr["bwhpos"]);
                s_Warehouse.Add(m_wa);
            }
            //如果有多少仓库，则需要添加一个空让操作人员第一次选择，若就一个仓库则默认
            if (s_Warehouse.Count > 1)
            {
                s_Warehouse.Insert(0, new Warehouse() { cwhcode = "-1", cwhname = "请选择仓库..." });
            }

            //foreach (DataRow dr in ds.Tables["Person"].Rows)
            //{
            //    person = new Person();
            //    person.cPersonCode = dr["cPersonCode"].ToString();
            //    person.cPersonName = dr["cPersonName"].ToString();
            //    lstPerson.Add(person);
            //}


            ///获取操作人员的操作权限
            int result = co.service.Competence(CurrentUser.UserId, CurrentUser.Accid, CurrentUser.Year, CurrentUser.ConnectionString, out ds, out errMsg);
            if (result == 0)
            {
                s_Competence = new Competence(ds.Tables[0].Rows[0]);
            }

            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }

        }

        public static int getSTOutInvBatch(string cwhcode, string cinvcode, string cbatch, out DataSet InvBatch, out string errMsg)
        {
            Common co = Common.GetInstance();
            return co.service.getSTOutInvBatch(cwhcode, cinvcode, cbatch, Common.CurrentUser.ConnectionString, out InvBatch, out errMsg);
        }

        /// <summary>
        /// 根据一维码（69码）获取存货编码
        /// 2012－09－04
        /// </summary>
        /// <param name="cbarcode">69码</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>查到返回true,没有查到返回false</returns>
        public static bool GetCInvCode(string cBarCode, out string cInvCode, out string errMsg)
        {
            Common co = Common.GetInstance();
            return co.service.GetCInvCode(cBarCode, Common.CurrentUser.ConnectionString, out cInvCode, out errMsg);
        }

         /// <summary>
        /// 根据仓库编码获取所有货位及货位存储信息
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-10</remarks>
        public static bool GetPosition(string cWhCode, out List<Position> list, out string errMsg)
        {
            bool flag = false;
            Common co = Common.GetInstance();
            DataSet ds_Position = null;
            list = null;
            flag = co.service.GetPosition(Common.CurrentUser.ConnectionString, cWhCode, out ds_Position, out errMsg);

            if (flag)
            {
                list = new List<Position>();
                Position position = null;
                foreach (DataRow row in ds_Position.Tables[0].Rows)
                {
                    position = new Position();
                    position.cPosCode = row["cPosCode"].ToString();
                    position.cInvCode = row["cInvCode"].ToString();
                    position.iQuantity = row["iQuantity"]==DBNull.Value?0:Convert.ToSingle(row["iQuantity"]);
                    list.Add(position);
                }
            }
            return flag;
        }
        
        /// <summary>
        /// 货位现存量查询
        /// </summary>
        /// <param name="cBarCode">69码</param>
        /// <param name="isPosition">标识是否是货位存量查询还是现存量查询</param>
        /// <param name="list">查询的结果集合</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-10-29</remarks>
        public static int QuantitySerarch(string cBarCode, bool isPosition, out List<IQuantitySearch> list, out string errMsg)
        {
            int result;
            list = null;
            ///仓库编码集合
            List<string> whcondition = null;
            if (s_Warehouse.Count > 0)
            {
                whcondition = new List<string>();
            }
            foreach (Warehouse wh in s_Warehouse)
            {
                whcondition.Add(wh.cwhcode);
            }
            //数据集合
            DataSet ds;
            result = co.service.QuantitySerarch(cBarCode, isPosition, whcondition.ToArray(), Common.CurrentUser.ConnectionString, out ds, out errMsg);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //初始化集合对象
                list = new List<IQuantitySearch>();
                IQuantitySearch qs = null;
                //遍历所有数据
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    qs = new IQuantitySearch();
                    qs.cWhCode = DB2String(row["cWhCode"]);
                    qs.cWhName = DB2String(row["cWhName"]);
                    qs.cPosCode = DB2String(row["cPosCode"]);
                    qs.cInvCode = DB2String(row["cInvCode"]);
                    qs.cInvName = DB2String(row["cInvName"]);
                    qs.cInvStd = DB2String(row["cInvStd"]);
                    qs.cInvDefine1 = DB2String(row["cInvDefine1"]);
                    qs.cInvDefine6 = DB2String(row["cInvDefine6"]);
                    qs.cBatch = DB2String(row["cBatch"]);
                    qs.iQuantity = DB2Decimal(row["iQuantity"]);
                    qs.dMdate = DB2DateTime(row["dMdate"]);
                    qs.cExpirationdate = DB2DateTime(row["cExpirationdate"]);
                    qs.dVDate = DB2DateTime(row["dVDate"]);
                    qs.iMassDate = DB2Int(row["iMassDate"]);
                    list.Add(qs);
                }
            }
            return result;
        }

        #region //数据库字段转换工具
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

        public static DateTime DB2DateTime(object DBValue)
        {
            DateTime dateReturn = err_Time;
            try
            {
                if (DBValue != System.DBNull.Value) dateReturn = Convert.ToDateTime(DBValue);
            }
            catch
            {
                dateReturn = err_Time;
            }
            return dateReturn;
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
