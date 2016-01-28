<%@ WebHandler Language="C#" Class="CheckOnlyOne" %>
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.AdminManager;
using XBase.Model.Office.AdminManager;

public class CheckOnlyOne : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    { 
        //判断唯一性
        string EquipCode = context.Request.QueryString["strcode"];//获取序列号
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["colname"];
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int EquipM = EquipMnetInfoBus.GetEquipInfoByIndex(EquipCode, CommonTable, ColName, companyCD);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (EquipM >= 1)
            jc = new JsonClass("faile", "", 0);
        else
            jc = new JsonClass("success", "", 1);
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}