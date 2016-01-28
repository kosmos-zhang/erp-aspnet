using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using XBase.Model.CustomAPI.CustomWebSite;

namespace XBase.Business.CustomAPI.CustomWebSite
{
   public  class WebSiteOrderBus
    {

        #region 接口站点下单方法
        /// <summary>
        /// 接口站点下单方法
        /// </summary>
        /// <param name="model">订单主表信息</param>
        /// <param name="modelList">订单明细集合</param>
        /// <returns>true：成功，false：失败</returns>
        public static bool Create(WebSiteSellOrderModel model, List<WebSiteSellOrderDetailModel> modelList)
        {
            bool res= Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.Create(model, modelList);
            //如果下单成功  则短信通知


            return res;
           
        }
        #endregion

        #region 修改订单方法
        /// <summary>
        /// 修改订单方法
        /// </summary>
        /// <param name="model">站点订单主表信息</param>
        /// <param name="modelList">站点订单明细信息</param>
        /// <returns>true:成功，false：失败</returns>
        public static bool Edit(WebSiteSellOrderModel model, List<WebSiteSellOrderDetailModel> modelList)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.Edit(model, modelList);
        }
        #endregion

        #region 读取订单列表
        /// <summary>
        /// 读取站点订单列表
        /// </summary>
        /// <param name="HtParams">参数集合 key:参数名，value:对应key的值</param>
        /// <returns>返回结果DataTable</returns>
        public static DataTable GetList(Hashtable HtParams, string OrderBy)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.GetList(HtParams, OrderBy);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="HtParams">参数集合 key:参数名，value:对应key的值</param>
        /// <param name="OrderBy">排序字段</param>
        /// <param name="PageIndex">索引页码</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="TotalCount">页数</param>
        /// <returns></returns>
        public static DataTable GetList(Hashtable HtParams, string OrderBy, int PageIndex, int PageSize, ref int TotalCount)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.GetList(HtParams, OrderBy, PageIndex, PageSize, ref TotalCount);
        }

        #endregion

        #region 读取订单主表详细信息
        /// <summary>
        /// 读取订单详细信息
        /// </summary>
        /// <param name="OrderID">订单主键</param>
        /// <returns>包含订单信息的数据集合</returns>
        public static DataTable GetOrderInfo(string OrderNo, string CompanyCD)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.GetOrderInfo(OrderNo, CompanyCD);
        }
        #endregion

        #region 读取明细信息
        public static DataTable GetDetailInfo(string OrderNo, string CompanyCD)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteOrderDBHelper.GetDetailInfo(OrderNo, CompanyCD);
        }
        #endregion


    }
}
