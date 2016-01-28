using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Office.StorageManager
{
    public class StorageAdjustDBHelper
    {
        public static DataTable GetStorage(string CompanyCD)
        {
            string sql = "select * from officedba.StorageInfo where UsedStatus='1' and CompanyCD='" + CompanyCD + "'";
            //过滤单据：显示当前用户拥有权限查看的单据
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetReason(string Flag, string CompanyCD)
        {
            string sql = "select * from officedba.CodeReasonType where Flag='" + Flag + "' and UsedStatus='1' and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static bool AddAdjust(StorageAdjustModel model, List<StorageAdjustDetail> detail, Hashtable ht)
        {
            ArrayList sqllist = new ArrayList();
            string sql = "";
            #region 主表---Start
            sql += " INSERT INTO [officedba].[StorageAdjust]";
            sql += "            ([CompanyCD]                        ";
            sql += "            ,[AdjustNo]                         ";
            sql += "            ,[StorageID]                        ";
            sql += "            ,[ReasonType]                       ";
            sql += "            ,[Executor]                         ";
            sql += "            ,[DeptID]                           ";
            if (model.AdjustDate != Convert.ToDateTime("9999-9-9"))
            {
                sql += "            ,[AdjustDate]                       ";
            }
            if (model.TotalPrice != 0)
            {
                sql += "            ,[TotalPrice]                       ";
            }
            if (model.CountTotal != 0)
            {
                sql += "            ,[CountTotal]                       ";
            }
            if (!string.IsNullOrEmpty(model.Summary))
            {
                sql += "            ,[Summary]                      ";
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql += "            ,[Remark]                       ";
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql += "            ,[Attachment]                   ";
            }
            sql += "            ,[Creator]                          ";
            sql += "            ,[CreateDate]                       ";
            sql += "            ,[BillStatus]                       ";
            sql += "            ,[ModifiedDate]                     ";
            sql += "            ,[Title]";
            sql += "            ,[ModifiedUserID])                  ";
            sql += "      VALUES                                    ";
            sql += "            (@CompanyCD                         ";
            sql += "            ,@AdjustNo                          ";
            sql += "            ,@StorageID                         ";
            sql += "            ,@ReasonType                        ";
            sql += "            ,@Executor                          ";
            sql += "            ,@DeptID                            ";
            if (model.AdjustDate != Convert.ToDateTime("9999-9-9"))
            {
                sql += "            ,@AdjustDate                        ";
            }
            if (model.TotalPrice != 0)
            {
                sql += "            ,@TotalPrice                        ";
            }
            if (model.CountTotal != 0)
            {
                sql += "            ,@CountTotal                        ";
            }
            if (!string.IsNullOrEmpty(model.Summary))
            {
                sql += "            ,@Summary                       ";
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql += "            ,@Remark                        ";
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql += "            ,@Attachment                    ";
            }
            sql += "            ,@Creator                           ";
            sql += "            ,@CreateDate                        ";
            sql += "            ,@BillStatus                        ";
            sql += "            ,@ModifiedDate                      ";
            sql += "            ,@Title                             ";
            sql += "            ,@ModifiedUserID)                    ";

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@AdjustNo", model.AdjustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReasonType", model.ReasonType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Executor", model.Executor));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            if (model.AdjustDate != Convert.ToDateTime("9999-9-9"))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AdjustDate", model.AdjustDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            if (model.TotalPrice != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            }
            if (model.CountTotal != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));
            }
            if (!string.IsNullOrEmpty(model.Summary))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Summary", model.Summary));
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));

            sqllist.Add(comm);
            #endregion
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, ht, cmd);
            if (ht.Count > 0)
                sqllist.Add(cmd);
            #endregion
            #region 明细---Start
            StorageAdjustDetail detailmodel = new StorageAdjustDetail();
            if (!string.IsNullOrEmpty(model.AdjustNo))
            {
                for (int i = 0; i < detail.Count; i++)
                {
                    string detailsql = "";
                    detailsql += "";
                    detailsql += " INSERT INTO [officedba].[StorageAdjustDetail]  ";
                    detailsql += "            ([CompanyCD]                                ";
                    detailsql += "            ,[AdjustNo]                                 ";
                    detailsql += "            ,[SortNo]                                   ";
                    detailsql += "            ,[ProductID]                                ";
                    detailsql += "            ,[UnitID]                                   ";
                    detailsql += "            ,[AdjustType]                               ";
                    detailsql += "            ,[AdjustCount]                              ";
                    detailsql += "            ,[CostPrice]                                ";
                    detailsql += "            ,[CostPriceTotal]                           ";
                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailsql += "            ,[Remark]                               ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].BatchNo))
                    {
                        detailsql += "            ,[BatchNo]                               ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                    {
                        detailsql += "            ,[UsedUnitID]                               ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                    {
                        detailsql += "            ,[UsedUnitCount]                               ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                    {
                        detailsql += "            ,[UsedPrice]                               ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].ExRate))
                    {
                        detailsql += "            ,[ExRate]                               ";
                    }
                    detailsql += "            ,[ModifiedDate]                             ";
                    detailsql += "            ,[ModifiedUserID])                          ";
                    detailsql += "      VALUES                                            ";
                    detailsql += "            (@CompanyCD                                 ";
                    detailsql += "            ,@AdjustNo                                  ";
                    detailsql += "            ,@SortNo                                    ";
                    detailsql += "            ,@ProductID                                 ";
                    detailsql += "            ,@UnitID                                    ";
                    detailsql += "            ,@AdjustType                                ";
                    detailsql += "            ,@AdjustCount                               ";
                    detailsql += "            ,@CostPrice                                 ";
                    detailsql += "            ,@CostPriceTotal                            ";
                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailsql += "            ,@Remark                                ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].BatchNo))
                    {
                        detailsql += "            ,@BatchNo                                ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                    {
                        detailsql += "            ,@UsedUnitID                                ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                    {
                        detailsql += "            ,@UsedUnitCount                                ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                    {
                        detailsql += "            ,@UsedPrice                                ";
                    }
                    if (!string.IsNullOrEmpty(detail[i].ExRate))
                    {
                        detailsql += "            ,@ExRate                                ";
                    }

                    detailsql += "            ,@ModifiedDate                              ";
                    detailsql += "            ,@ModifiedUserID)                           ";
                    SqlCommand detailcomm = new SqlCommand();
                    detailcomm.CommandText = detailsql;
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustNo", model.AdjustNo));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detail[i].SortNo));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detail[i].ProductID));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detail[i].UnitID));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustType", detail[i].AdjustType));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustCount", detail[i].AdjustCount));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@CostPrice", detail[i].CostPrice));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@CostPriceTotal", detail[i].CostPriceTotal));

                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detail[i].Remark));
                    }
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    if (!string.IsNullOrEmpty(detail[i].BatchNo))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@BatchNo", detail[i].BatchNo));
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", detail[i].UsedUnitID));
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", detail[i].UsedUnitCount));
                    }
                    if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", detail[i].UsedPrice));
                    }
                    if (!string.IsNullOrEmpty(detail[i].ExRate))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", detail[i].ExRate));
                    }
                    sqllist.Add(detailcomm);

                }
            }
            #endregion
            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public static bool UpdateAdjust(StorageAdjustModel model, List<StorageAdjustDetail> detail, string[] SortID, Hashtable ht)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//[待修改]
            ArrayList sqllist = new ArrayList();
            //string loginUserID = "admin123";
            if (string.IsNullOrEmpty(model.AdjustNo))
            {
                return false;
            }
            #region 基本信息
            string sql = "";
            sql += " UPDATE [officedba].[StorageAdjust]          ";
            sql += "    SET [StorageID] = @StorageID             ";
            sql += "       ,[ReasonType] = @ReasonType           ";
            sql += "       ,[Executor] = @Executor                ";
            sql += "       ,[DeptID] = @DeptID                 ";

            sql += "       ,[AdjustDate] = @AdjustDate       ";

            sql += "       ,[TotalPrice] = @TotalPrice      ";
            sql += "       ,[CountTotal] = @CountTotal        ";

            sql += "       ,[Summary] = @Summary        ";

            sql += "       ,[Remark] = @Remark         ";



            sql += "       ,[Attachment] = @Attachment  ";

            sql += "       ,[ModifiedDate] = @ModifiedDate               ";
            sql += "       ,[ModifiedUserID] = @ModifiedUserID           ";
            sql += "       ,[Title] = @Title                             ";
            sql += "  WHERE ID=@ID                                ";
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReasonType", model.ReasonType));
            comm.Parameters.Add(SqlHelper.GetParameter("@Executor", model.Executor));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            if (model.AdjustDate != Convert.ToDateTime("9999-9-9"))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AdjustDate", model.AdjustDate));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AdjustDate",System.Data.SqlTypes.SqlDateTime.Null));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalPrice", model.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameter("@CountTotal", model.CountTotal));

            comm.Parameters.Add(SqlHelper.GetParameter("@Summary", model.Summary));



            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));


            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));

            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sql;
            sqllist.Add(comm);
            #endregion
            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, ht, cmd);
            if (ht.Count > 0)
                sqllist.Add(cmd);
            #endregion
            #region 明细
            if (SortID.Length > 0)
            {
                string delsql = "DELETE FROM [officedba].[StorageAdjustDetail] where CompanyCD=@CompanyCD and AdjustNo=@AdjustNo ";
                SqlCommand delcomm = new SqlCommand();
                delcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                delcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustNo", model.AdjustNo));
                delcomm.CommandText = delsql;
                sqllist.Add(delcomm);
                if (SortID[0] != null && SortID[0] != "")
                {
                    for (int i = 0; i < detail.Count; i++)
                    {
                        string detailsql = "";
                        detailsql += "";
                        detailsql += " INSERT INTO [officedba].[StorageAdjustDetail]  ";
                        detailsql += "            ([CompanyCD]                                ";
                        detailsql += "            ,[AdjustNo]                                 ";
                        detailsql += "            ,[SortNo]                                   ";
                        detailsql += "            ,[ProductID]                                ";
                        detailsql += "            ,[UnitID]                                   ";
                        detailsql += "            ,[AdjustType]                               ";
                        detailsql += "            ,[AdjustCount]                              ";
                        detailsql += "            ,[CostPrice]                                ";
                        detailsql += "            ,[CostPriceTotal]                           ";
                        if (!string.IsNullOrEmpty(detail[i].Remark))
                        {
                            detailsql += "            ,[Remark]                               ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].BatchNo))
                        {
                            detailsql += "            ,[BatchNo]                               ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                        {
                            detailsql += "            ,[UsedUnitID]                               ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                        {
                            detailsql += "            ,[UsedUnitCount]                               ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                        {
                            detailsql += "            ,[UsedPrice]                               ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].ExRate))
                        {
                            detailsql += "            ,[ExRate]                               ";
                        }
                        detailsql += "            ,[ModifiedDate]                             ";
                        detailsql += "            ,[ModifiedUserID])                          ";
                        detailsql += "      VALUES                                            ";
                        detailsql += "            (@CompanyCD                                 ";
                        detailsql += "            ,@AdjustNo                                  ";
                        detailsql += "            ,@SortNo                                    ";
                        detailsql += "            ,@ProductID                                 ";
                        detailsql += "            ,@UnitID                                    ";
                        detailsql += "            ,@AdjustType                                ";
                        detailsql += "            ,@AdjustCount                               ";
                        detailsql += "            ,@CostPrice                                 ";
                        detailsql += "            ,@CostPriceTotal                            ";
                        if (!string.IsNullOrEmpty(detail[i].Remark))
                        {
                            detailsql += "            ,@Remark                                ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].BatchNo))
                        {
                            detailsql += "            ,@BatchNo                                ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                        {
                            detailsql += "            ,@UsedUnitID                                ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                        {
                            detailsql += "            ,@UsedUnitCount                                ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                        {
                            detailsql += "            ,@UsedPrice                                ";
                        }
                        if (!string.IsNullOrEmpty(detail[i].ExRate))
                        {
                            detailsql += "            ,@ExRate                                ";
                        }

                        detailsql += "            ,@ModifiedDate                              ";
                        detailsql += "            ,@ModifiedUserID)                           ";
                        SqlCommand detailcomm = new SqlCommand();
                        detailcomm.CommandText = detailsql;
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustNo", model.AdjustNo));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detail[i].SortNo));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detail[i].ProductID));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detail[i].UnitID));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustType", detail[i].AdjustType));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@AdjustCount", detail[i].AdjustCount));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@CostPrice", detail[i].CostPrice));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@CostPriceTotal", detail[i].CostPriceTotal));

                        if (!string.IsNullOrEmpty(detail[i].Remark))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detail[i].Remark));
                        }
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                        if (!string.IsNullOrEmpty(detail[i].BatchNo))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@BatchNo", detail[i].BatchNo));
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitID))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitID", detail[i].UsedUnitID));
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedUnitCount))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedUnitCount", detail[i].UsedUnitCount));
                        }
                        if (!string.IsNullOrEmpty(detail[i].UsedPrice))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@UsedPrice", detail[i].UsedPrice));
                        }
                        if (!string.IsNullOrEmpty(detail[i].ExRate))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@ExRate", detail[i].ExRate));
                        }
                        sqllist.Add(detailcomm);
                    }
                }
            }
            #endregion


            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DelAdjust(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.StorageAdjustDetail where CompanyCD=@CompanyCD and AdjustNo=(select top 1 AdjustNo from officedba.StorageAdjust where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.StorageAdjust where CompanyCD=@CompanyCD and ID=@ID");

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
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }

        public static bool ConfirmBill(StorageAdjustModel model, List<StorageAdjustDetail> detail)
        {
            ArrayList sqllist = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageAdjust SET");
            sql.AppendLine(" Confirmor          = @Confirmor,");
            sql.AppendLine(" ConfirmDate      = @ConfirmDate,");
            sql.AppendLine(" BillStatus              = 2,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = sql.ToString(); ;
            SqlParameter[] param = new SqlParameter[6];
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqllist.Add(sqlcomm);

            for (int i = 0; i < detail.Count; i++)
            {
                string upsql = "";
                if (detail[i].AdjustType == "1")//增加
                {
                    upsql += "   update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID";
                }
                else
                {
                    upsql += "   update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)-@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID";
                }
                if (detail[i].BatchNo != "")
                    upsql += " and BatchNo='" + detail[i].BatchNo.Trim() + "' ";
                else
                    upsql += " and BatchNo is null ";

                SqlCommand upcomm = new SqlCommand();
                upcomm.CommandText = upsql;
                upcomm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", detail[i].AdjustCount));
                upcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detail[i].ProductID.ToString()));
                upcomm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
                upcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                sqllist.Add(upcomm);
                #region 操作库存流水账                
                StorageAccountModel AccountM_ = new StorageAccountModel();
                AccountM_.BatchNo = detail[i].BatchNo;
                AccountM_.BillNo = model.AdjustNo;
                AccountM_.BillType = 14;
                AccountM_.CompanyCD = model.CompanyCD;
                AccountM_.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                AccountM_.HappenCount = Convert.ToDecimal(detail[i].AdjustCount.ToString());
                AccountM_.ProductCount = Convert.ToDecimal(detail[i].AdjustCount);
                AccountM_.HappenDate = System.DateTime.Now;
                AccountM_.PageUrl = "../Office/StorageManager/StorageAdjustAdd.aspx";
                AccountM_.Price = Convert.ToDecimal(detail[i].CostPrice.ToString());                
                AccountM_.ProductID = Convert.ToInt32(detail[i].ProductID.ToString());
                AccountM_.StorageID = Convert.ToInt32(model.StorageID);
                SqlCommand AccountCom_=new SqlCommand();
                if (detail[i].AdjustType.ToString() == "1")
                    AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"0");
                else
                    AccountCom_ = StorageAccountDBHelper.InsertStorageAccountCommand(AccountM_,"1");
                
                sqllist.Add(AccountCom_);
                #endregion
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
        /// 取消确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static bool UnConfirmBill(StorageAdjustModel model, List<StorageAdjustDetail> detail)
        {
            ArrayList sqllist = new ArrayList();
            SqlCommand comm = new SqlCommand();
            int BillTypeFlag = int.Parse(ConstUtil.CODING_RULE_Storage_NO);
            int BillTypeCode = int.Parse(ConstUtil.CODING_RULE_StoAdjust_NO);
            string theSql = "select BillStatus from officedba.StorageAdjust where ID=@ID";
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = theSql;
            DataTable theDt = SqlHelper.ExecuteSearch(comm);
            string BillStatus = "0";
            if (theDt.Rows.Count > 0)
            {
                BillStatus = theDt.Rows[0]["BillStatus"].ToString();
            }
            if (BillStatus == "2")
            {

                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" UPDATE officedba.StorageAdjust SET");
                sql.AppendLine(" Confirmor          = @Confirmor,");
                sql.AppendLine(" ConfirmDate      = @ConfirmDate,");
                sql.AppendLine(" BillStatus              = 2,");
                sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
                sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
                sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = sql.ToString(); ;
                SqlParameter[] param = new SqlParameter[6];
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                sqllist.Add(sqlcomm);

                for (int i = 0; i < detail.Count; i++)
                {
                    string upsql = "";
                    if (detail[i].AdjustType == "1")//增加
                    {
                        upsql += "   update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)-@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID";
                    }
                    else
                    {
                        upsql += "   update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)+@ProductCount where CompanyCD=@CompanyCD and ProductID=@ProductID and StorageID=@StorageID";
                    }
                    if (detail[i].BatchNo != "")
                        upsql += " and BatchNo='" + detail[i].BatchNo.Trim() + "' ";
                    else
                        upsql += " and BatchNo is null ";
                    SqlCommand upcomm = new SqlCommand();
                    upcomm.CommandText = upsql;
                    upcomm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", detail[i].AdjustCount));
                    upcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detail[i].ProductID.ToString()));
                    upcomm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
                    upcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    sqllist.Add(upcomm);
                }

                #region 撤消审批流程
                DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
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
                    commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(commHis);
                    #endregion

                    #region 更新流程任务处理表
                    StringBuilder sqlTask = new StringBuilder();
                    sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                    sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                    SqlCommand commTask = new SqlCommand();
                    commTask.CommandText = sqlTask.ToString();
                    commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
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
                    commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(commIns);
                    #endregion

                }
                #endregion
            }
            else
            {
                return false;
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
        public static bool CloseBill(StorageAdjustModel model, string method)
        {
            ArrayList listsql = new ArrayList();
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.StorageAdjust SET");
            sql.AppendLine(" BillStatus              = 4,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");

            if (method == "0")
            {
                sql.AppendLine(" Closer      = @Closer,");
                sql.AppendLine(" CloseDate                = @CloseDate, ");
            }
            sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            string BillStatus = "2";
            if (method == "0")
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

        public static DataTable GetAllAdjust(StorageAdjustModel model,string EFIndex,string EFDesc, string BetinTime, string EndTime, string FlowStatus, ref int TotalCount)
        {
            string sql = "";
            int BillTypeFlag = int.Parse(ConstUtil.CODING_RULE_Storage_NO);
            int BillTypeCode = int.Parse(ConstUtil.CODING_RULE_StoAdjust_NO);
            sql += " select * from (";
            sql += " SELECT DISTINCT s.[ID],s.ModifiedDate                              ";
            sql += "       ,s.[AdjustNo]                                       ";
            sql += "       ,isnull(s.[StorageID],0) as StorageID               ";
            sql += "       ,s.[ReasonType]                                     ";
            sql += "       ,isnull(s.[Executor],0) as Executor                 ";
            sql += "       ,isnull(s.[DeptID],0) as DeptID                     ";
            sql += "       ,isnull(substring(CONVERT(varchar,s.[AdjustDate],120),0,11),'') AdjustDate";
            sql += "       ,isnull(s.Title,'') as Title                        ";
            sql += "       ,s.[BillStatus]                                     ";
            sql += "       ,isnull(e.EmployeeName,'') as EmployeeName        ";
            sql += "       ,isnull(st.StorageName,'')  as StorageName        ";
            sql += "       ,isnull(d.DeptName,'') as DeptName                ";
            sql += "       ,isnull(c.CodeName,'') as CodeName                ";
            sql += "       ,isnull(f.FlowStatus,'0') as FlowStatusID";
            sql += "       ,CASE f.FlowStatus WHEN '1' THEN '待审批' WHEN '2' THEN '审批中' WHEN '3' THEN '审批通过' WHEN '4' THEN '审批不通过' when '5' then '撤消审批' ELSE '' END AS FlowStatus";
            sql += "       ,CASE s.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' ELSE '' END AS BillStatusName ";
            sql += "   FROM [officedba].[StorageAdjust] as s left join officedba.StorageInfo as st on s.StorageID=st.ID                                                                ";
            sql += "       left join officedba.EmployeeInfo as e on s.Executor=e.ID left join officedba.DeptInfo as d on s.DeptID=d.ID                                                         ";
            sql += "       left join officedba.CodeReasonType as c on s.ReasonType=c.ID                                                                                                        ";
            sql += "       left join officedba.StorageAdjustDetail as x on x.AdjustNo=s.AdjustNo  and x.CompanyCD=s.CompanyCD                                                                                                      ";
            sql += "       left join officedba.FlowInstance AS f ON s.CompanyCD = f.CompanyCD AND f.BillTypeFlag = " + BillTypeFlag + " AND f.BillTypeCode = " + BillTypeCode + " AND  s.ID = f.BillID    ";
            sql += "       and f.ID=(select max(ID) from officedba.FlowInstance where s.CompanyCD = officedba.FlowInstance.CompanyCD AND officedba.FlowInstance.BillTypeFlag = " + BillTypeFlag + " AND officedba.FlowInstance.BillTypeCode = " + BillTypeCode + " AND  s.ID = officedba.FlowInstance.BillID) ";
            sql += " where 1=1   ";
            SqlCommand comm = new SqlCommand();
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql += " and s.ExtField" + EFIndex + " like @EFDesc ";
                comm.Parameters.Add(SqlHelper.GetParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                sql += " and x.BatchNo = @BatchNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", model.BatchNo));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql += "   and s.Title like @Title            ";
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(model.AdjustNo))
            {
                sql += "   and s.AdjustNo like @AdjustNo           ";
                comm.Parameters.Add(SqlHelper.GetParameter("@AdjustNo", "%" + model.AdjustNo + "%"));
            }
            if (model.StorageID != 0)
            {
                sql += "  and s.StorageID=@StorageID";
                comm.Parameters.Add(SqlHelper.GetParameter("@StorageID", model.StorageID));
            }
            if (model.BillStatus != "00")
            {
                sql += "  and s.BillStatus=@BillStatus";
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(BetinTime))
            {
                sql += "    and s.AdjustDate>=@BeginTime";
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BetinTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql += "         and s.AdjustDate<=@EndTime";
                comm.Parameters.Add(SqlHelper.GetParameter("@EndTime", EndTime));
            }
            if (FlowStatus != "00" && FlowStatus != "6")
            {
                sql += "  and   f.FlowStatus=@FlowStatus";
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus", FlowStatus));
            }
            if (model.Executor != 0)
            {
                sql += " and  s.Executor=@Executor";
                comm.Parameters.Add(SqlHelper.GetParameter("@Executor", model.Executor));
            }
            if (model.DeptID != 0)
            {
                sql += "  and s.DeptID=@DeptID";
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.DeptID));
            }
            if (model.ReasonType != 0)
            {
                sql += " and s.ReasonType=@ReasonType";
                comm.Parameters.Add(SqlHelper.GetParameter("@ReasonType", model.ReasonType));
            }
            sql += " and  c.CompanyCD=@CompanyCD ";
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sql += "  ";
            sql += " ) as Info";
            if (FlowStatus == "6")
            {
                sql += " where  FlowStatusID='0'";
            }
            if (model.Creator == -100)
            {
                sql += " order by " + model.Attachment;
            }
            comm.CommandText = sql;
            DataTable dt = new DataTable();
            if (model.Creator == -100)
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                dt = SqlHelper.PagerWithCommand(comm, model.Creator, model.Confirmor, model.Attachment, ref TotalCount);
            }
            return dt;
        }

        public static DataTable GetAdjustInfo(StorageAdjustModel model)
        {
            string sql = "";
            sql += " SELECT [ID]                   ";
            sql += "       ,[AdjustNo]             ";
            sql += "       ,isnull([StorageID],0) as StorageID            ";
            sql += "       ,[ReasonType]           ";
            sql += "       ,[Executor]             ";
            sql += "       ,isnull([DeptID],0) as DeptID               ";
            sql += "       ,isnull(CONVERT(varchar,[AdjustDate],120),'')as AdjustDate           ";
            sql += "       ,isnull(TotalPrice,0) TotalPrice           ";
            sql += "       ,isnull(CountTotal,0) CountTotal           ";
            sql += "       ,isnull([Summary],'') as Summary              ";
            sql += "       ,isnull([ExtField1],'') as ExtField1              ";
            sql += "       ,isnull([ExtField2],'') as ExtField2              ";
            sql += "       ,isnull([ExtField3],'') as ExtField3              ";
            sql += "       ,isnull([ExtField4],'') as ExtField4              ";
            sql += "       ,isnull([ExtField5],'') as ExtField5              ";
            sql += "       ,isnull([ExtField6],'') as ExtField6              ";
            sql += "       ,isnull([ExtField7],'') as ExtField7              ";
            sql += "       ,isnull([ExtField8],'') as ExtField8              ";
            sql += "       ,isnull([ExtField9],'') as ExtField9              ";
            sql += "       ,isnull([ExtField10],'') as ExtField10              ";
            sql += "       ,isnull([Remark],'') as Remark               ";
            sql += "       ,Replace([Attachment],'\\',',') as Attachment ";
            sql += "       ,isnull(CONVERT(varchar,[CreateDate],120),'') as CreateDate           ";
            sql += "       ,[BillStatus]           ";
            sql += "       ,isnull(CONVERT(varchar,[ConfirmDate],120),'') as ConfirmDate          ";
            sql += "       ,isnull(CONVERT(varchar,[CloseDate],120),'') as CloseDate            ";
            sql += "       ,CONVERT(varchar,[ModifiedDate],120) as ModifiedDate         ";
            sql += "       ,[ModifiedUserID]       ";
            sql += "       ,isnull([Title],'') as Title                ";
            sql += "       ,(select UserID from officedba.UserInfo where officedba.UserInfo.UserID=s.ModifiedUserID) as ModifiedUserIDName";
            sql += "       ,isnull((select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=s.DeptID),'') as DeptName      ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e where e.ID=s.Closer),'') as CloserName        ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e where e.ID=s.Executor),'') as ExecutorName    ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e where e.ID=s.Confirmor),'') as ConfirmorName  ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e where e.ID=s.Creator),'') as CreatorName      ";
            sql += "   FROM [officedba].[StorageAdjust] as s                                                                   ";
            sql += "   where s.CompanyCD=@CompanyCD and ID=@ID                                                                         ";
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetAdjustDetail(StorageAdjustModel model)
        {
            string sql = "";
            sql += " SELECT s.[ID]                ";
            sql += "       ,isnull(s.[AdjustNo],'') as AdjustNo,s.BatchNo,s.UsedUnitID,s.UsedUnitCount,s.UsedPrice,s.ExRate,p.IsBatchNo          ";
            sql += "       ,isnull(s.[SortNo],0) as SortNo            ";
            sql += "       ,isnull(s.[ProductID],0) as ProductID         ";
            sql += "       ,isnull(s.[UnitID],0) as UnitID            ";
            sql += "       ,isnull(s.[AdjustType],'') as AdjustType        ";
            sql += "       ,isnull(s.[AdjustCount],0) as AdjustCount       ";
            sql += "       ,isnull(s.[CostPrice],0) as CostPrice         ";
            sql += "       ,isnull(s.[CostPriceTotal],0) as CostPriceTotal    ";
            sql += "       ,isnull(s.[Remark],'') as Remark            ";
            sql += "       ,isnull(p.ProductName,'') as ProductName         ";
            sql += "       ,isnull(p.ProdNo,'')  as ProNo     ";
            sql += "       ,isnull(c.CodeName,'') as CodeName            ";
            sql += "   FROM [officedba].[StorageAdjustDetail] as s left join officedba.ProductInfo as p on s.ProductID=p.ID          ";
            sql += "       left join officedba.CodeUnitType as c on s.UnitID=c.ID                                                            ";
            sql += "   where  s.CompanyCD=@CompanyCD  and s.AdjustNo=(select top 1 AdjustNo from [officedba].[StorageAdjust] where ID=@ID) ";

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        //--------------------------------------------------------------------------页面打印需要
        public static DataTable GetAdjustInfo(int ID)
        {
            string sql = "";
            sql += " SELECT  ";
            sql += "       s.AdjustNo";
            sql += "       ,isnull(b.[StorageName],'') as StorageID ";
            sql += "       ,isnull(e.CodeName,'') as ReasonType";
            sql += "       ,isnull(substring(CONVERT(varchar,s.AdjustDate,120),0,11),'')as AdjustDate";
            sql += "       ,convert(numeric(12,2),isnull(s.TotalPrice,0)) as TotalPrice ";
            sql += "       ,convert(numeric(12,2),isnull(s.CountTotal,0)) as CountTotal ";
            sql += "       ,isnull(s.[Summary],'') as Summary ";
            sql += "       ,isnull(s.[Remark],'') as Remark ";
            sql += "       ,isnull(substring(CONVERT(varchar,s.[CreateDate],120),0,11),'') as CreateDate ";
            sql += "       ,case s.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更' when '4' then '手工结单' when '5' then '自动结单' else '' end as BillStatus ";
            sql += "       ,isnull(substring(CONVERT(varchar,s.[ConfirmDate],120),0,11),'') as ConfirmDate ";
            sql += "       ,isnull(substring(CONVERT(varchar,s.[CloseDate],120),0,11),'') as CloseDate ";
            sql += "       ,isnull(substring(CONVERT(varchar,s.[ModifiedDate],120),0,11),'') as ModifiedDate ";
            sql += "       ,s.ModifiedUserID";
            sql += "       ,isnull(s.[Title],'') as Title";
            sql += "       ,isnull((select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=s.DeptID),'') as DeptID      ";
            sql += "       ,isnull(s.[ExtField1],'') as ExtField1              ";
            sql += "       ,isnull(s.[ExtField2],'') as ExtField2              ";
            sql += "       ,isnull(s.[ExtField3],'') as ExtField3              ";
            sql += "       ,isnull(s.[ExtField4],'') as ExtField4              ";
            sql += "       ,isnull(s.[ExtField5],'') as ExtField5              ";
            sql += "       ,isnull(s.[ExtField6],'') as ExtField6              ";
            sql += "       ,isnull(s.[ExtField7],'') as ExtField7              ";
            sql += "       ,isnull(s.[ExtField8],'') as ExtField8              ";
            sql += "       ,isnull(s.[ExtField9],'') as ExtField9              ";
            sql += "       ,isnull(s.[ExtField10],'') as ExtField10              ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e1 where e1.ID=s.Closer),'') as Closer        ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e2 where e2.ID=s.Executor),'') as Executor    ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e3 where e3.ID=s.Confirmor),'') as Confirmor  ";
            sql += "       ,isnull((select EmployeeName from officedba.EmployeeInfo as e4 where e4.ID=s.Creator),'') as Creator      ";
            sql += "   FROM officedba.StorageAdjust as s ";
            sql += "       left join officedba.StorageInfo as b on s.StorageID=b.ID";
            sql += "       left join officedba.CodeReasonType as e on e.ID=s.ReasonType";
            sql += "   where s.ID=@ID ";
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetAdjustDetail(int ID)
        {
            string sql = "";
            sql += " SELECT  ";
            sql += "        case s.AdjustType when '0' then '调减' when '1' then '调增' else '' end as AdjustType ";
            sql += "       ,isnull(s.[AdjustCount],0) as AdjustCount,s.BatchNo,s.UsedUnitID,s.UsedUnitCount,s.UsedPrice,s.ExRate,ii.CodeName as UsedUnitName  ";
            sql += "       ,isnull(Convert(numeric(10,2),s.[CostPrice]),0) as CostPrice ";
            sql += "       ,isnull(Convert(numeric(10,2),s.[CostPriceTotal]),0) as CostPriceTotal ";
            sql += "       ,isnull(s.[Remark],'') as Remark ";
            sql += "       ,isnull(p.ProductName,'') as ProductID ";
            sql += "       ,isnull(p.ProdNo,'')  as ID";
            sql += "       ,isnull(c.CodeName,'') as UnitID";
            sql += "   FROM officedba.StorageAdjustDetail as s left join officedba.ProductInfo as p on s.ProductID=p.ID ";
            sql += "       left join officedba.CodeUnitType as c on s.UnitID=c.ID ";
            sql += "       left join officedba.CodeUnitType as ii on ii.ID=s.UsedUnitID ";
            sql += "   where s.AdjustNo=(select top 1 AdjustNo from [officedba].[StorageAdjust] where [officedba].[StorageAdjust].ID=@ID) ";

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数           
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(StorageAdjustModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.StorageAdjust set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND AdjustNo = @AdjustNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@AdjustNo", model.AdjustNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion
    }
}
