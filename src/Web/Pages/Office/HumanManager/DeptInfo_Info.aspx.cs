/**********************************************
 * 类作用：   组织机构
 * 建立人：   吴志强
 * 建立时间： 2009/04/09
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_DeptInfo_Info : BasePage
{

    /// <summary>
    /// 类名：Dept_Info
    /// 描述：组织机构
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/09
    /// 最后修改时间：2009/04/09
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期设值
        if (!IsPostBack)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            divCompany.InnerHtml = userInfo.CompanyName  ;
            //编号初期处理
            codeRule.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            codeRule.ItemTypeID = ConstUtil.CODING_BASE_ITEM_DEPT;
        }
    }

}
