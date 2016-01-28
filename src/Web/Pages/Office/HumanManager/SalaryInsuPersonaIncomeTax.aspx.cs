/**********************************************
 * 类作用：   个人所得税
 * 建立人：   王保军
 * 建立时间： 2009/06/19
 ***********************************************/
using System;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Office.HumanManager;

public partial class Pages_Office_HumanManager_SalaryInsuPersonaIncomeTax : BasePage 
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
        divSalaryList.InnerHtml = CreateSalaryListTable() + InitSalaryDetailInfo() + EndTable();
    }

    #region 设置工资内容
    /// <summary>
    /// 设置工资内容
    /// </summary>
    private string InitSalaryDetailInfo()
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //获取数据

        DataTable dtSalaryInfo = InsuPersonaIncomeTaxBus.GetInsuPersonalTaxInfo();
        //数据存在时
        if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0)
        {
            //遍历显示所有数据
            for (int i = 0; i < dtSalaryInfo.Rows.Count; i++)
            {
                //插入行开始标识
                sbSalaryInfo.AppendLine("<tr>");
                //选择框
                if (i == dtSalaryInfo.Rows.Count - 1)
                {
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                              + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                              + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                              + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                              + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  /></td>");
                    //应缴税上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'    onchange='Number_round(this,\"0\");'   readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MinMoney")
                                + "' class='tdinput' id='txtMinMoney_" + (i + 1).ToString() + "' /></td>");

                    //应缴税下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'    onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //税率  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;'     onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtPersonTaxPercent_" + (i + 1).ToString() + "' /></td>");
                    //速算扣除数（元）
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                          + "<input type='text' maxlength = '16' readonly ='readonly'  style='width:98%;'  value='"
                          + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MinusMoney")
                          + "' class='tdinput' id='txtPersonMinusMoney_" + (i + 1).ToString() + "' /></td>");
                }
                else
                {
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                                + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  style=\"display:none \" /></td>");
                    //应缴税上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'    readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MinMoney")
                                + "' class='tdinput' id='txtMinMoney_" + (i + 1).ToString() + "' /></td>");

                    //应缴税下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'     readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //税率  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;'   onchange='Number_round(this,\"2\");'   readonly=\"readonly\" onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtPersonTaxPercent_" + (i + 1).ToString() + "' /></td>");
                    //速算扣除数（元）
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                          + "<input type='text' maxlength = '16' readonly ='readonly'  style='width:98%;'  value='"
                          + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MinusMoney")
                          + "' class='tdinput' id='txtPersonMinusMoney_" + (i + 1).ToString() + "' /></td>");

                }
             
                //插入行结束标识
                sbSalaryInfo.AppendLine("</tr>");
            }
        }

        //返回信息
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
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>应交税上限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>应交税下限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>税率(%)</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:13%'>速算扣除数</td>             ");
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
