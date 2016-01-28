<%@ WebHandler Language="C#" Class="EmployeeContract_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/28
 * 描    述： 新建合同
 * 修改日期： 2009/04/28
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class EmployeeContract_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //定义Model变量
        EmployeeContractModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = EmployeeContractBus.SaveEmployeeContractInfo(model);
            //保存成功时
            if (isSucce)
            {
                jc = new JsonClass(model.ID, model.ContractNo, 1);
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
    private EmployeeContractModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        EmployeeContractModel model = new EmployeeContractModel();
        //ID
        model.ID = request.Params["ID"].ToString();
        //获取编号
        string contactNo = request.Params["ContractNo"].ToString();

        //新建时
        if (string.IsNullOrEmpty(model.ID))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(contactNo))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取编号
                contactNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_EMPLOYEE_CONTRACT
                                , ConstUtil.CODING_RULE_COLUMN_EMPLOYEE_CONTRACT_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_EMPLOYEE_CONTRACT
                                , ConstUtil.CODING_RULE_COLUMN_EMPLOYEE_CONTRACT_NO, contactNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置编号
        model.ContractNo = contactNo;
        //员工
        model.EmployeeID = request.Params["EmployeeID"].ToString();       
        //合同名称
        model.ContractName = request.Params["ContractName"].ToString();    
        //主题
        model.Title = request.Params["Title"].ToString();           
        //合同类型
        model.ContractType = request.Params["ContractType"].ToString();    
        //合同属性
        model.ContractProperty = request.Params["ContractProperty"].ToString();
        //工种
        model.ContractKind = request.Params["ContractKind"].ToString();  
        //合同状态
        model.ContractStatus = request.Params["ContractStatus"].ToString();  
        //合同期限
        model.ContractPeriod = request.Params["ContractPeriod"].ToString();  
        //试用月数
        model.TrialMonthCount = request.Params["TrialMonthCount"].ToString(); 
        //试用工资
        model.TestWage = request.Params["TestWage"].ToString();        
        //转正工资
        model.Wage = request.Params["Wage"].ToString();            
        //签约时间
        model.SigningDate = request.Params["SigningDate"].ToString();     
        //生效时间
        model.StartDate = request.Params["StartDate"].ToString();       
        //失效时间
        model.EndDate = request.Params["EndDate"].ToString();
        //合同到期提醒人
        model.Reminder = request.Params["Reminder"].ToString();
        //提前时间
        model.AheadDay = request.Params["AheadDay"].ToString();   
              
        //转正标识
        model.Flag = request.Params["Flag"].ToString();            
        //附件
        model.Attachment = request.Params["Attachment"].ToString();
        model.PageAttachment = request.Params["PageAttachment"].ToString();
        model.AttachmentName = request.Params["AttachmentName"].ToString();

        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}