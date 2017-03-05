using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace U8DataAccess
{
    /// <summary>
    /// 快递单回写
    /// </summary>
    public class ExpressOrderProcess
    {
        /// <summary>
        /// 获取所有的物流名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetShoppingChoiceList(string connectionString)
        {
            string strSql = "SELECT cSCCode,cSCName FROM ShippingChoice";
            return DBHelperSQL.Query(connectionString,strSql).Tables[0];
        }

        /// <summary>
        /// 根据发货单号查询发货单信息
        /// </summary>
        /// <param name="cDLCode">单据号</param>
        /// <returns></returns>
        public static DataTable GetDispatchListByCDLCode(string connectionString,string cDLCode,out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            //首先判断单据是否存在
            string strSql = string.Format("IF EXISTS(SELECT 1 FROM dbo.DispatchList WHERE cDLCode ='{0}')	SELECT 1 ELSE SELECT 0", cDLCode);
            int result = Convert.ToInt32(DBHelperSQL.ExecuteScalar(connectionString,strSql));
            if (result == 0)
            {
                errMsg = "单据不存在！";
                return dt;
            }

            //再判断单据是否审核
            strSql = string.Format("IF EXISTS(SELECT 1 FROM dbo.DispatchList WHERE cDLCode ='{0}' AND cVerifier IS NOT NULL AND dverifydate IS NOT NULL)	SELECT 1 ELSE SELECT 0", cDLCode);
            result = Convert.ToInt32(DBHelperSQL.ExecuteScalar(connectionString,strSql));
            if (result == 0)
            {
                errMsg = "单据未审核！";
                return dt;
            }

            strSql = string.Format("SELECT * FROM dbo.DispatchList WHERE cDLCode ='{0}'",cDLCode);
            dt = DBHelperSQL.Query(connectionString,strSql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 回写发货单快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool UpdateDispatchListExpressOrder(string connectionString,Model.DispatchList data)
        {
            bool flag = false;
            string strSql = string.Format("UPDATE dbo.DispatchList SET cSCCode ='{1}',cDefine13 ='{2}' WHERE cDLCode='{0}'", data.cDLCode, data.cSCCode, data.cDefine13);
            int result = DBHelperSQL.ExecuteSql(connectionString,strSql);
            if (result == 1)
                flag = true;
            return flag;
        }

        /// <summary>
        /// 根据发票号查询发票单据信息
        /// </summary>
        /// <param name="cSBVCode">单据号</param>
        /// <returns></returns>
        public static DataTable GetSaleBillVouchByCSBVCode(string connectionString,string cSBVCode,out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            //首先判断单据是否存在
            string strSql = string.Format("IF EXISTS(SELECT 1 FROM SaleBillVouch WHERE cSBVCode='{0}')	SELECT 1 ELSE SELECT 0", cSBVCode);
            int result = Convert.ToInt32(DBHelperSQL.ExecuteScalar(connectionString,strSql));
            if (result == 0)
            {
                errMsg = "单据不存在！";
                return dt;
            }

            //再判断单据是否审核
            strSql = string.Format("IF EXISTS(SELECT 1 FROM SaleBillVouch WHERE cSBVCode='{0}' AND cVerifier IS NOT NULL AND dverifydate IS NOT NULL)	SELECT 1 ELSE SELECT 0", cSBVCode);
            result = Convert.ToInt32(DBHelperSQL.ExecuteScalar(connectionString,strSql));
            if (result == 0)
            {
                errMsg = "单据未审核！";
                return dt;
            }

            strSql = string.Format("SELECT * FROM SaleBillVouch WHERE cSBVCode='{0}'", cSBVCode);
            dt = DBHelperSQL.Query(connectionString,strSql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 回写销售发票快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool UpdateSaleBillVouchExpressOrder(string connectionString,Model.SaleBillVouch data)
        {
            bool flag = false;
            string strSql = string.Format("UPDATE dbo.SaleBillVouch SET cSCCode ='{1}',cDefine13 ='{2}' WHERE cSBVCode='{0}'", data.cSBVCode, data.cSCCode, data.cDefine13);
            int result = DBHelperSQL.ExecuteSql(connectionString,strSql);
            if (result == 1)
                flag = true;
            return flag;
        }
    }
}
