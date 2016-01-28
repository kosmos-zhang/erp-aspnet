/**********************************************
 * 类作用：   成本核算业务处理
 * 建立人：   江贻明
 * 建立时间： 2009/09/9
 ***********************************************/
using System;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using System.Data;
using System.Collections;

namespace XBase.Business.Office.ProductionManager
{
  public class CostProductionBus
    {

      #region 添加成本核算信息
      public static int InsertCostProductionInfo(ProcutionCostModel model, ArrayList Detailsmodels)
      {
          if (model == null || Detailsmodels == null) return 0;
          try
          {
              return CostProductionDBHelper.InsertCostProductionInfo(model, Detailsmodels);
          }
          catch(Exception ex)
          {
              throw ex;
          }
      }
      #endregion


    #region 删除成本核算信息
    public static bool DelCostProductionInfo(int ID)
    {
        return CostProductionDBHelper.DelCostProductionInfo(ID);
    }
    #endregion


    public static DataSet GetCostProductionInfoByProductID(int PID)
    {
        return CostProductionDBHelper.GetCostProductionInfoByProductID(PID);
    }
    public static DataSet GetCostProductionInfo(int CPID)
    {
        return CostProductionDBHelper.GetCostProductionInfo(CPID);
    }

    public static DataTable LoadList(string where, string orderby, int pageIndex, int pageSize, out int totalCnt)
    {
        return CostProductionDBHelper.LoadList(where, orderby, pageIndex, pageSize, out totalCnt);
    }
    public static DataTable ExportToExcel(string where, string orderby)
    {
        return CostProductionDBHelper.ExportToExcel(where, orderby);
    }
    }
}
