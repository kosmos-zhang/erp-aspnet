/**********************************************
 * 类作用：   新建考试记录
 * 建立人：   吴志强
 * 建立时间： 2009/04/08
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_EmployeeTest_Edit : BasePage
{

    /// <summary>
    /// 类名：EmployeeTest_Edit
    /// 描述：新建考试记录
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/08
    /// 最后修改时间：2009/04/08
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {

            #region 新建、修改共通处理
            //考试记录列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_TEST_INFO;
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

            //获取考试记录ID
            string testID = Request.QueryString["TestID"];
            //考试记录ID为空时，为新建模式
            if (string.IsNullOrEmpty(testID))
            {
             
                #region 新建时初期处理
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_TEST;
                //设置培训编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
                //考试结果
                divTestResultDetail.InnerHtml = CreateTestResultTable() + EndTable();
                #endregion
            }
            else
            {
                string TestStr = "var testID='" + testID + "'";
                ClientScript.RegisterStartupScript(this.GetType(), "testID1", TestStr, true);
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "考试记录";
               // $("#divCodeNo").attr("disabled",true);
                divCodeNo.Enabled=false;
                //获取并设置考试记录信息
                InitTestInfo(testID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
                #endregion
            }
        }
    }

    #region 根据考试记录ID，获取考试记录信息，并设置到页面显示
    /// <summary>
    /// 根据考试记录ID，获取考试记录信息，并设置到页面显示
    /// </summary>
    /// <param name="asseID">考核ID</param>
    private void InitTestInfo(string testID)
    {
        //查询考试信息
        DataSet dsTestInfo = EmployeeTestBus.GetTestInfoWithID(testID);
        //获取考试基本信息
        DataTable dtBaseInfo = dsTestInfo.Tables[0];
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置考试记录基本信息
            //设置培训编号不可见
            divCodeNo.Attributes.Add("style", "display:block;");
            divCodeNo.Attributes.Add("style", "width:60%;");
            //自动生成编号的控件设置为可见
            divCodeRule.Attributes.Add("style", "display:none;");
            //考试编号
            divCodeNo.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TestNo");
            //主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Title");
            //考试负责人
            UserTeacher.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeName");
            txtTeacher.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Teacher");
            //开始时间
            txtStartDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "StartDate");
            //结束时间
            txtEndDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EndDate");
            //考试地点
            txtAddress.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Addr");
            //考试结果
            txtTestResult.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TestResult");
            //考试状态
            ddlStatus.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Status");
            //附件
            string attach = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Attachment");
            //附件未上传时
            if (string.IsNullOrEmpty(attach))
            {
                divUploadAttachment.Attributes.Add("style", "display:block;");
                divDealAttachment.Attributes.Add("style", "display:none;");
            }
            //上传了附件时
            else
            {
                divUploadAttachment.Attributes.Add("style", "display:none;");
                divDealAttachment.Attributes.Add("style", "display:block;");
            }
            //
            spanAttachmentName.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "AttachmentName");
            //设置附件
            hfAttachment.Value = attach;
            hfPageAttachment.Value = attach;
            //考试内容摘要
            txtContent.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "TestContent");
            //备注
            txtRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Remark");
            //缺考人数
            txtAbsenceCount.Text = GetSafeData.GetStringFromInt(dtBaseInfo.Rows[0], "AbsenceCount");
            #endregion

            //设置考试结果
            InitTestResultInfo(dsTestInfo.Tables[1]);
        }
    }
    #endregion

    #region 设置考试结果
    /// <summary>
    /// 设置考试结果
    /// </summary>
    /// <param name="dtResult">考试结果信息</param>
    private void InitTestResultInfo(DataTable dtResult)
    {
        //定义保存进度安排的变量
        StringBuilder sbResultInfo = new StringBuilder();
        //人员ID
        StringBuilder sbUserIDInfo = new StringBuilder();
        //人员名
        StringBuilder sbUserNameInfo = new StringBuilder();
        //换行标识
        int flag = 0;
        //进度安排存在时，设置进度安排
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                /* 每行显示两条数据，所以通过对3取余来判断是否换行 以及 行是否结束 */
                //对3进行取余
                flag = i % 3;
                //余数为0时，表示需要新添加一行
                if (flag == 0)
                {
                    //插入行开始标识
                    sbResultInfo.AppendLine("<tr>");
                }
                //获取人员ID
                string employID = GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "EmployeeID");
                //获取人员名
                string employName = GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "EmployeeName");
                //姓名
                sbResultInfo.AppendLine("<td class='tdColInputCenter'><input type='hidden' value='"
                            + "' class='tdinput' id='txtUser_" + employID + "'>" + employName + "</td>");
                //考核得分
                sbResultInfo.AppendLine("<td class='tdColInput'><input type='text' maxlength='4' style='width:90%;' maxlength = '6' value='"
                            + GetSafeData.ValidateDataRow_String(dtResult.Rows[i], "TestScore")
                            + "' class='tdinput' id='txtUser_" + employID + "_Score'></td>");
                //设置人员ID
                sbUserIDInfo.Append("" + employID + ",");
                //设置人员名
                sbUserNameInfo.Append(employName + ",");
                //余数为2时，表示行结束
                if (flag == 2)
                {
                    //插入行结束标识
                    sbResultInfo.AppendLine("</tr>");
                }
            }
            //余数为0时，表示所有的记录为单数条，因此需要把未结束的行结束
            if (flag < 2)
            {
                int j = 2 - flag;
                for (int p = 0; p < j; p++)
                {
                    //插入空白列
                    sbResultInfo.AppendLine("<td class='tdColInputCenter'></td><td class='tdColInput'></td>");
                }
                //插入行结束标识
                sbResultInfo.AppendLine("</tr>");
            }
            //人员ID
            txtJoinUserID.Value = sbUserIDInfo.ToString().TrimEnd(',');
            txtJoinUserID4Modify.Value = sbUserIDInfo.ToString().TrimEnd(',');
            //人员名
            UserJoinName.Text = sbUserNameInfo.ToString().TrimEnd(',');
        }
        //设置参与人员信息
        //考试记录设置到DIV中表示
        divTestResultDetail.InnerHtml = CreateTestResultTable() + sbResultInfo.ToString() + EndTable();
    }
    #endregion

    #region 生成考试结果表格的标题部分
    /// <summary>
    /// 生成考试结果表格的标题部分
    /// </summary>
    /// <returns></returns>
    private string CreateTestResultTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table id='tblTestResultDetail'  width='100%' border='0'align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("<tr>");
        table.AppendLine("<td class='ListTitle' width='10%'>姓名</td>");
        table.AppendLine("<td class='ListTitle' width='23%'>考试得分</td>");
        table.AppendLine("<td class='ListTitle' width='10%'>姓名</td>");
        table.AppendLine("<td class='ListTitle' width='23%'>考试得分</td>");
        table.AppendLine("<td class='ListTitle' width='10%'>姓名</td>");
        table.AppendLine("<td class='ListTitle' width='24%'>考试得分</td>");
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
