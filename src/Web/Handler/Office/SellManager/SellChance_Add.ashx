<%@ WebHandler Language="C#" Class="SellChance_Add" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;
using System.Collections;

public class SellChance_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;

        string action = context.Request.Params["action"].ToString().Trim();	//动作

        string ChanceNo = context.Request.Params["ChanceNo"].ToString().Trim();	//发货单编号

        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        
        //状态为insert时才计算编号
        if (action == "insert")
        {
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//发货单编号产生的规则  
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "SellChance", "ChanceNo");
            }
            else
            {
                strCode = ChanceNo;
            }
            JsonClass jc;
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("SellChance", "ChanceNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 0);
                }
                else
                {
                    jc = new JsonClass("销售机会单据编号已经存在", "", 0);
                }
            }
            else
            {
                isSucc = InsertOrder(ht, strCode, context);
                int billID = 0;
                if (isSucc)
                {
                    billID = SellChanceBus.GetBillIDByBillNo(strCode, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    jc = new JsonClass(billID, "", strCode, "保存成功！", 1);
                }
                else
                {
                    jc = new JsonClass(billID,"", strCode,"保存失败！", 0);
                }
                
            }
            context.Response.Write(jc.ToJosnString());
        }
        else if (action == "update")//保存后修改
        {
            strCode = ChanceNo;
            isSucc = UpdateOrder(ht,strCode, context);
            JsonClass jc;
            int billID = 0;
            if (isSucc)
            {
                billID = SellChanceBus.GetBillIDByBillNo(strCode, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                jc = new JsonClass(billID, "", strCode, "修改成功！", 1);
            }
            else
            {
                jc = new JsonClass(billID, "", strCode,"修改失败！", 0);
            }
            context.Response.Write(jc.ToJosnString());
        }
        else if (action == "getPush")//获取推进历史
        {
            strCode = ChanceNo;
            string strJson = string.Empty;
            DataTable dt = SellChanceBus.GetPush(ChanceNo);
            strJson = "{";
            if (dt.Rows.Count > 0)
            {
                strJson += "\"data\":" + JsonClass.DataTable2Json(dt);
            }
            strJson += "}";
            context.Response.Write(strJson);
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
                    ht.Add(arrKey[y].Trim(), context.Request.Params[arrKey[y].Trim()].Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }
    
    /// <summary>
    /// 添加销售机会
    /// </summary>
    /// <returns></returns>
    private bool InsertOrder(Hashtable ht,string strCode, HttpContext context)
    {
        bool isSucc = false;//是否更新成功

        SellChanceModel sellChanceModel = GetSellChanceModel(strCode, context);
        SellChancePushModel sellChancePushModel = GetSellChancePushModel(strCode, context);
        isSucc = SellChanceBus.InsertSellChance(ht,sellChanceModel, sellChancePushModel);
        return isSucc;
    }

    /// <summary>
    /// 添加销售机会
    /// </summary>
    /// <returns></returns>
    private bool UpdateOrder(Hashtable ht,string strCode, HttpContext context)
    {
        bool isSucc = false;//是否更新成功

        SellChanceModel sellChanceModel = GetSellChanceModel(strCode, context);
        sellChanceModel.ID=Convert.ToInt32( context.Request.Params["billID"].Trim());
        SellChancePushModel sellChancePushModel = GetSellChancePushModel(strCode, context);
        isSucc = SellChanceBus.UpdateSellChance(ht,sellChanceModel, sellChancePushModel);
        return isSucc;
    }


    private SellChanceModel GetSellChanceModel(string strCode, HttpContext context)
    {
        SellChanceModel sellChanceModel = new SellChanceModel();
        string CustID = context.Request.Params["CustID"].ToString().Trim();     //客户ID（对应客户表ID）           
        string CustType = context.Request.Params["CustType"].ToString().Trim();   //客户类型ID                       
        string CustTel = context.Request.Params["CustTel"].ToString().Trim();    //客户联系电话                                        
        string Title = context.Request.Params["Title"].ToString().Trim();      //销售机会主题                     
        string ChanceType = context.Request.Params["ChanceType"].ToString().Trim(); //销售机会类型(对应分类代码表ID)   
        string HapSource = context.Request.Params["HapSource"].ToString().Trim();  //销售机会来源ID(对应分类代码表ID) 
        string Seller = context.Request.Params["Seller"].ToString().Trim();     //业务员(对应员工表ID)             
        string SellDeptId = context.Request.Params["SellDeptId"].ToString().Trim(); //部门(对部门表ID)                 
        string FindDate = context.Request.Params["FindDate"].ToString().Trim();   //发现日期                         
        string ProvideMan = context.Request.Params["ProvideMan"].ToString().Trim(); //提供人                           
        string Requires = context.Request.Params["Requires"].ToString().Trim();   //需求描述                         
        string IntendDate = context.Request.Params["IntendDate"].ToString().Trim(); //预计签单日期                     
        string IntendMoney = context.Request.Params["IntendMoney"].ToString().Trim();//预期金额                         
        string Remark = context.Request.Params["Remark"].ToString().Trim();     //备注                             
        string IsQuoted = context.Request.Params["IsQuoted"].ToString().Trim();   //是否被报价(0-否，1-是)        
        string CanViewUser = context.Request.Params["CanViewUser"].ToString().Trim();     //可查看该销售机会的人员（ID，多个）                             
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();   //可查看该销售机会的人员姓名 
        string IsMobileNotice = context.Request.Params["IsMobileNotice"].Trim();//是否设置手机提醒
        string RemindTime = context.Request.Params["RemindTime"].Trim();//提醒时间
        string RemindMTel = context.Request.Params["RemindMTel"].Trim();//提醒手机号码
        string RemindContent = context.Request.Params["RemindContent"].Trim();//提醒内容
        string ReceiverID = context.Request.Params["ReceiverID"].Trim();//接收人

        sellChanceModel.CustID = Convert.ToInt32(CustID);//客户ID（对应客户表ID）          
        sellChanceModel.CustType = CustType.Length == 0 ? null :(int?) Convert.ToInt32(CustType);//客户类型ID                      
        sellChanceModel.CustTel = CustTel;//客户联系电话                    
        sellChanceModel.ChanceNo = strCode;//销售机会编号                    
        sellChanceModel.Title = Title;//销售机会主题    
        if (ChanceType.Length != 0)
        {
            try
            {
                sellChanceModel.ChanceType = Convert.ToInt32(ChanceType);//销售机会类型(对应分类代码表ID)  
            }
            catch { }
        }
        if (HapSource.Length != 0)
        {
            try
            {
                sellChanceModel.HapSource = Convert.ToInt32(HapSource);//销售机会来源ID(对应分类代码表ID)
            }
            catch { }
        }
        sellChanceModel.Seller = Convert.ToInt32(Seller);//业务员(对应员工表ID)            
        sellChanceModel.SellDeptId = Convert.ToInt32(SellDeptId);//部门(对部门表ID)                
        sellChanceModel.FindDate = Convert.ToDateTime(FindDate);//发现日期                        
        sellChanceModel.ProvideMan = ProvideMan;//提供人                          
        sellChanceModel.Requires = Requires;//需求描述                        
        sellChanceModel.IntendDate = Convert.ToDateTime(IntendDate);//预计签单日期                    
        sellChanceModel.IntendMoney = Convert.ToDecimal(IntendMoney);//预期金额                        
        sellChanceModel.Remark = Remark;//备注                            
        sellChanceModel.IsQuoted = IsQuoted;//是否被报价(0-否，1-是)          
        sellChanceModel.CreateDate = DateTime.Now;
        sellChanceModel.ModifiedDate = DateTime.Now;
        sellChanceModel.CanViewUser = CanViewUser;
        sellChanceModel.CanViewUserName = CanViewUserName;
        sellChanceModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
        sellChanceModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
        sellChanceModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        sellChanceModel.IsMobileNotice = IsMobileNotice;
        sellChanceModel.RemindTime = RemindTime.Length == 0 ? null : RemindTime;
        sellChanceModel.RemindMTel = RemindMTel;
        sellChanceModel.RemindContent = RemindContent;
        sellChanceModel.ReceiverID = ReceiverID.Length == 0 ? null : ReceiverID;
        
        return sellChanceModel;
    }

    private SellChancePushModel GetSellChancePushModel(string strCode, HttpContext context)
    {
        SellChancePushModel sellChancePushModel = new SellChancePushModel();

        string ChanceNo = strCode;   //销售机会编号（对应销售机会表中的机会编号）                                            
        string PushDate = context.Request.Params["PushDate"].ToString().Trim();   //日期                                                                                  
        string Seller = context.Request.Params["Seller1"].ToString().Trim();     //业务员ID(对应员工表ID)                                                                
        string Phase = context.Request.Params["Phase"].ToString().Trim();      //阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
        string State = context.Request.Params["State"].ToString().Trim();      //状态（1跟踪2成功3失败4搁置5失效）                                                     
        string Feasibility = context.Request.Params["Feasibility"].ToString().Trim();//机会可能性ID(对应分类代码表ID)                                                        
        string DelayDate = context.Request.Params["DelayDate"].ToString().Trim();  //阶段滞留时间(天)                                                                      
        string Remark = context.Request.Params["Remark1"].ToString().Trim();     //阶段备注  

        sellChancePushModel.ChanceNo = ChanceNo;//销售机会编号（对应销售机会表中的机会编号）                                            
        sellChancePushModel.PushDate = Convert.ToDateTime(PushDate);//日期                                                                                  
        sellChancePushModel.Seller = Convert.ToInt32(Seller);//业务员ID(对应员工表ID)                                                                
        sellChancePushModel.Phase = Phase;//阶段（1初期沟通、2立项评估、3需求分析、4方案指定、5招投标/竞争、6商务谈判、7合同签约）
        sellChancePushModel.State = State;//状态（1跟踪2成功3失败4搁置5失效）     
        if (Feasibility.Length != 0)
        {
            try
            {
                sellChancePushModel.Feasibility = Convert.ToInt32(Feasibility);//机会可能性ID(对应分类代码表ID)    
            }
            catch { }
        }
        if (DelayDate.Length != 0)
        {
            try
            {
                sellChancePushModel.DelayDate = Convert.ToInt32(DelayDate);//阶段滞留时间(天) 
            }
            catch { }
        }
        sellChancePushModel.Remark = Remark;//阶段备注                                                                              
        sellChancePushModel.ModifiedDate = DateTime.Now;
       
            sellChancePushModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
            sellChancePushModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
     
        return sellChancePushModel;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}