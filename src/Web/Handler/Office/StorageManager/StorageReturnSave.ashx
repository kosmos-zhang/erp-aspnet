<%@ WebHandler Language="C#" Class="StorageReturnSave" %>

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


public class StorageReturnSave : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"] == null ? string.Empty : context.Request.Form["action"].ToString();
        if (action.ToUpper() == "ADD")
        {
            Add(context);
        }
        else if (action.ToUpper() == "EDIT")
        {
            Edit(context);
        }
        else if (action.ToUpper() == "GET")
        {
            Get(context);
        }
        else if (action.ToUpper() == "STA")
        {
            SetStauts(context);
        }

    }

    protected void SetStauts(HttpContext context)
    {
        string stype = FormatRequest(context, "type", false);
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        StorageReturn sr = new StorageReturn();
        sr.ModifiedDate = DateTime.Now;
        sr.ModifiedUserID = userinfo.UserID;
        /***************************************************
         * stype  1:确认 2：结单 3：取消结单 
         * **************************************************/
        if (stype == "1" || stype == "3")
        {
            sr.BillStatus = "2";
        }
        else if (stype == "2")
            sr.BillStatus = "4";

        sr.CloseDate = DateTime.Now;
        sr.Closer = userinfo.EmployeeID;
        sr.ConfirmDate = DateTime.Now;
        sr.Confirmor = userinfo.EmployeeID;
        sr.ReturnNo = FormatRequest(context, "ReturnNo", false);
        sr.ID = Convert.ToInt32(FormatRequest(context, "ReturnID", true));
        sr.CompanyCD = userinfo.CompanyCD;
        context.Response.Write(XBase.Business.Office.StorageManager.StorageReturnBus.UpdateStatus(sr, stype));

    }


    protected void Get(HttpContext context)
    {
        int ID = Convert.ToInt32(context.Request.Form["ReturnID"].ToString());
        string orderString = (context.Request.Form["orderby"] == null ? string.Empty : context.Request.Form["orderby"]);//排序
        string order = "descending";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "ascending";//排序：降序
        }
        DataTable dt = XBase.Business.Office.StorageManager.StorageReturnBus.GetStorageReturn(ID);
        XElement dsXML = ConvertDataTableToXML(dt);
        var dsLinq =
         (order == "ascending") ?
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value ascending
          select new StorageReturnDataSourceModel()
          {
              BillStatus = GetElementValue(x, "BillStatus", false),
              BorrowDate = GetElementValue(x, "BorrowDate", true),
              BorrowDept = GetElementValue(x, "OutDept", false),
              BorrowDeptName = GetElementValue(x, "BorrowDeptName", false),
              Borrower = GetElementValue(x, "Borrower", false),
              BorrowerName = GetElementValue(x, "BorrowerName", false),
              BorrowNo = GetElementValue(x, "BorrowNo", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              Confirmor = GetElementValue(x, "Confirmor", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              DeptID = GetElementValue(x, "DeptID", false),
              DeptName = GetElementValue(x, "DeptName", false),
              FromBillID = GetElementValue(x, "FromBillID", false),
              InDate = GetElementValue(x, "InDate", true),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUer = GetElementValue(x, "ModifiedUserID", false),
              OutDept = GetElementValue(x, "OutDept", false),
              OutDeptName = GetElementValue(x, "OutDeptName", false),
              Remark = GetElementValue(x, "Remark", false),
              ReturnDate = GetElementValue(x, "ReturnDate", true),
              ReturnPerson = GetElementValue(x, "ReturnPerson", false),
              ReturnPersonName = GetElementValue(x, "ReturnPersonName", false),
              StorageID = GetElementValue(x, "StorageID", false),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              TotalCount = GetElementValue(x, "CountTotal", false),
              TotalPrice = GetElementValue(x, "TotalPrice", false),
              Transactor = GetElementValue(x, "Transactor", false),
              TransactorName = GetElementValue(x, "TransactorName", false),
              ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField10", false),
              StorageName = GetElementValue(x, "StorageName", false)

          })

                   :
         (from x in dsXML.Descendants("Data")
          orderby x.Element(orderBy).Value descending
          select new StorageReturnDataSourceModel()
          {
              BillStatus = GetElementValue(x, "BillStatus", false),
              BorrowDate = GetElementValue(x, "BorrowDate", true),
              BorrowDept = GetElementValue(x, "OutDept", false),
              BorrowDeptName = GetElementValue(x, "BorrowDeptName", false),
              Borrower = GetElementValue(x, "Borrower", false),
              BorrowerName = GetElementValue(x, "BorrowerName", false),
              BorrowNo = GetElementValue(x, "BorrowNo", false),
              CloseDate = GetElementValue(x, "CloseDate", true),
              Closer = GetElementValue(x, "Closer", false),
              ConfirmDate = GetElementValue(x, "ConfirmDate", true),
              Confirmor = GetElementValue(x, "Confirmor", false),
              CreateDate = GetElementValue(x, "CreateDate", true),
              Creator = GetElementValue(x, "Creator", false),
              DeptID = GetElementValue(x, "DeptID", false),
              DeptName = GetElementValue(x, "DeptName", false),
              FromBillID = GetElementValue(x, "FromBillID", false),
              InDate = GetElementValue(x, "InDate", true),
              ModifiedDate = GetElementValue(x, "ModifiedDate", true),
              ModifiedUer = GetElementValue(x, "ModifiedUserID", false),
              OutDept = GetElementValue(x, "OutDept", false),
              OutDeptName = GetElementValue(x, "OutDeptName", false),
              Remark = GetElementValue(x, "Remark", false),
              ReturnDate = GetElementValue(x, "ReturnDate", true),
              ReturnPerson = GetElementValue(x, "ReturnPerson", false),
              ReturnPersonName = GetElementValue(x, "ReturnPersonName", false),
              StorageID = GetElementValue(x, "StorageID", false),
              Summary = GetElementValue(x, "Summary", false),
              Title = GetElementValue(x, "Title", false),
              TotalCount = GetElementValue(x, "CountTotal", false),
              TotalPrice = GetElementValue(x, "TotalPrice", false),
              Transactor = GetElementValue(x, "Transactor", false),
              TransactorName = GetElementValue(x, "TransactorName", false),
              ExtField1 = GetElementValue(x, "ExtField1", false),
              ExtField2 = GetElementValue(x, "ExtField2", false),
              ExtField3 = GetElementValue(x, "ExtField3", false),
              ExtField4 = GetElementValue(x, "ExtField4", false),
              ExtField5 = GetElementValue(x, "ExtField5", false),
              ExtField6 = GetElementValue(x, "ExtField6", false),
              ExtField7 = GetElementValue(x, "ExtField7", false),
              ExtField8 = GetElementValue(x, "ExtField8", false),
              ExtField9 = GetElementValue(x, "ExtField9", false),
              ExtField10 = GetElementValue(x, "ExtField10", false),
              StorageName = GetElementValue(x, "StorageName", false)



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

    protected void Add(HttpContext context)
    {

        StorageReturn sr = new StorageReturn();
        List<StorageReturnDetail> srDetailList = new List<StorageReturnDetail>();
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        sr.BillStatus = "1";
        sr.CompanyCD = userinfo.CompanyCD;
        sr.CountTotal = Convert.ToDecimal(FormatRequest(context, "TotalCount", true));
        sr.CreateDate = DateTime.Now;
        sr.DeptID = Convert.ToInt32(FormatRequest(context, "DeptID", true));
        sr.FromBillID = Convert.ToInt32(FormatRequest(context, "FromBillID", true));
        sr.FromType = "1";
        sr.InDate = DateTime.Now;
        sr.Remark = FormatRequest(context, "Remark", false);
        sr.ReturnDate = DateTime.Now;
        sr.Creator = userinfo.EmployeeID;
        if (context.Request.Form["bmgz"].ToString() == "zd")
        {
            sr.ReturnNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(context.Request.Form["ReturnNo"].ToString().Trim(), "StorageReturn", "ReturnNo");
            if (sr.ReturnNo == "")
            {
                context.Response.Write("3|该单据编号规则自动生成的序号已达到上限，请检查编号规则设置");
                return;
            }
        }
        else
            sr.ReturnNo = FormatRequest(context, "ReturnNo", false);
        if (!XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq("StorageReturn", "ReturnNo", sr.ReturnNo))
        {
            context.Response.Write("1|该借货单货单据编号已经存在");
            return;
        }

        sr.ReturnPerson = Convert.ToInt32(FormatRequest(context, "ReturnerID", true));
        sr.StorageID = Convert.ToInt32(FormatRequest(context, "StorageID", true));
        sr.Summary = FormatRequest(context, "Summary", false);
        sr.Title = FormatRequest(context, "ReturnTitle", false);
        sr.TotalPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", true));
        sr.Transactor = Convert.ToInt32(FormatRequest(context, "Transactor", true));
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        string[] SortNo = FormatRequest(context, "SortNo", false).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] UnitID = FormatRequest(context, "UnitID", false).Split(',');
        string[] ProductCount = FormatRequest(context, "ProductCount", false).Split(',');
        string[] ReturnCount = FormatRequest(context, "ReturnCount", false).Split(',');
        string[] UnitPrice = FormatRequest(context, "UnitPrice", false).Split(',');
        string[] dTotalPrice = FormatRequest(context, "dTotalPrice", false).Split(',');
        string[] FromLineNo = FormatRequest(context, "FromLineNo", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');

        string[] UsedUnitID = context.Request.Form["UsedUnitID"].ToString().Split(',');
        string[] UsedCount = context.Request.Form["UsedCount"].ToString().Split(',');
        string[] UsedPrice = context.Request.Form["UsedPrice"].ToString().Split(',');
        string[] ExRate = context.Request.Form["ExRate"].ToString().Split(',');
        string[] BatchNo = context.Request.Form["BatchNo"].ToString().Split(',');
        
        try
        {
            for (int i = 0; i < SortNo.Length; i++)
            {
                StorageReturnDetail srd = new StorageReturnDetail();
                srd.CompanyCD = userinfo.CompanyCD;
                srd.FromBillID = sr.FromBillID;
                srd.FromLineNo = Convert.ToInt32(FromLineNo[i]);
                srd.FromType = "1";
                srd.ProductCount = Convert.ToDecimal(ProductCount[i]);
                srd.ProductID = Convert.ToInt32(ProductID[i]);
                srd.ProductName = ProductName[i];
                srd.Remark = RemarkList[i];
                srd.ReturnCount = Convert.ToDecimal(ReturnCount[i]);
                srd.ReturnNo = sr.ReturnNo;
                srd.SortNo = Convert.ToInt32(SortNo[i]);
                srd.TotalPrice = Convert.ToDecimal(dTotalPrice[i]);
                //hm edit at 090927
                if (!string.IsNullOrEmpty(UnitID[i]))
                    srd.UnitID = Convert.ToInt32(UnitID[i]);
                else
                    srd.UnitID = null;
                //end
                srd.UnitPrice = Convert.ToDecimal(UnitPrice[i]);

                /*计量单位新增*/
                srd.UsedUnitID = Convert.ToInt32(UsedUnitID[i]);
                srd.UsedPrice = Convert.ToDecimal(UsedPrice[i]);
                srd.UsedUnitCount = Convert.ToDecimal(UsedCount[i]);
                srd.ExRate = Convert.ToDecimal(ExRate[i]);
                srd.BatchNo = BatchNo[i];
                srDetailList.Add(srd);
            }
        }
        catch
        { }



        context.Response.Write(XBase.Business.Office.StorageManager.StorageReturnBus.StorageReturnAdd(sr, srDetailList,ht));


    }


    protected void Edit(HttpContext context)
    {
        StorageReturn sr = new StorageReturn();
        List<StorageReturnDetail> srDetailList = new List<StorageReturnDetail>();
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        sr.BillStatus = "1";
        sr.CompanyCD = userinfo.CompanyCD;
        sr.CountTotal = Convert.ToDecimal(FormatRequest(context, "TotalCount", true));
        sr.CreateDate = DateTime.Now;
        sr.DeptID = Convert.ToInt32(FormatRequest(context, "DeptID", true));
        sr.FromBillID = Convert.ToInt32(FormatRequest(context, "FromBillID", true));
        sr.FromType = "1";
        sr.InDate = DateTime.Now;
        sr.Remark = FormatRequest(context, "Remark", false);
        sr.ReturnDate = DateTime.Now;
        sr.ID = Convert.ToInt32(FormatRequest(context, "ReturnID", true));
        sr.ReturnNo = FormatRequest(context, "ReturnNo", false);
        sr.ReturnPerson = Convert.ToInt32(FormatRequest(context, "ReturnerID", true));
        sr.StorageID = Convert.ToInt32(FormatRequest(context, "StorageID", true));
        sr.Summary = FormatRequest(context, "Summary", false);
        sr.Title = FormatRequest(context, "ReturnTitle", false);
        sr.TotalPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", true));
        sr.Transactor = Convert.ToInt32(FormatRequest(context, "Transactor", true));
        sr.ModifiedDate = DateTime.Now;
        sr.ModifiedUserID = userinfo.EmployeeName;
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        string[] SortNo = FormatRequest(context, "SortNo", false).Split(',');
        string[] ProductID = FormatRequest(context, "ProductID", false).Split(',');
        string[] ProductName = FormatRequest(context, "ProductName", false).Split(',');
        string[] UnitID = FormatRequest(context, "UnitID", false).Split(',');
        string[] ProductCount = FormatRequest(context, "ProductCount", false).Split(',');
        string[] ReturnCount = FormatRequest(context, "ReturnCount", false).Split(',');
        string[] UnitPrice = FormatRequest(context, "UnitPrice", false).Split(',');
        string[] dTotalPrice = FormatRequest(context, "dTotalPrice", false).Split(',');
        string[] FromLineNo = FormatRequest(context, "FromLineNo", false).Split(',');
        string[] RemarkList = FormatRequest(context, "RemarkList", false).Split(',');

        string[] UsedUnitID = context.Request.Form["UsedUnitID"].ToString().Split(',');
        string[] UsedCount = context.Request.Form["UsedCount"].ToString().Split(',');
        string[] UsedPrice = context.Request.Form["UsedPrice"].ToString().Split(',');
        string[] ExRate = context.Request.Form["ExRate"].ToString().Split(',');
        string[] BatchNo = context.Request.Form["BatchNo"].ToString().Split(',');
        try
        {
            for (int i = 0; i < SortNo.Length; i++)
            {
                StorageReturnDetail srd = new StorageReturnDetail();
                srd.CompanyCD = userinfo.CompanyCD;
                srd.FromBillID = sr.FromBillID;
                srd.FromLineNo = Convert.ToInt32(FromLineNo[i]);
                srd.FromType = "1";
                srd.ProductCount = Convert.ToDecimal(ProductCount[i]);
                srd.ProductID = Convert.ToInt32(ProductID[i]);
                srd.ProductName = ProductName[i];
                srd.Remark = RemarkList[i];
                srd.ReturnCount = Convert.ToDecimal(ReturnCount[i]);
                srd.ReturnNo = sr.ReturnNo;
                srd.SortNo = Convert.ToInt32(SortNo[i]);
                srd.TotalPrice = Convert.ToDecimal(dTotalPrice[i]);
                //hm edit at 090927
                if (!string.IsNullOrEmpty(UnitID[i]))
                    srd.UnitID = Convert.ToInt32(UnitID[i]);
                else
                    srd.UnitID = null;
                //end
                srd.UnitPrice = Convert.ToDecimal(UnitPrice[i]);

                /*计量单位新增*/
                srd.UsedUnitID = Convert.ToInt32(UsedUnitID[i]);
                srd.UsedPrice = Convert.ToDecimal(UsedPrice[i]);
                srd.UsedUnitCount = Convert.ToDecimal(UsedCount[i]);
                srd.ExRate = Convert.ToDecimal(ExRate[i]);
                srd.BatchNo = BatchNo[i];
                srDetailList.Add(srd);
            }
        }
        catch
        { }
        context.Response.Write(XBase.Business.Office.StorageManager.StorageReturnBus.StorageReturnUpdate(sr, srDetailList,ht));


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


    public class StorageReturnDataSourceModel
    {

        public string Title { get; set; }
        public string FromBillID { get; set; }
        public string StorageID { get; set; }
        public string DeptID { get; set; }
        public string DeptName { get; set; }
        public string ReturnPerson { get; set; }
        public string ReturnPersonName { get; set; }
        public string ReturnDate { get; set; }
        public string TotalPrice { get; set; }
        public string TotalCount { get; set; }
        public string Summary { get; set; }
        public string InDate { get; set; }
        public string Transactor { get; set; }
        public string TransactorName { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string BillStatus { get; set; }
        public string Confirmor { get; set; }
        public string ConfirmDate { get; set; }
        public string Closer { get; set; }
        public string CloseDate { get; set; }
        public string ModifiedUer { get; set; }
        public string ModifiedDate { get; set; }

        public string BorrowNo { get; set; }
        public string BorrowDept { get; set; }
        public string BorrowDeptName { get; set; }
        public string OutDept { get; set; }
        public string OutDeptName { get; set; }
        public string Borrower { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowDate { get; set; }
        public string StorageName { get; set; }

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





    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}