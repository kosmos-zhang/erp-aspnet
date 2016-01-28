<%@ WebHandler Language="C#" Class="ProviderLinkManAdd" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;

public class ProviderLinkManAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            ProviderLinkManModel model = new ProviderLinkManModel();
            model.CompanyCD = CompanyCD;
            //基本信息
            
            model.CustNo = context.Request.Params["custNo"].ToString().Trim();//编号 
            model.LinkManName = context.Request.Params["linkManName"].ToString().Trim();//联系人姓名 
            model.Sex = context.Request.Params["sex"].ToString().Trim();//性别 
            model.Important = context.Request.Params["important"].ToString().Trim();//重要程度 
            model.Company = context.Request.Params["company"].ToString().Trim();//单位
            model.Appellation = context.Request.Params["appellation"].ToString().Trim();//称谓 
            model.Department = context.Request.Params["department"].ToString().Trim();//部门
            model.Position = context.Request.Params["position"].ToString().Trim();//职务
            model.Operation = context.Request.Params["operation"].ToString().Trim();//负责业务 
            
            
            //辅助信息
            model.WorkTel = context.Request.Params["workTel"].ToString().Trim();//工作电话
            model.Fax = context.Request.Params["fax"].ToString().Trim();//传真
            model.Handset = context.Request.Params["handset"].ToString().Trim();//移动电话
            model.MailAddress = context.Request.Params["mailAddress"].ToString().Trim();//邮件地址
            model.HomeTel = context.Request.Params["homeTel"].ToString().Trim();//家庭电话
            model.MSN = context.Request.Params["mSN"].ToString().Trim();//MSN
            model.QQ = context.Request.Params["qQ"].ToString().Trim();//QQ
            model.Post = context.Request.Params["post"].ToString().Trim();//邮政编码
            model.HomeAddress = context.Request.Params["homeAddress"].ToString().Trim();//住址
            model.Age = context.Request.Params["age"].ToString().Trim();//年龄
            model.PaperType = context.Request.Params["paperType"].ToString().Trim();//证件类型
            model.PaperNum = context.Request.Params["paperNum"].ToString().Trim();//证件号
            if (context.Request.Params["linkType"].Trim() != null && context.Request.Params["linkType"].Trim() != "")
            {
                model.LinkType = Convert.ToInt32(context.Request.Params["linkType"].ToString().Trim());//联系人类型
            }
            model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();//最后更新用户
            
            
            //业务信息
            if (context.Request.Params["birthday"].Trim() != null && context.Request.Params["birthday"].Trim() != "")
            {
                model.Birthday =Convert.ToDateTime( context.Request.Params["birthday"].ToString().Trim());//生日
            }
            model.Likes = context.Request.Params["likes"].ToString().Trim();//爱好
            model.Photo = context.Request.Params["photo"].ToString().Trim();//照片
            model.Remark = context.Request.Params["remark"].ToString().Trim();// 备注
            
            if (ID > 0)
            {
                model.ID = ID;
            }

            string tempID = "0";
            if (ProviderLinkManBus.InsertProviderLinkMan(model, out tempID))
            {
                //string ContractDetailID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContractDetail").ToString();
                jc = new JsonClass("保存成功", "model.CustNo", int.Parse(tempID));
            }
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Update")
        {
            ProviderLinkManModel model = new ProviderLinkManModel();
            model.CompanyCD = CompanyCD;
            //基本信息

            model.CustNo = context.Request.Params["custNo"].ToString().Trim();//编号 
            model.LinkManName = context.Request.Params["linkManName"].ToString().Trim();//联系人姓名 
            model.Sex = context.Request.Params["sex"].ToString().Trim();//性别 
            model.Important = context.Request.Params["important"].ToString().Trim();//重要程度 
            model.Company = context.Request.Params["company"].ToString().Trim();//单位
            model.Appellation = context.Request.Params["appellation"].ToString().Trim();//称谓 
            model.Department = context.Request.Params["department"].ToString().Trim();//部门
            model.Position = context.Request.Params["position"].ToString().Trim();//职务
            model.Operation = context.Request.Params["operation"].ToString().Trim();//负责业务 


            //辅助信息
            model.WorkTel = context.Request.Params["workTel"].ToString().Trim();//工作电话
            model.Fax = context.Request.Params["fax"].ToString().Trim();//传真
            model.Handset = context.Request.Params["handset"].ToString().Trim();//移动电话
            model.MailAddress = context.Request.Params["mailAddress"].ToString().Trim();//邮件地址
            model.HomeTel = context.Request.Params["homeTel"].ToString().Trim();//家庭电话
            model.MSN = context.Request.Params["mSN"].ToString().Trim();//MSN
            model.QQ = context.Request.Params["qQ"].ToString().Trim();//QQ
            model.Post = context.Request.Params["post"].ToString().Trim();//邮政编码
            model.HomeAddress = context.Request.Params["homeAddress"].ToString().Trim();//住址
            model.Age = context.Request.Params["age"].ToString().Trim();//年龄
            model.PaperType = context.Request.Params["paperType"].ToString().Trim();//证件类型
            model.PaperNum = context.Request.Params["paperNum"].ToString().Trim();//证件号
            if (context.Request.Params["linkType"].Trim() != null && context.Request.Params["linkType"].Trim() != "")
            {
                model.LinkType = Convert.ToInt32(context.Request.Params["linkType"].ToString().Trim());//联系人类型
            }
            model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();//最后更新用户


            //业务信息
            if (context.Request.Params["birthday"].Trim() != null && context.Request.Params["birthday"].Trim() != "")
            {
                model.Birthday =Convert.ToDateTime( context.Request.Params["birthday"].ToString().Trim());//生日
            }
            model.Likes = context.Request.Params["likes"].ToString().Trim();//爱好
            model.Photo = context.Request.Params["photo"].ToString().Trim();//照片
            model.Remark = context.Request.Params["remark"].ToString().Trim();// 备注

            if (ID > 0)
            {
                model.ID = ID;
            }

            if (ProviderLinkManBus.UpdateProviderLinkMan(model))
                jc = new JsonClass("保存成功", "model.CustNo", model.ID);
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