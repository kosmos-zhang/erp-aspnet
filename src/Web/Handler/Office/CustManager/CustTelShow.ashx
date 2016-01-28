<%@ WebHandler Language="C#" Class="CustTelShow" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Common;
using System.Data;
using XBase.Business.Office.CustManager;

public class CustTelShow : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["action"].ToString()).Trim();//操作 
            switch (action)
            {
                case "TelLogList":
                    TelLogList(context);                   
                    break;
                case "CallLogAdd":
                    CallLogAdd(context);                   
                    break;
                case "HaveCust":
                    HaveCust(context);                   
                    break;
                case "GetCustInfo":
                    GetCustInfo(context);
                    break;
                case "GetCust_ts":
                    GetCust_ts(context);
                    break;
                case "GetCust_fw":
                    GetCust_fw(context);
                    break;
                case "GetCust_ll":
                    GetCust_ll(context);
                    break;
                case "CallLoad":
                    CallLoad(context);
                    break;
                case "CallEdit":
                    CallEdit(context);
                    break;
                case "CustCallInfo":
                    CustCallInfo(context);
                    break;
                case "LoadCustInfo":
                    LoadCustInfo(context);
                    break;
                    
                default:
                    break;
            }
        }
    }

    //根据客户ID或CustNo
    private void LoadCustInfo(HttpContext context)
    {
        string id = context.Request.Params["ID"].ToString().Trim();

        DataTable dt = CustCallBus.GetCustInfoByID(id);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        // context.Response.End();
    }

    //页面来电记录列表
    private void CustCallInfo(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustCallModel CustCallM = new CustCallModel();
        CustCallM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustCallM.CustID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustCallM.Tel = context.Request.Params["Tel"].ToString().Trim();
        
        string DateBegin = context.Request.Form["DateBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["DateEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCustCallByCon(CustCallM,DateBegin,DateEnd, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    //根据电话记录id修改电话记录
    private void CallEdit(HttpContext context)
    {
        JsonClass jc;
        CustCallModel CustCallM = new CustCallModel();
        CustCallM.ID = Convert.ToInt32(context.Request.Params["CallID"].ToString().Trim());
        CustCallM.Callor = context.Request.Params["Callor"].ToString().Trim();
        CustCallM.CallContents = context.Request.Params["CallContents"].ToString().Trim();//通话内容
        CustCallM.ModifiedDate = System.DateTime.Now.ToString();
        CustCallM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        CustCallM.Title = context.Request.Params["Title"].ToString().Trim();//通话Title

        if (CustCallBus.UpdateCallBuID(CustCallM))
            jc = new JsonClass("001", CustCallM.ID.ToString(), 1);
        else
            jc = new JsonClass("faile", "", 0);
        context.Response.Write(jc);
    }

    //根据电话记录id获取电话记录
    private void CallLoad(HttpContext context)
    {
        string id = context.Request.Params["id"].ToString().Trim();


        DataTable dt = CustCallBus.GetCallInfoByID(id);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        // context.Response.End();
    }

    private void GetCust_ll(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_ll"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "LinkDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_ll"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_ll"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        ContactHistoryModel ContactHistoryM = new ContactHistoryModel();

        ContactHistoryM.CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        ContactHistoryM.CustID = Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCustContactByCustID(ContactHistoryM, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    private void GetCust_fw(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_fw"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "BeginDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_fw"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_fw"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustServiceModel CustServiceM = new CustServiceModel();

        CustServiceM.CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        CustServiceM.CustID = Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCustServiceByCustID(CustServiceM, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    private void GetCust_ts(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_ts"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ComplainDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_ts"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_ts"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustComplainModel CustComplainM = new CustComplainModel();

        CustComplainM.CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        CustComplainM.CustID = Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCustComplainByCustID(CustComplainM, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    //根据客户id获取客户信息
    private void GetCustInfo(HttpContext context)
    {
        string ID = context.Request.Params["CustID"].ToString().Trim();
        string Tel = context.Request.Params["Tel"].ToString().Trim();
        string CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();

        DataTable dt = CustCallBus.GetCustInfoByID(ID,Tel,CompanyCD);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        // context.Response.End();
    }

    //根据来电号码判断是否有对应客户
    private void HaveCust(HttpContext context)
    {
        string CustTel = context.Request.Params["CustTel"].ToString().Trim();
        string CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        DataTable dt = CustCallBus.GetCustInfoByTel(CompanyCD, CustTel);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
       // context.Response.End();
    }
    
    //自动添加来电记录
    private void CallLogAdd(HttpContext context)
    {
        CustCallModel CustCallM = new CustCallModel();
        CustCallM.CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        CustCallM.Tel = context.Request.Params["CustTel"].ToString().Trim();
        CustCallM.Creator = context.Request.Params["EmployeeID"].ToString().Trim();
        CustCallM.CustID = Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustCallM.ModifiedDate = System.DateTime.Now.ToString();        

        JsonClass jc;

        bool CallID = CustCallBus.AddCustCallByTel(CustCallM);

        if (CallID)//自动添加成功
            jc = new JsonClass("001", "1", 1);
        else
            jc = new JsonClass("faile", "", 0);
        context.Response.Write(jc);
    }
    
    private void TelLogList(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CallTime";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustCallModel CustCallM = new CustCallModel();

        CustCallM.CompanyCD = context.Request.Params["CompanyCD"].ToString().Trim();
        CustCallM.Creator = context.Request.Params["EmployeeID"].ToString().Trim();
        CustCallM.Tel = context.Request.Params["CustTel"].ToString().Trim();
        
        string ord = orderBy+" "+order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCustCallByTel(CustCallM, pageIndex, pageCount, ord, ref totalCount);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}