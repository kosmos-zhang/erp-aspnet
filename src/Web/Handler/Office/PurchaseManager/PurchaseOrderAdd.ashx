<%@ WebHandler Language="C#" Class="PurchaseOrderAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Business.Common;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using XBase.Business.Office.FinanceManager;
public class PurchaseOrderAdd : IHttpHandler, IRequiresSessionState
{
    protected HttpContext _context = null;
    public void ProcessRequest (HttpContext context)
    {
        _context = context; 
        if (context.Request.RequestType == "POST")
        {
            string ActionOrder = context.Request.Form["ActionOrder"];
            JsonClass jc;
            Hashtable ht = GetExtAttr();
            if (ActionOrder == "Add")
            {
               
                int aaa;
                PurchaseOrderModel PurchaseOrderM = GetPurchaseOrder(context.Request);
                //校验编号唯一性
                bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("PurchaseOrder", "OrderNo", PurchaseOrderM.OrderNo);
                if (!isExsist || string.IsNullOrEmpty(PurchaseOrderM.OrderNo))
                {//存在时

                    if (string.IsNullOrEmpty(PurchaseOrderM.OrderNo))
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
                List<PurchaseOrderDetailModel> PurchaseOrderDetailMList = GetPurchaseOrderDetail(context.Request);
                string Reason = string.Empty;//保存失败的原因
                if (PurchaseOrderBus.InsertPurchaseOrder(PurchaseOrderM, PurchaseOrderDetailMList, out aaa, out Reason,ht) == true)
                {
                    int bbb = aaa;
                    string OrderNoID = PurchaseOrderM.OrderNo.ToString() + "#$@" + bbb.ToString();
                    jc = new JsonClass("success", OrderNoID, 1);
                    context.Response.Write(jc);
                }
                else
                {
                    jc = new JsonClass("success", Reason, 5);
                    context.Response.Write(jc);
                }
            }
            else if (ActionOrder == "Update")
            {
                //判断采购订单有没有被引用
                string ID = context.Request.Form["ID"];
                if (PurchaseOrderBus.IsCite(ID) == true)
                {//被引用
                    jc = new JsonClass("", "", 3);
                    context.Response.Write(jc);
                    return; 
                }   
                PurchaseOrderModel PurchaseOrderM = GetPurchaseOrder(context.Request);
                List<PurchaseOrderDetailModel> PurchaseOrderDetailMList = GetPurchaseOrderDetail(context.Request);
                List<ProductModel> ProductMStorList = null;//用于回写库存
                List<ProductModel> ProductMList = null;//用于回写合同或计划
                
                if (context.Request.Form["BillStatusID"] == "2")
                {//由执行变为变更状态时，获取物品相关信息，以回写库存分仓存量表
                    ProductMStorList = GetProductMStorList(context.Request);
                    ProductMList = GetProductMList(context.Request);
                }
                string Reason = string.Empty;//保存失败的原因
                if (PurchaseOrderBus.UpdatePurchaseOrder(PurchaseOrderM, PurchaseOrderDetailMList, ProductMStorList, ProductMList, out Reason,ht ) == true)
                {
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string UserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");

                    string BackValue = UserID + "#" + UserName + "#" + NowTime;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                    return;
                }
                else
                {
                    jc = new JsonClass("success", Reason, 5);
                    context.Response.Write(jc);
                    return;
                }
            }
            else if (ActionOrder == "Confirm")
            {
                int RowCount = Convert.ToInt32(context.Request.Form["RowCount"]);
                string ID = context.Request.Form["ID"];
                string TotalFee = context.Request.Form["TotalFee"];
                string FromType = context.Request.Form["FromType"];
                string OrderNo = context.Request.Form["OrderNo"];
                string TableInfo = "officedba.PurchaseOrder," + ID;
                string ProviderID = context.Request.Form["ProviderID"];
                
                string CurrencyTypeID = context.Request.Form["CurrencyTypeID"];
                string ExchangeRate = context.Request.Form["ExchangeRate"];
                string Currency = CurrencyTypeID + "," + ExchangeRate;
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string Reason;
                List<ProductModel> ProductMList = GetProductMList(context.Request);//更新合同，或计划                
                List<ProductModel> ProductMStorList = GetProductMStorList(context.Request);//更新库存
                
                bool sign1=PurchaseOrderBus.ConfirmPuechaseOrder(CompanyCD, ID, FromType, OrderNo, out Reason, ProductMList, ProductMStorList);
                  string rev = "";
                  string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                  string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                  string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                  string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                  string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime + "#" + UserID;
                  bool res = AutoVoucherBus.AutoVoucherInsert(1, Convert.ToDecimal(TotalFee), TableInfo, Currency, int.Parse(ProviderID), out rev);//凭证确认
                    if (sign1 && res)
                {
                
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
                else
                {
                    string ss = "";
                    if (sign1)
                    {
                        ss = "确认成功!";
                        jc = new JsonClass("success", BackValue, 3);
                    }
                    else
                    {
                        ss = "确认失败!";
                        jc = new JsonClass("success", ss + rev, 2);
                    }
                
                    context.Response.Write(jc);
                }
            }
            else if (ActionOrder == "ConcelConfirm")
            {//取消确认
                int RowCount = Convert.ToInt32(context.Request.Form["RowCount"]);
                string ID = context.Request.Form["ID"];
                if (PurchaseOrderBus.IsCite(ID))
                {
                    jc = new JsonClass("success", "", 2);
                    context.Response.Write(jc);
                    return;
                }
                string FromType = context.Request.Form["FromType"];
                string OrderNo = context.Request.Form["OrderNo"];
                string TableInfo = "officedba.PurchaseOrder," + ID;
                List<ProductModel> ProductMList = GetProductMList(context.Request);//更新合同，或计划                
                List<ProductModel> ProductMStorList = GetProductMStorList(context.Request);//更新库存
                string Reason = string.Empty;
                bool sign1=PurchaseOrderBus.ConcelConfirm(ID, FromType, OrderNo, ProductMList, ProductMStorList, out Reason);
                     string rev = "";
                    bool res = AutoVoucherBus.AntiConfirmVoucher(TableInfo,out rev);//凭证反确认

                    if (sign1 && res)
                {
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                    string BackValue = NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                    return;
                }
                else
                {
                    jc = new JsonClass("success", Reason, 2);
                    context.Response.Write(jc);
                    return;
                }
            }
            else if (ActionOrder == "Complete")
            {
                string ID = context.Request.Form["ID"];
                if (PurchaseOrderBus.CompletePurchaseOrder(ID) == true)
                {
                    string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                    string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionOrder == "ConcelComplete")
            {
                string ID = context.Request.Form["ID"];
                if (PurchaseOrderBus.ConcelCompletePurchaseOrder(ID) == true)
                {
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");

                    string BackValue = UserID + "#" + EmployeeName + "#" + NowTime;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionOrder == "Fill")
            {
                string ID = context.Request.Form["ID"];

                DataTable PurchaseOrderPrimary = PurchaseOrderBus.GetPurchaseOrderByID(ID);
                DataTable PurchaseOrderDetail = PurchaseOrderBus.GetPurchaseOrderDetail(ID);

                System.Text.StringBuilder PurchaseApply = new System.Text.StringBuilder();
                PurchaseApply.Append("{");
                PurchaseApply.Append("PurchaseOrderPrimary:");
                if (PurchaseOrderPrimary.Rows.Count > 0)
                {
                    PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseOrderPrimary));
                }
                else
                {
                    PurchaseApply.Append("[]");
                }
                
                //PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseOrderPrimary));
                PurchaseApply.Append(",PurchaseOrderDetail:");
                if (PurchaseOrderDetail.Rows.Count > 0)
                {
                    PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseOrderDetail));
                }
                else
                {
                    PurchaseApply.Append("[]");
                }
                //PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseOrderDetail));
                PurchaseApply.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(PurchaseApply.ToString());
                context.Response.End();

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
    
    //获取主表
    private PurchaseOrderModel GetPurchaseOrder(HttpRequest request)
    {
        string ActionOrder = request.Form["ActionOrder"];
        PurchaseOrderModel PurchaseOrderM = new PurchaseOrderModel();
        if (ActionOrder == "Add")
        {
            string CodeRuleID = request.Form["CodeRuleID"];
            if (CodeRuleID == "")
            {
                PurchaseOrderM.OrderNo = request.Form["PurOrderNo"];
            }
            else
            {

                PurchaseOrderM.OrderNo = ItemCodingRuleBus.GetCodeValue(CodeRuleID, "PurchaseOrder", "OrderNo");// 编号
            }
        }
        else if (ActionOrder == "Update")
        {
            PurchaseOrderM.OrderNo = request.Form["PurOrderNo"];
            PurchaseOrderM.ID = request.Form["ID"];
        }
        PurchaseOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        PurchaseOrderM.ProjectID  = request.Form["ProjectID"];
        PurchaseOrderM.Title = request.Form["Title"];
        PurchaseOrderM.FromType = request.Form["FromType"];
        PurchaseOrderM.CurrencyType = request.Form["CurrencyTypeID"];
        PurchaseOrderM.Rate = request.Form["ExchangeRate"];
        //PurchaseOrderM.PurchaseDate = request.Form["PurchaseDate"];
        PurchaseOrderM.OurDelegate = request.Form["OurDelegate"];
        PurchaseOrderM.TheyDelegate = request.Form["TheyDelegate"];
        PurchaseOrderM.OrderDate = request.Form["OrderDate"];
        PurchaseOrderM.ProviderID = request.Form["ProviderID"];
        PurchaseOrderM.DeptID = request.Form["DeptID"];
        PurchaseOrderM.PayType = request.Form["PayType"];
        PurchaseOrderM.MoneyType = request.Form["MoneyType"];
        PurchaseOrderM.Purchaser = request.Form["PurchaserID"];
        PurchaseOrderM.TypeID = request.Form["PurchaseType"];
        PurchaseOrderM.TakeType = request.Form["TakeType"];
        PurchaseOrderM.CarryType = request.Form["CarryType"];
        PurchaseOrderM.ProviderBillID = request.Form["ProviderBillID"];
        PurchaseOrderM.Remark = request.Form["Remark"];
        PurchaseOrderM.TotalPrice = request.Form["TotalPrice"];
        PurchaseOrderM.TotalTax = request.Form["TotalTax"];
        PurchaseOrderM.TotalFee = request.Form["TotalFee"];
        PurchaseOrderM.Discount = request.Form["Discount"];
        PurchaseOrderM.DiscountTotal = request.Form["DiscountTotal"];
        PurchaseOrderM.RealTotal = request.Form["RealTotal"];
        PurchaseOrderM.isAddTax = request.Form["IsAddTax"];
        PurchaseOrderM.OtherTotal = request.Form["OtherTotal"];
        PurchaseOrderM.CountTotal = request.Form["CountTotal"];
        PurchaseOrderM.isOpenbill = "0";
        PurchaseOrderM.Confirmor = request.Form["ConfirmorID"];
        PurchaseOrderM.ConfirmDate = request.Form["ConfirmorDate"];
        PurchaseOrderM.Closer = request.Form["CloserID"];
        PurchaseOrderM.CloseDate = request.Form["CloseDate"];
        PurchaseOrderM.BillStatus = request.Form["BillStatusID"];
        PurchaseOrderM.Creator = request.Form["CreatorID"];
        PurchaseOrderM.CreateDate = request.Form["CreateDate"];
        PurchaseOrderM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        PurchaseOrderM.ModifiedDate = DateTime.Now.ToShortDateString();
        PurchaseOrderM.Attachment = request.Form["Attachment"];
        PurchaseOrderM.CanUserID = request.Form["CanUserID"];
        PurchaseOrderM.CanUserName = request.Form["CanUserName"];
        return PurchaseOrderM;
    }

    private List<PurchaseOrderDetailModel> GetPurchaseOrderDetail(HttpRequest request)
    {
        int DetailLength = Convert.ToInt32(request.Form["DetailLength"]);
        List<PurchaseOrderDetailModel> PurchaseOrderDetailMList = new List<PurchaseOrderDetailModel>();
        for (int i = 0; i < DetailLength; ++i)
        {
            PurchaseOrderDetailModel PurchaseOrderDetailM = new PurchaseOrderDetailModel();
            PurchaseOrderDetailM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if(request.Form["SortNo" + i] != "")
            {
                PurchaseOrderDetailM.SortNo =request.Form["SortNo" + i];
            }
            if (request.Form["FromBillID" + i] != "")
            {
                PurchaseOrderDetailM.FromBillID = request.Form["FromBillID" + i];
                PurchaseOrderDetailM.FromType = request.Form["FromType"];
            }
            else
            {
                PurchaseOrderDetailM.FromType = "0";
            }
            if (request.Form["FromLineNo" + i] != "")
            {
                PurchaseOrderDetailM.FromLineNo = request.Form["FromLineNo" + i];
            }
            if (request.Form["ProductID" + i] != "")
            {
                PurchaseOrderDetailM.ProductID = request.Form["ProductID" + i];
            }
            if (request.Form["ProductNo" + i] != "")
            {
                PurchaseOrderDetailM.ProductNo = request.Form["ProductNo" + i];
            }

            if (request.Form["ProductName" + i] != "")
            {
                PurchaseOrderDetailM.ProductName = request.Form["ProductName" + i];
            }
            if (request.Form["ProductCount" + i] != "")
            {
                PurchaseOrderDetailM.ProductCount = request.Form["ProductCount" + i];
            }
            if (request.Form["UnitID" + i] != "")
            {
                PurchaseOrderDetailM.UnitID = request.Form["UnitID" + i];
            }
            if (request.Form["UnitPrice" + i] != "")
            {
                PurchaseOrderDetailM.UnitPrice = request.Form["UnitPrice" + i];
            }
            if (request.Form["TaxPrice" + i] != "")
            {
                PurchaseOrderDetailM.TaxPrice = request.Form["TaxPrice" + i];
            }
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                if (request.Form["UsedPrice" + i] != "")
                {
                    PurchaseOrderDetailM.UsedPrice = request.Form["UsedPrice" + i];
                }
                if (request.Form["UsedUnitCount" + i] != "")
                {
                    PurchaseOrderDetailM.UsedUnitCount = request.Form["UsedUnitCount" + i];
                }
                if (request.Form["ExRate" + i] != "")
                {
                    PurchaseOrderDetailM.ExRate = request.Form["ExRate" + i];
                }
                if (request.Form["UsedUnitID" + i] != "")
                {
                    PurchaseOrderDetailM.UsedUnitID = request.Form["UsedUnitID" + i];
                }
                
                 
            }

            //if (request.Form["Discount" + i] != "")
            //{
            //    PurchaseOrderDetailM.Discount = request.Form["Discount" + i];
            //}
            if (request.Form["TaxRate" + i] != "")
            {
                PurchaseOrderDetailM.TaxRate = request.Form["TaxRate" + i];
            }
            if (request.Form["TotalFee" + i] != "")
            {
                PurchaseOrderDetailM.TotalFee = request.Form["TotalFee" + i];
            }
            if (request.Form["TotalPrice" + i] != "")
            {
                PurchaseOrderDetailM.TotalPrice = request.Form["TotalPrice" + i];
            }
            if (request.Form["TotalTax" + i] != "")
            {
                PurchaseOrderDetailM.TotalTax = request.Form["TotalTax" + i];
            }

            if (request.Form["RequireDate" + i] != "")
            {
                PurchaseOrderDetailM.RequireDate = request.Form["RequireDate" + i];
            }
            if (request.Form["Remark" + i] != "")
            {
                PurchaseOrderDetailM.Remark = request.Form["Remark" + i];
            }
            PurchaseOrderDetailMList.Add(PurchaseOrderDetailM);
        }
        return PurchaseOrderDetailMList;
    }
    private List<ProductModel> GetProductMStorList(HttpRequest request)
    {
        //获取物品Model，用于回写库存
        List<ProductModel> ProductMList = new List<ProductModel>();
        int DetailLength = Convert.ToInt32(request.Form["DetailLength"]);
        for (int i = 0; i < DetailLength; ++i)
        {
            ProductModel ProductM = new ProductModel();
            ProductM.ProductID = request.Form["ProductID" + i];
            ProductM.ProductCount = request.Form["ProductCount" + i];
            ProductMList.Add(ProductM);
        }
        return ProductMList;
    }
    private List<ProductModel> GetProductMList(HttpRequest request)
    {//获取物品model用于回写采购合同,或者采购计划
        List<ProductModel> ProductMList = new List<ProductModel>();
        int DetailLength = Convert.ToInt32(request.Form["DetailLength"]);
        for (int i = 0; i < DetailLength; ++i)
        {
            if (request.Form["FromBillID" + i] != "" && request.Form["FromBillID" + i] != "0")
            {
                ProductModel ProductM = new ProductModel();
                ProductM.FromBillID = request.Form["FromBillID" + i];
                ProductM.FromBillNo = request.Form["FromBillNo"+i];
                ProductM.FromLineNo = request.Form["FromLineNo" + i];
                ProductM.ProductCount = request.Form["ProductCount" + i];
                if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                {
                    ProductM.UsedUnitCount = request.Form["UsedUnitCount" + i];
                }
                
                ProductMList.Add(ProductM);
            }
        }
        return ProductMList;    
    }
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }
    public class DataSourceModel
    {
        public string ID { get; set; }
        public string OrderNo { get; set; }
        public string Title { get; set; }
        public string TypeID { get; set; }
        public string FromType { get; set; }
        public string CurrencyType { get; set; }
        public string Rate { get; set; }
        //public string PurchaseDate { get; set; }
        public string OrderDate { get; set; }
        public string TheyDelegate { get; set; }
        public string OurDelegate { get; set; }
        public string OurDelegateName { get; set; }
        public string ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string PayType { get; set; }
        public string MoneyType { get; set; }
        public string Purchaser { get; set; }
        public string EmployeeName { get; set; }
        public string TakeType { get; set; }
        public string CarryType { get; set; }
        public string ProviderBillID { get; set; }
        public string remark { get; set; }
        public string TotalPrice { get; set; }
        public string TotalTax { get; set; }
        public string TotalFee { get; set; }
        public string Discount { get; set; }
        public string DiscountTotal { get; set; }
        public string OtherTotal { get; set; }
        public string RealTotal { get; set; }
        public string isAddTax { get; set; }
        public string CountTotal { get; set; }
        public string Confirmor { get; set; }
        public string ConfirmorName { get; set; }
        public string ConfirmDate { get; set; }
        public string Closer { get; set; }
        public string CloserName { get; set; }
        public string CloseDate { get; set; }
        public string BillStatus { get; set; }
        public string Creator { get; set; }
        public string CreatorName { get; set; }
        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }

    } 
}