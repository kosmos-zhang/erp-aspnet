<%@ WebHandler Language="C#" Class="Training_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/02
 * 描    述： 新建培训
 * 修改日期： 2009/04/02
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class Training_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //定义储存培训信息的Model变量
        TrainingModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = TrainingBus.SaveTrainingPlanInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TrainingNo, 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass(model.EditFlag, "", 0);
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
    private TrainingModel EditRequstData(HttpRequest request)
    {
        //定义培训信息Model变量
        TrainingModel trainModel = new TrainingModel();

        //获取培训ID
        trainModel.EditFlag = request.Params["EditFlag"].ToString().Trim();
        //获取培训编号
        string trainingNo = request.Params["TrainingNo"].ToString().Trim();

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(trainModel.EditFlag))
        {
            //培训编号为空时，通过编码规则编号获取培训编号
            if (string.IsNullOrEmpty(trainingNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString().Trim();
                //通过编码规则代码获取培训编号
                trainingNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_TRAINING
                                , ConstUtil.CODING_RULE_COLUMN_TRAINING_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_TRAINING
                                , ConstUtil.CODING_RULE_COLUMN_TRAINING_NO, trainingNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置培训编号
        trainModel.TrainingNo = trainingNo;
        //培训名称
        trainModel.TrainingName = request.Params["TrainingName"].ToString().Trim();
        //发起时间
        trainModel.ApplyDate = request.Params["ApplyDate"].ToString().Trim();
        //发起人
        string createID = request.Params["CreateID"].ToString().Trim();
        //发起人不为空时
        if (!string.IsNullOrEmpty(createID))
        {
            //获取选择的人员ID
            int idStart = createID.LastIndexOf("_");
            //修改了人员ID时
            if (idStart > 0)
            {
                //截取人员ID
                createID = createID.Substring(idStart + 1);
            }
        }
        //设置发起人ID
        trainModel.EmployeeID = createID;
        //项目编号
        trainModel.ProjectNo = request.Params["ProjectNo"].ToString().Trim();
        //项目名称
        trainModel.ProjectName = request.Params["ProjectName"].ToString().Trim();
        //培训机构
        trainModel.TrainingOrgan = request.Params["TrainingOrgan"].ToString().Trim();
        //预算费用
        trainModel.PlanCost = request.Params["PlanCost"].ToString().Trim();
        //培训天数
        trainModel.TrainingCount = request.Params["TrainingCount"].ToString().Trim();
        //培训地点 
        trainModel.TrainingPlace = request.Params["TrainingPlace"].ToString().Trim();
        //培训方式
        trainModel.TrainingWay = request.Params["TrainingWay"].ToString().Trim();
        //培训老师
        trainModel.TrainingTeacher = request.Params["TrainingTeacher"].ToString().Trim();
        //开始时间
        trainModel.StartDate = request.Params["StartDate"].ToString().Trim();
        //结束时间
        trainModel.EndDate = request.Params["EndDate"].ToString().Trim();
        //考核人
        trainModel.CheckPerson = request.Params["CheckPerson"].ToString().Trim();
        //附件
        trainModel.Attachment = request.Params["Attachment"].ToString().Trim();
        trainModel.PageAttachment = request.Params["PageAttachment"].ToString().Trim();
        trainModel.AttachmentName = request.Params["AttachmentName"].ToString().Trim();
        //目的
        trainModel.Goal = request.Params["Goal"].ToString().Trim();
        //培训备注
        trainModel.TrainingRemark = request.Params["TrainingRemark"].ToString().Trim();
        
        //获取参与人员
        string joinUser = request.Params["JoinUser"].ToString().Trim();
        //获取参与人员列表
        string[] joinUserList = joinUser.Split(',');
        //遍历，设置参与人员  参与人员页面上必须输入，因此不做非空等判断
        for (int i = 0; i < joinUserList.Length; i++)
        {
            //定义参与人Model变量
            TrainingUserModel userModel = new TrainingUserModel();
            //获取参与人ID
            string inputID = joinUserList[i];
            
            //参加人ID
           // int start = inputID.IndexOf(",");
            userModel.JoinID = inputID;

            //添加到招聘目标中
            trainModel.UserList.Add(userModel);
        }
        
        //设置进度安排
        int scheduleCount = int.Parse(request.Params["ScheduleCount"].ToString().Trim());
        //进度安排输入时，编辑进度安排
        if (scheduleCount > 0)
        {
            //遍历进度安排
            for (int i = 1; i <= scheduleCount; i++)
            {
                //定义进度安排Model变量
                TrainingScheduleModel scheduleModel = new TrainingScheduleModel();
                //进度时间
                scheduleModel.ScheduleDate = request.Params["ScheduleDate_" + i].ToString().Trim();
                //内容摘要
                scheduleModel.Abstract = request.Params["ScheduleAbstract_" + i].ToString().Trim();
                //备注
                scheduleModel.Remark = request.Params["Remark_" + i].ToString().Trim();

                //添加到进度安排列表中
                trainModel.ScheduleList.Add(scheduleModel);
            }
        }

        return trainModel;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}