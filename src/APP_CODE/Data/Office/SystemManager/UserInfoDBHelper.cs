/**********************************************
 * 类作用：   用户管理数据库层处理
 * 建立人：   吴志强
 * 建立时间： 2009/01/10
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
namespace XBase.Data.Office.SystemManager
{
    /// <summary>
    /// 类名：UserInfoDBHelper
    /// 描述：用户管理数据库层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    public class UserInfoDBHelper
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfo(UserInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //SQL拼写
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string sql = "select   ISNULL(IsHardValidate,1) AS IsHardValidate, isnull(UserID,'')as UserID,'' as UserName,isnull(IsRoot,0) as IsRoot,isnull(LastLoginTime,'') as LastLoginTime,"
                + "   isnull( CONVERT(CHAR(19), OpenDate, 120),'') as OpenDate ,"
                + " isnull( CONVERT(CHAR(10), CloseDate, 23),'') as CloseDate ,"
                + "Case WHEN LockFlag = '1' THEN '是' ELSE '否' END AS LockFlag ,Case WHEN UsedStatus = '1' THEN '启用' ELSE '停用' END AS UsedStatus,isnull( CONVERT(CHAR(19), ModifiedDate, 120),'') as ModifiedDate,ModifiedUserID,isnull(remark,'')as remark FROM officedba.UserInfo WHERE CompanyCD = '" + companyCD + "' and IsRoot!='1'";
            if (!string.IsNullOrEmpty(model.UserID))
            {
                sql += " AND UserID LIKE @UserID";
            }
            if (!string.IsNullOrEmpty(model.LockFlag))
            {
                if (model.LockFlag == "1")
                {
                    sql += " AND LockFlag = '1'";
                }
                else
                {
                    sql += " AND LockFlag = '0'";
                }
            }

            if (model.EmployeeID != 0)
            {
                sql += " AND EmployeeID = @EmployeeID";
            }
            //开始时间输入的场合，添加为条件
            if (model.OpenDate != null && model.CloseDate != null)
            {
                sql += " AND OpenDate >= @OpenDate  AND CloseDate<=@CloseDate ";
            }
            else
            {
                if (model.OpenDate != null)
                {
                    sql += " AND OpenDate=@OpenDate";
                }
                //结束时间输入的场合，添加为条件 
                if (model.CloseDate != null)
                {
                    sql += " AND CloseDate=@CloseDate";
                }
            }
            if (model.IsHardValidate != null)
            {
                if (model.IsHardValidate == "1")
                    sql += " AND ( IsHardValidate='1' OR IsHardValidate IS NULL  )";
                else
                    sql += "AND IsHardValidate='0' "; 
            }

           
            SqlParameter[] param = new SqlParameter[5];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UserID", "%" + model.UserID + "%");
            param[2] = SqlHelper.GetParameter("@EmployeeID", model.EmployeeID);
            param[3] = SqlHelper.GetParameter("@OpenDate", model.OpenDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.OpenDate.ToString()));
            param[4] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.CloseDate.ToString()));
            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
            return dt;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ComPanyCD"></param>
        /// <returns></returns>
        public static DataTable GetUserInfoByID(string UserId, string ComPanyCD)
        {
            string sql = "SELECT  ISNULL(IsHardValidate,1) AS IsHardValidate,CompanyCD,UserID,'' as UserName,EmployeeID,password,UsedStatus,LockFlag,OpenDate,CloseDate,ModifiedDate,ModifiedUserID,isnull(IsRoot,0) as IsRoot,";
                sql+=" remark FROM officedba.UserInfo where UserID=@UserID and CompanyCD=@CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@UserID", UserId);
                param[1] = SqlHelper.GetParameter("@ComPanyCD", ComPanyCD);
                return SqlHelper.ExecuteSql(sql,param);
        }

        /// <summary>
        /// 获取用户数
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetCompanyUserCount(string companyCD)
        {
            //查询语句
            string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD and IsRoot!='1' ";
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                return (int)userCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 判断用户ID是否已经存在
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetUserCount(string companyCD,string UserId)
        {
            //查询语句
            string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD and UserID=@UserID";
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UserID", UserId);
            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                return (int)userCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetCompanyMaxUserNum(string companyCD)
        {
            //查询语句
            string select = "SELECT MaxUers FROM [pubdba].[companyOpenServ] WHERE CompanyCD = @CompanyCD";
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                if (userCount.Rows[0][0] != null)
                {
                    return (int)userCount.Rows[0][0];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 用户信息更新或者插入
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <param name="loginUserID">登陆系统的用户ID</param>
        /// <returns>更新成功与否</returns>
        public static bool ModifyUserInfo(UserInfoModel model, string loginUserID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            //追加的场合
            if (model.IsInsert)
            {
                sql.AppendLine("INSERT INTO officedba.UserInfo");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,UserID         ");
              // sql.AppendLine("		,UserName       ");
                sql.AppendLine("		,password       ");
                sql.AppendLine("		,LockFlag       ");
                sql.AppendLine("		,EmployeeID       ");
                sql.AppendLine("		,UsedStatus     ");
                sql.AppendLine("		,OpenDate       ");
                sql.AppendLine("		,CloseDate      ");
                sql.AppendLine("		,ModifiedDate   ");
                sql.AppendLine("		,ModifiedUserID ");
                sql.AppendLine("		,remark      ");
                sql.AppendLine(",IsHardValidate)");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD     ");
                sql.AppendLine("		,@UserID        ");
           //     sql.AppendLine("		,@UserName      ");
                sql.AppendLine("		,@password      ");
                sql.AppendLine("		,@LockFlag      ");
                sql.AppendLine("		,@EmployeeID      ");
                sql.AppendLine("		,@UsedStatus      ");
                sql.AppendLine("		,@OpenDate      ");
                sql.AppendLine("		,@CloseDate     ");
                sql.AppendLine("		,@ModifiedDate  ");
                sql.AppendLine("		,@ModifiedUserID");
                sql.AppendLine("		,@remark       ");
                sql.AppendLine(",@IsHardValidate)");
            }
            //更新的场合
            else
            {
                sql.AppendLine("UPDATE officedba.UserInfo		     ");
                sql.AppendLine("SET                                      ");
             //   sql.AppendLine("		UserName = @UserName             ");
                sql.AppendLine("		LockFlag = @LockFlag            ");
                sql.AppendLine("		,EmployeeID = @EmployeeID        ");
                sql.AppendLine("		,UsedStatus = @UsedStatus        ");
                sql.AppendLine("		,OpenDate = @OpenDate            ");
                sql.AppendLine("		,CloseDate = @CloseDate          ");
                sql.AppendLine("		,ModifiedDate = @ModifiedDate    ");
                sql.AppendLine("		,ModifiedUserID = @ModifiedUserID");
                sql.AppendLine("		,remark = @remark ,IsHardValidate=@IsHardValidate               ");
                sql.AppendLine("WHERE                                    ");
                sql.AppendLine("		UserID = @UserID                 ");
                sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
            }
            //设置参数
            SqlParameter[] param;
            if (model.IsInsert){
                param = new SqlParameter[12];
            }
            else
            {
                param = new SqlParameter[11];
            }
            
            param[0] = SqlHelper.GetParameter("@UserID", model.UserID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
           // param[2] = SqlHelper.GetParameter("@UserName", model.UserName);
            param[2] = SqlHelper.GetParameter("@LockFlag", model.LockFlag);
            param[3] = SqlHelper.GetParameter("@OpenDate", model.OpenDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.OpenDate.ToString()));
            param[4] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.CloseDate.ToString()));
            param[5] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);
            param[7] = SqlHelper.GetParameter("@remark", model.Remark);
            param[8] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[9] = SqlHelper.GetParameter("@EmployeeID", model.EmployeeID==null
                                           ?SqlInt32.Null
                                           :SqlInt32.Parse(model.EmployeeID .ToString()));
            param[10] = SqlHelper.GetParameter("@IsHardValidate", model.IsHardValidate);
            if (model.IsInsert)
            {
                param[11] = SqlHelper.GetParameter("@password", model.Password);
            }            

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="psd"></param>
        /// <returns></returns>
        public static bool ModifyUserInfoPwd(string userID, string psd, string CompanyCD)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            //追加的场合
          
                sql.AppendLine("UPDATE officedba.UserInfo		     ");
                sql.AppendLine("SET                                      ");
                sql.AppendLine("		password = @password             ");
                sql.AppendLine("WHERE                                    ");
                sql.AppendLine("		UserID = @UserID                 ");
                sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
                SqlParameter[] param = param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@password", psd);
                param[1] = SqlHelper.GetParameter("@UserID", userID);
                param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        public static bool ModifyUserInfoPwdLog(string userID, string psd, string CompanyCD,string SessionUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.UserInfo		     ");
            sql.AppendLine("SET                                      ");
            sql.AppendLine("		ModifiedDate = @ModifiedDate    ");
            sql.AppendLine("		,ModifiedUserID = @ModifiedUserID");
            sql.AppendLine("WHERE                                    ");
            sql.AppendLine("		UserID = @UserID                 ");
            sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
            SqlParameter[] param = param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[1] = SqlHelper.GetParameter("@ModifiedUserID", SessionUserID);
            param[2] = SqlHelper.GetParameter("@UserID", userID);
            param[3] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static bool DeleteUserInfo(string userID, string companyCD)
        {
           string allUserID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] UserIdS = null;
               userID = userID.Substring(0, userID.Length);
               UserIdS = userID.Split(',');

               for (int i = 0; i < UserIdS.Length; i++)
               {
                   UserIdS[i] = "'" + UserIdS[i] + "'";
                   sb.Append(UserIdS[i]);
               }
               allUserID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.UserInfo WHERE UserID IN (" + allUserID + ") and CompanyCD = @CompanyCD";
               SqlCommand comm = new SqlCommand();
               comm.CommandText = Delsql[0].ToString();
               //设置参数
               comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
               ArrayList lstDelete = new ArrayList();
               lstDelete.Add(comm);

               SqlCommand sqlcomm = new SqlCommand();
               sqlcomm.CommandText = "DELETE FROM officedba.UserRole WHERE UserID IN (" + allUserID + ") and CompanyCD = @CompanyID";
               sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyID", companyCD));
               lstDelete.Add(sqlcomm);
               //添加基本信息更新命令
               return SqlHelper.ExecuteTransWithArrayList(lstDelete);
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }
        /// <summary>
        /// 获取员工姓名
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeInfo(string companyCD)
        {
            string sql = "select b.ID,b.EmployeeName,b.EmployeeNo from officedba.EmployeeInfo as b where b.CompanyCD=@companyCD and b.Flag='1' ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@companyCD", companyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }


        /// <summary>
        /// 获取用户信息 
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfo(string CompanyCD, string UserID)
        {
            //SQL拼写
            string sql = "select UserID,'' as UserName from officedba.UserInfo where CompanyCD=@CompanyCD and IsRoot!='1'";
            DataTable dt=null;
            if (!string.IsNullOrEmpty(UserID))
            {
                 sql += " and UserID=@UserID ";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                parms[1] = SqlHelper.GetParameter("@UserID", UserID);
                dt=SqlHelper.ExecuteSql(sql, parms);
                
            }
            else if (string.IsNullOrEmpty(UserID))
            {
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                dt= SqlHelper.ExecuteSql(sql, parms);
            }
            return dt;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static DataTable GetUserList()
        {
            string sql = "select * from officedba.UserInfo";
            return SqlHelper.ExecuteSql(sql,new SqlParameter[0]);
        }

        public static DataTable GetUserList(bool flag)
        {
            string sql = string.Empty;
            sql = "select * from officedba.UserInfo where isroot=0";
            return SqlHelper.ExecuteSql(sql, new SqlParameter[0]);
        }

        public static bool InsertPasswordHistory(string CompanyCD, string UserID,string password,string ModifiedUserID)
        {
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine("INSERT INTO officedba.PasswordHistory ");
            sqlInsert.AppendLine("           (CompanyCD, UserID    ");
            sqlInsert.AppendLine("		   	  , password            ");
            sqlInsert.AppendLine("           , ModifiedDate, ModifiedUserID)");
            sqlInsert.AppendLine("     VALUES                      ");
            sqlInsert.AppendLine("           (@CompanyCD           ");
            sqlInsert.AppendLine("           ,@UserID              ");
            sqlInsert.AppendLine("           ,@password            ");
            sqlInsert.AppendLine("           ,@ModifiedDate          ");
            sqlInsert.AppendLine("           ,@ModifiedUserID)            ");

            //设置参数
            SqlParameter[] param = new SqlParameter[5];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //操作用户ID
            param[i++] = SqlHelper.GetParameter("@UserID", UserID);
            //操作模块ID
            param[i++] = SqlHelper.GetParameter("@password", password);
            //操作单据编号
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            //操作对象
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);
            //执行插入
            return SqlHelper.ExecuteTransSql(sqlInsert.ToString(), param) > 0 ? true : false;
        }

        /// <summary>
        /// 验证公司是否启用USBKEY设备
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsOpenValidateByCompany(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT ISNULL(EnableUSBKEYLOGIN,1) AS EnableUSBKEYLOGIN FROM pubdba.CompanyOpenServ WHERE CompanyCD='" + CompanyCD+"'");

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString());
            if (dt == null || dt.Rows.Count < 0)
                return true;
            else
            {
                if (dt.Rows[0]["EnableUSBKEYLOGIN"].ToString() == "True")
                    return true;
                else
                    return false;
            }
        }



    }
}
