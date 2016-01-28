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

public partial class Pages_PrinttingModel_SellManager_AdversarySellPrint : BasePage
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_ADVERSARYSELL.ToString();
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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_ADVERSARYSELL;

        AdversarySellModel modelMRP = new AdversarySellModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.CustNo = this.OfferNo;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "对手编号", "CustNo"}, 
                                { "销售机会", "ChanceNo"}, 
                                { "竞争客户", "CustName" },

                                { "对手产品报价", "Price" },

                                { "竞争产品/方案", "Project"},

                                { "竞争能力", "Power"},

                                { "竞争优势", "Advantage"},

                                { "竞争劣势", "disadvantage"},

                                { "应对策略", "Policy"},

                                { "制单人", "EmployeeName"},

                                { "制单日期", "CreatDate"},

                                { "最后更新人", "ModifiedUserID"},

                                { "最后更新日期", "ModifiedDate"},

                                { "备注", "Remark"},
                          };

        string[,] aDetail = {{"",""}};
       

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMRP = AdversarySellBus.GetRepOrder(OfferNo); //SellOfferBus.GetRepOrder(OfferNo); 
       
        string strBaseFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "CustNo|ChanceNo|CustName|Price|Project|Power|Advantage|disadvantage|Policy|EmployeeName|CreatDate|ModifiedUserID|ModifiedDate|Remark";
           
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("销售竞争分析", strBaseFields, "", aBase, aDetail, dtMRP, null, true);
        }
        #endregion

        #region 明细信息
        //if (!string.IsNullOrEmpty(strDetailFields))
        //{
        //    tableDetail.InnerHtml = WritePrintPageTable("销售合同", strBaseFields, strDetailFields, aBase, aDetail, dtMRP, dtDetail, false);
        //}
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("销售竞争分析") + ".xls");
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
