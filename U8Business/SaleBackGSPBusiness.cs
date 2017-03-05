using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace U8Business
{
    public class SaleBackGSPBusiness
    {
        public static bool GetSaleBack(string ccode, out SaleBackGSPVouch salebackGSP, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            DataSet Details = null;
            co.Service.GetSaleBack(ccode, Common.CurrentUser.ConnectionString, out Details, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            else
            {
                if (Details.Tables[0] != null && Details.Tables[0].Rows.Count > 0)
                {
                    salebackGSP = new SaleBackGSPVouch();
                    salebackGSP.U8Details = new List<SaleBackGSPDetail>();
                    salebackGSP.OperateDetails = new List<SaleBackGSPDetail>();
                    foreach (DataRow dr in Details.Tables[0].Rows)
                    {
                        salebackGSP.U8Details.Add(new SaleBackGSPDetail(dr));
                    }
                    salebackGSP.ICODE = salebackGSP.U8Details[0].dlid;
                    salebackGSP.CCODE = salebackGSP.U8Details[0].cdlcode;
                    salebackGSP.DARVDATE = salebackGSP.U8Details[0].ddate;
                    return true;
                }
                else
                {
                    throw new Exception("获取销售退货单失败");
                    return false;
                }
            }
        }

        public static int SaveSaleBackGSP(SaleBackGSPVouch dl, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                U8Business.Service.SaleBackGSPVouch saleoutGSP = new U8Business.Service.SaleBackGSPVouch();

                saleoutGSP.CMAKER = dl.CMAKER;
                saleoutGSP.ICODE = dl.ICODE;
                saleoutGSP.CCODE = dl.CCODE;
                saleoutGSP.DARVDATE = dl.DARVDATE;

                saleoutGSP.OperateDetails = new U8Business.Service.SaleBackGSPDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (SaleBackGSPDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.SaleBackGSPDetail detail = new U8Business.Service.SaleBackGSPDetail();
                    detail.cinvcode = dd.cinvcode;
                    detail.FQUANTITY = dd.FQUANTITY;
                    detail.FARVQUANTITY = dd.FARVQUANTITY;
                    detail.DPRODATE = dd.DPRODATE;
                    detail.FQUANTITY = dd.FQUANTITY;
                    detail.DVDATE = dd.DVDATE;
                    detail.CVALDATE = dd.CVALDATE;
                    //detail.DDATE_T = dd.DDATE_T;
                    detail.FELGQUANTITY = dd.ScanCount;
                    detail.CBATCH = dd.CBATCH;
                    detail.CCUSCODE = dd.CCUSCODE;
                    detail.CDEFINE22 = dd.CDEFINE22;
                    detail.ICODE_T = dd.ICODE_T;
                    detail.cwhcode = dd.cwhcode;
                    detail.imassDate = dd.imassDate;
                    detail.DVDATE = dd.DVDATE;
                    detail.ddate = dd.ddate;
                    detail.COUTINSTANCE = dd.COUTINSTANCE;
                    detail.ccusname = dd.ccusname;
                    saleoutGSP.OperateDetails[i] = detail;
                    i++;
                }

                int rt = co.Service.SaveSaleBackGSP(saleoutGSP, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
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
