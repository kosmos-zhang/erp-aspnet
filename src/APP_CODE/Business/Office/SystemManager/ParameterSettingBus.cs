using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

using XBase.Common;
using XBase.Data.Office.SystemManager;


namespace XBase.Business.Office.SystemManager
{
    public class ParameterSettingBus
    {

        #region 参数设置
        /// <summary>
        /// 参数设置
        /// </summary>
        public static bool Set(string CompanyCD,string FunctionType,string Status)
        {
            return ParameterSettingDBHelper.Set(CompanyCD, FunctionType,Status);
        }
        #endregion
        #region 小数位设置
        /// <summary>
        /// 小数位设置
        /// </summary>
        public static bool SetPoint(string CompanyCD, string FunctionType, string SelPoint)
        {
            return ParameterSettingDBHelper.SetPoint(CompanyCD, FunctionType, SelPoint);
        }
        #endregion
        #region 验证是否启用辅助核算
        /// <summary>
        /// 验证是否启用辅助核算
        /// </summary>
        public static bool IfUsedAssistant(string CompanyCD)
        {
            return ParameterSettingDBHelper.IfUsedAssistant(CompanyCD);
        }
        #endregion


        #region 验证是否使用过多计量单位组
        /// <summary>
        /// 验证是否使用过多计量单位组
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IfUsedUnitGroup(string CompanyCD)
        {
            return ParameterSettingDBHelper.IfUsedUnitGroup(CompanyCD);
        }
        #endregion

        #region 读取配置
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="FunctionType"></param>
        /// <param name="isStop">默认是停用还是启用 isStop:</param>
        /// <returns></returns>
        public static bool Get(string CompanyCD, string FunctionType,bool isUsing)
        {
            DataTable dt =ParameterSettingDBHelper.Get(CompanyCD, FunctionType);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return isUsing;
            }
            else
            {
                if (dt.Rows[0]["UsedStatus"].ToString() == "1")
                    return true;
                else
                    return false;
            }
        }
        #endregion
        #region 读取配置
        public static DataTable GetPoint(string CompanyCD, string FunctionType)
        {
            DataTable dt = ParameterSettingDBHelper.Get(CompanyCD, FunctionType);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        #endregion


    }
}
