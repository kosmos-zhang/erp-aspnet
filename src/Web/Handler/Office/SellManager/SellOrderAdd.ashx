<%@ WebHandler Language="C#" Class="SellOrderAdd" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class SellOrderAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;
        string strMsg = string.Empty;//操作返回的信息  
        int orderID = 0;
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//报价单编号产生的规则                                    
        string OrderNo = context.Request.Params["OrderNo"].ToString().Trim();	//报价单编号 
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellOrder", "OrderNo");
            }
            else
            {
                strCode = OrderNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellOrder", "OrderNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "销售订单单据编号已经存在";
                }
            }
            else
            {
                SellOrderModel sellOrderModel = GetSellOrderModel(strCode, context);
                List<SellOrderDetailModel> sellOrderDetailModelList = GetSellOrderDetailModelList(strCode, context);
                List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList = GetSellOrderFeeDetailModelList(strCode, context);
                //获取扩展属性
                Hashtable ht = GetExtAttr(context);
                isSucc = SellOrderBus.Insert(ht, sellOrderModel, sellOrderDetailModelList, sellOrderFeeDetailModelList, out strMsg);
            }
        }
        else if (action == "update")//保存后修改
        {
            strCode = OrderNo;
            SellOrderModel sellOrderModel = GetSellOrderModel(strCode, context);
            List<SellOrderDetailModel> sellOrderDetailModelList = GetSellOrderDetailModelList(strCode, context);
            List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList = GetSellOrderFeeDetailModelList(strCode, context);
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            isSucc = SellOrderBus.Update(ht,sellOrderModel, sellOrderDetailModelList, sellOrderFeeDetailModelList, out strMsg);
        }
        else if (action == "end")//终止合同
        {
            strCode = OrderNo;
            isSucc = SellOrderBus.EndOrder(strCode, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = OrderNo;
            SellOrderModel sellOrderModel = GetSellOrderModel(strCode, context);
            sellOrderModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellOrderBus.ConfirmOrder(sellOrderModel, out strMsg);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = OrderNo;
            SellOrderModel sellOrderModel = GetSellOrderModel(strCode, context);
            sellOrderModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellOrderBus.UnConfirmOrder(sellOrderModel, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = OrderNo;
            isSucc = SellOrderBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = OrderNo;
            isSucc = SellOrderBus.UnCloseOrder(strCode, out strMsg);
        }
        JsonClass jc;
        if (isSucc)
        {
            orderID = SellOrderBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, "", strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, "", strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }


    /// <summary>
    /// SellOrderModelbiao shiti 
    /// </summary>
    /// <param name="strCode">合同编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellOrderModel GetSellOrderModel(string strCode, HttpContext context)
    {
        SellOrderModel sellOrderModel = new SellOrderModel();

        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string CustTel = context.Request.Params["CustTel"].ToString().Trim();
        string Title = context.Request.Params["Title"].ToString().Trim();
        string FromType = context.Request.Params["FromType"].ToString().Trim();
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();
        string Seller = context.Request.Params["Seller"].ToString().Trim();
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim();
        string SellType = context.Request.Params["SellType"].ToString().Trim();
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();
        string OrderMethod = context.Request.Params["OrderMethod"].ToString().Trim();
        string PayType = context.Request.Params["PayType"].ToString().Trim();
        string MoneyType = context.Request.Params["MoneyType"].ToString().Trim();
        string CarryType = context.Request.Params["CarryType"].ToString().Trim();
        string TakeType = context.Request.Params["TakeType"].ToString().Trim();
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();
        string Rate = context.Request.Params["Rate"].ToString().Trim();
        string TotalPrice = context.Request.Params["TotalPrice"].ToString().Trim();
        string Tax = context.Request.Params["Tax"].ToString().Trim();
        string TotalFee = context.Request.Params["TotalFee"].ToString().Trim();
        string Discount = context.Request.Params["Discount"].ToString().Trim();
        string SaleFeeTotal = context.Request.Params["SaleFeeTotal"].ToString().Trim();
        string DiscountTotal = context.Request.Params["DiscountTotal"].ToString().Trim();
        string RealTotal = context.Request.Params["RealTotal"].ToString().Trim();
        string isAddTax = context.Request.Params["isAddTax"].ToString().Trim();
        string CountTotal = context.Request.Params["CountTotal"].ToString().Trim();
        string SendDate = context.Request.Params["SendDate"].ToString().Trim();
        string OrderDate = context.Request.Params["OrderDate"].ToString().Trim();
        string StartDate = context.Request.Params["StartDate"].ToString().Trim();
        string EndDate = context.Request.Params["EndDate"].ToString().Trim();
        string TheyDelegate = context.Request.Params["TheyDelegate"].ToString().Trim();
        string OurDelegate = context.Request.Params["OurDelegate"].ToString().Trim();
        string Status = context.Request.Params["Status"].ToString().Trim();
        string PayRemark = context.Request.Params["PayRemark"].ToString().Trim();
        string DeliverRemark = context.Request.Params["DeliverRemark"].ToString().Trim();
        string PackTransit = context.Request.Params["PackTransit"].ToString().Trim();
        string StatusNote = context.Request.Params["StatusNote"].ToString().Trim();
        string CustOrderNo = context.Request.Params["CustOrderNo"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string Attachment = context.Request.Params["Attachment"].ToString().Trim();
        string CanViewUser = context.Request.Params["CanViewUser"].ToString().Trim();
        string ProjectID = context.Request.Params["ProjectID"].ToString().Trim();//所属项目ID

        sellOrderModel.ProjectID = ProjectID.Length == 0 ? null : (int?)Convert.ToInt32(ProjectID);
        sellOrderModel.CustID = Convert.ToInt32(CustID);//客户ID（关联客户信息表）                                
        sellOrderModel.CustTel = CustTel;//客户联系电话                                            
        sellOrderModel.OrderNo = strCode;//订单编号                                                
        sellOrderModel.Title = Title;//主题                                                    
        sellOrderModel.FromType = FromType;//源单类型（0无来源，1销售报价单，2销售合同）   
        try
        {
            sellOrderModel.FromBillID = Convert.ToInt32(FromBillID);
        }
        catch { }
      
        
        sellOrderModel.BusiType = BusiType;//业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨）
        try
        {
            sellOrderModel.CurrencyType = Convert.ToInt32(CurrencyType);
        }
        catch { }
        try
        {
            sellOrderModel.Rate = Convert.ToDecimal(Rate);
        }
        catch { sellOrderModel.Rate = 0; }
        try
        {
            sellOrderModel.TotalPrice = Convert.ToDecimal(TotalPrice);
        }
        catch { sellOrderModel.TotalPrice = 0; }
        try
        {
            sellOrderModel.Tax = Convert.ToDecimal(Tax);
        }
        catch { }
        try
        {
            sellOrderModel.TotalFee = Convert.ToDecimal(TotalFee);
        }
        catch { sellOrderModel.TotalFee = 0; }
        try
        {
            sellOrderModel.Discount = Convert.ToDecimal(Discount);
        }
        catch { sellOrderModel.Discount = 0; }
        try
        {
            sellOrderModel.DiscountTotal = Convert.ToDecimal(DiscountTotal);
        }
        catch { sellOrderModel.DiscountTotal = 0; }
        try
        {
            sellOrderModel.RealTotal = Convert.ToDecimal(RealTotal);
        }
        catch { sellOrderModel.RealTotal = 0; }
        try
        {
            sellOrderModel.CountTotal = Convert.ToDecimal(CountTotal);
        }
        catch { sellOrderModel.CountTotal = 0; }
        try
        {
            sellOrderModel.PayType = Convert.ToInt32(PayType);
        }
        catch { }
        try
        {
            sellOrderModel.MoneyType = Convert.ToInt32(MoneyType);
        }
        catch { }
        try
        {
            sellOrderModel.TakeType = Convert.ToInt32(TakeType);
        }
        catch { }
        try
        {
            sellOrderModel.CarryType = Convert.ToInt32(CarryType);
        }
        catch { }
        try
        {
            sellOrderModel.SellType = Convert.ToInt32(SellType);
        }
        catch { }
        try
        {
            sellOrderModel.Seller = Convert.ToInt32(Seller);
        }
        catch { }
        try
        {
            sellOrderModel.SellDeptId = Convert.ToInt32(SellDeptId);
        }
        catch { }
        try
        {
            sellOrderModel.OrderMethod = Convert.ToInt32(OrderMethod);
        }
        catch { }
        try
        {
            sellOrderModel.SaleFeeTotal = Convert.ToDecimal(SaleFeeTotal.Length == 0 ? "0" : SaleFeeTotal);//销售费用合计  
        }
        catch { }
        try
        {
            sellOrderModel.SendDate = Convert.ToDateTime(SendDate);//最迟发货时间（精确到分） 
        }
        catch { }
        try
        {
            sellOrderModel.OrderDate = Convert.ToDateTime(OrderDate);//下单日期 
        }
        catch { }
        try
        {
            sellOrderModel.StartDate = Convert.ToDateTime(StartDate);//开始日期 
        }
        catch { }
        try
        {
            sellOrderModel.EndDate = Convert.ToDateTime(EndDate);//截止日期     
        }
        catch { }
        sellOrderModel.isAddTax = isAddTax;
        sellOrderModel.TheyDelegate = TheyDelegate;//对方代表        
        try
        {
            sellOrderModel.OurDelegate = Convert.ToInt32(OurDelegate);//我方代表   
        }
        catch { }                                             
        sellOrderModel.Status = "1";//订单状态(1处理中 2处理完 3 终止)                        
        sellOrderModel.PayRemark = PayRemark;//付款说明                                                
        sellOrderModel.DeliverRemark = DeliverRemark;//交付说明                                                
        sellOrderModel.PackTransit = PackTransit;//包装运输说明                                            
        sellOrderModel.StatusNote = StatusNote;//异常终止原因                                            
        sellOrderModel.CustOrderNo = CustOrderNo;//客户订单号                                              
        sellOrderModel.Remark = Remark;//备注                                                    
        sellOrderModel.Attachment = Attachment;//附件                                                    
        sellOrderModel.BillStatus = "1";//单据状态（1制单，2执行，3变更，4手工结单，5自动结单） 
        
            sellOrderModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
            sellOrderModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
            sellOrderModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        
        sellOrderModel.CreateDate = DateTime.Now;//制单日期                                         
        sellOrderModel.ModifiedDate = DateTime.Now;//最后更新日期  
        sellOrderModel.CanViewUser = CanViewUser;//可查看此订单人员
        return sellOrderModel;
    }

    /// <summary>
    /// 获取SellOrderDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellOrderDetailModel> GetSellOrderDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        SellOrderDetailModel sellOrderDetailModel;
        List<SellOrderDetailModel> sellOrderDetailModelList = new List<SellOrderDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellOrderDetailModel = new SellOrderDetailModel();
                sellOrderDetailModel.OrderNo = strCode;
                sellOrderDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());
                sellOrderDetailModel.ProductID = Convert.ToInt32(inseritems[1].ToString());
                sellOrderDetailModel.ProductCount = Convert.ToDecimal(inseritems[2].ToString());
                sellOrderDetailModel.UnitID = inseritems[3].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[3].ToString());
                sellOrderDetailModel.UnitPrice = Convert.ToDecimal(inseritems[4].ToString());
                sellOrderDetailModel.TaxPrice = inseritems[5].Trim().Length == 0 ? null : (decimal?)Convert.ToDecimal(inseritems[5].ToString());
                sellOrderDetailModel.Discount = Convert.ToDecimal(inseritems[6].ToString());
                sellOrderDetailModel.TaxRate = inseritems[7].Trim().Length == 0 ? null : (decimal?)Convert.ToDecimal(inseritems[7].ToString());
                sellOrderDetailModel.TotalFee = Convert.ToDecimal(inseritems[8].ToString());
                sellOrderDetailModel.TotalPrice = Convert.ToDecimal(inseritems[9].ToString());
                sellOrderDetailModel.TotalTax = Convert.ToDecimal(inseritems[10].ToString());
                sellOrderDetailModel.SendTime = inseritems[11].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[11].ToString());
                sellOrderDetailModel.Remark = inseritems[13].ToString();

                if (inseritems[12].ToString().Trim().Length != 0)
                {
                    sellOrderDetailModel.Package = Convert.ToInt32(inseritems[12].ToString());
                }
                sellOrderDetailModel.UsedUnitID = inseritems[14].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[14].ToString());//单位
                sellOrderDetailModel.UsedUnitCount = Convert.ToDecimal(inseritems[15].ToString());//数量
                sellOrderDetailModel.UsedPrice = Convert.ToDecimal(inseritems[16].ToString());//单价
                sellOrderDetailModel.ExRate = Convert.ToDecimal(inseritems[17].ToString());//换算率
                
                sellOrderDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               
               
                sellOrderDetailModelList.Add(sellOrderDetailModel);
            }
        }
        return sellOrderDetailModelList;
    }

    /// <summary>
    /// 获取SellOrderFeeDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellOrderFeeDetailModel> GetSellOrderFeeDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strFeeInfo = context.Request.Params["strFeeInfo"].ToString().Trim();

        SellOrderFeeDetailModel sellOrderFeeDetailModel;
        List<SellOrderFeeDetailModel> sellOrderFeeDetailModelList = new List<SellOrderFeeDetailModel>();

        strarray = strFeeInfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellOrderFeeDetailModel = new SellOrderFeeDetailModel();
                if (inseritems[1].ToString().Trim().Length != 0)
                {
                    sellOrderFeeDetailModel.FeeID = Convert.ToInt32(inseritems[1].ToString());//包装要求
                }
                sellOrderFeeDetailModel.OrderNo = strCode;
                sellOrderFeeDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());//序号
                sellOrderFeeDetailModel.FeeTotal = Convert.ToDecimal(inseritems[2].ToString());//费用合计
               
                    sellOrderFeeDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               
              
                sellOrderFeeDetailModel.Remark = inseritems[3].ToString();//备注
                sellOrderFeeDetailModelList.Add(sellOrderFeeDetailModel);
            }
        }
        return sellOrderFeeDetailModelList;
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
            //string strKeyList = GetParam("keyList").Trim();
            string strKeyList = context.Request.Params["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    //ht.Add(arrKey[y].Trim(), GetParam(arrKey[y].Trim()).Trim());//添加keyvalue键值对
                    ht.Add(arrKey[y].Trim(), context.Request.Params[arrKey[y].Trim()].Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }

}