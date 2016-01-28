<%@ WebHandler Language="C#" Class="PurchaseApply" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;
using System.Collections.Generic;
using XBase.Business.Common;

public class PurchaseApply : IHttpHandler, IRequiresSessionState
{
    protected HttpContext _context = null;
    public void ProcessRequest(HttpContext context)
    {
        _context = context;
        if (context.Request.RequestType == "POST")
        {
            PurchaseApplyModel PurchaseApplyM = GetPurchaseApplyModel(context.Request);
            JsonClass jc;
            string ActionApply = context.Request.Form["ActionApply"];
            if (ActionApply == "Add")
            {
                bool isExsist = PrimekeyVerifyBus.CheckCodeUniq("PurchaseApply", "ApplyNo", PurchaseApplyM.ApplyNo);
                if (!isExsist || string.IsNullOrEmpty(PurchaseApplyM.ApplyNo))
                {//存在时


                    if (string.IsNullOrEmpty(PurchaseApplyM.ApplyNo))
                    {
                        jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限,请检查编号规则设置!", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                    else
                    {
                        jc = new JsonClass("success", "", 2);
                        return;
                    }
                    
                
                }
            }
            List<PurchaseApplyDetailSourceModel> PurchaseApplyDetailSourceMList= GetPurchaseApplyDetailSourceMList(context.Request);
            List<PurchaseApplyDetailModel> PurchaseApplyDetailMList = GetPurchaseApplyDetailMList(context.Request);
            Hashtable ht = GetExtAttr();
            if (ActionApply == "Add")
            {
                //获取扩展属性
                
                
                int PrimaryID;
                if (PurchaseApplyBus.InsertPurchaseApply(PurchaseApplyM, PurchaseApplyDetailSourceMList, PurchaseApplyDetailMList, out PrimaryID, ht) == true)
                {
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string EmployID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString ();
                    string EmployName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                    string ApplyNoID = PurchaseApplyM.ApplyNo.ToString() + "#" + PrimaryID.ToString()+"#"+NowTime.ToString()+"#"+UserID+"#"+EmployID+"#"+EmployName  ;
                    jc = new JsonClass("success", ApplyNoID, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionApply == "Update")
            {
                if (PurchaseApplyBus.UpdatePurchaseApply(PurchaseApplyM,PurchaseApplyDetailSourceMList,PurchaseApplyDetailMList,ht) == true)
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
            else if (ActionApply == "Fill")
            {
                string ID = context.Request.Form["ID"];
                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                DataTable PurchaseApplyPrimary = PurchaseApplyBus.GetPurchaseApply(ID);
                DataTable PurchaseApplySource = PurchaseApplyBus.GetPurchaseApplySource(ID);
                DataTable PurchaseApplyDetail = PurchaseApplyBus.GetPurchaseApplyDetail(ID);
                
                System.Text.StringBuilder PurchaseApply = new System.Text.StringBuilder();
                PurchaseApply.Append("{");
                PurchaseApply.Append("PurchaseApplyPrimary:");
                PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseApplyPrimary));
                PurchaseApply.Append(",PurchaseApplySource:");
                PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseApplySource));
                PurchaseApply.Append(",PurchaseApplyDetail:");
                PurchaseApply.Append(JsonClass.DataTable2Json(PurchaseApplyDetail));
                PurchaseApply.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(PurchaseApply.ToString());
                context.Response.End();
            }
            else if (ActionApply == "Confirm")
            {
                string ID = context.Request.Form["ID"];
                if (PurchaseApplyBus.ConfirmPurchaseApply(ID) == true)
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
            else if(ActionApply == "CancelConfirm")
            {//取消确认
                string ID = context.Request.Form["ID"];
                string No = context.Request.Form["No"];
                if (PurchaseApplyBus.IsCitePurApply(ID))
                {
                    jc = new JsonClass("success", "", 2);
                    context.Response.Write(jc);
                    return;
                }
                if (PurchaseApplyBus.CancelConfirm(ID, No))
                {//取消确认成功
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string BackValue = NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
            }
            else if (ActionApply == "Complete")
            {//结单
                string ID = context.Request.Form["ID"];
                if (PurchaseApplyBus.CompletePurchaseApply(ID) == true)
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
            else if (ActionApply == "ConcelComplete")
            {
                string ID = context.Request.Form["ID"];
                if (PurchaseApplyBus.ConcelCompletePurchaseApply(ID) == true)
                {
                    string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string BackValue = NowTime + "#" + UserID;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                }
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
    private PurchaseApplyModel GetPurchaseApplyModel(HttpRequest request)
    {
        string ActionApply = request.Form["ActionApply"];
        PurchaseApplyModel PurchaseApplyM = new PurchaseApplyModel();
        if (ActionApply == "Add")
        {
            string CodeRuleID = request.Form["CodeRuleID"];
            if (CodeRuleID == "")
            {
                PurchaseApplyM.ApplyNo = request.Form["PurApplyNo"];
            }
            else
            {

                PurchaseApplyM.ApplyNo = ItemCodingRuleBus.GetCodeValue(CodeRuleID, "PurchaseApply", "ApplyNo");// 编号
            }
        }
        else if (ActionApply == "Update")
        {
            PurchaseApplyM.ApplyNo = request.Form["PurApplyNo"];
            PurchaseApplyM.id = request.Form["ID"];
        }
        PurchaseApplyM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        PurchaseApplyM.Title = request.Form["Title"];
        PurchaseApplyM.TypeID = request.Form["PurchaseType"];
        PurchaseApplyM.FromType = request.Form["FromType"];
        PurchaseApplyM.ApplyUserID = request.Form["ApplyUserID"];
        PurchaseApplyM.ApplyDeptID = request.Form["DeptID"];
        PurchaseApplyM.ApplyDate = request.Form["ApplyDate"];
        
        PurchaseApplyM.Address = request.Form["Address"];
        PurchaseApplyM.CountTotal = request.Form["CountTotal"];
        PurchaseApplyM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID .ToString ();
        PurchaseApplyM.CreateDate = request.Form["CreateDate"];
        if (request.Form["BillStatusID"] == "2")
        {
            PurchaseApplyM.BillStatus = "3";
        }
        else
        {
            PurchaseApplyM.BillStatus = request.Form["BillStatusID"];
        }

        //PurchaseApplyM.Confirmor = request.Form["ConfirmorID"];
        //PurchaseApplyM.ConfirmDate = request.Form["ConfirmorDate"];
        PurchaseApplyM.ModifiedUserID = request.Form["ModifiedUserID"];
        PurchaseApplyM.ModifiedDate = request.Form["ModifiedDate"];
        //PurchaseApplyM.Closer = Convert.ToInt32(request.Form["CloserID"]);

        //PurchaseApplyM.CloseDate = Convert.ToDateTime(request.Form["CloseDate"]);
        PurchaseApplyM.Remark = request.Form["Remark"];

        return PurchaseApplyM;
    }

    private List<PurchaseApplyDetailSourceModel> GetPurchaseApplyDetailSourceMList(HttpRequest request)
    {
        int DetailSLength = Convert.ToInt32(request.Form["DetailSLength"]);
        List<PurchaseApplyDetailSourceModel> PurchaseApplyDetailSourceMList = new List<PurchaseApplyDetailSourceModel>();
        for (int i = 0; i < DetailSLength; ++i)
        {
            PurchaseApplyDetailSourceModel PurchaseApplyDetailSourceM = new PurchaseApplyDetailSourceModel();
            PurchaseApplyDetailSourceM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            PurchaseApplyDetailSourceM.SortNo = request.Form["DtlSSortNo"+i];
            PurchaseApplyDetailSourceM.ProductID = request.Form["DtlSProdID" + i];
            PurchaseApplyDetailSourceM.ProductNo = request.Form["DtlSProdNo" + i];
            PurchaseApplyDetailSourceM.ProductName = request.Form["DtlSProdName" + i];

            PurchaseApplyDetailSourceM.UnitID = request.Form["DtlSUnitID" + i];
            if (request.Form["DtlSProductCount" + i]!="")
            PurchaseApplyDetailSourceM.ProductCount = request.Form["DtlSProductCount" + i];
            PurchaseApplyDetailSourceM.PlanCount = request.Form["DtlSPlanCount" + i];
            if (request.Form["DtlSRequireDate" + i] !="")
            PurchaseApplyDetailSourceM.RequireDate = request.Form["DtlSRequireDate" + i];
            PurchaseApplyDetailSourceM.PlanTakeDate = request.Form["DtlSPlanTakeDate" + i];
            PurchaseApplyDetailSourceM.ApplyReason = request.Form["DtlSApplyReason"+i];
            if (request.Form["DtlSFromBillID" + i] != "")
            {
                PurchaseApplyDetailSourceM.FromBillID = request.Form["DtlSFromBillID" + i];
                PurchaseApplyDetailSourceM.FromType = request.Form["FromType"];
            }
            if (request.Form["DtlSFromLineNo" + i] != "")
            PurchaseApplyDetailSourceM.FromLineNo = request.Form["DtlSFromLineNo" + i];
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                PurchaseApplyDetailSourceM.UsedUnitCount = request.Form["UsedUnitCount" + i];
                PurchaseApplyDetailSourceM.ExRate = request.Form["ExRate" + i];
                PurchaseApplyDetailSourceM.UsedUnitID = request.Form["UsedUnitID" + i]; 
            }

            PurchaseApplyDetailSourceMList.Add(PurchaseApplyDetailSourceM);
            
        }
        return PurchaseApplyDetailSourceMList;
    }

    private List<PurchaseApplyDetailModel> GetPurchaseApplyDetailMList(HttpRequest request)
    {
        int DetailLength = Convert.ToInt32(request.Form["DetailLength"]);
        List<PurchaseApplyDetailModel> PurchaseApplyDetailMList = new List<PurchaseApplyDetailModel>();
        for (int i = 0; i < DetailLength; ++i)
        {
            PurchaseApplyDetailModel PurchaseApplyDetailM = new PurchaseApplyDetailModel();
            PurchaseApplyDetailM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            PurchaseApplyDetailM.SortNo = Convert.ToInt32(request.Form["DtlSortNo"+i]);
            PurchaseApplyDetailM.ProductID = Convert.ToInt32(request.Form["DtlProdID" + i]);
            PurchaseApplyDetailM.ProductNo = request.Form["DtlProdNo" + i];
            PurchaseApplyDetailM.ProductName = request.Form["DtlProdName" + i];
            PurchaseApplyDetailM.UnitID = Convert.ToInt32(request.Form["DtlUnitID" + i]);
            PurchaseApplyDetailM.ProductCount = Convert.ToDecimal(request.Form["DtlPlanCount" + i]);
            PurchaseApplyDetailM.RequireDate = Convert.ToDateTime(request.Form["DtlRequireDate" + i]);
            //PurchaseApplyDetailM.ApplyReason = Convert.ToInt32(request.Form["DtlApplyReason" + i]);
            //PurchaseApplyDetailM.Remark = request.Form["DtlRemark" + i];
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                PurchaseApplyDetailM.UsedUnitCount = request.Form["Used2UnitCount" + i];
                PurchaseApplyDetailM.ExRate = request.Form["Ex2Rate" + i];
                PurchaseApplyDetailM.UsedUnitID = request.Form["Used2UnitID" + i];
            }
            PurchaseApplyDetailMList.Add(PurchaseApplyDetailM);
        }
        return PurchaseApplyDetailMList;
    }
    public bool IsReusable {
        get {
            return false;
        }
    }
}