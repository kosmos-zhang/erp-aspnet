<%@ WebHandler Language="C#" Class="InputPersonalRoyalty" %>

using System;
using System.Data;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;
using System.Collections.Generic;

public class InputPersonalRoyalty : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        else if ("Save".Equals(action))
        {
            //保存社会保险信息
            SaveSalaryInfo(context);
        }
        else if ("getinfo".Equals(action))
        {
            GetSellOrderList(context);
        }
        else if ("SynchronizerSell".Equals(action))
        {
            SynchronizerSellOrder(context);
        }
        else if ("SynchronizerRate".Equals(action))
        {
            SynchronizerRate(context);
        }
    }
    /// <summary>
    /// .订单列表
    /// </summary>
    /// <param name="context"></param>
    private void GetSellOrderList(HttpContext context)
    {
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        string OrderNo = context.Request.Form["OrderNo"].ToString();
        string SellName = context.Request.Form["SellerName"].ToString();
        string StartDate = context.Request.Form["OpenDate"].ToString();
        string EndDate = context.Request.Form["CloseDate"].ToString();
        DataTable dt = InputPersonalRoyaltyBus.SearchPersonTaxInfo(OrderNo, SellName, StartDate, EndDate, pageIndex, pageCount, ord, ref totalCount);


        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    private void SynchronizerRate(HttpContext context)
    {

        string ID = context.Request.Params["str"].ToString().Trim();
        string RowNum = context.Request.Params["RowNum"].ToString().Trim();//序号
        string[] RowNumS = null;
        RowNumS = RowNum.Split(',');
        string NewCustomerTax = "";
        string NewRowNum = "";
        int j = 0;
        DataTable dt = InputPersonalRoyaltyBus.GetSynchronizerRate(ID);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (DataRow row in dt.Rows)
        {
            j++;
            bool EmpID = InputPersonalRoyaltyBus.ChargeEmp(row["EmployeeID"].ToString());//判断提成设置表有没有Empid
            bool CustID = InputPersonalRoyaltyBus.ChargeCust(row["CustID"].ToString());//判断提成设置表有没有CustID
            if (EmpID && CustID)
            {
                bool OldOrNew = InputPersonalRoyaltyBus.ChargeNewOrOld(row["CustID"].ToString(), row["CreateTime"].ToString());//判断是否是老客户
                DataTable dt1 = InputPersonalRoyaltyBus.GetSellOrderSynchronizerDetail(row["CustID"].ToString(), row["EmployeeID"].ToString());//取出上下限跟row["AfterTaxMoney"]比较
                if (dt1.Rows.Count == 0)
                {
                    dt1 = InputPersonalRoyaltyBus.GetSellOrderSynchronizerDetail("0", row["EmployeeID"].ToString());
                }
                int i = 0;
                foreach (DataRow row1 in dt1.Rows)
                {
                    //if(Convert.ToDecimal( row["AfterTaxMoney"].ToString())>)
                    if (Convert.ToDecimal(row["AfterTaxMoney"].ToString()) >= Convert.ToDecimal(row1["MiniMoney"].ToString()) && Convert.ToDecimal(row["AfterTaxMoney"].ToString()) <= Convert.ToDecimal(row1["MaxMoney"].ToString()))
                    {
                        if (string.IsNullOrEmpty(row1["TaxPercent"].ToString()))
                        {
                            if (OldOrNew)
                            {
                                NewCustomerTax += row1["OldCustomerTax"].ToString() + ",";//提成率是老客户
                                i++;
                            }
                            else
                            {
                                NewCustomerTax += row1["NewCustomerTax"].ToString() + ",";//提成率是新客户
                                i++;
                            }
                        }
                        else
                        {
                            NewCustomerTax += row1["TaxPercent"].ToString() + ",";
                            i++;
                        }

                    }
                }
                if (i == 0)
                {
                    //context.Response.Write(new JsonClass("您没有设置第" + int.Parse(RowNumS[j-1].ToString())+1 + "行含税金额合计是" + row["AfterTaxMoney"].ToString() + "的区间", j.ToString(), 2));
                    //return;
                    NewRowNum += RowNumS[j - 1].ToString() + ",";

                }
            }
            else if (EmpID && !CustID)//个人提成设置有员工没有客户
            {
                bool OldOrNew = InputPersonalRoyaltyBus.ChargeNewOrOld(row["CustID"].ToString(), row["CreateTime"].ToString());//判断是否是老客户
                DataTable dt1 = InputPersonalRoyaltyBus.GetSellOrderSynchronizerDetail("0", row["EmployeeID"].ToString());//取出上下限跟row["AfterTaxMoney"]比较
                int i = 0;
                foreach (DataRow row1 in dt1.Rows)
                {
                    //if(Convert.ToDecimal( row["AfterTaxMoney"].ToString())>)
                    if (Convert.ToDecimal(row["AfterTaxMoney"].ToString()) >= Convert.ToDecimal(row1["MiniMoney"].ToString()) && Convert.ToDecimal(row["AfterTaxMoney"].ToString()) <= Convert.ToDecimal(row1["MaxMoney"].ToString()))
                    {
                        if (string.IsNullOrEmpty(row1["TaxPercent"].ToString()))
                        {
                            if (OldOrNew)
                            {
                                NewCustomerTax += row1["OldCustomerTax"].ToString() + ",";//提成率是老客户
                                i++;
                            }
                            else
                            {
                                NewCustomerTax += row1["NewCustomerTax"].ToString() + ",";//提成率是新客户
                                i++;
                            }
                        }
                        else
                        {
                            NewCustomerTax += row1["TaxPercent"].ToString() + ",";
                            i++;
                        }

                    }
                }
                if (i == 0)
                {
                    //context.Response.Write(new JsonClass("您没有设置第" + int.Parse(RowNumS[j - 1].ToString()) + 1 + "行含税金额合计是" + row["AfterTaxMoney"].ToString() + "的区间", j.ToString(), 2));
                    //return;
                    NewRowNum += RowNumS[j - 1].ToString() + ",";
                }
            }
            else if (!EmpID && CustID)   //个人提成设置没有员工但有客户
            {
                bool OldOrNew = InputPersonalRoyaltyBus.ChargeNewOrOld(row["CustID"].ToString(), row["CreateTime"].ToString());//判断是否是老客户
                DataTable dt1 = InputPersonalRoyaltyBus.GetSellOrderSynchronizerDetail(row["CustID"].ToString(), "0");//取出上下限跟row["AfterTaxMoney"]比较
                int i = 0;
                foreach (DataRow row1 in dt1.Rows)
                {
                    //if(Convert.ToDecimal( row["AfterTaxMoney"].ToString())>)
                    if (Convert.ToDecimal(row["AfterTaxMoney"].ToString()) >= Convert.ToDecimal(row1["MiniMoney"].ToString()) && Convert.ToDecimal(row["AfterTaxMoney"].ToString()) <= Convert.ToDecimal(row1["MaxMoney"].ToString()))
                    {
                        if (string.IsNullOrEmpty(row1["TaxPercent"].ToString()))
                        {
                            if (OldOrNew)
                            {
                                NewCustomerTax += row1["OldCustomerTax"].ToString() + ",";//提成率是老客户
                                i++;
                            }
                            else
                            {
                                NewCustomerTax += row1["NewCustomerTax"].ToString() + ",";//提成率是新客户
                                i++;
                            }
                        }
                        else
                        {
                            NewCustomerTax += row1["TaxPercent"].ToString() + ",";
                            i++;
                        }

                    }
                }
                if (i == 0)
                {
                    //context.Response.Write(new JsonClass("您没有设置第" + int.Parse(RowNumS[j - 1].ToString()) + 1 + "行含税金额合计是" + row["AfterTaxMoney"].ToString() + "的区间", j.ToString(), 2));
                    //return;
                    NewRowNum += RowNumS[j - 1].ToString() + ",";
                }
            }
            else if (!EmpID && !CustID)   //个人提成设置没有员工也没有客户
            {
                bool OldOrNew = InputPersonalRoyaltyBus.ChargeNewOrOld(row["CustID"].ToString(), row["CreateTime"].ToString());//判断是否是老客户
                DataTable dt1 = InputPersonalRoyaltyBus.GetSellOrderSynchronizerDetail("0", "0");//取出上下限跟row["AfterTaxMoney"]比较
                int i = 0;
                foreach (DataRow row1 in dt1.Rows)
                {
                    //if(Convert.ToDecimal( row["AfterTaxMoney"].ToString())>)
                    if (Convert.ToDecimal(row["AfterTaxMoney"].ToString()) >= Convert.ToDecimal(row1["MiniMoney"].ToString()) && Convert.ToDecimal(row["AfterTaxMoney"].ToString()) <= Convert.ToDecimal(row1["MaxMoney"].ToString()))
                    {
                        if (string.IsNullOrEmpty(row1["TaxPercent"].ToString()))
                        {
                            if (OldOrNew)
                            {
                                NewCustomerTax += row1["OldCustomerTax"].ToString() + ",";//提成率是老客户
                                i++;
                            }
                            else
                            {
                                NewCustomerTax += row1["NewCustomerTax"].ToString() + ",";//提成率是新客户
                                i++;
                            }
                        }
                        else
                        {
                            NewCustomerTax += row1["TaxPercent"].ToString() + ",";
                            i++;
                        }

                    }
                }
                if (i == 0)
                {
                    //context.Response.Write(new JsonClass("您没有设置第" + int.Parse(RowNumS[j - 1].ToString()) + 1 + "行含税金额合计是" + row["AfterTaxMoney"].ToString() + "的区间", j.ToString(), 2));
                    //return;
                    NewRowNum += RowNumS[j - 1].ToString() + ",";
                }
            }

        }
        if (!string.IsNullOrEmpty(NewRowNum))
        {
            context.Response.Write(new JsonClass("", NewRowNum, 2));

        }
        else
        {
            if (InputPersonalRoyaltyBus.UpdateRate(ID, NewCustomerTax))
            {
                context.Response.Write(new JsonClass("同步提成率成功！", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("同步提成率失败！", "", 1));
            }
        }

        //string Edit_ID = IdS[j - 1].ToString();
        //string Edit_Rate = Rate[j - 1].ToString();
        //for (int i = 0; i < IdS.Length - 3; i++)
        //{
        //    if(string Edit_ID) 
        //}
        //foreach (string Edit_ID in IdS)
        //{
        //    j++;
        //    if (string.IsNullOrEmpty(Rate[j - 1]))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        if (InputPersonalRoyaltyBus.UpdateRate(ID,NewCustomerTax))
        //        {
        //            context.Response.Write(new JsonClass("同步提成率成功！", "", 1));
        //        }
        //        else
        //        {
        //            context.Response.Write(new JsonClass("同步提成率失败！", "", 1)); 
        //        }
        //    }
        //}


        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("{");
        //sb.Append("totalCount:");
        //sb.Append(totalCount.ToString());
        //sb.Append(",data:");
        //if (dt.Rows.Count == 0)
        //    sb.Append("[{\"ID\":\"\"}]");
        //else
        //    sb.Append(JsonClass.DataTable2Json(dt));
        //sb.Append("}");

        //context.Response.ContentType = "text/plain";
        //context.Response.Write(sb.ToString());
        //context.Response.End();
    }

    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SearchSalaryInfo(HttpContext context)
    {
        //获取社会保险项
        //设置参数
        EmployeeSearchModel model = new EmployeeSearchModel();
        //员工编号
        model.EmployeeNo = context.Request.Params["EmployeeNo"].ToString();
        //员工姓名
        model.EmployeeName = context.Request.Params["EmployeeName"].ToString();
        //所在岗位
        string OpenDate = context.Request.Params["OpenDate"].ToString();
        string CloseDate = context.Request.Params["CloseDate"].ToString();
        //获取员工信息
        DataTable dtEmplInfo = InputPersonalRoyaltyBus.SearchInputPersonalRoyalty(model.EmployeeNo, model.EmployeeName, OpenDate, CloseDate);
        DataTable dtNew = SalaryItemBus.GetOutEmployeeInfo(model);
        dtEmplInfo.Merge(dtNew);
        //获取人员总数
        int userCount = 0;
        //获取员工数
        if (dtEmplInfo != null && dtEmplInfo.Rows.Count > 0) userCount = dtEmplInfo.Rows.Count;
        //生成表格
        string salaraInfo = CreateListTitle(dtEmplInfo)
                            + InitInsuDetailInfo(dtEmplInfo, userCount) + "</table>";
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
    private string InitInsuDetailInfo(DataTable dtInsuSocial, int userCount)
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
        //获取员工社会保险
        //DataTable dtEmplInsu = InputPersonalRoyaltyBus.SearchPersonTaxInfo("","","","");
        //DataTable dtEmplInsu = null;
        #endregion

        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //员工信息不存在时，返回空值
        if (dtInsuSocial == null || dtInsuSocial.Rows.Count < 1) return sbSalaryInfo.ToString();
        //遍历员工信息，设置员工对应社会保险
        for (int i = 0; i < userCount; i++)
        {
            //行背景色定义
            string backColor = i % 2 == 0 ? "#FFFFFF" : "#E7E7E7";
            //插入行开始标识
            sbSalaryInfo.AppendLine("<tr style='background-color:" + backColor + ";' onmouseover='this.style.backgroundColor=\"#cfc\";'"
                                            + " onmouseout='this.style.backgroundColor=\"" + backColor + "\";'>");

            #region 设置员工信息
            sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                             + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                             + GetSafeData.GetStringFromInt(dtInsuSocial.Rows[i], "ID")
                             + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                             + "<input type='checkbox' name='chkSelect' value='"
                             + GetSafeData.GetStringFromInt(dtInsuSocial.Rows[i], "ID")
                             + "'  /></td>");
            //员工姓名
            sbSalaryInfo.AppendLine("<td align='center' id='tdEmployeeName_" + (i + 1).ToString() + "'>"
                        + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "EmployeeName") + "</td>");
            //订单编号
            sbSalaryInfo.AppendLine("<td align='center' title=\"" + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "FromBillNo") + "\">"
                        + fnjiequ(GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "FromBillNo"), 15) + "</td>");
            //币种
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "CurrencyName") + "</td>");
            //汇率
            sbSalaryInfo.AppendLine("<td align='center'>"
                      + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "Rate") + "</td>");
            //含税金额合计
            sbSalaryInfo.AppendLine("<td align='center'>"
                        + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "AfterTaxMoney") + "</td>");

            sbSalaryInfo.AppendLine("<td align='center'>"
                       + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "CustName") + "</td>");


            sbSalaryInfo.AppendLine("<td align='center'>"
                       + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "NewCustomerTax") + "</td>");

            sbSalaryInfo.AppendLine("<td align='center'>"
                      + GetSafeData.ValidateDataRow_String(dtInsuSocial.Rows[i], "CreateTime") + "</td>");

            #endregion

            //插入行结束标识
            sbSalaryInfo.AppendLine("</tr>");
        }

        //返回信息
        return sbSalaryInfo.ToString();
    }
    private string fnjiequ(string str, int strlen)
    {
        return str.Length > strlen + 3 ? str.Substring(0, strlen) + "…" : str;
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
        table.AppendLine("	<tr>                                                                  ");
        table.AppendLine("		<td class='ListTitle' style='width:5%'>                                 ");
        table.AppendLine("		   选择<input type='checkbox' id='chkAll' onclick='SelectAll();'/>");
        table.AppendLine("		</td>                                                             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>员工姓名</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>订单编号</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'>币种</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:10%'>汇率</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:12%'>含税金额合计</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:13%'>客户名称</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'>提成率</td>             ");
        table.AppendLine("		<td class='ListTitle' style='width:8%'>订单日期</td>             ");

        table.AppendLine("	</tr>                                                                 ");
        //返回表格语句
        return table.ToString();
    }

    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SaveSalaryInfo(HttpContext context)
    {
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string temp = context.Request.QueryString["TaxInfo"];
        string[] allTaxInfo = temp.Split(',');
        IList<PersonTrueIncomeTaxModel> modelList = new List<PersonTrueIncomeTaxModel>();
        //getInfo.push(EmplID, SalaryCount, TaxPercent, TaxCount, StartDate);
        for (int i = 0; i < allTaxInfo.Length - 3; i++)
        {
            PersonTrueIncomeTaxModel model = new PersonTrueIncomeTaxModel();
            model.CompanyCD = userInfo.CompanyCD;
            model.EmployeeID = allTaxInfo[i];
            model.ModifiedUserID = userInfo.EmployeeID.ToString();
            model.SalaryCount = allTaxInfo[i + 1];
            model.TaxPercent = allTaxInfo[i + 2];
            model.TaxCount = allTaxInfo[i + 3];
            model.StartDate = allTaxInfo[i + 4];
            i = i + 4;
            modelList.Add(model);


        }


        if (InputPersonalRoyaltyBus.SaveInsuPersonInfo(modelList))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }
        ////设置输出流的 HTTP MIME 类型
        //context.Response.ContentType = "text/plain";
        ////向响应中输出数据
        //context.Response.Write(sbReturn.ToString());
        //context.Response.End();
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


    private void SynchronizerSellOrder(HttpContext context)
    {
        string OrderNO = context.Request.QueryString["str"].Trim();
        bool flag = InputPersonalRoyaltyBus.ChargeHava();
        if (flag)
        {
            DataTable dt = InputPersonalRoyaltyBus.GetSellOrderSynchronizer(OrderNO);
            DataTable dt_SellDetail = InputPersonalRoyaltyBus.GetSellDetail(OrderNO);
            if (InputPersonalRoyaltyBus.UpdateIsuPersonalTaxInfo(dt))
            {
                InputPersonalRoyaltyBus.UpdateSellDetailInfo(dt_SellDetail);
                context.Response.Write(new JsonClass("同步销售订单成功！", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("同步销售订单失败", "", 0));
            }
        }
        else
        {
            DataTable dt_SellDetailProdNo = InputPersonalRoyaltyBus.GetSellDetailProdNo(OrderNO);
            DataTable dt_CommissionItemProdNo = InputPersonalRoyaltyBus.GetCommissionItemProdNo();
            foreach (DataRow row in dt_SellDetailProdNo.Rows)
            {
                bool temp = false;
                foreach (DataRow row1 in dt_CommissionItemProdNo.Rows)
                {
                    if (row1["ItemNo"].ToString() == row["ProdNo"].ToString())
                    {
                        temp = true;
                    }
                }
                if (!temp)
                {
                    context.Response.Write(new JsonClass("请在产品单品提成里设置默认的规则设置！", "", 0));
                    return ;
                }
                else {
                    continue;
                }
                
            }
            DataTable dt = InputPersonalRoyaltyBus.GetSellOrderSynchronizer(OrderNO);
            DataTable dt_SellDetail = InputPersonalRoyaltyBus.GetNotSetSellDetail(OrderNO);
            if (InputPersonalRoyaltyBus.UpdateIsuPersonalTaxInfo(dt))
            {
                InputPersonalRoyaltyBus.UpdateSellDetailInfo(dt_SellDetail);
                context.Response.Write(new JsonClass("同步销售订单成功！", "", 1));
            }
            else
            {
                context.Response.Write(new JsonClass("同步销售订单失败", "", 0));
            }
        }
     
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}