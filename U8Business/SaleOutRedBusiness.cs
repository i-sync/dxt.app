using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Model;

namespace U8Business
{
    public class SaleOutRedBusiness
    {
        public static bool VerifyGSPBack(string cchkcode, out SaleOutRedList redlist, out string errMsg)
        {
            redlist = null;
            errMsg = "";
            try
            {
                Common co = Common.GetInstance();
                U8Business.Service.SaleOutRedList list = null;
                int rt = co.Service.VerifyGSPBack(cchkcode, Common.CurrentUser.ConnectionString, out list, out errMsg);
                if (errMsg != "" || rt != 0)
                {
                    return false;
                }
                else
                {
                    //表头
                    redlist = new SaleOutRedList();
                    redlist.cbustype = list.cbustype;
                    redlist.cbuscode = list.cbuscode;
                    redlist.cdepcode = list.cdepcode;
                    redlist.cpersoncode = list.cpersoncode;
                    redlist.cstcode = list.cstcode;
                    redlist.ccuscode = list.ccuscode;
                    redlist.cdlid = list.cdlid;
                    redlist.cchkcode = list.cchkcode;
                    redlist.cchkperson = list.cchkperson;
                    redlist.dchkdate = list.dchkdate;
                    redlist.cmemo = list.cmemo;
                    redlist.ccusname = list.ccusname;
                    //表体
                    redlist.U8Details = new List<SaleOutRedDetail>();
                    redlist.OperateDetails = new List<SaleOutRedDetail>();
                    for (int i = 0; i < list.U8Details.Length; i++)
                    {
                        SaleOutRedDetail detail = new SaleOutRedDetail();
                        detail.cinvcode = list.U8Details[i].cinvcode;
                        detail.cinvname = list.U8Details[i].cinvname;
                        detail.cwhcode = list.U8Details[i].cwhcode;
                        detail.cinvstd = list.U8Details[i].cinvstd;
                        detail.cbatch = list.U8Details[i].cbatch;
                        detail.dvdate = list.U8Details[i].dvdate;
                        detail.dmadedate = list.U8Details[i].dmadedate;
                        detail.cExpirationdate = list.U8Details[i].cExpirationdate;
                        detail.imassdate = list.U8Details[i].imassdate;
                        detail.iunitcost = list.U8Details[i].iunitcost;
                        detail.iprice = list.U8Details[i].iprice;
                        detail.iquantity = list.U8Details[i].iquantity;
                        detail.cdefine22 = list.U8Details[i].cdefine22;
                        detail.icheckids = list.U8Details[i].icheckids;
                        detail.idlsid = list.U8Details[i].idlsid;
                        detail.cinvdefine1 = list.U8Details[i].cinvdefine1;
                        detail.cinvm_unit = list.U8Details[i].cinvm_unit;
                        detail.ccusname = list.U8Details[i].ccusname;

                        redlist.U8Details.Add(detail);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int SaveSaleOutRed(SaleOutRedList dl, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                #region webserver 实体类转换
                //表头
                U8Business.Service.SaleOutRedList list = new U8Business.Service.SaleOutRedList();
                list.cmaker = dl.cmaker;
                list.cwhcode = dl.cwhcode;
                list.cbustype = dl.cbustype;
                list.cbuscode = dl.cbuscode;
                list.cdepcode = dl.cdepcode;
                list.cpersoncode = dl.cpersoncode;
                list.cstcode = dl.cstcode;
                list.ccuscode = dl.ccuscode;
                list.cdlid = dl.cdlid;
                list.cchkcode = dl.cchkcode;
                list.dchkdate = dl.dchkdate;
                list.cchkperson = dl.cchkperson;
                list.cmemo = dl.cmemo;
                list.cdefine10 = dl.cdefine10;

                //表体
                list.OperateDetails = new U8Business.Service.SaleOutRedDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (SaleOutRedDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.SaleOutRedDetail detail = new U8Business.Service.SaleOutRedDetail();
                    detail.cinvcode = dd.cinvcode;
                    detail.iquantity = dd.iquantity;
                    detail.iunitcost = dd.iunitcost;
                    detail.iprice = dd.iprice;
                    detail.cbatch = dd.cbatch;
                    detail.dvdate = dd.dvdate;
                    detail.cdefine22 = dd.cdefine22;
                    detail.dmadedate = dd.dmadedate;
                    detail.idlsid = dd.idlsid;
                    detail.imassdate = dd.imassdate;
                    detail.icheckids = dd.icheckids;
                    //货位已扫描数量
                    detail.cposition = dd.cposition;
                    detail.inewquantity = dd.inewquantity;
                    list.OperateDetails[i] = detail;
                    i++;
                }
                #endregion
                int rt = co.Service.SaveSaleOutRed(list, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
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
