<%@ WebHandler Language="C#" Class="ProductPriceList" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;

public class ProductPriceList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
   //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int totalCount = 0;
            string ord = orderBy + " " + order;
            //获取数据
            ProductPriceChangeModel model = new ProductPriceChangeModel();
            model.ChangeNo = context.Request.Form["ChangeNo"].ToString().Trim();
            model.Title = context.Request.Form["Title"].ToString().Trim();
            model.ProductID = context.Request.Form["ProductID"].ToString().Trim();
            model.Chenger = context.Request.Form["Chenger"].ToString().Trim();
            string OpenDate = context.Request.Form["OpenDate"].ToString().Trim();
            string CloseDate = context.Request.Form["CloseDate"].ToString().Trim();
            //model.CompanyCD = "AAAAAA";
            model.CompanyCD = companyCD;
            //linq排序
            DataTable dt = ProductPriceChangeBus.GetProductPriceInfo(model, OpenDate, CloseDate, pageIndex, pageCount, ord, ref totalCount);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
            }
            catch
            {
                
                
            }
         
        }
    }
  

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
 

}