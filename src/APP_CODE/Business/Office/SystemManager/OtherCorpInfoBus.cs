/**********************************************
 * 类作用：   其他往来单位管理业务层处理
 * 建立人：   陶春
 * 建立时间： 2009/04/13
 ***********************************************/
using System;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using System.Data;
namespace XBase.Business.Office.SystemManager
{
   public class OtherCorpInfoBus
    {
      /// <summary>
      /// 根据企业编码获取企业其他往来客户信息
      /// </summary>
      /// <param name="CompanyCD">企业编码</param>
      /// <returns>DataTable</returns>
       public static DataTable GetOtherCorpInfo()
       {
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           try
           {
               return OtherCorpInfoDBHelper.GetOtherCorpInfo(CompanyCD);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
