/**********************************************
 * 作    者： 江贻明
 * 创建日期： 2009.03.13
 * 描    述： 获取指定表自动生成的ID
 * 版    本： 0.5.0
 ***********************************************/
using System;
using XBase.Data.Common;
namespace XBase.Business.Common
{
  public  class IDIdentityUtil
    {
      /// <summary>
      /// 获取指定表自动生成的主键标识ID
      /// </summary>
      /// <param name="TableName">表名</param>
      /// <returns>int</returns>
      public static int GetIDIdentity(string TableName)
      {
          if (string.IsNullOrEmpty(TableName)) return 0;
          try
          {
              return IDIdentityDBHelper.GetIDIdentity(TableName);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    }
}
