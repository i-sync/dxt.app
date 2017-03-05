using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;

namespace U8DataAccess
{
    /// <summary>
    /// 产成品入库
    /// </summary>
    public class STInProductProcess
    {     
        public static int GetSTInProduct(string cInvCode, string connectionString, out DataSet list, out string errMsg)
        {
            list = null;
            errMsg = "";
            string strSql = @"SELECT [KCInventoryEntity_Inventory].[cInvCode] as cInvCode,
                            [KCInventoryEntity_Inventory].[cInvAddCode] as cInvAddCode,
                            [KCInventoryEntity_Inventory].[cInvName] as cInvName,
                            [KCInventoryEntity_Inventory].[cInvStd] as cInvStd,
                            [KCInventoryEntity_Inventory].[cInvDefine1] as cInvDefine1,
                            [KCInventoryEntity_Inventory].[cInvCCode] as cInvCCode,
                            [KCInventoryEntity_Inventory].[iMassDate] as iMassDate,
                            [KCInventoryEntity_Inventory].[cMassUnit] as cMassUnit,
                            [KCInventoryEntity_Inventory].[iInvRCost] as iInvRCost,
                            [KCInventoryEntity_Inventory].[dSDate] as dSDate,
                            [KCInventoryEntity_Inventory].[dEDate] as dEDate,
                            [KCInventoryEntity_Inventory].[cInvDefine2] as cInvDefine2,
                            [KCInventoryEntity_ComputationUnit].[cGroupCode] as cGroupCode,
                            [KCInventoryEntity_Inventory].[cComUnitCode] as cComUnitCode,
                            [KCInventoryEntity_ComputationUnit].[cComUnitName] as cComUnitName,
                            [KCInventoryEntity_InventoryClass].[cInvCName] as cInvCName ,
                            [KCInventoryEntity_Inventory].[cSTComUnitCode] as cSTComUnitCode,
                            [COMUNIT].[iChangRate] as iinvexchrate
                            FROM [Inventory] AS [KCInventoryEntity_Inventory]
                            LEFT JOIN [ComputationUnit] AS [KCInventoryEntity_ComputationUnit] ON  [KCInventoryEntity_ComputationUnit].[cGroupCode] = [KCInventoryEntity_Inventory].[cGroupCode] and  [KCInventoryEntity_ComputationUnit].[cComunitCode] = [KCInventoryEntity_Inventory].[cComUnitCode]
                            LEFT JOIN [InventoryClass] AS [KCInventoryEntity_InventoryClass] ON  [KCInventoryEntity_InventoryClass].[cInvCCode] = [KCInventoryEntity_Inventory].[cInvCCode]
                            LEFT JOIN [ComputationUnit] AS [COMUNIT] ON [KCInventoryEntity_Inventory].[cSTComUnitCode] = [COMUNIT].[cComunitCode]
                            WHERE [KCInventoryEntity_Inventory].[bService]=0 
                            And [KCInventoryEntity_Inventory].[bInvType]=0  
                            and [KCInventoryEntity_Inventory].[bSelf]=1   
                            and datediff(day,isnull([KCInventoryEntity_Inventory].[dEDate],N'9999-12-31'),GETDATE())<=0 
                            AND isnull([KCInventoryEntity_Inventory].[bPlanInv],0)=0 
                            and isnull([KCInventoryEntity_Inventory].[bCheckItem],0)=0 
                            and isnull([KCInventoryEntity_Inventory].[bPTOModel],0)=0
                            and [KCInventoryEntity_Inventory].[cInvCode] = N'" + cInvCode + "'";
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

        #region 生成产成品入库单
        public static int SaveProductIn(STInProduct dl, string connectionString, string accid, string year, out string errMsg)
        {
            Exception ex;
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
                string cVouchName = "产成品入库单";
                string cardnumber, vt_id;//单据类型编码 模板号
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
                id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid
                cmd.CommandText = "select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码

                id = id + 1;

                sql = @"insert into rdrecord(id,brdflag,cvouchtype,cbustype,csource,cwhcode,ddate,ccode,crdcode,cmaker,cdefine10,bpufirst,biafirst,vt_id,bisstqc,iproorderid,iswfcontrolled,dnmaketime,dnmodifytime,cHandler,dVeriDate,dNVerifyTime)";
                sql += @"Values (@id,@brdflag,@cvouchtype,@cbustype,@csource,@cwhcode,@ddate,@ccode,@crdcode,@cmaker,@cdefine10,@bpufirst,@biafirst,@vt_id,@bisstqc,@iproorderid,@iswfcontrolled,@dnmaketime,Null,@cHandler,@dVeriDate,@dNVerifyTime)";
                sql = sql.Replace(@"@id", id.ToString());
                sql = sql.Replace(@"@brdflag", dl.brdflag.ToString());
                sql = sql.Replace(@"@cvouchtype", SelSql(dl.cvouchtype));
                sql = sql.Replace(@"@cbustype", SelSql(dl.cbustype));
                sql = sql.Replace(@"@csource", SelSql(dl.csource));
                sql = sql.Replace(@"@cwhcode", SelSql(dl.cwhcode));
                sql = sql.Replace(@"@ddate", "'" + dd + "'");
                sql = sql.Replace(@"@ccode", SelSql(id.ToString()));
                sql = sql.Replace(@"@crdcode", SelSql(dl.crdcode));
                sql = sql.Replace(@"@cmaker", SelSql(dl.cmaker));
                sql = sql.Replace(@"@cdefine10", SelSql(dl.cdefine10));
                sql = sql.Replace(@"@bpufirst", dl.bpufirst.ToString());
                sql = sql.Replace(@"@biafirst", dl.biafirst.ToString());
                sql = sql.Replace(@"@vt_id", dl.vt_id.ToString());
                sql = sql.Replace(@"@bisstqc", dl.bisstqc.ToString());
                sql = sql.Replace(@"@iproorderid", dl.iproorderid.ToString());
                sql = sql.Replace(@"@iswfcontrolled", dl.iswfcontrolled.ToString());
                sql = sql.Replace(@"@dnmaketime", "'" + dt + "'");

                //添加申核人，申核日期，申核时间
                sql = sql.Replace(@"@cHandler", SelSql(dl.cmaker));
                sql = sql.Replace(@"@dVeriDate", SelSql(dd));
                sql = sql.Replace(@"@dNVerifyTime", SelSql(dt));

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                int irowno = 1;

                ///
                ///2012-09-10 修改
                //已插入的批次
                //List<string> list = new List<string>();
                ///存货已经添加过的存货批次
                List<STInProductDetail> list = new List<STInProductDetail>();
                foreach (STInProductDetail detail in dl.OperateDetails)
                { 
                    //如果该批次已经插入 则跳过本次循环
                    //如果同一存货该批次已经插入 则跳过本次循环
                    bool flag = false;//标识存货的某一批次是否已经插入，默认为false;
                    foreach (STInProductDetail l in list)
                    {
                        if (l.cinvcode == detail.cinvcode && l.cbatch == detail.cbatch)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)//某存货的一个批次已经插入，跳出循环。
                    {
                        continue;
                    }

                    //在集合中查询所有批次为detail.Cbatch的对象
                    var v = from od in dl.OperateDetails where od.cinvcode == detail.cinvcode && od.cbatch == detail.cbatch select od;
                    int num = 0;//记录同一批次的货位个数
                    decimal iquantity = 0;//记录该批次货物总数量
                    autoid++;//rdrecords子表主键
                    foreach (STInProductDetail temp in v)
                    {
                        //如果货位信息为空，别表示不插入货位
                        if (string.IsNullOrEmpty(temp.cposition))
                        {
                            iquantity = temp.iquantity;
                            break;
                        }

                        //插入货位信息表
                        sql = @"insert into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)
                                    values(@rdsid,@rdid,@cwhcode,@cposcode,@cinvcode,@cbatch,null,null,@dvdate,@iquantity,@inum,null,@chandler,@ddate,1,null,null,null,null,null,null,null,null,null,@cassunit,null,null,@dmadedate,@imassdate,@cmassunit,null,2,@cexpirationdate,@dexpirationdate)";
                        sql = sql.Replace("@rdsid", autoid.ToString());
                        sql = sql.Replace("@rdid", SelSql(id.ToString ()));
                        sql = sql.Replace("@cwhcode", SelSql(temp.cwhcode));
                        sql = sql.Replace("@cposcode", SelSql(temp.cposition));
                        sql = sql.Replace("@cinvcode", SelSql(temp.cinvcode));
                        sql = sql.Replace("@cbatch", SelSql(temp.cbatch));

                        sql = sql.Replace("@dvdate", "'" + detail.dvdate + "'");
                        sql = sql.Replace("@iquantity", temp.iquantity.ToString());
                        //件数
                        if (temp.cassunit != null && temp.iinvexchrate != 0)
                        {
                            temp.inum = temp.iquantity / temp.iinvexchrate;
                            temp.inum = decimal.Round(temp.inum, 2);
                            sql = sql.Replace("@inum", temp.inum.ToString());
                        }
                        else
                        {
                            sql = sql.Replace("@inum", "null");
                        }

                        sql = sql.Replace("@chandler", SelSql(dl.cmaker));//子表中没有经手人
                        sql = sql.Replace("@ddate", "'" + dd + "'");

                        sql = sql.Replace("@cassunit", SelSql(temp.cassunit));
                        sql = sql.Replace("@dmadedate", "'"+temp.dmadedate+"'");
                        sql = sql.Replace("@imassdate", "datediff(month,'" + detail.dmadedate + "','" + detail.dvdate + "')");//在数据库中计算
                        sql = sql.Replace("@cmassunit", SelSql(temp.cmassunit.ToString()));
                        sql = sql.Replace("@cexpirationdate", "'"+Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd")+"'");//有效期至为失效日期－1
                        sql = sql.Replace("@dexpirationdate", "'" + Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd") + "'");

                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }

                        num++;
                        iquantity += temp.iquantity;
                    }

                    //插入rdrecords数据表
                    #region
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,inum,iquantity,iunitcost,iprice,cbatch,dvdate,cassunit,dmadedate,imassdate,cmassunit,brelated,bcosting,bvmiused,iinvexchrate,iexpiratdatecalcu,irowno,cposition,cExpirationdate,cdefine25)"
                           + @" Values (@autoid,@id,@cinvcode,@inum,@iquantity,@iunitcost,@iprice,@cbatch,@dvdate,@cassunit,@dmadedate,@imassdate,@cmassunit,0,1,0,@iinvexchrate,0,@irowno,@cposition,@cExpirationdate,@cdefine25)";
                    sql = sql.Replace("@autoid", autoid.ToString());
                    sql = sql.Replace("@id", id.ToString());
                    sql = sql.Replace("@cinvcode", SelSql(detail.cinvcode));
                    if (detail.cassunit != null && detail.iinvexchrate != 0)
                    {
                        detail.inum = iquantity / detail.iinvexchrate;
                        detail.inum = decimal.Round(detail.inum, 2);
                        sql = sql.Replace("@inum", detail.inum.ToString());
                    }
                    else
                    {
                        sql = sql.Replace("@inum", "null");
                    }
                    sql = sql.Replace("@iquantity", iquantity.ToString());
                    sql = sql.Replace("@iunitcost", detail.iunitcost.ToString());
                    sql = sql.Replace("@iprice", (iquantity*detail.iunitcost).ToString ());
                    sql = sql.Replace("@cbatch", SelSql(detail.cbatch));
                    sql = sql.Replace("@dvdate", "'" + detail.dvdate + "'");
                    sql = sql.Replace("@cassunit", SelSql(detail.cassunit));
                    //detail.dvdate = detail.dvdate.Substring(0, 4) + "-" + detail.dvdate.Substring(4, 2) + "-" + detail.dvdate.Substring(6, 2);
                    //detail.dmadedate = detail.dmadedate.Substring(0, 4) + "-" + detail.dmadedate.Substring(4, 2) + "-" + detail.dmadedate.Substring(6, 2);
                    sql = sql.Replace("@dmadedate", "'" + detail.dmadedate + "'");
                    sql = sql.Replace("@imassdate", "datediff(month,'" + detail.dmadedate + "','" + detail.dvdate + "')");
                    sql = sql.Replace("@cmassunit", detail.cmassunit.ToString());
                    sql = sql.Replace("@iinvexchrate", detail.iinvexchrate.ToString());
                    sql = sql.Replace("@irowno", irowno.ToString());

                    //添加货位
                    sql = sql.Replace("@cposition", num == 1 ? SelSql(detail.cposition) : "null");
                    //有效期至
                    sql = sql.Replace("@cExpirationdate", SelSql(detail.cExpirationdate));
                    //检验单号
                    sql = sql.Replace("@cdefine25", SelSql(detail.cCheckCode));

                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion

                    //把该存货批次添加到list中
                    list.Add(new STInProductDetail() { cinvcode = detail.cinvcode, cbatch = detail.cbatch });
                }


                #region temp table
                sql = @"if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select id,autoid ,convert(decimal(30,4),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, impoids,icheckidbaks,irejectids,brelated,Corufts ,  convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords  where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"Insert Into #Ufida_WBBuffers select id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), impoids,icheckidbaks,irejectids,brelated, Corufts  as Corufts,2 as iOperate   from rdrecords   where id=" + id.ToString();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //delete temp table
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null ) Drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(isnull(inum,0)) as inum,Max(corufts) as corufts,  max(impoids) as impoids,max(icheckidbaks) as icheckidbaks,max(irejectids) as irejectids,max(convert(smallint,brelated)) as brelated ,sum(iOperate) as iOperate,  case sum(iOperate)  when 3 then N'M' when 2 then N'A' when 1 then N'D' end as editprop  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update  #Ufida_WBBuffers_ST set corufts=null where iOperate<>2 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select top 1 autoid,(case when isnull(irejectids,0)<>0 then 1 when isnull(icheckidbaks,0)<> 0 then 2 when isnull(impoids,0)<> 0 then 3 else -1 end) as sflag   from #Ufida_WBBuffers_ST where isnull(impoids,0)<>0 or isnull(icheckidbaks,0)<>0 or isnull(irejectids,0)<>0 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#STCheckVouchDate') is null ) Drop table #STCheckVouchDate";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select cinvcode,convert(datetime,Null) as dstdate,convert(datetime,Null) as doridate ,convert(nvarchar(800),Null) as error   into #STCheckVouchDate  from inventory where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"exec  USP_STCheckVouchDate  N'10',N'#Ufida_WBBuffers'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers') is null )  drop table #Ufida_WBBuffers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_ST') is null )  drop table #Ufida_WBBuffers_ST";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target') is null ) Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_pu') is null ) Drop table #Ufida_WBBuffers_Target_pu";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"if exists (select 1 where not object_id('tempdb..#Ufida_WBBuffers_Target_track') is null ) Drop table #Ufida_WBBuffers_Target_track";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update #LPWriteBackTbl set inum = case when I.igrouptype =2 then isnull(a.inum,0) else 0 end , cfree1 = case when isnull(I.bconfigfree1,0) =0 then '' else isnull(a.cfree1,'') end , cfree2 = case when isnull(I.bconfigfree2,0) =0 then '' else isnull(a.cfree2,'') end , cfree3 = case when isnull(I.bconfigfree3,0) =0 then '' else isnull(a.cfree3,'') end , cfree4 = case when isnull(I.bconfigfree4,0) =0 then '' else isnull(a.cfree4,'') end , cfree5 = case when isnull(I.bconfigfree5,0) =0 then '' else isnull(a.cfree5,'') end , cfree6 = case when isnull(I.bconfigfree6,0) =0 then '' else isnull(a.cfree6,'') end , cfree7 = case when isnull(I.bconfigfree7,0) =0 then '' else isnull(a.cfree7,'') end , cfree8 = case when isnull(I.bconfigfree8,0) =0 then '' else isnull(a.cfree8,'') end , cfree9 = case when isnull(I.bconfigfree9,0) =0 then '' else isnull(a.cfree9,'') end , cfree10 = case when isnull(I.bconfigfree10,0) =0 then '' else isnull(a.cfree10,'') end  from #LPWriteBackTbl a inner join inventory I on a.cinvcode = I.cinvcode";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,sum(isnull(iquantity,0)) as iquantity,sum(isnull(inum,0)) as inum  into  #LPWriteBackSumTbl from #LPWriteBackTbl group by cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  create index index_lpwritebacksumtbl on #LPWriteBackSumTbl(cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"insert into ST_DemandKeepInfo (cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,idemandtype,cdemandid )   select cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,0,0,isotype,isodid   from #LPWriteBackSumTbl a where not exists (select cinvcode from ST_DemandKeepInfo where cinvcode=a.cinvcode and idemandtype = a.isotype and cdemandid = a.isodid    and cfree1 = a.cfree1 and  cfree2 = a.cfree2 and cfree3 = a.cfree3 and cfree4 = a.cfree4 and cfree5 = a.cfree5 and cfree6 = a.cfree6    and cfree7 = a.cfree7 and cfree8 = a.cfree8 and cfree9 = a.cfree9 and cfree10 = a.cfree10 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update ST_DemandKeepInfo set iquantity =isnull(a.iquantity,0) + isnull(b.iquantity,0) ,inum =isnull(a.inum,0) + isnull(b.inum,0)    from ST_DemandKeepInfo a inner join #LPWriteBackSumTbl b on a.cinvcode =b.cinvcode and a.idemandtype = b.isotype and a.cdemandid = b.isodid  and a.cfree1 =b.cfree1 and a.cfree2 =b.cfree2 and a.cfree3 =b.cfree3 and a.cfree4 =b.cfree4 and a.cfree5 =b.cfree5 and a.cfree6 =b.cfree6  and a.cfree7 =b.cfree7 and a.cfree8 =b.cfree8 and a.cfree9 =b.cfree9 and a.cfree10 =b.cfree10";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                #region procedure
                sql = "exec ST_SaveForStock N'10',N'" + id + "',1,0 ,1";
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

                sql = @"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)"
                     + " select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + " where a.transactionid=N'spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + " and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + " and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                #endregion

                #region 单据号处理
                cSeed = DateTime.Now.ToString("yyMM");
                //sql = "select isnull(cNumber,1) as Maxnumber from voucherhistory with (updLOCK) where cardnumber='" + cardnumber + "' and cContent is NULL ";
                //cmd.CommandText = sql;
                //ocode = cmd.ExecuteScalar();
                ////ln.Write("成品入库单号查询：", sql);
                //if (ocode == null)
                //{
                //    icode = 1;
                //    sql = "Insert into VoucherHistory(CardNumber,cNumber) values('" + cardnumber + "','1')";
                //}
                //else
                //{
                //    icode = int.Parse(ocode.ToString()) + 1;
                //    sql = "update voucherhistory set cnumber=" + icode + " where cardnumber='" + cardnumber + "' and cContent is NULL";
                //}
                //cmd.CommandText = sql;
                ///单据编号修改规则
                ///产成品入库单：单据年月（6位）+流水号（4位）
                ///流水号：根据单据日期，规则：月
                cSeed = DateTime.Now.ToString("yyMM");
                sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='日期' and cSeed='{1}'", cardnumber,cSeed);
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
                //普通销售的流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');
                
                sql = "select id from rdrecord where cvouchtype='10' and ccode='" + ccode + "'";
                cmd.CommandText = sql;
                if (cmd.ExecuteScalar() != null)
                {
                    myTran.Rollback();
                    errMsg = "单据号重复,请再次提交!" + ccode;
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
                if (!string.IsNullOrEmpty(dl.cdefine10))
                {
                    Model.Regulatory data = new Model.Regulatory();
                    data.RegCode = dl.cdefine10;
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


                //修改UA_Identity
                string sqlUpidentity = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + " where cvouchtype='rd' and cAcc_id=" + accid;
                cmd.CommandText = sqlUpidentity;
                cmd.ExecuteNonQuery();


                #region 申核单据
                ///20120908添加
                sql = string.Format(@"if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl
if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl
select cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 into #LPWriteBackTbl from rdrecords with (nolock) where 1=2  create index ix_cinvcode_lpwritebacktbl on #LPWriteBackTbl(cinvcode )
 insert into #LPWriteBackTbl (cinvcode,isotype,isodid,iquantity,inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)  select cinvcode,isotype,isodid,1* iquantity,1* inum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from rdrecords where isnull(isotype,0)>=4 and id ={0}
 --SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 --exec ST_GetInventoryVO  N'SR02020301',N'',0
 --SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
 update #LPWriteBackTbl set inum = case when I.igrouptype =2 then isnull(a.inum,0) else 0 end , cfree1 = case when isnull(I.bconfigfree1,0) =0 then '' else isnull(a.cfree1,'') end , cfree2 = case when isnull(I.bconfigfree2,0) =0 then '' else isnull(a.cfree2,'') end , cfree3 = case when isnull(I.bconfigfree3,0) =0 then '' else isnull(a.cfree3,'') end , cfree4 = case when isnull(I.bconfigfree4,0) =0 then '' else isnull(a.cfree4,'') end , cfree5 = case when isnull(I.bconfigfree5,0) =0 then '' else isnull(a.cfree5,'') end , cfree6 = case when isnull(I.bconfigfree6,0) =0 then '' else isnull(a.cfree6,'') end , cfree7 = case when isnull(I.bconfigfree7,0) =0 then '' else isnull(a.cfree7,'') end , cfree8 = case when isnull(I.bconfigfree8,0) =0 then '' else isnull(a.cfree8,'') end , cfree9 = case when isnull(I.bconfigfree9,0) =0 then '' else isnull(a.cfree9,'') end , cfree10 = case when isnull(I.bconfigfree10,0) =0 then '' else isnull(a.cfree10,'') end  from #LPWriteBackTbl a inner join inventory I on a.cinvcode = I.cinvcode 
 select cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,sum(isnull(iquantity,0)) as iquantity,sum(isnull(inum,0)) as inum  into  #LPWriteBackSumTbl from #LPWriteBackTbl group by cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  create index index_lpwritebacksumtbl on #LPWriteBackSumTbl(cinvcode,isotype,isodid,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10 ) 
 insert into ST_DemandKeepInfo (cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,iquantity,inum,idemandtype,cdemandid )   select cinvcode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,0,0,isotype,isodid   from #LPWriteBackSumTbl a where not exists (select cinvcode from ST_DemandKeepInfo where cinvcode=a.cinvcode and idemandtype = a.isotype and cdemandid = a.isodid    and cfree1 = a.cfree1 and  cfree2 = a.cfree2 and cfree3 = a.cfree3 and cfree4 = a.cfree4 and cfree5 = a.cfree5 and cfree6 = a.cfree6    and cfree7 = a.cfree7 and cfree8 = a.cfree8 and cfree9 = a.cfree9 and cfree10 = a.cfree10 ) 
 update ST_DemandKeepInfo set iquantity =isnull(a.iquantity,0) + isnull(b.iquantity,0) ,inum =isnull(a.inum,0) + isnull(b.inum,0)    from ST_DemandKeepInfo a inner join #LPWriteBackSumTbl b on a.cinvcode =b.cinvcode and a.idemandtype = b.isotype and a.cdemandid = b.isodid  and a.cfree1 =b.cfree1 and a.cfree2 =b.cfree2 and a.cfree3 =b.cfree3 and a.cfree4 =b.cfree4 and a.cfree5 =b.cfree5 and a.cfree6 =b.cfree6  and a.cfree7 =b.cfree7 and a.cfree8 =b.cfree8 and a.cfree9 =b.cfree9 and a.cfree10 =b.cfree10 
select a.cinvcode from  ST_DemandKeepInfo a inner join #LPWriteBackSumTbl b on a.cinvcode =b.cinvcode and a.idemandtype = b.isotype and a.cdemandid = b.isodid  and a.cfree1 =b.cfree1 and a.cfree2 =b.cfree2 and a.cfree3 =b.cfree3 and a.cfree4 =b.cfree4 and a.cfree5 =b.cfree5 and a.cfree6 =b.cfree6  and a.cfree7 =b.cfree7 and a.cfree8 =b.cfree8 and a.cfree9 =b.cfree9 and a.cfree10 =b.cfree10  where convert(decimal(36,6),a.iquantity) < 0  or convert(decimal(36,6),isnull(a.inum,0)) < 0  group by a.cinvcode 
if exists (select 1 where not object_id('tempdb..#lpwritebacktbl') is null )  drop table #LPWriteBackTbl
if exists (select 1 where not object_id('tempdb..#lpwritebacksumtbl') is null )  drop table #LPWriteBackSumTbl",id);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //存储过程
                sql = "exec ST_VerForStock N'10',N'" + id + "',0,1,1";
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

                sql = @"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)"
                     + " select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) "
                     + " where a.transactionid=N'spid_" + spid + "' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 "
                     + " and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 "
                     + " and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #endregion


                myTran.Commit();
                int_i = 0;
            }
            catch (Exception exception2)
            {
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
