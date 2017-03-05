using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

using U8Business.Service;
using Model;

namespace U8Business
{
    public class StockInBusiness
    {
        #region Function

        /// <summary>
        /// 取得上游单据列表，供单据选择画面使用。
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="Type"></param>
        /// <param name="OrderList"></param>
        public static void GetOrderList(string objType, out DataSet OrderList)
        {
             //Common co = Common.GetCommon();

            string errMsg = "";//出错信息
            OrderList = new System.Data.DataSet();
            //co.Service.GetStockInList(Common.CurrentUser.ConnectionString, out OrderList, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 根据取得的数据集，生成SIORDER对像。
        /// 生成入库单对象
        /// </summary>
        /// <param name="order"></param>
        public static void StockInTable(StockIn stock,DataSet order)
        {
            if (order.Tables["head"].Rows.Count > 0)
            {
                #region Head

                stock.Isstqc = Common.DB2Bool(order.Tables["head"].Rows[0]["bisstqc"]);
                stock.Pufirst = Common.DB2Bool(order.Tables["head"].Rows[0]["bpufirst"]);
                stock.Rdflag = Common.DB2Int(order.Tables["head"].Rows[0]["brdflag"]);
                stock.Accounter = Common.DB2String(order.Tables["head"].Rows[0]["caccounter"]);
                stock.Arvcode = Common.DB2String(order.Tables["head"].Rows[0]["carvcode"]);
                stock.Billcode = Common.DB2String(order.Tables["head"].Rows[0]["cbillcode"]);
                stock.Buscode = Common.DB2String(order.Tables["head"].Rows[0]["cbuscode"]);
                stock.Bustype = Common.DB2String(order.Tables["head"].Rows[0]["cbustype"]);
                stock.Chkcode = Common.DB2String(order.Tables["head"].Rows[0]["cchkcode"]);
                stock.Chkperson = Common.DB2String(order.Tables["head"].Rows[0]["cchkperson"]);
                stock.Code = Common.DB2String(order.Tables["head"].Rows[0]["ccode"]);
                stock.Define1 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine1"]);
                stock.Define10 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine10"]);
                stock.Define11 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine11"]);
                stock.Define12 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine12"]);
                stock.Define13 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine13"]);
                stock.Define14 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine14"]);
                stock.Define15 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine15"]);
                stock.Define16 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine16"]);
                stock.Define2 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine2"]);
                stock.Define3 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine3"]);
                stock.Define4 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine4"]);
                stock.Define5 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine5"]);
                stock.Define6 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine6"]);
                stock.Define7 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine7"]);
                stock.Define8 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine8"]);
                stock.Define9 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine9"]);
                stock.Depcode = Common.DB2String(order.Tables["head"].Rows[0]["cdepcode"]);
                stock.Depname = Common.DB2String(order.Tables["head"].Rows[0]["cdepname"]);
                stock.Exch_name = Common.DB2String(order.Tables["head"].Rows[0]["cexch_name"]);
                stock.Gspcheck = Common.DB2String(order.Tables["head"].Rows[0]["gspcheck"]);
                stock.Handler = Common.DB2String(order.Tables["head"].Rows[0]["chandler"]);
                stock.Maker = Common.DB2String(order.Tables["head"].Rows[0]["cmaker"]);
                stock.Memo = Common.DB2String(order.Tables["head"].Rows[0]["cmemo"]);
                stock.Ordercode = Common.DB2String(order.Tables["head"].Rows[0]["cordercode"]);
                stock.Personcode = Common.DB2String(order.Tables["head"].Rows[0]["cpersoncode"]);
                stock.Personname = Common.DB2String(order.Tables["head"].Rows[0]["cpersonname"]);
                stock.Ptcode = Common.DB2String(order.Tables["head"].Rows[0]["cptcode"]);
                stock.Ptname = Common.DB2String(order.Tables["head"].Rows[0]["cptname"]);
                stock.Rdcode = Common.DB2String(order.Tables["head"].Rows[0]["crdcode"]);
                stock.Rdname = Common.DB2String(order.Tables["head"].Rows[0]["crdname"]);
                stock.Source = Common.DB2String(order.Tables["head"].Rows[0]["csource"]);
                stock.Vencode = Common.DB2String(order.Tables["head"].Rows[0]["cvencode"]);
                stock.Vouchtype = Common.DB2String(order.Tables["head"].Rows[0]["cvouchtype"]);
                stock.WhPos = Common.DB2Bool(order.Tables["head"].Rows[0]["bwhpos"]);
                stock.Whcode = Common.DB2String(order.Tables["head"].Rows[0]["cwhcode"]);
                stock.Whname = Common.DB2String(order.Tables["head"].Rows[0]["cwhname"]);
                stock.Arvdate = Common.DB2String(order.Tables["head"].Rows[0]["darvdate"]).Trim();
                stock.Chkdate = Common.DB2String(order.Tables["head"].Rows[0]["dchkdate"]);
                stock.Date = Common.DB2String(order.Tables["head"].Rows[0]["ddate"]);
                stock.Veridate = Common.DB2String(order.Tables["head"].Rows[0]["dveridate"]);
                stock.Arriveid = Common.DB2Int(order.Tables["head"].Rows[0]["iarriveid"]);
                stock.Avanum = Common.DB2Decimal(order.Tables["head"].Rows[0]["iavanum"]);
                stock.Avaquantity = Common.DB2Decimal(order.Tables["head"].Rows[0]["iavaquantity"]);
                stock.ID = Common.DB2Int(order.Tables["head"].Rows[0]["id"]);
                stock.Discounttaxtype = Common.DB2Int(order.Tables["head"].Rows[0]["idiscounttaxtype"]);
                stock.Exchrate = Common.DB2Decimal(order.Tables["head"].Rows[0]["iexchrate"]);
                stock.Lowsum = Common.DB2Decimal(order.Tables["head"].Rows[0]["iLowsum"]);
                stock.Present = Common.DB2Int(order.Tables["head"].Rows[0]["ipresent"]);
                stock.Presentnum = Common.DB2Int(order.Tables["head"].Rows[0]["ipresentnum"]);
                stock.ProOrderId = Common.DB2Int(order.Tables["head"].Rows[0]["iproorderid"]);
                stock.Purarriveid = Common.DB2Int(order.Tables["head"].Rows[0]["ipurarriveid"]);
                stock.Purorderid = Common.DB2Int(order.Tables["head"].Rows[0]["ipurorderid"]);
                stock.Returncount = Common.DB2Int(order.Tables["head"].Rows[0]["ireturncount"]);
                stock.Safesum = Common.DB2Decimal(order.Tables["head"].Rows[0]["isafesum"]);
                stock.Salebillid = Common.DB2Int(order.Tables["head"].Rows[0]["isalebillid"]);
                stock.Swfcontrolled = Common.DB2Int(order.Tables["head"].Rows[0]["iswfcontrolled"]);
                stock.Taxrate = Common.DB2Decimal(order.Tables["head"].Rows[0]["itaxrate"]);
                stock.Topsum = Common.DB2Decimal(order.Tables["head"].Rows[0]["itopsum"]);
                stock.Verifystate = Common.DB2Int(order.Tables["head"].Rows[0]["iverifystate"]);
                stock.Venname = Common.DB2String(order.Tables["head"].Rows[0]["cvenname"]);
                stock.VouchRowNo = Common.DB2Int(order.Tables["head"].Rows[0]["ivouchrowno"]);
                stock.BredVouch = Common.DB2String(order.Tables["head"].Rows[0]["bredvouch"]);
                stock.Venabbname = Common.DB2String(order.Tables["head"].Rows[0]["cvenabbname"]);
                //材料
                stock.PspCode = Common.DB2String(order.Tables["head"].Rows[0]["cPsPcode"]);
                stock.MQuantity = Common.DB2Decimal(order.Tables["head"].Rows[0]["iMQuantity"]);
                stock.MpoCode = Common.DB2String(order.Tables["head"].Rows[0]["cMPoCode"]);

                #endregion

                //stock.Buscode = stock.Purarriveid.ToString();

                stock.Ufts = Common.DB2String(order.Tables["head"].Rows[0]["Ufts"]);
                stock.VT_id = Common.DB2Int(order.Tables["head"].Rows[0]["vt_id"]);
            }

            stock.U8Details = new List<StockInDetail>();

            foreach (DataRow dr in order.Tables["body"].Rows)
            {
                StockInDetail objDetail = new StockInDetail();
                StockInDetailRow(objDetail,dr);
                stock.U8Details.Add(objDetail);
            }

            stock.OperateDetails = new List<StockInDetail>();
        }

        /// <summary>
        /// 根据单据选择画面选择的数据，生成或合并入库单。
        /// </summary>
        /// <param name="OrderCodeList"></param>
        /// <param name="Type"></param>
        /// <param name="CurrentUser"></param>
        /// <returns></returns>
        public static StockIn CreateSIOrder(string cinvcode, string cbatch, string CheckCode, string arriveCode)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet order = new System.Data.DataSet();

            co.Service.CreateSIOrderByQMCheckOrder(cinvcode, cbatch, CheckCode, arriveCode, Common.CurrentUser.ConnectionString, out order, out errMsg);

            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        /// <summary>
        /// 根据到货单生成入库单信息
        /// </summary>
        /// <param name="ccode">到货单号</param>
        /// <param name="POCode">订单号</param>
        /// <param name="cinvcode">存货编号</param>
        /// <param name="isReturn">0到货1退货</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static StockIn CreateSIOrderArrive(string ccode, string POCode, string cinvcode, int isReturn, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateSIOrderByArriveOrder(ccode, POCode, cinvcode, isReturn, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        /// <summary>
        /// 根据GSP生成入库单
        /// </summary>
        /// <param name="cCode">GSP单号</param>
        /// <param name="sCode">到货单号</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static StockIn CreateSIOrderByGSPVouch(string cCode, string sCode, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateSIOrderByGSPVouch(cCode, sCode, Common.CurrentUser.ConnectionString, out order, out errMsg);

            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        /// <summary>
        /// 根据委外到货单生成入库单
        /// </summary>
        /// <param name="ccode">委外到货单号</param>
        /// <param name="POCode">委外订单号</param>
        /// <param name="cinvcode">存货编码</param>
        /// <param name="isReturn">0到货1退货</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static StockIn CreateSIOrderOSArrive(string ccode, string POCode, string cinvcode, int isReturn, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateSIOrderByOSArriveOrder(ccode, POCode, cinvcode, isReturn, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        /// <summary>
        /// 根据组装/拆卸/调拨出入库单生成入库单
        /// </summary>
        /// <param name="type">单据类型0拆卸1组装2调拨,0入库1出库:"00"为拆卸入库</param>
        /// <param name="ccode">其他出入库单号</param>
        /// <param name="bcode">拆卸组装调拨单号</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static StockIn CreateSIOrderByDismantle(string type, string ccode, string bcode, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";
            order = null;

            co.Service.GetRecordList(type, ccode, bcode, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        /// <summary>
        /// 提交入库单信息
        /// </summary>
        /// <param name="stock">入库信息</param>
        /// <param name="sourceVoucher">入库类型</param>
        public static void Save(StockIn stock,string sourceVoucher)
        {
            if (stock.OperateDetails.Count == 0)
            {
                throw new Exception("请操作数据后再提交");
            }
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息

            DataTable dtiquantity = new DataTable("iquantity");//单计量特别操作
            dtiquantity.Columns.Add("iquantity");
            //DataRow driquantity;


            DataSet dtResult = BuildStockInStruct();

            #region 表头部分

            DataRow drMain = dtResult.Tables[0].NewRow();

            drMain["id"] = stock.ID;
            drMain["csource"] = stock.Source;
            //drMain["bisstqc"] = stock.m_bIsstqc;
            //drMain["bpufirst"] = stock.m_bPufirst;
            drMain["brdflag"] = "1";
            drMain["caccounter"] = stock.Accounter;
            drMain["carvcode"] = stock.Arvcode;
            //drMain["cbillcode"] = stock.Billcode;
            drMain["cbuscode"] = stock.Buscode;
            drMain["cbustype"] = stock.Bustype;
            drMain["cchkcode"] = stock.Chkcode;
            drMain["cchkperson"] = stock.Chkperson;
            drMain["ccode"] = stock.Code;
            drMain["cdefine1"] = stock.Define1;
            drMain["cdefine2"] = stock.Define2;
            drMain["cdefine3"] = stock.Define3;
            drMain["cdefine4"] = stock.Define4;
            drMain["cdefine5"] = stock.Define5;
            drMain["cdefine6"] = stock.Define6;
            drMain["cdefine7"] = stock.Define7;
            drMain["cdefine8"] = stock.Define8;
            drMain["cdefine9"] = stock.Define9;
            drMain["cdefine10"] = stock.Define10;
            drMain["cdefine11"] = stock.Define11;
            drMain["cdefine12"] = stock.Define12;
            drMain["cdefine13"] = stock.Define13;
            drMain["cdefine14"] = stock.Define14;
            drMain["cdefine15"] = stock.Define15;
            drMain["cdefine16"] = stock.Define16;
            drMain["cdepcode"] = stock.Depcode;
            drMain["cdepname"] = stock.Depname;
            drMain["cexch_name"] = stock.Exch_name;
            drMain["chandler"] = stock.Handler;
            drMain["cmaker"] = stock.Maker;
            drMain["cmemo"] = stock.Memo;
            drMain["cordercode"] = stock.Ordercode;
            drMain["cpersoncode"] = stock.Personcode;
            drMain["cpersonname"] = stock.Personname;
            drMain["cptcode"] = stock.Ptcode;
            drMain["cptname"] = stock.Ptname;
            drMain["crdcode"] = stock.Rdcode;
            drMain["crdname"] = stock.Rdname;
            drMain["cvenabbname"] = stock.Venabbname;
            drMain["cvencode"] = stock.Vencode;
            drMain["cvenname"] = stock.Venname;
            drMain["cvouchtype"] = stock.Vouchtype;
            drMain["cwhcode"] = stock.Whcode;
            drMain["cwhname"] = stock.Whname;
            drMain["darvdate"] = stock.Arvdate;
            drMain["dchkdate"] = stock.Chkdate;
            drMain["ddate"] = stock.Date;
            drMain["dveridate"] = stock.Veridate;
            //drMain["gspcheck"] = stock.Gspcheck;
            //drMain["iarriveid"] = stock.Arriveid;
            //drMain["iavanum"] = stock.Avanum;
            //drMain["iavaquantity"] = stock.Avaquantity;
            drMain["idiscounttaxtype"] = stock.Discounttaxtype;
            drMain["iexchrate"] = stock.Exchrate;
            //drMain["ilowsum"] = stock.Lowsum;
            //drMain["ipresent"] = stock.Present;
            //drMain["ipresentnum"] = stock.Presentnum;
            drMain["iproorderid"] = stock.ProOrderId;
            drMain["ipurarriveid"] = stock.Purarriveid;
            drMain["ipurorderid"] = stock.Purorderid;
            //drMain["ireturncount"] = stock.Returncount;
            //drMain["isafesum"] = stock.Safesum;
            //drMain["isalebillid"] = stock.Salebillid;
            //drMain["iswfcontrolled"] = stock.Swfcontrolled;
            drMain["itaxrate"] = stock.Taxrate;
            drMain["dnmaketime"] = stock.nmaketime;
            drMain["cVouchName"] = stock.Venname;
            //drMain["iverifystate"] = stock.Verifystate;
            drMain["ufts"] = stock.Ufts;
            drMain["dnverifytime"] = stock.nverifytime;
            drMain["bredvouch"] = stock.BredVouch;
            //材料
            drMain["cpspcode"] = stock.PspCode;
            drMain["iMQuantity"] = stock.MQuantity;
            drMain["cMPoCode"] = stock.MpoCode;
            dtResult.Tables[0].Rows.Add(drMain);

            #endregion
            
            #region 表体部分

            foreach (StockInDetail objDetailTemp in stock.OperateDetails)
            {
                DataRow dr = dtResult.Tables[1].NewRow();
                dr["id"] = objDetailTemp.ID;
                dr["autoid"] = objDetailTemp.AutoID;
                //dr["bcosting"] = objDetailTemp.Costing;
                //dr["binvbatch"] = objDetailTemp.Invbatch;
                //dr["binvtype"] = objDetailTemp.Invtype;
                //dr["bservice"] = objDetailTemp.Service;
                dr["btaxcost"] = objDetailTemp.Taxcost;
                //dr["bvmiused"] = objDetailTemp.Vmiused;
                dr["cassunit"] = objDetailTemp.Assunit;
                //dr["cbaccounter"] = objDetailTemp.Baccounter;
                dr["cbarcode"] = objDetailTemp.Barcode;
                dr["cbatch"] = objDetailTemp.Batch;
                dr["cbvencode"] = objDetailTemp.Bvencode;
                dr["ccheckcode"] = objDetailTemp.Checkcode;
                dr["ccheckpersoncode"] = objDetailTemp.Checkpersoncode;
                dr["ccheckpersonname"] = objDetailTemp.Checkpersonname;
                dr["cdefine22"] = objDetailTemp.Define22;
                dr["cdefine23"] = objDetailTemp.Define23;
                dr["cdefine24"] = objDetailTemp.Define24;
                dr["cdefine25"] = objDetailTemp.Define25;
                dr["cdefine26"] = objDetailTemp.Define26;
                dr["cdefine27"] = objDetailTemp.Define27;
                dr["cdefine28"] = objDetailTemp.Define28;
                dr["cdefine29"] = objDetailTemp.Define29;
                dr["cdefine30"] = objDetailTemp.Define30;
                dr["cdefine31"] = objDetailTemp.Define31;
                dr["cdefine32"] = objDetailTemp.Define32;
                dr["cdefine33"] = objDetailTemp.Define33;
                dr["cdefine34"] = objDetailTemp.Define34;
                dr["cdefine35"] = objDetailTemp.Define35;
                dr["cdefine36"] = objDetailTemp.Define36;
                dr["cdefine37"] = objDetailTemp.Define37;
                dr["cfree1"] = objDetailTemp.Free1;
                dr["cfree2"] = objDetailTemp.Free2;
                dr["cfree3"] = objDetailTemp.Free3;
                dr["cfree4"] = objDetailTemp.Free4;
                dr["cfree5"] = objDetailTemp.Free5;
                dr["cfree6"] = objDetailTemp.Free6;
                dr["cfree7"] = objDetailTemp.Free7;
                dr["cfree8"] = objDetailTemp.Free8;
                dr["cfree9"] = objDetailTemp.Free9;
                dr["cfree10"] = objDetailTemp.Free10;
                dr["cgspstate"] = objDetailTemp.Gspstate;
                //dr["cinva_unit"] = objDetailTemp.Inva_unit;
                //dr["cinvaddcode"] = objDetailTemp.Invaddcode;
                dr["cinvccode"] = objDetailTemp.InvCCode;
                dr["cinvcode"] = objDetailTemp.cInvCode;
                dr["cinvdefine1"] = objDetailTemp.Invdefine1;
                dr["cinvdefine2"] = objDetailTemp.Invdefine2;
                dr["cinvdefine3"] = objDetailTemp.Invdefine3;
                dr["cinvdefine4"] = objDetailTemp.Invdefine4;
                dr["cinvdefine5"] = objDetailTemp.Invdefine5;
                dr["cinvdefine6"] = objDetailTemp.Invdefine6;
                dr["cinvdefine7"] = objDetailTemp.Invdefine7;
                dr["cinvdefine8"] = objDetailTemp.Invdefine8;
                dr["cinvdefine9"] = objDetailTemp.Invdefine9;
                dr["cinvdefine10"] = objDetailTemp.Invdefine10;
                dr["cinvdefine11"] = objDetailTemp.Invdefine11;
                dr["cinvdefine12"] = objDetailTemp.Invdefine12;
                dr["cinvdefine13"] = objDetailTemp.Invdefine13;
                dr["cinvdefine14"] = objDetailTemp.Invdefine14;
                dr["cinvdefine15"] = objDetailTemp.Invdefine15;
                dr["cinvdefine16"] = objDetailTemp.Invdefine16;
                dr["cinvm_unit"] = objDetailTemp.Invm_unit;
                dr["cinvname"] = objDetailTemp.Invname;
                dr["cinvouchcode"] = objDetailTemp.Invouchcode;
                //dr["cinvstd"] = objDetailTemp.Invstd;
                //dr["citem_class"] = objDetailTemp.Item_class;
                //dr["citemcname"] = objDetailTemp.Itemcname;
                //dr["citemcode"] = objDetailTemp.Itemcode;
                dr["cmassunit"] = objDetailTemp.Massunit;
                //dr["cname"] = objDetailTemp.Name;
                dr["corufts"] = objDetailTemp.Orufts;
                dr["cpoid"] = objDetailTemp.Poid;
                dr["cposition"] = objDetailTemp.Position;
                dr["cposname"] = objDetailTemp.Posname;
                dr["crejectcode"] = objDetailTemp.Rejectcode;
                dr["creplaceitem"] = objDetailTemp.Replaceitem;
                //dr["csocode"] = objDetailTemp.Socode;
                dr["cveninvcode"] = objDetailTemp.Veninvcode;
                dr["cveninvname"] = objDetailTemp.Veninvname;
                dr["cvencode"] = objDetailTemp.VenCode;
                dr["cvenname"] = objDetailTemp.Venname;
                //dr["cvmivencode"] = objDetailTemp.Vmivencode;
                //dr["cvmivenname"] = objDetailTemp.Vmivenname;
                //dr["cvouchcode"] = objDetailTemp.Vouchcode;
                dr["dcheckdate"] = objDetailTemp.CheckDate;
                dr["dmadedate"] = objDetailTemp.Madedate;
                //dr["dmsdate"] = objDetailTemp.Msdate;
                dr["dsdate"] = objDetailTemp.Sdate;
                dr["dvdate"] = objDetailTemp.Vdate;
                //dr["facost"] = objDetailTemp.Oricost;//money
                dr["facost"] = objDetailTemp.Unitcost;
                dr["iaprice"] = objDetailTemp.Unitcost * objDetailTemp.Quantity;//money
                dr["iarrsid"] = objDetailTemp.Arrsid;
                dr["icheckidbaks"] = objDetailTemp.Checkidbaks;
                dr["icheckids"] = objDetailTemp.Checkids;
                dr["iflag"] = "";
                //dr["ifnum"] = objDetailTemp.Fnum;
                //dr["ifquantity"] = objDetailTemp.Fquantity;
                //dr["iimbsid"] = objDetailTemp.Imbsid;
                //dr["iimosid"] = objDetailTemp.Imosid;
                dr["iinvexchrate"] = objDetailTemp.Invexchrate;
                //dr["iinvsncount"] = objDetailTemp.Invsncount;
                dr["imassdate"] = objDetailTemp.Massdate;
                //dr["imaterialfee"] = objDetailTemp.Materialfee;
                dr["imoney"] = objDetailTemp.Money;
                //dr["impcost"] = objDetailTemp.Mpcost;
                //dr["impoids"] = objDetailTemp.Mpoids;
                //dr["innum"] = objDetailTemp.Nnum;
                dr["inquantity"] = objDetailTemp.fShallInQuan;
                dr["inum"] = objDetailTemp.Num;
                dr["iomodid"] = objDetailTemp.Omodid;
                dr["ioricost"] = objDetailTemp.Oricost;
                dr["iorimoney"] = objDetailTemp.Orimoney;//money
                dr["iorisum"] = objDetailTemp.Orisum;//money
                dr["ioritaxcost"] = objDetailTemp.Oritaxcost;
                dr["ioritaxprice"] = objDetailTemp.Oritaxprice;//money
                dr["iposid"] = objDetailTemp.Posid;
                //dr["ipprice"]= objDetailTemp.Pprice;
                //dr["iprice"] = objDetailTemp.Oricost*objDetailTemp.Quantity;//money
                dr["iprice"] = objDetailTemp.Price;//money
                //dr["iprocesscost"] = objDetailTemp.Processcost;
                //dr["iprocessfee"] = objDetailTemp.Processfee;
                //dr["ipunitcost"] = objDetailTemp.Punitcost;
                dr["iquantity"] = objDetailTemp.Quantity;
                dr["irejectids"] = objDetailTemp.Rejectids;
                //dr["ismaterialfee"] = objDetailTemp.Smaterialfee;
                dr["isnum"] = objDetailTemp.Snum;
                dr["isodid"] = objDetailTemp.Sodid;
                //dr["isoseq"] = objDetailTemp.Soseq;
                dr["isotype"] = objDetailTemp.Sotype;
                //dr["isoutnum"] = objDetailTemp.Soutnum;
                //dr["isoutquantity"] = objDetailTemp.Soutquantity;
                //dr["isprocessfee"] = objDetailTemp.Sprocessfee;
                dr["isquantity"] = objDetailTemp.Squantity;
                dr["isum"] = objDetailTemp.Sum;//money
                //dr["isumbillquantity"] = objDetailTemp.Sumbillquantity;
                //dr["itax"] = objDetailTemp.Tax;
                //dr["itaxprice"] = (objDetailTemp.Oritaxcost - objDetailTemp.Oricost)*objDetailTemp.Quantity;//money
                dr["itaxprice"] = objDetailTemp.Taxprice;//money
                dr["itaxrate"] = objDetailTemp.Taxrate;
                //dr["itrids"] = objDetailTemp.Trids;
                //dr["iunitcost"] = objDetailTemp.Oricost;//money
                dr["iunitcost"] = objDetailTemp.Unitcost;//money
                dr["cbarvcode"] = objDetailTemp.barvcode;
                dr["dbarvdate"] = objDetailTemp.barvdate;
                //dr["inetlock"] = objDetailTemp.n
                //dr["ivmisettlenum"] = objDetailTemp.Vmisettlenum;
                //dr["ivmisettlequantity"] = objDetailTemp.Vmisettlequantity;
                //dr["scrapufts"] = objDetailTemp.Scrapufts;
                //dr["strcode"]=objDetailTemp.StrCode;

                //dr["strcontractid"] = objDetailTemp.Contractid;
                dr["irowno"] = objDetailTemp.irowno;

                dr["cexpirationdate"] = objDetailTemp.cExpirationDate;
                dr["dexpirationdate"] = objDetailTemp.dExpirationDate;
                dr["iexpiratdatecalcu"] = objDetailTemp.iExpiratDateCalcu;
                //新加
                dr["cvouchtype"] = objDetailTemp.cvouchtype;
                dr["cbatchproperty6"] = objDetailTemp.cbatchproperty6;
                dr["binvbatch"] = objDetailTemp.bInvBatch;
                dr["bGsp"] = objDetailTemp.IsGsp;
                dr["cordercode"] = objDetailTemp.OrderCode;
                dr["cwhcode"] = objDetailTemp.cWhCode;
                dr["modetailsID"] = objDetailTemp.cMoDetailsID;
                dr["iomomid"] = objDetailTemp.OmomID;
                dr["fvalidinquan"] = objDetailTemp.fValidInQuan;
                dr["orderquantity"] = objDetailTemp.OrderQuantity;
                dr["invcode"] = objDetailTemp.InvCode;
                dr["comcode"] = objDetailTemp.Omcode;
                dtResult.Tables[1].Rows.Add(dr);

                //foreach (decimal v in objDetailTemp.listiquantity)
                //{
                //    driquantity = dtiquantity.NewRow();
                //    driquantity["iquantity"] = v;
                //    dtiquantity.Rows.Add(driquantity);
                //}
            }

            dtResult.Tables.Add(dtiquantity);
            #endregion

            #region 货位部分
            foreach (InvPositionInfo ipInfo in stock.OperaPositions)
            {
                DataRow drPosition = dtResult.Tables["Position"].NewRow();
                drPosition["iquantity"] = ipInfo.Quantity;
                drPosition["cbatch"] = ipInfo.Batch;
                drPosition["cinvcode"] = ipInfo.InvCode;
                drPosition["cposcode"] = ipInfo.PosCode;
                drPosition["dmadedate"] = ipInfo.MadeDate;
                drPosition["dvdate"] = ipInfo.VDate;
                drPosition["cexpirationdate"] = ipInfo.ExpirationDate;
                drPosition["dexpirationdate"] = ipInfo.ExpirationDate;
                dtResult.Tables["Position"].Rows.Add(drPosition);
            }
            #endregion

            if (sourceVoucher == "02")
            {
                //co.Service.SaveQMCheckOrder(dtResult, sourceVoucher, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
                co.Service.SaveByArrival(dtResult, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            }
            else if (sourceVoucher == "03")
            {
                co.Service.SaveByOutsourcing(dtResult, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            }
            else if (sourceVoucher == "04")
            {
                co.Service.SaveByOMMOrder(dtResult, Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            }
            else if (sourceVoucher[0] == 't')
            {
                co.Service.AuditByDismantle(dtResult, sourceVoucher.Remove(0, 1), Common.CurrentUser.ConnectionString, Common.CurrentUser.Accid, Common.CurrentUser.Year, out errMsg);
            }
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 设计表结构
        /// </summary>
        /// <returns></returns>
        private static DataSet BuildStockInStruct()
        {
            DataSet ds = new DataSet();

            DataTable dtHead = new DataTable("Head");
            DataTable dtBody = new DataTable("Body");
            DataTable dtPosition = new DataTable("Position");

            ds.Tables.Add(dtHead);
            ds.Tables.Add(dtBody);
            ds.Tables.Add(dtPosition);

            #region 表头设计
            ds.Tables["Head"].Columns.Add("csource");
            ds.Tables["Head"].Columns.Add("bisstqc");
            ds.Tables["Head"].Columns.Add("bpufirst");
            ds.Tables["Head"].Columns.Add("brdflag");
            ds.Tables["Head"].Columns.Add("caccounter");
            ds.Tables["Head"].Columns.Add("carvcode");
            ds.Tables["Head"].Columns.Add("cbillcode");
            ds.Tables["Head"].Columns.Add("cbuscode");
            ds.Tables["Head"].Columns.Add("cbustype");
            ds.Tables["Head"].Columns.Add("cchkcode");
            ds.Tables["Head"].Columns.Add("cchkperson");
            ds.Tables["Head"].Columns.Add("ccode");
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
            ds.Tables["Head"].Columns.Add("cdepcode");
            ds.Tables["Head"].Columns.Add("cdepname");
            ds.Tables["Head"].Columns.Add("cexch_name");
            ds.Tables["Head"].Columns.Add("chandler");
            ds.Tables["Head"].Columns.Add("cmaker");
            ds.Tables["Head"].Columns.Add("cmemo");
            ds.Tables["Head"].Columns.Add("cordercode");
            ds.Tables["Head"].Columns.Add("cpersoncode");
            ds.Tables["Head"].Columns.Add("cpersonname");
            ds.Tables["Head"].Columns.Add("cptcode");
            ds.Tables["Head"].Columns.Add("cptname");
            ds.Tables["Head"].Columns.Add("crdcode");
            ds.Tables["Head"].Columns.Add("crdname");
            ds.Tables["Head"].Columns.Add("cvenabbname");
            ds.Tables["Head"].Columns.Add("cvencode");
            ds.Tables["Head"].Columns.Add("cvenname");
            ds.Tables["Head"].Columns.Add("cvouchtype");
            ds.Tables["Head"].Columns.Add("cwhcode");
            ds.Tables["Head"].Columns.Add("cwhname");
            ds.Tables["Head"].Columns.Add("darvdate");
            ds.Tables["Head"].Columns.Add("dchkdate");
            ds.Tables["Head"].Columns.Add("ddate");
            ds.Tables["Head"].Columns.Add("dveridate");
            ds.Tables["Head"].Columns.Add("gspcheck");
            ds.Tables["Head"].Columns.Add("iarriveid");
            ds.Tables["Head"].Columns.Add("iavanum");
            ds.Tables["Head"].Columns.Add("iavaquantity");
            ds.Tables["Head"].Columns.Add("id");
            ds.Tables["Head"].Columns.Add("idiscounttaxtype");
            ds.Tables["Head"].Columns.Add("iexchrate");
            ds.Tables["Head"].Columns.Add("ilowsum");
            ds.Tables["Head"].Columns.Add("ipresent");
            ds.Tables["Head"].Columns.Add("ipresentnum");
            ds.Tables["Head"].Columns.Add("iproorderid");
            ds.Tables["Head"].Columns.Add("ipurarriveid");
            ds.Tables["Head"].Columns.Add("ipurorderid");
            ds.Tables["Head"].Columns.Add("ireturncount");
            ds.Tables["Head"].Columns.Add("isafesum");
            ds.Tables["Head"].Columns.Add("isalebillid");
            ds.Tables["Head"].Columns.Add("iswfcontrolled");
            ds.Tables["Head"].Columns.Add("itaxrate");
            ds.Tables["Head"].Columns.Add("itopsum");
            ds.Tables["Head"].Columns.Add("iverifystate");
            ds.Tables["Head"].Columns.Add("ufts");
            ds.Tables["Head"].Columns.Add("vt_id");
            ds.Tables["Head"].Columns.Add("cvouchname");
            ds.Tables["Head"].Columns.Add("dnverifytime");
            ds.Tables["Head"].Columns.Add("dnmaketime");
            ds.Tables["Head"].Columns.Add("ivouchrowno");
            ds.Tables["Head"].Columns.Add("cinvname");
            ds.Tables["Head"].Columns.Add("bredvouch");
            ds.Tables["Head"].Columns.Add("invm_unit");
            //委外
            ds.Tables["Head"].Columns.Add("cPsPcode");
            ds.Tables["Head"].Columns.Add("iMQuantity");
            ds.Tables["Head"].Columns.Add("cMPoCode");
            #endregion

            #region 表体设计
            ds.Tables["Body"].Columns.Add("autoid");
            ds.Tables["Body"].Columns.Add("bcosting");
            ds.Tables["Body"].Columns.Add("binvbatch");
            ds.Tables["Body"].Columns.Add("bGsp");
            ds.Tables["Body"].Columns.Add("binvtype");
            ds.Tables["Body"].Columns.Add("bservice");
            ds.Tables["Body"].Columns.Add("btaxcost");
            ds.Tables["Body"].Columns.Add("bvmiused");
            ds.Tables["Body"].Columns.Add("cassunit");
            ds.Tables["Body"].Columns.Add("cbaccounter");
            ds.Tables["Body"].Columns.Add("cbarcode");
            ds.Tables["Body"].Columns.Add("cbatch");
            ds.Tables["Body"].Columns.Add("cbvencode");
            ds.Tables["Body"].Columns.Add("ccheckcode");
            ds.Tables["Body"].Columns.Add("ccheckpersoncode");
            ds.Tables["Body"].Columns.Add("ccheckpersonname");
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
            ds.Tables["Body"].Columns.Add("cgspstate");
            ds.Tables["Body"].Columns.Add("cinva_unit");
            ds.Tables["Body"].Columns.Add("cinvaddcode");
            ds.Tables["Body"].Columns.Add("cinvccode");
            ds.Tables["Body"].Columns.Add("cinvcode");
            ds.Tables["Body"].Columns.Add("cinvdefine1");
            ds.Tables["Body"].Columns.Add("cinvdefine2");
            ds.Tables["Body"].Columns.Add("cinvdefine3");
            ds.Tables["Body"].Columns.Add("cinvdefine4");
            ds.Tables["Body"].Columns.Add("cinvdefine5");
            ds.Tables["Body"].Columns.Add("cinvdefine6");
            ds.Tables["Body"].Columns.Add("cinvdefine7");
            ds.Tables["Body"].Columns.Add("cinvdefine8");
            ds.Tables["Body"].Columns.Add("cinvdefine9");
            ds.Tables["Body"].Columns.Add("cinvdefine10");
            ds.Tables["Body"].Columns.Add("cinvdefine11");
            ds.Tables["Body"].Columns.Add("cinvdefine12");
            ds.Tables["Body"].Columns.Add("cinvdefine13");
            ds.Tables["Body"].Columns.Add("cinvdefine14");
            ds.Tables["Body"].Columns.Add("cinvdefine15");
            ds.Tables["Body"].Columns.Add("cinvdefine16");
            ds.Tables["Body"].Columns.Add("cinvm_unit");
            ds.Tables["Body"].Columns.Add("cinvname");
            ds.Tables["Body"].Columns.Add("cinvouchcode");
            ds.Tables["Body"].Columns.Add("cinvstd");
            ds.Tables["Body"].Columns.Add("citem_class");
            ds.Tables["Body"].Columns.Add("citemcname");
            ds.Tables["Body"].Columns.Add("citemcode");
            ds.Tables["Body"].Columns.Add("cmassunit");
            ds.Tables["Body"].Columns.Add("cname");
            ds.Tables["Body"].Columns.Add("corufts");
            ds.Tables["Body"].Columns.Add("cpoid");
            ds.Tables["Body"].Columns.Add("cposition");
            ds.Tables["Body"].Columns.Add("cposname");
            ds.Tables["Body"].Columns.Add("crejectcode");
            ds.Tables["Body"].Columns.Add("creplaceitem");
            ds.Tables["Body"].Columns.Add("csocode");
            ds.Tables["Body"].Columns.Add("cveninvcode");
            ds.Tables["Body"].Columns.Add("cveninvname");
            ds.Tables["Body"].Columns.Add("cvenname");
            ds.Tables["Body"].Columns.Add("cvenabbname");
            ds.Tables["Body"].Columns.Add("cvencode");
            ds.Tables["Body"].Columns.Add("cvmivencode");
            ds.Tables["Body"].Columns.Add("cvmivenname");
            ds.Tables["Body"].Columns.Add("cvouchcode");
            ds.Tables["Body"].Columns.Add("dcheckdate");
            ds.Tables["Body"].Columns.Add("dmadedate");
            ds.Tables["Body"].Columns.Add("dmsdate");
            ds.Tables["Body"].Columns.Add("dsdate");
            ds.Tables["Body"].Columns.Add("dvdate");
            ds.Tables["Body"].Columns.Add("facost");
            ds.Tables["Body"].Columns.Add("iaprice");
            ds.Tables["Body"].Columns.Add("iarrsid");
            ds.Tables["Body"].Columns.Add("icheckidbaks");
            ds.Tables["Body"].Columns.Add("icheckids");
            ds.Tables["Body"].Columns.Add("id");
            ds.Tables["Body"].Columns.Add("iflag");
            ds.Tables["Body"].Columns.Add("ifnum");
            ds.Tables["Body"].Columns.Add("ifquantity");
            ds.Tables["Body"].Columns.Add("iimbsid");
            ds.Tables["Body"].Columns.Add("iimosid");
            ds.Tables["Body"].Columns.Add("iinvexchrate");
            ds.Tables["Body"].Columns.Add("iinvsncount");
            ds.Tables["Body"].Columns.Add("imassdate");
            ds.Tables["Body"].Columns.Add("imaterialfee");
            ds.Tables["Body"].Columns.Add("imoney");
            ds.Tables["Body"].Columns.Add("impcost");
            ds.Tables["Body"].Columns.Add("impoids");
            ds.Tables["Body"].Columns.Add("innum");
            ds.Tables["Body"].Columns.Add("inquantity");
            ds.Tables["Body"].Columns.Add("inum");
            ds.Tables["Body"].Columns.Add("iomodid");
            ds.Tables["Body"].Columns.Add("ioricost");
            ds.Tables["Body"].Columns.Add("iorimoney");
            ds.Tables["Body"].Columns.Add("iorisum");
            ds.Tables["Body"].Columns.Add("ioritaxcost");
            ds.Tables["Body"].Columns.Add("ioritaxprice");
            ds.Tables["Body"].Columns.Add("iposid");
            ds.Tables["Body"].Columns.Add("ipprice");
            ds.Tables["Body"].Columns.Add("iprice");
            ds.Tables["Body"].Columns.Add("iprocesscost");
            ds.Tables["Body"].Columns.Add("iprocessfee");
            ds.Tables["Body"].Columns.Add("ipunitcost");
            ds.Tables["Body"].Columns.Add("iquantity");
            ds.Tables["Body"].Columns.Add("irejectids");
            ds.Tables["Body"].Columns.Add("ismaterialfee");
            ds.Tables["Body"].Columns.Add("isnum");
            ds.Tables["Body"].Columns.Add("isodid");
            ds.Tables["Body"].Columns.Add("isoseq");
            ds.Tables["Body"].Columns.Add("isotype");
            ds.Tables["Body"].Columns.Add("isoutnum");
            ds.Tables["Body"].Columns.Add("isoutquantity");
            ds.Tables["Body"].Columns.Add("isprocessfee");
            ds.Tables["Body"].Columns.Add("isquantity");
            ds.Tables["Body"].Columns.Add("isum");
            ds.Tables["Body"].Columns.Add("isumbillquantity");
            ds.Tables["Body"].Columns.Add("itax");
            ds.Tables["Body"].Columns.Add("itaxprice");
            ds.Tables["Body"].Columns.Add("itaxrate");
            ds.Tables["Body"].Columns.Add("itrids");
            ds.Tables["Body"].Columns.Add("iunitcost");
            ds.Tables["Body"].Columns.Add("inetlock");
            ds.Tables["Body"].Columns.Add("ivmisettlenum");
            ds.Tables["Body"].Columns.Add("ivmisettlequantity");
            ds.Tables["Body"].Columns.Add("fvalidinquan");
            ds.Tables["Body"].Columns.Add("scrapufts");
            ds.Tables["Body"].Columns.Add("strcode");
            ds.Tables["Body"].Columns.Add("strcontractid");
            ds.Tables["Body"].Columns.Add("editprop");
            ds.Tables["Body"].Columns.Add("cvouchtype");
            ds.Tables["Body"].Columns.Add("dbarvdate");
            ds.Tables["Body"].Columns.Add("cbarvcode");
            ds.Tables["Body"].Columns.Add("irowno");

            ds.Tables["Body"].Columns.Add("cexpirationdate");
            ds.Tables["Body"].Columns.Add("dexpirationdate");
            ds.Tables["Body"].Columns.Add("iexpiratdatecalcu");
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
            ds.Tables["Body"].Columns.Add("caddress");
            ds.Tables["Body"].Columns.Add("cinvcname");
            ds.Tables["Body"].Columns.Add("cwhcode");
            ds.Tables["Body"].Columns.Add("cwhname");
            ds.Tables["Body"].Columns.Add("cordercode");
            ds.Tables["Body"].Columns.Add("ivouchrowno");
            ds.Tables["Body"].Columns.Add("modetailsid");

            ds.Tables["Body"].Columns.Add("iomomid");
            ds.Tables["Body"].Columns.Add("cmolotcode");
            ds.Tables["Body"].Columns.Add("iorderdid");
            ds.Tables["Body"].Columns.Add("cmworkcentercode");
            ds.Tables["Body"].Columns.Add("irsrowno");
            ds.Tables["Body"].Columns.Add("cwhpersoncode");
            ds.Tables["Body"].Columns.Add("cwhpersonname");
            ds.Tables["Body"].Columns.Add("imaids");
            ds.Tables["Body"].Columns.Add("iordercode");
            ds.Tables["Body"].Columns.Add("iorderseq");
            ds.Tables["Body"].Columns.Add("comcode");
            ds.Tables["Body"].Columns.Add("cmocode");
            ds.Tables["Body"].Columns.Add("invcode");
            ds.Tables["Body"].Columns.Add("iopseq");
            ds.Tables["Body"].Columns.Add("copdesc");
            ds.Tables["Body"].Columns.Add("cciqbookcode");
            ds.Tables["Body"].Columns.Add("ibondedsumqty");
            ds.Tables["Body"].Columns.Add("productinids");
            ds.Tables["Body"].Columns.Add("cbmemo");
            ds.Tables["Body"].Columns.Add("applydid");
            ds.Tables["Body"].Columns.Add("applycode");
            ds.Tables["Body"].Columns.Add("strowguid");
            ds.Tables["Body"].Columns.Add("cservicecode");
            ds.Tables["Body"].Columns.Add("orderquantity");
            ds.Tables["Body"].Columns.Add("iproorderid");
            ds.Tables["Body"].Columns.Add("iorderrowno");

            #endregion

            #region 货位部分
            ds.Tables["Position"].Columns.Add("iquantity");
            ds.Tables["Position"].Columns.Add("cbatch");
            ds.Tables["Position"].Columns.Add("cinvcode");
            ds.Tables["Position"].Columns.Add("cposcode");
            ds.Tables["Position"].Columns.Add("dmadedate");
            ds.Tables["Position"].Columns.Add("dvdate");
            ds.Tables["Position"].Columns.Add("cexpirationdate");
            ds.Tables["Position"].Columns.Add("dexpirationdate");

            #endregion

            return ds;
        }

        /// <summary>
        /// DataRow转化为StockInDetail
        /// </summary>
        /// <param name="siDetail"></param>
        /// <param name="dr"></param>
        public static void StockInDetailRow(StockInDetail siDetail ,DataRow dr)
        {
            #region Body_Row

            siDetail.AutoID = Common.DB2Int(dr["autoid"]);
            siDetail.Costing = Common.DB2String(dr["bcosting"]);
            siDetail.Invbatch = Common.DB2Bool(dr["binvbatch"]);
            siDetail.IsGsp = Common.DB2Bool(dr["bGsp"]);
            siDetail.Invtype = Common.DB2Bool(dr["binvtype"]);
            siDetail.Service = Common.DB2Bool(dr["bservice"]);
            siDetail.Taxcost = Common.DB2Int(dr["bTaxcost"]);
            siDetail.Vmiused = Common.DB2Bool(dr["bvmiused"]);
            siDetail.Assunit = Common.DB2String(dr["cassunit"]);
            siDetail.Baccounter = Common.DB2String(dr["cbaccounter"]);
            siDetail.Barcode = Common.DB2String(dr["cbarcode"]);
            siDetail.Batch = Common.DB2String(dr["cbatch"]);
            siDetail.Bvencode = Common.DB2String(dr["cbvencode"]);
            siDetail.Checkcode = Common.DB2String(dr["ccheckcode"]);
            siDetail.Checkpersoncode = Common.DB2String(dr["ccheckpersoncode"]);
            siDetail.Checkpersonname = Common.DB2String(dr["ccheckpersonname"]);
            siDetail.Define22 = Common.DB2String(dr["cdefine22"]);
            siDetail.Define23 = Common.DB2String(dr["cdefine23"]);
            siDetail.Define24 = Common.DB2String(dr["cdefine24"]);
            siDetail.Define25 = Common.DB2String(dr["cdefine25"]);
            siDetail.Define26 = Common.DB2String(dr["cdefine26"]);
            siDetail.Define27 = Common.DB2String(dr["cdefine27"]);
            siDetail.Define28 = Common.DB2String(dr["cdefine28"]);
            siDetail.Define29 = Common.DB2String(dr["cdefine29"]);
            siDetail.Define30 = Common.DB2String(dr["cdefine30"]);
            siDetail.Define31 = Common.DB2String(dr["cdefine31"]);
            siDetail.Define32 = Common.DB2String(dr["cdefine32"]);
            siDetail.Define33 = Common.DB2String(dr["cdefine33"]);
            siDetail.Define34 = Common.DB2String(dr["cdefine34"]);
            siDetail.Define35 = Common.DB2String(dr["cdefine35"]);
            siDetail.Define36 = Common.DB2String(dr["cdefine36"]);
            siDetail.Define37 = Common.DB2String(dr["cdefine37"]);
            siDetail.Free1 = Common.DB2String(dr["cfree1"]);
            siDetail.Free2 = Common.DB2String(dr["cfree2"]);
            siDetail.Free3 = Common.DB2String(dr["cfree3"]);
            siDetail.Free4 = Common.DB2String(dr["cfree4"]);
            siDetail.Free5 = Common.DB2String(dr["cfree5"]);
            siDetail.Free6 = Common.DB2String(dr["cfree6"]);
            siDetail.Free7 = Common.DB2String(dr["cfree7"]);
            siDetail.Free8 = Common.DB2String(dr["cfree8"]);
            siDetail.Free9 = Common.DB2String(dr["cfree9"]);
            siDetail.Free10 = Common.DB2String(dr["cfree10"]);
            siDetail.Gspstate = Common.DB2String(dr["cgspstate"]);
            siDetail.Inva_unit = Common.DB2String(dr["cinva_unit"]);
            siDetail.Invaddcode = Common.DB2String(dr["cinvaddcode"]);
            siDetail.cInvCode = Common.DB2String(dr["cinvcode"]);
            siDetail.InvCCode = Common.DB2String(dr["cinvccode"]);
            siDetail.Invdefine1 = Common.DB2String(dr["cinvdefine1"]);
            siDetail.Invdefine10 = Common.DB2String(dr["cinvdefine10"]);
            siDetail.Invdefine11 = Common.DB2String(dr["cinvdefine11"]);
            siDetail.Invdefine12 = Common.DB2String(dr["cinvdefine12"]);
            siDetail.Invdefine13 = Common.DB2String(dr["cinvdefine13"]);
            siDetail.Invdefine14 = Common.DB2String(dr["cinvdefine14"]);
            siDetail.Invdefine15 = Common.DB2String(dr["cinvdefine15"]);
            siDetail.Invdefine16 = Common.DB2String(dr["cinvdefine16"]);
            siDetail.Invdefine2 = Common.DB2String(dr["cinvdefine2"]);
            siDetail.Invdefine3 = Common.DB2String(dr["cinvdefine3"]);
            siDetail.Invdefine4 = Common.DB2String(dr["cinvdefine4"]);
            siDetail.Invdefine5 = Common.DB2String(dr["cinvdefine5"]);
            siDetail.Invdefine6 = Common.DB2String(dr["cinvdefine6"]);
            siDetail.Invdefine7 = Common.DB2String(dr["cinvdefine7"]);
            siDetail.Invdefine8 = Common.DB2String(dr["cinvdefine8"]);
            siDetail.Invdefine9 = Common.DB2String(dr["cinvdefine9"]);
            siDetail.Invm_unit = Common.DB2String(dr["cinvm_unit"]);
            siDetail.Invname = Common.DB2String(dr["cinvname"]);
            siDetail.Invouchcode = Common.DB2String(dr["cinvouchcode"]);
            siDetail.cInvStd = Common.DB2String(dr["cinvstd"]);
            siDetail.Item_class = Common.DB2String(dr["citem_class"]);
            siDetail.Itemcname = Common.DB2String(dr["citemcname"]);
            siDetail.Itemcode = Common.DB2String(dr["cItemcode"]);
            siDetail.Massunit = Common.DB2String(dr["cMassunit"]);
            siDetail.Name = Common.DB2String(dr["cname"]);
            siDetail.Orufts = Common.DB2String(dr["corufts"]);
            siDetail.Poid = Common.DB2String(dr["cpoid"]);
            siDetail.Position = Common.DB2String(dr["cposition"]);
            siDetail.Posname = Common.DB2String(dr["cposname"]);
            siDetail.Rejectcode = Common.DB2String(dr["crejectcode"]);
            siDetail.Replaceitem = Common.DB2String(dr["creplaceitem"]);
            siDetail.Scrapufts = Common.DB2String(dr["cscrapufts"]);
            siDetail.Socode = Common.DB2String(dr["csocode"]);
            siDetail.Veninvcode = Common.DB2String(dr["cveninvcode"]);
            siDetail.Veninvname = Common.DB2String(dr["cveninvname"]);
            siDetail.Venname = Common.DB2String(dr["cvenname"]);
            siDetail.VenCode = Common.DB2String(dr["cvencode"]);
            siDetail.VenAbbName = Common.DB2String(dr["cvenabbname"]);
            siDetail.Vmivencode = Common.DB2String(dr["cvmivencode"]);
            siDetail.Vmivenname = Common.DB2String(dr["cvmivenname"]);
            siDetail.Vouchcode = Common.DB2String(dr["cvouchcode"]);
            siDetail.CheckDate = Common.DB2String(dr["dcheckdate"]);
            siDetail.Madedate = Common.DB2String(dr["dmadedate"]);
            siDetail.Msdate = Common.DB2String(dr["dmsdate"]);
            siDetail.Sdate = Common.DB2String(dr["dsdate"]);
            siDetail.Vdate = Common.DB2String(dr["dvdate"]);
            siDetail.Acost = Common.DB2Decimal(dr["facost"]);
            siDetail.Aprice = Common.DB2Int(dr["iaprice"]);//特殊调整
            siDetail.Arrsid = Common.DB2Int(dr["iarrsid"]);
            siDetail.Checkidbaks = Common.DB2Int(dr["icheckidbaks"]);
            siDetail.Checkids = Common.DB2Int(dr["icheckids"]);
            siDetail.Flag = Common.DB2Int(dr["iflag"]);
            siDetail.Fnum = Common.DB2Int(dr["ifnum"]);
            siDetail.Fquantity = Common.DB2Int(dr["ifquantity"]);
            siDetail.ID = Common.DB2Int(dr["id"]);
            siDetail.Imbsid = Common.DB2Int(dr["iimbsid"]);
            siDetail.Imosid = Common.DB2Int(dr["iimosid"]);
            siDetail.Invexchrate = Common.DB2Decimal(dr["iinvexchrate"]);
            siDetail.Invsncount = Common.DB2Decimal(dr["iinvsncount"]);
            siDetail.Massdate = Common.DB2Decimal(dr["imassdate"]);
            siDetail.Materialfee = Common.DB2Decimal(dr["imaterialfee"]);
            siDetail.Money = Common.DB2Decimal(dr["imoney"]);
            siDetail.Mpcost = Common.DB2Decimal(dr["impcost"]);
            siDetail.Mpoids = Common.DB2Decimal(dr["impoids"]);
            siDetail.Nnum = Common.DB2Decimal(dr["innum"]);
            siDetail.Nquantity = Common.DB2Decimal(dr["inquantity"]);
            siDetail.fShallInQuan = Common.DB2Decimal(dr["inquantity"]);
            siDetail.Num = Common.DB2Decimal(dr["inum"]);
            siDetail.Omodid = Common.DB2Int(dr["iomodid"]);
            siDetail.Oricost = Common.DB2Decimal(dr["ioricost"]);
            siDetail.Orimoney = Common.DB2Decimal(dr["iorimoney"]);
            siDetail.Orisum = Common.DB2Decimal(dr["iorisum"]);
            siDetail.Oritaxcost = Common.DB2Decimal(dr["ioritaxcost"]);
            siDetail.Oritaxprice = Common.DB2Decimal(dr["ioritaxprice"]);
            siDetail.Posid = Common.DB2Int(dr["iposid"]);
            siDetail.Pprice = Common.DB2Decimal(dr["ipprice"]);
            siDetail.Price = Common.DB2Decimal(dr["iprice"]);
            siDetail.Processcost = Common.DB2Decimal(dr["iprocesscost"]);
            siDetail.Processfee = Common.DB2Decimal(dr["iprocessfee"]);
            siDetail.Punitcost = Common.DB2Decimal(dr["ipunitcost"]);
            siDetail.Quantity = Common.DB2Decimal(dr["iquantity"]);
            siDetail.Rejectids = Common.DB2Decimal(dr["irejectids"]);
            siDetail.Smaterialfee = Common.DB2Decimal(dr["ismaterialfee"]);
            siDetail.Snum = Common.DB2Decimal(dr["isnum"]);
            siDetail.Sodid = Common.DB2Decimal(dr["isodid"]);
            siDetail.Soseq = Common.DB2Decimal(dr["isoseq"]);
            siDetail.Sotype = Common.DB2Decimal(dr["isotype"]);
            siDetail.Soutnum = Common.DB2Decimal(dr["isoutnum"]);
            siDetail.Soutquantity = Common.DB2Decimal(dr["isoutquantity"]);
            siDetail.Sprocessfee = Common.DB2Decimal(dr["isprocessfee"]);
            siDetail.Squantity = Common.DB2Decimal(dr["isquantity"]);
            siDetail.Sum = Common.DB2Decimal(dr["isum"]);
            siDetail.Sumbillquantity = Common.DB2Decimal(dr["isumbillquantity"]);
            siDetail.Tax = Common.DB2Decimal(dr["itax"]);
            siDetail.Taxprice = Common.DB2Decimal(dr["itaxprice"]);
            siDetail.Taxrate = Common.DB2Decimal(dr["itaxrate"]);
            siDetail.Trids = Common.DB2Decimal(dr["itrids"]);
            siDetail.Unitcost = Common.DB2Decimal(dr["iunitcost"]);
            siDetail.Vdate = Common.DB2String(dr["dvdate"]);
            siDetail.Vmisettlenum = Common.DB2Decimal(dr["ivmisettlenum"]);
            siDetail.Vmisettlequantity = Common.DB2Decimal(dr["ivmisettlequantity"]);
            siDetail.StrCode = Common.DB2String(dr["strcode"]);
            siDetail.Contractid = Common.DB2String(dr["strcontractid"]);
            siDetail.OrderNumber = Common.DB2Decimal(dr["innum"]);
            siDetail.cvouchtype = Common.DB2String(dr["cvouchtype"]);
            siDetail.barvdate = Common.DB2String(dr["dbarvdate"]);
            siDetail.barvcode = Common.DB2String(dr["cbarvcode"]);
            siDetail.irowno = Common.DB2String(dr["irowno"]);

            siDetail.cExpirationDate = Common.DB2String(dr["cexpirationdate"]);
            siDetail.dExpirationDate = Common.DB2String(dr["dexpirationdate"]);
            siDetail.iExpiratDateCalcu = Common.DB2Int(dr["iexpiratdatecalcu"]);
            siDetail.cbatchproperty6 = Common.DB2String(dr["cbatchproperty6"]);
            siDetail.cinvccname = Common.DB2String(dr["cinvcname"]);
            siDetail.cWhCode = Common.DB2String(dr["cwhcode"]);
            siDetail.cWhName = Common.DB2String(dr["cwhname"]);
            siDetail.Address = Common.DB2String(dr["caddress"]);
            siDetail.VouchRowNo = Common.DB2Int(dr["ivouchrowno"]);
            siDetail.bInvBatch = Common.DB2Bool(dr["binvbatch"]);
            siDetail.cMoDetailsID = Common.DB2String(dr["modetailsid"]);
            siDetail.fValidInQuan = Common.DB2Decimal(dr["fvalidinquan"]);
            siDetail.OrderCode = Common.DB2String(dr["iordercode"]);
            siDetail.OrderQuantity = Common.DB2Decimal(dr["orderquantity"]);
            siDetail.iProOrderId = Common.DB2Int(dr["iproorderid"]);
            //材料	
            siDetail.InvCode = Common.DB2String(dr["invcode"]);
            siDetail.OmomID = Common.DB2String(dr["iomomid"]);
            siDetail.Omcode = Common.DB2String(dr["comcode"]);

            #endregion
        }

        public static StockIn CreateSIOrderMomain(string ccode, out DataSet order)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            order = null;

            co.Service.CreateSIOrderByMomain(ccode, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            StockIn objSIOrder = new StockIn();
            StockInTable(objSIOrder, order);

            return objSIOrder;
        }

        #region 委外材料出库部分
        /// <summary>
        /// 获取委外出库单表头
        /// </summary>
        /// <param name="cCode">委外出库单号</param>
        /// <param name="MoDetails"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static StockIn GetOMMOHead(string cCode, out List<Om_MoHeadInfo> MoDetails,out DataSet order)
        {
            MoDetails = new List<Om_MoHeadInfo>();
            StockIn stock = new StockIn();

            Common co = Common.GetInstance();

            string errMsg = "";            
            order = null;

            co.Service.GetOMMOHead(cCode, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            //表头信息
            #region StockIn
            if (order.Tables["head"].Rows.Count > 0)
            {
                #region Head

                stock.Isstqc = Common.DB2Bool(order.Tables["head"].Rows[0]["bisstqc"]);
                stock.Pufirst = Common.DB2Bool(order.Tables["head"].Rows[0]["bpufirst"]);
                stock.Rdflag = Common.DB2Int(order.Tables["head"].Rows[0]["brdflag"]);
                stock.Accounter = Common.DB2String(order.Tables["head"].Rows[0]["caccounter"]);
                stock.Arvcode = Common.DB2String(order.Tables["head"].Rows[0]["carvcode"]);
                stock.Billcode = Common.DB2String(order.Tables["head"].Rows[0]["cbillcode"]);
                stock.Buscode = Common.DB2String(order.Tables["head"].Rows[0]["cbuscode"]);
                stock.Bustype = Common.DB2String(order.Tables["head"].Rows[0]["cbustype"]);
                stock.Chkcode = Common.DB2String(order.Tables["head"].Rows[0]["cchkcode"]);
                stock.Chkperson = Common.DB2String(order.Tables["head"].Rows[0]["cchkperson"]);
                stock.Code = Common.DB2String(order.Tables["head"].Rows[0]["ccode"]);
                stock.Define1 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine1"]);
                stock.Define10 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine10"]);
                stock.Define11 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine11"]);
                stock.Define12 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine12"]);
                stock.Define13 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine13"]);
                stock.Define14 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine14"]);
                stock.Define15 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine15"]);
                stock.Define16 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine16"]);
                stock.Define2 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine2"]);
                stock.Define3 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine3"]);
                stock.Define4 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine4"]);
                stock.Define5 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine5"]);
                stock.Define6 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine6"]);
                stock.Define7 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine7"]);
                stock.Define8 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine8"]);
                stock.Define9 = Common.DB2String(order.Tables["head"].Rows[0]["cdefine9"]);
                stock.Depcode = Common.DB2String(order.Tables["head"].Rows[0]["cdepcode"]);
                stock.Depname = Common.DB2String(order.Tables["head"].Rows[0]["cdepname"]);
                stock.Exch_name = Common.DB2String(order.Tables["head"].Rows[0]["cexch_name"]);
                stock.Gspcheck = Common.DB2String(order.Tables["head"].Rows[0]["gspcheck"]);
                stock.Handler = Common.DB2String(order.Tables["head"].Rows[0]["chandler"]);
                stock.Maker = Common.DB2String(order.Tables["head"].Rows[0]["cmaker"]);
                stock.Memo = Common.DB2String(order.Tables["head"].Rows[0]["cmemo"]);
                stock.Ordercode = Common.DB2String(order.Tables["head"].Rows[0]["cordercode"]);
                stock.Personcode = Common.DB2String(order.Tables["head"].Rows[0]["cpersoncode"]);
                stock.Personname = Common.DB2String(order.Tables["head"].Rows[0]["cpersonname"]);
                stock.Ptcode = Common.DB2String(order.Tables["head"].Rows[0]["cptcode"]);
                stock.Ptname = Common.DB2String(order.Tables["head"].Rows[0]["cptname"]);
                stock.Rdcode = Common.DB2String(order.Tables["head"].Rows[0]["crdcode"]);
                stock.Rdname = Common.DB2String(order.Tables["head"].Rows[0]["crdname"]);
                stock.Source = Common.DB2String(order.Tables["head"].Rows[0]["csource"]);
                stock.Vencode = Common.DB2String(order.Tables["head"].Rows[0]["cvencode"]);
                stock.Vouchtype = Common.DB2String(order.Tables["head"].Rows[0]["cvouchtype"]);
                stock.Whcode = Common.DB2String(order.Tables["head"].Rows[0]["cwhcode"]);
                stock.Whname = Common.DB2String(order.Tables["head"].Rows[0]["cwhname"]);
                stock.Arvdate = Common.DB2String(order.Tables["head"].Rows[0]["darvdate"]).Trim();
                stock.Chkdate = Common.DB2String(order.Tables["head"].Rows[0]["dchkdate"]);
                stock.Date = Common.DB2String(order.Tables["head"].Rows[0]["ddate"]);
                stock.Veridate = Common.DB2String(order.Tables["head"].Rows[0]["dveridate"]);
                stock.Arriveid = Common.DB2Int(order.Tables["head"].Rows[0]["iarriveid"]);
                stock.Avanum = Common.DB2Decimal(order.Tables["head"].Rows[0]["iavanum"]);
                stock.Avaquantity = Common.DB2Decimal(order.Tables["head"].Rows[0]["iavaquantity"]);
                stock.ID = Common.DB2Int(order.Tables["head"].Rows[0]["id"]);
                stock.Discounttaxtype = Common.DB2Int(order.Tables["head"].Rows[0]["idiscounttaxtype"]);
                stock.Exchrate = Common.DB2Decimal(order.Tables["head"].Rows[0]["iexchrate"]);
                stock.Lowsum = Common.DB2Decimal(order.Tables["head"].Rows[0]["iLowsum"]);
                stock.Present = Common.DB2Int(order.Tables["head"].Rows[0]["ipresent"]);
                stock.Presentnum = Common.DB2Int(order.Tables["head"].Rows[0]["ipresentnum"]);
                stock.ProOrderId = Common.DB2Int(order.Tables["head"].Rows[0]["iproorderid"]);
                stock.Purarriveid = Common.DB2Int(order.Tables["head"].Rows[0]["ipurarriveid"]);
                stock.Purorderid = Common.DB2Int(order.Tables["head"].Rows[0]["ipurorderid"]);
                stock.Returncount = Common.DB2Int(order.Tables["head"].Rows[0]["ireturncount"]);
                stock.Safesum = Common.DB2Decimal(order.Tables["head"].Rows[0]["isafesum"]);
                stock.Salebillid = Common.DB2Int(order.Tables["head"].Rows[0]["isalebillid"]);
                stock.Swfcontrolled = Common.DB2Int(order.Tables["head"].Rows[0]["iswfcontrolled"]);
                stock.Taxrate = Common.DB2Decimal(order.Tables["head"].Rows[0]["itaxrate"]);
                stock.Topsum = Common.DB2Decimal(order.Tables["head"].Rows[0]["itopsum"]);
                stock.Verifystate = Common.DB2Int(order.Tables["head"].Rows[0]["iverifystate"]);
                stock.Venname = Common.DB2String(order.Tables["head"].Rows[0]["cvenname"]);
                stock.VouchRowNo = Common.DB2Int(order.Tables["head"].Rows[0]["ivouchrowno"]);
                stock.BredVouch = Common.DB2String(order.Tables["head"].Rows[0]["bredvouch"]);

                stock.Ufts = Common.DB2String(order.Tables["head"].Rows[0]["Ufts"]);
                stock.VT_id = Common.DB2Int(order.Tables["head"].Rows[0]["vt_id"]);
                stock.PspCode = Common.DB2String(order.Tables["head"].Rows[0]["cpspcode"]);
                stock.Venabbname = Common.DB2String(order.Tables["head"].Rows[0]["cvenabbname"]);

                #endregion
            }

            #endregion

            //表体选择列表
            #region MoDetails
            Om_MoHeadInfo Modetail;
            if (order.Tables["body"].Rows.Count > 0)
            {
                foreach (DataRow dr in order.Tables["body"].Rows)
                {
                    #region MoDetail
                    Modetail = new Om_MoHeadInfo();
                    Modetail.MoDetailsID = Common.DB2String(dr["MoDetailsID"]);
                    Modetail.Code = Common.DB2String(dr["cCode"]);
                    Modetail.Date = Common.DB2String(dr["dDate"]);
                    Modetail.DepCode = Common.DB2String(dr["cDepCode"]);
                    Modetail.DepName = Common.DB2String(dr["cDepName"]);
                    Modetail.VenCode = Common.DB2String(dr["cVenCode"]);
                    Modetail.VenName = Common.DB2String(dr["cVenName"]);
                    Modetail.VenAbbName = Common.DB2String(dr["cVenAbbName"]);
                    Modetail.PersonCode = Common.DB2String(dr["cPersonCode"]);
                    Modetail.PersonName = Common.DB2String(dr["cPersonName"]);
                    Modetail.MakerCode = Common.DB2String(dr["cMakerCode"]);
                    Modetail.MakerName = Common.DB2String(dr["cMakerName"]);
                    Modetail.InvCode = Common.DB2String(dr["cInvCode"]);
                    Modetail.InvName = Common.DB2String(dr["cInvName"]);
                    Modetail.InvSta = Common.DB2String(dr["cInvSta"]);
                    Modetail.InvUnit = Common.DB2String(dr["cInvUnit"]);
                    Modetail.Quantity = Common.DB2Decimal(dr["iQuantity"]);
                    Modetail.NQuantity = Common.DB2Decimal(dr["iNQuantity"]);
                    Modetail.RowNo = Common.DB2Decimal(dr["iRowNo"]);
                    Modetail.SetDisplayMember();
                    MoDetails.Add(Modetail);
                    #endregion
                }
            }

            #endregion

            return stock;
        }

        /// <summary>
        /// 根据主表材料出库单主表标识获取表体信息
        /// </summary>
        /// <param name="MoDetailID">材料出库单主表标识</param>
        /// <returns></returns>
        public static List<StockInDetail> GetOMMOBody(string MoDetailID)
        {
            List<StockInDetail> sidList = new List<StockInDetail>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet order = null;

            co.Service.GetOMMOBody(MoDetailID, Common.CurrentUser.ConnectionString, out order, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }

            #region StockInDetail
            if (order.Tables["body"].Rows.Count > 0)
            {
                StockInDetail sid;
                foreach (DataRow dr in order.Tables["body"].Rows)
                {
                    sid = new StockInDetail();
                    StockInDetailRow(sid, dr);
                    sidList.Add(sid);
                }
            }
            #endregion

            return sidList;
        }

        #endregion

        #endregion

        #region 辅助信息

        /// <summary>
        /// 获取批号列表
        /// </summary>
        /// <param name="invCode">存货编码</param>
        /// <param name="bhCode">存货批号</param>
        /// <param name="whCode">仓库编码</param>
        /// <returns>批号列表</returns>
        public static List<BatchInfo> GetBatchList(string invCode, string bhCode,string whCode)
        {
            if (string.IsNullOrEmpty(invCode))
            {
                throw new Exception("请输入产品编号！");
            }
            List<BatchInfo> batchList = new List<BatchInfo>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsBatch = new DataSet();

            co.Service.GetBatchList(invCode,bhCode, whCode, Common.CurrentUser.ConnectionString, out dsBatch, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsBatch == null || dsBatch.Tables[0].Rows.Count < 1)
                return null;

            BatchInfo batch;
            foreach (DataRow dr in dsBatch.Tables[0].Rows)
            {
                #region BatchInfo

                batch = new BatchInfo();
                batch.WhCode = dr["cwhcode"].ToString();
                batch.InvCode = dr["cinvcode"].ToString();
                batch.Batch = dr["cBatch"].ToString();
                batch.VMIVenCode = dr["cVMIVenCode"].ToString();
                batch.VenName = dr["cvenname"].ToString();
                batch.VenAbbName = dr["cvenabbname"].ToString();
                batch.Quantity = Common.DB2Decimal(dr["iQuantity"].ToString());
                batch.VDate = dr["dVDate"].ToString();
                batch.Mdate = dr["dMdate"].ToString();
                batch.MassDate = Common.DB2Decimal(dr["iMassDate"].ToString());
                batch.Free1 = dr["cFree1"].ToString();
                batch.Free2 = dr["cFree2"].ToString();
                batch.Free3 = dr["cFree3"].ToString();
                batch.Free4 = dr["cFree4"].ToString();
                batch.Free5 = dr["cFree5"].ToString();
                batch.Free6 = dr["cFree6"].ToString();
                batch.Free7 = dr["cFree7"].ToString();
                batch.Free8 = dr["cFree8"].ToString();
                batch.Free9 = dr["cFree9"].ToString();
                batch.Free10 = dr["cFree10"].ToString();
                batchList.Add(batch);
                #endregion
            }
            return batchList;
        }

        /// <summary>
        /// 获取批号结存数量
        /// </summary>
        /// <param name="invCode">存货编码</param>
        /// <param name="bhCode">批号</param>
        /// <param name="whCode">仓库编码</param>
        /// <returns>批次列表</returns>
        public static List<BatchInfo> GetBatchInfo(string invCode, string bhCode, string whCode)
        {
            List<BatchInfo> batchList = new List<BatchInfo>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsBatch = new DataSet();

            co.Service.GetBatchInfo(invCode, bhCode, whCode, Common.CurrentUser.ConnectionString, out dsBatch, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsBatch == null || dsBatch.Tables[0].Rows.Count < 1)
                return null;

            BatchInfo batch;
            foreach (DataRow dr in dsBatch.Tables[0].Rows)
            {
                #region BatchInfo

                batch = new BatchInfo();
                batch.WhCode = whCode;
                batch.InvCode = invCode;
                batch.Batch = dr["cBatch"].ToString();
                batch.Quantity = Common.DB2Decimal(dr["iQuantity"].ToString());
                batch.MassDate = Common.DB2Decimal(dr["iMassDate"].ToString());
                batch.Mdate = dr["dMdate"].ToString();
                batch.Expirationdate = dr["cExpirationdate"].ToString();
                batch.VDate = dr["dVDate"].ToString();
                batchList.Add(batch);
                #endregion
            }
            return batchList;
        }

        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <returns>仓库信息</returns>
        public static WareHouseInfo GetWareHouseInfo(string cWhCode)
        {
            WareHouseInfo whInfo = new WareHouseInfo();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsWareHouse = new DataSet();

            co.Service.GetWHInfo(cWhCode, Common.CurrentUser.ConnectionString, out dsWareHouse, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsWareHouse == null || dsWareHouse.Tables[0].Rows.Count < 1)
                throw new Exception("未获取到此仓库信息!");

            whInfo.WhCode = dsWareHouse.Tables[0].Rows[0]["cWhCode"].ToString();
            whInfo.WhName = dsWareHouse.Tables[0].Rows[0]["cWhName"].ToString();
            whInfo.DepCode = dsWareHouse.Tables[0].Rows[0]["cDepCode"].ToString();
            whInfo.WhPos = Common.DB2Bool(dsWareHouse.Tables[0].Rows[0]["bWhPos"].ToString());
            whInfo.Freeze = Common.DB2Bool(dsWareHouse.Tables[0].Rows[0]["bFreeze"].ToString());
            whInfo.Shop = Common.DB2Bool(dsWareHouse.Tables[0].Rows[0]["bShop"].ToString());

            return whInfo;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns>部门列表</returns>
        public static List<DepartMentInfo> GetDepartMentList()
        {
            List<DepartMentInfo> departList = new List<DepartMentInfo>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsDepartMent = new DataSet();

            co.Service.GetDeptList(Common.CurrentUser.UserName, Common.CurrentUser.ConnectionString, out dsDepartMent, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsDepartMent == null || dsDepartMent.Tables[0].Rows.Count < 1)
                return null;

            DepartMentInfo depInfo;
            foreach (DataRow dr in dsDepartMent.Tables[0].Rows)
            {
                #region DepartMentInfo

                depInfo = new DepartMentInfo();
                depInfo.RefSelectColumn = dr["bRefSelectColumn"] == null ? false : bool.Parse(dr["bRefSelectColumn"].ToString());
                depInfo.DepCode = dr["cDepCode"].ToString();
                depInfo.DepName = dr["cDepName"].ToString();
                depInfo.DepPerson = dr["cDepPerson"].ToString();
                depInfo.DepGrade = Common.DB2Decimal(dr["iDepGrade"].ToString());
                depInfo.DepEnd = dr["bDepEnd"] == null ? false : bool.Parse(dr["bDepEnd"].ToString());
                depInfo.DepProp = dr["cDepProp"].ToString();
                depInfo.DepPhone = dr["cDepPhone"].ToString();
                depInfo.DepAddress = dr["cDepAddress"].ToString();
                depInfo.DepMemo = dr["cDepMemo"].ToString();
                depInfo.CreLine = Common.DB2Decimal(dr["iCreLine"].ToString());
                depInfo.CreGrade = dr["cCreGrade"].ToString();
                depInfo.CreDate = Common.DB2Decimal(dr["iCreDate"].ToString());
                departList.Add(depInfo);
                #endregion
            }
            return departList;
        }

        /// <summary>
        /// 获取货位列表
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <param name="iCode">存货编码</param>
        /// <param name="cBatch">批次编码</param>
        /// <param name="iTrackID">入库单子表标识</param>
        /// <param name="bRdFlag">收发标志</param>
        /// <returns></returns>
        public static List<PositionInfo> GetPositionList(string cWhCode, string iCode, string cBatch, string iTrackID, int bRdFlag)
        {
            if (string.IsNullOrEmpty(cWhCode))
            {
                throw new Exception("请选择仓库！");
            }
            List<PositionInfo> ptList = new List<PositionInfo>();
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            DataSet dsPosition = new DataSet();

            co.Service.GetPTList(cWhCode, iCode, cBatch, iTrackID, bRdFlag, Common.CurrentUser.ConnectionString, out dsPosition, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);
            if (dsPosition == null || dsPosition.Tables[0].Rows.Count < 1)
                return null;

            PositionInfo ptInfo;
            foreach (DataRow dr in dsPosition.Tables[0].Rows)
            {
                #region PositionInfo

                ptInfo = new PositionInfo();
                ptInfo.PosCode = dr["cPosCode"].ToString();
                ptInfo.PosName = dr["cPosName"].ToString();
                ptInfo.PosGrade = Common.DB2Decimal(dr["iPosGrade"].ToString());
                ptInfo.PosEnd = Common.DB2Bool(dr["bPosEnd"].ToString());
                ptInfo.WhCode = dr["cWhCode"].ToString();
                ptInfo.MaxCubage = Common.DB2Decimal(dr["iMaxCubage"].ToString());
                ptInfo.MaxWeight = Common.DB2Decimal(dr["iMaxWeight"].ToString());
                ptInfo.Memo = dr["cMemo"].ToString();
                ptInfo.BarCode = dr["cBarCode"].ToString();
                ptInfo.Quantity = Common.DB2Decimal(dr["iQuantity"].ToString());
                ptInfo.Num = Common.DB2Decimal(dr["iNum"].ToString());
                ptList.Add(ptInfo);
                #endregion
            }

            return ptList;
        }

        /// <summary>
        /// 获取货位现存量
        /// </summary>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cBatch">批次编码</param>
        /// <param name="cWhCode">仓库编码</param>
        /// <param name="cPosCode">货位编码</param>
        /// <returns>货位现存量</returns>
        public static decimal GetPTQuan(string cInvCode, string cBatch, string cWhCode, string cPosCode)
        {
            decimal dStock = 0;
            Common co = Common.GetInstance();
            string errMsg = "";//出错信息

            dStock = co.Service.GetPTQuan(cInvCode, cBatch, cWhCode, cPosCode, Common.CurrentUser.ConnectionString, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);

            return dStock;
        }

        /// <summary>
        /// 获取仓库现存量
        /// </summary>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cBatch">批次编码</param>
        /// <param name="cWhCode">仓库编码</param>
        /// <returns>仓库现存量</returns>
        public static decimal GetWHQuan(string cInvCode, string cBatch, string cWhCode)
        {
            decimal dStock = 0;
            Common co = Common.GetInstance();
            string errMsg = "";//出错信息

            dStock = co.Service.GetWHQuan(cInvCode, cBatch, cWhCode, Common.CurrentUser.ConnectionString, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);

            return dStock;
        }

        /// <summary>
        /// 查看货位是否存在
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <param name="cPosCode">货位编码</param>
        /// <returns>是否存在</returns>
        public static bool GetPTExist(string cWhCode, string cPosCode)
        {
            bool isExist = false;
            Common co = Common.GetInstance();
            string errMsg = "";//出错信息

            isExist = co.Service.GetPTExist(cWhCode, cPosCode, Common.CurrentUser.ConnectionString, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
                throw new Exception(errMsg);

            return isExist;
        }

        #endregion
    }
}
