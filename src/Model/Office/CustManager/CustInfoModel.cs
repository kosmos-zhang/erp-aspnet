/**********************************************
 * 类作用：   CustInfo表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/03/10
 ***********************************************/

using System;

namespace XBase.Model.Office.CustManager
{
    public class CustInfoModel
    {
        #region 字段
        private int _id;
        private string _companycd;
        private string _custtypemanage;
        private string _custtypesell;
        private string _custtypetime;
        private DateTime? _firstbuydate;
        private int _custtype;
        private int _custclass;
        private int _relationtype;
        private string _custno;
        private string _custname;
        private string _custnam;
        private string _custshort;
        private int _creditgrade;
        private string _custnote;
        private int _seller;
        private int _areaid;
        private int _countryid;
        private string _province;
        private string _city;
        private string _tel;
        private string _fax;
        private string _online;
        private string _website;
        private string _post;
        private string _addr;
        private int _linkcycle;
        private string _hotis;
        private string _hothow;
        private string _hottype;
        private string _meritgrade;
        private string _relagrade;
        private string _relation;
        private string _companytype;
        private int _staffcount;
        private string _source;
        private string _phase;
        private string _custsupe;
        private string _trade;
        private DateTime? _setupdate;
        private string _artiperson;
        private decimal _setupmoney;
        private string _setupaddress;
        private decimal _capitalscale;
        private decimal _saleroomy;
        private decimal _profity;
        private string _taxcd;
        private string _businumber;
        private string _istax;
        private string _sellarea;
        private string _sellmode;
        private string _receiveaddress;
        private string _contactname;
        private string _mobile;
        private string _email;
        private int _taketype;
        private int _carrytype;
        private string _busitype;
        private string _billtype;
        private int _paytype;
        private int _moneytype;
        private int _currencytype;
        private string _creditmanage;
        private decimal _maxcredit;
        private decimal _maxcreditdate;

        private string _openbank;
        private string _accountman;
        private string _accountnum;

        private string _remark;
        private string _usedstatus;
        private int _creator;
        private DateTime? _createddate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _canviewuser;
        private string _canviewusername;

        private string _companyvalues;
        private string _catchword;
        private string _managevalues;
        private string _potential;
        private string _problem;
        private string _advantages;
        private string _tradeposition;
        private string _competition;
        private string _collaborator;
        private string _manageplan;
        private string _collaborate;

        private string _custbig;
        private string _custnum;

        #endregion

        #region
        /// <summary>
        /// 客户大类
        /// </summary>
        public string CustBig
        {
            set { _custbig = value; }
            get { return _custbig; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CustNum
        {
            set { _custnum = value; }
            get { return _custnum; }
        }

        /// <summary>
        /// 可查看该客户档案的人员ID
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该客户档案的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }

        /// <summary>
        /// 客户ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 客户管理分类
        /// </summary>
        public string CustTypeManage
        {
            set { _custtypemanage = value; }
            get { return _custtypemanage; }
        }
        /// <summary>
        /// 客户营销分类
        /// </summary>
        public string CustTypeSell
        {
            set { _custtypesell = value; }
            get { return _custtypesell; }
        }
        /// <summary>
        /// 客户时间分类
        /// </summary>
        public string CustTypeTime
        {
            set { _custtypetime = value; }
            get { return _custtypetime; }
        }
        /// <summary>
        /// 首次交易日期
        /// </summary>
        public DateTime? FirstBuyDate
        {
            set { _firstbuydate = value; }
            get { return _firstbuydate; }
        }
        /// <summary>
        /// 客户类别ID
        /// </summary>
        public int CustType
        {
            set { _custtype = value; }
            get { return _custtype; }
        }
        /// <summary>
        /// 客户分类ID
        /// </summary>
        public int CustClass
        {
            set { _custclass = value; }
            get { return _custclass; }
        }
        /// <summary>
        /// 客户类型ID(分类代码表定义)
        /// </summary>
        public int RelationType
        {
            set { _relationtype = value; }
            get { return _relationtype; }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 客户简称
        /// </summary>
        public string CustNam
        {
            set { _custnam = value; }
            get { return _custnam; }
        }
        /// <summary>
        /// 客户拼音代码
        /// </summary>
        public string CustShort
        {
            set { _custshort = value; }
            get { return _custshort; }
        }
        /// <summary>
        /// 客户优质级别ID
        /// </summary>
        public int CreditGrade
        {
            set { _creditgrade = value; }
            get { return _creditgrade; }
        }
        /// <summary>
        /// 客户简介
        /// </summary>
        public string CustNote
        {
            set { _custnote = value; }
            get { return _custnote; }
        }
        /// <summary>
        /// 销售人员ID
        /// </summary>
        public int Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 地区
        /// </summary>
        public int AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public int CountryID
        {
            set { _countryid = value; }
            get { return _countryid; }
        }
        /// <summary>
        /// 省
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 市（县）
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 在线咨询
        /// </summary>
        public string OnLine
        {
            set { _online = value; }
            get { return _online; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 联络期限ID
        /// </summary>
        public int LinkCycle
        {
            set { _linkcycle = value; }
            get { return _linkcycle; }
        }
        /// <summary>
        /// 热点客户（1是2否）
        /// </summary>
        public string HotIs
        {
            set { _hotis = value; }
            get { return _hotis; }
        }
        /// <summary>
        /// 热度（1低热2中热3高热）
        /// </summary>
        public string HotHow
        {
            set { _hothow = value; }
            get { return _hothow; }
        }
        /// <summary>
        /// 热点分类（1新客户2老客户3新合作）
        /// </summary>
        public string HotType
        {
            set { _hottype = value; }
            get { return _hottype; }
        }
        /// <summary>
        /// 价值评估（1高2中3低）
        /// </summary>
        public string MeritGrade
        {
            set { _meritgrade = value; }
            get { return _meritgrade; }
        }
        /// <summary>
        /// 关系等级（1密切2较好3一般4较差）
        /// </summary>
        public string RelaGrade
        {
            set { _relagrade = value; }
            get { return _relagrade; }
        }
        /// <summary>
        /// 关系描述
        /// </summary>
        public string Relation
        {
            set { _relation = value; }
            get { return _relation; }
        }
        /// <summary>
        /// 单位性质（1国有2私营3个体4外商独资5中外合资6国内合资7股份制）
        /// </summary>
        public string CompanyType
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// 人员规模
        /// </summary>
        public int StaffCount
        {
            set { _staffcount = value; }
            get { return _staffcount; }
        }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 阶段
        /// </summary>
        public string Phase
        {
            set { _phase = value; }
            get { return _phase; }
        }
        /// <summary>
        /// 上级客户
        /// </summary>
        public string CustSupe
        {
            set { _custsupe = value; }
            get { return _custsupe; }
        }
        /// <summary>
        /// 行业
        /// </summary>
        public string Trade
        {
            set { _trade = value; }
            get { return _trade; }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? SetupDate
        {
            set { _setupdate = value; }
            get { return _setupdate; }
        }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string ArtiPerson
        {
            set { _artiperson = value; }
            get { return _artiperson; }
        }
        /// <summary>
        /// 注册资本(万元)
        /// </summary>
        public decimal SetupMoney
        {
            set { _setupmoney = value; }
            get { return _setupmoney; }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string SetupAddress
        {
            set { _setupaddress = value; }
            get { return _setupaddress; }
        }
        /// <summary>
        /// 资产规模(万元)
        /// </summary>
        public decimal CapitalScale
        {
            set { _capitalscale = value; }
            get { return _capitalscale; }
        }
        /// <summary>
        /// 年销售额(万元)
        /// </summary>
        public decimal SaleroomY
        {
            set { _saleroomy = value; }
            get { return _saleroomy; }
        }
        /// <summary>
        /// 年利润额(万元)
        /// </summary>
        public decimal ProfitY
        {
            set { _profity = value; }
            get { return _profity; }
        }
        /// <summary>
        /// 公司税号
        /// </summary>
        public string TaxCD
        {
            set { _taxcd = value; }
            get { return _taxcd; }
        }
        /// <summary>
        /// 营业执照号
        /// </summary>
        public string BusiNumber
        {
            set { _businumber = value; }
            get { return _businumber; }
        }
        /// <summary>
        /// 是否为一般纳税人
        /// </summary>
        public string IsTax
        {
            set { _istax = value; }
            get { return _istax; }
        }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string SellArea
        {
            set { _sellarea = value; }
            get { return _sellarea; }
        }
        /// <summary>
        /// 销售模式（1内销，2外销）
        /// </summary>
        public string SellMode
        {
            set { _sellmode = value; }
            get { return _sellmode; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveAddress
        {
            set { _receiveaddress = value; }
            get { return _receiveaddress; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 邮件
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 交货方式ID
        /// </summary>
        public int TakeType
        {
            set { _taketype = value; }
            get { return _taketype; }
        }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public int CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }
        /// <summary>
        /// 业务类型（1普通销售,2委托代销,3直运）
        /// </summary>
        public string BusiType
        {
            set { _busitype = value; }
            get { return _busitype; }
        }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string BillType
        {
            set { _billtype = value; }
            get { return _billtype; }
        }
        /// <summary>
        /// 结算方式ID
        /// </summary>
        public int PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public int MoneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 结算币种ID
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 信用管理（0否，1是）
        /// </summary>
        public string CreditManage
        {
            set { _creditmanage = value; }
            get { return _creditmanage; }
        }
        /// <summary>
        /// 帐期天数(天)
        /// </summary>
        public decimal MaxCredit
        {
            set { _maxcredit = value; }
            get { return _maxcredit; }
        }        
        /// <summary>
        /// 帐期天数(天)
        /// </summary>
        public decimal MaxCreditDate
        {
            set { _maxcreditdate = value; }
            get { return _maxcreditdate; }
        }
        /// <summary>
        /// 开户行
        /// </summary>
        public string OpenBank
        {
            set { _openbank = value; }
            get { return _openbank; }
        }
        /// <summary>
        /// 户名
        /// </summary>
        public string AccountMan
        {
            set { _accountman = value; }
            get { return _accountman; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNum
        {
            set { _accountnum = value; }
            get { return _accountnum; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 启用状态（0未启用，1已启用）
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyValues
        {
            set { _companyvalues = value; }
            get { return _companyvalues; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CatchWord
        {
            set { _catchword = value; }
            get { return _catchword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManageValues
        {
            set { _managevalues = value; }
            get { return _managevalues; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Potential
        {
            set { _potential = value; }
            get { return _potential; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Problem
        {
            set { _problem = value; }
            get { return _problem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Advantages
        {
            set { _advantages = value; }
            get { return _advantages; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TradePosition
        {
            set { _tradeposition = value; }
            get { return _tradeposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Competition
        {
            set { _competition = value; }
            get { return _competition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Collaborator
        {
            set { _collaborator = value; }
            get { return _collaborator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManagePlan
        {
            set { _manageplan = value; }
            get { return _manageplan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Collaborate
        {
            set { _collaborate = value; }
            get { return _collaborate; }
        }
        #endregion Model
    }
}
