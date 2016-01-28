

/**********************************************
 * 类作用   施工摘要表数据处理层
 * 创建人   xz
 * 创建时间 2010-5-19 9:42:20 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.Office.ProjectBudget;


 namespace XBase.Data.Office.ProjectBudget
{
    /// <summary>
    /// 施工摘要表数据处理类
    /// </summary>
    public class ProjectConstructionDBHelper
    {
        #region 字段
        
        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT SummaryID,CompanyCD,SummaryName,StructID,ProcessScale,KeyPress,ProessID,BeginDate,EndDate,StartFlag" +
                                "    ,GmanagerID,ProjectMemo" +
                                " FROM officedba.ProjectConstruction";
		private const string C_SELECT_ID =
                                " SELECT SummaryID,CompanyCD,SummaryName,StructID,ProcessScale,KeyPress,ProessID,BeginDate,EndDate,StartFlag" +
                                "    ,GmanagerID,ProjectMemo" +
                                " FROM officedba.ProjectConstruction"+
                                " WHERE ID=@ID";
        private const string C_SELECT =
                                " SELECT SummaryID,CompanyCD,SummaryName,StructID,ProcessScale,KeyPress,ProessID,BeginDate,EndDate,StartFlag" +
                                "    ,GmanagerID,ProjectMemo" +
                                " FROM officedba.ProjectConstruction" +
                                " WHERE CompanyCD=@CompanyCD  ";
		private const string C_INSERT =
                                " INSERT officedba.ProjectConstruction(" +
                                "    CompanyCD,SummaryName,StructID,ProcessScale,KeyPress,ProessID,BeginDate,EndDate,StartFlag" +
                                "    ,GmanagerID,ProjectMemo )" +
                                " VALUES (" +
                                "    @CompanyCD,@SummaryName,@StructID,@ProcessScale,@KeyPress,@ProessID,@BeginDate,@EndDate,@StartFlag" +
                                "    ,@GmanagerID,@ProjectMemo )";
        private const string C_UPDATE =
                                " UPDATE officedba.ProjectConstruction SET" +
                                "    CompanyCD=@CompanyCD,SummaryName=@SummaryName,StructID=@StructID,ProcessScale=@ProcessScale" +
                                "    ,KeyPress=@KeyPress,ProessID=@ProessID,BeginDate=@BeginDate,EndDate=@EndDate,StartFlag=@StartFlag" +
                                "    ,GmanagerID=@GmanagerID,ProjectMemo=@ProjectMemo" +
                                " WHERE SummaryID=@SummaryID";
        private const string C_DELETE =
                                " DELETE FROM officedba.ProjectConstruction WHERE CompanyCD=@CompanyCD ";
		
		private const string C_DELETE_ID =
                                " DELETE FROM officedba.ProjectConstruction WHERE SummaryID IN (@SummaryID) ";


        //字段顺序变量定义
        private const byte m_summaryIDCol = 0; // 概要编号列
        private const byte m_companyCDCol = 1; // 企业编码列
        private const byte m_summaryNameCol = 2; // 概要名称列
        private const byte m_structIDCol = 3; // 所属工程概要编号列
        private const byte m_processScaleCol = 4; // 工艺定额列
        private const byte m_keyPressCol = 5; // 是否为受约进度(0:不是，1是)列
        private const byte m_proessIDCol = 6; // 受约进度编号(多编号用”,”隔开)列
        private const byte m_beginDateCol = 7; // 开始时间列
        private const byte m_endDateCol = 8; // 结束时间列
        private const byte m_startFlagCol = 9; // 是否执行(0:不执行，1执行)列
        private const byte m_gmanagerIDCol = 10; // 工程负责人列
        private const byte m_projectMemoCol = 11; // 工程备注列
        #endregion

        #region 方法
        
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="summaryID">概要编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int summaryID)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);
            
            // 参数设置
            SqlParameter[] parameters =  new SqlParameter[] 
                        {
                            new SqlParameter("@SummaryID", SqlDbType.Int, 4)
                        };
            parameters[0].Value= summaryID; // 概要编号
            
            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }
		
		/// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业编码</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string companyCD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);
            
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters= SetSelectAndDeleteParametersValue(parameters, companyCD);
            
            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }
		
		/// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , ProjectConstructionModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(C_SELECT_ALL);
            sql.Append(" WHERE 1=1 ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
		
		/// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(ProjectConstructionModel model)
        {
            // SQL语句
			SqlCommand comm = new SqlCommand();
			comm.CommandText = C_INSERT + " SET @IndexID = @@IDENTITY ";
			// 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);
            
			comm.Parameters.AddRange(parameters);
			SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
			
			return comm;
		}
		
		/// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(ProjectConstructionModel model)
        {
			 // SQL语句
			SqlCommand comm = new SqlCommand();
			comm.CommandText = C_UPDATE;
			// 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);
            
			comm.Parameters.AddRange(parameters);
			
			return comm;
        }
		
		/// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(ProjectConstructionModel model)
        {
            bool returnValue = false; // 返回值
            
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_UPDATE);
            
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);
            
            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;
            
            return returnValue;
        }
		
		/// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="summaryID">ID集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string summaryID)
        {
			// SQL语句
			SqlCommand comm = new SqlCommand();
			comm.CommandText = C_DELETE_ID;
			// 参数设置
            SqlParameter[] parameters =  new SqlParameter[] 
                        {
                            new SqlParameter("@SummaryID", SqlDbType.VarChar)
                        };
            parameters[0].Value= summaryID; //ID集合
            
			comm.Parameters.AddRange(parameters);
			
			return comm;
        }
		
		/// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="summaryID">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string summaryID)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE_ID);
            
            // 参数设置
            SqlParameter[] parameters =  new SqlParameter[] 
                        {
                            new SqlParameter("@SummaryID", SqlDbType.VarChar)
                        };
            parameters[0].Value= summaryID; //ID集合
            
            
            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }
		
		/// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业编码</param>
        
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(string companyCD)
        {
			// SQL语句
			SqlCommand comm = new SqlCommand();
			comm.CommandText = C_DELETE;
			// 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters= SetSelectAndDeleteParametersValue(parameters, companyCD);
            
			comm.Parameters.AddRange(parameters);
			
			return comm;
        }
		
		/// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业编码</param>
        
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(string companyCD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);
            
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters= SetSelectAndDeleteParametersValue(parameters, companyCD);
            
            
            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }
		

        /// <summary>
        /// 设置查询和删除的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar, 50) // 企业编码
                        };
                        
            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@SummaryID", SqlDbType.Int,4), // 概要编号
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,50), // 企业编码
                            new SqlParameter("@SummaryName", SqlDbType.VarChar,200), // 概要名称
                            new SqlParameter("@StructID", SqlDbType.Int,4), // 所属工程概要编号
                            new SqlParameter("@ProcessScale", SqlDbType.Decimal,13), // 工艺定额
                            new SqlParameter("@KeyPress", SqlDbType.Int,4), // 是否为受约进度(0:不是，1是)
                            new SqlParameter("@ProessID", SqlDbType.VarChar,500), // 受约进度编号(多编号用”,”隔开)
                            new SqlParameter("@BeginDate", SqlDbType.DateTime,8), // 开始时间
                            new SqlParameter("@EndDate", SqlDbType.DateTime,8), // 结束时间
                            new SqlParameter("@StartFlag", SqlDbType.Int,4), // 是否执行(0:不执行，1执行)
                            new SqlParameter("@GmanagerID", SqlDbType.Int,4), // 工程负责人
                            new SqlParameter("@ProjectMemo", SqlDbType.VarChar,1000)  // 工程备注
                        };
                        
            return parameters;
        }


        
        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="companyCD">企业编码的值</param>
        
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, string companyCD)
        {
            parameters[0].Value = companyCD; // 企业编码
            
            
            return parameters;
        }
        
        
		
        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, ProjectConstructionModel model)
        {
            if (!model.SummaryID.HasValue) parameters[m_summaryIDCol].Value = System.DBNull.Value; else parameters[m_summaryIDCol].Value = model.SummaryID; // 概要编号
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业编码
            parameters[m_summaryNameCol].Value = model.SummaryName; // 概要名称
            if (!model.StructID.HasValue) parameters[m_structIDCol].Value = System.DBNull.Value; else parameters[m_structIDCol].Value = model.StructID; // 所属工程概要编号
            if (!model.ProcessScale.HasValue) parameters[m_processScaleCol].Value = System.DBNull.Value; else parameters[m_processScaleCol].Value = model.ProcessScale; // 工艺定额
            if (!model.KeyPress.HasValue) parameters[m_keyPressCol].Value = System.DBNull.Value; else parameters[m_keyPressCol].Value = model.KeyPress; // 是否为受约进度(0:不是，1是)
            parameters[m_proessIDCol].Value = model.ProessID; // 受约进度编号(多编号用”,”隔开)
            if (!model.BeginDate.HasValue) parameters[m_beginDateCol].Value = System.DBNull.Value; else parameters[m_beginDateCol].Value = model.BeginDate; // 开始时间
            if (!model.EndDate.HasValue) parameters[m_endDateCol].Value = System.DBNull.Value; else parameters[m_endDateCol].Value = model.EndDate; // 结束时间
            if (!model.StartFlag.HasValue) parameters[m_startFlagCol].Value = System.DBNull.Value; else parameters[m_startFlagCol].Value = model.StartFlag; // 是否执行(0:不执行，1执行)
            if (!model.GmanagerID.HasValue) parameters[m_gmanagerIDCol].Value = System.DBNull.Value; else parameters[m_gmanagerIDCol].Value = model.GmanagerID; // 工程负责人
            parameters[m_projectMemoCol].Value = model.ProjectMemo; // 工程备注
            
            return parameters;
        }
        
        
        #endregion
		
		#region 自定义
		
		#endregion
    }
}

