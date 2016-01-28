<%@ WebHandler Language="C#" Class="CustColligate" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Common;
using System.Data;
using XBase.Business.Office.CustManager;

public class CustColligate : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["action"].ToString()).Trim();//操作 
            switch (action)
            {
                case "CustInfoList":
                    CustInfoList(context);
                    break;
                case "Cust_lxr":
                    Cust_lxr(context);
                    break;
                case "Cust_ll":
                    Cust_ll(context);
                    break;
                case "Cust_qt":
                    Cust_qt(context);
                    break;
                case "Cust_gh":
                    Cust_gh(context);
                    break;
                case "Cust_fw":
                    Cust_fw(context);
                    break;
                case "Cust_ts":
                    Cust_ts(context);
                    break;
                case "Cust_jy":
                    Cust_jy(context);
                    break;
                case "Cust_ld":
                    Cust_ld(context);
                    break;
                case "Cust_gm":
                    Cust_gm(context);
                    break;
                case "Cust_fh":
                    Cust_fh(context);
                    break;
                case "Cust_hk":
                    Cust_hk(context);
                    break;
                case "Cust_jl":
                    Cust_jl(context);
                    break;
                case "Cust_jh":
                    Cust_jh(context);
                    break;
                    
                default:
                    break;
            }
        }
    }

    //销售机会
    private void Cust_jh(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_jh"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_jh"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_jh"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_jhByCon(CustInfoM, pageIndex, pageCount, ord, ref totalCount);

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
    //回款记录（收款单）
    private void Cust_jl(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_jl"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_jl"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_jl"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_jlByCon(CustInfoM, pageIndex, pageCount, ord, ref totalCount);

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
    
    //回款计划
    private void Cust_hk(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_hk"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_hk"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_hk"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_hkByCon(CustInfoM, pageIndex, pageCount, ord, ref totalCount);

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
    
    //发货记录
    private void Cust_fh(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_fh"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_fh"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_fh"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_fhByCon(CustInfoM, pageIndex, pageCount, ord, ref totalCount);

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
    
    //购买记录
    private void Cust_gm(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_gm"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_gm"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_gm"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        
        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_gmByCon(CustInfoM, pageIndex, pageCount, ord, ref totalCount);

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

    //客户来电
    private void Cust_ld(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_ld"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_ld"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_ld"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_ldByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户建议列表
    private void Cust_jy(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_jy"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_jy"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_jy"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_jyByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户投诉息列表
    private void Cust_ts(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_ts"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_ts"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_ts"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_tsByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户关服务息列表
    private void Cust_fw(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_fw"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_fw"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_fw"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_fwByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户关怀信息列表
    private void Cust_gh(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_gh"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_gh"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_gh"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_ghByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户联络信息列表
    private void Cust_qt(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_qt"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_qt"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_qt"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_qtByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //客户联络信息列表
    private void Cust_ll(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_ll"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_ll"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_ll"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_llByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //联系人信息列表
    private void Cust_lxr(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_lxr"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_lxr"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_lxr"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.CustNo = context.Request.Params["CustNo"].ToString().Trim();
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_lxrByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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

    //综合查询-客户信息列表
    private void CustInfoList(HttpContext context)
    {
        //设置行为参数
        string orderString = (context.Request.Form["orderby_da"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount_da"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex_da"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        CustInfoModel CustInfoM = new CustInfoModel();
        CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        CustInfoM.ID = context.Request.Params["CustID"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["CustID"].ToString().Trim());
        CustInfoM.Tel = context.Request.Params["Tel"].ToString().Trim();
        string DateBegin = context.Request.Form["CreatedBegin"].ToString().Trim();//开始时间
        string DateEnd = context.Request.Form["CreatedEnd"].ToString().Trim();//结束时间 

        string ord = orderBy + " " + order;
        int totalCount = 0;
        DataTable dt = CustCallBus.GetCust_daByCon(CustInfoM, DateBegin, DateEnd, pageIndex, pageCount, ord, ref totalCount);

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