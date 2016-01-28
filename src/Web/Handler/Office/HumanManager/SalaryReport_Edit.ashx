<%@ WebHandler Language="C#" Class="SalaryReport_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/20
 * 描    述： 工资报表
 * 修改日期： 2009/05/20
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class SalaryReport_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //获取处理动作
        string action = context.Request.Params["Action"].ToString();
        //生成处理 
        if ("Create".Equals(action))
        {
            //处理生成报表
            CreateReport(context);
        }
        //重新生成
        else if ("DeleteInfo".Equals(action))
        {
            //处理保存报表
           DeleteReport(context);
        }
        //保存处理
        else if ("Save".Equals(action))
        {
            //处理保存报表
            SaveReport(context);
        }
        //审批相关处理
        else if ("FlowOperate".Equals(action))
        {
            //状态
            string status = context.Request.Params["Status"].ToString();
            //报表编号
            string reportNo = context.Request.Params["ReportNo"].ToString();
            //处理保存报表
            bool isSucc = SalaryReportBus.UpdateReportStatus(status, reportNo);
            //返回
            if (isSucc)
            {
                context.Response.Write(new JsonClass("", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
        }
        else if (action == "UpdateMoveApplyCancelConfirmInfo") { UpdateMoveApplyCancelConfirm(context); }//取消确认信息
    }
    private void UpdateMoveApplyCancelConfirm(HttpContext context)
    {
        //获得登录页面POST过来的参数 
        string userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//用户ID
        string CompanyID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

        JsonClass jc;
        string BillStatus = "0";//单据状态

        string ID = context.Request.Params["ID"].ToString().Trim();//id
        string EmpNo = context.Request.Params["ReportNo"].ToString().Trim();//No
        //if (EmplApplyDBHelper.IfCancelEmplApplyInf(EmpNo, CompanyID))
        //{
        //    jc = new JsonClass(ID, "", 2);
        //    context.Response.Write(jc);
        //    context.Response.End();
        //}
        if (SalaryReportDBHelper.UpdateMoveApplyCancelConfirm(BillStatus, ID, userID, CompanyID, EmpNo))
            jc = new JsonClass(ID, "", 1);
        else
            jc = new JsonClass(ID, "", 0);
        context.Response.Write(jc);
    }
    private void DeleteReport(HttpContext context)
    {
        string reportNo = context.Request.QueryString["ReportNo"];
        bool isSucc = SalaryReportBus.DeleteOneReport (reportNo);
        //返回
        if (isSucc)
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        
        
    }
    /// <summary>
    /// 生成报表处理
    /// </summary>
    /// <param name="context"></param>
    private void CreateReport(HttpContext context)
    {
        //获取所属月份
        string belongMonth = context.Request.QueryString["BelongMonth"];
        //校验该月份报表是否生成
        bool isDataExsist = SalaryReportBus.IsExsistReport(belongMonth);
        if (isDataExsist)
        {
            //设置输出流的 HTTP MIME 类型
            context.Response.ContentType = "text/plain";
            //向响应中输出数据
            context.Response.Write("3");
            context.Response.End();
            return;
        }
        //Model变量
        SalaryReportModel model = EditCreateRequstData(context.Request);
        StringBuilder sbReturn = new StringBuilder();
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            string reportInfo = SalaryReportBus.CreateSalaryReport(model);
            //保存成功时
            if (reportInfo != null)
            {
                sbReturn.AppendLine("1|");
                sbReturn.AppendLine(model.ReprotNo + "|");
                sbReturn.AppendLine(reportInfo);
                sbReturn.AppendLine("|" + model.ID);
            }
            //保存未成功时
            else
            {
                sbReturn.AppendLine("0");
            }
        }
        else
        {
            sbReturn.AppendLine("2");
        }
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 从请求中获取信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private SalaryReportModel EditCreateRequstData(HttpRequest request)
    {
        //定义Model变量
        SalaryReportModel model = new SalaryReportModel();
        //获取报表编号
        string reportNo = request.QueryString["ReportNo"];
        //报表编号为空时，通过编码规则编号获取编号
        if (string.IsNullOrEmpty(reportNo))
        {
            //获取编码规则ID
            string codeRuleID = request.QueryString["CodeRuleID"];
            //通过编码规则代码获取人员编码
            reportNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_SALARY_REPORT
                            , ConstUtil.CODING_RULE_COLUMN_SALARY_REPORT_NO);
        }
        //判断是否存在
        bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_SALARY_REPORT
                            , ConstUtil.CODING_RULE_COLUMN_SALARY_REPORT_NO, reportNo);
        //存在的场合
        if (!isAlready)
        {
            return null;
        }

      //  UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        
        //编制人
        model.Creator = request.QueryString["Creator"];
        //model.Creator = userInfo .EmployeeID .ToString ();
        //编制日期
        model.CreateDate = request.QueryString["CreateDate"];
        //设置编号
        model.ReprotNo = reportNo;

        /* 获取报表基本信息 */
        //工资报表主题
        model.ReportName = request.QueryString["Title"];
        //所属月份
        model.ReportMonth = request.QueryString["BelongMonth"];
        //开始时间
        model.StartDate = request.QueryString["StartDate"];
        //结束时间
        model.EndDate = request.QueryString["EndDate"];

        return model;
    }

    /// <summary>
    /// 保存报表处理
    /// </summary>
    /// <param name="context"></param>
    private void SaveReport(HttpContext context)
    {
        //定义变量
        ArrayList lstDetail = new ArrayList();
        ArrayList lstSummary = new ArrayList();
        
        #region 获取参数
        //获取基本数据
        HttpRequest request = context.Request;
        //定义Model变量
        SalaryReportModel model = new SalaryReportModel();
        /* 获取报表基本信息 */
        //报表编号
        string reportNo = request.Params["ReportNo"].ToString();
        model.ReprotNo = reportNo;
        //工资报表主题
        model.ReportName = request.Params["Title"].ToString();
        //所属月份
        model.ReportMonth = request.Params["BelongMonth"].ToString();
        //开始时间
        model.StartDate = request.Params["StartDate"].ToString();
        //结束时间
        model.EndDate = request.Params["EndDate"].ToString();
        
        //获取工资信息
        //获取人员总数
        int userCount = int.Parse(request.Params["UserCount"].ToString());
        //获取工资项列表数据总数
        int salaryCount = int.Parse(request.Params["SalaryCount"].ToString());

        //遍历所有员工
        for (int i = 1; i <= userCount; i++)
        {
            //获取员工ID
            string emplID = request.Params["EmployeeID_" + i.ToString()].ToString();
            //变量定义
            decimal fixedTotal = 0;
            SalaryReportSummaryModel summaryModel = new SalaryReportSummaryModel();
            summaryModel.ReprotNo = reportNo;
            //遍历所有工资项
            for (int j = 1; j <= salaryCount; j++)
            {
                //定义Model变量
                SalaryReportDetailModel detailModel = new SalaryReportDetailModel();
                //员工ID
                detailModel.EmployeeID = emplID;
                //报表编号
                detailModel.ReprotNo = reportNo;
                //工资项ID
                detailModel.ItemNo = request.Params["SalaryItemID_" + j.ToString()];
                //获取工资值
                string fixedMoney = request.Params["FixedMoney_" + i.ToString() + "_" + j.ToString()].ToString();
                //设置工资
                detailModel.SalaryMoney = fixedMoney;

                //计算固定工资总额
                if (!string.IsNullOrEmpty(fixedMoney))
                {
                    fixedTotal += decimal.Parse(fixedMoney);
                }
                
                //添加记录
                lstDetail.Add(detailModel);
            }
            //员工
            summaryModel.EmployeeID = emplID;
            //固定工资
            summaryModel.FixedMoney = fixedTotal.ToString();
            //计件工资
            summaryModel.WorkMoney = request.Params["WorkMoney_" + i.ToString()].ToString();
            //计时工资
            summaryModel.TimeMoney = request.Params["TimeMoney_" + i.ToString()].ToString();
            //提成工资
            summaryModel.CommissionMoney = request.Params["CommissionMoney_" + i.ToString()].ToString();
            //其他应付工资
            summaryModel.OtherGetMoney = request.Params["OtherPayMoney_" + i.ToString()].ToString();
            //应付工资合计
            summaryModel.AllGetMoney = request.Params["AllGetMoney_" + i.ToString()].ToString();
            //社会保险
            summaryModel.Insurance = request.Params["Insurance_" + i.ToString()].ToString();
            //个人所得税
            summaryModel.IncomeTax = request.Params["IncomeTax_" + i.ToString()].ToString();
            //其他应扣款
            summaryModel.OtherKillMoney = request.Params["OtherMinusMoney_" + i.ToString()].ToString();
            //应扣款合计
            summaryModel.AllKillMoney = request.Params["AllKillMoney_" + i.ToString()].ToString();
            //实发工资额
            summaryModel.SalaryMoney = request.Params["FactSalaryMoney_" + i.ToString()].ToString();
            
            //公司提成
            summaryModel.CompanyComMoney = request.Params["CompanyComMoney_" + i.ToString()].ToString();
            //部门提成
            summaryModel.DeptComMoney = request.Params["DeptComMoney_" + i.ToString()].ToString(); 
            //个人业务提成
            summaryModel.PersonComMoney = request.Params["PersonComMoney_" + i.ToString()].ToString();  
            //绩效工资
            summaryModel.PerformanceMoney = request.Params["PerformanceMoney_" + i.ToString()].ToString();   

            lstSummary.Add(summaryModel);

        }
        #endregion
        
        //执行更新
        bool isSucc = SalaryReportBus.SaveSalaryInfo(lstDetail, lstSummary, model);
        //返回
        if (isSucc)
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}