using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Model
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Competence
    {
        /// <summary>
        /// 采购到货(PU04200102:到货单录入,PU04200105:到货单审核)
        /// </summary>
        public bool CGDH
        {
            get;
            set;
        }
        /// <summary>
        /// 采购入库（包括红字）(ASM0102:采购入库单录入,ASM0103:采购入库单审核)
        /// </summary>
        public bool CGRK
        {
            get;
            set;
        }
        /// <summary>
        /// 采购退货GSP(GS03040102:采购退货出库质量复核记录单录入,GS03040103:采购退货出库质量复核记录单审核)
        /// </summary>
        public bool CGTHGSP
        {
            get;
            set;
        }
        /// <summary>
        /// 销售(发货)出库拣货(SA03020101:发货单录入|SA03040101:委托代销发货单录入)
        /// </summary>
        public bool XSFH
        { 
            get;
            set; 
        }
        /// <summary>
        /// 销售出库GSP(GS03030102:销售出库质量复核记录单录入,GS03030103:销售出库质量复核记录单审核|GS03030202:中药材、饮片销售出库质量复核记录单录入,GS03030203:中药材、饮片销售出库质量复核记录单审核)
        /// </summary>
        public bool XSCKGSP
        {
            get;
            set;
        }
        /// <summary>
        /// 销售退货GSP(GS03020102:销售退货质量验收记录单录入,GS03020103:销售退货质量验收记录单审核)
        /// </summary>
        public bool XSTHGSP
        {
            get;
            set;
        }
        /// <summary>
        /// 销售出库(红字)(ASM0202:销售出库单录入,ASM0203:销售出库单审核)
        /// </summary>
        public bool XSCK
        {
            get;
            set;
        }
        /// <summary>
        /// 产成品入库(ASM0302:产成品入库单录入,ASM0303:产成品入库单审核)
        /// </summary>
        public bool CCPRK
        {
            get;
            set;
        }
        /// <summary>
        /// 盘点(ST010202:盘点单录入)
        /// </summary>
        public bool PD
        {
            get;
            set;
        }
        /// <summary>
        /// 材料出库(ASM0402:材料出库单录入,ASM0403:材料出库单审核)
        /// </summary>
        public bool CLCK
        {
            get;
            set;
        }
        /// <summary>
        /// 委外到货(OM04200102:(委外)到货单录入,OM04200105:(委外)到货单审核)
        /// </summary>
        public bool WWDH
        {
            get;
            set;
        }
        /// <summary>
        /// 其它出库(ASM0603:其他出库单审核)
        /// </summary>
        public bool QTCK
        {
            get;
            set;
        }
        /// <summary>
        /// 其它入库(ASM0503:其他入库单审核)
        /// </summary>
        public bool QTRK
        {
            get;
            set;
        }
        /// <summary>
        /// 货位管理(ASM0202:销售出库单录入)
        /// </summary>
        public bool HWGL
        {
            get;
            set;
        }

        public Competence() { }
        public Competence(DataRow row)
        {
            CGDH = Convert.ToInt32(row["CGDH"]) == 1 ? true : false;
            CGRK = Convert.ToInt32(row["CGRK"]) == 1 ? true : false;
            CGTHGSP = Convert.ToInt32(row["CGTHGSP"]) == 1 ? true : false;
            XSFH = Convert.ToInt32(row["XSFH"]) == 1 ? true : false;
            XSCKGSP = Convert.ToInt32(row["XSCKGSP"]) == 1 ? true : false;
            XSTHGSP = Convert.ToInt32(row["XSTHGSP"]) == 1 ? true : false;
            XSCK = Convert.ToInt32(row["XSCK"]) == 1 ? true : false;
            CCPRK = Convert.ToInt32(row["CCPRK"]) == 1 ? true : false;
            PD = Convert.ToInt32(row["PD"]) == 1 ? true : false;
            CLCK = Convert.ToInt32(row["CLCK"]) == 1 ? true : false;
            WWDH = Convert.ToInt32(row["WWDH"]) == 1 ? true : false;
            QTCK = Convert.ToInt32(row["QTCK"]) == 1 ? true : false;
            QTRK = Convert.ToInt32(row["QTRK"]) == 1 ? true : false;
            HWGL = Convert.ToInt32(row["HWGL"]) == 1 ? true : false;
        }
    }
}
