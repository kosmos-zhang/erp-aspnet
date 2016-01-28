<%@ WebHandler Language="C#" Class="ProductInfoSubUC" %>

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
using XBase.Model.Office.SubStoreManager;
using XBase.Business.Office.SubStoreManager;
using System.Web.SessionState;
using System.Collections.Generic;
using XBase.Business.Common;

public class ProductInfoSubUC : IHttpHandler, IRequiresSessionState
{
    private UserInfoUtil _userInfo = null;

    public void ProcessRequest(HttpContext context)
    {
        _userInfo = SessionUtil.Session["UserInfo"] as UserInfoUtil;
        if (context.Request.RequestType == "POST")
        {
            string Action = context.Request.Form["Action"];
            if (Action == "Select")
            {//检索
                string CompanyCD = string.Empty;
                string DeptID = string.Empty;
                CompanyCD = _userInfo.CompanyCD;
                DeptID = _userInfo.DeptID.ToString();
                DataRow dr = SubStorageDBHelper.GetSubDeptFromDeptID(DeptID);
                if (dr != null)
                {
                    DeptID = dr["ID"].ToString();
                }

                string orderString = (context.Request.Form["orderBy"]);//排序
                string order = "asc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "desc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                string ord = orderBy + " " + order;
                int TotalCount = 0;
                string ProductName = context.Request.Form["ProductName"].ToString();
                string ProductNo = context.Request.Form["ProductNo"].ToString();
                string Rate = context.Request.Form["Rate"].ToString();
                string LastRate = context.Request.Form["LastRate"].ToString();
                string Specification = context.Request.Form["Specification"].ToString();
                string EFIndex = "";
                string EFDesc = "";
                if (context.Request.Form["SubEFIndex"] != null && context.Request.Form["SubEFDesc"] != null)
                {
                    EFIndex = context.Request.Form["SubEFIndex"].ToString().Trim();
                    EFDesc = context.Request.Form["SubEFDesc"].ToString().Trim();
                }

                DataTable dt = new DataTable();
                dt = SubSellOrderBus.GetProdPrice(CompanyCD, DeptID, pageIndex, pageCount
                    , ord, Rate, LastRate, ref TotalCount, EFIndex, EFDesc
                    , ProductName, ProductNo, Specification);
                XBase.Common.StringUtil.DecimalFormatPoint(int.Parse(_userInfo.SelPoint), dt);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            if (Action == "GetGoodsInfoByBarcode")//分店条码扫描需要 added by linfei 
            {
                string CompanyCD = _userInfo.CompanyCD;
                string Barcode = context.Request.Params["Barcode"].ToString().Trim();//条码
                string DeptID = _userInfo.DeptID.ToString();
                DataRow dr = SubStorageDBHelper.GetSubDeptFromDeptID(DeptID);
                if (dr != null)
                {
                    DeptID = dr["ID"].ToString();
                }
                else
                {
                    DeptID = "0";
                }
                string Rate = context.Request.Form["Rate"].ToString();
                string LastRate = context.Request.Form["LastRate"].ToString();
                string EFIndex = "";
                string EFDesc = "";
                DataTable dt = SubSellOrderBus.GetProdPrice(CompanyCD, DeptID, Rate, LastRate, EFIndex, EFDesc, Barcode);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();

            }
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
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}