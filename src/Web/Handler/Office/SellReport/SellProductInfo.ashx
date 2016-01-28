<%@ WebHandler Language="C#" Class="SellProductInfo" %>

using System;
using System.Web;
using XBase.Model.Office.SellReport;
using XBase.Business.Office.SellReport;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using XBase.Common;


public class SellProductInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string actionstr = context.Request.Form["action"].ToString();
            switch (actionstr)
            {
                case "addtable":
                    AddTable(context);//添加记录
                    break;
                case "datalist":
                    DataList(context);
                    break;
                case "deldata":
                    DelData(context);
                    break;
            }
        }
    }

    private void AddTable(HttpContext context)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string productno = context.Request.Params["productno"].ToString().Trim();
        string productname = context.Request.Params["productname"].ToString().Trim();
        string price = context.Request.Params["price"].ToString().Trim();
        string brief = context.Request.Params["brief"].ToString().Trim();
        string memo = context.Request.Params["memo"].ToString().Trim();
        string id = context.Request.Params["id"].ToString().Trim();
        int result = 0;
        if (price == "")
        {
            price = "0"; 
        }
        if (id != "0")
        {
            UserProductInfo model = new UserProductInfo(int.Parse(id),userInfo.CompanyCD, productno, productname, decimal.Parse(price), brief, memo);
            result = UserProjectInfoBus.Update(model);
        }
        else
        {
            UserProductInfo model = new UserProductInfo(userInfo.CompanyCD, productno, productname, decimal.Parse(price), brief, memo);
            result = UserProjectInfoBus.Add(model);
        }
        
        context.Response.Write(result.ToString());
    }

    private void DataList(HttpContext context)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        StringBuilder sb = new StringBuilder();
        string orderBy = context.Request.Params["orderBy"].ToString().Trim();
        int pageindex = int.Parse(context.Request.Params["pageIndex"].ToString().Trim());
        int pagecount = int.Parse(context.Request.Params["pageCount"].ToString().Trim());
        int totalCount = 0;
        DataTable dt = UserProjectInfoBus.DataList(pageindex, pagecount, orderBy, userInfo, ref totalCount);
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        sb.Append("");
        context.Response.Write(sb.ToString());
    }

    private void DelData(HttpContext context)
    {
        string idlist = context.Request.Params["ID"].ToString().Trim();
        int num = UserProjectInfoBus.Delete(idlist);
        context.Response.Write(num.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}