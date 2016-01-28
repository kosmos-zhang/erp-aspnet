<%@ WebHandler Language="C#" Class="SalaryInsuSocial" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/07
 * 描    述： 社会保险设置
 * 修改日期： 2009/05/07
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using System.Collections;
using XBase.Common;
using System.Text;
using XBase.Business.Common;

public class SalaryInsuSocial : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //编辑请求信息
        ArrayList lstEdit = new ArrayList();
        string insuranceName = EditRequstData(context.Request, lstEdit);
        //定义返回字符串变量
        StringBuilder sbReturn = new StringBuilder();
        //判断是否有错
        if (!string.IsNullOrEmpty(insuranceName))
        {
            //设置记录总数
            sbReturn.Append("{");
            sbReturn.Append("EditStatus:2");
            //设置数据
            sbReturn.Append(",InsuranceName:\"");
            sbReturn.Append(insuranceName);
            sbReturn.Append("\"}");
            //设置输出流的 HTTP MIME 类型
            context.Response.ContentType = "text/plain";
            //向响应中输出数据
            context.Response.Write(sbReturn.ToString());
            context.Response.End();
            return;
        }
        //执行保存
        bool isSucc = InsuSocialBus.SaveInsuSocialInfo(lstEdit);
        //保存成功时
        if (isSucc)
        {
            //设置记录总数
            sbReturn.Append("{");
            sbReturn.Append("EditStatus:1");
            //设置数据
            sbReturn.Append(",DataInfo:");
            sbReturn.Append(InsuSocial2Json(lstEdit));
            sbReturn.Append("}");
        }
        //保存未成功时
        else
        {
            //设置记录总数
            sbReturn.Append("{EditStatus:0}");
        }
        //设置输出流的 HTTP MIME 类型
        context.Response.ContentType = "text/plain";
        //向响应中输出数据
        context.Response.Write(sbReturn.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 社会保险记录转换为Json
    /// </summary>
    /// <param name="lstEdit">社会保险记录</param>
    /// <returns></returns>
    private string InsuSocial2Json(ArrayList lstEdit)
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //记录存在时
        if (lstEdit != null && lstEdit.Count > 0)
        {
            //数据开始符
            sbSalaryInfo.Append("[");
            //遍历所有记录转化为Json
            for (int i = 0; i < lstEdit.Count; i++)
            {
                //获取社会保险信息
                InsuSocialModel model = (InsuSocialModel)lstEdit[i];
                //未删除的项转换为Json值
                if ("0".Equals(model.EditFlag) || "1".Equals(model.EditFlag))
                {
                    //行数据开始符
                    sbSalaryInfo.Append("{");
                    //社会保险ID
                    sbSalaryInfo.Append("\"ID\":\"" + model.ID + "\"");
                    //保险名称
                    sbSalaryInfo.Append(",\"InsuranceName\":\"" + model.InsuranceName + "\"");
                    //单位比例
                    sbSalaryInfo.Append(",\"CompanyPayRate\":\"" + model.CompanyPayRate + "\"");
                    //个人比例
                    sbSalaryInfo.Append(",\"PersonPayRate\":\"" + model.PersonPayRate + "\"");
                    //投保方式
                    sbSalaryInfo.Append(",\"InsuranceWay\":\"" + model.InsuranceWay + "\"");
                    //启用状态
                    sbSalaryInfo.Append(",\"UsedStatus\":\"" + model.UsedStatus + "\"");
                    //行数据结束符
                    sbSalaryInfo.Append("},");
                }
            }
            //替换最后的,
            sbSalaryInfo.Replace(",", "", sbSalaryInfo.Length - 1, 1);
            //数据结束符
            sbSalaryInfo.Append("]");
        }
        //返回值
        return sbSalaryInfo.ToString();
    }

    /// <summary>
    /// 从请求中获取培训信息并转换为Model模式
    /// </summary>
    /// <param name="request">客户端请求</param>
    /// <returns></returns>
    private string EditRequstData(HttpRequest request, ArrayList lstReturn)
    {
        //获取社会保险列表数据总数
        int salaryCount = int.Parse(request.Params["InsuSocialCount"].ToString());
        //遍历所有社会保险
        for (int i = 1; i <= salaryCount; i++)
        {
            //定义Model变量
            InsuSocialModel model = new InsuSocialModel();
            //社会保险ID
            model.ID = request.Params["SalaryID_" + i.ToString()].ToString();
            //编辑模式
            model.EditFlag = request.Params["EditFlag_" + i.ToString()].ToString();
            //保险名称
            model.InsuranceName = request.Params["InsuranceName_" + i.ToString()].ToString();
            //不是删除时
            if ("0".Equals(model.EditFlag) || "1".Equals(model.EditFlag))
            {
                //单位比例
                model.CompanyPayRate = request.Params["CompanyPayRate_" + i.ToString()].ToString();
                //个人比例
                model.PersonPayRate = request.Params["PersonPayRate_" + i.ToString()].ToString();
                //投保方式
                model.InsuranceWay = request.Params["InsuranceWay_" + i.ToString()].ToString();
                //启用状态
                model.UsedStatus = request.Params["UsedStatus_" + i.ToString()].ToString();
            }
            //修改前启用状态
            string usedStatus = request.Params["UsedStatusModify_" + i.ToString()].ToString();
            //删除操作 或者 从启用修改到停用时，校验是否被使用
            if ("3".Equals(model.EditFlag)
                || ("1".Equals(usedStatus) && "0".Equals(model.UsedStatus)))
            {
                ////校验是否被使用
                //bool isSucc = InsuSocialBus.IsUsedItem(model.ID);
                ////如果已经被使用
                //if (isSucc)
                //{
                //    //返回
                //    return model.InsuranceName;
                //}
            }
            //添加记录
            lstReturn.Add(model);
        }

        return null;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}