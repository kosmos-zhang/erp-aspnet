<%@ WebHandler Language="C#" Class="SubSellOrderAdd" %>

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

public class SubSellOrderAdd : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string OrderNo = context.Request.Form["OrderNo"];
            string Action = context.Request.Form["Action"];
            JsonClass jc;
            if (Action == "Add" || Action == "Update")
            {
                if (Action == "Add")
                {
                    string CodeRule = context.Request.Form["CodeRule"];
                    if (CodeRule == null)
                    {//手工输入的编号
                        OrderNo = context.Request.Form["OrderNo"];
                    }
                    else
                    {//自动获取编号
                        OrderNo = ItemCodingRuleBus.GetCodeValue(CodeRule);
                    }
                    int DeptID = Convert.ToInt32(context.Request.Form["DeptID"]);
                    if (!PrimekeyVerifyBus.CheckCodeUniq("SubSellOrder", "OrderNo", OrderNo) || string.IsNullOrEmpty(OrderNo))
                    {
                        if (string.IsNullOrEmpty(OrderNo))
                        {//该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!
                            jc = new JsonClass("success", "", 3);
                            context.Response.Write(jc);
                            context.Response.End();
                        }
                        else
                        {//单据编号已经存在
                            jc = new JsonClass("success", "", 2);
                            context.Response.Write(jc);
                            context.Response.End();
                        }
                        return;
                    }
                }
                else
                {
                    OrderNo = context.Request.Form["OrderNo"];
                }
                SubSellOrderModel SubSellOrderM = GetSubSellOrderM(context.Request, OrderNo);
                List<SubSellOrderDetailModel> SubSellOrderDetailMList = GetSubSellOrderDetailMList(context.Request, OrderNo);

                // 获得扩展属性
                Hashtable htExtAttr = GetExtAttr(context);

                if (Action == "Add")
                {//新增
                    int IDENTITY;
                    if (true == SubSellOrderBus.InsertSubSellOrder(SubSellOrderM, SubSellOrderDetailMList, out IDENTITY, htExtAttr))
                    {
                        string ID = IDENTITY.ToString();
                        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                        string BackValue = OrderNo + "#" + ID + "#" + EmployeeName + "#" + NowDate + "#" + UserID;

                        jc = new JsonClass("success", BackValue, 1);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
                else
                {//更新
                    if (true == SubSellOrderBus.UpdateSubSellOrder(SubSellOrderM, SubSellOrderDetailMList, htExtAttr))
                    {
                        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                        string BackValue = NowDate + "#" + UserID;

                        jc = new JsonClass("success", BackValue, 1);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }

            }
            else if (Action == "Confirm")
            {//确认
                //填写确认人，确认时间，更改单据状态为执行，更改业务状态为发货
                string ID = context.Request.Form["ID"];
                string No = context.Request.Form["OrderNo"];

                string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                string BackValue = NowDate + "#" + UserID + "#" + EmployeeName;
                //判断零售客户表中有没有这条客户的记录，没有则插入
                string CustName = context.Request.Params["CustName"];// ;
                if (PrimekeyVerifyBus.CheckCodeUniq("SubSellCustInfo", "CustName", CustName))
                {//不存在
                    //获取客户信息
                    SubSellCustInfoModel SubSellCustInfoM = GetSubSellCustInfoM(context.Request);
                    //将客户信息插入分店销售客户表    
                    if (true == SubSellOrderBus.ConfirmSubSellOrder(ID, No, SubSellCustInfoM))
                    {
                        jc = new JsonClass("success", BackValue, 1);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
                else
                {
                    if (true == SubSellOrderBus.ConfirmSubSellOrder(ID, No))
                    {
                        jc = new JsonClass("success", BackValue, 1);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }

            }
            else if (Action == "ConcelConfirm")
            {//取消确认
                string ID = context.Request.Form["ID"];
                if (SubSellOrderBus.ConcelConfirm(ID))
                {
                    string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string BackValue = NowDate + "#" + UserID + "#" + EmployeeName;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
                else
                {
                    jc = new JsonClass("success", "", 2);
                    context.Response.Write(jc);
                    context.Response.End();
                }

            }
            else if (Action == "ConfirmOut")
            {//出库时确认

                SubSellOrderModel SubSellOrderM = GetSubSellOrderM(context.Request, OrderNo);
                SubSellOrderM.Remark = context.Request.UrlReferrer.ToString();
                string PorductID = "";
                List<SubSellOrderDetailModel> SubSellOrderDetailMList = GetSubSellOrderDetailMList(context.Request, OrderNo);
                int Flag = SubSellOrderBus.CanConfirmOutSubSellOrder(SubSellOrderM, SubSellOrderDetailMList, ref PorductID);
                if (Flag == 1)
                {//不会出现负库存
                    if (true == SubSellOrderBus.ConfirmOutSubSellOrder(SubSellOrderM, SubSellOrderDetailMList))
                    {
                        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                        string BackValue = NowDate + "#" + UserID + "#" + EmployeeName;
                        jc = new JsonClass("success", BackValue, 1);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
                else if (Flag == 2)
                {//确认后会出现负库存 不允许出现负库存
                    jc = new JsonClass("success", PorductID, 2);
                    context.Response.Write(jc);

                    context.Response.End();
                }
                else if (Flag == 3)
                {//确认后会出现负库存 允许出现负库存，需要给提示
                    jc = new JsonClass("success", PorductID, 3);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else if (Action == "ConfirmOutAgain")
            {//会出现负库存时
                SubSellOrderModel SubSellOrderM = GetSubSellOrderM(context.Request, OrderNo);
                SubSellOrderM.Remark = context.Request.UrlReferrer.ToString();
                List<SubSellOrderDetailModel> SubSellOrderDetailMList = GetSubSellOrderDetailMList(context.Request, OrderNo);
                if (true == SubSellOrderBus.ConfirmOutSubSellOrder(SubSellOrderM, SubSellOrderDetailMList))
                {
                    string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string BackValue = NowDate + "#" + UserID + "#" + EmployeeName;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else if (Action == "ConfirmSett")
            {//结算确认
                SubSellOrderModel SubSellOrderM = GetSubSellOrderM(context.Request, OrderNo);
                SubSellOrderM.SttlUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
                SubSellOrderM.SttlDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (true == SubSellOrderBus.ConfirmSttlSubSellOrder(SubSellOrderM))
                {
                    string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
                    string BackValue = NowDate + "#" + UserID + "#" + EmployeeName;
                    jc = new JsonClass("success", BackValue, 1);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else if (Action == "GetCust")
            {//获取客户信息

                //设置行为参数
                string orderString = context.Request.Form["orderBy"];//排序
                string order = "ascending";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "descending";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                //获取检索条件
                SubSellCustInfoModel SubSellCustInfoM = GetSubSellCustInfoM(context.Request);
                XElement dsXML = ConvertDataTableToXML(SubSellOrderBus.GetCustInfo(SubSellCustInfoM));
                //linq排序
                var dsLinq =
                    (order == "ascending") ?
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value ascending
                     select
                     new CustInfoModel()
                     {
                         ID = x.Element("ID").Value,
                         CustName = x.Element("CustName").Value,
                         CustTel = x.Element("CustTel").Value,
                         CustMobile = x.Element("CustMobile").Value,
                         CustAddr = x.Element("CustAddr").Value,
                     })
                              :
                    (from x in dsXML.Descendants("Data")
                     orderby x.Element(orderBy).Value descending
                     select new CustInfoModel()
                     {
                         ID = x.Element("ID").Value,
                         CustName = x.Element("CustName").Value,
                         CustTel = x.Element("CustTel").Value,
                         CustMobile = x.Element("CustMobile").Value,
                         CustAddr = x.Element("CustAddr").Value,
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
            else if (Action == "Fill")
            {
                string ID = context.Request.Form["ID"];

                DataTable SubSellOrderPri = SubSellOrderBus.GetSubSellOrder(ID);
                DataTable SubSellOrderDetail = SubSellOrderBus.GetSubSellOrderDetail(ID);

                System.Text.StringBuilder SubSellOrder = new System.Text.StringBuilder();
                SubSellOrder.Append("{");
                SubSellOrder.Append("SubSellOrderPri:");
                SubSellOrder.Append(JsonClass.DataTable2Json(SubSellOrderPri));
                SubSellOrder.Append(",SubSellOrderDetail:");
                SubSellOrder.Append(JsonClass.DataTable2Json(SubSellOrderDetail));
                SubSellOrder.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(SubSellOrder.ToString());
                context.Response.End();
            }
        }
    }

    //获取销售订单主表信息
    private SubSellOrderModel GetSubSellOrderM(HttpRequest request, string OrderNo)
    {
        string Action = request.Form["Action"];
        //基础信息
        SubSellOrderModel SubSellOrderM = new SubSellOrderModel();
        SubSellOrderM.OrderNo = OrderNo;
        SubSellOrderM.ID = request.Form["ID"];
        SubSellOrderM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SubSellOrderM.Title = request.Form["Title"];
        SubSellOrderM.DeptID = request.Form["DeptID"];
        SubSellOrderM.SendMode = request.Form["SendMode"];
        SubSellOrderM.Seller = request.Form["Seller"];
        SubSellOrderM.CustName = request.Form["CustName"];

        SubSellOrderM.CustTel = request.Form["CustTel"];
        SubSellOrderM.CustMobile = request.Form["CustMobile"];
        SubSellOrderM.CustAddr = request.Form["CustAddr"];
        SubSellOrderM.CurrencyType = request.Form["CurrencyType"];
        SubSellOrderM.Rate = request.Form["Rate"];

        SubSellOrderM.OrderMethod = request.Form["OrderMethod"];
        SubSellOrderM.TakeType = request.Form["TakeType"];
        SubSellOrderM.PayType = request.Form["PayType"];
        SubSellOrderM.MoneyType = request.Form["MoneyType"];
        SubSellOrderM.OrderDate = request.Form["OrderDate"];

        SubSellOrderM.isAddTax = request.Form["isAddTax"];

        //发货信息
        SubSellOrderM.PlanOutDate = request.Form["PlanOutDate"];
        SubSellOrderM.OutDate = request.Form["OutDate"];
        SubSellOrderM.CarryType = request.Form["CarryType"];
        SubSellOrderM.BusiStatus = request.Form["BusiStatus"];
        SubSellOrderM.OutDeptID = request.Form["OutDeptID"];
        SubSellOrderM.OutUserID = request.Form["OutUserID"];

        //安装信息
        SubSellOrderM.NeedSetup = request.Form["NeedSetup"];
        SubSellOrderM.PlanSetDate = request.Form["PlanSetDate"];
        SubSellOrderM.SetDate = request.Form["SetDate"];
        SubSellOrderM.SetUserInfo = request.Form["SetUserID"];

        //合计信息
        SubSellOrderM.CountTotal = request.Form["CountTotal"];
        SubSellOrderM.TotalPrice = request.Form["TotalPrice"];
        SubSellOrderM.Tax = request.Form["TotalTax"];
        SubSellOrderM.TotalFee = request.Form["TotalFee"];
        SubSellOrderM.Discount = request.Form["Discount"];
        SubSellOrderM.DiscountTotal = request.Form["DiscountTotal"];
        SubSellOrderM.RealTotal = request.Form["RealTotal"];
        SubSellOrderM.PayedTotal = request.Form["PayedTotal"];
        SubSellOrderM.WairPayTotal = request.Form["WairPayTotal"];

        //备注信息
        SubSellOrderM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        SubSellOrderM.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
        SubSellOrderM.BillStatus = request.Form["BillStatus"];
        SubSellOrderM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        SubSellOrderM.ModifiedDate = DateTime.Now.ToShortDateString();

        SubSellOrderM.isOpenbill = request.Form["isOpenBill"];
        return SubSellOrderM;
    }

    //发货单确认时，更新主表的Model
    private SubSellOrderModel GetSubSellOrderMOut(HttpRequest request)
    {
        SubSellOrderModel SubSellOrderM = new SubSellOrderModel();
        //发货模式
        SubSellOrderM.SendMode = request.Form["SendMode"];
        //部门
        SubSellOrderM.DeptID = request.Form["DeptID"];
        //发货信息
        SubSellOrderM.PlanOutDate = request.Form["PlanOutDate"];
        SubSellOrderM.OutDate = request.Form["OutDate"];
        SubSellOrderM.CarryType = request.Form["CarryType"];
        SubSellOrderM.BusiStatus = "3";
        SubSellOrderM.OutDeptID = request.Form["OutDeptID"];
        SubSellOrderM.OutUserID = request.Form["OutUserID"];
        SubSellOrderM.CustAddr = request.Form["CustAddr"];

        //安装信息
        SubSellOrderM.NeedSetup = request.Form["NeedSetup"];
        SubSellOrderM.PlanSetDate = request.Form["PlanSetDate"];
        SubSellOrderM.SetDate = request.Form["SetDate"];
        SubSellOrderM.SetUserInfo = request.Form["SetUserInfo"];
        return SubSellOrderM;
    }
    //获取销售订单明细
    private List<SubSellOrderDetailModel> GetSubSellOrderDetailMList(HttpRequest request, string OrderNo)
    {
        List<SubSellOrderDetailModel> SubSellOrderDetailMList = new List<SubSellOrderDetailModel>();
        int Length = int.Parse(request.Form["length"]);
        for (int i = 0; i < Length; ++i)
        {
            SubSellOrderDetailModel SubSellOrderDetailM = new SubSellOrderDetailModel();
            SubSellOrderDetailM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SubSellOrderDetailM.DeptID = request.Form["DeptID"];
            SubSellOrderDetailM.OrderNo = OrderNo;
            SubSellOrderDetailM.SortNo = request.Form["SortNo" + i];
            SubSellOrderDetailM.ProductID = request.Form["ProductID" + i];
            SubSellOrderDetailM.StorageID = request.Form["StorageID" + i];
            SubSellOrderDetailM.ProductCount = request.Form["ProductCount" + i];
            SubSellOrderDetailM.UnitID = request.Form["UnitID" + i];

            SubSellOrderDetailM.UnitPrice = request.Form["UnitPrice" + i];
            SubSellOrderDetailM.TaxPrice = request.Form["TaxPrice" + i];
            SubSellOrderDetailM.Discount = request.Form["Discount" + i];
            SubSellOrderDetailM.TaxRate = request.Form["TaxRate" + i];
            SubSellOrderDetailM.TotalFee = request.Form["TotalFee" + i];

            SubSellOrderDetailM.TotalPrice = request.Form["TotalPrice" + i];
            SubSellOrderDetailM.TotalTax = request.Form["TotalTax" + i];
            SubSellOrderDetailM.Remark = request.Form["Remark" + i];

            SubSellOrderDetailM.BatchNo = request.Form["BatchNo" + i];
            SubSellOrderDetailM.UsedUnitID = Convert.ToInt32(request.Form["UsedUnitID" + i]);
            SubSellOrderDetailM.UsedUnitCount = Convert.ToDecimal(request.Form["UsedUnitCount" + i]);
            SubSellOrderDetailM.UsedPrice = Convert.ToDecimal(request.Form["UsedPrice" + i]);
            SubSellOrderDetailM.ExRate = Convert.ToDecimal(request.Form["ExRate" + i]);


            SubSellOrderDetailMList.Add(SubSellOrderDetailM);
        }
        return SubSellOrderDetailMList;
    }

    private List<ProductModel> GetConfirmOutProductMList(HttpRequest request)
    {
        List<ProductModel> ProductMList = new List<ProductModel>();
        int Length = int.Parse(request.Form["length"]);
        for (int i = 0; i < Length; ++i)
        {
            ProductModel ProductM = new ProductModel();
            ProductM.ProductID = request.Form["ProductID" + i];
            ProductM.DeptID = request.Form["DeptID" + i];
            ProductM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ProductM.ProductCount = request.Form["ProductCount" + i];
            ProductM.StorageID = request.Form["StorageID" + i];
            ProductMList.Add(ProductM);
        }
        return ProductMList;
    }

    //获取客户信息model
    private SubSellCustInfoModel GetSubSellCustInfoM(HttpRequest request)
    {
        SubSellCustInfoModel SubSellCustInfoM = new SubSellCustInfoModel();
        SubSellCustInfoM.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        SubSellCustInfoM.DeptID = request.Form["DeptID"];
        SubSellCustInfoM.CustName = request.Form["CustName"];
        SubSellCustInfoM.CustTel = request.Form["CustTel"];
        SubSellCustInfoM.CustMobile = request.Form["CustMobile"];
        SubSellCustInfoM.CustAddr = request.Form["CustAddr"];

        return SubSellCustInfoM;
    }
    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private System.Xml.Linq.XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        System.IO.StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new JavaScriptSerializer();
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
    public class CustInfoModel
    {
        public string ID { get; set; }
        public string CustName { get; set; }
        public string CustTel { get; set; }
        public string CustMobile { get; set; }
        public string CustAddr { get; set; }
    }

    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = context.Request.Form["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Form[arrKey[y].Trim()].ToString().Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception)
        { return null; }
    }
}