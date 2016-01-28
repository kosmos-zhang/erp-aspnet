using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Data.Office.StorageManager;

namespace XBase.Business.Office.StorageManager
{
  public  class StorageCustInfo
    {
      public static DataTable GetCustInfor()
      {
          return  StorageCustInfoDBHelper.GetCustInfo();
      }
    }
}
