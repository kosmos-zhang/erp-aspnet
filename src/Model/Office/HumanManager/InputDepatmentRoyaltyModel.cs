using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.HumanManager
{
    public class InputDepatmentRoyaltyModel
    {
        public InputDepatmentRoyaltyModel()
        { }
        #region Model
        private string _id;
        private string _deptid;
        private string _companycd;
        private string _businessmoney;
        private string _ratepercent;
        private string _modifieduserid;
        private string _createtime;
        private string _modifieddate;
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
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
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
        public string BusinessMoney
        {
            set { _businessmoney = value; }
            get { return _businessmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RatePercent
        {
            set { _ratepercent = value; }
            get { return _ratepercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        #endregion Model

    }
}
