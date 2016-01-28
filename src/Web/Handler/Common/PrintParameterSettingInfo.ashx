<%@ WebHandler Language="C#" Class="PrintParameterSettingInfo" %>

using System;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Model.Common;
using System.Data;
using XBase.Business.Common;

public class PrintParameterSettingInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        if (context.Request.RequestType == "POST")
        {
            DataTable dtPrint = new DataTable();


            PrintParameterSettingModel model = new PrintParameterSettingModel();
            model.CompanyCD = companyCD;
            model.BillTypeFlag = int.Parse(context.Request.Params["BillTypeFlag"].ToString());
            model.PrintTypeFlag = int.Parse(context.Request.Params["PrintTypeFlag"].ToString());
            dtPrint = PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataPrint:");
            sb.Append(JsonClass.DataTable2Json(dtPrint));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}