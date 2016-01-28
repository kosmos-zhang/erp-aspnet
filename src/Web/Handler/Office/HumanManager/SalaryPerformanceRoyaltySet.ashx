<%@ WebHandler Language="C#" Class="SalaryPerformanceRoyaltySet" %>

/**********************************************
 * 作    者： 肖合明
 * 创建日期： 2009/09/04
 * 描    述： 公司提成
 * 修改日期： 2009/09/04
 * 版    本： 0.5.0
 ***********************************************/
using System;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using System.Collections;
using System.Collections.Generic;
public class SalaryPerformanceRoyaltySet : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取当前操作
        string action = request.Form["Action"];

        //分页控件查询数据
        if ("GetInfo".Equals(action))
        {
            //从请求中获取排序列
            string EmployeeID = request.Form["EmployeeID"];

            //考核类型
            string Taskflag = request.Form["Taskflag"];

            string sbReturn = string.Empty;

            sbReturn = InitSalaryDetailInfo(EmployeeID, Taskflag);
            //设置输出流的 HTTP MIME 类型
            context.Response.ContentType = "text/plain";
            //向响应中输出数据
            context.Response.Write(sbReturn);
            context.Response.End();

        }
        if ("Save".Equals(action))
        {
            saveMessage(context);

        }
    }

    //保存
    //tempInfo .push (i,MiniScore ,MaxScore,Confficent);
    public void saveMessage(HttpContext context)
    {
        //获取上下文的请求
        HttpRequest request = context.Request;
        //从请求中获取DeptID
        string EmployeeID = request.Form["EmployeeID"];
        string Taskflag = request.Form["Taskflag"];

        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        List<SalaryPerformanceRoyaltySetModel> Modelist = new List<SalaryPerformanceRoyaltySetModel>();
        string message = context.Request.Form["message"];
        string[] info = message.Split(',');
        int count = info.Length;
        for (int i = 0; i < count - 2; i++)
        {
            SalaryPerformanceRoyaltySetModel model = new SalaryPerformanceRoyaltySetModel();
            model.MiniScore = info[i + 1];
            model.MaxScore = info[i + 2];
            model.Confficent = info[i + 3];
            model.EmployeeID = EmployeeID;
            model.Taskflag = Taskflag;
            model.ModifiedUserID = userInfo.UserID;
            model.CompanyCD = userInfo.CompanyCD;
            Modelist.Add(model);
            i = i + 3;

        }

        if (SalaryPerformanceRoyaltySetBus.SaveInfo(EmployeeID, Taskflag, Modelist))
        {
            context.Response.Write(new JsonClass("", "", 1));
        }
        else
        {
            context.Response.Write(new JsonClass("", "", 0));
        }

    }

    #region 设置绩效信息内容(通过EmployeeID)
    /// <summary>
    /// 设置工资内容
    /// </summary>
    private string InitSalaryDetailInfo(string EmployeeID, string Taskflag)
    {
        //定义变量
        StringBuilder sbSalaryInfo = new StringBuilder();
        //获取数据

        DataTable dtSalaryInfo = SalaryPerformanceRoyaltySetBus.GetInfoTable(EmployeeID, Taskflag);
        //数据存在时
        if (dtSalaryInfo != null && dtSalaryInfo.Rows.Count > 0)
        {
            //遍历显示所有数据
            for (int i = 0; i < dtSalaryInfo.Rows.Count; i++)
            {
                //插入行开始标识
                sbSalaryInfo.AppendLine("<tr>");
                //选择框
                if (i == dtSalaryInfo.Rows.Count - 1)
                {
                    //选择
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                              + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                              + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                              + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                              + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  /></td>");

                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");'   readonly=\"readonly\" style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniScore")
                                + "' class='tdinput' id='txtMiniScore_" + (i + 1).ToString() + "' /></td>");

                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'  onchange='Number_round(this,\"0\");' onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxScore")
                                + "' class='tdinput' id='txtMaxScore_" + (i + 1).ToString() + "'  /></td>");

                    //系数  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();'   onchange='Number_round(this,\"2\");'   onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "Confficent")
                                + "' class='tdinput' id='txtConfficent_" + (i + 1).ToString() + "' /></td>");
                }
                else
                {
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='hidden' id='txtSalaryID_" + (i + 1).ToString() + "' value='"
                                + GetSafeData.GetStringFromInt(dtSalaryInfo.Rows[i], "ID")
                                + "' /><input type='hidden' id='txtEditFlag_" + (i + 1).ToString() + "' value='1' />"
                                + "<input type='checkbox' onclick='SetCheckAll(this);' id='chkSelect_" + (i + 1).ToString() + "'  style=\"display:none \" /></td>");


                    //业绩上限
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12'  onkeydown='Numeric_OnKeyDown();' readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MiniScore")
                                + "' class='tdinput' id='txtMiniScore_" + (i + 1).ToString() + "' /></td>");

                    //业绩下限 
                    sbSalaryInfo.AppendLine("<td class='tdColInputCenter'>"
                                + "<input type='text' maxlength = '12' onkeydown='Numeric_OnKeyDown();'   readonly=\"readonly\" onchange='Number_round(this,\"0\");'   style='width:98%;' value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "MaxScore")
                                + "' class='tdinput' id='txtMaxScore_" + (i + 1).ToString() + "'  /></td>");

                    //系数  
                    sbSalaryInfo.AppendLine("<td class='tdColInput'>"
                                + "<input type='text' maxlength = '3' style='width:98%;' onkeydown='Numeric_OnKeyDown();' onchange='Number_round(this,\"2\");' onblur='CalculateTotalSalary(this,\"" + (i + 1).ToString() + "\");'   value='"
                                + GetSafeData.ValidateDataRow_Decimal(dtSalaryInfo.Rows[i], "Confficent")
                                + "' class='tdinput' id='txtConfficent_" + (i + 1).ToString() + "' /></td>");

                }

                //插入行结束标识
                sbSalaryInfo.AppendLine("</tr>");
            }
            sbSalaryInfo.Append("*" + dtSalaryInfo.Rows[0]["Taskflag"].ToString());
        }

        //返回信息
        return sbSalaryInfo.ToString();
    }
    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}