<%@ WebHandler Language="C#" Class="DeptInfo_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/09
 * 描    述： 新建组织机构
 * 修改日期： 2009/04/09
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;
using System.Data;

public class DeptInfo_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //定义Model变量
        DeptModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucce = DeptInfoBus.SaveDeptInfo(model);
            //保存成功时
            if (isSucce)
            {

                DataTable dt = DeptInfoBus.GetDeptinfoByNo(model.DeptNO);
                string DeptName = "";
                string deptid = "";
                string superid = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    DeptName = dt.Rows[0]["DeptName"] == null ? "" : dt.Rows[0]["DeptName"].ToString ();
                    deptid =dt.Rows[0]["id"] == null ? "" : dt.Rows[0]["id"].ToString ();
                    superid =dt.Rows[0]["superdeptID"] == null ? "" : dt.Rows[0]["superdeptID"].ToString();
                }
            string temp=deptid +"|"+DeptName +"|"+superid+"|"+model.DeptNO ;
                
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, temp , 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass("", "", 0);
            }
        }
        else
        {
            jc = new JsonClass("", "", 2);
        }
        //输出响应
        context.Response.Write(jc);
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private DeptModel EditRequstData(HttpRequest request)
    {
        //定义Model变量
        DeptModel model = new DeptModel();
        //编辑标识
        model.EditFlag = request.QueryString["EditFlag"];
        //获取编号
        string deptNo = request.QueryString["DeptNo"];

        //新建时
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            //编号为空时，通过编码规则编号获取编号
            if (string.IsNullOrEmpty(deptNo))
            {
                //获取编码规则编号
                string codeRuleID = request.QueryString["CodeRuleID"];
                //通过编码规则代码获取编号
                deptNo = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_DEPT
                                , ConstUtil.CODING_RULE_COLUMN_DEPT_NO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_DEPT
                                , ConstUtil.CODING_RULE_COLUMN_DEPT_NO, deptNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置考核编号
        model.DeptNO = deptNo;

        //上级机构
        model.SuperDeptID = request.QueryString["SuperDeptID"];
        //独立核算
        model.AccountFlag = request.QueryString["AccountFlag"];
        //机构名称
        model.DeptName = request.QueryString["DeptName"];
        //拼音代码
        model.PYShort = request.QueryString["PYShort"];
        //机构职责
        model.Duty = request.QueryString["Duty"];
        //是否分公司
        model.SubFlag = request.QueryString["SubFlag"];
        //是否零售
        model.SaleFlag = request.QueryString["SaleFlag"];
        //描述信息
        model.Description = request.QueryString["Description"];
        //地址
        model.Address = request.QueryString["Address"];
        //邮编
        model.Post = request.QueryString["Post"];
        //联系人
        model.LinkMan = request.QueryString["LinkMan"];
        //电话
        model.Tel = request.QueryString["Tel"];
        //传真
        model.Fax = request.QueryString["Fax"];
        //Email
        model.Email = request.QueryString["Email"];
        //启用状态
        model.UsedStatus = request.QueryString["UsedStatus"];
        
        return model;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}