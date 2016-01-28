using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Decision
{
    /// <summary>
    /// 实体类 DataMyCollector
    /// </summary>
    [Serializable]
    public class DataMyCollector
    {
        public DataMyCollector()
        { }

        /// <summary>
        /// 构造函数 DataMyCollector
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="dSID">DSID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="flag">Flag</param>
        /// <param name="reportDate">ReportDate</param>
        /// <param name="owner">Owner</param>
        /// <param name="reportTxt">ReportTxt</param>
        /// <param name="readStatus">ReadStatus</param>
        /// <param name="readDate">ReadDate</param>
        public DataMyCollector(int iD, int KeyWordID, int ActionID, int ActionDetailID, int Frequency, string companyCD, string flag, DateTime reportDate, string owner, string reportTxt, string readStatus, DateTime readDate)
        {
            _iD = iD;
            _keyWordID = KeyWordID;
            _actionID = ActionID;
            _actionDetailID = ActionDetailID;
            _frequency = Frequency;
            _companyCD = companyCD;
            _flag = flag;
            _reportDate = reportDate;
            _owner = owner;
            _reportTxt = reportTxt;
            _readStatus = readStatus;
            _readDate = readDate;
        }

        #region Model
        private int _iD;
        private int _keyWordID;
        private int _actionID;
        private int _actionDetailID;
        private int _frequency;
        private string _companyCD;
        private string _flag;
        private DateTime _reportDate;
        private string _owner;
        private string _reportTxt;
        private string _readStatus;
        private DateTime _readDate;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }
        /// <summary>
        /// KeyWordID
        /// </summary>
        public int KeyWordID
        {
            set { _keyWordID = value; }
            get { return _keyWordID; }
        }
        /// <summary>
        /// ActionID
        /// </summary>
        public int ActionID
        {
            set { _actionID = value; }
            get { return _actionID; }
        }
        /// <summary>
        /// KeyWordID
        /// </summary>
        public int ActionDetailID
        {
            set { _actionDetailID = value; }
            get { return _actionDetailID; }
        }
        /// <summary>
        /// KeyWordID
        /// </summary>
        public int Frequency
        {
            set { _frequency = value; }
            get { return _frequency; }
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
        /// Flag
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// ReportDate
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportDate = value; }
            get { return _reportDate; }
        }
        /// <summary>
        /// Owner
        /// </summary>
        public string Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }
        /// <summary>
        /// ReportTxt
        /// </summary>
        public string ReportTxt
        {
            set { _reportTxt = value; }
            get { return _reportTxt; }
        }
        /// <summary>
        /// ReadStatus
        /// </summary>
        public string ReadStatus
        {
            set { _readStatus = value; }
            get { return _readStatus; }
        }
        /// <summary>
        /// ReadDate
        /// </summary>
        public DateTime ReadDate
        {
            set { _readDate = value; }
            get { return _readDate; }
        }
        #endregion Model
    }
}
