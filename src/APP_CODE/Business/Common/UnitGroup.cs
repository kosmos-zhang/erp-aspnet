/************************************************************************
 * 作    者： 朱贤兆
 * 创建日期： 2010.03.25
 * 描    述： 计量单位类
 * 修改日期： 2010.03.25
 * 版    本： 0.1.0                                                                     
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace XBase.Business.Common
{
    public class UnitGroup
    {
        /// <summary>
        /// 根据ProductId获取产品一条记录、计量单位明细
        /// </summary>
        /// <param name="ProductId">产品ID</param>
        /// <returns></returns>
        public static DataSet GetUnitGroupByProductId(int ProductId)
        {
           return XBase.Data.Common.UnitGroup.GetUnitGroupByProductId(ProductId);
        }

        /// <summary>
        /// 获取计量换算下拉列表html、返回单位换算率
        /// </summary>
        /// <param name="ProductId">产品ID</param>
        /// <param name="UnitParam">单位类型 <SaleUnit 销售计量单位> <InUnit 采购计量单位> <StockUnit 库存计量单位> <MakeUnit 生产计量单位></param>
        /// <param name="SelectId">下拉列表ID</param>
        /// <param name="Func1">下拉列表onchange方法</param>
        /// <param name="Val">下拉表填充对象ID</param>
        /// <param name="ExRate">单位换算率</param>
        /// <returns></returns>
        public static string GetUnitGroupByProductId(string ProductId,string UnitParam,string SelectId,string Func1,string Val,ref decimal ExRate){
            StringBuilder sb = new StringBuilder();
            DataSet ds = GetUnitGroupByProductId(Convert.ToInt32(ProductId));
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
                            ExRate = Convert.ToDecimal("1");
                            flag1 = false;
                        }
                        else
                        {
                            sb.Append(GetOption(dt1.Rows[0]["UnitID"].ToString(), "1",
                                      dt1.Rows[0]["CodeName"].ToString(), false));
                        }
                    }
                    else
                    {
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
                                ExRate = Convert.ToDecimal(dt2.Rows[i]["ExRate"].ToString());
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
                                ExRate = Convert.ToDecimal(dt2.Rows[i]["ExRate"].ToString());
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
            return sb.ToString();
        }

        /// <summary>
        /// 返回Option
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="ExRate"></param>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public static string GetOption(string UnitID, string ExRate, string CodeName, bool flag)
        {
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

}
