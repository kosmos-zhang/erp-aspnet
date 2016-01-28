/**********************************************
 * 类作用：   销售出库数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/22
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
using XBase.Model.Office.SellManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageOutSellDBHelper
    {
        #region 查询：销售出库单
        /// <summary>
        /// 查询销售出库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutSellTableBycondition(StorageOutSellModel model, string timeStart, string timeEnd, string SendNo,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

            //列表：出库单编号、出库单主题、销售发货通知单、所属部门、出库人、出库时间、出库数量、出库金额、摘要、单据状态。
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                       ");
            sql.AppendLine(",a.OutNo                                                                          ");
            sql.AppendLine(",a.Title                                                                          ");
            sql.AppendLine(",a.FromBillID                                                                     ");
            sql.AppendLine(",ISNULL(b.SendNo,'') SendNo                                                       ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as SenderName                                          ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName                                                ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                             ");
            sql.AppendLine(",ISNULL(a.CountTotal,0) as CountTotal                                             ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                  ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                      ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                  ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
            sql.AppendLine("FROM officedba.StorageOutSell a  ");
            sql.AppendLine("left join officedba.SellSend b on a.FromBillID=b.ID                         ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=a.Sender                               ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Transactor							  ");
            sql.AppendLine("left join officedba.StorageOutSellDetail f on f.OutNo=a.OutNo and a.CompanyCD=f.CompanyCD							  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD                                                      ");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");


            //出库单编号、出库单主题、销售发货通知单（选择）、出库部门（选择）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、仓库（下拉列表）、单据状态（下拉列表）
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like '%'+ @OutNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", "%" + model.OutNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                sql.AppendLine(" and f.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like  '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(SendNo))
            {
                sql.AppendLine(" and b.SendNo like @SendNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendNo", "%" + SendNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Transactor))
            {
                sql.AppendLine(" and a.Transactor = @Transactor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Transactor", model.Transactor));
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


        public static DataTable GetStorageOutSellTableBycondition(StorageOutSellModel model,string IndexValue,string TxtValue, string timeStart, string timeEnd, string SendNo,string BatchNo, string orderby)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string ListID = StorageDBHelper.GetStorageIDStr(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID, model.CompanyCD);
            //列表：出库单编号、出库单主题、销售发货通知单、所属部门、出库人、出库时间、出库数量、出库金额、摘要、单据状态。
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                       ");
            sql.AppendLine(",a.OutNo                                                                          ");
            sql.AppendLine(",a.Title                                                                          ");
            sql.AppendLine(",a.FromBillID                                                                     ");
            sql.AppendLine(",ISNULL(b.SendNo,'') SendNo                                                       ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as SenderName                                          ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName                                                ");
            sql.AppendLine(",ISNULL(a.TotalPrice,0) as TotalPrice                                             ");
            sql.AppendLine(",ISNULL(a.CountTotal,0) as CountTotal                                             ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                  ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                      ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                  ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
            sql.AppendLine("FROM officedba.StorageOutSell a                                             ");
            sql.AppendLine("left join officedba.SellSend b on a.FromBillID=b.ID                         ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=a.Sender                               ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Transactor							  ");
            sql.AppendLine("left join officedba.StorageOutSellDetail f on f.OutNo=a.OutNo and a.CompanyCD=f.CompanyCD							  ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD ");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");


            //出库单编号、出库单主题、销售发货通知单（选择）、出库部门（选择）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、仓库（下拉列表）、单据状态（下拉列表）
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like '%'+ @OutNo + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", "%" + model.OutNo + "%"));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and f.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like  '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(SendNo))
            {
                sql.AppendLine(" and b.SendNo like @SendNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendNo", "%" + SendNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Transactor))
            {
                sql.AppendLine(" and a.Transactor = @Transactor");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Transactor", model.Transactor));
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

            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }
            if (!string.IsNullOrEmpty(IndexValue) && !string.IsNullOrEmpty(TxtValue))
            {
                sql.AppendLine(" and a.ExtField" + IndexValue + " LIKE @TxtValue");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TxtValue", "%" + TxtValue + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 查看：销售出库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取销售出库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutSellDetailInfo(StorageOutSellModel model)
        {
            //a->officedba.StorageOutSell
            //b->officedba.StorageOutSellDetail
            //k->officedba.SellSend
            //l->officedba.SellSendDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT																																																				  	");
            sql.AppendLine("a.ID,a.CanViewUser,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10   ");
            sql.AppendLine(",a.CompanyCD,c.IsBatchNo,b.UnitID as HiddenUnitID,b.BatchNo,b.UsedUnitID,b.UsedUnitCount,b.UsedPrice,b.ExRate,ii.CodeName as UsedUnitName                                                                                                              ");
            sql.AppendLine(",x.StorageName,a.OutNo                                                                                                                  ");
            sql.AppendLine(",a.FromType                                                                                                               ");
            sql.AppendLine(",case a.FromType when  '1' then '销售发货通知单' else '' end as FromTypeName  ");
            sql.AppendLine(",a.FromBillID                                                                                                             ");
            sql.AppendLine(",k.SendNo as FromBillNo                                                                                                   ");
            sql.AppendLine(",a.Title                                                                                                                  ");
            sql.AppendLine(",ISNULL(y.CustName,'') as CustName                                                                                                    ");
            sql.AppendLine(",k.SendAddr                                                                                                               ");
            sql.AppendLine(",k.ReceiveAddr                                                                                                            ");
            sql.AppendLine(",z.DeptName  as SellDeptName                                                                                              ");
            sql.AppendLine(",aa.EmployeeName as SellerName                                                                                            ");
            sql.AppendLine(",a.Sender                                                                                                                 ");
            sql.AppendLine(",bb.EmployeeName as SenderName ");
            sql.AppendLine(",a.DeptID                                                                                                                 ");
            sql.AppendLine(",j.DeptName                                                                                                               ");
            sql.AppendLine(",a.Transactor                                                                                                             ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as TransactorName                                                                              ");
            sql.AppendLine(",case when a.OutDate Is NULL then '' else CONVERT(VARCHAR(10),a.OutDate, 21) end AS OutDate                               ");
            sql.AppendLine(",a.BillStatus                                                                                                             ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
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
            sql.AppendLine(",c.ProdNo as ProductNo                                                                                                   ");
            sql.AppendLine(",c.ProductName                                                                                                            ");
            sql.AppendLine(",c.Specification                                                                                                          ");
            sql.AppendLine(",ISNULL(c.MinusIs,0) as MinusIs");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                     ");
            sql.AppendLine(",b.UnitPrice as UnitPrice                                                                                                 ");
            sql.AppendLine(",b.StorageID                                                                                                              ");
            sql.AppendLine(",ISNULL(l.ProductCount,0)-ISNULL(l.BackCount,0) as FromBillCount                                                                                          ");
            sql.AppendLine(",ISNULL(l.OutCount,0) as OutCount");
            sql.AppendLine(",ISNULL(l.ProductCount,0)-ISNULL(l.BackCount,0)-ISNULL(l.OutCount,0) as NotOutCount");
            sql.AppendLine(",b.ProductCount                                                                                                           ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                             ");
            sql.AppendLine(",b.FromLineNo                                                                                                             ");
            sql.AppendLine(",b.SortNo,b.Package                                                                                                                 ");
            sql.AppendLine(",ISNULL(b.Remark,'') as DetaiRemark                                                                                       ");
            sql.AppendLine(" ,ISNULL(s.ProductCount,0)+ISNULL(s.RoadCount,0)+ISNULL(s.InCount,0)-ISNULL(s.OrderCount,0)-ISNULL(s.OutCount,0) as UseCount ");
            sql.AppendLine("FROM officedba.StorageOutSell a                                                                                           ");
            sql.AppendLine("left join officedba.StorageOutSellDetail b                                                                                ");
            sql.AppendLine("on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD                                                                 ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Transactor=f.ID                                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID     																												          ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                       ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                           ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                                                                       ");
            sql.AppendLine("left join officedba.SellSend k on k.ID=a.FromBillID                                                                       ");
            sql.AppendLine("left join officedba.CodeUnitType ii on ii.ID=b.UsedUnitid                                                                       ");
            sql.AppendLine("left join officedba.SellSendDetail l on l.SendNo=k.SendNo and l.SortNo=b.FromLineNo and l.CompanyCD=k.CompanyCD                                     ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m ");
            sql.AppendLine("on a.ModifiedUserID=m.UserID                                                                                              ");
            sql.AppendLine("left join officedba.CustInfo y on y.ID=k.CustID                                                                           ");
            sql.AppendLine("left join officedba.DeptInfo z on z.ID=k.SellDeptID                                                                       ");
            sql.AppendLine("left join officedba.EmployeeInfo aa on aa.ID=k.Seller																																			");
            sql.AppendLine("left join officedba.EmployeeInfo bb on bb.ID=a.Sender ");
            sql.AppendLine("left join officedba.StorageInfo x on x.ID=b.StorageID ");
            sql.AppendLine("left join officedba.StorageProduct s on s.CompanyCD=a.CompanyCD and s.StorageID=b.StorageID and b.ProductID=s.ProductID AND b.BatchNo=s.BatchNo ");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID + " order by b.sortno asc");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入销售出库和销售出库明细
        /// <summary>
        /// 插入销售出库和销售出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageOutSell(StorageOutSellModel model, List<StorageOutSellDetailModel> modelList,Hashtable htExtAttr, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageOutSell(");
            strSql.Append("CompanyCD,OutNo,Title,FromType,FromBillID,Sender,DeptID,TotalPrice,CountTotal,Summary,OutDate,Transactor,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@Sender,@DeptID,@TotalPrice,@CountTotal,@Summary,@OutDate,@Transactor,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@CanViewUser,@CanViewUserName)");
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
                //插入销售出库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutSellDetail(");
                strSqlDetail.Append("CompanyCD,OutNo,SortNo,StorageID,ProductID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,Package,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@OutNo,@SortNo,@StorageID,@ProductID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@Package,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutSellDetailInfo(commDetail, modelList[i]);
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
        private static void GetExtAttrCmd(StorageOutSellModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageOutSell set ";
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
        #region 更新销售出库及销售出库明细
        /// <summary>
        /// 更新销售出库及销售出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageOutSell(StorageOutSellModel model,Hashtable htExtAttr, List<StorageOutSellDetailModel> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageOutSell set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("Sender=@Sender,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("OutDate=@OutDate,");
            strSql.Append("Transactor=@Transactor,");
            strSql.Append("Remark=@Remark,");
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
            string delDetail = "delete from officedba.StorageOutSellDetail where CompanyCD='" + model.CompanyCD + "' and OutNo='" + model.OutNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutSellDetail(");
                strSqlDetail.Append("OutNo,ProductID,StorageID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,Package,FromType,FromLineNo,ModifiedDate,ModifiedUserID,CompanyCD,SortNo,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@OutNo,@ProductID,@StorageID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@Package,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@CompanyCD,@SortNo,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");

                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutSellDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                }

            }


            return SqlHelper.ExecuteTransWithArrayList(lstUpdate);


        }

        #endregion

        #region 更新分仓存量数据
        public static SqlCommand updateStorageProduct(string BatchNo,string ProductID, string StorageID, string ProductNum, StorageOutSellModel model, bool flag)
        {
            //true 表示入库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum ");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD ");// 
                if (BatchNo != "") 
                    strSql.AppendLine(" and BatchNo='"+BatchNo.Trim()+"' ");
                else
                    strSql.AppendLine(" and BatchNo is null ");
                
            }
            //否则 表示（入库减少）分仓存量数据（修改的时候）
            else
            {
                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum ");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD ");//and BatchNo=@BatchNo
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
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));
            commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //commRePD.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));

            return commRePD;

        }
        #endregion

        #region 删除：销售出库信息
        /// <summary>
        /// 删除销售出库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageOutSell(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageOutSellDetail where OutNo=(select OutNo from officedba.StorageOutSell where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageOutSell where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageOutSellModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            //@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@Sender,@DeptID,@TotalPrice,
            //@CountTotal,@Summary,@OutDate,@Transactor,@Remark,@Creator,@CreateDate,@BillStatus,getdate(),@ModifiedUserID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", "1"));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//源单ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sender ", model.Sender));//经办人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//出库部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//入库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Transactor ", model.Transactor));//入库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate ", model.OutDate));//入库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUser ", model.CanViewUser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CanViewUserName ", model.CanViewUserName));

            //foreach (SqlParameter para in comm.Parameters)
            //{
            //    if (para.Value == null)
            //    {
            //        para.Value = DBNull.Value;
            //    }
            //}

        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditOutSellDetailInfo(SqlCommand comm, StorageOutSellDetailModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价(基本单价)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量(基本数量)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Package ", model.Package));//包装

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID ", model.UnitID));//基本单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));//实际单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));//实际数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));//实际单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));//比率
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));//批次

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo ", model.FromLineNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//

        }
        #endregion

        #region 确认
        public static bool ConfirmBill(StorageOutSellModel model,out string retstrval)
        {
            //判断源单是无来源还是有来源，无来源则不需要更新受订量
            string sqlFromType = "select a.FromType,a.CustID,b.TotalPrice from officedba.SellSend a"
                        + " inner join officedba.StorageOutSell b on b.FromBillID=a.ID and b.ID=" + model.ID;
            DataTable dtFrom = SqlHelper.ExecuteSql(sqlFromType);
            string FromBillFromType = dtFrom.Rows[0]["FromType"].ToString();//得到的是“0”或“1”


            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutSell SET");
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


            List<StorageOutSellDetailModel> modelList = new List<StorageOutSellDetailModel>();
            string sqlSele = "select a.ProductID,a.UnitPrice,a.CompanyCD,a.OutNo,a.StorageID,a.BatchNo,a.UsedUnitCount,a.FromLineNo,a.ProductCount,b.StorageID as DefaultStorageID from officedba.StorageOutSellDetail a"
            +" inner join officedba.ProductInfo b on b.ID=a.ProductID"
            + " where a.CompanyCD='" + model.CompanyCD + "'"
            + "and a.OutNo=(select OutNo from officedba.StorageOutSell where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageOutSellDetailModel modelDetail = new StorageOutSellDetailModel();
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
                    if (dt.Rows[i]["FromLineNo"].ToString() != "")
                    {
                        modelDetail.FromLineNo = dt.Rows[i]["FromLineNo"].ToString();
                    }
                    if (dt.Rows[i]["DefaultStorageID"].ToString() != "")
                    {
                        modelDetail.DefaultStorageID = dt.Rows[i]["DefaultStorageID"].ToString();
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
                    AccountM_.BillType=7;
                    AccountM_.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    AccountM_.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.HappenDate = System.DateTime.Now;
                    AccountM_.PageUrl="../Office/StorageManager/StorageOutSellAdd.aspx";
                    AccountM_.Price=Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    AccountM_.ProductCount= Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.ProductID= Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    AccountM_.StorageID=Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    SqlCommand AccountCom_=StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"1");
                    lstConfirm.Add(AccountCom_);
                    #endregion
                }
            }
           
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strAddSSDetail = new StringBuilder();//增加销售发货单明细中的已出库数量
                strAddSSDetail.AppendLine("update officedba.SellSendDetail set ");
                strAddSSDetail.AppendLine(" OutCount =ISNULL(OutCount,0)+@ReOutCount where ");
                strAddSSDetail.AppendLine(" SendNo=(select SendNo from officedba.SellSend where ID=(select FromBillID from officedba.StorageOutSell where ID=" + model.ID + "))");
                strAddSSDetail.AppendLine(" and SortNo=@SortNo");


                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commReSSD = new SqlCommand();
                    commReSSD.CommandText = strAddSSDetail.ToString();

                    commReSSD.Parameters.Add(SqlHelper.GetParameterFromString("@ReOutCount", modelList[i].UsedUnitCount));//回写增加的数量(多单位启用时用数量，没启用时用基本数量)
                    commReSSD.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", modelList[i].FromLineNo));
                    lstConfirm.Add(commReSSD);//循环加入数组（把SellSendDetail已经入库数量增加）


                    SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, false);
                    lstConfirm.Add(commPD);

                    if (FromBillFromType == "1")
                    {
                        //更新主放仓库的受订量
                        SqlCommand commOrder = new SqlCommand();
                        commOrder = updateOrderCount(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].DefaultStorageID, modelList[i].ProductCount, model.CompanyCD);
                        lstConfirm.Add(commOrder);
                    }
                }
            }
            bool retval=SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            if (retval)
            {
                DataTable dtCurrtype = XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(model.CompanyCD);
                string IsVoucher=((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher?"1":"0";
                string IsApply=((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply?"1":"0";
                int custid = 0;
                if (dtFrom.Rows[0]["CustID"].ToString().Trim() != "")
                    custid = Convert.ToInt32(dtFrom.Rows[0]["CustID"].ToString());
                bool VocherFlag=XBase.Data.Office.FinanceManager.AutoVoucherDBHelper.AutoVoucherInsert(7, model.CompanyCD, IsVoucher,IsApply,Convert.ToDecimal(dtFrom.Rows[0]["TotalPrice"].ToString()), "officedba.StorageOutSell," + model.ID, dtCurrtype.Rows[0]["ID"].ToString()+","+dtCurrtype.Rows[0]["ExchangeRate"].ToString(),custid, out retstrval);
                if (VocherFlag) retstrval = "确认成功！";
                else retstrval = "确认成功！" + retstrval;
            }
            else retstrval = "";
            return retval;
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageOutSellModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutSell SET");
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
        public static bool CancelCloseBill(StorageOutSellModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutSell SET");
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

        #region 获取销售发货通知单
        /// <summary>
        /// 获取销售发货通知单
        /// </summary>
        /// <param name="companycd"></param>
        /// <returns></returns>
        public static DataTable GetSellSendList(SellSendModel model)
        {
            string sql = "select ID,SendNo,isnull(Title,'')Title,ISNULL(CONVERT(VARCHAR(10),CreateDate,21),'') AS CreateDate from officedba.SellSend where CompanyCD='" + model.CompanyCD + "' and BillStatus=2";
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(model.SendNo))
            {
                sql += " and SendNo like '%'+ @SendNo +'%'";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendNo", model.SendNo));
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

        #region 销售发货明细列表（弹出层显示）
        /// <summary>
        /// 销售发货明细列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSSDetailInfo(string CompanyCD, string SendNo, string Title)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select a.[ID],isnull(q.CodeName,'')CodeName,ISNULL(b.UnitPrice,0)UnitPrice,a.SendNo,isnull(a.Title,'')Title                                                    ");
            sql.AppendLine(",b.ID as DetailID,b.ProductID,ISNULL(b.UsedUnitID,0)UsedUnitID,ISNULL(b.UsedUnitCount,0)UsedUnitCount,ISNULL(b.UsedPrice,0)UsedPrice,ISNULL(b.ExRate,0)ExRate ");
            sql.AppendLine(",case when b.SendDate IS NULL then '' else CONVERT(varchar(10),b.SendDate, 23) end as SendDate ");
            sql.AppendLine(",ISNULL(i.ProductName,'') as ProductName                                                       ");
            sql.AppendLine(",ISNULL(b.ProductCount,0)-ISNULL(b.BackCount,0) as ProductCount --源单数量                     ");
            sql.AppendLine(",ISNULL(b.OutCount,0) as OutCount                                                              ");
            sql.AppendLine("from officedba.SellSendDetail b                                                                ");
            sql.AppendLine("left outer join officedba.SellSend a on b.SendNo=a.SendNo and a.CompanyCD=b.CompanyCD                   ");
            sql.AppendLine("left join officedba.ProductInfo i on b.ProductID=i.ID                                          ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=b.UnitID                                              ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2");
            sql.AppendLine(" and (ISNULL(b.ProductCount,0)-ISNULL(b.BackCount,0)-ISNULL(b.OutCount,0))>0");

            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(SendNo))
            {
                sql.AppendLine(" and a.SendNo like '%'+ @SendNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendNo", SendNo));
            }
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 根据销售发货单明细中ID数组来获取信息（填充出库单中的明细）
        /// <summary>
        /// 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细)
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select b.ID,a.SendNo,a.UsedUnitID,a.UsedUnitCount,a.UsedPrice,a.ExRate,a.UnitID as HiddenUnitID,p.IsBatchNo ");
            sql.AppendLine(",b.CustID                                                                                         ");
            sql.AppendLine(",r.CustName as CustName                                                                            ");
            sql.AppendLine(",b.SendAddr                                                                                       ");
            sql.AppendLine(",b.ReceiveAddr                                                                                    ");
            sql.AppendLine(",b.SellDeptId                                                                                     ");
            sql.AppendLine(",s.DeptName  as SellDeptName                                                                      ");
            sql.AppendLine(",b.Seller                                                                                         ");
            sql.AppendLine(",t.EmployeeName as SellerName                                                                     ");
            sql.AppendLine(",a.ProductID                                                                                      ");
            sql.AppendLine(",ISNULL(p.ProdNo,'') as ProdNo                                                                    ");
            sql.AppendLine(",ISNULL(p.ProductName,'') as ProductName                                                          ");
            sql.AppendLine(",q.CodeName as UnitID                                                                             ");
            sql.AppendLine(",ISNULL(p.Specification,'') as Specification,p.StorageID                             ");
            sql.AppendLine(",(ISNULL(a.ProductCount,0)-ISNULL(a.BackCount,0)) as ProductCount,ISNULL(a.OutCount,0) as OutCount ");
            sql.AppendLine(",ISNULL(a.ProductCount,0)-ISNULL(a.BackCount,0)-ISNULL(a.OutCount,0) as NotOutCount                                     ");
            sql.AppendLine(",a.TaxPrice as UnitPrice,a.TaxPrice*(ISNULL(a.ProductCount,0)-ISNULL(a.OutCount,0)) as TotalPrice ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark                                                                    ");
            sql.AppendLine(",a.SortNo from officedba.SellSendDetail a                                                         ");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID                                            ");
            sql.AppendLine(" left join officedba.SellSend b on b.SendNo=a.SendNo and a.CompanyCD=b.CompanyCD                ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=a.UnitID                                              ");
            sql.AppendLine(" left join officedba.CustInfo r on r.ID=b.CustID                                                  ");
            sql.AppendLine(" left join officedba.DeptInfo s on s.ID=b.SellDeptID                                              ");
            sql.AppendLine(" left join officedba.EmployeeInfo t on t.ID=b.Seller											  ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 是否可以被确认，判断明细中出库数量是否小于源单明细未出库数量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool ISConfirmBill(StorageOutSellModel model)
        {
            bool Result = true;//true表示可以确认
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.FromBillID,b.ProductID,b.ProductCount,ISNULL(b.UsedUnitCount,0)UsedUnitCount                                  ");
            sql.AppendLine(",ISNULL(l.ProductCount,0)-ISNULL(l.BackCount,0)-ISNULL(l.OutCount,0) as NotOutCount                       ");
            sql.AppendLine(" from officedba.StorageOutSellDetail b                                              ");
            sql.AppendLine("left join officedba.StorageOutSell a on a.OutNo=b.OutNo                             ");
            sql.AppendLine("left join officedba.SellSend k on k.ID=a.FromBillID                                 ");
            sql.AppendLine("left join officedba.SellSendDetail l on l.SendNo=k.SendNo and l.SortNo=b.FromlineNo ");
            sql.AppendLine(" where a.CompanyCD='" + model.CompanyCD + "' and a.ID=" + model.ID);
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                    {
                        if (decimal.Parse(dt.Rows[i]["UsedUnitCount"].ToString()) > decimal.Parse(dt.Rows[i]["NotOutCount"].ToString()))
                        {
                            Result = false;
                            break;
                        }
                    }
                    else 
                    {
                        if (decimal.Parse(dt.Rows[i]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[i]["NotOutCount"].ToString()))
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

        #region 确认的时候判断,只判断不允许负库存的
        /// <summary>
        /// 确认的时候判断,只判断不允许负库存的
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCant(StorageOutSellModel model)
        {
            string batchsql = "SELECT A.BatchNo FROM officedba.StorageOutSellDetail A LEFT OUTER JOIN officedba.StorageOutSell B on A.OutNo=B.OutNo AND A.CompanyCD=B.CompanyCD where A.CompanyCD='" + model.CompanyCD + "' and B.ID=" + model.ID + "";
            DataTable dtbatch = SqlHelper.ExecuteSql(batchsql.ToString());

            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存
            int point = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            
            if (dtbatch.Rows.Count > 0)
            {
                for (int i = 0; i < dtbatch.Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select b.ID,a.ExRate,a.ProductID,a.StorageID,a.ProductCount                                                                          ");
                    sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
                    sql.AppendLine(",Convert(numeric(22," + point + "),ISNULL(c.ProductCount,0)) as UseCount ");
                    sql.AppendLine(" from officedba.StorageOutSellDetail a                                                                                      ");
                    sql.AppendLine("left outer join officedba.StorageOutSell b on a.OutNo=b.OutNo                                                                     ");
                    sql.AppendLine("left outer join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                               "); //AND a.BatchNo=c.BatchNo 
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim()!="")
                        sql.AppendLine(" AND a.BatchNo=c.BatchNo    ");
                    sql.AppendLine("left outer join officedba.ProductInfo d on d.ID=a.ProductID                                             ");// and ISNULL(d.MinusIS,0)='0'                          
                    sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + " ");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND c.BatchNo='" + dtbatch.Rows[i]["BatchNo"].ToString().Trim() + "'    ");
                    else
                        sql.AppendLine(" AND (c.BatchNo is null or c.BatchNo='')   ");
                    sql.AppendLine("  order by a.sortno asc");
                    DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count == 1)
                        {
                            if (dt.Rows[0]["MinusIs"].ToString().Trim() == "0")
                            {
                                if (decimal.Parse(dt.Rows[0]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[0]["UseCount"].ToString()))
                                {
                                    if (RowNumList == "" || RowNumList == string.Empty)
                                    {
                                        RowNumList = (i + 1).ToString();
                                        UseCountList = dt.Rows[0]["UseCount"].ToString();
                                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                        {
                                            //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                            UseCountList = (Convert.ToDecimal(UseCountList) / Convert.ToDecimal(dt.Rows[0]["ExRate"].ToString())).ToString();
                                            UseCountList = Math.Round(Convert.ToDecimal(UseCountList), point).ToString();
                                        }
                                    }
                                    else
                                    {
                                        RowNumList += "," + (i + 1).ToString();
                                        string tempcount = dt.Rows[0]["UseCount"].ToString();
                                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                        {
                                            //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                            tempcount = (Convert.ToDecimal(tempcount) / Convert.ToDecimal(dt.Rows[0]["ExRate"].ToString())).ToString();
                                            tempcount = Math.Round(Convert.ToDecimal(tempcount), point).ToString();
                                        }
                                        UseCountList += "," + tempcount;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dt.Rows[i]["MinusIs"].ToString().Trim() == "0")
                            {
                                if (decimal.Parse(dt.Rows[i]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[i]["UseCount"].ToString()))
                                {
                                    if (RowNumList == "" || RowNumList == string.Empty)
                                    {
                                        RowNumList = (i + 1).ToString();
                                        UseCountList = dt.Rows[i]["UseCount"].ToString();
                                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                        {
                                            //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                            UseCountList = (Convert.ToDecimal(UseCountList) / Convert.ToDecimal(dt.Rows[i]["ExRate"].ToString())).ToString();
                                            UseCountList = Math.Round(Convert.ToDecimal(UseCountList), point).ToString();
                                        }
                                    }
                                    else
                                    {
                                        RowNumList += "," + (i + 1).ToString();
                                        string tempcount = dt.Rows[i]["UseCount"].ToString();
                                        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                        {
                                            //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                            tempcount = (Convert.ToDecimal(tempcount) / Convert.ToDecimal(dt.Rows[i]["ExRate"].ToString())).ToString();
                                            tempcount = Math.Round(Convert.ToDecimal(tempcount), point).ToString();
                                        }
                                        UseCountList += "," + tempcount;
                                    }
                                }
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

        }
        #endregion



        #region 确认的时候判断,当物品允许负库存，是否大于可用库存（090807判断现有库存）（不需要了）
        /// <summary>
        /// 查找出当前单据中明细，所有允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCan(StorageOutSellModel model)
        {
            string batchsql = "SELECT A.BatchNo FROM officedba.StorageOutSellDetail A LEFT OUTER JOIN officedba.StorageOutSell B on A.OutNo=B.OutNo AND A.CompanyCD=B.CompanyCD where A.CompanyCD='" + model.CompanyCD + "' and B.ID=" + model.ID + "";
            DataTable dtbatch = SqlHelper.ExecuteSql(batchsql.ToString());

            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存
            int point = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            
            if (dtbatch.Rows.Count > 0)
            {
                for (int i = 0; i < dtbatch.Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select b.ID,a.ProductID,a.ExRate,a.StorageID,a.ProductCount                                                                          ");
                    sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
                    sql.AppendLine(",Convert(numeric(22," + point + "),ISNULL(c.ProductCount,0)) as UseCount ");
                    sql.AppendLine(" from officedba.StorageOutSellDetail a                                                                                      ");
                    sql.AppendLine("left outer join officedba.StorageOutSell b on a.OutNo=b.OutNo                                                                     ");
                    sql.AppendLine("left outer join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                                 ");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND a.BatchNo=c.BatchNo    ");
                    sql.AppendLine("left outer join officedba.ProductInfo d on d.ID=a.ProductID  and ISNULL(d.MinusIS,0)='1'                                                                   ");
                    sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + "");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND c.BatchNo='" + dtbatch.Rows[i]["BatchNo"].ToString().Trim() + "'    ");
                    else
                        sql.AppendLine(" AND (c.BatchNo is null or c.BatchNo='')   ");
                    sql.AppendLine("  order by a.sortno asc");
                    DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count == 1)
                        {
                            if (decimal.Parse(dt.Rows[0]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[0]["UseCount"].ToString()))
                            {
                                if (RowNumList == "" || RowNumList == string.Empty)
                                {
                                    RowNumList = (i + 1).ToString();
                                    UseCountList = dt.Rows[0]["UseCount"].ToString();
                                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                    {
                                        //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                        UseCountList = (Convert.ToDecimal(UseCountList) / Convert.ToDecimal(dt.Rows[0]["ExRate"].ToString())).ToString();
                                        UseCountList = Math.Round(Convert.ToDecimal(UseCountList), point).ToString();

                                    }
                                }
                                else
                                {
                                    RowNumList += "," + (i + 1).ToString();
                                    string tempcount = dt.Rows[0]["UseCount"].ToString();
                                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                    {
                                        //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                        tempcount = (Convert.ToDecimal(tempcount) / Convert.ToDecimal(dt.Rows[0]["ExRate"].ToString())).ToString();
                                        tempcount = Math.Round(Convert.ToDecimal(tempcount), point).ToString();
                                    }
                                    UseCountList += "," + tempcount;
                                }
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dt.Rows[i]["ProductCount"].ToString()) > decimal.Parse(dt.Rows[i]["UseCount"].ToString()))
                            {
                                if (RowNumList == "" || RowNumList == string.Empty)
                                {
                                    RowNumList = (i + 1).ToString();
                                    UseCountList = dt.Rows[i]["UseCount"].ToString();
                                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                    {
                                        //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                        UseCountList = (Convert.ToDecimal(UseCountList) / Convert.ToDecimal(dt.Rows[i]["ExRate"].ToString())).ToString();
                                        UseCountList = Math.Round(Convert.ToDecimal(UseCountList), point).ToString();
                                    }
                                }
                                else
                                {
                                    RowNumList += "," + (i + 1).ToString();
                                    string tempcount = dt.Rows[i]["UseCount"].ToString();
                                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                                    {
                                        //if (!string.IsNullOrEmpty(dt.Rows[0]["ExRate"].ToString()))
                                        tempcount = (Convert.ToDecimal(tempcount) / Convert.ToDecimal(dt.Rows[i]["ExRate"].ToString())).ToString();
                                        tempcount = Math.Round(Convert.ToDecimal(tempcount), point).ToString();
                                    }
                                    UseCountList += "," + tempcount;
                                }
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

        }
        #endregion


        #region 单据打印
        public static DataTable GetStorageOutSellInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutSell where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageOutSellDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutSellDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion

        #region 更新受订量，注意这个仓库部不是你自己选的仓库，而是物品表中的主放仓库

        private static SqlCommand updateOrderCount(string BatchNo,string ProductID, string DefaultStorageID, string ProductNum, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.AppendLine("update officedba.StorageProduct set ");
            strSql.AppendLine("OrderCount=OrderCount-@ProductNum");
            strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
            if (!string.IsNullOrEmpty(BatchNo))
                strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
            else
                strSql.AppendLine(" and (BatchNo is null or BatchNo='') ");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            comm.Parameters.AddWithValue("@ProductNum", decimal.Parse(ProductNum));
            comm.Parameters.AddWithValue("@StorageID", DefaultStorageID);
            comm.Parameters.AddWithValue("@ProductID", ProductID);
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            return comm;
        }

        #endregion

        #region 根据仓库，商品ID获取批次列表
        /// <summary>
        /// 根据仓库，商品ID获取批次列表
        /// </summary>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="ProductID">物品ID</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetBatchNoList(string StorageID, string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,BatchNo,ProductCount from officedba.StorageProduct ");
            strSql.AppendLine(" where ProductID=@ProductID and StorageID=@StorageID ");
            strSql.AppendLine(" and CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@StorageID",StorageID ),
                                    new SqlParameter("@ProductID",ProductID ),
                                    new SqlParameter("@CompanyCD",CompanyCD ),
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

    }
}
