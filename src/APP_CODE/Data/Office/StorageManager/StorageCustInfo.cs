using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;
namespace XBase.Data.Office.StorageManager
{
    public class StorageCustInfoDBHelper
    {
        public static DataTable GetCustInfo()
        {
            string sql = "select a.ID as ProviderID,a.CustNo as ProviderNo,a.CustName as ProviderName,a.BigType as BigtypeID, Case a.BigType when '1' then '客户' when '2' then '供应商' when '3' then '竞争对手' when '4' then '银行' when '5' then '外协加工厂' when '6' then '运输商' when '7' then '其他' end as BigtypeName from officedba.ProviderInfo as a where 1=1";

            return SqlHelper.ExecuteSql(sql);
        }
    }
}
