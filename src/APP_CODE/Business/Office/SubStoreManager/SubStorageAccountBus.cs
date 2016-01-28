
/**********************************************
 * 类作用   门店库存流水账表业务处理层
 * 创建人   xz
 * 创建时间 2010-4-20 15:09:51 
 ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Common;

using XBase.Model.Office.SubStoreManager;
using XBase.Data.Office.SubStoreManager;


namespace XBase.Business.Office.SubStoreManager
{
    /// <summary>
    /// 门店库存流水账表业务类
    /// </summary>
    public class SubStorageAccountBus
    {
        #region 默认方法
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD">ID，自动生成</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return SubStorageAccountDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">公司编码</param>
        /// <param name="billType">单据类型</param>
        /// <param name="BillNo">单据编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(string companyCD, int billType, string BillNo)
        {
            return SubStorageAccountDBHelper.SelectWithKey(companyCD, billType, BillNo);
        }


        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , SubStorageAccountModel model)
        {
            return SubStorageAccountDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, model);
        }

        /// <summary>
        /// 插入数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>插入数据是否成功:true成功,false不成功</returns>
        public static bool Insert(SubStorageAccountModel model)
        {
            ArrayList sqlList = new ArrayList();
            SqlCommand cmd = SubStorageAccountDBHelper.InsertCommand(model);

            sqlList.Add(cmd);

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                int i = 0;
                if (int.TryParse(cmd.Parameters["@IndexID"].Value.ToString(), out i))
                {
                    model.ID = i;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        ///<param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(SubStorageAccountModel model)
        {
            return SubStorageAccountDBHelper.Update(model);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            return SubStorageAccountDBHelper.Delete(iD);
        }

        #endregion

        #region 自定义

        #endregion

    }
}



