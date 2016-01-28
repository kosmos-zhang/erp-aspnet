/**********************************************
 * 类作用：   期末项目记录数据库层处理
 * 建立人：   江贻明
 * 建立时间： 2009/05/04
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
namespace XBase.Data.Office.FinanceManager
{
   public class EndItemProcessedRecordDBHelper
   {
       #region 根据企业查找企业期末最大期数
       public static int GetMaxPeriodNum(string CompanyCD,int ItemID)
       {
           int result = 0;
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select max(cast(PeriodNum as int)) from ");
           sql.AppendLine("officedba.EndItemProcessedRecord where CompanyCD=@CompanyCD and ItemID=@ItemID");

           SqlParameter[] parm = {new SqlParameter("@CompanyCD",CompanyCD),
                                 new SqlParameter("@ItemID",ItemID)};

           object objs = SqlHelper.ExecuteScalar(sql.ToString(),parm);
           if(objs!=DBNull.Value)
           {
               result = Convert.ToInt32(objs);
           }

           return result;

       }
       #endregion

       #region 查询当期项目是否进行期末处理
       public static string CheckCurrentPeriodIsProced(string CompanyCD, string PeriodNum,int ItemID)
       {
           string result = string.Empty;

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select IsAccount from ");
           sql.AppendLine(" officedba.EndItemProcessedRecord ");
           sql.AppendLine("where CompanyCD=@CompanyCD and PeriodNum=@PeriodNum");
           sql.AppendLine("and  ItemID=@ItemID ");
           SqlParameter[] parms = 
           {
                new SqlParameter("@CompanyCD",CompanyCD),
                new SqlParameter("@PeriodNum",PeriodNum),
                new SqlParameter("@ItemID",ItemID),
           };

           DataTable dt = SqlHelper.ExecuteSql(sql.ToString(), parms);
           if (dt != null && dt.Rows.Count > 0)
           {
               result = dt.Rows[0]["IsAccount"].ToString();
           }

           return result;
       }
       #endregion

       #region  检查固定资产当期是否计提
       public static bool CheckFixAssetIsJT(string PeriodNum, int ItemID,string CompanyCD)
       {
           bool result = false;

           StringBuilder sql = new StringBuilder();
           sql.AppendLine("select count(PeriodNum) from ");
           sql.AppendLine(" officedba.EndItemProcessedRecord ");
           sql.AppendLine("where CompanyCD=@CompanyCD  and   ItemID=@ItemID  and PeriodNum=@PeriodNum");

           SqlParameter[] parms = 
           {
                new SqlParameter("@ItemID",ItemID),
                new SqlParameter("@PeriodNum",PeriodNum),
                new SqlParameter("@CompanyCD",CompanyCD)
           };
           object objs = SqlHelper.ExecuteScalar(sql.ToString(), parms);
           if (Convert.ToInt32(objs) > 0)
           {
               result = true;
           }
           return result;
       }

       #endregion


       #region 添加已期末处理的项目信息
       /// <summary>
       /// 添加已期末处理的项目信息 
       /// </summary>
       /// <param name="CompanyCD">企业编码</param>
       /// <param name="ItemID">项目编号</param>
       /// <param name="PeriodNum">期数</param>
       /// <returns>True成功,False失败</returns>
       public static bool InsertPeriodProced(string CompanyCD,
           string ItemID, string PeriodNum)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("Insert into officedba.EndItemProcessedRecord");
           sql.AppendLine("(CompanyCD,ItemID,PeriodNum,CreateDate)");
           sql.AppendLine("Values(@CompanyCD,@ItemID,@PeriodNum,getdate())");
           SqlParameter[] parms = 
           {
               new SqlParameter("@CompanyCD",CompanyCD),
               new SqlParameter("@ItemID",ItemID),
               new SqlParameter("@PeriodNum",PeriodNum)
           };

           SqlHelper.ExecuteTransSql(sql.ToString(), parms);
           return SqlHelper.Result.OprateCount > 0 ? true : false;
       }
       #endregion
   }
}
