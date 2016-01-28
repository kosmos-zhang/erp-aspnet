<%@ WebHandler Language="C#" Class="EmplApply_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/22
 * 描    述： 新建调职申请
 * 修改日期： 2009/04/22
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;
using System.Data;

public class EmplApply_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        string Action = context.Request.Params["action"].ToString();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //定义Json返回变量
        JsonClass jc;

        if (Action == "ConfirmEmplApplyInfo")//确认
        {
            string ID = context.Request.Params["ID"].ToString();
            bool isconfrim=EmplApplyDBHelper.ConfirmEmplApplyInfo(ID, userInfo.UserName);
            if (isconfrim)
            {
                jc = new JsonClass(ID, "", 1);
            }
            else
            {
                jc = new JsonClass(ID, "", 0);
            }
            context.Response.Write(jc);
        }
        else if (Action == "GetEmplApplyInfoById") 
        {
            string ID = context.Request.Params["ID"].ToString();
            DataTable dtBaseInfo = EmplApplyBus.GetEmplApplyInfoWithID(ID);
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(dtBaseInfo));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        else if (Action == "UpdateMoveApplyCancelConfirmInfo") { UpdateMoveApplyCancelConfirm(context); }//取消确认信息
        else
        {
            //定义Model变量
            EmplApplyModel model = EditRequstData(context.Request);

            //编码能用时
            if (model != null)
            {
                //执行保存操作
                bool isSucce = EmplApplyBus.SaveEmplApplyInfo(model);
                //保存成功时
                if (isSucce)
                {
                    jc = new JsonClass(model.ID, model.EmplApplyNo, 1);
                }
                //保存未成功时
                else
                {
                    jc = new JsonClass("", "", 0);
                }
            }
            else
            {
                jc = new JsonClass("", "", 2);
            }
            //输出响应
            context.Response.Write(jc);

        }

    }
    /// <summary>
    /// 取消确认信息
    /// </summary>
    private void UpdateMoveApplyCancelConfirm(HttpContext context)
    {
        //获得登录页面POST过来的参数 
        string userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//用户ID
        string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        
        JsonClass jc;
        string BillStatus = "0";//单据状态

        string ID = context.Request.Params["ID"].ToString().Trim();//id
        string EmpNo = context.Request.Params["EmpNo"].ToString().Trim();//No
        if (EmplApplyDBHelper.IfCancelEmplApplyInf(EmpNo,CompanyID))
        {
            jc = new JsonClass(ID, "", 2);
            context.Response.Write(jc);
            context.Response.End();
        }
        if (EmplApplyDBHelper.UpdateMoveApplyCancelConfirm(BillStatus, ID, userID, CompanyID))
            jc = new JsonClass(ID, "", 1);
        else
            jc = new JsonClass(ID, "", 0);
        context.Response.Write(jc);
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private EmplApplyModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        EmplApplyModel model = new EmplApplyModel();
        //ID
        model.ID = request.Params["ID"].ToString();
        if (model.ID == "0") model.ID = "";
        //获取编号
        string emplApplyNo = request.Params["EmplApplyNo"].ToString();

        //新建时
        if (string.IsNullOrEmpty(model.ID))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(emplApplyNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                emplApplyNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_EMPLAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_EMPLAPPLY_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_EMPLAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_EMPLAPPLY_NO, emplApplyNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.EmplApplyNo = emplApplyNo;
        //主题
        model.Title = request.Params["Title"].ToString();
        //申请人
        model.EmployeeID = request.Params["EmployeeID"].ToString();
        //入职时间
        model.EnterDate = request.Params["EnterDate"].ToString();
        //申请日期
        model.ApplyDate = request.Params["ApplyDate"].ToString();
        //希望日期
        model.HopeDate = request.Params["HopeDate"].ToString();
        //申报类别
        model.ApplyType = request.Params["ApplyType"].ToString();
        //目前部门
        model.NowDeptID = request.Params["NowDeptID"].ToString();
        //目前岗位
        model.NowQuarterID = request.Params["NowQuarterID"].ToString();
        //目前岗位职等
        model.NowAdminLevelID = request.Params["NowAdminLevelID"].ToString();
        //目前工资
        model.NowWage = request.Params["NowWage"].ToString();
        //调至部门
        model.NewDeptID = request.Params["NewDeptID"].ToString();
        //调至岗位
        model.NewQuarterID = request.Params["NewQuarterID"].ToString();
        //调至岗位职等
        model.NewAdminLevelID = request.Params["NewAdminLevelID"].ToString();
        //调至工资
        model.NewWage = request.Params["NewWage"].ToString();
        //事由
        model.Reason = request.Params["Reason"].ToString();
        //备注
        model.Remark = request.Params["Remark"].ToString();       

        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}