<%@ WebHandler Language="C#" Class="ContactHistory" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Common;
using XBase.Business.Common;

public class ContactHistory : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string ContactNo = context.Request.Params["ContactNo"].ToString().Trim(); //客户联络单编号
        string ContactNoType = context.Request.Params["ContactNoType"].ToString().Trim(); //客户联络单编号类型

        if (Action == "1") //新建操作的时候判断唯一性
        {
                string tableName = "CustContact";//联络表
                string columnName = "ContactNo";//联络单编号
                string codeValue = ContactNo;

                if (ContactNoType != "")
                    ContactNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(ContactNoType, tableName, columnName);//如果和手工录入的有重复则生成新的编码规则
                else
                {
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
                    if (!ishave)
                    {
                        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }          
        }                
       
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string CustID       = context.Request.Params["CustID"].ToString().Trim();     
        string CustLinkMan  = context.Request.Params["CustLinkMan"].ToString().Trim();
        string Title        = context.Request.Params["Title"].ToString().Trim();      
        string LinkReasonID = context.Request.Params["LinkReasonID"].ToString().Trim();  
        string LinkMode     = context.Request.Params["LinkMode"].ToString().Trim();   
        string LinkDate     = context.Request.Params["LinkDate"].ToString().Trim();   
        string Linker       = context.Request.Params["Linker"].ToString().Trim();     
        string Contents     = context.Request.Params["Contents"].ToString().Trim();

        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
        
        string ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//更新用户UserID
        string ID = context.Request.Params["id"].ToString().Trim();

        ContactHistoryModel ContactHistoryM = new ContactHistoryModel();
        ContactHistoryM.CompanyCD = CompanyCD;
        ContactHistoryM.CustID = Convert.ToInt32(CustID);
        ContactHistoryM.CustLinkMan = CustLinkMan == "" ? 0 :Convert.ToInt32(CustLinkMan);
        ContactHistoryM.ContactNo = ContactNo;//联络单编号
        ContactHistoryM.Title = Title;
        ContactHistoryM.LinkReasonID = Convert.ToInt32(LinkReasonID);
        ContactHistoryM.LinkMode = Convert.ToInt32(LinkMode);
        if (LinkDate != "" || LinkDate != string.Empty)
        {
            ContactHistoryM.LinkDate = Convert.ToDateTime(LinkDate);
        } 
        ContactHistoryM.Linker = Convert.ToInt32(Linker);
        ContactHistoryM.Contents = Contents;

        ContactHistoryM.CanViewUser = CanViewUser;
        ContactHistoryM.CanViewUserName = CanViewUserName;
        
        ContactHistoryM.ModifiedDate = DateTime.Now;
        ContactHistoryM.ModifiedUserID = ModifiedUserID;
        if (ID != "")
            ContactHistoryM.ID = Convert.ToInt32(ID); 
       
        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Contactid = ContactHistoryBus.ContactHistoryAdd(ContactHistoryM);

            if (Contactid > 0)
                jc = new JsonClass(ContactHistoryM.ContactNo, Contactid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
        else//修改
        {
            if (ContactHistoryBus.UpdateContact(ContactHistoryM))
                jc = new JsonClass(ContactHistoryM.ContactNo, ContactHistoryM.ID.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}