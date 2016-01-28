using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class StandardSequModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _sequno;
        private string _sequname;
        private string _pyshort;
        private int _wcid;
        private string _checkway;
        private string _timeunit;
        private int _readytime;
        private int _runtime;
        private string _ischarge;
        private string _isoutsource;
        private decimal _timewage;
        private decimal _piecewage;
        private int _creator;
        private DateTime _createdate;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _detailcompanycd;
        private string _detailsequno;
        private int _detailid;
        private int _detailtechid;
        private string _detailremark;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string SequNo
        {
            set { _sequno = value; }
            get { return _sequno; }
        }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string SequName
        {
            set { _sequname = value; }
            get { return _sequname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 工作中心ID
        /// </summary>
        public int WCID
        {
            set { _wcid = value; }
            get { return _wcid; }
        }
        /// <summary>
        /// 检验方式0:免检1:全检2:抽检
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 时间单位：1天，2时，3分，4秒
        /// </summary>
        public string TimeUnit
        {
            set { _timeunit = value; }
            get { return _timeunit; }
        }
        /// <summary>
        /// 准备时间
        /// </summary>
        public int ReadyTime
        {
            set { _readytime = value; }
            get { return _readytime; }
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        public int RunTime
        {
            set { _runtime = value; }
            get { return _runtime; }
        }
        /// <summary>
        /// 是否计费0：否1：是
        /// </summary>
        public string IsCharge
        {
            set { _ischarge = value; }
            get { return _ischarge; }
        }
        /// <summary>
        /// 是否外协0：否1：是
        /// </summary>
        public string Isoutsource
        {
            set { _isoutsource = value; }
            get { return _isoutsource; }
        }
        /// <summary>
        /// 单位计时工资
        /// </summary>
        public decimal TimeWage
        {
            set { _timewage = value; }
            get { return _timewage; }
        }
        /// <summary>
        /// 单位计件工资
        /// </summary>
        public decimal PieceWage
        {
            set { _piecewage = value; }
            get { return _piecewage; }
        }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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

        /// <summary>
        /// 公司编码
        /// </summary>
        public string DetailCompanyCD
        {
            set { _detailcompanycd = value; }
            get { return _detailcompanycd; }
        }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string DetailSequNo
        {
            set { _detailsequno = value; }
            get { return _detailsequno; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 工艺ID
        /// </summary>
        public int DetailTechID
        {
            set { _detailtechid = value; }
            get { return _detailtechid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DetailRemark
        {
            set { _detailremark = value; }
            get { return _detailremark; }
        }
        #endregion Model
    }
}
