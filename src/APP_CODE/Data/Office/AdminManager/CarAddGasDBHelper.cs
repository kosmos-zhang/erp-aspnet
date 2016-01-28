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
    /// 类名：CarAddGasDBHelper
    /// 描述：车辆维护数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/04
    /// </summary>
   public class CarAddGasDBHelper
   {
        #region 添加加油信息
        /// <summary>
        /// 添加加油信息
        /// </summary>
        /// <param name="WorkGroupInfos">加油信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
       public static int AddGasInfo(string GasInfos, string userID, string CompanyID,int Employeeid)
        {
           try{
               CarAddGasModel CarAddGasM = new CarAddGasModel();
                string[] GasInfoArrary = GasInfos.Split('|'); //把联系人列表流分隔成数组
                SqlCommand[] comms = new SqlCommand[GasInfoArrary.Length-1]; //申明cmd数组
                DateTime NowDate = System.DateTime.Now;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < GasInfoArrary.Length; i++) //循环数组
                {
                    recorditems = GasInfoArrary[i].ToString();
                    gasfield = recorditems.Split(','); 

                    string CarNo = gasfield[0].ToString();//车辆编号
                    string fieldname = gasfield[1].ToString();//车牌号
                    string fieldhandset = gasfield[2].ToString();//经办人
                    string HappenDate = gasfield[3].ToString();//加油日期
                    string GasCount = gasfield[4].ToString();//加油量
                    string Fee = gasfield[5].ToString();//总金额
                    string BillNo = gasfield[6].ToString();//相关票据号
                    string Remark = gasfield[7].ToString();//备注

                    if (BillNo != "") 
                    {
                        if (IsExistInfo(CarNo, BillNo, CompanyID)) 
                        {
                            return 3;
                        }
                    }

                    CarAddGasM.BillNo = BillNo;
                    CarAddGasM.CarNo = CarNo;
                    CarAddGasM.CompanyCD = CompanyID;
                    CarAddGasM.EmployeeID =Convert.ToInt32(fieldhandset);
                    CarAddGasM.Fee = Convert.ToDecimal(Fee);
                    CarAddGasM.GasCount = Convert.ToDecimal(GasCount);
                    CarAddGasM.HappenDate = Convert.ToDateTime(HappenDate);
                    CarAddGasM.ModifiedDate = NowDate;
                    CarAddGasM.ModifiedUserID = userID;
                    CarAddGasM.Remark = Remark;

                    #region 拼写添加加油记录信息sql语句
                    StringBuilder sqlgas = new StringBuilder();
                    sqlgas.AppendLine("INSERT INTO officedba.CarAddGas");
                    sqlgas.AppendLine("(CompanyCD");
                    sqlgas.AppendLine(",CarNo     ");
                    sqlgas.AppendLine(",EmployeeID");
                    sqlgas.AppendLine(",HappenDate   ");
                    sqlgas.AppendLine(",GasCount  ");
                    sqlgas.AppendLine(",Fee    ");
                    sqlgas.AppendLine(",BillNo    ");
                    sqlgas.AppendLine(",Remark    ");
                    sqlgas.AppendLine(",ModifiedDate");
                    sqlgas.AppendLine(",ModifiedUserID)");
                    sqlgas.AppendLine(" values ");
                    sqlgas.AppendLine("(@CompanyCD");
                    sqlgas.AppendLine(",@CarNo     ");
                    sqlgas.AppendLine(",@EmployeeID");
                    sqlgas.AppendLine(",@HappenDate   ");
                    sqlgas.AppendLine(",@GasCount  ");
                    sqlgas.AppendLine(",@Fee    ");
                    sqlgas.AppendLine(",@BillNo    ");
                    sqlgas.AppendLine(",@Remark    ");
                    sqlgas.AppendLine(",@ModifiedDate");
                    sqlgas.AppendLine(",@ModifiedUserID)");
                    #endregion

                    #region 设置参数
                    SqlParameter[] paramgas = new SqlParameter[10];
                    paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAddGasM.CompanyCD);
                    paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAddGasM.CarNo);
                    paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAddGasM.EmployeeID);
                    paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAddGasM.HappenDate);
                    paramgas[4] = SqlHelper.GetParameter("@GasCount", CarAddGasM.GasCount);
                    paramgas[5] = SqlHelper.GetParameter("@Fee", CarAddGasM.Fee);
                    paramgas[6] = SqlHelper.GetParameter("@BillNo", CarAddGasM.BillNo);
                    paramgas[7] = SqlHelper.GetParameter("@Remark", CarAddGasM.Remark);
                    paramgas[8] = SqlHelper.GetParameter("@ModifiedDate", CarAddGasM.ModifiedDate);
                    paramgas[9] = SqlHelper.GetParameter("@ModifiedUserID", CarAddGasM.ModifiedUserID);
                    #endregion
                    SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString()); 
                    cmdgasinfo.Parameters.AddRange(paramgas);
                    comms[i-1] = cmdgasinfo; 
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
       #region 是否存在重复的车辆编号和票据编号
       /// <summary>
       /// 是否存在重复的车辆编号和票据编号
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string CarNo,string BillNo, string CompanyID)
       {

           string sql = "SELECT * FROM officedba.CarAddGas WHERE CarNo='" + CarNo + "' AND CompanyCD='" + CompanyID + "' AND BillNo='"+BillNo+"'";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
       #region 修改加油信息
       /// <summary>
       /// 修改加油信息
       /// </summary>
       /// <param name="WorkGroupInfos">加油信息</param>
       /// <param name="userID">用户ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns>成功：true,失败:false</returns>
       public static int UpdateGasInfo(string GasInfos, string userID, string CompanyID, int Employeeid)
       {
           try
           {
               CarAddGasModel CarAddGasM = new CarAddGasModel();
               string[] GasInfoArrary = GasInfos.Split('|'); //把联系人列表流分隔成数组
               SqlCommand[] comms = new SqlCommand[GasInfoArrary.Length - 1]; //申明cmd数组
               DateTime NowDate = System.DateTime.Now;

               string recorditems = "";
               string[] gasfield = null;

               for (int i = 1; i < GasInfoArrary.Length; i++) //循环数组
               {
                   recorditems = GasInfoArrary[i].ToString();
                   gasfield = recorditems.Split(',');

                   string CarNo = gasfield[0].ToString();//车辆编号
                   string fieldname = gasfield[1].ToString();//车牌号
                   string fieldhandset = gasfield[2].ToString();//经办人
                   string HappenDate = gasfield[3].ToString();//加油日期
                   string GasCount = gasfield[4].ToString();//加油量
                   string Fee = gasfield[5].ToString();//总金额
                   string BillNo = gasfield[6].ToString();//相关票据号
                   string Remark = gasfield[7].ToString();//备注
                   string ID = gasfield[8].ToString();//ID

                   CarAddGasM.BillNo = BillNo;
                   CarAddGasM.CarNo = CarNo;
                   CarAddGasM.CompanyCD = CompanyID;
                   CarAddGasM.EmployeeID = Convert.ToInt32(fieldhandset);
                   CarAddGasM.Fee = Convert.ToDecimal(Fee);
                   CarAddGasM.GasCount = Convert.ToDecimal(GasCount);
                   CarAddGasM.HappenDate = Convert.ToDateTime(HappenDate);
                   CarAddGasM.ModifiedDate = NowDate;
                   CarAddGasM.ModifiedUserID = userID;
                   CarAddGasM.Remark = Remark;

                   
                   StringBuilder sqlgas = new StringBuilder();
                   if (ID == "0")
                   {
                       if (BillNo != "")
                       {
                           if (IsExistInfo(CarNo, BillNo, CompanyID))
                           {
                               return 3;
                           }
                       }
                       #region 拼写添加加油记录信息sql语句
                       sqlgas.AppendLine("INSERT INTO officedba.CarAddGas");
                       sqlgas.AppendLine("(CompanyCD");
                       sqlgas.AppendLine(",CarNo     ");
                       sqlgas.AppendLine(",EmployeeID");
                       sqlgas.AppendLine(",HappenDate   ");
                       sqlgas.AppendLine(",GasCount  ");
                       sqlgas.AppendLine(",Fee    ");
                       sqlgas.AppendLine(",BillNo    ");
                       sqlgas.AppendLine(",Remark    ");
                       sqlgas.AppendLine(",ModifiedDate");
                       sqlgas.AppendLine(",ModifiedUserID)");
                       sqlgas.AppendLine(" values ");
                       sqlgas.AppendLine("(@CompanyCD");
                       sqlgas.AppendLine(",@CarNo     ");
                       sqlgas.AppendLine(",@EmployeeID");
                       sqlgas.AppendLine(",@HappenDate   ");
                       sqlgas.AppendLine(",@GasCount  ");
                       sqlgas.AppendLine(",@Fee    ");
                       sqlgas.AppendLine(",@BillNo    ");
                       sqlgas.AppendLine(",@Remark    ");
                       sqlgas.AppendLine(",@ModifiedDate");
                       sqlgas.AppendLine(",@ModifiedUserID)");
                       #endregion

                       #region 设置参数
                       SqlParameter[] paramgas = new SqlParameter[10];
                       paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAddGasM.CompanyCD);
                       paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAddGasM.CarNo);
                       paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAddGasM.EmployeeID);
                       paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAddGasM.HappenDate);
                       paramgas[4] = SqlHelper.GetParameter("@GasCount", CarAddGasM.GasCount);
                       paramgas[5] = SqlHelper.GetParameter("@Fee", CarAddGasM.Fee);
                       paramgas[6] = SqlHelper.GetParameter("@BillNo", CarAddGasM.BillNo);
                       paramgas[7] = SqlHelper.GetParameter("@Remark", CarAddGasM.Remark);
                       paramgas[8] = SqlHelper.GetParameter("@ModifiedDate", CarAddGasM.ModifiedDate);
                       paramgas[9] = SqlHelper.GetParameter("@ModifiedUserID", CarAddGasM.ModifiedUserID);
                       #endregion
                       SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                       cmdgasinfo.Parameters.AddRange(paramgas);
                       comms[i - 1] = cmdgasinfo;
                   }
                   else 
                   {
                       #region 拼写添加加油记录信息sql语句
                       sqlgas.AppendLine("UPDATE officedba.CarAddGas");
                       sqlgas.AppendLine("SET CompanyCD=@CompanyCD");
                       sqlgas.AppendLine(",CarNo=@CarNo     ");
                       sqlgas.AppendLine(",EmployeeID=@EmployeeID");
                       sqlgas.AppendLine(",HappenDate=@HappenDate   ");
                       sqlgas.AppendLine(",GasCount=@GasCount  ");
                       sqlgas.AppendLine(",Fee=@Fee    ");
                       sqlgas.AppendLine(",BillNo=@BillNo    ");
                       sqlgas.AppendLine(",Remark=@Remark    ");
                       sqlgas.AppendLine(",ModifiedDate=@ModifiedDate");
                       sqlgas.AppendLine(",ModifiedUserID=@ModifiedUserID");
                       sqlgas.AppendLine(" WHERE ID=@ID ");

                       #endregion

                       #region 设置参数
                       SqlParameter[] paramgas = new SqlParameter[11];
                       paramgas[0] = SqlHelper.GetParameter("@CompanyCD", CarAddGasM.CompanyCD);
                       paramgas[1] = SqlHelper.GetParameter("@CarNo", CarAddGasM.CarNo);
                       paramgas[2] = SqlHelper.GetParameter("@EmployeeID", CarAddGasM.EmployeeID);
                       paramgas[3] = SqlHelper.GetParameter("@HappenDate", CarAddGasM.HappenDate);
                       paramgas[4] = SqlHelper.GetParameter("@GasCount", CarAddGasM.GasCount);
                       paramgas[5] = SqlHelper.GetParameter("@Fee", CarAddGasM.Fee);
                       paramgas[6] = SqlHelper.GetParameter("@BillNo", CarAddGasM.BillNo);
                       paramgas[7] = SqlHelper.GetParameter("@Remark", CarAddGasM.Remark);
                       paramgas[8] = SqlHelper.GetParameter("@ModifiedDate", CarAddGasM.ModifiedDate);
                       paramgas[9] = SqlHelper.GetParameter("@ModifiedUserID", CarAddGasM.ModifiedUserID);
                       paramgas[10] = SqlHelper.GetParameter("@ID",Convert.ToInt32(ID));
                       #endregion
                       SqlCommand cmdgasinfo = new SqlCommand(sqlgas.ToString());
                       cmdgasinfo.Parameters.AddRange(paramgas);
                       comms[i - 1] = cmdgasinfo;
                   }
               }
               //执行
               SqlHelper.ExecuteTransForList(comms);
               return SqlHelper.Result.OprateCount > 0 ? 1 : 0;
           }
           catch
           {
               return 0;
           }
       }
       #endregion

       #region 查询车辆加油信息列表
       /// <summary>
       /// 查询车辆加油信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetCarGasList(string CompanyID, string CarNo, string CarMark, string JoinUser, string StartGasDate, string EndGasDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.EmployeeID,isnull(a.Remark,'')Remark,a.CarNo,convert(varchar(10),a.HappenDate,120)HappenDate,a.GasCount,a.Fee,isnull(a.BillNo,'')BillNo,"
                          + "b.CarMark,isnull(c.EmployeeName,'')EmployeeName from officedba.CarAddGas a "
                          +"LEFT OUTER JOIN "
                          +"officedba.CarInfo b "
                          + "ON a.CarNo=b.CarNo  and a.companycd=b.companycd "
                          +"LEFT OUTER JOIN "
                          +"officedba.EmployeeInfo c "
                          +"ON a.EmployeeID=c.ID  "
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
       #region 删除车辆加油信息
       /// <summary>
       /// 删除车辆加油信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelCarGasByID(string CarInsuranceIDS)
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
               Delsql[0] = "DELETE FROM officedba.CarAddGas WHERE ID IN (" + allApplyID + ")";
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
