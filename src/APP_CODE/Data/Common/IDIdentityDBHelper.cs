/**********************************************
 * 作    者： 江贻明
 * 创建日期： 2009.03.13
 * 描    述： 获取指定已添加成功记录自动生成的ID
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.Common
{
  public  class IDIdentityDBHelper
    {

      /// <summary>
      /// 获取刚插入的标识值
      /// </summary>
      /// <param name="TableName">表名</param>
      /// <returns>ID</returns>
      public static int GetIDIdentity(string TableName)
      {
          int result = 0;
          string sql = "SELECT distinct IDENT_CURRENT('"+ TableName +"') from " + TableName + "  ";
          object obj = SqlHelper.ExecuteScalar(sql, null);
          if (Convert.ToInt32(obj) > 0)
          {
              result = Convert.ToInt32(obj);
          }
          return result;
      }
    }
}
