<%@ WebHandler Language="C#" Class="SellSendAdd" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using System.Collections;
using XBase.Model.Office.SellManager;

public class SellSendAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否更新成功       
        string strCode = string.Empty;
        int orderID = 0;
        string strMsg = string.Empty;//操作返回的信息  
        string strFieldText = string.Empty;
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//发货单编号产生的规则                                    
        string SendNo = context.Request.Params["SendNo"].ToString().Trim();	//发货单编号 
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellSend", "SendNo");
            }
            else
            {
                strCode = SendNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellSend", "SendNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "销售发货单据编号已经存在";
                }
            }
            else
            {
                SellSendModel sellSendModel = GetSellSendModel(strCode, context);
                List<SellSendDetailModel> sellSendDetailModelList = GetSellSendDetailModelList(strCode, context);
                //获取扩展属性
                Hashtable ht = GetExtAttr(context);
                isSucc = SellSendBus.SaveSellSend(ht, sellSendModel, sellSendDetailModelList, out strMsg);
            }
        }
        else if (action == "update")//保存后修改
        {
            strCode = SendNo;
            SellSendModel sellSendModel = GetSellSendModel(strCode, context);
            List<SellSendDetailModel> sellSendDetailModelList = GetSellSendDetailModelList(strCode, context);
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            isSucc = SellSendBus.UpdateSellSend(ht,sellSendModel, sellSendDetailModelList, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = SendNo;
            SellSendModel sellSendModel = GetSellSendModel(strCode, context);
            sellSendModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellSendBus.ConfirmOrder(sellSendModel, out strMsg, out strFieldText);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = SendNo;
            SellSendModel sellSendModel = GetSellSendModel(strCode, context);
            sellSendModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellSendBus.UnConfirmOrder(sellSendModel, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = SendNo;
            isSucc = SellSendBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = SendNo;
            isSucc = SellSendBus.UnCloseOrder(strCode, out strMsg);
        }
        else if (action == "checkProCount")//验证库存数量
        {
            CheckProCount(context, out strMsg, out strFieldText);
        }

        if (isSucc)
        {
            orderID = SellSendBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }

    /// <summary>
    /// 验证库存数量
    /// </summary>
    /// <param name="strMsg"></param>
    /// <param name="strFieldText"></param>
    private void CheckProCount(HttpContext context, out string strMsg, out string strFieldText)
    {
        strFieldText = "";
        strMsg = "";

        Hashtable ht = new Hashtable();
        List<SellSendDetailModel> sellSendDetailModelList = GetSellSendDetailModelList("", context);
        foreach (SellSendDetailModel item in sellSendDetailModelList)
        {
            if (ht.Contains(item.ProductID))
            {
                ht[item.ProductID] = Convert.ToInt32(ht[item.ProductID]) + item.ProductCount;
            }
            else
            {
                ht.Add(item.ProductID, item.ProductCount);
            }
        }
        if (ht.Count != 0)
        {
            SellSendBus.CheckProCount(ht, out strMsg,out strFieldText);
        }
    }


    /// <summary>
    /// SellSendModel实体
    /// </summary>
    /// <param name="strCode">发货单编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellSendModel GetSellSendModel(string strCode, HttpContext context)
    {
        SellSendModel sellSendModel = new SellSendModel();

        string CustID = context.Request.Params["CustID"].ToString().Trim();         //客户ID（关联客户信息表）                                          
        string SendNo = strCode;         //发货单编号                                                        
        string Title = context.Request.Params["Title"].ToString().Trim();          //主题                                                              
        string FromType = context.Request.Params["FromType"].ToString().Trim();       //源单类型（0无源单，1销售订单，2销售合同）                         
        string SellType = context.Request.Params["SellType"].ToString().Trim();       //销售类别ID（对应分类代码表ID）                                    
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();       //业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）
        string PayType = context.Request.Params["PayType"].ToString().Trim();        //结算方式ID（对应分类代码表ID）                                    
        string Seller = context.Request.Params["Seller"].ToString().Trim();         //业务员(对应员工表ID)                                              
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim();     //部门(对部门表ID)                                                  
        string MoneyType = context.Request.Params["MoneyType"].ToString().Trim();      //支付方式ID                                                        
        string TakeType = context.Request.Params["TakeType"].ToString().Trim();       //交货方式ID                                                        
        string CarryType = context.Request.Params["CarryType"].ToString().Trim();      //运送方式ID                                                        
        string SendAddr = context.Request.Params["SendAddr"].ToString().Trim();       //发货地址                                                          
        string Sender = context.Request.Params["Sender"].ToString().Trim();         //发货人ID（对应员工表ID）                                          
        string ReceiveAddr = context.Request.Params["ReceiveAddr"].ToString().Trim();    //收货地址                                                          
        string Receiver = context.Request.Params["Receiver"].ToString().Trim();       //收货人姓名                                                        
        string Tel = context.Request.Params["Tel"].ToString().Trim();            //收货人电话                                                        
        string Modile = context.Request.Params["Modile"].ToString().Trim();         //收货人移动电话                                                    
        string Post = context.Request.Params["Post"].ToString().Trim();           //收货人邮编                                                        
        string IntendSendDate = context.Request.Params["IntendSendDate"].ToString().Trim(); //预计发货时间                                                      
        string Transporter = context.Request.Params["Transporter"].ToString().Trim();    //运输商                                                            
        string TransportFee = context.Request.Params["TransportFee"].ToString().Trim();   //运费合计                                                          
        string TransPayType = context.Request.Params["TransPayType"].ToString().Trim();   //运费结算方式ID                                                    
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();   //币种ID                                                            
        string Rate = context.Request.Params["Rate"].ToString().Trim();           //汇率                                                              
        string TotalPrice = context.Request.Params["TotalPrice"].ToString().Trim();     //金额合计                                                          
        string Tax = context.Request.Params["Tax"].ToString().Trim();            //税额合计                                                          
        string TotalFee = context.Request.Params["TotalFee"].ToString().Trim();       //含税金额合计                                                      
        string Discount = context.Request.Params["Discount"].ToString().Trim();       //整单折扣（%）                                                     
        string DiscountTotal = context.Request.Params["DiscountTotal"].ToString().Trim();  //折扣金额                                                          
        string RealTotal = context.Request.Params["RealTotal"].ToString().Trim();      //折后含税金额                                                      
        string isAddTax = context.Request.Params["isAddTax"].ToString().Trim();       //是否增值税（0否,1是                                               
        string CountTotal = context.Request.Params["CountTotal"].ToString().Trim();     //发货数量合计                                                      
        string PayRemark = context.Request.Params["PayRemark"].ToString().Trim();      //付款说明                                                          
        string DeliverRemark = context.Request.Params["DeliverRemark"].ToString().Trim();  //交付说明                                                          
        string PackTransit = context.Request.Params["PackTransit"].ToString().Trim();    //包装运输说明                                                                                           
        string Remark = context.Request.Params["Remark"].ToString().Trim();         //备注                                                              
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();         //来源单据编号
        string ProjectID = context.Request.Params["ProjectID"].ToString().Trim();//所属项目ID
        string CanViewUser = context.Request.Params["CanViewUser"].ToString().Trim();

        sellSendModel.ProjectID = ProjectID.Length == 0 ? null : (int?)Convert.ToInt32(ProjectID);
        sellSendModel.CustID = Convert.ToInt32(CustID);//客户ID（关联客户信息表）                                                                       
        sellSendModel.SendNo = SendNo;//发货单编号                                                                                     
        sellSendModel.Title = Title;//主题                                                                                           
        sellSendModel.FromType = FromType;//源单类型（0无源单，1销售订单，2销售合同）  
        if (FromType == "1")
        {
            sellSendModel.FromBillID = Convert.ToInt32(FromBillID);
        }
        try
        {
            sellSendModel.SellType = Convert.ToInt32(SellType);//销售类别ID（对应分类代码表ID）   
        }
        catch { }
        sellSendModel.BusiType = BusiType;//业务类型（1普通销售,2委托代销,3直运，4零售，5销售调拨，6分期付款）  
        try
        {
            sellSendModel.PayType = Convert.ToInt32(PayType);//结算方式ID（对应分类代码表ID） 
        }
        catch { }
        try
        {
            sellSendModel.Seller = Convert.ToInt32(Seller);//业务员(对应员工表ID) 
        }
        catch { }
        try
        {
            sellSendModel.SellDeptId = Convert.ToInt32(SellDeptId);//部门(对部门表ID)  
        }
        catch { }
        try
        {
            sellSendModel.MoneyType = Convert.ToInt32(MoneyType);//支付方式ID 
        }
        catch { }
        try
        {
            sellSendModel.TakeType = Convert.ToInt32(TakeType);//交货方式ID   
        }
        catch { }
        try
        {
            sellSendModel.CarryType = Convert.ToInt32(CarryType);//运送方式ID  
        }
        catch { }
        sellSendModel.SendAddr = SendAddr;//发货地址       
        try
        {
            sellSendModel.Sender = Convert.ToInt32(Sender);//发货人ID（对应员工表ID）     
        }
        catch { }
        sellSendModel.ReceiveAddr = ReceiveAddr;//收货地址                                                                                       
        sellSendModel.Receiver = Receiver;//收货人姓名                                                                                     
        sellSendModel.Tel = Tel;//收货人电话                                                                                     
        sellSendModel.Modile = Modile;//收货人移动电话                                                                                 
        sellSendModel.Post = Post;//收货人邮编           
        try
        {
            sellSendModel.IntendSendDate = Convert.ToDateTime(IntendSendDate);//预计发货时间   
        }
        catch { }
        try
        {
            sellSendModel.Transporter = Convert.ToInt32(Transporter);//运输商 
        }
        catch { }
        try
        {
            sellSendModel.TransportFee = Convert.ToDecimal(TransportFee);//运费合计
        }
        catch { }
        try
        {
            sellSendModel.TransPayType = Convert.ToInt32(TransPayType);//运费结算方式ID 
        }
        catch { }
        try
        {
            sellSendModel.CurrencyType = Convert.ToInt32(CurrencyType);//币种ID                                                  
            sellSendModel.Rate = Convert.ToDecimal(Rate);//汇率  
        }
        catch { }
        try
        {
            sellSendModel.TotalPrice = Convert.ToDecimal(TotalPrice);//金额合计    
        }
        catch
        {
            sellSendModel.TotalPrice = 0;//金额合计    
        }
        try
        {
            sellSendModel.Tax = Convert.ToDecimal(Tax);//税额合计 
        }
        catch
        {
            sellSendModel.Tax = 0;//税额合计  
        }
        try
        {
            sellSendModel.TotalFee = Convert.ToDecimal(TotalFee);//含税金额合计   
        }
        catch
        {
            sellSendModel.TotalFee = 0;//含税金额合计   
        }
        try
        {
            sellSendModel.Discount = Convert.ToDecimal(Discount);//整单折扣（%）  
        }
        catch
        {
            sellSendModel.Discount = 0;//整单折扣（%）  
        }
        try
        {
            sellSendModel.DiscountTotal = Convert.ToDecimal(DiscountTotal);//折扣金额
        }
        catch
        {
            sellSendModel.DiscountTotal = 0;//折扣金额
        }
        try
        {
            sellSendModel.RealTotal = Convert.ToDecimal(RealTotal);//折后含税金额    
        }
        catch
        {
            sellSendModel.RealTotal = 0;//折后含税金额    
        }
        sellSendModel.isAddTax = isAddTax;//是否增值税（0否,1是 )  
        try
        {
            sellSendModel.CountTotal = Convert.ToDecimal(CountTotal);//发货数量合计
        }
        catch
        {
            sellSendModel.CountTotal = 0;//发货数量合计
        }
        sellSendModel.PayRemark = PayRemark;//付款说明                                                                                       
        sellSendModel.DeliverRemark = DeliverRemark;//交付说明                                                                                       
        sellSendModel.PackTransit = PackTransit;//包装运输说明                                                                                                                                                    
        sellSendModel.Remark = Remark;//备注                                                                                           
        sellSendModel.BillStatus = "1";//单据状态（1制单，2执行，3变更，4手工结单，5自动结单）                                                                                                     
        sellSendModel.CreateDate = DateTime.Now;
        sellSendModel.ModifiedDate = DateTime.Now;

        sellSendModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
        sellSendModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
        sellSendModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        sellSendModel.CanViewUser = CanViewUser;//可查看此单人员

        return sellSendModel;
    }

    /// <summary>
    /// 获取SellSendDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellSendDetailModel> GetSellSendDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        SellSendDetailModel sellSendDetailModel;
        List<SellSendDetailModel> sellSendDetailModelList = new List<SellSendDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellSendDetailModel = new SellSendDetailModel();
                sellSendDetailModel.SendNo = strCode;
                sellSendDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());
                sellSendDetailModel.ProductID = Convert.ToInt32(inseritems[1].ToString());
                sellSendDetailModel.ProductCount = Convert.ToDecimal(inseritems[2].ToString());
                sellSendDetailModel.UnitID = Convert.ToInt32(inseritems[3].ToString());
                sellSendDetailModel.UnitPrice = Convert.ToDecimal(inseritems[4].ToString());
                sellSendDetailModel.TaxPrice = Convert.ToDecimal(inseritems[5].ToString());
                sellSendDetailModel.Discount = Convert.ToDecimal(inseritems[6].ToString());
                sellSendDetailModel.TaxRate = Convert.ToDecimal(inseritems[7].ToString());
                sellSendDetailModel.TotalFee = Convert.ToDecimal(inseritems[8].ToString());
                sellSendDetailModel.TotalPrice = Convert.ToDecimal(inseritems[9].ToString());
                sellSendDetailModel.TotalTax = Convert.ToDecimal(inseritems[10].ToString());
                sellSendDetailModel.SendDate = Convert.ToDateTime(inseritems[11].ToString());
                if (inseritems[12].ToString().Trim().Length != 0)
                {
                    sellSendDetailModel.Package = Convert.ToInt32(inseritems[12].ToString());
                }
                sellSendDetailModel.Remark = inseritems[13].ToString();
                sellSendDetailModel.FromType = inseritems[14].ToString();
                if (inseritems[14].ToString().Trim() != "0")
                {
                    sellSendDetailModel.FromBillID = Convert.ToInt32(inseritems[15].ToString());
                    sellSendDetailModel.FromLineNo = Convert.ToInt32(inseritems[16].ToString());
                }
                sellSendDetailModel.UsedUnitID = Convert.ToInt32(inseritems[17].ToString());//单位
                sellSendDetailModel.UsedUnitCount = Convert.ToDecimal(inseritems[18].ToString());//数量
                sellSendDetailModel.UsedPrice = Convert.ToDecimal(inseritems[19].ToString());//单价
                sellSendDetailModel.ExRate = Convert.ToDecimal(inseritems[20].ToString());//换算率
                
                sellSendDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               

                sellSendDetailModelList.Add(sellSendDetailModel);
            }
        }
        return sellSendDetailModelList;
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