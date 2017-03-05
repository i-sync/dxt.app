using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using U8DataAccess;
using Model;
using System.Collections.Generic;

namespace DXTService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        //日志对象
        static log4net.ILog log = null;

        public Service()
        {
            //如果日志对象为空初始化对象
            if (log == null)
            {
                log = log4net.LogManager.GetLogger("Service.Logging");
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 自动升级

        /// <summary>
        /// 获取端服务版本信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description = "获取端服务版本信息")]
        public string GetVersion()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("update.xml"));
            XmlElement root = doc.DocumentElement;
            return root.SelectSingleNode("version").InnerText;
        }

        /// <summary>
        /// 获取最新的更新文档
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2013-08-04</remarks>
        [WebMethod(Description = "获取最新的配置文档")]
        public string GetNewDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("update.xml"));
            return doc.InnerXml;
        }

        /// <summary>
        /// 更新对应文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [WebMethod(Description = "更新对应文件")]
        public byte[] GetUpdateFile(string filePath)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + filePath;
                //判断文件是否存在
                if (!File.Exists(path))
                {
                    log.Error(string.Format("{0}文本不存在", filePath));
                    return null;
                }
                //读取文件
                log.Info(string.Format("读取文件：{0}", filePath));
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] array = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                //返回文件内容
                log.Info(string.Format("返回{0}文件内容!", filePath));
                return array;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}：下载文件失败", filePath), ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 根据用户名查询账套信息
        /// 此查询为登录前查询的主数据库，所以需要在这里拼装连接字符串
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "根据用户名查询账套信息")]
        public DataTable GetUAAcountInfo(string user_Id)
        {
            string sqlUser = System.Configuration.ConfigurationManager.AppSettings.Get("SqlUser");
            string sqlPassword = System.Configuration.ConfigurationManager.AppSettings.Get("SqlPassword");
            string dbService = System.Configuration.ConfigurationManager.AppSettings.Get("DBService");

            string connectionString = string.Format(@"user id={0};password={1};data source={2};persist security info=True;initial catalog=UFSystem;Connection Timeout=30", sqlUser, sqlPassword, dbService);

            return CommonDA.GetUAAccountInfo(connectionString, user_Id);
        }

        #region 初始数据
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="accID"></param>
        /// <param name="year"></param>
        /// <param name="customerName"></param>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "用户登录")]
        public int LoginIn(string userID, string password, string accID, string year, out string customerName, out string connectionString, out string errMsg)
        {
            CommonDA.info inf = new CommonDA.info();
            customerName = "";
            connectionString = "";
            errMsg = "";
            try
            {
                inf.subID = System.Configuration.ConfigurationManager.AppSettings.Get("SubID");

                inf.ERPService = System.Configuration.ConfigurationManager.AppSettings.Get("ERPService");
                inf.stockInCanChange = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("StockInCanChange"));
                inf.sqlUser = System.Configuration.ConfigurationManager.AppSettings.Get("SqlUser");
                inf.sqlPassword = System.Configuration.ConfigurationManager.AppSettings.Get("SqlPassword");
                inf.DBService = System.Configuration.ConfigurationManager.AppSettings.Get("DBService");
            }
            catch (Exception ex)
            {
                errMsg = ex.ToString();
                return -3;
            }
            int res = CommonDA.Login(userID, password, accID, year, inf, out customerName, out  connectionString, out  errMsg);
            return res;
        }

        /// <summary>
        /// 根据用户获取仓库管理权限获得基础数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="connectionString"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetInformation(string userID, string accID, string iYear, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = CommonDA.GetInformation(userID, accID, iYear, connectionString, out ds, out errMsg);
            return res;
        }

        /// <summary>
        /// 根据用户ID查询用户操作权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="accid">账套</param>
        /// <param name="year">年度</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="ds">返回结果</param>
        /// <param name="errMsg">错误内容</param>
        /// <returns></returns>
        [WebMethod(Description = "根据用户ID查询用户操作权限")]
        public int Competence(string cUser_Id, string cAcc_Id, string iYear, string connectionString, out DataSet ds, out string errMsg)
        {
            return CommonDA.Competence(cUser_Id, cAcc_Id, iYear, connectionString, out ds, out errMsg);
        }
        #endregion

        [WebMethod]
        public int getSTOutInvBatch(string cwhcode, string cinvcode, string cbatch, string connectionString, out DataSet InvBatch, out string errMsg)
        {
            return CommonDA.getSTOutInvBatch(cwhcode, cinvcode, cbatch, connectionString, out InvBatch, out errMsg);
        }

        #region 销售出库拣货
        /// <summary>
        /// 验证销售订单号
        /// </summary>
        /// <param name="csocode">销售订单号</param>
        /// <param name="connectionString"></param>
        /// <param name="list"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "验证销售订单号")]
        public int VerifySO_SO(string csocode, string connectionString, out DataSet list, out string errMsg)
        {
            return DispatchListProcess.VerifySO_SO(csocode, connectionString, out list, out errMsg);
        }

        /// <summary>
        /// 获取销售订单子表
        /// </summary>
        /// <param name="csocode_id">id号</param>
        /// <param name="connectionString"></param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取销售订单子表")]
        public int GetSO_SODetails(string csocode_id, string connectionString, out DataSet Details, out string errMsg)
        {
            return DispatchListProcess.GetSO_SODetails(csocode_id, connectionString, out Details, out errMsg);
        }

        /// <summary>
        /// 生成销售发货单
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "生成销售发货单")]
        public int SaveDispatchList(DispatchList dl, string connectionString, string accid, string year, out string errMsg)
        {
            return DispatchListProcess.SaveDispatchList(dl, connectionString, accid, year, out errMsg);
        }

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
        [WebMethod(Description = "批次管理：根据存货编码，仓库编码，货位编码来获取批次管理")]
        public int GetBatchList(string cInvCode, string cWhCode, string cPosition, string ConnectionString, out DataSet dsBatch, out string errMsg)
        {
            return DispatchListProcess.GetBatchList(cInvCode, cWhCode, cPosition, ConnectionString, out  dsBatch, out  errMsg);
        }

        /// <summary>
        /// 处理销售出库单，为销售出库单添加货位信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-12</remarks>
        [WebMethod(Description = "处理销售出库单，为销售出库单添加货位信息")]
        public int InsertInvPosition(string connectionString, out string errMsg)
        {
            return DispatchListProcess.InsertInvPosition(connectionString, out errMsg);
        }

        #endregion

        #region 销售出库GSP
        /// <summary>
        /// 验证销售出库单号并获取子表信息
        /// </summary>
        /// <param name="ccode">出库单号</param>
        /// <param name="connectionString"></param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetSaleOut(string ccode, string connectionString, out DataSet Details, out string errMsg)
        {
            return GSPVouchProcess.GetSaleOut(ccode, connectionString, out Details, out errMsg);
        }

        /// <summary>
        /// 生成销售出库GSP
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <param name="flag">true:中药材,饮片;flase:普通</param>
        /// <returns></returns>
        [WebMethod]
        public int SaveSaleOutGSP(SaleOutGSPVouch dl, string connectionString, bool flag, string accid, string year, out string errMsg)
        {
            if (flag)
                return GSPVouchProcess.SaveSaleOutGSP_CHM(dl, connectionString, accid, year, out errMsg);
            else
                return GSPVouchProcess.SaveSaleOutGSP(dl, connectionString, accid, year, out errMsg);
        }
        #endregion

        #region 销售退货GSP
        /// <summary>
        /// 验证销售退货单号并获取子表信息
        /// </summary>
        /// <param name="ccode">退货单号</param>
        /// <param name="connectionString"></param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetSaleBack(string cdlcode, string connectionString, out DataSet list, out string errMsg)
        {
            return GSPVouchProcess.GetSaleBack(cdlcode, connectionString, out list, out errMsg);
        }

        /// <summary>
        /// 生成销售退货GSP
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int SaveSaleBackGSP(SaleBackGSPVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            return GSPVouchProcess.SaveSaleBackGSP(dl, connectionString, accid, year, out errMsg);
        }
        #endregion

        #region 采购退货GSP
        /// <summary>
        /// 验证采购入库单红字单号并获取子表信息
        /// </summary>
        /// <param name="ccode">单号</param>
        /// <param name="connectionString"></param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetPurchaseBack(string ccode, string connectionString, out DataSet list, out string errMsg)
        {
            return GSPVouchProcess.GetPurchaseBack(ccode, connectionString, out list, out errMsg);
        }

        /// <summary>
        /// 生成采购退货GSP
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="connectionString"></param>
        /// <param name="accid"></param>
        /// <param name="year"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int SavePurchaseBackGSP(PurchaseBackVouch dl, string connectionString, string accid, string year, out string errMsg)
        {
            return GSPVouchProcess.SavePurchaseBackGSP(dl, connectionString, accid, year, out errMsg);
        }
        #endregion

        #region 产成品入库
        /// <summary>
        /// 验证销售退货单号并获取子表信息
        /// </summary>
        /// <param name="ccode">退货单号</param>
        /// <param name="connectionString"></param>
        /// <param name="Details"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetSTInProduct(string cInvCode, string connectionString, out DataSet list, out string errMsg)
        {
            return STInProductProcess.GetSTInProduct(cInvCode, connectionString, out list, out errMsg);
        }

        [WebMethod]
        public int SaveProductIn(STInProduct dl, string connectionString, string accid, string year, out string errMsg)
        {
            return STInProductProcess.SaveProductIn(dl, connectionString, accid, year, out errMsg);
        }
        #endregion

        #region 销售出库红字
        [WebMethod]
        public int VerifyGSPBack(string cchkcode, string connectionString, out SaleOutRedList list, out string errMsg)
        {
            return SaleOutRedProcess.VerifyGSPBack(cchkcode, connectionString, out list, out errMsg);
        }

        [WebMethod]
        public int SaveSaleOutRed(SaleOutRedList dl, string connectionString, string accid, string year, out string errMsg)
        {
            return SaleOutRedProcess.SaveSaleOutRed(dl, connectionString, accid, year, out errMsg);
        }
        #endregion

        #region 盘点
        [WebMethod]
        public int getCVcodeList(string connectionString, out DataSet ds, out string errMsg)
        {
            return CheckVouchProcess.getCVcodeList(connectionString, out ds, out errMsg);
        }
        //取条码数量
        [WebMethod]
        /// <summary>
        /// 根据存货编码，盘点单，批次信息查询盘点单详细
        /// </summary>
        /// <param name="barcode">存货编码</param>
        /// <param name="cCVCode">盘点单</param>
        /// <param name="cCVBatch">批次</param>
        /// <param name="connectionString">链接字符串</param>
        /// <param name="qty"></param>
        /// <param name="sqty"></param>
        /// <param name="autoid"></param>
        /// <param name="cinvname">存货名称</param>
        /// <param name="cinvdefine1">生产厂家</param>
        /// <param name="cinvstd">规格</param>
        /// <param name="cinvdefine6"> 产地</param>
        /// <returns></returns>
        public int getQtyByBarcode(string barcode, string cCVCode, ref string cCVBatch, string connectionString,
            out string qty, out string sqty, out string autoid, out string cinvname, out string cinvdefine1, out string cinvstd, out string cinvdefine6, out string errMsg)
        {
            errMsg = "";
            int res = CheckVouchProcess.getQtyByBarcode(barcode, cCVCode, ref cCVBatch, connectionString,
                out qty, out sqty, out autoid, out cinvname, out cinvdefine1, out cinvstd, out cinvdefine6, out errMsg);
            return res;
        }
        /// <summary>
        /// 根据货位及存货编码获取盘点单子表数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cCVCode">盘点单</param>
        /// <param name="cInvCode">存货编码</param>
        /// <param name="cPosition">货位</param>
        /// <param name="cBatch">批次</param>
        /// <param name="ds">查询结果列表</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetQtyByCode(string connectionString, string cCVCode, string cInvCode, string cPosition, string cBatch, out DataSet ds, out string errMsg)
        {
            return CheckVouchProcess.GetQtyByCode(connectionString, cCVCode, cInvCode, cPosition, cBatch, out ds, out errMsg);
        }

        [WebMethod]
        /// <summary>
        /// 提交盘点单
        /// </summary>
        /// <param name="cCVCode">盘点单号</param>
        /// <param name="list">盘点列表</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int SubmitCheckVouchs(string cCVCode, List<CheckDetail> list, string connectionString, out string errMsg)
        {
            return CheckVouchProcess.SubmitCheckVouchs(cCVCode, list, connectionString, out errMsg);
        }
        #endregion


        #region 杨臻

        #region 采购到货

        [WebMethod]
        public int CreateAVOrderByPomain(string cCode, string connStr, out DataSet ds, out string errMsg)
        {
            return ArrivalProcess.CreateAVOrderByPomain(cCode, connStr, out ds, out errMsg);
        }

        [WebMethod]
        public int SaveByPomain(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            return ArrivalProcess.SaveByPomain(ds, connectionString, accid, year, out errMsg);
        }

        #endregion


        #region GSP质量验收记录单
        /// <summary>
        /// 获取质量验收单主表数据,采购入库显示单据信息
        /// </summary>
        /// <param name="qcid"></param>
        /// <param name="connStr"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        [WebMethod]
        public List<GSP_Vouchqc> GetGSPVouchqc(string qcid, string connStr, out DataSet ds)
        {
            ds = null;
            return GSP_VouchQCProcess.GetGSPvouchqc(qcid, connStr, out ds);
        }

        [WebMethod]
        public List<GSP_Vouchsqc> GetGSPVouchsqc(string qcid, string connstr, out DataSet ds)
        {
            ds = null;
            return GSP_VouchQCProcess.GetGSPVouchsqc(qcid, connstr, out ds);
        }

        [WebMethod]
        public int CreateSIOrderByGSPVouch(string cCode, string sCode, string connectionString, out DataSet SIOrder, out string errMsg)
        {
            return StockInProcess.CreateSIOrderByGSPVouch(cCode, sCode, connectionString, out SIOrder, out errMsg);
        }
        #endregion


        #region 采购入库扫描

        [WebMethod]
        public int CreateSIOrderByArriveOrder(string ccode, string POCode, string cInvCode, int isReturn, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = StockInProcess.CreateSIOrderByArriveOrder(ccode, POCode, cInvCode, isReturn, connectionString, out ds, out errMsg);
            return res;
        }

        [WebMethod]
        public int CreateSIOrderByQMCheckOrder(string cinvcode, string cbatch, string CheckCode, string ArriveCode, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = StockInProcess.CreateSIOrderByQMCheckOrder(cinvcode, cbatch, CheckCode, ArriveCode, connectionString, out ds, out errMsg);
            return res;
        }

        [WebMethod]
        public int CreateSIOrderByQMREJECTVOUCHERS(string cinvcode, string cbatch, string cwhcode, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = StockInProcess.CreateSIOrderByQMREJECTVOUCHERS(cinvcode, cbatch, cwhcode, connectionString, out ds, out errMsg);
            return res;
        }

        [WebMethod]
        public int GetArriveList(string ccode, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = StockInProcess.GetArriveList(ccode, connectionString, out ds, out errMsg);
            return res;
        }

        [WebMethod]
        public int SaveQMCheckOrder(DataSet ds, string souceVoucher, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            int res = StockInProcess.SaveQMCheckOrder(ds, souceVoucher, connectionString, accid, year, out errMsg);
            return res;
        }

        [WebMethod]
        public int SaveByArrival(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            int res = StockInProcess.SaveByArrival(ds, connectionString, accid, year, out errMsg);
            return res;
        }



        #endregion


        #region 半成品委外生产入库

        [WebMethod]
        public int CreateSIOrderByOSArriveOrder(string ccode, string POCode, string cInvCode, int isReturn, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            errMsg = "";
            int res = StockInProcess.CreateSIOrderByOSArriveOrder(ccode, POCode, cInvCode, isReturn, connectionString, out ds, out errMsg);
            return res;
        }

        [WebMethod]
        public int SaveByOutsourcing(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            errMsg = "";
            int res = StockInProcess.SaveByOutsourcing(ds, connectionString, accid, year, out errMsg);
            return res;
        }

        #endregion


        #region 组装拆卸调拨出入库

        [WebMethod]
        public int GetRecordList(string type, string ccode, string bcode, string connectionString, out DataSet ds, out string errMsg)
        {
            ds = null;
            return StockInProcess.CreateSIOrderByRecordList(type, ccode, bcode, connectionString, out ds, out errMsg);
        }

        [WebMethod]
        public int AuditByDismantle(DataSet ds, string type, string connectionString, string accid, string year, out string errMsg)
        {
            return StockInProcess.AuditByDismantle(ds, type, connectionString, accid, year, out errMsg);
        }

        #endregion


        #region 材料出库单

        [WebMethod]
        public int CreateSIOrderByOMMOrder(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {
            return StockInProcess.CreateSIOrderByOMMOrder(ccode, connectionString, out OrderList, out errMsg);
        }

        [WebMethod]
        public int SaveByOMMOrder(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            return StockInProcess.SaveByOMMOrder(ds, connectionString, accid, year, out errMsg);
        }

        [WebMethod]
        public int GetOMMOHead(string ccode, string connectionString, out DataSet OrderList, out string errMsg)
        {
            return StockInProcess.GetOMMOHead(ccode, connectionString, out OrderList, out errMsg);
        }

        [WebMethod]
        public int GetOMMOBody(string strMoDetailID, string connectionString, out DataSet DetailList, out string errMsg)
        {
            return StockInProcess.GetOMMOBody(strMoDetailID, connectionString, out DetailList, out errMsg);
        }

        #endregion

        #region 委外到货

        [WebMethod]
        public int CreateAVOrderByMomain(string cCode, string connStr, out DataSet ds, out string errMsg)
        {
            return ArrivalProcess.CreateAVOrderByMomain(cCode, connStr, out ds, out errMsg);
        }

        [WebMethod]
        public int SaveByMomain(DataSet ds, string connectionString, string accid, string year, out string errMsg)
        {
            return ArrivalProcess.SaveByMomain(ds, connectionString, accid, year, out errMsg);
        }

        [WebMethod]
        public int CreateSIOrderByMomain(string cCode, string connectionString, out DataSet SIOrder, out string errMsg)
        {
            return StockInProcess.CreateSIOrderByMomain(cCode, connectionString, out SIOrder, out errMsg);
        }

        #endregion

        #region 辅助信息
        //[WebMethod]
        //public int GetBatchList(string invCode, string bhCode, string whCode, string ConnectionString, out DataSet dsBatch, out string errMsg)
        //{
        //    return StockInProcess.GetBatchList(invCode, bhCode, whCode, ConnectionString, out dsBatch, out errMsg);
        //}

        [WebMethod]
        public int GetWHList(string invCode, string userName, string ConnectionString, out DataSet dsWareHouse, out string errMsg)
        {
            return StockInProcess.GetWHList(invCode, userName, ConnectionString, out dsWareHouse, out errMsg);
        }

        [WebMethod]
        public int GetWHInfo(string cWhCode, string ConnectionString, out DataSet dsWareHouse, out string errMsg)
        {
            return StockInProcess.GetWHInfo(cWhCode, ConnectionString, out dsWareHouse, out errMsg);
        }

        [WebMethod]
        public int GetDeptList(string userName, string ConnectionString, out DataSet dsDepartment, out string errMsg)
        {
            return StockInProcess.GetDeptList(userName, ConnectionString, out dsDepartment, out errMsg);
        }

        [WebMethod]
        public int GetPTList(string cWhCode, string iCode, string cBatch, string iTrackID, int bRdFlag, string ConnectionString, out DataSet dsPosition, out string errMsg)
        {
            return StockInProcess.GetPTList(cWhCode, iCode, cBatch, iTrackID, bRdFlag, ConnectionString, out dsPosition, out errMsg);
        }

        //[WebMethod]
        //public decimal GetPTSum(string cWhCode, string pCode, string iCode, string cBatch, string ConnectionString, out string errMsg)
        //{
        //    return StockInProcess.GetPTSum(cWhCode, pCode, iCode, cBatch, ConnectionString, out errMsg);
        //}

        [WebMethod]
        public decimal GetWHQuan(string cInvCode, string cBatch, string cWhCode, string ConnectionString, out string errMsg)
        {
            return StockInProcess.GetWHQuan(cInvCode, cBatch, cWhCode, ConnectionString, out errMsg);
        }

        [WebMethod]
        public decimal GetPTQuan(string cInvCode, string cBatch, string cWhCode, string cPosCode, string ConnectionString, out string errMsg)
        {
            return StockInProcess.GetPTQuan(cInvCode, cBatch, cWhCode, cPosCode, ConnectionString, out errMsg);
        }

        [WebMethod]
        public bool GetPTExist(string cWhCode, string cPosCode, string ConnectionString, out string errMsg)
        {
            return StockInProcess.GetPTExist(cWhCode, cPosCode, ConnectionString, out errMsg);
        }

        [WebMethod]
        public int GetBatchInfo(string invCode, string bhCode, string whCode, string ConnectionString, out DataSet dsBatch, out string errMsg)
        {
            return StockInProcess.GetBatchInfo(invCode, bhCode, whCode, ConnectionString, out dsBatch, out errMsg);
        }

        #endregion

        #endregion


        /// <summary>
        /// 根据一维码（69码）获取存货编码
        /// 2012－09－04
        /// </summary>
        /// <param name="cbarcode">69码</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>查到返回true,没有查到返回false</returns>
        [WebMethod]
        public bool GetCInvCode(string cBarCode, string connectionString, out string cInvCode, out string errMsg)
        {
            return CommonDA.GetCInvCode(cBarCode, connectionString, out cInvCode, out errMsg);
        }

        /// <summary>
        /// 根据仓库编码获取所有货位及货位存储信息
        /// </summary>
        /// <param name="cWhCode">仓库编码</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-09-10</remarks>
        [WebMethod]
        public bool GetPosition(string connectionString, string cWhCode, out DataSet ds_Position, out string errMsg)
        {
            return CommonDA.GetPosition(connectionString, cWhCode, out ds_Position, out errMsg);
        }

        /// <summary>
        /// 货位现存量查询
        /// </summary>
        /// <param name="cBarCode">69码</param>
        /// <param name="isPosition">标识是否是货位存量查询还是现存量查询</param>
        /// <param name="cWhCode">仓库编码集合</param>
        /// <param name="connectionString">链接字符串</param>
        /// <param name="ds">结果</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-10-29</remarks>
        [WebMethod]
        public int QuantitySerarch(string cBarCode, bool isPosition, List<string> cWhCode, string connectionString, out DataSet ds, out string errMsg)
        {
            return CommonDA.QuantitySerarch(cBarCode, isPosition, cWhCode, connectionString, out ds, out errMsg);
        }


        #region 监管码管理

        /// <summary>
        /// 判断监管码是否已存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        [WebMethod(Description = "判断监管码是否已存在")]
        public bool ExistsRegulatory(string connectionString, Model.Regulatory data)
        {
            return U8DataAccess.Regulatory.ExistsRegulatory(connectionString, data);
        }

        /// <summary>
        /// 添加监管码对象
        /// </summary>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        [WebMethod(Description = "添加监管码对象")]
        public bool AddRegulatory(string connectionString, Model.Regulatory data, out string errMsg)
        {
            return U8DataAccess.Regulatory.AddRegulatory(connectionString, data, out errMsg);
        }

        /// <summary>
        /// 更新监管码
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        [WebMethod(Description = "更新监管码")]
        public bool UpdateRegulatory(string connectionString, Model.Regulatory data, out string errMsg)
        {
            return U8DataAccess.Regulatory.UpdateRegulatory(connectionString, data, out errMsg);
        }

        /// <summary>
        /// 获取单个监管码对象
        /// </summary>
        /// <param name="connnectionString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        [WebMethod(Description = "获取单个监管码对象")]
        public Model.Regulatory GetRegulatoryModel(string connnectionString, Model.Regulatory data, out string errMsg)
        {
            return U8DataAccess.Regulatory.GetModel(connnectionString, data, out errMsg);
        }
        /// <summary>
        /// 查询监管码列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        [WebMethod(Description = "查询监管码列表")]
        public DataTable GetRegulatoryList(string connectionString, Model.Regulatory data, out int total)
        {
            return U8DataAccess.Regulatory.GetList(connectionString, data, out total);
        }
        #endregion


        #region 回写快递单号2013-03-18

        /// <summary>
        /// 获取所有的物流名称
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获取所有的物流名称")]
        public DataTable GetShoppingChoiceList(string connectionString)
        {
            return ExpressOrderProcess.GetShoppingChoiceList(connectionString);
        }

        /// <summary>
        /// 根据发货单号查询发货单信息
        /// </summary>
        /// <param name="cDLCode">单据号</param>
        /// <returns></returns>
        [WebMethod(Description = "根据发货单号查询发货单信息")]
        public DataTable GetDispatchListByCDLCode(string connectionString, string cDLCode, out string errMsg)
        {
            return ExpressOrderProcess.GetDispatchListByCDLCode(connectionString, cDLCode, out errMsg);
        }

        /// <summary>
        /// 回写发货单快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateDispatchListExpressOrder(string connectionString, Model.DispatchList data)
        {
            return ExpressOrderProcess.UpdateDispatchListExpressOrder(connectionString, data);
        }

        /// <summary>
        /// 根据发票号查询发票单据信息
        /// </summary>
        /// <param name="cSBVCode">单据号</param>
        /// <returns></returns>
        [WebMethod(Description = "根据发票号查询发票单据信息")]
        public DataTable GetSaleBillVouchByCSBVCode(string connectionString, string cSBVCode, out string errMsg)
        {
            return ExpressOrderProcess.GetSaleBillVouchByCSBVCode(connectionString, cSBVCode, out errMsg);
        }

        /// <summary>
        /// 回写销售发票快递单号
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [WebMethod(Description = "回写销售发票快递单号")]
        public bool UpdateSaleBillVouchExpressOrder(string connectionString, Model.SaleBillVouch data)
        {
            return ExpressOrderProcess.UpdateSaleBillVouchExpressOrder(connectionString, data);
        }
        #endregion


        #region 条码项目升级修改 2013-11-26

        /// <summary>
        /// 根据采购订单加载订单信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cOrderCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据采购订单加载订单信息")]
        public DataSet Po_Pomain_Load(string connectionString, string cOrderCode, out string errMsg)
        {
            return new PurchaseArrivalProcess().Po_Pomain_Load(connectionString, cOrderCode, out errMsg);
        }

        /// <summary>
        /// 保存采购到货单
        /// </summary>
        /// <param name="arrivalVouch"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [WebMethod(Description = "保存采购到货单")]
        public bool PU_ArrivalVouch_Save(User user, ArrivalVouch arrivalVouch, out string errMsg)
        {
            return new PurchaseArrivalProcess().PU_ArrivalVouch_Save(user, arrivalVouch, out errMsg);
        }

        #endregion
    }
}
