<%@ WebHandler Language="C#" Class="PurchaseRejectAdd" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;
using System.Collections;

public class PurchaseRejectAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    protected HttpContext _context = null;
    private int id = 0;
    public void ProcessRequest(HttpContext context)
    {
        _context = context;
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        string strMsg = string.Empty;//操作返回的信息  

        JsonClass jc;
        Hashtable ht = GetExtAttr();
        if (action == "Add")
        {
            string RejectNo = context.Request.Params["rejectNo"].Trim();
            PurchaseRejectModel model = new PurchaseRejectModel();
            model.CompanyCD = CompanyCD;
            //基本信息
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            if (context.Request.Params["rejectNo"].Trim().Length == 0)
            {
                model.RejectNo = ItemCodingRuleBus.GetCodeValue(CodeType, "PurchaseReject", "RejectNo");//合同编号
            }
            else
            {
                model.RejectNo = context.Request.Params["rejectNo"].Trim();//合同编号  
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("PurchaseReject", "RejectNo", model.RejectNo);
            //存在的场合
            if (!isAlready || string.IsNullOrEmpty(model.RejectNo))
            {
                if (string.IsNullOrEmpty(model.RejectNo))
                {//该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 0);
                    context.Response.Write(jc);
                }
                else
                {//单据编号已经存在
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号!", "", 0);
                    context.Response.Write(jc);
                }
                return;
            }
            model.Title = context.Request.Params["title"].ToString().Trim(); //主题
            if (context.Request.Params["fromType"] != null && context.Request.Params["fromType"] != "")
            {
                model.FromType = context.Request.Params["fromType"].ToString().Trim();
            }
            if (context.Request.Params["providerID"] != null && context.Request.Params["providerID"] != "")
            {
                model.ProviderID = Convert.ToInt32(context.Request.Params["providerID"].ToString().Trim());
            }
            if (context.Request.Params["typeID"].Trim() != null && context.Request.Params["typeID"].Trim() != "")
            {
                model.TypeID = Convert.ToInt32(context.Request.Params["typeID"].ToString().Trim());
            }
            if (context.Request.Params["deptid"] != null && context.Request.Params["deptid"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptid"].ToString().Trim());
            }
            if (context.Request.Params["projectID"] != null && int.TryParse(context.Request.Params["projectID"], out id))
            {
                model.ProjectID = id;
            }
            if (context.Request.Params["purchase"] != null && context.Request.Params["purchase"] != "")
            {
                model.Purchaser = Convert.ToInt32(context.Request.Params["purchase"].ToString().Trim());
            }
            if (context.Request.Params["takeType"].Trim() != null && context.Request.Params["takeType"].Trim() != "")
            {
                model.TakeType = Convert.ToInt32(context.Request.Params["takeType"].ToString().Trim());
            }
            if (context.Request.Params["carryType"].Trim() != null && context.Request.Params["carryType"].Trim() != "")
            {
                model.CarryType = Convert.ToInt32(context.Request.Params["carryType"].ToString().Trim());
            }
            if (context.Request.Params["payeType"].Trim() != null && context.Request.Params["payeType"].Trim() != "")
            {
                model.PayType = Convert.ToInt32(context.Request.Params["payeType"].ToString().Trim());
            }
            if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
            {
                model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());
            }
            if (context.Request.Params["currencyType"] != null && context.Request.Params["currencyType"] != "")
            {
                model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());
            }
            model.isAddTax = context.Request.Params["isAddtax"].ToString().Trim();
            model.ReceiveMan = context.Request.Params["receiveMan"].ToString().Trim();
            model.ReceiveTel = context.Request.Params["receiveTel"].ToString().Trim();
            if (context.Request.Params["rejectDate"].Trim() != null && context.Request.Params["rejectDate"].Trim() != "")
            {
                model.RejectDate = Convert.ToDateTime(context.Request.Params["rejectDate"].ToString().Trim());
            }
            if (context.Request.Params["moneyType"].Trim() != null && context.Request.Params["moneyType"].Trim() != "")
            {
                model.MoneyType = Convert.ToInt32(context.Request.Params["moneyType"].ToString().Trim());
            }
            model.SendAddress = context.Request.Params["sendAddress"].ToString().Trim();
            model.ReceiveOverAddress = context.Request.Params["receiveOverAddress"].ToString().Trim();
            //合计信息
            if (context.Request.Params["countTotal"] != null && context.Request.Params["countTotal"] != "")
            {
                model.CountTotal = Convert.ToDecimal(context.Request.Params["countTotal"].ToString().Trim());
            }
            if (context.Request.Params["totalMoney"].Trim() != null && context.Request.Params["totalMoney"].Trim() != "")
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalMoney"].ToString().Trim());
            }
            if (context.Request.Params["totalTax"] != null && context.Request.Params["totalTax"] != "")
            {
                model.TotalTax = Convert.ToDecimal(context.Request.Params["totalTax"].ToString().Trim());
            }
            if (context.Request.Params["totalfee"] != null && context.Request.Params["totalfee"] != "")
            {
                model.TotalFee = Convert.ToDecimal(context.Request.Params["totalfee"].ToString().Trim());
            }
            if (context.Request.Params["discount"] != null && context.Request.Params["discount"] != "")
            {
                model.Discount = Convert.ToDecimal(context.Request.Params["discount"].ToString().Trim());
            }
            if (context.Request.Params["discounttotal"] != null && context.Request.Params["discounttotal"] != "")
            {
                model.DiscountTotal = Convert.ToDecimal(context.Request.Params["discounttotal"].ToString().Trim());
            }
            if (context.Request.Params["realtotal"].Trim() != null && context.Request.Params["realtotal"].Trim() != "")
            {
                model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
            }
            if (context.Request.Params["totalDyfzk"].Trim() != null && context.Request.Params["totalDyfzk"].Trim() != "")
            {
                model.TotalDyfzk = Convert.ToDecimal(context.Request.Params["totalDyfzk"].ToString().Trim());
            }
            if (context.Request.Params["totalYthkhj"].Trim() != null && context.Request.Params["totalYthkhj"].Trim() != "")
            {
                model.TotalYthkhj = Convert.ToDecimal(context.Request.Params["totalYthkhj"].ToString().Trim());
            }
            //备注信息
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"] != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());
            }

            if (context.Request.Params["createDate"].Trim() != null && context.Request.Params["createDate"].Trim() != "")
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["createDate"].ToString().Trim());
            }
            if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
            {
                model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            }
            if (context.Request.Params["confirmor"] != null && context.Request.Params["confirmor"] != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmDate"].Trim() != null && context.Request.Params["confirmDate"].Trim() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmDate"].ToString().Trim());
            }
            if (context.Request.Params["closer"] != null && context.Request.Params["closer"] != "")
            {
                model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());
            }
            if (context.Request.Params["closeDate"].Trim() != null && context.Request.Params["closeDate"].Trim() != "")
            {
                model.CloseDate = Convert.ToDateTime(context.Request.Params["closeDate"].ToString().Trim());
            }
            if (context.Request.Params["modifiedUserID"] != null && context.Request.Params["modifiedUserID"] != "")
            {
                model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();
            }
            model.remark = context.Request.Params["remark"].ToString().Trim();

            if (ID > 0)
            {
                model.ID = ID;
            }

            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
            string DetailProductName = context.Request.Params["DetailProductName"].ToString().Trim();
            string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
            string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailApplyReason = context.Request.Params["DetailApplyReason"].ToString().Trim();
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
            string DetailDiscount = "";
            string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
            string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
            string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
            string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
            string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
            string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            string tempID = "0";
            if (PurchaseRejectBus.InsertPurchaseReject(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailApplyReason, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, out tempID, ht))
            {
                jc = new JsonClass("保存成功", model.RejectNo, int.Parse(tempID));
            }
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Update")
        {
            string fflag2 = context.Request.Params["fflag2"].ToString();
            string no = context.Request.Params["cno"].ToString().Trim();
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            PurchaseRejectModel model = new PurchaseRejectModel();
            model.CompanyCD = CompanyCD;
            model.RejectNo = context.Request.Params["cno"].ToString().Trim();//合同编号 
            model.Title = context.Request.Params["title"].ToString().Trim(); //主题
            if (context.Request.Params["fromType"] != null && context.Request.Params["fromType"] != "")
            {
                model.FromType = context.Request.Params["fromType"].ToString().Trim();
            }
            if (context.Request.Params["providerID"] != null && context.Request.Params["providerID"] != "")
            {
                model.ProviderID = Convert.ToInt32(context.Request.Params["providerID"].ToString().Trim());
            }
            if (context.Request.Params["typeID"].Trim() != null && context.Request.Params["typeID"].Trim() != "")
            {
                model.TypeID = Convert.ToInt32(context.Request.Params["typeID"].ToString().Trim());
            }
            if (context.Request.Params["deptid"] != null && context.Request.Params["deptid"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptid"].ToString().Trim());
            }
            if (context.Request.Params["projectID"] != null && int.TryParse(context.Request.Params["projectID"], out id))
            {
                model.ProjectID = id;
            }
            if (context.Request.Params["purchase"] != null && context.Request.Params["purchase"] != "")
            {
                model.Purchaser = Convert.ToInt32(context.Request.Params["purchase"].ToString().Trim());
            }
            if (context.Request.Params["takeType"].Trim() != null && context.Request.Params["takeType"].Trim() != "")
            {
                model.TakeType = Convert.ToInt32(context.Request.Params["takeType"].ToString().Trim());
            }
            if (context.Request.Params["carryType"].Trim() != null && context.Request.Params["carryType"].Trim() != "")
            {
                model.CarryType = Convert.ToInt32(context.Request.Params["carryType"].ToString().Trim());
            }
            if (context.Request.Params["payeType"].Trim() != null && context.Request.Params["payeType"].Trim() != "")
            {
                model.PayType = Convert.ToInt32(context.Request.Params["payeType"].ToString().Trim());
            }
            if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
            {
                model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());
            }
            if (context.Request.Params["currencyType"] != null && context.Request.Params["currencyType"] != "")
            {
                model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());
            }
            model.isAddTax = context.Request.Params["isAddtax"].ToString().Trim();
            model.ReceiveMan = context.Request.Params["receiveMan"].ToString().Trim();
            model.ReceiveTel = context.Request.Params["receiveTel"].ToString().Trim();
            if (context.Request.Params["rejectDate"].Trim() != null && context.Request.Params["rejectDate"].Trim() != "")
            {
                model.RejectDate = Convert.ToDateTime(context.Request.Params["rejectDate"].ToString().Trim());
            }
            if (context.Request.Params["moneyType"].Trim() != null && context.Request.Params["moneyType"].Trim() != "")
            {
                model.MoneyType = Convert.ToInt32(context.Request.Params["moneyType"].ToString().Trim());
            }
            model.SendAddress = context.Request.Params["sendAddress"].ToString().Trim();
            model.ReceiveOverAddress = context.Request.Params["receiveOverAddress"].ToString().Trim();
            //合计信息
            if (context.Request.Params["countTotal"] != null && context.Request.Params["countTotal"] != "")
            {
                model.CountTotal = Convert.ToDecimal(context.Request.Params["countTotal"].ToString().Trim());
            }
            if (context.Request.Params["totalMoney"].Trim() != null && context.Request.Params["totalMoney"].Trim() != "")
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalMoney"].ToString().Trim());
            }
            if (context.Request.Params["totalTax"] != null && context.Request.Params["totalTax"] != "")
            {
                model.TotalTax = Convert.ToDecimal(context.Request.Params["totalTax"].ToString().Trim());
            }
            if (context.Request.Params["totalfee"] != null && context.Request.Params["totalfee"] != "")
            {
                model.TotalFee = Convert.ToDecimal(context.Request.Params["totalfee"].ToString().Trim());
            }
            if (context.Request.Params["discount"] != null && context.Request.Params["discount"] != "")
            {
                model.Discount = Convert.ToDecimal(context.Request.Params["discount"].ToString().Trim());
            }
            if (context.Request.Params["discounttotal"] != null && context.Request.Params["discounttotal"] != "")
            {
                model.DiscountTotal = Convert.ToDecimal(context.Request.Params["discounttotal"].ToString().Trim());
            }
            if (context.Request.Params["realtotal"].Trim() != null && context.Request.Params["realtotal"].Trim() != "")
            {
                model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
            }
            if (context.Request.Params["totalDyfzk"].Trim() != null && context.Request.Params["totalDyfzk"].Trim() != "")
            {
                model.TotalDyfzk = Convert.ToDecimal(context.Request.Params["totalDyfzk"].ToString().Trim());
            }
            if (context.Request.Params["totalYthkhj"].Trim() != null && context.Request.Params["totalYthkhj"].Trim() != "")
            {
                model.TotalYthkhj = Convert.ToDecimal(context.Request.Params["totalYthkhj"].ToString().Trim());
            }
            //备注信息
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"] != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());
            }
            if (context.Request.Params["createDate"].Trim() != null && context.Request.Params["createDate"].Trim() != "")
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["createDate"].ToString().Trim());
            }
            if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
            {
                model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            }
            if (context.Request.Params["confirmor"] != null && context.Request.Params["confirmor"] != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmDate"].Trim() != null && context.Request.Params["confirmDate"].Trim() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmDate"].ToString().Trim());
            }
            if (context.Request.Params["closer"] != null && context.Request.Params["closer"] != "")
            {
                model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());
            }
            if (context.Request.Params["closeDate"].Trim() != null && context.Request.Params["closeDate"].Trim() != "")
            {
                model.CloseDate = Convert.ToDateTime(context.Request.Params["closeDate"].ToString().Trim());
            }
            if (context.Request.Params["modifiedUserID"] != null && context.Request.Params["modifiedUserID"] != "")
            {
                model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();
            }
            model.remark = context.Request.Params["remark"].ToString().Trim();

            //string no = context.Request.Params["cno"].ToString().Trim();
            //int ID = int.Parse(context.Request.Params["ID"].ToString());
            if (ID > 0)
            {
                model.ID = ID;
            }

            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
            string DetailProductName = context.Request.Params["DetailProductName"].ToString().Trim();
            string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
            string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailApplyReason = context.Request.Params["DetailApplyReason"].ToString().Trim();
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
            string DetailDiscount = "";
            string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
            string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
            string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
            string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
            string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
            string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            if (PurchaseRejectBus.UpdatePurchaseReject(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailApplyReason, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRemark, DetailFromBillID, DetailFromBillNo, DetailFromLineNo, DetailUsedUnitCount, DetailUsedUnitID, DetailUsedPrice, length, fflag2, no, ht))
                jc = new JsonClass("保存成功", model.RejectNo, model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }



        else if (action == "Confirm")
        {
            PurchaseRejectModel Model = new PurchaseRejectModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            Model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            Model.ConfirmDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());
            Model.FromType = context.Request.Params["FromType"].ToString().Trim();
            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();
            string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
            //string arriveNo = context.Request.Params["arriveNo"];
            //int confirmor = Convert.ToInt32(context.Request.Params["confirmor"]);
            if (PurchaseRejectBus.ConfirmPurchaseReject(Model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, DetailProductNo, length, out strMsg))
            {
                jc = new JsonClass("success", strMsg, 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", strMsg, 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "CancelConfirm")
        {
            PurchaseRejectModel Model = new PurchaseRejectModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            if (context.Request.Params["confirmor"].Trim().ToString() != "")
            {
                Model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmdate"] != null && context.Request.Params["confirmdate"].Trim().ToString() != "")
            {
                Model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmdate"].ToString().Trim());//确认时间
            }
            Model.FromType = context.Request.Params["FromType"].ToString().Trim();
            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            //string arriveNo = context.Request.Params["arriveNo"];
            //int confirmor = Convert.ToInt32(context.Request.Params["confirmor"]);
            if (PurchaseRejectBus.CancelConfirmPurchaseReject(Model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, DetailProductNo, length, out strMsg))
            {
                jc = new JsonClass("success", strMsg, 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", strMsg, 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "Close")
        {
            PurchaseRejectModel Model = new PurchaseRejectModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            Model.Closer = int.Parse(context.Request.Params["closer"].ToString().Trim());
            Model.CloseDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());
            if (PurchaseRejectBus.ClosePurchaseReject(Model))
            {
                jc = new JsonClass("结单成功", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("结单失败", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "CancelClose")
        {
            PurchaseRejectModel Model = new PurchaseRejectModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            int closer = Convert.ToInt32(context.Request.Params["closer"]);
            if (true == PurchaseRejectBus.CancelClosePurchaseReject(Model))
            {
                jc = new JsonClass("取消结单成功", "", 2);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("取消结单失败", "", 0);
                context.Response.Write(jc);
            }
        }

    }
    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr()
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = GetParam("keyList").Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), GetParam(arrKey[y].Trim()).Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }

    /// <summary>
    /// 获取REQUEST的参数值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetParam(string key)
    {
        if (_context.Request[key] == null)
        {
            return string.Empty;
        }
        else
        {
            if (_context.Request[key].ToString().Trim() + "" == "")
            {
                return string.Empty;
            }
            else
            {
                return _context.Request[key].ToString().Trim();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}