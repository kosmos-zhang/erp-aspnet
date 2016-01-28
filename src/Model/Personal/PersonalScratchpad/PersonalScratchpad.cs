using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Personal.PersonalScratchpad
{
    public class PersonalScratchpad
    {
        public PersonalScratchpad()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private int _owner;
        private string _title;
        private string _content;
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
        public int Owner
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
        public string Content
        {
            set { _content = value; }
            get { return _content; }
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
