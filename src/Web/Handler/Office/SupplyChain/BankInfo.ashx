<%@ WebHandler Language="C#" Class="BankInfo" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Business.Common;
using XBase.Common;
public class BankInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    BankInfoModel model = new BankInfoModel();
    JsonClass jc;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string ActionFlag = context.Request.Form["ActionFlag"].ToString();
            string id = context.Request.QueryString["str"].ToString();

            switch (ActionFlag)
            {
                case "Search":
                    LoadData(context);//加载数据
                    break;
                case "Add":
                    AddCodePublicType(context);//添加记录
                    break;
                case "Del":
                    DelItem(context, id);//删除记录
                    break;
                case "Edit":
                    EditItem(context);//修改记录
                    break;
            }

        }
    }

    private void LoadData(HttpContext context)
    {
        try
        {
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "asc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int totalCount = 0;
            string ord = orderBy + " " + order;
            //获取数据
            //UserInfoModel model = new UserInfoModel();
            string BankNo = context.Request.Form["BankNo"].ToString();
            string BankName = context.Request.Form["BankName"].ToString();
            string UsedStatus = context.Request.Form["UsedStatus"].ToString();
            string create = context.Request.Form["create"].ToString();
            string py = context.Request.Form["py"].ToString();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //string CompanyCD = "AAAAAA";
            model.CustNo = BankNo;
            model.CustName = BankName;
            model.UsedStatus = UsedStatus;
            if (!string.IsNullOrEmpty(create))
                model.ContactName = create;
            model.CompanyCD = CompanyCD;
            model.PYShort = py;
            DataTable dt = BankInfoBus.GetBankInfo(model, pageIndex, pageCount, ord, ref totalCount);
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
        catch 
        {
            
        }
     
    }




    private void AddCodePublicType(HttpContext context)
    {
        string CustNo = context.Request.QueryString["CustNo"].ToString().Trim();
        //判断是否存在
          bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("BankInfo"
                                , "CustNo", CustNo);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("银行编号已经存在", "", 0);
                context.Response.Write(jc);
            }
            else
            {
                string CustName = context.Request.QueryString["CustName"].ToString().Trim();
                string CustNam = context.Request.QueryString["CustNam"].ToString().Trim();
                string PYShort = context.Request.QueryString["PYShort"].ToString().Trim();
                string ContactName = context.Request.QueryString["ContactName"].ToString().Trim();
                string Tel = context.Request.QueryString["Tel"].ToString().Trim();
                string Fax = context.Request.QueryString["Fax"].ToString().Trim();
                string Mobile = context.Request.QueryString["Mobile"].ToString().Trim();
                string Addr = context.Request.QueryString["Addr"].ToString().Trim();
                string Remark = context.Request.QueryString["Remark"].ToString().Trim();
                string UseStatus = context.Request.QueryString["UseStatus"].ToString().Trim();
                string Creator = context.Request.QueryString["Creator"].ToString().Trim();
                string CreateDate = context.Request.QueryString["CreateDate"].ToString().Trim();
                model.CustNo = CustNo;
                model.CustName = CustName;
                model.CustNam = CustNam;
                model.PYShort = PYShort;
                model.ContactName = ContactName;
                model.Tel = Tel;
                model.Fax = Fax;
                model.Mobile = Mobile;
                model.Addr = Addr;
                model.Remark = Remark;
                model.UsedStatus = UseStatus;
                if (!string.IsNullOrEmpty(Creator))
                    model.Creator = int.Parse(Creator);
                if (!string.IsNullOrEmpty(CreateDate))
                    model.CreateDate = DateTime.Parse(CreateDate);
                model.ModifiedDate = System.DateTime.Now;
                //model.ModifiedUserID = "admin";
                //model.CompanyCD = "AAAAAA";
                model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                bool result = BankInfoBus.InsertBankInfo(model);
                {
                    if (result)
                    {
                        jc = new JsonClass("保存成功！", "", 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败！", "", 0);
                    }
                    context.Response.Write(jc);
                    return;
                } 
            }
 
    }


    private void EditItem(HttpContext context)
    {
        string CustNo = context.Request.QueryString["CustNo"].ToString().Trim();
        string CustName = context.Request.QueryString["CustName"].ToString().Trim();
        string CustNam = context.Request.QueryString["CustNam"].ToString().Trim();
        string PYShort = context.Request.QueryString["PYShort"].ToString().Trim();
        string ContactName = context.Request.QueryString["ContactName"].ToString().Trim();
        string Tel = context.Request.QueryString["Tel"].ToString().Trim();
        string Fax = context.Request.QueryString["Fax"].ToString().Trim();
        string Mobile = context.Request.QueryString["Mobile"].ToString().Trim();
        string Addr = context.Request.QueryString["Addr"].ToString().Trim();
        string Remark = context.Request.QueryString["Remark"].ToString().Trim();
        string UseStatus = context.Request.QueryString["UseStatus"].ToString().Trim();
        string Creator = context.Request.QueryString["Creator"].ToString().Trim();
        string CreateDate = context.Request.QueryString["CreateDate"].ToString().Trim();
        string ID = context.Request.QueryString["ID"].ToString().Trim();
        model.ID = int.Parse(ID);
        model.CustNo = CustNo;
        model.CustName = CustName;
        model.CustNam = CustNam;
        model.PYShort = PYShort;
        model.ContactName = ContactName;
        model.Tel = Tel;
        model.Fax = Fax;
        model.Mobile = Mobile;
        model.Addr = Addr;
        model.Remark = Remark;
        model.UsedStatus = UseStatus;
        if (!string.IsNullOrEmpty(Creator))
            model.Creator = int.Parse(Creator);
        if (!string.IsNullOrEmpty(CreateDate))
            model.CreateDate = DateTime.Parse(CreateDate);
        model.ModifiedDate = System.DateTime.Now;
        //model.ModifiedUserID = "admin";
        //model.CompanyCD = "AAAAAA";
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        bool result = BankInfoBus.UpdateBankInfo(model);
        {
            if (result)
            {
                jc = new JsonClass("保存成功！", "", 1);
            }
            else
            {
                jc = new JsonClass("保存失败！", "", 0);
            }
            context.Response.Write(jc);
            return;
        }
    }

    /// <summary>
    /// 删除公共分类
    /// </summary>
    private void DelItem(HttpContext context, string id)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";
        JsonClass jc;
        bool isDelete = BankInfoBus.DeleteBankInfo(id, companyCD);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败", "", 0);
        }
        context.Response.Write(jc);
        return;
    }

 

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
 
}