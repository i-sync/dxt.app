using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 现存量查询
    /// </summary>
    public class IQuantitySearch
    {
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode
        { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName
        { get; set; }

        /// <summary>
        /// 货位编码
        /// </summary>
        public string cPosCode
        { get; set; }

        /// <summary>
        /// 货位名称
        /// </summary>
        public string cPosName
        { get; set; }

        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode
        { get; set; }

        /// <summary>
        /// 存货名称
        /// </summary>
        public string cInvName
        { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string cInvStd
        { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string cInvDefine1
        {
            get;
            set;
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string cInvDefine6
        {
            get;
            set;
        }

        /// <summary>
        /// 批次
        /// </summary>
        public string cBatch
        { get; set; }

        /// <summary>
        /// 结存数量
        /// </summary>
        public decimal iQuantity
        { get; set; }


        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime dMdate
        { get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime cExpirationdate
        { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime dVDate
        {
            get;
            set;
        }

        /// <summary>
        /// 保质期
        /// </summary>
        public int iMassDate
        { get; set; }

        /// <summary>
        /// 保质期单位
        /// </summary>
        public string cMassUnit
        { get; set; }
    }
}
