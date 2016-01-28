/**********************************************
 * 类作用：   RectApply表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/30
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectApplyModel
    /// 描述：RectApply表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/30
    /// 最后修改时间：2009/03/30
    /// </summary>
    ///
    public class RectPublishModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _publishplace;
        private string _publishdate;
        private string _valid;
        private string _enddate;
        private string _cost;
        private string _effect;
        private string _status;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 招聘计划编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
        }
        /// <summary>
        /// 发布渠道
        /// </summary>
        public string PublishPlace
        {
            set { _publishplace = value; }
            get { return _publishplace; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate
        {
            set { _publishdate = value; }
            get { return _publishdate; }
        }
        /// <summary>
        /// 有效时间(天数)
        /// </summary>
        public string Valid
        {
            set { _valid = value; }
            get { return _valid; }
        }
        /// <summary>
        /// 截止时间
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public string Cost
        {
            set { _cost = value; }
            get { return _cost; }
        }
        /// <summary>
        /// 效果
        /// </summary>
        public string Effect
        {
            set { _effect = value; }
            get { return _effect; }
        }
        /// <summary>
        /// 发布状态(0 暂停，1 发布中，2 结束)
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 最后更新时间
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
