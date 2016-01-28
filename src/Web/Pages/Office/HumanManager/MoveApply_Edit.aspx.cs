/**********************************************
 * 类作用：   新建离职申请单
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

public partial class Pages_Office_HumanManager_MoveApply_Edit : BasePage
{
    /// <summary>
    /// 类名：MoveApply_Edit
    /// 描述：新建离职申请单
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

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_MOVEAPPLY_INFO;
            FlowEmplApply.BillTypeFlag = ConstUtil.CODING_TYPE_HUMAN;
            FlowEmplApply.BillTypeCode = ConstUtil.CODING_HUMAN_ITEM_MOVEAPPLY;

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
                codeRule.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_MOVEAPPLY;
                //设置编号不可见
                divCodeNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置ID
                hidIdentityID.Value = "0";
                //申请日期
                txtApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //申请人
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                UserApply.Text = userInfo.EmployeeName;
                hidUserApply.Value = userInfo.EmployeeID.ToString();
                //目前部门
                DeptNow.Text = userInfo.DeptName;
                hidDeptNow.Value = userInfo.DeptID.ToString();
                //目前岗位
                #endregion
            }
            else
            {
                #region 修改时初期处理
                //设置标题
                divTitle.InnerText = "离职申请单";
                //获取并设置面试记录信息
                //InitMoveApplyInfo(ID);
                //设置编辑模式
                //hidIdentityID.Value = ID;
                #endregion
            }
        }
    }
    #endregion

    //#region 根据离职申请ID，获取信息，并设置到页面显示
    ///// <summary>
    ///// 根据离职申请ID，获取信息，并设置到页面显示
    ///// </summary>
    ///// <param name="ID">离职申请ID</param>
    //private void InitMoveApplyInfo(string ID)
    //{
    //    //设置编号可见
    //    divCodeNo.Attributes.Add("style", "display:block;");
    //    //自动生成编号的控件设置为不可见
    //    divCodeRule.Attributes.Add("style", "display:none;");

    //    //查询信息
    //    DataTable dtBaseInfo = MoveApplyBus.GetMoveApplyInfoWithID(ID);
    //    //基本信息存在时
    //    if (dtBaseInfo != null && dtBaseInfo.Rows.Count > 0)
    //    {
    //        #region 设置基本信息
    //        if (GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Status") == "0")//未办理
    //        {
    //            hidIdentityID.Value = ID.ToString();
    //            hiddenBillStatus.Value = "0";
    //            hidBillNo.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "MoveApplyNo").ToString();
    //        }
    //        else
    //        {
    //            hidIdentityID.Value = ID.ToString();
    //            hiddenBillStatus.Value = "0";
    //            hidBillNo.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "MoveApplyNo").ToString();
    //        }
    //        //编号
    //        divCodeNo.InnerHtml = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "MoveApplyNo");
    //        //主题
    //        txtTitle.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Title");
    //        //申请人
    //        UserApply.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeName");
    //        hidUserApply.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EmployeeID");
    //        //入职时间
    //        txtEnterDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "EnterDate");
    //        //申请日期
    //        txtApplyDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ApplyDate");
    //        //希望日期
    //        txtHopeDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "HopeDate");
    //        //目前部门
    //        DeptNow.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "DeptName");
    //        hidDeptNow.Value = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "DeptID");
    //        //目前岗位
    //        ddlNowQuarter.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "QuarterID");
    //        //合同有效期
    //        txtContractValid.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "ContractValid");
    //        //离职类型
    //        ddlMoveType.SelectedValue = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "MoveType");
    //        //通知离职日期
    //        txtMoveDate.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "MoveDate");
    //        //事由
    //        txtReason.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Reason");
    //        //访谈记录
    //        txtInterview.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Interview");
    //        //备注
    //        txtRemark.Text = GetSafeData.ValidateDataRow_String(dtBaseInfo.Rows[0], "Remark");
    //        #endregion
    //    }
    //}
    //#endregion
}
