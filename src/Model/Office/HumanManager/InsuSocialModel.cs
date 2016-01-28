/**********************************************
 * 类作用：   InsuSocial表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/06
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：InsuSocialModel
    /// 描述：InsuSocial表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/06
    /// 最后修改时间：2009/05/06
    /// </summary>
    ///
    public class InsuSocialModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _insurancename;
        private string _insuranceway;
        private string _companypayrate;
        private string _personpayrate;
        private string _usedstatus;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        /// <summary>
        /// 编辑模式
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
        /// 保险名称
        /// </summary>
        public string InsuranceName
        {
            set { _insurancename = value; }
            get { return _insurancename; }
        }
        /// <summary>
        /// 投保方式(1按月，2按年)

        /// </summary>
        public string InsuranceWay
        {
            set { _insuranceway = value; }
            get { return _insuranceway; }
        }
        /// <summary>
        /// 单位缴费比例
        /// </summary>
        public string CompanyPayRate
        {
            set { _companypayrate = value; }
            get { return _companypayrate; }
        }
        /// <summary>
        /// 个人缴费比例
        /// </summary>
        public string PersonPayRate
        {
            set { _personpayrate = value; }
            get { return _personpayrate; }
        }
        /// <summary>
        /// 启用状态(0 停用，1 启用)

        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
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
