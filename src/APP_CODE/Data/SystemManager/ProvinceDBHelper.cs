/**********************************************
 * 描述：     省份表数据层处理
 * 建立人：   江贻明
 * 建立时间： 2009/01/21
 * 类名：ProvinceDBHelper
 ***********************************************/
using System.Data;
using XBase.Model.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
namespace XBase.Data.SystemManager
{
  public  class ProvinceDBHelper
    {

      /// <summary>
      /// 获取省份表信息
      /// </summary>
      /// <returns>返回Table</returns>
      public static DataTable GetProvinceInfo()
      {
          string sql = "select ProvCD,ProvName from Province  order by Cast (ProvCD as int) asc";
          return SqlHelper.ExecuteSql(sql);
      }
    }
}
