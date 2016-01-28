using System;
using System.Collections;
using System.Collections.Generic;
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

/// <summary>
/// 质检申请单打印界面
/// </summary>
public partial class Pages_PrinttingModel_StorageManager_StorageQualityCheckPrint : BasePage
{
    #region 质检申请单ID
    public int intID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }

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
    #endregion

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
        pModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        pModel.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_QUALITYCHECK);
        pModel.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_QUALITYADD;

        StorageQualityCheckApplay model = new StorageQualityCheckApplay();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.ID = this.intID;

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
                                { "单据编号", "ApplyNo"}, 
                                { "主题", "Title"}, 
                                { "源单类型", "FromType" },
                                { "往来单位", "CustName" },
                                { "往来单位大类", "CustBigTypeName" },
                                { "质检类别", "CheckType" },
                                { "生产负责人", "PrincipalName" },
                                { "生产部门", "DeptName" },
                                { "检验方式", "CheckMode" },
                                { "报检人员", "CheckerName" },
                                { "报检部门", "CheckDeptName" },
                                { "报检日期", "CheckDate" },
                                { "报检数量合计", "CountTotal" },
                                { "单据状态", "BillStatusName"},
                                { "制单人", "CreatorName"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorName"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserName"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };

        string[,] aDetail = { 
                                { "序号", "SortNo"}, 
                                { "物品名称", "ProductName" },
                                { "物品编号", "ProdNo"}, 
                                { "单位", "CodeName" },
                                { "报检数量", "ProductCount"},
                                { "已报检数量", "CheckedCount"},
                                { "已检数量", "RealCheckedCount"},
                                { "备注", "Remark"}
                           };

        if (_isMoreUnit)
        {// 启用多计量单位
            aDetail = new string[,]
                            { 
                                { "序号", "SortNo"}, 
                                { "物品名称", "ProductName" },
                                { "物品编号", "ProdNo"}, 
                                { "基本单位", "CodeName" },
                                { "基本数量", "ProductCount"},
                                { "单位", "UsedUnitName" },
                                { "报检数量", "UsedUnitCount"},
                                { "已报检数量", "CheckedCount"},
                                { "已检数量", "RealCheckedCount"},
                                { "备注", "Remark"}
                           };

        }

        #region 扩展属性
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba.QualityCheckApplay");
        int countExt = 0;
        for (int i = 0; i < dtExtTable.Rows.Count; i++)
        {
            aBase[i, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
            countExt++;
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(pModel);
        DataTable dt = StorageQualityCheckPro.GetOneQuality(model.ID, model.CompanyCD);
        Convert(dt);
        DataTable dtDetail = StorageQualityCheckPro.GetQualityDetail(model.ID, model.CompanyCD);
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

            for (int i = 0; i < countExt; i++)
            {
                strBaseFields += "|ExtField" + (i + 1);
            }
            strDetailFields = GetDefaultFields(aDetail);
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("质检申请单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("质检申请单", strBaseFields, strDetailFields, aBase, aDetail, dt, dtDetail, false);
        }
        #endregion

    }

    /// <summary>
    /// 转换数据
    /// </summary>
    /// <param name="dt"></param>
    private void Convert(DataTable dt)
    {
        dt.Columns.Add("FromType1", typeof(string));
        dt.Columns.Add("CheckType1", typeof(string));
        dt.Columns.Add("CheckMode1", typeof(string));

        #region 数据转换
        foreach (DataRow dr in dt.Rows)
        {
            switch (dr["FromType"].ToString())
            {
                case "1":
                    dr["FromType1"] = "采购到货单";
                    break;
                case "0":
                    dr["FromType1"] = "无来源";
                    break;
                case "2":
                    dr["FromType1"] = "生产任务单";
                    break;
            }
            switch (dr["CheckType"].ToString())
            {
                case "1":
                    dr["CheckType1"] = "进货检验";
                    break;
                case "2":
                    dr["CheckType1"] = "过程检验";
                    break;
                case "3":
                    dr["CheckType1"] = "最终检验";
                    break;
            }
            switch (dr["CheckMode"].ToString())
            {
                case "1":
                    dr["CheckMode1"] = "全检";
                    break;
                case "2":
                    dr["CheckMode1"] = "抽检";
                    break;
                case "3":
                    dr["CheckMode1"] = "临检";
                    break;
            }

        }
        #endregion

        dt.Columns.Remove("FromType");
        dt.Columns.Remove("CheckType");
        dt.Columns.Remove("CheckMode");

        dt.Columns["FromType1"].ColumnName = "FromType";
        dt.Columns["CheckType1"].ColumnName = "CheckType";
        dt.Columns["CheckMode1"].ColumnName = "CheckMode";
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("质检申请单") + ".xls");
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
