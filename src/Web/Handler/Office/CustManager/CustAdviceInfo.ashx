<%@ WebHandler Language="C#" Class="CustAdviceInfo" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.CustManager;
using XBase.Model.Office.CustManager;

public class CustAdviceInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";

        if (context.Request.RequestType == "POST")
        {
            //
            int ID = int.Parse(context.Request.QueryString["ID"].ToString());
            DataTable dt = new DataTable();

            CustAdviceModel model = new CustAdviceModel();
            model.CompanyCD = companyCD;
            model.ID = ID;
            dt = CustAdviceBus.GetOneCustAdvice(model);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataCustAdvice:");
            sb.Append(JsonClass.DataTable2Json(dt));
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