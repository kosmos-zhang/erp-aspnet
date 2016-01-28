<%@ WebHandler Language="C#" Class="ApprovalFlowList" %>

using System;
using System.Web;
using XBase.Business.Office.SystemManager;
using System.Data;
using XBase.Common;
public class ApprovalFlowList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        //string companyCD = "AAAAAA";
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (context.Request.RequestType == "POST")
        {
            string ID = context.Request.QueryString["FlowNo"].ToString();
            DataTable DT = ApprovalFlowSetBus.GetFlowInfoByFlowId(ID,companyCD);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(DT));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
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