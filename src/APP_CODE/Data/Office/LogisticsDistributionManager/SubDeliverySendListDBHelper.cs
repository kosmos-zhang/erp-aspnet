using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.LogisticsDistributionManager;
namespace XBase.Data.Office.LogisticsDistributionManager
{
    public class SubDeliverySendListDBHelper
    {
        #region 读取配送单列表
        public static DataTable GetSubDeliverySendList(Hashtable htPara, string EFIndex, string EFDesc, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM  (");
            sbSql.Append("SELECT  sds.ID,sds.SendNo,sds.Title,sds.ApplyUserID,sds.ApplyDeptID,sds.OutDeptID,sds.RequireInDate,sds.SendPrice,sds.SendCount,sds.SendFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append("(select  di1.DeptName from officedba.DeptInfo as di1 where di1.ID=sds.OutDeptID ) as OutDeptIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus  from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliverySend_NO + "  AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + "   order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliverySend as sds where 1=1 AND sds.CompanyCD=@CompanyCD  ");


            string FlowStatusSql = string.Empty;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }

            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;

            #region 构造查询条件
            if (htPara.ContainsKey("@SendNo"))
            {
                sbSql.Append(" AND sds.SendNo LIKE @SendNo");
                Paras = new SqlParameter("@SendNo", SqlDbType.VarChar);
                Paras.Value = htPara["@SendNo"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@Title"))
            {
                sbSql.Append(" AND sds.Title LIKE @Title");
                Paras = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras.Value = htPara["@Title"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BatchNo"))
            {
                sbSql.Append(" AND sds.BatchNo LIKE @BatchNo ");
                Paras = new SqlParameter("@BatchNo", SqlDbType.VarChar);
                Paras.Value = htPara["@BatchNo"];
                list.Add(Paras);
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.Append(" and sds.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras.Value = "%" + EFDesc + "%";
                list.Add(Paras);
            }

            if (htPara.ContainsKey("@ApplyUserID"))
            {
                sbSql.Append(" AND sds.ApplyUserID=@ApplyUserID");
                Paras = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyUserID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@ApplyDeptID"))
            {
                sbSql.Append(" AND sds.ApplyDeptID=@ApplyDeptID");
                Paras = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@RequireInDate"))
            {
                sbSql.Append(" AND sds.RequireInDate=@RequireInDate");
                Paras = new SqlParameter("@RequireInDate", SqlDbType.DateTime);
                Paras.Value = htPara["@RequireInDate"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@OutDeptID"))
            {
                sbSql.Append(" AND sds.OutDeptID=@OutDeptID");
                Paras = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras.Value = htPara["@OutDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BusiStatus"))
            {
                sbSql.Append(" AND sds.BusiStatus=@BusiStatus");
                Paras = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BusiStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BillStatus"))
            {
                sbSql.Append(" AND sds.BillStatus=@BillStatus");
                Paras = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BillStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@CompanyCD"))
            {
                Paras = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                Paras.Value = htPara["@CompanyCD"];
                list.Add(Paras);
            }
            #endregion

            sbSql.Append(" ) as tmpt where 1=1 " + FlowStatusSql);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), Convert.ToInt32(htPara["@PageIndex"].ToString()), Convert.ToInt32(htPara["@PageSize"]), htPara["@OrderBy"].ToString(), list.ToArray(), ref TotalCount);

        }

        /*不分页*/
        public static DataTable GetSubDeliverySendList(Hashtable htPara, string EFIndex, string EFDesc)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT * FROM  (");
            sbSql.Append("SELECT  sds.ID,sds.SendNo,sds.Title,sds.ApplyUserID,sds.ApplyDeptID,sds.OutDeptID,sds.RequireInDate,sds.SendPrice,sds.SendCount,sds.SendFeeSum,sds.busiStatus, ");
            sbSql.Append(" (select ei1.EmployeeName from officedba.EmployeeInfo  as ei1  where ei1.ID=sds.ApplyUserID) as ApplyUserIDName,");
            sbSql.Append("(select  di1.DeptName from officedba.DeptInfo as di1 where di1.ID=sds.OutDeptID ) as OutDeptIDName,");
            sbSql.Append(" (select di2.DeptName from officedba.DeptInfo as di2 where di2.ID=sds.ApplyDeptID) as ApplyDeptIDName,");
            sbSql.Append("(CASE sds.BillStatus WHEN '1' THEN '制单' when '2' THEN '执行' when '3' THEN '变更' when '4' THEN '手工结单' when '5' then '自动结单' end) as BillStatus,( select top 1 fi.FlowStatus  from  officedba.FlowInstance AS FI  where fi.BillID=sds.ID  and fi.CompanyCD=sds.CompanyCD  AND fi.BillTypeCode=" + XBase.Common.ConstUtil.TYPECODE_SubDeliverySend_NO + "  AND fi.BillTypeFlag=" + XBase.Common.ConstUtil.TYPEFLAG_LogisticsDistribution_NO + "   order by FI.ModifiedDate DESC) as FlowStatus ");
            sbSql.Append("  FROM officedba.SubDeliverySend as sds where 1=1 AND sds.CompanyCD=@CompanyCD  ");


            string FlowStatusSql = string.Empty;
            if (htPara.ContainsKey("@FlowStatus"))
            {
                int SubmitStatus = Convert.ToInt32(htPara["@FlowStatus"].ToString());
                if (SubmitStatus > 0)
                    FlowStatusSql = "  AND  FlowStatus=" + htPara["@FlowStatus"].ToString();
                else if (SubmitStatus == 0)
                    FlowStatusSql = "  AND FlowStatus is null ";
            }

            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter Paras = null;
            #region 构造查询条件

            if (htPara.ContainsKey("@SendNo"))
            {
                sbSql.Append(" AND sds.SendNo LIKE @SendNo");
                Paras = new SqlParameter("@SendNo", SqlDbType.VarChar);
                Paras.Value = htPara["@SendNo"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@Title"))
            {
                sbSql.Append(" AND sds.Title LIKE @Title");
                Paras = new SqlParameter("@Title", SqlDbType.VarChar);
                Paras.Value = htPara["@Title"];
                list.Add(Paras);
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sbSql.Append(" and sds.ExtField" + EFIndex + " LIKE @EFDesc ");
                Paras = new SqlParameter("@EFDesc", SqlDbType.VarChar);
                Paras.Value = "%" + EFDesc + "%";
                list.Add(Paras);
            }

            if (htPara.ContainsKey("@ApplyUserID"))
            {
                sbSql.Append(" AND sds.ApplyUserID=@ApplyUserID");
                Paras = new SqlParameter("@ApplyUserID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyUserID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@ApplyDeptID"))
            {
                sbSql.Append(" AND sds.ApplyDeptID=@ApplyDeptID");
                Paras = new SqlParameter("@ApplyDeptID", SqlDbType.Int);
                Paras.Value = htPara["@ApplyDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@RequireInDate"))
            {
                sbSql.Append(" AND sds.RequireInDate=@RequireInDate");
                Paras = new SqlParameter("@RequireInDate", SqlDbType.DateTime);
                Paras.Value = htPara["@RequireInDate"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@OutDeptID"))
            {
                sbSql.Append(" AND sds.OutDeptID=@OutDeptID");
                Paras = new SqlParameter("@OutDeptID", SqlDbType.Int);
                Paras.Value = htPara["@OutDeptID"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BusiStatus"))
            {
                sbSql.Append(" AND sds.BusiStatus=@BusiStatus");
                Paras = new SqlParameter("@BusiStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BusiStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@BillStatus"))
            {
                sbSql.Append(" AND sds.BillStatus=@BillStatus");
                Paras = new SqlParameter("@BillStatus", SqlDbType.VarChar);
                Paras.Value = htPara["@BillStatus"];
                list.Add(Paras);
            }
            if (htPara.ContainsKey("@CompanyCD"))
            {
                Paras = new SqlParameter("@CompanyCD", SqlDbType.VarChar);
                Paras.Value = htPara["@CompanyCD"];
                list.Add(Paras);
            }
            #endregion

            sbSql.Append(" ) as tmpt where 1=1 " + FlowStatusSql);
            sbSql.Append(" ORDER BY " + htPara["@OrderBy"]);
            return SqlHelper.ExecuteSql(sbSql.ToString(), list.ToArray());

        }
        #endregion

        #region 删除
        public static bool DelSubDeliverySend(string[] IDList)
        {

            ArrayList SqlList = new ArrayList();
            for (int i = 0; i < IDList.Length; i++)
            {
                StringBuilder sbSubSql = new StringBuilder();
                sbSubSql.Append("DELETE officedba.SubDeliverySendDetail  where CompanyCD=(Select sds1.CompanyCD FROM officedba.SubDeliverySend as sds1 where sds1.ID=@ID) ");
                sbSubSql.Append(" AND SendNo=(SELECT sds2.SendNo from officedba.SubDeliverySend as sds2 where sds2.ID=@ID)");
                SqlParameter[] SubParas = { new SqlParameter("@ID", SqlDbType.Int) };
                SubParas[0].Value = IDList[i];

                SqlCommand SqlSubCmd = new SqlCommand();
                SqlSubCmd.CommandText = sbSubSql.ToString();
                SqlSubCmd.Parameters.AddRange(SubParas);
                SqlList.Add(SqlSubCmd);

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("DELETE officedba.SubDeliverySend where ID=@ID");
                SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
                Paras[0].Value = IDList[i];
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.CommandText = sbSql.ToString();
                SqlCmd.Parameters.AddRange(Paras);
                SqlList.Add(SqlCmd);
            }

            bool res = SqlHelper.ExecuteTransWithArrayList(SqlList);
            return res;
        }
        #endregion
    }
}
