<%@ WebHandler Language="C#" Class="PerformanceBetter" %>
/**********************************************
 * 作    者： 王保军
 * 创建日期： 2009/05/17
 * 描    述： 新建绩效改进计划
 * 修改日期： 2009/05/17
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
public class PerformanceBetter : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //定义储存人员信息的Model变量
     string action = context.Request.Params["action"];
     if (action != null)
     {
         if ("SearchInfo".Equals(action))//有用
         {
             //执行查询
             SearchData(context);
         }
         else if ("DeletePlanInfo".Equals(action))
         {
                 UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string[] planInfo = context.Request.Params["PlanNoList"].ToString().Split(',');
             IList<PerformanceBetterModel> BetterList = new List<PerformanceBetterModel>();
         
             for (int i = 0; i < planInfo.Length; i++)
             {
                 PerformanceBetterModel model = new PerformanceBetterModel();
                 model.PlanNo = planInfo[i];
                 model.CompanyCD = userInfo.CompanyCD;
                 BetterList.Add(model);

             }
             //删除评分项目信息
             bool isSucc = PerformanceBetterBus.DeletePlanInfo(BetterList);
             //删除成功
             if (isSucc)
             {
                 //输出响应
                 context.Response.Write(new JsonClass("", "", 1));
                 return;
             }
             //删除失败
             else
             {
                 //输出响应
                 context.Response.Write(new JsonClass("", "", 0));
                 return;
             }

         }
     }
     else
     {
         PerformanceBetterModel BetterModel = new PerformanceBetterModel();
         BetterModel = EditBetterRequstData(context.Request);
         //定义Json返回变量
         JsonClass jc;
         //编码能用时
         if (BetterModel != null)
         {
             IList<PerformanceBetterDetailModel> planModeList = new List<PerformanceBetterDetailModel>();
             planModeList = EditRequstData(context.Request, BetterModel.PlanNo);
             //执行保存操作

             bool isSucce = PerformanceBetterBus.SaveBetterInfo(BetterModel, planModeList);
             //保存成功时
             if (isSucce)
             {
                 jc = new JsonClass(ConstUtil.EDIT_FLAG_UPDATE, BetterModel.PlanNo, 1);
             }
             //保存未成功时
             else
             {
                 jc = new JsonClass(context.Request.QueryString["EditFlag"], "", 0);
             }
         }
         else
         {
             jc = new JsonClass("", "", 2);
         }
         //输出响应
         context.Response.Write(jc);
     }
    }

    private PerformanceBetterModel EditBetterRequstData(HttpRequest request)
    {
        //定义人员信息Model变量
        PerformanceBetterModel planModeList = new PerformanceBetterModel();
        //编辑标识

        string flag = request.QueryString["EditFlag"];
        /* 获取招聘活动编号 */
        string rectPlanNo = request.QueryString["RectPlanNo"];

        //新建时，设置创建者信息
        if (ConstUtil.EDIT_FLAG_INSERT.Equals(flag))
        {
            //招聘活动编号为空时，通过编码规则编号获取招聘活动编号
            if (string.IsNullOrEmpty(rectPlanNo))
            {
                //获取编码规则编号
                string codeRuleID = request.QueryString["CodeRuleID"];
                //通过编码规则代码获取人员编码
                rectPlanNo = ItemCodingRuleBus.GetCodeValue(codeRuleID);
            }
            //判断是否存在
            bool isAlready = PrimekeyVerifyBus.CheckCodeUniq(ConstUtil.CODING_RULE_TABLE_PERFORMANCEBETTER
                                , ConstUtil.CODING_RULE_COLUMN_PERFORMANCEBETTER_PLANO, rectPlanNo);
            //存在的场合
            if (!isAlready)
            {
                return null;
            }
        }
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //设置招聘活动编号

        //主题
           planModeList.Title = request.QueryString["Title"];
        //计划备注
          planModeList.Remark  = request.QueryString["PlanRemark"];
          planModeList.PlanNo = rectPlanNo;
          planModeList.CompanyCD = userInfo.CompanyCD;
          planModeList.Creator = userInfo.EmployeeID.ToString ();
          planModeList.EditFlag = flag; 
         planModeList.ModifiedUserID = userInfo.EmployeeID.ToString ();
         planModeList.CompanyCD = userInfo.CompanyCD;


          return planModeList;
    }
    /// <summary>
    /// 从请求中获取申请招聘信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private IList < PerformanceBetterDetailModel >  EditRequstData(HttpRequest request,string planNo )
    {
        //定义人员信息Model变量
        IList<PerformanceBetterDetailModel> planModeList = new List<PerformanceBetterDetailModel>();
        //编辑标识
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //设置招聘活动编号
     
        //主题
   //     planModel.Title = request.QueryString["Title"];
        //计划备注
     //   planModel.StartDate = request.QueryString["PlanRemark"];
        //设置信息发布
        int publishCount = int.Parse(request.QueryString["PublishCount"]);
        //信息发布输入时，编辑信息发布
        if (publishCount > 0)
        {
            //遍历信息发布
            for (int i = 1; i <= publishCount; i++)
            {
                //定义信息发布Model变量
                PerformanceBetterDetailModel planModel = new PerformanceBetterDetailModel();
                //发布媒体和渠道
                planModel.EditFlag = request.QueryString["EditFlag"];
                planModel.PlanNo = planNo;
                //员工编号
                if (request.QueryString["EmployeeID_" + i] == null)
                    continue;
                planModel.EmployeeID  = request.QueryString["EmployeeID_" + i];
                //待改进绩效
                planModel.Content  = request.QueryString["Content_" + i];
                //完成目标
                planModel.CompleteAim  = request.QueryString["CompleteAim_" + i];
                //完成时间
                planModel.CompleteDate  = request.QueryString["CompleteDate_" + i];
                //核查人
                planModel.Checker  = request.QueryString["Checker_" + i];
                //核查结果
                planModel.CheckResult  = request.QueryString["CheckResult_" + i];
                //核查时间
                planModel.CheckDate  = request.QueryString["CheckDate_" + i];
                //备注
                planModel.Remark = request.QueryString["Remark_" + i];
                ///公司代码
                planModel.CompanyCD = userInfo.CompanyCD;
                

                //添加到信息发布列表中
                planModeList.Add(planModel);
            }
        }

        return planModeList;
    }
    private void SearchData(HttpContext context)
    {
        //从请求中获取排序列
        string orderString = context.Request.QueryString["OrderBy"];
        //排序：默认为升序
        string orderBy = "ascending";
        //要排序的字段，如果为空，默认为"ID"
        string orderByCol = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";
        //降序时如果设置为降序
        if (orderString.EndsWith("_d"))
        {
            //排序：降序
            orderBy = "descending";
        }
        //从请求中获取当前页
        int pageIndex = int.Parse(context.Request.QueryString["PageIndex"]);
        //从请求中获取每页显示记录数
        int pageCount = int.Parse(context.Request.QueryString["PageCount"]);
        //跳过记录数
        int skipRecord = (pageIndex - 1) * pageCount;

        //获取数据
        PerformanceBetterModel searchModel = new PerformanceBetterModel();
        searchModel.PlanNo = context.Request.QueryString["searchPlanNo"];
        //启用状态
        searchModel.Title = context.Request.QueryString["Title"];
        searchModel.CreateDate  = context.Request.QueryString["StartDate"];
        searchModel.EndDate  = context.Request.QueryString["EndDate"];
        searchModel.EmployeeId  = context.Request.QueryString["EmployeeID"];
        //启用状态
        //查询数据
        
        DataTable dtData = PerformanceBetterBus.SearchPlanInfo (searchModel);
        //转化数据
        XElement dsXML = PageListCommon.ConvertDataTableToXML(dtData, "Data");
        //linq排序
        var dsLinq =
            (orderBy == "ascending") ?
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value ascending
             select new PerformanceBetterModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 PlanNo = x.Element("PlanNo") == null ? "" : x.Element("PlanNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 Creator = x.Element("Creator") == null ? "" : x.Element("Creator").Value
             })
                      :
            (from x in dsXML.Descendants("Data")
             orderby x.Element(orderByCol).Value descending
             select new PerformanceBetterModel()
             {
                 ID = x.Element("ID") == null ? "" : x.Element("ID").Value,//ID
                 PlanNo = x.Element("PlanNo") == null ? "" : x.Element("PlanNo").Value,
                 Title = x.Element("Title") == null ? "" : x.Element("Title").Value,
                 CreateDate = x.Element("CreateDate") == null ? "" : x.Element("CreateDate").Value,
                 Creator = x.Element("Creator") == null ? "" : x.Element("Creator").Value
             });
        //获取记录总数
        int totalCount = dsLinq.Count();
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //设置记录总数
        sbReturn.Append("{");
        sbReturn.Append("totalCount:");
        sbReturn.Append(totalCount.ToString());
        //设置数据
        sbReturn.Append(",data:");
        sbReturn.Append(PageListCommon.ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
        sbReturn.Append("}");
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}