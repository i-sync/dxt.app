using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PurchaseBackVouch
    {
        #region 属性
        private string m_iRdId;
        /// <summary>
        /// 采购入库单主表编号
        /// </summary>
        public string iRdId
        {
            get { return m_iRdId; }
            set { m_iRdId = value; }
        }

        private string m_cRdCode;
        /// <summary>
        /// 采购入库单编号
        /// </summary>
        public string cRdCode
        {
            get { return m_cRdCode; }
            set { m_cRdCode = value; }
        }

        private string m_dArvdate;
        /// <summary>
        /// 到货日期
        /// </summary>
        public string dArvdate
        {
            get { return m_dArvdate; }
            set { m_dArvdate = value; }
        }

        private string m_cVenCode;
        /// <summary>
        /// 供货单位编码 
        /// </summary>
        public string cVenCode
        {
            get { return m_cVenCode; }
            set { m_cVenCode = value; }
        }

        private string m_cRdMaker;
        /// <summary>
        /// 退货人
        /// </summary>
        public string cRdMaker
        {
            get { return m_cRdMaker; }
            set { m_cRdMaker = value; }
        }

        private string m_cMaker;
        /// <summary>
        /// 制单人 
        /// </summary>
        public string cMaker
        {
            get { return m_cMaker; }
            set { m_cMaker = value; }
        }

        private string m_cWhCode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode
        {
            get { return m_cWhCode; }
            set { m_cWhCode = value; }
        }

        private string m_cWhName;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName
        {
            get { return m_cWhName; }
            set { m_cWhName = value; }
        }

        private string m_cDefine1;
        /// <summary>
        /// 表头自定义项1
        /// </summary>
        public string cDefine1
        {
            get { return m_cDefine1; }
            set { m_cDefine1 = value; }
        }
        #endregion

        List<PurchaseBackDetail> m_U8Details;
        /// <summary>
        /// 来源数据
        /// </summary>
        public List<PurchaseBackDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        List<PurchaseBackDetail> m_OperateDetails;
        /// <summary>
        /// 扫描数据
        /// </summary>
        public List<PurchaseBackDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }

        #region 构造函数
        public PurchaseBackVouch()
        {}
        #endregion
    }
}
