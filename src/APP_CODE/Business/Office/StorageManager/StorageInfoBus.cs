using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Common;
using System.Data;
using XBase.Data.Office.StorageManager;
namespace XBase.Business.Office.StorageManager
{
    public class StorageInfoBus
    {
        public StorageInfoBus() { }

        public static DataSet GetStorageInfoFromExcel(string companycd, string fname, string tbname)
        {
            return StorageDBHelper.GetStorageInfoFromExcel(companycd, fname, tbname);
        }

        public static bool ChargeProductInfo(string codename, string compid, string prodNo)
        {
            return StorageDBHelper.ChargeProductInfo(codename, compid,prodNo);
        }

        public static int GetExcelToStorageInfo(string companycd,string usercode,string isbatchno,int creator)
        {
            return StorageDBHelper.GetExcelToStorageInfo(companycd, usercode, isbatchno, creator);
        }

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            return StorageDBHelper.ReadEexcel(FilePath, companycd);
        }
    }
}
