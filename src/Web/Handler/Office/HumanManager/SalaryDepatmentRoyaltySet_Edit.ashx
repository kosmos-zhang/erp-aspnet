<%@ WebHandler Language="C#" Class="SalaryDepatmentRoyaltySet_Edit" %>

using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;

public class SalaryDepatmentRoyaltySet_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        string action = context.Request.QueryString["Action"];
        if ("Save".Equals(action))
        {
            saveMessage(context);

        }
    }
    //tempInfo .push (i,minMoney ,maxMoney,personTaxPercent ,personMinusMoney );
    public void saveMessage(HttpContext context)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        IList<SalaryDepatmentRoyaltySetModel> Modelist = new List<SalaryDepatmentRoyaltySetModel>();
        string message = context.Request.QueryString["message"];
        string DeptID = context.Request.QueryString["DeptID"];
        if (string.IsNullOrEmpty(DeptID))
            DeptID = "0";
        string[] info = message.Split(',');
        int count = info.Length;
        for (int i = 0; i < count - 3; i++)
        {
            SalaryDepatmentRoyaltySetModel model = new SalaryDepatmentRoyaltySetModel();
            model.MiniMoney = info[i + 1];
            model.MaxMoney = info[i + 2];
            model.TaxPercent = info[i + 3];
            model.DeptID = info[i + 4];
            if (string.IsNullOrEmpty(model.DeptID))
            {
                model.DeptID = "0"; 
            }
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            Modelist.Add(model);
            i = i + 4;

        }

        if (SalaryDepatmentRoyaltySetBus.SaveInsuPersonInfo(DeptID,Modelist))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
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