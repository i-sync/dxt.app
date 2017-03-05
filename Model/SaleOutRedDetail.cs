using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaleOutRedDetail
    {
        #region 属性

        private string m_cinvcode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cinvcode
        {
            get { return m_cinvcode; }
            set { m_cinvcode = value; }
        }

        private string m_cinvname;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cinvname
        {
            get { return m_cinvname; }
            set { m_cinvname = value; }
        }

        private string m_cwhcode;

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cwhcode
        {
            get { return m_cwhcode; }
            set { m_cwhcode = value; }
        }

        private string m_cinvstd;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        private Decimal m_iquantity;
        /// <summary>
        /// 数量
        /// </summary>
        public Decimal iquantity
        {
            get { return m_iquantity; }
            set { m_iquantity = value; }
        }

        private decimal m_inewquantity;
        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal inewquantity
        {
            get { return m_inewquantity; }
            set { m_inewquantity = value; }
        }

        private Decimal m_inum;
        /// <summary>
        /// 件数
        /// </summary>
        public Decimal inum
        {
            get { return m_inum; }
            set { m_inum = value; }
        }

        private Decimal m_iunitcost;
        /// <summary>
        /// 单价
        /// </summary>
        public Decimal iunitcost
        {
            get { return m_iunitcost; }
            set { m_iunitcost = value; }
        }

        private Decimal m_iprice;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal iprice
        {
            get { return m_iprice; }
            set { m_iprice = value; }
        }

        private string m_cbatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string cbatch
        {
            get { return m_cbatch; }
            set { m_cbatch = value; }
        }

        private string m_dvdate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string dvdate
        {
            get { return m_dvdate; }
            set { m_dvdate = value; }
        }

        private string m_cdefine22;
        /// <summary>
        /// 表体自定义项1
        /// </summary>
        public string cdefine22
        {
            get { return m_cdefine22; }
            set { m_cdefine22 = value; }
        }

        private string m_dmadedate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string dmadedate
        {
            get { return m_dmadedate; }
            set { m_dmadedate = value; }
        }

        private int m_imassdate;
        /// <summary>
        /// 保质期
        /// </summary>
        public int imassdate
        {
            get { return m_imassdate; }
            set { m_imassdate = value; }
        }

        private int m_icheckids;
        /// <summary>
        /// 检验单子表id
        /// </summary>
        public int icheckids
        {
            get { return m_icheckids; }
            set { m_icheckids = value; }
        }

        private string m_cmassunit;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string cmassunit
        {
            get { return m_cmassunit; }
            set { m_cmassunit = value; }
        }

        private int m_bcosting;
        /// <summary>
        /// 是否核算
        /// </summary>
        public int bcosting
        {
            get { return m_bcosting; }
            set { m_bcosting = value; }
        }

        private string m_dExpirationdate;
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public string dExpirationdate
        {
            get { return m_dExpirationdate; }
            set { m_dExpirationdate = value; }
        }

        private string m_cExpirationdate;
        /// <summary>
        /// 有效期至
        /// </summary>
        public string cExpirationdate
        {
            get { return m_cExpirationdate; }
            set { m_cExpirationdate = value; }
        }

        private int m_iExpiratDateCalcu;
        /// <summary>
        /// 有效期推算方式
        /// </summary>
        public int iExpiratDateCalcu
        {
            get { return m_iExpiratDateCalcu; }
            set { m_iExpiratDateCalcu = value; }
        }

        private int m_idlsid;
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public int idlsid
        {
            get { return m_idlsid; }
            set { m_idlsid = value; }
        }

        private string m_cposition;
        /// <summary>
        /// 货位
        /// </summary>
        public string cposition
        {
            get { return m_cposition; }
            set { m_cposition = value; }
        }

        private string m_cinvdefine1;
        /// <summary>
        /// 生产单位
        /// </summary>
        public string cinvdefine1
        {
            get { return m_cinvdefine1; }
            set { m_cinvdefine1 = value; }
        }

        private string m_cinvm_unit;
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string cinvm_unit
        {
            get { return m_cinvm_unit; }
            set { m_cinvm_unit = value; }
        }
        private string m_ccusname;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ccusname
        {
            get { return m_ccusname; }
            set { m_ccusname = value; }
        }

        private string m_ccusabbname;
        /// <summary>
        /// 客户简称
        /// </summary>
        public string ccusabbname
        {
            get { return m_ccusabbname; }
            set { m_ccusabbname = value; }
        }

        #endregion

        public SaleOutRedDetail CreateAttriveDetail()
        {
            return (SaleOutRedDetail)this.MemberwiseClone();
        }
    }
}
