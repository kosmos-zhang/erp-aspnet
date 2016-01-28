/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/04/11
 * 描    述： 文件上传
 * 修改日期： 2009/04/11
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;
using XBase.Common;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：UploadFileBus
    /// 描述：文件上传
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/11
    /// 最后修改时间：2009/04/11
    /// </summary>
    ///
    public class UploadFileBus
    {
        
        /// <summary>
        /// 获取公司文件上传相关信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetCompanyUploadFileInfo()
        {
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //执行查询并返回值
            return UploadFileDBHelper.GetCompanyUploadFileInfo(companyCD);
        }
    }
}
