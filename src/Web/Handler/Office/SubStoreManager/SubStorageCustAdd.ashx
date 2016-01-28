<%@ WebHandler Language="C#" Class="SubStorageCustAdd" %>

using System;
using System.Web;
using System.Data;
using XBase.Common;
using System.Web.SessionState;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
public class SubStorageCustAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        SubSellCustInfoModel model = new SubSellCustInfoModel();

        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        int DeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID;
        DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(DeptID.ToString());
        if (dt != null)
        {
            DeptID = Convert.ToInt32(dt["ID"].ToString());
        }
        JsonClass jc = new JsonClass();
        int ID = 0;
        string action = "";
        if (context.Request.Form["ID"] != null)
        {
            ID = int.Parse(context.Request.Form["ID"]);
        }
        if (context.Request.Form["myAction"] != null)
        {
            action = context.Request.Form["myAction"].ToString();
        }
        if (context.Request.QueryString["myAction"] != null)
        {
            if (context.Request.QueryString["myAction"].ToString() == "Del")
            {
                string[] strID = context.Request.QueryString["strID"].ToString().Split(',');
                try
                {
                    for (int i = 0; i < strID.Length; i++)
                    {
                        if (strID[i] != "")
                        {
                            SubSellOrderBus.DeleteCust(int.Parse(strID[i]));
                        }
                    }
                    jc = new JsonClass("删除成功", "", 1);


                }
                catch
                { jc = new JsonClass("删除失败", "", 0); }



                context.Response.Write(jc);
                return;
            }
        }
        #region 添加
        else
        {
            model.CompanyCD = companyCD;

            model.CustName = context.Request.Form["CustName"].ToString();
            model.CustTel = context.Request.Form["CustTel"].ToString();
            model.CustMobile = context.Request.Form["CustPhone"].ToString();
            model.CustAddr = context.Request.Form["CustAddr"].ToString();

            if (action == "edit")
            {
                model.ID = ID.ToString();
                if (SubSellOrderBus.UpdateCust(model))
                {

                    jc = new JsonClass("保存成功", model.ID.ToString() + "|", 1);
                }
                else
                {
                    jc = new JsonClass("修改失败", "", 0);
                }
                context.Response.Write(jc);
                return;

            }
            else if (action == "add")
            {
                model.CreateDate = DateTime.Now.ToString();
                model.Creator = loginUser_id.ToString();
                model.DeptID = DeptID.ToString();
                if (SubSellOrderBus.SubStorageCustAdd(model) == true)
                {
                    jc = new JsonClass("保存成功", model.ID.ToString() + "|", 1);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
            }


        }

        #endregion
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