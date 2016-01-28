using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.SystemManager;
namespace XBase.Business.Office.SystemManager
{
   public class ProcessLogBus
    {
       public static DataTable SearchLog(string userid, string starttime, string endtime, string mod, string objid, string companycd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {
           try
           {
               return ProcessLogDBHelper.SearchLog(userid, starttime, endtime, mod, objid, companycd, pageIndex, pageCount, OrderBy, ref totalCount);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
    }
}
