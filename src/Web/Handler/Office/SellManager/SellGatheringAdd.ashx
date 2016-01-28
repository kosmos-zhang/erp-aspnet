<%@ WebHandler Language="C#" Class="SellGatheringAdd" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class SellGatheringAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;
        string strMsg = string.Empty;//操作返回的信息  
        string action = context.Request.Params["action"].ToString().Trim();	//动作
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//回款计划编号产生的规则                                    
        string GatheringNo = context.Request.Params["GatheringNo"].ToString().Trim();	//回款计划编号 
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
                strCode = GatheringNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellGathering", "GatheringNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    strMsg = "该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!";
                }
                else
                {
                    strMsg = "回款计划单据编号已经存在";
                }
            }
            else
            {
                SellGatheringModel sellGatheringModel = new SellGatheringModel();
                GetSellGatheringModel(sellGatheringModel, strCode, context);
                //获取扩展属性
                Hashtable ht = GetExtAttr(context);
                isSucc = SellGatheringBus.InsertSellGathering(ht, sellGatheringModel, out strMsg);
            }
        }
        else if (action == "update")
        {
            strCode = GatheringNo;
            SellGatheringModel sellGatheringModel = new SellGatheringModel();
            GetSellGatheringModel(sellGatheringModel, strCode, context);
            //获取扩展属性
            Hashtable ht = GetExtAttr(context);
            isSucc = SellGatheringBus.UpdateSellGathering(ht,sellGatheringModel, out strMsg);
        }
        JsonClass jc;
        if (isSucc)
        {
            
            jc = new JsonClass(0, "", strCode, strMsg, 1);
        }
        else
        {
            jc = new JsonClass(0, "", strCode, strMsg, 0);
        }
        context.Response.Write(jc.ToJosnString());
    }

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="strCode"></param>
    /// <param name="context"></param>
    private void GetSellGatheringModel(SellGatheringModel sellGatheringModel, string strCode, HttpContext context)
    {
        string GatheringNo = strCode;    //回款计划编号     
        string Title = context.Request.Params["Title"].ToString().Trim();         //主题                      
        string CustID = context.Request.Params["CustID"].ToString().Trim();         //客户ID（关联客户信息表）                    
        string FromType = context.Request.Params["FromType"].ToString().Trim();       //源单类型（0无来源，1发货通知单，2销售订单） 
        string FromBillID = context.Request.Params["FromBillID"].ToString().Trim();     //源单ID                                      
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();   //币种ID(对应货币代码表CD)                    
        string PlanGatherDate = context.Request.Params["PlanGatherDate"].ToString().Trim(); //计划回款日期                                
        string PlanPrice = context.Request.Params["PlanPrice"].ToString().Trim();      //计划回款金额                                
        string GatheringTime = context.Request.Params["GatheringTime"].ToString().Trim();  //期次                                        
        string FactPrice = context.Request.Params["FactPrice"].ToString().Trim();      //实际回款金额                                
        string FactGatherDate = context.Request.Params["FactGatherDate"].ToString().Trim(); //实际回款日期                                
        string Seller = context.Request.Params["Seller"].ToString().Trim();         //业务员(对应员工表ID)                        
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim();     //部门(对部门表ID)                            
        string LinkBillNo = context.Request.Params["LinkBillNo"].ToString().Trim();     //回款相关单据号                              
        string State = context.Request.Params["State"].ToString().Trim();          //状态(1已回款2未回款 3部分回款)              
        string Remark = context.Request.Params["Remark"].ToString().Trim();         //备注                                        

        sellGatheringModel.Title = Title;//主题
        sellGatheringModel.GatheringNo = GatheringNo;    //回款计划编号                                 
        sellGatheringModel.CustID = Convert.ToInt32(CustID);         //客户ID（关联客户信息表）                     
        sellGatheringModel.FromType = FromType;       //源单类型（0无来源，1发货通知单，2销售订单）
        try
        {
            sellGatheringModel.FromBillID = Convert.ToInt32(FromBillID);     //源单ID
        }
        catch { }
        try
        {

            sellGatheringModel.CurrencyType = Convert.ToInt32(CurrencyType);   //币种ID(对应货币代码表CD) 
        }
        catch { }
        try
        {
            sellGatheringModel.PlanGatherDate = Convert.ToDateTime(PlanGatherDate); //计划回款日期  
        }
        catch { }
        try
        {
            sellGatheringModel.PlanPrice = Convert.ToDecimal(PlanPrice);      //计划回款金额     
        }
        catch { }
                               
        sellGatheringModel.GatheringTime = GatheringTime;  //期次     
        try
        {
            sellGatheringModel.FactPrice = Convert.ToDecimal(FactPrice);      //实际回款金额  
        }
        catch { }
        try
        {
            sellGatheringModel.FactGatherDate = Convert.ToDateTime(FactGatherDate); //实际回款日期  
        }
        catch { }
        try
        {
            sellGatheringModel.Seller = Convert.ToInt32(Seller);         //业务员(对应员工表ID)  
        }
        catch { }
        try
        {
            sellGatheringModel.SellDeptId = Convert.ToInt32(SellDeptId);     //部门(对部门表ID)        
        }
        catch { }                     
        sellGatheringModel.LinkBillNo = LinkBillNo;     //回款相关单据号                               
        sellGatheringModel.State = State;          //状态(1已回款2未回款 3部分回款)               
        sellGatheringModel.Remark = Remark;         //备注                                         

        
            sellGatheringModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
            sellGatheringModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
            sellGatheringModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
      

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