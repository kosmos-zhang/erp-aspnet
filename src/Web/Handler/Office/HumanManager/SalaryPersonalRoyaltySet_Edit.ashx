<%@ WebHandler Language="C#" Class="SalaryPersonalRoyaltySet_Edit" %>
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;

public class SalaryPersonalRoyaltySet_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        IList<SalaryPersonalRoyaltySetModel> Modelist = new List<SalaryPersonalRoyaltySetModel>();
        string message = context.Request.QueryString["message"];
        string EmpID = context.Request.QueryString["EmpID"];
        string CustID = context.Request.QueryString["CustID"];
        if (string.IsNullOrEmpty(EmpID))
            EmpID = "0";
        if (string.IsNullOrEmpty(CustID))
            CustID = "0";
        string[] info = message.Split(',');
        int count = info.Length;
        for (int i = 0; i < count - 3; i++)
        {
            SalaryPersonalRoyaltySetModel model = new SalaryPersonalRoyaltySetModel();
            model.MiniMoney = info[i + 1];
            model.MaxMoney = info[i + 2];
            model.TaxPercent = info[i + 3];
            model.EmployeeID = info[i + 4];
            if (model.EmployeeID == "")
                model.EmployeeID = "0";
            model.CustID = info[i + 5];
            if (model.CustID == "")
                model.CustID = "0";
            model.ISCustomerRoyaltySet = info[i + 6];
            model.NewCustomerTax = info[i + 7];
            model.OldCustomerTax = info[i + 8];
            model.CompanyCD = userInfo.CompanyCD;
            model.ModifiedUserID = userInfo.UserID;
            Modelist.Add(model);
            i = i + 8;

        }

        if (SalaryPersonalRoyaltySetBus.SaveInsuPersonInfo(EmpID,CustID,Modelist))
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