<%@ WebHandler Language="C#" Class="ParameterSetting" %>

using System;
using System.Web;
using XBase.Business.Office.SystemManager;
using System.Data;
using XBase.Common;


public class ParameterSetting : SubBaseHandler
{
    public override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "set":
                Set();
                break;
            case "getpoint":
                GetPoint();
                break;
        }
    }
    /// <summary>
    /// 设置小数位
    /// </summary>
    protected void GetPoint()
    {
        string FunctionType = GetRequestForm("FunctionType", false);

        //验证
        if (string.IsNullOrEmpty(FunctionType))
        {
            OutputResult(false, "参数错误！");
            return;
        }

        //设置
        DataTable dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD, FunctionType);

        //判断
        if (dt!=null)
        {
            if(dt.Rows.Count>0)
                OutputResult(true, dt.Rows[0]["SelPoint"].ToString());
            else
                OutputResult(false, "！");
                
        }
        else
            OutputResult(false, "");

    }
 
    
    protected void Set()
    {
        string FunctionType = GetRequestForm("FunctionType", false);
        string Status = GetRequestForm("UsedStatus", false);

        //验证
        if (string.IsNullOrEmpty(FunctionType) || string.IsNullOrEmpty(Status))
        {
            OutputResult(false, "参数错误！");
            return; 
        }
        bool res = false;
        if (int.Parse(FunctionType) == 5)
        {
            string SelPoint = GetRequestForm("SelPoint", true);
            res = ParameterSettingBus.SetPoint(UserInfo.CompanyCD, FunctionType, SelPoint);
        }
        else
        {
            //设置
            if (int.Parse(FunctionType) == 6)
            {
                if (ParameterSettingBus.IfUsedAssistant(UserInfo.CompanyCD))
                {
                    res = ParameterSettingBus.Set(UserInfo.CompanyCD, FunctionType, Status);
                }
                else 
                {
                    OutputResult(false, "必须先设置辅助核算，才能设置自动生成凭证！");
                    return;
                }
            }
            else if (int.Parse(FunctionType) == 7 && Status.Equals("1"))
            {
                
                //是否启用自动生成凭证的
                if (!((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher)
                {
                    OutputResult(false, "必须启用自动生成凭证，才能设置自动审核登帐！");
                    return;
                }
                else
                {
                    res = ParameterSettingBus.Set(UserInfo.CompanyCD, FunctionType, Status);
                }

            }
            else if (int.Parse(FunctionType) == 3 && Status.Equals("0"))
            {
                if (ParameterSettingBus.IfUsedUnitGroup(UserInfo.CompanyCD))
                {
                    res = ParameterSettingBus.Set(UserInfo.CompanyCD, FunctionType, Status);
                }
                else
                {
                    OutputResult(false, "使用过多计量单位组的不可以再进行停用操作!");
                    return;
                }
            }
            else
                res = ParameterSettingBus.Set(UserInfo.CompanyCD, FunctionType, Status);
        }
        
        //判断
        if (res)
        {
            OutputResult(true, "设置成功！");

            XBase.Common.UserInfoUtil u = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            
            //更新Session
            //出入库价格是否显示
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "1",true))
                u.IsDisplayPrice = true;
            else
                u.IsDisplayPrice = false;

            //是否启用条码
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "2",true))
                u.IsBarCode = true;
            else
                u.IsBarCode = false;

            //是否启用多计量单位
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "3",false))
                u.IsMoreUnit = true;
            else
                u.IsMoreUnit = false;
            
            //是否启用批次规则设置
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "4", false))
                u.IsBatch = true;
            else
                u.IsBatch = false;
            
            //是否启用自动生成凭证
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "6", false))
                u.IsVoucher = true;
            else
                u.IsVoucher = false;
            //是否启用自动审核登帐
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "7", false))
                u.IsApply = true;
            else
                u.IsApply = false;
            //是否启用超订单发货
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "8", false))
                u.IsOverOrder = true;
            else
                u.IsOverOrder = false;
            //允许出入库价格为零
            if (ParameterSettingBus.Get(UserInfo.CompanyCD, "9", false))
                u.IsZero = true;
            else
                u.IsZero = false;
            

            //小数位数
            DataTable dtPoint = XBase.Business.Office.SystemManager.ParameterSettingBus.GetPoint(UserInfo.CompanyCD, "5");
            if (dtPoint != null)
            {
                if (dtPoint.Rows.Count > 0)
                {
                    u.SelPoint = dtPoint.Rows[0]["SelPoint"].ToString();
                }
            }
            //移除老Session
            //XBase.Common.SessionUtil.Session.Remove("UserInfo");
            //添加新的Session 
            XBase.Common.SessionUtil.Session.Add("UserInfo",u);

      
        }
        else
            OutputResult(false, "设置失败！");
        
    }
    //#CodeEnd
    
}

