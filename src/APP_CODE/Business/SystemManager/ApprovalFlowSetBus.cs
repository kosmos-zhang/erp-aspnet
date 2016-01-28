using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.SystemManager;
namespace XBase.Business.SystemManager
{
   public  class ApprovalFlowSetBus
    {
       /// <summary>
        /// 根据分类标志获取分类信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <returns></returns>
       public static DataTable GetBillTypeByTypeFlag(string TypeFlag)
       {
           try
           {
               if (TypeFlag.Length == 0) return null;
               return ApprovalFlowSetDBHelper.GetBillTypeByTypeFlag(TypeFlag);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       public static DataTable GetBillFlowTypeByTypeFlag(string TypeFlag)
       {
           try
           {
               if (TypeFlag.Length == 0) return null;
               return ApprovalFlowSetDBHelper.GetBillFlowTypeByTypeFlag(TypeFlag);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       public static DataTable GetCodePublicByTypeFlag(string TypeFlag)
       {
           try
           {
               if (TypeFlag.Length == 0) return null;
               return ApprovalFlowSetDBHelper.GetCodePublicByTypeFlag(TypeFlag);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 获取所有的单据分类
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <returns></returns>
       public static DataTable GetBillTypeByTypeFlag()
       {
           try
           {
               return ApprovalFlowSetDBHelper.GetBillTypeByTypeFlag();
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

       public static DataTable GetBillTypeByType()
       {
           try
           {
               return ApprovalFlowSetDBHelper.GetBillTypeByType();
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       public static DataTable GetBillTypeByType(string TypeFlag)
       {
           try
           {
               return ApprovalFlowSetDBHelper.GetBillTypeByType(TypeFlag);
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       public static DataTable GetBillTypeByTypeCode(string TypeFlag)
       {
           try
           {
               return ApprovalFlowSetDBHelper.GetBillTypeByTypeCode(TypeFlag);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
