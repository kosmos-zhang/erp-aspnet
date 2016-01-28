using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Decision
{
    /// <summary>
    /// 实体类 DataKeyPrepare
    /// </summary>
    [Serializable]
    public class DataKeyPrepare
    {
        public DataKeyPrepare()
        { }

        /// <summary>
        /// 构造函数 DataKeyPrepare
        /// </summary>
        /// <param name="dataID">DataID</param>
        /// <param name="dataName">DataName</param>
        /// <param name="dataVar">DataVar</param>
        /// <param name="dataVal">DataVal</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="dataSrc">DataSrc</param>
        /// <param name="isCond">IsCond</param>
        public DataKeyPrepare(int dataID, string dataName, string dataVar, string dataVal, string attachment, string dataSrc, string isCond)
        {
            _dataID = dataID;
            _dataName = dataName;
            _dataVar = dataVar;
            _dataVal = dataVal;
            _attachment = attachment;
            _dataSrc = dataSrc;
            _isCond = isCond;
        }

        #region Model
        private int _dataID;
        private string _dataName;
        private string _dataVar;
        private string _dataVal;
        private string _attachment;
        private string _dataSrc;
        private string _isCond;
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
        /// DataVar
        /// </summary>
        public string DataVar
        {
            set { _dataVar = value; }
            get { return _dataVar; }
        }
        /// <summary>
        /// DataVal
        /// </summary>
        public string DataVal
        {
            set { _dataVal = value; }
            get { return _dataVal; }
        }
        /// <summary>
        /// Attachment
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// DataSrc
        /// </summary>
        public string DataSrc
        {
            set { _dataSrc = value; }
            get { return _dataSrc; }
        }
        /// <summary>
        /// IsCond
        /// </summary>
        public string IsCond
        {
            set { _isCond = value; }
            get { return _isCond; }
        }
        #endregion Model
    }
}
