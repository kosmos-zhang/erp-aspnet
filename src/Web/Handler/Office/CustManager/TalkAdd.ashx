<%@ WebHandler Language="C#" Class="TalkAdd" %>

using System;
using System.Web;
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public class TalkAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string TalkNo = context.Request.Params["TalkNo"].ToString().Trim(); //编号
        string TalkNoType = context.Request.Params["TalkNoType"].ToString().Trim(); //编号类型

        if (Action == "1") //新建操作的时候判断唯一性
        {           
            string tableName = "CustTalk";//跟踪洽谈表
            string columnName = "TalkNo";//洽谈编号
            string codeValue = TalkNo;

            if (TalkNoType != "")
                TalkNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(TalkNoType, tableName, columnName);
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
        string CustID = context.Request.Params["CustID"].ToString().Trim();        
        string CustLinkMan = context.Request.Params["CustLinkMan"].ToString().Trim();
                
        string Linkers = context.Request.Params["Linker"].ToString().Trim();//执行人
                
        string Title = context.Request.Params["Title"].ToString().Trim();
        string Priority = context.Request.Params["Priority"].ToString().Trim();
        string TalkType = context.Request.Params["TalkType"].ToString().Trim();
        string Status = context.Request.Params["Status"].ToString().Trim();
        string CompleteDate = context.Request.Params["CompleteDate"].ToString().Trim();
        int Creator = Convert.ToInt32(context.Request.Params["Creator"].ToString().Trim());//((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//创建人ID
        string CreatedDate = context.Request.Params["CreatedDate"].ToString().Trim();
        string Contents = context.Request.Params["Contents"].ToString().Trim();//投诉内容        
        string Feedback = context.Request.Params["Feedback"].ToString().Trim();
        string Result = context.Request.Params["Result"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();//备注
        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
        string ID = context.Request.Params["ID"].ToString().Trim();

        CustTalkModel CustTalkM = new CustTalkModel();
        CustTalkM.TalkNo = TalkNo;
        CustTalkM.CompanyCD = CompanyCD;
        CustTalkM.CustID = Convert.ToInt32(CustID);
        CustTalkM.CustLinkMan = Convert.ToInt32(CustLinkMan);       
        CustTalkM.Linker = Linkers;
        CustTalkM.Title = Title;
        CustTalkM.Priority = Priority;
        CustTalkM.TalkType = Convert.ToInt32(TalkType);
        CustTalkM.Status = Status;        
        if (CompleteDate != "" || CompleteDate != string.Empty)
        {
            CustTalkM.CompleteDate = Convert.ToDateTime(CompleteDate);
        }
        
        CustTalkM.Creator = Creator;
        if (CreatedDate != "" || CreatedDate != string.Empty)
        {
            CustTalkM.CreatedDate = Convert.ToDateTime(CreatedDate);
        }

        //if (ModifiedDate != "" || ModifiedDate != string.Empty)
        //{
        //    CustTalkM.ModifiedDate = Convert.ToDateTime(ModifiedDate);
        //}
        //CustTalkM.ModifiedUserID = ModifiedUserID;
        CustTalkM.ModifiedDate = DateTime.Now;
        CustTalkM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        CustTalkM.Contents = Contents;
        CustTalkM.Feedback = Feedback;
        CustTalkM.Result = Result;
        CustTalkM.remark = Remark;
        CustTalkM.CanViewUser = CanViewUser;
        CustTalkM.CanViewUserName = CanViewUserName;
        
        if (ID != "")
            CustTalkM.ID = Convert.ToInt32(ID);

        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Talkid = TalkBus.CustTalkAdd(CustTalkM);

            if (Talkid > 0)
                jc = new JsonClass(CustTalkM.TalkNo, Talkid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
        else//修改
        {
            if (TalkBus.UpdateTalk(CustTalkM))
                jc = new JsonClass(CustTalkM.TalkNo, CustTalkM.ID.ToString(), 1);
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