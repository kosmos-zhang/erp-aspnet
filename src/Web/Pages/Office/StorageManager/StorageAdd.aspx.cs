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
using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Business.Common;
using System.Text.RegularExpressions;
public partial class Pages_Office_StorageManager_StorageAdd : BasePage
{

    private string companyCD = string.Empty;

    #region StorageID
    public new int ID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserList.Attributes.Add("onclick", "treeview1.show()");
        txtUserList.Attributes.Add("onkeydown", "return false;");
       

        companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
        if (!IsPostBack)
        {
        txtExecutorID.Value =    ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString ();
        UsertxtExecutor.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;

            txtStorageNo.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            txtStorageNo.ItemTypeID = ConstUtil.CODING_RULE_Storage;
            if (this.ID > 0)
            {
                this.div_StorageNo_uc.Attributes.Add("style", "display:none;");
                this.div_StorageNo_Lable.Attributes.Add("style", "display:block;");
                this.t_Edit.Attributes.Add("style", "display:inline");
                LoadStorageInfo();
            }
            else
            {
                this.div_StorageNo_uc.Attributes.Add("style", "display:block;");
                this.div_StorageNo_Lable.Attributes.Add("style", "display:none;");
                this.t_Add.Attributes.Add("style", "display:inline");
            }


            //模板列表模块ID
            hidModuleID.Value = ConstUtil.MODULE_ID_STORAGE_STORAGEINFO;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                btnBack.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.MODULE_ID_STORAGE_STORAGEINFO, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
            }
            else
            {
                //返回按钮不可见
                btnBack.Visible = false;
            }
        }
    }



    #region 加载仓库信息
    protected void LoadStorageInfo()
    {
        DataTable dt = StorageBus.GetStorageDetailInfo(companyCD, this.ID);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            this.lbStorageNo.Text = dr["StorageNo"] == null ? "" : dr["StorageNo"].ToString().Trim();
            this.txtStorageName.Text = dr["StorageName"] == null ? "" : dr["StorageName"].ToString().Trim();
            this.sltType.SelectedValue = dr["StorageType"] == null ? "" : dr["StorageType"].ToString().Trim();
            this.txtRemark.Text = dr["Remark"] == null ? "" : dr["Remark"].ToString().Trim();
            this.sltUsedStatus.Text = dr["UsedStatus"] == null ? "" : dr["UsedStatus"].ToString().Trim();
            this.txtIndentityID.Value = dr["ID"] == null ? "" : dr["ID"].ToString().Trim();

            //string userList = dr["CanViewUser"] == null ? "" : dr["CanViewUser"].ToString().Trim();
            //if (string.IsNullOrEmpty(userList))
            //{
            //    txtUserList.Text = "";
            //    txtUserListHidden.Value = "";
            //}
            //else
            //{
            //    string[] sArray=Regex.Split(userList,"@@",RegexOptions.IgnoreCase);
            //    txtUserList.Text = sArray[1]; 
            //    txtUserListHidden.Value = sArray[0];
            //}
 
            txtUserListHidden.Value = dr["CanViewUser"] == null ? "" : dr["CanViewUser"].ToString().Trim();
            if (!string.IsNullOrEmpty(txtUserListHidden.Value.Trim()))
            {
               DataTable dtName= StorageDBHelper.GetEmployeeNameByID( dr["CanViewUser"].ToString());
                string [] temp=dr["CanViewUser"].ToString().Split (',');
                string tmpName=string .Empty ;
                for (int a = 0; a < temp.Length; a++)
                {
                    if (dtName.Rows.Count > 0)
                    {
                        for (int b = 0; b < dtName.Rows.Count; b++)
                        {
                            string id = dtName.Rows[b]["id"] == null ? "" : dtName.Rows[b]["id"].ToString();
                            if ( int.Parse(id) == int.Parse (temp[a]))
                            {
                                if (a == (temp.Length - 1))
                                {
                                    tmpName = tmpName + dtName.Rows[b]["EmployeeName"];
                                }
                                else
                                {
                                    tmpName = tmpName + dtName.Rows[b]["EmployeeName"] + ",";
                                }
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                txtUserList.Text = tmpName;
            }
            else
            {
                txtUserList.Text = string.Empty;
            }

    

            txtExecutorID.Value = dr["StorageAdmin"] == null ? "" : dr["StorageAdmin"].ToString().Trim();
            UsertxtExecutor.Value = dr["StorageAdminName"] == null ? "" : dr["StorageAdminName"].ToString().Trim();


           

        }

    }
    #endregion


    #region 拼音缩写转换
    [AjaxPro.AjaxMethod]
    public static string GetPYShort(string strInput)
    {
        return XBase.Common.PYShortUtil.GetPYString(strInput);
    }
    #endregion
}
