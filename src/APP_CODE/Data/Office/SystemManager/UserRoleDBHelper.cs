/**********************************************
 * 类作用：   角色与用户关联数据库层处理
 * 建立人：  吴成好 EditBy 陶春
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
namespace XBase.Data.Office.SystemManager
{
    public class UserRoleDBHelper
    {
        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetUserRoleWithUserID(string UserID)
        {
            string sql = "SELECT officedba.UserRole.RoleID FROM officedba.UserRole WHERE UserID =@UserID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@UserID", UserID);
            return SqlHelper.ExecuteSql(sql,param);
        }
        /// <summary>
        /// 根据查询条件返回结果集
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static DataTable GetUserRoleByConditions(string CompanyCD, string RoleID, string UserID, string UserName, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string sql = "select UserID,isnull(RoleName,'')as RoleName ,isnull(RoleID,'')as RoleID,isnull( CONVERT(CHAR(10), ModifiedDate, 23),'') as ModifiedDate,isnull(ModifiedUserID,'')ModifiedUserID from officedba.V_UserRole ";
            sql += " where  UserID<>'' and (IsRoot!='1'or IsRoot is null) ";
            if (CompanyCD != "" && CompanyCD != null)
            {
                sql += " and CompanyCD=@CompanyCD";
            }
            if (RoleID != null && RoleID != "" && RoleID != "0")
            {
                sql += " and RoleID=@RoleID";
            }
            if (UserID != "" && UserID != null && UserID != "0")
            {
                sql += " and UserID like @UserID";
            }
            if (UserName != "" && UserName != null)
            {
                sql += " and UserName like @UserName";
            }
            SqlParameter[] param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameter("@CompanyCD",CompanyCD);
            param[1] = SqlHelper.GetParameter("@RoleID", RoleID);
            param[2] = SqlHelper.GetParameter("@UserID", "%" + UserID + "%");
            param[3] = SqlHelper.GetParameter("@UserName", "%" + UserName + "%");
            //DataTable dt = SqlHelper.ExecuteSql(sql, param);
            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
           return  dt;
        }
        /// <summary>
        /// 根据用户名ID获取角色信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable GetUserRoleInfo(string UserID)
        {
            string sql = " select UserID,RoleName,RoleID from officedba.V_UserRole where UserID=@UserID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@UserID", UserID);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        //public static DataTable GetUserRole(string CompanyCD)
        //{
        //    string sql = "SELECT dbo.UserRole.ModifiedDate, dbo.UserRole.ModifiedUserID, dbo.UserRole.UserID, dbo.UserRole.RoleID, dbo.UserRole.CompanyCD,dbo.RoleInfo.RoleName, dbo.RoleInfo.remark FROM dbo.RoleInfo INNER JOIN dbo.UserRole ON dbo.RoleInfo.CompanyCD = dbo.UserRole.CompanyCD AND dbo.RoleInfo.RoleID = dbo.UserRole.RoleID and dbo.UserRole.CompanyCD='" + CompanyCD + "'";
        //    return SqlHelper.ExecuteSql(sql);
        //}

        /// <summary>
        /// 删除角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool DeleteUserRole(string UserID, string RoleID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string allID = "";
            string allRoleId = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringBuilder sbid = new StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                string[] IdSL = null;
                UserID = UserID.Substring(0, UserID.Length);
                IdS = UserID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                RoleID = RoleID.Substring(0, RoleID.Length);
                IdSL = RoleID.Split(',');
                for (int j = 0; j < IdSL.Length; j++)
                {
                    IdSL[j] = "'" + IdSL[j] + "'";
                    sbid.Append(IdSL[j]);
                }
                allRoleId = sbid.ToString().Replace("''", "','");
                Delsql[0] = "delete from  officedba.UserRole where UserID IN (" + allID + ") and RoleID IN (" + allRoleId + ") and CompanyCD=@CompanyCD";
                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();
                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }




            //string sql = "delete from officedba.UserRole where UserID=@UserID and RoleID=@RoleID";
            //SqlParameter[] param = new SqlParameter[2];
            //param[0] = SqlHelper.GetParameter("@UserID", UserID);
            //param[1] = SqlHelper.GetParameter("@RoleID", RoleID);
            //SqlHelper.ExecuteTransSql(sql,param);
            //return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 添加角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertUserRole(UserRoleModel model, string loginUserID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO dbo.UserRole");
            sql.AppendLine("		(CompanyCD      ");
            sql.AppendLine("		,UserID         ");
            sql.AppendLine("		,RoleID         ");
            sql.AppendLine("		,ModifiedDate         ");
            sql.AppendLine("		,ModifiedUserID)        ");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@UserID        ");
            sql.AppendLine("		,@RoleID        ");
            sql.AppendLine("		,getdate()        ");
            sql.AppendLine("		,'" + loginUserID + "')       ");

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@UserID", model.UserID);
            param[2] = SqlHelper.GetParameter("@RoleID", model.RoleID);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }

        /// <summary>
        /// 添加角色记录
        /// </summary>
        /// <param name="userID">追加的用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="role">用户角色</param>
        /// <param name="loginUserID">登陆系统用户ID</param>
        /// <returns></returns>
        public static bool InsertUserRoleWithList(string userID, string companyCD, string[] role, string loginUserID)
        {
            //执行SQL拼写
            string[] executeSQL = new string[role.Length+1];
            //删除SQL拼写
            string deleteSQL = "DELETE FROM officedba.UserRole WHERE UserID = @UserID";            
            //追加SQL拼写
            string insertSQL1 = "INSERT INTO officedba.UserRole(CompanyCD,UserID,RoleID,ModifiedDate,ModifiedUserID) VALUES(@companyCD,@UserID,@roleid ,getdate(),@loginUserID)";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSQL;
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@UserID", userID));
            ArrayList lstDelete = new ArrayList();
            lstDelete.Add(comm);
            for (int i = 0; i < role.Length; i++)
            {
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@companyCD", companyCD));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@UserID", userID));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@roleid", role[i]));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@loginUserID", loginUserID));
                sqlcomm.CommandText = insertSQL1;
                lstDelete.Add(sqlcomm);
            }

         
            //sqlcomm.CommandText = "DELETE FROM officedba.UserRole WHERE UserID IN (" + allUserID + ") and CompanyCD = @CompanyID";
            //sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyID", companyCD));
            //lstDelete.Add(sqlcomm);
            //添加基本信息更新命令
            return SqlHelper.ExecuteTransWithArrayList(lstDelete);


            //executeSQL[0] = deleteSQL;
            //for(int i=0;i<role.Length;i++)
            //{
            //    executeSQL[i+1] = insertSQL1+","+role[i]+insertSQL2;
            //}
            //return SqlHelper.ExecuteTransForListWithSQL(executeSQL);

        }

        /// <summary>
        /// 修改角色记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool UpdateUserRole(UserRoleModel model, string loginUserID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.UserRole			     ");
            sql.AppendLine("SET                                      ");
            sql.AppendLine("		RoleID = @RoleID             ");
            sql.AppendLine("		,ModifiedDate = getdate()                ");
            sql.AppendLine("		,ModifiedUserID = '" + loginUserID + "'                ");
            sql.AppendLine("WHERE                                    ");
            sql.AppendLine("		UserID = @UserID                ");
            sql.AppendLine("		AND CompanyCD = @CompanyCD       ");

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@UserID", model.UserID);
            param[2] = SqlHelper.GetParameter("@RoleID", model.RoleID);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }


    }
}
