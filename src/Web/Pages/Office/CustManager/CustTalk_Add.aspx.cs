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
using XBase.Business.Common;

public partial class Pages_Office_CustManager_CustTalk_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            txtModifiedDate.Value = DateTime.Now.ToShortDateString();
            txtModifiedUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改用户为当前用户

            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//建单人
            hfCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//建单人   
            txtCreatedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            ddlTalkNo.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            ddlTalkNo.ItemTypeID = ConstUtil.CUST_BILL_Talk;
                       
            //BindTalkType();//关怀类型
        }
    }
}
