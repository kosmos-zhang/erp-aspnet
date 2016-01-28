using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
   public class OtherCorpInfoModel
    {
        public OtherCorpInfoModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _bigtype;
        private string _custno;
        private string _custname;
        private string _corpnam;
        private string _pyshort;
        private string _custnote;
        private string _areaid;
        private string _companytype;
        private string _staffcount;
        private string _setupdate;
        private string _artiperson;
        private string _setupmoney;
        private string _setupaddress;
        private string _capitalscale;
        private string _saleroomy;
        private string _profity;
        private string _taxcd;
        private string _businumber;
        private string _istax;
        private string _sellarea;
        private string _countryid;
        private string _province;
        private string _city;
        private string _post;
        private string _contactname;
        private string _tel;
        private string _fax;
        private string _mobile;
        private string _email;
        private string _addr;
        private string _billtype;
        private string _paytype;
        private string _currencytype;
        private string _remark;
        private string _usedstatus;
        private string _creator;
        private string _MoneyType;
        private string _createdate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _EmployeeName;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string EmployeeName
        {
            set { _EmployeeName = value; }
            get { return _EmployeeName; }
        }
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
       /// <summary>
       ///  支付方式
       /// </summary>
        public string MoneyType
        {
            set { _MoneyType = value; }
            get { return _MoneyType; }
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
        /// 往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加工厂，6运输商，7其他），此处可选5-7
        /// </summary>
        public string BigType
        {
            set { _bigtype = value; }
            get { return _bigtype; }
        }
        /// <summary>
        /// 往来单位编号
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 往来单位名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 往来单位简称
        /// </summary>
        public string CorpNam
        {
            set { _corpnam = value; }
            get { return _corpnam; }
        }
        /// <summary>
        /// 往来单位拼音代码
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 往来单位简介
        /// </summary>
        public string CustNote
        {
            set { _custnote = value; }
            get { return _custnote; }
        }
        /// <summary>
        /// 区域ID（分类代码表定义）
        /// </summary>
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 单位性质
        /// </summary>
        public string CompanyType
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// 人员规模
        /// </summary>
        public string StaffCount
        {
            set { _staffcount = value; }
            get { return _staffcount; }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        public string SetupDate
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
        public string SetupMoney
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
        public string CapitalScale
        {
            set { _capitalscale = value; }
            get { return _capitalscale; }
        }
        /// <summary>
        /// 年销售额(万元)
        /// </summary>
        public string SaleroomY
        {
            set { _saleroomy = value; }
            get { return _saleroomy; }
        }
        /// <summary>
        /// 年利润额(万元)
        /// </summary>
        public string ProfitY
        {
            set { _profity = value; }
            get { return _profity; }
        }
        /// <summary>
        /// 税务登记号
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
        public string isTax
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
        /// 国家地区ID(对应国家代码表ID)
        /// </summary>
        public string CountryID
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
        /// 邮编
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
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
        /// 地址
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
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
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 结算币种ID
        /// </summary>
        public string CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
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
        /// 启用状态（0停用，1启用）
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 建档人ID
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表中的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
