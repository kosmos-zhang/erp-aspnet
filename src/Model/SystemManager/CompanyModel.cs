/**********************************************
 * 类作用：   Company表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 ***********************************************/
using System;
namespace XBase.Model.SystemManager
{
 public class CompanyModel
    {
        private string _companycd;
        private string _companynamecn;
        private string _companynameen;
        private string _provcd;
        private string _citycd;
        private string _addr;
        private string _contact;
        private string _tel;
        private string _fax;
        private string _post;
        private string _homepage;
        private string _email;
        private string _qq;
        private string _msn;
        private string _im;
        private int _tradecd;
        private int _staff;
        private string _size;
        private string _production;
        private decimal _sale;
        private string _credit;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;

        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 企业中文名称
        /// </summary>
        public string CompanyNameCn
        {
            set { _companynamecn = value; }
            get { return _companynamecn; }
        }
        /// <summary>
        /// 企业英文名称
        /// </summary>
        public string CompanyNameEn
        {
            set { _companynameen = value; }
            get { return _companynameen; }
        }
        /// <summary>
        /// 所属省份
        /// </summary>
        public string ProvCD
        {
            set { _provcd = value; }
            get { return _provcd; }
        }
        /// <summary>
        /// 所属地市
        /// </summary>
        public string CityCD
        {
            set { _citycd = value; }
            get { return _citycd; }
        }
        /// <summary>
        /// 详细地址信息
        /// </summary>
        public string Addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            set { _contact = value; }
            get { return _contact; }
        }
        /// <summary>
        /// 联系电话
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
        /// 邮编
        /// </summary>
        public string Post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>
        /// 主页HTTP
        /// </summary>
        public string HomePage
        {
            set { _homepage = value; }
            get { return _homepage; }
        }
        /// <summary>
        /// Email地址
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// MSN号
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 其他IM联系方式
        /// </summary>
        public string IM
        {
            set { _im = value; }
            get { return _im; }
        }
        /// <summary>
        /// 所属行业
        /// </summary>
        public int TradeCD
        {
            set { _tradecd = value; }
            get { return _tradecd; }
        }
        /// <summary>
        /// 企业人数
        /// </summary>
        public int Staff
        {
            set { _staff = value; }
            get { return _staff; }
        }
        /// <summary>
        /// 企业规模
        /// </summary>
        public string Size
        {
            set { _size = value; }
            get { return _size; }
        }
        /// <summary>
        /// 年产量信息
        /// </summary>
        public string Production
        {
            set { _production = value; }
            get { return _production; }
        }
        /// <summary>
        /// 年销售额（万元）
        /// </summary>
        public decimal Sale
        {
            set { _sale = value; }
            get { return _sale; }
        }
        /// <summary>
        /// 企业信誉信息
        /// </summary>
        public string Credit
        {
            set { _credit = value; }
            get { return _credit; }
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
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
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

    }
}
