<%@ WebHandler Language="C#" Class="CustAdd" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Business.Common;
using System.Collections;
public class CustAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        JsonClass jc;
        string CustNo = context.Request.Params["CustNo"].ToString().Trim(); //客户编号
        string CustNoType = context.Request.Params["CustNoType"].ToString().Trim(); //客户编号
        
        if (Action == "1") //新建操作的时候判断唯一性
        {
            string tableName = "CustInfo";//入库表
            string columnName = "CustNo";//入库单编号
            string codeValue = CustNo;

            if (CustNoType != "")
                CustNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(CustNoType, tableName, columnName);

            else
            {
                 bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
                if (!ishave)
                {
                    jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            
        }
        
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码     
        string CustType = context.Request.Params["CustType"].ToString().Trim();//客户类别
        string CustClass = context.Request.Params["CustClass"].ToString().Trim();//客户分类            
        string CustName = context.Request.Params["CustName"].ToString().Trim();//客户名称

        string CustBig = context.Request.Params["CustBig"].ToString().Trim();//客户大类
        string CustNum = context.Request.Params["CustNum"].ToString().Trim();//卡号

        string[] arr = new string[] { "CustName", "CompanyCD" };
        string[] CustNames = new string[] { CustName, CompanyCD };

        bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNames);
        if (NameHas && Action=="1")
        {
            jc = new JsonClass("faile", "该客户名称已被使用，请输入未使用的客户名称！", 3);
            context.Response.Write(jc);
            context.Response.End();
        }
        
        
        string CustNam = context.Request.Params["CustNam"].ToString().Trim();
        string CustShort = context.Request.Params["CustShort"].ToString().Trim();
        string CreditGrade = context.Request.Params["CreditGrade"].ToString().Trim();//客户优质级别
        
        string Seller = context.Request.Params["Seller"].ToString().Trim();
        string AreaID = context.Request.Params["AreaID"].ToString().Trim();
        string CustNote = context.Request.Params["CustNote"].ToString().Trim();//客户简介
        string LinkCycle = context.Request.Params["LinkCycle"].ToString().Trim();//联络期限
        string HotIs = context.Request.Params["HotIs"].ToString().Trim();//热点客户
        string HotHow = context.Request.Params["HotHow"].ToString().Trim();       
        string MeritGrade = context.Request.Params["MeritGrade"].ToString().Trim();
        string RelaGrade = context.Request.Params["RelaGrade"].ToString().Trim();
        string Relation = context.Request.Params["Relation"].ToString().Trim();
        string CompanyType = context.Request.Params["CompanyType"].ToString().Trim();
        string StaffCount = context.Request.Params["StaffCount"].ToString().Trim();
        string Source = context.Request.Params["Source"].ToString().Trim();
        string Phase = context.Request.Params["Phase"].ToString().Trim();//阶段
        string CustSupe = context.Request.Params["CustSupe"].ToString().Trim();
        string Trade = context.Request.Params["Trade"].ToString().Trim();//行业
        string SetupDate = context.Request.Params["SetupDate"].ToString().Trim();                
        string ArtiPerson = context.Request.Params["ArtiPerson"].ToString().Trim();//法人代表
        string SetupMoney = context.Request.Params["SetupMoney"].ToString().Trim();//注册资本
        string SetupAddress = context.Request.Params["SetupAddress"].ToString().Trim();
        string CapitalScale = context.Request.Params["CapitalScale"].ToString().Trim();//资产规模
        string SaleroomY = context.Request.Params["SaleroomY"].ToString().Trim();
        string ProfitY = context.Request.Params["ProfitY"].ToString().Trim();
        string TaxCD = context.Request.Params["TaxCD"].ToString().Trim();
        string BusiNumber = context.Request.Params["BusiNumber"].ToString().Trim();//营业执照号
        string IsTax = context.Request.Params["IsTax"].ToString().Trim();
        string SellMode = context.Request.Params["SellMode"].ToString().Trim();
        string SellArea = context.Request.Params["SellArea"].ToString().Trim();
        string CountryID = context.Request.Params["CountryID"].ToString().Trim(); //国家地区
        string Province = context.Request.Params["Province"].ToString().Trim();
        string City = context.Request.Params["City"].ToString().Trim();
        string Tel = context.Request.Params["Tel"].ToString().Trim();
        string ContactName = context.Request.Params["ContactName"].ToString().Trim();
        string Mobile = context.Request.Params["Mobile"].ToString().Trim();//手机
        string ReceiveAddress = context.Request.Params["ReceiveAddress"].ToString().Trim();
        string WebSite = context.Request.Params["WebSite"].ToString().Trim();//公司网址
        string Post = context.Request.Params["Post"].ToString().Trim();//邮编
        string email = context.Request.Params["email"].ToString().Trim();
        string Fax = context.Request.Params["Fax"].ToString().Trim();
        string OnLine = context.Request.Params["OnLine"].ToString().Trim();
        string TakeType = context.Request.Params["TakeType"].ToString().Trim();//交货方式
        string CarryType = context.Request.Params["CarryType"].ToString().Trim();//运货方式
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();
        string BillType = context.Request.Params["BillType"].ToString().Trim();
        string PayType = context.Request.Params["PayType"].ToString().Trim();//结算方式
        string MoneyType = context.Request.Params["MoneyType"].ToString().Trim();//支付方式        
        string CurrencyType = context.Request.Params["CurrencyType"].ToString().Trim();//结算币种
        string CreditManage = context.Request.Params["CreditManage"].ToString().Trim();
        string MaxCredit = context.Request.Params["MaxCredit"].ToString().Trim();
        string MaxCreditDate = context.Request.Params["MaxCreditDate"].ToString().Trim();        
        string UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
        int Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID ; //建单人ID        
        string CreatedDate = context.Request.Params["CreatedDate"].ToString().Trim();
        string OpenBank = context.Request.Params["OpenBank"].ToString().Trim();
        string AccountMan = context.Request.Params["AccountMan"].ToString().Trim();
        string AccountNum = context.Request.Params["AccountNum"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string CustTypeManage = context.Request.Params["CustTypeManage"].ToString().Trim();
        string CustTypeSell = context.Request.Params["CustTypeSell"].ToString().Trim();
        string CustTypeTime = context.Request.Params["CustTypeTime"].ToString().Trim();
        string FirstBuyDate = context.Request.Params["FirstBuyDate"].ToString().Trim();
        string CanUserName = context.Request.Params["CanUserName"].ToString().Trim();
        string CanUserID = "," + context.Request.Params["CanUserID"].ToString().Trim() + ",";
        
        CustInfoModel CustInfoM = new CustInfoModel();
        
        CustInfoM.CompanyCD = CompanyCD;
        CustInfoM.CustBig = CustBig;
        CustInfoM.CustNum = CustNum;        
        CustInfoM.CustType = Convert.ToInt32(CustType);//客户类别
        CustInfoM.CustClass = CustClass == "" ? 0 : Convert.ToInt32(CustClass);//客户分类
        CustInfoM.CustNo = CustNo; //客户编号
        CustInfoM.CustName = CustName;//客户名称
        CustInfoM.CustNam = CustNam;
        CustInfoM.CustShort = CustShort;
        CustInfoM.CreditGrade = Convert.ToInt32(CreditGrade);  //客户优质级别
        CustInfoM.Seller = Convert.ToInt32(Seller);        
        CustInfoM.AreaID = Convert.ToInt32(AreaID);
        CustInfoM.CustNote = CustNote;//客户简介
        CustInfoM.LinkCycle = Convert.ToInt32(LinkCycle);//联络期限
        CustInfoM.HotIs = HotIs;//热点客户
        CustInfoM.HotHow = HotHow;        
        CustInfoM.MeritGrade = MeritGrade;
        CustInfoM.RelaGrade = RelaGrade;
        CustInfoM.Relation = Relation;
        CustInfoM.CompanyType = CompanyType;        
        CustInfoM.StaffCount = StaffCount == "" ? 0 : Convert.ToInt32(StaffCount);        
        CustInfoM.Source = Source;
        CustInfoM.Phase = Phase;//阶段
        CustInfoM.CustSupe = CustSupe;
        CustInfoM.Trade = Trade;//行业
        if (SetupDate != "" || SetupDate != string.Empty)
        {
            CustInfoM.SetupDate = Convert.ToDateTime(SetupDate);
        }
        
        CustInfoM.ArtiPerson = ArtiPerson;//法人代表
        CustInfoM.SetupMoney = SetupMoney == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(SetupMoney);//注册资本
        CustInfoM.SetupAddress = SetupAddress;
        CustInfoM.CapitalScale = CapitalScale == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(CapitalScale);//资产规模 
        CustInfoM.SaleroomY = SaleroomY == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(SaleroomY);
        CustInfoM.ProfitY = ProfitY == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(ProfitY);
        CustInfoM.TaxCD = TaxCD;
        CustInfoM.BusiNumber = BusiNumber;//营业执照号
        CustInfoM.IsTax = IsTax;
        CustInfoM.SellMode = SellMode;
        CustInfoM.SellArea = SellArea;
        CustInfoM.CountryID = Convert.ToInt32(CountryID); //国家地区
        CustInfoM.Province = Province;
        CustInfoM.City = City;
        CustInfoM.Tel = Tel;
        CustInfoM.ContactName = ContactName;
        CustInfoM.Mobile = Mobile;//手机
        CustInfoM.ReceiveAddress = ReceiveAddress;
        CustInfoM.WebSite = WebSite;
        CustInfoM.Post = Post;
        CustInfoM.email = email;
        CustInfoM.Fax = Fax;
        CustInfoM.OnLine = OnLine;
        CustInfoM.TakeType = Convert.ToInt32(TakeType);//交货方式
        CustInfoM.MoneyType = Convert.ToInt32(MoneyType);
        CustInfoM.CarryType = Convert.ToInt32(CarryType);//运货方式
        CustInfoM.BusiType = BusiType;
        CustInfoM.BillType = BillType;
        CustInfoM.PayType = Convert.ToInt32(PayType);//结算方式    
        CustInfoM.CurrencyType = Convert.ToInt32(CurrencyType);//结算币种
        CustInfoM.CreditManage = CreditManage;
        CustInfoM.MaxCredit = MaxCredit == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(MaxCredit);
        CustInfoM.MaxCreditDate = MaxCreditDate == "" ? 0 : Convert.ToInt32(MaxCreditDate);        
        CustInfoM.UsedStatus = UsedStatus;
        CustInfoM.Creator = Creator;//建单人
        if (CreatedDate != "" || CreatedDate != string.Empty)
        {
            CustInfoM.CreatedDate = Convert.ToDateTime(CreatedDate);
        }
        //CustInfoM.ModifiedDate = DateTime.Now;
        CustInfoM.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        CustInfoM.OpenBank = OpenBank;
        CustInfoM.AccountMan = AccountMan;
        CustInfoM.AccountNum = AccountNum;
        CustInfoM.Remark = Remark;

        CustInfoM.CustTypeManage = CustTypeManage;
        CustInfoM.CustTypeSell = CustTypeSell;
        CustInfoM.CustTypeTime = CustTypeTime;
        if (FirstBuyDate != "" || FirstBuyDate != string.Empty)
        {
            CustInfoM.FirstBuyDate = Convert.ToDateTime(FirstBuyDate);
        }
        CustInfoM.CanViewUser = CanUserID;//可查看该客户档案的人员ID
        CustInfoM.CanViewUserName = CanUserName;//可查看该客户档案的人员姓名
        
        CustInfoM.CompanyValues = context.Request.Params["CompanyValues"].ToString().Trim() == "" ? "" : context.Request.Params["CompanyValues"].ToString().Trim();
        CustInfoM.CatchWord = context.Request.Params["CatchWord"].ToString().Trim() == "" ? "" : context.Request.Params["CatchWord"].ToString().Trim();
        CustInfoM.ManageValues = context.Request.Params["ManageValues"].ToString().Trim() == "" ? "" : context.Request.Params["ManageValues"].ToString();
        CustInfoM.Potential = context.Request.Params["Potential"].ToString().Trim() == "" ? "" : context.Request.Params["Potential"].ToString();
        CustInfoM.Problem = context.Request.Params["Problem"].ToString().Trim() == "" ? "" : context.Request.Params["Problem"].ToString();
        CustInfoM.Advantages = context.Request.Params["Advantages"].ToString().Trim() == "" ? "" : context.Request.Params["Advantages"].ToString();
        CustInfoM.TradePosition = context.Request.Params["TradePosition"].ToString().Trim() == "" ? "" : context.Request.Params["TradePosition"].ToString();
        CustInfoM.Competition = context.Request.Params["Competition"].ToString().Trim() == "" ? "" : context.Request.Params["Competition"].ToString();
        CustInfoM.Collaborator = context.Request.Params["Collaborator"].ToString().Trim() == "" ? "" : context.Request.Params["Collaborator"].ToString();
        CustInfoM.ManagePlan = context.Request.Params["ManagePlan"].ToString().Trim() == "" ? "" : context.Request.Params["ManagePlan"].ToString();
        CustInfoM.Collaborate = context.Request.Params["Collaborate"].ToString().Trim() == "" ? "" : context.Request.Params["Collaborate"].ToString();
        Hashtable ht = GetExtAttr(context);
        
        //联系人信息数组串
        //string LinkManList = context.Request.Params["LinkManList"].ToString().Trim();
        LinkManModel LinkManM = new LinkManModel();
        if(CustInfoM.CustBig == "2")
        {
            LinkManM.Sex = context.Request.Params["Sex"].ToString().Trim();
            LinkManM.LinkType = context.Request.Params["LinkType"].ToString().Trim() == "" ? 0 : Convert.ToInt32(context.Request.Params["LinkType"].ToString().Trim());
            LinkManM.PaperNum = context.Request.Params["PaperNum"].ToString().Trim();
            if (context.Request.Params["Birthday"].ToString().Trim() != "" || context.Request.Params["Birthday"].ToString().Trim() != string.Empty)
            {
                LinkManM.Birthday = Convert.ToDateTime(context.Request.Params["Birthday"].ToString().Trim());
            }
            LinkManM.WorkTel = context.Request.Params["Tel"].ToString().Trim();
            LinkManM.Handset = context.Request.Params["Mobile"].ToString().Trim();
            LinkManM.Fax = context.Request.Params["Fax"].ToString().Trim();
            LinkManM.Position = context.Request.Params["Position"].ToString().Trim();
            LinkManM.Age = context.Request.Params["Age"].ToString().Trim();
            LinkManM.Post = context.Request.Params["Post"].ToString().Trim();
            LinkManM.MailAddress = context.Request.Params["email"].ToString().Trim();
            LinkManM.HomeTown = context.Request.Params["HomeTown"].ToString().Trim();
            LinkManM.NationalID = context.Request.Params["NationalID"].ToString().Trim();// == "" ? "0" : context.Request.Params["NationalID"].ToString().Trim();//民族
            LinkManM.CultureLevel = context.Request.Params["CultureLevel"].ToString().Trim();// == "" ? "0" : context.Request.Params["CultureLevel"].ToString().Trim();//所受教育
            LinkManM.Professional = context.Request.Params["Professional"].ToString().Trim();// == "" ? "0" : context.Request.Params["Professional"].ToString().Trim();//所学专业
            //if (!string.IsNullOrEmpty(context.Request.Params["NationalID"].ToString()))
            //{
            //    LinkManM.NationalID = context.Request.Params["NationalID"].ToString();
            //}
            //if (!string.IsNullOrEmpty(context.Request.Params["CultureLevel"].ToString()))
            //{
            //    LinkManM.CultureLevel = context.Request.Params["CultureLevel"].ToString();
            //}
            //if (!string.IsNullOrEmpty(context.Request.Params["Professional"].ToString()))
            //{
            //    LinkManM.Professional = context.Request.Params["Professional"].ToString();
            //}
            
            CustInfoM.RelaGrade = context.Request.Params["RelaGrade0"].ToString().Trim();
            CustInfoM.UsedStatus = context.Request.Params["UsedStatus0"].ToString().Trim();
            
            LinkManM.Professional = context.Request.Params["Professional"].ToString().Trim();//所学专业
            LinkManM.IncomeYear = context.Request.Params["IncomeYear"].ToString().Trim();
            LinkManM.FuoodDrink = context.Request.Params["FuoodDrink"].ToString().Trim();
            LinkManM.LoveMusic = context.Request.Params["LoveMusic"].ToString().Trim();
            LinkManM.LoveColor = context.Request.Params["LoveColor"].ToString().Trim();
            LinkManM.LoveSmoke = context.Request.Params["LoveSmoke"].ToString().Trim();
            LinkManM.LoveDrink = context.Request.Params["LoveDrink"].ToString().Trim();
            LinkManM.LoveTea = context.Request.Params["LoveTea"].ToString().Trim();
            LinkManM.LoveBook = context.Request.Params["LoveBook"].ToString().Trim();
            LinkManM.LoveSport = context.Request.Params["LoveSport"].ToString().Trim();
            LinkManM.LoveClothes = context.Request.Params["LoveClothes"].ToString().Trim();
            LinkManM.Cosmetic = context.Request.Params["Cosmetic"].ToString().Trim();
            LinkManM.Car = context.Request.Params["Car"].ToString().Trim();
            LinkManM.Nature = context.Request.Params["Nature"].ToString().Trim();
            LinkManM.AboutFamily = context.Request.Params["AboutFamily"].ToString().Trim();
            LinkManM.Appearance = context.Request.Params["Appearance"].ToString().Trim();
            LinkManM.AdoutBody = context.Request.Params["AdoutBody"].ToString().Trim();
        }
        
        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            if (CustInfoBus.CustInfoAdd(CustInfoM, LinkManM,ht))
                jc = new JsonClass("success", CustInfoM.CustNo, 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);           
        }
        else
        {
            if (CustInfoBus.UpdateCustInfo(CustInfoM,LinkManM, ht))
                jc = new JsonClass("success", CustInfoM.CustNo, 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
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
    
}