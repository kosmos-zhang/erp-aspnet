<%@ WebHandler Language="C#" Class="SellContractAdd" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;
using System.Collections;
 
public class SellContractAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;
        string strMsg = string.Empty;//操作返回的信息  
        int orderID = 0;
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//报价单编号产生的规则                                    
        string ContractNo = context.Request.Params["ContractNo"].ToString().Trim();	//报价单编号 
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellContract", "ContractNo");
            }
            else
            {
                strCode = ContractNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellContract", "ContractNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "销售合同单据编号已经存在";
                }
            }
            else
            {
                SellContractModel sellContractModel = GetSellContractModel(strCode, context);
                List<SellContractDetailModel> sellContractDetailModelList = GetOrderDetailModelList(strCode, context);
                isSucc = SellContractBus.InsertOrder(ht, sellContractModel, sellContractDetailModelList, out strMsg);
            }

        }
        else if (action == "update")//保存后修改
        {
            strCode = ContractNo;
            SellContractModel sellContractModel = GetSellContractModel(strCode, context);
            List<SellContractDetailModel> sellContractDetailModelList = GetOrderDetailModelList(strCode, context);

            isSucc = SellContractBus.UpdateOrder(ht,sellContractModel, sellContractDetailModelList, out strMsg);
        }
        else if (action == "end")//终止合同
        {
            strCode = ContractNo;
            isSucc = SellContractBus.EndOrder(strCode, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = ContractNo;
            isSucc = SellContractBus.ConfirmOrder(strCode, out strMsg);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = ContractNo;
            isSucc = SellContractBus.UnConfirmOrder(strCode, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = ContractNo;
            isSucc = SellContractBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = ContractNo;
            isSucc = SellContractBus.UnCloseOrder(strCode, out strMsg);
        }
        JsonClass jc;
        if (isSucc)
        {
            orderID = SellContractBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, "", strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, "", strCode, strMsg, 0);
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
    /// 
    /// </summary>
    /// <param name="strCode">合同编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellContractModel GetSellContractModel(string strCode, HttpContext context)
    {
        SellContractModel sellContractModel = new SellContractModel();
        string ContractNo = strCode;
        string FromType = context.Request.Params["FromType"].ToString().Trim();
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();
        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string CustTel = context.Request.Params["CustTel"].ToString().Trim();
        string Title = context.Request.Params["Title"].ToString().Trim();
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();
        string Rate = context.Request.Params["Rate"].ToString().Trim();
        string TotalPrice = context.Request.Params["TotalPrice"].ToString().Trim();
        string Tax = context.Request.Params["Tax"].ToString().Trim();
        string TotalFee = context.Request.Params["TotalFee"].ToString().Trim();
        string Discount = context.Request.Params["Discount"].ToString().Trim();
        string DiscountTotal = context.Request.Params["DiscountTotal"].ToString().Trim();
        string RealTotal = context.Request.Params["RealTotal"].ToString().Trim();
        string CountTotal = context.Request.Params["CountTotal"].ToString().Trim();
        string PayType = context.Request.Params["PayType"].ToString().Trim();
        string MoneyType = context.Request.Params["MoneyType"].ToString().Trim();
        string TakeType = context.Request.Params["TakeType"].ToString().Trim();
        string CarryType = context.Request.Params["CarryType"].ToString().Trim();
        string SellType = context.Request.Params["SellType"].ToString().Trim();
        string Seller = context.Request.Params["Seller"].ToString().Trim();
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim();
        string SignDate = context.Request.Params["SignDate"].ToString().Trim();
        string StartDate = context.Request.Params["StartDate"].ToString().Trim();
        string EndDate = context.Request.Params["EndDate"].ToString().Trim();
        string SignAddr = context.Request.Params["SignAddr"].ToString().Trim();
        string TheyDelegate = context.Request.Params["TheyDelegate"].ToString().Trim();
        string OurDelegate = context.Request.Params["OurDelegate"].ToString().Trim();
        string State = context.Request.Params["State"].ToString().Trim();
        string EndNote = context.Request.Params["EndNote"].ToString().Trim();
        string TalkProcess = context.Request.Params["TalkProcess"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string Attachment = context.Request.Params["Attachment"].ToString().Trim();
        string isAddTax = context.Request.Params["isAddTax"].ToString().Trim();
        string strCanViewUser = context.Request.Params["CanViewUser"].ToString().Trim();//可查看人员
        string CanViewUser = strCanViewUser.Length == 0 ? null : strCanViewUser;
        sellContractModel.CanViewUser = CanViewUser;

        sellContractModel.BillStatus = "1"; //单据状态
        sellContractModel.ContractNo = ContractNo;
        sellContractModel.FromType = FromType;
        try
        {
            sellContractModel.FromBillID = Convert.ToInt32(FromBillID);
        }
        catch { }
        try
        {
            sellContractModel.CustID = Convert.ToInt32(CustID);
        }
        catch { }
        sellContractModel.CustTel = CustTel;
        sellContractModel.Title = Title;
        sellContractModel.BusiType = BusiType;
        try
        {
            sellContractModel.CurrencyType = Convert.ToInt32(CurrencyType);
        }
        catch { }
        try
        {
            sellContractModel.Rate = Convert.ToDecimal(Rate);
        }
        catch { sellContractModel.Rate = 0; }
        try
        {
            sellContractModel.TotalPrice = Convert.ToDecimal(TotalPrice);
        }
        catch { sellContractModel.TotalPrice = 0; }
        try
        {
            sellContractModel.Tax = Convert.ToDecimal(Tax);
        }
        catch { }
        try
        {
            sellContractModel.TotalFee = Convert.ToDecimal(TotalFee);
        }
        catch { sellContractModel.TotalFee = 0; }
        try
        {
            sellContractModel.Discount = Convert.ToDecimal(Discount);
        }
        catch { sellContractModel.Discount = 0; }
        try
        {
            sellContractModel.DiscountTotal = Convert.ToDecimal(DiscountTotal);
        }
        catch { sellContractModel.DiscountTotal = 0; }
        try
        {
            sellContractModel.RealTotal = Convert.ToDecimal(RealTotal);
        }
        catch { sellContractModel.RealTotal = 0; }
        try
        {
            sellContractModel.CountTotal = Convert.ToDecimal(CountTotal);
        }
        catch { sellContractModel.CountTotal = 0; }
        try
        {
            sellContractModel.PayType = Convert.ToInt32(PayType);
        }
        catch { }
        try
        {
            sellContractModel.MoneyType = Convert.ToInt32(MoneyType);
        }
        catch { }
        try
        {
            sellContractModel.TakeType = Convert.ToInt32(TakeType);
        }
        catch { }
        try
        {
            sellContractModel.CarryType = Convert.ToInt32(CarryType);
        }
        catch { }
        try
        {
            sellContractModel.SellType = Convert.ToInt32(SellType);
        }
        catch { }
        try
        {
            sellContractModel.Seller = Convert.ToInt32(Seller);
        }
        catch { }
        try
        {
            sellContractModel.SellDeptId = Convert.ToInt32(SellDeptId);
        }
        catch { }
        try
        {
            sellContractModel.SignDate = Convert.ToDateTime(SignDate);
        }
        catch { }
        try
        {
            sellContractModel.StartDate = Convert.ToDateTime(StartDate);
        }
        catch { }
        try
        {
            sellContractModel.EndDate = Convert.ToDateTime(EndDate);
        }
        catch { }
        sellContractModel.SignAddr = SignAddr;
        sellContractModel.TheyDelegate = TheyDelegate;
        try
        {
            sellContractModel.OurDelegate = Convert.ToInt32(OurDelegate);
        }
        catch { }
        sellContractModel.State = "1";
        sellContractModel.EndNote = EndNote;
        sellContractModel.TalkProcess = TalkProcess;
        sellContractModel.Remark = Remark;
        sellContractModel.Attachment = Attachment;
        sellContractModel.isAddTax = isAddTax;


        sellContractModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码
        sellContractModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID; //创建人
        sellContractModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //最后更新用后

        return sellContractModel;
    }

    /// <summary>
    /// 获取SellOfferDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellContractDetailModel> GetOrderDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        SellContractDetailModel sellContractDetailModel;
        List<SellContractDetailModel> sellContractDetailModelList = new List<SellContractDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellContractDetailModel = new SellContractDetailModel();
                sellContractDetailModel.ContractNo = strCode;
                sellContractDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());
                sellContractDetailModel.ProductID = Convert.ToInt32(inseritems[1].ToString());
                sellContractDetailModel.ProductCount = Convert.ToDecimal(inseritems[2].ToString());
                sellContractDetailModel.UnitID = Convert.ToInt32(inseritems[3].ToString());
                sellContractDetailModel.UnitPrice = Convert.ToDecimal(inseritems[4].ToString());
                sellContractDetailModel.TaxPrice = Convert.ToDecimal(inseritems[5].ToString());
                sellContractDetailModel.Discount = Convert.ToDecimal(inseritems[6].ToString());
                sellContractDetailModel.TaxRate = Convert.ToDecimal(inseritems[7].ToString());
                sellContractDetailModel.TotalFee = Convert.ToDecimal(inseritems[8].ToString());
                sellContractDetailModel.TotalPrice = Convert.ToDecimal(inseritems[9].ToString());
                sellContractDetailModel.TotalTax = Convert.ToDecimal(inseritems[10].ToString());
                sellContractDetailModel.SendTime = Convert.ToInt32(inseritems[11].ToString());
                sellContractDetailModel.Remark = inseritems[13].ToString();

                if (inseritems[12].ToString().Trim().Length != 0)
                {
                    sellContractDetailModel.Package = Convert.ToInt32(inseritems[12].ToString());
                }

                sellContractDetailModel.UsedUnitID = Convert.ToInt32(inseritems[14].ToString());//单位
                sellContractDetailModel.UsedUnitCount = Convert.ToDecimal(inseritems[15].ToString());//数量
                sellContractDetailModel.UsedPrice = Convert.ToDecimal(inseritems[16].ToString());//单价
                sellContractDetailModel.ExRate = Convert.ToDecimal(inseritems[17].ToString());//换算率

                sellContractDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               

                sellContractDetailModelList.Add(sellContractDetailModel);
            }
        }

        return sellContractDetailModelList;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}