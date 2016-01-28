<%@ WebHandler Language="C#" Class="AdversaryInfo_Add" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Business.Common;
using XBase.Model.Office.SellManager;

public class AdversaryInfo_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        bool isSucc = false;//是否更新成功       
        string strCode = null;

        string action = context.Request.Params["action"].ToString().Trim();	//动作

        string CustNo = context.Request.Params["CustNo"].ToString().Trim();	//发货单编号 
        //状态为insert时才计算编号
        if (action == "insert")
        {
            JsonClass jc;
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();	//发货单编号产生的规则  
            //如果编码规则不为空，表示自动产生编号
            if (CodeType.Length != 0)
            {
                strCode = ItemCodingRuleBus.GetCodeValue(CodeType, "AdversaryInfo", "CustNo");
            }
            else
            {
                strCode = CustNo;
            }
            /*判断是否存在*/
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("AdversaryInfo", "CustNo", strCode);
            if (!isAlready || string.IsNullOrEmpty(strCode))
            {
                if (string.IsNullOrEmpty(strCode))
                {
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置!", "", 0);
                }
                else
                {
                    jc = new JsonClass("竞争对手单据编号已经存在", "", 0);
                }
            }
            else
            {
                isSucc = InsertOrder(strCode, context);
                if (isSucc)
                {
                    jc = new JsonClass("", strCode, 1);
                }
                else
                {
                    jc = new JsonClass("", "", 0);
                }
            }
            
            context.Response.Write(jc);
        }
        else if (action == "update")//保存后修改
        {
            strCode = CustNo;
            isSucc = UpdateOrder(strCode, context);
            JsonClass jc;
            if (isSucc)
            {

                jc = new JsonClass("", strCode, 1);
            }
            else
            {
                jc = new JsonClass("", "", 0);
            }
            context.Response.Write(jc);
        }
    }


    /// <summary>
    /// 添加销售机会
    /// </summary>
    /// <returns></returns>
    private bool InsertOrder(string strCode, HttpContext context)
    {
        bool isSucc = false;//是否更新成功

        AdversaryInfoModel adversaryInfoModel = GetAdversaryInfoModel(strCode, context);
        List<AdversaryDynamicModel> adversaryDynamicModelList = GetAdversaryDynamicModelList(strCode, context);
        isSucc = AdversaryInfoBus.InsertOrder(adversaryInfoModel, adversaryDynamicModelList);
        return isSucc;
    }

    /// <summary>
    /// 添加销售机会
    /// </summary>
    /// <returns></returns>
    private bool UpdateOrder(string strCode, HttpContext context)
    {
        bool isSucc = false;//是否更新成功

        AdversaryInfoModel adversaryInfoModel = GetAdversaryInfoModel(strCode, context);
        List<AdversaryDynamicModel> adversaryDynamicModelList = GetAdversaryDynamicModelList(strCode, context);
        isSucc = AdversaryInfoBus.UpdateOrder(adversaryInfoModel, adversaryDynamicModelList);
        return isSucc;
    }


    private AdversaryInfoModel GetAdversaryInfoModel(string strCode, HttpContext context)
    {
        AdversaryInfoModel adversaryInfoModel = new AdversaryInfoModel();
        string BigType = context.Request.Params["BigType"].ToString().Trim();
        string CustType = context.Request.Params["CustType"].ToString().Trim();
        string CustClass = context.Request.Params["CustClass"].ToString().Trim();
        string CustName = context.Request.Params["CustName"].ToString().Trim();
        string ShortNam = context.Request.Params["ShortNam"].ToString().Trim();
        string PYShort = context.Request.Params["PYShort"].ToString().Trim();
        string AreaID = context.Request.Params["AreaID"].ToString().Trim();
        string SetupDate = context.Request.Params["SetupDate"].ToString().Trim();
        string ArtiPerson = context.Request.Params["ArtiPerson"].ToString().Trim();
        string CompanyType = context.Request.Params["CompanyType"].ToString().Trim();
        string StaffCount = context.Request.Params["StaffCount"].ToString().Trim();
        string SetupMoney = context.Request.Params["SetupMoney"].ToString().Trim();
        string SetupAddress = context.Request.Params["SetupAddress"].ToString().Trim();
        string website = context.Request.Params["website"].ToString().Trim();
        string CapitalScale = context.Request.Params["CapitalScale"].ToString().Trim();
        string SellArea = context.Request.Params["SellArea"].ToString().Trim();
        string SaleroomY = context.Request.Params["SaleroomY"].ToString().Trim();
        string ProfitY = context.Request.Params["ProfitY"].ToString().Trim();
        string TaxCD = context.Request.Params["TaxCD"].ToString().Trim();
        string BusiNumber = context.Request.Params["BusiNumber"].ToString().Trim();
        string IsTax = context.Request.Params["IsTax"].ToString().Trim();
        string Address = context.Request.Params["Address"].ToString().Trim();
        string Post = context.Request.Params["Post"].ToString().Trim();
        string ContactName = context.Request.Params["ContactName"].ToString().Trim();
        string Tel = context.Request.Params["Tel"].ToString().Trim();
        string Mobile = context.Request.Params["Mobile"].ToString().Trim();
        string email = context.Request.Params["email"].ToString().Trim();
        string CustNote = context.Request.Params["CustNote"].ToString().Trim();
        string Product = context.Request.Params["Product"].ToString().Trim();
        string Market = context.Request.Params["Market"].ToString().Trim();
        string SellMode = context.Request.Params["SellMode"].ToString().Trim();
        string Project = context.Request.Params["Project"].ToString().Trim();
        string Power = context.Request.Params["Power"].ToString().Trim();
        string Advantage = context.Request.Params["Advantage"].ToString().Trim();
        string disadvantage = context.Request.Params["disadvantage"].ToString().Trim();
        string Policy = context.Request.Params["Policy"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();

        adversaryInfoModel.CustNo = strCode;
        adversaryInfoModel.BigType = BigType;
        try
        {
            adversaryInfoModel.CustType = Convert.ToInt32(CustType);
        }
        catch { }
        try
        {
            adversaryInfoModel.CustClass = Convert.ToInt32(CustClass);
        }
        catch { }
        adversaryInfoModel.CustName = CustName;
        adversaryInfoModel.ShortNam = ShortNam;
        adversaryInfoModel.PYShort = PYShort;
        try
        {
            adversaryInfoModel.AreaID = Convert.ToInt32(AreaID);
        }
        catch { }
        try
        {
            adversaryInfoModel.SetupDate = Convert.ToDateTime(SetupDate);
        }
        catch { }
        adversaryInfoModel.ArtiPerson = ArtiPerson;
        adversaryInfoModel.CompanyType = CompanyType;
        try
        {
            adversaryInfoModel.StaffCount = Convert.ToInt32(StaffCount);
        }
        catch
        {
        }
        try
        {
            adversaryInfoModel.SetupMoney = Convert.ToDecimal(SetupMoney);
        }
        catch { }
        adversaryInfoModel.SetupAddress = SetupAddress;
        adversaryInfoModel.website = website;
        try
        {
            adversaryInfoModel.CapitalScale = Convert.ToDecimal(CapitalScale);
        }
        catch { }
        adversaryInfoModel.SellArea = SellArea;
        try
        {
            adversaryInfoModel.SaleroomY = Convert.ToDecimal(SaleroomY);
        }
        catch { }
        try
        {
            adversaryInfoModel.ProfitY = Convert.ToDecimal(ProfitY);
        }
        catch { }
        adversaryInfoModel.TaxCD = TaxCD;
        adversaryInfoModel.BusiNumber = BusiNumber;
        adversaryInfoModel.IsTax = IsTax;
        adversaryInfoModel.Address = Address;
        adversaryInfoModel.Post = Post;
        adversaryInfoModel.ContactName = ContactName;
        adversaryInfoModel.Tel = Tel;
        adversaryInfoModel.Mobile = Mobile;
        adversaryInfoModel.email = email;
        adversaryInfoModel.CustNote = CustNote;
        adversaryInfoModel.Product = Product;
        adversaryInfoModel.Market = Market;
        adversaryInfoModel.SellMode = SellMode;
        adversaryInfoModel.Project = Project;
        adversaryInfoModel.Power = Power;
        adversaryInfoModel.Advantage = Advantage;
        adversaryInfoModel.disadvantage = disadvantage;
        adversaryInfoModel.Policy = Policy;
        adversaryInfoModel.Remark = Remark;
        adversaryInfoModel.UsedStatus = UsedStatus;


        adversaryInfoModel.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人ID                                                
        adversaryInfoModel.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; ;//最后更新用户ID(对应操作用户UserID)                      
        adversaryInfoModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  

        return adversaryInfoModel;
    }

    private List<AdversaryDynamicModel> GetAdversaryDynamicModelList(string strCode, HttpContext context)
    {
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();

        AdversaryDynamicModel adversaryDynamicModel;
        List<AdversaryDynamicModel> adversaryDynamicModelList = new List<AdversaryDynamicModel>();
        strarray = strfitinfo.Split('|');
        string[] sqlarray = new string[strarray.Length];

        for (int i = 0; i < strarray.Length; i++)
        {
            StringBuilder fitsql = new StringBuilder();
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                adversaryDynamicModel = new AdversaryDynamicModel();

                adversaryDynamicModel.CustNo = strCode;
                adversaryDynamicModel.Dynamic = inseritems[0].ToString();


                adversaryDynamicModel.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//单位编码   
                adversaryDynamicModel.InputDate = DateTime.Now;
                adversaryDynamicModel.InputUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//单位编码      

                adversaryDynamicModelList.Add(adversaryDynamicModel);
            }
        }
        return adversaryDynamicModelList;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}