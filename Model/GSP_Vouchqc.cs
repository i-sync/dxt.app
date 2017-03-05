using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    //药品入库质量验收记录单主表

    public class GSP_Vouchqc
    {
        /*
        select v.ID 主表标识,v.QCID 质量验收记录单号,v.ICODE 采购到货退货单主表标识,
v.CCODE 采购到货退货单号,v.DARVDATE 到货退货日期,v.CVERIFIER 审核人,
v.CMAKER 制单人,v.DDATE 单据日期,v.CVOUCHTYPE 单据类型编码,
v.IVTID 单据模版号,v.UFTS 时间戳,v.CDEFINE1,v.BREFER 是否参照,
v.IVERIFYSTATE 审批标志 from dbo.GSP_VOUCHQC v
where v.QCID ='113201001030002'
         */
        #region
        private int _ID;
        /// <summary>
        /// 药品入库质量验收记录单主表标识
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string qCID;
        /// <summary>
        /// 质量验收记录单号
        /// </summary>
        public string QCID
        {
            get { return qCID; }
            set { qCID = value; }
        }

        private int iCODE;
        /// <summary>
        /// 采购到货退货单主表标识
        /// </summary>
        public int ICODE
        {
            get { return iCODE; }
            set { iCODE = value; }
        }

        private string cCODE;
        /// <summary>
        /// 采购到货退货单号
        /// </summary>
        public string CCODE
        {
            get { return cCODE; }
            set { cCODE = value; }
        }
        #endregion
        #region
        private DateTime dARVDATE;
        /// <summary>
        /// 到货退货日期
        /// </summary>
        public DateTime DARVDATE
        {
            get { return dARVDATE; }
            set { dARVDATE = value; }
        }

        private string cVERIFIER;
        /// <summary>
        /// 审核人
        /// </summary>
        public string CVERIFIER
        {
            get { return cVERIFIER; }
            set { cVERIFIER = value; }
        }

        private string cMAKER;
        /// <summary>
        /// 制单人
        /// </summary>
        public string CMAKER
        {
            get { return cMAKER; }
            set { cMAKER = value; }
        }

        private DateTime dDATE;
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime DDATE
        {
            get { return dDATE; }
            set { dDATE = value; }
        }

        private string cVOUCHTYPE;
        /// <summary>
        /// 单据类型编码
        /// </summary>
        public string CVOUCHTYPE
        {
            get { return cVOUCHTYPE; }
            set { cVOUCHTYPE = value; }
        }
        #endregion
        private int iVTID;
        /// <summary>
        /// 单据模版号
        /// </summary>
        public int IVTID
        {
            get { return iVTID; }
            set { iVTID = value; }
        }

        private string cDEFINE1;
        /// <summary>
        /// 自定义项1
        /// </summary>
        public string CDEFINE1
        {
            get { return cDEFINE1; }
            set { cDEFINE1 = value; }
        }

        private bool bREFER;
        /// <summary>
        /// 是否参照
        /// </summary>
        public bool BREFER
        {
            get { return bREFER; }
            set { bREFER = value; }
        }

        private int iVERIFYSTATE;
        /// <summary>
        /// 审批标志
        /// </summary>
        public int IVERIFYSTATE
        {
            get { return iVERIFYSTATE; }
            set { iVERIFYSTATE = value; }
        }
    }
}
