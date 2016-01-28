<%@ WebHandler Language="C#" Class="RectApply_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/27
 * 描    述： 新建招聘申请
 * 修改日期： 2009/03/27
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;
public class RectApply_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    /// <summary>
    /// 处理新建新建招聘申请的请求
    /// </summary>
    /// <param name="context">请求上下文</param>
    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Params["Action"].ToString();
        if ("FlowOperate".Equals(action))
        {
            //状态
            string status = context.Request.Params["Status"].ToString();
            //报表编号 
            string reportNo = context.Request.Params["ReportNo"].ToString();
            string doSatus = context.Request.Params["Dostatus"].ToString();
            //处理保存报表
            bool isSucc = RectApplyBus.UpdateReportStatus(status, reportNo, doSatus);
            //返回
            if (isSucc)
            {
                string EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                string BackValue = EmployeeID + "#" + EmployeeName + "#" + NowTime + "#" + UserID;
                context.Response.Write(new JsonClass("", BackValue ,1));
            }
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
        else if (action == "UpdateMoveApplyCancelConfirmInfo")
        {
            UpdateMoveApplyCancelConfirm(context);
        }
        else
        {
            //取消确认信息
            //定义储存人员信息的Model变量
            RectApplyModel model = EditRequstData(context.Request);
            //定义Json返回变量
            JsonClass jc;
            //编码能用时
            if (model != null)
            {
                //执行保存操作
                model = RectApplyBus.SaveRectApplyInfo(model);
                //保存成功时
                if (model.IsSuccess)
                {
                    jc = new JsonClass(model.ID, model.RectApplyNo, 1);
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
    private void UpdateMoveApplyCancelConfirm(HttpContext context)
    {
        //获得登录页面POST过来的参数 
        string userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//用户ID
        string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

        JsonClass jc;
        string BillStatus = "1";//单据状态

        string ID = context.Request.Params["ID"].ToString().Trim();//id
        string EmpNo = context.Request.Params["ReportNo"].ToString().Trim();//No
        //if (EmplApplyDBHelper.IfCancelEmplApplyInf(EmpNo, CompanyID))
        //{
        //    jc = new JsonClass(ID, "", 2);
        //    context.Response.Write(jc);
        //    context.Response.End();
        //}
        if (RectApplyDBHelper.UpdateMoveApplyCancelConfirm(BillStatus, ID, userID, CompanyID, EmpNo))
            jc = new JsonClass(ID, "", 1);
        else
            jc = new JsonClass(ID, "", 0);
        context.Response.Write(jc);
    }
    /// <summary>
    /// 从请求中获取申请招聘信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private RectApplyModel EditRequstData(HttpRequest request)
    {
        //定义人才代理信息Model变量
        RectApplyModel model = new RectApplyModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //编辑标识
        model.EditFlag = request.Params["EditFlag"].ToString();
        /* 获取招聘申请编号 */
        string rectApplyNo = request.Params["RectApplyNo"].ToString();

        //新建时，设置创建者信息
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //人员编号为空时，通过编码规则编号获取人员编号
            if (string.IsNullOrEmpty(rectApplyNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取人员编码
                rectApplyNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_RECTAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_RECTAPPLYNO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_RECTAPPLY
                                , ConstUtil.CODING_RULE_COLUMN_RECTAPPLYNO, rectApplyNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置人才代理编号
        model.RectApplyNo = rectApplyNo;        
        
        //部门ID
        model.DeptID = request.Params["DeptID"].ToString();
        //现有人数
        model.NowNum  = request.Params["NowNum"].ToString();
        //职位名称
        model.MaxNum  = request.Params["MaxNum"].ToString();
        //招聘人数
        model.RequireNum   = request.Params["RectCount"].ToString();
        //用人日期
        model.RequstReason  = request.Params["RequstReason"].ToString();
        //专业
        model.Remark  = request.Params["Remark"].ToString();
        //制单人
        model.Creator = userInfo.EmployeeID.ToString();
       //职务说明
        model.Principal  = request.Params["Principal"].ToString();
        model.ID = request.Params["ID"].ToString();
        model.CompanyCD = userInfo.CompanyCD;
        //设置招聘目标
        int goalCount = int.Parse(request.Params["GoalCount"]);
        //招聘目标输入时，编辑招聘目标信息
        if (goalCount > 0)
        {
            //遍历招聘目标
            for (int i = 1; i <= goalCount; i++)
            {
                //定义招聘目标Model变量
                RectApplyDetailModel goalModel = new RectApplyDetailModel();
                goalModel.CompanyCD = userInfo.CompanyCD;
                goalModel.RectApplyNo = model.RectApplyNo;
                //岗位名称
                goalModel.JobName  = request.Params["DeptQuarter_" + i].ToString();
                //岗位ID
                goalModel.JobID = request.Params["DeptQuarter" + i + "Hidden"].ToString();
                //职务说明
                goalModel.JobDescripe  = request.Params["JobDescripe_" + i].ToString();
                //人员数量
                goalModel.RectCount  = request.Params["PersonCount_" + i].ToString();
                //最迟上岗时间
                goalModel.UsedDate  = request.Params["UsedDate_" + i].ToString();
                //工作地点
                goalModel.WorkPlace  = request.Params["WorkPlace_" + i].ToString();
                //工作性质
                goalModel.WorkNature  = request.Params["WorkNature_" + i].ToString();
                //性别
                goalModel.Sex  = request.Params["Sex_" + i].ToString();
                //起始年龄
                goalModel.MinAge  = request.Params["MinAge_" + i].ToString();
                //截止年龄
                goalModel.MaxAge  = request.Params["MaxAge_" + i].ToString();
                //专业
                goalModel.Professional  = request.Params["Professional_" + i].ToString();
                //学历
                goalModel.CultureLevel  = request.Params["CultureLevel_" + i].ToString();
               //工作年限
                goalModel.WorkAge = request.Params["WorkAge_" + i].ToString();
               //工作年限
                goalModel.WorkNeeds  = request.Params["Requisition_" + i].ToString();
                //其他要求
                goalModel.OtherAbility  = request.Params["OtherAbility_" + i].ToString();
                //可提供的其他待遇
                goalModel.SalaryNote  = request.Params["SalaryNote_" + i].ToString();
              


                //添加到招聘目标中
                model.GoalList.Add(goalModel);
            }
        }
        
        
        
        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}