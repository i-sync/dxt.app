using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace U8Business
{
    /// <summary>
    /// 快递单号
    /// </summary>
    public class ExpressOrderBusiness
    {
        /// <summary>
        /// 获取所有的物流名称
        /// </summary>
        /// <returns></returns>
        public static List<ShippingChoice> GetShoppingChoiceList()
        {
            List<ShippingChoice> list = null;
            DataTable dt = Common.GetInstance().Service.GetShoppingChoiceList(Common.CurrentUser.ConnectionString);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<ShippingChoice>();
            }
            ShippingChoice sc;
            ///循环遍历添加
            foreach (DataRow row in dt.Rows)
            {
                sc = new ShippingChoice();
                sc.cSCCode = row["cSCCode"].ToString ();
                sc.cSCName = row["cSCName"].ToString();
                list.Add(sc);
            }
            list.Insert(0, new ShippingChoice { cSCCode="00",cSCName="" });
            return list;
        }

        /// <summary>
        /// 根据发货单号查询发货单信息
        /// </summary>
        /// <param name="cDLCode">单据号</param>
        /// <returns></returns>
        public static DispatchList GetDispatchListByCDLCode(string cDLCode, out string errMsg)
        {
            DispatchList dispatchList= null;
            DataTable dt = Common.GetInstance().Service.GetDispatchListByCDLCode(Common.CurrentUser.ConnectionString,cDLCode, out errMsg);
            if (dt == null)
                return dispatchList;
            dispatchList = new DispatchList();
            DataRow row= dt.Rows[0];
            dispatchList.cDLCode = cDLCode;
            dispatchList.cstcode = row["cSTCode"].ToString();
            dispatchList.dDate = Convert.ToDateTime(row["dDate"]);
            dispatchList.cSCCode = row["cSCCode"].ToString();
            dispatchList.cmaker = row["cMaker"].ToString();
            dispatchList.ccuscode = row["cCusCode"].ToString();
            dispatchList.ccusname = row["cCusName"].ToString();
            return dispatchList;
        }

        /// <summary>
        /// 回写发货单快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool UpdateDispatchListExpressOrder(Model.DispatchList data)
        {
            U8Business.Service.DispatchList dispatchList = new U8Business.Service.DispatchList();
            dispatchList.cDLCode = data.cDLCode;
            dispatchList.cSCCode = data.cSCCode;
            dispatchList.cDefine13 = data.cDefine13;
            return Common.GetInstance().Service.UpdateDispatchListExpressOrder(Common.CurrentUser.ConnectionString,dispatchList);
        }

        /// <summary>
        /// 根据发票号查询发票单据信息
        /// </summary>
        /// <param name="cSBVCode">单据号</param>
        /// <returns></returns>
        public static SaleBillVouch GetSaleBillVouchByCSBVCode(string cSBVCode, out string errMsg)
        {
            SaleBillVouch saleBillVouch = null;
            DataTable dt = Common.GetInstance().Service.GetSaleBillVouchByCSBVCode(Common.CurrentUser.ConnectionString,cSBVCode, out errMsg);
            if (dt == null)
                return saleBillVouch;
            saleBillVouch = new SaleBillVouch();
            DataRow row = dt.Rows[0];
            saleBillVouch.cSBVCode = cSBVCode;
            saleBillVouch.cCusCode = row["cCusCode"].ToString();
            saleBillVouch.cCusName = row["cCusName"].ToString();
            saleBillVouch.dDate = Convert.ToDateTime(row["dDate"]);
            saleBillVouch.cSTCode = row["cSTCode"].ToString();
            saleBillVouch.cMaker = row["cMaker"].ToString();
            saleBillVouch.cSCCode = row["cSCCode"].ToString();
            return saleBillVouch;
        }

        /// <summary>
        /// 回写销售发票快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool UpdateSaleBillVouchExpressOrder(Model.SaleBillVouch data)
        {
            U8Business.Service.SaleBillVouch saleBillVouch = new U8Business.Service.SaleBillVouch();
            saleBillVouch.cSBVCode = data.cSBVCode;
            saleBillVouch.cSCCode = data.cSCCode;
            saleBillVouch.cDefine13 = data.cDefine13;
            return Common.GetInstance().Service.UpdateSaleBillVouchExpressOrder(Common.CurrentUser.ConnectionString,saleBillVouch);
        }
    }
}
