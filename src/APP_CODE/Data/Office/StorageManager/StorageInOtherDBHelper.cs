/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/09
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

namespace XBase.Data.Office.StorageManager
{
    public class StorageInOtherDBHelper
    {
        #region 查询：其他入库单
        /// <summary>
        /// 查询其他入库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInOtherTableBycondition(string BatchNo,StorageInOtherModel model, string StorageID, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            //入库单编号、入库单主题、交货人、验收人、人库人、入库时间、入库原因、入库数量、入库金额、摘要、单据状态。
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                              ");
            sql.AppendLine(",ISNULL(a.InNo,'') AS InNo                                                                        ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                      ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as TakerName                                                           ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') as CheckerName                                                         ");
            sql.AppendLine(",ISNULL(j.EmployeeName,'') as ExecutorName                                                        ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate ");
            sql.AppendLine(",ISNULL(k.CodeName,'') as ReasonTypeName                                                          ");
            sql.AppendLine(",ISNULL(l.DeptName,'') as InPutDept                                                               ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                           ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                           ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                  ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'                ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end as BillStatusName                           ");
            sql.AppendLine("FROM officedba.StorageInOther a                                                             ");
            sql.AppendLine("left join officedba.StorageInOtherDetail b on a.InNo = b.InNo                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo h on h.ID=a.Taker                                                ");
            sql.AppendLine("left join officedba.EmployeeInfo i on i.ID=a.Checker                                              ");
            sql.AppendLine("left join officedba.EmployeeInfo j on j.ID=a.Executor                                             ");
            sql.AppendLine("left join officedba.CodeReasonType k on k.ID=a.ReasonType                                         ");
            sql.AppendLine("left join officedba.DeptInfo l on l.ID=a.DeptID                                              	  ");
            sql.AppendLine("left join officedba.StorageInOtherDetail m on m.InNo=a.InNo                                              	  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD  AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");

            //出库单编号、出库单主题、源单类型（下拉列表）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //入库单编号、入库单主题、交货人（弹出窗口选择）、 验收人（弹出窗口选择）、入库人（选择）
            //、入库时间（日期段，日期控件）、仓库（下拉列表）、入库部门、单据状态（下拉列表）
            if (!string.IsNullOrEmpty(model.ProjectID))
            {
                sql.AppendLine(" and a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and m.BatchNo like '%'+ @BatchNo +'%'");
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
            if (!string.IsNullOrEmpty(model.Taker))
            {
                sql.AppendLine(" and a.Taker = @Taker");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker", model.Taker));
            }
            if (!string.IsNullOrEmpty(model.Checker))
            {
                sql.AppendLine(" and a.Checker=@Checker");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Checker", model.Checker));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor=@Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
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
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInOtherDetail where StorageID=@StorageID)");
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

        public static DataTable GetStorageInOtherTableBycondition(string BatchNo,StorageInOtherModel model, string StorageID, string timeStart, string timeEnd, string orderby)
        {
            //入库单编号、入库单主题、交货人、验收人、人库人、入库时间、入库原因、入库数量、入库金额、摘要、单据状态。
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                              ");
            sql.AppendLine(",ISNULL(a.InNo,'') AS InNo                                                                        ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                      ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as TakerName                                                           ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') as CheckerName                                                         ");
            sql.AppendLine(",ISNULL(j.EmployeeName,'') as ExecutorName                                                        ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate ");
            sql.AppendLine(",ISNULL(k.CodeName,'') as ReasonTypeName                                                          ");
            sql.AppendLine(",ISNULL(l.DeptName,'') as InPutDept                                                               ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                           ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                           ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                  ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'                ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' end as BillStatusName                           ");
            sql.AppendLine("FROM officedba.StorageInOther a                                                             ");
            sql.AppendLine("left join officedba.StorageInOtherDetail b on a.InNo = b.InNo                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo h on h.ID=a.Taker                                                ");
            sql.AppendLine("left join officedba.EmployeeInfo i on i.ID=a.Checker                                              ");
            sql.AppendLine("left join officedba.EmployeeInfo j on j.ID=a.Executor                                             ");
            sql.AppendLine("left join officedba.CodeReasonType k on k.ID=a.ReasonType                                         ");
            sql.AppendLine("left join officedba.DeptInfo l on l.ID=a.DeptID                                              	  ");
            sql.AppendLine("left join officedba.StorageInOtherDetail m on m.InNo=a.InNo                                              	  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");

            //出库单编号、出库单主题、源单类型（下拉列表）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //入库单编号、入库单主题、交货人（弹出窗口选择）、 验收人（弹出窗口选择）、入库人（选择）
            //、入库时间（日期段，日期控件）、仓库（下拉列表）、入库部门、单据状态（下拉列表）
            if (!string.IsNullOrEmpty(model.ProjectID))
            {
                sql.AppendLine(" and a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and m.BatchNo like '%'+ @BatchNo +'%'");
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
            if (!string.IsNullOrEmpty(model.Taker))
            {
                sql.AppendLine(" and a.Taker = @Taker");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker", model.Taker));
            }
            if (!string.IsNullOrEmpty(model.Checker))
            {
                sql.AppendLine(" and a.Checker=@Checker");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Checker", model.Checker));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor=@Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
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
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInOtherDetail where StorageID=@StorageID)");
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

        #region 查看：其他入库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取其他入库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInOtherDetailInfo(StorageInOtherModel model)
        {
            //a->officedba.StorageInOther
            //b->officedba.StorageInOtherDetail
            //l->officedba.SellBackDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine(" a.ID ,a.CanViewUser,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10 ");
            sql.AppendLine(",a.CompanyCD                                                                                                                                           ");
            sql.AppendLine(",a.InNo,isnull(z.TypeName,'') ColorName ");
            sql.AppendLine(",a.FromType                                                                                                                                            ");
            sql.AppendLine(",a.FromBillID                                                                                                                                          ");
            sql.AppendLine(",ISNULL(k.BackNo,'') AS BackNo                                                                                                                         ");
            sql.AppendLine(",a.Title,a.ProjectID,pj.ProjectName ");
            sql.AppendLine(",a.OtherCorpID                                                                                                                                          ");
            sql.AppendLine(",ISNULL(a.CorpBigType,'') as CorpBigType ");//往来单位类型
            sql.AppendLine(",case a.FromType when 0 then ISNULL(o.CodeName,'') else ISNULL(p.CustName,'') end as OtherCorpName");
            //sql.AppendLine(",ISNULL(o.CodeName,'') as OtherCorpName");
            sql.AppendLine(",a.Purchaser                                                                                                                                           ");
            sql.AppendLine(",a.DeptID                                                                                                                                              ");
            sql.AppendLine(",j.DeptName                                                                                                                                            ");
            sql.AppendLine(",a.Taker                                                                                                                                               ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as TakerName                                                                                                                ");
            sql.AppendLine(",a.Checker                                                                                                                                             ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as CheckerName                                                                                                              ");
            sql.AppendLine(",a.Executor                                                                                                                                            ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as ExecutorName                                                                                                             ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate                                                      ");
            sql.AppendLine(",a.ReasonType                                                                                                                                          ");
            sql.AppendLine(",a.BillStatus");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                                                       ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') as A_TotalPrice                                                                                                              ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                                                ");
            sql.AppendLine(",a.Creator                                                                                                                                             ");
            sql.AppendLine(",ISNULL(g.EmployeeName,'') as CreatorName                                                                                                              ");
            sql.AppendLine(",case when a.CreateDate Is NULL then '' else CONVERT(VARCHAR(10),a.CreateDate, 21) end AS CreateDate                                                   ");
            sql.AppendLine(",a.Confirmor                                                                                                                                           ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as ConfirmorName                                                                                                            ");
            sql.AppendLine(",case when a.ConfirmDate Is NULL then '' else CONVERT(VARCHAR(10),a.ConfirmDate, 21) end AS ConfirmDate                                                ");
            sql.AppendLine(",a.Closer                                                                                                                                              ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') as CloserName                                                                                                               ");
            sql.AppendLine(",case when a.CloseDate Is NULL then '' else CONVERT(VARCHAR(10),a.CloseDate, 21) end AS CloseDate                                                      ");
            sql.AppendLine(",case when a.ModifiedDate Is NULL then '' else CONVERT(VARCHAR(10),a.ModifiedDate, 21) end AS ModifiedDate                                             ");
            sql.AppendLine(",a.ModifiedUserID                                                                                                                                      ");
            sql.AppendLine(",a.ModifiedUserID as ModifiedUserName                                                                                                                    ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark");
            sql.AppendLine(",b.ID as DetailID                                                                                                                                      ");
            sql.AppendLine(",b.ProductID                                                                                                                                           ");
            sql.AppendLine(",c.ProdNo as ProductNo                                                                                                                                 ");
            sql.AppendLine(",c.ProductName,c.IsBatchNo");
            sql.AppendLine(",c.Specification ");
            sql.AppendLine(",b.UnitPrice as UnitPrice                                                                                                                              ");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                          ");
            sql.AppendLine(",b.StorageID                                                                                                                                           ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                                                          ");
            sql.AppendLine(",b.FromType                                                                                                                                            ");
            sql.AppendLine(",b.FromBillID                                                                                                                                          ");
            sql.AppendLine(",b.FromLineNo                                                                                                                                          ");
            sql.AppendLine(",b.SortNo                                                                                                                                              ");
            sql.AppendLine(",b.UsedUnitID ");
            sql.AppendLine(",b.UsedUnitCount ");
            sql.AppendLine(",ISNULL(b.UsedPrice,0)UsedPrice ");
            sql.AppendLine(",b.ExRate ");
            sql.AppendLine(",b.BatchNo ");

            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                                               ");
            sql.AppendLine(",ISNULL(l.BackNumber,0) as FromBillCount");
            sql.AppendLine(",ISNULL(l.InNumber,0) as InNumber");//已入库数量
            sql.AppendLine(",ISNULL(b.ProductCount,0) as InCount");
            sql.AppendLine(",ISNULL(l.BackNumber,0)-ISNULL(l.InNumber,0) as NotInCount");
            sql.AppendLine("FROM officedba.StorageInOther a                                                                                                                  ");
            sql.AppendLine("left join officedba.StorageInOtherDetail b                                                                                                             ");
            sql.AppendLine("on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                                                                                                                                     ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                                                  ");
            sql.AppendLine("left join officedba.EmployeeInfo d on a.Taker=d.ID                                                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo e on a.Checker=e.ID                                                                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Executor=f.ID                                                                                                  ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID                                                                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                                                    ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                                                        ");
            sql.AppendLine("left join officedba.SellBack k on k.ID=a.FromBillID                                                                                                    ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID               ");
            sql.AppendLine("left join officedba.SellBackDetail l on l.BackNo=k.BackNo and l.SortNo=b.FromlineNo and l.CompanyCD=k.CompanyCD ");
            sql.AppendLine("left join officedba.CodeCompanyType o on a.CorpBigType=o.BigType and a.OtherCorpID=o.ID ");
            sql.AppendLine("left join officedba.CustInfo p on a.OtherCorpID=p.ID ");
            sql.AppendLine("left join officedba.ProjectInfo pj on pj.ID=a.ProjectID ");
            sql.AppendLine("left join officedba.CodePublicType z on z.ID=c.ColorID ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m on a.ModifiedUserID=m.UserID ");
            sql.AppendLine("   where b.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入其他入库和其他入库明细
        /// <summary>
        /// 插入其他入库和其他入库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageInOther(StorageInOtherModel model, List<StorageInOtherDetailModel> modelList, Hashtable htExtAttr, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.StorageInOther(");
            strSql.AppendLine("CompanyCD,InNo,Title,ReasonType,DeptID,CorpBigType,OtherCorpID,Purchaser,PayType,SendAddress,ReceiveOverAddress,CurrencyType,Rate,Taker,Checker,Executor,EnterDate,TotalPrice,CountTotal,Summary,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,FromType,FromBillID,CanViewUser,CanViewUserName,ProjectID)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@CompanyCD,@InNo,@Title,@ReasonType,@DeptID,@CorpBigType,@OtherCorpID,@Purchaser,@PayType,@SendAddress,@ReceiveOverAddress,@CurrencyType,@Rate,@Taker,@Checker,@Executor,@EnterDate,@TotalPrice,@CountTotal,@Summary,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@FromType,@FromBillID,@CanViewUser,@CanViewUserName,@ProjectID)");
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

            if (modelList != null && modelList.Count > 0)//明细为空的时候
            {
                //插入其他入库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.AppendLine("insert into officedba.StorageInOtherDetail(");
                strSqlDetail.AppendLine("InNo,ProductID,StorageID,UnitPrice,ProductCount,TotalPrice,Remark,ModifiedDate,ModifiedUserID,CompanyCD,FromLineNo,SortNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.AppendLine(" values (");
                strSqlDetail.AppendLine("@InNo,@ProductID,@StorageID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,getdate(),@ModifiedUserID,@CompanyCD,@FromLineNo,@SortNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.AppendLine(";select @@IDENTITY");
                
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInOtherDetailInfo(commDetail, modelList[i]);
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

        #region 更新其他入库及其他入库明细
        /// <summary>
        /// 更新其他入库及其他入库明细
        /// </summary>
        /// <param name="model"></param>

        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageInOther(StorageInOtherModel model, List<StorageInOtherDetailModel> modelList, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.StorageInOther set ");
            strSql.AppendLine("InNo=@InNo,");
            strSql.AppendLine("Title=@Title,");
            strSql.AppendLine("ReasonType=@ReasonType,");
            strSql.AppendLine("DeptID=@DeptID,");
            strSql.AppendLine("OtherCorpID=@OtherCorpID,");
            strSql.AppendLine("CorpBigType=@CorpBigType,");
            strSql.AppendLine("Purchaser=@Purchaser,");
            strSql.AppendLine("PayType=@PayType,");
            strSql.AppendLine("SendAddress=@SendAddress,");
            strSql.AppendLine("ReceiveOverAddress=@ReceiveOverAddress,");
            strSql.AppendLine("CurrencyType=@CurrencyType,");
            strSql.AppendLine("Rate=@Rate,");
            strSql.AppendLine("Taker=@Taker,");
            strSql.AppendLine("Checker=@Checker,");
            strSql.AppendLine("Executor=@Executor,");
            strSql.AppendLine("TotalPrice=@TotalPrice,");
            strSql.AppendLine("CountTotal=@CountTotal,");
            strSql.AppendLine("Summary=@Summary,");
            strSql.AppendLine("Remark=@Remark,");
            strSql.AppendLine("CanViewUser=@CanViewUser,");
            strSql.AppendLine("CanViewUserName=@CanViewUserName,");
            strSql.AppendLine("BillStatus=@BillStatus,");
            strSql.AppendLine("ModifiedDate=getdate(),");
            strSql.AppendLine("ModifiedUserID=@ModifiedUserID,");
            strSql.AppendLine("FromType=@FromType,");
            strSql.AppendLine("ProjectID=@ProjectID,");
            strSql.AppendLine("FromBillID=@FromBillID");
            strSql.AppendLine(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);//数组加入插入基表的command

            string delDetail = "delete from officedba.StorageInOtherDetail where CompanyCD='" + model.CompanyCD + "' and InNo='" + model.InNo + "'";
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
                strSqlDetail.AppendLine("insert into officedba.StorageInOtherDetail(");
                strSqlDetail.AppendLine("InNo,ProductID,StorageID,UnitPrice,ProductCount,TotalPrice,Remark,ModifiedDate,ModifiedUserID,CompanyCD,FromLineNo,SortNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.AppendLine(" values (");
                strSqlDetail.AppendLine("@InNo,@ProductID,@StorageID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,getdate(),@ModifiedUserID,@CompanyCD,@FromLineNo,@SortNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.AppendLine(";select @@IDENTITY");
                
                for (int i = 0; i < modelList.Count; i++)
                {

                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInOtherDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）

                }
            }

            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);


        }

        #endregion

        #region 删除：其他入库信息
        /// <summary>
        /// 删除其他入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInOther(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageInOtherDetail where InNo=(select InNo from officedba.StorageInOther where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageInOther where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageInOtherModel model, out string Msg)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInOther SET");
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

            List<StorageInOtherDetailModel> modelList = new List<StorageInOtherDetailModel>();
            string sqlSele = "select a.CompanyCD,a.ProductID,a.StorageID,a.BatchNo,a.InNo,a.UnitPrice,a.UsedUnitCount,b.OtherCorpID," +
                "convert(varchar(10),b.EnterDate,23) HappenDate,a.ProductCount,a.Remark,a.FromLineNo from officedba.StorageInOtherDetail a " +
                " left join officedba.StorageInOther b on b.InNo = a.InNo  and a.CompanyCD = b.CompanyCD " +
                " where a.CompanyCD='" + model.CompanyCD + "' and a.InNo=(select InNo from officedba.StorageInOther where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageInOtherDetailModel modelDetail = new StorageInOtherDetailModel();
                    StorageAccountModel StorageAccountM = new StorageAccountModel();

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
                        StorageAccountM.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                        StorageAccountM.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    }
                    if (dt.Rows[i]["UsedUnitCount"].ToString() != "")
                    {
                        modelDetail.UsedUnitCount = dt.Rows[i]["UsedUnitCount"].ToString();
                    }
                    if (dt.Rows[i]["FromLineNo"].ToString() != "")
                    {
                        modelDetail.FromLineNo = dt.Rows[i]["FromLineNo"].ToString();
                    }
                    

                    StorageAccountM.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    StorageAccountM.BillType = 5;
                    if (dt.Rows[i]["BatchNo"].ToString() != "")
                    {
                        modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                        StorageAccountM.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    }
                    modelList.Add(modelDetail);

                    StorageAccountM.BillNo = dt.Rows[i]["InNo"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(dt.Rows[i]["HappenDate"].ToString());
                    StorageAccountM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/StorageInOtherAdd.aspx";
                    StorageAccountM.ReMark = dt.Rows[i]["Remark"].ToString();

                    SqlCommand commSA = new SqlCommand();
                    commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                    lstConfirm.Add(commSA);
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strAddSBDetail = new StringBuilder();
                strAddSBDetail.AppendLine("update officedba.SellBackDetail set ");
                strAddSBDetail.AppendLine(" InNumber =ISNULL(InNumber,0)+@ReBackNum where ");
                strAddSBDetail.AppendLine(" BackNo=(select BackNo from officedba.SellBack where ID=(select FromBillID from officedba.StorageInOther where ID=" + model.ID + "))");
                strAddSBDetail.AppendLine(" and SortNo=@SortNo");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commReSB = new SqlCommand();
                    commReSB.CommandText = strAddSBDetail.ToString();

                    if (modelList[i].UsedUnitCount != null)
                    {                        
                        commReSB.Parameters.Add(SqlHelper.GetParameterFromString("@ReBackNum", modelList[i].UsedUnitCount));//回写增加的数量
                    }
                    else
                    {
                        commReSB.Parameters.Add(SqlHelper.GetParameterFromString("@ReBackNum", modelList[i].ProductCount));//回写增加的数量 
                    }
                    
                    commReSB.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", modelList[i].FromLineNo));
                    lstConfirm.Add(commReSB);//循环加入数组（把SellbackDetail已经入库数量增加）

                    SqlCommand commPD = new SqlCommand();
                    if (Exists(modelList[i].BatchNo,modelList[i].StorageID, modelList[i].ProductID, model.CompanyCD))
                    {
                        commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, true);
                    }
                    else
                    {
                        commPD = InsertStorageProduct(modelList[i].BatchNo,modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model.CompanyCD);
                    }
                    lstConfirm.Add(commPD);
                }
            }

            bool IsOK = true;
            IsOK = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            string retstrval = "";
            if (IsOK)
            {
                string IsVoucher = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher ? "1" : "0";
                string IsApply = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply ? "1" : "0";
                decimal TotalPri = Convert.ToDecimal(model.TotalPrice);
                DataTable dtCurrtype = XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                string CurrencyInfo = dtCurrtype.Rows[0]["ID"].ToString();
                string ExchangeRate = dtCurrtype.Rows[0]["ExchangeRate"].ToString();
                int ProviderID = 0;
                if (dt.Rows[0]["OtherCorpID"].ToString() != "")
                {
                    ProviderID = Convert.ToInt32(dt.Rows[0]["OtherCorpID"].ToString());
                }
                bool IsTure = XBase.Data.Office.FinanceManager.AutoVoucherDBHelper.AutoVoucherInsert(8, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, IsVoucher, IsApply, TotalPri, "officedba.StorageInOther," + model.ID, CurrencyInfo+","+ExchangeRate, ProviderID, out retstrval);
                if (IsTure) retstrval = "确认成功！";
                else retstrval = "确认成功！" + retstrval;
              
                Msg = retstrval;
            }
            else
            {
                Msg = "确认失败！";
            }
            return IsOK;

        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageInOtherModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInOther SET");
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
        public static bool CancelCloseBill(StorageInOtherModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInOther SET");
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

        #region 更新分仓存量表
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageInOtherModel model, bool flag)
        {
            //true 表示入库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum ");
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
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum ");
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
        public static bool Exists(string BatchNo, string storageID, string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=@storageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
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

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageInOtherModel model)
        {
            if (model.ID != null && model.ID != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType ", model.ReasonType));//入库原因（对应原因表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//部门（对应部门表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser ", model.Purchaser));//业务员ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType ", model.PayType));//结算方式ID（对应分类代码表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendAddress ", model.SendAddress));//发货地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReceiveOverAddress ", model.ReceiveOverAddress));//收货地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType ", model.CurrencyType));//币种ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate ", model.Rate));//汇率
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker ", model.Taker));//交货人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Checker ", model.Checker));//验货人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//入库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate ", model.EnterDate));//入库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//入库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", model.ModifiedDate));//最后更新日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherCorpID ", model.OtherCorpID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CorpBigType ", model.CorpBigType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", model.CanViewUser));//可查看人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName ", model.CanViewUserName));//可查看人Name
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID ", model.ProjectID));//可查看人Name
        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditInOtherDetailInfo(SqlCommand comm, StorageInOtherDetailModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID ", model.UnitID));//单位ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));

        }
        #endregion

        #region 往来单位
        public static DataTable GetCodeCompany(string CompanyCD)
        {
            string strSql = "select  id,CodeName,BigType,SupperID from  officedba.CodeCompanyType where  BigType in(1,2,5)"
                            + " and CompanyCD='" + CompanyCD + "' and UsedStatus=1";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 是否可以被确认，判断明细中入库数量是否小于源单明细未入库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageInOtherModel model)
        {
            bool Result = true;//true表示可以确认
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.FromBillID,a.FromType,b.ProductID                   ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit == true)
            {
                sql.AppendLine(",b.UsedUnitCount ProductCount ");
            }
            else
            {
                sql.AppendLine(",b.ProductCount      ");
            }
            sql.AppendLine(",ISNULL(l.BackNumber,0)-ISNULL(l.InNumber,0) as NotInCount                         ");
            sql.AppendLine(" from officedba.StorageInOtherDetail b                                             ");
            sql.AppendLine("left join officedba.StorageInOther a on a.InNo=b.InNo                              ");
            sql.AppendLine("left join officedba.SellBack k on k.ID=a.FromBillID                                ");
            sql.AppendLine("left join officedba.SellBackDetail l on l.BackNo=k.BackNo and l.SortNo=b.FromlineNo and l.CompanyCD=b.CompanyCD");
            sql.AppendLine(" where a.CompanyCD='" + model.CompanyCD + "' and a.ID=" + model.ID);
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FromType"].ToString() == "1")//如果有来源才去判断是否大于源单未入库数量
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
            }
            return Result;
        }

        #endregion

        #region 单据打印
        public static DataTable GetStorageInOtherInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInOther where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageInOtherDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInOtherDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageInOtherModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageInOther set ";
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
