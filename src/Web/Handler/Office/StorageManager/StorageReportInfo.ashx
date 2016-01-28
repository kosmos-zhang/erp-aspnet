<%@ WebHandler Language="C#" Class="StorageReportInfo" %>

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

public class StorageReportInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";

        if (context.Request.RequestType == "POST")
        {

            int ID = int.Parse(context.Request.QueryString["ID"].ToString());
            // string BillStatus = context.Request.QueryString["BillStatusID"].ToString();
            DataTable dtReport = new DataTable();
            DataTable dtReportDetail = new DataTable();

            StorageQualityCheckReportModel model = new StorageQualityCheckReportModel();
            model.CompanyCD = companyCD;
            model.ID = ID;
            string IsTransfer = "0";
            string IsFlow = "0";
            //if (BillStatus == "2")
            //{
            IsTransfer = CheckReportBus.IsTransfer(ID, companyCD);
            // }
            // if (BillStatus == "1")
            // {
            IsFlow = CheckReportBus.IsFlow(ID);
            //}
            dtReport = CheckReportBus.GetReportInfo(model);
            dtReportDetail = CheckReportBus.GetReportDetailInfo(model);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("dataReport:");
            sb.Append(JsonClass.DataTable2Json(dtReport));
            if (dtReportDetail.Rows.Count > 0)
            {
                sb.Append(",");
                sb.Append("dataDetail:");
                sb.Append(JsonClass.DataTable2Json(dtReportDetail));
            }
            sb.Append(",IsTransfer:" + IsTransfer + "");
            sb.Append(",IsFlow:" + IsFlow + "");
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