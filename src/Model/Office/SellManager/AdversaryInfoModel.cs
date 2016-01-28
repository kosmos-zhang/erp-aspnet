using System;


namespace XBase.Model.Office.SellManager
{
    /// <summary>
	/// 类AdversaryInfo。竞争对手信息
	/// </summary>
    public class AdversaryInfoModel
    {
        public AdversaryInfoModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _bigtype;
        private int? _custtype;
        private int? _custclass;
        private string _custno;
        private string _custname;
        private string _shortnam;
        private string _pyshort;
        private int? _areaid;
        private DateTime? _setupdate;
        private string _artiperson;
        private string _companytype;
        private int? _staffcount;
        private decimal? _setupmoney;
        private string _setupaddress;
        private string _website;
        private decimal? _capitalscale;
        private string _sellarea;
        private decimal? _saleroomy;
        private decimal? _profity;
        private string _taxcd;
        private string _businumber;
        private string _istax;
        private string _address;
        private string _post;
        private string _contactname;
        private string _tel;
        private string _mobile;
        private string _email;
        private string _custnote;
        private string _product;
        private string _market;
        private string _sellmode;
        private string _project;
        private string _power;
        private string _advantage;
        private string _disadvantage;
        private string _policy;
        private string _remark;
        private string _usedstatus;
        private int? _creator;
        private DateTime? _creatdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// ID，自动生成
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
        /// 往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加工厂，6运输商，7其他），此处固定为3竞争对手
        /// </summary>
        public string BigType
        {
            set { _bigtype = value; }
            get { return _bigtype; }
        }
        /// <summary>
        /// 竞争对手类别ID(分类代码表定义)
        /// </summary>
        public int? CustType
        {
            set { _custtype = value; }
            get { return _custtype; }
        }
        /// <summary>
        /// 竞争对手分类ID(分类代码表定义)
        /// </summary>
        public int? CustClass
        {
            set { _custclass = value; }
            get { return _custclass; }
        }
        /// <summary>
        /// 竞争对手编号
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
        }
        /// <summary>
        /// 竞争对手名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 竞争简称
        /// </summary>
        public string ShortNam
        {
            set { _shortnam = value; }
            get { return _shortnam; }
        }
        /// <summary>
        /// 拼音缩写
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 所在区域（分类代码表定义，区域代码）
        /// </summary>
        public int? AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
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
        /// 单位性质（1事业，2企业，3社团，4自然人，5其他）
        /// </summary>
        public string CompanyType
        {
            set { _companytype = value; }
            get { return _companytype; }
        }
        /// <summary>
        /// 员工总数（个）
        /// </summary>
        public int? StaffCount
        {
            set { _staffcount = value; }
            get { return _staffcount; }
        }
        /// <summary>
        /// 注册资本(万元)
        /// </summary>
        public decimal? SetupMoney
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
        /// 公司网址
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 资产规模(万元)
        /// </summary>
        public decimal? CapitalScale
        {
            set { _capitalscale = value; }
            get { return _capitalscale; }
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
        /// 年销售额(万元)
        /// </summary>
        public decimal? SaleroomY
        {
            set { _saleroomy = value; }
            get { return _saleroomy; }
        }
        /// <summary>
        /// 年利润额(万元)
        /// </summary>
        public decimal? ProfitY
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
        /// 一般纳税人（0否，1是）
        /// </summary>
        public string IsTax
        {
            set { _istax = value; }
            get { return _istax; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
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
        /// 竞争对手对手简介
        /// </summary>
        public string CustNote
        {
            set { _custnote = value; }
            get { return _custnote; }
        }
        /// <summary>
        /// 主打产品
        /// </summary>
        public string Product
        {
            set { _product = value; }
            get { return _product; }
        }
        /// <summary>
        /// 市场占有率
        /// </summary>
        public string Market
        {
            set { _market = value; }
            get { return _market; }
        }
        /// <summary>
        /// 销售模式
        /// </summary>
        public string SellMode
        {
            set { _sellmode = value; }
            get { return _sellmode; }
        }
        /// <summary>
        /// 竞争产品/方案
        /// </summary>
        public string Project
        {
            set { _project = value; }
            get { return _project; }
        }
        /// <summary>
        /// 竞争能力
        /// </summary>
        public string Power
        {
            set { _power = value; }
            get { return _power; }
        }
        /// <summary>
        /// 竞争优势
        /// </summary>
        public string Advantage
        {
            set { _advantage = value; }
            get { return _advantage; }
        }
        /// <summary>
        /// 竞争劣势
        /// </summary>
        public string disadvantage
        {
            set { _disadvantage = value; }
            get { return _disadvantage; }
        }
        /// <summary>
        /// 应对策略
        /// </summary>
        public string Policy
        {
            set { _policy = value; }
            get { return _policy; }
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
        /// 建档人（对应员工表ID）
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime? CreatDate
        {
            set { _creatdate = value; }
            get { return _creatdate; }
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
