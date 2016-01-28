/**********************************************
 * 类作用：   面试评测要素操作
 * 建立人：   吴志强
 * 建立时间： 2009/04/15
 ***********************************************/
using System;
using XBase.Model.Office.HumanManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using XBase.Common;

namespace XBase.Data.Office.HumanManager
{
    /// <summary>
    /// 类名：RectCheckElemDBHelper
    /// 描述：面试评测要素操作
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/04/15
    /// 最后修改时间：2009/04/15
    /// </summary>
    ///
    public class RectCheckElemDBHelper
    {
        #region 添加面试评测要素信息
        /// <summary>
        /// 添加面试评测要素信息
        /// </summary>
        /// <param name="model">面试评测要素信息</param>
        /// <returns></returns>
        public static bool InsertCheckElemInfo(RectCheckElemModel model)
        {

            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO officedba.RectCheckElem ");
            insertSql.AppendLine("            (CompanyCD               ");
            insertSql.AppendLine("            ,ElemName                ");
            insertSql.AppendLine("            ,Standard                ");
            insertSql.AppendLine("            ,Remark                  ");
            insertSql.AppendLine("            ,UsedStatus              ");
            insertSql.AppendLine("            ,ModifiedDate            ");
            insertSql.AppendLine("            ,ModifiedUserID)         ");
            insertSql.AppendLine("      VALUES                         ");
            insertSql.AppendLine("            (@CompanyCD              ");
            insertSql.AppendLine("            ,@ElemName               ");
            insertSql.AppendLine("            ,@Standard               ");
            insertSql.AppendLine("            ,@Remark                 ");
            insertSql.AppendLine("            ,@UsedStatus             ");
            insertSql.AppendLine("            ,getdate()               ");
            insertSql.AppendLine("            ,@ModifiedUserID)        ");
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
        #endregion

        #region 更新面试评测要素信息
        /// <summary>
        /// 更新面试评测要素信息
        /// </summary>
        /// <param name="model">保存信息</param>
        /// <returns></returns>
        public static bool UpdateCheckElemInfo(RectCheckElemModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.RectCheckElem      ");
            updateSql.AppendLine(" SET CompanyCD = @CompanyCD          ");
            updateSql.AppendLine("   ,ElemName = @ElemName             ");
            updateSql.AppendLine("   ,Standard = @Standard             ");
            updateSql.AppendLine("   ,Remark = @Remark                 ");
            updateSql.AppendLine("   ,UsedStatus = @UsedStatus         ");
            updateSql.AppendLine("   ,ModifiedDate = getdate()         ");
            updateSql.AppendLine("   ,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine("  WHERE                              ");
            updateSql.AppendLine(" 	ID = @ElemID                       ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">保存信息</param>
        private static void SetSaveParameter(SqlCommand comm, RectCheckElemModel model)
        {
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName));//要素名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Standard", model.Standard));//评分标准
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemID", model.ID));//ID
            }
        }
        #endregion

        #region 通过检索条件查询面试评测要素信息
        /// <summary>
        /// 查询面试评测要素信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable SearchCheckElemInfo(RectCheckElemModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                             ");
            searchSql.AppendLine(" 	 ID                               ");
            searchSql.AppendLine(" 	,ISNULL(ElemName, '') AS ElemName ");
            searchSql.AppendLine(" 	,ISNULL(Standard, '') AS Standard ");
            searchSql.AppendLine(" 	,ISNULL(Remark, '') AS Remark     ");
            searchSql.AppendLine(" 	,CASE UsedStatus                  ");
            searchSql.AppendLine(" 	WHEN '0' THEN '停用'              ");
            searchSql.AppendLine(" 	WHEN '1' THEN '启用'              ");
            searchSql.AppendLine(" 	ELSE ''                           ");
            searchSql.AppendLine(" 	END AS UsedStatusName  ,isnull( Convert(varchar(100),ModifiedDate,23),'') AS ModifiedDate           ");
            searchSql.AppendLine(" FROM                               ");
            searchSql.AppendLine(" 	officedba.RectCheckElem           ");
            searchSql.AppendLine(" WHERE                              ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //要素名称
            if (!string.IsNullOrEmpty(model.ElemName))
            {
                searchSql.AppendLine(" AND ElemName LIKE '%' + @ElemName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ElemName", model.ElemName));
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
        #endregion

        #region 通过ID查询面试评测要素信息
        /// <summary>
        /// 查询面试评测要素信息
        /// </summary>
        /// <param name="elemID">面试评测要素ID</param>
        /// <returns></returns>
        public static DataTable GetCheckElemInfoWithID(string elemID)
        {
            #region 查询要素信息
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine(" 	 ElemName               ");
            searchSql.AppendLine(" 	,Standard               ");
            searchSql.AppendLine(" 	,Remark                 ");
            searchSql.AppendLine(" 	,UsedStatus             ");
            searchSql.AppendLine(" FROM                     ");
            searchSql.AppendLine(" 	officedba.RectCheckElem ");
            searchSql.AppendLine(" WHERE                    ");
            searchSql.AppendLine(" 	ID = @ElemID            ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //面试评测要素ID
            param[0] = SqlHelper.GetParameter("@ElemID", elemID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }

        #endregion

        #region 删除面试评测要素信息
        /// <summary>
        /// 删除面试评测要素信息
        /// </summary>
        /// <param name="elemID">面试评测要素ID</param>
        /// <returns></returns>
        public static bool DeleteRectCheckElemInfo(string elemID)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.RectCheckElem ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ID IN (" + elemID + ")");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 校验面试评测要素是否被引用
        /// <summary>
        /// 校验面试评测要素是否被引用
        /// </summary>
        /// <param name="elemID">要素ID</param>
        /// <returns></returns>
        public static bool IsRectCheckElemUsed(string elemID, string CompanyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.RectCheckTemplateElem ");
            searchSql.AppendLine(" WHERE    CompanyCD='"+CompanyCD +"'and                     ");
            searchSql.AppendLine("CheckElemID in (" + elemID + ")");
            
            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        #endregion
    }
}
