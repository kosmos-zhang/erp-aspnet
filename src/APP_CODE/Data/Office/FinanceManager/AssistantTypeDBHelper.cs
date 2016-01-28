/**********************************************
 * 类作用：   科目核算数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/09
 ***********************************************/
using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
namespace XBase.Data.Office.FinanceManager
{
 public  class AssistantTypeDBHelper
 {
     #region 判断名称是否存在
     public static bool NameIsExist(string CompanyCD, string Name)
     {
         bool result = false;

         string sql = "select count(name) from  officedba.AssistantType where CompanyCD=@CompanyCD and Name=@Name";
         SqlParameter[] parms = 
         {
             new SqlParameter("@CompanyCD",CompanyCD),
             new SqlParameter("@Name",Name)
         };

         object obj = SqlHelper.ExecuteScalar(sql, parms);
         if (Convert.ToInt32(obj) > 0)
         {
             result = true;
         }
         return result;
     }
     #endregion

     #region 添加科目核算类别
     /// <summary>
     /// 添加科目核算类别
     /// </summary>
     /// <param name="CompanyCD">公司编码</param>
     /// <param name="Name">核算名称</param>
     /// <param name="UsedStatus">使用状态</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool InsertAssistantType(string CompanyCD, string Name, string UsedStatus)
     {
         string sql = "Insert into officedba.AssistantType(CompanyCD,Name,UsedStatus)" +
                    "values(@CompanyCD,@Name,@UsedStatus)";
         SqlParameter[] parms = new SqlParameter[3];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         parms[1] = SqlHelper.GetParameter("@Name", Name);
         parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 修改科目核算类别信息
     /// <summary>
     /// 修改科目核算类别信息
     /// </summary>
     /// <param name="Name">名称</param>
     /// <param name="UsedStatus">使用状态</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool UpdateAssistantType(string CompanyCD,int ID,
         string Name, string UsedStatus)
     {
         string sql = "Update  officedba.AssistantType set Name=@Name,UsedStatus=@UsedStatus " +
             "where CompanyCD=@CompanyCD and ID=@ID";
         SqlParameter[] parms = new SqlParameter[4];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         parms[1] = SqlHelper.GetParameter("@Name", Name);
         parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
         parms[3] = SqlHelper.GetParameter("@ID", ID);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 删除科目核算类别
     /// <summary>
     /// 删除科目核算类别
     /// </summary>
     /// <param name="CompanyCD">公司编码</param>
     /// <param name="ID">主键</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool DelAssistantType(string CompanyCD, string ID)
     {
         string sql = "Delete from officedba.AssistantType where CompanyCD=@CompanyCD and ID in ("+ID+")";
         SqlParameter[] parms = {
                                    new SqlParameter("@CompanyCD",CompanyCD)
                                };
         SqlHelper.ExecuteTransSql(sql, parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 获取科目核算类别
     /// <summary>
     /// 获取科目核算类别
     /// </summary>
     /// <param name="CompanyCD">公司编码</param>
     /// <returns>DataTable</returns>
     public static DataTable GetAssistantType(string CompanyCD,string Name,string UsedStatus)
     {
         string sql = "select ID,Name,UsedStatus  from officedba.AssistantType where 1=1";
         SqlCommand cmdsql = new SqlCommand();
         if (!string.IsNullOrEmpty(CompanyCD))
         {
             sql += " and  CompanyCD=@CompanyCD";
             cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
         }
         if (!string.IsNullOrEmpty(Name))
         {
             sql += "  and name=@Name";
             cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@Name", Name));
         }
         if (!string.IsNullOrEmpty(UsedStatus))
         {
             sql += "  and UsedStatus=@UsedStatus";
             cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", UsedStatus));
         }
         //指定命令的SQL文
         cmdsql.CommandText = sql;
         //执行查询
         return SqlHelper.ExecuteSearch(cmdsql);
     }
     #endregion
 }
}
