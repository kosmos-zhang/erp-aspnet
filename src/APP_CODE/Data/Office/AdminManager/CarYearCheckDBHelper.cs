/**********************************************
 * 类作用：   车辆维护数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/04
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：CarInsuranceDBHelper
    /// 描述：车辆维护数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarYearCheckDBHelper
    {
        #region 添加车辆年检信息
        /// <summary>
        /// 添加车辆年检信息
        /// </summary>
        /// <param name="YearCheckInfos">车辆年检信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool AddCarYearCheckInfo(string YearCheckInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarRepairModel CarRepairM = new CarRepairModel();
                string[] GasInfoArrary = YearCheckInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[GasInfoArrary.Length - 1]; //申明cmd数组
                DateTime NowDate = System.DateTime.Now;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < GasInfoArrary.Length; i++) //循环数组
                {
                    recorditems = GasInfoArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string CarNo = gasfield[0].ToString();//车辆编号
                    string CarMark = gasfield[1].ToString();//车牌号
                    string User = gasfield[2].ToString();//经办人
                    string HappenDate = gasfield[3].ToString();//年检日期
                    string RepairFee = gasfield[4].ToString();//年检费用
                    string Remark = gasfield[5].ToString();//备注


                    CarRepairM.CarNo = CarNo;
                    CarRepairM.CompanyCD = CompanyID;
                    CarRepairM.EmployeeID = Convert.ToInt32(User);
                    CarRepairM.Fee = Convert.ToDecimal(RepairFee);
                    CarRepairM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarRepairM.ModifiedDate = NowDate;
                    CarRepairM.ModifiedUserID = userID;
                    CarRepairM.Remark = Remark;

                    #region 拼写添加维修记录信息sql语句
                    StringBuilder sqlgas = new StringBuilder();
                    sqlgas.AppendLine("INSERT INTO officedba.CarYearCheck");
                    sqlgas.AppendLine("(CompanyCD");
                    sqlgas.AppendLine(",CarNo     ");
                    sqlgas.AppendLine(",EmployeeID");
                    sqlgas.AppendLine(",HappenDate   ");
                    sqlgas.AppendLine(",Fee    ");
                    sqlgas.AppendLine(",Remark    ");
                    sqlgas.AppendLine(",ModifiedDate");
                    sqlgas.AppendLine(",ModifiedUserID)");
                    sqlgas.AppendLine(" values ");
                    sqlgas.AppendLine("(@CompanyCD");
                    sqlgas.AppendLine(",@CarNo     ");
                    sqlgas.AppendLine(",@EmployeeID");
                    sqlgas.AppendLine(",@HappenDate   ");
                    sqlgas.AppendLine(",@Fee    ");
                    sqlgas.AppendLine(",@Remark    ");
                    sqlgas.AppendLine(",@ModifiedDate");
                    sqlgas.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramgas = new SqlParameter[8];
                    paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarRepairM.CompanyCD);
                    paramgas[1] = SqlHelper.GetParameter("@CarNo", CarRepairM.CarNo);
                    paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarRepairM.EmployeeID);
                    paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarRepairM.HappenDate);
                    paramgas[4] = SqlHelper.GetParameter("@Fee", CarRepairM.Fee);
                    paramgas[5] = SqlHelper.GetParameter("@Remark", CarRepairM.Remark);
                    paramgas[6] = SqlHelper.GetParameter("@ModifiedDate", CarRepairM.ModifiedDate);
                    paramgas[7] = SqlHelper.GetParameter("@ModifiedUserID", CarRepairM.ModifiedUserID);
                    #endregion
                    SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                    cmdgasinfo.Parameters.AddRange(paramgas);
                    comms[i - 1] = cmdgasinfo;
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region 查询车辆年检信息列表
        /// <summary>
        /// 查询车辆年检信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarYearCheckList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.Fee,a.CarNo,"
                           + "convert(varchar(10),a.HappenDate,120)HappenDate,"
                           + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName from officedba.CarYearCheck a "
                           + "LEFT OUTER JOIN "
                           + "officedba.CarInfo b "
                           + "ON a.CarNo=b.CarNo  and a.companycd=b.companycd "
                           + "LEFT OUTER JOIN "
                           + "officedba.EmployeeInfo c "
                           + "ON a.EmployeeID=c.ID  "
                           + "WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (CarNo != "")
                sql += " and a.CarNo LIKE '%" + CarNo + "%'";
            if (CarMark != "")
                sql += " and b.CarMark LIKE '%" + CarMark + "%'";
            if (JoinUser != "")
                sql += " and c.EmployeeName LIKE '%" + JoinUser + "%'";
            if (StartGasDate != "")
                sql += " and a.HappenDate>= '" + StartGasDate + "'";
            if (EndGasDate != "")
                sql += " and a.HappenDate<= '" + EndGasDate + "'";

            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 修改车辆年检信息
        /// <summary>
        /// 修改车辆年检信息
        /// </summary>
        /// <param name="YearCheckInfos">车辆年检信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool UpdateYearCheckInfo(string YearCheckInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarRepairModel CarRepairM = new CarRepairModel();
                string[] GasInfoArrary = YearCheckInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[GasInfoArrary.Length - 1];//申明cmd数组
                DateTime NowDate = System.DateTime.Now;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < GasInfoArrary.Length; i++) //循环数组
                {
                    recorditems = GasInfoArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string CarNo = gasfield[0].ToString();//车辆编号
                    string CarMark = gasfield[1].ToString();//车牌号
                    string User = gasfield[2].ToString();//经办人
                    string HappenDate = gasfield[3].ToString();//年检日期
                    string RepairFee = gasfield[4].ToString();//年检费用
                    string Remark = gasfield[5].ToString();//备注
                    string ID = gasfield[6].ToString();//ID

                    CarRepairM.CarNo = CarNo;
                    CarRepairM.CompanyCD = CompanyID;
                    CarRepairM.EmployeeID = Convert.ToInt32(User);
                    CarRepairM.Fee = Convert.ToDecimal(RepairFee);
                    CarRepairM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarRepairM.ModifiedDate = NowDate;
                    CarRepairM.ModifiedUserID = userID;
                    CarRepairM.Remark = Remark;

                    StringBuilder sqlgas = new StringBuilder();
                    if (ID == "0")
                    {
                        #region 拼写添加年检记录信息sql语句
                        sqlgas.AppendLine("INSERT INTO officedba.CarYearCheck");
                        sqlgas.AppendLine("(CompanyCD");
                        sqlgas.AppendLine(",CarNo     ");
                        sqlgas.AppendLine(",EmployeeID");
                        sqlgas.AppendLine(",HappenDate   ");
                        sqlgas.AppendLine(",Fee    ");
                        sqlgas.AppendLine(",Remark    ");
                        sqlgas.AppendLine(",ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID)");
                        sqlgas.AppendLine(" values ");
                        sqlgas.AppendLine("(@CompanyCD");
                        sqlgas.AppendLine(",@CarNo     ");
                        sqlgas.AppendLine(",@EmployeeID");
                        sqlgas.AppendLine(",@HappenDate   ");
                        sqlgas.AppendLine(",@Fee    ");
                        sqlgas.AppendLine(",@Remark    ");
                        sqlgas.AppendLine(",@ModifiedDate");
                        sqlgas.AppendLine(",@ModifiedUserID)");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[8];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarRepairM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarRepairM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarRepairM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarRepairM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@Fee", CarRepairM.Fee);
                        paramgas[5] = SqlHelper.GetParameter("@Remark", CarRepairM.Remark);
                        paramgas[6] = SqlHelper.GetParameter("@ModifiedDate", CarRepairM.ModifiedDate);
                        paramgas[7] = SqlHelper.GetParameter("@ModifiedUserID", CarRepairM.ModifiedUserID);
                        #endregion
                        SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                        cmdgasinfo.Parameters.AddRange(paramgas);
                        comms[i - 1] = cmdgasinfo;
                    }
                    else
                    {
                        #region 拼写添加维修记录信息sql语句
                        sqlgas.AppendLine("UPDATE officedba.CarYearCheck");
                        sqlgas.AppendLine("SET CompanyCD=@CompanyCD");
                        sqlgas.AppendLine(",CarNo=@CarNo     ");
                        sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                        sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                        sqlgas.AppendLine(",Fee=@Fee    ");
                        sqlgas.AppendLine(",Remark=@Remark    ");
                        sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID");
                        sqlgas.AppendLine("  WHERE ID=@ID ");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[9];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarRepairM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarRepairM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarRepairM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarRepairM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@Fee", CarRepairM.Fee);
                        paramgas[5] = SqlHelper.GetParameter("@Remark", CarRepairM.Remark);
                        paramgas[6] = SqlHelper.GetParameter("@ModifiedDate", CarRepairM.ModifiedDate);
                        paramgas[7] = SqlHelper.GetParameter("@ModifiedUserID", CarRepairM.ModifiedUserID);
                        paramgas[8] = SqlHelper.GetParameter("@ID", Convert.ToInt32(ID));
                        #endregion
                        SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                        cmdgasinfo.Parameters.AddRange(paramgas);
                        comms[i - 1] = cmdgasinfo;
                    }
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
       #region 删除车辆年检信息
       /// <summary>
        /// 删除车辆年检信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelCarYearCheckByID(string CarInsuranceIDS)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] IDS = null;
               IDS = CarInsuranceIDS.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   IDS[i] = "'" + IDS[i] + "'";
                   sb.Append(IDS[i]);
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.CarYearCheck WHERE ID IN (" + allApplyID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion
    }
}
