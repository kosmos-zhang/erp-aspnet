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

using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Business.Office.SellManager;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_SellManager_SellContractPrint : BasePage
{
    #region OfferNo
    public string OfferNo
    {
        get
        {
            return Request["no"].ToString();
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_SALE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_SELLCONTRANCT.ToString();
        hidPlanNo.Value = OfferNo;

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
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SALE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_SELLCONTRANCT;

        SellOfferModel modelMRP = new SellOfferModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.OfferNo = this.OfferNo;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "{ExtField1}", "ExtField1"},
                                { "{ExtField2}", "ExtField2"},
                                { "{ExtField3}", "ExtField3"},
                                { "{ExtField4}", "ExtField4"},
                                { "{ExtField5}", "ExtField5"},
                                { "{ExtField6}", "ExtField6"},
                                { "{ExtField7}", "ExtField7"},
                                { "{ExtField8}", "ExtField8"},
                                { "{ExtField9}", "ExtField9"},
                                { "{ExtField10}", "ExtField10"},

                                { "单据编号", "ContractNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "FromTypeText" },

                                { "源单编号", "OfferNo" },
                                { "客户名称", "CustName"},
                                { "客户电话", "CustTel"},

                                { "业务类型", "BusiTypeName"},
                                { "销售类别", "SellTypeName"},
                                { "结算方式", "PayTypeName"},

                                { "支付方式", "MoneyTypeName"},
                                { "交货方式", "TakeTypeName"},
                                { "运送方式", "CarryTypeName"},

                                { "币种", "CurrencyName"},
                                { "汇率", "Rate"},
                                { "业务员", "SellerName"},

                                { "部门", "DeptName"},
                                { "客户方代表", "TheyDelegate"},
                                { "我方代表", "OurDelegateName"},

                                { "签约时间", "SignDate"},
                                { "签约地点", "SignAddr"},
                                { "开始日期 ", "StartDate"},

                                { "截止日期", "EndDate"},
                                { "洽谈进展", "TalkProcess"},
                                { "合同状态", "stateText"},

                                { "终止原因", "EndNote"},
                                { "是否增值税", "isAddTaxName"},
                                //{ "是否被引用 ", "RealTotal"},

                                { "金额合计", "TotalPrice"},
                                { "税额合计", "Tax"},
                                { "含税金额合计", "TotalFee"},
                                { "整单折扣(%) ", "Discount"},
                                { "折后含税金额", "RealTotal"},
                                { "折扣金额合计", "DiscountTotal"},
                                { "合同数量合计", "CountTotal"},


                                { "单据状态", "BillStatusText"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},

                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期 ", "ModifiedDate"},
                                { "备注", "Remark"},
                          };

        string[,] aDetail;
        //多计量单位
        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            aDetail = new string[,]{
                            { "物品编号", "ProdNo"}, 
                            { "物品名称", "ProductName"}, 
                            { "规格", "Specification"}, 
                            { "基本单位", "CodeName" },
                            { "基本数量", "ProductCount"},
                            { "单位", "UsedUnitName" },
                            { "数量", "UsedUnitCount"},
                            { "交货期限(天)", "SendTime" },
                            { "包装要求", "TypeName"},
                            { "单价", "UsedPrice"},
                            { "含税价", "TaxPrice"},
                            { "折扣(%)", "Discount"},
                            { "税率(%)", "TaxRate"},
                            { "含税金额", "TotalFee"},
                            { "金额", "TotalPrice"},
                            { "税额", "TotalTax"},
                            { "备注 ", "Remark"},
                       };
        }
        else
        { 
            aDetail = new string[,]{
                            { "物品编号", "ProdNo"}, 
                            { "物品名称", "ProductName"}, 
                            { "规格", "Specification"}, 
                            { "单位", "CodeName" },
                            { "数量", "ProductCount"},
                            { "交货期限(天)", "SendTime" },
                            { "包装要求", "TypeName"},
                            { "单价", "UnitPrice"},
                            { "含税价", "TaxPrice"},
                            { "折扣(%)", "Discount"},
                            { "税率(%)", "TaxRate"},
                            { "含税金额", "TotalFee"},
                            { "金额", "TotalPrice"},
                            { "税额", "TotalTax"},
                            { "备注 ", "Remark"},
                       };
        }

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.SellContract");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 15; x++)
                {
                    if (x == i)
                    {
                        aBase[x, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
                        countExt++;
                    }
                }
            }
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMRP = SellContractBus.GetRepOrder(OfferNo); //SellOfferBus.GetRepOrder(OfferNo); 
        DataTable dtDetail = SellContractBus.GetRepOrderDetail(OfferNo); //SellOfferBus.GetRepOrderDetail(OfferNo); 
        string strBaseFields = "";
        string strDetailFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "ContractNo|Title|FromTypeText|OfferNo|CustName|CustTel|BusiTypeName|SellTypeName|PayTypeName|MoneyTypeName|TakeTypeName|CarryTypeName|" +
                "CurrencyName|Rate|SellerName|DeptName|TheyDelegate|OurDelegateName|SignDate|SignAddr|StartDate|EndDate|TalkProcess|stateText|EndNote|isAddTaxName|" +
                "TotalPrice|Tax|TotalFee|Discount|RealTotal|DiscountTotal|CountTotal|BillStatusText|CreatorName|CreateDate|ConfirmorName|ConfirmDate|CloserName|" +
                "CloseDate|ModifiedUserID|ModifiedDate|Remark";

            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }
            //多计量单位
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                strDetailFields = "ProdNo|ProductName|Specification|CodeName|ProductCount|UsedUnitName|UsedUnitCount|SendTime|TypeName|UsedPrice|TaxPrice|Discount|" +
                "TaxRate|TotalFee|TotalPrice|TotalTax|Remark";
            }
            else
            { 
                strDetailFields = "ProdNo|ProductName|Specification|CodeName|ProductCount|SendTime|TypeName|UnitPrice|TaxPrice|Discount|" +
                "TaxRate|TotalFee|TotalPrice|TotalTax|Remark";
            }
            
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售合同", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("销售合同", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售合同") + ".xls");
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
