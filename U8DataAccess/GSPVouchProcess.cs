using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace U8DataAccess
{
    public class GSPVouchProcess
    {
        #region 验证销售出库单号并获取子表信息
        public static int GetSaleOut(string ccode, string connectionString, out DataSet list, out string errMsg)
        {
            list = null;
            errMsg = "";
            /* **   
            string strSql = @"select cbuscode,KCSaleOuth.cwhcode,KCSaleOuth.cwhname,cinvcode,cmaker,KCSaleOutB.ID as id,cCode,cinvm_unit,cassunit,iquantity,inum,ddate,cbatch,cposition,ccuscode,cinvname,cinvstd,dvdate,ccusabbname,autoid,dmadedate,cfree1,cfree2,cinva_unit,ltrim(rtrim(str(iinvexchrate,19,5))) as iinvexchrate,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,cmemo,ufts,cinvdefine1,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine5,cinvdefine6,cinvdefine7,cinvdefine8,cinvdefine9,cinvdefine10,cinvdefine11,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,imassdate, CMASSUNIT ,   (CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS [IEXPIRATDATECALCU],KCSaleOutB.dExpirationdate as CVALDATE  , case  isnull([Inventory_Sub].[iExpiratDateCalcu],0) when 1 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)   when 2 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)+'-'+RIGHT('00'+convert(varchar,day(KCSaleOutB.dExpirationdate), 2),2)  else '' end  as CVALDATES, KCSaleOutB.cbatchproperty1,KCSaleOutB.cbatchproperty2,KCSaleOutB.cbatchproperty3,KCSaleOutB.cbatchproperty4,KCSaleOutB.cbatchproperty5,KCSaleOutB.cbatchproperty6,KCSaleOutB.cbatchproperty7,KCSaleOutB.cbatchproperty8,KCSaleOutB.cbatchproperty9,KCSaleOutB.cbatchproperty10  
from KCSaleOutB inner join KCSaleOuth on kcsaleouth.id=kcsaleoutb.id 
INNER JOIN  [dbo].[Inventory_Sub] ON  [dbo].[KCSaleOutB].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode] 
where ddate>=N'2007-03-01'  And CCODE = N'" + ccode + @"' and isnull(cgspstate,'')=N'' and isnull(cHandler,'')<>N'' and isnull(iquantity,0)>0 order by ID";
            
             * 
             * */
            //Time:2012-09-04
            // Author:tianzhenyun 改ccode为cbuscode 把出库单改为发货单
            string strSql = @"select cbuscode,KCSaleOuth.cwhcode,KCSaleOuth.cwhname,cinvcode,cmaker,KCSaleOutB.ID as id,cCode,cinvm_unit,cassunit,iquantity,inum,ddate,cbatch,cposition,ccuscode,cinvname,cinvstd,dvdate,ccusabbname,autoid,dmadedate,cfree1,cfree2,cinva_unit,ltrim(rtrim(str(iinvexchrate,19,5))) as iinvexchrate,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,cmemo,ufts,cinvdefine1,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine5,cinvdefine6,cinvdefine7,cinvdefine8,cinvdefine9,cinvdefine10,cinvdefine11,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,imassdate, CMASSUNIT ,   (CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS [IEXPIRATDATECALCU],KCSaleOutB.dExpirationdate as CVALDATE  , case  isnull([Inventory_Sub].[iExpiratDateCalcu],0) when 1 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)   when 2 then convert(varchar ,Year(KCSaleOutB.dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(KCSaleOutB.dExpirationdate) ,2),2)+'-'+RIGHT('00'+convert(varchar,day(KCSaleOutB.dExpirationdate), 2),2)  else '' end  as CVALDATES, KCSaleOutB.cbatchproperty1,KCSaleOutB.cbatchproperty2,KCSaleOutB.cbatchproperty3,KCSaleOutB.cbatchproperty4,KCSaleOutB.cbatchproperty5,KCSaleOutB.cbatchproperty6,KCSaleOutB.cbatchproperty7,KCSaleOutB.cbatchproperty8,KCSaleOutB.cbatchproperty9,KCSaleOutB.cbatchproperty10  
from KCSaleOutB inner join KCSaleOuth on kcsaleouth.id=kcsaleoutb.id 
INNER JOIN  [dbo].[Inventory_Sub] ON  [dbo].[KCSaleOutB].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode] 
where ddate>=N'2007-03-01'  And cbuscode = N'" + ccode + @"' and isnull(cgspstate,'')=N'' and isnull(cHandler,'')<>N'' and isnull(iquantity,0)>0 order by ID";
            //return OperationSql.GetDataset(strSql, connectionString, out list, out errMsg);
            int flag = -1;
            try
            {
                list = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        #endregion

        #region 验证销售退货单号并获取子表信息
        public static int GetSaleBack(string cdlcode, string connectionString, out DataSet list, out string errMsg)
        {
            list = null;
            errMsg = "";
            string strSql = @"SELECT dispatchlists.cwhcode,warehouse.cwhname,dispatchlists.cinvcode,dispatchlists.cinvname,ccode,dispatchlist.csocode,convert(char,convert(money,dispatchlist.ufts),2) as ufts,cbustype,caccounter,cdlcode,dispatchlist.cvouchtype,cvouchname,dispatchlist.cstcode,cstname,ddate,dispatchlist.cdepcode,cdepname,dispatchlist.cpersoncode,cpersonname,dispatchlist.ccuscode,ccusabbname,dispatchlist.cpaycode,cexch_name,iexchrate,dispatchlist.itaxrate,cdefine1,cdefine2,breturnflag,cpayname,dispatchlist.dlid,cverifier,cmaker,bfirst,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,isale,Customer.ccusname,Customer.ccusabbname,dispatchlist.ccusperson,ccuspostcode,bservice,cinvstd,(case when igrouptype=1 then cunitid else '' end) as cunitid,unit1.ccomunitname as cinvm_unit,igrouptype,inventory.cgroupcode,(case when igrouptype=1 then unit2.ccomunitname else '' end) as cinva_unit,(case when isnull(dispatchlists.itb, 0) <> 0 then abs(dispatchlists.tbnum) else (case when igrouptype=0 then null else  abs(dispatchlists.inum)  end) end) as inum,(case when igrouptype=1 then convert(decimal(19,5),iinvexchrate) else null end) as iinvexchrate,(case when isnull(dispatchlists.itb, 0) <> 0 then abs(dispatchlists.tbquantity) else abs(dispatchlists.iquantity) end) as iquantity,isettlenum,isettlequantity,iquotedprice,itaxunitprice,iunitprice,abs(imoney) as imoney,cbatch,icorid,binvbatch,bfree1,bfree2,dvdate,dmdate,dispatchlists.cExpirationdate AS cvaldate,dispatchlists.dexpirationdate AS dvaldate,dispatchlists.imassdate, (CASE dispatchlists.CMASSUNIT WHEN 0 THEN '' WHEN 1 THEN '年' WHEN 2 THEN '月' WHEN 3 THEN '日' END) AS CMASSUNIT ,(CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS IEXPIRATDATECALCU ,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine32,cdefine31,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,idlsid,citemcode,citem_class,cvenabbname,citemname,citem_cname,dispatchlist.cmemo,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,case when bgsp=0 then '否' else '是' end as bgsp,bSpecialties as '特殊药品标志',abs(isum) as isum,(case when isnull(foutquantity,0)=0 then null else foutquantity end) as 累计出库数量,(case when isnull(foutnum,0)=0 then null else foutnum end) as 累计出库件数,Inventory.cinvdefine1,Inventory.cinvdefine2,Inventory.cinvdefine3,Inventory.cinvdefine4,Inventory.cinvdefine5,Inventory.cinvdefine6,Inventory.cinvdefine7,Inventory.cinvdefine8,Inventory.cinvdefine9,Inventory.cinvdefine10,Inventory.cinvdefine11,Inventory.cinvdefine12,Inventory.cinvdefine13,Inventory.cinvdefine14,Inventory.cinvdefine15,Inventory.cinvdefine16 ,DisPatchLists.AutoID, DisPatchLists.cbatchproperty1, DisPatchLists.cbatchproperty2,DisPatchLists.cbatchproperty3,DisPatchLists.cbatchproperty4,DisPatchLists.cbatchproperty5,DisPatchLists.cbatchproperty6,DisPatchLists.cbatchproperty7,DisPatchLists.cbatchproperty8,DisPatchLists.cbatchproperty9,DisPatchLists.cbatchproperty10 FROM DisPatchList INNER JOIN DisPatchLists ON DisPatchList.dlid=DisPatchLists.dlid LEFT OUTER JOIN Customer ON DispatchList.cCusCode = Customer.cCusCode LEFT OUTER JOIN Department ON DispatchList.cDepCode = Department.cDepCode LEFT OUTER JOIN PayCondition ON DispatchList.cPayCode = PayCondition.cPayCode LEFT OUTER JOIN Person ON DispatchList.cPersonCode = Person.cPersonCode LEFT OUTER JOIN SaleType ON DispatchList.cSTCode = SaleType.cSTCode LEFT OUTER JOIN ShippingChoice ON DispatchList.cSCCode = ShippingChoice.cSCCode LEFT OUTER JOIN VouchType ON DispatchList.cVouchType = VouchType.cVouchType LEFT JOIN Warehouse ON DispatchLists.cWhCode = Warehouse.cWhCode LEFT JOIN Inventory ON DispatchLists.cInvCode = Inventory.cInvCode left join ComputationUnit as Unit1 on inventory.cComUnitCode=Unit1.cComUnitCode left join ComputationUnit as Unit2 on dispatchlists.cunitid=Unit2.cComUnitCode INNER JOIN  [dbo].[Inventory_Sub] ON  [dbo].[Inventory].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode]  where (dispatchlist.cvouchtype=N'05' or dispatchlist.cvouchtype=N'06') and dispatchlist.ddate>=N'2007-03-01' and isnull(bgsp,0)=1 and isnull(cgspstate,'')=N'' and isnull(iquantity,0)<0 and isnull(cVerifier,'')<>N'' AND ISNULL(dispatchlists.bSettleAll,0)=0 AND isnull(itb,0)<>1 and isnull(dispatchlists.cwhcode,'')<>N'' 
                                and cdlcode = N'" + cdlcode + @"'
                                order by dispatchlist.dlid";
            //return OperationSql.GetDataset(strSql, connectionString, out list, out errMsg);
            int flag = -1;
            try
            {
                list = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        #endregion

        #region 生成GSP销售出库单
        /// <summary>
        /// 生成GSP销售出库质量复核记录单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveSaleOutGSP(SaleOutGSPVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            Exception ex;
            //LogNote ln = new LogNote("log.txt");
            int int_i;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            string spid = null;
            string cSeed = string.Empty;
            object ocode = null;
            int icode = 0;
            string ccode = null;
            try
            {
                int id;
                int autoid;
                string dd = null;
                string dt = null;
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataSet UA_Identity = new DataSet();
                cmd.CommandText = "select max(ID),(select max(AUTOID) from GSP_VouchsNote) from GSP_VouchNote";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                if (UA_Identity.Tables[0].Rows.Count == 0)
                {
                    id = 1;
                    autoid = 1;
                }
                else
                {
                    id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);
                    autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);
                }

                /*
                cmd.CommandText = @"select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='010' and cContent is NULL";
                ocode = cmd.ExecuteScalar();
                if (ocode == null)
                {
                    icode = 1;
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString().PadLeft(10, '0');
                */
                ///单据编号修改规则
                ///销售出库GSP单：单据年月日（8位）+流水号（4位）
                ///流水号：根据单据日期，规则：日
                cSeed = DateTime.Now.ToString("yyyyMMdd");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (ROWLOCK)  Where  CardNumber='010' and cContent='单据日期' and cSeed='{0}'", cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('010','单据日期','日','{0}','1')", cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='010' and cContent='单据日期' and cSeed='{1}'", icode, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                ///流水号为当日的4位
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                id++;
                sql = @"insert into GSP_VouchNote(ID,NOTEID,DDATE,CMAKER,CVOUCHTYPE,IVTID,BREFER,CDEFINE2,CDEFINE3,CDEFINE7,CDEFINE11,cverifier)";
                sql += @"Values (@ID,@NOTEID,@DDATE,@CMAKER,N'010',230,1,@CDEFINE2,@CDEFINE3,@CDEFINE7,@CDEFINE11,@cverifier)";
                sql = sql.Replace(@"@ID", id.ToString());
                sql = sql.Replace(@"@NOTEID", SelSql(ccode));
                sql = sql.Replace(@"@DDATE", "'" + dd + "'");
                sql = sql.Replace(@"@CMAKER", SelSql(dl.CMAKER));
                sql = sql.Replace(@"@CDEFINE2", SelSql(dl.CDEFINE2));
                sql = sql.Replace(@"@CDEFINE3", SelSql(dl.CDEFINE3));
                sql = sql.Replace(@"@CDEFINE7", dl.CDEFINE7.ToString());
                sql = sql.Replace(@"@CDEFINE11", SelSql(dl.CDEFINE11));
                sql = sql.Replace(@"@cverifier", SelSql(dl.CMAKER)); //2012-09-06添加申核人，制作单后，直接申核完成
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                foreach (GSPVouchDetail detail in dl.OperateDetails)
                {
                    autoid++;
                    sql = @"Insert Into GSP_VouchsNote(AUTOID,DARVDATE,CINVCODE,CBATCH,ID,FQUANTITY,DPRODATE,CWHCODE,CVALDATE,BFLAG,ICODE_T,CCUSCODE,CDEFINE22,CWHCODES,imassDate,cMassUnit,ICODE,CCODE,CSHIPPER,DValDate,CRESULT)"
                           + @" Values (@AUTOID,@DARVDATE,@CINVCODE,@CBATCH,@ID,@FQUANTITY,@DPRODATE,@cbuscode,@CVALDATE,0,@ICODE_T,@CCUSCODE,@CDEFINE22,@CWHCODES,@imassDate,2,@ICODE,@CCODE,@CSHIPPER,@DValDate,@CRESULT)";
                    sql = sql.Replace("@AUTOID", autoid.ToString());
                    sql = sql.Replace("@DARVDATE", "'" + detail.ddate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@CINVCODE", SelSql(detail.cinvcode));
                    sql = sql.Replace("@CBATCH", SelSql(detail.cbatch));
                    sql = sql.Replace("@ID", id.ToString());
                    sql = sql.Replace("@FQUANTITY", detail.FQUANTITY.ToString());
                    sql = sql.Replace("@DPRODATE", "'" + detail.dmadedate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@cbuscode", SelSql(detail.cbuscode));
                    sql = sql.Replace("@CVALDATE", "'" + detail.dvdate.AddDays(-1).ToString("yyyy-MM-dd") + "'");//有效期至
                    sql = sql.Replace("@ICODE_T", detail.autoid);
                    sql = sql.Replace("@CCUSCODE", SelSql(detail.ccuscode));
                    sql = sql.Replace("@CDEFINE22", SelSql(detail.cdefine22));
                    sql = sql.Replace("@CWHCODES", SelSql(detail.cwhcode));
                    sql = sql.Replace("@imassDate", detail.imassdate.ToString());
                    //sql = sql.Replace("@cMassUnit", detail.CMASSUNIT.ToString());
                    sql = sql.Replace("@ICODE", detail.ID.ToString());
                    sql = sql.Replace("@CCODE", SelSql(detail.cCode));
                    sql = sql.Replace("@CSHIPPER", SelSql(detail.cmaker));
                    sql = sql.Replace("@DValDate", "'" + detail.dvdate.ToString("yyyy-MM-dd") + "'");//失效日期
                    sql = sql.Replace("@CRESULT",SelSql(detail.CRESULT));//质量情况
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    sql = "update rdrecords set cgspstate='已复核' where AUTOID=" + detail.autoid;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }

                

                //sql = "select @@spid ";
                //cmd.CommandText = sql;
                //spid = cmd.ExecuteScalar().ToString();
                //if (spid == null)
                //{
                //    myTran.Rollback();
                //    return -1;
                //}
                //sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) where a.transactionid='spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'SA',1,1,1,1,1,1,1,1,1,1,1,0,1 ,0,'spid_" + spid + "'";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //string sqlUpidentity = string.Concat(new object[] { "update UFSystem..UA_Identity set ifatherid=", id, ",ichildid=", autoid, " where cvouchtype='rd' and cAcc_id=", accid });
                //cmd.CommandText = sqlUpidentity;
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "update voucherhistory set cnumber=" + ccode.ToString() + " where cardnumber='010' and cContent is NULL";
                //cmd.ExecuteNonQuery();
                myTran.Commit();
                int_i = 0;
            }
            catch (Exception exception2)
            {
                //ln.Write(exception2.Message, sql);
                ex = exception2;
                errMsg = ex.Message;
                myTran.Rollback();
                int_i = -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return int_i;
        }

        /// <summary>
        /// 生成GSP中药材、饮片销售出库质量复核记录单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveSaleOutGSP_CHM(SaleOutGSPVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            Exception ex;
            //LogNote ln = new LogNote("log.txt");
            int int_i;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            string spid = null;
            object ocode = null;
            string cSeed = string.Empty;
            int icode = 0;
            string ccode = null;
            try
            {
                int id;
                int autoid;
                string dd = null;
                string dt = null;
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataSet UA_Identity = new DataSet();
                cmd.CommandText = "select max(ID),(select max(AUTOID) from GSP_VOUCHSREGISTER) from GSP_VOUCHREGISTER";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                if (UA_Identity.Tables[0].Rows.Count == 0)
                {
                    id = 1;
                    autoid = 1;
                }
                else
                {
                    //id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);
                    //autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);
                    
                    //2012-09-25改
                    //如果表中数据为空，则查出的结果为行数为1   null,null,而不是0行,所以修改如下
                    DataRow row = UA_Identity.Tables[0].Rows[0];
                    id = row[0] == DBNull.Value ? 1 : Convert.ToInt32(row[0]);
                    autoid = row[1] == DBNull.Value ? 1 : Convert.ToInt32(row[1]);

                }

                /*
                cmd.CommandText = @"select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='051' and cContent is NULL";
                ocode = cmd.ExecuteScalar();
                if (ocode == null)
                {
                    icode = 1;
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString().PadLeft(10, '0');
                 * 
                 */

                ///单据编号修改规则
                ///销售出库GSP单：单据年月日（8位）+流水号（4位）
                ///流水号：根据单据日期，规则：日
                cSeed = DateTime.Now.ToString("yyyyMMdd");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (ROWLOCK)  Where  CardNumber='051' and cContent='单据日期' and cSeed='{0}'", cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('051','单据日期','日','{0}','1')", cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='051' and cContent='单据日期' and cSeed='{1}'", icode, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                ///流水号为当日的4位
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                id++;
                sql = @"insert into GSP_VOUCHREGISTER(ID,CRID,DDATE,CMAKER,CVERIFIER,CVOUCHTYPE,IVTID,BREFER,CDEFINE2,CDEFINE3,CDEFINE7,CDEFINE11)
                        values(@ID,@CRID,@DDATE,@CMAKER,@CVERIFIER,'051',271,1,@CDEFINE2,@CDEFINE3,@CDEFINE7,@CDEFINE11 )";   
                sql = sql.Replace(@"@ID", id.ToString());
                sql = sql.Replace(@"@CRID", SelSql(ccode));
                sql = sql.Replace(@"@DDATE", "'" + dd + "'");
                sql = sql.Replace(@"@CMAKER", SelSql(dl.CMAKER));
                sql = sql.Replace(@"@CDEFINE2", SelSql(dl.CDEFINE2));
                sql = sql.Replace(@"@CDEFINE3", SelSql(dl.CDEFINE3));
                sql = sql.Replace(@"@CDEFINE7", dl.CDEFINE7.ToString());
                sql = sql.Replace(@"@CDEFINE11", SelSql(dl.CDEFINE11));
                sql = sql.Replace(@"@CVERIFIER", SelSql(dl.CMAKER)); //2012-09-17添加申核人，制作单后，直接申核完成
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                foreach (GSPVouchDetail detail in dl.OperateDetails)
                {
                    autoid++;
                    sql = @"insert into GSP_VOUCHSREGISTER(AUTOID,ID,CVENCODE,DVDATE,CINVCODE,FQTY,CBATCH,ICODE_T,BFLAG,DPRODATE,CVALDATE,CDEFINE22,CWHCODE,imassDate,cMassUnit,ICODE,CCODE,CHANDLER,DValDate,CRESULT_T) 
			                                        values(@AUTOID,@ID,@CVENCODE,@DVDATE,@CINVCODE,@FQTY,@CBATCH,@ICODE_T,0,@DPRODATE,@CVALDATE,@CDEFINE22,@CWHCODE,@imassDate,2,@ICODE,@CCODE,@CHANDLER,@DValDate,@CRESULT_T)";
                    
                    sql = sql.Replace("@AUTOID", autoid.ToString());//自动编号
                    sql = sql.Replace("@ID", id.ToString());//主表ID
                    sql = sql.Replace("@CVENCODE", SelSql(detail.ccuscode));//客户编码
                    sql = sql.Replace("@DVDATE", SelSql(dd));
                    sql = sql.Replace("@CINVCODE", SelSql(detail.cinvcode));//存货编码
                    sql = sql.Replace("@FQTY", detail.FQUANTITY.ToString());//数量
                    sql = sql.Replace("@CBATCH", SelSql(detail.cbatch));//批次
                    sql = sql.Replace("@ICODE_T", detail.autoid);//药品抽检或送验单标识 
                    sql = sql.Replace("@DPRODATE", SelSql(detail.dmadedate.ToString("yyyy-MM-dd")));//生产日期
                    sql = sql.Replace("@CVALDATE",  SelSql(detail.dvdate.AddDays(-1).ToString("yyyy-MM-dd")));//有效期
                    sql = sql.Replace("@CDEFINE22", SelSql(detail.cdefine22));//表头自定义项22
                    sql = sql.Replace("@CWHCODE", SelSql(detail.cwhcode));//仓库编码
                    sql = sql.Replace("@imassDate", detail.imassdate.ToString());//保质期
                    //sql = sql.Replace("@cMassUnit", detail.CMASSUNIT.ToString());//保质期单位
                    sql = sql.Replace("@ICODE", detail.ID.ToString());
                    sql = sql.Replace("@CCODE", SelSql(detail.cCode));//出库单号
                    sql = sql.Replace("@CHANDLER", SelSql(detail.cmaker));//发货人  
                    sql = sql.Replace("@DValDate", SelSql(detail.dvdate.ToString("yyyy-MM-dd")));//失效日期
                    sql = sql.Replace("@CRESULT_T", SelSql(detail.CRESULT));//质量情况
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    sql = "update rdrecords set cgspstate='已复核' where AUTOID=" + detail.autoid;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }

                //cmd.CommandText = "update voucherhistory set cnumber=" + ccode.ToString() + " where cardnumber='051' and cContent is NULL ";
                //cmd.ExecuteNonQuery();

                myTran.Commit();
                int_i = 0;
            }
            catch (Exception exception2)
            {
                //ln.Write(exception2.Message, sql);
                ex = exception2;
                errMsg = ex.Message;
                myTran.Rollback();
                int_i = -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return int_i;
        }

        #endregion

        #region 生成GSP销售退货单
        public static int SaveSaleBackGSP(SaleBackGSPVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            Exception ex;
            //LogNote ln = new LogNote();
            int int_i;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            string spid = null;
            object ocode = null;
            string cSeed = string.Empty;
            int icode = 0;
            string ccode = null;
            try
            {
                int id;
                int autoid;
                string dd = null;
                string dt = null;
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataSet UA_Identity = new DataSet();
                cmd.CommandText = "select max(ID),(select max(AUTOID) from GSP_VouchsQC) from GSP_VouchQC";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                if (UA_Identity.Tables[0].Rows.Count == 0||UA_Identity.Tables[0].Rows[0][0].Equals(DBNull.Value))
                {
                    id = 1;
                    autoid = 1;
                }
                else
                {
                    id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);
                    autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);
                }

                /*
                cmd.CommandText = @"select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='004' and cContent is NULL";
                ocode = cmd.ExecuteScalar();
                if (ocode == null)
                {
                    icode = 1;
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString().PadLeft(10, '0');
                 * 
                 * 
                 * */

                ///单据编号修改规则
                ///GSP单：单据年月（6位）+流水号（4位）
                ///流水号：根据单据日期，规则：月
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (ROWLOCK)  Where  CardNumber='004' and cContent='制单日期' and cSeed='{0}'", cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('004','制单日期','月','{0}','1')", cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='004' and cContent='制单日期' and cSeed='{1}'", icode, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                ///流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                id++;
                sql = @"insert into GSP_VouchQC(ID,QCID,ICODE,CCODE,DARVDATE,CMAKER,DDATE,CVOUCHTYPE,IVTID,BREFER,CVERIFIER,CDEFINE2,CDEFINE11)";
                sql += @"Values (@ID,@QCID,@ICODE,@CCODE,@DARVDATE,@CMAKER,@DDATE,'004',224,1,@CVERIFIER,@CDEFINE2,@CDEFINE11)";
                sql = sql.Replace(@"@ID", id.ToString());
                sql = sql.Replace(@"@QCID", SelSql(ccode));
                sql = sql.Replace(@"@ICODE", dl.ICODE);
                sql = sql.Replace(@"@CCODE", SelSql(dl.CCODE));
                sql = sql.Replace(@"@DARVDATE", "'" + dl.DARVDATE + "'");
                sql = sql.Replace(@"@CMAKER", SelSql(dl.CMAKER));
                sql = sql.Replace(@"@DDATE", "'" + dd + "'");
                sql = sql.Replace(@"@CVERIFIER", SelSql(dl.CMAKER));//20120907,添加申核人
                sql = sql.Replace(@"@CDEFINE2", SelSql(dl.OperateDetails[0].ccusname));//客户名称
                sql = sql.Replace(@"@CDEFINE11", SelSql(dl.OperateDetails[0].ccusname));//客户名称
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                foreach (SaleBackGSPDetail detail in dl.OperateDetails)
                {
                    autoid++;
                    sql = @"Insert Into GSP_VouchsQC(AUTOID,ID,CINVCODE,FQUANTITY,FARVQUANTITY,DPRODATE,DVDATE,CVALDATE,DDATE_T,FELGQUANTITY,FNELGQUANTITY,BSPECIAL,BNAME,BSPEC,BPROC,CBATCH,BFINISH,CCUSCODE,BFLAG,CDEFINE22,ICODE_T,BCHECK,BMAKESCRAP,BMAKEPURIN,BMAKESALEOUT,CWHCODE,imassDate,cMassUnit,DValDate,COUTINSTANCE)"
                           + @" Values (@AUTOID,@ID,@CINVCODE,@FQUANTITY,@FARVQUANTITY,@DPRODATE,@DVDATE,@CVALDATE,@DDATE_T,@FELGQUANTITY,@FNELGQUANTITY,0,0,0,0,@CBATCH,0,@CCUSCODE,0,@CDEFINE22,@ICODE_T,0,0,0,0,@CWHCODE,@imassDate,2,@DValDates,@COUTINSTANCE)";
                    sql = sql.Replace("@AUTOID", autoid.ToString());
                    sql = sql.Replace("@ID", id.ToString());
                    sql = sql.Replace("@CINVCODE", SelSql(detail.cinvcode));
                    sql = sql.Replace("@FQUANTITY", detail.FQUANTITY.ToString());
                    sql = sql.Replace("@FARVQUANTITY", detail.FARVQUANTITY.ToString());
                    sql = sql.Replace("@DPRODATE", "'" + detail.DPRODATE + "'");
                    sql = sql.Replace("@DVDATE", "'" + detail.DVDATE + "'");
                    sql = sql.Replace("@CVALDATE", "'" + Convert.ToDateTime(detail.DVDATE).AddDays(-1).ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@DDATE_T", "'" +dd+ "'");
                    sql = sql.Replace("@FELGQUANTITY", detail.FELGQUANTITY.ToString());
                    if (detail.FELGQUANTITY != detail.FQUANTITY)
                    {
                        sql = sql.Replace("@FNELGQUANTITY", (detail.FQUANTITY - detail.FELGQUANTITY).ToString());
                    }
                    else
                    {
                        sql = sql.Replace("@FNELGQUANTITY", 0.ToString());
                    }
                    sql = sql.Replace("@CBATCH", SelSql(detail.CBATCH));
                    sql = sql.Replace("@CCUSCODE", SelSql(detail.CCUSCODE));
                    sql = sql.Replace("@CDEFINE22", SelSql(detail.CDEFINE22));
                    sql = sql.Replace("@ICODE_T", detail.ICODE_T);
                    sql = sql.Replace("@CWHCODE", SelSql(detail.cwhcode));
                    sql = sql.Replace("@imassDate", detail.imassDate.ToString());
                    sql = sql.Replace("@DValDates", "'" + detail.DVDATE + "'");
                    sql = sql.Replace("@COUTINSTANCE", SelSql(detail.COUTINSTANCE));
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    sql = "update DispatchLists set cgspstate ='已验收' where idlsid=" + detail.ICODE_T;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }



                //sql = "select @@spid ";
                //cmd.CommandText = sql;
                //spid = cmd.ExecuteScalar().ToString();
                //if (spid == null)
                //{
                //    myTran.Rollback();
                //    return -1;
                //}
                //sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) where a.transactionid='spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'SA',1,1,1,1,1,1,1,1,1,1,1,0,1 ,0,'spid_" + spid + "'";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //string sqlUpidentity = string.Concat(new object[] { "update UFSystem..UA_Identity set ifatherid=", id, ",ichildid=", autoid, " where cvouchtype='rd' and cAcc_id=", accid });
                //cmd.CommandText = sqlUpidentity;
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "update voucherhistory set cnumber=" + icode.ToString() + " where cardnumber='004' and cContent is NULL ";
                //cmd.ExecuteNonQuery();
                myTran.Commit();
                int_i = 0;
            }
            catch (Exception exception2)
            {
                //ln.Write(exception2.Message, sql);
                ex = exception2;
                errMsg = ex.Message;
                myTran.Rollback();
                int_i = -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return int_i;
        }
        #endregion

        #region 验证采购入库单红字单号并获取子表信息
        public static int GetPurchaseBack(string CCODE, string connectionString, out DataSet list, out string errMsg)
        {
            list = null;
            errMsg = "";
            string strSql = string.Format(@"select ID,ccode,darvdate,zpurRkdList.cvencode,vendor.cvenname as cvenname,cinvcode,cinvname,cinvstd,cbatch,iquantity,round(inum,8) as inum,cwhcode,cwhname,autoid,dmadedate,zpurRkdList.cmemo,cmaker,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,cinva_unit,cinvm_unit,ltrim(rtrim(str(iinvexchrate,19,5))) as iinvexchrate,cassunit,id,ufts,imassdate,zpurRkdList.CMASSUNIT ,(CASE [dbo].[Inventory_Sub].[iExpiratDateCalcu] WHEN 0 THEN '' WHEN 1 THEN '月' WHEN 2 THEN '日'  END) AS [IEXPIRATDATECALCU], case  isnull([Inventory_Sub].[iExpiratDateCalcu],0) when 1 then convert(varchar ,Year(dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(dExpirationdate) ,2),2) when 2 then convert(varchar ,Year(dExpirationdate) ,4)+'-'+ RIGHT('00'+convert(varchar,month(dExpirationdate) ,2),2)+'-'+RIGHT('00'+convert(varchar,day(dExpirationdate), 2),2) Else '' end  as CVALDATES, cbatchproperty1,cbatchproperty2,cbatchproperty3,cbatchproperty4,cbatchproperty5,cbatchproperty6,cbatchproperty7,cbatchproperty8,cbatchproperty9,cbatchproperty10   
                            from zpurRkdList 
                            inner join vendor on vendor.cvencode=zpurRkdList.cvencode 
                            INNER JOIN [dbo].[Inventory_Sub] ON  [dbo].[zpurRkdList].[CINVCODE]=[dbo].[Inventory_Sub].[cInvSubCode] 
                            where ddate>=N'2007-03-01' and isnull(bpufirst,0)=0 
                            and isnull(biafirst,0)=0  and ddate>=N'2007-03-01' 
                            and isnull(cgspstate,'')=N'' and isnull(cHandler,'')<>N'' and isnull(iquantity,0)<0 
                            And CCODE = N'{0}' 
                            order by ID ",CCODE);
            //return OperationSql.GetDataset(strSql, connectionString, out list, out errMsg);
            int flag = -1;
            try
            {
                list = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        #endregion

        #region 生成GSP采购退货单
        public static int SavePurchaseBackGSP(PurchaseBackVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            Exception ex;
            //LogNote ln = new LogNote();//(@"E:\项目\东兴堂\Code\project\DXTHTApp\BarcodeService\log.txt");
            int int_i;
            errMsg = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter adp = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
            }
            catch (Exception exception1)
            {
                ex = exception1;
                errMsg = ex.Message;
                return 1;
            }
            SqlTransaction myTran = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = myTran;
            DataSet result = new DataSet();
            string sql = null;
            string spid = null;
            object ocode = null;
            string cSeed = string.Empty;
            int icode = 0;
            string ccode = null;
            try
            {
                int id;
                int autoid;
                string dd = null;
                string dt = null;
                dd = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataSet UA_Identity = new DataSet();
                cmd.CommandText = "select max(ID),(select max(AUTOID) from GSP_VOUCHSUNSALABLE) from GSP_VOUCHUNSALABLE";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                if (UA_Identity.Tables[0].Rows.Count == 0)
                {
                    id = 1;
                    autoid = 1;
                }
                else
                {
                    id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);
                    autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);
                }

                /*
                cmd.CommandText = @"select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='091' and cContent is NULL";
                ocode = cmd.ExecuteScalar();
                if (ocode == null)
                {
                    icode = 1;
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                }
                ccode = icode.ToString().PadLeft(10, '0');
                 * 
                 * */

                ///单据编号修改规则
                ///GSP单：单据年月（6位）+流水号（4位）
                ///流水号：根据单据日期，规则：月
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (ROWLOCK)  Where  CardNumber='091' and cContent='制单日期' and cSeed='{0}'", cSeed);
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('091','制单日期','月','{0}','1')", cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='091' and cContent='制单日期' and cSeed='{1}'", icode, cSeed);
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                ///流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                id++;
                sql = @"insert into GSP_VOUCHUNSALABLE(ID,cVouchCode,cVouchType,dDate,iRdId,cRdCode,cVenCode,cRdMaker,cMaker,cWhCode,bRefer,cDefine1,cDefine7,cverifier,dArvdate)";
                sql += @"Values (@ID,@cVouchCode,'091',@dDate,@iRdId,@cRdCode,@cVenCode,@cRdMaker,@cMaker,@cWhCode,1,@cDefine1,0,@cverifier,@dArvdate)";
                sql = sql.Replace(@"@ID", id.ToString());
                sql = sql.Replace(@"@cVouchCode", SelSql(ccode));
                sql = sql.Replace(@"@dDate", "'" + dd + "'");
                sql = sql.Replace(@"@iRdId", dl.iRdId);
                sql = sql.Replace(@"@cRdCode", SelSql(dl.cRdCode));
                sql = sql.Replace(@"@dArvdate", "'" + dl.dArvdate + "'");
                sql = sql.Replace(@"@cVenCode", SelSql(dl.cVenCode));
                sql = sql.Replace(@"@cRdMaker", SelSql(dl.cRdMaker));
                sql = sql.Replace(@"@cMaker", SelSql(dl.cMaker));
                sql = sql.Replace(@"@cWhCode", SelSql(dl.cWhCode));
                sql = sql.Replace(@"@cDefine1", SelSql(dl.cDefine1));
                ///20120908添加申核人
                sql = sql.Replace(@"@cverifier", SelSql(dl.cMaker));
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                foreach (PurchaseBackDetail detail in dl.OperateDetails)
                {
                    autoid++;
                    sql = @"Insert Into GSP_VOUCHSUNSALABLE(AutoID,ID,iRdsID,cInvcode,dMadeDate,cBatch,dValDate,iQuantity,cDefine22,imassDate,cMassUnit,CValDate,cInstance)"
                           + @" Values (@AutoID,@ID,@iRdsID,@cInvcode,@dMadeDate,@cBatch,@dValDate,@iQuantity,@cDefine22,@imassDate,2,@CValDate,@cInstance)";
                    sql = sql.Replace("@AutoID", autoid.ToString());
                    sql = sql.Replace("@ID", id.ToString());
                    sql = sql.Replace("@iRdsID", SelSql(detail.IRdsID));
                    sql = sql.Replace("@cInvcode", SelSql(detail.cInvcode));
                    sql = sql.Replace("@dMadeDate", "'" + detail.dMadeDate + "'");
                    sql = sql.Replace("@cBatch", SelSql(detail.cBatch));
                    sql = sql.Replace("@dValDate", "'" + detail.dValDate + "'");//失效日期
                    sql = sql.Replace("@iQuantity", detail.iQuantity.ToString());
                    sql = sql.Replace("@cDefine22", SelSql(detail.cdefine22));
                    sql = sql.Replace("@imassDate", detail.imassDate.ToString());
                    sql = sql.Replace("@CValDate", "'" + Convert.ToDateTime(detail.CValDate).ToString("yyyy-MM-dd") + "'");//有效期至
                    sql = sql.Replace("@cInstance",SelSql(detail.cInstance));//质量情况
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    sql = "update rdrecords set cgspstate ='已复核' where AUTOID=" + detail.IRdsID;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                }



                //sql = "select @@spid ";
                //cmd.CommandText = sql;
                //spid = cmd.ExecuteScalar().ToString();
                //if (spid == null)
                //{
                //    myTran.Rollback();
                //    return -1;
                //}
                //sql = "insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10) select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) where a.transactionid='spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'SA',1,1,1,1,1,1,1,1,1,1,1,0,1 ,0,'spid_" + spid + "'";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //string sqlUpidentity = string.Concat(new object[] { "update UFSystem..UA_Identity set ifatherid=", id, ",ichildid=", autoid, " where cvouchtype='rd' and cAcc_id=", accid });
                //cmd.CommandText = sqlUpidentity;
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "update voucherhistory set cnumber=" + icode.ToString() + " where cardnumber='091' and cContent is NULL ";
                //if (cmd.ExecuteNonQuery() < 1)
                //{
                //    myTran.Rollback();
                //    return -1;
                //}
                myTran.Commit();
                int_i = 0;
            }
            catch (Exception exception2)
            {
                //ln.Write(exception2.Message, sql);
                ex = exception2;
                errMsg = ex.Message;
                myTran.Rollback();
                int_i = -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return int_i;
        }
        #endregion

        #region Replace Function
        public static string SelSql(string str)
        {
            if (str == "null" || str == "")
                return "N''";
            else
                return "N'" + str + "'";
        }
        #endregion
    }
}
