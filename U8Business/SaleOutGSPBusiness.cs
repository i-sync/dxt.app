using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Model;

namespace U8Business
{
    public class SaleOutGSPBusiness
    {
        public static bool GetSaleOut(string ccode, out SaleOutGSPVouch saleoutGSP, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            DataSet Details = null;
            co.Service.GetSaleOut(ccode, Common.CurrentUser.ConnectionString, out Details, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            else
            {
                if (Details.Tables[0] != null && Details.Tables[0].Rows.Count > 0)
                {
                    saleoutGSP = new SaleOutGSPVouch();
                    saleoutGSP.U8Details = new List<GSPVouchDetail>();
                    saleoutGSP.OperateDetails = new List<GSPVouchDetail>();
                    foreach (DataRow dr in Details.Tables[0].Rows)
                    {
                        saleoutGSP.U8Details.Add(new GSPVouchDetail(dr));
                    }
                    saleoutGSP.CDEFINE2 = saleoutGSP.U8Details[0].cdefine2;
                    saleoutGSP.CDEFINE3 = saleoutGSP.U8Details[0].cdefine3;
                    saleoutGSP.CDEFINE7 = saleoutGSP.U8Details[0].cdefine7;
                    saleoutGSP.CDEFINE11 = saleoutGSP.U8Details[0].cdefine11;
                    return true;
                }
                else
                {
                    throw new Exception("获取销售出库单失败");
                    return false;
                }
            }
        }

        /// <summary>
        /// 保存销售出库GSP检验单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="flag">true:中药材/饮片;false:普通</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveSaleOutGSP(SaleOutGSPVouch dl,bool flag, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                U8Business.Service.SaleOutGSPVouch saleoutGSP = new U8Business.Service.SaleOutGSPVouch();

                saleoutGSP.CMAKER = dl.CMAKER;
                saleoutGSP.CDEFINE2 = dl.CDEFINE2;
                saleoutGSP.CDEFINE3 = dl.CDEFINE3;
                saleoutGSP.CDEFINE7 = dl.CDEFINE7;
                saleoutGSP.CDEFINE11 = dl.CDEFINE11;

                saleoutGSP.OperateDetails = new U8Business.Service.GSPVouchDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (GSPVouchDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.GSPVouchDetail detail = new U8Business.Service.GSPVouchDetail();
                    detail.ddate = dd.ddate;
                    detail.dvdate = dd.dvdate;
                    detail.cinvcode = dd.cinvcode;
                    detail.cbatch = dd.cbatch;
                    detail.FQUANTITY = dd.FQUANTITY;
                    detail.dmadedate = dd.dmadedate;
                    detail.cbuscode = dd.cbuscode;
                    detail.CVALDATE = dd.CVALDATE;
                    detail.autoid = dd.autoid;
                    detail.ccuscode = dd.ccuscode;
                    detail.cdefine22 = dd.cdefine22;
                    detail.cwhcode = dd.cwhcode;
                    detail.imassdate = dd.imassdate;
                    detail.CMASSUNIT = dd.CMASSUNIT;
                    detail.ID = dd.ID;
                    detail.cCode = dd.cCode;
                    detail.cmaker = dd.cmaker;
                    detail.CVALDATES = dd.CVALDATES;
                    detail.CRESULT = dd.CRESULT;//质量情况
                    saleoutGSP.OperateDetails[i] = detail;
                    i++;
                }

                int rt = co.Service.SaveSaleOutGSP(saleoutGSP, Common.CurrentUser.ConnectionString,flag, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
                if (rt != -1 && errMsg.Equals(""))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }
    }
}
