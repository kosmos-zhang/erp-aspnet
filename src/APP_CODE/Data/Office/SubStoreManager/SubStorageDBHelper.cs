/***********************************************
 * 类作用：   门店管理事务层处理               *
 * 建立人：   宋飞                             *
 * 建立时间： 2009/05/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.SubStoreManager;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
using XBase.Data.Office.LogisticsDistributionManager;

namespace XBase.Data.Office.SubStoreManager
{
    /// <summary>
    /// 类名：SubStorageDBHelper
    /// 描述：门店库存数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/05/21
    /// 最后修改时间：2009/05/21
    /// </summary>
    ///
    public class SubStorageDBHelper
    {
        #region 绑定门店仓库
        public static DataTable GetdrpStorageName()
        {
            string sql = "select ID,StorageName from officedba.StorageInfo where CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "' and usedstatus=1";
            DataTable data = SqlHelper.ExecuteSql(sql);
            return data;
        }
        #endregion

        #region 插入门店库存入库单
        public static bool InsertSubStorageIn(SubStorageInModel model, string DetailProductID, string DetailSendCount, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, out string ID, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  门店库存入库单添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.SubStorageIn");
            sqlArrive.AppendLine("(CompanyCD,DeptID,InNo,Title,InType,Remark,Creator,CreateDate,");
            sqlArrive.AppendLine("BillStatus,Confirmor,ConfirmDate,ModifiedDate,ModifiedUserID)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@DeptID,@InNo,@Title,@InType,@Remark,@Creator,@CreateDate,");
            sqlArrive.AppendLine("@BillStatus,@Confirmor,@ConfirmDate,getdate(),@ModifiedUserID)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@InType", 1));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                listADD.Add(commExtAttr);
            }
            #endregion


            try
            {
                #region 门店库存产品信息
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] SendCount = DetailSendCount.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.SubStorageInDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("DeptID,");
                        cmdsql.AppendLine("InNo,");
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("ProductID,");
                        if (userInfo.IsMoreUnit)
                        {
                            cmdsql.AppendLine("UsedUnitID,");
                            cmdsql.AppendLine("UsedUnitCount,");
                            cmdsql.AppendLine("ExRate,");
                        }
                        cmdsql.AppendLine("BatchNo,");
                        cmdsql.AppendLine("SendCount)");

                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@DeptID");
                        cmdsql.AppendLine("            ,@InNo");
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@ProductID");
                        if (userInfo.IsMoreUnit)
                        {
                            cmdsql.AppendLine("            ,@UsedUnitID");
                            cmdsql.AppendLine("            ,@UsedUnitCount");
                            cmdsql.AppendLine("            ,@ExRate");
                        }
                        cmdsql.AppendLine("            ,@BatchNo");
                        cmdsql.AppendLine("            ,@SendCount)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                        comms.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        if (userInfo.IsMoreUnit)
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo[i]));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SendCount", SendCount[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }
                #endregion


                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
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
        /// 扩展属性更新命令
        /// </summary>
        /// <param name="model">门店库存入库单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(SubStorageInModel model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.SubStorageIn SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND InNo = @InNo ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }
        #endregion

        #region 修改门店库存入库单
        public static bool UpdateSubStorageIn(SubStorageInModel model, string DetailProductID, string DetailSendCount, string DetailUsedUnitID, string DetailUsedUnitCount, string DetailUsedPrice, string DetailExRate, string DetailBatchNo, string length, string no, Hashtable htExtAttr)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改门店库存入库单
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.SubStorageIn set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("DeptID=@DeptID,InNo=@InNo,Title=@Title,InType=@InType,Remark=@Remark,");
            sqlArrive.AppendLine("BillStatus=@BillStatus,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate,");
            sqlArrive.AppendLine("ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID where CompanyCD=@CompanyCD and InNo=@InNo and ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@InType", 1));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.CreateDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.ConfirmDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                listADD.Add(commExtAttr);
            }
            #endregion

            #region 删除门店库存产品信息
            System.Text.StringBuilder cmdddetail = new System.Text.StringBuilder();
            cmdddetail.AppendLine("DELETE  FROM officedba.SubStorageInDetail WHERE  CompanyCD=@CompanyCD and InNo=@InNo");
            SqlCommand comn = new SqlCommand();
            comn.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comn.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
            comn.CommandText = cmdddetail.ToString();
            listADD.Add(comn);
            #endregion



            try
            {
                #region 重新插入门店库存产品信息
                int lengs = Convert.ToInt32(length);
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] SendCount = DetailSendCount.Split(',');
                    string[] UsedUnitID = DetailUsedUnitID.Split(',');
                    string[] UsedUnitCount = DetailUsedUnitCount.Split(',');
                    string[] UsedPrice = DetailUsedPrice.Split(',');
                    string[] ExRate = DetailExRate.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');

                    for (int i = 0; i < lengs; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        SqlCommand comms = new SqlCommand();
                        cmdsql.AppendLine("INSERT INTO officedba.SubStorageInDetail");
                        cmdsql.AppendLine("(CompanyCD,");
                        cmdsql.AppendLine("DeptID,");
                        cmdsql.AppendLine("InNo,");
                        cmdsql.AppendLine("SortNo,");
                        cmdsql.AppendLine("ProductID,");
                        if (userInfo.IsMoreUnit)
                        {
                            cmdsql.AppendLine("UsedUnitID,");
                            cmdsql.AppendLine("UsedUnitCount,");
                            cmdsql.AppendLine("ExRate,");
                        }
                        cmdsql.AppendLine("BatchNo,");
                        cmdsql.AppendLine("SendCount)");

                        cmdsql.AppendLine(" Values(@CompanyCD");
                        cmdsql.AppendLine("            ,@DeptID");
                        cmdsql.AppendLine("            ,@InNo");
                        cmdsql.AppendLine("            ,@SortNo");
                        cmdsql.AppendLine("            ,@ProductID");
                        if (userInfo.IsMoreUnit)
                        {
                            cmdsql.AppendLine("            ,@UsedUnitID");
                            cmdsql.AppendLine("            ,@UsedUnitCount");
                            cmdsql.AppendLine("            ,@ExRate");
                        }
                        cmdsql.AppendLine("            ,@BatchNo");
                        cmdsql.AppendLine("            ,@SendCount)");


                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
                        comms.Parameters.Add(SqlHelper.GetParameter("@InNo", model.InNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SortNo", i + 1));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID[i].ToString()));
                        if (userInfo.IsMoreUnit)
                        {
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", UsedUnitID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", UsedUnitCount[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ExRate", ExRate[i].ToString()));
                        }
                        comms.Parameters.Add(SqlHelper.GetParameter("@BatchNo", BatchNo[i]));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SendCount", SendCount[i].ToString()));
                        comms.CommandText = cmdsql.ToString();
                        listADD.Add(comms);
                    }
                }
                #endregion


                if (SqlHelper.ExecuteTransWithArrayList(listADD))
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
        #endregion

        #region 确认门店库存入库单
        public static bool ConfirmSubStorageIn(SubStorageInModel Model, string DetailProductID, string DetailSendCount, string DetailUnitPrice, string DetailBatchNo, string length)
        {
            #region 确认门店库存单中信息
            ArrayList listADD = new ArrayList();
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.SubStorageIn set ");
            strSql.AppendLine(" Confirmor=@Confirmor");
            strSql.AppendLine(" ,BillStatus=5");
            strSql.AppendLine(" ,ConfirmDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" CompanyCD=@CompanyCD");
            strSql.AppendLine(" and ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", Model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", Model.ID));
            comm.CommandText = strSql.ToString();

            listADD.Add(comm);
            #endregion

            #region 确认时插入物流配送中的分店存量表
            try
            {

                int lengs = Convert.ToInt32(length);
                int id = 0;
                decimal count = 0m;
                if (lengs > 0)
                {
                    string[] ProductID = DetailProductID.Split(',');
                    string[] SendCount = DetailSendCount.Split(',');
                    string[] UnitPrice = DetailUnitPrice.Split(',');
                    string[] BatchNo = DetailBatchNo.Split(',');
                    for (int i = 0; i < lengs; i++)
                    {
                        #region 添加门店库存流水帐
                        SubStorageAccountModel aModel = new SubStorageAccountModel();
                        aModel.BatchNo = BatchNo[i];
                        aModel.BillNo = Model.InNo;
                        aModel.BillType = 1;
                        aModel.CompanyCD = Model.CompanyCD;
                        aModel.Creator = Model.Confirmor;
                        aModel.DeptID = Model.DeptID;
                        aModel.HappenDate = DateTime.Now;
                        if (int.TryParse(ProductID[i], out id))
                        {
                            aModel.ProductID = id;
                        }
                        if (decimal.TryParse(UnitPrice[i], out count))
                        {
                            aModel.Price = count;
                        }
                        if (decimal.TryParse(SendCount[i], out count))
                        {
                            aModel.HappenCount = count;
                        }
                        aModel.PageUrl = Model.Remark;
                        listADD.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                        #endregion

                        // 更新分店存量表
                        listADD.Add(StorageProductQueryDBHelper.UpdateProductCount(Model.CompanyCD
                            , ProductID[i], Model.DeptID.ToString(), BatchNo[i], count));
                    }
                }



                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    //ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

        }
        #endregion

        #region 查询门店分店期初库存列表所需数据
        public static DataTable SelectSubStorageInitList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string DeptID, string ProductID, string ProductName, string EFIndex, string EFDesc, string BatchNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.DeptID,isnull(B.DeptName,'') AS DeptName, A.InNo,isnull(A.Title,'') AS Title             ");
            sql.AppendLine("      ,isnull(A.Confirmor,0) AS Confirmor                                         ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS ConfirmorName,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS ConfirmDate");
            sql.AppendLine("      ,A.BillStatus,case A.BillStatus  when '1' then '制单' when '5'then '自动结单'end AS BillStatusName");
            if (userInfo.IsMoreUnit)
            {
                sql.AppendLine("      ,Convert(numeric(22," + userInfo.SelPoint + "),isnull(sum(D.UsedUnitCount),0))  AS  CountTotal");
            }
            else
            {
                sql.AppendLine("      ,Convert(numeric(22," + userInfo.SelPoint + "),isnull(sum(D.SendCount),0))  AS  CountTotal");
            }

            sql.AppendLine(" FROM officedba.SubStorageIn AS A                                                                        ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS B ON A.CompanyCD = B.CompanyCD AND A.DeptID=B.ID                         ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Confirmor=C.ID                  ");
            sql.AppendLine("LEFT JOIN officedba.SubStorageInDetail AS D ON A.CompanyCD = D.CompanyCD AND A.InNo=D.InNo               ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS E ON D.CompanyCD = E.CompanyCD AND D.ProductID=E.ID                   ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (DeptID != "" && DeptID != null)
            {
                sql.AppendLine(" AND A.DeptID=@DeptID ");
            }
            if (ProductID != null && ProductID != "")
            {
                sql.AppendLine(" AND E.ID =@ProductID");
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and A.ExtField" + EFIndex + " LIKE @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND E.ProductName like @ProductName  ");
            }

            if (BatchNo != null && BatchNo != "")
            {
                sql.AppendLine(" AND D.BatchNo like @BatchNo  ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + ProductName + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", "%" + BatchNo + "%"));
            sql.AppendLine(" group by A.ID,A.DeptID,B.DeptName,A.InNo,A.Title,A.Confirmor,C.EmployeeName,A.ConfirmDate,A.BillStatus   ");

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 查找加载单个门店期初库存录入
        public static DataTable SubStorageIn(int ID)
        {
            StringBuilder sql = new StringBuilder();


            sql.AppendLine("SELECT A.ID ,A.DeptID ,isnull(B.DeptName,'') AS DeptName,A.InNo,isnull(A.Title,'') AS Title,isnull(A.Remark,'') AS Remark");
            sql.AppendLine("     ,isnull(A.Creator,0) AS Creator,isnull(C.EmployeeName,'') AS CreatorName,isnull(CONVERT(varchar(100),A.CreateDate,23),'') AS CreateDate");
            sql.AppendLine("     ,isnull(A.Confirmor,0) AS Confirmor,isnull(D.EmployeeName,'') AS ConfirmorName,isnull(CONVERT(varchar(100),A.ConfirmDate,23),'') AS ConfirmDate");
            sql.AppendLine("     ,isnull(CONVERT(varchar(100),A.ModifiedDate,23),'') AS ModifiedDate,isnull(A.ModifiedUserID,'') AS ModifiedUserID");
            sql.AppendLine("     ,A.BillStatus,case A.BillStatus when '1' then '制单' when '5' then '自动结单' end AS BillStatusName");
            sql.AppendLine("     ,A.ExtField1,A.ExtField2,A.ExtField3,A.ExtField4,A.ExtField5,A.ExtField6,A.ExtField7,A.ExtField8,A.ExtField9,A.ExtField10 ");
            sql.AppendLine(" FROM officedba.SubStorageIn AS A  ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo     AS B ON A.CompanyCD = B.CompanyCD AND A.DeptID=B.ID          ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.Creator=C.ID         ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND A.Confirmor=D.ID       ");
            sql.AppendLine("WHERE 1=1                                   ");
            sql.AppendLine("AND A.ID=@ID          ");

            SqlParameter[] parms =
            {
                new SqlParameter("@ID",ID)
            };
            return SqlHelper.ExecuteSql(sql.ToString(), parms);

        }
        #endregion

        #region 查找加载门店期初库存录入明细
        public static DataTable Details(int ID, string DeptID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID,A.ProductID, Convert(numeric(12,2),isnull(A.SendCount,0)) AS SendCount,isnull(B.DeptName,'') AS DeptName,A.InNo,A.DeptID ,A.UsedUnitID,A.UsedUnitCount,A.UsedPrice,A.ExRate,A.BatchNo     ");
            sql.AppendLine(" ,isnull(C.ProdNo,'') AS ProductNo, isnull(C.ProductName,'') AS ProductName,isnull(C.Specification,'') AS standard,C.IsBatchNo ");
            sql.AppendLine(" ,isnull(C.UnitID,0) AS UnitID,isnull(E.CodeName,'') AS UnitName,F.CodeName AS UsedUnitName ");


            sql.AppendLine("FROM officedba.SubStorageInDetail AS A                                                        ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo      AS B ON A.CompanyCD = B.CompanyCD  AND A.DeptID=B.ID        ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo   AS C on A.CompanyCD = C.CompanyCD  AND A.ProductID=C.ID     ");
            sql.AppendLine("LEFT JOIN officedba.SubStorageIn  AS D on A.CompanyCD = D.CompanyCD  AND A.InNo=D.InNo        ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType  AS E ON C.CompanyCD = E.CompanyCD  AND C.UnitID=E.ID        ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType  AS F ON C.CompanyCD = F.CompanyCD  AND A.UsedUnitID=F.ID        ");

            sql.AppendLine("WHERE 1=1             ");
            sql.AppendLine("AND D.ID=@ID          ");
            sql.AppendLine("AND A.DeptID=@DeptID  ");

            SqlParameter[] parms = 
            {
               new SqlParameter("@ID",ID),
               new SqlParameter("@DeptID",DeptID)
            };

            return SqlHelper.ExecuteSql(sql.ToString(), parms);

        }
        #endregion

        #region 查询门店分店库存列表所需数据
        public static DataTable SelectSubStorageProduct(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string DeptID, string ProductID, string ProductName, string BatchNo)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT distinct isnull(A.ProductID,0) AS ProductID ,isnull(B.ProdNo,'') AS  ProductNo, isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification");
            sql.AppendLine("      ,isnull(B.UnitID,0) AS UnitID, isnull(C.CodeName,'') AS  UnitName,isnull(A.DeptID,0) AS  DeptID,isnull(D.DeptName,'') AS DeptName");
            sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(sum(A.ProductCount),0))) as ProductCount ,A.BatchNo ");
            sql.AppendLine("      ,Convert(decimal(18," + SelPoint + "),isnull(sum(A.ProductCount),0)) ProductCount2 ");

            sql.AppendLine(" FROM officedba.SubStorageProduct AS A                                                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C  ON  B.CompanyCD = C.CompanyCD AND B.UnitID = C.ID                 ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.CompanyCD = D.CompanyCD AND A.DeptID=D.ID                         ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (DeptID != "" && DeptID != null && DeptID != "00")
            {
                sql.AppendLine(" AND A.DeptID=@DeptID ");
            }
            if (ProductID != null && ProductID != "")
            {
                sql.AppendLine(" AND B.ID =@ProductID");
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND B.ProductName like '%" + @ProductName + "%'  ");
            }
            if (BatchNo != null && BatchNo != "")
            {
                sql.AppendLine(" AND A.BatchNo like '%" + BatchNo + "%'  ");
            }


            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            sql.AppendLine("group by A.ProductID,B.ProdNo,B.ProductName,B.Specification,B.UnitID,C.CodeName,A.DeptID,D.DeptName,A.BatchNo");

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 查询门店总部库存列表所需数据
        public static DataTable SelectStorageProduct(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string StorageID, string ProductID, string ProductName, string BatchNo)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,isnull(A.ProductID,0) AS ProductID ,isnull(B.ProdNo,'') AS  ProductNo, isnull(B.ProductName,'') AS ProductName,isnull(B.Specification,'') AS Specification");
            sql.AppendLine("      ,isnull(B.UnitID,0) AS UnitID, isnull(C.CodeName,'') AS  UnitName,isnull(A.DeptID,0) AS  DeptID,isnull(D.DeptName,'') AS DeptName");
            sql.AppendLine("       ,Convert(numeric(18," + SelPoint + "),isnull(A.ProductCount,0)) as  ProductCount");
            sql.AppendLine("       ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(A.ProductCount,0)))+'&nbsp;' as  ProductCount2");
            sql.AppendLine("       ,isnull(E.CodeName,'') AS TypeIDName");
            sql.AppendLine("      ,isnull(A.StorageID,0) AS StorageID, isnull(Convert(numeric(12,2),isnull(A.ProductCount,0)+isnull(A.RoadCount,0)-isnull(A.OrderCount,0)-isnull(A.OutCount,0)+isnull(A.InCount,0)),0) AS CanUseCount");
            sql.AppendLine("      ,isnull(F.StorageNo,'') AS StorageNo,isnull(F.StorageName,'') AS StorageName,A.BatchNo  ");

            sql.AppendLine(" FROM officedba.StorageProduct AS A                                                                      ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS B ON A.CompanyCD = B.CompanyCD AND A.ProductID=B.ID                   ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS C  ON  B.CompanyCD = C.CompanyCD AND B.UnitID = C.ID                 ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.CompanyCD = D.CompanyCD AND A.DeptID=D.ID                         ");
            sql.AppendLine("LEFT JOIN officedba.CodeProductType AS E ON B.CompanyCD = E.CompanyCD AND B.TypeID = E.ID                ");
            sql.AppendLine("LEFT JOIN officedba.StorageInfo AS F ON A.CompanyCD = F.CompanyCD AND A.StorageID = F.ID                 ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            if (StorageID != "" && StorageID != null)
            {
                sql.AppendLine(" AND A.StorageID=@StorageID ");
            }
            if (ProductID != null && ProductID != "")
            {
                sql.AppendLine(" AND B.ID =@ProductID");
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND B.ProductName like '%" + @ProductName + "%'  ");
            }

            if (BatchNo != null && BatchNo != "")
            {
                sql.AppendLine(" AND A.BatchNo like '%" + BatchNo + "%'  ");
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

        }
        #endregion

        #region 删除门店入库单
        public static SqlCommand DeleteSubStorageIn(string IDs)
        {
            SqlCommand comm = new SqlCommand();

            String sql = "DELETE FROM officedba.SubStorageIn WHERE ID in (" + IDs + ")";

            comm.CommandText = sql;

            return comm;
        }
        #endregion

        #region 删除门店入库单明细
        public static SqlCommand DeleteSubStorageInDetail(string InNos, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.SubStorageInDetail");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND InNo in (" + InNos + ")");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 报表相关操作
        /// <summary>
        /// 根据时间查询分店销售记录
        /// </summary>
        /// <param name="Querytime"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfo(DateTime Querytime, DateTime QueryEndtime, string strCompanyCD)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select z.DeptName as DeptID,Convert(decimal(18," + SelPoint + "),x.RealTotal) RealTotal,x.aDeptid as TheDeptID from( select t.aDeptid,isnull(Convert(numeric(20,4),y.RealTotal),0) as RealTotal  ");
            sql.AppendLine("from (select sum(isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100) as RealTotal,a.DeptID as aDeptid  ");
            sql.AppendLine("  from officedba.SubSellOrder as a where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("  group by a.DeptID)t ");
            sql.AppendLine("  left join (select sum(isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100) as RealTotal,a.DeptID as aDeptid ");
            sql.AppendLine("   from officedba. SubSellOrder as a   ");
            sql.AppendLine("  where a.OrderDate >=@Querytime and a.OrderDate<=@QueryEndtime and  a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            sql.AppendLine("    group by a.DeptID)y on t.aDeptid=y.aDeptid)x left join  officedba.DeptInfo z on z.ID=x.aDeptID  ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", QueryEndtime));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }


        /// <summary>
        /// 获取门店销售额按部门分析详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static SqlCommand SelectSubSellOrder(string CompanyCD, string BeginDate, string EndDate, string DeptID, string OrderBy)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT A.ID                     ");
            sql.AppendLine("      ,A.CompanyCD              ");
            sql.AppendLine("      ,A.OrderNo                ");
            sql.AppendLine("      ,A.Title                  ");
            sql.AppendLine("      ,A.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,A.SendMode               ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,A.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr              ");
            sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100))+'&nbsp;' as TotalPrice ");
            //sql.AppendLine("      ,   isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100 as TotalPrice");
            sql.AppendLine("  FROM officedba.SubSellOrder AS A");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON A.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller=C.ID");
            sql.AppendLine(" WHERE A.BillStatus<>'1' and A.BillStatus<>'3'");
            sql.AppendLine(" AND A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and A.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and A.OrderDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }

            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }
        /// <summary>
        /// 门店销售数量部门分析
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="QueryEndtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfoByDept(DateTime Querytime, DateTime QueryEndtime, string strCompanyCD, string ProductID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            ArrayList arr = new ArrayList();
            sql.AppendLine("select z.DeptName as DeptID,Convert(decimal(18," + SelPoint + "),x.RealTotal) RealTotal,x.aDeptid as TheDeptID from( select t.aDeptid,isnull(Convert(numeric(20,2),y.RealTotal),0) as RealTotal  ");
            sql.AppendLine("from (select sum(isnull(a.CountTotal,0)) as RealTotal,a.DeptID as aDeptid  ");
            sql.AppendLine("  from officedba. SubSellOrder as a where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("  group by a.DeptID)t ");
            sql.AppendLine("  left join (select sum(isnull(a.CountTotal,0)) as RealTotal,a.DeptID as aDeptid ");
            sql.AppendLine("   from officedba. SubSellOrder as a left join officedba.SubSellOrderDetail as b on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD   ");
            sql.AppendLine("  where a.OrderDate >=@Querytime and a.OrderDate<=@QueryEndtime and  a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and b.ProductID=@ProductID ");
                arr.Add(new SqlParameter("@ProductID", ProductID));
            }
            sql.AppendLine("    group by a.DeptID)y on t.aDeptid=y.aDeptid)x left join  officedba.DeptInfo z on z.ID=x.aDeptID  ");

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", QueryEndtime));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 门店销售数量部门分析-详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static SqlCommand SelectSubSellOrderByDeptID(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT A.ID                     ");
            sql.AppendLine("      ,A.CompanyCD              ");
            sql.AppendLine("      ,A.OrderNo                ");
            sql.AppendLine("      ,A.Title                  ");
            sql.AppendLine("      ,A.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,A.SendMode               ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,A.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr              ");
            sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(a.CountTotal,0)))+'&nbsp;' as TotalCount ");
            //sql.AppendLine("      ,isnull(a.CountTotal,0) as TotalCount");
            sql.AppendLine("  FROM officedba.SubSellOrder AS A left join officedba.SubSellOrderDetail as sd on A.OrderNo=sd.OrderNo and A.CompanyCD=sd.CompanyCD");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON A.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller=C.ID");
            sql.AppendLine(" WHERE A.BillStatus<>'1' and A.BillStatus<>'3'");
            sql.AppendLine(" AND A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" AND sd.ProductID = @ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            }
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and A.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and A.OrderDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }


        /// <summary>
        /// 门店销售数量产品分析
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="QueryEndtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubSellInfoByProduct(DateTime Querytime, DateTime QueryEndtime, string strCompanyCD, string DeptID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            ArrayList arr = new ArrayList();
            sql.AppendLine(" select Pro.ProductName,Convert(decimal(18," + SelPoint + "),sum(isnull(b.ProductCount,0))) as TotalCount,b.ProductID ");
            sql.AppendLine("   from officedba. SubSellOrder as a left join officedba.SubSellOrderDetail as b on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD   ");
            sql.AppendLine("   left join officedba.ProductInfo as Pro on Pro.ID=b.ProductID");
            sql.AppendLine("  where a.OrderDate >=@Querytime and a.OrderDate<=@QueryEndtime and  a.CompanyCD=@CompanyCD and a.BillStatus!='1' and a.BillStatus!='3'  ");
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" and a.DeptID=@DeptID ");
                arr.Add(new SqlParameter("@DeptID", DeptID));
            }
            sql.AppendLine("    group by b.ProductID,Pro.ProductName   ");
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", QueryEndtime));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 门店销售数量产品分析-详细信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public static SqlCommand GetSubSellInfoByProduct(string CompanyCD, string BeginDate, string EndDate, string ProductID, string DeptID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select");
            sql.AppendLine("      T2.OrderNo                ");
            sql.AppendLine("      ,T2.Title                  ");
            sql.AppendLine("      ,T2.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,T2.SendMode               ");
            sql.AppendLine(",case T2.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,T2.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(T2.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(T2.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(T2.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(T2.CustAddr,'') AS CustAddr, T1.*              ");
            //sql.AppendLine("  from ( select sum(isnull(a.ProductCount,0)) as TotalCount,a.ProductID,a.OrderNo as OrderNo2,a.CompanyCD ");
            sql.AppendLine("  from ( select Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(sum(isnull(a.ProductCount,0)),0)))+'&nbsp;' as TotalCount,a.ProductID,a.OrderNo as OrderNo2,a.CompanyCD ");
            //sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(a.CountTotal,0)))+'&nbsp;' as TotalCount ");
            sql.AppendLine("   from officedba.SubSellOrderDetail as a   ");
            sql.AppendLine("   where a.CompanyCD=@CompanyCD  ");
            if (ProductID != "" && ProductID != "0")
            {
                sql.AppendLine(" and a.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            sql.AppendLine("   group by a.ProductID,a.OrderNo,a.CompanyCD) as T1   ");
            sql.AppendLine(" left join officedba.SubSellOrder as T2 on T1.OrderNo2=T2.OrderNo and T1.CompanyCD=T2.CompanyCD ");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON T2.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON T2.Seller=C.ID");

            sql.AppendLine(" WHERE T2.BillStatus<>'1' and T2.BillStatus<>'3'");
            sql.AppendLine(" AND T2.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" AND T2.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and T2.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and T2.OrderDate<=@EndDate ");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }

            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }
        /// <summary>
        /// 根据时间和物品生成报表
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string ProID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("    select distinct x.*,y.ProductName from( select n.DeptName,t.ProductID,t.deptid,isnull(Convert(numeric(20,4),t.ProductCount),0) as ProductCount, ");
            sql.AppendLine("             isnull(Convert(numeric(24,4),t.TotalFee),0) as TotalFee                                                                                ");
            sql.AppendLine(" from( select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                                                                             ");
            sql.AppendLine("sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee                                                                                     ");
            sql.AppendLine("from officedba.SubSellOrderDetail as a                                                                                                              ");
            sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo and a.CompanyCD=b.CompanyCD where a.CompanyCD=@CompanyCD and  (b.BillStatus!='1' and b.BillStatus!='3')                                                                      ");
            if (!string.IsNullOrEmpty(ProID))
                sql.AppendLine(" and a.ProductID=@ProID ");
            sql.AppendLine("               group by a.ProductID,a.DeptID )m left join (select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                         ");
            sql.AppendLine("              sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee from officedba.SubSellOrderDetail as a                                ");
            sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo and a.CompanyCD=b.CompanyCD where (b.BillStatus!='1' and b.BillStatus!='3')and                                                                  ");
            sql.AppendLine("               a.CompanyCD=@CompanyCD and b.OrderDate >=@Querytime                                                                                    ");
            sql.AppendLine("              and b.OrderDate<=@QueryEndtime                                                                      ");
            if (!string.IsNullOrEmpty(ProID))
                sql.AppendLine(" and  a.ProductID=@ProID ");
            sql.AppendLine("              group by a.ProductID,a.DeptID )t on m.DeptID=t.DeptID                                                                                 ");
            sql.AppendLine("              left join officedba.DeptInfo n on n.ID=m.DeptID)x left join  officedba.ProductInfo y                                                  ");
            sql.AppendLine(" on    x.ProductID=y.ID                                                                                         ");

            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", Endtime));
            if (!string.IsNullOrEmpty(ProID))
                arr.Add(new SqlParameter("@ProID", ProID));
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 根据时间和物品生成报表（打印）
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string ProID, string ordercolumn, string ordertype)
        {
            StringBuilder sql = new StringBuilder();
            #region
            //sql.AppendLine("    select distinct x.*,y.ProductName from( select n.DeptName,t.ProductID,t.deptid,isnull(Convert(numeric(20,4),t.ProductCount),0) as ProductCount, ");
            //sql.AppendLine("             isnull(Convert(numeric(20,4),t.TotalFee),0) as TotalFee                                                                                ");
            //sql.AppendLine(" from( select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                                                                             ");
            //sql.AppendLine("sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee                                                                                     ");
            //sql.AppendLine("from officedba.SubSellOrderDetail as a                                                                                                              ");
            //sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo and a.CompanyCD=b.CompanyCD  and (b.BillStatus!='1' and b.BillStatus!='3')                                                                    ");
            //sql.AppendLine(" where a.CompanyCD=@CompanyCD");
            //if (!string.IsNullOrEmpty(ProID))
            //    sql.AppendLine("  and a.ProductID=@ProID    ");
            //sql.AppendLine("               group by a.ProductID,a.DeptID )m left join (select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                         ");
            //sql.AppendLine("              sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee from officedba.SubSellOrderDetail as a                                ");
            //sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo  and a.CompanyCD=b.CompanyCD where     (b.BillStatus!='1' and b.BillStatus!='3')                                                             ");
            //sql.AppendLine("              and a.CompanyCD=@CompanyCD and b.CompanyCD=@CompanyCD and b.OrderDate >=@Querytime                                                                                    ");
            //sql.AppendLine("              and b.OrderDate<=@QueryEndtime                                                                      ");
            //if (!string.IsNullOrEmpty(ProID))
            //    sql.AppendLine(" and  a.ProductID=@ProID ");
            //sql.AppendLine("              group by a.ProductID,a.DeptID )t on m.DeptID=t.DeptID                                                                                 ");
            //sql.AppendLine("              left join officedba.DeptInfo n on n.ID=m.DeptID)x left join  officedba.ProductInfo y                                                  ");
            //sql.AppendLine(" on    x.ProductID=y.ID                                                                                         ");
            #endregion
            sql.AppendLine("    select distinct x.*,y.ProductName from( select n.DeptName,t.ProductID,t.deptid,isnull(Convert(numeric(20,4),t.ProductCount),0) as ProductCount, ");
            sql.AppendLine("             isnull(Convert(numeric(20,4),t.TotalFee),0) as TotalFee                                                                                ");
            sql.AppendLine(" from( select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                                                                             ");
            sql.AppendLine("sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee                                                                                     ");
            sql.AppendLine("from officedba.SubSellOrderDetail as a                                                                                                              ");
            sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo and a.CompanyCD=b.CompanyCD where a.CompanyCD=@CompanyCD and  (b.BillStatus!='1' and b.BillStatus!='3')                                                                      ");
            if (!string.IsNullOrEmpty(ProID))
                sql.AppendLine(" and a.ProductID=@ProID ");
            sql.AppendLine("               group by a.ProductID,a.DeptID )m left join (select a.ProductID,sum(a.ProductCount)as ProductCount ,a.DeptID,                         ");
            sql.AppendLine("              sum(isnull(a.TotalPrice,0)*isnull(b.Discount,0)/100)as TotalFee from officedba.SubSellOrderDetail as a                                ");
            sql.AppendLine("               left join officedba.SubSellOrder as b on a.OrderNo =b.OrderNo and a.CompanyCD=b.CompanyCD where (b.BillStatus!='1' and b.BillStatus!='3')and                                                                  ");
            sql.AppendLine("               a.CompanyCD=@CompanyCD and b.OrderDate >=@Querytime                                                                                    ");
            sql.AppendLine("              and b.OrderDate<=@QueryEndtime                                                                      ");
            if (!string.IsNullOrEmpty(ProID))
                sql.AppendLine(" and  a.ProductID=@ProID ");
            sql.AppendLine("              group by a.ProductID,a.DeptID )t on m.DeptID=t.DeptID                                                                                 ");
            sql.AppendLine("              left join officedba.DeptInfo n on n.ID=m.DeptID)x left join  officedba.ProductInfo y                                                  ");
            sql.AppendLine(" on    x.ProductID=y.ID                                                                                         ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql.AppendLine("order by " + ordercolumn);
                //sql += "order by " + ordercolumn;
                ////sql+="(" order by " + ordercolumn)";
            }
            else
            {
                sql.AppendLine("order by TotalFee");
                //sql += " order by DeptName ";
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(" " + ordertype);
            }
            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", Endtime));
            if (!string.IsNullOrEmpty(ProID))
                arr.Add(new SqlParameter("@ProID", ProID));
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }

        /// <summary>
        /// 根据时间和门店生成报表
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfoByDept(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Flag, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string sql = string.Empty;
            if (Flag == "DEPT")
            {
                sql += "select n.DeptName, t.deptid,isnull(Convert(numeric(20,4),t.CountTotal),0)CountTotal,                                           ";
                sql += "isnull(Convert(numeric(20,4),t.RealTotal),0)RealTotal,isnull(Convert(numeric(20,4),0-t.BackCountTotal),0)BackCountTotal,         ";
                sql += "isnull(Convert(numeric(20,4),0-t.BackRealTota),0)BackRealTota from                                                               ";
                sql += "(SELECT isnull(a.deptid,b.deptid)deptid,a.CountTotal,a.RealTotal,isnull(b.BackCountTotal,0)BackCountTotal,                     ";
                sql += "isnull(b.BackRealTota,0)BackRealTota FROM                                                                                      ";
                sql += "(select DeptID,sum(CountTotal)CountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)RealTotal from officedba.SubSellOrder where  OrderDate>=@Querytime and CompanyCD=@CompanyCD    ";
                sql += "and OrderDate<=@QueryEndtime and  (BillStatus!='1' and BillStatus!='3') group by DeptID) a                                                                                 ";
                sql += "full JOIN                                                                                                                      ";
                sql += "(select DeptID,sum(CountTotal)BackCountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)BackRealTota from officedba.SubSellBack where BackDate>=@Querytime and CompanyCD=@CompanyCD ";
                sql += "and BackDate<=@QueryEndtime and (BillStatus!='1' and BillStatus!='3') group by DeptID) b                                                                                  ";
                sql += "on a.deptid=b.deptid)t left join officedba.DeptInfo n on n.ID=t.DeptID                                     ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += " where t.DeptID=@Dept";
            }
            else
            {
                sql += "select n.EmployeeName,y.DeptName,t.Seller,t.DeptID,isnull(Convert(numeric(20,4),t.CountTotal),0)CountTotal,                                        ";
                sql += "isnull(Convert(numeric(20,4),t.RealTotal),0)RealTotal,isnull(Convert(numeric(20,4),0-t.BackCountTotal),0)BackCountTotal,          ";
                sql += "isnull(Convert(numeric(20,4),0-t.BackRealTota),0)BackRealTota  from                                                               ";
                sql += "(SELECT isnull(a.Seller,b.Seller)Seller,isnull(a.DeptID,b.DeptID)DeptID,a.CountTotal,a.RealTotal,isnull(b.BackCountTotal,0)BackCountTotal,                     ";
                sql += "isnull(b.BackRealTota,0)BackRealTota                                                                                           ";
                sql += " FROM                                                                                                                          ";
                sql += "(select Seller,DeptID,sum(CountTotal)CountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)RealTotal from officedba.SubSellOrder where OrderDate>=@Querytime     ";
                sql += "and OrderDate<=@QueryEndtime and CompanyCD=@CompanyCD and (BillStatus!='1' and BillStatus!='3') ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += "and DeptID=@Dept ";
                sql += "group by Seller,DeptID) a      ";
                sql += "full JOIN                                                                                                                      ";
                sql += "(select DeptID, Seller,sum(CountTotal)BackCountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)BackRealTota from officedba.SubSellBack where BackDate>=@Querytime ";
                sql += "and BackDate<=@QueryEndtime  and CompanyCD=@CompanyCD  and (BillStatus!='1' and BillStatus!='3') ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += "and DeptID=@Dept";
                sql += " group by Seller,DeptID) b                                                                                  ";
                sql += "on a.Seller=b.Seller)t left join officedba.EmployeeInfo n on n.ID=t.Seller left join officedba.DeptInfo y on y.ID=t.DeptID                                     ";
                if (!string.IsNullOrEmpty(Flag))
                    sql += "where t.Seller=@Flag ";
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            if (Flag != "")
                arr.Add(new SqlParameter("@Flag", Flag));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }


        /// <summary>
        /// 根据时间和门店生成报表
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubProductSellInfoByDept(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Flag, string ordercolumn, string ordertype)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            string sql = string.Empty;
            if (Flag == "DEPT")
            {
                sql += "select n.DeptName, t.deptid,isnull(Convert(numeric(20," + SelPoint + "),t.CountTotal),0)CountTotal,                                           ";
                sql += "isnull(Convert(numeric(20," + SelPoint + "),t.RealTotal),0)RealTotal,isnull(Convert(numeric(20," + SelPoint + "),0-t.BackCountTotal),0)BackCountTotal,         ";
                sql += "isnull(Convert(numeric(20," + SelPoint + "),0-t.BackRealTota),0)BackRealTota from                                                               ";
                sql += "(SELECT isnull(a.deptid,b.deptid)deptid,a.CountTotal,a.RealTotal,isnull(b.BackCountTotal,0)BackCountTotal,                     ";
                sql += "isnull(b.BackRealTota,0)BackRealTota FROM                                                                                      ";
                sql += "(select DeptID,sum(CountTotal)CountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)RealTotal from officedba.SubSellOrder where  OrderDate>=@Querytime and CompanyCD=@CompanyCD   ";
                sql += "and OrderDate<=@QueryEndtime and (BillStatus!='1' and BillStatus!='3')  group by DeptID) a                                                                                 ";
                sql += "full JOIN                                                                                                                      ";
                sql += "(select DeptID,sum(CountTotal)BackCountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)BackRealTota from officedba.SubSellBack where BackDate>=@Querytime ";
                sql += "and BackDate<=@QueryEndtime and CompanyCD=@CompanyCD and (BillStatus!='1' and BillStatus!='3') group by DeptID) b                                                                                  ";
                sql += "on a.deptid=b.deptid)t left join officedba.DeptInfo n on n.ID=t.DeptID                                     ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += " where t.DeptID=@Dept";
                if (!string.IsNullOrEmpty(ordercolumn))
                {
                    sql += "order by " + ordercolumn;
                }
                else
                {
                    sql += " order by DeptName ";
                }
                if (!string.IsNullOrEmpty(ordertype))
                {
                    sql += "  " + ordertype;
                }

            }
            else
            {
                sql += "select n.EmployeeName,y.DeptName,t.Seller,t.DeptID,isnull(Convert(numeric(20," + SelPoint + "),t.CountTotal),0)CountTotal,                                        ";
                sql += "isnull(Convert(numeric(20," + SelPoint + "),t.RealTotal),0)RealTotal,isnull(Convert(numeric(20," + SelPoint + "),0-t.BackCountTotal),0)BackCountTotal,          ";
                sql += "isnull(Convert(numeric(20," + SelPoint + "),0-t.BackRealTota),0)BackRealTota  from                                                               ";
                sql += "(SELECT isnull(a.Seller,b.Seller)Seller,isnull(a.DeptID,b.DeptID)DeptID,a.CountTotal,a.RealTotal,isnull(b.BackCountTotal,0)BackCountTotal,                     ";
                sql += "isnull(b.BackRealTota,0)BackRealTota                                                                                           ";
                sql += " FROM                                                                                                                          ";
                sql += "(select Seller,DeptID,sum(CountTotal)CountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)RealTotal from officedba.SubSellOrder where OrderDate>=@Querytime     ";
                sql += "and OrderDate<=@QueryEndtime and CompanyCD=@CompanyCD and (BillStatus!='1' and BillStatus!='3') ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += "and DeptID=@Dept ";
                sql += "group by Seller,DeptID) a      ";
                sql += "full JOIN                                                                                                                      ";
                sql += "(select DeptID, Seller,sum(CountTotal)BackCountTotal,sum(isnull(TotalPrice,0)*isnull(Discount,0)/100)BackRealTota from officedba.SubSellBack where BackDate>=@Querytime ";
                sql += "and BackDate<=@QueryEndtime  and CompanyCD=@CompanyCD and (BillStatus!='1' and BillStatus!='3') ";
                if (!string.IsNullOrEmpty(Dept))
                    sql += "and DeptID=@Dept";
                sql += " group by Seller,DeptID) b                                                                                  ";
                sql += "on a.Seller=b.Seller)t left join officedba.EmployeeInfo n on n.ID=t.Seller left join officedba.DeptInfo y on y.ID=t.DeptID                                     ";
                if (!string.IsNullOrEmpty(Flag))
                    sql += "where t.Seller=@Flag ";
                if (!string.IsNullOrEmpty(ordercolumn))
                {
                    sql += "order by " + ordercolumn;
                }
                else
                {
                    sql += " order by Seller ";
                }
                if (!string.IsNullOrEmpty(ordertype))
                {
                    sql += "  " + ordertype;
                }
            }

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@QueryEndtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            if (Flag != "")
                arr.Add(new SqlParameter("@Flag", Flag));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql, arr);
            return dt;

        }
        /// <summary>
        /// 门店销售明细(查询)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoByDept(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select k.*,isnull(y.DeptName,'')DeptName from(select t.*,x.CodeName from                                                                                          ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine(" select distinct flag='销售订单',c.ID ,c.UnitID,c.DeptID,isnull( CONVERT(CHAR(10), e.OrderDate, 23),'') as OrderDate,d.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from                 ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine(" select b.OrderNo, b.ProductID,sum(b.ProductCount) ProductCount,                                                             ");
            sql.AppendLine("sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0))as SellTotalFee from officedba.SubSellOrderDetail                                            ");
            sql.AppendLine("b left join officedba.SubSellOrder a on a.OrderNo=b.OrderNo  where a.OrderDate>=@Querytime                                 ");
            sql.AppendLine("and a.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3') ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@Dept");
            sql.AppendLine(" group by ProductID,b.OrderNo");
            sql.AppendLine(")d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                       ");
            sql.AppendLine("and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                            ");
            sql.AppendLine("left join officedba.ProductInfo k on k.ID=d.ProductID                                                                        ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("where c.DeptID=@Dept                                                                                                       ");
            sql.AppendLine(")t  left join officedba.CodeUnitType x                                                                                       ");
            sql.AppendLine("on x.ID=t.UnitID                                                                                                             ");
            sql.AppendLine("UNION all select s.*,y. CodeName from                                                                                        ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine("select distinct flag='销售退货单', m.OrderID,m.UnitID,m.DeptID,isnull( CONVERT(CHAR(10), e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from            ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine("select b.BackNo,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                              ");
            sql.AppendLine("-convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)))as BackTotalFee from officedba.SubSellBackDetail                    ");
            sql.AppendLine("b  left join officedba.SubSellBack a on a.BackNo=b.BackNo  where a.BackDate>=@Querytime                                    ");
            sql.AppendLine("and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3') ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@Dept");
            sql.AppendLine(" group by ProductID,b.BackNo");
            sql.AppendLine(")n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                        ");
            sql.AppendLine("and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                 ");
            sql.AppendLine("left join officedba.ProductInfo k on k.ID=n.ProductID ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("  where m.DeptID=@Dept                                                ");
            sql.AppendLine(")                                                                                                                            ");
            sql.AppendLine("s                                                                                                                            ");
            sql.AppendLine("left join officedba.CodeUnitType y                                                                                           ");
            sql.AppendLine("on y.ID=s.UnitID ) as k left join officedba.DeptInfo y on k.DeptID=y.ID                                                     ");

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 门店销售明细（打印）
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoByDept(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string ordercolumn, string ordertype)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select k.*,isnull(y.DeptName,'')DeptName from(select t.*,x.CodeName from                                                                                          ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine(" select distinct flag='销售订单',c.ID ,c.UnitID,c.DeptID,isnull( CONVERT(CHAR(10), e.OrderDate, 23),'') as OrderDate,d.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from                 ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine(" select b.OrderNo, b.ProductID,sum(b.ProductCount) ProductCount,                                                             ");
            sql.AppendLine("sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0))as SellTotalFee from officedba.SubSellOrderDetail                                            ");
            sql.AppendLine("b left join officedba.SubSellOrder a on a.OrderNo=b.OrderNo  where a.OrderDate>=@Querytime                                 ");
            sql.AppendLine("and a.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@Dept");
            sql.AppendLine(" group by ProductID,b.OrderNo");
            sql.AppendLine(")d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                       ");
            sql.AppendLine("and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                            ");
            sql.AppendLine("left join officedba.ProductInfo k on k.ID=d.ProductID                                                                        ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("where c.DeptID=@Dept                                                                                                       ");
            sql.AppendLine(")t  left join officedba.CodeUnitType x                                                                                       ");
            sql.AppendLine("on x.ID=t.UnitID                                                                                                             ");
            sql.AppendLine("UNION all select s.*,y. CodeName from                                                                                        ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine("select distinct flag='销售退货单', m.OrderID,m.UnitID,m.DeptID,isnull( CONVERT(CHAR(10), e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from            ");
            sql.AppendLine("(                                                                                                                            ");
            sql.AppendLine("select b.BackNo,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                              ");
            sql.AppendLine("-convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)))as BackTotalFee from officedba.SubSellBackDetail                    ");


            sql.AppendLine("b  left join officedba.SubSellBack a on a.BackNo=b.BackNo  where a.BackDate>=@Querytime                                    ");
            sql.AppendLine("and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@Dept");
            sql.AppendLine(" group by ProductID,b.BackNo");
            sql.AppendLine(")n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                        ");
            sql.AppendLine("and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                 ");
            sql.AppendLine("left join officedba.ProductInfo k on k.ID=n.ProductID ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("  where m.DeptID=@Dept                                                ");
            sql.AppendLine(")                                                                                                                            ");
            sql.AppendLine("s                                                                                                                            ");
            sql.AppendLine("left join officedba.CodeUnitType y                                                                                           ");
            sql.AppendLine("on y.ID=s.UnitID ) as k left join officedba.DeptInfo y on k.DeptID=y.ID                                                     ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql.AppendLine("order by " + ordercolumn);
                //sql += "order by " + ordercolumn;
                ////sql+="(" order by " + ordercolumn)";
            }
            else
            {
                sql.AppendLine("order by OrderNo");
                //sql += " order by DeptName ";
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(" " + ordertype);
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        ///业务员销售明细（查询）
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoBySeller(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct y.*,w.DeptName,z.EmployeeName from                                                                                                    ");
            searchSql.AppendLine("(                                                                                                                                                     ");
            searchSql.AppendLine(" select t.*,x.CodeName from                                                                                                                           ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("             select distinct flag='销售订单',c.DeptID ,c.UnitID,isnull( CONVERT(CHAR(10), e.OrderDate, 23),'') as OrderDate,d.*,                                                                      ");
            searchSql.AppendLine("isnull(k.Specification,'')Specification ,                                                                                                             ");
            searchSql.AppendLine("k.ProductName,k.ProdNo from                                                                                                                           ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("             select b.OrderNo,a.Seller, b.ProductID,sum(b.ProductCount) ProductCount,                                                                 ");
            searchSql.AppendLine("            sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0))as SellTotalFee from officedba.SubSellOrderDetail                                                         ");
            searchSql.AppendLine("            b left join officedba.SubSellOrder a on a.OrderNo=b.OrderNo  where a.OrderDate>=@Querytime                                           ");
            searchSql.AppendLine("            and a.OrderDate<=@Endtime and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" and b.DeptID=@DeptID ");
            searchSql.AppendLine("and b.CompanyCD =@CompanyCD                                                                  ");
            searchSql.AppendLine("            group by ProductID,b.OrderNo,a.Seller                                                                                                          ");
            searchSql.AppendLine("            )d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                                    ");
            searchSql.AppendLine("            and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                                         ");
            searchSql.AppendLine("            left join officedba.ProductInfo k on k.ID=d.ProductID                                                                                     ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine("            where c.DeptID=@DeptID                                                                                                                        ");
            searchSql.AppendLine("            )t  left join officedba.CodeUnitType x                                                                                                    ");
            searchSql.AppendLine("            on x.ID=t.UnitID                                                                                                                          ");
            searchSql.AppendLine("            UNION all select s.*,y. CodeName from                                                                                                     ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("            select distinct flag='销售退货单',m.DeptID,m.UnitID,isnull( CONVERT(CHAR(10), e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from   ");
            searchSql.AppendLine("            (                                                                                                                                           ");
            searchSql.AppendLine("            select b.BackNo,a.Seller,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                                    ");
            searchSql.AppendLine("            -convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)))as BackTotalFee from officedba.SubSellBackDetail                                   ");



            searchSql.AppendLine("            b  left join officedba.SubSellBack a on a.BackNo=b.BackNo  where a.BackDate>=@Querytime                                                   ");
            searchSql.AppendLine("            and a.BackDate<=@Endtime and (a.BillStatus!='1' and a.BillStatus!='3') ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" and  b.DeptID=@DeptID ");
            searchSql.AppendLine(" and b.CompanyCD = @CompanyCD group by ProductID,b.BackNo ,a.Seller                              ");
            searchSql.AppendLine("            )n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                                       ");
            searchSql.AppendLine("            and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                                ");
            searchSql.AppendLine("            left join officedba.ProductInfo k on k.ID=n.ProductID  ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" where m.DeptID=@DeptID                                                                    ");
            searchSql.AppendLine("            )                                                                                                                                           ");
            searchSql.AppendLine("            s                                                                                                                                           ");
            searchSql.AppendLine("            left join officedba.CodeUnitType y                                                                                                          ");
            searchSql.AppendLine("            on y.ID=s.UnitID                                                                                                                            ");
            searchSql.AppendLine(")y left join officedba.EmployeeInfo z on z.ID=y.Seller left join officedba.DeptInfo w on w.ID= y.DeptID ");
            if (!string.IsNullOrEmpty(Seller))
                searchSql.AppendLine(" where y.Seller=@Seller    ");

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(searchSql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        ///业务员销售明细(打印)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <returns></returns>
        public static DataTable GetSubSellDetaileInfoBySeller(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct y.*,w.DeptName,z.EmployeeName from                                                                                                    ");
            searchSql.AppendLine("(                                                                                                                                                     ");
            searchSql.AppendLine(" select t.*,x.CodeName from                                                                                                                           ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("             select distinct flag='销售订单',c.DeptID ,c.UnitID,isnull( CONVERT(CHAR(10), e.OrderDate, 23),'') as OrderDate,d.*,                                                                      ");
            searchSql.AppendLine("isnull(k.Specification,'')Specification ,                                                                                                             ");
            searchSql.AppendLine("k.ProductName,k.ProdNo from                                                                                                                           ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("             select b.OrderNo,a.Seller, b.ProductID,sum(b.ProductCount) ProductCount,                                                                 ");
            searchSql.AppendLine("            sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0))as SellTotalFee from officedba.SubSellOrderDetail                                                         ");
            searchSql.AppendLine("            b left join officedba.SubSellOrder a on a.OrderNo=b.OrderNo  where a.OrderDate>=@Querytime                                           ");
            searchSql.AppendLine("            and a.OrderDate<=@Endtime and (a.BillStatus!='1' and a.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" and b.DeptID=@DeptID ");
            searchSql.AppendLine("and b.CompanyCD =@CompanyCD                                                                  ");
            searchSql.AppendLine("            group by ProductID,b.OrderNo,a.Seller                                                                                                          ");
            searchSql.AppendLine("            )d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                                    ");
            searchSql.AppendLine("            and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                                         ");
            searchSql.AppendLine("            left join officedba.ProductInfo k on k.ID=d.ProductID                                                                                     ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine("            where c.DeptID=@DeptID                                                                                                                        ");
            searchSql.AppendLine("            )t  left join officedba.CodeUnitType x                                                                                                    ");
            searchSql.AppendLine("            on x.ID=t.UnitID                                                                                                                          ");
            searchSql.AppendLine("            UNION all select s.*,y. CodeName from                                                                                                     ");
            searchSql.AppendLine("            (                                                                                                                                         ");
            searchSql.AppendLine("            select distinct flag='销售退货单',m.DeptID,m.UnitID,isnull( CONVERT(CHAR(10), e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from   ");
            searchSql.AppendLine("            (                                                                                                                                           ");
            searchSql.AppendLine("            select b.BackNo,a.Seller,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                                    ");
            searchSql.AppendLine("            -convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)))as BackTotalFee from officedba.SubSellBackDetail                                   ");
            searchSql.AppendLine("            b  left join officedba.SubSellBack a on a.BackNo=b.BackNo  where a.BackDate>=@Querytime                                                   ");
            searchSql.AppendLine("            and a.BackDate<=@Endtime and (a.BillStatus!='1' and a.BillStatus!='3') ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" and  b.DeptID=@DeptID ");
            searchSql.AppendLine(" and b.CompanyCD = @CompanyCD group by ProductID,b.BackNo ,a.Seller                              ");
            searchSql.AppendLine("            )n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                                       ");
            searchSql.AppendLine("            and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                                ");
            searchSql.AppendLine("            left join officedba.ProductInfo k on k.ID=n.ProductID  ");
            if (!string.IsNullOrEmpty(Dept))
                searchSql.AppendLine(" where m.DeptID=@DeptID                                                                    ");
            searchSql.AppendLine("            )                                                                                                                                           ");
            searchSql.AppendLine("            s                                                                                                                                           ");
            searchSql.AppendLine("            left join officedba.CodeUnitType y                                                                                                          ");
            searchSql.AppendLine("            on y.ID=s.UnitID                                                                                                                            ");
            searchSql.AppendLine(")y left join officedba.EmployeeInfo z on z.ID=y.Seller left join officedba.DeptInfo w on w.ID= y.DeptID ");
            if (!string.IsNullOrEmpty(Seller))
                searchSql.AppendLine(" where y.Seller=@Seller    ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                searchSql.AppendLine("order by " + ordercolumn);
                //sql += "order by " + ordercolumn;
                ////sql+="(" order by " + ordercolumn)";
            }
            else
            {
                searchSql.AppendLine("order by OrderNo");
                //sql += " order by DeptName ";
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                searchSql.AppendLine(" " + ordertype);
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(searchSql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 产品销售汇总（查询）
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductTotalInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Prod, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" select * from (select t.*,r.CodeName,s.UnitID,isnull(s.Specification,'')Specification,s.ProductName,s.ProdNo");
            sql.AppendLine(" from(select isnull(c.ProductID,d.ProductID)ProductID,isnull(c.SellCount,0)SellCount,                   ");
            sql.AppendLine("isnull(c.SellTotalFee,0)SellTotalFee                                                                                                                        ");
            sql.AppendLine(",isnull(d.BackCount,0)BackCount,isnull(d.BackTotalFee,0)BackTotalFee                                                                                            ");
            sql.AppendLine(" from(                                                                          ");
            sql.AppendLine("select a.ProductID,sum(a.ProductCount)SellCount ,    ");
            sql.AppendLine("sum(isnull(a.TotalPrice,0)*isnull(m.Discount/100,0))as SellTotalFee    ");
            sql.AppendLine("from officedba.SubSellOrderDetail a left join officedba.SubSellOrder m    ");
            sql.AppendLine("on m.OrderNo=a.OrderNo  and a.CompanyCD=m.CompanyCD  ");
            sql.AppendLine("where m.OrderDate>=@Querytime and m.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (m.BillStatus!='1' and m.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("and a.DeptID=@Dept      ");
            sql.AppendLine("group by  a.ProductID    ");
            sql.AppendLine(")c full join(   ");
            sql.AppendLine("select b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,    ");
            sql.AppendLine("-convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(w.Discount/100,0)))as BackTotalFee from    ");
            sql.AppendLine("officedba.SubSellBackDetail b left join  officedba.SubSellBack w on    ");
            sql.AppendLine(" w.BackNo=b.BackNo and b.CompanyCD=w.CompanyCD   ");
            sql.AppendLine("where w.BackDate>=@Querytime and w.BackDate<=@Endtime and b.CompanyCD=@CompanyCD and (w.BillStatus!='1' and w.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("and b.DeptID=@Dept    ");
            sql.AppendLine(" group by b.ProductID )d on c.ProductID=d.ProductID)t left join officedba.ProductInfo s    ");
            sql.AppendLine(" on t.ProductID=s.ID left join officedba.CodeUnitType r on r.ID= s.UnitID) n     ");
            if (!string.IsNullOrEmpty(Prod))
                sql.AppendLine(" where n.ProductID=@ProID   ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Prod))
                arr.Add(new SqlParameter("@ProID", Prod));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 产品销售汇总（打印）
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductTotalInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Prod, string ordercolumn, string ordertype)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" select n.CodeName as UnitID,n.ProductName as DeptID,n.ProdNo as CompanyCD,n.Specification as OrderID,n.SellCount as UnitPrice,n.SellTotalFee as TaxPrice,n.BackCount as Discount ,n.BackTotalFee as TotalFee from (select t.*,r.CodeName,s.UnitID,isnull(s.Specification,'')Specification,s.ProductName,s.ProdNo");
            sql.AppendLine(" from(select isnull(c.ProductID,d.ProductID)ProductID,isnull(c.SellCount,0)SellCount,                   ");
            sql.AppendLine("isnull(c.SellTotalFee,0)SellTotalFee                                                                                                                        ");
            sql.AppendLine(",isnull(d.BackCount,0)BackCount,isnull(d.BackTotalFee,0)BackTotalFee                                                                                            ");
            sql.AppendLine(" from(                                                                          ");
            sql.AppendLine("select a.ProductID,sum(a.ProductCount)SellCount ,    ");
            sql.AppendLine("sum(isnull(a.TotalPrice,0)*isnull(m.Discount/100,0))as SellTotalFee    ");
            sql.AppendLine("from officedba.SubSellOrderDetail a left join officedba.SubSellOrder m    ");
            sql.AppendLine("on m.OrderNo=a.OrderNo    ");
            sql.AppendLine("where m.OrderDate>=@Querytime and m.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (m.BillStatus!='1' and m.BillStatus!='3')   ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("and a.DeptID=@Dept      ");
            sql.AppendLine("group by  a.ProductID    ");
            sql.AppendLine(")c full join(   ");
            sql.AppendLine("select b.ProductID,sum(b.BackCount)BackCount,    ");
            sql.AppendLine("sum(isnull(b.TotalPrice,0)*isnull(w.Discount/100,0))as BackTotalFee from    ");
            sql.AppendLine("officedba.SubSellBackDetail b left join  officedba.SubSellBack w on    ");
            sql.AppendLine(" w.BackNo=b.BackNo    ");
            sql.AppendLine("where w.BackDate>=@Querytime and w.BackDate<=@Endtime and b.CompanyCD=@CompanyCD and (w.BillStatus!='1' and w.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine("and b.DeptID=@Dept    ");
            sql.AppendLine(" group by b.ProductID )d on c.ProductID=d.ProductID)t left join officedba.ProductInfo s    ");
            sql.AppendLine(" on t.ProductID=s.ID left join officedba.CodeUnitType r on r.ID= s.UnitID) n     ");
            if (!string.IsNullOrEmpty(Prod))
                sql.AppendLine(" where n.ProductID=@ProID   ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql.AppendLine("order by " + ordercolumn);
            }
            else
            {
                sql.AppendLine("order by ProdNo");
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(" " + ordertype);
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Prod))
                arr.Add(new SqlParameter("@ProID", Prod));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 产品销售明细(查询)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductSellDetailInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string ProID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            string sql = string.Empty;
            sql += " select * from( ";
            sql += " select k.*,isnull(y.DeptName,'')DeptName from(select t.*,x.CodeName from                                                                                          ";
            sql += "(                                                                                                                            ";
            sql += " select distinct flag='销售订单',c.ID ,c.UnitID,c.DeptID,isnull( CONVERT(CHAR(10),e.OrderDate, 23),'') as OrderDate,d.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from                 ";
            sql += "(                                                                                                                            ";
            sql += " select b.OrderNo, b.ProductID,sum(b.ProductCount) ProductCount,                                                             ";
            sql += "sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)*convert(numeric,isnull(k1.Discount,100)/100,2))as SellTotalFee from officedba.SubSellOrderDetail                                            ";
            sql += "b left join officedba.SubSellOrder  a on a.OrderNo=b.OrderNo and a.CompanyCD=b.CompanyCD left join officedba.ProductInfo k1 on k1.ID=b.ProductID  where a.OrderDate>=@Querytime                                 ";
            sql += "and a.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')  ";
            if (!string.IsNullOrEmpty(Dept))
                sql += " and b.DeptID=@Dept";
            sql += " group by ProductID,b.OrderNo";
            sql += ")d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                       ";
            sql += "and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                            ";
            sql += "left join officedba.ProductInfo k on k.ID=d.ProductID                                                                        ";
            if (!string.IsNullOrEmpty(Dept))
                sql += "where c.DeptID=@Dept                                                                                                       ";
            sql += ")t  left join officedba.CodeUnitType x                                                                                       ";
            sql += "on x.ID=t.UnitID                                                                                                             ";
            sql += "UNION all select s.*,y. CodeName from                                                                                        ";
            sql += "(                                                                                                                            ";
            sql += "select distinct flag='销售退货单', m.OrderID,m.UnitID,m.DeptID,isnull( CONVERT(CHAR(10),e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from            ";
            sql += "(                                                                                                                            ";
            sql += "select b.BackNo,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                              ";
            sql += "-convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)*convert(numeric,isnull(k2.Discount,100)/100,2)))as BackTotalFee from officedba.SubSellBackDetail                    ";
            sql += "b  left join officedba.SubSellBack a on a.BackNo=b.BackNo and a.CompanyCD=b.CompanyCD left join officedba.ProductInfo k2 on k2.ID=b.ProductID  where a.BackDate>=@Querytime                                    ";
            sql += "and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD and  (a.BillStatus!='1' and a.BillStatus!='3') ";
            if (!string.IsNullOrEmpty(Dept))
                sql += " and b.DeptID=@Dept";
            sql += " group by ProductID,b.BackNo";
            sql += ")n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                        ";
            sql += "and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                 ";
            sql += "left join officedba.ProductInfo k on k.ID=n.ProductID ";
            if (!string.IsNullOrEmpty(Dept))
                sql += "  where m.DeptID=@Dept                                                ";
            sql += ")                                                                                                                            ";
            sql += "s                                                                                                                            ";
            sql += "left join officedba.CodeUnitType y                                                                                           ";
            sql += "on y.ID=s.UnitID ) as k left join officedba.DeptInfo y on k.DeptID=y.ID)p                                                                                                         ";
            if (!string.IsNullOrEmpty(ProID))
                sql += " where p.ProductID=@ProID   ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            if (!string.IsNullOrEmpty(ProID))
                arr.Add(new SqlParameter("@ProID", ProID));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 产品销售明细(打印)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable GetProductSellDetailInfo(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string ProID, string ordercolumn, string ordertype)
        {
            string sql = string.Empty;
            sql += " select ProdNo,ProductName,OrderDate,OrderNo,flag,UnitID,CodeName,Specification,ProductCount,isnull(Convert(numeric(20,4),SellTotalFee),0) as SellTotalFee from( ";
            sql += " select k.*,isnull(y.DeptName,'')DeptName from(select t.*,x.CodeName from                                                                                          ";
            sql += "(                                                                                                                            ";
            sql += " select distinct flag='销售订单',c.ID ,c.UnitID,c.DeptID,isnull( CONVERT(CHAR(10),e.OrderDate, 23),'') as OrderDate,d.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from                 ";
            sql += "(                                                                                                                            ";
            sql += " select b.OrderNo, b.ProductID,sum(b.ProductCount) ProductCount,                                                             ";
            sql += "sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)*convert(numeric,isnull(k1.Discount,100)/100,2))as SellTotalFee from officedba.SubSellOrderDetail                                            ";
            sql += "b left join officedba.SubSellOrder a on a.OrderNo=b.OrderNo left join officedba.ProductInfo k1 on k1.ID=b.ProductID  where a.OrderDate>=@Querytime                                 ";
            sql += "and a.OrderDate<=@Endtime  and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3') ";
            if (!string.IsNullOrEmpty(Dept))
                sql += " and b.DeptID=@Dept";
            sql += " group by ProductID,b.OrderNo";
            sql += ")d left join officedba.SubSellOrderDetail c on c.ProductID=d.ProductID                                                       ";
            sql += "and c.OrderNo=d.OrderNo left join officedba.SubSellOrder e on e.OrderNo=d.OrderNo                                            ";
            sql += "left join officedba.ProductInfo k on k.ID=d.ProductID                                                                        ";
            if (!string.IsNullOrEmpty(Dept))
                sql += "where c.DeptID=@Dept                                                                                                       ";
            sql += ")t  left join officedba.CodeUnitType x                                                                                       ";
            sql += "on x.ID=t.UnitID                                                                                                             ";
            sql += "UNION all select s.*,y. CodeName from                                                                                        ";
            sql += "(                                                                                                                            ";
            sql += "select distinct flag='销售退货单', m.OrderID,m.UnitID,m.DeptID,isnull( CONVERT(CHAR(10),e.BackDate, 23),'') as BackDate,n.*,isnull(k.Specification,'')Specification ,k.ProductName,k.ProdNo from            ";
            sql += "(                                                                                                                            ";
            sql += "select b.BackNo,b.ProductID,-convert(NUMERIC,sum(b.BackCount))BackCount,                                              ";
            sql += "-convert(NUMERIC,sum(isnull(b.TotalPrice,0)*isnull(a.Discount/100,0)*convert(numeric,isnull(k2.Discount,100)/100,2)))as BackTotalFee from officedba.SubSellBackDetail                    ";
            sql += "b  left join officedba.SubSellBack a on a.BackNo=b.BackNo left join officedba.ProductInfo k2 on k2.ID=b.ProductID  where a.BackDate>=@Querytime                                    ";
            sql += "and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3') ";
            if (!string.IsNullOrEmpty(Dept))
                sql += " and b.DeptID=@Dept";
            sql += " group by ProductID,b.BackNo";
            sql += ")n left join officedba.SubSellBackDetail m on m.ProductID=n.ProductID                                                        ";
            sql += "and m.BackNo=n.BackNo left join officedba.SubSellBack e on e.BackNo=n.BackNo                                                 ";
            sql += "left join officedba.ProductInfo k on k.ID=n.ProductID ";
            if (!string.IsNullOrEmpty(Dept))
                sql += "  where m.DeptID=@Dept                                                ";
            sql += ")                                                                                                                            ";
            sql += "s                                                                                                                            ";
            sql += "left join officedba.CodeUnitType y                                                                                           ";
            sql += "on y.ID=s.UnitID ) as k left join officedba.DeptInfo y on k.DeptID=y.ID)p                                                                                                         ";
            if (!string.IsNullOrEmpty(ProID))
                sql += " where p.ProductID=@ProID   ";
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql += "order by " + ordercolumn;
            }
            else
            {
                sql += " order by OrderNo ";
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql += "  " + ordertype;
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@Dept", Dept));
            if (!string.IsNullOrEmpty(ProID))
                arr.Add(new SqlParameter("@ProID", ProID));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 收款员业务汇总(查询)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <param name="Prod"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusTotal(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select m.*,n.DeptName,x.EmployeeName from(select b.Seller,b.DeptID,                         ");
            sql.AppendLine("sum((isnull(b.RealTotal,0)-isnull(b.WairPayTotal,0))) as SellFee                            ");
            sql.AppendLine(" from  officedba.SubSellOrder b where b.OrderDate>=@Querytime and b.OrderDate<=@Endtime  and b.CompanyCD=@CompanyCD and (b.BillStatus!='1' and b.BillStatus!='3')  ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and b.Seller=@Seller");
            sql.AppendLine("group by b.seller,b.DeptID                                                                  ");
            sql.AppendLine("union all                                                                                   ");
            sql.AppendLine("select a.Seller,a.DeptID,                                                                   ");
            sql.AppendLine("-convert(NUMERIC,sum((isnull(a.RealTotal,0)- isnull(a.WairPayTotal,0) +isnull(a.PayedTotal,0)))) as BackFee   ");
            sql.AppendLine(" from  officedba.SubSellBack a where a.BackDate>=@Querytime and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD and (a.BillStatus!='1' and a.BillStatus!='3')       ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and a.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and a.Seller=@Seller");
            sql.AppendLine(" group by a.seller,DeptID)m left join officedba.DeptInfo n on n.ID=m.DeptID left join       ");
            sql.AppendLine("officedba.EmployeeInfo x on x.ID=m.Seller                                                   ");

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 收款员业务汇总(打印)
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <param name="Prod"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusTotal(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select m.SellFee as RealTotal,n.DeptName as DeptID,x.EmployeeName as Seller from(select b.Seller,b.DeptID,                         ");
            sql.AppendLine("sum((isnull(b.RealTotal,0)-isnull(b.WairPayTotal,0))) as SellFee                            ");
            sql.AppendLine(" from  officedba.SubSellOrder b where b.OrderDate>=@Querytime and b.OrderDate<=@Endtime  and b.CompanyCD=@CompanyCD and (b.BillStatus!='1' and b.BillStatus!='3')   ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and b.Seller=@Seller");
            sql.AppendLine("group by b.seller,b.DeptID                                                                  ");
            sql.AppendLine("union all                                                                                   ");
            sql.AppendLine("select a.Seller,a.DeptID,                                                                   ");
            sql.AppendLine("-convert(NUMERIC,sum((isnull(a.RealTotal,0)-isnull(a.WairPayTotal,0) +isnull(a.PayedTotal,0)))) as BackFee   ");
            sql.AppendLine(" from  officedba.SubSellBack a where a.BackDate>=@Querytime and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD  and (a.BillStatus!='1' and a.BillStatus!='3')     ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and a.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and a.Seller=@Seller");
            sql.AppendLine(" group by a.seller,DeptID)m left join officedba.DeptInfo n on n.ID=m.DeptID left join       ");
            sql.AppendLine("officedba.EmployeeInfo x on x.ID=m.Seller                                                   ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql.AppendLine("order by " + ordercolumn);
            }
            else
            {
                sql.AppendLine("order by EmployeeName");
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(" " + ordertype);
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        /// <summary>
        /// 收款员业务明细
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <param name="Prod"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusDetail(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select m.flag as isOpenbill,m.OrderNo,isnull( CONVERT(CHAR(19), m.OrderDate, 120),'') as OrderDate,m.SellFee as RealTotal,n.DeptName as DeptID,x.EmployeeName as Seller from(                                                                  ");
            sql.AppendLine("select flag='销售订单',b.Seller,b.DeptID,b.OrderNo,b.OrderDate,                                             ");
            sql.AppendLine("sum((isnull(b.RealTotal,0)-isnull(b.WairPayTotal,0))) as SellFee                                            ");
            sql.AppendLine(" from  officedba.SubSellOrder b where b.OrderDate>=@Querytime and b.OrderDate<=@Endtime and b.CompanyCD=@CompanyCD  and (b.BillStatus!='1' and b.BillStatus!='3')              ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and b.Seller=@Seller");
            sql.AppendLine("group by b.seller,b.DeptID,b.OrderNo ,b.OrderDate                                                           ");
            sql.AppendLine("union all                                                                                                   ");
            sql.AppendLine("select flag='销售退货单', a.Seller,a.DeptID,a.BackNo,a.BackDate,                                            ");
            sql.AppendLine("-convert(NUMERIC,sum((isnull(a.RealTotal,0)-isnull(a.WairPayTotal,0) +isnull(a.PayedTotal,0)))) as BackFee                   ");
            sql.AppendLine(" from  officedba.SubSellBack a where a.BackDate>=@Querytime and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD   and (a.BillStatus!='1' and a.BillStatus!='3')                ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and a.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and a.Seller=@Seller");
            sql.AppendLine("  group by a.seller,a.DeptID,a.BackNo,a.BackDate)m left join officedba.DeptInfo n on n.ID=m.DeptID left join ");
            sql.AppendLine(" officedba.EmployeeInfo x on x.ID=m.Seller                                                                   ");
            //if (!string.IsNullOrEmpty(OrderBy))
            //{
            //    sql.AppendLine(" order by " + OrderBy);
            //}
            //else
            //{
            //    sql.AppendLine(" order by DeptID");
            //}
            //if (!string.IsNullOrEmpty(ordertype))
            //{
            //    sql.AppendLine(ordertype);
            //}


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(sql.ToString(), pageIndex, pageCount, OrderBy, arr, ref totalCount);
            return dt;
        }

        /// <summary>
        /// 收款员业务明细
        /// </summary>
        /// <param name="Querytime"></param>
        /// <param name="Endtime"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="Dept"></param>
        /// <param name="Prod"></param>
        /// <returns></returns>
        public static DataTable GetSellerBusDetail(DateTime Querytime, DateTime Endtime, string strCompanyCD, string Dept, string Seller, string ordercolumn, string ordertype)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select m.flag as isOpenbill,m.OrderNo,isnull( CONVERT(CHAR(10), m.OrderDate, 120),'') as OrderDate,m.SellFee as RealTotal,n.DeptName as DeptID,x.EmployeeName as Seller from(                                                                  ");
            sql.AppendLine("select flag='销售订单',b.Seller,b.DeptID,b.OrderNo,b.OrderDate,                                             ");
            sql.AppendLine("sum((isnull(b.RealTotal,0)-isnull(b.WairPayTotal,0))) as SellFee                                            ");
            sql.AppendLine(" from  officedba.SubSellOrder b where b.OrderDate>=@Querytime and b.OrderDate<=@Endtime and b.CompanyCD=@CompanyCD  and (b.BillStatus!='1' and b.BillStatus!='3')              ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and b.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and b.Seller=@Seller");
            sql.AppendLine("group by b.seller,b.DeptID,b.OrderNo ,b.OrderDate                                                           ");
            sql.AppendLine("union all                                                                                                   ");
            sql.AppendLine("select flag='销售退货单', a.Seller,a.DeptID,a.BackNo,isnull( CONVERT(CHAR(10), a.BackDate, 120),'') as BackDate,                                            ");
            sql.AppendLine("-convert(NUMERIC,sum((isnull(a.RealTotal,0)-isnull(a.WairPayTotal,0) +isnull(a.PayedTotal,0)))) as BackFee                   ");
            sql.AppendLine(" from  officedba.SubSellBack a where a.BackDate>=@Querytime and a.BackDate<=@Endtime and a.CompanyCD=@CompanyCD    and (a.BillStatus!='1' and a.BillStatus!='3')               ");
            if (!string.IsNullOrEmpty(Dept))
                sql.AppendLine(" and a.DeptID=@DeptID");
            if (!string.IsNullOrEmpty(Seller))
                sql.AppendLine(" and a.Seller=@Seller");
            sql.AppendLine("  group by a.seller,a.DeptID,a.BackNo,a.BackDate)m left join officedba.DeptInfo n on n.ID=m.DeptID left join ");
            sql.AppendLine(" officedba.EmployeeInfo x on x.ID=m.Seller                                                                   ");
            if (!string.IsNullOrEmpty(ordercolumn))
            {
                sql.AppendLine("order by " + ordercolumn);
            }
            else
            {
                sql.AppendLine("order by DeptID");
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                sql.AppendLine(" " + ordertype);
            }


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@Querytime", Querytime));
            arr.Add(new SqlParameter("@Endtime", Endtime));
            if (!string.IsNullOrEmpty(Dept))
                arr.Add(new SqlParameter("@DeptID", Dept));
            if (!string.IsNullOrEmpty(Seller))
                arr.Add(new SqlParameter("@Seller", Seller));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(sql.ToString(), arr);
            return dt;
        }
        #endregion

        public static DataRow GetSubDeptFromDeptID(string deptID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from officedba.deptinfo where id= " + deptID + " ");


            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SaleFlag"].ToString() == "1")
                {
                    return dt.Rows[0];
                }
                else
                {
                    if (dt.Rows[0]["SuperDeptID"].ToString() != "")
                    {
                        return GetSubDeptFromDeptID(dt.Rows[0]["SuperDeptID"].ToString());
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }





            //execute 

            //if subflag == 1
            // return 


            // else

            //if SuperDeptID == null
            //return null


            //return GetSubDeptFromDeptID(SuperDeptID);

        }
        //-------------------------------------------------改版报表
        #region 门店销售额走势
        /// <summary>
        /// 门店销售额走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetSubStorageSellTen(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            string ColumnName = "";
            if (Method == "1")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年'";
            }
            if (Method == "2")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年'+datename(mm,a.OrderDate)+'月'";
            }
            if (Method == "3")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年第'+datename(week,a.OrderDate)+'周'";
            }
            sql.AppendLine("  select (" + ColumnName + ") as TheDate");
            sql.AppendLine("  ,Convert(decimal(18," + SelPoint + "),sum(isnull(a.TotalPrice,0)*a.Rate*isnull(a.Discount,0)/100)) as RealTotal ");
            sql.AppendLine("  from officedba. SubSellOrder as a  ");
            sql.AppendLine("  where a.OrderDate >=@BeginDate and a.OrderDate<=@EndDate and  a.CompanyCD=@CompanyCD and a.BillStatus<>'1' and a.BillStatus<>'3'  ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(BeginDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "0")
            {
                sql.AppendLine(" and a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            sql.AppendLine("  group by " + ColumnName);
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion
        #region  门店销售额走势-查看明细
        /// <summary>
        /// 门店销售额走势-查看明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetSubStorageSellTenDetail(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            string ColumnName = "";

            if (Method == "1")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年'";
            }
            if (Method == "2")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年'+datename(mm,A.OrderDate)+'月'";
            }
            if (Method == "3")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年第'+datename(week,A.OrderDate)+'周'";
            }

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from (SELECT A.ID                     ");
            sql.AppendLine("      ,A.OrderNo                ");

            sql.AppendLine(",(" + ColumnName + ")as TheDate ");

            sql.AppendLine("      ,A.Title                  ");
            sql.AppendLine("      ,A.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,A.SendMode               ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,A.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr              ");
            sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100))+'&nbsp;' as TotalPrice ");
            //sql.AppendLine("      ,isnull(a.TotalPrice,0)*isnull(a.Discount,0)/100 as TotalPrice");
            sql.AppendLine("  FROM officedba.SubSellOrder AS A");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON A.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller=C.ID");
            sql.AppendLine(" WHERE A.BillStatus<>'1' and A.BillStatus<>'3'");
            sql.AppendLine(" AND A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and A.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(XValue.Trim()))
            {
                sql.AppendLine(" and " + ColumnName + "=@XValue");
                comm.Parameters.Add(SqlHelper.GetParameter("@XValue", XValue));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and A.OrderDate<=@EndDate ) Info");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 门店销售数量走势
        /// <summary>
        /// 门店销售数量走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static DataTable GetSubSellCountTen(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            string ColumnName = "";
            if (Method == "1")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年'";
            }
            if (Method == "2")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年'+datename(mm,a.OrderDate)+'月'";
            }
            if (Method == "3")
            {
                ColumnName = "datename(yyyy,a.OrderDate)+'年第'+datename(week,a.OrderDate)+'周'";
            }
            sql.AppendLine("  select (" + ColumnName + ") as TheDate");
            sql.AppendLine("  ,Convert(decimal(18," + SelPoint + "),sum(isnull(a.CountTotal,0))) as RealTotal ");
            sql.AppendLine("  from officedba. SubSellOrder as a  ");
            sql.AppendLine("  where a.OrderDate >=@BeginDate and a.OrderDate<=@EndDate and  a.CompanyCD=@CompanyCD and a.BillStatus<>'1' and a.BillStatus<>'3'  ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (!string.IsNullOrEmpty(BeginDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "0")
            {
                sql.AppendLine(" and a.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", DeptID));
            }
            sql.AppendLine("  group by " + ColumnName);
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 门店销售数量走势-明细
        /// <summary>
        /// 门店销售数量走势-明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Method"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptID"></param>
        /// <param name="OrderBy"></param>
        /// <param name="XValue"></param>
        /// <returns></returns>
        public static SqlCommand GetSubSellCountTenDetail(string CompanyCD, string Method, string BeginDate, string EndDate, string DeptID, string OrderBy, string XValue)
        {
            string SelPoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlCommand comm = new SqlCommand();
            string ColumnName = "";

            if (Method == "1")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年'";
            }
            if (Method == "2")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年'+datename(mm,A.OrderDate)+'月'";
            }
            if (Method == "3")
            {
                ColumnName = "datename(yyyy,A.OrderDate)+'年第'+datename(week,A.OrderDate)+'周'";
            }

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from (SELECT A.ID                     ");
            sql.AppendLine("      ,A.OrderNo                ");

            sql.AppendLine(",(" + ColumnName + ")as TheDate ");

            sql.AppendLine("      ,A.Title                  ");
            sql.AppendLine("      ,A.DeptID                 ");
            sql.AppendLine("      ,isnull(B.DeptName,'') AS DeptName ");
            sql.AppendLine("      ,A.SendMode               ");
            sql.AppendLine(",case A.SendMode when '1' then '分店发货' when '2' then '总部发货' end AS SendModeName            ");
            sql.AppendLine("      ,A.Seller                 ");
            sql.AppendLine("      ,isnull(C.EmployeeName,'') AS SellerName ");
            sql.AppendLine("      ,isnull(A.CustName,'') AS CustName              ");
            sql.AppendLine("      ,isnull(A.CustTel,'') AS  CustTel              ");
            sql.AppendLine("      ,isnull(A.CustMobile,'') AS CustMobile            ");
            sql.AppendLine("      ,isnull(A.CustAddr,'') AS CustAddr              ");

            sql.AppendLine("      ,Convert(char(20),Convert(numeric(18," + SelPoint + "),isnull(a.CountTotal,0)))+'&nbsp;' as TotalCount ");
            sql.AppendLine("  FROM officedba.SubSellOrder AS A");
            sql.AppendLine(" LEFT JOIN officedba.DeptInfo AS B ON A.DeptID=B.ID  ");
            sql.AppendLine(" LEFT JOIN officedba.EmployeeInfo AS C ON A.Seller=C.ID");
            sql.AppendLine(" WHERE A.BillStatus<>'1' and A.BillStatus<>'3'");
            sql.AppendLine(" AND A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            if (DeptID != "" && DeptID != "0")
            {
                sql.AppendLine(" AND A.DeptID = @DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                sql.AppendLine(" and A.OrderDate>=@BeginDate");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginDate", BeginDate));
            }
            if (!string.IsNullOrEmpty(XValue.Trim()))
            {
                sql.AppendLine(" and " + ColumnName + "=@XValue");
                comm.Parameters.Add(SqlHelper.GetParameter("@XValue", XValue));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql.AppendLine(" and A.OrderDate<=@EndDate ) Info");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndDate", EndDate));
            }
            #endregion
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 分店期初批量导入

        /// <summary>
        /// 产品
        /// </summary>
        struct Product
        {
            /// <summary>
            /// 产品ID
            /// </summary>
            public int id;
            /// <summary>
            /// 产品批次
            /// </summary>
            public bool isBatchNo;
        }

        /// <summary>
        /// 批量导入数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        public static bool ImportData(DataTable dt, UserInfoUtil userInfo)
        {
            ArrayList list = new ArrayList();
            string temp = "";
            decimal count = 0m;
            Dictionary<string, Product> dicProduct = new Dictionary<string, Product>();
            Dictionary<string, int> dicDept = new Dictionary<string, int>();
            foreach (DataRow dr in dt.Rows)
            {
                #region 添加门店库存流水帐
                SubStorageAccountModel aModel = new SubStorageAccountModel();
                aModel.BillNo = "";
                aModel.BillType = 8;
                aModel.CompanyCD = userInfo.CompanyCD;
                aModel.Creator = userInfo.EmployeeID;
                aModel.HappenDate = DateTime.Now;
                aModel.Price = 0;
                temp = dr["物品编号"].ToString().Trim();
                if (!dicProduct.ContainsKey(temp))
                {// 物品编号
                    foreach (DataRow item in GetProductIDWithProdNo(userInfo.CompanyCD, temp).Rows)
                    {
                        Product p = new Product();
                        p.id = int.Parse(item["ID"].ToString());
                        p.isBatchNo = item["IsBatchNo"].ToString() == "1";
                        dicProduct.Add(temp, p);
                    }
                }
                aModel.BatchNo = dicProduct[temp].isBatchNo ? dr["批次"].ToString() : "";
                aModel.ProductID = dicProduct[temp].id;
                temp = dr["分店名称"].ToString().Trim();
                if (!dicDept.ContainsKey(temp))
                {// 分店名称
                    foreach (DataRow item in GetDeptIDWithDeptName(userInfo.CompanyCD, temp).Rows)
                    {
                        dicDept.Add(temp, int.Parse(item["ID"].ToString()));
                    }
                }
                aModel.DeptID = dicDept[temp];
                if (decimal.TryParse(dr["现有存量"].ToString(), out count))
                {
                    aModel.HappenCount = count;
                }
                list.Add(XBase.Data.Office.SubStoreManager.SubStorageAccountDBHelper.GetCountAndInsertCommand(aModel));
                #endregion
                // 更新分店存量表
                list.Add(StorageProductQueryDBHelper.UpdateProductCount(userInfo.CompanyCD
                    , aModel.ProductID.ToString(), aModel.DeptID.ToString(), aModel.BatchNo, count));
            }
            return SqlHelper.ExecuteTransWithArrayList(list);
        }

        /// <summary>
        /// 根据物品编号获得物品ID
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="ProdNo">物品编号</param>
        /// <returns></returns>
        public static DataTable GetProductIDWithProdNo(string CompanyCD, string ProdNo)
        {
            string sqlStr = @"SELECT pi1.ID,pi1.IsBatchNo
                                FROM officedba.ProductInfo pi1
                                WHERE pi1.CompanyCD=@CompanyCD AND pi1.CheckStatus=1 AND pi1.ProdNo=@ProdNo";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ProdNo",SqlDbType.VarChar)
                                   };
            paras[0].Value = CompanyCD;
            paras[1].Value = ProdNo;
            return SqlHelper.ExecuteSql(sqlStr, paras);
        }

        /// <summary>
        /// 根据部门名称获得ID
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="DeptName">分店</param>
        /// <returns></returns>
        public static DataTable GetDeptIDWithDeptName(string CompanyCD, string DeptName)
        {
            string sqlStr = @"SELECT di.ID 
                                FROM officedba.DeptInfo di
                                WHERE di.CompanyCD=@CompanyCD AND di.SaleFlag=1 AND di.DeptName=@DeptName";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@DeptName",SqlDbType.VarChar)
                                   };
            paras[0].Value = CompanyCD;
            paras[1].Value = DeptName;
            return SqlHelper.ExecuteSql(sqlStr, paras);
        }

        #endregion

    }


}
