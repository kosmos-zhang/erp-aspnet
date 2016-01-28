/**********************************************
 * 类作用：   设备配件添加数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/06
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
namespace XBase.Data.Office.AdminManager
{
    public class EquipmentFitDBHelper
    {
        /// <summary>
        /// 设备配件添加操作
        /// </summary>
        /// <param name="model">设备配件信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddEquipmentFitInfo(EquipmentFitModel EquipFitModel)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.EquipmentFittings");
                sql.AppendLine("		(FitXH      ");
                sql.AppendLine("		,FittingNO        ");
                sql.AppendLine("		,CompanyCD        ");
                sql.AppendLine("		,EquipmentID        ");
                sql.AppendLine("		,FittingName        ");
                sql.AppendLine("		,FittingDescription        ");
                sql.AppendLine("		,EquiName        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@FitXH     ");
                sql.AppendLine("		,@FittingNO       ");
                sql.AppendLine("		,@CompanyCD       ");
                sql.AppendLine("		,@EquipmentID       ");
                sql.AppendLine("		,@FittingName       ");
                sql.AppendLine("		,@FittingDescription       ");
                sql.AppendLine("		,@EquiName       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                //设置参数
                SqlParameter[] param;
                param = new SqlParameter[9];
                param[0] = SqlHelper.GetParameter("@FitXH", EquipFitModel.FitXH);
                param[1] = SqlHelper.GetParameter("@FittingNO", EquipFitModel.FittingNO);
                param[2] = SqlHelper.GetParameter("@CompanyCD", EquipFitModel.CompanyCD);
                param[3] = SqlHelper.GetParameter("@EquipmentID", EquipFitModel.EquipmentID);
                param[4] = SqlHelper.GetParameter("@FittingName", EquipFitModel.FittingName);
                param[5] = SqlHelper.GetParameter("@FittingDescription", EquipFitModel.FittingDescription);
                param[6] = SqlHelper.GetParameter("@EquiName", EquipFitModel.EquiName);
                param[7] = SqlHelper.GetParameter("@ModifiedDate", EquipFitModel.ModifiedDate);
                param[8] = SqlHelper.GetParameter("@ModifiedUserID", EquipFitModel.ModifiedUserID);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}
