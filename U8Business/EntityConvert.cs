using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;
using System.Reflection;

namespace U8Business
{
    /// <summary>
    /// 实体转换
    /// </summary>
    public class EntityConvert
    {

        /// <summary>
        /// 同一个类的属性赋值
        /// </summary>
        /// <param name="s">来源类</param>
        /// <param name="t">目标类</param>
        /// <returns></returns>
        public static T ConvertClass<S, T>(S s, T t)
        {
            //获取来源类的所有属性,目标类的所有属性
            PropertyInfo[] sPI = typeof(S).GetProperties();
            //PropertyInfo[] tPI = typeof(T).GetProperties();
            Type type = typeof(T);
            PropertyInfo propertyInfo;
            //循环遍历,相同属性赋值
            foreach (PropertyInfo spi in sPI)
            {
                //判断来源类某个属性是否为数组或泛型
                if (spi.PropertyType.IsArray || spi.PropertyType.IsGenericType)
                    continue;
                //判断属性是否为自定义类类型（字符串也是类类型，但它是密封的）
                if (spi.PropertyType.IsClass && !spi.PropertyType.IsSealed)
                    continue;

                propertyInfo = type.GetProperty(spi.Name);
                if (spi.PropertyType.IsEnum)
                {
                    object obj = spi.GetValue(s, null);
                    //tpi.SetValue(t,Enum.ToObject(typeof(T).GetProperty(spi.Name).PropertyType,obj),null);
                    propertyInfo.SetValue(t, Enum.ToObject(propertyInfo.PropertyType, obj), null);
                    continue;
                }
                //判断属性是否可写
                if (!spi.CanWrite)
                    continue;
                //相同属性名称,获取来源实体对象该属性的值赋值为目标实体对象的该属性
                //tpi.SetValue(t, spi.GetValue(s, null), null);
                propertyInfo.SetValue(t, spi.GetValue(s, null), null);
            }
            return t;
        }

        /// <summary>
        /// 把数据行转换为ArrivalVouch实体对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static ArrivalVouch ConvertToArrivalVouch(DataRow row)
        {
            ArrivalVouch arrivalVouch = new ArrivalVouch();
            //arrivalVouch.VT_ID = Cast.ToInteger(row["ivtid"]);
            //arrivalVouch.ID = Cast.ToInteger(row["id"]);
            //arrivalVouch.cCode = Cast.ToString(row["ccode"]);

            arrivalVouch.iExchRate = Cast.ToDecimal(row["iexchrate"]);
            arrivalVouch.iTaxRate = Cast.ToDecimal(row["itaxrate"]);
            arrivalVouch.iFlowId = Cast.ToInteger(row["iflowid"]);
            arrivalVouch.cBusType = Cast.ToString(row["cbustype"]);
            arrivalVouch.cOrderCode = Cast.ToString(row["cOrderCode"]);
            arrivalVouch.dDate = Cast.ToString(row["dPODate"]);
            arrivalVouch.cVenCode = Cast.ToString(row["cvencode"]);
            arrivalVouch.cVenAbbName = Cast.ToString(row["cVenAbbName"]);
            arrivalVouch.cDepCode = Cast.ToString(row["cdepcode"]);
            arrivalVouch.cDepName = Cast.ToString(row["cdepname"]);
            arrivalVouch.cPersonCode = Cast.ToString(row["cpersoncode"]);
            arrivalVouch.cPersonName = Cast.ToString(row["cpersonname"]);
            arrivalVouch.cSCCode = Cast.ToString(row["csccode"]);
            arrivalVouch.cMemo = Cast.ToString(row["cmemo"]);
            arrivalVouch.cPTCode = Cast.ToString(row["cptcode"]);
            arrivalVouch.cPayCode = Cast.ToString(row["cpaycode"]);
            arrivalVouch.Define1 = Cast.ToString(row["cdefine1"]);
            arrivalVouch.Define2 = Cast.ToString(row["cdefine2"]);
            arrivalVouch.Define3 = Cast.ToString(row["cdefine3"]);
            arrivalVouch.Define4 = Cast.ToDateTime(row["cdefine4"]);
            arrivalVouch.Define5 = Cast.ToInteger(row["cdefine5"]);
            arrivalVouch.Define6 = Cast.ToDateTime(row["cdefine6"]);
            arrivalVouch.Define7 = Cast.ToInteger(row["cdefine7"]);
            arrivalVouch.Define8 = Cast.ToString(row["cdefine8"]);
            arrivalVouch.Define9 = Cast.ToString(row["cdefine9"]);
            arrivalVouch.Define10 = Cast.ToString(row["cdefine10"]);
            arrivalVouch.Define11 = Cast.ToString(row["cdefine11"]);
            arrivalVouch.Define12 = Cast.ToString(row["cdefine12"]);
            arrivalVouch.Define13 = Cast.ToString(row["cdefine13"]);
            arrivalVouch.Define14 = Cast.ToString(row["cdefine14"]);
            arrivalVouch.Define15 = Cast.ToInteger(row["cdefine15"]);
            arrivalVouch.Define16 = Cast.ToDecimal(row["cdefine16"]);

            arrivalVouch.cExch_Name = Cast.ToString(row["cexch_name"]);
            arrivalVouch.cVenPUOMProtocol = Cast.ToString(row["cvenpuomprotocol"]);
            
            /*
             * 
             * 
            arrivalVouch.iExchRate = Cast.ToDecimal(row["iexchrate"]);
            arrivalVouch.iTaxRate = Cast.ToDecimal(row["itaxrate"]);
            arrivalVouch.cMaker = Cast.ToString(row["cmaker"]);
            arrivalVouch.bNegative = Cast.ToInteger(row["bnegative"]);
            arrivalVouch.cCloser = Cast.ToString(row["ccloser"]);
            arrivalVouch.iDiscountTaxType = Cast.ToString(row["idiscounttaxtype"]);
            arrivalVouch.iBillType = Cast.ToString(row["ibilltype"]);
            arrivalVouch.cVouchType = Cast.ToString(row["cvouchtype"]);
            arrivalVouch.cGeneralOrderCode = Cast.ToString(row["cgeneralordercode"]);
            arrivalVouch.cTmCode = Cast.ToString(row["ctmcode"]);
            arrivalVouch.cIncotermCode = Cast.ToString(row["cincotermcode"]);
            arrivalVouch.cTransOrderCode = Cast.ToString(row["ctransordercode"]);
            arrivalVouch.dPortDate = Cast.ToString(row["dportdate"]);
            arrivalVouch.cSportCode = Cast.ToString(row["csportcode"]);
            arrivalVouch.cAportCode = Cast.ToString(row["caportcode"]);
            arrivalVouch.cSvenCode = Cast.ToString(row["csvencode"]);
            arrivalVouch.cArrivalPlace = Cast.ToString(row["carrivalplace"]);
            arrivalVouch.dCloseDate = Cast.ToString(row["dclosedate"]);
            arrivalVouch.iDec = Cast.ToInteger(row["idec"]);
            arrivalVouch.bCal = Cast.ToBoolean(row["bcal"]);
            arrivalVouch.Guid = Cast.ToString(row["guid"]);
            arrivalVouch.iVerifyState = Cast.ToInteger(row["iverifystate"]);
            arrivalVouch.cAuditDate = Cast.ToString(row["cauditdate"]);
            arrivalVouch.cVerifier = Cast.ToString(row["cverifier"]);
            arrivalVouch.iVerifyStateex = Cast.ToInteger(row["iverifystateex"]);
            arrivalVouch.iReturnCount = Cast.ToInteger(row["ireturncount"]);
            arrivalVouch.isWfContRolled = Cast.ToBoolean(row["iswfcontrolled"]);
            arrivalVouch.cChanger = Cast.ToString(row["cchanger"]);
            arrivalVouch.cVenName = Cast.ToString(row["cvenname"]);
            arrivalVouch.cAddress = Cast.ToString(row["cadrowess"]);
            arrivalVouch.ufts = Cast.ToString(row["ufts"]);

             * 
             */ 
            return arrivalVouch;
        }

        /// <summary>
        /// 把数据行转换为ArrivalVouchs实体对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static ArrivalVouchs ConvertToArrivalVouchs(DataRow row)
        {
            ArrivalVouchs arrivalVouchs = new ArrivalVouchs();
            arrivalVouchs.ID = Cast.ToInteger(row["poid"]);//采购订单主表ID
            arrivalVouchs.Autoid = Cast.ToInteger(row["id"]);//采购订单子表ID

            //是否批次管理
            arrivalVouchs.bInvBatch = Cast.ToBoolean(row["bInvBatch"]);
            //是否保质期管理
            arrivalVouchs.bInvQuality = Cast.ToBoolean(row["bInvQuality"]);

            arrivalVouchs.cVenCode = Cast.ToString(row["cvencode"]); 
            arrivalVouchs.iExchRate = Cast.ToDecimal(row["iexchrate"]);
            //arrivalVouchs.ID = Cast.ToInteger(row["id"]);
            arrivalVouchs.bTaxCost = Cast.ToBoolean(row["btaxcost"]);
            arrivalVouchs.cInvCode = Cast.ToString(row["cinvcode"]);
            arrivalVouchs.cInvName = Cast.ToString(row["cinvname"]);
            arrivalVouchs.cInvStd = Cast.ToString(row["cinvstd"]);
            arrivalVouchs.cInvm_Unit = Cast.ToString(row["cinvm_unit"]);
            arrivalVouchs.bGsp = Cast.ToBoolean(row["bgsp"]);
            arrivalVouchs.iMassDate = Cast.ToInteger(row["imassdate"]);
            arrivalVouchs.cMassUnit = Cast.ToInteger(row["cmassunit"]);
            arrivalVouchs.Quantity = Cast.ToDecimal(row["iquantity"]);//订单数量
            arrivalVouchs.iArrQty = Cast.ToDecimal(row["iArrQty"]);//到货数量
            arrivalVouchs.iNum = Cast.ToDecimal(row["inum"]);
            arrivalVouchs.iInvexchRate = Cast.ToDecimal(row["iinvexchrate"]);

            arrivalVouchs.iunitprice = Cast.ToDecimal(row["iunitprice"]);//原币无税单价
            arrivalVouchs.iTaxPrice = Cast.ToDecimal(row["iTaxPrice"]);//原币含税单价
            arrivalVouchs.iTax = Cast.ToDecimal(row["iTax"]);//原币税额
            arrivalVouchs.iSum = Cast.ToDecimal(row["isum"]);//原币价税合计
            arrivalVouchs.iMoney = Cast.ToDecimal(row["iMoney"]);//原币无税金额

            arrivalVouchs.inatunitprice = Cast.ToDecimal(row["inatunitprice"]);
            arrivalVouchs.iVouchRowNo = Cast.ToInteger(row["ivouchrowno"]);
            arrivalVouchs.inatmoney = Cast.ToDecimal(row["inatmoney"]);
            arrivalVouchs.inattax = Cast.ToDecimal(row["inattax"]);
            arrivalVouchs.inatsum = Cast.ToDecimal(row["inatsum"]);

            arrivalVouchs.iTaxRate = Cast.ToDecimal(row["itaxrate"]);
            arrivalVouchs.Free1 = Cast.ToString(row["cfree1"]);
            arrivalVouchs.Free2 = Cast.ToString(row["cfree2"]);
            arrivalVouchs.Free3 = Cast.ToString(row["cfree3"]);
            arrivalVouchs.Free4 = Cast.ToString(row["cfree4"]);
            arrivalVouchs.Free5 = Cast.ToString(row["cfree5"]);
            arrivalVouchs.Free6 = Cast.ToString(row["cfree6"]);
            arrivalVouchs.Free7 = Cast.ToString(row["cfree7"]);
            arrivalVouchs.Free8 = Cast.ToString(row["cfree8"]);
            arrivalVouchs.Free9 = Cast.ToString(row["cfree9"]);
            arrivalVouchs.Free10 = Cast.ToString(row["cfree10"]);
            arrivalVouchs.Define22 = Cast.ToString(row["cdefine22"]);//产地
            arrivalVouchs.Define23 = Cast.ToString(row["cdefine23"]);
            arrivalVouchs.Define24 = Cast.ToString(row["cdefine24"]);
            arrivalVouchs.Define25 = Cast.ToString(row["cdefine25"]);
            arrivalVouchs.Define26 = Cast.ToDecimal(row["cdefine26"]);
            arrivalVouchs.Define27 = Cast.ToDecimal(row["cdefine27"]);
            arrivalVouchs.Define28 = Cast.ToString(row["cdefine28"]);
            arrivalVouchs.Define29 = Cast.ToString(row["cdefine29"]);
            arrivalVouchs.Define30 = Cast.ToString(row["cdefine30"]);
            arrivalVouchs.Define31 = Cast.ToString(row["cdefine31"]);
            arrivalVouchs.Define32 = Cast.ToString(row["cdefine32"]);
            arrivalVouchs.Define33 = Cast.ToString(row["cdefine33"]);
            arrivalVouchs.Define34 = Cast.ToInteger(row["cdefine34"]);
            arrivalVouchs.Define35 = Cast.ToInteger(row["cdefine35"]);
            arrivalVouchs.Define36 = Cast.ToDateTime(row["cdefine36"]);
            arrivalVouchs.Define37 = Cast.ToDateTime(row["cdefine37"]);
            arrivalVouchs.cItem_class = Cast.ToString(row["citem_class"]);
            arrivalVouchs.cItemCode = Cast.ToString(row["citemcode"]);
            arrivalVouchs.cItemName = Cast.ToString(row["citemname"]);


            arrivalVouchs.cinvdefine1 = Cast.ToString(row["cinvdefine1"]);
            arrivalVouchs.cinvdefine2 = Cast.ToString(row["cinvdefine2"]);
            arrivalVouchs.cinvdefine3 = Cast.ToString(row["cinvdefine3"]);
            arrivalVouchs.cinvdefine4 = Cast.ToString(row["cinvdefine4"]);
            arrivalVouchs.cinvdefine5 = Cast.ToString(row["cinvdefine5"]);
            arrivalVouchs.cinvdefine6 = Cast.ToString(row["cinvdefine6"]);
            arrivalVouchs.cinvdefine7 = Cast.ToString(row["cinvdefine7"]);
            arrivalVouchs.cinvdefine8 = Cast.ToString(row["cinvdefine8"]);
            arrivalVouchs.cinvdefine9 = Cast.ToString(row["cinvdefine9"]);
            arrivalVouchs.cinvdefine10 = Cast.ToString(row["cinvdefine10"]);
            arrivalVouchs.cinvdefine11 = Cast.ToString(row["cinvdefine11"]);
            arrivalVouchs.cinvdefine12 = Cast.ToString(row["cinvdefine12"]);
            arrivalVouchs.cinvdefine13 = Cast.ToString(row["cinvdefine13"]);
            arrivalVouchs.cinvdefine14 = Cast.ToString(row["cinvdefine14"]);
            arrivalVouchs.cinvdefine15 = Cast.ToString(row["cinvdefine15"]);
            arrivalVouchs.cinvdefine16 = Cast.ToString(row["cinvdefine16"]);


            arrivalVouchs.ContractCode = Cast.ToString(row["contractcode"]);
            arrivalVouchs.ContractRowNo = Cast.ToString(row["contractrowno"]);
            arrivalVouchs.ContractRowGUID = Cast.ToString(row["contractrowguid"]);
            arrivalVouchs.cSoCode = Cast.ToString(row["csocode"]);
            arrivalVouchs.SoType = Cast.ToInteger(row["sotype"]);
            arrivalVouchs.SoDId = Cast.ToString(row["sodid"]);
            arrivalVouchs.cWhCode = Cast.ToString(row["cwhcode"]);
            arrivalVouchs.cWhName = Cast.ToString(row["cwhname"]);
            arrivalVouchs.iInvMPCost = Cast.ToDecimal(row["iinvmpcost"]);
            arrivalVouchs.cOrderCode = Cast.ToString(row["cordercode"]);
            arrivalVouchs.dArriveDate = Cast.ToString(row["darrivedate"]);
            arrivalVouchs.iOrderdId = Cast.ToInteger(row["iorderdid"]);
            arrivalVouchs.iOrderType = Cast.ToInteger(row["iordertype"]);
            arrivalVouchs.cSoOrderCode = Cast.ToString(row["csoordercode"]);
            arrivalVouchs.iOrderSeq = Cast.ToInteger(row["iorderseq"]);
            arrivalVouchs.cDemandMemo = Cast.ToString(row["cdemandmemo"]);

            arrivalVouchs.iExpiratDateCalcu = Cast.ToInteger(row["iexpiratdatecalcu"]);
            //arrivalVouchs.cExpirationDate = Cast.ToString(row["cexpirationdate"]);
            //arrivalVouchs.dExpirationDate = Cast.ToDateTime(row["dexpirationdate"]);
            arrivalVouchs.iOrderRowNo = Cast.ToInteger(row["irowno"]);//订单行号
            
            //委外
            //arrivalVouchs.cMoDetailsID = Cast.ToString(row["modetailsID"]);
            return arrivalVouchs;
        }


    }
}
