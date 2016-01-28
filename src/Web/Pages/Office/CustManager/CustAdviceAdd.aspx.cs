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
using XBase.Business.Office.StorageManager;
public partial class Pages_Office_CustManager_CustAdviceAdd : BasePage
{
    #region  CustAdviceID
    public int CustAdviceID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request.QueryString["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            checkNo.CodingType = ConstUtil.CUST_CODINGTYPE_BILL;
            checkNo.ItemTypeID = ConstUtil.CUST_BILL_CustAdvice;
            #region 初始化
            if (Request.QueryString["myAction"] == null)
            {
                int EmployeeId = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                string Company = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                tbCreater.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtAdviceDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtModifiedUserID.Text = UserID.ToString();
                txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            }
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    this.DropDownList1.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                int myhour = DateTime.Now.Hour;
                if (myhour < 10)
                {
                    this.DropDownList1.SelectedValue = "0" + myhour.ToString();
                }
                else
                {
                    this.DropDownList1.SelectedValue = myhour.ToString();
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    this.DropDownList2.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    this.DropDownList2.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                int myminiute = DateTime.Now.Minute;
                if (myminiute < 10)
                {
                    this.DropDownList2.SelectedValue = "0" + myminiute.ToString();
                }
                else
                {
                    this.DropDownList2.SelectedValue = myminiute.ToString();
                }
            }
        }
            #endregion
    }
}

