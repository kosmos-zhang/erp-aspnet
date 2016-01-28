using System.Data;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.Personal.MessageBox
{
    public class GetUserDepList
    {
        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public  DataTable GetDeptInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID, CompanyCD, DeptNO ");
            selectSql.AppendLine(" , SuperDeptID, PYShort ");
            selectSql.AppendLine(" , DeptName, AccountFlag ");
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
        public DataTable GetUserInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID ");
            selectSql.AppendLine(" , ISNULL(EmployeeName,'') AS EmployeesName ");
            selectSql.AppendLine(" , DeptID ");
            selectSql.AppendLine(" FROM officedba.EmployeeInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND Flag = @Flag ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
            param[1] = SqlHelper.GetParameter("@Flag", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion
    }
}
