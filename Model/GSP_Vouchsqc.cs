using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    //药品入库质量验收记录单子表

    public class GSP_Vouchsqc
    {
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

        
        #region
        private int aUTOID;
        /// <summary>
        /// 药品入库质量验收记录单子表标识
        /// </summary>
        public int AUTOID
        {
            get { return aUTOID; }
            set { aUTOID = value; }
        }

        private int iD;
        /// <summary>
        /// 主表标识
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
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

        private string cINVNAME;
        /// <summary>
        /// 药品名称....
        /// </summary>
        public string CINVNAME
        {
            get { return cINVNAME; }
            set { cINVNAME = value; }
        }

        private string cINVCODE;
        /// <summary>
        /// 药品编号
        /// </summary>
        public string CINVCODE
        {
            get { return cINVCODE; }
            set { cINVCODE = value; }
        }

        private float fQUANTITY;
        /// <summary>
        /// 实收数
        /// </summary>
        public float FQUANTITY
        {
            get { return fQUANTITY; }
            set { fQUANTITY = value; }
        }
#endregion
        #region
        private float fARVQUANTITY;
        /// <summary>
        /// 到货数
        /// </summary>
        public float FARVQUANTITY
        {
            get { return fARVQUANTITY; }
            set { fARVQUANTITY = value; }
        }

        private DateTime dPRODATE;
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime DPRODATE
        {
            get { return dPRODATE; }
            set { dPRODATE = value; }
        }

        private string cVALDATE;
        /// <summary>
        /// 有效期
        /// </summary>
        public string CVALDATE
        {
            get { return cVALDATE; }
            set { cVALDATE = value; }
        }
        #endregion
        #region
        private string cVenName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string CVenName
        {
            get { return cVenName; }
            set { cVenName = value; }
        }

        private DateTime dDATE_T;
        /// <summary>
        /// 退货日期
        /// </summary>
        public DateTime DDATE_T
        {
            get { return dDATE_T; }
            set { dDATE_T = value; }
        }

        private string cOUTINSTANCE;
        /// <summary>
        /// 外观质量情况
        /// </summary>
        public string COUTINSTANCE
        {
            get { return cOUTINSTANCE; }
            set { cOUTINSTANCE = value; }
        }

        private string cCONCLUSION;
        /// <summary>
        /// 验收结论
        /// </summary>
        public string CCONCLUSION
        {
            get { return cCONCLUSION; }
            set { cCONCLUSION = value; }
        }

        private float fELGQUANTITY;
        /// <summary>
        /// 合格数量
        /// </summary>
        public float FELGQUANTITY
        {
            get { return fELGQUANTITY; }
            set { FELGQUANTITY = value; }
        }

        private float fNELGQUANTITY;
        /// <summary>
        /// 不合格数量
        /// </summary>
        public float FNELGQUANTITY
        {
            get { return fNELGQUANTITY; }
            set { fNELGQUANTITY = value; }
        }

        private string cBACKREASON;
        /// <summary>
        /// 拒收理由
        /// </summary>
        public string CBACKREASON
        {
            get { return cBACKREASON; }
            set { cBACKREASON = value; }
        }

        private string cBATCH;
        /// <summary>
        /// 生产批号
        /// </summary>
        public string CBATCH
        {
            get { return cBATCH; }
            set { cBATCH = value; }
        }
        #endregion
        private float fPRICE;
        /// <summary>
        /// 单价
        /// </summary>
        public float FPRICE
        {
            get { return fPRICE; }
            set { fPRICE = value; }
        }

        private string cDEFINE22;
        /// <summary>
        /// 定自义字段(存的产地?)
        /// </summary>
        public string CDEFINE22
        {
            get { return cDEFINE22; }
            set { cDEFINE22 = value; }
        }

        private string iCODE_T;
        /// <summary>
        /// 采购到货退货单号
        /// </summary>
        public string ICODE_T
        {
            get { return iCODE_T; }
            set { iCODE_T = value; }
        }

        private string bCHECK;
        /// <summary>
        /// 是否抽检
        /// </summary>
        public string BCHECK
        {
            get { return bCHECK; }
            set { bCHECK = value; }
        }

        private int imassDate;
        /// <summary>
        /// 保质期
        /// </summary>
        public int ImassDate
        {
            get { return imassDate; }
            set { imassDate = value; }
        }

        private string cMassUnit;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string CMassUnit
        {
            get { return cMassUnit; }
            set { cMassUnit = value; }
        }
    }
}
