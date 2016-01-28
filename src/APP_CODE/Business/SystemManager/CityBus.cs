/**********************************************
 * 描述：     城市信息业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：CityBus
 ***********************************************/
using System;
using XBase.Data.SystemManager;
using XBase.Model.SystemManager;
using System.Data;
namespace XBase.Business.SystemManager
{
   public class CityBus
    {
       
        /// <summary>
        /// 根据省份编码获取城市信息
        /// </summary>
        /// <param name="ProvinceCode">省编码</param>
        /// <returns>返回Table</returns>
       public static DataTable GetCityByCode(string ProvinceCode)
       {
           if (string.IsNullOrEmpty(ProvinceCode)) return null;
           try
           {
               return CityDBHelper.GetCityByCode(ProvinceCode);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
    }
}
