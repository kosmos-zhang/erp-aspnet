<%@ WebHandler Language="C#" Class="StorageQualityEdit" %>

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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;

public class StorageQualityEdit : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            JsonClass jc = new JsonClass();
            if (context.Request.Form["action"] != null)
            {
                if (context.Request.Form["action"].ToString() == "DELETE")
                {
                    string ID = context.Request.Form["id"].ToString();
                    StorageQualityCheckPro.DeleteQualityDB(ID);
                }
            }
            else
            {
                int ID = int.Parse(context.Request.QueryString["ID"].ToString().Trim());
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string IsTransfer = "0";
                string IsFlow = "0";
                IsTransfer = StorageQualityCheckPro.IsTransfer(ID, CompanyCD);
                IsFlow = StorageQualityCheckPro.IsFlow(ID);
                DataTable dtp = StorageQualityCheckPro.GetOneQuality(ID, CompanyCD);
                DataTable dt = StorageQualityCheckPro.GetQualityDetail(ID, CompanyCD);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("{");
                sb.Append("datap:");
                sb.Append(JsonClass.DataTable2Json(dtp));
                if (dt.Rows.Count > 0)
                {
                    sb.Append(",");
                    sb.Append("data:");
                    sb.Append(JsonClass.DataTable2Json(dt));
                }
                sb.Append(",IsTransfer:" + IsTransfer + "");
                sb.Append(",IsFlow:" + IsFlow + "");
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
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