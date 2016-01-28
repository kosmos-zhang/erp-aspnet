using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XBase.Model.Office.StorageManager;
using XBase.Data.Common;
using XBase.Common;

namespace XBase.Business.Common
{
    public class SubStorageInitBus
    {
        /// <summary>
        /// 初始化库存流水账--分店
        /// </summary>
        /// <returns></returns>
        public static void InitInfo()
        {
            /*获取已开通且使用进销存的企业*/
            DataTable CompanyDT = SubStorageInitDBHelper.GetDistinctCompany();
            foreach (DataRow row in CompanyDT.Rows)
            {
                /*获取某企业使用进销存的商品及仓库对应的记录*/
                DataTable ProAndStroDT = SubStorageInitDBHelper.GetDistinctProductAndStronge(row["CompanyCD"].ToString());

                /*获取某企业使用进销存的日期记录*/
                DataTable CompanyTimedt = SubStorageInitDBHelper.GetDistinctDate(row["CompanyCD"].ToString());
                string BeginDate = string.Empty;//企业使用进销存的开始日期
                string EndDate = string.Empty;
                if (CompanyTimedt.Rows.Count > 0 && CompanyTimedt != null)
                {
                    BeginDate = CompanyTimedt.Rows[0]["EnterDate"].ToString();
                    EndDate = CompanyTimedt.Rows[CompanyTimedt.Rows.Count - 1]["EnterDate"].ToString();
                }

                foreach (DataRow rowP in ProAndStroDT.Rows)
                {
                    /*获取该企业对应的物品，仓库的所有操作记录，按日期顺序取数据*/
                    DataTable CompanyInfoDT = SubStorageInitDBHelper.GetCompanyInfo(row["CompanyCD"].ToString(), rowP["ProductID"].ToString(), rowP["StorageID"].ToString());
                    decimal ret = 0;//计算累计现有存量
                    foreach (DataRow rowCom in CompanyInfoDT.Rows)
                    {
                        SubStorageAccountModel model = new SubStorageAccountModel();
                        if (!GetType(rowCom["flag"].ToString()))
                            ret += Convert.ToDecimal(0 - Convert.ToDecimal(rowCom["ProductCount"].ToString()));
                        else
                            ret += Convert.ToDecimal(rowCom["ProductCount"].ToString());//现有存量
                        model.BillNo = rowCom["NoCode"].ToString();//业务单编号
                        model.BillType = int.Parse(rowCom["flag"].ToString());//单据类型
                        model.CompanyCD = rowCom["CompanyCD"].ToString();//企业编号
                        model.HappenCount = Convert.ToDecimal(rowCom["ProductCount"].ToString());//出入库数量
                        model.HappenDate = Convert.ToDateTime(rowCom["EnterDate"].ToString());//出入库时间
                        model.Price = Convert.ToDecimal(rowCom["Price"].ToString());//单价
                        model.ProductID = int.Parse(rowCom["ProductID"].ToString());//物品
                        model.DeptID = int.Parse(rowCom["StorageID"].ToString());//仓库
                        model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//创建人
                        model.ProductCount = ret;//现有存量
                        model.PageUrl = "";//跳转URL
                        model.Remark = "";//备注

                        SubStorageInitDBHelper.InsertStorageAccount(model);//插入库存流水账
                    }
                   
                }
            }
        }


        /// <summary>
        /// 根据订单类别 判断出入库类型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns>true:入库 false 出库</returns>
        public static bool GetType(string TypeValue)
        {
            bool ret = true;
            switch (TypeValue)
            {
                case "3": ret = false;
                    break;
                case "5": ret = false;
                    break;
                case "7": ret = false;
                    break;
                default:
                    break;

            }
            return ret;
        }
    }
}
