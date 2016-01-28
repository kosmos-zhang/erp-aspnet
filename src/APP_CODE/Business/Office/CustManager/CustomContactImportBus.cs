using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.CustManager;
namespace XBase.Business.Office.CustManager
{
  public   class CustomContactImportBus
    {
        #region 验证对应客户是否存在
        public static string CustomExists(string companyCD, string custName)
        {
           return CustomContactImportDBHelper.CustomExists(companyCD, custName);
        }
        #endregion

        #region 验证联系人类型是否存在
        public static string ContactExists(string companyCD, string typeName)
        {
            return CustomContactImportDBHelper.ContactExists(companyCD, typeName);
        }
        #endregion
        #region 导入数据
        public static bool ImportContact(List<XBase.Model.Office.CustManager.LinkManModel> modeList)
        {
            return CustomContactImportDBHelper.ImportContact(modeList);
        }
        #endregion

    }
}
