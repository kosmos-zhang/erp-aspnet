/**********************************************
 * 类作用：   组织机构图
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
   * 修改人：   王保军
 * 建立时间： 2009/08/27
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：DeptQuarterEmployeeDBHelper
    /// 描述：组织机构图
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class DeptQuarterEmployeeDBHelper
    {
        #region 通过公司代码查询机构信息
        /// <summary>
        /// 通过公司代码查询机构信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetDeptInfoWithCompanyCD(string companyCD)
        {
            #region 查询机构信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 ID  AS ID                   ");
            searchSql.AppendLine(" 	,DeptNO                      ");
            searchSql.AppendLine(" 	,SuperDeptID AS SuperID      ");
            searchSql.AppendLine(" 	,DeptName    AS DisplayName  ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.DeptInfo           ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");
            searchSql.AppendLine(" 	AND UsedStatus = @UsedStatus ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        #endregion

        #region 通过公司代码查询岗位信息
        /// <summary>
        /// 通过公司代码查询岗位信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetQuarterInfoWithCompanyCD(string companyCD)
        {
            #region 查询岗位信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 ID   AS  ID                 ");
            searchSql.AppendLine(" 	,DeptID                      ");
            searchSql.AppendLine(" 	,SuperQuarterID AS SuperID   ");
            searchSql.AppendLine(" 	,QuarterName AS DisplayName  ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.DeptQuarter        ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD       ");
            searchSql.AppendLine(" 	AND UsedStatus = @UsedStatus ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //机构ID
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", ConstUtil.USED_STATUS_ON);

            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 查询员工信息
        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetEmployeeInfoWithCompanyCD(string companyCD)
        {
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                                ");
            searchSql.AppendLine(" 	 A.ID                                ");
            searchSql.AppendLine(" 	,A.EmployeeNo                        ");
            searchSql.AppendLine(" 	,A.EmployeeNum                       ");
            searchSql.AppendLine(" 	,A.CompanyCD                         ");
            searchSql.AppendLine(" 	,A.EmployeeName                      ");
            searchSql.AppendLine(" 	,A.QuarterID                         ");
            searchSql.AppendLine(" 	,C.QuarterName                       ");
            searchSql.AppendLine(" 	,A.DeptID                            ");
            searchSql.AppendLine(" 	,B.DeptName                          ");
            searchSql.AppendLine(" 	,A.AdminLevelID                      ");
            searchSql.AppendLine(" 	,D.TypeName AS AdminLevelName        ");
            searchSql.AppendLine(" FROM                                  ");
            searchSql.AppendLine(" 	officedba.EmployeeInfo A             ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B       ");
            searchSql.AppendLine(" 		ON   B.CompanyCD=A. CompanyCD  and  B.ID = A.DeptID          ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            searchSql.AppendLine(" 		ON  C.CompanyCD=A.CompanyCD   and   C.ID = A.QuarterID       ");
            searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D ");
            searchSql.AppendLine(" 		ON  D.CompanyCD=A.CompanyCD   and  D.ID = A.AdminLevelID    ");
            searchSql.AppendLine(" WHERE                                 ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD             ");
            searchSql.AppendLine(" 	AND A.Flag = @Flag                   ");
            searchSql.AppendLine(" ORDER BY A.DeptID, A.QuarterID, A.EnterDate ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //在职标识
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Flag", "1"));

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        public static DataTable GetUserInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName");
            selectSql.AppendLine(" 	, ISNULL(C.QuarterName,'') AS QuarterName ,                      ");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID ,a.QuarterID   from officedba.EmployeeInfo a ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.DeptInfo B       ");
            selectSql.AppendLine(" 		ON  B.CompanyCD=A.CompanyCD  and B.ID = A.DeptID              ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            selectSql.AppendLine(" 		ON  C.CompanyCD=A.CompanyCD   and  C.ID = A.QuarterID   and c.DeptID= A.DeptID       ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType D ");
            selectSql.AppendLine(" 		ON  D.CompanyCD=A.CompanyCD and D.ID = A.AdminLevelID        ");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag='1'  and a.DeptID Is not null  ");
            selectSql.AppendLine("  ORDER BY a.DeptID, C.ID asc,a.AdminLevelID asc");
            //, a.EnterDate a.QuarterID ASC,A.AdminLevelID  asc

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }


        public static DataTable GetUserQuterInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("select  ");
            selectSql.AppendLine(" 	 ISNULL(C.QuarterName,'') AS QuarterName ,                      ");
            selectSql.AppendLine("B.ID  as  DeptID ,c.id as  QuarterID,c.SuperQuarterID   from   ");
            selectSql.AppendLine(" 	  officedba.DeptInfo B       ");
            selectSql.AppendLine(" 		           ");
            selectSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter C    ");
            selectSql.AppendLine(" 		ON  C.CompanyCD=B.CompanyCD        and c.DeptID= B.id     ");

            selectSql.AppendLine("where B.CompanyCD=@CompanyCD and B.UsedStatus='1' and  c.id  is not null      ");
            selectSql.AppendLine("  ORDER BY  B.ID asc");
            //, a.EnterDate a.QuarterID ASC,A.AdminLevelID  asc

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
    }
}
