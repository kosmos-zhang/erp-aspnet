/**********************************************
 * 类作用：   仓库数据库层处理
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

namespace XBase.Data.Office.StorageManager
{
    public class StorageOutOtherDBHelper
    {
        #region 查询：其他出库单
        /// <summary>
        /// 查询其他出库单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutOtherTableBycondition(StorageOutOtherModel model, string timeStart, string timeEnd,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID

            //出库单编号、出库单主题、源单类型、出库人、出库时间、出库原因、摘要、出库数量、出库金额、单据状态
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                        ");
            sql.AppendLine(",a.OutNo                                                                               ");
            sql.AppendLine(",a.Title                                                                               ");
            sql.AppendLine(",a.FromType                                                                            ");
            sql.AppendLine(",case a.FromType when '1' then '采购退货单' when '0' then '无来源' end as FromTypeName ");
            sql.AppendLine(",ISNULL(b.RejectNo,'') as RejectNo                                                     ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as SenderName	                                                       ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName                                                     ");
            sql.AppendLine(",ISNULL(k.CodeName,'') as ReasonTypeName");
            sql.AppendLine(",a.TotalPrice                                                                          ");
            sql.AppendLine(",a.CountTotal                                                                          ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                       ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                       ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'     ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName        ");
            sql.AppendLine("FROM officedba.StorageOutOther a                                                 ");
            sql.AppendLine("left join officedba.PurchaseReject b on a.FromBillID=b.ID                              ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID                                        ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=a.Sender                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Transactor							       ");
            sql.AppendLine(" left join officedba.CodeReasonType k on k.ID=a.ReasonType");
            sql.AppendLine(" left join officedba.StorageOutOtherDetail x on x.OutNo=a.OutNo and x.CompanyCD=a.CompanyCD ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");


            //出库单编号、出库单主题、源单类型（下拉列表）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like '%'+ @OutNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", model.OutNo));
            }
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            }
            if (!string.IsNullOrEmpty(model.ProjectID))
            {
                sql.AppendLine(" and a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.FromType))
            {
                sql.AppendLine(" and a.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
            }
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType = @ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
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

        public static DataTable GetStorageOutOtherTableBycondition(StorageOutOtherModel model, string IndexValue, string TxtValue, string timeStart, string timeEnd,string BatchNo, string orderby)
        {
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            //出库单编号、出库单主题、源单类型、出库人、出库时间、出库原因、摘要、出库数量、出库金额、单据状态
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID                                                                            ");
            sql.AppendLine(",a.OutNo                                                                               ");
            sql.AppendLine(",a.Title                                                                               ");
            sql.AppendLine(",a.FromType                                                                            ");
            sql.AppendLine(",case a.FromType when '1' then '采购退货单' when '0' then '无来源' end as FromTypeName ");
            sql.AppendLine(",ISNULL(b.RejectNo,'') as RejectNo                                                     ");
            sql.AppendLine(",ISNULL(d.EmployeeName,'') as SenderName	                                                       ");
            sql.AppendLine(",ISNULL(c.DeptName,'') as DeptName                                                     ");
            sql.AppendLine(",ISNULL(k.CodeName,'') as ReasonTypeName");
            sql.AppendLine(",a.TotalPrice                                                                          ");
            sql.AppendLine(",a.CountTotal                                                                          ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                       ");
            sql.AppendLine(",ISNULL(CONVERT(VARCHAR(10),a.OutDate,21),'') AS OutDate	                           ");
            sql.AppendLine(",ISNULL(e.EmployeeName,'') as Transactor	                                                       ");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'     ");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName        ");
            sql.AppendLine("FROM officedba.StorageOutOther a                                                 ");
            sql.AppendLine("left join officedba.PurchaseReject b on a.FromBillID=b.ID                              ");
            sql.AppendLine("left join officedba.DeptInfo c on c.ID=a.DeptID                                        ");
            sql.AppendLine("left join officedba.EmployeeInfo d on d.ID=a.Sender                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Transactor							       ");
            sql.AppendLine(" left join officedba.CodeReasonType k on k.ID=a.ReasonType ");
            sql.AppendLine(" left join officedba.StorageOutOtherDetail x on x.OutNo=a.OutNo and x.CompanyCD=a.CompanyCD ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("  AND (a.CanViewUser like '%" + "," + CanUserID + "," + "%' or '" + CanUserID + "' = a.Creator or a.CanViewUser = ',,' or a.CanViewUser is null )                                                     ");
            //出库单编号、出库单主题、源单类型（下拉列表）、
            //出库人（弹出窗口选择）、出库时间（日期段，日期控件）、出库原因（选择）、单据状态（下拉列表）
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.OutNo))
            {
                sql.AppendLine("	and a.OutNo like '%'+ @OutNo +'%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", model.OutNo));
            }
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and x.BatchNo = @BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(model.ProjectID))
            {
                sql.AppendLine(" and a.ProjectID = @ProjectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID", model.ProjectID));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title + '%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            }
            if (!string.IsNullOrEmpty(model.FromType))
            {
                sql.AppendLine(" and a.FromType = @FromType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", model.FromType));
            }
            if (!string.IsNullOrEmpty(model.ReasonType))
            {
                sql.AppendLine(" and a.ReasonType = @ReasonType");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType", model.ReasonType));
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

        #region 查看：其他出库单信息及其详细信息(加载页面的时候)
        /// <summary>
        /// 获取其他出库详细信息(加载页面的时候)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageOutOtherDetailInfo(StorageOutOtherModel model)
        {
            //a->officedba.StorageOutOther
            //b->officedba.StorageOutOtherDetail
            //k->officedba.SellSend
            //l->officedba.SellSendDetail

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT");
            sql.AppendLine("a.ID,a.ProjectID,AA.ProjectName,a.CanViewUser,BB.TypeName as ColorName,a.CanViewUserName,a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10   ");
            sql.AppendLine(",x.StorageName,y.CodeName as ReasonTypeName,a.CompanyCD                                                                                                              ");
            sql.AppendLine(",a.OutNo,c.IsBatchNo,b.UnitID as HiddenUnitID,b.BatchNo,b.UsedUnitID,b.UsedUnitCount,b.UsedPrice,b.ExRate,ii.CodeName as UsedUnitName                                                                                                                  ");
            sql.AppendLine(",a.FromType                                                                                                               ");
            sql.AppendLine(",a.FromBillID                                                                                                             ");
            sql.AppendLine(",ISNULL(k.RejectNo,'') as FromBillNo                                                                                ");
            sql.AppendLine(",a.OtherCorpID                                                                                                            ");
            sql.AppendLine(",ISNULL(a.CorpBigType,'') as CorpBigType ");//往来单位类型
            sql.AppendLine(",case a.FromType when 0 then ISNULL(o.CodeName,'') else ISNULL(p.CustName,'') end as OtherCorpName");
            sql.AppendLine(",case a.FromType when  '1' then '采购退货单' else '' end as FromTypeName  ");
            sql.AppendLine(",case a.CorpBigType when  '1' then '客户' when  '2' then '供应商' when  '3' then '竞争对手' when  '4' then '银行' ");
            sql.AppendLine(" when  '5' then '外协加工厂' when  '6' then '运输商' when  '7' then '其他' else '无来源' end as CorpBigTypeName  ");
            sql.AppendLine(",ISNULL(a.ReasonType,0) as ReasonType");
            sql.AppendLine(",a.SendAddr");
            sql.AppendLine(",a.ReceiveAddr");
            sql.AppendLine(",a.Title                                                                                                                  ");
            sql.AppendLine(",a.Sender                                                                                                                 ");
            sql.AppendLine(",z.EmployeeName as SenderName");
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
            sql.AppendLine(",c.ProdNo as ProductNo                                                                                                    ");
            sql.AppendLine(",c.ProductName                                                                                                            ");
            sql.AppendLine(",c.Specification                                                                                                          ");
            sql.AppendLine(",ISNULL(c.MinusIs,0) as MinusIs");
            sql.AppendLine(",q.CodeName as UnitID                                                                                                     ");
            sql.AppendLine(",b.UnitPrice as UnitPrice                                                                                                 ");
            sql.AppendLine(",b.StorageID                                                                                                              ");
            sql.AppendLine(",ISNULL(l.BackCount,0)-ISNULL(l.OutedTotal,0) as NotOutCount              ");
            sql.AppendLine(",ISNULL(l.BackCount,0) as BackCount                                                                                       ");//源单退货数量
            sql.AppendLine(",ISNULL(l.OutedTotal,0) as OutedTotal");
            sql.AppendLine(",ISNULL(b.ProductCount,0) as ProductCount                                                                                 ");
            sql.AppendLine(",b.TotalPrice as B_TotalPrice                                                                                             ");
            sql.AppendLine(",b.FromLineNo                                                                                                             ");
            sql.AppendLine(",b.SortNo                                                                                                                 ");
            sql.AppendLine(",b.Remark as DetaiRemark                                                                                                  ");
            sql.AppendLine(" ,ISNULL(s.ProductCount,0)+ISNULL(s.RoadCount,0)+ISNULL(s.InCount,0)-ISNULL(s.OrderCount,0)-ISNULL(s.OutCount,0) as UseCount ");
            sql.AppendLine(" FROM officedba.StorageOutOther a                                                                                          ");
            sql.AppendLine("left join officedba.StorageOutOtherDetail b                                                                               ");
            sql.AppendLine("on a.OutNo=b.OutNo  and a.CompanyCD=b.CompanyCD                                                                                                      ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Transactor=f.ID                                                                   ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID                                                                      ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                       ");
            sql.AppendLine("left join officedba.EmployeeInfo z on a.Sender=z.ID");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                           ");
            sql.AppendLine("left join officedba.ProjectInfo AA on a.ProjectID=AA.ID                                                                           ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=b.UnitID                                                                       ");
            sql.AppendLine("left join officedba.PurchaseReject k on k.ID=a.FromBillID                                                                 ");
            sql.AppendLine("left join officedba.CodeUnitType ii on ii.ID=b.UsedUnitID                                                                       ");
            sql.AppendLine("left join officedba.PurchaseRejectDetail l on l.RejectNo=k.RejectNo and l.SortNo=b.FromLineNo and l.CompanyCD=k.CompanyCD               ");
            sql.AppendLine("left join officedba.StorageProduct s on s.CompanyCD=a.CompanyCD and s.StorageID=b.StorageID and b.ProductID=s.ProductID AND b.BatchNo=s.BatchNo ");
            sql.AppendLine("left join officedba.CodeCompanyType o on a.CorpBigType=o.BigType and a.OtherCorpID=o.ID ");
            sql.AppendLine("left join officedba.ProviderInfo p on a.OtherCorpID=p.ID ");
            sql.AppendLine("left join officedba.StorageInfo x on x.ID=b.StorageID ");
            sql.AppendLine("left join officedba.CodeReasonType y on a.ReasonType=y.ID ");
            sql.AppendLine("left join officedba.CodePublicType BB on c.ColorID=BB.ID AND c.CompanyCD=BB.CompanyCD ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m ");
            sql.AppendLine("on a.ModifiedUserID=m.UserID ");
            sql.AppendLine("   where b.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID + " order by b.sortno asc ");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 插入其他出库和其他出库明细
        /// <summary>
        /// 插入其他出库和其他出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool InsertStorageOutOther(StorageOutOtherModel model, Hashtable htExtAttr, List<StorageOutOtherDetailModel> modelList, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.StorageOutOther(");
            strSql.Append("CompanyCD,OutNo,Title,FromType,FromBillID,ReasonType,CorpBigType,OtherCorpID,SendAddr,ReceiveAddr,Sender,DeptID,TotalPrice,CountTotal,Summary,OutDate,Transactor,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,ProjectID,CanViewUser,CanViewUserName)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@ReasonType,@CorpBigType,@OtherCorpID,@SendAddr,@ReceiveAddr,@Sender,@DeptID,@TotalPrice,@CountTotal,@Summary,@OutDate,@Transactor,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@ProjectID,@CanViewUser,@CanViewUserName)");
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
                //插入其他出库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutOtherDetail(");
                strSqlDetail.Append("CompanyCD,OutNo,SortNo,StorageID,ProductID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@OutNo,@SortNo,@StorageID,@ProductID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutOtherDetailInfo(commDetail, modelList[i]);
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

        #region 更新其他出库及其他出库明细
        /// <summary>
        /// 更新其他出库及其他出库明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static bool UpdateStorageOutOther(StorageOutOtherModel model, Hashtable htExtAttr, List<StorageOutOtherDetailModel> modelList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageOutOther set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("ReasonType=@ReasonType,");
            strSql.AppendLine("OtherCorpID=@OtherCorpID,");
            strSql.AppendLine("CorpBigType=@CorpBigType,");
            strSql.Append("SendAddr=@SendAddr,");
            strSql.Append("ReceiveAddr=@ReceiveAddr,");
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
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("ProjectID=@ProjectID");
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
            string delDetail = "delete from officedba.StorageOutOtherDetail where CompanyCD='" + model.CompanyCD + "' and OutNo='" + model.OutNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageOutOtherDetail(");
                strSqlDetail.Append("CompanyCD,OutNo,SortNo,StorageID,ProductID,UnitID,UnitPrice,ProductCount,TotalPrice,Remark,FromType,FromLineNo,ModifiedDate,ModifiedUserID,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@CompanyCD,@OutNo,@SortNo,@StorageID,@ProductID,@UnitID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
                strSqlDetail.Append(";select @@IDENTITY");


                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commDetail = new SqlCommand();
                    commDetail.CommandText = strSqlDetail.ToString();
                    EditOutOtherDetailInfo(commDetail, modelList[i]);
                    lstUpdate.Add(commDetail);//循环加入数组（重新获取页面上明细数据）
                    //SqlCommand commPD = updateStorageProduct(modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, false);
                    //lstUpdate.Add(commPD);
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
        private static void GetExtAttrCmd(StorageOutOtherModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageOutOther set ";
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
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageOutOtherModel model, bool flag)
        {
            //true 表示入库增加分仓存量数据
            StringBuilder strSql = new StringBuilder();
            if (flag == true)
            {

                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)+@ProductNum ");
                strSql.AppendLine(" where StorageID=@StorageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
                if (BatchNo != "")
                    strSql.AppendLine(" and BatchNo='" + BatchNo.Trim() + "' ");
                else
                    strSql.AppendLine(" and BatchNo is null ");
            }
            //否则 表示（入库减少）分仓存量数据（修改的时候）
            else
            {
                strSql.AppendLine("update officedba.StorageProduct set ");
                strSql.AppendLine("ProductCount=ISNULL(ProductCount,0)-@ProductNum ");
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

        #region 删除：其他出库信息
        /// <summary>
        /// 删除其他出库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageOutOther(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageOutOtherDetail where OutNo=(select OutNo from officedba.StorageOutOther where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageOutOther where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, StorageOutOtherModel model)
        {
            if (!string.IsNullOrEmpty(model.ID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            //@CompanyCD,@OutNo,@Title,@FromType,@FromBillID,@ReasonType,@CustID,
            //@SendAddr,@ReceiveAddr,@Sender,@DeptID,@TotalPrice,@CountTotal,
            //@Summary,@OutDate,@Transactor,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType ", model.FromType));//入库单类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID ", model.FromBillID));//源单ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReasonType ", model.ReasonType));//原因ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SendAddr ", model.SendAddr));//发货地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReceiveAddr ", model.ReceiveAddr));//收货地址
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Sender ", model.Sender));//经办人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID ", model.DeptID));//出库部门
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal ", model.CountTotal));//入库数量合计
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Summary ", model.Summary));//摘要
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Transactor ", model.Transactor));//入库人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutDate ", model.OutDate));//入库时间
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus ", model.BillStatus));//单据状态
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OtherCorpID ", model.OtherCorpID));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CorpBigType ", model.CorpBigType));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectID ", model.ProjectID));//
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
        private static void EditOutOtherDetailInfo(SqlCommand comm, StorageOutOtherDetailModel model)
        {
            //@CompanyCD,@OutNo,@SortNo,@StorageID,@ProductID,@UnitPrice,@ProductCount,
            //@TotalPrice,@Remark,@FromType,@FromLineNo,getdate(),@ModifiedUserID

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo ", model.OutNo));//出库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));//仓库ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价(基本单价)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量(基本数量)
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库金额
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
        public static bool ConfirmBill(StorageOutOtherModel model,out string retstrval)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutOther SET");
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


            List<StorageOutOtherDetailModel> modelList = new List<StorageOutOtherDetailModel>();
            string sqlSele = "select ProductID,StorageID,CompanyCD,OutNo,UnitPrice,BatchNo,UsedUnitCount,ProductCount,FromType,FromLineNo from officedba.StorageOutOtherDetail where CompanyCD='" + model.CompanyCD + "'"
                            + "and OutNo=(select OutNo from officedba.StorageOutOther where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageOutOtherDetailModel modelDetail = new StorageOutOtherDetailModel();
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
                    if (dt.Rows[i]["FromType"].ToString() != "")
                    {
                        modelDetail.FromType = dt.Rows[i]["FromType"].ToString();
                    }
                    if (dt.Rows[i]["FromLineNo"].ToString() != "")
                    {
                        modelDetail.FromLineNo = dt.Rows[i]["FromLineNo"].ToString();
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
                    AccountM_.BillType = 8;
                    AccountM_.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    AccountM_.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.HappenDate = System.DateTime.Now;
                    AccountM_.PageUrl = "../Office/StorageManager/StorageOutOtherAdd.aspx";
                    if (dt.Rows[i]["UnitPrice"].ToString().Trim() == "")
                        AccountM_.Price = 0;
                    else
                        AccountM_.Price = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                    AccountM_.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    AccountM_.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    AccountM_.StorageID = Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    SqlCommand AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"1");
                    lstConfirm.Add(AccountCom_);
                    #endregion
                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                if (modelList[0].FromType == "1")
                {
                    StringBuilder strAddPRetail = new StringBuilder();//增加采购退货货单明细中的已出库数量
                    strAddPRetail.AppendLine("update officedba.PurchaseRejectDetail set ");
                    strAddPRetail.AppendLine(" OutedTotal =ISNULL(OutedTotal,0)+@ReBackNum where ");
                    strAddPRetail.AppendLine(" RejectNo=(select RejectNo from officedba.PurchaseReject where ID=(select FromBillID from officedba.StorageOutOther where ID=" + model.ID + "))");
                    strAddPRetail.AppendLine(" and SortNo=@SortNo");

                    for (int i = 0; i < modelList.Count; i++)
                    {
                        SqlCommand commRePR = new SqlCommand();
                        commRePR.CommandText = strAddPRetail.ToString();

                        commRePR.Parameters.Add(SqlHelper.GetParameterFromString("@ReBackNum", modelList[i].UsedUnitCount));//回写增加的数量
                        commRePR.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", modelList[i].FromLineNo));
                        lstConfirm.Add(commRePR);//循环加入数组（把PurchaseRejectDetail已经入库数量增加）

                        SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, false);
                        lstConfirm.Add(commPD);
                    }
                }
                else//FromType=0的时候，也是无来源的时候，只要更新StorageProduct中数据
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        SqlCommand commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, modelList[i].StorageID, modelList[i].ProductCount, model, false);
                        lstConfirm.Add(commPD);
                    }
                }
            }
            bool retval = SqlHelper.ExecuteTransWithArrayList(lstConfirm);
            if (retval)
            {
                string sqlFrom = "select TotalPrice,OtherCorpID from officedba.StorageOutOther "
                                +" where ID=" + model.ID + " ";
                DataTable dtFrom = SqlHelper.ExecuteSql(sqlFrom);
                int custid=0;
                DataTable dtCurrtype = XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(model.CompanyCD);
                string IsVoucher = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher ? "1" : "0";
                string IsApply = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply ? "1" : "0";
                if(dtFrom.Rows[0]["OtherCorpID"].ToString().Trim()!="")custid=Convert.ToInt32(dtFrom.Rows[0]["OtherCorpID"].ToString().Trim());
                bool VocherFlag = XBase.Data.Office.FinanceManager.AutoVoucherDBHelper.AutoVoucherInsert(6, model.CompanyCD, IsVoucher, IsApply, Convert.ToDecimal(dtFrom.Rows[0]["TotalPrice"].ToString()), "officedba.StorageOutOther," + model.ID, dtCurrtype.Rows[0]["ID"].ToString() +","+dtCurrtype.Rows[0]["ExchangeRate"].ToString(), custid, out retstrval);
                if (VocherFlag) retstrval = "确认成功！";
                else retstrval = "确认成功！" + retstrval;
            }
            else retstrval = "";
            return retval;
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageOutOtherModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutOther SET");
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
        public static bool CancelCloseBill(StorageOutOtherModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageOutOther SET");
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

        #region 采购退货明细列表（弹出层显示）
        /// <summary>
        /// 采购退货明细列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetPRDetailInfo(string CompanyCD, string RejectNo, string Title)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select a.ID,a.RejectNo,ISNULL(BB.TypeName,'')ColorName,a.Title,isnull(q.CodeName,'')CodeName,ISNULL(b.UnitPrice,0)UnitPrice                                                                        ");
            sql.AppendLine(",b.ID as DetailID,b.ProductID                                                                         ");
            sql.AppendLine(",case when a.RejectDate IS NULL then '' else CONVERT(varchar(10),a.RejectDate, 23) end as RejectDate  ");
            sql.AppendLine(",ISNULL(i.ProductName,'') as ProductName                                                              ");

            sql.AppendLine(",ISNULL(b.BackCount,0) as BackCount                                                             ");
            sql.AppendLine(",ISNULL(b.OutedTotal,0) as OutedTotal                                                                 ");
            sql.AppendLine("from officedba.PurchaseRejectDetail b                                                                 ");
            sql.AppendLine("left outer join officedba.PurchaseReject a on b.RejectNo=a.RejectNo and a.CompanyCD=b.CompanyCD                                  ");
            sql.AppendLine("left join officedba.ProductInfo i on b.ProductID=i.ID                               				  ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=b.UnitID                                              ");
            sql.AppendLine("left join officedba.CodePublicType BB on i.ColorID=BB.ID AND i.CompanyCD=BB.CompanyCD ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2");
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(RejectNo))
            {
                sql.AppendLine(" and a.RejectNo like '%'+ @RejectNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@RejectNo", RejectNo));
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

        #region 根据采购退货单明细中ID数组来获取信息（填充出库单中的明细）
        /// <summary>
        /// 根据采购退货单明细中ID数组来获取信息（填充入库单中的明细)
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select b.ID,a.RejectNo,a.UsedUnitID,BB.TypeName as ColorName,a.UsedUnitCount,a.UsedPrice,a.ExRate,a.UnitID as HiddenUnitID,p.IsBatchNo                                                    ");
            sql.AppendLine(",a.ProductID                                                              ");
            sql.AppendLine(",ISNULL(p.ProdNo,'') as ProdNo                                            ");
            sql.AppendLine(",ISNULL(p.ProductName,'') as ProductName                                  ");
            sql.AppendLine(",q.CodeName as UnitID                                                     ");
            sql.AppendLine(",ISNULL(p.Specification,'') as Specification,p.StorageID      ");
            sql.AppendLine(",ISNULL(a.BackCount,0) as BackCount                                    ");
            sql.AppendLine(",ISNULL(a.OutedTotal,0) as OutedTotal                                     ");
            sql.AppendLine(",ISNULL(a.BackCount,0)-ISNULL(a.OutedTotal,0) as NotOutCount              ");
            sql.AppendLine(",b.ProviderID");
            //sql.AppendLine(",case ISNULL(b.ProviderID,0) when 0 then '' else convert(varchar(20),b.ProviderID) end  ProviderID ");
            //sql.AppendLine(",ISNULL(o.CustName,'') as ProviderName");
            sql.AppendLine(",ISNULL(pp.CustName,'') as ProviderName");
            sql.AppendLine(",a.TaxPrice as UnitPrice                                                  ");
            sql.AppendLine(",a.TaxPrice*(ISNULL(a.BackCount,0)-ISNULL(a.OutedTotal,0)) as TotalPrice  ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark                                            ");
            sql.AppendLine(",a.SortNo                                                                 ");
            sql.AppendLine(" from officedba.PurchaseRejectDetail a                                    ");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID                    ");
            sql.AppendLine(" left join officedba.PurchaseReject b on b.RejectNo=a.RejectNo and a.CompanyCD=b.CompanyCD           ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=a.UnitID                      ");
            sql.AppendLine(" left join officedba.PurchaseArrive j on a.FromBillID=j.ID        ");
            sql.AppendLine(" left join officedba.ProviderInfo o on j.ProviderID=o.ID ");
            sql.AppendLine(" left join officedba.ProviderInfo pp on pp.ID=b.ProviderID");
            sql.AppendLine("left join officedba.CodePublicType BB on p.ColorID=BB.ID AND p.CompanyCD=BB.CompanyCD ");
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
        public static bool ISConfirmBill(StorageOutOtherModel model)
        {
            bool Result = true;//true表示可以确认
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.FromBillID,a.FromType,b.ProductID,b.ProductCount,ISNULL(b.UsedUnitCount,0)UsedUnitCount                                ");
            sql.AppendLine(",ISNULL(l.BackCount,0)-ISNULL(l.OutedTotal,0) as NotOutCount                                  ");
            sql.AppendLine(" from officedba.StorageOutOtherDetail b                                                       ");
            sql.AppendLine("left join officedba.StorageOutOther a on a.OutNo=b.OutNo                                      ");
            sql.AppendLine("left join officedba.PurchaseReject k on k.ID=a.FromBillID                                     ");
            sql.AppendLine("left join officedba.PurchaseRejectDetail l on l.RejectNo=k.RejectNo and k.CompanyCD=l.CompanyCD and l.SortNo=b.FromlineNo ");
            sql.AppendLine(" where a.CompanyCD='" + model.CompanyCD + "' and a.ID=" + model.ID);
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FromType"].ToString() == "1")//如果有来源才去判断是否大于源单未出库数量
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
            }
            return Result;
        }

        #endregion


        #region 确认的时候判断,当物品不允许负库存，是否大于可用库存
        /// <summary>
        /// 查找出当前单据中明细，所有不允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCant(StorageOutOtherModel model)
        {
            string batchsql = "SELECT A.BatchNo FROM officedba.StorageOutOtherDetail A LEFT OUTER JOIN officedba.StorageOutOther B on A.OutNo=B.OutNo AND A.CompanyCD=B.CompanyCD where A.CompanyCD='" + model.CompanyCD + "' and B.ID=" + model.ID + "";
            DataTable dtbatch = SqlHelper.ExecuteSql(batchsql.ToString());

            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存
            int point = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

            if (dtbatch.Rows.Count > 0)
            {
                for (int i = 0; i < dtbatch.Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select b.FromType,b.ID,a.ProductID,a.ExRate,a.StorageID,a.ProductCount                                                               ");
                    sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
                    sql.AppendLine(",Convert(numeric(22," + point + "),ISNULL(c.ProductCount,0)) as UseCount ");
                    sql.AppendLine(" from officedba.StorageOutOtherDetail a                                                                                     ");
                    sql.AppendLine("left outer join officedba.StorageOutOther b on a.OutNo=b.OutNo and a.CompanyCD=b.CompanyCD                                                                   ");
                    sql.AppendLine("left outer join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                                 ");//AND a.BatchNo=c.BatchNo
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND a.BatchNo=c.BatchNo    ");
                    sql.AppendLine("left outer join officedba.ProductInfo d on d.ID=a.ProductID                                                                      ");  //and ISNULL(d.MinusIS,0)='0'
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

        #region 确认的时候判断,当物品允许负库存，是否大于可用库存(不用了)
        /// <summary>
        /// 查找出当前单据中明细，所有允许的负库存的物品，然后判断是否出库数量大于负库存
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns>string:行号数组|对应行号的可用库存</returns>
        public static string ISBigUseCountWhenCan(StorageOutOtherModel model)
        {
            string batchsql = "SELECT A.BatchNo FROM officedba.StorageOutOtherDetail A LEFT OUTER JOIN officedba.StorageOutOther B on A.OutNo=B.OutNo AND A.CompanyCD=B.CompanyCD where A.CompanyCD='" + model.CompanyCD + "' and B.ID=" + model.ID + "";
            DataTable dtbatch = SqlHelper.ExecuteSql(batchsql.ToString());

            string RowNumList = string.Empty;//有状况的明细行号
            string UseCountList = string.Empty;//有状况的明细对应的可有库存
            int point = Convert.ToInt32(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);

            if (dtbatch.Rows.Count > 0)
            {
                for (int i = 0; i < dtbatch.Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select b.FromType,b.ID,a.ExRate,a.ProductID,a.StorageID,a.ProductCount                                                               ");
                    sql.AppendLine(",ISNULL(d.MinusIs,0) as MinusIs                                                                                             ");
                    sql.AppendLine(",Convert(numeric(22," + point + "),ISNULL(c.ProductCount,0)) as UseCount ");
                    sql.AppendLine(" from officedba.StorageOutOtherDetail a                                                                                     ");
                    sql.AppendLine("left outer join officedba.StorageOutOther b on a.OutNo=b.OutNo                                                                    ");
                    sql.AppendLine("left outer join officedba.StorageProduct c on a.StorageID=c.StorageID and a.ProductID=c.ProductID                                 ");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND a.BatchNo=c.BatchNo    ");
                    sql.AppendLine("left outer join officedba.ProductInfo d on d.ID=a.ProductID   and ISNULL(d.MinusIS,0)='1'                                                                    ");
                    sql.AppendLine(" where  a.CompanyCD='" + model.CompanyCD + "' and b.ID=" + model.ID + " ");
                    if (dtbatch.Rows[i]["BatchNo"].ToString().Trim() != "")
                        sql.AppendLine(" AND c.BatchNo='" + dtbatch.Rows[i]["BatchNo"].ToString().Trim() + "'    ");
                    else
                        sql.AppendLine(" AND (c.BatchNo is null or c.BatchNo='')   ");
                    sql.AppendLine("  order by a.sortno asc");
                    DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
                    if (dt.Rows.Count>0)
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


        #region 判断当前storageID中对应的ProductID记录是否存在
        private static bool Exists(string storageID, string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=@storageID and ProductID=@ProductID and CompanyCD=@CompanyCD");
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

        #region 返回不存在于分仓存量表中的行号
        //判断分仓存量表中是否有不存在的记录。
        //当在无来源的时候，选择物品没有通过当前仓库选择，而是从所有的仓库中选择物品
        //就会有这样的情况，当确认的时候，而物品又允许负库存的时候就会出现，在分仓存量表中
        //不存在也时候也能确认。

        public static string ifExist(StorageOutOtherModel model)
        {
            //返回的行号，就是在分仓存量表中不存在的记录。（1，2，5）
            string ReNumList = string.Empty;
            List<StorageOutOtherDetailModel> modelList = new List<StorageOutOtherDetailModel>();
            string sqlSele = "select CompanyCD,ProductID,StorageID,FromType from officedba.StorageOutOtherDetail where CompanyCD='" + model.CompanyCD + "'"
                            + "and OutNo=(select OutNo from officedba.StorageOutOther where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageOutOtherDetailModel modelDetail = new StorageOutOtherDetailModel();
                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                    }
                    if (dt.Rows[i]["StorageID"].ToString() != "")
                    {
                        modelDetail.StorageID = dt.Rows[i]["StorageID"].ToString();
                    }

                    if (dt.Rows[i]["FromType"].ToString() != "")
                    {
                        modelDetail.FromType = dt.Rows[i]["FromType"].ToString();
                    }
                    if (dt.Rows[i]["CompanyCD"].ToString() != "")
                    {
                        modelDetail.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    }
                    modelList.Add(modelDetail);
                }
            }

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                //当当前单据是无来源的时候
                if (modelList[0].FromType == "0")
                {
                    //循环
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        //如果判断当前行是返回false那么则记录当前行
                        if (!Exists(modelList[i].StorageID, modelList[i].ProductID, modelList[i].CompanyCD))
                        {
                            if (ReNumList == "" || ReNumList == string.Empty)
                            {
                                ReNumList = (i + 1).ToString();
                            }
                            else
                            {
                                ReNumList += "," + (i + 1).ToString();
                            }
                        }
                    }
                }
            }
            return ReNumList;
        }
        #endregion

        #region 单据打印
        public static DataTable GetStorageOutOtherInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutOther where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageOutOtherDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageOutOtherDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        #endregion
    }
}
