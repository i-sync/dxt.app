using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Model;

namespace U8Business
{
    public class STInProductBusiness
    {
        public static bool GetSTInProduct(string cInvCode, out STInProductDetail detail, out string errMsg)
        {
            Common co = Common.GetInstance();
            detail = null;
            errMsg = "";
            DataSet ds = null;
            co.Service.GetSTInProduct(cInvCode, Common.CurrentUser.ConnectionString, out ds, out errMsg);
            if (errMsg != "")
            {
                return false;
            }
            else
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    detail = new STInProductDetail(ds.Tables[0].Rows[0]); 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int SaveProductIn(STInProduct dl, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                #region webserver 实体类转换
                //表头
                U8Business.Service.STInProduct stin = new U8Business.Service.STInProduct();
                stin.brdflag = 1;
                stin.cvouchtype = "10";
                stin.cbustype = "成品入库";
                stin.csource = "库存";
                stin.crdcode = "102";
                stin.bpufirst = 0;
                stin.biafirst = 0;
                stin.vt_id = 63;
                stin.bisstqc = 0;
                stin.iproorderid = 0;
                stin.iswfcontrolled = 0;
                stin.cmaker = dl.cmaker;
                stin.cwhcode = dl.cwhcode;
                stin.cdefine10 = dl.cdefine10;

                //表体
                stin.OperateDetails = new U8Business.Service.STInProductDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (STInProductDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.STInProductDetail detail = new U8Business.Service.STInProductDetail();
                    detail.cinvcode = dd.cinvcode;
                    detail.iquantity = dd.iquantity;
                    detail.iinvexchrate = dd.iinvexchrate;
                    detail.iunitcost = dd.iunitcost;
                    detail.iprice = dd.iprice;
                    detail.cbatch = dd.cbatch;
                    detail.dvdate = dd.dvdate;
                    detail.cExpirationdate = dd.cExpirationdate;
                    detail.cassunit = dd.cassunit;
                    detail.dmadedate = dd.dmadedate;
                    detail.cmassunit = 2;

                    detail.cwhcode = dd.cwhcode;
                    detail.cposition = dd.cposition;
                    detail.cCheckCode = dd.cCheckCode;
                    stin.OperateDetails[i] = detail;
                    i++;
                }
                #endregion
                int rt = co.Service.SaveProductIn(stin, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
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
