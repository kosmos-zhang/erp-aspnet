/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.03.04
 * 描    述： 员工以及部门选择
 * 修改日期： 2009.03.04
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：UserDeptSelectDBHelper
    /// 描述：处理员工部门选择页面的业务处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/04
    /// 最后修改时间：2009/03/04
    /// </summary>
    ///
    public class UserDeptSelectDBHelper
    {

        #region 获取分公司信息
        public static DataTable GetSubCompanyinfo(string CompanyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID ");
            selectSql.AppendLine(" , SuperDeptID, ");
            selectSql.AppendLine("  DeptName");
            selectSql.AppendLine(" FROM officedba.DeptInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND UsedStatus = @UsedStatus and subflag !=0   ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);



        }
        #endregion

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDeptInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID, CompanyCD, DeptNO ");
            selectSql.AppendLine(" , SuperDeptID, PYShort ");
            selectSql.AppendLine(" , DeptName, AccountFlag,'' as isFlag,isnull(subflag,0) as subflag ");
            selectSql.AppendLine(" FROM officedba.DeptInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND UsedStatus = @UsedStatus ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        #region 获取员工信息
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            //selectSql.AppendLine(" SELECT A.EmployeesID AS EmployeesID ");
            //selectSql.AppendLine(" , ISNULL(B.EmployeeName,'') AS EmployeesName ");
            //selectSql.AppendLine(" , A.NowDeptID AS DeptID ");
            //selectSql.AppendLine(" FROM officedba.EmployeeJob AS A LEFT JOIN ");
            //selectSql.AppendLine(" officedba.EmployeeInfo AS B ON ");
            //selectSql.AppendLine(" A.CompanyCD = B.CompanyCD AND A.EmployeesID = B.ID ");
            //selectSql.AppendLine(" WHERE ");
            //selectSql.AppendLine(" A.CompanyCD = @CompanyCD ");
            //selectSql.AppendLine(" AND A.Flag <> @Flag ");


            //selectSql.AppendLine("select ID,ISNULL(EmployeeName,'') AS EmployeesName,Flag,");
            //selectSql.AppendLine("isnull(cast (DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo ");
            //selectSql.AppendLine("where CompanyCD=@CompanyCD and Flag!='2' and DeptID Is not null  ");

            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a");
            selectSql.AppendLine("left join officedba.deptinfo  as b on a.DeptID=b.ID");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and isnull(a.delFlag,0)=0  ");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
          //  param[1] = SqlHelper.GetParameter("@Flag", "3");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion


        #region 获取员工信息--去除已离职人员
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo(string companyCD,string flag)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            //selectSql.AppendLine(" SELECT A.EmployeesID AS EmployeesID ");
            //selectSql.AppendLine(" , ISNULL(B.EmployeeName,'') AS EmployeesName ");
            //selectSql.AppendLine(" , A.NowDeptID AS DeptID ");
            //selectSql.AppendLine(" FROM officedba.EmployeeJob AS A LEFT JOIN ");
            //selectSql.AppendLine(" officedba.EmployeeInfo AS B ON ");
            //selectSql.AppendLine(" A.CompanyCD = B.CompanyCD AND A.EmployeesID = B.ID ");
            //selectSql.AppendLine(" WHERE ");
            //selectSql.AppendLine(" A.CompanyCD = @CompanyCD ");
            //selectSql.AppendLine(" AND A.Flag <> @Flag ");


            //selectSql.AppendLine("select ID,ISNULL(EmployeeName,'') AS EmployeesName,Flag,");
            //selectSql.AppendLine("isnull(cast (DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo ");
            //selectSql.AppendLine("where CompanyCD=@CompanyCD and Flag!='2' and DeptID Is not null  ");

            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a");
            selectSql.AppendLine("left join officedba.deptinfo  as b on a.DeptID=b.ID");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3' and isnull(a.delFlag,0)=0");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
            //  param[1] = SqlHelper.GetParameter("@Flag", "3");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion


    }
}
