<%@ WebHandler Language="C#" Class="StorageTransferSave" %>

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

public class StorageTransferSave : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public UserInfoUtil UserInfo
    {
        get { return (UserInfoUtil)SessionUtil.Session["UserInfo"]; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"];
        if (action == "GETPUT")
        {
            GetProductList(context);
        }
        else if (action == "ADD")
        {
            Add(context);
        }
        else if (action == "EDIT")
        {
            Edit(context);
        }
        else if (action == "STA")
        {
            UpdateStatus(context);
        }
        else if (action == "GET")
        {
            GetBaseInfo(context);
        }
        else if (action == "GETDTL")
        {
            GetDetailInfo(context);
        }

    }


    protected void GetBaseInfo(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();
        st.ID = Convert.ToInt32(context.Request.Form["TransferID"].ToString());
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferInfo(st);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new StroageTransferDataSourceModel()
          {
              ApplyDeptID = GetElementValue(x, "ApplyDeptID", false),
              ApplyDeptIDName = GetElementValue(x, "ApplyDeptIDName", false),
              ApplyUserID = GetElementValue(x, "ApplyUserID", false),
              ApplyUserIDName = GetElementValue(x, "ApplyUserIDName", false),
              BillStatus = GetElementValue(x, "BillStatus", false),
              BusiStatus = GetElementValue(x, "BusiStatus", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              CloserName = GetElementValue(x, "CloserName", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              ConfirmorName = GetElementValue(x, "ConfirmorName", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              CreatorName = GetElementValue(x, "CreatorName", false),
              ID = GetElementValue(x, "ID", false),
              InStorageID = GetElementValue(x, "InStorageID", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              OutDate = GetElementValue(x, "OutDate", true),
              OutDeptID = GetElementValue(x, "OutDeptID", false),
              OutDeptIDName = GetElementValue(x, "OutDeptIDName", false),
              OutStorageID = GetElementValue(x, "OutStorageID", false),
              ReasonType = GetElementValue(x, "ReasonType", false),
              Remark = GetElementValue(x, "Remark", false),
              RequireInDate = GetElementValue(x, "RequireInDate", true),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              TransferCount = GetElementValue(x, "TransferCount", false),
              TransferFeeSum = GetElementValue(x, "TransferFeeSum", false),
              TransferNo = GetElementValue(x, "TransferNo", false),
              TransferPrice = GetElementValue(x, "TransferPrice", false),
              InCount = GetElementValue(x, "InCount", false),
              InDate = GetElementValue(x, "InDate", true),
              InFeeSum = GetElementValue(x, "InFeeSum", false),
              InUserID = GetElementValue(x, "InUserID", false),
              InUserIDName = GetElementValue(x, "InUserIDName", false),
              OutCount = GetElementValue(x, "OutCount", false),
              OutFeeSum = GetElementValue(x, "OutFeeSum", false),
              OutUserID = GetElementValue(x, "OutUserID", false),
              OutUserIDName = GetElementValue(x, "OutUserIDName", false),
              ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField10", false)

          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new StroageTransferDataSourceModel()
          {
              ApplyDeptID = GetElementValue(x, "ApplyDeptID", false),
              ApplyDeptIDName = GetElementValue(x, "ApplyDeptIDName", false),
              ApplyUserID = GetElementValue(x, "ApplyUserID", false),
              ApplyUserIDName = GetElementValue(x, "ApplyUserIDName", false),
              BillStatus = GetElementValue(x, "BillStatus", false),
              BusiStatus = GetElementValue(x, "BusiStatus", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              CloserName = GetElementValue(x, "CloserName", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              ConfirmorName = GetElementValue(x, "ConfirmorName", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              CreatorName = GetElementValue(x, "CreatorName", false),
              ID = GetElementValue(x, "ID", false),
              InStorageID = GetElementValue(x, "InStorageID", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              OutDate = GetElementValue(x, "OutDate", true),
              OutDeptID = GetElementValue(x, "OutDeptID", false),
              OutDeptIDName = GetElementValue(x, "OutDeptIDName", false),
              OutStorageID = GetElementValue(x, "OutStorageID", false),
              ReasonType = GetElementValue(x, "ReasonType", false),
              Remark = GetElementValue(x, "Remark", false),
              RequireInDate = GetElementValue(x, "RequireInDate", true),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              TransferCount = GetElementValue(x, "TransferCount", false),
              TransferFeeSum = GetElementValue(x, "TransferFeeSum", false),
              TransferNo = GetElementValue(x, "TransferNo", false),
              TransferPrice = GetElementValue(x, "TransferPrice", false),
              InCount = GetElementValue(x, "InCount", false),
              InDate = GetElementValue(x, "InDate", true),
              InFeeSum = GetElementValue(x, "InFeeSum", false),
              InUserID = GetElementValue(x, "InUserID", false),
              InUserIDName = GetElementValue(x, "InUserIDName", false),
              OutCount = GetElementValue(x, "OutCount", false),
              OutFeeSum = GetElementValue(x, "OutFeeSum", false),
              OutUserID = GetElementValue(x, "OutUserID", false),
              OutUserIDName = GetElementValue(x, "OutUserIDName", false),
                ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField10", false)

          });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.ToList()));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    protected void GetDetailInfo(HttpContext context)
    {
        StorageTransferDetail std = new StorageTransferDetail();
        std.TransferNo = FormatRequest(context, "TransferNo", false);
        std.CompanyCD = UserInfo.CompanyCD;
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetStorageTransferDetailInfo(std);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new StorageTransferDetailSourceModel()
          {
              CompanyCD = GetElementValue(x, "CompanyCD", false),
              ID = GetElementValue(x, "ID", false),
              ProdNo = GetElementValue(x, "ProdNo", false),
              ProductID = GetElementValue(x, "ProductID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              Remark = GetElementValue(x, "Remark", false),
              SortNo = GetElementValue(x, "SortNo", false),
              Specification = GetElementValue(x, "Specification", false),
              TranCount = GetElementValue(x, "TranCount", false),
              TranPrice = GetElementValue(x, "TranPrice", false),
              TranPriceTotal = GetElementValue(x, "TranPriceTotal", false),
              TransferNo = GetElementValue(x, "TransferNo", false),
              UnitName = GetElementValue(x, "UnitName", false),
              UnitID = GetElementValue(x, "UnitID", false),
              MinusIs = GetElementValue(x, "MinusIs", false),
              UseCount = GetElementValue(x, "UseCount", false),
              InCount = GetElementValue(x, "InCount", false),
              InPriceTotal = GetElementValue(x, "InPriceTotal", false),
              OutCount = GetElementValue(x, "OutCount", false),
              OutPriceTotal = GetElementValue(x, "OutPriceTotal", false),
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
              UsedPrice = GetElementValue(x, "UsedPrice", false),
              ExRate = GetElementValue(x, "ExRate", false),
              BatchNo=GetElementValue(x,"BatchNo",false),
              UsedUnitName=GetElementValue(x,"UsedUnitName",false)
          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new StorageTransferDetailSourceModel()
          {
              CompanyCD = GetElementValue(x, "CompanyCD", false),
              ID = GetElementValue(x, "ID", false),
              ProdNo = GetElementValue(x, "ProdNo", false),
              ProductID = GetElementValue(x, "ProductID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              Remark = GetElementValue(x, "Remark", false),
              SortNo = GetElementValue(x, "SortNo", false),
              Specification = GetElementValue(x, "Specification", false),
              TranCount = GetElementValue(x, "TranCount", false),
              TranPrice = GetElementValue(x, "TranPrice", false),
              TranPriceTotal = GetElementValue(x, "TranPriceTotal", false),
              TransferNo = GetElementValue(x, "TransferNo", false),
              UnitName = GetElementValue(x, "UnitName", false),
              UnitID = GetElementValue(x, "UnitID", false),
              MinusIs = GetElementValue(x, "MinusIs", false),
              UseCount = GetElementValue(x, "UseCount", false),
              InCount = GetElementValue(x, "InCount", false),
              InPriceTotal = GetElementValue(x, "InPriceTotal", false),
              OutCount = GetElementValue(x, "OutCount", false),
              OutPriceTotal = GetElementValue(x, "OutPriceTotal", false),
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),
              UsedPrice = GetElementValue(x, "UsedPrice", false),
              ExRate = GetElementValue(x, "ExRate", false),
              BatchNo = GetElementValue(x, "BatchNo", false),
              UsedUnitName = GetElementValue(x, "UsedUnitName", false)

          });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.ToList()));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
    }



    public class StorageTransferDetailSourceModel
    {
        public string ID { get; set; }
        public string CompanyCD { get; set; }
        public string TransferNo { set; get; }
        public string SortNo { set; get; }
        public string ProductName { set; get; }
        public string ProdNo { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string TranPrice { get; set; }
        public string TranCount { get; set; }
        public string TranPriceTotal { get; set; }
        public string Remark { get; set; }
        public string ProductID { get; set; }
        public string MinusIs { get; set; }
        public string UseCount { get; set; }
        public string InCount { get; set; }
        public string OutCount { get; set; }
        public string OutPriceTotal { get; set; }
        public string InPriceTotal { get; set; }
        public string UsedUnitID { get; set; }
        public string UsedUnitCount { get; set; }
        public string UsedPrice { get; set; }
        public string ExRate { get; set; }
        public string BatchNo { get; set; }
        public string UsedUnitName { get; set; }
    }



    public class StroageTransferDataSourceModel
    {
        public string ID { get; set; }
        public string TransferNo { get; set; }
        public string Title { get; set; }
        public string ApplyUserID { get; set; }
        public string ApplyUserIDName { get; set; }
        public string ApplyDeptID { get; set; }
        public string ApplyDeptIDName { get; set; }
        public string InStorageID { get; set; }
        public string RequireInDate { get; set; }
        public string ReasonType { get; set; }
        public string OutDeptID { get; set; }
        public string OutDeptIDName { get; set; }
        public string OutStorageID { get; set; }
        public string OutDate { get; set; }
        public string BusiStatus { get; set; }
        public string Summary { get; set; }
        public string TransferCount { get; set; }
        public string TransferPrice { get; set; }
        public string TransferFeeSum { get; set; }
        public string Creator { get; set; }
        public string CreatorName { get; set; }
        public string CreateDate { get; set; }
        public string BillStatus { get; set; }
        public string Remark { get; set; }
        public string ConfirmorName { get; set; }
        public string ConfirmDate { get; set; }
        public string Closer { get; set; }
        public string CloserName { get; set; }
        public string CloseDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { set; get; }
        public string OutFeeSum { get; set; }
        public string InFeeSum { get; set; }
        public string OutCount { get; set; }
        public string InCount { get; set; }
        public string OutUserID { get; set; }
        public string OutUserIDName { get; set; }
        public string InUserID { get; set; }
        public string InDate { get; set; }
        public string InUserIDName { get; set; }
        public string ExtField1 { get; set; }
        public string ExtField2 { get; set; }
        public string ExtField3 { get; set; }
        public string ExtField4 { get; set; }
        public string ExtField5 { get; set; }
        public string ExtField6 { get; set; }
        public string ExtField7 { get; set; }
        public string ExtField8 { get; set; }
        public string ExtField9 { get; set; }
        public string ExtField10 { get; set; }
    }







    protected void UpdateStatus(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();
        st.ModifiedDate = DateTime.Now;
        st.ModifiedUserID = UserInfo.UserID;
        int stype = Convert.ToInt32(FormatRequest(context, "type", true));
        st.BusiStatus = "2";
        if (stype == 1 || stype == 3)
        {
            st.BillStatus = "2";
        }
        else if (stype == 2)
            st.BillStatus = "4";
        else if (stype == 4)
        {
            st.BillStatus = "1";
            st.BusiStatus = "1";
        }
        st.Confirmor = UserInfo.EmployeeID;
        st.ConfirmDate = DateTime.Now;
        st.Closer = UserInfo.EmployeeID;
        st.CloseDate = DateTime.Now;
        st.CompanyCD = UserInfo.CompanyCD;
        st.TransferNo = FormatRequest(context, "TransferNo", false);
        st.ID = Convert.ToInt32(FormatRequest(context, "ID", true));
        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.UpdateStorageTransfer(stype, st));
    }
    protected void Edit(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();
        st.ApplyDeptID = Convert.ToInt32(FormatRequest(context, "ApplyDeptID", true));
        st.ApplyUserID = Convert.ToInt32(FormatRequest(context, "ApplyUserID", true));
        st.ID = Convert.ToInt32(FormatRequest(context, "ID", true));
        st.BillStatus = "1";
        st.BusiStatus = "1";
        st.CompanyCD = UserInfo.CompanyCD;
        st.Creator = UserInfo.EmployeeID;
        st.InStorageID = Convert.ToInt32(FormatRequest(context, "InStorageID", true));
        st.ModifiedDate = DateTime.Now;
        st.ModifiedUserID = UserInfo.UserID;
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
        
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        string[] SortNo = FormatRequest(context, "SortNo", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] ProductSpec = FormatRequest(context, "ProductSpec", false).Split(',');
        string[] ProductUnit = FormatRequest(context, "ProductUnit", true).Split(',');
        string[] TransferPrice = FormatRequest(context, "TransferPrice", false).Split(',');
        string[] TransferCount = FormatRequest(context, "TransferCount", false).Split(',');
        string[] TransferTotalPrice = FormatRequest(context, "TransferTotalPrice", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');
        string[] UsedUnitID = FormatRequest(context, "UsedUnitID", false).Split(',');
        string[] UsedUnitCount = FormatRequest(context, "UsedCount", false).Split(',');
        string[] UsedPrice = FormatRequest(context, "UsedPrice", false).Split(',');
        string[] ExRate = FormatRequest(context, "ExRate", false).Split(',');
        string[] BatchNo = FormatRequest(context, "BatchNo", false).Split(',');
        st.TransferFeeSum = Convert.ToDecimal(FormatRequest(context, "TransferFeeSum", true) == "-1" ? "0" : FormatRequest(context, "TransferFeeSum", true));
        List<StorageTransferDetail> stdList = new List<StorageTransferDetail>();
        for (int i = 0; i < SortNo.Length; i++)
        {
            StorageTransferDetail std = new StorageTransferDetail();
            std.CompanyCD = UserInfo.CompanyCD;
            std.ModifiedDate = DateTime.Now;
            std.ModifiedUserID = UserInfo.EmployeeName;
            std.SortNo = Convert.ToInt32(SortNo[i]);
            std.ProductID = Convert.ToInt32(ProductID[i]);
            std.Remark = RemarkList[i];
            std.TranCount = Convert.ToDecimal(TransferCount[i]);
            std.TranPrice = Convert.ToDecimal(TransferPrice[i]);
            std.TranPriceTotal = Convert.ToDecimal(TransferTotalPrice[i]);
            std.TransferNo = st.TransferNo;
            //hm edit at 090927
            if (!string.IsNullOrEmpty(ProductUnit[i]))
                std.UnitID = Convert.ToInt32(ProductUnit[i]);
            else
                std.UnitID = null;
            //end
            //std.UnitID = Convert.ToInt32(ProductUnit[i]);
            
            //计量单位
            std.UsedUnitID = Convert.ToInt32(UsedUnitID[i]);
            std.UsedUnitCount = Convert.ToDecimal(UsedUnitCount[i]);
            std.UsedPrice = Convert.ToDecimal(UsedPrice[i]);
            std.ExRate = Convert.ToDecimal(ExRate[i]);
            std.BatchNo = BatchNo[i];
            
            stdList.Add(std);

        }

        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.UpdateStorageTransfer(st, stdList,ht));
    }



    protected void Add(HttpContext context)
    {
        StorageTransfer st = new StorageTransfer();
        st.ApplyDeptID = Convert.ToInt32(FormatRequest(context, "ApplyDeptID", true));
        st.ApplyUserID = Convert.ToInt32(FormatRequest(context, "ApplyUserID", true));
        st.BillStatus = "1";
        st.BusiStatus = "1";
        st.CompanyCD = UserInfo.CompanyCD;
        st.Creator = UserInfo.EmployeeID;
        st.InStorageID = Convert.ToInt32(FormatRequest(context, "InStorageID", true));
        st.ModifiedDate = DateTime.Now;
        st.ModifiedUserID = UserInfo.UserID;
        st.ReasonType = Convert.ToInt32(FormatRequest(context, "ReasonType", true));
        st.Remark = FormatRequest(context, "Remark", false);
        st.RequireInDate = Convert.ToDateTime(FormatRequest(context, "RequireInDate", false));
        st.Title = FormatRequest(context, "Title", false);
        st.TransferCount = Convert.ToDecimal(FormatRequest(context, "TotalCount", false));
        st.OutStorageID = Convert.ToInt32(FormatRequest(context, "OutStorageID", true));
        st.OutDeptID = Convert.ToInt32(FormatRequest(context, "OutDeptID", true));
        st.TransferFeeSum = Convert.ToDecimal(FormatRequest(context, "TransferFeeSum", true) == "-1" ? "0" : FormatRequest(context, "TransferFeeSum", true));
        if (FormatRequest(context, "bmgz", false) == "zd")
        {
            st.TransferNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(FormatRequest(context, "TransferNo", false));
            if (st.TransferNo == "")
            {
                context.Response.Write("3|该单据编号规则自动生成的序号已达到上限，请检查编号规则设置!");
                return; 
            }
        }
        else
            st.TransferNo = FormatRequest(context, "TransferNo", false);
        if (!XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq("StorageTransfer", "TransferNo", st.TransferNo))
        {
            context.Response.Write("2|该编号已被使用，请输入未使用的编号！");
            return;
        }
        st.TransferPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", false));
        st.CreateDate = DateTime.Now;
        st.Summary = FormatRequest(context, "Summary", false);
        string[] SortNo = FormatRequest(context, "SortNo", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] ProductSpec = FormatRequest(context, "ProductSpec", false).Split(',');
        string[] ProductUnit = FormatRequest(context, "ProductUnit", true).Split(',');
        string[] TransferPrice = FormatRequest(context, "TransferPrice", false).Split(',');
        string[] TransferCount = FormatRequest(context, "TransferCount", false).Split(',');
        string[] TransferTotalPrice = FormatRequest(context, "TransferTotalPrice", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');
        string[] UsedUnitID = FormatRequest(context, "UsedUnitID", false).Split(',');
        string[] UsedUnitCount = FormatRequest(context, "UsedCount", false).Split(',');
        string[] UsedPrice = FormatRequest(context, "UsedPrice", false).Split(',');
        string[] ExRate = FormatRequest(context, "ExRate", false).Split(',');
        string[] BatchNo = FormatRequest(context, "BatchNo", false).Split(',');
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        List<StorageTransferDetail> stdList = new List<StorageTransferDetail>();
        for (int i = 0; i < SortNo.Length; i++)
        {
            StorageTransferDetail std = new StorageTransferDetail();
            std.CompanyCD = UserInfo.CompanyCD;
            std.ModifiedDate = DateTime.Now;
            std.ModifiedUserID = UserInfo.UserID;
            std.SortNo = Convert.ToInt32(SortNo[i]);
            std.ProductID = Convert.ToInt32(ProductID[i]);
            std.Remark = RemarkList[i];
            std.TranCount = Convert.ToDecimal(TransferCount[i]);
            std.TranPrice = Convert.ToDecimal(TransferPrice[i]);
            std.TranPriceTotal = Convert.ToDecimal(TransferTotalPrice[i]);
            std.TransferNo = st.TransferNo;
            //hm edit at 090927
            if (!string.IsNullOrEmpty(ProductUnit[i]))
                std.UnitID = Convert.ToInt32(ProductUnit[i]);
            else
                std.UnitID = null;
            //end
            //std.UnitID = Convert.ToInt32(ProductUnit[i]);
            //计量单位
            std.UsedUnitID = Convert.ToInt32(UsedUnitID[i]);
            std.UsedUnitCount = Convert.ToDecimal(UsedUnitCount[i]);
            std.UsedPrice = Convert.ToDecimal(UsedPrice[i]);
            std.ExRate = Convert.ToDecimal(ExRate[i]);
            std.BatchNo = BatchNo[i];
            stdList.Add(std);

        }

        context.Response.Write(XBase.Business.Office.StorageManager.StorageTransferBus.AddStorageTransfer(st, stdList,ht));

    }



    protected void GetProductList(HttpContext context)
    {
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        int OutDeptID = Convert.ToInt32(FormatRequest(context, "OutDeptID", true));
        int OutStorageID = Convert.ToInt32(FormatRequest(context, "OutStorageID", true));

        ///*解析自定义属性参数*/.

        string EFIndex = "";
        string EFDesc = "";
        if (context.Request.Form["EFIndex"] != null && context.Request.Form["EFDesc"] != null)
        {
            EFIndex = context.Request.Form["EFIndex"].ToString().Trim();
            EFDesc = context.Request.Form["EFDesc"].ToString().Trim();
        }

        string orderString = (context.Request.Form["orderby"] == "" ? string.Empty : context.Request.Form["orderby"].ToString());//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProdNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

        string ProductName = context.Request.Form["ProductName"].ToString();
        string ProdNo = context.Request.Form["ProdNo"].ToString();

        DataTable dt = XBase.Business.Office.StorageManager.StorageTransferBus.GetProduct(userinfo.CompanyCD, OutDeptID, OutStorageID, ProductName, ProdNo, EFIndex, EFDesc);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
            (order == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby  GetElementValue(x, orderBy, false) ascending
             select new ProductDataSourceModel()
             {
                 ProdcutUnitName = GetElementValue(x, "CodeName", false),
                 ProductName = GetElementValue(x, "ProductName", false),
                 ProductNo = GetElementValue(x, "ProdNo", false),
                 ProductSpec = GetElementValue(x, "Specification", false),
                 ProductUnitID = GetElementValue(x, "UnitID", false),
                 TransferPrice = GetElementValue(x, "TransferPrice", false),
                 ProductCount = GetElementValue(x, "ProductCount", false),
                 ID = GetElementValue(x, "ProductID", false),
                 NowCount = GetElementValue(x, "NowCount", false),
                 IsBatchNo = GetElementValue(x, "IsBatchNo", false),
                 BatchNo = GetElementValue(x, "BatchNo", false),
                 StandardCost = GetElementValue(x, "StandardCost", false)
             })

                      :
            (from x in dsXML.Descendants("Data")
             orderby GetElementValue(x, orderBy, false) descending
             select new ProductDataSourceModel()
             {
                 ProdcutUnitName = GetElementValue(x, "CodeName", false),
                 ProductName = GetElementValue(x, "ProductName", false),
                 ProductNo = GetElementValue(x, "ProdNo", false),
                 ProductSpec = GetElementValue(x, "Specification", false),
                 ProductUnitID = GetElementValue(x, "UnitID", false),
                 TransferPrice = GetElementValue(x, "TransferPrice", false),
                 ProductCount = GetElementValue(x, "ProductCount", false),
                 ID = GetElementValue(x, "ProductID", false),
                 NowCount = GetElementValue(x, "NowCount", false),
                 IsBatchNo = GetElementValue(x, "IsBatchNo", false),
                 BatchNo = GetElementValue(x, "BatchNo", false),
                 StandardCost = GetElementValue(x, "StandardCost", false)
             });
        int totalCount = dsLinq.Count();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();

    }



    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }


    public class ProductDataSourceModel
    {
        public string ID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductSpec { get; set; }
        public string ProductUnitID { get; set; }
        public string ProdcutUnitName { get; set; }
        public string TransferPrice { get; set; }
        public string ProductCount { get; set; }
        public string NowCount { get; set; }
        public string IsBatchNo { get; set; }
        public string BatchNo { get; set; }
        public string StandardCost { get; set; }
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

    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext _context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = GetParam(_context, "keyList").Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), GetParam(_context, arrKey[y].Trim()).Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }
    /// <summary>
    /// 获取REQUEST的参数值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetParam(HttpContext _context, string key)
    {
        if (_context.Request[key] == null)
        {
            return string.Empty;
        }
        else
        {
            if (_context.Request[key].ToString().Trim() + "" == "")
            {
                return string.Empty;
            }
            else
            {
                return _context.Request[key].ToString().Trim();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}