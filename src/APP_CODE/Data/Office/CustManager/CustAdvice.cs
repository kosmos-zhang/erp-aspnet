using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

namespace XBase.Data.Office.CustManager
{
    public class CustAdvice
    {
        /// <summary>
        /// 添加客户建议 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddCustAdvice(CustAdviceModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("INSERT INTO [officedba].[CustAdvice]  ");
            sql.AppendLine("           ([CompanyCD]               ");
            sql.AppendLine("           ,[AdviceNo]                ");
            sql.AppendLine("           ,[Title]                   ");
            sql.AppendLine("           ,[CustID]                  ");
            sql.AppendLine("           ,[Advicer]                 ");
            sql.AppendLine("           ,[CustLinkMan]             ");
            sql.AppendLine("           ,[DestClerk]               ");
            sql.AppendLine("           ,[AdviceType]              ");
            sql.AppendLine("           ,[AdviceDate]              ");
            sql.AppendLine("           ,[Accept]                  ");
            sql.AppendLine("           ,[State]                   ");
            sql.AppendLine("           ,[Contents]                ");
            if (!string.IsNullOrEmpty(model.DoSomething))
            {
                sql.AppendLine("           ,[DoSomething]             ");
            }
            if (!string.IsNullOrEmpty(model.LeadSay))
            {
                sql.AppendLine("           ,[LeadSay]                 ");
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("           ,[Remark]                  ");
            }
            sql.AppendLine("           ,[ModifiedDate]            ");
            sql.AppendLine("           ,[ModifiedUserID]          ");
            sql.AppendLine("           ,[Creator]                 ");
            sql.AppendLine("           ,[CreatedDate]            ");
            sql.AppendLine("           ,[CanViewUser]                 ");
            sql.AppendLine("           ,[CanViewUserName])            ");
            sql.AppendLine("     VALUES                           ");
            sql.AppendLine("           (@CompanyCD                ");
            sql.AppendLine("           ,@AdviceNo                 ");
            sql.AppendLine("           ,@Title                    ");
            sql.AppendLine("           ,@CustID                   ");
            sql.AppendLine("           ,@Advicer                  ");
            sql.AppendLine("           ,@CustLinkMan              ");
            sql.AppendLine("           ,@DestClerk                ");
            sql.AppendLine("           ,@AdviceType               ");
            sql.AppendLine("           ,@AdviceDate               ");
            sql.AppendLine("           ,@Accept                   ");
            sql.AppendLine("           ,@State                    ");
            sql.AppendLine("           ,@Contents                 ");            
            if (!string.IsNullOrEmpty(model.DoSomething))
            {
                sql.AppendLine("           ,@DoSomething              ");
            }
            if (!string.IsNullOrEmpty(model.LeadSay))
            {
                sql.AppendLine("           ,@LeadSay                  ");
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("           ,@Remark                   ");
            }
            sql.AppendLine("           ,@ModifiedDate             ");
            sql.AppendLine("           ,@ModifiedUserID           ");
            sql.AppendLine("           ,@Creator                  ");
            sql.AppendLine("           ,@CreatedDate            ");
            sql.AppendLine("           ,@CanViewUser                    ");
            sql.AppendLine("           ,@CanViewUserName )                ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdviceNo", model.AdviceNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Advicer", model.Advicer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustLinkMan", model.CustLinkMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@DestClerk", model.DestClerk));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdviceType", model.AdviceType));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate", model.AdviceDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Accept", model.Accept));
            comm.Parameters.Add(SqlHelper.GetParameter("@State", model.State));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contents", model.Contents));
            if (!string.IsNullOrEmpty(model.DoSomething))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@DoSomething", model.DoSomething));
            }
            if (!string.IsNullOrEmpty(model.LeadSay))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@LeadSay", model.LeadSay));
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreatedDate", model.CreatedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", model.CanViewUser));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", model.CanViewUserName));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        /// <summary>
        /// 修改建议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpCustAdvice(CustAdviceModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" UPDATE [officedba].[CustAdvice]             ");
            sql.AppendLine("    SET [Title] =@Title                      ");
            sql.AppendLine("       ,[CustID] =@CustID                    ");
            sql.AppendLine("       ,[Advicer] =@Advicer                  ");
            sql.AppendLine("       ,[CustLinkMan] =@CustLinkMan          ");
            sql.AppendLine("       ,[DestClerk] =@DestClerk              ");
            sql.AppendLine("       ,[AdviceType] =@AdviceType            ");
            sql.AppendLine("       ,[AdviceDate] =@AdviceDate            ");
            sql.AppendLine("       ,[Accept] =@Accept                    ");
            sql.AppendLine("       ,[State] =@State                      ");
            sql.AppendLine("       ,[Contents] =@Contents                ");
            sql.AppendLine("       ,[DoSomething] =@DoSomething          ");
            sql.AppendLine("       ,[LeadSay] =@LeadSay                  ");
            sql.AppendLine("       ,[Remark] =@Remark                    ");
            sql.AppendLine("       ,[CanViewUser] = @CanViewUser         ");
            sql.AppendLine("       ,[CanViewUserName] = @CanViewUserName ");
            sql.AppendLine("       ,[ModifiedDate] =@ModifiedDate        ");
            sql.AppendLine("       ,[ModifiedUserID] =@ModifiedUserID    ");
            sql.AppendLine("      where ID=@ID ");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Advicer", model.Advicer));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustLinkMan", model.CustLinkMan));
            comm.Parameters.Add(SqlHelper.GetParameter("@DestClerk", model.DestClerk));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdviceType", model.AdviceType));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate", model.AdviceDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Accept", model.Accept));
            comm.Parameters.Add(SqlHelper.GetParameter("@State", model.State));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contents", model.Contents));
            comm.Parameters.Add(SqlHelper.GetParameter("@DoSomething", model.DoSomething));
            comm.Parameters.Add(SqlHelper.GetParameter("@LeadSay", model.LeadSay));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUser", model.CanViewUser));
            comm.Parameters.Add(SqlHelper.GetParameter("@CanViewUserName", model.CanViewUserName));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteTransWithCommand(comm);

        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetCustAdvice(string CanUserID,CustAdviceModel model, int pageIndex, int pageSize, string OrderBy, string BeginTime, string EndTime, string CompanyCD, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select  a.ID,isnull(a.AdviceNo,'') as AdviceNo,isnull(a.Title,'') as Title,isnull(b.CustName,'') as CustName,isnull(c.LinkManName,'') as LinkManName,isnull(d.EmployeeName,'') as EmployeeName,                                                                        ");
            sql.AppendLine("        case a.Accept when '1' then '暂不考虑' when '2' then '一般' when '3' then '争取改进' when '4' then '一定做到' else '' end as Accept ");
            sql.AppendLine("        ,isnull(substring(CONVERT(varchar,a.[AdviceDate],120),0,11),'')as AdviceDate,case a.AdviceType when '1' then '不满意' when '2' then '希望做到' when '3' then '其他' else '' end as AdviceType      ");
            sql.AppendLine("        ,case a.State when '1' then '未处理' when '2' then '处理中' when '3' then '已处理' else '' end as State                             ");
            sql.AppendLine(" from    officedba.CustAdvice  as a  left join officedba.CustInfo as b on a.CustID=b.ID                                                     ");
            sql.AppendLine(" left join officedba.CustLinkMan as c on a.CustLinkMan=c.ID                                                                                 ");
            sql.AppendLine(" left join officedba.EmployeeInfo as d on a.DestClerk=d.ID                                                                                  ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            sql.AppendLine(" and (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.DestClerk  or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(model.AdviceNo))
            {
                sql.AppendLine(" and a.AdviceNo like @AdviceNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceNo", "%" + model.AdviceNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(model.CustID) && model.CustID != "0")
            {
                sql.AppendLine(" and a.CustID=@CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));

            }
            if (!string.IsNullOrEmpty(model.DestClerk) && model.DestClerk != "0")
            {
                sql.AppendLine(" and a.DestClerk=@DestClerk");
                comm.Parameters.Add(SqlHelper.GetParameter("@DestClerk", model.DestClerk));
            }
            if (model.AdviceType != "0")
            {
                sql.AppendLine(" and a.AdviceType=@AdviceType");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceType", model.AdviceType));
            }
            if (model.State != "0")
            {
                sql.AppendLine(" and a.State=@State");
                comm.Parameters.Add(SqlHelper.GetParameter("@State", model.State));
            }
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and a.AdviceDate>=@AdviceDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate", BeginTime));

            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and a.AdviceDate<@AdviceDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate1", Convert.ToDateTime(EndTime).AddDays(1)));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref TotalCount);

        }
        /// <summary>
        /// 获取一个单据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneCustAdvice(CustAdviceModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT a.[ID]                                                                                  ");
            sql.AppendLine("      ,isnull(a.[AdviceNo],'') as AdviceNo                                                     ");
            sql.AppendLine("      ,isnull(a.[Title],'') as Title                                                           ");
            sql.AppendLine("      ,isnull(a.[CustID],0) as CustID                                                          ");
            sql.AppendLine("      ,isnull(a.[Advicer],'') as Advicer                                                       ");
            sql.AppendLine("      ,isnull(a.[CustLinkMan],0) as CustLinkMan                                                ");
            sql.AppendLine("      ,isnull(a.[DestClerk],0) as DestClerk                                                    ");
            sql.AppendLine("      ,a.[AdviceType]                                                                          ");
            sql.AppendLine("      ,isnull(CONVERT(varchar,a.[AdviceDate],120),'')     as [AdviceDate]                                                                      ");
            sql.AppendLine("      ,a.[Accept]                                                                              ");
            sql.AppendLine("      ,a.[State]                                                                               ");
            sql.AppendLine("      ,isnull(a.[Contents],'') as Contents                                                     ");
            sql.AppendLine("      ,isnull(a.[DoSomething],'') as DoSomething                                               ");
            sql.AppendLine("      ,isnull(a.[LeadSay],'') as  LeadSay                                                      ");
            sql.AppendLine("      ,isnull(a.[Remark],'') as Remark                                                         ");
            sql.AppendLine("      ,a.Creator           ");
            sql.AppendLine("      ,isnull(substring(CONVERT(varchar,a.[CreatedDate],120),0,11),'') as CreatedDate          ");
            sql.AppendLine("      ,isnull(substring(CONVERT(varchar,a.[ModifiedDate],120),0,11),'') as ModifiedDate        ");
            sql.AppendLine("      ,a.[ModifiedUserID]                                                                      ");
            sql.AppendLine("      ,isnull(b.CustName,'') as CustName ,b.CustNo                                                      ");
            sql.AppendLine("      ,isnull(c.LinkManName,'') as LinkManName                                                 ");
            sql.AppendLine("      ,isnull(d.EmployeeName,'') as EmployeeName                                               ");
            sql.AppendLine("      ,isnull(e.EmployeeName,'') as  CreatorName                                                 ");
            sql.AppendLine(" ,a.CanViewUser,a.CanViewUserName                                                               ");
            sql.AppendLine("  FROM [officedba].[CustAdvice]  as a left join officedba.CustInfo as b on a.CustID=b.ID       ");
            sql.AppendLine("      left join officedba.CustLinkMan as c on a.CustLinkMan=c.ID                               ");
            sql.AppendLine("      left join officedba.EmployeeInfo as d on a.DestClerk=d.ID                                ");
            sql.AppendLine("      left join officedba.EmployeeInfo as e on a.Creator=e.ID                                ");
            sql.AppendLine(" where a.ID=@ID");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 单据打印需要
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetOneCustAdviceInfo(CustAdviceModel model)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("SELECT a.[ID]                                                                                  ");
            sql.AppendLine("      ,isnull(a.[AdviceNo],'') as AdviceNo                                                     ");
            sql.AppendLine("      ,isnull(a.[Title],'') as Title                                                           ");
            sql.AppendLine("      ,isnull(a.[Advicer],'') as Advicer                                                       ");
            sql.AppendLine("      ,case a.[AdviceType]  when '1' then '不满意' when '2' then '希望做到' when '3' then '其他' else '' end as AdviceType            ");
            sql.AppendLine("      ,a.[AdviceDate]                                                                          ");
            sql.AppendLine("      ,case a.[Accept]  when '1' then '暂不考虑' when '2' then '一般' when '3' then '争取改进' when '4' then '一定做到' else '' end as Accept  ");
            sql.AppendLine("      ,case  a.[State]   when '1' then '未处理' when '2' then '处理中' when '3' then '已处理' else '' end as State       ");
            sql.AppendLine("      ,isnull(a.[Contents],'') as Contents                                                     ");
            sql.AppendLine("      ,isnull(a.[DoSomething],'') as DoSomething                                               ");
            sql.AppendLine("      ,isnull(a.[LeadSay],'') as  LeadSay                                                      ");
            sql.AppendLine("      ,isnull(a.[Remark],'') as Remark                                                         ");
            sql.AppendLine("      ,a.[CreatedDate] as CreatedDate          ");
            sql.AppendLine("      ,isnull(substring(CONVERT(varchar,a.[ModifiedDate],120),0,11),'') as ModifiedDate        ");
            sql.AppendLine("      ,a.[ModifiedUserID]                                                                      ");
            sql.AppendLine("      ,isnull(b.CustName,'') as CustID                                                       ");
            sql.AppendLine("      ,isnull(c.LinkManName,'') as CustLinkMan                                                 ");
            sql.AppendLine("      ,isnull(d.EmployeeName,'') as DestClerk                                               ");
            sql.AppendLine("      ,isnull(e.EmployeeName,'') as  Creator ");
            sql.AppendLine("      ,isnull(a.CanViewUserName,'') as  CanViewUserName ");
            sql.AppendLine("  FROM [officedba].[CustAdvice]  as a left join officedba.CustInfo as b on a.CustID=b.ID       ");
            sql.AppendLine("      left join officedba.CustLinkMan as c on a.CustLinkMan=c.ID                               ");
            sql.AppendLine("      left join officedba.EmployeeInfo as d on a.DestClerk=d.ID                                ");
            sql.AppendLine("      left join officedba.EmployeeInfo as e on a.Creator=e.ID                                ");
            sql.AppendLine(" where a.ID=@ID");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 导出功能
        /// </summary>
        /// <param name="model"></param>
        /// <param name="OrderBy"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static DataTable GetCustAdvice(string CanUserID,CustAdviceModel model,  string OrderBy, string BeginTime, string EndTime)
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select  a.ID,isnull(a.AdviceNo,'') as AdviceNo,isnull(a.Title,'') as Title,isnull(b.CustName,'') as CustName,isnull(c.LinkManName,'') as LinkManName,isnull(d.EmployeeName,'') as EmployeeName,                                                                        ");
            sql.AppendLine("        case a.Accept when '1' then '暂不考虑' when '2' then '一般' when '3' then '争取改进' when '4' then '一定做到' else '' end as Accept ");
            sql.AppendLine("        ,isnull(substring(CONVERT(varchar,a.[AdviceDate],120),0,11),'')as AdviceDate,case a.AdviceType when '1' then '不满意' when '2' then '希望做到' when '3' then '其他' else '' end as AdviceType      ");
            sql.AppendLine("        ,case a.State when '1' then '未处理' when '2' then '处理中' when '3' then '已处理' else '' end as State                             ");
            sql.AppendLine(" from    officedba.CustAdvice  as a  left join officedba.CustInfo as b on a.CustID=b.ID                                                     ");
            sql.AppendLine(" left join officedba.CustLinkMan as c on a.CustLinkMan=c.ID                                                                                 ");
            sql.AppendLine(" left join officedba.EmployeeInfo as d on a.DestClerk=d.ID                                                                                  ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            sql.AppendLine(" and (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.DestClerk  or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )");

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD",model.CompanyCD));
            if (!string.IsNullOrEmpty(model.AdviceNo))
            {
                sql.AppendLine(" and a.AdviceNo like @AdviceNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceNo", "%" + model.AdviceNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(model.CustID) && model.CustID != "0")
            {
                sql.AppendLine(" and a.CustID=@CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));

            }
            if (!string.IsNullOrEmpty(model.DestClerk) && model.DestClerk != "0")
            {
                sql.AppendLine(" and a.DestClerk=@DestClerk");
                comm.Parameters.Add(SqlHelper.GetParameter("@DestClerk", model.DestClerk));
            }
            if (model.AdviceType != "0")
            {
                sql.AppendLine(" and a.AdviceType=@AdviceType");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceType", model.AdviceType));
            }
            if (model.State != "0")
            {
                sql.AppendLine(" and a.State=@State");
                comm.Parameters.Add(SqlHelper.GetParameter("@State", model.State));
            }
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and a.AdviceDate>=@AdviceDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate", BeginTime));

            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and a.AdviceDate<@AdviceDate1");
                comm.Parameters.Add(SqlHelper.GetParameter("@AdviceDate1", Convert.ToDateTime(EndTime).AddDays(1)));
            }
            sql.AppendLine(" order by "+OrderBy);
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DelCust(string ID)
        {
 
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    if (arrID[i] != "")
                    {
                        StringBuilder sqlDet = new StringBuilder();
                        sqlDet.AppendLine(" delete from officedba.[CustAdvice] where ID=@ID");
                        SqlCommand commDet = new SqlCommand();
                        commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                        commDet.CommandText = sqlDet.ToString();
                       
                        listADD.Add(commDet);

                    }
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
    }
}

