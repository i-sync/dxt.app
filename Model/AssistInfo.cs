using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 批次信息类
    /// </summary>
    public class BatchInfo
    {
        #region 批次信息

        private string cWhCode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhCode
        {
            get { return cWhCode; }
            set { cWhCode = value; }
        }
        private string cInvCode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string InvCode
        {
            get { return cInvCode; }
            set { cInvCode = value; }
        }
        private string cBatch;
        /// <summary>
        /// 批次编码
        /// </summary>
        public string Batch
        {
            get { return cBatch; }
            set { cBatch = value; }
        }
        private string cVMIVenCode;
        /// <summary>
        /// 代管商编码
        /// </summary>
        public string VMIVenCode
        {
            get { return cVMIVenCode; }
            set { cVMIVenCode = value; }
        }
        private string cVenName;
        /// <summary>
        /// 代管商名称
        /// </summary>
        public string VenName
        {
            get { return cVenName; }
            set { cVenName = value; }
        }
        private string cVenAbbName;
        /// <summary>
        /// 代管商简称
        /// </summary>
        public string VenAbbName
        {
            get { return cVenAbbName; }
            set { cVenAbbName = value; }
        }
        private decimal iQuantity;
        /// <summary>
        /// 结存数量
        /// </summary>
        public decimal Quantity
        {
            get { return iQuantity; }
            set { iQuantity = value; }
        }
        private string dVDate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string VDate
        {
            get { return dVDate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    dVDate = "";
                else
                {
                    try { dVDate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { dVDate = ""; }
                }
            }
        }
        private string dMdate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string Mdate
        {
            get { return dMdate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    dMdate = "";
                else
                {
                    try { dMdate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { dMdate = ""; }
                }
            }
        }
        private string cExpirationdate;
        /// <summary>
        /// 有效期至
        /// </summary>
        public string Expirationdate
        {
            get { return cExpirationdate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    cExpirationdate = "";
                else
                {
                    try { cExpirationdate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { cExpirationdate = ""; }
                }
            }
        }
        private decimal iMassDate;
        /// <summary>
        /// 保质期天数
        /// </summary>
        public decimal MassDate
        {
            get { return iMassDate; }
            set { iMassDate = value; }
        }
        private int cMassUnit;
        /// <summary>
        /// 保质期单位 
        /// </summary>
        public int MassUnit
        {
            get { return cMassUnit; }
            set { cMassUnit = value; }
        }

        /// <summary>
        /// 用于显示批次和可用数量
        /// </summary>
        public string DisPlayMember
        {
            get
            {
                return string.Format("{0}(可用量:{1:F2})", cBatch, iQuantity);
            }
        }

        #region 自由项
        private string cFree1;

        public string Free1
        {
            get { return cFree1; }
            set { cFree1 = value; }
        }
        private string cFree2;

        public string Free2
        {
            get { return cFree2; }
            set { cFree2 = value; }
        }
        private string cFree3;

        public string Free3
        {
            get { return cFree3; }
            set { cFree3 = value; }
        }
        private string cFree4;

        public string Free4
        {
            get { return cFree4; }
            set { cFree4 = value; }
        }
        private string cFree5;

        public string Free5
        {
            get { return cFree5; }
            set { cFree5 = value; }
        }
        private string cFree6;

        public string Free6
        {
            get { return cFree6; }
            set { cFree6 = value; }
        }
        private string cFree7;

        public string Free7
        {
            get { return cFree7; }
            set { cFree7 = value; }
        }
        private string cFree8;

        public string Free8
        {
            get { return cFree8; }
            set { cFree8 = value; }
        }
        private string cFree9;

        public string Free9
        {
            get { return cFree9; }
            set { cFree9 = value; }
        }
        private string cFree10;

        public string Free10
        {
            get { return cFree10; }
            set { cFree10 = value; }
        }

        #endregion
        #endregion

        #region 内部方法

        /// <summary>
        /// 设置保质期
        /// </summary>
        public void SetMassDate()
        {
            this.MassDate = CalcMonth(Mdate, VDate);
        }

        /// <summary>
        /// 计算月份
        /// </summary>
        /// <param name="prevDate">生产日期</param>
        /// <param name="lastDate">失效日期</param>
        /// <returns></returns>
        private static int CalcMonth(string prevDate, string lastDate)
        {
            int sum = 0;
            try
            {
                DateTime pTime = DateTime.Parse(prevDate);
                DateTime lTime = DateTime.Parse(lastDate);
                sum = 12 * (lTime.Year - pTime.Year) + (lTime.Month - pTime.Month);
                if (sum < 0)
                    sum *= -1;
            }
            catch
            {
                sum = -1;
            }
            return sum;
        }

        #endregion
    }

    /// <summary>
    /// 仓库信息类
    /// </summary>
    public class WareHouseInfo
    {
        #region 仓库信息
        private bool _bRefSelectColumn;
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool RefSelectColumn
        {
            get { return _bRefSelectColumn; }
            set { _bRefSelectColumn = value; }
        }

        private string _cWhCode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhCode
        {
            get { return _cWhCode; }
            set { _cWhCode = value; }
        }

        private string _cWhName;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhName
        {
            get { return _cWhName; }
            set { _cWhName = value; }
        }

        private string _cDepCode;
        /// <summary>
        /// 部门代码
        /// </summary>
        public string DepCode
        {
            get { return _cDepCode; }
            set { _cDepCode = value; }
        }

        private string _cDepName;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName
        {
            get { return _cDepName; }
            set { _cDepName = value; }
        }

        private bool _bWhPos;
        /// <summary>
        /// 是否货位管理
        /// </summary>
        public bool WhPos
        {
            get { return _bWhPos; }
            set { _bWhPos = value; }
        }

        private bool _bFreeze;
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool Freeze
        {
            get { return _bFreeze; }
            set { _bFreeze = value; }
        }

        private bool _bShop;
        /// <summary>
        /// 是否是门店
        /// </summary>
        public bool Shop
        {
            get { return _bShop; }
            set { _bShop = value; }
        }

        #endregion
    }

    /// <summary>
    /// 部门信息类
    /// </summary>
    public class DepartMentInfo
    {
        #region 部门信息
        private bool _bRefSelectColumn;
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool RefSelectColumn
        {
            get { return _bRefSelectColumn; }
            set { _bRefSelectColumn = value; }
        }

        private string _cDepCode;
        /// <summary>
        /// 部门编码 
        /// </summary>
        public string DepCode
        {
            get { return _cDepCode; }
            set { _cDepCode = value; }
        }

        private string _cDepName;
        /// <summary>
        /// 部门名称 
        /// </summary>
        public string DepName
        {
            get { return _cDepName; }
            set { _cDepName = value; }
        }

        private string _cDepPerson;
        /// <summary>
        /// 负责人编码 
        /// </summary>
        public string DepPerson
        {
            get { return _cDepPerson; }
            set { _cDepPerson = value; }
        }

        private decimal _iDepGrade;
        /// <summary>
        /// 编码级次 
        /// </summary>
        public decimal DepGrade
        {
            get { return _iDepGrade; }
            set { _iDepGrade = value; }
        }

        private bool _bDepEnd;
        /// <summary>
        /// 是否末级 
        /// </summary>
        public bool DepEnd
        {
            get { return _bDepEnd; }
            set { _bDepEnd = value; }
        }

        private string _cDepProp;
        /// <summary>
        /// 部门属性
        /// </summary>
        public string DepProp
        {
            get { return _cDepProp; }
            set { _cDepProp = value; }
        }

        private string _cDepPhone;
        /// <summary>
        /// 电话
        /// </summary>
        public string DepPhone
        {
            get { return _cDepPhone; }
            set { _cDepPhone = value; }
        }

        private string _cDepAddress;
        /// <summary>
        /// 地址
        /// </summary>
        public string DepAddress
        {
            get { return _cDepAddress; }
            set { _cDepAddress = value; }
        }

        private string _cDepMemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string DepMemo
        {
            get { return _cDepMemo; }
            set { _cDepMemo = value; }
        }

        private decimal _iCreLine;
        /// <summary>
        /// 信用额度 
        /// </summary>
        public decimal CreLine
        {
            get { return _iCreLine; }
            set { _iCreLine = value; }
        }

        private string _cCreGrade;
        /// <summary>
        /// 信用等级 
        /// </summary>
        public string CreGrade
        {
            get { return _cCreGrade; }
            set { _cCreGrade = value; }
        }

        private decimal _iCreDate;
        /// <summary>
        /// 信用天数
        /// </summary>
        public decimal CreDate
        {
            get { return _iCreDate; }
            set { _iCreDate = value; }
        }

        #endregion
    }

    /// <summary>
    /// 委外信息类
    /// </summary>
    public class Om_MoHeadInfo
    {
        #region 委外信息
        private string _cMoDetailsID;
        /// <summary>
        /// 委外明细ID
        /// </summary>
        public string MoDetailsID
        {
            get { return _cMoDetailsID; }
            set { _cMoDetailsID = value; }
        }

        private string _cCode;
        /// <summary>
        /// 委外订单号
        /// </summary>
        public string Code
        {
            get { return _cCode; }
            set { _cCode = value; }
        }

        private string _dDate;
        /// <summary>
        /// 订单日期
        /// </summary>
        public string Date
        {
            get { return _dDate; }
            set { _dDate = value; }
        }

        private string _cDepCode;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepCode
        {
            get { return _cDepCode; }
            set { _cDepCode = value; }
        }

        private string _cDepName;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName
        {
            get { return _cDepName; }
            set { _cDepName = value; }
        }

        private string _cVenCode;
        /// <summary>
        /// 委外商编码
        /// </summary>
        public string VenCode
        {
            get { return _cVenCode; }
            set { _cVenCode = value; }
        }

        private string _cVenName;
        /// <summary>
        /// 委外商全称
        /// </summary>
        public string VenName
        {
            get { return _cVenName; }
            set { _cVenName = value; }
        }

        private string _cVenAbbName;
        /// <summary>
        /// 委外商简称
        /// </summary>
        public string VenAbbName
        {
            get { return _cVenAbbName; }
            set { _cVenAbbName = value; }
        }

        private string _cPersonCode;
        /// <summary>
        /// 业务员编码
        /// </summary>
        public string PersonCode
        {
            get { return _cPersonCode; }
            set { _cPersonCode = value; }
        }

        private string _cPersonName;
        /// <summary>
        /// 业务员
        /// </summary>
        public string PersonName
        {
            get { return _cPersonName; }
            set { _cPersonName = value; }
        }

        private string _cMakerCode;
        /// <summary>
        /// 制单人编码
        /// </summary>
        public string MakerCode
        {
            get { return _cMakerCode; }
            set { _cMakerCode = value; }
        }

        private string _cMakerName;
        /// <summary>
        /// 制单人
        /// </summary>
        public string MakerName
        {
            get { return _cMakerName; }
            set { _cMakerName = value; }
        }

        private string _cInvCode;
        /// <summary>
        /// 货物编码
        /// </summary>
        public string InvCode
        {
            get { return _cInvCode; }
            set { _cInvCode = value; }
        }

        private string _cInvName;
        /// <summary>
        /// 货物名称
        /// </summary>
        public string InvName
        {
            get { return _cInvName; }
            set { _cInvName = value; }
        }

        private string _cInvSta;
        /// <summary>
        /// 货物规格
        /// </summary>
        public string InvSta
        {
            get { return _cInvSta; }
            set { _cInvSta = value; }
        }

        private string _cInvUnit;
        /// <summary>
        /// 货物单位
        /// </summary>
        public string InvUnit
        {
            get { return _cInvUnit; }
            set { _cInvUnit = value; }
        }

        private decimal _iQuantity;
        /// <summary>
        /// 操作数量
        /// </summary>
        public decimal Quantity
        {
            get { return _iQuantity; }
            set { _iQuantity = value; }
        }

        private decimal _iNQuantity;
        /// <summary>
        /// 来源数量
        /// </summary>
        public decimal NQuantity
        {
            get { return _iNQuantity; }
            set { _iNQuantity = value; }
        }

        private decimal _iRowNo;
        /// <summary>
        /// 行号
        /// </summary>
        public decimal RowNo
        {
            get { return _iRowNo; }
            set { _iRowNo = value; }
        }

        private string _cUfts;
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Ufts
        {
            get { return _cUfts; }
            set { _cUfts = value; }
        }

        private string _cDisplayMember;
        /// <summary>
        /// 显示绑定
        /// </summary>
        public string DisplayMember
        {
            get { return _cDisplayMember; }
            set { _cDisplayMember = value; }
        }

        #endregion

        #region 内部方法
        /// <summary>
        /// 设置显示属性
        /// </summary>
        public void SetDisplayMember()
        {
            this.DisplayMember = _cInvName.PadRight(6, ' ') + _iNQuantity.ToString("F2") + _cInvUnit;
        }
        #endregion
    }

    /// <summary>
    /// 货位信息类
    /// </summary>
    public class PositionInfo
    {
        #region 货位信息

        private string _cPosCode;
        /// <summary>
        /// 货位编码
        /// </summary>
        public string PosCode
        {
            get { return _cPosCode; }
            set { _cPosCode = value; }
        }

        private string _cPosName;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string PosName
        {
            get { return _cPosName; }
            set { _cPosName = value; }
        }

        private decimal _iPosGrade;
        /// <summary>
        /// 编码级次
        /// </summary>
        public decimal PosGrade
        {
            get { return _iPosGrade; }
            set { _iPosGrade = value; }
        }

        private bool _bPosEnd;
        /// <summary>
        /// 是否末级
        /// </summary>
        public bool PosEnd
        {
            get { return _bPosEnd; }
            set { _bPosEnd = value; }
        }

        private string _cWhCode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhCode
        {
            get { return _cWhCode; }
            set { _cWhCode = value; }
        }

        private decimal _iMaxCubage;
        /// <summary>
        /// 最大体积
        /// </summary>
        public decimal MaxCubage
        {
            get { return _iMaxCubage; }
            set { _iMaxCubage = value; }
        }

        private decimal _iMaxWeight;
        /// <summary>
        /// 最大重量
        /// </summary>
        public decimal MaxWeight
        {
            get { return _iMaxWeight; }
            set { _iMaxWeight = value; }
        }

        private string _cMemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _cMemo; }
            set { _cMemo = value; }
        }

        private string _cBarCode;
        /// <summary>
        /// 对应条码
        /// </summary>
        public string BarCode
        {
            get { return _cBarCode; }
            set { _cBarCode = value; }
        }

        private string _cPubUfts;
        /// <summary>
        /// 时间戳
        /// </summary>
        public string PubUfts
        {
            get { return _cPubUfts; }
            set { _cPubUfts = value; }
        }

        private decimal _iQuantity;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity
        {
            get { return _iQuantity; }
            set { _iQuantity = value; }
        }

        private decimal _iNum;
        /// <summary>
        /// 辅计量数量
        /// </summary>
        public decimal Num
        {
            get { return _iNum; }
            set { _iNum = value; }
        }

        #endregion
    }

    /// <summary>
    /// 货位记录类
    /// </summary>
    public class InvPositionInfo
    {
        #region 货位记录

        private string _dExpirationDate;
        /// <summary>
        /// 有效期计算项 
        /// </summary>
        public string dExpirationDate
        {
            get { return _dExpirationDate; }
            set { _dExpirationDate = value; }
        }
        private decimal _iExpiratDateCalcu;
        /// <summary>
        /// 有效期推算方式 
        /// </summary>
        public decimal ExpiratDateCalcu
        {
            get { return _iExpiratDateCalcu; }
            set { _iExpiratDateCalcu = value; }
        }
        private string _cVmivenCode;
        /// <summary>
        /// 代管商编码 
        /// </summary>
        public string VmivenCode
        {
            get { return _cVmivenCode; }
            set { _cVmivenCode = value; }
        }
        private string _cExpirationdate;
        /// <summary>
        /// 有效期至 
        /// </summary>
        public string ExpirationDate
        {
            get { return _cExpirationdate; }
            set { _cExpirationdate = value; }
        }
        private string _AutoID;
        /// <summary>
        /// 自动编号 
        /// </summary>
        public string AutoID
        {
            get { return _AutoID; }
            set { _AutoID = value; }
        }
        private string _RdsID;
        /// <summary>
        /// 收发记录子表标识 
        /// </summary>
        public string RdsID
        {
            get { return _RdsID; }
            set { _RdsID = value; }
        }
        private string _RdID;
        /// <summary>
        /// 收发记录主表标识 
        /// </summary>
        public string RdID
        {
            get { return _RdID; }
            set { _RdID = value; }
        }
        private string _cWhCode;
        /// <summary>
        /// 仓库编码 
        /// </summary>
        public string WhCode
        {
            get { return _cWhCode; }
            set { _cWhCode = value; }
        }
        private string _cPosCode;
        /// <summary>
        /// 货位编码 
        /// </summary>
        public string PosCode
        {
            get { return _cPosCode; }
            set { _cPosCode = value; }
        }
        private string _cInvCode;
        /// <summary>
        /// 存货编码 
        /// </summary>
        public string InvCode
        {
            get { return _cInvCode; }
            set { _cInvCode = value; }
        }
        private string _cInvName;
        /// <summary>
        /// 存货名称 
        /// </summary>
        public string InvName
        {
            get { return _cInvName; }
            set { _cInvName = value; }
        }
        private string _cInvStd;
        /// <summary>
        /// 存货规格
        /// </summary>
        public string InvStd
        {
            get { return _cInvStd; }
            set { _cInvStd = value; }
        }
        private string _cAddress;
        /// <summary>
        /// 存货产地
        /// </summary>
        public string Address
        {
            get { return _cAddress; }
            set { _cAddress = value; }
        }
        private string _cBatch;
        /// <summary>
        /// 批号 
        /// </summary>
        public string Batch
        {
            get { return _cBatch; }
            set { _cBatch = value; }
        }
        private decimal _iQuantity;
        /// <summary>
        /// 数量 
        /// </summary>
        public decimal Quantity
        {
            get { return _iQuantity; }
            set { _iQuantity = value; }
        }
        private decimal _iNum;
        /// <summary>
        /// 辅计量数量 
        /// </summary>
        public decimal Num
        {
            get { return _iNum; }
            set { _iNum = value; }
        }
        private string _cMemo;
        /// <summary>
        /// 备注 
        /// </summary>
        public string Memo
        {
            get { return _cMemo; }
            set { _cMemo = value; }
        }
        private string _cHandler;
        /// <summary>
        /// 经手人 
        /// </summary>
        public string Handler
        {
            get { return _cHandler; }
            set { _cHandler = value; }
        }
        private string _dDate;
        /// <summary>
        /// 单据日期 
        /// </summary>
        public string Date
        {
            get { return _dDate; }
            set { _dDate = value; }
        }
        private bool _bRdFlag;
        /// <summary>
        /// 收发标志 
        /// </summary>
        public bool RdFlag
        {
            get { return _bRdFlag; }
            set { _bRdFlag = value; }
        }
        private string _cSource;
        /// <summary>
        /// 单据来源 
        /// </summary>
        public string Source
        {
            get { return _cSource; }
            set { _cSource = value; }
        }
        private string _cAssUnit;
        /// <summary>
        /// 辅计量单位编码 
        /// </summary>
        public string AssUnit
        {
            get { return _cAssUnit; }
            set { _cAssUnit = value; }
        }
        private string _cBVencode;
        /// <summary>
        /// 供应商编码 
        /// </summary>
        public string BVencode
        {
            get { return _cBVencode; }
            set { _cBVencode = value; }
        }
        private string _iTrackId;
        /// <summary>
        /// 对应入库单子表标识 
        /// </summary>
        public string TrackId
        {
            get { return _iTrackId; }
            set { _iTrackId = value; }
        }
        private string _ufts;
        /// <summary>
        /// 时间戳 
        /// </summary>
        public string Ufts
        {
            get { return _ufts; }
            set { _ufts = value; }
        }
        private string _dMadeDate;
        /// <summary>
        /// 生产日期 
        /// </summary>
        public string MadeDate
        {
            get { return _dMadeDate; }
            set { _dMadeDate = value; }
        }
        private string _dVDate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string VDate
        {
            get { return _dVDate; }
            set { _dVDate = value; }
        }
        private string _cMassUnit;
        /// <summary>
        /// 保质期单位 
        /// </summary>
        public string MassUnit
        {
            get { return _cMassUnit; }
            set { _cMassUnit = value; }
        }
        private decimal _iMassDate;
        /// <summary>
        /// 保质期天数 
        /// </summary>
        public decimal MassDate
        {
            get { return _iMassDate; }
            set { _iMassDate = value; }
        }

        #region 自由项
        private string cFree1;
        /// <summary>
        /// 
        /// </summary>
        public string Free1
        {
            get { return cFree1; }
            set { cFree1 = value; }
        }
        private string cFree2;
        /// <summary>
        /// 
        /// </summary>
        public string Free2
        {
            get { return cFree2; }
            set { cFree2 = value; }
        }
        private string cFree3;
        /// <summary>
        /// 
        /// </summary>
        public string Free3
        {
            get { return cFree3; }
            set { cFree3 = value; }
        }
        private string cFree4;
        /// <summary>
        /// 
        /// </summary>
        public string Free4
        {
            get { return cFree4; }
            set { cFree4 = value; }
        }
        private string cFree5;
        /// <summary>
        /// 
        /// </summary>
        public string Free5
        {
            get { return cFree5; }
            set { cFree5 = value; }
        }
        private string cFree6;
        /// <summary>
        /// 
        /// </summary>
        public string Free6
        {
            get { return cFree6; }
            set { cFree6 = value; }
        }
        private string cFree7;
        /// <summary>
        /// 
        /// </summary>
        public string Free7
        {
            get { return cFree7; }
            set { cFree7 = value; }
        }
        private string cFree8;
        /// <summary>
        /// 
        /// </summary>
        public string Free8
        {
            get { return cFree8; }
            set { cFree8 = value; }
        }
        private string cFree9;
        /// <summary>
        /// 
        /// </summary>
        public string Free9
        {
            get { return cFree9; }
            set { cFree9 = value; }
        }
        private string cFree10;
        /// <summary>
        /// 
        /// </summary>
        public string Free10
        {
            get { return cFree10; }
            set { cFree10 = value; }
        }
        
        #endregion
        #endregion

        #region 内部方法

        /// <summary>
        /// 设置保质期
        /// </summary>
        public void SetMassDate()
        {
            this.MassDate = CalcMonth(MadeDate, VDate);
        }

        /// <summary>
        /// 计算月份
        /// </summary>
        /// <param name="prevDate">生产日期</param>
        /// <param name="lastDate">失效日期</param>
        /// <returns></returns>
        private static int CalcMonth(string prevDate, string lastDate)
        {
            int sum = 0;
            try
            {
                DateTime pTime = DateTime.Parse(prevDate);
                DateTime lTime = DateTime.Parse(lastDate);
                sum = 12 * (lTime.Year - pTime.Year) + (lTime.Month - pTime.Month);
                if (sum < 0)
                    sum *= -1;
            }
            catch
            {
                sum = -1;
            }
            return sum;
        }

        #endregion
    }
}