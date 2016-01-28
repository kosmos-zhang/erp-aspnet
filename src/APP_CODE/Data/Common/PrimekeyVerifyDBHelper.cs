/**********************************************
 * 作    者： 陶春
 * 创建日期： 2009.03.02
 * 描    述： 字段唯一性验证
 * 修改日期： 2009.03.02
 * 版    本： 0.1.0
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Business.Common
{
    public class PrimekeyVerifyDBHelper
    {
        //string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD";
        //  //设置参数
        //  SqlParameter[] param = new SqlParameter[1];
        //  param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

        //  DataTable userCount = SqlHelper.ExecuteSql(select, param);
        //  if (userCount != null && userCount.Rows.Count > 0)
        //  {
        //      return (int)userCount.Rows[0][0];
        //  }
        //  else
        //  {
        //      return 0;
        //  }
        /// <summary>
        ///  主键重复字段验证
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="arr">需要验证的数据库字段（数据库字段）数组</param>
        /// <param name="queryarr">验证字段如输入文本等的值</param>
        /// <returns></returns>

        public static bool PrimekeyVerifytc(string tablename, string[] arr, string[] queryarr)
        {
            DataTable dt;
            bool flag = false;
            if (arr.Length == 1 && (arr.Length != 0))///一个参数
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@queryarr", queryarr[0]);
                string sql = "select count(*) from " + tablename + " where " + arr[0] + "=@queryarr";
                dt = SqlHelper.ExecuteSql(sql, param);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return !flag;
                }
                else
                {
                    return flag;
                }
            }
            else if (arr.Length > 1)///两个参数
            {
                for (int n = 0; n < arr.Length - 1; n++)
                {
                    string sql = "select count(*) from " + tablename + " where " + arr[n] + "='" + queryarr[n] + "'" + " and " + arr[n + 1] + " ='" + queryarr[n + 1] + "'";
                    dt = SqlHelper.ExecuteSql(sql);
                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        return !flag;
                    }
                }
            }
            return flag;
        }

        #region 校验编号的唯一性
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue, string companyCD)
        {
            //校验SQL定义
            string checkSql = " SELECT " + columnName + " FROM officedba." + tableName
                                    + " WHERE CompanyCD = @CompanyCD AND " + columnName + " = @ColumnValue";

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //编码类型
            param[i++] = SqlHelper.GetParameter("@ColumnValue", codeValue);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }

        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue, string companyCD, string Condition)
        {
            //校验SQL定义
            string checkSql = " SELECT " + columnName + " FROM officedba." + tableName
                                    + " WHERE CompanyCD = @CompanyCD AND " + columnName + " = @ColumnValue "+ "AND "+Condition;

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //编码类型
            param[i++] = SqlHelper.GetParameter("@ColumnValue", codeValue);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 不根据企业编码验证
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="companyCD"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static bool CheckUserUniq(string tableName, string columnName, string codeValue)
        {
            string checkSql = " SELECT " + columnName + " FROM officedba." + tableName
                                    + " WHERE  " + columnName + " = @ColumnValue";

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            int i = 0;
            //公司代码
            //编码类型
            param[i++] = SqlHelper.GetParameter("@ColumnValue", codeValue);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }
        #endregion
        #region 校验编号的唯一性
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq1(string tableName, string columnName, string codeValue, string companyCD,string Flag)
        {
            //校验SQL定义
            string checkSql = " SELECT " + columnName + " FROM officedba." + tableName
                                    + " WHERE CompanyCD = @CompanyCD AND " + columnName + " = @ColumnValue and Flag=@Flag";

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //编码类型
            param[i++] = SqlHelper.GetParameter("@ColumnValue", codeValue);
            param[i++] = SqlHelper.GetParameter("@Flag", Flag);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }
        #endregion

    }
}
