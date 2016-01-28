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
using System.Text;
using XBase.Common;
using XBase.Business.Office.HumanManager;
public partial class Pages_Office_HumanManager_SalaryDepatmentRoyaltySet : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期表示
        if (!IsPostBack)
        {
            //显示工资项列表
            InitSalaryInfo();
        }
    }
    /// <summary>
    /// 设置工资表格
    /// </summary>
    private void InitSalaryInfo()
    {
        //设置工资表格内容
        divSalaryList.InnerHtml = CreateSalaryListTable() + EndTable();
    }

    #region 设置工资内容
    /// <summary>
    /// 设置工资内容
    /// </summary>
    private string InitSalaryDetailInfo()
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        ////获取数据

        //DataTable dtSalaryInfo = SalaryDepatmentRoyaltySetBus.GetInsuPersonalTaxInfo();
        //if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0)
        //{
        //    //遍历显示所有数据
        //    for (int i = 0; i < dtSalaryInfo.Rows.Count; i++)
        //    {
        //        //插入行开始标识
        //        sbSalaryInfo.AppendLine("<tr>");
        //        //选择框
        //        if (i == dtSalaryInfo.Rows.Count - 1)
        //        {
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                      + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
        //                      + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
        //                      + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
        //                      + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  /></td>");
        //            //部门名称
        //            sbSalaryInfo.AppendLine("<td class='tdColInput'>"
        //                  + "<input type='text' maxlength = '16' readonly ='readonly'  style='width:98%;'  value='"
        //                  + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "DeptName")
        //                  + "' class='tdinput' id='DeptName_" + (i + 1).ToString() + "' /></td>");
        //            //业绩上限
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                        + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   readonly=\"readonly\" style='width:98%;' value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
        //                        + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");

        //            //业绩下限 
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                        + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
        //                        + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

        //            //提成率 
        //            sbSalaryInfo.AppendLine("<td class='tdColInput'>"
        //                        + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();'   onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
        //                        + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");
        //        }
        //        else
        //        {
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                        + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
        //                        + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
        //                        + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
        //                        + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  style=\"display:none \" /></td>");
        //            //部门名称
        //            sbSalaryInfo.AppendLine("<td class='tdColInput'>"
        //                  + "<input type='text' maxlength = '16' readonly ='readonly'  style='width:98%;'  value='"
        //                  + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "DeptName")
        //                  + "' class='tdinput' id='txtDeptName_" + (i + 1).ToString() + "' /></td>");
        //            //业绩上限
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                        + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
        //                        + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");

        //            //业绩下限
        //            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
        //                        + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'   readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
        //                        + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

        //            //提成率  
        //            sbSalaryInfo.AppendLine("<td class='tdColInput'>"
        //                        + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,\"2\");'   readonly=\"readonly\" onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
        //                        + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
        //                        + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");
        //        }

        //        //插入行结束标识
        //        sbSalaryInfo.AppendLine("</tr>");
        //    }
        //}

        return sbSalaryInfo.ToString();
    }
    #endregion

    #region 生成表格的标题
    /// <summary>
    /// 生成表格的标题
    /// </summary>
    /// <returns></returns>
    private string CreateSalaryListTable()
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='99%' border='0' id='tblSalary'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>                                                                  ");
        table.AppendLine("		<td class='ListTitle' width='50'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>部门名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>业绩上限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>业绩下限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>提成率(%)</td>             ");
        table.AppendLine("	</tr>                                                                 ");
        //返回表格语句
        return table.ToString();
    }
    #endregion

    #region 返回表格的结束符
    /// <summary>
    /// 返回表格的结束符
    /// </summary>
    /// <returns></returns>
    private string EndTable()
    {
        return "</table>";
    }
    #endregion
}
