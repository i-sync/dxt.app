using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 货位
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 货位编码
        /// </summary>
        public string cPosCode
        {
            get;
            set;
        }

        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public float iQuantity
        {
            get;
            set;
        }
    }
}
