using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 销售发票
    /// </summary>
    public class SaleBillVouch
    {
        /// <summary>
        /// 销售发票主表标识
        /// </summary>
        public int SBVID { get; set; }
        /// <summary>
        /// 销售发票号
        /// </summary>
        public string cSBVCode { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public string cVouchType { get; set; }
        /// <summary>
        /// 销售类型编码
        /// </summary>
        public string cSTCode { get; set; }

        /// <summary>
        /// 单据日期 
        /// </summary>
        public DateTime dDate { get; set; }
        /// <summary>
        /// 销售订单号 
        /// </summary>
        public string cSOCode { get; set; }

        /// <summary>
        /// 客户编码 
        /// </summary>
        public string cCusCode { get; set; }
        /// <summary>
        /// 客户名称 
        /// </summary>
        public string cCusName { get; set; }

        /// <summary>
        /// 发运方式
        /// </summary>
        public string cSCCode { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string cDefine13 { get; set; }

        /// <summary>
        /// 发货地址 
        /// </summary>
        public string cShipAddress { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string cMaker { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string cVerifier { get; set; }
    }
}
