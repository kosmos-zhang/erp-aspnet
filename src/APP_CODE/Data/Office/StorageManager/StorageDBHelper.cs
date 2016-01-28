/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/12
 ***********************************************/


using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Model.Office.SupplyChain;

namespace XBase.Data.Office.StorageManager
{
    public class StorageDBHelper
    {
        /// <summary>
        /// 测试分页排序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetLitBycondition(StorageModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.StorageNo,ISNULL(a.StorageName,'') as StorageName  ,isnull(b.EmployeeName,'') as CanViewUserName,a.StorageType,a.UsedStatus,ISNULL(a.Remark,'') as Remark ");
            sql.AppendLine("from officedba.StorageInfo a");
            sql.AppendLine("left outer join officedba.EmployeeInfo b on a.StorageAdmin=b.ID");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StorageNo))
            {
                sql.AppendLine("	and a.StorageNo like '%'+ @StorageNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo", model.StorageNo));
            }
            if (!string.IsNullOrEmpty(model.StorageName))
            {
                sql.AppendLine(" and a.StorageName like '%'+ @StorageName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName", model.StorageName));
            }

            if (!string.IsNullOrEmpty(model.StorageType))
            {
                sql.AppendLine(" and a.StorageType = @StorageType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType", model.StorageType));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine(" and a.UsedStatus = @UsedStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }




        #region 查询：仓库
        /// <summary>
        /// 查询仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageTableBycondition(StorageModel model, string orderby)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.StorageNo,a.StorageName,a.StorageType,a.UsedStatus,ISNULL(a.Remark,'') as Remark,isnull(b.EmployeeName,'') as CanViewUserName ");
            sql.AppendLine("from officedba.StorageInfo a");
            sql.AppendLine("left outer join officedba.EmployeeInfo b on a.StorageAdmin=b.ID");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "'");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StorageNo))
            {
                sql.AppendLine("	and a.StorageNo like '%'+ @StorageNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo", model.StorageNo));
            }
            if (!string.IsNullOrEmpty(model.StorageName))
            {
                sql.AppendLine(" and a.StorageName like '%'+ @StorageName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName", model.StorageName));
            }

            if (!string.IsNullOrEmpty(model.StorageType))
            {
                sql.AppendLine(" and a.StorageType = @StorageType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType", model.StorageType));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine(" and a.UsedStatus = @UsedStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        public static DataTable GetStorageTableBycondition(StorageModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,StorageNo,StorageName,StorageType,UsedStatus,ISNULL(Remark,'') as Remark ");
            sql.AppendLine("from officedba.StorageInfo");
            sql.AppendLine("   where CompanyCD='" + model.CompanyCD + "'");
            SqlCommand comm = new SqlCommand();

            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StorageNo))
            {
                sql.AppendLine("	and StorageNo like '%'+ @StorageNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo", model.StorageNo));
            }
            if (!string.IsNullOrEmpty(model.StorageName))
            {
                sql.AppendLine(" and StorageName like '%'+ @StorageName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName", model.StorageName));
            }

            if (!string.IsNullOrEmpty(model.StorageType))
            {
                sql.AppendLine(" and StorageType = @StorageType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType", model.StorageType));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine(" and UsedStatus = @UsedStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        /// 重载绑定库存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetStorageTableByRed(StorageModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,StorageNo,StorageName,StorageType,UsedStatus,ISNULL(Remark,'') as Remark ");
            sql.AppendLine("from officedba.StorageInfo");
            sql.AppendLine("   where CompanyCD='" + model.CompanyCD + "'");
            SqlCommand comm = new SqlCommand();
            //过滤单据：显示当前用户拥有权限查看的单据
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StorageNo))
            {
                sql.AppendLine("	and StorageNo like '%'+ @StorageNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo", model.StorageNo));
            }
            if (!string.IsNullOrEmpty(model.StorageName))
            {
                sql.AppendLine(" and StorageName like '%'+ @StorageName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName", model.StorageName));
            }

            if (!string.IsNullOrEmpty(model.StorageType))
            {
                sql.AppendLine(" and StorageType = @StorageType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType", model.StorageType));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine(" and UsedStatus = @UsedStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        #region 查看：仓库信息
        /// <summary>
        /// 获取仓库详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorage(string CompanyCD, int ID)
        {
            string sql = "select a.ID,a.StorageNo,a.StorageName,a.StorageType,a.UsedStatus,a.Remark,a.StorageAdmin,a.CanViewUser,b.EmployeeName as  StorageAdminName from officedba.StorageInfo a left outer join officedba.EmployeeInfo b on a.CompanyCD=b.CompanyCD and a.StorageAdmin=b.id   where a.CompanyCD='" + CompanyCD + "' and a.ID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion


        public static DataTable GetEmployeeNameByID(string idList)
        {
            if (idList + "" == "")
            {
                idList = "-1";
            }

            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT ID,EmployeeName       ");
            searchSql.AppendLine("  FROM officedba.EmployeeInfo ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("	ID in (" + idList + ") ");
            #endregion

            return SqlHelper.ExecuteSql(searchSql.ToString());
        }

        #region 添加：仓库信息
        /// <summary>
        /// 添加仓库记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorage(StorageModel model, out int IndexIDentity)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.StorageInfo");
            sql.AppendLine("	    (CompanyCD      ");
            sql.AppendLine("		,StorageNo      ");
            sql.AppendLine("		,StorageName    ");
            sql.AppendLine("		,StorageType    ");
            sql.AppendLine("		,CanViewUser         ");
            sql.AppendLine("		,StorageAdmin    ");
            sql.AppendLine("		,Remark         ");
            sql.AppendLine("		,UsedStatus)     ");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@StorageNo     ");
            sql.AppendLine("		,@StorageName   ");
            sql.AppendLine("		,@StorageType   ");
            sql.AppendLine("		,@CanViewUser    ");
            sql.AppendLine("		,@StorageAdmin         ");

            sql.AppendLine("		,@Remark        ");
            sql.AppendLine("		,@UsedStatus)    ");
            sql.AppendLine("set @IndexID = @@IDENTITY");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD.Trim()));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo ", model.StorageNo.Trim()));//编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName ", model.StorageName.Trim()));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType ", model.StorageType.Trim()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark.Trim()));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus ", model.UsedStatus.Trim()));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", model.CanViewUser.Trim()));// 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageAdmin ", model.StorageAdmin.Trim()));//
            bool result = SqlHelper.ExecuteTransWithCommand(comm);
            if (result)
            {
                IndexIDentity = int.Parse(comm.Parameters["@IndexID"].Value.ToString());
            }
            else
            {
                IndexIDentity = 0;
            }
            return result;

        }
        #endregion

        #region 修改：仓库信息
        /// <summary>
        /// 更新仓库信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateStorage(StorageModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInfo SET");
            sql.AppendLine(" StorageName      = @StorageName,");
            sql.AppendLine(" StorageType      = @StorageType,");
            sql.AppendLine(" Remark                = @Remark,");
            sql.AppendLine(" UsedStatus        = @UsedStatus,");

            sql.AppendLine(" CanViewUser                = @CanViewUser,");
            sql.AppendLine(" StorageAdmin        = @StorageAdmin   ");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");



            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@ID", model.ID);
            param[1] = SqlHelper.GetParameter("@StorageName", model.StorageName);
            param[2] = SqlHelper.GetParameter("@StorageType", model.StorageType);
            param[3] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[6] = SqlHelper.GetParameter("@CanViewUser", model.CanViewUser.Trim());
            param[7] = SqlHelper.GetParameter("@StorageAdmin", model.StorageAdmin.Trim());
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorage(string ID, string CompanyCD)
        {

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Officedba.StorageInfo ");
            sql.AppendLine("WHERE ");
            sql.AppendLine("CompanyCD = '" + CompanyCD + "'");
            sql.AppendLine("AND ID IN (" + ID + ")");

            SqlHelper.ExecuteTransSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion


        #region 是否可以删除
        public static bool IsDeleteStorage(string ID, string CompanyCD)
        {
            bool result = true;
            string strSql1 = "select count(*) from officedba.ProductInfo where StorageID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            string strSql2 = "select count(*) from officedba.StorageInitail where StorageID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            string strSql3 = "select count(*) from officedba.StorageProduct where StorageID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            int a = int.Parse(SqlHelper.ExecuteScalar(strSql1, null).ToString());
            int b = int.Parse(SqlHelper.ExecuteScalar(strSql2, null).ToString());
            int c = int.Parse(SqlHelper.ExecuteScalar(strSql3, null).ToString());

            if (a != 0 || b != 0 || c != 0)
            {
                result = false;
            }
            return result;
        }
        #endregion


        /// <summary>
        /// 获取企业导入的库存Excel表的数据
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="companycd">企业编号</param>
        /// <param name="fname">excel文件名</param>
        /// <param name="tbname">excel表名</param>
        /// <returns></returns>
        public static DataSet GetStorageInfoFromExcel(string companycd, string fname, string tbname)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@fname",fname),
                new SqlParameter("@tbname",tbname)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelSelect_storage]", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 根据商品名称，企业编号查找商品是否存在
        /// zxb 2009-08-19
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeProductInfo(string codename, string compid, string prodNo)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50),
                                          new SqlParameter("@ProdNo",SqlDbType.VarChar,100)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            parameters[2].Value = prodNo;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.ProductInfo where ProdNo=@ProdNo AND ProductName=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 将excel中的分量库存数据导入到分量库存表中
        /// zxb 2009-08-19
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="fname">excel文件名</param>
        /// <param name="tbname">excel表名</param>
        /// <param name="headflag">总店标志，0表示总店，1表示分店</param>
        /// <returns></returns>
        public static int GetExcelToStorageInfo(string companycd, string usercode, string isbatchno, int creator)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@createPerson",usercode),
                 new SqlParameter("@isbatchno",isbatchno),
                 new SqlParameter("@creator",creator)
            };
            //return SqlHelper.ExecuteTransStoredProcedure("[officedba].[excelIntoSql_storage]", param);

            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelIntoSql_storage]", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds.Tables[0].Rows.Count;

        }

        /// <summary>
        /// 取Excel数据
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
            string sql = "SELECT distinct * FROM [Sheet1$]";
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
            da.Fill(ds);
            //删除历史记录
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            sql = "delete from officedba.StorageProduct_temp where [企业编号]=@companycd";
            SqlHelper.ExecuteTransSql(sql, paramter);
            //传到临时表中
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlParameter[] param = 
                {
                    new SqlParameter("@companycd",companycd),
                    new SqlParameter("@id",ds.Tables[0].Rows[i][0].ToString()),
                    new SqlParameter("@storagename",ds.Tables[0].Rows[i][1].ToString()),
                    new SqlParameter("@BatchNo",ds.Tables[0].Rows[i][2].ToString()),
                      new SqlParameter("@ProductNo",ds.Tables[0].Rows[i][3].ToString()),
                    new SqlParameter("@productName",ds.Tables[0].Rows[i][4].ToString()),
                    new SqlParameter("@storageNum",ds.Tables[0].Rows[i][5].ToString()),
                    new SqlParameter("@Price",ds.Tables[0].Rows[i][6].ToString())       
                };

                string lenstr = string.Empty;
                for (int j = 0; j < 4; j++)
                {
                    if (ds.Tables[0].Rows[i][j].ToString().Trim().Length < 1)
                    {
                        lenstr += "#";
                    }
                }
                if (lenstr.Length == 4)
                {
                    continue;
                }
                sql = "insert into officedba.StorageProduct_temp values(@id,@companycd,@storagename,@BatchNo,@ProductName,@storageNum,@ProductNo,@Price)";
                SqlHelper.ExecuteTransSql(sql, param);
            }
            sql = "select * from officedba.StorageProduct_temp where [企业编号]=@companycd order by [流水号]";
            ds = new DataSet();
            SqlParameter[] paramter1 = { new SqlParameter("@companycd", companycd) };
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter1);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 绑定仓库
        /// zxb
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static void BindStorateInfo(System.Web.UI.WebControls.DropDownList ddl, string companycd)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd)
            };

            DataTable dt = SqlHelper.ExecuteSql("select * from officedba.StorageInfo where CompanyCD=@companyCD and UsedStatus=1", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ddl.DataTextField = "StorageName";
            ddl.DataValueField = "id";
            ddl.DataSource = ds;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择仓库", "0"));
        }

        /// <summary>
        /// 库存出库走势表
        /// zxb
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public static DataTable GetStorageSetUp(string companycd, int storageID, int ProductID, string begindate, string enddate, int timeType,int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType),
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageSetup]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 出库走势明细
        /// zxb
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageSetUpDetails(string companycd, int storageID, int ProductID, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType, int ByTimeType, string timestr, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageSetupDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 出库对比
        /// zxb 2009-11-24
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetStorageCompare(string companycd, int storageID, string begindate, string enddate,int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType),
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageOutCompare]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 出库明细
        /// zxb 2009-11-25
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageCompareDetails(string companycd, int storageID, int ProductID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                //new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType),
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageOutCompareDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 入库走势
        /// zxb 2009-11-26
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public static DataTable GetStorageInSetUp(string companycd, int storageID, int ProductID, string begindate, string enddate, int timeType, int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageInSetup]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 入库明细
        /// zxb 2009-11-26
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInSetUpDetails(string companycd, int storageID, int ProductID, string begindate, string enddate, string order, int pageindex, int pagesize, int timeType,int ByTimeType, string timestr, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageInSetupDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 入库分析
        /// zxb 2009-11-26
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetStorageInCompare(string companycd, int storageID, string begindate, string enddate,int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageInCompare]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 入库明细
        /// zxb 2009-11-26
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInCompareDetails(string companycd, int storageID, int ProductID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                //new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageInCompareDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品库存分析
        /// zxb 2009-11-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static DataTable GetStorageNowCompare(string companycd, int ProductID)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageNowCompare]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// 物品库存明细
        /// zxb 2009-11-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageNowCompareDetails(string companycd, int storageID, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageNowCompareDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品库存分析
        /// zxb 2009-11-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static DataTable GetProductStorage(string companycd, int StorageID)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@StorageID",StorageID),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetProductStorage]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品库存明细
        /// zxb 2009-11-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="productID"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetProductStorageDetails(string companycd, int productID, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@productID",productID),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetProductStorageDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品报损走势
        /// zxb 2009-11-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public static DataTable GetStorageLossSetUp(string companycd, int storageID, int ProductID, string begindate, string enddate, int timeType,int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageLossSetup]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 库存保损走势分布
        /// zxb 2009-11-30
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageLossSetUpDetails(string companycd, int storageID, int ProductID, string begindate, string enddate, int pageindex, int pagesize, int timeType,int ByTimeType, string timestr, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageLossSetupDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品报损分析
        /// zxb 2009-11-30
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetStorageLossAnalysis(string companycd, int storageID, string begindate, string endDate, int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",endDate),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageLossAnalysis]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品报损明细分析
        /// zxb 2009-11-30
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageLossAnalysisDetails(string companycd, int productID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@productID",productID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageLossAnalysisDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 库存报损分析
        /// zxb 2009-11-30
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ProductID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetStorageProductLossAnalysis(string companycd, int ProductID, string begindate, string enddate,int ByTimeType)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@ProductID",ProductID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageProductLossAnalysis]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 库存报损分析明细
        /// zxb 2009-11-30
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="storageID"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageProductLossDetails(string companycd, int storageID, string begindate, string enddate,int ByTimeType, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@companyCD",companycd),
                new SqlParameter("@storageID",storageID),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@PointLength",((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint),
                new SqlParameter("@ByTimeType",ByTimeType)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetStorageProductLossAnalysisDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        #region 获取指定人员可查看仓库的ID串
        /// <summary>
        /// 获取指定人员可查看仓库的ID串(以逗号隔开形式)
        /// add by hexw 2010-3-10
        /// </summary>
        /// <param name="empid">当前登录人ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>仓库id串</returns>
        public static string GetStorageIDStr(int empid, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select ID ");
            strSql.AppendLine(" from officedba.StorageInfo ");
            strSql.AppendLine("   where CompanyCD=@CompanyCD ");
            //过滤单据：显示当前用户拥有权限查看的单据
            strSql.AppendLine(" and ( charindex('," + empid + ",' , ','+CanViewUser+',')>0 or StorageAdmin=" + empid + " OR CanViewUser='' OR CanViewUser is null) ");

            strSql.AppendLine(" and UsedStatus = 1 ");

            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), param);
            string idStr = "";
            for (int iCount = 0; iCount < dt.Rows.Count; iCount++)
            {
                if (iCount == 0)
                {
                    idStr = dt.Rows[iCount][0].ToString();
                }
                else
                {
                    idStr += "," + dt.Rows[iCount][0].ToString();
                }
            }
            return idStr;
        }
        #endregion

        #region 根据查询条件获取仓库列表(库存查询中用到，可查看人员过滤仓库)
        /// <summary>
        /// 根据查询条件获取仓库列表(库存查询中用到，可查看人员过滤仓库)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetStorageTableBycondition2(StorageModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,StorageNo,StorageName,StorageType,UsedStatus,ISNULL(Remark,'') as Remark ");
            sql.AppendLine("from officedba.StorageInfo");
            sql.AppendLine("   where CompanyCD='" + model.CompanyCD + "'");
            SqlCommand comm = new SqlCommand();
            //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            sql.AppendLine(" and ( charindex('," + empid + ",' , ','+CanViewUser+',')>0 or StorageAdmin=" + empid + " OR CanViewUser='' OR CanViewUser is null) ");

            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.StorageNo))
            {
                sql.AppendLine("	and StorageNo like '%'+ @StorageNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageNo", model.StorageNo));
            }
            if (!string.IsNullOrEmpty(model.StorageName))
            {
                sql.AppendLine(" and StorageName like '%'+ @StorageName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageName", model.StorageName));
            }

            if (!string.IsNullOrEmpty(model.StorageType))
            {
                sql.AppendLine(" and StorageType = @StorageType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageType", model.StorageType));
            }
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                sql.AppendLine(" and UsedStatus = @UsedStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 进销存汇总表（单品）
        /// <summary>
        /// 进销存汇总表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInAndOutTotalInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string point = userInfo.SelPoint;
            string startyearmonth = "";
            string endyearmonth = "";
            string startmonth = Convert.ToDateTime(DailyDate).AddDays(-1).Month.ToString();
            string endmonth = Convert.ToDateTime(EndDate).Month.ToString();
            if (Convert.ToInt32(startmonth) < 10) startmonth = "0" + startmonth;
            if (Convert.ToInt32(endmonth) < 10) endmonth = "0" + endmonth;
            startyearmonth = Convert.ToDateTime(DailyDate).AddDays(-1).Year + startmonth;
            endyearmonth = Convert.ToDateTime(EndDate).AddDays(-1).Year + endmonth;

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT A.*,Convert(numeric(22," + point + "),ISNULL(A.YestodayCount,0)*ISNULL(B.PeriodBeginCost,0))PeriodBeginCost,Convert(char(20),Convert(numeric(22," + point + "),ISNULL(A.YestodayCount,0)*ISNULL(B.PeriodBeginCost,0)))+'&nbsp;' PeriodBeginCost1,Convert(numeric(22," + point + "),ISNULL(A.TodayCount,0)*ISNULL(C.PeriodEndCost,0))PeriodEndCost,Convert(char(20),Convert(numeric(22," + point + "),ISNULL(A.TodayCount,0)*ISNULL(C.PeriodEndCost,0)))+'&nbsp;' PeriodEndCost1  ");
                            searchSql.AppendLine(" FROM ");
                            searchSql.AppendLine(" (SELECT	a.CompanyCD, ");
                            searchSql.AppendLine(" b.ProductName,a.ProductID,b.Specification,a.BatchNo ");
                            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                            {
                                if (int.Parse(EFIndex) > 0)
                                {
                                    searchSql.AppendLine(" ,b.ExtField" + EFIndex + " ");
                                }
                            }
                            searchSql.AppendLine(" ,SUM(Convert(numeric(22," + point + "),a.PhurInCount)) as PhurInCount, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.PhurInCount)))+'&nbsp;' as PhurInCount1, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.MakeInCount)) as MakeInCount, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.MakeInCount)))+'&nbsp;' as MakeInCount1, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.DispInCount)) as DispInCount, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.DispInCount)))+'&nbsp;' as DispInCount1, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.OtherInCount)) as OtherInCount, ");
                            searchSql.AppendLine("  Convert(char(20),SUM(Convert(numeric(22," + point + "),a.OtherInCount)))+'&nbsp;' as OtherInCount1, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SendInCount)) as SendInCount, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SendInCount)))+'&nbsp;' as SendInCount1, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SubSaleBackInCount)) as SubSaleBackInCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.TakeInCount)) as TakeInCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.InTotal)) as InTotal, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SaleFee)) as SaleFee, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.PhurBackFee)) as PhurBackFee, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.InitInCount)) as InitInCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.InitBatchCount)) as InitBatchCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SaleBackInCount)) as SaleBackInCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.RedInCount)) as RedInCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.BackInCount)) as BackInCount,		 ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SaleOutCount)) as SaleOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.TakeOutCount)) as TakeOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.DispOutCount)) as DispOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.BadCount)) as BadCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.OtherOutCount)) as OtherOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SendOutCount)) as SendOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SubSaleOutCount)) as SubSaleOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.OutTotal)) as OutTotal, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.PhurFee)) as PhurFee, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.SaleBackFee)) as SaleBackFee, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.PhurBackOutCount)) as PhurBackOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.RedOutCount)) as RedOutCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.LendCount)) as LendCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.CheckCount)) as CheckCount, ");
		                    searchSql.AppendLine(" SUM(Convert(numeric(22," + point + "),a.AdjustCount)) as AdjustCount, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SubSaleBackInCount)))+'&nbsp;' as SubSaleBackInCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.TakeInCount)))+'&nbsp;' as TakeInCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.InTotal)))+'&nbsp;' as InTotal1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SaleFee)))+'&nbsp;' as SaleFee1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.PhurBackFee)))+'&nbsp;' as PhurBackFee1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.InitInCount)))+'&nbsp;' as InitInCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.InitBatchCount)))+'&nbsp;' as InitBatchCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SaleBackInCount)))+'&nbsp;' as SaleBackInCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.RedInCount)))+'&nbsp;' as RedInCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.BackInCount)))+'&nbsp;' as BackInCount1,		 ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SaleOutCount)))+'&nbsp;' as SaleOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.TakeOutCount)))+'&nbsp;' as TakeOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.DispOutCount)))+'&nbsp;' as DispOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.BadCount)))+'&nbsp;' as BadCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.OtherOutCount)))+'&nbsp;' as OtherOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SendOutCount)))+'&nbsp;' as SendOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SubSaleOutCount)))+'&nbsp;' as SubSaleOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.OutTotal)))+'&nbsp;' as OutTotal1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.PhurFee)))+'&nbsp;' as PhurFee1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.SaleBackFee)))+'&nbsp;' as SaleBackFee1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.PhurBackOutCount)))+'&nbsp;' as PhurBackOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.RedOutCount)))+'&nbsp;' as RedOutCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.LendCount)))+'&nbsp;' as LendCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.CheckCount)))+'&nbsp;' as CheckCount1, ");
                            searchSql.AppendLine(" Convert(char(20),SUM(Convert(numeric(22," + point + "),a.AdjustCount)))+'&nbsp;' as AdjustCount1, ");



                            searchSql.AppendLine(" Convert(numeric(22," + point + "),ISNULL((select top 1 b.TodayCount  from officedba.StorageDaily b where ");
			                searchSql.AppendLine(" a.ProductID=b.ProductID ");
			                searchSql.AppendLine(" and b.DailyDate=CONVERT(CHAR(10), dateadd(dd,-1,'"+DailyDate+"'),120)),0)) as YestodayCount, ");

                            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),ISNULL((select top 1 b.TodayCount  from officedba.StorageDaily b where ");
                            searchSql.AppendLine(" a.ProductID=b.ProductID ");
                            searchSql.AppendLine(" and b.DailyDate=CONVERT(CHAR(10), dateadd(dd,-1,'" + DailyDate + "'),120)),0))) as YestodayCount1, ");

                            searchSql.AppendLine(" Convert(numeric(22," + point + "),ISNULL((select top 1 b.TodayCount  from officedba.StorageDaily b where ");
			                searchSql.AppendLine(" a.ProductID=b.ProductID ");
			                searchSql.AppendLine(" and b.DailyDate=CONVERT(CHAR(10), '"+EndDate+"',120)),0)) as TodayCount, ");
                            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),ISNULL((select top 1 b.TodayCount  from officedba.StorageDaily b where ");
                            searchSql.AppendLine(" a.ProductID=b.ProductID ");
                            searchSql.AppendLine(" and b.DailyDate=CONVERT(CHAR(10), '" + EndDate + "',120)),0)))+'&nbsp;' as TodayCount1 ");

                            searchSql.AppendLine(" FROM	officedba.StorageDaily a  ");
                            searchSql.AppendLine(" left join officedba.ProductInfo b on a.ProductID=b.ID  and a.CompanyCD=b.CompanyCD   ");
                            searchSql.AppendLine(" where a.CompanyCD=@CompanyCD  ");
                            searchSql.AppendLine(" and DailyDate>='"+DailyDate+"' ");
                            searchSql.AppendLine(" and a.DailyDate<='"+EndDate+"' ");
                            

                            //定义查询的命令
                            SqlCommand comm = new SqlCommand();
                            //添加公司代码参数
                            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
                            //开始日期
                            if (!string.IsNullOrEmpty(DailyDate))
                            {
                                searchSql.AppendLine(" and DailyDate>=@DailyDate");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
                            }
                            //结束日期
                            if (!string.IsNullOrEmpty(EndDate))
                            {
                                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
                            }
                            //物品编号
                            if (!string.IsNullOrEmpty(model.ProdNo))
                            {
                                searchSql.AppendLine(" and b.ProdNo=@ProdNo");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));
                            }
                            //物品名称
                            if (!string.IsNullOrEmpty(model.ProductName))
                            {
                                searchSql.AppendLine("	and b.ProductName like  '%'+ @ProductName + '%' ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
                            }
                            //物品规格
                            if (!string.IsNullOrEmpty(model.Specification))
                            {
                                searchSql.AppendLine("	and b.Specification like  '%'+ @Specification + '%' ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));
                            }
                            //颜色
                            if (!string.IsNullOrEmpty(model.ColorID))
                            {
                                if (int.Parse(model.ColorID) > 0)
                                {
                                    searchSql.AppendLine("	and b.ColorID=@ColorID ");
                                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
                                }
                            }
                            //产地
                            if (!string.IsNullOrEmpty(model.FromAddr))
                            {
                                searchSql.AppendLine("	and b.FromAddr like  '%'+ @FromAddr + '%' ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", model.FromAddr));
                            }
                            //厂家
                            if (!string.IsNullOrEmpty(model.Manufacturer))
                            {
                                searchSql.AppendLine("	and b.Manufacturer like  '%'+ @Manufacturer + '%' ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", model.Manufacturer));
                            }
                            //尺寸
                            if (!string.IsNullOrEmpty(model.Size))
                            {
                                searchSql.AppendLine("	and b.Size =@Size ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
                            }
                            //材质
                            if (!string.IsNullOrEmpty(model.Material))
                            {
                                if (int.Parse(model.Material) > 0)
                                {
                                    searchSql.AppendLine("	and b.Material=@Material ");
                                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
                                }
                            }
                            //条码
                            if (!string.IsNullOrEmpty(model.BarCode))
                            {
                                searchSql.AppendLine("	and b.BarCode=@BarCode ");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
                            }
                            //仓库
                            if (!string.IsNullOrEmpty(model.StorageID))
                            {
                                if (int.Parse(model.StorageID) > 0)
                                {
                                    searchSql.AppendLine(" and a.StorageID=@StorageID");
                                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
                                }
                            }
                            //批次
                            if (!string.IsNullOrEmpty(BatchNo))
                            {
                                searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                            }
                            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                            {
                                if (int.Parse(EFIndex) > 0)
                                {
                                    searchSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                                }
                            }
                            searchSql.AppendLine(" group by a.CompanyCD,a.ProductID,a.BatchNo,b.ProductName,b.Specification ");
                            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                            {
                                if (int.Parse(EFIndex) > 0)
                                {
                                    searchSql.AppendLine(" ,b.ExtField" + EFIndex + " ");
                                }
                            }
                            searchSql.AppendLine(" )A ");
                            searchSql.AppendLine(" LEFT OUTER JOIN  ");
                            searchSql.AppendLine(" (select * from officedba.StorageCost a ");
                            searchSql.AppendLine(" where a.YearMonth='" + startyearmonth + "') B ");
                            searchSql.AppendLine(" ON A.ProductID=B.ProductID and A.CompanyCD=B.CompanyCD ");
                            searchSql.AppendLine(" LEFT OUTER JOIN  ");
                            searchSql.AppendLine(" (select * from officedba.StorageCost a ");
                            searchSql.AppendLine(" where a.YearMonth='" + endyearmonth + "') C ");
                            searchSql.AppendLine(" ON A.ProductID=C.ProductID and A.CompanyCD=C.CompanyCD ");

            #endregion

            

            
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            dt = GetAllTotal(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, true);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
        #region 进销存汇总表（全部）
        /// <summary>
        /// 进销存汇总表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllStorageInAndOutInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	CONVERT(varchar(10),a.DailyDate,120) as DailyDate,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.PhurInCount)) as PhurInCount,SUM(Convert(numeric(22,2),a.MakeInCount)) as MakeInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.DispInCount)) as DispInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OtherInCount)) as OtherInCount,SUM(Convert(numeric(22,2),a.SendInCount)) as SendInCount,SUM(Convert(numeric(22,2),a.SubSaleBackInCount)) as SubSaleBackInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.TakeInCount)) as TakeInCount,SUM(Convert(numeric(22,2),a.InTotal)) as InTotal,SUM(Convert(numeric(22,2),a.SaleFee)) as SaleFee,SUM(Convert(numeric(22,2),a.PhurBackFee)) as PhurBackFee,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.InitInCount)) as InitInCount,SUM(Convert(numeric(22,2),a.InitBatchCount)) as InitBatchCount,SUM(Convert(numeric(22,2),a.SaleBackInCount)) as SaleBackInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.RedInCount)) as RedInCount,SUM(Convert(numeric(22,2),a.BackInCount)) as BackInCount,");
            searchSql.AppendLine("		");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.SaleOutCount)) as SaleOutCount,SUM(Convert(numeric(22,2),a.TakeOutCount)) as TakeOutCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.DispOutCount)) as DispOutCount,SUM(Convert(numeric(22,2),a.BadCount)) as BadCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OtherOutCount)) as OtherOutCount,SUM(Convert(numeric(22,2),a.SendOutCount)) as SendOutCount,SUM(Convert(numeric(22,2),a.SubSaleOutCount)) as SubSaleOutCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OutTotal)) as OutTotal,SUM(Convert(numeric(22,2),a.PhurFee)) as PhurFee,SUM(Convert(numeric(22,2),a.SaleBackFee)) as SaleBackFee,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.PhurBackOutCount)) as PhurBackOutCount,SUM(Convert(numeric(22,2),a.RedOutCount)) as RedOutCount,SUM(Convert(numeric(22,2),a.LendCount)) as LendCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),ABS(a.CheckCount))) as CheckCount,SUM(Convert(numeric(22,2),ABS(a.AdjustCount))) as AdjustCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),ABS(a.TodayCount))) as TodayCount ");
            searchSql.AppendLine("FROM	officedba.StorageDaily a ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //开始日期
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and a.DailyDate>=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //结束日期
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            searchSql.AppendLine(" GROUP BY CONVERT(varchar(10),a.DailyDate,120)");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            dt = GetAllTotal(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, false);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #region 进销存汇总 总计
        /// <summary>
        /// 进销存汇总 总计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetAllTotal(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, bool flag)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string point = userInfo.SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	Convert(numeric(22," + point + "),sum(InTotal)) as InTotalCount,Convert(char(20),Convert(numeric(22," + point + "),sum(InTotal)))+'&nbsp;' as InTotalCount1,Convert(numeric(22," + point + "),sum(OutTotal)) as outTotalCount,Convert(char(20),Convert(numeric(22," + point + "),sum(OutTotal)))+'&nbsp;' as outTotalCount1,Convert(numeric(22," + point + "),sum(SaleFee)) as SaleFeeCount,Convert(char(20),Convert(numeric(22," + point + "),sum(SaleFee)))+'&nbsp;' as SaleFeeCount1,");
            searchSql.AppendLine("		Convert(numeric(22," + point + "),sum(PhurFee)) as PhurFeeCount,Convert(char(20),Convert(numeric(22," + point + "),sum(PhurFee)))+'&nbsp;' as PhurFeeCount1,Convert(numeric(22," + point + "),sum(PhurBackFee)) as PhurBackFeeCount,Convert(char(20),Convert(numeric(22," + point + "),sum(PhurBackFee)))+'&nbsp;' as PhurBackFeeCount1,Convert(numeric(22," + point + "),sum(SaleBackFee)) as SaleBackFeeCount,Convert(char(20),Convert(numeric(22," + point + "),sum(SaleBackFee)))+'&nbsp;' as SaleBackFeeCount1 ");
            searchSql.AppendLine("FROM	officedba.StorageDaily a left join officedba.ProductInfo b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //开始日期
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and a.DailyDate>=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //结束日期
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            if (flag)
            {
                //物品编号
                if (!string.IsNullOrEmpty(model.ProdNo))
                {
                    searchSql.AppendLine(" and b.ProdNo=@ProdNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));
                }
                //物品名称
                if (!string.IsNullOrEmpty(model.ProductName))
                {
                    searchSql.AppendLine("	and b.ProductName like  '%'+ @ProductName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
                }
                //物品规格
                if (!string.IsNullOrEmpty(model.Specification))
                {
                    searchSql.AppendLine("	and b.Specification like  '%'+ @Specification + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));
                }
                //颜色
                if (!string.IsNullOrEmpty(model.ColorID))
                {
                    if (int.Parse(model.ColorID) > 0)
                    {
                        searchSql.AppendLine("	and b.ColorID=@ColorID ");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
                    }
                }
                //产地
                if (!string.IsNullOrEmpty(model.FromAddr))
                {
                    searchSql.AppendLine("	and b.FromAddr like  '%'+ @FromAddr + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", model.FromAddr));
                }
                //厂家
                if (!string.IsNullOrEmpty(model.Manufacturer))
                {
                    searchSql.AppendLine("	and b.Manufacturer like  '%'+ @Manufacturer + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", model.Manufacturer));
                }
                //尺寸
                if (!string.IsNullOrEmpty(model.Size))
                {
                    searchSql.AppendLine("	and b.Size =@Size ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
                }
                //材质
                if (!string.IsNullOrEmpty(model.Material))
                {
                    if (int.Parse(model.Material) > 0)
                    {
                        searchSql.AppendLine("	and b.Material=@Material ");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
                    }
                }
                //条码
                if (!string.IsNullOrEmpty(model.BarCode))
                {
                    searchSql.AppendLine("	and b.BarCode=@BarCode ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
                }

                //仓库
                if (!string.IsNullOrEmpty(model.StorageID))
                {
                    if (int.Parse(model.StorageID) > 0)
                    {
                        searchSql.AppendLine(" and a.StorageID=@StorageID");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
                    }
                }
                //批次
                if (!string.IsNullOrEmpty(BatchNo))
                {
                    searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                }
                if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                {
                    if (int.Parse(EFIndex) > 0)
                    {
                        searchSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                    }
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #endregion


        #region 库存数量验证
        public static  string ValidateStorageCount(string[] ProductID, string[] StorageID, string[] BatchNo, string CompanyCD)
        {

            string res = string.Empty;
            for (int i = 0; i < ProductID.Length; i++)
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ISNULL(a.ProductCount,0) AS ProductCount,");
                sbSql.AppendLine(" (SELECT ISNULL(MinusIs,0) FROM officedba.ProductInfo WHERE ID=@ProductID AND CompanyCD=@CompanyCD ) AS MinusIs");
                sbSql.AppendLine(" FROM officedba.StorageProduct AS a");
                sbSql.AppendLine(" WHERE  a.StorageID=@StorageID AND a.CompanyCD=@CompanyCD AND a.ProductID=@ProductID " + (BatchNo[i] == "" ? string.Empty : " AND a.BatchNo=@BatchNo"));

                SqlParameter[] sqlParams = new SqlParameter[(BatchNo[i] == "" ? 3 : 4)];
                int index = 0;
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", ProductID[i]);
                sqlParams[index++] = SqlHelper.GetParameter("@StorageID", StorageID[i]);
                sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                if (!string.IsNullOrEmpty(BatchNo[i]))
                    sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", BatchNo[i]);

                DataTable dtInfo = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    res += ProductID[i] + "|" + dtInfo.Rows[0]["ProductCount"].ToString() + "|" + dtInfo.Rows[0]["MinusIs"].ToString() + "^";
                }


            }

            return res.Remove(res.Length - 1, 1);



        }
        #endregion



        #region 根据物品名称 物品编号 判断是否启用批次
        public static bool IsBatchByProductNameAndNo(string ProductNo, string ProductName, string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT ISNULL(IsBatchNo,0) AS IsBatchNo FROM officedba.ProductInfo ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND ProdNo=@ProdNo AND ProductName=@ProductName ");

            SqlParameter[] sqlParams = new SqlParameter[3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@ProdNo", ProductNo);
            sqlParams[index++] = SqlHelper.GetParameter("@ProductName", ProductName);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["IsBatchNo"].ToString() == "1" ? true : false;
            }
            else
                return false;
        }
        #endregion

        #region 进销存汇分析(决策模式)
        /// <summary>
        /// 进销存汇分析(决策模式)
        /// </summary>
        /// <param name="StorageID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="ProductName"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="dt"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetStorageInAndOutTotalInfo(string StorageID, string BatchNo, string ProductName, string StartDate, string EndDate, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD.ToString();
            string point = userInfo.SelPoint;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();

            searchSql.AppendLine(" SELECT X.ProdNo,X.ProductName,X.Specification,  ");
            searchSql.AppendLine(" X.BatchNo,X.CodeName,X.StorageName,X.StorageID, ");
            searchSql.AppendLine(" SUM(OutSellTotal)OutSellTotal, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),SUM(OutSellTotal)))+'&nbsp;'OutSellTotal1, ");
            searchSql.AppendLine(" SUM(InPurchaseTotal)InPurchaseTotal, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),SUM(InPurchaseTotal)))+'&nbsp;'InPurchaseTotal1, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))) daysave, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))))+'&nbsp;' daysave1, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(ISNULL(SUM(OutSellTotal),0))/((datediff(day,'" + StartDate + "','" + EndDate + "')+1))) daysell, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),(ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1)))+'&nbsp;' daysell1, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(case when (ISNULL(SUM(OutSellTotal),0))=0 then 0   ");
            searchSql.AppendLine(" else ((ISNULL(SUM(InPurchaseTotal),0))/(SUM(OutSellTotal))) end))   ");
            searchSql.AppendLine(" as InOutRate, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),(case when (ISNULL(SUM(OutSellTotal),0))=0 then 0   ");
            searchSql.AppendLine(" else ((ISNULL(SUM(InPurchaseTotal),0))/(SUM(OutSellTotal))) end)))+'&nbsp;'  ");
            searchSql.AppendLine(" as InOutRate1, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(case when ((ISNULL(SUM(OutSellTotal),0))/((datediff(day,'" + StartDate + "','" + EndDate + "')+1)))=0 then 0   ");
            searchSql.AppendLine(" else (((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))/((ISNULL(SUM(OutSellTotal),0))/((datediff(day,'" + StartDate + "','" + EndDate + "')+1)))) end))    ");
            searchSql.AppendLine(" as savedays, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),(case when ((ISNULL(SUM(OutSellTotal),0))/((datediff(day,'" + StartDate + "','" + EndDate + "')+1)))=0 then 0   ");
            searchSql.AppendLine(" else (((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))/((ISNULL(SUM(OutSellTotal),0))/((datediff(day,'" + StartDate + "','" + EndDate + "')+1)))) end)))+'&nbsp;'    ");
            searchSql.AppendLine(" as savedays1, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(case when ((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))=0 then 0   ");
            searchSql.AppendLine(" else (SUM(SellCount)/((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))) end))    ");
            searchSql.AppendLine(" as savetimes, ");
            searchSql.AppendLine(" Convert(char(20),Convert(numeric(22," + point + "),(case when ((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))=0 then 0   ");
            searchSql.AppendLine(" else (SUM(SellCount)/((ISNULL(SUM(InPurchaseTotal),0)-ISNULL(SUM(OutSellTotal),0))/(datediff(day,'" + StartDate + "','" + EndDate + "')+1))) end)))+'&nbsp;'    ");
            searchSql.AppendLine(" as savetimes1 FROM ");
            searchSql.AppendLine(" (SELECT   ");
            searchSql.AppendLine(" ISNULL(E.ProdNo,ISNULL(D.ProdNo,ISNULL(B.ProdNo,C.ProdNo)))ProdNo, ");
            searchSql.AppendLine(" ISNULL(E.StorageID,ISNULL(D.StorageID,ISNULL(B.StorageID,C.StorageID)))StorageID, ");
            searchSql.AppendLine(" ISNULL(E.ProductName,ISNULL(D.ProductName,ISNULL(B.ProductName,C.ProductName)))ProductName, ");
            searchSql.AppendLine(" ISNULL(E.Specification,ISNULL(D.Specification,ISNULL(B.Specification,C.Specification)))Specification, ");
            searchSql.AppendLine(" ISNULL(E.BatchNo,ISNULL(D.BatchNo,ISNULL(B.BatchNo,C.BatchNo)))BatchNo, ");
            searchSql.AppendLine(" ISNULL(E.StorageName,ISNULL(D.StorageName,ISNULL(B.StorageName,C.StorageName)))StorageName, ");
            searchSql.AppendLine(" ISNULL(E.CodeName,ISNULL(D.CodeName,ISNULL(B.CodeName,C.CodeName)))CodeName, ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(ISNULL(B.ProductCount,0)-ISNULL(C.ProductCount,0))) OutSellTotal,  ");
            searchSql.AppendLine(" Convert(numeric(22," + point + "),(ISNULL(D.ProductCount,0)-ISNULL(E.ProductCount,0))) InPurchaseTotal,  ");
            searchSql.AppendLine(" ISNULL(B.ProductCount,0)SellCount  ");
            searchSql.AppendLine("  FROM  ");
            searchSql.AppendLine(" (select c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID, ");
            searchSql.AppendLine(" Sum(ISNULL(a.ProductCount,0))ProductCount from officedba.StorageOutSellDetail a  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageOutSell b  ");
            searchSql.AppendLine(" ON a.OutNo=b.OutNo AND a.CompanyCD=b.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.ProductInfo c  ");
            searchSql.AppendLine(" on a.ProductID=c.id AND b.CompanyCD=c.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.CodeUnitType d  ");
            searchSql.AppendLine(" ON c.UnitID=d.ID AND c.CompanyCD=d.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInfo e  ");
            searchSql.AppendLine(" ON a.StorageID=e.ID AND a.CompanyCD=e.CompanyCD  ");
            searchSql.AppendLine(" WHERE b.CompanyCD='"+CompanyCD+"' AND b.BillStatus='2'  ");
            if (!string.IsNullOrEmpty(StartDate))
                searchSql.AppendLine("  and b.ConfirmDate>='" + StartDate + "' ");
            if (!string.IsNullOrEmpty(EndDate))
                searchSql.AppendLine("  and b.ConfirmDate<='" + EndDate + "'  ");
            searchSql.AppendLine(" group by c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID) B ");
            searchSql.AppendLine(" Full JOIN  ");
            searchSql.AppendLine(" ( ");
            searchSql.AppendLine(" select c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID, ");
            searchSql.AppendLine(" Sum(ISNULL(a.ProductCount,0))ProductCount from officedba.StorageInOtherDetail a  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInOther b  ");
            searchSql.AppendLine(" ON a.InNo=b.InNo AND a.CompanyCD=b.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.ProductInfo c  ");
            searchSql.AppendLine(" on a.ProductID=c.id AND b.CompanyCD=c.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.CodeUnitType d  ");
            searchSql.AppendLine(" ON c.UnitID=d.ID AND c.CompanyCD=d.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInfo e  ");
            searchSql.AppendLine(" ON a.StorageID=e.ID AND a.CompanyCD=e.CompanyCD  ");
            searchSql.AppendLine(" WHERE b.CompanyCD='"+CompanyCD+"' AND b.BillStatus='2' and b.FromType='1' ");
            if (!string.IsNullOrEmpty(StartDate))
                searchSql.AppendLine("  and b.ConfirmDate>='" + StartDate + "' ");
            if (!string.IsNullOrEmpty(EndDate))
                searchSql.AppendLine("  and b.ConfirmDate<='" + EndDate + "'  ");
            searchSql.AppendLine(" group by c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID ");
            searchSql.AppendLine(" ) C ON B.ProdNo=C.ProdNo AND B.BatchNo=C.BatchNo AND B.StorageID=C.StorageID AND B.Specification=C.Specification AND B.UnitID=C.UnitID ");
            searchSql.AppendLine(" FULL JOIN ");
            searchSql.AppendLine(" ( ");
            searchSql.AppendLine(" select c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID, ");
            searchSql.AppendLine(" Sum(ISNULL(a.ProductCount,0))ProductCount from officedba.StorageInPurchaseDetail a  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInPurchase b  ");
            searchSql.AppendLine(" ON a.InNo=b.InNo AND a.CompanyCD=b.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.ProductInfo c  ");
            searchSql.AppendLine(" on a.ProductID=c.id AND b.CompanyCD=c.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.CodeUnitType d  ");
            searchSql.AppendLine(" ON c.UnitID=d.ID AND c.CompanyCD=d.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInfo e  ");
            searchSql.AppendLine("  ON a.StorageID=e.ID AND a.CompanyCD=e.CompanyCD  ");
            searchSql.AppendLine(" WHERE b.CompanyCD='"+CompanyCD+"' AND b.BillStatus='2'  ");
            if (!string.IsNullOrEmpty(StartDate))
                searchSql.AppendLine("  and b.ConfirmDate>='" + StartDate + "' ");
            if (!string.IsNullOrEmpty(EndDate))
                searchSql.AppendLine("  and b.ConfirmDate<='" + EndDate + "'  ");
            searchSql.AppendLine(" group by c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID ");
            searchSql.AppendLine(" )D ON B.ProdNo=D.ProdNo AND B.BatchNo=D.BatchNo AND B.StorageID=D.StorageID AND B.Specification=D.Specification AND B.UnitID=D.UnitID ");
            searchSql.AppendLine(" Full JOIN  ");
            searchSql.AppendLine(" ( ");
            searchSql.AppendLine(" select c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID, ");
            searchSql.AppendLine(" Sum(ISNULL(a.ProductCount,0))ProductCount from officedba.StorageOutOtherDetail a  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageOutOther b  ");
            searchSql.AppendLine(" ON a.OutNo=b.OutNo AND a.CompanyCD=b.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.ProductInfo c  ");
            searchSql.AppendLine(" on a.ProductID=c.id AND b.CompanyCD=c.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.CodeUnitType d  ");
            searchSql.AppendLine(" ON c.UnitID=d.ID AND c.CompanyCD=d.CompanyCD  ");
            searchSql.AppendLine(" LEFT OUTER JOIN officedba.StorageInfo e  ");
            searchSql.AppendLine(" ON a.StorageID=e.ID AND a.CompanyCD=e.CompanyCD  ");
            searchSql.AppendLine(" WHERE b.CompanyCD=@CompanyCD AND b.BillStatus='2' and b.FromType='1' ");
            if (!string.IsNullOrEmpty(StartDate))
                searchSql.AppendLine("  and b.ConfirmDate>='" + StartDate + "' ");
            if (!string.IsNullOrEmpty(EndDate))
                searchSql.AppendLine("  and b.ConfirmDate<='" + EndDate + "'  ");
            searchSql.AppendLine(" group by c.ProdNo,c.ProductName,c.Specification, ");
            searchSql.AppendLine(" a.BatchNo,a.UnitID,d.CodeName,e.StorageName,a.StorageID ");
            searchSql.AppendLine(" ) E ON B.ProdNo=E.ProdNo AND B.BatchNo=E.BatchNo AND B.StorageID=E.StorageID AND B.Specification=E.Specification AND B.UnitID=E.UnitID ");
            searchSql.AppendLine(" ) X ");
            searchSql.AppendLine(" WHERE 1=1  ");
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

            //仓库
            if (!string.IsNullOrEmpty(StorageID))
            {
                searchSql.AppendLine(" and X.StorageID=@StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            }
            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and X.BatchNo=@BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //品名
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and X.ProductName=@ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }
            searchSql.AppendLine(" GROUP BY X.ProdNo,X.ProductName,X.Specification, ");
            searchSql.AppendLine(" X.BatchNo,X.CodeName,X.StorageName,X.StorageID ");

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
    }
}
