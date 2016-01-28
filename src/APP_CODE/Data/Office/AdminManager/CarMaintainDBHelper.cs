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
    /// 类名：CarMaintainDBHelper
    /// 描述：车辆维护数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarMaintainDBHelper
    {
        #region 添加车辆保养信息
        /// <summary>
        /// 添加车辆保养信息
        /// </summary>
        /// <param name="MaintainInfos">车辆保养信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool AddCarMaintainInfo(string MaintainInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarMaintainModel CarMaintainM = new CarMaintainModel();
                string[] GasInfoArrary = MaintainInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//保养日期
                    string Fee = gasfield[4].ToString();//保养费用
                    string Items = gasfield[5].ToString();//保养项目
                    string NowMileage = gasfield[6].ToString();//当前里程数
                    string NextMileage = gasfield[7].ToString();//下次保养里程数
                    string Remark = gasfield[8].ToString();//备注


                    CarMaintainM.CarNo = CarNo;
                    CarMaintainM.CompanyCD = CompanyID;
                    CarMaintainM.EmployeeID = Convert.ToInt32(User);
                    CarMaintainM.Fee = Convert.ToDecimal(Fee);
                    CarMaintainM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarMaintainM.Items = Items;
                    CarMaintainM.NowMileage = Convert.ToDecimal(NowMileage);
                    CarMaintainM.NextMileage = Convert.ToDecimal(NextMileage);
                    CarMaintainM.ModifiedDate = NowDate;
                    CarMaintainM.ModifiedUserID = userID;
                    CarMaintainM.Remark = Remark;
                    StringBuilder sqlgas = new StringBuilder();
                    
                        #region 拼写添加维修记录信息sql语句
                        sqlgas.AppendLine("INSERT INTO officedba.CarMaintain");
                        sqlgas.AppendLine("(CompanyCD");
                        sqlgas.AppendLine(",CarNo     ");
                        sqlgas.AppendLine(",EmployeeID");
                        sqlgas.AppendLine(",HappenDate   ");
                        sqlgas.AppendLine(",Fee  ");
                        sqlgas.AppendLine(",Items    ");
                        sqlgas.AppendLine(",NowMileage    ");
                        sqlgas.AppendLine(",NextMileage    ");
                        sqlgas.AppendLine(",Remark    ");
                        sqlgas.AppendLine(",ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID)");
                        sqlgas.AppendLine(" values ");
                        sqlgas.AppendLine("(@CompanyCD");
                        sqlgas.AppendLine(",@CarNo     ");
                        sqlgas.AppendLine(",@EmployeeID");
                        sqlgas.AppendLine(",@HappenDate   ");
                        sqlgas.AppendLine(",@Fee   ");
                        sqlgas.AppendLine(",@Items  ");
                        sqlgas.AppendLine(",@NowMileage    ");
                        sqlgas.AppendLine(",@NextMileage    ");
                        sqlgas.AppendLine(",@Remark    ");
                        sqlgas.AppendLine(",@ModifiedDate");
                        sqlgas.AppendLine(",@ModifiedUserID)");
                        #endregion
                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[11];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarMaintainM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarMaintainM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarMaintainM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarMaintainM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@Fee", CarMaintainM.Fee);
                        paramgas[5] = SqlHelper.GetParameter("@Items", CarMaintainM.Items);
                        paramgas[6] = SqlHelper.GetParameter("@NowMileage", CarMaintainM.NowMileage);
                        paramgas[7] = SqlHelper.GetParameter("@NextMileage", CarMaintainM.NextMileage);
                        paramgas[8] = SqlHelper.GetParameter("@Remark", CarMaintainM.Remark);
                        paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarMaintainM.ModifiedDate);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarMaintainM.ModifiedUserID);
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
       /// <summary>
       /// 当天是否做过保养
       /// </summary>
       /// <param name="CarNo"></param>
       /// <param name="CompanyCD"></param>
       /// <param name="HappenDate"></param>
       /// <returns></returns>
        public static bool isexist(string CarNo,string CompanyCD,string HappenDate) 
        {
            string sql = "SELECT * FROM officedba.CarMaintain WHERE CarNo='" + CarNo + "' AND CompanyCD='" + CompanyCD + "' AND HappenDate='" + HappenDate + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #region 查询车辆保养信息列表
        /// <summary>
        /// 查询车辆保养信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarMaintainList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.CarNo,"                  
                           +"convert(varchar(10),a.HappenDate,120)HappenDate,"
                           +"a.Fee,a.Items,a.NowMileage,a.NextMileage,"
                           + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName from officedba.CarMaintain a "
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

        #region 修改车辆保养信息
        /// <summary>
        /// 修改车辆保养信息
        /// </summary>
        /// <param name="MaintainInfos">车辆保养信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
        public static bool UpdateMaintainInfo(string MaintainInfos, string userID, string CompanyID, int Employeeid)
        {
            try
            {
                CarMaintainModel CarMaintainM = new CarMaintainModel();
                string[] GasInfoArrary = MaintainInfos.Split('|');
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
                    string HappenDate = gasfield[3].ToString();//保养日期
                    string Fee = gasfield[4].ToString();//保养费用
                    string Items = gasfield[5].ToString();//保养项目
                    string NowMileage = gasfield[6].ToString();//当前里程数
                    string NextMileage = gasfield[7].ToString();//下次保养里程数
                    string Remark = gasfield[8].ToString();//备注
                    string ID = gasfield[9].ToString();//ID

                    CarMaintainM.CarNo = CarNo;
                    CarMaintainM.CompanyCD = CompanyID;
                    CarMaintainM.EmployeeID = Convert.ToInt32(User);
                    CarMaintainM.Fee = Convert.ToDecimal(Fee);
                    CarMaintainM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarMaintainM.Items = Items;
                    CarMaintainM.NowMileage = Convert.ToDecimal(NowMileage);
                    CarMaintainM.NextMileage = Convert.ToDecimal(NextMileage);
                    CarMaintainM.ModifiedDate = NowDate;
                    CarMaintainM.ModifiedUserID = userID;
                    CarMaintainM.Remark = Remark;

                    StringBuilder sqlgas = new StringBuilder();
                    if (ID == "0")
                    {
                        if (!isexist(CarNo, CompanyID, HappenDate))
                        {
                            #region 拼写添加保养记录信息sql语句
                            sqlgas.AppendLine("INSERT INTO officedba.CarMaintain");
                            sqlgas.AppendLine("(CompanyCD");
                            sqlgas.AppendLine(",CarNo     ");
                            sqlgas.AppendLine(",EmployeeID");
                            sqlgas.AppendLine(",HappenDate   ");
                            sqlgas.AppendLine(",Fee  ");
                            sqlgas.AppendLine(",Items    ");
                            sqlgas.AppendLine(",NowMileage    ");
                            sqlgas.AppendLine(",NextMileage    ");
                            sqlgas.AppendLine(",Remark    ");
                            sqlgas.AppendLine(",ModifiedDate");
                            sqlgas.AppendLine(",ModifiedUserID)");
                            sqlgas.AppendLine(" values ");
                            sqlgas.AppendLine("(@CompanyCD");
                            sqlgas.AppendLine(",@CarNo     ");
                            sqlgas.AppendLine(",@EmployeeID");
                            sqlgas.AppendLine(",@HappenDate   ");
                            sqlgas.AppendLine(",@Fee   ");
                            sqlgas.AppendLine(",@Items  ");
                            sqlgas.AppendLine(",@NowMileage    ");
                            sqlgas.AppendLine(",@NextMileage    ");
                            sqlgas.AppendLine(",@Remark    ");
                            sqlgas.AppendLine(",@ModifiedDate");
                            sqlgas.AppendLine(",@ModifiedUserID)");
                            #endregion

                            #region 设置参数
                            SqlParameter[] paramgas = new SqlParameter[11];
                            paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarMaintainM.CompanyCD);
                            paramgas[1] = SqlHelper.GetParameter("@CarNo", CarMaintainM.CarNo);
                            paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarMaintainM.EmployeeID);
                            paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarMaintainM.HappenDate);
                            paramgas[4] = SqlHelper.GetParameter("@Fee", CarMaintainM.Fee);
                            paramgas[5] = SqlHelper.GetParameter("@Items", CarMaintainM.Items);
                            paramgas[6] = SqlHelper.GetParameter("@NowMileage", CarMaintainM.NowMileage);
                            paramgas[7] = SqlHelper.GetParameter("@NextMileage", CarMaintainM.NextMileage);
                            paramgas[8] = SqlHelper.GetParameter("@Remark", CarMaintainM.Remark);
                            paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarMaintainM.ModifiedDate);
                            paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarMaintainM.ModifiedUserID);
                            #endregion
                            SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                            cmdgasinfo.Parameters.AddRange(paramgas);
                            comms[i - 1] = cmdgasinfo;
                        }
                        else 
                        {
                            #region 拼写添加保养记录信息sql语句
                            sqlgas.AppendLine("UPDATE officedba.CarMaintain");
                            sqlgas.AppendLine(" SET EmployeeID=@EmployeeID");
                            sqlgas.AppendLine(",Fee=@Fee  ");
                            sqlgas.AppendLine(",Items=@Items    ");
                            sqlgas.AppendLine(",NowMileage=@NowMileage    ");
                            sqlgas.AppendLine(",NextMileage=@NextMileage    ");
                            sqlgas.AppendLine(",Remark=@Remark    ");
                            sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                            sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID ");
                            sqlgas.AppendLine("  WHERE CompanyCD=@CompanyCD AND CarNo=@CarNo AND HappenDate=@HappenDate ");
                            #endregion

                            #region 设置参数
                            SqlParameter[] paramgas = new SqlParameter[11];
                            paramgas[0] = SqlHelper.GetParameter("@EmployeeID", CarMaintainM.EmployeeID);
                            paramgas[1] = SqlHelper.GetParameter("@Fee", CarMaintainM.Fee);
                            paramgas[2] = SqlHelper.GetParameter("@Items", CarMaintainM.Items);
                            paramgas[3] = SqlHelper.GetParameter("@NowMileage", CarMaintainM.NowMileage);
                            paramgas[4] = SqlHelper.GetParameter("@NextMileage", CarMaintainM.NextMileage);
                            paramgas[5] = SqlHelper.GetParameter("@Remark", CarMaintainM.Remark);
                            paramgas[6] = SqlHelper.GetParameter("@ModifiedDate", CarMaintainM.ModifiedDate);
                            paramgas[7] = SqlHelper.GetParameter("@ModifiedUserID", CarMaintainM.ModifiedUserID);
                            paramgas[8] = SqlHelper.GetParameter("@CompanyCD", CarMaintainM.CompanyCD);
                            paramgas[9] = SqlHelper.GetParameter("@CarNo", CarMaintainM.CarNo);
                            paramgas[10] = SqlHelper.GetParameter("@HappenDate", CarMaintainM.HappenDate);
                            #endregion
                            SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                            cmdgasinfo.Parameters.AddRange(paramgas);
                            comms[i - 1] = cmdgasinfo;
                        }
                    }
                    else
                    {
                        #region 拼写添加保养记录信息sql语句
                        sqlgas.AppendLine("UPDATE officedba.CarMaintain");
                        sqlgas.AppendLine(" SET CompanyCD=@CompanyCD");
                        sqlgas.AppendLine(",CarNo=@CarNo     ");
                        sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                        sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                        sqlgas.AppendLine(",Fee=@Fee  ");
                        sqlgas.AppendLine(",Items=@Items    ");
                        sqlgas.AppendLine(",NowMileage=@NowMileage    ");
                        sqlgas.AppendLine(",NextMileage=@NextMileage    ");
                        sqlgas.AppendLine(",Remark=@Remark    ");
                        sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                        sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID ");
                        sqlgas.AppendLine("  WHERE ID=@ID ");
                        #endregion

                        #region 设置参数
                        SqlParameter[] paramgas = new SqlParameter[12];
                        paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarMaintainM.CompanyCD);
                        paramgas[1] = SqlHelper.GetParameter("@CarNo", CarMaintainM.CarNo);
                        paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarMaintainM.EmployeeID);
                        paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarMaintainM.HappenDate);
                        paramgas[4] = SqlHelper.GetParameter("@Fee", CarMaintainM.Fee);
                        paramgas[5] = SqlHelper.GetParameter("@Items", CarMaintainM.Items);
                        paramgas[6] = SqlHelper.GetParameter("@NowMileage", CarMaintainM.NowMileage);
                        paramgas[7] = SqlHelper.GetParameter("@NextMileage", CarMaintainM.NextMileage);
                        paramgas[8] = SqlHelper.GetParameter("@Remark", CarMaintainM.Remark);
                        paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", CarMaintainM.ModifiedDate);
                        paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", CarMaintainM.ModifiedUserID);
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
        #region 删除车辆保养信息
        /// <summary>
        /// 删除车辆保养信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelCarMaintainByID(string CarInsuranceIDS)
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
                Delsql[0] = "DELETE FROM officedba.CarMaintain WHERE ID IN (" + allApplyID + ")";
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
