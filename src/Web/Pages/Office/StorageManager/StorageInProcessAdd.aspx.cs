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

public partial class Pages_Office_StorageManager_StorageInProcessAdd : BasePage
{
    private string companyCD = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string InNoID = Request.QueryString["InNoID"];
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        //明细单价是否允许为0
        hidZero.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsZero.ToString();

        //是否启用批次      
        BatchRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
        BatchRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_BATCH_NO;
       
        //启用小数位数,默认2位
        hidSelPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

        //是否启用单位组
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            hidMoreUnit.Value = "true";
            tdDanWei.Attributes.Add("style", "display:block;");
            tdDanWei.Attributes.Add("style", "width: 8%");
            tdShuLiang.Attributes.Add("style", "display:block;");
            tdShuLiang.Attributes.Add("style", "width: 8%");
            divSpan.Attributes.Add("style", "display:block;");
            divRedSpan.Attributes.Add("style", "display:none;");
            divJiBendw.Attributes.Add("style", "display:block;");
            divDanWei.Attributes.Add("style", "display:none;");
        }
        else
        {
            hidMoreUnit.Value = "false";
            tdDanWei.Attributes.Add("style", "display:none;");
            tdShuLiang.Attributes.Add("style", "display:none;");
            divSpan.Attributes.Add("style", "display:none;");
            divRedSpan.Attributes.Add("style", "display:block;");
            divJiBendw.Attributes.Add("style", "display:none;");
            divDanWei.Attributes.Add("style", "display:block;");
        }

        if (!IsPostBack)
        {
            txtInNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtInNo.ItemTypeID = ConstUtil.CODING_RULE_StorageIn_NO;
            if (InNoID != "" && InNoID != null)
            {
                txtIndentityID.Value = InNoID;//给隐藏域赋主键
            }
            else
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值
                UserExecutor.Value = UserInfo.EmployeeName;
                txtExecutorID.Value = UserInfo.EmployeeID.ToString();
                txtEnterDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (InNoID != "" && InNoID != null)
            {
                this.div_InNo_uc.Attributes.Add("style", "display:none;");
                this.div_InNo_Lable.Attributes.Add("style", "display:block;");
            }
            else
            {
                this.div_InNo_uc.Attributes.Add("style", "display:block;");
                this.div_InNo_Lable.Attributes.Add("style", "display:none;");
            }
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt = StorageBus.GetStorageListBycondition(model);
            if (dt.Rows.Count > 0)
            {
                ddlStorageInfo.DataSource = dt;
                ddlStorageInfo.DataTextField = "StorageName";
                ddlStorageInfo.DataValueField = "ID";
                ddlStorageInfo.DataBind();
            }

            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINPROCESS_LIST;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEINPROCESS_LIST, string.Empty);
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
