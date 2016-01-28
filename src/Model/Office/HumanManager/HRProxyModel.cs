/**********************************************
 * 类作用：   HRProxy表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/09
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：HRProxyModel
    /// 描述：HRProxy表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/09
    /// 最后修改时间：2009/03/09
    /// </summary>
    ///
    public class HRProxyModel
    {

        #region Model
        private string _id;
        private string _companycd;
        private string _proxycompanycd;
        private string _proxycompanyname;
        private string _nature;
        private string _address;
        private string _corporate;
        private string _telephone;
        private string _fax;
        private string _email;
        private string _website;
        private string _important;
        private string _cooperation;
        private string _standard;
        private string _contactname;
        private string _contacttel;
        private string _contactmobile;
        private string _contactweb;
        private string _contactposition;
        private string _contactcardno;
        private string _contactremark;
        private string _remark;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        /// <summary>
        /// 是否编辑标识
        /// </summary>
        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
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
        /// 代理公司代码
        /// </summary>
        public string ProxyCompanyCD
        {
            set { _proxycompanycd = value; }
            get { return _proxycompanycd; }
        }
        /// <summary>
        /// 代理公司名称
        /// </summary>
        public string ProxyCompanyName
        {
            set { _proxycompanyname = value; }
            get { return _proxycompanyname; }
        }
        /// <summary>
        /// 代理公司性质
        /// </summary>
        public string Nature
        {
            set { _nature = value; }
            get { return _nature; }
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
        /// 企业法人
        /// </summary>
        public string Corporate
        {
            set { _corporate = value; }
            get { return _corporate; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
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
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string Website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 重要程度(1不重要,2普通,3重要,4关键)
        /// </summary>
        public string Important
        {
            set { _important = value; }
            get { return _important; }
        }
        /// <summary>
        /// 合作关系(1 付费服务，2 一般服务)
        /// </summary>
        public string Cooperation
        {
            set { _cooperation = value; }
            get { return _cooperation; }
        }
        /// <summary>
        /// 收费标准
        /// </summary>
        public string Standard
        {
            set { _standard = value; }
            get { return _standard; }
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
        /// <summary>
        /// 联系人固定电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 联系人移动电话
        /// </summary>
        public string ContactMobile
        {
            set { _contactmobile = value; }
            get { return _contactmobile; }
        }
        /// <summary>
        /// 联系人网络通讯
        /// </summary>
        public string ContactWeb
        {
            set { _contactweb = value; }
            get { return _contactweb; }
        }
        /// <summary>
        /// 联系人公司职务
        /// </summary>
        public string ContactPosition
        {
            set { _contactposition = value; }
            get { return _contactposition; }
        }
        /// <summary>
        /// 联系人工号
        /// </summary>
        public string ContactCardNo
        {
            set { _contactcardno = value; }
            get { return _contactcardno; }
        }
        /// <summary>
        /// 联系人备注
        /// </summary>
        public string ContactRemark
        {
            set { _contactremark = value; }
            get { return _contactremark; }
        }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 启用标识(0 停用，1 启用)
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
