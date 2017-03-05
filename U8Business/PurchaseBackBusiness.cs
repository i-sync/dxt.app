using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;


namespace U8Business
{
    public class PurchaseBackBusiness
    {
        public static bool GetPurchaseBack(string ccode, out PurchaseBackVouch backGSP, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            DataSet Details = null;
            co.Service.GetPurchaseBack(ccode, Common.CurrentUser.ConnectionString, out Details, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            else
            {
                if (Details.Tables[0] != null && Details.Tables[0].Rows.Count > 0)
                {
                    backGSP = new PurchaseBackVouch();
                    backGSP.U8Details = new List<PurchaseBackDetail>();
                    backGSP.OperateDetails = new List<PurchaseBackDetail>();
                    foreach (DataRow dr in Details.Tables[0].Rows)
                    {
                        backGSP.U8Details.Add(new PurchaseBackDetail(dr));
                    }
                    backGSP.iRdId = backGSP.U8Details[0].iRdId;
                    backGSP.cRdCode = backGSP.U8Details[0].cRdCode;
                    backGSP.dArvdate = backGSP.U8Details[0].dArvdate;
                    backGSP.cVenCode = backGSP.U8Details[0].cVenCode;
                    backGSP.cRdMaker = backGSP.U8Details[0].cRdMaker;
                    backGSP.cWhCode = backGSP.U8Details[0].cWhCode;
                    backGSP.cWhName = backGSP.U8Details[0].cWhName;
                    backGSP.cDefine1 = backGSP.U8Details[0].cDefine1;
                    return true;
                }
                else
                {
                    throw new Exception("获取采购入库单红字失败");
                    return false;
                }
            }
        }

        public static int SavePurchaseBackGSP(PurchaseBackVouch dl, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                U8Business.Service.PurchaseBackVouch GSP = new U8Business.Service.PurchaseBackVouch();

                GSP.iRdId = dl.iRdId;
                GSP.cRdCode = dl.cRdCode;
                GSP.dArvdate = dl.dArvdate;
                GSP.cVenCode = dl.cVenCode;
                GSP.cRdMaker = dl.cRdMaker;
                GSP.cMaker = dl.cMaker;
                GSP.cWhCode = dl.cWhCode;
                GSP.cDefine1 = dl.cDefine1;

                GSP.OperateDetails = new U8Business.Service.PurchaseBackDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (PurchaseBackDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.PurchaseBackDetail detail = new U8Business.Service.PurchaseBackDetail();
                    detail.IRdsID = dd.IRdsID;
                    detail.cInvcode = dd.cInvcode;
                    detail.dMadeDate = dd.dMadeDate;
                    detail.cBatch = dd.cBatch;
                    detail.dValDate = dd.dValDate;
                    detail.iQuantity = dd.iQuantity;
                    detail.cdefine22 = dd.cdefine22;
                    detail.imassDate = dd.imassDate;
                    detail.CValDate = dd.CValDate;
                    detail.cInstance = dd.cInstance;
                    GSP.OperateDetails[i] = detail;
                    i++;
                }

                int rt = co.Service.SavePurchaseBackGSP(GSP, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
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
