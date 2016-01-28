<%@ WebHandler Language="C#" Class="PerformanceRoyaltyBase" %>

/**********************************************
 * 作    者： 肖合明
 * 创建日期： 2009/09/11
 * 描    述： 公司提成
 * 修改日期： 2009/09/11
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


public class PerformanceRoyaltyBase : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.QueryString["Act"];

        //查询
        if ("search".Equals(action))
        {
            DoSearch(context);
        }
        //删除
        if ("Del".Equals(action))
        {
            DoDelete(context);
        }
        //保存（插入或编辑）
        if ("edit".Equals(action))
        {
            DoEdit(context);
        }

    }

    private void DoSearch(HttpContext context)
    {
        //获取登陆用户信息
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        HttpRequest request = context.Request;

        string orderString = (request.QueryString["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "EmployeeID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(request.QueryString["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(request.QueryString["pageIndex"].ToString());//当前页

        string EmployeeID = request.QueryString["EmployeeID"];
        string TaskFlag = request.QueryString["TaskFlag"];

        PerformanceRoyaltyBaseModel model = new PerformanceRoyaltyBaseModel();
        model.EmployeeID = EmployeeID;
        model.TaskFlag = TaskFlag;
        model.CompanyCD = userInfo.CompanyCD;
        string ord = orderBy + " " + order;
        int TotalCount = 0;
        DataTable dt = PerformanceRoyaltyBaseBus.GetInfo(model, pageIndex, pageCount, ord, ref TotalCount);
        StringBuilder sbReturn = new StringBuilder();
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(TotalCount.ToString());
        sbReturn.Append(",data:");
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

    private void DoDelete(HttpContext context)
    {
        JsonClass jc = new JsonClass();
        //获取登陆用户信息
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        HttpRequest request = context.Request;
        string strID = (request.QueryString["strID"].ToString());//排序
        if (PerformanceRoyaltyBaseBus.DoDelete(strID))
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败", "", 0);
        }
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(jc);
        context.Response.End();
    }

    private void DoEdit(HttpContext context)
    {
        JsonClass jc = new JsonClass();
        //获取登陆用户信息
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        HttpRequest request = context.Request;
        PerformanceRoyaltyBaseModel model = new PerformanceRoyaltyBaseModel();
        string ID = request.QueryString["ID"];//主键
        string EmployeeID = request.QueryString["EmployeeID"];//
        string BaseMoney = request.QueryString["BaseMoney"];//
        string TaskFlag = request.QueryString["TaskFlag"];//
        model.ID = ID;
        model.EmployeeID = EmployeeID;
        model.BaseMoney = BaseMoney;
        model.TaskFlag = TaskFlag;
        model.CompanyCD = userInfo.CompanyCD;
        model.ModifiedUserID = userInfo.UserID;
        bool result;
        if (model.ID != "0")
        {
            if (PerformanceRoyaltyBaseBus.ifEitst(model))
            {
                jc = new JsonClass("该用户该类型已经设置", "", 2);
                context.Response.ContentType = "text/plain";
                //向响应中输出数据
                context.Response.Write(jc);
                context.Response.End();
            }
            result = PerformanceRoyaltyBaseBus.Update(model);

        }
        else
        {
            if (PerformanceRoyaltyBaseBus.ifEitst(model))
            {
                jc = new JsonClass("该用户该类型已经设置", "", 2);
                context.Response.ContentType = "text/plain";
                //向响应中输出数据
                context.Response.Write(jc);
                context.Response.End();
            }
            result = PerformanceRoyaltyBaseBus.InSert(model);
        }
        if (result)
        {
            jc = new JsonClass("保存成功", model.ID, 1);
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(jc);
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