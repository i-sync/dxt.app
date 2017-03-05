using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DepartMent
    {
        public DepartMent()
        { 
        }

        private string m_cdepcode;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cdepcode
        {
            get { return m_cdepcode; }
            set { m_cdepcode = value; }
        }

        private string m_cdepname;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string cdepname
        {
            get { return m_cdepname; }
            set { m_cdepname = value; }
        }
    }
}
