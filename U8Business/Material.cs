using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Model;

namespace U8Business
{
    public class Material
    {
        #region
        /// <summary>
        /// 保质期单位
        /// </summary>
        private string m_cMassUnit;
        public string cMassUnit
        {
            get { return m_cMassUnit; }
            set { m_cMassUnit = value; }
        }

        /// <summary>
        /// 保质期天数
        /// </summary>
        private int m_iMassDate;
        public int iMassDate
        {
            get { return m_iMassDate; }
            set { m_iMassDate = value; }
        }

        /// <summary>
        /// 是否固定换算率
        /// </summary>
        public enum FixExch { NO = 0, Fixation = 1, Fluctuate = 2 };
        private FixExch m_enmExchange;
        public FixExch ExchangeFuc
        {
            get { return m_enmExchange; }
            set { m_enmExchange = value; }
        }

        /// <summary>
        /// 存货大类编码
        /// </summary>
        private string m_strClassCode;
        public string ClassCode
        {
            get { return m_strClassCode; }
            set { m_strClassCode = value; }
        }

        /// <summary>
        /// 是否做批次管理
        /// </summary>
        private bool m_bInvBatch;
        public bool InvBatch
        {
            get { return m_bInvBatch; }
            set { m_bInvBatch = value; }
        }

        /// <summary>
        /// 存货编码
        /// </summary>
        private string m_cinvcode;
        public string cinvcode
        {
            get { return m_cinvcode; }
            set { m_cinvcode = value; }
        }

        /// <summary>
        /// 规格
        /// </summary>
        private string m_cinvstd;
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        /// <summary>
        /// 存货名称
        /// </summary>
        private string m_cinvname;
        public string cinvname
        {
            get { return m_cinvname; }
            set { m_cinvname = value; }
        }

        /// <summary>
        /// 存货计量组
        /// </summary>
        private string m_strGroupCode;
        public string GroupCode
        {
            get { return m_strGroupCode; }
            set { m_strGroupCode = value; }
        }

        /// <summary>
        /// 主计量单位
        /// </summary>
        private string m_strComUnit;
        public string ComUnit
        {
            get { return m_strComUnit; }
            set { m_strComUnit = value; }
        }

        /// <summary>
        /// 辅计量单位
        /// </summary>
        private string m_strAssComUnit;
        public string AssComUnit
        {
            get { return m_strAssComUnit; }
            set { m_strAssComUnit = value; }
        }

        /// <summary>
        /// 主计量单位名称
        /// </summary>
        private string m_strComUnitName;
        public string ComUnitName
        {
            get { return m_strComUnitName; }
            set { m_strComUnitName = value; }
        }

        /// <summary>
        /// 辅计量单位名称
        /// </summary>
        private string m_strAssComUnitName;
        public string AssComUnitName
        {
            get { return m_strAssComUnitName; }
            set { m_strAssComUnitName = value; }
        }

        /// <summary>
        /// 税率
        /// </summary>
        private decimal m_dTaxRate;
        public decimal TaxRate
        {
            get { return m_dTaxRate; }
            set { m_dTaxRate = value; }
        }


        private string m_cengineerfigno;
        /// <summary>
        /// 
        /// </summary>
        public string cengineerfigno
        {
            get { return m_cengineerfigno; }
            set { m_cengineerfigno = value; }
        }

        private decimal m_iquantity;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal iquantity
        {
            get { return m_iquantity; }
            set { m_iquantity = value; }
        }

        private string m_cdefine1;
        /// <summary>
        /// 
        /// </summary>
        public string cdefine1
        {
            get { return m_cdefine1; }
            set { m_cdefine1 = value; }
        }

        private string m_cdefine2;
        /// <summary>
        /// 
        /// </summary>
        public string cdefine2
        {
            get { return m_cdefine2; }
            set { m_cdefine2 = value; }
        }

        private string m_cdefine4;
        /// <summary>
        /// 
        /// </summary>
        public string cdefine4
        {
            get { return m_cdefine4; }
            set { m_cdefine4 = value; }
        }

        private decimal m_inum;
        /// <summary>
        /// 粒数
        /// </summary>
        public decimal inum
        {
            get { return m_inum; }
            set { m_inum = value; }
        }

        private string m_batch;
        /// <summary>
        /// 批次
        /// </summary>
        public string cbatch
        {
            get { return m_batch; }
            set { m_batch = value; }
        }

        private string m_cName;
        /// <summary>
        /// 检验员
        /// </summary>
        public string cName
        {
            get { return m_cName; }
            set { m_cName = value; }
        }

        /// <summary>
        /// 主副计量换算率
        /// </summary>
        private decimal m_dChangeRate;
        public decimal ChangeRate
        {
            get { return m_dChangeRate; }
            set { m_dChangeRate = value; }
        }

        /// <summary>
        /// 默认仓库
        /// </summary>
        private Warehouse m_oDefWareHouse;
        public Warehouse DefWareHouse
        {
            get { return m_oDefWareHouse; }
            set { m_oDefWareHouse = value; }
        }

        /// <summary>
        /// 默认储位名称
        /// </summary>
        private string m_cposition;
        public string cposition
        {
            get { return m_cposition; }
            set { m_cposition = value; }
        }

        /// <summary>
        /// 默认储位代码
        /// </summary>
        private string m_cposcode;
        public string cposcode
        {
            get { return m_cposcode; }
            set { m_cposcode = value; }
        }

        /// <summary>
        /// 图型号
        /// </summary>
        private string m_ccusinvcode;
        public string ccusinvcode
        {
            get { return m_ccusinvcode; }
            set { m_ccusinvcode = value; }
        }

        /// <summary>



        /// <summary>
        /// 订单号
        /// </summary>
        private string m_ordernumber;
        public string ordernumber
        {
            get { return m_ordernumber; }
            set { m_ordernumber = value; }
        }

        /// <summary>
        /// 英语名称
        /// </summary>
        private string m_cenglishname;
        public string cenglishname
        {
            get { return m_cenglishname; }
            set { m_cenglishname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string m_ccurrencyname;
        public string ccurrencyname
        {
            get { return m_ccurrencyname; }
            set { m_ccurrencyname = value; }
        }

        /// <summary>
        /// 部门号
        /// </summary>
        private string m_cinvdepcode;
        public string cinvdepcode
        {
            get { return m_cinvdepcode; }
            set { m_cinvdepcode = value; }
        }


        private decimal m_iInvSPrice;
        /// <summary>
        /// 参考成本
        /// </summary> 
        public decimal iInvSPrice
        {
            get { return m_iInvSPrice; }
            set { m_iInvSPrice = value; }
        }

        public string dvdate;
        public string dmdate;
        //public string imassdate;
        //public string cmassunit;
        public string iexpiratdatecalcu;
        public string cexpirationdate;
        public string dexpirationdate;

        public string cinvdefine1;
        public string cinvdefine2;
        public string cinvdefine4;
        public string cbatchproperty6;
        public decimal realiquantity;//净重
        public string cvenname;

        private string m_cinvccname;
        /// <summary>
        /// 大类名称
        /// </summary>
        public string cinvccname
        {
            get { return m_cinvccname; }
            set { m_cinvccname = value; }
        }

        private string m_cInvccode;
        public string Invccode
        {
            get { return m_cInvccode; }
            set { m_cInvccode = value; }
        }
        #endregion
        public Material()
        {
        }


        public Material(System.Data.DataSet dstIniData)
        {
            if (dstIniData != null && dstIniData.Tables.Count > 0 && dstIniData.Tables[0].Rows.Count > 0)
            {
                this.m_cinvcode = dstIniData.Tables[0].Rows[0]["cinvcode"].ToString();
                this.m_cinvname = dstIniData.Tables[0].Rows[0]["cinvname"].ToString();

                if (dstIniData.Tables[0].Columns.Contains("cinvstd"))
                    this.m_cinvstd = dstIniData.Tables[0].Rows[0]["cinvstd"].ToString();

                if (dstIniData.Tables[0].Columns.Contains("cposition"))
                    this.m_cposition = dstIniData.Tables[0].Rows[0]["cposition"].ToString();

                if (dstIniData.Tables[0].Columns.Contains("cengineerfigno"))
                    this.m_cengineerfigno = dstIniData.Tables[0].Rows[0]["cengineerfigno"].ToString();

                if (dstIniData.Tables[0].Columns.Contains("ccusinvcode"))
                    this.m_ccusinvcode = dstIniData.Tables[0].Rows[0]["ccusinvcode"].ToString();

                if (dstIniData.Tables[0].Columns.Contains("cdefine1"))
                    this.m_cdefine1 = dstIniData.Tables[0].Rows[0]["cdefine1"].ToString();


            }
        }
        /// <summary>
        /// 获取存货信息
        /// </summary>
        /// <param name="cInvCode">存货编码</param>
        /// <returns></returns>
        public static Material GetMaterial(string cInvCode)
        {
            Material objMaterial;
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            System.Data.DataSet dstResult = new System.Data.DataSet();
            //co.Service.GetMaterial(cInvCode, Common.CurrentUser.ConnectionString, out dstResult, out errMsg);
            if (dstResult.Tables[0].Rows.Count == 0)
            {
                throw new Exception("存货编码不正确!");
            }
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            objMaterial = new Material(dstResult);

            return objMaterial;
        }
        /*
        /// <summary>
        /// 获得形态转换的数据
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cbatch"></param>
        /// <param name="cwhcode"></param>
        /// <returns></returns>
        public static Material GetAssemInventory(string cInvCode, string cbatch, string cwhcode)
        {
            Material objMaterial;
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            System.Data.DataSet dstResult = new System.Data.DataSet();
            co.Service.GetAssemInventory(cInvCode, cbatch, cwhcode, Common.CurrentUser.ConnectionString, out dstResult, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            objMaterial = new Material();
            objMaterial.cinvcode = dstResult.Tables[0].Rows[0]["cinvcode"].ToString();
            objMaterial.cinvname = dstResult.Tables[0].Rows[0]["cinvname"].ToString();
            objMaterial.cinvdepcode = dstResult.Tables[0].Rows[0]["oldcinvname"].ToString();//临时用下
            objMaterial.dvdate = dstResult.Tables[0].Rows[0]["dvdate"].ToString();
            objMaterial.dmdate = dstResult.Tables[0].Rows[0]["dmdate"].ToString();
            objMaterial.iMassDate = Convert.ToInt32(dstResult.Tables[0].Rows[0]["imassdate"]);
            objMaterial.cMassUnit = dstResult.Tables[0].Rows[0]["cmassunit"].ToString();
            objMaterial.iexpiratdatecalcu = dstResult.Tables[0].Rows[0]["iexpiratdatecalcu"].ToString();
            objMaterial.cexpirationdate = dstResult.Tables[0].Rows[0]["cexpirationdate"].ToString();
            objMaterial.dexpirationdate = dstResult.Tables[0].Rows[0]["dexpirationdate"].ToString();
            objMaterial.iquantity = Convert.ToDecimal(dstResult.Tables[0].Rows[0]["iquantity"]);
            objMaterial.cbatchproperty6 = Convert.ToString(dstResult.Tables[0].Rows[0]["cbatchproperty6"]);
            objMaterial.iInvSPrice = Convert.ToDecimal(dstResult.Tables[0].Rows[0]["iInvSPrice"].ToString());

            return objMaterial;
        }

        public static int checkInvposition(string cInvCode, string cbatch, string cposition, decimal iquantity)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            int i = co.Service.checkInvposition(cInvCode, cbatch, cposition, iquantity, Common.CurrentUser.ConnectionString, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            return i;
        }

        public static int checkTMInvposition(string cInvCode, string cbatch, string cposition, decimal iquantity, string id, string autoid)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            int i = co.Service.checkTMInvposition(cInvCode, cbatch, cposition, iquantity, id, autoid, Common.CurrentUser.ConnectionString, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            return i;
        }

        //车间的信息
        public static List<Material> Getmom_order(string m_datetime, string cinvccode)
        {
            Material objMaterial;
            Common co = Common.GetInstance();
            List<Material> m_material = new List<Material>();
            string errMsg = "";//出错信息
            System.Data.DataSet dstResult = new System.Data.DataSet();
            try
            {
                co.Service.Getmom_order(m_datetime, cinvccode, Common.CurrentUser.ConnectionString, out dstResult, out errMsg);
                if (errMsg != "")
                {
                    throw new Exception(errMsg);
                }
                foreach (DataRow dr in dstResult.Tables[0].Rows)
                {
                    objMaterial = new Material();
                    objMaterial.cinvcode = dr["cinvcode"].ToString();
                    objMaterial.cbatch = dr["cbatch"].ToString();
                    objMaterial.cinvname = dr["cinvname"].ToString();
                    objMaterial.cdefine1 = dr["cinvdefine1"].ToString();
                    objMaterial.cdefine4 = dr["cinvdefine4"].ToString();
                    m_material.Add(objMaterial);
                }
                return m_material;
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        //打印使用,获得英文名
        public static Material Getcinvdefine4(string cInvCode, string cbatch)
        {
            Material objMaterial;
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            System.Data.DataSet dstResult = new System.Data.DataSet();
            co.Service.Getcinvdefine4(cInvCode, cbatch, Common.CurrentUser.ConnectionString, out dstResult, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            objMaterial = new Material();
            objMaterial.cinvdefine1 = dstResult.Tables[0].Rows[0]["cinvdefine1"].ToString();
            return objMaterial;
        }

        //获得成品或半成品单桶的重量信息
        public static decimal GetSCGiquantity(string barcode)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            decimal m_iquantity;
            co.Service.GetSCGiquantity(barcode, Common.CurrentUser.ConnectionString, out m_iquantity, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }

            return m_iquantity;
        }

        //成品仓库内获得物品的数量
        public static decimal GetCfreeiquantity(string cwhcode, string cinvcode, string cbatch, string cfree1)
        {
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            decimal m_iquantity;
            co.Service.GetCfreeiquantity(cinvcode, cwhcode, cfree1, cbatch, Common.CurrentUser.ConnectionString, out m_iquantity, out errMsg);
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }

            return m_iquantity;
        }
        public static Material GetMaterialByDefine6(string civnDefine6)
        {
            Material objMaterial;
            Common co = Common.GetInstance();

            string errMsg = "";//出错信息
            System.Data.DataSet dstResult = new System.Data.DataSet();
            co.Service.GetMaterialByDefine6(civnDefine6, Common.CurrentUser.ConnectionString, out dstResult, out errMsg);
            if (dstResult.Tables[0].Rows.Count == 0)
            {
                //throw new Exception("没有这个存货编码");
                return null;
            }
            if (errMsg != "")
            {
                throw new Exception(errMsg);
            }
            objMaterial = new Material(dstResult);

            return objMaterial;
        }
        */
    }
}
