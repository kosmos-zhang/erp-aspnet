/**********************************************
 * 类作用：   新建面试维护处理
 * 建立人：   吴志强
 * 建立时间： 2009/04/02
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

public partial class Pages_Office_HumanManager_RectInterview_Edit : BasePage
{
    /// <summary>
    /// 类名：RectInterview_Edit
    /// 描述：新建面试维护处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示设置
        if (!IsPostBack)
        {
            #region 新建、修改共通处理
            //获取招聘活动列表数据
            ddlRectPlan.Items.Clear();
           
            ddlRectPlan.DataSource = RectInterviewBus.GetRectPlanInfo();
            ddlRectPlan.DataValueField = "PlanNo";
            ddlRectPlan.DataTextField = "Title";
            ddlRectPlan.DataBind();
            ddlRectPlan.Items.Insert(0, new ListItem("--请选择-- ", ""));
            ddlRectPlan.SelectedIndex = 0;
            ddlRectPlan.Attributes.Add("onchange", "InitQuterInfo();");

            //获取消息渠道列表数据
            //ddlInfoFrom.DataSource = RectInterviewBus.GetProxyInfo();
            //ddlInfoFrom.DataValueField = "ID";
            //ddlInfoFrom.DataTextField = "ProxyCompanyName";
            //ddlInfoFrom.DataBind();
            //获取应聘岗位列表数据
            ddlQuarter.DataSource = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlQuarter.DataValueField = "ID";
            ddlQuarter.DataTextField = "QuarterName";
            ddlQuarter.DataBind();
            ddlQuarter.SelectedIndex = 0;
            //ddlQuarter.Items.Insert(0, new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE));
            ddlQuarter.Attributes.Add("onchange", "InitTemplateInfo();");
            ddlTemplate.Attributes.Add("onchange", "InitInterviewElem();");
            //面试方式
            ddlInterviewType.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlInterviewType.TypeCode = ConstUtil.CODE_TYPE_INTERVIEW;
            ddlInterviewType.IsInsertSelect = true;

            ddlCheckType.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCheckType.TypeCode = ConstUtil.CODE_TYPE_INTERVIEW;
            ddlCheckType.IsInsertSelect = true;
            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTINTERVIEW_INFO;
            hidEmployeeModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_ADD;

            FlowRectApply.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_HUMAN;
            FlowRectApply.BillTypeCode = ConstUtil.BILL_TYPECODE_HUMAN_RECT_INTERVIEW;
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
                hidIsliebiao.Value = "1";
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            #endregion

            //获取面试记录ID
            string interviewID = Request.QueryString["ID"];
            if (Request.QueryString["isLieBiao"] != null)
            {
                hidIsliebiao.Value = "1";
            }
            //模板ID为空时，为新建模式
            if (string.IsNullOrEmpty(interviewID))
            {

                #region 新建时初期处理
                this.hidIsSearch.Value  = "1";
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_INTERVIEW;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidInterviewID.Value = string.Empty;
                //生成表格
                divElemScoreDetail.InnerHtml = CreateElemScoreTable() + EndTable();
                //
              txtInterviewDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                this.hidIsSearch.Value = "2";
                divTitle.InnerText = "面试记录";
                //获取并设置面试记录信息
                InitInterviewInfo(interviewID);
                //设置编辑模式
                hidInterviewID.Value = interviewID;
                #endregion
            }
        }
    }
    #endregion

    #region 根据面试记录ID，获取信息，并设置到页面显示
    /// <summary>
    /// 根据面试记录ID，获取信息，并设置到页面显示
    /// </summary>
    /// <param name="interviewID">面试记录ID</param>
    private void InitInterviewInfo(string interviewID)
    {
        //设置编号可见
        divCodeNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");

        //查询考试信息
        DataSet dsInterviewInfo = RectInterviewBus.GetInterviewInfoWithID(interviewID);
        //获取考试基本信息
        DataTable dtBaseInfo = dsInterviewInfo.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置基本信息
           // 编号
            this.divCodeNo .InnerHtml  = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewNo");
            //招聘活动
            ddlRectPlan.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "PlanID");
            //姓名
            txtStaffName.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "StaffNameDisplay");
            hidStaffName.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "StaffName");
            //招聘方式
            ddlRectType.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "RectType");
            //应聘岗位
            ddlQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "QuarterID");
               GetTempalte(GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "QuarterID"), GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TemplateNo"));
            
            //初试日期
            txtInterviewDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewDate");
            //附件
            string attatchment = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Attachment");
            hfAttachment.Value = attatchment;
            hfPageAttachment.Value = attatchment;
            //设置下载 上传的显示
            if (string.IsNullOrEmpty(attatchment))
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
            spanAttachmentName.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "AttachmentName");
            //初试面试方式
            ddlInterviewType.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewType");
            //初试面试地点
            txtInterviewPlace.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewPlace");
            //初试人员意见
            txtInterviewNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewNote");
            //初试结果
            ddlInterviewResult.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "InterviewResult");

            //复试日期
            txtCheckDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckDate");
            //复试面试方式
            ddlCheckType.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckType");
            //复试面试地点
            txtCheckPlace.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckPlace");
            //复试人员意见
            txtCheckNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckNote");
            //复试结果
            ddlFinalResult.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "FinalResult");



            //综合素质
            txtManNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ManNote");
            //专业知识
            txtKnowNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "KnowNote");
            //工作经验
            txtWorkNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "WorkNote");
            //要求待遇
            txtSalaryNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "SalaryNote");
            //备注
            txtRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Remark");



            //工作经验
            txtOurSalary.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "OurSalary");
            //要求待遇
            txtFinalSalary.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "FinalSalary");
            //备注
            txtOtherNote.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "OtherNote");
            #endregion

            //设置要素信息
            InitElemScoreInfo(dsInterviewInfo.Tables[1], GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TestScore"));
        }
    }
    #endregion

    #region 设置要素得分
    /// <summary>
    /// 设置要素得分
    /// </summary>
    /// <param name="dtElemScore">要素信息</param>
    /// <param name="testScore">面试总成绩</param>
    /// 
    private void InitElemScoreInfo(DataTable dtElemScore, string testScore)
    {
        //面试总成绩为空时，设置默认值
        if (string.IsNullOrEmpty(testScore))
        {
            testScore = "0.00";
        }
        //定义变量
        StringBuilder sbElemScoreInfo = new StringBuilder();
        //要素得分时，设置要素得分
        if (dtElemScore != null && dtElemScore.Rows.Count > 0)
        {
            //遍历所有要素得分添加到表格中
            for (int i = 0; i < dtElemScore.Rows.Count; i++)
            {
                //插入行开始标识
                sbElemScoreInfo.AppendLine("<tr>");
                //要素名称
                sbElemScoreInfo.AppendLine("<td class='tdColInput'><input type='hidden' value='"
                            + GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "ElemID") + "' id='txtElemID_" + (i + 1).ToString() + "'>"
                            + GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "ElemName") + "</td>");
                //满分
                sbElemScoreInfo.AppendLine("<td class='tdColInputCenter' id='tdMaxScore_" + (i + 1).ToString() + "'>"
                            + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "MaxScore")) 
                            + "</td>");
                //权重
                sbElemScoreInfo.AppendLine("<td class='tdColInputCenter' id='tdRate_" + (i + 1).ToString() + "'>"
                            + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "Rate")) 
                            + "</td>");
                //面试得分
                sbElemScoreInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '6' value='"
                            + StringUtil.TrimZero(GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "RealScore")) 
                            + "' style= 'width:95%;' class='tdinput' id='txtRealScore_"
                            + (i + 1).ToString() + "'  onchange='Number_round(this,\"2\");'  onblur='CalculateTestScore(this);'></td>");
                //备注
                sbElemScoreInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '250' value='"
                            + GetSafeData.ValidateDataRow_String(dtElemScore.Rows[i], "Remark") 
                            + "' style= 'width:95%;' class='tdinput' id='txtScoreRemark_" + (i + 1).ToString() + "'></td>");

                //插入行结束标识
                sbElemScoreInfo.AppendLine("</tr>");
            }
            //插入面试总成绩行
            sbElemScoreInfo.AppendLine("<tr><td colSpan = '5' class='tdColInput' align='right'><table><tr><td>面试总成绩：</td><td><div id='divTestScore' style='width:100px;text-align:right;'>"
                                            + testScore + "</div></td></tr></table></td></tr>");
        }
        //要素得分设置到DIV中表示
        divElemScoreDetail.InnerHtml = CreateElemScoreTable() + sbElemScoreInfo.ToString() + EndTable();
    }
    #endregion

    #region 生成要素得分表格的标题部分
    /// <summary>
    /// 生成要素表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateElemScoreTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table id='tblElemScoreDetail'  width='100%' border='0'align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle'>要素名称</td>");
        table.AppendLine("<td class='ListTitle'>满分</td>");
        table.AppendLine("<td class='ListTitle'>权重(%)</td>");
        table.AppendLine("<td class='ListTitle'>面试得分</td>");
        table.AppendLine("<td class='ListTitle'>备注</td>");
        table.AppendLine("</tr>");

        //返回表格语句
        return table.ToString();
    }
    #endregion

    #region 返回表格的结束符
    /// <summary>
    /// 返回表格的结束符
    /// </summary>
    /// <returns></returns>
    private string EndTable()
    {
        return "</table>";
    }
    #endregion

    public void   GetTempalte(string quarterID,string templateNo)
    {
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

                if (no == templateNo )
                {
                    ddlTemplate.Items.Add(new ListItem(title, no));

                    //sbTemplateInfo.AppendLine("<option value=\"" + no + "\" selected=\"selected\">" + title + "</option>");
                }
                else
                {
                    ddlTemplate.Items.Add(new ListItem(title, no));
                    //sbTemplateInfo.AppendLine("<option value=\"" + no + "\">" + title + "</option>");
                }
            }
            ddlTemplate.SelectedValue = templateNo;
        
        }
   
     

    }

}
