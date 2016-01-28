using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Personal.WorkFlow
{
    public class WorkFlowDBHelper
    {
        /// <summary>
        /// GetPageData
        /// </summary>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt, string table,string key,string where,string fields,string orderExp,int pageindex,int pagesize)
        {
            /*
             set @table='[officedba].VflowMyApply'
            set @keyfield = '[ID]'
            set @where = '1=1'
            set @fields = '*'
            set @OrderExp = '[ID] ASC'
            set @pageIndex=1
            set @pageSize=10
             */
            if (where.Trim() + "" == "")
            {
                where = "1=1";
            }

            SqlParameter[] prams = {
                                       SqlParameterHelper.MakeInParam("@table",SqlDbType.NVarChar,0,table),
                                       SqlParameterHelper.MakeInParam("@keyfield",SqlDbType.NVarChar,0,key),
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,orderExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fields),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,where),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,pagesize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,pageindex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };
            DataSet ds = SqlHelper.ExecuteDataset("", "[dbo].GetPageData", prams); 
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);          
        }


        /// <summary>
        /// 我提交的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetVflowMyApplyList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return GetPageData(out dt,"[officedba].[VflowMyApply] ", "[ID]", where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        /// 我已处理的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetVFlowMyProcessList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return GetPageData(out dt,"[officedba].[VFlowMyProcess] ", "[ID]", where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        /// 待审批的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetVFlowWaitProcessList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return GetPageData(out dt,"[officedba].[VFlowWaitProcess] ", "[ID]", where, fields, orderExp, pageindex, pagesize);
        }

       /// <summary>
        /// 桌面待审批的流程
        /// 添加：王乾睿
        /// 2009-05-06
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetDeskTopVFlowWaitProcessList(int modifyid)
        {
              string sqlstr ="select  * from  officedba.VFlowWaitProcess  where  Actor= @modifyid  order    by   ApplyDate desc   ";
              SqlCommand comm = new SqlCommand();
              comm.CommandText = sqlstr;
              comm.Parameters.AddWithValue("@modifyid",SqlDbType.Int);
              comm.Parameters["@modifyid"].Value = modifyid;
              return SqlHelper.ExecuteSearch(comm);
   
        }
        

    }
}
