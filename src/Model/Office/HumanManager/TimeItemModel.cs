/**********************************************
 * 类作用：   TimeItem表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/05/06
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TimeItemModel
    /// 描述：TimeItem表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/06
    /// 最后修改时间：2009/05/06
    /// </summary>
    ///
    public class TimeItemModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _timeno;
        private string _timename;
        private string _unitprice;
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
        /// 项目编号
        /// </summary>
        public string TimeNo
        {
            set { _timeno = value; }
            get { return _timeno; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string TimeName
        {
            set { _timename = value; }
            get { return _timename; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public string UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
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
