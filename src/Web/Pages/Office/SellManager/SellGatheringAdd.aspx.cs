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
using XBase.Business.Office.SellManager;

public partial class Pages_Office_SellManager_SellGatheringAdd :BasePage
{
    //小数精度
    private int _selPoint = 2;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
                UserSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                hiddSeller.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                ////临时注释
                Creator.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
                ////临时注释
                ModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
           
            CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            SellGathering.CodingType = ConstUtil.CODING_RULE_SELL;//销售回款计划编号
            SellGathering.ItemTypeID = ConstUtil.CODING_RULE_GATHERING_NO;//销售回款计划编号
            // 小数位数
            _selPoint = int.Parse(UserInfo.SelPoint);
        }
    }
}
