

/**********************************************
 * 类作用   门店库存流水账表数据处理层
 * 创建人   xz
 * 创建时间 2010-4-20 15:09:51 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.Office.SubStoreManager;


namespace XBase.Data.Office.SubStoreManager
{
    /// <summary>
    /// 门店库存流水账表数据处理类
    /// </summary>
    public class SubStorageAccountDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,CompanyCD,DeptID,BillType,ProductID,BatchNo,BillNo,HappenDate,HappenCount,ProductCount" +
                                "    ,Creator,PageUrl,Price,Remark" +
                                " FROM officedba.SubStorageAccount";
        private const string C_SELECT_ID =
                                " SELECT ID,CompanyCD,DeptID,BillType,ProductID,BatchNo,BillNo,HappenDate,HappenCount,ProductCount" +
                                "    ,Creator,PageUrl,Price,Remark" +
                                " FROM officedba.SubStorageAccount" +
                                " WHERE ID=@ID";
        private const string C_SELECT =
                                " SELECT ID,CompanyCD,DeptID,BillType,ProductID,BatchNo,BillNo,HappenDate,HappenCount,ProductCount" +
                                "    ,Creator,PageUrl,Price,Remark" +
                                " FROM officedba.SubStorageAccount" +
                                " WHERE CompanyCD=@CompanyCD  AND BillType=@BillType AND BillNo=@BillNo";
        private const string C_INSERT =
                                " INSERT officedba.SubStorageAccount(" +
                                "    CompanyCD,DeptID,BillType,ProductID,BatchNo,BillNo,HappenDate,HappenCount,ProductCount" +
                                "    ,Creator,PageUrl,Price,Remark )" +
                                " VALUES (" +
                                "    @CompanyCD,@DeptID,@BillType,@ProductID,@BatchNo,@BillNo,@HappenDate,@HappenCount,@ProductCount" +
                                "    ,@Creator,@PageUrl,@Price,@Remark )";
        private const string C_UPDATE =
                                " UPDATE officedba.SubStorageAccount SET" +
                                "    CompanyCD=@CompanyCD,DeptID=@DeptID,BillType=@BillType,ProductID=@ProductID" +
                                "    ,BatchNo=@BatchNo,BillNo=@BillNo,HappenDate=@HappenDate,HappenCount=@HappenCount,ProductCount=@ProductCount" +
                                "    ,Creator=@Creator,PageUrl=@PageUrl,Price=@Price,Remark=@Remark" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM officedba.SubStorageAccount WHERE CompanyCD=@CompanyCD  AND BillType=@BillType AND BillNo=@BillNo";

        private const string C_DELETE_ID =
                                " DELETE FROM officedba.SubStorageAccount WHERE ID IN (@ID) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // ID，自动生成列
        private const byte m_companyCDCol = 1; // 公司编码列
        private const byte m_deptIDCol = 2; // 分店ID列
        private const byte m_billTypeCol = 3; // 单据类型列
        private const byte m_productIDCol = 4; // 物品ID列
        private const byte m_batchNoCol = 5; // 批次列
        private const byte m_billNoCol = 6; // 单据编号列
        private const byte m_happenDateCol = 7; // 出入库时间列
        private const byte m_happenCountCol = 8; // 出入库数量列
        private const byte m_productCountCol = 9; // 现有存量列
        private const byte m_creatorCol = 10; // 业务操作人(取当前登录人的ID)列
        private const byte m_pageUrlCol = 11; // 页面链接地址列
        private const byte m_priceCol = 12; // 单价列
        private const byte m_remarkCol = 13; // 备注列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">ID，自动生成</param>
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
            parameters[0].Value = iD; // ID，自动生成

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="billType">单据类型</param>
        /// <param name="BillNo">单据编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string companyCD, int billType, string BillNo)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, billType, BillNo);

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
            , SubStorageAccountModel model)
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
        public static SqlCommand InsertCommand(SubStorageAccountModel model)
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
        /// 插入操作的执行命令(并且获得现有库存量)
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand GetCountAndInsertCommand(SubStorageAccountModel model)
        {
            DataTable dt = GetSubProductCount(model.CompanyCD, model.ProductID.Value.ToString(), model.DeptID.Value.ToString(), model.BatchNo);
            decimal count = 0m;
            model.ProductCount = model.HappenCount;
            if (dt.Rows.Count > 0 && decimal.TryParse(dt.Rows[0]["ProductCount"].ToString(), out count))
            {
                model.ProductCount = count + (model.ProductCount.HasValue ? model.ProductCount.Value : 0);
            }
            model.HappenCount = Math.Abs(model.HappenCount.HasValue ? model.HappenCount.Value : 0);
            return InsertCommand(model);
        }

        /// <summary>
        /// 获得分店库存
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="ProductID">产品代码</param>
        /// <param name="DeptID">部门代码</param>
        /// <param name="BatchNo">批次</param>
        /// <returns></returns>
        public static DataTable GetSubProductCount(string CompanyCD, string ProductID, string DeptID, string BatchNo)
        {
            SqlParameter[] subPara = { 
                                          new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                          new SqlParameter("@ProductID",SqlDbType.Int),
                                          new SqlParameter("@DeptID",SqlDbType.Int),
                                          new SqlParameter("@BatchNo",SqlDbType.VarChar),};
            subPara[0].Value = CompanyCD;
            subPara[1].Value = ProductID;
            subPara[2].Value = DeptID;
            subPara[3].Value = BatchNo;
            return SqlHelper.ExecuteSql(@"SELECT * FROM officedba.SubStorageProduct 
                                WHERE ProductID=@ProductID AND DeptID=@DeptID AND CompanyCD=@CompanyCD AND ISNULL(BatchNo,'')=@BatchNo ", subPara);

        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(SubStorageAccountModel model)
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
        public static bool Update(SubStorageAccountModel model)
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
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE_ID;
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.VarChar)
                        };
            parameters[0].Value = iD; //ID集合

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.VarChar)
                        };
            parameters[0].Value = iD; //ID集合


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="billType">单据类型</param>
        /// <param name="BillNo">单据编号</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(string companyCD, int billType, string BillNo)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, billType, BillNo);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="billType">单据类型</param>
        /// <param name="BillNo">单据编号</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(string companyCD, int billType, string BillNo)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, companyCD, billType, BillNo);


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
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar, 8), // 公司编码
							new SqlParameter("@BillType", SqlDbType.Int, 4), // 单据类型
                            new SqlParameter("@BillNo", SqlDbType.VarChar,50), // 单据编号
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
                            new SqlParameter("@ID", SqlDbType.Int,4), // ID，自动生成
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8), // 公司编码
                            new SqlParameter("@DeptID", SqlDbType.Int,4), // 分店ID
                            new SqlParameter("@BillType", SqlDbType.Int,4), // 单据类型
                            new SqlParameter("@ProductID", SqlDbType.Int,4), // 物品ID
                            new SqlParameter("@BatchNo", SqlDbType.VarChar,50), // 批次
                            new SqlParameter("@BillNo", SqlDbType.VarChar,50), // 单据编号
                            new SqlParameter("@HappenDate", SqlDbType.DateTime,8), // 出入库时间
                            new SqlParameter("@HappenCount", SqlDbType.Decimal,13), // 出入库数量
                            new SqlParameter("@ProductCount", SqlDbType.Decimal,13), // 现有存量
                            new SqlParameter("@Creator", SqlDbType.Int,4), // 业务操作人(取当前登录人的ID)
                            new SqlParameter("@PageUrl", SqlDbType.VarChar,500), // 页面链接地址
                            new SqlParameter("@Price", SqlDbType.Decimal,9), // 单价
                            new SqlParameter("@Remark", SqlDbType.VarChar,100)  // 备注
                        };

            return parameters;
        }



        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="companyCD">公司编码的值</param>
        /// <param name="billType">单据类型的值</param>
        /// <param name="BillNo">单据编号</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, string companyCD, int billType, string BillNo)
        {
            parameters[0].Value = companyCD; // 公司编码
            parameters[1].Value = billType; // 单据类型
            parameters[2].Value = BillNo; //单据编号
            return parameters;
        }



        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, SubStorageAccountModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // ID，自动生成
            parameters[m_companyCDCol].Value = model.CompanyCD; // 公司编码
            if (!model.DeptID.HasValue) parameters[m_deptIDCol].Value = System.DBNull.Value; else parameters[m_deptIDCol].Value = model.DeptID; // 分店ID
            if (!model.BillType.HasValue) parameters[m_billTypeCol].Value = System.DBNull.Value; else parameters[m_billTypeCol].Value = model.BillType; // 单据类型
            if (!model.ProductID.HasValue) parameters[m_productIDCol].Value = System.DBNull.Value; else parameters[m_productIDCol].Value = model.ProductID; // 物品ID
            parameters[m_batchNoCol].Value = model.BatchNo; // 批次
            parameters[m_billNoCol].Value = model.BillNo; // 单据编号
            if (!model.HappenDate.HasValue) parameters[m_happenDateCol].Value = System.DBNull.Value; else parameters[m_happenDateCol].Value = model.HappenDate; // 出入库时间
            if (!model.HappenCount.HasValue) parameters[m_happenCountCol].Value = System.DBNull.Value; else parameters[m_happenCountCol].Value = model.HappenCount; // 出入库数量
            if (!model.ProductCount.HasValue) parameters[m_productCountCol].Value = System.DBNull.Value; else parameters[m_productCountCol].Value = model.ProductCount; // 现有存量
            if (!model.Creator.HasValue) parameters[m_creatorCol].Value = System.DBNull.Value; else parameters[m_creatorCol].Value = model.Creator; // 业务操作人(取当前登录人的ID)
            parameters[m_pageUrlCol].Value = model.PageUrl; // 页面链接地址
            if (!model.Price.HasValue) parameters[m_priceCol].Value = System.DBNull.Value; else parameters[m_priceCol].Value = model.Price; // 单价
            parameters[m_remarkCol].Value = model.Remark; // 备注

            return parameters;
        }


        #endregion

        #region 自定义

        #endregion
    }
}

