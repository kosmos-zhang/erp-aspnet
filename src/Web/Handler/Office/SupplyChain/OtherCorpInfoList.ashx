<%@ WebHandler Language="C#" Class="OtherCorpInfoList" %>

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

public class OtherCorpInfoList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                OtherCorpInfoModel model = new OtherCorpInfoModel();
                model.BigType = context.Request.Form["BigType"].ToString().Trim();
                model.CustNo = context.Request.Form["CustNo"].ToString();
                model.CustName = context.Request.Form["CustName"].ToString();
                model.isTax = context.Request.Form["isTax"].ToString();
                model.UsedStatus = context.Request.Form["UsedStatus"].ToString();
                model.AreaID = context.Request.Form["BillType"].ToString();
                DataTable dt = OtherCorpInfoBus.SearchRectOtherCorpInfo(model, pageIndex, pageCount, ord, ref totalCount);
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
            { }
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