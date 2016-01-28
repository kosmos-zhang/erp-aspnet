<%@ WebHandler Language="C#" Class="PurchaseContractAdd" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;
using System.Collections;
public class PurchaseContractAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    protected HttpContext _context = null;
    public void ProcessRequest (HttpContext context) {
        _context = context;    
        PurchaseContractModel model = new PurchaseContractModel(); 
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        int Confirmor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人 
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        string strMsg = string.Empty;//操作返回的信息  
        


        if (action == "Add")
        {
            JsonClass jc;
            string ContractNo = context.Request.Params["contractNo"].Trim();
          

         
                string CodeType = context.Request.Params["CodeType"].ToString().Trim();
                model.CompanyCD = CompanyCD;
                if (action == "Add")
                {
                    if (context.Request.Params["contractNo"].Length == 0)
                    {
                        model.ContractNo = ItemCodingRuleBus.GetCodeValue(CodeType, "PurchaseContract", "ContractNo");//合同编号
                    }
                    else
                    {
                        model.ContractNo = context.Request.Params["contractNo"].ToString().Trim();//合同编号  
                    }
                }
                else
                {
                    model.ContractNo = context.Request.Params["contractNo"].ToString().Trim();//合同编号 
                }
                //判断是否存在
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("PurchaseContract"
                                    , "ContractNo", model.ContractNo);

                //存在的场合
                if (!isAlready || string.IsNullOrEmpty(model.ContractNo))
                {
                    if (string.IsNullOrEmpty(model.ContractNo))
                    {
                        jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限,请检查编号规则设置!", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号!", "", 0);
                        context.Response.Write(jc);
                        return;
                    }


                }
                model.Title = context.Request.Params["Title"].ToString().Trim(); //主题
                if (context.Request.Params["ProviderID"] != null && context.Request.Params["ProviderID"].Trim().ToString() != "")
                {
                    model.ProviderID = Convert.ToInt32(context.Request.Params["providerID"].ToString().Trim());//供应商ID（对应供应商表ID）
                }
                if (context.Request.Params["FromType"] != null)
                {
                    model.FromType = context.Request.Params["FromType"].ToString().Trim();//源单类型（0无来源，1采购申请，2采购计划，3询价单）
                }
                if (context.Request.Params["FromBillID"] != null && context.Request.Params["FromBillID"].Trim().ToString() != "")
                {
                    model.FromBillID = Convert.ToInt32(context.Request.Params["FromBillID"].ToString().Trim());//源单ID（采购计划ID或询价单ID）
                }
                //if (context.Request.Params["TotalPrice"] != null)
                //{
                //    model.TotalPrice = Convert.ToDecimal(context.Request.Params["TotalPrice"].ToString().Trim());//合同总金额
                //}
                if (context.Request.Params["currencyType"].Trim() != null && context.Request.Params["currencyType"].Trim() != "")
                {
                    model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());//币种ID(对应货币代码表CD)
                }
                if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
                {
                    model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());//汇率
                }
                if (context.Request.Params["payeType"].Trim() != null && context.Request.Params["payeType"].Trim() !="")
                {
                    model.PayType = Convert.ToInt32(context.Request.Params["payeType"].ToString().Trim());//结算方式ID
                }
                if (context.Request.Params["moneyType"].Trim() != null && context.Request.Params["moneyType"].Trim() != "")
                {
                    model.MoneyType = Convert.ToInt32(context.Request.Params["moneyType"].ToString().Trim());//支付方式ID
                }
                if (context.Request.Params["SignDate"] != null && context.Request.Params["SignDate"].Trim().ToString() != "")
                {
                    model.SignDate = Convert.ToDateTime(context.Request.Params["SignDate"].ToString().Trim());//签约时间
                }
                if (context.Request.Params["Seller"] != null && context.Request.Params["Seller"].Trim().ToString() != "")
                {
                    model.Seller = Convert.ToInt32(context.Request.Params["Seller"].ToString().Trim());//业务员ID(对应员工表ID)
                }
                model.TheyDelegate = context.Request.Params["OppositerUserID"].ToString().Trim(); //对方签约人
                if (context.Request.Params["ourUserID"].Trim().ToString() != null && context.Request.Params["ourUserID"].Trim().ToString() != "")
                {
                    model.OurDelegate = Convert.ToInt32(context.Request.Params["ourUserID"].ToString().Trim());//我方签约人ID
                }
                model.SignAddr = context.Request.Params["address"].ToString().Trim();//签约地点
                //if (context.Request.Params["status"] != null && context.Request.Params["status"] !="")
                //{
                //    model.Status = context.Request.Params["status"].ToString().Trim();//合同状态：1：执行中 2：执行完 3：终止
                //}
                ////合同状态新建合同页面上没有
                if (context.Request.Params["Note"] != null)
                {
                    model.Note = context.Request.Params["Note"].ToString().Trim();//终止原因 
                }
                else
                {
                    model.Note = "";//终止原因 
                }
                if (context.Request.Params["TakeType"].Trim() != null && context.Request.Params["TakeType"].Trim() !="")
                {
                    model.TakeType = Convert.ToInt32(context.Request.Params["TakeType"].ToString().Trim());//交货方式ID
                }
                if (context.Request.Params["CarryType"].Trim() != null && context.Request.Params["CarryType"].Trim() !="")
                {
                    model.CarryType = Convert.ToInt32(context.Request.Params["CarryType"].ToString().Trim());//运送方式ID
                }
                model.Attachment = context.Request.Params["Attachment"].ToString().Trim();//附件
                model.remark = context.Request.Params["remark"].ToString().Trim();//备注
                if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
                {
                    model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();//单据状态
                }
                if (context.Request.Params["creator"].Trim() != null && context.Request.Params["creator"].Trim() != "")
                {
                    model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());//制单人ID
                }
                if (context.Request.Params["CreateDate"] != null)
                {
                    model.CreateDate = Convert.ToDateTime(context.Request.Params["CreateDate"].ToString().Trim());//制单日期
                }
                model.ModifiedUserID = context.Request.Params["ModifiedUserID"].ToString().Trim();//最后更新用户ID(对应操作用户UserID)
                model.Confirmor = Confirmor;
                if (context.Request.Params["ConfirmDate"] != null && context.Request.Params["ConfirmDate"].Trim().ToString() != "")
                {
                    model.ConfirmDate = Convert.ToDateTime(context.Request.Params["ConfirmDate"].ToString().Trim());//确认时间
                }
                if (context.Request.Params["closer"].Trim().ToString() != "")
                {
                    model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());//结单人
                }
                if (context.Request.Params["CloseDate"] != null && context.Request.Params["CloseDate"].Trim().ToString() != "")
                {
                    model.CloseDate = Convert.ToDateTime(context.Request.Params["CloseDate"].ToString().Trim());//结单时间
                }
                string no = context.Request.Params["cno"].ToString().Trim();
                if (context.Request.Params["deptid"] != null && context.Request.Params["deptid"] != "")
                {
                    model.DeptID = Convert.ToInt32(context.Request.Params["deptid"].ToString().Trim());
                }
                model.isAddTax = context.Request.Params["isaddtax"].ToString().Trim();
                if (context.Request.Params["totalmoney"] != null && context.Request.Params["totalmoney"] != "")
                {
                    model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalmoney"].ToString().Trim());
                }
                if (context.Request.Params["counttotal"] != null && context.Request.Params["counttotal"] != "")
                {
                    model.CountTotal = Convert.ToDecimal(context.Request.Params["counttotal"].ToString().Trim());
                }
                if (context.Request.Params["totaltax"] != null && context.Request.Params["totaltax"] != "")
                {
                    model.TotalTax = Convert.ToDecimal(context.Request.Params["totaltax"].ToString().Trim());
                }
                if (context.Request.Params["totalfee"] != null && context.Request.Params["totalfee"] != "")
                {
                    model.TotalFee = Convert.ToDecimal(context.Request.Params["totalfee"].ToString().Trim());
                }
                if (context.Request.Params["discounth"] != null && context.Request.Params["discounth"] != "")
                {
                    model.Discount = Convert.ToDecimal(context.Request.Params["discounth"].ToString().Trim());
                }
                if (context.Request.Params["discounttotal"] != null && context.Request.Params["discounttotal"] != "")
                {
                    model.DiscountTotal = Convert.ToDecimal(context.Request.Params["discounttotal"].ToString().Trim());
                }
                if (context.Request.Params["realtotal"].Trim().ToString() != "" && context.Request.Params["realtotal"] != null)
                {
                    model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
                }
                if (context.Request.Params["typeID"].ToString().Trim() != null && context.Request.Params["typeID"].ToString().Trim() != "")
                {
                    model.TypeID = Convert.ToInt32(context.Request.Params["typeID"].ToString().Trim());
                }
                if (ID > 0)
                {
                    model.ID = ID;
                }




                string DetailID = context.Request.Params["DetailID"].ToString().Trim();
                string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
                string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
                string DetailProductName = context.Request.Params["DetailProductName"].ToString().Trim();
                //string DetailStandard = context.Request.Params["DetailStandard"].ToString().Trim();
                string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
                string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
                string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
                string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
                //string DetailDiscount = context.Request.Params["DetailDiscount"].ToString().Trim();
                string DetailDiscount = "";
                string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
                string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
                string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
                string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
                string DetailRequireDate = context.Request.Params["DetailRequireDate"].ToString().Trim();
                string DetailApplyReason = context.Request.Params["DetailApplyReason"].ToString().Trim();
                string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
                string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
                string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();

                string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID "].ToString().Trim();
                string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
                string DetailUsedPrice = context.Request.Params["DetailUsedPrice "].ToString().Trim();
                string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
                
                
                string length = context.Request.Params["length"].ToString().Trim();



                //string action = context.Request.Params["action"].ToString().Trim();


                // 明细来源信息
                string dtlSInfo = context.Request.Params["dtlSInfo"].ToString().Trim();
                string dtlInfo = context.Request.Params["dtlInfo"].ToString().Trim();
                string[] strarray = null;
                strarray = dtlSInfo.Split('|');
                string[] strarray2 = null;
                strarray2 = dtlInfo.Split('|');
                Hashtable ht = GetExtAttr();
                string tempID = "0";
                if (PurchaseContractBus.InsertPurchaseContract(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRequireDate, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromLineNo,DetailUsedUnitID ,DetailUsedUnitCount ,DetailUsedPrice ,DetailExRate, length, out tempID,ht ))
                {
                    //string ContractDetailID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContractDetail").ToString();
                    jc = new JsonClass("保存成功", model.ContractNo, int.Parse(tempID));
                }
                else
                    jc = new JsonClass("保存失败", "", 0);
                context.Response.Write(jc);
            
        }
        else if (action == "Update")
        {
            Hashtable ht = GetExtAttr();
            string fflag2 = context.Request.Params["fflag2"].ToString();
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            model.CompanyCD = CompanyCD;
            if (action == "Add")
            {
                if (context.Request.Params["contractNo"].Length == 0)
                {
                    model.ContractNo = ItemCodingRuleBus.GetCodeValue(CodeType);//合同编号
                }
                else
                {
                    model.ContractNo = context.Request.Params["contractNo"].ToString().Trim();//合同编号  
                }
            }
            else
            {
                model.ContractNo = context.Request.Params["contractNo"].ToString().Trim();//合同编号 
            }
            model.Title = context.Request.Params["Title"].ToString().Trim(); //主题
            if (context.Request.Params["ProviderID"] != null && context.Request.Params["ProviderID"].Trim().ToString() != "")
            {
                model.ProviderID = Convert.ToInt32(context.Request.Params["providerID"].ToString().Trim());//供应商ID（对应供应商表ID）
            }
            if (context.Request.Params["FromType"] != null)
            {
                model.FromType = context.Request.Params["FromType"].ToString().Trim();//源单类型（0无来源，1采购申请，2采购计划，3询价单）
            }
            if (context.Request.Params["FromBillID"] != null && context.Request.Params["FromBillID"].Trim().ToString() != "")
            {
                model.FromBillID = Convert.ToInt32(context.Request.Params["FromBillID"].ToString().Trim());//源单ID（采购计划ID或询价单ID）
            }
            //if (context.Request.Params["TotalPrice"] != null)
            //{
            //    model.TotalPrice = Convert.ToDecimal(context.Request.Params["TotalPrice"].ToString().Trim());//合同总金额
            //}
            if (context.Request.Params["currencyType"].Trim() != null && context.Request.Params["currencyType"].Trim() != "")
            {
                model.CurrencyType = Convert.ToInt32(context.Request.Params["currencyType"].ToString().Trim());//币种ID(对应货币代码表CD)
            }
            if (context.Request.Params["rate"] != null && context.Request.Params["rate"] != "")
            {
                model.Rate = Convert.ToDecimal(context.Request.Params["rate"].ToString().Trim());//汇率
            }
            if (context.Request.Params["payeType"].Trim() != null && context.Request.Params["payeType"].Trim() !="")
            {
                model.PayType = Convert.ToInt32(context.Request.Params["payeType"].ToString().Trim());//结算方式ID
            }
            if (context.Request.Params["moneyType"].Trim() != null && context.Request.Params["moneyType"].Trim() != "")
            {
                model.MoneyType = Convert.ToInt32(context.Request.Params["moneyType"].ToString().Trim());//支付方式ID
            }
            if (context.Request.Params["SignDate"] != null && context.Request.Params["SignDate"].Trim().ToString() != "")
            {
                model.SignDate = Convert.ToDateTime(context.Request.Params["SignDate"].ToString().Trim());//签约时间
            }
            if (context.Request.Params["Seller"] != null && context.Request.Params["Seller"].Trim().ToString() != "")
            {
                model.Seller = Convert.ToInt32(context.Request.Params["Seller"].ToString().Trim());//业务员ID(对应员工表ID)
            }
            model.TheyDelegate = context.Request.Params["OppositerUserID"].ToString().Trim(); //对方签约人
            if (context.Request.Params["ourUserID"].Trim().ToString() != null && context.Request.Params["ourUserID"].Trim().ToString() != "")
            {
                model.OurDelegate = Convert.ToInt32(context.Request.Params["ourUserID"].ToString().Trim());//我方签约人ID
            }
            model.SignAddr = context.Request.Params["address"].ToString().Trim();//签约地点
            //if (context.Request.Params["status"] != null && context.Request.Params["status"] !="")
            //{
            //    model.Status = context.Request.Params["status"].ToString().Trim();//合同状态：1：执行中 2：执行完 3：终止
            //}
            ////合同状态新建合同页面上没有
            if (context.Request.Params["Note"] != null)
            {
                model.Note = context.Request.Params["Note"].ToString().Trim();//终止原因 
            }
            else
            {
                model.Note = "";//终止原因 
            }
            if (context.Request.Params["TakeType"].Trim() != null && context.Request.Params["TakeType"].Trim() !="")
            {
                model.TakeType = Convert.ToInt32(context.Request.Params["TakeType"].ToString().Trim());//交货方式ID
            }
            if (context.Request.Params["CarryType"].Trim() != null && context.Request.Params["CarryType"].Trim() !="")
            {
                model.CarryType = Convert.ToInt32(context.Request.Params["CarryType"].ToString().Trim());//运送方式ID
            }
            model.Attachment = context.Request.Params["Attachment"].ToString().Trim();//附件
            model.remark = context.Request.Params["remark"].ToString().Trim();//备注
            if (context.Request.Params["billStatus"] != null && context.Request.Params["billStatus"] != "")
            {
                model.BillStatus = context.Request.Params["billStatus"].ToString().Trim();//单据状态
            }
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"].Trim().ToString() != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());//制单人ID
            }
            if (context.Request.Params["CreateDate"] != null)
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["CreateDate"].ToString().Trim());//制单日期
            }
            model.ModifiedUserID = context.Request.Params["ModifiedUserID"].ToString().Trim();//最后更新用户ID(对应操作用户UserID)
            model.Confirmor = Confirmor;
            if (context.Request.Params["ConfirmDate"] != null && context.Request.Params["ConfirmDate"].Trim().ToString() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["ConfirmDate"].ToString().Trim());//确认时间
            }
            if (context.Request.Params["closer"].Trim().ToString() != "")
            {
                model.Closer = Convert.ToInt32(context.Request.Params["closer"].ToString().Trim());//结单人
            }
            if (context.Request.Params["CloseDate"] != null && context.Request.Params["CloseDate"].Trim().ToString() != "")
            {
                model.CloseDate = Convert.ToDateTime(context.Request.Params["CloseDate"].ToString().Trim());//结单时间
            }
            string no = context.Request.Params["cno"].ToString().Trim();
            if (context.Request.Params["deptid"] != null && context.Request.Params["deptid"] != "")
            {
                model.DeptID = Convert.ToInt32(context.Request.Params["deptid"].ToString().Trim());
            }
            model.isAddTax = context.Request.Params["isaddtax"].ToString().Trim();
            if (context.Request.Params["totalmoney"] != null && context.Request.Params["totalmoney"] != "")
            {
                model.TotalPrice = Convert.ToDecimal(context.Request.Params["totalmoney"].ToString().Trim());
            }
            if (context.Request.Params["counttotal"] != null && context.Request.Params["counttotal"] != "")
            {
                model.CountTotal = Convert.ToDecimal(context.Request.Params["counttotal"].ToString().Trim());
            }
            if (context.Request.Params["totaltax"] != null && context.Request.Params["totaltax"] != "")
            {
                model.TotalTax = Convert.ToDecimal(context.Request.Params["totaltax"].ToString().Trim());
            }
            if (context.Request.Params["totalfee"] != null && context.Request.Params["totalfee"] != "")
            {
                model.TotalFee = Convert.ToDecimal(context.Request.Params["totalfee"].ToString().Trim());
            }
            if (context.Request.Params["discounth"] != null && context.Request.Params["discounth"] != "")
            {
                model.Discount = Convert.ToDecimal(context.Request.Params["discounth"].ToString().Trim());
            }
            if (context.Request.Params["discounttotal"] != null && context.Request.Params["discounttotal"] != "")
            {
                model.DiscountTotal = Convert.ToDecimal(context.Request.Params["discounttotal"].ToString().Trim());
            }
            if (context.Request.Params["realtotal"].Trim().ToString() != "" && context.Request.Params["realtotal"] != null)
            {
                model.RealTotal = Convert.ToDecimal(context.Request.Params["realtotal"].ToString().Trim());
            }
            if (context.Request.Params["typeID"].ToString().Trim() != null && context.Request.Params["typeID"].ToString().Trim() != "")
            {
                model.TypeID = Convert.ToInt32(context.Request.Params["typeID"].ToString().Trim());
            }
            if (ID > 0)
            {
                model.ID = ID;
            }




            string DetailID = context.Request.Params["DetailID"].ToString().Trim();
            string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();
            string DetailProductNo = context.Request.Params["DetailProductNo"].ToString().Trim();
            string DetailProductName = context.Request.Params["DetailProductName"].ToString().Trim();
            //string DetailStandard = context.Request.Params["DetailStandard"].ToString().Trim();
            string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();
            string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
            string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();
            string DetailTaxPrice = context.Request.Params["DetailTaxPrice"].ToString().Trim();
            //string DetailDiscount = context.Request.Params["DetailDiscount"].ToString().Trim();
            string DetailDiscount = "";
            string DetailTaxRate = context.Request.Params["DetailTaxRate"].ToString().Trim();
            string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();
            string DetailTotalFee = context.Request.Params["DetailTotalFee"].ToString().Trim();
            string DetailTotalTax = context.Request.Params["DetailTotalTax"].ToString().Trim();
            string DetailRequireDate = context.Request.Params["DetailRequireDate"].ToString().Trim();
            string DetailApplyReason = context.Request.Params["DetailApplyReason"].ToString().Trim();
            string DetailRemark = context.Request.Params["DetailRemark"].ToString().Trim();
            string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string DetailUsedUnitID = context.Request.Params["DetailUsedUnitID "].ToString().Trim();
            string DetailUsedUnitCount = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            string DetailUsedPrice = context.Request.Params["DetailUsedPrice "].ToString().Trim();
            string DetailExRate = context.Request.Params["DetailExRate"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();



            //string action = context.Request.Params["action"].ToString().Trim();
            JsonClass jc;

            // 明细来源信息
            string dtlSInfo = context.Request.Params["dtlSInfo"].ToString().Trim();
            string dtlInfo = context.Request.Params["dtlInfo"].ToString().Trim();
            string[] strarray = null;
            strarray = dtlSInfo.Split('|');
            string[] strarray2 = null;
            strarray2 = dtlInfo.Split('|');
            
            //model.ID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContract");
            if (PurchaseContractBus.UpdatePurchaseContract(model, DetailProductID, DetailProductNo, DetailProductName, DetailUnitID, DetailProductCount, DetailUnitPrice, DetailTaxPrice, DetailDiscount, DetailTaxRate, DetailTotalPrice, DetailTotalFee, DetailTotalTax, DetailRequireDate, DetailApplyReason, DetailRemark, DetailFromBillID, DetailFromBillNo, DetailFromLineNo, DetailUsedUnitID, DetailUsedUnitCount, DetailUsedPrice, DetailExRate, length, fflag2, no, ht))
                //jc = new JsonClass("保存成功", model.ContractNo, model.ID);
                jc = new JsonClass("保存成功", CodeType, model.ID);
            else
                jc = new JsonClass("保存失败", "", 0);
            context.Response.Write(jc);
            context.Response.End();
        }
        else if (action == "Confirm")
        {
            JsonClass jc;
            PurchaseContractModel Model = new PurchaseContractModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            Model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());
            Model.ConfirmDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());
            Model.FromType = context.Request.Params["fromType"].ToString();

            string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
        
            
            string length = context.Request.Params["length"].ToString().Trim();
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                Model.Attachment = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            }

            //string arriveNo = context.Request.Params["arriveNo"];
            //int confirmor = Convert.ToInt32(context.Request.Params["confirmor"]);
            if (PurchaseContractBus.ConfirmPurchaseContract(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg))
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
            JsonClass jc;
            PurchaseContractModel Model = new PurchaseContractModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            if (context.Request.Params["confirmor"].Trim().ToString() != "")
            {
                model.Confirmor = Convert.ToInt32(context.Request.Params["confirmor"].ToString().Trim());//确认人
            }
            if (context.Request.Params["confirmdate"] != null && context.Request.Params["confirmdate"].Trim().ToString() != "")
            {
                model.ConfirmDate = Convert.ToDateTime(context.Request.Params["confirmdate"].ToString().Trim());//确认时间
            }
            Model.FromType = context.Request.Params["fromType"].ToString();

            string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();
            string DetailFromBillNo = context.Request.Params["DetailFromBillNo"].ToString().Trim();
            string DetailFromLineNo = context.Request.Params["DetailFromLineNo"].ToString().Trim();
            string length = context.Request.Params["length"].ToString().Trim();
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                model.Attachment = context.Request.Params["DetailUsedUnitCount"].ToString().Trim();
            }

            //string arriveNo = context.Request.Params["arriveNo"];
            //int confirmor = Convert.ToInt32(context.Request.Params["confirmor"]);
            if (PurchaseContractBus.CancelConfirmPurchaseContract(Model, DetailProductCount, DetailFromBillNo, DetailFromLineNo, length, out strMsg))
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
            JsonClass jc;
            PurchaseContractModel Model = new PurchaseContractModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            Model.Closer = int.Parse(context.Request.Params["closer"].ToString().Trim());
            Model.CloseDate = DateTime.Parse(System.DateTime.Today.ToShortDateString());
            if (PurchaseContractBus.ClosePurchaseContract(Model))
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
            JsonClass jc;
            PurchaseContractModel Model = new PurchaseContractModel();
            Model.CompanyCD = CompanyCD;
            Model.ID = ID;
            int closer = Convert.ToInt32(context.Request.Params["closer"]);
            if (true == PurchaseContractBus.CancelClosePurchaseContract(Model))
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
    public bool IsReusable {
        get {
            return false;
        }
    }

}