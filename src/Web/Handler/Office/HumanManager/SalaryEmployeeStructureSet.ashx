<%@ WebHandler Language="C#" Class="SalaryEmployeeStructureSet" %>

/**********************************************
 * 作    者： 肖合明
 * 创建日期： 2009/09/07
 * 描    述： 公司提成
 * 修改日期： 2009/09/07
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;

public class SalaryEmployeeStructureSet : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.Form["Action"];

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
            GetInfo(context);
        }
        if ("Save".Equals(action))
        {
            saveMessage(context);
        }
    }

    //保存
    private void saveMessage(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //获取当前用户信息
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //获取传过来的人员ID
        string EmployeeID = request.Form["EmployeeID"];
        //从请求中获取message
        string message = context.Request.Form["message"];
        string[] info = message.Split(',');
        SalaryEmployeeStructureSetModel model = new SalaryEmployeeStructureSetModel();
        model.EmployeeID = EmployeeID;
        model.ModifiedUserID = userInfo.UserID;
        model.CompanyCD = userInfo.CompanyCD;
        model.IsCompanyRoyaltySet = info[0];
        model.IsDeptRoyaltySet = info[1];
        model.IsProductRoyaltySet = info[2];
        model.IsFixSalarySet = info[3];
        model.IsPieceWorkSet = info[4];
        model.IsInsurenceSet = info[5];
        model.IsPerIncomeTaxSet = info[6];
        model.IsQuteerSet = info[7];
        model.IsTimeWorkSet = info[8];
        model.IsPersonalRoyaltySet = info[9];
        model.IsPerformanceSet = info[10];
        model.CompanyRatePercent = info[11];
        model.DeptRatePercent = info[12];

        if (SalaryEmployeeStructureSetBus.SaveInfo(EmployeeID, model))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }


    }

    //获取信息
    private void GetInfo(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取排序列
        string EmployeeID = request.Form["EmployeeID"];

        DataTable dt = SalaryEmployeeStructureSetBus.GetUserInfo(EmployeeID);
        StringBuilder sbReturn = new StringBuilder();
        sbReturn.Append("{");
        sbReturn.Append("data:");
        if (dt.Rows.Count == 0)
            sbReturn.Append("[{\"ID\":\"\"}]");
        else
            sbReturn.Append(JsonClass.DataTable2Json(dt));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}