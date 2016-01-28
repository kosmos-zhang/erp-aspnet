/**********************************************
 * 描述：     省份表业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：ProvinceBus
 ***********************************************/
using System;
using XBase.Data.SystemManager;
using System.Data;
namespace XBase.Business.SystemManager
{
  public class ProvinceBus
    {
       /// <summary>
      /// 获取省份表信息
      /// </summary>
      /// <returns>返回Table</returns>
      public static DataTable GetProvinceInfo()
      {
          try
          {
              return ProvinceDBHelper.GetProvinceInfo();
          }
          catch (System.Exception ex)
          {
              throw ex;
          }
      }

    }
}
