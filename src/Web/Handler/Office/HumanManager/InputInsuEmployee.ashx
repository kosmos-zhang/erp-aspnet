<%@ WebHandler Language="C#" Class="InputInsuEmployee" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/12
 * 描    述： 社会保险录入
 * 修改日期： 2009/05/12
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

public class InputInsuEmployee : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            //保存社会保险信息
            SaveSalaryInfo(context);
        }
    }

    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SearchSalaryInfo(HttpContext context)
    {
        //获取社会保险项
        DataTable dtInsuSocial = InsuSocialBus.SearchInsuSocialInfo(context.Request.Params["InsuranceName"]);
        //设置参数
        EmployeeSearchModel model = new EmployeeSearchModel();
        //员工编号
        model.EmployeeNo = context.Request.Params["EmployeeNo"].ToString();
        //员工姓名
        model.EmployeeName = context.Request.Params["EmployeeName"].ToString();
        //所在岗位
        model.QuarterID = context.Request.Params["QuarterID"].ToString();
        //获取员工信息
        DataTable dtEmplInfo = EmployeeInfoBus.GetWorkEmplInfo(model);
        DataTable dtNew = SalaryItemBus.GetOutEmployeeInfo(model);
        dtEmplInfo.Merge(dtNew);
        //获取人员总数
        int userCount = 0;
        //获取员工数
        if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0) userCount = dtEmplInfo.Rows.Count;
        //生成表格
        string salaraInfo = CreateListTitle(dtInsuSocial)
                            + InitInsuDetailInfo(dtInsuSocial, dtEmplInfo, userCount) + "</table>";
        //设置人员总数
        salaraInfo += "<input type='hidden' id='txtUserCount' value='" + userCount.ToString() + "' />";

        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(salaraInfo);
        context.Response.End();
    }

    /// <summary>
    /// 设置社会保险内容
    /// <param name="dtInsuSocial">社会保险项信息</param>
    /// <param name="dtEmplInfo">人员信息</param>
    /// <param name="userCount">人员总数</param>
    /// </summary>
    private string InitInsuDetailInfo(DataTable dtInsuSocial, DataTable dtEmplInfo, int userCount)
    {
        #region 获取社会保险设置相关信息
        //数据不存在时
        if (userCount == 0)
        {
            //定义变量
            int salaryCount = 0;
            //社会保险项不为空时，设置社会保险项总数
            if (dtInsuSocial != null && dtInsuSocial.Rows.Count > 0) salaryCount = dtInsuSocial.Rows.Count;
            return "<tr><td colspan='" + (salaryCount + 6).ToString() + "' align='center'>符合条件的数据不存在！</td></tr>";
        }
        else
        {
            //定义变量
            int salaryCount = 0;
            //社会保险项不为空时，设置社会保险项总数
            if (dtInsuSocial != null && dtInsuSocial.Rows.Count > 0)
            {
            }
            else
            {
                salaryCount = dtInsuSocial.Rows.Count;
                return "<tr><td colspan='" + (salaryCount + 6).ToString() + "' align='center'>符合条件的数据不存在！</td></tr>"; 
            }
        }
        //获取员工社会保险
        DataTable dtEmplInsu = InsuEmployeeBus.GetInsuEmployeeInfo();
        #endregion

        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //员工信息不存在时，返回空值
        if (dtEmplInfo == null || dtEmplInfo.Rows.Count < 1) return sbSalaryInfo.ToString();
        //遍历员工信息，设置员工对应社会保险
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

            #region 设置社会保险信息
            //社会保险信息数据存在时
            if (dtInsuSocial != null && dtInsuSocial.Rows.Count > 0)
            {
                //遍历显示所有社会保险信息
                for (int j = 0; j < dtInsuSocial.Rows.Count; j++)
                {
                    //获取社会保险项ID 
                    string insuID = GetSafeData.GetStringFromInt(dtInsuSocial.Rows[j], "ID");
                    //定义变量
                    string hadSetFlag = "0";
                    //参保时间
                    string startDate = string.Empty;
                    //参保地
                    string addr = string.Empty;
                    //缴费基数
                    string baseMoney = string.Empty;
                    //是否设置
                    DataRow[] drInsu = dtEmplInsu.Select("EmployeeID = " + emplyID
                                    + " and  InsuranceID = " + insuID);
                    //已经设置时
                    if (drInsu != null && drInsu.Length > 0)
                    {
                        //参保时间
                        startDate = GetSafeData.GetStringFromDateTime(drInsu[0], "StartDate", "yyyy-MM-dd");
                        //参保地
                        addr = GetSafeData.ValidateDataRow_String(drInsu[0], "Addr");
                        //缴费基数
                        baseMoney = GetSafeData.ValidateDataRow_String(drInsu[0], "InsuranceBase");
                        //设置编辑标识
                        hadSetFlag = "1";
                    }
                    //缴费基数
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' value='" + hadSetFlag + "' />"
                                + "<input type='text' maxlength = '8' style='width:98%;' value='" + StringUtil.TrimZero(baseMoney) + "' onchange='Number_round(this,\"2\");' "
                                + " class='tdinput' id='txtInsuranceBase_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' /></td>");
                    //参保时间
                    string startDateID = "txtStartDate_" + (i + 1).ToString() + "_" + (j + 1).ToString();
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' readonly style='width:98%;' value='" + startDate + "' "
                                + " onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('" + startDateID + "')})\" "
                                + " class='tdinput' id='" + startDateID + "' /></td>");
                    //参保地
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '50' style='width:90%;' value='" + addr + "' "
                                + " class='tdinput' id='txtAddr_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "' /></td>");
                }
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
    private string CreateListTitle(DataTable dtInsuSocial)
    {
        //定义表格变量
        StringBuilder table = new StringBuilder();
        //社会保险项数据存在时
        int insuCount = 0;
        //保险设置的场合，获取保险总数
        if (dtInsuSocial != null && dtInsuSocial.Rows.Count > 0) insuCount = dtInsuSocial.Rows.Count;
        //设置保险项目总数
        table.AppendLine("<input type='hidden' id='txtInsuSocialCount' value='" + insuCount.ToString() + "' />");
        //生成表格标题
        table.AppendLine("<table  width='100%' border='0' id='tblInsuDetail'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        table.AppendLine("	<tr>");
        table.AppendLine("    <td class='ListTitle' rowspan='2' style='font-weight: bolder;'  width='7%'>员工编号</td>");
        table.AppendLine("    <td class='ListTitle' rowspan='2' style='font-weight: bolder;'>员工姓名</td>");
        table.AppendLine("    <td class='ListTitle' rowspan='2' style='font-weight: bolder;'>所在部门</td>");
        table.AppendLine("    <td class='ListTitle' rowspan='2' style='font-weight: bolder;'>所在岗位</td>");
        table.AppendLine("    <td class='ListTitle' rowspan='2' style='font-weight: bolder;'>岗位职等</td>");
        
        //变量定义
        StringBuilder sbInsuName = new StringBuilder();
        StringBuilder sbInputInsu = new StringBuilder();
        //保险数据存在时
        if (insuCount > 0)
        {
            //遍历所有社会保险项，设置到表格中
            for (int i = 0; i < insuCount; i++)
            {
                //获取社会保险项名称
                string insuName = GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "InsuranceName");
                //获取单位比例
                string compRate = GetSafeData.GetStringFromDecimal(dtInsuSocial.Rows[i], "CompanyPayRate");
                //获取个人比例
                string perRate = GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "PersonPayRate");
                //设置社会保险项名称
                sbInsuName.AppendLine("<td colspan='3' class='ListTitle' style='font-weight: bolder;' colspan='3'>"
                                + "<input type='hidden' id='txtInsuranceName_" + (i + 1).ToString() + "' value='" + insuName + "' />"
                                + "<input type='hidden' id='txtInsuranceID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "ID") + "' />"
                                + insuName + "&nbsp;(单位比例：" + StringUtil.TrimZero(compRate) + "%&nbsp;&nbsp;个人比例：" + StringUtil.TrimZero(perRate) + "%)"
                                + "</td>");
                sbInputInsu.AppendLine("<td class='ListTitle' style='font-weight: bolder;'> 缴费基数</td>");
                sbInputInsu.AppendLine("<td class='ListTitle' style='font-weight: bolder;'> 参保时间</td>");
                sbInputInsu.AppendLine("<td class='ListTitle' style='font-weight: bolder;'>参保地</td>");
            }
        }
        table.AppendLine(sbInsuName.ToString());
        table.AppendLine("</tr><tr>" + sbInputInsu.ToString() + "</tr>");
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
        bool isSucc = InsuEmployeeBus.SaveInsuEmployeeInfo(lstEdit);
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //保存成功时
        if (isSucc)
        {
            //设置记录总数
        
            if (InsuItem2Json(lstEdit) != "")
            {
                sbReturn.Append("{");
                sbReturn.Append("EditStatus:1");
                //设置数据
                sbReturn.Append(",DataInfo:");
                sbReturn.Append(InsuItem2Json(lstEdit));
                sbReturn.Append("}");
            }
            else
            {
                sbReturn.Append("{EditStatus:1}");
            }
         
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
    /// 社会保险项记录转换为Json
    /// </summary>
    /// <param name="lstEdit">社会保险项记录</param>
    /// <returns></returns>
    private string InsuItem2Json(ArrayList lstEdit)
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
                //获取社会保险项信息
                InsuEmployeeModel model = (InsuEmployeeModel)lstEdit[i];
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
                    //社会保险项列编号
                    sbSalaryInfo.Append(",\"InsuColumnNo\":\"" + model.InsuColumnNo + "\"");
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
        //获取社会保险项列表数据总数
        int insuCount = int.Parse(request.Params["InsuCount"]);

        //遍历所有员工
        for (int i = 1; i <= userCount; i++)
        {
            //获取员工ID
            string emplID = request.Params["EmployeeID_" + i.ToString()].ToString();
            //遍历所有社会保险项
            for (int j = 1; j <= insuCount; j++)
            {
                //定义Model变量
                InsuEmployeeModel model = new InsuEmployeeModel();
                //员工ID
                model.EmployeeID = emplID;
                //社会保险项ID
                model.InsuranceID = request.Params["InsuID_" + j.ToString()].ToString();
                //获取缴费基数
                string insuBase = request.Params["InsuBase_" + i.ToString() + "_" + j.ToString()].ToString();
                //获取编辑模式
                string editFlag = request.Params["EditFlag_" + i.ToString() + "_" + j.ToString()].ToString();
                //缴费基数值未输入时
                if (string.IsNullOrEmpty(insuBase))
                {
                    //如果是已设置的项，删除原来设置的值
                    if ("1".Equals(editFlag))
                    {
                        //编辑模式
                        model.EditFlag = "3";
                        //设置行号
                        model.RowNo = i.ToString();
                        //设置社会保险列编号
                        model.InsuColumnNo = j.ToString();
                    }
                    //未设置时，跳过，执行下一个社会保险项
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //缴费基数
                    model.InsuranceBase = insuBase;
                    //参保时间
                    model.StartDate = request.Params["StartDate_" + i.ToString() + "_" + j.ToString()].ToString(); ;
                    //参保地
                    model.Addr = request.Params["Addr_" + i.ToString() + "_" + j.ToString()].ToString(); ;
                    //编辑模式
                    model.EditFlag = editFlag;
                    //新建时
                    if ("0".Equals(editFlag))
                    {
                        //设置行号
                        model.RowNo = i.ToString();
                        //设置社会保险列编号
                        model.InsuColumnNo = j.ToString();
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