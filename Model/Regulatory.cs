using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 监管码
    /// </summary>
    public class Regulatory
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 监管码
        /// </summary>
        public string RegCode { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 是否使用0：未使用，1：已使用
        /// </summary>
        public int IsUsed { get; set; }

        /// <summary>
        /// 账套号
        /// </summary>
        public string AccID { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页的大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
