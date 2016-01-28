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

public partial class Pages_Office_StorageManager_StorageOutRedAdd : BasePage
{
    private string companyCD = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        HiddenNow.Value = System.DateTime.Now.ToShortDateString();
        HiddenIsZero.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsZero.ToString();//是否允许价格为零
        string OutNoID = Request.QueryString["OutNoID"];
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
        HiddenMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();//是否启用多计量单位
        UserTransactor.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        txtTransactorID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        txtOutDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsDisplayPrice)
            IsDisplayPrice.Value = "true";
        else
            IsDisplayPrice.Value = "false";
        if (!IsPostBack)
        {
            ddlFromType.Attributes.Add("onchange", "DoChange();");
            txtOutNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtOutNo.ItemTypeID = ConstUtil.CODING_RULE_StorageOut_NO;

            DataTable dt = CodeReasonTypeBus.GetReasonType(companyCD);
            if (dt.Rows.Count > 0)
            {
                ddlReason.DataSource = dt;
                ddlReason.DataTextField = "CodeName";
                ddlReason.DataValueField = "ID";
                ddlReason.DataBind();
            }
            ddlReason.Items.Insert(0, new ListItem("--请选择--", ""));

            if (OutNoID != "" && OutNoID != null)
            {
                txtIndentityID.Value = OutNoID;//给隐藏域赋主键
            }
            else
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值
            }
            if (OutNoID != "" && OutNoID != null)
            {
                this.div_OutNo_uc.Attributes.Add("style", "display:none;");
                this.div_OutNo_Lable.Attributes.Add("style", "display:block;");
            }
            else
            {
                this.div_OutNo_uc.Attributes.Add("style", "display:block;");
                this.div_OutNo_Lable.Attributes.Add("style", "display:none;");
            }
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListByRed(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorageInfo.DataSource = dt1;
                ddlStorageInfo.DataTextField = "StorageName";
                ddlStorageInfo.DataValueField = "ID";
                ddlStorageInfo.DataBind();
            }

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEOUTRED_LIST;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEOUTRED_LIST, string.Empty);
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
