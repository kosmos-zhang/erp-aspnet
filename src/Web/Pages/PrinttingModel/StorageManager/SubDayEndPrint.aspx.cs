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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_StorageManager_SubDayEndPrint : BasePage 
{
    #region MRP ID
    public int intMrpID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ProductID"], out tempID);
            return tempID;
        }
    }
    public string BatchNo
    {
        get
        {
            return Request.QueryString["BatchNo"] == null ? "" : Request.QueryString["BatchNo"].ToString();
        }
    }
    public string DailyDate
    {
        get
        {
            return Request.QueryString["DailyDate"] == null ? "" : Request.QueryString["DailyDate"].ToString();
        }
    }
    public int DeptID
    {
        get
        {
            int tID = 0;
            int.TryParse(Request.QueryString["DeptID"], out tID);
            return tID;
        }
    }
    #endregion



    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_STORAGE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_SubDayEnd.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }


    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_STORAGE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SubDayEnd;
        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = {  
                                { "日结日期", "DailyDate"}, 
                                { "分店", "DeptName"}, 
                                { "品名", "ProductName" },
                                 { "批次", "BatchNo"},
                                { "物品编号", "ProductNo" },
                                  { "入库总数量 ", "InTotal" },
                                  { "出库总数量", "OutTotal" },
                                    { "当日结存量", "TodayCount" },
                                      { "当日销售金额", "SaleFee" },
                                         { "当日销售退货金额", "SaleBackFee" },
                                      { "配送退货出库数量", "SendOutCount" },
                                       { "期初库存录入数量", "InitInCount" },
                                          { "配送入库数量", "SendInCount" },
                                             { "门店调拨入库数量", "DispInCont" },
                                              { "门店调拨出库数量", "DispOutCount" },
                                                   { "销售出库数量(总店发货模式)", "SaleOutCount" },
                                                { "销售退货入库数量(总店发货模式)", "SaleBackInCount" },
                                                     { "销售退货入库数量(分店发货模式)", "SubSaleBackInCount" }, 
                                                         { "销售出库数量(分店发货模式)", "SubSaleOutCount" },  
                                { "操作人", "CreatorName"}, 
                                { "操作日期", "CreateDate"},
                          };







        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMRP = SubDayEndBus.GetSubStorageDailyInfo(Convert.ToString(this.intMrpID), this.BatchNo, Convert.ToString(this.DeptID ), this.DailyDate, model.CompanyCD);





        string strBaseFields = "";
        /*第二明细*/

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "DailyDate|DeptName|ProductName|BatchNo|ProductNo|InTotal|OutTotal|TodayCount|SaleFee|SaleBackFee|SendOutCount|InitInCount|SendInCount|DispInCont|DispOutCount|SaleOutCount|SaleBackInCount|SubSaleBackInCount|SubSaleOutCount|CreatorName|CreateDate";
        }


        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("门店日结明细", strBaseFields, null, aBase, null, dtMRP, null, true);
        }







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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("门店日结明细") + ".xls");
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
