using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class StockInDetail
    {
        public StockInDetail()
        {
            this.m_iNum = 0;
            this.m_iQuantity = 0;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        public StockInDetail getNewDetail()
        {
            StockInDetail sid = (StockInDetail)this.MemberwiseClone();
            return sid;
        }

        public List<decimal> listiquantity = new List<decimal>();//单品记录

        #region 属性
        private decimal m_iOrderQuantity;
        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal OrderQuantity
        {
            get { return m_iOrderQuantity; }
            set { m_iOrderQuantity = value; }
        }

        private decimal m_iOrderNumber;
        public decimal OrderNumber
        {
            get { return m_iOrderNumber; }
            set { m_iOrderNumber = value; }
        }

        private decimal m_iDoneQuantity;
        /// <summary>
        /// 操作IQUANTITY
        /// </summary>
        public decimal DoneQuantity
        {
            get { return m_iDoneQuantity; }
            set { m_iDoneQuantity = value; }
        }

        private decimal m_iDoneNumber;
        /// <summary>
        /// 操作INUM
        /// </summary>
        public decimal DoneNumber
        {
            get { return m_iDoneNumber; }
            set { m_iDoneNumber = value; }
        }


        private int m_iAutoid;
        public int AutoID
        {
            get { return m_iAutoid; }
            set { m_iAutoid = value; }
        }

        private string m_bCosting;
        public string Costing
        {
            get { return m_bCosting; }
            set { m_bCosting = value; }
        }

        private bool m_bInvbatch;
        /// <summary>
        /// 是否批次管理
        /// </summary>
        public bool Invbatch
        {
            get { return m_bInvbatch; }
            set { m_bInvbatch = value; }
        }

        private bool m_bInvtype;
        public bool Invtype
        {
            get { return m_bInvtype; }
            set { m_bInvtype = value; }
        }

        private bool m_bService;
        public bool Service
        {
            get { return m_bService; }
            set { m_bService = value; }
        }

        private int m_bTaxcost;
        public int Taxcost
        {
            get { return m_bTaxcost; }
            set { m_bTaxcost = value; }
        }

        private bool m_bVmiused;
        public bool Vmiused
        {
            get { return m_bVmiused; }
            set { m_bVmiused = value; }
        }

        private string m_cAssunit;
        public string Assunit
        {
            get { return m_cAssunit; }
            set { m_cAssunit = value; }
        }

        private string m_cBaccounter;
        public string Baccounter
        {
            get { return m_cBaccounter; }
            set { m_cBaccounter = value; }
        }

        private string m_cBarcode;
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return m_cBarcode; }
            set { m_cBarcode = value; }
        }

        private string m_cBatch;
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch
        {
            get { return m_cBatch; }
            set { m_cBatch = value; }
        }

        private string m_cBvencode;
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string Bvencode
        {
            get { return m_cBvencode; }
            set { m_cBvencode = value; }
        }

        private string m_cCheckcode;
        /// <summary>
        /// 检验单编号
        /// </summary>
        public string Checkcode
        {
            get { return m_cCheckcode; }
            set { m_cCheckcode = value; }
        }


        private string m_cCheckpersoncode;
        /// <summary>
        /// 检验人员编号
        /// </summary>
        public string Checkpersoncode
        {
            get { return m_cCheckpersoncode; }
            set { m_cCheckpersoncode = value; }
        }

        private string m_cCheckpersonname;
        /// <summary>
        /// 检验人员名称
        /// </summary>
        public string Checkpersonname
        {
            get { return m_cCheckpersonname; }
            set { m_cCheckpersonname = value; }
        }

        private string m_cDefine22;
        public string Define22
        {
            get { return m_cDefine22; }
            set { m_cDefine22 = value; }
        }

        private string m_cDefine23;
        public string Define23
        {
            get { return m_cDefine23; }
            set { m_cDefine23 = value; }
        }

        private string m_cDefine24;
        public string Define24
        {
            get { return m_cDefine24; }
            set { m_cDefine24 = value; }
        }

        private string m_cDefine25;
        public string Define25
        {
            get { return m_cDefine25; }
            set { m_cDefine25 = value; }
        }

        private string m_cDefine26;
        public string Define26
        {
            get { return m_cDefine26; }
            set { m_cDefine26 = value; }
        }

        private string m_cDefine27;
        public string Define27
        {
            get { return m_cDefine27; }
            set { m_cDefine27 = value; }
        }

        private string m_cDefine28;
        public string Define28
        {
            get { return m_cDefine28; }
            set { m_cDefine28 = value; }
        }

        private string m_cDefine29;
        public string Define29
        {
            get { return m_cDefine29; }
            set { m_cDefine29 = value; }
        }

        private string m_cDefine30;
        public string Define30
        {
            get { return m_cDefine30; }
            set { m_cDefine30 = value; }
        }

        private string m_cDefine31;
        public string Define31
        {
            get { return m_cDefine31; }
            set { m_cDefine31 = value; }
        }

        private string m_cDefine32;
        public string Define32
        {
            get { return m_cDefine32; }
            set { m_cDefine32 = value; }
        }

        private string m_cDefine33;
        public string Define33
        {
            get { return m_cDefine33; }
            set { m_cDefine33 = value; }
        }
        private string m_cDefine34;
        public string Define34
        {
            get { return m_cDefine34; }
            set { m_cDefine34 = value; }
        }
        private string m_cDefine35;
        public string Define35
        {
            get { return m_cDefine35; }
            set { m_cDefine35 = value; }
        }
        private string m_cDefine36;
        public string Define36
        {
            get { return m_cDefine36; }
            set { m_cDefine36 = value; }
        }
        private string m_cDefine37;
        public string Define37
        {
            get { return m_cDefine37; }
            set { m_cDefine37 = value; }
        }

        private string m_cFree1;
        public string Free1
        {
            get { return m_cFree1; }
            set { m_cFree1 = value; }
        }

        private string m_cFree10;
        public string Free10
        {
            get { return m_cFree10; }
            set { m_cFree10 = value; }
        }

        private string m_cFree2;
        public string Free2
        {
            get { return m_cFree2; }
            set { m_cFree2 = value; }
        }

        private string m_cFree3;
        public string Free3
        {
            get { return m_cFree3; }
            set { m_cFree3 = value; }
        }
        private string m_cFree4;
        public string Free4
        {
            get { return m_cFree4; }
            set { m_cFree4 = value; }
        }

        private string m_cFree5;
        public string Free5
        {
            get { return m_cFree5; }
            set { m_cFree5 = value; }
        }

        private string m_cFree6;
        public string Free6
        {
            get { return m_cFree6; }
            set { m_cFree6 = value; }
        }

        private string m_cFree7;
        public string Free7
        {
            get { return m_cFree7; }
            set { m_cFree7 = value; }
        }

        private string m_cFree8;
        public string Free8
        {
            get { return m_cFree8; }
            set { m_cFree8 = value; }
        }

        private string m_cFree9;
        public string Free9
        {
            get { return m_cFree9; }
            set { m_cFree9 = value; }
        }

        private bool m_bGsp;
        /// <summary>
        /// 是否质检
        /// </summary>
        public bool IsGsp
        {
            get { return m_bGsp; }
            set { m_bGsp = value; }
        }

        private string m_cGspstate;
        public string Gspstate
        {
            get { return m_cGspstate; }
            set { m_cGspstate = value; }
        }
        private string m_cInva_unit;
        public string Inva_unit
        {
            get { return m_cInva_unit; }
            set { m_cInva_unit = value; }
        }
        private string m_cInvaddcode;
        public string Invaddcode
        {
            get { return m_cInvaddcode; }
            set { m_cInvaddcode = value; }
        }
        private string m_cInvccode;
        public string InvCCode
        {
            get { return m_cInvccode; }
            set { m_cInvccode = value; }
        }

        private string m_cInvcode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode
        {
            get { return m_cInvcode; }
            set { m_cInvcode = value; }
        }

        private string m_InvCode;
        /// <summary>
        /// 产品编码
        /// </summary>
        public string InvCode
        {
            get { return m_InvCode; }
            set { m_InvCode = value; }
        }

        private string m_cInvdefine1;
        public string Invdefine1
        {
            get { return m_cInvdefine1; }
            set { m_cInvdefine1 = value; }
        }
        private string m_cInvdefine10;
        public string Invdefine10
        {
            get { return m_cInvdefine10; }
            set { m_cInvdefine10 = value; }
        }
        private string m_cInvdefine11;
        public string Invdefine11
        {
            get { return m_cInvdefine11; }
            set { m_cInvdefine11 = value; }
        }
        private string m_cInvdefine12;
        public string Invdefine12
        {
            get { return m_cInvdefine12; }
            set { m_cInvdefine12 = value; }
        }
        private string m_cInvdefine13;
        public string Invdefine13
        {
            get { return m_cInvdefine13; }
            set { m_cInvdefine13 = value; }
        }

        private string m_cInvdefine14;
        public string Invdefine14
        {
            get { return m_cInvdefine14; }
            set { m_cInvdefine14 = value; }
        }

        private string m_cInvdefine15;
        public string Invdefine15
        {
            get { return m_cInvdefine15; }
            set { m_cInvdefine15 = value; }
        }

        private string m_cInvdefine16;
        public string Invdefine16
        {
            get { return m_cInvdefine16; }
            set { m_cInvdefine16 = value; }
        }

        private string m_cInvdefine2;
        public string Invdefine2
        {
            get { return m_cInvdefine2; }
            set { m_cInvdefine2 = value; }
        }

        private string m_cInvdefine3;
        public string Invdefine3
        {
            get { return m_cInvdefine3; }
            set { m_cInvdefine3 = value; }
        }

        private string m_cInvdefine4;
        public string Invdefine4
        {
            get { return m_cInvdefine4; }
            set { m_cInvdefine4 = value; }
        }

        private string m_cInvdefine5;
        public string Invdefine5
        {
            get { return m_cInvdefine5; }
            set { m_cInvdefine5 = value; }
        }

        private string m_cInvdefine6;
        public string Invdefine6
        {
            get { return m_cInvdefine6; }
            set { m_cInvdefine6 = value; }
        }

        private string m_cInvdefine7;
        public string Invdefine7
        {
            get { return m_cInvdefine7; }
            set { m_cInvdefine7 = value; }
        }

        private string m_cInvdefine8;
        public string Invdefine8
        {
            get { return m_cInvdefine8; }
            set { m_cInvdefine8 = value; }
        }

        private string m_cInvdefine9;
        public string Invdefine9
        {
            get { return m_cInvdefine9; }
            set { m_cInvdefine9 = value; }
        }

        private string m_cInvm_unit;
        public string Invm_unit
        {
            get { return m_cInvm_unit; }
            set { m_cInvm_unit = value; }
        }

        private string m_cInvname;
        public string Invname
        {
            get { return m_cInvname; }
            set { m_cInvname = value; }
        }

        private string m_cInvouchcode;
        public string Invouchcode
        {
            get { return m_cInvouchcode; }
            set { m_cInvouchcode = value; }
        }

        private string m_cInvstd;
        public string cInvStd
        {
            get { return m_cInvstd; }
            set { m_cInvstd = value; }
        }

        private string m_cItem_class;
        public string Item_class
        {
            get { return m_cItem_class; }
            set { m_cItem_class = value; }
        }

        private string m_cItemcname;
        public string Itemcname
        {
            get { return m_cItemcname; }
            set { m_cItemcname = value; }
        }

        private string m_cItemcode;
        public string Itemcode
        {
            get { return m_cItemcode; }
            set { m_cItemcode = value; }
        }

        private string m_cMassunit;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string Massunit
        {
            get { return m_cMassunit; }
            set { m_cMassunit = value; }
        }

        private string m_cName;
        public string Name
        {
            get { return m_cName; }
            set { m_cName = value; }
        }

        private string m_cOrufts;
        public string Orufts
        {
            get { return m_cOrufts; }
            set { m_cOrufts = value; }
        }

        private string m_cPoid;
        public string Poid
        {
            get { return m_cPoid; }
            set { m_cPoid = value; }
        }

        private string m_cPosition;
        /// <summary>
        /// 货位编码
        /// </summary>
        public string Position
        {
            get { return m_cPosition; }
            set { m_cPosition = value; }
        }

        private string m_cPosname;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string Posname
        {
            get { return m_cPosname; }
            set { m_cPosname = value; }
        }

        private string m_cRejectcode;
        public string Rejectcode
        {
            get { return m_cRejectcode; }
            set { m_cRejectcode = value; }
        }
        private string m_cReplaceitem;
        public string Replaceitem
        {
            get { return m_cReplaceitem; }
            set { m_cReplaceitem = value; }
        }

        private string m_comcode;
        /// <summary>
        /// 委外订单号
        /// </summary>
        public string Omcode
        {
            get { return m_comcode; }
            set { m_comcode = value; }
        }

        private string m_cSocode;
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string Socode
        {
            get { return m_cSocode; }
            set { m_cSocode = value; }
        }

        private string m_cVeninvcode;
        public string Veninvcode
        {
            get { return m_cVeninvcode; }
            set { m_cVeninvcode = value; }
        }

        private string m_cVeninvname;
        public string Veninvname
        {
            get { return m_cVeninvname; }
            set { m_cVeninvname = value; }
        }

        private string m_cVenCode;

        public string VenCode
        {
            get { return m_cVenCode; }
            set { m_cVenCode = value; }
        }

        private string m_cVenname;
        public string Venname
        {
            get { return m_cVenname; }
            set { m_cVenname = value; }
        }

        private string m_cVmivencode;
        public string Vmivencode
        {
            get { return m_cVmivencode; }
            set { m_cVmivencode = value; }
        }

        private string m_cVmivenname;
        public string Vmivenname
        {
            get { return m_cVmivenname; }
            set { m_cVmivenname = value; }
        }

        private string m_cVouchcode;
        public string Vouchcode
        {
            get { return m_cVouchcode; }
            set { m_cVouchcode = value; }
        }

        private string m_dCheckdate;
        public string CheckDate
        {
            get { return m_dCheckdate; }
            set { m_dCheckdate = value; }
        }
        private string m_dMadedate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string Madedate
        {
            get { return m_dMadedate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_dMadedate = "";
                else
                {
                    try { m_dMadedate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { m_dMadedate = ""; }
                }
            }
        }

        private string m_dMsdate;
        public string Msdate
        {
            get { return m_dMsdate; }
            set { m_dMsdate = value; }
        }

        private string m_dSdate;
        public string Sdate
        {
            get { return m_dSdate; }
            set { m_dSdate = value; }
        }

        private string m_dVdate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string Vdate
        {
            get { return m_dVdate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_dVdate = "";
                else
                {
                    try { m_dVdate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { m_dVdate = ""; }
                }
            }
        }

        private string m_dbarvdate;
        /// <summary>
        /// 到货日期
        /// </summary>
        public string barvdate
        {
            get { return m_dbarvdate; }
            set { m_dbarvdate = value; }
        }

        private string m_cbarvcode;
        public string barvcode
        {
            get { return m_cbarvcode; }
            set { m_cbarvcode = value; }
        }

        private string m_darvdate;
        /// <summary>
        /// 到货日期
        /// </summary>
        public string arvdate
        {
            get { return m_darvdate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_darvdate = "";
                else
                {
                    try { m_darvdate = DateTime.Parse(value).ToString("yyyy-MM-dd"); }
                    catch { m_darvdate = ""; }
                }
            }
        }

        private decimal m_fAcost;
        public decimal Acost
        {
            get { return m_fAcost; }
            set { m_fAcost = value; }
        }

        private int m_iAprice;
        public int Aprice
        {
            get { return m_iAprice; }
            set { m_iAprice = value; }
        }

        private int m_iArrsid;
        public int Arrsid
        {
            get { return m_iArrsid; }
            set { m_iArrsid = value; }
        }

        private int m_iCheckidbaks;
        public int Checkidbaks
        {
            get { return m_iCheckidbaks; }
            set { m_iCheckidbaks = value; }
        }

        private int m_iCheckids;
        public int Checkids
        {
            get { return m_iCheckids; }
            set { m_iCheckids = value; }
        }

        private int m_iID;
        public int ID
        {
            get { return m_iID; }
            set { m_iID = value; }
        }

        private int m_iFlag;
        public int Flag
        {
            get { return m_iFlag; }
            set { m_iFlag = value; }
        }

        private int m_iFnum;
        public int Fnum
        {
            get { return m_iFnum; }
            set { m_iFnum = value; }
        }

        private int m_iFquantity;
        public int Fquantity
        {
            get { return m_iFquantity; }
            set { m_iFquantity = value; }
        }

        private int m_iImbsid;
        public int Imbsid
        {
            get { return m_iImbsid; }
            set { m_iImbsid = value; }
        }

        private int m_iImosid;
        public int Imosid
        {
            get { return m_iImosid; }
            set { m_iImosid = value; }
        }

        private decimal m_iInvexchrate;
        public decimal Invexchrate
        {
            get { return m_iInvexchrate; }
            set { m_iInvexchrate = value; }
        }

        private decimal m_iInvsncount;
        public decimal Invsncount
        {
            get { return m_iInvsncount; }
            set { m_iInvsncount = value; }
        }

        private decimal m_iMassdate;
        /// <summary>
        /// 保质期
        /// </summary>
        public decimal Massdate
        {
            get { return m_iMassdate; }
            set { m_iMassdate = value; }
        }

        private decimal m_iMaterialfee;
        public decimal Materialfee
        {
            get { return m_iMaterialfee; }
            set { m_iMaterialfee = value; }
        }

        private decimal m_iMoney;
        public decimal Money
        {
            get { return m_iMoney; }
            set { m_iMoney = value; }
        }
        private decimal m_iMpcost;
        public decimal Mpcost
        {
            get { return m_iMpcost; }
            set { m_iMpcost = value; }
        }
        private decimal m_iMpoids;
        public decimal Mpoids
        {
            get { return m_iMpoids; }
            set { m_iMpoids = value; }
        }

        private decimal m_iNnum;
        public decimal Nnum
        {
            get { return m_iNnum; }
            set { m_iNnum = value; }
        }

        private decimal m_iNquantity;
        /// <summary>
        /// 待扫描数量
        /// </summary>
        public decimal Nquantity
        {
            get { return m_iNquantity; }
            set { m_iNquantity = value; }
        }

        private decimal m_iNum;
        /// <summary>
        /// 辅计数数量
        /// </summary>
        public decimal Num
        {
            get { return m_iNum; }
            set { m_iNum = value; }
        }

        private decimal m_iOmodid;
        /// <summary>
        /// 委外订单子表ID 
        /// </summary>
        public decimal Omodid
        {
            get { return m_iOmodid; }
            set { m_iOmodid = value; }
        }

        private Decimal m_iOricost;
        /// <summary>
        /// 原币无税单价
        /// </summary>
        public Decimal Oricost
        {
            get { return m_iOricost; }
            set { m_iOricost = value; }
        }

        private Decimal m_iOrimoney;
        /// <summary>
        /// 原币无税金额
        /// </summary>
        public Decimal Orimoney
        {
            get { return m_iOrimoney; }
            set { m_iOrimoney = value; }
        }

        private Decimal m_iOrisum;
        /// <summary>
        /// 原币价税合计
        /// </summary>
        public Decimal Orisum
        {
            get { return m_iOrisum; }
            set { m_iOrisum = value; }
        }

        private Decimal m_iOritaxcost;
        /// <summary>
        /// 原币含税单价 
        /// </summary>
        public Decimal Oritaxcost
        {
            get { return m_iOritaxcost; }
            set { m_iOritaxcost = value; }
        }

        private Decimal m_iOritaxprice;
        /// <summary>
        /// 原币税额
        /// </summary>
        public Decimal Oritaxprice
        {
            get { return m_iOritaxprice; }
            set { m_iOritaxprice = value; }
        }

        private int m_iPosid;
        public int Posid
        {
            get { return m_iPosid; }
            set { m_iPosid = value; }
        }

        private decimal m_iPprice;
        public decimal Pprice
        {
            get { return m_iPprice; }
            set { m_iPprice = value; }
        }

        private Decimal m_iPrice;
        /// <summary>
        /// 本币无税金额
        /// </summary>
        public Decimal Price
        {
            get { return m_iPrice; }
            set { m_iPrice = value; }
        }

        private decimal m_iProcesscost;
        public decimal Processcost
        {
            get { return m_iProcesscost; }
            set { m_iProcesscost = value; }
        }

        private decimal m_iProcessfee;
        public decimal Processfee
        {
            get { return m_iProcessfee; }
            set { m_iProcessfee = value; }
        }

        private decimal m_iPunitcost;
        public decimal Punitcost
        {
            get { return m_iPunitcost; }
            set { m_iPunitcost = value; }
        }

        private decimal m_iQuantity;
        /// <summary>
        /// 已扫描数量
        /// </summary>
        public decimal Quantity
        {
            get { return m_iQuantity; }
            set { m_iQuantity = value; }
        }

        private decimal m_iRejectids;
        public decimal Rejectids
        {
            get { return m_iRejectids; }
            set { m_iRejectids = value; }
        }

        private decimal m_iSmaterialfee;
        public decimal Smaterialfee
        {
            get { return m_iSmaterialfee; }
            set { m_iSmaterialfee = value; }
        }

        private decimal m_iSnum;
        public decimal Snum
        {
            get { return m_iSnum; }
            set { m_iSnum = value; }
        }

        private decimal m_iSodid;
        /// <summary>
        /// 销售订单子表ID 
        /// </summary>
        public decimal Sodid
        {
            get { return m_iSodid; }
            set { m_iSodid = value; }
        }

        private decimal m_iSoseq;
        public decimal Soseq
        {
            get { return m_iSoseq; }
            set { m_iSoseq = value; }
        }

        private decimal m_iSotype;
        public decimal Sotype
        {
            get { return m_iSotype; }
            set { m_iSotype = value; }
        }

        private decimal m_iSoutnum;
        public decimal Soutnum
        {
            get { return m_iSoutnum; }
            set { m_iSoutnum = value; }
        }

        private decimal m_iSoutquantity;
        public decimal Soutquantity
        {
            get { return m_iSoutquantity; }
            set { m_iSoutquantity = value; }
        }

        private decimal m_iSprocessfee;
        public decimal Sprocessfee
        {
            get { return m_iSprocessfee; }
            set { m_iSprocessfee = value; }
        }

        private decimal m_iSquantity;
        public decimal Squantity
        {
            get { return m_iSquantity; }
            set { m_iSquantity = value; }
        }

        private decimal m_iSum;
        /// <summary>
        /// 本币价税合计
        /// </summary>
        public decimal Sum
        {
            get { return m_iSum; }
            set { m_iSum = value; }
        }

        private decimal m_iSumbillquantity;
        public decimal Sumbillquantity
        {
            get { return m_iSumbillquantity; }
            set { m_iSumbillquantity = value; }
        }

        private decimal m_iTax;
        public decimal Tax
        {
            get { return m_iTax; }
            set { m_iTax = value; }
        }

        private decimal m_iTaxprice;
        /// <summary>
        /// 本币税额
        /// </summary>
        public decimal Taxprice
        {
            get { return m_iTaxprice; }
            set { m_iTaxprice = value; }
        }

        private decimal m_iTaxrate;
        /// <summary>
        /// 表体税率
        /// </summary>
        public decimal Taxrate
        {
            get { return m_iTaxrate; }
            set { m_iTaxrate = value; }
        }

        private decimal m_iTrids;
        public decimal Trids
        {
            get { return m_iTrids; }
            set { m_iTrids = value; }
        }

        private decimal m_iUnitcost;
        /// <summary>
        /// 本币无税单价
        /// </summary>
        public decimal Unitcost
        {
            get { return m_iUnitcost; }
            set { m_iUnitcost = value; }
        }
        private decimal m_iVmisettlenum;
        public decimal Vmisettlenum
        {
            get { return m_iVmisettlenum; }
            set { m_iVmisettlenum = value; }
        }
        private decimal m_iVmisettlequantity;
        public decimal Vmisettlequantity
        {
            get { return m_iVmisettlequantity; }
            set { m_iVmisettlequantity = value; }
        }
        private string m_cScrapufts;
        public string Scrapufts
        {
            get { return m_cScrapufts; }
            set { m_cScrapufts = value; }
        }

        private string m_strCode;
        public string StrCode
        {
            get { return m_strCode; }
            set { m_strCode = value; }
        }
        private string m_strContractid;
        public string Contractid
        {
            get { return m_strContractid; }
            set { m_strContractid = value; }
        }

        private string m_cbatchproperty6;
        /// <summary>
        /// 批次属性，这里是供应商批次
        /// </summary>
        public string cbatchproperty6
        {
            get { return m_cbatchproperty6; }
            set { m_cbatchproperty6 = value; }
        }
        #endregion

        #region Info

        public string irowno;

        public string m_cWhCode;
        /// <summary>
        /// 表体仓库编码
        /// </summary>
        public string cWhCode
        {
            set { m_cWhCode = value; }
            get { return m_cWhCode; }
        }
        public string m_cWhName;
        /// <summary>
        /// 表体仓库名称
        /// </summary>
        public string cWhName
        {
            set { m_cWhName = value; }
            get { return m_cWhName; }
        }
        private string cexpirationdate;
        /// <summary>
        /// C有效期至
        /// </summary>
        public string cExpirationDate
        {
            get { return cexpirationdate; }
            set { cexpirationdate = value; }
        }

        private string dexpirationdate;
        /// <summary>
        /// D有效期至
        /// </summary>
        public string dExpirationDate
        {
            get { return dexpirationdate; }
            set { dexpirationdate = value; }
        }

        private int iexpiratdatecalcu;
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public int iExpiratDateCalcu
        {
            get { return iexpiratdatecalcu; }
            set { iexpiratdatecalcu = value; }
        }

        /// <summary>
        /// 对应入库单id
        /// </summary>
        public string cvouchtype
        {
            set { m_cvouchtype = value; }
            get { return m_cvouchtype; }
        }
        private string m_cvouchtype;

        private string m_cinvccname;
        /// <summary>
        /// 大类名称
        /// </summary>
        public string cinvccname
        {
            get { return m_cinvccname; }
            set { m_cinvccname = value; }
        }

        private string cVenAbbName;
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenAbbName
        {
            get { return cVenAbbName; }
            set { cVenAbbName = value; }
        }

        private string m_cAddress;
        /// <summary>
        /// 产地
        /// </summary>
        public string Address
        {
            get { return m_cAddress; }
            set { m_cAddress = value; }
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

        private bool _bInvBatch;
        /// <summary>
        /// 是否批次管理
        /// </summary>
        public bool bInvBatch
        {
            get { return _bInvBatch; }
            set { _bInvBatch = value; }
        }

        private string _cOrderCode;
        /// <summary>
        /// 单据编号
        /// </summary>
        public string OrderCode
        {
            get { return _cOrderCode; }
            set { _cOrderCode = value; }
        }

        private string _cMoDetailsID;
        /// <summary>
        /// 委外明细ID
        /// </summary>
        public string cMoDetailsID
        {
            get { return _cMoDetailsID; }
            set { _cMoDetailsID = value; }
        }

        private string _iomomid;
        /// <summary>
        /// 委外用料单子表ID  
        /// </summary>
        public string OmomID
        {
            get { return _iomomid; }
            set { _iomomid = value; }
        }

        private decimal _fvalidinquan;
        /// <summary>
        /// 已入库数量
        /// </summary>
        public decimal fValidInQuan
        {
            get { return _fvalidinquan; }
            set { _fvalidinquan = value; }
        }

        private decimal _fshallinquan;
        /// <summary>
        /// 待入库数量
        /// </summary>
        public decimal fShallInQuan
        {
            get { return _fshallinquan; }
            set { _fshallinquan = value; }
        }

        private int _iproorderid;
        /// <summary>
        /// 订单主表标识
        /// </summary>
        public int iProOrderId
        {
            get { return _iproorderid; }
            set { _iproorderid = value; }
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

        private bool _bIsPos;
        /// <summary>
        /// 是否有货位信息
        /// </summary>
        public bool IsPos
        {
            get { return _bIsPos; }
            set { _bIsPos = value; }
        }

        #endregion
    }
}
