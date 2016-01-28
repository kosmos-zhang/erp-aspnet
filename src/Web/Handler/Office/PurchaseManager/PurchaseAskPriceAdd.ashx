<%@ WebHandler Language="C#" Class="PurchaseAskPriceAdd" %>

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
public class PurchaseAskPriceAdd : IHttpHandler, IRequiresSessionState
{
    protected HttpContext _context = null;
 
    public void ProcessRequest (HttpContext context) 
    {
        _context = context;
        if (context.Request.RequestType == "POST")
        {
            JsonClass jc;
            string ActionAskPrice = context.Request.Params["ActionAskPrice"];
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            Hashtable ht = GetExtAttr();
            if (ActionAskPrice == "Add")
            {
            
               
                PurchaseAskPriceModel PurchaseAskPriceM = GetPurchaseAskPriceModel(context.Request);
                string AskNo = PurchaseAskPriceM.AskNo;
                bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("PurchaseAskPrice", "AskNo", AskNo);
                if (!isExsist || string.IsNullOrEmpty(AskNo))
                {//存在时
                    if (string.IsNullOrEmpty(AskNo))
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
                List<PurchaseAskPriceDetailModel> PurchaseAskPriceDetailMList = GetPurchaseAskPriceDetailModelList(context.Request,AskNo);
                int AskPriceID;
                if (PurchaseAskPriceBus.InsertPurchaseAskPrice(PurchaseAskPriceM, PurchaseAskPriceDetailMList,out AskPriceID,ht) == true)
                {
                    string AskPriceNoID = PurchaseAskPriceM.AskNo + "#$@" + AskPriceID.ToString();
                    jc = new JsonClass("success", AskPriceNoID, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionAskPrice == "Update")
            {
               
                
                PurchaseAskPriceModel PurchaseAskPriceM = GetPurchaseAskPriceModel(context.Request);
                string AskNo = PurchaseAskPriceM.AskNo;
                List<PurchaseAskPriceDetailModel> PurchaseAskPriceDetailMList = GetPurchaseAskPriceDetailModelList(context.Request, AskNo);
                if (PurchaseAskPriceBus.UpdatePurchaseAskPrice(PurchaseAskPriceM, PurchaseAskPriceDetailMList,ht) == true)
                {
                    string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    
                    string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime+"#"+UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionAskPrice == "Fill")
            {
                string ID = context.Request.Params["ID"];
                
                DataTable PurAskPricePri = PurchaseAskPriceBus.GetPurAskPricePriByID(ID);
                DataTable PurAskPriceDetail = PurchaseAskPriceBus.GetPurAskPriceDetail(ID);
                
                System.Text.StringBuilder PurchaseAskPrice = new System.Text.StringBuilder();
                PurchaseAskPrice.Append("{");
                PurchaseAskPrice.Append("PurAskPricePri:");
                PurchaseAskPrice.Append(JsonClass.DataTable2Json(PurAskPricePri));
                PurchaseAskPrice.Append(",PurAskPriceDetail:");
                PurchaseAskPrice.Append(JsonClass.DataTable2Json(PurAskPriceDetail));
                PurchaseAskPrice.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(PurchaseAskPrice.ToString());
                context.Response.End();
            }
            else if (ActionAskPrice == "Confirm")
            {
                string ID = context.Request.Params["ID"];
                string No = context.Request.Form["No"];
                if (true == PurchaseAskPriceBus.ConfirmPurAskPrice(ID,No))
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
            else if(ActionAskPrice == "CancelConfirm")
            {
                string ID = context.Request.Form["ID"];
                if (PurchaseAskPriceBus.IsCite(ID))
                {
                    jc = new JsonClass("success", "", 2);
                    context.Response.Write(jc);
                    return;
                }
                string No = context.Request.Form["No"];
                if (PurchaseAskPriceBus.CancelConfirm(ID, No))
                {
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                    string BackValue = NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionAskPrice == "Complete")
            {
                string ID = context.Request.Params["ID"];
                if (true == PurchaseAskPriceBus.CompletePurAskPrice(ID))
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
            else if (ActionAskPrice == "ConcelComplete")
            {
                string ID = context.Request.Params["ID"];
                if (true == PurchaseAskPriceBus.ConcelCompletePurAskPrice(ID))
                {
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string BackValue = NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionAskPrice == "History")
            {//点击再次询价时填充询价历史表
                string No = context.Request.Params["No"];
                int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());
                int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());
                string OrderBy = context.Request.Params["OrderBy"];
                int totalCount = 0;
                DataTable dt = PurchaseAskPriceBus.GetPurAskPriceHistory(CompanyCD,No,pageIndex,pageCount,OrderBy,out totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count > 0)
                {
                    sb.Append(JsonClass.DataTable2Json(dt));
                }
                else
                {
                    sb.Append("\"\"");
                }
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
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
                return _context.Request [key].ToString().Trim();
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

    private PurchaseAskPriceModel GetPurchaseAskPriceModel(HttpRequest request)
    {
        string ActionAskPrice = request.Params["ActionAskPrice"];
        PurchaseAskPriceModel PurchaseAskPriceM = new PurchaseAskPriceModel();
        if (ActionAskPrice == "Add")
        {
            string CodeRuleID = request.Params["CodeRuleID"];
            if (CodeRuleID == "")
            {
                PurchaseAskPriceM.AskNo = request.Params["PurAskPriceNo"];
            }
            else
            {

                PurchaseAskPriceM.AskNo = ItemCodingRuleBus.GetCodeValue(CodeRuleID, "PurchaseAskPrice", "AskNo");// 编号
            }
        }
        else if (ActionAskPrice == "Update")
        {
            PurchaseAskPriceM.AskNo = request.Params["PurAskPriceNo"];
            PurchaseAskPriceM.ID = request.Params["ID"];
        }
     
        PurchaseAskPriceM.AskTitle = request.Params["Title"];
        PurchaseAskPriceM.FromType = request.Params["FromType"];
        PurchaseAskPriceM.ProviderID = request.Params["ProviderID"];
        PurchaseAskPriceM.AskUserID = request.Params["AskUserID"];
        PurchaseAskPriceM.TypeID = request.Params["TypeID"];

        PurchaseAskPriceM.DeptID = request.Params["DeptID"];
        PurchaseAskPriceM.AskOrder = request.Params["AskOrder"];
        PurchaseAskPriceM.AskDate = request.Params["AskDate"];
        PurchaseAskPriceM.CurrencyType = request.Params["CurrencyType"];
        PurchaseAskPriceM.Rate = request.Params["ExchangeRate"];

        PurchaseAskPriceM.isAddTax = request.Params["IsAddTax"];
        PurchaseAskPriceM.CountTotal = request.Params["CountTotal"];
        PurchaseAskPriceM.TotalPrice = request.Params["TotalPrice"];
        PurchaseAskPriceM.TotalTax = request.Params["TotalTax"];
        PurchaseAskPriceM.TotalFee = request.Params["TotalFee"];

        PurchaseAskPriceM.Discount = request.Params["Discount"];
        PurchaseAskPriceM.DiscountTotal = request.Params["DiscountTotal"];
        PurchaseAskPriceM.RealTotal = request.Params["RealTotal"];
        PurchaseAskPriceM.Creator = request.Params["CreatorID"];
        PurchaseAskPriceM.CreateDate = request.Params["CreateDate"];

        if (request.Params["BillStatusID"] == "2")
        {
            PurchaseAskPriceM.BillStatus = "3";
        }
        else
        {
            PurchaseAskPriceM.BillStatus = request.Params["BillStatusID"];
        }
        PurchaseAskPriceM.Confirmor = request.Params["ConfirmorID"];
        PurchaseAskPriceM.ConfirmDate = request.Params["ConfirmorDate"];
        PurchaseAskPriceM.ModifiedUserID = request.Params["ModifiedUserID"];
        PurchaseAskPriceM.ModifiedDate = request.Params["ModifiedDate"];

        PurchaseAskPriceM.Closer = request.Params["CloserID"];
        PurchaseAskPriceM.CloseDate = request.Params["CloseDate"];
        PurchaseAskPriceM.remark = request.Params["Remark"];
        PurchaseAskPriceM.AskAgain = request.Params["ZCXJFlag"];

        return PurchaseAskPriceM;
    }

    private List<PurchaseAskPriceDetailModel> GetPurchaseAskPriceDetailModelList(HttpRequest request,string AskNo)
    {
        List<PurchaseAskPriceDetailModel> PurchaseAskPriceDetailMList = new List<PurchaseAskPriceDetailModel>();
        int Length = Convert.ToInt32(request.Params["Length"]);
        for (int i = 0; i < Length; ++i)
        {
            PurchaseAskPriceDetailModel PurchaseAskPriceDetailM = new PurchaseAskPriceDetailModel();
            PurchaseAskPriceDetailM.AskNo = AskNo;
            PurchaseAskPriceDetailM.SortNo = request.Params["SortNo"+i];
            PurchaseAskPriceDetailM.ProductID = request.Params["ProductID"+ i];
            PurchaseAskPriceDetailM.ProductNo = request.Params["ProductNo"+ i];
            PurchaseAskPriceDetailM.ProductName = request.Params["ProductName"+ i];
            PurchaseAskPriceDetailM.ProductCount = request.Params["ProductCount" + i];

            PurchaseAskPriceDetailM.RequireDate = request.Params["RequireDate" + i];
            PurchaseAskPriceDetailM.UnitID = request.Params["UnitID" + i];
            PurchaseAskPriceDetailM.UnitPrice = request.Params["UnitPrice" + i];
            PurchaseAskPriceDetailM.TaxPrice = request.Params["TaxPrice" + i]; 
   if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            PurchaseAskPriceDetailM.ExRate = request.Params["ExRate" + i];
            PurchaseAskPriceDetailM.UsedPrice = request.Params["UsedPrice" + i];
            PurchaseAskPriceDetailM.UsedUnitCount = request.Params["UsedUnitCount" + i];
            PurchaseAskPriceDetailM.UsedUnitID = request.Params["UsedUnitID" + i]; 
             
        }
            PurchaseAskPriceDetailM.TaxRate = request.Params["TaxRate" + i];
            PurchaseAskPriceDetailM.TotalPrice = request.Params["TotalPrice" + i];
            PurchaseAskPriceDetailM.TotalFee = request.Params["TotalFee" + i];
            PurchaseAskPriceDetailM.TotalTax = request.Params["TotalTax" + i];
            PurchaseAskPriceDetailM.ApplyReason = request.Params["ApplyReason" + i];

            PurchaseAskPriceDetailM.Remark = request.Params["Remark" + i];
            PurchaseAskPriceDetailM.FromBillID = request.Params["FromBillID" + i];
            PurchaseAskPriceDetailM.FromLineNo = request.Params["FromLineNo" + i];
            if (PurchaseAskPriceDetailM.FromBillID != null)
            {
                PurchaseAskPriceDetailM.FromType = request.Params["FromType"];
            }
            else
            {
                PurchaseAskPriceDetailM.FromType = "0";
            }
            PurchaseAskPriceDetailM.AskOrder = request.Params["AskOrder"];
            
            PurchaseAskPriceDetailMList.Add(PurchaseAskPriceDetailM);
        }
        return PurchaseAskPriceDetailMList;
    }
    private List<PurchaseAskPriceHistoryModel> GetPurchaseAskPriceHistoryModelList(HttpRequest request)
    {
        List<PurchaseAskPriceHistoryModel> PurchaseAskPriceHistoryMList = new List<PurchaseAskPriceHistoryModel>();
        int Length = Convert.ToInt32(request.Params["Length"]);
        for (int i = 0; i < Length; ++i)
        {
            PurchaseAskPriceHistoryModel PurchaseAskPriceHistoryM = new PurchaseAskPriceHistoryModel();
            PurchaseAskPriceHistoryM.AskNo = request.Params["AskNo"];
            PurchaseAskPriceHistoryM.ProviderID = request.Params["ProviderID"];
            PurchaseAskPriceHistoryM.AskUserID = request.Params["AskUserID"];
            PurchaseAskPriceHistoryM.DeptID = request.Params["DeptID"];
            PurchaseAskPriceHistoryM.TotalPrice = request.Params["TotalPrice"];

            PurchaseAskPriceHistoryM.TotalTax = request.Params["TotalTax"];
            PurchaseAskPriceHistoryM.TotalFee = request.Params["TotalFee"];
            PurchaseAskPriceHistoryM.AskDate = request.Params["AskDate"];
            PurchaseAskPriceHistoryM.AskOrder = request.Params["AskOrder"];
            PurchaseAskPriceHistoryM.isAddTax = request.Params["IsAddTax"];

            PurchaseAskPriceHistoryM.CountTotal = request.Params["CountTotal"];
            //PurchaseAskPriceHistoryM.Discount = request.Params["Discount"];
            
            PurchaseAskPriceHistoryM.ProductID = request.Params["ProductID"+i];
            PurchaseAskPriceHistoryM.ProductCount = request.Params["ProductCount" + i];
            PurchaseAskPriceHistoryM.UnitID = request.Params["UnitID" + i];
            PurchaseAskPriceHistoryM.DiscountDetail = request.Params["DiscountDetail" + i];

            PurchaseAskPriceHistoryM.UnitPrice = request.Params["UnitPrice" + i];
            PurchaseAskPriceHistoryM.TaxPrice = request.Params["TaxPrice" + i];
            PurchaseAskPriceHistoryM.TaxRate = request.Params["TaxRate" + i];
            PurchaseAskPriceHistoryM.TotalPriceDetail = request.Params["TotalPriceDetail" + i];
            PurchaseAskPriceHistoryM.TotalFeeDetail = request.Params["TotalFeeDetail" + i];

            PurchaseAskPriceHistoryM.TotalTaxDetail = request.Params["TotalTaxDetail" + i];

            PurchaseAskPriceHistoryMList.Add(PurchaseAskPriceHistoryM);
        }
        return PurchaseAskPriceHistoryMList;
    }
}