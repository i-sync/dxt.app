using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Reflection; 
using System.Diagnostics;

namespace U8DataAccess
{
    public class CommonDA
    {
        public struct info
        {
            public string subID;
            public string ERPService;
            public string sqlUser;
            public string sqlPassword;
            public bool stockInCanChange;
            public string DBService;
        }

        #region Login 系统登录
        /// <summary>
        /// 登陆U8系统确认身份，并得到登录字符串
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="accID">帐套名</param>
        /// <param name="year">财务年度</param>
        /// <param name="inf">struct类型</param>
        /// <param name="customerName">登录者名字</param>
        /// <param name="connectionString">生成的数据库连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>0：正确，非0，错误</returns>
        static public int Login(string userID, string password, string accID, string year, info inf,
            out string customerName, out string connectionString, out string errMsg)
        {

            customerName = "";
            connectionString = "";
            errMsg = "";
            string ERPService = "";     //ERP服务器地址
            string DBService = "";
            string sqlUser = "";
            string sqlPassword = "";

            try
            {
                sqlUser = inf.sqlUser;
                sqlPassword = inf.sqlPassword;
                ERPService = inf.ERPService;
                DBService = inf.DBService;

                connectionString = "user id=" + sqlUser + ";password=" + sqlPassword + ";data source='" + DBService
                        + "';persist security info=True;initial catalog=UFDATA_" + accID + "_" + year
                        + ";Connection Timeout=30";

                //方便调试
                //connectionString = "user id=sa;password=123;data source=192.168.80.128;persist security info=True;initial catalog=UFDATA_002_2010;Connection Timeout=30";
            }
            catch (Exception ex)
            {
                errMsg = "配置文件错误!" + ex.Message;
                return -1;
            }
            UFSoft.U8.Framework.Login.UI.clsLogin netLogin = new UFSoft.U8.Framework.Login.UI.clsLogin();
            //try
            //{
            string SQL = "select top 1 cUser_Name from ufsystem..ua_user where cUser_id=N'" + userID + "' and cPassword='" + netLogin.EnPassWord(password) + "'";
            DataSet Ds_User = new DataSet();
            //int i = OperationSql.GetDataset(SQL, connectionString, out Ds_User, out errMsg);
            //if (i != 0)
            //{
            //    return -2;
            //}
            //else
            //{
            //    if (Ds_User.Tables[0].Rows.Count == 0)
            //    {
            //        errMsg = "用户名或者密码错误";
            //        return -2;
            //    }
            //    else
            //    {
            //        customerName = Ds_User.Tables[0].Rows[0][0].ToString();
            //        return 0;
            //    }

            //}

            int flag = -1;
            try
            {
                Ds_User = DBHelperSQL.Query(connectionString, SQL);
                if (Ds_User.Tables[0].Rows.Count == 0)
                {
                    errMsg = "用户名或者密码错误";
                    return -2;
                }
                else
                {
                    customerName = Ds_User.Tables[0].Rows[0][0].ToString();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;

            //}
            //catch (Exception ex)
            //{
            //    errMsg = ex.Message;
            //    return -2;
            //}
        }
        #endregion


        public static string ServerDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static bool VerifyVersion(string[] FileVersion, string[] FileName)
        {
            try
            {
                string strVersion = null;
                for (int i = 0; i < FileName.Length; i++)
                {
                    FileVersionInfo fv = FileVersionInfo.GetVersionInfo(AppDomain.CurrentDomain.BaseDirectory + "\\update\\" + FileName[i]);
                    strVersion = fv.FileVersion;

                    if (FileVersion[i] != strVersion)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }


        //
        /// <summary>
        /// 根据用户获取仓库管理权限获得基础数据
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="connectionString"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        static public int GetInformation(string userID, string accID, string iYear,string connectionString, out DataSet ds, out string errMsg)
        {
            ds = new DataSet();
            DataTable dt1 = new DataTable("Department");
            DataTable dt2 = new DataTable("rd_style");
            DataTable dt3 = new DataTable("Warehouse");
            DataTable dt4 = new DataTable("packaging");
            DataTable dt5 = new DataTable("packag");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            ds.Tables.Add(dt4);
            ds.Tables.Add(dt5);
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();

            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
            cmd.Connection = conn;
            try
            {
                cmd.CommandText = "select cdepcode,cdepname from Department where bDepEnd=1 order by cdepcode";
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["Department"]);
                cmd.CommandText = "Select crdcode,crdname,bRdFlag,iRdGrade from Rd_Style  where irdgrade > 1 and brdend = 1";
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["rd_style"]);

                ///2012－10－24
                ///仓库权限：如果用户是账套主管或所在角色为账套主管UA_holdauth
                ///或者如果用户是权限主管或所有角色为权限主管AA_holdBusobject 
                ///那么显示所有的仓库
                ///如果不是那就进行普通权限分配查询，用户的仓库权限或所有角色仓库权限
                cmd.CommandText = string.Format(@"IF EXISTS(   
--查询用户是否为账套主管
SELECT 1 FROM  UFSystem.dbo.UA_HoldAuth WHERE cAuth_Id ='admin' AND cAcc_Id='{0}' AND iYear='{1}' AND cUser_Id ='{2}'
UNION ALL
--查询用户所有角色是否为账套主管
SELECT 1 FROM 
(SELECT cUser_Id FROM UFSystem.dbo.UA_HoldAuth WHERE cAuth_Id ='admin' AND cAcc_Id='{0}' AND iYear='{1}') h 
INNER JOIN (SELECT cGroup_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{2}') r ON h.cUser_Id= r.cGroup_Id

UNION ALL
--查询用户是否为仓库权限主管
SELECT 1 FROM AA_holdbusobject WHERE iAdmin =1 AND CBusObId ='warehouse' AND cUserId= '{2}'
UNION ALL
--查询用户所在角色是否仓库权限主管
SELECT 1 FROM 
(SELECT cUserId FROM  AA_holdbusobject WHERE iAdmin =1 AND CBusObId ='warehouse' ) h
INNER JOIN (SELECT cGroup_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{2}') r ON h.cUserId = r.cGroup_Id

)
--若是返回1
SELECT 1 AS flag
ELSE --否则返回0
SELECT 0 AS flag ", accID,iYear,userID);

                int flag = Convert.ToInt32(cmd.ExecuteScalar());
                //如果是账套主管显示所有仓库
                if (flag == 1)
                {
                    cmd.CommandText = "Select cwhname,cwhcode,bWhPos from Warehouse";
                }

                else
                {
                    ///根据用户名查询权限分配表 查找该用户有哪些仓库可用
                    ///cBusObId业务对象标识 这里为'仓库'

                    cmd.CommandText = string.Format(@"SELECT wh.* FROM 
(Select cwhname,cwhcode,bWhPos from Warehouse) wh
INNER JOIN
(
--查询该用户的仓库权限
SELECT cACCode FROM aa_holdauth WHERE cBusObId='warehouse' AND cUserId='{0}'
UNION ALL
--查询该用户所在角色的仓库权限
SELECT ha.cACCode FROM 
(SELECT cACCode,cUserId FROM dbo.AA_HoldAuth WHERE cBusObId ='warehouse' AND isUserGroup=1 ) ha
INNER JOIN (SELECT cGroup_Id,cUser_Id FROM UFSystem.dbo.UA_Role WHERE cUser_Id='{0}') r ON ha.cUserId = r.cGroup_Id
) temp ON wh.cwhcode =temp.cACCode", userID);
                }
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["Warehouse"]);
                //cmd.CommandText = "select cinvname from inventory where cinvccode='M43'";
                //adp.SelectCommand = cmd;
                //adp.Fill(ds.Tables["packaging"]);
                //cmd.CommandText = "select cinvname from inventory where cinvccode='M41'";
                //adp.SelectCommand = cmd;
                //adp.Fill(ds.Tables["packag"]);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 通过物料编码读取物料信息
        /// </summary>
        static public int GetMaterialInformation(string cinvcode, string cbatch, string connectionString, out DataSet result, out string errMsg)
        {
            errMsg = "";
            result = null;
            string sqlString = "select cinvcode,cinvname,cinvstd from inventory where a.cinvcode='" + cinvcode + "' ";
            //int i = OperationSql.GetDataset(sqlString, connectionString, out result, out errMsg);
            //if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count != 0)
            //{
            //    return i;
            //}
            ////sqlString = "select a.cinvccode,c.dvdate as dmdate,'' as cvenname,'' as cvencode,'' as cbatchproperty6,a.cinvcode,c.cbatch,a.cinvdefine1,a.cinvdefine2,a.cinvdefine4,cinvname from inventory as a left join currentstock as c on a.cinvcode=c.cinvcode where c.cinvcode='"+cinvcode+"' and cbatch='"+cbatch+"'";
            ////result = null;
            ////i = OperationSql.GetDataset(sqlString, connectionString, out result, out errMsg);
            ////if (result.Tables[0].Rows.Count == 0)
            ////{
            ////    errMsg = "请核对输入是否正确";
            ////}
            //return i;
            int flag = -1;
            try
            {
                result = DBHelperSQL.Query(connectionString, sqlString);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        static public int GetMaterial(string invCode, string connectionString, out DataSet result, out string errMsg)
        {
            result = new DataSet();
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            string sqlString = "select cMassUnit,cinvcode,cinvname,cinvstd,d.igrouptype,d.cgroupname,(case when d.igrouptype =1 then b.ichangrate/c.ichangrate else null end) as changrate,a.cComUnitCode,a.cAssComUnitCode,b.ccomunitname,c.ccomunitname as cAssComUnitName,bFixExch from inventory as a left join ComputationUnit as b on b.cComUnitCode=a.cComUnitCode left join ComputationUnit as c on c.cComUnitCode=a.cAssComUnitCode left join ComputationGroup as d on d.cgroupcode=a.cgroupcode where cInvCode='" + invCode + "'";
            //int i = OperationSql.GetDataset(sqlString, connectionString, out result, out errMsg);
            //return i;
            int flag = -1;
            try
            {
                result = DBHelperSQL.Query(connectionString, sqlString);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        //判断货位信息
        static public int GetPosition(string cwhcode, string cposcode, string connectionString, out string errMsg)
        {
            errMsg = "";
            string sqlString = "select count(*) from position where cwhcode='" + cwhcode + "' and cposcode='" + cposcode + "'";
            string str = "";
            //int i = OperationSql.GetString(sqlString, connectionString, out str, out errMsg);
            int flag = -1;
            try
            {
                str = DBHelperSQL.ExecuteScalar(connectionString, sqlString).ToString();
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            if (errMsg != "")
            {
                return -1;
            }
            if (str == "0")
            {
                errMsg = "没有该货位，请核查仓库和货位";
                return -1;
            }
            return flag ;
        }

        /*
        //形态转换获得编码
        public static int GetAssemInventory(string cinvcode, string cbatch, string cwhcode, out DataSet ds, string connectionString, out string errMsg)
        {
            errMsg = "";
            ds = null;
            string sqlString;
            if (cinvcode.Substring(0, 2) == "M1")
            {
                sqlString = "select a.cinvname as oldcinvname,b.cinvcode,b.cinvname,c.iquantity,c.dvdate,c.dmdate,c.imassdate,c.cmassunit,c.iexpiratdatecalcu,c.cexpirationdate,c.dexpirationdate,cbatchproperty6,a.iInvSPrice from currentstock as c left join inventory as a on a.cinvcode=c.cinvcode left join inventory as b on a.ccurrencyname=b.cinvcode  left join rdrecords as d on d.cinvcode=c.cinvcode and d.cbatch=c.cbatch  "
                    //+" left join rdrecord rd on rd.id=d.id and rd.cbustype='普通采购' "
                + " where c.cinvcode='" + cinvcode + "' and c.cwhcode='" + cwhcode + "' and c.cbatch='" + cbatch + "'";
            }
            else
            {
                sqlString = "select a.cinvname as oldcinvname,a.cinvcode,b.cinvname,c.iquantity,c.dvdate,c.dmdate,c.imassdate,c.cmassunit,c.iexpiratdatecalcu,c.cexpirationdate,c.dexpirationdate,a.iInvSPrice from currentstock as c left join inventory as a on a.ccurrencyname=c.cinvcode left join inventory as b on a.ccurrencyname=b.cinvcode where c.cinvcode='" + cinvcode + "' and c.cwhcode='" + cwhcode + "' and c.cbatch='" + cbatch + "'";
            }
            int i = OperationSql.GetDataset(sqlString, connectionString, out ds, out errMsg);
            if (ds.Tables[0].Rows.Count == 0)
            {
                errMsg = "该产品不可做形态转换或该仓库没有该产品信息";
                return -1;
            }
            if (ds.Tables[0].Rows[0]["cinvname"].ToString() == "" || ds.Tables[0].Rows[0]["cinvname"].ToString() == "Null")
            {
                errMsg = "该产品不可做形态转换";
                return -1;
            }
            return i;
        }
        */

        #region getSTOutInvBatch
        //取批号物料信息
        public static int getSTOutInvBatch(string cwhcode, string cinvcode, string cbatch, string connectionString, out DataSet InvBatch, out string errMsg)
        {
            InvBatch = new DataSet();
            errMsg = "";
            string sql;
            sql = "select i.cinvcode,i.cinvname,i.cmassunit,i.iMassDate,dMDate,dvdate,cu_m.cComUnitName ,I.cPosition,dexpirationdate ,cexpirationdate , "
+ " cast((CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0) - IsNull(fStopQuantity,0) END  ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0) - IsNull(fStopQuantity,0) END  END) as decimal(10,2)) fAvailQtty,"
+ " (CASE WHEN iGroupType = 0 THEN 0  WHEN iGroupType = 2 THEN  CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iNum,0) - IsNull(fStopNum,0) END  ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iNum,0) - IsNull(fStopNum,0) END  END WHEN iGroupType = 1 THEN  (CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0) - IsNull(fStopQuantity,0) END  ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0) - IsNull(fStopQuantity,0) END  END)/CU_G.iChangRate ELSE NULL END) AS fAvailNum"

+ " FROM v_CurrentStock CS inner join dbo.Inventory I ON I.cInvCode = CS.cInvCode   "
+ " left join dbo.InventoryClass IC ON IC.cInvCCode = I.cInvCCode LEFT OUTER JOIN dbo.ComputationUnit CU_G ON "
+ " I.cSTComUnitCode =CU_G.cComUnitCode "
+ " LEFT OUTER JOIN dbo.ComputationUnit CU_A ON I.cAssComUnitCode = CU_A.cComunitCode "
+ " LEFT OUTER JOIN dbo.ComputationUnit CU_M ON I.cComUnitCode = CU_M.cComunitCode "
+ " LEFT OUTER JOIN dbo.Warehouse W ON CS.cWhCode = W.cWhCode "
+ " left join vendor v1 on v1.cvencode = cs.cvmivencode "
+ " left join v_aa_enum E1 on E1.enumcode = ISNULL(cs.iExpiratDateCalcu,0) and E1.enumtype=N'SCM.ExpiratDateCalcu' "
+ " LEFT OUTER JOIN dbo.v_aa_enum E with (nolock) on E.enumcode=convert(nchar,CS.cMassUnit) and E.enumType=N'ST.MassUnit' "

                + " Where w.cWhcode=N'" + cwhcode + "' And i.cInvCode =N'" + cinvcode + "'";
            if (cbatch != null && cbatch.Length > 0)
            {
                sql += " And IsNull(CS.cBatch,N'')=N'" + cbatch + "'  ";
            }

            string connString = connectionString;

            errMsg = "";
            SqlCommand view = new SqlCommand();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = sql;
            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(InvBatch);
                if (InvBatch != null && InvBatch.Tables[0].Rows.Count > 0)
                    return 0;
                else
                {
                    InvBatch = null;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        /// <summary>
        /// 根据一维码（69码）获取存货编码
        /// 2012－09－04
        /// </summary>
        /// <param name="cbarcode">69码</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>查到返回true,没有查到返回false</returns>
        public static bool GetCInvCode(string cBarCode, string connectionString,out string cInvCode ,out string errMsg)
        {
            bool flag = false;
            cInvCode= string.Empty;
            errMsg = string.Empty;
            string sqlString = string.Format("select cinvcode from inventory where cbarcode='{0}'",cBarCode);
            DataSet ds = null;
            //int result = OperationSql.GetDataset(sqlString, connectionString, out ds, out errMsg);
            //if (result == 0)//判断是否正确
            //{
            //    ///判断是否查到数据
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        cInvCode = ds.Tables[0].Rows[0][0].ToString();
            //        flag = true;
            //    }
            //}
            //return flag;

            try
            {
                ds = DBHelperSQL.Query(connectionString, sqlString);
                ///判断是否查到数据
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cInvCode = ds.Tables[0].Rows[0][0].ToString();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        /// <summary>
        /// 根据仓库编码获取所有货位及货位存储信息
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-10</remarks>
        public static bool GetPosition(string connectionString,string cWhCode,out DataSet ds_Position,out string errMsg)
        {
            bool flag = false;
            ds_Position = null;
            errMsg = string.Empty;

//            string sqlString = string.Format(@"SELECT p.cPosCode,ip.cInvCode,ip.iQuantity FROM 
//(SELECT cPosCode FROM dbo.Position WHERE cWhCode='{0}') p
//INNER JOIN (SELECT cPosCode,cInvCode,SUM(iQuantity) AS iQuantity FROM dbo.InvPosition WHERE cWhCode='{0}' GROUP BY cPosCode,cInvCode) ip ON p.cPosCode = ip.cPosCode",cWhCode);

            string sqlString = string.Format("SELECT cPosCode,cInvCode,SUM(CASE WHEN bRdFlag = 1 THEN ISNULL(iQuantity,0) ELSE -ISNULL(iQuantity,0) END ) AS iQuantity FROM dbo.InvPosition WHERE cWhCode='{0}' GROUP BY cPosCode,cInvCode", cWhCode);
            //int result = OperationSql.GetDataset(sqlString, connectionString, out ds_Position, out errMsg);
            //if (result == 0)//判断是否正确
            //{
            //    flag = true;
            //}
            //return flag;
            try
            {
                ds_Position = DBHelperSQL.Query(connectionString, sqlString);
                flag = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }


        /// <summary>
        /// 货位现存量查询
        /// </summary>
        /// <param name="cBarCode">69码</param>
        /// <param name="isPosition">标识是否是货位存量查询还是现存量查询</param>
        /// <param name="cWhCode">仓库编码集合</param>
        /// <param name="connectionString">链接字符串</param>
        /// <param name="ds">结果</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-10-29</remarks>
        public static int QuantitySerarch(string cBarCode, bool isPosition, List<string> cWhCode, string connectionString, out DataSet ds, out string errMsg)
        {
            int result = 0;
            ds = null;
            errMsg = string.Empty;
            //sql语句
            string SQL = string.Empty;
            ///加上仓库条件
            string whcondition = " AND W.cWhCode IN (";
            foreach (string code in cWhCode)
            {
                whcondition += string.Format("'{0}',", code);
            }
            //去掉最后一个,加上)
            whcondition = whcondition.Substring(0, whcondition.Length - 1);
            whcondition = whcondition + ")";

            if (!isPosition)//现存量查询
            {
                SQL = string.Format(@"SELECT null as cPosCode,  CS.cExpirationdate,E1.enumname as 有效期推算方式,W.cWhCode, W.cWhName, I.cInvCode, I.cInvAddCode,  I.cInvName, I.cInvStd, I.cInvCCode , IC.cInvCName, 
 CU_M.cComUnitName AS cInvM_Unit, CASE WHEN I.iGroupType = 0 THEN NULL  WHEN I.iGrouptype = 2 THEN CU_A.cComUnitName  WHEN I.iGrouptype = 1 THEN CU_G.cComUnitName END  AS cInvA_Unit,convert(nvarchar(38),convert(decimal(38,0),CASE WHEN I.iGroupType = 0 THEN NULL      WHEN I.iGroupType = 2 THEN (CASE WHEN CS.iQuantity = 0.0 OR CS.iNum = 0.0 THEN NULL ELSE CS.iQuantity/CS.iNum END)      WHEN I.iGroupType = 1 THEN CU_G.iChangRate END)) AS iExchRate,
i.cInvDefine1,i.cInvDefine2,i.cInvDefine3,i.cInvDefine4,i.cInvDefine5,i.cInvDefine6,i.cInvDefine7, Null as cFree1, Null as cFree2, Null as cFree3, Null as cFree4, Null as cFree5, Null as cFree6, Null as cFree7, Null as cFree8, Null as cFree9, Null as cFree10, Null as cInvDefine8, Null as cInvDefine9, Null as cInvDefine10, Null as cInvDefine11, Null as cInvDefine12, Null as cInvDefine13, Null as cInvDefine14, Null as cInvDefine15, Null as cInvDefine16,cs.cBatch, cs.EnumName As iSoTypeName, cs.csocode as SOCode, convert(nvarchar,cs.isoseq) as iRowNo,
cs.cvmivencode,v1.cvenabbname as cvmivenname , isnull(E.enumname,N'') as cMassUnitName,CS.dVDate, CS.dMdate,convert(varchar(20),CS.iMassDate) as iMassDate,
 iQuantity,( CASE WHEN iGroupType = 0 THEN 0 WHEN iGroupType = 2 THEN ISNULL(iNum,0) WHEN iGroupType = 1 THEN iQuantity/ CU_G.iChangRate END) AS iNum,
  CASE WHEN CS.bStopFlag = 1 OR CS.bGspStop = 1 THEN iQuantity ELSE IsNull(fStopQuantity,0) END AS iStopQtty,
  CASE WHEN CS.bStopFlag = 1 OR CS.bGspStop = 1 THEN (CASE WHEN iGroupType = 0 THEN 0 WHEN iGroupType = 2 THEN ISNULL(iNum,0) WHEN iGroupType = 1 THEN iQuantity/ CU_G.iChangRate END) 
 ELSE (CASE WHEN iGroupType = 0 THEN 0 WHEN iGroupType = 2 THEN ISNULL(fStopNum,0) WHEN iGroupType = 1 THEN fStopQuantity/ CU_G.iChangRate END) END AS iStopNum,
 (fInQuantity) AS fInQtty, 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fInNum,0) WHEN iGroupType = 1 THEN fInQuantity/ CU_G.iChangRate END) AS fInNum,
 (fTransInQuantity) AS fTransInQtty,
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fTransInNum,0) WHEN iGroupType = 1 THEN fTransInQuantity/ CU_G.iChangRate END) AS fTransInNum,
 (ISNULL(fInQuantity,0) + ISNULL(fTransInQuantity,0)) AS fInQttySum,
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fInNum,0) + ISNULL(fTransInNum,0) WHEN iGroupType = 1 THEN (ISNULL(fInQuantity,0) + ISNULL(fTransInNum,0))/ CU_G.iChangRate END) AS fInNumSum,
 (fOutQuantity) AS fOutQtty, 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fOutNum,0) WHEN iGroupType = 1 THEN fOutQuantity/ CU_G.iChangRate END) AS fOutNum,
 CS.cBatchProperty1,CS.cBatchProperty2,CS.cBatchProperty3,CS.cBatchProperty4,CS.cBatchProperty5,CS.cBatchProperty6,CS.cBatchProperty7,CS.cBatchProperty8,CS.cBatchProperty9,CS.cBatchProperty10,
 (fTransOutQuantity) AS fTransOutQtty, 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fTransOutNum,0) WHEN iGroupType = 1 THEN fTransOutQuantity/ CU_G.iChangRate END) AS fTransOutNum,
 (ISNULL(fOutQuantity,0) + ISNULL(fTransOutQuantity,0)) AS fOutQttySum , 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fOutNum,0) + ISNULL(fTransOutNum,0) WHEN iGroupType = 1 THEN (ISNULL(fOutQuantity,0) + ISNULL(fTransOutNum,0))/ CU_G.iChangRate END) AS fOutNumSum,
 (fDisableQuantity) AS fDisableQtty, 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(fDisableNum,0) WHEN iGroupType = 1 THEN fDisableQuantity/ CU_G.iChangRate END) AS fDisableNum,
 (ipeqty) AS fpeqty, 
 (CASE WHEN iGroupType = 0 THEN NULL WHEN iGroupType=2 THEN ISNULL(ipenum,0) WHEN iGroupType = 1 THEN ipeqty/ CU_G.iChangRate END) AS fpenum,
 (CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0)- IsNull(fStopQuantity,0) END  - ISNULL(fOutQuantity,0) ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0)- IsNull(fStopQuantity,0) END  - ISNULL(fOutQuantity,0) END) AS fAvailQtty,dLastCheckDate, 
 (CASE WHEN iGroupType = 0 THEN 0  WHEN iGroupType = 2 THEN  CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iNum,0)- IsNull(fStopNum,0) END  - ISNULL(fOutNum,0) ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iNum,0)- IsNull(fStopNum,0) END  - ISNULL(fOutNum,0) END WHEN iGroupType = 1 THEN  (CASE WHEN bInvBatch=1 THEN  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0)- IsNull(fStopQuantity,0) END  - ISNULL(fOutQuantity,0) ELSE  CASE WHEN bStopFlag =1 OR bGSPStop= 1 THEN 0 ELSE ISNULL(iQuantity,0)- IsNull(fStopQuantity,0) END  - ISNULL(fOutQuantity,0) END)/CU_G.iChangRate ELSE NULL END) AS fAvailNum
 FROM v_ST_currentstockForReport  CS inner join dbo.Inventory I ON I.cInvCode = CS.cInvCode   
 left join dbo.InventoryClass IC ON IC.cInvCCode = I.cInvCCode LEFT OUTER JOIN dbo.ComputationUnit CU_G ON 
 I.cSTComUnitCode =CU_G.cComUnitCode 
 LEFT OUTER JOIN dbo.ComputationUnit CU_A ON I.cAssComUnitCode = CU_A.cComunitCode 
 LEFT OUTER JOIN dbo.ComputationUnit CU_M ON I.cComUnitCode = CU_M.cComunitCode 
 LEFT OUTER JOIN dbo.Warehouse W ON CS.cWhCode = W.cWhCode 
 left join vendor v1 on v1.cvencode = cs.cvmivencode 
 left join v_aa_enum E1 on E1.enumcode = ISNULL(cs.iExpiratDateCalcu,0) and E1.enumtype=N'SCM.ExpiratDateCalcu' 
 LEFT OUTER JOIN dbo.v_aa_enum E with (nolock) on E.enumcode=convert(nchar,CS.cMassUnit) and E.enumType=N'ST.MassUnit' 
 WHERE I.cBarCode ='{0}' AND iQuantity<>0 {1}", cBarCode, whcondition);
            }
            else//货位查询
            {
                SQL = string.Format(@"
--临时表a
if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempa') AND xtype='U')
DROP TABLE #tempa
SELECT DISTINCT CS.AutoID, CS.RdsID, CS.RdID, CS.cWhCode,W.cWhName, CS.cPosCode AS cPosCode, 
 Position.cPosName AS cPosName, CS.cInvCode, CS.cBatch,isnull(cs.cvmivencode,N'') as cvmivencode,v1.cvenabbname as cvmivenname ,
 CS.dMadeDate AS dMDate, CS.iMassDate AS iMassDate, 
 CS.cMassUnit, isnull(E.enumname,N'') AS cMassUnitName, CS.dVDate, CS.iQuantity, CS.iNum, CS.cMemo, CS.cHandler,CS.dDate, CS.bRdFlag, CS.cSource, 
 CS.cFree1, CS.cFree2, CS.cFree3, CS.cFree4, CS.cFree5, CS.cFree6,CS.cFree7, CS.cFree8, CS.cFree9, CS.cFree10, 
Batch.cBatchProperty1,Batch.cBatchProperty2,Batch.cBatchProperty3,Batch.cBatchProperty4,Batch.cBatchProperty5,Batch.cBatchProperty6,Batch.cBatchProperty7,Batch.cBatchProperty8,Batch.cBatchProperty9,Batch.cBatchProperty10,v2.enumname as 有效期推算方式,CS.cExpirationdate as 有效期至, CS.cAssUnit, CS.cBVencode,I.cInvAddCode, I.cInvName,I.cInvStd,  I.cInvDefine1,I.cInvDefine2,I.cInvDefine3,I.cInvDefine4,I.cInvDefine5,I.cInvDefine6,I.cInvDefine7,I.cInvDefine8,I.cInvDefine9,I.cInvDefine10,  I.cInvDefine11,I.cInvDefine12,I.cInvDefine13,I.cInvDefine14,I.cInvDefine15,I.cInvDefine16,CASE WHEN I.iGroupType = 0 THEN NULL 
     WHEN I.iGroupType = 2 THEN (CASE WHEN CS.iQuantity = 0.0 OR CS.iNum = 0.0 THEN NULL ELSE CS.iQuantity/CS.iNum END) 
     WHEN I.iGroupType = 1 THEN CU_G.iChangRate END AS iExchRate
, I.cInvCCode AS cInvCCode, I.iGroupType, CU_M.cComUnitName AS cInvM_Unit, CASE WHEN I.iGroupType = 0 THEN NULL 
 WHEN I.iGrouptype = 2 THEN CU_A.cComUnitName 
 WHEN I.iGrouptype = 1 THEN CU_G.cComUnitName END 
 AS cInvA_Unit, CU_G.iChangRate, 
 InventoryClass.cInvCName AS cInvCName
 INTO #tempa FROM Warehouse W with (nolock) RIGHT OUTER JOIN dbo.InvPosition CS  with (nolock) ON W.cWhCode = CS.cWhCode LEFT OUTER JOIN ComputationUnit CU_A RIGHT OUTER JOIN  dbo.Inventory I ON CU_A.cComunitCode = I.cAssComUnitCode LEFT OUTER JOIN dbo.ComputationUnit CU_M ON I.cComUnitCode = CU_M.cComunitCode  LEFT OUTER JOIN ComputationUnit CU_G ON  I.cSTComUnitCode = CU_G.cComUnitCode  ON CS.cInvCode = I.cInvCode 
 LEFT JOIN Position ON CS.cPosCode=Position.cPosCode LEFT JOIN InventoryClass ON InventoryClass.cInvCCode=I.cInvCCode 
 LEFT OUTER JOIN v_aa_enum E with (nolock) ON E.EnumCode=convert(nvarchar,CS.cMassUnit) and E.enumType=N'ST.MassUnit'  left join vendor v1 on cs.cvmivencode = v1.cvencode  left join v_aa_enum v2 on v2.enumcode=ISNULL(CS.iExpiratDateCalcu,0) and v2.enumtype=N'SCM.ExpiratDateCalcu' 
 left join V_ST_AA_BatchProperty batch on Batch.cbinvcode=CS.cinvcode and isnull(Batch.cbbatch,N'')=isnull(CS.cbatch,N'') and isnull(Batch.cbfree1,N'')=isnull(CS.cfree1,N'') and isnull(Batch.cbfree2,N'')=isnull(CS.cfree2,N'') and isnull(Batch.cbfree3,N'')=isnull(CS.cfree3,N'') and isnull(Batch.cbfree4,N'')=isnull(CS.cfree4,N'') and isnull(Batch.cbfree5,N'')=isnull(CS.cfree5,N'') and isnull(Batch.cbfree6,N'')=isnull(CS.cfree6,N'') and isnull(Batch.cbfree7,N'')=isnull(CS.cfree7,N'') and isnull(Batch.cbfree8,N'')=isnull(CS.cfree8,N'') and isnull(Batch.cbfree9,N'')=isnull(CS.cfree9,N'') and isnull(Batch.cbfree10,N'')=isnull(CS.cfree10,N'')
 WHERE I.cBarCode = '{0}' {1}

--临时表b
if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempb') AND xtype='U')
DROP TABLE #tempb

SELECT cWhCode,cWhName,cInvCode,cInvAddCode,cInvName,cInvStd,cInvCCode,cInvCName,cInvM_Unit,cInvA_Unit,cBatch,cvmivencode,cvmivenname,dMdate,iMassDate,cMassUnitName,dVDate,cPosCode,cPosName,有效期推算方式,有效期至,cFree1 , cFree2, cFree3, cFree4, cFree5, cFree6, cFree7, cFree8, cFree9, cFree10,  cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cBatchProperty1,cBatchProperty2,cBatchProperty3,cBatchProperty4,cBatchProperty5,cBatchProperty6,cBatchProperty7,cBatchProperty8,cBatchProperty9,cBatchProperty10, cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,(CASE WHEN iGroupType = 0 THEN NULL 
     WHEN iGroupType = 1 THEN AVG(iChangRate) 
     WHEN iGroupType = 2 THEN CASE WHEN ROUND(SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END),6) <> 0 THEN SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iQuantity,0) ELSE -ISNULL(iQuantity,0) END)/SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END) ELSE NULL END 
 ELSE NULL END) 
 as iExchRate,
 Round(SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iQuantity,0) ELSE -ISNULL(iQuantity,0) END ),6) AS iQtty, 
 Round(SUM( CASE WHEN iGroupType = 0 THEN 0 WHEN iGroupType = 2 THEN (CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END) 
                                      WHEN iGroupType = 1 THEN (CASE WHEN CS.bRdFlag = 1 THEN iQuantity/ iChangRate ELSE -iQuantity/ iChangRate END) END),6) AS iNum
 INTO #tempb FROM #tempa AS CS 
 GROUP BY cWhCode,cWhName,cInvCode,cInvAddCode,cInvName,cInvStd,cInvCCode,cInvCName,cInvM_Unit,cInvA_Unit,cBatch,cvmivencode,cvmivenname,dMdate,iMassDate,cMassUnitName,dVDate,cPosCode,cPosName,有效期推算方式,有效期至,cFree1 , cFree2, cFree3, cFree4, cFree5, cFree6, cFree7, cFree8, cFree9, cFree10,  cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cBatchProperty1,cBatchProperty2,cBatchProperty3,cBatchProperty4,cBatchProperty5,cBatchProperty6,cBatchProperty7,cBatchProperty8,cBatchProperty9,cBatchProperty10, cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,iGroupType
 
 --临时表c
 if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempc') AND xtype='U')
DROP TABLE #tempc
 SELECT [cWhCode],[cWhName] as [cWhName],[cPosCode],[cInvCode],[cInvName] as [cInvName],[cInvStd] as [cInvStd],[cInvAddCode] as [cInvAddCode],[cInvCCode] as [cInvCCode],[cInvCName] as [cInvCName],[cInvA_Unit] as [cInvA_Unit],round([iExchRate],6) as [iExchRate],[cInvDefine2] as [cInvDefine2],[cInvDefine3] as [cInvDefine3],[cInvDefine4] as [cInvDefine4],[cInvDefine5] as [cInvDefine5],[cInvDefine6] as [cInvDefine6],[cInvDefine7] as [cInvDefine7],[cInvDefine8] as [cInvDefine8],[cInvDefine9] as [cInvDefine9],[cInvDefine10] as [cInvDefine10],round([cInvDefine11],2) as [cInvDefine11],round([cInvDefine12],2) as [cInvDefine12],round([cInvDefine13],2) as [cInvDefine13],round([cInvDefine14],2) as [cInvDefine14],[cInvDefine15] as [cInvDefine15],[cInvDefine16] as [cInvDefine16],[cFree1] as [cFree1],[cFree2] as [cFree2],[cFree3] as [cFree3],[cFree4] as [cFree4],[cFree5] as [cFree5],[cFree6] as [cFree6],[cFree7] as [cFree7],[cFree8] as [cFree8],[cFree9] as [cFree9],[cFree10] as [cFree10],[cBatch] as [cBatch],[cvmivencode] as [cvmivencode],round([iNum],2) as [iNum],[cvmivenname] as [cvmivenname],round([iQtty],4) as [iQtty],[cInvM_Unit] as [cInvM_Unit],[dMdate] as [dMdate],[dVDate] as [dVDate],[有效期至] as [有效期至],[cPosName] as [cPosName],[有效期推算方式] as [有效期推算方式],[iMassDate] as [iMassDate],[cMassUnitName] as [cMassUnitName],[cInvDefine1] as [cInvDefine1],[cBatchProperty1],[cBatchProperty2],[cBatchProperty3],[cBatchProperty4],[cBatchProperty5],[cBatchProperty6],[cBatchProperty7],[cBatchProperty8],[cBatchProperty9],[cBatchProperty10] Into #tempc from #tempb
 
SELECT [cWhCode],max([cWhName]) as [cWhName],[cPosCode],[cInvCode],max([cInvName]) as [cInvName],max([cInvStd]) as [cInvStd],max([cInvAddCode]) as [cInvAddCode],max([cInvCCode]) as [cInvCCode],max([cInvCName]) as [cInvCName],max([cInvA_Unit]) as [cInvA_Unit],sum(round([iExchRate],6)) as [iExchRate],max([cInvDefine2]) as [cInvDefine2],max([cInvDefine3]) as [cInvDefine3],max([cInvDefine4]) as [cInvDefine4],max([cInvDefine5]) as [cInvDefine5],max([cInvDefine6]) as [cInvDefine6],max([cInvDefine7]) as [cInvDefine7],max([cInvDefine8]) as [cInvDefine8],max([cInvDefine9]) as [cInvDefine9],max([cInvDefine10]) as [cInvDefine10],sum(round([cInvDefine11],2)) as [cInvDefine11],sum(round([cInvDefine12],2)) as [cInvDefine12],sum(round([cInvDefine13],2)) as [cInvDefine13],sum(round([cInvDefine14],2)) as [cInvDefine14],max([cInvDefine15]) as [cInvDefine15],max([cInvDefine16]) as [cInvDefine16],max([cFree1]) as [cFree1],max([cFree2]) as [cFree2],max([cFree3]) as [cFree3],max([cFree4]) as [cFree4],max([cFree5]) as [cFree5],max([cFree6]) as [cFree6],max([cFree7]) as [cFree7],max([cFree8]) as [cFree8],max([cFree9]) as [cFree9],max([cFree10]) as [cFree10],max([cBatch]) as [cBatch],max([cvmivencode]) as [cvmivencode],sum(round([iNum],2)) as [iNum],max([cvmivenname]) as [cvmivenname],sum(round([iQtty],4)) as [iQuantity],max([cInvM_Unit]) as [cInvM_Unit],max([dMdate]) as [dMdate],max([dVDate]) as [dVDate],max([有效期至]) as cExpirationdate,max([cPosName]) as [cPosName],max([有效期推算方式]) as [有效期推算方式],sum([iMassDate]) as [iMassDate],max([cMassUnitName]) as [cMassUnitName],max([cInvDefine1]) as [cInvDefine1] 
FROM #tempc GROUP BY [cWhCode], [cPosCode], [cInvCode]", cBarCode, whcondition);
            }

            //result = OperationSql.GetDataset(SQL, connectionString, out ds, out errMsg);
            //return result;

            int flag = -1;
            try
            {
                ds = DBHelperSQL.Query(connectionString, SQL );
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        /// <summary>
        /// 根据用户ID查询用户操作权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="connectionString"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int Competence(string cUser_Id, string cAcc_Id, string iYear, string connectionString, out DataSet ds, out string errMsg)
        {
            string SQL = string.Empty;
            errMsg = string.Empty;
            ds = null;
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            SQL = string.Format(@"
--判断是否为账套主管:一个为账套主管组，一个为功能编码admin,功能名称：账套主管
SELECT CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='admin' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id",cUser_Id,iYear,cAcc_Id);
            cmd.CommandText = SQL;
            //如果为1，说明为账套主管
            if (cmd.ExecuteScalar().ToString() == "1")
            {
                SQL = @"SELECT 1 AS CGDH,1 AS CGRK,1 AS CGTHGSP,1 AS XSFH,1 AS XSCKGSP,1 AS XSTHGSP,1 AS XSCK,1 AS CCPRK,1 AS PD,1 AS CLCK,1 AS WWDH,1 AS QTCK,1 AS QTRK,1 AS HWGL";
                //result = OperationSql.GetDataset(SQL, connectionString, out ds, out errMsg);
                
                try
                {
                    ds = DBHelperSQL.Query(connectionString, SQL);
                    result = 0;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    result = -1;
                }
            }
            //否则为普通操作人员
            else
            {
                /*
                 * 
                SQL = string.Format(@"
--普通操作人员 
DECLARE 
@CGDH INT,--采购到货(PU04200102:到货单录入,PU04200105:到货单审核)
@CGRK INT,--采购入库（包括红字）(ASM0102:采购入库单录入,ASM0103:采购入库单审核)
@CGTHGSP INT,--采购退货GSP(GS03040102:采购退货出库质量复核记录单录入,GS03040103:采购退货出库质量复核记录单审核)
@XSFH INT,--销售(发货)出库拣货(SA03020101:发货单录入|SA03040101:委托代销发货单录入)
@XSCKGSP INT,--销售出库GSP(GS03030102:销售出库质量复核记录单录入,GS03030103:销售出库质量复核记录单审核|GS03030202:中药材、饮片销售出库质量复核记录单录入,GS03030203:中药材、饮片销售出库质量复核记录单审核)
@XSTHGSP INT,--销售退货GSP(GS03020102:销售退货质量验收记录单录入,GS03020103:销售退货质量验收记录单审核)
@XSCK INT,--销售出库(红字)(ASM0202:销售出库单录入,ASM0203:销售出库单审核)
@CCPRK INT,--产成品入库(ASM0302:产成品入库单录入,ASM0303:产成品入库单审核)
@PD INT,--盘点(ST010202:盘点单录入)
@CLCK INT,--材料出库(ASM0402:材料出库单录入,ASM0403:材料出库单审核)
@WWDH INT,--委外到货(OM04200102:(委外)到货单录入,OM04200105:(委外)到货单审核)
@QTCK INT,--其它出库(ASM0603:其他出库单审核)
@QTRK INT ,--其它入库(ASM0503:其他入库单审核)
@HWGL INT	--货位管理(ASM0202:销售出库单录入)

--采购到货
SELECT @CGDH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ('PU04200102' ,'PU04200105') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id

--采购入库（包括红字）
SELECT @CGRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('ASM0102','ASM0103') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id

--采购退货GSP
SELECT @CGTHGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ('GS03040102','GS03040103') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售(发货)出库拣货
SELECT @XSFH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='SA03020101' OR cAuth_Id='SA03040101' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售出库GSP
SELECT @XSCKGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN 
(SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('GS03030102' ,'GS03030103')AND iYear='{1}' AND cAcc_Id='{2}'  GROUP BY cUser_Id HAVING COUNT(1)=2 
UNION ALL
SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('GS03030202' ,'GS03030203')AND iYear='{1}' AND cAcc_Id='{2}'  GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售退货GSP
SELECT @XSTHGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ('GS03020102','GS03020103') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售出库(红字)
SELECT @XSCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('ASM0202' ,'ASM0203') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--产成品入库
SELECT @CCPRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ('ASM0302' ,'ASM0303') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--盘点
SELECT @PD = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ST010202' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--材料出库
SELECT @CLCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ('ASM0402' ,'ASM0403') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--委外到货
SELECT @WWDH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN('OM04200102' ,'OM04200105') AND iYear='{1}' AND cAcc_Id='{2}' GROUP BY cUser_Id HAVING COUNT(1)=2 ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--其它出库
SELECT @QTCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0603' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--其它入库
SELECT @QTRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0503' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--货位管理
SELECT @HWGL = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0202' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id


SELECT @CGDH AS CGDH,@CGRK AS CGRK,@CGTHGSP AS CGTHGSP,@XSFH AS XSFH,@XSCKGSP AS XSCKGSP,@XSTHGSP AS XSTHGSP,@XSCK AS XSCK,
@CCPRK AS CCPRK,@PD AS PD,@CLCK AS CLCK,@WWDH AS WWDH,@QTCK AS QTCK,@QTRK AS QTRK,@HWGL AS HWGL", cUser_Id, iYear, cAcc_Id);

                 * 
                 */
                //2013-12-10修改去掉审核
                SQL = string.Format(@"
--普通操作人员 
DECLARE 
@CGDH INT,--采购到货(PU04200102:到货单录入,PU04200105:到货单审核)
@CGRK INT,--采购入库（包括红字）(ASM0102:采购入库单录入,ASM0103:采购入库单审核)
@CGTHGSP INT,--采购退货GSP(GS03040102:采购退货出库质量复核记录单录入,GS03040103:采购退货出库质量复核记录单审核)
@XSFH INT,--销售(发货)出库拣货(SA03020101:发货单录入|SA03040101:委托代销发货单录入)
@XSCKGSP INT,--销售出库GSP(GS03030102:销售出库质量复核记录单录入,GS03030103:销售出库质量复核记录单审核|GS03030202:中药材、饮片销售出库质量复核记录单录入,GS03030203:中药材、饮片销售出库质量复核记录单审核)
@XSTHGSP INT,--销售退货GSP(GS03020102:销售退货质量验收记录单录入,GS03020103:销售退货质量验收记录单审核)
@XSCK INT,--销售出库(红字)(ASM0202:销售出库单录入,ASM0203:销售出库单审核)
@CCPRK INT,--产成品入库(ASM0302:产成品入库单录入,ASM0303:产成品入库单审核)
@PD INT,--盘点(ST010202:盘点单录入)
@CLCK INT,--材料出库(ASM0402:材料出库单录入,ASM0403:材料出库单审核)
@WWDH INT,--委外到货(OM04200102:(委外)到货单录入,OM04200105:(委外)到货单审核)
@QTCK INT,--其它出库(ASM0603:其他出库单审核)
@QTRK INT ,--其它入库(ASM0503:其他入库单审核)
@HWGL INT	--货位管理(ASM0202:销售出库单录入)

--采购到货
SELECT @CGDH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id ='PU04200102' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id

--采购入库（包括红字）
SELECT @CGRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ='ASM0102' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id

--采购退货GSP
SELECT @CGTHGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id IN ='GS03040102' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售(发货)出库拣货
SELECT @XSFH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='SA03020101' OR cAuth_Id='SA03040101' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售出库GSP
SELECT @XSCKGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN 
(SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id ='GS03030102' AND iYear='{1}' AND cAcc_Id='{2}' 
UNION ALL
SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id ='GS03030202' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售退货GSP
SELECT @XSTHGSP = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id = 'GS03020102' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--销售出库(红字)
SELECT @XSCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id ='ASM0202' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--产成品入库
SELECT @CCPRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0302' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--盘点
SELECT @PD = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ST010202' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--材料出库
SELECT @CLCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id = 'ASM0402' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--委外到货
SELECT @WWDH = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT cUser_Id FROM UFSystem..UA_HoldAuth WHERE cAuth_Id ='OM04200102' AND iYear='{1}' AND cAcc_Id='{2}' ) ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--其它出库
SELECT @QTCK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0603' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--其它入库
SELECT @QTRK = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0503' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id
--货位管理
SELECT @HWGL = CASE WHEN COUNT(1)>0 THEN 1 ELSE 0 END  FROM 
(SELECT * FROM UFSystem..UA_Role WHERE cUser_Id='{0}' ) r
INNER JOIN (SELECT * FROM UFSystem..UA_HoldAuth WHERE cAuth_Id='ASM0202' AND iYear='{1}' AND cAcc_Id='{2}') ha ON ha.cUser_Id= r.cUser_Id OR ha.cUser_Id = r.cGroup_Id


SELECT @CGDH AS CGDH,@CGRK AS CGRK,@CGTHGSP AS CGTHGSP,@XSFH AS XSFH,@XSCKGSP AS XSCKGSP,@XSTHGSP AS XSTHGSP,@XSCK AS XSCK,
@CCPRK AS CCPRK,@PD AS PD,@CLCK AS CLCK,@WWDH AS WWDH,@QTCK AS QTCK,@QTRK AS QTRK,@HWGL AS HWGL", cUser_Id, iYear, cAcc_Id);


                //result = OperationSql.GetDataset(SQL, connectionString, out ds, out errMsg);
                try
                {
                    ds = DBHelperSQL.Query(connectionString, SQL);
                    result = 0;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    result = -1;
                }
            }
            return result;
        }




        #region 查询账套信息

        /// <summary>
        /// 查询所有账套信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-05-08</remarks>
        public static DataTable GetUAAccountInfo(string connectionString, string user_Id)
        {
            DataSet ds = null;

            string strSql = string.Empty;
            if (user_Id == null)
            {
                strSql = string.Format(@"select  cAcc_ID as code,cAcc_name as name ,cUnitAbbre as Abbre,'' as industrytype from ufsystem..ua_account with (nolock)");
            }
            else
            {
                strSql = string.Format(@"
select  cAcc_ID as code,cAcc_name as name ,cUnitAbbre as Abbre,'' as industrytype 
from ufsystem..ua_account with (nolock) 
where cAcc_id IN
	(
		--查询用户名为‘gq’的账套号
		select cacc_id from ufsystem..ua_holdauth with (nolock) where cuser_id=N'{0}' and iisuser=1 group by cacc_id 
		Union All  
		--查询用户名为‘gq’所在组的账套号
		select  cacc_id from ufsystem..ua_holdauth with (nolock) where cUser_id in(select  distinct cgroup_id from ufsystem..ua_role with (nolock) where cUser_id=N'{0}' ) and iIsuser=0 group by cacc_id 
	)
order by cacc_id", user_Id);
            }
            string errMsg;
            try
            {
                ds = DBHelperSQL.Query(connectionString, strSql);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return null;
        }

        #endregion
    }
}
