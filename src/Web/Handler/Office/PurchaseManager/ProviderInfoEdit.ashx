<%@ WebHandler Language="C#" Class="ProviderInfoEdit" %>

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

public class ProviderInfoEdit : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //获取该界面有没有被引用标识
            int ID = Convert.ToInt32(context.Request.Params["ID"].ToString().Trim());

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //bool isValue = ProviderInfoBus.isValue(ID);//先判断采购订单中有没有符合条件的供应商相关信息
            //bool isValues = ProviderInfoBus.isValues(ID);//先判断采购退货中有没有符合条件的供应商相关信息
            //if (isValue == true)
            //{
                
            //    if (isValues == true)
            //    {
            //        DataTable dtp = ProviderInfoBus.SelectProviderInfo(ID);
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("{");
            //        sb.Append("data:");
            //        sb.Append(JsonClass.DataTable2Json(dtp));
            //        sb.Append("}");


            //        context.Response.ContentType = "text/plain";
            //        context.Response.Write(sb.ToString());
            //        context.Response.End();
            //    }
            //    else
            //    {
            //        DataTable dtp = ProviderInfoBus.SelectProviderInfo(ID);
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("{");
            //        sb.Append("data:");
            //        sb.Append(JsonClass.DataTable2Json(dtp));
            //        sb.Append("}");


            //        context.Response.ContentType = "text/plain";
            //        context.Response.Write(sb.ToString());
            //        context.Response.End();
            //    }
            //}
            //else
            //{
            //    if (isValues == true)
            //    { }
            //    else
            //    {
            //        DataTable dtp = ProviderInfoBus.SelectProviderInfo2(ID);
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("{");
            //        sb.Append("data:");
            //        sb.Append(JsonClass.DataTable2Json(dtp));
            //        sb.Append("}");


            //        context.Response.ContentType = "text/plain";
            //        context.Response.Write(sb.ToString());
            //        context.Response.End();
            //    }
            //}

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("{");
            //sb.Append("data:");
            //sb.Append(JsonClass.DataTable2Json(dtp));
            //sb.Append("}");


            //context.Response.ContentType = "text/plain";
            //context.Response.Write(sb.ToString());
            //context.Response.End();


            DataTable dtp = ProviderInfoBus.SelectProviderInfo(ID);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(dtp));
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