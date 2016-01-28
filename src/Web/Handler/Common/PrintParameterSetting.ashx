<%@ WebHandler Language="C#" Class="PrintParameterSetting" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;

public class PrintParameterSetting : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(context.Request.Params["BillTypeFlag"].ToString());
        model.PrintTypeFlag = int.Parse(context.Request.Params["PrintTypeFlag"].ToString());
        model.BaseFields = context.Request.Params["strBaseFields"].ToString().Trim().TrimEnd('|');
        model.DetailFields = context.Request.Params["strDetailFields"].ToString().Trim().TrimEnd('|');
        model.DetailSecondFields = context.Request.Params["strDetailSecondFields"].ToString().Trim().TrimEnd('|');
        model.DetailThreeFields = context.Request.Params["strDetailThreeFields"].ToString().Trim().TrimEnd('|');
        model.DetailFourFields = context.Request.Params["strDetailFourFields"].ToString().Trim().TrimEnd('|');

        JsonClass jc;
        if (PrintParameterSettingBus.EditPrintParameterSetting(model))
        {
            jc = new JsonClass("保存成功", "", 1);
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}