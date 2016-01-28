

/**********************************************
 * 类作用   施工摘要表数据处理层
 * 创建人   xz
 * 创建时间 2010-5-19 19:08:25 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.Office.ProjectProess;


namespace XBase.Data.Office.ProjectProess
{
    /// <summary>
    /// 施工摘要表数据处理类
    /// </summary>
    public class ProjectConstructionDBHelper
    {
        public static DataTable GetProessList(string projectid,string summaryid, XBase.Common.UserInfoUtil userinfo)
        {
            string sqlstr = string.Empty;
            string proesslist = "0";
            if (!string.IsNullOrEmpty(summaryid))
            {
                SqlParameter[] param = {
                                           new SqlParameter("@ID",SqlDbType.VarChar,50)
                                       };
                param[0].Value = summaryid;
                DataTable dt = SqlHelper.ExecuteSql("select ProessID from officedba.ProjectConstructionDetails where ID =@ID", param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ProessID"].ToString().Trim().Length > 0)
                    {
                        proesslist = dt.Rows[0]["ProessID"].ToString().Trim();
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.ID,A.SummaryName,isnull(B.CheckFlag,'') checkflag from officedba.ProjectConstructionDetails A ");
            sb.AppendLine("left join ");
            sb.AppendLine("(");
	        sb.AppendLine("    select *,'checked=checked' checkflag from officedba.ProjectConstructionDetails where ID in ( "+proesslist+" )");
            sb.AppendLine(")B on A.ID=B.ID");
            sb.AppendLine("where A.projectID=@projectID and A.CompanyCD=@companyCD");

            SqlParameter[] parm = {
                                      new SqlParameter("@projectID",SqlDbType.VarChar,50),
                                      new SqlParameter("@companyCD",SqlDbType.VarChar,50)
                                  };
            parm[0].Value = projectid;
            parm[1].Value = userinfo.CompanyCD;
            return SqlHelper.ExecuteSql(sb.ToString(), parm);
        }

        public static int Add(ProjectConstructionDetails model,string projectName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("INSERT INTO officedba.ProjectConstructionDetails(");
            strSql.AppendLine("SummaryName,DutyPerson,CompanyCD,projectID,ProcessScale,PersonNum,Rate,ProessID,BeginDate,EndDate,ProjectMemo)");
            strSql.AppendLine(" VALUES (");
            strSql.AppendLine("@in_SummaryName,@in_DutyPerson,@in_CompanyCD,@in_projectID,@in_ProcessScale,@in_PersonNum,@in_Rate,@in_ProessID,@in_BeginDate,@in_EndDate,@in_ProjectMemo)");
            strSql.AppendLine(";select @@IDENTITY");
            

            SqlParameter[] cmdParms = {
				new SqlParameter("@in_SummaryName",SqlDbType.VarChar,200),
				new SqlParameter("@in_DutyPerson", SqlDbType.Int,4),
				new SqlParameter("@in_CompanyCD", SqlDbType.VarChar,50),
				new SqlParameter("@in_projectID", SqlDbType.Int, 4),
				new SqlParameter("@in_ProcessScale", SqlDbType.Decimal),
				new SqlParameter("@in_PersonNum",  SqlDbType.Decimal),
				new SqlParameter("@in_Rate",  SqlDbType.Decimal),
				new SqlParameter("@in_ProessID",  SqlDbType.VarChar,50),
				new SqlParameter("@in_BeginDate", SqlDbType.DateTime),
				new SqlParameter("@in_EndDate", SqlDbType.DateTime),
				new SqlParameter("@in_ProjectMemo", SqlDbType.VarChar,50)
              //  new SqlParameter("@msgcontent",SqlDbType.VarChar,500)
                                      };
            cmdParms[0].Value = model.SummaryName;
            cmdParms[1].Value = model.DutyPerson;
            cmdParms[2].Value = model.CompanyCD;
            cmdParms[3].Value = model.projectID;
            cmdParms[4].Value = model.ProcessScale;
            cmdParms[5].Value = model.PersonNum;
            cmdParms[6].Value = model.Rate;
            cmdParms[7].Value = model.ProessID;
            cmdParms[8].Value = model.BeginDate;
            cmdParms[9].Value = model.EndDate;
            cmdParms[10].Value = model.ProjectMemo;
            int num = 0;
            try
            {
                num = int.Parse(SqlHelper.ExecuteScalar(strSql.ToString(), cmdParms).ToString());
            }
            catch {
                return num;
            }

            /*设置短信提醒*/
            //1、开工时间提醒 (提前一天提醒)
            strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.MsgSendList(CompanyCD,summaryID,msgContent,Empid) values(@CompanyCD,@summaryID,@msgcontent,@DutyPerson)");
            strSql.AppendLine(";select @@IDENTITY");
            SqlParameter[] msgParms = {
				new SqlParameter("@DutyPerson", SqlDbType.Int,4),
				new SqlParameter("@CompanyCD", SqlDbType.VarChar,50),
                new SqlParameter("@summaryID", SqlDbType.VarChar,50),
				new SqlParameter("@msgcontent",SqlDbType.VarChar,500)
                                      };

            msgParms[0].Value = model.DutyPerson;
            msgParms[1].Value = model.CompanyCD;
            msgParms[2].Value = num;
            msgParms[3].Value = "项目名称："+projectName +"中的["+ model.SummaryName + "]开工时间为：" + model.BeginDate.ToShortDateString() + ",完工时间为：" + model.EndDate.ToShortDateString()+"请您关注!";
            try
            {
                num = int.Parse(SqlHelper.ExecuteScalar(strSql.ToString(), msgParms).ToString());
                //追加到信息发送表
                string sqlstr = "insert into officedba.NoticeHistory(CompanyCD,SourceFlag,SourceID,PlanNoticeDate) values(@compCD,5,@num,@senddate)";
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                SqlParameter[] param = {
                                           new SqlParameter("@compCD",SqlDbType.VarChar,50),
                                           new SqlParameter("@num",SqlDbType.Int,4),
                                           new SqlParameter("@senddate",SqlDbType.DateTime)
                                       };
                param[0].Value = model.CompanyCD;
                param[1].Value = num;
                param[2].Value = model.BeginDate.AddDays(-1).AddHours(9);
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr, param);
                    tran.Commit();
                }
                catch {
                    tran.Rollback();
                    return 0;
                }
            }
            catch
            {
            }
            return num;
        }

        public static DataTable GetProessList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.*,B.ProjectName,C.EmployeeName from officedba.ProjectConstructionDetails A ");
            sb.AppendLine("left join officedba.ProjectInfo B  on A.ProjectID=B.ID");
            sb.AppendLine("left join officedba.EmployeeInfo C on A.DutyPerson=C.ID where A.CompanyCD=@CompanyCD");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));

            if (projectid != "0")
            {
                sb.AppendLine(" and A.ProjectID=@projectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@projectID", projectid));
            }
            if (!string.IsNullOrEmpty(summaryname))
            {
                sb.AppendLine(" and A.SummaryName like @summaryName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@summaryName", "%"+summaryname+"%"));
            }

            if (string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = "ID";
            }

            comm.CommandText = sb.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }


        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select ID,ProjectName from officedba.ProjectInfo");
            sqlstr.AppendLine("where CompanyCD=@CompanyCD");
            
            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                 };
            param[0].Value = userinfo.CompanyCD;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
            ddl.DataTextField = "ProjectName";
            ddl.DataValueField = "ID";
            ddl.DataSource = ds;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---请选择项目---", "0"));
        }

        public static DataTable GetSummaryByID(string id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.*,B.EmployeeName from officedba.ProjectConstructionDetails A left join officedba.EmployeeInfo B on A.DutyPerson = B.ID where A.id =@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = id;
            return SqlHelper.ExecuteSql(sb.ToString(),param);
        }

        public static int Update(ProjectConstructionDetails model)
        {
            TransactionManager trans = new TransactionManager();
            trans.BeginTransaction();
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE officedba.ProjectConstructionDetails SET ");
            strSql.Append("SummaryName=@in_SummaryName,");
            strSql.Append("DutyPerson=@in_DutyPerson,");
            strSql.Append("CompanyCD=@in_CompanyCD,");
            strSql.Append("projectID=@in_projectID,");
            strSql.Append("ProcessScale=@in_ProcessScale,");
            strSql.Append("PersonNum=@in_PersonNum,");
            strSql.Append("Rate=@in_Rate,");
            strSql.Append("ProessID=@in_ProessID,");
            strSql.Append("BeginDate=@in_BeginDate,");
            strSql.Append("EndDate=@in_EndDate,");
            strSql.Append("ProjectMemo=@in_ProjectMemo");
            strSql.Append(" WHERE ID=@in_ID");
            SqlParameter[] Parms = {
				new SqlParameter("@in_SummaryName", SqlDbType.VarChar,200),
				new SqlParameter("@in_DutyPerson", SqlDbType.Int),
				new SqlParameter("@in_CompanyCD", SqlDbType.VarChar,50),
				new SqlParameter("@in_projectID", SqlDbType.Int),
				new SqlParameter("@in_ProcessScale", SqlDbType.Decimal),
				new SqlParameter("@in_PersonNum", SqlDbType.Decimal),
				new SqlParameter("@in_Rate", SqlDbType.Decimal),
				new SqlParameter("@in_ProessID", SqlDbType.VarChar),
				new SqlParameter("@in_BeginDate", SqlDbType.VarChar,30),
				new SqlParameter("@in_EndDate", SqlDbType.VarChar,30),
				new SqlParameter("@in_ProjectMemo", SqlDbType.VarChar,5000),
				new SqlParameter("@in_ID", SqlDbType.Int)};

            Parms[0].Value = model.SummaryName;
            Parms[1].Value = model.DutyPerson;
            Parms[2].Value = model.CompanyCD;
            Parms[3].Value = model.projectID;
            Parms[4].Value = model.ProcessScale;
            Parms[5].Value = model.PersonNum;
            Parms[6].Value = model.Rate;
            Parms[7].Value = model.ProessID;
            Parms[8].Value = model.BeginDate.ToString();
            Parms[9].Value = model.EndDate.ToString();
            Parms[10].Value = model.ProjectMemo;
            Parms[11].Value = model.ID;
            int num = 0;
            try
            {
                num = SqlHelper.ExecuteNonQuery(trans.Trans, CommandType.Text, strSql.ToString(), Parms);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            return num;
        }

        public static int Delete(string IDList)
        {
            TransactionManager trans = new TransactionManager();
            trans.BeginTransaction();

            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("delete from officedba.ProjectConstructionDetails where id in(" + IDList + ")");
            strSql.AppendLine("delete from officedba.MsgSendList where summaryID in ("+IDList +")");
            int num = 0;
            try
            {
                num = SqlHelper.ExecuteNonQuery(trans.Trans, CommandType.Text, strSql.ToString());
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            return num;
        }
    }
}

