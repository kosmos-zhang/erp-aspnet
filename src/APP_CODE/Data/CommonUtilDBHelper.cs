/**********************************************
 * 类作用：   Common处理数据库访问类
 * 建立人：   吴志强
 * 建立时间： 2008/12/30
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

using System.Collections;

namespace XBase.Data
{
    /// <summary>
    /// 类名：CommonUtilDBHelper
    /// 描述：提供Common处理数据库访问一些方法
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class CommonUtilDBHelper
    {
        /// <summary>
        /// InitMenuDataSimple
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static ArrayList InitMenuDataSimple(string userID, string companyCD)
        {
            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT  DISTINCT 						 ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ModuleID) AS ModuleID,");
            searchSql.AppendLine("	E.ModuleName,						 ");
            searchSql.AppendLine("	E.ModuleType,	                     ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ParentID) AS ParentID,");
            searchSql.AppendLine("	E.PropertyType,	                     ");
            searchSql.AppendLine("	E.PropertyValue,	                  ");
            searchSql.AppendLine("	E.ImgPath,                          ");
            searchSql.AppendLine("	E.DefaultPage	                     ");
            searchSql.AppendLine("FROM		                             ");
            searchSql.AppendLine("	officedba.UserInfo A,	             ");
            searchSql.AppendLine("	officedba.UserRole B,                ");
            searchSql.AppendLine("	officedba.RoleFunction C,	         ");
            searchSql.AppendLine("	pubdba.CompanyModule D	INNER JOIN   ");
            searchSql.AppendLine("	pubdba.SysModule AS E ON             ");
            searchSql.AppendLine("	D.ModuleID = E.ModuleID              ");
            searchSql.AppendLine("WHERE                                  ");
            searchSql.AppendLine("	C.ModuleID = D.ModuleID              ");
            searchSql.AppendLine("	AND A.CompanyCD = D.CompanyCD        ");
            searchSql.AppendLine("	AND B.RoleID = C.RoleID	             ");
            searchSql.AppendLine("	AND B.CompanyCD = C.CompanyCD	     ");
            searchSql.AppendLine("	AND A.UserID = B.UserID	             ");
            searchSql.AppendLine("	AND A.CompanyCD = B.CompanyCD	     ");
            searchSql.AppendLine("	AND A.CompanyCD = @CompanyCD         ");
            searchSql.AppendLine("	AND A.UserID = @UserID				 ");
            searchSql.AppendLine("ORDER BY		                         ");
            searchSql.AppendLine("	ModuleID                             ");

            SqlCommand cmd =new SqlCommand();
            cmd.CommandText = searchSql.ToString();

            SqlParameter[] p = new SqlParameter[2];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            p[i++] = SqlHelper.GetParameter("@UserID", userID);

            return SqlHelper.SpecailExecuteList(cmd, p);
        }

        #region 获得用户可操作菜单数据
        
        /// <summary>
        /// 获得用户可操作菜单数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回用户可操作的菜单集。</returns>
        public static DataTable GetMenuData(string UserID, string CompanyCD)
        {
            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT  DISTINCT 						 ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ModuleID) AS ModuleID,");
            searchSql.AppendLine("	E.ModuleName,						 ");
            searchSql.AppendLine("	E.ModuleType,	                     ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ParentID) AS ParentID,");
            searchSql.AppendLine("	E.PropertyType,	                     ");
            searchSql.AppendLine("	E.PropertyValue,	                  ");
            searchSql.AppendLine("	E.ImgPath,                          ");
            searchSql.AppendLine("	E.DefaultPage	                     ");
            searchSql.AppendLine("	FROM  ");            	             
            searchSql.AppendLine("	officedba.UserRole B,       ");          
            searchSql.AppendLine("	officedba.RoleFunction C, ");
            searchSql.AppendLine("	pubdba.CompanyModule AS D ,         ");     
            searchSql.AppendLine("	pubdba.SysModule AS E          ");     
            searchSql.AppendLine("	WHERE  ");
            searchSql.AppendLine("	B.UserID = @UserID				 "); 
            searchSql.AppendLine("	AND B.CompanyCD = @CompanyCD    ");
            searchSql.AppendLine("	AND B.RoleID = C.RoleID	          ");    
            searchSql.AppendLine("	AND B.CompanyCD = C.CompanyCD         ");
            searchSql.AppendLine("	AND C.ModuleID = D.ModuleID and c.CompanyCD = d.CompanyCD        ");
            searchSql.AppendLine("	AND D.ModuleID = E.ModuleID         ");      
            searchSql.AppendLine("	ORDER BY		          ");
            searchSql.AppendLine("	ModuleID  ");

            //E.PropertyValue IS NULL and		                 

            SqlParameter[] p = new SqlParameter[2];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            p[i++] = SqlHelper.GetParameter("@UserID", UserID);

           // System.Collections.ArrayList datalist = SqlHelper.SpecailExecuteList(

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }


        public static DataTable GetMenuData(string UserID, string CompanyCD, string filterID)
        {
           

            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();

            searchSql.AppendLine("SELECT  DISTINCT 						 ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ModuleID) AS ModuleID,");
            searchSql.AppendLine("	E.ModuleName,						 ");
            searchSql.AppendLine("	E.ModuleType,	                     ");
            searchSql.AppendLine("	CONVERT(VARCHAR,E.ParentID) AS ParentID,");
            searchSql.AppendLine("	E.PropertyType,	                     ");
            searchSql.AppendLine("	E.PropertyValue,	                  ");
            searchSql.AppendLine("	E.ImgPath,                          ");
            searchSql.AppendLine("	E.DefaultPage	                     ");
            searchSql.AppendLine("	FROM  ");
            searchSql.AppendLine("	officedba.UserRole B,       ");
            searchSql.AppendLine("	officedba.RoleFunction C, ");
            searchSql.AppendLine("	pubdba.CompanyModule AS D ,         ");     
            searchSql.AppendLine("	pubdba.SysModule AS E          ");
            searchSql.AppendLine("	WHERE  ");
            searchSql.AppendLine("	B.UserID = @UserID				 ");
            searchSql.AppendLine("	AND B.CompanyCD = @CompanyCD    ");
            searchSql.AppendLine("	AND B.RoleID = C.RoleID	          ");
            searchSql.AppendLine("	AND B.CompanyCD = C.CompanyCD         ");
            searchSql.AppendLine("	AND C.ModuleID = D.ModuleID         ");
            searchSql.AppendLine("	AND D.ModuleID = E.ModuleID         ");      
            searchSql.AppendLine("	and " + filterID + "       ");
            searchSql.AppendLine("	ORDER BY		          ");
            searchSql.AppendLine("	ModuleID  ");


            //E.PropertyValue IS NULL and		                 

            SqlParameter[] p = new SqlParameter[2];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            p[i++] = SqlHelper.GetParameter("@UserID", UserID);

           // System.Collections.ArrayList datalist = SqlHelper.SpecailExecuteList(

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

        #region 获得用户每个页面可操作的业务数据
        
        /// <summary>
        /// 获得用户每个页面可操作的业务数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回用户可操作的页面业务集。</returns>
        public static DataTable GetPageAuthority(string UserID, string CompanyCD)
        {
            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT    DISTINCT              ");
            searchSql.AppendLine("	D.ModuleID,	                  ");
            searchSql.AppendLine("	D.FunctionID,	                ");
            searchSql.AppendLine("	D.FunctionCD	                ");
            searchSql.AppendLine("FROM		                        ");            
            searchSql.AppendLine("	officedba.UserRole B,	        ");
            searchSql.AppendLine("	officedba.RoleFunction C,	    ");
            searchSql.AppendLine("	pubdba.ModuleFunction D         ");
            searchSql.AppendLine("WHERE		                        ");
            searchSql.AppendLine("	B.CompanyCD = @CompanyCD    ");
            searchSql.AppendLine("	AND B.UserID = @UserID          ");            
            searchSql.AppendLine("	AND B.RoleID = C.RoleID	        ");
            searchSql.AppendLine("	AND B.CompanyCD = C.CompanyCD	");
            searchSql.AppendLine("	AND C.FunctionID = D.FunctionID	 ");
            searchSql.AppendLine("	AND C.ModuleID = D.ModuleID	    ");
            searchSql.AppendLine("ORDER BY		                    ");
            searchSql.AppendLine("	D.ModuleID,	                    ");
            searchSql.AppendLine("	D.FunctionID,	                ");
            searchSql.AppendLine("	D.FunctionCD	                ");

            SqlParameter[] p = new SqlParameter[2];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            p[i++] = SqlHelper.GetParameter("@UserID", UserID);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }


        /// <summary>
        /// 获得用户每个页面可操作的业务数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回用户可操作的页面业务集。</returns>
        public static DataTable GetPageAuthority(string UserID, string CompanyCD,string ModuleID)
        {
            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT   DISTINCT               ");
            searchSql.AppendLine("	D.ModuleID,	                  ");
            searchSql.AppendLine("	D.FunctionID,	                ");
            searchSql.AppendLine("	D.FunctionCD	                ");
            searchSql.AppendLine("FROM		                        ");            
            searchSql.AppendLine("	officedba.UserRole B,	        ");
            searchSql.AppendLine("	officedba.RoleFunction C,	    ");
            searchSql.AppendLine("	pubdba.ModuleFunction D         ");
            searchSql.AppendLine("WHERE		                        ");
            searchSql.AppendLine("	B.CompanyCD = @CompanyCD    ");
            searchSql.AppendLine("	AND B.UserID = @UserID          ");            
            searchSql.AppendLine("	AND B.RoleID = C.RoleID	        ");
            searchSql.AppendLine("	AND B.CompanyCD = C.CompanyCD	");                                    
            searchSql.AppendLine("	AND C.ModuleID = @ModuleID      ");
            searchSql.AppendLine("	AND C.FunctionID = D.FunctionID	    ");
            searchSql.AppendLine("	AND C.ModuleID = D.ModuleID	    ");            
            searchSql.AppendLine("ORDER BY		                    ");
            searchSql.AppendLine("	D.ModuleID,	                    ");
            searchSql.AppendLine("	D.FunctionID,	                ");
            searchSql.AppendLine("	D.FunctionCD	                ");

            SqlParameter[] p = new SqlParameter[3];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            p[i++] = SqlHelper.GetParameter("@UserID", UserID);
            p[i++] = SqlHelper.GetParameter("@ModuleID", ModuleID);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

        #region 获取企业文化分类内容

        /// <summary>
        /// 获得用户每个页面可操作的业务数据
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>返回用户可操作的页面业务集。</returns>
        public static DataTable GetCultureInfo(string companyCD)
        {
            //查询用SQL文
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT                  ");
            searchSql.AppendLine("	ID                    ");
            searchSql.AppendLine("	,TypeName             ");
            searchSql.AppendLine("	,SupperTypeID         ");
            searchSql.AppendLine("FROM                    ");
            searchSql.AppendLine("	officedba.CultureType ");
            searchSql.AppendLine("WHERE                   ");
            searchSql.AppendLine("	CompanyCD = @CompanyCD");

            SqlParameter[] p = new SqlParameter[1];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

        #region 企业分配模块
        public static DataTable GetCompanyModule(string CompanyCD)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.* from                                                                                       ");
            searchSql.AppendLine("	pubdba.SysModule a right join pubdba.CompanyModule b on a.ModuleID=b.ModuleID	                    ");
            searchSql.AppendLine("where b.CompanyCD=@CompanyCD                                                                          ");
         

            SqlParameter[] p = new SqlParameter[1];
            int i = 0;
            p[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }
        #endregion

    }
}
