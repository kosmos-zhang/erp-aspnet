<%@ WebHandler Language="C#" Class="SalaryPersonalRoyaltySet" %>

using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Data;

public class SalaryPersonalRoyaltySet  : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.Form["Action"];

        //分页控件查询数据
        if ("GetSub".Equals(action))
        {
            //从请求中获取排序列
            string EmpID = request.Form["EmpID"];
            string CustID = request.Form["CustID"];
            string sbReturn = string.Empty;

            sbReturn = CreateSalaryListTable() + InitSalaryDetailInfo(EmpID,CustID) + EndTable();
            //设置输出流的 HTTP MIME 类型
            context.Response.ContentType = "text/plain";
            //向响应中输出数据
            context.Response.Write(sbReturn);
            context.Response.End();

        }
    }


    #region 设置工资内容(通过分公司ID)


    private string InitSalaryDetailInfo(string EmpID, string CustID)
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //获取数据
        string selectOne = string.Empty;

        DataTable dtSalaryInfo = SalaryPersonalRoyaltySetBus.GetInsuPersonalTaxInfo(EmpID,CustID);
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
                    ////员工编号
                    //sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    //            + "</td>");
                    //员工姓名
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                             + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                             + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeName")
                             + "' class='tdinput' id='txtEmpName_" + (i + 1).ToString() + "' /><input type='hidden'  value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeNo")
                                + "'  id='txtEmpNO_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtEmp_" + (i + 1).ToString() + "'  value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeID")
                                + "'></td>");
                    
                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
                                + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");

                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //提成率 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '3' style='width:50%;' onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\"  onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");
                    //否
                    if ("0".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "ISCustomerRoyaltySet")))
                    {
                        selectOne = "";
                    }
                    //是
                    else
                    {
                        selectOne = "selected";
                    }
                    
                    //是否区分客户提成

                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                         + "<select id='ddlISCustomerRoyaltySet_" + (i + 1).ToString() + "' disabled=\"disabled\"><option value='0' >否</option><option value='1'" + selectOne + ">是</option></select></td>");

                    //客户编号
                    //sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    //+ "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                    //+ GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustNo")
                    //+ "' class='tdinput' id='txtCustNo_" + (i + 1).ToString() + "' onclick='fnSelectCustInfo("+(i+1)+")' /><input type='hidden' id='HidtxtCus_" + (i + 1).ToString() + "'  value='"
                    //+ GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustID")
                    //+ "'></td>");
                    //客户姓名
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                             + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                             + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustName")
                             + "' class='tdinput' id='txtCustName_" + (i + 1).ToString() + "' /><input type='hidden' value='"
                    + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustNo")
                    + "'  id='txtCustNo_" + (i + 1).ToString() + "'  /><input type='hidden' id='HidtxtCusID_" + (i + 1).ToString() + "'  value='"
                    + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustID")
                    + "'></td>");

                    //新客户提成
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    + "<input type='text' style='width:98%;' readonly=\"readonly\" value='"
                    + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "NewCustomerTax")
                    + "' class='tdinput' id='txtNewCustomerTax_" + (i + 1).ToString() + "' /></td>");
                    
                    //老客户提成
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                             + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                             + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "OldCustomerTax")
                             + "' class='tdinput' id='txtOldCustomerTax_" + (i + 1).ToString() + "' /></td>");
                    
                }
                else
                {
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                                + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  style=\"display:none \" /></td>");
                    
                    
                    ////部门名称
                    //sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                    //            + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                    //            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "DeptName")
                    //            + "' class='tdinput' id='txtDept_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtDept_" + (i + 1).ToString() + "'></td>");



                    //员工编号
                    //sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    //            + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                    //            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeNo")
                    //            + "' class='tdinput' id='txtEmpNO_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtEmp_" + (i + 1).ToString() + "'  value='"
                    //            + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeID")
                    //            + "'></td>");


                    //员工姓名
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                             + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                             + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeName")
                             + "' class='tdinput' id='txtEmpName_" + (i + 1).ToString() + "' /><input type='hidden'  value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeNo")
                                + "'  id='txtEmpNO_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtEmp_" + (i + 1).ToString() + "'  value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "EmployeeID")
                                + "'></td>");
                    
                    
                    
                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
                                + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");


                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //提成率 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '3' style='width:50%;' onkeydown='Numeric_OnKeyDown();'  readonly=\"readonly\" onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");
                    //否
                    if ("0".Equals(GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "ISCustomerRoyaltySet")))
                    {
                        selectOne = "";
                    }
                    //是
                    else
                    {
                        selectOne = "selected";
                    }

                    //是否区分客户提成

                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                         + "<select id='ddlISCustomerRoyaltySet_" + (i + 1).ToString() + "' disabled=\"disabled\"><option value='0' >否</option><option value='1'" + selectOne + ">是</option></select></td>");

                    //客户编号
                    //sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    //  + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                    //  + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustNo")
                    //  + "' class='tdinput' id='txtCustNo_" + (i + 1).ToString() + "' onclick='fnSelectCustInfo(" + (i+1) + ")' /><input type='hidden' id='HidtxtCus_" + (i + 1).ToString() + "'  value='"
                    //  + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustID")
                    //  + "'></td>");
                    //客户姓名
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                              + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                              + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustName")
                              + "' class='tdinput' id='txtCustName_" + (i + 1).ToString() + "' /><input type='hidden' value='"
                     + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustNo")
                     + "'  id='txtCustNo_" + (i + 1).ToString() + "'  /><input type='hidden' id='HidtxtCusID_" + (i + 1).ToString() + "'  value='"
                     + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "CustID")
                     + "'></td>");

                    //新客户提成
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                    + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                    + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "NewCustomerTax")
                    + "' class='tdinput' id='txtNewCustomerTax_" + (i + 1).ToString() + "' /></td>");

                    //老客户提成
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                             + "<input type='text' readonly=\"readonly\"  style='width:98%;' value='"
                             + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "OldCustomerTax")
                             + "' class='tdinput' id='txtOldCustomerTax_" + (i + 1).ToString() + "' /></td>");
                }

                //插入行结束标识
                sbSalaryInfo.AppendLine("</tr>");
            }
        }

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
        //table.AppendLine("		<td class='ListTitle' style='width:15%'>员工编号</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>员工姓名</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>业绩上限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'><span style=\"color:Red\">*</span>业绩下限</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'>提成率(%)</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:15%'><span style=\"color:Red\">*</span>是否区分客户提成</td>             ");
        //table.AppendLine("		<td class='ListTitle' style='width:15%'>客户编号</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'><span style=\"color:Red\">*</span>客户名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:14%'>新客户提成率(%)</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:14%'>老客户提成率(%)</td>             ");
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}