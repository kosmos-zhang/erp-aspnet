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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;


public partial class Pages_Office_StorageManager_StorageInitailAdd : BasePage
{

    private string companyCD = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]

        //明细单价是否允许为0
        hidZero.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsZero.ToString();

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

        //启用小数位数,默认2位
        hidSelPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;        

        BatchRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
        BatchRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_BATCH_NO;
       
        //设置隐藏域条码的值
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsBarCode)
        {
            hidCodeBar.Value = "1";
        }
        else
        {
            hidCodeBar.Value = "0";
        }
        if (!IsPostBack)
        {
            /*------------------ Start 是否可以使用期初录入模块-------------------------------*/
            //bool ifedit = StorageInitailBus.ISADD(companyCD);
            //if (ifedit == true)
            //{
            Hid_ifEdit.Value = "1";//可以
            //}
            //else
            //{
            //    Hid_ifEdit.Value = "0";//不能使用了
            //}
            /*------------------ End 是否可以使用期初录入模块-------------------------------*/

            string InNoID = Request.QueryString["InNoID"];
            txtInNo.CodingType = ConstUtil.CODING_RULE_Storage_NO;
            txtInNo.ItemTypeID = ConstUtil.CODING_RULE_StorageIn_NO;
            if (InNoID != "" && InNoID != null)
            {
                txtIndentityID.Value = InNoID;//给隐藏域赋主键
            }
            if (InNoID != "" && InNoID != null)//更新
            {
                this.div_InNo_uc.Attributes.Add("style", "display:none;");
                this.div_InNo_Lable.Attributes.Add("style", "display:block;");
            }
            else//新建
            {
                this.txtCreator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();//制单人
                this.txtCreateDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给那个制单日期赋值
                this.HiddCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();//最后更新人
                this.txtModifiedDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//新建的时候给最后更新时间日期赋值

                this.div_InNo_uc.Attributes.Add("style", "display:block;");
                this.div_InNo_Lable.Attributes.Add("style", "display:none;");
            }
            //仓库获取
            StorageModel model = new StorageModel();
            model.CompanyCD = companyCD;
            model.UsedStatus = "1";
            DataTable dt = StorageBus.GetStorageListBycondition(model);
            if (dt.Rows.Count > 0)
            {
                sltStorageID.DataSource = dt;
                sltStorageID.DataTextField = "StorageName";
                sltStorageID.DataValueField = "ID";
                sltStorageID.DataBind();

            }
            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINITAIL_LIST;
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
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEINITAIL_LIST, string.Empty);
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
