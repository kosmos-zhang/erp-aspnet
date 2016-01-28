/**********************************************
 * 类作用：   TrainingDetail表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/05
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingDetailModel
    /// 描述：TrainingDetail表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/05
    /// 最后修改时间：2009/04/05
    /// </summary>
    ///
    public class TrainingDetailModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _asseno;
        private string _employeeid;
        private string _assesslevel;
        private string _assessscore;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
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
        /// 考核ID
        /// </summary>
        public string AsseNo
        {
            set { _asseno = value; }
            get { return _asseno; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 考核等级
        /// </summary>
        public string AssessLevel
        {
            set { _assesslevel = value; }
            get { return _assesslevel; }
        }
        /// <summary>
        /// 考核分数
        /// </summary>
        public string AssessScore
        {
            set { _assessscore = value; }
            get { return _assessscore; }
        }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifiedDate
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
