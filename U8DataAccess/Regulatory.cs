using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Model;

namespace U8DataAccess
{
    /// <summary>
    /// 监管码数据访问类
    /// </summary>
    public class Regulatory
    {
        /// <summary>
        /// 判断监管码是否已存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ExistsRegulatory(string connectionString, Model.Regulatory data)
        {
            bool flag = false;
            SqlParameter[] parms =
            {
                new SqlParameter("@RegCode",SqlDbType.VarChar,32),
                new SqlParameter("@AccID",SqlDbType.VarChar,32)
            };
            parms[0].Value = data.RegCode;
            parms[1].Value = data.AccID;
            int result;
            DBHelperSQL.RunProcedures(connectionString, "Proc_Regulatory_Exists", parms, out result);
            if (result == 1)
            {
                flag = true;//说明已存在
            }
            return flag;
        }

        /// <summary>
        /// 添加监管码对象
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        public static bool AddRegulatory(string connectionString, Model.Regulatory data,out string errMsg)
        {
            bool flag = false;
            errMsg = string.Empty;
            SqlParameter[] parms = 
            {
                new SqlParameter("@RegCode",SqlDbType.VarChar,32),
                new SqlParameter("@AccID",SqlDbType.VarChar,32)
            };

            parms[0].Value = data.RegCode;
            parms[1].Value = data.AccID;

            int result ;
            DBHelperSQL.RunProcedures(connectionString, "Proc_Regulatory_Insert", parms, out result);
            if (result == 1)
            {
                flag = true;
            }
            else if (result == 0)
            {
                errMsg = "该监管码已存在";
            }
            else
            {
                errMsg = "添加失败";
            }
            return flag;
        }

        /// <summary>
        /// 更新监管码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        public static bool UpdateRegulatory(string connectionString, Model.Regulatory data, out string errMsg)
        {
            bool flag = false;
            errMsg = string.Empty;

            SqlParameter[] parms =
            {
                new SqlParameter("@RegCode",SqlDbType.VarChar,32),
                new SqlParameter("@CardNumber",SqlDbType.VarChar,128),
                new SqlParameter("@CardName",SqlDbType.VarChar,512),
                new SqlParameter("@CardCode",SqlDbType.VarChar,128),
                new SqlParameter("@AccID",SqlDbType.VarChar,32)
            };

            parms[0].Value = data.RegCode;
            parms[1].Value = data.CardNumber;
            parms[2].Value = data.CardName;
            parms[3].Value = data.CardCode;
            parms[4].Value = data.AccID;

            int result;
            DBHelperSQL.RunProcedures(connectionString, "UFSystem..Proc_Regulatory_Update", parms, out result);
            if (result == 1)
            {
                flag = true;
            }
            else if (result == -1)
            {
                errMsg = "监管码已经使用";
            }
            else 
            {
                errMsg = "更新失败";
            }
            return flag;
        }

        /// <summary>
        /// 获取单个监管码对象
        /// </summary>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        public static Model.Regulatory GetModel(string connnectionString,Model.Regulatory data,out string errMsg)
        {
            errMsg = string.Empty;
            SqlParameter[] parms =
            {
                new SqlParameter("@RegCode",SqlDbType.VarChar,32),                
                new SqlParameter("@AccID",SqlDbType.VarChar,32)
            };
            parms[0].Direction = ParameterDirection.Output;
            parms[1].Value = data.AccID;
            int result;
            DBHelperSQL.RunProcedures(connnectionString, "UFSystem..Proc_Regulatory_SelectSingle", parms, out result);
            if (parms[0].Value == DBNull.Value)
            {
                errMsg = "没有找到可用的监管码";
                data = null;
            }
            else
            {
                //data = new Model.Regulatory();
                data.RegCode = parms[0].Value.ToString() ;
            }
            return data;
        }

        /// <summary>
        /// 查询监管码列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        public static DataTable GetList(string connectionString, Model.Regulatory data,out int total)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@RegCode",SqlDbType.VarChar,32),
                new SqlParameter("@CardNumber",SqlDbType.VarChar,128),
                new SqlParameter("@CardName",SqlDbType.VarChar,512),
                new SqlParameter("@CardCode",SqlDbType.VarChar,128),
                new SqlParameter("@IsUsed",SqlDbType.Bit),                
                new SqlParameter("@AccID",SqlDbType.VarChar,32),
                new SqlParameter("@PageIndex",SqlDbType.Int),
                new SqlParameter("@PageSize",SqlDbType.Int),
                new SqlParameter("@TotalCount",SqlDbType.Int)
            };

            parms[0].Value = data.RegCode;
            parms[1].Value = data.CardNumber;
            parms[2].Value = data.CardName;
            parms[3].Value = data.CardCode;
            if (data.IsUsed == -1)
                parms[4].Value = DBNull.Value;
            else
                parms[4].Value = data.IsUsed;
            parms[5].Value = data.AccID;
            parms[6].Value = data.PageIndex;
            parms[7].Value = data.PageSize;
            parms[8].Direction = ParameterDirection.Output;

            DataTable dt = DBHelperSQL.RunProcedureTable(connectionString, "Proc_Regulatory_SelectList", parms);
            data.TotalCount = Convert.ToInt32(parms[8].Value == DBNull.Value ? 0 : parms[8].Value);
            total = data.TotalCount;
            dt.TableName = "Regulatory";
            return dt;
        }
    }
}
