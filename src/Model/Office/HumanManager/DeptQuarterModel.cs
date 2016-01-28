/**********************************************
 * 类作用：   DeptInfo表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/09
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：DeptModel
    /// 描述：DeptInfo表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/09
    /// 最后修改时间：2009/04/09
    /// </summary>
    ///
    public class DeptQuarterModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _deptid;
        private string _quarterno;
        private string _superquarterid;
        private string _pyshort;
        private string _quartername;
        private string _typeid;
        private string _levelid;
        private string _keyflag;
        private string _duty;
        private string _dutyrequire;
        private string _attachment;
        private string _pageattachment;
        private string _usedstatus;
        private string _description;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;

        private string _QuterContent;
        /// <summary>
        /// 岗位说明书内容
        /// </summary>
        public string QuterContent
        {
            set { _QuterContent = value; }
            get { return _QuterContent; }
        }



        /// <summary>
        /// 编辑标识
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
        /// 部门ID（对应部门表ID）
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string QuarterNo
        {
            set { _quarterno = value; }
            get { return _quarterno; }
        }
        /// <summary>
        /// 上级岗位ID
        /// </summary>
        public string SuperQuarterID
        {
            set { _superquarterid = value; }
            get { return _superquarterid; }
        }
        /// <summary>
        /// 岗位拼音代码
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string QuarterName
        {
            set { _quartername = value; }
            get { return _quartername; }
        }
        /// <summary>
        /// 岗位类型ID（对应分类代码表ID）
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 岗位级别ID（对应分类代码表ID）
        /// </summary>
        public string LevelID
        {
            set { _levelid = value; }
            get { return _levelid; }
        }
        /// <summary>
        /// 是否关键岗位(0 否，1 是)
        /// </summary>
        public string KeyFlag
        {
            set { _keyflag = value; }
            get { return _keyflag; }
        }
        /// <summary>
        /// 岗位职责
        /// </summary>
        public string Duty
        {
            set { _duty = value; }
            get { return _duty; }
        }
        /// <summary>
        /// 任职资格
        /// </summary>
        public string DutyRequire
        {
            set { _dutyrequire = value; }
            get { return _dutyrequire; }
        }
        /// <summary>
        /// 附件 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 附件 
        /// </summary>
        public string PageAttachment
        {
            set { _pageattachment = value; }
            get { return _pageattachment; }
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
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
