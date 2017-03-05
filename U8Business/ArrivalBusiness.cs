using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;


using U8Business;
using Model;

namespace U8Business
{
    public class ArrivalBusiness
    {

        /// <summary>
        /// 查询采购订单
        /// </summary>
        /// <param name="cCode">订单号</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ArrivalVouch CreateAVOrderByPomain(string cCode, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateAVOrderByPomain(cCode, Common.CurrentUser.ConnectionString, out order, out errMsg);

            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            ArrivalVouch objAVOrder = new ArrivalVouch();
            ArrivalVouchSource(objAVOrder, order);

            return objAVOrder;
        }

        /// <summary>
        /// 查询委外订单
        /// </summary>
        /// <param name="cCode">订单号</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ArrivalVouch CreateAVOrderByMomain(string cCode, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateAVOrderByMomain(cCode, Common.CurrentUser.ConnectionString, out order, out errMsg);

            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            ArrivalVouch objAVOrder = new ArrivalVouch();
            ArrivalVouchSource(objAVOrder, order);

            return objAVOrder;
        }

        /// <summary>
        /// 根据采购订单保存采购到货单
        /// </summary>
        /// <param name="arrival"></param>
        /// <param name="sourceVoucher"></param>
        public static void Save(ArrivalVouch arrival, string sourceVoucher)
        {
            if (arrival.OperateDetails.Count == 0)
            {
                throw new Exception("请操作数据后再提交");
            }
            Common co = Common.GetInstance();
            string errMsg = "";//出错信息

            DataSet ds = BuildArrivalStruct();
            ArrivalVouchResult(arrival, ds);
            if (sourceVoucher == "01")
                co.Service.SaveByPomain(ds, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            else if (sourceVoucher == "02")
                co.Service.SaveByMomain(ds, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 创建表结构
        /// </summary>
        /// <returns></returns>
        private static DataSet BuildArrivalStruct()
        {
            DataSet ds = new DataSet();

            DataTable dtHead = new DataTable("Head");
            DataTable dtBody = new DataTable("Body");
             
            ds.Tables.Add(dtHead);
            ds.Tables.Add(dtBody);

            #region 表头设计
            ds.Tables["Head"].Columns.Add("ivtid");
            ds.Tables["Head"].Columns.Add("ID");
            ds.Tables["Head"].Columns.Add("ccode");
            ds.Tables["Head"].Columns.Add("ddate");
            ds.Tables["Head"].Columns.Add("darvdate");
            ds.Tables["Head"].Columns.Add("dnmaketime");
            ds.Tables["Head"].Columns.Add("chandler");
            ds.Tables["Head"].Columns.Add("dnverifytime");
            ds.Tables["Head"].Columns.Add("dveridate");
            ds.Tables["Head"].Columns.Add("controlresult");
            ds.Tables["Head"].Columns.Add("cvencode");
            ds.Tables["Head"].Columns.Add("cptcode");
            ds.Tables["Head"].Columns.Add("cdepcode");
            ds.Tables["Head"].Columns.Add("cpersoncode");
            ds.Tables["Head"].Columns.Add("cpaycode");
            ds.Tables["Head"].Columns.Add("csccode");
            ds.Tables["Head"].Columns.Add("cexch_name");
            ds.Tables["Head"].Columns.Add("iexchrate");
            ds.Tables["Head"].Columns.Add("itaxrate");
            ds.Tables["Head"].Columns.Add("cmemo");
            ds.Tables["Head"].Columns.Add("cbustype");
            ds.Tables["Head"].Columns.Add("cmaker");
            ds.Tables["Head"].Columns.Add("bnegative");
            ds.Tables["Head"].Columns.Add("cdefine1");
            ds.Tables["Head"].Columns.Add("cdefine2");
            ds.Tables["Head"].Columns.Add("cdefine3");
            ds.Tables["Head"].Columns.Add("cdefine4");
            ds.Tables["Head"].Columns.Add("cdefine5");
            ds.Tables["Head"].Columns.Add("cdefine6");
            ds.Tables["Head"].Columns.Add("cdefine7");
            ds.Tables["Head"].Columns.Add("cdefine8");
            ds.Tables["Head"].Columns.Add("cdefine9");
            ds.Tables["Head"].Columns.Add("cdefine10");
            ds.Tables["Head"].Columns.Add("cdefine11");
            ds.Tables["Head"].Columns.Add("cdefine12");
            ds.Tables["Head"].Columns.Add("cdefine13");
            ds.Tables["Head"].Columns.Add("cdefine14");
            ds.Tables["Head"].Columns.Add("cdefine15");
            ds.Tables["Head"].Columns.Add("cdefine16");
            ds.Tables["Head"].Columns.Add("ccloser");
            ds.Tables["Head"].Columns.Add("idiscounttaxtype");
            ds.Tables["Head"].Columns.Add("ibilltype");
            ds.Tables["Head"].Columns.Add("cvouchtype");
            ds.Tables["Head"].Columns.Add("cgeneralordercode");
            ds.Tables["Head"].Columns.Add("ctmcode");
            ds.Tables["Head"].Columns.Add("cincotermcode");
            ds.Tables["Head"].Columns.Add("ctransordercode");
            ds.Tables["Head"].Columns.Add("dportdate");
            ds.Tables["Head"].Columns.Add("csportcode");
            ds.Tables["Head"].Columns.Add("caportcode");
            ds.Tables["Head"].Columns.Add("csvencode");
            ds.Tables["Head"].Columns.Add("carrivalplace");
            ds.Tables["Head"].Columns.Add("dclosedate");
            ds.Tables["Head"].Columns.Add("idec");
            ds.Tables["Head"].Columns.Add("bcal");
            ds.Tables["Head"].Columns.Add("guid");
            ds.Tables["Head"].Columns.Add("iverifystate");
            ds.Tables["Head"].Columns.Add("cauditdate");
            ds.Tables["Head"].Columns.Add("cverifier");
            ds.Tables["Head"].Columns.Add("iverifystateex");
            ds.Tables["Head"].Columns.Add("ireturncount");
            ds.Tables["Head"].Columns.Add("iswfcontrolled");
            ds.Tables["Head"].Columns.Add("cvenpuomprotocol");
            ds.Tables["Head"].Columns.Add("cchanger");
            ds.Tables["Head"].Columns.Add("iflowid");
            ds.Tables["Head"].Columns.Add("cvenname");
            ds.Tables["Head"].Columns.Add("caddress");
            ds.Tables["Head"].Columns.Add("ufts");
            #endregion

            #region 表体设计
            ds.Tables["Body"].Columns.Add("autoid");
            ds.Tables["Body"].Columns.Add("id");
            ds.Tables["Body"].Columns.Add("cwhcode");
            ds.Tables["Body"].Columns.Add("cinvcode");
            ds.Tables["Body"].Columns.Add("cinvname");
            ds.Tables["Body"].Columns.Add("cinvstd");
            ds.Tables["Body"].Columns.Add("inum");
            ds.Tables["Body"].Columns.Add("iquantity");
            ds.Tables["Body"].Columns.Add("ioricost");
            ds.Tables["Body"].Columns.Add("ioritaxcost");
            ds.Tables["Body"].Columns.Add("iorimoney");
            ds.Tables["Body"].Columns.Add("ioritaxprice");
            ds.Tables["Body"].Columns.Add("iorisum");
            ds.Tables["Body"].Columns.Add("icost");
            ds.Tables["Body"].Columns.Add("imoney");
            ds.Tables["Body"].Columns.Add("itaxprice");
            ds.Tables["Body"].Columns.Add("isum");
            ds.Tables["Body"].Columns.Add("cfree1");
            ds.Tables["Body"].Columns.Add("cfree2");
            ds.Tables["Body"].Columns.Add("cfree3");
            ds.Tables["Body"].Columns.Add("cfree4");
            ds.Tables["Body"].Columns.Add("cfree5");
            ds.Tables["Body"].Columns.Add("cfree6");
            ds.Tables["Body"].Columns.Add("cfree7");
            ds.Tables["Body"].Columns.Add("cfree8");
            ds.Tables["Body"].Columns.Add("cfree9");
            ds.Tables["Body"].Columns.Add("cfree10");
            ds.Tables["Body"].Columns.Add("itaxrate");
            ds.Tables["Body"].Columns.Add("cdefine22");
            ds.Tables["Body"].Columns.Add("cdefine23");
            ds.Tables["Body"].Columns.Add("cdefine24");
            ds.Tables["Body"].Columns.Add("cdefine25");
            ds.Tables["Body"].Columns.Add("cdefine26");
            ds.Tables["Body"].Columns.Add("cdefine27");
            ds.Tables["Body"].Columns.Add("cdefine28");
            ds.Tables["Body"].Columns.Add("cdefine29");
            ds.Tables["Body"].Columns.Add("cdefine30");
            ds.Tables["Body"].Columns.Add("cdefine31");
            ds.Tables["Body"].Columns.Add("cdefine32");
            ds.Tables["Body"].Columns.Add("cdefine33");
            ds.Tables["Body"].Columns.Add("cdefine34");
            ds.Tables["Body"].Columns.Add("cdefine35");
            ds.Tables["Body"].Columns.Add("cdefine36");
            ds.Tables["Body"].Columns.Add("cdefine37");
            ds.Tables["Body"].Columns.Add("citem_class");
            ds.Tables["Body"].Columns.Add("citemcode");
            ds.Tables["Body"].Columns.Add("iposid");
            ds.Tables["Body"].Columns.Add("citemname");
            ds.Tables["Body"].Columns.Add("cunitid");
            ds.Tables["Body"].Columns.Add("fkpquantity");
            ds.Tables["Body"].Columns.Add("frealquantity");
            ds.Tables["Body"].Columns.Add("fValidQuantity");
            ds.Tables["Body"].Columns.Add("fvalidInQuan");
            ds.Tables["Body"].Columns.Add("finvalidquantity");
            ds.Tables["Body"].Columns.Add("ccloser");
            ds.Tables["Body"].Columns.Add("icorid");
            ds.Tables["Body"].Columns.Add("bgsp");
            ds.Tables["Body"].Columns.Add("cbatch");
            ds.Tables["Body"].Columns.Add("dvdate");
            ds.Tables["Body"].Columns.Add("dpdate");
            ds.Tables["Body"].Columns.Add("frefusequantity");
            ds.Tables["Body"].Columns.Add("cgspstate");
            ds.Tables["Body"].Columns.Add("fvalidnum");
            ds.Tables["Body"].Columns.Add("finvalidnum");
            ds.Tables["Body"].Columns.Add("frealnum");
            ds.Tables["Body"].Columns.Add("btaxcost");
            ds.Tables["Body"].Columns.Add("binspect");
            ds.Tables["Body"].Columns.Add("frefusenum");
            ds.Tables["Body"].Columns.Add("ippartid");
            ds.Tables["Body"].Columns.Add("ipquantity");
            ds.Tables["Body"].Columns.Add("iptoseq");
            ds.Tables["Body"].Columns.Add("sodid");
            ds.Tables["Body"].Columns.Add("sotype");
            ds.Tables["Body"].Columns.Add("contractrowguid");
            ds.Tables["Body"].Columns.Add("imassdate");
            ds.Tables["Body"].Columns.Add("cmassunit");
            ds.Tables["Body"].Columns.Add("bexigency");
            ds.Tables["Body"].Columns.Add("cbcloser");
            ds.Tables["Body"].Columns.Add("fdtquantity");
            ds.Tables["Body"].Columns.Add("finvalidinnum");
            ds.Tables["Body"].Columns.Add("fdegradequantity");
            ds.Tables["Body"].Columns.Add("fdegradenum");
            ds.Tables["Body"].Columns.Add("fdegradeinquantity");
            ds.Tables["Body"].Columns.Add("fdegradeinnum");
            ds.Tables["Body"].Columns.Add("finspectquantity");
            ds.Tables["Body"].Columns.Add("finspectnum");
            ds.Tables["Body"].Columns.Add("iinvmpcost");
            ds.Tables["Body"].Columns.Add("guids");
            ds.Tables["Body"].Columns.Add("iinvexchrate");
            ds.Tables["Body"].Columns.Add("objectid_source");
            ds.Tables["Body"].Columns.Add("autoid_source");
            ds.Tables["Body"].Columns.Add("ufts_source");
            ds.Tables["Body"].Columns.Add("irowno_source");
            ds.Tables["Body"].Columns.Add("csocode");
            ds.Tables["Body"].Columns.Add("isorowno");
            ds.Tables["Body"].Columns.Add("iorderid");
            ds.Tables["Body"].Columns.Add("cordercode");
            ds.Tables["Body"].Columns.Add("iorderrowno");
            ds.Tables["Body"].Columns.Add("dlineclosedate");
            ds.Tables["Body"].Columns.Add("contractcode");
            ds.Tables["Body"].Columns.Add("contractrowno");
            ds.Tables["Body"].Columns.Add("rejectsource");
            ds.Tables["Body"].Columns.Add("iciqbookid");
            ds.Tables["Body"].Columns.Add("cciqbookcode");
            ds.Tables["Body"].Columns.Add("cciqcode");
            ds.Tables["Body"].Columns.Add("fciqchangrate");
            ds.Tables["Body"].Columns.Add("irejectautoid");
            ds.Tables["Body"].Columns.Add("iexpiratdatecalcu");
            ds.Tables["Body"].Columns.Add("cexpirationdate");
            ds.Tables["Body"].Columns.Add("dexpirationdate");
            ds.Tables["Body"].Columns.Add("cupsocode");
            ds.Tables["Body"].Columns.Add("iorderdid");
            ds.Tables["Body"].Columns.Add("iordertype");
            ds.Tables["Body"].Columns.Add("csoordercode");
            ds.Tables["Body"].Columns.Add("iorderseq");
            ds.Tables["Body"].Columns.Add("cbatchproperty1");
            ds.Tables["Body"].Columns.Add("cbatchproperty2");
            ds.Tables["Body"].Columns.Add("cbatchproperty3");
            ds.Tables["Body"].Columns.Add("cbatchproperty4");
            ds.Tables["Body"].Columns.Add("cbatchproperty5");
            ds.Tables["Body"].Columns.Add("cbatchproperty6");
            ds.Tables["Body"].Columns.Add("cbatchproperty7");
            ds.Tables["Body"].Columns.Add("cbatchproperty8");
            ds.Tables["Body"].Columns.Add("cbatchproperty9");
            ds.Tables["Body"].Columns.Add("cbatchproperty10");
            ds.Tables["Body"].Columns.Add("ivouchrowno");
            ds.Tables["Body"].Columns.Add("inquantity");
            ds.Tables["Body"].Columns.Add("cvenname");
            ds.Tables["Body"].Columns.Add("cvenabbname");
            ds.Tables["Body"].Columns.Add("caddress");
            ds.Tables["Body"].Columns.Add("cinvm_unit");
            ds.Tables["Body"].Columns.Add("darrivedate");
            ds.Tables["Body"].Columns.Add("cdemandmemo"); 
            ds.Tables["Body"].Columns.Add("binvbatch");
            ds.Tables["Body"].Columns.Add("orderquantity");
            //委外
            ds.Tables["Body"].Columns.Add("modetailsID");

            #endregion

            return ds;
        }

        /// <summary>
        /// 转换（表头）
        /// </summary>
        /// <param name="objAVOrder"></param>
        /// <param name="order"></param>
        private static void ArrivalVouchSource(ArrivalVouch objAVOrder, DataSet order)
        {
            #region Head
            objAVOrder.VT_ID = Common.DB2Int(order.Tables["head"].Rows[0]["ivtid"]);
            objAVOrder.ID = Common.DB2Int(order.Tables["head"].Rows[0]["id"]);
            objAVOrder.cCode = Common.DB2String(order.Tables["head"].Rows[0]["ccode"]);
            objAVOrder.cPTCode = Common.DB2String(order.Tables["head"].Rows[0]["cptcode"]);
            objAVOrder.dDate = Common.DB2String(order.Tables["head"].Rows[0]["ddate"]);
            objAVOrder.cVenCode = Common.DB2String(order.Tables["head"].Rows[0]["cvencode"]);
            objAVOrder.cDepCode = Common.DB2String(order.Tables["head"].Rows[0]["cdepcode"]);
            objAVOrder.cPersonCode = Common.DB2String(order.Tables["head"].Rows[0]["cpersoncode"]);
            objAVOrder.cPayCode = Common.DB2String(order.Tables["head"].Rows[0]["cpaycode"]);
            objAVOrder.cSCCode = Common.DB2String(order.Tables["head"].Rows[0]["csccode"]);
            objAVOrder.cExch_Name = Common.DB2String(order.Tables["head"].Rows[0]["cexch_name"]);
            objAVOrder.iExchRate = Common.DB2Decimal(order.Tables["head"].Rows[0]["iexchrate"]);
            objAVOrder.iTaxRate = Common.DB2Decimal(order.Tables["head"].Rows[0]["itaxrate"]);
            objAVOrder.cMemo = Common.DB2String(order.Tables["head"].Rows[0]["cmemo"]);
            objAVOrder.cBusType = Common.DB2String(order.Tables["head"].Rows[0]["cbustype"]);
            objAVOrder.cMaker = Common.DB2String(order.Tables["head"].Rows[0]["cmaker"]);
            objAVOrder.bNegative = Common.DB2Int(order.Tables["head"].Rows[0]["bnegative"]);
            objAVOrder.Define1 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine1"]);
            objAVOrder.Define2 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine2"]);
            objAVOrder.Define3 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine3"]);
            objAVOrder.Define4 = Common.DB2DateTime(order.Tables["head"].Rows[0]["cdefine4"]);
            objAVOrder.Define5 = Common.DB2Int(order.Tables["head"].Rows[0]["cdefine5"]);
            objAVOrder.Define6 = Common.DB2DateTime(order.Tables["head"].Rows[0]["cdefine6"]);
            objAVOrder.Define7 = Common.DB2Int(order.Tables["head"].Rows[0]["cdefine7"]);
            objAVOrder.Define8 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine8"]);
            objAVOrder.Define9 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine9"]);
            objAVOrder.Define10 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine10"]);
            objAVOrder.Define11 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine11"]);
            objAVOrder.Define12 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine12"]);
            objAVOrder.Define13 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine13"]);
            objAVOrder.Define14 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine14"]);
            objAVOrder.Define15 = Common.DB2Int(order.Tables["head"].Rows[0]["cdefine15"]);
            objAVOrder.Define16 = Common.DB2Decimal(order.Tables["head"].Rows[0]["cdefine16"]);
            objAVOrder.cCloser = Common.DB2String(order.Tables["head"].Rows[0]["ccloser"]);
            objAVOrder.iDiscountTaxType = Common.DB2String(order.Tables["head"].Rows[0]["idiscounttaxtype"]);
            objAVOrder.iBillType = Common.DB2String(order.Tables["head"].Rows[0]["ibilltype"]);
            objAVOrder.cVouchType = Common.DB2String(order.Tables["head"].Rows[0]["cvouchtype"]);
            objAVOrder.cGeneralOrderCode = Common.DB2String(order.Tables["head"].Rows[0]["cgeneralordercode"]);
            objAVOrder.cTmCode = Common.DB2String(order.Tables["head"].Rows[0]["ctmcode"]);
            objAVOrder.cIncotermCode = Common.DB2String(order.Tables["head"].Rows[0]["cincotermcode"]);
            objAVOrder.cTransOrderCode = Common.DB2String(order.Tables["head"].Rows[0]["ctransordercode"]);
            objAVOrder.dPortDate = Common.DB2String(order.Tables["head"].Rows[0]["dportdate"]);
            objAVOrder.cSportCode = Common.DB2String(order.Tables["head"].Rows[0]["csportcode"]);
            objAVOrder.cAportCode = Common.DB2String(order.Tables["head"].Rows[0]["caportcode"]);
            objAVOrder.cSvenCode = Common.DB2String(order.Tables["head"].Rows[0]["csvencode"]);
            objAVOrder.cArrivalPlace = Common.DB2String(order.Tables["head"].Rows[0]["carrivalplace"]);
            objAVOrder.dCloseDate = Common.DB2String(order.Tables["head"].Rows[0]["dclosedate"]);
            objAVOrder.iDec = Common.DB2Int(order.Tables["head"].Rows[0]["idec"]);
            objAVOrder.bCal = Common.DB2Bool(order.Tables["head"].Rows[0]["bcal"]);
            objAVOrder.Guid = Common.DB2String(order.Tables["head"].Rows[0]["guid"]);
            objAVOrder.iVerifyState = Common.DB2Int(order.Tables["head"].Rows[0]["iverifystate"]);
            objAVOrder.cAuditDate = Common.DB2String(order.Tables["head"].Rows[0]["cauditdate"]);
            objAVOrder.cVerifier = Common.DB2String(order.Tables["head"].Rows[0]["cverifier"]);
            objAVOrder.iVerifyStateex = Common.DB2Int(order.Tables["head"].Rows[0]["iverifystateex"]);
            objAVOrder.iReturnCount = Common.DB2Int(order.Tables["head"].Rows[0]["ireturncount"]);
            objAVOrder.isWfContRolled = Common.DB2Bool(order.Tables["head"].Rows[0]["iswfcontrolled"]);
            objAVOrder.cVenPUOMProtocol = Common.DB2String(order.Tables["head"].Rows[0]["cvenpuomprotocol"]);
            objAVOrder.cChanger = Common.DB2String(order.Tables["head"].Rows[0]["cchanger"]);
            objAVOrder.iFlowId = Common.DB2Int(order.Tables["head"].Rows[0]["iflowid"]);
            objAVOrder.cVenName = Common.DB2String(order.Tables["head"].Rows[0]["cvenname"]);
            objAVOrder.cAddress = Common.DB2String(order.Tables["head"].Rows[0]["caddress"]);
            objAVOrder.ufts = Common.DB2String(order.Tables["head"].Rows[0]["ufts"]);

            #endregion
            #region Body
            objAVOrder.U8Details=new List<ArrivalVouchs>();
            
            foreach (DataRow dr in order.Tables["body"].Rows)
            {
                ArrivalVouchs objDetail = new ArrivalVouchs();
                ArrivalDetailRow(objDetail,dr,true);
                objAVOrder.U8Details.Add(objDetail);
            }

            #endregion
        }
        
        /// <summary>
        /// 转换（表体）
        /// </summary>
        /// <param name="avDetail"></param>
        /// <param name="dr"></param>
        /// <param name="type"></param>
        public static void ArrivalDetailRow(ArrivalVouchs avDetail, DataRow dr,bool type)
        {
            if (type)
            {
                #region Body_Detail

                avDetail.Autoid = Common.DB2Int(dr["autoid"]);
                avDetail.ID = Common.DB2Int(dr["id"]);
                avDetail.cWhCode = Common.DB2String(dr["cwhcode"]);
                avDetail.cInvCode = Common.DB2String(dr["cinvcode"]);
                avDetail.cInvName = Common.DB2String(dr["cinvname"]);
                avDetail.cInvStd = Common.DB2String(dr["cinvstd"]);
                avDetail.iNum = Common.DB2Decimal(dr["inum"]);
                avDetail.Quantity = Common.DB2Decimal(dr["iquantity"]);
                avDetail.iOriCost = Common.DB2Decimal(dr["ioricost"]);
                avDetail.iOriTaxCost = Common.DB2Decimal(dr["ioritaxcost"]);
                avDetail.iOriMoney = Common.DB2Decimal(dr["iorimoney"]);
                avDetail.iOriTaxPrice = Common.DB2Decimal(dr["ioritaxprice"]);
                avDetail.iOriSum = Common.DB2Decimal(dr["iorisum"]);
                avDetail.iCost = Common.DB2Decimal(dr["icost"]);
                avDetail.iMoney = Common.DB2Decimal(dr["imoney"]);
                avDetail.iTaxPrice = Common.DB2Decimal(dr["itaxprice"]);
                avDetail.iSum = Common.DB2Decimal(dr["isum"]);
                avDetail.Free1 = Common.DB2String(dr["cfree1"]);
                avDetail.Free2 = Common.DB2String(dr["cfree2"]);
                avDetail.Free3 = Common.DB2String(dr["cfree3"]);
                avDetail.Free4 = Common.DB2String(dr["cfree4"]);
                avDetail.Free5 = Common.DB2String(dr["cfree5"]);
                avDetail.Free6 = Common.DB2String(dr["cfree6"]);
                avDetail.Free7 = Common.DB2String(dr["cfree7"]);
                avDetail.Free8 = Common.DB2String(dr["cfree8"]);
                avDetail.Free9 = Common.DB2String(dr["cfree9"]);
                avDetail.Free10 = Common.DB2String(dr["cfree10"]);
                avDetail.iTaxRate = Common.DB2Decimal(dr["itaxrate"]);
                avDetail.Define22 = Common.DB2String(dr["cdefine22"]);
                avDetail.Define23 = Common.DB2String(dr["cdefine23"]);
                avDetail.Define24 = Common.DB2String(dr["cdefine24"]);
                avDetail.Define25 = Common.DB2String(dr["cdefine25"]);
                avDetail.Define26 = Common.DB2Decimal(dr["cdefine26"]);
                avDetail.Define27 = Common.DB2Decimal(dr["cdefine27"]);
                avDetail.Define28 = Common.DB2String(dr["cdefine28"]);
                avDetail.Define29 = Common.DB2String(dr["cdefine29"]);
                avDetail.Define30 = Common.DB2String(dr["cdefine30"]);
                avDetail.Define31 = Common.DB2String(dr["cdefine31"]);
                avDetail.Define32 = Common.DB2String(dr["cdefine32"]);
                avDetail.Define33 = Common.DB2String(dr["cdefine33"]);
                avDetail.Define34 = Common.DB2Int(dr["cdefine34"]);
                avDetail.Define35 = Common.DB2Int(dr["cdefine35"]);
                avDetail.Define36 = Common.DB2DateTime(dr["cdefine36"]);
                avDetail.Define37 = Common.DB2DateTime(dr["cdefine37"]);
                avDetail.cItem_class = Common.DB2String(dr["citem_class"]);
                avDetail.cItemCode = Common.DB2String(dr["citemcode"]);
                avDetail.iPOsID = Common.DB2Int(dr["iposid"]);
                avDetail.cItemName = Common.DB2String(dr["citemname"]);
                avDetail.cUnitID = Common.DB2String(dr["cunitid"]);
                avDetail.fKPQuantity = Common.DB2Decimal(dr["fkpquantity"]);
                avDetail.fRealQuantity = Common.DB2Decimal(dr["frealquantity"]);
                avDetail.fValidInQuan = Common.DB2Decimal(dr["fvalidInQuan"]);
                avDetail.finValidQuantity = Common.DB2Decimal(dr["finvalidquantity"]);
                avDetail.cCloser = Common.DB2String(dr["ccloser"]);
                avDetail.iCorId = Common.DB2Int(dr["icorid"]);
                avDetail.bGsp = Common.DB2Bool(dr["bgsp"]);
                //avDetail.cGsp = avDetail.bGsp ? "是" : "否";
                avDetail.cBatch = Common.DB2String(dr["cbatch"]);
                avDetail.dVDate = Common.DB2DateTime(dr["dvdate"]);
                avDetail.dPDate = Common.DB2DateTime(dr["dpdate"]);
                avDetail.fRefuseQuantity = Common.DB2Decimal(dr["frefusequantity"]);
                avDetail.cGspState = Common.DB2String(dr["cgspstate"]);
                avDetail.fInvalidInNum = Common.DB2Decimal(dr["finvalidnum"]);
                avDetail.bTaxCost = Common.DB2Bool(dr["btaxcost"]);
                avDetail.bInspect = Common.DB2String(dr["binspect"]);
                avDetail.fRefuseNum = Common.DB2Decimal(dr["frefusenum"]);
                avDetail.iPPartId = Common.DB2Int(dr["ippartid"]);
                avDetail.iPTOSeq = Common.DB2Int(dr["iptoseq"]);
                avDetail.SoDId = Common.DB2String(dr["sodid"]);
                avDetail.SoType = Common.DB2Int(dr["sotype"]);
                avDetail.ContractRowGUID = Common.DB2String(dr["contractrowguid"]);
                avDetail.iMassDate = Common.DB2Int(dr["imassdate"]);
                avDetail.cMassUnit = Common.DB2Int(dr["cmassunit"]);
                avDetail.bExigency = Common.DB2String(dr["bexigency"]);
                avDetail.cBcloser = Common.DB2String(dr["cbcloser"]);
                avDetail.fDTQuantity = Common.DB2Decimal(dr["fdtquantity"]);
                avDetail.fInvalidInNum = Common.DB2Decimal(dr["finvalidinnum"]);
                avDetail.fDegradeQuantity = Common.DB2Decimal(dr["fdegradequantity"]);
                avDetail.fDegradeNum = Common.DB2Decimal(dr["fdegradenum"]);
                avDetail.fDegradeInQuantity = Common.DB2Decimal(dr["fdegradeinquantity"]);
                avDetail.fDegradeInNum = Common.DB2Decimal(dr["fdegradeinnum"]);
                avDetail.fInspectQuantity = Common.DB2Decimal(dr["finspectquantity"]);
                avDetail.fInspectNum = Common.DB2Decimal(dr["finspectnum"]);
                avDetail.iInvMPCost = Common.DB2Decimal(dr["iinvmpcost"]);
                avDetail.Guids = Common.DB2String(dr["guids"]);
                avDetail.iInvexchRate = Common.DB2Decimal(dr["iinvexchrate"]);
                avDetail.Objectid_Source = Common.DB2String(dr["objectid_source"]);
                avDetail.Autoid_Source = Common.DB2Int(dr["autoid_source"]);
                avDetail.Ufts_Source = Common.DB2String(dr["ufts_source"]);
                avDetail.iRowno_Source = Common.DB2Int(dr["irowno_source"]);
                avDetail.cSoCode = Common.DB2String(dr["csocode"]);
                avDetail.iSoRowNo = Common.DB2Int(dr["isorowno"]);
                avDetail.iOrderId = Common.DB2Int(dr["iorderid"]);
                avDetail.cOrderCode = Common.DB2String(dr["cordercode"]);
                avDetail.iOrderRowNo = Common.DB2Int(dr["iorderrowno"]);
                avDetail.dLineCloseDate = Common.DB2String(dr["dlineclosedate"]);
                avDetail.ContractCode = Common.DB2String(dr["contractcode"]);
                avDetail.ContractRowNo = Common.DB2String(dr["contractrowno"]);
                avDetail.RejectSource = Common.DB2Bool(dr["rejectsource"]);
                avDetail.iCiqBookId = Common.DB2Int(dr["iciqbookid"]);
                avDetail.cCiqBookCode = Common.DB2String(dr["cciqbookcode"]);
                avDetail.cCiqCode = Common.DB2String(dr["cciqcode"]);
                avDetail.iRejectAutoId = Common.DB2Int(dr["irejectautoid"]);
                avDetail.iExpiratDateCalcu = Common.DB2Int(dr["iexpiratdatecalcu"]);
                avDetail.cExpirationDate = Common.DB2String(dr["cexpirationdate"]);
                avDetail.dExpirationDate = Common.DB2DateTime(dr["dexpirationdate"]);
                avDetail.cUpSoCode = Common.DB2String(dr["cupsocode"]);
                avDetail.iOrderdId = Common.DB2Int(dr["iorderdid"]);
                avDetail.iOrderType = Common.DB2Int(dr["iordertype"]);
                avDetail.cSoOrderCode = Common.DB2String(dr["csoordercode"]);
                avDetail.iOrderSeq = Common.DB2Int(dr["iorderseq"]);
                avDetail.BatchProperty1 = Common.DB2Decimal(dr["cbatchproperty1"]);
                avDetail.BatchProperty2 = Common.DB2Decimal(dr["cbatchproperty2"]);
                avDetail.BatchProperty3 = Common.DB2Decimal(dr["cbatchproperty3"]);
                avDetail.BatchProperty4 = Common.DB2Decimal(dr["cbatchproperty4"]);
                avDetail.BatchProperty5 = Common.DB2Decimal(dr["cbatchproperty5"]);
                avDetail.BatchProperty6 = Common.DB2String(dr["cbatchproperty6"]);
                avDetail.BatchProperty7 = Common.DB2String(dr["cbatchproperty7"]);
                avDetail.BatchProperty8 = Common.DB2String(dr["cbatchproperty8"]);
                avDetail.BatchProperty9 = Common.DB2String(dr["cbatchproperty9"]);
                avDetail.BatchProperty10 = Common.DB2String(dr["cbatchproperty10"]);
                avDetail.iVouchRowNo = Common.DB2Int(dr["ivouchrowno"]);
                avDetail.cVenName = Common.DB2String(dr["cvenname"]);
                avDetail.cVenAbbName = Common.DB2String(dr["cvenabbname"]);
                avDetail.cAddress = Common.DB2String(dr["caddress"]);
                avDetail.nQuantity = Common.DB2Decimal(dr["inquantity"]);
                avDetail.cInvm_Unit = Common.DB2String(dr["cinvm_unit"]);
                avDetail.dArriveDate = Common.DB2String(dr["darrivedate"]);
                avDetail.cDemandMemo = Common.DB2String(dr["cdemandmemo"]);
                avDetail.bInvBatch = Common.DB2Bool(dr["binvbatch"]);
                avDetail.OrderQuantity = Common.DB2Decimal(dr["orderquantity"]);
                //委外
                avDetail.cMoDetailsID = Common.DB2String(dr["modetailsID"]);

                #endregion
            }
            else
            {
                #region Body_Row

                dr["autoid"] = avDetail.Autoid;
                dr["id"] = avDetail.ID;
                dr["cwhcode"] = avDetail.cWhCode;
                dr["cinvcode"] = avDetail.cInvCode;
                dr["cinvname"] = avDetail.cInvName;
                dr["cinvstd"] = avDetail.cInvStd;
                dr["inum"] = avDetail.iNum;
                dr["iquantity"] = avDetail.Quantity;
                dr["ioricost"] = avDetail.iOriCost;
                dr["ioritaxcost"] = avDetail.iOriTaxCost;
                dr["iorimoney"] = avDetail.iOriMoney;
                dr["ioritaxprice"] = avDetail.iOriTaxPrice;
                dr["iorisum"] = avDetail.iOriSum;
                dr["icost"] = avDetail.iCost;
                dr["imoney"] = avDetail.iMoney;
                dr["itaxprice"] = avDetail.iTaxPrice;
                dr["isum"] = avDetail.iSum;
                dr["cfree1"] = avDetail.Free1;
                dr["cfree2"] = avDetail.Free2;
                dr["cfree3"] = avDetail.Free3;
                dr["cfree4"] = avDetail.Free4;
                dr["cfree5"] = avDetail.Free5;
                dr["cfree6"] = avDetail.Free6;
                dr["cfree7"] = avDetail.Free7;
                dr["cfree8"] = avDetail.Free8;
                dr["cfree9"] = avDetail.Free9;
                dr["cfree10"] = avDetail.Free10;
                dr["itaxrate"] = avDetail.iTaxRate;
                dr["cdefine22"] = avDetail.Define22;
                dr["cdefine23"] = avDetail.Define23;
                dr["cdefine24"] = avDetail.Define24;
                dr["cdefine25"] = avDetail.Define25;
                dr["cdefine26"] = avDetail.Define26;
                dr["cdefine27"] = avDetail.Define27;
                dr["cdefine28"] = avDetail.Define28;
                dr["cdefine29"] = avDetail.Define29;
                dr["cdefine30"] = avDetail.Define30;
                dr["cdefine31"] = avDetail.Define31;
                dr["cdefine32"] = avDetail.Define32;
                dr["cdefine33"] = avDetail.Define33;
                dr["cdefine34"] = avDetail.Define34;
                dr["cdefine35"] = avDetail.Define35;
                dr["cdefine36"] = avDetail.Define36;
                dr["cdefine37"] = avDetail.Define37;
                dr["citem_class"] = avDetail.cItem_class;
                dr["citemcode"] = avDetail.cItemCode;
                dr["iposid"] = avDetail.iPOsID;
                dr["citemname"] = avDetail.cItemName;
                dr["cunitid"] = avDetail.cUnitID;
                dr["fkpquantity"] = avDetail.fKPQuantity;
                dr["frealquantity"] = avDetail.fRealQuantity;
                dr["fValidQuantity"] = avDetail.fValidQuantity;
                dr["fvalidInQuan"] = avDetail.fValidInQuan;
                dr["finvalidquantity"] = avDetail.finValidQuantity;
                dr["ccloser"] = avDetail.cCloser;
                dr["icorid"] = avDetail.iCorId;
                dr["bgsp"] = avDetail.bGsp;
                dr["cbatch"] = avDetail.cBatch;
                dr["dvdate"] = avDetail.dVDate;
                dr["dpdate"] = avDetail.dPDate;
                dr["frefusequantity"] = avDetail.fRefuseQuantity;
                dr["cgspstate"] = avDetail.cGspState;
                dr["finvalidnum"] = avDetail.fInvalidInNum;
                dr["btaxcost"] = avDetail.bTaxCost;
                dr["binspect"] = avDetail.bInspect;
                dr["frefusenum"] = avDetail.fRefuseNum;
                dr["ippartid"] = avDetail.iPPartId;
                dr["iptoseq"] = avDetail.iPTOSeq;
                dr["sodid"] = avDetail.SoDId;
                dr["sotype"] = avDetail.SoType;
                dr["contractrowguid"] = avDetail.ContractRowGUID;
                dr["imassdate"] = avDetail.iMassDate;
                dr["cmassunit"] = avDetail.cMassUnit;
                dr["bexigency"] = avDetail.bExigency;
                dr["cbcloser"] = avDetail.cBcloser;
                dr["fdtquantity"] = avDetail.fDTQuantity;
                dr["finvalidinnum"] = avDetail.fInvalidInNum;
                dr["fdegradequantity"] = avDetail.fDegradeQuantity;
                dr["fdegradenum"] = avDetail.fDegradeNum;
                dr["fdegradeinquantity"] = avDetail.fDegradeInQuantity;
                dr["fdegradeinnum"] = avDetail.fDegradeInNum;
                dr["finspectquantity"] = avDetail.fInspectQuantity;
                dr["finspectnum"] = avDetail.fInspectNum;
                dr["iinvmpcost"] = avDetail.iInvMPCost;
                dr["guids"] = avDetail.Guids;
                dr["iinvexchrate"] = avDetail.iInvexchRate;
                dr["objectid_source"] = avDetail.Objectid_Source;
                dr["autoid_source"] = avDetail.Autoid_Source;
                dr["ufts_source"] = avDetail.Ufts_Source;
                dr["irowno_source"] = avDetail.iRowno_Source;
                dr["csocode"] = avDetail.cSoCode;
                dr["isorowno"] = avDetail.iSoRowNo;
                dr["iorderid"] = avDetail.iOrderId;
                dr["cordercode"] = avDetail.cOrderCode;
                dr["iorderrowno"] = avDetail.iOrderRowNo;
                dr["dlineclosedate"] = avDetail.dLineCloseDate;
                dr["contractcode"] = avDetail.ContractCode;
                dr["contractrowno"] = avDetail.ContractRowNo;
                dr["rejectsource"] = avDetail.RejectSource;
                dr["iciqbookid"] = avDetail.iCiqBookId;
                dr["cciqbookcode"] = avDetail.cCiqBookCode;
                dr["cciqcode"] = avDetail.cCiqCode;
                dr["irejectautoid"] = avDetail.iRejectAutoId;
                dr["iexpiratdatecalcu"] = avDetail.iExpiratDateCalcu;
                dr["cexpirationdate"] = avDetail.cExpirationDate;
                dr["dexpirationdate"] = avDetail.dExpirationDate;
                dr["cupsocode"] = avDetail.cUpSoCode;
                dr["iorderdid"] = avDetail.iOrderdId;
                dr["iordertype"] = avDetail.iOrderType;
                dr["csoordercode"] = avDetail.cSoOrderCode;
                dr["iorderseq"] = avDetail.iOrderSeq;
                dr["cbatchproperty1"] = avDetail.BatchProperty1;
                dr["cbatchproperty2"] = avDetail.BatchProperty2;
                dr["cbatchproperty3"] = avDetail.BatchProperty3;
                dr["cbatchproperty4"] = avDetail.BatchProperty4;
                dr["cbatchproperty5"] = avDetail.BatchProperty5;
                dr["cbatchproperty6"] = avDetail.BatchProperty6;
                dr["cbatchproperty7"] = avDetail.BatchProperty7;
                dr["cbatchproperty8"] = avDetail.BatchProperty8;
                dr["cbatchproperty9"] = avDetail.BatchProperty9;
                dr["cbatchproperty10"] = avDetail.BatchProperty10;
                dr["ivouchrowno"] = avDetail.iVouchRowNo;
                dr["cvenname"] = avDetail.cVenName;
                dr["caddress"] = avDetail.cAddress;
                dr["inquantity"] = avDetail.nQuantity;
                dr["cinvm_unit"] = avDetail.cVenName;
                dr["darrivedate"] = avDetail.cAddress;
                dr["cdemandmemo"] = avDetail.nQuantity;
                dr["binvbatch"] = avDetail.bInvBatch;
                dr["orderquantity"] = avDetail.OrderQuantity;
                //委外
                dr["modetailsID"] = avDetail.cMoDetailsID;

                #endregion
            }
        }

        /// <summary>
        /// 转换（表头）实体－－>表
        /// </summary>
        /// <param name="objAVOrder"></param>
        /// <param name="order"></param>
        private static void ArrivalVouchResult(ArrivalVouch objAVOrder, DataSet order)
        {
            #region Head
            DataRow drMain = order.Tables["Head"].NewRow();
            drMain["ivtid"] = objAVOrder.VT_ID;
            drMain["id"] = objAVOrder.ID;
            drMain["ccode"] = objAVOrder.cCode;
            drMain["cptcode"] = objAVOrder.cPTCode;
            drMain["ddate"] = objAVOrder.dDate;
            drMain["cvencode"] = objAVOrder.cVenCode;
            drMain["cdepcode"] = objAVOrder.cDepCode;
            drMain["cpersoncode"] = objAVOrder.cPersonCode;
            drMain["cpaycode"] = objAVOrder.cPayCode;
            drMain["csccode"] = objAVOrder.cSCCode;
            drMain["cexch_name"] = objAVOrder.cExch_Name;
            drMain["iexchrate"] = objAVOrder.iExchRate;
            drMain["itaxrate"] = objAVOrder.iTaxRate;
            drMain["cmemo"] = objAVOrder.cMemo;
            drMain["cbustype"] = objAVOrder.cBusType;
            drMain["cmaker"] = objAVOrder.cMaker;
            drMain["bnegative"] = objAVOrder.bNegative;
            drMain["cdefine1"] = objAVOrder.Define1;
            drMain["cdefine2"] = objAVOrder.Define2;
            drMain["cdefine3"] = objAVOrder.Define3;
            drMain["cdefine4"] = objAVOrder.Define4;
            drMain["cdefine5"] = objAVOrder.Define5;
            drMain["cdefine6"] = objAVOrder.Define6;
            drMain["cdefine7"] = objAVOrder.Define7;
            drMain["cdefine8"] = objAVOrder.Define8;
            drMain["cdefine9"] = objAVOrder.Define9;
            drMain["cdefine10"] = objAVOrder.Define10;
            drMain["cdefine11"] = objAVOrder.Define11;
            drMain["cdefine12"] = objAVOrder.Define12;
            drMain["cdefine13"] = objAVOrder.Define13;
            drMain["cdefine14"] = objAVOrder.Define14;
            drMain["cdefine15"] = objAVOrder.Define15;
            drMain["cdefine16"] = objAVOrder.Define16;
            drMain["ccloser"] = objAVOrder.cCloser;
            drMain["idiscounttaxtype"] = objAVOrder.iDiscountTaxType;
            drMain["ibilltype"] = objAVOrder.iBillType;
            drMain["cvouchtype"] = objAVOrder.cVouchType;
            drMain["cgeneralordercode"] = objAVOrder.cGeneralOrderCode;
            drMain["ctmcode"] = objAVOrder.cTmCode;
            drMain["cincotermcode"] = objAVOrder.cIncotermCode;
            drMain["ctransordercode"] = objAVOrder.cTransOrderCode;
            drMain["dportdate"] = objAVOrder.dPortDate;
            drMain["csportcode"] = objAVOrder.cSportCode;
            drMain["caportcode"] = objAVOrder.cAportCode;
            drMain["csvencode"] = objAVOrder.cSvenCode;
            drMain["carrivalplace"] = objAVOrder.cArrivalPlace;
            drMain["dclosedate"] = objAVOrder.dCloseDate;
            drMain["idec"] = objAVOrder.iDec;
            drMain["bcal"] = objAVOrder.bCal;
            drMain["guid"] = objAVOrder.Guid;
            drMain["iverifystate"] = objAVOrder.iVerifyState;
            drMain["cauditdate"] = objAVOrder.cAuditDate;
            drMain["cverifier"] = objAVOrder.cVerifier;
            drMain["iverifystateex"] = objAVOrder.iVerifyStateex;
            drMain["ireturncount"] = objAVOrder.iReturnCount;
            drMain["iswfcontrolled"] = objAVOrder.isWfContRolled;
            drMain["cvenpuomprotocol"] = objAVOrder.cVenPUOMProtocol;
            drMain["cchanger"] = objAVOrder.cChanger;
            drMain["iflowid"] = objAVOrder.iFlowId;
            drMain["cvenname"] = objAVOrder.cVenName;
            drMain["caddress"] = objAVOrder.cAddress;
            drMain["csccode"] = objAVOrder.cSCCode;
            drMain["ufts"] = objAVOrder.ufts;

            order.Tables["Head"].Rows.Add(drMain);
            #endregion
            #region Body
            foreach (ArrivalVouchs objAVDetail in objAVOrder.OperateDetails)
            {
                DataRow drDetail = order.Tables["Body"].NewRow();
                ArrivalDetailRow(objAVDetail, drDetail, false);
                order.Tables["Body"].Rows.Add(drDetail);
            }

            #endregion
        }
    }
}