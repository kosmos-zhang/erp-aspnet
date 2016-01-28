/**********************************************
 * 类作用：   计时工资设置
 * 建立人：   吴志强
 * 建立时间： 2009/05/06
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
    /// 类名：TimeItemDBHelper
    /// 描述：计时工资设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/06
    /// 最后修改时间：2009/05/06
    /// </summary>
    ///
    public class TimeItemDBHelper
    {
        #region 查询计时工资项信息
        /// <summary>
        /// 查询计时工资项信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="isUsed">启用状态</param>
        /// <returns></returns>
        public static DataTable SearchTimeItemInfo(string companyCD, bool isUsed)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine(" 	 ID                     ");
            searchSql.AppendLine(" 	,TimeNo                 ");
            searchSql.AppendLine(" 	,TimeName               ");
            searchSql.AppendLine(" 	,UnitPrice              ");
            searchSql.AppendLine(" 	,UsedStatus             ");
            searchSql.AppendLine(" FROM                     ");
            searchSql.AppendLine(" 	officedba.TimeItem ");
            searchSql.AppendLine(" WHERE                    ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //只查询启用中的数据
            if (isUsed)
            {
                //添加启用参数
                searchSql.AppendLine(" AND UsedStatus = @UsedStatus ");
                //启用状态
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", ConstUtil.USED_STATUS_ON));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查询计时工资单价
        /// <summary>
        /// 查询计时工资单价
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="itemNo">工资项编号</param>
        /// <returns></returns>
        public static DataTable GetTimeItemPrice(string companyCD, string itemNo)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                   ");
            searchSql.AppendLine(" 	 ID                     ");
            searchSql.AppendLine(" 	,TimeNo                 ");
            searchSql.AppendLine(" 	,TimeName               ");
            searchSql.AppendLine(" 	,UnitPrice              ");
            searchSql.AppendLine(" 	,UsedStatus             ");
            searchSql.AppendLine(" FROM                     ");
            searchSql.AppendLine(" 	officedba.TimeItem ");
            searchSql.AppendLine(" WHERE                    ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD  ");
            searchSql.AppendLine(" 	AND TimeNo = @TimeNo  ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TimeNo", itemNo));

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion



        public static bool IsUsedItem(string itemNo, string companyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                      ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount      ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.TimeSalary ");
            searchSql.AppendLine(" WHERE                       ");
            searchSql.AppendLine("   companyCD=@companyCD   and   TimeNo = @TimeNo     ");

            SqlCommand cmd = new SqlCommand();
            //设置SQL语句
            cmd.CommandText = searchSql.ToString();
            //工资项编号
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@TimeNo", itemNo));
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@companyCD", companyCD));
            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSearch(cmd);
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        #region 保存计时工资项信息
        /// <summary>
        /// 保存计时工资项信息
        /// </summary>
        /// <param name="lstEdit">计时工资项信息</param>
        /// <param name="companyCD">公司代码</param>
        /// <param name="modifyUserID">最后修改用户</param>
        /// <returns></returns>
        public static bool SaveTimeItemInfo(ArrayList lstEdit, string companyCD, string modifyUserID)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //保存库列表
                ArrayList lstSave = new ArrayList();
                //遍历所有计时工资项，进行增删改操作
                for (int i = 0; i < lstEdit.Count; i++)
                {
                    //获取值
                    TimeItemModel model = (TimeItemModel)lstEdit[i];
                    //设置公司代码
                    model.CompanyCD = companyCD;
                    //最后修改人
                    model.ModifiedUserID = modifyUserID;
                    //更新
                    if ("1".Equals(model.EditFlag))
                    {
                        //执行更新操作
                        lstSave.Add(UpdateTimeItemInfo(model));
                    }
                    //插入
                    else if ("0".Equals(model.EditFlag))
                    {
                        //执行插入操作
                        lstSave.Add(InsertTimeItemInfo(model));
                    }
                    //删除
                    else
                    {
                        //执行删除操作
                        lstSave.Add(DeleteTimeItemInfo(model.ID));
                    }
                }
                //执行保存操作
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstSave);
                //获取插入数据的ID
                for (int j = 0; j < lstEdit.Count; j++)
                {
                    //获取值
                    TimeItemModel model = (TimeItemModel)lstEdit[j];
                    //插入时
                    if ("0".Equals(model.EditFlag))
                    {
                        //获取插入的命令
                        SqlCommand comm = (SqlCommand)lstSave[j];
                        //设置计时工资项编号
                        model.ID = comm.Parameters["@SalaryID"].Value.ToString();
                    }
                }
            }

            return isSucc;
        }
        #endregion

        #region 新建计时工资项信息
        /// <summary>
        /// 新建计时工资项信息 
        /// </summary>
        /// <param name="model">计时工资项信息</param>
        /// <returns></returns>
        private static SqlCommand InsertTimeItemInfo(TimeItemModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO             ");
            insertSql.AppendLine(" officedba.TimeItem      ");
            insertSql.AppendLine(" 	(CompanyCD             ");
            insertSql.AppendLine(" 	,TimeNo                ");
            insertSql.AppendLine(" 	,TimeName              ");
            insertSql.AppendLine(" 	,UnitPrice             ");
            insertSql.AppendLine(" 	,UsedStatus            ");
            insertSql.AppendLine(" 	,ModifiedDate          ");
            insertSql.AppendLine(" 	,ModifiedUserID)       ");
            insertSql.AppendLine(" VALUES                  ");
            insertSql.AppendLine(" 	(@CompanyCD            ");
            insertSql.AppendLine(" 	,@TimeNo               ");
            insertSql.AppendLine(" 	,@TimeName             ");
            insertSql.AppendLine(" 	,@UnitPrice            ");
            insertSql.AppendLine(" 	,@UsedStatus           ");
            insertSql.AppendLine(" 	,getdate()             ");
            insertSql.AppendLine(" 	,@ModifiedUserID)      ");
            insertSql.AppendLine(" SET @SalaryID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@SalaryID", SqlDbType.Int));

            //执行插入并返回插入结果
            return comm;
        }
        #endregion

        #region 更新计时工资项信息
        /// <summary>
        /// 更新计时工资项信息
        /// </summary>
        /// <param name="model">计时工资项信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateTimeItemInfo(TimeItemModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.TimeItem     ");
            updateSql.AppendLine(" SET  TimeName = @TimeName          ");
            updateSql.AppendLine(" 	,UnitPrice = @UnitPrice           ");
            updateSql.AppendLine(" 	,UsedStatus = @UsedStatus         ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()         ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID ");
            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND TimeNo = @TimeNo              ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //参数
            SetSaveParameter(comm, model);
            //执行更新
            return comm;
        }
        #endregion

        #region 保存时参数设置
        /// <summary>
        /// 保存时参数设置
        /// </summary>
        /// <param name="comm">命令</param>
        /// <param name="model">人才代理信息</param>
        private static void SetSaveParameter(SqlCommand comm, TimeItemModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TimeNo", model.TimeNo));//计时工资项编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TimeName", model.TimeName));//计时工资项名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice", model.UnitPrice));//单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后修改人
        }
        #endregion

        #region 删除计时工资项信息
        /// <summary>
        /// 删除计时工资项信息
        /// </summary>
        /// <param name="ID">计时工资项ID</param>
        /// <returns></returns>
        private static SqlCommand DeleteTimeItemInfo(string ID)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.TimeItem ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" ID = @ItemID");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ItemID", ID));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #endregion
    }
}
