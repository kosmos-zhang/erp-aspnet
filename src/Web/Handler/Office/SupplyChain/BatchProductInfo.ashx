<%@ WebHandler Language="C#" Class="BatchProductInfo" %>
/**********************************************
 * 类作用：   匹配物品信息Handler层处理
 * 建立人：   王玉贞
 * 建立时间： 2010/06/12
 ***********************************************/
using System;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using XBase.Business.Office.SupplyChain;
using System.Data;
using XBase.Common;

public class BatchProductInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


        if (context.Request.RequestType == "POST")
        {
            string ProdNo = context.Request.QueryString["ProdNo"].ToString();


            DataTable dbBatch = ProductInfoBus.GetMatchProductInfo(companyCD, ProdNo);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataBatch:");
            sb.Append(JsonClass.DataTable2Json(dbBatch));
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