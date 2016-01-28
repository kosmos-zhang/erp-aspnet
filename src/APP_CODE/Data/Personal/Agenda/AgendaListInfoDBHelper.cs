using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Collections;

using XBase.Common;


namespace XBase.Data.Personal.Agenda
{
    public class AgendaListInfoDBHelper
    {
        public static DataTable SelectAgendaCount(DateTime dtstart, DateTime dtend, int uid, bool isCurnetuser)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT COUNT(*) AS countNum, pda.StartDate,pda.EndDate 
                            FROM   officedba.PersonalDateArrange pda  
                            WHERE  pda.CompanyCD=@CompanyCD 
                                   AND DATEDIFF(DAY, pda.StartDate, @dtend) >= 0 AND DATEDIFF(DAY, pda.EndDate, @dtstart) <= 0 
                                   AND (pda.Creator =@uid OR pda.ToManagerID = @uid OR  ',' + ISNULL(pda.CanViewUser, '') + ',' LIKE '%,' + CONVERT(VARCHAR, @uid) + ',%' ) ");
            if (isCurnetuser != true)
            {
                strSql.Append("AND pda.IsPublic = '1' ");
            }
            strSql.Append("GROUP BY pda.StartDate,pda.EndDate ");
            #endregion
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            //设置保存的参数
            comm.Parameters.AddWithValue("@dtstart", SqlDbType.DateTime);
            comm.Parameters["@dtstart"].Value = dtstart;

            comm.Parameters.AddWithValue("@dtend", SqlDbType.DateTime);
            comm.Parameters["@dtend"].Value = dtend;

            comm.Parameters.AddWithValue("@uid", SqlDbType.Int);
            comm.Parameters["@uid"].Value = uid;

            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
            comm.Parameters["@CompanyCD"].Value = userInfo.CompanyCD;

            //返回操作结果  
            return SqlHelper.ExecuteSearch(comm);

        }

        public static DataTable SelectAgendaCountByDay(DateTime sdate, int uid, bool isCurnetuser)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region  SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT pda.ID, pda.CompanyCD, pda.ArrangeTItle, pda.Critical, pda.ArrangPerson,
                                   pda.[Content], pda.StartTime, pda.EndTime, pda.Creator,
                                   pda.IsPublic, pda.CreateDate, pda.IsMobileNotice, pda.AheadTimes,
                                   pda.ModifiedDate, pda.ModifiedUserID, pda.Important,
                                   pda.ToManagerID, pda.ManagerNote, pda.ManagerDate, pda.CanViewUser,
                                   pda.CanViewUserName, pda.[Status],ei.EmployeeName AS CreatorName
                                   ,SUBSTRING(CONVERT(VARCHAR,pda.StartDate,120),0,11) AS StartDate
                                   ,SUBSTRING(CONVERT(VARCHAR,pda.EndDate,120),0,11) AS EndDate
                            FROM   officedba.PersonalDateArrange pda
                            LEFT JOIN officedba.EmployeeInfo ei ON ei.ID=pda.Creator  
                            WHERE pda.CompanyCD=@CompanyCD 
                                   AND DATEDIFF(DAY, pda.StartDate, @sdate) >= 0 AND DATEDIFF(DAY, pda.EndDate, @sdate) <= 0 
                                   AND (pda.Creator =@uid OR pda.ToManagerID = @uid OR  ',' + ISNULL(pda.CanViewUser, '') + ',' LIKE '%,' + CONVERT(VARCHAR, @uid) + ',%' ) ");
            if (isCurnetuser != true)
            {
                strSql.Append("AND pda.IsPublic = '1' ");
            }

            #endregion
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            //设置保存的参数
            comm.Parameters.AddWithValue("@sdate", SqlDbType.DateTime);
            comm.Parameters["@sdate"].Value = sdate;

            comm.Parameters.AddWithValue("@uid", SqlDbType.Int);
            comm.Parameters["@uid"].Value = uid;

            comm.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar);
            comm.Parameters["@CompanyCD"].Value = userInfo.CompanyCD;

            //返回操作结果  
            return SqlHelper.ExecuteSearch(comm);

        }

    }
}
