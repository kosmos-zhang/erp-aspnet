/**********************************************
 * 类作用：   其他往来单位管理数据库层处理
 * 建立人：   陶春
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
namespace XBase.Data.Office.SystemManager
{
  public  class OtherCorpInfoDBHelper
    {

      /// <summary>
      /// 根据企业编码获取企业其他往来客户信息
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
      public static DataTable GetOtherCorpInfo(string CompanyCD)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine("select ID,CustName from officedba.OtherCorpInfo");
          sql.AppendLine("where CompanyCD=@CompanyCD and UsedStatus=1");
          SqlParameter[] parms = new SqlParameter[1];
          parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
          return SqlHelper.ExecuteSql(sql.ToString(), parms);
      }

    }
}
