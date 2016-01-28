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

public partial class Pages_PrinttingModel_SellManager_AdversaryPrint : BasePage
{
    #region PlanNo

    public string PlanNo
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_ADVERSARY.ToString();
        hidPlanNo.Value = PlanNo;

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_ADVERSARY;

        AdversaryInfoModel modelMRP = new AdversaryInfoModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.CustNo = this.PlanNo;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        #region 信息的字段
        string[,] aBase = { 
                                { "对手编号", "CustNo"}, 
                                { "对手类别", "CustTypeName"}, 
                                { "对手细分", "CustClassName" },

                                { "对手名称", "CustName" },
                                { "对手简称", "ShortNam"},
                                { "对手拼音代码", "PYShort"},
                                { "对手所在区域", "AreaIDName"},

                                { "成立时间", "SetupDate"},
                                { "法人代表", "ArtiPerson"},
                                { "单位性质", "CompanyTypeName"},

                                { "员工总数", "StaffCount"},
                                { "注册资本(万元)", "SetupMoney"},
                                { "注册地址", "SetupAddress"},

                                { "公司网址", "website"},
                                { "资产规模(万元)", "CapitalScale"},
                                { "经营范围", "SellArea"},

                                { "年销售额(万元)", "SaleroomY"},
                                { "年利润额(万元)", "ProfitY"},
                                { "税务登记号", "TaxCD"},

                                { "营业执照号", "BusiNumber"},
                                { "一般纳税人", "IsTaxName"},
                                { "地址", "Address"},

                                { "邮编", "Post"},
                                { "联系人", "ContactName"},
                                { "电话", "Tel"},

                                { "手机", "Mobile"},
                                { "邮件", "email"},
                                
                                { "对手简介", "CustNote"},
                                { "主打产品 ", "Product"},
                                { "竞争产品/方案", "Project"},
                                { "竞争能力", "Power"},
                                { "竞争优势", "Advantage"},
                                { "竞争劣势", "disadvantage"},
                                { "应对策略", "Policy"},
                                { "市场占有率(%)", "Market"},
                                { "销售模式", "SellMode"},

                                { "启用状态", "UsedStatusName"},
                                { "制单人", "EmployeeName"},
                                { "制单日期", "CreatDate"},
                                { "最后更新人", "ModifiedUserID"},

                                { "最后更新日", "ModifiedDate"},
                                { "备注", "Remark"},                              
                          };

        string[,] aDetail = {
                                { "对手动态", "Dynamic"},
                           };
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMRP = AdversaryInfoBus.GetRepOrder(PlanNo); //SellContractBus.GetRepOrder(OfferNo); 
        DataTable dtDetail = AdversaryInfoBus.GetRepOrderDetail(PlanNo);// SellContractBus.GetRepOrderDetail(OfferNo);
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
            strBaseFields = "CustNo|CustTypeName|CustClassName|CustName|ShortNam|PYShort|AreaIDName|SetupDate|ArtiPerson|CompanyTypeName|StaffCount|SetupMoney|" +
                "SetupAddress|website|CapitalScale|SellArea|SaleroomY|ProfitY|TaxCD|BusiNumber|IsTaxName|Address|Post|ContactName|Tel|Mobile|email|" +
                "CustNote|Product|Project|Power|Advantage|disadvantage|Policy|Market|SellMode|UsedStatusName|EmployeeName|CreatDate|ModifiedUserID|" +
                "ModifiedDate|Remark";

            strDetailFields = "Dynamic";
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("竞争对手档案", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("对手动态信息", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("竞争对手档案") + ".xls");
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
