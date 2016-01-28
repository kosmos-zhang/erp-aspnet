/*********************************
 *描述：销售机会实体
 * 修改记录:
 *      1.修改内容：添加手机提醒相关字段，即：是否设置手机提醒，提醒时间，提醒手机号，提醒内容。
 *        修改人：hexw
 *        修改时间：2010-6-7
 *      2.修改内容：添加接收人字段。
 *        修改人：hexw
 *        修改时间：2010-06-08
 *******************************/
using System;

namespace XBase.Model.Office.SellManager
{

    /// <summary>
    /// 实体类SellChance 。
    /// </summary>
    public class SellChanceModel
    {
        public SellChanceModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private int? _custid;
        private int? _custtype;
        private string _custtel;
        private string _chanceno;
        private string _title;
        private int? _chancetype;
        private int? _hapsource;
        private int? _seller;
        private int? _selldeptid;
        private DateTime? _finddate;
        private string _provideman;
        private string _requires;
        private DateTime? _intenddate;
        private decimal? _intendmoney;
        private string _remark;
        private string _isquoted;
        private string _canviewuser;
        private string _canviewusername;
        private int? _creator;
        private DateTime? _createdate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _isMobileNotice;//是否设置手机提醒
        private string _remindTime;//提醒时间
        private string _remindMTel;//提醒手机号码
        private string _remindContent;//提醒内容
        private string _receiverID;//接收人

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
        /// 客户ID（对应客户表ID）
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户类型ID
        /// </summary>
        public int? CustType
        {
            set { _custtype = value; }
            get { return _custtype; }
        }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string CustTel
        {
            set { _custtel = value; }
            get { return _custtel; }
        }
        /// <summary>
        /// 销售机会编号
        /// </summary>
        public string ChanceNo
        {
            set { _chanceno = value; }
            get { return _chanceno; }
        }
        /// <summary>
        /// 销售机会主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 销售机会类型(对应分类代码表ID)
        /// </summary>
        public int? ChanceType
        {
            set { _chancetype = value; }
            get { return _chancetype; }
        }
        /// <summary>
        /// 销售机会来源ID(对应分类代码表ID)
        /// </summary>
        public int? HapSource
        {
            set { _hapsource = value; }
            get { return _hapsource; }
        }
        /// <summary>
        /// 业务员(对应员工表ID)
        /// </summary>
        public int? Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 部门(对部门表ID)
        /// </summary>
        public int? SellDeptId
        {
            set { _selldeptid = value; }
            get { return _selldeptid; }
        }
        /// <summary>
        /// 发现日期
        /// </summary>
        public DateTime? FindDate
        {
            set { _finddate = value; }
            get { return _finddate; }
        }
        /// <summary>
        /// 提供人
        /// </summary>
        public string ProvideMan
        {
            set { _provideman = value; }
            get { return _provideman; }
        }
        /// <summary>
        /// 需求描述
        /// </summary>
        public string Requires
        {
            set { _requires = value; }
            get { return _requires; }
        }
        /// <summary>
        /// 预计签单日期
        /// </summary>
        public DateTime? IntendDate
        {
            set { _intenddate = value; }
            get { return _intenddate; }
        }
        /// <summary>
        /// 预期金额
        /// </summary>
        public decimal? IntendMoney
        {
            set { _intendmoney = value; }
            get { return _intendmoney; }
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
        /// 是否被报价(0-否，1-是)
        /// </summary>
        public string IsQuoted
        {
            set { _isquoted = value; }
            get { return _isquoted; }
        }

        /// <summary>
        /// 可查看该销售机会的人员（ID，多个）
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该销售机会的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
        }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }

        /// <summary>
        /// 是否设置手机提醒
        /// </summary>
        public string IsMobileNotice
        {
            set { _isMobileNotice = value; }
            get { return _isMobileNotice; }
        }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public string RemindTime
        {
            set { _remindTime = value; }
            get { return _remindTime; }
        }
        /// <summary>
        /// 提醒手机号
        /// </summary>
        public string RemindMTel
        {
            set { _remindMTel = value; }
            get { return _remindMTel; }
        }
        /// <summary>
        /// 提醒内容
        /// </summary>
        public string RemindContent
        {
            set { _remindContent = value; }
            get { return _remindContent; }
        }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public string ReceiverID
        {
            set { _receiverID = value; }
            get { return _receiverID; }
        }
        #endregion Model

    }
}
