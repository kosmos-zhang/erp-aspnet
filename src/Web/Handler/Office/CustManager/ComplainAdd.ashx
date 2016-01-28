<%@ WebHandler Language="C#" Class="ComplainAdd" %>

using System;
using System.Web;
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager; 
using XBase.Business.Office.CustManager;

public class ComplainAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string ComplainNo = context.Request.Params["ComplainNo"].ToString().Trim().Trim(); //客户投诉单编号
        string ComplainNoType = context.Request.Params["ComplainNoType"].ToString().Trim(); //客户投诉单编号类型

        if (Action == "1") //新建操作的时候判断唯一性
        {            
                string tableName = "CustComplain";//投诉表
                string columnName = "ComplainNo";//投诉单编号
                string codeValue = ComplainNo;

                if (ComplainNoType != "")
                    ComplainNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(ComplainNoType, tableName, columnName);
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
        string DestClerk = context.Request.Params["DestClerk"].ToString().Trim();
        string Title = context.Request.Params["Title"].ToString().Trim();
        string CustLinkTel = context.Request.Params["CustLinkTel"].ToString().Trim();
        string ComplainType = context.Request.Params["ComplainType"].ToString().Trim();
        string Critical = context.Request.Params["Critical"].ToString().Trim();        
        string State = context.Request.Params["State"].ToString().Trim();
        string SpendTime = context.Request.Params["SpendTime"].ToString().Trim() == "" ? "0" : context.Request.Params["SpendTime"].ToString().Trim();
        string DateUnit = context.Request.Params["DateUnit"].ToString().Trim();
        string ComplainDate = context.Request.Params["ComplainDate"].ToString().Trim();
        //string ModifiedDate = context.Request.Params["ModifiedDate"].ToString().Trim();
        //string ModifiedUserID = context.Request.Params["ModifiedUserID"].ToString().Trim();//更新用户UserID
        string Contents = context.Request.Params["Contents"].ToString().Trim();//投诉内容
        string ComplainedMan = context.Request.Params["ComplainedMan"].ToString().Trim();//被投诉人
        string DisposalProcess = context.Request.Params["DisposalProcess"].ToString().Trim();
        string Feedback = context.Request.Params["Feedback"].ToString().Trim();       
        string Remark = context.Request.Params["Remark"].ToString().Trim();//备注
        string ID = context.Request.Params["ID"].ToString().Trim();
        string CanViewUser = "," + context.Request.Params["CanViewUser"].ToString().Trim() + ",";
        string CanViewUserName = context.Request.Params["CanViewUserName"].ToString().Trim();
        
        CustComplainModel CustComplainM = new CustComplainModel();
        CustComplainM.ComplainNo = ComplainNo;
        CustComplainM.CompanyCD = CompanyCD;
        CustComplainM.CustID = Convert.ToInt32(CustID);
        CustComplainM.CustLinkMan = Convert.ToInt32(CustLinkMan);
        CustComplainM.DestClerk = Convert.ToInt32(DestClerk);
        CustComplainM.Title = Title;
        CustComplainM.CustLinkTel = CustLinkTel;
        CustComplainM.ComplainType = Convert.ToInt32(ComplainType);
        CustComplainM.Critical = Critical;
        CustComplainM.State = State;
        CustComplainM.SpendTime = Convert.ToDecimal(SpendTime);
        CustComplainM.DateUnit = DateUnit;
        if (ComplainDate != "" || ComplainDate != string.Empty)
        {
            CustComplainM.ComplainDate = Convert.ToDateTime(ComplainDate);
        }
        CustComplainM.ModifiedDate = DateTime.Now;
        CustComplainM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        CustComplainM.Contents = Contents;
        CustComplainM.ComplainedMan = ComplainedMan == "" ? 0 : Convert.ToInt32(ComplainedMan);
        CustComplainM.DisposalProcess = DisposalProcess;
        CustComplainM.Feedback = Feedback;
        CustComplainM.Remark = Remark;
        CustComplainM.CanViewUser = CanViewUser;
        CustComplainM.CanViewUserName = CanViewUserName;
        if (ID != "")
            CustComplainM.ID = Convert.ToInt32(ID);
        
       
        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Complainid = ComplainBus.CustComplainAdd(CustComplainM);

            if (Complainid > 0)
                jc = new JsonClass(CustComplainM.ComplainNo, Complainid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
        else//修改
        {
            if (ComplainBus.UpdateComplain(CustComplainM))
                jc = new JsonClass(CustComplainM.ComplainNo, CustComplainM.ID.ToString(), 1);
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