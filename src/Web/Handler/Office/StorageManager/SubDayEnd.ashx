<%@ WebHandler Language="C#" Class="SubDayEnd" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.StorageManager ;
using XBase.Model.Office.StorageManager;
using System.Web.SessionState;
using System.Collections.Generic;
using XBase.Business.Common;
using System.Data.SqlClient;
using System.Text;

public class SubDayEnd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
        string _deptID = "";
        DataRow dtSong = SubStorageDBHelper.GetSubDeptFromDeptID(user.DeptID.ToString());

        if (dtSong != null)
        {
            _deptID = dtSong["ID"].ToString(); 
        }
     
        
        if (context.Request.RequestType == "POST")
        {
            string ActionOrder = context.Request.Params["ActionApply"];
            if (ActionOrder == "DayEnd")
            {
                JsonClass jc;
                string day = context.Request.Params["day"];
                string frontDate = Convert.ToDateTime(day).AddDays(-1).ToString("yyyy-MM-dd");
                string sdDate = Convert.ToDateTime(day).AddDays(1).ToString("yyyy-MM-dd");
                string _companycd = user.CompanyCD;

                bool isFirst = SubDayEndBus.CheckFirstOperate(user.CompanyCD,_deptID );
               
                if (!isFirst)
                {
                    bool isHaveData = SubDayEndBus.isHaveData(user.CompanyCD, _deptID, sdDate);

                    if (!isHaveData)
                    {
                        if (DayEndOperate(day, _deptID, false))
                        {
                            jc = new JsonClass("success", "", 1);
                            context.Response.Write(jc);
                            return;

                        }
                    }
                    else
                    {

                        jc = new JsonClass("success", "", 4);
                        context.Response.Write(jc);
                        return;
                    }
                }
                else
                {
                    DataTable dtDateInfo = SubDayEndBus.GetOperateDateInfo(user.CompanyCD,_deptID );
                    if (dtDateInfo.Rows.Count > 0)
                    {
                        string FirstDailyDate = dtDateInfo.Rows[0]["FirstDailyDate"].ToString();
                        string LastDailyDate = dtDateInfo.Rows[0]["LastDailyDate"].ToString();
                        if (FirstDailyDate == day)
                        {
                            jc = new JsonClass("不允许日结第一次日结的数据", "", 2);//
                            context.Response.Write(jc);
                            return;
                        }
                        else
                        {
                            TimeSpan span = DateTime.Parse(day).Subtract(DateTime.Parse(LastDailyDate));
                            if (span.Days > 0)
                            {

                                if (span.Days == 1)
                                {
                                    if (DayEndOperate(day,_deptID , true))
                                    {
                                        jc = new JsonClass("success", "", 1);
                                        context.Response.Write(jc);
                                        return;
                                    }
                                }
                                else
                                {
                                    for (int i = 1; i <= span.Days; i++)
                                    {
                                        DateTime dtOperateTime = DateTime.Parse(LastDailyDate).AddDays(i);
                                        if (DayEndOperate(dtOperateTime.ToString("yyyy-MM-dd"),_deptID , true))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            jc = new JsonClass("日结" + day + "失败", "", 2);//日结失败
                                            context.Response.Write(jc);
                                            return;
                                        }


                                    }

                                    jc = new JsonClass("success", "", 1);
                                    context.Response.Write(jc);
                                    return;

                                }


                            }
                            else
                            {

                                if (DayEndOperate(day,_deptID , true))
                                {
                                    jc = new JsonClass("success", "", 1);
                                    context.Response.Write(jc);
                                    return;
                                }
                            }


                        }
                    }


                }
                
                
                
                
                
             
            }
            else if (ActionOrder == "Search")
            {
                //设置行为参数
                string orderString = (context.Request.Form["orderby"].ToString());//排序
                string order = "DESC";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreateDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "ASC";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string day = context.Request.Form["day"];

                int TotalCount = 0;

                orderBy = orderBy + " " + order;
                context.Response.ContentType = "text/plain";
                //string temp = JsonClass.DataTable2Json();
                DataTable dt = SubDayEndBus.SelectDayInfo(pageIndex, pageCount, orderBy, ref TotalCount, day,_deptID);

                System.Text.StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));

                }
                else
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                context.Response.Write(sb.ToString());
                context.Response.End();


            }
            else if (ActionOrder == "SearchRecord")
            {
                JsonClass jc;
                string day = context.Request.Params["day"];
                DataTable dtDateInfo = SubDayEndBus.GetOperateDateInfo(user.CompanyCD,_deptID );
                DateTime now = DateTime.Now;
                if (now.Subtract(DateTime.Parse(day)).Days == 0)
                {
                    jc = new JsonClass("查询日期不允许在" + now.AddDays(-1).ToString("yyyy-MM-dd") + "之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }
                if (now.AddDays(-1).Subtract(DateTime.Parse(day)).Days < 0)
                {
                    jc = new JsonClass("查询日期不允许在" + now.AddDays(-1).ToString("yyyy-MM-dd")+"之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }
                bool isFirst = SubDayEndBus.CheckFirstOperate(user.CompanyCD,_deptID );
                if (!isFirst)
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    return;
                }
                else
                {


                    if (dtDateInfo.Rows.Count > 0)
                    {
                        string FirstDailyDate = dtDateInfo.Rows[0]["FirstDailyDate"].ToString();
                        if (now.ToString("yyyy-MM-dd") == FirstDailyDate)
                        {
                            jc = new JsonClass("success", "", 1);
                            context.Response.Write(jc);
                            return;

                        }
                        else
                        {

                            if (DateTime.Parse(FirstDailyDate).Subtract(DateTime.Parse(day)).Days > 0)
                            {
                                jc = new JsonClass("查询日期不允许在第一次日结日期" + FirstDailyDate + "之前", "", 2);
                                context.Response.Write(jc);
                                return;

                            }
                            else
                            {

                                jc = new JsonClass("success", "", 1);
                                context.Response.Write(jc);
                                return;
                            }

                        }



                    }
                    else
                    {

                        jc = new JsonClass("success", "", 1);
                        context.Response.Write(jc);
                        return;

                    }
                }

            }

            else if (ActionOrder == "OperateRecord")
            {
                JsonClass jc;
                string day = context.Request.Params["day"];
           
                DataTable dtDateInfo = SubDayEndBus.GetOperateDateInfo(user.CompanyCD,_deptID );
                DateTime now = DateTime.Now;
                if (now.Subtract(DateTime.Parse(day)).Days == 0)
                {
                    jc = new JsonClass("日结日期不允许在" + now.AddDays(-1).ToString("yyyy-MM-dd") + "之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }
                
                if (now.AddDays(-1).Subtract(DateTime.Parse(day)).Days < 0)
                {
                    jc = new JsonClass("日结日期不允许在" + now.AddDays(-1).ToString ("yyyy-MM-dd")+"之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }

                bool isFirst = SubDayEndBus.CheckFirstOperate(user.CompanyCD,_deptID );
                if (!isFirst)
                {
                    jc = new JsonClass("success", "", 1);
                    context.Response.Write(jc);
                    return;
                }
                else
                {



                    if (dtDateInfo.Rows.Count > 0)
                    {
                        string FirstDailyDate = dtDateInfo.Rows[0]["FirstDailyDate"].ToString();
                        if (day == FirstDailyDate)
                        {
                            jc = new JsonClass("日结日期只能大于第一次的日结日期" + FirstDailyDate , "", 2);
                            context.Response.Write(jc);
                            return;

                        }
                        else
                        {

                            if (DateTime.Parse(FirstDailyDate).AddDays(-1).Subtract(DateTime.Parse(day)).Days >= 0)
                            {
                                jc = new JsonClass("日结日期不允许在第一次日结日期" + FirstDailyDate + "之前", "", 2);
                                context.Response.Write(jc);
                                return;

                            }
                            else
                            {
                                jc = new JsonClass("success", "", 1);
                                context.Response.Write(jc);
                                return;

                            }

                        }



                    }
                    else
                    {

                        jc = new JsonClass("success", "", 1);
                        context.Response.Write(jc);
                        return;

                    }

                }
            }




        }
    }


    public static bool DayEndOperate(string dateTime, string DeptID, bool isFirstOperate)
    {
        UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
        string _companycd = user.CompanyCD;
        ArrayList lstInsert = new ArrayList();//所有的插入sqlcommand命令集
        DataTable dtProductList = SubDayEndBus.GetCompanyProductList(_companycd,DeptID );//从门店分仓存量表中获取改公司分店下的所有产品信息
        if (isFirstOperate)
        {
            SubDayEndBus.DeleteDay(_companycd, dateTime, DeptID);
        }
        if (dtProductList.Rows.Count > 0)
        {
            for (int i = 0; i < dtProductList.Rows.Count; i++)
            {

              
                string ProductID = dtProductList.Rows[i]["ProductID"].ToString();
                string BatchNo = dtProductList.Rows[i]["BatchNo"].ToString();
                string frontDate = Convert.ToDateTime(dateTime).AddDays(-1).ToString("yyyy-MM-dd");

                if (string.IsNullOrEmpty(ProductID))
                {
                    continue; 
                }

                decimal frontDayCount = 0;
                if (!isFirstOperate)
                {
                    frontDayCount = SubDayEndBus.GetFirstDayCount(ProductID, BatchNo, _companycd, DeptID);//获得前一天的某个物品的结存量
                    if (frontDayCount == -123456789)
                    {
                        continue;
                    }
                }
                else
                {
             frontDayCount=       SubDayEndBus.GetFrontDayCount(ProductID, BatchNo, frontDate, _companycd, DeptID);//获得前一天的某个物品的结存量
                }

               
                
                decimal SurplusCount = 0;//结存量
                decimal InTotalCount = 0;//入库总数量（等于所有入库数量字段的累加减去)
                decimal OutTotalCount = 0;//出库总数量（等于所有出库数量字段的累加减去红冲出库数量）
                decimal SaleTotalPrice = 0;//销售金额总计
                decimal SaleRejectTotalPrice = 0;//销售退货金额总计
                SubStorageDailyModel model = new SubStorageDailyModel();
                model.CompanyCD = _companycd;
                model.ProductID = ProductID;
                model.BatchNo = BatchNo;
                model.DailyDate = dateTime;
                if (isFirstOperate)
                {
                    decimal BegInStorage = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.期初库存录入_1);
                    SurplusCount = SurplusCount + BegInStorage;
                    InTotalCount = InTotalCount + BegInStorage;
                    model.InitInCount = Convert.ToString(BegInStorage);

                    decimal DAORU = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.期初库存导入_8);
                    SurplusCount = SurplusCount + DAORU;
                    InTotalCount = InTotalCount + DAORU;
                    model.InitBatchCount = Convert.ToString(DAORU);
                    
                    

                    decimal DeleverOutCount = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.配送单_2);
                    SurplusCount = SurplusCount +DeleverOutCount;
                    InTotalCount = InTotalCount + DeleverOutCount;
                    model.SendInCount = Convert.ToString(DeleverOutCount);

                    decimal SendOutCount = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.配送退货单_7);
                    SurplusCount = SurplusCount - SendOutCount;
                    OutTotalCount = OutTotalCount + SendOutCount;
                    model.SendOutCount = Convert.ToString(SendOutCount);

                    DataTable dtSubSaleInfo = SubDayEndBus.GetSaleInfo(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)Mode.分店发货_1);
                    if (dtSubSaleInfo.Rows.Count > 0)
                    {
                        decimal SubSaleOutCount = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.销售单_3);
                        model.SubSaleOutCount = Convert.ToString(SubSaleOutCount);
                        SurplusCount = SurplusCount - SubSaleOutCount;
                      OutTotalCount = OutTotalCount + SubSaleOutCount;
                        decimal SubSaleOutPrice = Convert.ToDecimal(dtSubSaleInfo.Rows[0]["TotalPrice"] == null ? "0" : dtSubSaleInfo.Rows[0]["TotalPrice"].ToString());

                        SaleTotalPrice = SaleTotalPrice + SubSaleOutPrice;
                    }

                    DataTable dtSaleInfo = SubDayEndBus.GetSaleInfo(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)Mode.总店发货_2);
                    if (dtSaleInfo.Rows.Count > 0)
                    {
                        decimal SaleOutCount = Convert.ToDecimal(dtSaleInfo.Rows[0]["CountTotal"] == null ? "0" : dtSaleInfo.Rows[0]["CountTotal"].ToString());
                        model.SaleOutCount = Convert.ToString(SaleOutCount);
                       // SurplusCount = SurplusCount - SaleOutCount;
                      //  OutTotalCount = OutTotalCount + SaleOutCount;
                        decimal SaleOutPrice = Convert.ToDecimal(dtSaleInfo.Rows[0]["TotalPrice"] == null ? "0" : dtSaleInfo.Rows[0]["TotalPrice"].ToString());
                        SaleTotalPrice = SaleTotalPrice + SaleOutPrice;
                    }

                    DataTable dtSubSaleRejectInfo = SubDayEndBus.GetSaleRejectInfo(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)Mode.分店发货_1);
                    if (dtSubSaleRejectInfo.Rows.Count > 0)
                    {
                        decimal SubSaleBackInCount = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.销售退货单_4);
                        model.SubSaleBackInCount = Convert.ToString(SubSaleBackInCount);
                        SurplusCount = SurplusCount + SubSaleBackInCount;
                        InTotalCount = InTotalCount + SubSaleBackInCount;
                        decimal SubSaleBackPrice = Convert.ToDecimal(dtSubSaleRejectInfo.Rows[0]["TotalPrice"] == null ? "0" : dtSubSaleRejectInfo.Rows[0]["TotalPrice"].ToString());

                        SaleRejectTotalPrice = SaleRejectTotalPrice + SubSaleBackPrice;
                    }

                    DataTable dtSaleRejectInfo = SubDayEndBus.GetSaleRejectInfo(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)Mode.总店发货_2);
                    if (dtSaleRejectInfo.Rows.Count > 0)
                    {
                        decimal SaleBackInCount = Convert.ToDecimal(dtSaleRejectInfo.Rows[0]["CountTotal"] == null ? "0" : dtSaleRejectInfo.Rows[0]["CountTotal"].ToString());

                        model.SaleBackInCount = Convert.ToString(SaleBackInCount);
                       // SurplusCount = SurplusCount + SaleBackInCount;
                     //   InTotalCount = InTotalCount + SaleBackInCount;
                        decimal SaleBackPrice = Convert.ToDecimal(dtSaleRejectInfo.Rows[0]["TotalPrice"] == null ? "0" : dtSaleRejectInfo.Rows[0]["TotalPrice"].ToString());

                        SaleRejectTotalPrice = SaleRejectTotalPrice + SaleBackPrice;
                    }





                    decimal OtherInStorage = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.门店调拨出库_5);
                    SurplusCount = SurplusCount - OtherInStorage;
                    OutTotalCount = OutTotalCount + OtherInStorage;
                    model.DispOutCount = Convert.ToString(OtherInStorage);

                    decimal DispInCont = SubDayEndBus.GetDayItemsCount(ProductID, BatchNo, DeptID, dateTime, _companycd, (int)itemsType.门店调拨入库_6);
                    SurplusCount = SurplusCount + DispInCont;
                    InTotalCount = InTotalCount + DispInCont;
                    model.DispInCont = Convert.ToString(DispInCont);


                }
                model.TodayCount = Convert.ToString(frontDayCount + SurplusCount);
                model.InTotal = Convert.ToString(InTotalCount);
                model.OutTotal = Convert.ToString(OutTotalCount);
                model.SaleFee = Convert.ToString(SaleTotalPrice);
                model.SaleBackFee = Convert.ToString(SaleRejectTotalPrice);
                model.DeptID = DeptID;
                model.CreateDate = DateTime.Now.ToString();
                model.Creator = user.EmployeeID.ToString();
                SqlCommand comm = new SqlCommand();
                comm = InsertDaily(model);
                lstInsert.Add(comm);
            }
            return SqlHelper.ExecuteTransWithArrayList(lstInsert);
        }
        else
        {
            return true;
        }


    }

    protected static SqlCommand InsertDaily(SubStorageDailyModel model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("INSERT INTO officedba.SubStorageDaily");
        strSql.Append("  (CompanyCD");
        strSql.Append("  ,DailyDate");
        strSql.Append(" ,ProductID");
        strSql.Append(" ,DeptID"); 
        if (!string.IsNullOrEmpty(model.BatchNo))
        {
            strSql.Append("  ,BatchNo");
        }
        strSql.Append("  ,InitInCount");
        strSql.Append("   ,SendInCount");
        strSql.Append("  ,SubSaleBackInCount");
        strSql.Append("   ,SaleBackInCount");
        strSql.Append("   ,DispInCont");
        strSql.Append("   ,SubSaleOutCount");
        strSql.Append("  ,SaleOutCount");
        strSql.Append("    ,DispOutCount");
        strSql.Append("    ,SendOutCount");
        strSql.Append("    ,InTotal");
        strSql.Append("    ,OutTotal");
        strSql.Append("   ,TodayCount");
        strSql.Append("    ,SaleFee");
        strSql.Append("   ,SaleBackFee");
        strSql.Append("   ,Remark");
        strSql.Append("  ,CreateDate");
        strSql.Append("  ,InitBatchCount");
        
        strSql.Append("   ,Creator)");
        strSql.Append("   VALUES");
        strSql.Append("  (@CompanyCD");
        strSql.Append("  ,@DailyDate");
        strSql.Append(" ,@ProductID");
        strSql.Append(" ,@DeptID"); 
        if (!string.IsNullOrEmpty(model.BatchNo))
        {
            strSql.Append("  ,@BatchNo");
        }
        strSql.Append("  ,@InitInCount");
        strSql.Append("   ,@SendInCount");
        strSql.Append("  ,@SubSaleBackInCount");
        strSql.Append("   ,@SaleBackInCount");
        strSql.Append("   ,@DispInCont");
        strSql.Append("   ,@SubSaleOutCount");
        strSql.Append("  ,@SaleOutCount");
        strSql.Append("    ,@DispOutCount");
        strSql.Append("    ,@SendOutCount");
        strSql.Append("    ,@InTotal");
        strSql.Append("    ,@OutTotal");
        strSql.Append("   ,@TodayCount");
        strSql.Append("    ,@SaleFee");
        strSql.Append("   ,@SaleBackFee");
        strSql.Append("   ,@Remark");
        strSql.Append("  ,getdate()");
        strSql.Append("  ,@InitBatchCount");
        strSql.Append("   ,@Creator)");

        SqlCommand comm = new SqlCommand();
        comm.CommandText = strSql.ToString();
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate ", model.DailyDate));//编号
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//
        if (!string.IsNullOrEmpty(model.BatchNo))
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//
        }
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitInCount ", model.InitInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendInCount ", model.SendInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubSaleBackInCount ", model.SubSaleBackInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleBackInCount ", model.SaleBackInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispInCont ", model.DispInCont));
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubSaleOutCount ", model.SubSaleOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleOutCount ", model.SaleOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispOutCount ", model.DispOutCount));//
     
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitBatchCount ", model.InitBatchCount));//
        
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendOutCount ", model.SendOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InTotal ", model.InTotal));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutTotal ", model.OutTotal));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@TodayCount ", model.TodayCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleFee ", model.SaleFee));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleBackFee ", model.SaleBackFee));
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//

         
        return comm;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    enum itemsType
    {
        期初库存录入_1 = 1,
        配送单_2,
        销售单_3,
        销售退货单_4,
        门店调拨出库_5,
        门店调拨入库_6,
       配送退货单_7,
       期初库存导入_8,
    }
    enum Mode
    {
        分店发货_1=1,
        总店发货_2 
    }

}