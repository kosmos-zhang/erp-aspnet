/***************************
 * 描述：批次规则设置
 * 创建人：何小武
 * 创建时间：2010-3-24
 * *************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Common;
using XBase.Data.Office.SystemManager;
using XBase.Model.Office.SystemManager;

namespace XBase.Business.Office.SystemManager
{
    public class BatchNoRuleSetBus
    {
        #region 保存批次规则设置
        /// <summary>
        /// 保存批次规则设置
        /// </summary>
        /// <param name="model">BachNoRuleSet实体</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>规则ID</returns>
        public static int SaveBatchRule(BatchNoRuleSet model, out string strMsg)
        {
            return BatchNoRuleSetDBHelper.SaveBatchRule(model, out strMsg);
        }
        #endregion

        #region 修改批次规则设置
        /// <summary>
        ///  修改批次规则设置
        /// </summary>
        /// <param name="tbModel">ModuleTableModel模板实体</param>
        /// <param name="strMsg"></param>
        public static bool UpdateBatchRule(BatchNoRuleSet model, out string strMsg)
        {
            return BatchNoRuleSetDBHelper.UpdateBatchRule(model, out strMsg);

        }
        #endregion

        #region 根据公司编码获取批次规则
        /// <summary>
        /// 根据公司编码获取批次规则
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>datatable 批次规则信息</returns>
        public static DataTable GetBatchRuleInfo(string strCompanyCD)
        {
            return BatchNoRuleSetDBHelper.GetBatchNoByCompanyCD(strCompanyCD);
        }
        #endregion

        #region 根据公司编码获取该编码是否已启用
        /// <summary>
        /// 根据公司编码获取该编码是否已启用
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>false停用，true启用</returns>
        public static bool GetBatchStatus(string strCompanyCD)
        {
            bool isUsing = false;
            DataTable dt = BatchNoRuleSetDBHelper.GetBatchNoByCompanyCD(strCompanyCD);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return isUsing;
            }
            else
            {
                if (dt.Rows[0]["UsedStatus"].ToString() == "1")
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region 批次规则下拉列表查询数据

        /// <summary>
        /// 批次规则下拉列表查询数据
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetBatchRuleInfoForDrp(string strCompanyCD)
        {
            //查询批次规则信息
            return BatchNoRuleSetDBHelper.GetBatchRuleInfoForDrp(strCompanyCD);
        }
        #endregion


        #region 根据批次规则获取单据编号

        /// <summary>
        /// 根据批次规则获取单据编号
        /// </summary>
        /// <param name="codeID">批次规则ID</param>
        /// <returns></returns>
        public static string GetCodeValue(string codeID)
        {

            //上次生成的最大流水号
            //int lastpreNo =-1;
            //定义返回的值
            string codeValue = "";
            //获取编码规则详细设置内容
            DataTable ruleData = BatchNoRuleSetDBHelper.GetBatchRuleInfoWithID(codeID);
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

            //返回生成的单据编号
            return codeValue;
        }
        #endregion

    }
}
