/**********************************************
 * 类作用：   招聘活动维护处理
 * 建立人：   吴志强
 * 建立时间： 2009/03/28
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_RectPlan_Edit : BasePage
{
    /// <summary>
    /// 类名：RectPlan_Edit
    /// 描述：招聘活动维护处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/28
    /// 最后修改时间：2009/03/28
    /// </summary>
    ///
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示处理
        if (!IsPostBack)
        {
            #region 页面内容初期设置
            //学历
            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            ddlCulture.IsInsertSelect = true;
            divCulture.Attributes.Add("style", "display:none;");
            //专业
            ddlProfessional.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlProfessional.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            ddlProfessional.IsInsertSelect = true;
            divProfessional.Attributes.Add("style", "display:none;");
            //招聘活动列表模块ID
            hfModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTPLAN_INFO;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_RECTPLAN_EDIT, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
            #endregion

            //获取人员编号
            string planID = Request.QueryString["RectPlanID"];
            //planID = "38";
            //招聘活动编号为空，为新建模式
            if (string.IsNullOrEmpty(planID))
            {
                #region 新建时初期处理
                //编号初期处理
                codruleRectPlan.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codruleRectPlan.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_RECTPLAN;
                //设置申请招聘编号不可见
                divRectPalnNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                string emp = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                txtPrincipal.Value = emp;
                string empname = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                UserPrincipalName.Text = empname;
                //招聘目标
                divRectGoalDetail.InnerHtml = CreateGoalTable() + EndTable();
                //发布渠道
                divRectPublishDetail.InnerHtml = CreatePublishTable() + EndTable();
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "招聘计划";
                //获取并设置人员信息
                InitRectPlanInfo(planID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }
    #endregion

    #region 根据招聘计划ID，获取招聘计划信息，并设置到页面显示
    /// <summary>
    /// 根据招聘计划ID，获取招聘计划信息，并设置到页面显示
    /// </summary>
    /// <param name="planID">招聘计划ID</param>
    private void InitRectPlanInfo(string planID)
    {

        //设置申请计划编号可见
        divRectPalnNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");

        //查询招聘计划信息
        DataSet dsPlanInfos = RectPlanBus.GetRectPlanInfoWithID(planID);
        //获取招聘计划基本信息
        DataTable dtBaseInfo = dsPlanInfos.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            //设置申请计划编号
            divRectPalnNo.Value = dtBaseInfo.Rows[0]["PlanNo"] == null ? "" : dtBaseInfo.Rows[0]["PlanNo"].ToString();  
            //主题
            txtTitle.Text = dtBaseInfo.Rows[0]["Title"] == null ? "" : dtBaseInfo.Rows[0]["Title"].ToString();  
            //开始时间
            txtStartDate.Text = dtBaseInfo.Rows[0]["StartDate"] == null ? "" : dtBaseInfo.Rows[0]["StartDate"].ToString();  
            this.txtEndDate.Text = dtBaseInfo.Rows[0]["EndDate"] == null ? "" : dtBaseInfo.Rows[0]["EndDate"].ToString();  
            this.ddlStatus.Value = dtBaseInfo.Rows[0]["Status"] == null ? "" : dtBaseInfo.Rows[0]["Status"].ToString();  
            //负责人
            UserPrincipalName.Text = dtBaseInfo.Rows[0]["PrincipalName"] == null ? "" : dtBaseInfo.Rows[0]["PrincipalName"].ToString();  
            txtPrincipal.Value = dtBaseInfo.Rows[0]["Principal"] == null ? "" : dtBaseInfo.Rows[0]["Principal"].ToString();  
            txtPlanFee.Value = dtBaseInfo.Rows[0]["PlanFee"] == null ? "" : dtBaseInfo.Rows[0]["PlanFee"].ToString();
            this.txtFeeNote.Text  = dtBaseInfo.Rows[0]["FeeNote"] == null ? "" : dtBaseInfo.Rows[0]["FeeNote"].ToString();
            this.txtJoinMan.Value = dtBaseInfo.Rows[0]["JoinMan"] == null ? "" : dtBaseInfo.Rows[0]["JoinMan"].ToString();
            this.UserJoinMan.Text = dtBaseInfo.Rows[0]["JoinMan"] == null ? "" : dtBaseInfo.Rows[0]["JoinMan"].ToString();
            this.txtJoinNote.Text  = dtBaseInfo.Rows[0]["JoinNote"] == null ? "" : dtBaseInfo.Rows[0]["JoinNote"].ToString();
            this.txtRequireNum.Value = dtBaseInfo.Rows[0]["RequireNum"] == null ? "" : dtBaseInfo.Rows[0]["RequireNum"].ToString();
        }
        //设置招聘目标
        InitGoalInfo(dsPlanInfos.Tables[1]);
        //设置信息发布
        InitPublishInfo(dsPlanInfos.Tables[2]);
    }
    #endregion

    #region 设置信息发布
    /// <summary>
    /// 设置信息发布
    /// </summary>
    /// <param name="dtPublish">信息发布的信息</param>
    private void InitPublishInfo(DataTable dtPublish)
    {
        //定义保存信息发布的变量
        StringBuilder sbPublishInfo = new StringBuilder();

        //信息发布存在时，设置信息发布
        if (dtPublish != null && dtPublish.Rows.Count > 0)
        {
            for (int i = 0; i < dtPublish.Rows.Count; i++)
            {
                //插入行开始标识
                sbPublishInfo.AppendLine("<tr>");
                //选择框
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='checkbox' id='tblRectPublishDetailInfo_chkSelect_" + (i + 1).ToString() + "'></td>");
                //发布媒体和渠道
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' style='width:85%'  value='"
                            + GetSafeData.ValidateDataRow_String(dtPublish.Rows[i], "PublishPlace") + "' class='tdinput' id='txtPublishPlace_" + (i + 1).ToString() + "'> <span  style=\"cursor:hand\"   onclick=\"popTaskObj.ShowList('txtPublishPlace_" + (i + 1).ToString() + "')\">选择</span></td>");
                //发布时间
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='text' readonly maxlength = '10'  style='width:95%' onchange='SetEndDate(\"" + (i + 1).ToString() + "\");' value='"
                            + GetSafeData.GetStringFromDateTime(dtPublish.Rows[i], "PublishDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtPublishDate_" + (i + 1).ToString() + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtPublishDate_" + (i + 1).ToString() + "')})\"></td>");
                //有效时间
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '4' onblur='SetEndDate(\"" + (i + 1).ToString() + "\");' value='"
                            + GetSafeData.ValidateDataRow_String(dtPublish.Rows[i], "Valid") + "' style='width:95%' class='tdinput' id='txtValid_" + (i + 1).ToString() + "'></td>");
                //截止时间
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'><input type='text'  style='width:95%' maxlength = '10' value='"
                            + GetSafeData.GetStringFromDateTime(dtPublish.Rows[i], "EndDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtEndDate_" + (i + 1).ToString() + "' readonly></td>");
                //费用
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '12' style='width:95%'  onchange='Number_round(this,\"2\");'  value='"
                            + GetSafeData.ValidateDataRow_String(dtPublish.Rows[i], "Cost") + "' class='tdinput' id='txtCost_" + (i + 1).ToString() + "'></td>");
                //效果
                sbPublishInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '25' style='width:95%' value='"
                            + GetSafeData.ValidateDataRow_String(dtPublish.Rows[i], "Effect") + "' class='tdinput' id='txtEffect_" + (i + 1).ToString() + "'></td>");
                //发布状态
                sbPublishInfo.AppendLine("<td class='tdColInputCenter'>"
                        + InitPublishDropDownList("ddlStatus_" + (i + 1).ToString(), GetSafeData.ValidateDataRow_String(dtPublish.Rows[i], "Status")) + "</td>");


                //插入行结束标识
                sbPublishInfo.AppendLine("</tr>");
            }
        }
        //信息发布设置到DIV中表示
        divRectPublishDetail.InnerHtml = CreatePublishTable() + sbPublishInfo.ToString() + EndTable();
    }
    #endregion
public static string GetWorkAge(string i)
{
    if (i=="1")
    {
        return " <option value=\"0\">--请选择--</option><option value=\"1\" selected >在读学生</option> <option value=\"2\">应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="2")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\" selected>应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="3")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\" selected>一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="4")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\" selected>一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="5")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\" selected>三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="6")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\" selected>五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="7")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\" selected>十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="8")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\" selected>二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else if (i=="9")
    {
         return " <option value=\"0\">--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\"  >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\" selected>退休人员</option>";
    }
    else if (i == "0")
    {
        return " <option value=\"0\" selected>--请选择--</option><option value=\"1\"  >在读学生</option> <option value=\"2\" >应届毕业生</option><option value=\"3\">一年以内</option><option value=\"4\">一年以上</option><option value=\"5\">三年以上</option><option value=\"6\">五年以上</option><option value=\"7\">十年以上</option><option value=\"8\">二十年以上</option> <option value=\"9\">退休人员</option>";
    }
    else
    {
        return null;
    }



}
    #region 设置招聘目标
    /// <summary>
    /// 设置招聘目标
    /// </summary>
    /// <param name="dtGoal">招聘目标信息</param>
    private void InitGoalInfo(DataTable dtGoal)
    {
        //定义保存招聘目标的变量
        StringBuilder sbGoalInfo = new StringBuilder();

        //招聘目标存在时，设置招聘目标
        if (dtGoal != null && dtGoal.Rows.Count > 0)
        {
            for (int i = 0; i < dtGoal.Rows.Count; i++)
            {
                //插入行开始标识
                sbGoalInfo.AppendLine("<tr>");
                //选择框 
                sbGoalInfo.AppendLine("<td class='tdColInputCenter' ><input type='checkbox' id='tblRectGoalDetailInfo_chkSelect_" + (i + 1).ToString() + "'>");
                //部门ID
                sbGoalInfo.AppendLine("<input type='hidden' id='hidDeptID_" + (i + 1).ToString() + "' value='" 
                                        + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "ApplyDept") + "'></td>");
                //部门
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '10' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "DeptName") + "' class='tdinput' id='DeptrtName_" + (i + 1).ToString() + "' onclick=\"alertdiv('DeptrtName_" + (i + 1).ToString() + ",hidDeptID_" + (i + 1).ToString() + "');\"></td>");
           
                //岗位
                //sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '50' value='"
                //            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PositionTitle") + "' class='tdinput' id='txtPositionTitle_" + (i + 1).ToString() + "'></td>");

                sbGoalInfo.AppendLine("<td class='tdColInput'><input type=\"hidden\" id=\"DeptQuarter" + (i + 1).ToString() + "Hidden\"  value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PositionID") + "'/> <input id=\"DeptQuarter" + (i + 1).ToString() + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\"  value='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PositionTitle") + "'/></td>");

                //objTD.innerHTML = "<input type=\"hidden\" id=\"DeptQuarter" + (i + 1).ToString() + "Hidden\"  value='"
                //            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PositionID") + "'/> <input id=\"DeptQuarter" + (i + 1).ToString() + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\"  value='"    + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PositionTitle") + "'/> ";


                //人员数量
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '3' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "PersonCount") + "' class='tdinput' id='txtPersonCount_" + (i + 1).ToString() + "'  onchange='GetRequireNum();'></td>");
                //性别
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>"
                        + InitSexDropDownList("ddlSex_" + (i + 1).ToString(), GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "Sex")) + "</td>");
                //年龄
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '25' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "Age") + "' class='tdinput' id='txtAge_" + (i + 1).ToString() + "'></td>");

                sbGoalInfo.AppendLine("<td class='tdColInput'><select class='tdinput'id='txtWorkAge_" + (i + 1).ToString() + "'>" + GetWorkAge(GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "WorkAge")) + "</select></td>");

                //学历
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>" +
                    CodePublicTypeBus.CreateSelectInputControlString(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_CULTURE
                    , "ddlCultureLevel_" + (i + 1).ToString(), "tdinput", true, GetSafeData.GetStringFromInt(dtGoal.Rows[i], "CultureLevel"))
                        + "</td>");
                //专业
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>" +
                    CodePublicTypeBus.CreateSelectInputControlString(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_PROFESSIONAL
                    , "ddlProfessional_" + (i + 1).ToString(), "tdinput", true , GetSafeData.GetStringFromInt(dtGoal.Rows[i], "Professional"))
                        + "</td>");
                //要求
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '500' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "Requisition") + "' class='tdinput' id='txtRequisition_" + (i + 1).ToString() + "'></td>");
                //计划完成时间
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'><input type='text' readonly maxlength = '10' value='"
                            + GetSafeData.GetStringFromDateTime(dtGoal.Rows[i], "CompleteDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtCompleteDate_" + (i + 1).ToString() + "' onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCompleteDate_" + (i + 1).ToString() + "')})\"></td>");
                
                //插入行结束标识
                sbGoalInfo.AppendLine("</tr>");
            }
        }

        //招聘目标设置到DIV中表示
        divRectGoalDetail.InnerHtml = CreateGoalTable() + sbGoalInfo.ToString() + EndTable();
    }
    #endregion



    #region 设置性别下拉框
    /// <summary>
    /// 设置性别下拉框
    /// </summary>
    /// <param name="ctronlID">控件ID</param>
    /// <param name="selectValue">选中值</param>
    /// <returns></returns>
    private string InitSexDropDownList(string ctronlID, string selectValue)
    {
        //定义返回的变量
        StringBuilder inputSelect = new StringBuilder();
        //开始标识
        inputSelect.AppendLine("<select id='" + ctronlID + "' class='tdColInputs'>");
        //生成选择项//添加选择项
        if ("1".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value='3'>" + "不限</option>");
            inputSelect.AppendLine("<option value='1' selected>" + "男</option>");
            inputSelect.AppendLine("<option value='2'>" + "女</option>");
        }
        else if ("2".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value='3'>" + "不限</option>");
            inputSelect.AppendLine("<option value='1'>" + "男</option>");
            inputSelect.AppendLine("<option value='2' selected>" + "女</option>");
        }
        else
        {
            inputSelect.AppendLine("<option value='3' selected>" + "不限</option>");
            inputSelect.AppendLine("<option value='1'>" + "男</option>");
            inputSelect.AppendLine("<option value='2'>" + "女</option>");
        }
        //结束标识
        inputSelect.AppendLine("</select>");
        //返回生成的字符串
        return inputSelect.ToString();
    }
    #endregion

    #region 设置发布状态下拉框
    /// <summary>
    /// 设置发布状态下拉框
    /// </summary>
    /// <param name="ctronlID">控件ID</param>
    /// <param name="selectValue">选中值</param>
    /// <returns></returns>
    private string InitPublishDropDownList(string ctronlID, string selectValue)
    {
        //定义返回的变量
        StringBuilder inputSelect = new StringBuilder();
        //开始标识
        inputSelect.AppendLine("<select id='" + ctronlID + "' class='tdColInput'>");
        //生成选择项//添加选择项
        if ("0".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value='0' selected>" + "暂停</option>");
            inputSelect.AppendLine("<option value='1'>" + "发布中</option>");
            inputSelect.AppendLine("<option value='2'>" + "结束</option>");
        }
        else if ("1".Equals(selectValue))
        {
            inputSelect.AppendLine("<option value='0'>" + "暂停</option>");
            inputSelect.AppendLine("<option value='1' selected>" + "发布中</option>");
            inputSelect.AppendLine("<option value='2'>" + "结束</option>");
        }
        else
        {
            inputSelect.AppendLine("<option value='0'>" + "暂停</option>");
            inputSelect.AppendLine("<option value='1'>" + "发布中</option>");
            inputSelect.AppendLine("<option value='2' selected>" + "结束</option>");
        }
        //结束标识
        inputSelect.AppendLine("</select>");
        //返回生成的字符串
        return inputSelect.ToString();
    }
    #endregion

    #region 生成招聘目标表格的标题部分
    /// <summary>
    /// 生成招聘目标表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateGoalTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblRectGoalDetailInfo'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='50'>");
        table.AppendLine(" <input type='checkbox' name='tblRectGoalDetailInfo_chkAll' id='tblRectGoalDetailInfo_chkAll' onclick='SelectAll(\"tblRectGoalDetailInfo\");'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle'>招聘部门</td>");
        table.AppendLine("<td class='ListTitle'>岗位名称</td>");
        table.AppendLine("<td class='ListTitle'>人员数量</td>");
        table.AppendLine("<td class='ListTitle'>性别</td>");
        table.AppendLine("<td class='ListTitle'>年龄</td>");
        table.AppendLine("<td class='ListTitle'>工作年限</td>");
        table.AppendLine("<td class='ListTitle'>学历</td>");
        table.AppendLine("<td class='ListTitle'>专业</td>");
        table.AppendLine("<td class='ListTitle'>工作要求</td>");
        table.AppendLine("<td class='ListTitle'>计划完成时间</td>");
        table.AppendLine("</tr>");
        //返回表格语句
        return table.ToString();
    }
    #endregion

    

    #region 生成信息发布表格的标题部分
    /// <summary>
    /// 生成信息发布表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreatePublishTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblRectPublishDetailInfo'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='50'>");
        table.AppendLine(" <input type='checkbox' name='tblRectPublishDetailInfo_chkAll' id='tblRectPublishDetailInfo_chkAll' onclick='SelectAll(\"tblRectPublishDetailInfo\");'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle'>发布媒体和渠道</td>");
        table.AppendLine("<td class='ListTitle'>发布时间</td>");
        table.AppendLine("<td class='ListTitle'>有效时间(天)</td>");
        table.AppendLine("<td class='ListTitle'>截止时间</td>");
        table.AppendLine("<td class='ListTitle'>费用</td>");
        table.AppendLine("<td class='ListTitle'>效果</td>");
        table.AppendLine("<td class='ListTitle'>发布状态</td>");
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
