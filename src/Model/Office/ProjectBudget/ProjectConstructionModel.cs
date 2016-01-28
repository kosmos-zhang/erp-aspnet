
/**********************************************
 * 类作用   施工摘要表实体类层
 * 创建人   xz
 * 创建时间 2010-5-19 9:42:20 
 ***********************************************/

using System;

namespace XBase.Model.Office.ProjectBudget
{
    /// <summary>
    /// 施工摘要表实体类
    /// </summary>
    [Serializable]
    public class ProjectConstructionModel
    {
        #region 字段

        private Nullable<int> summaryID = null; //概要编号
        private string companyCD = String.Empty; //企业编码
        private string summaryName = String.Empty; //概要名称
        private Nullable<int> structID = null; //所属工程概要编号
        private Nullable<decimal> processScale = null; //工艺定额
        private Nullable<int> keyPress = null; //是否为受约进度(0:不是，1是)
        private string proessID = String.Empty; //受约进度编号(多编号用”,”隔开)
        private Nullable<DateTime> beginDate = null; //开始时间
        private Nullable<DateTime> endDate = null; //结束时间
        private Nullable<int> startFlag = null; //是否执行(0:不执行，1执行)
        private Nullable<int> gmanagerID = null; //工程负责人
        private string projectMemo = String.Empty; //工程备注
        
        #endregion
        

        #region 方法
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ProjectConstructionModel ()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="summaryID">概要编号</param>
        ///<param name="companyCD">企业编码</param>
        ///<param name="summaryName">概要名称</param>
        ///<param name="structID">所属工程概要编号</param>
        ///<param name="processScale">工艺定额</param>
        ///<param name="keyPress">是否为受约进度(0:不是，1是)</param>
        ///<param name="proessID">受约进度编号(多编号用”,”隔开)</param>
        ///<param name="beginDate">开始时间</param>
        ///<param name="endDate">结束时间</param>
        ///<param name="startFlag">是否执行(0:不执行，1执行)</param>
        ///<param name="gmanagerID">工程负责人</param>
        ///<param name="projectMemo">工程备注</param>
        public ProjectConstructionModel(
                    int summaryID,
                    string companyCD,
                    string summaryName,
                    int structID,
                    decimal processScale,
                    int keyPress,
                    string proessID,
                    Nullable<DateTime> beginDate,
                    Nullable<DateTime> endDate,
                    int startFlag,
                    int gmanagerID,
                    string projectMemo)
        {
            this.summaryID = summaryID; //概要编号
            this.companyCD = companyCD; //企业编码
            this.summaryName = summaryName; //概要名称
            this.structID = structID; //所属工程概要编号
            this.processScale = processScale; //工艺定额
            this.keyPress = keyPress; //是否为受约进度(0:不是，1是)
            this.proessID = proessID; //受约进度编号(多编号用”,”隔开)
            this.beginDate = beginDate; //开始时间
            this.endDate = endDate; //结束时间
            this.startFlag = startFlag; //是否执行(0:不执行，1执行)
            this.gmanagerID = gmanagerID; //工程负责人
            this.projectMemo = projectMemo; //工程备注
        }
        
        #endregion
        
        
        #region 属性
        
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
        /// 所属工程概要编号
        /// </summary>
        public Nullable<int> StructID
        {
            get
            {
                return structID;
            }
            set
            {
                structID = value;
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
        /// 是否为受约进度(0:不是，1是)
        /// </summary>
        public Nullable<int> KeyPress
        {
            get
            {
                return keyPress;
            }
            set
            {
                keyPress = value;
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
        /// 是否执行(0:不执行，1执行)
        /// </summary>
        public Nullable<int> StartFlag
        {
            get
            {
                return startFlag;
            }
            set
            {
                startFlag = value;
            }
        }
        
        /// <summary>
        /// 工程负责人
        /// </summary>
        public Nullable<int> GmanagerID
        {
            get
            {
                return gmanagerID;
            }
            set
            {
                gmanagerID = value;
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


