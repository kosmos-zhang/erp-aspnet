/**********************************************
 * 类作用   计量单位组数据处理层
 * 创建人   xz
 * 创建时间 2010-3-11 11:21:01 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.Office.SupplyChain;


namespace XBase.Data.Office.SupplyChain
{
    /// <summary>
    /// 计量单位组数据处理类
    /// </summary>
    public class UnitGroupDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,CompanyCD,GroupUnitNo,GroupUnitName,BaseUnitID,Remark" +
                                " FROM officedba.UnitGroup";
        private const string C_SELECT_ID =
                                @" SELECT ug.ID,ug.CompanyCD,ug.GroupUnitNo,ug.GroupUnitName,ug.BaseUnitID,ug.Remark
                                        ,case WHEN 
                                            (SELECT TOP(1) pi1.GroupUnitNo
							                    FROM officedba.ProductInfo pi1 
                                                WHERE pi1.CompanyCD=ug.CompanyCD AND pi1.GroupUnitNo =ug.GroupUnitNo 
                                             ) IS NULL
	                                          THEN 0
	                                          ELSE 1
                                        END AS isUse
                                  FROM officedba.UnitGroup ug
                                  WHERE ID=@ID";
        private const string C_SELECT =
                                " SELECT ID,CompanyCD,GroupUnitNo,GroupUnitName,BaseUnitID,Remark" +
                                " FROM officedba.UnitGroup" +
                                " WHERE CompanyCD=@CompanyCD AND GroupUnitNo=@GroupUnitNo ";
        private const string C_INSERT =
                                " INSERT officedba.UnitGroup(" +
                                "    CompanyCD,GroupUnitNo,GroupUnitName,BaseUnitID,Remark )" +
                                " VALUES (" +
                                "    @CompanyCD,@GroupUnitNo,@GroupUnitName,@BaseUnitID,@Remark )";
        private const string C_UPDATE =
                                " UPDATE officedba.UnitGroup SET" +
                                "    CompanyCD=@CompanyCD,GroupUnitNo=@GroupUnitNo,GroupUnitName=@GroupUnitName,BaseUnitID=@BaseUnitID" +
                                "    ,Remark=@Remark" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM officedba.UnitGroup WHERE CompanyCD=@CompanyCD AND GroupUnitNo=@GroupUnitNo";

        private const string C_DELETE_ID =
                                " DELETE FROM officedba.UnitGroup WHERE ID=@ID ";

        //字段顺序变量定义
        private const byte m_iDCol = 0; // 主键,自动生成列
        private const byte m_companyCDCol = 1; // 企业代码列
        private const byte m_groupUnitNoCol = 2; // 计量单位组编号列
        private const byte m_groupUnitNameCol = 3; // 计量单位组名列
        private const byte m_baseUnitIDCol = 4; // 基本计量单位ID(对应计量单位表)列
        private const byte m_remarkCol = 5; // 备注列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        /// <param name="groupUnitNo">计量单位组编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(string companyCD, string groupUnitNo)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, groupUnitNo);

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }


        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(UnitGroupModel model)
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
        public static SqlCommand UpdateCommand(UnitGroupModel model)
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
        /// 删除数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(int iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE_ID;
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(int iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        ///<param name="groupUnitNo">计量单位组编号</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string companyCD, string groupUnitNo)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, groupUnitNo);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        ///<param name="groupUnitNo">计量单位组编号</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string companyCD, string groupUnitNo)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, groupUnitNo);


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
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar, 8), // 企业代码
							new SqlParameter("@GroupUnitNo", SqlDbType.VarChar, 50) // 计量单位组编号
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
                            new SqlParameter("@ID", SqlDbType.Int,4), // 主键,自动生成
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8), // 企业代码
                            new SqlParameter("@GroupUnitNo", SqlDbType.VarChar,50), // 计量单位组编号
                            new SqlParameter("@GroupUnitName", SqlDbType.VarChar,200), // 计量单位组名
                            new SqlParameter("@BaseUnitID", SqlDbType.Int,4), // 基本计量单位ID(对应计量单位表)
                            new SqlParameter("@Remark", SqlDbType.VarChar,200)  // 备注
                        };

            return parameters;
        }


        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="companyCD">企业代码的值</param>
        /// <param name="groupUnitNo">计量单位组编号的值</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, string companyCD, string groupUnitNo)
        {
            parameters[0].Value = companyCD; // 企业代码
            parameters[1].Value = groupUnitNo; // 计量单位组编号

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, UnitGroupModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 主键,自动生成
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业代码
            parameters[m_groupUnitNoCol].Value = model.GroupUnitNo; // 计量单位组编号
            parameters[m_groupUnitNameCol].Value = model.GroupUnitName; // 计量单位组名
            if (!model.BaseUnitID.HasValue) parameters[m_baseUnitIDCol].Value = System.DBNull.Value; else parameters[m_baseUnitIDCol].Value = model.BaseUnitID; // 基本计量单位ID(对应计量单位表)
            parameters[m_remarkCol].Value = model.Remark; // 备注

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 计量单位组唯一验证
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="GroupUnitNo">组代码</param>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public static bool CheckUnitNo(string companyCD, string GroupUnitNo, int? id)
        {
            //校验SQL定义
            string checkSql = @" SELECT GroupUnitNo FROM officedba.UnitGroup WHERE CompanyCD ='{0}' AND GroupUnitNo = '{1}' ";
            if (id.HasValue)
            {
                checkSql += " AND id<>" + id.Value.ToString();
            }
            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(string.Format(checkSql, companyCD, GroupUnitNo));
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询界面列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="TotalCount"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="GroupUnitNo"></param>
        /// <param name="GroupUnitName"></param>
        /// <param name="BaseUnitID"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , string CompanyCD, string GroupUnitNo, string GroupUnitName, int? BaseUnitID, string Remark)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT ug.ID,ug.CompanyCD,ug.GroupUnitNo,ug.GroupUnitName,ug.BaseUnitID,ug.Remark,cut.CodeName 
                          ,CASE WHEN (SELECT TOP(1) pi1.GroupUnitNo FROM officedba.ProductInfo pi1
	                          WHERE pi1.CompanyCD=ug.CompanyCD AND pi1.GroupUnitNo=ug.GroupUnitNo 
	                        ) IS NULL THEN 0 ELSE 1 END AS isUse
                          FROM officedba.UnitGroup ug 
                          LEFT JOIN officedba.CodeUnitType cut ON ug.BaseUnitID=cut.ID ");
            sql.Append(" WHERE ug.CompanyCD = '" + CompanyCD + "' ");
            if (!string.IsNullOrEmpty(GroupUnitNo))
            {
                sql.Append(" AND ug.GroupUnitNo= '" + GroupUnitNo + "' ");
            }
            if (!string.IsNullOrEmpty(GroupUnitName))
            {
                sql.Append(" AND ug.GroupUnitName like '%" + GroupUnitName + "%' ");
            }
            if (BaseUnitID.HasValue)
            {
                sql.Append(" AND ug.BaseUnitID=" + BaseUnitID.Value + " ");
            }
            if (!string.IsNullOrEmpty(Remark))
            {
                sql.Append(" AND ug.Remark like '%" + Remark + "%' ");
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion
    }
}
