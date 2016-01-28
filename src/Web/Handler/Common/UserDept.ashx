<%@ WebHandler Language="C#" Class="UserDept" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Business.Office.CustManager;
using XBase.Business.Office.HumanManager;

using System.Collections;

public class UserDept : BaseHandler
{

    private static XBase.Business.Personal.MessageBox.GetUserDepList bll = new XBase.Business.Personal.MessageBox.GetUserDepList();
    protected override void ActionHandler(string action)
    {      
        switch (action.ToLower())
        {           
            case "loaduserlist":
                LoadUserList();
                break;
            case "loaduserlistwithdepartment":
                LoadUserListWithDepartment();
                break;          
            case "loaddepartmentlist":
                LoadDepartmentList();
                break;
            case "getlinkmanex":
                outputLinkMan();
                break;
            case "custclass":
                LoadCustClass();
                break;
            case "deptquarter":
                LoadDeptQuarter();
                break;
            case "doctype":
                LoadDocType();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }


    private void outputLinkMan()
    {
        DataTable linkman = XBase.Business.Office.CustManager.LinkManBus.GetLinkManListEx(UserInfo.CompanyCD);
        
        ArrayList custs = new ArrayList();
        ArrayList custNos = new ArrayList();
        foreach (DataRow row in linkman.Rows)
        {
            if (!custNos.Contains(row["CustNo"].ToString()))
            {
                custNos.Add(row["CustNo"].ToString());
                custs.Add(row);
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow cust in custs)
        {
            if (cust["CustNam"].ToString().Trim() == string.Empty)
            {
                continue;
            }
            
            DataRow[] rows = linkman.Select("CustNo='" + cust["CustNo"].ToString() + "' AND Handset<> ' '");

            if (sb[sb.Length - 1] == '}' && rows.Length>0)
            {
                sb.Append(",");
            }
         

           
            if (rows.Length == 0)
                continue;

            sb.Append("{text:\"" + cust["CustNam"].ToString() + "\",");
            sb.Append("value:\"" + cust["CustNo"].ToString() + "\",");
            sb.Append("nodeType:1,");
            sb.Append("subNodes:[");
            foreach (DataRow row2 in rows)
            {
                if (sb[sb.Length - 1] == '}')
                {
                    sb.Append(",");
                }

                sb.Append("{text:\"" + row2["LinkManName"].ToString() + "&nbsp;&nbsp;" + row2["Handset"].ToString() + "\",");
                sb.Append("value:\"" + row2["ID"].ToString() + "\",");
                sb.Append("nodeType:2,");
                sb.Append("subNodes:[]}");
            }

            sb.Append("]}");


        }

        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");       

    }

    private void LoadDepartmentList()
    {
        depts = bll.GetDeptInfo(UserInfo.CompanyCD);
        //SuperDeptID ,ID

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = depts.Select("SuperDeptID IS NULL");

        sb.Append("[");
        foreach (DataRow row in rows)
        {
            LoadSubDept2(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");       
        
        
    }

    private void LoadSubDept2(DataRow p, StringBuilder sb)
    {
        //do self
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        sb.Append("{");
        sb.Append("nodeType:1,");
        sb.Append("text:\"" + p["DeptName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",subNodes:[");
        //do users of this dept    
        DataRow[] rows = depts.Select("SuperDeptID=" + p["ID"].ToString());
        foreach (DataRow row in rows)
        {
            LoadSubDept2(row, sb);
        }

        sb.Append("]}");
    }
    
    private void LoadUserList()
    {
        DataTable users = bll.GetUserInfo(UserInfo.CompanyCD);
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow row in users.Rows)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("nodeType:2,");
            sb.Append("text:\"" + row["EmployeesName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",subNodes:[]}");
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private DataTable userlist = new DataTable();
    private DataTable depts = new DataTable();
    private void LoadUserListWithDepartment()
    {
        userlist = bll.GetUserInfo(UserInfo.CompanyCD);//获取所有在职员工的：ID,EmployeesName,DeptID
        depts = bll.GetDeptInfo(UserInfo.CompanyCD);//获取公司部门信息:ID,CompanyCD,DeptNO,SuperDeptID,PYShort,DeptName,AccountFlag
        //SuperDeptID ,ID

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = depts.Select("SuperDeptID IS NULL");//赛选上级机构为NULL的行（即顶级部门）
        
        sb.Append("[");
        foreach (DataRow row in rows)//循环每部门里的子部门
        {
            LoadSubDept(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");       
    }
    private void LoadSubDept(DataRow p,StringBuilder sb)
    { 
        //do self
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        sb.Append("{");
        sb.Append("nodeType:1,");
        sb.Append("text:\"" + p["DeptName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",subNodes:[");
        
        //do users of this dept
        DataRow[] users = userlist.Select("DeptID="+p["ID"].ToString());
        foreach (DataRow row in users)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("nodeType:2,");
            sb.Append("text:\"" + row["EmployeesName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",subNodes:[]}");
        }
        
        DataRow[] rows = depts.Select("SuperDeptID=" + p["ID"].ToString());
        foreach (DataRow row in rows)
        {
            LoadSubDept(row, sb);
        }

        sb.Append("]}");
    }

    private DataTable dtCustClass = new DataTable();
    private void LoadCustClass()
    {
        dtCustClass = CustInfoBus.GetCustClass();

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = dtCustClass.Select("SupperID = 0");//赛选父节点为0的行

        sb.Append("[");
        foreach (DataRow row in rows)//循环每节点里的子节点
        {
            LoadSubClass(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private int nodeLevel = 0;
    private void LoadSubClass(DataRow p, StringBuilder sb)
    {
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        DataRow[] rows = dtCustClass.Select("SupperID=" + p["ID"].ToString());
        string nodeType = nodeLevel.ToString();
        if (rows.Length == 0)
        {
            nodeType = "-1";
        }
        
        sb.Append("{");
        sb.Append("nodeType:" + nodeLevel + ",");
        sb.Append("text:\"" + p["CodeName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",subNodes:[");

        nodeLevel++;
       
        foreach (DataRow row in rows)
        {
            LoadSubClass(row, sb);
        }
        nodeLevel--;

        sb.Append("]}");
    }

    private DataTable dtDeptQuarter = new DataTable();
    private void LoadDeptQuarter()
    {
        dtDeptQuarter = DeptQuarterBus.GetQuarterType();

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = dtDeptQuarter.Select("SuperQuarterID = 0");//赛选父节点为0的行

        sb.Append("[");
        foreach (DataRow row in rows)//循环每节点里的子节点
        {
            LoadQuarter(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private int nodeQuarter = 0;
    private void LoadQuarter(DataRow p, StringBuilder sb)
    {
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        DataRow[] rows = dtDeptQuarter.Select("SuperQuarterID=" + p["id"].ToString());
        string nodeType = nodeQuarter.ToString();
        if (rows.Length == 0)
        {
            nodeType = "-1";
        }

        sb.Append("{");
        sb.Append("nodeType:" + nodeQuarter + ",");
        sb.Append("text:\"" + p["QuarterName"].ToString() + "\"");
        sb.Append(",value:\"" + p["id"].ToString() + "\"");
        sb.Append(",subNodes:[");

        nodeQuarter++;

        foreach (DataRow row in rows)
        {
            LoadQuarter(row, sb);
        }
        nodeQuarter--;

        sb.Append("]}");
    }
   
    private DataTable dtDocType = new DataTable();
    private void LoadDocType()
    {
        dtDocType = XBase.Business.Office.AdminManager.DocBus.GetDocType();

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = dtDocType.Select("SupperID = 0");//赛选父节点为0的行

        sb.Append("[");
        foreach (DataRow row in rows)//循环每节点里的子节点
        {
            LoadDocType(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private int nodeL = 0;
    private void LoadDocType(DataRow p, StringBuilder sb)
    {
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        DataRow[] rows = dtDocType.Select("SupperID=" + p["ID"].ToString());
        string nodeType = nodeL.ToString();
        if (rows.Length == 0)
        {
            nodeType = "-1";
        }

        sb.Append("{");
        sb.Append("nodeType:" + nodeL + ",");
        sb.Append("text:\"" + p["CodeName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",subNodes:[");

        nodeL++;

        foreach (DataRow row in rows)
        {
            LoadDocType(row, sb);
        }
        nodeL--;

        sb.Append("]}");
    }

}