<%@ WebHandler Language="C#" Class="StorageBorrowList" %>

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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
public class StorageBorrowList : SubBaseHandler
{
    
     public override void ActionHandler(string action)
    {
        switch (action)
        {
            case "DELDETAIL":
                DelStorageDetail();
                break;
            default:
                GetBorrowList();
                break;
        }
         
    }
    
    
     protected void DelStorageDetail()
     {
         string[] IDList = GetRequestForm("IDList", false).Split(',');
         OutputResult(StorageBorrowBus.DelStorageDetails(IDList), "");
     }
    
    
    public void  GetBorrowList()
    {
        StorageBorrow borrow = new StorageBorrow();

        borrow.BorrowNo = string.IsNullOrEmpty(GetRequestForm("BorrowNo", false)) ? string.Empty : ("%" + GetRequestForm("BorrowNo", false)+"%");
        string EFIndex = GetRequestForm("EFIndex", false);
        string EFDesc = string.IsNullOrEmpty(GetRequestForm("EFDesc", false)) ? string.Empty : ("%" + GetRequestForm("EFDesc", false) + "%"); 
        //context.Request.Form["BorrowNo"] == "" ? string.Empty : ("%" + context.Request.Form["BorrowNo"].ToString() + "%");
        borrow.Title = string.IsNullOrEmpty(GetRequestForm("Title", false)) ? string.Empty : ("%" + GetRequestForm("Title", false) + "%"); //context.Request.Form["Title"] == "" ? string.Empty : ("%" + context.Request.Form["Title"].ToString() + "%");
        borrow.Borrower = Convert.ToInt32(GetRequestForm("Borrower", true));// Convert.ToInt32(context.Request.Form["Borrower"] == "" ? "-1" : context.Request.Form["Borrower"].ToString());
        borrow.DeptID = Convert.ToInt32(GetRequestForm("DeptID", true));// Convert.ToInt32(context.Request.Form["DeptID"] == "" ? "-1" : context.Request.Form["DeptID"].ToString());
        borrow.OutDeptID = Convert.ToInt32(GetRequestForm("OutDeptID", true));// Convert.ToInt32(context.Request.Form["OutDeptID"] == "" ? "-1" : context.Request.Form["OutDeptID"].ToString());
        borrow.StorageID = Convert.ToInt32(GetRequestForm("StorageID", true));// Convert.ToInt32(context.Request.Form["StorageID"] == "" ? "-1" : context.Request.Form["StorageID"].ToString());
        borrow.Transactor = Convert.ToInt32(GetRequestForm("Transactor", true));// Convert.ToInt32(context.Request.Form["Transactor"] == "" ? "-1" : context.Request.Form["Transactor"].ToString());
        borrow.BillStatus = GetRequestForm("BillStatus", false);// context.Request.Form["BillStatus"] == null ? string.Empty : context.Request.Form["BillStatus"].ToString();
        borrow.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(GetRequestForm("StartTime", false)) ? DateTime.MinValue.ToString() : GetRequestForm("StartTime", false));
        DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(GetRequestForm("EndTime", false)) ? DateTime.MinValue.ToString() : GetRequestForm("EndTime", false));
        int SubmitStatus = Convert.ToInt32(GetRequestForm("SubmitStatus", true));// Convert.ToInt32(context.Request.Form["SubmitStatus"] == null ? "-1" : context.Request.Form["SubmitStatus"].ToString());

        string orderString = (string.IsNullOrEmpty(GetRequestForm("orderby", false)) ? string.Empty : GetRequestForm("orderby", false));//排序
        string order = "DESC";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        
        
        
        if (orderString.EndsWith("_a"))
        {
            order = "ASC";//排序：降序
        }

        orderBy = orderBy + " " + order;

        int pageCount = Convert.ToInt32(GetRequestForm("pageCount", true));// int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = Convert.ToInt32(GetRequestForm("pageIndex", true));// int.Parse(context.Request.Form["pageIndex"].ToString());//当前页

        int TotalCount = 0;
      //  OutputDataTable(

        DataTable dt = XBase.Business.Office.StorageManager.StorageBorrowBus.GetStorageList(EFIndex, EFDesc,pageIndex, pageCount, orderBy, ref TotalCount, borrow, start, end, SubmitStatus);
          OutputDataTable(dt, TotalCount);
        
      
    }

}