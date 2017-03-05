using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class STInProductDetail
    {
        #region 属性
        private string m_autoid;
        public string autoid
        {
            get { return m_autoid; }
            set { m_autoid = value; }
        }

        private int m_bcosting;
        /// <summary>
        /// 是否核算
        /// </summary>
        public int bcosting
        {
            get { return m_bcosting; }
            set { m_bcosting = value; }
        }

        private bool m_bgsp;
        /// <summary>
        /// 是否质检
        /// </summary>
        public bool bgsp
        {
            get { return m_bgsp; }
            set { m_bgsp = value; }
        }

        private int m_bvmiused;
        /// <summary>
        /// 代管消耗标识
        /// </summary>
        public int bvmiused
        {
            get { return m_bvmiused; }
            set { m_bvmiused = value; }
        }

        private string m_cassunit;
        /// <summary>
        /// 辅计量单位编码
        /// </summary>
        public string cassunit
        {
            get { return m_cassunit; }
            set { m_cassunit = value; }
        }

        private string m_cbaccounter;
        /// <summary>
        /// 记账人
        /// </summary>
        public string cbaccounter
        {
            get { return m_cbaccounter; }
            set { m_cbaccounter = value; }
        }

        private string m_cbarcode;
        /// <summary>
        /// 条形码
        /// </summary>
        public string cbarcode
        {
            get { return m_cbarcode; }
            set { m_cbarcode = value; }
        }

        private string m_cbatch;
        /// <summary>
        /// 批号
        /// </summary>
        public string cbatch
        {
            get { return m_cbatch; }
            set { m_cbatch = value; }
        }

        private string m_cbvencode;
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cbvencode
        {
            get { return m_cbvencode; }
            set { m_cbvencode = value; }
        }

        private string m_ccusinvcode;
        /// <summary>
        /// 客户存货编码
        /// </summary>
        public string ccusinvcode
        {
            get { return m_ccusinvcode; }
            set { m_ccusinvcode = value; }
        }

        private string m_ccusinvname;
        /// <summary>
        /// 客户存货名称
        /// </summary>
        public string ccusinvname
        {
            get { return m_ccusinvname; }
            set { m_ccusinvname = value; }
        }

        private string m_cdefine22;
        /// <summary>
        /// 表体自定义项1
        /// </summary>
        public string cdefine22
        {
            get { return m_cdefine22; }
            set { m_cdefine22 = value; }
        }

        private string m_cdefine23;
        /// <summary>
        /// 表体自定义项2
        /// </summary>
        public string cdefine23
        {
            get { return m_cdefine23; }
            set { m_cdefine23 = value; }
        }

        private string m_cdefine24;
        /// <summary>
        /// 表体自定义项3
        /// </summary>
        public string cdefine24
        {
            get { return m_cdefine24; }
            set { m_cdefine24 = value; }
        }

        private string m_cdefine25;
        /// <summary>
        /// 表体自定义项4
        /// </summary>
        public string cdefine25
        {
            get { return m_cdefine25; }
            set { m_cdefine25 = value; }
        }

        private string m_cdefine26;
        /// <summary>
        /// 表体自定义项5
        /// </summary>
        public string cdefine26
        {
            get { return m_cdefine26; }
            set { m_cdefine26 = value; }
        }

        private string m_cdefine27;
        /// <summary>
        /// 表体自定义项6
        /// </summary>
        public string cdefine27
        {
            get { return m_cdefine27; }
            set { m_cdefine27 = value; }
        }

        private string m_cdefine28;
        /// <summary>
        /// 表体自定义项7
        /// </summary>
        public string cdefine28
        {
            get { return m_cdefine28; }
            set { m_cdefine28 = value; }
        }

        private string m_cdefine29;
        /// <summary>
        /// 表体自定义项8
        /// </summary>
        public string cdefine29
        {
            get { return m_cdefine29; }
            set { m_cdefine29 = value; }
        }

        private string m_cdefine30;
        /// <summary>
        /// 表体自定义项9
        /// </summary>
        public string cdefine30
        {
            get { return m_cdefine30; }
            set { m_cdefine30 = value; }
        }

        private string m_cdefine31;
        /// <summary>
        /// 表体自定义项10
        /// </summary>
        public string cdefine31
        {
            get { return m_cdefine31; }
            set { m_cdefine31 = value; }
        }

        private string m_cdefine32;
        /// <summary>
        /// 表体自定义项11
        /// </summary>
        public string cdefine32
        {
            get { return m_cdefine32; }
            set { m_cdefine32 = value; }
        }

        private string m_cdefine33;
        /// <summary>
        /// 表体自定义项12
        /// </summary>
        public string cdefine33
        {
            get { return m_cdefine33; }
            set { m_cdefine33 = value; }
        }

        private string m_cdefine34;
        /// <summary>
        /// 表体自定义项13
        /// </summary>
        public string cdefine34
        {
            get { return m_cdefine34; }
            set { m_cdefine34 = value; }
        }

        private string m_cdefine35;
        /// <summary>
        /// 表体自定义项14
        /// </summary>
        public string cdefine35
        {
            get { return m_cdefine35; }
            set { m_cdefine35 = value; }
        }

        private string m_cdefine36;
        /// <summary>
        /// 表体自定义项15
        /// </summary>
        public string cdefine36
        {
            get { return m_cdefine36; }
            set { m_cdefine36 = value; }
        }

        private string m_cdefine37;
        /// <summary>
        /// 表体自定义项16
        /// </summary>
        public string cdefine37
        {
            get { return m_cdefine37; }
            set { m_cdefine37 = value; }
        }

        private string m_cfree1;
        /// <summary>
        /// 存货自由项1
        /// </summary>
        public string cfree1
        {
            get { return m_cfree1; }
            set { m_cfree1 = value; }
        }

        private string m_cfree10;
        /// <summary>
        /// 存货自由项10
        /// </summary>
        public string cfree10
        {
            get { return m_cfree10; }
            set { m_cfree10 = value; }
        }

        private string m_cfree2;
        /// <summary>
        /// 存货自由项2
        /// </summary>
        public string cfree2
        {
            get { return m_cfree2; }
            set { m_cfree2 = value; }
        }

        private string m_cfree3;
        /// <summary>
        /// 存货自由项3
        /// </summary>
        public string cfree3
        {
            get { return m_cfree3; }
            set { m_cfree3 = value; }
        }

        private string m_cfree4;
        /// <summary>
        /// 存货自由项4
        /// </summary>
        public string cfree4
        {
            get { return m_cfree4; }
            set { m_cfree4 = value; }
        }

        private string m_cfree5;
        /// <summary>
        /// 存货自由项5
        /// </summary>
        public string cfree5
        {
            get { return m_cfree5; }
            set { m_cfree5 = value; }
        }

        private string m_cfree6;
        /// <summary>
        /// 存货自由项6
        /// </summary>
        public string cfree6
        {
            get { return m_cfree6; }
            set { m_cfree6 = value; }
        }

        private string m_cfree7;
        /// <summary>
        /// 存货自由项7
        /// </summary>
        public string cfree7
        {
            get { return m_cfree7; }
            set { m_cfree7 = value; }
        }

        private string m_cfree8;
        /// <summary>
        /// 存货自由项8
        /// </summary>
        public string cfree8
        {
            get { return m_cfree8; }
            set { m_cfree8 = value; }
        }

        private string m_cfree9;
        /// <summary>
        /// 存货自由项9
        /// </summary>
        public string cfree9
        {
            get { return m_cfree9; }
            set { m_cfree9 = value; }
        }

        private string m_cgspstate;
        /// <summary>
        /// 检验状态
        /// </summary>
        public string cgspstate
        {
            get { return m_cgspstate; }
            set { m_cgspstate = value; }
        }

        private string m_cinva_unit;
        /// <summary>
        /// 库存单位
        /// </summary>
        public string cinva_unit
        {
            get { return m_cinva_unit; }
            set { m_cinva_unit = value; }
        }

        private string m_cinvaddcode;
        /// <summary>
        /// 存货代码
        /// </summary>
        public string cinvaddcode
        {
            get { return m_cinvaddcode; }
            set { m_cinvaddcode = value; }
        }

        private string m_cinvcode;
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cinvcode
        {
            get { return m_cinvcode; }
            set { m_cinvcode = value; }
        }

        private string m_cinvdefine1;
        /// <summary>
        /// 生产单位
        /// </summary>
        public string cinvdefine1
        {
            get { return m_cinvdefine1; }
            set { m_cinvdefine1 = value; }
        }

        private string m_cinvdefine10;
        /// <summary>
        /// 存货自定义项10
        /// </summary>
        public string cinvdefine10
        {
            get { return m_cinvdefine10; }
            set { m_cinvdefine10 = value; }
        }

        private string m_cinvdefine11;
        /// <summary>
        /// 存货自定义项11
        /// </summary>
        public string cinvdefine11
        {
            get { return m_cinvdefine11; }
            set { m_cinvdefine11 = value; }
        }

        private string m_cinvdefine12;
        /// <summary>
        /// 存货自定义项12
        /// </summary>
        public string cinvdefine12
        {
            get { return m_cinvdefine12; }
            set { m_cinvdefine12 = value; }
        }

        private string m_cinvdefine13;
        /// <summary>
        /// 存货自定义项13
        /// </summary>
        public string cinvdefine13
        {
            get { return m_cinvdefine13; }
            set { m_cinvdefine13 = value; }
        }

        private string m_cinvdefine14;
        /// <summary>
        /// 存货自定义项14
        /// </summary>
        public string cinvdefine14
        {
            get { return m_cinvdefine14; }
            set { m_cinvdefine14 = value; }
        }

        private string m_cinvdefine15;
        /// <summary>
        /// 存货自定义项15
        /// </summary>
        public string cinvdefine15
        {
            get { return m_cinvdefine15; }
            set { m_cinvdefine15 = value; }
        }

        private string m_cinvdefine16;
        /// <summary>
        /// 存货自定义项16
        /// </summary>
        public string cinvdefine16
        {
            get { return m_cinvdefine16; }
            set { m_cinvdefine16 = value; }
        }

        private string m_cinvdefine2;
        /// <summary>
        /// 存货自定义项2（产地）
        /// </summary>
        public string cinvdefine2
        {
            get { return m_cinvdefine2; }
            set { m_cinvdefine2 = value; }
        }

        private string m_cinvdefine3;
        /// <summary>
        /// 存货自定义项3
        /// </summary>
        public string cinvdefine3
        {
            get { return m_cinvdefine3; }
            set { m_cinvdefine3 = value; }
        }

        //存货自定义项4
        private string m_cinvdefine4;
        public string cinvdefine4
        {
            get { return m_cinvdefine4; }
            set { m_cinvdefine4 = value; }
        }

        private string m_cinvdefine5;
        /// <summary>
        /// 存货自定义项5
        /// </summary>
        public string cinvdefine5
        {
            get { return m_cinvdefine5; }
            set { m_cinvdefine5 = value; }
        }

        private string m_cinvdefine6;
        /// <summary>
        /// 存货自定义项6
        /// </summary>
        public string cinvdefine6
        {
            get { return m_cinvdefine6; }
            set { m_cinvdefine6 = value; }
        }

        private string m_cinvdefine7;
        /// <summary>
        /// 存货自定义项7
        /// </summary>
        public string cinvdefine7
        {
            get { return m_cinvdefine7; }
            set { m_cinvdefine7 = value; }
        }

        private string m_cinvdefine8;
        /// <summary>
        /// 存货自定义项8
        /// </summary>
        public string cinvdefine8
        {
            get { return m_cinvdefine8; }
            set { m_cinvdefine8 = value; }
        }

        private string m_cinvdefine9;
        /// <summary>
        /// 存货自定义项9
        /// </summary>
        public string cinvdefine9
        {
            get { return m_cinvdefine9; }
            set { m_cinvdefine9 = value; }
        }

        private string m_cinvm_unit;
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string cinvm_unit
        {
            get { return m_cinvm_unit; }
            set { m_cinvm_unit = value; }
        }

        private string m_cinvname;
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cinvname
        {
            get { return m_cinvname; }
            set { m_cinvname = value; }
        }

        private string m_cinvouchcode;
        /// <summary>
        /// 对应入库单号
        /// </summary>
        public string cinvouchcode
        {
            get { return m_cinvouchcode; }
            set { m_cinvouchcode = value; }
        }

        private string m_cinvstd;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string cinvstd
        {
            get { return m_cinvstd; }
            set { m_cinvstd = value; }
        }

        private string m_citem_class;
        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string citem_class
        {
            get { return m_citem_class; }
            set { m_citem_class = value; }
        }

        private string m_citemcname;
        /// <summary>
        /// 项目大类名称
        /// </summary>
        public string citemcname
        {
            get { return m_citemcname; }
            set { m_citemcname = value; }
        }

        private string m_citemcode;
        /// <summary>
        /// 项目编码
        /// </summary>
        public string citemcode
        {
            get { return m_citemcode; }
            set { m_citemcode = value; }
        }

        private int m_cmassunit;
        /// <summary>
        /// 保质期单位
        /// </summary>
        public int cmassunit
        {
            get { return m_cmassunit; }
            set { m_cmassunit = value; }
        }

        private string m_cname;
        /// <summary>
        /// 项目
        /// </summary>
        public string cname
        {
            get { return m_cname; }
            set { m_cname = value; }
        }

        private string m_corufts;
        /// <summary>
        /// 对应单据时间戳
        /// </summary>
        public string corufts
        {
            get { return m_corufts; }
            set { m_corufts = value; }
        }

        private string m_cposition;
        /// <summary>
        /// 货位编码
        /// </summary>
        public string cposition
        {
            get { return m_cposition; }
            set { m_cposition = value; }
        }

        private string m_cposname;
        /// <summary>
        /// 货位
        /// </summary>
        public string cposname
        {
            get { return m_cposname; }
            set { m_cposname = value; }
        }

        private string m_creplaceitem;
        /// <summary>
        /// 替换件
        /// </summary>
        public string creplaceitem
        {
            get { return m_creplaceitem; }
            set { m_creplaceitem = value; }
        }

        private string m_csocode;
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string csocode
        {
            get { return m_csocode; }
            set { m_csocode = value; }
        }

        private string m_cvenname;
        /// <summary>
        /// 供应商
        /// </summary>
        public string cvenname
        {
            get { return m_cvenname; }
            set { m_cvenname = value; }
        }

        private string m_cvmivencode;
        /// <summary>
        /// 代管商代码
        /// </summary>
        public string cvmivencode
        {
            get { return m_cvmivencode; }
            set { m_cvmivencode = value; }
        }

        private string m_cvmivenname;
        /// <summary>
        /// 代管商
        /// </summary>
        public string cvmivenname
        {
            get { return m_cvmivenname; }
            set { m_cvmivenname = value; }
        }

        private string m_cvouchcode;
        /// <summary>
        /// 对应入库单id
        /// </summary>
        public string cvouchcode
        {
            get { return m_cvouchcode; }
            set { m_cvouchcode = value; }
        }

        private string m_dmadedate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public string dmadedate
        {
            get { return m_dmadedate; }
            set { m_dmadedate = value; }
        }

        private string m_dvdate;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string dvdate
        {
            get { return m_dvdate; }
            set { m_dvdate = value; }
        }

        private int m_iavanum;
        /// <summary>
        /// 可用件数
        /// </summary>
        public int iavanum
        {
            get { return m_iavanum; }
            set { m_iavanum = value; }
        }

        private int m_iavaquantity;
        /// <summary>
        /// 可用量
        /// </summary>
        public int iavaquantity
        {
            get { return m_iavaquantity; }
            set { m_iavaquantity = value; }
        }

        private int m_icheckids;
        /// <summary>
        /// 检验单子表id
        /// </summary>
        public int icheckids
        {
            get { return m_icheckids; }
            set { m_icheckids = value; }
        }

        private int m_iCheckIdBaks;
        /// <summary>
        /// 检验单子表标识
        /// </summary>
        public int iCheckIdBaks
        {
            get { return m_iCheckIdBaks; }
            set { m_iCheckIdBaks = value; }
        }

        private string m_cCheckCode;
        /// <summary>
        /// 检验单号
        /// </summary>
        public string cCheckCode
        {
            get { return m_cCheckCode; }
            set { m_cCheckCode = value; }
        }

        private string m_dCheckDate;
        /// <summary>
        /// 检验日期
        /// </summary>
        public string dCheckDate
        {
            get { return m_dCheckDate; }
            set { m_dCheckDate = value; }
        }

        private string m_cCheckPersonCode;
        /// <summary>
        /// 检验员
        /// </summary>
        public string cCheckPersonCode
        {
            get { return m_cCheckPersonCode; }
            set { m_cCheckPersonCode = value; }
        }

        private int m_id;
        /// <summary>
        /// ID
        /// </summary>
        public int id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private int m_idlsid;
        /// <summary>
        /// 发货单子表ID
        /// </summary>
        public int idlsid
        {
            get { return m_idlsid; }
            set { m_idlsid = value; }
        }

        private int m_iensid;
        /// <summary>
        /// 委托子表id
        /// </summary>
        public int iensid
        {
            get { return m_iensid; }
            set { m_iensid = value; }
        }

        private int m_igrossweight;
        /// <summary>
        /// 毛重
        /// </summary>
        public int igrossweight
        {
            get { return m_igrossweight; }
            set { m_igrossweight = value; }
        }

        private Decimal m_iinvexchrate;
        /// <summary>
        /// 换算率
        /// </summary>
        public Decimal iinvexchrate
        {
            get { return m_iinvexchrate; }
            set { m_iinvexchrate = value; }
        }

        private int m_iinvsncount;
        /// <summary>
        /// 序列号个数
        /// </summary>
        public int iinvsncount
        {
            get { return m_iinvsncount; }
            set { m_iinvsncount = value; }
        }

        private int m_imassdate;
        /// <summary>
        /// 保质期
        /// </summary>
        public int imassdate
        {
            get { return m_imassdate; }
            set { m_imassdate = value; }
        }

        private int m_impoids;
        /// <summary>
        /// 生产订单子表ID
        /// </summary>
        public int impoids
        {
            get { return m_impoids; }
            set { m_impoids = value; }
        }

        private Decimal m_inetweight;
        /// <summary>
        /// 净重
        /// </summary>
        public Decimal inetweight
        {
            get { return m_inetweight; }
            set { m_inetweight = value; }
        }

        private Decimal m_innum;
        /// <summary>
        /// 应入件数
        /// </summary>
        public Decimal innum
        {
            get { return m_innum; }
            set { m_innum = value; }
        }

        private Decimal m_inum;
        /// <summary>
        /// 件数
        /// </summary>
        public Decimal inum
        {
            get { return m_inum; }
            set { m_inum = value; }
        }

        private Decimal m_ipprice;
        /// <summary>
        /// 计划金额/售价金额
        /// </summary>
        public Decimal ipprice
        {
            get { return m_ipprice; }
            set { m_ipprice = value; }
        }

        private Decimal m_ipresent;
        /// <summary>
        /// 现存量
        /// </summary>
        public Decimal ipresent
        {
            get { return m_ipresent; }
            set { m_ipresent = value; }
        }

        private Decimal m_ipresentnum;
        /// <summary>
        /// 现存件数
        /// </summary>
        public Decimal ipresentnum
        {
            get { return m_ipresentnum; }
            set { m_ipresentnum = value; }
        }

        private Decimal m_iprice;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal iprice
        {
            get { return m_iprice; }
            set { m_iprice = value; }
        }

        private Decimal m_iaprice;
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal iaprice
        {
            get { return m_iaprice; }
            set { m_iaprice = value; }
        }

        private Decimal m_ipunitcost;
        /// <summary>
        /// 计划单价/售价
        /// </summary>
        public Decimal ipunitcost
        {
            get { return m_ipunitcost; }
            set { m_ipunitcost = value; }
        }

        private Decimal m_iquantity;
        /// <summary>
        /// 数量
        /// </summary>
        public Decimal iquantity
        {
            get { return m_iquantity; }
            set { m_iquantity = value; }
        }

        private Decimal m_inquantity;
        /// <summary>
        /// 应收数量
        /// </summary>
        public Decimal inquantity
        {
            get { return m_inquantity; }
            set { m_inquantity = value; }
        }

        private Decimal m_isbsid;
        /// <summary>
        /// 发票子表ID
        /// </summary>
        public Decimal isbsid
        {
            get { return m_isbsid; }
            set { m_isbsid = value; }
        }

        private Decimal m_isendnum;
        /// <summary>
        /// 发货件数
        /// </summary>
        public Decimal isendnum
        {
            get { return m_isendnum; }
            set { m_isendnum = value; }
        }

        private Decimal m_isendquantity;
        /// <summary>
        /// 发货数量
        /// </summary>
        public Decimal isendquantity
        {
            get { return m_isendquantity; }
            set { m_isendquantity = value; }
        }

        private string m_isodid;
        /// <summary>
        /// 销售订单子表ID
        /// </summary>
        public string isodid
        {
            get { return m_isodid; }
            set { m_isodid = value; }
        }

        private Decimal m_isoseq;
        /// <summary>
        /// 销售订单行号
        /// </summary>
        public Decimal isoseq
        {
            get { return m_isoseq; }
            set { m_isoseq = value; }
        }

        private Decimal m_isotype;
        /// <summary>
        /// 销售订单类别
        /// </summary>
        public Decimal isotype
        {
            get { return m_isotype; }
            set { m_isotype = value; }
        }

        private Decimal m_isoutnum;
        /// <summary>
        /// 累计出库件数
        /// </summary>
        public Decimal isoutnum
        {
            get { return m_isoutnum; }
            set { m_isoutnum = value; }
        }

        private Decimal m_isoutquantity;
        /// <summary>
        /// 累计出库数量
        /// </summary>
        public Decimal isoutquantity
        {
            get { return m_isoutquantity; }
            set { m_isoutquantity = value; }
        }

        private Decimal m_itrids;
        /// <summary>
        /// 特殊单据子表标识
        /// </summary>
        public Decimal itrids
        {
            get { return m_itrids; }
            set { m_itrids = value; }
        }

        private Decimal m_iunitcost;
        /// <summary>
        /// 单价
        /// </summary>
        public Decimal iunitcost
        {
            get { return m_iunitcost; }
            set { m_iunitcost = value; }
        }

        private Decimal m_ivmisettlenum;
        /// <summary>
        /// 代管挂账确认单件数
        /// </summary>
        public Decimal ivmisettlenum
        {
            get { return m_ivmisettlenum; }
            set { m_ivmisettlenum = value; }
        }

        private Decimal m_ivmisettlequantity;
        /// <summary>
        /// 代管挂账确认单数量
        /// </summary>
        public Decimal ivmisettlequantity
        {
            get { return m_ivmisettlequantity; }
            set { m_ivmisettlequantity = value; }
        }

        private string m_scrapufts;
        /// <summary>
        /// 不合格品时间戳
        /// </summary>
        public string scrapufts
        {
            get { return m_scrapufts; }
            set { m_scrapufts = value; }
        }

        private string m_stockupid;
        /// <summary>
        /// stockupid
        /// </summary>
        public string stockupid
        {
            get { return m_stockupid; }
            set { m_stockupid = value; }
        }

        private string m_strcode;
        /// <summary>
        /// 合同标的编码
        /// </summary>
        public string strcode
        {
            get { return m_strcode; }
            set { m_strcode = value; }
        }

        private string m_strcontractid;
        /// <summary>
        /// 合同号
        /// </summary>
        public string strcontractid
        {
            get { return m_strcontractid; }
            set { m_strcontractid = value; }
        }

        private string m_cwhcode;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cwhcode
        {
            get { return m_cwhcode; }
            set { m_cwhcode = value; }
        }

        private string m_cwhname;
        /// <summary>
        /// 仓库名
        /// </summary>
        public string cwhname
        {
            get { return m_cwhname; }
            set { m_cwhname = value; }
        }

        private decimal m_MoQuantity;
        /// <summary>
        /// 生产订单数量
        /// </summary>
        public decimal MoQuantity
        {
            get { return m_MoQuantity; }
            set { m_MoQuantity = value; }
        }
        
        private decimal m_MoNum;
        /// <summary>
        /// 生产订单件数
        /// </summary>
        public decimal MoNum
        {
            get { return m_MoNum; }
            set { m_MoNum = value; }
        }
        
        private decimal m_QualifiedInQty;
        /// <summary>
        /// 已入库数量
        /// </summary>
        public decimal QualifiedInQty
        {
            get { return m_QualifiedInQty; }
            set { m_QualifiedInQty = value; }
        }
        
        private decimal m_Qty;
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }

        private int m_iorderdid;
        public int iorderdid
        {
            get { return m_iorderdid; }
            set { m_iorderdid = value; }
        }

        private string m_iordercode;
        public string iordercode
        {
            get { return m_iordercode; }
            set { m_iordercode = value; }
        }

        private string m_cbdlcode;
        public string cbdlcode
        {
            get { return m_cbdlcode; }
            set { m_cbdlcode = value; }
        }
        private int m_iorderseq;
        public int iorderseq
        {
            get { return m_iorderseq; }
            set { m_iorderseq = value; }
        }
        
        private int m_FregBreakQuantity;
        /// <summary>
        /// 合格样本破坏数量
        /// </summary>
        public int FREGBREAKQUANTITY
        {
            get { return m_FregBreakQuantity; }
            set { m_FregBreakQuantity = value; }
        }
        
        private string m_cProBatch;
        /// <summary>
        /// 生产批号
        /// </summary>
        public string cProBatch
        {
            get { return m_cProBatch; }
            set { m_cProBatch = value; }
        }
        
        private string m_MoCode;
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string MoCode
        {
            get { return m_MoCode; }
            set { m_MoCode = value; }
        }
        
        private int m_iMoSeq;
        /// <summary>
        /// 生产订单行号
        /// </summary>
        public int iMoSeq
        {
            get { return m_iMoSeq; }
            set { m_iMoSeq = value; }
        }
        
        private string m_dExpirationdate;
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public string dExpirationdate
        {
            get { return m_dExpirationdate; }
            set { m_dExpirationdate = value; }
        }
        
        private string m_cExpirationdate;
        /// <summary>
        /// 有效期至
        /// </summary>
        public string cExpirationdate
        {
            get { return m_cExpirationdate; }
            set { m_cExpirationdate = value; }
        }
        
        private int m_iExpiratDateCalcu;
        /// <summary>
        /// 有效期推算方式
        /// </summary>
        public int iExpiratDateCalcu
        {
            get { return m_iExpiratDateCalcu; }
            set { m_iExpiratDateCalcu = value; }
        }
                
        private string m_barcode;
        /// <summary>
        /// 条码
        /// </summary>
        public string barcode
        {
            get { return m_barcode; }
            set { m_barcode = value; }
        }
        
        private decimal m_FREGQUANTITY;
        /// <summary>
        /// 合格接收数量
        /// </summary>
        public decimal FREGQUANTITY
        {
            get { return m_FREGQUANTITY; }
            set { m_FREGQUANTITY = value; }
        }

        #endregion

        public STInProductDetail()
        {
        }

        public STInProductDetail(System.Data.DataRow dr)
        {
            this.cinvcode = DB2String(dr["cInvCode"]);
            this.cinvname = DB2String(dr["cInvName"]);
            this.cinvstd = DB2String(dr["cInvStd"]);
            this.cassunit = DB2String(dr["cSTComUnitCode"]);
            this.iinvexchrate = DB2Decimal(dr["iinvexchrate"]);

            //2012-10-23
            this.cinvdefine1 = DB2String(dr["cinvdefine1"]);
            this.cinvdefine2 = DB2String(dr["cinvdefine2"]);
            this.cmassunit = DB2Int(dr["cMassUnit"]);
            this.imassdate = DB2Int(dr["iMassDate"]);
        }

        #region 转换
        public static string DB2String(object DBValue)
        {
            return DBValue != System.DBNull.Value ? DBValue.ToString() : "";
        }

        public static int DB2Int(object DBValue)
        {
            int iReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) iReturn = Convert.ToInt32(DBValue);
            }
            catch
            {
                iReturn = -10;
            }
            return iReturn;
        }

        public static Decimal DB2Decimal(object DBValue)
        {
            Decimal dReturn = 0;
            try
            {
                if (DBValue != System.DBNull.Value) dReturn = Convert.ToDecimal(DBValue);
            }
            catch
            {
                dReturn = -10;
            }
            return dReturn;
        }
        public static Boolean DB2Bool(object DBValue)
        {
            bool blnReturn = false;
            try
            {
                if (DBValue != System.DBNull.Value) blnReturn = Convert.ToBoolean(DBValue);
            }
            catch
            {
                blnReturn = false;
            }
            return blnReturn;
        }

        public static DateTime DB2DateTime(object DBValue)
        {
            DateTime btReturn = DateTime.MinValue;
            try
            {
                if (DBValue != System.DBNull.Value)
                {
                    btReturn = Convert.ToDateTime(DBValue);
                }
            }
            catch
            {
                btReturn = DateTime.MaxValue;
            }
            return btReturn;
        }

        public static string GetNull(string str)
        {
            if (str == "null" || str == "")
                return "Null";
            else
                return "N'" + str + "'";
        }
        #endregion
    }
}
