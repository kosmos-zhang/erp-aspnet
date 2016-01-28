<%@ WebHandler Language="C#" Class="SellChannelSttlAdd" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class SellChannelSttlAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;
        int orderID = 0;
        string strMsg = string.Empty;//操作返回的信息  
        string strFieldText = string.Empty;
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//发货单编号产生的规则                                    
        string SttlNo = context.Request.Params["SttlNo"].ToString().Trim();	//发货单编号 
        //状态为insert时才计算编号
        if (action == "insert")
        {
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType);
            }
            else
            {
                strCode = SttlNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellChannelSttl", "SttlNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "委托代销单据编号已经存在";
                }
            }
            else
            {
                SellChannelSttlModel sellChannelSttlModel = GetSellChannelSttlModel(strCode, context);
                List<SellChannelSttlDetailModel> sellChannelSttlDetailModelList = GetSellChannelSttlDetailModelList(strCode, context);
                //获取扩展属性
                Hashtable ht = GetExtAttr(context);
                isSucc = SellChannelSttlBus.SaveOrder(ht, sellChannelSttlModel, sellChannelSttlDetailModelList, out strMsg);
            }
        }
        else if (action == "update")//保存后修改
        {
            strCode = SttlNo;
            SellChannelSttlModel sellChannelSttlModel = GetSellChannelSttlModel(strCode, context);
            List<SellChannelSttlDetailModel> sellChannelSttlDetailModelList = GetSellChannelSttlDetailModelList(strCode, context);
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            isSucc = SellChannelSttlBus.UpdateOrder(ht,sellChannelSttlModel, sellChannelSttlDetailModelList, out strMsg);
        }
        else if (action == "confirm")//确认
        {
            strCode = SttlNo;
            SellChannelSttlModel sellChannelSttlModel = GetSellChannelSttlModel(strCode, context);
            sellChannelSttlModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellChannelSttlBus.ConfirmOrder(sellChannelSttlModel, out strMsg, out strFieldText);
        }
        else if (action == "UnConfirm")//取消确认
        {
            strCode = SttlNo;
            SellChannelSttlModel sellChannelSttlModel = GetSellChannelSttlModel(strCode, context);
            sellChannelSttlModel.ID = Convert.ToInt32(context.Request.Params["ID"].Trim());
            isSucc = SellChannelSttlBus.UnConfirmOrder(sellChannelSttlModel, out strMsg);
        }
        else if (action == "close")//结单
        {
            strCode = SttlNo;
            isSucc = SellChannelSttlBus.CloseOrder(strCode, out strMsg);
        }
        else if (action == "unClose")//取消结单
        {
            strCode = SttlNo;
            isSucc = SellChannelSttlBus.UnCloseOrder(strCode, out strMsg);
        }
        JsonClass jc;
        if (isSucc)
        {
            orderID = SellChannelSttlBus.GetOrderID(strCode);
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(orderID, strFieldText, strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }

    

   
    /// <summary>
    /// SellChannelSttlModel实体
    /// </summary>
    /// <param name="strCode">发货单编号</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private SellChannelSttlModel GetSellChannelSttlModel(string strCode, HttpContext context)
    {
        SellChannelSttlModel sellChannelSttlModel = new SellChannelSttlModel();

                                              
        string SttlNo = strCode;         //发货单编号                                                                                            
        string Title = context.Request.Params["Title"].ToString();           //主题                                                 
        string CustID = context.Request.Params["CustID"].ToString();          //客户ID（客户表）                                     
        string FromType = "1";        //源单类型（1发货通知单）                              
        string FromBillID = context.Request.Params["FromBillID"].ToString();      //来源单据ID（发货通知单）                             
        string PayType = context.Request.Params["PayType"].ToString();         //结算方式ID         
        string MoneyType = context.Request.Params["MoneyType"].ToString();         //结算方式ID                                           
        string Seller = context.Request.Params["Seller"].ToString();          //业务员(对应员工表ID)                                 
        string SellDeptId = context.Request.Params["SellDeptId"].ToString();      //部门(对部门表ID)                                     
        string CurrencyType = context.Request.Params["CurrencyType"].ToString();    //币种ID                                               
        string Rate = context.Request.Params["Rate"].ToString();            //汇率                                                 
        string SttlDate = context.Request.Params["SttlDate"].ToString();        //结算日期                                             
        string CountTotal = context.Request.Params["CountTotal"].ToString();      //本次结算代销数量合计                                 
        string TotalFee = context.Request.Params["TotalFee"].ToString();        //本次结算代销金额合计                                 
        string PushMoneyPercent = context.Request.Params["PushMoneyPercent"].ToString();//代销提成率                                           
        string PushMoney = context.Request.Params["PushMoney"].ToString();       //代销提成额                                           
        string HandFeeTotal = context.Request.Params["HandFeeTotal"].ToString();    //代销手续费                                           
        string SttlTotal = context.Request.Params["SttlTotal"].ToString();       //应结金额合计                                                                 
        string Remark = context.Request.Params["Remark"].ToString();          //备注                                                 
        string BillStatus = "1";      //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
                                                

        sellChannelSttlModel.CustID = Convert.ToInt32(CustID);//客户ID（关联客户信息表）                                                                       
        sellChannelSttlModel.SttlNo = SttlNo;//发货单编号                                                                                     
        sellChannelSttlModel.Title = Title;//主题                                                                                           
        sellChannelSttlModel.FromType = FromType;//源单类型（0无源单，1销售订单，2销售合同）                                                      
        sellChannelSttlModel.FromBillID = Convert.ToInt32( FromBillID);
        if (PayType.Length != 0)
        {
            sellChannelSttlModel.PayType = Convert.ToInt32(PayType);
        }
        try
        {
            sellChannelSttlModel.MoneyType = Convert.ToInt32(MoneyType);
        }
        catch { }
        sellChannelSttlModel.Seller = Convert.ToInt32( Seller);
        sellChannelSttlModel.SellDeptId = Convert.ToInt32( SellDeptId);
        sellChannelSttlModel.CurrencyType = Convert.ToInt32( CurrencyType);
        sellChannelSttlModel.Rate = Convert.ToDecimal( Rate);
        sellChannelSttlModel.SttlDate = Convert.ToDateTime( SttlDate);
        sellChannelSttlModel.CountTotal = Convert.ToDecimal( CountTotal);
        sellChannelSttlModel.TotalFee =Convert.ToDecimal(  TotalFee);
        sellChannelSttlModel.PushMoneyPercent =Convert.ToDecimal(  PushMoneyPercent);
        sellChannelSttlModel.PushMoney = Convert.ToDecimal( PushMoney);
        if (HandFeeTotal.Length != 0)
        {
            sellChannelSttlModel.HandFeeTotal = Convert.ToDecimal(HandFeeTotal);
        }
        sellChannelSttlModel.SttlTotal = Convert.ToDecimal( SttlTotal);
                                                                                                             
        sellChannelSttlModel.Remark = Remark;//备注                                                                                           
        sellChannelSttlModel.BillStatus = BillStatus;//单据状态（1制单，2执行，3变更，4手工结单，5自动结单）                                                                                                     
        sellChannelSttlModel.CreateDate = DateTime.Now;
        sellChannelSttlModel.ModifiedDate = DateTime.Now;
       
            sellChannelSttlModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
            sellChannelSttlModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
            sellChannelSttlModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
     
        return sellChannelSttlModel;
    }

    /// <summary>
    /// 获取SellChannelSttlDetailModel表实体列表
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<SellChannelSttlDetailModel> GetSellChannelSttlDetailModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        SellChannelSttlDetailModel sellChannelSttlDetailModel;
        List<SellChannelSttlDetailModel> sellChannelSttlDetailModelList = new List<SellChannelSttlDetailModel>();

        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                sellChannelSttlDetailModel = new SellChannelSttlDetailModel();
                sellChannelSttlDetailModel.SttlNo = strCode;
                sellChannelSttlDetailModel.SortNo = Convert.ToInt32(inseritems[0].ToString());
                sellChannelSttlDetailModel.ProductID = Convert.ToInt32(inseritems[1].ToString());
                sellChannelSttlDetailModel.UnitID = inseritems[2].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[2].ToString());
                sellChannelSttlDetailModel.ProductCount = Convert.ToDecimal(inseritems[3].ToString());
                sellChannelSttlDetailModel.SttlNumber = Convert.ToDecimal(inseritems[4].ToString());
                sellChannelSttlDetailModel.UnitPrice = Convert.ToDecimal(inseritems[5].ToString());
                sellChannelSttlDetailModel.totalPrice = Convert.ToDecimal(inseritems[6].ToString());
                sellChannelSttlDetailModel.Remark = inseritems[7].ToString();
                sellChannelSttlDetailModel.FromType = "1";
                sellChannelSttlDetailModel.FromBillID = Convert.ToInt32(inseritems[9].ToString());
                sellChannelSttlDetailModel.FromLineNo = Convert.ToInt32(inseritems[10].ToString());

                sellChannelSttlDetailModel.UsedUnitID = inseritems[11].Trim().Length == 0 ? null : (int?)Convert.ToInt32(inseritems[11].ToString());//单位
                sellChannelSttlDetailModel.UsedUnitCount = Convert.ToDecimal(inseritems[12].ToString());//数量
                sellChannelSttlDetailModel.UsedPrice = Convert.ToDecimal(inseritems[13].ToString());//单价
                sellChannelSttlDetailModel.ExRate = Convert.ToDecimal(inseritems[14].ToString());//换算率
             
                sellChannelSttlDetailModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码               
               
                sellChannelSttlDetailModelList.Add(sellChannelSttlDetailModel);
            }
        }
        return sellChannelSttlDetailModelList;
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