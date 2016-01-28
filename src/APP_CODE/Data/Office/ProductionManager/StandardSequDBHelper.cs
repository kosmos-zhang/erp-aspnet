/**********************************************
 * 类作用：   标准工序数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/03/09
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.ProductionManager
{
    public class StandardSequDBHelper
    {

        #region 标准工序插入
        /// <summary>
        /// 领料单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertStandardSequ(StandardSequModel model, string TechID, string Remark, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  退料单添加SQL语句
                StringBuilder sqlSequ = new StringBuilder();
                sqlSequ.AppendLine("INSERT INTO officedba.StandardSequ");
                sqlSequ.AppendLine("	    (CompanyCD      ");
                sqlSequ.AppendLine("		,SequNo           ");
                sqlSequ.AppendLine("		,SequName         ");
                sqlSequ.AppendLine("		,PYShort        ");
                sqlSequ.AppendLine("		,WCID         ");
                sqlSequ.AppendLine("		,CheckWay         ");
                sqlSequ.AppendLine("		,TimeUnit        ");
                sqlSequ.AppendLine("		,ReadyTime     ");
                sqlSequ.AppendLine("		,RunTime     ");
                sqlSequ.AppendLine("		,IsCharge         ");
                sqlSequ.AppendLine("		,Isoutsource     ");
                sqlSequ.AppendLine("		,TimeWage   ");
                sqlSequ.AppendLine("		,PieceWage");
                sqlSequ.AppendLine("		,Creator");
                sqlSequ.AppendLine("		,CreateDate");
                sqlSequ.AppendLine("		,UsedStatus");
                sqlSequ.AppendLine("		,ModifiedDate");
                sqlSequ.AppendLine("		,ModifiedUserID)");
                sqlSequ.AppendLine("VALUES                  ");
                sqlSequ.AppendLine("		(@CompanyCD");
                sqlSequ.AppendLine("		,@SequNo");
                sqlSequ.AppendLine("		,@SequName");
                sqlSequ.AppendLine("		,@PYShort ");
                sqlSequ.AppendLine("		,@WCID");
                sqlSequ.AppendLine("		,@CheckWay");
                sqlSequ.AppendLine("		,@TimeUnit");
                sqlSequ.AppendLine("		,@ReadyTime");
                sqlSequ.AppendLine("		,@RunTime");
                sqlSequ.AppendLine("		,@IsCharge");
                sqlSequ.AppendLine("		,@Isoutsource");
                sqlSequ.AppendLine("		,@TimeWage");
                sqlSequ.AppendLine("		,@PieceWage");
                sqlSequ.AppendLine("		,@Creator");
                sqlSequ.AppendLine("		,@CreateDate ");
                sqlSequ.AppendLine("		,@UsedStatus ");
                sqlSequ.AppendLine("		,getdate()     ");
                sqlSequ.AppendLine("		,'" + loginUserID + "')       ");
                sqlSequ.AppendLine("set @ID=@@IDENTITY                      ");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlSequ.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@SequNo", model.SequNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@SequName", model.SequName));
                comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
                comm.Parameters.Add(SqlHelper.GetParameter("@WCID", model.WCID.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckWay", model.CheckWay.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@TimeUnit", model.TimeUnit.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ReadyTime", model.ReadyTime.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@RunTime", model.RunTime.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@IsCharge", model.IsCharge.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@Isoutsource", model.Isoutsource.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@TimeWage", model.TimeWage));
                comm.Parameters.Add(SqlHelper.GetParameter("@PieceWage", model.PieceWage));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                #region 标准工序明细
                if (!string.IsNullOrEmpty(TechID))
                {
                    string[] techID = TechID.Split(',');
                    string[] remark = Remark.Split(',');
                    if (techID.Length >= 1)
                    {
                        for (int i = 0; i < techID.Length; i++)
                        {
                            StringBuilder sqlSequDet = new StringBuilder();
                            sqlSequDet.AppendLine(" Insert into  officedba.StandardSequDetail(CompanyCD,SequNo,TechID,Remark)");
                            sqlSequDet.AppendLine(" Values(@CompanyCD,@SequNo,@TechID,@Remark)");

                            SqlCommand commDet = new SqlCommand();
                            commDet.CommandText = sqlSequDet.ToString();
                            commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@SequNo", model.SequNo));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@TechID", techID[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@Remark", remark[i].ToString()));
                            listADD.Add(commDet);
                        }
                    }
                }
                #endregion


                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                else
                {
                    ID = "0";
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 修改标准工序
        /// <summary>
        /// 修改标准工序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateStandardSequ(StandardSequModel model, string TechID,string Remark,string loginUserID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }


            #region 工序更新SQL语句
            StringBuilder sqlSequ = new StringBuilder();
            sqlSequ.AppendLine(" UPDATE officedba.StandardSequ SET      ");
            sqlSequ.AppendLine(" SequName       =@SequName,             ");
            sqlSequ.AppendLine(" PYShort        =@PYShort,              ");
            sqlSequ.AppendLine(" WCID           =@WCID,                 ");
            sqlSequ.AppendLine(" CheckWay       =@CheckWay,             ");
            sqlSequ.AppendLine(" TimeUnit       =@TimeUnit,             ");
            sqlSequ.AppendLine(" ReadyTime      =@ReadyTime,            ");
            sqlSequ.AppendLine(" RunTime        =@RunTime,              ");
            sqlSequ.AppendLine(" IsCharge       =@IsCharge,             ");
            sqlSequ.AppendLine(" Isoutsource    =@Isoutsource,          ");
            sqlSequ.AppendLine(" TimeWage       =@TimeWage,             ");
            sqlSequ.AppendLine(" pieceWage      =@pieceWage,            ");
            sqlSequ.AppendLine(" UsedStatus     =@UsedStatus,           ");
            sqlSequ.AppendLine(" ModifiedDate   =getdate(),             ");
            sqlSequ.AppendLine(" ModifiedUserID = '" + loginUserID + "' ");
            sqlSequ.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlSequ.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@SequName", model.SequName));
            comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
            comm.Parameters.Add(SqlHelper.GetParameter("@WCID", model.WCID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckWay", model.CheckWay));
            comm.Parameters.Add(SqlHelper.GetParameter("@TimeUnit", model.TimeUnit));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReadyTime", model.ReadyTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@RunTime", model.RunTime));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsCharge", model.IsCharge));
            comm.Parameters.Add(SqlHelper.GetParameter("@Isoutsource", model.Isoutsource));
            comm.Parameters.Add(SqlHelper.GetParameter("@TimeWage", model.TimeWage));
            comm.Parameters.Add(SqlHelper.GetParameter("@pieceWage", model.PieceWage));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));

            listADD.Add(comm);

            #endregion


            #region 标准工序明细处理

            #region 删除标准工序明细
            StringBuilder sqlProductDel = new StringBuilder();
            sqlProductDel.AppendLine("delete from officedba.StandardSequDetail ");
            sqlProductDel.AppendLine("where CompanyCD=@CompanyCD");
            sqlProductDel.AppendLine("and SequNo=(");
            sqlProductDel.AppendLine("				select top 1 SequNo from officedba.StandardSequ where CompanyCD=@CompanyCD and ID=@ID");
            sqlProductDel.AppendLine("			    )");

            SqlCommand commProductDel = new SqlCommand();
            commProductDel.CommandText = sqlProductDel.ToString();
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            listADD.Add(commProductDel);
            #endregion


            #region 标准工序明细添加SQL语句
            #region 标准工序明细
            if (!string.IsNullOrEmpty(TechID))
            {
                string[] techID = TechID.Split(',');
                string[] remark = Remark.Split(',');
                if (techID.Length >= 1)
                {
                    for (int i = 0; i < techID.Length; i++)
                    {
                        StringBuilder sqlSequDet = new StringBuilder();
                        sqlSequDet.AppendLine(" Insert into  officedba.StandardSequDetail(CompanyCD,SequNo,TechID,Remark)");
                        sqlSequDet.AppendLine(" Values(@CompanyCD,@SequNo,@TechID,@Remark)");

                        SqlCommand commDet = new SqlCommand();
                        commDet.CommandText = sqlSequDet.ToString();
                        commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commDet.Parameters.Add(SqlHelper.GetParameter("@SequNo", model.SequNo));
                        commDet.Parameters.Add(SqlHelper.GetParameter("@TechID", techID[i].ToString()));
                        commDet.Parameters.Add(SqlHelper.GetParameter("@Remark", remark[i].ToString()));
                        listADD.Add(commDet);
                    }
                }
            }
            #endregion

            #endregion

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 通过检索条件查询标准工序信息
        /// <summary>
        /// 通过检索条件查询标准工序信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetStandardSequTableBycondition(StandardSequModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from ");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select	a.CompanyCD,a.ID,a.SequNo,a.SequName,a.PYShort,a.WCID,isnull(b.WCName,'') as WCName,");
            searchSql.AppendLine("			a.CheckWay,a.ReadyTime,a.TimeUnit,a.RunTime,a.IsCharge,a.Isoutsource,");
            searchSql.AppendLine("case when a.IsCharge=0 then '否' when a.IsCharge=1 then '是' end as strCharge,");
            searchSql.AppendLine("case when a.Isoutsource=0 then '否' when a.Isoutsource=1 then '是' end as strOutsource,");
            searchSql.AppendLine("case when a.UsedStatus=0 then '停用' when a.UsedStatus=1 then '启用' end as strUsedStatus,");
            searchSql.AppendLine("case when a.CheckWay=0 then '免检' when a.CheckWay=1 then '全检' when a.CheckWay=2 then '抽检' when a.CheckWay=3 then '不定期检验' end as strCheckWay,");
            searchSql.AppendLine("			Convert(numeric(14,"+userInfo.SelPoint+"),a.TimeWage) as TimeWage,Convert(numeric(14,"+userInfo.SelPoint+"),a.PieceWage) as PieceWage,");
            searchSql.AppendLine("			a.UsedStatus,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.ModifiedDate ");
            searchSql.AppendLine("	 from officedba.StandardSequ a");
            searchSql.AppendLine("	left join officedba.WorkCenter b on a.WCID=b.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //标准工序编码
            if (!string.IsNullOrEmpty(model.SequNo))
            {
                searchSql.AppendLine("and SequNo like @SequNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SequNo", "%" + model.SequNo + "%"));
            }
            //工序名称
            if (!string.IsNullOrEmpty(model.SequName))
            {
                searchSql.AppendLine(" and SequName like @SequName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SequName", "%" + model.SequName + "%"));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine(" and PYShort=@PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));
            }
            //主要工作中心
            if (model.WCID > 0)
            {
                searchSql.AppendLine(" and WCID=@WCID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@WCID", model.WCID.ToString()));
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

        #region 获取标准工序信息
        /// <summary>
        /// 获取标准工序信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetStandardSequDetailInfo(StandardSequModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select	a.CompanyCD,a.ID,a.SequNo,a.SequName,a.PYShort,");
            infoSql.AppendLine("		a.WCID,a.CheckWay,a.TimeUnit,a.ReadyTime,a.RunTime,");
            infoSql.AppendLine("		a.IsCharge,a.Isoutsource,Convert(numeric(14," + userInfo.SelPoint+ "),a.TimeWage) as TimeWage,");
            infoSql.AppendLine("		Convert(numeric(14," + userInfo.SelPoint + "),a.PieceWage) as PieceWage,a.PieceWage,a.Creator,");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.UsedStatus,a.ModifiedUserID ,d.EmployeeName as CreatorReal,");
            infoSql.AppendLine("		b.* ");
            infoSql.AppendLine("from officedba.StandardSequ a ");
            infoSql.AppendLine("left join officedba.EmployeeInfo d on a.CompanyCD=@CompanyCD and a.Creator=d.ID");
            infoSql.AppendLine("left outer join (");
            infoSql.AppendLine("select c.ID as DetailID,c.TechID,c.Remark,c.SequNo,d.TechName");
            infoSql.AppendLine("from officedba.StandardSequDetail c ");
            infoSql.AppendLine("inner join officedba.TechnicsArchives d on c.CompanyCD=@CompanyCD and d.ID=c.TechID");
            infoSql.AppendLine(") as b on b.SequNo=a.SequNo where a.ID=@ID");
            #endregion


            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
       #endregion

        #region 删除：标准工序
        /// <summary>
        /// 删除标准工序
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStandardSequ(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.StandardSequDetail where CompanyCD=@CompanyCD and SequNo=(select top 1 SequNo from officedba.StandardSequ where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.StandardSequ where CompanyCD=@CompanyCD and ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
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
