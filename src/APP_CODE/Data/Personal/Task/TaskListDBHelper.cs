using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Model.Personal.Task;
using XBase.Data.DBHelper;

namespace XBase.Data.Personal.Task
{
    public class TaskListDBHelper
    {
        public static DataTable SelectTaskList(TaskModel model, string orderby)
        {

            SqlCommand comm = GetWhereString(model, orderby);
            return SqlHelper.ExecuteSearch(comm);

        }
        public static DataTable GetTaskListReportDept(TaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            SqlCommand comm = new SqlCommand();
            #region  SQL语句拼写
            string whereStr = GetReportWhere(model, ref comm);


            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (model.Remark == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getDeptNameByID(DeptID) as DeptName ,DeptID  ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY DeptID ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  * ,dbo.getEmployeeName(Principal) as PrincipalName, dbo.getDeptNameByID(DeptID)  as DeptName ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion
            //定义更新基本信息的命令 

            comm.CommandText = SelectAimListSqlString.ToString();
            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);

            return dt;

        }

        public static DataTable GetTaskListReportPrincipal(TaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            SqlCommand comm = new SqlCommand();
            #region  SQL语句拼写
            string whereStr = GetReportWhere(model, ref comm);

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (model.Remark == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , dbo.getEmployeeName(Principal) as PrincipalName ,Principal  ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY Principal ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  * ,dbo.getEmployeeName(Principal) as PrincipalName,dbo.getDeptNameByID(DeptID)  as DeptName    ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion
            //定义更新基本信息的命令 

            comm.CommandText = SelectAimListSqlString.ToString();
            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);

            return dt;

        }
        public static DataTable GetTaskListReportStatus(TaskModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            SqlCommand comm = new SqlCommand();
            #region  SQL语句拼写
            string whereStr = GetReportWhere(model, ref comm);

            StringBuilder SelectAimListSqlString = new StringBuilder();
            if (model.Remark == "PIC")
            {
                SelectAimListSqlString.Append("SELECT  count(*)  as Num , Status  ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
                SelectAimListSqlString.Append("  GROUP BY Status ");
            }
            else
            {
                SelectAimListSqlString.Append("SELECT  * ,dbo.getEmployeeName(Principal) as PrincipalName , dbo.getDeptNameByID(DeptID)  as DeptName ");
                SelectAimListSqlString.Append("          FROM  officedba.Task   ");
                SelectAimListSqlString.Append(whereStr);
            }

            #endregion
            //定义更新基本信息的命令 

            comm.CommandText = SelectAimListSqlString.ToString();
            DataTable dt = new DataTable();
            // 执行查找操作
            dt = SqlHelper.ExecuteSearch(comm);

            return dt;

        }

        public static string GetReportWhere(TaskModel model, ref SqlCommand com)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sb = new StringBuilder();
            comm.Parameters.AddWithValue("@Principal", SqlDbType.Int);
            comm.Parameters["@Principal"].Value = model.Principal;

            sb.Append(" where   CompanyCD = '" + userInfo.CompanyCD + "'  ");


            if (model.TaskTypeID > 0)
            {
                sb.Append(" and  TaskTypeID=@TaskTypeID   ");
                comm.Parameters.AddWithValue("@TaskTypeID", SqlDbType.Int);
                comm.Parameters["@TaskTypeID"].Value = model.TaskTypeID;
            }

            if (!string.IsNullOrEmpty(model.TaskNo) && model.TaskNo.Trim() != "")
            {
                sb.Append(" and  TaskNo  like  @TaskNo   ");
                comm.Parameters.AddWithValue("@TaskNo", SqlDbType.VarChar);
                comm.Parameters["@TaskNo"].Value = "%" + model.TaskNo.Trim() + "%";
            }


            if (!string.IsNullOrEmpty(model.Title))
            {
                sb.Append(" and  Title like @Title   ");
                comm.Parameters.AddWithValue("@Title", SqlDbType.VarChar);
                comm.Parameters["@Title"].Value = "%" + model.Title.Trim() + "%";
            }

            if (!string.IsNullOrEmpty(model.Critical))
            {
                sb.Append(" and  Critical=@Critical   ");
                comm.Parameters.AddWithValue("@Critical", SqlDbType.VarChar);
                comm.Parameters["@Critical"].Value = model.Critical;
            }

            if (!string.IsNullOrEmpty(model.Important))
            {
                sb.Append(" and  Important=@Important   ");
                comm.Parameters.AddWithValue("@Important", SqlDbType.VarChar);
                comm.Parameters["@Important"].Value = model.Important;
            }
            if (!string.IsNullOrEmpty(model.Priority))
            {
                sb.Append(" and  Priority=@Priority   ");
                comm.Parameters.AddWithValue("@Priority", SqlDbType.VarChar);
                comm.Parameters["@Priority"].Value = model.Priority;
            }
            if (!string.IsNullOrEmpty(model.Status))
            {
                sb.Append(" and  Status=@Status   ");
                comm.Parameters.AddWithValue("@Status", SqlDbType.VarChar);
                comm.Parameters["@Status"].Value = model.Status;
            }

            if (model.SendDate + "" != "")
            {
                sb.Append(" and  CompleteDate>= @SendDate   ");
                comm.Parameters.AddWithValue("@SendDate", SqlDbType.DateTime);
                comm.Parameters["@SendDate"].Value = model.SendDate;
            }

            if (model.ReportDate + "" != "")
            {
                sb.Append(" and  CompleteDate<= @ReportDate   ");
                comm.Parameters.AddWithValue("@ReportDate", SqlDbType.DateTime);
                comm.Parameters["@ReportDate"].Value = model.ReportDate.AddDays(1);
            }


            if (model.CheckUserID > 0)
            {
                if (model.CheckNote == "1")
                {
                    sb.Append("  AND  ");
                    sb.Append("  DeptID  = @SearchID ");
                }
                else
                {
                    sb.Append("  AND  ");
                    sb.Append("  Principal=@SearchID ");
                }
                comm.Parameters.AddWithValue("@SearchID", SqlDbType.Int);
                comm.Parameters["@SearchID"].Value = model.CheckUserID;
            }


            if (model.Principal > 0)
            {
                sb.Append(" and   ( charindex('," + userInfo.EmployeeID + ",' , ','+CanViewUser+',')>0 OR  Sendor = @Sendor   OR  charindex('," + userInfo.EmployeeID + ",' , ','+Joins+',')>0  OR Principal= " + userInfo.EmployeeID + " OR  Creator=" + userInfo.EmployeeID + " OR CanViewUser='' OR CanViewUser is null)  and Principal=@Principal   ");

                comm.Parameters.AddWithValue("@Sendor", SqlDbType.Int);
                try { comm.Parameters["@Sendor"].Value = Convert.ToInt32(model.Joins); }
                catch { comm.Parameters["@Sendor"].Value = 0; }

                comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
                try { comm.Parameters["@Creator"].Value = Convert.ToInt32(model.Joins); }
                catch { comm.Parameters["@Creator"].Value = 0; }

                comm.Parameters.AddWithValue("@Joins", SqlDbType.VarChar);
                comm.Parameters["@Joins"].Value = "%" + model.Joins + "%";

            }
            else
            {
                sb.Append(" and ( charindex('," + userInfo.EmployeeID + ",' , ','+CanViewUser+',')>0 OR  Sendor = @Sendor   OR  charindex('," + userInfo.EmployeeID + ",' , ','+Joins+',')>0  OR Principal= " + userInfo.EmployeeID + " OR  Creator=" + userInfo.EmployeeID + " OR CanViewUser='' OR CanViewUser is null)   ");

                comm.Parameters.AddWithValue("@Sendor", SqlDbType.Int);
                try { comm.Parameters["@Sendor"].Value = Convert.ToInt32(model.Joins); }
                catch { comm.Parameters["@Sendor"].Value = 0; }

                comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
                try { comm.Parameters["@Creator"].Value = Convert.ToInt32(model.Joins); }
                catch { comm.Parameters["@Creator"].Value = 0; }

                comm.Parameters.AddWithValue("@Joins", SqlDbType.VarChar);
                comm.Parameters["@Joins"].Value = "%" + model.Joins + "%";
            }

            com = comm;
            return sb.ToString();
        }

        private static SqlCommand GetWhereString(TaskModel model, string orderby)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sb = new StringBuilder(" select *,officedba.getEmployeeNameByID(Principal) as PrincipalName  from   officedba.Task ");
            comm.Parameters.AddWithValue("@Principal", SqlDbType.Int);
            if (model.TaskType == "1")
            {
                sb.Append(" where   CompanyCD = '" + userInfo.CompanyCD + "'  and   (     (Joins like  @Joins   or  Principal= @Principal   )   and status = '2'  ) ");
            }

            if (model.TaskType == "2")
            {
                sb.Append(" where  TaskType = '1'   and CompanyCD = '" + userInfo.CompanyCD + "'  ");
            }


            if (model.TaskType == "3")
            {
                sb.Append(" where TaskType = '2'   and CompanyCD = '" + userInfo.CompanyCD + "'   ");
            }

            if (model.TaskTypeID > 0)
            {
                sb.Append(" and  TaskTypeID=@TaskTypeID   ");
                comm.Parameters.AddWithValue("@TaskTypeID", SqlDbType.Int);
                comm.Parameters["@TaskTypeID"].Value = model.TaskTypeID;
            }

            if (!string.IsNullOrEmpty(model.TaskNo) && model.TaskNo.Trim() != "")
            {
                sb.Append(" and  TaskNo  like  @TaskNo   ");
                comm.Parameters.AddWithValue("@TaskNo", SqlDbType.VarChar);
                comm.Parameters["@TaskNo"].Value = "%" + model.TaskNo.Trim() + "%";
            }


            if (!string.IsNullOrEmpty(model.Title))
            {
                sb.Append(" and  Title like @Title   ");
                comm.Parameters.AddWithValue("@Title", SqlDbType.VarChar);
                comm.Parameters["@Title"].Value = "%" + model.Title.Trim() + "%";
            }

            if (!string.IsNullOrEmpty(model.Critical))
            {
                sb.Append(" and  Critical=@Critical   ");
                comm.Parameters.AddWithValue("@Critical", SqlDbType.VarChar);
                comm.Parameters["@Critical"].Value = model.Critical;
            }

            if (!string.IsNullOrEmpty(model.Important))
            {
                sb.Append(" and  Important=@Important   ");
                comm.Parameters.AddWithValue("@Important", SqlDbType.VarChar);
                comm.Parameters["@Important"].Value = model.Important;
            }
            if (!string.IsNullOrEmpty(model.Priority))
            {
                sb.Append(" and  Priority=@Priority   ");
                comm.Parameters.AddWithValue("@Priority", SqlDbType.VarChar);
                comm.Parameters["@Priority"].Value = model.Priority;
            }
            if (!string.IsNullOrEmpty(model.Status))
            {
                sb.Append(" and  Status=@Status   ");
                comm.Parameters.AddWithValue("@Status", SqlDbType.VarChar);
                comm.Parameters["@Status"].Value = model.Status;
            }

            if (model.Principal > 0)
            {
                sb.Append(" and   ( charindex('," + userInfo.EmployeeID + ",' , ','+CanViewUser+',')>0 OR  Sendor = @Sendor   OR  charindex('," + userInfo.EmployeeName + ",' , ','+Joins+',')>0  OR Principal= " + userInfo.EmployeeID + " OR  Creator=" + userInfo.EmployeeID + " OR CanViewUser='' OR CanViewUser is null)  and Principal=@Principal   ");
                comm.Parameters["@Principal"].Value = model.Principal;

                comm.Parameters.AddWithValue("@Sendor", SqlDbType.Int);
                try { comm.Parameters["@Sendor"].Value = Convert.ToInt32(userInfo.EmployeeID); }
                catch { comm.Parameters["@Sendor"].Value = 0; }

                comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
                try { comm.Parameters["@Creator"].Value = Convert.ToInt32(userInfo.EmployeeID); }
                catch { comm.Parameters["@Creator"].Value = 0; }

                comm.Parameters.AddWithValue("@Joins", SqlDbType.VarChar);
                comm.Parameters["@Joins"].Value = "%" + model.Joins + "%";

            }
            else
            {
                sb.Append(" and ( charindex('," + userInfo.EmployeeID + ",' , ','+CanViewUser+',')>0 OR  Sendor = @Sendor   OR  charindex('," + userInfo.EmployeeName + ",' , ','+Joins+',')>0  OR Principal= " + userInfo.EmployeeID + " OR  Creator=" + userInfo.EmployeeID + " OR CanViewUser='' OR CanViewUser is null)   ");

                comm.Parameters["@Principal"].Value = userInfo.EmployeeID;
                comm.Parameters.AddWithValue("@Sendor", SqlDbType.Int);
                try { comm.Parameters["@Sendor"].Value = Convert.ToInt32(userInfo.EmployeeID); }
                catch { comm.Parameters["@Sendor"].Value = 0; }

                comm.Parameters.AddWithValue("@Creator", SqlDbType.Int);
                try { comm.Parameters["@Creator"].Value = Convert.ToInt32(userInfo.EmployeeID); }
                catch { comm.Parameters["@Creator"].Value = 0; }

                comm.Parameters.AddWithValue("@Joins", SqlDbType.VarChar);
                comm.Parameters["@Joins"].Value = "%" + model.Joins + "%";
            }

            sb.Append("        " + orderby + "   ");

            comm.CommandText = sb.ToString();
            return comm;

        }

        public static DataTable SelectTaskById(int id)
        {
            string sqlStr = @"SELECT t.ID,t.CompanyCD,t.TaskNo,t.TaskType,t.TaskTypeID,t.Title,t.[Content]
                              ,t.TaskAim,t.TaskStep,t.CompleteDate,t.CompleteTime,t.Principal,t.Joins,t.Critical
                              ,t.Important,t.Priority,t.[Status],t.Attachment,t.Remark,t.Creator,t.CreateDate,t.Sendor
                              ,t.SendDate,t.Cancelor,t.CancelDate,t.ResultReport,t.EndDate,t.ReportDate,t.AddOrCut
                              ,t.CheckNote,t.CheckScore,t.CheckDate,t.CheckUserID,t.ModifiedUserID,t.DeptID,t.CanViewUser
                              ,t.CanViewUserName,t.IsMobileNotice
                              ,CONVERT(VARCHAR ,t.RemindTime ,120) AS RemindTime
                              ,dbo.getDeptNameByID(DeptID) AS DeptName
                              ,officedba.getEmployeeNameByID(Principal) AS PrincipalName
                              ,officedba.getEmployeeNameByID(Creator) AS CreatorName
                              ,joins AS joinsName
                        FROM   officedba.Task t
                        WHERE  t.id = @id ";
            SqlCommand comm = new SqlCommand();
            comm.Parameters.AddWithValue("@id", SqlDbType.Int);
            comm.Parameters["@id"].Value = id;
            comm.CommandText = sqlStr;
            return SqlHelper.ExecuteSearch(comm);
        }
    }
}
