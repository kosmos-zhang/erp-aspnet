/**********************************************
 * 类作用：   单据打印设置事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2010/01/27
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Common;
using XBase.Model.Common;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Business.Common
{
    public class PrintParameterSettingBus
    {
        #region MRP详细信息
        /// <summary>
        /// 获取MRP信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPrintParameterSettingInfo(PrintParameterSettingModel model)
        {
            try
            {
                return PrintParameterSettingDBHelper.GetPrintParameterSettingInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 打印模板参数设置编辑
        /// <summary>
        /// 打印模板参数设置编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EditPrintParameterSetting(PrintParameterSettingModel model)
        {
            return PrintParameterSettingDBHelper.EditPrintParameterSetting(model);
        }

        #endregion
    }
}
