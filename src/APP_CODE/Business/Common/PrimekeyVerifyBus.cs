/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.03.19
 * 描    述： 字段唯一性验证
 * 修改日期： 2009.03.19
 * 版    本： 0.1.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;
using XBase.Common;
using System.Text;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：PrimekeyVerifyBus
    /// 描述：字段唯一性验证
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/19
    /// 最后修改时间：2009/03/19
    /// </summary>
    ///
    public class PrimekeyVerifyBus
    {
        #region 校验编号的唯一性
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue)
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch (System.Exception)
            {

                companyCD = "AAAAAA"; ;
            }
            

            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq(tableName, columnName, codeValue, companyCD);
        }
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq1(string tableName, string columnName, string codeValue,string Flag)
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch (System.Exception)
            {

                companyCD = "AAAAAA"; ;
            }
            

            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq1(tableName, columnName, codeValue, companyCD,Flag);
        }
        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue,string Condition)
        {
            string companyCD = string.Empty;
            //获取公司代码
            try
            {
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }
            catch (System.Exception)
            {

                companyCD = "AAAAAA"; ;
            }


            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq(tableName, columnName, codeValue, companyCD,Condition);
        }
        /// <summary>
        /// 不根据企业编码验证唯一
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static bool CheckUserUniq(string tableName, string columnName, string codeValue)
        {
            return PrimekeyVerifyDBHelper.CheckUserUniq(tableName, columnName, codeValue);
        }
        #endregion
    }
}
