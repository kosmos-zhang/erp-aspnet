/**********************************************
 * 类作用：   工艺档案数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/03/07
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Office.ProductionManager
{
    public class TechnicsArchivesDBHelper
    {
        #region 通过检索条件查询工作中心信息
        /// <summary>
        /// 通过检索条件查询工作中心信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetTechnicsArchivesTableBycondition(TechnicsArchivesModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from ");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select	a.CompanyCD,a.ID,a.TechNo,a.PYShort,a.TechName,a.Description,a.UsedStatus,case when a.UsedStatus=1 then '启用' when a.UsedStatus=0 then '停用' end as strUsedStatus,");
            searchSql.AppendLine("			a.Creator,b.EmployeeName,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            searchSql.AppendLine("			a.Remark,a.ModifiedDate");
            searchSql.AppendLine("	from officedba.TechnicsArchives a ");
            searchSql.AppendLine("	left join officedba.EmployeeInfo b on a.Creator=b.ID");
            searchSql.AppendLine(")as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //工艺档案编号
            if (!string.IsNullOrEmpty(model.TechNo))
            {
                searchSql.AppendLine("and TechNo like @TechNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TechNo", "%" + model.TechNo + "%"));
            }
            //工艺档案名称
            if (!string.IsNullOrEmpty(model.TechName))
            {
                searchSql.AppendLine(" and TechName like @TechName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TechName", "%" + model.TechName + "%"));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine(" and PYShort=@PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));
            }
            //使用状态
            if (!string.IsNullOrEmpty(model.UsedStatus))
            {
                if (int.Parse(model.UsedStatus) > -1)
                {
                    searchSql.AppendLine("and UsedStatus=@UsedStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 工艺档案详细信息
        /// <summary>
        /// 工艺档案详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTechnicsArchivesDetailInfo(TechnicsArchivesModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select	a.CompanyCD,a.ID,a.TechNo,a.PYShort,a.TechName,a.Description,a.UsedStatus,");
            infoSql.AppendLine("			a.Creator,b.EmployeeName,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("			a.Remark,a.ModifiedDate ");
            infoSql.AppendLine("	from officedba.TechnicsArchives a");
            infoSql.AppendLine("	left join officedba.EmployeeInfo b on a.Creator=b.ID");
            infoSql.AppendLine("	where a.ID=@ID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 添加：工艺档案
        /// <summary>
        /// 添加工艺档案记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertTechnicsArchives(TechnicsArchivesModel model, string loginUserID, out string ID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.TechnicsArchives");
            sql.AppendLine("	    (CompanyCD      ");
            sql.AppendLine("		,TechNo         ");
            sql.AppendLine("		,TechName       ");
            sql.AppendLine("		,PYShort        ");
            sql.AppendLine("		,Description    ");
            sql.AppendLine("		,UsedStatus     ");
            sql.AppendLine("		,Creator        ");
            sql.AppendLine("		,CreateDate     ");
            sql.AppendLine("		,Remark         ");
            sql.AppendLine("		,ModifiedDate   ");
            sql.AppendLine("		,ModifiedUserID)");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@TechNo        ");
            sql.AppendLine("		,@TechName      ");
            sql.AppendLine("		,@PYShort       ");
            sql.AppendLine("		,@Description   ");
            sql.AppendLine("		,@UsedStatus    ");
            sql.AppendLine("		,@Creator       ");
            sql.AppendLine("		,@CreateDate    ");
            sql.AppendLine("		,@Remark        ");
            sql.AppendLine("		,getdate()      ");
            sql.AppendLine("		,'" + loginUserID + "')       ");
            sql.AppendLine("set @ID= @@IDENTITY");

            //设置参数
            SqlParameter[] param = new SqlParameter[10];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@TechNo", model.TechNo);
            param[2] = SqlHelper.GetParameter("@TechName", model.TechName);
            param[3] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[4] = SqlHelper.GetParameter("@Description", model.Description);
            param[5] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[6] = SqlHelper.GetParameter("@Creator", model.Creator);
            param[7] = SqlHelper.GetParameter("@CreateDate", model.CreateDate);
            param[8] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[9] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            ID = param[9].Value.ToString();
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 修改：工艺档案
        /// <summary>
        /// 更新工艺档案记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>                                        
        /// <returns></returns>
        public static bool UpdateTechnicsArchives(TechnicsArchivesModel model, string loginUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.TechnicsArchives SET");
            sql.AppendLine(" TechName       = @TechName,");
            sql.AppendLine(" PYShort        = @PYShort,");
            sql.AppendLine(" Description    = @Description,");
            sql.AppendLine(" UsedStatus     = @UsedStatus,");
            sql.AppendLine(" Remark         = @Remark,");
            sql.AppendLine(" ModifiedDate   = getdate(),");
            sql.AppendLine(" ModifiedUserID = '" + loginUserID + "' ");
            sql.AppendLine(" Where CompanyCD=@CompanyCD and ID=@ID");


            SqlParameter[] param = new SqlParameter[7];
            param[0] = SqlHelper.GetParameter("@TechName", model.TechName);
            param[1] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[2] = SqlHelper.GetParameter("@Description", model.Description);
            param[3] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[4] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[5] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[6] = SqlHelper.GetParameter("@ID", model.ID);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 删除：工艺档案
        /// <summary>
        /// 删除工艺档案
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteTechnicsArchives(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();

            StringBuilder sqlWork = new StringBuilder();
            sqlWork.AppendLine("delete from officedba.TechnicsArchives where CompanyCD=@CompanyCD and ID in(" + ID + ")");

            SqlCommand commWrok = new SqlCommand();
            commWrok.CommandText = sqlWork.ToString();
            commWrok.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            listADD.Add(commWrok);
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 单据是否被引用
        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(" + ID + ")";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
    }
}
