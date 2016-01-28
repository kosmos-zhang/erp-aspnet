<%@ WebHandler Language="C#" Class="RectPlan_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/28
 * 描    述： 新建招聘活动
 * 修改日期： 2009/03/28
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class RectPlan_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //定义储存人员信息的Model变量
        RectPlanModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = RectPlanBus.SaveRectPlanInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.PlanNo, 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass(context.Request.Params["EditFlag"], "", 0);
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
    /// 从请求中获取申请招聘信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private RectPlanModel EditRequstData(HttpRequest request)
    {
        //定义人员信息Model变量
        RectPlanModel planModel = new RectPlanModel();
        //编辑标识
        planModel.EditFlag = request.Params["EditFlag"].ToString();

        /* 获取招聘活动编号 */
        string rectPlanNo = request.Params["RectPlanNo"].ToString();

        //新建时，设置创建者信息
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(planModel.EditFlag))
        {
            //招聘活动编号为空时，通过编码规则编号获取招聘活动编号
            if (string.IsNullOrEmpty(rectPlanNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取人员编码
                rectPlanNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_RECTPLAN
                                , ConstUtil.CODING_RULE_COLUMN_RECTPLAN_PLANNO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_RECTPLAN
                                , ConstUtil.CODING_RULE_COLUMN_RECTPLAN_PLANNO, rectPlanNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置招聘活动编号
        planModel.PlanNo = rectPlanNo;
        //主题
        planModel.Title = request.Params["Title"].ToString();
        //开始时间
        planModel.StartDate = request.Params["StartDate"].ToString();
              planModel.EndDate  = request.Params["EndDate"].ToString();
              planModel.Status = request.Params["Status"].ToString();

              planModel.PlanFee = request.Params["PlanFee"].ToString();
              planModel.FeeNote = request.Params["FeeNote"].ToString();
              planModel.RequireNum  = request.Params["RequireNum"].ToString();

              planModel.JoinMan = request.Params["UserJoinMan"].ToString();
              planModel.JoinNote = request.Params["JoinNote"].ToString();
        //负责人
        string selectPrincipal = request.Params["Principal"].ToString();
        //负责人不为空时
        if (!string.IsNullOrEmpty(selectPrincipal))
        {
            //获取选择的人员ID
            int idStart = selectPrincipal.LastIndexOf("_");
            //修改了人员ID时
            if (idStart > 0)
            {
                //获取结束位置
                int idEnd = selectPrincipal.LastIndexOf("User");
                //截取人员ID
                selectPrincipal = selectPrincipal.Substring(idStart + 1, idEnd - idStart - 1);
            }
        }
        //设置人员ID
        planModel.Principal = selectPrincipal;
        
        //设置招聘目标
        int goalCount = int.Parse(request.Params["GoalCount"]);
        //招聘目标输入时，编辑招聘目标信息
        if (goalCount > 0)
        {
            //遍历招聘目标
            for (int i = 1; i <= goalCount; i++)
            {
                //定义招聘目标Model变量
                RectGoalModel goalModel = new RectGoalModel();
                //申请部门
                goalModel.ApplyDept = request.Params["ApplyDept_" + i].ToString();
                //岗位名称
                goalModel.PositionTitle = request.Params["DeptQuarter_" + i].ToString();
                //岗位ID
                goalModel.PositionID = request.Params["DeptQuarter" + i + "Hidden"].ToString();
                
                //人员数量
                goalModel.PersonCount = request.Params["PersonCount_" + i].ToString();

                goalModel.WorkAge = request.Params["WorkAge_" + i].ToString();
                //性别
                goalModel.Sex = request.Params["Sex_" + i].ToString();
                //年龄
                goalModel.Age = request.Params["Age_" + i].ToString();
                //学历
                goalModel.CultureLevel = request.Params["CultureLevel_" + i].ToString();
                //专业
                goalModel.Professional = request.Params["Professional_" + i].ToString();
                //要求
                goalModel.Requisition = request.Params["Requisition_" + i].ToString();
                //计划完成时间
                goalModel.CompleteDate = request.Params["CompleteDate_" + i].ToString();
                

                //添加到招聘目标中
                planModel.GoalList.Add(goalModel);
            }
        }
        //设置信息发布
        int publishCount = int.Parse(request.Params["PublishCount"]);
        //信息发布输入时，编辑信息发布
        if (publishCount > 0)
        {
            //遍历信息发布
            for (int i = 1; i <= publishCount; i++)
            {
                //定义信息发布Model变量
                RectPublishModel publishModel = new RectPublishModel();
                //发布媒体和渠道
                publishModel.PublishPlace = request.Params["PublishPlace_" + i].ToString();
                //发布时间
                publishModel.PublishDate = request.Params["PublishDate_" + i].ToString();
                //有效时间
                publishModel.Valid = request.Params["Valid_" + i].ToString();
                //截止时间
                publishModel.EndDate = request.Params["EndDate_" + i].ToString();
                //费用
                publishModel.Cost = request.Params["Cost_" + i].ToString();
                //效果
                publishModel.Effect = request.Params["Effect_" + i].ToString();
                //发布状态
                publishModel.Status = request.Params["Status_" + i].ToString();

                //添加到信息发布列表中
                planModel.PublishList.Add(publishModel);
            }
        }

        return planModel;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}