using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class WorkCenterModel
    {
        #region Model
        private string _companycd;
        private int _id;
        private string _wcno;
        private string _wcname;
        private string _pyshort;
        private string _ismain;
        private int _deptid;
        private int _creator;
        private DateTime _createdate;
        private string _remark;
        private string _usedstatus;
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
        /// 工作中心编号
        /// </summary>
        public string WCNo
        {
            set { _wcno = value; }
            get { return _wcno; }
        }
        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string WCName
        {
            set { _wcname = value; }
            get { return _wcname; }
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
        /// 是否关键工作中心0：否1：是
        /// </summary>
        public string IsMain
        {
            set { _ismain = value; }
            get { return _ismain; }
        }
        /// <summary>
        /// 所属部门ID（对应部门表ID）
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 创建人ID（对应员工表ID）
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
        /// 启用状态
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model
    }
}
