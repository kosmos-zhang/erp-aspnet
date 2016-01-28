using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
   public class CodeEquipmentTypeModel
    {
        public CodeEquipmentTypeModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _typeflag;
        private string _codename;
        private int _supperid;
        private string _description;
        private int _warninglimit;
        private string _usedstatus;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private string _BigType;
        public string BigType
        {
            set { _BigType = value; }
            get { return _BigType; }
        }
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 分类标识
        /// </summary>
        public string TypeFlag
        {
            set { _typeflag = value; }
            get { return _typeflag; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CodeName
        {
            set { _codename = value; }
            get { return _codename; }
        }
        /// <summary>
        /// 上级类别ID
        /// </summary>
        public int SupperID
        {
            set { _supperid = value; }
            get { return _supperid; }
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 报警下限
        /// </summary>
        public int WarningLimit
        {
            set { _warninglimit = value; }
            get { return _warninglimit; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
