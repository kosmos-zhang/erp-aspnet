/**********************************************
 * 类作用：   工艺档案事务层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/16
 ***********************************************/

using System;
using System.Collections.Generic;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
namespace XBase.Business.Office.StorageManager
{
    public class StorageProductBus
    {
        /// <summary>
        /// 仓库物品列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductListBycondition(StorageProductModel model)
        {
            try
            {
                return StorageProductDBHelper.GetStorageProductTableBycondition(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取仓库物品详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageProductDetailInfo(string CompanyCD, int ID)
        {
            try
            {
                return StorageProductDBHelper.GetStorageProductDetailInfo(CompanyCD, ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 仓库物品插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertStorageProduct(StorageProductModel model)
        {
            //获取登陆用户ID
            //string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;[待修改]
            string loginUserID = "admin123";
            bool result = false;
            if (StorageProductDBHelper.InsertStorageProduct(model, loginUserID))
            {
                model.ID = IDIdentityUtil.GetIDIdentity("officedba.StorageProduct").ToString();
                result = true;
            }
            return result;
        }


        /// <summary>
        /// 更新仓库物品信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool UpdateStorageProduct(StorageProductModel model)
        {
            string loginUserID = "admin123";
            return StorageProductDBHelper.UpdateStorageProduct(model, loginUserID);
        }
    }
}
