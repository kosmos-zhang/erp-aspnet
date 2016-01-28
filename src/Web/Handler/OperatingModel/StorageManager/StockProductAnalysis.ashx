<%@ WebHandler Language="C#" Class="StockProductAnalysis" %>

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

public class StockProductAnalysis : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (context.Request.RequestType == "POST")
        {
            StockAccountModel model = new StockAccountModel();
            model.CompanyCD = companyCD;
            model.ProductNo = context.Request.Form["txtProductNo"].Trim();
            DataTable dt1 = StockAccountBus.GetProdcutSellandPurchaseInfo(model);
            DataTable dt2 = StockAccountBus.GetProductStockAndPriceInfo(model);
            DataTable dt3 = StockAccountBus.GetProdcutLoseInfo(model);
            DataTable dt4 = StockAccountBus.GetStoProductInfo(model);
            string strJson = string.Empty;
            strJson = "{";
            strJson += "\"DateSellandPurchase\":" + JsonClass.DataTable2Json(dt1);
            strJson += ",\"DateStockandPrice\":" + JsonClass.DataTable2Json(dt2);
            strJson += ",\"DateLoss\":" + JsonClass.DataTable2Json(dt3);
            strJson += ",\"DateStockInfo\":" + JsonClass.DataTable2Json(dt4);
            strJson += "}";
            context.Response.Write(strJson);
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