/**********************************************
 * 类作用：   报损数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/29
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
using XBase.Data.Common;

namespace XBase.Data.Office.StorageManager
{
    public class StorageLossDBHelper
    {
        #region 查询：库存报损单
        /// <summary>
        /// 查询库存报损单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageLossTableBycondition(StorageLossModel model, string timeStart, string timeEnd, string TotalPriceStart, string TotalPriceEnd, string FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct a.ID                                                                        ");
            sql.AppendLine(",a.LossNo                                                                          ");
            sql.AppendLine(",a.Title                                                                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Executor                                             ");
            sql.AppendLine(",CASE isnull(i.FlowStatus,0)                                                       ");
            sql.AppendLine("WHEN 0 THEN ''                                                               ");
            sql.AppendLine("WHEN 1 THEN '待审批'                                                               ");
            sql.AppendLine("WHEN 2 THEN '审批中'                                                               ");
            sql.AppendLine("WHEN 3 THEN '审批通过'                                                             ");
            sql.AppendLine("WHEN 4 THEN '审批不通过'                                                       ");
            sql.AppendLine("WHEN 5 THEN '撤销审批' else ''                                                     ");
            sql.AppendLine("END FlowStatus                                                                     ");
            sql.AppendLine(",ISNULL(f.DeptName,'') as DeptName                                                 ");
            sql.AppendLine(",ISNULL(g.StorageName,'') as StorageName                                           ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.LossDate,21),'') AS LossDate                         ");
            sql.AppendLine(",ISNull(h.CodeName,'') as ReasonTypeName                                           ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                              ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更' ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName    ");
            sql.AppendLine("FROM officedba.StorageLoss a                                                 ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor                              ");
            sql.AppendLine(" left join officedba.StorageLossDetail x on x.LossNo=a.LossNo AND x.CompanyCD=a.CompanyCD ");
            sql.AppendLine("left join officedba.DeptInfo f on f.ID=a.DeptID                                    ");
            sql.AppendLine("left join officedba.StorageInfo g on g.ID=a.StorageID                              ");
            sql.AppendLine("left join officedba.CodeReasonType h on h.ID=a.ReasonType                          ");
            sql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance i ON a.LossNo=i.BillNo AND i.BillTypeFlag=" + ConstUtil.CODING_RULE_Storage_NO + "");
            sql.AppendLine("AND i.BillTypeCode='" + ConstUtil.CODING_RULE_StorageLoss_NO + "'							         ");
            sql.AppendLine(" AND i.ID=(SELECT max(ID) FROM officedba.FlowInstance AS j");
            sql.AppendLine(" WHERE a.ID = j.BillID AND j.BillTypeFlag = " + ConstUtil.CODING_RULE_Storage_NO + " AND j.BillTypeCode = " + ConstUtil.CODING_RULE_StorageLoss_NO + " )");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");

            // 库存报损单编号、库存报损单主题、报损部门（弹出窗口选择）、报损仓库（选择）、经办人（弹出窗口选择）、报损时间（日期段，日期控件）
            //、报损原因（下拉列表）、成本金额合计（区间）、单据状态（下拉列表）、审批状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.LossNo))
            {
                sql.AppendLine("	and a.LossNo like '%'+ @LossNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossNo", model.LossNo));
            }
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID = @StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(FlowStatus))
            {
                if (FlowStatus == "0")
                {
                    sql.AppendLine(" and i.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" and i.FlowStatus=@FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus));
                }
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.LossDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.LossDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType=@ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(TotalPriceStart))
            {
                sql.AppendLine(" and a.TotalPrice>=@TotalPriceStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceStart", TotalPriceStart));
            }

            if (!string.IsNullOrEmpty(TotalPriceEnd))
            {
                sql.AppendLine(" and a.TotalPrice<=@TotalPriceEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceEnd", TotalPriceEnd));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }


        public static DataTable GetStorageLossTableBycondition(StorageLossModel model, string IndexValue, string TxtValue, string timeStart, string timeEnd, string TotalPriceStart, string TotalPriceEnd, string FlowStatus,string BatchNo, string orderby)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT distinct a.ID                                                                        ");
            sql.AppendLine(",a.LossNo                                                                          ");
            sql.AppendLine(",a.Title                                                                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Executor                                             ");
            sql.AppendLine(",CASE isnull(i.FlowStatus,0)                                                       ");
            sql.AppendLine("WHEN 0 THEN ''                                                               ");
            sql.AppendLine("WHEN 1 THEN '待审批'                                                               ");
            sql.AppendLine("WHEN 2 THEN '审批中'                                                               ");
            sql.AppendLine("WHEN 3 THEN '审批通过'                                                             ");
            sql.AppendLine("WHEN 4 THEN '审批不通过'                                                       ");
            sql.AppendLine("WHEN 5 THEN '撤销审批' else ''                                                     ");
            sql.AppendLine("END FlowStatus                                                                     ");
            sql.AppendLine(",ISNULL(f.DeptName,'') as DeptName                                                 ");
            sql.AppendLine(",ISNULL(g.StorageName,'') as StorageName                                           ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.LossDate,21),'') AS LossDate                         ");
            sql.AppendLine(",ISNull(h.CodeName,'') as ReasonTypeName                                           ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                              ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更' ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName    ");
            sql.AppendLine("FROM officedba.StorageLoss a                                                 ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor                              ");
            sql.AppendLine(" left join officedba.StorageLossDetail x on x.LossNo=a.LossNo AND x.CompanyCD=a.CompanyCD ");
            sql.AppendLine("left join officedba.DeptInfo f on f.ID=a.DeptID                                    ");
            sql.AppendLine("left join officedba.StorageInfo g on g.ID=a.StorageID                              ");
            sql.AppendLine("left join officedba.CodeReasonType h on h.ID=a.ReasonType                          ");
            sql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance i ON a.LossNo=i.BillNo AND i.BillTypeFlag=" + ConstUtil.CODING_RULE_Storage_NO + "");
            sql.AppendLine("AND i.BillTypeCode='" + ConstUtil.CODING_RULE_StorageLoss_NO + "'							         ");
            sql.AppendLine(" AND i.ID=(SELECT max(ID) FROM officedba.FlowInstance AS j");
            sql.AppendLine(" WHERE a.ID = j.BillID AND j.BillTypeFlag = " + ConstUtil.CODING_RULE_Storage_NO + " AND j.BillTypeCode = " + ConstUtil.CODING_RULE_StorageLoss_NO + " )");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");

            // 库存报损单编号、库存报损单主题、报损部门（弹出窗口选择）、报损仓库（选择）、经办人（弹出窗口选择）、报损时间（日期段，日期控件）
            //、报损原因（下拉列表）、成本金额合计（区间）、单据状态（下拉列表）、审批状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.LossNo))
            {
                sql.AppendLine("	and a.LossNo like '%'+ @LossNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossNo", model.LossNo));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID = @StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(FlowStatus))
            {
                if (FlowStatus == "0")
                {
                    sql.AppendLine(" and i.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" and i.FlowStatus=@FlowStatus");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", FlowStatus));
                }
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.LossDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.LossDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType=@ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(TotalPriceStart))
            {
                sql.AppendLine(" and a.TotalPrice>=@TotalPriceStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceStart", TotalPriceStart));
            }

            if (!string.IsNullOrEmpty(TotalPriceEnd))
            {
                sql.AppendLine(" and a.TotalPrice<=@TotalPriceEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPriceEnd", TotalPriceEnd));
            }
            if (!string.IsNullOrEmpty(IndexValue) && !string.IsNullOrEmpty(TxtValue))
            {
                sql.AppendLine(" and a.ExtField" + IndexValue + " LIKE @TxtValue");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TxtValue", "%" + TxtValue + "%"));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查看：库存报损单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取库存报损详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageLossDetailInfo(StorageLossModel model)
        {
            //a->officedba.StorageLoss
            //b->officedba.StorageLossDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select DISTINCT a.ID,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10   ");
            sql.AppendLine(",x.StorageName,y.CodeName as ReasonTypeName,a.CompanyCD                                                                                                              ");
            sql.AppendLine(",a.LossNo,c.IsBatchNo,b.UnitID as HiddenUnitID,b.BatchNo,b.UsedUnitID,b.UsedUnitCount,b.UsedPrice,b.ExRate,ii.CodeName as UsedUnitName                                                                                                                 ");
            sql.AppendLine(",a.Title                                                                                                                  ");
            sql.AppendLine(",a.ReasonType                                                                                                             ");
            sql.AppendLine(",a.DeptID                                                                                                                 ");
            sql.AppendLine(",j.DeptName                                                                                                               ");
            sql.AppendLine(",a.StorageID");
            sql.AppendLine(",a.Attachment                                                                                                             ");
            sql.AppendLine(",a.Executor                                                                                                               ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as ExecutorName                                                                                ");
            sql.AppendLine(",case when a.LossDate Is NULL then '' else CONVERT(VARCHAR(10),a.LossDate, 21) end AS LossDate                            ");
            sql.AppendLine(",a.BillStatus                                                                                                             ");
            sql.AppendLine(",CASE when r.FlowStatus is null then 0 else r.FlowStatus end as FlowStatus");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                          ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as A_TotalPrice                                                                                 ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                   ");
            sql.AppendLine(",a.Creator                                                                                                                ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
            sql.AppendLine(",ISNULL(g.EmployeeName,'') as CreatorName                                                                                 ");
            sql.AppendLine(",case when a.CreateDate Is NULL then '' else CONVERT(VARCHAR(10),a.CreateDate, 21) end AS CreateDate                      ");
            sql.AppendLine(",a.Confirmor                                                                                                              ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as ConfirmorName                                                                               ");
            sql.AppendLine(",case when a.ConfirmDate Is NULL then '' else CONVERT(VARCHAR(10),a.ConfirmDate, 21) end AS ConfirmDate                   ");
            sql.AppendLine(",a.Closer                                                                                                                 ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') as CloserName                                                                                  ");
            sql.AppendLine(",case when a.CloseDate Is NULL then '' else CONVERT(VARCHAR(10),a.CloseDate, 21) end AS CloseDate                         ");
            sql.AppendLine(",case when a.ModifiedDate Is NULL then '' else CONVERT(VARCHAR(10),a.ModifiedDate, 21) end AS ModifiedDate                ");
            sql.AppendLine(",a.ModifiedUserID                                                                                                         ");
            sql.AppendLine(",a.ModifiedUserID as ModifiedUserName                                                                                       ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark                                                                                            ");
            sql.AppendLine(",b.ID as DetailID                                                                                                         ");
            sql.AppendLine(",b.ProductID                                                                                                              ");
            sql.AppendLine(",c.ProdNo as ProductNo                                                                                                    ");
            sql.AppendLine(",c.ProductName                                                                                                            ");
            sql.AppendLine(",c.Specification                                                                                                          ");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                     ");
            sql.AppendLine(",cast(b.UnitPrice as numeric(20,2)) as UnitPrice                                                                                                 ");
            sql.AppendLine(",cast(b.CostPrice as numeric(20,2)) as CostPrice                                                                                                 ");
            sql.AppendLine(",b.SortNo                                                                                                                 ");
            sql.AppendLine(",cast(b.CostPrice as numeric(20,2)) as B_TotalPrice                                                                                             ");
            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                  ");
            sql.AppendLine(",b.ProductCount                                                                                                           ");
            sql.AppendLine("FROM officedba.StorageLoss a                                                                                        ");
            sql.AppendLine("left join officedba.StorageLossDetail b                                                                                   ");
            sql.AppendLine("on a.LossNo=b.LossNo  and a.CompanyCD=b.CompanyCD                                                                                                    ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Executor=f.ID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID                                                                      ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                       ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                           ");
            sql.AppendLine("left join officedba.StorageInfo x on x.ID=a.StorageID ");
            sql.AppendLine("left join officedba.CodeReasonType y on a.ReasonType=y.ID ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                                                                       ");
            sql.AppendLine("left join officedba.CodeUnitType ii on ii.ID=b.UsedUnitID                                                                       ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m ");
            sql.AppendLine("on a.ModifiedUserID=m.UserID 																							                                                ");
            sql.AppendLine("LEFT OUTER JOIN officedba.FlowInstance r ON a.LossNo=r.BillNo AND r.BillTypeFlag=" + ConstUtil.CODING_RULE_Storage_NO + " AND r.BillTypeCode=" + ConstUtil.CODING_RULE_StorageLoss_NO + "");
            sql.AppendLine(" AND r.ID=(SELECT max(ID) FROM officedba.FlowInstance AS j WHERE a.ID = j.BillID AND r.BillTypeFlag =" + ConstUtil.CODING_RULE_Storage_NO + " AND r.BillTypeCode = " + ConstUtil.CODING_RULE_StorageLoss_NO + " )");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入库存报损和库存报损明细
        /// <summary>
        /// 插入库存报损和库存报损明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageLoss(StorageLossModel model, List<StorageLossDetailModel> modelList,Hashtable htExtAttr, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageLoss(");
            strSql.Append("CompanyCD,LossNo,Title,Executor,DeptID,StorageID,ReasonType,LossDate,TotalPrice,CountTotal,Summary,Remark,Attachment,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@LossNo,@Title,@Executor,@DeptID,@StorageID,@ReasonType,@LossDate,@TotalPrice,@CountTotal,@Summary,@Remark,@Attachment,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID)");
            strSql.Append(";set @IndexID = @@IDENTITY");
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

            if (modelList != null && modelList.Count > 0)//明细为空的时候
            {
                //插入库存报损明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageLossDetail(");
                strSqlDetail.Append("CompanyCD,LossNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,CostPrice,Remark,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@LossNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@CostPrice,@Remark,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditLossDetailInfo(commDetail, modelList[i]);
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
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageLossModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageLoss set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND LossNo = @LossNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@LossNo", model.LossNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 更新库存报损及库存报损明细
        /// <summary>
        /// 更新库存报损及库存报损明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageLoss(StorageLossModel model,Hashtable htExtAttr, List<StorageLossDetailModel> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageLoss set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("Title=@Title,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("StorageID=@StorageID,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.Append("LossDate=@LossDate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);//数组加入插入基表的command

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                lstUpdate.Add(cmd);
            #endregion
            //先删掉明细表中对应单据的所有数据
            string delDetail = "delete from officedba.StorageLossDetail where CompanyCD='" + model.CompanyCD + "' and LossNo='" + model.LossNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageLossDetail(");
                strSqlDetail.Append("CompanyCD,LossNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,CostPrice,Remark,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@LossNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@CostPrice,@Remark,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditLossDetailInfo(commDetail, modelList[i]);//赋参数
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }

            }
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        #endregion

        #region 更新分仓存量数据
        public static SqlCommand updateStorageProduct(string BatchNo,string ProductID, string StorageID, string ProductNum, StorageLossModel model, bool flag)
        {
            //true 表示报损增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ProductCount+@ProductNum ");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                 if (BatchNo != "")
                    strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
                else
                    strSql.AppendLine(" and BatchNo is null ");
            }
            //否则 表示（报损减少）分仓存量数据（修改的时候）
            else
            {
                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ProductCount-@ProductNum ");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                if (BatchNo != "")
                    strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
                else
                    strSql.AppendLine(" and BatchNo is null ");
            }
            SqlCommand commRePD = new SqlCommand();
            commRePD.CommandText = strSql.ToString();

            commRePD.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
            commRePD.Parameters.AddWithValue("@StorageID", StorageID);
            commRePD.Parameters.AddWithValue("@ProductID", ProductID);
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            return commRePD;

        }
        #endregion

        #region 删除：库存报损信息
        /// <summary>
        /// 删除库存报损信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageLoss(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageLossDetail where LossNo=(select LossNo from officedba.StorageLoss where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageLoss where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageLossModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            //@CompanyCD,@LossNo,@Title,@Executor,@DeptID,@StorageID,@ReasonType,@LossDate,@TotalPrice,@CountTotal,
            //@Summary,@Remark,@Attachment,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossNo ", model.LossNo));//报损单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//经办人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType ", model.ReasonType));//原因ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//报损部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossDate ", model.LossDate));//报损时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//报损金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//报损数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment ", model.Attachment));//附件地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）

        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditLossDetailInfo(SqlCommand comm, StorageLossDetailModel model)
        {
            //@CompanyCD,@LossNo,@SortNo,@ProductID,@ProductCount,@UnitPrice,@CostPrice,@Remark)

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@LossNo ", model.LossNo));//报损单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//报损单价(基本单价)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//报损数量(基本数量)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CostPrice ", model.CostPrice));//报损金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID ", model.UnitID));//基本单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));//实际单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));//实际数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));//实际单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));//比率
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//批次

        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageLossModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageLoss SET");
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


            List<StorageLossDetailModel> modelList = new List<StorageLossDetailModel>();
            string sqlSele = "select ProductID,BatchNo,LossNo,UnitPrice,CompanyCD,UsedUnitCount,ProductCount from officedba.StorageLossDetail where CompanyCD='" + model.CompanyCD + "'"
                            + "and LossNo=(select LossNo from officedba.StorageLoss where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            string sqlpri = "select StorageID from officedba.StorageLoss where ID=" + model.ID + "";
            DataTable dt1 = SqlHelper.ExecuteSql(sqlpri);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageLossDetailModel modelDetail = new StorageLossDetailModel();
                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                    }
                    if (dt.Rows[i]["ProductCount"].ToString() != "")
                    {
                        modelDetail.ProductCount = dt.Rows[i]["ProductCount"].ToString();
                    }
                    modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    if (dt.Rows[i]["UsedUnitCount"].ToString() == "")
                        modelDetail.UsedUnitCount = dt.Rows[i]["ProductCount"].ToString();
                    else
                        modelDetail.UsedUnitCount = dt.Rows[i]["UsedUnitCount"].ToString();
                    modelList.Add(modelDetail);
                    #region 操作库存流水账
                    StorageAccountModel AccountM_ = new StorageAccountModel();
                    AccountM_.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    AccountM_.BillNo = dt.Rows[i]["LossNo"].ToString();
                    AccountM_.BillType = 16;
                    AccountM_.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    AccountM_.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.HappenDate = System.DateTime.Now;
                    AccountM_.PageUrl = "../Office/StorageManager/StorageLossAdd.aspx";
                    AccountM_.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    AccountM_.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    AccountM_.StorageID = Convert.ToInt32(dt1.Rows[0]["StorageID"].ToString());
                    SqlCommand AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"1");
                    lstConfirm.Add(AccountCom_);
                    #endregion
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, model.StorageID, modelList[i].ProductCount, model, false);
                    lstConfirm.Add(commPD);
                }
            }


            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 取消确认
        public static bool CancelConfirmBill(StorageLossModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageLoss SET");
            sql.AppendLine(" Confirmor          = @Confirmor,");
            sql.AppendLine(" confirmDate      = getdate(),");
            sql.AppendLine(" BillStatus              = 1,");
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


            List<StorageLossDetailModel> modelList = new List<StorageLossDetailModel>();
            string sqlSele = "select ProductID,BatchNo,UsedUnitCount,ProductCount from officedba.StorageLossDetail where CompanyCD='" + model.CompanyCD + "'"
                            + "and LossNo=(select LossNo from officedba.StorageLoss where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageLossDetailModel modelDetail = new StorageLossDetailModel();
                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                    }
                    if (dt.Rows[i]["ProductCount"].ToString() != "")
                    {
                        modelDetail.ProductCount = dt.Rows[i]["ProductCount"].ToString();
                    }
                    modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    if (dt.Rows[i]["UsedUnitCount"].ToString() == "")
                        modelDetail.UsedUnitCount = dt.Rows[i]["ProductCount"].ToString();
                    else
                        modelDetail.UsedUnitCount = dt.Rows[i]["UsedUnitCount"].ToString();
                    modelList.Add(modelDetail);
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, model.StorageID, modelList[i].ProductCount, model, true);
                    lstConfirm.Add(commPD);
                }
            }


            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //撤销审批
            string CompanyCD = userInfo.CompanyCD;
            string BillTypeFlag = ConstUtil.CODING_RULE_Storage_NO;
            string BillTypeCode = ConstUtil.CODING_RULE_StorageLoss_NO;
            string strUserID = userInfo.UserID; ;
            DataTable dt2 = FlowDBHelper.GetFlowInstanceInfo(CompanyCD, Convert.ToInt32(BillTypeFlag), Convert.ToInt32(BillTypeCode), Convert.ToInt32(model.ID));
            if (dt.Rows.Count > 0)
            {
                string FlowInstanceID = dt2.Rows[0]["FlowInstanceID"].ToString();
                string FlowStatus = dt2.Rows[0]["FlowStatus"].ToString();
                string FlowNo = dt2.Rows[0]["FlowNo"].ToString();

                lstConfirm.Add(FlowDBHelper.CancelConfirmHis(CompanyCD, FlowInstanceID, FlowNo, BillTypeFlag, BillTypeCode, strUserID));
                lstConfirm.Add(FlowDBHelper.CancelConfirmTsk(CompanyCD, FlowInstanceID, strUserID));
                lstConfirm.Add(FlowDBHelper.CancelConfirmIns(CompanyCD, FlowNo, BillTypeFlag, BillTypeCode, model.ID, strUserID));
            }
            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageLossModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageLoss SET");
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
        public static bool CancelCloseBill(StorageLossModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageLoss SET");
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

        #region 是否可以删除
        public static bool IsDelStorageLoss(string ID, string CompanyCD)
        {
            bool result = true;
            string strSql1 = "select BillStatus from officedba.StorageLoss where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            string strSql2 = "select isnull(i.FlowStatus,0) as FlowStatus from officedba.StorageLoss a "
                             + "left join officedba.FlowInstance i ON a.LossNo=i.BillNo AND i.BillTypeFlag=" + ConstUtil.CODING_RULE_Storage_NO
                             + "AND i.BillTypeCode='" + ConstUtil.CODING_RULE_StorageLoss_NO + "'"
                             + " AND i.ID=(SELECT max(ID) FROM officedba.FlowInstance AS j"
                             + " WHERE a.ID = j.BillID AND i.BillTypeFlag = " + ConstUtil.CODING_RULE_Storage_NO + " AND i.BillTypeCode = " + ConstUtil.CODING_RULE_StorageLoss_NO + " )"
                             + " where a.ID=" + ID + " and a.CompanyCD='" + CompanyCD + "'";
            int a = int.Parse(SqlHelper.ExecuteScalar(strSql1, null).ToString());
            int b = int.Parse(SqlHelper.ExecuteScalar(strSql2, null).ToString());

            if (a != 1 || b != 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 单据打印
        public static DataTable GetStorageLossInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageLoss where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageLossDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageLossDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion

    }
}
