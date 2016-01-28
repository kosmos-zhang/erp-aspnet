/**********************************************
 * 类作用：   仓库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/17
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
using XBase.Model.Office.PurchaseManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageInPurchaseDHHelper
    {
        #region 查询：采购入库单
        /// <summary>
        /// 查询仓库
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInPurchaseTableBycondition(string BatchNo, StorageInPurchaseModel model, string timeStart, string timeEnd, string FromBillNo, string StorageID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            //入库单编号、入库单主题、采购到货单、交货人、验收人、
            //入库部门、人库人、入库时间、入库数量、入库金额、摘要、单据状态
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID");
            sql.AppendLine(",ISNULL(a.CompanyCD,'') AS CompanyCD");
            sql.AppendLine(" ,ISNULL(a.InNo,'') AS InNo");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title");
            sql.AppendLine(",ISNULL(f.ArriveNo,'') AS ArriveNo");
            sql.AppendLine(" ,CASE WHEN a.TotalPrice IS NULL THEN '' ELSE a.TotalPrice END AS TotalPrice");
            sql.AppendLine(",CASE WHEN a.CountTotal IS NULL THEN '0' ELSE a.CountTotal END AS CountTotal");
            sql.AppendLine(",ISNULL(b.DeptName,'') AS DeptName");
            sql.AppendLine(",ISNULL(c.EmployeeName,'') as Taker");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Checker");
            sql.AppendLine(" ,ISNULL(e.EmployeeName,'') AS Executor");
            sql.AppendLine("  ,case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end as BillStatus");
            sql.AppendLine(" ,ISNULL(a.Summary,'') as Summary");
            sql.AppendLine(" FROM officedba.StorageInPurchase a");
            sql.AppendLine("	left join officedba.DeptInfo b");
            sql.AppendLine("		on a.DeptID=b.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo c");
            sql.AppendLine("		on a.Taker=c.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo d");
            sql.AppendLine("		on a.Checker=d.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo e");
            sql.AppendLine("		on a.Executor=e.ID");
            sql.AppendLine("	left join officedba.PurchaseArrive f");
            sql.AppendLine("		on a.FromBillID=f.ID ");
            sql.AppendLine(" left join officedba.StorageInPurchaseDetail g on g.InNo = a.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and g.BatchNo like '%'+ @BatchNo +'%'");
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
                sql.AppendLine(" and f.ArriveNo= @FromBillNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNo));
            }
            if (!string.IsNullOrEmpty(model.Taker))
            {
                sql.AppendLine(" and a.Taker=@Taker");
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
            if (!string.IsNullOrEmpty(timeStart.ToString()))
            {
                sql.AppendLine(" and a.EnterDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }
            if (!string.IsNullOrEmpty(timeEnd.ToString()))
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
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInPurchaseDetail where StorageID=@StorageID)");
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


        public static DataTable GetStorageInPurchaseTableBycondition(string BatchNo, StorageInPurchaseModel model, string timeStart, string timeEnd, string FromBillNo, string StorageID, string orderby)
        {
            //入库单编号、入库单主题、采购到货单、交货人、验收人、
            //入库部门、人库人、入库时间、入库数量、入库金额、摘要、单据状态
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID");
            sql.AppendLine(",ISNULL(a.CompanyCD,'') AS CompanyCD");
            sql.AppendLine(" ,ISNULL(a.InNo,'') AS InNo");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title");
            sql.AppendLine(",ISNULL(f.ArriveNo,'') AS ArriveNo");
            sql.AppendLine(" ,CASE WHEN a.TotalPrice IS NULL THEN '' ELSE a.TotalPrice END AS TotalPrice");
            sql.AppendLine(",CASE WHEN a.CountTotal IS NULL THEN '0' ELSE a.CountTotal END AS CountTotal");
            sql.AppendLine(",ISNULL(b.DeptName,'') AS DeptName");
            sql.AppendLine(",ISNULL(c.EmployeeName,'') as Taker");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as Checker");
            sql.AppendLine(" ,ISNULL(e.EmployeeName,'') AS Executor");
            sql.AppendLine("  ,case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end as BillStatus");
            sql.AppendLine(" ,ISNULL(a.Summary,'') as Summary");
            sql.AppendLine(" FROM officedba.StorageInPurchase a");
            sql.AppendLine("	left join officedba.DeptInfo b");
            sql.AppendLine("		on a.DeptID=b.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo c");
            sql.AppendLine("		on a.Taker=c.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo d");
            sql.AppendLine("		on a.Checker=d.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo e");
            sql.AppendLine("		on a.Executor=e.ID");
            sql.AppendLine("	left join officedba.PurchaseArrive f");
            sql.AppendLine("		on a.FromBillID=f.ID");
            sql.AppendLine(" left join officedba.StorageInPurchaseDetail g on g.InNo = a.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD AND (CHARINDEX('," + CanUser + ",',','+a.CanViewUser+',')>0 OR a.CanViewUser='' or a.CanViewUser is null OR  a.Creator=" + CanUser + ")");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and g.BatchNo like '%'+ @BatchNo +'%'");
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
                sql.AppendLine(" and f.ArriveNo= @FromBillNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNo));
            }
            if (!string.IsNullOrEmpty(model.Taker))
            {
                sql.AppendLine(" and a.Taker=@Taker");
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
            if (!string.IsNullOrEmpty(timeStart.ToString()))
            {
                sql.AppendLine(" and a.EnterDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }
            if (!string.IsNullOrEmpty(timeEnd.ToString()))
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
                sql.AppendLine(" and a.InNo in(select InNo from officedba.StorageInPurchaseDetail where StorageID=@StorageID)");
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

        #region 查看：采购入库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取采购入库详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInPurchaseDetailInfo(StorageInPurchaseModel model)
        {

            //b-->StorageInPurchase,a-->StorageInPurchaseDetail;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select");
            sql.AppendLine(" b.CanViewUser,b.CanViewUserName,b.ExtField1,b.ExtField2,b.ExtField3,b.ExtField4,b.ExtField5,b.ExtField6,b.ExtField6,b.ExtField7,b.ExtField8,b.ExtField9,b.ExtField10   ");
            sql.AppendLine(",a.ID as DetailID");
            sql.AppendLine(",b.InNo");
            sql.AppendLine(",a.UsedUnitID as UsedUnitID ");
            sql.AppendLine(",a.UsedUnitCount  as UsedUnitCount ");
            sql.AppendLine(", ISNULL(a.UsedPrice,0) as UsedPrice ");
            sql.AppendLine(",a.ExRate as ExRate ");
            sql.AppendLine(",a.SortNo,a.BatchNo");
            sql.AppendLine(",a.ProductID");
            sql.AppendLine(",t.ProdNo as ProductNo,isnull(z.TypeName,'') ColorName ");
            sql.AppendLine(",a.UnitPrice");
            sql.AppendLine(",k.ProductName");
            sql.AppendLine(",k.IsBatchNo");
            sql.AppendLine(",k.Specification");
            sql.AppendLine(",aa.CodeName as UnitID");
            sql.AppendLine(",a.ProductCount");
            sql.AppendLine(",a.StorageID");
            sql.AppendLine(",a.TotalPrice");
            sql.AppendLine(",a.Remark as Remark1");
            sql.AppendLine(",b.FromBillID");
            sql.AppendLine(",c.ArriveNo");
            sql.AppendLine(",a.FromLineNo");
            sql.AppendLine(",b.ID");
            sql.AppendLine(",b.Title");
            sql.AppendLine(",b.FromType");
            sql.AppendLine(",case b.FromType when '0' then '无来源' else '采购到货单' end as FromTypeName");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice");
            sql.AppendLine(",b.CountTotal");//入库数量
            sql.AppendLine(",b.DeptID as InPutDeptID");
            sql.AppendLine(",w.DeptName as InPutDeptName");//入库部门
            sql.AppendLine(",b.Taker");
            sql.AppendLine(",l.EmployeeName as TakerName");
            sql.AppendLine(",b.Checker");
            sql.AppendLine(",m.EmployeeName as CheckerName");
            sql.AppendLine(",b.Executor");
            sql.AppendLine(",n.EmployeeName as ExecutorName");
            sql.AppendLine(",case when b.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),b.EnterDate, 21) end AS EnterDate");
            sql.AppendLine(",b.Summary");
            sql.AppendLine(",b.Remark");
            sql.AppendLine(",b.Creator");
            sql.AppendLine(",r.EmployeeName as CreatorName");
            sql.AppendLine(",case when b.CreateDate Is NULL then '' else CONVERT(VARCHAR(10),b.CreateDate, 21) end AS CreateDate");
            sql.AppendLine(",b.BillStatus");
            sql.AppendLine(",b.Confirmor");
            sql.AppendLine(",o.EmployeeName as ConfirmorName");
            sql.AppendLine(",case when b.ConfirmDate Is NULL then '' else CONVERT(VARCHAR(10),b.ConfirmDate, 21) end AS ConfirmDate");
            sql.AppendLine(",b.Closer");
            sql.AppendLine(",p.EmployeeName as CloserName");
            sql.AppendLine(",case when b.CloseDate Is NULL then '' else CONVERT(VARCHAR(10),b.CloseDate, 21) end AS CloseDate");
            sql.AppendLine(",case when b.ModifiedDate Is NULL then '' else CONVERT(VARCHAR(10),b.ModifiedDate, 21) end AS ModifiedDate");
            sql.AppendLine(",b.ModifiedUserID");
            sql.AppendLine(",b.ModifiedUserID as ModifiedUserName");
            sql.AppendLine(",c.ArriveNo");
            sql.AppendLine(",c.FromType AS CFromType");
            sql.AppendLine(",c.ProviderID");
            sql.AppendLine(",ISNULL(j.CustName,'') as ProviderName");
            sql.AppendLine(",c.Purchaser");
            sql.AppendLine(",f.EmployeeName as PurchaserName");
            sql.AppendLine(",c.TypeID");
            sql.AppendLine(",c.TakeType");
            sql.AppendLine(",c.CarryType");
            sql.AppendLine(",v.DeptName");//采购部门
            sql.AppendLine(",c.SendAddress");
            sql.AppendLine(",c.ReceiveOverAddress");
            sql.AppendLine(",c.CurrencyType");
            sql.AppendLine(",c.Rate AS CRate");
            sql.AppendLine(",ISNULL(bb.ProductCount,0)-ISNULL(bb.RejectCount,0)-ISNULL(bb.BackCount,0) as FromBillCount");
            sql.AppendLine(",(ISNULL(bb.ProductCount,0)-ISNULL(bb.RejectCount,0)-ISNULL(bb.BackCount,0)-ISNULL(bb.InCount,0)) as NotInCount");
            sql.AppendLine(",ISNULL(bb.InCount,0) as InCount");
            sql.AppendLine(",ISNULL(bb.TaxRate,0) as TaxRate,isnull(a.BackCount,0) as BackCount ");
            sql.AppendLine("from officedba.StorageInPurchaseDetail a");
            sql.AppendLine("	right outer join ");
            sql.AppendLine("officedba.StorageInPurchase b");
            sql.AppendLine("	on b.InNo = a.InNo and a.CompanyCD=b.CompanyCD and b.CompanyCD='" + model.CompanyCD + "'");
            sql.AppendLine("	left join ");
            sql.AppendLine("officedba.PurchaseArrive c ");
            sql.AppendLine("	on c.ID=b.FromBillID");
            sql.AppendLine("	left join officedba.EmployeeInfo f");
            sql.AppendLine("		on c.Purchaser = f.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo g");
            sql.AppendLine("		on c.ProviderID = g.ID");
            sql.AppendLine("	left join officedba.ProductInfo k");
            sql.AppendLine("		on a.ProductID=k.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo l");
            sql.AppendLine("		on b.Taker = l.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo m");
            sql.AppendLine("		on b.Checker = m.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo n");
            sql.AppendLine("		on b.Executor = n.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo o");
            sql.AppendLine("		on b.Confirmor = o.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo p");
            sql.AppendLine("		on b.Closer = p.ID");
            sql.AppendLine("	left join officedba.EmployeeInfo r");
            sql.AppendLine("		on b.Creator = r.ID");
            sql.AppendLine("	left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) s");
            sql.AppendLine("		on b.ModifiedUserID = s.UserID");
            sql.AppendLine("	left join officedba.ProductInfo t");
            sql.AppendLine("		on a.ProductID=t.ID");
            sql.AppendLine("	left join officedba.ProductInfo u");
            sql.AppendLine("		on a.ProductID=u.ID");
            sql.AppendLine("	left join officedba.DeptInfo v");
            sql.AppendLine("		on c.DeptID=v.ID");
            sql.AppendLine("	left join officedba.DeptInfo w");
            sql.AppendLine("		on b.DeptID=w.ID");
            sql.AppendLine(" left join officedba.CodeUnitType aa on aa.ID=u.UnitID");
            sql.AppendLine(" left join officedba.ProviderInfo j on j.ID=c.ProviderID");
            sql.AppendLine(" left join officedba.CodePublicType z on z.ID=t.ColorID");
            sql.AppendLine(" left join officedba.PurchaseArriveDetail bb on bb.ArriveNo=c.ArriveNo and bb.CompanyCD=c.CompanyCD and bb.SortNo=a.FromlineNo");
            sql.AppendLine("   where b.CompanyCD='" + model.CompanyCD + "' and b.id=" + model.ID);

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 添加：采购入库单信息及其详细信息
        /// <summary>
        /// 添加采购入库单信息及其详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorageInPurchase(StorageInPurchaseModel model, List<StorageInPurchaseDetailModel> modelList, Hashtable htExtAttr, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageInPurchase(");
            strSql.Append("CompanyCD,InNo,Title,FromType,FromBillID,TotalPrice,Taker,Checker,Executor,EnterDate,Summary,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,DeptID,CountTotal,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@InNo,@Title,1,@FromBillID,@TotalPrice,@Taker,@Checker,@Executor,@EnterDate,@Summary,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@DeptID,@CountTotal,@CanViewUser,@CanViewUserName)");
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
                //插入采购入库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInPurchaseDetail(");
                strSqlDetail.Append("InNo,SortNo,FromLineNo,FromType,ProductID,ProductCount,UnitPrice,TotalPrice,StorageID,Remark,ModifiedDate,ModifiedUserID,CompanyCD,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@SortNo,@FromLineNo,1,@ProductID,@ProductCount,@UnitPrice,@TotalPrice,@StorageID,@Remark,getdate(),@ModifiedUserID,@CompanyCD,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {

                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInPurchaseDetailInfo(commDetail, modelList[i]);
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
        private static void GetExtAttrCmd(StorageInPurchaseModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageInPurchase set ";
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

        #region 修改：采购入库单（采购入库单信息和详细信息）

        public static bool UpdateStorageInPurchase(StorageInPurchaseModel model, List<StorageInPurchaseDetailModel> modelList, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageInPurchase set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=1,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Taker=@Taker,");
            strSql.Append("Checker=@Checker,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("EnterDate=@EnterDate,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("CountTotal=@CountTotal");
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
            string delDetail = "delete from officedba.StorageInPurchaseDetail where CompanyCD='" + model.CompanyCD + "' and InNo='" + model.InNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);



            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInPurchaseDetail(");
                strSqlDetail.Append("InNo,SortNo,FromLineNo,ProductID,ProductCount,UnitPrice,TotalPrice,StorageID,Remark,ModifiedDate,ModifiedUserID,CompanyCD,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@SortNo,@FromLineNo,@ProductID,@ProductCount,@UnitPrice,@TotalPrice,@StorageID,@Remark,@ModifiedDate,@ModifiedUserID,@CompanyCD,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditInPurchaseDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }

            }

            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);


        }
        #endregion

        #region 删除：采购入库信息
        /// <summary>
        /// 删除采购入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInPurchase(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageInPurchaseDetail where InNo=(select InNo from officedba.StorageInPurchase where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageInPurchase where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageInPurchaseModel model, out string Msg)
        {
            //判断源单是无来源还是有来源，无来源则不需要更新在途量
            string sqlFromType = "select a.FromType,a.ProviderID from officedba.PurchaseArrive a"
                        + " inner join officedba.StorageInPurchase b on b.FromBillID=a.ID and b.ID=" + model.ID;
            DataTable dtF = SqlHelper.ExecuteSql(sqlFromType);
            //string FromBillFromType = SqlHelper.ExecuteScalar(sqlFromType, null).ToString();//得到的是“0”或“1”
            string FromBillFromType = dtF.Rows[0]["FromType"].ToString();

            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInPurchase SET");
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

            List<StorageInPurchaseDetailModel> modelList = new List<StorageInPurchaseDetailModel>();

            string sqlSele = "select a.CompanyCD,a.ProductID,a.StorageID,a.BatchNo,a.InNo BillNo,a.UnitPrice,c.EnterDate HappenDate,"
                            + "a.ProductCount,a.Remark,a.FromLineNo,b.StorageID as DefaultStorageID,a.UsedUnitCount "
                            + " from officedba.StorageInPurchaseDetail a"
                            + " left join officedba.ProductInfo b on b.ID=a.ProductID"
                            + " left join officedba.StorageInPurchase c on c.InNo = a.InNo and a.CompanyCD = c.CompanyCD "
                            + " where a.CompanyCD='" + model.CompanyCD + "' and a.InNo=(select InNo from officedba.StorageInPurchase where ID=" + model.ID + ")";

            //string sqlSele = "select ProductID,StorageID,ProductCount,FromLineNo from officedba.StorageInPurchaseDetail where CompanyCD='" + model.CompanyCD + "' and InNo=(select InNo from officedba.StorageInPurchase where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageInPurchaseDetailModel modelDetail = new StorageInPurchaseDetailModel();
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
                    if (dt.Rows[i]["DefaultStorageID"].ToString() != "")
                    {
                        modelDetail.DefaultStorageID = dt.Rows[i]["DefaultStorageID"].ToString();
                    }

                    StorageAccountM.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    StorageAccountM.BillType = 3;
                    if (dt.Rows[i]["BatchNo"].ToString() != "")
                    {
                        modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                        StorageAccountM.BatchNo = dt.Rows[i]["BatchNo"].ToString();
                    }
                    modelList.Add(modelDetail);

                    StorageAccountM.BillNo = dt.Rows[i]["BillNo"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(dt.Rows[i]["HappenDate"].ToString());
                    StorageAccountM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/StorageInPurchaseAdd.aspx";
                    StorageAccountM.ReMark = dt.Rows[i]["Remark"].ToString();

                    SqlCommand commSA = new SqlCommand();
                    commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                    lstConfirm.Add(commSA);
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strAddSBDetail = new StringBuilder();
                strAddSBDetail.AppendLine("update officedba.PurchaseArriveDetail set ");
                strAddSBDetail.AppendLine(" InCount =ISNULL(InCount,0)+@ReBackNum where ");
                strAddSBDetail.AppendLine(" ArriveNo=(select ArriveNo from officedba.PurchaseArrive where ID=(select FromBillID from officedba.StorageInPurchase where ID=" + model.ID + "))");
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
                    lstConfirm.Add(commReSB);//循环加入数组（"已入库数量"增加）

                    SqlCommand commPD = new SqlCommand();
                    if (Exists(modelList[i].BatchNo, modelList[i].StorageID, modelList[i].ProductID, model.CompanyCD))
                    {
                        commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, true);
                    }
                    else
                    {
                        commPD = InsertStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model.CompanyCD);
                    }
                    lstConfirm.Add(commPD);
                    if (FromBillFromType == "1")
                    {
                        SqlCommand commRoad = new SqlCommand();
                        commRoad = updateRoadCount(modelList[i].ProductID, modelList[i].DefaultStorageID, modelList[i].ProductCount, model);
                        lstConfirm.Add(commRoad);
                    }
                }
            }

            bool IsOK = true;
            IsOK = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            string retstrval = "";
            if (IsOK)
            {
                string IsVoucher = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher ? "1" : "0";
                string IsApply = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply ? "1" : "0";
                decimal TotalPri = Convert.ToDecimal(model.TotalPrice);//价格合计
                DataTable dtCurrtype = XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                string CurrencyInfo = dtCurrtype.Rows[0]["ID"].ToString();
                string ExchangeRate = dtCurrtype.Rows[0]["ExchangeRate"].ToString();
                int ProviderID = 0;
                if (dtF.Rows[0]["ProviderID"].ToString() != "")
                {
                    ProviderID = Convert.ToInt32(dtF.Rows[0]["ProviderID"].ToString());
                }

                bool IsTure = XBase.Data.Office.FinanceManager.AutoVoucherDBHelper.AutoVoucherInsert(5, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, IsVoucher, IsApply, TotalPri, "officedba.StorageInPurchase," + model.ID, CurrencyInfo + "," + ExchangeRate, ProviderID, out retstrval);
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
        public static bool CloseBill(StorageInPurchaseModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInPurchase SET");
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
        public static bool CancelCloseBill(StorageInPurchaseModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInPurchase SET");
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
        private static void SetSaveParameter(SqlCommand comm, StorageInPurchaseModel model)
        {
            if (model.ID != null && model.ID != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            }
            //CompanyCD,@InNo,@Title,@FromType,@FromBillID,@TotalPrice,@Taker,@Checker,@Executor,@EnterDate,@Summary,@Remark,
            //@Creator,getdate(),@BillStatus,@ModifiedDate,@ModifiedUserID,@DeptID,@CountTotal
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//部门（对应部门表ID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Taker ", model.Taker));//交货人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Checker ", model.Checker));//验货人ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Executor ", model.Executor));//入库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EnterDate ", model.EnterDate));//入库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//入库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
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
        private static void EditInPurchaseDetailInfo(SqlCommand comm, StorageInPurchaseDetailModel model)
        {
            //@InNo,@SortNo,@FromLineNo,@ProductID,@ProductCount,@UnitPrice,@StorageID,@Remark,@ModifiedDate,@ModifiedUserID,@CompanyCD
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", model.ModifiedDate));//最后更新日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));

        }
        #endregion

        #region 更新分仓存量表
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageInPurchaseModel model, bool flag)
        {
            //true 表示入库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum ");
                //strSql.AppendLine("RoadCount=RoadCount-@ProductNum ");
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

        #region 更新在途量，注意这个仓库部是你自己选的仓库，而是物品表中的主放仓库

        private static SqlCommand updateRoadCount(string ProductID, string DefaultStorageID, string ProductNum, StorageInPurchaseModel model)
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
        //public static SqlCommand InsertStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, string CompanyCD)
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

        #region 获取采购到货单
        /// <summary>
        /// 获取采购到货单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseList(PurchaseArriveModel model)
        {
            string sql = "select ID,ArriveNo,Title,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.PurchaseArrive where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.ArriveNo))
            {
                sql += " and ArriveNo like '%'+ @ArriveNo +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", model.ArriveNo));
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

        #region 是否可以被确认，判断明细中入库数量是否小于源单明细未入库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageInPurchaseModel model)
        {
            bool Result = true;//true表示可以确认
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.FromBillID,b.ProductID                                          ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit == true)
            {
                sql.AppendLine(",b.UsedUnitCount ProductCount ");
            }
            else
            {
                sql.AppendLine(",b.ProductCount ");
            }
            sql.AppendLine(",ISNULL(l.ProductCount,0)-ISNULL(l.InCount,0) as NotInCount                                  ");
            sql.AppendLine("from officedba.StorageInPurchaseDetail b                                                     ");
            sql.AppendLine("left join officedba.StorageInPurchase a on a.InNo=b.InNo                                     ");
            sql.AppendLine("left join officedba.PurchaseArrive k on k.ID=a.FromBillID                                    ");
            sql.AppendLine("left join officedba.PurchaseArriveDetail l on l.ArriveNo=k.ArriveNo and l.SortNo=b.FromlineNo and K.CompanyCD=l.CompanyCD");
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
        public static DataTable GetStorageInPurchaseInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInPurchase where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageInPurchaseDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInPurchaseDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 超账期应付款查询

        /// <summary>
        /// 超账期应付款查询
        /// </summary>
        /// <param name="ProviderID">供应商</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetOutDateAccount(string CompanyCD, string ProviderID
            , int pageIndex, int pageCount, string orderBy, ref int TotalCount)
        {
            string sqlStr = @"SELECT pa.ProviderID,pi1.CustName AS ProviderName,sip.InNo
		                                ,CONVERT(VARCHAR(10),pa.ConfirmDate,120) AS PurchaseDate
		                                ,CONVERT(VARCHAR(10),sip.ConfirmDate,120) AS ArriveDate
                                        ,ISNULL(DATEDIFF(dd,sip.ConfirmDate,GETDATE()),0)-ISNULL(pi1.AllowDefaultDays,0) AS OutDays
                                FROM officedba.StorageInPurchase sip
                                LEFT OUTER JOIN officedba.PurchaseArrive pa ON sip.CompanyCD=pa.CompanyCD AND sip.FromBillID=pa.ID
                                LEFT OUTER JOIN officedba.ProviderInfo pi1 ON pa.ProviderID=pi1.ID ";
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(sqlStr);
            sql.Append(" WHERE sip.CompanyCD=@CompanyCD AND sip.ConfirmDate IS NOT NULL");
            comm.Parameters.Add(new SqlParameter("@CompanyCD", CompanyCD));
            int id = 0;
            if (int.TryParse(ProviderID, out id))
            {
                sql.Append(" AND pi1.ID=@ProviderID ");
                comm.Parameters.Add(new SqlParameter("@ProviderID", id));
            }
            comm.CommandText = sql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion
    }
}
