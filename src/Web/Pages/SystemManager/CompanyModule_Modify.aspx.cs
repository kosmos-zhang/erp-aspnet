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
using XBase.Business.SystemManager;
using XBase.Model.SystemManager;

public partial class Pages_SystemManager_CompanyModule_Modify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CompanyCD = Request.QueryString["CompanyCD"];
        if (CompanyCD != "")
        {
            CompanyModuleModel Model = new CompanyModuleModel();
            Model.CompanyCD = CompanyCD;
            //获取所有功能模块信息
            DataTable DataTable = CompanyModBus.GetSysModuleInfo();            
            //生成tree
            string ScriptShow = "<script type=\"text/javascript\">";
            ScriptShow += "var rows = new Object;";
            ScriptShow += "var rowsPidIndex = new Object;";
            ScriptShow += "rows={";
            ArrayList rowsPidIndex = new ArrayList();
            //显示所有功能模块信息            
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                DataRow row = DataTable.Rows[i];
                ScriptShow += "'" + row["ModuleID"].ToString() + "' : {'id':'" + row["ModuleID"].ToString() + "', 'pid':'" + row["ParentID"].ToString() + "', 'title':'" + row["ModuleName"].ToString() + "' }";
                if (i < DataTable.Rows.Count-1)
                {
                    ScriptShow += ",";
                }
                //父节点
                if (row["ModuleType"].ToString() == "S")
                {
                    rowsPidIndex.Add(row["ModuleID"].ToString());
                }
            }
            ScriptShow += "};";
            //初始化顶级目录
            ScriptShow += "rowsPidIndex[0] = new Array(";
            ScriptShow += getChildNode("0", DataTable);
            ScriptShow += ") ; ";
            //初始化父节点
            for (int i = 0; i < rowsPidIndex.Count;i++ )
            {
                ScriptShow += "rowsPidIndex["+rowsPidIndex[i].ToString()+"] = new Array(";
                ScriptShow += getChildNode(rowsPidIndex[i].ToString(), DataTable);
                ScriptShow += ") ; ";                
            }
            ScriptShow += "</script>";
            LblScriptShow.Text = ScriptShow;

            //获取该企业的所有模块信息
            DataTable CompanyModuleInfo = CompanyModBus.GetCompanyModuleInfo(CompanyCD);
            //生成被选择项
            string checkBoxCheckedTemp = ",";
            for(int i=0;i<CompanyModuleInfo.Rows.Count;i++)
            {
                DataRow row = CompanyModuleInfo.Rows[i];
                checkBoxCheckedTemp += row["ModuleID"].ToString() + ",";
            }
            string checkBoxChecked = "";
            checkBoxChecked += "<input type='hidden' id='CompanyCD' name='CompanyCD' value='" + CompanyCD + "'>";
            checkBoxChecked += "<script>";
            checkBoxChecked += "var ctree = new treeCheckBox( 'ctree'  , rows , rowsPidIndex ); ";
            checkBoxChecked += "ctree.iconPath='../../js/dtree/images/';";
            checkBoxChecked += "ctree.useCheckBox=true;";
            checkBoxChecked += "ctree.checkBoxName='menu[]';";
            checkBoxChecked += "ctree.checkBoxChecked='"+checkBoxCheckedTemp+"';";
            checkBoxChecked += "ctree.toString( Tc );";
            checkBoxChecked += "</script>";
            LblcheckBoxChecked.Text = checkBoxChecked;
        }

    }

    //获取子节点
    public string getChildNode(string ParentID,DataTable datatable)
    {
        string result = "";
        for (int i = 0; i < datatable.Rows.Count; i++)
        {
            DataRow row = datatable.Rows[i];
            if (row["ParentID"].ToString() == ParentID)
            {
                result += ",'" + row["ModuleID"].ToString() + "'";
            }
        }
        if (result.Length>1)
        {
            result = result.Substring(1, result.Length - 1);
        }        
        return result;
    }







}
