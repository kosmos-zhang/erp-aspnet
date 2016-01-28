/************************************************************************
 * 作    者： 朱贤兆
 * 创建日期： 2010.03.26
 * 描    述： 计量单位类
 * 修改日期： 2010.03.26
 * 版    本： 0.1.0                                                                     
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;

namespace XBase.Data.Common
{
    public class UnitGroup
    {
        /// <summary>
        /// 根据ProductId获取产品一条记录、计量单位明细
        /// </summary>
        /// <param name="ProductId">产品ID</param>
        /// <returns></returns>
        public static DataSet GetUnitGroupByProductId(int ProductId){
            SqlParameter[] prams = {
                    SqlParameterHelper.MakeInParam("@ProductId",SqlDbType.Int,4,ProductId)									
			};
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper._connectionStringStr,"officedba.GetUnitGroupByProductId", prams);
            return ds;
        }
    }
}
