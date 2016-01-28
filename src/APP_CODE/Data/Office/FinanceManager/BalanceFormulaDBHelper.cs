/**********************************************
 * 类作用：   资产负债表计算公司数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/05/08
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;

namespace XBase.Data.Office.FinanceManager
{
   public class BalanceFormulaDBHelper
    { 
       /// <summary>
       /// 添加资产负债表公式
       /// </summary>
       /// <param name="model"></param>
       /// <param name="IntID"></param>
       /// <returns></returns>
       public static bool InsertBalanceFormula(BalanceFormulaModel model, out int IntID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("insert into officedba.BalanceFormula(");
           strSql.AppendLine("Name,Line)");
           strSql.AppendLine(" values (");
           strSql.AppendLine("@Name,@Line)");
           strSql.AppendLine("set @IntID= @@IDENTITY");
           SqlParameter[] parms = new SqlParameter[3];
           parms[0] = SqlHelper.GetParameter("@Name", model.Name);
           parms[1] = SqlHelper.GetParameter("@Line", model.Line);
           parms[2] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

           SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
           IntID = Convert.ToInt32(parms[4].Value);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       /// <summary>
       ///  修改资产负债表计算公式
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool UpdateBalanceFormula(BalanceFormulaModel model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("update officedba.BalanceFormula set ");
           strSql.AppendLine("Name=@Name,");
           strSql.AppendLine("Line=@Line,");
       
           strSql.AppendLine(" where ID=@ID");

           SqlParameter[] parms = new SqlParameter[3];
           parms[0] = SqlHelper.GetParameter("@Name", model.Name);
           parms[1] = SqlHelper.GetParameter("@Line", model.Line);
           parms[2] = SqlHelper.GetParameter("@ID", model.ID);

           SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       /// <summary>
       /// 根据查询条件查询资产负债表计算公式信息
       /// </summary>
       /// <param name="name">名称</param>
       /// <param name="KeyID">主键</param>
       /// <param name="companyCD">公司编码</param>
       /// <returns></returns>
       public static DataTable GetBalanceFormulaInfo(string name,string KeyID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("select ID,Name,Line  from officedba.BalanceFormula ");
           //定义查询的命令
           SqlCommand comm = new SqlCommand();

           //名称
           if (!string.IsNullOrEmpty(name))
           {
               strSql.AppendLine(" where Name LIKE @Name ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Name", "%" + name + "%"));

               //主键
               if (!string.IsNullOrEmpty(KeyID))
               {
                   strSql.AppendLine(" and ID=@ID ");
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", KeyID));
               }
           }
           else if (!string.IsNullOrEmpty(KeyID)) //主键
               {
                   strSql.AppendLine(" where ID=@ID ");
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", KeyID));
               }


           


           //指定命令的SQL文
           comm.CommandText = strSql.ToString();
           //执行查询
           DataTable dt = SqlHelper.ExecuteSearch(comm);

           DataTable newdt = new DataTable();
           newdt.Columns.Add("Name");
           newdt.Columns.Add("Line");
           newdt.Columns.Add("ID");
           newdt.Columns.Add("FName");
           newdt.Columns.Add("FLine");
           newdt.Columns.Add("FID");
           newdt.Columns.Add("CompanyCD");
           for (int i = 0; i < 32; i++)
           {
               DataRow row = newdt.NewRow();
               row["Name"] = dt.Rows[i]["Name"].ToString();
               row["Line"] = dt.Rows[i]["Line"].ToString() == "0" ? "" : dt.Rows[i]["Line"].ToString();
               row["ID"] = dt.Rows[i]["ID"].ToString();
               row["CompanyCD"] = "1";
               row["FName"] = dt.Rows[i + 32]["Name"].ToString();
               row["FLine"] = dt.Rows[i + 32]["Line"].ToString() == "0" ? "" : dt.Rows[i + 32]["Line"].ToString();
               row["FID"] = dt.Rows[i + 32]["ID"].ToString();
               
               newdt.Rows.Add(row);
           }

           return newdt;
       }

       /// <summary>
       /// 根据ID获取计算公式信息
       /// </summary>
       /// <param name="KeyID"></param>
       /// <param name="companyCD"></param>
       /// <returns></returns>
       public static DataTable GetBalanceFormulaInfo(string KeyID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.AppendLine("select ID,Name,Line  from officedba.BalanceFormula ");

           //定义查询的命令
           SqlCommand comm = new SqlCommand();

           //主键
           if (!string.IsNullOrEmpty(KeyID))
           {
               strSql.AppendLine(" where ID=@ID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", KeyID));
           }

           //指定命令的SQL文
           comm.CommandText = strSql.ToString();

           return SqlHelper.ExecuteSearch(comm);

       }


       /// <summary>
       /// 删除资产负债表计算公式信息
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static bool DeleteBalanceFormulaInfo(string ids)
       {
           string sql = string.Format(@"delete from officedba.BalanceFormula where ID in ( {0} )", ids);
           SqlParameter[] parms = new SqlParameter[0];
           SqlHelper.ExecuteTransSql(sql, parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;

       }
    }
}
