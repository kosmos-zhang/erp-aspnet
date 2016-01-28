/***********************************************************************
 * 
 * Module Name:APP_CODE.Common.XBase.Data.SystemManager.SystemDBHelper
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-07
 * End Date:
 * Description:
 * Version History:
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;
namespace XBase.Data.SystemManager
{
   public  class SystemDBHelper
    {

       /// <summary>
       /// 获取SysParam表信息
       /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable  GetPubParms()
       {
           string sql = "select ID,CompanyCD,IndexType,IndexNum,IndexCode" +
                      "IndexValue,remark from SysParam";
           return SqlHelper.ExecuteSql(sql);
       }

    }
}
