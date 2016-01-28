using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using XBase.Model.CustomAPI.CustomWebSite;
using XBase.Data.DBHelper;

namespace XBase.Data.CustomAPI.CustomWebSite
{
    public class WebSiteOrderDBHelper
    {
        /*定义表架构*/
        private const string SYS_SCHEMANAME = "websitedba";

        #region 接口站点下单方法
        /// <summary>
        /// 接口站点下单方法
        /// </summary>
        /// <param name="model">订单主表信息</param>
        /// <param name="modelList">订单明细集合</param>
        /// <returns>true：成功，false：失败</returns>
        public static bool Create(WebSiteSellOrderModel model, List<WebSiteSellOrderDetailModel> modelList)
        {
            /*用不保存SQL命令*/
            List<SqlCommand> CmdList = new List<SqlCommand>();

            #region 主表信息
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" INSERT INTO " + SYS_SCHEMANAME + ".WebStieSellOrder ");
            sbSql.AppendLine(" (OrderNo,CompanyCD,CustomID,Title,OrderDate,ConsignmentDate,Status,LoginID) VALUES ");
            sbSql.AppendLine(" (@OrderNo,@CompanyCD,@CustomID,@Title,@OrderDate,@ConsignmentDate,@Status,@LoginID )");

            SqlParameter[] Params = new SqlParameter[8];
            int index = 0;

            Params[index++] = SqlHelper.GetParameter("@OrderNo", model.OrderNo);
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@CustomID", model.CustomID);
            Params[index++] = SqlHelper.GetParameter("@Title", model.Title);
            Params[index++] = SqlHelper.GetParameter("@OrderDate", model.OrderDate);
            Params[index++] = SqlHelper.GetParameter("@ConsignmentDate", model.ConsignmentDate);
            Params[index++] = SqlHelper.GetParameter("@Status", model.Status);
            Params[index++] = SqlHelper.GetParameter("@LoginID", model.LoginID);

            SqlCommand mainCmd = new SqlCommand(sbSql.ToString());
            mainCmd.Parameters.AddRange(Params);

            CmdList.Add(mainCmd);

            #endregion

            #region 明细信息
            foreach (WebSiteSellOrderDetailModel item in modelList)
            {

                SqlParameter[] DetailParams = null;
                SqlCommand cmd = new SqlCommand(GetDetailSql(item,ref  DetailParams));
                cmd.Parameters.AddRange(DetailParams);
                CmdList.Add(cmd);
            }
            #endregion

            return SqlHelper.ExecuteTransWithCollections(CmdList);

        }
        #endregion

        #region 获取添加明细SQL Private
        private static  string GetDetailSql(WebSiteSellOrderDetailModel model, ref SqlParameter[] Params)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" INSERT INTO " + SYS_SCHEMANAME + ".WebSiteSellOrderDetail ");
            sbSql.AppendLine(" (CompanyCD,OrderNo,ProductID,BasePrice,BaseCount,UsedUnitID,UsedCount,ExRate,BaseUnitID,UsedPrice,TotalPrice) VALUES ");
            sbSql.AppendLine(" (@CompanyCD,@OrderNo,@ProductID,@BasePrice,@BaseCount,@UsedUnitID,@UsedCount,@ExRate,@BaseUnitID,@UsedPrice,@TotalPrice)  ");

            Params = new SqlParameter[11];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@OrderNo", model.OrderNo);
            Params[index++] = SqlHelper.GetParameter("@ProductID", model.ProductID);
            Params[index++] = SqlHelper.GetParameter("@BasePrice", model.BasePrice);
            Params[index++] = SqlHelper.GetParameter("@BaseCount", model.BaseCount);
            Params[index++] = SqlHelper.GetParameter("@UsedUnitID", model.UsedUnitID);
            Params[index++] = SqlHelper.GetParameter("@UsedCount", model.UsedCount);
            Params[index++] = SqlHelper.GetParameter("@ExRate", model.ExRate);
            Params[index++] = SqlHelper.GetParameter("@BaseUnitID", model.BaseUnitID);
            Params[index++] = SqlHelper.GetParameter("@UsedPrice", model.UsedPrice);
            Params[index++] = SqlHelper.GetParameter("@TotalPrice", model.TotalPrice);

            return sbSql.ToString();
        }
        #endregion

        #region 修改订单方法
        /// <summary>
        /// 修改订单方法
        /// </summary>
        /// <param name="model">站点订单主表信息</param>
        /// <param name="modelList">站点订单明细信息</param>
        /// <returns>true:成功，false：失败</returns>
        public static bool Edit(WebSiteSellOrderModel model, List<WebSiteSellOrderDetailModel> modelList)
        {
            /*保存SQL命令*/
            List<SqlCommand> CmdList = new List<SqlCommand>();

            #region 主表信息
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" UPDATE " + SYS_SCHEMANAME + ".WebStieSellOrder SET ");
            sbSql.AppendLine(" Title=@Title,OrderDate=@OrderDate,ConsignmentDate=@ConsignmentDate ,Status=@Status");
            sbSql.AppendLine(" WHERE ID=@ID ");

            SqlParameter[] Params = new SqlParameter[5];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@Title", model.Title);
            Params[index++] = SqlHelper.GetParameter("@OrderDate", model.OrderDate);
            Params[index++] = SqlHelper.GetParameter("@ConsignmentDate", model.ConsignmentDate);
            Params[index++] = SqlHelper.GetParameter("@Status", model.Status);
            Params[index++] = SqlHelper.GetParameter("@ID", model.ID);

            SqlCommand MainCmd = new SqlCommand(sbSql.ToString());
            MainCmd.Parameters.AddRange(Params);
            CmdList.Add(MainCmd);
            #endregion

            #region 明细信息

            #region  清空以前的明细信息
            StringBuilder sbDel = new StringBuilder();
            sbDel.AppendLine(" DELETE " + SYS_SCHEMANAME + ".WebSiteSellOrderDetail WHERE OrderNo=@OrderNo AND CompanyCD=@CompanyCD  ");
            SqlParameter[] DelParams = new SqlParameter[2];
            DelParams[0] = SqlHelper.GetParameter("@OrderNo", model.OrderNo);
            DelParams[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);

            SqlCommand delCmd = new SqlCommand(sbDel.ToString());
            delCmd.Parameters.AddRange(DelParams);
            #endregion

            #region 添加新的明细
            foreach (WebSiteSellOrderDetailModel item in modelList)
            {
                SqlParameter[] detailParams = null;
                SqlCommand detailCmd = new SqlCommand(GetDetailSql(item, ref detailParams));
                detailCmd.Parameters.AddRange(detailParams);
                CmdList.Add(detailCmd);
            }
            #endregion

            #endregion

            return SqlHelper.ExecuteTransWithCollections(CmdList);

        }
        #endregion

        #region 读取订单列表
      /// <summary>
        /// 读取站点订单列表
      /// </summary>
      /// <param name="HtParams">参数集合 key:参数名，value:对应key的值</param>
      /// <returns>返回结果DataTable</returns>
        public static DataTable GetList(Hashtable HtParams,string OrderBy)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT *,(CASE Status WHEN '1' THEN '待处理' WHEN '2' THEN '处理中'  WHEN '3' THEN '发货中' WHEN '4' THEN '结单' END ) AS StatusName FROM " + SYS_SCHEMANAME + ".WebStieSellOrder ");
            sbSql.AppendLine(" WHERE 1=1 ");

            SqlParameter[] Params = new SqlParameter[HtParams.Count];
            int index = 0;

            #region 构造条件
            if (HtParams.ContainsKey("CustomID"))
            {
                sbSql.AppendLine(" CustomID=@CustomID ");
                Params[index++] = SqlHelper.GetParameter("@CustomID", HtParams["CustomID"].ToString());
            }
            if (HtParams.ContainsKey("CompanyCD"))
            {
                sbSql.AppendLine("CompanyCD=@CompanyCD ");
                Params[index++] = SqlHelper.GetParameter("@CompanyCD", HtParams["CompanyCD"].ToString());
            }
            if (HtParams.ContainsKey("OrderNo"))
            {
                sbSql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                Params[index++] = SqlHelper.GetParameter("@OrderNo", HtParams["OrderNo"].ToString());
            }
            if (HtParams.ContainsKey("Title"))
            {
                sbSql.AppendLine(" AND Title LIKE @Title ");
                Params[index++] = SqlHelper.GetParameter("@Title", HtParams["Title"].ToString());
            }
            if (HtParams.ContainsKey("OrderStartDate"))
            {
                sbSql.AppendLine(" AND OrderDate >=@OrderStartDate ");
                Params[index++] = SqlHelper.GetParameter("@OrderStartDate", HtParams["OrderStartDate"].ToString());
            }
            if (HtParams.ContainsKey("OrderEndDate"))
            {
                sbSql.AppendLine(" AND OrderDate <=@OrderEndDate ");
                Params[index++] = SqlHelper.GetParameter("@OrderEndDate", HtParams["OrderEndDate"].ToString());
            }
            if (HtParams.ContainsKey("ConsignmentStartDate"))
            {
                sbSql.AppendLine(" AND ConsignmentDate>=@ConsignmentStartDate");
                Params[index++] = SqlHelper.GetParameter("@ConsignmentStartDate", HtParams["ConsignmentStartDate"].ToString());
            }
            if (HtParams.ContainsKey("ConsignmentEndDate"))
            {
                sbSql.AppendLine(" AND ConsignmentDate<=@ConsignmentEndDate ");
                Params[index++] = SqlHelper.GetParameter("@ConsignmentStartDate", HtParams["ConsignmentStartDate"].ToString());
            }
            if (HtParams.ContainsKey("Status"))
            {
                sbSql.AppendLine(" AND Status=@Status ");
                Params[index++] = SqlHelper.GetParameter("@Status", HtParams["Status"].ToString());
            }
            #endregion

            sbSql.AppendLine(" ORDER BY " + OrderBy);/*追加排序字段*/

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="HtParams">参数集合 key:参数名，value:对应key的值</param>
        /// <param name="OrderBy">排序字段</param>
        /// <param name="PageIndex">索引页码</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="TotalCount">页数</param>
        /// <returns></returns>
        public static DataTable GetList(Hashtable HtParams,string OrderBy,int PageIndex,int PageSize,ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.*,(CASE a.Status WHEN '1' THEN '待处理' WHEN '2' THEN '处理中'  WHEN '3' THEN '发货中' WHEN '4' THEN '结单' END ) AS StatusName ");
            sbSql.AppendLine(" ,b.CustName ");
            sbSql.AppendLine(" FROM " + SYS_SCHEMANAME + ".WebStieSellOrder AS a  ");
            sbSql.AppendLine(" LEFT JOIN officedba.CustInfo AS b ON a.CustomID=b.ID ");

            sbSql.AppendLine(" WHERE 1=1 ");

            SqlParameter[] Params = new SqlParameter[HtParams.Count];
            int index = 0;

            #region 构造条件
            if (HtParams.ContainsKey("CustomID"))
            {
                sbSql.AppendLine(" AND CustomID=@CustomID ");
                Params[index++] = SqlHelper.GetParameter("@CustomID", HtParams["CustomID"].ToString());
            }
            if (HtParams.ContainsKey("OrderNo"))
            {
                sbSql.AppendLine(" AND OrderNo LIKE @OrderNo ");
                Params[index++] = SqlHelper.GetParameter("@OrderNo", HtParams["OrderNo"].ToString());
            }
            if (HtParams.ContainsKey("Title"))
            {
                sbSql.AppendLine(" AND Title LIKE @Title ");
                Params[index++] = SqlHelper.GetParameter("@Title", HtParams["Title"].ToString());
            }
            if (HtParams.ContainsKey("OrderDateStart"))
            {
                sbSql.AppendLine(" AND  Convert(datetime, convert(varchar(10),OrderDate))  >=@OrderDateStart ");
                Params[index++] = SqlHelper.GetParameter("@OrderDateStart", HtParams["OrderDateStart"].ToString());
            }
            if (HtParams.ContainsKey("OrderDateEnd"))
            {
                sbSql.AppendLine(" AND  Convert(datetime, convert(varchar(10),OrderDate)) <=@OrderDateEnd ");
                Params[index++] = SqlHelper.GetParameter("@OrderDateEnd", HtParams["OrderDateEnd"].ToString());
            }
            if (HtParams.ContainsKey("ConsignmentDateStart"))
            {
                sbSql.AppendLine(" AND Convert(datetime, convert(varchar(10),ConsignmentDate))>=@ConsignmentDateStart");
                Params[index++] = SqlHelper.GetParameter("@ConsignmentDateStart", HtParams["ConsignmentDateStart"].ToString());
            }
            if (HtParams.ContainsKey("ConsignmentDateEnd"))
            {
                sbSql.AppendLine(" AND Convert(datetime, convert(varchar(10),ConsignmentDate))<=@ConsignmentDateEnd ");
                Params[index++] = SqlHelper.GetParameter("@ConsignmentDateEnd", HtParams["ConsignmentDateEnd"].ToString());
            }
            if (HtParams.ContainsKey("Status"))
            {
                sbSql.AppendLine(" AND Status=@Status ");
                Params[index++] = SqlHelper.GetParameter("@Status", HtParams["Status"].ToString());
            }
            if (HtParams.ContainsKey("CompanyCD"))
            {
                sbSql.AppendLine(" AND a.CompanyCD=@CompanyCD");
                Params[index++] = SqlHelper.GetParameter("@CompanyCD", HtParams["CompanyCD"].ToString());
            }
            #endregion

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(),PageIndex,PageSize,OrderBy,Params,ref TotalCount);
        }



        #endregion

        #region 读取订单主表详细信息
        /// <summary>
        /// 读取订单详细信息
        /// </summary>
        /// <param name="OrderID">订单主键</param>
        /// <returns>包含订单信息的数据集合</returns>
        public static DataTable GetOrderInfo(string OrderNo,string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("  SELECT a.*,e.CustName,f.LoginUserName , (CASE a.Status WHEN '1' THEN '待处理' WHEN '2' THEN '处理中' WHEN '3' THEN '发货中' WHEN '4' THEN '结单' END) AS  StatusName ");
            sbSql.AppendLine("  FROM "+ SYS_SCHEMANAME + ".WebStieSellOrder AS a");
            sbSql.AppendLine(" LEFT JOIN officedba.CustInfo AS e ON e.ID=a.CustomID ");
            sbSql.AppendLine(" LEFT JOIN  " + SYS_SCHEMANAME + ".WebSiteCustomInfo AS f ON f.ID =a.LoginID ");
            sbSql.AppendLine(" WHERE a.OrderNo=@OrderNo AND a.CompanyCD=@CompanyCD ");

            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = SqlHelper.GetParameter("@OrderNo", OrderNo);
            Params[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);  
        }
        #endregion

        #region 读取明细信息
        public static DataTable GetDetailInfo(string OrderNo, string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT a.*,b.CodeName AS BaseUnitName,c.CodeName AS UsedUnitName,d.ProdNo,d.ProductName FROM " + SYS_SCHEMANAME + ".WebSiteSellorderDetail AS a ");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS b ON a.BaseUnitID=b.ID");
            sbSql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS c ON a.UsedUnitID=c.ID");
            sbSql.AppendLine(" LEFT JOIN officedba.ProductInfo AS d ON a.ProductID=d.ID ");
          
           
            sbSql.AppendLine(" WHERE a.OrderNo=@OrderNo AND a.CompanyCD=@CompanyCD ");

            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = SqlHelper.GetParameter("@OrderNo", OrderNo);
            Params[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion

        #region 验证某个登录用户是否有订单
        /// <summary>
        /// 验证某个登录用户名下是否有订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable  ExistsOrderByLoginUser(string id)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM ( ");
            sbSql.AppendLine(" SELECT b.LoginUserName,Count(*) AS OrderCount ");
            sbSql.AppendLine(" FROM websitedba.WebStieSellOrder AS a");
            sbSql.AppendLine(" LEFT JOIN websitedba.WebSiteCustomInfo AS b ON b.ID=a.LoginID ");
            sbSql.AppendLine(" WHERE a.LoginID IN (" + id + ")");
            sbSql.AppendLine(" GROUP BY b.LoginUserName ) AS a");
            sbSql.AppendLine(" WHERE OrderCount>0 ");

            return SqlHelper.ExecuteSql(sbSql.ToString());

        }
        #endregion

    }
}
