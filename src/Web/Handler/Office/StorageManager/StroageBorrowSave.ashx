<%@ WebHandler Language="C#" Class="StroageBorrowSave" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Collections.Generic;

using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StroageBorrowSave : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //接收动作参数 
        string cmd = HttpContext.Current.Server.UrlDecode(context.Request.Form["action"].ToString().ToUpper());
        if (cmd == "ADD" || cmd == "EDIT")
        {
            context.Response.Write(Add(context));
        }
        else if (cmd == "DELETE")
        {
            context.Response.Write(DeleteStorageBorrow(context.Request.Form["id"].ToString().Split(',')));
        }
        else if (cmd.ToLower() == "checkcount")
        {
            ValidateStorageCount(context);
        }
    }


    protected void ValidateStorageCount(HttpContext context)
    {
        string[] ProductID = FormatRequest(context, "ProductID", true).Split(',');
        string[] StorageID = FormatRequest(context, "StorageID", true).Split(',');
        string[] BatchNo = FormatRequest(context, "BatchNo", false).Split(',');
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        context.Response.Write(XBase.Business.Office.StorageManager.StorageBus.ValidateStorageCount(ProductID, StorageID, BatchNo, userinfo.CompanyCD));

    }



 


    protected string DeleteStorageBorrow(string[] id)
    {
        return XBase.Business.Office.StorageManager.StorageBorrowBus.DeleteStorageBorrow(id);
    }



    protected string Add(HttpContext context)
    {
        //接受基本参数信息
        string cmd = context.Request.Form["action"].ToString().ToUpper();
        UserInfoUtil userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        StorageBorrow borrow = new StorageBorrow();
        borrow.CompanyCD = userinfo.CompanyCD;

        if (cmd == "ADD")
        {
            if (FormatRequest(context, "bmgz", false) == "zd")
            {
                borrow.BorrowNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(FormatRequest(context, "BorrowNo", false), "StorageBorrow", "BorrowNo");


                //验证编号规则生成的编号 是否已经用完
                if (borrow.BorrowNo == "")
                {
                    return "-2^";
                }

            }
            else
                borrow.BorrowNo = FormatRequest(context, "BorrowNo", false);
            if (!XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq("StorageBorrow", "BorrowNo", borrow.BorrowNo))
            {
                return "-1^";
            }
        }
        else
            borrow.BorrowNo = FormatRequest(context, "BorrowNo", false);



        borrow.Title = context.Request.Form["Title"].ToString();
        borrow.Borrower = Convert.ToInt32(FormatRequest(context, "BorrowerID", true));
        borrow.DeptID = Convert.ToInt32(FormatRequest(context, "DeptID", true));
        borrow.ReasonType = Convert.ToInt32(FormatRequest(context, "ResaonID", true));
        string borrowdate = FormatRequest(context, "BorrowDate", false);
        borrow.BorrowDate = Convert.ToDateTime(borrowdate == string.Empty ? "1753-1-1" : borrowdate);
        borrow.StorageID = Convert.ToInt32(FormatRequest(context, "DepotID", true));
        borrow.Transactor = Convert.ToInt32(FormatRequest(context, "TransactorID", true));
        borrow.Summary = context.Request.Form["Summary"].ToString();
        borrow.TotalPrice = Convert.ToDecimal(FormatRequest(context, "TotalPrice", false));
        borrow.CountTotal = Convert.ToDecimal(context.Request.Form["TotalCount"].ToString());
        borrow.OutDeptID = Convert.ToInt32(FormatRequest(context, "BorrowDeptID", true));
        borrow.Remark = context.Request.Form["Remark"].ToString();
        borrow.Creator = userinfo.EmployeeID;
        borrow.CreateDate = DateTime.Now;
        borrow.BillStatus = "1";
        borrow.ModifiedDate = DateTime.Now;
        borrow.ModifiedUserID = userinfo.UserID;
        borrow.OutDate = Convert.ToDateTime(FormatRequest(context, "OutDate", false));
        //获取扩展属性
        Hashtable ht = GetExtAttr(context);
        //接受明细参数
        string[] No = context.Request.Form["no"].ToString().Split(',');
        string[] ProdID = context.Request.Form["ProdID"].ToString().Split(',');
        string[] ProductName = context.Request.Form["ProductName"].ToString().Split(',');
        string[] UnitID = context.Request.Form["UnitID"].ToString().Split(',');
        string[] Price = context.Request.Form["Price"].ToString().Split(',');
        string[] Count = context.Request.Form["Count"].ToString().Split(',');
        string[] Cost = context.Request.Form["Cost"].ToString().Split(',');
        string[] ReturnDate = context.Request.Form["ReturnDate"].ToString().Split(',');
        string[] ReturnCount = context.Request.Form["ReturnCount"].ToString().Split(',');
        string[] Remark = context.Request.Form["RemarkList"].ToString().Split(',');


        string[] UsedUnitID = context.Request.Form["UsedUnitID"].ToString().Split(',');
        string[] UsedCount = context.Request.Form["UsedCount"].ToString().Split(',');
        string[] UsedPrice = context.Request.Form["UsedPrice"].ToString().Split(',');
        string[] ExRate = context.Request.Form["ExRate"].ToString().Split(',');

        //批次
        string[] BatchNo = context.Request.Form["BatchNo"].ToString().Split(',');

        //构造list
        List<StorageBorrowDetail> detaillist = new List<StorageBorrowDetail>();
        for (var i = 0; i < No.Length; i++)
        {
            try
            {
                StorageBorrowDetail sdetails = new StorageBorrowDetail();
                sdetails.BorrowNo = borrow.BorrowNo;
                sdetails.CompanyCD = userinfo.CompanyCD;
                sdetails.ModifiedDate = DateTime.Now;
                sdetails.ModifiedUserID = userinfo.UserID;
                sdetails.ProductCount = Convert.ToDecimal(Count[i] == "" ? "0" : Count[i].ToString());
                sdetails.ProductID = ProdID[i] == "" ? "0" : ProdID[i].ToString();
                sdetails.Remark = Remark[i] == "" ? string.Empty : Remark[i].ToString();
                sdetails.ReturnCount = Convert.ToDecimal(ReturnCount[i] == "" ? "0" : ReturnCount[i].ToString());
                sdetails.ReturnDate = Convert.ToDateTime(ReturnDate[i].ToString());
                sdetails.SortNo = Convert.ToInt32(No[i] == "" ? "0" : No[i].ToString());
                sdetails.TotalPrice = Convert.ToDecimal(Cost[i] == "" ? "0" : Cost[i].ToString());
                sdetails.UnitID = Convert.ToInt32(UnitID[i].ToString());
                sdetails.UnitPrice = Convert.ToDecimal(Price[i].ToString());
                sdetails.BatchNo = BatchNo[i].ToString();

                /*计量单位新增*/
                sdetails.UsedUnitID = Convert.ToInt32(UsedUnitID[i]);
                sdetails.UsedPrice = Convert.ToDecimal(UsedPrice[i]);
                sdetails.UsedUnitCount = Convert.ToDecimal(UsedCount[i]);
                sdetails.ExRate = Convert.ToDecimal(ExRate[i]);
                detaillist.Add(sdetails);
            }
            catch
            {
                continue;
            }
        }

        string[] result = null;
        if (cmd == "ADD")
            result = XBase.Business.Office.StorageManager.StorageBorrowBus.SetStorageBorrow(borrow, detaillist, ht).Split('|');
        else if (cmd == "EDIT")
        {
            borrow.ID = Convert.ToInt32(context.Request.Form["BorrowID"].ToString());
            result = XBase.Business.Office.StorageManager.StorageBorrowBus.UpdateStorageBorrow(borrow, detaillist, ht).Split('|');
        }

        if (result[0] == "1")
        {
            result[1] += "|" + borrow.BorrowNo;
            return RetrunData(0, result[1]);
        }
        else
            return RetrunData(1, result[1]);

    }

    protected string RetrunData(int flag, string msg)
    {
        StringBuilder sbJson = new StringBuilder();
        sbJson.Append(flag.ToString() + "^" + msg);
        return sbJson.ToString();
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


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}