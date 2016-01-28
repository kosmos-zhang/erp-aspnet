using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_ProductPriceAdd : BasePage
{
    public int intOtherCorpInfoID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intOtherCorpInfoID"], out tempID);
            return tempID;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidModuleID.Value = ConstUtil.Menu_SerchProductPrice;
            this.txtChenger.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.hf_Confirmor.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.hf_Creator.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.UserChengerName.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.txt_ConfirmorName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.txt_CreatorName.Text = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
            this.txt_CreateDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txt_ConfirmDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txt_ChangeDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txtModifiedDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                string requestParam = Request.QueryString.ToString();
                //通过参数个数来判断是否从菜单过来
                int firstIndex = requestParam.IndexOf("&");
                //从列表过来时
                if (firstIndex > 0)
                {
                    //获取列表的查询条件
                    string searchCondition = requestParam.Substring(firstIndex);
                    //去除参数
                    searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddProductPrice, string.Empty);
                    //设置检索条件
                    hidSearchCondition.Value = searchCondition;
                    //迁移页面
                    btnback.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                }
                
             if (Request["intOtherCorpInfoID"] != "" && Request["intOtherCorpInfoID"] != null)
              {
                this.divConfirmor.Attributes.Add("style", "display:block;");
                this.btn_AD.Attributes.Add("style", "display:block;");
                DataTable dt = ProductPriceChangeBus.GetProductPriceInfoByID(int.Parse(Request["intOtherCorpInfoID"]));
                   if (dt.Rows.Count > 0)
                   {
                       txt_ChangeNo.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ChangeNo");
                       txt_Title.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Title");
                       txt_ProductName.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProductName");
                       hf_ProductID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProductID");
                       this.txtModifiedUserID.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ModifiedUserID");
                       string sell=GetSafeData.ValidateDataRow_String(dt.Rows[0], "StandardSell");
                       txt_StandardSell.Text =sell;
                       this.txtModifiedDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ModifiedDate");

                       txt_SellTax.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellTax");
                       txt_StandardSellNew.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "StandardSellNew");
                       txt_SellTaxNew.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "SellTaxNew");
                       txt_TaxRate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TaxRate");
                       txt_Discount.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Discount");
                       txt_TaxRateNew.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TaxRateNew");
                       txt_DiscountNew.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "DiscountNew");



                       txt_ChangeDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ChangeDate");
                       this.txtChenger.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Chenger");
                       this.UserChengerName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ChengeName");

                       if (!string.IsNullOrEmpty(GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmorName")))
                       this.txt_ConfirmorName.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmorName");
                       this.txt_CreatorName.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CreatorName");

                       if (!string.IsNullOrEmpty(GetSafeData.ValidateDataRow_String(dt.Rows[0], "Confirmor")))
                       this.hf_Confirmor.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Confirmor");
                       this.hf_Creator.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Creator");

                       txt_Remark.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Remark");
                       sel_BillStatus.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BillStatus");
                       txt_CreateDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CreateDate");
                       if (!string.IsNullOrEmpty(GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmDate")))
                       txt_ConfirmDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmDate");
                       if (sel_BillStatus.Value == "1")
                       {
                           this.btnAdd.Attributes.Add("style", "display:block;float:left");
                           this.btnsave.Attributes.Add("style", "display:none;float:left");
                           this.btnsure.Attributes.Add("style", "display:none;float:left");
                           this.btn_AD.Attributes.Add("style", "display:block;float:left");
                           this.divConfirmor.Attributes.Add("style", "display:none;");
                       }
                       else if (sel_BillStatus.Value == "5")
                       {
                           this.btnAdd.Attributes.Add("style", "display:none;float:left");
                           this.btnsave.Attributes.Add("style", "display:block;float:left");
                           this.btnsure.Attributes.Add("style", "display:block;float:left");
                           this.btn_AD.Attributes.Add("style", "display:none;float:left");
                       }
                       this.btnback.Attributes.Add("style", "display:block;float:left");
                   }
               
            }
            //this.hf_Chenger.Value = "5";
            //this.hf_Confirmor.Value = "5";
            //this.hf_Creator.Value = "5";
            //this.txt_ChengerName.Text = "管理员";
            //this.txt_ConfirmorName.Value = "管理员";
            //this.txt_CreatorName.Text = "管理员";
        }
    }
}
