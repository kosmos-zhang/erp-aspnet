using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using XBase.Common;

public partial class system_pages_menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String ParentID = Request.QueryString["ModuleID"];
        if (ParentID == "" || ParentID == null)
        {
            ParentID = "1";
        }
        String ParentName = "功能菜单";
        //string sql;
        if (!IsPostBack)
        {
            try
            {
                UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                DataTable AllMenuInfo = UserInfo.MenuInfo;
                DataTable MenuInfo = new DataTable();
                MenuInfo = AllMenuInfo.Clone();
                //获取名称
                DataRow[] rowsParentName = AllMenuInfo.Select("ModuleID = '" + ParentID + "'");
                for (int i = 0; i < rowsParentName.Length; i++)
                {
                    ParentName = (string)rowsParentName[i]["ModuleName"];
                }
                //获取菜单
                DataRow[] rows = AllMenuInfo.Select("ParentID = '" + ParentID + "'");
                for (int i = 0; i < rows.Length; i++)
                {
                    MenuInfo.ImportRow((DataRow)rows[i]);
                }
                ////输出结果
                rpMenu.DataSource = MenuInfo;
                rpMenu.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        switch (ParentID)
        {
            case "1":
                //显示个人桌面
                ParentNameShow.Text = "当前模式：个人桌面";
                break;
            case "2":
                //显示办公模式
                ParentNameShow.Text = "当前模式：办公模式";
                break;
            case "3":
                //显示运营模式
                ParentNameShow.Text = "当前模式：运营模式";
                break;
            case "4":
                //显示决策模式
                ParentNameShow.Text = "当前模式：决策模式";
               break;
            case "5":
                //显示知识中心
                ParentNameShow.Text = "当前模式：知识中心";
                break;
            case "6":
                //显示信息中心
                ParentNameShow.Text = "当前模式：信息中心";
               break;
            default:
                break;
        }  

        

    }
}
