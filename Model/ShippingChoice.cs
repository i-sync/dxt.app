using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 快递方式
    /// </summary>
    public class ShippingChoice
    {
        /// <summary>
        /// 发运方式编码
        /// </summary>
        public string cSCCode { get; set; }
        /// <summary>
        /// 发运方式名称
        /// </summary>
        public string cSCName { get; set; }
    }
}
