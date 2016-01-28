using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
   public class BusiLogicSetModel
    {
        public BusiLogicSetModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _logictype;
        private int _logicid;
        private string _logicname;
        private string _logicset;
        private string _description;
        private string _usedstatus;
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
        public string LogicType
        {
            set { _logictype = value; }
            get { return _logictype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LogicID
        {
            set { _logicid = value; }
            get { return _logicid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogicName
        {
            set { _logicname = value; }
            get { return _logicname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogicSet
        {
            set { _logicset = value; }
            get { return _logicset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        #endregion Model
    }
}
