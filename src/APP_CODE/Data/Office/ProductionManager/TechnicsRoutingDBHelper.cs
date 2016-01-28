/**********************************************
 * 类作用：   工艺路线数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/03/17
 ***********************************************/

using System;
using XBase.Model.Office.ProductionManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;


namespace XBase.Data.Office.ProductionManager
{
    public class TechnicsRoutingDBHelper
    {

        #region 通过检索条件查询工艺路线信息
        /// <summary>
        /// 通过检索条件查询工艺路线信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetTechnicsRoutingTableBycondition(TechnicsRoutingModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from ");
            searchSql.AppendLine("(");
            searchSql.AppendLine("	select	a.CompanyCD,a.ID,a.RouteNo,a.RouteName,a.PYShort,a.Verson,a.ModifiedDate,case when a.IsMainTech=1 then '是' when a.IsMainTech=0 then '否' end as strMainTech,");
            searchSql.AppendLine("			a.IsMainTech,a.UsedStatus,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,a.Remark,isnull(b.ProductName,'') ProductName,case when a.UsedStatus=1 then '启用' when a.UsedStatus=0 then '停用' end as strUsedStatus ");
            searchSql.AppendLine("	from officedba.TechnicsRouting a ");
            searchSql.AppendLine("	left join officedba.ProductInfo b on a.ProductID=b.ID");
            searchSql.AppendLine(")");
            searchSql.AppendLine("as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //工艺路线编码
            if (!string.IsNullOrEmpty(model.RouteNo))
            {
                searchSql.AppendLine("and RouteNo like @RouteNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RouteNo", "%" + model.RouteNo + "%"));
            }
            //工艺路线名称
            if (!string.IsNullOrEmpty(model.RouteName))
            {
                searchSql.AppendLine(" and RouteName like @RouteName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RouteName", "%" + model.RouteName + "%"));
            }
            //拼音缩写
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine(" and PYShort=@PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));
            }
            //主要工作中心
            if (!string.IsNullOrEmpty(model.IsMainTech))
            {
                if (int.Parse(model.IsMainTech) > -1)
                {
                    searchSql.AppendLine(" and IsMainTech=@IsMainTech ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsMainTech", model.IsMainTech));
                }
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


        #region 工艺路线插入
        /// <summary>
        /// 领料单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertTechnicsRouting(TechnicsRoutingModel model,string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  退料单添加SQL语句
                StringBuilder sqlRoute = new StringBuilder();
                sqlRoute.AppendLine("INSERT INTO officedba.TechnicsRouting");
                sqlRoute.AppendLine("	    (CompanyCD          ");
                sqlRoute.AppendLine("		,RouteNo            ");
                sqlRoute.AppendLine("		,RouteName          ");
                sqlRoute.AppendLine("		,PYShort            ");
                sqlRoute.AppendLine("		,Verson             ");
                sqlRoute.AppendLine("		,IsMainTech         ");
                sqlRoute.AppendLine("		,BomID              ");
                sqlRoute.AppendLine("		,ProductID          ");
                sqlRoute.AppendLine("		,UsedStatus         ");
                sqlRoute.AppendLine("		,Creator            ");
                sqlRoute.AppendLine("		,CreateDate         ");
                sqlRoute.AppendLine("		,Remark             ");
                sqlRoute.AppendLine("		,ModifiedDate       ");
                sqlRoute.AppendLine("		,ModifiedUserID)    ");
                sqlRoute.AppendLine("VALUES                     ");
                sqlRoute.AppendLine("		(@CompanyCD         ");
                sqlRoute.AppendLine("		,@RouteNo           ");
                sqlRoute.AppendLine("		,@RouteName         ");
                sqlRoute.AppendLine("		,@PYShort           ");
                sqlRoute.AppendLine("		,@Verson            ");
                sqlRoute.AppendLine("		,@IsMainTech        ");
                sqlRoute.AppendLine("		,@BomID             ");
                sqlRoute.AppendLine("		,@ProductID         ");
                sqlRoute.AppendLine("		,@UsedStatus        ");
                sqlRoute.AppendLine("		,@Creator           ");
                sqlRoute.AppendLine("		,@CreateDate        ");
                sqlRoute.AppendLine("		,@Remark            ");
                sqlRoute.AppendLine("		,getdate()          ");
                sqlRoute.AppendLine("		,'" + loginUserID + "')       ");
                sqlRoute.AppendLine("set @ID=@@IDENTITY         ");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlRoute.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@RouteNo", model.RouteNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@RouteName", model.RouteName));
                comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
                comm.Parameters.Add(SqlHelper.GetParameter("@Verson", model.Verson));
                comm.Parameters.Add(SqlHelper.GetParameter("@IsMainTech", model.IsMainTech));
                comm.Parameters.Add(SqlHelper.GetParameter("@BomID", model.BomID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
                comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                listADD.Add(comm);
                #endregion
                

                #region 工艺路线明细
                try
                {

                    if (!String.IsNullOrEmpty(model.DetProcSequNumber) && !String.IsNullOrEmpty(model.DetSequID) && !String.IsNullOrEmpty(model.DetWCID) && !String.IsNullOrEmpty(model.DetReadyTime) && !String.IsNullOrEmpty(model.DetRunTime))
                    {
                        #region 工艺路线明细添加语句
                        string[] detSequNumber = model.DetProcSequNumber.Split(',');
                        string[] detSequID = model.DetSequID.Split(',');
                        string[] detWCID = model.DetWCID.Split(',');
                        string[] detTimeUnit = model.DetTimeUnit.Split(',');
                        string[] detReadyTime = model.DetReadyTime.Split(',');
                        string[] detRunTime = model.DetRunTime.Split(',');
                        string[] detIsCharge = model.DetIsCharge.Split(',');
                        string[] detIsoutsource = model.DetIsoutsource.Split(',');
                        string[] detCheckWay = model.DetCheckWay.Split(',');
                        string[] detTimeWage = model.DetTimeWage.Split(',');
                        string[] detPieceWage = model.DetPieceWage.Split(',');

                        //页面上这些字段都是必填，数组的长度必须是相同的
                        if (detSequNumber.Length >= 1)
                        {
                            for (int i = 0; i < detSequNumber.Length; i++)
                            {
                                System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                cmdsql.AppendLine("Insert into officedba.TechnicsRoutingDetail (CompanyCD,RouteNo,ProcSequNumber,SequID,WCID,TimeUnit,ReadyTime,RunTime,IsCharge,Isoutsource,CheckWay,TimeWage,PieceWage,UsedStatus)");
                                cmdsql.AppendLine(" Values(@CompanyCD");
                                cmdsql.AppendLine("            ,@RouteNo");
                                cmdsql.AppendLine("            ,@ProcSequNumber");
                                cmdsql.AppendLine("            ,@SequID");
                                cmdsql.AppendLine("            ,@WCID");
                                cmdsql.AppendLine("            ,@TimeUnit");
                                cmdsql.AppendLine("            ,@ReadyTime");
                                cmdsql.AppendLine("            ,@RunTime");
                                cmdsql.AppendLine("            ,@IsCharge");
                                cmdsql.AppendLine("            ,@Isoutsource");
                                cmdsql.AppendLine("            ,@CheckWay");
                                cmdsql.AppendLine("            ,@TimeWage");
                                cmdsql.AppendLine("            ,@PieceWage");
                                cmdsql.AppendLine("            ,1)");

                                SqlCommand commDet = new SqlCommand();
                                commDet.CommandText = cmdsql.ToString();
                                commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@RouteNo", model.RouteNo));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@ProcSequNumber", detSequNumber[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@SequID", detSequID[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@WCID", detWCID[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@TimeUnit", detTimeUnit[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@ReadyTime", detReadyTime[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@RunTime", detRunTime[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@IsCharge", detIsCharge[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@Isoutsource", detIsoutsource[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@CheckWay", detCheckWay[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@TimeWage", detTimeWage[i].ToString()));
                                commDet.Parameters.Add(SqlHelper.GetParameter("@PieceWage", detPieceWage[i].ToString()));
                                listADD.Add(commDet);
                            }
                        }
                        #endregion  
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
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


        #region 修改：修改工艺路线
        /// <summary>
        /// 修改工艺路线
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool UpdateTechnicsRouting(string[] sql)
        {
            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 修改标准工序
        /// <summary>
        /// 修改标准工序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateTechnicsRouting(TechnicsRoutingModel model,string loginUserID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }


            #region 工序更新SQL语句
            StringBuilder sqlRoute = new StringBuilder();
            sqlRoute.AppendLine(" UPDATE officedba.TechnicsRouting SET          ");
            sqlRoute.AppendLine(" RouteName         =@RouteName,                ");
            sqlRoute.AppendLine(" PYShort           =@PYShort,                  ");
            sqlRoute.AppendLine(" Verson            =@Verson,                   ");
            sqlRoute.AppendLine(" IsMainTech        =@IsMainTech,               ");
            sqlRoute.AppendLine(" BomID             =@BomID,                    ");
            sqlRoute.AppendLine(" ProductID         =@ProductID,                ");
            sqlRoute.AppendLine(" UsedStatus        =@UsedStatus,               ");
            sqlRoute.AppendLine(" Remark            =@Remark,                   ");
            sqlRoute.AppendLine(" ModifiedDate      =getdate(),                 ");
            sqlRoute.AppendLine(" ModifiedUserID    = '" + loginUserID + "'     ");
            sqlRoute.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID        ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlRoute.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@RouteName", model.RouteName));
            comm.Parameters.Add(SqlHelper.GetParameter("@PYShort", model.PYShort));
            comm.Parameters.Add(SqlHelper.GetParameter("@Verson", model.Verson));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsMainTech", model.IsMainTech));
            comm.Parameters.Add(SqlHelper.GetParameter("@BomID", model.BomID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));

            listADD.Add(comm);

            #endregion


            #region 工艺路线明细处理

            #region 删除工艺路线明细
            StringBuilder sqlProductDel = new StringBuilder();
            sqlProductDel.AppendLine("delete from officedba.TechnicsRoutingDetail ");
            sqlProductDel.AppendLine("where CompanyCD=@CompanyCD");
            sqlProductDel.AppendLine("and RouteNo=(");
            sqlProductDel.AppendLine("				select top 1 RouteNo from officedba.TechnicsRouting where CompanyCD=@CompanyCD and ID=@ID");
            sqlProductDel.AppendLine("			    )");

            SqlCommand commProductDel = new SqlCommand();
            commProductDel.CommandText = sqlProductDel.ToString();
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            commProductDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            listADD.Add(commProductDel);
            #endregion


            #region 工艺路线明细
            try
            {

                if (!String.IsNullOrEmpty(model.DetProcSequNumber) && !String.IsNullOrEmpty(model.DetSequID) && !String.IsNullOrEmpty(model.DetWCID) && !String.IsNullOrEmpty(model.DetReadyTime) && !String.IsNullOrEmpty(model.DetRunTime))
                {
                    #region 工艺路线明细添加语句
                    string[] detSequNumber = model.DetProcSequNumber.Split(',');
                    string[] detSequID = model.DetSequID.Split(',');
                    string[] detWCID = model.DetWCID.Split(',');
                    string[] detTimeUnit = model.DetTimeUnit.Split(',');
                    string[] detReadyTime = model.DetReadyTime.Split(',');
                    string[] detRunTime = model.DetRunTime.Split(',');
                    string[] detIsCharge = model.DetIsCharge.Split(',');
                    string[] detIsoutsource = model.DetIsoutsource.Split(',');
                    string[] detCheckWay = model.DetCheckWay.Split(',');
                    string[] detTimeWage = model.DetTimeWage.Split(',');
                    string[] detPieceWage = model.DetPieceWage.Split(',');

                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (detSequNumber.Length >= 1)
                    {
                        for (int i = 0; i < detSequNumber.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("Insert into officedba.TechnicsRoutingDetail (CompanyCD,RouteNo,ProcSequNumber,SequID,WCID,TimeUnit,ReadyTime,RunTime,IsCharge,Isoutsource,CheckWay,TimeWage,PieceWage,UsedStatus)");
                            cmdsql.AppendLine(" Values(@CompanyCD");
                            cmdsql.AppendLine("            ,@RouteNo");
                            cmdsql.AppendLine("            ,@ProcSequNumber");
                            cmdsql.AppendLine("            ,@SequID");
                            cmdsql.AppendLine("            ,@WCID");
                            cmdsql.AppendLine("            ,@TimeUnit");
                            cmdsql.AppendLine("            ,@ReadyTime");
                            cmdsql.AppendLine("            ,@RunTime");
                            cmdsql.AppendLine("            ,@IsCharge");
                            cmdsql.AppendLine("            ,@Isoutsource");
                            cmdsql.AppendLine("            ,@CheckWay");
                            cmdsql.AppendLine("            ,@TimeWage");
                            cmdsql.AppendLine("            ,@PieceWage");
                            cmdsql.AppendLine("            ,1)");

                            SqlCommand commDet = new SqlCommand();
                            commDet.CommandText = cmdsql.ToString();
                            commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@RouteNo", model.RouteNo));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@ProcSequNumber", detSequNumber[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@SequID", detSequID[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@WCID", detWCID[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@TimeUnit", detTimeUnit[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@ReadyTime", detReadyTime[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@RunTime", detRunTime[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@IsCharge", detIsCharge[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@Isoutsource", detIsoutsource[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@CheckWay", detCheckWay[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@TimeWage", detTimeWage[i].ToString()));
                            commDet.Parameters.Add(SqlHelper.GetParameter("@PieceWage", detPieceWage[i].ToString()));
                            listADD.Add(commDet);
                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion


        #region 获取工艺路线信息
        /// <summary>
        /// 获取工艺路线信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTechnicsRoutingInfo(TechnicsRoutingModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("Select * From (");
            infoSql.AppendLine("select	a.CompanyCD,a.ID,a.RouteNo,a.RouteName,");
            infoSql.AppendLine("		a.PYShort,a.Verson,a.IsMainTech,a.BomID,g.BomNo,");
            infoSql.AppendLine("		a.ProductID,a.UsedStatus,a.Creator,");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("		a.Remark,b.ProductName,f.EmployeeName ");
            infoSql.AppendLine("from officedba.TechnicsRouting a ");
            infoSql.AppendLine("left join officedba.ProductInfo b on b.ID=a.ProductID ");
            infoSql.AppendLine("left join officedba.EmployeeInfo f on f.ID=a.Creator");
            infoSql.AppendLine("left join officedba.Bom g on a.BomID=g.ID");
            infoSql.AppendLine(") as info");
            infoSql.AppendLine("Where ID=@ID");

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

        #region 获取工艺路线明细信息
        /// <summary>
        /// 获取工艺路线明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetTechnicsRoutingDetailInfo(TechnicsRoutingModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("	select	c.CompanyCD,c.ID as DetailID,c.ProcSequNumber,c.SequID,");
            infoSql.AppendLine("			c.RouteNo,c.WCID,c.TimeUnit,c.Isoutsource,c.CheckWay,");
            infoSql.AppendLine("			Convert(numeric(14,"+userInfo.SelPoint+"),c.TimeWage) as TimeWage,");
            infoSql.AppendLine("			Convert(numeric(14,"+userInfo.SelPoint+"),c.PieceWage) as PieceWage,");
            infoSql.AppendLine("			c.Remark as DetailRemark,c.ReadyTime,c.RunTime,");
            infoSql.AppendLine("			c.IsCharge,d.WCName,e.SequName ");
            infoSql.AppendLine("	from officedba.TechnicsRoutingDetail c");
            infoSql.AppendLine("	left join officedba.WorkCenter d on d.ID=c.WCID");
            infoSql.AppendLine("	left join officedba.StandardSequ e on e.ID=c.SequID");
            infoSql.AppendLine(")as info");
            infoSql.AppendLine("where CompanyCD=@CompanyCD and RouteNo=(select top 1 RouteNo from officedba.TechnicsRouting where ID=@ID)");

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

        #region 删除：工艺路线
        /// <summary>
        /// 删除工艺路线（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteTechnicsRouting(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.TechnicsRoutingDetail where CompanyCD=@CompanyCD and RouteNo=(select top 1 RouteNo from officedba.TechnicsRouting where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.TechnicsRouting where CompanyCD=@CompanyCD and ID=@ID");

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
