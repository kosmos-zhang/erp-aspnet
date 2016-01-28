/**********************************************
 * 类作用：   RectGoal表数据模板
 * 建立人：   吴志强
 * 建立时间： 2009/03/30
 ***********************************************/
using System;

namespace XBase.Model.Office.HumanManager
{
    /// <summary>
    /// 类名：RectGoalModel
    /// 描述：RectGoal表数据模板
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/30
    /// 最后修改时间：2009/03/30
    /// </summary>
    ///
    public class RectGoalModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _planno;
        private string _applydept;
        private string _positiontitle;
        private string _personcount;
        private string _sex;
        private string _age;
        private string _culturelevel;
        private string _professional;
        private string _requisition;
        private string _completedate;
        private string _modifieddate;
        private string _modifieduserid;
        private string _workAge;
        private string _positionID;
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PositionID
        {
            set { _positionID = value; }
            get { return _positionID; }
        }   
        /// <summary>
        /// 工作年限
        /// </summary>
        public string WorkAge
        {
            set { _workAge = value; }
            get { return _workAge; }
        }
        /// <summary>
        /// 内部id，自动生成
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 招聘计划编号
        /// </summary>
        public string PlanNo
        {
            set { _planno = value; }
            get { return _planno; }
        }
        /// <summary>
        /// 申请部门(对应部门表ID)
        /// </summary>
        public string ApplyDept
        {
            set { _applydept = value; }
            get { return _applydept; }
        }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string PositionTitle
        {
            set { _positiontitle = value; }
            get { return _positiontitle; }
        }
        /// <summary>
        /// 人员数量
        /// </summary>
        public string PersonCount
        {
            set { _personcount = value; }
            get { return _personcount; }
        }
        /// <summary>
        /// 性别(1 男，2 女，3不限)
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 年龄要求
        /// </summary>
        public string Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 学历ID(对应分类代码表ID)
        /// </summary>
        public string CultureLevel
        {
            set { _culturelevel = value; }
            get { return _culturelevel; }
        }
        /// <summary>
        /// 专业ID(对应分类代码表ID)
        /// </summary>
        public string Professional
        {
            set { _professional = value; }
            get { return _professional; }
        }
        /// <summary>
        /// 要求
        /// </summary>
        public string Requisition
        {
            set { _requisition = value; }
            get { return _requisition; }
        }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public string CompleteDate
        {
            set { _completedate = value; }
            get { return _completedate; }
        }
        /// <summary>
        /// 最后更新时间
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
