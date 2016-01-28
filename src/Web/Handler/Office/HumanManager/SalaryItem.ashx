<%@ WebHandler Language="C#" Class="SalaryItem" %>
/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/05/05
 * 描    述： 工资项设置
 * 修改日期： 2009/05/05
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

public class SalaryItem : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.QueryString["Action"] != null)
        {
            string action = context.Request.QueryString["Action"].ToString ();
            if ("IsDelete".Equals(action))
            {
                //获取要素ID
                string itemNo = context.Request.QueryString["itemNo"];
                string row = context.Request.QueryString["row"];
                
                //替换引号
                ////校验是否被使用
                bool isUsed = SalaryItemBus.IsUsedItem(itemNo);
                //如果已经被使用
              
                //已经被使用
                if (isUsed)
                {
                    //输出响应 返回不执行删除
                    context.Response.Write(new JsonClass("", "" , 1));
                }
                else
                {
                    context.Response.Write(new JsonClass("", row, 2));
                
                }
            }
            else if ("Check".Equals(action))
            {

                string cal = context.Request.Params["Calculate"].ToString();
                string numlist = context.Request.Params["numberlist"].ToString();
                string [] numberlist=numlist .Split (','); 
              while (cal.IndexOf('A') != -1)
              {
                  cal = cal.Replace('A', '+');
              }
              string temp = cal;

              for (int i = 0; i < numberlist.Length; i++)
              {
                  if (numberlist[i] == "@")
                  {
                      cal = cal.Replace("{" + numberlist[i] + "}", "0.8");
                  }
                  else
                  {
                      cal = cal.Replace("{" + numberlist[i] + "}", Convert.ToString(i + 1));
                  }
              }

              try
              {
                  string result = CustomEval.eval(cal).ToString();
                  context.Response.Write(new JsonClass(numlist, temp, 0));
                  return;
              }
              catch
              {
                  context.Response.Write(new JsonClass("", "", 1));
                  return;
              }
                
                
         
                
                
                
                
            }
        }
        else
        {



            //编辑请求信息
            ArrayList lstEdit = new ArrayList();
            string itemName = string.Empty;
            string itemNo = EditRequstData(context.Request, lstEdit, ref itemName);
            //定义返回字符串变量
            StringBuilder sbReturn = new StringBuilder();

            //判断是否有错
            if (!string.IsNullOrEmpty(itemNo))
            {
                //设置记录总数
                sbReturn.Append("{");
                sbReturn.Append("EditStatus:2");
                sbReturn.Append(",itemName:\"" + itemName + "\"");
                //设置数据
                sbReturn.Append(",ItemNo:\"");
                sbReturn.Append(itemNo);
                sbReturn.Append("\"}");
                //设置输出流的 HTTP MIME 类型
                context.Response.ContentType = "text/plain";
                //向响应中输出数据
                context.Response.Write(sbReturn.ToString());
                context.Response.End();
                return;
            }
            //执行保存
            bool isSucc = SalaryItemBus.SaveSalaryItemInfo(lstEdit);
            //保存成功时
            if (isSucc)
            {
                //设置记录总数
                sbReturn.Append("{");
                sbReturn.Append("EditStatus:1");
                //设置数据
                sbReturn.Append(",DataInfo:");
                sbReturn.Append(SalaryItem2Json(lstEdit));
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
    }

    /// <summary>
    /// 工资项记录转换为Json
    /// </summary>
    /// <param name="lstEdit">工资项记录</param>
    /// <returns></returns>
    private string SalaryItem2Json(ArrayList lstEdit)
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
                //获取工资项信息
                SalaryItemModel model = (SalaryItemModel)lstEdit[i];
                //未删除的项转换为Json值
                if ("0".Equals(model.EditFlag) || "1".Equals(model.EditFlag))
                {
                    //行数据开始符
                    sbSalaryInfo.Append("{");
                    //工资项编号
                    sbSalaryInfo.Append("\"ItemNo\":\"" + model.ItemNo + "\"");
                    //名称
                    sbSalaryInfo.Append(",\"ItemName\":\"" + model.ItemName + "\"");
                    //排列顺序
                    sbSalaryInfo.Append(",\"ItemOrder\":\"" + model.ItemOrder + "\"");
                    //是否扣款
                    sbSalaryInfo.Append(",\"PayFlag\":\"" + model.PayFlag + "\"");
                    //是否固定
                    sbSalaryInfo.Append(",\"ChangeFlag\":\"" + model.ChangeFlag + "\"");
                    //计算公式
                    sbSalaryInfo.Append(",\"Calculate\":\"" + model.Calculate + "\"");
                    //启用状态
                    sbSalaryInfo.Append(",\"UsedStatus\":\"" + model.UsedStatus + "\"");
                    //备注
                    sbSalaryInfo.Append(",\"Remark\":\"" + model.Remark + "\"");
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
    private string EditRequstData(HttpRequest request, ArrayList lstReturn,ref string itemName )
    {
        //获取工资项列表数据总数
        int salaryCount = int.Parse(request.Params["SalaryCount"].ToString());
        int itemOrder = 0;
        //遍历所有工资项
        for (int i = 1; i <= salaryCount; i++)
        {
            //定义Model变量
            SalaryItemModel model = new SalaryItemModel();
            //工资项编号
            model.ItemNo = request.Params["ItemNo_" + i.ToString()].ToString();
            //编辑模式
            model.EditFlag = request.Params["EditFlag_" + i.ToString()].ToString();
            //不是删除时
            if ("0".Equals(model.EditFlag) || "1".Equals(model.EditFlag))
            {
                itemOrder++;
                //名称
                model.ItemName = request.Params["SalaryName_" + i.ToString()].ToString();
                //排列顺序
                model.ItemOrder = itemOrder.ToString();
                //是否扣款
                model.PayFlag = request.Params["PayFlag_" + i.ToString()].ToString();
                //是否固定
                model.ChangeFlag = request.Params["ChangeFlag_" + i.ToString()].ToString();
                //计算公式
                model.Calculate = request.Params["Calculate_" + i.ToString()].ToString();
                //公式参数 
                string te=request.Params["CalculateParam_" + i.ToString()].ToString() ;
                if (te == "undefined" || string .IsNullOrEmpty (te))
                {

                }
                else
                {
                    model.CalculateParam = te ; 
                }
                //启用状态
                model.UsedStatus = request.Params["UsedStatus_" + i.ToString()].ToString();
                //备注
                model.Remark = request.Params["Remark_" + i.ToString()].ToString();
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                model.CompanyCD = userInfo.CompanyCD;
            }
            //修改前启用状态
            string usedStatus = request.Params["UsedStatusModify_" + i.ToString()].ToString();
            //删除操作 或者 从启用修改到停用时，校验是否被使用
                   bool isSucc =false ;
            if ("3".Equals(model.EditFlag)
                || ("1".Equals(usedStatus) && "0".Equals(model.UsedStatus)))
            {
                ////校验是否被使用
                //  isSucc = SalaryItemBus.IsUsedItem(model.ItemNo);
                ////如果已经被使用
                //if (isSucc)
                //{
                //    //返回
                //    itemName = model.ItemName;
                //    //return model.ItemNo;
                //}
            }
       //     SalaryItemBus.InsertSalaryItem (model);
            //添加记录
            if (isSucc)
            {

            }
            else
            {
                lstReturn.Add(model);
            }
        }

        return null;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}