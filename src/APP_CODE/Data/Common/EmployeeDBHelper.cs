using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.Common
{
    public class EmployeeDBHelper
    {
        #region 根据员工ID获取员工姓名的方法
        /// <summary>
        /// 根据员工ID获取员工姓名的方法
        /// </summary>
        /// <param name="ID">员工ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回员工姓名</returns>
        public static string GetEmployeeNameByID(int ID, string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                   " EmployeeName " +
                               " from " +
                                   " officedba.EmployeeInfo " +
                               " where " +
                                    " id = @ID " +
                               " and CompanyCD = @CompanyCD";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ID",ID);
                DataTable data = SqlHelper.ExecuteSql(sql, param);
                               
                //如果数据不存在，则返回null
                if (data == null || data.Rows.Count < 1)
                {
                    return null;
                }
                //如果数据存在，则返回对应的值
                else
                {
                    return data.Rows[0]["EmployeeName"].ToString();
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据员工ID获取员工姓名的方法
        /// <summary>
        /// 根据员工ID获取员工姓名的方法
        /// </summary>
        /// <param name="ID">员工ID</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回员工姓名</returns>
        public static string GetEmployeeNameByID(int ID)
        {
            try
            {
                string sql = "select " +
                                   " EmployeeName " +
                               " from " +
                                   " officedba.EmployeeInfo " +
                               " where " +
                                    " id = @ID ";                               
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@ID", ID);
                DataTable data = SqlHelper.ExecuteSql(sql, param);

                //如果数据不存在，则返回null
                if (data == null || data.Rows.Count < 1)
                {
                    return null;
                }
                //如果数据存在，则返回对应的值
                else
                {
                    return data.Rows[0]["EmployeeName"].ToString();
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
