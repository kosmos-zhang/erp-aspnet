<%@ WebHandler Language="C#" Class="StroageBorrowBillStatus" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;


using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StroageBorrowBillStatus : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    { 

        //   var Para="status="+id+"&BorrowID="+document.getElementById("borrowid").value+"&BorrowNo="+document.getElementById("tboxBorrowNo").value;

        StorageBorrow borrow = new StorageBorrow();
        borrow.BillStatus = context.Request.Form["status"].ToString();
        borrow.ID = Convert.ToInt32(context.Request.Form["BorrowID"].ToString());
        borrow.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        borrow.BorrowNo = context.Request.Form["BorrowNo"].ToString();
        //string t = context.Request.Form["DeptID"].ToString();
        borrow.DeptID = Convert.ToInt32(context.Request.Form["DeptID"].ToString() == "" ? "-1" : context.Request.Form["DeptID"].ToString());
        borrow.ModifiedDate = DateTime.Now;
        borrow.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        borrow.CloseDate = DateTime.Now;
        borrow.Closer = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        borrow.ConfirmDate = DateTime.Now;
        borrow.Confirmor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        borrow.StorageID = Convert.ToInt32(context.Request.Form["StorageID"].ToString());
        context.Response.Write(XBase.Business.Office.StorageManager.StorageBorrowBus.SetBillStatus(borrow));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}