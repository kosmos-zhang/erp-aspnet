<%@ WebHandler Language="C#" Class="ApprovalFlowSetAdd" %>

using System;
using System.Web;
using XBase.Model.Office.SystemManager;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using XBase.Common;
using System.Linq;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Linq;
using XBase.Business.Office.SystemManager;
public class ApprovalFlowSetAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        JsonClass jc;
        string action = context.Request.QueryString["action"].Trim();
        FlowModel model = new FlowModel();
        string Fno = "";
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (action=="Del")
        {
            string id = context.Request.QueryString["str"].Trim();
            string[] FlowNo = id.Split(',');
            for (int i = 0; i < FlowNo.Length; i++)
            {
                string ID = FlowNo[i].ToString();
                string[] No = ID.Split('|');
                string Fsta = No[1].ToString();
                if (Fsta != "草稿")
                {
                    jc = new JsonClass("只有流程状态为草稿时才可以删除，请检查数据", "", 2);
                    context.Response.Write(jc);
                    return;
                }
                else
                {
                    string flowflag = No[0].ToString();
                    Fno += flowflag + ",";
                }
            }
            Fno = Fno.Substring(0, Fno.Length-1);
            bool isDelete = ApprovalFlowSetBus.DeleteFlowStepInfo(Fno);
            //删除成功时
            if (isDelete)
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
            context.Response.Write(jc);
            return;
        }
        else if (action == "publish")
        {
            string PublishFlowID = context.Request.QueryString["PubFlowNo"].Trim();
            #region 发布流程
            if (ApprovalFlowSetBus.PublishFlow(ConstUtil.FlowPublishUseStatus, PublishFlowID, model.CompanyCD))
            {
                jc = new JsonClass("发布成功！", "2", 1);

            }
            else
            {
                jc = new JsonClass("发布失败！", "", 0);
            }
            #endregion
            context.Response.Write(jc);
            return;
        }
        else if (action == "stop")
        {
            string PublishFlowID = context.Request.QueryString["PubFlowNo"].Trim();
            DataTable dt = ApprovalFlowSetBus.GetFlowStatusbyFlowNo(PublishFlowID);
            int i = (int)dt.Rows[0][0];
            if (i > 0)
            {
                jc = new JsonClass("该流程已经提交审批，不能停止！", "1", 2);
                 context.Response.Write(jc);
                return;
            }
          
            #region 发布流程
            if (ApprovalFlowSetBus.PublishFlow(ConstUtil.FlowStopUseStatus, PublishFlowID, model.CompanyCD))
            {
                jc = new JsonClass("流程停止成功！", "1", 1);

            }
            else
            {
                jc = new JsonClass("流程停止失败！", "", 0);
            }
            #endregion
            context.Response.Write(jc);
            return;
        }

        string FlowID = "";
        string FlowName = context.Request.QueryString["FlowName"].Trim();
        string DeptName = context.Request.QueryString["DeptName"].Trim();
        string BillType = context.Request.QueryString["BillType"].Trim();
        string typecode = context.Request.QueryString["BillTypeName"].Trim();
        string UsedStatus = context.Request.QueryString["status"].Trim();
        string IsMobileNotice = context.Request.QueryString["IsMobileNotice"].Trim();
       
        model.BillTypeCode =int.Parse(typecode);
        model.BillTypeFlag = int.Parse(BillType);
        if (string.IsNullOrEmpty(DeptName))
        {
            model.DeptID = 0;
        }
        else 
        {
            model.DeptID = int.Parse(DeptName);
        }
        model.FlowNo = FlowID;
        model.FlowName = FlowName;
        model.UsedStatus = UsedStatus;
        model.IsMobileNotice = IsMobileNotice;/*是否发送手机短信*/
        model.ModifiedDate = System.DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        if (action == "Add")
        {
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            if (!string.IsNullOrEmpty(CodeType))
            {
                FlowID = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(CodeType, "Flow", "FlowNo");
            }
            else
            {
                FlowID = context.Request.QueryString["FlowNo"].Trim();//合同编号  
            }
            bool isAlready = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq("Flow"
                          , "FlowNo", FlowID);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("流程编号已经存在", "", 0);
                context.Response.Write(jc);
                return;
            }
            model.FlowNo = FlowID;
            #region 添加流程信息
            if (ApprovalFlowSetBus.InsertFlowInfo(model, context.Request.QueryString["DetailStepNo"].ToString().Trim(), context.Request.QueryString["DetailStepName"].ToString().Trim(),context.Request.QueryString["DetailActor"].ToString().Trim()))
            {
                jc = new JsonClass("保存成功", model.FlowNo, 1);
                
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);
            #endregion
        }
        else if (action == "Edit")
        { 
            FlowID = context.Request.QueryString["FlowNo"].Trim();
            model.FlowNo = FlowID;
            #region 修改流程信息
            if (ApprovalFlowSetBus.UpdateFlowInfo(model, context.Request.QueryString["DetailStepNo"].ToString().Trim(), context.Request.QueryString["DetailStepName"].ToString().Trim(), context.Request.QueryString["DetailActor"].ToString().Trim()))
            {
                jc = new JsonClass("保存成功", "", 1);

            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);
            #endregion
        }
    
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}