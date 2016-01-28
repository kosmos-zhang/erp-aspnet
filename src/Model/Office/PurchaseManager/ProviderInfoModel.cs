/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/24                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：ProviderInfoModel
    /// 描述：ProviderInfo表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/21
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    public class ProviderInfoModel
    {
		#region Model
		private int _id;
		private string _companycd;
		private string _bigtype;
		private int _custtype;
		private int _custclass;
		private string _custno;
		private string _custname;
		private string _custnam;
		private string _pyshort;
		private int _creditgrade;
		private string _custnote;
		private int _manager;
		private int _areaid;
		private int _linkcycle;
		private string _hotis;
		private string _hothow;
		private string _meritgrade;
		private string _companytype;
		private int _staffcount;
		private DateTime ? _setupdate;
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
		private int _countryid;
		private string _province;
		private string _city;
		private string _sendaddress;
		private string _post;
		private string _contactname;
		private string _tel;
		private string _fax;
		private string _mobile;
		private string _email;
		private string _online;
		private string _website;
		private int _taketype;
		private int _carrytype;
		private int _paytype;
		private int _currencytype;
		private string _remark;
		private string _usedstatus;
		private int _creator;
		private DateTime ? _createdate;
		private DateTime ? _modifieddate;
		private string _modifieduserid;
        private string _openbank;
        private string _accountman;
        private string _accountnum;
        private int _allowdefaultdays;
		/// <summary>
		/// 供应商ID，自动生成
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 公司代码
		/// </summary>
		public string CompanyCD
		{
		set{ _companycd=value;}
		get{return _companycd;}
		}
		/// <summary>
		/// 往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加工厂，6运输商，7其他），此处固定为2供应商
		/// </summary>
		public string BigType
		{
		set{ _bigtype=value;}
		get{return _bigtype;}
		}
		/// <summary>
		/// 供应商类别（分类代码表定义）
		/// </summary>
		public int CustType
		{
		set{ _custtype=value;}
		get{return _custtype;}
		}
		/// <summary>
		/// 供应商分类（分类代码表定义）
		/// </summary>
		public int CustClass
		{
		set{ _custclass=value;}
		get{return _custclass;}
		}
		/// <summary>
		/// 供应商编号
		/// </summary>
		public string CustNo
		{
		set{ _custno=value;}
		get{return _custno;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string CustName
		{
		set{ _custname=value;}
		get{return _custname;}
		}
		/// <summary>
		/// 供应商简称
		/// </summary>
		public string CustNam
		{
		set{ _custnam=value;}
		get{return _custnam;}
		}
		/// <summary>
		/// 供应商拼音代码
		/// </summary>
		public string PYShort
		{
		set{ _pyshort=value;}
		get{return _pyshort;}
		}
		/// <summary>
		/// 供应商优质级别ID（分类代码表定义）
		/// </summary>
		public int CreditGrade
		{
		set{ _creditgrade=value;}
		get{return _creditgrade;}
		}
		/// <summary>
		/// 供应商简介
		/// </summary>
		public string CustNote
		{
		set{ _custnote=value;}
		get{return _custnote;}
		}
		/// <summary>
		/// 分管采购员ID(来源员工表)
		/// </summary>
		public int Manager
		{
		set{ _manager=value;}
		get{return _manager;}
		}
		/// <summary>
		/// 区域ID（分类代码表定义，区域代码）
		/// </summary>
		public int AreaID
		{
		set{ _areaid=value;}
		get{return _areaid;}
		}
		/// <summary>
        /// 联络期限(天)（对应分类代码表定义，供应商联络期限）
		/// </summary>
		public int LinkCycle
		{
		set{ _linkcycle=value;}
		get{return _linkcycle;}
		}
		/// <summary>
		/// 热点供应商（1是2否）
		/// </summary>
		public string HotIs
		{
		set{ _hotis=value;}
		get{return _hotis;}
		}
		/// <summary>
		/// 热度（1低热2中热3高热）
		/// </summary>
		public string HotHow
		{
		set{ _hothow=value;}
		get{return _hothow;}
		}
		/// <summary>
		/// 价值评估（1高2中3低）
		/// </summary>
		public string MeritGrade
		{
		set{ _meritgrade=value;}
		get{return _meritgrade;}
		}
		/// <summary>
        /// 单位性质（1事业，2企业，3社团，4自然人，5其他）
		/// </summary>
		public string CompanyType
		{
		set{ _companytype=value;}
		get{return _companytype;}
		}
		/// <summary>
        /// 员工总数（个）
		/// </summary>
		public int StaffCount
		{
		set{ _staffcount=value;}
		get{return _staffcount;}
		}
		/// <summary>
		/// 成立时间
		/// </summary>
		public DateTime ? SetupDate
		{
		set{ _setupdate=value;}
		get{return _setupdate;}
		}
		/// <summary>
		/// 法人代表
		/// </summary>
		public string ArtiPerson
		{
		set{ _artiperson=value;}
		get{return _artiperson;}
		}
		/// <summary>
		/// 注册资本(万元)
		/// </summary>
		public decimal SetupMoney
		{
		set{ _setupmoney=value;}
		get{return _setupmoney;}
		}
		/// <summary>
		/// 注册地址
		/// </summary>
		public string SetupAddress
		{
		set{ _setupaddress=value;}
		get{return _setupaddress;}
		}
		/// <summary>
		/// 资产规模(万元)
		/// </summary>
		public decimal CapitalScale
		{
		set{ _capitalscale=value;}
		get{return _capitalscale;}
		}
		/// <summary>
		/// 年销售额(万元)
		/// </summary>
		public decimal SaleroomY
		{
		set{ _saleroomy=value;}
		get{return _saleroomy;}
		}
		/// <summary>
		/// 年利润额(万元)
		/// </summary>
		public decimal ProfitY
		{
		set{ _profity=value;}
		get{return _profity;}
		}
		/// <summary>
		/// 税务登记号
		/// </summary>
		public string TaxCD
		{
		set{ _taxcd=value;}
		get{return _taxcd;}
		}
		/// <summary>
		/// 营业执照号
		/// </summary>
		public string BusiNumber
		{
		set{ _businumber=value;}
		get{return _businumber;}
		}
		/// <summary>
        /// 一般纳税人（0否，1是）
		/// </summary>
		public string isTax
		{
		set{ _istax=value;}
		get{return _istax;}
		}
		/// <summary>
		/// 经营范围
		/// </summary>
		public string SellArea
		{
		set{ _sellarea=value;}
		get{return _sellarea;}
		}
		/// <summary>
		/// 国家地区ID(对应国家代码表ID)
		/// </summary>
		public int CountryID
		{
		set{ _countryid=value;}
		get{return _countryid;}
		}
		/// <summary>
		/// 省
		/// </summary>
		public string Province
		{
		set{ _province=value;}
		get{return _province;}
		}
		/// <summary>
		/// 市（县）
		/// </summary>
		public string City
		{
		set{ _city=value;}
		get{return _city;}
		}
		/// <summary>
		/// 发货地址
		/// </summary>
		public string SendAddress
		{
		set{ _sendaddress=value;}
		get{return _sendaddress;}
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string Post
		{
		set{ _post=value;}
		get{return _post;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string ContactName
		{
		set{ _contactname=value;}
		get{return _contactname;}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string Tel
		{
		set{ _tel=value;}
		get{return _tel;}
		}
		/// <summary>
		/// 传真
		/// </summary>
		public string Fax
		{
		set{ _fax=value;}
		get{return _fax;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile
		{
		set{ _mobile=value;}
		get{return _mobile;}
		}
		/// <summary>
		/// 邮件
		/// </summary>
		public string email
		{
		set{ _email=value;}
		get{return _email;}
		}
		/// <summary>
		/// 在线咨询
		/// </summary>
		public string OnLine
		{
		set{ _online=value;}
		get{return _online;}
		}
		/// <summary>
		/// 公司网址
		/// </summary>
		public string WebSite
		{
		set{ _website=value;}
		get{return _website;}
		}
		/// <summary>
		/// 交货方式ID
		/// </summary>
		public int TakeType
		{
		set{ _taketype=value;}
		get{return _taketype;}
		}
		/// <summary>
		/// 运送方式ID
		/// </summary>
		public int CarryType
		{
		set{ _carrytype=value;}
		get{return _carrytype;}
		}
		/// <summary>
		/// 结算方式ID
		/// </summary>
		public int PayType
		{
		set{ _paytype=value;}
		get{return _paytype;}
		}
		/// <summary>
		/// 结算币种ID
		/// </summary>
		public int CurrencyType
		{
		set{ _currencytype=value;}
		get{return _currencytype;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
		set{ _remark=value;}
		get{return _remark;}
		}
		/// <summary>
		/// 启用状态（0停用，1启用）
		/// </summary>
		public string UsedStatus
		{
		set{ _usedstatus=value;}
		get{return _usedstatus;}
		}
		/// <summary>
		/// 建档人ID
		/// </summary>
		public int Creator
		{
		set{ _creator=value;}
		get{return _creator;}
		}
		/// <summary>
		/// 建档日期
		/// </summary>
		public DateTime ? CreateDate
		{
		set{ _createdate=value;}
		get{return _createdate;}
		}
		/// <summary>
		/// 最后更新日期
		/// </summary>
		public DateTime ? ModifiedDate
		{
		set{ _modifieddate=value;}
		get{return _modifieddate;}
		}
		/// <summary>
		/// 最后更新用户ID（对应操作用户表中的UserID）
		/// </summary>
		public string ModifiedUserID
		{
		set{ _modifieduserid=value;}
		get{return _modifieduserid;}
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
        public int AllowDefaultDays
        {
            set { _allowdefaultdays = value; }
            get { return _allowdefaultdays; }
        }
		#endregion Model

    }
}