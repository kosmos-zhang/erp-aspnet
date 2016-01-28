/**********************************************
 * 类作用：   工资报表
 * 建立人：   吴志强
 * 建立时间： 2009/05/19
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;
using System.Web.UI.WebControls;

public partial class Pages_Office_HumanManager_SalaryReport_Edit : BasePage
{
    /// <summary>
    /// 类名：SalaryReport_Edit
    /// 描述：工资报表
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/19
    /// 最后修改时间：2009/05/19
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示设置
        if (!IsPostBack)
        {
            #region 新建、修改共通处理
            //年下拉列表
            DateTime.Now.ToString("yyyy");
            txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            for (int i = -2; i < 12; i++)
            {
                //获取年
                string year = Convert .ToString (2009+i);
                //添加下拉列表选项
                ddlYear.Items.Insert(i + 2, new ListItem(year, year));
            }
            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_SALARY_REPORT_LIST;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_SALARY_REPORT_NEW, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            //
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_SALARY_REPORT_LIST;
            FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_HUMAN_SALARY_REPORT;
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_HUMAN;
            #endregion
         
            //获取ID
            string ID = Request.QueryString["ID"];
            if (Request.QueryString["isLiebiao"] != null)
            {
                hidIsliebiao.Value = "1";
            }
            //ID为空时，为新建模式
            if (string.IsNullOrEmpty(ID))
            {
                #region 新建时初期处理
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_SALARY_REPORT;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置ID
                hidIdentityID.Value = string.Empty;
                //获取前一月的年份
                string lastYear = DateTime.Now.ToString("yyyy");
                //获取前一月的月份
                string lastMonth = DateTime.Now.ToString("MM");
                //开始时间
                txtStartDate.Text = lastYear + "-" + lastMonth + "-" + "01";
                //结束时间
                txtEndDate.Text = lastYear + "-" + lastMonth + "-" 
                    + DateTime.DaysInMonth(int.Parse(lastYear), int.Parse(lastMonth)).ToString();
                //设置所属年月
                ddlYear.SelectedValue = lastYear;
                ddlMonth.SelectedValue = lastMonth;
                //制单日期
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
               UserCreator.Text = userInfo.EmployeeName;
               txtCreator.Value = userInfo.EmployeeID.ToString ();
              //  txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "工资报表";
                //自动生成编号的控件设置不可见
                divCodeRule.Attributes.Add("style", "display:none;");
                //编号设置为可见
                divCodeNo.Attributes.Add("style", "display:block;");
                //获取并设置信息
                InitReprotInfo(ID);
                //设置编辑模式
                hidIdentityID.Value = ID;
                #endregion
            }
        }
    }

    #region 初期表示工资报表信息
    /// <summary>
    /// 初期表示工资报表信息
    /// </summary>
    /// <param name="ID">工资报表ID</param>
    private void InitReprotInfo(string ID)
    {
        //获取基本信息
        DataTable dtReportInfo = SalaryReportBus.GetReportInfoByID(ID);
        hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_SALARY_REPORT_LIST;
        //数据存在时
        if (dtReportInfo != null && dtReportInfo.Rows.Count > 0)
        {
            //报表编号
            string reportNo = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReprotNo");
            divCodeNo.InnerHtml = reportNo;
            txtIdentityID.Value = ID;
            txtReportNo.Value = reportNo;
            //报表主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportName");
            //报表状态
            txtReportStatus.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "Status");
            string ssd = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "FlowStatus");
            //if (string.IsNullOrEmpty(ssd))
            //{
            //    txtBillStatus.Value = "1";
            //}
            //else
            //{
            //    txtBillStatus.Value = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "FlowStatus");
            //}
            string temp = "";
            if (txtReportStatus.Text == "待提交")
            {
                temp = "1";
            }
            else if (txtReportStatus.Text == "已生成")
            {
                temp = "1";
            }
            else if (txtReportStatus.Text == "已确认")
            {
                temp = "2";
            }
            else if (txtReportStatus.Text == "已提交")
            {
                temp = "1";
            }
            txtBillStatus.Value = temp;

            
            if (txtReportStatus.Text == "已确认") 
            {
                //txtBillStatus.Value = "2";
               // btnSave.Visible = false;
                //btnSave.Attributes.Add("style", "display:none");
                //this.ImgReBuild .Attributes.Add("style", "display:none");
                //document.getElementById("ImgReBuild").src = "../../../Images/Button/ btn_qxsc.jpg";
                //document.getElementById("ImgReBuild").attachEvent("onclick", DoDelete);

                //document.getElementById("ImgReBuild").src = "../../../Images/Button/ UnClick_qxsc.jpg";
                //document.getElementById("ImgReBuild").attachEvent("onclick", gogo);

                ImgReBuild.Src = "../../../Images/Button/UnClick_qxsc.jpg";
                ImgReBuild.Attributes.Add("onclick", "gogo();");
                //btnUnSave.Attributes.Add("style", "display:Block");

                btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                btnSave.Attributes.Add("onclick", "gogo();");
               // Response.Write("<script language='javascript'>GetFlowButton_DisplayControl()</script>");
            }
            else if (txtReportStatus.Text == "已提交")
            {
             
                //btnSave.Attributes.Add("style", "display:none");
                //btnUnSave.Attributes.Add("style", "display:Block");
                ImgReBuild.Src = "../../../Images/Button/UnClick_qxsc.jpg";
                ImgReBuild.Attributes.Add("onclick", "gogo();");
                btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                btnSave.Attributes.Add("onclick", "gogo();");
                // Response.Write("<script language='javascript'>GetFlowButton_DisplayControl()</script>");
            }
            //所属年月
            ddlYear.SelectedValue = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportYear");
            ddlMonth.SelectedValue = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "ReportMonth");
            ddlYear.Enabled = false;
            ddlMonth.Enabled = false;
            //开始日期
            txtStartDate.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "StartDate");
            txtStartDate.Enabled = false ;
            //结束日期
            txtEndDate.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "EndDate");
            txtEndDate.Enabled = false ;
            //编制人
            UserCreator.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "Creator");
            //编制日期
            txtCreateDate.Text = GetSafeData.ValidateDataRow_String(dtReportInfo.Rows[0], "CreateDate");
            UserCreator.Enabled = false;
            txtCreateDate.Enabled = false;

            btnCreateReport.Disabled = true;
            btnCreateReport.Src = "../../../Images/Button/cw_uscbb.jpg";

            //设置页面信息
            divSalaryDetailInfo.InnerHtml = SalaryReportBus.InitSalaryReportInfo(reportNo);

        }
    }
    #endregion
    protected string getChangeDay(string month,int  year)
    {
        if ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))
        {
            return "31";
        }
        else if (month == "02")
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            {
                return "28";
            }
            else { return "29"; }
        }
        else
        {
            return "30";
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getChage();
    }

    private void getChage()
    {

        txtCreator.Value = string.Empty;
        string year = ddlYear.SelectedValue;
        string month = ddlMonth.SelectedValue;
        if (!string.IsNullOrEmpty(year))
        {
            txtStartDate.Text = year + "-" + month + "-" + "01";
            txtEndDate.Text = year + "-" + month + "-" + getChangeDay(month, Convert.ToInt32(year));
        }
      
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        getChage();
    }
}
