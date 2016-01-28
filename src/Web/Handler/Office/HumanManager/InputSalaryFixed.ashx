<%@ WebHandler Language="C#" Class="InputSalaryFixed" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/09
 * 描    述： 工资录入
 * 修改日期： 2009/05/09
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;

public class InputSalaryFixed : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取操作类型
        string action = context.Request.Params["Action"].ToString();
        //查询操作
        if ("Search".Equals(action))
        {
            //查询信息
            SearchSalaryInfo(context);
        }
  
        else
        {
            //保存工资信息
            SaveSalaryInfo(context);
        }
    }

    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SearchSalaryInfo(HttpContext context)
    {
        //设置参数
        EmployeeSearchModel model = new EmployeeSearchModel();
        //员工编号
        model.EmployeeNo = context.Request.Params["EmployeeNo"].ToString();
        //员工工号
        model.EmployeeNum = context.Request.Params["EmployeeNum"].ToString();
        //员工姓名
        model.EmployeeName = context.Request.Params["EmployeeName"].ToString();
        //所在岗位
        model.QuarterID = context.Request.Params["QuarterID"].ToString();
        //岗位职等
        model.AdminLevelID = context.Request.Params["AdminLevelID"].ToString();
        //获取工资项
        DataTable dtSalaryItem = SalaryItemBus.GetSalaryItemInfo(true);
        //获取员工信息
        DataTable dtEmplInfo = EmployeeInfoBus.GetWorkEmplInfo(model);
        DataTable dtNew = SalaryItemBus.GetOutEmployeeInfo(model);
        dtEmplInfo.Merge(dtNew);
        //获取人员总数
        int userCount = 0;
        //获取员工数
        if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0) userCount = dtEmplInfo.Rows.Count;
        //生成表格
        string salaraInfo = CreateSalaryListTable(dtSalaryItem)
                            + InitSalaryDetailInfo(dtSalaryItem, dtEmplInfo, userCount) + "</table>";
        //设置人员总数
        salaraInfo += "<input type='hidden' id='txtUserCount' value='" + userCount.ToString() + "' />";

        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(salaraInfo);
        context.Response.End();
    }

    /// <summary>
    /// 设置工资内容
    /// <param name="dtSalaryInfo">工资项信息</param>
    /// </summary>
    private string InitSalaryDetailInfo(DataTable dtSalaryInfo, DataTable dtEmplInfo, int userCount)
    {
        #region 获取工资设置相关信息
        //数据不存在时
        if (userCount == 0)
        {
            //定义变量
            int salaryCount = 0;
            //工资项不为空时，设置工资项总数
            if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0) salaryCount = dtSalaryInfo.Rows.Count;
            return "<tr><td colspan='" + (salaryCount + 6).ToString() + "' align='center'>符合条件的数据不存在！</td></tr>";
        }
        //变量定义
        SalaryStandardModel salaryModel = new SalaryStandardModel();
        //设置启用状态
        salaryModel.UsedStatus = ConstUtil.USED_STATUS_ON;
        //查询岗位标准工资
        DataTable dtQuarSalary = SalaryStandardBus.SearchSalaryStandardInfo(salaryModel);
        //获取员工工资
        DataTable dtEmplSalary = SalaryEmployeeBus.GetSalaryEmployeeInfo();
        #endregion

        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //员工信息不存在时，返回空值
        if (dtEmplInfo == null || dtEmplInfo.Rows.Count < 1) return sbSalaryInfo.ToString();
        //遍历员工信息设置员工对应工资
        for (int i = 0; i < userCount; i++)
        {
            //行背景色定义
            string backColor = i % 2 == 0 ? "#FFFFFF" : "#E7E7E7";
            //插入行开始标识
            sbSalaryInfo.AppendLine("<tr style='background-color:" + backColor + ";' onmouseover='this.style.backgroundColor=\"#cfc\";'"
                                            + " onmouseout='this.style.backgroundColor=\"" + backColor + "\";'>");

            #region 设置员工信息
            //员工ID
            string emplyID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "ID");
            //获取岗位ID
            string quarterID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "QuarterID");
            //获取岗位职等ID
            string adminLevelID = GetSafeData.GetStringFromInt(dtEmplInfo.Rows[i], "AdminLevelID");

            //员工编号
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + "<input type='hidden' id='txtEmplID_" + (i + 1).ToString() + "' value='" + emplyID + "' />"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeNo") + "</td>");
            //员工姓名
            sbSalaryInfo.AppendLine("<td align='center' id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "EmployeeName") + "</td>");
            //所在部门
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "DeptName") + "</td>");
            //所在岗位
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "QuarterName") + "</td>");
            //岗位职等
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + GetSafeData.ValidateDataRow_String(dtEmplInfo.Rows[i], "AdminLevelName") + "</td>");

            #endregion

            #region 设置工资信息
            //工资信息数据存在时
            if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0)
            {
                //工资合计
                decimal totalSalary = 0;
                //遍历显示所有工资信息
                for (int j = 0; j < dtSalaryInfo.Rows.Count; j++)
                {
                    //获取工资项ID 
                    string salaryID = GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[j], "ItemNo");
                    //获取工资项计算区分
                    string payFlag = GetSafeData.ValidateDataRow_String(dtSalaryInfo.Rows[j], "PayFlag");
                    //定义变量
                    string salaryMoney = string.Empty;
                    string hadSetSalaryFlag = "0";
                    //是否设置
                    DataRow[] drSalary = dtEmplSalary.Select("EmployeeID = " + emplyID
                                    + " and  ItemNo = " + salaryID);
                    //已经设置时
                    if (drSalary != null && drSalary.Length > 0)
                    {
                        //获取已经设置的工资
                        salaryMoney = GetSafeData.ValidateDataRow_String(drSalary[0], "SalaryMoney");
                        //设置编辑标识
                        hadSetSalaryFlag = "1";
                    }
                    else
                    {
                        //获取岗位工资信息
                        DataRow[] drQuarSalary;
                        if (!string.IsNullOrEmpty(quarterID) && !string.IsNullOrEmpty(adminLevelID))
                        {
                            drQuarSalary = dtQuarSalary.Select("QuarterID = " + quarterID
                                           + " and  AdminLevel = " + adminLevelID + " and  ItemNo = " + salaryID);
                        }
                        else
                        {
                            drQuarSalary = null;
                        }
                        //设置岗位工资
                        if (drQuarSalary != null && drQuarSalary.Length > 0)
                        {
                            //设置岗位工资
                            salaryMoney = GetSafeData.ValidateDataRow_String(drQuarSalary[0], "UnitPrice");
                        }
                    }
                    //工资设置时，计算总额
                    if (!string.IsNullOrEmpty(salaryMoney))
                    {
                        //判断是否扣钱
                        if ("0".Equals(payFlag))
                        {
                            totalSalary += decimal.Parse(salaryMoney);//加钱
                        }
                        else
                        {
                            totalSalary -= decimal.Parse(salaryMoney);//扣钱
                        }
                    }
                    //工资额
                    if (salaryMoney == "0.00")
                        salaryMoney = "";
                        
                    sbSalaryInfo.AppendLine("<td class='tdColInputLeft'>"
                                + "<input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='" + hadSetSalaryFlag + "' />"
                                + "<input type='hidden' id='txtPayFlag_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='" + payFlag + "' />"
                                + "<input type='text' maxlength = '12' size='12' style='width:85%;' value='" + salaryMoney + "' onchange='Number_round(this,\"2\");'    onblur='CalculateTotalSalary(this, \""
                                + (i + 1).ToString() + "\");'"
                                + " class='tdinput' id='txtSalaryMoney_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' /></td>");
                }
                //工资合计
                sbSalaryInfo.AppendLine("<td class='tdColInputLeft'>"
                            + "<input type='text' disabled style='width:85%;' value='"
                            + totalSalary.ToString()
                            + "' class='tdinput' id='txtTotalSalary_" + (i + 1).ToString() + "' /></td>");
            }
            #endregion

            //插入行结束标识
            sbSalaryInfo.AppendLine("</tr>");
        }

        //返回信息
        return sbSalaryInfo.ToString();
    }

    /// <summary>
    /// 生成表格的标题
    /// </summary>
    /// <returns></returns>
    private string CreateSalaryListTable(DataTable dtSalaryItem)
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblSalaryDetail'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>");
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>员工编号</td>");
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>员工姓名</td>");
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>所在部门</td>");
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>所在岗位</td>");
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>岗位职等</td>");

        //工资项数据存在时
        int salaryCount = 0;
        if (dtSalaryItem != null && dtSalaryItem.Rows.Count > 0)
        {
            //获取工资项总数
            salaryCount = dtSalaryItem.Rows.Count;
            //遍历所有工资项，设置到表格中
            for (int i = 0; i < salaryCount; i++)
            {
                //<span style=\"color:Red\">*</span>
                //获取工资项名称
                string salaryName = GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemName");
                //设置工资项名称
                table.AppendLine("<td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>"
                                + "<input type='hidden' id='txtSalaryName_" + (i + 1).ToString() + "' value='" + salaryName + "' />"
                                + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.ValidateDataRow_String(dtSalaryItem.Rows[i], "ItemNo") + "' />" + salaryName + "</td>");
            }
        }
        table.AppendLine("    <td class='ListTitle' style='font-weight: bolder;' background='../../../images/Main/Table_bg.jpg'>工资合计");

        table.AppendLine("<input type='hidden' id='txtSalaryCount' value='" + salaryCount.ToString() + "' /></td>");

        table.AppendLine("	</tr>");
        //返回表格语句
        return table.ToString();
    }
    
    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SaveSalaryInfo(HttpContext context)
    {
        //编辑请求信息
        ArrayList lstEdit = EditRequstData(context.Request);
        //执行保存
        bool isSucc = SalaryEmployeeBus.SaveSalaryEmployeeInfo(lstEdit);
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //保存成功时
        if (isSucc)
        {
            //设置记录总数
            sbReturn.Append("{");
            sbReturn.Append("EditStatus:1");
            //设置数据
            sbReturn.Append(",DataInfo:");
            sbReturn.Append(SalaryItem2Json(lstEdit));
            sbReturn.Append("}");
        }
        //保存未成功时
        else
        {
            //设置记录总数
            sbReturn.Append("{EditStatus:0}");
        }
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    
    /// <summary>
    /// 工资项记录转换为Json
    /// </summary>
    /// <param name="lstEdit">工资项记录</param>
    /// <returns></returns>
    private string SalaryItem2Json(ArrayList lstEdit)
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //记录存在时
        if (lstEdit != null && lstEdit.Count > 0)
        {
            //数据开始符
            sbSalaryInfo.Append("[");
            //遍历所有记录转化为Json
            for (int i = 0; i < lstEdit.Count; i++)
            {
                //获取工资项信息
                SalaryEmployeeModel model = (SalaryEmployeeModel)lstEdit[i];
                //编辑模式
                string editFlag = model.EditFlag;
                //新添加的项转换为Json值
                if ("0".Equals(editFlag) || "3".Equals(editFlag))
                {
                    //行数据开始符
                    sbSalaryInfo.Append("{");
                    //编辑模式
                    sbSalaryInfo.Append("\"EditFlag\":\"" + editFlag + "\"");
                    //行编号
                    sbSalaryInfo.Append(",\"RowNo\":\"" + model.RowNo + "\"");
                    //工资项列编号
                    sbSalaryInfo.Append(",\"SalaryColumnNo\":\"" + model.SalaryColumnNo + "\"");
                    //行数据结束符
                    sbSalaryInfo.Append("},");
                }
            }
            //替换最后的,
            sbSalaryInfo.Replace(",", "", sbSalaryInfo.Length - 1, 1);
            //数据结束符
            sbSalaryInfo.Append("]");
        }
        //返回值
        return sbSalaryInfo.ToString();
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private ArrayList EditRequstData(HttpRequest request)
    {
        //定义变量
        ArrayList lstReturn = new ArrayList();
        //获取人员总数
        int userCount = int.Parse(request.Params["UserCount"]);
        //获取工资项列表数据总数
        int salaryCount = int.Parse(request.Params["SalaryCount"]);

        //遍历所有员工
        for (int i = 1; i <= userCount; i++)
        {
            //获取员工ID
            string emplID = request.Params["EmployeeID_" + i.ToString()].ToString();
            //遍历所有工资项
            for (int j = 1; j <= salaryCount; j++)
            {
                //定义Model变量
                SalaryEmployeeModel model = new SalaryEmployeeModel();
                //员工ID
                model.EmployeeID = emplID;
                //工资项ID
                model.ItemNo = request.Params["SalaryItemID_" + j.ToString()].ToString();
                //获取工资值
                string salaryMoney = request.Params["SalaryMoney_" + i.ToString() + "_" + j.ToString()].ToString();
                //获取编辑模式
                string editFlag = request.Params["EditFlag_" + i.ToString() + "_" + j.ToString()].ToString();
                //工资值未输入时
                if (string.IsNullOrEmpty(salaryMoney))
                {
                    //如果是已设置的项，删除原来设置的值
                    if ("1".Equals(editFlag))
                    {
                        //编辑模式
                        model.EditFlag = "3";
                        //设置行号
                        model.RowNo = i.ToString();
                        //设置工资列编号
                        model.SalaryColumnNo = j.ToString();
                    }
                    //未设置时，跳过，执行下一个工资项
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //金额
                    model.SalaryMoney = salaryMoney;
                    //编辑模式
                    model.EditFlag = editFlag;
                    //新建时
                    if ("0".Equals(editFlag))
                    {
                        //设置行号
                        model.RowNo = i.ToString();
                        //设置工资列编号
                        model.SalaryColumnNo = j.ToString();
                    }
                }
                //添加记录
                lstReturn.Add(model);
            }
        }

        return lstReturn;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}