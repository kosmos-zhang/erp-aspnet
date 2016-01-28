<%@ WebHandler Language="C#" Class="ProductInfo" %>

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
using XBase.Business.Office.SystemManager;
public class ProductInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            if (context.Request.QueryString["action"].Trim() == "Change")
            {
                string PID = context.Request.QueryString["ID"].ToString().Trim();
                string TypeFlag = context.Request.QueryString["TypeFlag"].ToString().Trim();
                //id = CategorySetBus.GetProductTypeInfo(PID,TypeFlag);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(PID);
                JsonClass jc = new JsonClass("success", PID.ToString(), 2);
                context.Response.Write(jc);
                context.Response.End();

            }
            else if (context.Request.QueryString["action"].Trim() == "Load")
            {
                try
                {
                    string companyCD = string.Empty;
                    companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
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
                    ProductInfoModel model = new ProductInfoModel();
                    if (context.Request.Form["ProdNo"].ToString() != "")
                    {
                        model.ProdNo = context.Request.Form["ProdNo"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["ProductName"].ToString().Trim()))
                    {
                        model.ProductName = context.Request.Form["ProductName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["TypeID"].ToString().Trim()))
                    {
                        model.TypeID = context.Request.Form["TypeID"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["UsedStatus"].ToString().Trim()))
                    {
                        model.UsedStatus = context.Request.Form["UsedStatus"].ToString();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["PYShort"].ToString().Trim()))
                    {
                        model.PYShort = context.Request.Form["PYShort"].ToString();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["Specification"].ToString().Trim()))
                    {
                        string Specification = context.Request.Form["Specification"].ToString().Trim();
                        model.Specification = Specification.Replace("&#174", "×"); 
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["BarCode"].ToString().Trim()))
                    {
                        model.BarCode = context.Request.Form["BarCode"].ToString();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["CheckStatus"].ToString().Trim()))
                    {
                        model.CheckStatus = context.Request.Form["CheckStatus"].ToString();
                    }
                    if (!string.IsNullOrEmpty(context.Request.Form["Color"].ToString().Trim()))
                    {
                        model.ColorID = context.Request.Form["Color"].ToString();
                    }
                    ///*解析自定义属性参数*/.

                    string EFIndex = "";
                    string EFDesc = "";
                    if (context.Request.Form["EFIndex"] != null && context.Request.Form["EFDesc"] != null)
                    {
                        EFIndex = context.Request.Form["EFIndex"].ToString().Trim();
                        EFDesc = context.Request.Form["EFDesc"].ToString().Trim();
                    }
                    model.CompanyCD = companyCD;
                    DataTable dt = ProductInfoBus.GetProductInfo(model, EFIndex, EFDesc, pageIndex, pageCount, ord, ref totalCount);
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
                catch (Exception)
                {
                }

            }
            else if (context.Request.QueryString["action"].Trim() == "Info")
            {
                int ID = int.Parse(context.Request.QueryString["ID"].ToString());
                DataTable DtProduct = ProductInfoBus.GetProductInfoByAttr(ID);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("dataBase:");
                sb.Append(JsonClass.DataTable2Json(DtProduct));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();

            }
            else if (context.Request.QueryString["action"].Trim() == "LoadUnit")
            {
                if (!string.IsNullOrEmpty(context.Request.QueryString["GroupUnitNo"].ToString().Trim()))
                {
                    string GroupUnitNo = context.Request.QueryString["GroupUnitNo"].ToString().Trim();
                    DataTable dt_GroupUnit = ProductInfoBus.GetUnitGroupList(GroupUnitNo);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("{");
                    sb.Append("data:");
                    sb.Append(JsonClass.DataTable2Json(dt_GroupUnit));
                    sb.Append("}");

                    context.Response.ContentType = "text/plain";
                    context.Response.Write(sb.ToString());
                    context.Response.End();
                }
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