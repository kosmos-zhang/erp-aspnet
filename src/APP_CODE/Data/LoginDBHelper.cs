/**********************************************
 * 类作用：   用户登陆
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data
{
    /// <summary>
    /// 类名：LoginDBHelper
    /// 描述：获取登陆用户的信息
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class LoginDBHelper
    {
        #region 获取登陆用户信息
        
        /// <summary>
        /// 获取登陆用户的信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>DataTable 登陆用户信息</returns>
        public static DataTable GetUserInfo(string UserID)
        {//检索菜单项SQL文

            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT								");
            searchSql.AppendLine("	isnull(C.EmployeeName,case isroot when '1' then '系统管理员' else ''end) AS UserName,");
            searchSql.AppendLine("	A.IsRoot,                         ");
            searchSql.AppendLine("    ISNULL(A.IsHardValidate,1) AS IsHardValidate,");//针对用户是否启用加密狗
            searchSql.AppendLine("	A.password,                         ");
            searchSql.AppendLine("	A.EmployeeID,                       ");
            searchSql.AppendLine("	A.LockFlag,                         ");
            searchSql.AppendLine("	A.LastLoginTime,                       ");
            searchSql.AppendLine("	B.CompanyCD,                        ");
            searchSql.AppendLine("	isnull(B.EnableUSBKEYLOGIN,1) as EnableUSBKEYLOGIN,                        ");
            searchSql.AppendLine("	B.OpenDate AS CompanyOpenDate,      ");
            searchSql.AppendLine("	B.CloseDate AS CompanyCloseDate,    ");
            searchSql.AppendLine("	A.OpenDate AS UserOpenDate,         ");
            searchSql.AppendLine("	A.CloseDate	AS UserCloseDate,       ");
            searchSql.AppendLine("	C.DeptID AS DeptID,                 ");
            searchSql.AppendLine("	C.EmployeeNum AS EmployeeNum,       ");
            searchSql.AppendLine("	C.EmployeeName AS EmployeeName,     ");
            searchSql.AppendLine("	C.QuarterID AS QuarterID,           ");
            searchSql.AppendLine("	isnull(D.DeptName,'') AS DeptName,             ");
            searchSql.AppendLine("	isnull(E.QuarterName,'') AS QuarterName        ");
            searchSql.AppendLine("FROM                                  ");
            searchSql.AppendLine("	officedba.UserInfo AS A             ");
            searchSql.AppendLine("	LEFT OUTER JOIN                     ");
            searchSql.AppendLine("	officedba.EmployeeInfo AS C         ");
            searchSql.AppendLine("	     ON A.EmployeeID = C.ID         ");

            searchSql.AppendLine("	LEFT OUTER JOIN                     ");
            searchSql.AppendLine("	officedba.DeptInfo AS D             ");
            searchSql.AppendLine("	      ON C.DeptID = D.ID            ");
            searchSql.AppendLine("	LEFT OUTER JOIN                     ");
            searchSql.AppendLine("	officedba.DeptQuarter AS E          ");
            searchSql.AppendLine("	      ON C.QuarterID = E.ID         ");

            searchSql.AppendLine("	,pubdba.CompanyOpenServ AS B        ");
            searchSql.AppendLine("WHERE                                 ");
            searchSql.AppendLine("	A.CompanyCD = B.CompanyCD           ");
            searchSql.AppendLine("	AND A.UserID = @UserID              ");

            //设置参数
            SqlParameter[] p = new SqlParameter[1];
            p[0] = SqlHelper.GetParameter("@UserID", UserID);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

        #region 获取部门角色信息

        /// <summary>
        /// 获取部门角色信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门角色信息</returns>
        public static DataTable GetRoleInfo(string userID, string companyCD)
        {
            //检索菜单项SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT ");
            searchSql.AppendLine(" RoleID ");
            searchSql.AppendLine(" FROM officedba.UserRole ");
            searchSql.AppendLine(" WHERE ");
            searchSql.AppendLine(" CompanyCD = @CompanyCD ");
            searchSql.AppendLine(" AND UserID = @UserID ");

            //设置参数
            SqlParameter[] p = new SqlParameter[2];
            int i = 0;
            //公司代码
            p[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //用户ID
            p[i++] = SqlHelper.GetParameter("@UserID", userID);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

        #region 更新用户删除标识
        /// <summary>
        /// 更新用户删除标识
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门角色信息</returns>
        public static void UpdateUserFlag(string userID, string companyCD)
        {
            //检索菜单项SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" UPDATE officedba.UserInfo ");
            searchSql.AppendLine(" SET LastLoginTime = @LastLoginTime ");
            searchSql.AppendLine(" WHERE ");
            searchSql.AppendLine(" CompanyCD = @CompanyCD ");
            searchSql.AppendLine(" AND UserID = @UserID ");

            //设置参数
            SqlParameter[] p = new SqlParameter[3];
            int i = 0;
            //公司代码
            p[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //用户ID
            p[i++] = SqlHelper.GetParameter("@UserID", userID);
            p[i++] = SqlHelper.GetParameter("@LastLoginTime", System.DateTime.Now);
            SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }
        #endregion

        /// <summary>
        /// 读取企业的USBKEY列表
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetUSBKEYListByCompnayCD(string companyCD)
        {
            //检索菜单项SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT * FROM pubdba.CompanyUSBKEY ");           
            searchSql.AppendLine(" WHERE ");
            searchSql.AppendLine(" CompanyCD = @CompanyCD ");
          
            //设置参数
            SqlParameter[] p = new SqlParameter[1];
            
            //公司代码
            p[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
         
            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

    }
}
