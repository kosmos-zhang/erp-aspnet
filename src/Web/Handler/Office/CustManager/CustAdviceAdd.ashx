<%@ WebHandler Language="C#" Class="CustAdvuceAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.CustManager;
using XBase.Model.Office.CustManager;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;

public class CustAdvuceAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        CustAdviceModel model = new CustAdviceModel();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        JsonClass jc = new JsonClass();
        int ID = 0;
        string action = "";
        if (context.Request.Form["ID"] != null)
        {
            ID = int.Parse(context.Request.Form["ID"]);
        }
        if (context.Request.Form["myAction"] != null)
        {
            action = context.Request.Form["myAction"].ToString();
        }
        if (context.Request.Form["myAction"] != null)
        {
            if (context.Request.Form["myAction"].ToString() == "Del")
            {
                string strID = context.Request.Form["strID"].ToString();
                try
                {
                    CustAdviceBus.DelCust(strID);
                   jc = new JsonClass("删除成功", "", 1);
                }
                catch
                { jc = new JsonClass("删除失败", "", 0); }
                context.Response.Write(jc);
                return;
            }
            else
            {
                if (action == "edit")
                {
                    model = GetCustAdvice(context,model);
                    model.ID = ID;
                    model.AdviceNo = context.Request.Form["AdviceNo"];
                    if (CustAdviceBus.UpCustAdvice(model))
                    {

                        jc = new JsonClass("保存成功", model.ID.ToString() + "|"+model.AdviceNo, 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败", "", 0);
                    }

                }
                else if (action == "add")
                {
                    if (context.Request.Form["bmgz"].ToString().Trim() == "zd")
                    {
                        model.AdviceNo = ItemCodingRuleBus.GetCodeValue(context.Request.Form["AdviceNo"].ToString().Trim(), "CustAdvice", "AdviceNo");  //单据编号
                    }
                    else
                    {
                        model.AdviceNo = context.Request.Form["AdviceNo"].ToString().Trim();
                    }
                    bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("CustAdvice", "AdviceNo", model.AdviceNo);
                    if (!isAlready)
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    model = GetCustAdvice(context,model);
                    model.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.Creator = loginUser_id.ToString();
                    if (CustAdviceBus.AddCustAdvice(model) == true)
                    {
                        jc = new JsonClass("保存成功", model.ID.ToString() + "|"+model.AdviceNo, 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败", "", 0);
                    }
                }
            }
            context.Response.Write(jc);
        }
    }
    private CustAdviceModel GetCustAdvice(HttpContext context,CustAdviceModel model)
    {
        model.Title = context.Request.Form["Title"];
      
        model.Advicer = context.Request.Form["txtAdvicer"];
        model.CustID = context.Request.Form["CustID"];
        model.CustLinkMan = context.Request.Form["CustLinkMan"];
        model.DestClerk = context.Request.Form["DestClerk"];
        model.LeadSay = context.Request.Form["LeadSay"];
        model.DoSomething = context.Request.Form["DoSomething"];
        model.Accept = context.Request.Form["txtAccept"];
        model.AdviceDate = context.Request.Form["txtAdviceDate"];
        model.AdviceType = context.Request.Form["txtAdviceType"];
        model.State = context.Request.Form["txtState"];
        model.Contents = context.Request.Form["txtContents"];
        model.Remark = context.Request.Form["txtRemark"];
        model.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd");
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        model.CanViewUser = "," + context.Request.Form["CanViewUser"].ToString().Trim() + ",";
        model.CanViewUserName = context.Request.Form["CanViewUserName"].ToString().Trim();
        
        return model;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}