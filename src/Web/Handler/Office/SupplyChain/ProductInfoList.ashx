<%@ WebHandler Language="C#" Class="ProductInfoList" %>

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

public class ProductInfoList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                //设置行为参数
                string orderString = (context.Request.Form["orderByP"].ToString());//排序
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
                if (context.Request.Form["ProductID"].ToString() != "")
                {
                    model.ProdNo = context.Request.Form["ProductID"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["ProdName"].ToString().Trim()))
                {
                    model.ProductName = context.Request.Form["ProdName"].ToString();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["PYShort"].ToString().Trim()))
                {
                    model.PYShort = context.Request.Form["PYShort"].ToString();
                }
                string QueryID = context.Request.Form["QueryID"].ToString();
                if (!string.IsNullOrEmpty(context.Request.Form["typeid"].ToString().Trim()))
                {
                    model.TypeID = context.Request.Form["typeid"].ToString();
                }
                if (context.Request.Form["Specification"].ToString() != "")
                {
                    string Specification = context.Request.Form["Specification"].ToString().Trim();
                    model.Specification = Specification.Replace("&#174", "×"); 
                }
                if (context.Request.Form["Manufacturer"].ToString() != "")
                {
                    model.Manufacturer = context.Request.Form["Manufacturer"].ToString().Trim();
                }
                if (context.Request.Form["FromAddr"].ToString() != "")
                {
                    model.FromAddr = context.Request.Form["FromAddr"].ToString().Trim();
                }
                if (context.Request.Form["Material"].ToString() != "")
                {
                    model.Material = context.Request.Form["Material"].ToString().Trim();
                }
                if (context.Request.Form["StartStorage"].ToString() != "")
                {
                    model.StartStorage = context.Request.Form["StartStorage"].ToString().Trim();
                }
                if (context.Request.Form["EndStorage"].ToString() != "")
                {
                    model.EndStorage = context.Request.Form["EndStorage"].ToString().Trim();
                }
                if (context.Request.Form["ColorID"] != null)
                {
                    if (context.Request.Form["ColorID"].ToString() != "")
                    {
                        model.ColorID = context.Request.Form["ColorID"].ToString().Trim();
                    }
                }
                string EFIndex = "";
                string EFDesc = "";
                if (context.Request.Form["EFIndex"] != null && context.Request.Form["EFDesc"] != null)
                {
                    EFIndex = context.Request.Form["EFIndex"].ToString().Trim();
                    EFDesc = context.Request.Form["EFDesc"].ToString().Trim();
                }
                model.CompanyCD = companyCD;
                DataTable dt = ProductInfoBus.GetProductInfoTableBycondition(model, QueryID, EFIndex, EFDesc, pageIndex, pageCount, ord, ref totalCount);
                XBase.Common.StringUtil.DecimalFormatPoint(int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint), dt);
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