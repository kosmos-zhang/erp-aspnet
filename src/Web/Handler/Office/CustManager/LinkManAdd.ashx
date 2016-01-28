<%@ WebHandler Language="C#" Class="LinkManAdd" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
//using XBase.Business.Common;
using XBase.Common;

public class LinkManAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
   
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim(); //标示添加或修改
        
        //判断联系人唯一？       
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string CustNo = context.Request.Params["CustNo"].ToString().Trim();
        string LinkManName = context.Request.Params["LinkManName"].ToString().Trim();       
        
        string Sex = context.Request.Params["Sex"].ToString().Trim();
        string Important = context.Request.Params["Important"].ToString().Trim();
        string Company = context.Request.Params["Company"].ToString().Trim();
        string Appellation = context.Request.Params["Appellation"].ToString().Trim();
        string Department = context.Request.Params["Department"].ToString().Trim();
        string Position = context.Request.Params["Position"].ToString().Trim();
        string Operation = context.Request.Params["Operation"].ToString().Trim();
        string WorkTel = context.Request.Params["WorkTel"].ToString().Trim();
        string Fax = context.Request.Params["Fax"].ToString().Trim();
        string Handset = context.Request.Params["Handset"].ToString().Trim();
        string MailAddress = context.Request.Params["MailAddress"].ToString().Trim();
        string HomeTel = context.Request.Params["HomeTel"].ToString().Trim();
        string MSN = context.Request.Params["MSN"].ToString().Trim();
        string QQ = context.Request.Params["QQ"].ToString().Trim();
        string Post = context.Request.Params["Post"].ToString().Trim();
        string HomeAddress = context.Request.Params["HomeAddress"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string Age = context.Request.Params["Age"].ToString().Trim();
        string Likes = context.Request.Params["Likes"].ToString().Trim();
        string LinkType = context.Request.Params["LinkType"].ToString().Trim();
        string Birthday = context.Request.Params["Birthday"].ToString().Trim();
        string PaperType = context.Request.Params["PaperType"].ToString().Trim();
        string PaperNum = context.Request.Params["PaperNum"].ToString().Trim();
        string Photo = context.Request.Params["Photo"].ToString().Trim();
        string CanUserName = context.Request.Params["CanUserName"].ToString().Trim();
        string CanUserID = "," + context.Request.Params["CanUserID"].ToString().Trim() + ",";
       

        LinkManModel LinkMan = new LinkManModel();
        LinkMan.CompanyCD = CompanyCD;
        if (context.Request.Params["LinkID"].ToString().Trim() != "")
        {
            LinkMan.ID = Convert.ToInt32(context.Request.Params["LinkID"].ToString().Trim());
        }
        LinkMan.CustNo = CustNo;
        LinkMan.LinkManName = LinkManName;
        LinkMan.Sex = Sex;
        LinkMan.Important = Important;
        LinkMan.Company = Company;
        LinkMan.Appellation = Appellation;
        LinkMan.Department = Department;
        LinkMan.Position = Position;
        LinkMan.Operation = Operation;
        LinkMan.WorkTel = WorkTel;
        LinkMan.Fax = Fax;
        LinkMan.Handset = Handset;
        LinkMan.MailAddress = MailAddress;
        LinkMan.HomeTel = HomeTel;
        LinkMan.MSN = MSN;
        LinkMan.QQ = QQ;
        LinkMan.Post = Post;
        LinkMan.HomeAddress = HomeAddress;
        LinkMan.Remark = Remark;
        LinkMan.Age = Age;
        LinkMan.Likes = Likes;
        LinkMan.LinkType = Convert.ToInt32(LinkType);
        if (Birthday != "" || Birthday != string.Empty)
        {
            LinkMan.Birthday = Convert.ToDateTime(Birthday);
        }
        
        LinkMan.PaperType = PaperType;
        LinkMan.PaperNum = PaperNum;
        LinkMan.Photo = Photo;
        LinkMan.ModifiedDate = DateTime.Now;
        LinkMan.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        LinkMan.CanViewUser = CanUserID;
        LinkMan.CanViewUserName = CanUserName;
        LinkMan.CreatedDate = DateTime.Now.ToString();
        LinkMan.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

        LinkMan.HomeTown = context.Request.Params["HomeTown"].ToString();
        if (!string.IsNullOrEmpty(context.Request.Params["NationalID"].ToString()))
        {
            LinkMan.NationalID =context.Request.Params["NationalID"].ToString();
        }
        LinkMan.birthcity = context.Request.Params["Birthcity"].ToString();
        if (!string.IsNullOrEmpty(context.Request.Params["CultureLevel"].ToString()))
        {
            LinkMan.CultureLevel =context.Request.Params["CultureLevel"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request.Params["Professional"].ToString()))
        {
            LinkMan.Professional =context.Request.Params["Professional"].ToString();
        }
        LinkMan.GraduateSchool = context.Request.Params["GraduateSchool"].ToString();
        LinkMan.IncomeYear = context.Request.Params["IncomeYear"].ToString();
        LinkMan.FuoodDrink = context.Request.Params["FuoodDrink"].ToString();
        LinkMan.LoveMusic = context.Request.Params["LoveMusic"].ToString();
        LinkMan.LoveColor = context.Request.Params["LoveColor"].ToString();
        LinkMan.LoveSmoke = context.Request.Params["LoveSmoke"].ToString();
        LinkMan.LoveDrink = context.Request.Params["LoveDrink"].ToString();
        LinkMan.LoveTea = context.Request.Params["LoveTea"].ToString();
        LinkMan.LoveBook = context.Request.Params["LoveBook"].ToString();
        LinkMan.LoveSport = context.Request.Params["LoveSport"].ToString();
        LinkMan.LoveClothes = context.Request.Params["LoveClothes"].ToString();
        LinkMan.Cosmetic = context.Request.Params["Cosmetic"].ToString();
        LinkMan.Nature = context.Request.Params["Nature"].ToString();

        LinkMan.Appearance = context.Request.Params["Appearance"].ToString();
        LinkMan.AdoutBody = context.Request.Params["AdoutBody"].ToString();
        LinkMan.AboutFamily = context.Request.Params["AboutFamily"].ToString();
        LinkMan.Car = context.Request.Params["Car"].ToString();
        LinkMan.LiveHouse = context.Request.Params["LiveHouse"].ToString();
        LinkMan.ProfessionalDes = context.Request.Params["ProfessionalDes"].ToString().Trim();

        
        JsonClass jc;
        
        //判断为添加或修改操作
        if (Action == "1") //添加
        {
            int Linkid = LinkManBus.LinkManAdd(LinkMan);

            if (Linkid > 0)
                jc = new JsonClass("success", Linkid.ToString(), 1);
            else
                jc = new JsonClass("faile", "", 0);
            context.Response.Write(jc);

        }
        else//修改
        {
            if (LinkManBus.UpdateLinkMan(LinkMan))
                jc = new JsonClass("success", LinkMan.ID.ToString(), 1);
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

}