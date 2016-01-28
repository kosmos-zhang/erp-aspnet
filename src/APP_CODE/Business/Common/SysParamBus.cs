/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/10
 * 描    述： 公共分类列表
 * 修改日期： 2009/03/10
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：SysParamBus
    /// 描述：公共分类列表选择的数据处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///
    public class SysParamBus
    {

        #region 获取公共分类信息
        /// <summary>
        /// 公共下拉列表选择查询数据
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <param name="number">参数编号</param>
        /// <returns></returns>
        public static DataTable GetSysParamInfoForDrp(string type, string number)
        {
            //查询公共分类信息
            return SysParamDBHelper.GetSysParamInfoForDrp(type, number);
        }
        #endregion

        #region 通过参数类型、参数编号以及参数编码获取参数值/// <summary>
        /// 通过参数类型、参数编号以及参数编码获取参数值
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <param name="number">参数编号</param>
        /// <param name="code">参数编码</param>
        /// <returns></returns>
        public static string GetNameFromTypeNumCode(string type, string number, string code)
        {
            return SysParamDBHelper.GetNameFromTypeNumCode(type, number, code);
        }
        #endregion
    }
}