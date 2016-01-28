/**********************************************
 * 类作用：   摘要类别数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/17
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
namespace XBase.Data.Office.FinanceManager
{
 public class SummaryTypeDBHelper
 {

     #region  判断摘要类别是否被引用
     public static bool isSummaryTypeReference(int SummaryTypeID)
     {
         try
         {
             bool result = false;

             StringBuilder sql = new StringBuilder();
             sql.AppendLine("select count(*) from officedba.SummarySetting");
             sql.AppendLine("where summTypeID=@ID");

             SqlParameter[] parm = {new SqlParameter("@ID",SummaryTypeID) };
             object objs = SqlHelper.ExecuteScalar(sql.ToString(), parm);
             if (Convert.ToInt32(objs) > 0)
             {
                 result = true;
             }
             return result;
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     #endregion

     #region 判断名称是否存在
     public static bool NameIsExist(string CompanyCD, string Name)
     {
         bool result = false;

         string sql = "select count(TypeName) from  officedba.SummaryType where CompanyCD=@CompanyCD and TypeName=@TypeName";
         SqlParameter[] parms = 
         {
             new SqlParameter("@CompanyCD",CompanyCD),
             new SqlParameter("@TypeName",Name)
         };

         object obj = SqlHelper.ExecuteScalar(sql, parms);
         if (Convert.ToInt32(obj) > 0)
         {
             result = true;
         }
         return result;
     }
     #endregion

     #region 根据企业编码获取摘要类别信息
     /// <summary>
     /// 根据企业编码获取摘要类别信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <returns>DataTable</returns>
     public static DataTable  GetSummaryTypeByCD(string CompanyCD)
     {
         string sql = "select ID,TypeName,UsedStatus from officedba.SummaryType where CompanyCD=@CompanyCD ";
         //string sql = "select ID,TypeName,UsedStatus from officedba.SummaryType where CompanyCD=@CompanyCD and UsedStatus='1'";
         SqlParameter[] parms = new SqlParameter[1];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         return SqlHelper.ExecuteSql(sql, parms);
     }
     #endregion

     #region 添加类别信息
     /// <summary>
     /// 添加类别信息
     /// </summary>
     /// <param name="Name">类别名称</param>
     /// <param name="UsedStatus">使用状态</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool InsertSummaryType(string CompanyCD,string Name, string UsedStatus)
     {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("Insert into officedba.SummaryType");
         sql.AppendLine("(CompanyCD,TypeName,UsedStatus)");
         sql.AppendLine("values(@CompanyCD,@TypeName,@UsedStatus)");
         SqlParameter[] parms = new SqlParameter[3];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         parms[1] = SqlHelper.GetParameter("@TypeName", Name);
         parms[2] = SqlHelper.GetParameter("UsedStatus", UsedStatus);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 更新类别信息
     /// <summary>
     /// 更新类别信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <param name="ID">主键</param>
     /// <param name="Name">名称</param>
     /// <param name="UsedStatus">使用状态</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool UpdateSummaryType(string CompanyCD, int ID, string Name, string UsedStatus)
     {
         StringBuilder sql = new StringBuilder();
         sql.AppendLine("Update officedba.SummaryType");
         sql.AppendLine(" set TypeName=@TypeName, ");
         sql.AppendLine("UsedStatus=@UsedStatus");
         sql.AppendLine("where CompanyCD=@CompanyCD");
         sql.AppendLine("and ID=@ID");
         SqlParameter[]parms=new SqlParameter[4];
         parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
         parms[1] = SqlHelper.GetParameter("@TypeName", Name);
         parms[2] = SqlHelper.GetParameter("UsedStatus", UsedStatus);
         parms[3] = SqlHelper.GetParameter("@ID", ID);
         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 删除类别信息
     /// <summary>
     /// 删除类别信息
     /// </summary>
     /// <param name="CompanyCD">企业编码</param>
     /// <param name="ID">主键</param>
     /// <returns>true 成功,false 失败</returns>
     public static bool DelSummaryType(string CompanyCD, string ID)
     {
         string sql = "Delete from officedba.SummaryType where CompanyCD=@CompanyCD and ID in ("+ID+")";

         SqlParameter[] parms = 
         {
             new SqlParameter("@CompanyCD",CompanyCD)
         };

         SqlHelper.ExecuteTransSql(sql.ToString(), parms);
         return SqlHelper.Result.OprateCount > 0 ? true : false;
     }
     #endregion

     #region 根据ID获取摘要类别名称
     /// <summary>
      /// 根据ID获取摘要类别名称
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
     public static string GetSummaryTypeName(int id)
     {
         string sql = string.Format(@"select TypeName from officedba.SummaryType where id={0} ",id);
         return Convert.ToString(SqlHelper.ExecuteScalar(sql, null));
     }
     #endregion

 }
}
