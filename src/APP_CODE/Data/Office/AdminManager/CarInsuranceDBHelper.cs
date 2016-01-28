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
   public class CarInsuranceDBHelper
    {
        #region 添加车辆保险信息
        /// <summary>
        /// 添加车辆保险信息
        /// </summary>
        /// <param name="InSuranceInfos">车辆保险信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool AddCarInsuranceInfo(string InSuranceInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarInsuranceModel CarInsuranceM = new CarInsuranceModel();
                string[] GasInfoArrary = InSuranceInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//保险日期
                    string InsuranceCompany = gasfield[4].ToString();//保险公司
                    string Fee = gasfield[5].ToString();//保险费用
                    string BillNo = gasfield[6].ToString();//保单号
                    string StartDate = gasfield[7].ToString();//生效日期
                    string EndDate = gasfield[8].ToString();//失效日期
                    string Remark = gasfield[9].ToString();//备注

                    CarInsuranceM.BillNo = BillNo;
                    CarInsuranceM.CarNo = CarNo;
                    CarInsuranceM.CompanyCD = CompanyID;
                    CarInsuranceM.EmployeeID =Convert.ToInt32(User);
                    CarInsuranceM.EndDate = Convert.ToDateTime(EndDate);
                    CarInsuranceM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarInsuranceM.InsuranceCompany = InsuranceCompany;
                    CarInsuranceM.Fee = Convert.ToDecimal(Fee);
                    CarInsuranceM.ModifiedDate = System.DateTime.Now;
                    CarInsuranceM.ModifiedUserID = userID;
                    CarInsuranceM.Remark = Remark;
                    CarInsuranceM.StartDate = Convert.ToDateTime(EndDate);

                    #region 拼写添加保险记录信息sql语句
                    StringBuilder sqlgas = new StringBuilder();
                    sqlgas.AppendLine("INSERT INTO officedba.CarInsurance");
                    sqlgas.AppendLine("(CompanyCD");
                    sqlgas.AppendLine(",CarNo     ");
                    sqlgas.AppendLine(",EmployeeID");
                    sqlgas.AppendLine(",HappenDate   ");
                    sqlgas.AppendLine(",InsuranceCompany    ");
                    sqlgas.AppendLine(",Fee    ");
                    sqlgas.AppendLine(",BillNo    ");
                    sqlgas.AppendLine(",StartDate    ");
                    sqlgas.AppendLine(",EndDate    ");
                    sqlgas.AppendLine(",Remark    ");
                    sqlgas.AppendLine(",ModifiedDate");
                    sqlgas.AppendLine(",ModifiedUserID)");
                    sqlgas.AppendLine(" values ");
                    sqlgas.AppendLine("(@CompanyCD");
                    sqlgas.AppendLine(",@CarNo     ");
                    sqlgas.AppendLine(",@EmployeeID");
                    sqlgas.AppendLine(",@HappenDate   ");
                    sqlgas.AppendLine(",@InsuranceCompany    ");
                    sqlgas.AppendLine(",@Fee    ");
                    sqlgas.AppendLine(",@BillNo    ");
                    sqlgas.AppendLine(",@StartDate    ");
                    sqlgas.AppendLine(",@EndDate    ");
                    sqlgas.AppendLine(",@Remark    ");
                    sqlgas.AppendLine(",@ModifiedDate");
                    sqlgas.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramgas = new SqlParameter[12];
                    paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarInsuranceM.CompanyCD);
                    paramgas[1] = SqlHelper.GetParameter("@CarNo", CarInsuranceM.CarNo);
                    paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarInsuranceM.EmployeeID);
                    paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarInsuranceM.HappenDate);
                    paramgas[4] = SqlHelper.GetParameter("@InsuranceCompany", CarInsuranceM.InsuranceCompany);
                    paramgas[5] = SqlHelper.GetParameter("@Fee", CarInsuranceM.Fee);
                    paramgas[6] = SqlHelper.GetParameter("@BillNo", CarInsuranceM.BillNo);
                    paramgas[7] = SqlHelper.GetParameter("@StartDate", CarInsuranceM.StartDate);
                    paramgas[8] = SqlHelper.GetParameter("@EndDate", CarInsuranceM.EndDate);
                    paramgas[9] = SqlHelper.GetParameter("@Remark", CarInsuranceM.Remark);
                    paramgas[10] = SqlHelper.GetParameter("@ModifiedDate", CarInsuranceM.ModifiedDate);
                    paramgas[11] = SqlHelper.GetParameter("@ModifiedUserID", CarInsuranceM.ModifiedUserID);
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

        #region 查询车辆保险信息列表
        /// <summary>
        /// 查询车辆保险信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInsuranceList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.CarNo,"
                           +"convert(varchar(10),a.HappenDate,120)HappenDate,"
                           +"a.Fee,a.InsuranceCompany,a.BillNo,convert(varchar(10),a.StartDate,120)StartDate,"
						   +"convert(varchar(10),a.EndDate,120)EndDate,"
                           + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName from officedba.CarInsurance a "
                           + "LEFT OUTER JOIN "
                           + "officedba.CarInfo b "
                           + "ON a.CarNo=b.CarNo and  a.companycd=b.companycd "
                           + "LEFT OUTER JOIN "
                           + "officedba.EmployeeInfo  c "
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

        #region 修改车辆保险信息
        /// <summary>
        /// 修改车辆保险信息
        /// </summary>
        /// <param name="InSuranceInfos">车辆保险信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool UpdateCarInsuranceInfo(string InSuranceInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarInsuranceModel CarInsuranceM = new CarInsuranceModel();
                string[] GasInfoArrary = InSuranceInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//保险日期
                    string InsuranceCompany = gasfield[4].ToString();//保险公司
                    string Fee = gasfield[5].ToString();//保险费用
                    string BillNo = gasfield[6].ToString();//保单号
                    string StartDate = gasfield[7].ToString();//生效日期
                    string EndDate = gasfield[8].ToString();//失效日期
                    string Remark = gasfield[9].ToString();//备注
                    string ID = gasfield[10].ToString();//ID

                    CarInsuranceM.BillNo = BillNo;
                    CarInsuranceM.CarNo = CarNo;
                    CarInsuranceM.CompanyCD = CompanyID;
                    CarInsuranceM.EmployeeID = Convert.ToInt32(User);
                    CarInsuranceM.EndDate = Convert.ToDateTime(EndDate);
                    CarInsuranceM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarInsuranceM.InsuranceCompany = InsuranceCompany;
                    CarInsuranceM.Fee = Convert.ToDecimal(Fee);
                    CarInsuranceM.ModifiedDate = System.DateTime.Now;
                    CarInsuranceM.ModifiedUserID = userID;
                    CarInsuranceM.Remark = Remark;
                    CarInsuranceM.StartDate = Convert.ToDateTime(StartDate);

                    StringBuilder sqlgas = new StringBuilder();
                    if (ID == "0")
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("INSERT INTO officedba.CarInsurance");
                        sqlgas.AppendLine("(CompanyCD");
                        sqlgas.AppendLine(",CarNo     ");
                        sqlgas.AppendLine(",EmployeeID");
                        sqlgas.AppendLine(",HappenDate   ");
                        sqlgas.AppendLine(",InsuranceCompany    ");
                        sqlgas.AppendLine(",Fee    ");
                        sqlgas.AppendLine(",BillNo    ");
                        sqlgas.AppendLine(",StartDate    ");
                        sqlgas.AppendLine(",EndDate    ");
                        sqlgas.AppendLine(",Remark    ");
                        sqlgas.AppendLine(",ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID)");
                        sqlgas.AppendLine(" values ");
                        sqlgas.AppendLine("(@CompanyCD");
                        sqlgas.AppendLine(",@CarNo     ");
                        sqlgas.AppendLine(",@EmployeeID");
                        sqlgas.AppendLine(",@HappenDate   ");
                        sqlgas.AppendLine(",@InsuranceCompany    ");
                        sqlgas.AppendLine(",@Fee    ");
                        sqlgas.AppendLine(",@BillNo    ");
                        sqlgas.AppendLine(",@StartDate    ");
                        sqlgas.AppendLine(",@EndDate    ");
                        sqlgas.AppendLine(",@Remark    ");
                        sqlgas.AppendLine(",@ModifiedDate");
                        sqlgas.AppendLine(",@ModifiedUserID)");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[12];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarInsuranceM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarInsuranceM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarInsuranceM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarInsuranceM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@InsuranceCompany", CarInsuranceM.InsuranceCompany);
                        paramgas[5] = SqlHelper.GetParameter("@Fee", CarInsuranceM.Fee);
                        paramgas[6] = SqlHelper.GetParameter("@BillNo", CarInsuranceM.BillNo);
                        paramgas[7] = SqlHelper.GetParameter("@StartDate", CarInsuranceM.StartDate);
                        paramgas[8] = SqlHelper.GetParameter("@EndDate", CarInsuranceM.EndDate);
                        paramgas[9] = SqlHelper.GetParameter("@Remark", CarInsuranceM.Remark);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedDate", CarInsuranceM.ModifiedDate);
                        paramgas[11] = SqlHelper.GetParameter("@ModifiedUserID", CarInsuranceM.ModifiedUserID);
                        #endregion
                        SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                        cmdgasinfo.Parameters.AddRange(paramgas);
                        comms[i - 1] = cmdgasinfo;
                    }
                    else
                    {
                        #region 拼写添加保险记录信息sql语句
                        sqlgas.AppendLine("UPDATE officedba.CarInsurance");
                        sqlgas.AppendLine(" SET CompanyCD=@CompanyCD");
                        sqlgas.AppendLine(",CarNo=@CarNo     ");
                        sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                        sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                        sqlgas.AppendLine(",InsuranceCompany=@InsuranceCompany    ");
                        sqlgas.AppendLine(",Fee=@Fee    ");
                        sqlgas.AppendLine(",BillNo=@BillNo    ");
                        sqlgas.AppendLine(",StartDate=@StartDate    ");
                        sqlgas.AppendLine(",EndDate=@EndDate    ");
                        sqlgas.AppendLine(",Remark=@Remark    ");
                        sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID");
                        sqlgas.AppendLine(" WHERE ID=@ID ");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[13];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarInsuranceM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarInsuranceM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarInsuranceM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarInsuranceM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@InsuranceCompany", CarInsuranceM.InsuranceCompany);
                        paramgas[5] = SqlHelper.GetParameter("@Fee", CarInsuranceM.Fee);
                        paramgas[6] = SqlHelper.GetParameter("@BillNo", CarInsuranceM.BillNo);
                        paramgas[7] = SqlHelper.GetParameter("@StartDate", CarInsuranceM.StartDate);
                        paramgas[8] = SqlHelper.GetParameter("@EndDate", CarInsuranceM.EndDate);
                        paramgas[9] = SqlHelper.GetParameter("@Remark", CarInsuranceM.Remark);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedDate", CarInsuranceM.ModifiedDate);
                        paramgas[11] = SqlHelper.GetParameter("@ModifiedUserID", CarInsuranceM.ModifiedUserID);
                        paramgas[12] = SqlHelper.GetParameter("@ID",Convert.ToInt32(ID));
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

        #region 删除车辆保险信息
        /// <summary>
        /// 删除车辆保险信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelCarDispatchByID(string CarInsuranceIDS)
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
                Delsql[0] = "DELETE FROM officedba.CarInsurance WHERE ID IN (" + allApplyID + ")";
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
