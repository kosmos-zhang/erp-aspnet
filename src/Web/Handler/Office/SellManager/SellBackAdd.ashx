<%@ WebHandler Language="C#" Class="SellBackAdd" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class SellBackAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;
        string strMsg = string.Empty;//操作返回的信息  
        string strFieldText = string.Empty;
        int orderID = 0;
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//发货单编号产生的规则                                    
        string BackNo = context.Request.Params["BackNo"].ToString().Trim();	//发货单编号 
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellBack", "BackNo");
            }
            else
            {
                strCode = BackNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellBack", "BackNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "销售退货单据编号已经存在";
                }
            }
            else
            {
                SellBackModel sellBackModel = GetSellBackModel(strCode, context);
                List<SellBackDetailModel> sellBackDetailModellList = GetSellBackDetailModelList(strCode, context);
                //获取扩展属性
                Hashtable ht = GetExtAttr(context);
                isSucc = SellBackBus.SaveSellBack(ht, sellBackModel, sellBackDetailModellList, out strMsg);
            }
        }
        else if (action == "update")//保存后修改
        {
            strCode = BackNo;
            SellBackModel sellBackModel = GetSellBackModel(strCode, context);
            List<SellBackDetailModel> sellBackDetailModellList = GetSellBackDetailModelList(strCode, context);
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            isSucc = SellBackBus.UpdateSellBack(ht,sellBackModel, sellBackDetailModellList, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = BackNo;
            SellBackModel sellBackModel = GetSellBackModel(strCode, context);
            sellBackModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellBackBus.ConfirmOrder(sellBackModel, out strMsg, out strFieldText);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = BackNo;
            SellBackModel sellBackModel = GetSellBackModel(strCode, context);
            sellBackModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellBackBus.UnConfirmOrder(sellBackModel, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = BackNo;
            isSucc = SellBackBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = BackNo;
            isSucc = SellBackBus.UnCloseOrder(strCode, out strMsg);
        }
        JsonClass jc;
        if (isSucc)
        {
            orderID = SellBackBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }





    /// <summary>
    /// SellBackModel实体
    /// </summary>
    /// <param name="strCode">发货单编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellBackModel GetSellBackModel(string strCode, HttpContext context)
    {
        SellBackModel sellBackModel = new SellBackModel();

        string BackNo = strCode;         //发货单编号                                                        
        string CustID = context.Request.Params["CustID"].ToString().Trim();
        string FromType = context.Request.Params["FromType"].ToString().Trim();
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();
        string CustTel = context.Request.Params["CustTel"].ToString().Trim();
        string BackCargoTheme = context.Request.Params["BackCargoTheme"].ToString().Trim();
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();
        string BackDate = context.Request.Params["BackDate"].ToString().Trim();
        string Seller = context.Request.Params["Seller"].ToString().Trim();
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim();
        string CarryType = context.Request.Params["CarryType"].ToString().Trim();
        string SendAddress = context.Request.Params["SendAddress"].ToString().Trim();
        string ReceiveAddress = context.Request.Params["ReceiveAddress"].ToString().Trim();
        string PayType = context.Request.Params["PayType"].ToString().Trim();
        string MoneyType = context.Request.Params["MoneyType"].ToString().Trim();
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();
        string Rate = context.Request.Params["Rate"].ToString().Trim();
        string TotalPrice = context.Request.Params["TotalPrice"].ToString().Trim();
        string Tax = context.Request.Params["Tax"].ToString().Trim();
        string TotalFee = context.Request.Params["TotalFee"].ToString().Trim();
        string Discount = context.Request.Params["Discount"].ToString().Trim();
        string DiscountTotal = context.Request.Params["DiscountTotal"].ToString().Trim();
        string RealTotal = context.Request.Params["RealTotal"].ToString().Trim();
        string CountTotal = context.Request.Params["CountTotal"].ToString().Trim();
        string NotPayTotal = context.Request.Params["NotPayTotal"].ToString().Trim();
        string BackTotal = context.Request.Params["BackTotal"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string isAddTax = context.Request.Params["isAddTax"].ToString().Trim();
        string ProjectID = context.Request.Params["ProjectID"].ToString().Trim();//所属项目ID

        sellBackModel.ProjectID = ProjectID.Length == 0 ? null : (int?)Convert.ToInt32(ProjectID);
        sellBackModel.BackNo = BackNo;//退货单编号                                                        
        sellBackModel.CustID = Convert.ToInt32(CustID);//客户ID（关联客户信息表）                                          
        sellBackModel.FromType = FromType;//来源单据类型（0无来源，1发货通知单） 
        if (FromType != "0")
        {
            sellBackModel.FromBillID = Convert.ToInt32(FromBillID);//来源单据ID（发货通知单） 
        }
        sellBackModel.CustTel = CustTel;//客户联系电话                                                      
        sellBackModel.Title = BackCargoTheme;//主题                                                              
        sellBackModel.BusiType = BusiType;//业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）
        sellBackModel.BackDate = Convert.ToDateTime(BackDate);//退货日期                                                          
        sellBackModel.Seller = Convert.ToInt32(Seller);//业务员(对应员工表ID)                                              
        sellBackModel.SellDeptId = Convert.ToInt32(SellDeptId);//部门(对部门表ID) 
        if (CarryType.Length != 0)
        {
            sellBackModel.CarryType = Convert.ToInt32(CarryType);//运送方式ID       
        }
        sellBackModel.SendAddress = SendAddress;//发货地址                                                          
        sellBackModel.ReceiveAddress = ReceiveAddress;//收货地址  
        if (PayType.Length != 0)
        {
            sellBackModel.PayType = Convert.ToInt32(PayType);//结算方式ID  
        }
        if (MoneyType.Length != 0)
        {
            sellBackModel.MoneyType = Convert.ToInt32(MoneyType);//支付方式ID  
        }
        if (CurrencyType.Length != 0)
        {
            sellBackModel.CurrencyType = Convert.ToInt32(CurrencyType);//币种ID                                                            
            sellBackModel.Rate = Convert.ToDecimal(Rate);//汇率    
        }
        sellBackModel.isAddTax = isAddTax;//是否增值税（0否,1是                                               
        sellBackModel.TotalPrice = Convert.ToDecimal(TotalPrice);//金额合计                                                          
        sellBackModel.Tax = Convert.ToDecimal(Tax);//税额合计                                                          
        sellBackModel.TotalFee = Convert.ToDecimal(TotalFee);//含税金额合计                                                      
        sellBackModel.Discount = Convert.ToDecimal(Discount);//整单折扣（%）                                                     
        sellBackModel.DiscountTotal = Convert.ToDecimal(DiscountTotal);//折扣金额                                                          
        sellBackModel.RealTotal = Convert.ToDecimal(RealTotal);//折后含税金额合计                                                  
        sellBackModel.CountTotal = Convert.ToDecimal(CountTotal);//退货数量合计  
        try
        {
            sellBackModel.NotPayTotal = Convert.ToDecimal(NotPayTotal);//抵应收账款     
        }
        catch { }
        try
        {
            sellBackModel.BackTotal = Convert.ToDecimal(BackTotal);//应退货款总额    
        }
        catch { }
        sellBackModel.Remark = Remark;//备注                                                              
        sellBackModel.BillStatus = "1";//单据状态（1制单，2执行，3变更，4手工结单，5自动结单）             

        sellBackModel.CreateDate = DateTime.Now;
        sellBackModel.ModifiedDate = DateTime.Now;

        sellBackModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
        sellBackModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
        sellBackModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  

        return sellBackModel;
    }

    /// <summary>
    /// 获取SellBackDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellBackDetailModel> GetSellBackDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        SellBackDetailModel sellBackDetailModel;
        List<SellBackDetailModel> sellBackDetailModellList = new List<SellBackDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellBackDetailModel = new SellBackDetailModel();


                sellBackDetailModel.BackNo = strCode;
                sellBackDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());
                sellBackDetailModel.ProductID = Convert.ToInt32(inseritems[1].ToString());
                try
                {
                    sellBackDetailModel.ProductCount = Convert.ToDecimal(inseritems[2].ToString());
                }
                catch { sellBackDetailModel.ProductCount = 0; }
                sellBackDetailModel.UnitID = inseritems[3].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[3].ToString());
                sellBackDetailModel.UnitPrice = Convert.ToDecimal(inseritems[4].ToString());
                sellBackDetailModel.TaxPrice = Convert.ToDecimal(inseritems[5].ToString());
                sellBackDetailModel.Discount = Convert.ToDecimal(inseritems[6].ToString());
                sellBackDetailModel.TaxRate = Convert.ToDecimal(inseritems[7].ToString());
                sellBackDetailModel.TotalFee = Convert.ToDecimal(inseritems[8].ToString());
                sellBackDetailModel.TotalPrice = Convert.ToDecimal(inseritems[9].ToString());
                sellBackDetailModel.TotalTax = Convert.ToDecimal(inseritems[10].ToString());
                try
                {
                    sellBackDetailModel.BackNumber = Convert.ToDecimal(inseritems[12].ToString());
                }
                catch { sellBackDetailModel.BackNumber = 0; }
                sellBackDetailModel.Remark = inseritems[3].ToString();

                if (inseritems[11].ToString().Trim().Length != 0)
                {
                    sellBackDetailModel.Package = Convert.ToInt32(inseritems[11].ToString());
                }
                if (inseritems[13].ToString().Trim().Length != 0)
                {
                    sellBackDetailModel.Reason = Convert.ToInt32(inseritems[13].ToString());
                }
                sellBackDetailModel.Remark = inseritems[14].ToString();
                sellBackDetailModel.FromType = inseritems[15].ToString();
                if (inseritems[15].ToString().Trim() != "0")
                {
                    sellBackDetailModel.FromBillID = Convert.ToInt32(inseritems[16].ToString());
                    sellBackDetailModel.FromLineNo = Convert.ToInt32(inseritems[17].ToString());
                }
                sellBackDetailModel.UsedUnitID = inseritems[18].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[18].ToString());//单位
                sellBackDetailModel.UsedUnitCount = Convert.ToDecimal(inseritems[19].ToString());//数量
                sellBackDetailModel.UsedPrice = Convert.ToDecimal(inseritems[20].ToString());//单价
                sellBackDetailModel.ExRate = Convert.ToDecimal(inseritems[21].ToString());//换算率
                
                sellBackDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               

                sellBackDetailModellList.Add(sellBackDetailModel);
            }
        }
        return sellBackDetailModellList;
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