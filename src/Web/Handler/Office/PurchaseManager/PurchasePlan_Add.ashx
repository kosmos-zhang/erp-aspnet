<%@ WebHandler Language="C#" Class="PurchasePlan_Add" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Common;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Collections;

public class PurchasePlan_Add : IHttpHandler, IRequiresSessionState
{
    protected HttpContext _context = null;
    public void ProcessRequest (HttpContext context)
    {
        _context = context;   
        JsonClass jc;
        string ActionPlan =context.Request.Form["ActionPlan"].ToString().Trim();
        if (ActionPlan == "Add" || ActionPlan == "Update")
        {
            PurchasePlanModel PurchasePlanM = GetPurchasePlan(context.Request);
            if (ActionPlan == "Add")
            {
                bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("PurchasePlan", "PlanNo", PurchasePlanM.PlanNo);
                if (!isExsist || string.IsNullOrEmpty(PurchasePlanM.PlanNo))
                {//存在时
                    if (string.IsNullOrEmpty(PurchasePlanM.PlanNo))
                    {
                        jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限,请检查编号规则设置!", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else
                    {
                        jc = new JsonClass("success", "", 2);
                        context.Response.Write(jc);
                        return;
                    }
                }
            }
            List<PurchasePlanSourceModel> PurchasePlanSourceMList = GetPurchasePlanSource(context.Request, PurchasePlanM.PlanNo);
            List<PurchasePlanDetailModel> PurchasePlanDetailMList = GetPurchasePlanDetail(context.Request, PurchasePlanM.PlanNo);
            Hashtable ht = GetExtAttr();
            if (ActionPlan == "Add")
            {
               
                int aaa;
                if (PurchasePlanBus.InsertPurchasePlanAll(PurchasePlanM, PurchasePlanSourceMList, PurchasePlanDetailMList,out aaa,ht) == true)
                {
                    string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string BackValue = PurchasePlanM.PlanNo + "#$@" + aaa.ToString() + "#$@" + EmployeeID + "#$@" + EmployeeName + "#$@" + NowTime + "#$@" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
                else
                {
                    jc = new JsonClass("faile", "", 0);
                    context.Response.Write(jc);
                }
            }
            else if (ActionPlan == "Update")
            {
                List<ProductModel> ProductMList = null;
                if(context.Request.Form["BillStatusID"] == "2")
                {
                    ProductMList = GetProductMList(context.Request);
                }
                if (PurchasePlanBus.UpdatePurchasePlanAll(PurchasePlanM, PurchasePlanSourceMList, PurchasePlanDetailMList,ht) == true)
                {
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");

                    string BackValue = UserID + "#$@" + NowTime;
                    jc = new JsonClass("success",BackValue, 3);
                    context.Response.Write(jc);
                }
                else
                {
                    jc = new JsonClass("faile", "", 0);
                    context.Response.Write(jc);
                }
                    
            }
        }
        //if (ActionPlan == "Delete")
        //{
        //    string PlanNo = context.Request.Form["PlanNo"];
        //    if (PurchasePlanBus.DeletePurchasePlanAll(PlanNo) == true)
        //    {
        //        jc = new JsonClass("success", "", 1);
        //        context.Response.Write(jc);
        //    }
        //    else
        //    {
        //        jc = new JsonClass("faile", "", 0);
        //        context.Response.Write(jc);
        //    }
        //}
        else if (ActionPlan == "Confirm")
        {
            //string PlanNo = context.Request.Form["PlanNo"];
            //int Confirmor = Convert.ToInt32(context.Request.Form["Confirmor"]);
            string dds = context.Request.Form.ToString();
            string ID = context.Request.Form["ID"];
            string No = context.Request.Form["No"];
            string FromType = context.Request.Form["FromType"];
            List<ProductModel> ProductMList = null;
            if (FromType != "0")
            {
                ProductMList = GetProductMList(context.Request);
            }

            string Reason = string.Empty;
            if (true == PurchasePlanBus.ConfirmPurchasePlan(ID,No,ProductMList,out Reason))
            {
                string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime + "#" + UserID;
                jc = new JsonClass("success", BackValue, 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("success", Reason, 2);
                context.Response.Write(jc);
            }
        }
        else if (ActionPlan == "CancelConfirm")
        {
            string ID = context.Request.Form["ID"];
            if (PurchasePlanBus.IsCitePurPlan(ID))
            {
                jc = new JsonClass("success", "", 2);
                context.Response.Write(jc);
                return;
            }
            string PlanNo = context.Request.Form["PlanNo"];
            string FromType = context.Request.Form["FromType"];
            List<ProductModel> ProductMList = ProductMList = GetProductMList(context.Request);
            
            if (true == PurchasePlanBus.CancelConfirm(ID,PlanNo,ProductMList))
            {
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                string BackValue = NowTime + "#" + UserID;
                jc = new JsonClass("success", BackValue, 1);
                context.Response.Write(jc);
            }
        }
        else if (ActionPlan == "Complete")
        {
            string ID = context.Request.Form["ID"];
            int Closer = Convert.ToInt32(context.Request.Form["Closer"]);
            if (PurchasePlanBus.ClosePurchasePlan(ID) == true)
            {
                string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime + "#" + UserID;
                jc = new JsonClass("success", BackValue, 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
                context.Response.Write(jc);
            }
        }
        else if (ActionPlan == "ConcelComplete")
        {
            string ID = context.Request.Form["ID"];
            if (true == PurchasePlanBus.CancelClosePurchasePlan(ID))
            {
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string BackValue = NowTime + "#" + UserID;
                jc = new JsonClass("success", BackValue, 1);
                context.Response.Write(jc);
            }
            else
            {
                jc = new JsonClass("success", "", 2);
                context.Response.Write(jc);
            }
        }
        else if (ActionPlan == "GetPrv")
        {
            string ID = context.Request.Form["ID"];
            string ProviderIDName = PurchasePlanBus.GetRcmPrv(ID);
            jc = new JsonClass("success", ProviderIDName, 1);
            context.Response.Write(jc);
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
    
    
    //获取需要回写的model
    private List<ProductModel> GetProductMList(HttpRequest request)
    {
        List<ProductModel> ProductMList = new List<ProductModel>(); 
        int ProductLength = Convert.ToInt32(request.Form["ProductLength"]);
        for (int i = 0; i < ProductLength; ++i)
        {
            if(request.Form["FromBillID"+i] != "")
            {
                ProductModel ProductM = new ProductModel();
                ProductM.FromBillID = request.Form["FromBillID"+i];
                ProductM.FromBillNo = request.Form["FromBillNo"+i];
                ProductM.FromLineNo = request.Form["FromLineNo" + i];
                ProductM.ProductCount = request.Form["PlanCount" + i];
                ProductM.FromType = request.Form["FromType"];
                if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                {
                    ProductM.UsedUnitCount = request.Form["UsedUnitCount"+i];
                }
                ProductMList.Add(ProductM);
            }
        }
        return ProductMList;
    }
 
    //获取主表
    private PurchasePlanModel GetPurchasePlan(HttpRequest request)
    {
        string ActionPlan = request.Form["ActionPlan"];
        PurchasePlanModel PurchasePlanM = new PurchasePlanModel();
        if (ActionPlan == "Add")
        {
            string CodeRuleID = request.Form["CodeRuleID"];
            if (CodeRuleID == null)
            {
                PurchasePlanM.PlanNo = request.Form["PurPlanNo"];
            }
            else
            {

                PurchasePlanM.PlanNo = ItemCodingRuleBus.GetCodeValue(CodeRuleID, "PurchasePlan", "PlanNo");// 编号
            }
        }
        else if (ActionPlan == "Update")
        {
            PurchasePlanM.PlanNo = request.Form["PurPlanNo"];
        }
        PurchasePlanM.ID = request.Form["ID"];
        PurchasePlanM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchasePlanM.Title = request.Form["Title"];
        PurchasePlanM.TypeID = request.Form["PurchaseType"];
        PurchasePlanM.PlanUserID = request.Form["PlanUserID"];
        PurchasePlanM.Purchaser = request.Form["PurchaserID"];
        PurchasePlanM.PlanDeptID = request.Form["PlanDeptID"];
        PurchasePlanM.PlanDate = request.Form["PlanDate"];
        PurchasePlanM.FromType = request.Form["FromType"];
        PurchasePlanM.CountTotal = request.Form["CountTotal"];
        PurchasePlanM.PlanMoney = request.Form["PlanMoney"];
        PurchasePlanM.Creator = request.Form["CreatorID"];
        PurchasePlanM.CreateDate = request.Form["CreateDate"];
        if (request.Form["BillStatusID"] == "2")
        {
            PurchasePlanM.BillStatus = "3";
        }
        else
        {
            PurchasePlanM.BillStatus = request.Form["BillStatusID"];
        }
        PurchasePlanM.ModifiedUserID = request.Form["ModifiedUserID"];
        PurchasePlanM.ModifiedDate = request.Form["ModifiedDate"];
        return PurchasePlanM;
    }

    private List<PurchasePlanSourceModel> GetPurchasePlanSource(HttpRequest request,string PlanNo)
    {
        int DtlSLength = Convert.ToInt32(request.Form["DtlSLength"]);
        List<PurchasePlanSourceModel> PurchasePlanSourceMList = new List<PurchasePlanSourceModel>();
        
        for (int i = 0; i < DtlSLength; ++i)
        {
            PurchasePlanSourceModel PurchasePlanSourceM = new PurchasePlanSourceModel();
            PurchasePlanSourceM.PlanNo = PlanNo;
            PurchasePlanSourceM.SortNo = request.Form["DtlSSortNo" + i];
            PurchasePlanSourceM.ProductID = request.Form["DtlSProductID" + i];
            PurchasePlanSourceM.ProductNo = request.Form["DtlSProductNo" + i];
            PurchasePlanSourceM.ProductName = request.Form["DtlSProductName" + i];
            PurchasePlanSourceM.UnitID = request.Form["DtlSUnitID" + i];
            PurchasePlanSourceM.UnitPrice = request.Form["DtlSUnitPrice" + i];
            PurchasePlanSourceM.TotalPrice = request.Form["DtlSTotalPrice" + i];
            PurchasePlanSourceM.RequireCount = request.Form["DtlSRequireCount" + i];
            PurchasePlanSourceM.RequireDate = request.Form["DtlSRequireDate" + i];
            PurchasePlanSourceM.ProviderID = request.Form["DtlSProviderID" + i];
            PurchasePlanSourceM.ApplyReason = request.Form["DtlSApplyReasonID" + i];
            PurchasePlanSourceM.PlanCount = request.Form["DtlSPlanCount" + i];
            PurchasePlanSourceM.PlanTakeDate = request.Form["DtlSPlanTakeDate" + i];
            PurchasePlanSourceM.FromBillID = request.Form["DtlSFromBillID" + i];            
            PurchasePlanSourceM.FromSortNo = request.Form["DtlSFromLineNo" + i];
            if (PurchasePlanSourceM.FromBillID != "")
            {
                PurchasePlanSourceM.FromType = request.Form["FromType"];    
            }

            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                PurchasePlanSourceM.UsedUnitID = request.Form["UsedUnitID" + i];
                PurchasePlanSourceM.UsedUnitCount = request.Form["UsedUnitCount" + i];
                PurchasePlanSourceM.UsedPrice = request.Form["UsedPrice" + i];
                PurchasePlanSourceM.ExRate = request.Form["ExRate" + i];
            }
            
            
            
            PurchasePlanSourceMList.Add(PurchasePlanSourceM);          
            
        }
        return PurchasePlanSourceMList;
    }

    private List<PurchasePlanDetailModel> GetPurchasePlanDetail(HttpRequest request,string PlanNo)
    {
        int DtlLength = Convert.ToInt32(request.Form["DtlLength"]);
        List<PurchasePlanDetailModel> PurchasePlanDetailMList = new List<PurchasePlanDetailModel>();
        for (int i = 0; i < DtlLength; ++i)
        {
            PurchasePlanDetailModel PurchasePlanDetailM = new PurchasePlanDetailModel();
            PurchasePlanDetailM.PlanNo = PlanNo;
            PurchasePlanDetailM.SortNo = (i + 1).ToString();
            PurchasePlanDetailM.ProductID = request.Form["DtlProductID" + i];
            PurchasePlanDetailM.ProductNo = request.Form["DtlProductNo" + i];
            PurchasePlanDetailM.ProductName = request.Form["DtlProductName" + i];
            PurchasePlanDetailM.UnitID = request.Form["DtlUnitID" + i];
            PurchasePlanDetailM.UnitPrice = request.Form["DtlUnitPrice" + i];
            PurchasePlanDetailM.TotalPrice = request.Form["DtlTotalPrice" + i];
            PurchasePlanDetailM.ProductCount = request.Form["DtlProductCount" + i];
            PurchasePlanDetailM.RequireDate = request.Form["DtlRequireDate" + i];
            PurchasePlanDetailM.ProviderID = request.Form["DtlProviderID" + i];
            PurchasePlanDetailM.Remark = request.Form["DtlRemark" + i];
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                PurchasePlanDetailM.UsedUnitID = request.Form["Used2UnitID" + i];
                PurchasePlanDetailM.UsedUnitCount = request.Form["Used2UnitCount" + i];
                PurchasePlanDetailM.UsedPrice = request.Form["Used2Price" + i];
                PurchasePlanDetailM.ExRate = request.Form["Ex2Rate" + i];
            }
            PurchasePlanDetailMList.Add(PurchasePlanDetailM);
            
            
        }
        return PurchasePlanDetailMList;
    } 
    
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}