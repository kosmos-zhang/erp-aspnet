
/**********************************************
 * 类作用   商品扩展表业务处理层
 * 创建人   xz
 * 创建时间 2010-3-23 13:53:21 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.CustomAPI.CustomWebSite;
using XBase.Data.CustomAPI.CustomWebSite;


namespace XBase.Business.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 商品扩展表业务类
    /// </summary>
    public class WebSiteProductInfoBus
    {
        #region 默认方法
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">主键，自动生成</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return WebSiteProductInfoDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="productID">物品ID,对应表Officedba.ProductInfo</param>

        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(int productID)
        {
            return WebSiteProductInfoDBHelper.SelectWithKey(productID);
        }


        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , WebSiteProductInfoModel model, string CompanyCD, string productName)
        {
            return WebSiteProductInfoDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model, CompanyCD, productName);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(WebSiteProductInfoModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = WebSiteProductInfoDBHelper.InsertCommand(model);

            sqlList.Add(cmd);

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                int i = 0;
                if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                {
                    model.ID = i;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(WebSiteProductInfoModel model)
        {
            return WebSiteProductInfoDBHelper.Update(model);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return WebSiteProductInfoDBHelper.Delete(iD);
        }

        #endregion

        #region 自定义
        /// <summary>
        /// 判断产品是否已经存在
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <param name="ID">商品ID</param>
        /// <returns></returns>
        public static bool ExisitProduct(int productID, int? ID)
        {
            return WebSiteProductInfoDBHelper.ExisitProduct(productID, ID);
        }

        /// <summary>
        /// 获得产品图片
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public static string GetProductImage(int productID)
        {
            return WebSiteProductInfoDBHelper.GetProductImage(productID);
        }

        #region 读取产品列表
        /// <summary>
        /// 读取产品列表
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetProductList(string CompanyCD)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteProductInfoDBHelper.GetProductList(CompanyCD);
        }

        /// <summary>
        /// 带分页的产品列表方法
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="OrderBy">排序字段 例如：ID DESC</param>
        /// <returns></returns>
        public static DataTable GetProductList(string CompanyCD, int PageIndex, int PageSize, ref int TotalCount, string OrderBy,string[] arrParams)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteProductInfoDBHelper.GetProductList(CompanyCD, PageIndex, PageSize, ref TotalCount, OrderBy, arrParams);
        }

        #endregion

        #region 根据指定的物品ID读取数据
        public static DataTable GetProdcutByID(string id)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteProductInfoDBHelper.GetProdcutByID(id);
        }
        #endregion
        #endregion

    }
}



