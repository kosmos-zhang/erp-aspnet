

/**********************************************
 * 类作用   商品扩展表数据处理层
 * 创建人   xz
 * 创建时间 2010-3-23 13:53:21 
 ***********************************************/

using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using XBase.Data.DBHelper;

using XBase.Model.CustomAPI.CustomWebSite;


namespace XBase.Data.CustomAPI.CustomWebSite
{
    /// <summary>
    /// 商品扩展表数据处理类
    /// </summary>
    public class WebSiteProductInfoDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,ProductID,Description,Price,ImgDIr,Status,DiscountStatus" +
                                " FROM websitedba.WebSiteProductInfo";
        private const string C_SELECT_ID =
                                " SELECT wspi.ID,wspi.ProductID,wspi.Description,wspi.Price,wspi.ImgDIr,wspi.Status,wspi.DiscountStatus,pi1.ProductName" +
                                " FROM websitedba.WebSiteProductInfo wspi" +
                                " LEFT JOIN officedba.ProductInfo pi1 ON ProductID=pi1.ID" +
                                " WHERE wspi.ID=@ID";
        private const string C_SELECT =
                                " SELECT ID,ProductID,Description,Price,ImgDIr,Status,DiscountStatus" +
                                " FROM websitedba.WebSiteProductInfo" +
                                " WHERE ProductID=@ProductID  ";
        private const string C_INSERT =
                                " INSERT websitedba.WebSiteProductInfo(" +
                                "    ProductID,Description,Price,ImgDIr,Status,DiscountStatus )" +
                                " VALUES (" +
                                "    @ProductID,@Description,@Price,@ImgDIr,@Status,@DiscountStatus )";
        private const string C_UPDATE =
                                " UPDATE websitedba.WebSiteProductInfo SET" +
                                "    ProductID=@ProductID,Description=@Description,Price=@Price,ImgDIr=@ImgDIr" +
                                "    ,Status=@Status,DiscountStatus=@DiscountStatus" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM websitedba.WebSiteProductInfo WHERE ProductID=@ProductID ";

        private const string C_DELETE_ID =
                                " DELETE FROM websitedba.WebSiteProductInfo WHERE ID IN ({0}) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 主键，自动生成列
        private const byte m_productIDCol = 1; // 物品ID,对应表Officedba.ProductInfo列
        private const byte m_descriptionCol = 2; // 商品描述列
        private const byte m_priceCol = 3; // 商品价格列
        private const byte m_imgDIrCol = 4; // 图片路径列
        private const byte m_statusCol = 5; // 状态1 启用0 禁用列
        private const byte m_discountStatusCol = 6; // 折扣状态1 启用0 禁用列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">主键，自动生成</param>
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
            parameters[0].Value = iD; // 主键，自动生成

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="productID">物品ID,对应表Officedba.ProductInfo</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(int productID)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, productID);

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
            , WebSiteProductInfoModel model, string CompanyCD, string productName)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(@"SELECT wspi.ID,wspi.ProductID,wspi.Description,wspi.Price,wspi.ImgDIr,wspi.Status,wspi.DiscountStatus,pi1.ProductName
                                                    FROM websitedba.WebSiteProductInfo wspi
                                                    INNER JOIN officedba.ProductInfo pi1 ON ProductID=pi1.ID AND pi1.CompanyCD='" + CompanyCD + "'");
            sql.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(productName))
            {
                sql.AppendFormat(" AND pi1.ProductName LIKE '%{0}%' ", productName);
            }
            if (model.Price.HasValue)
            {
                sql.AppendFormat(" AND wspi.Price={0} ", model.Price.Value);
            }
            if (!String.IsNullOrEmpty(model.Status))
            {
                sql.AppendFormat(" AND wspi.Status='{0}' ", model.Status);
            }
            if (!String.IsNullOrEmpty(model.DiscountStatus))
            {
                sql.AppendFormat(" AND wspi.DiscountStatus='{0}' ", model.DiscountStatus);
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }

        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(WebSiteProductInfoModel model)
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
        public static SqlCommand UpdateCommand(WebSiteProductInfoModel model)
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
        public static bool Update(WebSiteProductInfoModel model)
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

            comm.CommandText = string.Format(C_DELETE_ID, iD);

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
            StringBuilder sqlSentence = new StringBuilder(string.Format(C_DELETE_ID, iD));


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), new SqlParameter[] { }) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="productID">物品ID,对应表Officedba.ProductInfo</param>

        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(int productID)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, productID);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="productID">物品ID,对应表Officedba.ProductInfo</param>

        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(int productID)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, productID);


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
                            new SqlParameter("@ProductID", SqlDbType.Int, 4) // 物品ID,对应表Officedba.ProductInfo
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
                            new SqlParameter("@ID", SqlDbType.Int,4), // 主键，自动生成
                            new SqlParameter("@ProductID", SqlDbType.Int,4), // 物品ID,对应表Officedba.ProductInfo
                            new SqlParameter("@Description", SqlDbType.VarChar,1000), // 商品描述
                            new SqlParameter("@Price", SqlDbType.Decimal,9), // 商品价格
                            new SqlParameter("@ImgDIr", SqlDbType.VarChar,200), // 图片路径
                            new SqlParameter("@Status", SqlDbType.VarChar,1), // 状态1 启用0 禁用
                            new SqlParameter("@DiscountStatus", SqlDbType.VarChar,1)  // 折扣状态1 启用0 禁用
                        };

            return parameters;
        }



        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="productID">物品ID,对应表Officedba.ProductInfo的值</param>

        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, int productID)
        {
            parameters[0].Value = productID; // 物品ID,对应表Officedba.ProductInfo


            return parameters;
        }



        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, WebSiteProductInfoModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 主键，自动生成
            if (!model.ProductID.HasValue) parameters[m_productIDCol].Value = System.DBNull.Value; else parameters[m_productIDCol].Value = model.ProductID; // 物品ID,对应表Officedba.ProductInfo
            parameters[m_descriptionCol].Value = model.Description; // 商品描述
            if (!model.Price.HasValue) parameters[m_priceCol].Value = System.DBNull.Value; else parameters[m_priceCol].Value = model.Price; // 商品价格
            parameters[m_imgDIrCol].Value = model.ImgDIr; // 图片路径
            parameters[m_statusCol].Value = model.Status; // 状态1 启用0 禁用
            parameters[m_discountStatusCol].Value = model.DiscountStatus; // 折扣状态1 启用0 禁用

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 判断产品是否已经存在
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <param name="ID">商品ID</param>
        /// <returns></returns>
        public static bool ExisitProduct(int productID, int? ID)
        {
            string sqlStr = " SELECT * FROM websitedba.WebSiteProductInfo  WHERE ProductID=" + productID.ToString();
            if (ID.HasValue)
            {
                sqlStr += " AND ID<>" + ID.Value.ToString();
            }
            return SqlHelper.Exists(sqlStr, null);
        }

        /// <summary>
        /// 获得产品图片
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public static string GetProductImage(int productID)
        {
            string sqlStr = "SELECT ImgUrl FROM officedba.productinfo WHERE ID={0}";
            object obj = SqlHelper.ExecuteScalar(string.Format(sqlStr, productID), null);
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }

        #region 读取产品列表
        /// <summary>
        /// 读取产品列表
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetProductList(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM websitedba.WebSiteProductInfo AS a ");
            // sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND Status=@Status");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Status", "1");

            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
        }



        /// <summary>
        /// 带分页的产品列表方法
        /// </summary>
        /// <param name="CompanyCD">公司编码</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="OrderBy">排序字段 例如：ID DESC</param>
        /// <returns></returns>
        public static DataTable GetProductList(string CompanyCD, int PageIndex, int PageSize, ref int TotalCount, string OrderBy, string[] arrParams)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.*,b.ProdNo,b.ProductName,b.Specification,c.CodeName AS UnitName FROM websitedba.WebSiteProductInfo AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS b ON a.ProductID=b.ID ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS c ON c.ID=b.UnitID ");
            sbSql.AppendLine(" WHERE  Status=@Status AND b.CompanyCD=@CompanyCD ");

            SqlParameter[] sqlParams = new SqlParameter[2 + arrParams.Length];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@Status", "1");

            #region 构造查询


            foreach (string item in arrParams)
            {
                switch (item.Split('|')[0])
                {
                    case "ProductName":
                        sbSql.AppendLine(" AND b.ProductName LIKE @ProductName ");
                        sqlParams[index++] = SqlHelper.GetParameter("@ProductName", item.Split('|')[1]);
                        break;
                }
            }

            #endregion

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, sqlParams, ref TotalCount);
        }

        #endregion

        #region 根据指定的物品ID读取数据
        public static DataTable GetProdcutByID(string id)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.ID,a.ProductName,a.ProdNo,a.UnitID,a.SaleUnitID,b.Price,a.GroupUnitNo,c.ExRate ");
            sbSql.AppendLine(" ,d.CodeName AS BaseUnitName, e.CodeName AS SaleUnitName ");
            sbSql.AppendLine(" FROM officedba.ProductInfo AS a ");
            sbSql.AppendLine(" LEFT JOIN websitedba.WebSiteProductInfo AS b ON b.ProductID=a.ID ");
            sbSql.AppendLine(" LEFT JOIN officedba.UnitGroupDetail AS c ON c.GroupUnitNo=a.GroupUnitNo AND c.UnitID=a.SaleUnitID ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS d ON d.ID=a.UnitID ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS e ON e.ID=a.SaleUnitID ");
            sbSql.AppendLine(" WHERE a.ID IN (" + id + ") ");

            return SqlHelper.ExecuteSql(sbSql.ToString());
        }
        #endregion
        #endregion
    }
}
