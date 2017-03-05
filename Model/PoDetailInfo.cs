using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    //采购到货界面：采购订单显示信息类
    public class PoDetailInfo
    {
        //采购订单号，状态，存货编码，品名，（采购）数量，规格，是否质检，对应的条形码（69码），供应商，产地

        private string cPOID;
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string CPOID
        {
            get { return cPOID; }
            set { cPOID = value; }
        }

        private int cState;
        /// <summary>
        /// 单据状态
        /// </summary>
        public int CState
        {
            get { return cState; }
            set { cState = value; }
        }

        private string cInvCode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string CInvCode
        {
            get { return cInvCode; }
            set { cInvCode = value; }
        }

        private string cInvName;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string CInvName
        {
            get { return cInvName; }
            set { cInvName = value; }
        }

        private decimal iQuantity;
        /// <summary>
        /// （采购）数量
        /// </summary>
        public decimal IQuantity
        {
            get { return iQuantity; }
            set { iQuantity = value; }
        }


        private string cInvStd;
        /// <summary>
        /// 存货档案中的规格型号
        /// </summary>
        public string CInvStd
        {
            get { return cInvStd; }
            set { cInvStd = value; }
        }

        private bool bGsp;
        /// <summary>
        /// 是否质检1or 0
        /// </summary>
        public bool BGsp
        {
            get { return bGsp; }
            set { bGsp = value; }
        }

        private string cBarCode;
        /// <summary>
        /// 对应条形码（69码）
        /// </summary>
        public string CBarCode
        {
            get { return cBarCode; }
            set { cBarCode = value; }
        }

        private string cVenName;
        /// <summary>
        /// 供应商名
        /// </summary>
        public string CVenName
        {
            get { return cVenName; }
            set { cVenName = value; }
        }

        private string cAddress;
        /// <summary>
        /// 产地
        /// </summary>
        public string CAddress
        {
            get { return cAddress; }
            set { cAddress = value; }
        }

        
    }
}
