using System;
using System.Collections.Generic;
using XBase.Model.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
 

namespace XBase.Business.Office.PurchaseManager
{
    public class PurchaseRequireBus
    {
        public static DataTable GetPurchaseRequireInfo(PurchaseRequireModel PurchaseRequireM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return PurchaseRequireDBHelper.GetPurchaseRequireInfo(PurchaseRequireM,pageIndex,pageCount,OrderBy,ref totalCount);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }

        public static DataTable GetPurchaseRequireInfo(PurchaseRequireModel PurchaseRequireM, string OrderBy)
        {
            try
            {
                return PurchaseRequireDBHelper.GetPurchaseRequireInfo(PurchaseRequireM, OrderBy);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public static DataTable GetProductType(int ParentID)
        {
            return PurchaseRequireDBHelper.GetProductType(ParentID);
        }

        public static DataTable GetProductInfo()
        {
            return PurchaseRequireDBHelper.GetProduct();
        }

        #region 删除
        public static bool DeletePurchaseRequireInfo(int[] IDS)
        {
            return PurchaseRequireDBHelper.DeletePurchaseRequireInfo(IDS);
        }
        #endregion
    }
}
