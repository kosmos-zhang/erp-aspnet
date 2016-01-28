////////////////////////////////////////////////////////////////////////////
/////////////////////////////////    业务层  /////////////////////////////////
////////////////////////////////////////////////////////////////////////////
/**********************************************
 * 类作用   计量单位组明细数据处理层
 * 创建人   xz
 * 创建时间 2010-3-12 11:29:17 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;


namespace XBase.Business.Office.SupplyChain
{
    /// <summary>
    /// 计量单位组明细业务类
    /// </summary>
    public class UnitGroupDetailBus
    {
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return UnitGroupDetailDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        /// <param name="groupUnitNo">计量单位组编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(string companyCD, string groupUnitNo)
        {
            return UnitGroupDetailDBHelper.SelectDataTable(companyCD, groupUnitNo);
        }

    }
}
