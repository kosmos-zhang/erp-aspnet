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
using XBase.Business.Common;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public partial class Pages_Office_CustManager_Cust_Info : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCustArea();
            BindCustCreditGrade();

            btnImport.Attributes["onclick"] = "return IfExp();";
        }
    }

    #region 绑定客户地区的方法
    private void BindCustArea()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_AREAID);
        if (dt.Rows.Count > 0)
        {
            ddlArea.DataTextField = "TypeName";
            ddlArea.DataValueField = "ID";
            ddlArea.DataSource = dt;
            ddlArea.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlArea.Items.Insert(0, Item);
    }
    #endregion

    #region 绑定客户优质级别的方法
    private void BindCustCreditGrade()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_CREDITGRADE);
        if (dt.Rows.Count > 0)
        {
            ddlCreditGrade.DataTextField = "TypeName";
            ddlCreditGrade.DataValueField = "ID";
            ddlCreditGrade.DataSource = dt;
            ddlCreditGrade.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlCreditGrade.Items.Insert(0, Item);
    }
    #endregion

    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderString = hiddExpOrder.Value.Trim();//排序
            string order = "asc";//排序：降序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CustNo";//要排序的字段，如果为空，默认为"ID"

            if (orderString.EndsWith("_d"))
            {
                order = "desc";//排序：降序
            }
            string ord = " ORDER BY " + orderBy + " " + order;

            CustInfoModel CustInfoM = new CustInfoModel();

            CustInfoM.CustNo = txtCustNo.Value.Trim();
            CustInfoM.CustNam = txtCustNam.Value;
            CustInfoM.CustClass = hiddCustClass.Value.Trim() == "" ? 0 : Convert.ToInt32(hiddCustClass.Value.Trim());

            CustInfoM.CustName = txtCustName.Value.Trim();
            CustInfoM.CustShort = txtCustShort.Value.Trim();
            CustInfoM.AreaID = ddlArea.SelectedItem.Value == "0" ? 0 : Convert.ToInt32(ddlArea.SelectedItem.Value);
            CustInfoM.CreditGrade = ddlCreditGrade.SelectedItem.Value == "0" ? 0 : Convert.ToInt32(ddlCreditGrade.SelectedItem.Value);
            CustInfoM.RelaGrade = seleRelaGrade.Value;
            CustInfoM.UsedStatus = seleUsedStatus.Value;
            CustInfoM.Tel = txtTel.Value.Trim();

            string Manager = txtManager.Value.Trim();
            string CreatedBegin = txtCreatedBegin.Value.Trim();
            string CreatedEnd = txtCreatedEnd.Value.Trim();
            CustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

            DataTable dt = CustInfoBus.ExportCustInfo(CustInfoM, Manager, CanUserID, CreatedBegin, CreatedEnd, ord);


            //OutputToExecl.ExportToTableFormat(this, dt,
            //    new string[] { "客户编号", "客户名称", "客户大类", "拼音缩写", "客户细分", "客户类别", "所在区域", "分管业务员", "客户优质级别", "客户关系等级", "创建人", "创建日期", "启用状态" },
            //    new string[] { "CustNo", "CustName", "CustBigName", "CustShort", "CodeName", "TypeName", "Area", "Manager", "CreditGrade", "RelaGradeName", "Creator", "CreatedDate", "UsedStatusName" },
            //    "客户档案列表");

            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "客户编号", "客户名称", "客户大类", "客户简称", "拼音缩写","客户管理分类", 
                    "客户营销分类","客户优质级别","客户时间分类","客户细分", "客户类别","建档人", "建档日期",
                    "客户简介","国家地区","区域","省","市(县)","业务类型","分管业务员","联系人","电话",
                    "手机","传真","在线咨询","公司网址","邮编","电子邮件","首次交易日期","运送方式","交货方式",
                    "联络期限(天)","收货地址","经营范围","允许延期付款","信用额度(元)","帐期天数(天)","结算方式",
                    "结算币种","发票类型","支付方式","开户行","户名","账号","单位性质","资产规模(万元)",
                    "成立时间","注册资本(万元)","员工总数(个)","法人代表","行业","营业执照号","注册地址",
                    "税务登记号","是否为一般纳税人","客户来源","年销售额(万元)","年利润额(万元)","销售模式",
                    "上级客户","价值评估","阶段","热点客户","热度","关系等级","启用状态","关系描述","备注",
                    "最后更新用户","最后更新日期","经营理念","企业口号","企业文化概述","发展潜力","存在问题",
                    "市场优劣势","行业地位","竞争对手","合作伙伴","发展计划","合作方法","可查看该客户档案的人员"


                     },

                new string[] { "CustNo", "CustName", "BigType", "CustNam", "CustShort", "CustTypeManage", 
                    "CustTypeSell", "CreditGradeNm", "CustTypeTime","CustClassName", "CustTypaNm","CreatorName","CreatedDate", 
                    "CustNote","CountryName","AreaName","Province","City","BusiType","ManagerName","ContactName","Tel",
                    "Mobile","Fax","OnLine","WebSite","Post","email","FirstBuyDate","CarryTypeNm","TakeTypeNm",
                    "LinkCycleNm","ReceiveAddress","SellArea","CreditManage","MaxCredit","MaxCreditDate","PayTypeNm",
                    "CurrencyaNm","BillTypeNm","MoneyTypeNm","OpenBank","AccountMan","AccountNum","CompanyType","CapitalScale",
                    "SetupDate","SetupMoney","StaffCount","ArtiPerson","Trade","BusiNumber","SetupAddress",
                    "TaxCD","IsTax","Source","SaleroomY","ProfitY","SellMode",
                    "CustSupe","MeritGrade","Phase","HotIs","HotHow","RelaGrade","UsedStatus","Relation","Remark",
                    "ModifiedUserID","ModifiedDate","CompanyValues","CatchWord","ManageValues","Potential","Problem",
                    "Advantages","TradePosition","Competition","Collaborator","ManagePlan","Collaborate","CanViewUserName"

                     },

                "客户档案列表");
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exp", "<script language=javascript>showPopup('../../../Images/Pic/Close.gif','../../../Images/Pic/note.gif','导出发生异常');</script>");
        }

    }
}
