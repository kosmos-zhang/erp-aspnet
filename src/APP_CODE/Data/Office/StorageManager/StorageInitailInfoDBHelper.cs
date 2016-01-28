/**********************************************
 * 类作用：   期初库存数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/13
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
    public class StorageInitailInfoDBHelper
    {

        #region 查询：期初库存录入信息
        /// <summary>
        /// 查询期初库存录入信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageInitailTableBycondition(string BatchNo,string EFIndex,string EFDesc,StorageInitailModel model, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT a.ID");
            sql.AppendLine(" ,ISNULL(a.InNo,'') AS InNo");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title");
            sql.AppendLine(" ,ISNULL(b.StorageName,'') AS StorageName");
            sql.AppendLine(" ,ISNULL(c.EmployeeName,'') AS Executor");
            sql.AppendLine(" ,ISNULL(d.DeptName,'') AS DeptName");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end as BillStatus");
            sql.AppendLine(" ,ISNULL(a.CountTotal,0) AS CountTotal");
            sql.AppendLine(" ,ISNULL(a.TotalPrice,0) AS TotalPrice");
            sql.AppendLine(" FROM officedba.StorageInitail a ");            
            sql.AppendLine(" left join officedba.EmployeeInfo c on a.Executor = c.ID");
            sql.AppendLine(" left join officedba.DeptInfo d on a.DeptID = d.ID");
            sql.AppendLine(" left join officedba.StorageInfo b on a.StorageID = b.ID");
            sql.AppendLine(" left join officedba.UserInfo h on a.ModifiedUserID=h.UserID ");
            sql.AppendLine(" left join officedba.StorageInitailDetail i on i.InNo = a.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and i.BatchNo like '%'+ @BatchNo +'%'");
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
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID=@StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor=@Executor");
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
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " like @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        
        public static DataTable GetStorageInitailTableBycondition(string BatchNo,StorageInitailModel model, string timeStart, string timeEnd, string orderby)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID");
            sql.AppendLine(" ,ISNULL(a.InNo,'') AS InNo");
            sql.AppendLine(",ISNULL(a.Title,'') AS Title");
            sql.AppendLine(" ,ISNULL(b.StorageName,'') AS StorageName");
            sql.AppendLine(" ,ISNULL(c.EmployeeName,'') AS Executor");
            sql.AppendLine(" ,ISNULL(d.DeptName,'') AS DeptName");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate");
            sql.AppendLine(",case a.billStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine(" when '4' then '手工结单' when '5' then '自动结单' end as BillStatus");
            sql.AppendLine(" ,ISNULL(a.CountTotal,0) AS CountTotal");
            sql.AppendLine(" ,ISNULL(a.TotalPrice,0) AS TotalPrice");
            sql.AppendLine(" FROM officedba.StorageInitail a ");
            sql.AppendLine(" left join officedba.EmployeeInfo c on a.Executor = c.ID");
            sql.AppendLine(" left join officedba.DeptInfo d on a.DeptID = d.ID");
            sql.AppendLine(" left join officedba.StorageInfo b on a.StorageID = b.ID");
            sql.AppendLine(" left join officedba.UserInfo h on a.ModifiedUserID=h.UserID ");
            sql.AppendLine(" left join officedba.StorageInitailDetail i on i.InNo = a.InNo ");
            sql.AppendLine("where a.CompanyCD=@CompanyCD");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sql.AppendLine(" and i.BatchNo like '%'+ @BatchNo +'%'");
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
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                sql.AppendLine(" and a.StorageID=@StorageID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
            }
            if (!string.IsNullOrEmpty(model.DeptID))
            {
                sql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.DeptID));
            }
            if (!string.IsNullOrEmpty(model.Executor))
            {
                sql.AppendLine(" and a.Executor=@Executor");
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
            if (!string.IsNullOrEmpty(orderby))
            {
                sql.AppendLine(" order by " + orderby);
            }

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 期初库存和期初库存明细详细信息
        public static DataTable GetStorageInitailDetailInfo(StorageInitailModel model)
        {
            //string[] sql = new string[2];

            //查询期初入库详细信息
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT a.ID                                                                                                               ");
            sql.AppendLine(",a.InNo,  b.BatchNo                                                                                                                  ");
            sql.AppendLine(",a.Title                                                                                                                  ");
            sql.AppendLine(",a.StorageID                                                                                                              ");
            sql.AppendLine(",s.StorageName                                                                                                              ");
            sql.AppendLine(",a.Executor                                                                                                               ");
            sql.AppendLine(",a.ExtField1                                                                                                               ");
            sql.AppendLine(",a.ExtField2                                                                                                               ");
            sql.AppendLine(",a.ExtField3                                                                                                               ");
            sql.AppendLine(",a.ExtField4                                                                                                               ");
            sql.AppendLine(",a.ExtField5                                                                                                               ");
            sql.AppendLine(",a.ExtField6                                                                                                               ");
            sql.AppendLine(",a.ExtField7                                                                                                               ");
            sql.AppendLine(",a.ExtField8                                                                                                               ");
            sql.AppendLine(",a.ExtField9                                                                                                               ");
            sql.AppendLine(",a.ExtField10                                                                                                               ");
            sql.AppendLine(",ISNULL(f.EmployeeName,'') as ExecutorName                                                                                ");
            sql.AppendLine(",case when a.EnterDate Is NULL then '' else CONVERT(VARCHAR(10),a.EnterDate, 21) end AS EnterDate                         ");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark                                                                                            ");
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
            sql.AppendLine(",a.ModifiedUserID as ModifiedUserName                                                                                       ");
            sql.AppendLine(",a.BillStatus                                                                                                             ");
            sql.AppendLine(",case a.BillStatus when  '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatusName   ");
            sql.AppendLine(",a.DeptID                                                                                                                 ");
            sql.AppendLine(",ISNULL(j.DeptName,'') as DeptName                                                                                        ");
            sql.AppendLine(",ISNULL(a.Summary,'') as Summary                                                                                          ");
            sql.AppendLine(",ISNULL(a.TotalPrice,'0') as A_TotalPrice                                                                                 ");
            sql.AppendLine(",ISNULL(a.CountTotal,'0') as CountTotal                                                                                   ");
            sql.AppendLine(",b.ID as DetailID                                                                                                         ");
            sql.AppendLine(",b.ProductID                                                                                                              ");
            sql.AppendLine(",b.SortNo                                                                                                              ");
            sql.AppendLine(",ISNULL(c.ProdNo,'') as ProductNo                                                                                         ");
            sql.AppendLine(",ISNULL(c.ProductName,'') as ProductName                                                                                  ");
            sql.AppendLine(",ISNULL(q.CodeName,'') as UnitID                                                                                          ");
            sql.AppendLine(",ISNULL(c.Specification,'') as Specification                                                                              ");
            sql.AppendLine(",ISNULL(b.UnitPrice,0) as UnitPrice                                                                                       ");
            sql.AppendLine(",ISNULL(b.TotalPrice,'0') as B_TotalPrice                                                                                 ");
            sql.AppendLine(",ISNULL(b.ProductCount,0) as ProductCount                                                                                 ");
            sql.AppendLine(",ISNULL(b.Remark,'') as DetaiRemark,b.TotalPrice");

            sql.AppendLine(",b.UsedUnitID as UsedUnitID,z.CodeName UsedUnitName ");
            sql.AppendLine(", b.UsedUnitCount  as UsedUnitCount                                                                                       ");
            sql.AppendLine(",ISNULL(b.UsedPrice,0) as UsedPrice                                                                                       ");
            sql.AppendLine(",b.ExRate as ExRate                                                                                       ");
            sql.AppendLine(",c.IsBatchNo ");

            sql.AppendLine("FROM officedba.StorageInitail a                                                                                     ");
            sql.AppendLine("left join officedba.StorageInitailDetail b on a.InNo=b.InNo and a.CompanyCD=b.CompanyCD                                                              ");
            sql.AppendLine("left join officedba.ProductInfo c on c.ID=b.ProductID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo f on a.Executor=f.ID                                                                     ");
            sql.AppendLine("left join officedba.EmployeeInfo g on a.Creator=g.ID     																								                  ");
            sql.AppendLine("left join officedba.EmployeeInfo h on a.Confirmor=h.ID                                                                    ");
            sql.AppendLine("left join officedba.EmployeeInfo i on a.Closer=i.ID                                                                       ");
            sql.AppendLine("left join officedba.DeptInfo j on a.DeptID=j.ID                                                                           ");
            sql.AppendLine("left join officedba.CodeUnitType q on q.ID=c.UnitID                                                                       ");
            sql.AppendLine("left join officedba.StorageInfo s on s.ID=a.StorageID                                                                       ");
            sql.AppendLine("left join officedba.CodeUnitType z on z.ID=b.UsedUnitID ");
            sql.AppendLine("left join (select w.UserID,x.EmployeeName from officedba.UserInfo w ,officedba.EmployeeInfo x where w.EmployeeID =x.ID) m ");
            sql.AppendLine("		on a.ModifiedUserID=m.UserID																																													");
            sql.AppendLine("   where a.CompanyCD='" + model.CompanyCD + "' and a.id=" + model.ID);
            return SqlHelper.ExecuteSql(sql.ToString());

            //SortNo,InNo,ProductID,UnitID,UnitPrice,TotalPrice,Remark
            //查询标准工序下的工艺明细列表信息
            // sql[1] = "select * from officedba.StorageInitailDetail a inner join officedba.StorageInitail b on b.InNo=a.InNo and b.CompanyCD='" + model.CompanyCD;

            //return SqlHelper.ExecuteSql(sql.ToString());

        }
        #endregion

        #region 添加：期初入库单信息及其详细信息
        /// <summary>
        /// 添加期初入库单信息及其详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool InsertStorageInitail(StorageInitailModel model, List<StorageInitailDetailModel> modelList,Hashtable ht, out int IndexIDentity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into officedba.StorageInitail(");
            strSql.AppendLine("CompanyCD,InNo,Title,StorageID,Executor,EnterDate,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID,DeptID,Summary,TotalPrice,CountTotal)");
            strSql.AppendLine(" values (");
            strSql.AppendLine("@CompanyCD,@InNo,@Title,@StorageID,@Executor,@EnterDate,@Remark,@Creator,getdate(),@BillStatus,getdate(),@ModifiedUserID,@DeptID,@Summary,@TotalPrice,@CountTotal)");
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
            GetExtAttrCmd(model, ht, cmd);
            if (ht.Count > 0)
                lstInsert.Add(cmd);
            #endregion

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                //插入期初入库明细
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInitailDetail(");
                strSqlDetail.Append("InNo,SortNo,ProductID,UnitPrice,ProductCount,TotalPrice,Remark,ModifiedDate,ModifiedUserID,CompanyCD,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@SortNo,@ProductID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,getdate(),@ModifiedUserID,@CompanyCD,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
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

        #region 修改：期初入库单（期初入库单信息和详细信息）

        public static bool UpdateStorageInitail(StorageInitailModel model, List<StorageInitailDetailModel> modelList,Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.StorageInitail set ");
            strSql.Append("Title=@Title,");
            strSql.Append("StorageID=@StorageID,");
            strSql.Append("Executor=@Executor,");
            strSql.Append("EnterDate=@EnterDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("Summary=@Summary,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("CountTotal=@CountTotal");
            strSql.Append(" where ID=@ID ");
            SqlCommand comm = new SqlCommand(strSql.ToString());
            SetSaveParameter(comm, model);
            ArrayList lstUpdate = new ArrayList();
            lstUpdate.Add(comm);//数组加入插入基表的command

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, ht, cmd);
            if (ht.Count > 0)
                lstUpdate.Add(cmd);
            #endregion

            //先删掉明细表中对应单据的所有数据
            string delDetail = "delete from officedba.StorageInitailDetail where CompanyCD='" + model.CompanyCD + "' and InNo='" + model.InNo + "'";
            SqlCommand commdel = new SqlCommand(delDetail);
            lstUpdate.Add(commdel);

            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                StringBuilder strSqlDetail = new StringBuilder();
                strSqlDetail.Append("insert into officedba.StorageInitailDetail(");
                strSqlDetail.Append("InNo,SortNo,ProductID,UnitPrice,ProductCount,TotalPrice,Remark,ModifiedDate,ModifiedUserID,CompanyCD,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,BatchNo)");
                strSqlDetail.Append(" values (");
                strSqlDetail.Append("@InNo,@SortNo,@ProductID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,getdate(),@ModifiedUserID,@CompanyCD,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@BatchNo)");
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

        #region 删除：期初入库信息
        /// <summary>
        /// 删除期初入库信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteStorageInitail(string ID, string CompanyCD)
        {
            string[] sql = new string[2];
            sql[0] = "delete from officedba.StorageInitailDetail where InNo=(select InNo from officedba.StorageInitail where ID=" + ID + ")";
            sql[1] = "delete from  officedba.StorageInitail where CompanyCD='" + CompanyCD + "' and ID=" + ID;

            return SqlHelper.ExecuteTransForListWithSQL(sql);
        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageInitailModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageInitail set ";
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

        #region 确认
        public static bool ConfirmBill(StorageInitailModel model)
        {
            ArrayList lstConfirm = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInitail SET");
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


            List<StorageInitailDetailModel> modelList = new List<StorageInitailDetailModel>();
            string sqlSele = "select a.ID,a.CompanyCD, a.ProductID,b.StorageID,a.BatchNo,a.InNo BillNo,a.UnitPrice Price,Convert(varchar(10),b.EnterDate,23) HappenDate," +
                            " a.Remark, a.ProductCount from officedba.StorageInitailDetail a left join" +
                             " officedba.StorageInitail b on a.InNo=b.InNo  and a.CompanyCD = b.CompanyCD where a.CompanyCD='" + model.CompanyCD + "' and a.InNo=(select InNo from officedba.StorageInitail where ID=" + model.ID + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlSele);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StorageInitailDetailModel modelDetail = new StorageInitailDetailModel();
                    StorageAccountModel StorageAccountM = new StorageAccountModel();

                    if (dt.Rows[i]["ProductID"].ToString() != "")
                    {
                        modelDetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                        StorageAccountM.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                    }
                    if (dt.Rows[i]["ProductCount"].ToString() != "")
                    {
                        modelDetail.ProductCount = dt.Rows[i]["ProductCount"].ToString();
                        StorageAccountM.HappenCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                        StorageAccountM.ProductCount = Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString());
                    }                   
                    if (dt.Rows[i]["StorageID"].ToString() != "")
                    {
                        model.StorageID = dt.Rows[i]["StorageID"].ToString();
                        StorageAccountM.StorageID = Convert.ToInt32(dt.Rows[i]["StorageID"].ToString());
                    }
                    //插入流水账表
                    StorageAccountM.CompanyCD = dt.Rows[i]["CompanyCD"].ToString();
                    StorageAccountM.BillType = 1;
                    if (dt.Rows[i]["BatchNo"].ToString() != "")
                    {
                        modelDetail.BatchNo = dt.Rows[i]["BatchNo"].ToString(); 
                        StorageAccountM.BatchNo = dt.Rows[i]["BatchNo"].ToString(); 
                    }

                    modelList.Add(modelDetail);
                    
                    StorageAccountM.BillNo = dt.Rows[i]["BillNo"].ToString();
                    StorageAccountM.Price = Convert.ToDecimal(dt.Rows[i]["Price"].ToString());
                    StorageAccountM.HappenDate = Convert.ToDateTime(dt.Rows[i]["HappenDate"].ToString());                   
                    StorageAccountM.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    StorageAccountM.PageUrl = "../Office/StorageManager/StorageInitailAdd.aspx";
                    StorageAccountM.ReMark = dt.Rows[i]["Remark"].ToString();

                    SqlCommand commSA = new SqlCommand();
                    commSA = StorageAccountDBHelper.InsertStorageAccountCommand(StorageAccountM, "0");
                    lstConfirm.Add(commSA);                    

                }
            }
            if (modelList != null && modelList.Count > 0)//明细不为空的时候
            {
                for (int i = 0; i < modelList.Count; i++)
                {
                    SqlCommand commPD = new SqlCommand();
                    if (Exists(modelList[i].BatchNo,model.StorageID, modelList[i].ProductID, model.CompanyCD))
                    {
                        commPD = updateStorageProduct(modelList[i].BatchNo, modelList[i].ProductID, model.StorageID, modelList[i].ProductCount, model, true);
                    }
                    else
                    {
                        commPD = InsertStorageProduct(modelList[i].BatchNo,modelList[i].ProductID, model.StorageID, modelList[i].ProductCount, model.CompanyCD);
                    }
                    lstConfirm.Add(commPD);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(lstConfirm);
        }
        #endregion

        #region 结单
        public static bool CloseBill(StorageInitailModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInitail SET");
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
        public static bool CancelCloseBill(StorageInitailModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageInitail SET");
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
        private static void SetSaveParameter(SqlCommand comm, StorageInitailModel model)
        {
            if (model.ID != null && model.ID != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID ", model.ID));//自动生成
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator ", model.Creator));//制单人
            }
            //@CompanyCD,@InNo,@Title,@StorageID,@Executor,@EnterDate,
            //@Remark,@Creator,@CreateDate,@BillStatus,getdate(),@ModifiedUserID,@DeptID,@Summary,@TotalPrice,@CountTotal
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title ", model.Title));//主题
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID ", model.StorageID));
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
        }
        #endregion

        #region 明细参数设置
        /// <summary>
        /// 明细参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void EditInitailDetailInfo(SqlCommand comm, StorageInitailDetailModel model)
        {
            //@InNo,@SortNo,@ProductID,@UnitPrice,@ProductCount,@TotalPrice,@Remark,getdate(),@ModifiedUserID,@CompanyCD
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InNo ", model.InNo));//入库单编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID ", model.ProductID));//物品ID
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice ", model.UnitPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice ", model.TotalPrice));//入库单价
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount ", model.ProductCount));//入库数量
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark ", model.Remark));//备注
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", model.ModifiedUserID));//最后更新用户ID（对应操作用户表中的UserID）
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo ", model.SortNo));//
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID ", model.UsedUnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount ", model.UsedUnitCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice ", model.UsedPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate ", model.ExRate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo ", model.BatchNo));

        }
        #endregion

        #region 更新分仓存量表
        public static SqlCommand updateStorageProduct(string BatchNo, string ProductID, string StorageID, string ProductNum, StorageInitailModel model, bool flag)
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


        #region 期初入库是否可新建和确认了
        public static bool ISADD(string CompanyCD)
        {
            bool result = true;
            string strSql = "select count(*) from officedba.StorageMonthly where CompanyCD='" + CompanyCD + "'";
            int a = int.Parse(SqlHelper.ExecuteScalar(strSql, null).ToString());
            if (a > 0)
            {
                result = false;
            }
            return result;
        }
        #endregion


        #region 导出
        public static DataTable GetStorageInitailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInitail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetStorageInitailDetailInfo(string ID, string CompanyCD)
        {
            string sql = "select * from officedba.V_StorageInitailDetail where ID=" + ID + " and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

    }
}
