using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        private string m_UserId;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        private string m_UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        private string m_Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        private string m_LoginString;
        /// <summary>
        /// 
        /// </summary>
        public string LoginString
        {
            get { return m_LoginString; }
            set { m_LoginString = value; }
        }

        

        private string m_ConnectionString;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }
        
        private string m_strAccid;
        /// <summary>
        /// 账套名
        /// </summary>
        public string Accid
        {
            get { return m_strAccid; }
            set { m_strAccid = value; }
        }

        private string m_Year;
        /// <summary>
        /// 年度
        /// </summary>
        public string Year
        {
            get { return m_Year; }
            set { m_Year = value; }
        }

        private DateTime m_LoginTime;
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime
        {
            get { return m_LoginTime; }
            set { m_LoginTime = value; }
        }

        private string m_ServiceUrl;
        /// <summary>
        /// 获得DB服务器的URL
        /// </summary>
        public string ServiceUrl
        {
            set { m_ServiceUrl = value; }
            get { return m_ServiceUrl; }
        }
        
        private string m_Version;
        /// <summary>
        /// 主程序版本
        /// </summary>
        public string Version
        {
            set { m_Version = value; }
            get { return m_Version; }
        }

        //private string m_CustomerName;
        //private string m_ErrorMsg = "";


        

    }
}
