/**********************************************
 * 类作用：   RectCheckElem表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectCheckElemModel
    /// 描述：RectCheckElem表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class RectCheckElemModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _elemname;
        private string _standard;
        private string _remark;
        private string _usedstatus;
        private string _usedstatusname;
        private string _modifieddate;
        private string _modifieduserid;
        private string _editflag;
        /// <summary>
        /// 编辑标识
        /// </summary>
        public string EditFlag
        {
            get
            {
                return _editflag;
            }
            set
            {
                _editflag = value;
            }
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
        /// 要素名称
        /// </summary>
        public string ElemName
        {
            set { _elemname = value; }
            get { return _elemname; }
        }
        /// <summary>
        /// 评分标准
        /// </summary>
        public string Standard
        {
            set { _standard = value; }
            get { return _standard; }
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
        /// 启用状态(0 停用，1 启用) 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 启用状态(0 停用，1 启用) 
        /// </summary>
        public string UsedStatusName
        {
            set { _usedstatusname = value; }
            get { return _usedstatusname; }
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
