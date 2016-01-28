<%@ WebHandler Language="C#" Class="StorageInitailInfo" %>

using System;
using System.Web;
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;
using System.Data;

public class StorageInitailInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{


    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
            string action = context.Request.QueryString["Act"];
            if ("Close".Equals(action.Trim()))//结单
            {
                CloseBill(context);
            }
            else if ("CancelClose".Equals(action.Trim()))//取消结单
            {
                CancelCloseBill(context);
            }
            else
            {
                int ID = int.Parse(context.Request.QueryString["ID"].ToString());
                StorageInitailModel model = new StorageInitailModel();
                model.CompanyCD = companyCD;
                model.ID = ID.ToString();
                DataTable DT = StorageInitailBus.GetStorageInitailDetailInfo(model);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(DT));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
        }
    }


    private void CloseBill(HttpContext context)
    {
        JsonClass jc = new JsonClass();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");

        int ID = int.Parse((context.Request.QueryString["ID"].ToString()));//明细信息的ID组合字符串
        if (context.Request.QueryString["Act"].ToString().Trim() == "Close")
            if (ID > 0)
            {
                StorageInitailModel model = new StorageInitailModel();
                model.ID = ID.ToString();
                model.CompanyCD = companyCD;
                model.Closer = loginUser_id.ToString();
                model.ModifiedUserID = LoginUserID;
                if (StorageInitailBus.CloseBill(model))
                {
                    jc = new JsonClass("结单成功", LoginUserName + " | " + Date, 1);
                }
                else
                {
                    jc = new JsonClass("结单失败", "", 0);
                }
            }
        context.Response.Write(jc);
    }

    private void CancelCloseBill(HttpContext context)
    {
        JsonClass jc = new JsonClass();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");

        int ID = int.Parse(context.Request.QueryString["ID"].ToString());//明细信息的ID组合字符串
        if (ID > 0)
        {
            StorageInitailModel model = new StorageInitailModel();
            model.ID = ID.ToString();
            model.CompanyCD = companyCD;
            model.Closer = loginUser_id.ToString();
            model.ModifiedUserID = LoginUserID;
            if (StorageInitailBus.CancelCloseBill(model))
            {
                jc = new JsonClass("取消结单成功", LoginUserName + " | " + Date, 1);
            }
            else
            {
                jc = new JsonClass("取消结单失败", "", 0);
            }
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