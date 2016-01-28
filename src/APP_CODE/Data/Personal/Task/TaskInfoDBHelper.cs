using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;
using XBase.Model.Personal.Task;

namespace XBase.Data.Personal.Task
{
    public class TaskInfoDBHelper
    {	/// <summary>
        /// 增加一条数据
        /// </summary>
        public static int AddTaskInfo(TaskModel model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into   officedba.Task (");
            strSql.Append("CompanyCD,TaskNo,TaskType,TaskTypeID,Title,Content,TaskAim,TaskStep,CompleteDate,CompleteTime,Principal,Joins,Critical,Important,Priority,Status,Attachment,Remark,Creator,CreateDate,ModifiedUserID,CanViewUser,CanViewUserName,IsMobileNotice,RemindTime,DeptID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@TaskNo,@TaskType,@TaskTypeID,@Title,@Content,@TaskAim,@TaskStep,@CompleteDate,@CompleteTime,@Principal,@Joins,@Critical,@Important,@Priority,@Status,@Attachment,@Remark,@Creator,@CreateDate,@ModifiedUserID,@CanViewUser,@CanViewUserName,@IsMobileNotice,@RemindTime,@DeptID)");
            if (model.IsMobileNotice == "0")
            {
                strSql.Replace(",@RemindTime", "");
                strSql.Replace(",RemindTime", "");
            }
            strSql.Append(";select  @ID= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TaskNo", SqlDbType.VarChar,50),
					new SqlParameter("@TaskType", SqlDbType.Char,1),
					new SqlParameter("@TaskTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@TaskAim", SqlDbType.Text),
					new SqlParameter("@TaskStep", SqlDbType.Text),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteTime", SqlDbType.VarChar,4),
					new SqlParameter("@Principal", SqlDbType.Int,4),
					new SqlParameter("@Joins", SqlDbType.VarChar,500),
					new SqlParameter("@Critical", SqlDbType.Char,1),
					new SqlParameter("@Important", SqlDbType.Char,1),
					new SqlParameter("@Priority", SqlDbType.Char,1),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                    new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
                    new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
                    new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
                    new SqlParameter("@RemindTime", SqlDbType.DateTime),
                    new SqlParameter("@DeptID", SqlDbType.Int,4),
                    new SqlParameter("@ID",SqlDbType.Int,4)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.TaskNo;
            parameters[2].Value = model.TaskType;
            parameters[3].Value = model.TaskTypeID;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.Content;
            parameters[6].Value = model.TaskAim;
            parameters[7].Value = model.TaskStep;
            parameters[8].Value = model.CompleteDate;
            parameters[9].Value = model.CompleteTime;
            parameters[10].Value = model.Principal;
            parameters[11].Value = model.Joins;
            parameters[12].Value = model.Critical;
            parameters[13].Value = model.Important;
            parameters[14].Value = model.Priority;
            parameters[15].Value = model.Status;
            parameters[16].Value = model.Attachment;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.Creator;
            parameters[19].Value = model.CreateDate;
            parameters[20].Value = model.ModifiedUserID;
            parameters[21].Value = model.CanViewUser;
            parameters[22].Value = model.CanViewUserName;
            parameters[23].Value = model.IsMobileNotice;
            if (model.IsMobileNotice == "1")
                parameters[24].Value = model.RemindTime;
            else
                parameters[24].Value = DateTime.Now;
            parameters[25].Value = model.DeptID;
            parameters[26].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            if (model.IsMobileNotice == "1")
            {

                StringBuilder strSqlt = new StringBuilder();
                strSqlt.Append("insert into officedba.NoticeHistory(");
                strSqlt.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                strSqlt.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                SqlCommand commN = new SqlCommand();
                commN.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
                commN.Parameters["@CompanyCD"].Value = model.CompanyCD;
                commN.Parameters.AddWithValue("@SourceFlag", SqlDbType.VarChar);
                commN.Parameters["@SourceFlag"].Value = "3";
                commN.Parameters.AddWithValue("@SourceID", SqlDbType.Int);
                commN.Parameters["@SourceID"].Value = parameters[26].Value;
                commN.Parameters.AddWithValue("@PlanNoticeDate", SqlDbType.DateTime);
                commN.Parameters["@PlanNoticeDate"].Value = model.RemindTime;
                commN.CommandText = strSqlt.ToString();
                SqlHelper.ExecuteTransWithCommand(commN);
            }
            return Convert.ToInt32((parameters[26].Value.ToString()));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool UpdateTaskInfo(TaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.Task set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("TaskNo=@TaskNo,");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("TaskTypeID=@TaskTypeID,");
            strSql.Append("Title=@Title,");
            strSql.Append("Content=@Content,");
            strSql.Append("TaskAim=@TaskAim,");
            strSql.Append("TaskStep=@TaskStep,");
            strSql.Append("CompleteDate=@CompleteDate,");
            strSql.Append("CompleteTime=@CompleteTime,");
            strSql.Append("Principal=@Principal,");
            strSql.Append("Joins=@Joins,");
            strSql.Append("      CanViewUser=@CanViewUser ,");
            strSql.Append("      CanViewUserName =@CanViewUserName ,");
            strSql.Append("      IsMobileNotice=@IsMobileNotice ,");
            if (model.IsMobileNotice == "1")
                strSql.Append("      RemindTime =@RemindTime,");
            strSql.Append("Critical=@Critical,");
            strSql.Append("Important=@Important,");
            strSql.Append("Priority=@Priority,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("DeptID=@DeptID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TaskNo", SqlDbType.VarChar,50),
					new SqlParameter("@TaskType", SqlDbType.Char,1),
					new SqlParameter("@TaskTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@TaskAim", SqlDbType.Text),
					new SqlParameter("@TaskStep", SqlDbType.Text),
					new SqlParameter("@CompleteDate", SqlDbType.DateTime),
					new SqlParameter("@CompleteTime", SqlDbType.VarChar,4),
					new SqlParameter("@Principal", SqlDbType.Int,4),
					new SqlParameter("@Joins", SqlDbType.VarChar,500),
					new SqlParameter("@Critical", SqlDbType.Char,1),
					new SqlParameter("@Important", SqlDbType.Char,1),
					new SqlParameter("@Priority", SqlDbType.Char,1),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                    new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
                    new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
                    new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
                    new SqlParameter("@RemindTime", SqlDbType.DateTime),
                    new SqlParameter("@DeptID", SqlDbType.Int,4)};

            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.TaskNo;
            parameters[3].Value = model.TaskType;
            parameters[4].Value = model.TaskTypeID;
            parameters[5].Value = model.Title;
            parameters[6].Value = model.Content;
            parameters[7].Value = model.TaskAim;
            parameters[8].Value = model.TaskStep;
            parameters[9].Value = model.CompleteDate;
            parameters[10].Value = model.CompleteTime;
            parameters[11].Value = model.Principal;
            parameters[12].Value = model.Joins;
            parameters[13].Value = model.Critical;
            parameters[14].Value = model.Important;
            parameters[15].Value = model.Priority;
            parameters[16].Value = model.Attachment;
            parameters[17].Value = model.Remark;
            parameters[18].Value = model.ModifiedUserID;
            parameters[19].Value = model.CanViewUser;
            parameters[20].Value = model.CanViewUserName;
            parameters[21].Value = model.IsMobileNotice;
            if (model.IsMobileNotice == "1")
                parameters[22].Value = model.RemindTime;
            else
                parameters[22].Value = DateTime.Now;
            parameters[23].Value = model.DeptID;
            SqlCommand comm = new SqlCommand();

            comm.CommandText = strSql.ToString();

            foreach (SqlParameter par in parameters)
                comm.Parameters.Add(par);

            bool result = SqlHelper.ExecuteTransWithCommand(comm);

            if (result)
            {
                if (model.IsMobileNotice == "1")
                {
                    StringBuilder strSqlt = new StringBuilder();
                    strSqlt.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and  SourceFlag ='3'  ");
                    strSqlt.Append("insert into officedba.NoticeHistory(");
                    strSqlt.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                    strSqlt.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                    SqlCommand commN = new SqlCommand();
                    commN.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
                    commN.Parameters["@CompanyCD"].Value = model.CompanyCD;
                    commN.Parameters.AddWithValue("@SourceFlag", SqlDbType.VarChar);
                    commN.Parameters["@SourceFlag"].Value = "3";
                    commN.Parameters.AddWithValue("@SourceID", SqlDbType.Int);
                    commN.Parameters["@SourceID"].Value = model.ID;
                    commN.Parameters.AddWithValue("@PlanNoticeDate", SqlDbType.DateTime);
                    commN.Parameters["@PlanNoticeDate"].Value = model.RemindTime;
                    commN.Parameters.AddWithValue("@ID", SqlDbType.Int);
                    commN.Parameters["@ID"].Value = model.ID;
                    commN.CommandText = strSqlt.ToString();
                    SqlHelper.ExecuteTransWithCommand(commN);
                }
                else
                {
                    StringBuilder strSqlt = new StringBuilder();
                    SqlCommand commN = new SqlCommand();
                    strSqlt.Append("delete  from  officedba.NoticeHistory  where  SourceID=@ID and  SourceFlag ='3'  ");
                    commN.Parameters.AddWithValue("@ID", SqlDbType.Int);
                    commN.Parameters["@ID"].Value = model.ID;
                    commN.CommandText = strSqlt.ToString();
                    SqlHelper.ExecuteTransWithCommand(commN);
                }
            }
            return result;
        }


        public static bool ChangeStatus(TaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.Task set ");
            strSql.Append("Status=@Status,");
            if (model.Status == "2")
            {
                strSql.Append("Sendor=@Sendor,");
                strSql.Append("SendDate=@SendDate,");
            }
            else if (model.Status == "4")
            {
                strSql.Append("Cancelor=@Cancelor,");
                strSql.Append("CancelDate=@CancelDate,");
            }
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@Sendor", SqlDbType.Int,4),
					new SqlParameter("@SendDate", SqlDbType.DateTime),
                    new SqlParameter("@Cancelor",SqlDbType.Int,4),
                    new SqlParameter("@CancelDate",SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.Sendor;
            parameters[3].Value = model.SendDate;
            parameters[4].Value = model.Cancelor;
            parameters[5].Value = model.CancelDate;
            parameters[6].Value = model.ModifiedUserID;
            SqlCommand comm = new SqlCommand();

            comm.CommandText = strSql.ToString();

            foreach (SqlParameter par in parameters)
                comm.Parameters.Add(par);

            return SqlHelper.ExecuteTransWithCommand(comm);

        }

        public static bool DeleteTaskById(string[] idarrary)
        {
            string sqlstr = "delete from  officedba.Task where ID = 0 ";
            foreach (string id in idarrary)
            {
                if (id != "")
                    sqlstr += " or ID = " + id;
            }
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlstr;
            return SqlHelper.ExecuteTransWithCommand(comm);
        }


        public static bool ReportTask(TaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.Task set ");
            strSql.Append("Status= '3' ,");
            strSql.Append("ResultReport = @ResultReport ,");
            strSql.Append("ReportDate= @ReportDate ");
            strSql.Append("  where  ID= @ID ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();

            comm.Parameters.AddWithValue("@ResultReport", SqlDbType.Text);
            comm.Parameters["@ResultReport"].Value = model.ResultReport;

            comm.Parameters.AddWithValue("@ReportDate", SqlDbType.DateTime);
            comm.Parameters["@ReportDate"].Value = model.ReportDate;

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        public static bool CheckTask(TaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.Task set ");
            strSql.Append("Status= '5' ,");
            strSql.Append("AddOrCut = @AddOrCut ,");
            strSql.Append("CheckDate = @CheckDate ,");
            strSql.Append("CheckUserID = @CheckUserID ,");
            strSql.Append("CheckNote = @CheckNote ,");
            strSql.Append("CheckScore= @CheckScore ");
            strSql.Append("  where  ID= @ID ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();

            comm.Parameters.AddWithValue("@AddOrCut", SqlDbType.VarChar);
            comm.Parameters["@AddOrCut"].Value = model.AddOrCut;

            comm.Parameters.AddWithValue("@CheckDate", SqlDbType.DateTime);
            comm.Parameters["@CheckDate"].Value = model.CheckDate;

            comm.Parameters.AddWithValue("@CheckUserID", SqlDbType.Int);
            comm.Parameters["@CheckUserID"].Value = model.CheckUserID;

            comm.Parameters.AddWithValue("@CheckNote", SqlDbType.Text);
            comm.Parameters["@CheckNote"].Value = model.CheckNote;

            comm.Parameters.AddWithValue("@CheckScore", SqlDbType.VarChar);
            comm.Parameters["@CheckScore"].Value = model.CheckScore;

            comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
            comm.Parameters["@ID"].Value = model.ID;

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
    }
}


