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

namespace XBase.WebSite
{
    public partial class Top : System.Web.UI.Page
    {


        public string Zxl100Path
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {



            //获得用户页面控制权限
            UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            hidCompanCD.Value = UserInfo.CompanyCD;
            hidEmployeeID.Value = UserInfo.EmployeeID.ToString();

            if (!IsPostBack)
            {
                switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
                {
                    case XBase.Common.ConstUtil.Ver_ERP_Guid://生产版
                        TopMenuCell1.FlashPath = "Images/flash/top/01.swf";
                        TopMenuCell2.FlashPath = "Images/flash/top/02.swf";
                        TopMenuCell3.FlashPath = "Images/flash/top/03.swf";
                        TopMenuCell4.FlashPath = "Images/flash/top/04.swf";
                        TopMenuCell5.FlashPath = "Images/flash/top/05.swf";
                        TopMenuCell6.FlashPath = "Images/flash/top/06.swf";
                        Zxl100Path = string.Empty;
                        workModelInfo.InnerHtml = "执行模式";
                        //spanCurrentType.Visible = true;
                        break;
                    default://未匹配到
                        break;
                }





                //初始化Top菜单
                InitTopMenu();

                //backSiteDomain
                string backSiteDomain = (string)ConfigurationManager.AppSettings["backSiteDomain"];
                if (!backSiteDomain.EndsWith("/"))
                {
                    backSiteDomain = backSiteDomain + "/";
                }


                XBase.Model.SystemManager.CompanyOpenServModel comInfo = XBase.Business.SystemManager.CompanyOpenServBus.GetCompanyOpenServInfo(UserInfo.CompanyCD);
                if (comInfo != null)
                {
                    if (comInfo.LogoImg.Trim() + "" != "")
                    {
                        img_logo.Src = backSiteDomain + "/CompanyLogo/" + comInfo.LogoImg;
                    }
                    else
                    {
                        img_logo.Src = "images/" + Zxl100Path + "LOGO.jpg";
                    }
                }

            }

        }

        /// <summary>
        /// 初始化Top菜单
        /// </summary>
        private void InitTopMenu()
        {
            //从用户信息中获取菜单信息
            DataTable menuInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).MenuInfo;
            //从菜单信息中获取Top信息
            DataRow[] rows = menuInfo.Select("ParentID is null");
            if (rows.Length < 1)
            {
                return;
            }
            for (int i = 0; i < rows.Length; i++)
            {
                DataRow data = rows[i];
                //获取Top菜单的模块ID
                int moduleID = int.Parse((string)data["ModuleID"]);
                switch (moduleID)
                {
                    case 1:
                        //显示个人桌面
                        TopMenuCell1.Visible = true;
                        continue;
                    case 2:
                        //显示办公模式
                        TopMenuCell2.Visible = true;
                        continue;
                    case 3:
                        //显示运营模式
                        TopMenuCell3.Visible = true;
                        continue;
                    case 4:
                        //显示决策模式
                        TopMenuCell4.Visible = true;
                        continue;
                    case 5:
                        //显示知识中心
                        TopMenuCell5.Visible = true;
                        continue;
                    case 6:
                        //显示智能表单
                        TopMenuCell6.Visible = true;
                        continue;
                }
            }
        }



    }
}