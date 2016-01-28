using System;
using System.Linq;
using System.Text;
using System.Data;
namespace XBase.Data.Office.SystemManager
{
    /// <summary>
    /// 类名：UserInfoDBHelper
    /// 描述：用户管理数据库层处理
    /// 
    /// 作者：陶春
    /// 创建时间：2009/03/27
    /// 最后修改时间：
    /// </summary>
    ///
    public class ApprovalFlowSet
    {
        /// <summary>
        /// 根据分类标志获取分类信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetBillTypeByTypeFlag()
        {
            //SQL拼写
            //    string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //    string sql = "select UserID,UserName,"
            //        + "   isnull( CONVERT(CHAR(10), OpenDate, 23),'') as OpenDate ,"
            //        + " isnull( CONVERT(CHAR(10), CloseDate, 23),'') as CloseDate ,"
            //        + "Case WHEN LockFlag = '1' THEN '是' ELSE '否' END AS LockFlag ,Case WHEN UsedStatus = '1' THEN '启用' ELSE '停用' END AS UsedStatus,ModifiedDate,ModifiedUserID,remark FROM officedba.UserInfo WHERE CompanyCD = '" + companyCD + "'";
            //    sql += " AND UserID LIKE '%" + model.UserID + "%'";
            //    if (!string.IsNullOrEmpty(model.UserName))
            //    {
            //        sql += " AND UserName LIKE '%" + model.UserName + "%'";
            //    }
            //    if (model.LockFlag == "1")
            //    {
            //        sql += " AND LockFlag = '" + ConstUtil.LOCK_FLAG_LOCKED + "'";
            //    }
            //    else
            //    {
            //        sql += " AND LockFlag = '" + ConstUtil.LOCK_FLAG_UNLOCKED + "'";
            //    }
            //    //开始时间输入的场合，添加为条件
            //    if (model.OpenDate != null)
            //    {
            //        sql += " AND OpenDate >= '" + model.OpenDate + "'";
            //    }
            //    //结束时间输入的场合，添加为条件
            //    if (model.CloseDate != null)
            //    {
            //        sql += " AND CloseDate >= '" + model.CloseDate + "'";
            //    }
            //    return SqlHelper.ExecuteSql(sql);
            //}

            ///// <summary>
            ///// 根据用户ID获取用户信息
            ///// </summary>
            ///// <param name="UserId"></param>
            ///// <param name="ComPanyCD"></param>
            ///// <returns></returns>
            //public static DataTable GetUserInfoByID(string UserId, string ComPanyCD)
            //{
            //    string sql = "SELECT CompanyCD,UserID,UserName,EmployeeID,UsedStatus,LockFlag,OpenDate,CloseDate,ModifiedDate,ModifiedUserID";
            //    sql += " remark FROM officedba.UserInfo where UserID=@UserID and CompanyCD=@CompanyCD";
            //    SqlParameter[] param = new SqlParameter[2];
            //    param[0] = SqlHelper.GetParameter("@UserID", UserId);
            //    param[1] = SqlHelper.GetParameter("@ComPanyCD", ComPanyCD);
            //    return SqlHelper.ExecuteSql(sql, param);
            //}

            ///// <summary>
            ///// 获取用户数
            ///// </summary>
            ///// <param name="companyCD">公司代码</param>
            ///// <returns>用户信息</returns>
            //public static int GetCompanyUserCount(string companyCD)
            //{
            //    //查询语句
            //    string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD";
            //    //设置参数
            //    SqlParameter[] param = new SqlParameter[1];
            //    param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //    DataTable userCount = SqlHelper.ExecuteSql(select, param);
            //    if (userCount != null && userCount.Rows.Count > 0)
            //    {
            //        return (int)userCount.Rows[0][0];
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
            ///// <summary>
            ///// 判断用户ID是否已经存在
            ///// </summary>
            ///// <param name="companyCD">公司代码</param>
            ///// <returns>用户信息</returns>
            //public static int GetUserCount(string companyCD, string UserId)
            //{
            //    //查询语句
            //    string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD and UserID=@UserID";
            //    //设置参数
            //    SqlParameter[] param = new SqlParameter[2];
            //    param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //    param[1] = SqlHelper.GetParameter("@UserID", UserId);
            //    DataTable userCount = SqlHelper.ExecuteSql(select, param);
            //    if (userCount != null && userCount.Rows.Count > 0)
            //    {
            //        return (int)userCount.Rows[0][0];
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}

            ///// <summary>
            ///// 获取用户信息
            ///// </summary>
            ///// <param name="companyCD">公司代码</param>
            ///// <returns>用户信息</returns>
            //public static int GetCompanyMaxUserNum(string companyCD)
            //{
            //    //查询语句
            //    string select = "SELECT MaxUers FROM [pubdba].[companyOpenServ] WHERE CompanyCD = @CompanyCD";
            //    //设置参数
            //    SqlParameter[] param = new SqlParameter[1];
            //    param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //    DataTable userCount = SqlHelper.ExecuteSql(select, param);
            //    if (userCount != null && userCount.Rows.Count > 0)
            //    {
            //        if (userCount.Rows[0][0] != null)
            //        {
            //            return (int)userCount.Rows[0][0];
            //        }
            //        else
            //        {
            //            return 0;
            //        }
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}

            ///// <summary>
            ///// 用户信息更新或者插入
            ///// </summary>
            ///// <param name="model">用户信息</param>
            ///// <param name="loginUserID">登陆系统的用户ID</param>
            ///// <returns>更新成功与否</returns>
            //public static bool ModifyUserInfo(UserInfoModel model, string loginUserID)
            //{
            //    //SQL拼写
            //    StringBuilder sql = new StringBuilder();
            //    //追加的场合
            //    if (model.IsInsert)
            //    {
            //        sql.AppendLine("INSERT INTO officedba.UserInfo");
            //        sql.AppendLine("		(CompanyCD      ");
            //        sql.AppendLine("		,UserID         ");
            //        sql.AppendLine("		,UserName       ");
            //        sql.AppendLine("		,password       ");
            //        sql.AppendLine("		,LockFlag       ");
            //        sql.AppendLine("		,EmployeeID       ");
            //        sql.AppendLine("		,UsedStatus     ");
            //        sql.AppendLine("		,OpenDate       ");
            //        sql.AppendLine("		,CloseDate      ");
            //        sql.AppendLine("		,ModifiedDate   ");
            //        sql.AppendLine("		,ModifiedUserID ");
            //        sql.AppendLine("		,remark)        ");
            //        sql.AppendLine("VALUES                  ");
            //        sql.AppendLine("		(@CompanyCD     ");
            //        sql.AppendLine("		,@UserID        ");
            //        sql.AppendLine("		,@UserName      ");
            //        sql.AppendLine("		,@password      ");
            //        sql.AppendLine("		,@LockFlag      ");
            //        sql.AppendLine("		,@EmployeeID      ");
            //        sql.AppendLine("		,@UsedStatus      ");
            //        sql.AppendLine("		,@OpenDate      ");
            //        sql.AppendLine("		,@CloseDate     ");
            //        sql.AppendLine("		,@ModifiedDate  ");
            //        sql.AppendLine("		,@ModifiedUserID");
            //        sql.AppendLine("		,@remark)       ");
            //    }
            //    //更新的场合
            //    else
            //    {
            //        sql.AppendLine("UPDATE officedba.UserInfo		     ");
            //        sql.AppendLine("SET                                      ");
            //        sql.AppendLine("		UserName = @UserName             ");
            //        sql.AppendLine("		,LockFlag = @LockFlag            ");
            //        sql.AppendLine("		,EmployeeID = @EmployeeID        ");
            //        sql.AppendLine("		,UsedStatus = @UsedStatus        ");
            //        sql.AppendLine("		,OpenDate = @OpenDate            ");
            //        sql.AppendLine("		,CloseDate = @CloseDate          ");
            //        sql.AppendLine("		,ModifiedDate = @ModifiedDate    ");
            //        sql.AppendLine("		,ModifiedUserID = @ModifiedUserID");
            //        sql.AppendLine("		,remark = @remark                ");
            //        sql.AppendLine("WHERE                                    ");
            //        sql.AppendLine("		UserID = @UserID                 ");
            //        sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
            //    }
            //    //设置参数
            //    SqlParameter[] param;
            //    if (model.IsInsert)
            //    {
            //        param = new SqlParameter[12];
            //    }
            //    else
            //    {
            //        param = new SqlParameter[11];
            //    }

            //    param[0] = SqlHelper.GetParameter("@UserID", model.UserID);
            //    param[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            //    param[2] = SqlHelper.GetParameter("@UserName", model.UserName);
            //    param[3] = SqlHelper.GetParameter("@LockFlag", model.LockFlag);
            //    param[4] = SqlHelper.GetParameter("@OpenDate", model.OpenDate == null
            //                                 ? SqlDateTime.Null
            //                                 : SqlDateTime.Parse(model.OpenDate.ToString()));
            //    param[5] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
            //                                 ? SqlDateTime.Null
            //                                 : SqlDateTime.Parse(model.CloseDate.ToString()));
            //    param[6] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            //    param[7] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);
            //    param[8] = SqlHelper.GetParameter("@remark", model.Remark);
            //    param[9] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            //    param[10] = SqlHelper.GetParameter("@EmployeeID", model.EmployeeID == null
            //                                   ? SqlInt32.Null
            //                                   : SqlInt32.Parse(model.EmployeeID.ToString()));
            //    if (model.IsInsert)
            //    {
            //        param[11] = SqlHelper.GetParameter("@password", model.Password);
            //    }

            //    SqlHelper.ExecuteTransSql(sql.ToString(), param);
            //    return SqlHelper.Result.OprateCount > 0 ? true : false;
            //}

            ///// <summary>
            ///// 获取用户信息
            ///// </summary>
            ///// <param name="userID">用户ID</param>
            ///// <param name="companyCD">公司代码</param>
            ///// <returns>用户信息</returns>
            //public static bool DeleteUserInfo(string userID, string companyCD)
            //{
            //    //SQL拼写
            //    StringBuilder sql = new StringBuilder();
            //    sql.AppendLine("DELETE FROM officedba.UserInfo ");
            //    sql.AppendLine("WHERE ");
            //    sql.AppendLine("CompanyCD = '" + companyCD + "'");
            //    sql.AppendLine("AND UserID IN (" + userID + ")");

            //    SqlHelper.ExecuteTransSql(sql.ToString());
            //    return SqlHelper.Result.OprateCount > 0 ? true : false;
            //}
            ///// <summary>
            ///// 获取员工姓名
            ///// </summary>
            ///// <param name="companyCD"></param>
            ///// <returns></returns>
            //public static DataTable GetEmployeeInfo(string companyCD)
            //{
            //    string sql = "select b.ID,b.EmployeeName from officedba.EmployeeJob as a inner join officedba. EmployeeInfo as b on a.CompanyCD=b.CompanyCD  ";
            //    sql += " where a.Flag!=@Flag and a.EmployeesID=b.ID and a.JobNo=(select Max(JobNo)as maxnum from officedba.EmployeeJob )  ";
            //    SqlParameter[] parms = new SqlParameter[1];
            //    parms[0] = SqlHelper.GetParameter("@Flag", flag);
            //    return SqlHelper.ExecuteSql(sql, parms);
            //}


            ///// <summary>
            ///// 获取用户信息
            ///// </summary>
            ///// <param name="userID">用户ID</param>
            ///// <param name="companyCD">公司代码</param>
            ///// <returns>用户信息</returns>
            //public static DataTable GetUserInfo(string CompanyCD, string UserID)
            //{
            //    //SQL拼写
            //    string sql = "select UserID,UserName from officedba.UserInfo where CompanyCD=@CompanyCD";
            //    DataTable dt = null;
            //    if (!string.IsNullOrEmpty(UserID))
            //    {
            //        sql += " and UserID=@UserID ";
            //        SqlParameter[] parms = new SqlParameter[2];
            //        parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //        parms[1] = SqlHelper.GetParameter("@UserID", UserID);
            //        dt = SqlHelper.ExecuteSql(sql, parms);

            //    }
            //    else if (string.IsNullOrEmpty(UserID))
            //    {
            //        SqlParameter[] parms = new SqlParameter[1];
            //        parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //        dt = SqlHelper.ExecuteSql(sql, parms);
            //    }
            //    return dt;
            //}
            return null;
        }
    }
}

