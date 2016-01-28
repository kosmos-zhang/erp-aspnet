/**********************************************
 * 类作用：   新建调职
 * 建立人：   吴志强
 * 建立时间： 2009/04/24
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Web.UI.WebControls;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_EmployeeShift_Edit : BasePage
{
    /// <summary>
    /// 类名：EmployeeShift_Edit
    /// 描述：新建调职
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/24
    /// 最后修改时间：2009/04/24
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示设置
        if (!IsPostBack)
        {
            #region 新建、修改共通处理

            //获取申请单数据
            DataTable dtApply = EmplApplyNotifyBus.GetApplyInfo();
            ddlApply.DataSource = dtApply;
            ddlApply.DataValueField = "EmplApplyNo";
            ddlApply.DataTextField = "Title";
            ddlApply.DataBind();
            ddlApply.Attributes.Add("onchange", "DoApplyChange();");
            //添加一请选择选项
            ListItem Item = new ListItem(ConstUtil.CODE_TYPE_INSERT_TEXT, ConstUtil.CODE_TYPE_INSERT_VALUE);
            ddlApply.Items.Insert(0, Item);
            //调至岗位
            DataTable dtQuarter = DeptQuarterBus.GetQuarterInfoWithCompanyCD();
            ddlNewQuarter.DataSource = dtQuarter;
            ddlNewQuarter.DataValueField = "ID";
            ddlNewQuarter.DataTextField = "QuarterName";
            ddlNewQuarter.DataBind();
            //调入岗位职等
            ctNewQuaterAdmin.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            ctNewQuaterAdmin.TypeCode = ConstUtil.CODE_TYPE_QUARTER_ADMIN;
            //获取申请单数据

            //模板列表模块ID
            hidListModuleID.Value = ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_SHIFT_INFO;
            hidFastModuleID.Value = ConstUtil.MODULE_ID_HUMAN_FAST_SHIFT;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_EMPLOYEE_SHIFT_EDIT, string.Empty);
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
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_SHIFT;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置ID
                hidIdentityID.Value = string.Empty;
                //制单日期
                txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //创建人姓名
                UserCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                //创建人ID
                txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                //获取员工ID
                string employeeID = Request.QueryString["EmployeeID"];
                //快速调职页面迁移过来时
                if (!string.IsNullOrEmpty(employeeID))
                {
                    //设置员工信息
                    InitEmployeInfo(employeeID);
                }
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "调职单";
                //自动生成编号的控件设置不可见
                divCodeRule.Attributes.Add("style", "display:none;");
                //编号设置为可见
                divCodeNo.Attributes.Add("style", "display:block;");
                divCodeNo.Attributes.Add("style", "width:60%;");
                //获取并设置信息
                InitNotifyInfo(ID);
                //设置编辑模式
                hidIdentityID.Value = ID;
                #endregion
            }
        }
    }

    #region 获取并设置人员信息
    /// <summary>
    /// 获取并设置人员信息
    /// </summary>
    /// <param name="ID">ID</param>
    private void InitEmployeInfo(string ID)
    {
        //获取申请单数据
        DataTable dtEmpl = EmployeeInfoBus.GetEmplDeptInfoWithID(ID);
        //数据存在时，设置值
        if (dtEmpl != null && dtEmpl.Rows.Count > 0)
        {
            //调职人
            txtEmployeeID.Value = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "ID");
            divCodeNo.Attributes.Add("style", "width:60%;");
            //员工编号
            txtEmployeeNo.Text = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "EmployeeNo");
            //员工名
            txtEmployeeName.Text = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "EmployeeName");
            //原部门名称
            txtNowDeptName.Text = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "DeptName");
            //原部门ID
            txtNowDeptID.Value = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "DeptID");
            //原岗位名
            txtNowQuarterName.Text = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "QuarterName");
            //原岗位ID
            txtNowQuarterID.Value = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "QuarterID");
            //原岗位职等名称
            txtNowQuarterAdminName.Text = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "AdminLevelName");
            //原岗位职等ID
            txtNowQuarterAdminID.Value = GetSafeData.ValidateDataRow_String(dtEmpl.Rows[0], "AdminLevelID");

            //设置不可编辑
            txtEmployeeNo.Enabled = false;
        }
    }
    #endregion

    #region 获取并设置页面信息
    /// <summary>
    /// 获取并设置页面信息
    /// </summary>
    /// <param name="ID">ID</param>
    private void InitNotifyInfo(string ID)
    {
        //获取申请单数据
        DataTable dtNotify = EmplApplyNotifyBus.GetEmplApplyNotifyInfoWithID(ID);
        //数据存在时，设置值
        if (dtNotify != null && dtNotify.Rows.Count > 0)
        {
            //调职单编号
            divCodeNo.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NotifyNo");
            //调职单主题
            txtTitle.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Title");
            //对应申请单
            string emlpApplyNo = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "EmplApplyNo");
            ddlApply.SelectedValue = emlpApplyNo;
            //调职人
            txtEmployeeID.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "EmployeeID");
            //员工编号
            txtEmployeeNo.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "EmployeeNo");
            //员工编号不可编辑
            txtEmployeeNo.Enabled = false;
            //员工名
            txtEmployeeName.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "EmployeeName");
            //事由
            txtReason.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Reason");
            //原部门ID
            txtNowDeptID.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowDeptID");
            //原部门名称
            txtNowDeptName.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowDeptName");
            //原岗位名
            txtNowQuarterName.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowQuarterName");
            //原岗位ID
            txtNowQuarterID.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowQuarterID");
            //原岗位职等名称
            txtNowQuarterAdminName.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowAdminLevelName");
            //原岗位职等ID
            txtNowQuarterAdminID.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NowAdminLevel");
            //调入部门ID
            txtNewDept.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NewDeptID");
            //调入部门名称
            DeptNew.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NewDeptName");
            //调入岗位
            ddlNewQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NewQuarterID");
            //调入岗位职等
            ctNewQuaterAdmin.SelectedValue = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "NewAdminLevel");
            //调出时间
            txtOutDate.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "OutDate");
            //调入时间
            txtIntDate.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "IntDate");
            //制单人
            txtCreator.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Creator");
            UserCreator.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "CreatorName");
            UserCreator.Enabled = false;
            //制单日期
            txtCreateDate.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "CreateDate");
            txtCreateDate.Enabled = false;
            if (GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Confirmor") != "")
            {
                //确认人
                txtConfirmor.Value = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Confirmor");
                UserConfirmor.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "ConfirmorName");
                //确认日期
                txtConfirmDate.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "ConfirmDate");
                btnSave.Src = "../../../Images/Button/UnClick_bc.jpg";
                btnConfirm.Src = "../../../Images/Button/UnClick_qr.jpg";
                //btnConfirm.Attributes.Add("onclick", "DoConfirm();");

                //btnSave.Attributes.Add("style", "display:none;");
                //UnSave.Attributes.Add("style", "display:block;");
            }
            else
            {
                btnSave.Src = "../../../Images/Button/Bottom_btn_save.jpg";
                btnConfirm.Src = "../../../Images/Button/Bottom_btn_confirm.jpg";
                btnConfirm.Attributes.Add("onclick", "DoConfirm();");
            }

            
            //备注
            txtRemark.Text = GetSafeData.ValidateDataRow_String(dtNotify.Rows[0], "Remark");

            //申请编号存在时
            if (!string.IsNullOrEmpty(emlpApplyNo))
            {
                //调至部门不可编辑
                DeptNew.Enabled = false;
                //调至岗位
                ddlNewQuarter.Enabled = false;
                //调至岗位职等不可编辑
                ctNewQuaterAdmin.Enabled = false;
            }

            //设置确定按钮
            //btnConfirm.Src = "../../../Images/Button/Bottom_btn_confirm.jpg";
            //btnConfirm.Attributes.Add("onclick", "DoConfirm();");
        }
    }
    #endregion
}
