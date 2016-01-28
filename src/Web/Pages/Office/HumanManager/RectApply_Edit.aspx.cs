/**********************************************
 * 类作用：   新建招聘申请
 * 建立人：   吴志强
 * 建立时间： 2009/03/27
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_RectApply_Edit : BasePage
{

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示处理
        if (!IsPostBack)
        {

            #region 新建、修改共通初期设置
            //页面初期设置
            //学历

            ddlCulture.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlCulture.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            ddlCulture.IsInsertSelect = true;
            //专业
            ddlProfessional.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ddlProfessional.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            ddlProfessional.IsInsertSelect = true;
            //提交审批调用方法例子
            FlowRectApply.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_HUMAN;
            FlowRectApply.BillTypeCode = ConstUtil.BILL_TYPECODE_HUMAN_RECT_APPLY;

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_RECTAPPLY_INFO;
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

            #region 页面内容初期设置
            //获取人才代理编号
            string rectApplyID = Request.QueryString["RectApplyID"];
            if (Request.QueryString["hidIsliebiao"] != null)
            {
                hidIsliebiao.Value  = "1";
            }

            //rectApplyID = "38";
            //申请招聘ID为空，为新建模式
            if (string.IsNullOrEmpty(rectApplyID))
            {
                //编号初期处理
                codruleRectApply.CodingType = ConstUtil.CODE_TYPE_HUMAN;
                codruleRectApply.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_RECT;
                //设置申请招聘编号不可见
                divRectApplyNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                //申请日期
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                txtCreator.Text = userInfo.EmployeeName;
                DeptApply.Text = userInfo.DeptName;
                hidDeptID.Value = userInfo.DeptID.ToString();
                txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                divRectGoalDetail.InnerHtml = CreateGoalTable() + EndTable();
            }
            else
            {
                //设置标题
                divTitle.InnerText = "招聘申请";
                //设置申请招聘编号可见
                divRectApplyNo.Attributes.Add("style", "display:block;");
                //自动生成编号的控件设置为不可见
                divCodeRule.Attributes.Add("style", "display:none;");
                //获取并设置申请招聘信息
                InitRectApplyInfo(rectApplyID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
            }

            #endregion

        }
    }
    #endregion

    #region 设置招聘申请信息
    /// <summary>
    /// 设置招聘申请信息
    /// </summary>
    /// <param name="rectApplyID">招聘申请ID</param>
    private void InitRectApplyInfo(string rectApplyID)
    {
        ////查询招聘申请信息
         DataTable dtRectApply = RectApplyBus.GetRectApplyInfoWithID(rectApplyID);
         //数据存在时
         if (dtRectApply != null && dtRectApply.Rows.Count > 0)
         {
             // hiddenBillStatus.Value = "2";
             //申请编号
             string billNo = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "RectApplyNo");
             divRectApplyNo.InnerHtml = billNo;
             hidBillNo.Value = billNo;
             txtIndentityID.Value = rectApplyID;
             //申请部门
             DeptApply.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "DeptName");
             hidDeptID.Value = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "DeptID");
             //直接主管
             txtMaxNum.Value = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "MaxNum"); 
             //职位名称   
             txtNowNum.Value = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "NowNum");
             //招聘人数
             txtRequireNum.Value = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "RequireNum");
             txtPrincipal.Value = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "Principal");
             UserApplyUserName.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "PrincipalName");
             txtRequstReason.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "RequstReason");
             //备注
             txtRemark.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "Remark");
             txtBillStatus.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "BillStatus");
             string FlowStatusName = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "FlowStatusName");
             if (txtBillStatus.Text == "制单")
             {
                 hiddenBillStatus.Value = "1";
                 if (FlowStatusName == "审批通过")
                 {
                     // btnSave.Attributes.Add("style", "display:none");
                     btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                     btnSave.Attributes.Add("onclick", "");
                 }
             }
             else if (txtBillStatus.Text == "执行")
             {
                 hiddenBillStatus.Value = "2";
                 //btnSave.Attributes.Add("style", "display:none");
                 btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                 btnSave.Attributes.Add("onclick", "");
             }
             else if (txtBillStatus.Text == "手工结单")
             {
                 hiddenBillStatus.Value = "4";
                 //btnSave.Attributes.Add("style", "display:none");
                 btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                 btnSave.Attributes.Add("onclick", "");
             }
             if (FlowStatusName != null)
             {
                 if (FlowStatusName == "待审批")
                 {
                     // btnSave.Attributes.Add("style", "display:none");
                     btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                     btnSave.Attributes.Add("onclick", "");
                 }
             }
             txtCreator.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "Creator");
             this.txtConfirmor.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "Confirmor");
             this.txtCloser.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "Closer");
             this.txtCreateDate.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "CreateDate");
             this.txtConfirmDate.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "ConfirmDate");
             this.txtCloseDate.Text = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "CloseDate");

             string companyCD = GetSafeData.ValidateDataRow_String(dtRectApply.Rows[0], "CompanyCD");
             DataTable dtGoalDetails = RectApplyBus.GetGoalDetailsWithID(billNo, companyCD);

             InitGoalInfo(dtGoalDetails);
         }
    }
    #endregion

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
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='checkbox' id='tblRectGoalDetailInfo_chkSelect_" + (i + 1).ToString() + "'>"); 
                //岗位 
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type=\"hidden\" id=\"DeptQuarter" + (i + 1).ToString() + "Hidden\"  value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "JobID") + "'/> <input id=\"DeptQuarter" + (i + 1).ToString() + "\" type=\"text\"  reado     maxlength =\"30\" class=\"tdinput\"       onclick =\"treeveiwPopUp.show()\" readonly=\"readonly\"  value='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "JobName") + "'/></td>");
                //职务说明
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '100' size='10' class='tdinput'  id='txtJobDescripe_" + (i + 1).ToString() + "'  value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "JobDescripe") + "'  ondblclick ='alertContent(this.id)' ></td>"); 
                //需求人数
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '3' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "RectCount") + "' class='tdinput' id='txtPersonCount_" + (i + 1).ToString() + "' onchange='GetRequireNum();'></td>");
                //最迟上岗时间
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'><input type='text' readonly maxlength = '10' value='"
                            + GetSafeData.GetStringFromDateTime(dtGoal.Rows[i], "UsedDate", "yyyy-MM-dd")
                            + "' class='tdinput' id='txtUsedDate_" + (i + 1).ToString() + "' onclick=\"J.calendar.get();\"></td>");
                //工作地点
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '100' size='10' class='tdinput'  id='txtWorkPlace_" + (i + 1).ToString() + "'   value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "WorkPlace") + "'  ondblclick   ='alertContent(this.id)'></td>");
                //工作性质
                sbGoalInfo.AppendLine("<td class='tdColInput'>"
                        +  InitWorkNatureDropDownList("ddlWorkNature_" + (i + 1).ToString(), GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "WorkNature")) + "</td>"); 
                //性别
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>"
                        + InitSexDropDownList("ddlSex_" + (i + 1).ToString(), GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "Sex")) + "</td>");


                //起始年龄
                sbGoalInfo.AppendLine("<td class='tdColInput'><input  type='text' maxlength = '3' size='3' class='tdinput' id='txtMinAge_" + (i + 1).ToString() + "' onkeydown='Numeric_OnKeyDown();'  value='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "MinAge") + "'></td>");
                //截止年龄
                sbGoalInfo.AppendLine("<td class='tdColInput'><input  type='text' maxlength = '3' size='3' class='tdinput' id='txtMaxAge_" + (i + 1).ToString() + "' onkeydown='Numeric_OnKeyDown();'  value='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "MaxAge") + "'></td>");

                //专业
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>" +
                    CodePublicTypeBus.CreateSelectInputControlString(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_PROFESSIONAL
                    , "ddlProfessional_" + (i + 1).ToString(), "tdinput", true, GetSafeData.GetStringFromInt(dtGoal.Rows[i], "Professional"))
                        + "</td>");

                //学历
                sbGoalInfo.AppendLine("<td class='tdColInputCenter'>" +
                    CodePublicTypeBus.CreateSelectInputControlString(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_CULTURE
                    , "ddlCultureLevel_" + (i + 1).ToString(), "tdinput", true, GetSafeData.GetStringFromInt(dtGoal.Rows[i], "CultureLevel"))
                        + "</td>");

                //工作年限
                sbGoalInfo.AppendLine("<td class='tdColInput'>"
                        + InitWorkAgeDropDownList("ddlWorkAge_" + (i + 1).ToString(), GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "WorkAge")) + "</td>");


                //工作要求
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '1000' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "WorkNeeds") + "' class='tdinput' id='txtRequisition_" + (i + 1).ToString() + "' ondblclick   ='alertContent(this.id)'  ></td>");

                //其他要求
                sbGoalInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength = '1000' value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "OtherAbility") + "' class='tdinput' id='txtOtherAbility_" + (i + 1).ToString() + "' title='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "OtherAbility") + "' ondblclick   ='alertContent(this.id)' ></td>");
                //其他要求
                sbGoalInfo.AppendLine("<td class='tdColInput'  title='" + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "SalaryNote") + "'  ><input type='text' maxlength = '1000'      value='"
                            + GetSafeData.ValidateDataRow_String(dtGoal.Rows[i], "SalaryNote") + "' class='tdinput' id='txtSalaryNote_" + (i + 1).ToString() + "' ondblclick   ='alertContent(this.id)' ></td>");
                //插入行结束标识
                sbGoalInfo.AppendLine("</tr>");
            }
        }

        //招聘目标设置到DIV中表示
        divRectGoalDetail.InnerHtml = CreateGoalTable() + sbGoalInfo.ToString() + EndTable();
    }
    private string InitWorkAgeDropDownList(string ctronlID, string selectValue)
    {
        //定义返回的变量
        StringBuilder inputSelect = new StringBuilder();
        //开始标识
        inputSelect.AppendLine("<select id='" + ctronlID + "' class='tdColInputs'>");
        //生成选择项//添加选择项
        if ("1".Equals(selectValue))
        {

            inputSelect.AppendLine("   <option value=\"0\" >--请选择--</option>  <option value=\"1\"  selected >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("2".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\"  selected>应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("3".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\" selected>一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("4".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\" selected>一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("5".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\"   selected>三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("6".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\" selected>五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("7".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\" selected>十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("8".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\" selected>二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        else if ("9".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\"  >--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\" selected>退休人员</option> ");
        }
        else if ("0".Equals(selectValue))
        {
            inputSelect.AppendLine("   <option value=\"0\" selected>--请选择--</option>  <option value=\"1\"  >在读学生</option>  <option value=\"2\">应届毕业生</option> <option value=\"3\">一年以内</option>  <option value=\"4\">一年以上</option>    <option value=\"5\">三年以上</option>  <option value=\"6\">五年以上</option> <option value=\"7\">十年以上</option> <option value=\"8\">二十年以上</option>   <option value=\"9\">退休人员</option> ");
        }
        //结束标识
        inputSelect.AppendLine("</select>");
        //返回生成的字符串
        return inputSelect.ToString();
    }
    private string InitWorkNatureDropDownList(string ctronlID, string selectValue)
    {
        //定义返回的变量
        StringBuilder inputSelect = new StringBuilder();
        //开始标识
        inputSelect.AppendLine("<select id='" + ctronlID + "' class='tdColInputs'>");
        //生成选择项//添加选择项
        if ("1".Equals(selectValue))
        {

            inputSelect.AppendLine(" <option value=\"1\" selected>不限</option> <option value=\"2\"  >全职</option> <option value=\"3\">兼职</option> <option value=\"4\"  >实习</option> "); 
        }
        else if ("2".Equals(selectValue))
        {
            inputSelect.AppendLine(" <option value=\"1\" >不限</option> <option value=\"2\"  selected>全职</option> <option value=\"3\">兼职</option> <option value=\"4\"  >实习</option> "); 
        }
        else if ("3".Equals(selectValue))
        {
            inputSelect.AppendLine(" <option value=\"1\"  >不限</option> <option value=\"2\"  >全职</option> <option value=\"3\" selected>兼职</option> <option value=\"4\"  >实习</option> "); 
        }
        else if ("4".Equals(selectValue))
        {
            inputSelect.AppendLine(" <option value=\"1\"  >不限</option> <option value=\"2\"  >全职</option> <option value=\"3\">兼职</option> <option value=\"4\"  selected>实习</option> ");
        }
        //结束标识
        inputSelect.AppendLine("</select>");
        //返回生成的字符串
        return inputSelect.ToString();
    }
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
    private string CreateGoalTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblRectGoalDetailInfo'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle'  >");
        table.AppendLine("  <input type='checkbox' name='tblRectGoalDetailInfo_chkAll' id='tblRectGoalDetailInfo_chkAll' onclick='SelectAll(\"tblRectGoalDetailInfo\");'/>");
        table.AppendLine("</td>");
        table.AppendLine("<td class='ListTitle'>岗位</td>");
        table.AppendLine("<td class='ListTitle'>职务说明</td>");
        table.AppendLine("<td class='ListTitle'>需求人数</td>");
        table.AppendLine("<td class='ListTitle'>最迟上岗时间</td>");
        table.AppendLine("<td class='ListTitle'>工作地点</td>");
        table.AppendLine("<td class='ListTitle'>工作性质</td>");
        table.AppendLine("<td class='ListTitle'>性别</td>");
        table.AppendLine("<td class='ListTitle'>起始年龄</td>");
        table.AppendLine("<td class='ListTitle'>截止年龄</td>");
        table.AppendLine("<td class='ListTitle'>专业</td>");

        table.AppendLine("<td class='ListTitle'>学历</td>");
        table.AppendLine("<td class='ListTitle'>工作年限</td>");
        table.AppendLine("<td class='ListTitle'>工作要求</td>");
        table.AppendLine("<td class='ListTitle'>其他要求</td>");
        table.AppendLine("<td class='ListTitle'>可提供的待遇</td>");
        table.AppendLine("</tr>");
        //返回表格语句
        return table.ToString();
    }
    private string EndTable()
    {
        return "</table>";
    }
}
