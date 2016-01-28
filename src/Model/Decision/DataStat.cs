using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Decision
{
    /// <summary>
    /// 实体类 DataStat
    /// </summary>
    [Serializable]
    public class DataStat
    {
        public DataStat()
        { }

        /// <summary>
        /// 构造函数 DataStat
        /// </summary>
        /// <param name="iD">ID</param>
        /// <param name="companyCD">CompanyCD</param>
        /// <param name="dataID">DataID</param>
        /// <param name="dataName">DataName</param>
        /// <param name="dataVarValue">DataVarValue</param>
        /// <param name="dataNum">DataNum</param>
        /// <param name="statType">StatType</param>
        /// <param name="dataDateim">DataDateim</param>
        public DataStat(int iD, string companyCD, int dataID, string dataName, string dataVarValue, decimal dataNum, string statType, DateTime dataDateim)
        {
            _iD = iD;
            _companyCD = companyCD;
            _dataID = dataID;
            _dataName = dataName;
            _dataVarValue = dataVarValue;
            _dataNum = dataNum;
            _statType = statType;
            _dataDateim = dataDateim;
        }

        #region Model
        private int _iD;
        private string _companyCD;
        private int _dataID;
        private string _dataName;
        private string _dataVarValue;
        private decimal _dataNum;
        private string _statType;
        private DateTime _dataDateim;
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
        public int DataID
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
        /// DataNum
        /// </summary>
        public decimal DataNum
        {
            set { _dataNum = value; }
            get { return _dataNum; }
        }
        /// <summary>
        /// StatType
        /// </summary>
        public string StatType
        {
            set { _statType = value; }
            get { return _statType; }
        }
        /// <summary>
        /// DataDateim
        /// </summary>
        public DateTime DataDateim
        {
            set { _dataDateim = value; }
            get { return _dataDateim; }
        }
        #endregion Model
    }
}
