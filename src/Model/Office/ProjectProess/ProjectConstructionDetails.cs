
/**********************************************
 * 类作用   施工摘要表实体类层
 * 创建人   zxb
 * 创建时间 2010-5-21 16:45:40 
 ***********************************************/

using System;

namespace XBase.Model.Office.ProjectProess
{
    /// <summary>
    /// 施工摘要表实体类
    /// </summary>
    [Serializable]
    public class ProjectConstructionDetails
    {
        public ProjectConstructionDetails()
        { }

        /// <summary>
        /// 构造函数 ProjectConstructionDetails
        /// </summary>
        /// <param name="iD">概要编号</param>
        /// <param name="summaryName">SummaryName</param>
        /// <param name="dutyPerson">DutyPerson</param>
        /// <param name="companyCD">企业编码</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="processScale">工艺定额</param>
        /// <param name="personNum">人工总量</param>
        /// <param name="rate">完成比例</param>
        /// <param name="proessID">受约进度编号(多编号用”,”隔开)</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="projectMemo">工程备注</param>
        public ProjectConstructionDetails(int iD, string summaryName, int dutyPerson, string companyCD, int projectID, decimal processScale, decimal personNum, decimal rate, string proessID, DateTime beginDate, DateTime endDate, string projectMemo)
        {
            _iD = iD;
            _summaryName = summaryName;
            _dutyPerson = dutyPerson;
            _companyCD = companyCD;
            _projectID = projectID;
            _processScale = processScale;
            _personNum = personNum;
            _rate = rate;
            _proessID = proessID;
            _beginDate = beginDate;
            _endDate = endDate;
            _projectMemo = projectMemo;
        }
        public ProjectConstructionDetails(string summaryName, int dutyPerson, string companyCD, int projectID, decimal processScale, decimal personNum, decimal rate, string proessID, DateTime beginDate, DateTime endDate, string projectMemo)
        {
            _summaryName = summaryName;
            _dutyPerson = dutyPerson;
            _companyCD = companyCD;
            _projectID = projectID;
            _processScale = processScale;
            _personNum = personNum;
            _rate = rate;
            _proessID = proessID;
            _beginDate = beginDate;
            _endDate = endDate;
            _projectMemo = projectMemo;
        }

        #region Model
        private int _iD;
        private string _summaryName;
        private int _dutyPerson;
        private string _companyCD;
        private int _projectID;
        private decimal _processScale;
        private decimal _personNum;
        private decimal _rate;
        private string _proessID;
        private DateTime _beginDate;
        private DateTime _endDate;
        private string _projectMemo;
        /// <summary>
        /// 概要编号
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }
        /// <summary>
        /// SummaryName
        /// </summary>
        public string SummaryName
        {
            set { _summaryName = value; }
            get { return _summaryName; }
        }
        /// <summary>
        /// DutyPerson
        /// </summary>
        public int DutyPerson
        {
            set { _dutyPerson = value; }
            get { return _dutyPerson; }
        }
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public int projectID
        {
            set { _projectID = value; }
            get { return _projectID; }
        }
        /// <summary>
        /// 工艺定额
        /// </summary>
        public decimal ProcessScale
        {
            set { _processScale = value; }
            get { return _processScale; }
        }
        /// <summary>
        /// 人工总量
        /// </summary>
        public decimal PersonNum
        {
            set { _personNum = value; }
            get { return _personNum; }
        }
        /// <summary>
        /// 完成比例
        /// </summary>
        public decimal Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }
        /// <summary>
        /// 受约进度编号(多编号用”,”隔开)
        /// </summary>
        public string ProessID
        {
            set { _proessID = value; }
            get { return _proessID; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate
        {
            set { _beginDate = value; }
            get { return _beginDate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }
        /// <summary>
        /// 工程备注
        /// </summary>
        public string ProjectMemo
        {
            set { _projectMemo = value; }
            get { return _projectMemo; }
        }
        #endregion Model
    }
}
