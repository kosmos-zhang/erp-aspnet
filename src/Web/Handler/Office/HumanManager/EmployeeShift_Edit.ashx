<%@ WebHandler Language="C#" Class="EmployeeShift_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/25
 * 描    述： 新建调职单
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

public class EmployeeShift_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        DataTable dtEmployee = EmplApplyBus.GetEmployeeInfoWithNo(applyNo);
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
        EmplApplyNotifyModel model = new EmplApplyNotifyModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        //离职编号
        model.NotifyNo = context.Request.Params["NotifyNo"].ToString();
        //对应申请单
        model.EmplApplyNo = context.Request.Params["EmplApplyNo"].ToString();
        //被调职人
        model.EmployeeID = context.Request.Params["EmployeeID"].ToString();
        //调至部门
        model.NewDeptID = context.Request.Params["NewDept"].ToString();
        //调至岗位
        model.NewQuarterID = context.Request.Params["NewQuarter"].ToString();
        //调至岗位职等
        model.NewAdminLevel = context.Request.Params["NewQuaterAdmin"].ToString();
        //确认人
        model.Confirmor = userInfo.EmployeeID.ToString(); // context.Request.Params["Confirmor"].ToString();
        //确认日期
        model.ConfirmDate = System.DateTime.Now.ToShortDateString(); //context.Request.Params["ConfirmDate"].ToString();

        //定义Json返回变量
        JsonClass jc;
        //执行保存操作
        bool isSucce = EmplApplyNotifyBus.ConfirmEmplApplyNotifyInfo(model);
        //保存成功时
        if (isSucce)
        {
            jc = new JsonClass(System.DateTime.Now.ToShortDateString(), userInfo.EmployeeName + "," + userInfo.EmployeeID, 1);
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
        EmplApplyNotifyModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            if (string.IsNullOrEmpty(model.ID))
            {
                if (EmplApplyNotifyDBHelper.IsExistInfo(model.EmployeeID))
                {
                    jc = new JsonClass(model.ID, model.NotifyNo, 3);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }

            //执行保存操作
            bool isSucce = EmplApplyNotifyBus.SaveEmplApplyNotifyInfo(model);
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
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private EmplApplyNotifyModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        EmplApplyNotifyModel model = new EmplApplyNotifyModel();
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
                notifyNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_SHIFT
                                , ConstUtil.CODING_RULE_COLUMN_SHIFT_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_SHIFT
                                , ConstUtil.CODING_RULE_COLUMN_SHIFT_NO, notifyNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.NotifyNo = notifyNo;

        //调职单主题
        model.Title = request.Params["Title"].ToString();
        //对应申请单
        model.EmplApplyNo = request.Params["EmplApplyNo"].ToString();
        //被调职人
        model.EmployeeID = request.Params["EmployeeID"].ToString();
        //事由
        model.Reason = request.Params["Reason"].ToString();
        //原部门
        model.NowDeptID = request.Params["NowDeptID"].ToString();
        //原岗位
        model.NowQuarterID = request.Params["NowQuarterID"].ToString();
        //原岗位职等
        model.NowAdminLevel = request.Params["NowAdminLevel"].ToString();
        //调入部门
        model.NewDeptID = request.Params["NewDeptID"].ToString();
        //调入岗位
        model.NewQuarterID = request.Params["NewQuarterID"].ToString();
        //调入岗位职等
        model.NewAdminLevel = request.Params["NewAdminLevel"].ToString();
        //调出时间
        model.OutDate = request.Params["OutDate"].ToString();
        //调入时间
        model.IntDate = request.Params["IntDate"].ToString();
        //单据状态
        //model.BillStatus = request.Params["BillStatus"].ToString();
        //制单人
        model.Creator = request.Params["Creator"].ToString();
        //制单日期
        model.CreateDate = request.Params["CreateDate"].ToString();
        //确认人
        model.Confirmor = request.Params["Confirmor"].ToString();
        //确认日期
        model.ConfirmDate = request.Params["ConfirmDate"].ToString();
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