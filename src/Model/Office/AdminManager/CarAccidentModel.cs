/**********************************************
 * 类作用：   CarAccident表数据模板
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;

namespace XBase.Model.Office.AdminManager
{
    /// <summary>
    /// 类名：CarAccidentModel
    /// 描述：CarAccident表数据模板
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarAccidentModel
   {
        #region CarAccident表数据模板
        private int _id;
        private string _companycd;
        private string _carno;
        private int _employeeid;
        private string _dispatchreason;
        private DateTime _happendate;
        private string _accidentplace;
        private string _accidentdescription;
        private int _transactor;
        private string _damagelevel;
        private decimal _comburdensum;
        private decimal _insurburdensum;
        private decimal _selfburdensum;
        private decimal _opposecompens;
        private string _reconciliation;
        private string _opposeinfo;
        private string _dealresult;
        private string _reamrk;
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
        /// 车辆编号（对应车辆信息表编号）
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 驾驶人（对应员工表ID）
        /// </summary>
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 出车事由
        /// </summary>
        public string DispatchReason
        {
            set { _dispatchreason = value; }
            get { return _dispatchreason; }
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenDate
        {
            set { _happendate = value; }
            get { return _happendate; }
        }
        /// <summary>
        /// 事故地点
        /// </summary>
        public string AccidentPlace
        {
            set { _accidentplace = value; }
            get { return _accidentplace; }
        }
        /// <summary>
        /// 事故描述
        /// </summary>
        public string AccidentDescription
        {
            set { _accidentdescription = value; }
            get { return _accidentdescription; }
        }
        /// <summary>
        /// 我方处理人
        /// </summary>
        public int Transactor
        {
            set { _transactor = value; }
            get { return _transactor; }
        }
        /// <summary>
        /// 损坏程度
        /// </summary>
        public string DamageLevel
        {
            set { _damagelevel = value; }
            get { return _damagelevel; }
        }
        /// <summary>
        /// 公司暂且负担金额（元）
        /// </summary>
        public decimal ComBurdenSum
        {
            set { _comburdensum = value; }
            get { return _comburdensum; }
        }
        /// <summary>
        /// 保险赔偿金额（元）
        /// </summary>
        public decimal InsurBurdenSum
        {
            set { _insurburdensum = value; }
            get { return _insurburdensum; }
        }
        /// <summary>
        /// 驾驶人负担金额（元）
        /// </summary>
        public decimal SelfBurdenSum
        {
            set { _selfburdensum = value; }
            get { return _selfburdensum; }
        }
        /// <summary>
        /// 对方赔偿金额（元）
        /// </summary>
        public decimal OpposeCompens
        {
            set { _opposecompens = value; }
            get { return _opposecompens; }
        }
        /// <summary>
        /// 和解内容
        /// </summary>
        public string Reconciliation
        {
            set { _reconciliation = value; }
            get { return _reconciliation; }
        }
        /// <summary>
        /// 对方情况描述
        /// </summary>
        public string OpposeInfo
        {
            set { _opposeinfo = value; }
            get { return _opposeinfo; }
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string DealResult
        {
            set { _dealresult = value; }
            get { return _dealresult; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Reamrk
        {
            set { _reamrk = value; }
            get { return _reamrk; }
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
