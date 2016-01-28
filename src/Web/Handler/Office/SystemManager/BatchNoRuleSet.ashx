<%@ WebHandler Language="C#" Class="BatchNoRuleSet" %>

using System;
using System.Web;
using System.Data;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;

public class BatchNoRuleSet : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch(action.ToLower())
        {
            case "getbatchruleinfo":
                LoadBatchData();//获取批次规则信息
                break;
            case "saverule":
                SaveBatchRule();//保存批次规则
                break;
            case "updaterule":
                UpdateBatchRule();//修改批次规则
                break;
            default :
                DefaultAction(action);
                break;
        }
    }

    //获取批次规则信息
    private void LoadBatchData()
    {
        DataTable dt = BatchNoRuleSetBus.GetBatchRuleInfo(UserInfo.CompanyCD);
        
        string strJson = string.Empty;
        strJson = "{";
        if (dt.Rows.Count > 0)
        {
            strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
        }
        strJson += "}";
        Output(strJson);
       
    }

    //保存批次规则
    private void SaveBatchRule()
    {
        XBase.Model.Office.SystemManager.BatchNoRuleSet model = GetBatchRuleModel();

        string strMsg = "";
        int ruleID = 0;
        ruleID = BatchNoRuleSetBus.SaveBatchRule(model, out strMsg);
        if (ruleID > 0)
        {
            //设置session中批次规则是否启用
            if (BatchNoRuleSetBus.GetBatchStatus(UserInfo.CompanyCD))
            {
                UserInfo.IsBatch = true;
            }
            else
            {
                UserInfo.IsBatch = false;
            }
        }
        Output(strMsg + "|" + ruleID.ToString());
    }

    //修改批次规则
    private void UpdateBatchRule()
    {
        XBase.Model.Office.SystemManager.BatchNoRuleSet model = GetBatchRuleModel();

        string strMsg = "";
        bool isSucc = false;
        int ruleID = Convert.ToInt32(GetParam("ID").Trim());
        model.ID = ruleID;
        isSucc = BatchNoRuleSetBus.UpdateBatchRule(model, out strMsg);
        if (isSucc)
        {
            strMsg = "保存成功";
            //设置session中批次规则是否启用
            if (BatchNoRuleSetBus.GetBatchStatus(UserInfo.CompanyCD))
            {
                UserInfo.IsBatch = true;
            }
            else
            {
                UserInfo.IsBatch = false;
            }
        }
        else 
        {
            strMsg = "保存失败，请联系管理员！";
        }

        Output(strMsg + "|" + ruleID.ToString());
    }
     
    private XBase.Model.Office.SystemManager.BatchNoRuleSet GetBatchRuleModel()
    {
        XBase.Model.Office.SystemManager.BatchNoRuleSet model = new XBase.Model.Office.SystemManager.BatchNoRuleSet();
        string usedStatus = GetParam("UsedStatus");//启用状态
        string ruleName = GetParam("RuleName");//规则名称
        string rulePrefix = GetParam("RulePrefix");//前缀
        string ruleDateType = GetParam("RuleDateType");//编号中的日期类型
        string ruleNoLength = GetParam("RuleNoLen");//编号流水号长度
        string ruleExample = GetParam("RuleExample");//编号示例
        string remark = GetParam("Remark");//备注
        string isDefault = GetParam("IsDefault");//是否缺省编号规则

        model.CompanyCD = UserInfo.CompanyCD;
        model.UsedStatus = usedStatus;
        model.RuleName = ruleName.Length == 0 ? null : ruleName;
        model.RulePrefix = rulePrefix.Length == 0 ? null : rulePrefix;
        model.RuleDateType = ruleDateType.Length == 0 ? null : ruleDateType;
        model.RuleNoLen = ruleNoLength.Length == 0 ? null : (int?)Convert.ToInt32(ruleNoLength);
        model.RuleExample = ruleExample.Length == 0 ? null : ruleExample;
        model.Remark = remark.Length == 0 ? null : remark;
        model.IsDefault = isDefault;
        model.ModifiedUserID = UserInfo.UserID;
        model.ModifiedDate = (DateTime?)System.DateTime.Now;

        return model;        
    }
    
}