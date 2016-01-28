using System;
using System.Collections.Generic;
using System.Text;

namespace XBase.Model.Personal.AimManager
{
    /// <summary>
    /// 类名：AimCheckInfoModel
    /// 描述：目标管理模块数据层方法
    /// 
    /// 作者：王乾睿
    /// 创建时间：2009.4.18
    /// 最后修改时间：2009.4.18
    /// </summary>
    ///
    public class AimCheckInfoModel
    {
        /// <summary>
        /// 实体类PlanCheck 。(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        #region Model
        private int _id;
        private string _companycd;
        private int? _aimid;
        private DateTime? _checkdate;
        private int? _checkor;
        private string _checkmethod;
        private string _checkcontent;
        private string _checkresult;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _addorcut;
        private string _editflag;
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
        public int? AimID
        {
            set { _aimid = value; }
            get { return _aimid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Checkor
        {
            set { _checkor = value; }
            get { return _checkor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckMethod
        {
            set { _checkmethod = value; }
            get { return _checkmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckContent
        {
            set { _checkcontent = value; }
            get { return _checkcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddOrCut
        {
            set { _addorcut = value; }
            get { return _addorcut; }
        }

        public string EditFlag
        {
            set { _editflag = value; }
            get { return _editflag; }
        }

        #endregion Model

    }
}
