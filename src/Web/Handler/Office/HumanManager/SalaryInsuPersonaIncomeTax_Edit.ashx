<%@ WebHandler Language="C#" Class="SalaryInsuPersonaIncomeTax_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/07
 * 描    述： 社会保险设置
 * 修改日期： 2009/05/07
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;

public class SalaryInsuPersonaIncomeTax_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        
        string action = context.Request.QueryString["Action"];
        if ("Save".Equals(action))
        {
            saveMessage(context );
             
        }
    }
     //tempInfo .push (i,minMoney ,maxMoney,personTaxPercent ,personMinusMoney );
    public void saveMessage(HttpContext  context)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        IList<PercentIncomeTaxModel> Modelist = new List<PercentIncomeTaxModel>();
        string message = context.Request.QueryString["message"];
        string[] info = message.Split(',');
        int count = info.Length;
        for (int i = 0; i < count-3; i++)
        {
            PercentIncomeTaxModel model = new PercentIncomeTaxModel();
            model.TaxLevel = info[i];
            model.MinMoney = info[i + 1];
            model.MaxMoney = info[i + 2];
            model.TaxPercent = info[i + 3];
            model.MinusMoney = info[i + 4];
            model.CompanyCD = userInfo.CompanyCD;
            Modelist.Add(model);
            i = i + 4;
            
        }
      
        if (InsuPersonaIncomeTaxBus.SaveInsuPersonInfo(Modelist))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}