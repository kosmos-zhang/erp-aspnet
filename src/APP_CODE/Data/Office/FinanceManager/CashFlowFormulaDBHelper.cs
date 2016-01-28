/**********************************************
 * 类作用：   现金流量表项目数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/26
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;

namespace XBase.Data.Office.FinanceManager
{
    public class CashFlowFormulaDBHelper
    {
        /// <summary> 
        /// 根据ID获取计算公式信息
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetCashFlowFormulaInfo(string KeyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select ID,Name,case  when cast(Line as varchar(10))='0' then '' else cast(Line as varchar(10))  end as Line  from officedba.CashFlowFormula ");

            //定义查询的命令
            SqlCommand comm = new SqlCommand();

            //主键
            if (!string.IsNullOrEmpty(KeyID))
            {
                strSql.AppendLine(" where ID=@ID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", KeyID));
            }

            strSql.AppendLine(" order by ID asc ");

            //指定命令的SQL文
            comm.CommandText = strSql.ToString();

            DataTable dt=SqlHelper.ExecuteSearch(comm);

            DataTable sourceDT = new DataTable();
            sourceDT.Columns.Add("ID");
            sourceDT.Columns.Add("Name");
            sourceDT.Columns.Add("Line");
            sourceDT.Columns.Add("ByOrder");

            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = sourceDT.NewRow();
                dr["ID"] = row["ID"].ToString();
                dr["Name"] = row["Name"].ToString();
                dr["Line"] = row["Line"].ToString();
                dr["ByOrder"] = "1";
                sourceDT.Rows.Add(dr);
            }

            return sourceDT;

        }
    }
}
