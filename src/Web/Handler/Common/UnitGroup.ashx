/************************************************************************
 * 作    者： 朱贤兆
 * 创建日期： 2010.03.25
 * 描    述： 计量单位类
 * 修改日期： 2010.03.25
 * 版    本： 0.1.0                                                                     
 ************************************************************************/
 
<%@ WebHandler Language="C#" Class="UnitGroup" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Common;

public class UnitGroup : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "getunitgroup":
                GetUnitGroup();//加载数据
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    
    /// <summary>
    /// 获取计量默认select;
    /// </summary>
    private void GetUnitGroup()
    {
        StringBuilder sb = new StringBuilder();
        string ProductId = GetParam("ProductId");
        string UnitParam = GetParam("UnitParam");
        string SelectId = GetParam("SelectId");
        string Func1 = GetParam("Func");
        string Val = GetParam("Val");
        DataSet ds = XBase.Business.Common.UnitGroup.GetUnitGroupByProductId(Convert.ToInt32(ProductId));
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("<select id='");
                    sb.Append(SelectId);
                    sb.Append("'");
                    if (!string.IsNullOrEmpty(Func1))
                    {
                        sb.Append(" onchange='" + Func1 + "' ");
                    }
                    sb.Append(">");
                    sb.Append(GetOption(dt.Rows[0]["UnitID"].ToString(), "1",
                          dt.Rows[0]["CodeName"].ToString(), false));
                    sb.Append("</select>");
                }
            }
            else if (ds.Tables.Count == 2)
            {
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                sb.Append("<select id='");
                sb.Append(SelectId);
                sb.Append("'");
                if (!string.IsNullOrEmpty(Func1))
                {
                    sb.Append(" onchange='" + Func1 + "' ");
                }
                sb.Append(">");
                bool flag1 = true;
                if (!string.IsNullOrEmpty(Val))
                {
                    if (Val == dt1.Rows[0]["UnitID"].ToString())
                    {
                        sb.Append(GetOption(dt1.Rows[0]["UnitID"].ToString(), "1",
                                  dt1.Rows[0]["CodeName"].ToString(), true));
                        flag1 = false;
                    }else{
                        sb.Append(GetOption(dt1.Rows[0]["UnitID"].ToString(), "1",
                                  dt1.Rows[0]["CodeName"].ToString(), false));
                    }
                }else{
                    sb.Append(GetOption(dt1.Rows[0]["UnitID"].ToString(), "1",
                                  dt1.Rows[0]["CodeName"].ToString(), false));
                }
                
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Val))
                    {
                        if (Val == dt2.Rows[i]["UnitID"].ToString())
                        {
                            sb.Append(GetOption(dt2.Rows[i]["UnitID"].ToString(),
                                  dt2.Rows[i]["ExRate"].ToString(),
                                  dt2.Rows[i]["CodeName"].ToString(),
                                  true));
                            flag1 = false;
                            continue;
                        }
                    }
                    if (dt1.Rows[0][UnitParam + "ID"].ToString() == dt2.Rows[i]["UnitID"].ToString())
                    {
                        if (flag1)
                        {
                            sb.Append(GetOption(dt2.Rows[i]["UnitID"].ToString(),
                                      dt2.Rows[i]["ExRate"].ToString(),
                                      dt2.Rows[i]["CodeName"].ToString(),
                                      true));
                            continue;
                        }
                    }
                    sb.Append(GetOption(dt2.Rows[i]["UnitID"].ToString(),
                              dt2.Rows[i]["ExRate"].ToString(),
                              dt2.Rows[i]["CodeName"].ToString(),
                              false));
                }
                sb.Append("</select>");
            }
        }
        Output(sb.ToString());
    }
    
    
    /// <summary>
    /// 返回Option
    /// </summary>
    /// <param name="UnitID"></param>
    /// <param name="ExRate"></param>
    /// <param name="CodeName"></param>
    /// <returns></returns>
    private string GetOption(string UnitID,string ExRate,string CodeName,bool flag){
        StringBuilder sb = new StringBuilder();
        if (flag)
        {
            sb.Append("<option selected='selected' value='");
        }
        else
        {
            sb.Append("<option value='");
        }
        sb.Append(UnitID);
        sb.Append("|");
        sb.Append(ExRate);
        sb.Append("'>");
        sb.Append(CodeName);
        sb.Append("</option>");
        return sb.ToString();
    }
    
    
}