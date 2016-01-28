<%@ WebHandler Language="C#" Class="PurchaseRejectEdit" %>

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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;

public class PurchaseRejectEdit : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //获取该界面有没有被引用标识
            //string RejectNo = context.Request.Params["RejectNo"].ToString().Trim();
            int ID = Convert.ToInt32(context.Request.Params["ID"].ToString().Trim());
            bool IsCite = PurchaseRejectBus.IsCitePurchaseReject(ID);

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string formType = string.Empty;
            DataTable dtp = PurchaseRejectBus.SelectReject(ID);
            if (dtp != null)
            {
                if (dtp.Rows.Count > 0)
                {
                    formType = dtp.Rows[0]["FromType"] == null ? "" : dtp.Rows[0]["FromType"].ToString();
                } 
            }
            DataTable dt2 = PurchaseRejectBus.Details(ID,formType );

            string str = "\"" + IsCite + "\"";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (dt2.Rows.Count >0)
            {
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dtp));
                sb.Append(",data2:");
                sb.Append(JsonClass.DataTable2Json(dt2));
                sb.Append(",IsCite:[{");
                sb.Append("IsCite:" + str);
                sb.Append("}]}");

            }
            else
            {
         

                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dtp));
                sb.Append(",data2:[{");
                sb.Append(" ID:null}]");
                sb.Append(",IsCite:[{");
                sb.Append("IsCite:" + str);
                sb.Append("}]}");
            }

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