/**********************************************
 * 类作用：   人才代理信息维护处理
 * 建立人：   吴志强
 * 建立时间： 2009/03/25
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.HumanManager;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_HumanManager_HRProxy_Edit : BasePage
{

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示处理
        if (!IsPostBack)
        {

            #region 页面内容初期设置

            //设置模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_HUMAN_HRPROXY_INFO;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_HUMAN_HRPROXY_ADD, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }

            //获取人才代理编号
            string proxyID = Request.QueryString["ProxyID"];
            //proxyID = "38";
            //人才代理编号为空，为新建模式
            if (string.IsNullOrEmpty(proxyID))
            {
                //编号初期处理
                codruleProxy.CodingType = ConstUtil.CODE_TYPE_HUMAN;
                codruleProxy.ItemTypeID = ConstUtil.CODING_HUMAN_ITEM_PROXY;
                //设置人才代理编号不可见
                divProxyCompanyNo.Attributes.Add("style", "display:none;");
                //自动生成编号的控件设置为可见
                divCodeRule.Attributes.Add("style", "display:block;");
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_INSERT;
            }
            else
            {
                //设置标题
                divTitle.InnerText = "人才代理";
                //设置人才代理编号可见
                divProxyCompanyNo.Attributes.Add("style", "display:block;");
                //自动生成编号的控件设置为不可见
                divCodeRule.Attributes.Add("style", "display:none;");
                //新建按钮不可见
                //btnNew.Visible = false;
                //获取并设置人才代理信息信息
                InitProxyInfo(proxyID);
                //设置编辑模式
                hidEditFlag.Value = ConstUtil.EDIT_FLAG_UPDATE;
            }
            #endregion

        }
    }
    #endregion

    #region 设置人才代理信息
    /// <summary>
    /// 设置人才代理信息
    /// </summary>
    /// <param name="proxyID">人才代理公司ID</param>
    private void InitProxyInfo(string proxyID)
    {

        //查询人才代理信息
        DataTable dtProxy = HRProxyBus.GetProxyInfoWithID(proxyID);
        //数据存在时
        if (dtProxy != null && dtProxy.Rows.Count > 0)
        {
            /* 代理公司信息设置 */

            //设置人才代理编号
            divProxyCompanyNo.InnerText = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ProxyCompanyCD");
            //企业名称
            txtProxyCompanyName.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ProxyCompanyName");
            //企业性质
            txtNature.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Nature");
            //地址
            txtAddress.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Address");
            //企业法人
            txtCorporate.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Corporate");
            //合作关系
            ddlCooperation.SelectedValue = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Cooperation");
            //电话
            txtTel.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Telephone"); 
            //传真
            txtFax.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Fax");
            //邮箱
            txtMail.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Email");
            //网址
            txtWebsite.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Website");
            //重要程度
            ddlImportant.SelectedValue = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Important");
            //启用状态
            ddlUsedStatus.SelectedValue = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "UsedStatus");
            //string usedStatus = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "UsedStatus");
            ////启用时
            //if (ConstUtil.USED_STATUS_ON.Equals(usedStatus))
            //{
            //    chkUsedStatus.Checked = true;
            //}
            //else
            //{
            //    chkUsedStatus.Checked = false;
            //}
            
            //收费标准
            txtStandard.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Standard");

            /* 代理公司联系人信息 */
            //姓名
            txtContactName.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactName");
            //固定电话
            txtContactTel.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactTel");
            //移动电话
            txtContactMobile.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactMobile");
            //网络通讯
            txtContactWeb.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactWeb");
            //工号
            txtContactCardNo.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactCardNo");
            //公司职务
            txtContactPosition.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactPosition");
            //备注
            txtContactRemark.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "ContactRemark");

            /* 附加信息 */
            txtAdditional.Text = GetSafeData.ValidateDataRow_String(dtProxy.Rows[0], "Remark");

        }
    }
    #endregion

}
