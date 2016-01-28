/**********************************************
 * 类作用：   社会保险设置
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
    /// 类名：InsuSocialDBHelper
    /// 描述：社会保险设置
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/05/04
    /// 最后修改时间：2009/05/04
    /// </summary>
    ///
    public class InsuSocialDBHelper
    {

        #region 查询社会保险信息
        /// <summary>
        /// 查询社会保险信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="isUsed">是否启用</param>
        /// <param name="insuName">保险名称</param>
        /// <returns></returns>
        public static DataTable SearchInsuSocialInfo(string companyCD, bool isUsed, string insuName)
        {

            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                  ");
            searchSql.AppendLine(" 	 ID                    ");
            searchSql.AppendLine(" 	,InsuranceName         ");
            searchSql.AppendLine(" 	,InsuranceWay          ");
            searchSql.AppendLine(" 	,CompanyPayRate        ");
            searchSql.AppendLine(" 	,PersonPayRate         ");
            searchSql.AppendLine(" 	,UsedStatus            ");
            searchSql.AppendLine(" FROM                    ");
            searchSql.AppendLine(" 	officedba.InsuSocial   ");
            searchSql.AppendLine(" WHERE                   ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //保险名称
            if (!string.IsNullOrEmpty(insuName))
            {
                //保险名称
                searchSql.AppendLine(" AND InsuranceName LIKE '%' + @InsuranceName + '%'");
                //参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceName", insuName));
            }
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

        #region 保存社会保险信息
        /// <summary>
        /// 保存社会保险信息
        /// </summary>
        /// <param name="lstEdit">社会保险信息</param>
        /// <param name="modifyUserID">最后修改人</param>
        /// <returns></returns>
        public static bool SaveInsuSocialInfo(ArrayList lstEdit, string companyCD, string modifyUserID)
        {
            //定义返回变量
            bool isSucc = true;
            //信息存在时，进行操作
            if (lstEdit != null && lstEdit.Count > 0)
            {
                //保存库列表
                ArrayList lstSave = new ArrayList();
                //遍历所有社会保险，进行增删改操作
                for (int i = 0; i < lstEdit.Count; i++)
                {
                    //获取值
                    InsuSocialModel model = (InsuSocialModel)lstEdit[i];
                    //设置最后修改人
                    model.ModifiedUserID = modifyUserID;
                    //设置公司代码
                    model.CompanyCD = companyCD;
                    //更新
                    if ("1".Equals(model.EditFlag))
                    {
                        //执行更新操作
                        lstSave.Add(UpdateInsuSocialInfo(model));
                    }
                    //插入
                    else if ("0".Equals(model.EditFlag))
                    {
                        //执行插入操作
                        lstSave.Add(InsertInsuSocialInfo(model));
                    }
                    //删除
                    else
                    {
                        //执行删除操作
                        lstSave.Add(DeleteInsuSocialInfo(model.ID,companyCD ));
                    }
                }
                //执行保存操作
                isSucc = SqlHelper.ExecuteTransWithArrayList(lstSave);
                //获取插入数据的ID
                for (int j = 0; j < lstEdit.Count; j++)
                {
                    //获取值
                    InsuSocialModel model = (InsuSocialModel)lstEdit[j];
                    //插入时
                    if ("0".Equals(model.EditFlag))
                    {
                        //获取插入的命令
                        SqlCommand comm = (SqlCommand)lstSave[j];
                        //设置社会保险编号
                        model.ID = comm.Parameters["@InsuSocialID"].Value.ToString();
                    }
                }
            }

            return isSucc;
        }
        #endregion

        #region 新建社会保险信息
        /// <summary>
        /// 新建社会保险申请信息 
        /// </summary>
        /// <param name="model">社会保险申请信息</param>
        /// <returns></returns>
        private static SqlCommand InsertInsuSocialInfo(InsuSocialModel model)
        {
            #region 登陆SQL文
            StringBuilder insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO          ");
            insertSql.AppendLine(" officedba.InsuSocial ");
            insertSql.AppendLine(" 	(CompanyCD          ");
            insertSql.AppendLine(" 	,InsuranceName      ");
            insertSql.AppendLine(" 	,InsuranceWay       ");
            insertSql.AppendLine(" 	,CompanyPayRate     ");
            insertSql.AppendLine(" 	,PersonPayRate      ");
            insertSql.AppendLine(" 	,UsedStatus         ");
            insertSql.AppendLine(" 	,ModifiedDate       ");
            insertSql.AppendLine(" 	,ModifiedUserID)    ");
            insertSql.AppendLine(" VALUES               ");
            insertSql.AppendLine(" 	(@CompanyCD         ");
            insertSql.AppendLine(" 	,@InsuranceName     ");
            insertSql.AppendLine(" 	,@InsuranceWay      ");
            insertSql.AppendLine(" 	,@CompanyPayRate    ");
            insertSql.AppendLine(" 	,@PersonPayRate     ");
            insertSql.AppendLine(" 	,@UsedStatus        ");
            insertSql.AppendLine(" 	,getdate()          ");
            insertSql.AppendLine(" 	,@ModifiedUserID)   ");
            insertSql.AppendLine("   SET @InsuSocialID= @@IDENTITY  ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = insertSql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);

            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@InsuSocialID", SqlDbType.Int));

            //执行插入并返回插入结果
            return comm;
        }
        #endregion

        #region 更新社会保险申请信息
        /// <summary>
        /// 更新社会保险申请信息
        /// </summary>
        /// <param name="model">社会保险申请信息</param>
        /// <returns></returns>
        private static SqlCommand UpdateInsuSocialInfo(InsuSocialModel model)
        {

            #region SQL文拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE officedba.InsuSocial         ");
            updateSql.AppendLine(" SET  InsuranceName = @InsuranceName ");
            updateSql.AppendLine(" 	,InsuranceWay = @InsuranceWay      ");
            updateSql.AppendLine(" 	,CompanyPayRate = @CompanyPayRate  ");
            updateSql.AppendLine(" 	,PersonPayRate = @PersonPayRate    ");
            updateSql.AppendLine(" 	,UsedStatus = @UsedStatus          ");
            updateSql.AppendLine(" 	,ModifiedDate = getdate()          ");
            updateSql.AppendLine(" 	,ModifiedUserID = @ModifiedUserID  ");
            updateSql.AppendLine(" WHERE                               ");
            updateSql.AppendLine("companyCD=@companyCD and  	ID = @ID                           ");
            #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //社会保险编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@companyCD", model.CompanyCD ));
            //其他参数
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
        private static void SetSaveParameter(SqlCommand comm, InsuSocialModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceName", model.InsuranceName));//社会保险名称
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceWay", model.InsuranceWay));//投保方式
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyPayRate", model.CompanyPayRate));//单位缴费比例
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PersonPayRate", model.PersonPayRate));//个人缴费比例
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态(0停用，1启用)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//更新用户ID
        }
        #endregion

        #region 删除社会保险信息
        /// <summary>
        /// 删除社会保险信息
        /// </summary>
        /// <param name="ID">社会保险ID</param>
        /// <returns></returns>
        private static SqlCommand DeleteInsuSocialInfo(string ID, string companyCD)
        {
            //删除SQL拼写
            StringBuilder deleteSql = new StringBuilder();
            deleteSql.AppendLine(" DELETE FROM officedba.InsuSocial ");
            deleteSql.AppendLine(" WHERE ");
            deleteSql.AppendLine(" companyCD=@companyCD and ID = @ID");

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //社会保险ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@companyCD", companyCD));
            //设置SQL语句
            comm.CommandText = deleteSql.ToString();

            //执行删除并返回
            return comm;
        }
        #endregion

        #region 校验保险项目是否被使用
        /// <summary>
        /// 校验保险项目是否被使用
        /// </summary>
        /// <param name="itemNo">保险项ID</param>
        /// <returns></returns>
        public static bool IsUsedItem(string itemNo, string companyCD)
        {
            //校验SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                      ");
            searchSql.AppendLine(" 	COUNT(*) AS UsedCount      ");
            searchSql.AppendLine(" FROM                        ");
            searchSql.AppendLine(" 	officedba.InsuEmployee ");
            searchSql.AppendLine(" WHERE  companyCD=@companyCD and                       ");
            searchSql.AppendLine(" InsuranceID = @InsuranceID  ");

            SqlCommand cmd = new SqlCommand();
            //设置SQL语句
            cmd.CommandText = searchSql.ToString();
            //工资项编号
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@InsuranceID", itemNo));
            cmd.Parameters.Add(SqlHelper.GetParameterFromString("@companyCD", companyCD ));
            //执行查询
            DataTable dtCount = SqlHelper.ExecuteSearch(cmd);
            //获取记录数
            int count = GetSafeData.ValidateDataRow_Int(dtCount.Rows[0], "UsedCount");

            //返回结果
            return count > 0 ? true : false;
        }
        #endregion

    }
}
