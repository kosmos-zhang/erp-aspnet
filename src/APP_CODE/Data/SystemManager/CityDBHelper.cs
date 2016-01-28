/**********************************************
 * 描述：     城市表数据层处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：CityDBHelper
 ***********************************************/
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.SystemManager
{
    public class CityDBHelper
    {

        /// <summary>
        /// 根据省份编码获取城市信息
        /// </summary>
        /// <param name="ProvinceCode">省编码</param>
        /// <returns>返回Table</returns>
        public static DataTable GetCityByCode(string ProvinceCode)
        {
            string sql = "select CityCD,CityName from City where ProvCD=@ProvCD";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ProvCD", ProvinceCode);
            return SqlHelper.ExecuteSql(sql,parms);
        }

    }
}
