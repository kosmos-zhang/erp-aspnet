using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellGatheringDBHelper
    {
        #region 添加回款计划
        /// <summary>
        /// 添加回款计划
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static bool InsertSellGathering(Hashtable htExtAttr, SellGatheringModel sellGatheringModel, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
             strMsg = "";
            //判断单据编号是否存在
             if (NoIsExist(sellGatheringModel.GatheringNo))
             {
                 StringBuilder strSql = new StringBuilder();
                 strSql.Append("insert into officedba.SellGathering(");
                 strSql.Append("Title,CompanyCD,GatheringNo,CustID,FromType,FromBillID,CurrencyType,PlanGatherDate,PlanPrice,GatheringTime,FactPrice,FactGatherDate,Seller,SellDeptId,LinkBillNo,State,Remark,ModifiedDate,ModifiedUserID,Creator,CreateDate)");
                 strSql.Append(" values (");
                 strSql.Append("@Title,@CompanyCD,@GatheringNo,@CustID,@FromType,@FromBillID,@CurrencyType,@PlanGatherDate,@PlanPrice,@GatheringTime,@FactPrice,@FactGatherDate,@Seller,@SellDeptId,@LinkBillNo,@State,@Remark,getdate(),@ModifiedUserID,@Creator,getdate())");

                 SqlParameter[] param = null;
                 ArrayList lcmd = new ArrayList();
                 #region 参数
                 lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellGatheringModel.CompanyCD));
                 lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellGatheringModel.CustID.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@GatheringNo", sellGatheringModel.GatheringNo));
                 lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellGatheringModel.Title));
                 lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellGatheringModel.FromType));
                 lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellGatheringModel.FromBillID.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellGatheringModel.CurrencyType.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@PlanGatherDate", sellGatheringModel.PlanGatherDate.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@PlanPrice", sellGatheringModel.PlanPrice.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@GatheringTime", sellGatheringModel.GatheringTime.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@FactPrice", sellGatheringModel.FactPrice.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@FactGatherDate", sellGatheringModel.FactGatherDate.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellGatheringModel.Seller.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellGatheringModel.SellDeptId.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@LinkBillNo", sellGatheringModel.LinkBillNo));
                 lcmd.Add(SqlHelper.GetParameterFromString("@State", sellGatheringModel.State));
                 lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellGatheringModel.Remark));
                 lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedDate", sellGatheringModel.ModifiedDate.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellGatheringModel.ModifiedUserID));
                 lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellGatheringModel.Creator.ToString()));
                 lcmd.Add(SqlHelper.GetParameterFromString("@CreateDate", sellGatheringModel.CreateDate.ToString()));
                 #endregion
                 #region 拓展属性

                 if (htExtAttr != null && htExtAttr.Count != 0)
                 {
                     strSql.Append(" ;UPDATE officedba.SellGathering set ");
                     foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                     {
                         strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                         lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                     }
                     strSql.Append(" ModifiedUserID=@ModifiedUserID  where CompanyCD=@CompanyCD and GatheringNo=@GatheringNo ");
                 }
                 if (lcmd != null && lcmd.Count > 0)
                 {
                     param = new SqlParameter[lcmd.Count];
                     for (int i = 0; i < lcmd.Count; i++)
                     {
                         param[i] = (SqlParameter)lcmd[i];
                     }
                 }
                 #endregion

                 foreach (SqlParameter para in param)
                 {
                     if (para.Value == null)
                     {
                         para.Value = DBNull.Value;
                     }
                 }
                 try
                 {
                     SqlHelper.ExecuteTransSql(strSql.ToString(), param);
                     isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
                     strMsg = "保存成功！";
                 }
                 catch (Exception ex)
                 {
                     isSucc = false;
                     strMsg = "保存失败，请联系系统管理员！";
                     throw ex;
                 }
             }
             else
             {
                 isSucc = false;
                 strMsg = "该编号已被使用，请输入未使用的编号！";
             }
            return isSucc;
        }
        #endregion

        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@GatheringNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellGathering ";
            strSql += " WHERE  (GatheringNo = @GatheringNo) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static DataTable GetSellGathering(SellGatheringModel sellGatheringModel, string PlanPrice0,string EFIndex,string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT g.ID, g.Title, g.GatheringNo,g.ModifiedDate, CONVERT(varchar(100), g.PlanGatherDate, 23) ");
            strSql.Append(" AS PlanGatherDate, ISNULL(c.CustName, '') AS CustName, ");
            strSql.Append(" ISNULL(CASE g.FromType WHEN '0' THEN '' WHEN '1' THEN ");
            strSql.Append(" (SELECT so.OrderNo FROM officedba.SellOrder AS so ");
            strSql.Append(" WHERE so.id = g.FromBillID) WHEN '2' THEN ");
            strSql.Append(" (SELECT ss.SendNo FROM officedba.SellSend AS ss ");
            strSql.Append(" WHERE ss.id = g.FromBillID) END, '') AS BillNo, ");
            strSql.Append(" CASE g.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售订单' ");
            strSql.Append(" WHEN '2' THEN '销售发货单' END AS fromTypeName, ");
            strSql.Append(" CASE g.State WHEN '1' THEN '已回款' WHEN '2' THEN '未回款' WHEN '3' ");
            strSql.Append(" THEN '部分回款' END AS stateName, ISNULL(e.EmployeeName, '') ");
            strSql.Append(" AS EmployeeName,isnull( g.PlanPrice,0) as PlanPrice, g.GatheringTime ");
            strSql.Append(" FROM officedba.SellGathering AS g LEFT OUTER JOIN ");
            strSql.Append(" officedba.CustInfo AS c ON g.CustID = c.ID LEFT OUTER JOIN ");
            strSql.Append(" officedba.EmployeeInfo AS e ON g.Seller = e.ID ");

            strSql.Append(" WHERE    g.CompanyCD=@CompanyCD");
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql.Append(" and g.ExtField" + EFIndex + " like @EFDesc ");
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }

            if (sellGatheringModel.CustID != null)
            {
                strSql.Append(" and  g.CustID = @CustID ");
                arr.Add(new SqlParameter("@CustID", sellGatheringModel.CustID));
            }
            if (sellGatheringModel.FromBillID != null)
            {
                strSql.Append(" and  g.FromBillID = @FromBillID ");

                arr.Add(new SqlParameter("@FromBillID", sellGatheringModel.FromBillID));
            }
            if (sellGatheringModel.FromType != null)
            {
                strSql.Append(" and  g.FromType = @FromType ");

                arr.Add(new SqlParameter("@FromType", sellGatheringModel.FromType));
            }
            if (sellGatheringModel.GatheringNo != null)
            {
                strSql.Append(" and  g.GatheringNo like @GatheringNo ");

                arr.Add(new SqlParameter("@GatheringNo", "%" + sellGatheringModel.GatheringNo + "%"));
            }
            if (sellGatheringModel.GatheringTime != null)
            {
                strSql.Append(" and  g.GatheringTime = @GatheringTime ");

                arr.Add(new SqlParameter("@GatheringTime", sellGatheringModel.GatheringTime));
            }
            if (sellGatheringModel.PlanPrice != null)
            {
                strSql.Append(" and  g.PlanPrice > @PlanPrice ");

                arr.Add(new SqlParameter("@PlanPrice",  sellGatheringModel.PlanPrice));
            }
            if (PlanPrice0 != null)
            {
                strSql.Append(" and  g.PlanPrice < @PlanPrice0 ");

                arr.Add(new SqlParameter("@PlanPrice0", PlanPrice0));
            }
            if (sellGatheringModel.Seller != null)
            {
                strSql.Append(" and  g.Seller = @Seller ");

                arr.Add(new SqlParameter("@Seller", sellGatheringModel.Seller));
            }
            if (sellGatheringModel.Title != null)
            {
                strSql.Append(" and  g.Title like @Title ");

                arr.Add(new SqlParameter("@Title", "%" + sellGatheringModel.Title + "%"));
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 删除回款计划
        /// </summary>
        /// <param name="strIDS"></param>
        /// <returns></returns>
        public static bool DelSellGathering(string strIDS, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            string strSql = "delete from officedba.SellGathering where ID = @ID";
            string[] strId = strIDS.Split(',');
            strMsg = "";
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                for (int i = 0; i < strId.Length; i++)
                {
                    SqlParameter[] para = { new SqlParameter("@ID", Convert.ToInt32(strId[i])) };
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql, para);
                }

                tran.Commit();
                isSucc = true;
                strMsg = "删除成功！";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "删除失败，请联系系统管理员！";
                isSucc = false;
                throw ex;
            }
            return isSucc;
        }
        
        #region 修改回款计划
        /// <summary>
        /// 修改回款计划
        /// </summary>
        /// <param name="sellGatheringModel"></param>
        /// <returns></returns>
        public static bool UpdateSellGathering(Hashtable htExtAttr, SellGatheringModel sellGatheringModel, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellGathering set ");
            strSql.Append("Title=@Title,");
            strSql.Append("CustID=@CustID,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("FromBillID=@FromBillID,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("PlanGatherDate=@PlanGatherDate,");
            strSql.Append("PlanPrice=@PlanPrice,");
            strSql.Append("GatheringTime=@GatheringTime,");
            strSql.Append("FactPrice=@FactPrice,");
            strSql.Append("FactGatherDate=@FactGatherDate,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("LinkBillNo=@LinkBillNo,");
            strSql.Append("State=@State,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where CompanyCD=@CompanyCD and GatheringNo=@GatheringNo ");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellGatheringModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellGatheringModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@GatheringNo", sellGatheringModel.GatheringNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellGatheringModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellGatheringModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellGatheringModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellGatheringModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PlanGatherDate", sellGatheringModel.PlanGatherDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PlanPrice", sellGatheringModel.PlanPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@GatheringTime", sellGatheringModel.GatheringTime.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FactPrice", sellGatheringModel.FactPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FactGatherDate", sellGatheringModel.FactGatherDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellGatheringModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellGatheringModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@LinkBillNo", sellGatheringModel.LinkBillNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@State", sellGatheringModel.State));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellGatheringModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellGatheringModel.ModifiedUserID));
            #endregion
            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellGathering set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" ModifiedUserID=@ModifiedUserID  where CompanyCD=@CompanyCD and GatheringNo=@GatheringNo ");
            }
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            #endregion

            foreach (SqlParameter para in param)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            try
            {
                SqlHelper.ExecuteTransSql(strSql.ToString(), param);
                isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
                strMsg = "保存成功！";
            }
            catch (Exception ex)
            {
                isSucc = false;
                strMsg = "保存失败，请联系系统管理员！";
                throw ex;
            }

            return isSucc;
        }
        #endregion

        #region 获取回款计划主表信息
        /// <summary>
        /// 获取发货单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT g.ID, g.Title, g.CompanyCD, g.GatheringNo, g.CustID, g.FromType, g.FromBillID, ";
            strSql += "g.CurrencyType, CONVERT(varchar(100), g.PlanGatherDate, 23) ";
            strSql += "AS PlanGatherDate, g.PlanPrice, g.GatheringTime, g.FactPrice, CONVERT(varchar(100), ";
            strSql += "g.FactGatherDate, 23) AS FactGatherDate, g.Seller, g.SellDeptId, ";
            strSql += "g.ExtField1,g.ExtField2,g.ExtField3,g.ExtField4,g.ExtField5,";
            strSql += "g.ExtField6,g.ExtField7,g.ExtField8,g.ExtField9,g.ExtField10, ";
            strSql += "g.LinkBillNo, g.State, g.Remark, CONVERT(varchar(100), g.ModifiedDate, 23) AS ModifiedDate, ";
            strSql += "g.ModifiedUserID, g.Creator, CONVERT(varchar(100), ";
            strSql += "g.CreateDate, 23) AS CreateDate, d.DeptName, ISNULL(c.CustName, '') AS CustName, t.CurrencyName, ";
            strSql += "CASE g.FromType WHEN '0' THEN '' WHEN '1' THEN (SELECT so.OrderNo ";
            strSql += "FROM officedba.SellOrder AS so WHERE so.id = g.FromBillID) WHEN '2' THEN ";
            strSql += "(SELECT ss.SendNo FROM officedba.SellSend AS ss ";
            strSql += "WHERE ss.id = g.FromBillID) END AS BillNo, ISNULL(e.EmployeeName, '') AS EmployeeName,ISNULL(e1.EmployeeName, '') AS CreatorName, c.Tel, cpt.TypeName ";
            strSql += "FROM officedba.SellGathering AS g left JOIN ";
            strSql += "officedba.CustInfo AS c ON g.CustID = c.ID left JOIN ";
            strSql += "officedba.DeptInfo AS d ON g.SellDeptId = d.ID left JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS t ON g.CurrencyType = t.ID left JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON g.Seller = e.ID left JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON g.Creator = e1.ID left JOIN ";
            strSql += "officedba.CodePublicType AS cpt ON c.CustType = cpt.ID ";                                                       
                                                  

            strSql += " WHERE (g.ID = @ID ) AND (g.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "select * from  officedba.sellmodule_report_SellGathering WHERE (GatheringNo = @GatheringNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@GatheringNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
    }
}
