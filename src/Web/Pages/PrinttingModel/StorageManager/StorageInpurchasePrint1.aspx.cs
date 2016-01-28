using System;
using System.Data;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Common;
using XBase.Common;

public partial class Pages_PrinttingModel_StorageManager_StorageInpurchasePrint1 : BasePage
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_INPURCHASE.ToString();

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_INPURCHASE;

        StorageInPurchaseModel InPurchaseM_ = new StorageInPurchaseModel();
        InPurchaseM_.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        InPurchaseM_.ID = this.intMrpID.ToString();

        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase;
        string[,] aDetail;
        if (GetIsDisplayPrice() != "none")
        {
            aBase = new string[,]{ 
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
                                { "入库单主题", "Title"}, 
                                { "源单类型", "FromTypeName" },
                                { "采购到货单", "ArriveNo" },
                                { "供应商", "ProviderName"},
                                { "采购员", "PurchaserName"},
                                { "采购部门", "DeptName"},
                                { "交货人", "TakerName"},
                                { "验收人", "CheckerName"},
                                { "入库部门", "InPutDeptName"},
                                { "入库人", "ExecutorName"},
                                { "入库时间", "EnterDate"},
                                { "摘要", "Summary"},
                                { "批次", "BatchNo"},
                                { "备注", "Remark"},
                                { "数量合计", "CountTotal"},
                                { "金额合计", "B_TotalPrice"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserName"},
                                { "最后更新日期", "ModifiedDate"},
                                { "单据状态", "BillStatus"},

                          };

            if (HiddenMoreUnit.Value == "true")
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "颜色", "ColorName" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitID"},
                                { "单位", "UsedUnitName"},  //++                                
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已入库数量", "InCount"},
                                { "未入库数量", "NotInCount"},
                                { "基本数量", "ProductCount"},
                                { "数量", "UsedUnitCount"}, //++
                                { "单价", "UnitPrice"},
                                { "税率", "TaxRate"},
                                { "金额", "TotalPrice"},
                                { "已退货数量", "BackCount"},
                           };
            }
            else
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "颜色", "ColorName" },
                                { "规格", "Specification" },
                                { "单位", "UnitID"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已入库数量", "InCount"},
                                { "未入库数量", "NotInCount"},
                                { "实收数量", "ProductCount"},
                                { "单价", "UnitPrice"},
                                { "税率", "TaxRate"},
                                { "金额", "TotalPrice"},
                                { "已退货数量", "BackCount"},
                           };
            }
            
        }
        else
        {
            aBase = new string[,]{ 
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
                                { "入库单主题", "Title"}, 
                                { "源单类型", "FromTypeName" },
                                { "采购到货单", "ArriveNo" },
                                { "供应商", "ProviderName"},
                                { "采购员", "PurchaserName"},
                                { "采购部门", "DeptName"},
                                { "交货人", "TakerName"},
                                { "验收人", "CheckerName"},
                                { "入库部门", "InPutDeptName"},
                                { "入库人", "ExecutorName"},
                                { "入库时间", "EnterDate"},
                                { "摘要", "Summary"},
                                { "批次", "BatchNo"},
                                { "备注", "Remark"},
                                { "数量合计", "CountTotal"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserName"},
                                { "最后更新日期", "ModifiedDate"},
                                { "单据状态", "BillStatus"},

                          };

            if (HiddenMoreUnit.Value == "true")
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "颜色", "ColorName" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitID"},
                                { "单位", "UsedUnitName"},  //++ 
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已入库数量", "InCount"},
                                { "未入库数量", "NotInCount"},
                                { "基本数量", "ProductCount"},
                                { "数量", "UsedUnitCount"}, //++
                                { "已退货数量", "BackCount"},
                           };
            }
            else
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName" },
                                { "颜色", "ColorName" },
                                { "规格", "Specification" },
                                { "单位", "UnitID"},
                                { "仓库", "StorageName"},
                                { "通知数量", "FromBillCount"},
                                { "已入库数量", "InCount"},
                                { "未入库数量", "NotInCount"},
                                { "实收数量", "ProductCount"},
                                { "已退货数量", "BackCount"},
                           };
            }
        }

        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = XBase.Business.Office.SupplyChain.TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.StorageInPurchase");
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
        DataTable dtDetail = StorageInPurchaseBus.GetStorageInPurchaseDetailInfo(InPurchaseM_.ID, InPurchaseM_.CompanyCD);
        DataTable dtMain = StorageInPurchaseBus.GetStorageInPurchaseInfo(InPurchaseM_.ID, InPurchaseM_.CompanyCD);
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
            #region 未设置过打印模板设置 默认显示所有的
            isSeted.Value = "0";

            /*未设置过打印模板设置时，默认显示的字段  基本信息字段*/
            for (int m = 10; m < aBase.Length / 2; m++)
            {
                strBaseFields = strBaseFields + aBase[m, 1] + "|";
            }
            /*未设置过打印模板设置时，默认显示的字段 基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "ExtField" + (i + 1) + "|";
                }
            }
            /*未设置过打印模板设置时，默认显示的字段 明细信息字段*/
            for (int n = 0; n < aDetail.Length / 2; n++)
            {
                strDetailFields = strDetailFields + aDetail[n, 1] + "|";
            }
            #endregion
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("采购入库单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("采购入库单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMain, dtDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("采购入库单") + ".xls");
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
