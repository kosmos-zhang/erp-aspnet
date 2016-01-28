<%@ WebHandler Language="C#" Class="ItemCodingRule" %>

using System;
using System.Web;
using System.Data;
using System.Xml.Linq;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
public class ItemCodingRule : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    ItemCodingRuleModel model = new ItemCodingRuleModel();
    JsonClass jc;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string ActionFlag = context.Request.Form["ActionFlag"].ToString();
            string id = context.Request.QueryString["str"].ToString();

            switch (ActionFlag)
            {
                case "Search":
                    LoadData(context);//加载数据
                    break;
                case "Add":
                    AddCodePublicType(context);//添加记录
                    break;
                case "Del":
                    DelItem(context, id);//删除记录
                    break;
                case "Edit":
                    EditItem(context);//修改记录
                    break;
            }

        }
    }

    private void LoadData(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "asc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_d"))
        {
            order = "desc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        //获取数据
        //UserInfoModel model = new UserInfoModel();
        string TypeFlag = context.Request.Form["TypeFlag"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //linq排序
        DataTable dt = ItemCodingRuleBus.GetItemCodingRule(TypeFlag, CompanyCD, pageIndex, pageCount, ord, ref totalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ItemTypeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
       
    }




    private void AddCodePublicType(HttpContext context)
    {
        string TypeFlag = context.Request.Form["TypeFlag"].ToString();
        string UseStatus = context.Request.Form["UseStatus"].ToString();
        string ItemTypeID = context.Request.Form["ItemTypeID"].ToString();
        string RuleName = context.Request.Form["RuleName"].ToString();
        string RulePrefix = context.Request.Form["RulePrefix"].ToString();
        string RuleNoLen = context.Request.Form["RuleNoLen"].ToString();
        string RuleExample = context.Request.Form["RuleExample"].ToString();
        string Remark = context.Request.Form["Remark"].ToString();
        string DefalutStatus = context.Request.Form["DefalutStatus"].ToString();
        string RuleDateType = context.Request.Form["RuleDateType"].ToString();
        string LastNo = "0";

        model.CodingType = TypeFlag;
        model.UsedStatus = UseStatus;
        model.ItemTypeID = int.Parse(ItemTypeID);
        model.RuleName = RuleName;
        model.RulePrefix = RulePrefix;
        model.RuleNoLen = int.Parse(RuleNoLen);
        model.RuleExample = RuleExample;
        model.Remark = Remark;
        model.IsDefault = DefalutStatus;
        model.RuleDateType = RuleDateType;
        model.LastNo = int.Parse(LastNo);
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        //判断是否存在
        bool isAlready = ItemCodingRuleBus.CheckCodeUniq(RuleName,TypeFlag,ItemTypeID);
        bool CheckRuleExample=ItemCodingRuleBus.CheckCodeRuleExample(RuleExample,TypeFlag,ItemTypeID);
        //存在的场合
        if (!isAlready)
        {
            jc = new JsonClass("编号规则名称已经存在", "", 0);
            context.Response.Write(jc);
            return;
        }
        if (!CheckRuleExample)
            {
                jc = new JsonClass("编号示例不能重复", "", 0);
                context.Response.Write(jc);
                return;
            }
        else
        {
            bool result = ItemCodingRuleBus.InsertItemCodingRule(model);
            {
                if (result)
                {
                    jc = new JsonClass("保存成功！", "", 1);
                }
                else
                {
                    jc = new JsonClass("保存失败！", "", 0);
                }
                context.Response.Write(jc);
                return;
            }
        }
        
    }

    

    private void EditItem(HttpContext context)
    {
        string TypeFlag = context.Request.Form["TypeFlag"].ToString();
        string UseStatus = context.Request.Form["UseStatus"].ToString();
        string ItemTypeID = context.Request.Form["ItemTypeID"].ToString();
        string RuleName = context.Request.Form["RuleName"].ToString();
        string RulePrefix = context.Request.Form["RulePrefix"].ToString();
        string RuleNoLen = context.Request.Form["RuleNoLen"].ToString();
        string RuleExample = context.Request.Form["RuleExample"].ToString();
        string Remark = context.Request.Form["Remark"].ToString();
        string DefalutStatus = context.Request.Form["DefalutStatus"].ToString();
        string RuleDateType = context.Request.Form["RuleDateType"].ToString();
        model.CodingType = TypeFlag;
        model.UsedStatus = UseStatus;
        model.ItemTypeID = int.Parse(ItemTypeID);
        model.RuleName = RuleName;
        model.RulePrefix = RulePrefix;
        model.RuleNoLen = int.Parse(RuleNoLen);
        model.RuleExample = RuleExample;
        model.Remark = Remark;
        model.IsDefault = DefalutStatus;
        model.RuleDateType = RuleDateType;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ModifiedDate = System.DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.ID = int.Parse(context.Request.Form["ID"].ToString());
        string name = context.Request.Form["name"].ToString();

        if (name != RuleName)
        {
            bool isAlready = ItemCodingRuleBus.CheckCodeUniq(RuleName, TypeFlag, ItemTypeID);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("编号规则名称已经存在", "", 0);
                context.Response.Write(jc);
                return;
            }
            else
            {
                bool result = ItemCodingRuleBus.UpdateItemCodingRule(model);
                {
                if (result)
                {
                    jc = new JsonClass("修改成功！", "", 1);
                }
                else
                {
                    jc = new JsonClass("修改失败！", "", 0);
                }
                   context.Response.Write(jc);
                   return;
                }
            }
        }
        else
        {
            bool result = ItemCodingRuleBus.UpdateItemCodingRule(model);
            {
                if (result)
                {
                    jc = new JsonClass("修改成功！", "", 1);
                }
                else
                {
                    jc = new JsonClass("修改失败！", "", 0);
                }
                context.Response.Write(jc);
                return;
            }
        }
      
    }

    /// <summary>
    /// 删除公共分类
    /// </summary>
    private void DelItem(HttpContext context, string id)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        JsonClass jc;
        bool isDelete = ItemCodingRuleBus.DeleteItemCodingRule(id, companyCD);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败", "", 0);
        }
        context.Response.Write(jc);
        return;
    }

  

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



}