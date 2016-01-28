<%@ WebHandler Language="C#" Class="SalaryStandard" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/07
 * 描    述： 工资标准设置
 * 修改日期： 2009/05/07
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using System.Data;
using XBase.Business.Common;
using System.Xml.Linq;
using System.Linq;

public class SalaryStandard : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取操作类型
        string action = context.Request.QueryString["Action"];
        //查询
        if ("Search".Equals(action))
        {
            DoSearch(context);
        }
        //保存
        else if ("Save".Equals(action))
        {
            DoSave(context);
        }
        //获取申请单对应的员工信息
        else if ("DeleteInfo".Equals(action))
        {
            DoDelete(context);
        }
        else if ("SearchDetailsInfo".Equals(action))
        {
            SearchDetailsInfo(context);
        }
    }

    /// <summary>
    /// 查询操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoSearch(HttpContext context)
    {
        HttpRequest request = context.Request;
        //从请求中获取排序列
        string orderString = request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        SalaryStandardModel searchModel = new SalaryStandardModel();
        //设置查询条件
        //岗位
        searchModel.QuarterID = request.QueryString["QuarterID"];
        //岗位职等
        searchModel.AdminLevel = request.QueryString["AdminLevel"];
        //启用状态
        searchModel.UsedStatus = request.QueryString["UsedStatus"];

        //查询数据
        DataTable dtNotify = SalaryStandardBus. SearchSalaryStandardInfo(searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtNotify, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new SalaryStandardModel()
             {
                 ID = x.Element("ID").Value,//ID
                 QuarterID = x.Element("QuarterID").Value,//岗位ID
                 QuarterName = x.Element("QuarterName").Value,//岗位名称
                 AdminLevel = x.Element("AdminLevel").Value,//岗位职等ID
                 AdminLevelName = x.Element("AdminLevelName").Value,//岗位职等名称
                 ItemNo = x.Element("ItemNo").Value,//工资项目编号
                 ItemName = x.Element("ItemName").Value,//工资项目名称
                 UnitPrice = x.Element("UnitPrice").Value,//金额
                 UsedStatus = x.Element("UsedStatus").Value,//启用状态
                 UsedStatusName = x.Element("UsedStatusName").Value,//启用状态名称
                 Remark = x.Element("Remark").Value//备注
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new SalaryStandardModel()
             {
                 ID = x.Element("ID").Value,//ID
                 QuarterID = x.Element("QuarterID").Value,//岗位ID
                 QuarterName = x.Element("QuarterName").Value,//岗位名称
                 AdminLevel = x.Element("AdminLevel").Value,//岗位职等ID
                 AdminLevelName = x.Element("AdminLevelName").Value,//岗位职等名称
                 ItemNo = x.Element("ItemNo").Value,//工资项目编号
                 ItemName = x.Element("ItemName").Value,//工资项目名称
                 UnitPrice = x.Element("UnitPrice").Value,//金额
                 UsedStatus = x.Element("UsedStatus").Value,//启用状态
                 UsedStatusName = x.Element("UsedStatusName").Value,//启用状态名称
                 Remark = x.Element("Remark").Value//备注
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
    /// 保存操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoSave(HttpContext context)
    {
        //定义Model变量
        SalaryStandardModel model = new SalaryStandardModel();
        //定义Json返回变量
        JsonClass jc;
        //ID 
        model.ID = context.Request.QueryString["ID"];
        //岗位
        model.QuarterID = context.Request.QueryString["QuarterID"];
        //岗位职等
        model.AdminLevel = context.Request.QueryString["AdminLevel"];
        //工资项
        model.ItemNo = context.Request.QueryString["ItemNo"];
        //校验是否已存在
        if (string.IsNullOrEmpty(model.ID))
        {
            //校验是否已经设置了该工资
            bool isExsi = SalaryStandardBus.CheckSalaryStandardInfo(model);

            //已经存在时
            if (isExsi)
            {
                jc = new JsonClass("", "", 2);
                //输出响应
                context.Response.Write(jc);
                return;
            }
        }
        
        //金额
        model.UnitPrice = context.Request.QueryString["UnitPrice"];
        //启用状态
        model.UsedStatus = context.Request.QueryString["UsedStatus"];
        //备注 
        model.Remark = context.Request.QueryString["Remark"];
        //执行保存操作
        bool isSucce = SalaryStandardBus.SaveSalaryStandard(model);
        //保存成功时
        if (isSucce)
        {
            jc = new JsonClass(model.ID, "", 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }
        //输出响应
        context.Response.Write(jc);
    }

    public void SearchDetailsInfo(HttpContext context)
    {

        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];

        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "deptName";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;
        
     
    //    string year = context.Request.QueryString["year"];
       string deptIDGet = context.Request.QueryString["DeptID"];

        string year = context.Request.QueryString["year"];
     
        DataTable dtData = new DataTable();
        dtData.Columns.Add("deptName");//部门
        dtData.Columns.Add("MonthCount1");//人数
        dtData.Columns.Add("MonthMoney1");//金额
        dtData.Columns.Add("MonthCount2");//人数
        dtData.Columns.Add("MonthMoney2");//金额
        dtData.Columns.Add("MonthCount3");//人数
        dtData.Columns.Add("MonthMoney3");//金额
        dtData.Columns.Add("MonthCount4");//人数
        dtData.Columns.Add("MonthMoney4");//金额
        dtData.Columns.Add("MonthCount5");//人数
        dtData.Columns.Add("MonthMoney5");//金额
        dtData.Columns.Add("MonthCount6");//人数
        dtData.Columns.Add("MonthMoney6");//金额
        dtData.Columns.Add("MonthCount7");//人数
        dtData.Columns.Add("MonthMoney7");//金额
        dtData.Columns.Add("MonthCount8");//人数
        dtData.Columns.Add("MonthMoney8");//金额
        dtData.Columns.Add("MonthCount9");//人数
        dtData.Columns.Add("MonthMoney9");//金额
        dtData.Columns.Add("MonthCount10");//人数
        dtData.Columns.Add("MonthMoney10");//金额
        dtData.Columns.Add("MonthCount11");//人数
        dtData.Columns.Add("MonthMoney11");//金额
        dtData.Columns.Add("MonthCount12");//人数
        dtData.Columns.Add("MonthMoney12");//金额
        dtData.Columns.Add("summary");//月平均金额


        if (!string.IsNullOrEmpty(deptIDGet))
        {
            decimal sum = 0;
            DataRow newRow = dtData.NewRow();



            string DeptName = SalaryStandardBus.GetNameByDeptID(deptIDGet);
            if (string.IsNullOrEmpty(DeptName))
            {
                DeptName = " ";
            }

            newRow["deptName"] = DeptName;
            for (int month = 1; month < 13; month++)
            {
                string monthTemp;
                if (month < 10)
                {
                    monthTemp = "0" + month.ToString();
                }
                else
                {
                    monthTemp = month.ToString();
                }

                DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptIDGet, monthTemp);
                if (dtNew.Rows.Count > 0)
                {
                    newRow["MonthCount" + month.ToString()] = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                    string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "0" : dtNew.Rows[0]["UnitPrice"].ToString();
                    newRow["MonthMoney" + month.ToString()] = UnitPrice;
                    if (string.IsNullOrEmpty(UnitPrice))
                    {
                        UnitPrice = "0";
                    }
                    sum = sum + Convert.ToDecimal(UnitPrice);
                }
                else
                {
                    newRow["MonthCount" + month.ToString()] = " ";
                    newRow["MonthMoney" + month.ToString()] = " ";
                    sum = sum + 0;
                }
            }
            decimal dd = Math.Round(sum / 12, 4);
            newRow["summary"] = Convert.ToString(dd);
            dtData.Rows.Add(newRow);
        }
        else
        {
            DataTable dt = SalaryStandardBus.GetDeptInfo();
            for (int a = 0; a < dt.Rows.Count; a++)
             {
                decimal sum = 0;
                DataRow newRow = dtData.NewRow();


                string deptID = dt.Rows[a]["DeptID"] == null ? "" : dt.Rows[a]["DeptID"].ToString();
                string DeptName = SalaryStandardBus.GetNameByDeptID(deptID);
                if (string.IsNullOrEmpty(DeptName))
                {
                    DeptName = " ";
                }

                newRow["deptName"] = DeptName;
                for (int month = 1; month < 13; month++)
                {
                    string monthTemp;
                    if (month < 10)
                    {
                        monthTemp = "0" + month.ToString();
                    }
                    else
                    {
                        monthTemp = month.ToString();
                    }

                    DataTable dtNew = SalaryStandardBus.GetMonthlyInfo(year, deptID, monthTemp);
                    if (dtNew.Rows.Count > 0)
                    {
                        newRow["MonthCount" + month.ToString()] = dtNew.Rows[0]["CompanyCD"] == null ? "" : dtNew.Rows[0]["CompanyCD"].ToString();
                        string UnitPrice = dtNew.Rows[0]["UnitPrice"] == null ? "0" : dtNew.Rows[0]["UnitPrice"].ToString();
                        if (string.IsNullOrEmpty(UnitPrice))
                        {
                            UnitPrice = "0"; 
                        }
                        
                        newRow["MonthMoney" + month.ToString()] = UnitPrice;

                         sum = sum + Convert.ToDecimal(UnitPrice);
                    }
                    else
                    {
                        newRow["MonthCount" + month.ToString()] = " ";
                        newRow["MonthMoney" + month.ToString()] = " ";
                        sum = sum + 0;
                    }
                }
                decimal dd = Math.Round(sum / 12, 4);
                newRow["summary"] = Convert.ToString(dd);
                dtData.Rows.Add(newRow);
            }
        }
     
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PurchaseRejectModel()
             {
                 BillStatus = x.Element("deptName") == null ? "" : x.Element("deptName").Value,//考核任务名称
                 CarryType = x.Element("MonthCount1") == null ? "" : x.Element("MonthCount1").Value,//
                 CloseDate = x.Element("MonthMoney1") == null ? "" : x.Element("MonthMoney1").Value,//
                 Closer = x.Element("MonthCount2") == null ? "" : x.Element("MonthCount2").Value,//ID
                 CompanyCD = x.Element("MonthMoney2") == null ? "" : x.Element("MonthMoney2").Value,//
                 ConfirmDate = x.Element("MonthCount3") == null ? "" : x.Element("MonthCount3").Value,//
                 Confirmor = x.Element("MonthMoney3") == null ? "" : x.Element("MonthMoney3").Value,//
                 CountTotal = x.Element("MonthCount4") == null ? "" : x.Element("MonthCount4").Value,//考核任务名称
                 CreateDate = x.Element("MonthMoney4") == null ? "" : x.Element("MonthMoney4").Value,//
                 Creator = x.Element("MonthCount5") == null ? "" : x.Element("MonthCount5").Value,//
                 CurrencyType = x.Element("MonthMoney5") == null ? "" : x.Element("MonthMoney5").Value,//ID
                 DeptID = x.Element("MonthCount6") == null ? "" : x.Element("MonthCount6").Value,//
                 Discount = x.Element("MonthMoney6") == null ? "" : x.Element("MonthMoney6").Value,//
                 DiscountTotal = x.Element("MonthCount7") == null ? "" : x.Element("MonthCount7").Value,//
                 ProviderID = x.Element("MonthMoney7") == null ? "" : x.Element("MonthMoney7").Value,//考核任务名称
                 MoneyType = x.Element("MonthCount8") == null ? "" : x.Element("MonthCount8").Value,//
                 PayType = x.Element("MonthMoney8") == null ? "" : x.Element("MonthMoney8").Value,//
                 Purchaser = x.Element("MonthCount9") == null ? "" : x.Element("MonthCount9").Value,//ID
                 Rate = x.Element("MonthMoney9") == null ? "" : x.Element("MonthMoney9").Value,//
                 RealTotal = x.Element("MonthCount10") == null ? "" : x.Element("MonthCount10").Value,//
                 ReceiveMan = x.Element("MonthMoney10") == null ? "" : x.Element("MonthMoney10").Value,//
                 FromType = x.Element("MonthCount11") == null ? "" : x.Element("MonthCount11").Value,//考核任务名称
                 ID = x.Element("MonthMoney11") == null ? "" : x.Element("MonthMoney11").Value,//
                 isAddTax = x.Element("MonthCount12") == null ? "" : x.Element("MonthCount12").Value,//
                 ModifiedDate = x.Element("MonthMoney12") == null ? "" : x.Element("MonthMoney12").Value,//ID
                 ModifiedUserID = x.Element("summary") == null ? "" : x.Element("summary").Value//
             })
                       :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PurchaseRejectModel()
             {
                 BillStatus = x.Element("deptName") == null ? "" : x.Element("deptName").Value,//考核任务名称
                 CarryType = x.Element("MonthCount1") == null ? "" : x.Element("MonthCount1").Value,//
                 CloseDate = x.Element("MonthMoney1") == null ? "" : x.Element("MonthMoney1").Value,//
                 Closer = x.Element("MonthCount2") == null ? "" : x.Element("MonthCount2").Value,//ID
                 CompanyCD = x.Element("MonthMoney2") == null ? "" : x.Element("MonthMoney2").Value,//
                 ConfirmDate = x.Element("MonthCount3") == null ? "" : x.Element("MonthCount3").Value,//
                 Confirmor = x.Element("MonthMoney3") == null ? "" : x.Element("MonthMoney3").Value,//
                 CountTotal = x.Element("MonthCount4") == null ? "" : x.Element("MonthCount4").Value,//考核任务名称
                 CreateDate = x.Element("MonthMoney4") == null ? "" : x.Element("MonthMoney4").Value,//
                 Creator = x.Element("MonthCount5") == null ? "" : x.Element("MonthCount5").Value,//
                 CurrencyType = x.Element("MonthMoney5") == null ? "" : x.Element("MonthMoney5").Value,//ID
                 DeptID = x.Element("MonthCount6") == null ? "" : x.Element("MonthCount6").Value,//
                 Discount = x.Element("MonthMoney6") == null ? "" : x.Element("MonthMoney6").Value,//
                 DiscountTotal = x.Element("MonthCount7") == null ? "" : x.Element("MonthCount7").Value,//
                 ProviderID = x.Element("MonthMoney7") == null ? "" : x.Element("MonthMoney7").Value,//考核任务名称
                 MoneyType = x.Element("MonthCount8") == null ? "" : x.Element("MonthCount8").Value,//
                 PayType = x.Element("MonthMoney8") == null ? "" : x.Element("MonthMoney8").Value,//
                 Purchaser = x.Element("MonthCount9") == null ? "" : x.Element("MonthCount9").Value,//ID
                 Rate = x.Element("MonthMoney9") == null ? "" : x.Element("MonthMoney9").Value,//
                 RealTotal = x.Element("MonthCount10") == null ? "" : x.Element("MonthCount10").Value,//
                 ReceiveMan = x.Element("MonthMoney10") == null ? "" : x.Element("MonthMoney10").Value,//
                 FromType = x.Element("MonthCount11") == null ? "" : x.Element("MonthCount11").Value,//考核任务名称
                 ID = x.Element("MonthMoney11") == null ? "" : x.Element("MonthMoney11").Value,//
                 isAddTax = x.Element("MonthCount12") == null ? "" : x.Element("MonthCount12").Value,//
                 ModifiedDate = x.Element("MonthMoney12") == null ? "" : x.Element("MonthMoney12").Value,//ID
                 ModifiedUserID = x.Element("summary") == null ? "" : x.Element("summary").Value//
             });
        //获取记录总数
        string ss = dsLinq.ToString();
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
    /// 删除操作
    /// </summary>
    /// <param name="context">请求上下文</param>
    private void DoDelete(HttpContext context)
    {
        //获取要素ID
        string elemID = context.Request.QueryString["DeleteNO"];
        //替换引号
        elemID = elemID.Replace("'", "");

            //删除要素
            bool isSucc = SalaryStandardBus.DeleteAllSalaryInfo (elemID);
            //删除成功
            if (isSucc)
            {
                //输出响应
                context.Response.Write(new JsonClass("", "", 1));
            }
            //删除失败
            else
            {
                //输出响应
                context.Response.Write(new JsonClass("", "", 0));
            }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}