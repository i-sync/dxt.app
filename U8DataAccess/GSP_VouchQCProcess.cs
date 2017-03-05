using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace U8DataAccess
{
     //质检单处理()
    public class GSP_VouchQCProcess
    {
        //药品入库质量验收记录单y主表
        public static List<GSP_Vouchqc> GetGSPvouchqc(string qcid, string connstr, out DataSet ds)
        {
            LogNote ln;
            ds = new DataSet();

            string sqlStr = @"select v.ID 主表标识,v.QCID 质量验收记录单号,v.ICODE 采购到货退货单主表标识,
v.CCODE 采购到货退货单号,v.DARVDATE 到货退货日期,v.CVERIFIER 审核人,
v.CMAKER 制单人,v.DDATE 单据日期,v.CVOUCHTYPE 单据类型编码,
v.IVTID 单据模版号,v.UFTS 时间戳,v.CDEFINE1,v.BREFER 是否参照,
v.IVERIFYSTATE 审批标志 from dbo.GSP_VOUCHQC v
where v.QCID ='" + qcid + "'";

            ds = SqlHelper.ExecuteDataSet(connstr, CommandType.Text, sqlStr, null);

            ln = new LogNote(AppDomain.CurrentDomain.BaseDirectory + "barcode.log");

            ln.Write("gsp_vouchqc query:", qcid);

            return bTable(ds.Tables[0]);
        }
        
        //保存质量验收记录单主表信息
        public static bool SaveGSPvouchqc(List<GSP_Vouchqc> list)
        {
            return true;
        }

        private static List<GSP_Vouchqc> bTable(DataTable dt)
        {
            GSP_Vouchqc gspvqc;
            List<GSP_Vouchqc> list = new List<GSP_Vouchqc>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    gspvqc = new GSP_Vouchqc();
                    /*
        select v.ID 主表标识,v.QCID 质量验收记录单号,v.ICODE 采购到货退货单主表标识,
v.CCODE 采购到货退货单号,v.DARVDATE 到货退货日期,v.CVERIFIER 审核人,
v.CMAKER 制单人,v.DDATE 单据日期,v.CVOUCHTYPE 单据类型编码,
v.IVTID 单据模版号,v.UFTS 时间戳,v.CDEFINE1,v.BREFER 是否参照,
v.IVERIFYSTATE 审批标志 from dbo.GSP_VOUCHQC v
where v.QCID ='113201001030002'
         */
                    gspvqc.ID = int.Parse(dr["主表标识"].ToString());
                    gspvqc.QCID = dr["质量验收记录单号"].ToString();
                    gspvqc.ICODE = int.Parse(dr["采购到货退货单主表标识"].ToString());
                    gspvqc.CCODE = dr["采购到货退货单号"].ToString();
                    gspvqc.DARVDATE = DateTime.Parse(dr["到货退货日期"].ToString());
                    gspvqc.CVERIFIER = dr["审核人"].ToString();
                    gspvqc.CMAKER = dr["制单人"].ToString();
                    gspvqc.DDATE = DateTime.Parse(dr["单据日期"].ToString());
                    gspvqc.CVOUCHTYPE = dr["单据类型编码"].ToString();
                    gspvqc.IVTID = int.Parse(dr["单据模版号"].ToString());
                    gspvqc.CDEFINE1 = dr["CDEFINE1"].ToString();
                    gspvqc.BREFER = dr["是否参照"].ToString() == "1" ? true : false;
                    gspvqc.IVERIFYSTATE = dr["审批标志"].ToString() == "True" ? 1 : 0;

                    list.Add(gspvqc);
                }
            }
            return list;
        }

       

        /// <summary>
        /// 质量复核记录单子表信息
        /// </summary>
        /// <returns></returns>
        public static List<GSP_Vouchsqc> GetGSPVouchsqc(string cvencode, string connstr, out DataSet ds)
        {
            LogNote ln;
            ds = new DataSet();

            string sqlStr = @"select g.AUTOID 子表ID,g.ID 主表标识,v.QCID 质量验收记录单号, g.CINVCODE 药品编码,
g.FQUANTITY 实收数,g.FARVQUANTITY 到货数,g.DPRODATE 生产日期,
g.CVALDATE 有效期,vd.cVenName 供应商名称,g.DDATE_T 退货日期,
g.COUTINSTANCE 外观质量情况,g.CCONCLUSION 验收结论,
g.FELGQUANTITY 合格数,g.FNELGQUANTITY 不合格数,
g.CBACKREASON 拒收理由,g.CBATCH 生产批号,g.FPRICE 单价,
g.CDEFINE22,g.ICODE_T 采购到货退货单号,g.BCHECK 是否抽检,
g.imassDate 保质期,g.cMassUnit 保质期单位 
from dbo.GSP_VOUCHSQC g left join dbo.GSP_VOUCHQC v
on g.ID=v.ID left join dbo.Vendor vd
on g.CVENCODE = vd.cVenCode
where g.CINVCODE ='" + cvencode + "'";

            ds = SqlHelper.ExecuteDataSet(connstr, CommandType.Text, sqlStr, null);

            ln = new LogNote(AppDomain.CurrentDomain.BaseDirectory + "barcode.log");

            ln.Write("gsp_vouchsqc query:", cvencode);

            return sqcbTable(ds.Tables[0]);
        }

        //保存质量复核记录单子表信息
        public static bool SaveGSPVouchsqc(List<GSP_Vouchsqc> list)
        {

            return true;
        }

        private static List<GSP_Vouchsqc> sqcbTable(DataTable dt)
        {
            GSP_Vouchsqc gspvsqc;
            List<GSP_Vouchsqc> list = new List<GSP_Vouchsqc>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    gspvsqc = new GSP_Vouchsqc();
                    /*
        * select g.AUTOID 子表ID,g.ID 主表标识,v.QCID 质量验收记录单号, g.CINVCODE 药品编码,
g.FQUANTITY 实收数,g.FARVQUANTITY 到货数,g.DPRODATE 生产日期,
g.CVALDATE 有效期,vd.cVenName 供应商名称,g.DDATE_T 退货日期,
g.COUTINSTANCE 外观质量情况,g.CCONCLUSION 验收结论,
g.FELGQUANTITY 合格数,g.FNELGQUANTITY 不合格数,
g.CBACKREASON 拒收理由,g.CBATCH 生产批号,g.FPRICE 单价,
g.CDEFINE22,g.ICODE_T 采购到货退货单号,g.BCHECK 是否抽检,
g.imassDate 保质期,g.cMassUnit 保质期单位 
from dbo.GSP_VOUCHSQC g left join dbo.GSP_VOUCHQC v
on g.ID=v.ID left join dbo.Vendor vd
on g.CVENCODE = vd.cVenCode 
        */
                    gspvsqc.AUTOID = int.Parse(dr["子表ID"].ToString());
                    gspvsqc.ID = int.Parse(dr["主表标识"].ToString());
                    gspvsqc.QCID = dr["质量验收记录单号"].ToString();
                    gspvsqc.CINVCODE = dr["药品编码"].ToString();
                    gspvsqc.FQUANTITY = float.Parse(dr["实收数"].ToString());
                    gspvsqc.FARVQUANTITY = float.Parse(dr["到货数"].ToString());
                    gspvsqc.DPRODATE = DateTime.Parse(dr["生产日期"].ToString());
                    gspvsqc.CVALDATE = dr["有效期"].ToString();
                    gspvsqc.CVenName = dr["供应商名称"].ToString();
                    gspvsqc.DDATE_T = DateTime.Parse(dr["退货日期"].ToString());
                    gspvsqc.COUTINSTANCE = dr["外观质量情况"].ToString();
                    gspvsqc.CCONCLUSION = dr["验收结论"].ToString();
                    gspvsqc.FELGQUANTITY = float.Parse(dr["合格数"].ToString());
                    gspvsqc.FNELGQUANTITY = float.Parse(dr["不合格数"].ToString());
                    gspvsqc.CBACKREASON = dr["拒收理由"].ToString();
                    gspvsqc.CBATCH = dr["生产批号"].ToString();
                    gspvsqc.FPRICE = float.Parse(dr["单价"].ToString());
                    gspvsqc.CDEFINE22 = dr["CDEFINE22"].ToString();
                    gspvsqc.ICODE_T = dr["采购到货退货单号"].ToString();
                    gspvsqc.BCHECK = dr["是否抽检"].ToString() == "1" ? "是" : "否";
                    gspvsqc.ImassDate = int.Parse(dr["保质期"].ToString());
                    gspvsqc.CMassUnit = dr["保质期单位"].ToString();



                    list.Add(gspvsqc);
                }
            }
            return list;
        }

        
    }
}
