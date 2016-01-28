/**********************************************
 * 类作用：   CarApply表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/04/29
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarDispatchModel
    /// 描述：CarDispatch表数据模板
    /// 作者：lysong
    /// 创建时间：2009/04/29
    /// </summary>
    public class CarDispatchModel
    {
        #region CarDispatch表数据模板
        private int _id;
        private string _companycd;
        private string _recordno;
        private string _title;
        private int _applyid;
        private string _carno;
        private DateTime _applydate;
        private int _appler;
        private int _applydept;
        private string _reason;
        private int _loadhumans;
        private decimal _loadgoods;
        private DateTime _requiredate;
        private string _requiretime;
        private DateTime _planreturndate;
        private string _planreturntime;
        private int _planmileage;
        private DateTime _outdate;
        private string _outtime;
        private DateTime _backdate;
        private string _backtime;
        private string _isreturn;
        private int _realmileage;
        private int _creator;
        private DateTime _createdate;
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
        /// 派车单据号
        /// </summary>
        public string RecordNo
        {
            set { _recordno = value; }
            get { return _recordno; }
        }
        /// <summary>
        /// 派车单主题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 对应请车单（对应请车单表中的ID）
        /// </summary>
        public int ApplyID
        {
            set { _applyid = value; }
            get { return _applyid; }
        }
        /// <summary>
        /// 车辆编号（对应车辆信息表编号）
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
        /// 请车部门
        /// </summary>
        public int ApplyDept
        {
            set { _applydept = value; }
            get { return _applydept; }
        }
        /// <summary>
        /// 请车事由
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 人数（人）
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
        /// 需车日期
        /// </summary>
        public DateTime RequireDate
        {
            set { _requiredate = value; }
            get { return _requiredate; }
        }
        /// <summary>
        /// 需车时间（精确到分）
        /// </summary>
        public string RequireTime
        {
            set { _requiretime = value; }
            get { return _requiretime; }
        }
        /// <summary>
        /// 预计返回日期
        /// </summary>
        public DateTime PlanReturnDate
        {
            set { _planreturndate = value; }
            get { return _planreturndate; }
        }
        /// <summary>
        /// 预计返回时间（精确到分）
        /// </summary>
        public string PlanReturnTime
        {
            set { _planreturntime = value; }
            get { return _planreturntime; }
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
        /// 出车日期
        /// </summary>
        public DateTime OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
        }
        /// <summary>
        /// 出车时间
        /// </summary>
        public string OutTime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }
        /// <summary>
        /// 回车日期
        /// </summary>
        public DateTime BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 回车时间
        /// </summary>
        public string BackTime
        {
            set { _backtime = value; }
            get { return _backtime; }
        }
        /// <summary>
        /// 是否已还车（0否，1是）
        /// </summary>
        public string isReturn
        {
            set { _isreturn = value; }
            get { return _isreturn; }
        }
        /// <summary>
        /// 实际行驶公里数
        /// </summary>
        public int RealMileage
        {
            set { _realmileage = value; }
            get { return _realmileage; }
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
