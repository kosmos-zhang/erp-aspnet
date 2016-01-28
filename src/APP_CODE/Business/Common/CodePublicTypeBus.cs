/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/10
 * 描    述： 公共分类列表
 * 修改日期： 2009/03/10
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;
using XBase.Common;
using System.Text;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：CodePublicTypeBus
    /// 描述：公共分类列表选择的数据处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/10
    /// 最后修改时间：2009/03/10
    /// </summary>
    ///
    public class CodePublicTypeBus
    {

        #region 获取公共分类信息
        /// <summary>
        /// 获取公共分类信息
        /// </summary>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <returns>DataTable 公共分类信息</returns>
        public static DataTable GetCodeTypeInfo(string codeFlag, string typeCode)
        {
            string companyCD = string.Empty;
           
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           

            //查询公共分类信息
            return CodePublicTypeDBHelper.GetCodeTypeInfoForDrp(companyCD, codeFlag, typeCode);
        }
        #endregion

        #region 返回input类型下拉列表的字符串
        /// <summary>
        /// 类型下拉列表的字符串
        /// </summary>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <param name="selectID">控件ID</param>
        /// <param name="className">样式表单</param>
        /// <param name="isInsert">是否插入【请选择】选项 true 插入 false 不插入</param>
        /// <param name="selectValue">选中项</param>
        /// <returns>input下拉列表字符串</returns>
        public static string CreateSelectInputControlString(string codeFlag, string typeCode, string selectID, string className, bool isInsert, string selectValue)
        {
            //定义返回的变量
            StringBuilder inputSelect = new StringBuilder();
            //开始标识
            inputSelect.AppendLine("<select id='" + selectID + "' class='" + className + "'>");
            //生成选择项
            inputSelect.AppendLine(CodePublicTypeBus.CreateSelectInputControlOptions(codeFlag, typeCode, isInsert, selectValue));
            //结束标识
            inputSelect.AppendLine("</select>");
            //返回生成的字符串
            return inputSelect.ToString();
        }
        #endregion

        #region 返回input类型下拉列表的选项字符串
        /// <summary>
        /// 返回input类型下拉列表的选项字符串
        /// </summary>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <param name="isInsert">是否插入【请选择】选项 true 插入 false 不插入</param>
        /// <param name="selectValue">选中项</param>
        /// <returns>input下拉列表字符串</returns>
        public static string CreateSelectInputControlOptions(string codeFlag, string typeCode, bool isInsert, string selectValue)
        {
            string companyCD = string.Empty;
           
                companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            //获取数据
            DataTable data = CodePublicTypeDBHelper.GetCodeTypeInfoForDrp(companyCD, codeFlag, typeCode);
            //定义返回的变量
            StringBuilder inputSelect = new StringBuilder();
            //数据存在时
            if (data != null && data.Rows.Count > 0)
            {
                //是否插入【请选择】选项
                if (isInsert)
                {
                    //添加选择项
                    inputSelect.AppendLine("<option value='" + ConstUtil.CODE_TYPE_INSERT_VALUE + "'>" + ConstUtil.CODE_TYPE_INSERT_TEXT + "</option>");
                }
                //是否设置过选中项标识
                bool isSelect = false;
                //遍历所有分类生成选择项
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    //获取分类ID
                    string typeID = data.Rows[i]["ID"].ToString();
                    //添加选择项
                    inputSelect.AppendLine("<option value='" + typeID + "' ");
                    //如果选中项还未设置，并且设置的选中项不为空时，设置选中项
                    if (!isSelect && !string.IsNullOrEmpty(selectValue) && selectValue.Equals(typeID))
                    {
                        //设置为选中项
                        inputSelect.AppendLine(" selected ");
                        //更改标识
                        isSelect = true;
                    }
                    inputSelect.AppendLine(">" + data.Rows[i]["TypeName"].ToString() + "</option>");

                }
            }
            //返回生成的字符串
            return inputSelect.ToString();
        }
        #endregion

        #region 根据分类标识分类编码获取分类名称

        /// <summary>
        /// 根据分类标识分类编码获取分类名称
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="codeFlag">分类标识</param>
        /// <param name="typeCode">分类编码</param>
        /// <returns></returns>
        public static string GetNameFromCode(string companyCD, string codeFlag, string typeCode)
        {
            //查询公共分类信息
            return CodePublicTypeDBHelper.GetNameFromFlagCode(companyCD, codeFlag, typeCode);
        }
        #endregion

        #region 根据ID获取分类名称

        /// <summary>
        /// 获取分类名称
        /// 主键ID
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public static string GetNameFromID(string ID)
        {
            //查询公共分类信息
            return CodePublicTypeDBHelper.GetNameFromID(ID);
        }
        #endregion

    }
}
