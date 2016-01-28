using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;

using XBase.Model.CustomAPI.CustomWebSite;
using XBase.Business.CustomAPI.CustomWebSite;

namespace CustomAPI.CustomWebSite
{
    /// <summary>
    /// WebSiteInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebSiteInfo : System.Web.Services.WebService
    {

        #region  登陆
        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="userName">登陆名</param>
        /// <param name="password">登陆密码（加密）</param>
        /// <returns>支持可序列化的DataTable</returns>
        [WebMethod]
        public DataTable Login(string userName, string password)
        {
            DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteCustomInfoBus.Login(userName, password);
            dtInfo.TableName = "LoginInfo";
            return dtInfo;
        }

        #endregion

        #region 读取产品
        /// <summary>
        /// 读取产品列表
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetProductList(string CompanyCD)
        {
            DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteProductInfoBus.GetProductList(CompanyCD);
            dtInfo.TableName = "ProductList";
            return dtInfo;
        }


        [WebMethod]
        public DataTable GetProductListWithPage(string CompanyCD, int PageIndex, int PageSize, ref int TotalCount, string OrderBy, string[] arrParams)
        {
            DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteProductInfoBus.GetProductList(CompanyCD, PageIndex, PageSize, ref TotalCount, OrderBy, arrParams);

            dtInfo.TableName = "ProductList";
            return dtInfo;

        }

        [WebMethod]
        public DataTable GetProductByID(string id)
        {
            DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteProductInfoBus.GetProdcutByID(id);
            dtInfo.TableName = "ProductList";
            return dtInfo;
        }


        #endregion

        #region 多单位控制
        #region  判断是否启用多单位
        /// <summary>
        /// 判断是否启用多单位
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>true：启用，false：不启用</returns>
        [WebMethod]
        public bool IsMulUnit(string CompanyCD)
        {
           return WebSiteUnitControlBus.IsMulUnit(CompanyCD);
            //return false;
        }
        #endregion

        #region  读取指定单位换算组
        [WebMethod]
        public DataTable GetUnitGroup(string UnitGroupNo,string CompanyCD)
        {
            DataTable dtInfo= WebSiteUnitControlBus.GetUnitGroup(UnitGroupNo,CompanyCD);
            dtInfo.TableName = "UnitGroup";
            return dtInfo;
        }
        #endregion
        #endregion

        #region 订单部门
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="model">主信息</param>
        /// <param name="modelList">明细信息</param>
        /// <returns></returns>
        [WebMethod]
        public bool CreateOrder(WebSiteSellOrderModel model, List<WebSiteSellOrderDetailModel> modelList)
        {
            return WebSiteOrderBus.Create(model, modelList);
        }
        #endregion

        #region 读取订单
        /// <summary>
        /// 读取订单列表
        /// </summary>
        /// <param name="CustomID"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="OrderNo"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetOrdertList(string CustomID,string CompanyCD,string OrderNo,string OrderBy, int PageIndex, int PageSize, ref int TotalCount)
        {
            Hashtable htParams = new Hashtable();
            if (!string.IsNullOrEmpty(CustomID))
            {
                htParams.Add("CustomID", CustomID);
            }
            if (!string.IsNullOrEmpty(OrderNo))
            {
                htParams.Add("OrderNo", OrderNo);
            }
            htParams.Add("CompanyCD", CompanyCD);

            DataTable dtInfo = XBase.Business.CustomAPI.CustomWebSite.WebSiteOrderBus.GetList(htParams, OrderBy, PageIndex, PageSize, ref TotalCount);
            dtInfo.TableName = "OrderList";
            return dtInfo;
        }

        /// <summary>
        /// 读取订单明细
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetOrderMainInfo(string OrderNo, string CompanyCD)
        {
            DataTable dt= XBase.Business.CustomAPI.CustomWebSite.WebSiteOrderBus.GetOrderInfo(OrderNo, CompanyCD);
            dt.TableName = "MainInfo";
            return dt;
        }

        /// <summary>
        /// 读取明细信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetOrderDetailInfo(string OrderNo, string CompanyCD)
        {
            DataTable dt= XBase.Business.CustomAPI.CustomWebSite.WebSiteOrderBus.GetDetailInfo(OrderNo, CompanyCD);
            dt.TableName = "DetailInfo";
            return dt;
        }

        
        #endregion

        #region 修改用户信息
        #region 修改密码
        [WebMethod]
        public bool ChangePwd( string newPwd,int id)
        {
            WebSiteCustomInfoModel item = new WebSiteCustomInfoModel();
            item.ID = id;
            item.LoginPassword = newPwd;

            return XBase.Business.CustomAPI.CustomWebSite.WebSiteCustomInfoBus.SetPassword(item);
        }
        #endregion

        #endregion



    }
}
