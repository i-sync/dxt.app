using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Model;
using System.Data.SqlClient;

namespace U8DataAccess
{
    public class StockInProcess
    {
        #region 取得上游单据列表

        //到货单
        public static int GetArriveList(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {

            OrderList = null;
            errMsg = "";
            string strSql = @"select pu_ArrHead.id,pu_ArrBody.autoid,cvenname  FROM  pu_ArrBody  inner join pu_ArrHead on pu_ArrHead.id=pu_ArrBody.id inner join (select cinvcode as cinvcode1,iId from inventory) inventory on pu_arrbody.cinvcode=inventory.cinvcode1  Where ( (1>0) AND (1>0) ) AND ( 1>0 ) and (isnull(cbustype,'')<>'委外加工') And (1=1)  and  isnull(cbcloser,N'')=N'' And isnull(cverifier,'')<>'' And iBillType =N'0' and isnull(bGsp,N'')=N'0' and (isnull(iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0)))";
            if (ccode != null && ccode.Length > 0)
                strSql += " and pu_ArrHead.ccode='" + OrderList + "'";
            //int i = OperationSql.GetDataset(strSql, connectionString, out OrderList, out errMsg);
            //return i;
            int flag = -1;
            try
            {
                OrderList = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        //检验单
        public static int GetQMCheckList(string connectionString, out DataSet dataset, out string errMsg)
        {
            dataset = null;
            errMsg = "";
            string strSql = @"select ccheckcode as ccode,ddate,b.cpersonname as cmaker,sourcecode,DARRIVALDATE,cvenname,c.cdepname from qmcheckvoucher a
left join person b on b.cpersoncode=a.CCHECKPERSONCODE
left join department c on c.cdepcode=a.cinspectdepcode
left join vendor d on d.cvencode=a.cvencode
left join pu_arrivalvouchs e on a.sourceautoid=e.autoid
where a.IVERIFYSTATE=1 and cvouchtype='qm03' and isnull(bpuinflag,0)=0 and isnull(a.cverifier,N'')<>N'' and (isnull(fregquantity,0)+isnull(fconquantiy,0))>0
and isnull(e.CBCLOSER,'') = ''
";
            //int i = OperationSql.GetDataset(strSql, connectionString, out dataset, out errMsg);
            //return i;
            int flag = -1;
            try
            {
                dataset = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        //退货单
        public static int GetReturnList(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {

            OrderList = null;
            errMsg = "";
            string strSql = @"select distinct pu_ArrHead.ccode,pu_ArrHead.ddate,pu_ArrHead.cdepname,pu_ArrHead.cmaker,pu_ArrHead.cvenname FROM  pu_ArrBody  inner join pu_ArrHead on pu_ArrHead.id=pu_ArrBody.id inner join (select cinvcode as cinvcode1,iId from inventory) inventory on pu_arrbody.cinvcode=inventory.cinvcode1  Where ( (1>0) AND (1>0) ) AND ( 1>0 ) and (isnull(cbustype,'')<>'委外加工') And (1=1)  and  isnull(cbcloser,N'')=N'' And isnull(cverifier,'')<>'' and iBillType=N'1' and isnull(bGsp,N'')=N'0' and   (isnull(iquantity,0)-isnull(frefusequantity,0)<isnull(fvalidinquan,0) or (igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)<isnull(fvalidinnum,0)))  ";
            if (ccode != null && ccode.Length > 0)
                strSql += " and pu_ArrHead.ccode='" + OrderList + "'";

            //int i = OperationSql.GetDataset(strSql, connectionString, out OrderList, out errMsg);
            //return i;
            int flag = -1;
            try
            {
                OrderList = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }

        #endregion

        #region 入库单上游单据查询
        
        #region 根据到货单生成入库单
        public static int CreateSIOrderByArriveOrder(string ArriveCode, string POCode, string cInvCode, int isReturn, string ConnectionString, out DataSet SIOrder, out string errMsg)
        {
            //string id = null;//到货单主表ID
//            string strHead = @"select distinct a.ddate as darvdate,f.cVenAbbName as cvenname,f.cvencode,a.cpersoncode,a.cvencode,a.cbustype,a.cdepcode,e.cdepname,a.cptcode,d.cpoid as cordercode,a.ccode as carvcode,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a.cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a.id as ipurarriveid,a.itaxrate,a.iexchrate,a.cexch_name,a.ccode as carvcode ,g.cwhcode,g.cwhname
//            from pu_arrivalvouch a
//            join pu_arrivalvouchs b on a.id=b.id join inventory i on i.cinvcode=b.cinvcode 
//            left join PO_Podetails c on c.id=b.iposid left join po_pomain d on d.poid=c.poid 
//            join department e on e.cdepcode=a.cdepcode join vendor f on f.cvencode=a.cvencode 
//            join Warehouse g on g.cWhCode=b.cWhCode
//            where isnull(b.cbcloser,N'')=N'' and isnull(b.bGsp,N'')=N'0' and isnull(a.cverifier,'')<>'' And iBillType =" + GetNull(isReturn.ToString()) + " And (isnull(a.cbustype,'')<>'委外加工') ";
            string strHead = "select distinct '01' cvouchtype,ibilltype bredvouch ,'" + ArriveCode + "' carvcode,a.ddate darvdate,a.id ipurarriveid,bwhpos,cwhcode,cwhname,'' as selcol,a.ccode,a.ddate,a.cptcode,a.cptname,a.cbustype,a.cvencode,a.cvenabbname,a.cdepcode,a.cdepname,a.cpersoncode,a.cpersonname,a.cmaker,a.cexch_name,a.iexchrate,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a.cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a.cmemo,a.ufts,a.itaxrate,'' as coufts,a.id,a.idiscounttaxtype,a.cvenpuomprotocol,a.cvenpuomprotocolname,a.iflowid,a.cflowname from pu_ArrHead a "
            + "join (select distinct pu_ArrHead.id,pu_ArrBody.autoid,convert(money,pu_ArrHead.ufts) as ufts,wh.bwhpos,wh.cwhcode,wh.cwhname FROM  pu_ArrBody  inner join pu_ArrHead on pu_ArrHead.id=pu_ArrBody.id inner join (select cinvcode as cinvcode1,iId from inventory) inventory on pu_arrbody.cinvcode=inventory.cinvcode1 join (select distinct cWhCode,cWhName,(case bWhPos when 1 then N'TRUE' else N'FALSE' end) as bWhPos from Warehouse) wh on isnull(pu_ArrBody.cwhcode,'01')=wh.cwhcode"  
            +" Where ( (1>0) AND (1>0) ) AND ( 1>0 ) and (isnull(cbustype,'')<>'委外加工') And (1=1)  and  isnull(cbcloser,N'')=N'' And isnull(cverifier,'')<>'' and iBillType=" + GetNull(isReturn.ToString()) + " and isnull(bGsp,N'')=N'0' and (2=2) ) as tempid on a.id=tempid.id where 1=1 ";
            if (isReturn == 1)
            {
//                strHead += @" and (isnull(b.iQuantity,0)-isnull(b.fRefuseQuantity,0)<=isnull(b.fValidInQuan,0) or 
//                            (i.igrouptype=2 and isnull(b.inum,0)-isnull(frefusenum,0)<=isnull(fvalidinnum,0))) ";
                strHead = strHead.Replace("2=2", " (isnull(iquantity,0)-isnull(frefusequantity,0)<isnull(fvalidinquan,0) or (igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)<isnull(fvalidinnum,0)))");
            }
            else
            {
//                strHead += @" and (isnull(b.iQuantity,0)-isnull(b.fRefuseQuantity,0)>isnull(b.fValidInQuan,0) or 
//                            (i.igrouptype=2 and isnull(b.inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0))) ";
                strHead = strHead.Replace("2=2", " (isnull(iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0))) and  isnull(bStorageArrivalOrder,1)=1 ");
            }
            if (string.IsNullOrEmpty(ArriveCode))
                strHead.Replace("distinct", "top 1 ");
            else
            {
                if (ArriveCode.Length > 0)
                    strHead += " and a.ccode='" + ArriveCode + "'  ";
                if (POCode.Length > 0)
                    strHead += " and d.cpoid='" + POCode + "'  ";
                if (cInvCode.Length > 0)
                    strHead += " and b.cinvcode='" + cInvCode + "'  ";
            }


            string strBody = @"select distinct a.iquantity,a.fvalidinquan,isnull(a.iquantity,0) orderquantity,Vendor.cVenCode,Vendor.cVenName,Vendor.cVenAbbName,(case a.bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case i.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,b.cPTCode,a.cwhcode,f.cinvccode,a.cbatchproperty6,i.cinvdefine1,i.cinvdefine2,i.cinvdefine4,a.cmassunit,a.imassdate,Convert(money,b.ufts) as corufts,a.cbatch,a.dpdate as dmadedate,a.dvdate,a.itaxprice,b.cauditdate as dbarvdate,a.sotype as isotype,b.ccode as cbarvcode,a.autoid,a.autoid as iArrsId,a.cordercode as cpoid,a.iposid,a.cdefine22 caddress,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37,a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cinvcode,a.icost as iunitcost,a.ioricost as facost,iorimoney as iprice,iorimoney as iaprice,iorimoney as imoney,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,
            cast((case when abs(isnull(a.iquantity,0)-isnull(frefusequantity,0))>abs(isnull(fvalidinquan,0)) then isnull(a.iquantity,0)-isnull(frefusequantity,0)-isnull(fvalidinquan,0) else 0 end) as decimal(10,2)) as iNQuantity,i.cinvstd,i.cinvname,b.itaxrate,w.cwhname,w.cwhcode,i.caddress,a.cordercode iordercode,a.cordercode,cComUnitName cinvm_unit
            from pu_arrivalvouchs a join pu_arrivalvouch b on a.id=b.id left join warehouse w on w.cwhcode=a.cwhcode 
            left join inventory i on i.cinvcode=a.cinvcode
            left join computationunit c on i.ccomunitcode=c.ccomunitcode
            left join InventoryClass f on f.cinvccode=i.cinvccode
            left join po_podetails pds on pds.id=a.iposid 
            left join po_pomain pm on pm.poid=pds.poid 
            left join Vendor on Vendor.cVenCode=b.cVenCode 
            Where isnull(a.cbcloser,N'')=N'' and isnull(b.cverifier,'')<>'' And b.iBillType =" + GetNull(isReturn.ToString()) + " and isnull(a.bGsp,N'')=N'0' And (isnull(b.cbustype,'')<>'委外加工') ";
            if (isReturn == 1)
            {
//                strBody += @" and (isnull(a.iQuantity,0)-isnull(a.fRefuseQuantity,0)<=isnull(a.fValidInQuan,0) or 
//                            (i.igrouptype=2 and isnull(a.inum,0)-isnull(frefusenum,0)<=isnull(fvalidinnum,0))) ";
                strBody += @" and (isnull(a.iquantity,0)-isnull(frefusequantity,0)<isnull(fvalidinquan,0) or (igrouptype=2 and isnull(a.inum,0)-isnull(frefusenum,0)<isnull(fvalidinnum,0))) and isnull(a.iquantity,0)<0 ";
            }
            else
            {
//                strBody += @" and (isnull(a.iQuantity,0)-isnull(a.fRefuseQuantity,0)>isnull(a.fValidInQuan,0) or 
//                            (i.igrouptype=2 and isnull(a.inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0))) ";
                strBody += @" and (isnull(a.iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(a.inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0)))
   and cast((case when abs(isnull(a.iquantity,0)-isnull(frefusequantity,0))>abs(isnull(fvalidinquan,0)) then isnull(a.iquantity,0)-isnull(frefusequantity,0)-isnull(fvalidinquan,0) else 0 end) as decimal(10,2))>0";
            }
            //通过后面的到货单主表ID条件查询
            if (string.IsNullOrEmpty(ArriveCode))
                strBody += " and 2=1";
            else
            {
                if (ArriveCode.Length > 0)
                    strBody += " and b.ccode='" + ArriveCode + "'  ";
                if (POCode.Length > 0)
                    strBody += " and pm.cpoid='" + POCode + "'  ";
                if (cInvCode.Length > 0)
                    strBody += " and a.cinvcode='" + cInvCode + "'  ";
            }

            int i;
            string connString = ConnectionString;
            DataSet ds = new DataSet();
            SIOrder = null;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                if (ds.Tables["head"].Rows.Count == 0)
                {
                    throw new Exception("单号不存在或未审核!");
                }

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                if (ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("单据已处理或数据有误!");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                SIOrder = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            i = 0;
            return i;
        }
        #endregion

        #region 根据GSP质检单生成入库单

        public static int CreateSIOrderByGSPVouch(string cCode,string sCode, string connectionString, out DataSet SIOrder, out string errMsg)
        {
            int i;
            errMsg = "";
            SIOrder = null;
            string strHead = null;
            string strBody = null;
            string connString = connectionString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            strHead = "select distinct cptcode,cbuscode,cbustype,0 bredvouch,a.csourcevouchcode carvcode,a.ddate dchkdate,cmaker cchkperson,itaxrate,cexch_name,iexchrate,c.cpersoncode,c.cdepcode,darvdate,'' as SelCol,a.cvouchtype,a.cvouchname csource,a.cvouchcode cchkcode,a.sourceid,a.csourcevouchcode cbuscode,a.ddate,ufts,a.cDefine16 cDefine1,a.cDefine2,a.cDefine3,a.cDefine4,a.cDefine5,a.cDefine6,a.cDefine7,a.cDefine8,a.cDefine9,a.cDefine10,a.cDefine11,a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine15 cvencode,a.cDefine16 cvenname,a.cmemo,a.ID,bwhpos,cwhcode,cwhname "
                + " from  kc_gsp_purchaseinH a join kc_gsp_purchasein b on a.id=b.id join (select cCode,iTaxRate,cexch_name,iExchRate,cptcode,ccode cbuscode,cbustype,cPersonCode,cdepcode,ddate dArvDate from PU_ArrivalVouch) c on b.csourcevouchcode=c.cCode join (select distinct cWhCode whcode,cWhname whname,(case bWhPos when 1 then N'TRUE' else N'FALSE' end) as bWhPos from Warehouse) wh on isnull(b.cwhcode,'01')=wh.whcode where( (1>0) AND (1>0) ) AND ( 1>0 ) and  isnull(bMake,'')=N'否' and (isnull(iquantity,0)<>0) ";
            if (string.IsNullOrEmpty(cCode) && string.IsNullOrEmpty(sCode))
                strHead.Replace("distinct", "top 1 ");
            {
                if (!string.IsNullOrEmpty(cCode))
                    strHead += " and a.cvouchcode = '" + cCode + "'";
                if (!string.IsNullOrEmpty(sCode))
                    strHead += " and a.csourcevouchcode = '" + sCode + "'";
            }

            strBody = "select (case b.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,itaxrate,fvalidinquan,isnull(a.iquantity,0) orderquantity,a.Autoid iCheckIds,dCheckDate,iproorderid,ufts corufts,'' cgspstate,a.Autoid,c.Autoid iArrsId,bGSP,iOriCost,iOriTaxCost,iOriMoney,iOriTaxPrice,iOriSum,iCost,iMoney,iSum,cordercode cpoid,cordercode iordercode,cSourceVouchCode cBarvCode,cVouchCode cCheckCode,'' as SelCol,a.cdepcode,a.cdepname,a.cpersoncode,a.ccheckperson,a.ccuscode,a.ccusname,a.cvencode,a.cvenname,d.cVenAbbName,a.cmaker,a.ufts,a.cverifier,a.autoid,a.sourceautoid,a.cinvcode,a.cinvname,a.cinvstd,a.cbatch,a.dmadedate,a.dvdate,a.imassdate,a.ccomunitcode,a.cinvm_unit,a.cassunit,a.cinva_unit,(isnull(a.iquantity,0)-isnull(c.fValidInQuan,0)) inquantity,a.inum,a.ichangerate,a.cwhcode,a.cwhname,a.citemccode,a.citemcname,a.citemcode,a.citemname,a.iunitcost,a.iprice,a.cdefine22 caddress,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cDefine28,a.cDefine29,a.cDefine30,a.cDefine31,a.cDefine32,a.cDefine33,a.cDefine34,a.cDefine35,a.cDefine36,a.cDefine37,a.cFree1,a.cFree2,a.cFree3,a.cFree4,a.cFree5,a.cFree6,a.cFree7,a.cFree8,a.cFree9,a.cFree10,a.cbmemo,a.bmake,a.cmassunit,a.cinvouchcode,a.cvouchcode1,a.ID FROM  kc_gsp_purchasein a join inventory b on a.cinvcode=b.cinvcode join (select autoid,cordercode,fValidInQuan from PU_ArrivalVouchs) c on a.sourceautoid=c.autoid join (select cVenCode,cVenAbbName from Vendor) d on a.cvencode=d.cVenCode join (select distinct autoid,(case bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,iOriCost,iOriTaxCost,iOriMoney,iOriTaxPrice,iOriSum,iCost,iMoney,iSum from PU_ArrivalVouchs) avs on c.autoid=avs.autoid join (select distinct autoid,ddate_t dCheckDate,POID iproorderid from GSP_VouchsQC a left join po_pomain b on a.cordercode=b.cPOID) chk on a.autoid=chk.autoid Where ( (1>0) AND (1>0) ) AND ( 1>0 ) and  isnull(a.bMake,'')=N'否' and (isnull(a.iquantity,0)<>0) and a.id in (select distinct id FROM kc_gsp_purchasein Where (isnull(a.iquantity,0)-isnull(c.fValidInQuan,0))>0 and ( (1>0) AND (1>0) ) AND ( 1>0 ) and  isnull(bMake,'')=N'否' and (isnull(iquantity,0)<>0))";
            if (!string.IsNullOrEmpty(cCode))
                strBody += " and a.cvouchcode = '" + cCode + "'";
            else if (!string.IsNullOrEmpty(sCode))
                strBody += " and a.csourcevouchcode = '" + sCode + "'";
            else
                strBody += " and 1=2";

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                if (ds.Tables["head"].Rows.Count == 0)
                {
                    throw new Exception("单号不存在或未审核!");
                }

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                if (ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("单据已处理或数据有误!");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                SIOrder = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            i = 0;
            return i;
        }

        #endregion

        #region 组装拆卸调拨生成入库单

        public static int CreateSIOrderByRecordList(string type, string ccode, string bcode, string connectionString, out DataSet OrderList, out string errMsg)
        {
            OrderList = null;
            errMsg = "";
            #region 自定义参数
            string source = "";
            string bustype = "";
            string vouchtype = "";
            byte brdflag = 0;

            string _source = "";
            string _bustype = "";
            string _vouchtype = "";
            byte _brdflag = 0;

            if (type == "00")
            {
                source = "拆卸";
                bustype = "拆卸入库";
                vouchtype = "08";
                brdflag = 1;

                _source = "拆卸";
                _bustype = "拆卸出库";
                _vouchtype = "09";
                _brdflag = 0;
            }
            else if (type == "01")
            {
                source = "拆卸";
                bustype = "拆卸出库";
                vouchtype = "09";
                brdflag = 0;
            }
            else if (type == "10")
            {
                source = "组装";
                bustype = "组装入库";
                vouchtype = "08";
                brdflag = 1;

                _source = "组装";
                _bustype = "组装出库";
                _vouchtype = "09";
                _brdflag = 0;
            }
            else if (type == "11")
            {
                source = "组装";
                bustype = "组装出库";
                vouchtype = "09";
                brdflag = 0;
            }
            else if (type == "20")
            {
                source = "调拨";
                bustype = "调拨入库";
                vouchtype = "08";
                brdflag = 1;

                _source = "调拨";
                _bustype = "调拨出库";
                _vouchtype = "09";
                _brdflag = 0;
            }
            else if (type == "21")
            {
                source = "调拨";
                bustype = "调拨出库";
                vouchtype = "09";
                brdflag = 0;
            }
            else
            {
                throw new Exception("参数错误!");
            }
            #endregion

            string strHead = @"select distinct ccode,cbuscode,id,brdflag,cvouchtype,cbustype,csource,bwhpos,cwhcode,cWhName,ddate,cdepcode,cvencode,cordercode,carvcode,cmaker,bpufirst,darvdate,vt_id,bisstqc,ipurarriveid,itaxrate,iexchrate,cexch_name,idiscounttaxtype,iswfcontrolled,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,bcredit,bomfirst,dnmaketime,chandler,dnverifytime,crdcode,dveridate,cptcode,cpersoncode,controlresult from rdrecord join (select cWhCode whcode,cWhName,(case bWhPos when 1 then N'TRUE' else N'FALSE' end) as bWhPos from Warehouse) wh on rdrecord.cwhcode=wh.whcode where brdflag=" + brdflag + " and cvouchtype=" + vouchtype + " and cbustype='" + bustype + "' and csource='" + source + "' and isnull(dVeriDate,'')='' and isnull(cHandler,'')='' ";
            if (!string.IsNullOrEmpty(bcode))
                strHead += " and cBusCode  ='" + bcode + "'";
            else if (!string.IsNullOrEmpty(ccode))
                strHead += " and cCode  ='" + ccode + "'";
            else
                strHead.Replace("distinct", "top 1 ");
            string strBody = "select distinct (case bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case b.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,cComUnitName cinvm_unit,b.itaxrate,ccode,brdflag,c.cwhcode,cwhname,autoid,a.id,a.cinvcode,c.cvencode,cvenname,cvenabbname,a.inum,a.iquantity inquantity,a.iquantity orderquantity,0 fvalidinquan,a.iunitcost,a.iprice,a.iaprice,a.ipunitcost,a.ipprice,a.cbatch,a.cvouchcode,a.cfree1,a.cfree2,a.dsdate,a.itax,a.isquantity,a.isnum,a.imoney,a.isoutquantity,a.isoutnum,a.ifnum,a.ifquantity,a.dvdate,a.itrids,a.cposition,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.citem_class,a.citemcode,a.iposid,a.facost,a.cname,a.citemcname,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cbarcode,a.innum,a.cassunit,a.dmadedate,a.imassdate,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37,a.impoids,a.icheckids,a.cbvencode,a.cinvouchcode,a.cgspstate,a.iarrsid,a.ccheckcode,a.icheckidbaks,a.crejectcode,a.irejectids,a.ccheckpersoncode,a.dcheckdate,a.ioritaxcost,a.ioricost,a.iorimoney,a.ioritaxprice,a.iorisum,a.itaxrate,a.itaxprice,a.isum,a.btaxcost,a.cpoid,a.cmassunit,a.imaterialfee,a.iprocesscost,a.iprocessfee,a.dmsdate,a.ismaterialfee,a.isprocessfee,a.iomodid,a.isodid,a.strcontractid,a.strcode,a.isotype,a.corufts,a.cbaccounter,a.bcosting,a.isumbillquantity,a.bvmiused,a.ivmisettlequantity,a.ivmisettlenum,a.cvmivencode,a.iinvsncount,a.impcost,a.iimosid,a.iimbsid,a.cbarvcode,a.dbarvdate,a.cexpirationdate,a.dexpirationdate,a.iexpiratdatecalcu,a.cbatchproperty6,a.iordertype,b.caddress,b.cinvname ,b.cInvStd ,b.cinvccode from rdrecords a join inventory b on a.cinvcode=b.cinvcode join rdrecord c on a.ID=c.ID join (select cWhCode,cWhName from Warehouse) wh on c.cwhcode=wh.cWhCode left join Vendor d on c.cVenCode=d.cVenCode join ComputationUnit e on b.cComUnitCode = e.cComunitCode where brdflag=" + brdflag + " and cvouchtype=" + vouchtype + " and cbustype='" + bustype + "' and csource='" + source + "'";
           if (!string.IsNullOrEmpty(bcode))
                strBody += " and c.cbuscode = '" + bcode + "'";
           else if (!string.IsNullOrEmpty(ccode))
               strBody += " and c.cCode  ='" + ccode + "'";
            else
                strBody.Replace("distinct", "top 1 ");

            int i;
            string connString = connectionString;
            DataSet ds = new DataSet();
            OrderList = null;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                if (ds.Tables["head"].Rows.Count == 0)
                {
                    throw new Exception("单号不存在!");
                }
                if (type[1] == '0')
                {
                    #region 先出后入

                    bool isVerify = true;
                    string strOut = @"select distinct top 1 (case isnull(dVeriDate,'') when '' then N'FALSE' else N'TRUE' end) bVeriDate,(case isnull(cHandler,'') when '' then N'FALSE' else N'TRUE' end) bVeriDate from rdrecord where brdflag=" + _brdflag + " and cvouchtype=" + _vouchtype + " and cbustype='" + _bustype + "' and csource='" + _source + "' ";
                    if (!string.IsNullOrEmpty(bcode))
                        strOut += " and cBusCode  ='" + bcode + "'";
                    else if (!string.IsNullOrEmpty(ccode))
                        strOut += " and cCode  ='" + ccode + "'";
                    cmd.CommandText = strOut;
                    object verify = cmd.ExecuteScalar();
                    if (!Convert.IsDBNull(verify) && verify != null)
                        isVerify = bool.Parse(verify.ToString());
                    if (!isVerify)
                        throw new Exception("请先处理出库单据!");
                    #endregion
                }

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                OrderList = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            i = 0;
            return i;
        }

        #endregion

        #region 根据委外订单生成出库单

        public static int CreateSIOrderByMomain(string cCode, string connectionString, out DataSet SIOrder, out string errMsg)
        {
            int i;
            errMsg = "";
            SIOrder = null;
            string strHead = null;
            string strBody = null;
            string connString = connectionString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            strHead = "select distinct cwhcode,cwhname,om_mohead.moid ipurorderid,iquantity imquantity,ccode cMPoCode,itaxrate,'' as selcol,irowno,csocode,sotype,ccode,ddate,cdepcode,cdepname,cvencode,cvenabbname,cvenname,cpersoncode,cpersonname,cmaker,cverifier,cinvcode,cinvcode cpspcode,cinvaddcode,cinvname,cinvstd,cinvdefine6 caddress,ccomunitcode,cinvm_unit,cunitid,cinva_unit,citem_class,citem_name,citemcode,citemname,dstartdate,darrivedate,iquantity,inum,(case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end ) as ireceivedqty,((isnull(iquantity,0)- (case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end))) as iqty,modetailsid,ufts,irowno ivouchrowno,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cinvdefine1,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine5,cinvdefine6,cinvdefine7,cinvdefine8,cinvdefine9,cinvdefine10,cinvdefine11,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16 FROM om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did,moid,cwhcode,cwhname  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) ";
            if (!string.IsNullOrEmpty(cCode))
                strHead += " AND ccode = " + SelSql(cCode);

            strBody = " select (case c.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,d.cvencode,d.cvenabbname,d.cvenname,c.caddress,ufts corufts,csocode,a.MODetailsID isodid,momaterialsID iomomid,'' iposid,b.ccode comcode,a.MODetailsID iOMoDID,ccode cmocode,'' as selcol,a.cwhcode,a.cwhname,a.cinvcode,a.cinvaddcode,a.cinvname,a.cinvstd,a.ccomunitcode,a.cinvm_unit,isnull(iInvSCost,0) iunitcost,a.cbatch,b.iTaxRate,imassdate,a.drequireddate,a.iunitquantity,a.iquantity orderquantity,a.iquantity,((isnull(a.isendqty,0)-isnull(a.iComplementQty,0))) as isendqty,((isnull(a.iquantity,0)-isnull(a.isendqty,0)+isnull(a.iComplementQty,0))) as inquantity,(a.ftransqty) as ftransqty,(a.ftransnum) as ftransnum,a.MoDetailsID,a.MOMaterialsID,b.ufts,a.iWIPType,a.cassunit,a.cinva_unit,a.iinvexchrate,a.itnum,a.isnum,a.iunnum,a.iComplementQty,8 iSoType,2 iExpiratDateCalcu,1 bCosting,a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cinvdefine1,a.cinvdefine2,a.cinvdefine3,a.cinvdefine4,a.cinvdefine5,a.cinvdefine6,a.cinvdefine7,a.cinvdefine8,a.cinvdefine9,a.cinvdefine10,a.cinvdefine11,a.cinvdefine12,a.cinvdefine13,a.cinvdefine14,a.cinvdefine15,a.cinvdefine16,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37 "
                + "FROM  om_momaterialsbody a inner join om_mohead b on a.moid=b.moid join Inventory c on a.cinvcode=c.cinvcode join Vendor d on b.cvencode=d.cVenCode where isnull(iwiptype,3)=3 and (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0))>0  AND 1=1 ";
            if (!string.IsNullOrEmpty(cCode))
                strBody += " and MoDetailsID in (select distinct MoDetailsID from OM_MODetails where moid in (select moid from OM_MOMain where ccode = " + SelSql(cCode) + "))";

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                if (ds.Tables["head"].Rows.Count == 0)
                {
                    throw new Exception("单号不存在或未审核!");
                }

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                if (ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("单据已处理或数据有误!");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                SIOrder = resDs;
                GetMoCost(cCode, connString, SIOrder, out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            i = 0;
            return i;
        }

        #endregion

        #region 根据检验单生成入库单

        public static int CreateSIOrderByQMCheckOrder(string cinvcode, string cbatch, string cCheckCode, string cArriveCode, string ConnectionString, out DataSet SIOrder, out string errMsg)
        {
            string strHead = @"select b.ddate as darvdate,a.cvencode,c.cvenname,b.cptcode,a.ccheckpersoncode,b.cpersoncode,b.cdepcode,b.cbustype,a.cvencode,cpocode as cordercode,sourcecode as carvcode,b.itaxrate,b.iexchrate,b.cexch_name,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a.cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,e.cwhcode,w.cwhname  
from qmcheckvoucher a join pu_arrivalvouchs e on a.sourceautoid=e.autoid join pu_arrivalvouch b on b.id=e.id 
join Vendor  c on c.cvencode=b.cvencode join warehouse w on w.cwhcode=e.cwhcode 
where isnull(a.cverifier,N'')<>N'' and isnull(bpuinflag,0)=0 and a.cvouchtype=N'qm03' and 
(isnull(fregquantity,0)+isnull(fconquantiy,0))>0 AND isnull(e.CBCLOSER,'') = '' ";
            if (cCheckCode != null)
                strHead += " and a.CINSPECTCODE ='" + cCheckCode + "'  ";
            if (cArriveCode != null)
                strHead += " and b.ccode ='" + cArriveCode + "'  ";

            string strBody = @"select distinct (case b.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,e.iexpiratdatecalcu,e.cexpirationdate,e.dexpirationdate,b.cPosition,e.cwhcode,e.dpdate as dmadedate,e.dvdate,a.cbatch,e.itaxrate,convert(money,d.ufts) as corufts,a.ccheckcode,a.ccheckpersoncode,a.ddate as dcheckdate,d.cauditdate as dbarvdate,d.ccode as cbarvcode,e.cordercode as cpoid,b.cmassunit,b.imassdate,e.autoid as iarrsid,c.ccomunitname as cinva_unit,e.autoid,e.iposid,a.id as icheckidbaks,e.cdefine22,e.cdefine23,e.cdefine24,e.cdefine25,e.cdefine26,e.cdefine27,e.cdefine28,e.cdefine29,e.cdefine30,e.cdefine31,e.cdefine32,e.cdefine33,e.cdefine34,e.cdefine35,e.cdefine36,e.cdefine37,a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cinvcode,e.icost as iunitcost,iorimoney as iprice,iorimoney as iaprice,((isnull(fregquantity,0)+isnull(fconquantiy,0))*e.imoney/e.iquantity) as  imoney,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,b.cinvname,b.cinvstd,
cast(isnull(fregquantity,0)+isnull(fconquantiy,0)-isnull(fregbreakquantity,0)-isnull(FsumQuantity,0) as decimal(10,2)) as inquantity,Case isnull(b.igrouptype,0) When 0 then 0 When 1 then (isnull(fregquantity,0)+isnull(fconquantiy,0)-isnull(FsumQuantity ,0))/c.ichangrate When 2 then (isnull(fregnum,0)+isnull(fconnum,0)-isnull(FsumNum ,0)) End as inum 

from qmcheckvoucher a inner join inventory b on a.cinvcode=b.cinvcode inner join pu_arrivalvouchs e on a.sourceautoid=e.autoid 
inner join pu_arrivalvouch d on e.id=d.id left join person on a.ccheckpersoncode=person.cpersoncode 
left join purchasetype on d.cptcode=purchasetype.cptcode left join vendor on a.cvencode=vendor.cvencode 
left join department on a.cinspectdepcode=department.cdepcode 
left join person as person1 on d.cpersoncode=person1.cpersoncode left join warehouse on a.cwhcode=warehouse.cwhcode 
left join rd_style on purchasetype .crdcode=rd_style.crdcode left join computationunit c on b.ccomunitcode=c.ccomunitcode 
left join computationunit as computationunit1 on a.cunitid = computationunit1.ccomunitcode 
left join aa_agreement on d.cvenpuomprotocol = aa_agreement.ccode  
LEFT OUTER JOIN SO_SODETAILS ON cast(SO_SODETAILS.ISOSID as nvarchar(50))=a.ISOORDERAUTOID  
LEFT OUTER JOIN EX_ORDERDETAIL ON cast(EX_ORDERDETAIL.AUTOID as nvarchar(50))=a.ISOORDERAUTOID 
where isnull(a.cverifier,N'')<>N'' and isnull(bpuinflag,0)=0 and a.cvouchtype=N'qm03' and 
(isnull(fregquantity,0)+isnull(fconquantiy,0))>0 AND isnull(e.CBCLOSER,'') = '' ";
            if (cCheckCode != null)
                strBody += " and a.CINSPECTCODE ='" + cCheckCode + "'  ";
            if (cArriveCode != null)
                strBody += " and d.ccode ='" + cArriveCode + "'  ";

            int i;
            string connString = ConnectionString;
            DataSet ds = new DataSet();
            SIOrder = null;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);

                if (ds.Tables["head"].Rows.Count == 0 || ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("检验单不存在或未审核或批检结果不接收!");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                SIOrder = resDs;
                i = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;

                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return i;
        }
        #endregion

        #region 不良品处理单
        public static int CreateSIOrderByQMREJECTVOUCHERS(string cinvcode, string cbatch, string cwhcode, string ConnectionString, out DataSet SIOrder, out string errMsg)
        {
            string strHead = @"
Select * From (Select N'' as selcol,QMRejectVoucher.cRejectCode,QMRejectVoucher.dCheckDate,
cCheckCode,cCheckperson as cCheckpersoncode,person.cpersonname as cCheckpersonName,QMRejectVouchers.autoid as iRejectIds,
dArrivalDate as darvdate,SourceCode,pu_arrivalvouch.cPTCode,cPTName,pu_arrivalvouch.cBusType,pu_arrivalvouchs.iOriCost as iunitcost,pu_arrivalvouchs.itaxrate,
pu_arrivalvouch.cexch_name,pu_arrivalvouch.idiscounttaxtype,pu_arrivalvouch.iexchrate,vendor.iid as vendoriid,
QMRejectVoucher.cVenCode,cVenName,cInspectDepCode,pu_arrivalvouchs.dpdate as dmadedate,
department.cdepname as cInspectDepName,pu_arrivalvouch.cpersonCode,pu_arrivalvouch.ccode as cbarvcode,pu_arrivalvouch.ddate as dbarvdate,
person1.cpersonName,PU_ArrivalVouch.cMemo,pu_arrivalvouchs.iposid,pu_arrivalvouchs.iOritaxcost,
QMRejectVoucher.cDefine1,QMRejectVoucher.cDefine2,QMRejectVoucher.cDefine3,QMRejectVoucher.cDefine4,QMRejectVoucher.cDefine5,QMRejectVoucher.cDefine6,QMRejectVoucher.cDefine7,QMRejectVoucher.cDefine8,QMRejectVoucher.cDefine9,QMRejectVoucher.cDefine10,QMRejectVoucher.cDefine11,QMRejectVoucher.cDefine12,QMRejectVoucher.cDefine13,QMRejectVoucher.cDefine14,QMRejectVoucher.cDefine15,QMRejectVoucher.cDefine16,
cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,
QMRejectVouchers.cFree1 as cFree1,QMRejectVouchers.cFree2 as cFree2,QMRejectVouchers.cFree3 as cFree3,QMRejectVouchers.cFree4 as cFree4,QMRejectVouchers.cFree5 as cFree5,QMRejectVouchers.cFree6 as cFree6,QMRejectVouchers.cFree7 as cFree7,QMRejectVouchers.cFree8 as cFree8,QMRejectVouchers.cFree9 as cFree9,QMRejectVouchers.cFree10 as cFree10,
PU_ArrivalVouchs.cDefine22,PU_ArrivalVouchs.cDefine23,PU_ArrivalVouchs.cDefine24,PU_ArrivalVouchs.cDefine25,PU_ArrivalVouchs.cDefine26,PU_ArrivalVouchs.cDefine27,PU_ArrivalVouchs.cDefine28,PU_ArrivalVouchs.cDefine29,PU_ArrivalVouchs.cDefine30,PU_ArrivalVouchs.cDefine31,PU_ArrivalVouchs.cDefine32,PU_ArrivalVouchs.cDefine33,PU_ArrivalVouchs.cDefine34,PU_ArrivalVouchs.cDefine35,PU_ArrivalVouchs.cDefine36,PU_ArrivalVouchs.cDefine37,
pu_arrivalvouchs.ivouchrowno,PU_ArrivalVouch.ccode as carvcode,PU_ArrivalVouch.cauditdate as carvdate,
QMRejectVoucher.cWhCode as csourcewhcode,sourcewarehouse.cwhname as csourcewhname,PU_ArrivalVouchs.autoid as iarrsid,
QMRejectVouchers.cBWhCode as cWhCode,warehouse.cWhName as cwhname,purchasetype.cRdCode,cRdName,
QMRejectVouchers.cdimInvCode as cInvCode,cInvAddCode,Inventory.cInvName,Inventory.cInvStd,
QMRejectVouchers.Autoid,QMRejectVouchers.Id,SourceAutoID,pu_arrivalvouchs.icost as fCost,
((isnull(fdimquantity,0))*pu_arrivalvouchs.icost) as  fMOney,iInvRCost as fInvRCost,
(isnull(fdimQuantity,0)*iInvRCost) as fInvRMOney,inventory.cComUnitCode,
QMRejectVouchers.CdimUNITID as CUNITID,cItemClass,cItemCName,pu_arrivalvouch.cdepcode,
QMRejectVoucher.cItemCode,QMRejectVoucher.cItemName,ComputatiOnUnit.cComUnitName,
ComputatiOnUnit1.cComUnitName as CUNITNAME,QMRejectVoucher.checkid as icheckidbaks,
QMRejectVouchers.FDIMCHANGRATE AS FCHANGRATE,fDimQuantity as iQuantity,FDimnum as inum,
(Case isnull(Inventory.bInvBatch,0) when 0 Then null Else QMRejectVouchers.cdimBatch End)as cBatch,
(case  when (isnull(inventory.binvbatch,0)<>0 and isnull(inventory.cmassunit,N'')<>N'') then QMRejectVoucher.dProDate else null end) as dprodate,
(Case isnull(inventory.bInvQuality,0) when 0 Then null Else QMRejectVouchers.idimmassdate End) as imassdate,
(Case isnull(inventory.bInvQuality,0) when 0 Then null Else QMRejectVouchers.ddimVDate End) as dVDate,
QMRejectVouchers.iDIMExpiratDateCalcu as iExpiratDateCalcu,QMRejectVouchers.cDIMExpirationdate as cExpirationdate,QMRejectVouchers.dDIMExpirationdate as dExpirationdate,
(case when bservice=1 then N'是' else N'否' end) as bservice,
binvbatch,
CONVERT(char,COnvert(mOney,QMRejectVoucher.Ufts),2) as ufts,
inventory.cGroupCode,iGroupType,QMRejectVouchers.cdimmassunit as cmassunit,
0 as iordertype,cast('' as nvarchar(100)) as csocode,cast('' as nvarchar(50)) as iSoDID, cast(null as int) as isoseq,
QMRejectVoucher.IORDERDID,QMRejectVoucher.ISOORDERTYPE,PO_Pomain.cpoid as CORDERCODE,QMRejectVoucher.IORDERSEQ,
 pu_arrivalvouch.cvenpuomprotocol,aa_agreement.cname as cvenpuomprotocolname, QMRejectVoucher.CBATCHPROPERTY1,QMRejectVoucher.CBATCHPROPERTY2,QMRejectVoucher.CBATCHPROPERTY3,
QMRejectVoucher.CBATCHPROPERTY4,QMRejectVoucher.CBATCHPROPERTY5,QMRejectVoucher.CBATCHPROPERTY6,
QMRejectVoucher.CBATCHPROPERTY7,QMRejectVoucher.CBATCHPROPERTY8,QMRejectVoucher.CBATCHPROPERTY9,
QMRejectVoucher.CBATCHPROPERTY10,
inventory.iid,pu_arrivalvouchs.cciqbookcode,pu_arrivalvouch.iflowid as iflowid,pubizflow.cflowname as cflowname  From QMRejectVoucher Inner Join QMRejectVouchers On QMRejectVoucher.id = QMRejectVouchers.id Inner Join inventory On QMRejectVouchers.cdiminvcode = inventory.cinvcode Inner Join PU_ArrivalVouchs On QMRejectVoucher.SourceAutoid = PU_ArrivalVouchs.autoid Inner Join PU_ArrivalVouch On PU_ArrivalVouchs.id = PU_ArrivalVouch.id 
left join pubizflow on pubizflow.iflowid=pu_arrivalvouch.iflowid 
Left Join person On QMRejectVoucher.cCheckperson = person.cpersoncode 
Left Join PurchaseType On PU_ArrivalVouch.cptcode = PurchaseType.cptcode 
Left Join vEndor On QMRejectVoucher.cvencode = vEndor.cvencode 
Left Join department On QMRejectVoucher.cinspectdepcode = department.cdepcode 
Left Join person as person1 On PU_ArrivalVouch.cpersoncode = person1.cpersoncode 
Left Join warehouse On QMRejectVouchers.cbwhcode = warehouse.cwhcode 
Left Join Rd_Style On PurchaseType.crdcode = Rd_Style.crdcode 
Left Join ComputatiOnUnit On inventory.cComUnitCode = ComputatiOnUnit.cComUnitCode 
Left Join ComputatiOnUnit as ComputatiOnUnit1 On QMRejectVouchers.CdimUNITID  =  ComputatiOnUnit1.cComUnitCode 
left join aa_agreement on pu_arrivalvouch.cvenpuomprotocol = aa_agreement.ccode  
LEFT OUTER JOIN SO_SODETAILS ON  QMREJECTVOUCHER.IORDERTYPE =1 and cast(SO_SODETAILS.ISOSID as nvarchar(50))=QMRejectVoucher.ISOORDERAUTOID  
LEFT OUTER JOIN EX_ORDERDETAIL ON  QMREJECTVOUCHER.IORDERTYPE =3 and cast(EX_ORDERDETAIL.AUTOID as nvarchar(50))=QMRejectVoucher.ISOORDERAUTOID  
Left Join warehouse sourcewarehouse On QMRejectVoucher.cwhcode = sourcewarehouse.cwhcode 
left join po_podetails on po_podetails.id =pu_arrivalvouchs.iPOsID
left join PO_Pomain on PO_Pomain.poid=po_podetails.poid
Where isnull(QMRejectVoucher.cVerifier,N'')<>N'' and IDISPOSEFLOW = 2 and QMRejectVoucher.cvouchtype = N'QM05'  and isnull(QMRejectVouchers.bflag,0) = 0 and (isnull(fdimQuantity,0)>0) and isnull(pu_arrivalvouchs.CBCLOSER,'') = '' ) as aa  Where 1 = 1  and ( (1>0) AND (1>0) ) AND ( 1>0 ) 
and cinvcode='" + cinvcode + "' and cbatch='" + cbatch + "'";

            string strBody = @"
Select * From (Select N'' as selcol,QMRejectVoucher.cRejectCode,QMRejectVoucher.dCheckDate,
cCheckCode,cCheckperson as cCheckpersoncode,person.cpersonname as cCheckpersonName,QMRejectVouchers.autoid as iRejectIds,
dArrivalDate as darvdate,SourceCode,pu_arrivalvouch.cPTCode,cPTName,pu_arrivalvouch.cBusType,pu_arrivalvouchs.iOriCost,pu_arrivalvouchs.itaxrate,
pu_arrivalvouch.cexch_name,pu_arrivalvouch.idiscounttaxtype,pu_arrivalvouch.iexchrate,vendor.iid as vendoriid,
QMRejectVoucher.cVenCode,cVenName,cInspectDepCode,pu_arrivalvouchs.dpdate as dmadedate,
department.cdepname as cInspectDepName,pu_arrivalvouch.cpersonCode,pu_arrivalvouch.ccode as cbarvcode,pu_arrivalvouch.ddate as dbarvdate,
person1.cpersonName,PU_ArrivalVouch.cMemo,pu_arrivalvouchs.iposid,pu_arrivalvouchs.iOritaxcost,
QMRejectVoucher.cDefine1,QMRejectVoucher.cDefine2,QMRejectVoucher.cDefine3,QMRejectVoucher.cDefine4,QMRejectVoucher.cDefine5,QMRejectVoucher.cDefine6,QMRejectVoucher.cDefine7,QMRejectVoucher.cDefine8,QMRejectVoucher.cDefine9,QMRejectVoucher.cDefine10,QMRejectVoucher.cDefine11,QMRejectVoucher.cDefine12,QMRejectVoucher.cDefine13,QMRejectVoucher.cDefine14,QMRejectVoucher.cDefine15,QMRejectVoucher.cDefine16,
cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,
QMRejectVouchers.cFree1 as cFree1,QMRejectVouchers.cFree2 as cFree2,QMRejectVouchers.cFree3 as cFree3,QMRejectVouchers.cFree4 as cFree4,QMRejectVouchers.cFree5 as cFree5,QMRejectVouchers.cFree6 as cFree6,QMRejectVouchers.cFree7 as cFree7,QMRejectVouchers.cFree8 as cFree8,QMRejectVouchers.cFree9 as cFree9,QMRejectVouchers.cFree10 as cFree10,
PU_ArrivalVouchs.cDefine22,PU_ArrivalVouchs.cDefine23,PU_ArrivalVouchs.cDefine24,PU_ArrivalVouchs.cDefine25,PU_ArrivalVouchs.cDefine26,PU_ArrivalVouchs.cDefine27,PU_ArrivalVouchs.cDefine28,PU_ArrivalVouchs.cDefine29,PU_ArrivalVouchs.cDefine30,PU_ArrivalVouchs.cDefine31,PU_ArrivalVouchs.cDefine32,PU_ArrivalVouchs.cDefine33,PU_ArrivalVouchs.cDefine34,PU_ArrivalVouchs.cDefine35,PU_ArrivalVouchs.cDefine36,PU_ArrivalVouchs.cDefine37,
pu_arrivalvouchs.ivouchrowno,PU_ArrivalVouch.ccode as carvcode,PU_ArrivalVouch.cauditdate as carvdate,
QMRejectVoucher.cWhCode as csourcewhcode,sourcewarehouse.cwhname as csourcewhname,PU_ArrivalVouchs.autoid as iarrsid,
QMRejectVouchers.cBWhCode as cWhCode,warehouse.cWhName as cwhname,purchasetype.cRdCode,cRdName,
QMRejectVouchers.cdimInvCode as cInvCode,cInvAddCode,Inventory.cInvName,Inventory.cInvStd,
QMRejectVouchers.Autoid,QMRejectVouchers.Id,SourceAutoID,pu_arrivalvouchs.icost as fCost,
((isnull(fdimquantity,0))*pu_arrivalvouchs.icost) as  fMOney,iInvRCost as fInvRCost,
(isnull(fdimQuantity,0)*iInvRCost) as fInvRMOney,inventory.cComUnitCode,
QMRejectVouchers.CdimUNITID as CUNITID,cItemClass,cItemCName,pu_arrivalvouch.cdepcode,
QMRejectVoucher.cItemCode,QMRejectVoucher.cItemName,ComputatiOnUnit.cComUnitName,
ComputatiOnUnit1.cComUnitName as CUNITNAME,QMRejectVoucher.checkid as icheckidbaks,
QMRejectVouchers.FDIMCHANGRATE AS FCHANGRATE,fDimQuantity as iQuantity,FDimnum as inum,
(Case isnull(Inventory.bInvBatch,0) when 0 Then null Else QMRejectVouchers.cdimBatch End)as cBatch,
(case  when (isnull(inventory.binvbatch,0)<>0 and isnull(inventory.cmassunit,N'')<>N'') then QMRejectVoucher.dProDate else null end) as dprodate,
(Case isnull(inventory.bInvQuality,0) when 0 Then null Else QMRejectVouchers.idimmassdate End) as imassdate,
(Case isnull(inventory.bInvQuality,0) when 0 Then null Else QMRejectVouchers.ddimVDate End) as dVDate,
QMRejectVouchers.iDIMExpiratDateCalcu as iExpiratDateCalcu,QMRejectVouchers.cDIMExpirationdate as cExpirationdate,QMRejectVouchers.dDIMExpirationdate as dExpirationdate,
(case when bservice=1 then N'是' else N'否' end) as bservice,
binvbatch,
CONVERT(char,COnvert(mOney,QMRejectVoucher.Ufts),2) as ufts,
inventory.cGroupCode,iGroupType,QMRejectVouchers.cdimmassunit as cmassunit,
0 as iordertype,cast('' as nvarchar(100)) as csocode,cast('' as nvarchar(50)) as iSoDID, cast(null as int) as isoseq,
QMRejectVoucher.IORDERDID,QMRejectVoucher.ISOORDERTYPE,PO_Pomain.cpoid as CORDERCODE,QMRejectVoucher.IORDERSEQ,
 pu_arrivalvouch.cvenpuomprotocol,aa_agreement.cname as cvenpuomprotocolname, QMRejectVoucher.CBATCHPROPERTY1,QMRejectVoucher.CBATCHPROPERTY2,QMRejectVoucher.CBATCHPROPERTY3,
QMRejectVoucher.CBATCHPROPERTY4,QMRejectVoucher.CBATCHPROPERTY5,QMRejectVoucher.CBATCHPROPERTY6,
QMRejectVoucher.CBATCHPROPERTY7,QMRejectVoucher.CBATCHPROPERTY8,QMRejectVoucher.CBATCHPROPERTY9,
QMRejectVoucher.CBATCHPROPERTY10,
inventory.iid,pu_arrivalvouchs.cciqbookcode,pu_arrivalvouch.iflowid as iflowid,pubizflow.cflowname as cflowname  From QMRejectVoucher Inner Join QMRejectVouchers On QMRejectVoucher.id = QMRejectVouchers.id Inner Join inventory On QMRejectVouchers.cdiminvcode = inventory.cinvcode Inner Join PU_ArrivalVouchs On QMRejectVoucher.SourceAutoid = PU_ArrivalVouchs.autoid Inner Join PU_ArrivalVouch On PU_ArrivalVouchs.id = PU_ArrivalVouch.id 
left join pubizflow on pubizflow.iflowid=pu_arrivalvouch.iflowid 
Left Join person On QMRejectVoucher.cCheckperson = person.cpersoncode 
Left Join PurchaseType On PU_ArrivalVouch.cptcode = PurchaseType.cptcode 
Left Join vEndor On QMRejectVoucher.cvencode = vEndor.cvencode 
Left Join department On QMRejectVoucher.cinspectdepcode = department.cdepcode 
Left Join person as person1 On PU_ArrivalVouch.cpersoncode = person1.cpersoncode 
Left Join warehouse On QMRejectVouchers.cbwhcode = warehouse.cwhcode 
Left Join Rd_Style On PurchaseType.crdcode = Rd_Style.crdcode 
Left Join ComputatiOnUnit On inventory.cComUnitCode = ComputatiOnUnit.cComUnitCode 
Left Join ComputatiOnUnit as ComputatiOnUnit1 On QMRejectVouchers.CdimUNITID  =  ComputatiOnUnit1.cComUnitCode 
left join aa_agreement on pu_arrivalvouch.cvenpuomprotocol = aa_agreement.ccode  
LEFT OUTER JOIN SO_SODETAILS ON  QMREJECTVOUCHER.IORDERTYPE =1 and cast(SO_SODETAILS.ISOSID as nvarchar(50))=QMRejectVoucher.ISOORDERAUTOID  
LEFT OUTER JOIN EX_ORDERDETAIL ON  QMREJECTVOUCHER.IORDERTYPE =3 and cast(EX_ORDERDETAIL.AUTOID as nvarchar(50))=QMRejectVoucher.ISOORDERAUTOID  
Left Join warehouse sourcewarehouse On QMRejectVoucher.cwhcode = sourcewarehouse.cwhcode 
left join po_podetails on po_podetails.id =pu_arrivalvouchs.iPOsID
left join PO_Pomain on PO_Pomain.poid=po_podetails.poid
Where isnull(QMRejectVoucher.cVerifier,N'')<>N'' and IDISPOSEFLOW = 2 and QMRejectVoucher.cvouchtype = N'QM05'  and isnull(QMRejectVouchers.bflag,0) = 0 and (isnull(fdimQuantity,0)>0) and isnull(pu_arrivalvouchs.CBCLOSER,'') = '' ) as aa  Where 1 = 1  and ( (1>0) AND (1>0) ) AND ( 1>0 ) and cinvcode='" + cinvcode + "' and cbatch='" + cbatch + "'";
            //and e.cwhcode='"+cwhcode+"' 
            int i;
            string connString = ConnectionString;
            DataSet ds = new DataSet();
            SIOrder = null;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                if (ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("没有该批号该编码需入库的单据");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                SIOrder = resDs;
                i = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;

                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return i;
        }
        #endregion

        #region 根据委外到货单生成入库单
        public static int CreateSIOrderByOSArriveOrder(string ArriveCode, string POCode, string cInvCode, int isReturn, string ConnectionString, out DataSet SIOrder, out string errMsg)
        {
            string strHead = "select distinct '" + ArriveCode + "' carvcode,'0' bredvouch,ibilltype,cCode,a.ddate,a.ddate as darvdate,cVenAbbName,a.cvencode,f.cVenAbbName as cvenname,a.cpersoncode,a.cMaker,a.cMemo,a.ufts,'' coufts,a.ID,a.iDiscountTaxType,a.cVenPUOMProtocol,a.iflowid,a.cbustype,a.cdepcode,e.cdepname,a.cptcode,d.cpoid as cordercode,a.ccode as carvcode,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a.cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a.id as ipurarriveid,a.itaxrate,a.iexchrate,a.cexch_name,a.ccode as carvcode "
            + "from pu_arrivalvouch a join pu_arrivalvouchs b on a.id=b.id join inventory i on i.cinvcode=b.cinvcode "
            + "left join PO_Podetails c on c.id=b.iposid left join po_pomain d on d.poid=c.poid "
            + "join department e on e.cdepcode=a.cdepcode join vendor f on f.cvencode=a.cvencode "
            + "where isnull(a.cverifier,'')<>'' and a.cptcode=N'02' and a.cbustype=N'委外加工' "
            + "and (isnull(b.iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(b.inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0)))";

            if (string.IsNullOrEmpty(ArriveCode))
                strHead.Replace("distinct", "top 1 ");
            else
            {
                if (ArriveCode.Length > 0)
                    strHead += " and a.ccode='" + ArriveCode + "'  ";
                if (POCode.Length > 0)
                    strHead += " and d.cpoid='" + POCode + "'  ";
                if (cInvCode.Length > 0)
                    strHead += " and b.cinvcode='" + cInvCode + "'  ";
            }


            string strBody = @"select distinct isnull(a.iquantity,0) orderquantity,(case a.bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case i.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,b.cptcode,iBillType,f.cinvccode,a.cbatchproperty6,i.cinvdefine1,i.cinvdefine2,i.cinvdefine4,a.cmassunit,a.imassdate,Convert(money,b.ufts) as corufts,a.cbatch,a.dpdate as dmadedate,a.dvdate,a.itaxprice,b.cauditdate as dbarvdate,a.sotype as isotype,b.ccode as cbarvcode,a.autoid,a.autoid as iarrsid,a.cordercode as cpoid,a.iposid,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37,a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cinvcode,a.icost as iunitcost,a.ioricost as facost,iorimoney as iprice,iorimoney as iaprice,iorimoney as imoney,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,
cast((case when abs(isnull(a.iquantity,0)-isnull(frefusequantity,0))>abs(isnull(fvalidinquan,0)) then isnull(a.iquantity,0)-isnull(frefusequantity,0)-isnull(fvalidinquan,0) else 0 end) as decimal(10,2)) as iNQuantity,i.cinvstd,i.cinvname,b.itaxrate,w.cwhname,w.cwhcode,i.cAddress

from pu_arrivalvouchs a join pu_arrivalvouch b on a.id=b.id left join warehouse w on w.cwhcode=a.cwhcode 
left join inventory i on i.cinvcode=a.cinvcode
left join computationunit c on i.ccomunitcode=c.ccomunitcode
left join InventoryClass f on f.cinvccode=i.cinvccode
left join po_podetails pds on pds.id=a.iposid 
left join po_pomain pm on pm.poid=pds.poid 

Where  isnull(b.cverifier,'')<>'' and b.cptcode=N'02' and b.cbustype=N'委外加工' and b.iBillType =0 
and (isnull(a.iQuantity,0)-isnull(fRefuseQuantity,0)>isnull(fValidInQuan,0) or (igrouptype=2 and isnull(a.inum,0)-isnull(frefusenum,0)>isnull(fvalidinnum,0)))";
            //通过后面的到货单主表ID条件查询
            if (string.IsNullOrEmpty(ArriveCode))
                strBody += " and 1=2";
            else
            {
                if (ArriveCode.Length > 0)
                    strBody += " and b.ccode='" + ArriveCode + "'  ";
                if (POCode.Length > 0)
                    strBody += " and pm.cpoid='" + POCode + "'  ";
                if (cInvCode.Length > 0)
                    strBody += " and a.cinvcode='" + cInvCode + "'  ";
            }

            int i;
            string connString = ConnectionString;
            DataSet ds = new DataSet();
            SIOrder = null;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                if (ds.Tables["head"].Rows.Count == 0)
                {
                    throw new Exception("单号不存在或未审核!");
                }

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                if (ds.Tables["body"].Rows.Count == 0)
                {
                    throw new Exception("单据已处理或数据有误!");
                }
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                SIOrder = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            i = 0;
            return i;
        }
        #endregion

        #region 根据委外订单生成入库单

        public static int CreateSIOrderByOMMOrder(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {
            int i;
            errMsg = "";
            OrderList = null;
            string strHead = null;
            string strBody = null;
            string connString = connectionString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            strHead = "select distinct irowno ivouchrowno,'' as selcol,irowno,csocode,sotype,ccode,ddate,cdepcode,cdepname,cvencode,cvenabbname,cpersoncode,cpersonname,cmaker,cverifier,cinvcode,cinvaddcode,cinvname,cinvstd,cinvdefine1,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine5,cinvdefine6,cinvdefine7,cinvdefine8,cinvdefine9,cinvdefine10,cinvdefine11,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,ccomunitcode,cinvm_unit,cunitid,cinva_unit,citem_class,citem_name,citemcode,citemname,dstartdate,darrivedate,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,(case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end ) as ireceivedqty,((isnull(iquantity,0)- (case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end))) as iqty,modetailsid,ufts FROM  om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 and isnull(isendqty,0)-isnull(iComplementQty,0)>0  ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) AND (1>0)  AND ( 1>0 ) and MoDetailsID in "
                + " (select distinct MoDetailsID FROM  om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 and isnull(isendqty,0)-isnull(iComplementQty,0)>0  ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) AND (1>0)  AND ( 1>0 ) )";
            if (!string.IsNullOrEmpty(ccode))
                strHead += " and  cCode = " + GetNull(ccode);
                
            strBody = "select distinct (case c.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,ivouchrowno,'' as selcol,a.cwhcode,cwhname,a.cinvcode,a.cinvaddcode,a.cinvname,a.cinvstd,a.cinvdefine1,a.cinvdefine2,a.cinvdefine3,a.cinvdefine4,a.cinvdefine5,a.cinvdefine6 caddress,a.cinvdefine7,a.cinvdefine8,a.cinvdefine9,a.cinvdefine10,a.cinvdefine11,a.cinvdefine12,a.cinvdefine13,a.cinvdefine14,a.cinvdefine15,a.cinvdefine16,a.ccomunitcode,cinvm_unit,"
                + "a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cbatch,a.drequireddate,a.iunitquantity,0 iquantity,((isnull(a.isendqty,0)-isnull(a.icomplementqty,0))) as isendqty,((isnull(a.iquantity,0)-isnull(a.isendqty,0)+isnull(a.iComplementQty,0))) as inquantity,(a.ftransqty) as ftransqty,c.cinvccode,c.cAddress, "
                + " ftransnum,a.modetailsid,a.momaterialsid,ufts,a.iwiptype,cassunit,cinva_unit,iinvexchrate,itnum,isnum,iunnum,a.icomplementqty,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37 "
                + " FROM  om_momaterialsbody a inner join om_mohead b on a.moid=b.moid join Inventory c on a.cinvcode=c.cInvCode join OM_MODetails d on a.modetailsid=d.MODetailsID "
                + " where isnull(a.iwiptype,3)=3 and (isnull(a.iquantity,0)-isnull(a.isendqty,0)+isnull(a.icomplementqty,0))>0  AND 1=1 and a.modetailsid in (select modetailsid from OM_MOMain a join om_mobody b on a.MOID=b.moid where cCode='" + ccode + "') order by ivouchrowno";

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                //if (ds.Tables["head"].Rows.Count == 0)
                //{
                //    throw new Exception("单号不存在!");
                //}

                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                //resDs = FillDs
                OrderList = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                i = 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

            i=0;
            return i;
        }

        #endregion

        #region 委外订单处理

        #region Head
        public static int GetOMMOHead(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {
            errMsg = "";
            OrderList = null;
            string strHead = null;
            string strBody = null;
            string strMoDetail=" (select distinct MoDetailsID FROM  om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) AND (1>0)  AND ( 1>0 ) )";
            string connString = connectionString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            strHead = " select distinct irowno ivouchrowno,cwhcode,cwhname,om_mohead.moid ipurorderid,'' as selcol,irowno,csocode,sotype,ccode,ddate,cdepcode,cdepname,cvencode,cvenabbname,cinvdefine1 cvenname,cpersoncode,cpersonname,cmaker,cverifier,cinvcode,cinvcode cpspcode,cinvaddcode,cinvname,cinvstd,cinvdefine6 caddress,cinvdefine1,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine5,cinvdefine6,cinvdefine7,cinvdefine8,cinvdefine9,cinvdefine10,cinvdefine11,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,ccomunitcode,cinvm_unit,cunitid,cinva_unit,citem_class,citem_name,citemcode,citemname,dstartdate,darrivedate,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,(case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end ) as ireceivedqty,((isnull(iquantity,0)- (case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end))) as iqty,modetailsid,ufts FROM  om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did,moid,cwhcode,cwhname  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) AND (1>0)  AND ( 1>0 ) and MoDetailsID in " + strMoDetail;
            if (!string.IsNullOrEmpty(ccode))
                strHead += " and  cCode = " + GetNull(ccode);

            strBody = "select distinct ccode,ddate,cdepcode,cdepname,cvencode,cvenname,cvenabbname,cpersoncode,cpersonname,cmaker cmakername,cinvcode,cinvname,cinvstd,cinvm_unit cinvunit,(case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end ) as inquantity,((isnull(iquantity,0)- (case when isnull(iarrqty,0)<> 0 then isnull(iarrqty,0) else isnull(ireceivedqty,0) end))) as iquantity,modetailsid,ufts FROM  om_mohead inner join om_mobody on om_mohead.moid=om_mobody.moid  INNER JOIN (SELECT DISTINCT MoDetailsID as did  from om_momaterialsbody WHERE 1=1 and isnull(iwiptype,3)=3  AND (isnull(iquantity,0)-isnull(isendqty,0)+isnull(iComplementQty,0)) >0 ) om_momaterialsbody ON om_mobody.modetailsid =  om_momaterialsbody.did where  (case when isnull(cchanger,N'') <> N'' then isnull(cchangeverifier ,N'') else isnull(cverifier,N'') end) <> N'' and isnull(cbcloser,N'')=N'' AND  (1>0) AND (1>0)  AND ( 1>0 ) and MoDetailsID in " + strMoDetail;
            if (!string.IsNullOrEmpty(ccode))
                strBody += " and  cCode = " + GetNull(ccode);
            strBody += " order by ccode";

            cmd.Connection = conn;
            cmd.CommandText = strHead;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            
            adp.SelectCommand = cmd;
            try
            {
                adp.Fill(ds.Tables["head"]);
                cmd.CommandText = strBody;
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                BuildMODetailStruct(resDs.Tables["body"]);
                resDs = FillDs(ds, resDs, true);
                OrderList = resDs;
                GetMoCost(ccode, connString, OrderList, out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return 0;
        }

        #endregion
        #region Body
        public static int GetOMMOBody(string strMoDetailID, string connectionString, out DataSet DetailList, out string errMsg)
        {
            errMsg = "";
            DetailList = null;
            //string strHead = null;
            string strBody = null;
            string connString = connectionString;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }

            //strHead = "exec ST_GetInventoryVO  N'"+invcode+"',N'',0";

            strBody = "select distinct (case bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case c.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,ivouchrowno irowno,cmassunit,imassdate,a.modetailsid iomodid,iPerTaxRate itaxrate,iNatTax iTaxPrice,iTaxPrice iOriTaxCost,iUnitPrice iUnitCost,iNatSum iSum,iTax iOriTaxPrice,iTax,a.moid ipurorderid,'' as selcol,a.cwhcode,cwhname,a.cinvcode,a.cinvaddcode,a.cinvname,a.cinvstd,a.cinvdefine1,a.cinvdefine2,a.cinvdefine3,a.cinvdefine4,a.cinvdefine5,a.cinvdefine6 caddress,a.cinvdefine7,a.cinvdefine8,a.cinvdefine9,a.cinvdefine10,a.cinvdefine11,a.cinvdefine12,a.cinvdefine13,a.cinvdefine14,a.cinvdefine15,a.cinvdefine16,a.ccomunitcode,cinvm_unit,a.cfree1,a.cfree2,a.cfree3,a.cfree4,a.cfree5,a.cfree6,a.cfree7,a.cfree8,a.cfree9,a.cfree10,a.cbatch,a.drequireddate,a.iunitquantity,0 iquantity,((isnull(a.isendqty,0)-isnull(a.icomplementqty,0))) as inquantity,((isnull(a.iquantity,0)-isnull(a.isendqty,0)+isnull(a.iComplementQty,0))) as inquantity,(a.ftransqty) as ftransqty,c.cinvccode,c.cAddress,ftransnum,a.modetailsid,a.momaterialsid iomomid,ufts corufts,a.iwiptype,cassunit,cinva_unit,iinvexchrate,itnum,isnum,iunnum,a.icomplementqty,a.cdefine22,a.cdefine23,a.cdefine24,a.cdefine25,a.cdefine26,a.cdefine27,a.cdefine28,a.cdefine29,a.cdefine30,a.cdefine31,a.cdefine32,a.cdefine33,a.cdefine34,a.cdefine35,a.cdefine36,a.cdefine37 FROM  om_momaterialsbody a inner join om_mohead b on a.moid=b.moid join Inventory c on a.cinvcode=c.cInvCode join OM_MODetails d on a.modetailsid=d.MODetailsID where isnull(a.iwiptype,3)=3 and (isnull(a.iquantity,0)-isnull(a.isendqty,0)+isnull(a.icomplementqty,0))>0  AND 1=1 and a.modetailsid in (" + math(strMoDetailID) + ")";

            cmd.Connection = conn;
            cmd.CommandText = strBody;
            DataTable dt1 = new DataTable("head");
            DataTable dt2 = new DataTable("body");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            adp.SelectCommand = cmd;
            try
            {
                adp.SelectCommand = cmd;
                adp.Fill(ds.Tables["body"]);
                DataSet resDs = new DataSet();
                resDs = BuildStockInStruct();
                resDs = FillDs(ds, resDs, true);
                DetailList = resDs;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return 0;
        }

        #endregion

        #endregion

        #region BuildStockInStruct
        /// <summary>
        /// 入库单表结构设计
        /// </summary>
        /// <returns></returns>
        public static DataSet BuildStockInStruct()
        {
            DataSet ds = new DataSet();

            DataTable dtHead = new DataTable("Head");
            DataTable dtBody = new DataTable("Body");


            ds.Tables.Add(dtHead);
            ds.Tables.Add(dtBody);

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
            ds.Tables["Head"].Columns.Add("cvouchtype");
            ds.Tables["Head"].Columns.Add("bwhpos");
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
            ds.Tables["Head"].Columns.Add("cvenname");
            ds.Tables["Head"].Columns.Add("bredvouch");
            ds.Tables["Head"].Columns.Add("invm_unit");
            //委外
            ds.Tables["Head"].Columns.Add("ivouchrowno");
            ds.Tables["Head"].Columns.Add("cinvname");
            ds.Tables["Head"].Columns.Add("iquantity");
            ds.Tables["Head"].Columns.Add("cinvm_unit");
            ds.Tables["Head"].Columns.Add("invcode");
            ds.Tables["Head"].Columns.Add("inquantity");
            ds.Tables["Head"].Columns.Add("invname");
            ds.Tables["Head"].Columns.Add("cPsPcode");  
            ds.Tables["Head"].Columns.Add("iMQuantity");
            ds.Tables["Head"].Columns.Add("iOMoMID");
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
            ds.Tables["Body"].Columns.Add("cscrapufts");
            ds.Tables["Body"].Columns.Add("csocode");
            ds.Tables["Body"].Columns.Add("cveninvcode");
            ds.Tables["Body"].Columns.Add("cveninvname");
            ds.Tables["Body"].Columns.Add("cvencode");
            ds.Tables["Body"].Columns.Add("cvenname");
            ds.Tables["Body"].Columns.Add("cvenabbname");
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
            ds.Tables["Body"].Columns.Add("iautoid");
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
            ds.Tables["Body"].Columns.Add("cordercode");
            ds.Tables["Body"].Columns.Add("dbarvdate");
            ds.Tables["Body"].Columns.Add("cbarvcode");
            ds.Tables["Body"].Columns.Add("cvouchtype");
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

            return ds;
        }
        /// <summary>
        /// 材料出库表头选择列表结构
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int  BuildMODetailStruct(DataTable dt)
        {
            try
            {
                #region 表体设计
                dt.Columns.Clear();
                dt.Columns.Add("MoDetailsID");
                dt.Columns.Add("cCode");
                dt.Columns.Add("dDate");
                dt.Columns.Add("cDepCode");
                dt.Columns.Add("cDepName");
                dt.Columns.Add("cVenCode");
                dt.Columns.Add("cVenName");
                dt.Columns.Add("cVenAbbName");
                dt.Columns.Add("cPersonCode");
                dt.Columns.Add("cPersonName");
                dt.Columns.Add("cMakerCode");
                dt.Columns.Add("cMakerName");
                dt.Columns.Add("cInvCode");
                dt.Columns.Add("cInvName");
                dt.Columns.Add("cInvSta");
                dt.Columns.Add("cInvUnit");
                dt.Columns.Add("iQuantity");
                dt.Columns.Add("iNQuantity");
                dt.Columns.Add("iRowNo");

                #endregion
            }
            catch 
            {
                return 1;
            }
            return 0;
        }
        #endregion

        #endregion

        #region FillDs
        /// <summary>
        /// 表结构填充
        /// </summary>
        /// <param name="sourceDs"></param>
        /// <param name="tagds"></param>
        /// <param name="blnIsArriveOrder"></param>
        /// <returns></returns>
        private static DataSet FillDs(DataSet sourceDs, DataSet tagds, bool blnIsArriveOrder)
        {
            DataRow dr;
            int iHeadColumns = sourceDs.Tables["Head"].Columns.Count;
            int iBodyColumns = sourceDs.Tables["Body"].Columns.Count;
            //表头
            for (int j = 0; j < sourceDs.Tables["Head"].Rows.Count; j++)
            {
                dr = tagds.Tables["Head"].NewRow();
                tagds.Tables["Head"].Rows.Add(dr);
                for (int i = 0; i < iHeadColumns; i++)
                {
                    if (tagds.Tables["Head"].Columns.IndexOf
                        (sourceDs.Tables["Head"].Columns[i].ColumnName) > 0)
                    {
                        if (sourceDs.Tables["Head"].Rows[j][i] == DBNull.Value)
                        {
                            tagds.Tables["Head"].Rows[j][sourceDs.Tables["Head"].Columns[i].ColumnName] = DBNull.Value;
                            continue;
                        }

                        tagds.Tables["Head"].Rows[j][sourceDs.Tables["Head"].Columns[i].ColumnName]
                            = sourceDs.Tables["Head"].Rows[j][i].ToString();
                    }
                }
            }
            //表体
            foreach (DataRow drr in sourceDs.Tables["Body"].Rows)
            {
                DataRow idr = tagds.Tables["Body"].NewRow();
                tagds.Tables["Body"].Rows.Add(idr);
                for (int i = 0; i < iBodyColumns; i++)
                {
                    if (tagds.Tables["Body"].Columns.IndexOf
                            (sourceDs.Tables["Body"].Columns[i].ColumnName) >= 0)
                    {
                        if (drr[i] == DBNull.Value)
                        {
                            idr[sourceDs.Tables["Body"].Columns[i].ColumnName] =
                                DBNull.Value;
                            continue;
                        }

                        idr[sourceDs.Tables["Body"].Columns[i].ColumnName]
                            = drr[i].ToString();
                    }
                }
            }
            return tagds;
        }
        #endregion

        #region GetPersonDH
        private static int GetPersonDH(string cCode, out string cPersonCode, out string cPersonName, string strConnection)
        {

            cPersonCode = "";
            cPersonName = "";
            DataSet ds = new DataSet();
            string errMsg = "";
            string strSql = @"Select PU_ArrivalVouch.cPersonCode,Person.cPersonName
                                        from PU_ArrivalVouch
                                        Inner Join Person On
	                                        Person.cPersonCode = PU_ArrivalVouch.cPersonCode
                                        where cCode = '" + cCode + "'";

            //int iResult = OperationSql.GetDataset(strSql, strConnection, out ds, out errMsg);
            //if (iResult == 0)
            //{
            //    cPersonCode = ds.Tables[0].Rows[0]["cPersonCode"].ToString();
            //    cPersonName = ds.Tables[0].Rows[0]["cPersonName"].ToString();
            //}
            //else
            //{
            //    return -1;
            //}
            //return 0;

            int flag = -1;
            try
            {
                ds = DBHelperSQL.Query(strConnection, strSql);
                cPersonCode = ds.Tables[0].Rows[0]["cPersonCode"].ToString();
                cPersonName = ds.Tables[0].Rows[0]["cPersonName"].ToString();
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        #endregion

        #region 采购入库单保存---by 检验单
        public static int SaveQMCheckOrder(DataSet ds, string sourceVoucher, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            string DataFile = DateTime.Now.ToString("yyyyMMddhhmmss");
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            int i;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string dd = null;
            string arrQty = null;//到货数量

            try
            {
                string cVouchName = "采购入库单";
                string cardnumber, vt_id;//单据类型编码 模板号
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                dd = ds.Tables[0].Rows[0]["ddate"].ToString();
                string cSeed = dd.Replace("-", "").Substring(0, 8);

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select ifatherid,ichildid from UFSystem..UA_Identity where cvouchtype='rd' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;

                id = id + 1;

                #region rdrecord
                sql = @"insert into rdrecord(id,brdflag,cvouchtype,cbustype,csource,cwhcode,ddate,ccode,cdepcode,cvencode,cordercode,carvcode,cmaker,bpufirst,darvdate,vt_id,bisstqc,ipurarriveid,itaxrate,iexchrate,cexch_name,idiscounttaxtype,iswfcontrolled,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,bcredit,bomfirst,dnmaketime,chandler,dnverifytime,crdcode,dveridate,cptcode,cpersoncode,controlresult) values (@ID,@brdflag,@cvouchtype,@cbustype,@csource,@cwhcode,@ddate,@ccode,@cdepcode,@cvencode,@cordercode,@carvcode,@cmaker,@bpufirst,@darvdate,@vt_id,@bisstqc,@ipurarriveid,@itaxrate,@iexchrate,@cexch_name,@idiscounttaxtype,@iswfcontrolled,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,0,0,@dnmaketime,@chandler,@dnverifytime,@crdcode,@dveridate,@cptcode,@cpersoncode,@controlresult)";
                sql = sql.Replace("@ID", id.ToString());
                sql = sql.Replace("@brdflag", SelSql(ds.Tables[0].Rows[0]["brdflag"].ToString()));
                sql = sql.Replace("@cvouchtype", SelSql(ds.Tables[0].Rows[0]["cvouchtype"].ToString()));
                sql = sql.Replace("@cbustype", SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString()));
                sql = sql.Replace("@csource", SelSql(ds.Tables[0].Rows[0]["csource"].ToString()));
                sql = sql.Replace("@cwhcode", SelSql(ds.Tables[0].Rows[0]["cwhcode"].ToString()));
                sql = sql.Replace("@ddate", SelSql(dd));
                sql = sql.Replace("@ccode", SelSql(id.ToString()));
                sql = sql.Replace("@cdepcode", GetNull(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cordercode", SelSql(ds.Tables[0].Rows[0]["cordercode"].ToString()));
                sql = sql.Replace("@carvcode", SelSql(ds.Tables[0].Rows[0]["carvcode"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bpufirst", SelSql(ds.Tables[0].Rows[0]["bpufirst"].ToString()));
                if (ds.Tables[0].Rows[0]["darvdate"].ToString() != "")
                    sql = sql.Replace("@darvdate", SelSql(Convert.ToDateTime(ds.Tables[0].Rows[0]["darvdate"]).ToString("yyyy-MM-dd")));
                else
                    sql = sql.Replace("@darvdate", GetNull(""));
                sql = sql.Replace("@vt_id", vt_id);
                sql = sql.Replace("@bisstqc", SelSql(ds.Tables[0].Rows[0]["bisstqc"].ToString()));
                sql = sql.Replace("@ipurarriveid", mathNULL(ds.Tables[0].Rows[0]["ipurarriveid"].ToString()));
                sql = sql.Replace("@itaxrate", SelSql(ds.Tables[0].Rows[0]["itaxrate"].ToString()));
                sql = sql.Replace("@iexchrate", SelSql(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@iswfcontrolled", SelSql(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", GetNull(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", GetNull(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull("barcode"));//条码单据标志
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", GetNull(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", math("7"));
                sql = sql.Replace("@dnmaketime", GetNull(ds.Tables[0].Rows[0]["dnmaketime"].ToString()));
                sql = sql.Replace("@chandler", GetNull(""));
                sql = sql.Replace("@dnverifytime", GetNull(""));
                sql = sql.Replace("@crdcode", SelSql(ds.Tables[0].Rows[0]["crdcode"].ToString()));
                sql = sql.Replace("@dveridate", GetNull(""));
                sql = sql.Replace("@cptcode", GetNull(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@controlresult", SelSql("-1"));

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                foreach (DataRow dr in ds.Tables["body"].Rows)
                {
                    autoid++;
                    if (sourceVoucher.Equals("01") || sourceVoucher.Equals("02") || sourceVoucher.Equals("03"))
                    {
                        sql = "update pu_arrivalvouchs set fvalidinquan=isnull(fvalidinquan,0)+" + dr["iquantity"].ToString() + " where autoid=@autoid"
                            + " and iquantity>=isnull(fvalidinquan,0)+" + dr["iquantity"].ToString() + "";
                        sql = sql.Replace("@autoid", dr["iarrsid"].ToString());
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            errMsg = "更新到货单失败!" + dr["cinvcode"].ToString();
                            myTran.Rollback();
                            return -1;
                        }
                        sql = "select iquantity from pu_arrivalvouchs where autoid='" + dr["iarrsid"] + "' ";
                        cmd.CommandText = sql;
                        arrQty = cmd.ExecuteScalar().ToString();

                    }
                    if (sourceVoucher == "01")
                    {
                        sql = "select isnull(fregquantity,0)+isnull(fconquantiy,0)-isnull(fregbreakquantity,0)-isnull(FsumQuantity,0) from qmcheckvoucher where id=" + dr["icheckidbaks"].ToString();
                        cmd.CommandText = sql;


                        decimal m_i = Convert.ToDecimal(cmd.ExecuteScalar());
                        if (m_i - Convert.ToDecimal(dr["iquantity"].ToString()) <= 0)//判断是否完成
                        {
                            sql = "update qmcheckvoucher set FsumQuantity=isnull(FsumQuantity,0)+'" + dr["iquantity"].ToString() + "',bpuinflag='1' where id=" + dr["icheckidbaks"].ToString();
                        }
                        else
                        {
                            sql = "update qmcheckvoucher set FsumQuantity=isnull(FsumQuantity,0)+'" + dr["iquantity"].ToString() + "' where id=" + dr["icheckidbaks"].ToString();
                        }
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            errMsg = "检验单信息无效!";
                            myTran.Rollback();
                            return -1;
                        }
                    }
                    #region rdrecords
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,inum,iquantity,iunitcost,iprice,iaprice,ipunitcost,ipprice,cbatch,cvouchcode,cfree1,cfree2,dsdate,itax,isquantity,isnum,imoney,isoutquantity,isoutnum,ifnum,ifquantity,dvdate,itrids,cposition,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,citem_class,citemcode,iposid,facost,cname,citemcname,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbarcode,inquantity,innum,cassunit,dmadedate,imassdate,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,impoids,icheckids,cbvencode,cinvouchcode,cgspstate,iarrsid,ccheckcode,icheckidbaks,crejectcode,irejectids,ccheckpersoncode,dcheckdate,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,itaxrate,itaxprice,isum,btaxcost,cpoid,cmassunit,imaterialfee,iprocesscost,iprocessfee,dmsdate,ismaterialfee,isprocessfee,iomodid,isodid,strcontractid,strcode,isotype,corufts,cbaccounter,bcosting,isumbillquantity,bvmiused,ivmisettlequantity,ivmisettlenum,cvmivencode,iinvsncount,impcost,iimosid,iimbsid,cbarvcode,dbarvdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,cbatchproperty6,iordertype)
	 Values (@autoid,@id,@cinvcode,@inum,@iquantity,@iunitcost,@iprice,@iaprice,@ipunitcost,@ipprice,@cbatch,@cvouchcode,@CFREE1,@cfree2,@dsdate,@ITAX,@isquantity,@isnum,@imoney,@isoutquantity,@isoutnum,@ifnum,@ifquantity,@dvdate,@itrids,@cposition,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@citem_class,@citemcode,@iposid,@facost,@cname,@citemcname,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbarcode,@inquantity,@innum,@cassunit,@dmadedate,@imassdate,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@impoids,@icheckids,@cbvencode,@cinvouchcode,@cgspstate,@iarrsid,@ccheckcode,@icheckidbaks,@crejectcode,@irejectids,@ccheckpersoncode,@dcheckdate,@ioritaxcost,@ioricost,@iorimoney,@ioritaxprice,@iorisum,@itaxrate,@itaxprice,@ISUM,@btaxcost,@cpoid,@cmassunit,@imaterialfee,@iprocesscost,@iprocessfee,@dmsdate,@ismaterialfee,@isprocessfee,@iomodid,@isodid,@strcontractid,@strcode,@isotype,@corufts,@cbaccounter,@bcosting,@isumbillquantity,@bvmiused,@ivmisettlequantity,@ivmisettlenum,@cvmivencode,@iinvsncount,@impcost,@iimosid,@iimbsid,@cbarvcode,@dbarvdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@Cbatchproperty6,@iordertype)";
                    #region Replace
                    sql = sql.Replace("@autoid", autoid.ToString());
                    sql = sql.Replace("@id", id.ToString());
                    sql = sql.Replace("@cinvcode", SelSql(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", mathNULL(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@iunitcost", math(dr["iunitcost"].ToString()));
                    sql = sql.Replace("@iprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@iaprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@ipunitcost", math(dr["ipunitcost"].ToString()));
                    sql = sql.Replace("@ipprice", math(dr["ipprice"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@cvouchcode", GetNull(dr["cvouchcode"].ToString()));
                    sql = sql.Replace("@CFREE1", GetNull(dr["cfree1"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree2"].ToString()));
                    sql = sql.Replace("@dsdate", GetNull(dr["dsdate"].ToString()));
                    sql = sql.Replace("@ITAX", math(dr["ITAX"].ToString()));
                    sql = sql.Replace("@isquantity", SelSql(dr["isquantity"].ToString()));
                    sql = sql.Replace("@isnum", SelSql(dr["isnum"].ToString()));
                    sql = sql.Replace("@imoney", math("0"));
                    sql = sql.Replace("@isoutquantity", math(dr["isoutquantity"].ToString()));
                    sql = sql.Replace("@isoutnum", math(dr["isoutnum"].ToString()));
                    sql = sql.Replace("@ifnum", math(dr["ifnum"].ToString()));
                    sql = sql.Replace("@ifquantity", math(dr["ifquantity"].ToString()));
                    sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@itrids", math(dr["itrids"].ToString()));
                    sql = sql.Replace("@cposition", GetNull(dr["cposition"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["cdefine22"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", math(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", math(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", math(dr["iposid"].ToString()));
                    sql = sql.Replace("@facost", math(dr["facost"].ToString()));
                    sql = sql.Replace("@cname", GetNull(dr["cname"].ToString()));
                    sql = sql.Replace("@citemcname", GetNull(dr["citemcname"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@cbarcode", GetNull(dr["cbarcode"].ToString()));
                    sql = sql.Replace("@inquantity", math(arrQty));
                    sql = sql.Replace("@innum", math(dr["innum"].ToString()));
                    sql = sql.Replace("@cassunit", GetNull(dr["cassunit"].ToString()));
                    sql = sql.Replace("@dmadedate", GetNull(dr["dmadedate"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", math(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", math(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", GetNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", GetNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@impoids", math(dr["impoids"].ToString()));
                    sql = sql.Replace("@icheckids", mathNULL(dr["icheckids"].ToString()));
                    sql = sql.Replace("@cbvencode", GetNull(dr["cbvencode"].ToString()));
                    sql = sql.Replace("@cinvouchcode", GetNull(dr["cinvouchcode"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));
                    sql = sql.Replace("@iarrsid", math(dr["iarrsid"].ToString()));
                    sql = sql.Replace("@ccheckcode", SelSql(dr["ccheckcode"].ToString()));
                    sql = sql.Replace("@icheckidbaks", mathNULL(dr["icheckidbaks"].ToString()));
                    sql = sql.Replace("@crejectcode", GetNull(dr["crejectcode"].ToString()));
                    sql = sql.Replace("@irejectids", mathNULL(dr["irejectids"].ToString()));
                    sql = sql.Replace("@ccheckpersoncode", GetNull(dr["ccheckpersoncode"].ToString()));
                    sql = sql.Replace("@dcheckdate", GetNull(dr["dcheckdate"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@iorimoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@itaxrate", math(dr["itaxrate"].ToString()));
                    sql = sql.Replace("@itaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@ISUM", math(dr["isum"].ToString()));//本币价税合计
                    sql = sql.Replace("@btaxcost", math(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@cpoid", SelSql(dr["cpoid"].ToString()));
                    sql = sql.Replace("@cmassunit", math(dr["cmassunit"].ToString()));
                    sql = sql.Replace("@imaterialfee", math(dr["imaterialfee"].ToString()));
                    sql = sql.Replace("@iprocesscost", math(dr["iprocesscost"].ToString()));
                    sql = sql.Replace("@iprocessfee", math(dr["iprocessfee"].ToString()));
                    sql = sql.Replace("@dmsdate", GetNull(dr["dmsdate"].ToString()));
                    sql = sql.Replace("@ismaterialfee", math(dr["ismaterialfee"].ToString()));
                    sql = sql.Replace("@isprocessfee", math(dr["isprocessfee"].ToString()));
                    sql = sql.Replace("@iomodid", math(dr["iomodid"].ToString()));
                    sql = sql.Replace("@isodid", math(dr["isodid"].ToString()));
                    sql = sql.Replace("@strcontractid", GetNull(dr["strcontractid"].ToString()));
                    sql = sql.Replace("@strcode", GetNull(dr["strcode"].ToString()));
                    sql = sql.Replace("@isotype", mathNULL(dr["isotype"].ToString()));
                    sql = sql.Replace("@corufts", SelSql(dr["corufts"].ToString().Trim()));
                    sql = sql.Replace("@cbaccounter", GetNull(dr["cbaccounter"].ToString()));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@isumbillquantity", math(dr["isumbillquantity"].ToString()));
                    sql = sql.Replace("@bvmiused", math(dr["bvmiused"].ToString()));
                    sql = sql.Replace("@ivmisettlequantity", math(dr["ivmisettlequantity"].ToString()));
                    sql = sql.Replace("@ivmisettlenum", math(dr["ivmisettlenum"].ToString()));
                    sql = sql.Replace("@cvmivencode", GetNull(dr["cvmivencode"].ToString()));
                    sql = sql.Replace("@iinvsncount", math(dr["iinvsncount"].ToString()));
                    sql = sql.Replace("@impcost", math(dr["impcost"].ToString()));
                    sql = sql.Replace("@iimosid", math(dr["iimosid"].ToString()));
                    sql = sql.Replace("@iimbsid", math(dr["iimbsid"].ToString()));
                    sql = sql.Replace("@dbarvdate", GetNull(dr["dbarvdate"].ToString()));
                    sql = sql.Replace("@cbarvcode", GetNull(dr["cbarvcode"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", GetNull(dr["dexpirationdate"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", math(dr["iexpiratdatecalcu"].ToString()));
                    //sql = sql.Replace("@irowno", math(dr["irowno"].ToString()));
                    sql = sql.Replace("@Cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    sql = sql.Replace("@iordertype", mathNULL("0"));

                    #endregion
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion


                    //" + GetNull(dr["cPosition"].ToString()) + "
                    //记录货位
                    //                    sql = @"Insert Into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,dvdate,iquantity,inum,cmemo,chandler,
                    //                            ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,cbvencode,itrackid,dmadedate,
                    //                            imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)
                    //                            Values ('" + autoid.ToString() + "'," + id + "," + SelSql(ds.Tables[0].Rows[0]["cwhcode"].ToString()) + ",'" + (dr["cPosition"].ToString()) + "'," + SelSql(dr["cinvcode"].ToString()) +
                    //                            @",Null,Null,Null,Null," + float.Parse(dr["iquantity"].ToString()) + @"," + float.Parse(dr["inum"].ToString()) + @",Null," + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()) + ",N'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + SelSql(ds.Tables[0].Rows[0]["brdflag"].ToString()) + @",Null,Null,
                    //                            Null,Null,Null,Null,Null,Null,Null," + GetNull(dr["cassunit"].ToString()) + @",Null,Null,Null,Null,Null,Null,'" + math(dr["iexpiratdatecalcu"].ToString()) + @"',Null,Null)
                    //                            ";
                    //                    cmd.CommandText = sql;
                    //                    if (cmd.ExecuteNonQuery() < 1)
                    //                    {
                    //                        errMsg = "货位保存失败!";
                    //                        myTran.Rollback();
                    //                        return -1;
                    //                    }



                    #region CurrentStock
                    string selitemid = @"select isnull(max(id),0) from SCM_Item where cinvcode=@cinvcode";
                    selitemid = selitemid.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                    cmd.CommandText = selitemid;
                    string ItemID = cmd.ExecuteScalar().ToString();//CurrentStock的itemid同时也是SCM_Item的id
                    if (ItemID == "0")
                    {

                        sql = @"insert into SCM_Item (cinvcode,Cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) values (@cinvcode,@Cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10)";
                        //sql = sql.Replace("@id", GetNull(ItemID));
                        sql = sql.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                        sql = sql.Replace("@Cfree1", SelSql(dr["cfree1"].ToString()));
                        sql = sql.Replace("@cfree2", SelSql(dr["cfree2"].ToString()));
                        sql = sql.Replace("@cfree3", SelSql(dr["cfree3"].ToString()));
                        sql = sql.Replace("@cfree4", SelSql(dr["cfree4"].ToString()));
                        sql = sql.Replace("@cfree5", SelSql(dr["cfree5"].ToString()));
                        sql = sql.Replace("@cfree6", SelSql(dr["cfree6"].ToString()));
                        sql = sql.Replace("@cfree7", SelSql(dr["cfree7"].ToString()));
                        sql = sql.Replace("@cfree8", SelSql(dr["cfree8"].ToString()));
                        sql = sql.Replace("@cfree9", SelSql(dr["cfree9"].ToString()));
                        sql = sql.Replace("@cfree10", SelSql(dr["cfree9"].ToString()));
                        cmd.CommandText = sql;//增加新的scm_item
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select isnull(max(id),0) from SCM_Item";
                        ItemID = cmd.ExecuteScalar().ToString();

                        sql = "insert into CurrentStock (cwhcode,itemid,cinvcode,iquantity,finnum,Cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbatch,imassdate,cmassunit,dvdate,dmdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,isodid) values (@cwhcode,@itemid,@cinvcode,@iquantity,@inum,@Cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbatch,@imassdate,@cmassunit,@dvdate,@dmdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@isodid)";//插入库存表
                        sql = sql.Replace("@Cfree1", SelSql(dr["cfree1"].ToString()));
                        sql = sql.Replace("@cfree2", SelSql(dr["cfree2"].ToString()));
                        sql = sql.Replace("@cfree3", SelSql(dr["cfree3"].ToString()));
                        sql = sql.Replace("@cfree4", SelSql(dr["cfree4"].ToString()));
                        sql = sql.Replace("@cfree5", SelSql(dr["cfree5"].ToString()));
                        sql = sql.Replace("@cfree6", SelSql(dr["cfree6"].ToString()));
                        sql = sql.Replace("@cfree7", SelSql(dr["cfree7"].ToString()));
                        sql = sql.Replace("@cfree8", SelSql(dr["cfree8"].ToString()));
                        sql = sql.Replace("@cfree9", SelSql(dr["cfree9"].ToString()));
                        sql = sql.Replace("@cfree10", SelSql(dr["cfree9"].ToString()));
                        sql = sql.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                        sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                        sql = sql.Replace("@cbatch", SelSql(dr["cbatch"].ToString()));
                        sql = sql.Replace("@inum", "0");
                        sql = sql.Replace("@itemid", GetNull(ItemID));
                        sql = sql.Replace("@cwhcode", GetNull(ds.Tables["head"].Rows[0]["cwhcode"].ToString()));
                        sql = sql.Replace("@imassdate", GetNull(dr["imassdate"].ToString()));
                        sql = sql.Replace("@cmassunit", GetNull(dr["cmassunit"].ToString()));
                        sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                        sql = sql.Replace("@dmdate", GetNull(dr["dmadedate"].ToString()));
                        sql = sql.Replace("@cexpirationdate", SelSql(dr["cexpirationdate"].ToString()));
                        sql = sql.Replace("@dexpirationdate", SelSql(dr["dexpirationdate"].ToString()));
                        sql = sql.Replace("@iexpiratdatecalcu", math(dr["iexpiratdatecalcu"].ToString()));
                        sql = sql.Replace("@isodid", SelSql(dr["isodid"].ToString()));
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = "select isnull(max(autoid),0) from currentstock where itemid=@itemid and cwhcode=@cwhcode ";
                        sql = sql.Replace("@cwhcode", GetNull(ds.Tables["head"].Rows[0]["cwhcode"].ToString()));
                        sql = sql.Replace("@itemid", GetNull(ItemID));
                        if (dr["cbatch"].ToString() != "")
                            sql += " and cbatch='" + dr["cbatch"] + "'";
                        cmd.CommandText = sql;
                        string cs_autoid = cmd.ExecuteScalar().ToString();

                        if (cs_autoid != "0")
                        {
                            sql = "update currentstock set iquantity=iquantity+@iquantity where autoid=@autoid";
                            sql = sql.Replace("@iquantity", dr["iquantity"].ToString());
                            sql = sql.Replace("@inum", "0");
                            sql = sql.Replace("@autoid", cs_autoid);
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "select isnull(max(autoid),0) from currentstock";
                            cs_autoid = (Convert.ToInt32(cmd.ExecuteScalar()) + 1).ToString();

                            sql = "insert into CurrentStock (cwhcode,itemid,cinvcode,iquantity,finnum,Cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbatch,imassdate,cmassunit,dvdate,dmdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,isodid) values (@cwhcode,@itemid,@cinvcode,@iquantity,@inum,@Cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbatch,@imassdate,@cmassunit,@dvdate,@dmdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@isodid)";//插入库存表
                            sql = sql.Replace("@Cfree1", SelSql(dr["cfree1"].ToString()));
                            sql = sql.Replace("@cfree2", SelSql(dr["cfree2"].ToString()));
                            sql = sql.Replace("@cfree3", SelSql(dr["cfree3"].ToString()));
                            sql = sql.Replace("@cfree4", SelSql(dr["cfree4"].ToString()));
                            sql = sql.Replace("@cfree5", SelSql(dr["cfree5"].ToString()));
                            sql = sql.Replace("@cfree6", SelSql(dr["cfree6"].ToString()));
                            sql = sql.Replace("@cfree7", SelSql(dr["cfree7"].ToString()));
                            sql = sql.Replace("@cfree8", SelSql(dr["cfree8"].ToString()));
                            sql = sql.Replace("@cfree9", SelSql(dr["cfree9"].ToString()));
                            sql = sql.Replace("@cfree10", SelSql(dr["cfree10"].ToString()));
                            sql = sql.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                            sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                            sql = sql.Replace("@cbatch", SelSql(dr["cbatch"].ToString()));
                            sql = sql.Replace("@inum", "0");
                            sql = sql.Replace("@itemid", GetNull(ItemID));
                            sql = sql.Replace("@cwhcode", GetNull(ds.Tables["head"].Rows[0]["cwhcode"].ToString()));
                            sql = sql.Replace("@imassdate", GetNull(dr["imassdate"].ToString()));
                            sql = sql.Replace("@cmassunit", GetNull(dr["cmassunit"].ToString()));
                            sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                            sql = sql.Replace("@dmdate", GetNull(dr["dmadedate"].ToString()));
                            sql = sql.Replace("@cexpirationdate", SelSql(dr["cexpirationdate"].ToString()));
                            sql = sql.Replace("@dexpirationdate", SelSql(dr["dexpirationdate"].ToString()));
                            sql = sql.Replace("@iexpiratdatecalcu", math(dr["iexpiratdatecalcu"].ToString()));
                            sql = sql.Replace("@isodid", SelSql(dr["isodid"].ToString()));
                            sql = sql.Replace(" 上午12:00", "");
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    #endregion

                }

                //修改UA_Identity
                string sqlUpidentity = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + " where cvouchtype='rd' and cAcc_id=" + accid;
                cmd.CommandText = sqlUpidentity;
                cmd.ExecuteNonQuery();

                #region 单据号处理
                cmd.CommandText = "select cnumber from voucherhistory with (updLOCK) where cardnumber='" + cardnumber + "'  and cContent='日期' and cSeed='" + cSeed + "'";
                ocode = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();//获得ccode的流水号
                if (ocode == null)
                {
                    icode = 1;
                    sql = " Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','日期','月','" + cSeed + "','1')";
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = "update voucherhistory set cnumber=" + icode.ToString() + " where cardnumber='" + cardnumber + "'  and cContent='日期' and cSeed='" + cSeed + "'";
                }
                ccode = icode.ToString();
                ccode = ccode.PadLeft(4, '0');
                ccode = cSeed + ccode;//年年月月+4位流水号

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                sql = "Update RdRecord Set cCode = N'" + ccode + "' Where Id = " + id + "";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update rdrecord ccode error!";
                    return -1;
                }
                #endregion

                #region 监管码操作
                ///日期：2012－12－14
                ///人员：tianzhenyun
                ///功能：更新监管码数据表

                //判断监管码是否为空，如果为空则不操作，否则修改监管码
                string cdefine10 = ds.Tables[0].Rows[0]["cdefine10"].ToString();
                if (!string.IsNullOrEmpty(cdefine10))
                {
                    Model.Regulatory data = new Model.Regulatory();
                    data.RegCode = cdefine10;
                    data.CardNumber = cardnumber;
                    data.CardName = cVouchName;
                    data.CardCode = ccode;
                    //账套号
                    data.AccID = accid;

                    bool flag = Regulatory.UpdateRegulatory(connectionString, data, out errMsg);
                    if (!flag)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }
                #endregion

                myTran.Commit();
                i = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                i = -1;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return i;
        }
        #endregion

        #region 采购入库单保存---by 到货单
        public static int SaveByArrival(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            //add
            string sql = null;
            string spid = null;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string user = null;
            string cSeed = null;
            int trans = 0;
            int iRowNo = 0;
            string username = null;
            string checkCode = ds.Tables[0].Rows[0]["cchkcode"] != null ? ds.Tables[0].Rows[0]["cchkcode"].ToString() : "";
            string bredvouch = math(ds.Tables[0].Rows[0]["bredvouch"].ToString());
            string packName = "NULL";

            try
            {
                #region 处理参数

                #region 单据日期/时间
                string dd = null, dt = null;//单据日期、时间

                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                #endregion

                string ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ddate"]).ToString("yyyyMMdd");
                string cVouchName = "采购入库单";
                string cardnumber, vt_id;//单据类型编码 模板号
                sql = "select cUser_id from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                cmd.CommandText = sql;
                username = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString();
                string tempSeed = username + ddate;
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select max(ifatherid) ifatherid,max(ichildid) ichildid from UFSystem..UA_Identity where cvouchtype='rd' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;

                /*
                cmd.CommandText = "select isnull(max(convert(decimal,cnumber)),0) from voucherhistory where cardnumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed = " + GetNull(tempSeed);
                //cmd.CommandText = "select isnull(max(convert(decimal,right(ccode,4))),0) from rdrecord where substring(substring(ccode,4,13),1,8)=" + SelSql(ddate) + " and cbustype=" + SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString());
                ocode = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();//获得ccode的流水号

                if (ocode == null)
                    icode = 1;
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString();
                // ccode = ccode.PadLeft(4, '0');
                // ccode = cSeed.Substring(2, 6) + ccode;//年年月月+4位流水号
                // ccode = ds.Tables[0].Rows[0]["cwhcode"].ToString() + ccode;

                 * 
                 */
 
                id = id + 1;
                if (id < 10000000)
                    id += 860000000;    //暂定
                if (autoid < 10000000)
                    autoid += 860000000;    //暂定
                object isnull = null;
                do
                {
                    id = id + 1;
                    cmd.CommandText = "select id from RdRecord where id =" + id;
                    isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                }
                while (isnull != null);

                string month = "";
                if (DateTime.Now.Month < 10)
                {
                    month = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    month = DateTime.Now.Month.ToString();
                }
                #endregion

                #region rdrecord
                sql = @"insert into rdrecord(bredvouch,id,brdflag,cvouchtype,cbustype,csource,cwhcode,ddate,ccode,cdepcode,cvencode,cordercode,carvcode,cmaker,bpufirst,darvdate,vt_id,bisstqc,ipurarriveid,itaxrate,iexchrate,cexch_name,idiscounttaxtype,iswfcontrolled,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,bcredit,bomfirst,dnmaketime,chandler,dnverifytime,crdcode,dveridate,cptcode,cpersoncode,controlresult,cbusCode,cchkCode,dchkDate,cchkPerson,iProOrderId) values (@bredvouch,@ID,@brdflag,@cvouchtype,@cbustype,@csource,@cwhcode,@ddate,@ccode,@cdepcode,@cvencode,@cordercode,@carvcode,@cmaker,@bpufirst,@darvdate,@vt_id,@bisstqc,@ipurarriveid,@itaxrate,@iexchrate,@cexch_name,@idiscounttaxtype,@iswfcontrolled,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,0,0,@dnmaketime,@chandler,@dnverifytime,@crdcode,@dveridate,@cptcode,@cpersoncode,@controlresult,@cbusCode,@cchkCode,@dchkDate,@cchkPerson,@iProOrderId)";
                #region SQL_Replace
                sql = sql.Replace("@ID", id.ToString());
                sql = sql.Replace("@bredvouch", bredvouch);
                sql = sql.Replace("@brdflag", SelSql(ds.Tables[0].Rows[0]["brdflag"].ToString()));
                sql = sql.Replace("@cvouchtype", SelSql(ds.Tables[0].Rows[0]["cvouchtype"].ToString()));
                sql = sql.Replace("@cbustype", SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString()));
                sql = sql.Replace("@csource", SelSql(ds.Tables[0].Rows[0]["csource"].ToString()));
                sql = sql.Replace("@cwhcode", SelSql(ds.Tables[0].Rows[0]["cwhcode"].ToString()));
                sql = sql.Replace("@ddate", SelSql(dd));
                sql = sql.Replace("@ccode", id.ToString());//SelSql(username + ddate + ccode));
                sql = sql.Replace("@cdepcode", GetNull(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cordercode", SelSql(ds.Tables[0].Rows[0]["cordercode"].ToString()));
                sql = sql.Replace("@carvcode", SelSql(ds.Tables[0].Rows[0]["carvcode"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bpufirst", SelSql(ds.Tables[0].Rows[0]["bpufirst"].ToString()));
                sql = sql.Replace("@darvdate", dateNull(ds.Tables[0].Rows[0]["darvdate"].ToString()));
                sql = sql.Replace("@vt_id", vt_id);
                sql = sql.Replace("@bisstqc", SelSql(ds.Tables[0].Rows[0]["bisstqc"].ToString()));
                sql = sql.Replace("@ipurarriveid", mathNULL(ds.Tables[0].Rows[0]["ipurarriveid"].ToString()));
                sql = sql.Replace("@itaxrate", SelSql(ds.Tables[0].Rows[0]["itaxrate"].ToString()));
                sql = sql.Replace("@iexchrate", SelSql(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@iswfcontrolled", SelSql(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", GetNull(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", GetNull(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull(ds.Tables[0].Rows[0]["cdefine9"].ToString()));
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine11"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", GetNull(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", GetNull(ds.Tables[0].Rows[0]["cdefine16"].ToString()));
                sql = sql.Replace("@dnmaketime", GetNull(dt));
                sql = sql.Replace("@chandler", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));//审核人
                sql = sql.Replace("@dnverifytime", GetNull(dt));//审核时间
                sql = sql.Replace("@dveridate", GetNull(dd));//审核日期
                sql = sql.Replace("@crdcode", SelSql(ds.Tables[0].Rows[0]["crdcode"].ToString()));
                sql = sql.Replace("@cptcode", GetNull(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@controlresult", SelSql("-1"));
                sql = sql.Replace("@cbusCode", GetNull(ds.Tables[0].Rows[0]["cbuscode"].ToString()));
                sql = sql.Replace("@cchkCode", GetNull(ds.Tables[0].Rows[0]["cchkcode"].ToString()));
                sql = sql.Replace("@dchkDate", GetNull(ds.Tables[0].Rows[0]["dchkdate"].ToString())); 
                sql = sql.Replace("@cchkPerson", GetNull(ds.Tables[0].Rows[0]["cchkperson"].ToString()));
                sql = sql.Replace("@iProOrderId", mathNULL(ds.Tables[0].Rows[0]["iProOrderId"].ToString()));
                
                #endregion
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                #region temp table

                //delete temp
                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackTbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackSumTbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null )  drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null )  Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null )  Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //LPWriteBackTbl
                sql = "select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //Ufida_WBBuffers
                sql = "select a.id,autoid ,convert(decimal(30,2),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, iPrice as Imoney ,a.Cinvcode,Corufts ,iArrsID,iPOsID,iOMoDID,iIMBSID,iIMOSID,isotype,iSoDid,iRejectIds,iCheckIdBaks ,csource,cbustype,iCheckIds, convert(smallint,0) as iOperate "
                    +"into #Ufida_WBBuffers from rdrecords a inner join rdrecord b on a.id=b.id where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update rdrecords set corufts ='' where id=" + id.ToString();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //GSP
                sql = "if exists (select 1 where not object_id('tempdb..#GSP') is null ) Drop table #GSP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#GSPUFTS') is null ) Drop table #GSPUFTS";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select convert(money,ufts) as gspufts,convert(int,Null) as iCheckIds into #GSPUFTS from rdrecord where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion 

                foreach (DataRow dr in ds.Tables["body"].Rows)
                {
                    isnull = null;
                    do
                    {
                        autoid = autoid + 1;
                        cmd.CommandText = "select autoid from RdRecords where autoid =" + autoid;
                        isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                    }
                    while (isnull != null);
                    iRowNo++;
                    
                    #region rdrecords
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,inum,iquantity,iunitcost,iprice,iaprice,ipunitcost,ipprice,cbatch,cvouchcode,cfree1,cfree2,dsdate,itax,isquantity,isnum,imoney,isoutquantity,isoutnum,ifnum,ifquantity,dvdate,itrids,cposition,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,citem_class,citemcode,iposid,facost,cname,citemcname,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbarcode,inquantity,innum,cassunit,dmadedate,imassdate,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,impoids,icheckids,cbvencode,cinvouchcode,cgspstate,iarrsid,ccheckcode,icheckidbaks,crejectcode,irejectids,ccheckpersoncode,dcheckdate,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,itaxrate,itaxprice,isum,btaxcost,cpoid,cmassunit,imaterialfee,iprocesscost,iprocessfee,dmsdate,ismaterialfee,isprocessfee,iomodid,isodid,strcontractid,strcode,isotype,corufts,cbaccounter,bcosting,isumbillquantity,bvmiused,ivmisettlequantity,ivmisettlenum,cvmivencode,iinvsncount,impcost,iimosid,iimbsid,cbarvcode,dbarvdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,cbatchproperty6,iordertype,bGsp,iRowNo,iordercode)
	 Values (@autoid,@id,@cinvcode,@inum,@iquantity,@iunitcost,@iprice,@iaprice,@ipunitcost,@ipprice,@cbatch,@cvouchcode,@CFREE1,@cfree2,@dsdate,@ITAX,@isquantity,@isnum,@imoney,@isoutquantity,@isoutnum,@ifnum,@ifquantity,@dvdate,@itrids,@cposition,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@citem_class,@citemcode,@iposid,@facost,@cname,@citemcname,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbarcode,@inquantity,@innum,@cassunit,@dmadedate,@imassdate,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@impoids,@icheckids,@cbvencode,@cinvouchcode,@cgspstate,@iarrsid,@ccheckcode,@icheckidbaks,@crejectcode,@irejectids,@ccheckpersoncode,@dcheckdate,@ioritaxcost,@ioricost,@iorimoney,@ioritaxprice,@iorisum,@itaxrate,@itaxprice,@ISUM,@btaxcost,@cpoid,@cmassunit,@imaterialfee,@iprocesscost,@iprocessfee,@dmsdate,@ismaterialfee,@isprocessfee,@iomodid,@isodid,@strcontractid,@strcode,@isotype,@corufts,@cbaccounter,@bcosting,@isumbillquantity,@bvmiused,@ivmisettlequantity,@ivmisettlenum,@cvmivencode,@iinvsncount,@impcost,@iimosid,@iimbsid,@cbarvcode,@dbarvdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@Cbatchproperty6,@iordertype,@bGsp,@iRowNo,@iordercode)";
                    #region SQL_Replace
                    sql = sql.Replace("@autoid", autoid.ToString());
                    sql = sql.Replace("@id", id.ToString());
                    sql = sql.Replace("@cinvcode", SelSql(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", mathNULL(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@iunitcost", math(dr["iunitcost"].ToString()));
                    sql = sql.Replace("@iprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@iaprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@ipunitcost", math(dr["ipunitcost"].ToString()));
                    sql = sql.Replace("@ipprice", math(dr["ipprice"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@cvouchcode", GetNull(dr["cvouchcode"].ToString()));
                    sql = sql.Replace("@dsdate", GetNull(dr["dsdate"].ToString()));
                    sql = sql.Replace("@ITAX", math(dr["ITAX"].ToString()));
                    sql = sql.Replace("@isquantity", SelSql(dr["isquantity"].ToString()));
                    sql = sql.Replace("@isnum", SelSql(dr["isnum"].ToString()));
                    sql = sql.Replace("@imoney", math("0"));
                    sql = sql.Replace("@isoutquantity", math(dr["isoutquantity"].ToString()));
                    sql = sql.Replace("@isoutnum", math(dr["isoutnum"].ToString()));
                    sql = sql.Replace("@ifnum", math(dr["ifnum"].ToString()));
                    sql = sql.Replace("@ifquantity", math(dr["ifquantity"].ToString()));
                    sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@itrids", math(dr["itrids"].ToString()));
                    sql = sql.Replace("@cposition", GetNull(dr["cposition"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["cdefine22"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", math(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", math(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", mathNULL(dr["iposid"].ToString()));  //采购订单子表标识 
                    sql = sql.Replace("@facost", mathNULL(dr["facost"].ToString()));  //暂估单价
                    sql = sql.Replace("@cname", GetNull(dr["cname"].ToString()));
                    sql = sql.Replace("@citemcname", GetNull(dr["citemcname"].ToString()));
                    sql = sql.Replace("@CFREE1", GetNull(dr["cfree1"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree2"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@cbarcode", GetNull(dr["cbarcode"].ToString()));
                    sql = sql.Replace("@inquantity", math(dr["OrderQuantity"].ToString()));
                    sql = sql.Replace("@innum", math(dr["innum"].ToString()));
                    sql = sql.Replace("@cassunit", GetNull(dr["cassunit"].ToString())); 
                    sql = sql.Replace("@dmadedate", GetNull(dr["dmadedate"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", math(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", math(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", GetNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", GetNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@impoids", math(dr["impoids"].ToString()));
                    sql = sql.Replace("@icheckids", mathNULL(dr["icheckids"].ToString()));
                    sql = sql.Replace("@cbvencode", GetNull(dr["cbvencode"].ToString()));
                    sql = sql.Replace("@cinvouchcode", GetNull(dr["cinvouchcode"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));//已验收
                    sql = sql.Replace("@iarrsid", math(dr["iarrsid"].ToString()));
                    sql = sql.Replace("@ccheckcode", SelSql(dr["ccheckcode"].ToString()));
                    sql = sql.Replace("@icheckidbaks", mathNULL(dr["icheckidbaks"].ToString()));
                    sql = sql.Replace("@crejectcode", GetNull(dr["crejectcode"].ToString()));
                    sql = sql.Replace("@irejectids", mathNULL(dr["irejectids"].ToString()));
                    sql = sql.Replace("@ccheckpersoncode", GetNull(dr["ccheckpersoncode"].ToString()));
                    sql = sql.Replace("@dcheckdate", GetNull(dr["dcheckdate"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@iorimoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@itaxrate", math(dr["itaxrate"].ToString()));
                    sql = sql.Replace("@itaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@ISUM", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@btaxcost", math(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@cpoid", SelSql(dr["cpoid"].ToString()));
                    sql = sql.Replace("@cmassunit", SetUnit(dr["cmassunit"].ToString()));   
                    sql = sql.Replace("@imaterialfee", math(dr["imaterialfee"].ToString()));
                    sql = sql.Replace("@iprocesscost", math(dr["iprocesscost"].ToString()));
                    sql = sql.Replace("@iprocessfee", math(dr["iprocessfee"].ToString()));
                    sql = sql.Replace("@dmsdate", GetNull(dr["dmsdate"].ToString()));
                    sql = sql.Replace("@ismaterialfee", math(dr["ismaterialfee"].ToString()));
                    sql = sql.Replace("@isprocessfee", math(dr["isprocessfee"].ToString()));
                    sql = sql.Replace("@iomodid", math(dr["iomodid"].ToString()));
                    sql = sql.Replace("@isodid", math(dr["isodid"].ToString()));
                    sql = sql.Replace("@strcontractid", GetNull(dr["strcontractid"].ToString()));
                    sql = sql.Replace("@strcode", GetNull(dr["strcode"].ToString()));
                    sql = sql.Replace("@isotype", mathNULL(dr["isotype"].ToString()));
                    sql = sql.Replace("@corufts", SelSql(dr["corufts"].ToString().Trim()));
                    sql = sql.Replace("@cbaccounter", GetNull(dr["cbaccounter"].ToString()));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@isumbillquantity", math(dr["isumbillquantity"].ToString()));
                    sql = sql.Replace("@bvmiused", math(dr["bvmiused"].ToString()));
                    sql = sql.Replace("@ivmisettlequantity", math(dr["ivmisettlequantity"].ToString()));
                    sql = sql.Replace("@ivmisettlenum", math(dr["ivmisettlenum"].ToString()));
                    sql = sql.Replace("@cvmivencode", GetNull(dr["cvmivencode"].ToString()));
                    sql = sql.Replace("@iinvsncount", math(dr["iinvsncount"].ToString()));
                    sql = sql.Replace("@impcost", math(dr["impcost"].ToString()));
                    sql = sql.Replace("@iimosid", math(dr["iimosid"].ToString()));
                    sql = sql.Replace("@iimbsid", math(dr["iimbsid"].ToString()));
                    sql = sql.Replace("@dbarvdate", GetNull(dr["dbarvdate"].ToString()));
                    sql = sql.Replace("@cbarvcode", GetNull(dr["cbarvcode"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", math(dr["iexpiratdatecalcu"].ToString()));
                    sql = sql.Replace("@iRowNo", math(iRowNo.ToString()));
                    sql = sql.Replace("@Cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    sql = sql.Replace("@iordertype", mathNULL("1"));
                    sql = sql.Replace("@bGsp", SelSql(dr["bGsp"].ToString()));
                    sql = sql.Replace("@iordercode", SelSql(dr["iordercode"].ToString()));

                    #endregion
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion

                    #region GSP

                    if (!string.IsNullOrEmpty(checkCode))
                    {
                        sql = "Insert into #GSPUFTS (gspufts,iCheckIds) values(''," + math(dr["icheckids"].ToString()) + ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    #endregion

                    #region InvPosition

                    decimal sum = 0;
                    decimal qty = decimal.Parse(dr["iquantity"].ToString());
                    foreach (DataRow drPsn in ds.Tables["Position"].Rows)
                    {
                        if (drPsn["cbatch"].ToString().Trim() == dr["cbatch"].ToString().Trim() && drPsn["cinvcode"].ToString().Trim() == dr["cinvcode"].ToString().Trim())
                        {
                            sql = "Insert Into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,"
                                + " dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,"
                                + " cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)"

                                + " Values (" + autoid + ",N'" + id + "'," + SelSql(dr["cwhcode"].ToString()) + "," + SelSql(drPsn["cposcode"].ToString()) + "," + SelSql(dr["cinvcode"].ToString()) + "," + SelSql(dr["cbatch"].ToString()) + "," + GetNull(packName) + ",Null,"
                                + " " + dateNull(dr["dvdate"].ToString()) + "," + " " + drPsn["iquantity"].ToString() + "," + mathNULL(dr["inum"].ToString()) + ",Null," + SelSql(ds.Tables["head"].Rows[0]["cmaker"].ToString()) + ","
                                + SelSql(dd) + ",1,Null,Null,Null,Null,Null,Null,Null,Null,Null," + SelSql(dr["cassunit"].ToString()) + ","
                                + " Null,Null," + dateNull(dr["dmadedate"].ToString()) + "," + mathNULL(dr["imassdate"].ToString()) + "," + SetUnit(dr["cmassunit"].ToString()) + ","
                                + " Null,2," + dateNull(dr["cexpirationdate"].ToString()) + "," + dateNull(dr["cexpirationdate"].ToString()) + ")";
                            cmd.CommandText = sql;
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                myTran.Rollback();
                                errMsg = "插入货位失败! ";
                                return -1;
                            }

                            sum += decimal.Parse(drPsn["iquantity"].ToString());
                            if (sum >= qty)
                                break;
                        }
                    }

                    #endregion
                }
                cmd.CommandText = "update rdrecord set bredvouch = " + bredvouch + " where id = " + id.ToString();
                cmd.ExecuteNonQuery();

                int abc;
                #region update trans

                //Ufida_WBBuffers
                sql = "Insert Into #Ufida_WBBuffers select a.id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), 1 * iPrice as Imoney ,a.Cinvcode, Corufts  as Corufts, iArrsID,iPOsID,iOMoDID,iIMBSID,iIMOSID,isotype,iSoDid,iRejectIds,iCheckIdBaks ,csource,cbustype,iCheckIds, 2 as iOperate   from rdrecord b inner join rdrecords a on a.id=b.id where b.id =" + id.ToString();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #region GSP
                if (!string.IsNullOrEmpty(checkCode))
                {
                    sql = "select iCheckIds ,sum(iquantity) as qty ,sum(inum) as inum into #GSP from #Ufida_WBBuffers group by iCheckIds having ( sum(iquantity)<>0 or sum (inum)<>0)";
                    cmd.CommandText = sql;
                    abc = cmd.ExecuteNonQuery();

                    sql = " update GSP_VouchQC  set cmemo=cmemo from GSP_VouchQC inner join GSP_VouchsQC with (nolock)   on GSP_VouchQC.ID=GSP_VouchsQC.ID where  GSP_VouchsQC.autoId  in (select iCheckIds  from  #GSPUFTS )";
                    cmd.CommandText = sql;
                    abc = cmd.ExecuteNonQuery();

                    sql = " update a set FSTQTY=isnull(FSTQTY,0) + b.qty,FSTNUM=isnull(FSTNUM,0) + b.inum  from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds ";
                    cmd.CommandText = sql;
                    abc = cmd.ExecuteNonQuery();

                    sql = " update a  set BMAKEPURIN =  case when convert (decimal (38,6),FELGQUANTITY-isnull(FSTQTY,0))=0  then 1 else 0 end     from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds ";
                    cmd.CommandText = sql;
                    abc = cmd.ExecuteNonQuery();
                }
                #endregion

                #region Ufida Table

                //Ufida_WBBuffers_ST
                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(inum) as inum,sum(imoney) as imoney,max(cinvcode) as cinvcode ,Max(corufts) as corufts,  max(iArrsID) as iArrsID,max(iPOsID) as iPOsID,max(iOMoDID) as iOMoDID,max(iIMBSID) as iIMBSID ,max(iIMOSID) as iIMOSID, max(isotype) as isotype,max(iSoDid) as iSoDid,max(iRejectIds) as iRejectIds,max(iCheckIdBaks) as iCheckIdBaks , max(csource) as csource,max(cbustype) as cbustype,sum(iOperate) as iOperate, case  sum(iOperate)  when 3 then N'M' when 2 then N'A' when 1 then N'D' end as editprop  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 or Sum(imoney)<>0)";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "update  #Ufida_WBBuffers_ST set corufts=null where iOperate<>2";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                //Ufida_WBBuffers_Target
                sql = "if exists (select 1 where not object_id('#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "select iArrsID as idID ,Sum(iquantity) as iquantity,sum(inum) as inum,sum(imoney) as imoney,0 as iBHGQuantity, 0 as iBHGnum,min(cinvcode) as cinvcode,min(corufts) as corufts ,0 as istflowid  into #Ufida_WBBuffers_Target from #Ufida_WBBuffers_ST where isnull(iArrsID,0)<>0 group by iArrsID";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,-1*iNum,-1*iQuantity,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck  where DocumentDId in (select idID from #Ufida_WBBuffers_Target)";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                //Pu_ArrivalVouchs
                sql = "UPDATE T1  SET T1.fValidInQuan=CONVERT(DECIMAL(38,4),ISNULL(T1.fValidInQuan,0))+CONVERT(DECIMAL(38,4),ISNULL(T2.iQuantity,0)),T1.fValidInNum=CONVERT(DECIMAL(38,2),ISNULL(T1.fValidInNum,0))+CONVERT(DECIMAL(38,2),ISNULL( T2.iNum,0)),T1.fInValidInQuan=CONVERT(DECIMAL(38,2),ISNULL(T1.fInValidInQuan,0))+CONVERT(DECIMAL(38,2),ISNULL(T2.iBHGQuantity,0)),T1.fInValidInNum=CONVERT(DECIMAL(38,2),ISNULL(T1.fInValidInNum,0))+CONVERT(DECIMAL(38,2),ISNULL(T2.iBHGNum,0)) FROM Pu_ArrivalVouchs T1 INNER JOIN #Ufida_WBBuffers_Target AS T2 ON T2.idID=T1.AutoID";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,1*iNum,1*iQuantity,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck  where DocumentDId in (select idID from #Ufida_WBBuffers_Target)";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                //Pu_ArrivalVouch
                sql = "update Pu_ArrivalVouch set ddate =Pu_ArrivalVouch.ddate  from Pu_ArrivalVouch  INNER JOIN Pu_ArrivalVouchs ON Pu_ArrivalVouch.id=Pu_ArrivalVouchs.id inner join #Ufida_WBBuffers_Target as T on T.idID=Pu_ArrivalVouchs.AutoID  where  isnull(T.Corufts,'')<>'' ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                //LPWriteBackTbl
                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackTbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode )";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = " insert into #LPWriteBackTbl (cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)  select cinvcode,isotype,isodid,1* iquantity,1* inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from rdrecords where isnull(isotype,0)>=4 and id = " + id.ToString();
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = " update #LPWriteBackTbl set inum = case when I.igrouptype =2 then isnull(a.inum,0) else 0 end , cfree1 = case when isnull(I.bconfigfree1,0) =0 then '' else isnull(a.cfree1,'') end , cfree2 = case when isnull(I.bconfigfree2,0) =0 then '' else isnull(a.cfree2,'') end , cfree3 = case when isnull(I.bconfigfree3,0) =0 then '' else isnull(a.cfree3,'') end , cfree4 = case when isnull(I.bconfigfree4,0) =0 then '' else isnull(a.cfree4,'') end , cfree5 = case when isnull(I.bconfigfree5,0) =0 then '' else isnull(a.cfree5,'') end , cfree6 = case when isnull(I.bconfigfree6,0) =0 then '' else isnull(a.cfree6,'') end , cfree7 = case when isnull(I.bconfigfree7,0) =0 then '' else isnull(a.cfree7,'') end , cfree8 = case when isnull(I.bconfigfree8,0) =0 then '' else isnull(a.cfree8,'') end , cfree9 = case when isnull(I.bconfigfree9,0) =0 then '' else isnull(a.cfree9,'') end , cfree10 = case when isnull(I.bconfigfree10,0) =0 then '' else isnull(a.cfree10,'') end  from #LPWriteBackTbl a inner join inventory I on a.cinvcode = I.cinvcode ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                //LPWriteBackSumTbl
                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackSumTbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackSumTbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql=" select cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,sum(isnull(iquantity,0)) as iquantity,sum(isnull(inum,0)) as inum  into  #LPWriteBackSumTbl from #LPWriteBackTbl group by cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  create index index_lpwritebacksumtbl on #LPWriteBackSumTbl(cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 ) ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = " insert into ST_DemandKeepInfo (cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,idemandtype,cdemandid )   select cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,0,0,isotype,isodid   from #LPWriteBackSumTbl a where not exists (select cinvcode from ST_DemandKeepInfo where cinvcode=a.cinvcode and idemandtype = a.isotype and cdemandid = a.isodid    and cfree1 = a.cfree1 and  cfree2 = a.cfree2 and cfree3 = a.cfree3 and cfree4 = a.cfree4 and cfree5 = a.cfree5 and cfree6 = a.cfree6    and cfree7 = a.cfree7 and cfree8 = a.cfree8 and cfree9 = a.cfree9 and cfree10 = a.cfree10 ) ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();
                #endregion

                #region delete temple

                sql = "if exists (select 1 where not object_id('tempdb..#GSP') is null ) Drop table #GSP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#GSPUFTS') is null ) Drop table #GSPUFTS";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null ) Drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_pu') is null ) Drop table #Ufida_WBBuffers_Target_pu";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_track') is null ) Drop table #Ufida_WBBuffers_Target_track";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#STWBMOOrder_Ma') is null ) Drop table #STWBMOOrder_Ma";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#STWBMOOrder_Pro') is null ) Drop table #STWBMOOrder_Pro";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                
                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                //保存
                sql = "exec ST_SaveForStock N'01',N'" + id + "',1,0,1";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();
                if (abc < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                //审核
                sql = "exec ST_VerForStock N'01',N'" + id + "',0,1,1";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();
                if (abc < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (spid == null)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) "
                     + "select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + "where a.transactionid=N'spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + "and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + "and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();
                #endregion

                #region UA_Identity

                /*
                cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + username + "'";
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                if (user.Length > 3)
                    user = user.Substring(0, 3);
                else
                    user = user.PadLeft(3, '0');
                cSeed = user + ddate;

                sql = "IF NOT EXISTS(select cNumber as Maxnumber From VoucherHistory  with (XLOCK) Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + username + ddate + "') "
                    + "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','制单人|日期','','" + username + ddate + "','" + ccode + "')";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                if (abc < 1)
                {
                    sql = "update VoucherHistory set cNumber='" + ccode + "' Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + username + ddate + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                 * 
                 * */

                ///单据编号修改规则
                ///采购退货入库单：单据年月（6位）+流水号（4位）
                ///流水号：根据单据日期，规则：月
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='日期' and cSeed='{1}'",cardnumber, cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('{0}','日期','月','{1}','1')",cardnumber, cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent='日期' and cSeed='{2}'", icode,cardnumber, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                //流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                //修改单据号
                cmd.CommandText = "Update RdRecord Set cCode = " + SelSql(ccode) + " Where Id = " + id.ToString();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update RdRecord cCode error!";
                    return -1;
                }

                if (id > 10000000)
                    id -= 860000000;    //暂定
                if (autoid > 10000000)
                    autoid -= 860000000;    //暂定
                //UA_Identity
                cmd.CommandText = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + "  where cvouchtype='rd' and cAcc_id='" + accid + "'";
                abc = cmd.ExecuteNonQuery();

                #endregion

                #region 监管码操作
                ///日期：2012－12－14
                ///人员：tianzhenyun
                ///功能：更新监管码数据表

                //判断监管码是否为空，如果为空则不操作，否则修改监管码
                string cdefine10 = ds.Tables[0].Rows[0]["cdefine10"].ToString();
                if (!string.IsNullOrEmpty(cdefine10))
                {
                    Model.Regulatory data = new Model.Regulatory();
                    data.RegCode = cdefine10;
                    data.CardNumber = cardnumber;
                    data.CardName = cVouchName;
                    data.CardCode = cSeed;
                    //账套号
                    data.AccID = accid;

                    bool flag = Regulatory.UpdateRegulatory(connectionString, data, out errMsg);
                    if (!flag)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }
                #endregion

                myTran.Commit();
                //myTran.Rollback();
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                return -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
        #endregion

        #region 委外入库单保存---by 委外加工
        public static int SaveByOutsourcing(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            //add
            string sql = null;
            string spid = null;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string cSeed = null;
            string user = null;
            int trans = 0;
            string bredvouch = math(ds.Tables[0].Rows[0]["bredvouch"].ToString());
            string packName = "NULL";

            try
            {
                #region 处理参数

                #region 单据日期/时间
                string dd = null, dt = null;//单据日期、时间

                    dd = DateTime.Now.ToString("yyyy-MM-dd");
                    dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                #endregion

                string ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ddate"]).ToString("yyyyMMdd");
                string cVouchName = "其他入库单";
                string cardnumber, vt_id;//单据类型编码 模板号
                sql = "select cUser_id from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                cmd.CommandText = sql;
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString();
                string tempSeed = user + ddate;
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select ifatherid,ichildid from UFSystem..UA_Identity where cvouchtype='rd' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;
                cmd.CommandText = "select isnull(max(convert(decimal,cnumber)),0) from voucherhistory where cardnumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed = " + GetNull(tempSeed);
                //cmd.CommandText = "select isnull(max(convert(decimal,right(ccode,4))),0) from rdrecord where substring(substring(ccode,4,13),1,8)=" + SelSql(ddate) + " and cbustype=" + SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString());
                ocode = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();//获得ccode的流水号

                if (ocode == null)
                    icode = 1;
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString();
                // ccode = ccode.PadLeft(4, '0');
                // ccode = cSeed.Substring(2, 6) + ccode;//年年月月+4位流水号
                // ccode = ds.Tables[0].Rows[0]["cwhcode"].ToString() + ccode;

                id = id + 1;
                if (id < 10000000)
                    id += 860000000;    //暂定
                object isnull = null;
                do
                {
                    id = id + 1;
                    cmd.CommandText = "select id from RdRecord where id =" + id;
                    isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                }
                while (isnull != null);

                string month = "";
                if (DateTime.Now.Month < 10)
                {
                    month = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    month = DateTime.Now.Month.ToString();
                }
                #endregion
                //csource
                #region rdrecord
                sql = @"insert into rdrecord(id,brdflag,cvouchtype,cbustype,csource,cwhcode,ddate,ccode,cdepcode,cvencode,cordercode,carvcode,cmaker,bpufirst,darvdate,vt_id,bisstqc,ipurarriveid,itaxrate,iexchrate,cexch_name,idiscounttaxtype,iswfcontrolled,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,bcredit,bomfirst,dnmaketime,chandler,dnverifytime,crdcode,dveridate,cptcode,cpersoncode,controlresult) values (@ID,@brdflag,@cvouchtype,@cbustype,@csource,@cwhcode,@ddate,@ccode,@cdepcode,@cvencode,@cordercode,@carvcode,@cmaker,@bpufirst,@darvdate,@vt_id,@bisstqc,@ipurarriveid,@itaxrate,@iexchrate,@cexch_name,@idiscounttaxtype,@iswfcontrolled,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,0,0,@dnmaketime,@chandler,@dnverifytime,@crdcode,@dveridate,@cptcode,@cpersoncode,@controlresult)";
                #region SQL_Replace
                sql = sql.Replace("@ID", id.ToString());
                sql = sql.Replace("@brdflag", SelSql(ds.Tables[0].Rows[0]["brdflag"].ToString()));
                sql = sql.Replace("@cvouchtype", SelSql(ds.Tables[0].Rows[0]["cvouchtype"].ToString()));
                sql = sql.Replace("@cbustype", SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString()));
                sql = sql.Replace("@csource", SelSql(ds.Tables[0].Rows[0]["csource"].ToString()));
                sql = sql.Replace("@cwhcode", SelSql(ds.Tables[0].Rows[0]["cwhcode"].ToString()));
                sql = sql.Replace("@ddate", SelSql(dd));
                sql = sql.Replace("@ccode", SelSql(tempSeed + ccode));
                sql = sql.Replace("@cdepcode", GetNull(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cordercode", SelSql(ds.Tables[0].Rows[0]["cordercode"].ToString()));
                sql = sql.Replace("@carvcode", SelSql(ds.Tables[0].Rows[0]["carvcode"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bpufirst", SelSql(ds.Tables[0].Rows[0]["bpufirst"].ToString()));
                sql = sql.Replace("@darvdate", SelSql(Convert.ToDateTime(ds.Tables[0].Rows[0]["darvdate"]).ToString("yyyy-MM-dd")));
                sql = sql.Replace("@vt_id", vt_id);
                sql = sql.Replace("@bisstqc", SelSql(ds.Tables[0].Rows[0]["bisstqc"].ToString()));
                sql = sql.Replace("@ipurarriveid", mathNULL(ds.Tables[0].Rows[0]["ipurarriveid"].ToString()));
                sql = sql.Replace("@itaxrate", SelSql(ds.Tables[0].Rows[0]["itaxrate"].ToString()));
                sql = sql.Replace("@iexchrate", SelSql(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@iswfcontrolled", SelSql(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", GetNull(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", GetNull(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull(ds.Tables[0].Rows[0]["cdefine9"].ToString()));
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine11"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", GetNull(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", GetNull(ds.Tables[0].Rows[0]["cdefine16"].ToString()));
                sql = sql.Replace("@darvdate", SelSql(ds.Tables[0].Rows[0]["darvdate"].ToString()));
                sql = sql.Replace("@chandler", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));//审核人
                sql = sql.Replace("@dnverifytime", GetNull(dt));//审核时间
                sql = sql.Replace("@dveridate", GetNull(dd));//审核日期
                sql = sql.Replace("@dnmaketime", GetNull(dt));
                sql = sql.Replace("@crdcode", SelSql(ds.Tables[0].Rows[0]["crdcode"].ToString()));
                sql = sql.Replace("@cptcode", GetNull(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@controlresult", SelSql("-1"));

                #endregion

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                #region temp table
                //delete temp
                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                foreach (DataRow dr in ds.Tables["body"].Rows)
                {
                    isnull = null;
                    do
                    {
                        autoid = autoid + 1;
                        cmd.CommandText = "select autoid from RdRecords where autoid =" + autoid;
                        isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                    }
                    while (isnull != null);

                    #region rdrecords
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,inum,iquantity,iunitcost,iprice,iaprice,ipunitcost,ipprice,cbatch,cvouchcode,cfree1,cfree2,dsdate,itax,isquantity,isnum,imoney,isoutquantity,isoutnum,ifnum,ifquantity,dvdate,itrids,cposition,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,citem_class,citemcode,iposid,facost,cname,citemcname,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbarcode,inquantity,innum,cassunit,dmadedate,imassdate,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,impoids,icheckids,cbvencode,cinvouchcode,cgspstate,iarrsid,ccheckcode,icheckidbaks,crejectcode,irejectids,ccheckpersoncode,dcheckdate,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,itaxrate,itaxprice,isum,btaxcost,cpoid,cmassunit,imaterialfee,iprocesscost,iprocessfee,dmsdate,ismaterialfee,isprocessfee,iomodid,isodid,strcontractid,strcode,isotype,corufts,cbaccounter,bcosting,isumbillquantity,bvmiused,ivmisettlequantity,ivmisettlenum,cvmivencode,iinvsncount,impcost,iimosid,iimbsid,cbarvcode,dbarvdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,cbatchproperty6,iordertype,comcode)
	                Values (@autoid,@id,@cinvcode,@inum,@iquantity,@iunitcost,@iprice,@iaprice,@ipunitcost,@ipprice,@cbatch,@cvouchcode,@CFREE1,@cfree2,@dsdate,@ITAX,@isquantity,@isnum,@imoney,@isoutquantity,@isoutnum,@ifnum,@ifquantity,@dvdate,@itrids,@cposition,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@citem_class,@citemcode,@iposid,@facost,@cname,@citemcname,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbarcode,@inquantity,@innum,@cassunit,@dmadedate,@imassdate,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@impoids,@icheckids,@cbvencode,@cinvouchcode,@cgspstate,@iarrsid,@ccheckcode,@icheckidbaks,@crejectcode,@irejectids,@ccheckpersoncode,@dcheckdate,@ioritaxcost,@ioricost,@iorimoney,@ioritaxprice,@iorisum,@itaxrate,@itaxprice,@ISUM,@btaxcost,@cpoid,@cmassunit,@imaterialfee,@iprocesscost,@iprocessfee,@dmsdate,@ismaterialfee,@isprocessfee,@iomodid,@isodid,@strcontractid,@strcode,@isotype,@corufts,@cbaccounter,@bcosting,@isumbillquantity,@bvmiused,@ivmisettlequantity,@ivmisettlenum,@cvmivencode,@iinvsncount,@impcost,@iimosid,@iimbsid,@cbarvcode,@dbarvdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@Cbatchproperty6,@iordertype,@comcode)";
                    #region SQL_Replace
                    sql = sql.Replace("@autoid", autoid.ToString());
                    sql = sql.Replace("@id", id.ToString());
                    sql = sql.Replace("@cinvcode", SelSql(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", mathNULL(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@iunitcost", math(dr["iunitcost"].ToString()));
                    sql = sql.Replace("@iprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@iaprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@ipunitcost", math(dr["ipunitcost"].ToString()));
                    sql = sql.Replace("@ipprice", math(dr["ipprice"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@cvouchcode", GetNull(dr["cvouchcode"].ToString()));
                    sql = sql.Replace("@CFREE1", GetNull(dr["cfree1"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree2"].ToString()));
                    sql = sql.Replace("@dsdate", GetNull(dr["dsdate"].ToString()));
                    sql = sql.Replace("@ITAX", math(dr["ITAX"].ToString()));
                    sql = sql.Replace("@isquantity", SelSql(dr["isquantity"].ToString()));
                    sql = sql.Replace("@isnum", SelSql(dr["isnum"].ToString()));
                    sql = sql.Replace("@imoney", math("0"));
                    sql = sql.Replace("@isoutquantity", math(dr["isoutquantity"].ToString()));
                    sql = sql.Replace("@isoutnum", math(dr["isoutnum"].ToString()));
                    sql = sql.Replace("@ifnum", math(dr["ifnum"].ToString()));
                    sql = sql.Replace("@ifquantity", math(dr["ifquantity"].ToString()));
                    sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@itrids", math(dr["itrids"].ToString()));
                    sql = sql.Replace("@cposition", GetNull(dr["cposition"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["cdefine22"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", math(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", math(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", math(dr["iposid"].ToString()));
                    sql = sql.Replace("@facost", math(dr["facost"].ToString()));
                    sql = sql.Replace("@cname", GetNull(dr["cname"].ToString()));
                    sql = sql.Replace("@citemcname", GetNull(dr["citemcname"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@cbarcode", GetNull(dr["cbarcode"].ToString()));
                    sql = sql.Replace("@inquantity", math(dr["OrderQuantity"].ToString()));
                    sql = sql.Replace("@innum", math(dr["innum"].ToString()));
                    sql = sql.Replace("@cassunit", GetNull(dr["cassunit"].ToString()));
                    sql = sql.Replace("@dmadedate", GetNull(dr["dmadedate"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", math(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", math(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", GetNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", GetNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@impoids", math(dr["impoids"].ToString()));
                    sql = sql.Replace("@icheckids", mathNULL(dr["icheckids"].ToString()));
                    sql = sql.Replace("@cbvencode", GetNull(dr["cbvencode"].ToString()));
                    sql = sql.Replace("@cinvouchcode", GetNull(dr["cinvouchcode"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));
                    sql = sql.Replace("@iarrsid", math(dr["iarrsid"].ToString()));
                    sql = sql.Replace("@ccheckcode", SelSql(dr["ccheckcode"].ToString()));
                    sql = sql.Replace("@icheckidbaks", mathNULL(dr["icheckidbaks"].ToString()));
                    sql = sql.Replace("@crejectcode", GetNull(dr["crejectcode"].ToString()));
                    sql = sql.Replace("@irejectids", mathNULL(dr["irejectids"].ToString()));
                    sql = sql.Replace("@ccheckpersoncode", GetNull(dr["ccheckpersoncode"].ToString()));
                    sql = sql.Replace("@dcheckdate", GetNull(dr["dcheckdate"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@iorimoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@itaxrate", math(dr["itaxrate"].ToString()));
                    sql = sql.Replace("@itaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@ISUM", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@btaxcost", math(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@cpoid", SelSql(dr["cpoid"].ToString()));
                    sql = sql.Replace("@cmassunit", math(dr["cmassunit"].ToString()));
                    sql = sql.Replace("@imaterialfee", math(dr["imaterialfee"].ToString()));
                    sql = sql.Replace("@iprocesscost", math(dr["iprocesscost"].ToString()));
                    sql = sql.Replace("@iprocessfee", math(dr["iprocessfee"].ToString()));
                    sql = sql.Replace("@dmsdate", GetNull(dr["dmsdate"].ToString()));
                    sql = sql.Replace("@ismaterialfee", math(dr["ismaterialfee"].ToString()));
                    sql = sql.Replace("@isprocessfee", math(dr["isprocessfee"].ToString()));
                    sql = sql.Replace("@iomodid", math(dr["iomodid"].ToString()));
                    sql = sql.Replace("@isodid", math(dr["isodid"].ToString()));
                    sql = sql.Replace("@strcontractid", GetNull(dr["strcontractid"].ToString()));
                    sql = sql.Replace("@strcode", GetNull(dr["strcode"].ToString()));
                    sql = sql.Replace("@isotype", mathNULL(dr["isotype"].ToString()));
                    sql = sql.Replace("@corufts", SelSql(dr["corufts"].ToString().Trim()));
                    sql = sql.Replace("@cbaccounter", GetNull(dr["cbaccounter"].ToString()));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@isumbillquantity", math(dr["isumbillquantity"].ToString()));
                    sql = sql.Replace("@bvmiused", math(dr["bvmiused"].ToString()));
                    sql = sql.Replace("@ivmisettlequantity", math(dr["ivmisettlequantity"].ToString()));
                    sql = sql.Replace("@ivmisettlenum", math(dr["ivmisettlenum"].ToString()));
                    sql = sql.Replace("@cvmivencode", GetNull(dr["cvmivencode"].ToString()));
                    sql = sql.Replace("@iinvsncount", math(dr["iinvsncount"].ToString()));
                    sql = sql.Replace("@impcost", math(dr["impcost"].ToString()));
                    sql = sql.Replace("@iimosid", math(dr["iimosid"].ToString()));
                    sql = sql.Replace("@iimbsid", math(dr["iimbsid"].ToString()));
                    sql = sql.Replace("@dbarvdate", GetNull(dr["dbarvdate"].ToString()));
                    sql = sql.Replace("@cbarvcode", GetNull(dr["cbarvcode"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", GetNull(dr["dexpirationdate"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", math(dr["iexpiratdatecalcu"].ToString()));
                    //sql = sql.Replace("@irowno", math(dr["irowno"].ToString()));
                    sql = sql.Replace("@Cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    //sql = sql.Replace(" 上午12:00", "");
                    sql = sql.Replace("@iordertype", mathNULL("0"));
                    //sql = sql.Replace("@comcode", GetNull(dr["ordercode"].ToString()));

                    #endregion
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion

                    #region InvPosition

                    decimal sum = 0;
                    decimal qty = decimal.Parse(dr["iquantity"].ToString());
                    foreach (DataRow drPsn in ds.Tables["Position"].Rows)
                    {
                        if (drPsn["cbatch"].ToString().Trim() == dr["cbatch"].ToString().Trim() && drPsn["cinvcode"].ToString().Trim() == dr["cinvcode"].ToString().Trim())
                        {
                            sql = "Insert Into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,"
                                + " dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,"
                                + " cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)"

                                + " Values (" + autoid + ",N'" + id + "'," + SelSql(dr["cwhcode"].ToString()) + "," + SelSql(drPsn["cposcode"].ToString()) + "," + SelSql(dr["cinvcode"].ToString()) + "," + SelSql(dr["cbatch"].ToString()) + "," + GetNull(packName) + ",Null,"
                                + " " + dateNull(dr["dvdate"].ToString()) + "," + " " + drPsn["iquantity"].ToString() + "," + mathNULL(dr["inum"].ToString()) + ",Null," + SelSql(ds.Tables["head"].Rows[0]["cmaker"].ToString()) + ","
                                + SelSql(dd) + ",1,Null,Null,Null,Null,Null,Null,Null,Null,Null," + SelSql(dr["cassunit"].ToString()) + ","
                                + " Null,Null," + dateNull(dr["dmadedate"].ToString()) + "," + dr["imassdate"].ToString() + "," + dr["cmassunit"].ToString() + ","
                                + " Null,2," + dateNull(dr["cexpirationdate"].ToString()) + "," + dateNull(dr["cexpirationdate"].ToString()) + ")";
                            cmd.CommandText = sql;
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                myTran.Rollback();
                                errMsg = "插入货位失败! ";
                                return -1;
                            }

                            sum += decimal.Parse(drPsn["iquantity"].ToString());
                            if (sum >= qty)
                                break;
                        }
                    }

                    #endregion

                }
                int abc;
                #region update trans

                sql = "update #LPWriteBackTbl set inum = case when I.igrouptype =2 then isnull(a.inum,0) else 0 end , cfree1 = case when isnull(I.bconfigfree1,0) =0 then '' else isnull(a.cfree1,'') end , cfree2 = case when isnull(I.bconfigfree2,0) =0 then '' else isnull(a.cfree2,'') end , cfree3 = case when isnull(I.bconfigfree3,0) =0 then '' else isnull(a.cfree3,'') end , cfree4 = case when isnull(I.bconfigfree4,0) =0 then '' else isnull(a.cfree4,'') end , cfree5 = case when isnull(I.bconfigfree5,0) =0 then '' else isnull(a.cfree5,'') end , cfree6 = case when isnull(I.bconfigfree6,0) =0 then '' else isnull(a.cfree6,'') end , cfree7 = case when isnull(I.bconfigfree7,0) =0 then '' else isnull(a.cfree7,'') end , cfree8 = case when isnull(I.bconfigfree8,0) =0 then '' else isnull(a.cfree8,'') end , cfree9 = case when isnull(I.bconfigfree9,0) =0 then '' else isnull(a.cfree9,'') end , cfree10 = case when isnull(I.bconfigfree10,0) =0 then '' else isnull(a.cfree10,'') end  from #LPWriteBackTbl a inner join inventory I on a.cinvcode = I.cinvcode";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,sum(isnull(iquantity,0)) as iquantity,sum(isnull(inum,0)) as inum  into  #LPWriteBackSumTbl from #LPWriteBackTbl group by cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  create index index_lpwritebacksumtbl on #LPWriteBackSumTbl(cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "insert into ST_DemandKeepInfo (cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,idemandtype,cdemandid )   select cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,0,0,isotype,isodid   from #LPWriteBackSumTbl a where not exists (select cinvcode from ST_DemandKeepInfo where cinvcode=a.cinvcode and idemandtype = a.isotype and cdemandid = a.isodid    and cfree1 = a.cfree1 and  cfree2 = a.cfree2 and cfree3 = a.cfree3 and cfree4 = a.cfree4 and cfree5 = a.cfree5 and cfree6 = a.cfree6 and cfree7 = a.cfree7 and cfree8 = a.cfree8 and cfree9 = a.cfree9 and cfree10 = a.cfree10) ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update ST_DemandKeepInfo set iquantity =isnull(a.iquantity,0) + isnull(b.iquantity,0) ,inum =isnull(a.inum,0) + isnull(b.inum,0)    from ST_DemandKeepInfo a inner join #LPWriteBackSumTbl b on a.cinvcode =b.cinvcode and a.idemandtype = b.isotype and a.cdemandid = b.isodid  and a.cfree1 =b.cfree1 and a.cfree2 =b.cfree2 and a.cfree3 =b.cfree3 and a.cfree4 =b.cfree4 and a.cfree5 =b.cfree5 and a.cfree6 =b.cfree6  and a.cfree7 =b.cfree7 and a.cfree8 =b.cfree8 and a.cfree9 =b.cfree9 and a.cfree10 =b.cfree10 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #region delete temble

                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null ) Drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..##Ufida_WBBuffers_Target_pu') is null ) Drop table ##Ufida_WBBuffers_Target_pu";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..##Ufida_WBBuffers_Target_track') is null ) Drop table ##Ufida_WBBuffers_Target_track";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..##STWBMOOrder_Ma') is null ) Drop table ##STWBMOOrder_Ma";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..###STWBMOOrder_Pro') is null ) Drop table ###STWBMOOrder_Pro";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                sql = "exec ST_SaveForStock N'08',N'" + id + "',1,0 ,1";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (spid == null)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) "
                     + "select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + "where a.transactionid='spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + "and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + "and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                #region VoucherHistory

                cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + user + "'";
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                if (user.Length > 3)
                    user = user.Substring(0, 3);
                else
                    user = user.PadLeft(3, '0');
                cSeed = user + ddate;

                sql = "IF NOT EXISTS(select cNumber as Maxnumber From VoucherHistory  with (XLOCK) Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + cSeed + "') "
                    + "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','制单人|日期','','" + cSeed + "','" + ccode + "')";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                if (abc < 1)
                {
                    sql = "update VoucherHistory set cNumber='" + ccode + "' Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + cSeed + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "select @@TRANCOUNT ";
                trans = int.Parse(cmd.ExecuteScalar().ToString());
                if (trans > 0)
                {
                    cSeed = cSeed + icode.ToString().PadLeft(4, '0');
                    cmd.CommandText = "Update RdRecord Set cCode = " + SelSql(cSeed) + " Where Id = " + id.ToString();
                    cmd.ExecuteNonQuery();
                }

                sql = "if exists (select 1 where not object_id('tempdb..#stbatcharchief') is null )  drop table #STBatchArchief";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select distinct convert(int,1) as autoid , a.cinvcode as cinvcode , isnull(a.cbatch ,'') as cbatch, isnull(a.cfree1,'') as cfree1, isnull(a.cfree2,'') as cfree2, isnull(a.cfree3,'') as cfree3, isnull(a.cfree4,'') as cfree4, isnull(a.cfree5,'') as cfree5, isnull(a.cfree6,'') as cfree6, isnull(a.cfree7,'') as cfree7, isnull(a.cfree8,'') as cfree8, isnull(a.cfree9,'') as cfree9, isnull(a.cfree10,'') as cfree10, a.cBatchProperty1 as cBatchProperty1, a.cBatchProperty2 as cBatchProperty2, a.cBatchProperty3 as cBatchProperty3, a.cBatchProperty4 as cBatchProperty4, a.cBatchProperty5 as cBatchProperty5, a.cBatchProperty6 as cBatchProperty6, a.cBatchProperty7 as cBatchProperty7, a.cBatchProperty8 as cBatchProperty8, a.cBatchProperty9 as cBatchProperty9, a.cBatchProperty10 as cBatchProperty10 into #STBatchArchief  from rdrecords a with (nolock)  inner join inventory_sub b   with (nolock)  on a.cinvcode = b.cinvsubcode and isnull(bbatchcreate,0)=1  where a.id =" + id.ToString() + " and isnull(a.cbatch ,'')<>'' ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#stbatcharchief') is null )  drop table #STBatchArchief";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                if (id > 10000000)
                    id -= 860000000;    //暂定
                //UA_Identity
                cmd.CommandText = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + "  where cvouchtype='mo_om' and cAcc_id='" + accid + "'";
                cmd.ExecuteNonQuery();
                #endregion

                myTran.Commit();
                //myTran.Rollback();
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                return -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
        #endregion

        #region 材料出库单保存---by 委外订单
        public static int SaveByOMMOrder(DataSet ds, string connectionString, string accid,string year, out string errMsg)
        { 
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            string spid = null;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string user = null;
            string cSeed = null;
            int trans = 0;
            int iRowNo = 0;
            string bredvouch = math("0");
            string packName = "NULL";

            try
            {
                #region 处理参数

                string dd = null, dt = null;//单据日期、时间
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ddate"].ToString()).ToString("yyyyMMdd");
                sql = "select cUser_id from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                cmd.CommandText = sql;
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString();
                string tempSeed = user + ddate;
                string cVouchName = "材料出库单";
                string cardnumber, vt_id;//单据类型编码 模板号
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select max(ifatherid) ifatherid,max(ichildid) ichildid from UFSystem..UA_Identity where cvouchtype='rd' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;
                
                /*
                cmd.CommandText = "select isnull(max(convert(decimal,cnumber)),0) from voucherhistory where cardnumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed=" + GetNull(tempSeed);
                //cmd.CommandText = "select isnull(max(convert(decimal,right(ccode,4))),0) from rdrecord where substring(substring(ccode,4,13),1,8)=" + SelSql(ddate) + " and cbustype=" + SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString());
                ocode = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();//获得ccode的流水号
                if (ocode == null)
                    icode = 1;
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString();
                 * 
                 * */


                id = id + 1;
                if (id < 10000000)
                    id += 860000000;    //暂定
                if (autoid < 10000000)
                    autoid += 860000000;    //暂定
                object isnull = null;
                do
                {
                    id = id + 1;
                    cmd.CommandText = "select id from RdRecord where id =" + id;
                    isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                }
                while (isnull != null);

                string month = DateTime.Now.Month.ToString("MM");

                #endregion

                #region rdrecord
                sql = @"insert into rdrecord(bredvouch,id,brdflag,cvouchtype,cbustype,csource,cwhcode,ddate,ccode,cdepcode,cvencode,cordercode,carvcode,cmaker,bpufirst,darvdate,vt_id,bisstqc,ipurarriveid,itaxrate,iexchrate,cexch_name,idiscounttaxtype,iswfcontrolled,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,bcredit,bomfirst,dnmaketime,chandler,dnverifytime,crdcode,dveridate,cptcode,cpersoncode,controlresult,cbusCode,cchkCode,dchkDate,cchkPerson,iProOrderId,ipurorderid,iMQuantity,cMPoCode,cPsPcode) values (@bredvouch,@ID,@brdflag,@cvouchtype,@cbustype,@csource,@cwhcode,@ddate,@ccode,@cdepcode,@cvencode,@cordercode,@carvcode,@cmaker,@bpufirst,@darvdate,@vt_id,@bisstqc,@ipurarriveid,@itaxrate,@iexchrate,@cexch_name,@idiscounttaxtype,@iswfcontrolled,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,0,0,@dnmaketime,@chandler,@dnverifytime,@crdcode,@dveridate,@cptcode,@cpersoncode,@controlresult,@cbusCode,@cchkCode,@dchkDate,@cchkPerson,@iProOrderId,@ipurorderid,@iMQuantity,@cMPoCode,@cPsPcode)";
                #region SQL_Replace
                sql = sql.Replace("@ID", id.ToString());
                sql = sql.Replace("@bredvouch", SelSql("0"));
                sql = sql.Replace("@brdflag", SelSql("0"));
                sql = sql.Replace("@cvouchtype", SelSql("11"));
                sql = sql.Replace("@cbustype", SelSql("委外发料"));
                sql = sql.Replace("@csource", SelSql("委外订单"));
                sql = sql.Replace("@cwhcode", SelSql(ds.Tables[0].Rows[0]["cwhcode"].ToString()));
                sql = sql.Replace("@ddate", SelSql(dd));
                sql = sql.Replace("@ccode", SelSql(id.ToString()));
                sql = sql.Replace("@cdepcode", GetNull(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cordercode", SelSql(ds.Tables[0].Rows[0]["cordercode"].ToString()));
                sql = sql.Replace("@carvcode", SelSql(ds.Tables[0].Rows[0]["carvcode"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bpufirst", SelSql(ds.Tables[0].Rows[0]["bpufirst"].ToString()));
                sql = sql.Replace("@darvdate", dateNull(ds.Tables[0].Rows[0]["darvdate"].ToString()));
                sql = sql.Replace("@vt_id", vt_id);
                sql = sql.Replace("@bisstqc", SelSql(ds.Tables[0].Rows[0]["bisstqc"].ToString()));
                sql = sql.Replace("@ipurarriveid", mathNULL(ds.Tables[0].Rows[0]["ipurarriveid"].ToString()));
                sql = sql.Replace("@itaxrate", math("0"));
                sql = sql.Replace("@iexchrate", SelSql(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@iswfcontrolled", SelSql(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", GetNull(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", GetNull(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull(ds.Tables[0].Rows[0]["cdefine9"].ToString()));
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine11"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", GetNull(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", GetNull(ds.Tables[0].Rows[0]["cdefine16"].ToString()));
                sql = sql.Replace("@dnmaketime", GetNull(dt));
                sql = sql.Replace("@chandler", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));//审核人
                sql = sql.Replace("@dnverifytime", GetNull(dt));//审核时间
                sql = sql.Replace("@dveridate", GetNull(dd));//审核日期
                sql = sql.Replace("@crdcode", SelSql(ds.Tables[0].Rows[0]["crdcode"].ToString()));
                sql = sql.Replace("@cptcode", GetNull(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@controlresult", SelSql("-1"));
                sql = sql.Replace("@cbusCode", GetNull(ds.Tables[0].Rows[0]["cbuscode"].ToString()));
                sql = sql.Replace("@cchkCode", GetNull(ds.Tables[0].Rows[0]["cchkcode"].ToString()));
                sql = sql.Replace("@dchkDate", GetNull(ds.Tables[0].Rows[0]["dchkdate"].ToString()));
                sql = sql.Replace("@cchkPerson", GetNull(ds.Tables[0].Rows[0]["cchkperson"].ToString()));
                sql = sql.Replace("@iProOrderId", mathNULL(ds.Tables[0].Rows[0]["iProOrderId"].ToString()));
                //材料
                sql = sql.Replace("@ipurorderid", GetNull(ds.Tables[0].Rows[0]["ipurorderid"].ToString()));
                sql = sql.Replace("@cMPoCode", GetNull(ds.Tables[0].Rows[0]["cMPoCode"].ToString()));
                sql = sql.Replace("@iMQuantity", GetNull(ds.Tables[0].Rows[0]["iMQuantity"].ToString()));
                sql = sql.Replace("@cPsPcode", GetNull(ds.Tables[0].Rows[0]["cPsPcode"].ToString()));


                #endregion
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                #region TempTable

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null ) Drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select id,autoid ,convert(decimal(30,4),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, impoids,iomomid,applydid,Corufts ,  convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords  where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update rdrecords set corufts ='' where id =" + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                foreach (DataRow dr in ds.Tables["body"].Rows)
                {
                    isnull = null;
                    do
                    {
                        autoid = autoid + 1;
                        cmd.CommandText = "select autoid from RdRecords where autoid =" + autoid;
                        isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                    }
                    while (isnull != null);
                    iRowNo++;

                    #region rdrecords
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,inum,iquantity,iunitcost,iprice,iaprice,ipunitcost,ipprice,cbatch,cvouchcode,cfree1,cfree2,dsdate,itax,isquantity,isnum,imoney,isoutquantity,isoutnum,ifnum,ifquantity,dvdate,itrids,cposition,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,citem_class,citemcode,iposid,facost,cname,citemcname,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cbarcode,inquantity,innum,cassunit,dmadedate,imassdate,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,impoids,icheckids,cbvencode,cinvouchcode,cgspstate,iarrsid,ccheckcode,icheckidbaks,crejectcode,irejectids,ccheckpersoncode,dcheckdate,ioritaxcost,ioricost,iorimoney,ioritaxprice,iorisum,itaxrate,itaxprice,isum,btaxcost,cpoid,cmassunit,imaterialfee,iprocesscost,iprocessfee,dmsdate,ismaterialfee,isprocessfee,iomodid,isodid,strcontractid,strcode,isotype,corufts,cbaccounter,bcosting,isumbillquantity,bvmiused,ivmisettlequantity,ivmisettlenum,cvmivencode,iinvsncount,impcost,iimosid,iimbsid,cbarvcode,dbarvdate,cexpirationdate,dexpirationdate,iexpiratdatecalcu,cbatchproperty6,iordertype,bGsp,iRowNo,iordercode,invCode,csoCode,iOMoMID,comCode)
	 Values (@autoid,@id,@cinvcode,@inum,@iquantity,@iunitcost,@iprice,@iaprice,@ipunitcost,@ipprice,@cbatch,@cvouchcode,@CFREE1,@cfree2,@dsdate,@ITAX,@isquantity,@isnum,@imoney,@isoutquantity,@isoutnum,@ifnum,@ifquantity,@dvdate,@itrids,@cposition,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@citem_class,@citemcode,@iposid,@facost,@cname,@citemcname,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@cbarcode,@inquantity,@innum,@cassunit,@dmadedate,@imassdate,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@impoids,@icheckids,@cbvencode,@cinvouchcode,@cgspstate,@iarrsid,@ccheckcode,@icheckidbaks,@crejectcode,@irejectids,@ccheckpersoncode,@dcheckdate,@ioritaxcost,@ioricost,@iorimoney,@ioritaxprice,@iorisum,@itaxrate,@itaxprice,@ISUM,@btaxcost,@cpoid,@cmassunit,@imaterialfee,@iprocesscost,@iprocessfee,@dmsdate,@ismaterialfee,@isprocessfee,@iomodid,@isodid,@strcontractid,@strcode,@isotype,@corufts,@cbaccounter,@bcosting,@isumbillquantity,@bvmiused,@ivmisettlequantity,@ivmisettlenum,@cvmivencode,@iinvsncount,@impcost,@iimosid,@iimbsid,@cbarvcode,@dbarvdate,@cexpirationdate,@dexpirationdate,@iexpiratdatecalcu,@Cbatchproperty6,@iordertype,@bGsp,@iRowNo,@iordercode,@invCode,@csoCode,@iOMoMID,@comCode)";
                    #region SQL_Replace
                    sql = sql.Replace("@autoid", autoid.ToString());
                    sql = sql.Replace("@id", id.ToString());
                    sql = sql.Replace("@cinvcode", SelSql(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", mathNULL(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@iunitcost", math(dr["iunitcost"].ToString()));
                    sql = sql.Replace("@iprice", math(dr["iprice"].ToString()));
                    sql = sql.Replace("@iaprice", math("0"));
                    sql = sql.Replace("@ipunitcost", math(dr["ipunitcost"].ToString()));
                    sql = sql.Replace("@ipprice", math(dr["ipprice"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@cvouchcode", GetNull(dr["cvouchcode"].ToString()));
                    sql = sql.Replace("@dsdate", GetNull(dr["dsdate"].ToString()));
                    sql = sql.Replace("@ITAX", math(dr["ITAX"].ToString()));
                    sql = sql.Replace("@isquantity", SelSql(dr["isquantity"].ToString()));
                    sql = sql.Replace("@isnum", SelSql(dr["isnum"].ToString()));
                    sql = sql.Replace("@imoney", math("0"));
                    sql = sql.Replace("@isoutquantity", math(dr["isoutquantity"].ToString()));
                    sql = sql.Replace("@isoutnum", math(dr["isoutnum"].ToString()));
                    sql = sql.Replace("@ifnum", math(dr["ifnum"].ToString()));
                    sql = sql.Replace("@ifquantity", math(dr["ifquantity"].ToString()));
                    sql = sql.Replace("@dvdate", GetNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@itrids", math(dr["itrids"].ToString()));
                    sql = sql.Replace("@cposition", GetNull(dr["cposition"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["cdefine22"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", math(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", math(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", GetNull(""));
                    sql = sql.Replace("@facost", GetNull(""));
                    sql = sql.Replace("@cname", GetNull(dr["cname"].ToString()));
                    sql = sql.Replace("@citemcname", GetNull(dr["citemcname"].ToString()));
                    sql = sql.Replace("@CFREE1", GetNull(dr["cfree1"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree2"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@cbarcode", GetNull(dr["cbarcode"].ToString()));
                    sql = sql.Replace("@inquantity", math(dr["OrderQuantity"].ToString()));
                    sql = sql.Replace("@innum", math(dr["innum"].ToString()));
                    sql = sql.Replace("@cassunit", GetNull(dr["cassunit"].ToString()));
                    sql = sql.Replace("@dmadedate", GetNull(dr["dmadedate"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", math(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", math(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", GetNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", GetNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@impoids", math(dr["impoids"].ToString()));
                    sql = sql.Replace("@icheckids", mathNULL(dr["icheckids"].ToString()));
                    sql = sql.Replace("@cbvencode", GetNull(dr["cbvencode"].ToString()));
                    sql = sql.Replace("@cinvouchcode", GetNull(dr["cinvouchcode"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));//已验收
                    sql = sql.Replace("@iarrsid", math(dr["iarrsid"].ToString()));
                    sql = sql.Replace("@ccheckcode", SelSql(dr["ccheckcode"].ToString()));
                    sql = sql.Replace("@icheckidbaks", mathNULL(dr["icheckidbaks"].ToString()));
                    sql = sql.Replace("@crejectcode", GetNull(dr["crejectcode"].ToString()));
                    sql = sql.Replace("@irejectids", mathNULL(dr["irejectids"].ToString()));
                    sql = sql.Replace("@ccheckpersoncode", GetNull(dr["ccheckpersoncode"].ToString()));
                    sql = sql.Replace("@dcheckdate", GetNull(dr["dcheckdate"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@iorimoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@itaxrate", math("0"));
                    sql = sql.Replace("@itaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@ISUM", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@btaxcost", math(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@cpoid", SelSql(dr["cpoid"].ToString()));
                    sql = sql.Replace("@cmassunit", SetUnit(dr["cmassunit"].ToString()));
                    sql = sql.Replace("@imaterialfee", math(dr["imaterialfee"].ToString()));
                    sql = sql.Replace("@iprocesscost", math(dr["iprocesscost"].ToString()));
                    sql = sql.Replace("@iprocessfee", math(dr["iprocessfee"].ToString()));
                    sql = sql.Replace("@dmsdate", GetNull(dr["dmsdate"].ToString()));
                    sql = sql.Replace("@ismaterialfee", math(dr["ismaterialfee"].ToString()));
                    sql = sql.Replace("@isprocessfee", math(dr["isprocessfee"].ToString()));
                    sql = sql.Replace("@iomodid", math(dr["iomodid"].ToString()));
                    sql = sql.Replace("@isodid", math(dr["isodid"].ToString()));
                    sql = sql.Replace("@strcontractid", GetNull(dr["strcontractid"].ToString()));
                    sql = sql.Replace("@strcode", GetNull(dr["strcode"].ToString()));
                    sql = sql.Replace("@isotype", mathNULL(dr["isotype"].ToString()));
                    sql = sql.Replace("@corufts", SelSql(dr["corufts"].ToString().Trim()));
                    sql = sql.Replace("@cbaccounter", GetNull(dr["cbaccounter"].ToString()));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@isumbillquantity", math(dr["isumbillquantity"].ToString()));
                    sql = sql.Replace("@bvmiused", math(dr["bvmiused"].ToString()));
                    sql = sql.Replace("@ivmisettlequantity", math(dr["ivmisettlequantity"].ToString()));
                    sql = sql.Replace("@ivmisettlenum", math(dr["ivmisettlenum"].ToString()));
                    sql = sql.Replace("@cvmivencode", GetNull(dr["cvmivencode"].ToString()));
                    sql = sql.Replace("@iinvsncount", math(dr["iinvsncount"].ToString()));
                    sql = sql.Replace("@impcost", math(dr["impcost"].ToString()));
                    sql = sql.Replace("@iimosid", math(dr["iimosid"].ToString()));
                    sql = sql.Replace("@iimbsid", math(dr["iimbsid"].ToString()));
                    sql = sql.Replace("@dbarvdate", GetNull(dr["dbarvdate"].ToString()));
                    sql = sql.Replace("@cbarvcode", GetNull(dr["cbarvcode"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", math("2"));
                    sql = sql.Replace("@iRowNo", math(iRowNo.ToString()));
                    sql = sql.Replace("@Cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    sql = sql.Replace("@iordertype", mathNULL(""));
                    sql = sql.Replace("@bGsp", SelSql(dr["bGsp"].ToString()));
                    sql = sql.Replace("@iordercode", GetNull(""));
                    //材料
                    sql = sql.Replace("@comCode", GetNull(dr["comCode"].ToString()));
                    sql = sql.Replace("@invCode", GetNull(dr["invCode"].ToString()));
                    sql = sql.Replace("@csoCode", GetNull(dr["comCode"].ToString()));
                    sql = sql.Replace("@iOMoMID", GetNull(dr["iomomid"].ToString()));

                    #endregion
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion

                    #region InvPosition

                    decimal sum = 0;
                    decimal qty = decimal.Parse(dr["iquantity"].ToString());
                    foreach (DataRow drPsn in ds.Tables["Position"].Rows)
                    {
                        if (drPsn["cbatch"].ToString().Trim() == dr["cbatch"].ToString().Trim() && drPsn["cinvcode"].ToString().Trim() == dr["cinvcode"].ToString().Trim())
                        {
                            sql = "Insert Into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,"
                                + " dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,"
                                + " cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)"

                                + " Values (" + autoid + ",N'" + id + "'," + SelSql(dr["cwhcode"].ToString()) + "," + SelSql(drPsn["cposcode"].ToString()) + "," + SelSql(dr["cinvcode"].ToString()) + "," + SelSql(dr["cbatch"].ToString()) + "," + GetNull(packName) + ",Null,"
                                + " " + dateNull(dr["dvdate"].ToString()) + "," + " " + drPsn["iquantity"].ToString() + "," + mathNULL(dr["inum"].ToString()) + ",Null," + SelSql(ds.Tables["head"].Rows[0]["cmaker"].ToString()) + ","
                                + SelSql(dd) + ",1,Null,Null,Null,Null,Null,Null,Null,Null,Null," + SelSql(dr["cassunit"].ToString()) + ","
                                + " Null,Null," + dateNull(dr["dmadedate"].ToString()) + "," + dr["imassdate"].ToString() + "," + dr["cmassunit"].ToString() + ","
                                + " Null,2," + dateNull(dr["cexpirationdate"].ToString()) + "," + dateNull(dr["cexpirationdate"].ToString()) + ")";
                            cmd.CommandText = sql;
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                myTran.Rollback();
                                errMsg = "插入货位失败! ";
                                return -1;
                            }

                            sum += decimal.Parse(drPsn["iquantity"].ToString());
                            if (sum >= qty)
                                break;
                        }
                    }

                    #endregion

                }
                int abc;
                #region UpdateTrans

                sql = "Insert Into #Ufida_WBBuffers select b.id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), b.impoids,b.iomomid,b.applydid, Corufts  as Corufts,2 as iOperate   from rdrecords b INNER join rdrecord on rdrecord.id = b.id  where cBusType <> N'假退料' and b.id=" + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(inum) as inum,Max(corufts) as corufts,  max(impoids) as impoids,max(iomomid) as iomomid,max(applydid) as applydid,sum(iOperate) as iOperate,  case sum(iOperate)  when 3 then N'M' when 2 then N'A' when 1 then N'D' end as editprop  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update  #Ufida_WBBuffers_ST set corufts=null where iOperate<>2 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select iomomid as iMpoids ,Sum(iquantity) as iqty,sum(inum) as inum, min(corufts) as corufts  into #Ufida_WBBuffers_Target from #Ufida_WBBuffers_ST where isnull(iomomid,0)<>0 group by iomomid  ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update a set isendqty = CONVERT(DECIMAL(20,4), isnull(a.isendqty,0)) + CONVERT(DECIMAL(20,4),isnull(b.iQty,0)),iSendNum = CONVERT(DECIMAL(20,2), isnull(a.iSendNum,0)) + CONVERT(DECIMAL(20,2),isnull(b.inum,0))  FROM OM_MOMaterials a INNER JOIN #Ufida_WBBuffers_Target  b ON b.iMpoids=a.momaterialsID ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "update om_momain set om_momain.ddate=om_momain.ddate FROM om_momain INNER JOIN om_modetails ON om_momain.moid=om_modetails.moid  INNER JOIN OM_MOMaterials ON OM_MOMaterials.modetailsid=om_modetails.modetailsid  inner join #Ufida_WBBuffers_Target  b on b.iMpoids=OM_MOMaterials.momaterialsid ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "update OM_MOdetails set imaterialsendqty =CONVERT(DECIMAL(20,4),isnull(imaterialsendqty,0)) + CONVERT(DECIMAL(20,4),isnull(a.iqty,0)) from OM_MOdetails inner join (select OM_MOdetails.modetailsid,sum(iqty) as iqty from #Ufida_WBBuffers_Target b inner join OM_MOMaterials on b.iMpoids=OM_MOMaterials.momaterialsid  inner join OM_MOdetails on OM_MOdetails.modetailsid=OM_MOMaterials.modetailsid group by OM_MOdetails.modetailsid) a on a.modetailsid=OM_MOdetails.modetailsid ";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#STCheckVouchDate') is null ) Drop table #STCheckVouchDate";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select cinvcode,convert(datetime,Null) as dstdate,convert(datetime,Null) as doridate ,convert(nvarchar(800),Null) as error into #STCheckVouchDate  from inventory where 1=0 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "exec  USP_STCheckVouchDate  N'11',N'#Ufida_WBBuffers'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #region delete table

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null ) Drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_pu') is null ) Drop table #Ufida_WBBuffers_Target_pu";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_track') is null ) Drop table #Ufida_WBBuffers_Target_track";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#STWBMOOrder_Ma') is null ) Drop table #STWBMOOrder_Ma";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#STWBMOOrder_Pro') is null ) Drop table #STWBMOOrder_Pro";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion
                //生单
                sql = "exec ST_SaveForStock N'11',N'" + id + "',1,0 ,1";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //审核
                sql = "exec ST_VerForStock N'11',N'" + id + "',0,1 ,1";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (spid == null)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) "
                     + "select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + "where a.transactionid=N'spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + "and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + "and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                #region VoucherHistory

                //cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + user + "'";
                //user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                //if (user.Length > 3)
                //    user = user.Substring(0, 3);
                //else
                //    user = user.PadLeft(3, '0');
                /*
                cSeed = user + ddate;

                sql = "IF NOT EXISTS(select cNumber as Maxnumber From VoucherHistory  with (XLOCK) Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + cSeed + "') "
                    + "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','制单人|日期','','" + cSeed + "','"+ccode+"')";
                cmd.CommandText = sql;
                abc=cmd.ExecuteNonQuery();

                if (abc < 1)
                {
                    sql = "update VoucherHistory set cNumber='" + ccode + "' Where  CardNumber='" + cardnumber + "' and cContent='制单人|日期' and cSeed='" + cSeed + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + user + "'";
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                if (user.Length > 3)
                    user = user.Substring(0, 3);
                else
                    user = user.PadLeft(3, '0');
                cSeed = user + ddate;

                cmd.CommandText = "select @@TRANCOUNT ";
                trans = int.Parse(cmd.ExecuteScalar().ToString());
                if (trans > 0)
                {
                    cSeed = cSeed  + icode.ToString().PadLeft(4, '0');
                    cmd.CommandText = "Update RdRecord Set cCode = " + SelSql(cSeed) + " Where Id = " + id.ToString();
                    cmd.ExecuteNonQuery();
                }
                 * 
                 * 
                 * */

                ///单据编号修改规则
                ///材料出库单：单据年月（6位）+流水号（4位）
                ///流水号：根据单据日期，规则：月
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='日期' and cSeed='{1}'", cardnumber, cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('{0}','日期','月','{1}','1')", cardnumber, cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent='日期' and cSeed='{2}'", icode, cardnumber, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                //普通销售的流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                sql = "Update RdRecord Set cCode = N'" + ccode + "' Where Id = " + id + "";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update rdrecord ccode error!";
                    return -1;
                }


                if (id > 10000000)
                    id -= 860000000;    //暂定
                if (autoid > 10000000)
                    autoid -= 860000000;    //暂定
                //UA_Identity
                cmd.CommandText = "update UFSystem..UA_Identity set ifatherid = " + id + ",ichildid = " + autoid + "  where cvouchtype='rd' and cAcc_id='" + accid + "'";
                cmd.ExecuteNonQuery();

                #endregion

                myTran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                return -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }

        }
                    
        #endregion

        #region 组装拆卸调拨审核

        public static int AuditByDismantle(DataSet ds, string type, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran;
            myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;

            #region 自定义参数
            string sql = null;
            string spid = null;
            string id = ds.Tables[0].Rows[0]["id"].ToString();
            string bcode = ds.Tables[0].Rows[0]["cbuscode"].ToString();
            string handler = ds.Tables[0].Rows[0]["cHandler"].ToString();
            string autoid = null;
            string cposition = null;
            string packName = null;
            string source = "";
            string bustype = "";
            string vouchtype = "00";

            if (type == "00")
            {
                source = "拆卸";
                bustype = "拆卸入库";
                vouchtype = "08";
            }
            else if (type == "01")
            {
                source = "拆卸";
                bustype = "拆卸出库";
                vouchtype = "09";
            }
            else if (type == "10")
            {
                source = "组装";
                bustype = "组装入库";
                vouchtype = "08";
            }
            else if (type == "11")
            {
                source = "组装";
                bustype = "组装出库";
                vouchtype = "09";
            }
            else if (type == "20")
            {
                source = "调拨";
                bustype = "调拨入库";
                vouchtype = "08";
            }
            else if (type == "21")
            {
                source = "调拨";
                bustype = "调拨出库";
                vouchtype = "09";
            }
            #endregion

            try
            {
                #region 单据日期/时间
                string dd = null, dt = null;//单据日期、时间

                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                #endregion

                #region temp table
                //delete temp
                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackTbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#LPWriteBackSumTbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                #region rdrecord
                sql = @"Update Rdrecord  WITH (UPDLOCK)  Set cHandler=N'" + handler + "', dVeriDate=N'" + dd + "',dNVerifyTime=getdate() Where cvouchtype=" + vouchtype + " and cbustype='" + bustype + "' and csource='" + source + "' and cbuscode = '" + bcode + "'";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                foreach (DataRow dr in ds.Tables["body"].Rows)
                {
                    autoid = SelSql(dr["autoid"].ToString());
                    cposition = GetNull(dr["cposition"].ToString());
                    #region rdrecords
                    sql = "update Rdrecords set cPosition =" + cposition + " where AutoID=" + autoid;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region InvPosition

                    decimal sum = 0;
                    decimal qty = decimal.Parse(dr["iquantity"].ToString());
                    foreach (DataRow drPsn in ds.Tables["Position"].Rows)
                    {
                        if (drPsn["cbatch"].ToString().Trim() == dr["cbatch"].ToString().Trim() && drPsn["cinvcode"].ToString().Trim() == dr["cinvcode"].ToString().Trim())
                        {
                            sql = "Insert Into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,"
                                + " dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,"
                                + " cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)"

                                + " Values (" + autoid + ",N'" + id + "'," + SelSql(dr["cwhcode"].ToString()) + "," + SelSql(drPsn["cposcode"].ToString()) + "," + SelSql(dr["cinvcode"].ToString()) + "," + SelSql(dr["cbatch"].ToString()) + "," + GetNull(packName) + ",Null,"
                                + " " + dateNull(dr["dvdate"].ToString()) + "," + " " + drPsn["iquantity"].ToString() + "," + mathNULL(dr["inum"].ToString()) + ",Null," + SelSql(ds.Tables["head"].Rows[0]["cmaker"].ToString()) + ","
                                + SelSql(dd) + ",1,Null,Null,Null,Null,Null,Null,Null,Null,Null," + SelSql(dr["cassunit"].ToString()) + ","
                                + " Null,Null," + dateNull(dr["dmadedate"].ToString()) + "," + mathNULL(dr["imassdate"].ToString()) + "," + SetUnit(dr["cmassunit"].ToString()) + ","
                                + " Null,2," + dateNull(dr["cexpirationdate"].ToString()) + "," + dateNull(dr["cexpirationdate"].ToString()) + ")";
                            cmd.CommandText = sql;
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                myTran.Rollback();
                                errMsg = "插入货位失败! ";
                                return -1;
                            }

                            sum += decimal.Parse(drPsn["iquantity"].ToString());
                            if (sum >= qty)
                                break;
                        }
                    }

                    #endregion

                }

                #region update trans

                sql = "insert into #LPWriteBackTbl (cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)  select cinvcode,isotype,isodid,1* iquantity,1* inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from rdrecords where isnull(isotype,0)>=4 and id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update #LPWriteBackTbl set inum = case when I.igrouptype =2 then isnull(a.inum,0) else 0 end , cfree1 = case when isnull(I.bconfigfree1,0) =0 then '' else isnull(a.cfree1,'') end , cfree2 = case when isnull(I.bconfigfree2,0) =0 then '' else isnull(a.cfree2,'') end , cfree3 = case when isnull(I.bconfigfree3,0) =0 then '' else isnull(a.cfree3,'') end , cfree4 = case when isnull(I.bconfigfree4,0) =0 then '' else isnull(a.cfree4,'') end , cfree5 = case when isnull(I.bconfigfree5,0) =0 then '' else isnull(a.cfree5,'') end , cfree6 = case when isnull(I.bconfigfree6,0) =0 then '' else isnull(a.cfree6,'') end , cfree7 = case when isnull(I.bconfigfree7,0) =0 then '' else isnull(a.cfree7,'') end , cfree8 = case when isnull(I.bconfigfree8,0) =0 then '' else isnull(a.cfree8,'') end , cfree9 = case when isnull(I.bconfigfree9,0) =0 then '' else isnull(a.cfree9,'') end , cfree10 = case when isnull(I.bconfigfree10,0) =0 then '' else isnull(a.cfree10,'') end  from #LPWriteBackTbl a inner join inventory I on a.cinvcode = I.cinvcode ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,sum(isnull(iquantity,0)) as iquantity,sum(isnull(inum,0)) as inum  into  #LPWriteBackSumTbl from #LPWriteBackTbl group by cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  create index index_lpwritebacksumtbl on #LPWriteBackSumTbl(cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 ) ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "insert into ST_DemandKeepInfo (cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,idemandtype,cdemandid )   select cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,0,0,isotype,isodid   from #LPWriteBackSumTbl a where not exists (select cinvcode from ST_DemandKeepInfo where cinvcode=a.cinvcode and idemandtype = a.isotype and cdemandid = a.isodid    and cfree1 = a.cfree1 and  cfree2 = a.cfree2 and cfree3 = a.cfree3 and cfree4 = a.cfree4 and cfree5 = a.cfree5 and cfree6 = a.cfree6    and cfree7 = a.cfree7 and cfree8 = a.cfree8 and cfree9 = a.cfree9 and cfree10 = a.cfree10 ) ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "update ST_DemandKeepInfo set iquantity =isnull(a.iquantity,0) + isnull(b.iquantity,0) ,inum =isnull(a.inum,0) + isnull(b.inum,0)    from ST_DemandKeepInfo a inner join #LPWriteBackSumTbl b on a.cinvcode =b.cinvcode and a.idemandtype = b.isotype and a.cdemandid = b.isodid  and a.cfree1 =b.cfree1 and a.cfree2 =b.cfree2 and a.cfree3 =b.cfree3 and a.cfree4 =b.cfree4 and a.cfree5 =b.cfree5 and a.cfree6 =b.cfree6  and a.cfree7 =b.cfree7 and a.cfree8 =b.cfree8 and a.cfree9 =b.cfree9 and a.cfree10 =b.cfree10 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //delete temble
                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                int abc=cmd.ExecuteNonQuery();

                sql = "if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl";
                cmd.CommandText = sql;
                abc=cmd.ExecuteNonQuery();

                sql = "exec ST_VerForStock N'08',N'" + id + "',0,1 ,1";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (spid == null)
                {
                    myTran.Rollback();
                    return -1;
                }

                sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) "
                     + "select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + "where a.transactionid=N'spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + "and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + "and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();
                #endregion

                myTran.Commit();
                return 0;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                return -1;
            }
            finally
            {
                adp.Dispose();
                cmd.Dispose();
                conn.Dispose();
                conn.Close();
            }
        }
        #endregion

        #region Replace Function

        public static string GetNull(string str)
        {
            if (string.IsNullOrEmpty(str) || (str.ToLower().Trim() == "null"))
            {
                return "Null";
            }
            return ("N'" + str.Trim() + "'");
        }

        public static string SelSql(string str)
        {
            if (string.IsNullOrEmpty(str) || (str.ToLower().Trim() == "null"))
            {
                return "N''";
            }
            else if (str.ToLower().Trim() == "true")
            {
                return "1";
            }
            else if (str.ToLower().Trim() == "false")
            {
                return "0";
            }
            return ("N'" + str.Trim() + "'");
        }

        public static string math(string str)
        {
            if (string.IsNullOrEmpty(str) || (str.ToLower().Trim() == "null"))
            {
                return "Null";
            }
            try
            {
                str = decimal.Parse(str).ToString();
            }
            catch
            {
                str = "Null";
            }
            return str.Trim();
        }

        public static string mathNULL(string str)
        {
            if (string.IsNullOrEmpty(str) || (str == "0") || (decimal.Parse(str) == 0M))
            {
                return "NULL";
            }
            try
            {
                str = decimal.Parse(str).ToString();
            }
            catch
            {
                str = "Null";
            }
            return str.Trim();
        }

        public static string dateNull(string str)
        {
            if (string.IsNullOrEmpty(str) || (str.ToLower().Trim() == "null"))
            {
                return "Null";
            }
            try
            {
                DateTime date;
                date = Convert.ToDateTime(str.Trim());
                return "N'" + date.ToString("yyyy-MM-dd") + "'";
            }
            catch
            {
                return "Null";
            }
        }

        public static string SetUnit(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "NULL";
            if (str.Trim().Length == 1 && char.IsNumber(str[0]))
                return str;
            string strUnit;
            switch (str.Trim())
            {
                case "":
                    strUnit = "0";
                    break;
                case "年":
                    strUnit = "1";
                    break;
                case "月":
                    strUnit = "2";
                    break;
                case "日":
                    strUnit = "3";
                    break;
                default:
                    strUnit = "null";
                    break;
            }
            return strUnit;
        }

        #endregion

        #region 辅助管理

        #region 批次管理

        public static int GetBatchList(string invCode, string bhCode,string whCode, string ConnectionString,out DataSet dsBatch, out string errMsg)
        {
            dsBatch = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;

            sql = "select cwhcode,cinvcode,cBatch,currentstock.cVMIVenCode,vendor.cvenname,vendor.cvenabbname,iQuantity,dVDate,dMdate,iMassDate,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10 from currentstock  left join vendor on currentstock.cVMIVenCode=vendor.cvenname where cinvcode = " + GetNull(invCode);
            if(!string.IsNullOrEmpty(bhCode))
                sql += " and cBatch = " + GetNull(bhCode);
            if (!string.IsNullOrEmpty(whCode))            
                sql += " and cwhcode = " + GetNull(whCode);
            try
            {
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsBatch);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        public static int GetBatchInfo(string invCode, string bhCode, string whCode, string ConnectionString, out DataSet dsBatch, out string errMsg)
        {
            dsBatch = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;

            sql = "select cBatch,iQuantity,iMassDate,dVDate,cExpirationdate,dMdate from CurrentStock where isnull(iQuantity,0)>0 and dVDate>getdate() ";
            if (!string.IsNullOrEmpty(invCode))
                sql += " and cinvcode = " + GetNull(invCode);
            if (!string.IsNullOrEmpty(bhCode))
                sql += " and cBatch = " + GetNull(bhCode);
            if (!string.IsNullOrEmpty(whCode))
                sql += " and cwhcode = " + GetNull(whCode);
            sql += " order by iQuantity desc";
            try
            {
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsBatch);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        #endregion

        #region 仓库管理
        public static int GetWHList( string invCode,string userName, string ConnectionString, out DataSet dsWareHouse, out string errMsg)
        {
            dsWareHouse = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;
            string ddate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet dsUserInfo = new DataSet();
            string loginer = null;
            string loginlanguage = null;
            try
            {
                sql = "select cuser_id daesysloginer,localeid daesysloginlanguage from ua_user where cuser_name = " + SelSql(userName);
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsUserInfo);
                loginer = dsUserInfo.Tables[0].Rows[0]["daesysloginer"].ToString();
                loginlanguage = dsUserInfo.Tables[0].Rows[0]["daesysloginlanguage"].ToString();

                sql = "EXEC sp_executesql N'SELECT TOP 100  cast(0 as bit) as bRefSelectColumn,[WarehouseEntity_Warehouse].[cWhCode] as cWhCode,[WarehouseEntity_Warehouse].[cWhName] as cWhName,[WarehouseEntity_Warehouse].[cDepCode] as cDepCode,[WarehouseEntity_Department].[cDepName] as cDepName "
                    + " FROM [Warehouse] AS [WarehouseEntity_Warehouse]"
                    + " LEFT JOIN [Department] AS [WarehouseEntity_Department] ON  [WarehouseEntity_Department].[cDepCode] = [WarehouseEntity_Warehouse].[cDepCode]"
                    + " WHERE 1=1   and bProxywh=0  and bWhAsset<>1  AND  (bBondedWh = (select top 1 isnull(bbondedinv,0) from inventory_sub where cinvsubcode=N''" + invCode + "'') or (select top 1 isnull(bbondedinv,0) from inventory_sub where cinvsubcode=N''" + invCode + "'')=N''1'') "
                    + " Order by cWhCode ASC ',N'@daesyslogintime nvarchar(4000),@daesysloginlanguage nvarchar(4000),@daesysloginer nvarchar(4000)',@daesyslogintime=" + SelSql(ddate) + ",@daesysloginlanguage=" + SelSql(loginlanguage) + ",@daesysloginer=" + SelSql(loginer);

                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsWareHouse);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        public static int GetWHInfo(string cWhCode, string ConnectionString, out DataSet dsWareHouse, out string errMsg)
        {
            dsWareHouse = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;
            try
            {
                sql = " select cWhCode,cWhName,cDepCode,(case bWhPos when 1 then N'TRUE' else N'FALSE' end) as bWhPos,(case bFreeze when 1 then N'TRUE' else N'FALSE' end) as bFreeze,(case bShop when 1 then N'TRUE' else N'FALSE' end) as bShop from Warehouse where cWhCode=" + SelSql(cWhCode);
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsWareHouse);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        public static decimal GetWHQuan(string cInvCode, string cBatch, string cWhCode, string ConnectionString, out string errMsg)
        {
            decimal sum = 0;
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 0;
            }
            cmd.Connection = conn;
            try
            {
                sql = "select isnull(sum(iQuantity),0) iQuantity  from currentstock where 1=1 ";
                if (!string.IsNullOrEmpty(cInvCode))
                    sql += " and cInvCode=" + SelSql(cInvCode);
                if (!string.IsNullOrEmpty(cBatch))
                    sql += " and cBatch=" + SelSql(cBatch);
                if (!string.IsNullOrEmpty(cWhCode))
                    sql += " and cWhCode=" + SelSql(cWhCode);
                cmd.CommandText = sql;
                object oSum = cmd.ExecuteScalar();
                if (oSum != DBNull.Value && oSum != null && math(oSum.ToString()) != "Null")
                    sum = decimal.Parse(math(oSum.ToString()));
                return sum;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 0;
            }
        }
        #endregion

        #region 部门管理
        public static int GetDeptList(string userName, string ConnectionString, out DataSet dsDepartment, out string errMsg)
        {
            dsDepartment = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;
            string ddate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet dsUserInfo = new DataSet();
            string loginer = null;
            string loginlanguage = null;
            try
            {
                sql = "select cuser_id daesysloginer,localeid daesysloginlanguage from ua_user where cuser_name = " + SelSql(userName);
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsUserInfo);
                loginer = dsUserInfo.Tables[0].Rows[0]["daesysloginer"].ToString();
                loginlanguage = dsUserInfo.Tables[0].Rows[0]["daesysloginlanguage"].ToString();

                sql = "EXEC sp_executesql N'SELECT TOP 100  cast(0 as bit) as bRefSelectColumn,[DepartmentEntity_Department].[cDepCode] as cDepCode,[DepartmentEntity_Department].[cDepName] as cDepName,[DepartmentEntity_Department].[cDepPerson] as cDepPerson,[DepartmentEntity_Department].[iDepGrade] as iDepGrade,[DepartmentEntity_Department].[bDepEnd] as bDepEnd,[DepartmentEntity_Department].[cDepProp] as cDepProp,[DepartmentEntity_Department].[cDepPhone] as cDepPhone,[DepartmentEntity_Department].[cDepAddress] as cDepAddress,[DepartmentEntity_Department].[cDepMemo] as cDepMemo,[DepartmentEntity_Department].[iCreLine] as iCreLine,[DepartmentEntity_Department].[cCreGrade] as cCreGrade,[DepartmentEntity_Department].[iCreDate] as iCreDate "
                    + " FROM [Department] AS [DepartmentEntity_Department]"
                    + " WHERE 1=1   and bDepEnd=1  and  (case when ISNULL([DepartmentEntity_Department].[dDepEndDate],N'''')=N'''' then ''9999-12-31'' else [DepartmentEntity_Department].[dDepEndDate] end) >  ''"+ddate+"'' "
                    + " Order by cDepCode ASC ',N'@daesyslogintime nvarchar(4000),@daesysloginlanguage nvarchar(4000),@daesysloginer nvarchar(4000)',@daesyslogintime=" + SelSql(ddate) + ",@daesysloginlanguage=" + SelSql(loginlanguage) + ",@daesysloginer=" + SelSql(loginer);

                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsDepartment);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }
        #endregion

        #region 货位管理
        public static int GetPTList(string cWhCode, string iCode, string cBatch, string iTrackID, int bRdFlag, string ConnectionString, out DataSet dsPosition, out string errMsg)
        {
            dsPosition = new DataSet();
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;
            try
            {
                sql = "select cPosCode,cPosName,iPosGrade,bPosEnd,cWhCode,iMaxCubage,iMaxWeight,cMemo,isnull(cBarCode,cPosCode) cBarCode,iQuantity,iNum,iInvExchrate from (SELECT P.cPosCode,P.cPosName, P.iPosGrade, (CASE  WHEN P.bPosEnd=0 THEN N'TRUE' ELSE N'FALSE' END) as bPosEnd,P.cWhCode, P.iMaxCubage, P.iMaxWeight,P.cMemo , P.cBarCode ,convert(decimal(20,4),sum(case when IP.brdflag=" + math(bRdFlag.ToString()) + " then IP.iquantity else -IP.iquantity end)) as iQuantity, Null as iNum ,N'' as iInvExchRate From Position P left join (select InvPosition.* from InvPosition left join vendor on InvPosition.cvmivencode=vendor.cvencode where InvPosition.cInvCode =" + SelSql(iCode) + " and isnull(InvPosition.cbatch,N'') =" + SelSql(cBatch) + " and isnull(InvPosition.itrackid,N'') =" + SelSql(iTrackID) + "  ) IP on P.cPosCode = IP.cPosCode Where P.bPosEnd=1 And P.cWhcode =" + GetNull(cWhCode) + " Group by P.cPosCode,P.cPosName, P.iPosGrade, (CASE  WHEN P.bPosEnd=0 THEN N'TRUE' ELSE N'FALSE' END),P.cWhCode, P.iMaxCubage, P.iMaxWeight,P.cMemo , P.cBarCode ) a";

                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsPosition);
                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        public static decimal GetPTQuan(string cInvCode, string cBatch, string cWhCode,string cPosCode, string ConnectionString, out string errMsg)
        {
            decimal sum = 0;
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 0;
            }
            cmd.Connection = conn;
            try
            {
                sql = "select isnull(sum(iQuantity),0) iQuantity  from InvPosition where 1=1 ";
                if (!string.IsNullOrEmpty(cInvCode))
                    sql += " and cInvCode=" + SelSql(cInvCode);
                if (!string.IsNullOrEmpty(cBatch))
                    sql += " and cBatch=" + SelSql(cBatch);
                if (!string.IsNullOrEmpty(cWhCode))
                    sql += " and cWhCode=" + SelSql(cWhCode);
                if (!string.IsNullOrEmpty(cPosCode))
                    sql += " and cPosCode=" + SelSql(cPosCode);
                cmd.CommandText = sql;
                object oSum = cmd.ExecuteScalar();
                if (oSum != DBNull.Value && oSum != null && math(oSum.ToString()) != "Null")
                    sum = decimal.Parse(math(oSum.ToString()));
                return sum;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 0;
            }
        }

        public static bool GetPTExist(string cWhCode, string cPosCode, string ConnectionString, out string errMsg)
        {
            errMsg = "";
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            cmd.Connection = conn;
            try
            {
                sql = "select distinct (case isnull(cPosCode,'') when '' then N'FALSE' else N'TRUE' end) as cPosCode from Warehouse w join Position p on w.cWhCode = p.cWhCode where w.bWhPos = 1 ";
                if (!string.IsNullOrEmpty(cWhCode))
                    sql += " and w.cWhCode=" + SelSql(cWhCode);
                if (!string.IsNullOrEmpty(cPosCode))
                    sql += " and p.cPosCode=" + SelSql(cPosCode);
                cmd.CommandText = sql;
                object exist = cmd.ExecuteScalar();
                if (!Convert.IsDBNull(exist) && exist != null)
                    return bool.Parse(exist.ToString());
                return false;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 价格管理

        /// <summary>
        /// 从补料表中取出存货最近一次单价
        /// </summary>
        /// <param name="cCode"></param>
        /// <param name="ConnectionString"></param>
        /// <param name="dsMoList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static int GetMoCost(string cCode, string ConnectionString,DataSet dsMoList, out string errMsg)
        {
            if (string.IsNullOrEmpty(cCode))
            {
                errMsg = "单据号不能为空!";
                return 1;
            }
            errMsg = "";
            DataSet dsInfo = new DataSet();
            object oCost = null;
            decimal dCost = 0;
            string connString = ConnectionString;
            string sql = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
            cmd.Connection = conn;
            try
            {
                sql = "select distinct cinvcode,cwhcode from om_momaterialsbody where moid =(select moid from om_momain where ccode=" + SelSql(cCode) + ")";
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                adp.Fill(dsInfo);

                foreach (DataRow dr in dsInfo.Tables[0].Rows)
                {
                    cmd.CommandText = "select top 1 iOutCost from IA_Subsidiary where isnull(iOutCost,0)<>0 and cwhcode=" + SelSql(dr["cwhcode"].ToString()) + " and cinvcode=" + SelSql(dr["cinvcode"].ToString()) + " order by dkeepdate desc";
                    oCost = cmd.ExecuteScalar();
                    if (oCost != DBNull.Value && oCost != null)
                        dCost = decimal.Parse(oCost.ToString());
                    else
                        dCost = 0;
                    foreach (DataRow mo in dsMoList.Tables["body"].Rows)
                    {
                        if (mo["cinvcode"].ToString() == dr["cinvcode"].ToString())
                        {
                            if (dCost != 0)
                                mo["iunitcost"] = dCost;
                            mo["iprice"] = decimal.Parse(mo["inquantity"].ToString()) * dCost;
                        }
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return 1;
            }
        }

        #endregion

        #endregion
    }
}
