<%@ WebHandler Language="C#" Class="ProviderProduct" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;
using System.Data;

public class ProviderProduct : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        //获得登录页面POST过来的参数//按照JS中从页面上顺序取
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());

        if (context.Request.Params["productID"] != null && context.Request.Params["productID"] != "")
        {
            int ProductID = Convert.ToInt32(context.Request.Params["productID"].ToString().Trim());
            //bool IscunzaiProductid = ProviderProductBus.ISCunzaiProviderProduct(ProductID);
            DataTable dtx = ProviderProductBus.ISCunzaiProviderProduct(ProductID, CompanyCD);
            if (dtx.Rows.Count != 0)
            {
                action = "Update";
                ID =Convert.ToInt32( dtx.Rows[0]["ID"].ToString());
                
            }
        }

        JsonClass jc;

        if (action == "Add")
        {
            ProviderProductModel model = new ProviderProductModel();
            model.CompanyCD = CompanyCD;
            //基本信息

            model.CustNo = context.Request.Params["custNo"].ToString().Trim();//供应商编号 
            if (context.Request.Params["productID"] != null && context.Request.Params["productID"] != "")
            {
                model.ProductID =Convert.ToInt32( context.Request.Params["productID"].ToString().Trim());//物品id 
            }
            if (context.Request.Params["grade"] != null && context.Request.Params["grade"] != "")
            {
                model.Grade = context.Request.Params["grade"].ToString().Trim();//推荐程度 
            }
            if (context.Request.Params["joiner"].Trim() != null && context.Request.Params["joiner"].Trim() != "")
            {
                model.Joiner =Convert.ToInt32( context.Request.Params["joiner"].ToString().Trim());//推荐人 
            }
            if (context.Request.Params["joinDate"].Trim() != null && context.Request.Params["joinDate"].Trim() != "")
            {
                model.JoinDate =Convert.ToDateTime( context.Request.Params["joinDate"].ToString().Trim());//推荐时间
            }
            model.Remark = context.Request.Params["remark"].ToString().Trim();//推荐理由 
            
            
            if (ID > 0)
            {
                model.ID = ID;
            }

            string tempID = "0";
            if (ProviderProductBus.InsertProviderProduct(model, out tempID))
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
            ProviderProductModel model = new ProviderProductModel();
            model.CompanyCD = CompanyCD;

            model.CustNo = context.Request.Params["custNo"].ToString().Trim();//供应商编号 
            if (context.Request.Params["productID"] != null && context.Request.Params["productID"] != "")
            {
                model.ProductID = Convert.ToInt32(context.Request.Params["productID"].ToString().Trim());//物品id 
            }
            if (context.Request.Params["grade"] != null && context.Request.Params["grade"] != "")
            {
                model.Grade = context.Request.Params["grade"].ToString().Trim();//推荐程度 
            }
            if (context.Request.Params["joiner"].Trim() != null && context.Request.Params["joiner"].Trim() != "")
            {
                model.Joiner = Convert.ToInt32(context.Request.Params["joiner"].ToString().Trim());//推荐人 
            }
            if (context.Request.Params["joinDate"].Trim() != null && context.Request.Params["joinDate"].Trim() != "")
            {
                model.JoinDate =Convert.ToDateTime( context.Request.Params["joinDate"].ToString().Trim());//推荐时间
            }
            model.Remark = context.Request.Params["remark"].ToString().Trim();//推荐理由 
            

            if (ID > 0)
            {
                model.ID = ID;
            }

            if (ProviderProductBus.UpdateProviderProduct(model))
                jc = new JsonClass("保存成功", "model.CustNo", model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
            //context.Response.End();
        }
        //else if (action == "Searchs")
        //{
        //    ProviderProductModel model = new ProviderProductModel();
        //    model.CompanyCD = CompanyCD;

        //    if (context.Request.Params["productID"] != null && context.Request.Params["productID"] != "")
        //    {
        //        model.ProductID = Convert.ToInt32(context.Request.Params["productID"].ToString().Trim());//物品id 
        //    }
            

        //    if (ProviderProductBus.UpdateProviderProduct(model))
        //        jc = new JsonClass("保存成功", "model.CustNo", model.ID);
        //    else
        //        jc = new JsonClass("保存失败", "", 0);
        //    context.Response.Write(jc);
        //    //context.Response.End();
        //}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}