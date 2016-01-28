using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;


public partial class Pages_Office_StorageManager_StorageLossAdd : BasePage
{
    private string companyCD = string.Empty;


    #region List ModuleID
    public int ListModuleID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ListModuleID"], out tempID);
            return tempID;
        }
    }
    #endregion

    //#region From Type
    //public int intFromType
    //{
    //    get
    //    {
    //        int tempID = 0;
    //        int.TryParse(Request["intFromType"], out tempID);
    //        return tempID;
    //    }
    //}
    //#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string LossNoID = Request.QueryString["LossNoID"];
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        btnGetGoods.Visible = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        if (!IsPostBack)
        {
            ddlStorage.Attributes.Add("onchange", "DoChange();");

            txtLossNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtLossNo.ItemTypeID = ConstUtil.CODING_RULE_StorageLoss_NO;

            DataTable dt = CodeReasonTypeBus.GetReasonTypeByFlag(companyCD, "5");
            if (dt.Rows.Count > 0)
            {
                ddlReason.DataSource = dt;
                ddlReason.DataTextField = "CodeName";
                ddlReason.DataValueField = "ID";
                ddlReason.DataBind();
            }
            ddlReason.Items.Insert(0, new ListItem("--请选择--", ""));

            if (LossNoID != "" && LossNoID != null)
            {
                txtIndentityID.Value = LossNoID;//给隐藏域赋主键
            }
            else
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值

            }
            if (LossNoID != "" && LossNoID != null)
            {
                this.div_LossNo_uc.Attributes.Add("style", "display:none;");
                this.div_LossNo_Lable.Attributes.Add("style", "display:block;");
            }
            else
            {
                this.div_LossNo_uc.Attributes.Add("style", "display:block;");
                this.div_LossNo_Lable.Attributes.Add("style", "display:none;");
            }
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorage.DataSource = dt1;
                ddlStorage.DataTextField = "StorageName";
                ddlStorage.DataValueField = "ID";
                ddlStorage.DataBind();
            }

            //审批流程设置弹出层设置
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPEFLAG_STORAGE;
            FlowApply1.BillTypeCode = ConstUtil.CODING_RULE_StorageLoss_NO;

            //模板列表模块ID
            hidModuleID.Value = Request.QueryString["ListModuleID"];
            intFromType.Value = Request.QueryString["intFromType"];
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGELOSS_LIST, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }

        }
    }
}
