using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

using System.Data.SqlClient;

namespace U8DataAccess
{
    /// <summary>
    /// 采购到货单处理
    /// 2013-11-25
    /// </summary>
    public class PurchaseArrivalProcess
    {
        /// <summary>
        /// 根据采购订单加载订单信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cOrderCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataSet Po_Pomain_Load(string connectionString, string cOrderCode, out string errMsg)
        {
            DataSet ds = new DataSet();
            errMsg = string.Empty;
            string strSql = string.Format(@"SELECT copypolist.nflat AS iExchRate,PO_Pomain.iTaxRate, copypolist.iflowid,copypolist.cflowname,copypolist.cbustype,copypolist.cordercode,copypolist.dpodate,copypolist.cvencode,copypolist.cvenabbname,copypolist.cdepcode,copypolist.cdepname,copypolist.cpersoncode,copypolist.cpersonname,copypolist.csccode,copypolist.cscname,copypolist.cmemo,copypolist.cptcode,copypolist.cptname,copypolist.cpaycode,copypolist.cpayname,copypolist.cdefine1
,copypolist.cdefine2,copypolist.cdefine3,copypolist.cdefine4,copypolist.cdefine5,copypolist.cdefine6,copypolist.cdefine7
,copypolist.cdefine8,copypolist.cdefine9,copypolist.cdefine10,copypolist.cdefine11,copypolist.cdefine12,copypolist.cdefine13,copypolist.cdefine14,copypolist.cdefine15,copypolist.cdefine16,
copypolist.cexch_name,copypolist.cvenpuomprotocol,copypolist.cvenpuomprotocolname 
from copypolist INNER JOIN dbo.PO_Pomain ON dbo.copypolist.poid = dbo.PO_Pomain.POID
where  bposourcearr=1 
and bservice <> 1 
and binvtype<>1 
and isnull(ireceivedqty,0)=0 
and 
	(
		(isnull(iquantity,0)>isnull(iarrqty,0) or CONVERT(DECIMAL(38,4),isnull(iquantity,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrqty,0))) 
		or 
		((isnull(inum,0)>isnull(iarrnum,0) or CONVERT(DECIMAL(38,4),isnull(inum,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrnum,0)))) 
		and iGroupType = 2
	) 
and isnull(copypolist.cbustype,'')<>N'直运采购' 
and	((isnull(copypolist.cVerifier,'')<>'' and isnull(copypolist.cchanger,'')='') or (isnull(copypolist.cchangverifier,'')<>'')) 
and  isnull(copypolist.cbustype,'')<>N'直运采购' 
AND isnull(copypolist.cbustype,'')<>N'委外加工'  
and  copypolist.iDiscountTaxType=0 
and isnull(copypolist.iflowid,0)= '0' 
And	cOrderCode = N'{0}'", cOrderCode);

            //            string strSql = string.Format(@"select distinct a.nflat iexchrate,b.itaxrate,a.ufts,'' as selcol,a.iflowid,a.cflowname,a.cbustype,a.cordercode,a.dpodate,a.cvencode,a.cvenabbname,a.cdepcode,a.cdepname,a.cpersoncode,a.cpersonname,a.csccode,a.cscname,a.cmemo,a.cptcode,a.cptname,a.cpaycode,a.cpayname,a.cdefine1,a.cdefine2,a.cdefine3,a.cdefine4,a. cdefine5,a.cdefine6,a.cdefine7,a.cdefine8,a.cdefine9,a.cdefine10,a.cdefine11,a.cdefine12,a.cdefine13,a.cdefine14,a.cdefine15,a.cdefine16,a. cexch_name,a.cvenpuomprotocol,a.cvenpuomprotocolname 
            //from copypolist a join PO_Pomain b on a.cordercode=b.cpoid where  bposourcearr=1 and bservice <> 1 and binvtype<>1 and isnull(ireceivedqty,0)=0 and ((isnull(iquantity,0)>isnull(iarrqty,0) or CONVERT(DECIMAL(38,4),isnull(iquantity,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrqty,0))) or ((isnull(inum,0)>isnull(iarrnum,0) or CONVERT(DECIMAL(38,2),isnull(inum,0)*(1+isnull(fInExcess,0)))>CONVERT(DECIMAL(38,2),isnull(iarrnum,0)))) and iGroupType = 2) and isnull(a.cbustype,'')<>N'直运采购' and ((isnull(a.cVerifier,'')<>'' and isnull(a.cchanger,'')='') or (isnull(a.cchangverifier,'')<>''))  AND  bInvEntrust=0 AND isnull(a.cbustype,'')=N'普通采购'  and a.iDiscountTaxType=0 and isnull(b.ccloser,'')=''  and cordercode='{0}' ", cOrderCode);
            try
            {
                DataTable dtMain = DBHelperSQL.QueryTable(connectionString, strSql);
                dtMain.TableName = "dtMain";
                ds.Tables.Add(dtMain);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
            strSql = string.Format(@"SELECT Inventory.bInvBatch,Inventory.bInvQuality , copypolist.ufts,copypolist.cvencode,copypolist.nflat as iexchrate,copypolist.poid,copypolist.id,copypolist.btaxcost,copypolist.cinvcode,copypolist.cinvaddcode,copypolist.cinvname,copypolist.cinvstd,copypolist.cinvm_unit,copypolist.bgsp,copypolist.imassdate,isnull(copypolist.iquantity,0) as iquantity,isnull(copypolist.iarrqty,0) as iarrqty,copypolist.iinvexchrate,isnull(copypolist.inum,0) as inum,copypolist.iunitprice,copypolist.itaxprice,copypolist.imoney,copypolist.itax,copypolist.isum,copypolist.inatunitprice,ivouchrowno,copypolist.inatmoney,copypolist.inattax,copypolist.inatsum,isnull(copypolist.ipertaxrate,0) as itaxrate,copypolist.iGroupType,copypolist.bInvEntrust,copypolist.cunitid,copypolist.cinva_unit,copypolist.cfree1,copypolist.cfree2,copypolist.cfree3,copypolist.cfree4,copypolist.cfree5,copypolist.cfree6,copypolist.cfree7,copypolist.cfree8,copypolist.cfree9,copypolist.cfree10,copypolist.cdefine22,copypolist.cdefine23,copypolist.cdefine24,copypolist.cdefine25,copypolist.cdefine26,copypolist.cdefine27,copypolist.cdefine28,copypolist.cdefine29,copypolist.cdefine30,copypolist.cdefine31,copypolist.cdefine32,copypolist.cdefine33,copypolist.cdefine34,copypolist.cdefine35,copypolist.cdefine36,copypolist.cdefine37,copypolist.citemcode,copypolist.citemname,copypolist.citem_class,copypolist.citem_name,copypolist.cinvdefine1,copypolist.cinvdefine2,copypolist.cinvdefine3,copypolist.cinvdefine4,copypolist.cinvdefine5,copypolist.cinvdefine6,copypolist.cinvdefine7,copypolist.cinvdefine8,copypolist.cinvdefine9,copypolist.cinvdefine10,copypolist.cinvdefine11,copypolist.cinvdefine12,copypolist.cinvdefine13,copypolist.cinvdefine14,copypolist.cinvdefine15,copypolist.cinvdefine16,copypolist.contractcode,copypolist.contractrowno,copypolist.contractrowguid,copypolist.irowno,copypolist.csocode,copypolist.sotype,copypolist.sodid,copypolist.cmassunit,copypolist.cwhcode,copypolist.cwhname,copypolist.iinvmpcost,copypolist.cordercode,copypolist.darrivedate,iarrnum,copypolist.iorderdid,copypolist.iordertype,copypolist.csoordercode,copypolist.iorderseq,copypolist.cdemandmemo,copypolist.iexpiratdatecalcu,copypolist.cbmemo
 from copypolist   
 INNER JOIN Inventory  ON copypolist.cinvcode = Inventory.cInvCode
 WHERE bposourcearr=1 and copypolist.bservice <> 1 and copypolist.binvtype<>1 and isnull(ireceivedqty,0)=0 and ((isnull(iquantity,0)>isnull(iarrqty,0) or CONVERT(DECIMAL(38,4),isnull(iquantity,0)*(1+isnull(copypolist.fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrqty,0))) or ((isnull(inum,0)>isnull(iarrnum,0) or CONVERT(DECIMAL(38,4),isnull(inum,0)*(1+isnull(copypolist.fInExcess,0)))>CONVERT(DECIMAL(38,4),isnull(iarrnum,0)))) and copypolist.iGroupType = 2) and isnull(cbustype,'')<>N'直运采购' and ((isnull(cVerifier,'')<>'' and isnull(cchanger,'')='') or (isnull(cchangverifier,'')<>''))  AND isnull(cbustype,'')<>N'直运采购' AND isnull(cbustype,'')<>N'委外加工' AND iDiscountTaxType=0 and isnull(iflowid,0)= '0'
 AND copypolist.cordercode =N'{0}'", cOrderCode);
            try
            {
                DataTable dtDetails = DBHelperSQL.QueryTable(connectionString, strSql);
                dtDetails.TableName = "dtDetails";
                ds.Tables.Add(dtDetails);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }

            return ds;
        }

        /// <summary>
        /// 保存采购到单
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="arrivalVouch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool PU_ArrivalVouch_Save(User user, ArrivalVouch arrivalVouch, out string errMsg)
        {
            bool flag = false;
            errMsg = string.Empty;
            SqlConnection conn = new SqlConnection(user.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return flag;
            }
            cmd.Connection = conn;
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;

            string strSql = string.Empty;
            DataTable dt = null;
            int vt_id;
            string cardNumber;
            int result;
            int rowno = 1;//单据行号
            string cCode = string.Empty;//到货单号
            string autoIdAll = string.Empty;//记录所有autoid，后面存储过程会用到    形式：1,2,3,4
            try
            {
                int id, autoid;
                DateTime now = DateTime.Now;
                string cVouchName = "到货单";

                dt = new DataTable();//模板号及
                cmd.CommandText = string.Format("select def_id,cardnumber from Vouchers where ccardname='{0}'", cVouchName);
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                vt_id = Cast.ToInteger(dt.Rows[0]["def_id"]);
                cardNumber = Cast.ToString(dt.Rows[0]["cardnumber"]);

                dt = new DataTable();//ID,AutoID
                cmd.CommandText = string.Format("select ifatherid,ichildid from UFSystem..UA_Identity where cvouchtype='PUARRIVAL' and cAcc_id='{0}'", user.Accid);
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                id = Cast.ToInteger(dt.Rows[0]["ifatherid"]);//表头ID
                autoid = Cast.ToInteger(dt.Rows[0]["ichildid"]);//表体autoid

                cmd.CommandText = "insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,-1*iNum,-1*iQuantity,0,0,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId =0";
                cmd.ExecuteNonQuery();

                //插入到货主表
                id++;
                strSql = string.Format(@"insert into PU_ArrivalVouch(ivtid,id,ccode,ddate,cvencode,cptcode,cdepcode,cpersoncode,cpaycode,csccode,cexch_name,iexchrate,itaxrate,cmemo,cbustype,cmaker,cdefine1,cdefine2,cdefine3,cdefine4,cdefine5,cdefine6,cdefine7,cdefine8,cdefine9,cdefine10,cdefine11,cdefine12,cdefine13,cdefine14,cdefine15,cdefine16,ibilltype,cvouchtype,ireturncount,iswfcontrolled,iflowid,cpocode)
values(@ivtid,@Id,@ccode,@ddate,@cvencode,@cptcode,@cdepcode,@cpersoncode,@cpaycode,@csccode,@cexch_name,@iexchrate,@itaxrate,@cmemo,@cbustype,@cmaker,@cdefine1,@cdefine2,@cdefine3,@cdefine4,@cdefine5,@cdefine6,@cdefine7,@cdefine8,@cdefine9,@cdefine10,@cdefine11,@cdefine12,@cdefine13,@cdefine14,@cdefine15,@cdefine16,@ibilltype,@cvouchtype,@ireturncount,@iswfcontrolled,@iflowid,@cpocode)");

                SqlParameter[] parms =
                {
                    new SqlParameter("@ivtid",SqlDbType.Int),//单据模版号 
                    new SqlParameter("@id",SqlDbType.Int),//采购到货单主表标识
                    new SqlParameter("@ccode",SqlDbType.NVarChar,30),//采购到货单号 
                    new SqlParameter("@ddate",SqlDbType.DateTime),//单据日期
                    new SqlParameter("@cvencode",SqlDbType.NVarChar,20),//供应商编码 
                    new SqlParameter("@cptcode",SqlDbType.NVarChar,2),//采购类型编码 
                    new SqlParameter("@cdepcode",SqlDbType.NVarChar,20),//部门编码 
                    new SqlParameter("@cpersoncode",SqlDbType.NVarChar,20),//业务员编码
                    new SqlParameter("@cpaycode",SqlDbType.NVarChar,3),//付款条件编码
                    new SqlParameter("@csccode",SqlDbType.NVarChar,2),//发运方式编码 
                    new SqlParameter("@cexch_name",SqlDbType.NVarChar,8),//币种名称
                    new SqlParameter("@iexchrate",SqlDbType.Float),//汇率 
                    new SqlParameter("@itaxrate",SqlDbType.Decimal),//表头税率 
                    new SqlParameter("@cmemo",SqlDbType.NVarChar,60),//备注 
                    new SqlParameter("@cbustype",SqlDbType.NVarChar,8),//业务类型 
                    new SqlParameter("@cmaker",SqlDbType.NVarChar,20),//制单人
                    new SqlParameter("@cdefine1",SqlDbType.NVarChar,20),//自定义项1 
                    new SqlParameter("@cdefine2",SqlDbType.NVarChar,20),//自定义项2 
                    new SqlParameter("@cdefine3",SqlDbType.NVarChar,20),//自定义项3 
                    new SqlParameter("@cdefine4",SqlDbType.DateTime),//自定义项4 
                    new SqlParameter("@cdefine5",SqlDbType.Int),//自定义项5 
                    new SqlParameter("@cdefine6",SqlDbType.DateTime),//自定义项5 
                    new SqlParameter("@cdefine7",SqlDbType.Float),//自定义项5 
                    new SqlParameter("@cdefine8",SqlDbType.NVarChar,4),//自定义项5 
                    new SqlParameter("@cdefine9",SqlDbType.NVarChar,8),
                    new SqlParameter("@cdefine10",SqlDbType.NVarChar,60),
                    new SqlParameter("@cdefine11",SqlDbType.NVarChar,120),
                    new SqlParameter("@cdefine12",SqlDbType.NVarChar,120),
                    new SqlParameter("@cdefine13",SqlDbType.NVarChar,120),
                    new SqlParameter("@cdefine14",SqlDbType.NVarChar,120),
                    new SqlParameter("@cdefine15",SqlDbType.Int),
                    new SqlParameter("@cdefine16",SqlDbType.Float),

                    new SqlParameter("@ibilltype",SqlDbType.TinyInt),//单据类型 
                    new SqlParameter("@cvouchtype",SqlDbType.NVarChar,10),//单据类型 
                    new SqlParameter("@ireturncount",SqlDbType.Int),//打回次数 
                    new SqlParameter("@iswfcontrolled",SqlDbType.Bit),//是否工作流控制 
                    new SqlParameter("@iflowid",SqlDbType.Int),//流程ID
                    new SqlParameter("@cpocode",SqlDbType.NVarChar,64)//采购订单编号
                };

                parms[0].Value = vt_id;
                parms[1].Value = 860000000 + id;
                parms[2].Value = id;//到货单号后面修改
                parms[3].Value = Cast.ToDateTime(now.ToShortDateString());//单据日期
                parms[4].Value = arrivalVouch.cVenCode;
                parms[5].Value = string.IsNullOrEmpty(arrivalVouch.cPTCode) ? DBNull.Value : (object)arrivalVouch.cPTCode;
                parms[6].Value = string.IsNullOrEmpty(arrivalVouch.cDepCode) ? DBNull.Value : (object)arrivalVouch.cDepCode;
                parms[7].Value = string.IsNullOrEmpty(arrivalVouch.cPersonCode) ? DBNull.Value : (object)arrivalVouch.cPersonCode;
                parms[8].Value = string.IsNullOrEmpty(arrivalVouch.cPayCode) ? DBNull.Value : (object)arrivalVouch.cPayCode;
                parms[9].Value = string.IsNullOrEmpty(arrivalVouch.cSCCode) ? DBNull.Value : (object)arrivalVouch.cSCCode;
                parms[10].Value = arrivalVouch.cExch_Name;
                parms[11].Value = arrivalVouch.iExchRate;
                parms[12].Value = arrivalVouch.iTaxRate;
                parms[13].Value = arrivalVouch.cMemo;
                parms[14].Value = arrivalVouch.cBusType;
                parms[15].Value = arrivalVouch.cMaker;
                parms[16].Value = arrivalVouch.Define1;
                parms[17].Value = arrivalVouch.Define2;
                parms[18].Value = arrivalVouch.Define3;
                parms[19].Value = arrivalVouch.Define4 == DateTime.MinValue ? DBNull.Value : (object)arrivalVouch.Define4;
                parms[20].Value = arrivalVouch.Define5;
                parms[21].Value = arrivalVouch.Define6 == DateTime.MinValue ? DBNull.Value : (object)arrivalVouch.Define6;
                parms[22].Value = arrivalVouch.Define7;
                parms[23].Value = arrivalVouch.Define8;
                parms[24].Value = arrivalVouch.Define9;
                parms[25].Value = arrivalVouch.Define10;
                parms[26].Value = arrivalVouch.Define11;
                parms[27].Value = arrivalVouch.Define12;
                parms[28].Value = arrivalVouch.Define13;
                parms[29].Value = arrivalVouch.Define14;
                parms[30].Value = arrivalVouch.Define15;
                parms[31].Value = arrivalVouch.Define16;
                parms[32].Value = 0; //arrivalVouch.Define17;
                parms[33].Value = 0;
                parms[34].Value = 0;
                parms[35].Value = 0;
                parms[36].Value = 0;
                parms[37].Value = arrivalVouch.cOrderCode;

                cmd.CommandText = strSql;
                cmd.Parameters.AddRange(parms);
                result = cmd.ExecuteNonQuery();
                if (result < 1)
                {
                    errMsg = "采购到货主表插入失败！";
                    tran.Rollback();
                    return flag;
                }

                //清空参数
                cmd.Parameters.Clear();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"Create table TmpPU_A_ArrivalVouchs_Detail
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
                cmd.ExecuteNonQuery();

                //循环插入子表
                foreach (ArrivalVouchs avs in arrivalVouch.OperateDetails)
                {
                    autoid++;
                    //记录autoid
                    autoIdAll += string.Format("{0},", 860000000 + autoid);

                    strSql = string.Format(@"Insert Into PU_ArrivalVouchs(autoid,id,cwhcode,cinvcode,inum,iquantity,ioricost,ioritaxcost,iorimoney,ioritaxprice,iorisum,icost,imoney,itaxprice,isum,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10,itaxrate,cdefine22,cdefine23,cdefine24,cdefine25,cdefine26,cdefine27,cdefine28,cdefine29,cdefine30,cdefine31,cdefine32,cdefine33,cdefine34,cdefine35,cdefine36,cdefine37,citem_class,citemcode,iposid,citemname,frealquantity,fValidQuantity,fvalidInQuan,finvalidquantity,icorid,bgsp,cbatch,dvdate,dpdate,frefusequantity,cgspstate,btaxcost,ippartid,ipquantity,sotype,contractrowguid,imassdate,cmassunit,iinvexchrate,csocode,isorowno,iorderid,cordercode,iorderrowno,contractcode,contractrowno,iexpiratdatecalcu,cexpirationdate,dexpirationdate,iorderdid,iordertype,iorderseq,ivouchrowno,RejectSource)
values(@Autoid,@Id,@cwhcode,@cinvcode,@inum,@iquantity,@ioricost,@ioritaxcost,@ioriMoney,@ioritaxprice,@iorisum,@icost,@imoney,@itaxprice,@isum,@cfree1,@cfree2,@cfree3,@cfree4,@cfree5,@cfree6,@cfree7,@cfree8,@cfree9,@cfree10,@itaxrate,@cdefine22,@cdefine23,@cdefine24,@cdefine25,@cdefine26,@cdefine27,@cdefine28,@cdefine29,@cdefine30,@cdefine31,@cdefine32,@cdefine33,@cdefine34,@cdefine35,@cdefine36,@cdefine37,@citem_class,@citemcode,@iposid,@citemname,@frealquantity,@fValidQuantity,@fvalidInQuan,@finvalidquantity,@icorid,@bgsp,@cbatch,@dvdate,@dpdate,@frefusequantity,@cgspstate,@btaxcost,@ippartid,@ipquantity,@sotype,@contractrowguid,@imassdate,@cmassunit,@iinvexchrate,@csocode,@isorowno,@iorderid,@cordercode,@iorderrowno,@contractcode,@contractrowno,@iexpiratdatecalcu,@cexpirationdate,@dexpirationdate,@iorderdid,@iordertype,@iorderseq,@ivouchrowno,@RejectSource)");
                    parms = new SqlParameter[]
                    {
                        new SqlParameter("@Autoid",SqlDbType.Int),//采购到货单子表标识 
                        new SqlParameter("@id",SqlDbType.Int),//采购到货单主表标识 
                        new SqlParameter("@cwhcode",SqlDbType.NVarChar,10),//仓库编码
                        new SqlParameter("@cinvcode",SqlDbType.NVarChar,20),//存货编码 
                        new SqlParameter("@inum",SqlDbType.Decimal),//辅计量数量 
                        new SqlParameter("@iquantity",SqlDbType.Decimal),//数量 
                        new SqlParameter("@ioricost",SqlDbType.Decimal),//原币无税单价
                        new SqlParameter("@ioritaxcost",SqlDbType.Decimal),//原币含税单价 
                        new SqlParameter("@ioriMoney",SqlDbType.Money),//原币无税金额
                        new SqlParameter("@ioritaxprice",SqlDbType.Money),//原币税额
                        new SqlParameter("@iorisum",SqlDbType.Money),//原币价税合计 
                        new SqlParameter("@icost",SqlDbType.Decimal),//本币无税单价 
                        new SqlParameter("@imoney",SqlDbType.Money),//本币无税金额 
                        new SqlParameter("@itaxprice",SqlDbType.Money),//本币税额
                        new SqlParameter("@isum",SqlDbType.Money),//本币价税合计 
                        new SqlParameter("@cfree1",SqlDbType.NVarChar,20),//存货自由项1 
                        new SqlParameter("@cfree2",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree3",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree4",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree5",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree6",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree7",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree8",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree9",SqlDbType.NVarChar,20),
                        new SqlParameter("@cfree10",SqlDbType.NVarChar,20),
                        new SqlParameter("@itaxrate",SqlDbType.Decimal),//税率
                        new SqlParameter("@cdefine22",SqlDbType.NVarChar,60),//表体自定义项22 
                        new SqlParameter("@cdefine23",SqlDbType.NVarChar,60),//表体自定义项23 
                        new SqlParameter("@cdefine24",SqlDbType.NVarChar,60),//表体自定义项24 
                        new SqlParameter("@cdefine25",SqlDbType.NVarChar,60),//表体自定义项25
                        new SqlParameter("@cdefine26",SqlDbType.Float),//表体自定义项26 
                        new SqlParameter("@cdefine27",SqlDbType.Float),//表体自定义项27
                        new SqlParameter("@cdefine28",SqlDbType.NVarChar,120),//表体自定义项28 
                        new SqlParameter("@cdefine29",SqlDbType.NVarChar,120),//表体自定义项29 
                        new SqlParameter("@cdefine30",SqlDbType.NVarChar,120),//表体自定义项30 
                        new SqlParameter("@cdefine31",SqlDbType.NVarChar,120),//表体自定义项31 
                        new SqlParameter("@cdefine32",SqlDbType.NVarChar,120),//表体自定义项32 
                        new SqlParameter("@cdefine33",SqlDbType.NVarChar,120),//表体自定义项33 
                        new SqlParameter("@cdefine34",SqlDbType.Int),//表体自定义项34 
                        new SqlParameter("@cdefine35",SqlDbType.Int),//表体自定义项35
                        new SqlParameter("@cdefine36",SqlDbType.DateTime),//表体自定义项36
                        new SqlParameter("@cdefine37",SqlDbType.DateTime),//表体自定义项37 
                        new SqlParameter("@citem_class",SqlDbType.NVarChar,2),//项目大类编码
                        new SqlParameter("@citemcode",SqlDbType.NVarChar,20),//项目编码 
                        new SqlParameter("@iposid",SqlDbType.Int),//采购订单子表标识 
                        new SqlParameter("@citemname",SqlDbType.NVarChar,60),//项目名称 
                        new SqlParameter("@frealquantity",SqlDbType.Decimal),//实收数量 
                        new SqlParameter("@fValidQuantity",SqlDbType.Decimal),//合格数量 
                        new SqlParameter("@fvalidInQuan",SqlDbType.Decimal),//合格品入库数量
                        new SqlParameter("@finvalidquantity",SqlDbType.Decimal),//不合格数量 
                        new SqlParameter("@icorid",SqlDbType.Int),//到货退货子表标识
                        new SqlParameter("@bgsp",SqlDbType.TinyInt),//是否质检 
                        new SqlParameter("@cbatch",SqlDbType.NVarChar,60),//批号 
                        new SqlParameter("@dvdate",SqlDbType.DateTime),//失效日期 
                        new SqlParameter("@dpdate",SqlDbType.DateTime),//生产日期 
                        new SqlParameter("@frefusequantity",SqlDbType.Decimal),//拒收数量 
                        new SqlParameter("@cgspstate",SqlDbType.NVarChar,20),//质检状态
                        new SqlParameter("@btaxcost",SqlDbType.Bit),//价格标准 
                        new SqlParameter("@ippartid",SqlDbType.Int),//母件Id 
                        new SqlParameter("@ipquantity",SqlDbType.Decimal),//母件数量
                        new SqlParameter("@sotype",SqlDbType.TinyInt),//需求跟踪方式
                        new SqlParameter("@contractrowguid",SqlDbType.UniqueIdentifier),//合同子表表识
                        new SqlParameter("@imassdate",SqlDbType.Int),//保质期 
                        new SqlParameter("@cmassunit",SqlDbType.TinyInt),//保质期单位 
                        new SqlParameter("@iinvexchrate",SqlDbType.Decimal),//换算率
                        new SqlParameter("@csocode",SqlDbType.NVarChar,50),//需求跟踪号
                        new SqlParameter("@isorowno",SqlDbType.Int),//需求跟踪行号销售订单行号 
                        new SqlParameter("@iorderid",SqlDbType.Int),//订单ID 
                        new SqlParameter("@cordercode",SqlDbType.NVarChar,50),//订单编号 
                        new SqlParameter("@iorderrowno",SqlDbType.Int),//订单行号 
                        new SqlParameter("@contractcode",SqlDbType.NVarChar,128),//合同号 
                        new SqlParameter("@contractrowno",SqlDbType.NVarChar,128),//合同行号 
                        new SqlParameter("@iexpiratdatecalcu",SqlDbType.SmallInt),//有效期推断方式 
                        new SqlParameter("@cexpirationdate",SqlDbType.NVarChar,20),//有效期至 
                        new SqlParameter("@dexpirationdate",SqlDbType.DateTime),//有效期计算项                         
                        new SqlParameter("@iorderdid",SqlDbType.Int),//销售订单子表ID 
                        new SqlParameter("@iordertype",SqlDbType.TinyInt),//销售订单类型 
                        new SqlParameter("@iorderseq",SqlDbType.Int),//销售订单类型 
                        new SqlParameter("@ivouchrowno",SqlDbType.Int),//单据行号 
                        new SqlParameter("@RejectSource",SqlDbType.Bit)//拒收来源标示（有拒收为1）
                    };
                    parms[0].Value = 860000000 + autoid;
                    parms[1].Value = 860000000 + id;
                    parms[2].Value = avs.cWhCode;
                    parms[3].Value = avs.cInvCode;
                    parms[4].Value = avs.iNum;
                    parms[5].Value = avs.iScanQuantity;//扫描数量
                    parms[6].Value = avs.iunitprice;//原币无税单价
                    parms[7].Value = avs.iTaxPrice;//原币含税单价
                    parms[8].Value = avs.iMoney;//原币无税金额
                    parms[9].Value = avs.iTax;//原币税额
                    parms[10].Value = avs.iSum;//原币价税合计
                    parms[11].Value = avs.iunitprice;//本币无税单价
                    parms[12].Value = avs.iMoney;//本币无税金额
                    parms[13].Value = avs.iTax;//本币税额
                    parms[14].Value = avs.iSum;//本币价税合计

                    parms[15].Value = avs.Free1;
                    parms[16].Value = avs.Free2;
                    parms[17].Value = avs.Free3;
                    parms[18].Value = avs.Free4;
                    parms[19].Value = avs.Free5;
                    parms[20].Value = avs.Free6;
                    parms[21].Value = avs.Free7;
                    parms[22].Value = avs.Free8;
                    parms[23].Value = avs.Free9;
                    parms[24].Value = avs.Free10;

                    parms[25].Value = avs.iTaxRate;//税率

                    parms[26].Value = avs.Define22;
                    parms[27].Value = avs.Define23;
                    parms[28].Value = avs.Define24;
                    parms[29].Value = avs.Define25;
                    parms[30].Value = avs.Define26;
                    parms[31].Value = avs.Define27;
                    parms[32].Value = avs.Define28;
                    parms[33].Value = avs.Define29;
                    parms[34].Value = avs.Define30;
                    parms[35].Value = avs.Define31;
                    parms[36].Value = avs.Define32;
                    parms[37].Value = avs.Define33;
                    parms[38].Value = avs.Define34;
                    parms[39].Value = avs.Define35;
                    parms[40].Value = avs.Define36 == DateTime.MinValue ? DBNull.Value : (object)avs.Define36;
                    parms[41].Value = avs.Define37 == DateTime.MinValue ? DBNull.Value : (object)avs.Define37;

                    parms[42].Value = avs.cItem_class;
                    parms[43].Value = avs.cItemCode;
                    parms[44].Value = avs.Autoid; //avs.iPOsID;//采购订单子表ID
                    parms[45].Value = avs.cItemName;
                    parms[46].Value = avs.fRealQuantity;//实收数量
                    parms[47].Value = avs.fValidQuantity;//合格数量
                    parms[48].Value = DBNull.Value;//avs.fValidInQuan;//合格入庫數量
                    parms[49].Value = avs.RejectSource ? (object)avs.finValidQuantity : DBNull.Value;//不合格數量(如果有拒收则为0,没有拒收为NULL)
                    parms[50].Value = DBNull.Value; //avs.iCorId;//
                    parms[51].Value = avs.bGsp;//
                    parms[52].Value = avs.cBatch;
                    parms[53].Value = avs.dVDate;
                    parms[54].Value = avs.dPDate;
                    parms[55].Value = avs.RejectSource ? (object)avs.fRefuseQuantity : DBNull.Value;//拒收数量
                    parms[56].Value = DBNull.Value; //avs.cGspState;
                    parms[57].Value = avs.bTaxCost;
                    parms[58].Value = DBNull.Value; // avs.iPPartId;
                    parms[59].Value = DBNull.Value;
                    parms[60].Value = avs.SoType;
                    parms[61].Value = string.IsNullOrEmpty(avs.ContractRowGUID) ? DBNull.Value : (object)avs.ContractRowGUID;
                    parms[62].Value = avs.iMassDate;
                    parms[63].Value = avs.cMassUnit;
                    parms[64].Value = avs.iInvexchRate;
                    parms[65].Value = avs.cSoCode;
                    parms[66].Value = DBNull.Value;//avs.iSoRowNo;
                    parms[67].Value = DBNull.Value;//avs.iOrderId;//
                    parms[68].Value = avs.cOrderCode;//订单编号
                    parms[69].Value = avs.iOrderRowNo;//订单行号
                    parms[70].Value = avs.ContractCode;
                    parms[71].Value = avs.ContractRowNo;
                    parms[72].Value = avs.iExpiratDateCalcu;
                    parms[73].Value = avs.cExpirationDate;
                    parms[74].Value = avs.dExpirationDate == DateTime.MinValue ? DBNull.Value : (object)avs.dExpirationDate;
                    parms[75].Value = DBNull.Value;//avs.iOrderdId;
                    parms[76].Value = DBNull.Value; //avs.iOrderType;
                    parms[77].Value = avs.iOrderSeq;
                    parms[78].Value = rowno++;//行号
                    parms[79].Value = avs.RejectSource;//拒收来源

                    cmd.CommandText = strSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parms);
                    result = cmd.ExecuteNonQuery();
                    if (result < 1)
                    {
                        errMsg = "采购到货单子表插入失败！";
                        tran.Rollback();
                        return flag;
                    }

                    //更新采购订单子表中的到货数量及其它数量
                    strSql = string.Format(@"update po_podetails set iArrQTY=isnull(iArrQTY,0)+@iArrQTY,iArrNum=isnull(iArrNum,0)+@iArrNum,iArrMoney=isnull(iArrMoney,0)+@iArrMoney,iNatArrMoney=isnull(iNatArrMoney,0)+@iNatArrMoney
                    ,fPoValidQuantity=isnull(fPoValidQuantity,0)+@fPoValidQuantity,fPoValidNum=isnull(fPoValidNum,0)+@fPoValidNum,fPoArrQuantity=isnull(fPoArrQuantity,0)+@fPoArrQuantity,fPoArrNum=isnull(fPoArrNum,0)+@fPoArrNum
                    ,fPoRetQuantity=isnull(fPoRetQuantity,0)+@fPoRetQuantity,fPoRetNum=isnull(fPoRetNum,0)+@fPoRetNum,fPoRefuseQuantity=isnull(fPoRefuseQuantity,0)+@fPoRefuseQuantity,fPoRefuseNum=isnull(iArrQTY,0)+@fPoRefuseNum 
                    where poid=@poid and cInvCode= @cInvCode and ID=@id ");
                    parms = new SqlParameter[]
                    {
                        new SqlParameter("@id",SqlDbType.Int),//订单子表ID
                        new SqlParameter("@poid",SqlDbType.Int),//订单主表ID
                        new SqlParameter("@iArrQTY",SqlDbType.Decimal),//到货数量
                        new SqlParameter("@iArrNum",SqlDbType.Decimal),//到货件数
                        new SqlParameter("@iArrMoney",SqlDbType.Money),//到货金额
                        new SqlParameter("@iNatArrMoney",SqlDbType.Money),//本币到货金额 
                        new SqlParameter("@fPoValidQuantity",SqlDbType.Decimal),//合格数量 
                        new SqlParameter("@fPoValidNum",SqlDbType.Decimal),//合格件数 
                        new SqlParameter("@fPoArrQuantity",SqlDbType.Decimal),//到货数量 
                        new SqlParameter("@fPoArrNum",SqlDbType.Decimal),//到货件数
                        new SqlParameter("@fPoRetQuantity",SqlDbType.Decimal),//退货数量 
                        new SqlParameter("@fPoRetNum",SqlDbType.Decimal),//退货件数 
                        new SqlParameter("@fPoRefuseQuantity",SqlDbType.Decimal),//拒收数量  
                        new SqlParameter("@fPoRefuseNum",SqlDbType.Decimal),//拒收件数                    
                        new SqlParameter("@cInvCode",SqlDbType.NVarChar,30)//存货名称
                    };
                    parms[0].Value = avs.Autoid;
                    parms[1].Value = avs.ID;
                    parms[2].Value = avs.iScanQuantity;
                    parms[3].Value = 0;
                    parms[4].Value = avs.iSum;
                    parms[5].Value = avs.iSum;
                    parms[6].Value = avs.fValidQuantity;
                    parms[7].Value = 0;
                    parms[8].Value = avs.iScanQuantity;
                    parms[9].Value = 0;
                    parms[10].Value = avs.fRetQuantity;
                    parms[11].Value = avs.fRetNum;
                    parms[12].Value = avs.fRefuseQuantity;
                    parms[13].Value = avs.fRefuseNum;
                    parms[14].Value = avs.cInvCode;

                    cmd.CommandText = strSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parms);
                    result = cmd.ExecuteNonQuery();
                    if (result < 1)
                    {
                        errMsg = "修改采购订单子表出错";
                        tran.Rollback();
                        return flag;
                    }

                    //处理临时表
                    strSql = string.Format(@"insert into TmpPU_A_ArrivalVouchs_Detail(iSourceautoid,iArrqty,iArrnum,iArrmoney,iArrnatmoney,fPoValidQuantity,fPoValidNum,fPoArrQuantity,fPoArrNum,fPoRetQuantity,fPoRetNum,fPoRefuseQuantity,fPoRefuseNum,sotype)
values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}')", avs.Autoid, avs.iScanQuantity, 0, avs.iMoney, avs.iMoney, avs.fValidQuantity, 0, avs.iScanQuantity, 0, avs.fRetQuantity, avs.fRetNum, avs.fRefuseQuantity, avs.fRefuseNum, "po");

                    cmd.CommandText = strSql;
                    cmd.Parameters.Clear();//清空参数
                    result = cmd.ExecuteNonQuery();
                    if (result < 1)
                    {
                        errMsg = "修改采购订单子表出错";
                        tran.Rollback();
                        return flag;
                    }
                }//子表循环结束


                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select isourceautoid,sum(T.iArrqty) as iArrqty,sum(T.iArrnum) as iArrnum,sum(T.iArrmoney) as iArrmoney,sum(T.iArrnatmoney) as iArrnatmoney,sum(T.fPoValidQuantity) as fPoValidQuantity,sum(T.fPoValidNum) as fPoValidNum,sum(T.fPoArrQuantity) as fPoArrQuantity,sum(T.fPoArrNum) as fPoArrNum,sum(T.fPoRetQuantity) as fPoRetQuantity,sum(T.fPoRetNum) as fPoRetNum,sum(T.fPoRefuseQuantity) as fPoRefuseQuantity,sum(T.fPoRefuseNum) as fPoRefuseNum,sotype,min(ufts) as ufts into TmpPU_B_ArrivalVouchs_Detail from TmpPU_A_ArrivalVouchs_Detail as T group by isourceautoid,sotype ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'TmpPU_B_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'[TmpPU_B_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_B_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'TmpPU_A_ArrivalVouchs_Detail' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"If Exists(select Name from sysobjects where name=N'[TmpPU_A_ArrivalVouchs_Detail]' and xtype=N'U') Drop table TmpPU_A_ArrivalVouchs_Detail";
                cmd.ExecuteNonQuery();

                cmd.CommandText = String.Format("insert into SCM_EntryLedgerBuffer (Subject,iNum,iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate) select Subject,1*iNum,1*iQuantity,iBillQuantity,iBillNum,cWhCode,cInvCode,ItemId,cVMIVenCode,cBatch,DocumentType,DocumentId,DocumentDId,DemandPlanType,DemandPlanDId,cFree1,cFree2,cFree3,cFree4,cFree5,cFree6,cFree7,cFree8,cFree9,cFree10,dMDate,dVDate,imassunit,imassday,cBusType,cSource,ddate,iexpiratdatecalcu,cexpirationdate,dexpirationdate from pu_v_preparestockbyarrnocheck where ISNULL(cWhCode,N'')<>N'' AND DocumentId = {0}", 860000000 + id);
                cmd.ExecuteNonQuery();

                strSql = "select @@spid";
                cmd.CommandText = strSql;
                string spid = cmd.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(spid))
                {
                    tran.Rollback();
                    return flag;
                }

                strSql = String.Format(@"insert into SCM_Item(cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10)
 select distinct cInvCode,cfree1,cfree2,cfree3,cfree4,cfree5,cfree6,cfree7,cfree8,cfree9,cfree10  from SCM_EntryLedgerBuffer a with (nolock) 
 where a.transactionid=N'spid_{0}' and not exists (select 1 from SCM_Item Item where Item.cInvCode=a.cInvCode and Item.cfree1=a.cfree1 
 and Item.cfree2=a.cfree2 and Item.cfree3=a.cfree3 and Item.cfree4=a.cfree4 and Item.cfree5=a.cfree5 
 and Item.cfree6=a.cfree6 and Item.cfree7=a.cfree7 and Item.cfree8=a.cfree8 and Item.cfree9=a.cfree9 and Item.cfree10=a.cfree10  )", spid);
                cmd.CommandText = strSql;
                result = cmd.ExecuteNonQuery();
                strSql = string.Format("exec Usp_SCM_CommitGeneralLedgerWithCheck N'PU',1,1,0,1,0,0,1,0,1,0,0,0,1 ,0,'spid_{0}'", spid);
                cmd.CommandText = strSql;
                result = cmd.ExecuteNonQuery();

                //单据号处理
                string cSeed = DateTime.Now.ToString("yyMM");
                strSql = string.Format("select cNumber as Maxnumber From VoucherHistory  with (NOLOCK) Where  CardNumber='{0}' and cContent='单据日期' and cSeed='{1}'", cardNumber, cSeed);
                cmd.CommandText = strSql;
                object ocode = cmd.ExecuteScalar();
                int icode;
                if (ocode == null || ocode.Equals(DBNull.Value))
                {
                    icode = 1;
                    strSql = string.Format("Insert into VoucherHistory(CardNumber,cContent,cContentRule,cSeed,cNumber) values('{0}','单据日期','月','{1}','1')", cardNumber, cSeed);
                }
                else
                {
                    icode = Convert.ToInt32(ocode) + 1;
                    strSql = string.Format("update VoucherHistory set cNumber='{0}' Where  CardNumber='{1}' and cContent='单据日期' and cSeed='{2}'", icode, cardNumber, cSeed);
                }
                cmd.CommandText = strSql;
                if (cmd.ExecuteNonQuery() != 1)
                {
                    tran.Rollback();
                    errMsg = "update voucherhistory error!";
                    return flag;
                }
                //采购到货的流水号为当月的4位
                cSeed = DateTime.Now.ToString("yyyyMM");
                cCode = cSeed + icode.ToString().PadLeft(4, '0');

                //判断到货单号是否重复
                strSql = string.Format("select ccode from pu_arrivalvouch where ccode = '{0}'  and  isnull(cvouchtype,'')='' and id<>'{1}'", cCode, 860000000 + id);
                cmd.CommandText = strSql;
                if (cmd.ExecuteScalar() != null)
                {
                    errMsg = string.Format("单据号重复,请稍后再次提交：{0}", cCode);
                    tran.Rollback();
                    return flag;
                }

                //更新到货单号
                cmd.CommandText = string.Format("Update pu_arrivalvouch Set cCode='{0}' where id={1}", cCode, 860000000 + id);//"Update pu_arrivalvouch Set cCode = " + SelSql(cSeed) + " Where Id = " + id.ToString();
                result = cmd.ExecuteNonQuery();

                //删除autoIdAll的最后一个,然后执行存储过程
                autoIdAll = autoIdAll.Remove(autoIdAll.Length - 1);
                strSql = string.Format("exec Scm_SaveBatchProperty '{0}','autoid','pu_arrivalvouchs','{1}'", autoIdAll, user.UserName);
                cmd.CommandText = strSql;
                cmd.ExecuteNonQuery();

                strSql = string.Format("update UFSystem..UA_Identity set ifatherid={0},ichildid={1}  where cvouchtype='PUARRIVAL' and cAcc_id='{2}' ", id, autoid, user.Accid);
                cmd.CommandText = strSql;
                result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    errMsg = "UA_Identity table update error!";
                    tran.Rollback();
                    return flag;
                }

                //操作成功 
                flag = true;
                tran.Commit();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}
