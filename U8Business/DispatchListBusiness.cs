using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Model;

namespace U8Business
{
    public class DispatchListBusiness
    {
        /// <summary>
        /// 验证销售订单号
        /// </summary>
        /// <param name="csocode">销售订单号</param>
        /// <param name="csocode_id">id号</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool VerifySO_SO(string csocode, out Model.DispatchList dispatchlist, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            dispatchlist = null;
            DataSet list = null;
            co.Service.VerifySO_SO(csocode, Common.CurrentUser.ConnectionString, out list, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            else
            {
                if (list.Tables[0] != null && list.Tables[0].Rows.Count > 0)
                {
                    dispatchlist = new Model.DispatchList(list);
                    dispatchlist.U8Details = new List<Model.DispatchDetail>();
                    dispatchlist.OperateDetails = new List<Model.DispatchDetail>();
                    DataSet ds = null;
                    int int_ds = GetSO_SODetails(dispatchlist.csoid, out ds, out errMsg);
                    if (errMsg != "" && int_ds != 0)
                    {
                        throw new Exception(errMsg);
                        return false;
                    }
                    DispatchDetail dd = null;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dd = new DispatchDetail(dr);
                        dd.ccusname = dispatchlist.ccusname;//客户名称
                        dd.ccusabbname = dispatchlist.ccusabbname;//客户简称
                        dispatchlist.U8Details.Add(dd);
                    }
                    return true;
                }
                else
                {
                    errMsg = "验证销售订单号出错";
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取销售订单子表
        /// </summary>
        /// <param name="csocode_id">id号</param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int GetSO_SODetails(string csocode_id, out DataSet Details, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            Details = null;
            co.Service.GetSO_SODetails(csocode_id, Common.CurrentUser.ConnectionString, out Details, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
                return -1;
            }
            else
            {
                if (Details.Tables[0] != null && Details.Tables[0].Rows.Count > 0)
                {
                    return 0;
                }
                else
                {
                    throw new Exception("获取表体数据失败");
                    return -1;
                }
            }
        }

        /// <summary>
        /// 生成销售发货单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveDispatchList(DispatchList dl, out string errMsg)
        {
            Common co = Common.GetInstance();
            errMsg = "";
            try
            {
                U8Business.Service.DispatchList dispatchlist = new U8Business.Service.DispatchList();

                #region webserver 实体类转换

                //表头
                dispatchlist.cpersoncode = dl.cpersoncode;
                dispatchlist.cbustype = dl.cbustype;
                dispatchlist.cDLCode = dl.cDLCode;
                dispatchlist.cdepcode = dl.cdepcode;
                dispatchlist.csocode = dl.csocode;
                dispatchlist.ccuscode = dl.ccuscode;
                dispatchlist.cexch_name = dl.cexch_name;
                dispatchlist.iExchRate = dl.iExchRate;
                dispatchlist.itaxrate = dl.itaxrate;
                dispatchlist.cmemo = dl.cmemo;
                dispatchlist.cdefine2 = dl.cdefine2;
                dispatchlist.cdefine3 = dl.cdefine3;
                dispatchlist.cdefine10 = dl.cdefine10;//监管码
                dispatchlist.cdefine11 = dl.cdefine11;
                dispatchlist.cmaker = dl.cmaker;
                dispatchlist.ccusname = dl.ccusname;
                dispatchlist.ccusperson = dl.ccusperson;
                dispatchlist.ccusoaddress = dl.ccusoaddress;
                dispatchlist.cSCCode = dl.cSCCode;
                dispatchlist.caddcode = dl.caddcode;

                //2013-11-11
                dispatchlist.cinvoicecompany = dl.cinvoicecompany;//开票单位编码 
                dispatchlist.ccuspersoncode = dl.ccuspersoncode;//联系人编码 

                dispatchlist.cstcode = dl.cstcode;//销售类型

                //表体
                dispatchlist.OperateDetails = new U8Business.Service.DispatchDetail[dl.OperateDetails.Count];
                int i = 0;
                foreach (DispatchDetail dd in dl.OperateDetails)
                {
                    U8Business.Service.DispatchDetail detail = new U8Business.Service.DispatchDetail();
                    detail.cposition = dd.cposition;//货位
                    detail.cwhcode = dd.cwhcode;
                    detail.cinvcode = dd.cinvcode;
                    detail.iquantity = dd.iquantity;
                    detail.inewquantity = dd.inewquantity;
                    detail.iquotedprice = dd.iquotedprice;
                    detail.iunitprice = dd.iunitprice;
                    detail.itaxunitprice = dd.itaxunitprice;
                    detail.imoney = dd.imoney * dd.inewquantity / dd.iquantity;
                    detail.itax = dd.itax * dd.inewquantity / dd.iquantity;
                    detail.isum = dd.isum * dd.inewquantity / dd.iquantity;
                    detail.idiscount = dd.idiscount * dd.inewquantity / dd.iquantity;
                    detail.inatunitprice = dd.inatunitprice;
                    detail.inatmoney = dd.inatmoney * dd.inewquantity / dd.iquantity;
                    detail.inattax = dd.inattax * dd.inewquantity / dd.iquantity;
                    detail.inatsum = dd.inatsum * dd.inewquantity / dd.iquantity;
                    detail.inatdiscount = dd.inatdiscount * dd.inewquantity / dd.iquantity;
                    detail.invbatch = dd.invbatch;
                    detail.dvdate = dd.dvdate;
                    detail.isosid = dd.isosid;
                    detail.kl = dd.kl;
                    detail.kl2 = dd.kl2;
                    detail.cinvname = dd.cinvname;
                    detail.itaxrate = dd.itaxrate;
                    detail.cdefine22 = dd.cdefine22;
                    detail.fsalecost = dd.fsalecost;
                    detail.fsaleprice = dd.fsaleprice * dd.inewquantity / dd.iquantity;
                    detail.cvenabbname = dd.cvenabbname;
                    detail.dmdate = dd.dmdate;
                    detail.csocode = dd.csocode;
                    detail.cmassunit = dd.cmassunit;
                    detail.imassdate = dd.imassdate;
                    detail.cordercode = dd.cordercode;
                    detail.iexpiratdatecalcu = dd.iexpiratdatecalcu;
                    detail.dexpirationdate = dd.dexpirationdate;
                    detail.cexpirationdate = dd.cexpirationdate;
                    detail.cvencode = dd.cvencode;
                    detail.cdefine25 = dd.cdefine25;//请货单号

                    //2013-11-11
                    detail.bsaleprice = dd.bsaleprice;//报价含税标识
                    detail.bgift = dd.bgift;//是否赠品

                    detail.iorderrowno = dd.iorderrowno;//订单行号

                    dispatchlist.OperateDetails[i] = detail;
                    i++;
                }
                #endregion

                int rt = co.Service.SaveDispatchList(dispatchlist, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
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

        /// <summary>
        /// 批次管理：根据存货编码，仓库编码，货位编码来获取批次管理
        /// </summary>
        /// <param name="invCode">存货编码</param>
        /// <param name="whCode">仓库编码</param>
        /// <param name="cPosition">货位编码</param>
        /// <param name="ConnectionString"></param>
        /// <param name="dsBatch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-10-27</remarks>
        public static List<BatchInfo> GetBatchList(string cInvCode, string cWhCode, string cPosition)
        {
            if (string.IsNullOrEmpty(cInvCode))
            {
                throw new Exception("请输入产品编号！");
            }
            List<BatchInfo> batchList = new List<BatchInfo>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsBatch = new DataSet();

            co.Service.GetBatchList(cInvCode, cWhCode, cPosition, Common.CurrentUser.ConnectionString, out dsBatch, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsBatch == null || dsBatch.Tables[0].Rows.Count < 1)
                return null;

            BatchInfo batch;
            foreach (DataRow dr in dsBatch.Tables[0].Rows)
            {
                batch = new BatchInfo();
                batch.WhCode = dr["cwhcode"].ToString();
                batch.InvCode = dr["cinvcode"].ToString();
                batch.Batch = dr["cBatch"].ToString();
                batch.Quantity = Common.DB2Decimal(dr["iQuantity"].ToString());
                batch.VDate = dr["dVDate"].ToString();
                batch.Mdate = dr["dMdate"].ToString();
                batch.MassDate = Common.DB2Decimal(dr["iMassDate"].ToString());
                batch.MassUnit= Convert.ToInt32(dr["cMassUnit"]);
                batch.Expirationdate = dr["cExpirationdate"].ToString();
                batchList.Add(batch);
            }
            return batchList;
        }



        /// <summary>
        /// 处理销售出库单，为销售出库单添加货位信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-12</remarks>
        public static int InsertInvPosition(out string errMsg)
        {
            Common co = Common.GetInstance();
            return co.Service.InsertInvPosition(Common.CurrentUser.ConnectionString , out errMsg);
        }
    }
}
