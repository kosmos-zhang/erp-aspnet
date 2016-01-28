
/**********************************************
 * 类作用   工作日志汇报明细表实体类层
 * 创建人   xz
 * 创建时间 2010-7-2 15:08:55 
 ***********************************************/

using System;

namespace XBase.Model.Personal.Note
{
    /// <summary>
    /// 工作日志汇报明细表实体类
    /// </summary>
    [Serializable]
    public class PersonalNoteReportModel
    {
        #region 字段

        private Nullable<int> iD = null; //流水号
        private string companyCD = String.Empty; //企业代码
        private Nullable<int> reportType = null; //汇报类别（0:任务;1:日程）
        private string noteNo = String.Empty; //日志编号
        private Nullable<int> reportID = null; //汇报流水号(任务流水号,日程流水号)
        private string reportNote = String.Empty; //汇报内容
        private Nullable<decimal> reportProgress = null; //任务完成进度(100为最大值)
        private Nullable<int> creator = null; //创建人ID
        private Nullable<DateTime> createDate = null; //创建时间
        
        #endregion
        

        #region 方法
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PersonalNoteReportModel ()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        ///<param name="iD">流水号</param>
        ///<param name="companyCD">企业代码</param>
        ///<param name="reportType">汇报类别（0:任务;1:日程）</param>
        ///<param name="noteNo">日志编号</param>
        ///<param name="reportID">汇报流水号(任务流水号,日程流水号)</param>
        ///<param name="reportNote">汇报内容</param>
        ///<param name="reportProgress">任务完成进度(100为最大值)</param>
        ///<param name="creator">创建人ID</param>
        ///<param name="createDate">创建时间</param>
        public PersonalNoteReportModel(
                    int iD,
                    string companyCD,
                    int reportType,
                    string noteNo,
                    int reportID,
                    string reportNote,
                    decimal reportProgress,
                    int creator,
                    Nullable<DateTime> createDate)
        {
            this.iD = iD; //流水号
            this.companyCD = companyCD; //企业代码
            this.reportType = reportType; //汇报类别（0:任务;1:日程）
            this.noteNo = noteNo; //日志编号
            this.reportID = reportID; //汇报流水号(任务流水号,日程流水号)
            this.reportNote = reportNote; //汇报内容
            this.reportProgress = reportProgress; //任务完成进度(100为最大值)
            this.creator = creator; //创建人ID
            this.createDate = createDate; //创建时间
        }
        
        #endregion
        
        
        #region 属性
        
        /// <summary>
        /// 流水号
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
        /// 企业代码
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
        /// 汇报类别（0:任务;1:日程）
        /// </summary>
        public Nullable<int> ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }
        
        /// <summary>
        /// 日志编号
        /// </summary>
        public string NoteNo
        {
            get
            {
                return noteNo;
            }
            set
            {
                noteNo = value;
            }
        }
        
        /// <summary>
        /// 汇报流水号(任务流水号,日程流水号)
        /// </summary>
        public Nullable<int> ReportID
        {
            get
            {
                return reportID;
            }
            set
            {
                reportID = value;
            }
        }
        
        /// <summary>
        /// 汇报内容
        /// </summary>
        public string ReportNote
        {
            get
            {
                return reportNote;
            }
            set
            {
                reportNote = value;
            }
        }
        
        /// <summary>
        /// 任务完成进度(100为最大值)
        /// </summary>
        public Nullable<decimal> ReportProgress
        {
            get
            {
                return reportProgress;
            }
            set
            {
                reportProgress = value;
            }
        }
        
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Nullable<int> Creator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
            }
        }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<DateTime> CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }
        }

        #endregion
    }
}


