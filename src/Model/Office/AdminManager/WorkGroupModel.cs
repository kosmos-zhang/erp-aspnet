/**********************************************
 * 类作用：   WorkGroup表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/08
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：WorkGroupModel
    /// 描述：WorkGroup表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/08
    /// </summary>
   public class WorkGroupModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _workgroupno;
        private string _workgroupname;
        private string _workgrouptype;
        private string _workshiftno;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 自增ID
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
        /// 班组编号
        /// </summary>
        public string WorkGroupNo
        {
            set { _workgroupno = value; }
            get { return _workgroupno; }
        }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string WorkGroupName
        {
            set { _workgroupname = value; }
            get { return _workgroupname; }
        }
        /// <summary>
        /// 班组类型
        /// </summary>
        public string WorkGroupType
        {
            set { _workgrouptype = value; }
            get { return _workgrouptype; }
        }
        /// <summary>
        /// 对应班次号
        /// </summary>
        public string WorkShiftNo
        {
            set { _workshiftno = value; }
            get { return _workshiftno; }
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
        #endregion Model
    }
}
