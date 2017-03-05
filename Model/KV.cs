using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class KV
    {
        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        public string Name
        {
            get
            {
                return string.Format("[{0}]{1}", Key, Value);
            }
        }
    }
}
