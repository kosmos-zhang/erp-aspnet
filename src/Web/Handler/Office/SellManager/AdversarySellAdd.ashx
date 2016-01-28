<%@ WebHandler Language="C#" Class="AdversarySellAdd" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class AdversarySellAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {

        int? id = null;

        string action = context.Request.Params["action"].ToString().Trim();	//动作

        //状态为insert时才计算编号
        if (action == "insert")
        {
            AdversarySellModel adversarySell = GetAdversarySellModel(context);
            id = AdversarySellBus.Insert(adversarySell);
            JsonClass jc;
            if (id != null)
            {
                jc = new JsonClass("", (id ?? 0).ToString(), 1);
            }
            else
            {
                jc = new JsonClass("", (id ?? 0).ToString(), 0);
            }
            context.Response.Write(jc);
        }
        else if (action == "update")//保存后修改
        {
            AdversarySellModel adversarySell = GetAdversarySellModel(context);
            adversarySell.ID = Convert.ToInt32(context.Request.Params["ID"].ToString().Trim());
            bool bl = AdversarySellBus.Update(adversarySell);
            JsonClass jc;
            if (bl)
            {

                jc = new JsonClass("", context.Request.Params["ID"].ToString().Trim(), 1);
            }
            else
            {
                jc = new JsonClass("", context.Request.Params["ID"].ToString().Trim(), 0);
            }
            context.Response.Write(jc);
        }

    }



    private AdversarySellModel GetAdversarySellModel(HttpContext context)
    {
        AdversarySellModel adversarySell = new AdversarySellModel();
        string CustNo = context.Request.Params["CustNo"].ToString().Trim();
        string ChanceID = context.Request.Params["ChanceID"].ToString().Trim();
        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string Project = context.Request.Params["Project"].ToString().Trim();
        string Price = context.Request.Params["Price"].ToString().Trim();
        string Power = context.Request.Params["Power"].ToString().Trim();
        string Advantage = context.Request.Params["Advantage"].ToString().Trim();
        string disadvantage = context.Request.Params["disadvantage"].ToString().Trim();
        string Policy = context.Request.Params["Policy"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();


        adversarySell.CustNo = CustNo;
        adversarySell.ChanceID = Convert.ToInt32(ChanceID);
        adversarySell.CustID = Convert.ToInt32(CustID);
        adversarySell.Project = Project;
        try
        {
            adversarySell.Price = Convert.ToDecimal(Price);
        }
        catch { }
        adversarySell.Power = Power;
        adversarySell.Advantage = Advantage;
        adversarySell.disadvantage = disadvantage;
        adversarySell.Policy = Policy;
        adversarySell.Remark = Remark;


        adversarySell.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
        adversarySell.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
        adversarySell.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  

        return adversarySell;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}