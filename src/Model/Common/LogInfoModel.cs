/**********************************************
 * 类作用：   LogInfo表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/06
 ***********************************************/
using System;

namespace XBase.Model.Common
{
    /// <summary>
    /// 类名：LogInfoModel
    /// 描述：LogInfo表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/06
    /// 最后修改时间：2009/03/06
    /// </summary>
    ///
    public class LogInfoModel
    {
        #region Model
        private string _companycd;
        private string _userid;
        private DateTime _operatedate;
        private string _moduleid;
        private string _objectid;
        private string _objectname;
        private string _element;
        private string _remark;

        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 操作用户ID
        /// </summary>
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateDate
        {
            set { _operatedate = value; }
            get { return _operatedate; }
        }
        /// <summary>
        /// 操作模块ID
        /// </summary>
        public string ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 操作单据编号
        /// </summary>
        public string ObjectID
        {
            set { _objectid = value; }
            get { return _objectid; }
        }
        /// <summary>
        /// 操作对象
        /// </summary>
        public string ObjectName
        {
            set { _objectname = value; }
            get { return _objectname; }
        }
        /// <summary>
        /// 涉及关键元素
        /// </summary>
        public string Element
        {
            set { _element = value; }
            get { return _element; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
