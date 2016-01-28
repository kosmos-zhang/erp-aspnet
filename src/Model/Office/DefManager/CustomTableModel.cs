using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.DefManager
{
    [Serializable]
    public class CustomTableModel
    {
        
        #region Model
        private int _id;
        private string _companycd;
        private string _customtablename;
        private string _aliastablename;
        private int _parentid;
        private int _columnnumber;

        private int _isdic;

        private int _totalFlag;


        /// <summary>
        /// 
        /// </summary>
        public int totalFlag
        {
            set { _totalFlag = value; }
            get { return _totalFlag; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IsDic
        {
            set { _isdic = value; }
            get { return _isdic; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ColumnNumber
        {
            set { _columnnumber = value; }
            get { return _columnnumber; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }

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
        public string CustomTableName
        {
            set { _customtablename = value; }
            get { return _customtablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AliasTableName
        {
            set { _aliastablename = value; }
            get { return _aliastablename; }
        }
        #endregion Model
    }
}
