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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
public partial class Pages_PrinttingModel_PurchaseManager_ProviderInfoPrint : BasePage
{
    #region MRP ID
    public int intMrpID 
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_PURCHASE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_PROVIDERINFO.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_PURCHASE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_PROVIDERINFO;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "供应商编号", "CustNo"},
                                { "供应商类别", "CustTypeName"}, 
                                { "供应商分类", "CustClassName" },
                                { "供应商名称", "CustName" },
                                { "供应商简称", "CustNam"},
                                { "供应商拼音代码", "PYShort"},
                                { "供应商简介", "CustNote"},
                                { "国家地区", "CountryName"},
                                { "省", "Province"},
                                { "市", "City"},
                                { "邮编", "Post"},
                                { "联系人", "ContactName"},
                                { "电话", "Tel"},
                                { "传真", "Fax"},
                                { "手机", "Mobile"},
                                { "邮件", "email"},
                                { "在线咨询", "OnLine"},
                                { "公司网址", "WebSite"},
                                { "交货方式", "TakeTypeName"},
                                { "运送方式", "CarryTypeName"},
                                { "供应商优先级别", "CreditGradeName"},
                                { "热点供应商", "HotIsName"},
                                { "启用状态", "UsedStatusName"},
                                { "分管采购员", "ManagerName"},
                                { "联络期限", "LinkCycle"},
                                { "所在地区", "AreaName"},
                                { "发货地址", "SendAddress"},
                                { "经营范围", "SellArea"},
                                { "结算方式", "PayTypeName"},
                                { "币种", "CurrencyTypeName"},
                                { "开户行", "OpenBank"},
                                { "户名", "AccountMan"},
                                { "帐号", "AccountNum"},
                                { "成立时间", "SetupDate"},
                                { "法人代表", "ArtiPerson"},
                                { "一般纳税人", "IsTaxName"},
                                { "税务登记号", "TaxCD"},
                                { "营业执照号", "BusiNumber"},
                                { "热度", "HotHowName"},

                          };
        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);

        DataTable dtProviderInfo = ProviderInfoBus.SelectProviderInfo(this.intMrpID);

        string strBaseFields = "";
        string strDetailFields = "";
        /*第二明细*/
        string strDetailSecondFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
            /*第二明细*/
            strDetailSecondFields = dbPrint.Rows[0]["DetailSecondFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "CustNo|CustTypeName|CustClassName|CustName|CustNam|PYShort|CustNote|CountryName|Province|City|Post|ContactName|Tel|Fax|Mobile|email|OnLine|WebSite|TakeTypeName|CarryTypeName|CreditGradeName|HotIsName|UsedStatusName|ManagerName|LinkCycle|AreaName|SendAddress|SellArea|PayTypeName|CurrencyTypeName|OpenBank|AccountMan|AccountNum|SetupDate|ArtiPerson|IsTaxName|TaxCD|BusiNumber|HotHowName";
           
        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("供应商档案", strBaseFields, null, aBase, null, dtProviderInfo, null, true);
        }
        #endregion

        //#region 3.明细信息
        //if (!string.IsNullOrEmpty(strDetailFields))
        //{
        //    tableDetail.InnerHtml = WritePrintPageTable("物料需求计划", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        //}
        //#endregion

        ///*第二明细*/
        //#region 4.明细信息2
        //if (!string.IsNullOrEmpty(strDetailSecondFields))
        //{
        //    tableDetail2.InnerHtml = WritePrintPageTable("物料需求计划", strBaseFields, strDetailSecondFields, aBase, aSecondDetail, dtMRP, dtDetail, false);
        //}
        //#endregion

    }
    #endregion

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("供应商档案") + ".xls");
        Response.Write("<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></head><body>");
        Response.Write(hiddExcel.Value);
        Response.Write(tw.ToString());
        Response.Write("</body></html>");
        Response.End();
        hw.Close();
        hw.Flush();
        tw.Close();
        tw.Flush();
    }
    #endregion
}
