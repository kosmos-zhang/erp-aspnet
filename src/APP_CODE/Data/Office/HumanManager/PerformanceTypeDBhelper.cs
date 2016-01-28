/**********************************************
 * 类作用：   员工绩效考核数据库层处理
 * 建立人：  王保军
 * 建立时间： 2009/04/20
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
{
    /// <summary>
    /// 类名：PerformanceTypeDBhelper
    /// 描述：员工绩效考核数据库层处理
    /// 
    /// 作者：王保军
    /// 创建时间：2009/04/20
    /// 最后修改时间：2009/04/20
    /// </summary>
    ///
    public  class PerformanceTypeDBhelper
    {

        public static  bool InsertPerformanceType(PerformanceTypeModel model)
        {
            #region 插入SQL拼写
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine("INSERT INTO officedba.PerformanceType ");
            insertSql.AppendLine("           (CompanyCD             ");
            insertSql.AppendLine("           ,TypeName                ");
            insertSql.AppendLine("           ,CreateUser              ");
            insertSql.AppendLine("           ,CreateDate                 ");
            insertSql.AppendLine("           ,UsedStatus           ");
            insertSql.AppendLine("           ,ModifiedDate               ");
            insertSql.AppendLine("           ,ModifiedUserID)                 ");
          
            insertSql.AppendLine("     VALUES                        ");
            insertSql.AppendLine("           (@CompanyCD            ");
            insertSql.AppendLine("           ,@TypeName               ");
            insertSql.AppendLine("           ,@CreateUser             ");
            insertSql.AppendLine("           ,getdate()               ");
            insertSql.AppendLine("           ,@UsedStatus          ");
            insertSql.AppendLine("           ,getdate()             ");
            insertSql.AppendLine("           ,@ModifiedUserID)                ");
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
        private static void SetSaveParameter(SqlCommand comm, PerformanceTypeModel  model)
        {
            //设置参数
         
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));	//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeName", model.TypeName));	//类型名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateUser", model.CreateUser.ToString ()));	//创建人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));	//启用状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));	//更新用户ID
            if (ConstUtil.EDIT_FLAG_UPDATE.Equals(model.EditFlag))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID));	//考核类型编号
            }
          
           
            
        }
        #endregion
        public  static bool UpdatePerformanceType(PerformanceTypeModel model)
        {

             StringBuilder updateSql = new StringBuilder();
             updateSql.AppendLine(" UPDATE officedba.PerformanceType      ");
            updateSql.AppendLine(" SET CompanyCD = @CompanyCD          ");
            updateSql.AppendLine("   ,TypeName = @TypeName             ");
            updateSql.AppendLine("   ,UsedStatus = @UsedStatus             ");
            updateSql.AppendLine("   ,ModifiedDate = getdate()         ");
            updateSql.AppendLine("   ,ModifiedUserID = @ModifiedUserID        ");
            updateSql.AppendLine("  WHERE                              ");
            updateSql.AppendLine(" 	ID = @ID                       ");
           

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);



        }
        public static DataTable SearchCheckElemInfo(PerformanceTypeModel  model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                             ");
            searchSql.AppendLine(" 	 ID                               ");
            searchSql.AppendLine(" 	,ISNULL(TypeName, '') AS TypeName ");
            searchSql.AppendLine(" 	,CASE UsedStatus                  ");
            searchSql.AppendLine(" 	WHEN '0' THEN '停用'              ");
            searchSql.AppendLine(" 	WHEN '1' THEN '启用'              ");
            searchSql.AppendLine(" 	 WHEN NULL THEN ''                  ");
            searchSql.AppendLine(" 	ELSE ''                           ");
            searchSql.AppendLine(" 	END AS UsedStatusName ,isnull( Convert(varchar(100),ModifiedDate,23),'') AS ModifiedDate          ");
            searchSql.AppendLine(" FROM                               ");
            searchSql.AppendLine(" 	officedba.PerformanceType         ");
            searchSql.AppendLine(" WHERE                              ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //l
            if (!string.IsNullOrEmpty(model.TypeName ))
            {
                searchSql.AppendLine(" AND TypeName LIKE @TypeName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeName", "%"+model.TypeName+"%" ));
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
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine(" 	 TypeName               ");
            searchSql.AppendLine(" 	,UsedStatus             ");
            searchSql.AppendLine(" FROM                     ");
            searchSql.AppendLine(" 	officedba.PerformanceType ");
            searchSql.AppendLine(" WHERE                    ");
            searchSql.AppendLine(" 	ID = @ID            ");
            #endregion

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //面试评测要素ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            return SqlHelper.ExecuteSql(searchSql.ToString(), param);
        }
        public static bool IsTemplateUsed(string elemID, string CompanyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                       ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount       ");
            searchSql.AppendLine(" FROM                         ");
            searchSql.AppendLine(" 	officedba.PerformanceTemplate ");
            searchSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "' ");
            searchSql.AppendLine("and TypeID in (" + elemID + ")");

            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSql(searchSql.ToString());
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        public static bool DeletePerTypeInfo(string elemID, string CompanyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.PerformanceType ");
            deleteSql.AppendLine(" WHERE  CompanyCD='" + CompanyCD + "'");
            deleteSql.AppendLine("and  ID IN (" + elemID + ")");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = deleteSql.ToString();

            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
       

    }
}
