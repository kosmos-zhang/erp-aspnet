<%@ WebHandler Language="C#" Class="DayEnd" %>
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
public class DayEnd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.RequestType == "POST")
        {
            string ActionOrder = context.Request.Params ["ActionApply"];
            if (ActionOrder == "DayEnd")
            {
                JsonClass jc;
                string day = context.Request.Params ["day"];
                string frontDate = Convert.ToDateTime(day).AddDays(-1).ToString("yyyy-MM-dd");
                UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                string sdDate = Convert.ToDateTime(day).AddDays(1).ToString("yyyy-MM-dd");
                bool isFirst = DayEndBus.CheckFirstOperate(user.CompanyCD);
                if (!isFirst)
                {
                    bool isHaveData = DayEndBus.isHaveData(user.CompanyCD, sdDate);
                     if (!isHaveData)
                     {
                         if (DayEndOperate(day, false))
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
                     DataTable dtDateInfo=   DayEndBus.GetOperateDateInfo(user.CompanyCD);
                     if (dtDateInfo.Rows.Count > 0)
                     {
                         string FirstDailyDate = dtDateInfo.Rows[0]["FirstDailyDate"].ToString();
                         string LastDailyDate = dtDateInfo.Rows[0]["LastDailyDate"].ToString();
                         if (FirstDailyDate == day)
                         {
                             jc = new JsonClass("不允许日结第一次日结的数据", "", 2);//不允许日结第一次日结的数据
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
                                     if (DayEndOperate(day, true))
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
                                         DateTime dtOperateTime = DateTime.Parse(LastDailyDate).AddDays (i);
                                         if (DayEndOperate(dtOperateTime.ToString("yyyy-MM-dd"), true))
                                         {
                                             continue;
                                         }
                                         else
                                         {
                                             jc = new JsonClass("日结" + day+"失败", "", 2);//日结失败
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

                                 if (DayEndOperate(day, true ))
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
                DataTable dt = DayEndBus.SelectDayInfo (pageIndex, pageCount, orderBy, ref TotalCount, day );

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
                UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                 DataTable dtDateInfo=   DayEndBus.GetOperateDateInfo(user.CompanyCD);
                 DateTime now = DateTime.Now;
                 if (now.Subtract(DateTime.Parse(day)).Days == 0)
                 {
                     jc = new JsonClass("查询日期不允许在" + now.AddDays(-1).ToString("yyyy-MM-dd") + "之后", "", 2);
                     context.Response.Write(jc);
                     return;

                 }
                 if (now.AddDays(-1).Subtract(DateTime.Parse(day)).Days < 0)
                 {
                     jc = new JsonClass("查询日期不允许超出" + now.AddDays(-1).ToString("yyyy-MM-dd"), "", 2);
                     context.Response.Write(jc);
                     return;
                 
                 }
                 bool isFirst = DayEndBus.CheckFirstOperate(user.CompanyCD);
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
                UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
                DataTable dtDateInfo = DayEndBus.GetOperateDateInfo(user.CompanyCD);
                DateTime now = DateTime.Now;
                if (now.Subtract(DateTime.Parse(day)).Days== 0)
                {
                    jc = new JsonClass("日结日期不允许在" + now.AddDays(-1).ToString("yyyy-MM-dd")+"之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }
                if (now.AddDays(-1).Subtract(DateTime.Parse(day)).Days < 0)
                {
                    jc = new JsonClass("日结日期不允许在" + now.AddDays(-1).ToString ("yyyy-MM-dd")+"之后", "", 2);
                    context.Response.Write(jc);
                    return;

                }

                bool isFirst = DayEndBus.CheckFirstOperate(user.CompanyCD);
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
                            jc = new JsonClass("日结日期只能大于第一次的日结日期" + FirstDailyDate, "", 2);
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


    public static bool DayEndOperate(string dateTime,bool isFirstOperate)
    {
        UserInfoUtil user = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
           string _companycd=user.CompanyCD;
           ArrayList lstInsert = new ArrayList();//所有的插入sqlcommand命令集
        
           DataTable dtProductList = DayEndBus.GetCompanyProductList(_companycd);//从分仓存量表中获取改公司的所有产品信息
           if (isFirstOperate)
           {
               DayEndBus.DeleteDay(_companycd, dateTime);
           }
           if (dtProductList.Rows.Count > 0)
           {
               for (int i = 0; i < dtProductList.Rows.Count; i++)
               {

                   //取得前一天的某个物品的结存量
                   string ProductID = dtProductList.Rows[i]["ProductID"].ToString();
                   string BatchNo = dtProductList.Rows[i]["BatchNo"].ToString();
                   string StorageID = dtProductList.Rows[i]["StorageID"].ToString();
                   string frontDate = Convert.ToDateTime(dateTime).AddDays(-1).ToString("yyyy-MM-dd");

                   if (string.IsNullOrEmpty(ProductID) || string.IsNullOrEmpty(StorageID))
                   {
                       continue;
                   }
                   
                   decimal frontDayCount=0;
                   if (!isFirstOperate)
                     {
                         frontDayCount = DayEndBus.GetFirstDayCount(ProductID, BatchNo, StorageID, _companycd);//获得分仓存量表里的数据
                         if (frontDayCount == -123456789)
                         {
                             continue;
                         }
                     }
                     else
                     {
                         frontDayCount = DayEndBus.GetFrontDayCount(ProductID, BatchNo, StorageID, frontDate, _companycd);//获得前一天的某个物品的结存量
                     }

                 
                     
                 
                   
                   
                   decimal SurplusCount = 0;//结存量
                   decimal InTotalCount = 0;//入库总数量（等于所有入库数量字段的累加减去)
                   decimal OutTotalCount = 0;//出库总数量（等于所有出库数量字段的累加减去红冲出库数量）

                   DayEndModelDetail model = new DayEndModelDetail();
                   model.CompanyCD = _companycd;
                   model.ProductID = ProductID;
                   model.BatchNo = BatchNo;
                   model.StorageID = StorageID;
                   model.DailyDate = dateTime;
                   if (isFirstOperate)
                   {
                       decimal BegInStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.期初库存录入_1);
                       SurplusCount = SurplusCount + BegInStorage;
                       InTotalCount = InTotalCount + BegInStorage;
                       model.InitInCount = Convert.ToString(BegInStorage);

                       decimal BegStorageInQuantities = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.期初库存批量导入_2);
                       SurplusCount = SurplusCount + BegStorageInQuantities;
                       InTotalCount = InTotalCount + BegStorageInQuantities;
                       model.InitBatchCount = Convert.ToString(BegStorageInQuantities);

                       decimal PurchaseInStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.采购入库单_3);
                       SurplusCount = SurplusCount + PurchaseInStorage;
                       InTotalCount = InTotalCount + PurchaseInStorage;
                       model.PhurInCount = Convert.ToString(PurchaseInStorage);

                       decimal ProductionInCompleted = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.生产完工入库单_4);
                       SurplusCount = SurplusCount + ProductionInCompleted;
                       InTotalCount = InTotalCount + ProductionInCompleted;
                       model.MakeInCount = Convert.ToString(ProductionInCompleted);

                       decimal OtherInStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.其他入库单_5);
                       SurplusCount = SurplusCount + OtherInStorage;
                       InTotalCount = InTotalCount + OtherInStorage;
                       model.OtherInCount = Convert.ToString(OtherInStorage);

                       decimal HongChongInStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.红冲入库单_6);
                       SurplusCount = SurplusCount - HongChongInStorage;
                       InTotalCount = InTotalCount - HongChongInStorage;
                       model.RedInCount = Convert.ToString(HongChongInStorage);

                       decimal SalesOutStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.销售出库单_7);
                       SurplusCount = SurplusCount - SalesOutStorage;
                       OutTotalCount = OutTotalCount + SalesOutStorage;
                       model.SaleOutCount = Convert.ToString(SalesOutStorage);

                       decimal OtherOutStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.其他出库单_8);
                       SurplusCount = SurplusCount - OtherOutStorage;
                       OutTotalCount = OutTotalCount + OtherOutStorage;
                       model.OtherOutCount = Convert.ToString(OtherOutStorage);

                       decimal HongChongOutStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.红冲出库单_9);
                       SurplusCount = SurplusCount + HongChongOutStorage;
                       OutTotalCount = OutTotalCount - OtherOutStorage;
                       model.RedOutCount = Convert.ToString(HongChongOutStorage);

                       decimal BorrowerOutApply = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.借货申请单_10);
                       SurplusCount = SurplusCount - BorrowerOutApply;
                       OutTotalCount = OutTotalCount + BorrowerOutApply;
                       model.LendCount = Convert.ToString(BorrowerOutApply);

                       decimal BorrowerIn = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.借货返还单_11);
                       SurplusCount = SurplusCount + BorrowerIn;
                       InTotalCount = InTotalCount + BorrowerIn;
                       model.BackInCount = Convert.ToString(BorrowerIn);

                       decimal AllocationOut = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.调拨出库_12);
                       SurplusCount = SurplusCount - AllocationOut;
                       OutTotalCount = OutTotalCount + AllocationOut;
                       model.DispOutCount = Convert.ToString(AllocationOut);

                       decimal AllocationIn = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.调拨入库_13);
                       SurplusCount = SurplusCount + AllocationIn;
                       InTotalCount = InTotalCount + AllocationIn;
                       model.DispInCount = Convert.ToString(AllocationIn);

                       decimal DailyAdjust = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.日常调整单_14);
                       SurplusCount = SurplusCount + DailyAdjust;
                       if (DailyAdjust > 0)
                       {
                           InTotalCount = InTotalCount + DailyAdjust;
                       }
                       else
                       {
                           OutTotalCount = OutTotalCount - DailyAdjust;
                       }
                       model.AdjustCount = Convert.ToString(DailyAdjust);

                       decimal EndingInventory = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.期末盘点单_15);
                       SurplusCount = SurplusCount + EndingInventory;
                       if (SurplusCount > 0)
                       {
                           InTotalCount = InTotalCount + EndingInventory;
                       }
                       else
                       {
                           OutTotalCount = OutTotalCount - EndingInventory;
                       }
                       model.CheckCount = Convert.ToString(EndingInventory);

                       decimal StorageReportLoss = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.库存报损单_16);
                       SurplusCount = SurplusCount - StorageReportLoss;
                       OutTotalCount = OutTotalCount + StorageReportLoss;
                       model.BadCount = Convert.ToString(StorageReportLoss);

                       decimal CollarMaterial = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.领料单_17);
                       SurplusCount = SurplusCount - CollarMaterial;
                       OutTotalCount = OutTotalCount + CollarMaterial;
                       model.TakeOutCount = Convert.ToString(CollarMaterial);

                       decimal RejectMaterial = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.退料单_18);
                       SurplusCount = SurplusCount + RejectMaterial;
                       InTotalCount = InTotalCount + RejectMaterial;
                       model.TakeInCount = Convert.ToString(RejectMaterial);

                       decimal Distribution = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.配送单_19);
                       SurplusCount = SurplusCount - Distribution;
                       OutTotalCount = OutTotalCount + Distribution;
                       model.SendOutCount = Convert.ToString(Distribution);

                       decimal DistributionReject = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.配送退货单_20);
                       SurplusCount = SurplusCount + DistributionReject;
                       InTotalCount = InTotalCount + DistributionReject;
                       model.SendInCount = Convert.ToString(DistributionReject);

                       decimal StoresStorage = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.门店销售管理_21);
                       SurplusCount = SurplusCount - StoresStorage;
                       OutTotalCount = OutTotalCount + StoresStorage;
                       model.SubSaleOutCount = Convert.ToString(StoresStorage);

                       decimal StoresStorageReject = DayEndBus.GetDayItemsCount(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.门店销售退货_22);
                       SurplusCount = SurplusCount + StoresStorageReject;
                       InTotalCount = InTotalCount + StoresStorageReject;
                       model.SubSaleBackInCount = Convert.ToString(StoresStorageReject);
                   }
                   model.TodayCount = Convert.ToString(frontDayCount + SurplusCount);
                   model.InTotal = Convert.ToString(InTotalCount);
                   model.OutTotal = Convert.ToString(OutTotalCount);

                   if (isFirstOperate)
                   {
                       decimal StoresStorageRejectPrice = DayEndBus.GetDayItemsPrice(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.门店销售退货_22);
                       model.SaleBackFee = Convert.ToString(StoresStorageRejectPrice);
                       decimal PurchaseInStoragePrice = DayEndBus.GetDayItemsPrice(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.采购入库单_3);
                       model.PhurFee = Convert.ToString(PurchaseInStoragePrice);
                       decimal SalesOutStoragePrice = DayEndBus.GetDayItemsPrice(ProductID, BatchNo, StorageID, dateTime, _companycd, (int)itemsType.销售出库单_7);
                       model.SaleFee = Convert.ToString(SalesOutStoragePrice);

                       DataTable dtRejectInfo = DayEndBus.GetPurchaseRejectInfo(ProductID, BatchNo, StorageID, dateTime, _companycd);
                       if (dtRejectInfo.Rows.Count > 0)
                       {
                           model.PhurBackOutCount = dtRejectInfo.Rows[0]["Count"] == null ? "0" : dtRejectInfo.Rows[0]["Count"].ToString();
                           model.PhurBackFee = dtRejectInfo.Rows[0]["TotalPrice"] == null ? "0" : dtRejectInfo.Rows[0]["TotalPrice"].ToString();
                       }
                   }
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

    protected static SqlCommand InsertDaily(DayEndModelDetail model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("INSERT INTO officedba.StorageDaily");
          strSql.Append("  (CompanyCD");
          strSql.Append("  ,DailyDate");
           strSql.Append(" ,ProductID");
          strSql.Append("  ,StorageID");
          if (!string.IsNullOrEmpty(model.BatchNo))
          {
              strSql.Append("  ,BatchNo");
          }
          strSql.Append("  ,InitInCount");
         strSql.Append("   ,InitBatchCount");
          strSql.Append("  ,PhurInCount");
         strSql.Append("   ,MakeInCount");
         //strSql.Append("   ,SaleBackInCount");
         strSql.Append("   ,SubSaleBackInCount");
          strSql.Append("  ,RedInCount");
        strSql.Append("    ,OtherInCount");
        strSql.Append("    ,BackInCount");
        strSql.Append("    ,TakeInCount");
        strSql.Append("    ,DispInCount");
         strSql.Append("   ,SendInCount");
        strSql.Append("    ,SaleOutCount");
         strSql.Append("   ,SubSaleOutCount");
         if (!string.IsNullOrEmpty(model.PhurBackOutCount))
         {
             strSql.Append("   ,PhurBackOutCount");
         }
          strSql.Append("  ,RedOutCount");
         strSql.Append("   ,OtherOutCount");
        strSql.Append("    ,DispOutCount");
        strSql.Append("    ,LendCount");
        strSql.Append("    ,AdjustCount");
         strSql.Append("   ,BadCount");
        strSql.Append("   ,CheckCount");
         strSql.Append("   ,TakeOutCount");
        strSql.Append("    ,SendOutCount");
        strSql.Append("    ,InTotal");
        strSql.Append("    ,OutTotal");
        strSql.Append("    ,TodayCount");
       strSql.Append("     ,SaleFee");
       strSql.Append("     ,SaleBackFee");
      strSql.Append("      ,PhurFee");
      if (!string.IsNullOrEmpty(model.PhurBackFee))
      {
          strSql.Append("     ,PhurBackFee");
      }
      // strSql.Append("     ,Remark");
          strSql.Append("  ,CreateDate");
        strSql.Append("    ,Creator)");
   strSql.Append("   VALUES");
   strSql.Append("  (@CompanyCD");
   strSql.Append("  ,@DailyDate");
   strSql.Append(" ,@ProductID");
   strSql.Append("  ,@StorageID");
   if (!string.IsNullOrEmpty(model.BatchNo))
   {
       strSql.Append("  ,@BatchNo");
   }
   strSql.Append("  ,@InitInCount");
   strSql.Append("   ,@InitBatchCount");
   strSql.Append("  ,@PhurInCount");
   strSql.Append("   ,@MakeInCount");
   //strSql.Append("   ,@SaleBackInCount");
   strSql.Append("   ,@SubSaleBackInCount");
   strSql.Append("  ,@RedInCount");
   strSql.Append("    ,@OtherInCount");
   strSql.Append("    ,@BackInCount");
   strSql.Append("    ,@TakeInCount");
   strSql.Append("    ,@DispInCount");
   strSql.Append("   ,@SendInCount");
   strSql.Append("    ,@SaleOutCount");
   strSql.Append("   ,@SubSaleOutCount");
   if (!string.IsNullOrEmpty(model.PhurBackOutCount))
   {
       strSql.Append("   ,@PhurBackOutCount");
   }
   strSql.Append("  ,@RedOutCount");
   strSql.Append("   ,@OtherOutCount");
   strSql.Append("    ,@DispOutCount");
   strSql.Append("    ,@LendCount");
   strSql.Append("    ,@AdjustCount");
   strSql.Append("   ,@BadCount");
   strSql.Append("   ,@CheckCount");
   strSql.Append("   ,@TakeOutCount");
   strSql.Append("    ,@SendOutCount");
   strSql.Append("    ,@InTotal");
   strSql.Append("    ,@OutTotal");
   strSql.Append("    ,@TodayCount");
   strSql.Append("     ,@SaleFee");
   strSql.Append("     ,@SaleBackFee");
   strSql.Append("      ,@PhurFee");
   if (!string.IsNullOrEmpty(model.PhurBackFee))
   {
       strSql.Append("     ,@PhurBackFee");
   }
  // strSql.Append("     ,@Remark");
   strSql.Append("  ,@CreateDate");
   strSql.Append("    ,@Creator)");

        SqlCommand comm = new SqlCommand();
        comm.CommandText = strSql.ToString();
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate ", model.DailyDate));//编号
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));
        if (!string.IsNullOrEmpty(model.BatchNo))
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//
        }
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitInCount ", model.InitInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitBatchCount ", model.InitBatchCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@PhurInCount ", model.PhurInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@MakeInCount ", model.MakeInCount));//
       // comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleBackInCount ", model.SaleBackInCount));
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubSaleBackInCount ", model.SubSaleBackInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@RedInCount ", model.RedInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherInCount ", model.OtherInCount));//

        comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackInCount ", model.BackInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeInCount ", model.TakeInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispInCount ", model.DispInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendInCount ", model.SendInCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleOutCount ", model.SaleOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubSaleOutCount ", model.SubSaleOutCount));
        if (!string.IsNullOrEmpty(model.PhurBackOutCount))
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PhurBackOutCount ", model.PhurBackOutCount));//
        }
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@RedOutCount ", model.RedOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherOutCount ", model.OtherOutCount));//

        comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispOutCount ", model.DispOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@LendCount ", model.LendCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdjustCount ", model.AdjustCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@BadCount ", model.BadCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckCount ", model.CheckCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@TakeOutCount ", model.TakeOutCount));
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendOutCount ", model.SendOutCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@InTotal ", model.InTotal));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutTotal ", model.OutTotal));//

        comm.Parameters.Add(SqlHelper.GetParameterFromString("@TodayCount ", model.TodayCount));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleFee ", model.SaleFee));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleBackFee ", model.SaleBackFee));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@PhurFee ", model.PhurFee));
        if (!string.IsNullOrEmpty(model.PhurBackFee))
        {

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PhurBackFee ", model.PhurBackFee));//
        }
        //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate ", model.CreateDate));//
        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//
        return comm;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    enum itemsType
    {
        期初库存录入_1=1,
        期初库存批量导入_2,
        采购入库单_3,
        生产完工入库单_4,
        其他入库单_5,
        红冲入库单_6,
        销售出库单_7,
        其他出库单_8,
        红冲出库单_9,
        借货申请单_10,
        借货返还单_11,
        调拨出库_12,
        调拨入库_13,
        日常调整单_14,
        期末盘点单_15,
        库存报损单_16,
        领料单_17,
        退料单_18,
        配送单_19,
        配送退货单_20,
        门店销售管理_21,
        门店销售退货_22
        
    }
    
    

}