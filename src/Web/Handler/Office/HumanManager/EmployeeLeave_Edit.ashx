<%@ WebHandler Language="C#" Class="EmployeeLeave_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/25
 * 描    述： 新建离职单
 * 修改日期： 2009/04/25
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;

public class EmployeeLeave_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取操作类型
        string action = context.Request.Params["Action"].ToString();
        //保存
        if ("Save".Equals(action))
        {
            DoSave(context);
        }
        //确认操作
        else if ("Confirm".Equals(action))
        {
            DoConfirm(context);
        }
        //获取申请单对应的员工信息
        else if ("Apply".Equals(action))
        {
            DoApply(context);
        }
    }

    /// <summary>
    /// 获取员工信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoApply(HttpContext context)
    {
        //离职编号
        string applyNo = context.Request.Params["ApplyNo"].ToString();

        //定义Json返回变量
        //执行查询操作
        DataTable dtEmployee = MoveApplyBus.GetEmployeeInfoWithNo(applyNo);
        //定义放回变量
        StringBuilder sbReturn = new StringBuilder();
        sbReturn.Append("{");
        sbReturn.Append("data:");
        sbReturn.Append(JsonClass.DataTable2Json(dtEmployee));
        sbReturn.Append("}");
        context.Response.ContentType = "text/plain";
        //返回数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoConfirm(HttpContext context)
    {
        //定义Model变量
        MoveNotifyModel model = new MoveNotifyModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //离职编号
        model.NotifyNo = context.Request.Params["NotifyNo"].ToString();
        //对应申请单
        model.MoveApplyNo = context.Request.Params["MoveApplyNo"].ToString();
        //离职人
        model.EmployeeID = context.Request.Params["EmployeeID"].ToString();
        //确认人
        model.Confirmor = userInfo.EmployeeID.ToString();// context.Request.Params["Confirmor"].ToString();
        //确认日期
        model.ConfirmDate =System.DateTime.Now.ToShortDateString();// context.Request.Params["ConfirmDate"].ToString();
        //定义Json返回变量
        JsonClass jc;
        //执行保存操作
        bool isSucce = MoveNotifyBus.ConfirmMoveNotifyInfo(model);
        //保存成功时
        if (isSucce)
        {
            jc = new JsonClass(System.DateTime.Now.ToShortDateString(), userInfo.EmployeeName +","+ userInfo.EmployeeID, 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }
        //输出响应
        context.Response.Write(jc);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoSave(HttpContext context)
    {
        //定义Model变量
        MoveNotifyModel model = EditRequstData(context.Request);
        JsonClass jc;
        
        //定义Json返回变量
        //编码能用时
        if (model != null)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                if (MoveNotifyDBHelper.IsExistInfo(model.EmployeeID))
                {
                    jc = new JsonClass(model.ID, model.NotifyNo, 3);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            //执行保存操作
            bool isSucce = MoveNotifyBus.SaveMoveNotifyInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(model.ID, model.NotifyNo, 1);
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

    /// <summary>
    /// 从请求中获取信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private MoveNotifyModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        MoveNotifyModel model = new MoveNotifyModel();
        //ID
        model.ID = request.Params["ID"].ToString();
        //获取编号
        string notifyNo = request.Params["NotifyNo"].ToString();

        //新建时
        if (string.IsNullOrEmpty(model.ID))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(notifyNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                notifyNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_LEAVE
                                , ConstUtil.CODING_RULE_COLUMN_LEAVE_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_LEAVE
                                , ConstUtil.CODING_RULE_COLUMN_LEAVE_NO, notifyNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.NotifyNo = notifyNo;

        //离职单主题
        model.Title = request.Params["Title"].ToString();
        //对应申请单
        model.MoveApplyNo = request.Params["MoveApplyNo"].ToString();
        //离职人
        model.EmployeeID = request.Params["EmployeeID"].ToString();
        //离职事由
        model.Reason = request.Params["Reason"].ToString();
        //离职时间
        model.OutDate = request.Params["OutDate"].ToString();
        //离职交接说明
        model.JobNote = request.Params["JobNote"].ToString();
        //单据状态
        //model.BillStatus = request.Params["BillStatus"].ToString();
        //制单人
        model.Creator = request.Params["Creator"].ToString();
        //制单日期
        model.CreateDate = request.Params["CreateDate"].ToString();
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