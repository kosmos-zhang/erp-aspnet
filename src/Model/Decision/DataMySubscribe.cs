using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Decision
{
    /// <summary>
    /// 实体类 DataMySubscribe
    /// </summary>
    [Serializable]
    public class DataMySubscribe
    {
        public DataMySubscribe()
        { }

        /// <summary>
        /// 构造函数 DataMySubscribe
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="dataID">DataID</param>
        /// <param name="dataName">DataName</param>
        /// <param name="dataVarValue">DataVarValue</param>
        /// <param name="conditions">Conditions</param>
        /// <param name="myMobile">MyMobile</param>
        /// <param name="creator">Creator</param>
        /// <param name="createDate">CreateDate</param>
        public DataMySubscribe(int iD, string companyCD, string dataID, string dataName, string dataVarValue, string conditions, string myMobile, string creator, DateTime createDate)
        {
            _iD = iD;
            _companyCD = companyCD;
            _dataID = dataID;
            _dataName = dataName;
            _dataVarValue = dataVarValue;
            _conditions = conditions;
            _myMobile = myMobile;
            _creator = creator;
            _createDate = createDate;
        }

        #region Model
        private int _iD;
        private string _companyCD;
        private string _dataID;
        private string _dataName;
        private string _dataVarValue;
        private string _conditions;
        private string _myMobile;
        private string _creator;
        private DateTime _createDate;
        private string _datanote;
        private string _frequency;
        private string _format;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }
        /// <summary>
        /// CompanyCD
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// DataID
        /// </summary>
        public string DataID
        {
            set { _dataID = value; }
            get { return _dataID; }
        }
        /// <summary>
        /// DataName
        /// </summary>
        public string DataName
        {
            set { _dataName = value; }
            get { return _dataName; }
        }
        /// <summary>
        /// DataVarValue
        /// </summary>
        public string DataVarValue
        {
            set { _dataVarValue = value; }
            get { return _dataVarValue; }
        }
        /// <summary>
        /// Conditions
        /// </summary>
        public string Conditions
        {
            set { _conditions = value; }
            get { return _conditions; }
        }
        /// <summary>
        /// MyMobile
        /// </summary>
        public string MyMobile
        {
            set { _myMobile = value; }
            get { return _myMobile; }
        }
        /// <summary>
        /// Creator
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }
        public string DataNote
        {
            set { _datanote = value; }
            get { return _datanote; }
        }
        public string Frequency
        {
            set { _frequency = value; }
            get { return _frequency; }
        }
        public string Format
        {
            set { _format = value; }
            get { return _format; }
        }
        #endregion Model
    }
}
