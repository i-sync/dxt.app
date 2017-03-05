using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class STInProduct
    {
        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        private int m_id;
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// brdflag
        /// </summary>
        private int m_brdflag;
        public int brdflag
        {
            get { return m_brdflag; }
            set { m_brdflag = value; }
        }

        /// <summary>
        /// cvouchtype
        /// </summary>
        private string m_cvouchtype;
        public string cvouchtype
        {
            get { return m_cvouchtype; }
            set { m_cvouchtype = value; }
        }

        /// <summary>
        /// cbustype
        /// </summary>
        private string m_cbustype;
        public string cbustype
        {
            get { return m_cbustype; }
            set { m_cbustype = value; }
        }

        /// <summary>
        /// csource
        /// </summary>
        private string m_csource;
        public string csource
        {
            get { return m_csource; }
            set { m_csource = value; }
        }

        /// <summary>
        /// 仓库编码
        /// </summary>
        private string m_cwhcode;
        public string cwhcode
        {
            get { return m_cwhcode; }
            set { m_cwhcode = value; }
        }

        /// <summary>
        /// 仓库名称
        /// </summary>
        private string m_cwhname;
        public string cwhname
        {
            get { return m_cwhname; }
            set { m_cwhname = value; }
        }

        /// <summary>
        /// 单据日期
        /// </summary>
        private string m_ddate;
        public string ddate
        {
            get { return m_ddate; }
            set { m_ddate = value; }
        }

        /// <summary>
        /// 单据号
        /// </summary>
        private string m_ccode;
        public string ccode
        {
            get { return m_ccode; }
            set { m_ccode = value; }
        }

        /// <summary>
        /// crdcode
        /// </summary>
        private string m_crdcode;
        public string crdcode
        {
            get { return m_crdcode; }
            set { m_crdcode = value; }
        }

        /// <summary>
        /// cmaker
        /// </summary>
        private string m_cmaker;
        public string cmaker
        {
            get { return m_cmaker; }
            set { m_cmaker = value; }
        }

        /// <summary>
        /// 流通监管码
        /// </summary>
        private string m_cdefine10;
        public string cdefine10
        {
            get { return m_cdefine10; }
            set { m_cdefine10 = value; }
        }

        /// <summary>
        /// bpufirst
        /// </summary>
        private int m_bpufirst;
        public int bpufirst
        {
            get { return m_bpufirst; }
            set { m_bpufirst = value; }
        }

        /// <summary>
        /// biafirst
        /// </summary>
        private int m_biafirst;
        public int biafirst
        {
            get { return m_biafirst; }
            set { m_biafirst = value; }
        }

        /// <summary>
        /// vt_id
        /// </summary>
        private int m_vt_id;
        public int vt_id
        {
            get { return m_vt_id; }
            set { m_vt_id = value; }
        }

        /// <summary>
        /// bisstqc
        /// </summary>
        private int m_bisstqc;
        public int bisstqc
        {
            get { return m_bisstqc; }
            set { m_bisstqc = value; }
        }

        /// <summary>
        /// iproorderid
        /// </summary>
        private int m_iproorderid;
        public int iproorderid
        {
            get { return m_iproorderid; }
            set { m_iproorderid = value; }
        }

        /// <summary>
        /// iswfcontrolled
        /// </summary>
        private int m_iswfcontrolled;
        public int iswfcontrolled
        {
            get { return m_iswfcontrolled; }
            set { m_iswfcontrolled = value; }
        }

        /// <summary>
        /// dnmaketime
        /// </summary>
        private DateTime m_dnmaketime;
        public DateTime dnmaketime
        {
            get { return m_dnmaketime; }
            set { m_dnmaketime = value; }
        }
        #endregion

        private List<STInProductDetail> m_U8Details;
        public List<STInProductDetail> U8Details
        {
            get { return m_U8Details; }
            set { m_U8Details = value; }
        }

        private List<STInProductDetail> m_OperateDetails;

        public List<STInProductDetail> OperateDetails
        {
            get { return m_OperateDetails; }
            set { m_OperateDetails = value; }
        }

        public STInProduct()
        {
            this.brdflag = 1;
            this.cvouchtype = "10";
            this.cbustype = "成品入库";
            this.csource = "库存";
            this.crdcode = "102";
            this.bpufirst = 0;
            this.biafirst = 0;
            this.vt_id = 63;
            this.bisstqc = 0;
            this.iproorderid = 0;
            this.iswfcontrolled = 0;

            this.U8Details = new List<STInProductDetail>();
            this.OperateDetails = new List<STInProductDetail>();
        }
    }
}
