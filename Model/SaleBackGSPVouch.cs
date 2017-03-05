using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaleBackGSPVouch
    {
        #region 属性
        private string m_ICODE;
        /// <summary>
        /// 采购到货退货单主表标识 
        /// </summary>
        public string ICODE
        {
            get { return m_ICODE; }
            set { m_ICODE = value; }
        }

        private string m_CCODE;
        /// <summary>
        /// 采购到货退货单号 
        /// </summary>
        public string CCODE
        {
            get { return m_CCODE; }
            set { m_CCODE = value; }
        }

        private string m_DARVDATE;
        /// <summary>
        /// 到货退货日期
        /// </summary>
        public string DARVDATE
        {
            get { return m_DARVDATE; }
            set { m_DARVDATE = value; }
        }

        private string m_CMAKER;
        /// <summary>
        /// 制单人 
        /// </summary>
        public string CMAKER
        {
            get { return m_CMAKER; }
            set { m_CMAKER = value; }
        }
        #endregion

        List<SaleBackGSPDetail> m_U8Details;
        /// <summary>
        /// 来源数据
        /// </summary>
        public List<SaleBackGSPDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        List<SaleBackGSPDetail> m_OperateDetails;
        /// <summary>
        /// 扫描数据
        /// </summary>
        public List<SaleBackGSPDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }

        #region 构造函数
        public SaleBackGSPVouch()
        {}
        #endregion
    }
}
