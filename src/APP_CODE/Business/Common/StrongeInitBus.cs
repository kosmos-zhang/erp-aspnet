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
    public class StrongeInitBus
    {
       /// <summary>
       /// 初始化库存流水账--总店
       /// </summary>
       /// <returns></returns>
       public static void InitInfo()
       {
           /*获取已开通且使用进销存的企业*/
           DataTable CompanyDT = StrongeInit.GetDistinctCompany();
           foreach (DataRow row in CompanyDT.Rows)
           {
               /*获取某企业使用进销存的商品及仓库对应的记录*/
               DataTable ProAndStroDT = StrongeInit.GetDistinctProductAndStronge(row["CompanyCD"].ToString());

               /*获取某企业使用进销存的日期记录*/
               DataTable CompanyTimedt = StrongeInit.GetDistinctDate(row["CompanyCD"].ToString());
               string BeginDate = string.Empty;//企业使用进销存的开始日期
               string EndDate = string.Empty;
               if (CompanyTimedt.Rows.Count > 0 && CompanyTimedt != null)
               {
                   BeginDate = CompanyTimedt.Rows[0]["EnterDate"].ToString();
                   EndDate = CompanyTimedt.Rows[CompanyTimedt.Rows.Count-1]["EnterDate"].ToString();
               }

               foreach (DataRow rowP in ProAndStroDT.Rows)
               {
                   /*获取该企业对应的物品，仓库的所有操作记录，按日期顺序取数据*/
                   DataTable CompanyInfoDT = StrongeInit.GetCompanyInfo(row["CompanyCD"].ToString(), rowP["ProductID"].ToString(), rowP["StorageID"].ToString());
                   decimal ret = 0;//计算累计现有存量
                   foreach (DataRow rowCom in CompanyInfoDT.Rows)
                   {
                       StorageAccountModel model = new StorageAccountModel();
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
                       model.StorageID = int.Parse(rowCom["StorageID"].ToString());//仓库
                       model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//创建人
                       model.ProductCount = ret;//现有存量
                       model.PageUrl = "";//跳转URL
                       model.ReMark = "";//备注

                       StrongeInit.InsertStorageAccount(model);//插入库存流水账
                   }
                   decimal StorageProduct = StrongeInit.GetStorageProduct(row["CompanyCD"].ToString(), rowP["ProductID"].ToString(), rowP["StorageID"].ToString());
                   decimal DiffCount = StorageProduct - ret;//判断分仓存量表中的现有存量和计算出的现有存量之前是否存在差异，若存在差异，则说明该物品-仓库在前期存在批量导入.DiffCount为批量导入数量
                   if (DiffCount > 0)//若存在差异,则在该企业最初使用进销存当天插入批量导入信息
                   {
                       //获取符合条件的库存流水账的最大ID
                       int maxid = StrongeInit.GetMaxID(row["CompanyCD"].ToString(), rowP["ProductID"].ToString(), rowP["StorageID"].ToString());

                       //更新有批量导入记录物品的库存流水账记录
                       StrongeInit.UpdateStorageAccount(row["CompanyCD"].ToString(), rowP["ProductID"].ToString(), rowP["StorageID"].ToString(), maxid.ToString());

                       //更新最大一条记录的现有存量（有批量导入的物品）
                       StrongeInit.UpdateMaxAccount(maxid.ToString(), StorageProduct);

                       StorageAccountModel model = new StorageAccountModel();
                       
                       model.BillNo = "";//业务单编号
                       model.BillType = 2;//单据类型
                       model.CompanyCD = row["CompanyCD"].ToString();//企业编号
                       model.HappenCount = DiffCount;//出入库数量
                       model.HappenDate = Convert.ToDateTime(BeginDate);//出入库时间
                       model.Price = 0;//单价
                       model.ProductID = int.Parse(rowP["ProductID"].ToString());//物品
                       model.StorageID = int.Parse(rowP["StorageID"].ToString());//仓库
                       model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//创建人
                       model.ProductCount = 0;//现有存量
                       model.PageUrl = "";//跳转URL
                       model.ReMark = "在"+BeginDate+"到"+EndDate+"期间的批量导入总和";//备注

                       StrongeInit.InsertStorageAccount(model);//插入库存流水账
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
               case "6": ret = false;
                   break;
               case "7": ret = false;
                   break;
               case "8": ret = false;
                   break;
               case "10": ret = false;
                   break;
               case "12": ret = false;
                   break;
               case "16": ret = false;
                   break;
               case "17": ret = false;
                   break;
               case "19": ret = false;
                   break;
               case "21": ret = false;
                   break;
               default: 
                   break;

           }
           return ret;
       }
    }
}
