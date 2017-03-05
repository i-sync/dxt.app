using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ArrivalVouchs
    {
        public ArrivalVouchs()
        {
            this.iNum = 0;
            this.Quantity = 0;
        }

        public ArrivalVouchs getNewDetail()
        {
            ArrivalVouchs avs = (ArrivalVouchs)this.MemberwiseClone();
            return avs;
        }


        #region 成员属性

        private int _Autoid;
        /// <summary>
        /// 子表标识
        /// </summary>
        public int Autoid
        {
            set { _Autoid = value; }

            get { return _Autoid; }
        }

        private int _ID;
        /// <summary>
        /// 主表标识
        /// </summary>
        public int ID
        {
            set { _ID = value; }

            get { return _ID; }
        }

        private string _cWhCode;
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string cWhCode
        {
            set { _cWhCode = value; }

            get { return _cWhCode; }
        }

        private string _cInvCode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode
        {
            set { _cInvCode = value; }

            get { return _cInvCode; }
        }

        private decimal _iNum;
        /// <summary>
        /// 辅计量数量 
        /// </summary>
        public decimal iNum
        {
            set { _iNum = value; }

            get { return _iNum; }
        }

        private decimal _iQuantity;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity
        {
            set { _iQuantity = value; }

            get { return _iQuantity; }
        }

        /// <summary>
        /// 扫描数量
        /// </summary>
        public decimal iScanQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// 到货数量
        /// </summary>
        public decimal iArrQty
        { get; set; }

        private decimal _iOriCost;
        /// <summary>
        /// 原币无税单价
        /// </summary>
        public decimal iOriCost
        {
            set { _iOriCost = value; }

            get { return _iOriCost; }
        }

        private decimal _iOriTaxCost;
        public decimal iOriTaxCost
        {
            set { _iOriTaxCost = value; }

            get { return _iOriTaxCost; }
        }

        private decimal _iOriMoney;
        /// <summary>
        /// 原币无税金额
        /// </summary>
        public decimal iOriMoney
        {
            set { _iOriMoney = value; }

            get { return _iOriMoney; }
        }

        private decimal _iOriTaxPrice;
        /// <summary>
        /// 原币税额
        /// </summary>
        public decimal iOriTaxPrice
        {
            set { _iOriTaxPrice = value; }

            get { return _iOriTaxPrice; }
        }

        private decimal _iOriSum;
        /// <summary>
        /// 原币价税合计
        /// </summary>
        public decimal iOriSum
        {
            set { _iOriSum = value; }

            get { return _iOriSum; }
        }

        private decimal _iCost;
        /// <summary>
        /// 本币无税单价
        /// </summary>
        public decimal iCost
        {
            set { _iCost = value; }

            get { return _iCost; }
        }

        private decimal _iTax;
        /// <summary>
        /// 原币税额
        /// </summary>
        public decimal iTax
        {
            set { _iTax = value; }

            get { return _iTax; }
        }

        private decimal _iMoney;
        /// <summary>
        ///原币无税金额
        /// </summary>
        public decimal iMoney
        {
            set { _iMoney = value; }

            get { return _iMoney; }
        }

        private decimal _iTaxPrice;
        /// <summary>
        /// 原币含税单价
        /// </summary>
        public decimal iTaxPrice
        {
            set { _iTaxPrice = value; }

            get { return _iTaxPrice; }
        }

        private decimal _iSum;
        /// <summary>
        /// 原币价税合计 
        /// </summary>
        public decimal iSum
        {
            set { _iSum = value; }

            get { return _iSum; }
        }

        private string _cFree1;
        public string Free1
        {
            set { _cFree1 = value; }

            get { return _cFree1; }
        }

        private string _cFree2;
        public string Free2
        {
            set { _cFree2 = value; }

            get { return _cFree2; }
        }

        private string _cFree3;
        public string Free3
        {
            set { _cFree3 = value; }

            get { return _cFree3; }
        }

        private string _cFree4;
        public string Free4
        {
            set { _cFree4 = value; }

            get { return _cFree4; }
        }

        private string _cFree5;
        public string Free5
        {
            set { _cFree5 = value; }

            get { return _cFree5; }
        }

        private string _cFree6;
        public string Free6
        {
            set { _cFree6 = value; }

            get { return _cFree6; }
        }

        private string _cFree7;
        public string Free7
        {
            set { _cFree7 = value; }

            get { return _cFree7; }
        }

        private string _cFree8;
        public string Free8
        {
            set { _cFree8 = value; }

            get { return _cFree8; }
        }

        private string _cFree9;
        public string Free9
        {
            set { _cFree9 = value; }

            get { return _cFree9; }
        }

        private string _cFree10;
        public string Free10
        {
            set { _cFree10 = value; }

            get { return _cFree10; }
        }

        private decimal _iTaxRate;
        /// <summary>
        /// 税率
        /// </summary>
        public decimal iTaxRate
        {
            set { _iTaxRate = value; }

            get { return _iTaxRate; }
        }

        private string _cDefine22;
        public string Define22
        {
            set { _cDefine22 = value; }

            get { return _cDefine22; }
        }

        private string _cDefine23;
        /// <summary>
        /// 中成药生产日期
        /// </summary>
        public string Define23
        {
            set { _cDefine23 = value; }

            get { return _cDefine23; }
        }

        private string _cDefine24;
        public string Define24
        {
            set { _cDefine24 = value; }

            get { return _cDefine24; }
        }

        private string _cDefine25;
        public string Define25
        {
            set { _cDefine25 = value; }

            get { return _cDefine25; }
        }

        private decimal _cDefine26;
        public decimal Define26
        {
            set { _cDefine26 = value; }

            get { return _cDefine26; }
        }

        private decimal _cDefine27;
        public decimal Define27
        {
            set { _cDefine27 = value; }

            get { return _cDefine27; }
        }

        private string _cDefine28;
        public string Define28
        {
            set { _cDefine28 = value; }

            get { return _cDefine28; }
        }

        private string _cDefine29;
        public string Define29
        {
            set { _cDefine29 = value; }

            get { return _cDefine29; }
        }

        private string _cDefine30;
        public string Define30
        {
            set { _cDefine30 = value; }

            get { return _cDefine30; }
        }

        private string _cDefine31;
        public string Define31
        {
            set { _cDefine31 = value; }

            get { return _cDefine31; }
        }

        private string _cDefine32;
        public string Define32
        {
            set { _cDefine32 = value; }

            get { return _cDefine32; }
        }

        private string _cDefine33;
        public string Define33
        {
            set { _cDefine33 = value; }

            get { return _cDefine33; }
        }

        private int _cDefine34;
        public int Define34
        {
            set { _cDefine34 = value; }

            get { return _cDefine34; }
        }

        private int _cDefine35;
        public int Define35
        {
            set { _cDefine35 = value; }

            get { return _cDefine35; }
        }

        private DateTime _cDefine36;
        public DateTime Define36
        {
            set { _cDefine36 = value; }

            get { return _cDefine36; }
        }

        private DateTime _cDefine37;
        public DateTime Define37
        {
            set { _cDefine37 = value; }

            get { return _cDefine37; }
        }

        private string _cItem_class;
        public string cItem_class
        {
            set { _cItem_class = value; }

            get { return _cItem_class; }
        }

        private string _cItemCode;
        public string cItemCode
        {
            set { _cItemCode = value; }

            get { return _cItemCode; }
        }

        private int _iPOsID;
        public int iPOsID
        {
            set { _iPOsID = value; }

            get { return _iPOsID; }
        }

        private string _cItemName;
        public string cItemName
        {
            set { _cItemName = value; }

            get { return _cItemName; }
        }

        private string _cUnitID;
        public string cUnitID
        {
            set { _cUnitID = value; }

            get { return _cUnitID; }
        }

        private decimal _fValidInQuan;
        public decimal fValidInQuan
        {
            set { _fValidInQuan = value; }

            get { return _fValidInQuan; }
        }

        private decimal _fKPQuantity;
        public decimal fKPQuantity
        {
            set { _fKPQuantity = value; }

            get { return _fKPQuantity; }
        }

        private decimal _fRealQuantity;
        /// <summary>
        /// 实收数量 
        /// </summary>
        public decimal fRealQuantity
        {
            set { _fRealQuantity = value; }

            get { return _fRealQuantity; }
        }

        private decimal _fValidQuantity;
        /// <summary>
        /// 合格数量 
        /// </summary>
        public decimal fValidQuantity
        {
            set { _fValidQuantity = value; }

            get { return _fValidQuantity; }
        }

        private decimal _finValidQuantity;
        public decimal finValidQuantity
        {
            set { _finValidQuantity = value; }

            get { return _finValidQuantity; }
        }

        private string _cCloser;
        public string cCloser
        {
            set { _cCloser = value; }

            get { return _cCloser; }
        }

        private int _iCorId;
        public int iCorId
        {
            set { _iCorId = value; }

            get { return _iCorId; }
        }

        private decimal _fRetQuantity;
        public decimal fRetQuantity
        {
            set { _fRetQuantity = value; }

            get { return _fRetQuantity; }
        }

        private decimal _fInValidInQuan;
        public decimal fInValidInQuan
        {
            set { _fInValidInQuan = value; }

            get { return _fInValidInQuan; }
        }

        private bool _bGsp;
        /// <summary>
        /// 是否质检
        /// </summary>
        public bool bGsp
        {
            set { _bGsp = value; }

            get { return _bGsp; }
        }

        /// <summary>
        /// 是否质检显示字段
        /// </summary>
        public string cGsp
        {
            get
            {
                return bGsp ? "是" : "否";
            }
        }

        private string _cBatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch
        {
            set { _cBatch = value; }

            get { return _cBatch; }
        }

        private DateTime _dVDate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime dVDate
        {
            set
            {
                _dVDate = value;
            }

            get { return _dVDate; }
        }

        private DateTime _dPDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dPDate
        {
            set
            {
                _dPDate = value;
            }

            get { return _dPDate; }
        }

        private decimal _fRefuseQuantity;
        public decimal fRefuseQuantity
        {
            set { _fRefuseQuantity = value; }

            get { return _fRefuseQuantity; }
        }

        private string _cGspState;
        public string cGspState
        {
            set { _cGspState = value; }

            get { return _cGspState; }
        }

        private bool _bTaxCost;
        public bool bTaxCost
        {
            set { _bTaxCost = value; }

            get { return _bTaxCost; }
        }

        private string _bInspect;
        public string bInspect
        {
            set { _bInspect = value; }

            get { return _bInspect; }
        }

        private decimal _fRefuseNum;
        public decimal fRefuseNum
        {
            set { _fRefuseNum = value; }

            get { return _fRefuseNum; }
        }

        private int _iPPartId;
        public int iPPartId
        {
            set { _iPPartId = value; }

            get { return _iPPartId; }
        }

        private int _iPTOSeq;
        public int iPTOSeq
        {
            set { _iPTOSeq = value; }

            get { return _iPTOSeq; }
        }

        private string _SoDId;
        public string SoDId
        {
            set { _SoDId = value; }

            get { return _SoDId; }
        }

        private int _SoType;
        public int SoType
        {
            set { _SoType = value; }

            get { return _SoType; }
        }

        private string _ContractRowGUID;
        public string ContractRowGUID
        {
            set { _ContractRowGUID = value; }

            get { return _ContractRowGUID; }
        }

        private int _imassdate;
        /// <summary>
        /// 保质期
        /// </summary>
        public int iMassDate
        {
            set { _imassdate = value; }

            get { return _imassdate; }
        }

        private int _cmassunit;
        /// <summary>
        /// 有效期单位
        /// </summary>
        public int cMassUnit
        {
            set { _cmassunit = value; }

            get { return _cmassunit; }
        }

        private string _bexigency;
        public string bExigency
        {
            set { _bexigency = value; }

            get { return _bexigency; }
        }

        private string _cbcloser;
        public string cBcloser
        {
            set { _cbcloser = value; }

            get { return _cbcloser; }
        }

        private decimal _fSumRefuseQuantity;
        public decimal fSumRefuseQuantity
        {
            set { _fSumRefuseQuantity = value; }

            get { return _fSumRefuseQuantity; }
        }

        private decimal _FSumRefuseNum;
        public decimal FSumRefuseNum
        {
            set { _FSumRefuseNum = value; }

            get { return _FSumRefuseNum; }
        }

        private decimal _fRetNum;
        public decimal fRetNum
        {
            set { _fRetNum = value; }

            get { return _fRetNum; }
        }

        private decimal _fDTQuantity;
        public decimal fDTQuantity
        {
            set { _fDTQuantity = value; }

            get { return _fDTQuantity; }
        }

        private decimal _fInvalidInNum;
        public decimal fInvalidInNum
        {
            set { _fInvalidInNum = value; }

            get { return _fInvalidInNum; }
        }

        private decimal _fDegradeQuantity;
        public decimal fDegradeQuantity
        {
            set { _fDegradeQuantity = value; }

            get { return _fDegradeQuantity; }
        }

        private decimal _fDegradeNum;
        public decimal fDegradeNum
        {
            set { _fDegradeNum = value; }

            get { return _fDegradeNum; }
        }

        private decimal _fDegradeInQuantity;
        public decimal fDegradeInQuantity
        {
            set { _fDegradeInQuantity = value; }

            get { return _fDegradeInQuantity; }
        }

        private decimal _fDegradeInNum;
        public decimal fDegradeInNum
        {
            set { _fDegradeInNum = value; }

            get { return _fDegradeInNum; }
        }

        private decimal _fInspectQuantity;
        public decimal fInspectQuantity
        {
            set { _fInspectQuantity = value; }

            get { return _fInspectQuantity; }
        }

        private decimal _fInspectNum;
        public decimal fInspectNum
        {
            set { _fInspectNum = value; }

            get { return _fInspectNum; }
        }

        private decimal _iInvMPCost;
        public decimal iInvMPCost
        {
            set { _iInvMPCost = value; }

            get { return _iInvMPCost; }
        }

        private string _guids;
        public string Guids
        {
            set { _guids = value; }

            get { return _guids; }
        }

        private decimal _iinvexchrate;
        public decimal iInvexchRate
        {
            set { _iinvexchrate = value; }

            get { return _iinvexchrate; }
        }

        private string _objectid_source;
        public string Objectid_Source
        {
            set { _objectid_source = value; }

            get { return _objectid_source; }
        }

        private int _autoid_source;
        public int Autoid_Source
        {
            set { _autoid_source = value; }

            get { return _autoid_source; }
        }

        private string _ufts_source;
        public string Ufts_Source
        {
            set { _ufts_source = value; }

            get { return _ufts_source; }
        }

        private int _irowno_source;
        public int iRowno_Source
        {
            set { _irowno_source = value; }

            get { return _irowno_source; }
        }

        private string _csocode;
        public string cSoCode
        {
            set { _csocode = value; }

            get { return _csocode; }
        }

        private int _isorowno;
        public int iSoRowNo
        {
            set { _isorowno = value; }

            get { return _isorowno; }
        }

        private int _iorderid;
        public int iOrderId
        {
            set { _iorderid = value; }

            get { return _iorderid; }
        }

        private string _cordercode;
        public string cOrderCode
        {
            set { _cordercode = value; }

            get { return _cordercode; }
        }

        private int _iorderrowno;
        public int iOrderRowNo
        {
            set { _iorderrowno = value; }

            get { return _iorderrowno; }
        }

        private string _dlineclosedate;
        public string dLineCloseDate
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    _dlineclosedate = "";
                else
                {
                    try { _dlineclosedate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { _dlineclosedate = ""; }
                }
            }

            get { return _dlineclosedate; }
        }

        private string _ContractCode;
        public string ContractCode
        {
            set { _ContractCode = value; }

            get { return _ContractCode; }
        }

        private string _ContractRowNo;
        public string ContractRowNo
        {
            set { _ContractRowNo = value; }

            get { return _ContractRowNo; }
        }

        private bool _RejectSource;
        public bool RejectSource
        {
            set { _RejectSource = value; }

            get { return _RejectSource; }
        }

        private int _iciqbookid;
        public int iCiqBookId
        {
            set { _iciqbookid = value; }

            get { return _iciqbookid; }
        }

        private string _cciqbookcode;
        public string cCiqBookCode
        {
            set { _cciqbookcode = value; }

            get { return _cciqbookcode; }
        }

        private string _cciqcode;
        public string cCiqCode
        {
            set { _cciqcode = value; }

            get { return _cciqcode; }
        }

        private int _irejectautoid;
        public int iRejectAutoId
        {
            set { _irejectautoid = value; }

            get { return _irejectautoid; }
        }

        private int _iExpiratDateCalcu;
        /// <summary>
        /// 有效期推断方式 
        /// </summary>
        public int iExpiratDateCalcu
        {
            set { _iExpiratDateCalcu = value; }

            get { return _iExpiratDateCalcu; }
        }

        private string _cExpirationdate;
        /// <summary>
        /// 有效期至
        /// </summary>
        public string cExpirationDate
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    _cExpirationdate = "";
                else
                {
                    try { _cExpirationdate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { _cExpirationdate = ""; }
                }
            }

            get { return _cExpirationdate; }
        }

        private DateTime _dExpirationdate;
        /// <summary>
        /// 有效期计算项 
        /// </summary>
        public DateTime dExpirationDate
        {
            set { _dExpirationdate = value; }

            get { return _dExpirationdate; }
        }

        private string _cupsocode;
        public string cUpSoCode
        {
            set { _cupsocode = value; }

            get { return _cupsocode; }
        }

        private int _iorderdid;
        public int iOrderdId
        {
            set { _iorderdid = value; }

            get { return _iorderdid; }
        }

        private int _iordertype;
        public int iOrderType
        {
            set { _iordertype = value; }

            get { return _iordertype; }
        }

        private string _csoordercode;
        public string cSoOrderCode
        {
            set { _csoordercode = value; }

            get { return _csoordercode; }
        }

        private int _iorderseq;
        public int iOrderSeq
        {
            set { _iorderseq = value; }

            get { return _iorderseq; }
        }

        private decimal _cBatchProperty1;
        public decimal BatchProperty1
        {
            set { _cBatchProperty1 = value; }

            get { return _cBatchProperty1; }
        }

        private decimal _cBatchProperty2;
        public decimal BatchProperty2
        {
            set { _cBatchProperty2 = value; }

            get { return _cBatchProperty2; }
        }

        private decimal _cBatchProperty3;
        public decimal BatchProperty3
        {
            set { _cBatchProperty3 = value; }

            get { return _cBatchProperty3; }
        }

        private decimal _cBatchProperty4;
        public decimal BatchProperty4
        {
            set { _cBatchProperty4 = value; }

            get { return _cBatchProperty4; }
        }

        private decimal _cBatchProperty5;
        public decimal BatchProperty5
        {
            set { _cBatchProperty5 = value; }

            get { return _cBatchProperty5; }
        }

        private string _cBatchProperty6;
        public string BatchProperty6
        {
            set { _cBatchProperty6 = value; }

            get { return _cBatchProperty6; }
        }

        private string _cBatchProperty7;
        public string BatchProperty7
        {
            set { _cBatchProperty7 = value; }

            get { return _cBatchProperty7; }
        }

        private string _cBatchProperty8;
        public string BatchProperty8
        {
            set { _cBatchProperty8 = value; }

            get { return _cBatchProperty8; }
        }

        private string _cBatchProperty9;
        public string BatchProperty9
        {
            set { _cBatchProperty9 = value; }

            get { return _cBatchProperty9; }
        }

        private string _cBatchProperty10;
        public string BatchProperty10
        {
            set { _cBatchProperty10 = value; }

            get { return _cBatchProperty10; }
        }

        private int _ivouchrowno;
        public int iVouchRowNo
        {
            set { _ivouchrowno = value; }

            get { return _ivouchrowno; }
        }

        #endregion

        #region Info
        private string _cWhName;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName
        {
            get { return _cWhName; }
            set { _cWhName = value; }
        }

        private string _cInvName;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cInvName
        {
            get { return _cInvName; }
            set { _cInvName = value; }
        }

        private string _cInvStd;
        /// <summary>
        /// 存货规格
        /// </summary>
        public string cInvStd
        {
            get { return _cInvStd; }
            set { _cInvStd = value; }
        }

        private string _cComUnitCode;

        public string cComUnitCode
        {
            get { return _cComUnitCode; }
            set { _cComUnitCode = value; }
        }

        private string _bGspStr;
        /// <summary>
        /// 是否质检显示字段
        /// </summary>
        public string bGspStr
        {
            get { return _bGspStr; }
            set { _bGspStr = value; }
        }

        private string _cAddress;
        /// <summary>
        /// 产地
        /// </summary>
        public string cAddress
        {
            get { return _cAddress; }
            set { _cAddress = value; }
        }

        private decimal _nQuantity;
        /// <summary>
        /// 待扫描数量
        /// </summary>
        public decimal nQuantity
        {
            get { return _nQuantity; }
            set { _nQuantity = value; }
        }

        private string _cVenCode;
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string cVenCode
        {
            get { return _cVenCode; }
            set { _cVenCode = value; }
        }

        private string _cVenName;
        /// <summary>
        /// 供应商全称
        /// </summary>
        public string cVenName
        {
            get { return _cVenName; }
            set { _cVenName = value; }
        }

        private string _cVenAbbName;
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string cVenAbbName
        {
            get { return _cVenAbbName; }
            set { _cVenAbbName = value; }
        }

        private string _cInvm_Unit;
        /// <summary>
        /// 计量单位
        /// </summary>
        public string cInvm_Unit
        {
            get { return _cInvm_Unit; }
            set { _cInvm_Unit = value; }
        }

        private string _dArriveDate;
        /// <summary>
        /// 预计到货日期
        /// </summary>
        public string dArriveDate
        {
            get { return _dArriveDate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _dArriveDate = "";
                else
                {
                    try { _dArriveDate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { _dArriveDate = ""; }
                }
            }
        }

        private string _cDemandMemo;

        public string cDemandMemo
        {
            get { return _cDemandMemo; }
            set { _cDemandMemo = value; }
        }

        private bool _bInvBatch;
        /// <summary>
        /// 是否批次管理
        /// </summary>
        public bool bInvBatch
        {
            get { return _bInvBatch; }
            set { _bInvBatch = value; }
        }


        private decimal _fOrderQuantity;
        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal OrderQuantity
        {
            get { return _fOrderQuantity; }
            set { _fOrderQuantity = value; }
        }

        private string _cMoDetailsId;
        /// <summary>
        /// 委外订单子表ID
        /// </summary>
        public string cMoDetailsID
        {
            get { return _cMoDetailsId; }
            set { _cMoDetailsId = value; }
        }

        #endregion

        public decimal iExchRate
        { get; set; }
        /// <summary>
        /// 原币无税单价
        /// </summary>
        public decimal iunitprice
        { get; set; }

        public decimal inatunitprice
        {
            get;
            set;
        }

        public decimal inatmoney
        {
            get;
            set;
        }

        public decimal inattax
        { get; set; }
        public decimal inatsum
        { get; set; }

        public string cinvdefine1 { get; set; }
        public string cinvdefine2 { get; set; }
        public string cinvdefine3 { get; set; }
        public string cinvdefine4 { get; set; }
        public string cinvdefine5 { get; set; }
        public string cinvdefine6 { get; set; }
        public string cinvdefine7 { get; set; }
        public string cinvdefine8 { get; set; }
        public string cinvdefine9 { get; set; }
        public string cinvdefine10 { get; set; }
        public string cinvdefine11 { get; set; }
        public string cinvdefine12 { get; set; }
        public string cinvdefine13 { get; set; }
        public string cinvdefine14 { get; set; }
        public string cinvdefine15 { get; set; }
        public string cinvdefine16 { get; set; }

        /// <summary>
        /// 是否保质期管理 
        /// </summary>
        public bool bInvQuality { get; set; }
    }
}
