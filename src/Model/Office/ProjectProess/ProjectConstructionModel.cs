
/**********************************************
 * 类作用   施工摘要表实体类层
 * 创建人   xz
 * 创建时间 2010-5-21 16:45:40 
 ***********************************************/

using System;

namespace XBase.Model.Office.ProjectProess
{
    /// <summary>
    /// 施工摘要表实体类
    /// </summary>
    [Serializable]
    public class ProjectConstructionModel
    {
        #region 字段

        private Nullable<int> iD = null; //编号
        private string companyCD = String.Empty; //企业编码
        private Nullable<int> projectID = null; //项目编号
        private string summaryName = String.Empty; //概要名称
        private string userlist = String.Empty; //可查看人员代码ID
        private string userNamelist = String.Empty; //可查看人员姓名

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ProjectConstructionModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">编号</param>
        ///<param name="companyCD">企业编码</param>
        ///<param name="projectID">项目编号</param>
        ///<param name="summaryName">概要名称</param>
        ///<param name="userlist">可查看人员代码ID</param>
        ///<param name="userNamelist">可查看人员姓名</param>
        public ProjectConstructionModel(
                    int iD,
                    string companyCD,
                    int projectID,
                    string summaryName,
                    string userlist,
                    string userNamelist)
        {
            this.iD = iD; //编号
            this.companyCD = companyCD; //企业编码
            this.projectID = projectID; //项目编号
            this.summaryName = summaryName; //概要名称
            this.userlist = userlist; //可查看人员代码ID
            this.userNamelist = userNamelist; //可查看人员姓名
        }

        #endregion


        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        public Nullable<int> ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return companyCD;
            }
            set
            {
                companyCD = value;
            }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        public Nullable<int> ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }

        /// <summary>
        /// 概要名称
        /// </summary>
        public string SummaryName
        {
            get
            {
                return summaryName;
            }
            set
            {
                summaryName = value;
            }
        }

        /// <summary>
        /// 可查看人员代码ID
        /// </summary>
        public string Userlist
        {
            get
            {
                return userlist;
            }
            set
            {
                userlist = value;
            }
        }

        /// <summary>
        /// 可查看人员姓名
        /// </summary>
        public string UserNamelist
        {
            get
            {
                return userNamelist;
            }
            set
            {
                userNamelist = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// 施工摘要明细表实体类
    /// </summary>
    [Serializable]
    public class ProjectConstructionDetailsModel
    {
        #region 字段

        private Nullable<int> iD = null; //概要编号
        private string companyCD = String.Empty; //企业编码
        private Nullable<int> summaryID = null; //概要编号
        private Nullable<int> projectID = null; //项目编号
        private Nullable<decimal> processScale = null; //工艺定额
        private Nullable<decimal> personNum = null; //人工总量
        private Nullable<decimal> rate = null; //完成比例
        private string proessID = String.Empty; //受约进度编号(多编号用”,”隔开)
        private Nullable<DateTime> beginDate = null; //开始时间
        private Nullable<DateTime> endDate = null; //结束时间
        private string projectMemo = String.Empty; //工程备注

        #endregion


        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ProjectConstructionDetailsModel()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">概要编号</param>
        ///<param name="companyCD">企业编码</param>
        ///<param name="summaryID">概要编号</param>
        ///<param name="projectID">项目编号</param>
        ///<param name="processScale">工艺定额</param>
        ///<param name="personNum">人工总量</param>
        ///<param name="rate">完成比例</param>
        ///<param name="proessID">受约进度编号(多编号用”,”隔开)</param>
        ///<param name="beginDate">开始时间</param>
        ///<param name="endDate">结束时间</param>
        ///<param name="projectMemo">工程备注</param>
        public ProjectConstructionDetailsModel(
                    int iD,
                    string companyCD,
                    int summaryID,
                    int projectID,
                    decimal processScale,
                    decimal personNum,
                    decimal rate,
                    string proessID,
                    Nullable<DateTime> beginDate,
                    Nullable<DateTime> endDate,
                    string projectMemo)
        {
            this.iD = iD; //概要编号
            this.companyCD = companyCD; //企业编码
            this.summaryID = summaryID; //概要编号
            this.projectID = projectID; //项目编号
            this.processScale = processScale; //工艺定额
            this.personNum = personNum; //人工总量
            this.rate = rate; //完成比例
            this.proessID = proessID; //受约进度编号(多编号用”,”隔开)
            this.beginDate = beginDate; //开始时间
            this.endDate = endDate; //结束时间
            this.projectMemo = projectMemo; //工程备注
        }

        #endregion


        #region 属性

        /// <summary>
        /// 概要编号
        /// </summary>
        public Nullable<int> ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            get
            {
                return companyCD;
            }
            set
            {
                companyCD = value;
            }
        }

        /// <summary>
        /// 概要编号
        /// </summary>
        public Nullable<int> SummaryID
        {
            get
            {
                return summaryID;
            }
            set
            {
                summaryID = value;
            }
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        public Nullable<int> ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }

        /// <summary>
        /// 工艺定额
        /// </summary>
        public Nullable<decimal> ProcessScale
        {
            get
            {
                return processScale;
            }
            set
            {
                processScale = value;
            }
        }

        /// <summary>
        /// 人工总量
        /// </summary>
        public Nullable<decimal> PersonNum
        {
            get
            {
                return personNum;
            }
            set
            {
                personNum = value;
            }
        }

        /// <summary>
        /// 完成比例
        /// </summary>
        public Nullable<decimal> Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
            }
        }

        /// <summary>
        /// 受约进度编号(多编号用”,”隔开)
        /// </summary>
        public string ProessID
        {
            get
            {
                return proessID;
            }
            set
            {
                proessID = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public Nullable<DateTime> BeginDate
        {
            get
            {
                return beginDate;
            }
            set
            {
                beginDate = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public Nullable<DateTime> EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        /// <summary>
        /// 工程备注
        /// </summary>
        public string ProjectMemo
        {
            get
            {
                return projectMemo;
            }
            set
            {
                projectMemo = value;
            }
        }

        #endregion
    }
}
