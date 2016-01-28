using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public  class SellPlanDetailModel
    {
        #region Model
        private int? _id;
        private string _companycd;
        private string _planno;
        private string _detailtype;
        private int? _parentid;
        private int? _detailid;
        private decimal? _detailtotal;
        private decimal? _mindetailotal;
        private DateTime? _summarizedate;
        private string _summarizenote;
        private int? _summarizer;
        private string _aimrealresult;
        private string _addorcut;
        private string _difference;
        private decimal? _completepercent;
        private string _issummarize;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int? ID
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
        /// 计划编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
        }
        /// <summary>
        /// 明细种类（1.部门明细2.人员明细3.物品明细）
        /// </summary>
        public string DetailType
        {
            set { _detailtype = value; }
            get { return _detailtype; }
        }
        /// <summary>
        /// 上级明细的id
        /// </summary>
        public int? ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 明细信息对应的部门、物品或人员id
        /// </summary>
        public int? DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 目标总金额
        /// </summary>
        public decimal? DetailTotal
        {
            set { _detailtotal = value; }
            get { return _detailtotal; }
        }
        /// <summary>
        /// 最低目标额
        /// </summary>
        public decimal? MinDetailotal
        {
            set { _mindetailotal = value; }
            get { return _mindetailotal; }
        }
        /// <summary>
        /// 总结时间
        /// </summary>
        public DateTime? SummarizeDate
        {
            set { _summarizedate = value; }
            get { return _summarizedate; }
        }
        /// <summary>
        /// 总结内容
        /// </summary>
        public string SummarizeNote
        {
            set { _summarizenote = value; }
            get { return _summarizenote; }
        }
        /// <summary>
        /// 总结人
        /// </summary>
        public int? Summarizer
        {
            set { _summarizer = value; }
            get { return _summarizer; }
        }
        /// <summary>
        /// 目标实绩
        /// </summary>
        public string AimRealResult
        {
            set { _aimrealresult = value; }
            get { return _aimrealresult; }
        }
        /// <summary>
        /// 完成情况（0低于目标值，1等于目标值，2超过目标值）
        /// </summary>
        public string AddOrCut
        {
            set { _addorcut = value; }
            get { return _addorcut; }
        }
        /// <summary>
        /// 差额
        /// </summary>
        public string Difference
        {
            set { _difference = value; }
            get { return _difference; }
        }
        /// <summary>
        /// 目标达成率%
        /// </summary>
        public decimal? CompletePercent
        {
            set { _completepercent = value; }
            get { return _completepercent; }
        }
        /// <summary>
        /// 是否以总结（0否，1是）
        /// </summary>
        public string IsSummarize
        {
            set { _issummarize = value; }
            get { return _issummarize; }
        }
        #endregion Model
    }
}
