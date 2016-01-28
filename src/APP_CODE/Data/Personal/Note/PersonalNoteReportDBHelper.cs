

/**********************************************
 * 类作用   工作日志汇报明细表数据处理层
 * 创建人   xz
 * 创建时间 2010-7-2 15:08:55 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.Personal.Note;


namespace XBase.Data.Personal.Note
{
    /// <summary>
    /// 工作日志汇报明细表数据处理类
    /// </summary>
    public class PersonalNoteReportDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                @" SELECT ID,CompanyCD,ReportType,NoteNo,ReportID,ReportNote,ReportProgress,Creator,CreateDate
                                   FROM officedba.PersonalNoteReport";
        private const string C_SELECT_ID =
                               @" SELECT ID,CompanyCD,ReportType,NoteNo,ReportID,ReportNote,ReportProgress,Creator,CreateDate
                                  FROM officedba.PersonalNoteReport
                                  WHERE ID =@ID";
        private const string C_SELECT =
                               @" SELECT ID,CompanyCD,ReportType,NoteNo,ReportID,ReportNote,ReportProgress,Creator,CreateDate
                                  FROM officedba.PersonalNoteReport
                                  WHERE NoteNo=@NoteNo  AND ReportID=@ReportID ";
        private const string C_INSERT =
                               @" INSERT officedba.PersonalNoteReport(
                                     CompanyCD,ReportType,NoteNo,ReportID,ReportNote,ReportProgress,Creator,CreateDate )
                                  VALUES (
                                     @CompanyCD,@ReportType,@NoteNo,@ReportID,@ReportNote,@ReportProgress,@Creator,@CreateDate )";
        private const string C_UPDATE =
                               @" UPDATE officedba.PersonalNoteReport SET
                                     CompanyCD=@CompanyCD,ReportType=@ReportType,NoteNo=@NoteNo,ReportID=@ReportID
                                     ,ReportNote=@ReportNote,ReportProgress=@ReportProgress,Creator=@Creator,CreateDate=@CreateDate
                                  WHERE ID=@ID";
        private const string C_DELETE =
                               @" DELETE FROM officedba.PersonalNoteReport WHERE NoteNo=@NoteNo  AND ReportID=@ReportID";

        private const string C_DELETE_ID =
                               @" DELETE FROM officedba.PersonalNoteReport WHERE ID IN ({0}) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 流水号列
        private const byte m_companyCDCol = 1; // 企业代码列
        private const byte m_reportTypeCol = 2; // 汇报类别（0:任务;1:日程）列
        private const byte m_noteNoCol = 3; // 日志编号列
        private const byte m_reportIDCol = 4; // 汇报流水号(任务流水号,日程流水号)列
        private const byte m_reportNoteCol = 5; // 汇报内容列
        private const byte m_reportProgressCol = 6; // 任务完成进度(100为最大值)列
        private const byte m_creatorCol = 7; // 创建人ID列
        private const byte m_createDateCol = 8; // 创建时间列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">流水号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 流水号

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="noteNo">日志编号</param>
        /// <param name="reportID">汇报流水号(任务流水号,日程流水号)</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string noteNo, int reportID)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, noteNo, reportID);

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , PersonalNoteReportModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(C_SELECT_ALL);
            sql.Append(" WHERE 1=1 ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }

        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(PersonalNoteReportModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_INSERT + " SET @IndexID = @@IDENTITY ";
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(PersonalNoteReportModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_UPDATE;
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(PersonalNoteReportModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_UPDATE);

            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">流水号集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = String.Format(C_DELETE_ID, iD); // 流水号集合

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">流水号集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder();

            sqlSentence.AppendFormat(C_DELETE_ID, iD);// 流水号集合

            SqlParameter[] parameters = new SqlParameter[] { };

            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="noteNo">日志编号</param>
        /// <param name="reportID">汇报流水号(任务流水号,日程流水号)</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(string noteNo, int reportID)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, noteNo, reportID);

            comm.Parameters.AddRange(parameters);

            return comm;
        }



        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="noteNo">日志编号</param>
        /// <param name="reportID">汇报流水号(任务流水号,日程流水号)</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(string noteNo, int reportID)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, noteNo, reportID);


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }


        /// <summary>
        /// 设置查询和删除的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@NoteNo", SqlDbType.VarChar, 50), // 日志编号
							new SqlParameter("@ReportID", SqlDbType.Int, 4) // 汇报流水号(任务流水号,日程流水号)
                        };

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int,4), // 流水号
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8), // 企业代码
                            new SqlParameter("@ReportType", SqlDbType.Int,4), // 汇报类别（0:任务;1:日程）
                            new SqlParameter("@NoteNo", SqlDbType.VarChar,50), // 日志编号
                            new SqlParameter("@ReportID", SqlDbType.Int,4), // 汇报流水号(任务流水号,日程流水号)
                            new SqlParameter("@ReportNote", SqlDbType.Text), // 汇报内容
                            new SqlParameter("@ReportProgress", SqlDbType.Decimal,9), // 任务完成进度(100为最大值)
                            new SqlParameter("@Creator", SqlDbType.Int,4), // 创建人ID
                            new SqlParameter("@CreateDate", SqlDbType.DateTime,8)  // 创建时间
                        };

            return parameters;
        }



        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="noteNo">日志编号的值</param>
        /// <param name="reportID">汇报流水号(任务流水号,日程流水号)的值</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, string noteNo, int reportID)
        {
            parameters[0].Value = noteNo; // 日志编号
            parameters[1].Value = reportID; // 汇报流水号(任务流水号,日程流水号)

            return parameters;
        }



        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, PersonalNoteReportModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 流水号
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业代码
            if (!model.ReportType.HasValue) parameters[m_reportTypeCol].Value = System.DBNull.Value; else parameters[m_reportTypeCol].Value = model.ReportType; // 汇报类别（0:任务;1:日程）
            parameters[m_noteNoCol].Value = model.NoteNo; // 日志编号
            if (!model.ReportID.HasValue) parameters[m_reportIDCol].Value = System.DBNull.Value; else parameters[m_reportIDCol].Value = model.ReportID; // 汇报流水号(任务流水号,日程流水号)
            parameters[m_reportNoteCol].Value = model.ReportNote; // 汇报内容
            if (!model.ReportProgress.HasValue) parameters[m_reportProgressCol].Value = System.DBNull.Value; else parameters[m_reportProgressCol].Value = model.ReportProgress; // 任务完成进度(100为最大值)
            if (!model.Creator.HasValue) parameters[m_creatorCol].Value = System.DBNull.Value; else parameters[m_creatorCol].Value = model.Creator; // 创建人ID
            if (!model.CreateDate.HasValue) parameters[m_createDateCol].Value = System.DBNull.Value; else parameters[m_createDateCol].Value = model.CreateDate; // 创建时间

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 根据日志编号查询数据记录
        /// </summary>
        /// <param name="noteNo">企业代码</param>
        /// <param name="reportID">日志编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithNoteNo(string CompanyCD, string noteNo)
        {
            // SQL语句
            string strSql = @" SELECT ID,CompanyCD,ReportType,NoteNo,ReportID,ReportNote,ReportProgress,Creator,CreateDate
                                FROM officedba.PersonalNoteReport
                                WHERE CompanyCD=@CompanyCD AND NoteNo=@NoteNo ";
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@NoteNo", SqlDbType.VarChar, 50), // 日志编号
							new SqlParameter("@CompanyCD", SqlDbType.VarChar, 8) // 企业代码
                        };
            parameters[0].Value = noteNo;
            parameters[1].Value = CompanyCD;
            // 执行
            return SqlHelper.ExecuteSql(strSql, parameters);
        }

        /// <summary>
        /// 根据任务ID获得汇报信息
        /// </summary>
        /// <param name="CompanyCD">企业代码</param>
        /// <param name="ReportID">任务ID</param>
        /// <param name="ReportType">报表类别</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable GetTaskReport(string CompanyCD, int ReportID, int ReportType)
        {
            // SQL语句
            string strSql = @" SELECT pnr.ID
                                  ,pnr.CompanyCD
                                  ,pnr.ReportType
                                  ,pnr.NoteNo
                                  ,pnr.ReportID
                                  ,pnr.ReportNote
                                  ,pnr.ReportProgress
                                  ,pnr.Creator
                                  ,SUBSTRING(CONVERT(VARCHAR ,pnr.CreateDate ,120),0 ,11) AS CreateDate
                                  ,ei.EmployeeName
                            FROM   officedba.PersonalNoteReport pnr
                                   LEFT JOIN officedba.EmployeeInfo ei
                                        ON  ei.ID = pnr.Creator
                            WHERE  pnr.ReportType = @ReportType AND pnr.CompanyCD=@CompanyCD AND pnr.ReportID=@ReportID ";
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ReportID", SqlDbType.Int), // 任务ID
                            new SqlParameter("@ReportType", SqlDbType.Int), // 报表类别
							new SqlParameter("@CompanyCD", SqlDbType.VarChar, 8) // 企业代码
                        };
            parameters[0].Value = ReportID;
            parameters[1].Value = ReportType;
            parameters[2].Value = CompanyCD;
            // 执行
            return SqlHelper.ExecuteSql(strSql, parameters);
        }

        /// <summary>
        /// 根据日志ID删除数据记录
        /// </summary>
        /// <param name="ID">日志ID</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(int NoteID)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = @"DECLARE @NoteNo     VARCHAR(100)
DECLARE @CompanyCD  VARCHAR(20)
SELECT @NoteNo = pn.NoteNo,
       @CompanyCD = pn.CompanyCD
FROM   officedba.PersonalNote pn
WHERE  pn.ID = @ID;

DELETE 
FROM   officedba.PersonalNoteReport
WHERE  CompanyCD = @CompanyCD
       AND NoteNo = @NoteNo;

DELETE 
FROM   officedba.PersonalNote
WHERE  ID = @ID;";
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int,4) // 流水号
                        };
            parameters[0].Value = NoteID;

            comm.Parameters.AddRange(parameters);

            return comm;
        }


        #endregion
    }
}

