<%@ WebHandler Language="C#" Class="EmployeeTest_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/08
 * 描    述： 新建考试记录
 * 修改日期： 2009/04/08
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class EmployeeTest_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //定义储存培训考核信息的Model变量
        EmployeeTestModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = EmployeeTestBus.SaveTestInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.TestNo, 1);
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
    private EmployeeTestModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        EmployeeTestModel model = new EmployeeTestModel();
        //编辑标识
        model.EditFlag = request.Params["EditFlag"].ToString();
        //获取培训考核编号
        string testNo = request.Params["TestNo"].ToString();

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(testNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                testNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_TEST
                                , ConstUtil.CODING_RULE_COLUMN_TEST_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_TEST
                                , ConstUtil.CODING_RULE_COLUMN_TEST_NO, testNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置考核编号
        model.TestNo = testNo;

        //主题
        model.Title = request.Params["Title"].ToString();
        //考试负责人 
        string teacher = request.Params["Teacher"].ToString();
        //解析获取考试负责人
        //teacher = teacher.Substring(5);
        //设置考试负责人
        model.Teacher = teacher;
        //开始时间                                                    
        model.StartDate = request.Params["StartDate"].ToString();
        //结束时间                                                  
        model.EndDate = request.Params["EndDate"].ToString();
        //考试地点                                                  
        model.Addr = request.Params["Address"].ToString();
        //考试结果                                                  
        model.TestResult = request.Params["TestResult"].ToString();
        //考试状态                                                  
        model.Status = request.Params["Status"].ToString();
        //附件
        model.Attachment = request.Params["Attachment"].ToString();
        model.PageAttachment = request.Params["PageAttachment"].ToString();
        model.AttachmentName = request.Params["AttachmentName"].ToString();
        //缺考人数
        model.AbsenceCount = request.Params["AbsenceCount"].ToString();
        //考试内容摘要                                                  
        model.TestContent = request.Params["Content"].ToString();
        //备注                                                  
        model.Remark = request.Params["Remark"].ToString();

        //设置考核结果
        string joinUserID = request.Params["JoinUserID"].ToString();
        //因为参与人员 和 考核结果必须输入，所以对考核结果记录数不做是否大于0判断
        string[] IDList = joinUserID.Split(',');
        //遍历考核结果
        for (int i = 0; i < IDList.Length; i++)
        {
            //string userID = IDList[i].Substring(5);
            string userID = IDList[i];
            if (userID == "")
                break;
            //定义考试结果Model变量
            EmployeeTestScoreModel scoreModel = new EmployeeTestScoreModel();
            //获取考试得分
            string score = request.Form["User" + userID + "Score"].ToString();
            //设置考试得分
            scoreModel.TestScore = score;
            //没有输入得分的，视作缺考
            if (string.IsNullOrEmpty(score))
            {
                scoreModel.Flag = ConstUtil.CODING_RULE_DEFAULT_FLASE;
            }
            //输入得分者视为参加考试的
            else
            {
                scoreModel.Flag = ConstUtil.CODING_RULE_DEFAULT_TRUE;
            }
            //员工ID
            scoreModel.EmployeeID = userID;

            //添加到列表中
            model.ScoreList.Add(scoreModel);
        }

        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}