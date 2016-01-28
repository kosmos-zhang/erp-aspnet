/**********************************************
 * 类作用   计量单位组明细数据处理层
 * 创建人   xz
 * 创建时间 2010-3-11 17:05:00 
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
    /// 计量单位组明细数据处理类
    /// </summary>
    public class UnitGroupDetailDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,CompanyCD,GroupUnitNo,UnitID,ExRate,Remark" +
                                " FROM officedba.UnitGroupDetail";
        private const string C_SELECT_ID =
                                " SELECT ID,CompanyCD,GroupUnitNo,UnitID,ExRate,Remark" +
                                " FROM officedba.UnitGroupDetail" +
                                " WHERE ID=@ID";
        private const string C_SELECT =
                                @" SELECT ugd.ID,ugd.CompanyCD,ugd.GroupUnitNo,ugd.UnitID,ugd.ExRate,ugd.Remark
                                            ,CASE WHEN (SELECT TOP(1) pi1.GroupUnitNo FROM officedba.ProductInfo pi1
                                                          WHERE pi1.GroupUnitNo=ugd.GroupUnitNo AND ( pi1.SaleUnitID=ugd.UnitID OR pi1.InUnitID=ugd.UnitID OR pi1.StockUnitID=ugd.UnitID OR pi1.MakeUnitID=ugd.UnitID)
                                                        ) IS NULL 
                                                        THEN 0
                                                        ELSE 1
                                                        END 
                                                AS isUse" +
                                " FROM officedba.UnitGroupDetail ugd" +
                                " WHERE CompanyCD=@CompanyCD AND GroupUnitNo=@GroupUnitNo ";
        private const string C_INSERT =
                                " INSERT officedba.UnitGroupDetail(" +
                                "    CompanyCD,GroupUnitNo,UnitID,ExRate,Remark )" +
                                " VALUES (" +
                                "    @CompanyCD,@GroupUnitNo,@UnitID,@ExRate,@Remark )";
        private const string C_UPDATE =
                                " UPDATE officedba.UnitGroupDetail SET" +
                                "    CompanyCD=@CompanyCD,GroupUnitNo=@GroupUnitNo,UnitID=@UnitID,ExRate=@ExRate" +
                                "    ,Remark=@Remark" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM officedba.UnitGroupDetail WHERE CompanyCD=@CompanyCD AND GroupUnitNo=@GroupUnitNo";

        private const string C_DELETE_ID =
                                " DELETE FROM officedba.UnitGroupDetail WHERE ID=@ID ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 主键,自动生成列
        private const byte m_companyCDCol = 1; // 企业代码列
        private const byte m_groupUnitNoCol = 2; // 计量单位组编号列
        private const byte m_unitIDCol = 3; // 计量单位ID(对应计量单位表)列
        private const byte m_exRateCol = 4; // 换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)列
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
        public static SqlCommand InsertCommand(UnitGroupDetailModel model)
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
        public static SqlCommand UpdateCommand(UnitGroupDetailModel model)
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
                            new SqlParameter("@UnitID", SqlDbType.Int,4), // 计量单位ID(对应计量单位表)
                            new SqlParameter("@ExRate", SqlDbType.Decimal,9), // 换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)
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
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, UnitGroupDetailModel model)
        {
            parameters[m_iDCol].Value = DBNull.Value; // 主键,自动生成
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业代码
            parameters[m_groupUnitNoCol].Value = model.GroupUnitNo; // 计量单位组编号
            if (!model.UnitID.HasValue) parameters[m_unitIDCol].Value = System.DBNull.Value; else parameters[m_unitIDCol].Value = model.UnitID; // 计量单位ID(对应计量单位表)
            if (!model.ExRate.HasValue) parameters[m_exRateCol].Value = System.DBNull.Value; else parameters[m_exRateCol].Value = model.ExRate; // 换算比率(相对于基本计量单位，也就是一个计量单位=少个基本计量单位，如：一箱为10块，则此处换算率为10)
            parameters[m_remarkCol].Value = model.Remark; // 备注

            return parameters;
        }


        #endregion

        #region 自定义

        #endregion
    }
}