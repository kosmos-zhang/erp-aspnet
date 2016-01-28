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
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.StorageManager;


/// <summary>
/// 质检申请单页面
/// </summary>
public partial class Pages_Office_StorageManager_StorageQualityCheckAdd : BasePage
{
    #region 变量
    /// <summary>
    /// 公司代码
    /// </summary>
    private string companyCD = string.Empty;
    /// <summary>
    /// 界面
    /// </summary>
    public string myFrom;

    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;

    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;
    #endregion

    #region 属性
    /// <summary>
    /// 流水号
    /// </summary>
    public int intMasterProductID
    {
        get
        {

            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }

    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }



    /// <summary>
    /// 小数位数
    /// </summary>
    public int SelPoint
    {
        get
        {
            return _selPoint;
        }
        set
        {
            _selPoint = value;
        }
    }

    #endregion

    #region 事件、方法

    #region 页面

    #region 事件

    /// <summary>
    /// 加载窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPECODE_STORAGE_QUALITY;
        FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_QUALITYADD;
        if (Request.QueryString["From"] != null)
        {
            myFrom = Request.QueryString["From"].ToString();
        }
        if (!IsPostBack)
        {
            #region 加载基本信息
            string InNoID = Request.QueryString["InNoID"];
            int EmployeeId = UserInfo.EmployeeID;
            string userId = UserInfo.UserID;
            companyCD = UserInfo.CompanyCD;
            txtCreator.Value = EmployeeId.ToString();
            tbCreator.Text = UserInfo.EmployeeName;
            tbConfirmor.Text = UserInfo.EmployeeName;
            tbCloser.Text = UserInfo.EmployeeName;
            tbModifiedUserID.Text = userId;
            txtCloser.Value = EmployeeId.ToString();
            txtConfirmor.Value = EmployeeId.ToString();
            txtConfirmDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Value = EmployeeId.ToString();
            txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtCloseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEnterDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            // 条目按钮是否显示
            btnGetGoods.Visible = UserInfo.IsBarCode;
            unbtnGetGoods.Visible = UserInfo.IsBarCode;

            // 多计量单位控制
            _isMoreUnit = UserInfo.IsMoreUnit;
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
            #endregion

            txtInNo.CodingType = ConstUtil.CODING_RULE_StorageQuality_NO;
            txtInNo.ItemTypeID = ConstUtil.CODING_RULE_StorageQualityCheck_NO;
            hidNoID.Value = InNoID;

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

        }
    }
    #endregion

    #region 方法

    #endregion

    #endregion

    #endregion

}
