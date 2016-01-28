<%@ WebHandler Language="C#" Class="ImportLog" %>

using System;
using System.Web;

using System.Data;
using XBase.Common;
using XBase.Business.Office.SupplyChain;
public class ImportLog : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string orderString = context.Request.Form["orderby"].ToString();
            string order = "desc";
            string orderBy = string.IsNullOrEmpty(orderString) ? "ExportDate" : orderString.Substring(0, orderString.Length - 2);
            if (orderString.EndsWith("_d"))
            {
                order = "asc";
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());
            int totalCount = 0;
            string ord = orderBy + " " + order;

            try
            {
                string userid = context.Request.Form["userID"].ToString();
                string begindate = context.Request.Form["OpenDate"].ToString();
                string enddate = context.Request.Form["CloseDate"].ToString();
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string mod = context.Request.Form["mod"].ToString();
                string usercode = string.Empty;
                if (userid.Length > 0)
                {
                    string[] userArray = userid.Split(',');
                    foreach (string temp in userArray)
                    {
                        if (usercode.Length < 1)
                        {
                            usercode = "'" + temp + "'";
                        }
                        else
                        {
                            usercode = usercode + ",'" + temp + "'";
                        }
                    }
                }

                DataTable dt = ProductInfoBus.GetLogPage(usercode, begindate, enddate, mod, CompanyCD, pageIndex, pageCount, ord, ref totalCount);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count > 0)
                {
                    sb.Append(JsonClass.DataTable2Json(dt));
                }
                else
                {
                    sb.Append("[{\"ID\":\"\"}]");
                }

                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString().Replace("'","‘"));
                //context.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch(Exception ex) {
                string str = ex.Message;
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