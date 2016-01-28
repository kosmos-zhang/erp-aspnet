using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageQualityCheckReportDetailModel
    {
        public StorageQualityCheckReportDetailModel()
        { }

        private int _id;
        private string _companycd;
        private string _reportno;
        private int _sortno;
        private int _checkitem;
        private string _checkstandard;
        private string _checkvalue;
        private decimal _checknum;
        private decimal _passnum;
        private decimal _notpassnum;
        //private decimal _badnum;
        private string _standardvalue;
        private string _normuplimit;
        private string _lowerlimit;
        private string _checkresult;
        private string _ispass;
        private int _checker;
        private int _checkdeptid;
        private string _remark;
        private DateTime _modifieddate;
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
        /// 
        /// </summary>
        public string ReportNo
        {
            set { _reportno = value; }
            get { return _reportno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckItem
        {
            set { _checkitem = value; }
            get { return _checkitem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStandard
        {
            set { _checkstandard = value; }
            get { return _checkstandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckValue
        {
            set { _checkvalue = value; }
            get { return _checkvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheckNum
        {
            set { _checknum = value; }
            get { return _checknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PassNum
        {
            set { _passnum = value; }
            get { return _passnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal NotPassNum
        {
            set { _notpassnum = value; }
            get { return _notpassnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StandardValue
        {
            set { _standardvalue = value; }
            get { return _standardvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NormUpLimit
        {
            set { _normuplimit = value; }
            get { return _normuplimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LowerLimit
        {
            set { _lowerlimit = value; }
            get { return _lowerlimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckDeptID
        {
            set { _checkdeptid = value; }
            get { return _checkdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
    }
}
