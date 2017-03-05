using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaleOutRedList
    {
        #region 属性

        private string m_cbuscode;
        /// <summary>
        /// 对应业务单号 
        /// </summary>
        public string cbuscode
        {
            get { return m_cbuscode; }
            set { m_cbuscode = value; }
        }

        private DateTime m_dDate;
        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime dDate
        {
            get { return m_dDate; }
            set { m_dDate = value; }
        }

        private string m_cbustype;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string cbustype
        {
            get { return m_cbustype; }
            set { m_cbustype = value; }
        }

        private string m_cstcode;
        /// <summary>
        /// 销售类型
        /// </summary>
        public string cstcode
        {
            get { return m_cstcode; }
            set { m_cstcode = value; }
        }
        private string m_csocode;
        /// <summary>
        /// 订单号
        /// </summary>
        public string csocode
        {
            get { return m_csocode; }
            set { m_csocode = value; }
        }

        private string m_ccuscode;
        /// <summary>
        /// 客户编码
        /// </summary>
        public string ccuscode
        {
            get { return m_ccuscode; }
            set { m_ccuscode = value; }
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

        private string m_cdepcode;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cdepcode
        {
            get { return m_cdepcode; }
            set { m_cdepcode = value; }
        }

        private string m_cdepname;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string cdepname
        {
            get { return m_cdepname; }
            set { m_cdepname = value; }
        }

        private string m_cpersoncode;
        /// <summary>
        /// 业务员编码
        /// </summary>
        public string cpersoncode
        {
            get { return m_cpersoncode; }
            set { m_cpersoncode = value; }
        }

        private string m_cpersonname;
        /// <summary>
        /// 业务员
        /// </summary>
        public string cpersonname
        {
            get { return m_cpersonname; }
            set { m_cpersonname = value; }
        }

        private string m_ccusperson;
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string ccusperson
        {
            get { return m_ccusperson; }
            set { m_ccusperson = value; }
        }

        private string m_cmemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string cmemo
        {
            get { return m_cmemo; }
            set { m_cmemo = value; }
        }

        private string m_cmaker;
        /// <summary>
        /// 制单人
        /// </summary>
        public string cmaker
        {
            get { return m_cmaker; }
            set { m_cmaker = value; }
        }

        private string m_cchkcode;
        /// <summary>
        /// 检验单号
        /// </summary>
        public string cchkcode
        {
            get { return m_cchkcode; }
            set { m_cchkcode = value; }
        }

        private string m_dchkdate;
        /// <summary>
        /// 检验日期
        /// </summary>
        public string dchkdate
        {
            get { return m_dchkdate; }
            set { m_dchkdate = value; }
        }

        private string m_cchkperson;
        /// <summary>
        /// 检验员 
        /// </summary>
        public string cchkperson
        {
            get { return m_cchkperson; }
            set { m_cchkperson = value; }
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

        private string m_cdlid;
        /// <summary>
        /// 发货退货单主表标识 
        /// </summary>
        public string cdlid
        {
            get { return m_cdlid; }
            set { m_cdlid = value; }
        }

        private string m_cdefine10;
        /// <summary>
        /// 流通监管码
        /// </summary>
        public string cdefine10
        {
            get { return m_cdefine10; }
            set { m_cdefine10 = value; }
        }

        #endregion

        private List<SaleOutRedDetail> m_U8Details;
        public List<SaleOutRedDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        private List<SaleOutRedDetail> m_OperateDetails;

        public List<SaleOutRedDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }

        #region 构造函数
        public SaleOutRedList()
        { }

        #endregion
    }
}
