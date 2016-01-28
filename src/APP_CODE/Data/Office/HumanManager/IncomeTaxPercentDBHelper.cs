/**********************************************
 * 类作用：   个人所得税税率
 * 建立人：   吴志强
 * 建立时间： 2009/05/18
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
using System.Collections;

namespace XBase.Data.Office.HumanManager
{
    public class IncomeTaxPercentDBHelper
    {
        #region 查询个人所得税税率
        /// <summary>
        /// 查询个人所得税税率
        /// </summary>
        /// <param name="taxMoney">缴税金额</param>
        /// <returns></returns>
        public static DataTable GetTaxRate(string taxMoney)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                        ");
            searchSql.AppendLine(" 	 TaxLevel                    ");
            searchSql.AppendLine(" 	,MinMoney                    ");
            searchSql.AppendLine(" 	,MaxMoney                    ");
            searchSql.AppendLine(" 	,TaxPercent                  ");
            searchSql.AppendLine(" 	,MinusMoney                  ");
            searchSql.AppendLine(" FROM                          ");
            searchSql.AppendLine(" 	officedba.IncomeTaxPercent   ");
            searchSql.AppendLine(" WHERE                         ");
            searchSql.AppendLine(" 	MinMoney < @SalaryMoney      ");
            searchSql.AppendLine(" 	AND MaxMoney >= @SalaryMoney ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //缴税金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SalaryMoney", taxMoney));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
    }
}
