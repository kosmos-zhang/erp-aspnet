/**********************************************
 * 类作用：   CarApply表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/27
 ***********************************************/
using System;


namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarApplyModel
    /// 描述：CarApply表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/27
    /// </summary>
   public class CarApplyModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _recordno;
        private string _title;
        private string _carno;
        private DateTime _applydate;
        private int _appler;
        private int _applydept;
        private string _reason;
        private int _loadhumans;
        private decimal _loadgoods;
        private DateTime _plandate;
        private string _plantime;
        private DateTime _returndate;
        private string _returntime;
        private int _planmileage;
        private string _billstatus;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private string _remark;
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
        /// 请车单编号
        /// </summary>
        public string RecordNo
        {
            set { _recordno = value; }
            get { return _recordno; }
        }
        /// <summary>
        /// 请车单主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 请车人
        /// </summary>
        public int Appler
        {
            set { _appler = value; }
            get { return _appler; }
        }
        /// <summary>
        /// 请车人部门
        /// </summary>
        public int ApplyDept
        {
            set { _applydept = value; }
            get { return _applydept; }
        }
        /// <summary>
        /// 申请事由
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 人数
        /// </summary>
        public int LoadHumans
        {
            set { _loadhumans = value; }
            get { return _loadhumans; }
        }
        /// <summary>
        /// 载货重（吨）
        /// </summary>
        public decimal LoadGoods
        {
            set { _loadgoods = value; }
            get { return _loadgoods; }
        }
        /// <summary>
        /// 预定日期
        /// </summary>
        public DateTime PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 预定时间（精确到分）
        /// </summary>
        public string PlanTime
        {
            set { _plantime = value; }
            get { return _plantime; }
        }
        /// <summary>
        /// 预计返回日期
        /// </summary>
        public DateTime ReturnDate
        {
            set { _returndate = value; }
            get { return _returndate; }
        }
        /// <summary>
        /// 预计返回时间（精确到分）
        /// </summary>
        public string ReturnTime
        {
            set { _returntime = value; }
            get { return _returntime; }
        }
        /// <summary>
        /// 预计行驶公里数
        /// </summary>
        public int PlanMileage
        {
            set { _planmileage = value; }
            get { return _planmileage; }
        }
        /// <summary>
        /// 制单状态（1制单，2结单）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
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
