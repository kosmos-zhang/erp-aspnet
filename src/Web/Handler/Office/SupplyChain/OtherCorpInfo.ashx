<%@ WebHandler Language="C#" Class="OtherCorpInfo" %>

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
public class OtherCorpInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string companyCD = "AAAAAA";//[待修改]

        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        if (Action == ActionUtil.Del.ToString())
        {
            string strID = context.Request.QueryString["str"].ToString().Trim();
            strID = strID.Substring(1, strID.Length-1);
            if (OtherCorpInfoBus.DeleteOtherCorpInfo(strID))
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
            context.Response.Write(jc);
        }
        else if(Action =="Add")
        {
            #region 添加其他往来单位
            string CustNo = context.Request.QueryString["CustNo"].Trim();
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("OtherCorpInfo"
                                , "CustNo", CustNo);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("往来单位编号已经存在", "", 0);
                context.Response.Write(jc);
            }
            else
            {
                OtherCorpInfoModel Model = new OtherCorpInfoModel();
                Model.CompanyCD = companyCD;
                Model.BigType = context.Request.QueryString["BigType"].ToString().Trim();
                Model.CustNo = context.Request.QueryString["CustNo"].ToString().Trim();
                Model.CustName = context.Request.QueryString["CustName"].ToString().Trim();
                Model.CorpNam = context.Request.QueryString["CorpNam"].ToString().Trim();
                Model.PYShort = context.Request.QueryString["PYShort"].ToString().Trim();
                Model.CustNote = context.Request.QueryString["CustNote"].ToString().Trim();
                Model.AreaID = context.Request.QueryString["AreaID"].ToString().Trim();
                Model.CompanyType = context.Request.QueryString["CompanyType"].ToString().Trim();
                Model.StaffCount = context.Request.QueryString["StaffCount"].ToString().Trim();
                Model.SetupDate = context.Request.QueryString["SetupDate"].ToString().Trim();
                Model.ArtiPerson = context.Request.QueryString["ArtiPerson"].ToString().Trim();
                Model.SetupMoney = context.Request.QueryString["SetupMoney"].ToString().Trim();
                Model.SetupAddress = context.Request.QueryString["SetupAddress"].ToString().Trim();
                Model.CapitalScale = context.Request.QueryString["CapitalScale"].ToString().Trim();
                Model.SaleroomY = context.Request.QueryString["SaleroomY"].ToString().Trim();
                Model.ProfitY = context.Request.QueryString["ProfitY"].ToString().Trim();
                Model.TaxCD = context.Request.QueryString["TaxCD"].ToString().Trim();
                Model.BusiNumber = context.Request.QueryString["BusiNumber"].ToString().Trim();
                Model.isTax = context.Request.QueryString["isTax"].ToString().Trim();
                Model.SellArea = context.Request.QueryString["SellArea"].ToString().Trim();
                Model.CountryID = context.Request.QueryString["CountryID"].ToString().Trim();
                Model.Province = context.Request.QueryString["Province"].ToString().Trim();
                Model.City = context.Request.QueryString["City"].ToString().Trim();
                Model.Post = context.Request.QueryString["Post"].ToString().Trim();
                Model.ContactName = context.Request.QueryString["ContactName"].ToString().Trim();
                Model.Tel = context.Request.QueryString["Tel"].ToString().Trim();
                Model.Fax = context.Request.QueryString["Fax"].ToString().Trim();
                Model.Mobile = context.Request.QueryString["Mobile"].ToString().Trim();
                Model.email = context.Request.QueryString["email"].ToString().Trim();
                Model.Addr=context.Request.QueryString["Addr"].ToString().Trim();
                Model.BillType=context.Request.QueryString["BillType"].ToString().Trim();         
                Model.PayType=context.Request.QueryString["PayType"].ToString().Trim();           
                Model.MoneyType=context.Request.QueryString["MoneyType"].ToString().Trim();
                Model.CurrencyType = context.Request.QueryString["CurrencyType"].ToString().Trim(); 
                Model.Remark=context.Request.QueryString["Remark"].ToString().Trim();             
                Model.UsedStatus=context.Request.QueryString["UsedStatus"].ToString().Trim();     
                Model.Creator=context.Request.QueryString["Creator"].ToString().Trim();           
                Model.CreateDate=context.Request.QueryString["CreateDate"].ToString().Trim();     

                Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                //Model.ModifiedUserID = "Admin";
                Model.ModifiedUserID=((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                if (OtherCorpInfoBus.InsertCropInfo(Model))
                {
                    jc = new JsonClass("保存成功", "", 1);
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
            OtherCorpInfoModel Model = new OtherCorpInfoModel();
            Model.CustNo = context.Request.QueryString["CustNo"].Trim();
            Model.CompanyCD = companyCD;
            Model.BigType = context.Request.QueryString["BigType"].ToString().Trim();
            Model.CustNo = context.Request.QueryString["CustNo"].ToString().Trim();
            Model.CustName = context.Request.QueryString["CustName"].ToString().Trim();
            Model.CorpNam = context.Request.QueryString["CorpNam"].ToString().Trim();
            Model.PYShort = context.Request.QueryString["PYShort"].ToString().Trim();
            Model.CustNote = context.Request.QueryString["CustNote"].ToString().Trim();
            Model.AreaID = context.Request.QueryString["AreaID"].ToString().Trim();
            Model.CompanyType = context.Request.QueryString["CompanyType"].ToString().Trim();
            Model.StaffCount = context.Request.QueryString["StaffCount"].ToString().Trim();
            Model.SetupDate = context.Request.QueryString["SetupDate"].ToString().Trim();
            Model.ArtiPerson = context.Request.QueryString["ArtiPerson"].ToString().Trim();
            Model.SetupMoney = context.Request.QueryString["SetupMoney"].ToString().Trim();
            Model.SetupAddress = context.Request.QueryString["SetupAddress"].ToString().Trim();
            Model.CapitalScale = context.Request.QueryString["CapitalScale"].ToString().Trim();
            Model.SaleroomY = context.Request.QueryString["SaleroomY"].ToString().Trim();
            Model.ProfitY = context.Request.QueryString["ProfitY"].ToString().Trim();
            Model.TaxCD = context.Request.QueryString["TaxCD"].ToString().Trim();
            Model.BusiNumber = context.Request.QueryString["BusiNumber"].ToString().Trim();
            Model.isTax = context.Request.QueryString["isTax"].ToString().Trim();
            Model.SellArea = context.Request.QueryString["SellArea"].ToString().Trim();
            Model.CountryID = context.Request.QueryString["CountryID"].ToString().Trim();
            Model.Province = context.Request.QueryString["Province"].ToString().Trim();
            Model.City = context.Request.QueryString["City"].ToString().Trim();
            Model.Post = context.Request.QueryString["Post"].ToString().Trim();
            Model.ContactName = context.Request.QueryString["ContactName"].ToString().Trim();
            Model.Tel = context.Request.QueryString["Tel"].ToString().Trim();
            Model.Fax = context.Request.QueryString["Fax"].ToString().Trim();
            Model.Mobile = context.Request.QueryString["Mobile"].ToString().Trim();
            Model.email = context.Request.QueryString["email"].ToString().Trim();
            Model.Addr = context.Request.QueryString["Addr"].ToString().Trim();
            Model.BillType = context.Request.QueryString["BillType"].ToString().Trim();
            Model.PayType = context.Request.QueryString["PayType"].ToString().Trim();
            Model.MoneyType = context.Request.QueryString["MoneyType"].ToString().Trim();
            Model.CurrencyType = context.Request.QueryString["CurrencyType"].ToString().Trim();
            Model.Remark = context.Request.QueryString["Remark"].ToString().Trim();
            Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString().Trim();
            Model.Creator = context.Request.QueryString["Creator"].ToString().Trim();
            Model.CreateDate = context.Request.QueryString["CreateDate"].ToString().Trim();
            Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            if (OtherCorpInfoBus.UpdateCropInfo(Model))
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}