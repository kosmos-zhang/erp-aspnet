/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009/03/11
 * 描    述： 编码规则
 * 修改日期： 2009/03/11
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;
using XBase.Common;
using System;
using System.Text;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：ItemCodingRuleBus
    /// 描述：编码规则
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/11
    /// 最后修改时间：2009/03/11
    /// </summary>
    ///
    public class ItemCodingRuleBus
    {

        #region 编码规则下拉列表查询数据

        /// <summary>
        /// 编码规则下拉列表查询数据
        /// </summary>
        /// <param name="codingType">编码类型</param>
        /// <param name="itemTypeID">单据代码或基础数据代码</param>
        /// <returns></returns>
        public static DataTable GetCodingRuleInfoForDrp(string codingType, string itemTypeID)
        {
            string companyCD = string.Empty;
            //获取公司代码
            
                 companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            //查询编码规则信息
            return ItemCodingRuleDBHelper.GetCodingRuleInfoForDrp(companyCD, codingType, itemTypeID);
        }
        #endregion


        #region 根据编码规则获取单据编号

        /// <summary>
        /// 根据编码规则获取单据编号
        /// </summary>
        /// <param name="codeID">编码规则ID</param>
        /// <returns></returns>
        public static string GetCodeValue(string codeID)
        {
           
            //上次生成的最大流水号
            //int lastpreNo =-1;
            //定义返回的值
            string codeValue = "";
            //获取编码规则详细设置内容
            DataTable ruleData = ItemCodingRuleDBHelper.GetCodingRuleInfoWithID(codeID);
            //数据存在时进行Code生成操作
            if (ruleData != null && ruleData.Rows.Count > 0)
            {
                //获取编码示例
                string ruleExample = GetSafeData.ValidateDataRow_String(ruleData.Rows[0], "RuleExample");
                //编码示例长度小于4时，进行设置
                if (!string.IsNullOrEmpty(ruleExample) && ruleExample.Length > 4)
                {
                    //获取流水号类型
                    int start = ruleExample.LastIndexOf(ConstUtil.RULE_EXAMPLE_START);
                    string ruleNoType = ruleExample.Substring(start);

                    //获取当前流水号最大值
                    string lastNo = GetSafeData.ValidateDataRow_Int(ruleData.Rows[0], "LastNo").ToString();
                    //编码流水号长度
                    int ruleNoLen = GetSafeData.ValidateDataRow_Int(ruleData.Rows[0], "RuleNoLen");
                    //长度不够时左边添加0
                    lastNo = lastNo.PadLeft(ruleNoLen, ConstUtil.RULE_EXAMPLE_ZERO);
                    //替换流水号
                    codeValue = ruleExample.Replace(ruleNoType, lastNo);

                    //获取编码类型
                    string codingType = GetSafeData.ValidateDataRow_String(ruleData.Rows[0], "CodingType");
                    //编码类型为单据时，替换日期设置
                    if (!ConstUtil.CODING_RULE_TYPE_ZERO.Equals(codingType))
                    {
                        //获取流水号类型
                        start = ruleExample.IndexOf(ConstUtil.RULE_EXAMPLE_START);
                        int end = ruleExample.IndexOf(ConstUtil.RULE_EXAMPLE_END);
                        string dateType = ruleExample.Substring(start, end - start + 1);
                        //获取当前日期值
                        string dateTypeValue = DateTime.Now.ToString(dateType.Substring(1, dateType.Length - 2));
                        //替换日期类型
                        codeValue = codeValue.Replace(dateType, dateTypeValue);
                    }
                }
            }

            //返回生成的单据编号
            return codeValue;
        }
        #endregion

        #region 根据编码规则获取单据编号
        /// <summary>
        /// 根据编码规则获取单据编号
        /// </summary>
        /// <param name="codeID">编码规则ID</param>
        /// <param name="table">表名</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        public static string GetCodeValue(string codeID, string table, string column)
        {
            //获取单据编号
            string code = GetCodeValue(codeID);
            //校验是否已经存在
            bool isExist = PrimekeyVerifyBus.CheckCodeUniq(table, column, code);
            //已经存在时
            if (!isExist)
            {
                return GetCodeValue(codeID, table, column);
            }
            //不存在时
            else
            {
                return code;
            }
        }
        #endregion

    }
}
