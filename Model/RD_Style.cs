using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RD_Style
    {
        private string m_crdcode;
        /// <summary>
        /// 收发类别编码
        /// </summary>
        public string crdcode
        {
            get { return m_crdcode; }
            set { m_crdcode = value; }
        }

        private string m_crdname;
        /// <summary>
        /// 收发类别名称
        /// </summary>
        public string crdname
        {
            get { return m_crdname; }
            set { m_crdname = value; }
        }

        private int m_brdflag;
        /// <summary>
        /// 收发类别标志
        /// </summary>
        public int brdflag
        {
            get { return m_brdflag; }
            set { m_brdflag = value; }
        }
        private int m_iRdGrade;
        /// <summary>
        /// 编码级次
        /// </summary>
        public int iRdGrade
        {
            get { return m_iRdGrade; }
            set { m_iRdGrade = value; }
        }
    }
}
