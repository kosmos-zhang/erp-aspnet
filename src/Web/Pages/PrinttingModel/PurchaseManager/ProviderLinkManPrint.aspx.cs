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

public partial class Pages_PrinttingModel_PurchaseManager_ProviderLinkManPrint : BasePage
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_PROVIDERLINKMAN.ToString();

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_PROVIDERLINKMAN;

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "联系人姓名", "LinkManName"}, 
                                { "供应商姓名", "CustName"}, 
                                { "性别", "SexName"},
                                { "重要程度", "ImportantName"},
                                { "称谓", "Appellation"},
                                { "单位", "Company"},
                                { "部门", "Department"},
                                { "职务", "Position"},
                                { "负责业务", "Operation"},
                                { "工作电话", "WorkTel"},
                                { "传真", "Fax"},
                                { "移动电话", "Handset"},
                                { "邮件地址", "MailAddress"},
                                { "家庭电话", "HomeTel"},
                                { "MSN", "MSN"},
                                { "QQ", "QQ"},
                                { "邮编", "Post"},
                                { "联系人类型", "LinkTypeName"},
                                { "年龄", "Age"},
                                { "证件类型", "PaperType"},
                                { "证件号", "PaperNum"},
                                { "最后更新日期", "ModifiedDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "生日", "Birthday"},
                                { "爱好", "Likes"},
                                { "备注", "Remark"},
                          };
        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);

        DataTable dtProviderInfo = ProviderLinkManBus.SelectProviderLinkMan(this.intMrpID);

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
            strBaseFields = "LinkManName|CustName|SexName|ImportantName|Appellation|Company|Department|Position|Operation|WorkTel|Fax|Handset|MailAddress|HomeTel|MSN|QQ|Post|LinkTypeName|Age|PaperType|PaperNum|ModifiedDate|ModifiedUserID|Birthday|Likes|Remark";

        }

        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("供应商联系人", strBaseFields, null, aBase, null, dtProviderInfo, null, true);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("供应商联系人") + ".xls");
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
