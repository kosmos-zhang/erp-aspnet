using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Model.Office.StorageManager
{
    /// <summary>
    /// 实体类StorageInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class StorageInfo
    {
        public StorageInfo()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _storageno;
        private string _storagename;
        private string _storagetype;
        private string _usedstatus;
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
        public string StorageNo
        {
            set { _storageno = value; }
            get { return _storageno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageName
        {
            set { _storagename = value; }
            get { return _storagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageType
        {
            set { _storagetype = value; }
            get { return _storagetype; }
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }



        #endregion Model

    }
}