/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/27
 ***********************************************/


using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;
using XBase.Model.Office.ProductionManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageInProcessDBHelper
    {
        #region 查询：生产完工入库单
        /// <summary>
        /// 查询仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInProcessTableBycondition(string BatchNo,StorageInProcessModel model, string timeStart, string timeEnd, string StorageID, string FromBillNo, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            //这个是列表查询
            //入库单编号、入库单主题、生产任务单、加工类别、加工单位、生产负责人、
            //部门、人库人、入库时间、入库数量、入库金额、摘要、单据状态
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID,ISNULL(a.InNo,'') AS InNo                                                                ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                         ");
            sql.AppendLine(",ISNULL(j.TaskNo,'') AS TaskNo                                                                       ");
            sql.AppendLine(",ISNULL(b.DeptName,'') as ProcessDeptName                                                            ");
            sql.AppendLine(",case j.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件'  else '' end as ProcessType");
            sql.AppendLine(",ISNULL(k.DeptName,'') as InPutDeptName                                                              ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') AS Processor                                                              ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as Executor                                                               ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate    ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                              ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                              ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                     ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'                   ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end as BillStatusName                              ");
            sql.AppendLine("	FROM officedba.StorageInProcess a                                                          ");
            sql.AppendLine("	left join officedba.EmployeeInfo h on h.ID=a.Executor                                            ");
            sql.AppendLine("	left join officedba.ManufactureTask j on j.ID=a.FromBillID                                       ");
            sql.AppendLine("	left join officedba.DeptInfo b on j.DeptID=b.ID                                             ");
            sql.AppendLine("	left join officedba.EmployeeInfo i on i.ID=j.Principal                                           ");
            sql.AppendLine("	left join officedba.DeptInfo k on a.DeptID=k.ID                                                  ");
            sql.AppendLine("	left join officedba.StorageInProcessDetail l on a.InNo=l.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and l.BatchNo like '%'+ @BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.InNo))
            {
                sql.AppendLine(" and a.InNo like '%'+ @InNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", model.InNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(FromBillNo))
            {
                sql.AppendLine(" and j.TaskNo = @FromBillNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNo));
            }
            if (!string.IsNullOrEmpty(model.ProcessDept))
            {
                sql.AppendLine(" and j.DeptID = @ProcessDept");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProcessDept", model.ProcessDept));
            }
            if (!string.IsNullOrEmpty(model.Processor))
            {
                sql.AppendLine(" and j.Principal = @Processor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Processor", model.Processor));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.EnterDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.EnterDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(StorageID))
            {
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInProcessDetail where StorageID=@StorageID)");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            }
            if (!string.IsNullOrEmpty(model.EFIndex) && !string.IsNullOrEmpty(model.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + model.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + model.EFDesc + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }

        public static DataTable GetStorageInProcessTableBycondition(string BatchNo,StorageInProcessModel model, string timeStart, string timeEnd, string StorageID, string FromBillNo, string orderby)
        {
            //这个是列表查询
            //入库单编号、入库单主题、生产任务单、加工类别、加工单位、生产负责人、
            //部门、人库人、入库时间、入库数量、入库金额、摘要、单据状态
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID,ISNULL(a.InNo,'') AS InNo                                                                ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                         ");
            sql.AppendLine(",ISNULL(j.TaskNo,'') AS TaskNo                                                                       ");
            sql.AppendLine(",ISNULL(b.DeptName,'') as ProcessDeptName                                                            ");
            sql.AppendLine(",case j.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件'  else '' end as ProcessType");
            sql.AppendLine(",ISNULL(k.DeptName,'') as InPutDeptName                                                              ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') AS Processor                                                              ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as Executor                                                               ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate    ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                              ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                              ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                     ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'                   ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end as BillStatusName                              ");
            sql.AppendLine("	FROM officedba.StorageInProcess a                                                          ");
            sql.AppendLine("	left join officedba.EmployeeInfo h on h.ID=a.Executor                                            ");
            sql.AppendLine("	left join officedba.ManufactureTask j on j.ID=a.FromBillID                                       ");
            sql.AppendLine("	left join officedba.DeptInfo b on j.DeptID=b.ID                                             ");
            sql.AppendLine("	left join officedba.EmployeeInfo i on i.ID=j.Principal                                           ");
            sql.AppendLine("	left join officedba.DeptInfo k on a.DeptID=k.ID                                                  ");
            sql.AppendLine("	left join officedba.StorageInProcessDetail l on a.InNo=l.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and l.BatchNo like '%'+ @BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.InNo))
            {
                sql.AppendLine(" and a.InNo like '%'+ @InNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", model.InNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(FromBillNo))
            {
                sql.AppendLine(" and j.TaskNo = @FromBillNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNo));
            }
            if (!string.IsNullOrEmpty(model.ProcessDept))
            {
                sql.AppendLine(" and j.DeptID = @ProcessDept");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProcessDept", model.ProcessDept));
            }
            if (!string.IsNullOrEmpty(model.Processor))
            {
                sql.AppendLine(" and j.Principal = @Processor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Processor", model.Processor));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.EnterDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.EnterDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(StorageID))
            {
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInProcessDetail where StorageID=@StorageID)");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 查看：生产完工入库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取采购入库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInProcessDetailInfo(StorageInProcessModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID ,a.CanViewUser,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10 ");
            sql.AppendLine(",a.InNo                                                                                                                                                ");
            sql.AppendLine(",a.FromType                                                                                                                                            ");
            sql.AppendLine(",a.FromBillID                                                                                                                                          ");
            sql.AppendLine(",ISNULL(k.TaskNo,'') AS TaskNo                                                                                                                         ");
            sql.AppendLine(",a.Title                                                                                                                                               ");
            sql.AppendLine(",ISNULL(j.DeptName,'') as ProcessDeptName                                                                                                              ");
            sql.AppendLine(",k.ManufactureType as ProcessType                                                                                                                      ");
            sql.AppendLine(",ISNULL(x.EmployeeName,'') as ProcessorName                                                                                                            ");
            sql.AppendLine(",ISNULL(a.DeptID,'') as InPutDept                                                                                                                      ");
            sql.AppendLine(",ISNULL(l.DeptName,'') as InPutDeptName                                                                                                                ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') as A_TotalPrice                                                                                                              ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                                                ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                                                       ");
            sql.AppendLine(",a.Executor                                                                                                                                            ");
            sql.AppendLine(",d.EmployeeName as ExecutorName                                                                                                                        ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate                                                      ");
            sql.AppendLine(",a.Remark                                                                                                                                              ");
            sql.AppendLine(",a.Creator                                                                                                                                             ");
            sql.AppendLine(",e.EmployeeName as CreatorName                                                                                                                         ");
            sql.AppendLine(",case when a.CreateDate Is NULL then '' else CONVERT(VARCHAR(10),a.CreateDate, 21) end AS CreateDate                                                   ");
            sql.AppendLine(",a.BillStatus                                                                                                                                          ");
            sql.AppendLine(",a.Confirmor                                                                                                                                           ");
            sql.AppendLine(",f.EmployeeName as ConfirmorName                                                                                                                       ");
            sql.AppendLine(",case when a.ConfirmDate Is NULL then '' else CONVERT(VARCHAR(10),a.ConfirmDate, 21) end AS ConfirmDate                                                ");
            sql.AppendLine(",a.Closer                                                                                                                                              ");
            sql.AppendLine(",g.EmployeeName as CloserName                                                                                                                          ");
            sql.AppendLine(",case when a.CloseDate Is NULL then '' else CONVERT(VARCHAR(10),a.CloseDate, 21) end AS CloseDate                                                      ");
            sql.AppendLine(",case when a.ModifiedDate Is NULL then '' else CONVERT(VARCHAR(10),a.ModifiedDate, 21) end AS ModifiedDate                                             ");
            sql.AppendLine(",a.ModifiedUserID                                                                                                                                      ");
            sql.AppendLine(",a.ModifiedUserID as ModifiedUserName                                                                                                                    ");
            sql.AppendLine(",b.ID as DetailID                                                                                                                                      ");
            sql.AppendLine(",b.ProductID                                                                                                                                           ");
            sql.AppendLine(",ISNULL(c.ProdNo,'') as ProductNo                                                                                                                      ");
            sql.AppendLine(",ISNULL(c.ProductName,'') as ProductName                                                                                                               ");
            sql.AppendLine(",ISNULL(q.CodeName,'') as UnitID                                                                                                                       ");
            sql.AppendLine(",ISNULL(c.Specification,'') as Specification                                                                                                           ");
            sql.AppendLine(",c.IsBatchNo");
            sql.AppendLine(",ISNULL(b.UnitPrice,0) as UnitPrice                                                                                                                 ");
            sql.AppendLine(",b.StorageID                                                                                                                                           ");
            sql.AppendLine(",ISNULL(MD.ProductedCount,0) as FromBillCount");
            sql.AppendLine(",ISNULL(MD.InCount,0) as InCount");
            sql.AppendLine("  ,ISNULL(MD.ProductedCount,0)-ISNULL(MD.InCount,0) as NotInCount                     ");
            sql.AppendLine(",ISNULL(b.ProductCount,0) as ProductCount                                                                                                              ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                                                          ");
            sql.AppendLine(",b.FromLineNo                                                                                                                                          ");
            sql.AppendLine(",b.SortNo                                                                                                                                              ");
            sql.AppendLine(",b.BatchNo ");
            sql.AppendLine(",b.UsedUnitID ");
            sql.AppendLine(",b.UsedUnitCount ");
            sql.AppendLine(",ISNULL(b.UsedPrice,0)UsedPrice ");
            sql.AppendLine(",b.ExRate ");
            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                                               ");
            sql.AppendLine("FROM officedba.StorageInProcess a                                                                                                                ");
            sql.AppendLine("left join officedba.StorageInprocessDetail b                                                                                                           ");
            sql.AppendLine("on a.InNo=b.InNo  and a.CompanyCD=b.CompanyCD                                                                                                                                     ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                                                  ");
            sql.AppendLine("left join officedba.EmployeeInfo d on a.Executor=d.ID                                                                                                  ");
            sql.AppendLine("left join officedba.EmployeeInfo e on a.Creator=e.ID                                                                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Confirmor=f.ID                                                                                                 ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Closer=g.ID                                                                                                    ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) h on a.ModifiedUserID=h.UserID ");
            sql.AppendLine("left join officedba.ManufactureTask k on k.ID=a.FromBillID                                                                                             ");
            sql.AppendLine("left join officedba.ManufactureTaskDetail MD on MD.TaskNo=k.TaskNo  and MD.SortNo=b.FromlineNo and MD.CompanyCD=k.CompanyCD ");
            sql.AppendLine("left join officedba.DeptInfo j on j.ID=k.DeptID                                                                                                        ");
            sql.AppendLine("left join officedba.EmployeeInfo x on x.id=k.Principal                    																																						 ");
            sql.AppendLine("left join officedba.DeptInfo l on a.DeptID=l.ID                                                                                                        ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID																																																		 ");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 添加：生产完工入库单信息及其详细信息
        /// <summary>
        /// 添加生产完工入库单信息及其详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorageInProcess(StorageInProcessModel model, List<StorageInProcessDetailModel> modelList, Hashtable htExtAttr, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageInProcess(");
            strSql.Append("CompanyCD,InNo,FromType,FromBillID,Title,Executor,EnterDate,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,DeptID,TotalPrice,CountTotal,Summary,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@InNo,@FromType,@FromBillID,@Title,@Executor,@EnterDate,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@DeptID,@TotalPrice,@CountTotal,@Summary,@CanViewUser,@CanViewUserName)");
            strSql.AppendLine("set @IndexID = @@IDENTITY");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            SetSaveParameter(comm, model);
            ArrayList lstInsert = new ArrayList();
            lstInsert.Add(comm);//数组加入插入基表的command

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstInsert.Add(cmd);
            #endregion

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                //插入生产完工入库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInProcessDetail(");
                strSqlDetail.Append("InNo,ProductID,StorageID,ProductCount,FromLineNo,ModifiedDate,ModifiedUserID,SortNo,Remark,CompanyCD,UnitPrice,TotalPrice,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@ProductID,@StorageID,@ProductCount,@FromLineNo,getdate(),@ModifiedUserID,@SortNo,@Remark,@CompanyCD,@UnitPrice,@TotalPrice,@BatchNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {

                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInitailDetailInfo(commDetail, modelList[i]);
                    lstInsert.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }
            }

            bool result = SqlHelper.ExecuteTransWithArrayList(lstInsert);
            if (result)
            {
                IndexIDentity = int.Parse(((SqlCommand)lstInsert[0]).Parameters["@IndexID"].Value.ToString());
            }
            else
            {
                IndexIDentity = 0;
            }
            return result;


        }

        #endregion

        #region 修改：生产完工入库单（生产完工入库单信息和详细信息）

        public static bool UpdateStorageInProcess(StorageInProcessModel model, List<StorageInProcessDetailModel> modelList, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageInProcess set ");
            strSql.Append("InNo=@InNo,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("Title=@Title,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("EnterDate=@EnterDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary");
            strSql.Append(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);//数组加入插入基表的command

            //先删掉明细表中对应单据的所有数据
            string delDetail = "delete from officedba.StorageInProcessDetail where CompanyCD='" + model.CompanyCD + "' and InNo='" + model.InNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstUpdate.Add(cmd);
            #endregion

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInProcessDetail(");
                strSqlDetail.Append("InNo,ProductID,StorageID,ProductCount,FromLineNo,ModifiedDate,ModifiedUserID,SortNo,Remark,CompanyCD,UnitPrice,TotalPrice,BatchNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@ProductID,@StorageID,@ProductCount,@FromLineNo,getdate(),@ModifiedUserID,@SortNo,@Remark,@CompanyCD,@UnitPrice,@TotalPrice,@BatchNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInitailDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }

            }

            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);


        }
        #endregion

        #region 删除：生产完工入库信息
        /// <summary>
        /// 删除生产完工入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInProcess(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageInProcessDetail where InNo=(select InNo from officedba.StorageInProcess where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageInProcess where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageInProcessModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInProcess SET");
            sql.AppendLine(" Confirmor          = @Confirmor,");
            sql.AppendLine(" confirmDate      = getdate(),");
            sql.AppendLine(" BillStatus              = 2,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = getdate()");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID));

            lstConfirm.Add(comm);


            List<StorageInProcessDetailModel> modelList = new List<StorageInProcessDetailModel>();
            string sqlSele = "select a.CompanyCD,a.ProductID,a.StorageID,a.BatchNo,a.InNo,a.UnitPrice,a.UsedUnitCount,"
                            + " Convert(varchar(10),c.EnterDate,23) HappenDate,a.ProductCount,a.Remark,a.ProductCount,a.FromLineNo,b.StorageID as DefaultStorageID"
                            + " from officedba.StorageInProcessDetail a"
                            + " left join officedba.ProductInfo b on b.ID=a.ProductID"
                            + " left join officedba.StorageInProcess c on c.InNo=a.InNo and a.CompanyCD = c.CompanyCD "
                            + " where a.CompanyCD='" + model.CompanyCD + "' and a.InNo=(select InNo from officedba.StorageInProcess where ID=" + model.ID + ")";
           
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageInProcessDetailModel modelDetail = new StorageInProcessDetailModel();
                    StorageAccountModel StorageAccountM = new StorageAccountModel();

                    StorageAccountM.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    StorageAccountM.BillType = 4;
                    if (dt.Rows[i]["BatchNo"].ToString() != "")
                    {
                        modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                        StorageAccountM.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    }
                    StorageAccountM.BillNo = dt.Rows[i]["InNo"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(dt.Rows[i]["HappenDate"].ToString());
                    StorageAccountM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/StorageInProcessAdd.aspx";
                    StorageAccountM.ReMark = dt.Rows[i]["Remark"].ToString();                   

                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                        StorageAccountM.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    }
                    if (dt.Rows[i]["StorageID"].ToString() != "")
                    {
                        modelDetail.StorageID = dt.Rows[i]["StorageID"].ToString();
                        StorageAccountM.StorageID = Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    }
                    if (dt.Rows[i]["ProductCount"].ToString() != "")
                    {
                        modelDetail.ProductCount = dt.Rows[i]["ProductCount"].ToString();
                        StorageAccountM.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                        StorageAccountM.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    }
                    if (dt.Rows[i]["UsedUnitCount"].ToString() != "")
                    {
                        modelDetail.UsedUnitCount = dt.Rows[i]["UsedUnitCount"].ToString();
                    }
                    if (dt.Rows[i]["FromLineNo"].ToString() != "")
                    {
                        modelDetail.FromLineNo = dt.Rows[i]["FromLineNo"].ToString();
                    }
                    if (dt.Rows[i]["DefaultStorageID"].ToString() != "")
                    {
                        modelDetail.DefaultStorageID = dt.Rows[i]["DefaultStorageID"].ToString();
                    }
                    modelList.Add(modelDetail);

                    SqlCommand commSA = new SqlCommand();
                    commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                    lstConfirm.Add(commSA);
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strAddMTDetail = new StringBuilder();
                strAddMTDetail.AppendLine("update officedba.ManufactureTaskDetail set ");
                strAddMTDetail.AppendLine(" InCount =ISNULL(InCount,0)+@ReBackNum where ");
                strAddMTDetail.AppendLine(" TaskNo=(select TaskNo from officedba.ManufactureTask where ID=(select FromBillID from officedba.StorageInProcess where ID=" + model.ID + "))");
                strAddMTDetail.AppendLine(" and SortNo=@SortNo");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commReMT = new SqlCommand();
                    commReMT.CommandText = strAddMTDetail.ToString();
                                        
                    if (modelList[i].UsedUnitCount != null)
                    {
                        commReMT.Parameters.Add(SqlHelper.GetParameterFromString("@ReBackNum", modelList[i].UsedUnitCount));//回写增加的数量
                    }
                    else
                    {
                        commReMT.Parameters.Add(SqlHelper.GetParameterFromString("@ReBackNum", modelList[i].ProductCount));//回写增加的数量
                    }
                    commReMT.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", modelList[i].FromLineNo));
                    lstConfirm.Add(commReMT);//循环加入数组（"已入库数量"增加）

                    SqlCommand commPD = new SqlCommand();
                    if (Exists(modelList[i].BatchNo, modelList[i].StorageID, modelList[i].ProductID, model.CompanyCD))
                    {
                        commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, true);
                    }
                    else
                    {
                        commPD = InsertStorageProduct(modelList[i].BatchNo,modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model.CompanyCD);
                    }
                    lstConfirm.Add(commPD);
                    SqlCommand commRoad = new SqlCommand();
                    commRoad = updateRoadCount(modelList[i].ProductID, modelList[i].DefaultStorageID, modelList[i].ProductCount, model);
                    lstConfirm.Add(commRoad);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageInProcessModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInProcess SET");
            sql.AppendLine(" Closer          = @Closer,");
            sql.AppendLine(" CloseDate      = getdate(),");
            sql.AppendLine(" BillStatus              = 4,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = getdate()");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", model.Closer));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion


        #region 取消结单
        public static bool CancelCloseBill(StorageInProcessModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInProcess SET");
            sql.AppendLine(" Closer          = NULL,");
            sql.AppendLine(" CloseDate      = NULL,");
            sql.AppendLine(" BillStatus              = 2,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = getdate()");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID));

            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageInProcessModel model)
        {
            if (model.ID != null && model.ID != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            }
            //@CompanyCD,@InNo,@FromType,@FromBillID,@Title,@ProcessDept,@ProcessType,@Processor,@Executor,@EnterDate,
            //@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@DeptID,@TotalPrice,@CountTotal,@Summary
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate ", model.EnterDate));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate ", model.CreateDate));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", model.CanViewUser));//可查看人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName ", model.CanViewUserName));//可查看人Name
        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditInitailDetailInfo(SqlCommand comm, StorageInProcessDetailModel model)
        {
            //@InNo,@ProductID,@StorageID,@ProductCount,@FromLineNo,getdate(),@ModifiedUserID,@SortNo,@Remark,@CompanyCD,@UnitPrice,@TotalPrice
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//批次
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));
        }
        #endregion

        #region 更新分仓存量表
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageInProcessModel model, bool flag)
        {
            //true 表示入库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum");
                //strSql.AppendLine("RoadCount=RoadCount-@ProductNum");//减少在途量
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                if (!string.IsNullOrEmpty(BatchNo))
                {
                    strSql.Append(" and BatchNo =@BatchNo ");
                }
                else
                {
                    strSql.Append(" and (BatchNo is null or BatchNo='') ");
                }
            }
            //否则 表示（入库减少）分仓存量数据（修改的时候）
            else
            {
                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum,");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                if (!string.IsNullOrEmpty(BatchNo))
                {
                    strSql.Append(" and BatchNo =@BatchNo ");
                }
                else
                {
                    strSql.Append(" and (BatchNo is null or BatchNo='') ");
                }
            }
            SqlCommand commRePD = new SqlCommand();
            commRePD.CommandText = strSql.ToString();

            commRePD.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
            commRePD.Parameters.AddWithValue("@StorageID", StorageID);
            commRePD.Parameters.AddWithValue("@ProductID", ProductID);
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                commRePD.Parameters.AddWithValue("@BatchNo", BatchNo);
            }

            return commRePD;
        }
        #endregion

        #region 更新在途量，注意这个仓库部是你自己选的仓库，而是物品表中的主放仓库

        private static SqlCommand updateRoadCount(string ProductID, string DefaultStorageID, string ProductNum, StorageInProcessModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.AppendLine("update officedba.StorageProduct set ");
            strSql.AppendLine("RoadCount=RoadCount-@ProductNum");
            //strSql.AppendLine("RoadCount=RoadCount-@ProductNum");//减少在途量
            strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            comm.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
            comm.Parameters.AddWithValue("@StorageID", DefaultStorageID);
            comm.Parameters.AddWithValue("@ProductID", ProductID);
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            return comm;
        }

        #endregion

        #region 当分仓存量表中不存在此记录的时候，插入
        public static SqlCommand InsertStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageProduct(");
            strSql.Append("CompanyCD,StorageID,ProductID ");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(",BatchNo ");
            }
            strSql.Append(" ,ProductCount) values (");
            strSql.Append("@CompanyCD,@StorageID,@ProductID");
            if (!string.IsNullOrEmpty(BatchNo))
            {
                strSql.Append(",@BatchNo ");
            }
            strSql.Append(",@ProductNum)");
            strSql.Append(";select @@IDENTITY");

            SqlCommand commRePD = new SqlCommand();
            commRePD.CommandText = strSql.ToString();

            commRePD.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
            commRePD.Parameters.AddWithValue("@StorageID", StorageID);
            commRePD.Parameters.AddWithValue("@ProductID", ProductID);
            commRePD.Parameters.AddWithValue("@CompanyCD", CompanyCD);
            if (!string.IsNullOrEmpty(BatchNo))
            {
                commRePD.Parameters.AddWithValue("@BatchNo", BatchNo);
            }

            return commRePD;
        }
        //public static SqlCommand InsertStorageProduct(string BatchNo,string ProductID, string StorageID, string ProductNum, string CompanyCD)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into officedba.StorageProduct(");
        //    strSql.Append("CompanyCD,StorageID,ProductID,ProductCount,BatchNo)");
        //    strSql.Append(" values (");
        //    strSql.Append("@CompanyCD,@StorageID,@ProductID,@ProductNum,@BatchNo)");
        //    strSql.Append(";select @@IDENTITY");

        //    SqlCommand commRePD = new SqlCommand();
        //    commRePD.CommandText = strSql.ToString();

        //    commRePD.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
        //    commRePD.Parameters.AddWithValue("@StorageID", StorageID);
        //    commRePD.Parameters.AddWithValue("@ProductID", ProductID);
        //    commRePD.Parameters.AddWithValue("@CompanyCD", CompanyCD);
        //    commRePD.Parameters.AddWithValue("@BatchNo", BatchNo == null ? DBNull.Value.ToString() : BatchNo);
        //    return commRePD;
        //}
        #endregion

        #region 判断当前storageID中对应的ProductID记录是否存在
        public static bool Exists(string BatchNo,string storageID, string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=@storageID and ProductID=@ProductID and CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(BatchNo))
                strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
            else
                strSql.AppendLine(" and (BatchNo is null or BatchNo='') ");

            SqlParameter[] parameters = {
					new SqlParameter("@storageID", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.VarChar),
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),};
            parameters[0].Value = storageID;
            parameters[1].Value = ProductID;
            parameters[2].Value = CompanyCD;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }
        #endregion

        #region 获取生产任务单
        /// <summary>
        /// 获取生产任务单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetMTList(ManufactureTaskModel model)
        {
            string sql = "select ID,TaskNo,Subject as Title,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.ManufactureTask where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.TaskNo))
            {
                sql += " and TaskNo like '%'+ @TaskNo +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
            }
            if (!string.IsNullOrEmpty(model.Subject))
            {
                sql += " and Subject like '%'+ @Subject +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", model.Subject));
            }
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion


        #region 是否可以被确认，判断明细中入库数量是否小于源单明细未入库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageInProcessModel model)
        {
            bool Result = true;//true表示可以确认
            StringBuilder sql = new StringBuilder();
           
            sql.AppendLine("select a.ID,a.FromBillID,b.ProductID                    ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit == true)
            {
                sql.AppendLine(",b.UsedUnitCount ProductCount ");
            }
            else
            {
                sql.AppendLine(",b.ProductCount ");
            }

            sql.AppendLine(",ISNULL(l.ProductedCount,0)-ISNULL(l.InCount,0) as NotInCount ");
            sql.AppendLine(" from officedba.StorageInProcessDetail b                                             ");
            sql.AppendLine("left join officedba.StorageInProcess a on a.InNo=b.InNo                              ");
            sql.AppendLine("left join officedba.ManufactureTask k on k.ID=a.FromBillID                                ");
            sql.AppendLine("left join officedba.ManufactureTaskDetail l on l.TaskNo=k.TaskNo and l.SortNo=b.FromlineNo and l.CompanyCD=b.CompanyCD");
            sql.AppendLine(" where a.CompanyCD='" + model.CompanyCD + "' and a.ID=" + model.ID);
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (decimal.Parse(dt.Rows[i]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[i]["NotInCount"].ToString()))
                    {
                        Result = false;
                        break;
                    }
                }
            }
            return Result;
        }

        #endregion


        #region 单据打印
        public static DataTable GetStorageInProcessInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInProcess where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageInProcessDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInProcessDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageInProcessModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageInProcess set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND InNo = @InNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@InNo", model.InNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}
