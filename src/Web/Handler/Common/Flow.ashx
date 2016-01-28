<%@ WebHandler Language="C#" Class="Flow" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Common;
using System.Data;
using System.Text;
using XBase.Business.Office.SystemManager;

public class Flow : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        string companyCD = string.Empty;
        string loginUserID = string.Empty;
        int loginEmployeeID = 0;
        
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        loginEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        
        string Action = context.Request.Params["Action"].ToString().Trim();
        
        
        
        int BillTypeFlag = int.Parse(context.Request.QueryString["BillTypeFlag"].ToString().Trim());
        int BillTypeCode = int.Parse(context.Request.QueryString["BillTypeCode"].ToString().Trim());
        int BillID = int.Parse(context.Request.QueryString["BillID"].ToString().Trim());
        string ModifiedUserID = loginUserID;
        
        JsonClass jc;
        if (Action == ActionUtil.Add.ToString())//提交审批
        {
            string FlowNo = context.Request.QueryString["FlowNo"].ToString().Trim();
            string PageUrl = context.Request.QueryString["PageUrl"].ToString().Trim();
            string BillNo = context.Request.QueryString["BillNo"].ToString().Trim();
            string ApplyUserID = loginUserID;
            int MsgRemind = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["MsgRemind"].ToString().Trim()))
            {
                MsgRemind = int.Parse(context.Request.QueryString["MsgRemind"].ToString().Trim());
            }

            string strRetVal = FlowBus.FlowApplyAdd(companyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID, BillNo, PageUrl, ApplyUserID, ModifiedUserID, MsgRemind);
            if (!String.IsNullOrEmpty(strRetVal))
            {
                string[] retArray = strRetVal.Split('|');
                jc = new JsonClass(retArray[1].ToString(), "", int.Parse(retArray[0].ToString()));
            }
            else
            {
                jc = new JsonClass("提交审批失败", "", 0);
            }
        }
        else if (Action == ActionUtil.Edit.ToString())//审批
        {
            string FlowNo = context.Request.QueryString["FlowNo"].ToString().Trim();
            string BillNo = context.Request.QueryString["BillNo"].ToString().Trim();
            string State = context.Request.QueryString["State"].ToString().Trim();
            string Note = context.Request.QueryString["Note"].ToString().Trim();
            int MsgRemind = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["MsgRemind"].ToString().Trim()))
            {
                MsgRemind = int.Parse(context.Request.QueryString["MsgRemind"].ToString().Trim());
            }

            string strRetVal = FlowBus.FlowApproveAdd(companyCD, FlowNo, BillTypeFlag, BillTypeCode, BillID,BillNo, State, Note, ModifiedUserID,loginEmployeeID,MsgRemind);
            if (!String.IsNullOrEmpty(strRetVal))
            {
                string[] retArray = strRetVal.Split('|');
                jc = new JsonClass(retArray[1].ToString(), "", int.Parse(retArray[0].ToString()));
            }
            else
            {
                jc = new JsonClass("提交审批失败", "", 0);
            }
        }
        else if (Action == "GetStep")//流程步骤
        {
            DataTable db = FlowBus.GetFlowStep(companyCD, BillTypeFlag, BillTypeCode, BillID);
            StringBuilder sbStep = new StringBuilder();
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    sbStep.Append("" + db.Rows[i]["StepNo"].ToString() + "." + db.Rows[i]["StepName"].ToString() + "<br>");
                }
                jc = new JsonClass(sbStep.ToString(), "", db.Rows.Count);
            }
            else
            {
                jc = new JsonClass("未找到流程步骤", "", 0);
            }
            
        }
        else if(Action == "GetSet")//是否设置了流程
        {
            int UsedStatus = 0;
            UsedStatus = ApprovalFlowSetBus.GetFlowUsedStatus(companyCD, BillTypeFlag, BillTypeCode);
            jc = new JsonClass("流程使用状态", "", UsedStatus);
            
        }
        else if (Action == "GetFlow")//获取流程
        {
            int intSure = int.Parse(context.Request.QueryString["intSure"].ToString().Trim());
            string sbFlow = "";
            DataSet ds = XBase.Business.Common.FlowBus.GetFlow(companyCD, BillTypeFlag, BillTypeCode, BillID);
            if (ds.Tables.Count > 0)
            {
                string strTypeName = "";
                if (ds.Tables[intSure].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[intSure].Rows.Count; i++)
                    {
                        //sbFlow = sbFlow + "<option value=\"" + ds.Tables[intSure].Rows[i]["FlowNo"].ToString() + "\">" + ds.Tables[intSure].Rows[i]["FlowName"].ToString() + "</option>";
                        sbFlow = sbFlow + ds.Tables[intSure].Rows[i]["FlowNo"].ToString() + "#" + ds.Tables[intSure].Rows[i]["IsMobileNotice"].ToString() + "," + ds.Tables[intSure].Rows[i]["FlowName"].ToString() + "|";
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    strTypeName = ds.Tables[2].Rows[0]["TypeName"].ToString();
                }
                jc = new JsonClass(sbFlow, strTypeName, ds.Tables[intSure].Rows.Count);

            }
            else
            {
                jc = new JsonClass("没有找到流程", "", 0);
            }
            
        }
        else if (Action == "Cancel")//撤消审批
        {
            string strRetVal = FlowBus.FlowApproveUpdate(companyCD,BillTypeFlag, BillTypeCode, BillID,ModifiedUserID, loginEmployeeID);
            if (!String.IsNullOrEmpty(strRetVal))
            {
                string[] retArray = strRetVal.Split('|');
                jc = new JsonClass(retArray[1].ToString(), "", int.Parse(retArray[0].ToString()));
            }
            else
            {
                jc = new JsonClass("撤消审批失败", "", 0);
            }
        }
        else if (Action == "Authority")
        {
            string strRetVal = FlowBus.GetFlowApprovalAuthority(companyCD,BillTypeFlag, BillTypeCode, BillID, loginEmployeeID);
            if (!String.IsNullOrEmpty(strRetVal))
            {
                string[] retArray = strRetVal.Split('|');
                jc = new JsonClass(retArray[1].ToString(), "", int.Parse(retArray[0].ToString()));
            }
            else
            {
                jc = new JsonClass("没有权限审批!", "", 0);
            }
        }
        else  //单据状态
        {
            //1：待审批 2：审批中 3：审批通过 4：审批不通过
            int intBillStatus = FlowBus.GetBillUsedStatus(companyCD, BillTypeFlag, BillTypeCode, BillID);
            jc = new JsonClass("单据状态", "", intBillStatus);
        }
            
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}