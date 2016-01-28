<%@ WebHandler Language="C#" Class="InputFloatSalary" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/13
 * 描    述： 浮动工资录入
 * 修改日期： 2009/05/13
 * 版    本： 0.5.0
 ***********************************************/
using System.Linq;
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
using System.Xml.Linq;
using System.Web.Script.Serialization;

public class InputFloatSalary : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        else if ("SearchRoyaltyItemInfo".Equals(action))
        {
            //执行查询
            SearchRoyaltyItemInfo(context);
        }
        else if ("SearchPieceItemInfo".Equals(action))
        {
            //执行查询
            SearchPieceItemInfo(context);
        }
        else if ("SearchTimeItemInfo".Equals(action))
        {
            //执行查询
            SearchTimeItemInfo(context);
        }
        else if ("Update".Equals(action))
        {
            //列表保存浮动工资信息
            UpdateSalaryInfo(context);
        }
        else if ("New".Equals(action))
        {
            //保存浮动工资信息
            NewSalaryInfo(context);
        }
        else if ("Price".Equals(action))
        {
            //获取标准价格
            GetUnitPrice(context);
        }
        else if ("DeleteInfo".Equals(action))
        {
            //获取要素ID
            string elemID = context.Request.QueryString["DeleteNO"];
            string flag = context.Request.QueryString["flag"];
            //替换引号
           // elemID = elemID.Replace("'", "");

            string[] elemList = elemID.Split(',');
            //判断要素是否被使用
            //bool isUsed = PerformanceTypeBus.IsTemplateUsed(elemID);
            ////已经被使用
            //if (isUsed)
            //{
            //    //输出响应 返回不执行删除
            //    context.Response.Write(new JsonClass("", "", 2));
            //}
            //else
            //{
            bool sign = true;
            foreach (string no in elemList)
            {
                //删除要素
                bool isSucc =false ;
                    
                    if (flag =="1")
                    {
                        isSucc = PieceworkItemBus.DeletePerTypeInfo(no);
                    }
                    else if (flag == "2")
                    {
                        isSucc = TimeSalaryBus.DeletePerTypeInfo(no);
                    }
                    else if (flag == "3")
                    {
                        isSucc = CommissionSalaryBus.DeletePerTypeInfo(no);
                    }
                
                //删除成功
                if (isSucc)
                {
                    //输出响应
                    //context.Response.Write(new JsonClass("", "", 1));
                    continue;
                }
                //删除失败
                else
                {
                    sign = false;
                    break ;
                    //输出响应
                   // context.Response.Write(new JsonClass("", "", 0));
                }
            }
            if (!sign)
            {
                context.Response.Write(new JsonClass("", "", 0));
            }
            else
            {
                context.Response.Write(new JsonClass("", "", 1));
            }
            //}
        }
    }

    private void SearchRoyaltyItemInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ItemNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据 
        //查询数据
        DataTable dtData = CommissionItemBus.GetCommissionItemInfo(true);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("ItemNo").Value,//ID
                 Title = x.Element("ItemName").Value//类型名称



             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("ItemNo").Value,//ID
                 Title = x.Element("ItemName").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchTimeItemInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "TimeNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据 
        //查询数据
        DataTable dtData = TimeItemBus.GetTimeItemInfo(true);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("TimeNo").Value,//ID
                 Title = x.Element("TimeName").Value//类型名称



             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("TimeNo").Value,//ID
                 Title = x.Element("TimeName").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    private void SearchPieceItemInfo(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ItemNo";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据 
        //查询数据
        DataTable dtData = PieceworkItemBus.GetPieceworkItemInfo(true);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("ItemNo").Value,//ID
                 Title = x.Element("ItemName").Value//类型名称



             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceTemplateModel()
             {
                 TemplateNo = x.Element("ItemNo").Value,//ID
                 Title = x.Element("ItemName").Value//类型名称
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
    /// <summary>
    /// 获取标准价格
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void GetUnitPrice(HttpContext context)
    {
        //获取操作的工资项
        string flag = context.Request.Params["Flag"].ToString();
        //获取工资项编号
        string itemNo = context.Request.Params["ItemNo"].ToString();
        string price = "0";
        //计件工资
        if ("1".Equals(flag))
        {
            //获取单价
            DataTable dtPrice = PieceworkItemBus.GetPieceworkPrice(itemNo);
            //工资项存在时
            if (dtPrice != null && dtPrice.Rows.Count > 0)
            {
                //获取提成率
                price = GetSafeData.GetStringFromDecimal(dtPrice.Rows[0], "UnitPrice");
            }
        }
        else if ("2".Equals(flag))
        {
            //获取单价
            DataTable dtPrice = TimeItemBus.GetTimeItemPrice(itemNo);
            //工资项存在时
            if (dtPrice != null && dtPrice.Rows.Count > 0)
            {
                //获取提成率
                price = GetSafeData.GetStringFromDecimal(dtPrice.Rows[0], "UnitPrice");
            }
        }
        else if ("3".Equals(flag))
        {
            //获取提成率
            DataTable dtRate = CommissionItemBus.GetCommissionRate(itemNo);
            //工资项存在时
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                //获取提成率
                price = GetSafeData.GetStringFromDecimal(dtRate.Rows[0], "Rate");
            }
        }
        price = StringUtil.TrimZero(price);
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(price);
        context.Response.End();
    }
    
    /// <summary>
    /// 新建保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void NewSalaryInfo(HttpContext context)
    {
        //编辑请求信息
        ArrayList lstEdit = new ArrayList();

        //更新成功与否标识
        bool isSucc = false;
        //标识
        string flag = context.Request.Params["Flag"].ToString();
        //编辑模式
        string editFlag = context.Request.Params["EditFlag"].ToString();
        //员工ID
        string emplID = context.Request.Params["EmplID"].ToString();
        //日期
        string inputDate = context.Request.Params["InputDate"].ToString();
        //数量
        string amount = context.Request.Params["Amount"].ToString();
        //小计金额
        string totalMoney = context.Request.Params["TotalMoney"].ToString();
        
        //
        //计件工资
        if ("1".Equals(flag))
        {
            PieceworkSalaryModel model = new PieceworkSalaryModel();
            //员工ID
            model.EmployeeID = emplID;
            //工资项编号
            model.ItemNo = context.Request.Params["PieceNo"].ToString();
            //数量
            model.Amount = amount;
            //金额
            model.SalaryMoney = totalMoney;
            //日期
            model.PieceDate = inputDate;
            //编辑模式
            model.EditFlag = editFlag;
            //添加
            lstEdit.Add(model);
            //执行保存
            isSucc = PieceworkSalaryBus.SavePieceworkSalaryInfo(lstEdit);
        }
        //计时工资
        else if ("2".Equals(flag))
        {
            //变量定义
            TimeSalaryModel model = new TimeSalaryModel();
            //员工ID
            model.EmployeeID = emplID;
            //工资项编号
            model.TimeNo = context.Request.Params["TimeNo"].ToString();
            //数量
            model.TimeCount = amount;
            //金额
            model.SalaryMoney = totalMoney;
            //日期
            model.TimeDate = inputDate;
            //编辑模式
            model.EditFlag = editFlag;
            //添加
            lstEdit.Add(model);
            //执行保存
            isSucc = TimeSalaryBus.SaveTimeSalaryInfo(lstEdit);
        }
        //提成工资
        else if ("3".Equals(flag))
        {
            CommissionSalaryModel model = new CommissionSalaryModel();
            //员工ID
            model.EmployeeID = emplID;
            //工资项编号
            model.ItemNo = context.Request.Params["CommissionNo"].ToString();
            //数量
            model.Amount = amount;
            //金额
            model.SalaryMoney = totalMoney;
            //日期
            model.CommDate = inputDate;
            //编辑模式
            model.EditFlag = editFlag;
            //添加
            lstEdit.Add(model);
            //执行保存
            isSucc = CommissionSalaryBus.SaveCommissionSalaryInfo(lstEdit);
        }
        //定义Json返回变量
        JsonClass jc;
        //保存成功时
        if (isSucc)
        {
            jc = new JsonClass("", "", 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }
        //输出响应
        context.Response.Write(jc);
    }

    /// <summary>
    /// 列表保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void UpdateSalaryInfo(HttpContext context)
    {
        //编辑请求信息
        ArrayList lstEdit = EditRequstData(context.Request);

        //更新成功与否标识
        bool isSucc = false;
        //标识
        string flag = context.Request.Params["Flag"].ToString();
        //计件工资
        if ("1".Equals(flag))
        {
            //执行保存
            isSucc = PieceworkSalaryBus.SavePieceworkSalaryInfo(lstEdit);
        }
        //计时工资
        else if ("2".Equals(flag))
        {
            //执行保存
            isSucc = TimeSalaryBus.SaveTimeSalaryInfo(lstEdit);
        }
        //提成工资
        else if ("3".Equals(flag))
        {
            //执行保存
            isSucc = CommissionSalaryBus.SaveCommissionSalaryInfo(lstEdit);
        }
        //定义Json返回变量
        JsonClass jc;
        //保存成功时
        if (isSucc)
        {
            jc = new JsonClass("", "", 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }
        //输出响应
        context.Response.Write(jc);
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
        //标识
        string flag = request.Params["Flag"].ToString();
        //遍历所有员工
        for (int i = 1; i <= userCount; i++)
        {
            //获取员工ID
            string emplID = request.Params["EmplID_" + i.ToString()].ToString();
            //获取社会保险项列表数据总数
            int itemCount = int.Parse(request.Params["EmplItemCount_" + i.ToString()]);
            //遍历所有项目
            for (int j = 1; j <= itemCount; j++)
            {
                //获取工资项编号
                string salaryNo = request.Params["SalaryNo_" + i.ToString() + "_" + j.ToString()].ToString();
                //获取社会保险项列表数据总数
                int itemSalaryCount = int.Parse(request.Params["ItemSalaryCount_" + i.ToString() + "_" + j.ToString()]);
                //遍历所有的具体工资项
                for (int x = 1; x <= itemSalaryCount; x++)
                {
                    //获取数量
                    string salaryAmount = request.Params["SalaryAmount_" + i.ToString() + "_" + j.ToString() + "_" + x.ToString()].ToString();
                    //获取金额
                    string amountMoney = request.Params["AmountMoney_" + i.ToString() + "_" + j.ToString() + "_" + x.ToString()].ToString();
                    //获取日期
                    string salaryDate = request.Params["SalaryDate_" + i.ToString() + "_" + j.ToString() + "_" + x.ToString()].ToString();
                    //计件
                    if ("1".Equals(flag))
                    {
                        PieceworkSalaryModel model = new PieceworkSalaryModel();
                        //员工ID
                        model.EmployeeID = emplID;
                        //工资项编号
                        model.ItemNo = salaryNo;
                        //数量
                        model.Amount = salaryAmount;
                        //金额
                        model.SalaryMoney = amountMoney;
                        //日期
                        model.PieceDate = salaryDate;
                        //编辑模式
                        model.EditFlag = "1";

                        lstReturn.Add(model);
                    }
                    //计时
                    else if ("2".Equals(flag))
                    {
                        //变量定义
                        TimeSalaryModel model = new TimeSalaryModel();
                        //员工ID
                        model.EmployeeID = emplID;
                        //工资项编号
                        model.TimeNo = salaryNo;
                        //数量
                        model.TimeCount = salaryAmount;
                        //金额
                        model.SalaryMoney = amountMoney;
                        //日期
                        model.TimeDate = salaryDate;
                        //编辑模式
                        model.EditFlag = "1";

                        lstReturn.Add(model);
                    }
                    //提成
                    else if ("3".Equals(flag))
                    {
                        CommissionSalaryModel model = new CommissionSalaryModel();
                        //员工ID
                        model.EmployeeID = emplID;
                        //工资项编号
                        model.ItemNo = salaryNo;
                        //数量
                        model.Amount = salaryAmount;
                        //金额
                        model.SalaryMoney = amountMoney;
                        //日期
                        model.CommDate = salaryDate;
                        //编辑模式
                        model.EditFlag = "1";

                        lstReturn.Add(model);
                    }
                }
            }
        }

        return lstReturn;
    }
    
    /// <summary>
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void SearchSalaryInfo(HttpContext context)
    {
        //获取操作的工资项
        string flag = context.Request.Params["Flag"].ToString();
        //设置参数
        EmployeeSearchModel model = new EmployeeSearchModel();
        //员工编号
        model.EmployeeNo = context.Request.Params["EmployeeNo"].ToString();
        //员工姓名
        model.EmployeeName = context.Request.Params["EmployeeName"].ToString();
        //所在岗位
        model.QuarterID = context.Request.Params["QuarterID"].ToString();
        //开始时间
        model.StartDate = context.Request.Params["StartDate"].ToString();
        //结束时间
        model.EndDate = context.Request.Params["EndDate"].ToString();
        //计件项目
        string pieceworkNo = context.Request.Params["PieceworkNo"].ToString();
        //计时项目
        string timeNo = context.Request.Params["TimeNo"].ToString();
        //提成项目
        string commissionNo = context.Request.Params["CommissionNo"].ToString();
        //变量定义
        string tableDetail = string.Empty;
        //计件工资
        if ("1".Equals(flag))
        {
            //获取计件工资详细
            tableDetail = PieceworkSalaryBus.GetPieceworkSalaryInfo(model, pieceworkNo);
        }
        else if ("2".Equals(flag))
        {
            //获取计时工资详细
            tableDetail = TimeSalaryBus.GetTimeSalaryInfo(model, timeNo);
        }
        else if ("3".Equals(flag))
        {
            //获取提成工资详细
            tableDetail = CommissionSalaryBus.GetCommissionSalaryInfo(model, commissionNo);
        }
        //设置表格标题
        string tableTitle = CreateTableTitle(flag);
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        string strResult = tableTitle + tableDetail + "</table>"
                            + "<input type='hidden' id='txtUserCount' value='" + model.RecordCount + "' />";
        context.Response.Write(strResult);
        context.Response.End();
    }
    
    /// <summary>
    /// 生成浮动工资表格的标题部分
    /// </summary>
    /// <param name="flag">工资项标识</param>
    /// <returns></returns>
    private string CreateTableTitle(string flag)
    {
        //变量定义
        StringBuilder sbTitle = new StringBuilder();
        //生成表格标题
        sbTitle.AppendLine("<table  width='100%' border='0' id='tblInsuDetail'  align='center' cellpadding='0' cellspacing='1' bgcolor='#999999'>");
        sbTitle.AppendLine("	<tr>"); 
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>选择<input type=\"checkbox\" id=\"chkCheckAll\" name=\"chkCheckAll\" onclick=\"AllSelect('chkCheckAll', 'chkSelect')\"></td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>员工编号</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>员工姓名</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>所在部门</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>所在岗位</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>岗位职等</td>");

        //计件工资
        if ("1".Equals(flag))
        {
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>计件项目</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>单价</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'> <span style=\"color:Red\">*</span>数量</td>");
        }
        //计时工资
        else if ("2".Equals(flag))
        {
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>计时项目</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>单价</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'><span style=\"color:Red\">*</span>时长(小时)</td>");
        }
        //提成工资
        else if ("3".Equals(flag))
        {
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>提成项目</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>提成率</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'><span style=\"color:Red\">*</span>业务量</td>");
            sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>来源</td>");
        }

        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>金额</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>日期</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>小计</td>");
        sbTitle.AppendLine("    <td class='ListTitle' style='font-weight: bolder;'>合计</td>");
        sbTitle.AppendLine("</tr>");
        //返回
        return sbTitle.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}