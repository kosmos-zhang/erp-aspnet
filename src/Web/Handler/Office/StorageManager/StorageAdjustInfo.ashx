<%@ WebHandler Language="C#" Class="StorageAdjustInfo" %>

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

public class StorageAdjustInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";

        if (context.Request.RequestType == "POST")
        {
            //
            int ID = int.Parse(context.Request.QueryString["ID"].ToString());
            DataTable deAdjust = new DataTable();
            DataTable deAdjustDetail = new DataTable();

            StorageAdjustModel model = new StorageAdjustModel();
            model.CompanyCD = companyCD;
            model.ID = ID;


            deAdjust =StorageAdjustBus.GetAdjustInfo(model);
            deAdjustDetail = StorageAdjustBus.GetAdjustDetail(model);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataAdjust:");
            sb.Append(JsonClass.DataTable2Json(deAdjust));
            if (deAdjustDetail.Rows.Count > 0)
            {
                sb.Append(",");
                sb.Append("dataDetail:");
                sb.Append(JsonClass.DataTable2Json(deAdjustDetail));
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