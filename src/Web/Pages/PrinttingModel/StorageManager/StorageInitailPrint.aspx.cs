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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using System.Text;
using XBase.Common;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Model.Common;
public partial class Pages_PrinttingModel_StorageManager_StorageInitailPrint : BasePage
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
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_STORAGE;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_Init.ToString();

        if (!IsPostBack)
        {
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit == true)
            {
                HiddenMoreUnit.Value = "true";
                td_UsedUnitName.Attributes.Add("style", "display:block;");
                td_UsedUnitCount.Attributes.Add("style", "display:block;");
            }
            else
            {
                HiddenMoreUnit.Value = "false";
                td_UsedUnitName.Attributes.Add("style", "display:none;");
                td_UsedUnitCount.Attributes.Add("style", "display:none;");
            }

            LoadPrintInfo();
           
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_STORAGE);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_Init;

        StorageInitailModel OutSellM_ = new StorageInitailModel();
        OutSellM_.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        OutSellM_.ID = this.intMrpID.ToString();

        string[,] aDetail;

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
                                { "入库单编号", "InNo"}, 
                                { "入库单主题 ", "Title"}, 
                                { "仓库", "StorageName" },
                                { "入库部门 ", "DeptName" },
                                { "入库人", "ExecutorName"},
                                { "入库时间", "EnterDate"},
                                { "摘要 ", "Summary"},
                                { "批次", "BatchNo"},
                                { "入库数量合计 ", "CountTotal"},
                                { "入库金额合计 ", "A_TotalPrice"},
                             
                                { "制单人", "CreatorName"},

                                { "制单日期", "CreateDate"},
                                { "单据状态", "BillStatusName"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserName"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };
        if (HiddenMoreUnit.Value == "true")
        {
            aDetail = new string[,]  { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitID"},
                                { "基本数量", "ProductCount"},
                                { "单位", "UsedUnitName"},  //++
                                { "数量", "UsedUnitCount"}, //++
                                { "入库单价 ", "UnitPrice"},
                                { "入库金额 ", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
        }
        else
        {
            aDetail = new string[,]  { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "单位", "UnitID"},
                                { "数量", "ProductCount"},
                                { "入库单价 ", "UnitPrice"},
                                { "入库金额 ", "B_TotalPrice"},
                                { "备注", "DetaiRemark"},
                           };
        }

        


        #region 1.扩展属性
        DataTable dtExtTable = XBase.Business.Office.SupplyChain.TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.StorageInitail");
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 15; x++)
                {
                    if (x == i)
                    {
                        aBase[x, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
                    }
                }
            }
        }
        #endregion
        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMain = StorageInitailBus.GetStorageInitailDetailInfo(OutSellM_);
        // DataTable dtDetail = MRPBus.GetMRPDetailInfo(modelMRP);
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
            strBaseFields = "InNo|Title|StorageName|DeptName|ExecutorName|EnterDate|Summary|BatchNo|CountTotal|A_TotalPrice"
                            + "|CreatorName|CreateDate|BillStatusName|ConfirmorName|ConfirmDate|CloserName|CloseDate"
                            + "|ModifiedUserName|ModifiedDate|Remark"
                            + "|ExtField1|ExtField2|ExtField3|ExtField4|ExtField5|ExtField6|ExtField7|ExtField8|ExtField9|ExtField10";
            if (HiddenMoreUnit.Value == "True")
            {
                strDetailFields = "SortNo|ProductNo|ProductName|Specification|UnitID|ProductCount|UsedUnitName|UsedUnitCount|UnitPrice|B_TotalPrice|DetaiRemark";
            }
            else
            {
                strDetailFields = "SortNo|ProductNo|ProductName|Specification|UnitID|ProductCount|UnitPrice|B_TotalPrice|DetaiRemark";
            }
            
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("期初库存录入单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtMain, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("期初库存录入单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtMain, false);
        }
        #endregion

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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("期初库存录入单") + ".xls");
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
