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
    /// 类名：CarPeccancyDBHelper
    /// 描述：车辆维护数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
    public class CarPeccancyDBHelper
    {
        #region 添加车辆违章记录信息
        /// <summary>
        /// 添加车辆违章记录信息
        /// </summary>
        /// <param name="CarPeccancyInfos">车辆违章信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool AddCarPeccancyInfo(string CarPeccancyInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarPeccancyModel CarPeccancyM = new CarPeccancyModel();
                string[] GasInfoArrary = CarPeccancyInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//违章日期
                    string HappenPlace = gasfield[4].ToString();//违章时间
                    string Fee = gasfield[5].ToString();//处罚金额
                    string Party = gasfield[6].ToString();//同行人员
                    string Description = gasfield[7].ToString();//违章描述
                    string Remark = gasfield[8].ToString();//备注

                    CarPeccancyM.CarNo = CarNo;
                    CarPeccancyM.CompanyCD = CompanyID;
                    CarPeccancyM.Description = Description;
                    CarPeccancyM.EmployeeID = Convert.ToInt32(User);
                    CarPeccancyM.Fee = Convert.ToDecimal(Fee);
                    CarPeccancyM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarPeccancyM.HappenPlace = HappenPlace;
                    CarPeccancyM.ModifiedDate = System.DateTime.Now;
                    CarPeccancyM.ModifiedUserID = userID;
                    CarPeccancyM.Party = Party;
                    CarPeccancyM.Remark = Remark;
                    

                    #region 拼写添加保险记录信息sql语句
                    StringBuilder sqlgas = new StringBuilder();
                    sqlgas.AppendLine("INSERT INTO officedba.CarPeccancy");
                    sqlgas.AppendLine("(CompanyCD");
                    sqlgas.AppendLine(",CarNo     ");
                    sqlgas.AppendLine(",EmployeeID");
                    sqlgas.AppendLine(",HappenDate   ");
                    sqlgas.AppendLine(",HappenPlace    ");
                    sqlgas.AppendLine(",Description    ");
                    sqlgas.AppendLine(",Party    ");
                    sqlgas.AppendLine(",Fee    ");
                    sqlgas.AppendLine(",Remark    ");
                    sqlgas.AppendLine(",ModifiedDate");
                    sqlgas.AppendLine(",ModifiedUserID)");
                    sqlgas.AppendLine(" values ");
                    sqlgas.AppendLine("(@CompanyCD");
                    sqlgas.AppendLine(",@CarNo     ");
                    sqlgas.AppendLine(",@EmployeeID");
                    sqlgas.AppendLine(",@HappenDate   ");
                    sqlgas.AppendLine(",@HappenPlace    ");
                    sqlgas.AppendLine(",@Description    ");
                    sqlgas.AppendLine(",@Party    ");
                    sqlgas.AppendLine(",@Fee    ");
                    sqlgas.AppendLine(",@Remark    ");
                    sqlgas.AppendLine(",@ModifiedDate");
                    sqlgas.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramgas = new SqlParameter[11];
                    paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarPeccancyM.CompanyCD);
                    paramgas[1] = SqlHelper.GetParameter("@CarNo", CarPeccancyM.CarNo);
                    paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarPeccancyM.EmployeeID);
                    paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarPeccancyM.HappenDate);
                    paramgas[4] = SqlHelper.GetParameter("@HappenPlace", CarPeccancyM.HappenPlace);
                    paramgas[5] = SqlHelper.GetParameter("@Description", CarPeccancyM.Description);
                    paramgas[6] = SqlHelper.GetParameter("@Party", CarPeccancyM.Party);
                    paramgas[7] = SqlHelper.GetParameter("@Fee", CarPeccancyM.Fee);
                    paramgas[8] = SqlHelper.GetParameter("@Remark", CarPeccancyM.Remark);
                    paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarPeccancyM.ModifiedDate);
                    paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarPeccancyM.ModifiedUserID);
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

        #region 查询车辆违章信息列表
        /// <summary>
        /// 查询车辆违章信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarPeccancyList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.CarNo,"
                           +"convert(varchar(10),a.HappenDate,120)HappenDate,"
                           +"a.Fee,a.HappenPlace,a.Description,"
                           +"a.Party,"
                           + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName from officedba.CarPeccancy a "
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

        #region 修改车辆违章信息
        /// <summary>
        /// 修改车辆违章信息
        /// </summary>
        /// <param name="CarPeccancyInfos">车辆违章信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool UpdateCarPeeccancyInfo(string CarPeccancyInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarPeccancyModel CarPeccancyM = new CarPeccancyModel();
                string[] GasInfoArrary = CarPeccancyInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//违章日期
                    string HappenPlace = gasfield[4].ToString();//违章时间
                    string Fee = gasfield[5].ToString();//处罚金额
                    string Party = gasfield[6].ToString();//同行人员
                    string Description = gasfield[7].ToString();//违章描述
                    string Remark = gasfield[8].ToString();//备注
                    string ID = gasfield[9].ToString();//ID

                    CarPeccancyM.CarNo = CarNo;
                    CarPeccancyM.CompanyCD = CompanyID;
                    CarPeccancyM.Description = Description;
                    CarPeccancyM.EmployeeID = Convert.ToInt32(User);
                    CarPeccancyM.Fee = Convert.ToDecimal(Fee);
                    CarPeccancyM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarPeccancyM.HappenPlace = HappenPlace;
                    CarPeccancyM.ModifiedDate = System.DateTime.Now;
                    CarPeccancyM.ModifiedUserID = userID;
                    CarPeccancyM.Party = Party;
                    CarPeccancyM.Remark = Remark;

                    StringBuilder sqlgas = new StringBuilder();
                    if (ID == "0")
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("INSERT INTO officedba.CarPeccancy");
                        sqlgas.AppendLine("(CompanyCD");
                        sqlgas.AppendLine(",CarNo     ");
                        sqlgas.AppendLine(",EmployeeID");
                        sqlgas.AppendLine(",HappenDate   ");
                        sqlgas.AppendLine(",HappenPlace    ");
                        sqlgas.AppendLine(",Description    ");
                        sqlgas.AppendLine(",Party    ");
                        sqlgas.AppendLine(",Fee    ");
                        sqlgas.AppendLine(",Remark    ");
                        sqlgas.AppendLine(",ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID)");
                        sqlgas.AppendLine(" values ");
                        sqlgas.AppendLine("(@CompanyCD");
                        sqlgas.AppendLine(",@CarNo     ");
                        sqlgas.AppendLine(",@EmployeeID");
                        sqlgas.AppendLine(",@HappenDate   ");
                        sqlgas.AppendLine(",@HappenPlace    ");
                        sqlgas.AppendLine(",@Description    ");
                        sqlgas.AppendLine(",@Party    ");
                        sqlgas.AppendLine(",@Fee    ");
                        sqlgas.AppendLine(",@Remark    ");
                        sqlgas.AppendLine(",@ModifiedDate");
                        sqlgas.AppendLine(",@ModifiedUserID)");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[11];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarPeccancyM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarPeccancyM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarPeccancyM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarPeccancyM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@HappenPlace", CarPeccancyM.HappenPlace);
                        paramgas[5] = SqlHelper.GetParameter("@Description", CarPeccancyM.Description);
                        paramgas[6] = SqlHelper.GetParameter("@Party", CarPeccancyM.Party);
                        paramgas[7] = SqlHelper.GetParameter("@Fee", CarPeccancyM.Fee);
                        paramgas[8] = SqlHelper.GetParameter("@Remark", CarPeccancyM.Remark);
                        paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarPeccancyM.ModifiedDate);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarPeccancyM.ModifiedUserID);
                        #endregion
                        SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                        cmdgasinfo.Parameters.AddRange(paramgas);
                        comms[i - 1] = cmdgasinfo;
                    }
                    else
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("UPDATE officedba.CarPeccancy");
                        sqlgas.AppendLine(" SET CompanyCD=@CompanyCD");
                        sqlgas.AppendLine(",CarNo=@CarNo     ");
                        sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                        sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                        sqlgas.AppendLine(",HappenPlace=@HappenPlace    ");
                        sqlgas.AppendLine(",Description=@Description    ");
                        sqlgas.AppendLine(",Party=@Party    ");
                        sqlgas.AppendLine(",Fee=@Fee    ");
                        sqlgas.AppendLine(",Remark=@Remark    ");
                        sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID ");
                        sqlgas.AppendLine(" WHERE ID=@ID ");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[12];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarPeccancyM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarPeccancyM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarPeccancyM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarPeccancyM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@HappenPlace", CarPeccancyM.HappenPlace);
                        paramgas[5] = SqlHelper.GetParameter("@Description", CarPeccancyM.Description);
                        paramgas[6] = SqlHelper.GetParameter("@Party", CarPeccancyM.Party);
                        paramgas[7] = SqlHelper.GetParameter("@Fee", CarPeccancyM.Fee);
                        paramgas[8] = SqlHelper.GetParameter("@Remark", CarPeccancyM.Remark);
                        paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarPeccancyM.ModifiedDate);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarPeccancyM.ModifiedUserID);
                        paramgas[11] = SqlHelper.GetParameter("@ID", Convert.ToInt32(ID));
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
        #region 删除车辆违章信息
        /// <summary>
        /// 删除车辆违章信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelCarPeccancyByID(string CarInsuranceIDS)
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
                Delsql[0] = "DELETE FROM officedba.CarPeccancy WHERE ID IN (" + allApplyID + ")";
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
