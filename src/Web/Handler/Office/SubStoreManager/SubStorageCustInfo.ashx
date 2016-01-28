<%@ WebHandler Language="C#" Class="SubStorageCustInfo" %>

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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;

public class SubStorageCustInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //string companyCD = "AAAAAA";

        if (context.Request.RequestType == "POST")
        {
            
            string ID = context.Request.QueryString["ID"].ToString();
            DataTable deNoPass = new DataTable();

            SubSellCustInfoModel model = new SubSellCustInfoModel();
            model.CompanyCD = UserInfo.CompanyCD;
            model.ID = ID;

            deNoPass = SubSellOrderBus.GetOneData(int.Parse(model.ID));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataReport:");
            sb.Append(JsonClass.DataTable2Json(deNoPass));
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