/**********************************************
 * 类作用：   EmployeeTestScore表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：EmployeeTestScoreModel
    /// 描述：EmployeeTestScore表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    public class EmployeeTestScoreModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _testno;
        private string _employeeid;
        private string _testscore;
        private string _flag;
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
        /// 考试编号
        /// </summary>
        public string TestNo
        {
            set { _testno = value; }
            get { return _testno; }
        }
        /// <summary>
        /// 考试人ID(对应员工表ID)
        /// </summary>
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 考试成绩
        /// </summary>
        public string TestScore
        {
            set { _testscore = value; }
            get { return _testscore; }
        }
        /// <summary>
        /// 是否参加考试标识(0 否，1是)

        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
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
