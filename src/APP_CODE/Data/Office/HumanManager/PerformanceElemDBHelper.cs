/**********************************************
 * 类作用：   员工绩效考核指标及评分规则设置数据库层处理
 * 建立人：  王保军
 * 建立时间： 2009/04/21
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

namespace XBase.Data.Office.HumanManager
{  /// <summary>
    /// 类名：PerformanceElemDBHelper
    /// 描述：员工绩效考核指标及评分规则设置数据库层处理
    /// 
    /// 作者：王保军
    /// 创建时间：2009/04/21
    /// 最后修改时间：2009/04/21
    /// </summary>
    ///
    public class PerformanceElemDBHelper
    {

        /// <summary>
        /// 插入一个评分项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertPerformanceElem(PerformanceElemModel model)
        {
            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba.PerformanceElem ");
            insertSql.AppendLine("           (CompanyCD             ");
            insertSql.AppendLine("           ,ElemNo                ");
            insertSql.AppendLine("           ,ElemName                ");
            if (!(string.IsNullOrEmpty(model.ParentElemNo)))
            {
                insertSql.AppendLine("           ,ParentElemNo                ");
            }
            insertSql.AppendLine("           ,ScoreRules              ");
            insertSql.AppendLine("           ,StandardScore                 ");
            insertSql.AppendLine("           ,MinScore        ");
            insertSql.AppendLine("           ,MaxScore               ");
            insertSql.AppendLine("           ,AsseStandard                 ");
            insertSql.AppendLine("           ,AsseFrom               ");
            insertSql.AppendLine("           ,Remark               ");
            insertSql.AppendLine("           ,UsedStatus               ");
            insertSql.AppendLine("           ,ModifiedDate               ");
            insertSql.AppendLine("           ,ModifiedUserID    )           ");

            insertSql.AppendLine("     VALUES                        ");
            insertSql.AppendLine("           (@CompanyCD            ");
            insertSql.AppendLine("           ,@ElemNo               ");
            insertSql.AppendLine("           ,@ElemName               ");
            if (!(string.IsNullOrEmpty(model.ParentElemNo)))
            {
                insertSql.AppendLine("           ,@ParentElemNo                ");
            }
            insertSql.AppendLine("           ,@ScoreRules             ");
            insertSql.AppendLine("           ,@StandardScore               ");
            insertSql.AppendLine("           ,@MinScore         ");
            insertSql.AppendLine("           ,@MaxScore           ");
            insertSql.AppendLine("           ,@AsseStandard                ");
            insertSql.AppendLine("           ,@AsseFrom           ");
            insertSql.AppendLine("           ,@Remark           ");
            insertSql.AppendLine("           ,@UsedStatus           ");
            insertSql.AppendLine("           ,getdate()      ");
            insertSql.AppendLine("           ,@ModifiedUserID)           ");
            insertSql.AppendLine("     set @ElemID= @@IDENTITY         ");
            #endregion
            //定义插入基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);


            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ElemID", SqlDbType.Int));

            //执行插入操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            model.ID = comm.Parameters["@ElemID"].Value.ToString();
            //返回更新结果
            return isSucc;


        }
        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">考核类型信息</param>
        private static void SetSaveParameter(SqlCommand comm, PerformanceElemModel model)
        {

            //设置参数

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemNo", model.ElemNo));	//指标编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName));	//指标名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreRules", model.ScoreRules));	//评分细则
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardScore", model.StandardScore.ToString()));	//标准分
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinScore", model.MinScore.ToString()));	//评分最小值
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxScore", model.MaxScore.ToString()));	//评分最大值
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseStandard", model.AsseStandard));	//评分标准
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseFrom", model.AsseFrom));	//评分来源(如业绩)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));	//备注  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID

            if (!(string.IsNullOrEmpty(model.ParentElemNo)))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParentElemNo", model.ParentElemNo));	//更新用户ID
            }
            ///  if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            // {
            //     comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID ));	//指标类型编号
            //  }



        }
        #endregion
        /// <summary>
        /// 更新一个指标项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdatePerformanceElem(PerformanceElemModel model)
        {

            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.PerformanceElem      ");
            updateSql.AppendLine("   SET ElemName = @ElemName             ");
            updateSql.AppendLine("   ,ScoreRules = @ScoreRules       ");
            updateSql.AppendLine("   ,StandardScore = @StandardScore        ");

            updateSql.AppendLine("   ,MinScore = @MinScore             ");
            updateSql.AppendLine("   ,MaxScore = @MaxScore             ");
            updateSql.AppendLine("   ,AsseStandard = @AsseStandard       ");
            updateSql.AppendLine("   ,AsseFrom = @AsseFrom        ");

            updateSql.AppendLine("   ,Remark = @Remark             ");
            updateSql.AppendLine("   ,UsedStatus = @UsedStatus             ");
            updateSql.AppendLine("   ,ModifiedUserID = @ModifiedUserID        ");

            updateSql.AppendLine("   ,ModifiedDate = getdate()        ");

            updateSql.AppendLine("  WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD and  ID = @ElemNo                       ");


            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemNo", model.ID));	//指标编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName));	//指标名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ScoreRules", model.ScoreRules));	//评分细则
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardScore", model.StandardScore.ToString()));	//标准分
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinScore", model.MinScore.ToString()));	//评分最小值
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxScore", model.MaxScore.ToString()));	//评分最大值
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseStandard", model.AsseStandard));	//评分标准
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@AsseFrom", model.AsseFrom));	//评分来源(如业绩)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));	//备注  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID

            // if (!(string.IsNullOrEmpty(model.ParentElemNo)))
            // {
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParentElemNo", model.ParentElemNo));	//更新用户ID
            //}
            // comm.Parameters.Add(SqlHelper.GetParameterFromString("@ParentElemNo", model.ParentElemNo));
            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);



        }
        /// <summary>
        /// 完成搜索
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="deptID">指标编号</param>
        /// <returns></returns>
        public static DataTable SearchDeptInfo(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 A.ID                    ");
            searchSql.AppendLine(" 	,A.ElemNo               ");
            searchSql.AppendLine(" 	,ISNULL(A.ElemName,'') as ElemName               ");
            searchSql.AppendLine(" 	,A.ParentElemNo          ");
            searchSql.AppendLine(" 	,(SELECT COUNT(ID)       ");
            searchSql.AppendLine(" 		FROM                 ");
            searchSql.AppendLine(" 		officedba.PerformanceElem B");
            searchSql.AppendLine(" 		WHERE                ");
            searchSql.AppendLine(" 		 B.CompanyCD=A.CompanyCD  and B.ParentElemNo = A.ID)");
            searchSql.AppendLine(" 	AS SubCount              ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.PerformanceElem    A  ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //评分项目父指标编号未输入时，查询
            if (string.IsNullOrEmpty(deptID))
            {
                searchSql.AppendLine(" AND A.ParentElemNo IS NULL ");
            }
            //获取子指标编号机构
            else
            {
                searchSql.AppendLine(" AND A.ParentElemNo = @SuperDeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SuperDeptID", deptID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 删除指标项目
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static bool DeleteDeptInfo(string deptID, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.PerformanceElem ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine("  companyCD=@companyCD  and ID = @ElemNo");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@ElemNo", deptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@companyCD", companyCD));

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        /// <summary>
        /// 查看是否已经被模板库引用
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static bool IsHaveReferring(string elemNo, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" select count(*)  from officedba.PerformanceTemplateElem where  CompanyCD=@CompanyCD  and ElemID=@ElemID");

            //定义更新基本信息的命令
            // SqlCommand comm = new SqlCommand();
            //comm.CommandText = deleteSql.ToString();

            //设置参数
            // comm.Parameters.Add(SqlHelper.GetParameter("@ElemID", elemNo));

            SqlParameter[] param = new SqlParameter[2];
            //指标编号要素ID
            param[0] = SqlHelper.GetParameter("@ElemID", elemNo);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行插入操作并返回更新结果
            int a = Convert.ToInt32(SqlHelper.ExecuteScalar(deleteSql.ToString(), param));
            if (a > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断是否有子节点
        /// </summary>
        /// <param name="elemNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsHaveChild(string elemNo, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" select isnull(ParentElemNo,'') as ParentElemNo   from officedba.PerformanceElem where  CompanyCD=@CompanyCD and ElemNo=@ElemNo");

            //定义更新基本信息的命令
            // SqlCommand comm = new SqlCommand();
            //comm.CommandText = deleteSql.ToString();

            //设置参数
            // comm.Parameters.Add(SqlHelper.GetParameter("@ElemID", elemNo));

            SqlParameter[] param = new SqlParameter[2];
            //指标编号要素ID
            param[0] = SqlHelper.GetParameter("@ElemNo", elemNo);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //执行插入操作并返回更新结果
            return Convert.ToString(SqlHelper.ExecuteScalar(deleteSql.ToString(), param)) == "" ? true : false;

        }
        /// <summary>
        /// 完成搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable SearchCheckElemInfo(PerformanceElemModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                             ");
            searchSql.AppendLine(" 	 ID                               ");
            searchSql.AppendLine(" 	,ISNULL(ElemName, '') AS ElemName ");
            searchSql.AppendLine(" 	,CASE UsedStatus                  ");
            searchSql.AppendLine(" 	WHEN '0' THEN '停用'              ");
            searchSql.AppendLine(" 	WHEN '1' THEN '启用'              ");
            searchSql.AppendLine(" 	ELSE ''                           ");
            searchSql.AppendLine(" 	END AS UsedStatusName            ");
            searchSql.AppendLine(" 	,MaxScore-MinScore AS ScoreArrange ");
            searchSql.AppendLine(" 	,StandardScore ");
            searchSql.AppendLine(" 	,isnull(AsseStandard,'') asAsseStandard  ");
            searchSql.AppendLine(" 	,isnull(AsseFrom,'') as AsseFrom ");
            searchSql.AppendLine(" 	,isnull(Remark,'') as Remark ");
            searchSql.AppendLine(" FROM                               ");
            searchSql.AppendLine(" 	officedba.PerformanceElem         ");
            searchSql.AppendLine(" WHERE                              ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //l
            if (!string.IsNullOrEmpty(model.ElemName))
            {
                searchSql.AppendLine(" AND ElemName LIKE @ElemName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", "%" + model.ElemName + "%"));
            }
            //启用状态
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                searchSql.AppendLine(" AND UsedStatus = @UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetCheckElemInfoWithID(string ID)
        {
            #region 查询要素信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                             ");
            searchSql.AppendLine(" 	ElemNo ");
            searchSql.AppendLine(" 	,ISNULL(ElemName,'') as ElemName ");
            searchSql.AppendLine(" 	, UsedStatus                  ");
            searchSql.AppendLine(" 	,MaxScore ");
            searchSql.AppendLine(" 	,MinScore ");
            searchSql.AppendLine(" 	,StandardScore ");
            searchSql.AppendLine(" 	,ISNULL(AsseStandard,'') as AsseStandard ");
            searchSql.AppendLine(" 	,ISNULL(AsseFrom,'') as AsseForm ");
            searchSql.AppendLine(" 	,ISNULL(Remark,'') as Remark ");
            searchSql.AppendLine(" 	,isnull(ScoreRules,'') as ScoreRules ");
            searchSql.AppendLine(" FROM                     ");
            searchSql.AppendLine(" 	officedba.PerformanceElem ");
            searchSql.AppendLine(" WHERE                    ");
            searchSql.AppendLine(" 	ID = @ID            ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //指标编号要素ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        public static bool IsExist(string ElemNo, string CompanyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.PerformanceElem ");
            searchSql.AppendLine(" WHERE  CompanyCD='"+CompanyCD+"' ");
            searchSql.AppendLine("and ElemNo = '"+ElemNo+"'");

            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
    }
}
