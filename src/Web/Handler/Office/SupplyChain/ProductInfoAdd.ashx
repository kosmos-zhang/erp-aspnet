<%@ WebHandler Language="C#" Class="ProductInfoAdd" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Common;

public class ProductInfoAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //string companyCD = "AAAAAA";//[待修改]

        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        if (Action == ActionUtil.Del.ToString())
        {
            string strID = context.Request.QueryString["str"].ToString().Trim();
            strID = strID.Substring(1, strID.Length-1);
            string Fno = "";

            string[] ProductsNo = strID.Split(',');
            for (int i = 0; i < ProductsNo.Length; i++)
            {
                string ID = ProductsNo[i].ToString();
                string[] No = ID.Split('|');
                string Fsta = No[1].ToString();
                if (Fsta != "0")
                {
                    jc = new JsonClass("只有审核状态为草稿时才可以删除，请检查数据", "", 0);
                    context.Response.Write(jc);
                    return;
                }
                else
                {
                    string flowflag = No[0].ToString();
                    Fno += flowflag + ",";
                }
            }
            Fno = Fno.Substring(0, Fno.Length - 1);
            bool succ = ProductInfoBus.Existss(Fno);
            if (succ)
            {
                jc = new JsonClass("请检查物品是否被其他地方引用，无法删除！", "", 0);
            }
            else
            {
                if (ProductInfoBus.DeleteProductInfo(Fno))
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
            }
        
            context.Response.Write(jc);
        }
        else if (Action == "extValue")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            string strKey = context.Request.Form["keyList"].ToString().Trim();
            string strProNo = context.Request.Form["ProNo"].ToString().Trim();
            strKey = strKey.Substring(1, strKey.Length - 1);
            strKey = strKey.Replace('|', ',');
            DataTable dt = ProductInfoBus.GetExtAttrValue(strKey, strProNo, CompanyCD);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(dt.Rows.Count.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
            return;

        }
        else if (Action == "ChangeStatus")
        {
            int ID = int.Parse(context.Request.QueryString["ProductID"].ToString());
            string CheckDate = context.Request.QueryString["CheckDate"].ToString().Trim();
            string CheckUser = context.Request.QueryString["CheckUser"].ToString().Trim();
            string StorageID = context.Request.QueryString["StorageID"].ToString().Trim();
            if (ProductInfoBus.UpdateStatus(ID,"1",CheckUser,CheckDate,StorageID))
            {
                jc = new JsonClass("审核成功", "", 1);
            }
            else
            {
                jc = new JsonClass("审核失败", "", 0);
            }
            context.Response.Write(jc);
        }
        else if (Action == "Add")
        {
            Hashtable ht = GetExtAttr(context);
            #region 添加其他往来单位
            string ProNo = "";
            ProductInfoModel Model = new ProductInfoModel();
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            //if (action == "Add")
            //{
                if (!string.IsNullOrEmpty(CodeType))
                {
                     ProNo = ItemCodingRuleBus.GetCodeValue(CodeType, "ProductInfo", "ProdNo");
                }
                else
                {
                    ProNo = context.Request.Params["ProdNo"].ToString().Trim();//合同编号  
                     //bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductInfo"
                     //         , "ProdNo", ProNo);
                     ////存在的场合
                     //if (!isAlready)
                     //{
                     //    jc = new JsonClass("物品档案编码已经存在", "",0);
                     //    context.Response.Write(jc);
                     //    return;
                     //}
                     //Model.ProdNo = ProNo;
                }
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductInfo"
                              , "ProdNo", ProNo);
                //存在的场合
                if (!isAlready)
                {
                    jc = new JsonClass("物品档案编码已经存在", "", 0);
                    context.Response.Write(jc);
                    return;
                }

                //验证条码的唯一性
                string BarCode = context.Request.Params["BarCode"].ToString().Trim();
                if (!string.IsNullOrEmpty(BarCode))
                {
                    //为true，已经存在
                    if (ProductInfoBus.CheckBarCode(BarCode, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD))
                    {
                        jc = new JsonClass("物品条码已经存在", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                }
                Model.ProdNo = ProNo;
            //}
            //else
            //{
            //    model.RejectNo = context.Request.Params["arriveNo"].ToString().Trim();//合同编号 
            //}
            
            //判断是否存在
         
            //else
            //{
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                Model.CompanyCD = companyCD;
                Model.PYShort = context.Request.Params["PYShort"].ToString().Trim();
                Model.ProductName = context.Request.Params["ProductName"].ToString().Trim();
                Model.ShortNam = context.Request.Params["ShortNam"].ToString().Trim() ;
                Model.BarCode = context.Request.Params["BarCode"].ToString().Trim();
                Model.TypeID = context.Request.Params["TypeID"].ToString().Trim();
                Model.BigType = context.Request.Params["BigType"].ToString().Trim();
                Model.GradeID = context.Request.Params["GradeID"].ToString().Trim();
                Model.UnitID = context.Request.Params["UnitID"].ToString().Trim();
                Model.Brand = context.Request.Params["Brand"].ToString().Trim();
                Model.ColorID = context.Request.Params["ColorID"].ToString().Trim();
                /*规格型号特殊符号(+)处理*/
                string Specification = context.Request.Params["Specification"].ToString().Trim();
                string tmpSpec = "";
                for (int i = 0; i < Specification.Length; i++)
                {
                    if (Specification[i].ToString() == "＋")
                    {
                        tmpSpec = tmpSpec + '+';
                    }
                    else
                    {
                        tmpSpec = tmpSpec + Specification[i].ToString();
                    }
                }

                Model.Specification = tmpSpec.Replace("&#174", "×"); 
                Model.Size = context.Request.Params["Size"].ToString().Trim();
                Model.Source = context.Request.Params["Source"].ToString().Trim();
                Model.FromAddr = context.Request.Params["FromAddr"].ToString().Trim();
                Model.DrawingNum = context.Request.Params["DrawingNum"].ToString().Trim();
                Model.ImgUrl = context.Request.Params["ImgUrl"].ToString().Trim();
                Model.FileNo = context.Request.Params["FileNo"].ToString().Trim();
                Model.PricePolicy = context.Request.Params["PricePolicy"].ToString().Trim();
                Model.Params = context.Request.Params["Params"].ToString().Trim();
                Model.Questions = context.Request.Params["Questions"].ToString().Trim();
                Model.ReplaceName = context.Request.Params["ReplaceName"].ToString().Trim();
                Model.Description = context.Request.Params["Description"].ToString().Trim();
                Model.StockIs = context.Request.Params["StockIs"].ToString().Trim();
                Model.MinusIs = context.Request.Params["MinusIs"].ToString().Trim();
                Model.StorageID = context.Request.Params["StorageID"].ToString().Trim();
                Model.SafeStockNum = context.Request.Params["SafeStockNum"].ToString().Trim();
                Model.MinStockNum = context.Request.Params["MinStockNum"].ToString().Trim();
                Model.MaxStockNum = context.Request.Params["MaxStockNum"].ToString().Trim();
                Model.ABCType = context.Request.Params["ABCType"].ToString().Trim();
                Model.CalcPriceWays = context.Request.Params["CalcPriceWays"].ToString().Trim();
                Model.StandardCost = context.Request.Params["StandardCost"].ToString().Trim();
                Model.PlanCost = context.Request.Params["PlanCost"].ToString().Trim();
                Model.StandardSell = context.Request.Params["StandardSell"].ToString().Trim();
                Model.SellMin = context.Request.Params["SellMin"].ToString().Trim();
                Model.SellMax = context.Request.Params["SellMax"].ToString().Trim();
                Model.TaxRate = context.Request.Params["TaxRate"].ToString().Trim();
                Model.InTaxRate = context.Request.Params["InTaxRate"].ToString().Trim();
                Model.SellTax = context.Request.Params["SellTax"].ToString().Trim();
                Model.SellPrice = context.Request.Params["SellPrice"].ToString().Trim();
                Model.TransferPrice = context.Request.Params["TransfrePrice"].ToString().Trim();
                Model.Discount = context.Request.Params["Discount"].ToString().Trim();
                Model.StandardBuy = context.Request.Params["StandardBuy"].ToString().Trim();
                Model.TaxBuy = context.Request.Params["TaxBuy"].ToString().Trim();
                Model.BuyMax = context.Request.Params["BuyMax"].ToString().Trim();
                Model.Remark = context.Request.Params["Remark"].ToString().Trim();
                Model.Creator = context.Request.Params["Creator"].ToString().Trim();
                Model.CreateDate = context.Request.Params["CreateDate"].ToString().Trim();
                Model.CheckStatus = context.Request.Params["CheckStatus"].ToString().Trim();
                Model.CheckUser = context.Request.Params["CheckUser"].ToString().Trim();
                Model.CheckDate = context.Request.Params["CheckDate"].ToString().Trim();
                Model.UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
                Model.Manufacturer = context.Request.Params["Manufacturer"].ToString().Trim();
                Model.Material = context.Request.Params["Material"].ToString().Trim();
                Model.IsBatchNo = context.Request.Params["IsBatchNo"].ToString().Trim(); 
                Model.StorageUnit = context.Request.Params["StorageUnit"].ToString().Trim();
                Model.SellUnit = context.Request.Params["SellUnit"].ToString().Trim();
                Model.PurchseUnit = context.Request.Params["PurchseUnit"].ToString().Trim();
                Model.ProductUnit = context.Request.Params["ProductUnit"].ToString().Trim();
                Model.GroupNo = context.Request.Params["GroupNo"].ToString().Trim();
                Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                //Model.ModifiedUserID = "Admin";
                Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string tempID = "0";
                if (ProductInfoBus.InsertProductInfo(Model, out tempID, ht))
                {
                    jc = new JsonClass("保存成功", Model.ProdNo, int.Parse(tempID));
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                context.Response.Write(jc);
            //}
            #endregion
        }
        if (Action == "Edit")
        {
            Hashtable ht = GetExtAttr(context);
            ProductInfoModel Model = new ProductInfoModel();
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            Model.CompanyCD = companyCD;
            Model.ProdNo = context.Request.Params["ProdNo"].ToString().Trim();
            Model.PYShort = context.Request.Params["PYShort"].ToString().Trim();
            Model.ProductName = context.Request.Params["ProductName"].ToString().Trim();
            Model.ShortNam = context.Request.Params["ShortNam"].ToString().Trim();
            Model.BarCode = context.Request.Params["BarCode"].ToString().Trim();
            Model.TypeID = context.Request.Params["TypeID"].ToString().Trim();
            Model.BigType = context.Request.Params["BigType"].ToString().Trim();
            Model.GradeID = context.Request.Params["GradeID"].ToString().Trim();
            Model.UnitID = context.Request.Params["UnitID"].ToString().Trim();
            Model.Brand = context.Request.Params["Brand"].ToString().Trim();
            Model.ColorID = context.Request.Params["ColorID"].ToString().Trim();
            /*规格型号特殊符号(+)处理*/
            string Specification = context.Request.Params["Specification"].ToString().Trim();
            
            string tmpSpec = "";
            for (int i = 0; i < Specification.Length; i++)
            {
                if (Specification[i].ToString() =="＋")
                {
                    tmpSpec = tmpSpec + '+';
                }
                else
                {
                    tmpSpec = tmpSpec + Specification[i].ToString();
                }
            }
            Model.Specification = tmpSpec.Replace("&#174", "×");


            Model.Size = context.Request.Params["Size"].ToString().Trim();
            Model.Source = context.Request.Params["Source"].ToString().Trim();
            Model.FromAddr = context.Request.Params["FromAddr"].ToString().Trim();
            Model.DrawingNum = context.Request.Params["DrawingNum"].ToString().Trim();
            Model.ImgUrl = context.Request.Params["ImgUrl"].ToString().Trim();
            Model.FileNo = context.Request.Params["FileNo"].ToString().Trim();
            Model.PricePolicy = context.Request.Params["PricePolicy"].ToString().Trim();
            Model.Params = context.Request.Params["Params"].ToString().Trim();
            Model.Questions = context.Request.Params["Questions"].ToString().Trim();
            Model.ReplaceName = context.Request.Params["ReplaceName"].ToString().Trim();
            Model.Description = context.Request.Params["Description"].ToString().Trim();
            Model.StockIs = context.Request.Params["StockIs"].ToString().Trim();
            Model.MinusIs = context.Request.Params["MinusIs"].ToString().Trim();
            Model.StorageID = context.Request.Params["StorageID"].ToString().Trim();
            Model.SafeStockNum = context.Request.Params["SafeStockNum"].ToString().Trim();
            Model.MinStockNum = context.Request.Params["MinStockNum"].ToString().Trim();
            Model.MaxStockNum = context.Request.Params["MaxStockNum"].ToString().Trim();
            Model.ABCType = context.Request.Params["ABCType"].ToString().Trim();
            Model.CalcPriceWays = context.Request.Params["CalcPriceWays"].ToString().Trim();
            Model.StandardCost = context.Request.Params["StandardCost"].ToString().Trim();
            Model.PlanCost = context.Request.Params["PlanCost"].ToString().Trim();
            Model.StandardSell = context.Request.Params["StandardSell"].ToString().Trim();
            Model.SellMin = context.Request.Params["SellMin"].ToString().Trim();
            Model.SellMax = context.Request.Params["SellMax"].ToString().Trim();
            Model.TaxRate = context.Request.Params["TaxRate"].ToString().Trim();
            Model.InTaxRate = context.Request.Params["InTaxRate"].ToString().Trim();
            Model.SellTax = context.Request.Params["SellTax"].ToString().Trim();
            Model.SellPrice = context.Request.Params["SellPrice"].ToString().Trim();
            Model.TransferPrice = context.Request.Params["TransfrePrice"].ToString().Trim();
            Model.Discount = context.Request.Params["Discount"].ToString().Trim();
            Model.StandardBuy = context.Request.Params["StandardBuy"].ToString().Trim();
            Model.TaxBuy = context.Request.Params["TaxBuy"].ToString().Trim();
            Model.BuyMax = context.Request.Params["BuyMax"].ToString().Trim();
            Model.Remark = context.Request.Params["Remark"].ToString().Trim();
            Model.Creator = context.Request.Params["Creator"].ToString().Trim();
            Model.CreateDate = context.Request.Params["CreateDate"].ToString().Trim();
            Model.CheckStatus = "0";
            Model.CheckUser = context.Request.Params["CheckUser"].ToString().Trim();
            Model.CheckDate = context.Request.Params["CheckDate"].ToString().Trim();
            Model.UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
            Model.Manufacturer = context.Request.Params["Manufacturer"].ToString().Trim();
            Model.Material = context.Request.Params["Material"].ToString().Trim();
            Model.IsBatchNo = context.Request.Params["IsBatchNo"].ToString().Trim();
            Model.StorageUnit = context.Request.Params["StorageUnit"].ToString().Trim();
            Model.SellUnit = context.Request.Params["SellUnit"].ToString().Trim();
            Model.PurchseUnit = context.Request.Params["PurchseUnit"].ToString().Trim();
            Model.ProductUnit = context.Request.Params["ProductUnit"].ToString().Trim();
            Model.GroupNo = context.Request.Params["GroupNo"].ToString().Trim();
            Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            //Model.ModifiedUserID = "Admin";
            Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

            if (Model.IsBatchNo.Equals("0"))
            {
                decimal totalProductCount = ProductInfoBus.GetProductCountByAllBatchNo(Model.ProdNo, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                if (totalProductCount!=0)
                {
                    jc = new JsonClass("该物品的批次的现有存量没有清零，暂时无法停用批次", "", 0);
                    context.Response.Write(jc);
                    return;
                }
            }
            if (ProductInfoBus.UpdateProductInfo(Model, ht))
            {
                jc = new JsonClass("保存成功", Model.ProdNo, 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            
            context.Response.Write(jc);
        }
        if (Action == "GetGoodsInfoByBarcode")//根据扫描条码获取物品信息
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Barcode = context.Request.Params["Barcode"].ToString().Trim();//条码


            //int DeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID;
            //bool flag = ProductInfoBus.GetDeptID(DeptID);
            string StorageID = context.Request.Params["StorageID"].ToString().Trim();//仓库ID
            DataTable dt = ProductInfoDBHelper.GetDtGoodsInfoByBarcode(CompanyCD, Barcode,StorageID);

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
    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = context.Request.Params["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Params[arrKey[y].Trim()].ToString().Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch
        { return null; }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}