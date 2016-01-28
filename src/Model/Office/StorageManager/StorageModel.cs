using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Model.Office.StorageManager
{
    /// <summary>
    /// 实体类StorageInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class StorageModel
    {
        public StorageModel()
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
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StorageNo
        {
            set { _storageno = value; }
            get { return _storageno; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName
        {
            set { _storagename = value; }
            get { return _storagename; }
        }
        /// <summary>
        /// 仓库类型
        /// </summary>
        public string StorageType
        {
            set { _storagetype = value; }
            get { return _storagetype; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanViewUser
        {
            get;
            set;
        
        }

        public string CanViewUserName
        {
            get;
            set;
         
        }

        /// <summary>
        /// 
        /// </summary>
        public string StorageAdmin
        {
            get;
            set;
         
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageAdminName
        {
            get;
            set;
        
        }
        #endregion Model

    }
}