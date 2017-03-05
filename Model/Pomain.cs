using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Pomain
    {
        //采购订单主表

         #region PO_Pomain  表字段
        private string cPOID;
        private DateTime dPODate;
        private string cVenCode;
        private string cDepCode;
        private string cPersonCode;
        private string cPTCode;
        private string cArrivalPlace;
        private string cSCCode;
        private string cexch_name;
        private double nflat;
        private double iTaxRate;
        private string cPayCode;
        private decimal iCost;
        private decimal iBargain;
        private string cMemo;
        private byte cState;
        private string cPeriod;
        private string cMaker;
        private string cVerifier;
        private string cCloser;
        private string cDefine1;
        private string cDefine2;
        private string cDefine3;
        private DateTime cDefine4;
        private int cDefine5;
        private DateTime cDefine6;
        private double cDefine7;
        private string cDefine8;
        private string cDefine9;
        private string cDefine10;
        private int pOID;
        private int iVTid;
        private byte[] ufts;
        private string cChanger;
        private string cBusType;
        private string cDefine11;
        private string cDefine12;
        private string cDefine13;
        private string cDefine14;
        private int cDefine15;
        private double cDefine16;
        private string cLocker;
        private byte iDiscountTaxType;
        private int iverifystateex;
        private int ireturncount;
        private bool isWfControlled;
        private DateTime cmaketime;
        private DateTime cModifyTime;
        private DateTime cAuditTime;
        private DateTime cAuditDate;
        private DateTime cModifyDate;
        private string cReviser;
        private string cVenPUOMProtocol;
        private string cChangVerifier;
        private DateTime cChangAuditTime;
        private DateTime cChangAuditDate;
        private short iBG_OverFlag;
        private string cBG_Auditor;
        private string cBG_AuditTime;
        private short controlResult;
        private int iflowid;

        private string cVenName;

        private List<PoDetail> list;



     #endregion
     #region  属性
        public string CPOID
        {
            get { return cPOID; }
            set { cPOID = value; }
        }
  
        public DateTime DPODate
        {
            get { return dPODate; }
            set { dPODate = value; }
        }
  
        public string CVenCode
        {
            get { return cVenCode; }
            set { cVenCode = value; }
        }
  
        public string CDepCode
        {
            get { return cDepCode; }
            set { cDepCode = value; }
        }
  
        public string CPersonCode
        {
            get { return cPersonCode; }
            set { cPersonCode = value; }
        }
  
        public string CPTCode
        {
            get { return cPTCode; }
            set { cPTCode = value; }
        }
  
        public string CArrivalPlace
        {
            get { return cArrivalPlace; }
            set { cArrivalPlace = value; }
        }
  
        public string CSCCode
        {
            get { return cSCCode; }
            set { cSCCode = value; }
        }
  
        public string Cexch_name
        {
            get { return cexch_name; }
            set { cexch_name = value; }
        }
  
        public double Nflat
        {
            get { return nflat; }
            set { nflat = value; }
        }
  
        public double ITaxRate
        {
            get { return iTaxRate; }
            set { iTaxRate = value; }
        }
  
        public string CPayCode
        {
            get { return cPayCode; }
            set { cPayCode = value; }
        }
  
        public decimal ICost
        {
            get { return iCost; }
            set { iCost = value; }
        }
  
        public decimal IBargain
        {
            get { return iBargain; }
            set { iBargain = value; }
        }
  
        public string CMemo
        {
            get { return cMemo; }
            set { cMemo = value; }
        }
  
        public byte CState
        {
            get { return cState; }
            set { cState = value; }
        }
  
        public string CPeriod
        {
            get { return cPeriod; }
            set { cPeriod = value; }
        }
  
        public string CMaker
        {
            get { return cMaker; }
            set { cMaker = value; }
        }
  
        public string CVerifier
        {
            get { return cVerifier; }
            set { cVerifier = value; }
        }
  
        public string CCloser
        {
            get { return cCloser; }
            set { cCloser = value; }
        }
  
        public string CDefine1
        {
            get { return cDefine1; }
            set { cDefine1 = value; }
        }
  
        public string CDefine2
        {
            get { return cDefine2; }
            set { cDefine2 = value; }
        }
  
        public string CDefine3
        {
            get { return cDefine3; }
            set { cDefine3 = value; }
        }
  
        public DateTime CDefine4
        {
            get { return cDefine4; }
            set { cDefine4 = value; }
        }
  
        public int CDefine5
        {
            get { return cDefine5; }
            set { cDefine5 = value; }
        }
  
        public DateTime CDefine6
        {
            get { return cDefine6; }
            set { cDefine6 = value; }
        }
  
        public double CDefine7
        {
            get { return cDefine7; }
            set { cDefine7 = value; }
        }
  
        public string CDefine8
        {
            get { return cDefine8; }
            set { cDefine8 = value; }
        }
  
        public string CDefine9
        {
            get { return cDefine9; }
            set { cDefine9 = value; }
        }
  
        public string CDefine10
        {
            get { return cDefine10; }
            set { cDefine10 = value; }
        }
  
        public int POID
        {
            get { return pOID; }
            set { pOID = value; }
        }
  
        public int IVTid
        {
            get { return iVTid; }
            set { iVTid = value; }
        }
  
        public byte[] Ufts
        {
            get { return ufts; }
            set { ufts = value; }
        }
  
        public string CChanger
        {
            get { return cChanger; }
            set { cChanger = value; }
        }
  
        public string CBusType
        {
            get { return cBusType; }
            set { cBusType = value; }
        }
  
        public string CDefine11
        {
            get { return cDefine11; }
            set { cDefine11 = value; }
        }
  
        public string CDefine12
        {
            get { return cDefine12; }
            set { cDefine12 = value; }
        }
  
        public string CDefine13
        {
            get { return cDefine13; }
            set { cDefine13 = value; }
        }
  
        public string CDefine14
        {
            get { return cDefine14; }
            set { cDefine14 = value; }
        }
  
        public int CDefine15
        {
            get { return cDefine15; }
            set { cDefine15 = value; }
        }
  
        public double CDefine16
        {
            get { return cDefine16; }
            set { cDefine16 = value; }
        }
  
        public string CLocker
        {
            get { return cLocker; }
            set { cLocker = value; }
        }
  
        public byte IDiscountTaxType
        {
            get { return iDiscountTaxType; }
            set { iDiscountTaxType = value; }
        }
  
        public int Iverifystateex
        {
            get { return iverifystateex; }
            set { iverifystateex = value; }
        }
  
        public int Ireturncount
        {
            get { return ireturncount; }
            set { ireturncount = value; }
        }
  
        public bool IsWfControlled
        {
            get { return isWfControlled; }
            set { isWfControlled = value; }
        }
  
        public DateTime Cmaketime
        {
            get { return cmaketime; }
            set { cmaketime = value; }
        }
  
        public DateTime CModifyTime
        {
            get { return cModifyTime; }
            set { cModifyTime = value; }
        }
  
        public DateTime CAuditTime
        {
            get { return cAuditTime; }
            set { cAuditTime = value; }
        }
  
        public DateTime CAuditDate
        {
            get { return cAuditDate; }
            set { cAuditDate = value; }
        }
  
        public DateTime CModifyDate
        {
            get { return cModifyDate; }
            set { cModifyDate = value; }
        }
  
        public string CReviser
        {
            get { return cReviser; }
            set { cReviser = value; }
        }
  
        public string CVenPUOMProtocol
        {
            get { return cVenPUOMProtocol; }
            set { cVenPUOMProtocol = value; }
        }
  
        public string CChangVerifier
        {
            get { return cChangVerifier; }
            set { cChangVerifier = value; }
        }
  
        public DateTime CChangAuditTime
        {
            get { return cChangAuditTime; }
            set { cChangAuditTime = value; }
        }
  
        public DateTime CChangAuditDate
        {
            get { return cChangAuditDate; }
            set { cChangAuditDate = value; }
        }
  
        public short IBG_OverFlag
        {
            get { return iBG_OverFlag; }
            set { iBG_OverFlag = value; }
        }
  
        public string CBG_Auditor
        {
            get { return cBG_Auditor; }
            set { cBG_Auditor = value; }
        }
  
        public string CBG_AuditTime
        {
            get { return cBG_AuditTime; }
            set { cBG_AuditTime = value; }
        }
  
        public short ControlResult
        {
            get { return controlResult; }
            set { controlResult = value; }
        }
  
        public int Iflowid
        {
            get { return iflowid; }
            set { iflowid = value; }
        }

        public string CVenName
        {
            get { return cVenName; }
            set { cVenName = value; }
        }

        public List<PoDetail> List
        {
            get { return list; }
            set { list = value; }
        }
  
    #endregion
    
    }
}
