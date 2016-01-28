using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellModuleSelectCustDBHelper
    {

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCustList(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql = "SELECT ID, CustNo, CustName, ArtiPerson, CustNote, Relation" +
                     " FROM officedba.CustInfo " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql += " and ( charindex('," + empid + ",' , ','+CanViewUser+',')>0 or Creator=" + empid + "or Manager=" + empid + " OR CanViewUser=',,' OR CanViewUser is null) ";
            
            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";

            }
            if (model != "all")
            {
                strSql += " and  UsedStatus = '1' ";
            }
           
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
        }

        /// <summary>
        /// 获取客户详细信息
        /// </summary>
        /// <param name="strID">客户编号</param>
        /// <returns></returns>
        public static DataTable GetCustInfo(string strID)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           

            strSql = "SELECT isnull(officedba.CustInfo.CustType,'') as CustType , " +
                     " officedba.CustInfo.CurrencyType, officedba.CustInfo.TakeType, " +
                     " officedba.CustInfo.PayType,officedba.CustInfo.Tel,officedba.CustInfo.MoneyType, officedba.CustInfo.BusiType," +
                     " officedba.CustInfo.CarryType, officedba.CustInfo.CustName, " +
                     " officedba.CodePublicType.TypeName, officedba.CurrencyTypeSetting.ExchangeRate, officedba.CurrencyTypeSetting.CurrencyName " +
                     " FROM officedba.CustInfo LEFT OUTER JOIN " +
                     " officedba.CodePublicType ON officedba.CustInfo.CustType = officedba.CodePublicType.ID LEFT OUTER JOIN " +
                     " officedba.CurrencyTypeSetting ON officedba.CustInfo.CurrencyType = officedba.CurrencyTypeSetting.ID";
            strSql += " where officedba.CustInfo.ID='" + strID + "' and officedba.CustInfo.CompanyCD='" + strCompanyCD + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}
