<%@ WebHandler Language="C#" Class="SalaryCompanyRoyaltySet" %>

/**********************************************
 * 作    者： 肖合明
 * 创建日期： 2009/09/03
 * 描    述： 公司提成
 * 修改日期： 2009/09/03
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;

public class SalaryCompanyRoyaltySet : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            string DeptID = request.Form["DeptID"];

            string sbReturn = string.Empty;

            sbReturn = CreateSalaryListTable() + InitSalaryDetailInfo(DeptID) + EndTable();
            //设置输出流的 HTTP MIME 类型
            context.Response.ContentType = "text/plain";
            //向响应中输出数据
            context.Response.Write(sbReturn);
            context.Response.End();

        }
        if ("Save".Equals(action))
        {
            saveMessage(context);

        }
    }


    //保存
    //tempInfo .push (i,minMoney ,maxMoney,TaxPercent);
    public void saveMessage(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取DeptID
        string DeptID = request.Form["DeptID"];

        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        List<SalaryCompanyRoyaltySetModel> Modelist = new List<SalaryCompanyRoyaltySetModel>();
        string message = context.Request.Form["message"];
        string[] info = message.Split(',');
        int count = info.Length;
        for (int i = 0; i < count - 2; i++)
        {
            SalaryCompanyRoyaltySetModel model = new SalaryCompanyRoyaltySetModel();
            model.MiniMoney = info[i + 1];
            model.MaxMoney = info[i + 2];
            model.TaxPercent = info[i + 3];
            model.DeptID = DeptID;
            model.ModifiedUserID = userInfo.UserID;
            model.CompanyCD = userInfo.CompanyCD;
            Modelist.Add(model);
            i = i + 3;

        }

        if (SalaryCompanyRoyaltySetBus.SaveInfo(DeptID, Modelist))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }

    }

    #region 设置工资内容(通过分公司ID)
    /// <summary>
    /// 设置工资内容
    /// </summary>
    private string InitSalaryDetailInfo(string DeptID)
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //获取数据

        DataTable dtSalaryInfo = SalaryCompanyRoyaltySetBus.GetInfoTable(DeptID);
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
                    //选择
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                              + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                              + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                              + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                              + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  /></td>");
                    //公司名称
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'  align='center' >"
                                + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "DeptName")
                                + "' class='tdinput' id='txtDept_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtDept_" + (i + 1).ToString() + "'></td>");

                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
                                + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");

                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //提成率  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();'   onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");
                }
                else
                {
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                                + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  style=\"display:none \" /></td>");
                    //公司名称
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[i], "DeptName")
                                + "' class='tdinput' id='txtDept_" + (i + 1).ToString() + "' /><input type='hidden' id='HidtxtDept_" + (i + 1).ToString() + "'></td>");


                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniMoney")
                                + "' class='tdinput' id='txtMiniMoney_" + (i + 1).ToString() + "' /></td>");

                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'   readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxMoney")
                                + "' class='tdinput' id='txtMaxMoney_" + (i + 1).ToString() + "'  /></td>");

                    //提成率  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,\"2\");' onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "TaxPercent")
                                + "' class='tdinput' id='txtTaxPercent_" + (i + 1).ToString() + "' /></td>");

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
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>公司名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:30%'><span style=\"color:Red\">*</span>业绩上限（元）</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:20%'><span style=\"color:Red\">*</span>业绩下限（元）</td>               ");
        table.AppendLine("		<td class='ListTitle' style='width:15%'><span style=\"color:Red\">*</span>提成率（%）</td>             ");
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