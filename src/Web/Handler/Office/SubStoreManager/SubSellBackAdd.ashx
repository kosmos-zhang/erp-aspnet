<%@ WebHandler Language="C#" Class="SubSellBackAdd" %>

using System;
using System.Web;
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;
using System.Collections;

public class SubSellBackAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());

        // 获得扩展属性
        Hashtable htExtAttr;
        JsonClass jc;

        if (action == "Add")
        {

            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;
            //基本信息
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            if (context.Request.Params["backNo"].Trim().Length == 0)
            {
                model.BackNo = ItemCodingRuleBus.GetCodeValue(CodeType, "SubSellBack", "BackNo");
            }
            else
            {
                model.BackNo = context.Request.Params["backNo"].Trim();
            }

            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SubSellBack"
                                , "BackNo", model.BackNo);
            //存在的场合
            if (!isAlready || string.IsNullOrEmpty(model.BackNo))
            {
                if (string.IsNullOrEmpty(model.BackNo))
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
            if (context.Request.Params["deptID"] != null && context.Request.Params["deptID"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());//销售分店
            }
            if (context.Request.Params["orderID"] != null && context.Request.Params["orderID"] != "")
            {
                model.OrderID = Convert.ToInt32(context.Request.Params["orderID"].ToString().Trim());
            }
            model.SendMode = context.Request.Params["sendMode"].ToString().Trim();
            model.FromType = context.Request.Params["fromType"].ToString().Trim();
            if (context.Request.Params["outDate"].Trim() != null && context.Request.Params["outDate"].Trim() != "")
            {
                model.OutDate = Convert.ToDateTime(context.Request.Params["outDate"].ToString().Trim());
            }
            if (context.Request.Params["outUserID"] != null && context.Request.Params["outUserID"] != "")
            {
                model.OutUserID = Convert.ToInt32(context.Request.Params["outUserID"].ToString().Trim());
            }
            model.isAddTax = context.Request.Params["isAddtax"].ToString().Trim();
            model.CustName = context.Request.Params["custName"].ToString().Trim();
            model.CustTel = context.Request.Params["custTel"].ToString().Trim();
            model.CustMobile = context.Request.Params["custMobile"].ToString().Trim();
            if (context.Request.Params["currencyType"] != null && context.Request.Params["currencyType"] != "")
            {
                model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());
            }
            if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
            {
                model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());
            }
            model.BusiStatus = context.Request.Params["busiStatus"].ToString().Trim();
            if (context.Request.Params["backDate"].Trim() != null && context.Request.Params["backDate"].Trim() != "")
            {
                model.BackDate = Convert.ToDateTime(context.Request.Params["backDate"].ToString().Trim());
            }
            if (context.Request.Params["seller"] != null && context.Request.Params["seller"] != "")
            {
                model.Seller = Convert.ToInt32(context.Request.Params["seller"].ToString().Trim());
            }
            model.CustAddr = context.Request.Params["custAddr"].ToString().Trim();
            model.BackReason = context.Request.Params["backReason"].ToString().Trim();

            // 获得扩展属性
            htExtAttr = GetExtAttr(context);

            //合计信息
            if (context.Request.Params["countTotal"] != null && context.Request.Params["countTotal"] != "")
            {
                model.CountTotal = Convert.ToDecimal(context.Request.Params["countTotal"].ToString().Trim());
            }
            if (context.Request.Params["totalMoney"] != null && context.Request.Params["totalMoney"] != "")
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalMoney"].ToString().Trim());
            }
            if (context.Request.Params["totalTax"] != null && context.Request.Params["totalTax"] != "")
            {
                model.Tax = Convert.ToDecimal(context.Request.Params["totalTax"].ToString().Trim());
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
            if (context.Request.Params["realtotal"] != null && context.Request.Params["realtotal"] != "")
            {
                model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
            }
            if (context.Request.Params["payedTotal"] != null && context.Request.Params["payedTotal"] != "")
            {
                model.PayedTotal = Convert.ToDecimal(context.Request.Params["payedTotal"].ToString().Trim());
            }
            if (context.Request.Params["wairPayTotal"] != null && context.Request.Params["wairPayTotal"] != "")
            {
                model.WairPayTotal = Convert.ToDecimal(context.Request.Params["wairPayTotal"].ToString().Trim());
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
            model.Attachment = context.Request.Params["attachment"].ToString().Trim();
            model.Remark = context.Request.Params["remark"].ToString().Trim();

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
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
            string DetailDiscount = context.Request.Params["DetailDiscount"].ToString().Trim();
            string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
            string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
            string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
            string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
            string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();
            string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();
            string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
            string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            string tempID = "0";
            if (SubSellBackBus.InsertSubSellBack(model, htExtAttr, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailStorageID, DetailFromBillID, DetailFromLineNo, DetailRemark, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, out tempID))
            {
                jc = new JsonClass("保存成功", model.BackNo, int.Parse(tempID));
            }
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Update")
        {
            string no = context.Request.Params["cno"].ToString().Trim();
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;

            model.BackNo = context.Request.Params["cno"].ToString().Trim();//合同编号 

            model.Title = context.Request.Params["title"].ToString().Trim(); //主题
            if (context.Request.Params["deptID"] != null && context.Request.Params["deptID"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());//销售分店
            }
            if (context.Request.Params["orderID"] != null && context.Request.Params["orderID"] != "")
            {
                model.OrderID = Convert.ToInt32(context.Request.Params["orderID"].ToString().Trim());
            }
            model.SendMode = context.Request.Params["sendMode"].ToString().Trim();
            model.FromType = context.Request.Params["fromType"].ToString().Trim();
            if (context.Request.Params["outDate"].Trim() != null && context.Request.Params["outDate"].Trim() != "")
            {
                model.OutDate = Convert.ToDateTime(context.Request.Params["outDate"].ToString().Trim());
            }
            if (context.Request.Params["outUserID"] != null && context.Request.Params["outUserID"] != "")
            {
                model.OutUserID = Convert.ToInt32(context.Request.Params["outUserID"].ToString().Trim());
            }
            model.isAddTax = context.Request.Params["isAddtax"].ToString().Trim();
            model.CustName = context.Request.Params["custName"].ToString().Trim();
            model.CustTel = context.Request.Params["custTel"].ToString().Trim();
            model.CustMobile = context.Request.Params["custMobile"].ToString().Trim();
            if (context.Request.Params["currencyType"] != null && context.Request.Params["currencyType"] != "")
            {
                model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());
            }
            if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
            {
                model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());
            }
            model.BusiStatus = context.Request.Params["busiStatus"].ToString().Trim();
            if (context.Request.Params["backDate"].Trim() != null && context.Request.Params["backDate"].Trim() != "")
            {
                model.BackDate = Convert.ToDateTime(context.Request.Params["backDate"].ToString().Trim());
            }
            if (context.Request.Params["seller"] != null && context.Request.Params["seller"] != "")
            {
                model.Seller = Convert.ToInt32(context.Request.Params["seller"].ToString().Trim());
            }
            model.CustAddr = context.Request.Params["custAddr"].ToString().Trim();
            model.BackReason = context.Request.Params["backReason"].ToString().Trim();

            // 获得扩展属性
            htExtAttr = GetExtAttr(context);
            //合计信息
            if (context.Request.Params["countTotal"] != null && context.Request.Params["countTotal"] != "")
            {
                model.CountTotal = Convert.ToDecimal(context.Request.Params["countTotal"].ToString().Trim());
            }
            if (context.Request.Params["totalMoney"] != null && context.Request.Params["totalMoney"] != "")
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalMoney"].ToString().Trim());
            }
            if (context.Request.Params["totalTax"] != null && context.Request.Params["totalTax"] != "")
            {
                model.Tax = Convert.ToDecimal(context.Request.Params["totalTax"].ToString().Trim());
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
            if (context.Request.Params["realtotal"] != null && context.Request.Params["realtotal"] != "")
            {
                model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
            }
            if (context.Request.Params["payedTotal"] != null && context.Request.Params["payedTotal"] != "")
            {
                model.PayedTotal = Convert.ToDecimal(context.Request.Params["payedTotal"].ToString().Trim());
            }
            if (context.Request.Params["wairPayTotal"] != null && context.Request.Params["wairPayTotal"] != "")
            {
                model.WairPayTotal = Convert.ToDecimal(context.Request.Params["wairPayTotal"].ToString().Trim());
            }


            //备注信息
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
            model.Attachment = context.Request.Params["attachment"].ToString().Trim();
            model.Remark = context.Request.Params["remark"].ToString().Trim();

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
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
            string DetailDiscount = context.Request.Params["DetailDiscount"].ToString().Trim();
            string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
            string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
            string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
            string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
            string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();
            string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID"].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedPrice = context.Request.Params["DetailUsedPrice"].ToString().Trim();
            string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
            string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();


            if (SubSellBackBus.UpdateSubSellBack(model, htExtAttr, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailBackCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailStorageID, DetailFromBillID, DetailFromLineNo, DetailRemark, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, DetailBatchNo, length, no))
                jc = new JsonClass("保存成功", model.BackNo, model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
        }
        else if (action == "Confirm")
        {
            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;
            model.ID = ID;
            model.BackNo = context.Request.Params["BackNo"].ToString().Trim();//合同编号 
            model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            model.ConfirmDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());

            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            if (SubSellBackBus.ConfirmSubSellBack(model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, length))
            {
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "QxConfirm")
        {
            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;
            model.ID = ID;
            model.BackNo = context.Request.Params["BackNo"].ToString().Trim();//合同编号 
            model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            if (context.Request.Params["confirmor"].Trim() != null && context.Request.Params["confirmor"].Trim() != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            }
            if (context.Request.Params["confirmDate"].Trim() != null && context.Request.Params["confirmDate"].Trim() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmDate"].ToString().Trim());
            }

            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            if (SubSellBackBus.QxConfirmSubSellBack(model, DetailBackCount, DetailFromBillNo, DetailFromLineNo, length))
            {
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "Ruku")
        {
            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;
            model.ID = ID;
            model.BackNo = context.Request.Params["BackNo"].ToString().Trim();//合同编号 
            model.DeptID = Convert.ToInt32(context.Request.Params["deptID"].ToString().Trim());
            model.InUserID = Convert.ToInt32(context.Request.Params["inUserID"].ToString().Trim());
            model.InDate = Convert.ToDateTime(context.Request.Params["inDate"].ToString().Trim());
            model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            model.BusiStatus = context.Request.Params["busiStatus"].ToString().Trim();
            model.SendMode = context.Request.Params["sendMode"].ToString().Trim();
            model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());
            model.Remark = context.Request.UrlReferrer.ToString();
            if (context.Request.Params["payedTotal"] != null && context.Request.Params["payedTotal"] != "")
            {
                model.PayedTotal = Convert.ToDecimal(context.Request.Params["payedTotal"].ToString().Trim());
            }
            if (context.Request.Params["wairPayTotal"] != null && context.Request.Params["wairPayTotal"] != "")
            {
                model.WairPayTotal = Convert.ToDecimal(context.Request.Params["wairPayTotal"].ToString().Trim());
            }


            string DetailBackCount = context.Request.Params["DetailBackCount"].ToString().Trim();
            string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();
            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();

            if (SubSellBackBus.RukuSubSellBack(model, DetailBackCount, DetailStorageID, DetailProductID, DetailUnitPrice, DetailBatchNo, length))
            {
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (action == "Jiesuan")
        {
            SubSellBackModel model = new SubSellBackModel();
            model.CompanyCD = CompanyCD;
            model.ID = ID;
            model.SttlUserID = Convert.ToInt32(context.Request.Params["sttlUserID"].ToString().Trim());
            model.SttlDate = Convert.ToDateTime(context.Request.Params["sttlDate"].ToString().Trim());
            model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();
            model.BusiStatus = context.Request.Params["busiStatus"].ToString().Trim();
            model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());
            if (context.Request.Params["payedTotal"] != null && context.Request.Params["payedTotal"] != "")
            {
                model.PayedTotal = Convert.ToDecimal(context.Request.Params["payedTotal"].ToString().Trim());
            }
            if (context.Request.Params["wairPayTotal"] != null && context.Request.Params["wairPayTotal"] != "")
            {
                model.WairPayTotal = Convert.ToDecimal(context.Request.Params["wairPayTotal"].ToString().Trim());
            }


            if (SubSellBackBus.JiesuanubSellBack(model))
            {
                jc = new JsonClass("success", "", 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
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

    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = context.Request.Form["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Form[arrKey[y].Trim()].ToString().Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }

}