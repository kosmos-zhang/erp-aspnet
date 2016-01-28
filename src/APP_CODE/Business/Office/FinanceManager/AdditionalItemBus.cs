/**********************************************
 * 描述：     辅助数据项业务处理
 * 建立人：   莫申林
 * 建立时间： 2009/06/01
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using System.Collections;

namespace XBase.Business.Office.FinanceManager
{
   
   public  class AdditionalItemBus
    {
       private AdditionalItemDBHelper additionalItemDBHelper = new AdditionalItemDBHelper();//实例化数据层对象

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AdditionalItemBus()
        {

        }
       /// <summary>
       /// 添加辅助数据项
       /// </summary>
       /// <param name="Model">辅助数据项实体</param>
       /// <param name="IntID">抛出生成记录的主键ID</param>
       /// <returns></returns>
        public bool InsertAdditionalItem(ArrayList List)
       {
           try
           {
               return additionalItemDBHelper.InsertAdditionalItem(List);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 更新辅助数据项信息
       /// </summary>
       /// <param name="Model">辅助数据项实体</param>
       /// <returns></returns>
       public bool UpdateAdditionalItem(AdditionalItemModel Model)
       {
           try
           {
               return additionalItemDBHelper.UpdateAdditionalItem(Model);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 删除辅助数据项信息
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <returns></returns>
       public bool DeleteAdditionalItem(string DuringDate)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
               if (string.IsNullOrEmpty(CompanyCD))
               {
                   return false;
               }
               else
               {
                   return additionalItemDBHelper.DeleteAdditionalItem(DuringDate, CompanyCD);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 判断辅助数据项是否设置
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <returns></returns>
       public bool IsExist(string DuringDate)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
               if (string.IsNullOrEmpty(CompanyCD))
               {
                   return false;
               }
               else
               {
                   return additionalItemDBHelper.IsExist(DuringDate, CompanyCD);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// 获取辅助数据项信息
       /// </summary>
       /// <param name="DuringDate">会计期间</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public DataTable GetAdditionalItemInfo(string DuringDate)
       {
           try
           {
               string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司编码
               if (string.IsNullOrEmpty(CompanyCD))
               {
                   return null;
               }
               else
               {
                   return additionalItemDBHelper.GetAdditionalItemInfo(DuringDate, CompanyCD);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
