/**********************************************
 * 类作用：   凭证模板
 * 建立人：   莫申林
 * 建立时间： 2010/04/06
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using System.Collections.Generic;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
   public class VoucherTemaplateDBHelper
    {
        #region 判断单据编号是否存在
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
                                       new SqlParameter("@TemNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.VoucherTemplate ";
            strSql += " WHERE  (TemNo  = @TemNo ) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 添加
        #region 添加新单据(费用票据)
        /// <summary>
        /// 添加新单据
        /// </summary>
        /// <returns></returns>
        public static bool Insert(VoucherTemplateModel voucherTemplateModel, List<VoucherTemplateDetailModel> voucherTemplateDetailModelList, out string strMsg, out int Id)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            Id = 0;
            //判断单据编号是否存在
            if (NoIsExist(voucherTemplateModel.TemNo))
            {
                if (IsTypeUsed(voucherTemplateModel.TemType.ToString(), voucherTemplateModel.CompanyCD))
                {
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        InsertVoucherTemplate(voucherTemplateModel, tran, out Id);
                        InsertVoucherTemplateDetail(voucherTemplateDetailModelList, tran);
                        tran.Commit();
                        isSucc = true;
                        strMsg = "保存成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        strMsg = "保存失败，请联系系统管理员！";
                        throw ex;
                    }
                }
                else
                {
                    isSucc = false;
                    strMsg = "该模板类型已存在，请选择其他未使用的模板类型！";
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


        #region 控制一种模板类型只能添加一套凭证模板
        public static bool IsTypeUsed(string TemType, string CompanyCD)
        {
            bool isSucc = true;

            string sql = "select count(id) from officedba.VoucherTemplate where CompanyCD='" + CompanyCD + "' and TemType='"+TemType+"'";

            object obj = SqlHelper.ExecuteScalar(sql, null);
            if (obj != null)
            {
                if (Convert.ToInt32(obj) > 0)
                {
                    isSucc = false;
                }
            }
            return isSucc;
        }
        #endregion

        #region 为主表插入数据
        /// <summary> .
        /// 为主表插入数据
        /// </summary>
        /// <param name="voucherTemplateModel"></param>
        /// <param name="tran"></param>
        private static void InsertVoucherTemplate(VoucherTemplateModel voucherTemplateModel, TransactionManager tran, out int IntID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.VoucherTemplate(");
            strSql.Append("CompanyCD,TemNo ,TemName,TemType,Abstract,Remark,UsedStatus,Creator,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@TemNo ,@TemName,@TemType,@Abstract,@Remark,@UsedStatus,@Creator,@CreateDate)");
            strSql.Append(" set @IntID= @@IDENTITY");

            #region 参数

            SqlParameter[] parms = new SqlParameter[10];

           parms[0] = SqlHelper.GetParameter("@CompanyCD", voucherTemplateModel.CompanyCD);
           parms[1] = SqlHelper.GetParameter("@TemNo ", voucherTemplateModel.TemNo);
           parms[2] = SqlHelper.GetParameter("@TemName", voucherTemplateModel.TemName);
           parms[3] = SqlHelper.GetParameter("@TemType", voucherTemplateModel.TemType.ToString());
           parms[4] = SqlHelper.GetParameter("@Abstract", voucherTemplateModel.Abstract);
           parms[5] = SqlHelper.GetParameter("@Remark", voucherTemplateModel.Remark);
           parms[6] = SqlHelper.GetParameter("@UsedStatus", voucherTemplateModel.UsedStatus);
           parms[7] = SqlHelper.GetParameter("@Creator", voucherTemplateModel.Creator.ToString());
           parms[8] = SqlHelper.GetParameter("@CreateDate", voucherTemplateModel.CreateDate.ToString());
           parms[9] = SqlHelper.GetOutputParameter("@IntID", SqlDbType.Int);

            #endregion

           SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parms);

           IntID = Convert.ToInt32(parms[9].Value);
        }
        #endregion
        #region 为明细表插入数据
        /// <summary>
        /// 为明细表插入数据
        /// </summary>
        /// <param name="voucherTemplateDetailModelList"></param>
        /// <param name="tran"></param>
        private static void InsertVoucherTemplateDetail(List<VoucherTemplateDetailModel> voucherTemplateDetailModelList, TransactionManager tran)
        {
            foreach (VoucherTemplateDetailModel voucherTemplateDetailModel in voucherTemplateDetailModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.VoucherTemplateDetail(");
                strSql.Append("CompanyCD,TemNo,SortNo,SubjectsNo,Direction,Scale,Remark)");
                strSql.Append(" values (");
                strSql.Append("@CompanyCD,@TemNo,@SortNo,@SubjectsNo,@Direction,@Scale,@Remark)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@TemNo ", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@SubjectsNo", SqlDbType.VarChar,50),
					new SqlParameter("@Direction", SqlDbType.Char,1),
					new SqlParameter("@Scale", SqlDbType.Decimal,12),
					new SqlParameter("@Remark", SqlDbType.VarChar,200)
				};
                parameters[0].Value = voucherTemplateDetailModel.CompanyCD;
                parameters[1].Value = voucherTemplateDetailModel.TemNo;
                parameters[2].Value = Convert.ToInt32(voucherTemplateDetailModel.SortNo);
                parameters[3].Value = voucherTemplateDetailModel.SubjectsNo;
                parameters[4].Value = voucherTemplateDetailModel.Direction;
                parameters[5].Value =Convert.ToDecimal(voucherTemplateDetailModel.Scale);
                parameters[6].Value = voucherTemplateDetailModel.Remark;

                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        #endregion

        #endregion

        #region 修改

        #region 修改方法
        public static bool Update(VoucherTemplateModel voucherTemplateModel, List<VoucherTemplateDetailModel> voucherTemplateDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
                string strSql = "delete from officedba.VoucherTemplateDetail where  TemNo =@TemNo   and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@TemNo ", voucherTemplateModel.TemNo), new SqlParameter("@CompanyCD", voucherTemplateModel.CompanyCD) };
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateVoucherTemplate(voucherTemplateModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertVoucherTemplateDetail(voucherTemplateDetailModelList, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
           
            return isSucc;
        }
        #endregion

        #region 更新主表数据
        /// <summary>
        /// 跟新主表数据
        /// </summary>
        /// <param name="voucherTemplateModel"></param>
        /// <param name="tran"></param>
        private static void UpdateVoucherTemplate(VoucherTemplateModel voucherTemplateModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            #region sql语句
            strSql.Append("update officedba.VoucherTemplate set ");
            strSql.Append("TemName=@TemName,");
            strSql.Append("TemType=@TemType,");
            strSql.Append("Abstract=@Abstract,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UsedStatus=@UsedStatus,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate");
          
            strSql.Append(" where CompanyCD=@CompanyCD and TemNo =@TemNo  ");
            #endregion

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@TemName", voucherTemplateModel.TemName));
            lcmd.Add(SqlHelper.GetParameterFromString("@TemType", voucherTemplateModel.TemType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Abstract", voucherTemplateModel.Abstract));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", voucherTemplateModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@UsedStatus", voucherTemplateModel.UsedStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", voucherTemplateModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CreateDate", voucherTemplateModel.CreateDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", voucherTemplateModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@TemNo ", voucherTemplateModel.TemNo));

            #endregion

            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }

            foreach (SqlParameter para in param)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }

            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion

        #endregion

        #region 删除
        public static bool Del(string orderNos, out string strMsg, out string strFieldText)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {

                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.VoucherTemplate WHERE TemNo  IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.VoucherTemplateDetail WHERE TemNo  IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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

        #region 判断状态
        private static bool IsFlow(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@TemNo ", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.VoucherTemplate ";
            strSql += "  WHERE TemNo = @TemNo and ConfirmStatus  = '1' AND CompanyCD = @CompanyCD ";
            try
            {
                iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            }
            catch 
            {
                isSuc = false;
            }

            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #endregion

        #region 凭证模板列表
        public static DataTable GetFeesBySearch(string CompanyCD, VoucherTemplateModel VoucherTemplateM, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
           
                #region 查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.TemNo,a.TemName                   ");
                searchSql.AppendLine(",a.Abstract  ");
                searchSql.AppendLine(",case (a.TemType)                ");
                searchSql.AppendLine(" when '1' then '采购订单' ");
                searchSql.AppendLine(" when '2' then '销售订单' ");
                searchSql.AppendLine(" when '3' then '委托代销单' ");
                searchSql.AppendLine(" when '4' then '销售退货单' ");
                searchSql.AppendLine(" when '5' then '采购入库'  ");
                searchSql.AppendLine(" when '6' then '其他出库单'  ");
                searchSql.AppendLine(" when '7' then '销售出库单' ");
                searchSql.AppendLine(" when '8' then '其他入库单'  ");
                searchSql.AppendLine(" when '9' then '收款单' ");
                searchSql.AppendLine(" when '10' then '付款单' ");
                searchSql.AppendLine(" when '11' then '销售发货通知单' ");
                searchSql.AppendLine(" end as  TemTypeName ");
                searchSql.AppendLine("      ,a.Remark                   ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TemType,a.UsedStatus,case(a.UsedStatus) when '1' then '启用' when '0' then '停用' end as UsedStatusName,b.EmployeeName  as  CreatorName,a.Creator ");
                searchSql.AppendLine("  FROM officedba.VoucherTemplate a ");
                searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Creator ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");

                #endregion
                #region 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

                //模板编号
                if (!string.IsNullOrEmpty(VoucherTemplateM.TemNo))
                {
                    searchSql.AppendLine("	AND a.TemNo LIKE  '%' + @TemNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemNo", VoucherTemplateM.TemNo));
                }
                //摘要
                if (!string.IsNullOrEmpty(VoucherTemplateM.TemName))
                {
                    searchSql.AppendLine("	AND a.TemName LIKE  '%' + @TemName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemName", VoucherTemplateM.TemName));
                }
                //模板类别
                if (VoucherTemplateM.TemType != 0)
                {
                    searchSql.AppendLine("	AND a.TemType=@TemType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemType", VoucherTemplateM.TemType.ToString()));
                }
              

                #endregion

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 根据ID获取详细
        public static DataTable GetOrderInfo(int ID)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                       ");
            searchSql.AppendLine("      ,a.CompanyCD                ");
            searchSql.AppendLine("      ,a.TemNo,a.TemName                   ");
            searchSql.AppendLine(",a.Abstract  ");
            searchSql.AppendLine(",case (a.TemType)                ");
            searchSql.AppendLine(" when '1' then '采购订单' ");
            searchSql.AppendLine(" when '2' then '销售订单' ");
            searchSql.AppendLine(" when '3' then '委托代销单' ");
            searchSql.AppendLine(" when '4' then '销售退货单' ");
            searchSql.AppendLine(" when '5' then '采购入库'  ");
            searchSql.AppendLine(" when '6' then '其他出库单'  ");
            searchSql.AppendLine(" when '7' then '销售出库单' ");
            searchSql.AppendLine(" when '8' then '其他入库单'  ");
            searchSql.AppendLine(" when '9' then '收款单' ");
            searchSql.AppendLine(" when '10' then '付款单' ");
            searchSql.AppendLine(" when '11' then '销售发货通知单' ");
            searchSql.AppendLine(" end as  TemTypeName ");
            searchSql.AppendLine("      ,a.Remark                   ");
            searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
            searchSql.AppendLine("      ,a.TemType,a.UsedStatus,case(a.UsedStatus) when '1' then '启用' when '0' then '停用' end as UsedStatusName,b.EmployeeName  as  CreatorName,a.Creator ");
            searchSql.AppendLine("  FROM officedba.VoucherTemplate a ");
            searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Creator ");
            searchSql.AppendLine(" WHERE a.ID = @ID and a.CompanyCD = @CompanyCD   ");

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", ID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 根据编号获取对应明细
        public static DataTable GetFeeDetail(string CompanyCD, string TemNo)
        {
           
            #region 查询SQL拼写
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                       ");
            searchSql.AppendLine("      ,a.CompanyCD,a.TemNo,a.SortNo      ");
            searchSql.AppendLine("      ,a.SubjectsNo,b.SubjectsName ");
            searchSql.AppendLine("      ,a.Direction,case(a.Direction) when '0' then '借方' when '1' then '贷方' end as DirectionName,a.Scale,a.Remark ");
            searchSql.AppendLine(" from officedba.VoucherTemplateDetail a");
            searchSql.AppendLine(" left join officedba.AccountSubjects b on b.SubjectsCD = a.SubjectsNo and b.CompanyCD = a.CompanyCD ");
            searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");
            searchSql.AppendLine(" and a.TemNo = @TemNo");

            #endregion 定义查询的命令
            SqlCommand comm = new SqlCommand();
            //公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //单据编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemNo", TemNo));

            //设定comm的SQL文
            comm.CommandText = searchSql.ToString();

            //执行查询
            return SqlHelper.ExecuteSearch(comm);
            
        }
        #endregion

        #region 根据编号获取详细
        public static DataTable GetOrderInfoByNo(string BillNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                       ");
            searchSql.AppendLine("      ,a.CompanyCD                ");
            searchSql.AppendLine("      ,a.TemNo,a.TemName                   ");
            searchSql.AppendLine(",a.Abstract  ");
            searchSql.AppendLine(",case (a.TemType)                ");
            searchSql.AppendLine(" when '1' then '采购订单' ");
            searchSql.AppendLine(" when '2' then '销售订单' ");
            searchSql.AppendLine(" when '3' then '委托代销单' ");
            searchSql.AppendLine(" when '4' then '销售退货单' ");
            searchSql.AppendLine(" when '5' then '采购入库'  ");
            searchSql.AppendLine(" when '6' then '其他出库单'  ");
            searchSql.AppendLine(" when '7' then '销售出库单' ");
            searchSql.AppendLine(" when '8' then '其他入库单'  ");
            searchSql.AppendLine(" when '9' then '收款单' ");
            searchSql.AppendLine(" when '10' then '付款单' ");
            searchSql.AppendLine(" when '11' then '销售发货通知单' ");
            searchSql.AppendLine(" end as  TemTypeName ");
            searchSql.AppendLine("      ,a.Remark                   ");
            searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
            searchSql.AppendLine("      ,a.TemType,a.UsedStatus,case(a.UsedStatus) when '1' then '启用' when '0' then '停用' end as UsedStatusName,b.EmployeeName  as  CreatorName,a.Creator ");
            searchSql.AppendLine("  FROM officedba.VoucherTemplate a ");
            searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Creator ");
            searchSql.AppendLine(" WHERE a.TemNo = @TemNo and a.CompanyCD = @CompanyCD ");

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@TemNo", BillNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(searchSql.ToString(), arr);
        }
        #endregion

        #region 导出凭证模板列表
        public static DataTable ExportFeesBySearch(string CompanyCD, VoucherTemplateModel VoucherTemplateM)
        {
            try
            {
                #region 查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("SELECT a.ID                       ");
                searchSql.AppendLine("      ,a.CompanyCD                ");
                searchSql.AppendLine("      ,a.TemNo,a.TemName                   ");
                searchSql.AppendLine(",a.Abstract  ");
                searchSql.AppendLine(",case (a.TemType)                ");
                searchSql.AppendLine(" when '1' then '采购订单' ");
                searchSql.AppendLine(" when '2' then '销售订单' ");
                searchSql.AppendLine(" when '3' then '委托代销单' ");
                searchSql.AppendLine(" when '4' then '销售退货单' ");
                searchSql.AppendLine(" when '5' then '采购入库'  ");
                searchSql.AppendLine(" when '6' then '其他出库单'  ");
                searchSql.AppendLine(" when '7' then '销售出库单' ");
                searchSql.AppendLine(" when '8' then '其他入库单'  ");
                searchSql.AppendLine(" when '9' then '收款单' ");
                searchSql.AppendLine(" when '10' then '付款单' ");
                searchSql.AppendLine(" when '11' then '销售发货通知单' ");
                searchSql.AppendLine(" end as  TemTypeName ");
                searchSql.AppendLine("      ,a.Remark                   ");
                searchSql.AppendLine("      , CONVERT(varchar(100), a.CreateDate, 23) CreateDate ");
                searchSql.AppendLine("      ,a.TemType,a.UsedStatus,case(a.UsedStatus) when '1' then '启用' when '0' then '停用' end as UsedStatusName,b.EmployeeName  as  CreatorName,a.Creator ");
                searchSql.AppendLine("  FROM officedba.VoucherTemplate a ");
                searchSql.AppendLine("  left join officedba.EmployeeInfo b on b.id = a.Creator ");
                searchSql.AppendLine(" WHERE a.CompanyCD = @CompanyCD   ");

                #endregion
                #region 定义查询的命令
                SqlCommand comm = new SqlCommand();
                //公司代码
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

                //模板编号
                if (!string.IsNullOrEmpty(VoucherTemplateM.TemNo))
                {
                    searchSql.AppendLine("	AND a.TemNo LIKE  '%' + @TemNo + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemNo", VoucherTemplateM.TemNo));
                }
                //摘要
                if (!string.IsNullOrEmpty(VoucherTemplateM.Abstract))
                {
                    searchSql.AppendLine("	AND a.Abstract LIKE  '%' + @Abstract + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Abstract", VoucherTemplateM.Abstract));
                }
                //模板类别
                if (VoucherTemplateM.TemType != 0)
                {
                    searchSql.AppendLine("	AND a.TemType=@TemType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TemType", VoucherTemplateM.TemType.ToString()));
                }
               
                #endregion

                //设定comm的SQL文
                comm.CommandText = searchSql.ToString();

                //执行查询
                return SqlHelper.ExecuteSearch(comm);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
