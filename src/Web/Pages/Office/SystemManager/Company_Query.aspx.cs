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
public partial class Pages_Office_SystemManager_Company_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }

    }
    private void InitPage()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string CompanyCD = UserInfo.CompanyCD;
        //定义用户控件的标题
        string[,] Title = { { "公司代码", "CompanyCD" }, { "公司编号", "CompanyNo" }, { "公司名称", "CompanyName" }, { "启用状态", "UsedStatus" } };
        //获取所查询的信息
        string sql = "select CompanyCD,CompanyNo,CompanyName,case when UsedStatus='1'then '是'else '否'end as UsedStatus  ";
        sql += "from officedba.CompanyBaseInfo where CompanyCD='"+CompanyCD+"'";
        
        //分页控件设置
       // ucComPanyInfo.Setsql = sql;
      //  ucComPanyInfo.IsSort = true;
      //  ucComPanyInfo.CheckBox = true;
      //  ucComPanyInfo.TableTitle = Title;
      //  ucComPanyInfo.GetData();
       // ucComPanyInfo.Visible = true;


    }
    protected void btnModify_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
}
