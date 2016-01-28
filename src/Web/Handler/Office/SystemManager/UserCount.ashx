<%@ WebHandler Language="C#" Class="UserCount" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.AdminManager;
using XBase.Business.Common;
public class UserCount : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        string Code = context.Request.QueryString["strcode"];//获取序列号
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["colname"];
        bool result = PrimekeyVerifyBus.CheckUserUniq(CommonTable, ColName, Code);
        //唯一性字段是否存在存在
        JsonClass jc;
        if (!result)
        {
            jc = new JsonClass("faile", "用户名已经存在，请重新输入！", 3);
        }
        else
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int isMaxUser = UserInfoBus.IsMaxUserCount(CompanyCD);
            //达到上限时，提示错误信息
            if (isMaxUser == 1)
            {
                jc = new JsonClass("faile", "企业用户已经达到上限，您不能再添加新的用户了！", 3);
            }
            else
                jc = new JsonClass("success", "", 1);

        }

        context.Response.Write(jc);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}