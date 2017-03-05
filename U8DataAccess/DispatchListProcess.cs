using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;
using System.Data.SqlClient;
using System.Linq;

namespace U8DataAccess
{
    public class DispatchListProcess
    {
        /// <summary>
        /// 锁只读变量
        /// </summary>
        private static readonly object objLock = new object();
        #region 验证销售订单号
        public static int VerifySO_SO(string csocode, string connectionString, out DataSet list, out string errMsg)
        {
            list = null;
            errMsg = "";
            string strSql = @"select id,cstcode,ddate,csocode,ccuscode,ccusabbname,cdepcode,cdepname,cpersoncode,cpersonname,caddcode,ccusoaddress,cexch_name,itaxrate,cmemo,ccusname,cbustype,ccusperson,iExchRate,cdefine2,cdefine3,cdefine11,cSCCode,ccreditcuscode,ccreditcusname,ccuspersoncode,cinvoicecompany  from sale_RefSOVouch_T 
where csocode = N'" + csocode + @"' and id in (select distinct sale_RefSOVouch_T.id from sale_RefSOVouch_T WITH (NOLOCK) 
inner join sale_RefSOVouch_B WITH (NOLOCK) on sale_RefSOVouch_T.id=sale_RefSOVouch_B.id 
where (isnull(istatus,0)=1 and IsNULL(cCloser,N'')=N'' And IsNull(cSCloser,N'')=N'') and ( 1=1   And (cBustype = N'普通销售' or cBustype = N'委托代销') and  cast(CASE 
WHEN ISNULL(iQuantity,0)=0 AND cBusType<>N'直运销售'  THEN ABS(ISNULL(iSum,0))-ABS(ISNULL(iFHMoney,0))   WHEN ISNULL(iQuantity,0)=0 AND cBusType=N'直运销售' THEN 
ABS(ISNULL(iSum,0))-ABS(ISNULL(iKPMoney,0))    WHEN ISNULL(iQuantity,0)<>0 AND cBusType=N'直运销售' THEN ABS(ISNULL(iQuantity,0))-ABS(ISNULL(iKPQuantity,0))  WHEN ISNULL(iQuantity,0)<>0 AND 
cBusType<>N'直运销售' THEN ABS(ISNULL(iQuantity,0))-ABS(ISNULL(iFHQuantity,0)) end as decimal(26,9)) >0 and (isnull(iflowid,0)=0))) ";
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

        #region 获取销售订单子表
        public static int GetSO_SODetails(string csocode_id, string connectionString, out DataSet Details, out string errMsg)
        {
            Details = null;
            errMsg = "";
            string strSql = @"select cwhcode,cinvcode,iquantity,inum,iquotedprice,iunitprice,itaxunitprice,imoney,itax,isum,idiscount,inatunitprice,inatmoney,inattax,inatsum,inatdiscount,itb,isosid,kl,kl2,cinvname,itaxrate,cdefine22,fsalecost,fsaleprice,cvenabbname,bgsp,csocode,imassdate,cmassunit,cordercode,iorderrowno,fcusminprice,irowno,iexpiratdatecalcu,cvencode,iFHQuantity,
(cordercode) as cordercode,(cordercode) as csocode,(iorderrowno) as iorderrowno,ccontractid,ccontracttagcode,(N'') as 
cquocode,cwhcode,cwhname,cinvcode,cinvaddcode,cinvname,cinvstd,(ccontractrowguid) as ccontractrowguid,ccusinvcode,cvenabbname,ccusinvname,dpredate,(dpremodate) as 
dpremodate,cinvm_unit,iquantity,cgroupcode,igrouptype,iinvexchrate,cunitid,cinva_unit,inum,iquotedprice,itaxunitprice,iunitprice,(iMoney) as imoney,itax,imassdate,(N'') as 
cconfigstatus,cmassunit,isum,(iTaxRate) as 
itaxrate,inatunitprice,inatmoney,cscloser,inattax,inatsum,bfree1,bfree10,bfree2,bfree3,bfree4,bfree5,bfree6,bfree7,bfree8,bfree9,bsalepricefree1,bsalepricefree10,bsalepricefree2,bsalepricefree3,bsalepricefree4,bsalepricefree5,bsalepricefree6,bsalepricefree7,bsalepricefree8,bsalepricefree9,iinvlscost,kl,kl2,(N'') 
as dkl1,(N'') as dkl2,itb,idiscount,inatdiscount,fsalecost,binvquality,fsaleprice,(iexpiratdatecalcu) as 
iexpiratdatecalcu,ifhquantity,ifhnum,ifhmoney,ikpquantity,ikpnum,ikpmoney,imoquantity,(iprekeepquantity) as iprekeepquantity,(iprekeepnum) as 
iprekeepnum,fcusminprice,binvtype,bservice,(iprekeeptotquantity) as iprekeeptotquantity,(iprekeeptotnum) as iprekeeptotnum,(iquantity) as inewquantity,(inum) as inewnum,(imoney) as inewmoney,(itax) as 
inewtax,(isum) as inewsum,(inatmoney) as inewnatmoney,(inattax) as inewnattax,(idiscount) as inewdiscount,(N'') as corufts,(inatsum) as inewnatsum,(inatdiscount) as inewnatdiscount,(cMemo) as 
cmemo,citem_class,citem_cname,btrack,binvbatch,citemcode,citemname,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,(N'') as flossrate,cfree7,cfree8,cfree9,cfree10,cinvdefine1,(N'') as 
frlossqty,cinvdefine2,cinvdefine3,cinvdefine4,cinvdefine6,cinvdefine5,cinvdefine7,cinvdefine8,cinvdefine9,(N'否') as bqaneedcheck,cinvdefine10,cinvdefine11,(N'否') as 
bqaurgency,bproxywh,cinvdefine12,cinvdefine13,cinvdefine14,cinvdefine15,cinvdefine16,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,(N'否') 
as bgsp,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,csrpolicy,icusbomid,isosid,(dreleasedate) as dreleasedate,(ID) as id,(N'') as 
bneedsign,iinvweight,idemandseq,idemandtype,cdemandcode,cdemandid,cdemandmemo,irowno,bsaleprice,bgift,(bserial) as bserial,(cvencode) as cvencode from sale_RefSOVouch_B where sale_RefSOVouch_B.id=" + csocode_id + @" and isosid in 
(select isosid from sale_RefSOVouch_T inner join sale_RefSOVouch_B on sale_RefSOVouch_T.id=sale_RefSOVouch_B.id where ((sale_RefSOVouch_B.id=" + csocode_id + @" ) and (isnull(istatus,0)=1 and 
IsNULL(cCloser,N'')=N'' And IsNull(cSCloser,N'')=N'')) and ( 1=1   And (cBustype = N'普通销售' or cBustype = N'委托代销') and  cast(CASE WHEN ISNULL(iQuantity,0)=0 AND cBusType<>N'直运销售'  THEN 
ABS(ISNULL(iSum,0))-ABS(ISNULL(iFHMoney,0))   WHEN ISNULL(iQuantity,0)=0 AND cBusType=N'直运销售' THEN ABS(ISNULL(iSum,0))-ABS(ISNULL(iKPMoney,0))    WHEN ISNULL(iQuantity,0)<>0 AND cBusType=N'直运销售' 
THEN ABS(ISNULL(iQuantity,0))-ABS(ISNULL(iKPQuantity,0))  WHEN ISNULL(iQuantity,0)<>0 AND cBusType<>N'直运销售' THEN ABS(ISNULL(iQuantity,0))-ABS(ISNULL(iFHQuantity,0)) end as decimal(26,9)) >0 and 
(isnull(iflowid,0)=0)))";
            //return OperationSql.GetDataset(strSql, connectionString, out Details, out errMsg);
            int flag = -1;
            try
            {
                Details = DBHelperSQL.Query(connectionString, strSql);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }
        #endregion
        

        #region 生成销售发货单
       
        /// <summary>
        /// 根据销售订单生成销售发货单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int SaveDispatchList(DispatchList dl, string connectionString, string accid, string year, out string errMsg)
        {
            int int_i;
            errMsg = "";
            //创建数据对象
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
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
                string cVouchName = "发货单";
                string cVouchName1 = "委托代销发货单";
                string cardnumber, vt_id;//单据类型编码 模板号
                DataSet Vouchers = new DataSet();
                cmd.CommandText = string.Format("select def_id,cardnumber from Vouchers where ccardname='{0}'", dl.cbustype.Equals("普通销售") ? cVouchName : cVouchName1); 
                adp.SelectCommand = cmd;
                adp.Fill(Vouchers);
                vt_id = Vouchers.Tables[0].Rows[0]["def_id"].ToString();
                cardnumber = Vouchers.Tables[0].Rows[0]["cardnumber"].ToString();

                DataSet UA_Identity = new DataSet();
                cmd.CommandText = @"select ifatherid,ichildid from UFSystem..UA_Identity where cvouchtype='DISPATCH' and cAcc_id='" + accid + "'";
                adp.SelectCommand = cmd;
                adp.Fill(UA_Identity);
                id = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][0]);//表头ID
                autoid = Convert.ToInt32(UA_Identity.Tables[0].Rows[0][1]);//表体autoid

                cmd.CommandText = string.Format("select cvouchtype from vouchtype where cvouchname='{0}'", dl.cbustype.Equals("普通销售") ? cVouchName : cVouchName1); //"select cvouchtype from vouchtype where cvouchname='" + cVouchName + "'";
                string cvouchtype = cmd.ExecuteScalar().ToString();//获得vouchtype的单据类型编码
                id = id + 1;
                
                /*
                 * 字符串替换的这种方式不可取，如果有两个参数名类似，就会出现替换错误情况：@ccusperson与@ccuspersoncode
                 * 
                sql = @"insert into dispatchlist(cbustype,ivtid,cdlcode,cvouchtype,cstcode,ddate,dcreatesystime,cdepcode,cpersoncode,csocode,ccuscode,cexch_name,iexchrate,itaxrate,cmemo,cdefine2,cdefine3,cdefine10,cdefine11,breturnflag,dlid,cmaker,bfirst,sbvid,isale,ccusname,ccusperson,iflowid,bcredit,iverifystate,iswfcontrolled,bcashsale,bsigncreate,cshipaddress,cSCCode,caddcode,ccuspersoncode,bneedbill,cinvoicecompany)";
                sql += @"Values (@cbustype,@ivtid,@cdlcode,@cvouchtype,N'01',@ddate,@dcreatesystime,@cdepcode,@cpersoncode,@csocode,@ccuscode,@cexch_name,@iexchrate,@itaxrate,@cmemo,@cdefine2,@cdefine3,@cdefine10,@cdefine11,0,@dlid,@cmaker,0,0,0,@ccusname,@ccusperson,0,0,0,0,0,0,@cshipaddress,@cSCCode,@caddcode,@ccuspersoncode,@bneedbill,@cinvoicecompany)";
                sql = sql.Replace(@"@cbustype", SelSql(dl.cbustype));
                sql = sql.Replace(@"@ivtid", SelSql(vt_id));
                sql = sql.Replace(@"@cdlcode", SelSql((id + 860000000).ToString()));//dl.cDLCode);
                sql = sql.Replace(@"@cvouchtype", SelSql(cvouchtype));
                sql = sql.Replace(@"@cstcode", SelSql(dl.cstcode));
                sql = sql.Replace(@"@ddate", "'" + dd + "'");//单据日期
                sql = sql.Replace(@"@dcreatesystime", "'" + dt + "'");//制单时间  
                sql = sql.Replace(@"@cdepcode", SelSql(dl.cdepcode));
                sql = sql.Replace(@"@cpersoncode", SelSql(dl.cpersoncode));
                sql = sql.Replace(@"@csocode", SelSql(dl.csocode));
                sql = sql.Replace(@"@ccuscode", SelSql(dl.ccuscode));
                sql = sql.Replace(@"@cexch_name", SelSql(dl.cexch_name));
                sql = sql.Replace(@"@iexchrate", dl.iExchRate.ToString());
                sql = sql.Replace(@"@itaxrate", dl.itaxrate.ToString());
                sql = sql.Replace(@"@cmemo", SelSql(dl.cmemo));
                sql = sql.Replace(@"@cdefine2", SelSql(dl.cdefine2));
                sql = sql.Replace(@"@cdefine3", SelSql(dl.cdefine3));
                sql = sql.Replace(@"@cdefine10", SelSql(dl.cdefine10));//监管码
                sql = sql.Replace(@"@cdefine11", SelSql(dl.cdefine11));
                sql = sql.Replace(@"@dlid", (id + 860000000).ToString());
                sql = sql.Replace(@"@cmaker", SelSql(dl.cmaker));
                sql = sql.Replace(@"@ccusname", SelSql(dl.ccusname));
                sql = sql.Replace(@"@ccusperson", SelSql(dl.ccusperson));
                sql = sql.Replace(@"@cshipaddress", SelSql(dl.ccusoaddress));
                sql = sql.Replace(@"@cSCCode", string.IsNullOrEmpty(dl.cSCCode) ? "null" : SelSql(dl.cSCCode));//发送方式编码
                sql = sql.Replace(@"@caddcode", SelSql(dl.caddcode));//发货地址编码（为了关联查询收货人信息）
                //2013-11-10    U811.1添加
                sql = sql.Replace(@"@ccuspersoncode", SelSql(dl.ccuspersoncode));//联系人编码 
                sql = sql.Replace(@"@bneedbill", string.IsNullOrEmpty(dl.cinvoicecompany) ? "0" : "1");//是否开票，目前根据开票单位来判断
                sql = sql.Replace(@"@cinvoicecompany", SelSql(dl.cinvoicecompany));//开票单位编码 

                throw new Exception(sql);
                */
                
                sql = string.Format(@"insert into dispatchlist(cbustype,ivtid,cdlcode,cvouchtype,cstcode,ddate,dcreatesystime,cdepcode,cpersoncode,csocode,ccuscode,cexch_name,iexchrate,itaxrate,cmemo,cdefine2,cdefine3,cdefine10,cdefine11,breturnflag,dlid,cmaker,bfirst,sbvid,isale,ccusname,ccusperson,iflowid,bcredit,iverifystate,iswfcontrolled,bcashsale,bsigncreate,cshipaddress,cSCCode,caddcode,ccuspersoncode,bneedbill,cinvoicecompany)
Values (@cbustype,@ivtid,@cdlcode,@cvouchtype,@cstcode,@ddate,@dcreatesystime,@cdepcode,@cpersoncode,@csocode,@ccuscode,@cexch_name,@iexchrate,@itaxrate,@cmemo,@cdefine2,@cdefine3,@cdefine10,@cdefine11,@breturnflag,@dlid,@cmaker,@bfirst,@sbvid,@isale,@ccusname,@ccusperson,@iflowid,@bcredit,@iverifystate,@iswfcontrolled,@bcashsale,@bsigncreate,@cshipaddress,@cSCCode,@caddcode,@ccuspersoncode,@bneedbill,@cinvoicecompany);");
                SqlParameter[] parms =
                {
                    new SqlParameter("@cbustype",SqlDbType.NVarChar,8),//业务类型
                    new SqlParameter("@ivtid",SqlDbType.Int),//模板号
                    new SqlParameter("@cdlcode",SqlDbType.NVarChar,30),//发货退货单号
                    new SqlParameter("@cvouchtype",SqlDbType.NVarChar,2),//单据类型编码 
                    new SqlParameter("@cstcode",SqlDbType.NVarChar,2),//销售类型编码 
                    new SqlParameter("@ddate",SqlDbType.DateTime),//单据日期 
                    new SqlParameter("@dcreatesystime",SqlDbType.DateTime),//制单时间 
                    new SqlParameter("@cdepcode",SqlDbType.NVarChar,12),//部门编码 
                    new SqlParameter("@cpersoncode",SqlDbType.NVarChar,20),//业务员编码 
                    new SqlParameter("@csocode",SqlDbType.NVarChar,30),//销售订单号 
                    new SqlParameter("@ccuscode",SqlDbType.NVarChar,20),//客户编码 
                    new SqlParameter("@cexch_name",SqlDbType.NVarChar,8),//币种名称 
                    new SqlParameter("@iexchrate",SqlDbType.Float),//汇率 
                    new SqlParameter("@itaxrate",SqlDbType.Float),//表头税率 
                    new SqlParameter("@cmemo",SqlDbType.NVarChar,60),//备注 
                    new SqlParameter("@cdefine2",SqlDbType.NVarChar,20),//自定义项2 
                    new SqlParameter("@cdefine3",SqlDbType.NVarChar,20),//自定义项3
                    new SqlParameter("@cdefine10",SqlDbType.NVarChar,60),//自定义项60
                    new SqlParameter("@cdefine11",SqlDbType.NVarChar,120),//自定义项120 
                    new SqlParameter("@dlid",SqlDbType.Int),//发货退货单主表标识
                    new SqlParameter("@cmaker",SqlDbType.NVarChar,20),//制单人 
                    new SqlParameter("@ccusname",SqlDbType.NVarChar,120),//客户名称 
                    new SqlParameter("@ccusperson",SqlDbType.NVarChar,100),//客户联系人 
                    new SqlParameter("@cshipaddress",SqlDbType.NVarChar,200),//发往地址 
                    new SqlParameter("@cSCCode",SqlDbType.NVarChar,2),//发运方式编码 
                    new SqlParameter("@caddcode",SqlDbType.NVarChar,30),//发货地址编码 
                    new SqlParameter("@ccuspersoncode",SqlDbType.NVarChar,30),//联系人编码 
                    new SqlParameter("@bneedbill",SqlDbType.Bit),//是否需要开票
                    new SqlParameter("@cinvoicecompany",SqlDbType.NVarChar,20),//开票单位编码 

                    //那些固定写1或0 的参数
                    new SqlParameter("@breturnflag",SqlDbType.Bit),//退货标志 
                    new SqlParameter("@bfirst",SqlDbType.Bit),//销售期初标志
                    new SqlParameter("@sbvid",SqlDbType.Int),//销售发票主表标识
                    new SqlParameter("@isale",SqlDbType.SmallInt),//是否先发货 
                    new SqlParameter("@iflowid",SqlDbType.Int),//流程id 
                    new SqlParameter("@bcredit",SqlDbType.Bit),//是否立账单据 
                    new SqlParameter("@iverifystate",SqlDbType.Int),//审核状态 
                    new SqlParameter("@iswfcontrolled",SqlDbType.TinyInt),//启用工作流 
                    new SqlParameter("@bcashsale",SqlDbType.Bit),//现款结算 
                    new SqlParameter("@bsigncreate",SqlDbType.Bit)//签回损失生成 
                };
                parms[0].Value = dl.cbustype;
                parms[1].Value = Convert.ToInt32(vt_id);
                parms[2].Value = id.ToString();//后面做修改
                parms[3].Value = cvouchtype;
                parms[4].Value = dl.cstcode;
                parms[5].Value = Convert.ToDateTime(dd);
                parms[6].Value = Convert.ToDateTime(dt);
                parms[7].Value = dl.cdepcode;
                parms[8].Value = dl.cpersoncode;
                parms[9].Value = dl.csocode;
                parms[10].Value = dl.ccuscode;
                parms[11].Value = dl.cexch_name;
                parms[12].Value = dl.iExchRate;
                parms[13].Value = dl.itaxrate;
                parms[14].Value = dl.cmemo;
                parms[15].Value = dl.cdefine2;
                parms[16].Value = dl.cdefine3;
                parms[17].Value = dl.cdefine10;
                parms[18].Value = dl.cdefine11;
                parms[19].Value = 860000000 + id;
                parms[20].Value = dl.cmaker;
                parms[21].Value = dl.ccusname;
                parms[22].Value = dl.ccusperson;
                parms[23].Value = dl.ccusoaddress;
                parms[24].Value = string.IsNullOrEmpty(dl.cSCCode) ? DBNull.Value : (object)dl.cSCCode;
                parms[25].Value = dl.caddcode;

                parms[26].Value = dl.ccuspersoncode;
                parms[27].Value = string.IsNullOrEmpty(dl.cinvoicecompany) ? 0 : 1;
                parms[28].Value = dl.cinvoicecompany;

                //那些固定写的0或1
                parms[29].Value = 0;
                parms[30].Value = 0;
                parms[31].Value = 0;
                parms[32].Value = 0;
                parms[33].Value = 0;
                parms[34].Value = 0;
                parms[35].Value = 0;
                parms[36].Value = 0;
                parms[37].Value = 0;
                parms[38].Value = 0;

                cmd.CommandText = sql;
                //添加参数
                cmd.Parameters.AddRange(parms);
                if (cmd.ExecuteNonQuery() < 1)
                {
                    myTran.Rollback();
                    errMsg = "主表插入失败！";
                    return -1;
                }
                //清空命令行中的参数
                cmd.Parameters.Clear();

                int irowno = 1;


                //已插入的存货及批次
                //Dictionary<string, string> list = new Dictionary<string, string>();
                List<DispatchDetail> list = new List<DispatchDetail>();
                ///货位添加思路：
                ///循环查询集合中某存货所有批次，若存货批次列表中没有该批次，再从集合中查找所有同批次的数据 
                ///循环插入记录，然后把该存货批次添加到存货批次列表中，下次直接跳过所有该存货批的数据
                foreach (DispatchDetail detail in dl.OperateDetails)
                {
                    //如果该批次已经插入 则跳过本次循环
                    //如果同一存货该批次已经插入 则跳过本次循环
                    bool flag = false;//标识存货的某一批次是否已经插入，默认为false;
                    foreach (DispatchDetail l in list)
                    {
                        if (l.cinvcode == detail.cinvcode && l.invbatch == detail.invbatch)
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
                    var v = from od in dl.OperateDetails where od.cinvcode == detail.cinvcode && od.invbatch == detail.invbatch select od;
                    decimal iquantity = 0;//记录该批次货物总数量
                    //声明变量，一些金额因为分货位而分散，所有要计算同存货同批次的金额总合
                    //分别是：无税金额，原币税额,价税合计，原币折扣额，本币无税金额，本币税额，本币价税合计，本币折扣额，零售金额
                    decimal imoney = 0, itax = 0, isum = 0, idiscount = 0, inatmoney = 0, inattax = 0, inatsum = 0, inatdiscount = 0, fsaleprice = 0;

                    ///货位字符串
                    string strBatch = string.Empty;
                    foreach (DispatchDetail temp in v)
                    {
                        //如果一个没有货位，说明所有都没有，然后直接跳出
                        if (string.IsNullOrEmpty(temp.cposition))
                        {
                            iquantity = temp.inewquantity;
                            imoney = temp.imoney;//无税金额
                            itax = temp.itax;//原币税额
                            isum = temp.isum;//价税合计
                            idiscount = temp.idiscount;//原币折扣额
                            inatmoney = temp.inatmoney;//本币无税额
                            inattax = temp.inattax;//本币税额
                            inatsum = temp.inatsum;//本币价税合计
                            inatdiscount = temp.inatdiscount;//本币折扣额
                            fsaleprice = temp.fsaleprice;//零售金额
                            break;
                        }
                        strBatch += string.Format("{0}:{1}@", temp.cposition, temp.inewquantity.ToString("F2"));
                        iquantity += temp.inewquantity;
                        //累加（2012-11-16改）
                        imoney += temp.imoney;
                        itax += temp.itax;
                        isum += temp.isum;
                        idiscount += temp.idiscount;
                        inatmoney += temp.inatmoney;
                        inattax += temp.inattax;
                        inatsum += temp.inatsum;
                        inatdiscount += temp.inatdiscount;
                        fsaleprice += temp.fsaleprice;
                    }
                    //如果不为空
                    if (!string.IsNullOrEmpty(strBatch))
                        strBatch = strBatch.Substring(0, strBatch.Length - 1);


                    autoid++;
                    sql = @"Insert Into dispatchlists(dlid,cwhcode,cinvcode,iquantity,inum,iquotedprice,iunitprice,itaxunitprice,imoney,itax,isum,idiscount,inatunitprice,inatmoney,inattax,inatsum,inatdiscount,cbatch,itb,dvdate,isosid,idlsid,kl,kl2,cinvname,itaxrate,cdefine22,fsalecost,fsaleprice,cvenabbname,dmdate,bgsp,csocode,imassdate,cmassunit,bqaneedcheck,bqaurgency,bcosting,cordercode,iorderrowno,fcusminprice,irowno,iexpiratdatecalcu,dexpirationdate,cexpirationdate,cvencode,bneedsign,frlossqty,cDefine33,cdefine25,bsaleprice,bgift)"
                           + @" Values (@dlid,@cwhcode,@cinvcode,@iquantity,0,@iquotedprice,@iunitprice,@itaxunitprice,@imoney,@itax1,@isum,@idiscount,@inatunitprice,@inatmoney,@inattax,@inatsum,@inatdiscount,@cbatch,0,@dvdate,@isosid,@idlsid,@kl,@kl2,@cinvname,@itaxrate,@cdefine22,@fsalecost,@fsaleprice,@cvenabbname,@dmdate, 0,@csocode,@imassdate, @cmassunit, 0, 0, 1,@cordercode,@iorderrowno,0,@irowno,@iexpiratdatecalcu,@dexpirationdate,@cexpirationdate,@cvencode,0,0,@cDefine33,@cdefine25,@bsaleprice,@bgift)";
                    sql = sql.Replace("@dlid", (id + 860000000).ToString());
                    sql = sql.Replace("@cwhcode", SelSql(detail.cwhcode));
                    sql = sql.Replace("@cinvcode", SelSql(detail.cinvcode));
                    sql = sql.Replace("@iquantity", iquantity.ToString());//detail.inewquantity.ToString());
                    sql = sql.Replace("@iquotedprice", detail.iquotedprice.ToString());
                    sql = sql.Replace("@iunitprice", detail.iunitprice.ToString());
                    sql = sql.Replace("@itaxunitprice", detail.itaxunitprice.ToString());
                    sql = sql.Replace("@imoney", imoney.ToString());//detail.imoney.ToString());
                    sql = sql.Replace("@itax1", itax.ToString());//detail.itax.ToString());
                    sql = sql.Replace("@isum", isum.ToString());//detail.isum.ToString());
                    sql = sql.Replace("@idiscount", idiscount.ToString());//detail.idiscount.ToString());
                    sql = sql.Replace("@inatunitprice",detail.inatunitprice.ToString());
                    sql = sql.Replace("@inatmoney", inatmoney.ToString());//detail.inatmoney.ToString());
                    sql = sql.Replace("@inattax", inattax.ToString());//detail.inattax.ToString());
                    sql = sql.Replace("@inatsum", inatsum.ToString());//detail.inatsum.ToString());
                    sql = sql.Replace("@inatdiscount", inatdiscount.ToString());//detail.inatdiscount.ToString());
                    sql = sql.Replace("@cbatch", SelSql(detail.invbatch));
                    sql = sql.Replace("@dvdate", "'" + detail.dvdate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@isosid", detail.isosid.ToString());
                    sql = sql.Replace("@idlsid", (autoid + 860000000).ToString());
                    sql = sql.Replace("@kl", detail.kl.ToString());
                    sql = sql.Replace("@kl2", detail.kl2.ToString());
                    sql = sql.Replace("@cinvname", SelSql(detail.cinvname));
                    sql = sql.Replace("@itaxrate", detail.itaxrate.ToString());
                    sql = sql.Replace("@cdefine22", SelSql(detail.cdefine22));
                    sql = sql.Replace("@fsalecost", detail.fsalecost.ToString());
                    sql = sql.Replace("@fsaleprice", fsaleprice.ToString());//detail.fsaleprice.ToString());
                    sql = sql.Replace("@cvenabbname", SelSql(detail.cvenabbname));
                    sql = sql.Replace("@dmdate", "'" + detail.dmdate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@csocode", SelSql(detail.csocode));
                    sql = sql.Replace("@cmassunit", detail.cmassunit.ToString());
                    sql = sql.Replace("@imassdate", detail.imassdate.ToString());
                    sql = sql.Replace("@cordercode", SelSql(detail.cordercode));
                    sql = sql.Replace("@iorderrowno", detail.iorderrowno.ToString());
                    sql = sql.Replace("@irowno", irowno.ToString());
                    sql = sql.Replace("@iexpiratdatecalcu", detail.iexpiratdatecalcu.ToString());
                    sql = sql.Replace("@dexpirationdate", "'" + detail.dexpirationdate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@cexpirationdate", "'" + detail.cexpirationdate.ToString("yyyy-MM-dd") + "'");
                    sql = sql.Replace("@cvencode", SelSql(detail.cvencode));

                    ///插入自定义项33，存储货位信息
                    sql = sql.Replace("@cDefine33", string.IsNullOrEmpty(strBatch) ? "null" : SelSql(strBatch));
                    sql = sql.Replace("@cdefine25", SelSql(detail.cdefine25));//请货单号

                    sql = sql.Replace("@bsaleprice", detail.bsaleprice.ToString());//报价含税标识
                    sql = sql.Replace("@bgift", detail.bgift.ToString());//是否赠品
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        myTran.Rollback();
                        return -1;
                    }
                    irowno++;

                    ///手动更新CurrentStock表中的待发货数量：生成发货单后，CurrentStock表中的待数量没有增加，目前还没有找到原因
                    ///所有在这里手动更新
                    ///todo this is error!
                    sql = string.Format("UPDATE dbo.CurrentStock SET fOutQuantity=fOutQuantity+{0} WHERE cWhCode='{1}' AND cInvCode='{2}' AND cBatch='{3}'",iquantity,detail.cwhcode,detail.cinvcode,detail.invbatch);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    //把该批次添加到list中
                    list.Add(new DispatchDetail() { cinvcode = detail.cinvcode, invbatch = detail.invbatch });
                }
                sql = "select @@spid";
                cmd.CommandText = sql;
                spid = cmd.ExecuteScalar().ToString ();
                if(string.IsNullOrEmpty(spid))
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

                sql = string.Format("exec Usp_SCM_CommitGeneralLedgerWithCheck N'SA',1,1,1,1,1,1,1,1,1,1,1,0,1 ,0,'spid_{0}'",spid);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                /*
                 * 2013-11-15                                                   
                 * 此操作用来验证发货数量是否超过订货数量                      
                 * (用于解决U8与终端同时操作同一个单据时出现两个相同的销售发货单)
                 * 
                 * */
                sql = string.Format(@"SELECT * FROM 
(
	SELECT 
		CAST( (CASE WHEN ISNULL(iQuantity,0)>0 THEN 1 WHEN ISNULL(iQuantity,0)<0 THEN -1 ELSE 0 END )* (ISNULL(iQuantity,0)-ISNULL(iFHQuantity,0)) AS decimal(30,1) ) AS iQuantity ,
		CAST( (CASE WHEN ISNULL(iNum,0)>0 THEN 1 WHEN ISNULL(iNum,0)<0 THEN -1 ELSE 0 END )* (ISNULL(iNum,0)-ISNULL(iFHNum,0)) AS decimal(30,1) ) AS iNum ,
		cInvCode,cInvName 
	FROM dbo.SO_SODetails WHERE cSOCode='{0}'
) temp WHERE temp.iQuantity<0 OR temp.iNum<0",dl.csocode);
                cmd.CommandText = sql;
                adp.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                adp.Fill(dataTable);
                if (dataTable.Rows.Count > 0)   //如果有数据 说明这些存货的数量超过了订单数量
                {
                    errMsg = "此单据可能已生成发货单！\r\n";
                    foreach (DataRow row in dataTable.Rows)
                    {
                        errMsg += string.Format("名称：[{0}] 编号：[{1}] 此存货的发货数量超过了订单数量！\r\n", row["cinvname"], row["cinvcode"]);
                    }

                    myTran.Rollback();//开始回滚
                    return -1;
                }

                ///单据处理
                ///-----------------------------
                string cSeed;//= dl.cpersoncode.Substring(dl.cpersoncode.Length - 3, 3) + dd.Replace("-", "");
                ///判断是普通销售还是委托代销
                if (cardnumber == "01")//表示普通销售
                {
                    ///单据编号修改规则
                    ///普通销售发货单：单据年月（6位）+流水号（4位）
                    ///流水号：根据单据日期，规则：月
                    cSeed = DateTime.Now.ToString("yyMM");
                    sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='01' and cContent='单据日期' and cSeed='{0}'", cSeed);
                    cmd.CommandText = sql;
                    ocode = cmd.ExecuteScalar();
                    if (ocode == null || ocode.Equals(DBNull.Value))
                    {
                        icode = 1;
                        sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('01','单据日期','月','{0}','1')", cSeed);
                    }
                    else
                    {
                        icode = Convert.ToInt32(ocode) + 1;
                        sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='01' and cContent='单据日期' and cSeed='{1}'", icode,  cSeed);
                    }
                    //普通销售的流水号为当月的4位
                    cSeed = DateTime.Now.ToString("yyyyMM");
                    ccode = cSeed + icode.ToString().PadLeft(4, '0');
                }
                else   //表示委托供销发货单
                {
                    ///单据编号修改规则
                    ///委托销售发货单：单据年月日（8位）+流水号（4位）
                    ///流水号：根据单据日期，规则：日
                    cSeed = DateTime.Now.ToString("yyyyMMdd");
                    sql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (ROWLOCK)  Where  CardNumber='05' and cContent='单据日期' and cSeed='{0}'",cSeed);
                    cmd.CommandText = sql;
                    ocode = cmd.ExecuteScalar();
                    if (ocode == null || ocode.Equals(DBNull.Value))
                    {
                        icode = 1;
                        sql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('05','单据日期','日','{0}','1')", cSeed);
                    }
                    else
                    {
                        icode = Convert.ToInt32(ocode) + 1;
                        sql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='05' and cContent='单据日期' and cSeed='{1}'", icode, cSeed);
                    }
                    ///委托代销的流水号为当日的4位
                    ccode = cSeed + icode.ToString().PadLeft(4, '0');
                }
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return -1;
                }

                sql = string.Format("select dlid from dispatchlist where cvouchtype='{0}' and cdlcode='{1}'", cvouchtype, ccode);
                cmd.CommandText = sql;
                if (cmd.ExecuteScalar() != null)
                {
                    myTran.Rollback();
                    errMsg = "单据号重复,请再次提交!" + ccode;
                    return -1;
                }
                sql = "Update dispatchlist Set cdlcode = N'" + ccode + "' Where dlid = " + (id + 860000000).ToString() + "";
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    myTran.Rollback();
                    errMsg = "update dispatchlist cdlcode error!";
                    return -1;
                }
                ///------------------------

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
                    data.CardName = dl.cbustype.Equals("普通销售") ? cVouchName : cVouchName1;
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
                sql = string.Format("update UFSystem..UA_Identity set ifatherid={0},ichildid={1} where cvouchtype='DISPATCH' and cAcc_id={2}",id,autoid,accid);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                myTran.Commit();
                int_i = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                return -1;
            }
            finally
            {
                conn.Close();
            }
            return int_i;
        }
        #endregion


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
        public static int GetBatchList(string cInvCode, string cWhCode, string cPosition, string ConnectionString, out DataSet dsBatch, out string errMsg)
        {
            string SQL = string.Empty;
            dsBatch = null;
            errMsg = string.Empty;
            ///判断是否有货位
            ///如果没有货位只查询批次，否则查询货位批次
            if (string.IsNullOrEmpty(cPosition))
            {
                SQL = string.Format(@"select cwhcode,cinvcode,cBatch,iQuantity,dVDate,dMdate,iMassDate,cMassUnit,cExpirationdate 
from currentstock where cinvcode ='{0}' and cWhCode='{1}' AND iQuantity <>0", cInvCode, cWhCode);
            }
            else
            {
                //下面是跟踪用友货位存量查询语句的修改
                SQL = string.Format(@"
--临时表a
if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempa') AND xtype='U')
DROP TABLE #tempa
SELECT DISTINCT CS.AutoID, CS.RdsID, CS.RdID, CS.cWhCode,W.cWhName, CS.cPosCode AS cPosCode, 
 Position.cPosName AS cPosName, CS.cInvCode, CS.cBatch,isnull(cs.cvmivencode,N'') as cvmivencode,v1.cvenabbname as cvmivenname ,
 CS.dMadeDate AS dMDate, CS.iMassDate AS iMassDate, 
 CS.cMassUnit, isnull(E.enumname,N'') AS cMassUnitName, CS.dVDate, CS.iQuantity, CS.iNum, CS.cMemo, CS.cHandler,CS.dDate, CS.bRdFlag, CS.cSource, 
 CS.cFree1, CS.cFree2, CS.cFree3, CS.cFree4, CS.cFree5, CS.cFree6,CS.cFree7, CS.cFree8, CS.cFree9, CS.cFree10, 
Batch.cBatchProperty1,Batch.cBatchProperty2,Batch.cBatchProperty3,Batch.cBatchProperty4,Batch.cBatchProperty5,Batch.cBatchProperty6,Batch.cBatchProperty7,Batch.cBatchProperty8,Batch.cBatchProperty9,Batch.cBatchProperty10,v2.enumname as 有效期推算方式,CS.cExpirationdate as 有效期至, CS.cAssUnit, CS.cBVencode,I.cInvAddCode, I.cInvName,I.cInvStd,  I.cInvDefine1,I.cInvDefine2,I.cInvDefine3,I.cInvDefine4,I.cInvDefine5,I.cInvDefine6,I.cInvDefine7,I.cInvDefine8,I.cInvDefine9,I.cInvDefine10,  I.cInvDefine11,I.cInvDefine12,I.cInvDefine13,I.cInvDefine14,I.cInvDefine15,I.cInvDefine16,CASE WHEN I.iGroupType = 0 THEN NULL 
     WHEN I.iGroupType = 2 THEN (CASE WHEN CS.iQuantity = 0.0 OR CS.iNum = 0.0 THEN NULL ELSE CS.iQuantity/CS.iNum END) 
     WHEN I.iGroupType = 1 THEN CU_G.iChangRate END AS iExchRate
, I.cInvCCode AS cInvCCode, I.iGroupType, CU_M.cComUnitName AS cInvM_Unit, CASE WHEN I.iGroupType = 0 THEN NULL 
 WHEN I.iGrouptype = 2 THEN CU_A.cComUnitName 
 WHEN I.iGrouptype = 1 THEN CU_G.cComUnitName END 
 AS cInvA_Unit, CU_G.iChangRate, 
 InventoryClass.cInvCName AS cInvCName
 INTO #tempa FROM Warehouse W with (nolock) RIGHT OUTER JOIN dbo.InvPosition CS  with (nolock) ON W.cWhCode = CS.cWhCode LEFT OUTER JOIN ComputationUnit CU_A RIGHT OUTER JOIN  dbo.Inventory I ON CU_A.cComunitCode = I.cAssComUnitCode LEFT OUTER JOIN dbo.ComputationUnit CU_M ON I.cComUnitCode = CU_M.cComunitCode  LEFT OUTER JOIN ComputationUnit CU_G ON  I.cSTComUnitCode = CU_G.cComUnitCode  ON CS.cInvCode = I.cInvCode 
 LEFT JOIN Position ON CS.cPosCode=Position.cPosCode LEFT JOIN InventoryClass ON InventoryClass.cInvCCode=I.cInvCCode 
 LEFT OUTER JOIN v_aa_enum E with (nolock) ON E.EnumCode=convert(nvarchar,CS.cMassUnit) and E.enumType=N'ST.MassUnit'  left join vendor v1 on cs.cvmivencode = v1.cvencode  left join v_aa_enum v2 on v2.enumcode=ISNULL(CS.iExpiratDateCalcu,0) and v2.enumtype=N'SCM.ExpiratDateCalcu' 
 left join V_ST_AA_BatchProperty batch on Batch.cbinvcode=CS.cinvcode and isnull(Batch.cbbatch,N'')=isnull(CS.cbatch,N'') and isnull(Batch.cbfree1,N'')=isnull(CS.cfree1,N'') and isnull(Batch.cbfree2,N'')=isnull(CS.cfree2,N'') and isnull(Batch.cbfree3,N'')=isnull(CS.cfree3,N'') and isnull(Batch.cbfree4,N'')=isnull(CS.cfree4,N'') and isnull(Batch.cbfree5,N'')=isnull(CS.cfree5,N'') and isnull(Batch.cbfree6,N'')=isnull(CS.cfree6,N'') and isnull(Batch.cbfree7,N'')=isnull(CS.cfree7,N'') and isnull(Batch.cbfree8,N'')=isnull(CS.cfree8,N'') and isnull(Batch.cbfree9,N'')=isnull(CS.cfree9,N'') and isnull(Batch.cbfree10,N'')=isnull(CS.cfree10,N'')
 WHERE I.cInvCode = N'{0}' AND w.cWhCode=N'{1}'

--临时表b
if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempb') AND xtype='U')
DROP TABLE #tempb

SELECT cWhCode,cWhName,cInvCode,cInvAddCode,cInvName,cInvStd,cInvCCode,cInvCName,cInvM_Unit,cInvA_Unit,cBatch,cvmivencode,cvmivenname,dMdate,iMassDate,cMassUnit,cMassUnitName,dVDate,cPosCode,cPosName,有效期推算方式,有效期至,cFree1 , cFree2, cFree3, cFree4, cFree5, cFree6, cFree7, cFree8, cFree9, cFree10,  cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cBatchProperty1,cBatchProperty2,cBatchProperty3,cBatchProperty4,cBatchProperty5,cBatchProperty6,cBatchProperty7,cBatchProperty8,cBatchProperty9,cBatchProperty10, cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,(CASE WHEN iGroupType = 0 THEN NULL 
     WHEN iGroupType = 1 THEN AVG(iChangRate) 
     WHEN iGroupType = 2 THEN CASE WHEN ROUND(SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END),6) <> 0 THEN SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iQuantity,0) ELSE -ISNULL(iQuantity,0) END)/SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END) ELSE NULL END 
 ELSE NULL END) 
 as iExchRate,
 Round(SUM(CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iQuantity,0) ELSE -ISNULL(iQuantity,0) END ),6) AS iQtty, 
 Round(SUM( CASE WHEN iGroupType = 0 THEN 0 WHEN iGroupType = 2 THEN (CASE WHEN CS.bRdFlag = 1 THEN ISNULL(iNum,0) ELSE -ISNULL(iNum,0) END) 
                                      WHEN iGroupType = 1 THEN (CASE WHEN CS.bRdFlag = 1 THEN iQuantity/ iChangRate ELSE -iQuantity/ iChangRate END) END),6) AS iNum
 INTO #tempb FROM #tempa AS CS WHERE cs.cPosCode=N'{2}'
 GROUP BY cWhCode,cWhName,cInvCode,cInvAddCode,cInvName,cInvStd,cInvCCode,cInvCName,cInvM_Unit,cInvA_Unit,cBatch,cvmivencode,cvmivenname,dMdate,iMassDate,cMassUnit,cMassUnitName,dVDate,cPosCode,cPosName,有效期推算方式,有效期至,cFree1 , cFree2, cFree3, cFree4, cFree5, cFree6, cFree7, cFree8, cFree9, cFree10,  cInvDefine1,cInvDefine2,cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,cInvDefine10,cBatchProperty1,cBatchProperty2,cBatchProperty3,cBatchProperty4,cBatchProperty5,cBatchProperty6,cBatchProperty7,cBatchProperty8,cBatchProperty9,cBatchProperty10, cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,iGroupType
 
 --临时表c
 if exists( select 1 from tempdb..sysobjects where id=OBJECT_ID(N'tempdb..#tempc') AND xtype='U')
DROP TABLE #tempc
 SELECT [cWhCode],[cWhName] as [cWhName],[cPosCode],[cInvCode],[cInvName] as [cInvName],[cInvStd] as [cInvStd],[cInvAddCode] as [cInvAddCode],[cInvCCode] as [cInvCCode],[cInvCName] as [cInvCName],[cInvA_Unit] as [cInvA_Unit],round([iExchRate],6) as [iExchRate],[cInvDefine2] as [cInvDefine2],[cInvDefine3] as [cInvDefine3],[cInvDefine4] as [cInvDefine4],[cInvDefine5] as [cInvDefine5],[cInvDefine6] as [cInvDefine6],[cInvDefine7] as [cInvDefine7],[cInvDefine8] as [cInvDefine8],[cInvDefine9] as [cInvDefine9],[cInvDefine10] as [cInvDefine10],round([cInvDefine11],2) as [cInvDefine11],round([cInvDefine12],2) as [cInvDefine12],round([cInvDefine13],2) as [cInvDefine13],round([cInvDefine14],2) as [cInvDefine14],[cInvDefine15] as [cInvDefine15],[cInvDefine16] as [cInvDefine16],[cFree1] as [cFree1],[cFree2] as [cFree2],[cFree3] as [cFree3],[cFree4] as [cFree4],[cFree5] as [cFree5],[cFree6] as [cFree6],[cFree7] as [cFree7],[cFree8] as [cFree8],[cFree9] as [cFree9],[cFree10] as [cFree10],[cBatch] as [cBatch],[cvmivencode] as [cvmivencode],round([iNum],2) as [iNum],[cvmivenname] as [cvmivenname],round([iQtty],4) as [iQtty],[cInvM_Unit] as [cInvM_Unit],[dMdate] as [dMdate],[dVDate] as [dVDate],[有效期至] as [有效期至],[cPosName] as [cPosName],[有效期推算方式] as [有效期推算方式],[iMassDate] as [iMassDate],[cMassUnit] as [cMassUnit],[cMassUnitName] as [cMassUnitName],[cInvDefine1] as [cInvDefine1],[cBatchProperty1],[cBatchProperty2],[cBatchProperty3],[cBatchProperty4],[cBatchProperty5],[cBatchProperty6],[cBatchProperty7],[cBatchProperty8],[cBatchProperty9],[cBatchProperty10] Into #tempc from #tempb
 
SELECT [cWhCode],max([cWhName]) as [cWhName],[cPosCode],[cInvCode],max([cInvName]) as [cInvName],max([cInvStd]) as [cInvStd],max([cInvAddCode]) as [cInvAddCode],max([cInvCCode]) as [cInvCCode],max([cInvCName]) as [cInvCName],max([cInvA_Unit]) as [cInvA_Unit],sum(round([iExchRate],6)) as [iExchRate],max([cInvDefine2]) as [cInvDefine2],max([cInvDefine3]) as [cInvDefine3],max([cInvDefine4]) as [cInvDefine4],max([cInvDefine5]) as [cInvDefine5],max([cInvDefine6]) as [cInvDefine6],max([cInvDefine7]) as [cInvDefine7],max([cInvDefine8]) as [cInvDefine8],max([cInvDefine9]) as [cInvDefine9],max([cInvDefine10]) as [cInvDefine10],sum(round([cInvDefine11],2)) as [cInvDefine11],sum(round([cInvDefine12],2)) as [cInvDefine12],sum(round([cInvDefine13],2)) as [cInvDefine13],sum(round([cInvDefine14],2)) as [cInvDefine14],max([cInvDefine15]) as [cInvDefine15],max([cInvDefine16]) as [cInvDefine16],max([cFree1]) as [cFree1],max([cFree2]) as [cFree2],max([cFree3]) as [cFree3],max([cFree4]) as [cFree4],max([cFree5]) as [cFree5],max([cFree6]) as [cFree6],max([cFree7]) as [cFree7],max([cFree8]) as [cFree8],max([cFree9]) as [cFree9],max([cFree10]) as [cFree10],max([cBatch]) as [cBatch],max([cvmivencode]) as [cvmivencode],sum(round([iNum],2)) as [iNum],max([cvmivenname]) as [cvmivenname],sum(round([iQtty],4)) as [iQuantity],max([cInvM_Unit]) as [cInvM_Unit],max([dMdate]) as [dMdate],max([dVDate]) as [dVDate],max([有效期至]) as cExpirationdate,max([cPosName]) as [cPosName],max([有效期推算方式]) as [有效期推算方式],sum([iMassDate]) as [iMassDate],max([cMassUnit]) as [cMassUnit],max([cMassUnitName]) as [cMassUnitName],max([cInvDefine1]) as [cInvDefine1] 
FROM #tempc GROUP BY [cWhCode], [cPosCode], [cInvCode]", cInvCode, cWhCode, cPosition);
            }

            //int result = OperationSql.GetDataset(SQL, ConnectionString, out dsBatch, out errMsg);

            /*
             * 2013-11-18   U811.1
             * 新的货位现存量查询语句
             select Inv.cWhCode,w.cwhname,Inv.cPosCode,p.cposname,Inv.iQuantity,convert(decimal(38,6),case when i.igrouptype=1 and isnull(c.iChangRate,0)<>0 then Inv.iQuantity/c.iChangRate else Inv.inum end) as inum,
convert(decimal(38,6),case when i.igrouptype=0 then 0 when i.igrouptype=1 then ( case when isnull(c.iChangRate,0)<>0 then c.iChangRate else null end ) else (case when isnull(inv.inum,0)=0 then null else Inv.iQuantity/Inv.inum end) end) as iinvexchrate,convert(decimal(38,6),0) as ioutquantity,
convert(decimal(38,6),0) as ioutnum,Inv.cBatch,Inv.cFree1,Inv.cFree2,Inv.cFree3,Inv.cFree4,Inv.cFree5,Inv.cFree6,Inv.cFree7,
Inv.cFree8,Inv.cFree9,Inv.cFree10,Inv.cMassUnit,Inv.iMassDate,Inv.dMadeDate,Inv.dVDate,Inv.iTrackid,Inv.cInVouchType,Inv.cvmivencode,v.cvenname as cvmivenname,Inv.iExpiratDateCalcu,Inv.cExpirationdate,Inv.dExpirationdate,
 a.cbatchproperty1,a.cbatchproperty2,a.cbatchproperty3,a.cbatchproperty4,a.cbatchproperty5,a.cbatchproperty6,a.cbatchproperty7,a.cbatchproperty8,a.cbatchproperty9,a.cbatchproperty10,isnull(Invpos.iprior,20000) as iprior  
 from invpositionsum Inv 
 inner join Inventory I on Inv.cinvcode=I.cinvcode 
 inner join warehouse W on inv.cwhcode=w.cwhcode 
 left join invposcontrapose Invpos on Inv.cposcode=Invpos.cposcode and Invpos.cinvcode=Inv.cinvcode 
 left join Position p on p.cposcode=Inv.cposcode 
 left join Vendor V on inv.cvmivencode=v.cvencode 
 left join ComputationUnit c on i.cstcomunitcode=c.cComunitCode and i.igrouptype=1 
 left join aa_batchproperty a on a.cinvcode=Inv.cinvcode and a.cbatch=inv.cbatch and a.cfree1=inv.cfree1 and  a.cfree2=inv.cfree2 and a.cfree3=inv.cfree3 and a.cfree4=inv.cfree4 and a.cfree5=inv.cfree5 AND a.cfree6=inv.cfree6 and a.cfree7=inv.cfree7 and a.cfree8=inv.cfree8 and a.cfree9=inv.cfree9 and a.cfree10=inv.cfree10 
where Inv.cinvcode=N'DZ2-14-01' and Inv.cwhcode=N'09' and isnull(inv.cbatch,N'')=N'DZ130901' and (inv.iquantity>0 or (i.igrouptype =2 and inv.inum>0) ) 

             * * 
             * */

            //return result;

            int flag = -1;
            try
            {
                dsBatch = DBHelperSQL.Query(ConnectionString, SQL);
                flag = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return flag;
        }


        #region 处理销售出库单，把出库单的货位信息cDefine33，分别插入到货位信息表中
       
        /// <summary>
        /// 处理销售出库单，为销售出库单添加货位信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-12
        /// 2013-05-30添加排它锁
        /// 2013-11-12升级修改为销售出库表为RdRecord32,对应子表RdRecords32
        /// </remarks>
        public static int InsertInvPosition(string connectionString, out string errMsg)
        {
            errMsg = string.Empty;
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
            string sql = string.Empty;
            ///处理结果
            int result = -1;

            try
            {
                lock (objLock)
                {

                    DataSet ds = new DataSet();
                    //先获取所有未处理的出库单(蓝字)，标识cDefine33字段是否为空
                    sql = @"SELECT father.cWhCode,father.cMaker,father.dDate,father.cVouchType,child.* FROM 
                        (SELECT ID,cWhCode,cMaker,dDate,cVouchType FROM dbo.RdRecord32 ) father
                        INNER JOIN (SELECT AutoID,ID,cDefine33,cInvCode,cBatch,dVDate,cAssUnit,iinvexchrate,dMadeDate,iMassDate,cMassUnit,iExpiratDateCalcu,cexpirationdate,dexpirationdate FROM dbo.RdRecords32 WHERE iQuantity > 0 AND cDefine33 IS NOT NULL) child ON father.ID= child.ID";

                    cmd.CommandText = sql;
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    if (ds == null || ds.Tables[0].Rows.Count == 0)//没有要处理的数据
                    {
                        myTran.Rollback();
                        errMsg = "没有要处理的数据";
                        return -2;
                    }


                    //循环表中所有数据并执行插入货位
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //辅计量单位编码与换算率
                        decimal cassunit = row["cAssUnit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cAssUnit"]);
                        decimal iinvexchrate = row["iinvexchrate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["iinvexchrate"]);

                        //插入货位信息表
                        sql = @"insert into InvPosition(rdsid,rdid,cVouchType,cwhcode,cposcode,cinvcode,cbatch,cfree1,cfree2,dvdate,iquantity,inum,cmemo,chandler,ddate,dVouchDate,brdflag,csource,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,cassunit,cbvencode,itrackid,dmadedate,imassdate,cmassunit,cvmivencode,iexpiratdatecalcu,cexpirationdate,dexpirationdate)
                                    values(@rdsid,@rdid,@cVouchType,@cwhcode,'{0}',@cinvcode,@cbatch,null,null,@dvdate,{1},@inum,null,@chandler,@ddate,@dVouchDate,0,null,null,null,null,null,null,null,null,null,@cassunit,null,null,@dmadedate,@imassdate,@cmassunit,null,@iexpiratdatecalcu,@cexpirationdate,@dexpirationdate);";
                        sql = sql.Replace("@rdsid", row["AutoID"].ToString());
                        sql = sql.Replace("@rdid", SelSql(row["ID"].ToString()));
                        sql = sql.Replace("@cVouchType", SelSql(row["cVouchType"].ToString()));
                        sql = sql.Replace("@cwhcode", SelSql(row["cWhCode"].ToString()));
                        //sql = sql.Replace("@cposcode", "'{0}'");
                        sql = sql.Replace("@cinvcode", SelSql(row["cInvCode"].ToString()));
                        sql = sql.Replace("@cbatch", SelSql(row["cBatch"].ToString()));

                        sql = sql.Replace("@dvdate", "'" + row["dVDate"].ToString() + "'");
                        //sql = sql.Replace("@iquantity","{1}");

                        sql = sql.Replace("@inum", "null");
                        sql = sql.Replace("@chandler", SelSql(row["cMaker"].ToString()));
                        sql = sql.Replace("@ddate", "'" + row["dDate"].ToString() + "'");
                        sql = sql.Replace("@dVouchDate", "'" + row["dDate"].ToString() + "'");//u811.1  add

                        sql = sql.Replace("@cassunit", SelSql(row["cAssUnit"].ToString()));
                        sql = sql.Replace("@dmadedate", "'" + row["dMadeDate"].ToString() + "'");
                        sql = sql.Replace("@imassdate", row["iMassDate"].ToString());
                        sql = sql.Replace("@cmassunit", row["cMassUnit"].ToString());
                        sql = sql.Replace("@iexpiratdatecalcu", row["iExpiratDateCalcu"].ToString());
                        sql = sql.Replace("@cexpirationdate", "'" + row["cexpirationdate"].ToString() + "'");
                        sql = sql.Replace("@dexpirationdate", "'" + row["dexpirationdate"].ToString() + "'");

                        string cDefine33 = row["cDefine33"].ToString();
                        string[] pArray = cDefine33.Split('@');//分离数组
                        string tempsql = string.Empty;

                        //修改invPositionSum表语句（u811.1新增数据表）
                        string strInvSum = "UPDATE dbo.InvPositionSum SET iQuantity= iQuantity- {0} WHERE  cWhCode='" +row["cWhCode"]+"' AND cInvCode='"+row["cInvCode"]+"' AND cBatch='"+row["cBatch"]+"' AND cPosCode='{1}';";
                        string tempInvSum = string.Empty;
                        //循环数组
                        foreach (string temp in pArray)
                        {
                            //首先判断格式是否正确
                            if (temp.IndexOf(':') == -1)
                            {
                                break;
                            }

                            //获取货位与数量
                            string cposition = temp.Substring(0, temp.IndexOf(':'));
                            decimal iquantity = Convert.ToDecimal(temp.Substring(temp.IndexOf(':') + 1));
                            tempsql += string.Format(sql, cposition, iquantity);
                            //ImvSum
                            tempInvSum += string.Format(strInvSum, iquantity, cposition);
                        }

                        //插入位置记录表
                        cmd.CommandText = tempsql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }
                        //修改位置存在量表
                        cmd.CommandText = tempInvSum;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }


                        string pTemp = string.Empty;
                        //如果该批次就一个货位，那就更新rdrecords32子表中的cPosition字段为该货位，
                        //如果一个批次多个货位，那么rdrecords32子表的cPosition为null
                        if (pArray.Length == 1 && pArray[0].IndexOf(':') > -1)
                        {
                            //货位
                            string cposition = pArray[0].Substring(0, pArray[0].IndexOf(':'));
                            pTemp = string.Format(",cPosition='{0}'", cposition);

                        }

                        //为该批次插入货位后，修改rdrecords32子表中的cDefine33字段为null
                        sql = string.Format("UPDATE RdRecords32 SET cDefine33=null {0} WHERE AutoID={1}", pTemp, row["AutoID"]);

                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() < 1)
                        {
                            myTran.Rollback();
                            return -1;
                        }

                    }
                    myTran.Commit();
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                myTran.Rollback();
                result = -1;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            return result;
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
