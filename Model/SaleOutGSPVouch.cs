using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaleOutGSPVouch
    {
        #region 属性
        private string m_id;
        /// <summary>
        /// 药品记录单主表标识
        /// </summary>
        public string ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_NOTEID;
        /// <summary>
        /// 药品记录单号
        /// </summary>
        public string NOTEID
        {
            get { return m_NOTEID; }
            set { m_NOTEID = value; }
        }

        private DateTime m_DDATE;
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime DDATE
        {
            get { return m_DDATE; }
            set { m_DDATE = value; }
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

        private string m_CDEFINE2;
        /// <summary>
        /// 自定义项2 
        /// </summary>
        public string CDEFINE2
        {
            get { return m_CDEFINE2; }
            set { m_CDEFINE2 = value; }
        }

        private string m_CDEFINE3;
        /// <summary>
        /// 自定义项3 
        /// </summary>
        public string CDEFINE3
        {
            get { return m_CDEFINE3; }
            set { m_CDEFINE3 = value; }
        }

        private decimal m_CDEFINE7;
        /// <summary>
        /// 自定义项7 
        /// </summary>
        public decimal CDEFINE7
        {
            get { return m_CDEFINE7; }
            set { m_CDEFINE7 = value; }
        }

        private string m_CDEFINE11;
        /// <summary>
        /// 自定义项11 
        /// </summary>
        public string CDEFINE11
        {
            get { return m_CDEFINE11; }
            set { m_CDEFINE11 = value; }
        }
        #endregion

        private List<GSPVouchDetail> m_U8Details;
        /// <summary>
        /// 出库单
        /// </summary>
        public List<GSPVouchDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        private List<GSPVouchDetail> m_OperateDetails;
        /// <summary>
        /// 扫描数据
        /// </summary>
        public List<GSPVouchDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }

        #region 构造函数
        public SaleOutGSPVouch()
        {}

        public SaleOutGSPVouch(System.Data.DataSet ds)
        {

        }
        #endregion
    }
}
