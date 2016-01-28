/**********************************************
 * 类作用：   红冲出库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/24
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
    public class StorageOutRedDBHelper
    {
        #region 查询：红冲出库单
        /// <summary>
        /// 查询红冲出库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutRedTableBycondition(StorageOutRedModel model, string timeStart, string timeEnd, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

            //出库单编号、出库单主题、源单类型、原始出库单、出库人、出库时间、出库原因、红冲数量、红冲金额、摘要、单据状态
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                           ");
            sql.AppendLine(",a.OutNo                                                                               ");
            sql.AppendLine(",a.Title                                                                               ");
            sql.AppendLine(",a.FromType                                                                            ");
            sql.AppendLine(",case a.FromType when '1' then '销售出库单' when '2' then '其他出库单' end as FromTypeName ");
            sql.AppendLine(",case a.FromType                                                                           ");
            sql.AppendLine("when '1' then (select distinct b.OutNo from  officedba.StorageOutSell b where b.id=a.frombillID) ");
            sql.AppendLine("when '2' then (select distinct c.OutNo from officedba.StorageOutOther c where c.id=a.frombillID) ");
            sql.AppendLine("end FromOutNo                                                                         ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                                  ");
            sql.AppendLine(",ISNULL(a.CountTotal,0) CountTotal                                                     ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                        ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                       ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'     ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName        ");
            sql.AppendLine("FROM officedba.StorageOutRed a                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor							       ");
            sql.AppendLine(" left join officedba.StorageOutRedDetail x on x.OutNo=a.OutNo and x.CompanyCD=a.CompanyCD ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");


            //：出库单编号、出库单主题、源单类型（选择）、原始出库单（选择）
            //、出库人（选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like  '%'+ @OutNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", model.OutNo));
            }
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like  '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.FromBillID))
            {
                sql.AppendLine(" and a.FromBillID = @FromBillID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID", model.FromBillID));
            }
            if (!string.IsNullOrEmpty(model.FromType))
            {
                sql.AppendLine(" and a.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.OutDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.OutDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }


        public static DataTable GetStorageOutRedTableBycondition(StorageOutRedModel model, string IndexValue, string TxtValue, string timeStart, string timeEnd,string BatchNo, string orderby)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            //出库单编号、出库单主题、源单类型、原始出库单、出库人、出库时间、出库原因、红冲数量、红冲金额、摘要、单据状态
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                            ");
            sql.AppendLine(",a.OutNo                                                                               ");
            sql.AppendLine(",a.Title                                                                               ");
            sql.AppendLine(",a.FromType                                                                            ");
            sql.AppendLine(",case a.FromType when '1' then '销售出库单' when '2' then '其他出库单' end as FromTypeName ");
            sql.AppendLine(",case a.FromType                                                                           ");
            sql.AppendLine("when '1' then (select distinct b.OutNo from  officedba.StorageOutSell b where b.id=a.frombillID) ");
            sql.AppendLine("when '2' then (select distinct c.OutNo from officedba.StorageOutOther c where c.id=a.frombillID) ");
            sql.AppendLine("end FromOutNo                                                                         ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                                  ");
            sql.AppendLine(",ISNULL(a.CountTotal,0) CountTotal                                                     ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                        ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                       ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'     ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName        ");
            sql.AppendLine("FROM officedba.StorageOutRed a                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor							       ");
            sql.AppendLine(" left join officedba.StorageOutRedDetail x on x.OutNo=a.OutNo and x.CompanyCD=a.CompanyCD ");

            sql.AppendLine("where a.CompanyCD=@CompanyCD ");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");


            //：出库单编号、出库单主题、源单类型（选择）、原始出库单（选择）
            //、出库人（选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like  '%'+ @OutNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", model.OutNo));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like  '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.FromBillID))
            {
                sql.AppendLine(" and a.FromBillID = @FromBillID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID", model.FromBillID));
            }
            if (!string.IsNullOrEmpty(model.FromType))
            {
                sql.AppendLine(" and a.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor = @Executor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor", model.Executor));
            }
            if (!string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.OutDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.OutDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
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

        #region 查看：红冲出库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取红冲出库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutRedDetailInfo(StorageOutRedModel model)
        {
            //a->officedba.StorageOutRed
            //b->officedba.StorageOutRedDetail
            //k->officedba.SellSend
            //l->officedba.SellSendDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select DISTINCT a.ID,a.CanViewUser,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10   ");
            sql.AppendLine(",x.StorageName,y.CodeName as ReasonTypeName,a.CompanyCD                                                                                                                                                                                      ");
            sql.AppendLine(",a.OutNo,c.IsBatchNo,b.UnitID as HiddenUnitID,b.BatchNo,b.UsedUnitID,b.UsedUnitCount,b.UsedPrice,b.ExRate,ii.CodeName as UsedUnitName                                                                                                                                                                                          ");
            sql.AppendLine(",case a.FromType when  '1' then '销售出库单' when '2' then '其他出库单' else '' end as FromTypeName  ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
            sql.AppendLine(",a.FromType                                                                                                                                                                                       ");
            sql.AppendLine(",a.FromBillID                                                                                                                                                                                     ");
            sql.AppendLine(",a.ReasonType");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select  bb.OutNo from officedba.StorageOutSell bb where bb.id=a.FrombillID)                                                                                                        ");
            sql.AppendLine("when '2' then (select  cc.OutNo from officedba.StorageOutOther cc where cc.id=a.FrombillID)                                                                                                       ");
            sql.AppendLine("end FromOutNo                                                                                                                                                                                     ");
            sql.AppendLine("                                                                                                                                                                                                  ");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select  CONVERT(VARCHAR(10),bb.OutDate, 21) from officedba.StorageOutSell bb where bb.id=a.FrombillID)                                                                             ");
            sql.AppendLine("when '2' then (select  CONVERT(VARCHAR(10),cc.OutDate, 21) from officedba.StorageOutOther cc where cc.id=a.FrombillID)                                                                            ");
            sql.AppendLine("end FromOutDate                                                                                                                                                                                   ");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select ISNULL(ee.DeptName,'') from officedba.StorageOutSell bb left join officedba.DeptInfo ee on bb.DeptID=ee.ID where bb.id=a.FrombillID)                                                                             ");
            sql.AppendLine("when '2' then (select ISNULL(ee.DeptName,'') from officedba.StorageOutOther cc left join officedba.DeptInfo ee on cc.DeptID=ee.ID where cc.id=a.FrombillID)                                                                            ");
            sql.AppendLine("end FromDeptName                                                                                                                                                                                   ");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select  ee.EmployeeName from officedba.StorageOutSell bb left join officedba.EmployeeInfo ee on bb.Transactor=ee.ID where bb.id=a.FrombillID)                                      ");
            sql.AppendLine("when '2' then (select  ee.EmployeeName from officedba.StorageOutOther cc left join officedba.EmployeeInfo ee on cc.Transactor=ee.ID where cc.id=a.FrombillID)                                     ");
            sql.AppendLine("end FromExecutor                                                                                                                                                                                  ");
            sql.AppendLine("                                                                                                                                                                                                  ");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select  bb.ProductCount from officedba.StorageOutSellDetail bb left join officedba.StorageOutSell ee on bb.OutNo=ee.OutNo and bb.Companycd=ee.Companycd  and bb.SortNo=b.FromLineNo where ee.id=a.FrombillID)     ");
            sql.AppendLine("when '2' then (select  bb.ProductCount from officedba.StorageOutOtherDetail  bb left join officedba.StorageOutOther  ee on bb.OutNo=ee.OutNo and bb.Companycd=ee.Companycd  and bb.SortNo=b.FromLineNo where ee.ID=a.FrombillID) ");
            sql.AppendLine("end FromBillCount                                                                                                                                                                                 ");
            sql.AppendLine(",case a.FromType                                                                                                                                                                                  ");
            sql.AppendLine("when '1' then (select  bb.Summary from officedba.StorageOutSell bb where bb.id=a.FrombillID)");
            sql.AppendLine("when '2' then (select  bb.Summary from officedba.StorageOutOther  bb where bb.id=a.FrombillID) ");
            sql.AppendLine("end FromSummary                                                                                                                                                                                 ");
            sql.AppendLine("                                                                                                                                                                                                  ");
            sql.AppendLine(",a.Title                                                                                                                                                                                          ");
            sql.AppendLine(",a.DeptID                                                                                                                                                                                         ");
            sql.AppendLine(",j.DeptName                                                                                                                                                                                       ");
            sql.AppendLine(",a.Executor                                                                                                                                                                                       ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as TransactorName                                                                                                                                                      ");
            sql.AppendLine(",case when a.OutDate Is NULL then '' else CONVERT(VARCHAR(10),a.OutDate, 21) end AS OutDate                                                                                                       ");
            sql.AppendLine(",a.BillStatus                                                                                                                                                                                     ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                                                                                                  ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') as A_TotalPrice                                                                                                                                                         ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                                                                                           ");
            sql.AppendLine(",a.Creator                                                                                                                                                                                        ");
            sql.AppendLine(",ISNULL(g.EmployeeName,'') as CreatorName                                                                                                                                                         ");
            sql.AppendLine(",case when a.CreateDate Is NULL then '' else CONVERT(VARCHAR(10),a.CreateDate, 21) end AS CreateDate                                                                                              ");
            sql.AppendLine(",a.Confirmor                                                                                                                                                                                      ");
            sql.AppendLine(",ISNULL(h.EmployeeName,'') as ConfirmorName                                                                                                                                                       ");
            sql.AppendLine(",case when a.ConfirmDate Is NULL then '' else CONVERT(VARCHAR(10),a.ConfirmDate, 21) end AS ConfirmDate                                                                                           ");
            sql.AppendLine(",a.Closer                                                                                                                                                                                         ");
            sql.AppendLine(",ISNULL(i.EmployeeName,'') as CloserName                                                                                                                                                          ");
            sql.AppendLine(",case when a.CloseDate Is NULL then '' else CONVERT(VARCHAR(10),a.CloseDate, 21) end AS CloseDate                                                                                                 ");
            sql.AppendLine(",case when a.ModifiedDate Is NULL then '' else CONVERT(VARCHAR(10),a.ModifiedDate, 21) end AS ModifiedDate                                                                                        ");
            sql.AppendLine(",a.ModifiedUserID                                                                                                                                                                                 ");
            sql.AppendLine(",a.ModifiedUserID as ModifiedUserName                                                                                                                                                               ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark                                                                                                                                                                    ");
            sql.AppendLine(",b.ID as DetailID                                                                                                                                                                                 ");
            sql.AppendLine(",b.ProductID                                                                                                                                                                                      ");
            sql.AppendLine(",c.ProdNo as ProductNo                                                                                                                                                                            ");
            sql.AppendLine(",c.ProductName                                                                                                                                                                                    ");
            sql.AppendLine(",c.Specification                                                                                                                                                                                  ");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                                                                                             ");
            sql.AppendLine(",b.UnitPrice as UnitPrice                                                                                                                                                                         ");
            sql.AppendLine(",b.StorageID                                                                                                                                                                                      ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                                                                                                     ");
            sql.AppendLine(",b.FromType                                                                                                                                                                                       ");
            sql.AppendLine(",b.FromBillID                                                                                                                                                                                     ");
            sql.AppendLine(",b.FromLineNo                                                                                                                                                                                     ");
            sql.AppendLine(",b.SortNo                                                                                                                                                                                         ");
            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                                                                                          ");
            sql.AppendLine(",b.ProductCount                                                                                                                                                                                   ");
            sql.AppendLine("FROM officedba.StorageOutRed a                                                                                                                                                              ");
            sql.AppendLine("left join officedba.StorageOutRedDetail b                                                                                                                                                         ");
            sql.AppendLine("on a.OutNo=b.OutNo  and a.CompanyCD=b.CompanyCD                                                                                                                                                                              ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                                                                                             ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Executor=f.ID                                                                                                                                             ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID                                                                                                                                              ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                                                                                            ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                                                                                               ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                                                                                                   ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                                                                                                                                               ");
            sql.AppendLine("left join officedba.StorageInfo x on x.ID=b.StorageID ");
            sql.AppendLine("left join officedba.CodeUnitType ii on ii.ID=b.UsedUnitID                                                                                                                                               ");
            sql.AppendLine("left join officedba.CodeReasonType y on a.ReasonType=y.ID ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m                                                                         ");
            sql.AppendLine("on a.ModifiedUserID=m.UserID 																							                                                                                                                        ");
            sql.AppendLine("   where b.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入红冲出库和红冲出库明细
        /// <summary>
        /// 插入红冲出库和红冲出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageOutRed(StorageOutRedModel model,Hashtable htExtAttr, List<StorageOutRedDetailModel> modelList, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageOutRed(");
            strSql.Append("CompanyCD,OutNo,Title,FromType,FromBillID,ReasonType,DeptID,Executor,OutDate,TotalPrice,CountTotal,Summary,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@ReasonType,@DeptID,@Executor,@OutDate,@TotalPrice,@CountTotal,@Summary,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@CanViewUser,@CanViewUserName)");
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
                //插入红冲出库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutRedDetail(");
                strSqlDetail.Append("CompanyCD,OutNo,SortNo,ProductID,StorageID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@OutNo,@SortNo,@ProductID,@StorageID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutRedDetailInfo(commDetail, modelList[i]);
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

        #region 更新红冲出库及红冲出库明细
        /// <summary>
        /// 更新红冲出库及红冲出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageOutRed(StorageOutRedModel model,Hashtable htExtAttr,List<StorageOutRedDetailModel> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageOutRed set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("OutDate=@OutDate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("Creator=@Creator,");
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
            string delDetail = "delete from officedba.StorageOutRedDetail where CompanyCD='" + model.CompanyCD + "' and OutNo='" + model.OutNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutRedDetail(");
                strSqlDetail.Append("CompanyCD,OutNo,SortNo,ProductID,StorageID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@OutNo,@SortNo,@ProductID,@StorageID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutRedDetailInfo(commDetail, modelList[i]);//赋参数
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }

            }
            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);
        }

        #endregion
        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageOutRedModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageOutRed set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND OutNo = @OutNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@OutNo", model.OutNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
        #region 更新分仓存量数据
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageOutRedModel model, bool flag)
        {
            //true 表示出库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                if (BatchNo != "")
                    strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
                else
                    strSql.AppendLine(" and BatchNo is null ");
            }
            //否则 表示（出库减少）分仓存量数据（修改的时候）
            else
            {
                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum");
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

        #region 删除：红冲出库信息
        /// <summary>
        /// 删除红冲出库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageOutRed(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageOutRedDetail where OutNo=(select OutNo from officedba.StorageOutRed where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageOutRed where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageOutRedModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            //@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@ReasonType,@DeptID,@Executor,@OutDate,@TotalPrice,
            //@CountTotal,@Summary,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//出库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//源单类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//源单ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType ", model.ReasonType));//原因ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//出库部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//出库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//出库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//出库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate ", model.OutDate));//出库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", model.CanViewUser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName ", model.CanViewUserName));
        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditOutRedDetailInfo(SqlCommand comm, StorageOutRedDetailModel model)
        {
            //@CompanyCD,@OutNo,@SortNo,@ProductID,@StorageID,@UnitPrice,
            //@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID)

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//出库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//出库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//出库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//出库金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID ", model.UnitID));//基本单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));//实际单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));//实际数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));//实际单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));//比率
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//批次

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//


        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageOutRedModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutRed SET");
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


            List<StorageOutRedDetailModel> modelList = new List<StorageOutRedDetailModel>();
            string sqlSele = "select ProductID,CompanyCD,StorageID,UnitPrice,OutNo,BatchNo,UsedUnitCount,ProductCount from officedba.StorageOutRedDetail where CompanyCD='" + model.CompanyCD + "'"
                            + "and OutNo=(select OutNo from officedba.StorageOutRed where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageOutRedDetailModel modelDetail = new StorageOutRedDetailModel();
                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                    }
                    if (dt.Rows[i]["StorageID"].ToString() != "")
                    {
                        modelDetail.StorageID = dt.Rows[i]["StorageID"].ToString();
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
                    AccountM_.BillNo = dt.Rows[i]["OutNo"].ToString();
                    AccountM_.BillType = 9;
                    AccountM_.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    AccountM_.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.HappenDate = System.DateTime.Now;
                    AccountM_.PageUrl = "../Office/StorageManager/StorageOutRedAdd.aspx";
                    AccountM_.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    AccountM_.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    AccountM_.StorageID = Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    SqlCommand AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"0");
                    lstConfirm.Add(AccountCom_);
                    #endregion
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, true);
                    lstConfirm.Add(commPD);
                }
            }
            foreach (SqlCommand cmd in GetOutFromBillInfo(model.CompanyCD, model.ID))
            {
                lstConfirm.Add(cmd);
            }

            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageOutRedModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutRed SET");
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
        public static bool CancelCloseBill(StorageOutRedModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutRed SET");
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

        #region 获取出库单列表
        /// <summary>
        /// 获取出库单列表
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetStorageOutList(StorageOutRedModel model, string OutType)
        {
            string sql = string.Empty;
            switch (OutType)
            {
                case "1":
                    {
                        sql = "select ID,'销售出库单' as FromType,OutNo,Title,CountTotal,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.StorageOutSell where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
                    }
                    break;
                case "2":
                    {
                        sql = "select ID,'其他出库单' as FromType,OutNo,Title,CountTotal,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.StorageOutOther where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
                    }
                    break;
                default:
                    break;
            }
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql += " and OutNo like '%'+ @OutNo +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", model.OutNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql += " and Title like '%'+ @Title +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查出对应出库类型的基本信息,弹出层信息
        /// <summary>
        /// 查出对应出库类型的基本信息,弹出层信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OutType"></param>
        /// <returns></returns>
        public static DataTable GetOutTypeInfo(string CompanyCD, string OutType, string OutNo, string Title)
        {
            DateTime nowDate = System.DateTime.Now;
            string monthdays = DateTime.DaysInMonth(nowDate.Year, nowDate.Month).ToString();
            string DateMonth = nowDate.ToString("yyyy-MM");
            string EndDate = DateMonth + "-" + monthdays;//本月的结束日期
            string StartDate = DateMonth + "-" + "01";//本月的开始日期（1号）
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            switch (OutType)
            {
                case "1":
                    strSql.AppendLine("select ID,");
                    strSql.AppendLine("ISNULL(OutNo,'') as OutNo,'销售出库单' as OutType,");
                    strSql.AppendLine("ISNULL(Title,'') as Title,");
                    strSql.AppendLine("ISNULL(CountTotal,0) as CountTotal");
                    strSql.AppendLine("from officedba.StorageOutSell where CompanyCD='" + CompanyCD + "' and BillStatus=2");
                    strSql.AppendLine(" and ConfirmDate>='" + StartDate + "'");
                    strSql.AppendLine(" and ConfirmDate<='" + EndDate + "'");
                    if (!string.IsNullOrEmpty(OutNo))
                    {
                        strSql.AppendLine(" and OutNo like '%'+ @OutNo +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNo));
                    }
                    if (!string.IsNullOrEmpty(Title))
                    {
                        strSql.AppendLine(" and Title like '%'+ @Title +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
                    }
                    comm.CommandText = strSql.ToString();
                    break;
                case "2":
                    strSql.AppendLine("select ID,");
                    strSql.AppendLine("ISNULL(OutNo,'') as OutNo,'其他出库单' as OutType,");
                    strSql.AppendLine("ISNULL(Title,'') as Title,");
                    strSql.AppendLine("ISNULL(CountTotal,0) as CountTotal");
                    strSql.AppendLine("from officedba.StorageOutOther where CompanyCD='" + CompanyCD + "' and BillStatus=2");
                    strSql.AppendLine(" and ConfirmDate>='" + StartDate + "'");
                    strSql.AppendLine(" and ConfirmDate<='" + EndDate + "'");
                    if (!string.IsNullOrEmpty(OutNo))
                    {
                        strSql.AppendLine(" and OutNo like '%'+ @OutNo +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNo));
                    }
                    if (!string.IsNullOrEmpty(Title))
                    {
                        strSql.AppendLine(" and Title like '%'+ @Title +'%'");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
                    }
                    comm.CommandText = strSql.ToString();
                    break;
                default:
                    break;
            }
            return SqlHelper.ExecuteSearch(comm);
        }

        #endregion

        #region 根据的出库编号查询出相关信息及其明细
        /// <summary>
        /// 根据的出库编号查询出相关信息及其明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="OutType"></param>
        /// <param name="OutNo"></param>
        /// <returns></returns>
        public static DataTable GetDetailInfo(string CompanyCD, string OutType, string OutNo)
        {
            string tablename = "";
            string detailtablename = "";
            StringBuilder strSql = new StringBuilder();

            switch (OutType)
            {
                case "1":
                    tablename = "officedba.StorageOutSell";
                    detailtablename = "officedba.StorageOutSellDetail";
                    break;
                case "2":
                    tablename = "officedba.StorageOutOther";
                    detailtablename = "officedba.StorageOutOtherDetail";
                    break;
                case "":
                    return null;
                default:
                    break;
            }
            strSql.AppendLine("select a.ID,a.OutNo");
            strSql.AppendLine(",a.Title");
            strSql.AppendLine(",a.Summary");
            strSql.AppendLine(",ISNULL(a.CountTotal,0) as CountTotal   ");
            strSql.AppendLine(",a.DeptID                                                         ");
            strSql.AppendLine(",r.EmployeeName as Transactor                                     ");
            strSql.AppendLine(",case when a.OutDate Is NULL then '' else CONVERT(VARCHAR(10),a.OutDate, 21) end AS OutDate");
            strSql.AppendLine(",ISNULL(s.DeptName,'') as DeptName                                ");
            strSql.AppendLine(",b.ProductID                                                      ");
            strSql.AppendLine(",p.ProdNo                                                         ");
            strSql.AppendLine(",p.ProductName                                                    ");
            strSql.AppendLine(",p.Specification                                                  ");
            strSql.AppendLine(",q.CodeName                                                       ");
            strSql.AppendLine(",b.ProductCount,b.UsedUnitID,b.UsedUnitCount,b.UsedPrice,b.ExRate,b.UnitID as HiddenUnitID,p.IsBatchNo                                                   ");
            strSql.AppendLine(",b.UnitPrice                                                      ");
            strSql.AppendLine(",b.StorageID                                                      ");
            strSql.AppendLine(",b.SortNo                                                         ");
            strSql.AppendLine(",b.Remark                                                         ");
            strSql.AppendLine("from " + tablename + " a                                          ");
            strSql.AppendLine("left join " + detailtablename + " b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD           ");
            strSql.AppendLine("left join officedba.ProductInfo p on p.ID=b.ProductID             ");
            strSql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID               ");
            strSql.AppendLine("left join officedba.EmployeeInfo r on r.ID=a.Transactor           ");
            strSql.AppendLine("left join officedba.DeptInfo s on s.ID=a.DeptID	        	 	 ");
            strSql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.OutNo='" + OutNo + "' ");

            return SqlHelper.ExecuteSql(strSql.ToString());
        }
        #endregion


        #region 单据打印
        public static DataTable GetStorageOutRedInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutRed where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageOutRedDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutRedDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion


        #region 确认的时候，回写销售发货，采购退货单(其他出库单且有来源的时候)

        /// <summary>
        /// CompanyCD ProductCount FromBillID FromType   FromLineNo      FromBillNo       FromFlag
        /// C1002	   10.0000     	7	        1	        1	        CK2009070018 	1
        /// 这里得到是还是原始入库单，比如采购入库单，FroType(1:销售；2：其他)，FromFlag（当FromType为2且无来源的时候为0，其他时候都是1）
        /// FromLineNo 是 原始入库单的SortNo
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ArrayList GetOutFromBillInfo(string CompanyCD, string ID)
        {
            ArrayList listcmmd = new ArrayList();
            string TableName = string.Empty;//明细表名字
            string column = string.Empty;//更新的字段（已入库字段名）
            string BillNoName = string.Empty;//编号字段名

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select a.CompanyCD,a.UsedUnitCount,a.ProductCount,b.FromBillID,b.FromType,a.FromLineNo,                                                   ");
            Sql.AppendLine("case b.FromType when 1 then c.OutNo when 2 then d.OutNo end as FromBillNo,                                                ");
            Sql.AppendLine("case b.FromType when 1 then c.FromType when 2 then d.FromType end as FromFlag--1说明是有来源，0无来源（主要针对其他入库） ");
            Sql.AppendLine("from officedba.StorageOutRedDetail a                                                                                      ");
            Sql.AppendLine("inner join officedba.StorageOutRed b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD                                       ");
            Sql.AppendLine("left join officedba.StorageOutSell c on b.FromBillID=c.ID and b.CompanyCD=c.CompanyCD                                     ");
            Sql.AppendLine("left join officedba.StorageOutOther d on b.FromBillID=d.ID and b.CompanyCD=d.CompanyCD                                    ");
            Sql.AppendLine("where b.ID=" + ID + " and  a.CompanyCD='" + CompanyCD + "'                                                                                       ");
            DataTable dt = SqlHelper.ExecuteSql(Sql.ToString());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["FromType"].ToString())
                    {
                        case "1":
                            TableName = "officedba.SellSendDetail";
                            column = "OutCount";
                            BillNoName = "SendNo";
                            DataTable dt1 = GetSSDetailInfo(dr["FromBillNo"].ToString(), dr["FromLineNo"].ToString(), CompanyCD);
                            if (dt1.Rows.Count > 0)
                            {
                                if (dr["UsedUnitCount"].ToString() == "")
                                    listcmmd.Add(CmdComfirmReturn(dr["ProductCount"].ToString(), TableName, column, BillNoName, dt1.Rows[0]["SendNo"].ToString(), dt1.Rows[0]["SortNo"].ToString(), CompanyCD));
                                else
                                    listcmmd.Add(CmdComfirmReturn(dr["UsedUnitCount"].ToString(), TableName, column, BillNoName, dt1.Rows[0]["SendNo"].ToString(), dt1.Rows[0]["SortNo"].ToString(), CompanyCD));
                            }

                            break;
                        case "2":
                            if (dr["FromFlag"].ToString() != "0")//其他出库单的时候，要判断有来源才回写
                            {
                                TableName = "officedba.PurchaseRejectDetail";
                                column = "OutedTotal";
                                BillNoName = "RejectNo";
                                DataTable dt3 = GetSBDetailInfo(dr["FromBillNo"].ToString(), dr["FromLineNo"].ToString(), CompanyCD);
                                if (dt3.Rows.Count > 0)
                                {
                                    if (dr["UsedUnitCount"].ToString() == "")
                                        listcmmd.Add(CmdComfirmReturn(dr["ProductCount"].ToString(), TableName, column, BillNoName, dt3.Rows[0]["RejectNo"].ToString(), dt3.Rows[0]["SortNo"].ToString(), CompanyCD));
                                    else
                                        listcmmd.Add(CmdComfirmReturn(dr["UsedUnitCount"].ToString(), TableName, column, BillNoName, dt3.Rows[0]["RejectNo"].ToString(), dt3.Rows[0]["SortNo"].ToString(), CompanyCD));
                                    
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return listcmmd;

        }
        /// <summary>
        /// 销售出库的时候，根据出库单获取销售发货明细信息（单条记录）
        /// </summary>
        /// <param name="InNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSSDetailInfo(string OutNo, string sortNo, string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select c.SendNo, d.SortNo ,c.CompanyCD,b.OutNo                                                                           ");
            Sql.AppendLine("from officedba.StorageOutSellDetail a                                                                                  ");
            Sql.AppendLine("inner join officedba.StorageOutSell b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD                                     ");
            Sql.AppendLine("inner join officedba.SellSend c on c.ID=b.FromBillID and a.CompanyCD=c.CompanyCD                                    ");
            Sql.AppendLine("inner join officedba.SellSendDetail d on c.SendNo=d.SendNo and a.CompanyCD=d.CompanyCD and a.FromLineNo=d.SortNo");
            Sql.AppendLine("where b.OutNo='" + OutNo + "' and a.CompanyCD='" + CompanyCD + "' and a.SortNo=" + sortNo + "                                           ");
            return SqlHelper.ExecuteSql(Sql.ToString());
        }


        /// <summary>
        /// 其他出库出库的时候，根据出库单获取采购退货明细信息（单条记录）
        /// </summary>
        /// <param name="InNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSBDetailInfo(string OutNo, string sortNo, string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select c.RejectNo, d.SortNo ,c.CompanyCD,b.OutNo                                                                   ");
            Sql.AppendLine("from officedba.StorageOutOtherDetail a                                                                           ");
            Sql.AppendLine("inner join officedba.StorageOutOther b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD                              ");
            Sql.AppendLine("inner join officedba.PurchaseReject c on c.ID=b.FromBillID and a.CompanyCD=c.CompanyCD                                ");
            Sql.AppendLine("inner join officedba.PurchaseRejectDetail d on c.RejectNo=d.RejectNo and a.CompanyCD=d.CompanyCD and a.FromLineNo=d.SortNo");
            Sql.AppendLine("where b.OutNo='" + OutNo + "' and a.CompanyCD='" + CompanyCD + "' and a.SortNo=" + sortNo + "                     ");
            return SqlHelper.ExecuteSql(Sql.ToString());
        }

        /// <summary>
        /// 回写数据
        /// </summary>
        /// <param name="ProductCount">回写的数量（也是已入库数量需要减掉的数量）</param>
        /// <param name="TableName">明细表的表明Example：officedba.SellSendDetail</param>
        /// <param name="column">回写的那字段名:example:InCount</param>
        /// <param name="BillNoName">单据编号那字段名：example：SendNo</param>
        /// <param name="BillNo">单据编号参数:DHTZD0001</param>
        /// <param name="sortNo">明细行号</param>
        /// <param name="CompanyCD">公司编号</param>
        /// <returns></returns>
        public static SqlCommand CmdComfirmReturn(string ProductCount, string TableName, string column, string BillNoName, string BillNo, string sortNo, string CompanyCD)
        {
            StringBuilder StrSql = new StringBuilder();
            StrSql.AppendLine("Update " + TableName + " set " + column + "= ISNULL(" + column + ",0)-" + ProductCount + " where ");
            StrSql.AppendLine(" CompanyCD='" + CompanyCD + "' and " + BillNoName + " like '" + BillNo + "' and SortNo=" + sortNo);
            SqlCommand cmmd = new SqlCommand();
            cmmd.CommandText = StrSql.ToString();
            return cmmd;
        }

        #endregion
    }
}
