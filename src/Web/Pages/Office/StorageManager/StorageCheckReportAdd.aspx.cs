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
using XBase.Common;

public partial class Pages_Office_StorageManager_StorageCheckReportAdd : BasePage
{
    /// <summary>
    /// 用户信息
    /// </summary>
    protected UserInfoUtil userInfo = null;
    /// <summary>
    /// 小数位数
    /// </summary>
    private int _selPoint = 2;

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

    #region  ReportID
    public int ReportID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request.QueryString["ID"], out tempID);
            return tempID;
        }
    }

    public int intMasterProductID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        // 小数位数
        _selPoint = int.Parse(userInfo.SelPoint);
        if (!IsPostBack)
        {
            FlowApply1.BillTypeFlag = ConstUtil.BILL_TYPECODE_STORAGE_QUALITY;
            FlowApply1.BillTypeCode = ConstUtil.BILL_TYPECODE_STORAGE_REPORT;

            checkNo.CodingType = ConstUtil.CODING_RULE_StorageQuality_NO;
            checkNo.ItemTypeID = ConstUtil.CODING_RULE_StorageCheckReport_NO;

            #region 初始化
            int EmployeeId = userInfo.EmployeeID;
            string userId = userInfo.UserID;
            string Company = userInfo.CompanyCD;
            tbCreater.Text = userInfo.EmployeeName;
            txtModifiedUserID.Text = userId;
            txtConfirmorReal.Value = EmployeeId.ToString();
            txtConfirmor.Value = userInfo.EmployeeName;
            txtCloserReal.Value = userInfo.EmployeeName;
            txtCloseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtConfirmDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreator.Value = Convert.ToString(EmployeeId);
            this.CheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");   //检验日期



            #endregion
        }
    }
}
