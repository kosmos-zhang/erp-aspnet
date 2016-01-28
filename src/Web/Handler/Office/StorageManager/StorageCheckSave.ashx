<%@ WebHandler Language="C#" Class="StorageCheckSave" %>

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
public class StorageCheckSave : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public UserInfoUtil UserInfo
    {
        get { return (UserInfoUtil)SessionUtil.Session["UserInfo"]; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"];
        if (action == "ADD")
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
        else if (action == "SCK")
        {
            StorageCheck(context);
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
        StorageCheck sc = new StorageCheck();
        sc.ID = Convert.ToInt32(context.Request.Form["CheckID"].ToString());
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageCheckBus.StorageCheckGet(sc);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new StorageCheckDataSourceModel()
          {
              Attachment = GetElementValue(x, "Attachment", false),
              BillStatus = GetElementValue(x, "BillStatus", false),
              CheckCount = GetElementValue(x, "CheckCount", false),
              CheckDate = GetElementValue(x, "CheckDate", true),
              CheckMoney = GetElementValue(x, "CheckMoney", false),
              CheckNo = GetElementValue(x, "CheckNo", false),
              CheckType = GetElementValue(x, "CheckType", false),
              CheckUserID = GetElementValue(x, "CheckUserID", false),
              CheckUserName = GetElementValue(x, "CheckUserName", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              CloserName = GetElementValue(x, "CloserName", false),
              CompanyCD = GetElementValue(x, "CompanyCD", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              Confirmor = GetElementValue(x, "Confirmor", false),
              ConfirmorName = GetElementValue(x, "ConfirmorName", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              CreatorName = GetElementValue(x, "CreatorName", false),
              DeptID = GetElementValue(x, "DeptID", false),
              DeptName = GetElementValue(x, "DeptName", false),
              DiffMoney = GetElementValue(x, "DiffMoney", false),
              DiffCount = GetElementValue(x, "DiffCount", false),
              ID = GetElementValue(x, "ID", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              NowCount = GetElementValue(x, "NowCount", false),
              NowMoney = GetElementValue(x, "NowMoney", false),
              Remark = GetElementValue(x, "Remark", false),
              StorageID = GetElementValue(x, "StorageID", false),
              StorageName = GetElementValue(x, "StorageName", false),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              Transactor = GetElementValue(x, "Transactor", false),
              TransactorName = GetElementValue(x, "TransactorName", false),
              CheckEndDate = GetElementValue(x, "CheckEndDate", true),
              CheckStartDate = GetElementValue(x, "CheckStartDate", true),
              ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField1", false)
              

          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new StorageCheckDataSourceModel()
          {
              Attachment = GetElementValue(x, "Attachment", false),
              BillStatus = GetElementValue(x, "BillStatus", false),
              CheckCount = GetElementValue(x, "CheckCount", false),
              CheckDate = GetElementValue(x, "CheckDate", true),
              CheckMoney = GetElementValue(x, "CheckMoney", false),
              CheckNo = GetElementValue(x, "CheckNo", false),
              CheckType = GetElementValue(x, "CheckType", false),
              CheckUserID = GetElementValue(x, "CheckUserID", false),
              CheckUserName = GetElementValue(x, "CheckUserName", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              CloserName = GetElementValue(x, "CloserName", false),
              CompanyCD = GetElementValue(x, "CompanyCD", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              Confirmor = GetElementValue(x, "Confirmor", false),
              ConfirmorName = GetElementValue(x, "ConfirmorName", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              CreatorName = GetElementValue(x, "CreatorName", false),
              DeptID = GetElementValue(x, "DeptID", false),
              DeptName = GetElementValue(x, "DeptName", false),
              DiffMoney = GetElementValue(x, "DiffMoney", false),
              DiffCount = GetElementValue(x, "DiffCount", false),
              ID = GetElementValue(x, "ID", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              NowCount = GetElementValue(x, "NowCount", false),
              NowMoney = GetElementValue(x, "NowMoney", false),
              Remark = GetElementValue(x, "Remark", false),
              StorageID = GetElementValue(x, "StorageID", false),
              StorageName = GetElementValue(x, "StorageName", false),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              Transactor = GetElementValue(x, "Transactor", false),
              TransactorName = GetElementValue(x, "TransactorName", false),
              CheckEndDate = GetElementValue(x, "CheckEndDate", true),
              CheckStartDate = GetElementValue(x, "CheckStartDate", true),
              ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField1", false)



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

    public class StorageCheckDataSourceModel
    {
        public string ID { get; set; }
        public string CompanyCD { get; set; }
        public string CheckNo { get; set; }
        public string Title { get; set; }
        public string CheckDate { get; set; }
        public string StorageID { get; set; }
        public string StorageName { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string CheckType { get; set; }
        public string Transactor { get; set; }
        public string TransactorName { get; set; }
        public string NowCount { get; set; }
        public string CheckCount { get; set; }
        public string DiffCount { get; set; }
        public string NowMoney { get; set; }
        public string CheckMoney { get; set; }
        public string DiffMoney { get; set; }
        public string Summary { get; set; }
        public string Remark { get; set; }
        public string Attachment { get; set; }
        public string Creator { get; set; }
        public string CreatorName { get; set; }
        public string CreateDate { get; set; }
        public string BillStatus { get; set; }
        public string CheckUserID { get; set; }
        public string CheckUserName { get; set; }
        public string Confirmor { get; set; }
        public string ConfirmorName { get; set; }
        public string ConfirmDate { get; set; }
        public string Closer { get; set; }
        public string CloserName { get; set; }
        public string CloseDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string CheckStartDate { get; set; }        
        public string CheckEndDate { get; set; }
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


    protected void GetDetailInfo(HttpContext context)
    {
        StorageCheck sc = new StorageCheck();
        sc.CheckNo = context.Request.Form["CheckNo"].ToString();
        sc.CompanyCD = UserInfo.CompanyCD;
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "ascending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "SortNo";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageCheckBus.GetStorageCheckDetail(sc);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new StorageCheckDetailDataSourceModel()
          {
              CheckCount = GetElementValue(x, "CheckCount", false),
              DiffCount = GetElementValue(x, "DiffCount", false),
              DiffType = GetElementValue(x, "DiffType", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              NowCount = GetElementValue(x, "NowCount", false),
              ProdNo = GetElementValue(x, "ProdNo", false),
              ProductID = GetElementValue(x, "ProductID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              Remark = GetElementValue(x, "Remark", false),
              SortNo = GetElementValue(x, "SortNo", false),
              Specification = GetElementValue(x, "Specification", false),
              StandardCost = GetElementValue(x, "StandardCost", false),
              UnitID = GetElementValue(x, "UnitID", false),
              IsBatchNo = GetElementValue(x, "IsBatchNo", false),//
              BatchNo = GetElementValue(x, "BatchNo", false),//
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),//
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),//
              UsedPrice = GetElementValue(x, "UsedPrice", false),//
              ExRate = GetElementValue(x, "ExRate", false),//
              UnitName = GetElementValue(x, "UnitName", false)

          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new StorageCheckDetailDataSourceModel()
          {
              CheckCount = GetElementValue(x, "CheckCount", false),
              DiffCount = GetElementValue(x, "DiffCount", false),
              DiffType = GetElementValue(x, "DiffType", false),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUserID = GetElementValue(x, "ModifiedUserID", false),
              NowCount = GetElementValue(x, "NowCount", false),
              ProdNo = GetElementValue(x, "ProdNo", false),
              ProductID = GetElementValue(x, "ProductID", false),
              ProductName = GetElementValue(x, "ProductName", false),
              Remark = GetElementValue(x, "Remark", false),
              SortNo = GetElementValue(x, "SortNo", false),
              Specification = GetElementValue(x, "Specification", false),
              StandardCost = GetElementValue(x, "StandardCost", false),
              UnitID = GetElementValue(x, "UnitID", false),
              IsBatchNo = GetElementValue(x, "IsBatchNo", false),//
              BatchNo = GetElementValue(x, "BatchNo", false),//
              UsedUnitID = GetElementValue(x, "UsedUnitID", false),//
              UsedUnitCount = GetElementValue(x, "UsedUnitCount", false),//
              UsedPrice = GetElementValue(x, "UsedPrice", false),//
              ExRate = GetElementValue(x, "ExRate", false),//
              UnitName = GetElementValue(x, "UnitName", false)

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

    public class StorageCheckDetailDataSourceModel
    {
        public string SortNo { get; set; }
        public string ProductID { get; set; }
        public string UnitID { get; set; }
        public string NowCount { get; set; }
        public string CheckCount { get; set; }
        public string DiffCount { get; set; }
        public string DiffType { get; set; }
        public string Remark { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string ProdNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string StandardCost { get; set; }
        public string UnitName { get; set; }
        public string IsBatchNo { get; set; }//
        public string BatchNo { get; set; }//
        public string UsedUnitID { get; set; }//
        public string UsedUnitCount { get; set; }//
        public string UsedPrice { get; set; }//
        public string ExRate { get; set; }//
    }




    protected void StorageCheck(HttpContext context)
    {
        StorageCheck sc = new StorageCheck();
        sc.ID = Convert.ToInt32(FormatRequest(context, "ID", true));
        sc.CheckNo = FormatRequest(context, "CheckNo", false);
        sc.CompanyCD = UserInfo.CompanyCD;
        sc.CheckUserID = UserInfo.EmployeeID;
        sc.CloseDate = DateTime.Now;
        context.Response.Write(XBase.Business.Office.StorageManager.StorageCheckBus.StorageCheck(sc));
    }

    protected void UpdateStatus(HttpContext context)
    {
        StorageCheck sc = new StorageCheck();
        sc.ModifiedDate = DateTime.Now;
        sc.ModifiedUserID = UserInfo.EmployeeName;
        sc.CheckNo = FormatRequest(context, "CheckNo", false);
        int stype = Convert.ToInt32(FormatRequest(context, "type", true));
        if (stype == 1 || stype == 3)
        {
            sc.BillStatus = "2";
        }
        else if (stype == 2)
            sc.BillStatus = "4";
        else if (stype == 4)
        {
            sc.BillStatus = "1";
        }
        sc.Confirmor = UserInfo.EmployeeID;
        sc.ConfirmDate = DateTime.Now;
        sc.Closer = UserInfo.EmployeeID;
        sc.CloseDate = DateTime.Now;
        sc.ID = Convert.ToInt32(FormatRequest(context, "ID", true));
        sc.CompanyCD = UserInfo.CompanyCD;
        context.Response.Write(XBase.Business.Office.StorageManager.StorageCheckBus.UpdateStorageCheckStatus(stype, sc));
    }


    public void Add(HttpContext context)
    {
        StorageCheck sc = new StorageCheck();
        sc.Attachment = FormatRequest(context, "Attachment", false);
        sc.BillStatus = "1";
        sc.CheckCount = Convert.ToDecimal(FormatRequest(context, "CheckCount", true));
        sc.CheckEndDate = Convert.ToDateTime(FormatRequest(context, "CheckEndDate", false));
        sc.CheckMoney = Convert.ToDecimal(FormatRequest(context, "CheckMoney", false));
        if (FormatRequest(context, "bmgz", false) == "zd")
        {
            sc.CheckNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(FormatRequest(context, "CheckNo", false));
            if (string.IsNullOrEmpty(sc.CheckNo))
            {
                context.Response.Write("6|该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置");
                return;
            }
        }
        else
            sc.CheckNo = FormatRequest(context, "CheckNo", false);
        if (!XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq("StorageCheck", "CheckNo", sc.CheckNo))
        {
            context.Response.Write("2|改单据编号已经存在");
            return;
        }
        sc.CheckType = Convert.ToInt32(FormatRequest(context, "CheckType", true));
        sc.CheckStartkDate = Convert.ToDateTime(FormatRequest(context, "CheckStartDate", false));
        sc.CompanyCD = UserInfo.CompanyCD;
        sc.CreateDate = DateTime.Now;
        sc.Creator = UserInfo.EmployeeID;
        sc.DeptID = Convert.ToInt32(FormatRequest(context, "DeptID", true));
        sc.DiffMoney = Convert.ToDecimal(FormatRequest(context, "DiffMoney", true));
        sc.DiffCount = Convert.ToDecimal(FormatRequest(context, "DiffCount", true));
        sc.ModifiedDate = DateTime.Now;
        sc.ModifiedUserID = UserInfo.EmployeeName;
        sc.NowCount = Convert.ToDecimal(FormatRequest(context, "NowCount", true));
        sc.NowMoney = Convert.ToDecimal(FormatRequest(context, "NowMoney", true));
        sc.Remark = FormatRequest(context, "Remark", false);
        sc.StorageID = Convert.ToInt32(FormatRequest(context, "StorageID", true));
        sc.Summary = FormatRequest(context, "Summary", false);
        sc.Title = FormatRequest(context, "Title", false);
        sc.Transactor = Convert.ToInt32(FormatRequest(context, "Transactor", true));

        string DetailCostPrice = "";//实际单价
        string DetailBaseUnitID = "";//基本单位
        string UnitIDs = "";//实际单位
        string DetailBaseCount = "";//基本数量
        //string DetailBasePrice = "";//基本单价（没有存储）
        string DetailExtRate = "";//比率

        string CheckCounts = "";//实盘数量

        try
        {
            UnitIDs = context.Request.Form["UnitIDs"].ToString().Trim();
        }
        catch (Exception) { }//实际单位
        try { DetailBaseUnitID = context.Request.Form["DetailBaseUnitID"].ToString().Trim(); }
        catch (Exception) { }//基本单位
        try { DetailBaseCount = context.Request.Form["DetailBaseCount"].ToString().Trim(); }
        catch (Exception) { }//基本数量
        try { DetailCostPrice = context.Request.Form["DetailCostPrice"].ToString().Trim(); }
        catch (Exception) { }//基本单价
        try { DetailExtRate = context.Request.Form["DetailExtRate"].ToString().Trim(); }
        catch (Exception) { }//比率

        try { CheckCounts = context.Request.Form["CheckCounts"].ToString().Trim(); }
        catch (Exception) { }//实盘数量

        string DetailBatchNo = context.Request.Form["DetailBatchNo"].ToString().Trim();//批次


        string[] SortNo = FormatRequest(context, "SortNos", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductIDs", false).Split(',');
        //string[] UnitID = FormatRequest(context, "UnitIDs", true).Split(',');//实际单位
        string[] NowCount = FormatRequest(context, "NowCounts", true).Split(',');
        //string[] CheckCount = FormatRequest(context, "CheckCounts", true).Split(',');//实盘数量
        string[] DiffCount = FormatRequest(context, "DiffCounts", true).Split(',');
        string[] DiffType = FormatRequest(context, "DiffTypes", true).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');


        //多计量单位、批次（2010.04.13）
        string[] CostPrice = DetailCostPrice.Split(',');//实际单价
        string[] BaseUnitID = DetailBaseUnitID.Split(',');//基本单位
        string[] BaseCount = DetailBaseCount.Split(',');//基本数量
        //string[] BasePrice = DetailBasePrice.Split(',');
        string[] ExtRate = DetailExtRate.Split(',');//比率
        string[] BatchNo = DetailBatchNo.Split(',');//批次

        string[] UnitID = UnitIDs.Split(',');//实际单位
        string[] CheckCount = CheckCounts.Split(',');//实际数量

        List<StorageCheckDetail> scdList = new List<StorageCheckDetail>();
        for (int i = 0; i < SortNo.Length; i++)
        {
            StorageCheckDetail scd = new StorageCheckDetail();
            scd.CheckCount = Convert.ToDecimal(BaseCount[i]);//基本数量
            scd.CheckNo = sc.CheckNo;
            scd.CompanyCD = UserInfo.CompanyCD;
            scd.DiffCount = Convert.ToDecimal(DiffCount[i]);
            scd.DiffType = DiffType[i];
            scd.ModifiedDate = DateTime.Now;
            scd.ModifiedUserID = UserInfo.EmployeeName;
            scd.NowCount = Convert.ToDecimal(NowCount[i]);
            scd.ProductID = Convert.ToInt32(ProductID[i]);
            scd.Remark = RemarkList[i];
            scd.SortNo = Convert.ToInt32(SortNo[i]);
            //hm edit at 090927
            if (!string.IsNullOrEmpty(BaseUnitID[i]))//基本单位
                scd.UnitID = Convert.ToInt32(BaseUnitID[i]);
            else
                scd.UnitID = null;
            try
            {
                if (!string.IsNullOrEmpty(CostPrice[i]))//实际单价
                    scd.UsedPrice = Convert.ToDecimal(UnitID[i]);
                else
                    scd.UsedPrice = null;
            }
            catch(Exception) { }
            try
            {
            if (!string.IsNullOrEmpty(UnitID[i]))//实际单位
                scd.UsedUnitID = Convert.ToInt32(UnitID[i]);
            else
                scd.UsedUnitID = null;
            }
            catch (Exception) { }
            try
            {
                if (!string.IsNullOrEmpty(CheckCount[i]))//实际数量
                    scd.UsedUnitCount = Convert.ToDecimal(CheckCount[i]);
                else
                    scd.UsedUnitCount = null;
            }
            catch (Exception) { }
            try
            {
                if (!string.IsNullOrEmpty(ExtRate[i]))//比率
                    scd.ExRate = Convert.ToDecimal(ExtRate[i]);
                else
                    scd.ExRate = null;
            }
            catch (Exception) { }
            if (!string.IsNullOrEmpty(BatchNo[i]))//批次
                scd.BatchNo = BatchNo[i];
            else
                scd.BatchNo = null;
           
            //end
            //scd.UnitID = Convert.ToInt32(UnitID[i]);
            
            scdList.Add(scd);
        }
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        context.Response.Write(XBase.Business.Office.StorageManager.StorageCheckBus.StorageCheckAdd(sc,ht, scdList));

    }

    protected void Edit(HttpContext context)
    {
        StorageCheck sc = new StorageCheck();
        sc.Attachment = FormatRequest(context, "Attachment", false);
        sc.BillStatus = "1";
        sc.CheckCount = Convert.ToDecimal(FormatRequest(context, "CheckCount", true));
        sc.CheckEndDate = Convert.ToDateTime(FormatRequest(context, "CheckEndDate", false));
        sc.CheckMoney = Convert.ToDecimal(FormatRequest(context, "CheckMoney", false));
        sc.CheckNo = FormatRequest(context, "CheckNo", false);
        sc.CheckType = Convert.ToInt32(FormatRequest(context, "CheckType", true));
        sc.CheckStartkDate = Convert.ToDateTime(FormatRequest(context, "CheckStartDate", false));
        sc.CompanyCD = UserInfo.CompanyCD;
        sc.DeptID = Convert.ToInt32(FormatRequest(context, "DeptID", true));
        sc.DiffMoney = Convert.ToDecimal(FormatRequest(context, "DiffMoney", true));
        sc.DiffCount = Convert.ToDecimal(FormatRequest(context, "DiffCount", true));
        sc.ModifiedDate = DateTime.Now;
        sc.ModifiedUserID = UserInfo.EmployeeName;
        sc.NowCount = Convert.ToDecimal(FormatRequest(context, "NowCount", true));
        sc.NowMoney = Convert.ToDecimal(FormatRequest(context, "NowMoney", true));
        sc.Remark = FormatRequest(context, "Remark", false);
        sc.StorageID = Convert.ToInt32(FormatRequest(context, "StorageID", true));
        sc.Summary = FormatRequest(context, "Summary", false);
        sc.Title = FormatRequest(context, "Title", false);
        sc.Transactor = Convert.ToInt32(FormatRequest(context, "Transactor", true));
        sc.ID = Convert.ToInt32(FormatRequest(context, "CheckID", true));



        string DetailCostPrice = "";//实际单价
        string DetailBaseUnitID = "";//基本单位
        string UnitIDs = "";//实际单位
        string DetailBaseCount = "";//基本数量
        //string DetailBasePrice = "";//基本单价（没有存储）
        string DetailExtRate = "";//比率

        string CheckCounts = "";//实盘数量

        try
        {
            UnitIDs = context.Request.Form["UnitIDs"].ToString().Trim();
        }
        catch (Exception) { }//实际单位
        try { DetailBaseUnitID = context.Request.Form["DetailBaseUnitID"].ToString().Trim(); }
        catch (Exception) { }//基本单位
        try { DetailBaseCount = context.Request.Form["DetailBaseCount"].ToString().Trim(); }
        catch (Exception) { }//基本数量
        try { DetailCostPrice = context.Request.Form["DetailCostPrice"].ToString().Trim(); }
        catch (Exception) { }//基本单价
        try { DetailExtRate = context.Request.Form["DetailExtRate"].ToString().Trim(); }
        catch (Exception) { }//比率

        try { CheckCounts = context.Request.Form["CheckCounts"].ToString().Trim(); }
        catch (Exception) { }//实盘数量

        string DetailBatchNo = context.Request.Form["DetailBatchNo"].ToString().Trim();//批次
        
        
        
        string[] SortNo = FormatRequest(context, "SortNos", true).Split(',');
        string[] ProductID = FormatRequest(context, "ProductIDs", false).Split(',');
        //string[] UnitID = FormatRequest(context, "UnitIDs", true).Split(',');
        string[] NowCount = FormatRequest(context, "NowCounts", true).Split(',');
        //string[] CheckCount = FormatRequest(context, "CheckCounts", true).Split(',');
        string[] DiffCount = FormatRequest(context, "DiffCounts", true).Split(',');
        string[] DiffType = FormatRequest(context, "DiffTypes", true).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');

        //多计量单位、批次（2010.04.13）
        string[] CostPrice = DetailCostPrice.Split(',');//实际单价
        string[] BaseUnitID = DetailBaseUnitID.Split(',');//基本单位
        string[] BaseCount = DetailBaseCount.Split(',');//基本数量
        //string[] BasePrice = DetailBasePrice.Split(',');
        string[] ExtRate = DetailExtRate.Split(',');//比率
        string[] BatchNo = DetailBatchNo.Split(',');//批次

        string[] UnitID = UnitIDs.Split(',');//实际单位
        string[] CheckCount = CheckCounts.Split(',');//实际数量
        
        List<StorageCheckDetail> scdList = new List<StorageCheckDetail>();
        for (int i = 0; i < SortNo.Length; i++)
        {
            StorageCheckDetail scd = new StorageCheckDetail();
            scd.CheckCount = Convert.ToDecimal(BaseCount[i]);//基本数量
            scd.CheckNo = sc.CheckNo;
            scd.CompanyCD = UserInfo.CompanyCD;
            scd.DiffCount = Convert.ToDecimal(DiffCount[i]);
            scd.DiffType = DiffType[i];
            scd.ModifiedDate = DateTime.Now;
            scd.ModifiedUserID = UserInfo.EmployeeName;
            scd.NowCount = Convert.ToDecimal(NowCount[i]);
            scd.ProductID = Convert.ToInt32(ProductID[i]);
            scd.Remark = RemarkList[i];
            scd.SortNo = Convert.ToInt32(SortNo[i]);
            if (!string.IsNullOrEmpty(BaseUnitID[i]))//基本单位
                scd.UnitID = Convert.ToInt32(BaseUnitID[i]);
            else
                scd.UnitID = null;
            try
            {
                if (!string.IsNullOrEmpty(CostPrice[i]))//实际单价
                    scd.UsedPrice = Convert.ToDecimal(UnitID[i]);
                else
                    scd.UsedPrice = null;
            }
            catch (Exception) { }
            try
            {
                if (!string.IsNullOrEmpty(UnitID[i]))//实际单位
                    scd.UsedUnitID = Convert.ToInt32(UnitID[i]);
                else
                    scd.UsedUnitID = null;
            }
            catch (Exception) { }
            try
            {
                if (!string.IsNullOrEmpty(CheckCount[i]))//实际数量
                    scd.UsedUnitCount = Convert.ToDecimal(CheckCount[i]);
                else
                    scd.UsedUnitCount = null;
            }
            catch (Exception) { }
            try
            {
                if (!string.IsNullOrEmpty(ExtRate[i]))//比率
                    scd.ExRate = Convert.ToDecimal(ExtRate[i]);
                else
                    scd.ExRate = null;
            }
            catch (Exception) { }
            if (!string.IsNullOrEmpty(BatchNo[i]))//批次
                scd.BatchNo = BatchNo[i];
            else
                scd.BatchNo = null;
            scdList.Add(scd);
        }
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        context.Response.Write(XBase.Business.Office.StorageManager.StorageCheckBus.StorageCheckUpdate(sc,ht, scdList));
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}