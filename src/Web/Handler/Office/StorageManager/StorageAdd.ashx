<%@ WebHandler Language="C#" Class="StorageAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Business.Common;

public class StorageAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{


    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        bool ifdele = true;
        if (Action == ActionUtil.Del.ToString())
        {

            string ID = context.Request.QueryString["ID"].ToString();
            string[] IDArray = ID.Split(',');

            for (int i = 0; i < IDArray.Length; i++)
            {
                if (!StorageBus.IsDeleteStorage(IDArray[i].ToString(), companyCD))
                {
                    ifdele = false;
                    break;
                }
            }
            if (ifdele == true)
            {
                if (StorageBus.DeleteStorage(ID, companyCD))
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
            }
            else
            {
                jc = new JsonClass("已存放物品的仓库不允许删除！", "", 0);
            }
        }
        else
        {
            int ID = int.Parse(context.Request.QueryString["ID"].ToString());

            if (ID > 0)
            {
                #region 修改仓库信息
                StorageModel Model = new StorageModel();
                Model.CompanyCD = companyCD;
                Model.ID = ID;
                Model.StorageNo = context.Request.QueryString["StorageNo"].ToString().Trim();
                Model.StorageName = context.Request.QueryString["StorageName"].ToString().Trim();
                Model.StorageType = context.Request.QueryString["StorageType"].ToString().Trim();
                Model.Remark = context.Request.QueryString["Remark"].ToString().Trim();
                Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString().Trim();
                Model.CanViewUser = context.Request.QueryString["CanViewUser"].ToString();
                Model.StorageAdmin = context.Request.QueryString["storageAdmin"].ToString();
                if (StorageBus.UpdateStorage(Model))
                {
                    jc = new JsonClass("保存成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                #endregion
            }
            else
            {
                #region 添加仓库
                StorageModel Model = new StorageModel();
                Model.CompanyCD = companyCD;
                if (context.Request.QueryString["bmgz"].ToString().Trim() == "zd")
                {
                    Model.StorageNo = ItemCodingRuleBus.GetCodeValue(context.Request.QueryString["StorageNO"].ToString().Trim(), "StorageInfo", "StorageNo");
                }
                else
                {
                    Model.StorageNo = context.Request.QueryString["StorageNO"].ToString();
                }
                //
                bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("StorageInfo", "StorageNo", Model.StorageNo);
                if (!isExsist)
                {
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 2);
                }
                else
                {
                    Model.StorageName = context.Request.QueryString["StorageName"].ToString();
                    Model.StorageType = context.Request.QueryString["StorageType"].ToString();
                    Model.Remark = context.Request.QueryString["Remark"].ToString();
                    Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString();
                    Model.CanViewUser = context.Request.QueryString["CanViewUser"].ToString();
                    Model.StorageAdmin  = context.Request.QueryString["storageAdmin"].ToString();
                    int IndexIDentity = 0;
                    if (StorageBus.InsertStorage(Model, out IndexIDentity))
                    {
                        jc = new JsonClass("保存成功", IndexIDentity.ToString() + "|" + Model.StorageNo.ToString(), 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败", "", 0);
                    }
                }
                #endregion
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

