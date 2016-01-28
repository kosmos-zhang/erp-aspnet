using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
   public   class SellPlanModel
    {
        #region Model
        private int? _id;
        private string _companycd;
        private string _planno;
        private string _title;
        private string _plantype;
        private string _planyear;
        private string _plantime;
        private decimal? _plantotal;
        private DateTime? _startdate;
        private DateTime? _enddate;
        private decimal? _minplantotal;
        private string _hortation;
        private string _canviewuser;
        private string _canviewusername;
        private string _remark;
        private string _billstatus;
        private int? _creator;
        private DateTime? _createdate;
        private int? _confirmor;
        private DateTime? _confirmdate;
        private int? _closer;
        private DateTime? _closedate;
        private DateTime? _modifieddate;
        private string _modifieduserid;
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
        /// 计划主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 计划类型（1.日、2.周、3.月、4.季、5.半年、6.年7.其他）
        /// </summary>
        public string PlanType
        {
            set { _plantype = value; }
            get { return _plantype; }
        }
        /// <summary>
        /// 目标年份
        /// </summary>
        public string PlanYear
        {
            set { _planyear = value; }
            get { return _planyear; }
        }
        /// <summary>
        /// 目标的月份或季度
        /// </summary>
        public string PlanTime
        {
            set { _plantime = value; }
            get { return _plantime; }
        }
        /// <summary>
        /// 目标总金额
        /// </summary>
        public decimal? PlanTotal
        {
            set { _plantotal = value; }
            get { return _plantotal; }
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 最低目标额
        /// </summary>
        public decimal? MinPlanTotal
        {
            set { _minplantotal = value; }
            get { return _minplantotal; }
        }
        /// <summary>
        /// 奖励方案
        /// </summary>
        public string Hortation
        {
            set { _hortation = value; }
            get { return _hortation; }
        }
        /// <summary>
        /// 可查看该销售机会的人员（ID，多个）
        /// </summary>
        public string CanViewUser
        {
            set { _canviewuser = value; }
            get { return _canviewuser; }
        }
        /// <summary>
        /// 可查看该销售机会的人员姓名
        /// </summary>
        public string CanViewUserName
        {
            set { _canviewusername = value; }
            get { return _canviewusername; }
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
        /// 单据状态（1制单，2执行， 3手工结单，4自动结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int? Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public int? Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 结单人ID
        /// </summary>
        public int? Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单日期
        /// </summary>
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
