<%@ WebHandler Language="C#" Class="PurchaseContractInfo" %>

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
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using System.Web.SessionState;
using System.Text;

public class PurchaseContractInfo : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string ActionPlan = context.Request.Params["ActionPlan"];
            

            if (ActionPlan == "Delete")
            {
                string DetailNo = context.Request.Params["DetailNo"].ToString().Trim();
                DetailNo = DetailNo.Remove(DetailNo.Length - 1, 1);
                JsonClass JC;
                if (PurchaseContractBus.DeletePurchaseContractAll(DetailNo))
                    JC = new JsonClass("success", "", 1);
                else
                    JC = new JsonClass("faile", "", 0);
                context.Response.Write(JC);
                
                //JsonClass jc;
                //int Length = Convert.ToInt32(context.Request.Params["Length"]);
                //bool Flag = true;
                //for (int i = 0; i < Length; ++i)
                //{

                //    string ContractNo = context.Request.Params["ContractNo" + i];
                //    try
                //    {
                //        Flag = Flag && (PurchaseContractBus.DeletePurchaseContractAll(ContractNo));
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //    if (Flag == false)
                //    {
                //        jc = new JsonClass("faile", "", 0);
                //        context.Response.Write(jc);
                //        return;
                //    }
                //}
                //jc = new JsonClass("success", "", 1);
                //context.Response.Write(jc);
            }
            else
            {

                //设置行为参数
                string orderString = context.Request.QueryString["orderby"].ToString();//排序
                string order = "DESC";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ModifiedDate";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "ASC";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string ContractNo1 = context.Request.Form["ContractNo1"];
                string Title = context.Request.Form["Title"];
                string TypeID = context.Request.Form["TypeID"];
                string DeptID = context.Request.Form["DeptID"];
                string Seller = context.Request.Form["Seller"];
                string FromType = context.Request.Form["FromType"];
                string ProviderID = context.Request.Form["ProviderID"];
                string BillStatus = context.Request.Form["BillStatus"];
                string UsedStatus = context.Request.Form["UsedStatus"];
                string EFDesc = context.Request.Params["EFDesc"];
                string EFIndex = context.Request.Params["EFIndex"];
                
                
                int TotalCount = 0;

                
                orderBy = orderBy + " " + order;
                //获取数据
                //string[] str = new string[9];
                //if (context.Request.Params["ContractNo"] != null && context.Request.Params["ContractNo"] != "")
                //{
                //    str[0] = context.Request.Params["ContractNo"].ToString();
                //}
                //else
                //{
                //    str[0] = "";
                //}
                //if (context.Request.Params["Title"] != null && context.Request.Params["Title"] != "")
                //{
                //    str[1] = context.Request.Params["Title"].ToString();
                //}
                //else
                //{
                //    str[1] = "";
                //}
                //if (context.Request.Params["TypeID"] != null && context.Request.Params["TypeID"] != "")
                //{
                //    str[2] = context.Request.Params["TypeID"].ToString();
                //}
                //else
                //{
                //    str[2] = "";
                //}
                //if (context.Request.Params["DeptID"] != null && context.Request.Params["DeptID"] != "")
                //{
                //    str[3] = context.Request.Params["DeptID"].ToString();
                //}
                //else
                //{
                //    str[3] = "";
                //}
                //if (context.Request.Params["Seller"] != null && context.Request.Params["Seller"] != "")
                //{
                //    str[4] = context.Request.Params["Seller"].ToString();
                //}
                //else
                //{
                //    str[4] = "";
                //}
                //if (context.Request.Params["FromType"] != null && context.Request.Params["FromType"] != "")
                //{
                //    str[5] = context.Request.Params["FromType"].ToString();
                //}
                //else
                //{
                //    str[5] = "";
                //}
                //if (context.Request.Params["ProviderID"] != null && context.Request.Params["ProviderID"] != "")
                //{
                //    str[6] = context.Request.Params["ProviderID"].ToString();
                //}
                //else
                //{
                //    str[6] = "";
                //}
                //if (context.Request.Params["BillStatus"] != null && context.Request.Params["BillStatus"] != "")
                //{
                //    str[7] = context.Request.Params["BillStatus"].ToString();
                //}
                //else
                //{
                //    str[7] = "";
                //}
                //if (context.Request.Params["UsedStatus"] != null && context.Request.Params["UsedStatus"] != "")
                //{
                //    str[8] = context.Request.Params["UsedStatus"].ToString();
                //}
                //else
                //{
                //    str[8] = "";
                //}

                //XElement dsXML = ConvertDataTableToXML();

                
                
                ////linq排序
                //var dsLinq =
                //    (order == "ascending") ?
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value ascending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         ContractNo = x.Element("ContractNo").Value,
                //         Title = x.Element("Title").Value,
                //         TypeID = x.Element("TypeID").Value,
                //         TypeName = x.Element("TypeName").Value,
                //         DeptID = x.Element("DeptID").Value,
                //         DeptName = x.Element("DeptName").Value,
                //         Seller = x.Element("Seller").Value,
                //         EmployeeName = x.Element("EmployeeName").Value,
                //         FromType = x.Element("FromType").Value,
                //         ProviderID = x.Element("ProviderID").Value,
                //         CustName = x.Element("CustName").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         TotalTax = x.Element("TotalTax").Value,
                //         TotalFee = x.Element("TotalFee").Value,
                //         UsedStatus = x.Element("UsedStatus").Value,
                //         Isyinyong = x.Element("Isyinyong").Value,
                //     })
                //              :
                //    (from x in dsXML.Descendants("Data")
                //     orderby x.Element(orderBy).Value descending
                //     select new DataSourceModel()
                //     {
                //         ID = x.Element("ID").Value,
                //         ContractNo = x.Element("ContractNo").Value,
                //         Title = x.Element("Title").Value,
                //         TypeID = x.Element("TypeID").Value,
                //         TypeName = x.Element("TypeName").Value,
                //         DeptID = x.Element("DeptID").Value,
                //         DeptName = x.Element("DeptName").Value,
                //         Seller = x.Element("Seller").Value,
                //         EmployeeName = x.Element("EmployeeName").Value,
                //         FromType = x.Element("FromType").Value,
                //         ProviderID = x.Element("ProviderID").Value,
                //         CustName = x.Element("CustName").Value,
                //         BillStatus = x.Element("BillStatus").Value,
                //         TotalPrice = x.Element("TotalPrice").Value,
                //         TotalTax = x.Element("TotalTax").Value,
                //         TotalFee = x.Element("TotalFee").Value,
                //         UsedStatus = x.Element("UsedStatus").Value,
                //         Isyinyong = x.Element("Isyinyong").Value,
                //     });
                //int totalCount = dsLinq.Count();
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("{");
                //sb.Append("totalCount:");
                //sb.Append(totalCount.ToString());
                //sb.Append(",data:");
                //sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
                //sb.Append("}");

                context.Response.ContentType = "text/plain";
                //string temp = JsonClass.DataTable2Json();
                DataTable dt = PurchaseContractBus.SelectPurchaseContract(pageIndex, pageCount, orderBy, ref TotalCount, ContractNo1, Title, TypeID, DeptID, Seller, FromType, ProviderID, BillStatus, UsedStatus,EFIndex ,EFDesc );

                StringBuilder sb = new StringBuilder();
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
    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string ContractNo { get; set; }
    //    public string Title { get; set; }
    //    public string TypeID { get; set; }
    //    public string TypeName { get; set; }
    //    public string DeptID { get; set; }
    //    public string DeptName { get; set; }
    //    public string Seller { get; set; }
    //    public string EmployeeName { get; set; }
    //    public string FromType { get; set; }
    //    public string ProviderID { get; set; }
    //    public string CustName { get; set; }
    //    public string BillStatus { get; set; }
    //    public string TotalPrice { get; set; }
    //    public string TotalTax { get; set; }
    //    public string TotalFee { get; set; }
    //    public string UsedStatus { get; set; }
    //    public string Isyinyong { get; set; }
    //}
}