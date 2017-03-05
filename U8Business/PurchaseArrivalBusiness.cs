using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace U8Business
{
    /// <summary>
    /// 采购到货处理
    /// </summary>
    public class PurchaseArrivalBusiness
    {

        /// <summary>
        /// 根据采购订单号查询订单信息
        /// </summary>
        /// <param name="cOrderCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public ArrivalVouch PO_POMian_Load(string cOrderCode, out string errMsg)
        {
            DataSet ds = Common.GetInstance().Service.Po_Pomain_Load(Common.CurrentUser.ConnectionString, cOrderCode, out errMsg);
            //判断是否有数据
            if (ds.Tables["dtMain"] == null || ds.Tables["dtMain"].Rows.Count == 0)
            {
                errMsg = "没有查询到数据:单据号不存在或已被处理";
                return null;
            }
            //转换主表
            ArrivalVouch arrivalVouch = EntityConvert.ConvertToArrivalVouch(ds.Tables["dtMain"].Rows[0]);
            ArrivalVouchs arrivalVouchs = null;
            //循环转换子表
            foreach (DataRow row in ds.Tables["dtDetails"].Rows)
            {
                arrivalVouchs = EntityConvert.ConvertToArrivalVouchs(row);
                arrivalVouchs.cVenAbbName = arrivalVouch.cVenAbbName;
                arrivalVouch.U8Details.Add(arrivalVouchs);
            }
            return arrivalVouch;
        }

        /// <summary>
        /// 保存采购到货单
        /// </summary>
        /// <param name="arrivalVouch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool PU_ArrivalVouch_Save(ArrivalVouch arrivalVouch, out string errMsg)
        {
            U8Business.Service.ArrivalVouch tArrivalVouch = new U8Business.Service.ArrivalVouch();
            //主表转换
            EntityConvert.ConvertClass<ArrivalVouch, U8Business.Service.ArrivalVouch>(arrivalVouch, tArrivalVouch);
            //初始化数组
            tArrivalVouch.OperateDetails = new U8Business.Service.ArrivalVouchs[arrivalVouch.OperateDetails.Count];
            U8Business.Service.ArrivalVouchs tArrivalVouchs;
            int  i=0;
            foreach (ArrivalVouchs avs in arrivalVouch.OperateDetails)
            {
                tArrivalVouchs = new U8Business.Service.ArrivalVouchs();
                EntityConvert.ConvertClass<ArrivalVouchs, U8Business.Service.ArrivalVouchs>(avs, tArrivalVouchs);
                tArrivalVouch.OperateDetails[i++] = tArrivalVouchs;
            }

            U8Business.Service.User tUser = new U8Business.Service.User();
            EntityConvert.ConvertClass<User, U8Business.Service.User>(Common.CurrentUser, tUser);
            return Common.GetInstance().Service.PU_ArrivalVouch_Save(tUser, tArrivalVouch, out errMsg);
        }
    }
}
