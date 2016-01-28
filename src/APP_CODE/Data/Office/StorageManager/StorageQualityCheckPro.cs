
using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using XBase.Common;
using System.Collections;
namespace XBase.Data.Office.StorageManager
{
    public class StorageQualityCheckProDBHelper
    {
        public static DataTable GetQualityCheckPro(string Method, string QuaStr, string CompanyCD)
        {

            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            if (Method == "0")  //无来源
            {
                sql.AppendLine("SELECT  b.[ID] as ProID,b.UnitID as UnitID,a.CodeName as UnitName, b.ProductName as ProName, b.ProdNo  FROM  officedba.CodeUnitType as a right JOIN officedba.ProductInfo as b ON a.ID = b.UnitID where  a.BillStatus = 2 and b.CompanyCD=@CompanyCD and a.CompanyCD=@CompanyCD");
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            }
            else if (Method == "1")  // 采购类型
            {
                string[] str = QuaStr.Split('?');
                string ProductNo = str[0];
                string ProductName = str[1];
                sql.AppendLine("SELECT  a.ArriveNo as FromBillNo,c.ModifiedDate,a.ID as PurID, ISNULL(a.ProductCount,0) as ProductCount,ISNULL(a.ProductID,'') AS ProID,isnull(case a.FromType when '0' then '无来源' when '1' then '采购到货单' end,'') as FromTypeName,isnull(a.CompanyCD,'') as CompanyCD,isnull(a.SortNo,'') as SortNo,a.ProductID,ISNULL(a.UsedUnitID,0) AS UsedUnitID,ISNULL(a.UsedUnitCount,0) AS UsedUnitCount,ISNULL(CU.CodeName,'') AS UsedUnitName, ");
                sql.AppendLine("ISNULL(a.UnitID,'')as UnitID,ISNULL(a.ProductName,'') as ProductName,isnull(a.ProductNo,'') as ProductNo,isnull(b.CodeName,'') as CodeName,isnull(a.CheckedCount,0) as CheckedCount, a.ID as FromBillID, a.SortNo as FromLineNo,isnull(a.ApplyCheckCount,0) as ApplyCheckCount, isnull(a.Remark,'') as Remark,(isnull(a.ProductCount,0) - isnull(a.ApplyCheckCount,0)) AS QuaCheckedCount ");
                sql.AppendLine(" ,case a.FromType when '0' then '无来源' when '1' then '采购订单' else '' end as FromType");
                sql.AppendLine(@" FROM officedba.PurchaseArriveDetail AS a 
                                  LEFT JOIN officedba.CodeUnitType AS b ON a.UnitID = b.ID and a.CompanyCD=b.CompanyCD 
                                  LEFT JOIN officedba.CodeUnitType AS CU ON a.UsedUnitID = CU.ID and a.CompanyCD=CU.CompanyCD 
                                  LEFT JOIN officedba.PurchaseArrive as c on a.ArriveNo=c.ArriveNo and c.CompanyCD=a.CompanyCD ");
                sql.AppendLine("  where c.BillStatus=2 and c.CompanyCD=@CompanyCD  and isnull(a.ProductCount,0)>isnull(a.ApplyCheckCount,0) and a.ProductCount is not null");
                if (ProductNo != "")
                {
                    sql.AppendLine(" and a.ProductNo like @ProNo");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ProNo", "%" + ProductNo + "%"));
                }
                if (ProductName != "")
                {
                    sql.AppendLine(" and a.ProductName like @ProductName");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", "%" + ProductName + "%"));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            }
            else if (Method == "2")  //生产类型 
            {
                string[] str = QuaStr.Split('?');
                string ProductNo = str[0];
                string ProductName = str[1];
                sql.AppendLine("SELECT isnull(b.ProductName,'') as ProductName,isnull(a.ModifiedDate,'') as ModifiedDate,isnull(a.ProductedCount,0) as InCount, isnull(b.ProdNo,'') as ProdNo,isnull(a.TaskNo,'') as FromBillNo,isnull(a.ApplyCheckCount,0) as ApplyCheckCount,ISNULL(a.UsedUnitID,0) AS UsedUnitID,ISNULL(CU.CodeName,'') AS UsedUnitName,ISNULL(a.UsedUnitCount,0) AS UsedUnitCount, ");
                sql.AppendLine(" isnull(a.CheckedCount,0) as CheckedCount, isnull(c.CodeName,'') as CodeName, isnull(a.ID,'') as FromBillID, isnull(a.SortNo,'') as FromLineNo,");
                sql.AppendLine(" isnull(e.EmployeeName,'') as EmployeeName,(isnull(a.ProductedCount,0)-isnull(a.ApplyCheckCount,0)) as ManCheckCount ,isnull(f.DeptName,'') as DeptName,isnull(a.ID,0) as ManID,isnull(c.ID,0) AS UnitID, ");
                sql.AppendLine(" isnull(d.Principal,0) as Principal,isnull(d.DeptID,0) as DeptID,isnull(a.ProductID,0) as ProductID ");
                sql.AppendLine(" FROM officedba.ManufactureTaskDetail AS a LEFT  JOIN  officedba.ProductInfo AS b ON a.ProductID = b.ID and a.CompanyCD=b.CompanyCD ");
                sql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS c ON b.UnitID = c.ID and c.CompanyCD=b.CompanyCD  ");
                sql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS CU ON a.UsedUnitID = CU.ID and a.CompanyCD=CU.CompanyCD  ");
                sql.AppendLine(" LEFT JOIN officedba.ManufactureTask AS d ON a.TaskNo = d.TaskNo and d.CompanyCD=a.CompanyCD ");
                sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS e ON e.ID = d.Principal and e.CompanyCD=d.CompanyCD ");
                sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS f ON f.ID = d.DeptID ");
                sql.AppendLine(" where d.BillStatus = 2 and d.CompanyCD=@CompanyCD  and isnull(a.ProductedCount,0)>isnull(a.ApplyCheckCount,0) and a.ProductedCount is not null and a.CompanyCD=@CompanyCD");
                if (ProductNo != "")
                {
                    sql.AppendLine(" and b.ProdNo like @ProNo");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ProNo", "%" + ProductNo + "%"));
                }
                if (ProductName != "")
                {
                    sql.AppendLine(" and b.ProductName like @ProductName");
                    comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", "%" + ProductName + "%"));
                }
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));

            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetPurDetailList(string IDList, string CompanyCD)   //源单类型
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  a.ID as PurID,isnull(a.ApplyCheckCount,0) as ApplyCheckCount,isnull(a.ProductCount,0) as ProductCount,a.ProductID AS ProID,a.CompanyCD,a.SortNo,a.ProductID, a.UnitID,isnull(a.ProductName,'') as ProductName,ISNULL(a.UsedUnitID,0) AS UsedUnitID,ISNULL(a.UsedUnitCount,0) AS UsedUnitCount,");
            sql.AppendLine("         isnull(a.ProductNo,'') as ProductNo,isnull(b.CodeName,'') as CodeName,a.ID as FromBillID,a.ArriveNo as FromBillNo, a.SortNo as FromLineNo,isnull(a.CheckedCount,0) as CheckedCount,isnull(a.Remark,'') as Remark,isnull(a.ProductCount,0) -isnull(a.ApplyCheckCount,0) AS QuaCheckedCount ");
            sql.AppendLine("        FROM officedba.PurchaseArriveDetail AS a LEFT  JOIN officedba.CodeUnitType AS b ON a.UnitID = b.ID  ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD.Trim() + "'");
            sql.AppendLine(" and a.[ID] in ( ");
            string[] myIDList = IDList.Split(',');
            for (int i = 0; i < myIDList.Length - 1; i++)
            {

                if (i > 0)
                {
                    sql.AppendLine(",'" + myIDList[i].ToString() + "' ");
                }
                else
                {
                    sql.AppendLine("'" + myIDList[i].ToString() + "' ");
                }
            }
            sql.AppendLine(")");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        public static DataTable GetManDetailList(string IDList, string CompanyCD)   //源单类型 为生产任务单 填充行
        {
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" select a.ID as ManID,a.TaskNo as FromBillNo,a.ID as FromBillID,isnull(a.ApplyCheckCount,0) as ApplyCheckCount,a.ProductID,isnull(a.CheckedCount,0) as CheckedCount,isnull(a.FromBillNo,'') as FromBillNo,isnull(a.SortNo,0) as FromLineNo,isnull(a.Remark,'') as Remark,isnull(P.ProductName,'') as ProductName,isnull(p.ProdNo,'') as ProNo,isnull(c.CodeName,'') as CodeName,p.UnitID ");
            sql.AppendLine(" from officedba.ManufactureTaskDetail as a left join officedba.ProductInfo as p on a.ProductID=p.ID                                           ");
            sql.AppendLine("      left join officedba.CodeUnitType as c on p.UnitID=c.ID                                                                                  ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD.Trim() + "'");
            sql.AppendLine(" and a.[ID] in ( ");
            string[] myIDList = IDList.Split(',');
            for (int i = 0; i < myIDList.Length - 1; i++)
            {
                sql.AppendLine("'" + myIDList[i].ToString() + "', ");
            }
            string mysql = "";
            mysql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            mysql += " ) ";
            return SqlHelper.ExecuteSql(mysql);
        }

        public static DataTable GetEmployeeForQuality()  //获取报检人员与部门
        {
            string sql = "SELECT  a.Sex,b.DeptID,a.ID as EmployeeID,a.EmployeeName,c.DeptName FROM  officedba.EmployeeInfo AS a LEFT OUTER JOIN  officedba.DeptQuarter AS b ON a.QuarterID = b.ID INNER JOIN  officedba.DeptInfo AS c ON b.DeptID = c.ID";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 添加质检
        /// </summary>
        /// <param name="model">质检申请单</param>
        /// <param name="detailList">明细信息</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool AddStoryQualityCheckDB(StorageQualityCheckApplay model, List<StorageQualityCheckApplyDetail> detailList, Hashtable htExtAttr)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//[待修改]
            ArrayList sqllist = new ArrayList();
            if (string.IsNullOrEmpty(model.ApplyNO))
            {
                return false;
            }
            bool result = false;

            #region 质检
            SqlCommand sqlcomm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("Insert into officedba.QualityCheckApplay");
            sql.AppendLine("             (CompanyCD     ");
            sql.AppendLine("              ,ApplyNo      ");
            sql.AppendLine("              ,Title      ");
            sql.AppendLine("              ,FromType   ");
            sql.AppendLine("              ,CheckDeptId        ");
            sql.AppendLine("              ,CheckType     ");
            sql.AppendLine("              ,CheckMode     ");
            sql.AppendLine("              ,Checker        ");
            sql.AppendLine("              ,CheckDate      ");
            sql.AppendLine("              ,Creator        ");
            sql.AppendLine("              ,CreateDate     ");
            sql.AppendLine("              ,BillStatus     ");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("           ,Remark");
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql.AppendLine("           ,Attachment");
            }
            if (model.CustID != 0)
            {
                sql.AppendLine("           ,CustID ");
                sql.AppendLine("           ,CustName");
            }
            if (model.CustBigType != "0")
            {
                sql.AppendLine("           ,CustBigType  ");
            }
            if (model.Principal != 0)
            {
                sql.AppendLine("           ,Principal");
            }
            if (model.CountTotal != -1)
            {
                sql.AppendLine("              ,CountTotal     ");
            }
            if (model.DeptID != 0)
            {
                sql.AppendLine("             ,DeptID");
            }
            sql.AppendLine("               ,ModifiedDate");
            sql.AppendLine("               ,ModifiedUserID)");
            sql.AppendLine("     values   (@CompanyCD     ");
            sql.AppendLine("              ,@ApplyNo      ");
            sql.AppendLine("              ,@Title      ");
            sql.AppendLine("              ,@FromType   ");
            sql.AppendLine("              ,@CheckDeptId        ");
            sql.AppendLine("              ,@CheckType     ");
            sql.AppendLine("              ,@CheckMode     ");
            sql.AppendLine("              ,@Checker        ");
            sql.AppendLine("              ,@CheckDate      ");
            sql.AppendLine("              ,@Creator        ");
            sql.AppendLine("              ,@CreateDate     ");
            sql.AppendLine("              ,@BillStatus     ");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("          ,@Remark         ");
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql.AppendLine("          ,@Attachment     ");
            }
            if (model.CustID != 0)
            {
                sql.AppendLine("         ,@CustID     ");
                sql.AppendLine("         ,@CustName   ");
            }
            if (model.CustBigType != "0")
            {
                sql.AppendLine("        ,@CustBigType   ");
            }
            if (model.Principal != 0)
            {
                sql.AppendLine("        ,@Principal        ");
            }
            if (model.CountTotal != -1)
            {
                sql.AppendLine("              ,@CountTotal     ");
            }
            if (model.DeptID != 0)
            {
                sql.AppendLine("           ,@DeptID");
            }
            sql.AppendLine("              ,@ModifiedDate   ");
            sql.AppendLine("              ,@ModifiedUserID)");
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptId", model.CheckDeptID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));
            if (model.CheckDate != Convert.ToDateTime("9999-09-09"))
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate));
            }
            else
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", System.Data.SqlTypes.SqlDateTime.Null));
            }
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creater));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", DateTime.Now.ToShortDateString()));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            }
            if (model.CustID != 0)
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));
            }
            if (model.CustBigType != "0")
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustBigType", model.CustBigType));
            }
            if (model.Principal != 0)
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            }

            if (model.CountTotal != -1)
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("CountTotal", model.CountTotal));
            }
            if (model.DeptID != 0)
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            }
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToShortDateString()));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));

            sqlcomm.CommandText = sql.ToString();
            sqllist.Add(sqlcomm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqllist.Add(commExtAttr);
            }
            #endregion

            #region 明细
            for (int i = 0; i < detailList.Count; i++)
            {
                StringBuilder cmdsql = new StringBuilder();
                cmdsql.AppendLine("Insert into  officedba.QualityCheckApplyDetail");
                cmdsql.AppendLine("(CompanyCD,ApplyNo,SortNo,ProductID,UnitID,ProductCount");
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    cmdsql.AppendLine(",Remark");
                }
                cmdsql.AppendLine(",FromType");
                if (detailList[i].FromBillID != 0)
                {
                    cmdsql.AppendLine(",FromBillID");
                }
                cmdsql.AppendLine(",FromLineNo,CheckedCount");
                cmdsql.AppendLine(",UsedUnitID,UsedUnitCount,ExRate)");
                cmdsql.AppendLine("values (@CompanyCD,@ApplyNO,@SortNo");
                cmdsql.AppendLine(",@ProductID ,@UnitID,@ProductCount");
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    cmdsql.AppendLine(",@Remark");
                }
                cmdsql.AppendLine(",@FromType");
                if (detailList[i].FromBillID != 0)
                {
                    cmdsql.AppendLine(",@FromBillID");
                }
                cmdsql.AppendLine(",@FromLineNo,@CheckedCount");
                cmdsql.AppendLine(",@UsedUnitID,@UsedUnitCount,@ExRate)");
                SqlCommand detailcomm = new SqlCommand();
                detailcomm.CommandText = cmdsql.ToString();
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNO", model.ApplyNO));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detailList[i].SortNo));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detailList[i].ProductID));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detailList[i].UnitID));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", detailList[i].ProductCount));
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detailList[i].Remark));
                }
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                if (detailList[i].FromBillID != 0)
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detailList[i].FromBillID));
                }
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", detailList[i].FromLineNo));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", detailList[i].CheckedCount));
                if (detailList[i].UsedUnitID.HasValue)
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitID", detailList[i].UsedUnitID.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitID", DBNull.Value));
                }
                if (detailList[i].UsedUnitCount.HasValue)
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitCount", detailList[i].UsedUnitCount.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitCount", DBNull.Value));
                }

                if (detailList[i].ExRate.HasValue)
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", detailList[i].ExRate.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
                }
                detailcomm.CommandText = cmdsql.ToString();
                sqllist.Add(detailcomm);
            }
            #endregion

            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(sqllist))
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 质检更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="detailList"></param>
        /// <param name="ProductID"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool UpdateStorageCheck(StorageQualityCheckApplay model, List<StorageQualityCheckApplyDetail> detailList, string[] ProductID, Hashtable htExtAttr)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//[待修改]
            ArrayList sqllist = new ArrayList();

            #region 质检
            SqlCommand sqlcomm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.QualityCheckApplay");
            sql.AppendLine("             set CompanyCD=@CompanyCD     ");
            sql.AppendLine("              ,ApplyNo=@ApplyNo      ");
            sql.AppendLine("              ,Title=@Title      ");
            sql.AppendLine("              ,FromType=@FromType   ");
            sql.AppendLine("              ,CheckDeptId=@CheckDeptId        ");
            sql.AppendLine("              ,CheckType=@CheckType     ");
            sql.AppendLine("              ,CheckMode=@CheckMode     ");
            sql.AppendLine("              ,Checker=@Checker        ");

            sql.AppendLine("              ,CheckDate=@CheckDate      ");


            sql.AppendLine("              ,Creator=@Creator        ");
            sql.AppendLine("              ,CreateDate=@CreateDate     ");
            sql.AppendLine("              ,BillStatus=@BillStatus     ");

            sql.AppendLine("           ,Remark=@Remark");



            sql.AppendLine("           ,Attachment=@Attachment");


            sql.AppendLine("           ,CustID=@CustID ");
            sql.AppendLine("           ,CustName=@CustName");


            sql.AppendLine("           ,CustBigType=@CustBigType  ");

            sql.AppendLine("           ,Principal=@Principal");


            sql.AppendLine("              ,CountTotal=@CountTotal     ");


            sql.AppendLine("               ,DeptID=@DeptID");

            sql.AppendLine("               ,ModifiedDate=@ModifiedDate");
            sql.AppendLine("               ,ModifiedUserID=@ModifiedUserID");
            sql.AppendLine("  where ID=@ID");
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptId", model.CheckDeptID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));
            if (model.CheckDate != Convert.ToDateTime("9999-09-09"))
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate));
            }
            else
            {
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", System.Data.SqlTypes.SqlDateTime.Null));
            } sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creater));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", DateTime.Now.ToShortDateString()));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));



            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));


            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustName", model.CustName));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CustBigType", model.CustBigType));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));

            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToShortDateString()));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));

            sqlcomm.CommandText = sql.ToString();
            sqllist.Add(sqlcomm);
            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqllist.Add(commExtAttr);
            }
            #endregion

            #region 明细



            string delsql = "delete officedba.QualityCheckApplyDetail where CompanyCD=@CompanyCD and ApplyNo=@ApplyNo ";
            SqlCommand delcomm = new SqlCommand();
            delcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            delcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
            delcomm.CommandText = delsql;
            sqllist.Add(delcomm);

            for (int i = 0; i < detailList.Count; i++)
            {
                StringBuilder cmdsql = new StringBuilder();
                cmdsql.AppendLine("Insert into  officedba.QualityCheckApplyDetail");
                cmdsql.AppendLine("(CompanyCD,ApplyNo,SortNo,ProductID,UnitID,ProductCount");
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    cmdsql.AppendLine(",Remark");
                }
                if (detailList[i].RealCheckedCount != 0)
                {
                    cmdsql.AppendLine(",RealCheckedCount");
                }
                cmdsql.AppendLine(",FromType,FromBillID,FromLineNo,CheckedCount");
                cmdsql.AppendLine(",UsedUnitID,UsedUnitCount,ExRate)");
                cmdsql.AppendLine("values (@CompanyCD,@ApplyNO,@SortNo");
                cmdsql.AppendLine(",@ProductID ,@UnitID,@ProductCount");
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    cmdsql.AppendLine(",@Remark");
                }
                cmdsql.AppendLine(",@FromType,@FromBillID");
                cmdsql.AppendLine(",@FromLineNo,@CheckedCount");
                cmdsql.AppendLine(",@UsedUnitID,@UsedUnitCount,@ExRate)");
                SqlCommand detailcomm = new SqlCommand();
                detailcomm.CommandText = cmdsql.ToString();
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNO", model.ApplyNO));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detailList[i].SortNo));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detailList[i].ProductID));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detailList[i].UnitID));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", detailList[i].ProductCount));
                if (!string.IsNullOrEmpty(detailList[i].Remark))
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detailList[i].Remark));
                }
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", detailList[i].FromBillID));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", detailList[i].FromLineNo));
                detailcomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", detailList[i].CheckedCount));
                if (detailList[i].UsedUnitID.HasValue)
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitID", detailList[i].UsedUnitID.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitID", DBNull.Value));
                }
                if (detailList[i].UsedUnitCount.HasValue)
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitCount", detailList[i].UsedUnitCount.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(new SqlParameter("@UsedUnitCount", DBNull.Value));
                }
                if (detailList[i].ExRate.HasValue)
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", detailList[i].ExRate.Value));
                }
                else
                {
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", DBNull.Value));
                }
                detailcomm.CommandText = cmdsql.ToString();
                sqllist.Add(detailcomm);

            }
            #endregion

            try
            {
                return SqlHelper.ExecuteTransWithArrayList(sqllist);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 扩展属性更新命令
        /// </summary>
        /// <param name="model">质检申请单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(StorageQualityCheckApplay model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.QualityCheckApplay SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND ApplyNO = @ApplyNO ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyNO", model.ApplyNO));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }

        /// <summary>
        /// 确认质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ConfirmBill(StorageQualityCheckApplay model)
        {
            ArrayList sqllist = new ArrayList();
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(" UPDATE officedba.QualityCheckApplay SET");
            sql.AppendLine(" Confirmor          = @Confirmor,");
            sql.AppendLine(" ConfirmDate      = @ConfirmDate,");
            sql.AppendLine(" BillStatus              = 2,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = @ModifiedDate");
            sql.AppendLine(" Where  CompanyCD=@CompanyCD and ID=@ID");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.MdodifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD.Trim()));
            comm.CommandText = sql.ToString();
            sqllist.Add(comm);
            SqlCommand DetailComm = new SqlCommand();
            string DetailSql = "update officedba.QualityCheckApplyDetail set CheckedCount=isnull(CheckedCount,0)+ProductCount where ApplyNo=@ApplyNo and CompanyCD=@CompanyCD";
            DetailComm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
            DetailComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            DetailComm.CommandText = DetailSql;
            sqllist.Add(DetailComm);

            return SqlHelper.ExecuteTransWithArrayList(sqllist);
        }

        /// <summary>
        /// 获取质检列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetQualityCheckList(StorageQualityCheckApplay model, DateTime EndCheckDate, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITYADD);
            sql.AppendLine(" select * from ( ");
            sql.AppendLine("SELECT     a.ID,a.ModifiedDate,isnull(a.BillStatus,'') as BillStatusID,isnull(e.FlowStatus,'0') as FlowStatusID,a.ApplyNo,isnull(a.Title,'') as Title,isnull(a.CustName,'') as CustName,isnull(b.EmployeeName,'') as EmployeeName,isnull(c.DeptName,'') as DeptName,isnull(substring(CONVERT(varchar,a.CheckDate,120),0,11),'')  as CheckDate,isnull(a.FromType,'')  as FromTypeID, ");
            sql.AppendLine("               isnull(CASE a.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '采购到货单' WHEN '2' THEN '生产任务单' END,'')  AS FromType, ");
            sql.AppendLine("               isnull(CASE a.CheckType WHEN '1' THEN '进货检验' WHEN '2' THEN '过程检验' WHEN '3' THEN '最终检验' END,'')  AS CheckType,");
            sql.AppendLine("               isnull(CASE a.CheckMode WHEN '1' THEN '全检' WHEN '2' THEN '抽检' WHEN '3' THEN '临检' END,'')  AS CheckMode, ");
            sql.AppendLine("               isnull(CASE a.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END,'') AS BillStatus,");
            sql.AppendLine("               isnull(CASE e.FlowStatus WHEN '1' THEN '待审批' WHEN '2' THEN '审批中' WHEN '3' THEN '审批通过' WHEN '4' THEN '审批不通过' when '5' then '撤消审批' else  '' END,'') AS FlowStatus, ");
            sql.AppendLine("               isnull(CASE a.CustBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商' WHEN '3' THEN '竞争对手' WHEN '4' THEN '银行' WHEN '5' THEN '外协加工厂' WHEN  '6' THEN '运输商' WHEN '7' THEN '其他' END,'') AS CustBigType ");
            sql.AppendLine("FROM         officedba.QualityCheckApplay AS a left JOIN");
            sql.AppendLine("                      officedba.EmployeeInfo AS b ON a.Checker = b.ID AND a.CompanyCD = b.CompanyCD LEFT  JOIN");
            sql.AppendLine("                      officedba.DeptInfo AS c ON a.CheckDeptId = c.ID AND a.CompanyCD = c.CompanyCD LEFT  JOIN");
            sql.AppendLine("                      officedba.CustInfo AS d ON a.CustID = d.ID AND a.CompanyCD = d.CompanyCD LEFT  JOIN");
            sql.AppendLine("                      officedba.FlowInstance AS e ON a.CompanyCD = e.CompanyCD AND e.BillTypeFlag =" + BillTypeFlag + " AND e.BillTypeCode = " + BillTypeCode + " AND a.ID = e.BillID");
            sql.AppendLine("   and e.ID=(select max(ID) from officedba.FlowInstance where a.CompanyCD = officedba.FlowInstance.CompanyCD AND officedba.FlowInstance.BillTypeFlag = " + BillTypeFlag + " AND officedba.FlowInstance.BillTypeCode = " + BillTypeCode + " AND  a.ID = officedba.FlowInstance.BillID) ");
            sql.AppendLine("WHERE   1=1 ");
            SqlCommand comm = new SqlCommand();

            if (model.FromType != "00" && model.FromType != null)
            {
                sql.AppendLine(" and a.FromType=@FromType");
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            }
            if (model.ApplyNO != null && model.ApplyNO != "")
            {
                sql.AppendLine(" and a.ApplyNo like @ApplyNO");
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyNO", "%" + model.ApplyNO + "%"));
            }
            if (model.Title != null && model.Title != "")
            {
                sql.AppendLine(" and a.Title like @Title ");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));
            }
            if (model.Checker != "0" && model.Checker != "" && model.Checker != null)
            {
                sql.AppendLine(" and a.Checker=@Checker");
                comm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));
            }
            if (!string.IsNullOrEmpty(model.CustName) && model.CustName != "0")
            {
                sql.AppendLine(" and a.CustID = @CustID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustName));
            }
            if (model.CheckDeptID != "0" && model.CheckDeptID != "" && model.CheckDeptID != null)
            {
                sql.AppendLine(" and a.CheckDeptId=@CheckDeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptID", model.CheckDeptID));
            }
            if (model.CheckDate != Convert.ToDateTime("1800-1-1"))  //起始日期
            {
                sql.AppendLine(" and a.CheckDate >=@CheckDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate));
            }
            if (EndCheckDate != Convert.ToDateTime("9999-2-3")) //终止日期
            {
                sql.AppendLine(" and a.CheckDate <= @EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndCheckDate));
            }
            if (model.CheckType != "00" && model.CheckType != null)
            {
                sql.AppendLine(" and a.CheckType=@CheckType ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            }
            if (model.CheckMode != "00" && model.CheckMode != null)
            {
                sql.AppendLine(" and a.CheckMode=@CheckMode ");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + EFIndex + " LIKE @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            if (model.BillStatus != "00" && model.BillStatus != null)
            {
                sql.AppendLine(" and a.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            }
            if (FlowStatus != null && FlowStatus != "00" && FlowStatus != "6")
            {
                sql.AppendLine(" and e.FlowStatus=@FlowStatus");
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus", FlowStatus));
            }
            sql.AppendLine(" and a.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

            sql.AppendLine(" ) as Info ");
            if (FlowStatus == "6")
            {
                sql.AppendLine(" where FlowStatusID='0'");

            }
            if (model.Creater == -100)//用这个属性来存储排序信息
            {
                sql.AppendLine(" order by " + model.Attachment);
            }

            comm.CommandText = sql.ToString();
            DataTable dt = new DataTable();
            if (model.Creater == -100)
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                dt = SqlHelper.PagerWithCommand(comm, model.Creater, model.Confirmor, model.Attachment, ref TotalCount);
            }
            return dt;

        }

        public static DataTable GetOneQualityDB(int ID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT     isnull(Replace(a.[Attachment],'\\',','),'') as Attachment,isnull(a.Remark,'') as Remark,a.BillStatus,a.Title,a.ID,a.ApplyNo, isnull(a.CustName,'') as CustName,isnull(a.CustID,0) as CustID, a.FromType,isnull(a.CustBigType,'0') as CustBigType, a.CheckType, isnull(a.Checker,0) AS CheckerID,isnull(a.Principal,0) as Principal,  ");
            sql.AppendLine("    CASE a.CustBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商'  WHEN '5' THEN '外协加工厂'  WHEN '7' THEN '其他' else '' END AS CustBigTypeName, isnull(a.CustBigType,'0') AS CustBigTypeID, ");
            sql.AppendLine("    case a.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '4' then '自动结单' else '' end as BillStatusName, ");
            sql.AppendLine("    a.ModifiedUserID AS ModifiedName, ");
            sql.AppendLine("    isnull(e1.EmployeeName,'') AS CheckerName, ");
            sql.AppendLine("    isnull(e2.EmployeeName,'') AS CreatorName, ");
            sql.AppendLine("    isnull(e3.EmployeeName,'') AS CloserName, ");
            sql.AppendLine("    isnull(e4.EmployeeName,'') AS ConfirmorName, ");
            sql.AppendLine("    isnull(e5.EmployeeName,'') AS PrincipalName,");
            sql.AppendLine("    isnull(a.DeptID,0) as DeptID,a.CheckMode,ISNULL(SUBSTRING(CONVERT(VARCHAR,a.CheckDate,120),0,11),'') AS CheckDate , ");
            sql.AppendLine("    isnull(c.DeptName,'')as DeptName,isnull(a.CheckDeptId,0) as CheckDeptId,isnull(a.CountTotal,0) as CountTotal, a.Closer,ISNULL(SUBSTRING(CONVERT(VARCHAR,a.CloseDate,120),0,11),'') as CloseDate,a.Confirmor, ISNULL(SUBSTRING(CONVERT(VARCHAR,a.ConfirmDate,120),0,11),'') as ConfirmDate, a.ModifiedUserID,ISNULL(SUBSTRING(CONVERT(VARCHAR,a.ModifiedDate,120),0,11),'') as ModifiedDate, a.Creator,ISNULL(SUBSTRING(CONVERT(VARCHAR,a.CreateDate,120),0,11),'') as CreateDate, ");
            sql.AppendLine("    isnull(d1.DeptName,'') as CheckDeptName,");
            sql.AppendLine("    a.ExtField1,a.ExtField2,a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10");
            sql.AppendLine("    FROM         officedba.QualityCheckApplay AS a LEFT  JOIN officedba.DeptInfo AS c ON a.DeptId = c.ID  ");
            sql.AppendLine("    left join  officedba.DeptInfo as d1 on d1.ID=a.CheckDeptId ");
            sql.AppendLine("    left join  officedba.EmployeeInfo as e1 on e1.ID=a.Checker ");
            sql.AppendLine("    left join  officedba.EmployeeInfo as e2 on e2.ID=a.Creator ");
            sql.AppendLine("    left join  officedba.EmployeeInfo as e3 on e3.ID=a.Closer ");
            sql.AppendLine("    left join  officedba.EmployeeInfo as e4 on e4.ID=a.Confirmor ");
            sql.AppendLine("    left join  officedba.EmployeeInfo as e5 on e5.ID=a.Principal ");
            sql.AppendLine("    WHERE     a.ID =@ID and a.CompanyCD=@CompanyCD  ");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetQualityDetail(int ID, string CompanyCD)   //修改时候带出源单类型   
        {
            string Method = "";
            StringBuilder sql = new StringBuilder();
            string ApplyNo = "";
            string mySql = " select ApplyNo,FromType from officedba.QualityCheckApplay where ID=@ID";
            SqlCommand myComm = new SqlCommand();
            myComm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            myComm.CommandText = mySql;
            DataTable myDt = SqlHelper.ExecuteSearch(myComm);
            if (myDt.Rows.Count > 0)
            {
                Method = myDt.Rows[0]["FromType"].ToString();
                ApplyNo = myDt.Rows[0]["ApplyNo"].ToString();

                if (Method == "1")//采购   
                {
                    sql.Append(@"SELECT a.SortNo,b.ArriveNo as FromBillNo,isnull(b.CheckedCount,0) as CheckedCount,isnull(a.RealCheckedCount,0) as RealCheckedCount
                                        ,a.FromType,isnull(c.ProductName,'') as ProductName, a.ProductID as ProID
                                        ,isnull(a.ProductCount,0) as ProductCount,isnull(a.Remark,'') as Remark
                                        , c.ProdNo, a.UnitID,isnull(d.CodeName,'') as CodeName,isnull(b.ProductCount,0) AS ProductCount2, a.FromBillID,a.UsedUnitID,cut.CodeName as UsedUnitName,a.UsedUnitCount
                                        ,a.FromLineNo,isnull(b.ApplyCheckCount,0) as ApplyCheckCount, a.ID 
                                        FROM  officedba.QualityCheckApplyDetail AS a 
                                        LEFT  JOIN officedba.CodeUnitType AS d ON a.UnitID = d.ID 
                                        LEFT  JOIN officedba.CodeUnitType AS cut ON a.UsedUnitID = cut.ID 
                                        LEFT  JOIN officedba.PurchaseArriveDetail AS b ON  b.ID=a.FromBillID
                                        LEFT  JOIN officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     (a.ApplyNo ='" + ApplyNo + "') AND (a.CompanyCD = '" + CompanyCD + "') and b.CompanyCD='" + CompanyCD + "'");
                }
                else if (Method == "2")  //生产任务
                {
                    sql.AppendLine("SELECT    isnull(d.ProductedCount,0) as InCount,isnull(a.RealCheckedCount,0) as RealCheckedCount,isnull(d.CheckedCount,0) as CheckedCount, a.FromType,a.ID,c.ProductName,isnull(c.ProdNo,'') as ProdNo, a.ProductID, a.UnitID,isnull(b.CodeName,'') as CodeName,isnull(a.ProductCount,0) as ProductCount, a.FromBillID, a.FromLineNo,isnull(d.ApplyCheckCount,0) as ApplyCheckCount, a.SortNo,a.UsedUnitID,cut.CodeName as UsedUnitName,a.UsedUnitCount, ");
                    sql.AppendLine("           isnull(a.Remark,'') as Remark,d.TaskNo as FromBillNo FROM  officedba.QualityCheckApplyDetail AS a LEFT  JOIN   officedba.ManufactureTaskDetail AS d ON a.FromBillID = d.ID LEFT JOIN ");
                    sql.AppendLine("            officedba.CodeUnitType AS b ON a.UnitID = b.ID LEFT  JOIN officedba.CodeUnitType AS cut ON a.UsedUnitID = cut.ID LEFT  JOIN  officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     (a.ApplyNo = '" + ApplyNo + "') AND (a.CompanyCD = '" + CompanyCD + "') and d.CompanyCD='" + CompanyCD + "' ");
                }
                else  //无来源时
                {
                    sql.AppendLine("SELECT     a.ID,isnull(c.ProductName,'') as ProductName,isnull(a.RealCheckedCount,0) as RealCheckedCount,isnull(c.ProdNo,'') as ProdNo,isnull(a.CheckedCount,0) as CheckedCount,  a.ProductID, a.UnitID,isnull(b.CodeName,'') as CodeName,isnull(a.ProductCount,0) as ProductCount, a.FromBillID, a.FromLineNo, a.SortNo,isnull(a.Remark,'') as Remark,a.UsedUnitID,cut.CodeName as UsedUnitName,a.UsedUnitCount, ");
                    sql.AppendLine("            a.FromType FROM         officedba.QualityCheckApplyDetail AS a LEFT  JOIN   officedba.CodeUnitType AS b ON a.UnitID = b.ID LEFT  JOIN officedba.CodeUnitType AS cut ON a.UsedUnitID = cut.ID LEFT  JOIN ");
                    sql.AppendLine("            officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     (a.ApplyNo = '" + ApplyNo + "') AND (a.CompanyCD = '" + CompanyCD + "') ");
                }
            }
            return SqlHelper.ExecuteSql(sql.ToString());
        }


        public static bool DeleteQualityApply(string ID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改]
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    if (arrID[i] != "")
                    {
                        StringBuilder sqlDet = new StringBuilder();
                        StringBuilder sqlBom = new StringBuilder();
                        sqlDet.AppendLine("delete from officedba.QualityCheckApplyDetail where CompanyCD=@CompanyCD and ApplyNo=(select top 1 ApplyNo from officedba.QualityCheckApplay where CompanyCD=@CompanyCD and ID=@ID)");
                        sqlBom.AppendLine("delete from officedba.QualityCheckApplay where CompanyCD=@CompanyCD and ID=@ID");

                        SqlCommand commDet = new SqlCommand();
                        commDet.CommandText = sqlDet.ToString();
                        commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                        listADD.Add(commDet);

                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = sqlBom.ToString();
                        comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                        listADD.Add(comm);
                    }
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }

        /// <summary>
        /// 确认时回写采购 生产
        /// </summary>
        /// <param name="CheckedCount"></param>
        /// <param name="ID"></param>
        /// <param name="FromType"></param>
        /// <returns></returns>
        public static bool UpdatePurDetail(string CheckedCount, string ID, string FromType)
        {
            string sql = "";
            ArrayList sqllist = new ArrayList();

            if (FromType == "1")   //采购
            {


                string[] myID = ID.Split(',');
                string[] myCount = CheckedCount.Split(',');
                for (int i = 0; i < myID.Length; i++)
                {
                    SqlCommand comm = new SqlCommand();
                    sql = "update officedba.PurchaseArriveDetail set ApplyCheckCount=isnull(ApplyCheckCount,0) +@ApplyCheckCount where ID=@ID";
                    comm.CommandText = sql;
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i].ToString()));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", myCount[i].ToString()));
                    sqllist.Add(comm);
                }
            }
            else if (FromType == "2")// 生产任务单
            {
                string[] myID = ID.Split(',');
                string[] myCount = CheckedCount.Split(',');
                for (int i = 0; i < myID.Length; i++)
                {
                    SqlCommand detailcomm = new SqlCommand();
                    sql = " update officedba.ManufactureTaskDetail set ApplyCheckCount=isnull(ApplyCheckCount,0)+" + myCount[i].ToString() + " where ID=" + myID[i].ToString() + "";
                    detailcomm.CommandText = sql;
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i].ToString()));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", myCount[i].ToString()));
                    sqllist.Add(detailcomm);
                }
            }

            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CheckedCount">明细报检数量</param>
        /// <param name="ID">源单ID</param>
        /// <param name="FromType">源单类型</param>
        /// <param name="ModifiedUserID">最后更新人</param>
        /// <param name="CompanyCD"></param>
        /// <param name="QuaID">质检主表ID</param>
        /// <param name="ApplyNo">质检编号</param>
        /// <returns></returns>
        public static bool UpPurDetail(string CheckedCount, string ID, string FromType, string ModifiedUserID, string CompanyCD, StorageQualityCheckApplay model)
        {

            string sql = "";
            bool returnvalue = false;
            ArrayList sqllist = new ArrayList();
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITYADD);
            // 通过明细ID获取主表的编号
            SqlCommand PurComm = new SqlCommand();
            string PurSql = "select BillStatus from officedba.QualityCheckApplay  where ApplyNo=@ApplyNo and CompanyCD=@CompanyCD";
            PurComm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
            PurComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            PurComm.CommandText = PurSql;
            DataTable PurDt = SqlHelper.ExecuteSearch(PurComm);
            string BillStatus = "0";
            if (PurDt.Rows.Count > 0)
            {
                BillStatus = PurDt.Rows[0]["BillStatus"].ToString();
            }
            if (BillStatus == "2")
            {
                if (FromType == "1")   //采购
                {
                    string[] myID = ID.Split(',');
                    string[] myCount = CheckedCount.Split(',');
                    for (int i = 0; i < myID.Length; i++)
                    {

                        if (BillStatus == "2")
                        {
                            SqlCommand comm = new SqlCommand();
                            sql = "update officedba.PurchaseArriveDetail set ApplyCheckCount=isnull(ApplyCheckCount,0)-@ApplyCheckCount where ID=@ID";
                            comm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i].ToString()));
                            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", myCount[i].ToString()));
                            comm.CommandText = sql;
                            sqllist.Add(comm);
                        }
                    }
                }
                else if (FromType == "2")// 生产任务单
                {
                    string[] myID = ID.Split(',');
                    string[] myCount = CheckedCount.Split(',');
                    for (int i = 0; i < myID.Length; i++)
                    {

                        SqlCommand detailcomm = new SqlCommand();
                        sql = " update officedba.ManufactureTaskDetail set ApplyCheckCount=isnull(ApplyCheckCount,0)-@ApplyCheckCount  where ID=@ID";
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i].ToString()));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", myCount[i].ToString()));
                        detailcomm.CommandText = sql;
                        sqllist.Add(detailcomm);

                    }
                }
                #region 撤消审批流程
                DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
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
                    commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
                    sqllist.Add(commHis);
                    #endregion

                    #region 更新流程任务处理表
                    StringBuilder sqlTask = new StringBuilder();
                    sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                    sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                    SqlCommand commTask = new SqlCommand();
                    commTask.CommandText = sqlTask.ToString();
                    commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
                    sqllist.Add(commTask);
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
                    commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
                    sqllist.Add(commIns);
                    #endregion

                }
                #endregion

                #region  更新质检主表
                SqlCommand QuaComm = new SqlCommand();
                string QuaSql = "update officedba.QualityCheckApplay set BillStatus='1',ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate";
                QuaSql += " where ApplyNo=@ApplyNo and CompanyCD=@CompanyCD";
                QuaComm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
                QuaComm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
                QuaComm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
                QuaComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                QuaComm.CommandText = QuaSql;
                sqllist.Add(QuaComm);
                #endregion

                #region 更新质检明细 已检数量
                SqlCommand QuaDetailComm = new SqlCommand();
                string QuaDetailSql = "update officedba.QualityCheckApplyDetail set CheckedCount=isnull(CheckedCount,0)-isnull(ProductCount,0)";
                QuaDetailSql += "  where ApplyNo=@ApplyNo and CompanyCD=@CompanyCD";
                QuaDetailComm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                QuaDetailComm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", model.ApplyNO));
                QuaDetailComm.CommandText = QuaDetailSql;
                sqllist.Add(QuaDetailComm);
                #endregion
            }
            else
            {
                returnvalue = false;
            }
            if (BillStatus == "2")
            {
                if (SqlHelper.ExecuteTransWithArrayList(sqllist))
                {
                    returnvalue = true;
                }
                else
                {
                    returnvalue = false;
                }
            }
            return returnvalue;
        }
        public static bool CloseBill(StorageQualityCheckApplay model, string method)
        {
            ArrayList listsql = new ArrayList();
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.QualityCheckApplay SET");
            sql.AppendLine(" BillStatus              = @BillStatus,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");

            if (method == "0")
            {
                sql.AppendLine(" Closer= @Closer,");
                sql.AppendLine(" CloseDate = @CloseDate, ");
            }
            sql.AppendLine(" ModifiedDate= @ModifiedDate");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.MdodifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));

            string BillStatus = "2";
            if (method == "0") //结单时
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
                BillStatus = "4";
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", BillStatus));
            comm.CommandText = sql.ToString();
            listsql.Add(comm);
            if (SqlHelper.ExecuteTransWithArrayList(listsql))
            { return true; }
            else
            { return false; }

        }

        /// <summary>
        /// 修改质检申请单时判断是否被引用
        /// </summary>
        /// <param name="FromReportNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static string IsTransfer(int ID, string CompanyCD)
        {
            string myResult = "0";
            string sql = "select ApplyNo from officedba.QualityCheckApplay where ID=" + ID + "";
            DataTable dt1 = SqlHelper.ExecuteSql(sql);
            string FromReportNo = "";
            if (dt1.Rows.Count > 0)
            {
                FromReportNo = dt1.Rows[0]["ApplyNo"].ToString();


                int Rows = 0;
                string sql1 = "select Count(ID) as Rows from officedba.QualityCheckReport  where FromReportNo='" + FromReportNo + "' and FromType='1' and CompanyCD='" + CompanyCD + "'";
                DataTable dt = SqlHelper.ExecuteSql(sql1);
                if (dt.Rows.Count > 0)
                {
                    Rows = int.Parse(dt.Rows[0]["Rows"].ToString());
                }
                if (Rows > 0)
                {
                    myResult = "1";
                }
            }
            return myResult;

        }
        /// <summary>
        /// 判断制单状态的单据是否提交审批
        /// </summary>
        /// <returns></returns>
        public static string IsFlow(int ID)
        {
            string Rows = "0";
            string returnValue = "0";
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITYADD);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(e.ID) as Rows from officedba.QualityCheckApplay as a left join officedba.FlowInstance AS e ON a.CompanyCD = e.CompanyCD AND e.BillTypeFlag =" + BillTypeFlag + " AND e.BillTypeCode = " + BillTypeCode + " AND a.ID = e.BillID ");
            sql.AppendLine(" where a.ID=" + ID + " and e.FlowStatus!=4 and e.FlowStatus!=5");
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                Rows = dt.Rows[0]["Rows"].ToString();
            }
            if (int.Parse(Rows) > 0)
            {
                returnValue = Rows;
            }
            return returnValue;
        }


        public static bool GetCheckCount(StorageQualityCheckApplay model, string IDList, string TheCheckedCount)
        {
            ArrayList sqllist = new ArrayList();
            bool returnvalue = true;
            string[] myID = IDList.Split(',');
            string[] myCheckedCount = TheCheckedCount.Split(',');
            if (model.FromType == "1")//采购
            {
                for (int i = 0; i < myID.Length; i++)
                {
                    string ProductCount = "00";
                    string CheckedCount = "00";
                    string CheckedCount1 = "00";
                    string pursql = "select ProductCount,ApplyCheckCount as CheckedCount,isnull(ProductCount,0)-isnull(ApplyCheckCount,0) as CheckedCount1 from officedba.PurchaseArriveDetail  as  a where a.ID=@ID";
                    SqlCommand purcomm = new SqlCommand();
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i]));
                    purcomm.CommandText = pursql;
                    DataTable dt = SqlHelper.ExecuteSearch(purcomm);
                    if (dt.Rows.Count > 0)
                    {
                        ProductCount = dt.Rows[0]["ProductCount"].ToString();
                        CheckedCount = dt.Rows[0]["CheckedCount"].ToString();
                        CheckedCount1 = dt.Rows[0]["CheckedCount1"].ToString();
                        if (ProductCount != "" && CheckedCount != "")
                        {
                            if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount))
                            {
                                returnvalue = false;
                            }
                        }
                        if (Convert.ToDecimal(myCheckedCount[i]) > Convert.ToDecimal(CheckedCount1))
                        {
                            returnvalue = false;
                        }
                    }
                }

            }
            if (model.FromType == "2")
            {

                for (int i = 0; i < myID.Length; i++)
                {
                    string ProductCount = "00";
                    string CheckedCount = "00";
                    string CheckedCount1 = "00";
                    string pursql = "select ProductedCount,ApplyCheckCount as CheckedCount,isnull(ProductedCount,0)-isnull(ApplyCheckCount,0) as CheckedCount1 from officedba.ManufactureTaskDetail  as  a where a.ID=@ID";
                    SqlCommand purcomm = new SqlCommand();
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i]));
                    purcomm.CommandText = pursql;
                    DataTable dt = SqlHelper.ExecuteSearch(purcomm);
                    if (dt.Rows.Count > 0)
                    {
                        ProductCount = dt.Rows[0]["ProductedCount"].ToString();
                        CheckedCount = dt.Rows[0]["CheckedCount"].ToString();
                        CheckedCount1 = dt.Rows[0]["CheckedCount1"].ToString();
                        if (ProductCount != "" && CheckedCount != "")
                        {
                            if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount))
                            {
                                returnvalue = false;
                            }
                        }
                        if (Convert.ToDecimal(myCheckedCount[i]) > Convert.ToDecimal(CheckedCount1))
                        {
                            returnvalue = false;
                        }
                    }
                }
            }
            return returnvalue;
        }

        public static bool GetCheckSaveCount(StorageQualityCheckApplay model, string IDList, string TheCheckedCount)
        {
            ArrayList sqllist = new ArrayList();
            bool returnvalue = true;
            string[] myID = IDList.Split(',');
            string[] myCheckedCount = TheCheckedCount.Split(',');
            if (model.FromType == "1")//采购
            {
                for (int i = 0; i < myID.Length; i++)
                {
                    string ProductCount = "00";
                    string CheckedCount = "00";
                    string CheckedCount1 = "00";
                    string pursql = "select ProductCount,ApplyCheckCount as CheckedCount,isnull(ProductCount,0)-isnull(ApplyCheckCount,0) as CheckedCount1 from officedba.PurchaseArriveDetail  as  a where a.ID=@ID";
                    SqlCommand purcomm = new SqlCommand();
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i]));
                    purcomm.CommandText = pursql;
                    DataTable dt = SqlHelper.ExecuteSearch(purcomm);
                    if (dt.Rows.Count > 0)
                    {
                        ProductCount = dt.Rows[0]["ProductCount"].ToString(); //到货数量
                        CheckedCount = dt.Rows[0]["CheckedCount"].ToString();//已报检数量
                        CheckedCount1 = dt.Rows[0]["CheckedCount1"].ToString();//未报检数量
                        if (ProductCount != "" && CheckedCount != "")
                        {
                            if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount))
                            {
                                returnvalue = false;
                            }
                        }
                        if (Convert.ToDecimal(myCheckedCount[i]) > Convert.ToDecimal(CheckedCount1))
                        {
                            returnvalue = false;
                        }
                    }
                }

            }
            if (model.FromType == "2")
            {

                for (int i = 0; i < myID.Length; i++)
                {
                    string ProductCount = "00";
                    string CheckedCount = "00";
                    string CheckedCount1 = "00";
                    string pursql = "select ProductedCount,ApplyCheckCount as CheckedCount,isnull(ProductedCount,0)-isnull(ApplyCheckCount,0) as CheckedCount1 from officedba.ManufactureTaskDetail  as  a where a.ID=@ID";
                    SqlCommand purcomm = new SqlCommand();
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", myID[i]));
                    purcomm.CommandText = pursql;
                    DataTable dt = SqlHelper.ExecuteSearch(purcomm);
                    if (dt.Rows.Count > 0)
                    {
                        ProductCount = dt.Rows[0]["ProductedCount"].ToString();
                        CheckedCount = dt.Rows[0]["CheckedCount"].ToString();
                        CheckedCount1 = dt.Rows[0]["CheckedCount1"].ToString();
                        if (ProductCount != "" && CheckedCount != "")
                        {
                            if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount))
                            {
                                returnvalue = false;
                            }
                        }
                        if (Convert.ToDecimal(myCheckedCount[i]) > Convert.ToDecimal(CheckedCount1))
                        {
                            returnvalue = false;
                        }
                    }
                }
            }
            return returnvalue;
        }


        //-----------------------------------------------------页面打印需要
        public static DataTable GetPrintQualityDB(int ID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT     isnull(a.Attachment,'') as Attachment,isnull(a.Remark,'') as Remark,case a.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatus");
            sql.AppendLine("    ,a.Title,a.ApplyNo,a.CustName,case a.FromType when '0' then '无来源' when '1' then '采购到货单' when '2' then '生产任务单' else '' end as FromType,case a.CheckType when '1' then '进货检验' when '2' then '过程检验' when '3' then '最终检验' else '' end as CheckType");
            sql.AppendLine("    ,CASE a.CustBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商'  WHEN '5' THEN '外协加工厂'  WHEN '7' THEN '其他' END AS CustBigType ");
            sql.AppendLine("    ,case a.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '4' then '自动结单' end as BillStatus ");
            sql.AppendLine("    ,a.ModifiedUserID as ModifiedUserID ");
            sql.AppendLine("    ,isnull((SELECT     EmployeeName   FROM officedba.EmployeeInfo AS b  WHERE      (ID = a.Checker)),'') AS Checker,isnull((SELECT EmployeeName  FROM  officedba.EmployeeInfo AS b WHERE (ID = a.Creator)),'') AS Creator");
            sql.AppendLine("    ,isnull((SELECT     EmployeeName   FROM officedba.EmployeeInfo AS b  WHERE      (ID = a.Closer)),'') AS Closer ");
            sql.AppendLine("    ,isnull((SELECT     EmployeeName   FROM officedba.EmployeeInfo AS b  WHERE      (ID = a.Confirmor)),'') AS Confirmor,isnull((SELECT  EmployeeName FROM officedba.EmployeeInfo AS b ");
            sql.AppendLine("    WHERE      (ID = a.Principal)),'') AS Principal,case a.CheckMode when '1' then '全检' when '2' then '抽检' when '3' then '临检' else '' end as CheckMode,isnull(substring(CONVERT(varchar,a.CheckDate,120),0,11),'')as CheckDate,isnull(c.DeptName,'')as DeptID");
            sql.AppendLine("    ,isnull(a.CountTotal,0) as CountTotal,isnull(substring(CONVERT(varchar,a.CloseDate,120),0,11),'') as CloseDate");
            sql.AppendLine("    , isnull(substring(CONVERT(varchar,a.ConfirmDate,120),0,11),'') as ConfirmDate,isnull(substring(CONVERT(varchar,a.ModifiedDate,120),0,11),'') as ModifiedDate,isnull(substring(CONVERT(varchar,a.CreateDate,120),0,11),'') as CreateDate, ");
            sql.AppendLine("    isnull((select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=a.CheckDeptId ),'') as CheckDeptId");
            sql.AppendLine("    FROM         officedba.QualityCheckApplay AS a LEFT  JOIN officedba.DeptInfo AS c ON a.DeptId = c.ID  ");
            sql.AppendLine("    WHERE     a.ID =@ID ");
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetPrintQualityDetail(string ApplyNo, string CompanyCD, string Method)   //
        {
            StringBuilder sql = new StringBuilder();
            DataTable mydt = new DataTable();
            switch (Method)
            {
                case "1"://采购  
                    sql.AppendLine("SELECT   b.ArriveNo as FromBillID,isnull(b.CheckedCount,0) as CheckedCount,isnull(c.ProductName,'') as ID,c.ProdNo as SortNo");
                    sql.AppendLine("        ,isnull(a.ProductCount,0) as ProductCount,isnull(a.Remark,'') as Remark,isnull(d.CodeName,'') as UnitID");
                    sql.AppendLine("        ,isnull(b.ProductCount,0) AS ProductID");
                    sql.AppendLine("        ,isnull(b.ApplyCheckCount,0) as RealCheckedCount");
                    sql.AppendLine(" FROM  officedba.QualityCheckApplyDetail AS a LEFT  JOIN ");
                    sql.AppendLine("       officedba.CodeUnitType AS d ON a.UnitID = d.ID LEFT  JOIN  officedba.PurchaseArriveDetail AS b ON  b.ID=a.FromBillID LEFT  JOIN ");
                    sql.AppendLine("       officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     (a.ApplyNo ='" + ApplyNo + "') AND (a.CompanyCD = '" + CompanyCD + "') ");
                    mydt = SqlHelper.ExecuteSql(sql.ToString());
                    break;
                case "2"://生产任务
                    sql.AppendLine("SELECT    isnull(d.ProductedCount,0) as SortNo,isnull(d.CheckedCount,0) as ID");
                    sql.AppendLine("         ,c.ProductName as FromType,isnull(b.CodeName,'') as UnitID,isnull(a.ProductCount,0) as ProductCount");
                    sql.AppendLine("         ,isnull(d.ApplyCheckCount,0) as ProductID");
                    sql.AppendLine("         ,isnull(a.Remark,'') as Remark,d.TaskNo as FromBillID,c.ProdNo as  ApplyNo");
                    sql.AppendLine(" FROM  officedba.QualityCheckApplyDetail AS a LEFT  JOIN   officedba.ManufactureTaskDetail AS d ON a.FromBillID = d.ID LEFT JOIN ");
                    sql.AppendLine("         officedba.CodeUnitType AS b ON a.UnitID = b.ID LEFT  JOIN  officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     (a.ApplyNo = '" + ApplyNo + "') AND (a.CompanyCD = '" + CompanyCD + "') ");
                    mydt = SqlHelper.ExecuteSql(sql.ToString());
                    break;
                case "0": //无来源时
                    sql.AppendLine("SELECT     isnull(c.ProductName,'') as ID,isnull(a.RealCheckedCount,0) as RealCheckedCount,isnull(c.ProdNo,'') as SortNo,");
                    sql.AppendLine("isnull(a.CheckedCount,0) as CheckedCount,  a.ProductID,isnull(b.CodeName,'') as UnitID,isnull(a.ProductCount,0) as ProductCount ");
                    sql.AppendLine(" ,isnull(a.Remark,'') as Remark  ");
                    sql.AppendLine(" FROM  officedba.QualityCheckApplyDetail AS a LEFT  JOIN   officedba.CodeUnitType AS b ON a.UnitID = b.ID LEFT  JOIN ");
                    sql.AppendLine("       officedba.ProductInfo AS c ON a.ProductID = c.ID WHERE     a.ApplyNo = '" + ApplyNo + "'  AND a.CompanyCD = '" + CompanyCD + "' ");
                    mydt = SqlHelper.ExecuteSql(sql.ToString());
                    break;
                default:
                    break;
            }
            return mydt;

        }


        /// <summary>
        /// 更新附件字段
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Attachment">附件URL</param>
        public static void UpDateAttachment(int ID, string Attachment)
        {
            string sql = "UPDATE officedba.QualityCheckApplay SET Attachment = '{1}' WHERE ID={0}";
            sql = string.Format(sql, ID, Attachment);
            SqlHelper.ExecuteTransSql(sql, new SqlParameter[] { });
        }
    }
}
