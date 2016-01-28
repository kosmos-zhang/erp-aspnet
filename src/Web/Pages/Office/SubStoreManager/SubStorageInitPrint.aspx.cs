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
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.SubStoreManager;
using XBase.Model.Office.SubStoreManager;
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

/// <summary>
/// 分店期初库存打印界面
/// </summary>
public partial class Pages_Office_SubStoreManager_SubStorageInitPrint : BasePage
{
    #region 分店期初库存ID
    public int intID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;


    /// <summary>
    /// 是否启用多计量单位(ture：启用；false：不启用)
    /// </summary>
    public bool IsMoreUnit
    {
        get
        {
            return _isMoreUnit;
        }
        set
        {
            _isMoreUnit = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        UserInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        _isMoreUnit = UserInfo.IsMoreUnit;
        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel pModel = new PrintParameterSettingModel();
        pModel.CompanyCD = UserInfo.CompanyCD;
        pModel.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_SUBSTORAGE);
        pModel.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_SUBSTORAGEIN;


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
                                { "入库单主题", "Title"}, 
                                { "分店名称", "DeptName" },
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "单据状态", "BillStatusName"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };

        string[,] aDetail = { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "standard"}, 
                                { "单位", "UnitName"}, 
                                { "入库数量", "SendCount"}
                           };
        if (_isMoreUnit)
        {// 启用多计量单位
            aDetail = new string[,]
                            { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProductNo"}, 
                                { "物品名称", "ProductName"}, 
                                { "批次", "BatchNo"}, 
                                { "规格", "standard"}, 
                                { "基本单位", "UnitName" },
                                { "基本数量", "SendCount"},
                                { "单位", "UsedUnitName"}, 
                                { "入库数量", "UsedUnitCount"}
                           };
        }

        #region 扩展属性
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(UserInfo.CompanyCD, "", "officedba.SubStorageIn");
        int countExt = 0;
        for (int i = 0; i < dtExtTable.Rows.Count; i++)
        {
            aBase[i, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
            countExt++;
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(pModel);
        DataTable dt = SubStorageBus.SubStorageIn(intID);
        string deptID = UserInfo.DeptID.ToString();
        DataRow dtDeptID = SubStorageDBHelper.GetSubDeptFromDeptID(UserInfo.DeptID.ToString());
        if (dtDeptID != null)
        {
            deptID = dtDeptID["ID"].ToString();
        }
        DataTable dtDetail = SubStorageBus.Details(intID, deptID);
        dtDetail.Columns.Add("SortNo", typeof(string));
        for (int i = 0; i < dtDetail.Rows.Count; i++)
        {
            dtDetail.Rows[i]["SortNo"] = i + 1;
        }
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
            strBaseFields = GetDefaultFields(aBase);
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields += "|ExtField" + (i + 1);
                }
            }
            strDetailFields = GetDefaultFields(aDetail);
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("分店期初库存", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("分店期初库存", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, false);
        }
        #endregion

    }

    /// <summary>
    /// 获得默认字段
    /// </summary>
    /// <param name="a">二维字符串数组</param>
    /// <returns></returns>
    private string GetDefaultFields(string[,] aBase)
    {
        StringBuilder sb = new StringBuilder();
        int len = aBase.Length / 2;
        for (int i = 0; i < len; i++)
        {
            if (aBase[i, 1].StartsWith("ExtField"))
            {
                continue;
            }
            sb.AppendFormat("{0}|", aBase[i, 1]);
        }
        return sb.ToString().TrimEnd('|');
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("分店期初库存") + ".xls");
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
