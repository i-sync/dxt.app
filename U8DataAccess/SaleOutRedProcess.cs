using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;
using System.Data.SqlClient;
using System.Linq;

namespace U8DataAccess
{
    public class SaleOutRedProcess
    {
        /// <summary>
        /// 验证销售退货单
        /// </summary>
        /// <param name="cchkcode"></param>
        /// <param name="connectionString"></param>
        /// <param name="list"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int VerifyGSPBack(string cchkcode, string connectionString, out SaleOutRedList list, out string errMsg)
        {
            list = null;
            errMsg = "";
            string strSql = @"select * from dispatchlist
                            where dlid = (select sourceid from kc_gsp_saleoutH
                            where id in(select distinct top 100 percent id FROM  kc_gsp_saleout  
                            Where isnull(bMake,'')=N'否' and (isnull(iquantity,0)<>0)
                            and cvouchcode = N'" + cchkcode + @"'))";
            DataSet ds = null;
            //OperationSql.GetDataset(strSql, connectionString, out ds, out errMsg);
            ds = DBHelperSQL.Query(connectionString, strSql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = new SaleOutRedList();
                list.cbustype = DB2String(ds.Tables[0].Rows[0]["cbustype"]);
                list.cbuscode = DB2String(ds.Tables[0].Rows[0]["cdlcode"]);
                list.cdepcode = DB2String(ds.Tables[0].Rows[0]["cdepcode"]);
                list.cpersoncode = DB2String(ds.Tables[0].Rows[0]["cpersoncode"]);
                list.cstcode = DB2String(ds.Tables[0].Rows[0]["cstcode"]);
                list.ccuscode = DB2String(ds.Tables[0].Rows[0]["ccuscode"]);
                list.cdlid = DB2String(ds.Tables[0].Rows[0]["dlid"]);
                list.cmemo = DB2String(ds.Tables[0].Rows[0]["cmemo"]);
                list.ccusname = DB2String(ds.Tables[0].Rows[0]["cCusName"]);
                list.cchkcode = cchkcode;
                DataSet dstmp = null;
                strSql = "select * from GSP_VouchQC where qcid = N'" + cchkcode + "'";
                //OperationSql.GetDataset(strSql, connectionString, out dstmp, out errMsg);
                dstmp = DBHelperSQL.Query(connectionString, strSql);
                if (dstmp != null && dstmp.Tables[0].Rows.Count > 0)
                {
                    list.cchkperson = DB2String(dstmp.Tables[0].Rows[0]["cmaker"]);
                    list.dchkdate = DB2String(dstmp.Tables[0].Rows[0]["ddate"]);

                    #region 表体

                    strSql = @"SELECT v.*,i.cInvDefine1 FROM (select * from kc_gsp_saleout
                            where id in(select distinct top 100 percent id FROM  kc_gsp_saleout  
                            Where isnull(bMake,'')=N'否' and (isnull(iquantity,0)<>0)
                            and cvouchcode = N'" + cchkcode + @"') ) v INNER JOIN (SELECT cInvCode,cInvDefine1 FROM inventory) i ON v.cinvcode = i.cInvCode";
                    DataSet detail_ds = null;
                    //OperationSql.GetDataset(strSql, connectionString, out detail_ds, out errMsg);
                    detail_ds = DBHelperSQL.Query(connectionString, strSql);
                    if (detail_ds != null && detail_ds.Tables[0].Rows.Count > 0)
                    {
                        list.U8Details = new List<SaleOutRedDetail>();
                        for (int i = 0; i < detail_ds.Tables[0].Rows.Count; i++)
                        {
                            SaleOutRedDetail detail = new SaleOutRedDetail();
                            detail.cinvcode = DB2String(detail_ds.Tables[0].Rows[i]["cinvcode"]);
                            detail.cinvname = DB2String(detail_ds.Tables[0].Rows[i]["cinvname"]);
                            detail.cwhcode = DB2String(detail_ds.Tables[0].Rows[i]["cwhcode"]);
                            detail.cinvstd = DB2String(detail_ds.Tables[0].Rows[i]["cinvstd"]);
                            detail.cbatch = DB2String(detail_ds.Tables[0].Rows[i]["cbatch"]);
                            detail.dvdate = DB2DateTime(detail_ds.Tables[0].Rows[i]["dvdate"]).ToString("yyyy-MM-dd") ;//DB2String(detail_ds.Tables[0].Rows[i]["dvdate"]);
                            detail.dmadedate = DB2DateTime(detail_ds.Tables[0].Rows[i]["dmadedate"]).ToString("yyyy-MM-dd"); //DB2String(detail_ds.Tables[0].Rows[i]["dmadedate"]);
                            detail.cExpirationdate = DB2DateTime(detail_ds.Tables[0].Rows[i]["dvdate"]).AddDays(-1).ToString("yyyy-MM-dd");
                            detail.imassdate =DB2Int(detail_ds.Tables[0].Rows[i]["imassdate"]);
                            detail.iunitcost = DB2Decimal(detail_ds.Tables[0].Rows[i]["iunitcost"]);
                            detail.iprice = DB2Decimal(detail_ds.Tables[0].Rows[i]["iprice"]);
                            detail.iquantity = DB2Decimal(detail_ds.Tables[0].Rows[i]["iquantity"]);
                            detail.cdefine22 = DB2String(detail_ds.Tables[0].Rows[i]["cdefine22"]);
                            detail.icheckids = DB2Int(detail_ds.Tables[0].Rows[i]["autoid"]);
                            //生产单位
                            detail.cinvdefine1 = DB2String(detail_ds.Tables[0].Rows[i]["cInvDefine1"]);
                            detail.cinvm_unit = DB2String(detail_ds.Tables[0].Rows[i]["cinvm_unit"]);
                            detail.ccusname = list.ccusname;
                            DataSet ds2 = null;
                            strSql = @"select * from dispatchlists where dlid = " + list.cdlid + " and cinvcode = N'" + detail.cinvcode + "'";
                            //OperationSql.GetDataset(strSql, connectionString, out ds2, out errMsg);
                            ds2 = DBHelperSQL.Query(connectionString, strSql);
                            detail.idlsid = DB2Int(ds2.Tables[0].Rows[0]["idlsid"]);
                            list.U8Details.Add(detail);
                        }

                        return 0;
                    }
                    else
                    {
                        errMsg = "表体信息错误";
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    errMsg = "检验单信息错误";
                    return -1;
                }
            }
            else
            {
                errMsg = "检验单信息错误";
                return -1;
            }
        }

        #region
        /*
        /// <summary>
        /// 保存销售出库红字单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveSaleOutRed(SaleOutRedList dl, string connectionString, string accid, string year, out string errMsg)
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
                string cVouchName = "销售出库单";
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

                ///,cHandler,dVeriDate,dNVerifyTime
                ///,@cHandler,@dVeriDate,@dNVerifyTime
                sql = @"insert into rdrecord(id,brdflag,cvouchtype,cbustype,csource,cbuscode,cwhcode,ddate,ccode,crdcode,cdepcode,cpersoncode,cstcode,ccuscode,cdlcode,cmemo,cmaker,cchkcode,dchkdate,cchkperson,vt_id,iarriveid,iswfcontrolled,dnmaketime)";
                sql += @"Values (@id,0,N'32',@cbustype,N'退货验收单',@cbuscode,@cwhcode,@ddate,@ccode,N'201',@cdepcode,@cpersoncode,N'01',@ccuscode,@cdlcode,@cmemo,@cmaker,@cchkcode,@dchkdate,@cchkperson ,87,@iarriveid,0,@dnmaketime)";
                sql = sql.Replace(@"@id", "86" + id.ToString().PadLeft(7,'0'));
                sql = sql.Replace(@"@cbustype", SelSql(dl.cbustype.ToString()));
                sql = sql.Replace(@"@cbuscode", SelSql(dl.cbuscode));
                sql = sql.Replace(@"@cwhcode", SelSql(dl.cwhcode));
                sql = sql.Replace(@"@ddate", "'" + dd + "'");
                sql = sql.Replace(@"@ccode", SelSql("86" + id.ToString().PadLeft(7, '0')));
                sql = sql.Replace(@"@cdepcode", SelSql(dl.cdepcode));
                sql = sql.Replace(@"@cpersoncode", SelSql(dl.cpersoncode));
                sql = sql.Replace(@"@ccuscode", SelSql(dl.ccuscode));
                sql = sql.Replace(@"@cdlcode", dl.cdlid);
                sql = sql.Replace(@"@cmemo", SelSql(dl.cmemo));
                sql = sql.Replace(@"@cmaker", SelSql(dl.cmaker));
                sql = sql.Replace(@"@cchkcode", SelSql(dl.cchkcode));
                sql = sql.Replace(@"@dchkdate", SelSql(dl.dchkdate));
                sql = sql.Replace(@"@cchkperson", SelSql(dl.cchkperson));
                sql = sql.Replace(@"@iarriveid", SelSql(dl.cbuscode));
                sql = sql.Replace(@"@dnmaketime", "'" + dt + "'");

                //20120907添加申核人，申核日期，申核时间，直接把单申核掉
                //sql = sql.Replace(@"@cHandler", SelSql(dl.cmaker));
                //sql = sql.Replace(@"@dVeriDate", SelSql(dd));
                //sql = sql.Replace(@"@dNVerifyTime", SelSql(dt));

                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                cmd.CommandText = @"select a.id,autoid ,convert(decimal(30,4),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, a.Cinvcode,Corufts ,idlsid, convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords a where 1=0";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"update rdrecords set corufts ='' where id =" + "86" + id.ToString().PadLeft(7, '0');
                cmd.ExecuteNonQuery();

                int irowno = 1;

                ///2012-09-14 修改
                //已插入的批次
                List<string> list = new List<string>();
                foreach (SaleOutRedDetail detail in dl.OperateDetails)
                {
                    //如果该批次已经插入 则跳过本次循环
                    if (list.Contains(detail.cbatch))
                    {
                        continue;
                    }
                    //在集合中查询所有批次为detail.Cbatch的对象
                    var v = from od in dl.OperateDetails where od.cbatch == detail.cbatch select od;
                    int num = 0;//记录同一批次的货位个数
                    //decimal iquantity = 0;//记录该批次货物总数量

                    autoid++;

                    foreach (SaleOutRedDetail temp in v)
                    {
                        //如果货位信息为空，别表示不插入货位
                        if (string.IsNullOrEmpty(temp.cposition))
                        {
                            break;
                        }
                        //插入货位信息表
                        sql = @"insert into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)
                                    values(@rdsid,@rdid,@cwhcode,@cposcode,@cinvcode,@cbatch,null,null,@dvdate,@iquantity *-1 ,@inum,null,@chandler,@ddate,1,null,null,null,null,null,null,null,null,null,@cassunit,null,null,@dmadedate,@imassdate,@cmassunit,null,2,@cexpirationdate,@dexpirationdate)";
                        sql = sql.Replace("@rdsid", "86" + autoid.ToString().PadLeft(7, '0'));
                        sql = sql.Replace("@rdid", SelSql("86" + id.ToString().PadLeft(7, '0')));
                        sql = sql.Replace("@cwhcode", SelSql(dl.cwhcode));
                        sql = sql.Replace("@cposcode", SelSql(temp.cposition));
                        sql = sql.Replace("@cinvcode", SelSql(temp.cinvcode));
                        sql = sql.Replace("@cbatch", SelSql(temp.cbatch));

                        sql = sql.Replace("@dvdate", "'" + detail.dvdate + "'");
                        sql = sql.Replace("@iquantity", temp.inewquantity.ToString());
                        //件数
                        sql = sql.Replace("@inum", "null");

                        sql = sql.Replace("@chandler", SelSql(dl.cmaker));//子表中没有经手人
                        sql = sql.Replace("@ddate", "'" + dd + "'");

                        sql = sql.Replace("@cassunit", "null");//SelSql(temp.cassunit));
                        sql = sql.Replace("@dmadedate", "'" + temp.dmadedate + "'");
                        sql = sql.Replace("@imassdate", detail.imassdate.ToString());
                        sql = sql.Replace("@cmassunit", SelSql(temp.cmassunit));
                        sql = sql.Replace("@cexpirationdate", "'" + Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd") + "'");//有效期至为失效日期－1
                        sql = sql.Replace("@dexpirationdate", "'" + Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd") + "'");

                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }

                        num++;
                        //iquantity += temp.iquantity;
                    }

                    #region 
                    //插入rdrecords数据表
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,iquantity,iunitcost,iprice,cbatch,dvdate,cdefine22,idlsid,dmadedate,imassdate,icheckids,cmassunit,bcosting,bvmiused,iexpiratdatecalcu,cexpirationdate,dexpirationdate,irowno,cposition)"
                           + @" Values (@autoid,@ID,@cinvcode,@iquantity * -1,@iunitcost,@iprice * -1,@cbatch,@dvdate,@cdefine22,@idlsid,@dmadedate,@imassdate,@icheckids,2,1,0,2,@cexpirationdate,@dexpirationdate,@irowno,@cposition)";
                    sql = sql.Replace("@autoid", "86" + autoid.ToString().PadLeft(7, '0'));
                    sql = sql.Replace("@ID", "86" + id.ToString().PadLeft(7, '0'));
                    sql = sql.Replace("@cinvcode", SelSql(detail.cinvcode));
                    sql = sql.Replace("@iquantity", detail.iquantity.ToString());
                    sql = sql.Replace("@iunitcost", detail.iunitcost.ToString());
                    sql = sql.Replace("@iprice", detail.iprice.ToString());
                    sql = sql.Replace("@cbatch", SelSql(detail.cbatch));
                    sql = sql.Replace("@dvdate", SelSql(detail.dvdate));
                    sql = sql.Replace("@cdefine22", SelSql(detail.cdefine22));
                    sql = sql.Replace("@idlsid", detail.idlsid.ToString());
                    sql = sql.Replace("@dmadedate", SelSql(detail.dmadedate));
                    sql = sql.Replace("@imassdate", detail.imassdate.ToString());
                    sql = sql.Replace("@icheckids", detail.icheckids.ToString());
                    DateTime expirationdate = DateTime.Parse(detail.dvdate).AddDays(-1);
                    sql = sql.Replace("@cexpirationdate", SelSql(expirationdate.ToString("yyyy-MM-dd")));
                    sql = sql.Replace("@dexpirationdate", SelSql(expirationdate.ToString("yyyy-MM-dd")));
                    sql = sql.Replace("@irowno", irowno.ToString());
                    //添加货位,若没有货位或指定多个货位则插入空,否则插入货位编码
                    sql = sql.Replace("@cposition", num == 1 ? SelSql(detail.cposition) : "null");
                    irowno++;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    //  2012-10-25原因单据弃审后无法重做
                    //sql = @"Insert Into #Ufida_WBBuffers select a.id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), a.Cinvcode, Corufts  as Corufts, idlsid,iCheckIds, 2 as iOperate   from rdrecords a  where id=" + "86" + id.ToString().PadLeft(7, '0');
                    //cmd.CommandText = sql;
                    //if (cmd.ExecuteNonQuery() < 1)
                    //{
                    //    myTran.Rollback();
                    //    return -1;
                    //}
                    //sql = @"if exists (select 1 where not object_id('tempdb..#GSP') is null ) Drop table #GSP";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                    //sql = @"if exists (select 1 where not object_id('tempdb..#GSPUFTS') is null ) Drop table #GSPUFTS";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                    //sql = @"select convert(money,ufts) as gspufts,convert(int,Null) as iCheckIds into #GSPUFTS from rdrecord where 1=0";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                    //sql = @"Insert into #GSPUFTS (iCheckIds) values(" + detail.icheckids.ToString() + ")";
                    //cmd.CommandText = sql;
                    //if (cmd.ExecuteNonQuery() < 1)
                    //{
                    //    myTran.Rollback();
                    //    return -1;
                    //}
                    //sql = @"select iCheckIds ,-1* sum(iquantity) as qty ,-1* sum(inum) as inum into #GSP from #Ufida_WBBuffers group by iCheckIds having ( sum(iquantity)<>0 or sum (inum)<>0)";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                    //sql = @"update GSP_VouchQC  set cmemo=cmemo from GSP_VouchQC inner join GSP_VouchsQC with (nolock)   on GSP_VouchQC.ID=GSP_VouchsQC.ID where  GSP_VouchsQC.autoId  in (select iCheckIds  from  #GSPUFTS )";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();
                    //sql = @"update a set FSTQTY=isnull(FSTQTY,0) + b.qty,FSTNUM=isnull(FSTNUM,0) + b.inum  from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds";
                    //cmd.CommandText = sql;
                    //if (cmd.ExecuteNonQuery() < 1)
                    //{
                    //    myTran.Rollback();
                    //    return -1;
                    //}
                    //sql = @"update a  set BMAKESALEOUT =  case when convert (decimal (38,6),FELGQUANTITY-isnull(FSTQTY,0))=0  then 1 else 0 end     from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds ";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();


                    sql = string.Format("update GSP_VouchQC with (updlock) set cmemo=cmemo from GSP_VouchQC inner join GSP_VouchsQC on GSP_VouchQC.ID=GSP_VouchsQC.ID and GSP_VouchsQC.autoId={0} and isnull(GSP_VouchQC.cVerifier,N'')<>N''", detail.icheckids);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = string.Format("update GSP_VouchsQC with (updlock) set BMAKESALEOUT =1 where autoId={0}",detail.icheckids);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    #endregion

                    //把该批次添加到list中
                    list.Add(detail.cbatch);
                }

                
                //sql = "Drop table #Ufida_WBBuffers";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //sql = "select a.id,autoid ,convert(decimal(30,4),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, a.Cinvcode,Corufts ,idlsid, convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords a where 1=0";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                //sql = string.Format("update rdrecords set corufts ='' where id ={0}", "86" + id.ToString().PadLeft(7, '0'));
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                sql = @"Insert Into #Ufida_WBBuffers select a.id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), a.Cinvcode, Corufts  as Corufts, idlsid, 2 as iOperate   from rdrecords a where id=" + "86" + id.ToString().PadLeft(7, '0');
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }


                #region temp table
                //delete temp table
                sql = @"select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode ,Max(corufts) as corufts,  max(idlsid) as idlsid, sum(iOperate) as iOperate  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update  #Ufida_WBBuffers_ST set corufts=null where iOperate<>2 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select idlsid as idid,sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode,Max(corufts) as corufts, 0 as istflowid into #Ufida_WBBuffers_Target from #Ufida_WBBuffers_ST group by idlsid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update dispatchlists with (UPDLOCK) set fOutQuantity=cast(isnull(fOutQuantity,0)+isnull(#Ufida_WBBuffers_Target.iquantity,0)  as decimal(30,4)), fOutNum=cast(isnull(fOutNum,0)+isnull(#Ufida_WBBuffers_Target.inum,0) as decimal(30,2)) from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid where bsettleall=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update gsp_specialouts set fsumoutqty=cast(isnull(fsumoutqty,0)+isnull(#Ufida_WBBuffers_Target.iquantity,0) as decimal(20,6)),fsumoutqtys=cast(isnull(fsumoutqtys,0)+isnull(#Ufida_WBBuffers_Target.inum,0) as decimal(20,6)) from gsp_specialout inner join gsp_specialouts on gsp_specialout.id=gsp_specialouts.id inner join #Ufida_WBBuffers_Target on gsp_specialouts.icode_t=#Ufida_WBBuffers_Target.idid where cvouchtype in (N'052',N'053',N'054',N'055',N'056',N'057')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select case when isnull(iinvsncount,0)=0 then (case when isnull(iflowid,0)=0 then 0 else (case when isnull(bopeningprocess,0)=0 then 0 else isnull(bShippingOverDelivery,0) end) end) else 0 end as bover,#Ufida_WBBuffers_Target.* into #DispTmp from #Ufida_WBBuffers_Target left join sabizflow on #Ufida_WBBuffers_Target .istflowid=sabizflow.iflowid inner join  dispatchlists  on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select distinct dlid into #tmpdlid from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update dispatchlist  set cSaleOut=N'' from dispatchlist inner join #tmpdlid b on dispatchlist.dlid=b.dlid where isnull(cSaleOut,'')='' or isnull(cSaleOut,'')='ST' ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"update dispatchlist  set cSaleOut=N'ST' from dispatchlist  inner join #tmpdlid b on dispatchlist.dlid=b.dlid inner join  dispatchlists c on c.dlid=dispatchlist.dlid inner join rdrecords on c.idlsid=rdrecords.idlsid inner join rdrecord on rdrecord.id=rdrecords.id WHERE csource in (N'发货单',N'委托代销',N'普通发票',N'专用发票','销售日报',N'销售调拨单')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"select cinvcode,convert(datetime,Null) as dstdate,convert(datetime,Null) as doridate ,convert(nvarchar(800),Null) as error   into #STCheckVouchDate  from inventory where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"exec  USP_STCheckVouchDate  N'32',N'#Ufida_WBBuffers'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                
                #endregion

                #region procedure
                //添加
                sql = @"exec ST_SaveForStock N'32',N'86" + id.ToString().PadLeft(7, '0') + "',1,0,1";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                ////审核
                //sql = @"exec ST_VerForStock N'32',N'86" + id.ToString().PadLeft(7, '0') + "',0,1,1";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

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
                sql = @"select cUser_id from UfSystem..UA_User where cUser_Name = '" + dl.cmaker + "'";
                cmd.CommandText = sql;
                string cuser_id = cmd.ExecuteScalar().ToString();
                sql = @"Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + cuser_id + "'";
                cmd.CommandText = sql;
                string cSeed = cmd.ExecuteScalar().ToString() + dd.Replace("-","");
                sql = "select isnull(cNumber,1) as Maxnumber from voucherhistory with (ROWLOCK) where cardnumber='" + cardnumber + "' and cContent ='制单人|日期' and cSeed='" + cuser_id + dd.Replace("-", "") + "'";
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                //ln.Write("成品入库单号查询：", sql);
                if (ocode == null)
                {
                    icode = 1;
                    sql = "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','制单人|日期','','" + cuser_id + dd.Replace("-", "") + "','1')";
                }
                else
                {
                    icode = int.Parse(ocode.ToString()) + 1;
                    sql = "update voucherhistory set cnumber=" + icode + " where cardnumber='" + cardnumber + "' and cContent ='制单人|日期' and cSeed='" + cuser_id + dd.Replace("-", "") + "'";
                }
                cmd.CommandText = sql;

                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }
                ccode = cSeed + icode.ToString().PadLeft(4, '0');
                sql = "select id from rdrecord where cvouchtype='32' and ccode='" + ccode + "'";
                cmd.CommandText = sql;
                if (cmd.ExecuteScalar() != null)
                {
                    myTran.Rollback();
                    errMsg = "单据号重复,请再次提交!" + ccode;
                    return -1;
                }
                sql = "Update RdRecord Set cCode = N'" + ccode + "' Where Id = 86" + id.ToString().PadLeft(7, '0') + "";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update rdrecord ccode error!";
                    return -1;
                }
                #endregion

                //修改UA_Identity
                string sqlUpidentity = "update UFSystem..UA_Identity set ifatherid=" + id + ",ichildid=" + autoid + " where cvouchtype='rd' and cAcc_id=" + accid;
                cmd.CommandText = sqlUpidentity;
                cmd.ExecuteNonQuery();


                //todo 再次执行（用于申核单子）有错误申核后，代入库数量没有减少
                #region procedure
                
 //               sql = @"exec ST_VerForStock N'32',N'86" + id.ToString().PadLeft(7, '0') + "',0,1,1";
 //               cmd.CommandText = sql;
 //               cmd.ExecuteNonQuery();
                
 //               sql = "select @@spid ";
 //               cmd.CommandText = sql;
 //               spid = cmd.ExecuteScalar().ToString();
 //               if (spid == null)
 //               {
 //                   myTran.Rollback();
 //                   return -1;
 //               }
                                
 //               sql=string.Format(@"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)
 //select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) 
 //where a.transactionid=N'spid_{0}' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 
 //and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 
 //and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )",spid);
                
 //               cmd.CommandText = sql;
 //               cmd.ExecuteNonQuery();

 //               sql = "exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_" + spid + "'";
 //               cmd.CommandText = sql;
 //               cmd.ExecuteNonQuery();
                 
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


        */
        #endregion
        
        /// <summary>
        /// 保存销售出库红字单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveSaleOutRed(SaleOutRedList dl, string connectionString, string accid, string year, out string errMsg)
        {
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
            catch (Exception ex)
            {                
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
                string cVouchName = "销售出库单";
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

                ///插入表头
                sql = @"insert into rdrecord(id,brdflag,cvouchtype,cbustype,csource,cbuscode,cwhcode,ddate,ccode,crdcode,cdepcode,cpersoncode,cstcode,ccuscode,cdlcode,cmemo,cmaker,cchkcode,dchkdate,cchkperson,vt_id,iarriveid,iswfcontrolled,dnmaketime,cHandler,dVeriDate,dNVerifyTime,cdefine10)";
                sql += @"Values (@id,0,N'32',@cbustype,N'退货验收单',@cbuscode,@cwhcode,@ddate,@ccode,N'201',@cdepcode,@cpersoncode,N'01',@ccuscode,@cdlcode,@cmemo,@cmaker,@cchkcode,@dchkdate,@cchkperson ,87,@iarriveid,0,@dnmaketime,@cHandler,@dVeriDate,@dNVerifyTime,@cdefine10)";
                sql = sql.Replace(@"@id", "86" + id.ToString().PadLeft(7, '0'));
                sql = sql.Replace(@"@cbustype", SelSql(dl.cbustype.ToString()));
                sql = sql.Replace(@"@cbuscode", SelSql(dl.cbuscode));
                sql = sql.Replace(@"@cwhcode", SelSql(dl.cwhcode));
                sql = sql.Replace(@"@ddate", "'" + dd + "'");
                sql = sql.Replace(@"@ccode", SelSql("86" + id.ToString().PadLeft(7, '0')));
                sql = sql.Replace(@"@cdepcode", SelSql(dl.cdepcode));
                sql = sql.Replace(@"@cpersoncode", SelSql(dl.cpersoncode));
                sql = sql.Replace(@"@ccuscode", SelSql(dl.ccuscode));
                sql = sql.Replace(@"@cdlcode", dl.cdlid);
                sql = sql.Replace(@"@cmemo", SelSql(dl.cmemo));
                sql = sql.Replace(@"@cmaker", SelSql(dl.cmaker));
                sql = sql.Replace(@"@cchkcode", SelSql(dl.cchkcode));
                sql = sql.Replace(@"@dchkdate", SelSql(dl.dchkdate));
                sql = sql.Replace(@"@cchkperson", SelSql(dl.cchkperson));
                sql = sql.Replace(@"@iarriveid", SelSql(dl.cbuscode));
                sql = sql.Replace(@"@dnmaketime", "'" + dt + "'");

                //20120907添加申核人，申核日期，申核时间，直接把单申核掉
                sql = sql.Replace(@"@cHandler", SelSql(dl.cmaker));
                sql = sql.Replace(@"@dVeriDate", SelSql(dd));
                sql = sql.Replace(@"@dNVerifyTime", SelSql(dt));
                //2013-03-12监管码
                sql = sql.Replace("@cdefine10", SelSql(dl.cdefine10));
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }

                //sql = string.Format("update GSP_VouchQC with (updlock) set cmemo=cmemo from GSP_VouchQC inner join GSP_VouchsQC on GSP_VouchQC.ID=GSP_VouchsQC.ID and GSP_VouchsQC.autoId={0} and isnull(GSP_VouchQC.cVerifier,N'')<>N'' ",dl.OperateDetails[0].icheckids);
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                //sql = "Drop table #Ufida_WBBuffers";
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();
                sql = "select a.id,autoid ,convert(decimal(30,4),iquantity) as iquantity,convert(decimal(30,2),inum) as iNum, a.Cinvcode,Corufts ,idlsid,iCheckIds, convert(smallint,0) as iOperate into #Ufida_WBBuffers from rdrecords a where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = string.Format(" update rdrecords set corufts ='' where id ={0}", "86" + id.ToString().PadLeft(7, '0'));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //此临时表，用于回写GSP_VOUCHQC,在循环中记录rdrecords中对应的iCheckedid
                sql = "if exists (select 1 where not object_id('tempdb..#GSPUFTS') is null ) Drop table #GSPUFTS";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select convert(money,ufts) as gspufts,convert(int,Null) as iCheckIds into #GSPUFTS from rdrecord where 1=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //插入表体
                int irowno = 1;

                ///2012-09-14 修改
                ///2012-11-01 再次修改之前为根据批次判断，现存在加上存货编码
                //已插入的批次
                List<SaleOutRedDetail> list = new List<SaleOutRedDetail>();
                foreach (SaleOutRedDetail detail in dl.OperateDetails)
                {                    
                    //如果同一存货该批次已经插入 则跳过本次循环
                    bool flag = false;//标识存货的某一批次是否已经插入，默认为false;
                    foreach (SaleOutRedDetail l in list)
                    {
                        if (l.cinvcode== detail.cinvcode && l.cbatch == detail.cbatch)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)//某存货的一个批次已经插入，跳出循环。
                    {
                        continue;
                    }

                    //在集合中查询同一存货所有批次为detail.Cbatch的对象
                    var v = from od in dl.OperateDetails where od.cinvcode==detail.cinvcode && od.cbatch == detail.cbatch select od;
                    int num = 0;//记录同一批次的货位个数
                    //decimal iquantity = 0;//记录该批次货物总数量

                    autoid++;

                    foreach (SaleOutRedDetail temp in v)
                    {
                        //如果货位信息为空，别表示不插入货位
                        if (string.IsNullOrEmpty(temp.cposition))
                        {
                            break;
                        }
                        //插入货位信息表
                        sql = @"insert into InvPosition(rdsid,rdid,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,dvdate,iquantity,inum,cmemo,chandler,ddate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)
                                    values(@rdsid,@rdid,@cwhcode,@cposcode,@cinvcode,@cbatch,null,null,@dvdate,@iquantity *-1 ,@inum,null,@chandler,@ddate,0,null,null,null,null,null,null,null,null,null,@cassunit,null,null,@dmadedate,@imassdate,@cmassunit,null,2,@cexpirationdate,@dexpirationdate)";
                        sql = sql.Replace("@rdsid", "86" + autoid.ToString().PadLeft(7, '0'));
                        sql = sql.Replace("@rdid", SelSql("86" + id.ToString().PadLeft(7, '0')));
                        sql = sql.Replace("@cwhcode", SelSql(dl.cwhcode));
                        sql = sql.Replace("@cposcode", SelSql(temp.cposition));
                        sql = sql.Replace("@cinvcode", SelSql(temp.cinvcode));
                        sql = sql.Replace("@cbatch", SelSql(temp.cbatch));

                        sql = sql.Replace("@dvdate", "'" + detail.dvdate + "'");
                        sql = sql.Replace("@iquantity", temp.inewquantity.ToString());
                        //件数
                        sql = sql.Replace("@inum", "null");

                        sql = sql.Replace("@chandler", SelSql(dl.cmaker));//子表中没有经手人
                        sql = sql.Replace("@ddate", "'" + dd + "'");

                        sql = sql.Replace("@cassunit", "null");//SelSql(temp.cassunit));
                        sql = sql.Replace("@dmadedate", "'" + temp.dmadedate + "'");
                        sql = sql.Replace("@imassdate", detail.imassdate.ToString());
                        sql = sql.Replace("@cmassunit", "2");
                        sql = sql.Replace("@cexpirationdate", "'" + Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd") + "'");//有效期至为失效日期－1
                        sql = sql.Replace("@dexpirationdate", "'" + Convert.ToDateTime(temp.dvdate).AddDays(-1).ToString("yyyy-MM-dd") + "'");

                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }

                        num++;
                        //iquantity += temp.iquantity;
                    }

                    #region
                    //插入rdrecords数据表
                    sql = @"Insert Into rdrecords(autoid,id,cinvcode,iquantity,iunitcost,iprice,cbatch,dvdate,cdefine22,idlsid,dmadedate,imassdate,icheckids,cmassunit,bcosting,bvmiused,iexpiratdatecalcu,cexpirationdate,dexpirationdate,irowno,cposition)"
                           + @" Values (@autoid,@ID,@cinvcode,@iquantity * -1,@iunitcost,@iprice * -1,@cbatch,@dvdate,@cdefine22,@idlsid,@dmadedate,@imassdate,@icheckids,2,1,0,2,@cexpirationdate,@dexpirationdate,@irowno,@cposition)";
                    sql = sql.Replace("@autoid", "86" + autoid.ToString().PadLeft(7, '0'));
                    sql = sql.Replace("@ID", "86" + id.ToString().PadLeft(7, '0'));
                    sql = sql.Replace("@cinvcode", SelSql(detail.cinvcode));
                    sql = sql.Replace("@iquantity", detail.iquantity.ToString());
                    sql = sql.Replace("@iunitcost", detail.iunitcost.ToString());
                    sql = sql.Replace("@iprice", detail.iprice.ToString());
                    sql = sql.Replace("@cbatch", SelSql(detail.cbatch));
                    sql = sql.Replace("@dvdate", SelSql(detail.dvdate));
                    sql = sql.Replace("@cdefine22", SelSql(detail.cdefine22));
                    sql = sql.Replace("@idlsid", detail.idlsid.ToString());
                    sql = sql.Replace("@dmadedate", SelSql(detail.dmadedate));
                    sql = sql.Replace("@imassdate", detail.imassdate.ToString());
                    sql = sql.Replace("@icheckids", detail.icheckids.ToString());
                    DateTime expirationdate = DateTime.Parse(detail.dvdate).AddDays(-1);
                    sql = sql.Replace("@cexpirationdate", SelSql(expirationdate.ToString("yyyy-MM-dd")));
                    sql = sql.Replace("@dexpirationdate", SelSql(expirationdate.ToString("yyyy-MM-dd")));
                    sql = sql.Replace("@irowno", irowno.ToString());
                    //添加货位,若没有货位或指定多个货位则插入空,否则插入货位编码
                    sql = sql.Replace("@cposition", num == 1 ? SelSql(detail.cposition) : "null");
                    irowno++;
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    #endregion

                    //记录rdrecords中对应的iCheckedid
                    sql = string.Format("Insert into #GSPUFTS (iCheckIds) values({0})", detail.icheckids);
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }

                    //把该存货，批次添加到list中
                    list.Add(new SaleOutRedDetail() { cinvcode = detail.cinvcode, cbatch = detail.cbatch });
                }

                //回写GSP_VOUCHQC
                sql = string.Format("Insert Into #Ufida_WBBuffers select a.id,autoid ,1 * convert (decimal(30,4),iquantity),1 * convert(decimal(30,2),inum), a.Cinvcode, Corufts  as Corufts, idlsid,iCheckIds, 2 as iOperate   from rdrecords a  where id={0}", "86" + id.ToString().PadLeft(7, '0'));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "if exists (select 1 where not object_id('tempdb..#GSP') is null ) Drop table #GSP";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select iCheckIds ,-1* sum(iquantity) as qty ,-1* sum(inum) as inum into #GSP from #Ufida_WBBuffers group by iCheckIds having ( sum(iquantity)<>0 or sum (inum)<>0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select a.iCheckIds from #GSPUFTS a left join GSP_VouchsQC b  with (nolock) on b.autoid =a.iCheckIds left join   GSP_VouchQC c with (updlock)  on b.id =c.id   where c.id is null or convert(money,c.ufts) <>a.gspufts  or isnull(c.cVerifier,'')=''";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update GSP_VouchQC  set cmemo=cmemo from GSP_VouchQC inner join GSP_VouchsQC with (nolock)   on GSP_VouchQC.ID=GSP_VouchsQC.ID where  GSP_VouchsQC.autoId  in (select iCheckIds  from  #GSPUFTS )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update a set FSTQTY=isnull(FSTQTY,0) + b.qty,FSTNUM=isnull(FSTNUM,0) + b.inum  from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    return -1;
                }
                sql = "update a  set BMAKESALEOUT =  case when convert (decimal (38,6),FELGQUANTITY-isnull(FSTQTY,0))=0  then 1 else 0 end     from GSP_VouchsQC a inner join #GSP b on a.autoid =b.iCheckIds ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //--------------------

                
                sql = "select max(id) as id,autoid,Sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode ,Max(corufts) as corufts,  max(idlsid) as idlsid, sum(iOperate) as iOperate  into  #Ufida_WBBuffers_ST from #Ufida_WBBuffers group by autoid having (Sum(iquantity)<>0 or Sum(inum)<>0 )";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = " update  #Ufida_WBBuffers_ST set corufts=null where iOperate<>2 ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = " select idlsid as idid,sum(iquantity) as iquantity,sum(inum) as inum,max(cinvcode) as cinvcode,Max(corufts) as corufts, 0 as istflowid into #Ufida_WBBuffers_Target from #Ufida_WBBuffers_ST group by idlsid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update dispatchlists with (UPDLOCK) set fOutQuantity=cast(isnull(fOutQuantity,0)+isnull(#Ufida_WBBuffers_Target.iquantity,0)  as decimal(30,4)), fOutNum=cast(isnull(fOutNum,0)+isnull(#Ufida_WBBuffers_Target.inum,0) as decimal(30,2)) from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid where bsettleall=0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update gsp_specialouts set fsumoutqty=cast(isnull(fsumoutqty,0)+isnull(#Ufida_WBBuffers_Target.iquantity,0) as decimal(20,6)),fsumoutqtys=cast(isnull(fsumoutqtys,0)+isnull(#Ufida_WBBuffers_Target.inum,0) as decimal(20,6)) from gsp_specialout inner join gsp_specialouts on gsp_specialout.id=gsp_specialouts.id inner join #Ufida_WBBuffers_Target on gsp_specialouts.icode_t=#Ufida_WBBuffers_Target.idid where cvouchtype in (N'052',N'053',N'054',N'055',N'056',N'057')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "if not object_id('tempdb..#DispTmp') is null drop table tempdb..#DispTmp";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select case when isnull(iinvsncount,0)=0 then (case when isnull(iflowid,0)=0 then 0 else (case when isnull(bopeningprocess,0)=0 then 0 else isnull(bShippingOverDelivery,0) end) end) else 0 end as bover,#Ufida_WBBuffers_Target.* into #DispTmp from #Ufida_WBBuffers_Target left join sabizflow on #Ufida_WBBuffers_Target .istflowid=sabizflow.iflowid inner join  dispatchlists  on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select cinvcode,igrouptype,sign(cast(iquantity as decimal(26,4)))*sign(cast(iquantity*(1+fOutExcess)-foutquantity as decimal(26,4))) as iquantity,sign(cast(inum as decimal(26,2)))*sign(cast(inum*(1+fOutExcess)-foutnum as decimal(26,2))) as inum from (select inventory.cinvcode,igrouptype,(case when bover=1 then isnull(fOutExcess,0) else 0 end) as fOutExcess ,isnull((case when isnull(bqaneedcheck ,0)=0 then dispatchlists.iquantity else iqaquantity end),0)  as iquantity ,isnull(foutquantity,0) as foutquantity,isnull((case when isnull(bqaneedcheck ,0)=0 then dispatchlists.inum else iqanum end),0) as inum,isnull(foutnum,0) as foutnum  from dispatchlists  inner join inventory on dispatchlists.cinvcode=inventory.cinvcode inner join #DispTmp on dispatchlists.idlsid=#DispTmp.idid ) a where sign(cast(iquantity as decimal(26,4)))*sign(cast(iquantity*(1+fOutExcess)-foutquantity as decimal(26,4)))<0 or sign(cast(inum as decimal(26,2)))*sign(cast(inum*(1+fOutExcess)-foutnum as decimal(26,2)))<0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "select top 1 dispatchlist.dlid from dispatchlists inner join dispatchlist on dispatchlist.dlid=dispatchlists.dlid right join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid where( CONVERT(NvarCHAR,Convert(MONEY,ufts),2) <> CONVERT(NvarCHAR,Convert(MONEY,#Ufida_WBBuffers_Target.corufts),2)  and #Ufida_WBBuffers_Target.corufts <>'' ) or dispatchlists.dlid is null or dispatchlists.bsettleall=1";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
               
                sql = "select distinct dlid into #tmpdlid from dispatchlists inner join #Ufida_WBBuffers_Target on dispatchlists.idlsid=#Ufida_WBBuffers_Target.idid";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update dispatchlist  set cSaleOut=N'' from dispatchlist inner join #tmpdlid b on dispatchlist.dlid=b.dlid where isnull(cSaleOut,'')='' or isnull(cSaleOut,'')='ST' ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "update dispatchlist  set cSaleOut=N'ST' from dispatchlist  inner join #tmpdlid b on dispatchlist.dlid=b.dlid inner join  dispatchlists c on c.dlid=dispatchlist.dlid inner join rdrecords on c.idlsid=rdrecords.idlsid inner join rdrecord on rdrecord.id=rdrecords.id WHERE csource in (N'发货单',N'委托代销',N'普通发票',N'专用发票','销售日报',N'销售调拨单')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = @"
drop table #tmpdlid
Drop table #Ufida_WBBuffers
Drop table #Ufida_WBBuffers_ST
Drop table #Ufida_WBBuffers_Target";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                /// procedure
                //添加
                sql = string.Format("exec ST_SaveForStock N'32',N'{0}',1,0 ,1",id.ToString ().PadLeft(7,'0'));
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                //审核
                sql = @"exec ST_VerForStock N'32',N'86" + id.ToString().PadLeft(7, '0') + "',0,1,1";
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

                sql = string.Format(@"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)
 select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) 
 where a.transactionid=N'spid_{0}' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 
 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 
 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )",spid);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = string.Format("exec Usp_SCM_CommitGeneralLedgerWithCheck N'ST',1,1,0,1,0,0,1,0,1,0,0,1,1 ,0,'spid_{0}'",spid);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                #region 单据号处理

                /*
                sql = @"select cUser_id from UfSystem..UA_User where cUser_Name = '" + dl.cmaker + "'";
                cmd.CommandText = sql;
                string cuser_id = cmd.ExecuteScalar().ToString();
                sql = @"Select cCode from Vouchercontrapose Where cContent='UA_User' and cSeed='" + cuser_id + "'";
                cmd.CommandText = sql;
                string cSeed = cmd.ExecuteScalar().ToString() + dd.Replace("-", "");
                sql = "select isnull(cNumber,1) as Maxnumber from voucherhistory with (ROWLOCK) where cardnumber='" + cardnumber + "' and cContent ='制单人|日期' and cSeed='" + cuser_id + dd.Replace("-", "") + "'";
                cmd.CommandText = sql;
                ocode = cmd.ExecuteScalar();
                //ln.Write("成品入库单号查询：", sql);
                if (ocode == null)
                {
                    icode = 1;
                    sql = "Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('" + cardnumber + "','制单人|日期','','" + cuser_id + dd.Replace("-", "") + "','1')";
                }
                else
                {
                    icode = int.Parse(ocode.ToString()) + 1;
                    sql = "update voucherhistory set cnumber=" + icode + " where cardnumber='" + cardnumber + "' and cContent ='制单人|日期' and cSeed='" + cuser_id + dd.Replace("-", "") + "'";
                }
                 * 
                 * */

                ///单据编号修改规则
                ///销售退货单：单据年月（6位）+流水号（4位）
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
                //流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                ccode = cSeed + icode.ToString().PadLeft(4, '0');

                sql = "select id from rdrecord where cvouchtype='32' and ccode='" + ccode + "'";
                cmd.CommandText = sql;
                if (cmd.ExecuteScalar() != null)
                {
                    myTran.Rollback();
                    errMsg = "单据号重复,请再次提交!" + ccode;
                    return -1;
                }
                sql = "Update RdRecord Set cCode = N'" + ccode + "' Where Id = 86" + id.ToString().PadLeft(7, '0') + "";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update rdrecord ccode error!";
                    return -1;
                }
                #endregion

                #region 监管码操作
                ///日期：2013-03-12
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

                myTran.Commit();
                int_i = 0;
            }
            catch (Exception ex)
            {
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
        
        
        
        #region 转换
        public static string DB2String(object DBValue)
        {
            return DBValue != System.DBNull.Value ? DBValue.ToString() : "";
        }

        public static int DB2Int(object DBValue)
        {
            int iReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) iReturn = Convert.ToInt32(DBValue);
            }
            catch
            {
                iReturn = -10;
            }
            return iReturn;
        }

        public static Decimal DB2Decimal(object DBValue)
        {
            Decimal dReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) dReturn = Convert.ToDecimal(DBValue);
            }
            catch
            {
                dReturn = -10;
            }
            return dReturn;
        }
        public static DateTime DB2DateTime(object DBValue)
        {
            DateTime btReturn = DateTime.MinValue;
            try
            {
                if (DBValue != System.DBNull.Value)
                {
                    btReturn = Convert.ToDateTime(DBValue);
                }
            }
            catch
            {
                btReturn = DateTime.MaxValue;
            }
            return btReturn;
        }
        public static Boolean DB2Bool(object DBValue)
        {
            bool blnReturn = false;
            try
            {
                if (DBValue != System.DBNull.Value) blnReturn = Convert.ToBoolean(DBValue);
            }
            catch
            {
                blnReturn = false;
            }
            return blnReturn;
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
