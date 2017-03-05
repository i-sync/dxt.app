using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace U8DataAccess
{
    public class LabelPrint
    {
        //标签中二维内容
        //根据单据类型（采购订单），单据号，取得：物料号（存货档案－存货编码）、
        //品名，规格，产地，净含量，对应条形码（69码），生产账套存货编码、销售账套存货编码
        //供应商原厂批次代码，

        //先根据单据号，单据类型，取出该单据下的物料名cInvName，存货编码cInvCode

        //点击品名下拉框时，根据存货编码，取出规格，产地，净含量，对应条形码（69码）

    }
   
}
