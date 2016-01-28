/**********************************************
 * 类作用：   TrainingUser表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/04/03
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：TrainingUserModel
    /// 描述：TrainingUser表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/03
    /// 最后修改时间：2009/04/03
    /// </summary>
    ///
    public class TrainingUserModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _trainingno;
        private string _flag;
        private string _joinid;
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
        /// 培训编号（对应培训表中的培训编号）
        /// </summary>
        public string TrainingNo
        {
            set { _trainingno = value; }
            get { return _trainingno; }
        }
        /// <summary>
        /// 区分(1 员工，2部门)
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 参加人ID
        /// </summary>
        public string JoinID
        {
            set { _joinid = value; }
            get { return _joinid; }
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
