<%@ WebHandler Language="C#" Class="RectInterview_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/16
 * 描    述： 面试模板
 * 修改日期： 2009/04/16
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
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Linq;

public class RectInterview_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //获取操作
        string action = context.Request.Params["Action"].ToString();
        //获取面试要素 
        if ("InitElem".Equals(action))
        {
            //获取要素信息
            GetElemInfo(context);
        }
        else if ("SaveInfo".Equals(action))
        {
            //保存面试记录信息
            SaveInterviewInfo(context);
        }
        else if ("InitTemplate".Equals(action))
        {
            //保存面试记录信息
            InitTemplateInfo(context);
        }
        else if ("InitQuter".Equals(action))
        {
            //保存面试记录信息
            InitQuter(context);
        }
    }


    private void InitQuter(HttpContext context)
    {
        //获取岗位ID
        string planNo = context.Request.Params["QuarterID"].ToString();

        //查询面试要素
        DataTable dtTemplate = null;
        if (planNo == "Special")
        {
            dtTemplate = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
        }
        else
        {
            dtTemplate = RectInterviewBus.GetQuterInfo(planNo); 
        }
         
        StringBuilder sbTemplateInfo = new StringBuilder();
        //数据存在的时候
        if (dtTemplate != null && dtTemplate.Rows.Count > 0)
        {
            //遍历所有模板信息
            for (int i = 0; i < dtTemplate.Rows.Count; i++)
            {
                string no = string.Empty;
                string title = string.Empty;
                if (planNo == "Special")
                {
                    //获取岗位编号
                    no = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "ID");
                    //获取岗位主题
                    title = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "QuarterName");
                }
                else
                {
                    //获取岗位编号
                      no = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "PositionID");
                    //获取岗位主题
                      title = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "PositionTitle");
                }
               

                if (i == 0)
                {
                    sbTemplateInfo.AppendLine("<option value=\"" + no + "\" selected=\"selected\">" + title + "</option>");
                }
                else
                {
                    sbTemplateInfo.AppendLine("<option value=\"" + no + "\">" + title + "</option>");
                }
            }
        }
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbTemplateInfo.ToString());
        context.Response.End();
    }
    
    
    /// <summary>
    /// 查询模板信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void InitTemplateInfo(HttpContext context)
    {
        //获取岗位ID
        string quarterID = context.Request.Params["QuarterID"].ToString();

        //查询面试要素
        DataTable dtTemplate = RectInterviewBus.GetTemplateInfo(quarterID);
        StringBuilder sbTemplateInfo = new StringBuilder();
        //数据存在的时候
        if (dtTemplate != null && dtTemplate.Rows.Count > 0)
        {
            //遍历所有模板信息
            for (int i = 0; i < dtTemplate.Rows.Count; i++)
            {
                //获取模板编号
                string no = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "TemplateNo");
                //获取模板主题
                string title = GetSafeData.ValidateDataRow_String(dtTemplate.Rows[i], "Title");
                
                if (i == 0)
                {
                    sbTemplateInfo.AppendLine("<option value=\"" + no + "\" selected=\"selected\">" + title + "</option>");
                }
                else
                {
                    sbTemplateInfo.AppendLine("<option value=\"" + no + "\">" + title + "</option>");
                }
            }
        }
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbTemplateInfo.ToString());
        context.Response.End();
    }
    
    /// <summary>
    /// 保存面试记录信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SaveInterviewInfo(HttpContext context)
    {
        //定义储存培训考核信息的Model变量
        RectInterviewModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = RectInterviewBus.SaveInterviewInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(model.ID, model.InterviewNo, 1);
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
    private RectInterviewModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        RectInterviewModel model = new RectInterviewModel();
        //面试记录ID
        model.ID = request.Params["InterviewID"].ToString();
        //获取编号
        string interviewNo = request.Params["InterviewNo"].ToString();

        //新建时
        if (string.IsNullOrEmpty(model.ID))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(interviewNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                interviewNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_RECTINTERVIEW
                                , ConstUtil.CODING_RULE_COLUMN_INTERVIEW_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_RECTINTERVIEW
                                , ConstUtil.CODING_RULE_COLUMN_INTERVIEW_NO, interviewNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.InterviewNo = interviewNo;

        //招聘活动
        model.PlanID = request.Params["PlanID"].ToString();
        //面试者
        model.StaffName = request.Params["StaffName"].ToString();
        //消息渠道
        model.RectType = request.Params["RectType"].ToString();
        //应聘岗位 
        model.QuarterID = request.Params["QuarterID"].ToString();
        model.TemplateNo = request.Params["TemplateNo"].ToString();

        //初试日期
        model.InterviewDate = request.Params["InterviewDate"].ToString();
        //初试面试方式
        model.InterviewType  = request.Params["InterviewType"].ToString();
        //初试面试地点
        model.InterviewPlace  = request.Params["InterviewPlace"].ToString();
        //初试人员意见
        model.InterviewNote  = request.Params["InterviewNote"].ToString();
        //初试结果
        model.InterviewResult  = request.Params["InterviewResult"].ToString();


        //复试日期
        model.CheckDate  = request.Params["CheckDate"].ToString();
        ////复试方式
        model.CheckType  = request.Params["CheckType"].ToString();
        //复试地点

        model.CheckPlace  = request.Params["CheckPlace"].ToString();
        //复试人员意见
        model.CheckNote  = request.Params["CheckNote"].ToString();
        //复试结果
        model.FinalResult  = request.Params["FinalResult"].ToString();

        //综合素质
        model.ManNote  = request.Params["ManNote"].ToString();
        //专业知识
        model.KnowNote  = request.Params["KnowNote"].ToString();
        //工作经验
        model.WorkNote  = request.Params["WorkNote"].ToString();
        //要求待遇
        model.SalaryNote  = request.Params["SalaryNote"].ToString();

        //可提供的待遇
        model.OurSalary = request.Params["OurSalary"].ToString();
        //确认工资
        model.FinalSalary  = request.Params["FinalSalary"].ToString();
        //其他方面
        model.OtherNote = request.Params["OtherNote"].ToString();
 
        //附件
        model.Attachment = request.Params["Attachment"].ToString();
        model.PageAttachment = request.Params["PageAttachment"].ToString();
        model.AttachmentName = request.Params["AttachmentName"].ToString();
        //复试备注
        model.Remark = request.Params["Remark"].ToString();
        //面试总成绩
        if (request.Params["TestScore"] != null)
        {
            model.TestScore = request.Params["TestScore"].ToString();
        }

        if (request.Params["ScoreCount"] != null)
        {
            //设置要素得分
            int scoreCount = int.Parse(request.Params["ScoreCount"].ToString());
            if (scoreCount > 0)
            {
                //遍历要素得分
                for (int i = 1; i <= scoreCount; i++)
                {
                    //定义Model变量
                    RectInterviewDetailModel elemModel = new RectInterviewDetailModel();
                    //要素ID
                    elemModel.CheckElemID = request.Params["ElemScoreID_" + i].ToString();
                    //面试得分
                    elemModel.RealScore = request.Params["RealScore_" + i].ToString();
                    //备注
                    elemModel.Remark = request.Params["ScoreRemark_" + i].ToString();

                    //添加到列表中
                    model.ElemScoreList.Add(elemModel);
                }
            }
        }

        return model;
    }

    /// <summary>
    /// 查询要素信息
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void GetElemInfo(HttpContext context)
    {
        //获取岗位ID
        string templateNo = context.Request.Params["TemplateNo"].ToString();
        
        //查询面试要素
        DataTable dtElem = RectInterviewBus.GetCheckElemInfo(templateNo);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtElem, "Data");
        //linq排序
        var dsLinq =
                from x in dsXML.Descendants("Data")
                select new ElemInfo()
                {
                    ElemID = x.Element("ElemID").Value,//ID
                    ElemName = x.Element("ElemName").Value,//名称
                    MaxScore = x.Element("MaxScore").Value,//最大得分
                    Rate = x.Element("Rate").Value//权重     
                };
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 要素信息
    /// </summary>
    public class ElemInfo
    {
        public string ElemID { get; set; }
        public string ElemName { get; set; }
        public string MaxScore { get; set; }
        public string Rate { get; set; }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}