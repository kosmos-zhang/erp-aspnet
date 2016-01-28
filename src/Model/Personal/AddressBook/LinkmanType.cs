using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.AddressBook
{
    public class LinkmanType
    {
        public LinkmanType()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private int  _owner;
        private string _title;
        private string _remark;
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
        public int  Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
