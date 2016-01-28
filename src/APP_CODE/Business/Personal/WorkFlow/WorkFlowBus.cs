using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Personal.WorkFlow
{
    public class WorkFlowBus
    {
        private static readonly XBase.Data.Personal.WorkFlow.WorkFlowDBHelper dal = new XBase.Data.Personal.WorkFlow.WorkFlowDBHelper();

        /// <summary>
        /// 我提交的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetVflowMyApplyList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return dal.GetVflowMyApplyList(out dt, where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        /// 我已处理的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetVFlowMyProcessList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return dal.GetVFlowMyProcessList(out dt, where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        /// 待审批的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetVFlowWaitProcessList(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return dal.GetVFlowWaitProcessList(out dt,where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        ///首页待审批的流程
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int GetDeskTopVFlowWaitProcessList(out DataTable dt, int  modifyid )
        {
            dt =  dal.GetDeskTopVFlowWaitProcessList(modifyid);
            return dt.Rows.Count; 
        }

    }
}
