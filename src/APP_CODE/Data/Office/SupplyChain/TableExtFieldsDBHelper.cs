using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Office.SupplyChain
{
    public class TableExtFieldsDBHelper
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public static bool Add(TableExtFields tableExtFields, out string strMsg, string CustomValues)
        //{
        //    int EFIndex = GetEFIndex(tableExtFields);//索引
        //    bool isSucc = false;//是否成功
        //    if (EFIndex != 0)
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("insert into officedba.TableExtFields(");
        //        strSql.Append("TabName,CompanyCD,EFIndex,EFDesc,EFType,EFValueList)");
        //        strSql.Append(" values (");
        //        strSql.Append("'officedba.ProductInfo',@CompanyCD,@EFIndex,@EFDesc,@EFType,@EFValueList)");
        //        tableExtFields.EFIndex = EFIndex;
        //        SqlParameter[] parameters = {			
        //            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
        //            new SqlParameter("@EFIndex", SqlDbType.Int,4),
        //            new SqlParameter("@EFDesc", SqlDbType.VarChar,20),
        //            new SqlParameter("@EFType", SqlDbType.Char,1),
        //            new SqlParameter("@EFValueList", SqlDbType.VarChar,256)};

        //        parameters[0].Value = tableExtFields.CompanyCD;
        //        parameters[1].Value = tableExtFields.EFIndex;
        //        parameters[2].Value = tableExtFields.EFDesc;
        //        parameters[3].Value = tableExtFields.EFType;
        //        parameters[4].Value = tableExtFields.EFValueList;
        //        foreach (SqlParameter para in parameters)
        //        {
        //            if (para.Value == null)
        //            {
        //                para.Value = DBNull.Value;
        //            }
        //        }

        //        try
        //        {
        //            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        //            isSucc = SqlHelper.Result.OprateCount > 0 ? true : false;
        //            strMsg = "保存成功！";
        //        }
        //        catch(Exception ex)
        //        {
        //            strMsg = "保存失败，请联系系统管理员！";
        //            isSucc = false;
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        strMsg = "已达最大数量，您不能添加新的商品特性！";
        //    }
        //    return isSucc;
        //}

        public static bool Add(TableExtFields tableExtFields, out string strMsg, string CustomValues)
        {
            try
            {
                bool result = false;
                SqlCommand cmd = null;
                StringBuilder sql = null;
                ArrayList cmdlist = new ArrayList();
                string[] CustomValuesArray = CustomValues.Split('$');
                if (CustomValuesArray.Length > 0)
                {
                    for (int i = 0; i < CustomValuesArray.Length; i++)
                    {
                        cmd = new SqlCommand();
                        sql = new StringBuilder();

                        sql.AppendLine("insert into officedba.TableExtFields( ");
                        sql.AppendLine("FunctionModule,ModelNo,TabName,CompanyCD,EFIndex,EFDesc,EFType) ");
                        sql.AppendLine(" values ( ");
                        sql.AppendLine("@FunctionModule,@ModelNo,@TableName,@CompanyCD,@EFIndex,@EFDesc,'1')");

                        cmd.Parameters.AddWithValue("@FunctionModule", SqlDbType.VarChar).Value = tableExtFields.FunctionModule;
                        cmd.Parameters.AddWithValue("@ModelNo", SqlDbType.VarChar).Value = tableExtFields.ModelNo;
                        cmd.Parameters.AddWithValue("@TableName", SqlDbType.VarChar).Value = tableExtFields.TabName;
                        cmd.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = tableExtFields.CompanyCD;
                        //cmd.Parameters.AddWithValue("@BranchID", SqlDbType.Int).Value = Convert.ToInt32(tableExtFields.BranchID);
                        cmd.Parameters.AddWithValue("@EFIndex", SqlDbType.Int).Value = (i + 1);
                        cmd.Parameters.AddWithValue("@EFDesc", SqlDbType.VarChar).Value = CustomValuesArray[i].ToString();

                        cmd.CommandText = sql.ToString();
                        cmdlist.Add(cmd);
                    }
                }
                result = SqlHelper.ExecuteTransWithArrayList(cmdlist);
                strMsg = "保存成功";
                return result;
            }
            catch
            {
                strMsg = "保存失败";
                return false;
            }
        }

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <returns></returns>
        private static int GetEFIndex(TableExtFields tableExtFields)
        {
            int EFIndex = 0;//索引
            int iCount = 0;//使用字段合计
            string strSql = string.Empty;
            strSql = "select count(1) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo'";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD)
                                        };
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, parameters));
            //总使用扩展字段小于三十，获取索引，等于三十时不可添加新扩展字段
            if (iCount < 30)
            {
                strSql = "select isnull(max(EFIndex),0) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo'";
                SqlParameter[] parameters1 = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD)
                                        };
                EFIndex = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, parameters1));
                //最大索引未到三十就使用最大索引+1作为当前索引
                if (EFIndex < 30)
                {
                    EFIndex += 1;
                }
                //当最大索引为三十时，查找小于三十的未被使用的索引
                if (EFIndex == 30)
                {
                    for (int i = 1; i < 30; i++)
                    {
                        strSql = "select count(1) as EFIndex from officedba.TableExtFields where CompanyCD=@CompanyCD and TabName='officedba.ProductInfo' and EFIndex=@EFIndex";
                        SqlParameter[] paras = { 
                                            new SqlParameter("@CompanyCD",tableExtFields.CompanyCD),
                                            new SqlParameter("@EFIndex",i)
                                        };
                        iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
                        if (iCount == 0)
                        {
                            EFIndex = i;
                            break;
                        }
                    }
                }
            }
            return EFIndex;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable Getlist(string FunctionModule,string FormType,string CompanyCD, string BranchID, string EFDesc, string ModelNo, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select DISTINCT ModelNo,ID='',Case FunctionModule when '1' THEN '销售管理'");
            sb.Append(" when '2' then '采购管理' ");
            sb.Append(" when '3' then '库存管理' ");
            sb.Append(" when '4' then '生产管理' ");
            sb.Append(" when '5' then '质检管理' ");
            sb.Append(" when '6' then '门店配送' ");
            sb.Append(" when '7' then '门店管理' ");
            sb.Append(" when '8' then '物品档案' ");
            sb.Append(" END  FunctionModule,");
            sb.Append(" case TabName when 'officedba.SellPlan' then '销售计划' when 'officedba.SellChance' then '销售机会' ");
            sb.Append(" when 'officedba.SellOffer' then '销售报价单' when 'officedba.SellContract' then '销售合同' when 'officedba.SellOrder' then '销售订单' ");
            sb.Append(" when 'officedba.SellSend' then '销售发货通知单' when 'officedba.SellGathering' then '回款计划' when 'officedba.SellBack' then '销售退货单' ");
            sb.Append(" when 'officedba.SellChannelSttl' then '委托代销结算单' when 'officedba.PurchaseApply' then '采购申请单' ");
            sb.Append(" when 'officedba.PurchasePlan' then '采购计划单' when 'officedba.PurchaseAskPrice' then '采购询价单' ");
            sb.Append(" when 'officedba.PurchaseContract' then '采购合同' ");
            sb.Append(" when 'officedba.PurchaseOrder' then '采购订单' ");
            sb.Append(" when 'officedba.PurchaseArrive' then '采购到货单' ");
            sb.Append(" when 'officedba.PurchaseReject' then '采购退货单' ");
            sb.Append(" when 'officedba.StorageInPurchase' then '采购入库单' ");
            sb.Append(" when 'officedba.StorageInProcess' then '生产完工入库单' ");
            sb.Append(" when 'officedba.StorageInOther' then '其他入库单' ");
            sb.Append(" when 'officedba.StorageInRed' then '红冲入库单' ");
            sb.Append(" when 'officedba.StorageOutSell' then '销售出库单' ");
            sb.Append(" when 'officedba.StorageOutOther' then '其他出库单' ");
            sb.Append(" when 'officedba.StorageOutRed' then '红冲出库单' ");
            sb.Append(" when 'officedba.StorageBorrow' then '借货单' ");
            sb.Append(" when 'officedba.StorageReturn' then '借货返还单' ");
            sb.Append(" when 'officedba.StorageTransfer' then '库存调拨单' ");
            sb.Append(" when 'officedba.StorageAdjust' then '库存调整单' ");
            sb.Append(" when 'officedba.StorageCheck' then '盘点单' ");
            sb.Append(" when 'officedba.StorageLoss' then '库存报损单' ");
            sb.Append(" when 'officedba.MasterProductSchedule' then '主生产计划' ");
            sb.Append(" when 'officedba.MRP' then '物料需求计划' ");
            sb.Append(" when 'officedba.ManufactureReport' then '生产任务单汇报' ");
            sb.Append(" when 'officedba.ManufactureTask' then '生产任务单' ");
            sb.Append(" when 'officedba.TakeMaterial' then '领料单' ");
            sb.Append(" when 'officedba.BackMaterial' then '退料单' ");
            sb.Append(" when 'officedba.QualityCheckApplay' then '质检申请单' ");
            sb.Append(" when 'officedba.QualityCheckReport' then '质检报告单' ");
            sb.Append(" when 'officedba.CheckNotPass' then '不合格品处置单' ");
            sb.Append(" when 'officedba.SubDeliverySend' then '配送单' ");
            sb.Append(" when 'officedba.SubDeliveryBack' then '配送退货单' ");
            sb.Append(" when 'officedba.SubDeliveryTrans' then '分店调拨单' ");
            sb.Append(" when 'officedba.SubStorageIn' then '分店入库单' ");
            sb.Append(" when 'officedba.SubSellOrder' then '分店销售订单' ");
            sb.Append(" when 'officedba.SubSellBack' then '分店销售退货单' ");
            sb.Append(" when 'officedba.ProjectInfo' then '项目档案' ");
            //sb.Append(" when 'officedba.ProductInfo' then '物品特性' ");
            sb.Append(" end as TabName from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD AND TabName NOT IN ('officedba.ProductInfo')");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            // arr.Add(new SqlParameter("@BranchID", BranchID));
            if (FunctionModule != "")
            {
                sb.Append(" and  FunctionModule=@FunctionModule ");
                arr.Add(new SqlParameter("@FunctionModule", FunctionModule));
            }
            if (FormType != "")
            {
                sb.Append(" and  TabName=@FormType ");
                arr.Add(new SqlParameter("@FormType", FormType));
            }
            if (EFDesc != "")
            {
                sb.Append(" and  EFDesc like @EFDesc ");
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            if (ModelNo != "")
            {
                sb.Append(" and  ModelNo like @ModelNo ");
                arr.Add(new SqlParameter("@ModelNo", "%" + ModelNo + "%"));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 初始化商品档案页面获取所有字段
        /// </summary>
        /// <param name="EFDesc">描述</param>
        /// <param name="EFType">类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetAllList(string CompanyCD, string BranchID, string TableName)
        {
            // DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,EFIndex,EFDesc+char(31) EFDesc,EFType,EFValueList from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and TabName='" + TableName + "'  ORDER BY EFType, ID asc ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            //arr.Add(new SqlParameter("@BranchID", BranchID));
            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }
        /// <summary>
        /// 编辑查看
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ModelNo"></param>
        /// <returns></returns>
        public static DataTable GetDataByNo(string CompanyCD, string BranchID, string ModelNo)
        {
            // DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and ModelNo='" + ModelNo + "' ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            //arr.Add(new SqlParameter("@BranchID", BranchID));

            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(TableExtFields tableExtFields, out string strMsg, string CustomValues)
        {
            try
            {
                bool result = false;
                SqlCommand cmd = null;
                StringBuilder sql = null;
                SqlCommand delcmd = null;
                StringBuilder delsql = null;
                ArrayList cmdlist = new ArrayList();
                #region 更新前先删除
                delcmd = new SqlCommand();
                delsql = new StringBuilder();

                delsql.AppendLine("DELETE FROM  officedba.TableExtFields WHERE ModelNo=@ModelNo AND CompanyCD=CompanyCD ");

                delcmd.Parameters.AddWithValue("@ModelNo", SqlDbType.VarChar).Value = tableExtFields.ModelNo;
                delcmd.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = tableExtFields.CompanyCD;
                //delcmd.Parameters.AddWithValue("@BranchID", SqlDbType.Int).Value = Convert.ToInt32(tableExtFields.BranchID);
                delcmd.CommandText = delsql.ToString();
                cmdlist.Add(delcmd);
                #endregion
                string[] CustomValuesArray = CustomValues.Split('$');
                if (CustomValuesArray.Length > 0)
                {
                    for (int i = 0; i < CustomValuesArray.Length; i++)
                    {
                        cmd = new SqlCommand();
                        sql = new StringBuilder();

                        sql.AppendLine("insert into officedba.TableExtFields( ");
                        sql.AppendLine("FunctionModule,ModelNo,TabName,CompanyCD,EFIndex,EFDesc,EFType) ");
                        sql.AppendLine(" values ( ");
                        sql.AppendLine("@FunctionModule,@ModelNo,@TableName,@CompanyCD,@EFIndex,@EFDesc,'1')");

                        cmd.Parameters.AddWithValue("@FunctionModule", SqlDbType.VarChar).Value = tableExtFields.FunctionModule;
                        cmd.Parameters.AddWithValue("@ModelNo", SqlDbType.VarChar).Value = tableExtFields.ModelNo;
                        cmd.Parameters.AddWithValue("@TableName", SqlDbType.VarChar).Value = tableExtFields.TabName;
                        cmd.Parameters.AddWithValue("@CompanyCD", SqlDbType.VarChar).Value = tableExtFields.CompanyCD;
                        //cmd.Parameters.AddWithValue("@BranchID", SqlDbType.Int).Value = Convert.ToInt32(tableExtFields.BranchID);
                        cmd.Parameters.AddWithValue("@EFIndex", SqlDbType.Int).Value = (i + 1);
                        cmd.Parameters.AddWithValue("@EFDesc", SqlDbType.VarChar).Value = CustomValuesArray[i].ToString();

                        cmd.CommandText = sql.ToString();
                        cmdlist.Add(cmd);
                    }
                }
                result = SqlHelper.ExecuteTransWithArrayList(cmdlist);
                strMsg = "保存成功";
                return result;
            }
            catch
            {
                strMsg = "保存失败";
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <param name="IDS">id列表</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool Delete(string strCompanyCD, string IDS, string BranchID, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除

            string[] orderNoS = null;
            orderNoS = IDS.Split(',');
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < orderNoS.Length; i++)
            {
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                try
                {
                    SqlHelper.ExecuteScalar("DELETE from officedba.TableExtFields  where CompanyCD='" + strCompanyCD + "' and  ModelNo in(" + allOrderNo + ")", null);
                    isSucc = true;

                    strMsg = "删除成功！";
                }
                catch
                {

                    strMsg = "删除失败，请联系系统管理员！";
                    isSucc = false;
                }
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }

        /// <summary>
        /// 属性是否可以删除
        /// </summary>
        /// <returns></returns>
        private static bool isDel(string id, string strCompanyCD)
        {
            bool isSuc = false;
            string strSql = string.Empty;
            string strKeyName = string.Empty;//要删除的字段名
            int index = 0;//要删除属性的索引值
            index = Convert.ToInt32(SqlHelper.ExecuteScalar("select isnull(EFIndex,0) as EFIndex from officedba.TableExtFields where ID=" + id, null));
            strKeyName = "ExtField" + index.ToString();
            strSql = "SELECT COUNT(1) FROM officedba.ProductInfo WHERE (" + strKeyName + " IS NOT NULL) AND (CompanyCD = '" + strCompanyCD + "') AND (" + strKeyName + " <> '') ";
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, null)) == 0)
            {
                isSuc = true;
            }
            return isSuc;
        }

        #endregion  成员方法
        #region 是否有重复编号
        /// <summary>
        /// 是否有重复编号
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ModelNo"></param>
        /// <returns></returns>
        public static DataTable IsHaveRepeatNo(string CompanyCD, string BranchID, string ModelNo)
        {
            // DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and ModelNo='" + ModelNo + "'  ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            //arr.Add(new SqlParameter("@BranchID", BranchID));

            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }
        #endregion
        #region 表中是否已经有扩展属性
        /// <summary>
        /// 表中是否已经有扩展属性
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="ModelNo"></param>
        /// <returns></returns>
        public static DataTable IsHaveAttr(string CompanyCD, string BranchID, string TableName)
        {
            // DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from officedba.TableExtFields");
            sb.Append(" where CompanyCD=@CompanyCD and TabName='" + TableName + "'  ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            //arr.Add(new SqlParameter("@BranchID", BranchID));

            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }
        #endregion
    }
}
