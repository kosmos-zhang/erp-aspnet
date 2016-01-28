<%@ WebHandler Language="C#" Class="StorageNoPassInfo" %>

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

public class StorageNoPassInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";

        if (context.Request.RequestType == "POST")
        {
            //主生产任务详细信息
            int ID = int.Parse(context.Request.QueryString["ID"].ToString());
            DataTable deNoPass = new DataTable();
            DataTable deNoPassDetail = new DataTable();

            CheckNotPassModel model = new CheckNotPassModel();
            model.CompanyCD = companyCD;
            model.ID = ID;


            deNoPass =CheckNotPassBus.GetNoPassInfo(model);
            deNoPassDetail = CheckNotPassBus.GetNoPassDetailInfo(model);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataReport:");
            sb.Append(JsonClass.DataTable2Json(deNoPass));
            if (deNoPassDetail.Rows.Count > 0)
            {
                sb.Append(",");
                sb.Append("dataDetail:");
                sb.Append(JsonClass.DataTable2Json(deNoPassDetail));
            }
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