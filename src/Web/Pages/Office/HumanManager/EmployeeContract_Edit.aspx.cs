/**********************************************
 * 类作用：   新建合同
 * 建立人：   吴志强
 * 建立时间： 2009/04/28
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_EmployeeContract_Edit : BasePage
{
    /// <summary>
    /// 类名：EmployeeContract_Edit
    /// 描述：新建合同
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示设置
        if (!IsPostBack)
        {
            #region  新建、修改共通处理
            //模板列表模块ID
            hidListModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_INFO;
            hidEnterModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ENTER;
            //合同名称
            ctContractName.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctContractName.TypeCode = ConstUtil.CODE_TYPE_CONTRACT_NAME;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_CONTRACT_EDIT, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
                hidFromPage.Value = Request.QueryString["FromPage"];
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            #endregion

            //获取ID
            string ID = Request.QueryString["ID"];
            //ID为空时，为新建模式
            if (string.IsNullOrEmpty(ID))
            {
                #region 新建时初期处理
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_CONTRACT;
                
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                divCodeRule.Attributes.Add("style", "color: #666666");
                //设置ID
                hidIdentityID.Value = string.Empty;
                //获取员工ID
                string employeeID = Request.QueryString["UserID"];
                //待入职页面迁移过来时
                if (!string.IsNullOrEmpty(employeeID))
                {
                    //员工
                    UserEmployee.Text = Request.QueryString["UserName"];
                    txtEmployeeID.Value = employeeID;
                    //设置员工不可编辑
                    UserEmployee.Enabled = false;
                }
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "劳动合同";
                //自动生成编号的控件设置不可见
                divCodeRule.Attributes.Add("style", "display:none;");

                //编号设置为可见
                divCodeNo.Attributes.Add("style", "display:block;");
                divCodeNo.Attributes.Add("style", "color: #666666");
                //获取并设置信息
                InitContractInfo(ID);
                //设置编辑模式
                hidIdentityID.Value = ID;
                #endregion
            }
        }
    }

    #region 获取并设置页面信息
    /// <summary>
    /// 获取并设置页面信息
    /// </summary>
    /// <param name="ID">ID</param>
    private void InitContractInfo(string ID)
    {
        //获取数据
        DataTable dtContract = EmployeeContractBus.GetEmployeeContractInfoWithID(ID);
        //数据存在时，设置值
        if (dtContract != null && dtContract.Rows.Count > 0)
        {
            //编号
            divCodeNo.InnerHtml = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractNo");
            //员工
            UserEmployee.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "EmployeeName");
            txtEmployeeID.Value = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "EmployeeID");
            //合同名称
            ctContractName.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractName");
            //主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "Title");
            //合同类型
            ddlContractType.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractType");
            //合同属性
            ddlContractProperty.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractProperty");
            //工种
            //ddlContractKind.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractKind");
            //合同状态
            ddlContractStatus.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractStatus");
            //合同期限
            ddlContractPeriod.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ContractPeriod");
            //试用月数
            txtTestMonth.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "TrialMonthCount");
            //试用工资
            txtTestWage.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "TestWage");
            //转正工资
            txtWage.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "Wage");
            //签约时间
            txtSigningDate.Text = GetSafeData.GetStringFromDateTime(dtContract.Rows[0], "SigningDate", "yyyy-MM-dd");
            //生效时间
            txtStartDate.Text = GetSafeData.GetStringFromDateTime(dtContract.Rows[0], "StartDate", "yyyy-MM-dd");
            //失效时间
            txtEndDate.Text = GetSafeData.GetStringFromDateTime(dtContract.Rows[0], "EndDate", "yyyy-MM-dd");
            //转正标识
            ddlFlag.SelectedValue = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "Flag");
            //提醒人
            UserReminder.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "ReminderName");
            hidReminder.Value = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "Reminder");
            //提前时间
            txtAheadDay.Text = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "AheadDay");


            //附件
            string attachment = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "Attachment");
            hfAttachment.Value = attachment;
            hfPageAttachment.Value = attachment;
            //设置下载 上传的显示
            if (string.IsNullOrEmpty(attachment))
            {
                //简历处理不显示  
                divDealAttachment.Attributes.Add("style", "display:none;");
                //上传简历显示
                divUploadAttachment.Attributes.Add("style", "display:block;");
            }
            else
            {
                //上传简历不显示
                divUploadAttachment.Attributes.Add("style", "display:none;");
                //简历处理显示
                divDealAttachment.Attributes.Add("style", "display:block;");
            }
            spanAttachmentName.InnerHtml = GetSafeData.ValidateDataRow_String(dtContract.Rows[0], "AttachmentName");
        }
    }
    #endregion
}
