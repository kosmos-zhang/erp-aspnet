using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
   public class CompanyBaseInfoModel
    {
        #region Model
        private string _companycd;
        private string _companyno;
        private int? _supercompanyid;
        private string _companyname;
        private string _usedstatus;
        private string _description;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyNo
        {
            set { _companyno = value; }
            get { return _companyno; }
        }
        /// <summary>
        /// 上级公司
        /// </summary>
        public int? SuperCompanyID
        {
            set { _supercompanyid = value; }
            get { return _supercompanyid; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
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
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
