<%@ WebHandler Language="C#" Class="StorageSearchInfo" %>

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

public class StorageSearchInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string action = context.Request.QueryString["action"].ToString();

            if (action == "Get")
            {
                //设置行为参数
                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductNo";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


                //获取数据
                StorageProductModel model = new StorageProductModel();
                model.CompanyCD = companyCD;

                string ProductNo = string.Empty;
                string ProductName = string.Empty;
                string BarCode = string.Empty;
                model.StorageID = context.Request.QueryString["ddlStorage"].Trim();
                ProductNo = context.Request.QueryString["txtProductNo"].ToString();
                ProductName = context.Request.QueryString["txtProductName"].ToString();
                BarCode = context.Request.QueryString["BarCode"].ToString();

                string Specification = context.Request.Params["txtSpecification"].ToString().Trim();
                string Manufacturer = context.Request.Params["txtManufacturer"].ToString().Trim();
                string Material = context.Request.Params["ddlMaterial"].ToString().Trim();
                string FromAddr = context.Request.Params["txtFromAddr"].ToString().Trim();
                string StorageCount = context.Request.Params["txtStorageCount"].ToString().Trim();
                string StorageCount1 = context.Request.Params["txtStorageCount1"].ToString().Trim();
                string EFIndex = context.Request.Params["EFIndex"].ToString().Trim();
                string EFDesc = context.Request.Params["EFDesc"].ToString().Trim();
                string BatchNo = context.Request.Params["BatchNo"].ToString().Trim();
                string ColorID = context.Request.Params["ColorID"].ToString().Trim();
                string TypeID = context.Request.Params["TypeID"].ToString().Trim();
                model.ProductCount = StorageCount;

                XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel = new XBase.Model.Office.SupplyChain.ProductInfoModel();
                pdtModel.ProdNo = ProductNo;
                pdtModel.ProductName = ProductName;
                pdtModel.BarCode = BarCode;
                pdtModel.Specification = Specification;
                pdtModel.Manufacturer = Manufacturer;
                pdtModel.Material = Material;
                pdtModel.FromAddr = FromAddr;
                pdtModel.ColorID = ColorID;
                pdtModel.TypeID = TypeID;
                

                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StorageSearchBus.GetProductStorageTableBycondition(model, pdtModel, StorageCount1, EFIndex, EFDesc, pageIndex, pageCount, ord,BatchNo, ref TotalCount);

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
            else if (action == "GetBatchNo")
            {
                string ProductNo = string.Empty;
                string Storage = string.Empty;
                Storage = context.Request.QueryString["Storage"].Trim();
                ProductNo = context.Request.QueryString["ProductNo"].ToString();
                string sb = StorageSearchBus.GetBatchNo(ProductNo, Storage);

                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "GetSubBatchNo")
            {
                string sb = StorageSearchBus.GetSubBatchNo();

                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "SumGet")
            {
                //设置行为参数
                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductNo";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数


                //获取数据
                StorageProductModel model = new StorageProductModel();
                model.CompanyCD = companyCD;

                string ProductNo = string.Empty;
                string ProductName = string.Empty;
                string BarCode = string.Empty;
                model.StorageID = context.Request.QueryString["ddlStorage"].Trim();
                ProductNo = context.Request.QueryString["txtProductNo"].ToString();
                ProductName = context.Request.QueryString["txtProductName"].ToString();
                BarCode = context.Request.QueryString["BarCode"].ToString();

                string Specification = context.Request.Params["txtSpecification"].ToString().Trim();
                string Manufacturer = context.Request.Params["txtManufacturer"].ToString().Trim();
                string Material = context.Request.Params["ddlMaterial"].ToString().Trim();
                string FromAddr = context.Request.Params["txtFromAddr"].ToString().Trim();
                string StorageCount = context.Request.Params["txtStorageCount"].ToString().Trim();
                string StorageCount1 = context.Request.Params["txtStorageCount1"].ToString().Trim();
                string EFIndex = context.Request.Params["EFIndex"].ToString().Trim();
                string EFDesc = context.Request.Params["EFDesc"].ToString().Trim();
                string BatchNo = context.Request.Params["BatchNo"].ToString().Trim();
                string ColorID = context.Request.Params["ColorID"].ToString().Trim();
                string TypeID = context.Request.Params["TypeID"].ToString().Trim();
                model.ProductCount = StorageCount;

                XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel = new XBase.Model.Office.SupplyChain.ProductInfoModel();
                pdtModel.ProdNo = ProductNo;
                pdtModel.ProductName = ProductName;
                pdtModel.BarCode = BarCode;
                pdtModel.Specification = Specification;
                pdtModel.Manufacturer = Manufacturer;
                pdtModel.Material = Material;
                pdtModel.FromAddr = FromAddr;
                pdtModel.ColorID = ColorID;
                pdtModel.TypeID = TypeID;

                string ord = orderBy + " " + order;
                
                string returnStr = StorageSearchBus.GetSumStorageInfo(model, pdtModel, StorageCount1, EFIndex, EFDesc, ord, BatchNo);


                JsonClass jc;
                jc = new JsonClass("", "", 0);

                if (returnStr.Trim().Length > 0)
                {
                    jc = new JsonClass(returnStr, "", 1);
                }
                else
                {
                    jc = new JsonClass("", "", 0);
                }
                context.Response.Write(jc);
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
    //数据源结构
    public class DataSourceModel
    {

        public string ID { get; set; }
        public string StorageNo { get; set; }
        public string StorageName { get; set; }
        public string DeptName { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Specification { get; set; }
        public string UnitID { get; set; }
        public string ProductCount { get; set; }
        public string UseCount { get; set; }
        public string OrderCount { get; set; }
        public string RoadCount { get; set; }
        public string OutCount { get; set; }
    }

}