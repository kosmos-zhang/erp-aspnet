/**********************************************
 * 类作用：   RectPlan表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/30
 ***********************************************/
using System;
using System.Collections;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectPlanModel
    /// 描述：RectPlan表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/30
    /// 最后修改时间：2009/03/30
    /// </summary>
    ///
    public class RectPlanModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _title;
        private string _startdate;
        private string _principal;
        private string _status;
        private string _modifieddate;
        private string _modifieduserid;
        private ArrayList _goalList;
        private ArrayList _publishList;
        private string _editflag;
        private string _endDate;
        private string _planFee;
        private string _feeNote;
        private string _joinMan;
        private string _joinNote;
        private string _requireNum;
        /// <summary>
        /// 总需求人数
        /// </summary>
        public string RequireNum
        {
            set { _requireNum = value; }
            get { return _requireNum; }
        }
        /// <summary>
        /// 招聘小组成员分工说明
        /// </summary>
        public string JoinNote
        {
            set { _joinNote = value; }
            get { return _joinNote; }
        }
        /// <summary>
        /// 招聘小组成员(多选)
        /// </summary>
        public string JoinMan
        {
            set { _joinMan = value; }
            get { return _joinMan; }
        }
        /// <summary>
        /// 预算说明
        /// </summary>
        public string FeeNote
        {
            set { _feeNote = value; }
            get { return _feeNote; }
        }
        /// <summary>
        /// 招聘预算（元）
        /// </summary>
        public string PlanFee
        {
            set { _planFee = value; }
            get { return _planFee; }
        }




        /// <summary>
        /// 结束时期
        /// </summary>
        public string EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
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
        /// 招聘目标
        /// </summary>
        public ArrayList GoalList
        {
            set { _goalList = value; }
            get {
                if (_goalList == null) _goalList = new ArrayList();
                return _goalList; 
            }
        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public ArrayList PublishList
        {
            set { _publishList = value; }
            get {

                if (_publishList == null) _publishList = new ArrayList();
                return _publishList; 
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
        /// 主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 负责人(对应员工表ID)
        /// </summary>
        public string Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 状态(0未完成，1已完成)
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
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
