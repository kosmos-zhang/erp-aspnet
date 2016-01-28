/**********************************************
 * 类作用：   主生产计划单数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/03
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
    public class MasterProductScheduleDBHelper
    {
        

        #region 主生产计划单插入
        /// <summary>
        /// 主生产计划单插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMasterProductSchedule(MasterProductScheduleModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            ArrayList listADD = new ArrayList();
            bool result = false;

            #region 传参

            #region  主生产计划单添加SQL语句
            StringBuilder sqlMaster = new StringBuilder();
            sqlMaster.AppendLine("INSERT INTO officedba. MasterProductSchedule");
            sqlMaster.AppendLine("(CompanyCD,PlanNo,Subject,Principal,DeptID,CountTotal,Creator,CreateDate,BillStatus,Remark,ModifiedDate,ModifiedUserID)");
            sqlMaster.AppendLine("VALUES                  ");
            sqlMaster.AppendLine("		(@CompanyCD");
            sqlMaster.AppendLine("		,@PlanNo");
            sqlMaster.AppendLine("		,@Subject");
            sqlMaster.AppendLine("		,@Principal");
            sqlMaster.AppendLine("		,@DeptID");
            sqlMaster.AppendLine("		,@CountTotal");
            sqlMaster.AppendLine("		,@Creator");
            sqlMaster.AppendLine("		,@CreateDate");
            sqlMaster.AppendLine("		,@BillStatus");
            sqlMaster.AppendLine("		,@Remark");
            sqlMaster.AppendLine("		,getdate()     ");
            sqlMaster.AppendLine("		,'" + loginUserID + "')       ");
            sqlMaster.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlMaster.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));



            listADD.Add(comm);
            #endregion

            #region 主生产计划单明细
            try
            {
                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                if (!String.IsNullOrEmpty(model.DetSortNo) && !String.IsNullOrEmpty(model.DetProductID))
                {
                    #region 主生产计划单明细添加语句
                    string[] detSortNo = model.DetSortNo.Split(',');
                    string[] detProductID = model.DetProductID.Split(',');
                    string[] detUnitID = model.DetUnitID.Split(',');
                    string[] detStartDate = model.DetStartDate.Split(',');
                    string[] detEndDate = model.DetEndDate.Split(',');
                    string[] detFromBiilID = model.DetFromBillID.Split(',');
                    string[] detFromBillNo = model.DetFromBillNo.Split(',');
                    string[] detFromLineNo = model.DetFromLineNo.Split(',');
                    string[] detProductCount = model.DetProductCount.Split(',');
                    string[] detProduceCount = model.DetProduceCount.Split(',');
                    string[] detRemark = model.DetRemark.Split(',');
                    string[] detUsedUnitID = model.DetUsedUnitID.Split(',');
                    string[] detUsedUnitCount = model.DetUsedUnitCount.Split(',');
                    string[] detExRate = model.DetExRate.Split(',');



                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (detSortNo.Length >= 1)
                    {
                        for (int i = 0; i < detSortNo.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("insert into officedba. MasterProductScheduleDetail");
                            cmdsql.AppendLine("(CompanyCD,");
                            cmdsql.AppendLine("PlanNo,");
                            cmdsql.AppendLine("SortNo,");
                            cmdsql.AppendLine("ProductID,");
                            cmdsql.AppendLine("UnitID,");
                            if (detProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(detProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("ProductCount,");
                                }
                            }
                            
                            cmdsql.AppendLine("ProduceCount,");
                            cmdsql.AppendLine("StartDate,");
                            cmdsql.AppendLine("EndDate,");
                            cmdsql.AppendLine("FromBillID,");
                            cmdsql.AppendLine("FromBillNo,");
                            cmdsql.AppendLine("FromLineNo,");
                            cmdsql.AppendLine("Remark,");
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("UsedUnitID,");
                                cmdsql.AppendLine("UsedUnitCount,");
                                cmdsql.AppendLine("ExRate,");
                            }
                            cmdsql.AppendLine("ModifiedUserID)");
                            cmdsql.AppendLine(" Values(@CompanyCD");
                            cmdsql.AppendLine("            ,@PlanNo");
                            cmdsql.AppendLine("            ,@SortNo");
                            cmdsql.AppendLine("            ,@ProductID");
                            cmdsql.AppendLine("            ,@UnitID");
                            if (detProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(detProductCount[i].ToString().Trim()))
                                {
                                    cmdsql.AppendLine("            ,@ProductCount");
                                }
                            }
                            
                            cmdsql.AppendLine("            ,@ProduceCount");
                            cmdsql.AppendLine("            ,@StartDate");
                            cmdsql.AppendLine("            ,@EndDate");
                            cmdsql.AppendLine("            ,@FromBillID");
                            cmdsql.AppendLine("            ,@FromBillNo");
                            cmdsql.AppendLine("            ,@FromLineNo");
                            cmdsql.AppendLine("            ,@Remark");
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("            ,@UsedUnitID");
                                cmdsql.AppendLine("            ,@UsedUnitCount");
                                cmdsql.AppendLine("            ,@ExRate");
                            }
                            cmdsql.AppendLine("            ,@ModifiedUserID)");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                            comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", detSortNo[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", detProductID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", detUnitID[i].ToString()));
                            if (detProductCount[i].ToString().Length > 0)
                            {
                                if (!string.IsNullOrEmpty(detProductCount[i].ToString().Trim()))
                                {
                                    comms.Parameters.Add(SqlHelper.GetParameter("@ProductCount", detProductCount[i].ToString()));
                                }
                            }
                            
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProduceCount", detProduceCount[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@StartDate", detStartDate[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@EndDate", detEndDate[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detFromBiilID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", detFromBillNo[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", detFromLineNo[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@Remark", detRemark[i].ToString()));
                            if (userInfo.IsMoreUnit)
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", detUsedUnitID[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", detUsedUnitCount[i].ToString()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", detExRate[i].ToString()));
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                            listADD.Add(comms);
                        }
                    }
                    #endregion
                }
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
            #endregion
           

            #endregion

        }
        #endregion

        #region 主生产计划单详细信息
        /// <summary>
        /// 主生产计划单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleDetailInfo(MasterProductScheduleModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("			select	a.CompanyCD,a.ID,a.PlanNo,a.DeptID,a.Subject,a.Principal,");
            infoSql.AppendLine("					Convert(numeric(22,"+userInfo.SelPoint+"),a.CountTotal) as CountTotal,a.Creator,");
            infoSql.AppendLine("					isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,");
            infoSql.AppendLine("					a.BillStatus,a.Confirmor,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("					a.Closer,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,a.Remark,b.EmployeeName as CreatorReal,");
            infoSql.AppendLine("                    a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("					c.EmployeeName as PrincipalReal ,d.EmployeeName as ConfirmorReal,e.EmployeeName as CloserReal,f.DeptName ");
            infoSql.AppendLine("			from officedba.MasterProductSchedule as a ");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS B ON a.CompanyCD=@CompanyCD and a.Creator=b.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS c on a.Principal=c.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS d on a.Confirmor=d.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS e on a.Closer=e.ID");
            infoSql.AppendLine("			left join officedba.DeptInfo As f on f.ID=a.DeptID");
            infoSql.AppendLine("			) as  m ");
            infoSql.AppendLine("left outer join (");
            infoSql.AppendLine("			select	d.ID as DetailID,d.PlanNo,d.SortNo,d.ProductID,d.UnitID,");
            infoSql.AppendLine("					Convert(numeric(14,"+userInfo.SelPoint+"),d.ProductCount) as ProductCount,");
            infoSql.AppendLine("					Convert(numeric(14," + userInfo.SelPoint + "),d.UsedUnitCount) as UsedUnitCount,");
            infoSql.AppendLine("					isnull( CONVERT(CHAR(10), d.StartDate, 23),'') as StartDate,");
            infoSql.AppendLine("					isnull( CONVERT(CHAR(10), d.EndDate, 23),'') as EndDate,");
            infoSql.AppendLine("					d.FromBillID,d.FromBillNo,d.FromLineNo,Convert(numeric(14,"+userInfo.SelPoint+"),d.PlanCount) as PlanCount,");
            infoSql.AppendLine("					Convert(numeric(14,"+userInfo.SelPoint+"),d.ProcessedCount) as ProcessedCount,");
            infoSql.AppendLine("					Convert(numeric(14,"+userInfo.SelPoint+"),d.ProduceCount) as ProduceCount,d.Remark as DetailRemark,");
            infoSql.AppendLine("					p.ProductName,p.ProdNo,p.Specification,t.CodeName");
            infoSql.AppendLine("					from officedba. MasterProductScheduleDetail d");
            infoSql.AppendLine("					inner join officedba.ProductInfo p on p.ID=d.ProductID");
            infoSql.AppendLine("					inner join officedba.CodeUnitType t on t.ID=p.UnitID where d.CompanyCD=@CompanyCD");
            infoSql.AppendLine(") as info ");
            infoSql.AppendLine("on info.PlanNo=m.PlanNo");
            infoSql.AppendLine("where m.ID=@ID");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 主生产计划单主表信息
        /// <summary>
        /// 主生产计划单主表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleInfo(MasterProductScheduleModel model)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from");
            infoSql.AppendLine("(");
            infoSql.AppendLine("			select	a.CompanyCD,a.ID,a.PlanNo,a.DeptID,a.Subject,a.Principal,");
            infoSql.AppendLine("					Convert(numeric(22," + userInfo.SelPoint+ "),a.CountTotal) as CountTotal,a.Creator,");
            infoSql.AppendLine("					isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID,");
            infoSql.AppendLine("                    case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("					a.BillStatus,a.Confirmor,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("					a.Closer,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,a.Remark,b.EmployeeName as CreatorReal,");
            infoSql.AppendLine("					c.EmployeeName as PrincipalReal ,d.EmployeeName as ConfirmorReal,e.EmployeeName as CloserReal,f.DeptName, ");
            infoSql.AppendLine("					a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10 ");
            infoSql.AppendLine("			from officedba.MasterProductSchedule as a");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS B ON a.Creator=b.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS c on a.Principal=c.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS d on a.Confirmor=d.ID");
            infoSql.AppendLine("			left join  officedba.EmployeeInfo AS e on a.Closer=e.ID");
            infoSql.AppendLine("			left join officedba.DeptInfo As f on f.ID=a.DeptID");
            infoSql.AppendLine(")as info");
            infoSql.AppendLine("where ID=@ID");


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

        #region 主生产计划单明细详细信息
        /// <summary>
        /// 主生产计划单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleDetailInfoList(MasterProductScheduleModel model)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select	a.ID as DetailID,a.CompanyCD,a.PlanNo,a.SortNo,a.ProductID,b.ProdNo,b.ProductName,b.Specification,a.UsedUnitID,d.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount,a.ExRate,a.UnitID,c.CodeName as CodeName,");
            detSql.AppendLine("		Convert(numeric(14,"+userInfo.SelPoint+"),a.ProductCount) as ProductCount,Convert(numeric(14," + userInfo.SelPoint + "),a.ProduceCount) as ProduceCount,");
            detSql.AppendLine("		isnull( CONVERT(CHAR(10), a.StartDate, 23),'') as StartDate,isnull( CONVERT(CHAR(10), a.EndDate, 23),'') as EndDate,");
            detSql.AppendLine("		a.FromBillID,a.FromBillNo,a.FromLineNo,Convert(numeric(14," + userInfo.SelPoint + "),a.PlanCount) as PlanCount,Convert(numeric(14," + userInfo.SelPoint + "),a.ProcessedCount) as ProcessedCount,");
            detSql.AppendLine("		a.Remark as DetailRemark ");
            detSql.AppendLine("from officedba.MasterProductScheduleDetail a");
            detSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("left join officedba.CodeUnitType c on a.UnitID=c.ID");
            detSql.AppendLine("left join officedba.CodeUnitType d on a.UsedUnitID=d.ID");
            detSql.AppendLine("where a.CompanyCD=@CompanyCD");
            detSql.AppendLine("and a.PlanNo=(select top 1 PlanNo from officedba.MasterProductSchedule where ID=@ID)");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 修改主生产计划单
        /// <summary>
        /// 修改退料单和各明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool UpdateMasterproductSchedule(MasterProductScheduleModel model, Hashtable htExtAttr, string DetailUpdate,string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  主生产计划单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine(" UPDATE officedba.MasterProductSchedule SET ");
            sqlEdit.AppendLine(" Subject       =@Subject,");
            sqlEdit.AppendLine(" Principal     =@Principal,");
            sqlEdit.AppendLine(" CountTotal    =@CountTotal,");
            sqlEdit.AppendLine(" Remark        =@Remark,");
            sqlEdit.AppendLine(" DeptID        =@DeptID,");
            sqlEdit.AppendLine(" ModifiedDate   =getdate(),");
            sqlEdit.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
            sqlEdit.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");



            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));

            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region 主生产计划单明细处理

            if (DetailUpdate.Length > 0)
            {
                string[] updateID = DetailUpdate.Split(',');
                if (!string.IsNullOrEmpty(DetailUpdate) && updateID.Length > 0)
                {
                    #region 有更新记录的
                    //先删除不在工序明细中的ID
                    //更新工序明细中的ID
                    //添加其余工序明细信息
                    //先删除不在工序明细中的ID
                        #region 先删除不在主生产计划明细中的ID
                        StringBuilder sqlDel = new StringBuilder();
                        sqlDel.AppendLine("Delete From officedba.MasterProductScheduleDetail where CompanyCD=@CompanyCD and PlanNo=@PlanNo and  ID not in(" + DetailUpdate + ")");



                        SqlCommand commDel = new SqlCommand();
                        commDel.CommandText = sqlDel.ToString();
                        commDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        commDel.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                        listADD.Add(commDel);
                        #endregion

                        for (int i = 0; i < updateID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            int intUpdateID = int.Parse(updateID[i].ToString());
                            if (intUpdateID > 0)
                            {

                                #region 更新主生产计划明细
                                cmdsql.AppendLine("Update officedba.MasterProductScheduleDetail                 ");
                                cmdsql.AppendLine(" Set                                                         ");
                                cmdsql.AppendLine(" SortNo=@SortNo,                                             ");
                                cmdsql.AppendLine(" ProductID=@ProductID,                                       ");
                                cmdsql.AppendLine(" UnitID=@UnitID,                                             ");
                                if (model.DetProductCount.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(" ProductCount=@ProductCount,                                 ");
                                }
                                else
                                {
                                    cmdsql.AppendLine(" ProductCount=null,                                 ");
                                }
                                
                                cmdsql.AppendLine(" ProduceCount=@ProduceCount,                                 ");
                                cmdsql.AppendLine(" StartDate=@StartDate,                                       ");
                                cmdsql.AppendLine(" EndDate=@EndDate,                                           ");
                                if (model.DetFromBillID.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(" FromBillID=@FromBillID,                                 ");
                                }
                                if (model.DetFromBillNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(" FromBillNo=@FromBillNo,                                 ");
                                }

                                if (model.DetFromLineNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(" FromLineNo=@FromLineNo,                                 ");
                                }

                                cmdsql.AppendLine(" Remark=@Remark,");
                                if (userInfo.IsMoreUnit)
                                {
                                    cmdsql.AppendLine(" UsedUnitID=@UsedUnitID,");
                                    cmdsql.AppendLine(" UsedUnitCount=@UsedUnitCount,");
                                    cmdsql.AppendLine(" ExRate=@ExRate,");
                                }
                                cmdsql.AppendLine(" ModifiedDate= getdate(),                                    ");
                                cmdsql.AppendLine(" ModifiedUserID='" + loginUserID + "'                        ");
                                cmdsql.AppendLine("  Where CompanyCD=@CompanyCD and PlanNo=@PlanNo and ID=@ID   ");

                                SqlCommand commSql = new SqlCommand();
                                commSql.CommandText = cmdsql.ToString();
                                commSql.Parameters.Add(SqlHelper.GetParameter("@ID", intUpdateID));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@SortNo", model.DetSortNo.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.DetProductID.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@UnitID", model.DetUnitID.Split(',')[i].ToString()));
                                if (model.DetProductCount.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@ProductCount", model.DetProductCount.Split(',')[i].ToString()));
                                }
                                
                                commSql.Parameters.Add(SqlHelper.GetParameter("@ProduceCount", model.DetProduceCount.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@StartDate", model.DetStartDate.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@EndDate", model.DetEndDate.Split(',')[i].ToString()));
                                if (model.DetFromBillID.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.DetFromBillID.Split(',')[i].ToString().Trim()));
                                }
                                if (model.DetFromBillNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", model.DetFromBillNo.Split(',')[i].ToString().Trim()));
                                }
                                if (model.DetFromLineNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", model.DetFromLineNo.Split(',')[i].ToString().Trim()));
                                }
                                commSql.Parameters.Add(SqlHelper.GetParameter("@Remark", model.DetRemark.Split(',')[i].ToString().Trim()));
                                if (userInfo.IsMoreUnit)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", model.DetUsedUnitID.Split(',')[i].ToString().Trim()));
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", model.DetUsedUnitCount.Split(',')[i].ToString().Trim()));
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@ExRate", model.DetExRate.Split(',')[i].ToString().Trim()));
                                }
                                listADD.Add(commSql);

                                #endregion

                            }
                            else
                            {
                                #region 添加主生产计划明细
                                //添加
                                cmdsql.AppendLine(" Insert into officedba.MasterProductScheduleDetail(  CompanyCD,      ");
                                cmdsql.AppendLine("                                                     PlanNo,         ");
                                cmdsql.AppendLine("                                                     SortNo,         ");
                                cmdsql.AppendLine("                                                     ProductID,      ");
                                cmdsql.AppendLine("                                                     UnitID,         ");
                                if (model.DetProductCount.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine("                                                     ProductCount,   ");
                                }
                                cmdsql.AppendLine("                                                     ProduceCount,   ");
                                cmdsql.AppendLine("                                                     StartDate,      ");
                                cmdsql.AppendLine("                                                     EndDate,        ");
                                if (model.DetFromBillID.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine("                                                     FromBillID,     ");
                                }
                                if (model.DetFromBillNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine("                                                     FromBillNo,     ");
                                }
                                if (model.DetFromLineNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine("                                                     FromLineNo,     ");
                                }
                                cmdsql.AppendLine("                                                     Remark,         ");
                                if (userInfo.IsMoreUnit)
                                {
                                    cmdsql.AppendLine("                                                     UsedUnitID,   ");
                                    cmdsql.AppendLine("                                                     UsedUnitCount,   ");
                                    cmdsql.AppendLine("                                                     ExRate,   ");
                                }
                                cmdsql.AppendLine("                                                     ModifiedDate,   ");
                                cmdsql.AppendLine("                                                     ModifiedUserID) ");
                                cmdsql.AppendLine(" Values( @CompanyCD              ");
                                cmdsql.AppendLine(",        @PlanNo                 ");
                                cmdsql.AppendLine(",        @SortNo                 ");
                                cmdsql.AppendLine(" ,       @ProductID              ");
                                cmdsql.AppendLine(" ,       @UnitID                 ");
                                if (model.DetProductCount.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(" ,       @ProductCount           ");
                                }
                                cmdsql.AppendLine(",        @ProduceCount           ");
                                cmdsql.AppendLine(",        @StartDate              ");
                                cmdsql.AppendLine(",        @EndDate                ");
                                if (model.DetFromBillID.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(",        @FromBillID             ");
                                }
                                if (model.DetFromBillNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(",        @FromBillNo             ");
                                }
                                if (model.DetFromLineNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    cmdsql.AppendLine(",        @FromLineNo             ");
                                } 
                                cmdsql.AppendLine(",        @Remark                 ");
                                if (userInfo.IsMoreUnit)
                                {
                                    cmdsql.AppendLine(",        @UsedUnitID                 ");
                                    cmdsql.AppendLine(",        @UsedUnitCount                 ");
                                    cmdsql.AppendLine(",        @ExRate                 ");
                                }
                                cmdsql.AppendLine(",        getdate(),              ");
                                cmdsql.AppendLine("     '" + loginUserID + "')      ");


                                SqlCommand commSql = new SqlCommand();
                                commSql.CommandText = cmdsql.ToString();
                                commSql.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@SortNo", model.DetSortNo.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.DetProductID.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@UnitID", model.DetUnitID.Split(',')[i].ToString()));
                                if (model.DetProductCount.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@ProductCount", model.DetProductCount.Split(',')[i].ToString()));
                                }
                                commSql.Parameters.Add(SqlHelper.GetParameter("@ProduceCount", model.DetProduceCount.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@StartDate", model.DetStartDate.Split(',')[i].ToString()));
                                commSql.Parameters.Add(SqlHelper.GetParameter("@EndDate", model.DetEndDate.Split(',')[i].ToString()));
                                if (model.DetFromBillID.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.DetFromBillID.Split(',')[i].ToString().Trim()));
                                }
                                if (model.DetFromBillNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromBillNo", model.DetFromBillNo.Split(',')[i].ToString().Trim()));
                                }
                                if (model.DetFromLineNo.Split(',')[i].ToString().Trim().Length > 0)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", model.DetFromLineNo.Split(',')[i].ToString().Trim()));
                                }
                                
                                commSql.Parameters.Add(SqlHelper.GetParameter("@Remark", model.DetRemark.Split(',')[i].ToString().Trim()));
                                if (userInfo.IsMoreUnit)
                                {
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", model.DetUsedUnitID.Split(',')[i].ToString().Trim()));
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", model.DetUsedUnitCount.Split(',')[i].ToString().Trim()));
                                    commSql.Parameters.Add(SqlHelper.GetParameter("@ExRate", model.DetExRate.Split(',')[i].ToString().Trim()));
                                }
                                listADD.Add(commSql);

                                #endregion
                            }
                        }
                    #endregion

                }
                else
                {
                    #region 没有要更新,删除所有明细

                    StringBuilder sqlDelAll = new StringBuilder();
                    sqlDelAll.AppendLine("Delete From officedba.MasterProductScheduleDetail where CompanyCD=@CompanyCD and PlanNo=@PlanNo ");

                    SqlCommand commDelAll = new SqlCommand();
                    commDelAll.CommandText = sqlDelAll.ToString();
                    commDelAll.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commDelAll.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                    listADD.Add(commDelAll);

                    #endregion
                }
            }
            else
            {
                #region 无明细记录
                StringBuilder sqlDelAll = new StringBuilder();
                sqlDelAll.AppendLine("Delete From officedba.MasterProductScheduleDetail where CompanyCD=@CompanyCD and PlanNo=@PlanNo ");

                SqlCommand commDelAll = new SqlCommand();
                commDelAll.CommandText = sqlDelAll.ToString();
                commDelAll.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDelAll.Parameters.Add(SqlHelper.GetParameter("@PlanNo", model.PlanNo));
                listADD.Add(commDelAll);
                #endregion
            }
            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 修改：确认或结单
        /// <summary>
        /// 确认或结单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="isConfirm"></param>
        /// <returns></returns>
        public static bool ConfirmOrCompleteMasterProductSchedule(MasterProductScheduleModel model, string loginUserID, int OperateType)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            if (OperateType == 1)
            {
                DataTable dbMaster = GetMasterProductScheduleDetailInfo(model);
                if (dbMaster.Rows.Count > 0)
                {
                    int BillStatus = int.Parse(dbMaster.Rows[0]["BillStatus"].ToString());
                    if (BillStatus == 1)
                    {
                        //防止多用户登录该页面，同时操作确认操作，需要判断当前单据是否还是制单状态
                        //单据确认后，同时更新对应销售订单中对应明细的计划生产数量。
                        #region 更新对应销售订单中的计划生产数量
                        DataTable dtDetail = new DataTable();
                        dtDetail = GetMasterProductScheduleDetailInfoList(model);
                        if (dtDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDetail.Rows.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(dtDetail.Rows[i]["FromBillID"].ToString()))
                                {
                                    int FromBillID = int.Parse(dtDetail.Rows[i]["FromBillID"].ToString());
                                    int FromLineNo = int.Parse(dtDetail.Rows[i]["FromLineNo"].ToString());

                                    Decimal ProduceCount = Decimal.Round(Decimal.Parse(dtDetail.Rows[i]["ProduceCount"].ToString()));
                                    if (userInfo.IsMoreUnit)
                                    {
                                        ProduceCount = Decimal.Round(Decimal.Parse(dtDetail.Rows[i]["UsedUnitCount"].ToString()));
                                    }

                                    if (FromBillID > 0 && FromLineNo > 0)
                                    {
                                        //更新
                                        //   update officedba.SellOrderDetail set PlanProductCount=isnull(PlanProductCount,0)+@ProduceCount 
                                        //   where CompanyCD=@CompanyCD and ID=@FromBillID and SortNo=@FromLineNo
                                        #region 更新销售订单明细的计划生产数量
                                        StringBuilder sqlSell = new StringBuilder();
                                        sqlSell.AppendLine("update officedba.SellOrderDetail set PlanProductCount=isnull(PlanProductCount,0)+@ProduceCount where CompanyCD=@CompanyCD and SortNo=@FromLineNo and OrderNo=(select OrderNo from officedba.SellOrder where  ID=@FromBillID)");

                                        SqlCommand commSell = new SqlCommand();
                                        commSell.CommandText = sqlSell.ToString();
                                        commSell.Parameters.Add(SqlHelper.GetParameter("@ProduceCount", ProduceCount));
                                        commSell.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                        commSell.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));
                                        commSell.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo));

                                        listADD.Add(commSell);
                                        #endregion

                                    }
                                }
                            }
                        }
                        #endregion

                        #region 更新确认人
                        StringBuilder sqlMaster = new StringBuilder();
                        sqlMaster.AppendLine(" UPDATE officedba.MasterProductSchedule SET");
                        sqlMaster.AppendLine(" Confirmor         = @Confirmor,");
                        sqlMaster.AppendLine(" ConfirmDate        = @ConfirmDate,");
                        sqlMaster.AppendLine(" ModifiedDate   = getdate(),");
                        sqlMaster.AppendLine(" BillStatus   = 2,");
                        sqlMaster.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                        sqlMaster.AppendLine(" Where  ID=@ID");



                        SqlCommand commMaster = new SqlCommand();
                        commMaster.CommandText = sqlMaster.ToString();
                        commMaster.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                        commMaster.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                        commMaster.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));

                        listADD.Add(commMaster);
                        #endregion
                    }
                }
            }
            else if (OperateType == 2)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.MasterProductSchedule SET");
                sql.AppendLine(" Closer         = @Closer,");
                sql.AppendLine(" CloseDate   = @CloseDate,");
                sql.AppendLine(" BillStatus   = 4,");
                sql.AppendLine(" ModifiedDate   = getdate(),");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  ID=@ID");

                SqlCommand commMaster = new SqlCommand();
                commMaster.CommandText = sql.ToString();
                commMaster.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                commMaster.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                commMaster.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
                listADD.Add(commMaster);
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" update officedba.MasterProductSchedule set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine("  Where  ID=@ID");

                SqlCommand commMaster = new SqlCommand();
                commMaster.CommandText = sql.ToString();
                commMaster.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                listADD.Add(commMaster);
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool CancelConfirmOperate(MasterProductScheduleModel model,int BillTypeFlag,int BillTypeCode,string loginUserID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();

            //#region 传参
            try
            {
                 DataTable dbMaster = GetMasterProductScheduleDetailInfo(model);
                 if (dbMaster.Rows.Count > 0)
                 {
                     int BillStatus = int.Parse(dbMaster.Rows[0]["BillStatus"].ToString());
                     if (BillStatus == 2)
                     {
                         #region 撤消审批流程
                         #region 撤消审批处理逻辑描述
                         //可参见撤消审批的存储过程[FlowApproval_Update],个别的判断去掉

                         //--1.往流程任务历史记录表（officedba.FlowTaskHistory）插1条处理记录，
                         //--记录的步骤序号为0（表示返回到流程提交人环节)，审批状态为撤销审批   
                         //Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)
                         //Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())

                         //--2.更新流程任务处理表（officedba.FlowTaskList）中的流程步骤序号为0（表示返回到流程提交人环节）
                         //Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID
                         //Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID

                         //--3更新流程实例表（officedba.FlowInstance）中的流程状态为“撤销审批”
                         //Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID 
                         //Where CompanyCD=@CompanyCD 
                         //and FlowNo=@tempFlowNo 
                         //and BillTypeFlag=@BillTypeFlag 
                         //and BillTypeCode=@BillTypeCode 
                         //and BillID=@BillID
                         #endregion


                         DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
                         if (dtFlowInstance.Rows.Count > 0)
                         {
                             //提交审批了的单据
                             string FlowInstanceID = dtFlowInstance.Rows[0]["FlowInstanceID"].ToString();
                             string FlowStatus = dtFlowInstance.Rows[0]["FlowStatus"].ToString();
                             string FlowNo = dtFlowInstance.Rows[0]["FlowNo"].ToString();

                             #region 往流程任务历史记录表
                             StringBuilder sqlHis = new StringBuilder();
                             sqlHis.AppendLine("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)");
                             sqlHis.AppendLine("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");


                             SqlCommand commHis = new SqlCommand();
                             commHis.CommandText = sqlHis.ToString();
                             commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                             commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                             commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                             commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                             commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                             commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                             listADD.Add(commHis);
                             #endregion

                             #region 更新流程任务处理表
                             StringBuilder sqlTask = new StringBuilder();
                             sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                             sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                             SqlCommand commTask = new SqlCommand();
                             commTask.CommandText = sqlTask.ToString();
                             commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                             commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                             commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                             listADD.Add(commTask);
                             #endregion

                             #region 更新流程实例表
                             StringBuilder sqlIns = new StringBuilder();
                             sqlIns.AppendLine("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                             sqlIns.AppendLine("Where CompanyCD=@CompanyCD ");
                             sqlIns.AppendLine("and FlowNo=@tempFlowNo ");
                             sqlIns.AppendLine("and BillTypeFlag=@BillTypeFlag ");
                             sqlIns.AppendLine("and BillTypeCode=@BillTypeCode ");
                             sqlIns.AppendLine("and BillID=@BillID");


                             SqlCommand commIns = new SqlCommand();
                             commIns.CommandText = sqlIns.ToString();
                             commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                             commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                             commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                             commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                             commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                             commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                             listADD.Add(commIns);
                             #endregion

                         }
                         #endregion

                         #region  处理自己的业务逻辑

                         //更新销售订单中的计划生产数量

                         #region 回写销售订单数据
                         DataTable dtDetail = new DataTable();
                         dtDetail = GetMasterProductScheduleDetailInfoList(model);
                         if (dtDetail.Rows.Count > 0)
                         {
                             for (int i = 0; i < dtDetail.Rows.Count; i++)
                             {
                                 if (!string.IsNullOrEmpty(dtDetail.Rows[i]["FromBillID"].ToString()))
                                 {
                                     int FromBillID = int.Parse(dtDetail.Rows[i]["FromBillID"].ToString());
                                     int FromLineNo = int.Parse(dtDetail.Rows[i]["FromLineNo"].ToString());

                                     Decimal ProduceCount = Decimal.Round(Decimal.Parse(dtDetail.Rows[i]["ProduceCount"].ToString()));
                                     if (userInfo.IsMoreUnit)
                                     {
                                         ProduceCount = Decimal.Round(Decimal.Parse(dtDetail.Rows[i]["UsedUnitCount"].ToString()));
                                     }

                                     if (FromBillID > 0 && FromLineNo > 0)
                                     {
                                         //更新
                                         //   update officedba.SellOrderDetail set PlanProductCount=isnull(PlanProductCount,0)+@ProduceCount 
                                         //   where CompanyCD=@CompanyCD and ID=@FromBillID and SortNo=@FromLineNo
                                         #region 更新销售订单明细的计划生产数量
                                         StringBuilder sqlSell = new StringBuilder();
                                         sqlSell.AppendLine("update officedba.SellOrderDetail set PlanProductCount=isnull(PlanProductCount,0)-@ProduceCount where CompanyCD=@CompanyCD and SortNo=@FromLineNo and OrderNo=(select OrderNo from officedba.SellOrder where ID=@FromBillID)");

                                         SqlCommand commSell = new SqlCommand();
                                         commSell.CommandText = sqlSell.ToString();
                                         commSell.Parameters.Add(SqlHelper.GetParameter("@ProduceCount", ProduceCount));
                                         commSell.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                                         commSell.Parameters.Add(SqlHelper.GetParameter("@FromBillID", FromBillID));
                                         commSell.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", FromLineNo));

                                         listADD.Add(commSell);
                                         #endregion

                                     }
                                 }
                             }
                         }

                         #endregion

                         StringBuilder sqlUn = new StringBuilder();
                         sqlUn.AppendLine(" UPDATE officedba.MasterProductSchedule SET");
                         sqlUn.AppendLine(" Confirmor         = null,");
                         sqlUn.AppendLine(" ConfirmDate        = null,");
                         sqlUn.AppendLine(" ModifiedDate   = getdate(),");
                         sqlUn.AppendLine(" BillStatus   = 1,");
                         sqlUn.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                         sqlUn.AppendLine(" Where  ID=@ID");


                         SqlCommand commUn = new SqlCommand();
                         commUn.CommandText = sqlUn.ToString();
                         commUn.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                         listADD.Add(commUn);
                         #endregion

                         return SqlHelper.ExecuteTransWithArrayList(listADD);
                     }
                 }
                 return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 通过检索条件查询主生产计划单信息
        /// <summary>
        /// 查询主生产计划单信息
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleListBycondition(MasterProductScheduleModel model, int FromBillID, int FlowStatus, string BillTypeFlag, string BillTypeCode, string EFIndex,string EFDesc,int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();

            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("			select	a.CompanyCD,a.ID,a.PlanNo,a.Subject,a.Principal,Convert(numeric(22,"+userInfo.SelPoint+"),a.CountTotal) as TotalCounts,a.ModifiedDate,");
            searchSql.AppendLine("                  a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("					a.DeptID,isnull(a.Confirmor,'')as Confirmor,isnull(c.EmployeeName,'')as ConfirmorReal,a.BillStatus,isnull(d.EmployeeName,'')as PrincipalReal,");
            searchSql.AppendLine("					isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,isnull(e.DeptName,'')as DeptName,isnull(b.FlowStatus,'0')as FlowStatus,");
            searchSql.AppendLine("                  case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '变更' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatus,");
            searchSql.AppendLine("          case when b.FlowStatus=1 then '待审批' when b.FlowStatus=2 then '审批中' when b.FlowStatus=3 then '审批通过' when b.FlowStatus=4 then '审批不通过' when b.FlowStatus=5 then '撤消审批' end as strFlowStatus ");
            searchSql.AppendLine("			from officedba.MasterProductSchedule a");
            searchSql.AppendLine("			left join officedba.FlowInstance b ON a.ID=b.BillID");
            searchSql.AppendLine("			and b.BillTypeFlag=@BillTypeFlag");
            searchSql.AppendLine("			and b.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine("			and b.ID =( ");
            searchSql.AppendLine("							select  max(ID)");
            searchSql.AppendLine("							from  officedba.FlowInstance H");
            searchSql.AppendLine("							where   H.CompanyCD = A.CompanyCD");
            searchSql.AppendLine("							and H.BillID = A.ID");
            searchSql.AppendLine("							and H.BillTypeFlag =@BillTypeFlag");
            searchSql.AppendLine("							and H.BillTypeCode = @BillTypeCode)");
            searchSql.AppendLine("            			left join officedba.EmployeeInfo c on c.ID=a.Confirmor");
            searchSql.AppendLine("            			left join officedba.EmployeeInfo d on d.ID=a.Principal");
            searchSql.AppendLine("						left join officedba.DeptInfo e on a.DeptID=e.ID");
            searchSql.AppendLine(")as info");
            searchSql.AppendLine("  where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));

            //单据编号
            if (!string.IsNullOrEmpty(model.PlanNo))
            {
                searchSql.AppendLine(" and PlanNo like @PlanNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", "%" + model.PlanNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //负责人
            if (model.Principal > 0)
            {
                searchSql.AppendLine(" and Principal=@Principal ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Principal", model.Principal.ToString()));
            }
            //部门
            if (model.DeptID > 0)
            {
                searchSql.AppendLine(" and DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID.ToString()));
            }
            //销售订单
            if (FromBillID > 0)
            {
                searchSql.AppendLine(" and PlanNo in(select  PlanNo from officedba.MasterProductScheduleDetail   where FromBillID=@FromBillID group by PlanNo) ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID", FromBillID.ToString()));
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            if (FlowStatus >-1)
            {
                searchSql.AppendLine(" and FlowStatus=@FlowStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus.ToString()));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 删除：主生产计划单
        /// <summary>
        /// 删除主生产计划单（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteMasterProductSchedule(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for(int i=0;i<arrID.Length;i++)
                {
                    StringBuilder sqlMasterDet = new StringBuilder();
                    StringBuilder sqlMaster = new StringBuilder();
                    sqlMasterDet.AppendLine("delete from officedba.MasterProductScheduleDetail where CompanyCD=@CompanyCD and PlanNo=(select PlanNo from officedba.MasterProductSchedule where ID=@ID)");
                    sqlMaster.AppendLine("delete from officedba.MasterProductSchedule where ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlMasterDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlMaster.ToString();
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

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(MasterProductScheduleModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.MasterProductSchedule set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND PlanNo = @PlanNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@PlanNo", model.PlanNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

    }
}
