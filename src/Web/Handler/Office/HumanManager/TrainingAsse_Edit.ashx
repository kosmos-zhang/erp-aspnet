<%@ WebHandler Language="C#" Class="TrainingAsse_Edit" %>
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

public class TrainingAsse_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //定义储存培训考核信息的Model变量
        TrainingAsseModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = TrainingAsseBus.SaveTrainingAsseInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.AsseNo, 1);
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
    private TrainingAsseModel EditRequstData(HttpRequest request)
    {
        //定义培训信息Model变量
        TrainingAsseModel asseModel = new TrainingAsseModel();
        //编辑标识
        asseModel.EditFlag = request.Params["EditFlag"].ToString().Trim();
        //获取培训考核编号
        string trainingAsseNo = request.Params["TrainingAsseNo"].ToString().Trim();

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(asseModel.EditFlag))
        {
            //培训编号为空时，通过编码规则编号获取培训编号
            if (string.IsNullOrEmpty(trainingAsseNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString().Trim();
                //通过编码规则代码获取培训编号
                trainingAsseNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_TRAININGASSE
                                , ConstUtil.CODING_RULE_COLUMN_TRAININGASSE_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_TRAININGASSE
                                , ConstUtil.CODING_RULE_COLUMN_TRAININGASSE_NO, trainingAsseNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置考核编号
        asseModel.AsseNo = trainingAsseNo;
        
        //培训编号
        asseModel.TrainingNo = request.Params["TrainingNo"].ToString().Trim();
        //填写人                                                    
        asseModel.CheckPerson = request.Params["CheckPerson"].ToString().Trim();
        //填写人                                                    
        asseModel.FillUser = request.Params["FillUserID"].ToString().Trim();
        //考核方式                                                  
        asseModel.CheckWay = request.Params["CheckWay"].ToString().Trim();
        //考核时间                                                  
        asseModel.CheckDate = request.Params["CheckDate"].ToString().Trim();
        //培训规划                                                  
        asseModel.TrainingPlan = request.Params["TrainingPlan"].ToString().Trim();
        //领导意见                                                  
        asseModel.LeadViews = request.Params["LeadViews"].ToString().Trim();
        //说明                                                      
        asseModel.Description = request.Params["Description"].ToString().Trim();
        //考核总评                                                  
        asseModel.GeneralComment = request.Params["GeneralComment"].ToString().Trim();
        //考核备注                                                  
        asseModel.CheckRemark = request.Params["CheckRemark"].ToString().Trim();

        //设置考核结果
        int resultCount = int.Parse(request.Params["ResultCount"].ToString());
        //因为参与人员 和 考核结果必须输入，所以对考核结果记录数不做是否大于0判断
        //遍历考核结果
        for (int i = 0; i < resultCount; i++)
        {
            //定义考核结果Model变量
            TrainingDetailModel scoreModel = new TrainingDetailModel();
            //员工ID
            scoreModel.EmployeeID = request.Params["JoinUserID_" + i].ToString().Trim();
            //考核分数
            scoreModel.AssessScore = request.Params["AsseScore_" + i].ToString().Trim();
            //考核等级
            scoreModel.AssessLevel = request.Params["AsseLevel_" + i].ToString().Trim();

            //添加到考核结果列表中
            asseModel.ResultList.Add(scoreModel);
        }

        return asseModel;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}