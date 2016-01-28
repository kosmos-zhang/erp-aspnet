<%@ WebHandler Language="C#" Class="PurchaseContractUC2" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Model.Office.ProductionManager;
using System.Web.SessionState;

public class PurchaseContractUC2 : IHttpHandler,System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string CompanyCD = string.Empty;
        
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         
        
        string Action = context.Request.Form["action"];

        if (Action == "Select")
        {
            string OrderBy = context.Request.Form["OrderBy"].ToString().Trim();
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int pageSize = int.Parse(context.Request.Form["pageSize"].ToString());//每页显示记录数
            string ContractNo = context.Request.Form["No"].ToString().Trim();
            string Title = context.Request.Form["Title"].ToString().Trim();
            int ProviderID = 0;
            try
            {
                ProviderID = int.Parse(context.Request.Form["ProviderID"].ToString().Trim());
            }
            catch
            {
                ProviderID = 0;
            }
            int CurrencyType = 0;
            try
            {
                CurrencyType = int.Parse(context.Request.Form["CurrencyID"].ToString().Trim());
            }
            catch
            {
                CurrencyType = 0;
            }
            
            int totalRecord = 0;

            DataTable dtdata = PurchaseOrderBus.GetContractDetail(CompanyCD, ContractNo, Title, ProviderID, CurrencyType, pageIndex, pageSize, OrderBy, out totalRecord);
            DataTable dt = GetNewDataTable(dtdata, string.Empty, OrderBy);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalRecord.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("\"\"");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        else if (Action == "Pri")
        {
            int ID = int.Parse(context.Request.Form["ID"].ToString());
            DataTable dt = PurchaseOrderBus.GetContract(CompanyCD, ID);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            if (dt.Rows.Count == 0)
                sb.Append("\"\"");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
    private DataTable GetNewDataTable(DataTable dt, string condition, string Order)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition, Order);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}