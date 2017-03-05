using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace U8Business
{
    /// <summary>
    /// 监管码的管理
    /// </summary>
    public class Regulatory
    {
        /// <summary>
        /// 获取单个监管码对象
        /// </summary>
        /// <remarks>tianzhenyun 2012-12-09</remarks>
        public static Model.Regulatory GetModel(out string errMsg)
        {
            Common co = Common.GetInstance();

            Service.Regulatory temp = new U8Business.Service.Regulatory();
            temp.AccID = Common.CurrentUser.Accid;
            temp= co.Service.GetRegulatoryModel(Common.CurrentUser.ConnectionString,temp, out errMsg);

            Model.Regulatory data ;
            if(temp ==null)
                return null;
            data = new Model.Regulatory ();
            data.RegCode = temp.RegCode;
            return data;
        }

        /// <summary>
        /// 更新监管码的使用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateRegulatory(Model.Regulatory data)
        {
            string errMsg;
            Common co = Common.GetInstance();
            Service.Regulatory temp = new U8Business.Service.Regulatory();
            temp.RegCode = data.RegCode;
            temp.CardNumber = data.CardNumber;
            temp.CardName = data.CardName;
            temp.CardCode = data.CardCode;
            //添加账套号
            temp.AccID = Common.CurrentUser.Accid;
            bool flag = co.Service.UpdateRegulatory(Common.CurrentUser.ConnectionString, temp, out errMsg);
            return flag;
        }
    }
}
