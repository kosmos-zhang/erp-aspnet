using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
namespace XBase.Data.Office.SellManager
{
    public class SellOrderActiveMoneyDBHelper
    {
        /// <summary>
        /// 定单应收费用+已收费用(部门)
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="getvalue"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyDetials(string companycd,string getvalue,string begindate,string enddate,string order,int pageindex,int pagesize,ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@getvalue",getvalue),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetSellOrderDetailsByDept]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    decimal RealTotal = Convert.ToDecimal(dr["RealTotal"] == null ? "0" : dr["RealTotal"].ToString());

                    dr["RealTotal"] = RealTotal.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);


                    decimal BlendingAmount = Convert.ToDecimal(dr["BlendingAmount"] == null ? "0" : dr["BlendingAmount"].ToString());

                    dr["BlendingAmount"] = BlendingAmount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);


                }
            }
            return dt;          
        }

        /// <summary>
        /// 定单应收费用+已收费用（业务员）
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="getvalue"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyDetials_person(string companycd, string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@getvalue",getvalue),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetSellOrderDetailsByperson]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    decimal RealTotal = Convert.ToDecimal(dr["RealTotal"] == null ? "0" : dr["RealTotal"].ToString());

                    dr["RealTotal"] = RealTotal.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);


                    decimal BlendingAmount = Convert.ToDecimal(dr["BlendingAmount"] == null ? "0" : dr["BlendingAmount"].ToString());

                    dr["BlendingAmount"] = BlendingAmount.ToString("F" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);


                }
            }
            return dt;
        }

        /// <summary>
        /// 实收金额(部门)
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="getvalue"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyDetialsActive(string companycd, string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@getvalue",getvalue),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetSellOrderActiveDetailsByDept]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
          
            return dt;
        }

        /// <summary>
        /// 实收金额(业务员)
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="getvalue"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyDetialsActive_Person(string companycd, string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@getvalue",getvalue),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetSellOrderActiveDetailsByPerson]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品销售走势
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="getvalue"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetOrderSellProductSetup(string companycd,int typeID,int timetype, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@typeID",typeID),
                new SqlParameter("@timetype",timetype),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetProductTimeSetup]", param);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品分类
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ddl"></param>
        public static void GetProductType(string companycd, System.Web.UI.WebControls.DropDownList ddl)
        {
            SqlParameter[] paramter = { new SqlParameter("@companyCD", companycd) };
            string sql = "select * from officedba.CodeProductType where CompanyCD=@companyCD";
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ddl.DataSource = ds;
            ddl.DataTextField = "CodeName";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("物品分类", "0"));
        }

        /// <summary>
        /// 物品销售走势明细
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable SellOrderProductBySetUpDetials(string companycd, string timeType, string timestr,int productType, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr),
                new SqlParameter("@productType",productType),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)

            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetProductTimeDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 物品销售分类
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="deptid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderProductType(string companycd, int deptid,string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@deptcode",deptid),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetSellOrderProductType]", param);
            return dt;
        }

    }
}
