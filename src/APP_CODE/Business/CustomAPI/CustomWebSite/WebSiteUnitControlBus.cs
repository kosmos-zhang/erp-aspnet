using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.CustomAPI.CustomWebSite
{
    public class WebSiteUnitControlBus
    {
        #region 判断是否启用多单位
        public static bool IsMulUnit(string CompanyCD)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteUnitControlDBHelper.IsMulUnit(CompanyCD);
        }
        #endregion

        #region 读取指定单位换算组
        /// <summary>
        /// 读取指定单位换算组明细
        /// </summary>
        /// <param name="GroupID">单位组主表ID</param>
        /// <returns>包含指定单位组的明细的数据表</returns>
        public static DataTable GetUnitGroup(string UnitGroupNo,string CompanyCD)
        {
            return Data.CustomAPI.CustomWebSite.WebSiteUnitControlDBHelper.GetUnitGroup(UnitGroupNo,CompanyCD);

        }
        #endregion
    }
}
