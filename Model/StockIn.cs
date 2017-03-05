using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Model
{
    public class StockIn
    {
        public StockIn()
        {
            OperateDetails = new List<StockInDetail>();
            this.m_arrOperateDetails = new List<StockInDetail>();
            OperaPositions = new List<InvPositionInfo>();
            this._OperaPositions = new List<InvPositionInfo>();
        }
        public enum OrderType { CO = 0, AO = 1, SO = 2, NO = 4, GO = 3 };

        public enum VouchType { SalesOrder, WorkOrder, OtherOrder };
        private VouchType m_VouchType;
        public VouchType VouchTypeEnm
        {
            get { return m_VouchType; }
            set { m_VouchType = value; }
        }

        private List<StockInDetail> m_arrU8Details;
        /// <summary>
        /// 来源信息
        /// </summary>
        public List<StockInDetail> U8Details
        {
            get { return m_arrU8Details; }
            set { m_arrU8Details = value; }
        }

        private List<StockInDetail> m_arrOperateDetails;
        /// <summary>
        /// 已操作信息
        /// </summary>
        public List<StockInDetail> OperateDetails
        {
            get { return m_arrOperateDetails; }
            set { m_arrOperateDetails = value; }
        }

        private List<InvPositionInfo> _OperaPositions;
        /// <summary>
        /// 已操作货位
        /// </summary>
        public List<InvPositionInfo> OperaPositions
        {
            get { return _OperaPositions; }
            set { _OperaPositions = value; }
        }

        #region 属性

        private int m_ControlResult;
        public int ControlResult
        {
            get { return m_ControlResult; }
            set { m_ControlResult = value; }
        }

        private bool m_bIsstqc;
        public bool Isstqc
        {
            get { return m_bIsstqc; }
            set { m_bIsstqc = value; }
        }

        private bool m_bPufirst;
        public bool Pufirst
        {
            get { return m_bPufirst; }
            set { m_bPufirst = value; }
        }

        private bool m_biafirst;
        public bool Biafirst
        {
            get { return m_biafirst; }
            set { m_biafirst = value; }
        }

        private int m_bRdflag;
        public int Rdflag
        {
            get { return m_bRdflag; }
            set { m_bRdflag = value; }
        }

        private bool m_bFromPreYear;
        public bool bFromPreYear
        {
            get { return m_bFromPreYear; }
            set { m_bFromPreYear = value; }
        }

        private string m_cAccounter;
        public string Accounter
        {
            get { return m_cAccounter; }
            set { m_cAccounter = value; }
        }

        private string m_cArvcode;
        public string Arvcode
        {
            get { return m_cArvcode; }
            set { m_cArvcode = value; }
        }

        private string m_cBillcode;
        public string Billcode
        {
            get { return m_cBillcode; }
            set { m_cBillcode = value; }
        }

        private string m_cBuscode;
        public string Buscode
        {
            get { return m_cBuscode; }
            set { m_cBuscode = value; }
        }

        private string m_cBustype;
        public string Bustype
        {
            get { return m_cBustype; }
            set { m_cBustype = value; }
        }
        private string m_cChkcode;
        public string Chkcode
        {
            get { return m_cChkcode; }
            set { m_cChkcode = value; }
        }
        private string m_cChkperson;
        public string Chkperson
        {
            get { return m_cChkperson; }
            set { m_cChkperson = value; }
        }
        private string m_cCode;
        public string Code
        {
            get { return m_cCode; }
            set { m_cCode = value; }
        }
        private string m_cDefine1;
        public string Define1
        {
            get { return m_cDefine1; }
            set { m_cDefine1 = value; }
        }
        private string m_cDefine10;
        public string Define10
        {
            get { return m_cDefine10; }
            set { m_cDefine10 = value; }
        }
        private string m_cDefine11;
        public string Define11
        {
            get { return m_cDefine11; }
            set { m_cDefine11 = value; }
        }
        private string m_cDefine12;
        public string Define12
        {
            get { return m_cDefine12; }
            set { m_cDefine12 = value; }
        }
        private string m_cDefine13;
        public string Define13
        {
            get { return m_cDefine13; }
            set { m_cDefine13 = value; }
        }
        private string m_cDefine14;
        public string Define14
        {
            get { return m_cDefine14; }
            set { m_cDefine14 = value; }
        }
        private string m_cDefine15;
        public string Define15
        {
            get { return m_cDefine15; }
            set { m_cDefine15 = value; }
        }
        private string m_cDefine16;
        public string Define16
        {
            get { return m_cDefine16; }
            set { m_cDefine16 = value; }
        }
        private string m_cDefine2;
        public string Define2
        {
            get { return m_cDefine2; }
            set { m_cDefine2 = value; }
        }
        private string m_cDefine3;
        public string Define3
        {
            get { return m_cDefine3; }
            set { m_cDefine3 = value; }
        }
        private string m_cDefine4;
        public string Define4
        {
            get { return m_cDefine4; }
            set { m_cDefine4 = value; }
        }
        private string m_cDefine5;
        public string Define5
        {
            get { return m_cDefine5; }
            set { m_cDefine5 = value; }
        }
        private string m_cDefine6;
        public string Define6
        {
            get { return m_cDefine6; }
            set { m_cDefine6 = value; }
        }
        private string m_cDefine7;
        public string Define7
        {
            get { return m_cDefine7; }
            set { m_cDefine7 = value; }
        }
        private string m_cDefine8;
        public string Define8
        {
            get { return m_cDefine8; }
            set { m_cDefine8 = value; }
        }
        private string m_cDefine9;
        public string Define9
        {
            get { return m_cDefine9; }
            set { m_cDefine9 = value; }
        }
        private string m_cDepcode;
        public string Depcode
        {
            get { return m_cDepcode; }
            set { m_cDepcode = value; }
        }
        private string m_cDepname;
        public string Depname
        {
            get { return m_cDepname; }
            set { m_cDepname = value; }
        }
        private string m_cExch_name;
        /// <summary>
        /// 币种
        /// </summary>
        public string Exch_name
        {
            get { return m_cExch_name; }
            set { m_cExch_name = value; }
        }
        private string m_cHandler;
        /// <summary>
        /// 审核人
        /// </summary>
        public string Handler
        {
            get { return m_cHandler; }
            set { m_cHandler = value; }
        }
        private string m_cMaker;
        /// <summary>
        /// 制单人
        /// </summary>
        public string Maker
        {
            get { return m_cMaker; }
            set { m_cMaker = value; }
        }
        private string m_cMemo;
        public string Memo
        {
            get { return m_cMemo; }
            set { m_cMemo = value; }
        }

        private string m_cOrdercode;
        /// <summary>
        /// 来源单据号
        /// </summary>
        public string Ordercode
        {
            get { return m_cOrdercode; }
            set { m_cOrdercode = value; }
        }

        private string m_cPersoncode;
        public string Personcode
        {
            get { return m_cPersoncode; }
            set { m_cPersoncode = value; }
        }

        private string m_cPersonname;
        public string Personname
        {
            get { return m_cPersonname; }
            set { m_cPersonname = value; }
        }

        private string m_cPtcode;
        public string Ptcode
        {
            get { return m_cPtcode; }
            set { m_cPtcode = value; }
        }

        private string m_cPtname;
        public string Ptname
        {
            get { return m_cPtname; }
            set { m_cPtname = value; }
        }

        private string m_cRdcode;
        public string Rdcode
        {
            get { return m_cRdcode; }
            set { m_cRdcode = value; }
        }

        private string m_cRdname;
        public string Rdname
        {
            get { return m_cRdname; }
            set { m_cRdname = value; }
        }


        private string m_cSource;
        /// <summary>
        /// 单据来源 
        /// </summary>
        public string Source
        {
            get { return m_cSource; }
            set { m_cSource = value; }
        }

        private string m_cVenabbname;
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string Venabbname
        {
            get { return m_cVenabbname; }
            set { m_cVenabbname = value; }
        }

        private string m_cVencode;
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string Vencode
        {
            get { return m_cVencode; }
            set { m_cVencode = value; }
        }

        private string m_cVouchtype;
        /// <summary>
        /// 单据类型编码 
        /// </summary>
        public string Vouchtype
        {
            get { return m_cVouchtype; }
            set { m_cVouchtype = value; }
        }

        private string m_cWhcode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string Whcode
        {
            get { return m_cWhcode; }
            set { m_cWhcode = value; }
        }

        private string m_cWhname;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Whname
        {
            get { return m_cWhname; }
            set { m_cWhname = value; }
        }

        private string m_dArvdate;
        /// <summary>
        /// 到货日期
        /// </summary>
        public string Arvdate
        {
            get { return m_dArvdate; }
            set { m_dArvdate = value; }
        }

        private string m_dChkdate;
        /// <summary>
        /// 检验日期
        /// </summary>
        public string Chkdate
        {
            get { return m_dChkdate; }
            set { m_dChkdate = value; }
        }

        private string m_dDate;
        /// <summary>
        /// 制单日期
        /// </summary>
        public string Date
        {
            get { return m_dDate; }
            set { m_dDate = value; }
        }

        private string m_dnmaketime;
        /// <summary>
        /// 生成时间
        /// </summary>
        public string nmaketime
        {
            get { return m_dnmaketime; }
            set { m_dnmaketime = value; }
        }

        private string m_dVeridate;
        /// <summary>
        /// 审核日期
        /// </summary>
        public string Veridate
        {
            get { return m_dVeridate; }
            set { m_dVeridate = value; }
        }


        private string m_dnverifytime;
        /// <summary>
        /// 审核时间 
        /// </summary>
        public string nverifytime
        {
            get { return m_dnverifytime; }
            set { m_dnverifytime = value; }
        }
        private string m_cGspcheck;
        public string Gspcheck
        {
            get { return m_cGspcheck; }
            set { m_cGspcheck = value; }
        }



        //private string m_cGspcheck;
        //public string Gspcheck
        //{
        //    get { return m_cGspcheck; }
        //    set { m_cGspcheck = value; }
        //}


        private int m_iArriveid;
        public int Arriveid
        {
            get { return m_iArriveid; }
            set { m_iArriveid = value; }
        }

        private decimal m_iAvanum;
        public decimal Avanum
        {
            get { return m_iAvanum; }
            set { m_iAvanum = value; }
        }

        private decimal m_iAvaquantity;
        public decimal Avaquantity
        {
            get { return m_iAvaquantity; }
            set { m_iAvaquantity = value; }
        }

        private string m_cVouchName;
        /// <summary>
        /// 入库单类型
        /// </summary>
        public string cVouchName
        {
            get { return m_cVouchName; }
            set { m_cVouchName = value; }
        }

        //只读属性，自动生成的ID
        private int m_id;
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private int m_iDiscounttaxtype;
        public int Discounttaxtype
        {
            get { return m_iDiscounttaxtype; }
            set { m_iDiscounttaxtype = value; }
        }

        private decimal m_iExchrate;
        public decimal Exchrate
        {
            get { return m_iExchrate; }
            set { m_iExchrate = value; }
        }

        private decimal m_iLowsum;
        public decimal Lowsum
        {
            get { return m_iLowsum; }
            set { m_iLowsum = value; }
        }

        private int m_iPresent;
        public int Present
        {
            get { return m_iPresent; }
            set { m_iPresent = value; }
        }

        private int m_iPresentnum;
        public int Presentnum
        {
            get { return m_iPresentnum; }
            set { m_iPresentnum = value; }
        }

        private int m_iProOrderId;
        /// <summary>
        /// 生产订单主表标识 
        /// </summary>
        public int ProOrderId
        {
            get { return m_iProOrderId; }
            set { m_iProOrderId = value; }
        }

        private int m_iPurarriveid;
        /// <summary>
        /// 采购到货单主表标识 
        /// </summary>
        public int Purarriveid
        {
            get { return m_iPurarriveid; }
            set { m_iPurarriveid = value; }
        }

        private int m_iPurorderid;
        /// <summary>
        /// 采购订单主表标识
        /// </summary>
        public int Purorderid
        {
            get { return m_iPurorderid; }
            set { m_iPurorderid = value; }
        }

        private int m_iReturncount;
        public int Returncount
        {
            get { return m_iReturncount; }
            set { m_iReturncount = value; }
        }

        private decimal m_iSafesum;
        public decimal Safesum
        {
            get { return m_iSafesum; }
            set { m_iSafesum = value; }
        }

        private int m_iSalebillid;
        public int Salebillid
        {
            get { return m_iSalebillid; }
            set { m_iSalebillid = value; }
        }

        private int m_iSwfcontrolled;
        public int Swfcontrolled
        {
            get { return m_iSwfcontrolled; }
            set { m_iSwfcontrolled = value; }
        }

        private decimal m_iTaxrate;
        /// <summary>
        /// 表头税率
        /// </summary>
        public decimal Taxrate
        {
            get { return m_iTaxrate; }
            set { m_iTaxrate = value; }
        }

        private decimal m_iTopsum;
        public decimal Topsum
        {
            get { return m_iTopsum; }
            set { m_iTopsum = value; }
        }

        private int m_iVerifystate;
        public int Verifystate
        {
            get { return m_iVerifystate; }
            set { m_iVerifystate = value; }
        }

        private string m_timeUfts;
        public string Ufts
        {
            get { return m_timeUfts; }
            set { m_timeUfts = value; }
        }

        private int m_VT_id;
        public int VT_id
        {
            get { return m_VT_id; }
            set { m_VT_id = value; }
        }

        private string m_cVenname;
        public string Venname
        {
            get { return m_cVenname; }
            set { m_cVenname = value; }
        }

        private int om_iVouchRowNo;
        /// <summary>
        /// 行号
        /// </summary>
        public int VouchRowNo
        {
            get { return om_iVouchRowNo; }
            set { om_iVouchRowNo = value; }
        }

        private string _strDisplay;
        /// <summary>
        /// 显示字段
        /// </summary>
        public string strDisplay
        {
            get { return _strDisplay; }
            set { _strDisplay = value; }
        }

        private string _invCode;
        /// <summary>
        /// 子项编码
        /// </summary>
        public string InvCode
        {
            get { return _invCode; }
            set { _invCode = value; }
        }

        private string _invName;
        /// <summary>
        /// 子项名称
        /// </summary>
        public string InvName
        {
            get { return _invName; }
            set { _invName = value; }
        }

        private decimal _inQuantity;
        /// <summary>
        /// 子项数量
        /// </summary>
        public decimal InQuantity
        {
            get { return _inQuantity; }
            set { _inQuantity = value; }
        }

        private string _BredVouch;
        /// <summary>
        /// 红蓝标识
        /// </summary>
        public string BredVouch
        {
            get { return _BredVouch; }
            set { _BredVouch = value; }
        }

        private string _cPspCode;
        /// <summary>
        /// 父项产品编码
        /// </summary>
        public string PspCode
        {
            get { return _cPspCode; }
            set { _cPspCode = value; }
        }

        private string _cMpoCode;
        /// <summary>
        /// 生产订单编号
        /// </summary>
        public string MpoCode
        {
            get { return _cMpoCode; }
            set { _cMpoCode = value; }
        }

        private decimal _iMQuantity;
        /// <summary>
        /// 产量
        /// </summary>
        public decimal MQuantity
        {
            get { return _iMQuantity; }
            set { _iMQuantity = value; }
        }

        private string _iOMoMID;
        /// <summary>
        /// 委外订单用料表ID 
        /// </summary>
        public string IOMoMID
        {
            get { return _iOMoMID; }
            set { _iOMoMID = value; }
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

        private bool _bIsOut;
        /// <summary>
        /// 是否出库
        /// </summary>
        public bool IsOut
        {
            get { return _bIsOut; }
            set { _bIsOut = value; }
        }

        private string _cSaveVouch;
        /// <summary>
        /// 保存参数
        /// </summary>
        public string SaveVouch
        {
            get { return _cSaveVouch; }
            set { _cSaveVouch = value; }
        }

        #endregion
                
    }
}
