using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_RoleFunctionEdit :BasePage
{
    private static System.Text.RegularExpressions.Regex safeJSON = new System.Text.RegularExpressions.Regex("[\\n\\r]");
    protected string GetSafeJSONString(string input)
    {
        string output = input.Replace("\"", "\\\"");
        output = safeJSON.Replace(output, "<br>");

        return output;

    }

    private void OutputTreeViewJs()
    { 
       string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

       DataTable rolesDT =  RoleInfoBus.GetRoleInfo(CompanyCD);

       StringBuilder sb = new StringBuilder();

       sb.AppendLine("var roleNodes = [{text:\"角色权限赋值\",");
       sb.Append("value:-1,");
       sb.Append("nodeType:0,");
       sb.Append("subNodes:[");

       foreach (DataRow row in rolesDT.Rows)
       {
           
           if (sb[sb.Length - 1] == '}')
           {
               sb.Append(",");
           }
           sb.Append("{text:\"" + GetSafeJSONString(row["RoleName"].ToString()) + "\",");
           sb.Append("value:\"" + row["RoleID"].ToString() + "\",");
           sb.Append("nodeType:1,");
           sb.Append("subNodes:[]}");
       }

       sb.AppendLine("]}];");


      
       //DataTable modulesDT = CompanyModuleBus.GetParentSysModuleInfo(CompanyCD);
       DataTable modulesDT = CompanyModuleBus.GetCompanyModuleInfo2(CompanyCD);
       DataTable functionsDT = XBase.Business.SystemManager.ModuleFunBus.GetAllModuleFunInfo();

       sb.Append("var moduleNodes = [");
       DataRow[] topRows = modulesDT.Select("ParentID is null");
       foreach (DataRow row in topRows)
       {
           if (sb[sb.Length - 1] == '}')
           {
               sb.Append(",");
           }
           sb.Append("{text:\"" + GetSafeJSONString(row["ModuleName"].ToString()) + "\",");
           sb.Append("value:\"" + row["ModuleID"].ToString() + "\",");
           sb.Append("nodeType:1,");
           sb.Append("subNodes:[");

           BuildMoudleTreeJS(sb, functionsDT,modulesDT, row["ModuleID"].ToString());
           
           sb.Append("]}");

           //break;
       }

       sb.AppendLine("];");


       ClientScript.RegisterStartupScript(this.GetType(), "treenodesblock", sb.ToString(), true);
       
    }

    private void BuildMoudleTreeJS(StringBuilder sb,DataTable dt2, DataTable dt, string pid)
    {
        DataRow[] topRows = dt.Select("ParentID =" + pid);
        foreach (DataRow row in topRows)
        {
            string mid = row["ModuleID"].ToString();

            if (sb[sb.Length - 1] == '}')
            {
                sb.Append(",");
            }
            sb.Append("{text:\"" + GetSafeJSONString(row["ModuleName"].ToString()) + "\",");
            sb.Append("value:\"" + mid + "\",");
            sb.Append("nodeType:1,");
            sb.Append("subNodes:[");

            if (dt.Select("ParentID =" + mid).Length == 0)
            {
                if (dt2.Select("FunctionType='1' AND ModuleID=" + mid).Length > 0)
                {
                    sb.Append("{text:\"查看权限\",value:\"" + mid + "_1\",nodeType:2,subNodes:[]}");
                }
                if (dt2.Select("FunctionType='2' AND ModuleID=" + mid).Length > 0)
                {
                    if (sb[sb.Length - 1] == '}')
                    {
                        sb.Append(",");
                    }
                    sb.Append("{text:\"操作权限\",value:\"" + mid + "_2\",nodeType:2,subNodes:[]}");
                }
            }
            else
            {
                BuildMoudleTreeJS(sb,dt2, dt, mid);
            }
            sb.Append("]}");
        }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
       //if (!Page.IsPostBack)
       // {
            OutputTreeViewJs();
       // }
    }

}
