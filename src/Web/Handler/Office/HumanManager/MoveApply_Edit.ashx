<%@ WebHandler Language="C#" Class="MoveApply_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/23
 * 描    述： 新建离职申请
 * 修改日期： 2009/04/23
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

public class MoveApply_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string Action = context.Request.Params["action"].ToString();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        JsonClass jc;

        if (Action == "GetMoveApplyInfoById")
        {
            string ID = context.Request.Params["ID"].ToString();
            DataTable dtBaseInfo = MoveApplyBus.GetMoveApplyInfoWithID(ID);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(dtBaseInfo));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        else if (Action == "ConfirmMoveApplyInfo")//确认
        {
            string ID = context.Request.Params["ID"].ToString();
            bool isconfrim = MoveApplyDBHelper.ConfirmMoveApplyInfo(ID, userInfo.UserName);
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
        else if (Action == "UpdateMoveApplyCancelConfirmInfo") { UpdateMoveApplyCancelConfirmInfo(context); }//取消确认信息
        else
        {
            //定义Model变量
            MoveApplyModel model = EditRequstData(context.Request);
            //定义Json返回变量
            //编码能用时
            if (model != null)
            {
                //执行保存操作
                bool isSucce = MoveApplyBus.SaveMoveApplyInfo(model);
                //保存成功时
                if (isSucce)
                {
                    jc = new JsonClass(model.ID, model.MoveApplyNo, 1);
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
    private void UpdateMoveApplyCancelConfirmInfo(HttpContext context)
    {
        //获得登录页面POST过来的参数 
        string userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//用户ID
        string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

        JsonClass jc;
        string BillStatus = "0";//单据状态

        string ID = context.Request.Params["ID"].ToString().Trim();//id
        string EmpNo = context.Request.Params["EmpNo"].ToString().Trim();//No
        if (MoveApplyDBHelper.IfCancelMoveApplyInf(EmpNo, CompanyID))
        {
            jc = new JsonClass(ID, "", 2);
            context.Response.Write(jc);
            context.Response.End();
        }
        if (MoveApplyDBHelper.UpdateMoveApplyCancelConfirm(BillStatus, ID, userID, CompanyID))
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
    private MoveApplyModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        MoveApplyModel model = new MoveApplyModel();
        //ID
        model.ID = request.Params["ID"].ToString();
        if (model.ID == "0") model.ID = "";
        //获取编号
        string moveApplyNo = request.Params["MoveApplyNo"].ToString();

        //新建时
        if (string.IsNullOrEmpty(model.ID))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(moveApplyNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                moveApplyNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_MOVEAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_MOVEAPPLY_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_MOVEAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_MOVEAPPLY_NO, moveApplyNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.MoveApplyNo = moveApplyNo;
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
        //目前部门
        model.DeptID = request.Params["DeptID"].ToString();
        //目前岗位
        model.QuarterID = request.Params["QuarterID"].ToString();
        //合同有效期
        model.ContractValid = request.Params["ContractValid"].ToString();
        //通知离职日期
        model.MoveDate = request.Params["MoveDate"].ToString();
        //调至岗位
        model.MoveType = request.Params["MoveType"].ToString();
        //访谈记录
        model.Interview = request.Params["Interview"].ToString();
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