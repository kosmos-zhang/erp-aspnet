<%@ WebHandler Language="C#" Class="DeptInfo_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/09
 * 描    述： 组织机构
 * 修改日期： 2009/04/09
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;

public class DeptInfo_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        //获取组织机构ID
        string ID = context.Request.Params["DeptID"];
        //初期化组织机构树时
        if ("InitTree".Equals(action))
        {
            //定义变量
         
           
            StringBuilder sbDeptTree = new StringBuilder();
            //获取组织机构数据
            DataTable dtDeptInfo = DeptInfoBus.SearchDeptInfo(ID);
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
                    string deptName = GetSafeData.ValidateDataRow_String(dtDeptInfo.Rows[i], "DeptName");
                    //获取父组织机构
                    string superDeptID = GetSafeData.GetStringFromInt(dtDeptInfo.Rows[i], "SuperDeptID");
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
        //获取组织机构信息
        else if ("GetDeptInfo".Equals(action))
        {
            //获取组织机构信息
            DataTable dtDeptInfo = DeptInfoBus.GetDeptInfoWithID(ID);
            //定义放回变量
            StringBuilder sbReturn = new StringBuilder();
            sbReturn.Append("{");
            sbReturn.Append("data:");
            sbReturn.Append(JsonClass.DataTable2Json(dtDeptInfo));
            sbReturn.Append("}");
            context.Response.ContentType = "text/plain";
            //返回数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();
        }
        //删除组织机构信息
        else if ("DeleteDeptInfo".Equals(action))
        {
            //判断组织是否有自组织机构
            DataTable dtSubDeptInfo = DeptInfoBus.SearchDeptInfo(ID);
            //存在子组织机构时
            if (dtSubDeptInfo != null && dtSubDeptInfo.Rows.Count > 0)
            {
                //输出响应 返回不执行删除
                context.Response.Write(new JsonClass("", "", 2));
            }
            else
            {
                DataTable dtISQuter = DeptInfoBus.ISDequter(ID);
                if (dtISQuter != null && dtISQuter.Rows.Count > 0)
                {
                    context.Response.Write(new JsonClass("", "", 4));
                    return;
                }
                else
                {
                    if (DeptInfoBus.ISHavePerson(ID) > 0)
                    {
                        context.Response.Write(new JsonClass("", "", 5));
                    }
                    else
                    {

                        //删除组织机构信息
                        bool isSucc = DeptInfoBus.DeleteDeptInfo(ID);
                        //删除成功
                        if (isSucc)
                        {
                            //输出响应
                            context.Response.Write(new JsonClass("", "", 1));
                        }
                        //删除失败
                        else
                        {
                            //输出响应
                            context.Response.Write(new JsonClass("", "", 0));
                        }
                    }
                }
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}