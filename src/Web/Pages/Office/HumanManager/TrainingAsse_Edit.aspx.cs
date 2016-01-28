/**********************************************
 * 类作用：   新建培训考核处理
 * 建立人：   吴志强
 * 建立时间： 2009/04/03
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_TrainingAsse_Edit : BasePage
{
    /// <summary>
    /// 类名：TrainingAsse_Edit
    /// 描述：新建培训考核处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/03
    /// 最后修改时间：2009/04/03
    /// </summary>
    ///
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            #region 新建、修改共通处理
            //ddlCheckWay.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //ddlCheckWay.TypeCode = ConstUtil.CODE_TYPE_CHECK_WAY;
            //获取培训信息
            DataTable dtTraning = TrainingBus.SearchOnTrainingInfo();
            //设定培训信息控件的数据源
            ddlTraining.DataSource = dtTraning;
            //指定培训信息控件的Value值
            ddlTraining.DataValueField = "TrainingNo";
            //指定培训信息控件的Text表示值
            ddlTraining.DataTextField = "TrainingName";
            //绑定培训信息控件
            ddlTraining.DataBind();
            //添加一请选择选项
            //ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            //ddlTraining.Items.Insert(0, Item);
            
            //给控件添加onchange事件
            ddlTraining.Attributes.Add("onchange", "GetJoinUserInfo();");
            //设置模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_TRAININGASSE_INFO;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            string from = string.Empty;
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //获取跳转前页面
                from = Request.QueryString["From"];
                //链接过来的页面
                hidFrom.Value = from;
                //培训列表页面过来时
                if (!string.IsNullOrEmpty(from) && "1".Equals(from))
                {
                    //设置培训列表模块ID
                    hidModuleIDTraining.Value = ConstUtil.MODULE_ID_HUMAN_TRAINING_INFO;
                    //设置培训编号
                    ddlTraining.SelectedValue = Request.QueryString["SelectTrainingNo"];
                }
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            //设置当前日期
            hidTodayDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            #endregion

            //获取培训考核ID
            string trainingAsseID = Request.QueryString["TrainingAsseID"];
            //培训考核ID为空，为新建模式
            if (string.IsNullOrEmpty(trainingAsseID))
            {
                #region 新建时初期处理
                //编号初期处理
                codruleTrainingAsse.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codruleTrainingAsse.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_RTRAININGASSE;
                //设置培训考核编号不可见
                divTrainingAsseNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //填写人
                //从session中获取登陆用户信息
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                //设置用户名
                txtFillUserName.Text = userInfo.EmployeeName;
                //设置用户ID
                hidFillUserID.Value = userInfo.EmployeeID.ToString();
                //考核结果
                divRsseResultDetail.InnerHtml = CreateResultTable() + EndTable();
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                if (!"1".Equals(from))
                {
                    ddlTraining.SelectedIndex = 0;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "GetJoinUserInfo"
                        , "<script language=javascript>GetJoinUserInfo();</script>");
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "培训考核";
                //设置培训考核编号可见
                divTrainingAsseNo.Attributes.Add("style", "display:block;");
                //自动生成编号的控件设置为不可见
                divCodeRule.Attributes.Add("style", "display:none;");
                //获取并设置培训信息
                InitTrainingAsseInfo(trainingAsseID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }
    #endregion

    #region 根据考核ID，获取考核信息，并设置到页面显示
    /// <summary>
    /// 根据考核ID，获取考核信息，并设置到页面显示
    /// </summary>
    /// <param name="asseID">考核ID</param>
    private void InitTrainingAsseInfo(string asseID)
    {
        //查询培训信息
        DataSet dsTrainingAsseInfo = TrainingAsseBus.GetTrainingAsseInfoWithID(asseID);
        //获取培训基本信息
        DataTable dtBaseInfo = dsTrainingAsseInfo.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置培训基本信息
            //考核编号
            divTrainingAsseNo.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "AsseNo");
            //培训
            ddlTraining.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingNo");
            //考核人
            UserCheckPerson.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckPerson");
            //考核方式
            //ddlCheckWay.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckWay");
            txtCheckWay.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckWay");
            //考核时间
            txtCheckDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckDate");
            //填写人
            txtFillUserName.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "FillUserName");
            //培训规划
            txtTrainingPlan.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TrainingPlan");
            //领导意见
            txtLeadViews.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "LeadViews");
            //说明
            txtDescription.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Description");
            //考核总评
            txtGeneralComment.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "GeneralComment");
            //考核备注
            txtCheckRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "CheckRemark");
            #endregion

            //设置考核结果
            InitAsseResultInfo(dsTrainingAsseInfo.Tables[1]);
        }
    }
    #endregion

    #region 设置考核结果
    /// <summary>
    /// 设置考核结果
    /// </summary>
    /// <param name="dtSchedule">考核结果信息</param>
    private void InitAsseResultInfo(DataTable dtResult)
    {
        //定义保存进度安排的变量
        StringBuilder sbResultInfo = new StringBuilder();
        //换行标识
        int flag = 0;
        //进度安排存在时，设置进度安排
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                /* 每行显示两条数据，所以通过对2取余来判断是否换行 以及 行是否结束 */
                //对2进行取余
                flag = i % 2;
                //余数为0时，表示需要新添加一行
                if (flag == 0)
                {
                    //插入行开始标识
                    sbResultInfo.AppendLine("<tr>");
                }
                //姓名
                sbResultInfo.AppendLine("<td class='tdColInputCenter'><input type='hidden' value='"
                            + GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "EmployeeID") + "' class='tdinput' id='txtJoinUserID_" + i.ToString()
                            + "'>" + GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "EmployeeName") + "</td>");
                //考核得分
                sbResultInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '6' onchange='Number_round(this,2);' value='"
                            + GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "AssessScore") + "' class='tdinput' id='txtAsseScore_" + i.ToString() + "'></td>");
                //考核等级
                sbResultInfo.AppendLine("<td class='tdColInputCenter'>"
                            + InitLevelDropDownList("ddlAsseLevel_" + i.ToString(), GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "AssessLevel")) + "</td>");
                //余数为1时，表示行结束
                if (flag == 1)
                {
                    //插入行结束标识
                    sbResultInfo.AppendLine("</tr>");
                }
            }
            //余数为0时，表示所有的记录为单数条，因此需要把未结束的行结束
            if (flag == 0)
            {
                //插入空白列
                sbResultInfo.AppendLine("<td class='tdColInputCenter'></td><td class='tdColInput'></td><td class='tdColInputCenter'></td>");
                //插入行结束标识
                sbResultInfo.AppendLine("</tr>");
            }
        }
        //考核结果设置到DIV中表示
        divRsseResultDetail.InnerHtml = CreateResultTable() + sbResultInfo.ToString() + EndTable();
    }
    #endregion

    #region 设置考核等级下拉框
    /// <summary>
    /// 设置性别下拉框
    /// </summary>
    /// <param name="ctronlID">控件ID</param>
    /// <param name="selectValue">选中值</param>
    /// <returns></returns>
    private string InitLevelDropDownList(string ctronlID, string selectValue)
    {
        //定义返回的变量
        StringBuilder inputSelect = new StringBuilder();
        //开始标识
        inputSelect.AppendLine("<select id='" + ctronlID + "' class='tdColInput'>");
        //生成选择项//添加选择项
        if ("1".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value=''>--请选择--</option>");
            inputSelect.AppendLine("<option value='1' selected>不及格</option>");
            inputSelect.AppendLine("<option value='2'>及格</option>");
            inputSelect.AppendLine("<option value='3'>良好</option>");
            inputSelect.AppendLine("<option value='4'>优秀</option>");
        }
        else if ("2".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value=''>--请选择--</option>");
            inputSelect.AppendLine("<option value='1'>不及格</option>");
            inputSelect.AppendLine("<option value='2' selected>及格</option>");
            inputSelect.AppendLine("<option value='3'>良好</option>");
            inputSelect.AppendLine("<option value='4'>优秀</option>");
        }
        else if ("3".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value=''>--请选择--</option>");
            inputSelect.AppendLine("<option value='1'>不及格</option>");
            inputSelect.AppendLine("<option value='2'>及格</option>");
            inputSelect.AppendLine("<option value='3' selected>良好</option>");
            inputSelect.AppendLine("<option value='4'>优秀</option>");
        }
        else if ("4".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value=''>--请选择--</option>");
            inputSelect.AppendLine("<option value='1'>不及格</option>");
            inputSelect.AppendLine("<option value='2'>及格</option>");
            inputSelect.AppendLine("<option value='3'>良好</option>");
            inputSelect.AppendLine("<option value='4' selected>优秀</option>");
        }
        else
        {
            inputSelect.AppendLine("<option value='' selected>--请选择--</option>");
            inputSelect.AppendLine("<option value='1'>不及格</option>");
            inputSelect.AppendLine("<option value='2'>及格</option>");
            inputSelect.AppendLine("<option value='3'>良好</option>");
            inputSelect.AppendLine("<option value='4'>优秀</option>");
        }
        //结束标识
        inputSelect.AppendLine("</select>");
        //返回生成的字符串
        return inputSelect.ToString();
    }
    #endregion

    #region 生成考核结果表格的标题部分
    /// <summary>
    /// 生成考核结果表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateResultTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table id='tblRsseResultDetail'  width='100%' border='0'align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='10%'>姓名</td>");
        table.AppendLine("<td class='ListTitle' width='20%'>考核得分</td>");
        table.AppendLine("<td class='ListTitle' width='20%'>考核等级</td>");
        table.AppendLine("<td class='ListTitle' width='10%'>姓名</td>");
        table.AppendLine("<td class='ListTitle' width='20%'>考核得分</td>");
        table.AppendLine("<td class='ListTitle' width='20%'>考核等级</td>");
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
