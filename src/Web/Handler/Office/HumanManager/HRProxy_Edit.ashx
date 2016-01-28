<%@ WebHandler Language="C#" Class="HRProxy_Edit" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/25
 * 描    述： 新建人才代理
 * 修改日期： 2009/03/25
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using XBase.Business.Common;

public class HRProxy_Edit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    /// <summary>
    /// 处理新建新建人才代理的请求
    /// </summary>
    /// <param name="context">请求上下文</param>
    public void ProcessRequest (HttpContext context)
    {
        //定义储存人员信息的Model变量
        HRProxyModel model = EditRequstData(context.Request);
        //定义Json返回变量
        JsonClass jc;
        //编码能用时
        if (model != null)
        {
            //执行保存操作
            bool isSucc = HRProxyBus.SaveHRProxyInfo(model);
            //保存成功时
            if (isSucc)
            {
                jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, model.ProxyCompanyCD, 1);
            }
            //保存未成功时
            else
            {
                jc = new JsonClass(context.Request.Params["EditFlag"], "", 0);
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
    /// 从请求中获取人员信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private HRProxyModel EditRequstData(HttpRequest request)
    {
        //定义人才代理信息Model变量
        HRProxyModel model = new HRProxyModel();
        //编辑标识
        model.EditFlag = request.Params["EditFlag"].ToString();
        /* 获取人员编号 */
        string proxyCompanyCD = request.Params["ProxyCompanyCD"].ToString();

        //新建时，设置创建者信息
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(model.EditFlag))
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            model.ModifiedUserID = userInfo.UserID;//创建人

            //人员编号为空时，通过编码规则编号获取人员编号
            if (string.IsNullOrEmpty(proxyCompanyCD))
            {
                //获取编码规则编号
                string codeRuleID = request.Params["CodeRuleID"].ToString();
                //通过编码规则代码获取人员编码
                proxyCompanyCD = ItemCodingRuleBus.GetCodeValue(codeRuleID, ConstUtil.CODING_RULE_TABLE_PROXY
                                , ConstUtil.CODING_RULE_COLUMN_PROXYNO);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_PROXY
                                , ConstUtil.CODING_RULE_COLUMN_PROXYNO, proxyCompanyCD);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        //设置人才代理编号
        model.ProxyCompanyCD = proxyCompanyCD;
        
        /* 获取代理公司信息 */
        //企业名称
        model.ProxyCompanyName = request.Params["ProxyCompanyName"].ToString().Trim();
        //企业性质
        model.Nature = request.Params["Nature"].ToString().Trim();
        //地址
        model.Address = request.Params["Address"].ToString().Trim();
        //企业法人
        model.Corporate = request.Params["Corporate"].ToString().Trim();
        //电话
        model.Telephone = request.Params["CompanyTel"].ToString().Trim();
        //传真
        model.Fax = request.Params["CompanyFax"].ToString().Trim();
        //邮箱
        model.Email = request.Params["CompanyMail"].ToString().Trim();
        //网址
        model.Website = request.Params["Website"].ToString().Trim(); 
        //合作关系
        model.Cooperation = request.Params["Cooperation"].ToString().Trim(); 
        //重要程度
        model.Important = request.Params["Important"].ToString().Trim(); 
        //收费标准
        model.Standard = request.Params["Standard"].ToString().Trim(); 
        //启用状态
        model.UsedStatus = request.Params["UsedStatus"].ToString().Trim();

        /* 获取代理公司联系人信息 */
        //姓名
        model.ContactName = request.Params["ContactName"].ToString().Trim();
        //固定电话
        model.ContactTel = request.Params["ContactTel"].ToString().Trim();
        //移动电话
        model.ContactMobile = request.Params["ContactMobile"].ToString().Trim(); 
        //网络通讯
        model.ContactWeb = request.Params["ContactWeb"].ToString().Trim(); 
        //工号
        model.ContactCardNo = request.Params["ContactCardNo"].ToString().Trim();
        //公司职务
        model.ContactPosition = request.Params["ContactPosition"].ToString().Trim();
        //备注
        model.ContactRemark = request.Params["ContactRemark"].ToString().Trim();

        /* 获取代理公司联系人信息 */
        //附加信息
        model.Remark = request.Params["Additional"].ToString().Trim();
        
        return model;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
    

}