<%@ WebHandler Language="C#" Class="SellPlanAdd" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using System.Collections;
using XBase.Model.Office.SellManager;

public class SellPlanAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否更新成功       
        string strCode = string.Empty;
        int orderID = 0;
        string strMsg = string.Empty;//操作返回的信息  
        string strFieldText = string.Empty;
        string action = context.Request.Params["action"].ToString().Trim();	//动作

        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//销售计划编号产生的规则                                    
        string PlanNo = context.Request.Params["PlanNo"].ToString().Trim();	//发货单编号 

        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellPlan", "PlanNo");
            }
            else
            {
                strCode = PlanNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellPlan", "PlanNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "销售计划单据编号已经存在";
                }
            }
            else
            {
                string detailAction = context.Request.Params["detailAction"].ToString().Trim();	//明细动作
                SellPlanModel sellPlanModel = GetOrderModel(strCode, context);
                SellPlanDetailModel sellPlanDetail = new SellPlanDetailModel();
                if (detailAction != "1")
                {
                    sellPlanDetail = GetOrderDetail(strCode, context);
                }
                isSucc = SellPlanBus.SaveSellSend(ht, sellPlanModel, sellPlanDetail, out strMsg);
            }
        }
        else if (action == "update")//保存后修改
        {
            string detailAction = context.Request.Params["detailAction"].ToString().Trim();	//明细动作
            strCode = PlanNo;
            SellPlanModel sellPlanModel = GetOrderModel(strCode, context);
            SellPlanDetailModel sellPlanDetail = GetOrderDetail(strCode, context);
            isSucc = SellPlanBus.Update(ht,sellPlanModel, sellPlanDetail, detailAction, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = PlanNo;
            isSucc = SellPlanBus.ConfirmOrder(strCode, out strMsg, out strFieldText);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = PlanNo;
            isSucc = SellPlanBus.UnConfirmOrder(strCode, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = PlanNo;
            isSucc = SellPlanBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = PlanNo;
            isSucc = SellPlanBus.UnCloseOrder(strCode, out strMsg);
        }
        else if (action == "summ")//总结
        {
            strCode = PlanNo;
            SellPlanDetailModel sellPlanDetail = GetOrderDetail1(strCode, context);
            isSucc = SellPlanBus.SummarizeOrder(sellPlanDetail,out strMsg);
        }

        if (isSucc)
        {
            orderID = SellPlanBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }

    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            //string strKeyList = GetParam("keyList").Trim();
            string strKeyList = context.Request.Params["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Params[arrKey[y].Trim()].Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }

    /// <summary>
    /// SellPlanModel实体
    /// </summary>
    /// <param name="strCode">发货单编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellPlanModel GetOrderModel(string strCode, HttpContext context)
    {
        SellPlanModel sellPlanModel = new SellPlanModel();
        string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//制单人ID
        string strPlanNo = strCode;
        string strTitle = context.Request.Params["Title"].ToString().Trim();
        string strID = context.Request.Params["OrderID"].ToString().Trim();
        string strPlanType = context.Request.Params["PlanType"].ToString().Trim();
        string strPlanYear = context.Request.Params["PlanYear"].ToString().Trim();
        string strPlanTime = context.Request.Params["PlanTime"].ToString().Trim();
        string strPlanTotal = context.Request.Params["PlanTotal"].ToString().Trim();
        string strMinPlanTotal = context.Request.Params["MinPlanTotal"].ToString().Trim();
        string strRemark = context.Request.Params["Remark"].ToString().Trim();

        
        string strStartDate = context.Request.Params["StartDate"].ToString().Trim();
        string strEndDate = context.Request.Params["EndDate"].ToString().Trim();
        string strHortation = context.Request.Params["Hortation"].ToString().Trim();
        string strCanViewUser = context.Request.Params["CanViewUser"].ToString().Trim();
        string strCanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
        
        string strBillStatus = "1";

        sellPlanModel.ID = Convert.ToInt32(strID);
        sellPlanModel.CompanyCD = strCompanyCD;
        sellPlanModel.PlanNo = strPlanNo;
        sellPlanModel.Title = strTitle;
        sellPlanModel.PlanType = strPlanType;
        sellPlanModel.PlanYear = strPlanYear;
        sellPlanModel.PlanTime = strPlanTime;
        sellPlanModel.PlanTotal = Convert.ToDecimal(strPlanTotal);
        sellPlanModel.MinPlanTotal = Convert.ToDecimal(strMinPlanTotal);
        sellPlanModel.Remark = strRemark;

        sellPlanModel.Hortation = strHortation;
        sellPlanModel.CanViewUser = strCanViewUser;
        try
        {
            sellPlanModel.StartDate = Convert.ToDateTime(strStartDate);
        }
        catch { }
        try
        {
            sellPlanModel.EndDate = Convert.ToDateTime(strEndDate);
        }
        catch { }
        sellPlanModel.CanViewUserName = strCanViewUserName;
        
        
        sellPlanModel.BillStatus = strBillStatus;
        sellPlanModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        sellPlanModel.CreateDate = DateTime.Now;

        sellPlanModel.ModifiedDate = DateTime.Now;
        sellPlanModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;


        return sellPlanModel;
    }

    /// <summary>
    /// 获取SellSendDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellPlanDetailModel GetOrderDetail(string strCode, HttpContext context)
    {
        SellPlanDetailModel sellPlanDetailModel = new SellPlanDetailModel();
        string strID = context.Request.Params["ID"].ToString().Trim();

        string strPlanNo = strCode;
        string strDetailType = context.Request.Params["DetailType"].ToString().Trim();
        string strParentID = context.Request.Params["ParentID"].ToString().Trim();
        string strDetailID = context.Request.Params["DetailID"].ToString().Trim();
        string strDetailTotal = context.Request.Params["DetailTotal"].ToString().Trim();
        string strMinDetailotal = context.Request.Params["MinDetailotal"].ToString().Trim();
        sellPlanDetailModel.ID = strID == "" ? null : (int?)Convert.ToInt32(strID);
        sellPlanDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD; ;
        sellPlanDetailModel.PlanNo = strPlanNo;
        sellPlanDetailModel.DetailType = strDetailType;
        sellPlanDetailModel.ParentID = strParentID == "" ? null : (int?)Convert.ToInt32(strParentID);
        sellPlanDetailModel.DetailID = strDetailID == "" ? null : (int?)Convert.ToInt32(strDetailID);
        sellPlanDetailModel.DetailTotal = strDetailTotal == "" ? null : (int?)Convert.ToDecimal(strDetailTotal);
        sellPlanDetailModel.MinDetailotal = strMinDetailotal == "" ? null : (int?)Convert.ToDecimal(strMinDetailotal);

        return sellPlanDetailModel;
    }

    /// <summary>
    /// 总结时获取SellSendDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellPlanDetailModel GetOrderDetail1(string strCode, HttpContext context)
    {
        SellPlanDetailModel sellPlanDetailModel = new SellPlanDetailModel();
        string strID = context.Request.Params["ID"].ToString().Trim();

        string strPlanNo = strCode;
        string strSummarizeNote = context.Request.Params["SummarizeNote"].ToString().Trim();
        string strAimRealResult = context.Request.Params["AimRealResult"].ToString().Trim();
        string strAddOrCut = context.Request.Params["AddOrCut"].ToString().Trim();
        string strDifference = context.Request.Params["Difference"].ToString().Trim();
        string strCompletePercent = context.Request.Params["CompletePercent"].ToString().Trim();
        sellPlanDetailModel.ID = strID == "" ? null : (int?)Convert.ToInt32(strID);
       
        sellPlanDetailModel.PlanNo = strPlanNo;
        sellPlanDetailModel.SummarizeDate = DateTime.Now;
        sellPlanDetailModel.SummarizeNote = strSummarizeNote;
        sellPlanDetailModel.Summarizer = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        sellPlanDetailModel.AimRealResult = strAimRealResult;
        sellPlanDetailModel.AddOrCut = strAddOrCut;
        sellPlanDetailModel.Difference = strDifference;
        sellPlanDetailModel.CompletePercent = Convert.ToDecimal(strCompletePercent);
        sellPlanDetailModel.IsSummarize = "1";

        return sellPlanDetailModel;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}