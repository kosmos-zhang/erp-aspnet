<%@ WebHandler Language="C#" Class="ProductPrice" %>

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
public class ProductPrice : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //string companyCD = "AAAAAA";//[待修改]

        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        if (Action == ActionUtil.Del.ToString())
        {
            string strID = context.Request.QueryString["str"].ToString().Trim();
            strID = strID.Substring(1, strID.Length - 1);
            if (ProductPriceChangeBus.DeleteProductPriceInfo(strID))
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
            context.Response.Write(jc);
        }
        else if (Action == "ChangeStatus")
        {
            int ID = int.Parse(context.Request.QueryString["ChangeID"].ToString());
            int ProductID = int.Parse(context.Request.QueryString["ProductID"].ToString());
            string StandardSellNew = context.Request.QueryString["StandardSellNew"].ToString();
            string SellTaxNew = context.Request.QueryString["SellTaxNew"].ToString();
            string Confirmor = context.Request.QueryString["Confirmor"].ToString();
            string ConfirmDate = context.Request.QueryString["ConfirmDate"].ToString();
            string TaxRateNew = context.Request.QueryString["TaxRateNew"].ToString();
            string DiscountNew = context.Request.QueryString["DiscountNew"].ToString();
            if (ProductPriceChangeBus.UpdateStatus(ID, "5",ProductID,StandardSellNew ,SellTaxNew,Confirmor,ConfirmDate,TaxRateNew,DiscountNew))
            {
                jc = new JsonClass("确认成功", "", 5);
            }
            else
            {
                jc = new JsonClass("确认失败", "", 0);
            }
            context.Response.Write(jc);
        }
        else if (Action == "Add")
        {
            #region 添加物品售价变更
            string ChangeNo = context.Request.QueryString["ChangeNo"].Trim();
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductPriceChange"
                                , "ChangeNo", ChangeNo);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("价格调整单编号已经存在", "", 0);
                context.Response.Write(jc);
            }
            else
            {
                ProductPriceChangeModel Model = new ProductPriceChangeModel();
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //string cmpanyCD = "AAAAAA";
                Model.CompanyCD = companyCD;
                Model.ChangeNo = context.Request.QueryString["ChangeNo"].ToString().Trim();
                Model.Title = context.Request.QueryString["Title"].ToString().Trim();
                Model.ProductID = context.Request.QueryString["ProductID"].ToString().Trim();
                Model.StandardSell = context.Request.QueryString["StandardSell"].ToString().Trim();
                Model.SellTax = context.Request.QueryString["SellTax"].ToString().Trim();
                Model.StandardSellNew = context.Request.QueryString["StandardSellNew"].ToString().Trim();
                Model.SellTaxNew = context.Request.QueryString["SellTaxNew"].ToString().Trim();
                Model.ChangeDate = context.Request.QueryString["ChangeDate"].ToString().Trim();
                Model.Chenger = context.Request.QueryString["Chenger"].ToString().Trim();
                Model.Remark = context.Request.QueryString["Remark"].ToString().Trim();
                Model.BillStatus = context.Request.QueryString["BillStatus"].ToString().Trim();
                Model.Creator = context.Request.QueryString["Creator"].ToString().Trim();
                Model.CreateDate = context.Request.QueryString["CreateDate"].ToString().Trim();
                Model.Confirmor = context.Request.QueryString["Confirmor"].ToString().Trim();
                Model.ConfirmDate = "" ;            
                Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                Model.TaxRate = context.Request.QueryString["TaxRate"].ToString().Trim();
                Model.TaxRateNew = context.Request.QueryString["TaxRateNew"].ToString().Trim();
                Model.Discount = context.Request.QueryString["Discount"].ToString().Trim();
                Model.DiscountNew = context.Request.QueryString["DiscountNew"].ToString().Trim();
                //Model.ModifiedUserID = "Admin";
                Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string tempID = "0";
                if (ProductPriceChangeBus.InsertProductPriceInfo(Model, out tempID))
                {
                    jc = new JsonClass("保存成功", "", int.Parse(tempID));
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                context.Response.Write(jc);
            }
            #endregion
        }
        else if (Action == "Edit")
        {
            ProductPriceChangeModel Model = new ProductPriceChangeModel();
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //string cmpanyCD = "AAAAAA";
            Model.CompanyCD = companyCD;
            Model.ID = int.Parse( context.Request.QueryString["ID"].ToString().Trim());
            Model.ChangeNo = context.Request.QueryString["ChangeNo"].ToString().Trim();
            Model.Title = context.Request.QueryString["Title"].ToString().Trim();
            Model.ProductID = context.Request.QueryString["ProductID"].ToString().Trim();
            Model.StandardSell = context.Request.QueryString["StandardSell"].ToString().Trim();
            Model.SellTax = context.Request.QueryString["SellTax"].ToString().Trim();
            Model.StandardSellNew = context.Request.QueryString["StandardSellNew"].ToString().Trim();
            Model.SellTaxNew = context.Request.QueryString["SellTaxNew"].ToString().Trim();
            Model.ChangeDate = context.Request.QueryString["ChangeDate"].ToString().Trim();
            Model.Chenger = context.Request.QueryString["Chenger"].ToString().Trim();
            Model.Remark = context.Request.QueryString["Remark"].ToString().Trim();
            Model.BillStatus = context.Request.QueryString["BillStatus"].ToString().Trim();
            Model.Creator = context.Request.QueryString["Creator"].ToString().Trim();
            Model.CreateDate = context.Request.QueryString["CreateDate"].ToString().Trim();
            Model.Confirmor = context.Request.QueryString["Confirmor"].ToString().Trim();
            Model.ConfirmDate = context.Request.QueryString["ConfirmDate"].ToString().Trim();
            Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            Model.TaxRate = context.Request.QueryString["TaxRate"].ToString().Trim();
            Model.TaxRateNew = context.Request.QueryString["TaxRateNew"].ToString().Trim();
            Model.Discount = context.Request.QueryString["Discount"].ToString().Trim();
            Model.DiscountNew = context.Request.QueryString["DiscountNew"].ToString().Trim();
            Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            //Model.ModifiedUserID = "Admin";
            //Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            if (ProductPriceChangeBus.UpdateProductPriceInfo(Model))
            {
                jc = new JsonClass("保存成功", "", 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            context.Response.Write(jc);
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