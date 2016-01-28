/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/16
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
    public class StorageInRedDBHelper
    {
        #region 查询：红冲入库单
        /// <summary>
        /// 查询红冲入库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInRedTableBycondition(string BatchNo,StorageInRedModel model, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            //列表：入库单编号、入库单主题、源单类型、原始入库单、入库部门、人库人、入库时间、红冲数量、红冲金额、摘要、单据状态。
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select DISTINCT x.*,ISNULL(l.DeptName,'') as DeptName,ISNULL(m.EmployeeName,'') as ExecutorName from                                                                                                    ");
            sql.AppendLine("(select a.ID                                                                                                                                                                    ");
            sql.AppendLine(",ISNULL(c.CodeName,'') as CodeName");
            sql.AppendLine(",ISNULL(a.InNo,'') AS InNo                                                                                                                                                      ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                                                                                                    ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                                                                                                         ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                                                                                                         ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                                                                                                ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName              ");
            sql.AppendLine(",case a.FromType when '1' then '采购入库单' when '2' then '生产完工入库单' when '3' then '其他入库单' end FromType                                                              ");
            sql.AppendLine(",case a.fromtype                                                                                                                                                                ");
            sql.AppendLine("when '1' then (select distinct b.InNO from  officedba.StorageInPurchase b where b.id=a.frombillID)                                                                              ");
            sql.AppendLine("when '2' then (select distinct c.InNO from officedba.StorageInProcess c where c.id=a.frombillID)                                                                                ");
            sql.AppendLine("when '3' then (select distinct d.InNO from officedba.StorageInOther d where d.id=a.frombillID)                                                                                  ");
            sql.AppendLine("end FromInNo,                                                                                                                                                                   ");
            sql.AppendLine("case a.fromtype                                                                                                                                                                 ");
            sql.AppendLine("when '1' then (select distinct ISNULL(b.DeptID,'') from  officedba.StorageInPurchase b where b.id=a.frombillID)                                                                            ");
            sql.AppendLine("when '2' then (select distinct ISNULL(c.DeptID,'') from officedba.StorageInProcess c where c.id=a.frombillID)                                                                              ");
            sql.AppendLine("when '3' then (select distinct ISNULL(d.DeptID,'') from officedba.StorageInOther d where d.id=a.frombillID)                                                                                ");
            sql.AppendLine("end DeptID,                                                                                                                                                                     ");
            sql.AppendLine("a.Executor,                                                                                                                                                                   ");
            sql.AppendLine("case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end as EnterDate");
            //sql.AppendLine("case a.fromtype                                                                                                                                                                 ");
            //sql.AppendLine("when '1' then (select distinct case when b.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),b.EnterDate, 21) end from officedba.StorageInPurchase b where b.id=a.frombillID)  ");
            //sql.AppendLine("when '2' then (select distinct case when c.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),c.EnterDate, 21) end from officedba.StorageInProcess c where c.id=a.frombillID)   ");
            //sql.AppendLine("when '3' then (select distinct case when d.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),d.EnterDate, 21) end from officedba.StorageInOther d where d.id=a.frombillID)     ");
            //sql.AppendLine("end EnterDate                                                                                                                                                                   ");
            sql.AppendLine(" from officedba.StorageInRed a ");
            sql.AppendLine(" left join officedba.CodeReasonType as c on a.ReasonType=c.ID");
            sql.AppendLine(" left join officedba.StorageInRedDetail as d on d.InNo=a.InNo ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //查询条件：入库单编号、入库单主题、源单类型（选择）、原始入库单（选择）、入库部门、入库人（选择）、入库时间（日期段，日期控件）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine("	and d.BatchNo like '%'+ @BatchNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.InNo))
            {
                sql.AppendLine("	and a.InNo like '%'+ @InNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", model.InNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
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
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType = @ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
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
            if (!string.IsNullOrEmpty(model.EFIndex) && !string.IsNullOrEmpty(model.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + model.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + model.EFDesc + "%"));
            }
            sql.AppendLine(") x                                                                                        ");
            sql.AppendLine("left join officedba.DeptInfo l on l.ID=x.DeptID                                            ");
            sql.AppendLine("left join officedba.EmployeeInfo m on m.ID=x.Executor");

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }


        public static DataTable GetStorageInRedTableBycondition(string BatchNo,StorageInRedModel model, string timeStart, string timeEnd, string orderby)
        {
            //列表：入库单编号、入库单主题、源单类型、原始入库单、入库部门、人库人、入库时间、红冲数量、红冲金额、摘要、单据状态。
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select x.*,ISNULL(l.DeptName,'') as DeptName,ISNULL(m.EmployeeName,'') as ExecutorName from                                                                                                    ");
            sql.AppendLine("(select a.ID                                                                                                                                                                    ");
            sql.AppendLine(",ISNULL(c.CodeName,'') as CodeName");
            sql.AppendLine(",ISNULL(a.InNo,'') AS InNo                                                                                                                                                      ");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title                                                                                                                                                    ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') AS CountTotal                                                                                                                                         ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') AS TotalPrice                                                                                                                                         ");
            sql.AppendLine(",ISNULL(a.Summary,'') AS Summary                                                                                                                                                ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName              ");
            sql.AppendLine(",case a.FromType when '1' then '采购入库单' when '2' then '生产完工入库单' when '3' then '其他入库单' end FromType                                                              ");
            sql.AppendLine(",case a.fromtype                                                                                                                                                                ");
            sql.AppendLine("when '1' then (select distinct b.InNO from  officedba.StorageInPurchase b where b.id=a.frombillID)                                                                              ");
            sql.AppendLine("when '2' then (select distinct c.InNO from officedba.StorageInProcess c where c.id=a.frombillID)                                                                                ");
            sql.AppendLine("when '3' then (select distinct d.InNO from officedba.StorageInOther d where d.id=a.frombillID)                                                                                  ");
            sql.AppendLine("end FromInNo,                                                                                                                                                                   ");
            sql.AppendLine("case a.fromtype                                                                                                                                                                 ");
            sql.AppendLine("when '1' then (select distinct ISNULL(b.DeptID,'') from  officedba.StorageInPurchase b where b.id=a.frombillID)                                                                            ");
            sql.AppendLine("when '2' then (select distinct ISNULL(c.DeptID,'') from officedba.StorageInProcess c where c.id=a.frombillID)                                                                              ");
            sql.AppendLine("when '3' then (select distinct ISNULL(d.DeptID,'') from officedba.StorageInOther d where d.id=a.frombillID)                                                                                ");
            sql.AppendLine("end DeptID,                                                                                                                                                                     ");
            sql.AppendLine("a.Executor,                                                                                                                                                                   ");
            sql.AppendLine("case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end as EnterDate");
            sql.AppendLine(" from officedba.StorageInRed a ");
            sql.AppendLine(" left join officedba.CodeReasonType as c on a.ReasonType=c.ID");
            sql.AppendLine(" left join officedba.StorageInRedDetail as d on d.InNo=a.InNo ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //查询条件：入库单编号、入库单主题、源单类型（选择）、原始入库单（选择）、入库部门、入库人（选择）、入库时间（日期段，日期控件）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine("	and d.BatchNo like '%'+ @BatchNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.InNo))
            {
                sql.AppendLine("	and a.InNo like '%'+ @InNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", model.InNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
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
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType = @ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
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
            sql.AppendLine(") x                                                                                        ");
            sql.AppendLine("left join officedba.DeptInfo l on l.ID=x.DeptID                                            ");
            sql.AppendLine("left join officedba.EmployeeInfo m on m.ID=x.Executor");

            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查看：红冲入库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取红冲入库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInRedDetailInfo(StorageInRedModel model)
        {
            //a->officedba.StorageInRed
            //b->officedba.StorageInRedDetail
            //l->officedba.SellBackDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT ");
            sql.AppendLine("a.ID ,a.CanViewUser,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10 ");
            sql.AppendLine(",a.CompanyCD                                                                                                              ");
            sql.AppendLine(",a.InNo                                                                                                                   ");
            sql.AppendLine(",a.FromType                                                                                                               ");
            sql.AppendLine(",a.FromBillID                                                                                                             ");
            sql.AppendLine(",a.ReasonType   ");
            sql.AppendLine(",case a.FromType                                                                                                           ");
            sql.AppendLine("when '1' then (select  bb.InNo from officedba.StorageInPurchase bb where bb.id=a.FrombillID)                             ");
            sql.AppendLine("when '2' then (select  cc.InNo from officedba.StorageInProcess cc where cc.id=a.FrombillID)                               ");
            sql.AppendLine("when '3' then (select  dd.InNo from officedba.StorageInOther dd where dd.id=a.FrombillID)                                 ");
            sql.AppendLine("end FromInNo                                                                                                             ");

            sql.AppendLine(",case a.FromType                                                                                                           ");
            sql.AppendLine("when '1' then (select  CONVERT(VARCHAR(10),bb.EnterDate, 21) from officedba.StorageInPurchase bb where bb.id=a.FrombillID)                             ");
            sql.AppendLine("when '2' then (select  CONVERT(VARCHAR(10),cc.EnterDate, 21) from officedba.StorageInProcess cc where cc.id=a.FrombillID)                               ");
            sql.AppendLine("when '3' then (select  CONVERT(VARCHAR(10),dd.EnterDate, 21) from officedba.StorageInOther dd where dd.id=a.FrombillID)                                 ");
            sql.AppendLine("end FromEnterDate                                                                                                             ");

            sql.AppendLine(",case a.FromType                                                                                                           ");
            sql.AppendLine("when '1' then (select  ee.EmployeeName from officedba.StorageInPurchase bb left join officedba.EmployeeInfo ee on bb.Executor=ee.ID where bb.id=a.FrombillID)                             ");
            sql.AppendLine("when '2' then (select  ee.EmployeeName from officedba.StorageInProcess cc left join officedba.EmployeeInfo ee on cc.Executor=ee.ID where cc.id=a.FrombillID)                               ");
            sql.AppendLine("when '3' then (select  ee.EmployeeName from officedba.StorageInOther dd left join officedba.EmployeeInfo ee on dd.Executor=ee.ID where dd.id=a.FrombillID)                                 ");
            sql.AppendLine("end FromExecutor                                                                                                             ");

            sql.AppendLine(",case a.FromType                                                                                                           ");
            sql.AppendLine("when '1' then (select  bb.ProductCount from officedba.StorageInPurchaseDetail bb left join officedba.StorageInPurchase ee on bb.InNo=ee.InNo and bb.SortNo=b.FromLineNo where ee.id=a.FrombillID)");
            sql.AppendLine("when '2' then (select  bb.ProductCount from officedba.StorageInProcessDetail  bb left join officedba.StorageInProcess  ee on bb.InNo=ee.InNo and bb.SortNo=b.FromLineNo where ee.ID=a.FrombillID)");
            sql.AppendLine("when '3' then (select  bb.ProductCount from officedba.StorageInOtherDetail bb left join officedba.StorageInOther ee on ee.InNo=bb.InNo and bb.SortNo=b.FromLineNo where ee.id=a.FrombillID)      ");
            sql.AppendLine("end FromBillCount                                                                                                             ");

            sql.AppendLine(",case a.FromType                                                                                                           ");
            sql.AppendLine("when '1' then (select  ISNULL(bb.Summary,'') from officedba.StorageInPurchase bb where bb.id=a.FrombillID)                             ");
            sql.AppendLine("when '2' then (select  ISNULL(cc.Summary,'') from officedba.StorageInProcess cc where cc.id=a.FrombillID)                               ");
            sql.AppendLine("when '3' then (select  ISNULL(dd.Summary,'') from officedba.StorageInOther dd where dd.id=a.FrombillID)                                 ");
            sql.AppendLine("end FromSummary                                                                                                             ");

            sql.AppendLine(",a.Title                                                                                                                  ");
            sql.AppendLine(",a.DeptID                                                                                                                 ");
            sql.AppendLine(",j.DeptName                                                                                                               ");
            sql.AppendLine(",a.Executor                                                                                                               ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as ExecutorName                                                                                ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate                         ");
            sql.AppendLine(",a.BillStatus                                                                                                             ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                          ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') as A_TotalPrice                                                                                 ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                   ");
            sql.AppendLine(",a.Creator                                                                                                                ");
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
            sql.AppendLine(",ISNULL(c.MinusIs,0) as MinusIs");
            sql.AppendLine(",c.Specification,c.IsBatchNo  ");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                          ");
            sql.AppendLine(",isnull(b.UnitPrice,0) as UnitPrice                                                                                                 ");
            sql.AppendLine(",b.StorageID                                                                                                              ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                             ");
            sql.AppendLine(",b.FromType                                                                                                               ");
            sql.AppendLine(",b.FromBillID                                                                                                             ");
            sql.AppendLine(",b.FromLineNo                                                                                                             ");
            sql.AppendLine(",b.SortNo                                                                                                                 ");
            sql.AppendLine(",b.UsedUnitID ");
            sql.AppendLine(",b.UsedUnitCount ");
            sql.AppendLine(",isnull(b.UsedPrice,0)UsedPrice ");
            sql.AppendLine(",b.ExRate ");
            sql.AppendLine(",b.BatchNo ");
            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                  ");
            sql.AppendLine(",b.ProductCount                                                                                             ");
            sql.AppendLine(" ,ISNULL(s.ProductCount,0)+ISNULL(s.RoadCount,0)+ISNULL(s.InCount,0)-ISNULL(s.OrderCount,0)-ISNULL(s.OutCount,0) as UseCount ");
            sql.AppendLine("FROM officedba.StorageInRed a                                                                                       ");
            sql.AppendLine("left join officedba.StorageInRedDetail b                                                                                  ");
            sql.AppendLine("on a.InNo=b.InNo  and a.CompanyCD=b.CompanyCD                                                                                                         ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Executor=f.ID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID                                                                      ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                       ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                           ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID               ");
            sql.AppendLine("left join officedba.StorageProduct s on s.CompanyCD=a.CompanyCD and s.StorageID=b.StorageID and b.ProductID=s.ProductID  and s.BatchNo = b.BatchNo ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m ");
            sql.AppendLine("on a.ModifiedUserID=m.UserID 																							  ");
            sql.AppendLine("   where b.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入红冲入库和红冲入库明细
        /// <summary>
        /// 插入红冲入库和红冲入库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageInRed(StorageInRedModel model, List<StorageInRedDetailModel> modelList, out int IndexIDentity, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageInRed(");
            strSql.Append("CompanyCD,InNo,Title,FromType,FromBillID,ReasonType,DeptID,Executor,EnterDate,TotalPrice,CountTotal,Summary,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@InNo,@Title,@FromType,@FromBillID,@ReasonType,@DeptID,@Executor,@EnterDate,@TotalPrice,@CountTotal,@Summary,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@CanViewUser,@CanViewUserName)");
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
                //插入红冲入库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInRedDetail(");
                strSqlDetail.Append("InNo,ProductID,StorageID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromBillID,FromLineNo,ModifiedDate,ModifiedUserID,CompanyCD,SortNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@ProductID,@StorageID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromBillID,@FromLineNo,getdate(),@ModifiedUserID,@CompanyCD,@SortNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {

                    ////更新减少分仓存量表中的现有存量和可用数量
                    //#region 更新减少分仓存量表中的现有存量和可用数量
                    //SqlCommand commPD = updateStorageProduct(int.Parse(modelList[i].ProductID), int.Parse(modelList[i].StorageID), decimal.Parse(modelList[i].ProductCount), model, false);
                    //lstInsert.Add(commPD);
                    //#endregion

                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInRedDetailInfo(commDetail, modelList[i]);
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

        #region 更新红冲入库及红冲入库明细
        /// <summary>
        /// 更新红冲入库及红冲入库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageInRed(StorageInRedModel model, List<StorageInRedDetailModel> modelList, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageInRed set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("InNo=@InNo,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("EnterDate=@EnterDate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);//数组加入插入基表的command

            //先删掉明细表中对应单据的所有数据
            string delDetail = "delete from officedba.StorageInRedDetail where CompanyCD='" + model.CompanyCD + "' and InNo='" + model.InNo + "'";
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
                strSqlDetail.Append("insert into officedba.StorageInRedDetail(");
                strSqlDetail.Append("InNo,ProductID,StorageID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromLineNo,ModifiedDate,ModifiedUserID,CompanyCD,SortNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@ProductID,@StorageID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@CompanyCD,@SortNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInRedDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）

                    //SqlCommand commPD = updateStorageProduct(modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, false);
                    //lstUpdate.Add(commPD);
                }

            }


            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);


        }

        #endregion

        #region 删除：红冲入库信息
        /// <summary>
        /// 删除红冲入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInRed(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageInRedDetail where InNo=(select InNo from officedba.StorageInRed where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageInRed where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageInRedModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//入库部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//入库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate ", model.EnterDate));//入库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//入库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate ", model.ConfirmDate));//确认日期
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", "getdate()"));//最后更新日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType ", model.ReasonType));//ReasonType
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
        private static void EditInRedDetailInfo(SqlCommand comm, StorageInRedDetailModel model)
        {
            //InNo,ProductID,UnitID,StorageID,UnitPrice,ProductCount,
            //TotalPrice,Remark,FromType,FromBillID,FromLineNo,ModifiedDate,ModifiedUserID,CompanyCD,SortNo
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", "model.ModifiedDate"));//最后更新日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));//

        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageInRedModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInRed SET");
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

            List<StorageInRedDetailModel> modelList = new List<StorageInRedDetailModel>();
            string sqlSele = "select a.CompanyCD,a.ProductID,a.StorageID,a.BatchNo,a.InNo,a.UnitPrice,convert(varchar(10),b.EnterDate,23) HappenDate," +
                " a.ProductCount,a.Remark from officedba.StorageInRedDetail a " +
                " left join officedba.StorageInRed b on b.InNo = a.InNo and a.CompanyCD = b.CompanyCD " +
                " where a.CompanyCD='" + model.CompanyCD + "' and a.InNo=(select InNo from officedba.StorageInRed where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageInRedDetailModel modelDetail = new StorageInRedDetailModel();
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

                    StorageAccountM.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    StorageAccountM.BillType = 6;
                    if (dt.Rows[i]["BatchNo"].ToString() != "")
                    {
                        StorageAccountM.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                        modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    }
                    modelList.Add(modelDetail);

                    StorageAccountM.BillNo = dt.Rows[i]["InNo"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(dt.Rows[i]["HappenDate"].ToString());
                    StorageAccountM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/StorageInRedAdd.aspx";
                    StorageAccountM.ReMark = dt.Rows[i]["Remark"].ToString();

                    SqlCommand commSA = new SqlCommand();
                    commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "1");
                    lstConfirm.Add(commSA);
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commPD = updateStorageProduct(modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model.CompanyCD, modelList[i].BatchNo);
                    lstConfirm.Add(commPD);
                }
            }
            foreach (SqlCommand cmd in GetInFromBillInfo(model.CompanyCD, model.ID))
            {
                lstConfirm.Add(cmd);
            }
            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageInRedModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInRed SET");
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
        public static bool CancelCloseBill(StorageInRedModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInRed SET");
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

        #region 获取入库单列表
        /// <summary>
        /// 获取入库单列表
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetStorageInList(StorageInRedModel model, string InType)
        {
            string sql = string.Empty;
            switch (InType)
            {
                case "1":
                    {
                        sql = "select ID,'采购入库单' as FromType,InNo,Title,CountTotal,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.StorageInPurchase where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
                    }
                    break;
                case "2":
                    {
                        sql = "select ID,'生产完工入库单' as FromType,InNo,Title,CountTotal,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.StorageInProcess where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
                    }
                    break;
                case "3":
                    {
                        sql = "select ID,'其他入库单' as FromType,InNo,Title,CountTotal,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.StorageInOther where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
                    }
                    break;
                default:
                    break;
            }
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.InNo))
            {
                sql += " and InNo like '%'+ @InNo +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo", model.InNo));
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

        #region 更新分仓存量表
        /// <summary>
        /// 更新分仓存量表
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="StorageID"></param>
        /// <param name="ProductNum"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static SqlCommand updateStorageProduct(string ProductID, string StorageID, string ProductNum, string CompanyCD, string BatchNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.StorageProduct set ");
            strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum");
            strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(BatchNo))
                strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
            else
                strSql.AppendLine(" and (BatchNo is null or BatchNo='') ");


            SqlCommand commRePD = new SqlCommand();
            commRePD.CommandText = strSql.ToString();

            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNum", ProductNum));
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));

            return commRePD;
        }
        #endregion

        #region 确认的时候判断,当物品不允许负库存，是否大于可用库存
        /// <summary>
        /// 查找出当前单据中明细，所有不允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCant(StorageInRedModel model)
        {
            string batchsql = "SELECT A.BatchNo FROM officedba.StorageInRedDetail A LEFT OUTER JOIN officedba.StorageInRed B on A.InNo=B.InNo AND A.CompanyCD=B.CompanyCD where A.CompanyCD='" + model.CompanyCD + "' and B.ID=" + model.ID + "";
            DataTable dtbatch = SqlHelper.ExecuteSql(batchsql.ToString());
                        
            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存

            if (dtbatch.Rows.Count > 0)
            {
                for (int i = 0; i < dtbatch.Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select b.ID,a.ProductID,a.StorageID,a.ProductCount                                                                          ");
                    sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
                    sql.AppendLine(",ISNULL(c.ProductCount,0) as UseCount ");
                    sql.AppendLine(" from officedba.StorageInRedDetail a                                                                                      ");
                    sql.AppendLine("left join officedba.StorageInRed b on a.InNo=b.InNo                                                                     ");
                    sql.AppendLine("left join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                               "); //AND a.BatchNo=c.BatchNo 
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND a.BatchNo=c.BatchNo    ");
                    sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID                                                                       ");
                    sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + " and ISNULL(d.MinusIS,0)='0' ");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND c.BatchNo='" + dtbatch.Rows[i]["BatchNo"].ToString().Trim() + "'    ");
                    else
                        sql.AppendLine(" AND (c.BatchNo is null or c.BatchNo='')   ");
                    
                    DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (decimal.Parse(dt.Rows[0]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[0]["UseCount"].ToString()))
                        {
                            if (RowNumList == "" || RowNumList == string.Empty)
                            {
                                RowNumList = (i + 1).ToString();
                                UseCountList = dt.Rows[i]["UseCount"].ToString();
                            }
                            else
                            {
                                RowNumList += "," + (i + 1).ToString();
                                UseCountList += "," + dt.Rows[i]["UseCount"].ToString();
                            }
                        }
                    }                
                }
            }

            if (RowNumList == "" || RowNumList == string.Empty)
            {
                return "";
            }
            else
            {
                return RowNumList + "|" + UseCountList;
            }
            #region 作废SQL
            //StringBuilder sql = new StringBuilder();
            //sql.AppendLine("select b.FromType,b.ID,a.ProductID,a.StorageID ");
            //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit == true)
            //{
            //    sql.AppendLine(",a.UsedUnitCount ProductCount ");
            //}
            //else
            //{
            //    sql.AppendLine(",a.ProductCount ");
            //}
            //sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
            //sql.AppendLine(",ISNULL(c.ProductCount,0)+ISNULL(c.RoadCount,0)+ISNULL(c.InCount,0)-ISNULL(c.OrderCount,0)-ISNULL(c.OutCount,0) as UseCount ");
            //sql.AppendLine(" from officedba.StorageInRedDetail a                                                                                        ");
            //sql.AppendLine("left join officedba.StorageInRed b on a.InNo=b.InNo                                                                         ");
            //sql.AppendLine("left join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID ");
            //sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID                                                                       ");
            //sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + " and ISNULL(d.MinusIS,0)='0'");
            #endregion
        }
        #endregion

        #region 确认的时候判断,当物品允许负库存，是否大于可用库存
        /// <summary>
        /// 查找出当前单据中明细，所有允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCan(StorageInRedModel model)
        {
            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select b.FromType,b.ID,a.ProductID,a.StorageID,a.ProductCount                                                               ");
            sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
            sql.AppendLine(",ISNULL(c.ProductCount,0)+ISNULL(c.RoadCount,0)+ISNULL(c.InCount,0)-ISNULL(c.OrderCount,0)-ISNULL(c.OutCount,0) as UseCount ");
            sql.AppendLine(" from officedba.StorageInRedDetail a                                                                                        ");
            sql.AppendLine("left join officedba.StorageInRed b on a.InNo=b.InNo                                                                         ");
            sql.AppendLine("left join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                                 ");
            sql.AppendLine("left join officedba.ProductInfo d on d.ID=a.ProductID                                                                       ");
            sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + " and ISNULL(d.MinusIS,0)='1'");
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (decimal.Parse(dt.Rows[i]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[i]["UseCount"].ToString()))
                    {
                        if (RowNumList == "" || RowNumList == string.Empty)
                        {
                            RowNumList = (i + 1).ToString();
                            UseCountList = dt.Rows[i]["UseCount"].ToString();
                        }
                        else
                        {
                            RowNumList += "," + (i + 1).ToString();
                            UseCountList += "," + dt.Rows[i]["UseCount"].ToString();
                        }
                    }
                }
            }
            if (RowNumList == "" || RowNumList == string.Empty)
            {
                return "";
            }
            else
            {
                return RowNumList + "|" + UseCountList;
            }

        }
        #endregion
        
        #region 单据打印
        public static DataTable GetStorageInRedInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInRed where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageInRedDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInRedDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion
        
        #region 确认的时候，回写采购到货，生产任务单，销售退货单(其他入库单且有来源的时候)

        /// <summary>
        /// CompanyCD ProductCount FromBillID FromType   FromLineNo      FromBillNo       FromFlag
        /// C1002	   10.0000     	7	        1	        1	        RKDBH2009070018 	1
        /// 这里得到是还是原始入库单，比如采购入库单，FroType(1:采购；2：生产;3:其他)，FromFlag（当FromType为3且无来源的时候为0，其他时候都是1）
        /// FromLineNo 是 原始入库单的SortNo
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ArrayList GetInFromBillInfo(string CompanyCD, string ID)
        {
            ArrayList listcmmd = new ArrayList();
            string TableName = string.Empty;//明细表名字
            string column = string.Empty;//更新的字段（已入库字段名）
            string BillNoName = string.Empty;//编号字段名

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select a.CompanyCD,a.ProductCount,b.FromBillID,b.FromType,a.FromLineNo,                                                                          ");
            Sql.AppendLine("case b.FromType when 1 then c.InNo when 2 then d.InNo when 3 then e.InNo end as FromBillNo,                                                      ");
            Sql.AppendLine("case b.FromType when 1 then c.FromType when 2 then d.FromType when 3 then e.FromType end as FromFlag--1说明是有来源，0无来源（主要针对其他入库） ");
            Sql.AppendLine(" from officedba.StorageInRedDetail a                                                                                                             ");
            Sql.AppendLine("inner join officedba.StorageInRed b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                                                                 ");
            Sql.AppendLine("left join officedba.StorageInPurchase c on b.FromBillID=c.ID and b.CompanyCD=c.CompanyCD                                                         ");
            Sql.AppendLine("left join officedba.StorageInProcess d on b.FromBillID=d.ID and b.CompanyCD=d.CompanyCD                                                          ");
            Sql.AppendLine("left join officedba.StorageInOther e on b.FromBillID=e.ID and b.CompanyCD=e.CompanyCD                                                            ");
            Sql.AppendLine("where b.ID=" + ID + " and  a.CompanyCD='" + CompanyCD + "'                                                                                       ");
            DataTable dt = SqlHelper.ExecuteSql(Sql.ToString());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["FromType"].ToString())
                    {
                        case "1":
                            TableName = "officedba.PurchaseArriveDetail";
                            column = "InCount";
                            BillNoName = "ArriveNo";
                            DataTable dt1 = GetArriveDetailInfo(dr["FromBillNo"].ToString(), dr["FromLineNo"].ToString(), CompanyCD);
                            if (dt1.Rows.Count > 0)
                            {
                                listcmmd.Add(CmdComfirmReturn(dr["ProductCount"].ToString(), TableName, column, BillNoName, dt1.Rows[0]["ArriveNo"].ToString(), dt1.Rows[0]["SortNo"].ToString(), CompanyCD));
                            }

                            break;
                        case "2":
                            TableName = "officedba.ManufactureTaskDetail";
                            column = "InCount";
                            BillNoName = "TaskNo";
                            DataTable dt2 = GetMTDetailInfo(dr["FromBillNo"].ToString(), dr["FromLineNo"].ToString(), CompanyCD);
                            if (dt2.Rows.Count > 0)
                            {
                                listcmmd.Add(CmdComfirmReturn(dr["ProductCount"].ToString(), TableName, column, BillNoName, dt2.Rows[0]["TaskNo"].ToString(), dt2.Rows[0]["SortNo"].ToString(), CompanyCD));
                            }
                            break;
                        case "3":
                            if (dr["FromFlag"].ToString() != "0")//其他入库单的时候，要判断有来源才回写
                            {
                                TableName = "officedba.SellBackDetail";
                                column = "InNumber";
                                BillNoName = "BackNo";
                                DataTable dt3 = GetSBDetailInfo(dr["FromBillNo"].ToString(), dr["FromLineNo"].ToString(), CompanyCD);
                                if (dt3.Rows.Count > 0)
                                {
                                    listcmmd.Add(CmdComfirmReturn(dr["ProductCount"].ToString(), TableName, column, BillNoName, dt3.Rows[0]["BackNo"].ToString(), dt3.Rows[0]["SortNo"].ToString(), CompanyCD));
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
        /// 采购入库的时候，根据入库单获取采购到货明细信息（单条记录）
        /// </summary>
        /// <param name="InNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetArriveDetailInfo(string InNo, string sortNo, string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select c.ArriveNo, d.SortNo ,c.CompanyCD,b.InNo                                                                           ");
            Sql.AppendLine("from officedba.StorageInPurchaseDetail a                                                                                  ");
            Sql.AppendLine("inner join officedba.StorageInPurchase b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                                     ");
            Sql.AppendLine("inner join officedba.PurchaseArrive c on c.ID=b.FromBillID and a.CompanyCD=c.CompanyCD                                    ");
            Sql.AppendLine("inner join officedba.PurchaseArriveDetail d on c.ArriveNo=d.ArriveNo and a.CompanyCD=d.CompanyCD and a.FromLineNo=d.SortNo");
            Sql.AppendLine("where b.InNo='" + InNo + "' and a.CompanyCD='" + CompanyCD + "' and a.SortNo=" + sortNo + "                                           ");
            return SqlHelper.ExecuteSql(Sql.ToString());
        }

        /// <summary>
        /// 生产完工入库的时候，根据入库单获取生产任务明细信息（单条记录）
        /// </summary>
        /// <param name="InNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMTDetailInfo(string InNo, string sortNo, string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select c.TaskNo, d.SortNo ,c.CompanyCD,b.InNo                                                                          ");
            Sql.AppendLine("from officedba.StorageInProcessDetail a                                                                                ");
            Sql.AppendLine("inner join officedba.StorageInProcess b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                                   ");
            Sql.AppendLine("inner join officedba.ManufactureTask c on c.ID=b.FromBillID and a.CompanyCD=c.CompanyCD                                ");
            Sql.AppendLine("inner join officedba.ManufactureTaskDetail d on c.TaskNo=d.TaskNo and a.CompanyCD=d.CompanyCD and a.FromLineNo=d.SortNo");
            Sql.AppendLine("where b.InNo='" + InNo + "' and a.CompanyCD='" + CompanyCD + "' and a.SortNo=" + sortNo + "                            ");
            return SqlHelper.ExecuteSql(Sql.ToString());
        }

        /// <summary>
        /// 其他入库入库的时候，根据入库单获取销售退货明细信息（单条记录）
        /// </summary>
        /// <param name="InNo"></param>
        /// <param name="sortNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSBDetailInfo(string InNo, string sortNo, string CompanyCD)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine("select c.BackNo, d.SortNo ,c.CompanyCD,b.InNo                                                                   ");
            Sql.AppendLine("from officedba.StorageInOtherDetail a                                                                           ");
            Sql.AppendLine("inner join officedba.StorageInOther b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                              ");
            Sql.AppendLine("inner join officedba.SellBack c on c.ID=b.FromBillID and a.CompanyCD=c.CompanyCD                                ");
            Sql.AppendLine("inner join officedba.SellBackDetail d on c.BackNo=d.BackNo and a.CompanyCD=d.CompanyCD and a.FromLineNo=d.SortNo");
            Sql.AppendLine("where b.InNo='" + InNo + "' and a.CompanyCD='" + CompanyCD + "' and a.SortNo=" + sortNo + "                     ");
            return SqlHelper.ExecuteSql(Sql.ToString());
        }

        /// <summary>
        /// 回写数据
        /// </summary>
        /// <param name="ProductCount">回写的数量（也是已入库数量需要减掉的数量）</param>
        /// <param name="TableName">明细表的表明Example：officedba.ArrivePurchaseDetail</param>
        /// <param name="column">回写的那字段名:example:InCount</param>
        /// <param name="BillNoName">单据编号那字段名：example：ArriveNo</param>
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

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageInRedModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageInRed set ";
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
