using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ArrivalVouch
    {

        #region AVDetailList
        public ArrivalVouch()
        {
            U8Details = new List<ArrivalVouchs>();
            OperateDetails = new List<ArrivalVouchs>();
        }

        private List<ArrivalVouchs> m_arrU8Details;
        /// <summary>
        /// 来源数据
        /// </summary>
        public List<ArrivalVouchs> U8Details
        {
            get { return m_arrU8Details; }
            set { m_arrU8Details = value; }
        }

        private List<ArrivalVouchs> m_arrOperateDetails;
        /// <summary>
        /// 已操作数据
        /// </summary>
        public List<ArrivalVouchs> OperateDetails
        {
            get { return m_arrOperateDetails; }
            set { m_arrOperateDetails = value; }
        }

        #endregion
        #region 成员属性

        private int _iVTid;
        public int VT_ID
        {
            set{ _iVTid = value; }

            get{ return _iVTid; }
        }

        private string _ufts;
        /// <summary>
        /// 时间戳 
        /// </summary>
        public string ufts
        {
            set{ _ufts = value; }

            get{ return _ufts; }
        }

        private int _ID;
        /// <summary>
        /// 主表标识
        /// </summary>
        public int ID
        {
            set{ _ID = value; }

            get{ return _ID; }
        }

        private string _cCode;
        /// <summary>
        /// 到货单号 
        /// </summary>
        public string cCode
        {
            set{ _cCode = value; }

            get{ return _cCode; }
        }

        private string _cPTCode;
        /// <summary>
        /// 采购类型编码 
        /// </summary>
        public string cPTCode
        {
            set{ _cPTCode = value; }

            get{ return _cPTCode; }
        }

        private string _dDate;
        /// <summary>
        /// 制单日期
        /// </summary>
        public string dDate
        {
            set{ _dDate = value; }

            get{ return _dDate; }
        }

        private string _cVenCode;
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cVenCode
        {
            set{ _cVenCode = value; }

            get{ return _cVenCode; }
        }

        private string _cDepCode;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode
        {
            set{ _cDepCode = value; }

            get{ return _cDepCode; }
        }

        private string _cPersonCode;
        /// <summary>
        /// 业务员编码 
        /// </summary>
        public string cPersonCode
        {
            set{ _cPersonCode = value; }

            get{ return _cPersonCode; }
        }

        private string _cPayCode;
        /// <summary>
        /// 付款条件编码 
        /// </summary>
        public string cPayCode
        {
            set{ _cPayCode = value; }

            get{ return _cPayCode; }
        }

        private string _cSCCode;
        /// <summary>
        /// 发运方式编码 
        /// </summary>
        public string cSCCode
        {
            set{ _cSCCode = value; }

            get{ return _cSCCode; }
        }

        private string _cexch_name;
        /// <summary>
        /// 币种名称 
        /// </summary>
        public string cExch_Name
        {
            set{ _cexch_name = value; }

            get{ return _cexch_name; }
        }

        private decimal _iExchRate;
        /// <summary>
        /// 汇率 
        /// </summary>
        public decimal iExchRate
        {
            set{ _iExchRate = value; }

            get{ return _iExchRate; }
        }

        private decimal _iTaxRate;
        /// <summary>
        /// 表关税率
        /// </summary>
        public decimal iTaxRate
        {
            set{ _iTaxRate = value; }

            get{ return _iTaxRate; }
        }

        private string _cMemo;
        public string cMemo
        {
            set{ _cMemo = value; }

            get{ return _cMemo; }
        }

        private string _cBusType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string cBusType
        {
            set{ _cBusType = value; }

            get{ return _cBusType; }
        }

        private string _cMaker;
        /// <summary>
        /// 制单人
        /// </summary>
        public string cMaker
        {
            set{ _cMaker = value; }

            get{ return _cMaker; }
        }

        private int _bNegative;
        public int bNegative
        {
            set{ _bNegative = value; }

            get{ return _bNegative; }
        }

        private string _cDefine1;
        public string Define1
        {
            set{ _cDefine1 = value; }

            get{ return _cDefine1; }
        }

        private string _cDefine2;
        public string Define2
        {
            set{ _cDefine2 = value; }

            get{ return _cDefine2; }
        }

        private string _cDefine3;
        public string Define3
        {
            set{ _cDefine3 = value; }

            get{ return _cDefine3; }
        }

        private DateTime _cDefine4;
        public DateTime Define4
        {
            set{ _cDefine4 = value; }

            get{ return _cDefine4; }
        }

        private int _cDefine5;
        public int Define5
        {
            set{ _cDefine5 = value; }

            get{ return _cDefine5; }
        }

        private DateTime _cDefine6;
        public DateTime Define6
        {
            set{ _cDefine6 = value; }

            get{ return _cDefine6; }
        }

        private decimal _cDefine7;
        public decimal Define7
        {
            set{ _cDefine7 = value; }

            get{ return _cDefine7; }
        }

        private string _cDefine8;
        public string Define8
        {
            set{ _cDefine8 = value; }

            get{ return _cDefine8; }
        }

        private string _cDefine9;
        public string Define9
        {
            set{ _cDefine9 = value; }

            get{ return _cDefine9; }
        }

        private string _cDefine10;
        public string Define10
        {
            set{ _cDefine10 = value; }

            get{ return _cDefine10; }
        }

        private string _cDefine11;
        public string Define11
        {
            set{ _cDefine11 = value; }

            get{ return _cDefine11; }
        }

        private string _cDefine12;
        public string Define12
        {
            set{ _cDefine12 = value; }

            get{ return _cDefine12; }
        }

        private string _cDefine13;
        public string Define13
        {
            set{ _cDefine13 = value; }

            get{ return _cDefine13; }
        }

        private string _cDefine14;
        public string Define14
        {
            set{ _cDefine14 = value; }

            get{ return _cDefine14; }
        }

        private int _cDefine15;
        public int Define15
        {
            set{ _cDefine15 = value; }

            get{ return _cDefine15; }
        }

        private decimal _cDefine16;
        public decimal Define16
        {
            set{ _cDefine16 = value; }

            get{ return _cDefine16; }
        }

        private string _ccloser;
        public string cCloser
        {
            set{ _ccloser = value; }

            get{ return _ccloser; }
        }

        private string _iDiscountTaxType;
        public string iDiscountTaxType
        {
            set{ _iDiscountTaxType = value; }

            get{ return _iDiscountTaxType; }
        }

        private string _iBillType;
        public string iBillType
        {
            set{ _iBillType = value; }

            get{ return _iBillType; }
        }

        private string _cvouchtype;
        /// <summary>
        /// 单据类型 
        /// </summary>
        public string cVouchType
        {
            set{ _cvouchtype = value; }

            get{ return _cvouchtype; }
        }

        private string _cgeneralordercode;
        public string cGeneralOrderCode
        {
            set{ _cgeneralordercode = value; }

            get{ return _cgeneralordercode; }
        }

        private string _ctmcode;
        public string cTmCode
        {
            set{ _ctmcode = value; }

            get{ return _ctmcode; }
        }

        private string _cincotermcode;
        public string cIncotermCode
        {
            set{ _cincotermcode = value; }

            get{ return _cincotermcode; }
        }

        private string _ctransordercode;
        public string cTransOrderCode
        {
            set{ _ctransordercode = value; }

            get{ return _ctransordercode; }
        }

        private string _dportdate;
        public string dPortDate
        {
            set{ _dportdate = value; }

            get{ return _dportdate; }
        }

        private string _csportcode;
        public string cSportCode
        {
            set{ _csportcode = value; }

            get{ return _csportcode; }
        }

        private string _caportcode;
        public string cAportCode
        {
            set{ _caportcode = value; }

            get{ return _caportcode; }
        }

        private string _csvencode;
        public string cSvenCode
        {
            set{ _csvencode = value; }

            get{ return _csvencode; }
        }

        private string _carrivalplace;
        public string cArrivalPlace
        {
            set{ _carrivalplace = value; }

            get{ return _carrivalplace; }
        }

        private string _dclosedate;
        public string dCloseDate
        {
            set{ _dclosedate = value; }

            get{ return _dclosedate; }
        }

        private int _idec;
        public int iDec
        {
            set{ _idec = value; }

            get{ return _idec; }
        }

        private bool _bcal;
        public bool bCal
        {
            set{ _bcal = value; }

            get{ return _bcal; }
        }

        private string _guid;
        /// <summary>
        /// 唯一标识 
        /// </summary>
        public string Guid
        {
            set{ _guid = value; }

            get{ return _guid; }
        }

        private string _cMakeTime;
        /// <summary>
        /// 制单时间
        /// </summary>
        public string cMakeTime
        {
            set{ _cMakeTime = value; }

            get{ return _cMakeTime; }
        }

        private string _cModifyTime;
        public string cModifyTime
        {
            set{ _cModifyTime = value; }

            get{ return _cModifyTime; }
        }

        private string _cModifyDate;
        public string cModifyDate
        {
            set{ _cModifyDate = value; }

            get{ return _cModifyDate; }
        }

        private string _cReviser;
        public string cReviser
        {
            set{ _cReviser = value; }

            get{ return _cReviser; }
        }

        private int _iverifystate;
        public int iVerifyState
        {
            set{ _iverifystate = value; }

            get{ return _iverifystate; }
        }

        private string _cAuditDate;
        public string cAuditDate
        {
            set{ _cAuditDate = value; }

            get{ return _cAuditDate; }
        }

        private string _caudittime;
        public string cAuditTime
        {
            set{ _caudittime = value; }

            get{ return _caudittime; }
        }

        private string _cverifier;
        /// <summary>
        /// 审核人
        /// </summary>
        public string cVerifier
        {
            set{ _cverifier = value; }

            get{ return _cverifier; }
        }

        private int _iverifystateex;
        public int iVerifyStateex
        {
            set{ _iverifystateex = value; }

            get{ return _iverifystateex; }
        }

        private int _ireturncount;
        public int iReturnCount
        {
            set{ _ireturncount = value; }

            get{ return _ireturncount; }
        }

        private bool _IsWfControlled;
        public bool isWfContRolled
        {
            set{ _IsWfControlled = value; }

            get{ return _IsWfControlled; }
        }

        private string _cVenPUOMProtocol;
        public string cVenPUOMProtocol
        {
            set{ _cVenPUOMProtocol = value; }

            get{ return _cVenPUOMProtocol; }
        }

        private string _cchanger;
        public string cChanger
        {
            set{ _cchanger = value; }

            get{ return _cchanger; }
        }

        private int _iflowid;
        public int iFlowId
        {
            set{ _iflowid = value; }

            get{ return _iflowid; }
        }

        #endregion

        #region Info

        private string _cPTName;

        public string cPTName
        {
            get { return _cPTName; }
            set { _cPTName = value; }
        }

        private string _cVenName;

        public string cVenName
        {
            get { return _cVenName; }
            set { _cVenName = value; }
        }

        private string _cDepName;

        public string cDepName
        {
            get { return _cDepName; }
            set { _cDepName = value; }
        }

        private string _cPsn_Name;

        public string cPsn_Name
        {
            get { return _cPsn_Name; }
            set { _cPsn_Name = value; }
        }

        private string _cAddress;

        public string cAddress
        {
            get { return _cAddress; }
            set { _cAddress = value; }
        }

        private bool _bIsOut;
        /// <summary>
        /// 是否出库 
        /// </summary>
        public bool bIsOut
        {
            get { return _bIsOut; }
            set { _bIsOut = value; }
        }

        private string _cSaveVouch;
        /// <summary>
        /// 提交参数
        /// </summary>
        public string cSaveVouch
        {
            get { return _cSaveVouch; }
            set { _cSaveVouch = value; }
        }

        #endregion

        /// <summary>
        /// 采购订单号
        /// </summary>
        public string cOrderCode
        {
            get;
            set;
        }

        public string cVenAbbName
        {
            get;
            set;
        }

        public string cPersonName
        {
            get;
            set;
        }
    }
}
