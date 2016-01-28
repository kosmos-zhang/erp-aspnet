using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using System.Data;
using XBase.Common;
namespace XBase.Business.Office.SupplyChain
{
   public class BusiLogicSetBus
    {
  
        /// <summary>
        /// 修改业务规则
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="UsedStatus">使用状态</param>
        /// <returns>true 成功,false 失败</returns>
       public static bool UpdateBusiLogic(int ID, string LogicSet)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            try
            {
                return BusiLogicSetDBHelper.UpdateBusiLogic(CompanyCD, ID, LogicSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 获取科目核算类别
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>DataTable</returns>
       public static DataTable GetBusiLogicSet(string Name)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //string CompanyCD = "AAAAAA";
            if (string.IsNullOrEmpty(CompanyCD)) return null;
            try
            {
                DataTable dt = BusiLogicSetDBHelper.GetBusiLogicSet(CompanyCD, Name);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Rows in dt.Rows)
                    {
                        if (Rows["LogicSet"].ToString() == "0")
                        {
                            Rows["LogicSet"] = "否";
                        }
                        else if (Rows["LogicSet"].ToString() == "1")
                        {
                            Rows["LogicSet"] = "是";
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
