/**********************************************
 * 类作用：   期末项目设置数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/03/27
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
namespace XBase.Data.Office.FinanceManager
{
  public class EndItemSettingDBHelper
  {
      #region  添加期末项目
      /// <summary>
      /// 添加期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="ItemName">项目名称</param>
      /// <param name="UsedStatus">使用状态</param>
      /// <returns>true 成功，false失败</returns>
      public static bool InsertEndItemSetting(string CompanyCD, string ItemName, string UsedStatus,int ShowIndex)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Insert into officedba.EndItemProcSetting ");
          sql.AppendLine("(CompanyCD,ItemName,UsedStatus,ShowIndex)");
          sql.AppendLine("values(@CompanyCD,@ItemName,@UsedStatus,@ShowIndex)");
          SqlParameter[] parms = new SqlParameter[4];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ItemName", ItemName);
          parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
          parms[3] = SqlHelper.GetParameter("@ShowIndex", ShowIndex);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 修改期末项目
      /// <summary>
      /// 修改期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <param name="ID">主键</param>
      /// <param name="UsedStatus">使用状态</param>
      /// <returns>true 成功，false失败</returns>
      public static bool UpdateEndItemSetting(string CompanyCD, string ID,string UsedStatus)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.EndItemProcSetting set UsedStatus=@UsedStatus");
          sql.AppendLine(" where CompanyCD=@CompanyCD and ID=@ID");
          SqlParameter[] parms = new SqlParameter[3];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ID", ID);
          parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion


      #region 更新期末项目
      public static bool UpdateEndItemByName(string CompanyCD, string ItemName, string UsedStatus)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("Update officedba.EndItemProcSetting set UsedStatus=@UsedStatus");
          sql.AppendLine(" where CompanyCD=@CompanyCD and ItemName=@ItemName");
          SqlParameter[] parms = new SqlParameter[3];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          parms[1] = SqlHelper.GetParameter("@ItemName", ItemName);
          parms[2] = SqlHelper.GetParameter("@UsedStatus", UsedStatus);
          SqlHelper.ExecuteTransSql(sql.ToString(), parms);
          return SqlHelper.Result.OprateCount > 0 ? true : false;
      }
      #endregion

      #region 根据企业编码查看企业项目设置
      /// <summary>
      /// 根据企业编码查看企业项目设置
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetEndItemSettingInfo(string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select ID,ItemName,UsedStatus from");
          sql.AppendLine("officedba.EndItemProcSetting  ");
          sql.AppendLine("where CompanyCD=@CompanyCD");
          SqlParameter[]parms=new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          return SqlHelper.ExecuteSql(sql.ToString(), parms);
      }
      #endregion

      #region 查看当前企业是否存在期末项目
      public static bool Isexist(string CompanyCD, string EndItemName)
      {
          bool result = false;

          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select count(ItemName) from  officedba.EndItemProcSetting");
          sql.AppendLine("where CompanyCD=@CompanyCD and ItemName=@ItemName ");

          SqlParameter[] parms = 
          {
              new SqlParameter("@CompanyCD",CompanyCD),
              new SqlParameter("@ItemName",EndItemName)
          };

          object objs = SqlHelper.ExecuteScalar(sql.ToString(),parms);
          if (Convert.ToInt32(objs)>0)
          {
              result = true;
          }

          return result;
      }
      #endregion

      #region 获取为已启用的期末项目
      /// <summary>
      /// 获取为已启用的期末项目
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetEndItemIsUsed(string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select ID,ItemName ");
          sql.AppendLine("from officedba.EndItemProcSetting ");
          sql.AppendLine("where CompanyCD=@CompanyCD and UsedStatus='1' order by ShowIndex");
          SqlParameter[] parms = {
                                     new SqlParameter("@CompanyCD", CompanyCD) 
                                 };
          return SqlHelper.ExecuteSql(sql.ToString(),parms);
      }
      #endregion

  }
}
