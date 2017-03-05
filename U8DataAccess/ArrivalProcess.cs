using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Model;
using System.Data.SqlClient;

namespace U8DataAccess
{
    public class ArrivalProcess
    {
        #region 根据采购订单生成到货单

        public static int CreateAVOrderByPomain(string cCode, string connectionString, out DataSet OrderList, out string errMsg)
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

            strHead = "select distinct a.nflat iexchrate,b.itaxrate,a.ufts,'' as selcol,a.iflowid,a.cflowname,a.cbustype,a.cordercode,a.dpodate,a.cvencode,a.cvenabbname,a.cdepcode,a.cdepname,a.cpersoncode,a.cpersonname,a.csccode,a.cscname,a.cmemo,a.cptcode,a.cptname,a.cpaycode,a.cpayname,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a. cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a. cexch_name,a.cvenpuomprotocol,a.cvenpuomprotocolname from copypolist a join PO_Pomain b on a.cordercode=b.cpoid where  bposourcearr=1 and bservice <> 1 and binvtype<>1 and isnull(ireceivedqty,0)=0 and ((isnull(iquantity,0)>isnull(iarrqty,0) or CONVERT(DECIMAL(38,4),isnull(iquantity,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrqty,0))) or ((isnull(inum,0)>isnull(iarrnum,0) or CONVERT(DECIMAL(38,2),isnull(inum,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,2),isnull(iarrnum,0)))) and iGroupType = 2) and isnull(a.cbustype,'')<>N'直运采购' and ((isnull(a.cVerifier,'')<>'' and isnull(a.cchanger,'')='') or (isnull(a.cchangverifier,'')<>''))  AND  bInvEntrust=0 AND isnull(a.cbustype,'')=N'普通采购'  and  1=1  and a.iDiscountTaxType=0 and isnull(b.ccloser,'')='' ";
            if (!string.IsNullOrEmpty(cCode))
                strHead += " and cordercode = '" + cCode + "'";
            else
                strHead.Replace("distinct", " top 1");

            strBody = "SELECT distinct (case a.bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,(a.id) as iposid,iinvqty fKPQuantity,cordercode,isnull(iquantity,0) orderquantity,cdefine1 cVenName,cVenAbbName,'' as selcol,(a.ufts) as corufts,(a.cvencode) as cvencode,(a.nflat) as iexchrate,(a.id) as id,(a.btaxcost) as btaxcost,(a.cinvcode) as cinvcode,(a.cinvaddcode) as cinvaddcode,(a.cinvname) as cinvname,(a.cinvstd) as cinvstd,(a.cinvm_unit) as cinvm_unit,(a.imassdate) as imassdate,(isnull(iquantity,0)-isnull(iarrqty,0)) as inquantity,(isnull(a.iarrqty,0)) as fvalidinquan,(a.iinvexchrate) as iinvexchrate,(isnull(a.inum,0)) as inum,(a.iunitprice) as iOriCost,(a.itaxprice) as iOriTaxCost,(a.imoney) as iOriMoney,(a.itax) as iOriTaxPrice,(a.isum) as iOriSum,(a.iNatUnitPrice) as iCost,(a.iNatMoney) as iMoney,ivouchrowno,(a.inattax) as iTaxPrice,(a.inatsum) as iSum,(isnull(a.ipertaxrate,0)) as itaxrate,(a.iGroupType) as igrouptype,(a.bInvEntrust) as bInvEntrust,(a.cunitid) as cunitid,(a.cinva_unit) as cinva_unit,(a.cfree1) as cfree1,(a.cfree2) as cfree2,(a.cfree3) as cfree3,(a.cfree4) as cfree4,(a.cfree5) as cfree5,(a.cfree6) as cfree6,(a.cfree7) as cfree7,(a.cfree8) as cfree8,(a.cfree9) as cfree9,(a.cfree10) as cfree10,(a.cdefine22) as caddress,(a.cdefine23) as cdefine23,(a.cdefine24) as cdefine24,(a.cdefine25) as cdefine25,(a.cdefine26) as cdefine26,(a.cdefine27) as cdefine27,(a.cdefine28) as cdefine28,(a.cdefine29) as cdefine29,(a.cdefine30) as cdefine30,(a.cdefine31) as cdefine31,(a.cdefine32) as cdefine32,(a.cdefine33) as cdefine33,(a.cdefine34) as cdefine34,(a.cdefine35) as cdefine35,(a.cdefine36) as cdefine36,(a.cdefine37) as cdefine37,(a.citemcode) as citemcode,(a.citemname) as citemname,(a.citem_class) as citem_class,(a.citem_name) as citem_name,(a.cinvdefine1) as cinvdefine1,(a.cinvdefine2) as cinvdefine2,(a.cinvdefine3) as cinvdefine3,(a.cinvdefine4) as cinvdefine4,(a.cinvdefine5) as cinvdefine5,(a.cinvdefine6) as cinvdefine6,(a.cinvdefine7) as cinvdefine7,(a.cinvdefine8) as cinvdefine8,(a.cinvdefine9) as cinvdefine9,(a.cinvdefine10) as cinvdefine10,(a.cinvdefine11) as cinvdefine11,(a.cinvdefine12) as cinvdefine12,(a.cinvdefine13) as cinvdefine13,(a.cinvdefine14) as cinvdefine14,(a.cinvdefine15) as cinvdefine15,(a.cinvdefine16) as cinvdefine16,(a.contractcode) as contractcode,(a.contractrowno) as contractrowno,(a.contractrowguid) as contractrowguid,(a.irowno) as irowno,(a.csocode) as csocode,(a.sotype) as sotype,(a.sodid) as sodid,(a.cmassunit) as cmassunit,(a.cwhcode) as cwhcode,(a.cwhname) as cwhname,(a.iinvmpcost) as iinvmpcost,(a.cordercode) as cordercode,(a.darrivedate) as dArrivedate,iarrnum,(a.iorderdid) as iorderdid,(a.iordertype) as iordertype,(a.csoordercode) as csoordercode,(a.iorderseq) as iorderseq,(a.cdemandmemo) as cdemandmemo,(a.iexpiratdatecalcu) as iexpiratdatecalcu from copypolist a join inventory b on a.cinvcode=b.cinvcode where  bposourcearr=1 and a.bservice <> 1 and a.binvtype<>1 and isnull(ireceivedqty,0)=0 and ((isnull(iquantity,0)>isnull(iarrqty,0) or CONVERT(DECIMAL(38,4),isnull(iquantity,0)*(1+isnull(a.fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrqty,0))) or ((isnull(inum,0)>isnull(iarrnum,0) or CONVERT(DECIMAL(38,2),isnull(inum,0)*(1+isnull(a.fInExcess,0)))>CONVERT(DECIMAL(38,2),isnull(iarrnum,0)))) and a.iGroupType = 2) and isnull(cbustype,'')<>N'直运采购' and ((isnull(cVerifier,'')<>'' and isnull(cchanger,'')='') or (isnull(cchangverifier,'')<>''))  AND  a.bInvEntrust=0 AND isnull(cbustype,'')=N'普通采购'  and  1=1  and iDiscountTaxType=0 ";
            if (!string.IsNullOrEmpty(cCode))
                strBody += " and cordercode = '" + cCode + "'";
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
                DataSet resDs = new DataSet();
                resDs = BuildArrivalStruct();
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

        #region 根据委外订单生成到货单

        public static int CreateAVOrderByMomain(string cCode, string connectionString, out DataSet OrderList, out string errMsg)
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

            strHead = "SELECT distinct a.nflat iexchrate,a.ufts,a.cdefine22 caddress,'' as selcol,(a.ipertaxrate) as itaxrate,(a.dStartDate) as dpodate,(a.dArriveDate) as darvdate,(a.cwhcode) as cwhcode,(a.cwhname) as cwhname,(a.iarrnum) as iarrnum,(a.cdemandmemo) as cdemandmemo,(a.iexpiratdatecalcu) as iexpiratdatecalcu,(a.ivouchrowno) as ivouchrowno,(a.cCode) as cCode,(a.cbustype) as cbustype,(a.dDate) as dDate,(a.cvencode) as cvencode,(a.cvenabbname) as cvenabbname,(a.cdepcode) as cdepcode,(a.cdepname) as cdepname,(a.cpersoncode) as cpersoncode,(a.cpersonname) as cpersonname,(a.csccode) as csccode,(a.cscname) as cscname,(a.cmemo) as cmemo,(a.cptcode) as cptcode,(a.cptname) as cptname,(a.cpaycode) as cpaycode,(a.cpayname) as cpayname,(a.cdefine1) as cdefine1,(a.cdefine2) as cdefine2,(a.cdefine3) as cdefine3,(a.cdefine4) as cdefine4,(a.cdefine5) as cdefine5,(a.cdefine6) as cdefine6,(a.cdefine7) as cdefine7,(a.cdefine8) as cdefine8,(a.cdefine9) as cdefine9,(a.cdefine10) as cdefine10,(a.cdefine11) as cdefine11,(a.cdefine12) as cdefine12,(a.cdefine13) as cdefine13,(a.cdefine14) as cdefine14,(a.cdefine15) as cdefine15,(a.cdefine16) as cdefine16,(a.cexch_name) as cexch_name,(a.nflat) as iexchrate,(a.MOID) as moid from copyommolist a where isnull(ireceivedqty,0)=0 and isnull(iquantity,0)>isnull(iarrqty,0) and ((isnull(a.cVerifier,N'')<>N'' and isnull(a.cchanger,N'')=N'' and isnull(a.cchangeverifier,N'')=N'') or (isnull(a.cVerifier,N'')<>N'' and isnull(a.cchanger,N'')<>N'' and isnull(a.cchangeverifier,N'')<>N'')) and isnull(a.cbCloser,'') = '' AND  1=1 ";
            if (!string.IsNullOrEmpty(cCode))
                strHead += " and a.ccode = '" + cCode + "'";
            else
                strHead.Replace("distinct", " top 1");

            strBody = "SELECT distinct (case a.bGsp when 1 then N'TRUE' else N'FALSE' end) as bGsp,(case b.binvbatch when 1 then N'TRUE' else N'FALSE' end) as binvbatch,a.nflat iexchrate,a.ufts,a.cdefine22 caddress,iinvqty fKPQuantity,a.cvencode,c.cVenName,c.cVenAbbName,a.ccode cordercode,a.MODetailsID iPosID,'' as selcol,(a.ufts) as corufts,(a.MODetailsID) as modetailsid,(a.btaxcost) as btaxcost,(a.cinvcode) as cinvcode,(a.cinvaddcode) as cinvaddcode,(a.cinvname) as cinvname,(a.cinvstd) as cinvstd,(a.cinvm_unit) as cinvm_unit,(a.bgsp) as bgsp,(a.imassdate) as imassdate,(isnull(a.iquantity,0))-(isnull(a.iarrqty,0)) as inquantity,(isnull(a.iquantity,0)) as orderquantity,(isnull(a.iarrqty,0)) as fvalidinquan,(a.iinvexchrate) as iinvexchrate,(isnull(a.inum,0)) as inum,(a.iunitprice) as iOriCost,(a.itaxprice) as iOriTaxCost,(a.imoney) as iOriMoney,(a.itax) as iOriTaxPrice,(a.isum) as iOriSum,(a.iNatUnitPrice) as iCost,(a.iNatMoney) as iMoney,ivouchrowno,(a.inattax) as iTaxPrice,(a.iNatSum) as iSum,(isnull(a.iquantity,0)) as iquantity,(isnull(a.iarrqty,0)) as iarrqty,(isnull(a.iinvexchrate,0)) as iinvexchrate,(isnull(a.inum,0)) as inum,(a.iunitprice) as iunitprice,(a.itax) as itax,(a.inatunitprice) as inatunitprice,(a.inatmoney) as inatmoney,(a.inattax) as inattax,(a.inatsum) as inatsum,(a.iGroupType) as igrouptype,(a.bInvEntrust) as bInvEntrust,(a.cunitid) as cunitid,(a.cinva_unit) as cinva_unit,(a.cfree1) as cfree1,(a.cfree2) as cfree2,(a.cfree3) as cfree3,(a.cfree4) as cfree4,(a.cfree5) as cfree5,(a.cfree6) as cfree6,(a.cfree7) as cfree7,(a.cfree8) as cfree8,(a.cfree9) as cfree9,(a.cfree10) as cfree10,(a.cdefine22) as cdefine22,(a.cdefine23) as cdefine23,(a.cdefine24) as cdefine24,(a.cdefine25) as cdefine25,(a.cdefine26) as cdefine26,(a.cdefine27) as cdefine27,(a.cdefine28) as cdefine28,(a.cdefine29) as cdefine29,(a.cdefine30) as cdefine30,(a.cdefine31) as cdefine31,(a.cdefine32) as cdefine32,(a.cdefine33) as cdefine33,(a.cdefine34) as cdefine34,(a.cdefine35) as cdefine35,(a.cdefine36) as cdefine36,(a.cdefine37) as cdefine37,(a.citemcode) as citemcode,(a.citemname) as citemname,(a.citem_class) as citem_class,(a.citem_name) as citem_name,(a.cinvdefine1) as cinvdefine1,(a.cinvdefine2) as cinvdefine2,(a.cinvdefine3) as cinvdefine3,(a.cinvdefine4) as cinvdefine4,(a.cinvdefine5) as cinvdefine5,(a.cinvdefine6) as cinvdefine6,(a.cinvdefine7) as cinvdefine7,(a.cinvdefine8) as cinvdefine8,(a.cinvdefine9) as cinvdefine9,(a.cinvdefine10) as cinvdefine10,(a.cinvdefine11) as cinvdefine11,(a.cinvdefine12) as cinvdefine12,(a.cinvdefine13) as cinvdefine13,(a.cinvdefine14) as cinvdefine14,(a.cinvdefine15) as cinvdefine15,(a.cinvdefine16) as cinvdefine16,(a.SOType) as sotype,(a.sodid) as sodid,(a.iinvqty) as iinvqty,(a.csocode) as csocode,(a.irowno) as irowno,(a.cmassunit) as cmassunit,(a.dStartDate) as dStartDate,(a.dArriveDate) as dArriveDate,(a.cwhcode) as cwhcode,(a.cwhname) as cwhname,(a.cCode) as cCode,(a.ipertaxrate) as itaxrate,(a.iarrnum) as iarrnum,(a.cdemandmemo) as cdemandmemo,(a.iexpiratdatecalcu) as iexpiratdatecalcu,(a.ivouchrowno) as iorderrowno from copyommolist a join inventory b on a.cinvcode=b.cinvcode join Vendor c on a.cvencode=c.cvencode where isnull(ireceivedqty,0)=0 and isnull(iquantity,0)>isnull(iarrqty,0) and ((isnull(a.cVerifier,N'')<>N'' and isnull(a.cchanger,N'')=N'' and isnull(a.cchangeverifier,N'')=N'') or (isnull(a.cVerifier,N'')<>N'' and isnull(a.cchanger,N'')<>N'' and isnull(a.cchangeverifier,N'')<>N'')) and isnull(a.cbCloser,'') = '' AND  1=1 ";
            if (!string.IsNullOrEmpty(cCode))
                strBody += " and a.ccode = '" + cCode + "'";
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
                DataSet resDs = new DataSet();
                resDs = BuildArrivalStruct();
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

        #region 采购到货单保存 --by 采购订单

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 20130407修改单据编号规则</remarks>
        public static int SaveByPomain(DataSet ds, string connectionString, string accid, string year, out string errMsg)
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
            int iVouchRow = 0;
            string sql = null;
            string spid = null;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string user = null;
            string cSeed = null;
            string cGsp = null;
            bool bGsp = false;
            string strAutoId = null;

            try
            {
                #region 处理参数

                string dd = null, dt = null;//单据日期、时间
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cGsp = ds.Tables["Body"].Rows[0]["bGsp"].ToString();
                if (cGsp == "是" || cGsp == "1" || cGsp.ToUpper() == "TRUE")
                    bGsp = true;
                string ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ddate"]).ToString("yyyyMMdd");
                /*string tempSeed = ds.Tables[0].Rows[0]["cmaker"].ToString() + ddate + "0";*/
                //cmd.CommandText = "select cDept from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                //ds.Tables[0].Rows[0]["cdepcode"] = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                string cVouchName = "到货单";
                string cardnumber, vt_id;//单据类型编码 模板号
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                string cPtName = "普通采购";
                cmd.CommandText = "select cptcode from PurchaseType where cPTName ='" + cPtName + "'";
                ds.Tables[0].Rows[0]["cptcode"] = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select max(ifatherid) ifatherid,max(ichildid) ichildid from UFSystem..UA_Identity where cvouchtype='PUARRIVAL' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar() == null ? "null" : cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;
                /*
                cmd.CommandText = "select isnull(max(convert(decimal,cnumber)),0) from voucherhistory where cardnumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed = " + GetNull(tempSeed);
                //cmd.CommandText = "select isnull(max(convert(decimal,right(ccode,4))),0) from pu_arrivalvouch where substring(substring(ccode,4,13),1,8)=" + SelSql(ddate) ;
                ocode = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();//获得ccode的流水号
                if (ocode == null)
                    icode = 1;
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString();

                 * 
                 */

                if (id < 10000000)
                    id += 860000000;    //暂定
                if (autoid < 10000000)
                    autoid += 860000000;    //暂定
                object isnull = null;
                do
                {
                    id = id + 1;
                    cmd.CommandText = "select id from PU_ArrivalVouch where id =" + id;
                    isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                }
                while (isnull != null);
                string month = DateTime.Now.Month.ToString("MM");
                #endregion

                #region TempTable
                sql = "insert into SCM_EntryLedgerBuffer ([Subject],iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select [Subject],-1*iNum,-1*iQuantity,0,0,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId =0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                #region PU_ArrivalVouch
                sql = "insert into PU_ArrivalVouch(ivtid,id,ccode,ddate,cvencode,cptcode,cdepcode,cpersoncode,cpaycode,csccode,cexch_name,iexchrate,itaxrate,cmemo,cbustype,cmaker,bnegative,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,ccloser,idiscounttaxtype,ibilltype,cvouchtype,cgeneralordercode,ctmcode,cincotermcode,ctransordercode,dportdate,csportcode,caportcode,csvencode,carrivalplace,dclosedate,idec,bcal,guid,iverifystate,cauditdate,caudittime,cverifier,iverifystateex,ireturncount,iswfcontrolled,cvenpuomprotocol,cchanger,iflowid) " +
                    "values(@ivtid,@Id,@ccode,@ddate,@cvencode,@cptcode,@cdepcode,@cpersoncode,@cpaycode,@csccode,@cexch_name,@iexchrate,@itaxrate,@cmemo,@cbustype,@cmaker,@bnegative,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,@ccloser,@idiscounttaxtype,@ibilltype,@cvouchtype,@cgeneralordercode,@ctmcode,@cincotermcode,@ctransordercode,@dportdate,@csportcode,@caportcode,@csvencode,@carrivalplace,@dclosedate,@idec,@bcal,@guid,@iVerifyState,@cauditdate,@caudittime,@cverifier,@iverifystateex,@ireturncount,@iswfcontrolled,@cvenpuomprotocol,@cchanger,@iflowid)";
                #region SQL_Replace

                sql = sql.Replace("@ivtid", vt_id);
                sql = sql.Replace("@Id", id.ToString());
                sql = sql.Replace("@ccode", id.ToString());//SelSql(tempSeed + ccode.PadLeft(4, '0')));next update
                sql = sql.Replace("@ddate", GetNull(dd));
                sql = sql.Replace("@darvdate", dateNull(ds.Tables[0].Rows[0]["darvdate"].ToString()));
                sql = sql.Replace("@dnmaketime", GetNull(dt));
                sql = sql.Replace("@chandler", GetNull(""));
                sql = sql.Replace("@controlresult", SelSql("-1"));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cptcode", SelSql(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cdepcode", SelSql(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@cpaycode", GetNull(ds.Tables[0].Rows[0]["cpaycode"].ToString()));
                sql = sql.Replace("@csccode", GetNull(ds.Tables[0].Rows[0]["csccode"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@iexchrate", math(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@itaxrate", math(ds.Tables[0].Rows[0]["itaxrate"].ToString()));
                sql = sql.Replace("@cmemo", SelSql(ds.Tables[0].Rows[0]["cmemo"].ToString()));
                sql = sql.Replace("@cbustype", SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bnegative", math(ds.Tables[0].Rows[0]["bnegative"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", mathNULL(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", mathNULL(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull(ds.Tables[0].Rows[0]["cdefine9"].ToString()));
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine11"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", mathNULL(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", mathNULL(ds.Tables[0].Rows[0]["cdefine16"].ToString()));
                sql = sql.Replace("@ccloser", SelSql(ds.Tables[0].Rows[0]["ccloser"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@ibilltype", SelSql(ds.Tables[0].Rows[0]["ibilltype"].ToString()));
                sql = sql.Replace("@cvouchtype", SelSql(ds.Tables[0].Rows[0]["cvouchtype"].ToString()));
                sql = sql.Replace("@cgeneralordercode", SelSql(ds.Tables[0].Rows[0]["cgeneralordercode"].ToString()));
                sql = sql.Replace("@ctmcode", SelSql(ds.Tables[0].Rows[0]["ctmcode"].ToString()));
                sql = sql.Replace("@cincotermcode", GetNull(ds.Tables[0].Rows[0]["cincotermcode"].ToString()));
                sql = sql.Replace("@ctransordercode", GetNull(ds.Tables[0].Rows[0]["ctransordercode"].ToString()));
                sql = sql.Replace("@dportdate", dateNull(ds.Tables[0].Rows[0]["dportdate"].ToString()));
                sql = sql.Replace("@csportcode", SelSql(ds.Tables[0].Rows[0]["csportcode"].ToString()));
                sql = sql.Replace("@caportcode", SelSql(ds.Tables[0].Rows[0]["caportcode"].ToString()));
                sql = sql.Replace("@csvencode", SelSql(ds.Tables[0].Rows[0]["csvencode"].ToString()));
                sql = sql.Replace("@carrivalplace", SelSql(ds.Tables[0].Rows[0]["carrivalplace"].ToString()));
                sql = sql.Replace("@dclosedate", dateNull(ds.Tables[0].Rows[0]["dclosedate"].ToString()));
                sql = sql.Replace("@idec", math(ds.Tables[0].Rows[0]["idec"].ToString()));
                sql = sql.Replace("@bcal", GetNull(ds.Tables[0].Rows[0]["bcal"].ToString()));
                sql = sql.Replace("@guid", SelSql(ds.Tables[0].Rows[0]["guid"].ToString()));
                sql = sql.Replace("@ireturncount", math(ds.Tables[0].Rows[0]["ireturncount"].ToString()));
                sql = sql.Replace("@iswfcontrolled", GetNull(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@cvenpuomprotocol", GetNull(ds.Tables[0].Rows[0]["cvenpuomprotocol"].ToString()));
                sql = sql.Replace("@cchanger", SelSql(ds.Tables[0].Rows[0]["cchanger"].ToString()));
                sql = sql.Replace("@iflowid", mathNULL(ds.Tables[0].Rows[0]["iflowid"].ToString()));
                sql = sql.Replace("@cverifier", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString())); //审核人
                sql = sql.Replace("@dnverifytime", GetNull(dt));
                sql = sql.Replace("@dveridate", GetNull(dd));
                sql = sql.Replace("@cauditdate", GetNull(dd));      //审核日期
                sql = sql.Replace("@caudittime", GetNull(dt));      //审核时间
                sql = sql.Replace("@iVerifyState", GetNull(""));    //审核状态
                sql = sql.Replace("@iverifystateex", GetNull("2")); //工作流审核状态
                #endregion

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                #region TempTable
                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"Create table TmpPU_A_ArrivalVouchs_Detail
                        (
                        iSourceautoid int,
                        iArrqty decimal(38,4),
                        iArrnum decimal(38,2),
                        iArrmoney decimal(38,2),
                        iArrnatmoney decimal(38,2),
                        fPoValidQuantity decimal(38,4),
                        fPoValidNum decimal(38,2),
                        fPoArrQuantity decimal(38,4),
                        fPoArrNum decimal(38,2),
                        fPoRetQuantity decimal(38,4),
                        fPoRetNum decimal(38,2),
                        fPoRefuseQuantity decimal(38,4),
                        fPoRefuseNum decimal(38,2),
                        sotype nvarchar (10),
                        ufts money
                        )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                foreach (DataRow dr in ds.Tables["Body"].Rows)
                {
                    isnull = null;
                    do
                    {
                        autoid = autoid + 1;
                        cmd.CommandText = "select autoid from PU_ArrivalVouchs where autoid =" + autoid;
                        isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                    }
                    while (isnull != null);

                    #region PU_ArrivalVouchs
                    sql = "Insert Into PU_ArrivalVouchs(autoid,id,cwhcode,cinvcode,inum,iquantity,ioricost,ioritaxcost,iorimoney,ioritaxprice,iorisum,icost,imoney,itaxprice,isum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,itaxrate,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,citem_class,citemcode,iposid,citemname,cunitid,fkpquantity,frealquantity,fValidQuantity,fvalidInQuan,finvalidquantity,ccloser,icorid,bgsp,cbatch,dvdate,dpdate,frefusequantity,cgspstate,fvalidnum,finvalidnum,frealnum,btaxcost,binspect,frefusenum,ippartid,ipquantity,iptoseq,sodid,sotype,contractrowguid,imassdate,cmassunit,bexigency,cbcloser,fdtquantity,finvalidinnum,fdegradequantity,fdegradenum,fdegradeinquantity,fdegradeinnum,finspectquantity,finspectnum,iinvmpcost,guids,iinvexchrate,objectid_source,autoid_source,ufts_source,irowno_source,csocode,isorowno,iorderid,cordercode,iorderrowno,dlineclosedate,contractcode,contractrowno,rejectsource,iciqbookid,cciqbookcode,cciqcode,fciqchangrate,irejectautoid,iexpiratdatecalcu,cexpirationdate,dexpirationdate,cupsocode,iorderdid,iordertype,csoordercode,iorderseq,cbatchproperty1,cbatchproperty2,cbatchproperty3,cbatchproperty4,cbatchproperty5,cbatchproperty6,cbatchproperty7,cbatchproperty8,cbatchproperty9,cbatchproperty10,ivouchrowno)"
                        + "values(@Autoid,@Id,@cwhcode,@cinvcode,@inum,@iquantity,@ioricost,@ioritaxcost,@ioriMoney,@ioritaxprice,@iorisum,@icost,@imoney,@itaxprice,@isum,@Cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@itaxrate,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@citem_class,@citemcode,@iposid,@citemname,@cunitid,@fkpquantity,@frealquantity,@fValidQuantity,@fvalidInQuan,@finvalidquantity,@ccloser,@icorid,@bgsp,@cbatch,@dvdate,@dpdate,@frefusequantity,@cgspstate,@fvalidnum,@finvalidnum,@frealnum,@btaxcost,@binspect,@frefusenum,@ippartid,@ipquantity,@iptoseq,@sodid,@sotype,@contractrowguid,@imassdate,@cmassunit,@bexigency,@cbcloser,@fdtquantity,@finvalidinnum,@fdegradequantity,@fdegradenum,@fdegradeinquantity,@fdegradeinnum,@finspectquantity,@finspectnum,@iinvmpcost,@guids,@iinvexchrate,@objectid_source,@autoid_source,@ufts_source,@irowno_source,@csocode,@isorowno,@iorderid,@cordercode,@iorderrowno,@dlineclosedate,@contractcode,@contractrowno,@rejectsource,@iciqbookid,@cciqbookcode,@cciqcode,@fciqchangrate,@irejectautoid,@iexpiratdatecalcu,@cexpirationdate,@dexpirationdate,@cupsocode,@iorderdid,@iordertype,@csoordercode,@iorderseq,@Cbatchproperty1,@cbatchproperty2,@cbatchproperty3,@cbatchproperty4,@cbatchproperty5,@cbatchproperty6,@cbatchproperty7,@cbatchproperty8,@cbatchproperty9,@cbatchproperty10,@ivouchrowno)";
                    #region SQL_Replace

                    sql = sql.Replace("@Autoid", autoid.ToString());
                    sql = sql.Replace("@Id", id.ToString());
                    sql = sql.Replace("@inquantity", math("0"));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@iordertype", mathNULL("0"));
                    sql = sql.Replace("@cwhcode", GetNull(dr["cwhcode"].ToString()));
                    sql = sql.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", math(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@imoney", math(dr["imoney"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioriMoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@icost", math(dr["icost"].ToString()));
                    sql = sql.Replace("@itaxprice", math(dr["itaxprice"].ToString()));
                    sql = sql.Replace("@isum", math(dr["isum"].ToString()));
                    sql = sql.Replace("@Cfree1", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@itaxrate", mathNULL(dr["itaxrate"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["caddress"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", mathNULL(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", mathNULL(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", mathNULL(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", mathNULL(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", dateNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", dateNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@Cbatchproperty1", GetNull(dr["cbatchproperty1"].ToString()));
                    sql = sql.Replace("@cbatchproperty2", GetNull(dr["cbatchproperty2"].ToString()));
                    sql = sql.Replace("@cbatchproperty3", GetNull(dr["cbatchproperty3"].ToString()));
                    sql = sql.Replace("@cbatchproperty4", GetNull(dr["cbatchproperty4"].ToString()));
                    sql = sql.Replace("@cbatchproperty5", GetNull(dr["cbatchproperty5"].ToString()));
                    sql = sql.Replace("@cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    sql = sql.Replace("@cbatchproperty7", GetNull(dr["cbatchproperty7"].ToString()));
                    sql = sql.Replace("@cbatchproperty8", GetNull(dr["cbatchproperty8"].ToString()));
                    sql = sql.Replace("@cbatchproperty9", GetNull(dr["cbatchproperty9"].ToString()));
                    sql = sql.Replace("@cbatchproperty10", GetNull(dr["cbatchproperty10"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", mathNULL(dr["iposid"].ToString()));
                    sql = sql.Replace("@citemname", GetNull(dr["citemname"].ToString()));
                    sql = sql.Replace("@cunitid", GetNull(dr["cunitid"].ToString()));
                    sql = sql.Replace("@fkpquantity", mathNULL(dr["fkpquantity"].ToString()));
                    sql = sql.Replace("@frealquantity", mathNULL(dr["frealquantity"].ToString()));//实收数量
                    sql = sql.Replace("@fValidQuantity", mathNULL(dr["fValidQuantity"].ToString()));//合格数量
                    sql = sql.Replace("@finvalidquantity", mathNULL("0"));//不合格数量
                    sql = sql.Replace("@fvalidInQuan", math("0"));//合格品入库数量
                    sql = sql.Replace("@ccloser", GetNull(dr["ccloser"].ToString()));
                    sql = sql.Replace("@icorid", mathNULL(dr["icorid"].ToString()));
                    sql = sql.Replace("@bgsp", SelSql(dr["bgsp"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@dvdate", dateNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@dpdate", dateNull(dr["dpdate"].ToString()));
                    sql = sql.Replace("@frefusequantity", GetNull(dr["frefusequantity"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));
                    sql = sql.Replace("@fvalidnum", GetNull(dr["fvalidnum"].ToString()));
                    sql = sql.Replace("@finvalidnum", GetNull(dr["finvalidnum"].ToString()));
                    sql = sql.Replace("@frealnum", GetNull(dr["frealnum"].ToString()));
                    sql = sql.Replace("@btaxcost", GetNull(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@binspect", GetNull(dr["binspect"].ToString()));
                    sql = sql.Replace("@frefusenum", mathNULL(dr["frefusenum"].ToString()));
                    sql = sql.Replace("@ippartid", mathNULL(dr["ippartid"].ToString()));
                    sql = sql.Replace("@ipquantity", mathNULL(dr["ipquantity"].ToString()));
                    sql = sql.Replace("@iptoseq", mathNULL(dr["iptoseq"].ToString()));
                    sql = sql.Replace("@sodid", GetNull(dr["sodid"].ToString()));
                    sql = sql.Replace("@sotype", GetNull(dr["sotype"].ToString()));
                    sql = sql.Replace("@contractrowguid", GetNull(dr["contractrowguid"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cmassunit", GetNull(dr["cmassunit"].ToString()));
                    sql = sql.Replace("@bexigency", GetNull(dr["bexigency"].ToString()));
                    sql = sql.Replace("@cbcloser", GetNull(dr["cbcloser"].ToString()));
                    sql = sql.Replace("@fdtquantity", GetNull(dr["fdtquantity"].ToString()));
                    sql = sql.Replace("@finvalidinnum", GetNull(dr["finvalidinnum"].ToString()));
                    sql = sql.Replace("@fdegradequantity", GetNull(dr["fdegradequantity"].ToString()));
                    sql = sql.Replace("@fdegradenum", GetNull(dr["fdegradenum"].ToString()));
                    sql = sql.Replace("@fdegradeinquantity", GetNull(dr["fdegradeinquantity"].ToString()));
                    sql = sql.Replace("@fdegradeinnum", GetNull(dr["fdegradeinnum"].ToString()));
                    sql = sql.Replace("@finspectquantity", math("0"));
                    sql = sql.Replace("@finspectnum", GetNull(dr["finspectnum"].ToString()));
                    sql = sql.Replace("@iinvmpcost", GetNull(dr["iinvmpcost"].ToString()));
                    sql = sql.Replace("@guids", GetNull(dr["guids"].ToString()));
                    sql = sql.Replace("@iinvexchrate", GetNull(dr["iinvexchrate"].ToString()));
                    sql = sql.Replace("@objectid_source", GetNull(dr["objectid_source"].ToString()));
                    sql = sql.Replace("@autoid_source", mathNULL(dr["autoid_source"].ToString()));
                    sql = sql.Replace("@ufts_source", GetNull(dr["ufts_source"].ToString()));
                    sql = sql.Replace("@irowno_source", mathNULL(dr["irowno_source"].ToString()));
                    sql = sql.Replace("@csocode", GetNull(dr["csocode"].ToString()));
                    sql = sql.Replace("@isorowno", mathNULL(dr["isorowno"].ToString()));
                    sql = sql.Replace("@iorderid", mathNULL(dr["iorderid"].ToString()));
                    sql = sql.Replace("@cordercode", GetNull(dr["cordercode"].ToString()));
                    sql = sql.Replace("@iorderrowno", mathNULL(dr["iorderrowno"].ToString()));
                    sql = sql.Replace("@dlineclosedate", dateNull(dr["dlineclosedate"].ToString()));
                    sql = sql.Replace("@contractcode", GetNull(dr["contractcode"].ToString()));
                    sql = sql.Replace("@contractrowno", GetNull(dr["contractrowno"].ToString()));
                    sql = sql.Replace("@rejectsource", GetNull(dr["rejectsource"].ToString()));
                    sql = sql.Replace("@iciqbookid", mathNULL(dr["iciqbookid"].ToString()));
                    sql = sql.Replace("@cciqbookcode", GetNull(dr["cciqbookcode"].ToString()));
                    sql = sql.Replace("@cciqcode", GetNull(dr["cciqcode"].ToString()));
                    sql = sql.Replace("@fciqchangrate", GetNull(dr["fciqchangrate"].ToString()));
                    sql = sql.Replace("@irejectautoid", GetNull(dr["irejectautoid"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", mathNULL(dr["iexpiratdatecalcu"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", dateNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@cupsocode", GetNull(dr["cupsocode"].ToString()));
                    sql = sql.Replace("@iorderdid", mathNULL(dr["iorderdid"].ToString()));
                    sql = sql.Replace("@iordertype", GetNull(dr["iordertype"].ToString()));
                    sql = sql.Replace("@csoordercode", GetNull(dr["csoordercode"].ToString()));
                    sql = sql.Replace("@iorderseq", mathNULL(dr["iorderseq"].ToString()));
                    sql = sql.Replace("@ivouchrowno", mathNULL((++iVouchRow).ToString()));
                    #endregion

                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    if (string.IsNullOrEmpty(strAutoId))
                        strAutoId = autoid.ToString();
                    else
                        strAutoId += "," + autoid.ToString();
                    #endregion

                    #region PO_Podetails
                    sql = "update po_podetails set iArrQTY=isnull(iArrQTY,0)+@iArrQTY,iArrNum=isnull(iArrNum,0)+@iArrNum,iArrMoney=isnull(iArrMoney,0)+@iArrMoney,iNatArrMoney=isnull(iNatArrMoney,0)+@iNatArrMoney,fPoValidQuantity=isnull(fPoValidQuantity,0)+@fPoValidQuantity,fPoValidNum=isnull(fPoValidNum,0)+@fPoValidNum,fPoArrQuantity=isnull(fPoArrQuantity,0)+@fPoArrQuantity,fPoArrNum=isnull(fPoArrNum,0)+@fPoArrNum,fPoRetQuantity=isnull(fPoRetQuantity,0)+@fPoRetQuantity,fPoRetNum=isnull(fPoRetNum,0)+@fPoRetNum,fPoRefuseQuantity=isnull(fPoRefuseQuantity,0)+@fPoRefuseQuantity,fPoRefuseNum=isnull(iArrQTY,0)+@fPoRefuseNum where poid=(select poid from PO_Pomain where cPOID = @cPOID) and cInvCode= @cInvCode and ID=@iPOsID ";
                    #region SQL_Replace
                    sql = sql.Replace("@cInvCode", GetNull(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@cPOID", GetNull(dr["cordercode"].ToString()));
                    sql = sql.Replace("@iPOsID", GetNull(dr["iPOsID"].ToString()));
                    sql = sql.Replace("@fPoRefuseNum", math("0"));
                    sql = sql.Replace("@fPoRefuseQuantity", math(dr["fRefuseQuantity"].ToString()));//退货数量
                    sql = sql.Replace("@fPoRetNum", math("0"));//退货件数
                    sql = sql.Replace("@fPoRetQuantity", math(dr["fRefuseQuantity"].ToString()));//拒收数量
                    sql = sql.Replace("@fPoArrNum", math(dr["iNum"].ToString()));
                    sql = sql.Replace("@fPoArrQuantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@fPoValidNum", math("0"));   //合格件数
                    sql = sql.Replace("@fPoValidQuantity", math("0"));//合格数量
                    sql = sql.Replace("@iNatArrMoney", math(dr["iSum"].ToString()));
                    sql = sql.Replace("@iArrMoney", math(dr["iOriSum"].ToString()));
                    sql = sql.Replace("@iArrNum", math(dr["iNum"].ToString()));
                    sql = sql.Replace("@iArrQTY", math(dr["iQuantity"].ToString()));
                    #endregion
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region TmpPU_A_ArrivalVouchs_Detail
                    if (bGsp)
                    {
                        sql = "insert into TmpPU_A_ArrivalVouchs_Detail(iSourceautoid,iArrqty,iArrnum,iArrmoney,iArrnatmoney,fPoValidQuantity,fPoValidNum,fPoArrQuantity,fPoArrNum,fPoRetQuantity,fPoRetNum,fPoRefuseQuantity,fPoRefuseNum,sotype)"
                            + "values(@iSourceautoid,@iArrqty,@iArrnum,@iArrmoney,@iArrnatmoney,@fPoValidQuantity,@fPoValidNum,@fPoArrQuantity,@fPoArrNum,@fPoRetQuantity,@fPoRetNum,@fPoRefuseQuantity,@fPoRefuseNum,@sotype)";
                        #region SQL_Replace
                        sql = sql.Replace("@iSourceautoid", mathNULL(dr["iposid"].ToString()));
                        sql = sql.Replace("@iArrqty", math(dr["iquantity"].ToString()));
                        sql = sql.Replace("@iArrnum", math(dr["inum"].ToString()));
                        sql = sql.Replace("@iArrmoney", math(dr["iorisum"].ToString()));
                        sql = sql.Replace("@iArrnatmoney", math(dr["iorisum"].ToString()));
                        sql = sql.Replace("@fPoValidQuantity", math("0"));
                        sql = sql.Replace("@fPoValidNum", math("0"));
                        sql = sql.Replace("@fPoArrQuantity", math(dr["iquantity"].ToString()));
                        sql = sql.Replace("@fPoArrNum", math(dr["inum"].ToString()));
                        sql = sql.Replace("@fPoRetQuantity", math("0"));
                        sql = sql.Replace("@fPoRetNum", math("0"));
                        sql = sql.Replace("@fPoRefuseQuantity", math("0"));
                        sql = sql.Replace("@fPoRefuseNum", math("0"));
                        sql = sql.Replace("@sotype", SelSql("po"));
                        #endregion

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    #endregion
                }
                #region TempTable

                #region TmpPU_B_ArrivalVouchs_Detail
                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select isourceautoid,sum(T.iArrqty) as iArrqty,sum(T.iArrnum) as iArrnum,sum(T.iArrmoney) as iArrmoney,sum(T.iArrnatmoney) as iArrnatmoney,sum(T.fPoValidQuantity) as fPoValidQuantity,sum(T.fPoValidNum) as fPoValidNum,sum(T.fPoArrQuantity) as fPoArrQuantity,sum(T.fPoArrNum) as fPoArrNum,sum(T.fPoRetQuantity) as fPoRetQuantity,sum(T.fPoRetNum) as fPoRetNum,sum(T.fPoRefuseQuantity) as fPoRefuseQuantity,sum(T.fPoRefuseNum) as fPoRefuseNum,sotype,min(ufts) as ufts into TmpPU_B_ArrivalVouchs_Detail from TmpPU_A_ArrivalVouchs_Detail as T group by isourceautoid,sotype ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,1*iNum,1*iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId = " + id.ToString();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                int abc;
                #region UpdateTrans

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(spid))
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

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'PU',1,1,0,1,0,0,1,0,1,0,0,0,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                #region VoucherHistory

                sql = "select cUser_id from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                cmd.CommandText = sql;
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();

                cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed=" + SelSql(user);
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                if (user.Length > 3)
                    user = user.Substring(0, 3);
                else
                    user = user.PadLeft(3, '0');
                
                /*
                cSeed = user + ddate + "0";                
                sql = "IF NOT EXISTS(select cNumber as Maxnumber From VoucherHistory  with (XLOCK) Where  CardNumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed='" + tempSeed + "') "
                    + "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','操作员|单据日期|红蓝标志','','" + tempSeed + "','" + icode + "')";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "update VoucherHistory set cNumber='" + icode + "' Where  CardNumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed='" + tempSeed + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cSeed = cSeed + icode.ToString().PadLeft(4, '0');
                if (cSeed.Length > 16)
                {
                    myTran.Rollback();
                    return -1;
                }
                 * */
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='单据日期' and cSeed='{1}'",cardnumber, cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('{0}','单据日期','月','{1}','1')",cardnumber, cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent='单据日期' and cSeed='{2}'", icode,cardnumber, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }

                //采购到货的流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                cmd.CommandText = string.Format("Update pu_arrivalvouch Set cCode='{0}' where id={1}",ccode,id);//"Update pu_arrivalvouch Set cCode = " + SelSql(cSeed) + " Where Id = " + id.ToString();
                abc = cmd.ExecuteNonQuery();

                cmd.CommandText = "exec Scm_SaveBatchProperty '" + strAutoId + "','autoid','pu_arrivalvouchs','" + user + "'";
                abc = cmd.ExecuteNonQuery();

                if (id > 10000000)
                    id -= 860000000;    //暂定
                if (autoid > 10000000)
                    autoid -= 860000000;    //暂定
                //UA_Identity
                cmd.CommandText = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + "  where cvouchtype='PUARRIVAL' and cAcc_id='" + accid + "'";
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

        #region 委外到货单保存 --by 委外订单

        public static int SaveByMomain(DataSet ds, string connectionString, string accid, string year, out string errMsg)
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
            int iVouchRow = 0;
            string sql = null;
            string spid = null;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            string user = null;
            string cSeed = null;
            string cGsp = null;
            bool bGsp = false;
            string strAutoId = null;

            try
            {
                #region 处理参数

                string dd = null, dt = null;//单据日期、时间
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cGsp = ds.Tables["Body"].Rows[0]["bGsp"].ToString();
                if (cGsp == "是" || cGsp == "1" || cGsp.ToUpper() == "TRUE")
                    bGsp = true;
                string ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ddate"]).ToString("yyyyMMdd");
                //string tempSeed = ds.Tables[0].Rows[0]["cmaker"].ToString() + ddate + "0";
                //cmd.CommandText = "select cDept from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                //ds.Tables[0].Rows[0]["cdepcode"] = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                string cVouchName = "到货单";
                string cardnumber, vt_id;//单据类型编码 模板号
                DataSet Vouchers = new DataSet();
                cmd.CommandText = @"select def_id,cardnumber from Vouchers where ccardname='" + cVouchName + "'";
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                string cPtName = "加工入库";
                cmd.CommandText = "select cptcode from PurchaseType where cPTName ='" + cPtName + "'";
                ds.Tables[0].Rows[0]["cptcode"] = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select max(ifatherid) ifatherid,max(ichildid) ichildid from UFSystem..UA_Identity where cvouchtype='PUARRIVAL' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                int id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                int autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar() == null ? "null" : cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                ds.Tables["head"].Rows[0]["cvouchtype"] = cvouchtype;
                
                /*
                cmd.CommandText = "select isnull(max(convert(decimal,cnumber)),0) from voucherhistory where cardnumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed = " + GetNull(tempSeed);
                //cmd.CommandText = "select isnull(max(convert(decimal,right(ccode,4))),0) from pu_arrivalvouch where substring(substring(ccode,4,13),1,8)=" + SelSql(ddate) ;
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

                if (id < 10000000)
                    id += 860000000;    //暂定
                if (autoid < 10000000)
                    autoid += 860000000;    //暂定
                object isnull = null;
                do
                {
                    id = id + 1;
                    cmd.CommandText = "select id from PU_ArrivalVouch where id =" + id;
                    isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                }
                while (isnull != null);
                string month = DateTime.Now.Month.ToString("MM");
                #endregion

                #region TempTable
                sql = "insert into SCM_EntryLedgerBuffer ([Subject],iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select [Subject],-1*iNum,-1*iQuantity,0,0,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId =0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                #region PU_ArrivalVouch
                sql = "insert into PU_ArrivalVouch(ivtid,id,ccode,ddate,cvencode,cptcode,cdepcode,cpersoncode,cpaycode,csccode,cexch_name,iexchrate,itaxrate,cmemo,cbustype,cmaker,bnegative,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,ccloser,idiscounttaxtype,ibilltype,cvouchtype,cgeneralordercode,ctmcode,cincotermcode,ctransordercode,dportdate,csportcode,caportcode,csvencode,carrivalplace,dclosedate,idec,bcal,guid,iverifystate,cauditdate,caudittime,cverifier,iverifystateex,ireturncount,iswfcontrolled,cvenpuomprotocol,cchanger,iflowid) " +
                    "values(@ivtid,@Id,@ccode,@ddate,@cvencode,@cptcode,@cdepcode,@cpersoncode,@cpaycode,@csccode,@cexch_name,@iexchrate,@itaxrate,@cmemo,@cbustype,@cmaker,@bnegative,@Cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,@ccloser,@idiscounttaxtype,@ibilltype,@cvouchtype,@cgeneralordercode,@ctmcode,@cincotermcode,@ctransordercode,@dportdate,@csportcode,@caportcode,@csvencode,@carrivalplace,@dclosedate,@idec,@bcal,@guid,@iVerifyState,@cauditdate,@caudittime,@cverifier,@iverifystateex,@ireturncount,@iswfcontrolled,@cvenpuomprotocol,@cchanger,@iflowid)";
                #region SQL_Replace

                sql = sql.Replace("@ivtid", vt_id);
                sql = sql.Replace("@Id", id.ToString());
                sql = sql.Replace("@ccode", id.ToString()); //SelSql(tempSeed + ccode.PadLeft(4, '0')));
                sql = sql.Replace("@ddate", GetNull(dd));
                sql = sql.Replace("@darvdate", dateNull(ds.Tables[0].Rows[0]["darvdate"].ToString()));
                sql = sql.Replace("@dnmaketime", GetNull(dt));
                sql = sql.Replace("@chandler", GetNull(""));
                sql = sql.Replace("@controlresult", SelSql("-1"));
                sql = sql.Replace("@cvencode", SelSql(ds.Tables[0].Rows[0]["cvencode"].ToString()));
                sql = sql.Replace("@cptcode", SelSql(ds.Tables[0].Rows[0]["cptcode"].ToString()));
                sql = sql.Replace("@cdepcode", SelSql(ds.Tables[0].Rows[0]["cdepcode"].ToString()));
                sql = sql.Replace("@cpersoncode", GetNull(ds.Tables[0].Rows[0]["cpersoncode"].ToString()));
                sql = sql.Replace("@cpaycode", GetNull(ds.Tables[0].Rows[0]["cpaycode"].ToString()));
                sql = sql.Replace("@csccode", GetNull(ds.Tables[0].Rows[0]["csccode"].ToString()));
                sql = sql.Replace("@cexch_name", SelSql(ds.Tables[0].Rows[0]["cexch_name"].ToString()));
                sql = sql.Replace("@iexchrate", math(ds.Tables[0].Rows[0]["iexchrate"].ToString()));
                sql = sql.Replace("@itaxrate", math(ds.Tables[0].Rows[0]["itaxrate"].ToString()));
                sql = sql.Replace("@cmemo", SelSql(ds.Tables[0].Rows[0]["cmemo"].ToString()));
                sql = sql.Replace("@cbustype", SelSql(ds.Tables[0].Rows[0]["cbustype"].ToString()));
                sql = sql.Replace("@cmaker", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString()));
                sql = sql.Replace("@bnegative", math(ds.Tables[0].Rows[0]["bnegative"].ToString()));
                sql = sql.Replace("@Cdefine1", GetNull(ds.Tables[0].Rows[0]["cdefine1"].ToString()));
                sql = sql.Replace("@cdefine2", GetNull(ds.Tables[0].Rows[0]["cdefine2"].ToString()));
                sql = sql.Replace("@cdefine3", GetNull(ds.Tables[0].Rows[0]["cdefine3"].ToString()));
                sql = sql.Replace("@cdefine4", GetNull(ds.Tables[0].Rows[0]["cdefine4"].ToString()));
                sql = sql.Replace("@cdefine5", mathNULL(ds.Tables[0].Rows[0]["cdefine5"].ToString()));
                sql = sql.Replace("@cdefine6", GetNull(ds.Tables[0].Rows[0]["cdefine6"].ToString()));
                sql = sql.Replace("@cdefine7", mathNULL(ds.Tables[0].Rows[0]["cdefine7"].ToString()));
                sql = sql.Replace("@cdefine8", GetNull(ds.Tables[0].Rows[0]["cdefine8"].ToString()));
                sql = sql.Replace("@cdefine9", GetNull(ds.Tables[0].Rows[0]["cdefine9"].ToString()));
                sql = sql.Replace("@cdefine10", GetNull(ds.Tables[0].Rows[0]["cdefine10"].ToString()));
                sql = sql.Replace("@cdefine11", GetNull(ds.Tables[0].Rows[0]["cdefine11"].ToString()));
                sql = sql.Replace("@cdefine12", GetNull(ds.Tables[0].Rows[0]["cdefine12"].ToString()));
                sql = sql.Replace("@cdefine13", GetNull(ds.Tables[0].Rows[0]["cdefine13"].ToString()));
                sql = sql.Replace("@cdefine14", GetNull(ds.Tables[0].Rows[0]["cdefine14"].ToString()));
                sql = sql.Replace("@cdefine15", mathNULL(ds.Tables[0].Rows[0]["cdefine15"].ToString()));
                sql = sql.Replace("@cdefine16", mathNULL(ds.Tables[0].Rows[0]["cdefine16"].ToString()));
                sql = sql.Replace("@ccloser", SelSql(ds.Tables[0].Rows[0]["ccloser"].ToString()));
                sql = sql.Replace("@idiscounttaxtype", SelSql(ds.Tables[0].Rows[0]["idiscounttaxtype"].ToString()));
                sql = sql.Replace("@ibilltype", SelSql(ds.Tables[0].Rows[0]["ibilltype"].ToString()));
                sql = sql.Replace("@cvouchtype", SelSql(ds.Tables[0].Rows[0]["cvouchtype"].ToString()));
                sql = sql.Replace("@cgeneralordercode", SelSql(ds.Tables[0].Rows[0]["cgeneralordercode"].ToString()));
                sql = sql.Replace("@ctmcode", SelSql(ds.Tables[0].Rows[0]["ctmcode"].ToString()));
                sql = sql.Replace("@cincotermcode", GetNull(ds.Tables[0].Rows[0]["cincotermcode"].ToString()));
                sql = sql.Replace("@ctransordercode", GetNull(ds.Tables[0].Rows[0]["ctransordercode"].ToString()));
                sql = sql.Replace("@dportdate", dateNull(ds.Tables[0].Rows[0]["dportdate"].ToString()));
                sql = sql.Replace("@csportcode", SelSql(ds.Tables[0].Rows[0]["csportcode"].ToString()));
                sql = sql.Replace("@caportcode", SelSql(ds.Tables[0].Rows[0]["caportcode"].ToString()));
                sql = sql.Replace("@csvencode", SelSql(ds.Tables[0].Rows[0]["csvencode"].ToString()));
                sql = sql.Replace("@carrivalplace", SelSql(ds.Tables[0].Rows[0]["carrivalplace"].ToString()));
                sql = sql.Replace("@dclosedate", dateNull(ds.Tables[0].Rows[0]["dclosedate"].ToString()));
                sql = sql.Replace("@idec", math(ds.Tables[0].Rows[0]["idec"].ToString()));
                sql = sql.Replace("@bcal", GetNull(ds.Tables[0].Rows[0]["bcal"].ToString()));
                sql = sql.Replace("@guid", SelSql(ds.Tables[0].Rows[0]["guid"].ToString()));
                sql = sql.Replace("@ireturncount", math(ds.Tables[0].Rows[0]["ireturncount"].ToString()));
                sql = sql.Replace("@iswfcontrolled", GetNull(ds.Tables[0].Rows[0]["iswfcontrolled"].ToString()));
                sql = sql.Replace("@cvenpuomprotocol", GetNull(ds.Tables[0].Rows[0]["cvenpuomprotocol"].ToString()));
                sql = sql.Replace("@cchanger", SelSql(ds.Tables[0].Rows[0]["cchanger"].ToString()));
                sql = sql.Replace("@iflowid", mathNULL(ds.Tables[0].Rows[0]["iflowid"].ToString()));
                sql = sql.Replace("@cverifier", SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString())); //审核人
                sql = sql.Replace("@dnverifytime", GetNull(dt));
                sql = sql.Replace("@dveridate", GetNull(dd));
                sql = sql.Replace("@cauditdate", GetNull(dd));      //审核日期
                sql = sql.Replace("@caudittime", GetNull(dt));      //审核时间
                sql = sql.Replace("@iVerifyState", GetNull(""));    //审核状态
                sql = sql.Replace("@iverifystateex", GetNull("2")); //工作流审核状态
                #endregion

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                #endregion

                #region TempTable
                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"Create table TmpPU_A_ArrivalVouchs_Detail
                        (
                        iSourceautoid int,
                        iArrqty decimal(38,4),
                        iArrnum decimal(38,2),
                        iArrmoney decimal(38,2),
                        iArrnatmoney decimal(38,2),
                        fPoValidQuantity decimal(38,4),
                        fPoValidNum decimal(38,2),
                        fPoArrQuantity decimal(38,4),
                        fPoArrNum decimal(38,2),
                        fPoRetQuantity decimal(38,4),
                        fPoRetNum decimal(38,2),
                        fPoRefuseQuantity decimal(38,4),
                        fPoRefuseNum decimal(38,2),
                        sotype nvarchar (10),
                        ufts money
                        )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                foreach (DataRow dr in ds.Tables["Body"].Rows)
                {
                    isnull = null;
                    do
                    {
                        autoid = autoid + 1;
                        cmd.CommandText = "select autoid from PU_ArrivalVouchs where autoid =" + autoid;
                        isnull = cmd.ExecuteScalar() == DBNull.Value ? null : cmd.ExecuteScalar();
                    }
                    while (isnull != null);

                    #region PU_ArrivalVouchs
                    sql = "Insert Into PU_ArrivalVouchs(autoid,id,cwhcode,cinvcode,inum,iquantity,ioricost,ioritaxcost,iorimoney,ioritaxprice,iorisum,icost,imoney,itaxprice,isum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,itaxrate,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,citem_class,citemcode,iposid,citemname,cunitid,fkpquantity,frealquantity,fValidQuantity,fvalidInQuan,finvalidquantity,ccloser,icorid,bgsp,cbatch,dvdate,dpdate,frefusequantity,cgspstate,fvalidnum,finvalidnum,frealnum,btaxcost,binspect,frefusenum,ippartid,ipquantity,iptoseq,sodid,sotype,contractrowguid,imassdate,cmassunit,bexigency,cbcloser,fdtquantity,finvalidinnum,fdegradequantity,fdegradenum,fdegradeinquantity,fdegradeinnum,finspectquantity,finspectnum,iinvmpcost,guids,iinvexchrate,objectid_source,autoid_source,ufts_source,irowno_source,csocode,isorowno,iorderid,cordercode,iorderrowno,dlineclosedate,contractcode,contractrowno,rejectsource,iciqbookid,cciqbookcode,cciqcode,fciqchangrate,irejectautoid,iexpiratdatecalcu,cexpirationdate,dexpirationdate,cupsocode,iorderdid,iordertype,csoordercode,iorderseq,cbatchproperty1,cbatchproperty2,cbatchproperty3,cbatchproperty4,cbatchproperty5,cbatchproperty6,cbatchproperty7,cbatchproperty8,cbatchproperty9,cbatchproperty10,ivouchrowno)"
                        + "values(@Autoid,@Id,@cwhcode,@cinvcode,@inum,@iquantity,@ioricost,@ioritaxcost,@ioriMoney,@ioritaxprice,@iorisum,@icost,@imoney,@itaxprice,@isum,@Cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@itaxrate,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@citem_class,@citemcode,@iposid,@citemname,@cunitid,@fkpquantity,@frealquantity,@fValidQuantity,@fvalidInQuan,@finvalidquantity,@ccloser,@icorid,@bgsp,@cbatch,@dvdate,@dpdate,@frefusequantity,@cgspstate,@fvalidnum,@finvalidnum,@frealnum,@btaxcost,@binspect,@frefusenum,@ippartid,@ipquantity,@iptoseq,@sodid,@sotype,@contractrowguid,@imassdate,@cmassunit,@bexigency,@cbcloser,@fdtquantity,@finvalidinnum,@fdegradequantity,@fdegradenum,@fdegradeinquantity,@fdegradeinnum,@finspectquantity,@finspectnum,@iinvmpcost,@guids,@iinvexchrate,@objectid_source,@autoid_source,@ufts_source,@irowno_source,@csocode,@isorowno,@iorderid,@cordercode,@iorderrowno,@dlineclosedate,@contractcode,@contractrowno,@rejectsource,@iciqbookid,@cciqbookcode,@cciqcode,@fciqchangrate,@irejectautoid,@iexpiratdatecalcu,@cexpirationdate,@dexpirationdate,@cupsocode,@iorderdid,@iordertype,@csoordercode,@iorderseq,@Cbatchproperty1,@cbatchproperty2,@cbatchproperty3,@cbatchproperty4,@cbatchproperty5,@cbatchproperty6,@cbatchproperty7,@cbatchproperty8,@cbatchproperty9,@cbatchproperty10,@ivouchrowno)";
                    #region SQL_Replace

                    sql = sql.Replace("@Autoid", autoid.ToString());
                    sql = sql.Replace("@Id", id.ToString());
                    sql = sql.Replace("@inquantity", math("0"));
                    sql = sql.Replace("@bcosting", math("1"));
                    sql = sql.Replace("@iordertype", mathNULL("0"));
                    sql = sql.Replace("@cwhcode", GetNull(dr["cwhcode"].ToString()));
                    sql = sql.Replace("@cinvcode", GetNull(dr["cinvcode"].ToString()));
                    sql = sql.Replace("@inum", math(dr["inum"].ToString()));
                    sql = sql.Replace("@iquantity", math(dr["iquantity"].ToString()));
                    sql = sql.Replace("@ioricost", math(dr["ioricost"].ToString()));
                    sql = sql.Replace("@imoney", math(dr["imoney"].ToString()));
                    sql = sql.Replace("@ioritaxcost", math(dr["ioritaxcost"].ToString()));
                    sql = sql.Replace("@ioriMoney", math(dr["iorimoney"].ToString()));
                    sql = sql.Replace("@ioritaxprice", math(dr["ioritaxprice"].ToString()));
                    sql = sql.Replace("@iorisum", math(dr["iorisum"].ToString()));
                    sql = sql.Replace("@icost", math(dr["icost"].ToString()));
                    sql = sql.Replace("@itaxprice", math(dr["itaxprice"].ToString()));
                    sql = sql.Replace("@isum", math(dr["isum"].ToString()));
                    sql = sql.Replace("@Cfree1", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree2", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree3", GetNull(dr["cfree3"].ToString()));
                    sql = sql.Replace("@cfree4", GetNull(dr["cfree4"].ToString()));
                    sql = sql.Replace("@cfree5", GetNull(dr["cfree5"].ToString()));
                    sql = sql.Replace("@cfree6", GetNull(dr["cfree6"].ToString()));
                    sql = sql.Replace("@cfree7", GetNull(dr["cfree7"].ToString()));
                    sql = sql.Replace("@cfree8", GetNull(dr["cfree8"].ToString()));
                    sql = sql.Replace("@cfree9", GetNull(dr["cfree9"].ToString()));
                    sql = sql.Replace("@cfree10", GetNull(dr["cfree10"].ToString()));
                    sql = sql.Replace("@itaxrate", mathNULL(dr["itaxrate"].ToString()));
                    sql = sql.Replace("@cdefine22", GetNull(dr["caddress"].ToString()));
                    sql = sql.Replace("@cdefine23", GetNull(dr["cdefine23"].ToString()));
                    sql = sql.Replace("@cdefine24", GetNull(dr["cdefine24"].ToString()));
                    sql = sql.Replace("@cdefine25", GetNull(dr["cdefine25"].ToString()));
                    sql = sql.Replace("@cdefine26", mathNULL(dr["cdefine26"].ToString()));
                    sql = sql.Replace("@cdefine27", mathNULL(dr["cdefine27"].ToString()));
                    sql = sql.Replace("@cdefine28", GetNull(dr["cdefine28"].ToString()));
                    sql = sql.Replace("@cdefine29", GetNull(dr["cdefine29"].ToString()));
                    sql = sql.Replace("@cdefine30", GetNull(dr["cdefine30"].ToString()));
                    sql = sql.Replace("@cdefine31", GetNull(dr["cdefine31"].ToString()));
                    sql = sql.Replace("@cdefine32", GetNull(dr["cdefine32"].ToString()));
                    sql = sql.Replace("@cdefine33", GetNull(dr["cdefine33"].ToString()));
                    sql = sql.Replace("@cdefine34", mathNULL(dr["cdefine34"].ToString()));
                    sql = sql.Replace("@cdefine35", mathNULL(dr["cdefine35"].ToString()));
                    sql = sql.Replace("@cdefine36", dateNull(dr["cdefine36"].ToString()));
                    sql = sql.Replace("@cdefine37", dateNull(dr["cdefine37"].ToString()));
                    sql = sql.Replace("@Cbatchproperty1", GetNull(dr["cbatchproperty1"].ToString()));
                    sql = sql.Replace("@cbatchproperty2", GetNull(dr["cbatchproperty2"].ToString()));
                    sql = sql.Replace("@cbatchproperty3", GetNull(dr["cbatchproperty3"].ToString()));
                    sql = sql.Replace("@cbatchproperty4", GetNull(dr["cbatchproperty4"].ToString()));
                    sql = sql.Replace("@cbatchproperty5", GetNull(dr["cbatchproperty5"].ToString()));
                    sql = sql.Replace("@cbatchproperty6", GetNull(dr["cbatchproperty6"].ToString()));
                    sql = sql.Replace("@cbatchproperty7", GetNull(dr["cbatchproperty7"].ToString()));
                    sql = sql.Replace("@cbatchproperty8", GetNull(dr["cbatchproperty8"].ToString()));
                    sql = sql.Replace("@cbatchproperty9", GetNull(dr["cbatchproperty9"].ToString()));
                    sql = sql.Replace("@cbatchproperty10", GetNull(dr["cbatchproperty10"].ToString()));
                    sql = sql.Replace("@citem_class", GetNull(dr["citem_class"].ToString()));
                    sql = sql.Replace("@citemcode", GetNull(dr["citemcode"].ToString()));
                    sql = sql.Replace("@iposid", mathNULL(dr["iposid"].ToString()));
                    sql = sql.Replace("@citemname", GetNull(dr["citemname"].ToString()));
                    sql = sql.Replace("@cunitid", GetNull(dr["cunitid"].ToString()));
                    sql = sql.Replace("@fkpquantity", mathNULL(dr["fkpquantity"].ToString()));
                    sql = sql.Replace("@frealquantity", mathNULL(dr["frealquantity"].ToString()));//实收数量
                    sql = sql.Replace("@fValidQuantity", mathNULL(dr["fValidQuantity"].ToString()));//合格数量
                    sql = sql.Replace("@finvalidquantity", mathNULL("0"));//不合格数量
                    sql = sql.Replace("@fvalidInQuan", math("0"));//合格品入库数量
                    sql = sql.Replace("@ccloser", GetNull(dr["ccloser"].ToString()));
                    sql = sql.Replace("@icorid", mathNULL(dr["icorid"].ToString()));
                    sql = sql.Replace("@bgsp", SelSql(dr["bgsp"].ToString()));
                    sql = sql.Replace("@cbatch", GetNull(dr["cbatch"].ToString()));
                    sql = sql.Replace("@dvdate", dateNull(dr["dvdate"].ToString()));
                    sql = sql.Replace("@dpdate", dateNull(dr["dpdate"].ToString()));
                    sql = sql.Replace("@frefusequantity", GetNull(dr["frefusequantity"].ToString()));
                    sql = sql.Replace("@cgspstate", GetNull(dr["cgspstate"].ToString()));
                    sql = sql.Replace("@fvalidnum", GetNull(dr["fvalidnum"].ToString()));
                    sql = sql.Replace("@finvalidnum", GetNull(dr["finvalidnum"].ToString()));
                    sql = sql.Replace("@frealnum", GetNull(dr["frealnum"].ToString()));
                    sql = sql.Replace("@btaxcost", GetNull(dr["btaxcost"].ToString()));
                    sql = sql.Replace("@binspect", GetNull(dr["binspect"].ToString()));
                    sql = sql.Replace("@frefusenum", mathNULL(dr["frefusenum"].ToString()));
                    sql = sql.Replace("@ippartid", mathNULL(dr["ippartid"].ToString()));
                    sql = sql.Replace("@ipquantity", mathNULL(dr["ipquantity"].ToString()));
                    sql = sql.Replace("@iptoseq", mathNULL(dr["iptoseq"].ToString()));
                    sql = sql.Replace("@sodid", GetNull(dr["sodid"].ToString()));
                    sql = sql.Replace("@sotype", GetNull(dr["sotype"].ToString()));
                    sql = sql.Replace("@contractrowguid", GetNull(dr["contractrowguid"].ToString()));
                    sql = sql.Replace("@imassdate", mathNULL(dr["imassdate"].ToString()));
                    sql = sql.Replace("@cmassunit", GetNull(dr["cmassunit"].ToString()));
                    sql = sql.Replace("@bexigency", GetNull(dr["bexigency"].ToString()));
                    sql = sql.Replace("@cbcloser", GetNull(dr["cbcloser"].ToString()));
                    sql = sql.Replace("@fdtquantity", GetNull(dr["fdtquantity"].ToString()));
                    sql = sql.Replace("@finvalidinnum", GetNull(dr["finvalidinnum"].ToString()));
                    sql = sql.Replace("@fdegradequantity", GetNull(dr["fdegradequantity"].ToString()));
                    sql = sql.Replace("@fdegradenum", GetNull(dr["fdegradenum"].ToString()));
                    sql = sql.Replace("@fdegradeinquantity", GetNull(dr["fdegradeinquantity"].ToString()));
                    sql = sql.Replace("@fdegradeinnum", GetNull(dr["fdegradeinnum"].ToString()));
                    sql = sql.Replace("@finspectquantity", math("0"));
                    sql = sql.Replace("@finspectnum", GetNull(dr["finspectnum"].ToString()));
                    sql = sql.Replace("@iinvmpcost", GetNull(dr["iinvmpcost"].ToString()));
                    sql = sql.Replace("@guids", GetNull(dr["guids"].ToString()));
                    sql = sql.Replace("@iinvexchrate", GetNull(dr["iinvexchrate"].ToString()));
                    sql = sql.Replace("@objectid_source", GetNull(dr["objectid_source"].ToString()));
                    sql = sql.Replace("@autoid_source", mathNULL(dr["autoid_source"].ToString()));
                    sql = sql.Replace("@ufts_source", GetNull(dr["ufts_source"].ToString()));
                    sql = sql.Replace("@irowno_source", mathNULL(dr["irowno_source"].ToString()));
                    sql = sql.Replace("@csocode", GetNull(dr["csocode"].ToString()));
                    sql = sql.Replace("@isorowno", mathNULL(dr["isorowno"].ToString()));
                    sql = sql.Replace("@iorderid", mathNULL(dr["iorderid"].ToString()));
                    sql = sql.Replace("@cordercode", GetNull(dr["cordercode"].ToString()));
                    sql = sql.Replace("@iorderrowno", mathNULL(dr["iorderrowno"].ToString()));
                    sql = sql.Replace("@dlineclosedate", dateNull(dr["dlineclosedate"].ToString()));
                    sql = sql.Replace("@contractcode", GetNull(dr["contractcode"].ToString()));
                    sql = sql.Replace("@contractrowno", GetNull(dr["contractrowno"].ToString()));
                    sql = sql.Replace("@rejectsource", GetNull(dr["rejectsource"].ToString()));
                    sql = sql.Replace("@iciqbookid", mathNULL(dr["iciqbookid"].ToString()));
                    sql = sql.Replace("@cciqbookcode", GetNull(dr["cciqbookcode"].ToString()));
                    sql = sql.Replace("@cciqcode", GetNull(dr["cciqcode"].ToString()));
                    sql = sql.Replace("@fciqchangrate", GetNull(dr["fciqchangrate"].ToString()));
                    sql = sql.Replace("@irejectautoid", GetNull(dr["irejectautoid"].ToString()));
                    sql = sql.Replace("@iexpiratdatecalcu", mathNULL(dr["iexpiratdatecalcu"].ToString()));
                    sql = sql.Replace("@cexpirationdate", GetNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@dexpirationdate", dateNull(dr["cexpirationdate"].ToString()));
                    sql = sql.Replace("@cupsocode", GetNull(dr["cupsocode"].ToString()));
                    sql = sql.Replace("@iorderdid", mathNULL(dr["iorderdid"].ToString()));
                    sql = sql.Replace("@iordertype", GetNull(dr["iordertype"].ToString()));
                    sql = sql.Replace("@csoordercode", GetNull(dr["csoordercode"].ToString()));
                    sql = sql.Replace("@iorderseq", mathNULL(dr["iorderseq"].ToString()));
                    sql = sql.Replace("@ivouchrowno", mathNULL((++iVouchRow).ToString()));
                    #endregion

                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    if (string.IsNullOrEmpty(strAutoId))
                        strAutoId = autoid.ToString();
                    else
                        strAutoId += "," + autoid.ToString();
                    #endregion

                    #region OM_MOdetails
                    sql = "update OM_MOdetails set iArrQTY=isnull(iArrQTY,0)+@iArrQTY,iArrNum=isnull(iArrNum,0)+@iArrNum,iArrMoney=isnull(iArrMoney,0)+@iArrMoney,iNatArrMoney=isnull(iNatArrMoney,0)+@iNatArrMoney where modetailsID = @modetailsID and isnull(iReceivedQTY,0)=0 and isnull(iReceivedNum,0)=0";
                    #region SQL_Replace
                    sql = sql.Replace("@modetailsID", GetNull(dr["modetailsID"].ToString()));
                    sql = sql.Replace("@iNatArrMoney", math(dr["iSum"].ToString()));
                    sql = sql.Replace("@iArrMoney", math(dr["iOriSum"].ToString()));
                    sql = sql.Replace("@iArrNum", math(dr["iNum"].ToString()));
                    sql = sql.Replace("@iArrQTY", math(dr["iQuantity"].ToString()));
                    #endregion
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    #endregion

                    #region TmpPU_A_ArrivalVouchs_Detail
                    if (bGsp)
                    {
                        sql = "insert into TmpPU_A_ArrivalVouchs_Detail(iSourceautoid,iArrqty,iArrnum,iArrmoney,iArrnatmoney,fPoValidQuantity,fPoValidNum,fPoArrQuantity,fPoArrNum,fPoRetQuantity,fPoRetNum,fPoRefuseQuantity,fPoRefuseNum,sotype)"
                            + "values(@iSourceautoid,@iArrqty,@iArrnum,@iArrmoney,@iArrnatmoney,@fPoValidQuantity,@fPoValidNum,@fPoArrQuantity,@fPoArrNum,@fPoRetQuantity,@fPoRetNum,@fPoRefuseQuantity,@fPoRefuseNum,@sotype)";
                        #region SQL_Replace
                        sql = sql.Replace("@iSourceautoid", mathNULL(dr["iposid"].ToString()));
                        sql = sql.Replace("@iArrqty", math(dr["iquantity"].ToString()));
                        sql = sql.Replace("@iArrnum", math(dr["inum"].ToString()));
                        sql = sql.Replace("@iArrmoney", math(dr["iorisum"].ToString()));
                        sql = sql.Replace("@iArrnatmoney", math(dr["iorisum"].ToString()));
                        sql = sql.Replace("@fPoValidQuantity", math("0"));
                        sql = sql.Replace("@fPoValidNum", math("0"));
                        sql = sql.Replace("@fPoArrQuantity", math(dr["iquantity"].ToString()));
                        sql = sql.Replace("@fPoArrNum", math(dr["inum"].ToString()));
                        sql = sql.Replace("@fPoRetQuantity", math("0"));
                        sql = sql.Replace("@fPoRetNum", math("0"));
                        sql = sql.Replace("@fPoRefuseQuantity", math("0"));
                        sql = sql.Replace("@fPoRefuseNum", math("0"));
                        sql = sql.Replace("@sotype", SelSql("mo"));
                        #endregion

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    #endregion
                }
                #region TempTable
                #region TmpPU_B_ArrivalVouchs_Detail

                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "select isourceautoid,sum(T.iArrqty) as iArrqty,sum(T.iArrnum) as iArrnum,sum(T.iArrmoney) as iArrmoney,sum(T.iArrnatmoney) as iArrnatmoney,sum(T.fPoValidQuantity) as fPoValidQuantity,sum(T.fPoValidNum) as fPoValidNum,sum(T.fPoArrQuantity) as fPoArrQuantity,sum(T.fPoArrNum) as fPoArrNum,sum(T.fPoRetQuantity) as fPoRetQuantity,sum(T.fPoRetNum) as fPoRetNum,sum(T.fPoRefuseQuantity) as fPoRefuseQuantity,sum(T.fPoRefuseNum) as fPoRefuseNum,sotype,min(ufts) as ufts into TmpPU_B_ArrivalVouchs_Detail from TmpPU_A_ArrivalVouchs_Detail as T group by isourceautoid,sotype ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                sql = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,1*iNum,1*iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId = " + id.ToString();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                int abc;
                #region UpdateTrans

                sql = "select @@spid ";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(spid))
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

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'OM',1,1,0,1,0,0,1,0,1,0,0,0,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion

                #region VoucherHistory

                sql = "select cUser_id from UfSystem..UA_User where cUser_Name = " + SelSql(ds.Tables[0].Rows[0]["cmaker"].ToString());
                cmd.CommandText = sql;
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();

                cmd.CommandText = "Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed=" + SelSql(user);
                user = cmd.ExecuteScalar() == DBNull.Value ? "" : cmd.ExecuteScalar().ToString().ToUpper();
                if (user.Length > 3)
                    user = user.Substring(0, 3);
                else
                    user = user.PadLeft(3, '0');
                
                /*
                cSeed = user + ddate + "0";

                sql = "IF NOT EXISTS(select cNumber as Maxnumber From VoucherHistory  with (XLOCK) Where  CardNumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed='" + tempSeed + "') "
                    + "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','操作员|单据日期|红蓝标志','','" + tempSeed + "','" + icode + "')";
                cmd.CommandText = sql;
                abc = cmd.ExecuteNonQuery();

                sql = "update VoucherHistory set cNumber='" + icode + "' Where  CardNumber='" + cardnumber + "' and cContent='操作员|单据日期|红蓝标志' and cSeed='" + tempSeed + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cSeed = cSeed + icode.ToString().PadLeft(4, '0');
                if (cSeed.Length > 16)
                {
                    myTran.Rollback();
                    return -1;
                }
                cmd.CommandText = "Update pu_arrivalvouch Set cCode = " + SelSql(cSeed) + " Where Id = " + id.ToString();
                abc = cmd.ExecuteNonQuery();

                 * 
                 * */

                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='单据日期' and cSeed='{1}'", cardnumber, cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('{0}','单据日期','月','{1}','1')", cardnumber, cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent='单据日期' and cSeed='{2}'", icode, cardnumber, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }

                //采购到货的流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                cmd.CommandText = string.Format("Update pu_arrivalvouch Set cCode='{0}' where id={1}", ccode, id);
                abc = cmd.ExecuteNonQuery();

                cmd.CommandText = "exec Scm_SaveBatchProperty '" + strAutoId + "','autoid','pu_arrivalvouchs','" + user + "'";
                abc = cmd.ExecuteNonQuery();

                if (id > 10000000)
                    id -= 860000000;    //暂定
                if (autoid > 10000000)
                    autoid -= 860000000;    //暂定
                //UA_Identity
                cmd.CommandText = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + "  where cvouchtype='PUARRIVAL' and cAcc_id='" + accid + "'";
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

        #region Function

        private static DataSet BuildArrivalStruct()
        {
            DataSet ds = new DataSet();

            DataTable dtHead = new DataTable("Head");
            DataTable dtBody = new DataTable("Body");

            ds.Tables.Add(dtHead);
            ds.Tables.Add(dtBody);

            #region Head
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
            //ds.Tables["Head"].Columns.Add("darvdate");    
            #endregion
            #region Body
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
            ds.Tables["Body"].Columns.Add("itax");
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

        private static DataSet FillDs(DataSet sourceDs, DataSet tagDs, bool blnIsArriveOrder)
        {
            DataRow dr;
            int iHeadColumns = sourceDs.Tables["Head"].Columns.Count;
            int iBodyColumns = sourceDs.Tables["Body"].Columns.Count;

            #region Head

            for (int j = 0; j < sourceDs.Tables["Head"].Rows.Count; j++)
            {
                dr = tagDs.Tables["Head"].NewRow();
                tagDs.Tables["Head"].Rows.Add(dr);
                for (int i = 0; i < iHeadColumns; i++)
                {
                    if (tagDs.Tables["Head"].Columns.IndexOf
                        (sourceDs.Tables["Head"].Columns[i].ColumnName) > 0)
                    {
                        if (sourceDs.Tables["Head"].Rows[j][i] == DBNull.Value)
                        {
                            tagDs.Tables["Head"].Rows[j][sourceDs.Tables["Head"].Columns[i].ColumnName] = DBNull.Value;
                            continue;
                        }

                        tagDs.Tables["Head"].Rows[j][sourceDs.Tables["Head"].Columns[i].ColumnName]
                            = sourceDs.Tables["Head"].Rows[j][i].ToString();
                    }
                }
            }

            #endregion
            #region Body
            foreach (DataRow drr in sourceDs.Tables["Body"].Rows)
            {
                DataRow idr = tagDs.Tables["Body"].NewRow();
                tagDs.Tables["Body"].Rows.Add(idr);
                for (int i = 0; i < iBodyColumns; i++)
                {
                    if (tagDs.Tables["Body"].Columns.IndexOf
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
            #endregion

            return tagDs;
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
            return str.Trim();
        }

        public static string mathNULL(string str)
        {
            if (string.IsNullOrEmpty(str) || (str == "0") || (decimal.Parse(str) == 0M))
            {
                return "NULL";
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

    }
}