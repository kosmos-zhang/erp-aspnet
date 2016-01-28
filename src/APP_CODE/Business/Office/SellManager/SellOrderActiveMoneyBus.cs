using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using XBase.Data.Office.SellManager;
using XBase.Common;
namespace XBase.Business.Office.SellManager
{
    public class SellOrderActiveMoneyBus
    {
        public static DataTable GetOrderMoneyDetials(string getvalue, string begindate, string enddate, string order,int pageindex,int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetOrderMoneyDetials(companyCD, getvalue, begindate, enddate, order,pageindex,pagesize, ref recordCount);
        }

        public static DataTable GetOrderMoneyDetials_person(string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetOrderMoneyDetials_person(companyCD, getvalue, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetOrderMoneyDetialsActive(string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetOrderMoneyDetialsActive(companyCD, getvalue, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetOrderMoneyDetialsActive_Person(string getvalue, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetOrderMoneyDetialsActive_Person(companyCD, getvalue, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetOrderSellProductSetup(int typeID, int timetype, string begindate, string enddate)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetOrderSellProductSetup(companyCD,typeID, timetype, begindate, enddate);
        }

        public static void GetProductType(System.Web.UI.WebControls.DropDownList ddl)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            SellOrderActiveMoneyDBHelper.GetProductType(companyCD, ddl);
        }

        public static DataTable SellOrderProductBySetUpDetials(string timeType, string timestr,int productType, string begindate, string enddate, string order, int pageindex, int pagesize, ref int recordCount)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.SellOrderProductBySetUpDetials(companyCD, timeType, timestr,productType, begindate, enddate, order, pageindex, pagesize, ref recordCount);
        }

        public static DataTable GetSellOrderProductType(int deptid, string begindate, string enddate)
        {
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            return SellOrderActiveMoneyDBHelper.GetSellOrderProductType(companyCD,deptid, begindate, enddate);
        }
    }
}
