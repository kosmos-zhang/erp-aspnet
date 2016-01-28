/***********************************************
 * 类作用：   PurchaseManager表数据模板        *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/04/25                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 类名：ProviderLinkManModel
    /// 描述：ProviderLinkMan表数据模板
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/25
    /// 最后修改时间：2009/04/25
    /// </summary>
    ///
    public class ProviderLinkManModel
    {
        #region Model
        private int _id;
        private string _custno;
        private string _companycd;
        private string _linkmanname;
        private string _sex;
        private string _important;
        private string _company;
        private string _appellation;
        private string _department;
        private string _position;
        private string _operation;
        private string _worktel;
        private string _fax;
        private string _handset;
        private string _mailaddress;
        private string _hometel;
        private string _msn;
        private string _qq;
        private string _post;
        private string _homeaddress;
        private string _remark;
        private string _age;
        private string _likes;
        private int _linktype;
        private DateTime ? _birthday;
        private string _papertype;
        private string _papernum;
        private string _photo;
        private DateTime ? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 供应商编号(对应供应商信息表中的编号)
        /// </summary>
        public string CustNo
        {
            set { _custno = value; }
            get { return _custno; }
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
        /// 联系人姓名
        /// </summary>
        public string LinkManName
        {
            set { _linkmanname = value; }
            get { return _linkmanname; }
        }
        /// <summary>
        /// 性别（1男2女）
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
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
        /// 单位
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 称谓
        /// </summary>
        public string Appellation
        {
            set { _appellation = value; }
            get { return _appellation; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>
        /// 负责业务
        /// </summary>
        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        /// <summary>
        /// 工作电话
        /// </summary>
        public string WorkTel
        {
            set { _worktel = value; }
            get { return _worktel; }
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
        /// 移动电话
        /// </summary>
        public string Handset
        {
            set { _handset = value; }
            get { return _handset; }
        }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string MailAddress
        {
            set { _mailaddress = value; }
            get { return _mailaddress; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomeTel
        {
            set { _hometel = value; }
            get { return _hometel; }
        }
        /// <summary>
        /// MSN
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
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
        /// 住址
        /// </summary>
        public string HomeAddress
        {
            set { _homeaddress = value; }
            get { return _homeaddress; }
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
        /// 年龄
        /// </summary>
        public string Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Likes
        {
            set { _likes = value; }
            get { return _likes; }
        }
        /// <summary>
        /// 供应商联系人类型ID（分类代码表设置）
        /// </summary>
        public int LinkType
        {
            set { _linktype = value; }
            get { return _linktype; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime ? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string PaperType
        {
            set { _papertype = value; }
            get { return _papertype; }
        }
        /// <summary>
        /// 证件号
        /// </summary>
        public string PaperNum
        {
            set { _papernum = value; }
            get { return _papernum; }
        }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ? ModifiedDate
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
