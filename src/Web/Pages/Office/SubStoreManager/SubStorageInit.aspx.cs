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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;


public partial class Pages_Office_SubStoreManager_SubStorageInit : BasePage
{
    #region Master SubStorageIn ID
    public int intMasterSubStorageInID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intMasterSubStorageInID"], out tempID);
            return tempID;
        }
    }
    #endregion

    string CompanyCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyCD = UserInfo.CompanyCD;
        this.txtIndentityID.Value = this.intMasterSubStorageInID.ToString();
        if (!Page.IsPostBack)
        {
            // 批次
            BatchRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            BatchRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_BATCH_NO;

            hidModuleID.Value = ConstUtil.MODULE_ID_SUBSTOREMANAGER_SUBSTORAGEINITLIST;
            UserID.Value = UserInfo.EmployeeID.ToString();
            UserName.Value = UserInfo.EmployeeName;
            SystemTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            txtCreator.Value = UserInfo.EmployeeID.ToString();
            UserInfoUtil userInfo = UserInfo;
            usernametemp.Value = userInfo.UserName;
            txtCreatorReal.Value = userInfo.EmployeeName;
            datetemp.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtCreateDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID.Value = UserInfo.UserID.ToString();
            txtModifiedUserIDReal.Value = UserInfo.UserID.ToString();
            txtModifiedDate2.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUserID2.Value = UserInfo.UserID.ToString();
            DataRow dt = SubStorageDBHelper.GetSubDeptFromDeptID(UserInfo.DeptID.ToString());

            if ( dt != null )
            {
                HidDeptID.Value = dt["ID"].ToString();
                HidDeptID2.Value = dt["ID"].ToString();
                txtDeptName.Text = dt["DeptName"].ToString();

            }
            btnGetGoods.Visible = userInfo.IsBarCode;
            unbtnGetGoods.Visible = userInfo.IsBarCode;

            #region 门店入库单单据规则
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_SUBSTORE;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODING_RULE_SUBSTORE_SUBSTORAGEIN;
            #endregion
            if (userInfo.IsMoreUnit)
            {
                txtIsMoreUnit.Value = "1";
                BasicUnitTd.Visible = true;
                BasicNumTd.Visible = true;
            }
            else
            {
                txtIsMoreUnit.Value = "0";
                BasicUnitTd.Visible = false;
                BasicNumTd.Visible = false;
            }
        }

        //小数点位数
        hidSelPoint.Value = UserInfo.SelPoint;
    }
}
