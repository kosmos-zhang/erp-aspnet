<%@ WebHandler Language="C#" Class="ProviderContactHistoryAdd" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;

public class ProviderContactHistoryAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        //获得登录页面POST过来的参数//按照JS中从页面上顺序取
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());

        JsonClass jc;

        if (action == "Add")
        {
            string ContactNo = context.Request.Params["contactNo"].Trim();
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProviderContactHistory"
                                , "ContactNo", ContactNo);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("该编号已被使用，请输入未使用的编号!", "", 0);
                context.Response.Write(jc);
            }
            else
            {
                ProviderContactHistoryModel model = new ProviderContactHistoryModel();
                model.CompanyCD = CompanyCD;
                //基本信息
                string CodeType = context.Request.Params["CodeType"].ToString().Trim();
                //string aaa = context.Request.Params["contactNo"];
                if (context.Request.Params["contactNo"].Trim() == null || context.Request.Params["contactNo"].Trim() == "")
                {
                    model.ContactNo = ItemCodingRuleBus.GetCodeValue(CodeType, "ProviderContactHistory", "ContactNo");//合同编号
                }
                else
                {
                    model.ContactNo = context.Request.Params["contactNo"].ToString().Trim();//合同编号  
                }
                model.CustID = context.Request.Params["custID"].ToString().Trim();//供应商id
                model.Title = context.Request.Params["title"].ToString().Trim();//联络主题
                if (context.Request.Params["linkManID"].Trim() != null && context.Request.Params["linkManID"].Trim() != "")
                {
                    model.LinkManID = Convert.ToInt32(context.Request.Params["linkManID"].ToString().Trim());//供应商被联络人id 
                }
                model.LinkManName = context.Request.Params["linkManName"].ToString().Trim();//供应商被联络人名称
                if (context.Request.Params["linkReasonID"].Trim() != null && context.Request.Params["linkReasonID"].Trim() != "")
                {
                    model.LinkReasonID = Convert.ToInt32(context.Request.Params["linkReasonID"].ToString().Trim());//联络事由 
                }
                if (context.Request.Params["linkMode"].Trim() != null && context.Request.Params["linkMode"].Trim() != "")
                {
                    model.LinkMode = Convert.ToInt32(context.Request.Params["linkMode"].ToString().Trim());//联络方式
                }
                if (context.Request.Params["linkDate"].Trim() != null && context.Request.Params["linkDate"].Trim() != "")
                {
                    model.LinkDate = Convert.ToDateTime(context.Request.Params["linkDate"].ToString().Trim());//联络时间
                }
                if (context.Request.Params["linker"].Trim() != null && context.Request.Params["linker"].Trim() != "")
                {
                    model.Linker = Convert.ToInt32(context.Request.Params["linker"].ToString().Trim());//联络方式
                }
                model.Contents = context.Request.Params["contents"].ToString().Trim();//联络内容 


                if (ID > 0)
                {
                    model.ID = ID;
                }

                string tempID = "0";
                if (ProviderContactHistoryBus.InsertProviderContactHistory(model, out tempID))
                {
                    jc = new JsonClass("保存成功", model.ContactNo, int.Parse(tempID));
                }
                else
                    jc = new JsonClass("保存失败", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "Update")
        {
            ProviderContactHistoryModel model = new ProviderContactHistoryModel();
            model.CompanyCD = CompanyCD;
            model.ContactNo = context.Request.Params["cno"].ToString().Trim();//合同编号

            model.CustID = context.Request.Params["custID"].ToString().Trim();//供应商id
            model.Title = context.Request.Params["title"].ToString().Trim();//联络主题
            if (context.Request.Params["linkManID"].Trim() != null && context.Request.Params["linkManID"].Trim() != "")
            {
                model.LinkManID = Convert.ToInt32(context.Request.Params["linkManID"].ToString().Trim());//供应商被联络人id 
            }
            model.LinkManName = context.Request.Params["linkManName"].ToString().Trim();//供应商被联络人名称
            if (context.Request.Params["linkReasonID"] != null && context.Request.Params["linkReasonID"] != "")
            {
                model.LinkReasonID = Convert.ToInt32(context.Request.Params["linkReasonID"].ToString().Trim());//联络事由 
            }
            if (context.Request.Params["linkMode"] != null && context.Request.Params["linkMode"] != "")
            {
                model.LinkMode = Convert.ToInt32(context.Request.Params["linkMode"].ToString().Trim());//联络方式
            }
            if (context.Request.Params["linkDate"].Trim() != null && context.Request.Params["linkDate"].Trim() != "")
            {
                model.LinkDate = Convert.ToDateTime(context.Request.Params["linkDate"].ToString().Trim());//联络时间
            }
            if (context.Request.Params["linker"].Trim() != null && context.Request.Params["linker"].Trim() != "")
            {
                model.Linker = Convert.ToInt32(context.Request.Params["linker"].ToString().Trim());//联络方式
            }
            model.Contents = context.Request.Params["contents"].ToString().Trim();//联络内容 


            if (ID > 0)
            {
                model.ID = ID;
            }

            if (ProviderContactHistoryBus.UpdateProviderContactHistory(model))
                //jc = new JsonClass("更新成功", "model.CustID", model.ID);
                jc = new JsonClass("保存成功", model.ContactNo, model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
            //context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}