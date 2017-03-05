using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Warehouse
    {
        private string m_cwhname;
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cwhname
        {
            get { return m_cwhname; }
            set { m_cwhname = value; }
        }

        private string m_cwhcode;
        /// <summary>
        /// 仓库代码
        /// </summary>
        public string cwhcode
        {
            get { return m_cwhcode; }
            set { m_cwhcode = value; }
        }

        private string m_Address;
        /// <summary>
        /// 仓库地址
        /// </summary>
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }

        private string m_Phone;
        /// <summary>
        /// 仓库电话
        /// </summary>
        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        private string m_Person;
        /// <summary>
        /// 负责人
        /// </summary>
        public string Person
        {
            get { return m_Person; }
            set { m_Person = value; }
        }

        private int m_bwhpos;
        /// <summary>
        /// 是否做库位管理
        /// </summary>
        public int bwhpos
        {
            get { return m_bwhpos; }
            set { m_bwhpos = value; }
        }

        private bool m_isFreezen;
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool isFreezen
        {
            get { return m_isFreezen; }
            set { m_isFreezen = value; }
        }

        private bool m_isShop;
        /// <summary>
        /// 是否是门店
        /// </summary>
        public bool isShop
        {
            get { return m_isShop; }
            set { m_isShop = value; }
        }
    }
}
