/**********************************************
 * 类作用：   新建调职申请单
 * 建立人：   吴志强
 * 建立时间： 2009/04/22
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

public partial class Pages_Office_HumanManager_EmplApply_Edit : BasePage
{
    /// <summary>
    /// 类名：EmplApply_Edit
    /// 描述：新建调职申请单
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/22
    /// 最后修改时间：2009/04/22
    /// </summary>
    ///

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示设置
        if (!IsPostBack)
        {
            #region 新建、修改共通处理
            //目前岗位
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlNowQuarter.DataSource = dtQuarter;
            ddlNowQuarter.DataValueField = "ID";
            ddlNowQuarter.DataTextField = "QuarterName";
            ddlNowQuarter.DataBind();
            //目前岗位职等
            ctNowQuaterAdmin.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctNowQuaterAdmin.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            //调至岗位
            ddlNewQuarter.DataSource = dtQuarter;
            ddlNewQuarter.DataValueField = "ID";
            ddlNewQuarter.DataTextField = "QuarterName";
            ddlNewQuarter.DataBind();
            //调至岗位职等
            ctNewQuaterAdmin.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctNewQuaterAdmin.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLAPPLY_INFO;
            FlowEmplApply.BillTypeFlag = ConstUtil.CODING_TYPE_HUMAN;
            FlowEmplApply.BillTypeCode = ConstUtil.CODING_HUMAN_ITEM_EMPLAPPLY;

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

            //获取ID
            string ID = Request.QueryString["ID"];
            //ID为空时，为新建模式
            if (string.IsNullOrEmpty(ID))
            {
                #region 新建时初期处理
                //编号初期处理
                codeRule.CodingType = ConstUtil.CODING_TYPE_HUMAN;
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_EMPLAPPLY;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置ID
                //hidIdentityID.Value = "0";
                //申请日期
                txtApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //申请人
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                UserApply.Text = userInfo.EmployeeName;
                hidUserApply.Value = userInfo.EmployeeID.ToString();
                //目前部门
                DeptNow.Text = userInfo.DeptName;
                hidDeptNow.Value = userInfo.DeptID.ToString();
                //Response.Write("<script language='javascript'>();</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>GetFlowButton_DisplayControl();</script>");
                //目前岗位
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "调职申请";
                //获取并设置面试记录信息
                //InitEmplApplyInfo(ID);
                //设置编辑模式
                hidIdentityID.Value = ID;
                #endregion
            }
        }
    }
    #endregion

    #region 根据调职申请ID，获取信息，并设置到页面显示
    /// <summary>
    /// 根据调职申请ID，获取信息，并设置到页面显示
    /// </summary>
    /// <param name="ID">调职申请ID</param>
    private void InitEmplApplyInfo(string ID)
    {
        //设置编号可见
        divCodeNo.Attributes.Add("style", "display:block;");
        //自动生成编号的控件设置为不可见
        divCodeRule.Attributes.Add("style", "display:none;");

        //查询信息
        DataTable dtBaseInfo = EmplApplyBus.GetEmplApplyInfoWithID(ID);
        //基本信息存在时
        if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
        {
            #region 设置基本信息
            if (GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Status") == "0")//未办理
            {
                hidIdentityID.Value = ID.ToString();
                hiddenBillStatus.Value = "0";
                hidBillNo.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmplApplyNo").ToString();
            }
            else
            {
                hidIdentityID.Value = ID.ToString();
                hiddenBillStatus.Value = "1";
                hidBillNo.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmplApplyNo").ToString();
            }
            //编号
            divCodeNo.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmplApplyNo");
            //主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Title");
            //申请人
            UserApply.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeName");
            hidUserApply.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeID");
            //入职时间
            txtEnterDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EnterDate");
            //申请日期
            txtApplyDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ApplyDate");
            //希望日期
            txtHopeDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "HopeDate");
            //申报类别
            txtApplyType.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ApplyType");
            //目前工资
            txtNowWage.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NowWage");
            //目前部门
            DeptNow.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NowDeptName");
            hidDeptNow.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NowDeptID");
            //目前岗位
            ddlNowQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NowQuarterID");
            //目前岗位职等
            ctNowQuaterAdmin.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NowAdminLevelID");
            //调至工资
            txtNewWage.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NewWage");
            //调至部门
            DeptNew.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NewDeptName");
            hidDeptNew.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NewDeptID");
            //调至岗位
            ddlNewQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NewQuarterID");
            //调至岗位职等
            ctNewQuaterAdmin.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "NewAdminLevelID");
            //事由
            txtReason.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Reason");
            //备注
            txtRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Remark");
            #endregion
        }
    }
    #endregion

}
