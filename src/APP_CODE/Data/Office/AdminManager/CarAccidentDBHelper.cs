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
    /// 类名：CarAccidentDBHelper
    /// 描述：车辆维护数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarAccidentDBHelper
    {
        #region 添加车辆事故信息
        /// <summary>
        /// 添加车辆事故信息
        /// </summary>
        /// <param name="CarAccidentInfos">车辆事故信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool AddCarAccidentInfo(string CarAccidentInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarAccidentModel CarAccidentM = new CarAccidentModel();
                string[] GasInfoArrary = CarAccidentInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//发生日期
                    string AccidentPlace = gasfield[4].ToString();//事故地点
                    string AccidentDescription = gasfield[5].ToString();//事故描述
                    string Transactor = gasfield[6].ToString();//我方处理人
                    string DamageLevel = gasfield[7].ToString();//损坏程度
                    string ComBurdenSum = gasfield[8].ToString();//公司赔偿
                    string InsurBurdenSum = gasfield[9].ToString();//保险赔偿
                    string SelfBurdenSum = gasfield[10].ToString();//驾驶人赔偿
                    string OpposeCompens = gasfield[11].ToString();//对方赔偿
                    string Reconciliation = gasfield[12].ToString();//和解内容
                    string OpposeInfo = gasfield[13].ToString();//对方情况描述
                    string DealResult = gasfield[14].ToString();//处理结果
                    string Remark = gasfield[15].ToString();//备注

                    CarAccidentM.AccidentDescription = AccidentDescription;
                    CarAccidentM.AccidentPlace = AccidentPlace;
                    CarAccidentM.CarNo = CarNo;
                    CarAccidentM.ComBurdenSum =Convert.ToDecimal(ComBurdenSum);
                    CarAccidentM.CompanyCD = CompanyID;
                    CarAccidentM.DamageLevel = DamageLevel;
                    CarAccidentM.DealResult = DealResult;
                    CarAccidentM.EmployeeID =Convert.ToInt32(User);
                    CarAccidentM.HappenDate =Convert.ToDateTime(HappenDate);
                    CarAccidentM.InsurBurdenSum =Convert.ToDecimal(InsurBurdenSum);
                    CarAccidentM.ModifiedDate = System.DateTime.Now;
                    CarAccidentM.ModifiedUserID = userID;
                    CarAccidentM.OpposeCompens =Convert.ToDecimal(OpposeCompens);
                    CarAccidentM.OpposeInfo = OpposeInfo;
                    CarAccidentM.Reamrk = Remark;
                    CarAccidentM.Reconciliation = Reconciliation;
                    CarAccidentM.SelfBurdenSum =Convert.ToDecimal(SelfBurdenSum);
                    CarAccidentM.Transactor =Convert.ToInt32(Transactor);

                    #region 拼写添加保险记录信息sql语句
                    StringBuilder sqlgas = new StringBuilder();
                    sqlgas.AppendLine("INSERT INTO officedba.CarAccident");
                    sqlgas.AppendLine("(CompanyCD");
                    sqlgas.AppendLine(",CarNo     ");
                    sqlgas.AppendLine(",EmployeeID");
                    sqlgas.AppendLine(",HappenDate   ");
                    sqlgas.AppendLine(",AccidentPlace    ");
                    sqlgas.AppendLine(",AccidentDescription    ");
                    sqlgas.AppendLine(",Transactor    ");
                    sqlgas.AppendLine(",DamageLevel    ");
                    sqlgas.AppendLine(",ComBurdenSum    ");
                    sqlgas.AppendLine(",InsurBurdenSum    ");
                    sqlgas.AppendLine(",SelfBurdenSum    ");
                    sqlgas.AppendLine(",OpposeCompens    ");
                    sqlgas.AppendLine(",Reconciliation    ");
                    sqlgas.AppendLine(",OpposeInfo    ");
                    sqlgas.AppendLine(",DealResult    ");
                    sqlgas.AppendLine(",Remark    ");
                    sqlgas.AppendLine(",ModifiedDate");
                    sqlgas.AppendLine(",ModifiedUserID)");
                    sqlgas.AppendLine(" values ");
                    sqlgas.AppendLine("(@CompanyCD");
                    sqlgas.AppendLine(",@CarNo     ");
                    sqlgas.AppendLine(",@EmployeeID");
                    sqlgas.AppendLine(",@HappenDate   ");
                    sqlgas.AppendLine(",@AccidentPlace    ");
                    sqlgas.AppendLine(",@AccidentDescription    ");
                    sqlgas.AppendLine(",@Transactor    ");
                    sqlgas.AppendLine(",@DamageLevel    ");
                    sqlgas.AppendLine(",@ComBurdenSum    ");
                    sqlgas.AppendLine(",@InsurBurdenSum    ");
                    sqlgas.AppendLine(",@SelfBurdenSum    ");
                    sqlgas.AppendLine(",@OpposeCompens    ");
                    sqlgas.AppendLine(",@Reconciliation    ");
                    sqlgas.AppendLine(",@OpposeInfo    ");
                    sqlgas.AppendLine(",@DealResult    ");
                    sqlgas.AppendLine(",@Reamrk    ");
                    sqlgas.AppendLine(",@ModifiedDate");
                    sqlgas.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramgas = new SqlParameter[18];
                    paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAccidentM.CompanyCD);
                    paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAccidentM.CarNo);
                    paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAccidentM.EmployeeID);
                    paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAccidentM.HappenDate);
                    paramgas[4] = SqlHelper.GetParameter("@AccidentPlace", CarAccidentM.AccidentPlace);
                    paramgas[5] = SqlHelper.GetParameter("@AccidentDescription", CarAccidentM.AccidentDescription);
                    paramgas[6] = SqlHelper.GetParameter("@Transactor", CarAccidentM.Transactor);
                    paramgas[7] = SqlHelper.GetParameter("@DamageLevel", CarAccidentM.DamageLevel);
                    paramgas[8] = SqlHelper.GetParameter("@ComBurdenSum", CarAccidentM.ComBurdenSum);
                    paramgas[9] = SqlHelper.GetParameter("@InsurBurdenSum", CarAccidentM.InsurBurdenSum);
                    paramgas[10] = SqlHelper.GetParameter("@SelfBurdenSum", CarAccidentM.SelfBurdenSum);
                    paramgas[11] = SqlHelper.GetParameter("@OpposeCompens", CarAccidentM.OpposeCompens);
                    paramgas[12] = SqlHelper.GetParameter("@Reconciliation", CarAccidentM.Reconciliation);
                    paramgas[13] = SqlHelper.GetParameter("@OpposeInfo", CarAccidentM.OpposeInfo);
                    paramgas[14] = SqlHelper.GetParameter("@DealResult", CarAccidentM.DealResult);
                    paramgas[15] = SqlHelper.GetParameter("@Reamrk", CarAccidentM.Reamrk);
                    paramgas[16] = SqlHelper.GetParameter("@ModifiedDate", CarAccidentM.ModifiedDate);
                    paramgas[17] = SqlHelper.GetParameter("@ModifiedUserID", CarAccidentM.ModifiedUserID);
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

        #region 查询车辆事故信息列表
        /// <summary>
        /// 查询车辆事故信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarAccidentList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.CarNo,"
                           + "convert(varchar(10),a.HappenDate,120)HappenDate,a.DamageLevel,"
                           + "a.AccidentPlace,isnull(a.AccidentDescription,'')AccidentDescription,"
						   +"a.Transactor,a.ComburdenSum,a.InsurBurdenSum,a.SelfBurDenSum,"
                           + "a.OpposeCompens,isnull(a.Reconciliation,'')Reconciliation,a.OpposeInfo,a.DealResult,"
                           + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName,isnull(d.EmployeeName,'') as EmployeeName1 from officedba.CarAccident a "
                           +"LEFT OUTER JOIN "
                           +"officedba.CarInfo b "
                           + "ON a.CarNo=b.CarNo  and a.companycd=b.companycd "
                           +"LEFT OUTER JOIN "
                           +"officedba.EmployeeInfo c "
                           +"ON a.EmployeeID=c.ID "
					       +"LEFT OUTER JOIN "
                           +"officedba.EmployeeInfo d "
                           +"ON a.Transactor=d.ID  "
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

        #region 修改车辆事故信息
        /// <summary>
        /// 修改车辆事故信息
        /// </summary>
        /// <param name="CarAccidentInfos">车辆事故信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool UpdateCarAccidentInfo(string CarAccidentInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarAccidentModel CarAccidentM = new CarAccidentModel();
                string[] GasInfoArrary = CarAccidentInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//发生日期
                    string AccidentPlace = gasfield[4].ToString();//事故地点
                    string AccidentDescription = gasfield[5].ToString();//事故描述
                    string Transactor = gasfield[6].ToString();//我方处理人
                    string DamageLevel = gasfield[7].ToString();//损坏程度
                    string ComBurdenSum = gasfield[8].ToString();//公司赔偿
                    string InsurBurdenSum = gasfield[9].ToString();//保险赔偿
                    string SelfBurdenSum = gasfield[10].ToString();//驾驶人赔偿
                    string OpposeCompens = gasfield[11].ToString();//对方赔偿
                    string Reconciliation = gasfield[12].ToString();//和解内容
                    string OpposeInfo = gasfield[13].ToString();//对方情况描述
                    string DealResult = gasfield[14].ToString();//处理结果
                    string Remark = gasfield[15].ToString();//备注
                    string ID = gasfield[16].ToString();//ID

                    CarAccidentM.AccidentDescription = AccidentDescription;
                    CarAccidentM.AccidentPlace = AccidentPlace;
                    CarAccidentM.CarNo = CarNo;
                    CarAccidentM.ComBurdenSum = Convert.ToDecimal(ComBurdenSum);
                    CarAccidentM.CompanyCD = CompanyID;
                    CarAccidentM.DamageLevel = DamageLevel;
                    CarAccidentM.DealResult = DealResult;
                    CarAccidentM.EmployeeID = Convert.ToInt32(User);
                    CarAccidentM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarAccidentM.InsurBurdenSum = Convert.ToDecimal(InsurBurdenSum);
                    CarAccidentM.ModifiedDate = System.DateTime.Now;
                    CarAccidentM.ModifiedUserID = userID;
                    CarAccidentM.OpposeCompens = Convert.ToDecimal(OpposeCompens);
                    CarAccidentM.OpposeInfo = OpposeInfo;
                    CarAccidentM.Reamrk = Remark;
                    CarAccidentM.Reconciliation = Reconciliation;
                    CarAccidentM.SelfBurdenSum = Convert.ToDecimal(SelfBurdenSum);
                    CarAccidentM.Transactor = Convert.ToInt32(Transactor);

                    StringBuilder sqlgas = new StringBuilder();
                    if (ID == "0")
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("INSERT INTO officedba.CarAccident");
                        sqlgas.AppendLine("(CompanyCD");
                        sqlgas.AppendLine(",CarNo     ");
                        sqlgas.AppendLine(",EmployeeID");
                        sqlgas.AppendLine(",HappenDate   ");
                        sqlgas.AppendLine(",AccidentPlace    ");
                        sqlgas.AppendLine(",AccidentDescription    ");
                        sqlgas.AppendLine(",Transactor    ");
                        sqlgas.AppendLine(",DamageLevel    ");
                        sqlgas.AppendLine(",ComBurdenSum    ");
                        sqlgas.AppendLine(",InsurBurdenSum    ");
                        sqlgas.AppendLine(",SelfBurdenSum    ");
                        sqlgas.AppendLine(",OpposeCompens    ");
                        sqlgas.AppendLine(",Reconciliation    ");
                        sqlgas.AppendLine(",OpposeInfo    ");
                        sqlgas.AppendLine(",DealResult    ");
                        sqlgas.AppendLine(",Remark    ");
                        sqlgas.AppendLine(",ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID)");
                        sqlgas.AppendLine(" values ");
                        sqlgas.AppendLine("(@CompanyCD");
                        sqlgas.AppendLine(",@CarNo     ");
                        sqlgas.AppendLine(",@EmployeeID");
                        sqlgas.AppendLine(",@HappenDate   ");
                        sqlgas.AppendLine(",@AccidentPlace    ");
                        sqlgas.AppendLine(",@AccidentDescription    ");
                        sqlgas.AppendLine(",@Transactor    ");
                        sqlgas.AppendLine(",@DamageLevel    ");
                        sqlgas.AppendLine(",@ComBurdenSum    ");
                        sqlgas.AppendLine(",@InsurBurdenSum    ");
                        sqlgas.AppendLine(",@SelfBurdenSum    ");
                        sqlgas.AppendLine(",@OpposeCompens    ");
                        sqlgas.AppendLine(",@Reconciliation    ");
                        sqlgas.AppendLine(",@OpposeInfo    ");
                        sqlgas.AppendLine(",@DealResult    ");
                        sqlgas.AppendLine(",@Reamrk    ");
                        sqlgas.AppendLine(",@ModifiedDate");
                        sqlgas.AppendLine(",@ModifiedUserID)");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[18];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAccidentM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAccidentM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAccidentM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAccidentM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@AccidentPlace", CarAccidentM.AccidentPlace);
                        paramgas[5] = SqlHelper.GetParameter("@AccidentDescription", CarAccidentM.AccidentDescription);
                        paramgas[6] = SqlHelper.GetParameter("@Transactor", CarAccidentM.Transactor);
                        paramgas[7] = SqlHelper.GetParameter("@DamageLevel", CarAccidentM.DamageLevel);
                        paramgas[8] = SqlHelper.GetParameter("@ComBurdenSum", CarAccidentM.ComBurdenSum);
                        paramgas[9] = SqlHelper.GetParameter("@InsurBurdenSum", CarAccidentM.InsurBurdenSum);
                        paramgas[10] = SqlHelper.GetParameter("@SelfBurdenSum", CarAccidentM.SelfBurdenSum);
                        paramgas[11] = SqlHelper.GetParameter("@OpposeCompens", CarAccidentM.OpposeCompens);
                        paramgas[12] = SqlHelper.GetParameter("@Reconciliation", CarAccidentM.Reconciliation);
                        paramgas[13] = SqlHelper.GetParameter("@OpposeInfo", CarAccidentM.OpposeInfo);
                        paramgas[14] = SqlHelper.GetParameter("@DealResult", CarAccidentM.DealResult);
                        paramgas[15] = SqlHelper.GetParameter("@Reamrk", CarAccidentM.Reamrk);
                        paramgas[16] = SqlHelper.GetParameter("@ModifiedDate", CarAccidentM.ModifiedDate);
                        paramgas[17] = SqlHelper.GetParameter("@ModifiedUserID", CarAccidentM.ModifiedUserID);
                        #endregion
                        SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                        cmdgasinfo.Parameters.AddRange(paramgas);
                        comms[i - 1] = cmdgasinfo;
                    }
                    else
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("UPDATE officedba.CarAccident");
                        sqlgas.AppendLine(" SET CompanyCD=@CompanyCD");
                        sqlgas.AppendLine(",CarNo=@CarNo     ");
                        sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                        sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                        sqlgas.AppendLine(",AccidentPlace=@AccidentPlace    ");
                        sqlgas.AppendLine(",AccidentDescription=@AccidentDescription    ");
                        sqlgas.AppendLine(",Transactor=@Transactor    ");
                        sqlgas.AppendLine(",DamageLevel=@DamageLevel    ");
                        sqlgas.AppendLine(",ComBurdenSum=@ComBurdenSum    ");
                        sqlgas.AppendLine(",InsurBurdenSum=@InsurBurdenSum    ");
                        sqlgas.AppendLine(",SelfBurdenSum=@SelfBurdenSum    ");
                        sqlgas.AppendLine(",OpposeCompens=@OpposeCompens    ");
                        sqlgas.AppendLine(",Reconciliation=@Reconciliation    ");
                        sqlgas.AppendLine(",OpposeInfo=@OpposeInfo    ");
                        sqlgas.AppendLine(",DealResult=@DealResult    ");
                        sqlgas.AppendLine(",Remark=@Remark    ");
                        sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID");
                        sqlgas.AppendLine(" WHERE ID=@ID ");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[19];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAccidentM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAccidentM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAccidentM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAccidentM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@AccidentPlace", CarAccidentM.AccidentPlace);
                        paramgas[5] = SqlHelper.GetParameter("@AccidentDescription", CarAccidentM.AccidentDescription);
                        paramgas[6] = SqlHelper.GetParameter("@Transactor", CarAccidentM.Transactor);
                        paramgas[7] = SqlHelper.GetParameter("@DamageLevel", CarAccidentM.DamageLevel);
                        paramgas[8] = SqlHelper.GetParameter("@ComBurdenSum", CarAccidentM.ComBurdenSum);
                        paramgas[9] = SqlHelper.GetParameter("@InsurBurdenSum", CarAccidentM.InsurBurdenSum);
                        paramgas[10] = SqlHelper.GetParameter("@SelfBurdenSum", CarAccidentM.SelfBurdenSum);
                        paramgas[11] = SqlHelper.GetParameter("@OpposeCompens", CarAccidentM.OpposeCompens);
                        paramgas[12] = SqlHelper.GetParameter("@Reconciliation", CarAccidentM.Reconciliation);
                        paramgas[13] = SqlHelper.GetParameter("@OpposeInfo", CarAccidentM.OpposeInfo);
                        paramgas[14] = SqlHelper.GetParameter("@DealResult", CarAccidentM.DealResult);
                        paramgas[15] = SqlHelper.GetParameter("@Remark", CarAccidentM.Reamrk);
                        paramgas[16] = SqlHelper.GetParameter("@ModifiedDate", CarAccidentM.ModifiedDate);
                        paramgas[17] = SqlHelper.GetParameter("@ModifiedUserID", CarAccidentM.ModifiedUserID);
                        paramgas[18] = SqlHelper.GetParameter("@ID", Convert.ToInt32(ID));
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
        #region 删除车辆加油信息
        /// <summary>
        /// 删除车辆加油信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelCarAccidentByID(string CarInsuranceIDS)
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
                Delsql[0] = "DELETE FROM officedba.CarAccident WHERE ID IN (" + allApplyID + ")";
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
