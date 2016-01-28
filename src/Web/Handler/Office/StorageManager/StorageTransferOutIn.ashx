<%@ WebHandler Language="C#" Class="StorageTransferOutIn" %>

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
using System.Collections.Generic;

using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StorageTransferOutIn : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public UserInfoUtil UserInfo
    {
        get { return (UserInfoUtil)SessionUtil.Session["UserInfo"]; }
    }

    public void ProcessRequest (HttpContext context)
    {
        string action=string.Empty;
        if (context.Request.Form["action"] != null)
            action = context.Request.Form["action"];
        if (action == "SAVE")
        {
               Out(context);
        }
        else if (action == "IN")
        {
            In(context);
        }
    }


    protected void In(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();

        st.BusiStatus = "4";
        st.OutCount = Convert.ToDecimal(FormatRequest(context, "OutCountTotal", true));
        st.OutUserID = Convert.ToInt32(FormatRequest(context, "OutUserID", true));
        st.OutFeeSum = Convert.ToDecimal(FormatRequest(context, "OutFeeSum", true));
        st.ApplyDeptID = Convert.ToInt32(FormatRequest(context, "ApplyDeptID", true));
        st.ApplyUserID = Convert.ToInt32(FormatRequest(context, "ApplyUserID", true));
        st.CompanyCD = UserInfo.CompanyCD;
        st.Creator = UserInfo.EmployeeID;
        st.InStorageID = Convert.ToInt32(FormatRequest(context, "InStorageID", true));
        st.ModifiedDate = DateTime.Now;
        st.ModifiedUserID = UserInfo.EmployeeName;
        st.ReasonType = Convert.ToInt32(FormatRequest(context, "ReasonType", true));
        st.Remark = FormatRequest(context, "Remark", false);
        st.RequireInDate = Convert.ToDateTime(FormatRequest(context, "RequireInDate", false));
        st.Title = FormatRequest(context, "Title", false);
        st.TransferCount = Convert.ToDecimal(FormatRequest(context, "TotalCount", false));
        st.OutStorageID = Convert.ToInt32(FormatRequest(context, "OutStorageID", true));
        st.OutDeptID = Convert.ToInt32(FormatRequest(context, "OutDeptID", true));
        st.TransferNo = FormatRequest(context, "TransferNo", false);
        st.TransferPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", false));
        st.Summary = FormatRequest(context, "Summary", false);
        st.ID = Convert.ToInt32(FormatRequest(context, "TransferID", true));
        st.InCount = Convert.ToDecimal(FormatRequest(context, "InCountTotal", true));
        st.InDate = Convert.ToDateTime(FormatRequest(context, "InDate", false));
        st.InFeeSum = Convert.ToDecimal(FormatRequest(context, "InFeeSum", true));
        st.InUserID = Convert.ToInt32(FormatRequest(context, "InUserID", true));

        string[] SortNo = FormatRequest(context, "SortNo", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] ProductSpec = FormatRequest(context, "ProductSpec", false).Split(',');
        string[] ProductUnit = FormatRequest(context, "ProductUnit", true).Split(',');
        string[] TransferPrice = FormatRequest(context, "TransferPrice", false).Split(',');
        string[] TransferCount = FormatRequest(context, "TransferCount", false).Split(',');
        string[] TransferTotalPrice = FormatRequest(context, "TransferTotalPrice", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');
        string[] OutCount = FormatRequest(context, "OutCount", false).Split(',');
        string[] OutPriceTotal = FormatRequest(context, "OutPriceTotal", false).Split(',');
        string[] InCount = FormatRequest(context, "InCount", false).Split(',');
        string[] InPriceTotal = FormatRequest(context, "InPriceTotal", false).Split(',');
        
        StorageTransferDetail std1=new StorageTransferDetail();
        std1.CompanyCD = UserInfo.CompanyCD;
        std1.TransferNo = st.TransferNo;

        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferDetailInfo(std1);
        
        
        
        List<StorageTransferDetail> stdList = new List<StorageTransferDetail>();
        for (int i = 0; i < dt.Rows.Count ; i++)
        {
            dt.Rows[i]["InCount"] = Convert.ToDecimal(InCount[i]);
            dt.Rows[i]["InPriceTotal"] = Convert.ToDecimal(InPriceTotal[i]);

        }

        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.StorageTransferIn(st, dt, UserInfo.IsBatch));
    }
    
    
    
  protected void Out(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();
       
        st.BusiStatus = "3";
        st.OutDate = Convert.ToDateTime(FormatRequest(context, "OutDate", false));
        st.OutCount = Convert.ToDecimal(FormatRequest(context, "OutCountTotal", true));
        st.OutUserID = Convert.ToInt32(FormatRequest(context, "OutUserID", true));
        st.OutFeeSum = Convert.ToDecimal(FormatRequest(context, "OutFeeSum", true));
        st.ApplyDeptID = Convert.ToInt32(FormatRequest(context, "ApplyDeptID", true));
        st.ApplyUserID = Convert.ToInt32(FormatRequest(context, "ApplyUserID", true));
        st.CompanyCD = UserInfo.CompanyCD;
        st.Creator = UserInfo.EmployeeID;
        st.InStorageID = Convert.ToInt32(FormatRequest(context, "InStorageID", true));
        st.ModifiedDate = DateTime.Now;
        st.ModifiedUserID = UserInfo.EmployeeName;
        st.ReasonType = Convert.ToInt32(FormatRequest(context, "ReasonType", true));
        st.Remark = FormatRequest(context, "Remark", false);
        st.RequireInDate = Convert.ToDateTime(FormatRequest(context, "RequireInDate", false));
        st.Title = FormatRequest(context, "Title", false);
        st.TransferCount = Convert.ToDecimal(FormatRequest(context, "TotalCount", false));
        st.OutStorageID = Convert.ToInt32(FormatRequest(context, "OutStorageID", true));
        st.OutDeptID = Convert.ToInt32(FormatRequest(context, "OutDeptID", true));
        st.TransferNo = FormatRequest(context, "TransferNo", false);
        st.TransferPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", false));
        st.Summary = FormatRequest(context, "Summary", false);
        st.ID = Convert.ToInt32(FormatRequest(context, "TransferID", true));
      
        string[] SortNo = FormatRequest(context, "SortNo", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] ProductSpec = FormatRequest(context, "ProductSpec", false).Split(',');
        string[] ProductUnit = FormatRequest(context, "ProductUnit", true).Split(',');
        string[] TransferPrice = FormatRequest(context, "TransferPrice", false).Split(',');
        string[] TransferCount = FormatRequest(context, "TransferCount", false).Split(',');
        string[] TransferTotalPrice = FormatRequest(context, "TransferTotalPrice", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');
        string[] OutCount = FormatRequest(context, "OutCount", false).Split(',');
        string[] OutPriceTotal = FormatRequest(context, "OutPriceTotal", false).Split(',');


        StorageTransferDetail std1 = new StorageTransferDetail();
        std1.CompanyCD = UserInfo.CompanyCD;
        std1.TransferNo = st.TransferNo;

        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferDetailInfo(std1);
        
      
      
        List<StorageTransferDetail> stdList = new List<StorageTransferDetail>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["OutCount"] = Convert.ToDecimal(OutCount[i]);
            dt.Rows[i]["OutPriceTotal"] = Convert.ToDecimal(OutPriceTotal[i]);
            

        }

        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.StorageTransferOut(st, dt,UserInfo.IsBatch));
  }

  protected string FormatRequest(HttpContext context, string key, bool IsNum)
  {
      if (context.Request.Form[key] == null)
      {
          if (IsNum)
              return "-1";
          else
              return string.Empty;
      }
      else
      {
          if (string.IsNullOrEmpty(context.Request.Form[key].ToString()))
              if (IsNum)
                  return "-1";
              else
                  return string.Empty;
          else
              return context.Request.Form[key].ToString();
      }
  }


  //判断节点是否存在 及格式化日期
  protected string GetElementValue(XElement x, string Key, bool IsDate)
  {
      if (x.Element(Key) != null)
      {
          string tempValue = x.Element(Key).Value;
          if (IsDate)
          {
              if (string.IsNullOrEmpty(tempValue))
              {
                  return string.Empty;
              }
              else
                  return Convert.ToDateTime(tempValue).ToString("yyyy-MM-dd");
          }
          else
              return x.Element(Key).Value;
      }
      else
          return string.Empty;
  }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}