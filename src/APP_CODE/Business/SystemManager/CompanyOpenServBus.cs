/**********************************************
 * 类作用：   客户公司开通业务
 * 建立人：   吴志强
 * 建立时间： 2009/01/21
 ***********************************************/

using System.Data;
using XBase.Data.SystemManager;
using XBase.Model.SystemManager;

namespace XBase.Business.SystemManager
{
    /// <summary>
    /// 类名：CompanyOpenServDBHelper
    /// 描述：公司业务开通数据层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/21
    /// 最后修改时间：2009/01/21
    /// </summary>
    ///
    public class CompanyOpenServBus
    {
        /// <summary>
        /// 客户公司信息更新或者插入
        /// </summary>
        /// <param name="model">公司信息</param>
        /// <returns>更新成功与否</returns>
        public static bool ModifyCompanyOpenServInfo(CompanyOpenServModel model)
        {
            model.ModifiedUserID = "admin";
            return CompanyOpenServDBHelper.ModifyCompanyOpenServInfo(model);
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>客户公司信息</returns>
        public static CompanyOpenServModel GetCompanyOpenServInfo(string companyCD)
        {
            return CompanyOpenServDBHelper.GetCompanyOpenServInfo(companyCD);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static bool DeleteCompanyOpenServInfo(string companyCD)
        {
            return CompanyOpenServDBHelper.DeleteCompanyOpenServInfo(companyCD);
        }

        public static void UpdateCompanyManMsgNum(string companyCD, int count)
        {
            CompanyOpenServDBHelper.UpdateCompanyManMsgNum(companyCD, count);
        }

        public static void UpdateCompanyAutoMsgNum(string companyCD, int count)
        {
            CompanyOpenServDBHelper.UpdateCompanyAutoMsgNum(companyCD, count);
        }
    }
}
