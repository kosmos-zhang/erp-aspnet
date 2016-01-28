using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StorageMonthlyModel
    {
        public StorageMonthlyModel()
        { }
        #region Model
        private string _id;
        private string _companycd;
        private string _monthno;
        private string _startdate;
        private string _enddate;
        private string _deptid;
        private string _storageno;
        private string _storageid;
        private string _productid;
        private string _oldrealcost;
        private string _nowrealcost;
        private string _oldcount;
        private string _oldtotal;
        private string _nowcount;
        private string _nowtotal;
        private string _remark;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 
        /// </summary>
        public string ID
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
        public string MonthNo
        {
            set { _monthno = value; }
            get { return _monthno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageNo
        {
            set { _storageno = value; }
            get { return _storageno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldRealCost
        {
            set { _oldrealcost = value; }
            get { return _oldrealcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NowRealCost
        {
            set { _nowrealcost = value; }
            get { return _nowrealcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldCount
        {
            set { _oldcount = value; }
            get { return _oldcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldTotal
        {
            set { _oldtotal = value; }
            get { return _oldtotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NowCount
        {
            set { _nowcount = value; }
            get { return _nowcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NowTotal
        {
            set { _nowtotal = value; }
            get { return _nowtotal; }
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
        public string ModifiedDate
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
        #endregion Model

    }
}
