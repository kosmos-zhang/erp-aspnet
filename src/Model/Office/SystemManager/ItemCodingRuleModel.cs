using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
   public class ItemCodingRuleModel
    {
        public ItemCodingRuleModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _codingtype;
        private int _itemtypeid;
        private string _rulename;
        private string _ruleprefix;
        private string _ruledatetype;
        private int _rulenolen;
        private int _lastno;
        private string _ruleexample;
        private string _isdefault;
        private string _remark;
        private string _usedstatus;
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
        public string CodingType
        {
            set { _codingtype = value; }
            get { return _codingtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ItemTypeID
        {
            set { _itemtypeid = value; }
            get { return _itemtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RuleName
        {
            set { _rulename = value; }
            get { return _rulename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RulePrefix
        {
            set { _ruleprefix = value; }
            get { return _ruleprefix; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RuleDateType
        {
            set { _ruledatetype = value; }
            get { return _ruledatetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RuleNoLen
        {
            set { _rulenolen = value; }
            get { return _rulenolen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LastNo
        {
            set { _lastno = value; }
            get { return _lastno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RuleExample
        {
            set { _ruleexample = value; }
            get { return _ruleexample; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
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
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
        #endregion Model
    }
}
