<%@ WebHandler Language="C#" Class="Tree" %>

using System;
using System.Web;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using XBase.Common;
using System.Text;
using System.Data;
public class Tree : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string action = context.Request.Params["Action"];
        //获取组织机构ID
        string ID = context.Request.Params["DeptID"];
        string deptNO = context.Request.Params["deptNO"];
        string TableName = context.Request.Params["TableName"];
        //初期化组织机构树时
        if ("InitTree".Equals(action))
        {
            //定义变量
            StringBuilder sbDeptTree = new StringBuilder();
           string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           DataTable dtDept = CategorySetBus.SearchDeptInfo(CompanyCD, ID,TableName);
           DataView dataView = dtDept.DefaultView;
           dataView.RowFilter = "TypeFlag='" + deptNO + "'";
           DataTable dtnew = new DataTable();
           dtnew = dataView.ToTable();
           DataTable dtDeptInfo = dtnew;
            //组织机构数据存在时
            if (dtDeptInfo != null && dtDeptInfo.Rows.Count > 0)
            {
                //获取记录数
                int deptCount = dtDeptInfo.Rows.Count;
                //遍历所有组织机构，以显示数据
                for (int i = 0; i < dtDeptInfo.Rows.Count; i++)
                {
                    //获取组织机构ID
                    string deptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "ID");
                    //或组组织机构名称
                    string deptName = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "CodeName");
                    //获取父组织机构
                    string superDeptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "SupperID");
                    //获取是否有子组织机构
                    int subCount = GetSafeData.ValidateDataRow_Int(dtDeptInfo.Rows[i], "SubCount");

                    //样式名称
                    string className = "file";
                    //Javascript事件名
                    string jsAction = "";
                    //样式表名称
                    string showClass = "list";

                    //有子结点时
                    if (subCount > 0)
                    {
                        //最后一个结点
                        if (i == deptCount - 1)
                        {
                            className = "folder_close_end";
                            showClass = "list_last";
                        }
                        else
                        {
                            className = "folder_close";
                        }
                        jsAction = "onclick=\"ShowDeptTree('" + deptID + "');\"";
                    }
                    else if (i == deptCount - 1)
                    {
                        className = "file_end";
                    }
                    //生成子树代码
                    sbDeptTree.Append("<table border='0' cellpadding='0' cellspacing='0'>");
                    sbDeptTree.Append("<tr><td><div id='divSuper_" + deptID + "' class='" + className + "' " + jsAction
                                    + " alt='" + deptName + "'><a  href='#' onclick=\"SetSelectValue('"
                                    + deptID + "','" + deptName + "','" + superDeptID + "');\"><div id='divDeptName_" + deptID
                                    + "'>" + " " + deptName + "</div></a></div>");
                    sbDeptTree.Append("<div id='divSub_" + deptID + "' style='display:none;' class='" + showClass + "'></div></td></tr>");
                    sbDeptTree.Append("</table>");
                }
            }
            //返回生成的组织机构树 
            context.Response.Write(sbDeptTree.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}