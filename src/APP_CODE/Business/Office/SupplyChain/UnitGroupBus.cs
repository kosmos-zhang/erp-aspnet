/**********************************************
 * 类作用   计量单位组数据处理层
 * 创建人   xz
 * 创建时间 2010-3-11 11:34:02 
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

using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;


namespace XBase.Business.Office.SupplyChain
{
    /// <summary>
    /// 计量单位组业务类
    /// </summary>
    public class UnitGroupBus
    {
        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            return UnitGroupDBHelper.SelectDataTable(iD);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="companyCD">企业代码</param>
        /// <param name="groupUnitNo">计量单位组编号</param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(string companyCD, string groupUnitNo)
        {
            return UnitGroupDBHelper.SelectDataTable(companyCD, groupUnitNo);
        }

        /// <summary>
        /// 计量单位组唯一验证
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <param name="GroupUnitNo">组代码</param>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public static bool CheckUnitNo(string companyCD, string GroupUnitNo, int? id)
        {
            return UnitGroupDBHelper.CheckUnitNo(companyCD, GroupUnitNo, id);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SaveData(UnitGroupModel model, IList<UnitGroupDetailModel> list)
        {

            ArrayList sqlList = new ArrayList();
            if (!model.ID.HasValue)
            {// 新增
                sqlList.Add(UnitGroupDBHelper.InsertCommand(model));
            }
            else
            {// 修改
                sqlList.Add(UnitGroupDBHelper.UpdateCommand(model));
                sqlList.Add(UnitGroupDetailDBHelper.DeleteCommand(model.CompanyCD, model.GroupUnitNo));
            }
            foreach (UnitGroupDetailModel info in list)
            {
                sqlList.Add(UnitGroupDetailDBHelper.InsertCommand(info));
            }

            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询界面列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="TotalCount"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="GroupUnitNo"></param>
        /// <param name="GroupUnitName"></param>
        /// <param name="BaseUnitID"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , string CompanyCD, string GroupUnitNo, string GroupUnitName, int? BaseUnitID, string Remark)
        {
            return UnitGroupDBHelper.SelectListData(pageIndex, pageCount, orderBy, ref TotalCount, CompanyCD, GroupUnitNo, GroupUnitName, BaseUnitID, Remark);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="GroupUnitNo"></param>
        /// <returns></returns>
        public static bool DeleteData(int[] iD, string CompanyCD, string[] GroupUnitNo)
        {
            ArrayList sqlList = new ArrayList();
            for (int i = 0; i < iD.Length; i++)
            {
                sqlList.Add(UnitGroupDBHelper.DeleteCommand(iD[i]));
                if (i < GroupUnitNo.Length)
                {
                    sqlList.Add(UnitGroupDetailDBHelper.DeleteCommand(CompanyCD, GroupUnitNo[i]));
                }
            }
            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
