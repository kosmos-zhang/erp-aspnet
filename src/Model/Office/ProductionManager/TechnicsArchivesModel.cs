using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class TechnicsArchivesModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _techno;
        private string _pyshort;
        private string _techname;
        private string _description;
        private string _usedstatus;
        private int _creator;
        private DateTime _createdate;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 工艺代码
        /// </summary>
        public string TechNo
        {
            set { _techno = value; }
            get { return _techno; }
        }
        /// <summary>
        /// 拼音缩写
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string TechName
        {
            set { _techname = value; }
            get { return _techname; }
        }
        /// <summary>
        /// 工艺描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
        /// 创建人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
