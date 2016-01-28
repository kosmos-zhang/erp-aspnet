using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Common
{
    public class PrintParameterSettingModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _billtypeflag;
        private int _printtypeflag;
        private string _basefields;
        private string _detailfields;
        private string _detailsecondfields;
        private string _detailthreefields;
        private string _detailfourfields;
        private DateTime _modifieddate;
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
        public int BillTypeFlag
        {
            set { _billtypeflag = value; }
            get { return _billtypeflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PrintTypeFlag
        {
            set { _printtypeflag = value; }
            get { return _printtypeflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaseFields
        {
            set { _basefields = value; }
            get { return _basefields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailFields
        {
            set { _detailfields = value; }
            get { return _detailfields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailSecondFields
        {
            set { _detailsecondfields = value; }
            get { return _detailsecondfields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailThreeFields
        {
            set { _detailthreefields = value; }
            get { return _detailthreefields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailFourFields
        {
            set { _detailfourfields = value; }
            get { return _detailfourfields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        #endregion Model
    }
}
