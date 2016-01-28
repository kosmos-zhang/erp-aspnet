/**********************************************
 * 类作用：   officedba.MeetingDecision表数据模板
 * 建立人：   张玉圆
 * 建立时间： 2009/05/06
 ***********************************************/

using System;

namespace XBase.Model.Office.AdminManager
{
    public class MeetingDecisionModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _recordno;
        private string _decisionno;
        private string _contents;
        private int _principal;
        private string _aim;
        private DateTime? _completedate;
        private string _status;
        private int _cheker;
        private DateTime? _checkdate;
        private string _checkresult;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 会议记录编号（对应会议记录编号）
        /// </summary>
        public string RecordNo
        {
            set { _recordno = value; }
            get { return _recordno; }
        }
        /// <summary>
        /// 会议决议编号
        /// </summary>
        public string DecisionNo
        {
            set { _decisionno = value; }
            get { return _decisionno; }
        }
        /// <summary>
        /// 决议事项
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 执行负责人
        /// </summary>
        public int Principal
        {
            set { _principal = value; }
            get { return _principal; }
        }
        /// <summary>
        /// 实施目标
        /// </summary>
        public string Aim
        {
            set { _aim = value; }
            get { return _aim; }
        }
        /// <summary>
        /// 完成期限
        /// </summary>
        public DateTime? CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 完成状态（1未完成，2已完成）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 检查人
        /// </summary>
        public int Cheker
        {
            set { _cheker = value; }
            get { return _cheker; }
        }
        /// <summary>
        /// 核查时间
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 核查结果
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
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
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
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
