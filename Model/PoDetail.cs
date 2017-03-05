using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PoDetail
    {
        //采购订单子表

        #region PO_Podetail  表字段
        private int iD;
        private string cPOID;
        private string cInvCode;
        private decimal iQuantity;
        private decimal iNum;
        private decimal iQuotedPrice;
        private decimal iUnitPrice;
        private decimal iMoney;
        private decimal iTax;
        private decimal iSum;
        private decimal iDisCount;
        private decimal iNatUnitPrice;
        private decimal iNatMoney;
        private decimal iNatTax;
        private decimal iNatSum;
        private decimal iNatDisCount;
        private DateTime dArriveDate;
        private decimal iReceivedQTY;
        private decimal iReceivedNum;
        private decimal iReceivedMoney;
        private decimal iInvQTY;
        private decimal iInvNum;
        private decimal iInvMoney;
        private string cFree1;
        private string cFree2;
        private decimal iNatInvMoney;
        private decimal iOriTotal;
        private decimal iTotal;
        private decimal iPerTaxRate;
        private string cDefine22;
        private string cDefine23;
        private string cDefine24;
        private string cDefine25;
        private double cDefine26;
        private double cDefine27;
        private byte iflag;
        private string cItemCode;
        private string cItem_class;
        private int pPCIds;
        private string cItemName;
        private string cFree3;
        private string cFree4;
        private string cFree5;
        private string cFree6;
        private string cFree7;
        private string cFree8;
        private string cFree9;
        private string cFree10;
        private byte bGsp;
        private int pOID;
        private string cUnitID;
        private decimal iTaxPrice;
        private decimal iArrQTY;
        private decimal iArrNum;
        private decimal iArrMoney;
        private decimal iNatArrMoney;
        private int iAppIds;
        private string cDefine28;
        private string cDefine29;
        private string cDefine30;
        private string cDefine31;
        private string cDefine32;
        private string cDefine33;
        private int cDefine34;
        private int cDefine35;
        private DateTime cDefine36;
        private DateTime cDefine37;
        private int iSOsID;
        private bool bTaxCost;
        private string cSource;
        private string cbCloser;
        private int iPPartId;
        private decimal iPQuantity;
        private int iPTOSeq;
        private byte soType;
        private string soDId;
        private Guid contractRowGUID;
        private string cupsocode;
        private decimal iInvMPCost;
        private string contractCode;
        private string contractRowNo;
        private decimal fPoValidQuantity;
        private decimal fPoValidNum;
        private decimal fPoArrQuantity;
        private decimal fPoArrNum;
        private decimal fPoRetQuantity;
        private decimal fPoRetNum;
        private decimal fPoRefuseQuantity;
        private decimal fPoRefuseNum;
        private byte[] dUfts;
        private int iorderdid;
        private byte iordertype;
        private string csoordercode;
        private int iorderseq;
        private DateTime cbCloseTime;
        private DateTime cbCloseDate;
        private string cBG_ItemCode;
        private string cBG_ItemName;
        private string cBG_CaliberKey1;
        private string cBG_CaliberKeyName1;
        private string cBG_CaliberKey2;
        private string cBG_CaliberKeyName2;
        private string cBG_CaliberKey3;
        private string cBG_CaliberKeyName3;
        private string cBG_CaliberCode1;
        private string cBG_CaliberName1;
        private string cBG_CaliberCode2;
        private string cBG_CaliberName2;
        private string cBG_CaliberCode3;
        private string cBG_CaliberName3;
        private byte iBG_Ctrl;
        private string cBG_Auditopinion;
        private decimal fexquantity;
        private decimal fexnum;
        private int ivouchrowno;
        #endregion
        #region  属性
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string CPOID
        {
            get { return cPOID; }
            set { cPOID = value; }
        }

        public string CInvCode
        {
            get { return cInvCode; }
            set { cInvCode = value; }
        }

        public decimal IQuantity
        {
            get { return iQuantity; }
            set { iQuantity = value; }
        }

        public decimal INum
        {
            get { return iNum; }
            set { iNum = value; }
        }

        public decimal IQuotedPrice
        {
            get { return iQuotedPrice; }
            set { iQuotedPrice = value; }
        }

        public decimal IUnitPrice
        {
            get { return iUnitPrice; }
            set { iUnitPrice = value; }
        }

        public decimal IMoney
        {
            get { return iMoney; }
            set { iMoney = value; }
        }

        public decimal ITax
        {
            get { return iTax; }
            set { iTax = value; }
        }

        public decimal ISum
        {
            get { return iSum; }
            set { iSum = value; }
        }

        public decimal IDisCount
        {
            get { return iDisCount; }
            set { iDisCount = value; }
        }

        public decimal INatUnitPrice
        {
            get { return iNatUnitPrice; }
            set { iNatUnitPrice = value; }
        }

        public decimal INatMoney
        {
            get { return iNatMoney; }
            set { iNatMoney = value; }
        }

        public decimal INatTax
        {
            get { return iNatTax; }
            set { iNatTax = value; }
        }

        public decimal INatSum
        {
            get { return iNatSum; }
            set { iNatSum = value; }
        }

        public decimal INatDisCount
        {
            get { return iNatDisCount; }
            set { iNatDisCount = value; }
        }

        public DateTime DArriveDate
        {
            get { return dArriveDate; }
            set { dArriveDate = value; }
        }

        public decimal IReceivedQTY
        {
            get { return iReceivedQTY; }
            set { iReceivedQTY = value; }
        }

        public decimal IReceivedNum
        {
            get { return iReceivedNum; }
            set { iReceivedNum = value; }
        }

        public decimal IReceivedMoney
        {
            get { return iReceivedMoney; }
            set { iReceivedMoney = value; }
        }

        public decimal IInvQTY
        {
            get { return iInvQTY; }
            set { iInvQTY = value; }
        }

        public decimal IInvNum
        {
            get { return iInvNum; }
            set { iInvNum = value; }
        }

        public decimal IInvMoney
        {
            get { return iInvMoney; }
            set { iInvMoney = value; }
        }

        public string CFree1
        {
            get { return cFree1; }
            set { cFree1 = value; }
        }

        public string CFree2
        {
            get { return cFree2; }
            set { cFree2 = value; }
        }

        public decimal INatInvMoney
        {
            get { return iNatInvMoney; }
            set { iNatInvMoney = value; }
        }

        public decimal IOriTotal
        {
            get { return iOriTotal; }
            set { iOriTotal = value; }
        }

        public decimal ITotal
        {
            get { return iTotal; }
            set { iTotal = value; }
        }

        public decimal IPerTaxRate
        {
            get { return iPerTaxRate; }
            set { iPerTaxRate = value; }
        }

        public string CDefine22
        {
            get { return cDefine22; }
            set { cDefine22 = value; }
        }

        public string CDefine23
        {
            get { return cDefine23; }
            set { cDefine23 = value; }
        }

        public string CDefine24
        {
            get { return cDefine24; }
            set { cDefine24 = value; }
        }

        public string CDefine25
        {
            get { return cDefine25; }
            set { cDefine25 = value; }
        }

        public double CDefine26
        {
            get { return cDefine26; }
            set { cDefine26 = value; }
        }

        public double CDefine27
        {
            get { return cDefine27; }
            set { cDefine27 = value; }
        }

        public byte Iflag
        {
            get { return iflag; }
            set { iflag = value; }
        }

        public string CItemCode
        {
            get { return cItemCode; }
            set { cItemCode = value; }
        }

        public string CItem_class
        {
            get { return cItem_class; }
            set { cItem_class = value; }
        }

        public int PPCIds
        {
            get { return pPCIds; }
            set { pPCIds = value; }
        }

        public string CItemName
        {
            get { return cItemName; }
            set { cItemName = value; }
        }

        public string CFree3
        {
            get { return cFree3; }
            set { cFree3 = value; }
        }

        public string CFree4
        {
            get { return cFree4; }
            set { cFree4 = value; }
        }

        public string CFree5
        {
            get { return cFree5; }
            set { cFree5 = value; }
        }

        public string CFree6
        {
            get { return cFree6; }
            set { cFree6 = value; }
        }

        public string CFree7
        {
            get { return cFree7; }
            set { cFree7 = value; }
        }

        public string CFree8
        {
            get { return cFree8; }
            set { cFree8 = value; }
        }

        public string CFree9
        {
            get { return cFree9; }
            set { cFree9 = value; }
        }

        public string CFree10
        {
            get { return cFree10; }
            set { cFree10 = value; }
        }

        public byte BGsp
        {
            get { return bGsp; }
            set { bGsp = value; }
        }

        public int POID
        {
            get { return pOID; }
            set { pOID = value; }
        }

        public string CUnitID
        {
            get { return cUnitID; }
            set { cUnitID = value; }
        }

        public decimal ITaxPrice
        {
            get { return iTaxPrice; }
            set { iTaxPrice = value; }
        }

        public decimal IArrQTY
        {
            get { return iArrQTY; }
            set { iArrQTY = value; }
        }

        public decimal IArrNum
        {
            get { return iArrNum; }
            set { iArrNum = value; }
        }

        public decimal IArrMoney
        {
            get { return iArrMoney; }
            set { iArrMoney = value; }
        }

        public decimal INatArrMoney
        {
            get { return iNatArrMoney; }
            set { iNatArrMoney = value; }
        }

        public int IAppIds
        {
            get { return iAppIds; }
            set { iAppIds = value; }
        }

        public string CDefine28
        {
            get { return cDefine28; }
            set { cDefine28 = value; }
        }

        public string CDefine29
        {
            get { return cDefine29; }
            set { cDefine29 = value; }
        }

        public string CDefine30
        {
            get { return cDefine30; }
            set { cDefine30 = value; }
        }

        public string CDefine31
        {
            get { return cDefine31; }
            set { cDefine31 = value; }
        }

        public string CDefine32
        {
            get { return cDefine32; }
            set { cDefine32 = value; }
        }

        public string CDefine33
        {
            get { return cDefine33; }
            set { cDefine33 = value; }
        }

        public int CDefine34
        {
            get { return cDefine34; }
            set { cDefine34 = value; }
        }

        public int CDefine35
        {
            get { return cDefine35; }
            set { cDefine35 = value; }
        }

        public DateTime CDefine36
        {
            get { return cDefine36; }
            set { cDefine36 = value; }
        }

        public DateTime CDefine37
        {
            get { return cDefine37; }
            set { cDefine37 = value; }
        }

        public int ISOsID
        {
            get { return iSOsID; }
            set { iSOsID = value; }
        }

        public bool BTaxCost
        {
            get { return bTaxCost; }
            set { bTaxCost = value; }
        }

        public string CSource
        {
            get { return cSource; }
            set { cSource = value; }
        }

        public string CbCloser
        {
            get { return cbCloser; }
            set { cbCloser = value; }
        }

        public int IPPartId
        {
            get { return iPPartId; }
            set { iPPartId = value; }
        }

        public decimal IPQuantity
        {
            get { return iPQuantity; }
            set { iPQuantity = value; }
        }

        public int IPTOSeq
        {
            get { return iPTOSeq; }
            set { iPTOSeq = value; }
        }

        public byte SoType
        {
            get { return soType; }
            set { soType = value; }
        }

        public string SoDId
        {
            get { return soDId; }
            set { soDId = value; }
        }

        public Guid ContractRowGUID
        {
            get { return contractRowGUID; }
            set { contractRowGUID = value; }
        }

        public string Cupsocode
        {
            get { return cupsocode; }
            set { cupsocode = value; }
        }

        public decimal IInvMPCost
        {
            get { return iInvMPCost; }
            set { iInvMPCost = value; }
        }

        public string ContractCode
        {
            get { return contractCode; }
            set { contractCode = value; }
        }

        public string ContractRowNo
        {
            get { return contractRowNo; }
            set { contractRowNo = value; }
        }

        public decimal FPoValidQuantity
        {
            get { return fPoValidQuantity; }
            set { fPoValidQuantity = value; }
        }

        public decimal FPoValidNum
        {
            get { return fPoValidNum; }
            set { fPoValidNum = value; }
        }

        public decimal FPoArrQuantity
        {
            get { return fPoArrQuantity; }
            set { fPoArrQuantity = value; }
        }

        public decimal FPoArrNum
        {
            get { return fPoArrNum; }
            set { fPoArrNum = value; }
        }

        public decimal FPoRetQuantity
        {
            get { return fPoRetQuantity; }
            set { fPoRetQuantity = value; }
        }

        public decimal FPoRetNum
        {
            get { return fPoRetNum; }
            set { fPoRetNum = value; }
        }

        public decimal FPoRefuseQuantity
        {
            get { return fPoRefuseQuantity; }
            set { fPoRefuseQuantity = value; }
        }

        public decimal FPoRefuseNum
        {
            get { return fPoRefuseNum; }
            set { fPoRefuseNum = value; }
        }

        public byte[] DUfts
        {
            get { return dUfts; }
            set { dUfts = value; }
        }

        public int Iorderdid
        {
            get { return iorderdid; }
            set { iorderdid = value; }
        }

        public byte Iordertype
        {
            get { return iordertype; }
            set { iordertype = value; }
        }

        public string Csoordercode
        {
            get { return csoordercode; }
            set { csoordercode = value; }
        }

        public int Iorderseq
        {
            get { return iorderseq; }
            set { iorderseq = value; }
        }

        public DateTime CbCloseTime
        {
            get { return cbCloseTime; }
            set { cbCloseTime = value; }
        }

        public DateTime CbCloseDate
        {
            get { return cbCloseDate; }
            set { cbCloseDate = value; }
        }

        public string CBG_ItemCode
        {
            get { return cBG_ItemCode; }
            set { cBG_ItemCode = value; }
        }

        public string CBG_ItemName
        {
            get { return cBG_ItemName; }
            set { cBG_ItemName = value; }
        }

        public string CBG_CaliberKey1
        {
            get { return cBG_CaliberKey1; }
            set { cBG_CaliberKey1 = value; }
        }

        public string CBG_CaliberKeyName1
        {
            get { return cBG_CaliberKeyName1; }
            set { cBG_CaliberKeyName1 = value; }
        }

        public string CBG_CaliberKey2
        {
            get { return cBG_CaliberKey2; }
            set { cBG_CaliberKey2 = value; }
        }

        public string CBG_CaliberKeyName2
        {
            get { return cBG_CaliberKeyName2; }
            set { cBG_CaliberKeyName2 = value; }
        }

        public string CBG_CaliberKey3
        {
            get { return cBG_CaliberKey3; }
            set { cBG_CaliberKey3 = value; }
        }

        public string CBG_CaliberKeyName3
        {
            get { return cBG_CaliberKeyName3; }
            set { cBG_CaliberKeyName3 = value; }
        }

        public string CBG_CaliberCode1
        {
            get { return cBG_CaliberCode1; }
            set { cBG_CaliberCode1 = value; }
        }

        public string CBG_CaliberName1
        {
            get { return cBG_CaliberName1; }
            set { cBG_CaliberName1 = value; }
        }

        public string CBG_CaliberCode2
        {
            get { return cBG_CaliberCode2; }
            set { cBG_CaliberCode2 = value; }
        }

        public string CBG_CaliberName2
        {
            get { return cBG_CaliberName2; }
            set { cBG_CaliberName2 = value; }
        }

        public string CBG_CaliberCode3
        {
            get { return cBG_CaliberCode3; }
            set { cBG_CaliberCode3 = value; }
        }

        public string CBG_CaliberName3
        {
            get { return cBG_CaliberName3; }
            set { cBG_CaliberName3 = value; }
        }

        public byte IBG_Ctrl
        {
            get { return iBG_Ctrl; }
            set { iBG_Ctrl = value; }
        }

        public string CBG_Auditopinion
        {
            get { return cBG_Auditopinion; }
            set { cBG_Auditopinion = value; }
        }

        public decimal Fexquantity
        {
            get { return fexquantity; }
            set { fexquantity = value; }
        }

        public decimal Fexnum
        {
            get { return fexnum; }
            set { fexnum = value; }
        }

        public int Ivouchrowno
        {
            get { return ivouchrowno; }
            set { ivouchrowno = value; }
        }


     #endregion
    }
}
