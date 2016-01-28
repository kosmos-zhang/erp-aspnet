/**********************************************
 * 类作用：   新建培训维护处理
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

public partial class Pages_Office_HumanManager_Training_Edit : BasePage
{

    /// <summary>
    /// 类名：Training_Edit
    /// 描述：新建培训维护处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/02
    /// 最后修改时间：2009/04/02
    /// </summary>
    ///
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            
            #region 新建、修改共通处理
            //培训列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_TRAINING_INFO;
            //培训方式
            ddlTrainingWay.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlTrainingWay.TypeCode = ConstUtil.CODE_TYPE_TRAINING;
            ddlTrainingWay.IsInsertSelect = true;
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
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            #endregion

            //获取培训ID
            string trainingID = Request.QueryString["TrainingID"];
            //培训编号为空，为新建模式
            if (string.IsNullOrEmpty(trainingID))
            {
                #region 新建时初期处理
                //编号初期处理
                codruleTraining.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codruleTraining.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_RTRAINING;
                //设置培训编号不可见
                divTrainingNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                //进度安排
                divScheduleInfo.InnerHtml = CreateScheduleTable() + EndTable();
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "培训";
                //获取并设置培训信息
                InitTrainingInfo(trainingID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }
    #endregion

    #region 根据培训ID，获取培训信息，并设置到页面显示
    /// <summary>
    /// 根据培训ID，获取培训信息，并设置到页面显示
    /// </summary>
    /// <param name="trainingID">培训ID</param>
    private void InitTrainingInfo(string trainingID)
    {

        //设置培训编号可见
        divTrainingNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");

        //查询培训信息
        DataSet dsTrainingInfo = TrainingBus.GetTrainingInfoWithID(trainingID);
        //获取培训基本信息
        DataTable dtBaseInfo = dsTrainingInfo.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置培训基本信息
            //培训编号
            divTrainingNo.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingNo");
            //培训名称
            txtTrainingName.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingName");
            //发起时间
            txtApplyDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ApplyDate");
            //发起人
            UserCreaterName.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeName");
            txtCreateID.Value = GetSafeData.GetStringFromInt(dtBaseInfo.Rows[0], "EmployeeID");
            //项目编号
            txtProjectNo.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ProjectNo");
            //项目名称
            txtProjectName.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ProjectName");
            //培训机构
            txtTrainingOrgan.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingOrgan");
            //预算费用
            txtPlanCost.Text = GetSafeData.GetStringFromDecimal(dtBaseInfo.Rows[0], "PlanCost");
            //培训天数
            txtTrainingCount.Text = GetSafeData.GetStringFromInt(dtBaseInfo.Rows[0], "TrainingCount");
            //培训地点 
            txtTrainingPlace.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingPlace");
            //培训方式
            ddlTrainingWay.SelectedValue = GetSafeData.GetStringFromInt(dtBaseInfo.Rows[0], "TrainingWay");
            //培训老师
            txtTrainingTeacher.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingTeacher");
            //开始时间
            txtStartDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "StartDate");
            //结束时间
            txtEndDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EndDate");
            //考核人
            txtCheckPerson.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckPerson");
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
            //目的
            txtGoal.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Goal");
            //培训备注
            txtTrainingRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingRemark");
            #endregion

            //设置参与人员
            InitJionUserInfo(dsTrainingInfo.Tables[1]);
            //设置招聘目标
            InitScheduleInfo(dsTrainingInfo.Tables[2]);
        }
    }
    #endregion

    #region 设置参与人员
    /// <summary>
    /// 设置参与人员
    /// </summary>
    /// <param name="dtJionUser">参与人员信息</param>
    private void InitJionUserInfo(DataTable dtJionUser)
    {
        //参与人员ID
        StringBuilder sbJoinUserName = new StringBuilder();
        //参与人员名
        StringBuilder sbJoinUserID = new StringBuilder();
        //参与人员存在时，设置参与人员
        if (dtJionUser != null && dtJionUser.Rows.Count > 0)
        {
            //遍历所有参与人员
            for (int i = 0; i < dtJionUser.Rows.Count; i++)
            {
                //获取人员部门区分
                //string flag = GetSafeData.ValidateDataRow_String(dtJionUser.Rows[i], "Flag");
                //获取参与人ID
                string joinID = GetSafeData.GetStringFromInt(dtJionUser.Rows[i], "JoinID");
                ////部门时
                //if (flag.Equals(ConstUtil.DEPT_EMPLOY_FLAG_DEPT))
                //{
                //    //设置参与人ID
                //    sbJoinUserID.Append(ConstUtil.DEPT_EMPLOY_SELECT_DEPT + joinID + ",");
                //    //设置参与人名
                //    sbJoinUserName.Append(GetSafeData.ValidateDataRow_String(dtJionUser.Rows[i], "DeptName") + ";");
                //}
                //人员时
                //else if (flag.Equals(ConstUtil.DEPT_EMPLOY_FLAG_EMPLOY))
                //{
                    //设置参与人ID
                    //sbJoinUserID.Append(ConstUtil.DEPT_EMPLOY_SELECT_EMPLOY + joinID + ",");
                sbJoinUserID.Append(joinID + ",");
                    //设置参与人名
                //sbJoinUserName.Append();
                    sbJoinUserName.Append(GetSafeData.ValidateDataRow_String(dtJionUser.Rows[i], "EmployeeName") + ",");
                //}
            }
        }
        //设置参与人名在页面表示
        //DeptJoinUserName.Text = sbJoinUserName.ToString().TrimEnd(';');
        UserJoinUserName.Text = sbJoinUserName.ToString().TrimEnd(',');
        //设置参与人ID到隐藏域
        txtJoinUser.Value = sbJoinUserID.ToString().TrimEnd(',');
    }
     #endregion

    #region 设置进度安排
    /// <summary>
    /// 设置进度安排
    /// </summary>
    /// <param name="dtSchedule">进度安排信息</param>
    private void InitScheduleInfo(DataTable dtSchedule)
    {
        //定义保存进度安排的变量
        StringBuilder sbScheduleInfo = new StringBuilder();

        //进度安排存在时，设置进度安排
        if (dtSchedule != null && dtSchedule.Rows.Count > 0)
        {
            for (int i = 0; i < dtSchedule.Rows.Count; i++)
            {
                //插入行开始标识
                sbScheduleInfo.AppendLine("<tr>");
                //选择框
                sbScheduleInfo.AppendLine("<td class='tdColInputCenter'><input type='checkbox' id='chkSelect_" + (i + 1).ToString() + "'></td>");
                
                //进度时间
                sbScheduleInfo.AppendLine("<td class='tdColInputCenter'><input type='text' maxlength = '10' value='"
                            + GetSafeData.GetStringFromDateTime(dtSchedule.Rows[i], "ScheduleDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtScheduleDate_" + (i + 1).ToString() + "' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtScheduleDate_" + (i + 1).ToString() + "')})\"></td>");
                //内容摘要时间
                sbScheduleInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '100' value='"
                            + GetSafeData.ValidateDataRow_String(dtSchedule.Rows[i], "Abstract") + "' class='tdinput' id='txtAbstract_" + (i + 1).ToString() + "'></td>");
                //备注
                sbScheduleInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '100' value='"
                            + GetSafeData.ValidateDataRow_String(dtSchedule.Rows[i], "Remark") + "' class='tdinput' id='txtRemark_" + (i + 1).ToString() + "'></td>");
                
                //插入行结束标识
                sbScheduleInfo.AppendLine("</tr>");
            }
        }
        //进度安排设置到DIV中表示
        divScheduleInfo.InnerHtml = CreateScheduleTable() + sbScheduleInfo.ToString() + EndTable();
    }
    #endregion

    #region 生成进度安排表格的标题部分
    /// <summary>
    /// 生成进度安排表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateScheduleTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblScheduleDetailInfo'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='50'>");
        table.AppendLine("选择<input type='checkbox' name='chkAll' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle'>时间</td>");
        table.AppendLine("<td class='ListTitle'>内容摘要</td>");
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

}
