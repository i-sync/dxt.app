using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace U8DataAccess
{
    public class CheckVouchProcess
    {
        #region 获取盘点单列表
        public static int getCVcodeList(string connectionString, out DataSet ds, out string errMsg)
        {
            errMsg = string.Empty;
            ds = null;
            //string sql = @"select cCVCode, cWhName   from CheckVouch c left join Warehouse w on w.cWhCode = c.cWhCode  where dveridate is null";
            string sql = "SELECT cCVCode ,cWhCode FROM dbo.CheckVouch WHERE dveridate IS NULL";

            try
            {
                ds = DBHelperSQL.Query(connectionString, sql);

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                errMsg = "没有查询到盘点单";
                return -1;
            }
            return 0;
        }
        #endregion

        #region getQtyByBarcode
        /// <summary>
        /// 根据存货编码，盘点单，批次信息查询盘点单详细
        /// </summary>
        /// <param name="barcode">存货编码</param>
        /// <param name="cCVCode">盘点单</param>
        /// <param name="cCVBatch">批次</param>
        /// <param name="connectionString">链接字符串</param>
        /// <param name="qty"></param>
        /// <param name="sqty"></param>
        /// <param name="autoid"></param>
        /// <param name="cinvname">存货名称</param>
        /// <param name="cinvdefine1">生产厂家</param>
        /// <param name="cinvstd">规格</param>
        /// <param name="cinvdefine6">产地</param>
        /// <returns></returns>
        public static int getQtyByBarcode(string barcode, string cCVCode, ref string cCVBatch, string connectionString,
            out string qty, out string sqty, out string autoid, out string cinvname, out string cinvdefine1,out string cinvstd,out string cinvdefine6, out string errMsg)
        {
            string sql = "";
            DataSet ds = null;
            errMsg = string.Empty;
            qty = "";
            sqty = "";
            autoid = "";
            cinvname = "";
            cinvdefine1 = "";
            cinvstd = "";
            cinvdefine6 = "";

            sql = "select a.iCVQuantity,a.autoid,a.ccvbatch,i.cinvname,i.cinvdefine1,i.cInvStd,i.cinvdefine6  from checkvouchs a "
                + " join inventory i on i.cinvcode=a.cinvcode "
                + "join checkvouch b on b.id=a.id and b.cCVCode='" + cCVCode + "' "
                + " and a.cinvcode='" + barcode + "'";
            if(!cCVBatch.Equals(""))
            {
                sql = sql + " and a.cCVBatch = '" + cCVBatch + "'";
            }

            //OperationSql.GetDataset(sql, connectionString, out ds, out errMsg);
            ds = DBHelperSQL.Query(connectionString, sql);
            if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                string _invname = null;
                sql = "select cinvname from inventory where cinvcode='" + barcode + "'";
                //OperationSql.GetString(sql, connectionString, out _invname, out errMsg);
                _invname = DBHelperSQL.ExecuteScalar(connectionString, sql).ToString();
                if (_invname == null || _invname.Length < 1)
                {
                    errMsg = "该产品条码有误!";
                    return -1;
                }
                sqty = "0";
                autoid = "0";
                cinvname = _invname;
                errMsg = "该产品不属于该盘点单!";
                return -1;
            }
            sqty = ds.Tables[0].Rows[0][0].ToString();
            autoid = ds.Tables[0].Rows[0][1].ToString();
            cCVBatch = ds.Tables[0].Rows[0][2].ToString();
            cinvname = ds.Tables[0].Rows[0][3].ToString();
            cinvdefine1 = ds.Tables[0].Rows[0][4].ToString();//生产厂家
            cinvstd = ds.Tables[0].Rows[0][5].ToString();//批次
            cinvdefine6 = ds.Tables[0].Rows[0][6].ToString();//产地
            return 0;
        }

        /// <summary>
        /// 根据货位及存货编码获取盘点单子表数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cCVCode">盘点单</param>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cPosition">货位</param>
        /// <param name="cBatch">批次</param>
        /// <param name="ds">查询结果列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int GetQtyByCode(string connectionString,string cCVCode,string cInvCode,string cPosition,string cBatch, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = string.Empty;
            ///查询条件
            string sqlCon = string.Empty;
            //判断是否有货位管理
            if(!string.IsNullOrEmpty(cPosition))
            {
                sqlCon+=string.Format(" AND a.cPosition='{0}'",cPosition);
            }
            //判断是否按批次查询
            if (!string.IsNullOrEmpty(cBatch))
            {
                sqlCon += string.Format(" AND a.cCVBatch='{0}'",cBatch);
            }
            string SQL = string.Format(@"SELECT i.cInvCode, i.cInvName,i.cInvStd,i.cinvdefine1,i.cinvdefine6,c.cComUnitName,a.cCVBatch,a.cPosition,a.iCVQuantity,a.dMadeDate,a.cExpirationdate,a.dDisDate 
	FROM dbo.CheckVouchs a              
	join inventory i on i.cinvcode=a.cinvcode 
	LEFT JOIN ComputationUnit c ON i.cComUnitCode= c.cComunitCode
	join checkvouch b on b.id=a.id 
	WHERE b.cCVCode='{0}' and a.cinvcode='{1}' {2}",cCVCode,cInvCode,sqlCon);

            //return OperationSql.GetDataset(SQL, connectionString, out ds, out errMsg);
            int flag = -1;
            try
            {
                ds = DBHelperSQL.Query(connectionString, SQL);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
            
        }
        #endregion

        #region 提交盘点

        /// <summary>
        /// 提交盘点单
        /// </summary>
        /// <param name="cCVCode">盘点单号</param>
        /// <param name="list">盘点列表</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SubmitCheckVouchs(string cCVCode, List<CheckDetail> list, string connectionString, out string errMsg)
        {
            errMsg = "";
            string sql = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            try
            {
                string strCon;
                //循环所有盘点数据
                foreach (CheckDetail cd in list)
                {
                    strCon = string.Empty;
                    //判断是否有货位
                    if (!string.IsNullOrEmpty(cd.cPosition))
                    {
                        strCon += string.Format(" AND a.cPosition ='{0}'", cd.cPosition);
                    }
                    //判断是否有批次
                    if (!string.IsNullOrEmpty(cd.cbatch))
                    {
                        strCon += string.Format(" AND a.cCVBatch ='{0}'", cd.cbatch);
                    }
                    sql = string.Format(@"UPDATE a SET iCVCQuantity={0} 
FROM  dbo.CheckVouchs a 
INNER JOIN  dbo.CheckVouch b ON a.ID=b.ID
WHERE b.cCVCode='{1}' AND cInvCode='{2}' {3}",cd.iQuantity, cCVCode, cd.cinvcode, strCon);
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("提交盘点失败:" + sql);
                    }
                }
                myTran.Commit();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                errMsg = ex.Message;
                return -1;
            }
            finally
            {
                conn.Close();
            }
            return 0;
        }
        #endregion

        #region use
        public static string SelSql(string str)
        {
            if (str == "null" || str == "")
                return "Null";
            else
                return "N'" + str + "'";
        }

        public static string math(string str)
        {
            if (str == "null" || str == "")
                return "null";
            else
                return str;
        }
        #endregion
    }
}
