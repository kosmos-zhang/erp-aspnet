/**********************************************
 * 类作用：   物料需求计划数据库层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/04/22
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
    public class MRPDBHelper
    {
        #region 物料需求计划插入
        /// <summary>
        /// 物料需求计划插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMRP(MRPModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;

            //#region 传参
            try
            {
                #region  物料需求计划添加SQL语句
                StringBuilder sqlMrp = new StringBuilder();
                sqlMrp.AppendLine("insert into officedba.MRP");
                sqlMrp.AppendLine("(CompanyCD,MRPNo,Subject,PlanID,Remark,Principal,DeptID,CountTotal,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)");
                sqlMrp.AppendLine("VALUES                  ");
                sqlMrp.AppendLine("		(@CompanyCD");
                sqlMrp.AppendLine("		,@MRPNo");
                sqlMrp.AppendLine("		,@Subject");
                sqlMrp.AppendLine("		,@PlanID");
                sqlMrp.AppendLine("		,@Remark");
                sqlMrp.AppendLine("		,@Principal");
                sqlMrp.AppendLine("		,@DeptID");
                sqlMrp.AppendLine("		,@CountTotal");
                sqlMrp.AppendLine("		,@Creator");
                sqlMrp.AppendLine("		,@CreateDate");
                sqlMrp.AppendLine("		,@BillStatus");
                sqlMrp.AppendLine("		,getdate()");
                sqlMrp.AppendLine("		,'" + loginUserID + "')       ");
                sqlMrp.AppendLine("set @ID=@@IDENTITY");

                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlMrp.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@MRPNo", model.MRPNo));
                comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
                comm.Parameters.Add(SqlHelper.GetParameter("@PlanID", model.PlanID));
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
                comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
                comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
                comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

                listADD.Add(comm);
                #endregion

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion

                #region 物料需求计划明细信息添加SQL语句
                if (!String.IsNullOrEmpty(model.detPlanCount) && !String.IsNullOrEmpty(model.detPlanDate) && !String.IsNullOrEmpty(model.detMaterialSource))
                {
                    string[] dtSortNo = model.detSortNo.Split(',');
                    string[] dtProductID = model.detProductID.Split(',');
                    string[] dtUnitID = model.detUnitID.Split(',');
                    string[] dtGrossCount = model.detGrossCount.Split(',');
                    string[] dtPlanCount = model.detPlanCount.Split(',');
                    string[] dtPlanDate = model.detPlanDate.Split(',');
                    string[] dtMaterialSource = model.detMaterialSource.Split(',');
                    string[] dtRemark = model.detRemark.Split(',');
                    string[] dtFromBillID = model.detFromBillID.Split(',');
                    string[] dtUsedUnitID = model.detUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.detUsedUnitCount.Split(',');
                    string[] dtExRate = model.detExRate.Split(',');

                    //页面上这些字段都是必填，数组的长度必须是相同的
                    if (dtPlanCount.Length >= 1)
                    {
                        for (int i = 0; i < dtPlanCount.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("insert into officedba.MRPDetail");
                            cmdsql.AppendLine("(CompanyCD,");
                            cmdsql.AppendLine("MRPNo,");
                            cmdsql.AppendLine("SortNo,");
                            cmdsql.AppendLine("ProductID,");
                            cmdsql.AppendLine("UnitID,");
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("GrossCount,");
                            }
                            cmdsql.AppendLine("PlanCount,");
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("UsedUnitID,");
                                cmdsql.AppendLine("UsedUnitCount,");
                                cmdsql.AppendLine("ExRate,");
                            }
                            cmdsql.AppendLine("PlanDate,");
                            cmdsql.AppendLine("MaterialSource,");
                            cmdsql.AppendLine("Remark,");
                            cmdsql.AppendLine("FromBillID,");
                            cmdsql.AppendLine("ModifiedDate,");
                            cmdsql.AppendLine("ModifiedUserID)");
                            cmdsql.AppendLine(" Values(@CompanyCD");
                            cmdsql.AppendLine("            ,@MRPNo");
                            cmdsql.AppendLine("            ,@SortNo");
                            cmdsql.AppendLine("            ,@ProductID");
                            cmdsql.AppendLine("            ,@UnitID");
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                cmdsql.AppendLine("            ,@GrossCount");
                            }
                            cmdsql.AppendLine("            ,@PlanCount");
                            if (userInfo.IsMoreUnit)
                            {
                                cmdsql.AppendLine("            ,@UsedUnitID");
                                cmdsql.AppendLine("            ,@UsedUnitCount");
                                cmdsql.AppendLine("            ,@ExRate");
                            }
                            cmdsql.AppendLine("            ,@PlanDate");
                            cmdsql.AppendLine("            ,@MaterialSource");
                            cmdsql.AppendLine("            ,@Remark");
                            cmdsql.AppendLine("            ,@FromBillID");
                            cmdsql.AppendLine("            ,getdate()");
                            cmdsql.AppendLine("            ,'" + loginUserID + "')");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@MRPNo", model.MRPNo));
                            comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString().Trim()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UnitID", dtUnitID[i].ToString().Trim()));
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@GrossCount", dtGrossCount[i].ToString().Trim()));
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@PlanCount", dtPlanCount[i].ToString().Trim()));
                            if (userInfo.IsMoreUnit)
                            {
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString().Trim()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString().Trim()));
                                comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString().Trim()));
                            }
                            comms.Parameters.Add(SqlHelper.GetParameter("@PlanDate", dtPlanDate[i].ToString().Trim()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@MaterialSource", dtMaterialSource[i].ToString().Trim()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@Remark", dtRemark[i].ToString().Trim()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString().Trim()));
                            listADD.Add(comms);
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

        #region 修改物料需求计划和物料需求计划单明细信息
        /// <summary>
        /// 修改物料需求计划和物料需求计划单明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateMRPInfo(MRPModel model, Hashtable htExtAttr,string loginUserID, string UpdateID)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }

            #region  MRP修改SQL语句
            StringBuilder sqlBom = new StringBuilder();
            sqlBom.AppendLine("update officedba.MRP set");
            sqlBom.AppendLine("                         Subject=@Subject,");
            sqlBom.AppendLine("                         PlanID=@PlanID,");
            sqlBom.AppendLine("                         Remark=@Remark,");
            sqlBom.AppendLine("                         Principal=@Principal,");
            sqlBom.AppendLine("                         DeptID=@DeptID,");
            sqlBom.AppendLine("                         CountTotal=@CountTotal,");
            sqlBom.AppendLine("                         ModifiedDate=getdate(),");
            sqlBom.AppendLine("                         ModifiedUserID='" + loginUserID + "'");
            sqlBom.AppendLine("where CompanyCD=@CompanyCD");
            sqlBom.AppendLine("and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlBom.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Subject", model.Subject));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@PlanID", model.PlanID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion

            #region MRP明细信息更新语句
            //1.先删除不在BOM子件中的
            //2.更新子件中的ID
            //3.添加其它明细

            #region 先删除不在BOM子件中的
            if (!string.IsNullOrEmpty(UpdateID))
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.MRPDetail where CompanyCD=@CompanyCD and MRPNo=@MRPNo and  ID not in(" + UpdateID + ")");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@MRPNo", model.MRPNo));

                listADD.Add(commDel);
            }
            else
            {
                StringBuilder sqlDel = new StringBuilder();
                sqlDel.AppendLine("delete from officedba.MRPDetail where CompanyCD=@CompanyCD and MRPNo=@MRPNo");

                SqlCommand commDel = new SqlCommand();
                commDel.CommandText = sqlDel.ToString();
                commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                commDel.Parameters.Add(SqlHelper.GetParameter("@MRPNo", model.MRPNo));

                listADD.Add(commDel);
            }
            #endregion

            #region 添加或更新操作
            string[] updateID = UpdateID.Split(',');
            if (!string.IsNullOrEmpty(UpdateID) && updateID.Length > 0)
            {
                if (!String.IsNullOrEmpty(model.detPlanCount) && !String.IsNullOrEmpty(model.detPlanDate) && !String.IsNullOrEmpty(model.detMaterialSource))
                {
                    string[] dtSortNo = model.detSortNo.Split(',');
                    string[] dtProductID = model.detProductID.Split(',');
                    string[] dtUnitID = model.detUnitID.Split(',');
                    string[] dtGrossCount = model.detGrossCount.Split(',');
                    string[] dtPlanCount = model.detPlanCount.Split(',');
                    string[] dtPlanDate = model.detPlanDate.Split(',');
                    string[] dtMaterialSource = model.detMaterialSource.Split(',');
                    string[] dtRemark = model.detRemark.Split(',');
                    string[] dtUsedUnitID = model.detUsedUnitID.Split(',');
                    string[] dtUsedUnitCount = model.detUsedUnitCount.Split(',');
                    string[] dtExRate = model.detExRate.Split(',');
                    string[] dtFromBillID = model.detFromBillID.Split(',');

                    for (int i = 0; i < updateID.Length; i++)
                    {
                        int intUpdateID = int.Parse(updateID[i].ToString());
                        if (intUpdateID > 0)
                        {
                            #region 更新MRP明细中的ID
                            StringBuilder sqlEdit = new StringBuilder();
                            sqlEdit.AppendLine("Update officedba.MRPDetail");
                            sqlEdit.AppendLine("Set SortNo=@SortNo,");
                            sqlEdit.AppendLine("	ProductID=@ProductID,");
                            sqlEdit.AppendLine("	UnitID=@UnitID,");
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                sqlEdit.AppendLine("	GrossCount=@GrossCount,");
                            }
                            sqlEdit.AppendLine("	PlanCount=@PlanCount,");
                            if (userInfo.IsMoreUnit)
                            {
                                sqlEdit.AppendLine("	UsedUnitID=@UsedUnitID,");
                                sqlEdit.AppendLine("	UsedUnitCount=@UsedUnitCount,");
                                sqlEdit.AppendLine("	ExRate=@ExRate,");
                            }
                            sqlEdit.AppendLine("	PlanDate=@PlanDate,");
                            sqlEdit.AppendLine("	MaterialSource=@MaterialSource,");
                            sqlEdit.AppendLine("	Remark=@Remark,");
                            sqlEdit.AppendLine("	ModifiedDate=getdate(),");
                            sqlEdit.AppendLine("	ModifiedUserID='" + loginUserID + "'");
                            sqlEdit.AppendLine("where CompanyCD=@CompanyCD");
                            sqlEdit.AppendLine("and ID=@ID");

                            SqlCommand commEdit = new SqlCommand();
                            commEdit.CommandText = sqlEdit.ToString();
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@UnitID", dtUnitID[i].ToString()));
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@GrossCount", dtGrossCount[i].ToString()));
                            }
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@PlanCount", dtPlanCount[i].ToString()));
                            if (userInfo.IsMoreUnit)
                            {
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                commEdit.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@PlanDate", dtPlanDate[i].ToString()));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@MaterialSource", dtMaterialSource[i].ToString()));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@Remark", dtRemark[i].ToString()));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commEdit.Parameters.Add(SqlHelper.GetParameter("@ID", updateID[i].ToString()));
                            listADD.Add(commEdit);
                            #endregion
                        }
                        else
                        {
                            #region 添加MRP明细中的ID
                            StringBuilder sqlIn = new StringBuilder();
                            sqlIn.AppendLine("insert into officedba.MRPDetail(");
                            sqlIn.AppendLine("								CompanyCD,");
                            sqlIn.AppendLine("								MRPNo,");
                            sqlIn.AppendLine("								SortNo,");
                            sqlIn.AppendLine("								ProductID,");
                            sqlIn.AppendLine("								UnitID,");
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("								GrossCount,");
                            }
                            sqlIn.AppendLine("								PlanCount,");
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("								UsedUnitID,");
                                sqlIn.AppendLine("								UsedUnitCount,");
                                sqlIn.AppendLine("								ExRate,");
                            }
                            sqlIn.AppendLine("								PlanDate,");
                            sqlIn.AppendLine("								MaterialSource,");
                            sqlIn.AppendLine("								Remark,");
                            sqlIn.AppendLine("								FromBillID,");
                            sqlIn.AppendLine("								ModifiedDate,");
                            sqlIn.AppendLine("								ModifiedUserID)");
                            sqlIn.AppendLine("Values(	@CompanyCD,");
                            sqlIn.AppendLine("		    @MRPNo,");
                            sqlIn.AppendLine("		    @SortNo,");
                            sqlIn.AppendLine("		    @ProductID,");
                            sqlIn.AppendLine("		    @UnitID,");
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                sqlIn.AppendLine("		    @GrossCount,");
                            }
                            sqlIn.AppendLine("		    @PlanCount,");
                            if (userInfo.IsMoreUnit)
                            {
                                sqlIn.AppendLine("		    @UsedUnitID,");
                                sqlIn.AppendLine("		    @UsedUnitCount,");
                                sqlIn.AppendLine("		    @ExRate,");
                            }
                            sqlIn.AppendLine("		    @PlanDate,");
                            sqlIn.AppendLine("		    @MaterialSource,");
                            sqlIn.AppendLine("		    @Remark,");
                            sqlIn.AppendLine("		    @FromBillID,");
                            sqlIn.AppendLine("		    getdate(),");
                            sqlIn.AppendLine("		    '"+loginUserID+"')");

                            SqlCommand commIn = new SqlCommand();
                            commIn.CommandText = sqlIn.ToString();
                            commIn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@MRPNo", model.MRPNo));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@SortNo", dtSortNo[i].ToString()));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@ProductID", dtProductID[i].ToString()));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@UnitID", dtUnitID[i].ToString()));
                            if (!string.IsNullOrEmpty(dtGrossCount[i].ToString().Trim()))
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@GrossCount", dtGrossCount[i].ToString()));
                            }
                            commIn.Parameters.Add(SqlHelper.GetParameter("@PlanCount", dtPlanCount[i].ToString()));
                            if (userInfo.IsMoreUnit)
                            {
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", dtUsedUnitID[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", dtUsedUnitCount[i].ToString()));
                                commIn.Parameters.Add(SqlHelper.GetParameter("@ExRate", dtExRate[i].ToString()));
                            }
                            commIn.Parameters.Add(SqlHelper.GetParameter("@PlanDate", dtPlanDate[i].ToString()));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@MaterialSource", dtMaterialSource[i].ToString()));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@Remark", dtRemark[i].ToString()));
                            commIn.Parameters.Add(SqlHelper.GetParameter("@FromBillID", dtFromBillID[i].ToString()));

                            listADD.Add(commIn);
                            #endregion
                        }
                    }
                }
            }
            #endregion

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region MRP详细信息
        /// <summary>
        /// MRP详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetMRPInfo(MRPModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select * from (");
            infoSql.AppendLine("	select a.CompanyCD,a.ID,a.MRPNo,a.Subject,");
            infoSql.AppendLine("	a.PlanID,e.PlanNo,a.Remark,a.Principal,g.EmployeeName as PricipalReal,");
            infoSql.AppendLine("	a.DeptID,f.DeptName,Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal,");
            infoSql.AppendLine("	a.Creator,b.EmployeeName as CreatorReal,isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,");
            infoSql.AppendLine("	a.Confirmor,c.EmployeeName as ConfirmorReal,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            infoSql.AppendLine("	a.Closer,d.EmployeeName as CloserReal,isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate,");
            infoSql.AppendLine("    case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatusText,");
            infoSql.AppendLine("    a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            infoSql.AppendLine("	a.BillStatus,isnull( CONVERT(CHAR(10), a.ModifiedDate, 23),'') as ModifiedDate,a.ModifiedUserID from officedba.MRP as a");
            infoSql.AppendLine("	left join officedba.EmployeeInfo as b on a.Creator=b.ID");
            infoSql.AppendLine("	left join officedba.EmployeeInfo as c on a.Confirmor=c.ID");
            infoSql.AppendLine("	left join officedba.EmployeeInfo as d on a.Closer=d.ID");
            infoSql.AppendLine("    left join officedba.EmployeeInfo as g on a.Principal=g.ID");
            infoSql.AppendLine("	left join officedba.DeptInfo as f on a.DeptID=f.ID");
            infoSql.AppendLine("	left join officedba.MasterProductSchedule e on a.PlanID=e.ID");
            infoSql.AppendLine(") as m");
            infoSql.AppendLine("where ID=@ID");

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

        #region MRP明细信息
        /// <summary>
        /// MRP明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetMRPDetailInfo(MRPModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select * from (");
            detSql.AppendLine("	select  a.CompanyCD,a.ID as DetailID,a.MRPNo,a.SortNo,");
            detSql.AppendLine("			a.ProductID,b.ProdNo,b.ProductName,b.Specification,a.UnitID,c.CodeName as UnitName,a.UsedUnitID,e.CodeName as UsedUnitName,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount,");
            detSql.AppendLine("         f.TypeName as ColorName,g.TypeName as MaterialName,");
            detSql.AppendLine("			Convert(numeric(14,"+userInfo.SelPoint+"),a.GrossCount) as GrossCount,Convert(numeric(14,"+userInfo.SelPoint+"),a.PlanCount) as PlanCount,isnull( CONVERT(CHAR(10), a.PlanDate, 23),'') as PlanDate,a.MaterialSource,");
            detSql.AppendLine("			a.FromBillID,a.FromBillNo,a.FromLineNo,a.Remark,Convert(numeric(14,"+userInfo.SelPoint+"),a.ProcessedCount) as ProcessedCount,");
            detSql.AppendLine("         case when a.MaterialSource=0 then '采购' when a.MaterialSource=1 then '生产' when a.MaterialSource=2 then '库存' end as strMaterialSource,");
            detSql.AppendLine("			a.ModifiedDate,a.ModifiedUserID");
            detSql.AppendLine("	from officedba.MRPDetail a");
            detSql.AppendLine("			left join officedba.ProductInfo b on a.ProductID=b.ID");
            detSql.AppendLine("			left join officedba.CodeUnitType c on a.UnitID=c.ID");
            detSql.AppendLine("         left join officedba.CodeUnitType e on a.UsedUnitID=e.ID");
            detSql.AppendLine("         left join officedba.CodePublicType f on b.ColorID=f.ID");
            detSql.AppendLine("         left join officedba.CodePublicType g on b.Material=g.ID");
            detSql.AppendLine("	where a.CompanyCD=@CompanyCD and MRPNo=(");
            detSql.AppendLine("					select top 1 MRPNo from officedba.MRP where ID=@ID");
            detSql.AppendLine("			     )");
            detSql.AppendLine(") as info");
            detSql.AppendLine("where CompanyCD=@CompanyCD");


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

        #region 通过检索条件查询物料需求计划单信息
        /// <summary>
        /// 通过检索条件查询物料需求计划单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetMRPListBycondition(MRPModel model, int FlowStatus, int BillTypeFlag, int BillTypeCode, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select * from (");
            searchSql.AppendLine("	  SELECT a.CompanyCD,a.ID,a.MRPNo,a.Subject,a.PlanID,isnull(e.PlanNo,'')as PlanNo,a.ModifiedDate,");
            searchSql.AppendLine("           a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10,");
            searchSql.AppendLine("			 a.Principal,isnull(c.EmployeeName,'')as PrincipalReal,a.DeptID,isnull(d.DeptName,'')as DeptName ,");
            searchSql.AppendLine("			 isnull( a.Confirmor,'')as Confirmor ,isnull(f.EmployeeName,'')as ConfirmorReal,");
            searchSql.AppendLine("			 isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,");
            searchSql.AppendLine("			 a.BillStatus,isnull( b.FlowStatus,'0')as FlowStatus,");
            searchSql.AppendLine("           case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '变更' when a.BillStatus=4 then '手工结单' when a.BillStatus=5 then '自动结单' end as strBillStatus,");
            searchSql.AppendLine("           case when b.FlowStatus=1 then '待审批' when b.FlowStatus=2 then '审批中' when b.FlowStatus=3 then '审批通过' when b.FlowStatus=4 then '审批不通过' when b.FlowStatus=5 then '撤消审批' end as strFlowStatus ");
            searchSql.AppendLine("	  FROM officedba.MRP a ");
            searchSql.AppendLine("	  LEFT JOIN officedba.FlowInstance b ON a.ID=b.BillID and b.BillTypeFlag=@BillTypeFlag and b.BillTypeCode=@BillTypeCode");
            searchSql.AppendLine("				AND b.ID =(  select  max(ID)   from  officedba.FlowInstance H    where   H.CompanyCD = A.CompanyCD  AND H.BillID = A.ID  and H.BillTypeFlag =@BillTypeFlag    and H.BillTypeCode = @BillTypeCode)");
            searchSql.AppendLine("	  LEFT JOIN officedba.EmployeeInfo c  ON a.Principal=c.ID");
            searchSql.AppendLine("	  LEFT JOIN officedba.EmployeeInfo f ON a.Confirmor =f.ID");
            searchSql.AppendLine("	  LEFT JOIN officedba.DeptInfo d  ON a.DeptID=d.ID");
            searchSql.AppendLine("	  LEFT Join officedba.MasterProductSchedule e ON a.PlanID=e.ID");
            searchSql.AppendLine(") as info");
            searchSql.AppendLine("where CompanyCD=@CompanyCD");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeFlag", BillTypeFlag.ToString()));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillTypeCode", BillTypeCode.ToString()));

            //单据编号
            if (!string.IsNullOrEmpty(model.MRPNo))
            {
                searchSql.AppendLine("and MRPNo like @MRPNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MRPNo", "%" + model.MRPNo + "%"));
            }
            //单据主题
            if (!string.IsNullOrEmpty(model.Subject))
            {
                searchSql.AppendLine(" and Subject like @Subject");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", "%" + model.Subject + "%"));
            }
            //负责人
            if (model.PlanID > 0)
            {
                searchSql.AppendLine(" and PlanID=@PlanID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanID", model.PlanID.ToString()));
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
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                }
            }
            //审批状态
            if (FlowStatus > -1)
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


        #region MRP运算:根据父件物品ID，列出所有子件
        /// <summary>
        /// MRP运算时,根据父件物品ID，列出所有子件
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductID"></param>
        /// <param name="ProduceCount"></param>
        /// <returns></returns>
        public static DataTable MRPCompute_ByParentProduct_GetList(string CompanyCD, int ProductID, Decimal ProduceCount,Decimal ParentProduceCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[4];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@ProductID", ProductID);
                param[2] = SqlHelper.GetParameter("@ProduceCount", ProduceCount);
                param[3] = SqlHelper.GetParameter("@ParentProduceCount", ParentProduceCount);
                #endregion

                //创建命令
                return SqlHelper.ExecuteStoredProcedure("officedba.ProductionManager_MRPCompute_Select",param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MRP运算:根据主生产计划单据编号查询主生产计划明细
        /// <summary>
        /// 根据主生产计划单据编号查询主生产计划明细
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMasterProductScheduleDetail_ByPlanNo(string PlanNo, string CompanyCD)
        {

            #region
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("Select ProductID,ProduceCount,ID as DetailID,SortNo,FromBillNo,FromBillID,FromLineNo,isnull(ExRate,1) asExRate from officedba.MasterProductScheduleDetail where CompanyCD=@CompanyCD and PlanNo=@PlanNo");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", PlanNo));
            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region MRP运算:判断是否有父件等于该ProductID的
        /// <summary>
        /// MRP运算:判断是否有父件等于该ProductID的
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static int HaveBomParentProduct(string CompanyCD,int ProductID)
        {
            string sql = "  select Count(ID)  from officedba.BomDetail where CompanyCD=@CompanyCD and BomNo=(select BomNO from officedba.Bom Where CompanyCD=@CompanyCD and ProductID=@ProductID)";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@ProductID", ProductID);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            return Convert.ToInt32(obj);
        }
        #endregion

        #region MRP运算:物品库存快照
        /// <summary>
        /// 物品库存快照
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetProductSnapshot(int ProductID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            //infoSql.AppendLine("select a.ID as ProductID,a.ProductName,a.ProdNo as ProductNo,");
            //infoSql.AppendLine("	   Convert(numeric(22,"+userInfo.SelPoint+"),isnull(a.SafeStockNum,0)) as SafeCount ,");
            //infoSql.AppendLine("	   b.CodeName as UnitName,");
            //infoSql.AppendLine("	  (");
            //infoSql.AppendLine("	   select sum(Convert(numeric(22,"+userInfo.SelPoint+"),isnull(c.ProductCount,0))) as ProductCount ");
            //infoSql.AppendLine("	   from officedba.StorageProduct  c");
            //infoSql.AppendLine("	   where a.ID=c.ProductID");
            //infoSql.AppendLine("	  )as ProductCount");
            //infoSql.AppendLine("from officedba.ProductInfo a ");
            //infoSql.AppendLine("left join officedba.CodeUnitType b on a.UnitID=b.ID");
            //infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ID=@ProductID");
            infoSql.AppendLine("select	a.ID as ProductID,a.ProductName,a.ProdNo as ProductNo,isnull(c.BatchNo,'') BatchNo,");
            infoSql.AppendLine("		Convert(numeric(22,"+userInfo.SelPoint+"),isnull(a.SafeStockNum,0)) as SafeCount,");
            infoSql.AppendLine("		b.CodeName as UnitName,Convert(numeric(22,"+userInfo.SelPoint+"),isnull(c.ProductCount,0)) as ProductCount,c.StorageID,d.StorageName");
            infoSql.AppendLine("from officedba.ProductInfo a");
            infoSql.AppendLine("left join officedba.CodeUnitType b on a.UnitID=b.ID");
            infoSql.AppendLine("left join officedba.StorageProduct c on a.ID=c.ProductID");
            infoSql.AppendLine("left join officedba.StorageInfo d on c.StorageID=d.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ID=@ProductID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 主生产计划唯一性验证
        /// <summary>
        /// 主生产计划唯一性验证
        /// </summary>
        /// <param name="ParentCode">上级编码</param>
        /// <returns>大于0：已经有物料需求计划引用该计划了，否则无物料需求计划引用该计划</returns>
        public static int PlanCount(MRPModel model)
        {
            string sql = string.Empty;
            if (model.ID > 0)
            {
                sql = "select Count(ID) from officedba.MRP where CompanyCD=@CompanyCD and PlanID=@PlanID and ID<>@ID";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parms[1] = SqlHelper.GetParameter("@PlanID", model.PlanID);
                parms[2] = SqlHelper.GetParameter("@ID", model.ID);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                return Convert.ToInt32(obj);
            }
            else
            {
                sql = "select Count(ID) from officedba.MRP where CompanyCD=@CompanyCD and PlanID=@PlanID";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parms[1] = SqlHelper.GetParameter("@PlanID", model.PlanID);
                object obj = SqlHelper.ExecuteScalar(sql, parms);
                return Convert.ToInt32(obj);
            }


        }
        #endregion

        #region 删除物料需求计划
        /// <summary>
        /// 删除物料需求计划
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteMRP(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.MRPDetail where CompanyCD=@CompanyCD and MRPNo=(select top 1 MRPNo from officedba.MRP where ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.MRP where ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
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
        public static bool ConfirmOrCompleteMRP(MRPModel model, string loginUserID, int OperateType)
        {
            //1：确认  2:结单  3:取消结单
            if (OperateType == 1)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.MRP SET");
                sql.AppendLine(" Confirmor         = @Confirmor,");
                sql.AppendLine(" ConfirmDate        = @ConfirmDate,");
                sql.AppendLine(" ModifiedDate   = getdate(),");
                sql.AppendLine(" BillStatus   = 2,");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  ID=@ID");


                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);
                param[1] = SqlHelper.GetParameter("@Confirmor", model.Confirmor);
                param[2] = SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            else if (OperateType == 2)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.MRP SET");
                sql.AppendLine(" Closer         = @Closer,");
                sql.AppendLine(" CloseDate   = @CloseDate,");
                sql.AppendLine(" BillStatus   = 4,");
                sql.AppendLine(" ModifiedDate   = getdate(),");
                sql.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine(" Where  ID=@ID");


                SqlParameter[] param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);
                param[1] = SqlHelper.GetParameter("@Closer", model.Closer);
                param[2] = SqlHelper.GetParameter("@CloseDate", model.CloseDate);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" update officedba.MRP set Closer=null,CloseDate=null,ModifiedDate=getdate(),BillStatus=2,ModifiedUserID = '" + loginUserID + "'");
                sql.AppendLine("  Where  ID=@ID");


                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@ID", model.ID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
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
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and " + ColumnName + " in(select MRPNo from officedba.MRP where ID in(" + ID + "))";
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

        #region 采购需求中是否被引用
        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static int CountRefrencePurchaseRequire(string CompanyCD, string ID, string TableName, string ColumnName)
        {
            string sql = "select count(*) as RefCount from officedba." + TableName + " where CompanyCD=@CompanyCD and OrderCount is not null OR OrderCount>0 and " + ColumnName + " in(" + ID + ")";
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

        #region 物品可用存量之和
        /// <summary>
        /// 物品可用存量之和
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static Decimal ProductUseCount(int ProductID,string CompanyCD)
        {
            //string sql = "select isnull(Convert(numeric(12,2),sum(isnull(ProductCount,0)+isnull(RoadCount,0)-isnull(OutCount,0)-isnull(OrderCount,0))),0) as UseCountTotal from officedba.StorageProduct where CompanyCD=@CompanyCD and ProductID=@ProductID";
            /*只取现有存量*/
            string sql = "select isnull(Convert(numeric(12,2),sum(isnull(ProductCount,0))),0) as UseCountTotal from officedba.StorageProduct where CompanyCD=@CompanyCD and ProductID=@ProductID";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            parms[1] = SqlHelper.GetParameter("@ProductID", ProductID);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return Decimal.Parse("0");
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        #endregion

        #region 单个物品是否已经生成采购需求
        /// <summary>
        /// 物料需求计划明细中的物品是否已经在采购需求计划表里了
        /// </summary>
        /// <param name="CompayCD"></param>
        /// <param name="MRPID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static int CountPurchaseRequire(string CompayCD,string MRPID,int ProductID)
        {
            string sql = "select Count(*) as HasMRP from officedba.PurchaseRequire where CompanyCD=@CompanyCD and MRPCD=@MRPCD and ProdID=@ProdID";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompayCD);
            parms[1] = SqlHelper.GetParameter("@MRPCD", MRPID);
            parms[2] = SqlHelper.GetParameter("@ProdID", ProductID);

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

        #region 物料需求计划明细中是否包含已经生成采购需求的
        /// <summary>
        /// 物料需求计划明细中是否包含已经生成采购需求的
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="strMRPID"></param>
        /// <returns></returns>
        public static bool IsHavePurchaseRequire(string CompanyCD,string strMRPID)
        {
            DataTable dbMRPDet = GetMRPDetail_PurchaseList(CompanyCD, strMRPID);
            if (dbMRPDet.Rows.Count > 0)
            {
                for (int i = 0; i < dbMRPDet.Rows.Count; i++)
                {
                    int ProdID = int.Parse(dbMRPDet.Rows[i]["ProductID"].ToString());    //物品ID
                    string MRPID = dbMRPDet.Rows[i]["MRPID"].ToString();                 //MRPID

                    if (CountPurchaseRequire(CompanyCD, MRPID, ProdID) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 生成采购计划：物料需求计划明细中来源是采购的数据
        /// <summary>
        /// 物料需求计划明细中来源是采购的数据
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="MRPID"></param>
        /// <returns></returns>
        public static DataTable GetMRPDetail_PurchaseList(string CompanyCD, string MRPID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select	a.ID as DetailID,a.ProductID,a.UnitID,a.PlanCount,a.UsedUnitID,a.UsedUnitCount,a.PlanDate,Convert(numeric(12,2),isnull(c.MinStockNum,0)) as MinStockNum,Convert(numeric(12,2),isnull(c.SafeStockNum,0)) as SafeStockNum,");
            detSql.AppendLine("		b.ID as MRPID,c.ProdNo,c.ProductName,c.TypeID as ProdType,");
            detSql.AppendLine("		c.Specification as Standards from officedba.MRPDetail a");
            detSql.AppendLine("left join officedba.MRP b  on a.MRPNO=b.MRPNo");
            detSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
            detSql.AppendLine("where a.CompanyCD=@CompanyCD");
            detSql.AppendLine("and a.MaterialSource=0");
            detSql.AppendLine("and a.MRPNo in(");
            detSql.AppendLine("				select MRPNo from officedba.MRP where CompanyCD=@CompanyCD and id in(" + MRPID + ")");
            detSql.AppendLine("		       )");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 生成采购需求计划
        /// <summary>
        /// 生成采购需求计划
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="MRPID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool SendPurchase(string CompanyCD, string MRPID, int LoginUserID, out string reason)
        {
            //判断MRPID 是否已经生成采购需求计划,如果已经生成过了，返回给予提示;如果没有生成，则添加数据到officedba. PurchaseRequire表
            //取出MRP明细中物品来源是采购的，取出该物品的可用存量,判断 可用存量（库存）-最低下限库存量 <应计划数量，小于时往采购需求表里插入
            // 生成采购需求因在执行状态下允许操作，会出现反复生成采购需求操作,可能存在三种情况：
            //1> 明细中的物品没有生成采购需求，直接添加入采购需求
            //2> 明细中的物品已经有生成采购需求，返回提示是否替换已生成的采购需要求，同意的情况下修改采购需求中的对应数量(计划数量、物品可用存量、需申购数量),拒绝的情况下不进行任何操作
            //3> 明细中的物品已经生成采购计划时，返回提示是否替换已生成的采购计划，同意的情况下，修改采购需求中的对应数量（计划数量、物品可用存量、需申购数量）,忽略考虑物品是否已经采购完成等情况,拒绝的情况下不进行任何操作

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            string tempReason = "";
            bool tempOperateResult = false;

            DataTable dbMRPDet = GetMRPDetail_PurchaseList(CompanyCD, MRPID);
            if (dbMRPDet.Rows.Count > 0)
            {
                for (int i = 0; i < dbMRPDet.Rows.Count; i++)
                {
                    int DetailID = int.Parse(dbMRPDet.Rows[i]["DetailID"].ToString());              //明细ID
                    int ProdID = int.Parse(dbMRPDet.Rows[i]["ProductID"].ToString());               //物品ID
                    DateTime PlanDate = DateTime.Parse(dbMRPDet.Rows[i]["PlanDate"].ToString());    //计划供料日期
                    int intMRPID = int.Parse(dbMRPDet.Rows[i]["MRPID"].ToString());                 //MRPID
                    Decimal NeedCount = Decimal.Parse(dbMRPDet.Rows[i]["PlanCount"].ToString());    //计划数量
                    Decimal NeedCount2 = Decimal.Parse(dbMRPDet.Rows[i]["PlanCount"].ToString());    //计划数量(写入采购需求表中用基本数量，写入物料需求明细时需根据是否启用多计量单位动态判断是用基本数量还是实际数量)
                    if (userInfo.IsMoreUnit)
                    {
                        NeedCount2 = Decimal.Parse(dbMRPDet.Rows[i]["UsedUnitCount"].ToString());
                    }
                    //Decimal HasNum = ProductUseCount(ProdID, CompanyCD);                            //可用存量
                    Decimal HasNum = GetStorageProductCount(ProdID);                                //现有存量
                    Decimal WantingNum = NeedCount;                                                 //需申购数量
                    Decimal WantingNum2 = NeedCount2;

                    #region 是否已经生成过采购需求了
                    if (CountPurchaseRequire(CompanyCD, intMRPID.ToString(), ProdID) > 0)
                    {
                        #region 更新操作
                        StringBuilder sqlEditPur = new StringBuilder();
                        sqlEditPur.AppendLine("Update officedba.PurchaseRequire");
                        sqlEditPur.AppendLine("Set NeedCount=@NeedCount,");
                        sqlEditPur.AppendLine("	HasNum=@HasNum,");
                        sqlEditPur.AppendLine("	WantingNum=@WantingNum,");
                        sqlEditPur.AppendLine("	RequireDate=@RequireDate ");
                        sqlEditPur.AppendLine("where CompanyCD=@CompanyCD");
                        sqlEditPur.AppendLine("and MRPCD=@MRPCD");
                        sqlEditPur.AppendLine("and ProdID=@ProdID");

                        SqlCommand commEditPur = new SqlCommand();
                        commEditPur.CommandText = sqlEditPur.ToString();
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@MRPCD", intMRPID));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@ProdID", ProdID));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@NeedCount", NeedCount));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@HasNum", HasNum));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@WantingNum", WantingNum));
                        commEditPur.Parameters.Add(SqlHelper.GetParameter("@RequireDate", PlanDate));

                        listADD.Add(commEditPur);
                        #endregion

                        #region 更新物料需求计划明细的已生成采购需求数量 
                        StringBuilder sqlEditMRPDet = new StringBuilder();
                        sqlEditMRPDet.AppendLine("Update officedba.MRPDetail Set ProcessedCount=@ProcessedCount where ID=@DetailID");

                        SqlCommand commEditMRPDet = new SqlCommand();
                        commEditMRPDet.CommandText = sqlEditMRPDet.ToString();
                        commEditMRPDet.Parameters.Add(SqlHelper.GetParameter("@DetailID", DetailID));
                        commEditMRPDet.Parameters.Add(SqlHelper.GetParameter("@ProcessedCount", WantingNum2));
                        listADD.Add(commEditMRPDet);
                        #endregion
                    }
                    else
                    {
                        #region 添加操作
                        StringBuilder sqlInPur = new StringBuilder();
                        sqlInPur.AppendLine("insert into officedba. PurchaseRequire");
                        sqlInPur.AppendLine("(	CompanyCD,");
                        sqlInPur.AppendLine("	MRPCD,");
                        sqlInPur.AppendLine("	ProdID,");
                        sqlInPur.AppendLine("	NeedCount,");
                        sqlInPur.AppendLine("	HasNum,");
                        sqlInPur.AppendLine("	WantingNum,");
                        sqlInPur.AppendLine("	RequireDate,");
                        sqlInPur.AppendLine("	Creator,");
                        sqlInPur.AppendLine("	CreateDate");
                        sqlInPur.AppendLine(")");
                        sqlInPur.AppendLine("Values");
                        sqlInPur.AppendLine("(	@CompanyCD,");
                        sqlInPur.AppendLine("	@MRPCD,");
                        sqlInPur.AppendLine("	@ProdID,");
                        sqlInPur.AppendLine("	@NeedCount,");
                        sqlInPur.AppendLine("	@HasNum,");
                        sqlInPur.AppendLine("	@WantingNum,");
                        sqlInPur.AppendLine("	@RequireDate,");
                        sqlInPur.AppendLine("	@Creator,");
                        sqlInPur.AppendLine("	getdate()");
                        sqlInPur.AppendLine(")");

                        SqlCommand commInPur = new SqlCommand();
                        commInPur.CommandText = sqlInPur.ToString();
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@MRPCD", intMRPID));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@ProdID", ProdID));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@NeedCount", NeedCount));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@HasNum", HasNum));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@WantingNum", WantingNum));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@RequireDate", PlanDate));
                        commInPur.Parameters.Add(SqlHelper.GetParameter("@Creator", LoginUserID));

                        listADD.Add(commInPur);
                        #endregion

                        #region 更新物料需求计划明细的已生成采购需求数量
                        StringBuilder sqlInMRPDet = new StringBuilder();
                        sqlInMRPDet.AppendLine("Update officedba.MRPDetail Set ProcessedCount=@ProcessedCount where  ID=@DetailID");

                        SqlCommand commInMRPDet = new SqlCommand();
                        commInMRPDet.CommandText = sqlInMRPDet.ToString();
                        commInMRPDet.Parameters.Add(SqlHelper.GetParameter("@DetailID", DetailID));
                        commInMRPDet.Parameters.Add(SqlHelper.GetParameter("@ProcessedCount", WantingNum2));
                        listADD.Add(commInMRPDet);
                        #endregion
                    }
                    #endregion

                }
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    tempOperateResult = true;
                }
            }
            else
            {
                tempReason = "物料需求计划中没有明细信息可以生成采购需求计划";
            }
            reason = tempReason;

            return tempOperateResult;

        }
        #endregion

        #region 现有存量
        public static decimal GetStorageProductCount(int intProductID)
        {
            string sql = "select Convert(numeric(22,6),isnull(ProductCount,0)) as ProductCount from officedba.StorageProduct where ProductID=@ProductID";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ProductID", intProductID);
            object obj = SqlHelper.ExecuteScalar(sql, parms);

            if (obj == null)
            {
                return Convert.ToDecimal("0");
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        #endregion

        #region 取消确认
        /// <summary>
        /// 取消确认(未生成采购计划的才可以取消确认)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="BillID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public static bool CancelConfirmOperate(MRPModel model, int BillTypeFlag, int BillTypeCode, string loginUserID)
        {
            ArrayList listADD = new ArrayList();

            //#region 传参
            try
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

                    #region 更新主表信息

                    StringBuilder sqlUn = new StringBuilder();
                    sqlUn.AppendLine(" UPDATE officedba.MRP SET");
                    sqlUn.AppendLine(" Confirmor         = null,");
                    sqlUn.AppendLine(" ConfirmDate        = null,");
                    sqlUn.AppendLine(" ModifiedDate   = getdate(),");
                    sqlUn.AppendLine(" BillStatus   = 1,");
                    sqlUn.AppendLine(" ModifiedUserID = '" + loginUserID + "'");
                    sqlUn.AppendLine(" Where ID=@ID");


                    SqlCommand commUn = new SqlCommand();
                    commUn.CommandText = sqlUn.ToString();
                    commUn.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                    listADD.Add(commUn);
                    #endregion

                    #region 删除采购需求计划表里的数据
                    StringBuilder sqlDel = new StringBuilder();
                    sqlDel.AppendLine("delete from officedba.PurchaseRequire where CompanyCd=@CompanyCD and MRPCD=@ID");


                    SqlCommand commDel= new SqlCommand();
                    commDel.CommandText = sqlDel.ToString();
                    commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commDel.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                    listADD.Add(commDel);
                    #endregion

                    #region 更新明细表里的已生成采购需求数量
                    StringBuilder sqlUnDet = new StringBuilder();
                    sqlUnDet.AppendLine("Update officedba.MRPDetail Set ProcessedCount=null where CompanyCD=@CompanyCD and MRPNo=(select top 1 MRPNo from officedba.MRP where ID=@ID)");


                    SqlCommand commUnDet = new SqlCommand();
                    commUnDet.CommandText = sqlUnDet.ToString();
                    commUnDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commUnDet.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                    listADD.Add(commUnDet);
                    #endregion

                #endregion


                return SqlHelper.ExecuteTransWithArrayList(listADD);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(MRPModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.MRP set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND MRPNo = @MRPNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@MRPNo", model.MRPNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}
