<%@ WebHandler Language="C#" Class="DeptQuarter_Query" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/13
 * 描    述： 机构岗位
 * 修改日期： 2009/04/13
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;

public class DeptQuarter_Query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取请求动作
        string action = context.Request.Params["Action"];
        //获取机构岗位ID
        string quarterID = context.Request.Params["QuarterID"];
        //初期化机构岗位树
        if ("InitPage".Equals(action))
        {
            string getMess = DeptQuarterBus.InitDeptQuarterInfo();
            //返回生成的组织机构树 
            context.Response.Write(getMess ); 
        }
        else if ("InitTree".Equals(action))
        {
            //定义变量

            string ID = context.Request.Params["DeptID"];
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
                                    + " alt='" + deptName + "'><a  href='#' onclick=\"GetInfoByDeptID('" + deptID + "', '" + deptName + "')\"><div id='divDeptName_" + deptID
                                    + "'>" + " " + deptName + "</div></a></div>");
                    sbDeptTree.Append("<div id='divSub_" + deptID + "' style='display:none;' class='" + showClass + "'></div></td></tr>");
                    sbDeptTree.Append("</table>");
                }
            }
            //返回生成的组织机构树 
            context.Response.Write(sbDeptTree.ToString());
        }
            
        //编辑组织机构信息  
        else if ("InitQuarter".Equals(action))
        {
            //返回生成的组织机构树 
            context.Response.Write(DeptQuarterBus.GetQuarterInfoWithID(quarterID));
        }
        else if ("InitQuarterByDeptID".Equals(action))
        {
            string deptID = context.Request.Params["deptID"];
            string getMess = "<div id='divDept_" + deptID + "'>" + DeptQuarterBus.GetQuarterInfoWithDeptID(deptID, quarterID) + "</div>";


            context.Response.Write(getMess);
        }
        //编辑组织机构信息 
        else if ("EditQuarter".Equals(action))
        {
            //定义Model变量
            DeptQuarterModel model = EditRequstData(context.Request);
            //定义Json返回变量
            JsonClass jc;
            //编码能用时
            if (model != null)
            {
                //编辑更新机构岗位
                jc = EditQuarterInfo(model);
            }
            else
            {
                jc = new JsonClass("", "", 2);
            }
            //输出响应
            context.Response.Write(jc);
        }
        //获取组织机构信息 
        else if ("GetQuarterInfo".Equals(action))
        {
            //获取组织机构信息
            DataTable dtDeptInfo = DeptQuarterBus.GetDeptQuarterInfoWithID(quarterID);
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
        //删除岗位信息
        else if ("DeleteQuarter".Equals(action))
        {
            //判断组织是否有自组织机构
            DataTable dtSubDeptInfo = DeptQuarterBus.GetSubQuarterInfoWithID(quarterID);
            //存在子组织机构时
            if (dtSubDeptInfo != null && dtSubDeptInfo.Rows.Count > 0)
            {
                //输出响应 返回不执行删除
                context.Response.Write(new JsonClass("", "", 2));
            }
            else
            {
                //删除组织机构信息
                bool isSucc = DeptQuarterBus.DeleteDeptQuarterInfo(quarterID);
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

    /// <summary>
    /// 编辑机构岗位信息
    /// </summary>
    /// <param name="model">机构岗位信息</param>
    /// <returns></returns>
    private JsonClass EditQuarterInfo(DeptQuarterModel model)
    {
        JsonClass jc;
        //执行保存操作
        bool isSucce = DeptQuarterBus.SaveDeptQuarterInfo(model);
        //保存成功时
        if (isSucce)
        {
            jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.QuarterNo, 1);
        }
        //保存未成功时
        else
        {
            jc = new JsonClass("", "", 0);
        }

        return jc;
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private DeptQuarterModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        DeptQuarterModel model = new DeptQuarterModel();
        //编辑标识
        model.EditFlag = request.QueryString["EditFlag"];
        //获取编号
        string quarterNo = request.QueryString["QuarterNo"];

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(quarterNo))
            {
                //获取编码规则编号
                string codeRuleID = request.QueryString["CodeRuleID"];
                //通过编码规则代码获取编号
                quarterNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_DEPTQUARTER
                                , ConstUtil.CODING_RULE_COLUMN_DEPTQUARTER_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_DEPTQUARTER
                                , ConstUtil.CODING_RULE_COLUMN_DEPTQUARTER_NO, quarterNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置岗位编号
        model.QuarterNo = quarterNo;

     
        //所属机构
        model.DeptID = request.QueryString["DeptID"];
        //上级岗位
        model.SuperQuarterID = request.QueryString["SuperQuarter"];
        //岗位名称
        model.QuarterName = request.QueryString["QuarterName"];
        //拼音代码
        model.PYShort = request.QueryString["PYShort"];
        //是否关键岗位
        model.KeyFlag = request.QueryString["KeyFlag"];
        //岗位分类
        model.TypeID = request.QueryString["TypeID"];
        //岗位级别
        model.LevelID = request.QueryString["LevelID"];
        //描述信息
        model.Description = request.QueryString["Description"];
        //启用状态
        model.UsedStatus = request.QueryString["UsedStatus"];
        //附件
        model.Attachment = request.QueryString["Attachment"];
        model.PageAttachment = request.QueryString["PageAttachment"];
        //岗位职责
        model.Duty = request.QueryString["Duty"];
        //任职资格
        model.DutyRequire = request.QueryString["DutyRequire"];

        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}