/**********************************************
 * 类作用：   工作中心数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/03/05
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
    public class WorkCenterDBHelper
    {

        #region 通过检索条件查询工作中心信息
        /// <summary>
        /// 通过检索条件查询工作中心信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetWorkCenterTableBycondition(WorkCenterModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from ");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select	a.CompanyCD,a.ID,a.WCNo,a.WCName,a.PYShort,a.IsMain,case when a.IsMain=1 then '是' when a.Ismain=0 then '否' end as strMain,a.DeptID,b.DeptName,a.ModifiedDate,");
            searchSql.AppendLine("			a.Creator,c.EmployeeName,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.Remark,a.UsedStatus,case when a.UsedStatus=1 then '启用' when a.UsedStatus=0 then '停用' end as strUsedStatus");
            searchSql.AppendLine("	from officedba.WorkCenter a");
            searchSql.AppendLine("	left join officedba.DeptInfo b on a.DeptID=b.ID");
            searchSql.AppendLine("	left join officedba.EmployeeInfo c on a.Creator=c.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //工作中心编码
            if (!string.IsNullOrEmpty(model.WCNo))
            {
                searchSql.AppendLine("and WCNo like @WCNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@WCNo", "%" + model.WCNo + "%"));
            }
            //工作中心名称
            if (!string.IsNullOrEmpty(model.WCName))
            {
                searchSql.AppendLine(" and WCName like @WCName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@WCName", "%" + model.WCName + "%"));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine(" and PYShort=@PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));
            }
            //主要工作中心
            if (!string.IsNullOrEmpty(model.IsMain))
            {
                if (int.Parse(model.IsMain)>-1)
                {
                    searchSql.AppendLine(" and IsMain=@IsMain ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsMain", model.IsMain));
                }
            }
            //部门
            if (model.DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID.ToString()));
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
            //return SqlHelper.ExecuteSearch(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 工作中心详细信息
        /// <summary>
        /// 工作中心详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetWorkCenter(WorkCenterModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select	a.CompanyCD,a.ID,a.WCNo,a.WCName,a.PYShort,a.IsMain,a.DeptID,c.DeptName,");
            infoSql.AppendLine("		a.Creator,b.EmployeeName,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate ,");
            infoSql.AppendLine("		a.Remark,a.UsedStatus,isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate ");
            infoSql.AppendLine("from officedba.WorkCenter a");
            infoSql.AppendLine("left join officedba.EmployeeInfo b on a.Creator=b.ID");
            infoSql.AppendLine("left join officedba.DeptInfo c on a.DeptID=c.ID");
            infoSql.AppendLine("where a.ID=@ID");

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

        #region 添加：工作中心
        /// <summary>
        /// 添加工作中心记录
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertWorkCenter(WorkCenterModel model, string loginUserID, out string ID)
        {

            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.WorkCenter");
            sql.AppendLine("	    (CompanyCD      ");
            sql.AppendLine("		,WCNo           ");
            sql.AppendLine("		,WCName         ");
            sql.AppendLine("		,PYShort        ");
            sql.AppendLine("		,IsMain         ");
            sql.AppendLine("		,DeptID         ");
            sql.AppendLine("		,Creator        ");
            sql.AppendLine("		,CreateDate     ");
            sql.AppendLine("		,Remark         ");
            sql.AppendLine("		,UsedStatus     ");
            sql.AppendLine("		,ModifiedDate   ");
            sql.AppendLine("		,ModifiedUserID)");
            sql.AppendLine("VALUES                  ");
            sql.AppendLine("		(@CompanyCD     ");
            sql.AppendLine("		,@WCNo          ");
            sql.AppendLine("		,@WCName        ");
            sql.AppendLine("		,@PYShort       ");
            sql.AppendLine("		,@IsMain        ");
            sql.AppendLine("		,@DeptID        ");
            sql.AppendLine("		,@Creator       ");
            sql.AppendLine("		,@CreateDate    ");
            sql.AppendLine("		,@Remark        ");
            sql.AppendLine("		,@UsedStatus    ");
            sql.AppendLine("		,getdate()     ");
            sql.AppendLine("		,'" + loginUserID + "')       ");
            sql.AppendLine("set @ID= @@IDENTITY");

            //设置参数
            SqlParameter[] param = new SqlParameter[11];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@WCNo", model.WCNo);
            param[2] = SqlHelper.GetParameter("@WCName", model.WCName);
            param[3] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[4] = SqlHelper.GetParameter("@IsMain", model.IsMain);
            param[5] = SqlHelper.GetParameter("@DeptID", model.DeptID);
            param[6] = SqlHelper.GetParameter("@Creator", model.Creator);
            param[7] = SqlHelper.GetParameter("@CreateDate", model.CreateDate);
            param[8] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[9] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[10] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            ID = param[10].Value.ToString();
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        #endregion

        #region 修改：工作中心
        /// <summary>
        /// 更新工作中心记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateWorkCenter(WorkCenterModel model, string loginUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.WorkCenter SET");
            sql.AppendLine(" WCName         = @WCName,");
            sql.AppendLine(" PYShort        = @PYShort,");
            sql.AppendLine(" IsMain         = @IsMain,");
            sql.AppendLine(" DeptID         = @DeptID,");
            sql.AppendLine(" Remark         = @Remark,");
            sql.AppendLine(" UsedStatus     = @UsedStatus,");
            sql.AppendLine(" ModifiedDate   = getdate(),");
            sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");


            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@ID", model.ID);
            param[1] = SqlHelper.GetParameter("@WCName", model.WCName);
            param[2] = SqlHelper.GetParameter("@PYShort", model.PYShort);
            param[3] = SqlHelper.GetParameter("@IsMain", model.IsMain);
            param[4] = SqlHelper.GetParameter("@DeptID", model.DeptID);
            param[5] = SqlHelper.GetParameter("@Remark", model.Remark);
            param[6] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[7] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 删除工作中心
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteWorkCenter(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();

            StringBuilder sqlWork = new StringBuilder();
            sqlWork.AppendLine("delete from officedba.WorkCenter where CompanyCD=@CompanyCD and ID in("+ID+")");

            SqlCommand commWrok = new SqlCommand();
            commWrok.CommandText = sqlWork.ToString();
            commWrok.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            listADD.Add(commWrok);
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }

        #region 单据是否被引用
        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrence(string CompanyCD,string ID,string TableName,string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba."+TableName+" where CompanyCD=@CompanyCD and "+ColumnName+" in("+ID+")";
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
