/***************************
 * 描述：批次规则设置
 * 创建人：何小武
 * 创建时间：2010-3-24
 * *************************/
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.SystemManager;

namespace XBase.Data.Office.SystemManager
{
    public class BatchNoRuleSetDBHelper
    {
        #region 根据公司编码获取批次规则表
        /// <summary>
        /// 根据公司编码获取批次规则表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>datatable批次规则信息</returns>
        public static DataTable GetBatchNoByCompanyCD(string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,CompanyCD,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,");
            strSql.AppendLine(" RuleExample,Remark,IsDefault,UsedStatus,Convert(varchar(100),ModifiedDate,23) as ModifiedDate,ModifiedUserID ");
            strSql.AppendLine(" from officedba.BatchRule ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };

            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 保存批次规则设置
        /// <summary>
        /// 保存批次规则设置
        /// </summary>
        /// <param name="model">BachNoRuleSet实体</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>规则ID</returns>
        public static int SaveBatchRule(BatchNoRuleSet model, out string strMsg)
        {
            int ruleID = 0;
            strMsg = "";
            StringBuilder strSql = new StringBuilder();
            if (!IsExisted(model.CompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.AppendLine(" insert into officedba.BatchRule ");
                    strSql.AppendLine(" (CompanyCD,RuleName,RulePrefix,RuleDateType,RuleNoLen,LastNo,RuleExample,");
                    strSql.AppendLine(" Remark,IsDefault,UsedStatus,ModifiedDate,ModifiedUserID) values");
                    strSql.AppendLine(" (@CompanyCD,@RuleName,@RulePrefix,@RuleDateType,@RuleNoLen,@LastNo,@RuleExample,");
                    strSql.AppendLine(" @Remark,@IsDefault,@UsedStatus,getdate(),@ModifiedUserID)");
                    strSql.AppendLine(" ;select @@IDENTITY ");
                    SqlParameter[] param = { 
                                                new SqlParameter("@CompanyCD",model.CompanyCD),
                                                new SqlParameter("@RuleName",model.RuleName),
                                                new SqlParameter("@RulePrefix",model.RulePrefix),
                                                new SqlParameter("@RuleDateType",model.RuleDateType),
                                                new SqlParameter("@RuleNoLen",model.RuleNoLen),
                                                new SqlParameter("@LastNo",model.LastNo),
                                                new SqlParameter("@RuleExample",model.RuleExample),
                                                new SqlParameter("@Remark",model.Remark),
                                                new SqlParameter("@IsDefault",model.IsDefault),
                                                new SqlParameter("@UsedStatus",model.UsedStatus),
                                                new SqlParameter("@ModifiedUserID",model.ModifiedUserID)
                                               };

                    foreach (SqlParameter para in param)
                    {
                        if (para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                    }
                    ruleID = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
                    tran.Commit();
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                strMsg = "保存失败，已存在批次规则！";   
            }

            return ruleID;
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
            StringBuilder strSql = new StringBuilder();
            strMsg = "";
            bool isSuc = false;
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                strSql.AppendLine(" update officedba.BatchRule set ");
                strSql.AppendLine(" RuleName=@RuleName,RulePrefix=@RulePrefix,RuleDateType=@RuleDateType,RuleNoLen=@RuleNoLen,");
                strSql.AppendLine(" RuleExample=@RuleExample,Remark=@Remark,IsDefault=@IsDefault,");
                strSql.AppendLine(" UsedStatus=@UsedStatus,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                strSql.AppendLine(" where ID=@ID and CompanyCD=@CompanyCD ");
                SqlParameter[] param = { 
                                        new SqlParameter("@ID",model.ID),
                                        new SqlParameter("@CompanyCD",model.CompanyCD),
                                        new SqlParameter("@RuleName",model.RuleName),
                                        new SqlParameter("@RulePrefix",model.RulePrefix),
                                        new SqlParameter("@RuleDateType",model.RuleDateType),
                                        new SqlParameter("@RuleNoLen",model.RuleNoLen),
                                        //new SqlParameter("@LastNo",model.LastNo),
                                        new SqlParameter("@RuleExample",model.RuleExample),
                                        new SqlParameter("@Remark",model.Remark),
                                        new SqlParameter("@IsDefault",model.IsDefault),
                                        new SqlParameter("@UsedStatus",model.UsedStatus),
                                        new SqlParameter("@ModifiedUserID",model.ModifiedUserID)
                                   };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                tran.Commit();
                strMsg = "保存成功！";
                isSuc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "保存失败，请联系系统管理员！";
                isSuc = false;
                throw ex;
            }

            return isSuc;

        }
        #endregion

        #region 判断该公司是否已定义了批次规则
        /// <summary>
        /// 判断该公司是否已定义了批次规则
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>true已定义，false没定义</returns>
        private static bool IsExisted(string strCompanyCD)
        {
            int count = 0;
            bool isScc = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select count(1) from officedba.BatchRule ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD )
                                   };

            count = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (count > 0)
            {
                isScc = true;
            }
            return isScc;
        }
        #endregion

        #region 批次规则下拉列表查询数据
        /// <summary>
        /// 批次规则下拉列表查询数据
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="codingType">编码类型</param>
        /// <param name="itemTypeID">单据代码或基础数据代码</param>
        /// <returns></returns>
        public static DataTable GetBatchRuleInfoForDrp(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine("SELECT ID                     ");
            searchSql.AppendLine("		,CompanyCD              ");
            searchSql.AppendLine("		,RuleName               ");
            searchSql.AppendLine("      ,IsDefault              ");
            searchSql.AppendLine(" FROM officedba.BatchRule  ");
            searchSql.AppendLine(" WHERE                        ");
            searchSql.AppendLine("	CompanyCD = @CompanyCD      ");
            searchSql.AppendLine("	AND UsedStatus = @UsedStatus");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[i++] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的分类信息
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        #endregion

        #region 根据批次规则ID获取批次规则数据
        /// <summary>
        /// 根据批次规则ID获取批次规则数据
        /// </summary>
        /// <param name="codeID">批次规则ID</param>
        /// <returns></returns>
        public static DataTable GetBatchRuleInfoWithID(string codeID)
        {
            //定义查询SQL变量
            StringBuilder searchSql = new StringBuilder();
            //定义查询语句
            searchSql.AppendLine(" SELECT ID           ");
            searchSql.AppendLine("      ,CompanyCD     ");
            //searchSql.AppendLine("      ,CodingType    ");
            //searchSql.AppendLine("      ,ItemTypeID    ");
            searchSql.AppendLine("      ,RuleName      ");
            searchSql.AppendLine("      ,RulePrefix    ");
            searchSql.AppendLine("      ,RuleDateType  ");
            searchSql.AppendLine("      ,RuleNoLen     ");
            searchSql.AppendLine("      ,LastNo        ");
            searchSql.AppendLine("      ,RuleExample   ");
            searchSql.AppendLine("      ,IsDefault     ");
            searchSql.AppendLine("      ,Remark        ");
            searchSql.AppendLine("      ,UsedStatus    ");
            searchSql.AppendLine("      ,ModifiedDate  ");
            searchSql.AppendLine("      ,ModifiedUserID ");
            searchSql.AppendLine(" FROM officedba.BatchRule  ");
            searchSql.AppendLine("WHERE     ");
            searchSql.AppendLine("	ID = @CodeID       ");
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //编码规则ID
            param[0] = SqlHelper.GetParameter("@CodeID", codeID);

            //执行查询并返回的查询到的分类信息
            DataTable data = SqlHelper.ExecuteSql(searchSql.ToString(), param);
            //数据存在时，更新当前流水号最大值
            bool updateFlag = false; //更新成功标识
            if (data.Rows.Count > 0)
            {
                //编码流水号长度
                int ruleNoLen = GetSafeData.ValidateDataRow_Int(data.Rows[0], "RuleNoLen");
                //定义最大流水号
                string maxNo = string.Empty;
                //最大流水号
                for (int i = 0; i < ruleNoLen; i++)
                {
                    maxNo += "9";
                }
                //获取当前流水号最大值
                int lastNo = GetSafeData.ValidateDataRow_Int(data.Rows[0], "LastNo");
                //当前最大流水号+1
                int updateNo = lastNo + 1;
                //当前最大流水号+1 大于 最大流水号时，从1开始记数
                if (updateNo > int.Parse(maxNo))
                {

                    return null;
                    //updateNo = 1;
                }
                //重新设置最大流水号，生成编号不用重复计算
                data.Rows[0]["LastNo"] = updateNo;
                //更新操作
                StringBuilder updateSql = new StringBuilder();
                updateSql.AppendLine(" UPDATE officedba.BatchRule ");
                updateSql.AppendLine("    SET                          ");
                updateSql.AppendLine("       LastNo = @LastNo          ");
                updateSql.AppendLine("  WHERE ID = @CodeID             ");
                //设置参数
                SqlParameter[] updatePram = new SqlParameter[2];
                //编码规则ID
                updatePram[0] = SqlHelper.GetParameter("@CodeID", codeID);
                //最大流水号
                updatePram[1] = SqlHelper.GetParameter("@LastNo", updateNo);
                //执行更新
                updateFlag = SqlHelper.ExecuteTransSql(updateSql.ToString(), updatePram) > 0 ? true : false;
            }

            //返回获取的数据
            if (updateFlag)
            {
                return data;
            }
            return null;
        }
        #endregion
    }
}
